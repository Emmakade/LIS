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
    public partial class Ebooks : Form
    {
        string path;

        string cs = "URI=file:" + Application.StartupPath + "\\lifo_table.db";

        SQLiteConnection conn = null;
        SQLiteCommand cmd = null;
        SQLiteDataReader rdr = null;
        DataTable dt;
        SQLiteDataAdapter sda;
        DataSet ds;
        public Ebooks()
        {
            InitializeComponent();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Ebooks_Load(object sender, EventArgs e)
        {
            try
            {
                string q = "select id,title,author,year,viewed from book";
                using (SQLiteConnection conn = new SQLiteConnection(cs))
                {
                    conn.Open();
                    using (cmd = new SQLiteCommand(q, conn))
                    {
                        using (sda = new SQLiteDataAdapter(cmd))
                        {
                            ds = new DataSet();
                            conn.Close();
                            sda.Fill(ds, "Books");

                            dt = ds.Tables["Books"];
                            for (int i = 0; i <= dt.Rows.Count - 1; i++)
                            {
                                listView1.Items.Add(dt.Rows[i].ItemArray[0].ToString());
                                listView1.Items[i].SubItems.Add(dt.Rows[i].ItemArray[1].ToString());
                                listView1.Items[i].SubItems.Add(dt.Rows[i].ItemArray[2].ToString());
                                listView1.Items[i].SubItems.Add(dt.Rows[i].ItemArray[3].ToString());
                                listView1.Items[i].SubItems.Add(dt.Rows[i].ItemArray[4].ToString());
                            }
                        }
                    }
                }
            }
            catch (Exception ed) { MessageBox.Show(ed.Message); conn.Close(); conn.Dispose(); }
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                foreach (int i in listView1.SelectedIndices)
                {
                    int id = int.Parse(listView1.Items[i].Text);
                    string query = string.Format("SELECT `path` FROM `book` WHERE `id` = {0}", id);
                    conn.Open();
                    cmd = new SQLiteCommand(query, conn);
                     
                    rdr = cmd.ExecuteReader();
                    if (rdr.Read())
                    {
                        path = rdr.GetString(0);
                    }
                    cmd.Dispose(); rdr.Dispose();

                    string filename = path;
                    //string dirPath = Application.StartupPath.Substring(0, (Application.StartupPath.Length - 10));

                    if (!string.IsNullOrEmpty(path))
                    {
                        string qw = string.Format("UPDATE book SET viewed = viewed + 1 where id='{0}'", id);
                        cmd = new SQLiteCommand(qw, conn);

                        cmd.ExecuteNonQuery();
                        
                        System.Diagnostics.Process.Start(filename);
                        this.Dispose(); conn.Dispose(); this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Empty Path","Null!", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    }
                        

                } 
            } catch(Exception s){
                MessageBox.Show(s.Message);
            }
        }

    }
}
