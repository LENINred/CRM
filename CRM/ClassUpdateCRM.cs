using MySql.Data.MySqlClient;
using System.Data.Common;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Reflection;
using System.Windows.Forms;

namespace CRM
{
    class ClassUpdateCRM
    {
        public void checkUpdate()
        {
            FormLoading loading = new FormLoading();
            loading.Show();

            string productVersion = FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).ProductVersion;

            string update = productVersion;
            using (var mySqlConnection = new BackDoorConnection().getDBConnection())
            {
                mySqlConnection.Open();
                using (var cmd = new MySqlCommand("SELECT `new_update` FROM `UPDATE` WHERE 1", mySqlConnection))
                {
                    using (DbDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                update = reader.GetString(0);
                            }
                        }
                    }
                }
            }
            if (update != productVersion)
            {
                File.Delete(System.Reflection.Assembly.GetEntryAssembly().Location + "CRM.bak");
                File.Move(System.Reflection.Assembly.GetEntryAssembly().Location + "CRM.exe", System.Reflection.Assembly.GetEntryAssembly().Location + "CRM.bak");
                using (var client = new WebClient())
                {
                    client.DownloadFile("https://github.com/LENINred/CRM/raw/master/CRM/bin/Debug/CRM.exe", "CRM.exe");
                }
                Application.Restart();
            }
            loading.Dispose();
        }
    }
}
