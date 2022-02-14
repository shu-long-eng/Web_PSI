<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="PSIsystem.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <style>
        #Msg {
            color:red;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            帳號:<asp:TextBox ID="AccountText" runat="server" MaxLength="8"></asp:TextBox>
            密碼:<asp:TextBox ID="PWDText" runat="server" MaxLength="8" TextMode="Password"></asp:TextBox>
            <asp:Button ID="LogIn" runat="server" Text="登入" OnClick="Login_Click"/>
            <asp:Label ID="Msg" runat="server" Text="Label">帳號或密碼錯誤</asp:Label>
        </div>
    </form>
</body>
</html>
