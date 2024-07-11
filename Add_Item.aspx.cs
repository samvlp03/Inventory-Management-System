using System;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI.WebControls;

namespace Learning_the_Basics
{
    public partial class Add_Item : System.Web.UI.Page
    {
        private static readonly string connectionString = "Data Source=SAMARTH-PC;Initial Catalog=loginasp;Integrated Security=True;TrustServerCertificate=True";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGrid();
                LoadCookies();
            }
        }

        protected void AddNewButton_Click(object sender, EventArgs e)
        {
            AddItemPanel.Visible = true; // Show the panel for adding new items
            AddNewButton.Visible = false; // Hide the "Add New Item" button
        }

        protected void AddItemButton_Click(object sender, EventArgs e)
        {
            string itemName = ItemName.Text;
            string itemModel = ItemModel.Text;
            string itemDescription = ItemDescription.Text;
            string itemCategory = ItemCategory.Text;
            int itemQuantity = int.Parse(ItemQuantity.Text);
            decimal itemPrice = decimal.Parse(ItemPrice.Text);
            DateTime lastUpdated = DateTime.Now;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO Main (ProductName, Model, Description, Category, Qty, Price, LastUpdated) " +
                               "VALUES (@ProductName, @Model, @Description, @Category, @Qty, @Price, @LastUpdated)";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@ProductName", itemName);
                    cmd.Parameters.AddWithValue("@Model", itemModel);
                    cmd.Parameters.AddWithValue("@Description", itemDescription);
                    cmd.Parameters.AddWithValue("@Category", itemCategory);
                    cmd.Parameters.AddWithValue("@Qty", itemQuantity);
                    cmd.Parameters.AddWithValue("@Price", itemPrice);
                    cmd.Parameters.AddWithValue("@LastUpdated", lastUpdated);

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }

            // Clear the input fields
            ClearInputFields();

            // Update the GridView
            BindGrid();

            // Hide the panel for adding new items and show the "Add New Item" button
            AddItemPanel.Visible = false;
            AddNewButton.Visible = true;
        }

        protected void CancelButton_Click(object sender, EventArgs e)
        {
            ClearInputFields();

            // Hide the panel for adding new items and show the "Add New Item" button
            AddItemPanel.Visible = false;
            AddNewButton.Visible = true;
        }

        protected void SearchCategoryButton_Click(object sender, EventArgs e)
        {
            string category = SearchCategory.Text;
            SetCookie("SearchCategory", category);
            SearchItems("Category", category);
        }

        protected void SearchModelButton_Click(object sender, EventArgs e)
        {
            string model = SearchModel.Text;
            SetCookie("SearchModel", model);
            SearchItems("Model", model);
        }

        private void SetCookie(string key, string value)
        {
            HttpCookie cookie = new HttpCookie(key, value);
            cookie.Expires = DateTime.Now.AddDays(30); // Set the cookie to expire in 30 days
            Response.Cookies.Add(cookie);
        }

        private void LoadCookies()
        {
            if (Request.Cookies["SearchCategory"] != null)
            {
                SearchCategory.Text = Request.Cookies["SearchCategory"].Value;
            }

            if (Request.Cookies["SearchModel"] != null)
            {
                SearchModel.Text = Request.Cookies["SearchModel"].Value;
            }
        }

        private void SearchItems(string field, string value)
        {
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = $"SELECT * FROM Main WHERE {field} = @Value";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@Value", value);

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }

            ItemsGridView.DataSource = dt;
            ItemsGridView.DataBind();
        }

        private void BindGrid()
        {
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "SELECT ProductName, Model, LastUpdated FROM Main ORDER BY LastUpdated DESC";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }

            ItemsGridView.DataSource = dt;
            ItemsGridView.DataBind();
        }

        private void ClearInputFields()
        {
            ItemName.Text = string.Empty;
            ItemModel.Text = string.Empty;
            ItemDescription.Text = string.Empty;
            ItemCategory.Text = string.Empty;
            ItemQuantity.Text = string.Empty;
            ItemPrice.Text = string.Empty;
        }

        protected void ItemsGridView_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridViewRow row = ItemsGridView.Rows[e.NewEditIndex];
            string productName = row.Cells[0].Text;
            string model = row.Cells[1].Text;
            Response.Redirect($"Edit_Item.aspx?ProductName={productName}&Model={model}");
        }

        protected void ItemsGridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            GridViewRow row = ItemsGridView.Rows[e.RowIndex];
            string productName = row.Cells[0].Text;
            string model = row.Cells[1].Text;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM Main WHERE ProductName = @ProductName AND Model = @Model";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@ProductName", productName);
                    cmd.Parameters.AddWithValue("@Model", model);

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }

            BindGrid();
        }
    }
}
