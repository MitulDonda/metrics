using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ClosedXML.Excel;
using System.Configuration;
using System.Web.Security;
using System.Data.SqlClient;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using System.Data.OleDb;
using System.Data;

using Excel = Microsoft.Office.Interop.Excel;


public partial class _Default : System.Web.UI.Page
{
    string oledbConn;
    User user = new User();
    static string connString1 = ConfigurationManager.ConnectionStrings["DSTUMConnectionString"].ConnectionString;


    public static SqlConnection conn =
        new SqlConnection(ConfigurationManager.ConnectionStrings["DSTUMConnectionString"].ConnectionString);
    public static SqlCommand cmd;
    public static SqlDataReader dr;

    protected void Page_Load(object sender, EventArgs e)
    {
        bool isuser = DstumDAL.isUserExist(Context.User.Identity.Name);

        if (!isuser)
        {
            Response.Redirect("~/ContactToAdmin.aspx");
        }
        else
        {
            user = DstumDAL.getUserdetails(Context.User.Identity.Name);
            if (user.role.ToString().Equals("User"))
            {
                divbutton.Visible = false;

            }
        }
        if (!IsPostBack)
        {
            toolDropdownbind();
            DataBindGrid();

        }
    }
    protected void addArtifact_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Default2.aspx");
    }

    protected void addUser_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/AddUser.aspx");
    }
    protected void btndstumsheet_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/DstumSheet.aspx");
    }
    protected void addtool_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/ADDTools.aspx");
    }


    protected void btnLockHour_Click(object sender, EventArgs e)
    {

        bool inset = false;
        DSTUM dstum = new DSTUM();
        User user1 = DstumDAL.getUserdetails(Context.User.Identity.Name);
        dstum.userid = user1.userid;
        dstum.Todaydate = DateTime.Now.ToString("yyyy/MM/dd"); ;
        dstum.aid = Convert.ToInt32(ddlairtifact.SelectedValue.ToString());
        dstum.Todayhour = Convert.ToInt32(txttodayhour.Text.ToString());
        dstum.yesterdayHour = Convert.ToInt32(txtYesterdayhour.Text.ToString());
        dstum.desc = txtdesc.Text.ToString();





        try
        {
            inset = DstumDAL.lockHour(dstum);
        }
        catch (Exception ex)
        {

        }
        if (inset == true)
        {
            Response.Write(@"<script language='javascript'>window.alert('Lock Your hour successfully \n ');</script>");

            txtdesc.Text = "";
            txttodayhour.Text = "";
            txtYesterdayhour.Text = "";
            DataBindGrid();
        }
        else
        {
            Response.Write(@"<script language='javascript'>alert('some error is occurs \n ');</script>");

        }
    }


    public void toolDropdownbind()
    {
        User user1 = DstumDAL.getUserdetails(Context.User.Identity.Name);
        String sCommand1_subbu1 = "select ar.AID,ar.ArtifactName from artifact ar where createdfor=@userid ";
        sCommand1_subbu1 += " union select '-1' as aid, 'Meeting/KT/Other' as ArtifactName ";

        //  DataTable dataSubbu1 = new DataTable();
        SqlDataAdapter adapterSubbu1 = new SqlDataAdapter(sCommand1_subbu1, connString1);
        adapterSubbu1.SelectCommand.Parameters.AddWithValue("@userid", user1.userid);

        DataSet ds = new DataSet();
        adapterSubbu1.Fill(ds);

        ddlairtifact.DataSource = ds;
        ddlairtifact.DataTextField = "ArtifactName";
        ddlairtifact.DataValueField = "AID";
        ddlairtifact.DataBind();
        conn.Close();
    }

    private void DataBindGrid()
    {
        user = DstumDAL.getUserdetails(Context.User.Identity.Name);

        String sCommand1_subbu1 = "select   ds.DID,  replace(convert(NVARCHAR, ds.todaydate, 106), ' ', '/') todaydate,  ds.userid,  ds.aid,  ds.Todayhour,  ds.yesterdayhour,  ds.[desc],  us.Name,  CASE      WHEN Cast(ar.[ArtifactName] AS varchar) is null THEN 'Meeting/KT/Other'     ELSE Cast(ar.[ArtifactName] AS varchar)     END AS ArtifactName, CASE      WHEN Cast(ar.est_hour AS varchar) is null THEN 0     ELSE Cast(ar.est_hour AS varchar)      END AS est_hour, CASE      WHEN Cast(ar.est_hour AS varchar) is null THEN 0     ELSE Cast(ar.est_hour-ar.lock_hour AS varchar)      END AS remhour,  ar.status from dstum ds left outer join [user] us on ds.userid = us.userid left outer join artifact ar on ar.AID=ds.AID where us.userID=@userid";
        //  DataTable dataSubbu1 = new DataTable();
        using (SqlDataAdapter adapterSubbu1 = new SqlDataAdapter(sCommand1_subbu1, connString1))
        {
            adapterSubbu1.SelectCommand.Parameters.AddWithValue("@userid", user.userid);

            DataSet ds = new DataSet();
            adapterSubbu1.Fill(ds);

            GridView1.DataSource = ds;


            GridView1.DataBind();
            conn.Close();
        }
    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {

        if (e.CommandName == "EditRow")
        {
            int rowIndex = ((GridViewRow)((Button)e.CommandSource).NamingContainer).RowIndex;
            GridView1.EditIndex = rowIndex;
            DataBindGrid();
        }
        else if (e.CommandName == "CancelRow")
        {
            GridView1.EditIndex = -1;
            DataBindGrid();
        }
        else if (e.CommandName == "UpdateRow")
        {



            int RowIndex = ((GridViewRow)((Button)e.CommandSource).NamingContainer).RowIndex;

            int employeeId = Convert.ToInt32(e.CommandArgument);
            //    string name = ((TextBox)GridView1.Rows[rowIndex].FindControl("TextBox1")).Text;
            //    string gender = ((DropDownList)GridView1.Rows[rowIndex].FindControl("DropDownList1")).SelectedValue;
            //  string city = ((TextBox)GridView1.Rows[rowIndex].FindControl("TextBox3")).Text;


            string id = GridView1.DataKeys[RowIndex].Value.ToString();


            string desc = ((TextBox)GridView1.Rows[RowIndex].FindControl("txtdesc")).Text;
            string txtodayhour = ((TextBox)GridView1.Rows[RowIndex].FindControl("txtodayhour")).Text;
            var ddlartname = ((DropDownList)GridView1.Rows[RowIndex].FindControl("ddlartname")).SelectedValue.ToString();

            if (ddlartname.Equals("-1"))
            {
                String query = "update dstum set [desc]=@desc,todayhour=@todayhout where DID=@did";
                cmd = new SqlCommand(query, conn);



                cmd.Parameters.AddWithValue("@desc", desc);
                cmd.Parameters.AddWithValue("@todayhout", txtodayhour);

                cmd.Parameters.AddWithValue("@did", id);
            }
            else
            {
                String query = "update dstum set aid=@aid ,[desc]=@desc,todayhour=@todayhout where DID=@did";

                cmd = new SqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@desc", desc);
                cmd.Parameters.AddWithValue("@todayhout", txtodayhour);
                cmd.Parameters.AddWithValue("@aid", ddlartname);
                cmd.Parameters.AddWithValue("@did", id);
            }

            conn.Open();
            int dr = cmd.ExecuteNonQuery();
            if (dr > 0)
            {


                GridView1.EditIndex = -1;
                DataBindGrid();


                // inserted = true;
            }
            conn.Close();



        }
    }

    protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
    {

    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            if ((e.Row.RowState & DataControlRowState.Edit) > 0)
            {
                DropDownList ddList = (DropDownList)e.Row.FindControl("ddlartname");
                //bind dropdown-list
                //        DropDownList ddlrole1 = (DropDownList)e.Row.FindControl("ddlrole1");


                Label dr = (Label)e.Row.FindControl("lblaid12");
                if (dr.Text.ToString().Equals(""))
                {
                    String sCommand1_subbu1 = "select  'Meeting/KT/Other' as ArtifactName ,'-1' as aid";
                    using (SqlDataAdapter adapterSubbu1 = new SqlDataAdapter(sCommand1_subbu1, connString1))
                    {

                        DataSet ds = new DataSet();
                        adapterSubbu1.Fill(ds);

                        ddList.DataSource = ds;
                        ddList.DataTextField = "ArtifactName";
                        ddList.DataValueField = "AID";
                        ddList.DataBind();
                        conn.Close();


                        ddList.SelectedValue = dr.Text.ToString();
                    }
                }
                else
                {
                    User user2 = DstumDAL.getUserdetails(Context.User.Identity.Name);
                    String sCommand1_subbu1 = "select * from artifact where createdfor=@userid";
                    //  DataTable dataSubbu1 = new DataTable();
                    using (SqlDataAdapter adapterSubbu1 = new SqlDataAdapter(sCommand1_subbu1, connString1))
                    {
                        adapterSubbu1.SelectCommand.Parameters.AddWithValue("@userid", user2.userid);

                        DataSet ds = new DataSet();
                        adapterSubbu1.Fill(ds);

                        ddList.DataSource = ds;
                        ddList.DataTextField = "ArtifactName";
                        ddList.DataValueField = "AID";
                        ddList.DataBind();
                        conn.Close();


                        ddList.SelectedValue = dr.Text.ToString();
                    }
                }
            }
        }
    }



    private static void DrawLine(PdfWriter writer, float x1, float y1, float x2, float y2, Color color)
    {
        PdfContentByte contentByte = writer.DirectContent;
        contentByte.SetColorStroke(color);
        contentByte.MoveTo(x1, y1);
        contentByte.LineTo(x2, y2);
        contentByte.Stroke();
    }
    private static PdfPCell PhraseCell(Phrase phrase, int align)
    {
        PdfPCell cell = new PdfPCell(phrase);
        cell.BorderColor = Color.WHITE;
        cell.VerticalAlignment = PdfCell.ALIGN_TOP;
        cell.HorizontalAlignment = align;
        cell.PaddingBottom = 2f;
        cell.PaddingTop = 0f;
        return cell;
    }
    private static PdfPCell PhraseCell1(Phrase phrase, int align)
    {
        PdfPCell cell = new PdfPCell(phrase);
        cell.BorderColor = new Color(0, 70, 139);
        cell.VerticalAlignment = PdfCell.ALIGN_TOP;
        cell.HorizontalAlignment = align;
        cell.PaddingBottom = 5f;
        cell.PaddingTop = 5f;
        return cell;
    }
    private static PdfPCell ImageCell(string path, float scale, int align)
    {
        iTextSharp.text.Image image = iTextSharp.text.Image.GetInstance(path);
        image.ScalePercent(scale);
        PdfPCell cell = new PdfPCell(image);
        cell.BorderColor = new Color(0, 70, 139);
        cell.VerticalAlignment = PdfCell.ALIGN_TOP;
        cell.HorizontalAlignment = align;
        cell.PaddingBottom = 5f;
        cell.PaddingTop = 5f;
        return cell;
    }

    protected void btntodayreport_Click(object sender, EventArgs e)
    {
        Document document = new Document(PageSize.A4, 88f, 88f, 10f, 10f);
        List<Report> listReport = DstumDAL.getReportDetails();
        using (System.IO.MemoryStream memoryStream = new System.IO.MemoryStream())
        {
            PdfWriter writer = PdfWriter.GetInstance(document, memoryStream);
            Phrase phrase = null;
            PdfPCell cell = null;
            PdfPTable table = null;
            Color color = null;

            document.Open();

            //Header Table
            table = new PdfPTable(2);
            table.TotalWidth = 500f;
            table.LockedWidth = true;
            table.SetWidths(new float[] { 0.3f, 0.7f });

            //Company Logo

            color = new Color(System.Drawing.ColorTranslator.FromHtml("#00468b"));
            DrawLine(writer, 25f, document.Top - 29f, document.PageSize.Width - 25f, document.Top - 29f, color);
            //  DrawLine(writer, 25f, document.Top - 30f, document.PageSize.Width - 25f, document.Top - 30f, color);
            document.Add(table);

            //Company Name and Address


            //Separater Line


            table = new PdfPTable(2);
            table.HorizontalAlignment = Element.ALIGN_LEFT;
            table.SetWidths(new float[] { 0.3f, 1f });
            table.SpacingBefore = 50f;

            //Employee Details
            cell = PhraseCell(new Phrase("DSTUM Report for " + DateTime.Now.ToString("dd/MM/yyyy"), FontFactory.GetFont("Arial", 18, Font.BOLD, new Color(0, 70, 139))), PdfPCell.ALIGN_CENTER);
            cell.Colspan = 3;
            table.AddCell(cell);
            cell = PhraseCell(new Phrase(), PdfPCell.ALIGN_CENTER);
            cell.Colspan = 3;
            cell.PaddingBottom = 30f;
            table.AddCell(cell);

            document.Add(table);

            // data table


            table = new PdfPTable(5);
            table.AddCell(PhraseCell1(new Phrase(" Name ", FontFactory.GetFont("Arial", 10, Font.BOLD, new Color(0, 70, 139))), PdfPCell.ALIGN_CENTER));
            table.AddCell(PhraseCell1(new Phrase(" Tools ", FontFactory.GetFont("Arial", 10, Font.BOLD, new Color(0, 70, 139))), PdfPCell.ALIGN_CENTER));
            table.AddCell(PhraseCell1(new Phrase(" Attendance ", FontFactory.GetFont("Arial", 10, Font.BOLD, new Color(0, 70, 139))), PdfPCell.ALIGN_CENTER));
            table.AddCell(PhraseCell1(new Phrase(" Availibility ", FontFactory.GetFont("Arial", 10, Font.BOLD, new Color(0, 70, 139))), PdfPCell.ALIGN_CENTER));
            table.AddCell(PhraseCell1(new Phrase(" mood ", FontFactory.GetFont("Arial", 10, Font.BOLD, new Color(0, 70, 139))), PdfPCell.ALIGN_CENTER));

            for (int i = 0; i < listReport.Count; i++)
            {
                table.AddCell(PhraseCell1(new Phrase(" " + listReport[i].name, FontFactory.GetFont("Arial", 10, Font.BOLD, Color.BLACK)), PdfPCell.ALIGN_CENTER));
                table.AddCell(PhraseCell1(new Phrase(" " + listReport[i].tools, FontFactory.GetFont("Arial", 10, Font.BOLD, Color.BLACK)), PdfPCell.ALIGN_CENTER));
                table.AddCell(PhraseCell1(new Phrase(" " + listReport[i].attendance, FontFactory.GetFont("Arial", 10, Font.BOLD, Color.BLACK)), PdfPCell.ALIGN_CENTER));
                if (listReport[i].availability.ToString().Equals("Leave"))
                {
                    table.AddCell(PhraseCell1(new Phrase(" " + listReport[i].availability, FontFactory.GetFont("Arial", 10, Font.BOLD, Color.BLACK)), PdfPCell.ALIGN_CENTER));
                }
                else if (Convert.ToInt32(listReport[i].availability) < 100)
                {
                    table.AddCell(PhraseCell1(new Phrase(" " + (100 - Convert.ToInt32(listReport[i].availability)) + "%", FontFactory.GetFont("Arial", 10, Font.BOLD, Color.BLACK)), PdfPCell.ALIGN_CENTER));
                }
                else
                {
                    table.AddCell(PhraseCell1(new Phrase("0%", FontFactory.GetFont("Arial", 10, Font.BOLD, Color.BLACK)), PdfPCell.ALIGN_CENTER));

                }
                if (listReport[i].mood.ToString().Equals("green"))
                {
                    cell = ImageCell(System.Web.HttpContext.Current.Server.MapPath("~/img/green.jpg"), 30f, PdfPCell.ALIGN_CENTER);
                    table.AddCell(cell);
                }
                else if (listReport[i].mood.ToString().Equals("red"))
                {
                    cell = ImageCell(System.Web.HttpContext.Current.Server.MapPath("~/img/red.jpg"), 30f, PdfPCell.ALIGN_CENTER);
                    table.AddCell(cell);
                }
                else if (listReport[i].mood.ToString().Equals("orange"))
                {
                    cell = ImageCell(System.Web.HttpContext.Current.Server.MapPath("~/img/orange.jpg"), 30f, PdfPCell.ALIGN_CENTER);
                    table.AddCell(cell);
                }
                else
                {
                    cell = ImageCell(System.Web.HttpContext.Current.Server.MapPath("~/img/red.jpg"), 30f, PdfPCell.ALIGN_CENTER);
                    table.AddCell(cell);
                }


                //  table.AddCell(PhraseCell1(new Phrase(" " + listReport[i].mood, FontFactory.GetFont("Arial", 10, Font.BOLD, Color.BLACK)), PdfPCell.ALIGN_CENTER));

            }

            document.Add(table);

            document.Close();
            byte[] bytes = memoryStream.ToArray();
            memoryStream.Close();
            Response.Clear();
            Response.ContentType = "application/pdf";
            Response.AddHeader("Content-Disposition", "attachment; filename=DSTUM " + DateTime.Now.ToString("dd/MM/yyyy") + ".pdf");
            Response.ContentType = "application/pdf";
            Response.Buffer = true;
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.BinaryWrite(bytes);
            Response.End();
            Response.Close();
        }
    }



    protected void btnexcelsheet_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable dt = DstumDAL.getExcelSheetData();
            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt, "DSTUM");
                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;filename=DSTUM Service Management.xlsx");
                using (MemoryStream MyMemoryStream = new MemoryStream())
                {
                    wb.SaveAs(MyMemoryStream);
                    MyMemoryStream.WriteTo(Response.OutputStream);
                    Response.Flush();
                    Response.End();
                }
            }
        }
        catch
        {
        }
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
                if (isArtifactAdded == true)
                {
                    Response.Write(@"<script language='javascript'>window.alert('Excel file upload and Data added sucessfully \n ');</script>");

                    txtdesc.Text = "";
                    txttodayhour.Text = "";
                    txtYesterdayhour.Text = "";
                    DataBindGrid();
                }
                else
                {
                    Response.Write(@"<script language='javascript'>alert('some error is occurs \n ');</script>");

                }
                //if (isArtifactAdded)
                //{
                //    Response.Redirect("~/Default.aspx");
                //}
            }







            //Bind the datatable to the Grid  
            File.Delete(fileLocation);
        }
        catch (Exception ex)
        {
        }
        //Delete the excel file from the server  

    }
}
