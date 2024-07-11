<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Add_Item.aspx.cs" Inherits="Learning_the_Basics.Add_Item" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml" lang="en">
<head runat="server">
    <meta charset="UTF-8"/>
    <meta name="viewport" content="width=device-width, initial-scale=1.0"/>
    <title>Inventory Management</title>
    <link rel="stylesheet" href="styles.css" />
    <script>
        function addItem() {
            var item = {
                ProductName: document.getElementById('<%= ItemName.ClientID %>').value,
                Model: document.getElementById('<%= ItemModel.ClientID %>').value,
                Description: document.getElementById('<%= ItemDescription.ClientID %>').value,
                Category: document.getElementById('<%= ItemCategory.ClientID %>').value,
                Qty: document.getElementById('<%= ItemQuantity.ClientID %>').value,
                Price: document.getElementById('<%= ItemPrice.ClientID %>').value,
                LastUpdated: new Date().toISOString()
            };

            fetch('AddItem', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(item)
            }).then(response => {
                if (response.ok) {
                    alert('Item added successfully');
                    window.location.reload();
                } else {
                    alert('Failed to add item');
                }
            });
        }
    </script>
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
            <h1>Inventory Management System</h1>
            <div class="form-group">
                <asp:Panel ID="AddItemPanel" runat="server" Visible="false">
                    <asp:TextBox ID="ItemName" runat="server" CssClass="form-control" placeholder="Item Name"></asp:TextBox>
                    <asp:TextBox ID="ItemModel" runat="server" CssClass="form-control" placeholder="Item Model"></asp:TextBox>
                    <asp:TextBox ID="ItemDescription" runat="server" CssClass="form-control" placeholder="Item Description"></asp:TextBox>
                    <asp:TextBox ID="ItemCategory" runat="server" CssClass="form-control" placeholder="Item Category"></asp:TextBox>
                    <asp:TextBox ID="ItemQuantity" runat="server" CssClass="form-control" placeholder="Item Quantity" TextMode="Number"></asp:TextBox>
                    <asp:TextBox ID="ItemPrice" runat="server" CssClass="form-control" placeholder="Item Price" TextMode="Number"></asp:TextBox>
                    <asp:Button ID="AddItemButton" runat="server" CssClass="btn btn-primary" Text="Add Item" OnClick="AddItemButton_Click" />
                    <asp:Button ID="CancelButton" runat="server" CssClass="btn btn-secondary" Text="Cancel" OnClick="CancelButton_Click" />
                </asp:Panel>
                <asp:Button ID="AddNewButton" runat="server" CssClass="btn btn-primary" Text="Add New Item" OnClick="AddNewButton_Click" Visible="true" />
            </div>
        </div>

        <div class="container">
            <div class="search-container">
                <asp:TextBox ID="SearchCategory" runat="server" CssClass="form-control" placeholder="Search by Category"></asp:TextBox>
                <asp:Button ID="SearchCategoryButton" runat="server" CssClass="btn btn-secondary" Text="Search" OnClick="SearchCategoryButton_Click" />
                <asp:TextBox ID="SearchModel" runat="server" CssClass="form-control" placeholder="Search by Model"></asp:TextBox>
                <asp:Button ID="SearchModelButton" runat="server" CssClass="btn btn-secondary" Text="Search" OnClick="SearchModelButton_Click" />
            </div>

            <asp:GridView ID="ItemsGridView" runat="server" AutoGenerateColumns="False" CssClass="table table-striped" OnRowEditing="ItemsGridView_RowEditing" OnRowDeleting="ItemsGridView_RowDeleting">
                <Columns>
                    <asp:BoundField DataField="ProductName" HeaderText="Product Name" />
                    <asp:BoundField DataField="Model" HeaderText="Model" />
                    <asp:BoundField DataField="LastUpdated" HeaderText="Last Updated" />
                    <asp:TemplateField HeaderText="Actions">
                        <ItemTemplate>
                            <asp:Button ID="EditButton" runat="server" CssClass="edit" Text="Edit" CommandName="Edit" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </form>
</body>
</html>
