﻿namespace CRM
{
    partial class FormAdmin
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
            this.buttonLogs = new System.Windows.Forms.Button();
            this.buttonUsersList = new System.Windows.Forms.Button();
            this.buttonOrdersTypeList = new System.Windows.Forms.Button();
            this.buttonChangePaperSizes = new System.Windows.Forms.Button();
            this.buttonChangePrototermServer = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // buttonLogs
            // 
            this.buttonLogs.Location = new System.Drawing.Point(12, 12);
            this.buttonLogs.Name = "buttonLogs";
            this.buttonLogs.Size = new System.Drawing.Size(179, 23);
            this.buttonLogs.TabIndex = 0;
            this.buttonLogs.Text = "Логи изменений заявок";
            this.buttonLogs.UseVisualStyleBackColor = true;
            this.buttonLogs.Click += new System.EventHandler(this.buttonLogs_Click);
            // 
            // buttonUsersList
            // 
            this.buttonUsersList.Location = new System.Drawing.Point(12, 41);
            this.buttonUsersList.Name = "buttonUsersList";
            this.buttonUsersList.Size = new System.Drawing.Size(179, 23);
            this.buttonUsersList.TabIndex = 1;
            this.buttonUsersList.Text = "Изменение списка сотрудников";
            this.buttonUsersList.UseVisualStyleBackColor = true;
            this.buttonUsersList.Click += new System.EventHandler(this.buttonUsersList_Click);
            // 
            // buttonOrdersTypeList
            // 
            this.buttonOrdersTypeList.Location = new System.Drawing.Point(12, 70);
            this.buttonOrdersTypeList.Name = "buttonOrdersTypeList";
            this.buttonOrdersTypeList.Size = new System.Drawing.Size(179, 23);
            this.buttonOrdersTypeList.TabIndex = 2;
            this.buttonOrdersTypeList.Text = "Изменение списка типов задач";
            this.buttonOrdersTypeList.UseVisualStyleBackColor = true;
            this.buttonOrdersTypeList.Click += new System.EventHandler(this.buttonOrdersTypeList_Click);
            // 
            // buttonChangePaperSizes
            // 
            this.buttonChangePaperSizes.Location = new System.Drawing.Point(12, 99);
            this.buttonChangePaperSizes.Name = "buttonChangePaperSizes";
            this.buttonChangePaperSizes.Size = new System.Drawing.Size(179, 37);
            this.buttonChangePaperSizes.TabIndex = 3;
            this.buttonChangePaperSizes.Text = "Изменение списка форматов бумаги";
            this.buttonChangePaperSizes.UseVisualStyleBackColor = true;
            this.buttonChangePaperSizes.Click += new System.EventHandler(this.buttonChangePaperSizes_Click);
            // 
            // buttonChangePrototermServer
            // 
            this.buttonChangePrototermServer.Location = new System.Drawing.Point(13, 144);
            this.buttonChangePrototermServer.Name = "buttonChangePrototermServer";
            this.buttonChangePrototermServer.Size = new System.Drawing.Size(179, 37);
            this.buttonChangePrototermServer.TabIndex = 4;
            this.buttonChangePrototermServer.Text = "Изменение адреса сервера фототерминалов";
            this.buttonChangePrototermServer.UseVisualStyleBackColor = true;
            this.buttonChangePrototermServer.Click += new System.EventHandler(this.buttonChangePrototermServer_Click);
            // 
            // FormAdmin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(205, 325);
            this.Controls.Add(this.buttonChangePrototermServer);
            this.Controls.Add(this.buttonChangePaperSizes);
            this.Controls.Add(this.buttonOrdersTypeList);
            this.Controls.Add(this.buttonUsersList);
            this.Controls.Add(this.buttonLogs);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FormAdmin";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Меню админа";
            this.Load += new System.EventHandler(this.FormAdmin_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonLogs;
        private System.Windows.Forms.Button buttonUsersList;
        private System.Windows.Forms.Button buttonOrdersTypeList;
        private System.Windows.Forms.Button buttonChangePaperSizes;
        private System.Windows.Forms.Button buttonChangePrototermServer;
    }
}