using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.Script.Serialization;
using System.Net;
using System.IO;
using Newtonsoft.Json.Linq;
using System.DirectoryServices;

public partial class Login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        FormsAuthentication.SignOut();
    }

    protected void login_Click(object sender, EventArgs e)
       {
        string txtPwd = txtPassword.Value;
        string userName = txtLoginID.Value.Trim();
        string strError = string.Empty;


        DirectoryEntry root = new DirectoryEntry(
         "LDAP://ldapfr.capgemini.com/CN=Users,DC=corp,DC=capgemini,DC=com",
            userName,
            txtPwd
    );

        //query for the username provided
        DirectorySearcher searcher = new DirectorySearcher(
            root,
            "(sAMAccountName=" + userName + ")"
            );

        //a success means the password was right
        bool success = false;
        try
        {
            searcher.FindOne();
            success = true;
        }
        catch
        {
            success = false;
        }

        if (success == true)
        {
            Session["login"] = userName;
            Session["pwd"] = txtPwd;
            FormsAuthentication.RedirectFromLoginPage
              (userName, true);

        }
        else
        {

            txtLoginID.Value = string.Empty;
            txtPassword.Value = string.Empty;
            lblError.Text = "Invalid credentials. Please try again.";

        }

    }


}