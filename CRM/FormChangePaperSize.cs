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
    public partial class FormChangePaperSize : Form
    {
        public FormChangePaperSize(string sizeX, string sizeY, string price)
        {
            InitializeComponent();
            if (sizeX != "")
            {
                textBoxX.Text = sizeX;
                textBoxY.Text = sizeY;
                textBoxX.Enabled = false;
                textBoxY.Enabled = false;
                textBoxPrice.Text = price;
            }
            else
            {
                textBoxX.Text = "";
                textBoxY.Text = "";
                textBoxPrice.Text = "";
            }
        }

        private void FormChangePaperSize_Load(object sender, EventArgs e)
        {

        }
    }
}
