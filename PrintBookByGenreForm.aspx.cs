using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace LibraryApp4
{
    public partial class PrintBookByGenreForm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void buttonPrintByGenre_Click(object sender, EventArgs e)
        {
            // Connect to DB
            string cs = ConfigurationManager.ConnectionStrings["myConnectionString"].ConnectionString;      //connection string and DB name in web.config file
            //string cs = @"Server=localhost\SQLEXPRESS;Database=ahmad_test;Trusted_Connection=True;";
            SqlConnection con = new SqlConnection(cs);
            //CheckConnectionVersion(con);

            string inptGenre = textBookGenre.Text;

            if (inptGenre == "")
            {
                lblMsg.Text = "Please type genre";
                return;
            }

            PrintBooksByGenre(inptGenre, con);
        }

        protected void buttonBacktoMM_Click(object sender, EventArgs e)
        {

            Response.Redirect("~/Default.aspx");
        }

        protected void CheckConnectionVersion(SqlConnection con)
        {
            if (con != null && con.State == ConnectionState.Closed)   //check if connection is open to prevent errors
            {
                con.Open();
            }
            string ver_query = "SELECT @@VERSION";
            SqlCommand cmd = new SqlCommand(ver_query, con);
            string version = cmd.ExecuteScalar().ToString();
            Response.Write(version);
            con.Close();
        }


        protected void PrintBooksByGenre(string genre, SqlConnection con)
        {
            if (con != null && con.State == ConnectionState.Closed)   //check if connection is open to prevent errors
            {
                con.Open();
            }

            lblMsg.Attributes.Remove("class");
            lblMsg.Attributes.Add("class", "lblMsg");

            string query = $@"SELECT Book_ID, Title, AuthorFirstName, AuthorLastName FROM books_tbl WHERE Genre = '{genre}'";

            //add results to dataset in order to iterate multiple rows
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = new SqlCommand(query, con);
            DataSet set = new DataSet("genreBooks");
            int rowsNum = adapter.Fill(set, "books_tbl");   //fill dataset and return num of rows to check if it's empty or not

            if (rowsNum == 0)   //no results found in dataset
            {
                lblMsg.Text = "No Books were found with this genre!";
                return;
            }

            lblMsg.Attributes.Add("class", "success");
            lblMsg.Text = "";

            foreach (DataTable table in set.Tables)
            {
                foreach (DataRow dr in table.Rows)
                {
                    string book_id = dr["Book_ID"].ToString();
                    string title = dr["Title"].ToString();
                    string first_name = dr["AuthorFirstName"].ToString();
                    string last_name = dr["AuthorLastName"].ToString();
                    string str = $"{book_id}, {title}, {first_name} {last_name}";
                    lblMsg.Text += "<br />" + str;
                }
            }
            con.Close();
        }


    }
}