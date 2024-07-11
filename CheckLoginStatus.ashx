using System;
using System.Data.SqlClient;
using System.Web;

public class CheckLoginStatus : IHttpHandler
{
    public void ProcessRequest(HttpContext context)
    {
        string token = context.Request.QueryString["token"];
        bool isLoggedIn = CheckLoginStatus(token);
        context.Response.ContentType = "application/json";
        context.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(new { isLoggedIn = isLoggedIn }));
    }

    private bool CheckLoginStatus(string token)
    {
        using (SqlConnection con = new SqlConnection("Data Source=SAMARTH-PC;Initial Catalog=loginasp;Integrated Security=True;TrustServerCertificate=True"))
        {
            string query = "SELECT IsLoggedIn FROM LoginTokens WHERE Token = @token";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@token", token);
            
            con.Open();
            bool isLoggedIn = (bool)cmd.ExecuteScalar();
            con.Close();
            
            return isLoggedIn;
        }
    }

    public bool IsReusable => false;
}
