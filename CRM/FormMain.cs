using ClosedXML.Excel;
using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Diagnostics;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;

namespace CRM
{
    public partial class FormMain : Form
    {
        int user_type;
        string what = "1";
        public FormMain(string username, int user_tp)
        {
            InitializeComponent();
            user_type = user_tp;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if ((user_type == 1) || (user_type == 2))
                buttonLogs.Visible = false;

            dateTimePickerFrom.Text = DateTime.Now.AddDays(-7).ToString("dd-MM-yyyy");
            dateTimePickerTo.Text = DateTime.Now.ToString("dd-MM-yyyy");

            dataGridView1.DataSource = loadOrdersFromDB(what);
            loadGroupTree();

            new Thread(() => setLogInTime()).Start();
        }

        private void loadGroupTree()
        {
            int acc = 0, work = 0, end = 0, delv = 0;
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                switch (dataGridView1.Rows[i].Cells[5].Value.ToString())
                {
                    case "Принят":
                        acc++;
                        break;
                    case "Выдан":
                        delv++;
                        break;
                    case "Работа завершена":
                        end++;
                        break;
                    default:
                        work++;
                        break;
                }
            }
            treeViewGroups.Nodes[0].Text = "Все (" + (acc + work + end + delv) + ")";
            treeViewGroups.Nodes[0].Nodes[0].Text = "Принятые (" + acc + ")";
            treeViewGroups.Nodes[0].Nodes[1].Text = "В работе (" + work + ")";
            treeViewGroups.Nodes[0].Nodes[2].Text = "Завершенные (" + end + ")";
            treeViewGroups.Nodes[0].Nodes[3].Text = "Выданные (" + delv + ")";
            treeViewGroups.ExpandAll();
        }

        private DataTable loadOrdersFromDB(string what)
        {
            FormLoading loading = new FormLoading();
            loading.Show();

            DataTable tblOrders = new DataTable();
            using (var con = new DBUtils().getDBConnection()){
                using (var cmd = new MySqlCommand("get_orders", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new MySqlParameter("@what", MySqlDbType.VarChar));
                    cmd.Parameters["@what"].Value = what;
                    cmd.Parameters.Add(new MySqlParameter("@dateFrom", MySqlDbType.VarChar));
                    cmd.Parameters["@dateFrom"].Value = Convert.ToDateTime(dateTimePickerFrom.Text).ToString("yyyy-MM-dd"); ;
                    cmd.Parameters.Add(new MySqlParameter("@dateTo", MySqlDbType.VarChar));
                    cmd.Parameters["@dateTo"].Value = Convert.ToDateTime(dateTimePickerTo.Text).ToString("yyyy-MM-dd");
                    MySqlDataAdapter dap = new MySqlDataAdapter(cmd);
                    dap.Fill(tblOrders);
                }
            }
            loading.Dispose();
            return tblOrders;
        }

