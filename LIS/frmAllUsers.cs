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
    public partial class frmAllUsers : Form
    {
        string cs = "URI=file:" + Application.StartupPath + "\\lifo_table.db";

        SQLiteConnection conn = null;
        SQLiteCommand cmd = null;
        DataTable dt;
        SQLiteDataAdapter sda;
        DataSet ds;
        public frmAllUsers()
        {
            InitializeComponent();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void frmAllUsers_Load(object sender, EventArgs e)
        {
            try
            {
                listView1.Columns.Add("#", 30);
                listView1.Columns.Add("Firstname", 110);
                listView1.Columns.Add("Lastname", 110);
                listView1.Columns.Add("Username", 110);
                listView1.Columns.Add("Matric", 110);
                listView1.Columns.Add("Phone", 110);
                listView1.Columns.Add("email", 150);
                listView1.View = View.Details;
                string q = "select id,firstname,lastname,username,matric,phone,email from users";

                using (SQLiteConnection conn = new SQLiteConnection(cs))
                {
                    conn.Open();
                    using (SQLiteCommand cmd = new SQLiteCommand(q, conn))
                    {
                        using (SQLiteDataAdapter sda = new SQLiteDataAdapter(cmd))
                        {
                            ds = new DataSet();
                            conn.Close();
                            sda.Fill(ds, "users");

                            dt = ds.Tables["users"];
                            for (int i = 0; i <= dt.Rows.Count - 1; i++)
                            {
                                listView1.Items.Add(dt.Rows[i].ItemArray[0].ToString());
                                listView1.Items[i].SubItems.Add(dt.Rows[i].ItemArray[1].ToString());
                                listView1.Items[i].SubItems.Add(dt.Rows[i].ItemArray[2].ToString());
                                listView1.Items[i].SubItems.Add(dt.Rows[i].ItemArray[3].ToString());
                                listView1.Items[i].SubItems.Add(dt.Rows[i].ItemArray[4].ToString());
                                listView1.Items[i].SubItems.Add(dt.Rows[i].ItemArray[5].ToString());
                                listView1.Items[i].SubItems.Add(dt.Rows[i].ItemArray[6].ToString());
                            }
                            conn.Dispose();
                            lblName.Text = "All Members";
                        } // end sda
                    } //end cmd
                } //end conn
            }
            catch (Exception ed) { MessageBox.Show(ed.Message); conn.Close(); conn.Dispose(); }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            listView1.Clear();
            try
            {
                if (btnLoad.Text == "Load All Staffs")
                {
                    listView1.Columns.Add("#", 30);
                    listView1.Columns.Add("name", 180);
                    listView1.Columns.Add("username", 110);
                    listView1.View = View.Details;
                    string qry = "select id,name,username from staff";

                    using (SQLiteConnection conn = new SQLiteConnection(cs))
                    {
                        conn.Open();
                        using (SQLiteCommand cmd = new SQLiteCommand(qry, conn))
                        {
                            using (SQLiteDataAdapter sda = new SQLiteDataAdapter(cmd))
                            {
                                ds = new DataSet();
                                conn.Close();
                                sda.Fill(ds, "staff");

                                dt = ds.Tables["staff"];
                                for (int i = 0; i <= dt.Rows.Count - 1; i++)
                                {
                                    listView1.Items.Add(dt.Rows[i].ItemArray[0].ToString());
                                    listView1.Items[i].SubItems.Add(dt.Rows[i].ItemArray[1].ToString());
                                    listView1.Items[i].SubItems.Add(dt.Rows[i].ItemArray[2].ToString());

                                }
                                conn.Close(); conn.Dispose();

                                lblName.Text = "All Staffs"; btnLoad.Text = "Load All Members";
                            }
                            
                        }
                        
                    } //end conn
                }
                else if (btnLoad.Text == "Load All Members")
                {
                    listView1.Columns.Add("#", 30);
                    listView1.Columns.Add("Firstname", 110);
                    listView1.Columns.Add("Lastname", 110);
                    listView1.Columns.Add("Username", 110);
                    listView1.Columns.Add("Matric", 110);
                    listView1.Columns.Add("Phone", 110);
                    listView1.Columns.Add("email", 150);
                    listView1.View = View.Details;
                    string qry = "select id,firstname,lastname,username,matric,phone,email from users";

                    using (SQLiteConnection conn = new SQLiteConnection(cs))
                    {
                        conn.Open();
                        using (SQLiteCommand cmd = new SQLiteCommand(qry, conn))
                        {
                            using (SQLiteDataAdapter sda = new SQLiteDataAdapter(cmd))
                            {
                                ds = new DataSet();
                                conn.Close();
                                sda.Fill(ds, "users");

                                dt = ds.Tables["users"];
                                for (int i = 0; i <= dt.Rows.Count - 1; i++)
                                {
                                    listView1.Items.Add(dt.Rows[i].ItemArray[0].ToString());
                                    listView1.Items[i].SubItems.Add(dt.Rows[i].ItemArray[1].ToString());
                                    listView1.Items[i].SubItems.Add(dt.Rows[i].ItemArray[2].ToString());
                                    listView1.Items[i].SubItems.Add(dt.Rows[i].ItemArray[3].ToString());
                                    listView1.Items[i].SubItems.Add(dt.Rows[i].ItemArray[4].ToString());
                                    listView1.Items[i].SubItems.Add(dt.Rows[i].ItemArray[5].ToString());
                                    listView1.Items[i].SubItems.Add(dt.Rows[i].ItemArray[6].ToString());
                                }
                                conn.Close(); conn.Dispose();

                                lblName.Text = "All Members"; btnLoad.Text = "Load All Staffs";
                            }
                        }
                        
                    }//end conn
                }
                
            }
            catch (Exception ed) { MessageBox.Show(ed.Message); conn.Close(); conn.Dispose(); }
            
        }
    }
}
