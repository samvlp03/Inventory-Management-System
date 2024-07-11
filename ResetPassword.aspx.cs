using StoreManagement;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace StoreManagement
{
    public partial class ResetPassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string token = Request.QueryString["token"];
                if (string.IsNullOrEmpty(token))
                {
                    Response.Redirect("Login_Page.aspx");
                }
            }
        }

        protected void ResetButton_Click(object sender, EventArgs e)
        {
            string newPassword = NewPasswordTextBox.Text.Trim();
            string token = Request.QueryString["token"];

            using (SqlConnection con = new SqlConnection("Data Source=SAMARTH-PC;Initial Catalog=loginasp;Integrated Security=True;TrustServerCertificate=True"))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT EMAIL FROM RegLog WHERE ResetToken = @ResetToken AND ResetTokenExpiry > @CurrentTime", con);
                cmd.Parameters.AddWithValue("@ResetToken", token);
                cmd.Parameters.AddWithValue("@CurrentTime", DateTime.Now);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    string email = reader["email"].ToString();
                    reader.Close();

                    cmd = new SqlCommand("UPDATE RegLog SET PASSWORD = @password, ResetToken = NULL, ResetTokenExpiry = NULL WHERE EMAIL = @email", con);
                    cmd.Parameters.AddWithValue("@Password", newPassword); // Hash the password in real implementation
                    cmd.Parameters.AddWithValue("@email", email);
                    cmd.ExecuteNonQuery();

                    MessageLabel.Text = "Your password has been reset successfully.";
                    Response.Redirect("Login_page.aspx");
                }
                else
                {
                    MessageLabel.Text = "Invalid or expired token.";
                }
            }
        }
    }
}
