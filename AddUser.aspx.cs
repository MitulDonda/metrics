using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;


public partial class AddUser : System.Web.UI.Page
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
            toolDropdownbind();
            GridViewdataBind();
        }
    }

   

    protected void AddUser_Click(object sender, EventArgs e)
    {
        bool inset = false;
        User user = new User();
        user.name = txtUsername.Text.ToString();
        user.username = txtUserid.Text.ToString();
        user.role = ddlrole.SelectedValue.ToString();
        user.toolid = Convert.ToInt32(ddltools.SelectedValue.ToString());
      


        try
         {
            inset = DstumDAL.insertUser(user);
        }
        catch (Exception ex)
        {

        }
        if (inset == true)
        {
          
  
            txtUserid.Text = " ";
            txtUsername.Text = " ";
            GridViewdataBind();
        }
        else
        {
          //Response.Write(@"<script language='javascript'>alert('some error is occurs \n ');</script>");

        }

    }
    public void toolDropdownbind()
    {

        cmd = new SqlCommand("select * from tools", conn);

        cmd.Connection = conn;
        conn.Open();
        DataTable dt = new DataTable();

        dt.Load(cmd.ExecuteReader());
        conn.Close();

        ddltools.DataSource = dt;
        ddltools.DataTextField = "toolname";
        ddltools.DataValueField = "toolid";
        ddltools.DataBind();
      
    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {

        if (e.CommandName == "EditRow")
        {
            int rowIndex = ((GridViewRow)((Button)e.CommandSource).NamingContainer).RowIndex;

            GridView1.EditIndex = rowIndex;
            GridViewdataBind();

        }
        else if (e.CommandName == "CancelRow")
        {
            GridView1.EditIndex = -1;
            GridViewdataBind();
        }
        else if (e.CommandName == "UpdateRow")
        {



            int RowIndex = ((GridViewRow)((Button)e.CommandSource).NamingContainer).RowIndex;

            int userId = Convert.ToInt32(e.CommandArgument);
       

            string id = GridView1.DataKeys[RowIndex].Value.ToString();

            string username = ((TextBox)GridView1.Rows[RowIndex].FindControl("txtUserID")).Text;
            string name = ((TextBox)GridView1.Rows[RowIndex].FindControl("txtUserName")).Text;
            var toolid = ((DropDownList)GridView1.Rows[RowIndex].FindControl("ddltoolname")).SelectedValue.ToString();

            var role = ((DropDownList)GridView1.Rows[RowIndex].FindControl("ddlrole1")).SelectedValue.ToString();
          
                String query = "update [user] set username=@username ,[name]=@name,toolid=@toolid,role=@role where userid=@uid";
                cmd = new SqlCommand(query, conn);


                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@toolid", toolid);
                cmd.Parameters.AddWithValue("@role", role);
              
                cmd.Parameters.AddWithValue("@uid", id);

         
         
            conn.Open();
            int dr = cmd.ExecuteNonQuery();
            if (dr > 0)
            {


                GridView1.EditIndex = -1;
                GridViewdataBind();


                // inserted = true;
            }
            conn.Close();



        }
    }

    protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
    {

    }
    private void GridViewdataBind()
    {
        

        String sCommand1_subbu1 = " select ur.*,tl.toolname  from  [user] ur inner join tools tl on ur.toolID=tl.ToolID";
        //  DataTable dataSubbu1 = new DataTable();
        SqlDataAdapter adapterSubbu1 = new SqlDataAdapter(sCommand1_subbu1, connString1);
      
        DataSet ds = new DataSet();
        adapterSubbu1.Fill(ds);

        GridView1.DataSource = ds;


        GridView1.DataBind();
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            
            if ((e.Row.RowState & DataControlRowState.Edit) > 0)
            {
                DropDownList ddList = (DropDownList)e.Row.FindControl("ddltoolname");
                //bind dropdown-list
                DropDownList ddlrole1 = (DropDownList)e.Row.FindControl("ddlrole1");



                cmd = new SqlCommand("select * from tools", conn);

                cmd.Connection = conn;
                conn.Open();
                DataTable dt = new DataTable();

                dt.Load(cmd.ExecuteReader());
                conn.Close();


                ddList.DataSource = dt;
                ddList.DataTextField = "toolname";
                ddList.DataValueField = "toolid";
                ddList.DataBind();


                Label dr = (Label)e.Row.FindControl("lbltoolname12");
                ddList.SelectedValue = dr.Text.ToString();
                Label dr1 = (Label)e.Row.FindControl("lblrole12");
                ddlrole1.SelectedValue = dr1.Text.ToString();
            }
        }
    }
}