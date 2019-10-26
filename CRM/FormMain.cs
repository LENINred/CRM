﻿using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Data;
using System.Data.Common;
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
            dataGridView1.DataSource = loadOrdersFromDB(what);
            loadGroupTree();

            if ((user_type == 1) || (user_type == 2))
                buttonLogs.Visible = false;

            textBoxDateFrom.Text = "01-09-2019";
            textBoxDateTo.Text = DateTime.Now.ToString("dd-MM-yyyy");
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
                    MySqlDataAdapter dap = new MySqlDataAdapter(cmd);
                    dap.Fill(tblOrders);
                }
            }
            loading.Dispose();
            return tblOrders;
        }

        private DataTable getOrdersDataInterval(string dateFrom, string dateTo)
        {
            FormLoading loading = new FormLoading();
            loading.Show();

            dateFrom = Convert.ToDateTime(dateFrom).ToString("yyyy-MM-dd");
            dateTo = Convert.ToDateTime(dateTo).ToString("yyyy-MM-dd");

            DataTable tblOrders = new DataTable();
            using (var con = new DBUtils().getDBConnection())
            {
                using (var cmd = new MySqlCommand("get_orders_by_date", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new MySqlParameter("@dateFrom", MySqlDbType.Date));
                    cmd.Parameters["@dateFrom"].Value = dateFrom;
                    cmd.Parameters.Add(new MySqlParameter("@dateTo", MySqlDbType.Date));
                    cmd.Parameters["@dateTo"].Value = dateTo;
                    MySqlDataAdapter dap = new MySqlDataAdapter(cmd);
                    dap.Fill(tblOrders);
                }
            }
            loading.Dispose();
            return tblOrders;
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
        }

        private void buttonDateSort_Click(object sender, EventArgs e)
        {
            if (!checkInet()) return;
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
                FormAddOrder add = new FormAddOrder(false, user_type, int.Parse(id));
                this.Activated += Form1_Activated;
                add.Show(this);
            }
        }

        private string showFindOrder()
        {
            using (var frmFindOrder = new FormFindOrder())
            {
                DialogResult result = frmFindOrder.ShowDialog();
                if (result == DialogResult.OK)
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
                else if (result == DialogResult.Cancel)
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
            using (var con = new DBUtils().getDBConnection())
            {
                con.Open();
                using (var cmd = new MySqlCommand("find_order", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new MySqlParameter("@order_id", MySqlDbType.Int32));
                    cmd.Parameters["@order_id"].Value = id;
                    MySqlDataAdapter dap = new MySqlDataAdapter(cmd);
                    if (cmd.ExecuteReader().HasRows)
                    {
                        finded = true;
                    }
                }
            }
            loading.Dispose();
            return finded;
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
    }
}
