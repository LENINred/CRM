using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM
{
    class DBUtils
    {
        public DBUtils()
        {
        }

        public MySqlConnection getDBConnection()
        {
            String connString = @"Server=83.220.174.171;Database=CRM01;user id=CRM_user;password=Az123456!;charset=utf8";
            return new MySqlConnection(connString);
        }
    }
}
