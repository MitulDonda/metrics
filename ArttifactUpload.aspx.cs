using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data.OleDb;
using System.Data;

public partial class ArttifactUpload : System.Web.UI.Page
{
    string oledbConn;
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void load_excel_Click(object sender, EventArgs e)
    {
        List<ArtifactExcel> listArtifact = new List<ArtifactExcel>();
        //if File is not selected then return  
        if (Request.Files["FileUpload1"].ContentLength <= 0)
        { return; }
        try
        {
            //Get the file extension  
            string fileExtension = Path.GetExtension(Request.Files["FileUpload1"].FileName);

        //If file is not in excel format then return  
        if (fileExtension != ".xls" && fileExtension != ".xlsx")
        { return; }

        //Get the File name and create new path to save it on server  
        string fileLocation = Server.MapPath("\\") + Request.Files["FileUpload1"].FileName;

        //if the File is exist on serevr then delete it  
        if (File.Exists(fileLocation))
        {
            File.Delete(fileLocation);
        }
        //save the file lon the server before loading  
        Request.Files["FileUpload1"].SaveAs(fileLocation);


        //Create the QueryString for differnt version of fexcel file  
        
        switch (fileExtension)
        {
            case ".xls": //Excel 1997-2003  

                    oledbConn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + fileLocation + ";Extended Properties = 'Excel 8.0;HDR=Yes;IMEX=1'; ";
                    break;
            case ".xlsx": //Excel 2007-2010  
                    oledbConn = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=" + fileLocation + ";Extended Properties = 'Excel 12.0;HDR=YES;IMEX=1;'; ";
                break;
        }




            using (OleDbConnection connection = new OleDbConnection(oledbConn))
            {
                OleDbCommand command = new OleDbCommand("SELECT * FROM [Sheet1$]", connection);

                connection.Open();
                OleDbDataReader reader = command.ExecuteReader();




                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        ArtifactExcel artifact = new ArtifactExcel();
                        artifact.artNum = reader[0].ToString(); ;
                        artifact.artName = reader[2].ToString(); ;
                        artifact.esthour = Convert.ToInt32(reader[4].ToString()); 
                        artifact.desc = reader[3].ToString(); ;
                        artifact.createdby = reader[1].ToString();

                        listArtifact.Add(artifact);
                    }
                }
               
              
                reader.Close();

                for (int i = 0; i < listArtifact.Count; i++)
                {
                   int id = DstumDAL.getIdfromName(listArtifact[i].createdby);
                    int toolid = DstumDAL.gettoolIdfromName(listArtifact[i].createdby);
                    listArtifact[i].id = id;
                    listArtifact[i].toolid = toolid;
                }
                bool isArtifactAdded = DstumDAL.addArtifactFromExcel(listArtifact);

                if (isArtifactAdded)
                {
                    Response.Redirect("~/Default.aspx");
                }
            }
           
         


          
       ///     GridView1.DataSource = ds;
            GridView1.DataBind();
         
            //Bind the datatable to the Grid  
            File.Delete(fileLocation);
        }
        catch (Exception ex)
        {
        }
        //Delete the excel file from the server  
      
    }
}