<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="Inventory_Management_System.Home" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Home Page</title>
    <link rel="stylesheet" href="styles.css" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="buttons">
    <asp:Button ID="btnAddNewItem" runat="server" Text="Add New Item/Edit" OnClick="btnAddNewItem_Click" CssClass="btn" />
    <asp:Button ID="btnLogout" runat="server" Text="Logout" OnClick="btnLogout_Click" CssClass="btn" />
</div>
        <div class="container">
            <h1>Inventory Management System</h1>
            <table id="productTable" runat="server" class="table">
                <thead>
                    <tr>
                    
                        <th>ProductName</th>
                        <th>Model</th>
                        <th>Description</th>
                        <th>Category</th>
                        <th>Qty</th>
                        <th>Price</th>
                        <th>LastUpdated</th>
                    </tr>
                </thead>
                <tbody>
                    <!-- Data rows will be populated dynamically -->
                </tbody>
            </table>
            
        </div>
    </form>
</body>
</html>
