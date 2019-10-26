using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Data.Common;
using System.Windows.Forms;

namespace CRM
{
    public partial class FormLogs : Form
    {
        public FormLogs()
        {
            InitializeComponent();
        }

        private void FormLogs_Load(object sender, EventArgs e)
        {
            FormLoading loading = new FormLoading();
            loading.Show();

            using (var mySqlConnection = new DBUtils().getDBConnection())
            {
                mySqlConnection.Open();
                using (var cmd = new MySqlCommand("load_logs", mySqlConnection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (DbDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                string strResult = reader.GetString(0).Replace("\\n", Environment.NewLine);
                                richTextBox1.Text += strResult + "\n--------------------------\n";
                            }
                        }
                    }
                }
            }
            loading.Dispose();
        }
    }
}
