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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            string[] userType = new string[2];
            userType[0] = "admin";
            userType[1] = "user";

            cboUserType.DataSource = userType;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {

            if (!string.IsNullOrEmpty(txtUser.Text) && !string.IsNullOrEmpty(txtPassword.Text))
            {

                SqlConnection con = new SqlConnection("Data Source=Rasel;Initial Catalog=BloodmanagementSystem;Integrated Security=True");

                SqlCommand cmd = new SqlCommand("select * from Login where UserName='" + txtUser.Text + "' and Password= '" + txtPassword.Text + "'", con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);

                string type = cboUserType.SelectedItem.ToString();


                string[] arr = new string[2];
                arr[0] = txtUser.Text;
                arr[1] = txtPassword.Text;

                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (dt.Rows[i]["UserType"].ToString() == type)
                        {
                            MessageBox.Show("You are login as " + dt.Rows[i][2]);
                            if (cboUserType.SelectedIndex == 0)
                            {
                                Form2 f = new Form2(arr);
                                f.Show();
                                this.Hide();
                            }
                            else
                            {
                                Form3 ff = new Form3(arr);
                                ff.Show();
                                this.Hide();
                            }
                        }
                        else
                        {
                            MessageBox.Show("Incorrect User Name or Password ");
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Incorrect User Name or Password ");
                }

            }
            else
            {
                MessageBox.Show("UserName or Password Requirde");
            }
        }

        private void btnSignUp_Click(object sender, EventArgs e)
        {
            Form4 f = new Form4();
            f.Show();
            this.Hide();
        }
    }
}
