<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login_Page.aspx.cs" Inherits="WebApplication2.LoginPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Login Page</title>
    <link rel="stylesheet" href="styles.css" />
    
    <script>
        function togglePasswordVisibility() {
            var passwordField = document.getElementById('<%= TextBox2.ClientID %>');
            var showPasswordCheckbox = document.getElementById('<%= CheckBoxShowPassword.ClientID %>');

            passwordField.type = showPasswordCheckbox.checked ? 'text' : 'password';
        }

        function checkLoginStatus(token) {
            setInterval(function() {
                fetch(`/CheckLoginStatus.ashx?token=${token}`)
                    .then(response => response.json())
                    .then(data => {
                        if (data.isLoggedIn) {
                            window.location.href = 'LoginSuccess.aspx';
                        }
                    });
            }, 5000); // Poll every 5 seconds
        }

        document.addEventListener('DOMContentLoaded', function() {
            var token = '<%= ViewState["token"] %>';
            if (token) {
                checkLoginStatus(token);
            }
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Label ID="Label1" runat="server" Text="Login Form"></asp:Label>
        </div>
        <div>
            <label for="TextBox1">Email:</label>
            <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
        </div>
        <div>
            <label for="TextBox2">Password:</label>
            <asp:TextBox ID="TextBox2" runat="server" TextMode="Password"></asp:TextBox>
        </div>
        <div>
            <asp:CheckBox ID="CheckBoxShowPassword" runat="server" Text="Show Password" onchange="togglePasswordVisibility();" />
        </div>
        <div>
            <asp:Button CssClass="btn-login" ID="Button1" runat="server" Text="Login" OnClick="Button1_Click" />
            <asp:Button CssClass="btn-cancel" ID="Button2" runat="server" Text="Cancel" />
        </div>
        <div class="links">
            <a href="Registration_Page.aspx">New User? Sign Up</a> |
            <a href="ForgotPassword.aspx">Forgot Password?</a>
        </div>
        <!-- QR Code section 
        <div class="qr-code-section">
            <h3>Scan QR Code to Login</h3>
            <img id="qrCodeLogin" alt="QR Code" />
        </div>
            -->
    </form>
</body>
</html>
