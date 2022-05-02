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
    public partial class ShowSubBookListForm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void buttonSubBookList_Click(object sender, EventArgs e)
        {
            // Connect to DB
            string cs = ConfigurationManager.ConnectionStrings["myConnectionString"].ConnectionString;      //connection string and DB name in web.config file
            //string cs = @"Server=localhost\SQLEXPRESS;Database=ahmad_test;Trusted_Connection=True;";
            SqlConnection con = new SqlConnection(cs);
            //CheckConnectionVersion(con);


            string inpt_id = textSubID.Text;
            int sub_id;
            try
            {
                sub_id = Convert.ToInt32(inpt_id);
            }
            catch
            {
                lblMsg.Text = "Error: Only numbers allowed for subscriber ID";
                return;
            }

            ShowSubBooks(sub_id, con);
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



        protected void ShowSubBooks(int subscriber_id, SqlConnection con)
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
            cmd.CommandText = $@"SELECT Count(*) FROM subscribers_tbl WHERE Sub_ID = {subscriber_id}";
            int subCount = 0;
            subCount = Convert.ToInt32(cmd.ExecuteScalar());

            if (subCount == 0)     //subscriber doesn't exist
            {
                lblMsg.Text = "Subscriber doesn't exist";
                con.Close();
                return;
            }

            string query = $@"SELECT books_tbl.Book_ID, books_tbl.Title, books_tbl.AuthorFirstName, books_tbl.AuthorLastName
                            FROM books_tbl, loans_tbl
                            WHERE loans_tbl.Sub_ID = {subscriber_id} AND loans_tbl.Book_ID = books_tbl.Book_ID ";

            //add results to dataset in order to iterate multiple rows
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = new SqlCommand(query, con);
            DataSet set = new DataSet("subBooks");
            int rowsNum = adapter.Fill(set, "books_tbl");   //fill dataset and return num of rows to check if it's empty or not

            if (rowsNum == 0)   //no results found in dataset
            {
                lblMsg.Text = "Subscriber doesn't own any books";
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

                    string str = $"Book ID: {book_id}, Title: {title}, Author: {first_name} {last_name}";
                    lblMsg.Text += "<br />" + str;
                }
            }
            con.Close();
        }



    }
}