using MySql.Data.MySqlClient;
using System.Data.Common;
using System.Diagnostics;
using System.Reflection;

namespace CRM
{
    class ClassUpdateCRM
    {
        public bool checkUpdate()
        {
            /*FormLoading loading = new FormLoading();
            loading.Show();*/

            string productVersion = FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).ProductVersion;

            string update = "";
            using (var mySqlConnection = new BackDoorConnection().getDBConnection())
            {
                mySqlConnection.Open();
                using (var cmd = new MySqlCommand("SELECT `new_build` FROM `UPDATE` WHERE 1", mySqlConnection))
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
                return true;
            }
            else
            {
                //loading.Dispose();
                return false;
            }
        }
    }
}
