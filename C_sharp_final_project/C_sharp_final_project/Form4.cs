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
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();

            string[] group = new string[8];
            group[0] = "A+";
            group[1] = "O+";
            group[2] = "B+";
            group[3] = "AB+";
            group[4] = "A-";
            group[5] = "O-";
            group[6] = "B-";
            group[7] = "AB-";

            cboBloodGroup.DataSource = group;

            string[] userType = new string[1];
            userType[0] = "user";

            cboUserType.DataSource = userType;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string userName = txtUserName.Text;
            
            if (!string.IsNullOrEmpty(txtUserName.Text) && !string.IsNullOrEmpty(txtPassword.Text) && !string.IsNullOrEmpty(txtPhoneNo.Text) && !string.IsNullOrEmpty(txtAddress.Text))
            {
                //Initiating SQL Connection:
                SqlConnection con = new SqlConnection();

                con.ConnectionString = "data source = rasel; database = BloodmanagementSystem; " +
                                      "integrated security = SSPI";
                //user informations

                string bloodGroup = cboBloodGroup.SelectedItem.ToString();
                DateTime DOB = dateTimePicker1.Value;
                string phoneNo = txtPhoneNo.Text;
                string address = txtAddress.Text;
                string password = txtPassword.Text;
                string userType = cboUserType.SelectedItem.ToString();
                
                //for sending userName and Password to User Interface
                string[] arr = new string[2];
                arr[0] = txtUserName.Text;
                arr[1] = txtPassword.Text;

                //Generating SQL Query into user table
                string sql = "INSERT INTO UserInfo(UserName,BloodGroup,DOB,PhoneNo,Address,Password,UserType)" +
                    " VALUES(" + "'" + userName + "'" + "," + "'" + bloodGroup + "'" + "," + "'" + DOB + "'" + "," + "'" + phoneNo + "'" + "," + "'" + address + "'" + "," + "'" + password + "'" + "," + "'" + userType + "'" + ")";

                // connection open and close for user table
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    //Opening the connection:
                    con.Open();

                    //cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();

                    //Disconnect
                    con.Close();
                }

                //Generating SQL Query into login table
                string sql1 = "INSERT INTO Login(UserName,Password,UserType)" +
                    " VALUES(" + "'" + userName + "'" + "," + "'" + password + "'" + "," + "'" + userType + "'" + ")";

                // connection open and close for login table
                using (SqlCommand cmd1 = new SqlCommand(sql1, con))
                {
                    //Opening the connection:
                    con.Open();

                    //cmd.CommandType = CommandType.Text;
                    cmd1.ExecuteNonQuery();

                    //Disconnect
                    con.Close();
                }



                //show massage
                MessageBox.Show("Account created Successfully");

                Form3 f = new Form3(arr);
                f.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Failed! More Information Required.");
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            //for go Back to Login Page
            Form1 f = new Form1();
            f.Show();
            this.Hide();
        }
    }
}
