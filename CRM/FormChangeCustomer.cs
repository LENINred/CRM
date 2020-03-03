using System;
using System.Windows.Forms;

namespace CRM
{
    public partial class FormChangeCustomer : Form
    {
        public FormChangeCustomer(string name, string comm, string mail)
        {
            InitializeComponent();
            textBoxName.Text = name;
            textBoxComm.Text = comm;
            textBoxMail.Text = mail;
        }

        private void FormChangeCustomer_Load(object sender, EventArgs e)
        {

        }

        private void buttonSave_Click(object sender, EventArgs e)
        {

        }
    }
}
