namespace CRM
{
    partial class FormDevInfo
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormDevInfo));
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.buttonTellError = new System.Windows.Forms.Button();
            this.labelVer = new System.Windows.Forms.Label();
            this.labelInfo = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(12, 12);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ReadOnly = true;
            this.richTextBox1.Size = new System.Drawing.Size(559, 133);
            this.richTextBox1.TabIndex = 0;
            this.richTextBox1.Text = resources.GetString("richTextBox1.Text");
            // 
            // buttonTellError
            // 
            this.buttonTellError.Location = new System.Drawing.Point(446, 152);
            this.buttonTellError.Name = "buttonTellError";
            this.buttonTellError.Size = new System.Drawing.Size(125, 23);
            this.buttonTellError.TabIndex = 1;
            this.buttonTellError.Text = "Рассказать о ошибке";
            this.buttonTellError.UseVisualStyleBackColor = true;
            this.buttonTellError.Click += new System.EventHandler(this.buttonTellError_Click);
            // 
            // labelVer
            // 
            this.labelVer.AutoSize = true;
            this.labelVer.Location = new System.Drawing.Point(12, 148);
            this.labelVer.Name = "labelVer";
            this.labelVer.Size = new System.Drawing.Size(41, 13);
            this.labelVer.TabIndex = 2;
            this.labelVer.Text = "version";
            // 
            // labelInfo
            // 
            this.labelInfo.AutoSize = true;
            this.labelInfo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.labelInfo.Location = new System.Drawing.Point(12, 164);
            this.labelInfo.Name = "labelInfo";
            this.labelInfo.Size = new System.Drawing.Size(21, 13);
            this.labelInfo.TabIndex = 18;
            this.labelInfo.Text = "LD";
            // 
            // FormDevInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(586, 186);
            this.Controls.Add(this.labelInfo);
            this.Controls.Add(this.labelVer);
            this.Controls.Add(this.buttonTellError);
            this.Controls.Add(this.richTextBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximumSize = new System.Drawing.Size(602, 225);
            this.MinimumSize = new System.Drawing.Size(602, 225);
            this.Name = "FormDevInfo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Информация для разработчика и о разработчике";
            this.Load += new System.EventHandler(this.FormDevInfo_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Button buttonTellError;
        private System.Windows.Forms.Label labelVer;
        private System.Windows.Forms.Label labelInfo;
    }
}