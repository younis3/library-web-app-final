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
    public partial class AddSubForm : System.Web.UI.Page
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


        protected void buttonAddSub_Click(object sender, EventArgs e)
        {
            // Connect to DB
            string cs = ConfigurationManager.ConnectionStrings["myConnectionString"].ConnectionString;      //connection string and DB name in web.config file
            //string cs = @"Server=localhost\SQLEXPRESS;Database=ahmad_test;Trusted_Connection=True;";
            SqlConnection con = new SqlConnection(cs);
            //CheckConnectionVersion(con);

            //get requested info
            string subID = txtSubID.Text;
            int sub_id;
            try
            {
                sub_id = int.Parse(subID);
            }
            catch
            {
                lblMsg.Text = "Error: Please type valid ID";
                return;
            }

            string subFirstName = textSubFirstName.Text;
            string subLastName = textSubLastName.Text;

            //add to DB
            AddSubscriber(sub_id, subFirstName, subLastName, con);
        }

        protected void buttonBacktoMM_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Default.aspx");
        }


        protected void AddSubscriber(int sub_id, string subFirstName, string subLastName, SqlConnection con)
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

            if (subCount > 0)     //subscriber exists
            {
                lblMsg.Text = "Subscriber already exists";
            }
            else    //subscriber doesn't exist. Add subscriber to library DB
            {
                cmd.CommandText = $@"INSERT INTO subscribers_tbl(Sub_ID, FirstName, LastName, CurrLoanNum) VALUES
                ({sub_id}, '{subFirstName}','{subLastName}', 0)";

                cmd.ExecuteNonQuery();
                lblMsg.Text = "Subscriber added successfully";
                lblMsg.Attributes.Add("class", "success");
            }
            con.Close();
        }
    }
}
