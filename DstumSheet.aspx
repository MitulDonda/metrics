<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DstumSheet.aspx.cs" Inherits="DstumSheet" %>

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

    <script src="http://getbootstrap.com/2.3.2/assets/css/bootstrap.css"></script>
    <script src="http://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.10.0.min.js" type="text/javascript"></script>
    <script src="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.9.2/jquery-ui.min.js" type="text/javascript"></script>
    <link href="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.9.2/themes/blitzer/jquery-ui.css"
        rel="Stylesheet" type="text/css" />
    <script type="text/javascript">

        $(function () {
            $("[id$=txtusename]").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: '<%=ResolveUrl("~/DstumSheet.aspx/GetMembers") %>',
                        data: "{ 'prefix': '" + request.term + "'}",
                        dataType: "json",
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        success: function (data) {
                            response($.map(data.d, function (item) {
                                return {
                                    label: item.split('-')[0],
                                    val: item.split('-')[1]
                                }
                            }))
                        },
                        error: function (response) {
                            alert(response.responseText);
                        },
                        failure: function (response) {
                            alert(response.responseText);
                        }
                    });
                },
                select: function (e, i) {
                    $("[id$=hfuserid]").val(i.item.val);

                },
                minLength: 1
            });
        });

    </script>



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
<body class="background">
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
        <div runat="server">
            <div align="center">
                <b>
                    <asp:Label ID="Label1" runat="server" Text="Label" CssClass="header">DSTUM Sheet</asp:Label>
                </b>
                <asp:Button ID="btnexptoexcel" CssClass="btn btn-default" runat="server" Text="Export To Excel File" OnClick="btnexptoexcel_Click" Visible="false" />

            </div>

            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

            <div align="center">
                <table>
                    <tr>
                        <td>
                               <asp:TextBox ID="txtusename"  class="form-control" runat="server"  AutoPostBack="true"  OnTextChanged="txtSearch_TextChanged" placeholder="Name"></asp:TextBox>

                        </td>
                        <td>
                            <div class="input-group date form_date" data-date="" data-date-format="yyyy-mm-dd" data-link-field="dtp_input2" data-link-format="yyyy-mm-dd"  runat="server">
                             <asp:TextBox  class="form-control" ID="dtp_input211"  size="16" type="text" value=""  placeholder="Choose Date"  runat="server" OnTextChanged="txtSearch_TextChanged" AutoPostBack="true"></asp:TextBox>
                                  
                                <span class="input-group-addon"><span class="glyphicon glyphicon-remove"></span></span>
                                <span class="input-group-addon"><span class="glyphicon glyphicon-calendar"></span></span>
                            </div>

                        </td>
                    </tr>
                </table>





                <asp:TextBox ID="dtp_input2" runat="server" Visible="false"  AutoPostBack="true"  ></asp:TextBox>
                <asp:HiddenField ID="hfuserid" runat="server" />
            </div>



           

            <div id="dvGrid" style="padding: 10px; width: 550px" runat="server">


                <asp:GridView ID="GridView1" CssClass="gridview2" runat="server" AutoGenerateColumns="False"
                    DataKeyNames="DID" CellSpacing="10"
                    CellPadding="6" ForeColor="#333333" GridLines="None" OnRowCommand="GridView1_RowCommand">
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>

                        <asp:TemplateField HeaderText="Date">
                            <ItemTemplate>
                                <asp:Label ID="lbldate" runat="server"
                                    Text='<%# Eval("todaydate")%>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="30px" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Member Name">
                            <ItemTemplate>
                                <asp:Label ID="lblName" runat="server"
                                    Text='<%# Eval("Name")%>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtName" runat="server"
                                    Text='<%# Eval("Name")%>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemStyle Width="100px" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Tool">
                            <ItemTemplate>
                                <asp:Label ID="lbltoolname" runat="server"
                                    Text='<%# Eval("toolname")%>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txttoolname" runat="server"
                                    Text='<%# Eval("[toolname]")%>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemStyle Width="100px" />
                        </asp:TemplateField>



                        <asp:TemplateField HeaderText="Artifact Id">
                            <ItemTemplate>
                                <asp:Label ID="lblartifactid" runat="server"
                                    Text='<%# Eval("artifactid")%>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtartifactid" runat="server"
                                    Text='<%# Eval("artifactid")%>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemStyle Width="100px" />
                        </asp:TemplateField>


                        <asp:TemplateField HeaderText="Work Description">
                            <ItemTemplate>
                                <asp:Label ID="lblartifactname" runat="server"
                                    Text='<%# Eval("artifactname")%>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtartifactname" runat="server"
                                    Text='<%# Eval("artifactname")%>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemStyle Width="100px" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="status">
                            <ItemTemplate>
                                <asp:Label ID="lblstatus" runat="server"
                                    Text='<%# Eval("status")%>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtstatus" runat="server"
                                    Text='<%# Eval("status")%>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemStyle Width="100px" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Commentes">
                            <ItemTemplate>
                                <asp:Label ID="lblcomments" runat="server"
                                    Text='<%# Eval("[desc]")%>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtcomments" runat="server"
                                    Text='<%# Eval("[desc")%>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemStyle Width="100px" />
                        </asp:TemplateField>


                        <asp:TemplateField HeaderText="Est. hour">
                            <ItemTemplate>
                                <asp:Label ID="lblesthour" runat="server"
                                    Text='<%# Eval("est_hour")%>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtesthour" runat="server"
                                    Text='<%# Eval("est_hour")%>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemStyle Width="100px" />
                        </asp:TemplateField>


                        <asp:TemplateField HeaderText="Remaining Hour">
                            <ItemTemplate>
                                <asp:Label ID="lblremhour" runat="server"
                                    Text='<%# Eval("remhour")%>'></asp:Label>
                            </ItemTemplate>

                            <ItemStyle Width="100px" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Today hour">
                            <ItemTemplate>
                                <asp:Label ID="lbltohour" runat="server"
                                    Text='<%# Eval("todayhour")%>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txttohour" runat="server"
                                    Text='<%# Eval("todayhour")%>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemStyle Width="100px" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Yesterday hour">
                            <ItemTemplate>
                                <asp:Label ID="lblyesterdayhour" runat="server"
                                    Text='<%# Eval("yesterdayhour")%>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtyesterdayhour" runat="server"
                                    Text='<%# Eval("yesterdayhour")%>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemStyle Width="100px" />
                        </asp:TemplateField>




                        <asp:TemplateField HeaderText="Fixed ON">
                            <ItemTemplate>
                                <asp:Label ID="lblfixedon" runat="server"
                                    Text='<%# Eval("enddate")%>'></asp:Label>
                            </ItemTemplate>
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
                                        <td scope="col">Today date
                                        </td>
                                        <td scope="col">Est. hour
                                        </td>
                                        <td scope="col">Rem Hour
                                        </td>
                                        <td scope="col">Status
                                        </td>
                                    </tr>
                                    <tr style="color: #1C5E55; background-color: #FFF7E7;">
                                        <td colspan="4">There are no data for perticulare Date and User
                                        </td>
                                    </tr>
                                </table>
                            </EmptyDataTemplate>
                </asp:GridView>



            </div>



        </div>
    </form>
    <script type="text/javascript" src="./js/bootstrap-datetimepicker.js" charset="UTF-8"></script>
    <script type="text/javascript" src="./js/locales/bootstrap-datetimepicker.id.js" charset="UTF-8"></script>
    <script type="text/javascript">

        $('.form_date').datetimepicker({
            language: 'fr',
            weekStart: 1,
            todayBtn: 1,
            autoclose: 1,
            todayHighlight: 1,
            startView: 2,
            minView: 2,
            forceParse: 0
        });

</script>
</body>

</html>
