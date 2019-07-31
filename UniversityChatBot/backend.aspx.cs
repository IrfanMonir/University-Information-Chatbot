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
    public partial class backend : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDb)\MSSQLLocalDB;Initial Catalog=ChatbotDB;Integrated Security=True");

        protected void Page_Load(object sender, EventArgs e)
        {
            string sr = Request.QueryString["type"];
            string rep = "";
            sr = sr.ToLower();
            string s = sr.Trim('\'');
            string ans = "no";
            if (s != "")
            {
                SqlCommand cmd = new SqlCommand("Insert into Ques(Question,Date,Time) values('" + s + "','" + DateTime.Now.ToShortDateString() + "','" + DateTime.Now.ToShortTimeString() + "')", con);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();


                Session["action"] = "added";
                if (s.Equals("hi") || s.Equals("hii") || s.Equals("hiii") || s.Equals("hiiii") || s.Equals("hhii") || s.Equals("hello") || s.Equals("helloo") || s.Equals("helo") || s.Equals("heello") || s.Equals("helllo") || s.Equals("helloooo"))
                {
                    ans = "OK";
                    SqlDataAdapter da = new SqlDataAdapter("Select top 1 * from generalreply order by NEWID()", con);
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    rep = Convert.ToString(ds.Tables[0].Rows[0][1]);
                    string del = "delete from Answer";
                    SqlCommand dcmd = new SqlCommand(del, con);
                    con.Open();
                    dcmd.ExecuteNonQuery();
                    con.Close();
                    SqlCommand cmd1 = new SqlCommand("Insert into Answer(Question,Answer) values('" + s + "','" + rep + "')", con);
                    con.Open();
                    cmd1.ExecuteNonQuery();
                    con.Close();
                    Response.Redirect("bot.aspx?reply=" + rep + "");
                }

                else if (s.Contains("i") && s.Contains("fine") || s.Contains("i") && s.Contains("good"))
                {
                    ans = "OK";
                    SqlDataAdapter da = new SqlDataAdapter("Select top 1 * from addqueries order by NEWID()", con);
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    rep = Convert.ToString(ds.Tables[0].Rows[0][1]);
                    string del = "delete from Answer";
                    SqlCommand dcmd = new SqlCommand(del, con);
                    con.Open();
                    dcmd.ExecuteNonQuery();
                    con.Close();
                    SqlCommand cmd1 = new SqlCommand("Insert into Answer(Question,Answer) values('" + s + "','" + rep + "')", con);
                    con.Open();
                    cmd1.ExecuteNonQuery();
                    con.Close();
                    Response.Redirect("bot.aspx?reply=" + rep + "");

                }
                //if (s.Contains("you") && s.Contains("how") || s.Contains("you") && s.Contains("how") && s.Contains("do"))
                //{
                //    ans = "OK";
                //    rep = "I am Fine..How Are You ?";
                //    string del = "delete from Answer";
                //    SqlCommand dcmd = new SqlCommand(del, con);
                //    con.Open();
                //    dcmd.ExecuteNonQuery();
                //    con.Close();
                //    SqlCommand cmd1 = new SqlCommand("Insert into Answer(Question,Answer) values('" + s + "','" + rep + "')", con);
                //    con.Open();
                //    cmd1.ExecuteNonQuery();
                //    con.Close();
                //    Response.Redirect("bot.aspx?reply=" + rep + "");
                //}
                else if (s.Contains("timetable") || s.Contains("time"))
                {
                    ans = "OK";
                    String s2, k1, k2, k3, rep1 = "s", link = "";
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
                        if (s.Contains(k1) && s.Contains(k2) && s.Contains(k3))
                        {
                            rep1 = Convert.ToString(ds4.Tables[0].Rows[i][7]);
                            rep = rep1;
                            link = Convert.ToString(ds4.Tables[0].Rows[i][8]);
                            string del = "delete from Answer";
                            SqlCommand dcmd = new SqlCommand(del, con);
                            con.Open();
                            dcmd.ExecuteNonQuery();
                            con.Close();
                            SqlCommand cmd1 = new SqlCommand("Insert into Answer(Question,Answer,Link) values('" + s + "','" + rep + "','" + link + "')", con);
                            con.Open();
                            cmd1.ExecuteNonQuery();
                            con.Close();
                            Response.Redirect("bot.aspx?reply=" + rep + "&link=" + link + "");

                        }
                    }
                    if (rep1 == "s")
                    {
                        da4 = new SqlDataAdapter("Select * from timetable where keywords = '2' order by newid()", con);
                        ds4 = new DataSet();
                        da4.Fill(ds4);
                        rep = Convert.ToString(ds4.Tables[0].Rows[0][7]);
                        link = Convert.ToString(ds4.Tables[0].Rows[0][8]);
                        string del = "delete from Answer";
                        SqlCommand dcmd = new SqlCommand(del, con);
                        con.Open();
                        dcmd.ExecuteNonQuery();
                        con.Close();
                        SqlCommand cmd1 = new SqlCommand("Insert into Answer(Question,Answer,Link) values('" + s + "','" + rep + "','" + link + "')", con);
                        con.Open();
                        cmd1.ExecuteNonQuery();
                        con.Close();
                        Response.Redirect("bot.aspx?reply=" + rep + "&link=" + link + "");
                    }
                }
                else if (s.Contains("question papers") || s.Contains("question paper"))
                {
                    ans = "OK";
                    String s2, k1, k2, rep1 = "s", link = "";
                    int count;
                    SqlDataAdapter da3 = new SqlDataAdapter("Select * from QuestionPapers where keywords = '2'", con);
                    DataSet ds3 = new DataSet();
                    da3.Fill(ds3);
                    count = Convert.ToInt32(ds3.Tables[0].Rows.Count);
                    for (int i = 0; i < count; i++)
                    {
                        k1 = Convert.ToString(ds3.Tables[0].Rows[i][1]).ToLower();
                        k2 = Convert.ToString(ds3.Tables[0].Rows[i][2]).ToLower();
                        if (s.Contains(k1) && s.Contains(k2))
                        {
                            rep1 = Convert.ToString(ds3.Tables[0].Rows[i][6]);
                            rep = rep1;
                            link = Convert.ToString(ds3.Tables[0].Rows[i][7]);
                            string del = "delete from Answer";
                            SqlCommand dcmd = new SqlCommand(del, con);
                            con.Open();
                            dcmd.ExecuteNonQuery();
                            con.Close();
                            SqlCommand cmd1 = new SqlCommand("Insert into Answer(Question,Answer,Link) values('" + s + "','" + rep + "','" + link + "')", con);
                            con.Open();
                            cmd1.ExecuteNonQuery();
                            con.Close();
                            Response.Redirect("bot.aspx?reply=" + rep + "&link=" + link + "");

                        }
                    }
                    if (rep1 == "s")
                    {
                        da3 = new SqlDataAdapter("Select * from QuestionPapers where keywords = '2' order by newid()", con);
                        ds3 = new DataSet();
                        da3.Fill(ds3);
                        rep = Convert.ToString(ds3.Tables[0].Rows[0][6]);
                        link = Convert.ToString(ds3.Tables[0].Rows[0][7]);
                        string del = "delete from Answer";
                        SqlCommand dcmd = new SqlCommand(del, con);
                        con.Open();
                        dcmd.ExecuteNonQuery();
                        con.Close();
                        SqlCommand cmd1 = new SqlCommand("Insert into Answer(Question,Answer,Link) values('" + s + "','" + rep + "','" + link + "')", con);
                        con.Open();
                        cmd1.ExecuteNonQuery();
                        con.Close();
                        Response.Redirect("bot.aspx?reply=" + rep + "&link=" + link + "");
                    }
                }
                else if (s.Contains("reference book"))
                {
                    ans = "OK";
                    String s2, k1, k2, rep1 = "s";
                    int count;
                    SqlDataAdapter da3 = new SqlDataAdapter("Select * from ReferenceBook where keywords = '2'", con);
                    DataSet ds3 = new DataSet();
                    da3.Fill(ds3);
                    count = Convert.ToInt32(ds3.Tables[0].Rows.Count);
                    for (int i = 0; i < count; i++)
                    {
                        k1 = Convert.ToString(ds3.Tables[0].Rows[i][1]).ToLower();
                        k2 = Convert.ToString(ds3.Tables[0].Rows[i][2]).ToLower();
                        // k3 = Convert.ToString(ds3.Tables[0].Rows[i][2]).ToLower();
                        if (s.Contains(k1) && s.Contains(k2))
                        {
                            rep1 = Convert.ToString(ds3.Tables[0].Rows[i][6]);
                            rep = rep1;
                            string del = "delete from Answer";
                            SqlCommand dcmd = new SqlCommand(del, con);
                            con.Open();
                            dcmd.ExecuteNonQuery();
                            con.Close();
                            SqlCommand cmd1 = new SqlCommand("Insert into Answer(Question,Answer) values('" + s + "','" + rep + "')", con);
                            con.Open();
                            cmd1.ExecuteNonQuery();
                            con.Close();
                            Response.Redirect("bot.aspx?reply=" + rep + "");
                        }

                    }
                    if (rep1 == "s")
                    {
                        da3 = new SqlDataAdapter("Select * from ReferenceBook where keywords = '2' order by newid()", con);
                        ds3 = new DataSet();
                        da3.Fill(ds3);
                        rep = Convert.ToString(ds3.Tables[0].Rows[0][6]);
                        string del = "delete from Answer";
                        SqlCommand dcmd = new SqlCommand(del, con);
                        con.Open();
                        dcmd.ExecuteNonQuery();
                        con.Close();
                        SqlCommand cmd1 = new SqlCommand("Insert into Answer(Question,Answer) values('" + s + "','" + rep + "')", con);
                        con.Open();
                        cmd1.ExecuteNonQuery();
                        con.Close();
                        Response.Redirect("bot.aspx?reply=" + rep + "");
                    }
                }
                else if (s.Contains("subjects") || s.Contains("subject"))
                {
                    ans = "OK";
                    String s2, k1, k2, k3, k4, rep1 = "s";
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
                        // k5 = Convert.ToString(ds3.Tables[0].Rows[i][5]).ToLower();
                        if (s.Contains(k1) && (s.Contains(k2) || s.Contains(k4)) && s.Contains(k3))
                        {
                            rep1 = Convert.ToString(ds3.Tables[0].Rows[i][9]);
                            rep = rep1;
                            string del = "delete from Answer";
                            SqlCommand dcmd = new SqlCommand(del, con);
                            con.Open();
                            dcmd.ExecuteNonQuery();
                            con.Close();
                            SqlCommand cmd1 = new SqlCommand("Insert into Answer(Question,Answer) values('" + s + "','" + rep + "')", con);
                            con.Open();
                            cmd1.ExecuteNonQuery();
                            con.Close();
                            Response.Redirect("bot.aspx?reply=" + rep + "");
                        }
                    }
                    if (rep1 == "s")
                    {
                        da3 = new SqlDataAdapter("Select * from Subjects where keywords = '2' order by newid()", con);
                        ds3 = new DataSet();
                        da3.Fill(ds3);
                        rep = Convert.ToString(ds3.Tables[0].Rows[0][9]);
                        string del = "delete from Answer";
                        SqlCommand dcmd = new SqlCommand(del, con);
                        con.Open();
                        dcmd.ExecuteNonQuery();
                        con.Close();
                        SqlCommand cmd1 = new SqlCommand("Insert into Answer(Question,Answer) values('" + s + "','" + rep + "')", con);
                        con.Open();
                        cmd1.ExecuteNonQuery();
                        con.Close();
                        Response.Redirect("bot.aspx?reply=" + rep + "");
                    }
                }
                else if (s.Contains("cabin"))
                {
                    ans = "OK";
                    String s2, k1, k2, rep1 = "s";
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
                            rep1 = Convert.ToString(ds3.Tables[0].Rows[i][6]);
                            rep = rep1;
                            string del = "delete from Answer";
                            SqlCommand dcmd = new SqlCommand(del, con);
                            con.Open();
                            dcmd.ExecuteNonQuery();
                            con.Close();
                            SqlCommand cmd1 = new SqlCommand("Insert into Answer(Question,Answer) values('" + s + "','" + rep + "')", con);
                            con.Open();
                            cmd1.ExecuteNonQuery();
                            con.Close();
                            Response.Redirect("bot.aspx?reply=" + rep + "");
                        }
                    }
                    if (rep1 == "s")
                    {
                        da3 = new SqlDataAdapter("Select * from Cabin where keywords = '2' order by newid()", con);
                        ds3 = new DataSet();
                        da3.Fill(ds3);
                        rep = Convert.ToString(ds3.Tables[0].Rows[0][6]);
                        string del = "delete from Answer";
                        SqlCommand dcmd = new SqlCommand(del, con);
                        con.Open();
                        dcmd.ExecuteNonQuery();
                        con.Close();
                        SqlCommand cmd1 = new SqlCommand("Insert into Answer(Question,Answer) values('" + s + "','" + rep + "')", con);
                        con.Open();
                        cmd1.ExecuteNonQuery();
                        con.Close();
                        Response.Redirect("bot.aspx?reply=" + rep + "");
                    }
                }
                else if (s.Contains("contact") || s.Contains("mobile"))
                {
                    ans = "OK";
                    String s2, k1, k3, k4, k5, k6, rep1 = "s";
                    int count;
                    SqlDataAdapter da3 = new SqlDataAdapter("Select * from Contact where keywords = '3'", con);
                    DataSet ds3 = new DataSet();
                    da3.Fill(ds3);
                    count = Convert.ToInt32(ds3.Tables[0].Rows.Count);
                    for (int i = 0; i < count; i++)
                    {
                        k1 = Convert.ToString(ds3.Tables[0].Rows[i][1]).ToLower();
                        // k2 = Convert.ToString(ds3.Tables[0].Rows[i][2]).ToLower();
                        k3 = Convert.ToString(ds3.Tables[0].Rows[i][3]).ToLower();
                        k4 = Convert.ToString(ds3.Tables[0].Rows[i][4]).ToLower();
                        k5 = Convert.ToString(ds3.Tables[0].Rows[i][5]).ToLower();
                        // k6 = Convert.ToString(ds3.Tables[0].Rows[i][6]).ToLower();
                        // k7 = Convert.ToString(ds3.Tables[0].Rows[i][7]).ToLower();
                        if (s.Contains(k1) && s.Contains(k3) && (s.Contains(k4) || s.Contains(k5)))

                        {
                            rep1 = Convert.ToString(ds3.Tables[0].Rows[i][9]);
                            rep = rep1;
                            string del = "delete from Answer";
                            SqlCommand dcmd = new SqlCommand(del, con);
                            con.Open();
                            dcmd.ExecuteNonQuery();
                            con.Close();
                            SqlCommand cmd1 = new SqlCommand("Insert into Answer(Question,Answer) values('" + s + "','" + rep + "')", con);
                            con.Open();
                            cmd1.ExecuteNonQuery();
                            con.Close();
                            Response.Redirect("bot.aspx?reply=" + rep + "");
                        }
                    }
                    if (rep1 == "s")
                    {
                        da3 = new SqlDataAdapter("Select * from Contact where keywords = '3' order by newid()", con);
                        ds3 = new DataSet();
                        da3.Fill(ds3);
                        rep = Convert.ToString(ds3.Tables[0].Rows[0][9]);
                        string del = "delete from Answer";
                        SqlCommand dcmd = new SqlCommand(del, con);
                        con.Open();
                        dcmd.ExecuteNonQuery();
                        con.Close();
                        SqlCommand cmd1 = new SqlCommand("Insert into Answer(Question,Answer) values('" + s + "','" + rep + "')", con);
                        con.Open();
                        cmd1.ExecuteNonQuery();
                        con.Close();
                        Response.Redirect("bot.aspx?reply=" + rep + "");
                    }
                }
                else if (s.Contains("specialization") || s.Contains("specialization") || s.Contains("specialisation"))
                {
                    ans = "OK";
                    String s2, k1, k2, rep1 = "s";
                    int count;
                    SqlDataAdapter da3 = new SqlDataAdapter("Select * from specialization where keywords = '2'", con);
                    DataSet ds3 = new DataSet();
                    da3.Fill(ds3);
                    count = Convert.ToInt32(ds3.Tables[0].Rows.Count);
                    for (int i = 0; i < count; i++)
                    {
                        k1 = Convert.ToString(ds3.Tables[0].Rows[i][1]).ToLower();
                        k2 = Convert.ToString(ds3.Tables[0].Rows[i][2]).ToLower();
                        if (s.Contains(k1) && s.Contains(k2))
                        {
                            rep1 = Convert.ToString(ds3.Tables[0].Rows[i][6]);
                            rep = rep1;
                            string del = "delete from Answer";
                            SqlCommand dcmd = new SqlCommand(del, con);
                            con.Open();
                            dcmd.ExecuteNonQuery();
                            con.Close();
                            SqlCommand cmd1 = new SqlCommand("Insert into Answer(Question,Answer) values('" + s + "','" + rep + "')", con);
                            con.Open();
                            cmd1.ExecuteNonQuery();
                            con.Close();
                            Response.Redirect("bot.aspx?reply=" + rep + "");
                        }
                    }
                    if (rep1 == "s")
                    {
                        da3 = new SqlDataAdapter("Select * from specialization where keywords = '2' order by newid()", con);
                        ds3 = new DataSet();
                        da3.Fill(ds3);
                        rep = Convert.ToString(ds3.Tables[0].Rows[0][6]);
                        string del = "delete from Answer";
                        SqlCommand dcmd = new SqlCommand(del, con);
                        con.Open();
                        dcmd.ExecuteNonQuery();
                        con.Close();
                        SqlCommand cmd1 = new SqlCommand("Insert into Answer(Question,Answer) values('" + s + "','" + rep + "')", con);
                        con.Open();
                        cmd1.ExecuteNonQuery();
                        con.Close();
                        Response.Redirect("bot.aspx?reply=" + rep + "");
                    }
                }
                else if (s.Contains("faculties"))
                {
                    ans = "OK";
                    String s2, k1, k2, k3, rep1 = "s";
                    int count;
                    SqlDataAdapter da3 = new SqlDataAdapter("Select * from faculaties where keywords = '3'", con);
                    DataSet ds3 = new DataSet();
                    da3.Fill(ds3);
                    count = Convert.ToInt32(ds3.Tables[0].Rows.Count);
                    for (int i = 0; i < count; i++)
                    {
                        k1 = Convert.ToString(ds3.Tables[0].Rows[i][1]).ToLower();
                        k2 = Convert.ToString(ds3.Tables[0].Rows[i][2]).ToLower();
                        k3 = Convert.ToString(ds3.Tables[0].Rows[i][3]).ToLower();
                        if (s.Contains(k1) && s.Contains(k2) && s.Contains(k3))
                        {
                            rep1 = Convert.ToString(ds3.Tables[0].Rows[i][6]);
                            rep = rep1;
                            string del = "delete from Answer";
                            SqlCommand dcmd = new SqlCommand(del, con);
                            con.Open();
                            dcmd.ExecuteNonQuery();
                            con.Close();
                            SqlCommand cmd1 = new SqlCommand("Insert into Answer(Question,Answer) values('" + s + "','" + rep + "')", con);
                            con.Open();
                            cmd1.ExecuteNonQuery();
                            con.Close();
                            Response.Redirect("bot.aspx?reply=" + rep + "");
                        }
                    }
                    if (rep1 == "s")
                    {
                        da3 = new SqlDataAdapter("Select * from faculaties where keywords = '3' order by newid()", con);
                        ds3 = new DataSet();
                        da3.Fill(ds3);
                        rep = Convert.ToString(ds3.Tables[0].Rows[0][6]);
                        string del = "delete from Answer";
                        SqlCommand dcmd = new SqlCommand(del, con);
                        con.Open();
                        dcmd.ExecuteNonQuery();
                        con.Close();
                        SqlCommand cmd1 = new SqlCommand("Insert into Answer(Question,Answer) values('" + s + "','" + rep + "')", con);
                        con.Open();
                        cmd1.ExecuteNonQuery();
                        con.Close();
                        Response.Redirect("bot.aspx?reply=" + rep + "");
                    }
                }
                else if (s.Contains("classroom"))
                {
                    ans = "OK";
                    String s2, k1, k2, rep1 = "s";
                    int count;
                    SqlDataAdapter da3 = new SqlDataAdapter("Select * from classroom where keywords = '2'", con);
                    DataSet ds3 = new DataSet();
                    da3.Fill(ds3);
                    count = Convert.ToInt32(ds3.Tables[0].Rows.Count);
                    for (int i = 0; i < count; i++)
                    {
                        k1 = Convert.ToString(ds3.Tables[0].Rows[i][1]).ToLower();
                        k2 = Convert.ToString(ds3.Tables[0].Rows[i][2]).ToLower();
                        if (s.Contains(k1) && s.Contains(k2))
                        {
                            rep1 = Convert.ToString(ds3.Tables[0].Rows[i][6]);
                            rep = rep1;
                            string del = "delete from Answer";
                            SqlCommand dcmd = new SqlCommand(del, con);
                            con.Open();
                            dcmd.ExecuteNonQuery();
                            con.Close();
                            SqlCommand cmd1 = new SqlCommand("Insert into Answer(Question,Answer) values('" + s + "','" + rep + "')", con);
                            con.Open();
                            cmd1.ExecuteNonQuery();
                            con.Close();
                            Response.Redirect("bot.aspx?reply=" + rep + "");
                        }
                    }
                    if (rep1 == "s")
                    {
                        da3 = new SqlDataAdapter("Select * from classroom where keywords = '2' order by newid()", con);
                        ds3 = new DataSet();
                        da3.Fill(ds3);
                        rep = Convert.ToString(ds3.Tables[0].Rows[0][6]);
                        string del = "delete from Answer";
                        SqlCommand dcmd = new SqlCommand(del, con);
                        con.Open();
                        dcmd.ExecuteNonQuery();
                        con.Close();
                        SqlCommand cmd1 = new SqlCommand("Insert into Answer(Question,Answer) values('" + s + "','" + rep + "')", con);
                        con.Open();
                        cmd1.ExecuteNonQuery();
                        con.Close();
                        Response.Redirect("bot.aspx?reply=" + rep + "");
                    }
                }


                else if (s.Contains("how"))
                {
                    ans = "OK";
                    String s2, query1, query2, query3, rep1 = "s";
                    int count;
                    SqlDataAdapter da3 = new SqlDataAdapter("Select * from doublereply where keywords = '3'", con);
                    DataSet ds3 = new DataSet();
                    da3.Fill(ds3);
                    count = Convert.ToInt32(ds3.Tables[0].Rows.Count);
                    for (int i = 0; i < count; i++)
                    {
                        query1 = Convert.ToString(ds3.Tables[0].Rows[i][1]).ToLower();
                        query2 = Convert.ToString(ds3.Tables[0].Rows[i][2]).ToLower();
                        query3 = Convert.ToString(ds3.Tables[0].Rows[i][3]).ToLower();
                        if (s.Contains(query1) && s.Contains(query2) && s.Contains(query3))
                        {
                            rep1 = Convert.ToString(ds3.Tables[0].Rows[i][6]);
                            rep = rep1;
                            string del = "delete from Answer";
                            SqlCommand dcmd = new SqlCommand(del, con);
                            con.Open();
                            dcmd.ExecuteNonQuery();
                            con.Close();
                            SqlCommand cmd1 = new SqlCommand("Insert into Answer(Question,Answer) values('" + s + "','" + rep + "')", con);
                            con.Open();
                            cmd1.ExecuteNonQuery();
                            con.Close();
                            Response.Redirect("bot.aspx?reply=" + rep + "");
                        }
                    }
                    if (rep1 == "s")
                    {
                        da3 = new SqlDataAdapter("Select * from  doublereply where keywords = '3' order by newid()", con);
                        ds3 = new DataSet();
                        da3.Fill(ds3);
                        rep = Convert.ToString(ds3.Tables[0].Rows[0][6]);
                        string del = "delete from Answer";
                        SqlCommand dcmd = new SqlCommand(del, con);
                        con.Open();
                        dcmd.ExecuteNonQuery();
                        con.Close();
                        SqlCommand cmd1 = new SqlCommand("Insert into Answer(Question,Answer) values('" + s + "','" + rep + "')", con);
                        con.Open();
                        cmd1.ExecuteNonQuery();
                        con.Close();
                        Response.Redirect("bot.aspx?reply=" + rep + "");
                    }
                }



                else if (s.Contains("what"))
                {
                    ans = "OK";
                    String s2, query1, query2, query3, rep1 = "s";
                    int count;
                    SqlDataAdapter da3 = new SqlDataAdapter("Select * from whatdoublereply where keywords = '3'", con);
                    DataSet ds3 = new DataSet();
                    da3.Fill(ds3);
                    count = Convert.ToInt32(ds3.Tables[0].Rows.Count);
                    for (int i = 0; i < count; i++)
                    {
                        query1 = Convert.ToString(ds3.Tables[0].Rows[i][1]).ToLower();
                        query2 = Convert.ToString(ds3.Tables[0].Rows[i][2]).ToLower();
                        query3 = Convert.ToString(ds3.Tables[0].Rows[i][3]).ToLower();
                        if (s.Contains(query1) && s.Contains(query2) && s.Contains(query3))
                        {
                            rep1 = Convert.ToString(ds3.Tables[0].Rows[i][6]);
                            rep = rep1;
                            string del = "delete from Answer";
                            SqlCommand dcmd = new SqlCommand(del, con);
                            con.Open();
                            dcmd.ExecuteNonQuery();
                            con.Close();
                            SqlCommand cmd1 = new SqlCommand("Insert into Answer(Question,Answer) values('" + s + "','" + rep + "')", con);
                            con.Open();
                            cmd1.ExecuteNonQuery();
                            con.Close();
                            Response.Redirect("bot.aspx?reply=" + rep + "");
                        }
                    }
                    if (rep1 == "s")
                    {
                        da3 = new SqlDataAdapter("Select * from  whatdoublereply where keywords = '3' order by newid()", con);
                        ds3 = new DataSet();
                        da3.Fill(ds3);
                        rep = Convert.ToString(ds3.Tables[0].Rows[0][6]);
                        string del = "delete from Answer";
                        SqlCommand dcmd = new SqlCommand(del, con);
                        con.Open();
                        dcmd.ExecuteNonQuery();
                        con.Close();
                        SqlCommand cmd1 = new SqlCommand("Insert into Answer(Question,Answer) values('" + s + "','" + rep + "')", con);
                        con.Open();
                        cmd1.ExecuteNonQuery();
                        con.Close();
                        Response.Redirect("bot.aspx?reply=" + rep + "");
                    }
                }


                else if (s.Contains("where"))
                {
                    ans = "OK";
                    String s2, query1, query2, query3, rep1 = "s";
                    int count;
                    SqlDataAdapter da3 = new SqlDataAdapter("Select * from wheredoublereply where keywords = '3'", con);
                    DataSet ds3 = new DataSet();
                    da3.Fill(ds3);
                    count = Convert.ToInt32(ds3.Tables[0].Rows.Count);
                    for (int i = 0; i < count; i++)
                    {
                        query1 = Convert.ToString(ds3.Tables[0].Rows[i][1]).ToLower();
                        query2 = Convert.ToString(ds3.Tables[0].Rows[i][2]).ToLower();
                        query3 = Convert.ToString(ds3.Tables[0].Rows[i][3]).ToLower();
                        if (s.Contains(query1) && s.Contains(query2) && s.Contains(query3))
                        {
                            rep1 = Convert.ToString(ds3.Tables[0].Rows[i][6]);
                            rep = rep1;
                            string del = "delete from Answer";
                            SqlCommand dcmd = new SqlCommand(del, con);
                            con.Open();
                            dcmd.ExecuteNonQuery();
                            con.Close();
                            SqlCommand cmd1 = new SqlCommand("Insert into Answer(Question,Answer) values('" + s + "','" + rep + "')", con);
                            con.Open();
                            cmd1.ExecuteNonQuery();
                            con.Close();
                            Response.Redirect("bot.aspx?reply=" + rep + "");
                        }
                    }
                    if (rep1 == "s")
                    {
                        da3 = new SqlDataAdapter("Select * from  wheredoublereply where keywords = '3' order by newid()", con);
                        ds3 = new DataSet();
                        da3.Fill(ds3);
                        rep = Convert.ToString(ds3.Tables[0].Rows[0][6]);
                        string del = "delete from Answer";
                        SqlCommand dcmd = new SqlCommand(del, con);
                        con.Open();
                        dcmd.ExecuteNonQuery();
                        con.Close();
                        SqlCommand cmd1 = new SqlCommand("Insert into Answer(Question,Answer) values('" + s + "','" + rep + "')", con);
                        con.Open();
                        cmd1.ExecuteNonQuery();
                        con.Close();
                        Response.Redirect("bot.aspx?reply=" + rep + "");
                    }
                }



                else if (s.Contains("who"))
                {
                    ans = "OK";
                    String s2, query1, query2, query3, rep1 = "s";
                    int count;
                    SqlDataAdapter da3 = new SqlDataAdapter("Select * from whodoublereply where keywords = '3'", con);
                    DataSet ds3 = new DataSet();
                    da3.Fill(ds3);
                    count = Convert.ToInt32(ds3.Tables[0].Rows.Count);
                    for (int i = 0; i < count; i++)
                    {
                        query1 = Convert.ToString(ds3.Tables[0].Rows[i][1]).ToLower();
                        query2 = Convert.ToString(ds3.Tables[0].Rows[i][2]).ToLower();
                        query3 = Convert.ToString(ds3.Tables[0].Rows[i][3]).ToLower();
                        if (s.Contains(query1) && s.Contains(query2) && s.Contains(query3))
                        {
                            rep1 = Convert.ToString(ds3.Tables[0].Rows[i][6]);
                            rep = rep1;
                            string del = "delete from Answer";
                            SqlCommand dcmd = new SqlCommand(del, con);
                            con.Open();
                            dcmd.ExecuteNonQuery();
                            con.Close();
                            SqlCommand cmd1 = new SqlCommand("Insert into Answer(Question,Answer) values('" + s + "','" + rep + "')", con);
                            con.Open();
                            cmd1.ExecuteNonQuery();
                            con.Close();
                            Response.Redirect("bot.aspx?reply=" + rep + "");
                        }
                    }
                    if (rep1 == "s")
                    {
                        da3 = new SqlDataAdapter("Select * from  whodoublereply where keywords = '3' order by newid()", con);
                        ds3 = new DataSet();
                        da3.Fill(ds3);
                        rep = Convert.ToString(ds3.Tables[0].Rows[0][6]);
                        string del = "delete from Answer";
                        SqlCommand dcmd = new SqlCommand(del, con);
                        con.Open();
                        dcmd.ExecuteNonQuery();
                        con.Close();
                        SqlCommand cmd1 = new SqlCommand("Insert into Answer(Question,Answer) values('" + s + "','" + rep + "')", con);
                        con.Open();
                        cmd1.ExecuteNonQuery();
                        con.Close();
                        Response.Redirect("bot.aspx?reply=" + rep + "");
                    }
                }


                else if (ans != "OK")
                {
                    rep = Convert.ToString("No related Answer Found");
                    string del = "delete from Answer";
                    SqlCommand dcmd = new SqlCommand(del, con);
                    con.Open();
                    dcmd.ExecuteNonQuery();
                    con.Close();
                    SqlCommand cmd1 = new SqlCommand("Insert into Answer(Question,Answer) values('" + s + "','" + rep + "')", con);
                    con.Open();
                    cmd1.ExecuteNonQuery();
                    con.Close();
                    Response.Redirect("bot.aspx?reply=" + rep + "");
                }

            }
            else
            {
                Response.Redirect("bot.aspx?reply=none");
            }



        }
    }
}