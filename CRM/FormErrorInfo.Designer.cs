namespace CRM
{
    partial class FormErrorInfo
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.richTextBoxErrorInfo = new System.Windows.Forms.RichTextBox();
            this.buttonSend = new System.Windows.Forms.Button();
            this.buttonAddFile = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(447, 30);
            this.label1.TabIndex = 0;
            this.label1.Text = "Опишите в какой момент происходит проблема, а также попробуйте инициировать повто" +
    "рно ошибку и при возможности сделайте скриншот окна с текстом ошибки.";
            // 
            // richTextBoxErrorInfo
            // 
            this.richTextBoxErrorInfo.Location = new System.Drawing.Point(16, 46);
            this.richTextBoxErrorInfo.Name = "richTextBoxErrorInfo";
            this.richTextBoxErrorInfo.Size = new System.Drawing.Size(444, 195);
            this.richTextBoxErrorInfo.TabIndex = 1;
            this.richTextBoxErrorInfo.Text = "";
            // 
            // buttonSend
            // 
            this.buttonSend.Location = new System.Drawing.Point(385, 247);
            this.buttonSend.Name = "buttonSend";
            this.buttonSend.Size = new System.Drawing.Size(75, 23);
            this.buttonSend.TabIndex = 2;
            this.buttonSend.Text = "Отправить";
            this.buttonSend.UseVisualStyleBackColor = true;
            this.buttonSend.Click += new System.EventHandler(this.buttonSend_Click);
            // 
            // buttonAddFile
            // 
            this.buttonAddFile.Location = new System.Drawing.Point(249, 247);
            this.buttonAddFile.Name = "buttonAddFile";
            this.buttonAddFile.Size = new System.Drawing.Size(130, 23);
            this.buttonAddFile.TabIndex = 3;
            this.buttonAddFile.Text = "Прикрепить скриншот";
            this.buttonAddFile.UseVisualStyleBackColor = true;
            this.buttonAddFile.Click += new System.EventHandler(this.buttonAddFile_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // FormErrorInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(481, 282);
            this.Controls.Add(this.buttonAddFile);
            this.Controls.Add(this.buttonSend);
            this.Controls.Add(this.richTextBoxErrorInfo);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximumSize = new System.Drawing.Size(497, 321);
            this.MinimumSize = new System.Drawing.Size(497, 321);
            this.Name = "FormErrorInfo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Информация о ошибке";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RichTextBox richTextBoxErrorInfo;
        private System.Windows.Forms.Button buttonSend;
        private System.Windows.Forms.Button buttonAddFile;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
    }
}