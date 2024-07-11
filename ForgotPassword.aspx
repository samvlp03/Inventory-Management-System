<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ForgotPassword.aspx.cs" Inherits="StoreManagement.ForgotPassword" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Forgot Password</title>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.0.0-beta3/css/all.min.css" />
    <link rel="stylesheet" href="style.css" />
    <style>
        body {
            font-family: Arial, sans-serif;
            background-color: #f2f2f2;
            margin: 0;
            padding: 0;
            display: flex;
            justify-content: center;
            align-items: center;
            height: 100vh;
        }
        .container {
            background-color: #fff;
            padding: 40px 50px;
            border-radius: 10px;
            box-shadow: 0 0 20px rgba(0, 0, 0, 0.1);
            text-align: center;
            max-width: 500px;
            width: 100%;
        }
        .container h2 {
            color: #2d572c;
            margin-bottom: 30px;
            font-size: 28px;
        }
        .form-group {
            margin-bottom: 20px;
            text-align: left;
        }
        .form-group label {
            display: block;
            margin-bottom: 10px;
            color: #333;
            font-size: 16px;
        }
        .form-group input {
            width: 100%;
            padding: 12px;
            border: 1px solid #ccc;
            border-radius: 6px;
            font-size: 16px;
        }
        .form-group input:focus {
            border-color: #2d572c;
            outline: none;
        }
        .btn-submit {
            width: 100%;
            padding: 12px;
            background-color: #2d572c;
            border: none;
            border-radius: 6px;
            color: #fff;
            font-size: 16px;
            cursor: pointer;
        }
        .btn-submit:hover {
            background-color: #1e3e1f;
        }
        .message {
            margin-top: 20px;
            color: #e74c3c;
            font-size: 16px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <h2>Forgot Password</h2>
            <div class="form-group">
                <asp:Label ID="Label1" runat="server" Text="Enter your email address:" AssociatedControlID="EmailTextBox" />
                <asp:TextBox ID="EmailTextBox" runat="server" CssClass="form-control" />
            </div>
            <div class="form-group">
                <asp:Button ID="SubmitButton" runat="server" Text="Submit" OnClick="SubmitButton_Click" CssClass="btn-submit" />
            </div>
            <asp:Label ID="MessageLabel" runat="server" Text="" CssClass="message" />
        </div>
    </form>
</body>
</html>