using System;
using System.Windows.Forms;

namespace CRM
{
    public partial class FormEditText : Form
    {
        string name;
        public FormEditText(string nm, string text)
        {
            InitializeComponent();
            name = nm;
            this.Text = text;
        }

        private void FormUserName_Load(object sender, EventArgs e)
        {
            textBox1.Text = name;
        }
    }
}
