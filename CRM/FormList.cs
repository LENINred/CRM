using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace CRM
{
    public partial class FormList : Form
    {
        int type;
        string query;
        string formText;
        string[] querys;
        public FormList(int tp)
        {
            InitializeComponent();
            type = tp;
        }

        private void FormList_Load(object sender, EventArgs e)
        {
            if (type == 1)
            {
                query = "load_users_list";
                querys = new[] { "add_user", "delete_user", "change_user" };
                this.Text = "Сотрудники";
                formText = "Ф.И.О.";
                buttonAdd.Click += buttonAdd_Click;
            }
            else if (type == 2)
            {
                query = "load_types_list";
                querys = new[] { "add_type", "delete_type", "change_type" };
                this.Text = "Виды работ";
                formText = "Вид работы";
                buttonAdd.Click += buttonAdd_Click;
            }
            else if (type == 3)
            {
                query = "load_cust_list";
                querys = new[] { "add_new_customer", "delete_customer", "change_cust" };
                this.Text = "Клиенты";
                formText = "Ф.И.О.";
                buttonAdd.Click += buttonAddCust_Click;
            }

            loadList(query);
        }

        List<string> list = new List<string>();
        private void loadList(string query)
        {
            using (var mySqlConnection = new DBUtils().getDBConnection())
            {
                mySqlConnection.Open();
                using (var cmd = new MySqlCommand(query, mySqlConnection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (DbDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                if(type == 3)
                                    list.Add(reader.GetInt32(0) + ";" + reader.GetString(1) + ";" + reader.GetString(2) + ";" + reader.GetString(3));
                                else list.Add(reader.GetInt32(0) + ";" + reader.GetString(1));
                            }
                        }
                    }
                }
            }
            Thread thread = new Thread(() => createObjList(list));
            thread.Start();
        }

        private void createObjList(List<string> list)
        {
            int x = 13, y = 13;
            int i = 0;
            Label lb = new Label();
            lb.AutoSize = true;
            lb.AutoEllipsis = false;
            lb.Text = "";
            foreach (string obj in list)
            {
                if (obj.Length > lb.Text.Length)
                    lb.Text = obj;
            }
            Graphics cg = this.CreateGraphics();
            SizeF size = cg.MeasureString(lb.Text, lb.Font);

            foreach (string obj in list)
            {
                Label lab = new Label();
                lab.Name = "label-" + i;
                lab.Tag = obj.Split(';')[0];
                if(type == 3) lab.Tag += (";" + obj.Split(';')[2]) + (";" + obj.Split(';')[3]);
                lab.Text = obj.Split(';')[1];
                lab.AutoSize = true;
                lab.AutoEllipsis = false;
                lab.Location = new Point(x, y);

                Button buttonCh = new Button();
                buttonCh.Name = "buttonCh-" + i;
                buttonCh.AutoSize = true;
                buttonCh.AutoSizeMode = AutoSizeMode.GrowAndShrink;
                buttonCh.AutoEllipsis = false;
                buttonCh.Text = "Изменить";
                buttonCh.Location = new Point(x + (int)size.Width + 5, y - 5);
                if(type == 3)
                {
                    buttonCh.Click += ButtonChCust_Click;
                }
                else buttonCh.Click += ButtonCh_Click;

                Button buttonRm = new Button();
                buttonRm.Name = "buttonRm-" + i;
                buttonRm.AutoSize = true;
                buttonRm.AutoSizeMode = AutoSizeMode.GrowAndShrink;
                buttonRm.AutoEllipsis = false;
                buttonRm.Text = "Удалить";
                buttonRm.Location = new Point(x + (int)size.Width + 10 + buttonCh.Width, y - 5);
                buttonRm.Click += ButtonRm_Click;

                this.BeginInvoke((ThreadStart)delegate {

                    panelUsers.Controls.Add(lab);
                    panelUsers.Controls.Add(buttonCh);
                    panelUsers.Controls.Add(buttonRm);
                });
                
                y += 29;
                i++;
            }
        }

        private void ButtonRm_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Удалить", "Подтверждение", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                Label lab = (Label)((Button)sender).Parent.Controls.Find("label-" + ((Button)sender).Name.ToString().Split('-')[1], true)[0];
                using (var mySqlConnection = new DBUtils().getDBConnection())
                {
                    using (var cmd = new MySqlCommand())
                    {
                        cmd.Connection = mySqlConnection;
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.CommandText = querys[1];
                        cmd.Parameters.Clear();
                        MySqlParameter p1 = cmd.Parameters.Add("@id", MySqlDbType.Int32);
                        p1.Direction = ParameterDirection.Input;

                        p1.Value = lab.Tag.ToString().Split(';')[0];
                        mySqlConnection.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
                panelUsers.Controls.Clear();
                list.Clear();
                loadList(query);
            }
            else if (dialogResult == DialogResult.No)
            {
                //--
            }
        }

        private void ButtonCh_Click(object sender, EventArgs e)
        {
            Label lab = (Label)((Button)sender).Parent.Controls.Find("label-" + ((Button)sender).Name.ToString().Split('-')[1], true)[0];
            FormEditText editText = new FormEditText(lab.Text, formText);
            if (editText.ShowDialog(this) == DialogResult.OK)
            {
                if (editText.textBox1.Text.Trim().Length > 0)
                {
                    using (var mySqlConnection = new DBUtils().getDBConnection())
                    {
                        using (var cmd = new MySqlCommand())
                        {
                            cmd.Connection = mySqlConnection;
                            cmd.CommandType = System.Data.CommandType.StoredProcedure;
                            cmd.CommandText = querys[2];
                            cmd.Parameters.Clear();
                            MySqlParameter p1 = cmd.Parameters.Add("@id", MySqlDbType.Int32);
                            p1.Direction = ParameterDirection.Input;
                            MySqlParameter p2 = cmd.Parameters.Add("@oldname", MySqlDbType.VarChar);
                            p2.Direction = ParameterDirection.Input;
                            MySqlParameter p3 = cmd.Parameters.Add("@newname", MySqlDbType.VarChar);
                            p3.Direction = ParameterDirection.Input;

                            p1.Value = lab.Tag;
                            p2.Value = lab.Text;
                            p3.Value = editText.textBox1.Text;
                            mySqlConnection.Open();
                            cmd.ExecuteNonQuery();
                        }
                    }
                    lab.Text = editText.textBox1.Text;
                }
            }
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            FormEditText userName = new FormEditText("", formText);
            if (userName.ShowDialog(this) == DialogResult.OK)
            {
                if (userName.textBox1.Text.Trim().Length > 0)
                {
                    using (var mySqlConnection = new DBUtils().getDBConnection())
                    {
                        using (var cmd = new MySqlCommand())
                        {
                            cmd.Connection = mySqlConnection;
                            cmd.CommandType = System.Data.CommandType.StoredProcedure;
                            cmd.CommandText = querys[0];
                            cmd.Parameters.Clear();
                            MySqlParameter p1 = cmd.Parameters.Add("@name", MySqlDbType.VarChar);
                            p1.Direction = ParameterDirection.Input;

                            p1.Value = userName.textBox1.Text;
                            mySqlConnection.Open();
                            cmd.ExecuteNonQuery();
                        }
                    }
                    panelUsers.Controls.Clear();
                    list.Clear();
                    loadList(query);
                }
            }
        }

        private void ButtonChCust_Click(object sender, EventArgs e)
        {
            Label lab = (Label)((Button)sender).Parent.Controls.Find("label-" + ((Button)sender).Name.ToString().Split('-')[1], true)[0];
            FormChangeCustomer editText = new FormChangeCustomer(lab.Text, lab.Tag.ToString().Split(';')[1], lab.Tag.ToString().Split(';')[2]);
            if (editText.ShowDialog(this) == DialogResult.OK)
            {
                if ((editText.textBoxName.Text.Trim().Length > 0) && (editText.textBoxComm.Text.Trim().Length > 0))
                {
                    using (var mySqlConnection = new DBUtils().getDBConnection())
                    {
                        using (var cmd = new MySqlCommand())
                        {
                            cmd.Connection = mySqlConnection;
                            cmd.CommandType = System.Data.CommandType.StoredProcedure;
                            cmd.CommandText = querys[2];
                            cmd.Parameters.Clear();
                            MySqlParameter p1 = cmd.Parameters.Add("@id", MySqlDbType.Int32);
                            p1.Direction = ParameterDirection.Input;
                            MySqlParameter p2 = cmd.Parameters.Add("@new_name", MySqlDbType.VarChar);
                            p2.Direction = ParameterDirection.Input;
                            MySqlParameter p3 = cmd.Parameters.Add("@new_comm", MySqlDbType.VarChar);
                            p3.Direction = ParameterDirection.Input;
                            MySqlParameter p4 = cmd.Parameters.Add("@new_mail", MySqlDbType.VarChar);
                            p4.Direction = ParameterDirection.Input;

                            p1.Value = lab.Tag.ToString().Split(';')[0];
                            p2.Value = editText.textBoxName.Text;
                            p3.Value = editText.textBoxComm.Text;
                            p4.Value = editText.textBoxMail.Text;
                            mySqlConnection.Open();
                            cmd.ExecuteNonQuery();
                        }
                    }
                    lab.Text = editText.textBoxName.Text;
                }
            }
        }

        private void buttonAddCust_Click(object sender, EventArgs e)
        {
            FormChangeCustomer custData = new FormChangeCustomer("", "", "");
            if (custData.ShowDialog(this) == DialogResult.OK)
            {
                if ((custData.textBoxName.Text.Trim().Length > 0) && (custData.textBoxComm.Text.Trim().Length > 0))
                {
                    using (var mySqlConnection = new DBUtils().getDBConnection())
                    {
                        using (var cmd = new MySqlCommand())
                        {
                            cmd.Connection = mySqlConnection;
                            cmd.CommandType = System.Data.CommandType.StoredProcedure;
                            cmd.CommandText = querys[0];
                            cmd.Parameters.Clear();
                            MySqlParameter p1 = cmd.Parameters.Add("@customer", MySqlDbType.VarChar);
                            p1.Direction = ParameterDirection.Input;
                            MySqlParameter p2 = cmd.Parameters.Add("@comm", MySqlDbType.VarChar);
                            p2.Direction = ParameterDirection.Input;
                            MySqlParameter p3 = cmd.Parameters.Add("@sub_comm", MySqlDbType.VarChar);
                            p3.Direction = ParameterDirection.Input;

                            p1.Value = custData.textBoxName.Text;
                            p2.Value = custData.textBoxComm.Text;
                            p3.Value = custData.textBoxMail.Text;
                            mySqlConnection.Open();
                            cmd.ExecuteNonQuery();
                        }
                    }
                    panelUsers.Controls.Clear();
                    list.Clear();
                    loadList(query);
                }
            }
        }
    }
}
