using System;
using System.Diagnostics;
using System.Reflection;
using System.Windows.Forms;

namespace CRM
{
    public partial class FormDevInfo : Form
    {
        public FormDevInfo()
        {
            InitializeComponent();
        }

        private void FormDevInfo_Load(object sender, EventArgs e)
        {
            labelVer.Text = "Версия CRM: " + FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).ProductVersion;
        }

        private void buttonTellError_Click(object sender, EventArgs e)
        {
            FormErrorInfo errorInfo = new FormErrorInfo();
            errorInfo.ShowDialog(this);
        }
    }
}
