<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ADDTools.aspx.cs" Inherits="ADDTools" %>

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
            padding: 5px;
            border: 1px solid #ddd;
        }

        .gridview2 tr th {
            padding: 10px;
            border: 1px solid #ddd;
        }

        .gridview2 tr td #Button1 {
            padding: 10px;
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

        th, td {
            text-align: left;
            padding: 5px;
        }

        .header {
            overflow: hidden;
            color: #f1f1f1;
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
                <ul class="nav navbar-nav" runat="server">
                    <li runat="server">
                        <asp:HyperLink ID="hlhome" runat="server" NavigateUrl="~/Default.aspx">Home</asp:HyperLink></li>
                    <li runat="server">
                        <asp:HyperLink ID="hladdartifact" runat="server" NavigateUrl="~/Default2.aspx">Add Artifact</asp:HyperLink></li>

                </ul>
                <ul class="nav navbar-nav navbar-right">
                    <li>
                        <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Login.aspx"><span class="glyphicon glyphicon-log-out"></span>&nbsp;&nbsp;Logout</asp:HyperLink></li>
                </ul>
            </div>
        </div>
    </nav>

    <form id="form1" runat="server">
        <div>
            <div align="center">
                <b>
                    <asp:Label ID="Label1" runat="server" Text="Label" CssClass="header">ADD Tools</asp:Label>
                </b>
            </div>
            <div class="outside_border">
                <table align="center">

                    <tr>
                        <td><b>Tool Name</b>
                        </td>
                        <td>
                            <asp:TextBox ID="txtToolname" runat="server" placeholder="Tool Name"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*" ControlToValidate="txtToolname" ForeColor="Red" ValidationGroup="addtool"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td><b>Description</b>
                        </td>
                        <td>
                            <asp:TextBox ID="txtdesc" runat="server" placeholder="Description"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*" ControlToValidate="txtdesc" ForeColor="Red" ValidationGroup="addtool"></asp:RequiredFieldValidator>
                        </td>
                    </tr>



                    <tr align="center">
                        <td colspan="2" style="margin-left: 60px">
                            <asp:Button ID="Addtools" CssClass="btn btn-default" runat="server" ValidationGroup="addtool" Text="ADD Tools" OnClick="Addtools_Click" />
                        </td>
                    </tr>
                </table>


            </div>
            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>


            <div id="dvGrid" style="padding: 10px; width: 550px">

                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">

                    <ContentTemplate>


                        <asp:GridView ID="GridView1" CssClass="gridview2" runat="server" AutoGenerateColumns="False"
                            DataKeyNames="toolID" CellSpacing="10"
                            CellPadding="6" ForeColor="#333333" GridLines="None" OnRowCommand="GridView1_RowCommand" OnRowCreated="GridView1_RowCreated">
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>

                                <asp:TemplateField HeaderText="Artifact Id">
                                    <ItemTemplate>
                                        <asp:Label ID="lblToolName" runat="server"
                                            Text='<%# Eval("toolname")%>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtToolName" runat="server"
                                            Text='<%# Eval("toolname")%>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemStyle Width="100px" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Artifact Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lbldesc" runat="server"
                                            Text='<%# Eval("desc")%>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtdesc" runat="server"
                                            Text='<%# Eval("desc")%>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemStyle Width="100px" />
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="action">
                                    <ItemTemplate>
                                        <asp:Button ID="Button1" CssClass="btn btn-primary  btn-sm" runat="server" Text="edit" CommandArgument='<%# Eval("toolID")%>' CommandName="EditRow" />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:Button ID="Button1" CssClass="btn btn-success btn-sm" runat="server" Text="Update" CommandArgument='<%# Eval("toolID")%>' CommandName="UpdateRow" />
                                        <asp:Button ID="Button2" CssClass="btn btn-danger  btn-sm" runat="server" Text="Cancel" CommandArgument='<%# Eval("toolID")%>' CommandName="CancelRow" />
                                    </EditItemTemplate>
                                    <ItemStyle Width="100px" />
                                </asp:TemplateField>


                            </Columns>
                            <EditRowStyle BackColor="#7C6F57" />
                            <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                            <RowStyle BackColor="#E3EAEB" />
                            <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                            <SortedAscendingCellStyle BackColor="#F8FAFA" />
                            <SortedAscendingHeaderStyle BackColor="#246B61" />
                            <SortedDescendingCellStyle BackColor="#D4DFE1" />
                            <SortedDescendingHeaderStyle BackColor="#15524A" />
                               <EmptyDataTemplate>
                                <table cellspacing="2" cellpadding="3" rules="all" id="GridView1" style="background-color: #1C5E55; border-color: #1C5E55; border-width: 1px; border-style: None;">
                                    <tr style="color: White; background-color: #1C5E55; font-weight: bold;">
                                        <td scope="col">Tool Name
                                        </td>
                                        <td scope="col">Toolid
                                        </td>
                                        <td scope="col">desc
                                        </td>
                                        
                                    </tr>
                                    <tr style="color: #1C5E55; background-color: #FFF7E7;">
                                        <td colspan="4">There are no data to display
                                        </td>
                                    </tr>
                                </table>
                            </EmptyDataTemplate>
                        </asp:GridView>
                        <asp:GridView ID="GridView2" runat="server">
                        </asp:GridView>
                    </ContentTemplate>

                    <Triggers>

                        <asp:AsyncPostBackTrigger ControlID="GridView1" />

                    </Triggers>

                </asp:UpdatePanel>
            </div>



        </div>



        </div>
    </form>
</body>
</html>
