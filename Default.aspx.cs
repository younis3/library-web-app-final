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
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Connect to DB
            string cs = ConfigurationManager.ConnectionStrings["myConnectionString"].ConnectionString;      //connection string and DB name in web.config file
            //string cs = @"Server=localhost\SQLEXPRESS;Database=ahmad_test;Trusted_Connection=True;";
            SqlConnection con = new SqlConnection(cs);
            //CheckConnectionVersion(con);
            //--------------------------------------------------------------------

            //Creating DB tables
            bool isCreatedNew = Create_Tables(con);     //returns true if tables newly created
            if (isCreatedNew)    //only if created new tables initialize them with some data
            {
                InsertInitialData(con);
            }
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


        protected bool Create_Tables(SqlConnection con)
        {
            //Create Database Tables
            if (con != null && con.State == ConnectionState.Closed)   //check if connection is open to prevent errors
            {
                con.Open();
            }

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            int tblCount;


            //books
            //check if table exists
            /*
             * I used this method instead of dropping since I coudn't drop tables which has foreign keys so I decided to check if table exists or not
             * */
            cmd.CommandText = @"SELECT Count(*) FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'books_tbl'";
            tblCount = 0;
            tblCount = Convert.ToInt32(cmd.ExecuteScalar());  //ExecuteScalar is used because select COUNT returns only one result (no need for while(read))

            if (tblCount == 0)     //table doesn't exist, Create it
            {
                cmd.CommandText = @"CREATE TABLE books_tbl(
                                Book_ID INT NOT NULL PRIMARY KEY,
                                Title VARCHAR(255),
	                            BookType VARCHAR(255),
	                            AuthorFirstName VARCHAR(255),
	                            AuthorLastName VARCHAR(255),
	                            Genre VARCHAR(255),
	                            NumOfCopies INT
                                )";
                cmd.ExecuteNonQuery();
                Console.WriteLine("books_tbl created successfully");
            }


            //subscribers
            //check if table exists
            cmd.CommandText = @"SELECT Count(*) FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'subscribers_tbl'";
            tblCount = 0;
            tblCount = Convert.ToInt32(cmd.ExecuteScalar());
            if (tblCount == 0)     //table doesn't exist, Create it
            {
                cmd.CommandText = @"CREATE TABLE subscribers_tbl(
                                Sub_ID INT NOT NULL PRIMARY KEY,
	                            FirstName VARCHAR(255),
	                            LastName VARCHAR(255),
	                            CurrLoanNum INT
                                )";
                cmd.ExecuteNonQuery();
                Console.WriteLine("subscribers_tbl created successfully");
            }


            //loans
            //check if table exists
            cmd.CommandText = @"SELECT Count(*) FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'loans_tbl'";
            tblCount = 0;
            tblCount = Convert.ToInt32(cmd.ExecuteScalar());
            if (tblCount == 0)     //table doesn't exist, Create it
            {
                cmd.CommandText = @"CREATE TABLE loans_tbl(
                                Loan_ID INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
	                            Sub_ID INT FOREIGN KEY REFERENCES subscribers_tbl(Sub_ID),
	                            Book_ID INT FOREIGN KEY REFERENCES books_tbl(Book_ID)
                                )";
                cmd.ExecuteNonQuery();
                Console.WriteLine("loans_tbl created successfully");

            }

            con.Close();

            if (tblCount > 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }



        protected void InsertInitialData(SqlConnection con)
        {
            if (con != null && con.State == ConnectionState.Closed)   //check if connection is open to prevent errors
            {
                con.Open();
            }
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;

            //books
            cmd.CommandText = @"INSERT INTO books_tbl(Book_ID, Title, BookType, AuthorFirstName, AuthorLastName, Genre, NumOfCopies) VALUES
                (100, 'WW2','paper', 'John', 'Ve', 'History', 1),
                (101, 'WW2','digital', 'Reem', 'Somenya', 'History', -1),
                (102, 'Java Lessons','paper', 'Robin', 'Sam', 'Tech', 11),
                (103, 'Machine Learning','digital', 'Sera', 'Voiski', 'Tech', 2), 
                (104, 'Dead Sea','paper', 'Rena', 'Jackson', 'Geography', 5),
                (105, 'Hidden In Mars','paper', 'Rami', 'Shein', 'Sci-Fi', -1)";

            cmd.ExecuteNonQuery();

            //subscribers
            cmd.CommandText = @"INSERT INTO subscribers_tbl(Sub_ID, FirstName, LastName, CurrLoanNum) VALUES
                (204443162, 'Ahmad','Younis', 0),
                (200025111, 'Sena','Kaf', 0),
                (111222333, 'Robi','Beinze', 0),
                (333555111, 'Jerry','Sak', 0),
                (888444012, 'Michael','Fen', 0)";

            cmd.ExecuteNonQuery();
            Console.WriteLine("Data initialized successfully");

            con.Close();
        }


        protected void GoToAddNewBook_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/AddBookForm.aspx");
        }

        protected void GoToAddNewSub_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/AddSubForm.aspx");
        }

        protected void GoToLoanBook_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/LoanBookForm.aspx");
        }

        protected void GoToReturnBook_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/ReturnBookForm.aspx");
        }

        protected void GoToShowBookDtls_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/ShowBookDtlsForm.aspx");
        }

        protected void GoToPrintGenreBooks_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/PrintBookByGenreForm.aspx");
        }

        protected void GoToShowSubBookList_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/ShowSubBookListForm.aspx");
        }
    }
}
