using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Windows.Forms;

namespace CRM
{
    public partial class FormAddOrder : Form
    {
        bool enab;
        int user_type, order_id;
        string comText;
        string log = "";
        List<string> logData = new List<string>();
        public FormAddOrder(bool enabled, int usr_tp, int ordr_tp)
        {
            InitializeComponent();
            enab = enabled;
            user_type = usr_tp;
            order_id = ordr_tp;
        }

        private void AddOrder_Load(object sender, EventArgs e)
        {
            comboBoxCustomers.Enabled = enab;
            textBoxCustomer.Enabled = false;
            textBoxPriorComm.Enabled = false;
            textBoxSubComm.Enabled = false;
            checkBoxSubComm.Enabled = false;
            richTextBoxOrderInfo.Enabled = enab;
            radioButtonExistCustomer.Enabled = enab;
            radioButtonNewCustomer.Enabled = enab;
            comboBoxCustomers.Items.AddRange(loadCustomers().ToArray());
            comboBoxExecutor.Items.AddRange(loadExecutors().ToArray());
            comboBoxOrderType.Items.AddRange(loadOrderTypes().ToArray());

            comboBoxOrderStatus.Items.Add("Принят");
            comboBoxOrderStatus.Items.Add("Выдан");
            log = "Продавцы";

            if ((user_type == 2) || (user_type == 3))
            {
                log = "Дизайнеры";
                comboBoxOrderStatus.Items.Add("Подтверждение макета заказчиком");
                comboBoxOrderStatus.Items.Add("Макет подтвержден заказчиком");
                comboBoxOrderStatus.Items.Add("Ожидание внешнего подрядчика");
                comboBoxOrderStatus.Items.Add("Работа завершена");
            }
            if (order_id > 0)
            {
                this.Text = "Изменение заказа";
                comText = "change_the_order";

                richTextBoxOrderInfo.Enabled = true;
                textBoxCustomer.Enabled = false;

                radioButtonExistCustomer.Enabled = false;
                radioButtonNewCustomer.Checked = true;
                comboBoxCustomers.Enabled = false;
                if (user_type != 3)
                {
                    textBoxDate.Enabled = false;
                    comboBoxOrderType.Enabled = false;
                }
                buttonAddOrder.Text = "Сохранить";
            }
            else 
                comText = "add_new_order";

            loadOrderData();
            logData.Add(textBoxPriorComm.Text);
            logData.Add(textBoxSubComm.Text);
            logData.Add(richTextBoxOrderInfo.Text);
            logData.Add(textBoxCost.Text);
            logData.Add(comboBoxOrderType.Text);
            logData.Add(comboBoxOrderStatus.Text);
            logData.Add(comboBoxExecutor.Text);
            logData.Add(textBoxDate.Text);
        }

        private void loadOrderData()
        {
            DataTable tblOrders = new DataTable();
            using (var mySqlConnection = new DBUtils().getDBConnection())
            {
                mySqlConnection.Open();
                using (var cmd = new MySqlCommand("get_order_info", mySqlConnection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new MySqlParameter("@order_id", MySqlDbType.VarChar));
                    cmd.Parameters["@order_id"].Value = order_id;
                    using (DbDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                textBoxCustomer.Enabled = false;
                                richTextBoxOrderInfo.Text = reader.GetString(0);
                                comboBoxOrderType.Text = reader.GetString(1);
                                if (!comboBoxOrderStatus.Items.Contains(reader.GetString(2)))
                                    comboBoxOrderStatus.Items.Add(reader.GetString(2));
                                comboBoxOrderStatus.Text = reader.GetString(2);
                                textBoxCost.Text = reader.GetString(3);
                                textBoxDate.Text = Convert.ToDateTime(reader.GetString(4)).ToString("MM-dd-yyyy HH:mm");
                                comboBoxExecutor.Text = reader.GetString(5);
                                textBoxCustomer.Text = reader.GetString(6);
                                textBoxPriorComm.Text = reader.GetString(7);
                                textBoxSubComm.Text = reader.GetString(8);
                            }
                        }
                    }
                }
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

