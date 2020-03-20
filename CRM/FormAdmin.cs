using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Data.Common;
using System.Windows.Forms;

namespace CRM
{
    public partial class FormAdmin : Form
    {
        public FormAdmin()
        {
            InitializeComponent();
        }

        private void buttonLogs_Click(object sender, EventArgs e)
        {
            if (!new InternetConnection().CheckForInternetConnection())
            {
                MessageBox.Show("На компьютере отсутствует интернет соединение");
                return;
            }
            FormLogs logs = new FormLogs();
            logs.ShowDialog();
        }

        private void buttonUsersList_Click(object sender, EventArgs e)
        {
            if (!new InternetConnection().CheckForInternetConnection())
            {
                MessageBox.Show("На компьютере отсутствует интернет соединение");
                return;
            }
            FormList list = new FormList(1);
            list.ShowDialog();
        }

        private void buttonOrdersTypeList_Click(object sender, EventArgs e)
        {
            if (!new InternetConnection().CheckForInternetConnection())
            {
                MessageBox.Show("На компьютере отсутствует интернет соединение");
                return;
            }
            FormList list = new FormList(2);
            list.ShowDialog();
        }

        private void FormAdmin_Load(object sender, EventArgs e)
        {

        }

        private void buttonChangePaperSizes_Click(object sender, EventArgs e)
        {
            if (!new InternetConnection().CheckForInternetConnection())
            {
                MessageBox.Show("На компьютере отсутствует интернет соединение");
                return;
            }
            FormList list = new FormList(4);
            list.ShowDialog();
        }

        private string getServerIP()
        {
            try
            {
                using (var mySqlConnection = new DBUtils().getDBConnection())
                {
                    mySqlConnection.Open();
                    using (var cmd = new MySqlCommand("get_phototerminal_server_ip", mySqlConnection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        using (DbDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    return reader.GetString(0);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return "";
            }
            return "";
        }

        private void buttonChangePrototermServer_Click(object sender, EventArgs e)
        {
            FormEnterServerIP serverIP = new FormEnterServerIP(getServerIP());
            if (serverIP.ShowDialog(this) == DialogResult.OK)
            {
                if (serverIP.textBoxIP.Text.Trim().Length > 0)
                {
                    using (var mySqlConnection = new DBUtils().getDBConnection())
                    {
                        using (var cmd = new MySqlCommand())
                        {
                            cmd.Connection = mySqlConnection;
                            cmd.CommandType = System.Data.CommandType.StoredProcedure;
                            cmd.CommandText = "change_phototerminal_server_ip";
                            cmd.Parameters.Clear();
                            MySqlParameter p1 = cmd.Parameters.Add("@ip", MySqlDbType.VarChar);
                            p1.Direction = ParameterDirection.Input;

                            p1.Value = serverIP.textBoxIP.Text;
                            mySqlConnection.Open();
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
            }
        }
    }
}
