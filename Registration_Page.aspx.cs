using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.ComponentModel;
using QRCoder;
using System.Drawing;
using System.IO;

namespace Inventory_Management_System
{
    public partial class Registration_Page : System.Web.UI.Page
    {
        string gender;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string registrationPageUrl = Request.Url.AbsoluteUri;
                GenerateQRCode(registrationPageUrl, "qrCodeRegistration");
            }
        }

        protected void btnReg_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection("Data Source=SAMARTH-PC;Initial Catalog=loginasp;Integrated Security=True;TrustServerCertificate=True"))
            {
                string query = @"INSERT INTO [dbo].[RegLog]
                                ([NAME], [PHONE], [EMAIL], [GENDER], [PASSWORD])
                                VALUES (@Name, @Phone, @Email, @Gender, @Password)";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@Name", txtName.Text);
                    cmd.Parameters.AddWithValue("@Phone", txtPhone.Text);
                    cmd.Parameters.AddWithValue("@Email", txtEmail.Text);
                    cmd.Parameters.AddWithValue("@Gender", gender);
                    cmd.Parameters.AddWithValue("@Password", txtPass.Text);

                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }

            Response.Write("<script>alert('User Registration Successful!');</script>");
            Response.Redirect("Login_Page.aspx");
        }

        protected void RadioButton1_CheckedChanged(object sender, EventArgs e)
        {
            gender = "Male";
        }

        protected void RadioButton2_CheckedChanged(object sender, EventArgs e)
        {
            gender = "Female";
        }

        protected void RadioButton3_CheckedChanged(object sender, EventArgs e)
        {
            gender = "Others";
        }
        private void GenerateQRCode(string content, string controlId)
        {
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(content, QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCode(qrCodeData);
            Bitmap qrCodeImage = qrCode.GetGraphic(10);

            // Convert Bitmap to byte array
            using (MemoryStream ms = new MemoryStream())
            {
                qrCodeImage.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                byte[] qrCodeByte = ms.ToArray();

                // Convert byte array to base64 string
                string qrCodeBase64 = Convert.ToBase64String(qrCodeByte);
                string imageSrc = $"data:image/png;base64,{qrCodeBase64}";

                // Inject the QR code image into the HTML
                string script = $"<script>document.getElementById('{controlId}').src = '{imageSrc}';</script>";
                Page.ClientScript.RegisterStartupScript(GetType(), controlId, script);
            }
        }
    }
}