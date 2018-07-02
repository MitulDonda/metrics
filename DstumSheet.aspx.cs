using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.Services;


public partial class DstumSheet : System.Web.UI.Page
{

            User user = new User();
    static string connString1 = ConfigurationManager.ConnectionStrings["DSTUMConnectionString"].ConnectionString;
    
   

    public static SqlConnection conn =
        new SqlConnection(ConfigurationManager.ConnectionStrings["DSTUMConnectionString"].ConnectionString);
    public static SqlCommand cmd;
    public static SqlDataReader dr;
    protected void Page_Load(object sender, EventArgs e)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append(" if (DATEPART(d, GETDATE()) < 5) ");
        sb.Append(" begin ");
        sb.Append(" select ds.DID,ds.[desc],replace(convert(NVARCHAR, ds.todaydate, 106), ' ', '/') todaydate ,ds.Todayhour,ds.yesterdayhour,ur.Name,tl.toolname,ar.ArtifactID,ar.[status],replace(convert(NVARCHAR, ar.enddate, 106), ' ', '/') enddate,ar.enddate, CASE WHEN Cast(ar.[ArtifactName] AS varchar) is null THEN 'Meeting/KT/Other'     ELSE Cast(ar.[ArtifactName] AS varchar)  END AS ArtifactName,  CASE WHEN Cast(ar.est_hour AS varchar) is null THEN 0      ELSE Cast(ar.est_hour AS varchar)      END AS est_hour,CASE WHEN Cast(ar.est_hour AS varchar) is null THEN 0     ELSE Cast(ar.est_hour-ar.lock_hour AS varchar)      END AS remhour,ar.comments from dstum ds left outer join[user] ur on ur.userID = ds.userid left outer join artifact ar on ar.AID = ds.AID left outer join tools tl on tl.toolID = ur.toolid   where DATEPART(m, GETDATE()) = DATEPART(m, ds.todaydate)");
        sb.Append(" union");
        sb.Append(" select ds.DID,ds.[desc],replace(convert(NVARCHAR, ds.todaydate, 106), ' ', '/') todaydate,ds.Todayhour,ds.yesterdayhour,ur.Name,tl.toolname,ar.ArtifactID, ar.[status],replace(convert(NVARCHAR, ar.enddate, 106), ' ', '/') enddate,ar.enddate,CASE WHEN Cast(ar.[ArtifactName] AS varchar) is null THEN 'Meeting/KT/Other'     ELSE Cast(ar.[ArtifactName] AS varchar)  END AS ArtifactName,  CASE WHEN Cast(ar.est_hour AS varchar) is null THEN 0      ELSE Cast(ar.est_hour AS varchar)      END AS est_hour,CASE WHEN Cast(ar.est_hour AS varchar) is null THEN 0     ELSE Cast(ar.est_hour-ar.lock_hour AS varchar)      END AS remhour,ar.comments from dstum ds left outer join[user] ur on ur.userID = ds.userid left outer join artifact ar on ar.AID = ds.AID left outer join tools tl on tl.toolID = ur.toolid   where DATEPART(m, ds.todaydate) = DATEPART(m, GETDATE()) - 1");
        sb.Append(" end");
        sb.Append(" else");
        sb.Append(" begin ");
        sb.Append(" select ds.DID,ds.[desc],replace(convert(NVARCHAR, ds.todaydate, 106), ' ', '/') todaydate,ds.Todayhour,ds.yesterdayhour,ur.Name,tl.toolname,ar.ArtifactID,ar.[status],replace(convert(NVARCHAR, ar.enddate, 106), ' ', '/') enddate, CASE WHEN Cast(ar.[ArtifactName] AS varchar) is null THEN 'Meeting/KT/Other'     ELSE Cast(ar.[ArtifactName] AS varchar)  END AS ArtifactName,  CASE WHEN Cast(ar.est_hour AS varchar) is null THEN 0      ELSE Cast(ar.est_hour AS varchar)      END AS est_hour,CASE WHEN Cast(ar.est_hour AS varchar) is null THEN 0     ELSE Cast(ar.est_hour-ar.lock_hour AS varchar)      END AS remhour,ar.comments from dstum ds left outer join[user] ur on ur.userID = ds.userid left outer join artifact ar on ar.AID = ds.AID left outer join tools tl on tl.toolID = ur.toolid   where DATEPART(m, GETDATE()) = DATEPART(m, ds.todaydate) ");
        sb.Append(" end");
        if (!IsPostBack)
        {
            this.SearchMembers();


        }
    }
    ///public override void VerifyRenderingInServerForm(Control control)
    //{
        /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
           server control at run time. */
    //}
    private void DataBindGrid()
    {


        StringBuilder sb = new StringBuilder();
        sb.Append(" if (DATEPART(d, GETDATE()) < 5) ");
        sb.Append(" begin ");
        sb.Append(" select ds.DID,ds.[desc],replace(convert(NVARCHAR, ds.todaydate, 106), ' ', '/') todaydate ,ds.Todayhour,ds.yesterdayhour,ur.Name,tl.toolname,ar.ArtifactID,ar.[status], CASE WHEN Cast(ar.[ArtifactName] AS varchar) is null THEN 'Meeting/KT/Other'     ELSE Cast(ar.[ArtifactName] AS varchar)  END AS ArtifactName,ar.enddate,   CASE WHEN Cast(ar.est_hour AS varchar) is null THEN 0      ELSE Cast(ar.est_hour AS varchar)      END AS est_hour,CASE WHEN Cast(ar.est_hour AS varchar) is null THEN 0     ELSE Cast(ar.est_hour-ar.lock_hour AS varchar)      END AS remhour,ar.comments,replace(convert(NVARCHAR, ar.enddate, 106), ' ', '/') enddate from dstum ds left outer join[user] ur on ur.userID = ds.userid left outer join artifact ar on ar.AID = ds.AID left outer join tools tl on tl.toolID = ur.toolid   where DATEPART(m, GETDATE()) = DATEPART(m, ds.todaydate)");
        sb.Append(" union");
        sb.Append(" select ds.DID,ds.[desc],replace(convert(NVARCHAR, ds.todaydate, 106), ' ', '/') todaydate,ds.Todayhour,ds.yesterdayhour,ur.Name,tl.toolname,ar.ArtifactID, ar.[status], CASE WHEN Cast(ar.[ArtifactName] AS varchar) is null THEN 'Meeting/KT/Other'     ELSE Cast(ar.[ArtifactName] AS varchar)  END AS ArtifactName,ar.enddate,  CASE WHEN Cast(ar.est_hour AS varchar) is null THEN 0      ELSE Cast(ar.est_hour AS varchar)      END AS est_hour,CASE WHEN Cast(ar.est_hour AS varchar) is null THEN 0     ELSE Cast(ar.est_hour-ar.lock_hour AS varchar)      END AS remhour,ar.comments,replace(convert(NVARCHAR, ar.enddate, 106), ' ', '/') enddate from dstum ds left outer join[user] ur on ur.userID = ds.userid left outer join artifact ar on ar.AID = ds.AID left outer join tools tl on tl.toolID = ur.toolid   where DATEPART(m, ds.todaydate) = DATEPART(m, GETDATE()) - 1");
        sb.Append(" end");
        sb.Append(" else");
        sb.Append(" begin ");
        sb.Append(" select ds.DID,ds.[desc],replace(convert(NVARCHAR, ds.todaydate, 106), ' ', '/') todaydate,ds.Todayhour,ds.yesterdayhour,ur.Name,tl.toolname,ar.ArtifactID,ar.[status], CASE WHEN Cast(ar.[ArtifactName] AS varchar) is null THEN 'Meeting/KT/Other'     ELSE Cast(ar.[ArtifactName] AS varchar)  END AS ArtifactName,  CASE WHEN Cast(ar.est_hour AS varchar) is null THEN 0      ELSE Cast(ar.est_hour AS varchar)      END AS est_hour,CASE WHEN Cast(ar.est_hour AS varchar) is null THEN 0     ELSE Cast(ar.est_hour-ar.lock_hour AS varchar)      END AS remhour,ar.comments,replace(convert(NVARCHAR, ar.enddate, 106), ' ', '/') enddate from dstum ds left outer join[user] ur on ur.userID = ds.userid left outer join artifact ar on ar.AID = ds.AID left outer join tools tl on tl.toolID = ur.toolid   where DATEPART(m, GETDATE()) = DATEPART(m, ds.todaydate) ");
        sb.Append(" end");
        //  DataTable dataSubbu1 = new DataTable();
        using (SqlDataAdapter adapterSubbu1 = new SqlDataAdapter(sb.ToString(), connString1))
        {


            DataSet ds = new DataSet();
            adapterSubbu1.Fill(ds);

            GridView1.DataSource = ds;


            GridView1.DataBind();
            conn.Close();
        }
    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {

    }

    protected void GridView1_RowCreated(object sender, GridViewRowEventArgs e)
    {

    }

    protected void btnexptoexcel_Click(object sender, EventArgs e)
    {
        Response.Clear();
        Response.Buffer = true;
        Response.ClearContent();
        Response.ClearHeaders();
        Response.Charset = "";
        string FileName = "DSTUM_" + DateTime.Now.Date.ToString("MM/dd/yyyy") + ".xls";
        StringWriter strwritter = new StringWriter();
        HtmlTextWriter htmltextwrtter = new HtmlTextWriter(strwritter);
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.ContentType = "application/vnd.ms-excel";
        Response.AddHeader("Content-Disposition", "attachment;filename=" + FileName);
        GridView1.GridLines = GridLines.Both;
        GridView1.HeaderStyle.Font.Bold = true;
        GridView1.RenderControl(htmltextwrtter);
        Response.Write(strwritter.ToString());
        Response.End();
    }

    private void SearchMembers()
    {
        string constr = ConfigurationManager.ConnectionStrings["DSTUMConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(" if (DATEPART(d, GETDATE()) < 5) ");
                sb.Append(" begin ");
                sb.Append(" select ds.DID,ds.[desc],replace(convert(NVARCHAR, ds.todaydate, 106), ' ', '/') todaydate ,ds.Todayhour,ds.yesterdayhour,ur.Name,tl.toolname,ar.ArtifactID,ar.[status],replace(convert(NVARCHAR, ar.enddate, 106), ' ', '/') enddate,ar.enddate, CASE WHEN Cast(ar.[ArtifactName] AS varchar) is null THEN 'Meeting/KT/Other'     ELSE Cast(ar.[ArtifactName] AS varchar)  END AS ArtifactName,  CASE WHEN Cast(ar.est_hour AS varchar) is null THEN 0      ELSE Cast(ar.est_hour AS varchar)      END AS est_hour,CASE WHEN Cast(ar.est_hour AS varchar) is null THEN 0     ELSE Cast(ar.est_hour-ar.lock_hour AS varchar)      END AS remhour,ar.comments from dstum ds left outer join[user] ur on ur.userID = ds.userid left outer join artifact ar on ar.AID = ds.AID left outer join tools tl on tl.toolID = ur.toolid   where DATEPART(m, GETDATE()) = DATEPART(m, ds.todaydate)");
                sb.Append(" union");
                sb.Append(" select ds.DID,ds.[desc],replace(convert(NVARCHAR, ds.todaydate, 106), ' ', '/') todaydate,ds.Todayhour,ds.yesterdayhour,ur.Name,tl.toolname,ar.ArtifactID, ar.[status],replace(convert(NVARCHAR, ar.enddate, 106), ' ', '/') enddate,ar.enddate,CASE WHEN Cast(ar.[ArtifactName] AS varchar) is null THEN 'Meeting/KT/Other'     ELSE Cast(ar.[ArtifactName] AS varchar)  END AS ArtifactName,  CASE WHEN Cast(ar.est_hour AS varchar) is null THEN 0      ELSE Cast(ar.est_hour AS varchar)      END AS est_hour,CASE WHEN Cast(ar.est_hour AS varchar) is null THEN 0     ELSE Cast(ar.est_hour-ar.lock_hour AS varchar)      END AS remhour,ar.comments from dstum ds left outer join[user] ur on ur.userID = ds.userid left outer join artifact ar on ar.AID = ds.AID left outer join tools tl on tl.toolID = ur.toolid   where DATEPART(m, ds.todaydate) = DATEPART(m, GETDATE()) - 1");
                sb.Append(" end");
                sb.Append(" else");
                sb.Append(" begin ");
                sb.Append(" select ds.DID,ds.[desc],replace(convert(NVARCHAR, ds.todaydate, 106), ' ', '/') todaydate,ds.Todayhour,ds.yesterdayhour,ur.Name,tl.toolname,ar.ArtifactID,ar.[status],replace(convert(NVARCHAR, ar.enddate, 106), ' ', '/') enddate, CASE WHEN Cast(ar.[ArtifactName] AS varchar) is null THEN 'Meeting/KT/Other'     ELSE Cast(ar.[ArtifactName] AS varchar)  END AS ArtifactName,  CASE WHEN Cast(ar.est_hour AS varchar) is null THEN 0      ELSE Cast(ar.est_hour AS varchar)      END AS est_hour,CASE WHEN Cast(ar.est_hour AS varchar) is null THEN 0     ELSE Cast(ar.est_hour-ar.lock_hour AS varchar)      END AS remhour,ar.comments from dstum ds left outer join[user] ur on ur.userID = ds.userid left outer join artifact ar on ar.AID = ds.AID left outer join tools tl on tl.toolID = ur.toolid   where DATEPART(m, GETDATE()) = DATEPART(m, ds.todaydate) ");
                sb.Append(" end");
                string sql = sb.ToString();
                StringBuilder sb1 = new StringBuilder();

                if (!string.IsNullOrEmpty(txtusename.Text.Trim()) && string.IsNullOrEmpty(dtp_input211.Text.Trim()))
                {


                    sb1.Append(" if (DATEPART(d, GETDATE()) < 5) ");
                    sb1.Append(" begin ");
                    sb1.Append(" select ds.DID,ds.[desc],replace(convert(NVARCHAR, ds.todaydate, 106), ' ', '/') todaydate ,ds.Todayhour,ds.yesterdayhour,ur.Name,tl.toolname,ar.ArtifactID,ar.[status],replace(convert(NVARCHAR, ar.enddate, 106), ' ', '/') enddate,ar.enddate, CASE WHEN Cast(ar.[ArtifactName] AS varchar) is null THEN 'Meeting/KT/Other'     ELSE Cast(ar.[ArtifactName] AS varchar)  END AS ArtifactName,  CASE WHEN Cast(ar.est_hour AS varchar) is null THEN 0      ELSE Cast(ar.est_hour AS varchar)      END AS est_hour,CASE WHEN Cast(ar.est_hour AS varchar) is null THEN 0     ELSE Cast(ar.est_hour-ar.lock_hour AS varchar)      END AS remhour,ar.comments from dstum ds left outer join[user] ur on ur.userID = ds.userid left outer join artifact ar on ar.AID = ds.AID left outer join tools tl on tl.toolID = ur.toolid   where DATEPART(m, GETDATE()) = DATEPART(m, ds.todaydate) and ur.Name like @Name + '%'");
                    sb1.Append(" union");
                    sb1.Append(" select ds.DID,ds.[desc],replace(convert(NVARCHAR, ds.todaydate, 106), ' ', '/') todaydate,ds.Todayhour,ds.yesterdayhour,ur.Name,tl.toolname,ar.ArtifactID, ar.[status],replace(convert(NVARCHAR, ar.enddate, 106), ' ', '/') enddate,ar.enddate,CASE WHEN Cast(ar.[ArtifactName] AS varchar) is null THEN 'Meeting/KT/Other'     ELSE Cast(ar.[ArtifactName] AS varchar)  END AS ArtifactName,  CASE WHEN Cast(ar.est_hour AS varchar) is null THEN 0      ELSE Cast(ar.est_hour AS varchar)      END AS est_hour,CASE WHEN Cast(ar.est_hour AS varchar) is null THEN 0     ELSE Cast(ar.est_hour-ar.lock_hour AS varchar)      END AS remhour,ar.comments from dstum ds left outer join[user] ur on ur.userID = ds.userid left outer join artifact ar on ar.AID = ds.AID left outer join tools tl on tl.toolID = ur.toolid   where DATEPART(m, ds.todaydate) = DATEPART(m, GETDATE()) - 1  and ur.Name like @Name + '%'");
                    sb1.Append(" end");
                    sb1.Append(" else");
                    sb1.Append(" begin ");
                    sb1.Append(" select ds.DID,ds.[desc],replace(convert(NVARCHAR, ds.todaydate, 106), ' ', '/') todaydate,ds.Todayhour,ds.yesterdayhour,ur.Name,tl.toolname,ar.ArtifactID,ar.[status],replace(convert(NVARCHAR, ar.enddate, 106), ' ', '/') enddate, CASE WHEN Cast(ar.[ArtifactName] AS varchar) is null THEN 'Meeting/KT/Other'     ELSE Cast(ar.[ArtifactName] AS varchar)  END AS ArtifactName,  CASE WHEN Cast(ar.est_hour AS varchar) is null THEN 0      ELSE Cast(ar.est_hour AS varchar)      END AS est_hour,CASE WHEN Cast(ar.est_hour AS varchar) is null THEN 0     ELSE Cast(ar.est_hour-ar.lock_hour AS varchar)      END AS remhour,ar.comments from dstum ds left outer join[user] ur on ur.userID = ds.userid left outer join artifact ar on ar.AID = ds.AID left outer join tools tl on tl.toolID = ur.toolid   where DATEPART(m, GETDATE()) = DATEPART(m, ds.todaydate) and ur.Name like @Name + '%'");
                    sb1.Append(" end");
                    sql = sb1.ToString();
                    cmd.Parameters.AddWithValue("@Name", txtusename.Text.Trim());
                }
                else if (string.IsNullOrEmpty(txtusename.Text.Trim()) && !string.IsNullOrEmpty(dtp_input211.Text.Trim()))
                {

                    sb1.Append(" if (DATEPART(d, GETDATE()) < 5) ");
                    sb1.Append(" begin ");
                    sb1.Append(" select ds.DID,ds.[desc],replace(convert(NVARCHAR, ds.todaydate, 106), ' ', '/') todaydate ,ds.Todayhour,ds.yesterdayhour,ur.Name,tl.toolname,ar.ArtifactID,ar.[status],replace(convert(NVARCHAR, ar.enddate, 106), ' ', '/') enddate,ar.enddate, CASE WHEN Cast(ar.[ArtifactName] AS varchar) is null THEN 'Meeting/KT/Other'     ELSE Cast(ar.[ArtifactName] AS varchar)  END AS ArtifactName,  CASE WHEN Cast(ar.est_hour AS varchar) is null THEN 0      ELSE Cast(ar.est_hour AS varchar)      END AS est_hour,CASE WHEN Cast(ar.est_hour AS varchar) is null THEN 0     ELSE Cast(ar.est_hour-ar.lock_hour AS varchar)      END AS remhour,ar.comments from dstum ds left outer join[user] ur on ur.userID = ds.userid left outer join artifact ar on ar.AID = ds.AID left outer join tools tl on tl.toolID = ur.toolid   where DATEPART(m, GETDATE()) = DATEPART(m, ds.todaydate) and ds.todaydate like @todaydate + '%'");
                    sb1.Append(" union");
                    sb1.Append(" select ds.DID,ds.[desc],replace(convert(NVARCHAR, ds.todaydate, 106), ' ', '/') todaydate,ds.Todayhour,ds.yesterdayhour,ur.Name,tl.toolname,ar.ArtifactID, ar.[status],replace(convert(NVARCHAR, ar.enddate, 106), ' ', '/') enddate,ar.enddate,CASE WHEN Cast(ar.[ArtifactName] AS varchar) is null THEN 'Meeting/KT/Other'     ELSE Cast(ar.[ArtifactName] AS varchar)  END AS ArtifactName,  CASE WHEN Cast(ar.est_hour AS varchar) is null THEN 0      ELSE Cast(ar.est_hour AS varchar)      END AS est_hour,CASE WHEN Cast(ar.est_hour AS varchar) is null THEN 0     ELSE Cast(ar.est_hour-ar.lock_hour AS varchar)      END AS remhour,ar.comments from dstum ds left outer join[user] ur on ur.userID = ds.userid left outer join artifact ar on ar.AID = ds.AID left outer join tools tl on tl.toolID = ur.toolid   where DATEPART(m, ds.todaydate) = DATEPART(m, GETDATE()) - 1  and ds.todaydate like @todaydate + '%'");
                    sb1.Append(" end");
                    sb1.Append(" else");
                    sb1.Append(" begin ");
                    sb1.Append(" select ds.DID,ds.[desc],replace(convert(NVARCHAR, ds.todaydate, 106), ' ', '/') todaydate,ds.Todayhour,ds.yesterdayhour,ur.Name,tl.toolname,ar.ArtifactID,ar.[status],replace(convert(NVARCHAR, ar.enddate, 106), ' ', '/') enddate, CASE WHEN Cast(ar.[ArtifactName] AS varchar) is null THEN 'Meeting/KT/Other'     ELSE Cast(ar.[ArtifactName] AS varchar)  END AS ArtifactName,  CASE WHEN Cast(ar.est_hour AS varchar) is null THEN 0      ELSE Cast(ar.est_hour AS varchar)      END AS est_hour,CASE WHEN Cast(ar.est_hour AS varchar) is null THEN 0     ELSE Cast(ar.est_hour-ar.lock_hour AS varchar)      END AS remhour,ar.comments from dstum ds left outer join[user] ur on ur.userID = ds.userid left outer join artifact ar on ar.AID = ds.AID left outer join tools tl on tl.toolID = ur.toolid   where DATEPART(m, GETDATE()) = DATEPART(m, ds.todaydate) and ds.todaydate like @todaydate +  '%'");
                    sb1.Append(" end");
                    sql = sb1.ToString();
                    cmd.Parameters.AddWithValue("@todaydate", dtp_input211.Text.Trim());
                }
               else  if (!string.IsNullOrEmpty(txtusename.Text.Trim()) && !string.IsNullOrEmpty(dtp_input211.Text.Trim()))
                {


                    sb1.Append(" if (DATEPART(d, GETDATE()) < 5) ");
                    sb1.Append(" begin ");
                    sb1.Append(" select ds.DID,ds.[desc],replace(convert(NVARCHAR, ds.todaydate, 106), ' ', '/') todaydate ,ds.Todayhour,ds.yesterdayhour,ur.Name,tl.toolname,ar.ArtifactID,ar.[status],replace(convert(NVARCHAR, ar.enddate, 106), ' ', '/') enddate,ar.enddate, CASE WHEN Cast(ar.[ArtifactName] AS varchar) is null THEN 'Meeting/KT/Other'     ELSE Cast(ar.[ArtifactName] AS varchar)  END AS ArtifactName,  CASE WHEN Cast(ar.est_hour AS varchar) is null THEN 0      ELSE Cast(ar.est_hour AS varchar)      END AS est_hour,CASE WHEN Cast(ar.est_hour AS varchar) is null THEN 0     ELSE Cast(ar.est_hour-ar.lock_hour AS varchar)      END AS remhour,ar.comments from dstum ds left outer join[user] ur on ur.userID = ds.userid left outer join artifact ar on ar.AID = ds.AID left outer join tools tl on tl.toolID = ur.toolid   where DATEPART(m, GETDATE()) = DATEPART(m, ds.todaydate) and ur.Name like @Name + '%' and ds.todaydate like @todaydate +  '%'");
                    sb1.Append(" union");
                    sb1.Append(" select ds.DID,ds.[desc],replace(convert(NVARCHAR, ds.todaydate, 106), ' ', '/') todaydate,ds.Todayhour,ds.yesterdayhour,ur.Name,tl.toolname,ar.ArtifactID, ar.[status],replace(convert(NVARCHAR, ar.enddate, 106), ' ', '/') enddate,ar.enddate,CASE WHEN Cast(ar.[ArtifactName] AS varchar) is null THEN 'Meeting/KT/Other'     ELSE Cast(ar.[ArtifactName] AS varchar)  END AS ArtifactName,  CASE WHEN Cast(ar.est_hour AS varchar) is null THEN 0      ELSE Cast(ar.est_hour AS varchar)      END AS est_hour,CASE WHEN Cast(ar.est_hour AS varchar) is null THEN 0     ELSE Cast(ar.est_hour-ar.lock_hour AS varchar)      END AS remhour,ar.comments from dstum ds left outer join[user] ur on ur.userID = ds.userid left outer join artifact ar on ar.AID = ds.AID left outer join tools tl on tl.toolID = ur.toolid   where DATEPART(m, ds.todaydate) = DATEPART(m, GETDATE()) - 1  and ur.Name like @Name + '%' and ds.todaydate like @todaydate +  '%'");
                    sb1.Append(" end");
                    sb1.Append(" else");
                    sb1.Append(" begin ");
                    sb1.Append(" select ds.DID,ds.[desc],replace(convert(NVARCHAR, ds.todaydate, 106), ' ', '/') todaydate,ds.Todayhour,ds.yesterdayhour,ur.Name,tl.toolname,ar.ArtifactID,ar.[status],replace(convert(NVARCHAR, ar.enddate, 106), ' ', '/') enddate, CASE WHEN Cast(ar.[ArtifactName] AS varchar) is null THEN 'Meeting/KT/Other'     ELSE Cast(ar.[ArtifactName] AS varchar)  END AS ArtifactName,  CASE WHEN Cast(ar.est_hour AS varchar) is null THEN 0      ELSE Cast(ar.est_hour AS varchar)      END AS est_hour,CASE WHEN Cast(ar.est_hour AS varchar) is null THEN 0     ELSE Cast(ar.est_hour-ar.lock_hour AS varchar)      END AS remhour,ar.comments from dstum ds left outer join[user] ur on ur.userID = ds.userid left outer join artifact ar on ar.AID = ds.AID left outer join tools tl on tl.toolID = ur.toolid   where DATEPART(m, GETDATE()) = DATEPART(m, ds.todaydate) and ur.Name like @Name + '%' and ds.todaydate like @todaydate +  '%'");
                    sb1.Append(" end");
                    sql = sb1.ToString();
                    cmd.Parameters.AddWithValue("@Name", txtusename.Text.Trim());
                    cmd.Parameters.AddWithValue("@todaydate", dtp_input211.Text.Trim());
                }

                cmd.CommandText = sql;
                cmd.Connection = con;
                using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    GridView1.DataSource = dt;
                    GridView1.DataBind();
                }
            }
        }
    }


    [WebMethod]
    public static string[] GetMembers(string prefix)
    {
        List<string> members = new List<string>();
        using (SqlConnection conn = new SqlConnection())
        {
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["DSTUMConnectionString"].ConnectionString;
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = "select Name,userID  from [user] where Name like @SearchText + '%'";
                cmd.Parameters.AddWithValue("@SearchText", prefix);
                cmd.Connection = conn;
                conn.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        members.Add(string.Format("{0}-{1}", sdr["Name"], sdr["userID"]));
                    }
                }
                conn.Close();
            }
        }
        return members.ToArray();
    }



    protected void search_Click(object sender, EventArgs e)
    {
        SearchMembers();
    }

    protected void txtSearch_TextChanged(object sender, EventArgs e)
    {
        SearchMembers();
    }
}
