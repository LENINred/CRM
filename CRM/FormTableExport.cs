using ClosedXML.Excel;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Windows.Forms;

namespace CRM
{
    public partial class FormTableExport : Form
    {
        public FormTableExport()
        {
            InitializeComponent();
        }

        private void FormTableExport_Load(object sender, EventArgs e)
        {
            comboBoxCustomers.Items.AddRange(loadCustomers().ToArray());
            comboBoxExecutor.Items.AddRange(loadExecutors().ToArray());
            comboBoxAcceptor.Items.AddRange(loadExecutors().ToArray());
            comboBoxOrderType.Items.AddRange(loadOrderTypes().ToArray());
            comboBoxCustomers.SelectedIndex = 0;
            comboBoxExecutor.SelectedIndex = 0;
            comboBoxAcceptor.SelectedIndex = 0;
            comboBoxOrderType.SelectedIndex = 0;
            comboBoxOrderStatus.SelectedIndex = 0;
            dateTimePickerFrom.Text = "01-09-2019";
            dateTimePickerTo.Text = DateTime.Now.ToString("dd-MM-yyyy");
        }

        private void buttonExport_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Filter = "Excel файл (*.xlsx)|*.xlsx";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string filename = saveFileDialog1.FileName;

                string custName = comboBoxCustomers.SelectedItem.ToString();
                if (custName != "Все")
                    custName = comboBoxCustomers.SelectedItem.ToString().Substring(0, comboBoxCustomers.SelectedItem.ToString().IndexOf('('));


                DataTable tblOrders = new DataTable();
                using (var mySqlConnection = new DBUtils().getDBConnection())
                {
                    using (var cmd = new MySqlCommand())
                    {
                        cmd.Connection = mySqlConnection;
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.CommandText = "export_table_new";
                        cmd.Parameters.Clear();
                        MySqlParameter p1 = cmd.Parameters.Add("@type", MySqlDbType.VarChar);
                        p1.Direction = ParameterDirection.Input;
                        MySqlParameter p2 = cmd.Parameters.Add("@status", MySqlDbType.VarChar);
                        p2.Direction = ParameterDirection.Input;
                        MySqlParameter p3 = cmd.Parameters.Add("@executor", MySqlDbType.VarChar);
                        p3.Direction = ParameterDirection.Input;
                        MySqlParameter p4 = cmd.Parameters.Add("@acceptor", MySqlDbType.VarChar);
                        p4.Direction = ParameterDirection.Input;
                        MySqlParameter p5 = cmd.Parameters.Add("@customer", MySqlDbType.VarChar);
                        p5.Direction = ParameterDirection.Input;
                        MySqlParameter p6 = cmd.Parameters.Add("@dateFrom", MySqlDbType.Date);
                        p6.Direction = ParameterDirection.Input;
                        MySqlParameter p7 = cmd.Parameters.Add("@dateTo", MySqlDbType.Date);
                        p7.Direction = ParameterDirection.Input;

                        p1.Value = comboBoxOrderType.SelectedItem.ToString();
                        p2.Value = comboBoxOrderStatus.SelectedItem.ToString();
                        p3.Value = comboBoxExecutor.SelectedItem.ToString();
                        p4.Value = comboBoxAcceptor.SelectedItem.ToString();
                        p5.Value = custName;
                        p6.Value = dateTimePickerFrom.Value.ToString("yyyy-MM-dd");
                        p7.Value = dateTimePickerTo.Value.ToString("yyyy-MM-dd");

                        mySqlConnection.Open();
                        MySqlDataAdapter dap = new MySqlDataAdapter(cmd);
                        dap.Fill(tblOrders);
                    }
                }

                XLWorkbook wb = new XLWorkbook();
                wb.Worksheets.Add(tblOrders, "Экспортированная таблица");
                wb.SaveAs(filename);

                MessageBox.Show("Данные выгружены");
            }
        }

        private List<string> loadCustomers()
        {
            List<string> custs = new List<string>();
            using (var mySqlConnection = new DBUtils().getDBConnection())
            {
                mySqlConnection.Open();
                using (var cmd = new MySqlCommand("load_customers", mySqlConnection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (DbDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                custs.Add(reader.GetString(0) + " (" + reader.GetString(1) + ")");
                            }
                        }
                    }
                }
            }
            return custs;
        }

        private List<string> loadOrderTypes()
        {
            List<string> types = new List<string>();
            using (var mySqlConnection = new DBUtils().getDBConnection())
            {
                mySqlConnection.Open();
                using (var cmd = new MySqlCommand("load_order_types", mySqlConnection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (DbDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                types.Add(reader.GetString(1));
                            }
                        }
                    }
                }
            }
            return types;
        }

        private List<string> loadExecutors()
        {
            List<string> execs = new List<string>();
            using (var mySqlConnection = new DBUtils().getDBConnection())
            {
                mySqlConnection.Open();
                using (var cmd = new MySqlCommand("load_executors", mySqlConnection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (DbDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                execs.Add(reader.GetString(0));
                            }
                        }
                    }
                }
            }
            return execs;
        }
    }
}
