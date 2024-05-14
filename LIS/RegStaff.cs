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
    public partial class RegStaff : Form
    {
        string cs = "URI=file:" + Application.StartupPath + "\\lifo_table.db";

        SQLiteConnection conn = null;
        SQLiteCommand cmd = null;
        SQLiteDataReader rdr = null;
        public RegStaff()
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
                if (!(txtUsername.Text.Equals(string.Empty)) && !(txtPassword.Text.Equals(string.Empty)) && !(txtName.Text.Equals(string.Empty)))
                {
                    using (conn = new SQLiteConnection(cs))
                    {
                        conn.Open();

                        string qry = "insert into staff(name,username,password,admin_level) values(@name,@username,@password,@admin_level)";
                        using(cmd = new SQLiteCommand(qry, conn))
                        {
                            cmd.Parameters.AddWithValue("@name", txtName.Text);
                            cmd.Parameters.AddWithValue("@username", txtUsername.Text);
                            cmd.Parameters.AddWithValue("@password", txtPassword.Text);
                            cmd.Parameters.AddWithValue("@admin_level", 1);

                            cmd.Prepare();
                            cmd.ExecuteNonQuery();
                            conn.Close();

                            MessageBox.Show(txtName.Text + "\r\n" + "Account created Successfully");
                        }
                        
                    }
                    

                }
                else
                {
                    conn.Close(); cmd.Dispose();
                    MessageBox.Show("Check required field and fill", "Error", MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    return;
                }
            }
            catch (Exception dd) { MessageBox.Show(dd.Message); conn.Close(); cmd.Dispose(); }
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
