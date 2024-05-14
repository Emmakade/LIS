using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Reflection;
using System.Threading;
using System.Security.AccessControl;
using System.Security.Principal;

namespace LIS
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            string appGuid = ((GuidAttribute)Assembly.GetEntryAssembly().
                GetCustomAttributes(typeof(GuidAttribute), false).GetValue(0)).Value.ToString();

            string mutexId = string.Format("Global\\{{{0}}}", appGuid);

            using (Mutex mutex = new Mutex(false, mutexId))
            {
                if (!mutex.WaitOne(0, false))
                {
                    MessageBox.Show("Application Already Launched");
                    return;
                }
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                try
                {
                    Application.Run(new Splash());
                }
                catch (Exception pr) { MessageBox.Show(pr.Message + "  \r\n PLEASE CONTACT 08062457590"); }
            }
        }
    }
}
