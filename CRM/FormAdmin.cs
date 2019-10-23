using System;
using System.Windows.Forms;

namespace CRM
{
    public partial class FormAdmin : Form
    {
        public FormAdmin()
        {
            InitializeComponent();
        }

        private void buttonLogs_Click(object sender, EventArgs e)
        {
            if (!new InternetConnection().CheckForInternetConnection())
            {
                MessageBox.Show("На компьютере отсутствует интернет соединение");
                return;
            }
            FormLogs logs = new FormLogs();
            logs.Show();
        }

        private void buttonUsersList_Click(object sender, EventArgs e)
        {
            if (!new InternetConnection().CheckForInternetConnection())
            {
                MessageBox.Show("На компьютере отсутствует интернет соединение");
                return;
            }
            FormList list = new FormList(1);
            list.Show();
        }

        private void buttonOrdersTypeList_Click(object sender, EventArgs e)
        {
            if (!new InternetConnection().CheckForInternetConnection())
            {
                MessageBox.Show("На компьютере отсутствует интернет соединение");
                return;
            }
            FormList list = new FormList(2);
            list.Show();
        }
    }
}