        private int getLastOrderID()
        {
            using (var mySqlConnection = new DBUtils().getDBConnection())
            {
                mySqlConnection.Open();
                using (var cmd = new MySqlCommand("get_last_order_id", mySqlConnection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (DbDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                try
                                {
                                    return reader.GetInt32(0);
                                }
                                catch(System.Data.SqlTypes.SqlNullValueException)
                                {
                                    return 0;
                                }
                            }
                        }
                    }
                }
            }
            return -1;
        }

        private void buttonAddOrder_Click(object sender, EventArgs e)
        {
            if (!checkInet()) return;
            FormLoading loading = new FormLoading();
            loading.Show();

            string custName, comm, subcomm = "-";
            if (radioButtonExistCustomer.Checked)
            {
                if (comboBoxCustomers.SelectedIndex != -1)
                {
                    string selectedCustomer = comboBoxCustomers.SelectedItem.ToString();
                    custName = selectedCustomer.Substring(0, selectedCustomer.IndexOf('('));
                    comm = selectedCustomer.Substring(selectedCustomer.IndexOf('(') + 1, selectedCustomer.IndexOf(')') - selectedCustomer.IndexOf('(') - 1);
                }
                else
                {
                    MessageBox.Show("Выберите заказчика");
                    loading.Dispose();
                    return;
                }
            }
            else
            {
                custName = textBoxCustomer.Text;
                comm = textBoxPriorComm.Text;
                if (checkBoxSubComm.Checked)
                    subcomm = textBoxSubComm.Text.ToString();
            }

            if (!checkCustomerExist(custName))
                addNewCustomer(custName, comm, subcomm);

            if ((comboBoxOrderStatus.SelectedIndex != -1) && (comboBoxOrderType.SelectedIndex != -1) && (comboBoxExecutor.SelectedIndex != -1))
            {
                if (!custName.Trim().Equals(""))
                {
                    if (!comm.Trim().Equals(""))
                    {
                        if (richTextBoxOrderInfo.Text.Length > 10)
                        {
                            if (textBoxCost.Text.Length > 1)
                            {
                                if (order_id == 0) order_id = getLastOrderID() + 1;
                                else
                                {
                                    string l = "";
                                    if (!logData[0].Equals(textBoxPriorComm.Text))
                                        l += "Вид связи: " + logData[0] + " -> " + textBoxPriorComm.Text + "\n";
                                    if (!logData[1].Equals(textBoxSubComm.Text))
                                        l += "Доп вид связи: " + logData[1] + " -> " + textBoxSubComm.Text + "\n";
                                    if (!logData[2].Equals(richTextBoxOrderInfo.Text))
                                        l += "Инфо заказа: " + logData[2] + " -> " + richTextBoxOrderInfo.Text + "\n";
                                    if (!logData[3].Equals(textBoxCost.Text))
                                        l += "Цена: " + logData[3] + " -> " + textBoxCost.Text + "\n";
                                    if (!logData[4].Equals(comboBoxOrderType.Text))
                                        l += "Тип заказа: " + logData[4] + " -> " + comboBoxOrderType.Text + "\n";
                                    if (!logData[5].Equals(comboBoxOrderStatus.Text))
                                        l += "Статус заказа: " + logData[5] + " -> " + comboBoxOrderStatus.Text + "\n";
                                    if (!logData[6].Equals(comboBoxExecutor.Text))
                                        l += "Исполнитель: " + logData[6] + " -> " + comboBoxExecutor.Text + "\n";
                                    if (!logData[7].Equals(textBoxDate.Text))
                                        l += "Дата дедлайна:" + logData[7] + " -> " + textBoxDate.Text + "\n";
                                    if (l.Length > 0)
                                    {
                                        log += " " + DateTime.Now.ToString("yyyy-MM-dd") + " изменили заявку No_" + order_id + ": \n";
                                        log += l;
                                        using (var mySqlConnection = new DBUtils().getDBConnection())
                                        {
                                            using (var cmd = new MySqlCommand())
                                            {
                                                cmd.Connection = mySqlConnection;
                                                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                                                cmd.CommandText = "addLog";
                                                cmd.Parameters.Clear();
                                                MySqlParameter p1 = cmd.Parameters.Add("@log", MySqlDbType.VarChar);
                                                p1.Direction = ParameterDirection.Input;

                                                p1.Value = log;

                                                mySqlConnection.Open();
                                                cmd.ExecuteNonQuery();
                                            }
                                        }
                                    }
                                }

                                if (openFileDialog1.FileName != "")
                                {
                                    UploadFtpFile("" + order_id, openFileDialog1.FileNames);
                                }

                                using (var mySqlConnection = new DBUtils().getDBConnection())
                                {
                                    using (var cmd = new MySqlCommand())
                                    {
                                        cmd.Connection = mySqlConnection;
                                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                                        cmd.CommandText = comText;
                                        cmd.Parameters.Clear();
                                        MySqlParameter p1 = cmd.Parameters.Add("@customer", MySqlDbType.VarChar);
                                        p1.Direction = ParameterDirection.Input;
                                        MySqlParameter p2 = cmd.Parameters.Add("@order_info", MySqlDbType.VarChar);
                                        p2.Direction = ParameterDirection.Input;
                                        MySqlParameter p3 = cmd.Parameters.Add("@order_status", MySqlDbType.VarChar);
                                        p3.Direction = ParameterDirection.Input;
                                        MySqlParameter p4 = cmd.Parameters.Add("@order_type", MySqlDbType.VarChar);
                                        p4.Direction = ParameterDirection.Input;
                                        MySqlParameter p5 = cmd.Parameters.Add("@executor", MySqlDbType.VarChar);
                                        p5.Direction = ParameterDirection.Input;
                                        MySqlParameter p6 = cmd.Parameters.Add("@cost", MySqlDbType.VarChar);
                                        p6.Direction = ParameterDirection.Input;
                                        MySqlParameter p7 = cmd.Parameters.Add("@communication", MySqlDbType.VarChar);
                                        p7.Direction = ParameterDirection.Input;
                                        MySqlParameter p8 = cmd.Parameters.Add("@subCommunication", MySqlDbType.VarChar);
                                        p8.Direction = ParameterDirection.Input;
                                        MySqlParameter p9 = cmd.Parameters.Add("@orderId", MySqlDbType.Int32);
                                        p9.Direction = ParameterDirection.Input;
                                        MySqlParameter p10 = cmd.Parameters.Add("@deadline", MySqlDbType.VarChar);
                                        p10.Direction = ParameterDirection.Input;

                                        p1.Value = custName.TrimStart();
                                        p2.Value = richTextBoxOrderInfo.Text.TrimStart();
                                        p3.Value = comboBoxOrderStatus.SelectedItem.ToString();
                                        p4.Value = comboBoxOrderType.SelectedItem.ToString();
                                        p5.Value = comboBoxExecutor.SelectedItem.ToString();
                                        p6.Value = textBoxCost.Text.ToString();
                                        p7.Value = textBoxPriorComm.Text.TrimStart();
                                        p8.Value = textBoxSubComm.Text.TrimStart();
                                        p9.Value = order_id;
                                        p10.Value = Convert.ToDateTime(textBoxDate.Text).ToString("yyyy-dd-MM HH:mm");
                                        mySqlConnection.Open();
                                        cmd.ExecuteNonQuery();
                                    }
                                }
                                MessageBox.Show("Заявка номер "+ order_id + " составлена");
                                this.Close();
                            }
                            else
                            {
                                MessageBox.Show("Введите стоимость услуги");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Информация о заявке должна быть длиннее десяти символов");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Введите хотябы основной вид связи");
                    }
                }
                else
                {
                    MessageBox.Show("Введите Ф.И.О заказчика");
                }
            }
            else
            {
                MessageBox.Show("Выберите статус, тип и исполнителя заявки");
            }
            loading.Dispose();
        }

        private bool checkCustomerExist(string customer)
        {
            using (var con = new DBUtils().getDBConnection())
            {
                con.Open();
                using (var cmd = new MySqlCommand("check_customer_exist", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new MySqlParameter("@customer", MySqlDbType.VarChar));
                    cmd.Parameters["@customer"].Value = customer;
                    MySqlDataAdapter dap = new MySqlDataAdapter(cmd);
                    if (cmd.ExecuteReader().HasRows)
                    {
                        return true;
                    }
                    else return false;
                }
            }
        }

        private void addNewCustomer(string customer, string comm, string sub_comm)
        {
            using (var mySqlConnection = new DBUtils().getDBConnection())
            {
                using (var cmd = new MySqlCommand())
                {
                    cmd.Connection = mySqlConnection;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandText = "add_new_customer";
                    cmd.Parameters.Clear();
                    MySqlParameter p1 = cmd.Parameters.Add("@customer", MySqlDbType.VarChar);
                    p1.Direction = ParameterDirection.Input;
                    MySqlParameter p2 = cmd.Parameters.Add("@comm", MySqlDbType.VarChar);
                    p2.Direction = ParameterDirection.Input;
                    MySqlParameter p3 = cmd.Parameters.Add("@sub_comm", MySqlDbType.VarChar);
                    p3.Direction = ParameterDirection.Input;

                    p1.Value = customer;
                    p2.Value = comm;
                    p3.Value = sub_comm;

                    mySqlConnection.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonExistCustomer.Checked)
            {
                comboBoxCustomers.Enabled = true;
                textBoxCustomer.Enabled = false;
                textBoxPriorComm.Enabled = false;
                textBoxSubComm.Enabled = false;
                checkBoxSubComm.Enabled = false;
            }
            else
            {
                comboBoxCustomers.Enabled = false;
                textBoxCustomer.Enabled = true;
                textBoxPriorComm.Enabled = true;
                if (checkBoxSubComm.Checked)
                {
                    textBoxSubComm.Enabled = true;
                }
                else
                {
                    textBoxSubComm.Enabled = false;
                }
                checkBoxSubComm.Enabled = true;
            }
        }

        private void buttonAddFile_Click(object sender, EventArgs e)
        {
            if (!checkInet()) return;
            openFileDialog1.Filter = "jpg files (*.jpg)|*.jpg|bmp files (*.bmp)|*.bmp|png files (*.png)|*.png";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                //--
            }
        }

        public void UploadFtpFile(string ordr_id, string[] fileNames)
        {
            string absoluteFileName = "";

            if (user_type == 1)
            {
                absoluteFileName = "/startImage/";
            }
            else if ((user_type == 2) || (user_type == 3))
            {
                absoluteFileName = "/endImage/";
            }

            WebRequest request = WebRequest.Create("ftp://83.220.174.171/order_" + ordr_id);
            request.Method = WebRequestMethods.Ftp.MakeDirectory;
            request.Credentials = new NetworkCredential("seller", "Az123456!");
            try
            {
                request.GetResponse();
            }
            catch {  }

            request = WebRequest.Create("ftp://83.220.174.171/order_" + ordr_id + absoluteFileName);
            request.Method = WebRequestMethods.Ftp.MakeDirectory;
            request.Credentials = new NetworkCredential("seller", "Az123456!");
            try
            {
                request.GetResponse();
            }
            catch { }

            using (WebClient client = new WebClient())
            {
                client.Credentials = new NetworkCredential("seller", "Az123456!");
                int i = 1;
                foreach (string file in fileNames)
                {
                    client.UploadFile("ftp://83.220.174.171/order_" + ordr_id + "" + absoluteFileName + i + Path.GetExtension(file), "STOR", file);
                    i++;
                }
            }
        }

        private void buttonDownloadFiles_Click(object sender, EventArgs e)
        {
            if (!checkInet()) return;
            FormLoading formLoading = new FormLoading();
            formLoading.Show();
            downloadFiles(true);
            downloadFiles(false);
            try
            {
                Process.Start(@"C:\\Users\Public\orders\order_" + order_id);
                formLoading.Dispose();
            }
            catch
            {
                formLoading.Dispose();
                MessageBox.Show("Файлы отсутствуют");
            }
            
        }

        private void downloadFiles(bool who)
        {
            string localDir = "", ftpDir = "";
            if (who)
            {
                localDir = "\\start\\";
                ftpDir = "/startImage/";
            }
            else
            {
                localDir = "\\end\\";
                ftpDir = "/endImage/";
            }
            StreamReader streamReader;
            List<string> directories;
            try
            {
                FtpWebRequest ftpRequest = (FtpWebRequest)WebRequest.Create("ftp://83.220.174.171/order_" + order_id + ftpDir);
                ftpRequest.Credentials = new NetworkCredential("seller", "Az123456!");
                ftpRequest.Method = WebRequestMethods.Ftp.ListDirectory;
                FtpWebResponse response = (FtpWebResponse)ftpRequest.GetResponse();
                streamReader = new StreamReader(response.GetResponseStream());
                directories = new List<string>();
            }
            catch
            {
                return;
            }

            string line = streamReader.ReadLine();
            while (!string.IsNullOrEmpty(line))
            {
                directories.Add(line);
                line = streamReader.ReadLine();
            }
            streamReader.Close();

            Directory.CreateDirectory(@"C:\\Users\Public\orders\order_" + order_id);
            Directory.CreateDirectory(@"C:\\Users\Public\orders\order_" + order_id + localDir);

            using (WebClient ftpClient = new WebClient())
            {
                ftpClient.Credentials = new System.Net.NetworkCredential("seller", "Az123456!");

                for (int i = 0; i <= directories.Count - 1; i++)
                {
                    if (directories[i].Contains("."))
                    {

                        string path = "ftp://83.220.174.171/order_" + order_id + ftpDir + directories[i].ToString();
                        string trnsfrpth = @"C:\\Users\Public\orders\order_" + order_id + localDir + directories[i].ToString();
                        ftpClient.DownloadFile(path, trnsfrpth);
                    }
                }
            }
        }

        private void comboBoxOrderType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((comboBoxOrderType.SelectedItem.ToString().Contains("Печать на ")) && (!comboBoxOrderType.SelectedItem.ToString().Equals("Печать на принтере")))
                textBoxDate.Text = DateTime.Now.AddDays(2).ToString("dd-MM-yyyy HH:mm");
            else if (comboBoxOrderType.SelectedItem.ToString().Contains("Металло"))
                textBoxDate.Text = DateTime.Now.AddDays(14).ToString("dd-MM-yyyy HH:mm");
            else if (comboBoxOrderType.SelectedItem.ToString().Contains("Оциф"))
                textBoxDate.Text = DateTime.Now.AddDays(14).ToString("dd-MM-yyyy HH:mm");
            else if (comboBoxOrderType.SelectedItem.ToString().Contains("Фотокн"))
                textBoxDate.Text = DateTime.Now.AddDays(14).ToString("dd-MM-yyyy HH:mm");
            else textBoxDate.Text = DateTime.Now.AddDays(14).ToString("dd-MM-yyyy HH:mm");
        }

