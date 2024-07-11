using System;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Web.UI;
using QRCoder;
using System.Web.Security;
using System.Web.Services;

namespace WebApplication2
{
    public partial class LoginPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string token = Guid.NewGuid().ToString();
                ViewState["token"] = token;
                StoreToken(token);
                string loginPageUrl = Request.Url.AbsoluteUri;
                GenerateQRCode(token);
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            AuthenticateUser(TextBox1.Text, TextBox2.Text);
        }

        private void GenerateQRCode(string token)
        {
            string localIpAddress = " 192.168.29.212"; // Replace with your local IP address
            string qrContent = $"http://{localIpAddress}:44382/CheckLogin.aspx?token={token}"; // Replace 'port' with your web server port

            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(qrContent, QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCode(qrCodeData);
            Bitmap qrCodeImage = qrCode.GetGraphic(10);

            using (MemoryStream ms = new MemoryStream())
            {
                qrCodeImage.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                byte[] qrCodeByte = ms.ToArray();
                string qrCodeBase64 = Convert.ToBase64String(qrCodeByte);
                string imageSrc = $"data:image/png;base64,{qrCodeBase64}";
                string script = $"<script>document.getElementById('qrCodeLogin').src = '{imageSrc}';</script>";
                Page.ClientScript.RegisterStartupScript(GetType(), "qrCodeLogin", script);
            }
        }


        private void StoreToken(string token)
        {
            using (SqlConnection con = new SqlConnection("Data Source=SAMARTH-PC;Initial Catalog=loginasp;Integrated Security=True;TrustServerCertificate=True"))
            {
                string query = "INSERT INTO LoginTokens (Token) VALUES (@token)";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@token", token);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        [WebMethod]
        public static void MarkLoginSuccessful(string token)
        {
            using (SqlConnection con = new SqlConnection("Data Source=SAMARTH-PC;Initial Catalog=loginasp;Integrated Security=True;TrustServerCertificate=True"))
            {
                string query = "UPDATE LoginTokens SET IsLoggedIn = 1 WHERE Token = @token";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@token", token);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }


        private void AuthenticateUser(string email, string password)
        {
            using (SqlConnection con = new SqlConnection("Data Source=SAMARTH-PC;Initial Catalog=loginasp;Integrated Security=True;TrustServerCertificate=True"))
            {
                string loginQuery = "SELECT COUNT(*) FROM RegLog WHERE EMAIL = @Email AND PASSWORD = @Password";
                SqlCommand cmd = new SqlCommand(loginQuery, con);
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@Password", password);

                con.Open();
                int count = (int)cmd.ExecuteScalar();
                con.Close();

                if (count > 0)
                {
                    FormsAuthentication.SetAuthCookie(email, false);
                    Response.Redirect("Home.aspx");
                }
                else
                {
                    Response.Write("<script>alert('Login Failed!');</script>");
                }
            }
        }
    }
}
