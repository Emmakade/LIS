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
    public partial class mysettings : Form
    {
        string cs = "URI=file:" + Application.StartupPath + "\\lifo_table.db";

        SQLiteConnection conn = null;
        SQLiteCommand cmd = null;

        string path;
        bool isPathExist = false;
        public mysettings()
        {
            InitializeComponent();
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog fbd = new FolderBrowserDialog() { Description = "Select uploading path for document." })
            {
                if (fbd.ShowDialog() == DialogResult.OK)
                    txtPath.Text = fbd.SelectedPath;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtPath.Text))
                {
                    string pt = txtPath.Text.Trim();
                    
                    if (isPathExist)
                    {
                        using (SQLiteConnection conn = new SQLiteConnection(cs))
                        {
                            conn.Open();
                            string qw = string.Format("UPDATE pathfinder SET name = @name where id='{0}'", 1);
                            using (cmd = new SQLiteCommand(qw, conn))
                            {
                                cmd.Parameters.AddWithValue("@name", pt);
                                cmd.Prepare();
                                cmd.ExecuteNonQuery();
                                this.Dispose(); conn.Dispose(); 
                                this.Close();
                            } //end using cmd
                            
                        } //end using conn
                        
                    }
                    else
                    {
                        using (conn = new SQLiteConnection(cs))
                        {
                            conn.Open();
                            string qry = "insert into pathfinder (name) values(@name)";
                            using (SQLiteCommand cmd = new SQLiteCommand(qry, conn))
                            {
                                //cmd.CommandText = "INSERT INTO pathfinder (name) values(@name)";
                                cmd.Parameters.AddWithValue("@name", pt);
                                cmd.Prepare();
                                cmd.ExecuteNonQuery();
                                conn.Close(); conn.Dispose();
                                this.Close();
                            } //end cmd
                            
                        } //end conn
                        
                    }
                }
                else
                    MessageBox.Show("Please select your path,", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            } catch (Exception ex) { MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK); }
        }

        private void mysettings_Load(object sender, EventArgs e)
        {
            string query = string.Format("SELECT name FROM pathfinder");
            using (conn = new SQLiteConnection(cs))
            {
                conn.Open();
                using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                {
                    //cmd.CommandText = "SELECT name FROM pathfinder";
                    using (SQLiteDataReader rd = cmd.ExecuteReader())
                    {
                        if (rd.Read())
                        {
                            path = rd.GetString(0);
                            isPathExist = true;
                        }
                        else
                        {
                            MessageBox.Show("Please Select A folder for all Document", "EMPTY DIRECTORY!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        cmd.Dispose();
                        conn.Close();
                        conn.Dispose(); conn = null;
                        lblPath.Text = txtPath.Text = path;
                    } //end rd
                    
                }
                
            } //end using conn
            
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            Admin ad = new Admin();
            this.Hide();
            ad.Show();
        }
    }
}
