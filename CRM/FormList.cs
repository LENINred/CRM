using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Drawing;
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
                query = "SELECT * FROM `Executors` WHERE 1";
                querys = new[] { "add_user", "delete_user", "change_user" };
                this.Text = "Сотрудники";
                formText = "Ф.И.О.";
            }
            else
            {
                query = "SELECT * FROM `WorkTypes` WHERE 1";
                querys = new[] { "add_type", "delete_type", "change_type" };
                this.Text = "Виды работ";
                formText = "Вид работы";
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
                    using (DbDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                list.Add(reader.GetInt32(0) + ";" + reader.GetString(1));
                            }
                        }
                    }
                }
            }
            createObjList(list);
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
                buttonCh.Click += ButtonCh_Click;

                Button buttonRm = new Button();
                buttonRm.Name = "buttonRm-" + i;
                buttonRm.AutoSize = true;
                buttonRm.AutoSizeMode = AutoSizeMode.GrowAndShrink;
                buttonRm.AutoEllipsis = false;
                buttonRm.Text = "Удалить";
                buttonRm.Location = new Point(x + (int)size.Width + 10 + buttonCh.Width, y - 5);
                buttonRm.Click += ButtonRm_Click;

                panelUsers.Controls.Add(lab);
                panelUsers.Controls.Add(buttonCh);
                panelUsers.Controls.Add(buttonRm);
                y += 29;
                i++;
            }
        }

        private void ButtonRm_Click(object sender, EventArgs e)
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

                    p1.Value = lab.Tag;
                    mySqlConnection.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            panelUsers.Controls.Clear();
            list.Clear();
            loadList(query);
        }

        private void ButtonCh_Click(object sender, EventArgs e)
        {
            Label lab = (Label)((Button)sender).Parent.Controls.Find("label-" + ((Button)sender).Name.ToString().Split('-')[1], true)[0];
            FormEditText editText = new FormEditText(lab.Text, formText);
            if (editText.ShowDialog(this) == DialogResult.OK)
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
            }
            panelUsers.Controls.Clear();
            list.Clear();
            loadList(query);
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            FormEditText userName = new FormEditText("", formText);
            if (userName.ShowDialog(this) == DialogResult.OK)
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
            }
            panelUsers.Controls.Clear();
            list.Clear();
            loadList(query);
        }
    }
}
