using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

public partial class ADDTools : System.Web.UI.Page
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
            DataSet ds = DstumDAL.toolList();
            GridView1.DataSource = ds;


            GridView1.DataBind();
        }

    }

    protected void Addtools_Click(object sender, EventArgs e)
    {
        bool inset = false;
        Tools tools = new Tools();
        tools.toolname = txtToolname.Text.ToString();
        tools.desc = txtdesc.Text.ToString();
     


        try
        {
            inset = DstumDAL.insertTools(tools);
        }
       catch (Exception ex)
        {

        }
        if (inset == true)
        {
            Response.Write(@"<script language='javascript'>window.alert('Tools Added successfully \n .');</script>");
            //     Response.Write(@"<script language='javascript'>alert('Artifact Added successfully \n .');</script>");
            //    Response.Redirect("~/Default.aspx");
            DataBindGrid();
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

            string toolname = ((TextBox)GridView1.Rows[RowIndex].FindControl("txtToolName")).Text;
            string desc = ((TextBox)GridView1.Rows[RowIndex].FindControl("txtdesc")).Text;
        
                String query = "update tools set toolname=@toolname ,[desc]=@desc where ToolID=@toolid";
                cmd = new SqlCommand(query, conn);
                var date = DateTime.Now.ToString("yyyy/MM/dd");

            cmd.Parameters.AddWithValue("@toolname", toolname);
                cmd.Parameters.AddWithValue("@desc", desc);
            cmd.Parameters.AddWithValue("@toolid", id);

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

     public void DataBindGrid()
    {
        DataSet ds = DstumDAL.toolList();
        GridView1.DataSource = ds;


        GridView1.DataBind();
    }
}