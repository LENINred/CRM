using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace CRM
{
    public partial class FormAddOrder : Form
    {
        bool enab;
        int ord_type;
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
            ord_type = ordr_tp;
        }

        private void AddOrder_Load(object sender, EventArgs e)
        {
            comboBoxCustomers.Enabled = enab;
            textBoxCustomer.Enabled = false;
            textBoxPriorComm.Enabled = false;
            textBoxSubComm.Enabled = false;
            richTextBoxOrderInfo.Enabled = enab;
            radioButtonExistCustomer.Enabled = enab;
            radioButtonNewCustomer.Enabled = enab;
            comboBoxCustomers.Items.AddRange(loadCustomers().ToArray());
            List<string> users = loadExecutors();
            comboBoxExecutor.Items.AddRange(users.ToArray());
            comboBoxAcceptor.Items.AddRange(users.ToArray());
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
            logData.Add(textBoxFactCost.Text);
            logData.Add(comboBoxOrderType.Text);
            logData.Add(comboBoxOrderStatus.Text);
            logData.Add(comboBoxExecutor.Text);
            logData.Add(comboBoxAcceptor.Text);
            logData.Add(textBoxDate.Text);
            logData.Add(comboBoxPointOfGrub.Text);
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
                                textBoxFactCost.Text = reader.GetString(4);
                                textBoxDate.Text = reader.GetString(5);
                                comboBoxExecutor.Text = reader.GetString(6);
                                comboBoxAcceptor.Text = reader.GetString(7);
                                textBoxCustomer.Text = reader.GetString(8);
                                textBoxPriorComm.Text = reader.GetString(9);
                                textBoxSubComm.Text = reader.GetString(10);
                                comboBoxPointOfGrub.Text = reader.GetString(11);
                                checkBoxCustNotif.Enabled = reader.GetBoolean(12);
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
                                catch (System.Data.SqlTypes.SqlNullValueException)
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
                subcomm = textBoxSubComm.Text.ToString();
            }

            if (!checkCustomerExist(custName))
                addNewCustomer(custName, comm, subcomm);

            if ((comboBoxOrderStatus.SelectedIndex != -1) && (comboBoxOrderType.SelectedIndex != -1) && (comboBoxAcceptor.SelectedIndex != -1) && (comboBoxPointOfGrub.SelectedIndex != -1))
            {
                if (!custName.Trim().Equals(""))
                {
                    if (!comm.Trim().Equals(""))
                    {
                        if (!subcomm.Trim().Equals(""))
                        {
                            if (richTextBoxOrderInfo.Text.Length > 10)
                            {
                                if (textBoxCost.Text.Length > 1)
                                {
                                    if (textBoxFactCost.Text.Length > 1)
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
                                            if (!logData[4].Equals(textBoxFactCost.Text))
                                                l += "Фактический оплаченныя сумма: " + logData[4] + " -> " + textBoxFactCost.Text + "\n";
                                            if (!logData[5].Equals(comboBoxOrderType.Text))
                                                l += "Тип заказа: " + logData[5] + " -> " + comboBoxOrderType.Text + "\n";
                                            if (!logData[6].Equals(comboBoxOrderStatus.Text))
                                                l += "Статус заказа: " + logData[6] + " -> " + comboBoxOrderStatus.Text + "\n";
                                            if (!logData[7].Equals(comboBoxExecutor.Text))
                                                l += "Исполнитель: " + logData[7] + " -> " + comboBoxExecutor.Text + "\n";
                                            if (!logData[8].Equals(comboBoxExecutor.Text))
                                                l += "Принявший заказ: " + logData[8] + " -> " + comboBoxAcceptor.Text + "\n";
                                            if (!logData[9].Equals(textBoxDate.Text))
                                                l += "Дата дедлайна:" + logData[9] + " -> " + textBoxDate.Text + "\n";
                                            if (!logData[10].Equals(textBoxDate.Text))
                                                l += "Точка выдачи:" + logData[10] + " -> " + comboBoxPointOfGrub.Text + "\n";
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
                                            uploadFiles("" + order_id, openFileDialog1.FileNames);
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
                                                MySqlParameter p6 = cmd.Parameters.Add("@acceptor", MySqlDbType.VarChar);
                                                p6.Direction = ParameterDirection.Input;
                                                MySqlParameter p7 = cmd.Parameters.Add("@cost", MySqlDbType.VarChar);
                                                p7.Direction = ParameterDirection.Input;
                                                MySqlParameter p8 = cmd.Parameters.Add("@fact_cost", MySqlDbType.VarChar);
                                                p8.Direction = ParameterDirection.Input;
                                                MySqlParameter p9 = cmd.Parameters.Add("@communication", MySqlDbType.VarChar);
                                                p9.Direction = ParameterDirection.Input;
                                                MySqlParameter p10 = cmd.Parameters.Add("@subCommunication", MySqlDbType.VarChar);
                                                p10.Direction = ParameterDirection.Input;
                                                MySqlParameter p11 = cmd.Parameters.Add("@orderId", MySqlDbType.Int32);
                                                p11.Direction = ParameterDirection.Input;
                                                MySqlParameter p12 = cmd.Parameters.Add("@deadline", MySqlDbType.VarChar);
                                                p12.Direction = ParameterDirection.Input;
                                                MySqlParameter p13 = cmd.Parameters.Add("@point", MySqlDbType.VarChar);
                                                p13.Direction = ParameterDirection.Input;
                                                MySqlParameter p14 = cmd.Parameters.Add("@cust_notif", MySqlDbType.VarChar);
                                                p14.Direction = ParameterDirection.Input;

                                                p1.Value = custName.TrimStart();
                                                p2.Value = richTextBoxOrderInfo.Text.TrimStart();
                                                p3.Value = comboBoxOrderStatus.SelectedItem.ToString();
                                                p4.Value = comboBoxOrderType.SelectedItem.ToString();
                                                p5.Value = comboBoxExecutor.SelectedItem.ToString();
                                                p6.Value = comboBoxAcceptor.SelectedItem.ToString();
                                                p7.Value = textBoxCost.Text.ToString();
                                                p8.Value = textBoxFactCost.Text.ToString();
                                                p9.Value = textBoxPriorComm.Text.TrimStart();
                                                p10.Value = textBoxSubComm.Text.TrimStart();
                                                p11.Value = order_id;
                                                p12.Value = textBoxDate.Text;
                                                p13.Value = comboBoxPointOfGrub.Text;
                                                p14.Value = checkBoxCustNotif.Checked;
                                                mySqlConnection.Open();
                                                cmd.ExecuteNonQuery();
                                            }
                                        }
                                        string json = "";
                                        if (ord_type == 0)
                                        {
                                            json = @"{" +
                                               "\"ordertype\":\"new\"," +
                                               "\"order_id\":\"" + order_id + "\"," +
                                               "\"deadline\":\"" + textBoxDate.Text + "\"," +
                                               "\"cust_name\":\"" + custName.TrimStart() + "\"," +
                                               "\"cust_phone\":\"" + textBoxPriorComm.Text.TrimStart() + "\"" + "}";
                                        }
                                        else
                                        {
                                            if (comboBoxOrderStatus.Text == "Ожидание внешнего подрядчика")
                                            {
                                                json = @"{" +
                                               "\"ordertype\":\"ext\"," +
                                               "\"order_id\":\"" + order_id + "\"," +
                                               "\"deadline\":\"" + textBoxDate.Text + "\"," +
                                               "\"cust_name\":\"" + custName.TrimStart() + "\"," +
                                               "\"cust_phone\":\"" + textBoxPriorComm.Text.TrimStart() + "\"" + "}";

                                            }
                                        }
                                        try
                                        {
                                            ClientLaunchAsync(json);
                                        }
                                        catch { MessageBox.Show("Оповещение о создании/изменении заявки не отправлено, обратитесь к руководителю либо напишите разработчику из меню сообщения об ошибке"); }
                                        MessageBox.Show("Заявка номер " + order_id + " составлена");
                                        this.Close();
                                    }
                                    else
                                    {
                                        MessageBox.Show("Введите фактически оплаченную сумму");
                                    }
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
                            MessageBox.Show("Почта заказчика - обязательное поле для ввода");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Номер телефона заказчика - обязательное поле для ввода");
                    }
                }
                else
                {
                    MessageBox.Show("Введите Ф.И.О заказчика");
                }
            }
            else
            {
                MessageBox.Show("Выберите статус, тип и сотрудника принявшего заявку");
            }
            loading.Dispose();
        }

        private static async void ClientLaunchAsync(string message)
        {
            ClientWebSocket webSocket = null;
            webSocket = new ClientWebSocket();
            await webSocket.ConnectAsync(new Uri("ws://83.220.174.171:18357"), CancellationToken.None);

            // Do something with WebSocket

            var arraySegment = new ArraySegment<byte>(Encoding.UTF8.GetBytes(message));
            await webSocket.SendAsync(arraySegment, WebSocketMessageType.Text, true, CancellationToken.None);
            await webSocket.CloseOutputAsync(WebSocketCloseStatus.NormalClosure, "ItsOk", CancellationToken.None);
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
            }
            else
            {
                comboBoxCustomers.Enabled = false;
                textBoxCustomer.Enabled = true;
                textBoxPriorComm.Enabled = true;
                textBoxSubComm.Enabled = true;
            }
        }

        private void buttonAddFile_Click(object sender, EventArgs e)
        {
            if (!checkInet()) return;
            openFileDialog1.Filter = "jpg files (*.jpg)|*.jpg|bmp files (*.bmp)|*.bmp|png files (*.png)|*.png|psd files (*.psd)|*.psd";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                //--
            }
        }

        private void downloadFiles(string localDir, string ftpDir)
        {
            Directory.CreateDirectory(@"C:\\Users\Public\orders\order_" + order_id);

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

            Directory.CreateDirectory(@"C:\\Users\Public\orders\order_" + order_id + localDir);
            for (int i = 0; i <= directories.Count - 1; i++)
            {
                if (directories[i].Contains("."))
                {
                    string url = "ftp://83.220.174.171/order_" + order_id + ftpDir + directories[i].ToString();
                    NetworkCredential credentials = new NetworkCredential("seller", "Az123456!");

                    // Query size of the file to be downloaded
                    WebRequest sizeRequest = WebRequest.Create(url);
                    sizeRequest.Credentials = credentials;
                    sizeRequest.Method = WebRequestMethods.Ftp.GetFileSize;
                    int size = (int)sizeRequest.GetResponse().ContentLength;

                    progressBar1.Invoke(
                        (MethodInvoker)(() => progressBar1.Maximum = size));

                    // Download the file
                    WebRequest request = WebRequest.Create(url);
                    request.Credentials = credentials;
                    request.Method = WebRequestMethods.Ftp.DownloadFile;

                    using (Stream ftpStream = request.GetResponse().GetResponseStream())
                    using (Stream fileStream = File.Create(@"C:\\Users\Public\orders\order_" + order_id + localDir + directories[i].ToString()))
                    {
                        byte[] buffer = new byte[10240];
                        int read;
                        while ((read = ftpStream.Read(buffer, 0, buffer.Length)) > 0)
                        {
                            fileStream.Write(buffer, 0, read);
                            int position = (int)fileStream.Position;
                            progressBar1.Invoke(
                                (MethodInvoker)(() => progressBar1.Value = position));
                        }
                    }
                }
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
            catch { }

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

        public void uploadFiles(string ordr_id, string[] fileNames)
        {
            string folderName = "";

            if (user_type == 1)
            {
                folderName = "/startImage/";
            }
            else if ((user_type == 2) || (user_type == 3))
            {
                folderName = "/endImage/";
            }

            WebRequest request = WebRequest.Create("ftp://83.220.174.171/order_" + ordr_id);
            request.Method = WebRequestMethods.Ftp.MakeDirectory;
            request.Credentials = new NetworkCredential("seller", "Az123456!");
            try
            {
                request.GetResponse();
            }
            catch { }

            request = WebRequest.Create("ftp://83.220.174.171/order_" + ordr_id + folderName);
            request.Method = WebRequestMethods.Ftp.MakeDirectory;
            request.Credentials = new NetworkCredential("seller", "Az123456!");
            try
            {
                request.GetResponse();
            }
            catch { }

            int i = 1;
            foreach (string file in fileNames)
            {
                FtpWebRequest request_n = (FtpWebRequest)WebRequest.Create("ftp://83.220.174.171/order_" + ordr_id + "" + folderName + i + Path.GetExtension(file));
                request_n.Credentials = new NetworkCredential("seller", "Az123456!");
                request_n.Method = WebRequestMethods.Ftp.UploadFile;

                using (Stream fileStream = File.OpenRead(file))
                using (Stream ftpStream = request_n.GetRequestStream())
                {
                    progressBar1.Invoke(
                        (MethodInvoker)delegate { progressBar1.Maximum = (int)fileStream.Length; });

                    byte[] buffer = new byte[10240];
                    int read;
                    while ((read = fileStream.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        ftpStream.Write(buffer, 0, read);
                        progressBar1.Invoke(
                            (MethodInvoker)delegate
                            {
                                progressBar1.Value = (int)fileStream.Position;
                            });
                    }
                }

                i++;
            }
        }

        private void buttonDownloadFiles_Click(object sender, EventArgs e)
        {
            if (!checkInet()) return;

            downloadFiles("\\start\\", "/startImage/");
            downloadFiles("\\end\\", "/endImage/");
            try
            {
                Process.Start(@"C:\\Users\Public\orders\order_" + order_id);
            }
            catch
            {
                MessageBox.Show("Файлы отсутствуют");
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

        private void label10_Click(object sender, EventArgs e)
        {

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
