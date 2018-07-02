<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ArttifactUpload.aspx.cs" Inherits="ArttifactUpload" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:FileUpload ID="FileUpload1" runat="server" />  
     <asp:Button ID="load_excel" runat="server" Text="Load Excel"  
             OnClick="load_excel_Click" /> 

            <asp:GridView ID="GridView1" runat="server"></asp:GridView>  <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
        </div>
    </form>
</body>
</html>
