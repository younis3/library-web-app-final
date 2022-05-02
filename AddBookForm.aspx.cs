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
    public partial class AddBookForm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

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


        protected void buttonAddBook_Click(object sender, EventArgs e)
        {
            // Connect to DB
            string cs = ConfigurationManager.ConnectionStrings["myConnectionString"].ConnectionString;      //connection string and DB name in web.config file
            //string cs = @"Server=localhost\SQLEXPRESS;Database=ahmad_test;Trusted_Connection=True;";
            SqlConnection con = new SqlConnection(cs);
            //CheckConnectionVersion(con);

            //get requested info
            string bookID = txtBookID.Text;
            int book_id;
            try
            {
                book_id = int.Parse(bookID);
            }
            catch
            {
                lblMsg.Text = "Error: Please type valid ID";
                return;
            }

            string title = textTitle.Text;
            string fname = textFirstName.Text;
            string lname = textLastName.Text;
            string genre = textGenre.Text;

            //get book type from radio;
            string type;
            bool isCheckedPaper = radioTypePaper.Checked;
            bool isCheckedDigital = radioTypeDigital.Checked;
            if (!isCheckedPaper && !isCheckedDigital)
            {
                lblMsg.Text = "Please select book type";
                return;
            }
            else
            {
                if (isCheckedPaper)
                {
                    type = radioTypePaper.Text.ToLower(); ;
                }

                else
                {
                    type = radioTypeDigital.Text.ToLower();
                }
            }
            //Response.Write(type);

            int copynum;
            if (type == "paper")
            {
                copynum = 1;
            }
            else
            {
                copynum = -1;
            }
           
            //add to DB
            AddBook(book_id, title, fname, lname, genre, type, copynum, con);
        }


        protected void AddBook(int key, string title, string fname, string lname, string genre, string type, int copynum, SqlConnection con)
        {
            if (con != null && con.State == ConnectionState.Closed)   //check if connection is open to prevent errors
            {
                con.Open();
            }

            lblMsg.Attributes.Remove("class");
            lblMsg.Attributes.Add("class", "lblMsg");

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;


            //check if book exists
            cmd.CommandText = $@"SELECT Count(*) FROM books_tbl WHERE Book_ID = {key}";
            int bookCount = 0;
            bookCount = Convert.ToInt32(cmd.ExecuteScalar());

            if (bookCount > 0)     //book exists
            {
                if (type == "paper")
                {
                    cmd.CommandText = $@"UPDATE books_tbl SET NumOfCopies = NumOfCopies + 1 WHERE Book_ID = {key}";
                    cmd.ExecuteNonQuery();
                    lblMsg.Attributes.Add("class", "success");
                    lblMsg.Text = "Updated number of copies";
                }
                else
                {
                    lblMsg.Text = "Book ID already exists in the Library!";
                }

            }
            else    //book doesn't exist in the library. Add it
            {
                if (type == "paper")
                {
                    cmd.CommandText = $@"INSERT INTO books_tbl(Book_ID, Title, BookType, AuthorFirstName, AuthorLastName, Genre, NumOfCopies) VALUES
                                    ({key}, '{title}', 'paper', '{fname}', '{lname}', '{genre}', {copynum})";
                    cmd.ExecuteNonQuery();
                }
                else
                {
                    cmd.CommandText = $@"INSERT INTO books_tbl(Book_ID, Title, BookType, AuthorFirstName, AuthorLastName, Genre, NumOfCopies) VALUES
                                    ({key},'{title}', 'digital', '{fname}', '{lname}', '{genre}', -1)";
                    cmd.ExecuteNonQuery();
                }
                lblMsg.Attributes.Add("class", "success");
                lblMsg.Text = "Success";
            }
            con.Close();
        }

        protected void buttonBacktoMM_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Default.aspx");
        }
    }
}