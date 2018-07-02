using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
public partial class Default2 : System.Web.UI.Page
{

    User user = new User();
    static string connString1 = ConfigurationManager.ConnectionStrings["DSTUMConnectionString"].ConnectionString;


    public static SqlConnection conn =
        new SqlConnection(ConfigurationManager.ConnectionStrings["DSTUMConnectionString"].ConnectionString);
    public static SqlCommand cmd;
    public static SqlDataReader dr;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

           
            user = DstumDAL.getUserdetails(Context.User.Identity.Name);


            Tools tools = DstumDAL.getToolDetails(user.toolid);
            lblusername.Text = user.username;
            lbluserid.Text = user.userid.ToString();
            lbltoolid.Text = user.toolid.ToString();
            lblToolName.Text = tools.toolname;
            DataBindGrid();
        }
       
    }

    private void DataBindGrid()
    {
        user = DstumDAL.getUserdetails(Context.User.Identity.Name);

        String sCommand1_subbu1 = " select ar.*,est_hour-lock_hour as remhour,tr.toolname from artifact ar inner join tools tr  on ar.toolid=tr.toolID where createdfor= @userid and status='In Progress'  or status= 'pending'";
        //  DataTable dataSubbu1 = new DataTable();
        SqlDataAdapter adapterSubbu1 = new SqlDataAdapter(sCommand1_subbu1, connString1);
        adapterSubbu1.SelectCommand.Parameters.AddWithValue("@userid", user.userid);

        DataSet ds = new DataSet();
        adapterSubbu1.Fill(ds);

        GridView1.DataSource = ds;


        GridView1.DataBind();
    }




 

    


    protected void Addartifact_Click(object sender, EventArgs e)
    {
        bool inset = false;
        Addartifact addArtifact = new Addartifact();
        addArtifact.artNum = txtArtNum.Text.ToString();
        addArtifact.artName = txtArtName.Text.ToString();
        addArtifact.toolid = Convert.ToInt32(lbltoolid.Text);
        addArtifact.createdby = Convert.ToInt32(lbluserid.Text);
        addArtifact.desc = txtArtdesc.Text.ToString();
        addArtifact.esthour = Convert.ToInt32(txtEsthour.Text);
        user = DstumDAL.getUserdetails(Context.User.Identity.Name);

        try
        {
            inset = DstumDAL.insertArtifact(addArtifact, user.userid);
        }
        catch (Exception ex)
        {

        }
        if (inset == true)
        {
            Response.Write(@"<script language='javascript'>window.alert('Artifact Added successfully \n .');window.location='Default.aspx';</script>");
            //     Response.Write(@"<script language='javascript'>alert('Artifact Added successfully \n .');</script>");
            //    Response.Redirect("~/Default.aspx");
            //    UpdatePanel1.Update();
        }
        else
        {
            Response.Write(@"<script language='javascript'>alert('some error is occurs \n .');</script>");

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

            string artname = ((TextBox)GridView1.Rows[RowIndex].FindControl("txtArtName")).Text;
            string desc = ((TextBox)GridView1.Rows[RowIndex].FindControl("txtdesc")).Text;
            string esthour = ((TextBox)GridView1.Rows[RowIndex].FindControl("txtesthour")).Text;
            string lockhour = ((TextBox)GridView1.Rows[RowIndex].FindControl("txtlockhour")).Text;
            string txtcomments = ((TextBox)GridView1.Rows[RowIndex].FindControl("txtcomments")).Text;
            var ststus = ((DropDownList)GridView1.Rows[RowIndex].FindControl("status")).SelectedValue.ToString();
            if (ststus.Equals("In Progress"))

            {

                String query = "update artifact set ArtifactName=@artname ,[desc]=@desc,est_hour=@esthout,lock_hour=@lockhour,comments=@comments,status=@status where AID=@aid";
                cmd = new SqlCommand(query, conn);


                cmd.Parameters.AddWithValue("@artname", artname);
                cmd.Parameters.AddWithValue("@desc", desc);
                cmd.Parameters.AddWithValue("@esthout", esthour);
                cmd.Parameters.AddWithValue("@lockhour", lockhour);
                cmd.Parameters.AddWithValue("@comments", txtcomments);
                cmd.Parameters.AddWithValue("@status", ststus);
                cmd.Parameters.AddWithValue("@aid", id);

            }
            else
            {
                String query = "update artifact set ArtifactName=@artname ,[desc]=@desc,est_hour=@esthout,lock_hour=@lockhour,comments=@comments,status=@status,enddate=@date where AID=@aid";
                cmd = new SqlCommand(query, conn);
                var date = DateTime.Now.ToString("yyyy/MM/dd");

                cmd.Parameters.AddWithValue("@artname", artname);
                cmd.Parameters.AddWithValue("@desc", desc);
                cmd.Parameters.AddWithValue("@esthout", esthour);
                cmd.Parameters.AddWithValue("@lockhour", lockhour);
                cmd.Parameters.AddWithValue("@comments", txtcomments);
                cmd.Parameters.AddWithValue("@status", ststus);
                cmd.Parameters.AddWithValue("@aid", id);
                cmd.Parameters.AddWithValue("@date", date);
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
}