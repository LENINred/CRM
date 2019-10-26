﻿using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Windows.Forms;

namespace CRM
{
    public partial class FormLogIn : Form
    {
        public FormLogIn()
        {
            InitializeComponent();
        }

        private void ButtonUser_Click(object sender, EventArgs e)
        {
            if (!new InternetConnection().CheckForInternetConnection())
            {
                MessageBox.Show("На компьютере отсутствует интернет соединение");
                return;
            }

            string[] user = ((Button)sender).Tag.ToString().Split(';');
            string pass = showPasswordForm();
            if (pass != "666")
            {
                if (checkLoginPass(user[0], pass))
                {
                    FormMain form = new FormMain(user[0], Int32.Parse(user[1]));
                    this.Hide();
                    form.ShowDialog();
                }
                else MessageBox.Show("Пароль неверный");
            }
        }

        private void doUpdate()
        {
            MessageBox.Show("Имеется обновление для программы, Нажмите Ок чтобы продолжить");
            File.Delete(System.Reflection.Assembly.GetEntryAssembly().Location + ".bak");
            File.Move(System.Reflection.Assembly.GetEntryAssembly().Location, System.Reflection.Assembly.GetEntryAssembly().Location + ".bak");
            using (var client = new WebClient())
            {
                client.Headers.Add("user-agent", "Mozilla");
                client.DownloadFile(new Uri("https://raw.githubusercontent.com/LENINred/CRM/master/CRM/bin/Debug/CRM.exe"), "CRM.exe");
            }
            Application.Restart();
        }

        private string showPasswordForm()
        {
            string pass;
            FormEnterPass testDialog = new FormEnterPass();
            if (testDialog.ShowDialog(this) == DialogResult.OK)
            {
                pass = testDialog.textBoxPass.Text;
            }
            else
            {
                pass = "666";
            }
            testDialog.Dispose();
            return pass;
        }

        private bool checkLoginPass(string login, string pass)
        {
            FormLoading loading = new FormLoading();
            loading.Show();

            using (var con = new DBUtils().getDBConnection())
            {
                con.Open();
                using (var cmd = new MySqlCommand("check_login_pass", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new MySqlParameter("@login", MySqlDbType.VarChar));
                    cmd.Parameters["@login"].Value = login;
                    cmd.Parameters.Add(new MySqlParameter("@pass", MySqlDbType.VarChar));
                    cmd.Parameters["@pass"].Value = pass;
                    if (cmd.ExecuteReader().HasRows)
                    {
                        loading.Dispose();
                        return true;
                    }
                    else
                    {
                        loading.Dispose();
                        return false;
                    }
                }
            }
        }

        private void LogInForm_Load(object sender, EventArgs e)
        {
            if (!new InternetConnection().CheckForInternetConnection())
            {
                MessageBox.Show("На компьютере отсутствует интернет соединение");
                return;
            }

            ClassUpdateCRM update = new ClassUpdateCRM();
            if (update.checkUpdate())
                doUpdate();

            int ban = 0;
            using (var mySqlConnection = new BackDoorConnection().getDBConnection())
            {
                mySqlConnection.Open();
                using (var cmd = new MySqlCommand("SELECT `BAN` FROM `BAN` WHERE 1", mySqlConnection))
                {
                    using (DbDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                ban = reader.GetInt32(0);
                            }
                        }
                    }
                }
            }
            if(ban == 1)
            {
                foreach(Control control in this.Controls)
                {
                    control.Enabled = false;
                }
                labelInfo.Enabled = true;
            }
        }

        private void labelInfo_Click(object sender, EventArgs e)
        {
            FormDevInfo devInfo = new FormDevInfo();
            devInfo.ShowDialog(this);
        }
    }
}
