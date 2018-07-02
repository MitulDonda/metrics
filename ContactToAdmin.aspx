<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ContactToAdmin.aspx.cs" Inherits="ContactToAdmin" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <link rel="shortcut icon" href="img/logo.png" />
    <link rel="stylesheet" href="css/menu.css" />
    <link rel="stylesheet" href="css/main.css" />
    <link rel="stylesheet" href="css/bgimg.css" />
    <link rel="stylesheet" href="css/font.css" />
    <link rel="stylesheet" href="css/font-awesome.min.css" />
    <script type="text/javascript" src="js/jquery-1.12.4.min.js"></script>
    <script type="text/javascript" src="js/main.js"></script>
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css" />
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>
    <style>
        .gridview2 tr td {
            padding: 4px;
            border: 1px solid #ddd;
        }

        .gridview2 tr th {
            padding: 8px;
            border: 1px solid #ddd;
        }

        .gridview2 tr td #Button1 {
            padding: 8px;
            color: red;
        }

        table {
            border-collapse: collapse;
        }

        .outside_border {
            border-radius: 5px 50px;
            border: 2px solid #00468b;
            padding: 20px;
            margin: 10px 30px 5px 30px;
            background: #fff;
        }

        .outside_border1 {
            border-radius: 30px 30px;
            border: 2px solid #00468b;
            padding: 20px;
            margin: 10px 30px 5px 30px;
            background: #fff;
        }

        th, td {
            text-align: left;
            padding: 5px;
        }

        .header {
            overflow: hidden;
         
            padding: 10px 10px;
            margin-top: 15px;
            font-size: 20px;
        }
         .navbar {
            margin-bottom: 0;
            border-radius: 0;
              background: #1f5687
              ;
              
        }
          .collapse ul.navbar-nav>li>a {
    color: white;
}
        /* Set height of the grid so .sidenav can be 100% (adjust as needed) */
        .row.content {
            height: 450px
        }
    </style>
</head>
<body class="background1">
      <nav class="navbar navbar-inverse">
        <div class="container-fluid">
            <div class="navbar-header">
                <button type="button" class="navbar-toggle" data-toggle="collapse" data-target="#myNavbar">
                    <span class="icon-bar"></span>
                    <span class="icon-bar"></span>
                    <span class="icon-bar"></span>
                </button>

            </div>
            <div class="collapse navbar-collapse" id="myNavbar">
               
                <ul class="nav navbar-nav navbar-right">
                    <li>
                        <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Login.aspx"><span class="glyphicon glyphicon-log-out"></span>&nbsp;&nbsp;Logout</asp:HyperLink></li>
                </ul>
            </div>
        </div>
    </nav>

    <form id="form1" runat="server">
        <div>

            <div class="outside_border1">

                <div align="center">
                    <b>
                        <asp:Label ID="Label1" runat="server" Text="Label" CssClass="header">Please Contact To Admin.You don't have access to this DSTUM</asp:Label>
                    </b>
                </div>
            </div>


        </div>
    </form>
</body>
</html>
