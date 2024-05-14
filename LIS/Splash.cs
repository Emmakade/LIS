using System;
using System.Windows.Forms;
using System.Data.SQLite;
using System.Security.AccessControl;
using System.Security.Principal;
using System.IO;

namespace LIS
{
    public partial class Splash : Form
    {
        bool isFirstTime = false;
        string path = "lifo_table.db";
        //string cs = "URI=file:"+Application.StartupPath+ "\\lifo_table.db";

        public Splash()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                
                progressBar1.Increment(1);
                if (progressBar1.Value == 100)
                {
                    if (isFirstTime) {
                        isFirstTime = false;
                        Super sp = new Super();
                        timer1.Stop();
                        this.Hide();
                        sp.Show();
                    } else {
                        Form1 fm = new Form1();
                        timer1.Stop();
                        this.Hide();
                        fm.Show();
                    }

                }
            }
            catch (Exception tk) { MessageBox.Show(tk.Message); }
        }

        private void Splash_Load(object sender, EventArgs e)
        {
            createDB();
        }
        private void createDB()
        {
            if (!File.Exists(path))
            {
                isFirstTime = true;
                var rule = new FileSystemAccessRule(new SecurityIdentifier(WellKnownSidType.WorldSid, null), FileSystemRights.FullControl, AccessControlType.Allow);
                var security = new FileSecurity();    
                security.AddAccessRule(rule);
                SQLiteConnection.CreateFile(path);
                using (File.Create(path, 100, FileOptions.None, security))
                {

                }
                using (var db = new SQLiteConnection(@"Data Source=" + path+";Version=3;"))
                {
                    try {
                        db.Open();
                        string sql = "CREATE TABLE users (" +
                            "id INTEGER PRIMARY KEY AUTOINCREMENT," +
                            "firstname VARCHAR(30)," +
                            "lastname VARCHAR(30)," +
                            "username VARCHAR(20) NOT NULL," +
                            "password VARCHAR(20) NOT NULL," +
                            "matric VARCHAR(30)," +
                            "phone VARCHAR(15)," +
                            "email VARCHAR(30));";
                        sql += "CREATE TABLE book (" +
                            "id INTEGER PRIMARY KEY AUTOINCREMENT," +
                            "title VARCHAR(20)," +
                            "author VARCHAR(20)," +
                            "publisher VARCHAR(25) NOT NULL," +
                            "year VARCHAR(4) NOT NULL," +
                            "faculty VARCHAR(30)," +
                            "department VARCHAR(30)," +
                            "path VARCHAR(30)," +
                            "viewed INT NOT NULL DEFAULT 0);";
                        sql += "CREATE TABLE pathfinder (" +
                                "id INTEGER PRIMARY KEY AUTOINCREMENT," +
                                "name VARCHAR(30));";
                        sql += "CREATE TABLE staff (" +
                               "id INTEGER PRIMARY KEY AUTOINCREMENT," +
                               "name VARCHAR(50),username VARCHAR(10)," +
                               "password VARCHAR(15),admin_level INT(6));";
                        using (SQLiteCommand command = new SQLiteCommand(sql, db))
                        {
                            command.Prepare();
                            command.ExecuteNonQuery();
                        }
                        
                    } catch(Exception e) {
                        MessageBox.Show(e.Message);
                    }
                }

            }
            else
            {
                Console.WriteLine("Database Cannot be created or already exist");

                return;
            }
        }
    }
}
