using SpeechLib;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace UniversityChatBot
{
    public partial class bot : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDb)\MSSQLLocalDB;Initial Catalog=ChatbotDB;Integrated Security=True");
        System.Timers.Timer t = new System.Timers.Timer();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["action"] != null)
            {
                voice.Attributes.CssStyle.Add("visibility", "visible");
                Label5.Text = Session["action"].ToString();
                SpVoice voice1;
                voice1 = new SpVoice();
                voice1.Volume = 100;  // 0...100
                voice1.Rate = -4;     // -10...10
                string reply = Request.QueryString["reply"];
                string link = Request.QueryString["link"];
                if (reply != null)
                {
                    string sel = "select * from Answer";
                    SqlDataAdapter da = new SqlDataAdapter(sel, con);
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    int count = ds.Tables[0].Rows.Count;
                    string ques = ds.Tables[0].Rows[0][1].ToString();
                    string ans = ds.Tables[0].Rows[0][2].ToString();
                    string url = ds.Tables[0].Rows[0][3].ToString();
                    if (count > 0)
                    {
                        Button3.Visible = true;
                        speak_text.Text = ques;
                        Label4.Text = ans;
                        if (url != "")
                        {
                            HyperLink2.Visible = true;
                            HyperLink2.NavigateUrl = Convert.ToString(url);
                        }
                    }
                    else
                    {
                        Button3.Visible = false;
                    }


                    if (reply != "none")
                    {

                        voice1.Speak(reply);
                        Label4.Text = reply;
                        // voice.Visible = true;
                        speak_text.Text = "";

                        //ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "PageLoad()", true);
                        this.HiddenField1.Value = "yes";

                    }

                }
                else
                {
                    voice.Attributes.CssStyle.Add("visibility", "hidden");
                    Label5.Text = "";
                }
            }
            else
            {
                voice.Attributes.CssStyle.Add("visibility", "hidden");
                Label5.Text = "";
            }

        }
        protected void Timer1_Tick(object sender, EventArgs e)
        {
            Label1.Text = DateTime.Now.ToString();
        }

        protected void txtuser_TextChanged(object sender, EventArgs e)
        {
            Session["Question"] = txtuser.Text;
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            HyperLink1.Visible = false;


            SpVoice voice;
            voice = new SpVoice();
            voice.Volume = 100;  // 0...100
            voice.Rate = -4;     // -10...10

            String s = Convert.ToString(txtuser.Text).Trim();  //white spaces removed
            String reply;
            s = s.ToLower();
            string ans = "no";
            if (s.Equals("hi") || s.Equals("hii") || s.Equals("hiii") || s.Equals("hiiii") || s.Equals("hhii") || s.Equals("hello") || s.Equals("helloo") || s.Equals("helo") || s.Equals("heello") || s.Equals("helllo") || s.Equals("helloooo"))
            {
                ans = "OK";
                SqlDataAdapter da = new SqlDataAdapter("Select top 1 * from generalreply order by NEWID()", con);
                DataSet ds = new DataSet();
                da.Fill(ds);
                reply = Convert.ToString(ds.Tables[0].Rows[0][1]);
                lblbot.Text = reply;
                voice.Speak(reply);
            }
            else if (s.Contains("i") && s.Contains("fine") || s.Contains("i") && s.Contains("good"))
            {
                ans = "OK";
                SqlDataAdapter da = new SqlDataAdapter("Select top 1 * from addqueries order by NEWID()", con);
                DataSet ds = new DataSet();
                da.Fill(ds);
                reply = Convert.ToString(ds.Tables[0].Rows[0][1]);
                lblbot.Text = reply;
                voice.Speak(reply);
            }
            //if (s.Contains("you") && s.Contains("how") || s.Contains("you") && s.Contains("how") && s.Contains("do"))
            //{
            //    ans = "OK";
            //    lblbot.Text = "I am Fine.. How Are You ?";
            //    voice.Speak("I am Fine.. How Are You ?");
            //}
            else if (s.Contains("timetable"))
            {
                ans = "OK";
                String s2, k1, k2, k3, rep = s;   // s is not a string
                int count;
                SqlDataAdapter da4 = new SqlDataAdapter("Select * from timetable where keywords = '2'", con);
                DataSet ds4 = new DataSet();
                da4.Fill(ds4);
                count = Convert.ToInt32(ds4.Tables[0].Rows.Count);
                for (int i = 0; i < count; i++)
                {
                    k1 = Convert.ToString(ds4.Tables[0].Rows[i][2]).ToLower();
                    k2 = Convert.ToString(ds4.Tables[0].Rows[i][3]).ToLower();
                    k3 = Convert.ToString(ds4.Tables[0].Rows[i][4]).ToLower();
                    if (s.Contains(k1) && s.Contains(k2))
                    {
                        rep = Convert.ToString(ds4.Tables[0].Rows[i][7]);
                        HyperLink1.Visible = true;
                        HyperLink1.NavigateUrl = Convert.ToString(ds4.Tables[0].Rows[i][8]);
                        voice.Speak(rep);
                    }

                    //-------------------- continue from here


                }
                if (rep == s)
                {
                    da4 = new SqlDataAdapter("Select * from timetable where keywords = '2' order by newid()", con);
                    ds4 = new DataSet();
                    da4.Fill(ds4);
                    rep = Convert.ToString(ds4.Tables[0].Rows[0][7]);
                    HyperLink1.Visible = true;
                    HyperLink1.NavigateUrl = Convert.ToString(ds4.Tables[0].Rows[0][8]);
                    voice.Speak(rep);
                }

                lblbot.Text = rep;

            }
            else if (s.Contains("question papers") || s.Contains("question paper"))
            {
                ans = "OK";
                String s2, k1, k2, rep = "s";
                int count;
                SqlDataAdapter da3 = new SqlDataAdapter("Select * from QuestionPapers where keywords = '2'", con);
                DataSet ds3 = new DataSet();
                da3.Fill(ds3);
                count = Convert.ToInt32(ds3.Tables[0].Rows.Count);
                for (int i = 0; i < count; i++)
                {
                    k1 = Convert.ToString(ds3.Tables[0].Rows[i][3]).ToLower();    //reviews needed
                    k2 = Convert.ToString(ds3.Tables[0].Rows[i][4]).ToLower();
                    if (s.Contains(k1) && s.Contains(k2))
                    {
                        rep = Convert.ToString(ds3.Tables[0].Rows[i][10]);
                        HyperLink1.Visible = true;
                        HyperLink1.NavigateUrl = Convert.ToString(ds3.Tables[0].Rows[i][11]);
                        voice.Speak(rep);
                    }
                }
                if (rep == "s")
                {
                    da3 = new SqlDataAdapter("Select * from QuestionPapers where keywords = '2' order by newid()", con);
                    ds3 = new DataSet();
                    da3.Fill(ds3);
                    rep = Convert.ToString(ds3.Tables[0].Rows[0][7]);
                    HyperLink1.Visible = true;
                    HyperLink1.NavigateUrl = Convert.ToString(ds3.Tables[0].Rows[0][8]);
                    voice.Speak(rep);
                }
                lblbot.Text = rep;
            }
            else if (s.Contains("reference book"))
            {
                ans = "OK";
                String s2, k1, k2, rep = "s";
                int count;
                SqlDataAdapter da3 = new SqlDataAdapter("Select * from ReferenceBook where keywords = '2'", con);
                DataSet ds3 = new DataSet();
                da3.Fill(ds3);
                count = Convert.ToInt32(ds3.Tables[0].Rows.Count);
                for (int i = 0; i < count; i++)
                {
                    k1 = Convert.ToString(ds3.Tables[0].Rows[i][2]).ToLower();
                    k2 = Convert.ToString(ds3.Tables[0].Rows[i][3]).ToLower();
                    //k3 = Convert.ToString(ds3.Tables[0].Rows[i][3]).ToLower();
                    if (s.Contains(k1) && s.Contains(k2))
                    {
                        rep = Convert.ToString(ds3.Tables[0].Rows[i][7]); ;
                        voice.Speak(rep);
                        break;
                    }

                }
                if (rep == "s")
                {
                    da3 = new SqlDataAdapter("Select * from ReferenceBook where keywords = '2' order by newid()", con);
                    ds3 = new DataSet();
                    da3.Fill(ds3);
                    rep = Convert.ToString(ds3.Tables[0].Rows[0][7]); ;
                    voice.Speak(rep);

                }

                lblbot.Text = rep;

            }
            else if (s.Contains("subjects") || s.Contains("subject"))
            {
                ans = "OK";
                String k1, k2, k3, k4, rep = "s";
                int count;
                SqlDataAdapter da3 = new SqlDataAdapter("Select * from Subjects where keywords = '2'", con);
                DataSet ds3 = new DataSet();
                da3.Fill(ds3);
                count = Convert.ToInt32(ds3.Tables[0].Rows.Count);
                for (int i = 0; i < count; i++)
                {
                    k1 = Convert.ToString(ds3.Tables[0].Rows[i][2]).ToLower();
                    k2 = Convert.ToString(ds3.Tables[0].Rows[i][3]).ToLower();
                    k3 = Convert.ToString(ds3.Tables[0].Rows[i][4]).ToLower();
                    k4 = Convert.ToString(ds3.Tables[0].Rows[i][5]).ToLower();
                    //k5 = Convert.ToString(ds3.Tables[0].Rows[i][5]).ToLower();
                    if (s.Contains(k1) && (s.Contains(k2) || s.Contains(k4)) && s.Contains(k3))
                    {
                        rep = Convert.ToString(ds3.Tables[0].Rows[i][9]);
                        voice.Speak(rep);
                        break;

                    }

                }
                if (rep == "s")
                {
                    da3 = new SqlDataAdapter("Select * from Subjects where keywords = '2' order by newid()", con);
                    ds3 = new DataSet();
                    da3.Fill(ds3);
                    rep = Convert.ToString(ds3.Tables[0].Rows[0][9]);
                    voice.Speak(rep);
                }
                lblbot.Text = rep;

            }
            else if (s.Contains("cabin"))
            {
                ans = "OK";
                String s2, k1, k2, rep = "s";
                int count;
                SqlDataAdapter da3 = new SqlDataAdapter("Select * from Cabin where keywords = '2'", con);
                DataSet ds3 = new DataSet();
                da3.Fill(ds3);
                count = Convert.ToInt32(ds3.Tables[0].Rows.Count);
                for (int i = 0; i < count; i++)
                {
                    k1 = Convert.ToString(ds3.Tables[0].Rows[i][1]).ToLower();
                    k2 = Convert.ToString(ds3.Tables[0].Rows[i][2]).ToLower();
                    if (s.Contains(k1) && s.Contains(k2))
                    {
                        rep = Convert.ToString(ds3.Tables[0].Rows[i][6]); ;
                        voice.Speak(rep);
                    }
                }
                if (rep == "s")
                {
                    da3 = new SqlDataAdapter("Select * from Cabin where keywords = '2' order by newid()", con);
                    ds3 = new DataSet();
                    da3.Fill(ds3);
                    rep = Convert.ToString(ds3.Tables[0].Rows[0][6]); ;
                    voice.Speak(rep);
                }
                lblbot.Text = rep;
            }
            else if (s.Contains("contact") || s.Contains("mobile"))
            {
                ans = "OK";
                String s2, k1, k2, k3, k4, k5, k6, k7, rep = "s";
                int count;
                SqlDataAdapter da3 = new SqlDataAdapter("Select * from Contact where keywords = '3'", con);
                DataSet ds3 = new DataSet();
                da3.Fill(ds3);
                count = Convert.ToInt32(ds3.Tables[0].Rows.Count);
                for (int i = 0; i < count; i++)
                {
                    k1 = Convert.ToString(ds3.Tables[0].Rows[i][2]).ToLower();
                    k2 = Convert.ToString(ds3.Tables[0].Rows[i][3]).ToLower();
                    k3 = Convert.ToString(ds3.Tables[0].Rows[i][4]).ToLower();
                    k4 = Convert.ToString(ds3.Tables[0].Rows[i][5]).ToLower();
                    k5 = Convert.ToString(ds3.Tables[0].Rows[i][6]).ToLower();
                    // k6 = Convert.ToString(ds3.Tables[0].Rows[i][7]).ToLower();
                    // k7 = Convert.ToString(ds3.Tables[0].Rows[i][8]).ToLower();
                    if (s.Contains(k1) && s.Contains(k2) && s.Contains(k3) && s.Contains(k4) && s.Contains(k5))
                    {
                        rep = Convert.ToString(ds3.Tables[0].Rows[i][10]); ;
                        voice.Speak(rep);
                        break;
                    }
                }
                if (rep == "s")
                {
                    da3 = new SqlDataAdapter("Select * from Contact where keywords = '3' order by newid()", con);
                    ds3 = new DataSet();
                    da3.Fill(ds3);
                    rep = Convert.ToString(ds3.Tables[0].Rows[0][10]); ;
                    voice.Speak(rep);
                }
                lblbot.Text = rep;
            }
            else if (s.Contains("specialization") || s.Contains("specialization"))
            {
                ans = "OK";
                String s2, k1, k2, rep = "s";
                int count;
                SqlDataAdapter da3 = new SqlDataAdapter("Select * from specialization where keywords = '2'", con);
                DataSet ds3 = new DataSet();
                da3.Fill(ds3);
                count = Convert.ToInt32(ds3.Tables[0].Rows.Count);
                for (int i = 0; i < count; i++)
                {
                    k1 = Convert.ToString(ds3.Tables[0].Rows[i][2]).ToLower();
                    k2 = Convert.ToString(ds3.Tables[0].Rows[i][3]).ToLower();
                    if (s.Contains(k1) && s.Contains(k2))
                    {
                        rep = Convert.ToString(ds3.Tables[0].Rows[i][7]); ;
                        voice.Speak(rep);
                    }
                }
                if (rep == "s")
                {
                    da3 = new SqlDataAdapter("Select * from specialization where keywords = '2' order by newid()", con);
                    ds3 = new DataSet();
                    da3.Fill(ds3);
                    rep = Convert.ToString(ds3.Tables[0].Rows[0][7]); ;
                    voice.Speak(rep);
                }
                lblbot.Text = rep;
            }
            else if (s.Contains("faculaties"))
            {
                ans = "OK";
                String s2, k1, k2, k3, rep = "s";
                int count;
                SqlDataAdapter da3 = new SqlDataAdapter("Select * from faculaties where keywords = '3'", con);
                DataSet ds3 = new DataSet();
                da3.Fill(ds3);
                count = Convert.ToInt32(ds3.Tables[0].Rows.Count);
                for (int i = 0; i < count; i++)
                {
                    k1 = Convert.ToString(ds3.Tables[0].Rows[i][2]).ToLower();
                    k2 = Convert.ToString(ds3.Tables[0].Rows[i][3]).ToLower();
                    k3 = Convert.ToString(ds3.Tables[0].Rows[i][4]).ToLower();
                    if (s.Contains(k1) && s.Contains(k2) && s.Contains(k3))
                    {
                        rep = Convert.ToString(ds3.Tables[0].Rows[i][7]); ;
                        voice.Speak(rep);
                    }
                }
                if (rep == "s")
                {
                    da3 = new SqlDataAdapter("Select * from faculaties where keywords = '3' order by newid()", con);
                    ds3 = new DataSet();
                    da3.Fill(ds3);
                    rep = Convert.ToString(ds3.Tables[0].Rows[0][7]); ;
                    voice.Speak(rep);
                }
                lblbot.Text = rep;
            }
            else if (s.Contains("classroom"))
            {
                ans = "OK";
                String s2, k1, k2, rep = "s";
                int count;
                SqlDataAdapter da3 = new SqlDataAdapter("Select * from classroom where keywords = '2'", con);
                DataSet ds3 = new DataSet();
                da3.Fill(ds3);
                count = Convert.ToInt32(ds3.Tables[0].Rows.Count);
                for (int i = 0; i < count; i++)
                {
                    k1 = Convert.ToString(ds3.Tables[0].Rows[i][2]).ToLower();
                    k2 = Convert.ToString(ds3.Tables[0].Rows[i][3]).ToLower();
                    if (s.Contains(k1) && s.Contains(k2))
                    {
                        rep = Convert.ToString(ds3.Tables[0].Rows[i][7]); ;
                        voice.Speak(rep);
                    }
                }
                if (rep == "s")
                {
                    da3 = new SqlDataAdapter("Select * from classroom where keywords = '2' order by newid()", con);
                    ds3 = new DataSet();
                    da3.Fill(ds3);
                    rep = Convert.ToString(ds3.Tables[0].Rows[0][7]); ;
                    voice.Speak(rep);
                }
                lblbot.Text = rep;
            }
            else if (s.Contains("how"))
            {
                ans = "OK";
                String s2, query1, query2, query3, rep = "s";
                int count;
                SqlDataAdapter da3 = new SqlDataAdapter("Select * from doublereply where keywords = '3'", con);
                DataSet ds3 = new DataSet();
                da3.Fill(ds3);
                count = Convert.ToInt32(ds3.Tables[0].Rows.Count);
                for (int i = 0; i < count; i++)
                {
                    query1 = Convert.ToString(ds3.Tables[0].Rows[i][2]).ToLower();
                    query2 = Convert.ToString(ds3.Tables[0].Rows[i][3]).ToLower();
                    query3 = Convert.ToString(ds3.Tables[0].Rows[i][4]).ToLower();
                    if (s.Contains(query1) && s.Contains(query2) && s.Contains(query3))
                    {
                        rep = Convert.ToString(ds3.Tables[0].Rows[i][7]); ;
                        voice.Speak(rep);
                    }
                }
                if (rep == "s")
                {
                    da3 = new SqlDataAdapter("Select * from doublereply where keywords = '3' order by newid()", con);
                    ds3 = new DataSet();
                    da3.Fill(ds3);
                    rep = Convert.ToString(ds3.Tables[0].Rows[0][7]); ;
                    voice.Speak(rep);
                }
                lblbot.Text = rep;
            }
            else if (s.Contains("what"))
            {
                ans = "OK";
                String s2, query1, query2, query3, rep = "s";
                int count;
                SqlDataAdapter da3 = new SqlDataAdapter("Select * from whatdoublereply where keywords = '3'", con);
                DataSet ds3 = new DataSet();
                da3.Fill(ds3);
                count = Convert.ToInt32(ds3.Tables[0].Rows.Count);
                for (int i = 0; i < count; i++)
                {
                    query1 = Convert.ToString(ds3.Tables[0].Rows[i][3]).ToLower();
                    query2 = Convert.ToString(ds3.Tables[0].Rows[i][4]).ToLower();
                    query3 = Convert.ToString(ds3.Tables[0].Rows[i][5]).ToLower();
                    if (s.Contains(query1) && s.Contains(query2) && s.Contains(query3))
                    {
                        rep = Convert.ToString(ds3.Tables[0].Rows[i][8]); ;
                        voice.Speak(rep);
                    }
                }
                if (rep == "s")
                {
                    da3 = new SqlDataAdapter("Select * from whatdoublereply where keywords = '3' order by newid()", con);
                    ds3 = new DataSet();
                    da3.Fill(ds3);
                    rep = Convert.ToString(ds3.Tables[0].Rows[0][8]); ;
                    voice.Speak(rep);
                }
                lblbot.Text = rep;
            }
            else if (s.Contains("where"))
            {
                ans = "OK";
                String s2, query1, query2, query3, rep = "s";
                int count;
                SqlDataAdapter da3 = new SqlDataAdapter("Select * from wheredoublereply where keywords = '3'", con);
                DataSet ds3 = new DataSet();
                da3.Fill(ds3);
                count = Convert.ToInt32(ds3.Tables[0].Rows.Count);
                for (int i = 0; i < count; i++)
                {
                    query1 = Convert.ToString(ds3.Tables[0].Rows[i][2]).ToLower();
                    query2 = Convert.ToString(ds3.Tables[0].Rows[i][3]).ToLower();
                    query3 = Convert.ToString(ds3.Tables[0].Rows[i][4]).ToLower();
                    if (s.Contains(query1) && s.Contains(query2) && s.Contains(query3))
                    {
                        rep = Convert.ToString(ds3.Tables[0].Rows[i][7]); ;
                        voice.Speak(rep);
                    }
                }
                if (rep == "s")
                {
                    da3 = new SqlDataAdapter("Select * from wheredoublereply where keywords = '3' order by newid()", con);
                    ds3 = new DataSet();
                    da3.Fill(ds3);
                    rep = Convert.ToString(ds3.Tables[0].Rows[0][7]); ;
                    voice.Speak(rep);
                }
                lblbot.Text = rep;
            }
            else if (s.Contains("who"))
            {
                ans = "OK";
                String s2, query1, query2, query3, rep = "s";
                int count;
                SqlDataAdapter da3 = new SqlDataAdapter("Select * from whodoublereply where keywords = '3'", con);
                DataSet ds3 = new DataSet();
                da3.Fill(ds3);
                count = Convert.ToInt32(ds3.Tables[0].Rows.Count);
                for (int i = 0; i < count; i++)
                {
                    query1 = Convert.ToString(ds3.Tables[0].Rows[i][2]).ToLower();
                    query2 = Convert.ToString(ds3.Tables[0].Rows[i][3]).ToLower();
                    query3 = Convert.ToString(ds3.Tables[0].Rows[i][4]).ToLower();
                    if (s.Contains(query1) && s.Contains(query2) && s.Contains(query3))
                    {
                        rep = Convert.ToString(ds3.Tables[0].Rows[i][7]); ;
                        voice.Speak(rep);
                    }
                }
                if (rep == "s")
                {
                    da3 = new SqlDataAdapter("Select * from whodoublereply where keywords = '3' order by newid()", con);
                    ds3 = new DataSet();
                    da3.Fill(ds3);
                    rep = Convert.ToString(ds3.Tables[0].Rows[0][7]); ;
                    voice.Speak(rep);
                }
                lblbot.Text = rep;
            }
            else if (ans != "OK")
            {
                string rep = Convert.ToString("No related Answer Found"); ;
                voice.Speak(rep);
                lblbot.Text = rep;
            }

            SqlCommand cmd = new SqlCommand("Insert into Ques(Question,Date,Time) values('" + txtuser.Text + "','" + DateTime.Now.ToShortDateString() + "','" + DateTime.Now.ToShortTimeString() + "')", con);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();

            speak_text.Text = null;

            txtuser.Text = "";
        }
        protected void ImageButton3_Click(object sender, ImageClickEventArgs e)
        {

        }
        protected void Button2_Click(object sender, EventArgs e)
        {
            //Session["Question"] = txtuser.Text;
            Session["Answer"] = lblbot.Text;
            Response.Redirect("Invalid.aspx");
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            Session["Question"] = speak_text.Text;
            Session["Answer"] = Label4.Text;
            Response.Redirect("Invalid.aspx");
        }
    }
}