        private void setLogInTime()
        {
            using (var con = new DBUtils().getDBConnection())
            {
                using (var cmd = new MySqlCommand("set_login_time", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new MySqlParameter("@ip", MySqlDbType.VarChar));
                    cmd.Parameters["@ip"].Value = new System.Net.WebClient().DownloadString("https://api.ipify.org");
                    cmd.Parameters.Add(new MySqlParameter("@time", MySqlDbType.Date));
                    cmd.Parameters["@time"].Value = DateTime.Now.ToString("yyyy-MM-dd"); ;
                    cmd.Parameters.Add(new MySqlParameter("@app_ver", MySqlDbType.VarChar));
                    cmd.Parameters["@app_ver"].Value = FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).ProductVersion;
                    cmd.Parameters.Add(new MySqlParameter("@pc_name", MySqlDbType.VarChar));
                    cmd.Parameters["@pc_name"].Value = Environment.MachineName;
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void buttonNew_Click(object sender, EventArgs e)
        {
            if (!checkInet()) return;
            FormAddOrder add = new FormAddOrder(true, user_type, 0);
            this.Activated += Form1_Activated;
            add.Show(this);
        }

        private void buttonShow_Click(object sender, EventArgs e)
        {
            if (!checkInet()) return;
            FormAddOrder add = new FormAddOrder(false, user_type, order_id);
            this.Activated += Form1_Activated;
            add.Show(this);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        int order_id = 1;
        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            buttonShow.Enabled = true;
            if(dataGridView1.SelectedRows.Count > 0)
                order_id = Int32.Parse(dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            if (!checkInet()) return;
            int selIndex = 0;
            if (dataGridView1.SelectedRows.Count > 0)
            {
                selIndex = dataGridView1.SelectedRows[0].Index;
            }
            dataGridView1.DataSource = loadOrdersFromDB(what);
            if (dataGridView1.SelectedRows.Count > 0)
            {
                dataGridView1.Rows[selIndex].Selected = true;
            }
            if (treeViewGroups.SelectedNode.Text.Contains("Все"))
                loadGroupTree();
        }

        private void treeViewGroups_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            switch (e.Node.Text.Split('(')[0].Trim())
            {
                case "Все":
                    what = "1";
                    dataGridView1.DataSource = loadOrdersFromDB(what);
                    break;
                case "Принятые":
                    what = "Принят";
                    dataGridView1.DataSource = loadOrdersFromDB(what);
                    break;
                case "Выданные":
                    what = "Выдан";
                    dataGridView1.DataSource = loadOrdersFromDB(what);
                    break;
                case "В работе":
                    what = "('Подтверждение макета заказчиком' OR `status` = 'Макет подтвержден заказчиком' OR `status` = 'Ожидание внешнего подрядчика')";
                    dataGridView1.DataSource = loadOrdersFromDB(what);
                    break;
                case "Завершенные":
                    what = "Работа завершена";
                    dataGridView1.DataSource = loadOrdersFromDB(what);
                    break;
            }
            loadGroupTree();
        }

        private void buttonLogs_Click(object sender, EventArgs e)
        {
            FormAdmin adminForm = new FormAdmin();
            this.Activated += Form1_Activated;
            adminForm.Show();
        }
        
        private void dataGridView1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            FormAddOrder add = new FormAddOrder(false, user_type, order_id);
            this.Activated += Form1_Activated;
            add.Show(this);
        }

        private void Form1_Activated(object sender, EventArgs e)
        {
            buttonUpdate.PerformClick();
            this.Activated -= Form1_Activated;
        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            if (!checkInet()) return;

            try
            {
                DataTable table = showFindAnyOrder();
                dataGridView1.DataSource = table;
            }
            catch
            {
                MessageBox.Show("Заявка не найдена");
                buttonSearch.PerformClick();
            }
        }

        private DataTable showFindAnyOrder()
        {
            using (var frmFindOrder = new FormFindOrder())
            {
                DialogResult result = frmFindOrder.ShowDialog();
                if (result == DialogResult.OK)
                {
                    DataTable table = findAnyOrder(frmFindOrder.textBox1.Text);
                    if ((frmFindOrder.textBox1.Text.Trim().Length > 0) && (table.Rows.Count > 0))
                    {
                        return table;
                    }
                    else
                    {
                        return table;
                    }
                }
                else if (result == DialogResult.Cancel)
                {
                    return null;
                }
                else
                {
                    return null;
                }
            }
        }

        private DataTable findAnyOrder(string text)
        {
            DataTable tblOrders = new DataTable();
            using (var con = new DBUtils().getDBConnection())
            {
                using (var cmd = new MySqlCommand("find_any_order", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new MySqlParameter("@text", MySqlDbType.VarChar));
                    cmd.Parameters["@text"].Value = text;
                    MySqlDataAdapter dap = new MySqlDataAdapter(cmd);
                    dap.Fill(tblOrders);
                    return tblOrders;
                }
            }
        }

        private bool checkInet()
        {
            if (!new InternetConnection().CheckForInternetConnection())
            {
                MessageBox.Show("На компьютере отсутствует интернет соединение");
                return false;
            }
            else return true;
        }

        private void labelInfo_Click(object sender, EventArgs e)
        {
            FormDevInfo devInfo = new FormDevInfo();
            devInfo.ShowDialog(this);
        }

        private void buttonTableExport_Click(object sender, EventArgs e)
        {
            FormTableExport tableExport = new FormTableExport();
            tableExport.ShowDialog();
        }

        private void buttonCustomersExport_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Filter = "Excel файл (*.xlsx)|*.xlsx";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string filename = saveFileDialog1.FileName;

                DataTable tblOrders = new DataTable();
                using (var mySqlConnection = new DBUtils().getDBConnection())
                {
                    using (var cmd = new MySqlCommand())
                    {
                        cmd.Connection = mySqlConnection;
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.CommandText = "load_customers";

                        mySqlConnection.Open();
                        MySqlDataAdapter dap = new MySqlDataAdapter(cmd);
                        dap.Fill(tblOrders);
                    }
                }

                XLWorkbook wb = new XLWorkbook();
                wb.Worksheets.Add(tblOrders, "Список клиентов");
                wb.SaveAs(filename);

                MessageBox.Show("Данные выгружены");
            }
        }

        private void dateTimePickerTo_CloseUp(object sender, EventArgs e)
        {
            dataGridView1.DataSource = loadOrdersFromDB(what);
            loadGroupTree();
        }

        private void dateTimePickerFrom_CloseUp(object sender, EventArgs e)
        {
            dataGridView1.DataSource = loadOrdersFromDB(what);
            loadGroupTree();
        }
    }
}
