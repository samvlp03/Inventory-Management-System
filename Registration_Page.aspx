<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Registration_Page.aspx.cs" Inherits="Inventory_Management_System.Registration_Page" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Registration Page</title>
    <link rel="stylesheet" href="styles.css" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <table>
                <tr>
                    <td>Full Name</td>
                    <td>
                        <asp:TextBox ID="txtName" runat="server" placeholder="Enter your full name"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>Gender</td>
                    <td>
                        <asp:RadioButton GroupName="user" ID="RadioButton1" runat="server" Text="Male" OnCheckedChanged="RadioButton1_CheckedChanged" /><br />
                        <asp:RadioButton GroupName="user" ID="RadioButton2" runat="server" Text="Female" OnCheckedChanged="RadioButton2_CheckedChanged" /><br />
                        <asp:RadioButton GroupName="user" ID="RadioButton3" runat="server" Text="Others" OnCheckedChanged="RadioButton3_CheckedChanged" />
                    </td>
                </tr>
                <tr>
                    <td>Phone</td>
                    <td>
                        <asp:TextBox ID="txtPhone" runat="server" placeholder="+977-012-345-6789"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>Email</td>
                    <td>
                        <asp:TextBox ID="txtEmail" runat="server" placeholder="example@example.com" TextMode="Email"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>Password</td>
                    <td>
                        <asp:TextBox ID="txtPass" runat="server" placeholder="*****" TextMode="Password"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td></td>
                    <td align="right">
                        <asp:Button ID="btnReg" runat="server" Text="Register" CssClass="btn" OnClick="btnReg_Click" />
                    </td>
                </tr>
            </table>
            <div class="links">
                <a href="Login_Page.aspx">Existing User? Sign In</a> 
  
            </div>
        </div>
        <!-- QR Code section 
        <div class="qr-code-section">
            <h3>Scan QR Code to Register</h3>
            <img id="qrCodeRegistration" alt="QR Code" />
        </div>
            -->
    </form>
</body>
</html>
