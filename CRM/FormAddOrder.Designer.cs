namespace CRM
{
    partial class FormAddOrder
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
            this.comboBoxCustomers = new System.Windows.Forms.ComboBox();
            this.textBoxCustomer = new System.Windows.Forms.TextBox();
            this.richTextBoxOrderInfo = new System.Windows.Forms.RichTextBox();
            this.buttonAddOrder = new System.Windows.Forms.Button();
            this.comboBoxOrderStatus = new System.Windows.Forms.ComboBox();
            this.radioButtonExistCustomer = new System.Windows.Forms.RadioButton();
            this.radioButtonNewCustomer = new System.Windows.Forms.RadioButton();
            this.textBoxPriorComm = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBoxOrderType = new System.Windows.Forms.ComboBox();
            this.comboBoxExecutor = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.checkBoxSubComm = new System.Windows.Forms.CheckBox();
            this.textBoxSubComm = new System.Windows.Forms.TextBox();
            this.textBoxCost = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.buttonAddFile = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.buttonDownloadFiles = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.textBoxDate = new System.Windows.Forms.MaskedTextBox();
            this.SuspendLayout();
            // 
            // comboBoxCustomers
            // 
            this.comboBoxCustomers.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.comboBoxCustomers.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comboBoxCustomers.FormattingEnabled = true;
            this.comboBoxCustomers.Location = new System.Drawing.Point(189, 16);
            this.comboBoxCustomers.Name = "comboBoxCustomers";
            this.comboBoxCustomers.Size = new System.Drawing.Size(370, 21);
            this.comboBoxCustomers.TabIndex = 0;
            // 
            // textBoxCustomer
            // 
            this.textBoxCustomer.Location = new System.Drawing.Point(189, 60);
            this.textBoxCustomer.MaxLength = 64;
            this.textBoxCustomer.Name = "textBoxCustomer";
            this.textBoxCustomer.Size = new System.Drawing.Size(370, 20);
            this.textBoxCustomer.TabIndex = 2;
            this.textBoxCustomer.TextChanged += new System.EventHandler(this.textBoxCustomer_TextChanged);
            // 
            // richTextBoxOrderInfo
            // 
            this.richTextBoxOrderInfo.Location = new System.Drawing.Point(12, 139);
            this.richTextBoxOrderInfo.Name = "richTextBoxOrderInfo";
            this.richTextBoxOrderInfo.Size = new System.Drawing.Size(547, 96);
            this.richTextBoxOrderInfo.TabIndex = 6;
            this.richTextBoxOrderInfo.Text = "";
            // 
            // buttonAddOrder
            // 
            this.buttonAddOrder.Location = new System.Drawing.Point(425, 267);
            this.buttonAddOrder.Name = "buttonAddOrder";
            this.buttonAddOrder.Size = new System.Drawing.Size(134, 107);
            this.buttonAddOrder.TabIndex = 13;
            this.buttonAddOrder.Text = "Добавить";
            this.buttonAddOrder.UseVisualStyleBackColor = true;
            this.buttonAddOrder.Click += new System.EventHandler(this.buttonAddOrder_Click);
            // 
            // comboBoxOrderStatus
            // 
            this.comboBoxOrderStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxOrderStatus.FormattingEnabled = true;
            this.comboBoxOrderStatus.Location = new System.Drawing.Point(12, 325);
            this.comboBoxOrderStatus.Name = "comboBoxOrderStatus";
            this.comboBoxOrderStatus.Size = new System.Drawing.Size(406, 21);
            this.comboBoxOrderStatus.TabIndex = 11;
            this.comboBoxOrderStatus.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.comboBoxOrderStatus_KeyPress);
            // 
            // radioButtonExistCustomer
            // 
            this.radioButtonExistCustomer.AutoSize = true;
            this.radioButtonExistCustomer.Checked = true;
            this.radioButtonExistCustomer.Location = new System.Drawing.Point(12, 17);
            this.radioButtonExistCustomer.Name = "radioButtonExistCustomer";
            this.radioButtonExistCustomer.Size = new System.Drawing.Size(138, 17);
            this.radioButtonExistCustomer.TabIndex = 7;
            this.radioButtonExistCustomer.TabStop = true;
            this.radioButtonExistCustomer.Text = "Имеющийся заказчик";
            this.radioButtonExistCustomer.UseVisualStyleBackColor = true;
            this.radioButtonExistCustomer.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // radioButtonNewCustomer
            // 
            this.radioButtonNewCustomer.AutoSize = true;
            this.radioButtonNewCustomer.Location = new System.Drawing.Point(12, 76);
            this.radioButtonNewCustomer.Name = "radioButtonNewCustomer";
            this.radioButtonNewCustomer.Size = new System.Drawing.Size(109, 17);
            this.radioButtonNewCustomer.TabIndex = 1;
            this.radioButtonNewCustomer.Text = "Новый заказчик";
            this.radioButtonNewCustomer.UseVisualStyleBackColor = true;
            // 
            // textBoxPriorComm
            // 
            this.textBoxPriorComm.Location = new System.Drawing.Point(248, 86);
            this.textBoxPriorComm.MaxLength = 24;
            this.textBoxPriorComm.Name = "textBoxPriorComm";
            this.textBoxPriorComm.Size = new System.Drawing.Size(311, 20);
            this.textBoxPriorComm.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(140, 63);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "Ф.И.О.";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(114, 104);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 13);
            this.label2.TabIndex = 11;
            this.label2.Text = "Вид связи";
            // 
            // comboBoxOrderType
            // 
            this.comboBoxOrderType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxOrderType.FormattingEnabled = true;
            this.comboBoxOrderType.Location = new System.Drawing.Point(12, 298);
            this.comboBoxOrderType.Name = "comboBoxOrderType";
            this.comboBoxOrderType.Size = new System.Drawing.Size(406, 21);
            this.comboBoxOrderType.TabIndex = 10;
            this.comboBoxOrderType.SelectedIndexChanged += new System.EventHandler(this.comboBoxOrderType_SelectedIndexChanged);
            // 
            // comboBoxExecutor
            // 
            this.comboBoxExecutor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxExecutor.FormattingEnabled = true;
            this.comboBoxExecutor.Location = new System.Drawing.Point(12, 353);
            this.comboBoxExecutor.Name = "comboBoxExecutor";
            this.comboBoxExecutor.Size = new System.Drawing.Size(406, 21);
            this.comboBoxExecutor.TabIndex = 12;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(181, 93);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(57, 13);
            this.label3.TabIndex = 14;
            this.label3.Text = "Основной";
            // 
            // checkBoxSubComm
            // 
            this.checkBoxSubComm.AutoSize = true;
            this.checkBoxSubComm.Checked = true;
            this.checkBoxSubComm.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxSubComm.Location = new System.Drawing.Point(184, 115);
            this.checkBoxSubComm.Name = "checkBoxSubComm";
            this.checkBoxSubComm.Size = new System.Drawing.Size(50, 17);
            this.checkBoxSubComm.TabIndex = 4;
            this.checkBoxSubComm.Text = "Доп.";
            this.checkBoxSubComm.UseVisualStyleBackColor = true;
            this.checkBoxSubComm.CheckedChanged += new System.EventHandler(this.checkBoxDop_CheckedChanged);
            // 
            // textBoxSubComm
            // 
            this.textBoxSubComm.Location = new System.Drawing.Point(248, 112);
            this.textBoxSubComm.MaxLength = 24;
            this.textBoxSubComm.Name = "textBoxSubComm";
            this.textBoxSubComm.Size = new System.Drawing.Size(311, 20);
            this.textBoxSubComm.TabIndex = 5;
            // 
            // textBoxCost
            // 
            this.textBoxCost.Location = new System.Drawing.Point(425, 241);
            this.textBoxCost.MaxLength = 24;
            this.textBoxCost.Name = "textBoxCost";
            this.textBoxCost.Size = new System.Drawing.Size(112, 20);
            this.textBoxCost.TabIndex = 9;
            this.textBoxCost.TextChanged += new System.EventHandler(this.textBoxCost_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(356, 244);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(62, 13);
            this.label4.TabIndex = 20;
            this.label4.Text = "Стоимость";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(543, 244);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(16, 13);
            this.label5.TabIndex = 21;
            this.label5.Text = "р.";
            // 
            // buttonAddFile
            // 
            this.buttonAddFile.Location = new System.Drawing.Point(12, 241);
            this.buttonAddFile.Name = "buttonAddFile";
            this.buttonAddFile.Size = new System.Drawing.Size(125, 23);
            this.buttonAddFile.TabIndex = 7;
            this.buttonAddFile.Text = "Прикрепить файлы";
            this.buttonAddFile.UseVisualStyleBackColor = true;
            this.buttonAddFile.Click += new System.EventHandler(this.buttonAddFile_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.Multiselect = true;
            // 
            // buttonDownloadFiles
            // 
            this.buttonDownloadFiles.Location = new System.Drawing.Point(143, 241);
            this.buttonDownloadFiles.Name = "buttonDownloadFiles";
            this.buttonDownloadFiles.Size = new System.Drawing.Size(137, 23);
            this.buttonDownloadFiles.TabIndex = 8;
            this.buttonDownloadFiles.Text = "Прикрепленные файлы";
            this.buttonDownloadFiles.UseVisualStyleBackColor = true;
            this.buttonDownloadFiles.Click += new System.EventHandler(this.buttonDownloadFiles_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(236, 274);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(77, 13);
            this.label6.TabIndex = 24;
            this.label6.Text = "Крайний срок";
            // 
            // textBoxDate
            // 
            this.textBoxDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textBoxDate.Location = new System.Drawing.Point(319, 271);
            this.textBoxDate.Mask = "00.00.0000 00:00";
            this.textBoxDate.Name = "textBoxDate";
            this.textBoxDate.Size = new System.Drawing.Size(99, 20);
            this.textBoxDate.TabIndex = 25;
            this.textBoxDate.ValidatingType = typeof(System.DateTime);
            // 
            // AddOrder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(571, 381);
            this.Controls.Add(this.textBoxDate);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.buttonDownloadFiles);
            this.Controls.Add(this.buttonAddFile);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textBoxCost);
            this.Controls.Add(this.textBoxSubComm);
            this.Controls.Add(this.checkBoxSubComm);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.comboBoxExecutor);
            this.Controls.Add(this.comboBoxOrderType);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxPriorComm);
            this.Controls.Add(this.radioButtonNewCustomer);
            this.Controls.Add(this.radioButtonExistCustomer);
            this.Controls.Add(this.comboBoxOrderStatus);
            this.Controls.Add(this.buttonAddOrder);
            this.Controls.Add(this.richTextBoxOrderInfo);
            this.Controls.Add(this.textBoxCustomer);
            this.Controls.Add(this.comboBoxCustomers);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MinimumSize = new System.Drawing.Size(587, 395);
            this.Name = "AddOrder";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Создание заказа";
            this.Load += new System.EventHandler(this.AddOrder_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBoxCustomers;
        private System.Windows.Forms.TextBox textBoxCustomer;
        private System.Windows.Forms.RichTextBox richTextBoxOrderInfo;
        private System.Windows.Forms.Button buttonAddOrder;
        private System.Windows.Forms.ComboBox comboBoxOrderStatus;
        private System.Windows.Forms.RadioButton radioButtonExistCustomer;
        private System.Windows.Forms.RadioButton radioButtonNewCustomer;
        private System.Windows.Forms.TextBox textBoxPriorComm;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBoxOrderType;
        private System.Windows.Forms.ComboBox comboBoxExecutor;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox checkBoxSubComm;
        private System.Windows.Forms.TextBox textBoxSubComm;
        private System.Windows.Forms.TextBox textBoxCost;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button buttonAddFile;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button buttonDownloadFiles;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.MaskedTextBox textBoxDate;
    }
}