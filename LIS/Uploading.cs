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
    public partial class Uploading : Form
    {
        string cs = "URI=file:" + Application.StartupPath + "\\lifo_table.db";

        SQLiteConnection conn = null;
        SQLiteCommand cmd = null;
        SQLiteDataReader rdr = null;
        private static string path;
        bool isUser; bool isDirectoryEmpty = true;

        public Uploading()
        {
            InitializeComponent();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            using (mysettings frm = new mysettings())
            {
                frm.ShowDialog();
            }
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            openFileDialog1.InitialDirectory = "C://Desktop";
            openFileDialog1.Title = "Select book file to be uploaded";
            openFileDialog1.Filter = "Select Valid Document(*.pdf; *.doc; *.docx; *.xls; *.xlsx; *.rtf) | "
                + "*.pdf; *.doc; *.docx; *.xls; *.xlsx; *.rtf";
            openFileDialog1.FilterIndex = 1;

            try
            {
                if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    if (openFileDialog1.CheckFileExists)
                    {
                        string bookPath = System.IO.Path.GetFullPath(openFileDialog1.FileName);
                        lblBookPath.Text = bookPath;
                    }

                }
                else
                {
                    MessageBox.Show("Please Upload a Document");
                }
            }
            catch (Exception sd) { MessageBox.Show(sd.Message); }
        }

        private void btnUpload_Click(object sender, EventArgs e)
        {
            try
            {
                if (!isDirectoryEmpty) 
                {
                    if (!string.IsNullOrEmpty(txtTitle.Text) && !string.IsNullOrEmpty(txtAuthor.Text))
                    {
                        string filename = System.IO.Path.GetFileName(openFileDialog1.FileName);
                        if (filename == null)
                        {
                            MessageBox.Show("Please select a valid document with name.");
                        }
                        else
                        {
                            string dir = path + "\\" + filename;
                            //MessageBox.Show(path);
                            using (conn = new SQLiteConnection(cs))
                            {
                                conn.Open();
                                string qry = "insert into book (title,author,publisher,year,faculty,department,path) values(@title,@author,@publisher,@year,@faculty,@department,@path)";
                                using (cmd = new SQLiteCommand(qry, conn))
                                {
                                    cmd.Parameters.AddWithValue("@title", txtTitle.Text);
                                    cmd.Parameters.AddWithValue("@author", txtAuthor.Text);
                                    cmd.Parameters.AddWithValue("@publisher", txtPub.Text);
                                    cmd.Parameters.AddWithValue("@year", txtYear.Text);
                                    cmd.Parameters.AddWithValue("@faculty", txtFaculty.Text);
                                    cmd.Parameters.AddWithValue("@department", txtDept.Text);
                                    cmd.Parameters.AddWithValue("@path", dir);

                                    //string dirPath = Application.StartupPath.Substring(0, (Application.StartupPath.Length - 10));
                                    System.IO.File.Copy(openFileDialog1.FileName, dir);
                                    cmd.Prepare();
                                    cmd.ExecuteNonQuery();
                                    conn.Close();
                                    MessageBox.Show(txtTitle.Text + " DOCUMENT \r\n" + " uploaded Successfully", "SUCCESS!",
                                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                
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
                else
                {
                    MessageBox.Show("Please browse and set A folder for all Document", "EMPTY DIRECTORY!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception dd) { MessageBox.Show(dd.Message); }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Uploading_Load(object sender, EventArgs e)
        {
            isUser = User.isUser;

            if (isUser)
            {
                btnBrowse.Visible = false;
            }

            string query = string.Format("SELECT name FROM `pathfinder`");
            using (conn = new SQLiteConnection(cs))
            {
                conn.Open();
                using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                {
                    using (SQLiteDataReader rd = cmd.ExecuteReader())
                    {
                        if (rd.Read())
                        {
                            path = rd.GetString(0);
                            isDirectoryEmpty = false;
                        }
                        else
                        {
                            isDirectoryEmpty = true;
                            MessageBox.Show("Please Select A folder for all Document", "EMPTY DIRECTORY!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        cmd.Dispose(); conn.Close(); conn.Dispose();
                    }
                        
                }
                
            }
        }

    }
}
