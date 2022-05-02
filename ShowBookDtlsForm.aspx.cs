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
    public partial class ShowBookDtlsForm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }


        protected void buttonShowBookDtls_Click(object sender, EventArgs e)
        {
            // Connect to DB
            string cs = ConfigurationManager.ConnectionStrings["myConnectionString"].ConnectionString;      //connection string and DB name in web.config file
            //string cs = @"Server=localhost\SQLEXPRESS;Database=ahmad_test;Trusted_Connection=True;";
            SqlConnection con = new SqlConnection(cs);
            //CheckConnectionVersion(con);


            string inptBookName = textBookTitle.Text;
            string inptAuthorFirstName = textBookAuthFirstName.Text;
            string inptAuthorLastName = textBookAuthLastName.Text;

            if(inptBookName == "" || inptAuthorFirstName == "" || inptAuthorLastName == "")
            {
                lblMsg.Text = "One of the required information is missing";
                return;
            }

            PrintBookInfo(inptBookName, inptAuthorFirstName, inptAuthorLastName, con);
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

        protected void PrintBookInfo(string bookName, string authorFirstName, string authorLastName, SqlConnection con)
        {
            if (con != null && con.State == ConnectionState.Closed)   //check if connection is open to prevent errors
            {
                con.Open();
            }

            lblMsg.Attributes.Remove("class");
            lblMsg.Attributes.Add("class", "lblMsg");

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;

            //get book details
            cmd.CommandText = $@"SELECT BookType, Genre, NumOfCopies FROM books_tbl WHERE Title = '{bookName}' AND AuthorFirstName = '{authorFirstName}'AND AuthorLastName = '{authorLastName}'";
            var reader = cmd.ExecuteReader();
            string book_type;
            string genre;
            int copiesNum;
            if (reader.Read())
            {
                book_type = reader.GetString(0);
                genre = reader.GetString(1);
                copiesNum = reader.GetInt32(2);
            }
            else
            {
                lblMsg.Text = "Book was not found!";
                reader.Close();
                return;
            }
            reader.Close();

            //print book details
            lblMsg.Attributes.Add("class", "success");
            if (book_type == "paper")
            {
                lblMsg.Text = $"{bookName}, paper-book, {genre}, number of available copies: {copiesNum}";
            }
            else
            {
                lblMsg.Text = $"{bookName}, digital-book, {genre}";
            }
            con.Close();
        }

    }
}