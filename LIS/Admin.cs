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
    public partial class Admin : Form
    {
        string cs = "URI=file:" + Application.StartupPath + "\\lifo_table.db";

        public static string title;
        public static string author;
        public static string publisher;
        public static string year;
        public static string faculty;
        public static string department;
        public static string path;
        public static int id,viewed;
        public static bool isAdmin = false;

        public Admin()
        {
            InitializeComponent();
        }

        private void toolStripLabel3_Click(object sender, EventArgs e)
        {
            using (mysettings frm = new mysettings())
            {
                frm.ShowDialog();
            }
        }

        private void toolStripLabel2_Click(object sender, EventArgs e)
        {
            using (Form2 fr = new Form2())
            {
                fr.ShowDialog();
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void toolStripLabel5_Click(object sender, EventArgs e)
        {
            using (Uploading ff = new Uploading())
            {
                ff.ShowDialog();
            }
        }

        private void toolStripLabel6_Click(object sender, EventArgs e)
        {
            using (RegStaff rf = new RegStaff())
            {
                rf.ShowDialog();
            }
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            Form1 lg = new Form1();
            this.Close();
            this.Dispose();
            lg.Show();
        }

        private void Admin_Load(object sender, EventArgs e)
        {
            using (SQLiteConnection conn = new SQLiteConnection(cs))
            {
                conn.Open();
                using (SQLiteDataAdapter sda = new SQLiteDataAdapter("select count(*) from book", conn))
                {
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    lblCount.Text = dt.Rows[0][0].ToString();
                    conn.Close(); conn.Dispose();
                }
                
            }
            
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string searchName = txtSearch.Text;
            string query = string.Format("SELECT * FROM `book` WHERE `title` LIKE '%{0}%' LIMIT 1", searchName);

            try
            {
                using(SQLiteConnection conn = new SQLiteConnection(cs))
                {
                    conn.Open();
                    using(SQLiteCommand cmd = new SQLiteCommand(query, conn)){

                        using (SQLiteDataReader rd = cmd.ExecuteReader())
                        {
                            if (rd.Read())
                            {
                                id = rd.GetInt16(0);
                                title = rd.GetString(1);
                                author = rd.GetString(2);
                                publisher = rd.GetString(3);
                                year = rd.GetString(4);
                                faculty = rd.GetString(5);
                                department = rd.GetString(6);
                                path = rd.GetString(7);
                                viewed = rd.GetInt32(8);

                                //conn.Close();
                                //conn.Dispose();

                                isAdmin = true;
                               
                                using (Preview pa = new Preview())
                                {
                                    if (pa.ShowDialog() == DialogResult.OK)
                                        isAdmin = false;
                                }
                                
                            }
                            else
                            {
                                MessageBox.Show("No book found", "Not Found!", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                            }

                            conn.Close();
                            conn.Dispose();
                        } //end using dr
                        
                    } //end Using CMD

                } //end using conn

            }
            catch (Exception ca)
            {
                MessageBox.Show(ca.Message);
            }
        }

        private void toolStripLabel7_Click(object sender, EventArgs e)
        {
            using (Ebooks eb = new Ebooks())
            {
                eb.ShowDialog();
            }
        }

        private void toolStripLabel4_Click(object sender, EventArgs e)
        {
            MessageBox.Show("N/A", "N/A!", MessageBoxButtons.OK, MessageBoxIcon.Stop);
        }

        private void tsAll_Click(object sender, EventArgs e)
        {
            using (frmAllUsers au = new frmAllUsers())
            {
                au.ShowDialog();
            }
        }

    }
}
