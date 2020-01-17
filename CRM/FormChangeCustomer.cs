using System;
using System.Windows.Forms;

namespace CRM
{
    public partial class FormChangeCustomer : Form
    {
        public FormChangeCustomer(string name, string comm)
        {
            InitializeComponent();
            textBoxName.Text = name;
            textBoxComm.Text = comm;
        }

        private void FormChangeCustomer_Load(object sender, EventArgs e)
        {

        }
    }
}
