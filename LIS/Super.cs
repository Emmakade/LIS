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

namespace LIS
{
    public partial class Super : Form
    {
        string cs = "URI=file:" + Application.StartupPath + "\\lifo_table.db";

        SQLiteConnection conn = null;
        SQLiteCommand cmd = null;
        public Super()
        {
            InitializeComponent();
        }

        private void btnReg_Click(object sender, EventArgs e)
        {
            try
            {
                if (!(txtUsername.Text.Equals(string.Empty)) && !(txtPassword.Text.Equals(string.Empty)) && !(txtName.Text.Equals(string.Empty)))
                {
                    using (conn = new SQLiteConnection(cs))
                    {
                        conn.Open();
                        using (cmd = new SQLiteCommand(conn))
                        {
                            cmd.CommandText = "insert into staff (name,username,password,admin_level) values(@name,@username,@password,@admin_level)";
                            //cmd = new MySqlCommand(qry, conn);


                            cmd.Parameters.AddWithValue("@name", txtName.Text);
                            cmd.Parameters.AddWithValue("@username", txtUsername.Text);
                            cmd.Parameters.AddWithValue("@password", txtPassword.Text);
                            cmd.Parameters.AddWithValue("@admin_level", 2);

                            cmd.Prepare();
                            cmd.ExecuteNonQuery();
                            conn.Close();

                            MessageBox.Show("Super Admin \r\n" + "Account created Successfully");

                            Form1 fm = new Form1();
                            this.Hide();
                            fm.Show();
                        }
                       
                    }
                    
                }
                else
                {
                    conn.Close(); cmd.Dispose(); conn.Dispose();
                    MessageBox.Show("Check required field and fill", "Error", MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    return;
                }
            }
            catch (Exception dd) { MessageBox.Show(dd.Message); }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
       
        private void Super_Load(object sender, EventArgs e)
        {
            txtUsername.Text = "admin";
            txtUsername.ReadOnly = true;
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
