<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Edit_Item.aspx.cs" Inherits="Learning_the_Basics.Edit_Item" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml" lang="en">
<head runat="server">
    <meta charset="UTF-8"/>
    <meta name="viewport" content="width=device-width, initial-scale=1.0"/>
    <title>Edit Item</title>
    <link rel="stylesheet" href="styles.css" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="navbar-container">
            <div class="navbar">
                
                <nav>
                    <a href="Home.aspx">Home</a>
                    <a href="Login_Page.aspx">Logout</a>
                </nav>
            </div>
        </div>

        <div class="container">
            <h1>Edit Item</h1>
            <div class="form-group">
                <asp:TextBox ID="ItemName" runat="server" CssClass="form-control" placeholder="Item Name" ReadOnly="True"></asp:TextBox>
                <asp:TextBox ID="ItemModel" runat="server" CssClass="form-control" placeholder="Item Model"></asp:TextBox>
                <asp:TextBox ID="ItemDescription" runat="server" CssClass="form-control" placeholder="Item Description"></asp:TextBox>
                <asp:TextBox ID="ItemCategory" runat="server" CssClass="form-control" placeholder="Item Category"></asp:TextBox>
                <asp:TextBox ID="ItemQuantity" runat="server" CssClass="form-control" placeholder="Item Quantity" TextMode="Number"></asp:TextBox>
                <asp:TextBox ID="ItemPrice" runat="server" CssClass="form-control" placeholder="Item Price" TextMode="Number"></asp:TextBox>
                <asp:Button ID="SaveChangesButton" runat="server" CssClass="btn btn-primary" Text="Save Changes" OnClick="SaveChangesButton_Click" />
            </div>
        </div>
    </form>
</body>
</html>
