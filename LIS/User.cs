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
    public partial class User : Form
    {
        string path = "lifo_table.db";
        string cs = "URI=file:" + Application.StartupPath + "\\lifo_table.db";

        SQLiteConnection conn = null;
        SQLiteCommand cmd = null;
        SQLiteDataReader rdr = null;

        public static string title;
        public static string author;
        public static string publisher;
        public static string year;
        public static string faculty;
        public static string department;
        public static string savedpath;
        public static int id, viewed;
        public static bool isUser = false;
        public User()
        {
            InitializeComponent();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            Form1 lg = new Form1();
            this.Close();
            lg.Show();
        }

        private void toolStripLabel5_Click(object sender, EventArgs e)
        {
            isUser = true;
            using (Uploading ff = new Uploading())
            {
                if (ff.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    isUser = false;
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void toolStripLabel3_Click(object sender, EventArgs e)
        {
            MessageBox.Show("N/A", "N/A!", MessageBoxButtons.OK, MessageBoxIcon.Stop);
        }

        private void User_Load(object sender, EventArgs e)
        {
            using (conn = new SQLiteConnection(cs))
            {
                conn.Open();
                using (SQLiteDataAdapter sda = new SQLiteDataAdapter("select count(*) from book", conn))
                {
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    lblCount.Text = dt.Rows[0][0].ToString();
                }
                   
            }
            
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string searchName = txtSearch.Text;
            string query = string.Format("SELECT * FROM `book` WHERE `title` LIKE '%{0}%' LIMIT 1", searchName);

            try
            {
                using(conn = new SQLiteConnection(cs))
                {
                    conn.Open();
                    using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                    {
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
                                savedpath = rd.GetString(7);
                                viewed = rd.GetInt32(8);

                                /*conn.Close();
                                conn.Dispose();*/

                                isUser = true;
                                using (Preview pf = new Preview())
                                {
                                    if (pf.ShowDialog() == DialogResult.OK)
                                        isUser = false;
                                }
                            }
                            else
                            {
                                MessageBox.Show("No book found", "Not Found!", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                            }

                            conn.Close();
                            conn.Dispose();
                        }
                        
                    }
                    
                }
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

        private void toolStripLabel1_Click(object sender, EventArgs e)
        {

        }

        private void toolStripLabel2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("N/A", "N/A!", MessageBoxButtons.OK, MessageBoxIcon.Stop);
        }
    }
}
