using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Windows.Forms;

namespace CRM
{
    public partial class Form1 : Form
    {
        int user_type;
        string what = "1";
        public Form1(string username, int user_tp)
        {
            InitializeComponent();
            user_type = user_tp;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = loadOrdersFromDB(what);
            dataGrid = dataGridView1;
            loadGroupTree();

            if ((user_type == 1) || (user_type == 2))
                buttonLogs.Visible = false;

            textBoxDateFrom.Text = "01-09-2019";
            textBoxDateTo.Text = DateTime.Now.ToString("dd-MM-yyyy");
        }

        List<int> accInd = new List<int>(), workInd = new List<int>(), endInd = new List<int>(), delvInd = new List<int>();
        private void loadGroupTree()
        {
            accInd.Clear();
            workInd.Clear();
            endInd.Clear();
            delvInd.Clear();
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                string cellStat = dataGridView1.Rows[i].Cells[5].Value.ToString();
                if (cellStat.Equals("Принят"))
                {
                    accInd.Add(i);
                }
                else
                if (cellStat.Equals("Выдан"))
                {
                    delvInd.Add(i);
                }
                else
                if (cellStat.Equals("Работа завершена"))
                {
                    endInd.Add(i);
                }
                else
                {
                    workInd.Add(i);
                }
            }
            treeViewGroups.Nodes[0].Text = "Все (" + (accInd.Count + workInd.Count + endInd.Count + delvInd.Count) + ")";
            treeViewGroups.Nodes[0].Nodes[0].Text = "Принятые (" + accInd.Count + ")";
            treeViewGroups.Nodes[0].Nodes[1].Text = "В работе (" + workInd.Count + ")";
            treeViewGroups.Nodes[0].Nodes[2].Text = "Завершенные (" + endInd.Count + ")";
            treeViewGroups.Nodes[0].Nodes[3].Text = "Выданные (" + delvInd.Count + ")";
            treeViewGroups.ExpandAll();
        }

        private DataTable loadOrdersFromDB(string what)
        {
            FormLoading loading = new FormLoading();
            loading.Show();

            DataTable dataTable = new DataTable();
            using (var mySqlConnection = new DBUtils().getDBConnection())
            {
                mySqlConnection.Open();
                using (var cmd = new MySqlCommand("SELECT `order_id` AS 'ИД заказа', `name` AS 'Заказчик', `info` AS 'Информация', `type` AS 'Тип', `date` AS 'Дата заказа', `status` AS 'Статус', `cost` AS 'Стоимость', `executor` AS 'Исполнитель' " +
                    "FROM Orders ords " +
                    "JOIN Customers cs on ords.customer_id = cs.customer_id and " + what, mySqlConnection))
                {
                    using (DbDataReader reader = cmd.ExecuteReader())
                    {
                        dataTable.Load(reader);
                        loading.Dispose(); 
                        return dataTable;
                    }
                }
            }
        }

        private DataTable getOrdersDataInterval(string dateFrom, string dateTo)
        {
            FormLoading loading = new FormLoading();
            loading.Show();

            dateFrom = Convert.ToDateTime(dateFrom).ToString("yyyy-MM-dd");
            dateTo = Convert.ToDateTime(dateTo).ToString("yyyy-MM-dd");

            DataTable dataTable = new DataTable();
            using (var mySqlConnection = new DBUtils().getDBConnection())
            {
                mySqlConnection.Open();
                using (var cmd = new MySqlCommand("SELECT `order_id` AS 'ИД заказа', `name` AS 'Заказчик', `info` AS 'Информация', `type` AS 'Тип', `date` AS 'Дата заказа', `status` AS 'Статус', `cost` AS 'Стоимость', `executor` AS 'Исполнитель' " +
                    "from Orders ords " +
                    "JOIN Customers cs on ords.customer_id = cs.customer_id and `date` >= '" + dateFrom + "' and `date` <= '"+ dateTo + "'", mySqlConnection))
                {
                    using (DbDataReader reader = cmd.ExecuteReader())
                    {
                        dataTable.Load(reader);
                        loading.Dispose();
                        return dataTable;
                    }
                }
            }
        }

        private void buttonNew_Click(object sender, EventArgs e)
        {
            if (!new InternetConnection().CheckForInternetConnection())
            {
                MessageBox.Show("На компьютере отсутствует интернет соединение");
                return;
            }
            AddOrder add = new AddOrder(true, user_type, 0);
            this.Activated += Form1_Activated;
            add.Show(this);
        }

