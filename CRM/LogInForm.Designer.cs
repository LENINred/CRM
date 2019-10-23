namespace CRM
{
    partial class LogInForm
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
            this.buttonAdmin = new System.Windows.Forms.Button();
            this.buttonSeller = new System.Windows.Forms.Button();
            this.buttonDesigner = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // buttonAdmin
            // 
            this.buttonAdmin.Location = new System.Drawing.Point(16, 107);
            this.buttonAdmin.Name = "buttonAdmin";
            this.buttonAdmin.Size = new System.Drawing.Size(100, 23);
            this.buttonAdmin.TabIndex = 4;
            this.buttonAdmin.Tag = "Admin;3";
            this.buttonAdmin.Text = "Руководитель";
            this.buttonAdmin.UseVisualStyleBackColor = true;
            this.buttonAdmin.Click += new System.EventHandler(this.ButtonUser_Click);
            // 
            // buttonSeller
            // 
            this.buttonSeller.Location = new System.Drawing.Point(16, 20);
            this.buttonSeller.Name = "buttonSeller";
            this.buttonSeller.Size = new System.Drawing.Size(100, 23);
            this.buttonSeller.TabIndex = 5;
            this.buttonSeller.Tag = "Seller;1";
            this.buttonSeller.Text = "Продавец";
            this.buttonSeller.UseVisualStyleBackColor = true;
            this.buttonSeller.Click += new System.EventHandler(this.ButtonUser_Click);
            // 
            // buttonDesigner
            // 
            this.buttonDesigner.Location = new System.Drawing.Point(16, 65);
            this.buttonDesigner.Name = "buttonDesigner";
            this.buttonDesigner.Size = new System.Drawing.Size(100, 23);
            this.buttonDesigner.TabIndex = 6;
            this.buttonDesigner.Tag = "Designer;2";
            this.buttonDesigner.Text = "Дизайнер";
            this.buttonDesigner.UseVisualStyleBackColor = true;
            this.buttonDesigner.Click += new System.EventHandler(this.ButtonUser_Click);
            // 
            // LogInForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(136, 147);
            this.Controls.Add(this.buttonDesigner);
            this.Controls.Add(this.buttonSeller);
            this.Controls.Add(this.buttonAdmin);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "LogInForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Войдите";
            this.Load += new System.EventHandler(this.LogInForm_Load);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button buttonAdmin;
        private System.Windows.Forms.Button buttonSeller;
        private System.Windows.Forms.Button buttonDesigner;
    }
}