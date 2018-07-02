<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<!DOCTYPE html>
<html>
	<head>
		<title>DSTUM</title>
		<meta name="viewport" content="width=device-width, initial-scale=1.0"/>
		<link rel="shortcut icon" href="img/logo.png"/>
		<link rel="stylesheet" href="css/menu.css"/>
		<link rel="stylesheet" href="css/main.css"/>
		<link rel="stylesheet" href="css/bgimg.css"/>
		<link rel="stylesheet" href="css/font.css"/>
		<link rel="stylesheet" href="css/font-awesome.min.css"/>
		<script type="text/javascript" src="js/jquery-1.12.4.min.js"></script>
		<script type="text/javascript" src="js/main.js"></script>
	</head>
<body>
	
	<div class="background"></div>
	<div class="backdrop"></div>
	<div class="login-form-container" id="login-form">
		<div class="login-form-content">
			<div class="login-form-header">
				<div >
					<img src="img/logo.jpg"/>
				</div>
				<h3>Login to DSTUM</h3>
			</div>
			<form method="post" action="" class="login-form" runat="server">
				<div class="input-container">
					<i class="fa fa-user"></i>
                                 <input type="text" id="txtLoginID" value="Username" class="input" runat="server"  onfocus="this.value = '';" onblur="if (this.value == '') {this.value = 'Username';}"/>
       
					
				</div>
				<div class="input-container">
					<i class="fa fa-lock"></i>
               <input type="password" id="txtPassword"  value="Password" class="input" runat="server" placeholder="Password" />
         
					<i id="show-password" class="fa fa-eye"></i>
				</div>
				  <asp:Button ID="login" class="button" runat="server" Text="Login" OnClick="login_Click" />
					  <asp:Label ID="lblError" runat="server" ForeColor="Red" Text=""></asp:Label>
   
			</form>
			
			
		</div>
	</div>
</body>
</html>
