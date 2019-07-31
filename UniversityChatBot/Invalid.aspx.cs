using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace UniversityChatBot
{
    public partial class Invalid : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDb)\MSSQLLocalDB;Initial Catalog=ChatbotDB;Integrated Security=True");

        protected void Page_Load(object sender, EventArgs e)
        {
            Label3.Text = Session["Question"].ToString();
            Label5.Text = Session["Answer"].ToString();

        }
        protected void Button2_Click(object sender, EventArgs e)
        {
            Response.Redirect("bot.aspx");
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("insert into Invalid Values ('" + Label3.Text + "','" + Label5.Text + "','" + DateTime.Now.ToShortDateString() + "')", con);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            Response.Redirect("bot.aspx");
        }
    }
    
}