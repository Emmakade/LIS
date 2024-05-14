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
    public partial class Preview : Form
    {
        string cs = "URI=file:" + Application.StartupPath + "\\lifo_table.db";

        SQLiteConnection conn = null;
        SQLiteCommand cmd = null;

        public static string path;
        public static int id, viewed;
        static bool isAdmin, isStaff, isUser;
        public Preview()
        {
            InitializeComponent();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            isAdmin = false; isStaff = false; isUser = false;
            this.Dispose();
            this.Close();
        }

        private void btnOPen_Click(object sender, EventArgs e)
        {
            string filename = path;
            System.Diagnostics.Process.Start(filename);
            //string dirPath = Application.StartupPath.Substring(0, Application.StartupPath.Length - 10);
            /*try
            {
                using (conn = new SQLiteConnection(cs))
                {
                    conn.Open();
                    string qw = string.Format("UPDATE book SET viewed = viewed + 1 where id='{0}'", id);
                    using (SQLiteCommand cmd = new SQLiteCommand(conn))
                    {
                        cmd.CommandText = qw;
                        //cmd.Prepare();
                        cmd.ExecuteNonQuery();
                        //conn.Close();

                        *//* isAdmin = false; isStaff = false; isUser = false;

                         this.Dispose(); conn.Dispose(); this.Close();*//*

                    }

                }
            }
            catch (Exception pt) { MessageBox.Show(pt.Message); }*/

            
        }

        private void Prev_Load(object sender, EventArgs e)
        {
            isAdmin = Admin.isAdmin; isStaff = Staff.isStaff; isUser = User.isUser;

            if(isAdmin){

                lbltitle.Text = Admin.title;
                lblAuthor.Text = "(Author : " + Admin.author +" )";
                lblPub.Text = Admin.publisher;
                lblYear.Text = Admin.year;
                lblFaculty.Text = Admin.faculty;
                lblDept.Text = Admin.department;
                lblView.Text = Admin.viewed.ToString();
                id = Admin.id;
                path = Admin.path;

            } else if(isStaff){

                lbltitle.Text = Staff.title;
                lblAuthor.Text = "(Author : " + Staff.author + " )";
                lblPub.Text = Staff.publisher;
                lblYear.Text = Staff.year;
                lblFaculty.Text = Staff.faculty;
                lblDept.Text = Staff.department;
                lblView.Text = Staff.viewed.ToString();
                id = Staff.id;
                path = Staff.path;

            } else if(isUser){

                lbltitle.Text = User.title;
                lblAuthor.Text = "(Author : " + User.author + " )";
                lblPub.Text = User.publisher;
                lblYear.Text = User.year;
                lblFaculty.Text = User.faculty;
                lblDept.Text = User.department;
                lblView.Text = User.viewed.ToString();
                id = User.id;
                path = User.savedpath;
            }
            
        }
    }
}