        private void comboBoxOrderStatus_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void textBoxCost_TextChanged(object sender, EventArgs e)
        {
            string actualdata = string.Empty;
            char[] entereddata = textBoxCost.Text.ToCharArray();
            foreach (char aChar in entereddata)
            {
                if (Char.IsDigit(aChar))
                {
                    actualdata += aChar;
                }
                else
                {
                    actualdata.Replace(aChar, ' ');
                    actualdata.Trim();
                }
            }
            textBoxCost.Text = actualdata;
            textBoxCost.SelectionStart = textBoxCost.Text.Length;
            textBoxCost.SelectionLength = 0;
        }

        private void textBoxCustomer_TextChanged(object sender, EventArgs e)
        {
            string actualdata = string.Empty;
            char[] entereddata = textBoxCustomer.Text.ToCharArray();
            foreach (char aChar in entereddata)
            {
                if ((Char.IsLetter(aChar)) || (aChar == ' '))
                {
                    actualdata += aChar;
                }
                else
                {
                    actualdata.Replace(aChar, ' ');
                    actualdata.Trim();
                }
            }
            textBoxCustomer.Text = actualdata;
            textBoxCustomer.SelectionStart = textBoxCustomer.Text.Length;
            textBoxCustomer.SelectionLength = 0;
        }

        private void checkBoxDop_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxSubComm.Checked)
            {
                textBoxSubComm.Enabled = true;
            }
            else
            {
                textBoxSubComm.Enabled = false;
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
    }
}
