using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace C_sharp_final_project
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();

        }
        public Form2(string[] arr)
        {
            InitializeComponent();
            txtAdminUserName.Text = arr[0];
            string adminPassword = arr[1];

            //string[] userType = new string[1];
            //userType[0] = "admin";

            //cboAdmin.DataSource = userType;

        }

        private void btnShow_Click(object sender, EventArgs e)
        {
            string Usertype = "user";
            //Initiating SQL Connection:
            SqlConnection con = new SqlConnection();

            con.ConnectionString = "data source = rasel; database = BloodmanagementSystem; " +
                                  "integrated security = SSPI";

            //Generating SQL Query
            string query = "Select UserName,Password from Login where UserType='" + Usertype + "'";

            SqlCommand command = new SqlCommand(query, con);

            //Opening the connection for login table:
            con.Open();

            //Execute SQL Query:
            SqlDataReader reader = command.ExecuteReader();

            //Binding reader to source
            BindingSource source = new BindingSource();
            source.DataSource = reader;

            //closing connection for login table:
            con.Close();

            //binding source to grid view control
            dataGridView1.DataSource = source;

        }

        private void btnShowUser_Click(object sender, EventArgs e)
        {
            string Usertype = "user";
            //Initiating SQL Connection:
            SqlConnection con = new SqlConnection();

            con.ConnectionString = "data source = rasel; database = BloodmanagementSystem; " +
                                  "integrated security = SSPI";

            //Generating SQL Query
            string query = "Select UserName,BloodGroup,DOB,PhoneNo,Address,Password from UserInfo where UserType='" + Usertype + "'";

            SqlCommand command = new SqlCommand(query, con);

            //Opening the connection for login table:
            con.Open();

            //Execute SQL Query:
            SqlDataReader reader = command.ExecuteReader();

            //Binding reader to source
            BindingSource source = new BindingSource();
            source.DataSource = reader;

            //closing connection for login table:
            con.Close();

            //binding source to grid view control
            dataGridView1.DataSource = source;

        }

        private void btnDeleteUser_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtDeleteName.Text) && !string.IsNullOrEmpty(txtInfo.Text))
            {
                SqlConnection con = new SqlConnection();

                con.ConnectionString = "data source = rasel; database = BloodmanagementSystem; " +
                                      "integrated security = SSPI";

                //deleting user from UserInfo table
                string sql = "DELETE FROM UserInfo where UserName ='" + this.txtDeleteName.Text + "'";

                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    //    //Opening the connection:
                    con.Open();

                    //cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();

                    //    //Disconnect
                    con.Close();
                }
                //string Admin = admin.Text;
                //string Admin = cboAdmin.SelectedItem.ToString();
                //delete user from login table
                //int i = 0;
                //DataTable dt = new DataTable();


                string sql1 = "DELETE FROM Login where UserName ='" + this.txtDeleteName.Text + "'";

                using (SqlCommand cmd1 = new SqlCommand(sql1, con))
                {
                    //    //Opening the connection:
                    con.Open();

                    //cmd.CommandType = CommandType.Text;
                    cmd1.ExecuteNonQuery();
                    //i = 1;
                    //    //Disconnect
                    con.Close();
                }

                MessageBox.Show("User deleted successfully");


            }
            else
            {
                MessageBox.Show("Failed! More Information Required.");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //for logout operation
            Form1 f = new Form1();
            f.Show();
            this.Hide();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtDeleteName.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            txtInfo.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
        }

        private void btnDonateHistory_Click(object sender, EventArgs e)
        {
            //Initiating SQL Connection:
            SqlConnection con = new SqlConnection();

            con.ConnectionString = "data source = rasel; database = BloodmanagementSystem; " +
                                  "integrated security = SSPI";

            //Generating SQL Query
            string query = "Select * from Donor";

            SqlCommand command = new SqlCommand(query, con);

            //Opening the connection for login table:
            con.Open();

            //Execute SQL Query:
            SqlDataReader reader = command.ExecuteReader();

            //Binding reader to source
            BindingSource source = new BindingSource();
            source.DataSource = reader;

            //closing connection for login table:
            con.Close();

            //binding source to grid view control
            dataGridView2.DataSource = source;
        }

        private void btnDeleteDonor_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtName.Text) && !string.IsNullOrEmpty(txtBlood.Text) && !string.IsNullOrEmpty(txtPhone.Text))
            {
                SqlConnection con = new SqlConnection();

                con.ConnectionString = "data source = rasel; database = BloodmanagementSystem; " +
                                      "integrated security = SSPI";

                //deleting user from UserInfo table
                string sql = "DELETE FROM Donor where DonorName ='" + txtName.Text + "'and BloodGroup='" + txtBlood.Text + "'and PhoneNo='" + txtPhone.Text + "'";

                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    //    //Opening the connection:
                    con.Open();

                    //cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();

                    //    //Disconnect
                    con.Close();
                }
                MessageBox.Show("Delete is Successful");
            }
            else
            {
                MessageBox.Show("Failed!  Select The ROW to Delete");
            }
        }

        private void btnAvailable_Click(object sender, EventArgs e)
        {
            //Initiating SQL Connection:
            SqlConnection con = new SqlConnection();

            con.ConnectionString = "data source = rasel; database = BloodmanagementSystem; " +
                                  "integrated security = SSPI";

            //Generating SQL Query
            string query = "Select * from Available";

            SqlCommand command = new SqlCommand(query, con);

            //Opening the connection for login table:
            con.Open();

            //Execute SQL Query:
            SqlDataReader reader = command.ExecuteReader();

            //Binding reader to source
            BindingSource source = new BindingSource();
            source.DataSource = reader;

            //closing connection for login table:
            con.Close();

            //binding source to grid view control
            dataGridView3.DataSource = source;
        }

        private void btnDeleteAvailable_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtDeleteDonorName.Text) && !string.IsNullOrEmpty(txtDeleteBlood.Text) && !string.IsNullOrEmpty(txtDeletePhone.Text))
            {
                SqlConnection con = new SqlConnection();

                con.ConnectionString = "data source = rasel; database = BloodmanagementSystem; " +
                                      "integrated security = SSPI";

                //deleting user from UserInfo table
                string sql = "DELETE FROM Available where DonorName ='" + txtDeleteDonorName.Text + "'and BloodGroup='" + txtDeleteBlood.Text + "'and PhoneNo='" + txtDeletePhone.Text + "'";

                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    //    //Opening the connection:
                    con.Open();

                    //cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();

                    //    //Disconnect
                    con.Close();
                }
                MessageBox.Show("Delete is Successful");
            }
            else
            {
                MessageBox.Show("Failed! First Select the ROW to Delete.");
            }
        }

        private void btnCheckRequest_Click(object sender, EventArgs e)
        {
            //
            //checking if any requeste is done yet or not
            string approvalStatus = "Not Approved";
            SqlConnection con1 = new SqlConnection("Data Source=Rasel;Initial Catalog=BloodmanagementSystem;Integrated Security=True");

            SqlCommand cmd1 = new SqlCommand("select * from Request where ApprovalStatus='" + approvalStatus +"'", con1);
            SqlDataAdapter sda1 = new SqlDataAdapter(cmd1);
            DataTable dt1 = new DataTable();
            sda1.Fill(dt1);
            if (dt1.Rows.Count > 0)
            {
                btnApprove.Visible = true;
                //
                //Initiating SQL Connection:

                SqlConnection con = new SqlConnection();

                con.ConnectionString = "data source = rasel; database = BloodmanagementSystem; " +
                                      "integrated security = SSPI";

                //Generating SQL Query
                string query = "Select * from Request where ApprovalStatus='" + approvalStatus + "'";

                SqlCommand command = new SqlCommand(query, con);

                //Opening the connection for login table:
                con.Open();

                //Execute SQL Query:
                SqlDataReader reader = command.ExecuteReader();

                //Binding reader to source
                BindingSource source = new BindingSource();
                source.DataSource = reader;

                //closing connection for login table:
                con.Close();

                //binding source to grid view control
                dataGridView4.DataSource = source;
            }

            else
            {
                //dataGridView4.Rows.Clear();
                //dataGridView4.Refresh();
                dataGridView4.DataSource = null;
                txtDonor.Clear();
                txtBloodGroup.Clear();
                txtRequesterName.Clear();

                MessageBox.Show("Request List is empty.");
            }
            //
        }

        private void btnApprove_Click(object sender, EventArgs e)
        {
            //
            //Initiating SQL Connection:

            SqlConnection con = new SqlConnection();

            con.ConnectionString = "data source = rasel; database = BloodmanagementSystem; " +
                                  "integrated security = SSPI";

            //string searchGroup = cboSearch.SelectedItem.ToString();
            //DateTime requestDate = DateTime.Now;
            DateTime approvalDate = DateTime.Now;
            string approvalStatus = "Approved";
            string donorName = txtDonor.Text;
            String bloodGroup = txtBloodGroup.Text;
            string requesterName = txtRequesterName.Text;

            if (!string.IsNullOrEmpty(donorName) && !string.IsNullOrEmpty(bloodGroup) && !string.IsNullOrEmpty(requesterName))
            {

                //Generating SQL Query into Donor table
                string sql = "UPDATE Request SET ApprovalDate = " + "'" + approvalDate + "'" + "," +
                          "ApprovalStatus = " + "'" + approvalStatus + "'" +
                          "where DonorName = " + "'" + donorName + "'" +
                          "and BloodGroup = " + "'" + bloodGroup + "'" +
                          "and RequesterName = " + "'" + requesterName + "'";

                // connection open and close for Donor table
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    //Opening the connection:
                    con.Open();

                    //cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();

                    //Disconnect
                    con.Close();
                }


                //After approving delete blood from available Table
               /* string sql1 = "DELETE FROM Available where DonorName ='" + this.txtDonor.Text + "'and BloodGroup='" + txtBloodGroup.Text + "'";

                using (SqlCommand cmd1 = new SqlCommand(sql1, con))
                {
                    //    //Opening the connection:
                    con.Open();

                    //cmd.CommandType = CommandType.Text;
                    cmd1.ExecuteNonQuery();

                    //    //Disconnect
                    con.Close();
                }*/

                MessageBox.Show("Request has been Approved!.");
            }
            else
            {
                MessageBox.Show("More Information needed.");
            }
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtName.Text = dataGridView2.CurrentRow.Cells[0].Value.ToString();

            txtBlood.Text = dataGridView2.CurrentRow.Cells[1].Value.ToString();
            txtPhone.Text = dataGridView2.CurrentRow.Cells[3].Value.ToString();
        }

        private void dataGridView3_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtDeleteDonorName.Text = dataGridView3.CurrentRow.Cells[0].Value.ToString();
            txtDeleteBlood.Text = dataGridView3.CurrentRow.Cells[1].Value.ToString();
            txtDeletePhone.Text = dataGridView3.CurrentRow.Cells[3].Value.ToString();
        }

        private void dataGridView4_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtDonor.Text = dataGridView4.CurrentRow.Cells[0].Value.ToString();
            txtBloodGroup.Text = dataGridView4.CurrentRow.Cells[1].Value.ToString();
            txtRequesterName.Text = dataGridView4.CurrentRow.Cells[3].Value.ToString();
        }

        private void btnAdminInfo_Click(object sender, EventArgs e)
        {
            string Usertype = "admin";
            //Initiating SQL Connection:

            SqlConnection con = new SqlConnection();

            con.ConnectionString = "data source = rasel; database = BloodmanagementSystem; " +
                                  "integrated security = SSPI";

            //Generating SQL Query
            string query = "Select UserName,Password from Login where UserType='" + Usertype + "'";

            SqlCommand command = new SqlCommand(query, con);

            //Opening the connection for login table:
            con.Open();

            //Execute SQL Query:
            SqlDataReader reader = command.ExecuteReader();

            //Binding reader to source
            BindingSource source = new BindingSource();
            source.DataSource = reader;

            //closing connection for login table:
            con.Close();

            //binding source to grid view control
            dataGridView5.DataSource = source;
        }

        private void btnUpdateAdminInfo_Click(object sender, EventArgs e)
        {

            if (!string.IsNullOrEmpty(txtAdminUserName.Text) && !string.IsNullOrEmpty(txtAdminPassword.Text))
            {
                //Initiating SQL Connection:

                SqlConnection con = new SqlConnection();

                con.ConnectionString = "data source = rasel; database = BloodmanagementSystem; " +
                                      "integrated security = SSPI";

                string sql = "UPDATE Login SET Password = " + "'" + txtAdminPassword.Text + "'" +
                             " where UserName = " + "'" + txtAdminUserName.Text + "'";

                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    //Opening the connection:
                    con.Open();

                    //cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();


                    //Disconnect
                    con.Close();


                }

                MessageBox.Show("Password changed successfully");
            }
            else
            {
                MessageBox.Show("More information required!");
            }
        }

        private void dataGridView5_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtAdminUserName.Text = dataGridView5.CurrentRow.Cells[0].Value.ToString();
            txtAdminPassword.Text = dataGridView5.CurrentRow.Cells[1].Value.ToString();
        }

        private void AddToStorage_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtName.Text) && !string.IsNullOrEmpty(txtBlood.Text) && !string.IsNullOrEmpty(txtPhone.Text))
            {
                SqlConnection con = new SqlConnection("Data Source=Rasel;Initial Catalog=BloodmanagementSystem;Integrated Security=True");

                SqlCommand cmd1 = new SqlCommand("select * from Donor where DonorName='" + txtName.Text + "' and BloodGroup= '" + txtBlood.Text + "' and PhoneNo= '" + txtPhone.Text + "'", con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd1);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        string date = dt.Rows[i]["DonationDate"].ToString();
                        DateTime dDate = Convert.ToDateTime(date);
                        string age = dt.Rows[i]["Age"].ToString();
                        string address = dt.Rows[i]["Address"].ToString();
                        string gender = dt.Rows[i]["Gender"].ToString();

                        //Generating SQL Query into Available table
                        string sql = "INSERT INTO Available(DonorName,BloodGroup,Age,PhoneNo,Address,Gender,DonationDate,ExpiredDate)" +
                            " VALUES(" + "'" + txtName.Text + "'" + "," + "'" + txtBlood.Text + "'" + "," + "'" + age + "'" + "," + "'" + txtPhone.Text + "'" + "," + "'" + address + "'" + "," + "'" + gender + "'" + "," + "'" + dDate + "'" + "," + "'" + dDate.AddDays(30) + "'" + ")";

                        // connection open and close for Available table
                        using (SqlCommand cmd = new SqlCommand(sql, con))
                        {
                            //Opening the connection:
                            con.Open();

                            //cmd.CommandType = CommandType.Text;
                            cmd.ExecuteNonQuery();

                            //Disconnect
                            con.Close();
                        }
                        //
                        //Initiating SQL Connection:
                        //SqlConnection con2 = new SqlConnection();

                        //con2.ConnectionString = "data source = DESKTOP-T8L630M; database = Connection; " +
                        //"integrated security = SSPI";
                        string status = "Received";
                        string sql2 = "UPDATE Donor SET Status = " + "'" + status + "'" +
                                     " where DonorName = " + "'" + txtName.Text + "'" +
                                     " and BloodGroup = " + "'" + txtBlood.Text + "'" +
                                     " and PhoneNo = " + "'" + txtPhone.Text + "'";

                        using (SqlCommand cmd2 = new SqlCommand(sql2, con))
                        {
                            //Opening the connection:
                            con.Open();

                            //cmd.CommandType = CommandType.Text;
                            cmd2.ExecuteNonQuery();


                            //Disconnect
                            con.Close();


                        }

                        //

                        MessageBox.Show("Adding into Storage is Successful!");
                    }
                }
            }
            else
            {
                MessageBox.Show("Failed! Information Required.");
            }
        }

        private void btnDeleteRequest_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtDonor.Text) && !string.IsNullOrEmpty(txtBloodGroup.Text) && !string.IsNullOrEmpty(txtRequesterName.Text))
            {
                SqlConnection con = new SqlConnection();

                con.ConnectionString = "data source = rasel; database = BloodmanagementSystem; " +
                                      "integrated security = SSPI";

                //deleting user from UserInfo table
                string sql = "DELETE FROM Request where DonorName ='" + txtDonor.Text + "'and BloodGroup='" + txtBloodGroup.Text + "'and RequesterName='" + txtRequesterName.Text + "'";

                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    //    //Opening the connection:
                    con.Open();

                    //cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();

                    //    //Disconnect
                    con.Close();
                }
                MessageBox.Show("Delete is Successful");
            }
            else
            {
                MessageBox.Show("Failed! Information Required.");
            }
        }

        private void btnDeliveredList_Click(object sender, EventArgs e)
        {
            btnApprove.Visible = false;
            //
            //checking if any Delivery is done yet or not
            string approvalStatus = "Approved";
            SqlConnection con1 = new SqlConnection("Data Source=Rasel;Initial Catalog=BloodmanagementSystem;Integrated Security=True");

            SqlCommand cmd1 = new SqlCommand("select * from Request where ApprovalStatus='" + approvalStatus + "'", con1);
            SqlDataAdapter sda1 = new SqlDataAdapter(cmd1);
            DataTable dt1 = new DataTable();
            sda1.Fill(dt1);
            if (dt1.Rows.Count > 0)
            {
                //
                //Initiating SQL Connection:
                SqlConnection con = new SqlConnection();

                con.ConnectionString = "data source = rasel; database = BloodmanagementSystem; " +
                                      "integrated security = SSPI";
                //Generating SQL Query
                string query = "Select * from Request where ApprovalStatus='" + approvalStatus + "'";

                SqlCommand command = new SqlCommand(query, con);

                //Opening the connection for login table:
                con.Open();

                //Execute SQL Query:
                SqlDataReader reader = command.ExecuteReader();

                //Binding reader to source
                BindingSource source = new BindingSource();
                source.DataSource = reader;

                //closing connection for login table:
                con.Close();

                //binding source to grid view control
                dataGridView4.DataSource = source;
            }

            else
            {
                //dataGridView4.Rows.Clear();
                //dataGridView4.Refresh();
                dataGridView4.DataSource = null;
                txtDonor.Clear();
                txtBloodGroup.Clear();
                txtRequesterName.Clear();

                MessageBox.Show("delivered List is empty.");
            }
            //
        }
    }
}
