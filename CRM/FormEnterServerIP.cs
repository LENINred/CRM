using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CRM
{
    public partial class FormEnterServerIP : Form
    {
        public FormEnterServerIP(string IP)
        {
            InitializeComponent();
            textBoxIP.Text = IP;
        }

        private void FormEnterServerIP_Load(object sender, EventArgs e)
        {

        }
    }
}
