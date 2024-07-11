using System;
using System.Data.SqlClient;
using System.Web.UI.HtmlControls;

namespace Inventory_Management_System
{
    public partial class Home : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!User.Identity.IsAuthenticated)
            {
                Response.Redirect("Login_Page.aspx");
            }
            else if (!IsPostBack) // Only populate table on initial page load
            {
                PopulateProductTable(); // Populate product data on page load
            }
        }

        private void PopulateProductTable()
        {
            string connectionString = "Data Source=SAMARTH-PC;Initial Catalog=loginasp;Integrated Security=True;Encrypt=True;TrustServerCertificate=True";
            string query = "SELECT ProductName, Model, Description, Category, Qty, Price, LastUpdated FROM Main ORDER BY LastUpdated DESC";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    try
                    {
                        con.Open();
                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            HtmlTableRow row = new HtmlTableRow(); // Create a new HTML row

                     

                            HtmlTableCell cellProductName = new HtmlTableCell();
                            cellProductName.InnerText = reader["ProductName"].ToString();
                            row.Cells.Add(cellProductName);

                            HtmlTableCell cellModel = new HtmlTableCell();
                            cellModel.InnerText = reader["Model"].ToString();
                            row.Cells.Add(cellModel);

                            HtmlTableCell cellDescription = new HtmlTableCell();
                            cellDescription.InnerText = reader["Description"].ToString();
                            row.Cells.Add(cellDescription);

                            HtmlTableCell cellCategory = new HtmlTableCell();
                            cellCategory.InnerText = reader["Category"].ToString();
                            row.Cells.Add(cellCategory);

                            HtmlTableCell cellQty = new HtmlTableCell();
                            cellQty.InnerText = reader["Qty"].ToString();
                            row.Cells.Add(cellQty);

                            HtmlTableCell cellPrice = new HtmlTableCell();
                            cellPrice.InnerText = reader["Price"].ToString();
                            row.Cells.Add(cellPrice);

                            HtmlTableCell cellLastUpdated = new HtmlTableCell();
                            cellLastUpdated.InnerText = ((DateTime)reader["LastUpdated"]).ToString("yyyy-MM-dd HH:mm:ss");
                            row.Cells.Add(cellLastUpdated);

                            // Add the row to the productTable
                            productTable.Rows.Add(row);
                        }

                        reader.Close();
                    }
                    catch (Exception ex)
                    {
                        // Handle or log the exception
                        throw ex;
                    }
                    finally
                    {
                        con.Close();
                    }
                }
            }
        }

        protected void btnAddNewItem_Click(object sender, EventArgs e)
        {
            Response.Redirect("Add_Item.aspx");
        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            System.Web.Security.FormsAuthentication.SignOut();
            Response.Redirect("Login_Page.aspx");
        }
    }
}
