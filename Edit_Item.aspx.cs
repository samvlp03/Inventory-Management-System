using System;
using System.Data.SqlClient;

namespace Learning_the_Basics
{
    public partial class Edit_Item : System.Web.UI.Page
    {
        private static readonly string connectionString = "Data Source=SAMARTH-PC;Initial Catalog=loginasp;Integrated Security=True;TrustServerCertificate=True";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string productName = Request.QueryString["ProductName"];
                LoadItemDetails(productName);
            }
        }

        protected void SaveChangesButton_Click(object sender, EventArgs e)
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
                string query = "UPDATE Main SET Model = @Model, Description = @Description, Category = @Category, Qty = @Qty, Price = @Price, LastUpdated = @LastUpdated WHERE ProductName = @ProductName";
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

            Response.Redirect("Add_Item.aspx");
        }

        private void LoadItemDetails(string productName)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Main WHERE ProductName = @ProductName";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@ProductName", productName);

                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        ItemName.Text = reader["ProductName"].ToString();
                        ItemModel.Text = reader["Model"].ToString();
                        ItemDescription.Text = reader["Description"].ToString();
                        ItemCategory.Text = reader["Category"].ToString();
                        ItemQuantity.Text = reader["Qty"].ToString();
                        ItemPrice.Text = reader["Price"].ToString();
                    }
                    con.Close();
                }
            }
        }
    }
}
