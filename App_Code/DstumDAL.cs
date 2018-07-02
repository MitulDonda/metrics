using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using System.Text;

public class DstumDAL
{
    static string connString1 = ConfigurationManager.ConnectionStrings["DSTUMConnectionString"].ConnectionString;
    public static SqlConnection conn =
          new SqlConnection(ConfigurationManager.ConnectionStrings["DSTUMConnectionString"].ConnectionString);
    public static SqlCommand cmd;
    public static SqlDataReader dr;
    public DstumDAL()
    {
    }




    public static string getToolsData(String username)
    {
        bool validateLogin = false;
        string tools = "";
        try
        {
            cmd = new SqlCommand("select * from tools", conn);

            conn.Open();
            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.HasRows)
            {
                dr.Read();

                tools += dr.GetString(1);

                validateLogin = true;

                conn.Close();
            }
            dr.Close();
            if (validateLogin)
            {
                return tools;
            }
            return tools;



        }

        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            conn.Close();

        }
    }


    public static User getUserdetails(string username)
    {
        User user = new User();

        try
        {
            cmd = new SqlCommand("select * from [user]  where username=@userid", conn);
            cmd.Parameters.AddWithValue("@userid", username);

            conn.Open();
            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.HasRows)
            {
                dr.Read();
                user.userid = (int)dr[0];
                user.username = (string)dr[1];
                user.name = (string)dr[2];
                user.toolid = (int)dr[3];
                user.role = (string)dr[4];
                conn.Close();
            }
            dr.Close();

            return user;



        }

        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            conn.Close();

        }
    }



    public static Tools getToolDetails(int Toolid)
    {
        Tools tools = new Tools();

        try
        {
            cmd = new SqlCommand("select * from tools  where toolid=@toolid", conn);
            cmd.Parameters.AddWithValue("@toolid", Toolid);

            conn.Open();
            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.HasRows)
            {
                dr.Read();
                tools.toolid = (int)dr[0];
                tools.toolname = (string)dr[1];

                conn.Close();
            }
            dr.Close();

            return tools;



        }

        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            conn.Close();

        }
    }

    public static bool insertArtifact(Addartifact addArtifact, int userid)
    {
        try
        {
            bool inserted = false;
            
            String query = "insert into artifact(ArtifactID, ArtifactName, [desc], est_hour, lock_hour, toolid, comments, status, createdfor) " +
                " values(@artid, @artname, @artdesc, @esthour, 0, @toolid, ' ', 'In progress', @userid)";


            cmd = new SqlCommand(query, conn);



            cmd.Parameters.AddWithValue("@artid", addArtifact.artNum);
            cmd.Parameters.AddWithValue("@artname", addArtifact.artName);
            cmd.Parameters.AddWithValue("@artdesc", addArtifact.desc);
            cmd.Parameters.AddWithValue("@esthour", addArtifact.esthour);
            cmd.Parameters.AddWithValue("@toolid", addArtifact.toolid);
            cmd.Parameters.AddWithValue("@userid", userid);


            conn.Open();
            int dr = cmd.ExecuteNonQuery();
            if (dr > 0)
                inserted = true;
            conn.Close();
            return inserted;

        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            conn.Close();

        }

    }


    // public static bool addArtifact(a)

    public static DataSet getArtifactDataset(int userid)
    {
        try
        {
            String sCommand1_subbu1 = " select ar.*,est_hour-lock_hour as remhour,tr.toolname from artifact ar inner join tools tr  on ar.toolid=tr.toolID where createdfor= @userid and status='In Progress'  or status= 'pending'";
            //  DataTable dataSubbu1 = new DataTable();
            SqlDataAdapter adapterSubbu1 = new SqlDataAdapter(sCommand1_subbu1, connString1);
            adapterSubbu1.SelectCommand.Parameters.AddWithValue("@userid", userid);

            DataSet ds = new DataSet();
            adapterSubbu1.Fill(ds);
            conn.Close();
            return ds;
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            conn.Close();

        }
    }

    public static DataSet toolList()
    {
        String sCommand1_subbu1 = " select * from tools";
        //  DataTable dataSubbu1 = new DataTable();
        SqlDataAdapter adapterSubbu1 = new SqlDataAdapter(sCommand1_subbu1, connString1);

        DataSet ds = new DataSet();
        adapterSubbu1.Fill(ds);
        conn.Close();

        return ds;
    }
    public static bool insertTools(Tools tools)
    {
        try
        {
            bool inserted = false;

            String query = "insert into tools(toolname,  [desc]) " +
                " values(@toolname, @desc)";


            cmd = new SqlCommand(query, conn);



            cmd.Parameters.AddWithValue("@toolname", tools.toolname);
            cmd.Parameters.AddWithValue("@desc", tools.desc);


            conn.Open();
            int dr = cmd.ExecuteNonQuery();
            if (dr > 0)
                inserted = true;
            conn.Close();
            return inserted;
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            conn.Close();

        }
    }

    public static bool insertUser(User user)
    {
        try
        {
            bool inserted = false;

            String query = "insert into [user](username,name,toolid,role,first) " +
                " values(@username, @name,@toolid,@role,0)";


            cmd = new SqlCommand(query, conn);



            cmd.Parameters.AddWithValue("@username", user.username);
            cmd.Parameters.AddWithValue("@name", user.name);
            cmd.Parameters.AddWithValue("@toolid", user.toolid);
            cmd.Parameters.AddWithValue("@role", user.role);

            conn.Open();
            int dr = cmd.ExecuteNonQuery();
            if (dr > 0)
                inserted = true;
            conn.Close();
            return inserted;
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            conn.Close();

        }
    }
    public static bool lockHour(DSTUM dstum)
    {
        try
        {
            bool inserted = false;

            if (dstum.aid < 0)
            {
                String query = "insert into [dstum](todaydate,userid,todayhour,yesterdayhour,[desc]) " +
                   " values(@todaydate, @userid,@todayhour,@yesterdayhour,@desc)";


                cmd = new SqlCommand(query, conn);



                cmd.Parameters.AddWithValue("@todaydate", dstum.Todaydate);
                cmd.Parameters.AddWithValue("@userid", dstum.userid);

                cmd.Parameters.AddWithValue("@todayhour", dstum.Todayhour);
                cmd.Parameters.AddWithValue("@yesterdayhour", dstum.yesterdayHour);
                cmd.Parameters.AddWithValue("@desc", dstum.desc);

                conn.Open();
                int dr = cmd.ExecuteNonQuery();
                if (dr > 0)
                    inserted = true;
            }
            else
            {
                String query = "insert into [dstum](todaydate,userid,aid,todayhour,yesterdayhour,[desc]) " +
                    " values(@todaydate, @userid,@aid,@todayhour,@yesterdayhour,@desc)";


                using (cmd = new SqlCommand(query, conn))
                {



                    cmd.Parameters.AddWithValue("@todaydate", dstum.Todaydate);
                    cmd.Parameters.AddWithValue("@userid", dstum.userid);
                    cmd.Parameters.AddWithValue("@aid", dstum.aid);
                    cmd.Parameters.AddWithValue("@todayhour", dstum.Todayhour);
                    cmd.Parameters.AddWithValue("@yesterdayhour", dstum.yesterdayHour);
                    cmd.Parameters.AddWithValue("@desc", dstum.desc);

                    conn.Open();
                    int dr = cmd.ExecuteNonQuery();
                    if (dr > 0)
                        inserted = true;
                    conn.Close();
                }







                String updatequery = "update artifact set lock_hour=@lock_hour where AID=@aid";
                int lockhour = getlockHourforArtifact(dstum.aid);
                int updaatedlockhour = lockhour + dstum.yesterdayHour;
                cmd = new SqlCommand(updatequery, conn);

                cmd.Parameters.AddWithValue("@lock_hour", updaatedlockhour);
                cmd.Parameters.AddWithValue("@aid", dstum.aid);


                conn.Open();
                int dr1 = cmd.ExecuteNonQuery();
                if (dr1 > 0)
                    inserted = true;
                conn.Close();


            }
            return inserted;
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            conn.Close();

        }
    }

    private static int getlockHourforArtifact(int aid)
    {
        int lockhour = 0;
        try
        {
            cmd = new SqlCommand("select lock_hour from artifact where aid=@aid", conn);
            cmd.Parameters.AddWithValue("@aid", aid);

            conn.Open();
            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.HasRows)
            {
                dr.Read();

                lockhour = (int)dr[0];

            }
            dr.Close();

            return lockhour;

        }

        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            conn.Close();

        }

    }

    public static bool isUserExist(string username)
    {
        bool userExist = true;
        try
        {
            SqlCommand cmd = new SqlCommand("Select * from [user] where Username= @Username", conn);
            cmd.Parameters.AddWithValue("@Username", username);
            conn.Open();
            var result = cmd.ExecuteScalar();
            if (result == null)
            {
                userExist = false;
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            conn.Close();

        }
        return userExist;
    }


    public static List<Report> getReportDetails()
    {

        List<Report> listReport = new List<Report>();
        string query = " ;with username as";
        query += " (select distinct userid from[user] as ud";
        query += " ),report as ";
        query += " ( ";
        query += " select un.userID,case when(sum(todayhour) * 100 / 8) is null  then - 1 else(sum(todayhour) * 100 / 8) end as utilazation, case when sum(todayhour)< 4 then 'red' ";
        query += " else case when sum(todayhour)< 8 then 'orange' ";
        query += " else 'green' end end as mood  from username un  left join dstum ds on ds.userid = un.userID where ds.todaydate = CONVERT(date, getdate()) or ds.todaydate is null group by un.userID)";
        query += " select case when rt.utilazation < 0 then 'Leave' else cast(rt.utilazation as varchar) end as utilazation,mood ,'Yes' as attendance,ur.Name,tl.toolname from Report rt left join[user] ur on ur.userID = rt.userID left join tools tl  on tl.toolID = ur.toolid";

        try
        {

            cmd = new SqlCommand(query, conn);


            conn.Open();
            SqlDataReader dr = cmd.ExecuteReader();

            if(dr.HasRows)
            {
                while (dr.Read())
                {
                    Report report = new Report();
                    report.name = (string)dr[3];
                    report.attendance = (string)dr[2];
                    report.availability = (string)dr[0];
                    report.tools = (string)dr[4];
                    report.mood = (string)dr[1];

                    listReport.Add(report);
                }
            }
            conn.Close();
            dr.Close();

            return listReport;



        }

        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            conn.Close();

        }
    }

    public static DataTable getExcelSheetData()
    {

        DataTable dt = new DataTable();
        StringBuilder sb = new StringBuilder();
        sb.Append(" if (DATEPART(d, GETDATE()) < 5) ");
        sb.Append(" begin ");
        sb.Append(" select replace(convert(NVARCHAR, ds.todaydate, 106), ' ', '/') todaydate,ur.Name,tl.toolname,ar.ArtifactID,ar.[status],CASE WHEN Cast(ar.[ArtifactName] AS varchar) is null THEN 'Meeting/KT/Other'     ELSE Cast(ar.[ArtifactName] AS varchar)  END AS ArtifactName,ds.[desc],ds.Todayhour,ds.yesterdayhour, CASE WHEN Cast(ar.est_hour AS varchar) is null THEN 0      ELSE Cast(ar.est_hour AS varchar)      END AS est_hour,CASE WHEN Cast(ar.est_hour AS varchar) is null THEN 0     ELSE Cast(ar.est_hour-ar.lock_hour AS varchar)      END AS remhour,ar.comments,replace(convert(NVARCHAR, ar.enddate, 106), ' ', '/') enddate from dstum ds left outer join[user] ur on ur.userID = ds.userid left outer join artifact ar on ar.AID = ds.AID left outer join tools tl on tl.toolID = ur.toolid   where DATEPART(m, GETDATE()) = DATEPART(m, ds.todaydate)");
        sb.Append(" union");
        sb.Append(" select replace(convert(NVARCHAR, ds.todaydate, 106), ' ', '/') todaydate,ur.Name,tl.toolname,ar.ArtifactID,ar.[status],CASE WHEN Cast(ar.[ArtifactName] AS varchar) is null THEN 'Meeting/KT/Other'     ELSE Cast(ar.[ArtifactName] AS varchar)  END AS ArtifactName,ds.[desc],ds.Todayhour,ds.yesterdayhour,  CASE WHEN Cast(ar.est_hour AS varchar) is null THEN 0      ELSE Cast(ar.est_hour AS varchar)      END AS est_hour,CASE WHEN Cast(ar.est_hour AS varchar) is null THEN 0     ELSE Cast(ar.est_hour-ar.lock_hour AS varchar)      END AS remhour,ar.comments,replace(convert(NVARCHAR, ar.enddate, 106), ' ', '/') enddate from dstum ds left outer join[user] ur on ur.userID = ds.userid left outer join artifact ar on ar.AID = ds.AID left outer join tools tl on tl.toolID = ur.toolid   where DATEPART(m, ds.todaydate) = DATEPART(m, GETDATE()) - 1");
        sb.Append(" end");
        sb.Append(" else");
        sb.Append(" begin ");
        sb.Append(" select replace(convert(NVARCHAR, ds.todaydate, 106), ' ', '/') todaydate, ur.Name,tl.toolname,ar.ArtifactID,ar.[status],CASE WHEN Cast(ar.[ArtifactName] AS varchar) is null THEN 'Meeting/KT/Other'     ELSE Cast(ar.[ArtifactName] AS varchar)  END AS ArtifactName,ds.[desc],ds.Todayhour,ds.yesterdayhour,CASE WHEN Cast(ar.est_hour AS varchar) is null THEN 0      ELSE Cast(ar.est_hour AS varchar)      END AS est_hour,CASE WHEN Cast(ar.est_hour AS varchar) is null THEN 0     ELSE Cast(ar.est_hour-ar.lock_hour AS varchar)      END AS remhour,ar.comments,replace(convert(NVARCHAR, ar.enddate, 106), ' ', '/') enddate from dstum ds left outer join[user] ur on ur.userID = ds.userid left outer join artifact ar on ar.AID = ds.AID left outer join tools tl on tl.toolID = ur.toolid   where DATEPART(m, GETDATE()) = DATEPART(m, ds.todaydate) ");
        sb.Append(" end");
        try
        {



            using (SqlDataAdapter adapterSubbu1 = new SqlDataAdapter(sb.ToString(), connString1))
            {
                adapterSubbu1.Fill(dt);

            }


            return dt;

        }

        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            conn.Close();

        }
    }

    public static int getIdfromName(string Name)
    {
        int id = 0;

        try
        {
            cmd = new SqlCommand("select userID from [user]  where Name=@userid", conn);
            cmd.Parameters.AddWithValue("@userid", Name);

            conn.Open();
            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.HasRows)
            {
                dr.Read();
                id = (int)dr[0];
            
                conn.Close();
            }
            dr.Close();

        


        }

        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            conn.Close();

        }
        return id;

    }



    public static int gettoolIdfromName(string Name)
    {
        int id = 0;

        try
        {
            cmd = new SqlCommand("select toolid from [user]  where Name=@userid", conn);
            cmd.Parameters.AddWithValue("@userid", Name);

            conn.Open();
            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.HasRows)
            {
                dr.Read();
                id = (int)dr[0];

                conn.Close();
            }
            dr.Close();




        }

        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            conn.Close();

        }
        return id;

    }




    public static bool addArtifactFromExcel(List<ArtifactExcel> listArtifact)
    {
        bool isArtifactAdded = false;
        for (int i=0;i<listArtifact.Count;i++)
        {
            try
            {
                isArtifactAdded = false;

                String query = "insert into artifact(ArtifactID,ArtifactName,[desc],est_hour,lock_hour,toolid,[status],createdfor) values(@artid, @artname, @desc, @est_hour, 0, @toolid, 'In Progress', @id)";


                cmd = new SqlCommand(query, conn);



                cmd.Parameters.AddWithValue("@artid",listArtifact[i].artNum);
                cmd.Parameters.AddWithValue("@artname", listArtifact[i].artName);

                cmd.Parameters.AddWithValue("@desc", listArtifact[i].desc);
                cmd.Parameters.AddWithValue("@est_hour", listArtifact[i].esthour);

                cmd.Parameters.AddWithValue("@toolid", listArtifact[i].toolid);
                cmd.Parameters.AddWithValue("@id", listArtifact[i].id);


                conn.Open();
                int dr = cmd.ExecuteNonQuery();
                if (dr > 0)
                    isArtifactAdded = true;
                conn.Close();
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();

            }
        }

       return isArtifactAdded;
    }
}