        private void buttonShow_Click(object sender, EventArgs e)
        {
            if (!new InternetConnection().CheckForInternetConnection())
            {
                MessageBox.Show("На компьютере отсутствует интернет соединение");
                return;
            }
            AddOrder add = new AddOrder(false, user_type, order_id);
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

        DataGridView dataGrid = new DataGridView();
        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            if (!new InternetConnection().CheckForInternetConnection())
            {
                MessageBox.Show("На компьютере отсутствует интернет соединение");
                return;
            }
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int selIndex = dataGridView1.SelectedRows[0].Index;
                dataGridView1.DataSource = loadOrdersFromDB(what);
                dataGridView1.Rows[selIndex].Selected = true;
                if(treeViewGroups.SelectedNode.Text.Contains("Все"))
                    loadGroupTree();
            }
        }

        private void treeViewGroups_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Node.Text.Contains("Все"))
            {
                what = "1";
                dataGridView1.DataSource = loadOrdersFromDB(what);
            }
            if (e.Node.Text.Contains("Принятые"))
            {
                what = "`status` = 'Принят'";
                dataGridView1.DataSource = loadOrdersFromDB(what);
            }
            if (e.Node.Text.Contains("Выданные"))
            {
                what = "`status` = 'Выдан'";
                dataGridView1.DataSource = loadOrdersFromDB(what);
            }
            if (e.Node.Text.Contains("В работе"))
            {
                what = "(`status` = 'Подтверждение макета заказчиком' OR `status` = 'Макет подтвержден заказчиком' OR `status` = 'Ожидание внешнего подрядчика')";
                dataGridView1.DataSource = loadOrdersFromDB(what);
            }
            if (e.Node.Text.Contains("Завершенные"))
            {
                what = "`status` = 'Работа завершена'";
                dataGridView1.DataSource = loadOrdersFromDB(what);
            }
        }

        private void buttonDateSort_Click(object sender, EventArgs e)
        {
            if (!new InternetConnection().CheckForInternetConnection())
            {
                MessageBox.Show("На компьютере отсутствует интернет соединение");
                return;
            }
            if ((textBoxDateFrom.MaskCompleted) && (textBoxDateTo.MaskCompleted))
                dataGridView1.DataSource = getOrdersDataInterval(textBoxDateFrom.Text, textBoxDateTo.Text);
            else
                MessageBox.Show("Введите даты корректно");
        }

        private void buttonLogs_Click(object sender, EventArgs e)
        {
            FormAdmin adminForm = new FormAdmin();
            this.Activated += Form1_Activated;
            adminForm.Show();
        }
        
        private void dataGridView1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            AddOrder add = new AddOrder(false, user_type, order_id);
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
            if (!new InternetConnection().CheckForInternetConnection())
            {
                MessageBox.Show("На компьютере отсутствует интернет соединение");
                return;
            }
            string id = showFindOrder();
            if (id == "0")
            {
                MessageBox.Show("Заявка не найдена");
                buttonSearch.PerformClick();
            }
            else if (id == "-1")
            {
                //--
            }
            else
            {
                AddOrder add = new AddOrder(false, user_type, int.Parse(id));
                this.Activated += Form1_Activated;
                add.Show(this);
            }
        }

        private string showFindOrder()
        {
            using (var frmFindOrder = new FormFindOrder())
            {
                if (frmFindOrder.ShowDialog(this) == DialogResult.OK)
                {
                    if ((frmFindOrder.textBox1.Text.Trim().Length > 0) && (findOrder(frmFindOrder.textBox1.Text)))
                    {
                        return frmFindOrder.textBox1.Text;
                    }
                    else
                    {
                        return "0";
                    }
                }
                else if (frmFindOrder.ShowDialog(this) == DialogResult.Cancel)
                {
                    return "-1";
                }
                else
                {
                    return "-1";
                }
            }
        }

        private bool findOrder(string id)
        {
            FormLoading loading = new FormLoading();
            loading.Show();

            bool finded = false;
            using (var mySqlConnection = new DBUtils().getDBConnection())
            {
                mySqlConnection.Open();
                using (var cmd = new MySqlCommand("SELECT `order_id` FROM `Orders` WHERE `order_id` = " + id, mySqlConnection))
                {
                    if (cmd.ExecuteReader().HasRows)
                    {
                        finded = true;
                    }
                }
            }
            loading.Dispose();
            return finded;
        }
    }
}
