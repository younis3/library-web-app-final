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
    public partial class LoanBookForm : System.Web.UI.Page
    {
        int Loan_Limit = 3;

        protected void Page_Load(object sender, EventArgs e)
        {
            textfindkey.Visible = false;
            labelfindkeylist.Visible = false;
        }

        protected void buttonLoanBook_Click(object sender, EventArgs e)
        {
            // Connect to DB
            string cs = ConfigurationManager.ConnectionStrings["myConnectionString"].ConnectionString;      //connection string and DB name in web.config file
            //string cs = @"Server=localhost\SQLEXPRESS;Database=ahmad_test;Trusted_Connection=True;";
            SqlConnection con = new SqlConnection(cs);
            //CheckConnectionVersion(con);

            //get user option from radio;
            string opt;
            int inptOpt;
            bool isCheckedID = radioOptionID.Checked;
            bool isCheckedTitle = radioOptionTitle.Checked;
            if (!isCheckedID && !isCheckedTitle)
            {
                lblMsg.Text = "Please choose Key / Title";
                return;
            }
            else
            {
                if (isCheckedID)
                {
                    opt = radioOptionID.Text.ToLower();
                    inptOpt = 1;
                }

                else
                {
                    opt = radioOptionTitle.Text.ToLower();
                    inptOpt = 2;
                }
            }
            //Response.Write(inptOpt);

            string inptBook = textinpt.Text.ToLower();
            if(inptBook == "")
            {
                lblMsg.Text = "Please type Key / Title";
                return;
            }


            string inptSubID = txtSubID.Text;
            int sub_id;
            try
            {
                sub_id = int.Parse(inptSubID);
            }
            catch
            {
                lblMsg.Text = "Error: type Subscriber ID in numbers only";
                return;
            }

            //process order
            LoanBook(inptOpt, inptBook, sub_id, con);
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

        protected void LoanBook(int inptOpt, string inptBook, int subscriberID, SqlConnection con)
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
            cmd.CommandText = $@"SELECT Count(*) FROM subscribers_tbl WHERE Sub_ID = {subscriberID}";
            int subCount = 0;
            subCount = Convert.ToInt32(cmd.ExecuteScalar());

            if (subCount == 0)     //subscriber doesn't exist
            {
                lblMsg.Text = "Subscriber doesn't exist";
                return;
            }

            int bookID;
            if (inptOpt == 1)   //lookup book by key
            {
                try
                {
                    bookID = int.Parse(inptBook);

                    //check if book exist
                    cmd.CommandText = $@"SELECT Count(*) FROM books_tbl WHERE Book_ID = {bookID}";
                    int bookCount = 0;
                    bookCount = Convert.ToInt32(cmd.ExecuteScalar());

                    if (bookCount == 0)     //book doesn't exist
                    {
                        lblMsg.Text = "Book doesn't exist";
                        return;
                    }
                }
                catch (Exception e)    // error in case of user input string instead of int
                {
                    lblMsg.Text = "Error! Please type valid book ID!";
                    return;
                }
            }
            else if (inptOpt == 2)      //lookup book by name and loop through all results
            {
                
                string bookTitle = inptBook;
                cmd.CommandText = $@"SELECT Book_ID, Title, AuthorFirstName, AuthorLastName FROM books_tbl WHERE Title = '{bookTitle}'";
                var reader = cmd.ExecuteReader();
                if (!reader.HasRows)
                {
                    lblMsg.Text = "No books found matches the same title!";
                    reader.Close();
                    //con.Close();
                    return;
                }

                lblMsg.Text = "";
                labelfindkeylist.Text = "";

                while (reader.Read())
                {
                    int id = reader.GetInt32(0);
                    string title = reader.GetString(1);
                    string fname = reader.GetString(2);
                    string lname = reader.GetString(3);
                    string str = $"Book ID: {id}, Book Info: {title}, {fname} {lname}";
                    labelfindkeylist.Text += "<br />" + str;
                }
                
                reader.Close();

                labelOption.Visible = false;
                radioOptionID.Visible = false;
                radioOptionTitle.Visible = false;
                textinpt.Visible = false;
                labelSubID.Visible = false;
                txtSubID.Visible = false;
                textfindkey.Visible = true;
                labelfindkeylist.Visible = true;


                string inptbooknew = textfindkey.Text;

                if(inptbooknew == ""  && textfindkey.Visible)
                {
                    lblMsg.Text = "Please type book ID!";
                    return;
                }

                try
                {
                    bookID = int.Parse(inptbooknew);
                }
                catch
                {
                    lblMsg.Text = "Error! Please type valid book ID!";
                    return;
                }
                
            }
            else
            {
                return;
            }

            LoanBookHelper(bookID, subscriberID, con);

            con.Close();
        }



        protected void LoanBookHelper(int book_id, int sub_id, SqlConnection con)
        {
            SqlCommand cmd_helper = new SqlCommand();
            cmd_helper.Connection = con;

            //check if subscriber already has the book
            cmd_helper.CommandText = $@"SELECT Count(*) FROM loans_tbl WHERE Book_ID = {book_id} AND Sub_ID = {sub_id}";
            int bookCountSub = 0;
            bookCountSub = Convert.ToInt32(cmd_helper.ExecuteScalar());
            if (bookCountSub > 0)     //subscriber already has the book
            {
                lblMsg.Text = "Subscriber already has the book";
                return;
            }

            //check subscriber if reached loan limit
            cmd_helper.CommandText = $@"SELECT CurrLoanNum FROM subscribers_tbl WHERE Sub_ID = {sub_id}";
            var reader_helper = cmd_helper.ExecuteReader();
            int loanNum;
            if (reader_helper.Read())
            {
                loanNum = reader_helper.GetInt32(0);
            }
            else
            {
                lblMsg.Text = "Error";
                return;
            }
            reader_helper.Close();

            if (loanNum < Loan_Limit)   //only if subscriber didn't reach loan limit allow order
            {
                //check book type
                cmd_helper.CommandText = $@"SELECT BookType, NumOfCopies FROM books_tbl WHERE Book_ID = {book_id}";
                reader_helper = cmd_helper.ExecuteReader();
                string book_type;
                int copiesNum;
                if (reader_helper.Read())
                {
                    book_type = reader_helper.GetString(0);
                    copiesNum = reader_helper.GetInt32(1);
                }
                else
                {
                    lblMsg.Text = "Error";
                    return;
                }
                reader_helper.Close();

                if (book_type == "paper")
                {
                    if (copiesNum > 0)
                    {
                        cmd_helper.CommandText = $@"UPDATE books_tbl SET NumOfCopies = NumOfCopies - 1 WHERE Book_ID = {book_id}";
                        cmd_helper.ExecuteNonQuery();
                        cmd_helper.CommandText = $@"UPDATE subscribers_tbl SET CurrLoanNum = CurrLoanNum + 1 WHERE Sub_ID = {sub_id}";
                        cmd_helper.ExecuteNonQuery();
                        lblMsg.Text = "Paper Book successfully loaned";
                        lblMsg.Attributes.Add("class", "success");

                        //insert into loans table
                        cmd_helper.CommandText = $@"INSERT INTO loans_tbl(Sub_ID, Book_ID) VALUES
                                            ({sub_id}, {book_id})";
                        cmd_helper.ExecuteNonQuery();
                    }
                    else
                    {
                        lblMsg.Text = "All copies of the book are already taken";
                    }
                }
                else
                {
                    cmd_helper.CommandText = $@"UPDATE subscribers_tbl SET CurrLoanNum = CurrLoanNum + 1 WHERE Sub_ID = {sub_id}";
                    cmd_helper.ExecuteNonQuery();
                    lblMsg.Text = "Digital Book successfully loaned";
                    lblMsg.Attributes.Add("class", "success");

                    //insert into loans table
                    cmd_helper.CommandText = $@"INSERT INTO loans_tbl(Sub_ID, Book_ID) VALUES
                                            ({sub_id}, {book_id})";
                    cmd_helper.ExecuteNonQuery();
                }
            }
            else
            {
                lblMsg.Text = "Subscriber reached loan limit";
            }
        }

    }
}