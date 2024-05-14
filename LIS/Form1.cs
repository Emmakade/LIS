using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;

namespace LIS
{
    public partial class Form1 : Form
    {
        string path = "lifo_table.db";
        string cs = "URI=file:" + Application.StartupPath + "\\lifo_table.db";

        SQLiteConnection conn = null;
        SQLiteCommand cmd = null;
        SQLiteDataReader rdr = null;
        public static string usr;
        public Form1()
        {
            try
            {
                //Thread t = new Thread(new ThreadStart(splashScr));
                //t.Start();
                //Thread.Sleep(10000);
                InitializeComponent();
                //t.Abort();
            }
            catch (Exception ed) { MessageBox.Show(ed.Message); }
        }
        public void splashScr()
        {
            Application.Run(new Splash());
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (!(txtUsername.Text == string.Empty) && !(txtPassword.Text == string.Empty))
            {
                usr = txtUsername.Text.Trim();
                usr = usr.ToLower();
                if (cmbRole.Text == "User")
                {
                    try
                    {
                        using (SQLiteConnection conn = new SQLiteConnection(cs))
                        {
                            conn.Open();
                            using (SQLiteDataAdapter sda = new SQLiteDataAdapter("select count(*) from users where username ='" + usr +
                                "' and password='" + this.txtPassword.Text + "'", conn))
                            {
                                DataTable dt = new DataTable();
                                sda.Fill(dt);
                                if (dt.Rows[0][0].ToString() == "1")
                                {
                                    sda.Dispose(); dt.Dispose(); conn.Close();
                                    User ad = new User();
                                    this.Hide();
                                    ad.Show();
                                }
                                else
                                {
                                    MessageBox.Show("Username and password combination incorrect for User, \r\n Kindly correct and retry", "Error",
                                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    conn.Close(); conn.Dispose(); sda.Dispose();
                                }
                            }
                           
                        } //end conn
                        
                    }
                    catch (Exception ex) { MessageBox.Show(ex.Message); conn.Close(); }
                }
                else if (cmbRole.Text == "Admin")
                {
                    int level;
                    string query = string.Format("SELECT admin_level from staff where username ='" + usr +
                            "' and password='" + this.txtPassword.Text + "'", usr);
                    try
                    {
                        using (conn = new SQLiteConnection(cs))
                        {
                            conn.Open();
                            using (cmd = new SQLiteCommand(query, conn))
                            {
                                using (SQLiteDataReader rdr = cmd.ExecuteReader())
                                {
                                    if (rdr.Read())
                                    {
                                        level = rdr.GetInt16(0);
                                        conn.Close(); conn.Dispose();
                                        if (level == 1)
                                        {
                                            Staff sf = new Staff();
                                            this.Hide();
                                            sf.Show();
                                        }
                                        else if (level == 2)
                                        {
                                            Admin ad = new Admin();
                                            this.Hide();
                                            ad.Show();
                                        }

                                    }
                                    else
                                    {
                                        MessageBox.Show("Username and password combination incorrect for Administrator, \r\n Kindly correct and retry", "Error",
                                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        conn.Close(); conn.Dispose();
                                    }
                                }
                                
                            }
                            
                        } //end con
                        
                    }
                    catch (Exception ex) { MessageBox.Show(ex.Message); conn.Close(); }
                }
                else
                {
                    MessageBox.Show("Please select a role, \r\n and try again", "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                    conn.Close();
                }
            }
            else
            {
                MessageBox.Show("Username and/or Password Cannot be empty, \r\n fill and try again", "Error",
                               MessageBoxButtons.OK, MessageBoxIcon.Error);
                conn.Close();
            }
            
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
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
