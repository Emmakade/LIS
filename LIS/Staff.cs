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
    public partial class Staff : Form
    {
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
        public static string path;
        public static int id, viewed;
        public static bool isStaff = false;
        public Staff()
        {
            InitializeComponent();
        }

        private void toolStripLabel2_Click(object sender, EventArgs e)
        {
            using (Form2 fr = new Form2())
            {
                fr.ShowDialog();
            }
        }

        private void toolStripLabel3_Click(object sender, EventArgs e)
        {
            using (mysettings frm = new mysettings())
            {
                frm.ShowDialog();
            }
        }

        private void toolStripLabel5_Click(object sender, EventArgs e)
        {
            using (Uploading ff = new Uploading())
            {
                ff.ShowDialog();
            }
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            Form1 lg = new Form1();
            this.Close();
            lg.Show();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Staff_Load(object sender, EventArgs e)
        {
            using (conn = new SQLiteConnection(cs))
            {
                conn.Open();
                using (SQLiteDataAdapter sda = new SQLiteDataAdapter("select count(*) from book", conn))
                {
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    lblCount.Text = dt.Rows[0][0].ToString();
                    conn.Close();
                }
                
            }
            
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string searchName = txtSearch.Text;
            string query = string.Format("SELECT * FROM `book` WHERE `title` LIKE '%{0}%' LIMIT 1", searchName);

            try
            {
                using (conn = new SQLiteConnection(cs))
                {
                    conn.Open();
                    using (cmd = new SQLiteCommand(query, conn))
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
                                path = rd.GetString(7);
                                viewed = rd.GetInt32(8);

                                

                                isStaff = true;
                                using (Preview ps = new Preview())
                                {
                                    if (ps.ShowDialog() == DialogResult.OK)
                                        isStaff = false;
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
    }
}
