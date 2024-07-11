using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace StoreManagement
{
    public partial class ForgotPassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void SubmitButton_Click(object sender, EventArgs e)
        {
            string email = EmailTextBox.Text.Trim();
            string resetToken = Guid.NewGuid().ToString();

            // Save the reset token and timestamp in the database
            using (SqlConnection con = new SqlConnection("Data Source=SAMARTH-PC;Initial Catalog=loginasp;Integrated Security=True;TrustServerCertificate=True"))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("UPDATE RegLog SET ResetToken = @ResetToken, ResetTokenExpiry = @Expiry WHERE EMAIL = @email", con);
                cmd.Parameters.AddWithValue("@ResetToken", resetToken);
                cmd.Parameters.AddWithValue("@Expiry", DateTime.Now.AddHours(1)); // Token expires in 1 hour
                cmd.Parameters.AddWithValue("@email", email);
                int rowsAffected = cmd.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    string result = SendResetEmail(email, resetToken);
                    MessageLabel.Text = "A password reset link has been sent to your email address.";
                }
                else
                {
                    MessageLabel.Text = "Email address not found.";
                }
            }
        }

        private string SendResetEmail(string email, string resetToken)
        {
            try
            {
                string resetLink = "https://localhost:44382/ResetPassword?token=" + resetToken;
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress("your-email@gmail.com");
                mail.To.Add(email);
                mail.Subject = "Password Reset";
                mail.Body = "Click this link to reset your password: " + resetLink;

                SmtpClient client = new SmtpClient("smtp.gmail.com");
                client.Port = 587; // Use port 465 for SSL
                client.Credentials = new System.Net.NetworkCredential("bhatiuser004@gmail.com", "mswedgufatgumyds");
                client.EnableSsl = true;

                client.Send(mail);
                // Console.WriteLine("executed");
                return "Mail sent successfully.";
            }
            catch (Exception ex)
            {
                // Handle the exception
                MessageLabel.Text = "Error sending email: " + ex.Message;
                Console.WriteLine(ex.Message);
                return ex.Message;
            }
        }
    }
}        