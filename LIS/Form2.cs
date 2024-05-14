using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;
using System.IO;

namespace LIS
{
    public partial class Form2 : Form
    {
        string cs = "URI=file:" + Application.StartupPath + "\\lifo_table.db";

        SQLiteConnection conn = null;
        SQLiteCommand cmd = null;

        public static string firstname;
        public Form2()
        {
            InitializeComponent();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnReg_Click(object sender, EventArgs e)
        {
            try
            {
                if (!(txtUsername.Text.Equals(string.Empty)) && !(txtPassword.Text.Equals(string.Empty)) && !(txtMatric.Text.Equals(string.Empty)))
                {
                    using (conn = new SQLiteConnection(cs))
                    {
                        conn.Open();

                        string qry = "insert into users(firstname,lastname,username,password,matric,phone,email) values(@firstname,@lastname,@username,@password,@matric,@phone,@email)";
                        using (cmd = new SQLiteCommand(qry, conn))
                        {
                            firstname = txtFirstname.Text;
                            cmd.Parameters.AddWithValue("@firstname", firstname);
                            cmd.Parameters.AddWithValue("@lastname", txtLastname.Text);
                            cmd.Parameters.AddWithValue("@username", txtUsername.Text);
                            cmd.Parameters.AddWithValue("@password", txtPassword.Text);
                            cmd.Parameters.AddWithValue("@matric", txtMatric.Text);
                            cmd.Parameters.AddWithValue("@phone", txtPhone.Text);
                            cmd.Parameters.AddWithValue("@email", txtEmail.Text);

                            cmd.Prepare();
                            cmd.ExecuteNonQuery();
                            conn.Close();

                            MessageBox.Show(firstname + "\r\n" + "Account created Successfully");
                            this.Close();
                        }
                    }
                    
                }
                else
                {
                    conn.Close(); cmd.Dispose();
                    MessageBox.Show("Check required field and fill", "Error", MessageBoxButtons.OK,
                        MessageBoxIcon.Error); this.Close();
                }
            }
            catch (Exception dd) { MessageBox.Show(dd.Message); }
        }

        private void btnVisiblePass_Click(object sender, EventArgs e)
        {
            if (txtPassword.UseSystemPasswordChar == true)
            {
                txtPassword.UseSystemPasswordChar = false;
            }
            else
            {
                txtPassword.UseSystemPasswordChar = true;
            }
        }
    }
}
