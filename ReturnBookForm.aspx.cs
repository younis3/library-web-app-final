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
    public partial class ReturnBookForm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void buttonReturnBook_Click(object sender, EventArgs e)
        {
            // Connect to DB
            string cs = ConfigurationManager.ConnectionStrings["myConnectionString"].ConnectionString;      //connection string and DB name in web.config file
            //string cs = @"Server=localhost\SQLEXPRESS;Database=ahmad_test;Trusted_Connection=True;";
            SqlConnection con = new SqlConnection(cs);
            //CheckConnectionVersion(con);

            string inptSubID = textSubID.Text;
            string BookID = textBookID.Text;

            int sub_id;
            int book_key;
            try
            {
                sub_id = Convert.ToInt32(inptSubID);
                book_key = Convert.ToInt32(BookID);
            }
            catch
            {
                lblMsg.Text = "Error: Please use numbers only for book and subscriber IDs";
                return;
                
            }

            ReturnBook(sub_id, book_key, con);
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

        protected void ReturnBook(int sub_id, int book_key, SqlConnection con)
        {
            if (con != null && con.State == ConnectionState.Closed)   //check if connection is open to prevent errors
            {
                con.Open();
            }

            lblMsg.Attributes.Remove("class");
            lblMsg.Attributes.Add("class", "lblMsg");

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;

            //check if subscriber exists
            cmd.CommandText = $@"SELECT Count(*) FROM subscribers_tbl WHERE Sub_ID = {sub_id}";
            int subCount = 0;
            subCount = Convert.ToInt32(cmd.ExecuteScalar());

            if (subCount == 0)     //subscriber doesn't exist
            {
                lblMsg.Text = "Subscriber doesn't exist";
                return;
            }


            //check if book exist
            cmd.CommandText = $@"SELECT Count(*) FROM books_tbl WHERE Book_ID = {book_key}";
            int bookCount = 0;
            bookCount = Convert.ToInt32(cmd.ExecuteScalar());

            if (bookCount == 0)     //book doesn't exist
            {
                lblMsg.Text = "Book doesn't exist";
                return;
            }


            //check if subscriber has the book
            cmd.CommandText = $@"SELECT Count(*) FROM loans_tbl WHERE Book_ID = {book_key} AND Sub_ID = {sub_id}";
            int bookCountSub = 0;
            bookCountSub = Convert.ToInt32(cmd.ExecuteScalar());
            if (bookCountSub == 0)     //book doesn't exist in subscriber loaned books list
            {
                lblMsg.Text = "Book doesn't exist in subscriber loaned books list";
                return;
            }


            //check book type
            cmd.CommandText = $@"SELECT BookType, NumOfCopies FROM books_tbl WHERE Book_ID = {book_key}";
            var reader = cmd.ExecuteReader();
            string book_type;
            int copiesNum;
            if (reader.Read())
            {
                book_type = reader.GetString(0);
                copiesNum = reader.GetInt32(1);
            }
            else
            {
                lblMsg.Text = "Error";
                return;
            }
            reader.Close();


            //if all success. Return book
            if (book_type == "paper")
            {
                cmd.CommandText = $@"UPDATE books_tbl SET NumOfCopies = NumOfCopies + 1 WHERE Book_ID = {book_key}";
                cmd.ExecuteNonQuery();
            }
            cmd.CommandText = $@"UPDATE subscribers_tbl SET CurrLoanNum = CurrLoanNum - 1 WHERE Sub_ID = {sub_id}";
            cmd.ExecuteNonQuery();

            //remove from loans table
            cmd.CommandText = $@"DELETE FROM loans_tbl WHERE Book_ID = {book_key} AND Sub_ID = {sub_id}";
            cmd.ExecuteNonQuery();
            lblMsg.Text = "Book returned successfully";
            lblMsg.Attributes.Add("class", "success");

            con.Close();
        }

    }
}