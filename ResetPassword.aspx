<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ResetPassword.aspx.cs" Inherits="StoreManagement.ResetPassword" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Reset Password</title>
    <link rel="stylesheet" href="styles.css" />
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Label ID="Label1" runat="server" Text="New Password:"></asp:Label>
            <asp:TextBox ID="NewPasswordTextBox" runat="server" TextMode="Password"></asp:TextBox>
            <br />
            <asp:Label ID="Label2" runat="server" Text="Confirm Password:"></asp:Label>
            <asp:TextBox ID="ConfirmPasswordTextBox" runat="server" TextMode="Password"></asp:TextBox>
            <br />
            <asp:Button ID="ResetButton" runat="server" Text="Reset Password" OnClick="ResetButton_Click" />
            <br />
            <asp:Label ID="MessageLabel" runat="server" ForeColor="Red"></asp:Label>
        </div>
    </form>
</body>
</html>
