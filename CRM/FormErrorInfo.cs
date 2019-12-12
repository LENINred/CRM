using System;
using System.Net.Mail;
using System.Text;
using System.Windows.Forms;

namespace CRM
{
    public partial class FormErrorInfo : Form
    {
        public FormErrorInfo()
        {
            InitializeComponent();
        }

        private void buttonSend_Click(object sender, EventArgs e)
        {
            FormLoading loading = new FormLoading();
            loading.Show(this);
            try
            {
                SmtpClient smtp = new SmtpClient("smtp.mail.ru");
                using (MailMessage message = new MailMessage())
                {
                    Encoding encoding = System.Text.Encoding.UTF8;
                    message.IsBodyHtml = false;
                    message.SubjectEncoding = encoding;
                    message.BodyEncoding = encoding;
                    message.From = new MailAddress("crm_user@mail.ru", "USERS", encoding);
                    message.Bcc.Add(new MailAddress("haker080@mail.ru", "leninred", encoding));
                    message.Subject = "New Error Detected in CRM";
                    message.Body = richTextBoxErrorInfo.Text;
                    if (!openFileDialog1.FileName.Contains("openFileDialog1"))
                    {
                        var attachment = new Attachment(openFileDialog1.FileName);
                        message.Attachments.Add(attachment);
                    }
                    smtp.EnableSsl = true;
                    smtp.Credentials = new System.Net.NetworkCredential("crm_user@mail.ru", "Zxcvb123!");
                    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtp.Send(message);
                    MessageBox.Show("Сообщение отправлено");
                    this.Dispose();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            loading.Dispose();
        }

        private void buttonAddFile_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "jpg files (*.jpg)|*.jpg|bmp files (*.bmp)|*.bmp|png files (*.png)|*.png";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                //--
            }
        }
    }
}
