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
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();

        }
        public Form3(string[] arr)
        {
            InitializeComponent();
            txtUserName.Text = arr[0];
            txtUserPassword.Text = arr[1];
            txtDonorName.Text = arr[0];

            string[] bGroup = new string[8];
            bGroup[0] = "A+";
            bGroup[1] = "O+";
            bGroup[2] = "B+";
            bGroup[3] = "AB+";
            bGroup[4] = "A-";
            bGroup[5] = "O-";
            bGroup[6] = "B-";
            bGroup[7] = "AB-";

            cboBloodGroup.DataSource = bGroup;
            cboDonorBlood.DataSource = bGroup;
            cboGroup.DataSource = bGroup;
            cboSearch.DataSource = bGroup;

            //address writing
            string details = "Office Address: 7/5 Aurongzeb Road, Mohammadpur, Dhaka.";

            rtbAddress.Text = details;
            rtbAddress2.Text = details;
        }

        private void btnShowInfo_Click(object sender, EventArgs e)
        {
           // label8.Text = "Age:";
            //label8.Refresh();

            label10.Text = "PhoneNo:";
            label10.Refresh();

            label11.Text = "Address:";
            label11.Refresh();
            //Initiating SQL Connection:
            SqlConnection con = new SqlConnection();

            con.ConnectionString = "data source = rasel; database = BloodmanagementSystem; " +
                                  "integrated security = SSPI";
            //Generating SQL Query
            string query = "Select * from UserInfo where UserName ='" + txtUserName.Text + "'and Password='" + txtUserPassword.Text + "'";

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

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            string bloodGroup = cboBloodGroup.SelectedItem.ToString();
            string searchGroup = cboSearch.SelectedItem.ToString();
            DateTime DOB = dateTimePicker2.Value;

            if (!string.IsNullOrEmpty(txtUserName.Text) && !string.IsNullOrEmpty(txtUserPassword.Text) && !string.IsNullOrEmpty(txtPhoneNo.Text) && !string.IsNullOrEmpty(txtAddress.Text))
            {
                //Initiating SQL Connection:
                SqlConnection con = new SqlConnection();

                con.ConnectionString = "data source = rasel; database = BloodmanagementSystem; " +
                                      "integrated security = SSPI";
                string sql = "UPDATE UserInfo SET BloodGroup = " + "'" + bloodGroup + "'" + "," +
                             " DateOfBirth = " + "'" + DOB + "'" + "," +
                             " PhoneNo = " + "'" + txtPhoneNo.Text + "'" + "," +
                             " Address = " + "'" + txtAddress.Text + "'" +
                             " where UserName = " + "'" + txtUserName.Text + "'" +
                             " and Password = " + "'" + txtUserPassword.Text + "'";

                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    //Opening the connection:
                    con.Open();

                    //cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();


                    //Disconnect
                    con.Close();

                    
                }

                MessageBox.Show("Information update successfully");
            }
            else
            {
                MessageBox.Show("More information required!");
            }
        }

        private void btnDonate_Click(object sender, EventArgs e)
        {
            string gender = string.Empty;
            gender = rbMale.Checked ? "Male" : "Female";
            string bloodGroup = cboDonorBlood.SelectedItem.ToString();
            string age = txtDonorAge.Text;
            DateTime Today = DateTime.Now;
            string ReceivedStatus = "Received";
            //DateTime donationDate = dateTimePicker1.Value;

            //insure 30 days difference between two Donation date

            SqlConnection con2 = new SqlConnection("Data Source=Rasel;Initial Catalog=BloodmanagementSystem;Integrated Security=True");

            SqlCommand cmd2 = new SqlCommand("select * from Donor where DonorName='" + txtDonorName.Text + "' and BloodGroup= '" + bloodGroup + "' and Status= '" + ReceivedStatus + "'", con2);
            SqlDataAdapter sda = new SqlDataAdapter(cmd2);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string date = dt.Rows[i]["DonationDate"].ToString();
                    DateTime dDate = Convert.ToDateTime(date);

                    TimeSpan tspan = Today - dDate;
                    int differenceInDays = tspan.Days;
                    if (differenceInDays >= 30)
                    {
                        //

                        //user informations
                        string donorName = txtDonorName.Text;
                        //string bloodGroup = cboBloodGroup.SelectedItem.ToString(); string age = txtAge.Text;
                        string phoneNo = txtDonorPhoneNo.Text;
                        string address = txtDonorAddress.Text;
                        string status = "not Received";




                        if (!string.IsNullOrEmpty(txtDonorName.Text) && !string.IsNullOrEmpty(txtDonorAge.Text) && !string.IsNullOrEmpty(txtDonorPhoneNo.Text) && !string.IsNullOrEmpty(txtDonorAddress.Text) && !string.IsNullOrEmpty(gender))
                        {
                            //Initiating SQL Connection:
                            SqlConnection con = new SqlConnection();

                            con.ConnectionString = "data source = rasel; database = BloodmanagementSystem; " +
                                                  "integrated security = SSPI";

                            //Generating SQL Query into Donor table
                            string sql = "INSERT INTO Donor(DonorName,BloodGroup,Age,PhoneNo,Address,Gender,DonationDate,Status)" +
                                " VALUES(" + "'" + donorName + "'" + "," + "'" + bloodGroup + "'" + "," + "'" + age + "'" + "," + "'" + phoneNo + "'" + "," + "'" + address + "'" + "," + "'" + gender + "'" + "," + "'" + Today + "'" + "," + "'" + status + "'" + ")";

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

                            ////Generating SQL Query into Available table
                            //string sql1 = "INSERT INTO Available(DonorName,BloodGroup,Age,PhoneNo,Address,Gender,DonationDate,ExpiredDate)" +
                            //    " VALUES(" + "'" + donorName + "'" + "," + "'" + bloodGroup + "'" + "," + "'" + age + "'" + "," + "'" + phoneNo + "'" + "," + "'" + address + "'" + "," + "'" + gender + "'" + "," + "'" + Today + "'" + "," + "'" + Today.AddDays(30) + "'" + ")";

                            //// connection open and close for Available table
                            //using (SqlCommand cmd1 = new SqlCommand(sql1, con))
                            //{
                            //    //Opening the connection:
                            //    con.Open();

                            //    //cmd.CommandType = CommandType.Text;
                            //    cmd1.ExecuteNonQuery();

                            //    //Disconnect
                            //    con.Close();
                            //}

                            MessageBox.Show("Donation Successful!");


                        }
                        else
                        {
                            MessageBox.Show("More information required!");
                        }
                    }
                    else
                    {
                        MessageBox.Show("30 Days is not passed since Last Blood Donation Date");
                    }
                }
            }
            //if available blood is empty
            else
            {
                //user informations
                string donorName = txtDonorName.Text;
                //string bloodGroup = cboBloodGroup.SelectedItem.ToString(); string age = txtAge.Text;
                string phoneNo = txtDonorPhoneNo.Text;
                string address = txtDonorAddress.Text;
                string status = "Not Received";



                if (!string.IsNullOrEmpty(txtDonorName.Text) && !string.IsNullOrEmpty(txtDonorAge.Text) && !string.IsNullOrEmpty(txtDonorPhoneNo.Text) && !string.IsNullOrEmpty(txtDonorAddress.Text) && !string.IsNullOrEmpty(gender))
                {
                    //Initiating SQL Connection:
                    SqlConnection con = new SqlConnection();

                    con.ConnectionString = "data source = rasel; database = BloodmanagementSystem; " +
                                          "integrated security = SSPI";

                    //Generating SQL Query into Donor table
                    string sql = "INSERT INTO Donor(DonorName,BloodGroup,Age,PhoneNo,Address,Gender,DonationDate,Status)" +
                        " VALUES(" + "'" + donorName + "'" + "," + "'" + bloodGroup + "'" + "," + "'" + age + "'" + "," + "'" + phoneNo + "'" + "," + "'" + address + "'" + "," + "'" + gender + "'" + "," + "'" + Today + "'" + "," + "'" + status + "'" + ")";

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

                    ////Generating SQL Query into Available table
                    //string sql1 = "INSERT INTO Available(DonorName,BloodGroup,Age,PhoneNo,Address,Gender,DonationDate,ExpiredDate)" +
                    //    " VALUES(" + "'" + donorName + "'" + "," + "'" + bloodGroup + "'" + "," + "'" + age + "'" + "," + "'" + phoneNo + "'" + "," + "'" + address + "'" + "," + "'" + gender + "'" + "," + "'" + Today + "'" + "," + "'" + Today.AddDays(30) + "'" + ")";

                    //// connection open and close for Available table
                    //using (SqlCommand cmd1 = new SqlCommand(sql1, con))
                    //{
                    //    //Opening the connection:
                    //    con.Open();

                    //    //cmd.CommandType = CommandType.Text;
                    //    cmd1.ExecuteNonQuery();

                    //    //Disconnect
                    //    con.Close();
                    //}

                    MessageBox.Show("Donation Successful!");


                }
                else
                {
                    MessageBox.Show("More information required!");
                }
                //
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            

                txtUserName.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                cboBloodGroup.SelectedItem = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                string date = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            //
            
                dateTimePicker2.Value = Convert.ToDateTime(date);
            //
                txtPhoneNo.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
                txtAddress.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            

        }

       private void btnSearch_Click(object sender, EventArgs e)
        {
            //label8.Text = "Age";
            //label8.Refresh();

            label20.Text = "Donor Phone No:";
            label20.Refresh();

            //label11.Text = "Address:";
            //label11.Refresh();

            //Initiating SQL Connection:
            SqlConnection con = new SqlConnection();
            DataTable dt = new DataTable();
            DateTime date = DateTime.Now;
           

            con.ConnectionString = "data source = rasel; database = BloodmanagementSystem; " +
                                  "integrated security = SSPI";
            string group = cboSearch.SelectedItem.ToString();
            //Generating SQL Query
            string query = "Select * from Available where BloodGroup ='" + group + "'and ExpiredDate>='" + date + "'";

            SqlCommand command = new SqlCommand(query, con);

            SqlDataAdapter sda = new SqlDataAdapter(command);
            sda.Fill(dt);



            if (dt.Rows.Count > 0)
                {
                btnRequest.Visible = true;
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
                    //dataGridView1.Visible = true;
                }

                else
                {
                //dataGridView1.Visible = false;
                btnRequest.Visible = false;
                dataGridView2.DataSource = null;
                txtDonor.Clear();
                txtPhone.Clear();
                    MessageBox.Show("Blood is Not Available.");
                }
            
        }
        
        private void btnLogOut_Click(object sender, EventArgs e)
        {
            //for logout operation
            Form1 f = new Form1();
            f.Show();
            this.Hide();
        }

        private void btnRequest_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtDonor.Text) && !string.IsNullOrEmpty(txtPhone.Text))
            {
                //
                SqlConnection con2 = new SqlConnection("Data Source=Rasel;Initial Catalog=BloodmanagementSystem;Integrated Security=True");

                SqlCommand cmd2 = new SqlCommand("select * from UserInfo where UserName='" + txtUserName.Text + "' and Password= '" + txtUserPassword.Text + "'", con2);
                SqlDataAdapter sda2 = new SqlDataAdapter(cmd2);
                DataTable dt2 = new DataTable();
                sda2.Fill(dt2);
                if (dt2.Rows.Count > 0)
                {
                    for (int i = 0; i < dt2.Rows.Count; i++)
                    {

                        string phoneNo = dt2.Rows[i]["PhoneNo"].ToString();

                        //checking if request is already done for same bloodgroup and same donor or not
                        string searchGroup = cboGroup.SelectedItem.ToString();
                        SqlConnection con1 = new SqlConnection("Data Source=Rasel;Initial Catalog=BloodmanagementSystem;Integrated Security=True");


                        SqlCommand cmd1 = new SqlCommand("select * from Request where DonorName='" + txtDonor.Text + "' and BloodGroup= '" + searchGroup + "'and RequesterName='" + txtUserName.Text + "'and RequesterPhoneNo='" + phoneNo + "'", con1);
                        SqlDataAdapter sda1 = new SqlDataAdapter(cmd1);
                        DataTable dt1 = new DataTable();
                        sda1.Fill(dt1);
                        if (dt1.Rows.Count > 0)
                        {
                            for (int j = 0; j < dt1.Rows.Count; j++)
                            {
                                string donorName = dt1.Rows[j]["DonorName"].ToString();
                                string bloodGroup = dt1.Rows[j]["BloodGroup"].ToString();
                                string requesterName = dt1.Rows[j]["RequesterName"].ToString();
                                string requesterPhoneNo = dt1.Rows[j]["RequesterPhoneNo"].ToString();

                                if (donorName != txtDonorName.Text && bloodGroup != searchGroup && requesterName != txtUserName.Text && requesterPhoneNo != phoneNo)
                                {
                                    //

                                    //Initiating SQL Connection:
                                    SqlConnection con = new SqlConnection();

                                    con.ConnectionString = "data source = rasel; database = BloodmanagementSystem; " +
                                                          "integrated security = SSPI";
                                    //string searchGroup = cboSearch.SelectedItem.ToString();
                                    DateTime requestDate = DateTime.Now;
                                    Nullable<DateTime> approvalDate = null;
                                    //DateTime date = null;

                                    string approvalStatus = "Not Approved";
                                    //Generating SQL Query into Donor table
                                    string sql = "INSERT INTO Request(DonorName,BloodGroup,DonorPhoneNo,RequesterName,RequesterPhoneNo,RequestedDate,ApprovalDate,ApprovalStatus)" +
                                        " VALUES(" + "'" + txtDonor.Text + "'" + "," + "'" + searchGroup + "'" + "," + "'" + txtPhone.Text + "'" + "," + "'" + txtUserName.Text + "'" + "," + "'" + phoneNo + "'" + "," + "'" + requestDate + "'" + "," + "'" + approvalDate + "'" + "," + "'" + approvalStatus + "'" + ")";

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
                                    MessageBox.Show("Request has been Sent.");
                                }
                                else
                                {
                                    MessageBox.Show("Failed! Already requested for same blood");
                                }
                            }
                        }
                        //if Request List is empty
                        else
                        {
                            //
                            //Initiating SQL Connection:
                            SqlConnection con = new SqlConnection();

                            con.ConnectionString = "data source = rasel; database = BloodmanagementSystem; " +
                                                  "integrated security = SSPI";
                            //string searchGroup = cboSearch.SelectedItem.ToString();
                            DateTime requestDate = DateTime.Now;
                            Nullable<DateTime> approvalDate = null;
                            //DateTime date = null;

                            string approvalStatus = "Not Approved";
                            //Generating SQL Query into Donor table
                            string sql = "INSERT INTO Request(DonorName,BloodGroup,DonorPhoneNo,RequesterName,RequesterPhoneNo,RequestedDate,ApprovalDate,ApprovalStatus)" +
                                " VALUES(" + "'" + txtDonor.Text + "'" + "," + "'" + searchGroup + "'" + "," + "'" + txtPhone.Text + "'" + "," + "'" + txtUserName.Text + "'" + "," + "'" + phoneNo + "'" + "," + "'" + requestDate + "'" + "," + "'" + approvalDate + "'" + "," + "'" + approvalStatus + "'" + ")";

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
                            MessageBox.Show("Request has been Sent.");
                            //
                        }
                    }
                    //
                }
            }
            else
            {
                MessageBox.Show("Failed! Information Required.");
            }
        }

        private void btnStatus_Click(object sender, EventArgs e)
        {
            //label8.Text = "PhoneNo:";
            //label8.Refresh();

            label20.Text = "Requester Name:";
            label20.Refresh();

            //label11.Text = "PhoneNo:";
            //label11.Refresh();
            //

            //for taking phone no of user
            SqlConnection con2 = new SqlConnection("Data Source=Rasel;Initial Catalog=BloodmanagementSystem;Integrated Security=True");

            SqlCommand cmd2 = new SqlCommand("select * from UserInfo where UserName='" + txtUserName.Text + "' and Password= '" + txtUserPassword.Text + "'", con2);
            SqlDataAdapter sda = new SqlDataAdapter(cmd2);
            DataTable dt = new DataTable();
            sda.Fill(dt);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string phoneNo = dt.Rows[i]["PhoneNo"].ToString();
                //
                //checking if any requeste is done yet or not
                SqlConnection con1 = new SqlConnection("Data Source=Rasel;Initial Catalog=BloodmanagementSystem;Integrated Security=True");


                SqlCommand cmd1 = new SqlCommand("select * from Request where RequesterName='" + txtUserName.Text + "' and RequesterPhoneNo= '" + phoneNo + "'", con1);
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
                    string query = "Select * from Request where RequesterName ='" + txtUserName.Text + "'and RequesterPhoneNo='" + phoneNo + "'";

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

                else
                {
                    dataGridView2.DataSource = null;
                    txtDonor.Clear();
                    txtPhone.Clear();
                    MessageBox.Show("You haven't made any Request Yet.");
                }
            }
        }

        private void btnDeleteAccount_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection();

            con.ConnectionString = "data source = rasel; database = BloodmanagementSystem; " +
                                  "integrated security = SSPI";
            //deleting user from UserInfo table
            string sql = "DELETE FROM UserInfo where UserName ='" + txtUserName.Text + "' and Password= '" + txtUserPassword.Text + "'";

            using (SqlCommand cmd = new SqlCommand(sql, con))
            {
                //    //Opening the connection:
                con.Open();

                //cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();

                //    //Disconnect
                con.Close();
            }
            //deleting from Login Table
            
            
            string sql1 = "DELETE FROM Login where UserName ='" + txtUserName.Text + "' and Password= '" + txtUserPassword.Text + "'";

            using (SqlCommand cmd1 = new SqlCommand(sql1, con))
            {
                //    //Opening the connection:
                con.Open();

                //cmd.CommandType = CommandType.Text;
                cmd1.ExecuteNonQuery();

                //    //Disconnect
                con.Close();
            }
            MessageBox.Show("your Account is Deleted from server.");
            Form1 f = new Form1();
            f.Show();
            this.Hide();
        }

       private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtDonor.Text = dataGridView2.CurrentRow.Cells[0].Value.ToString();
            cboGroup.SelectedItem = dataGridView2.CurrentRow.Cells[1].Value.ToString();
            txtPhone.Text = dataGridView2.CurrentRow.Cells[3].Value.ToString();
        }
    }
}
