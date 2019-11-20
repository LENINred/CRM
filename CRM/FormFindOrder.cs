using System;
using System.Windows.Forms;

namespace CRM
{
    public partial class FormFindOrder : Form
    {
        public FormFindOrder()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string actualdata = string.Empty;
            char[] entereddata = textBox1.Text.ToCharArray();
            foreach (char aChar in entereddata)
            {
                if (Char.IsLetterOrDigit(aChar))
                {
                    actualdata += aChar;
                }
                else
                {
                    actualdata.Replace(aChar, ' ');
                    actualdata.Trim();
                }
            }
            textBox1.Text = actualdata;
            textBox1.SelectionStart = textBox1.Text.Length;
            textBox1.SelectionLength = 0;
        }

        private void FormFindOrder_Load(object sender, EventArgs e)
        {

        }
    }
}
