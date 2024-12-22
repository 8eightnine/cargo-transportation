namespace Orders
{
    partial class OrderForm
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
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.senderNameBox = new System.Windows.Forms.TextBox();
            this.physPersonComboBox = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.senderAddressBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.deliveryAddressBox = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.tripLengthBox = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.costBox = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.tripIDComboBox = new System.Windows.Forms.ComboBox();
            this.createNewCheckBox = new System.Windows.Forms.CheckBox();
            this.clientNameBox = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.clientIDComboBox = new System.Windows.Forms.ComboBox();
            this.phoneNumberBox = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.companyPersonComboBox = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.showCargoList = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Дата заказа";
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePicker1.Location = new System.Drawing.Point(12, 25);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(200, 20);
            this.dateTimePicker1.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(101, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "ФИО отправителя";
            // 
            // senderNameBox
            // 
            this.senderNameBox.Location = new System.Drawing.Point(12, 64);
            this.senderNameBox.Name = "senderNameBox";
            this.senderNameBox.Size = new System.Drawing.Size(200, 20);
            this.senderNameBox.TabIndex = 3;
            // 
            // physPersonComboBox
            // 
            this.physPersonComboBox.FormattingEnabled = true;
            this.physPersonComboBox.Location = new System.Drawing.Point(9, 155);
            this.physPersonComboBox.Name = "physPersonComboBox";
            this.physPersonComboBox.Size = new System.Drawing.Size(200, 21);
            this.physPersonComboBox.TabIndex = 4;
            this.physPersonComboBox.SelectedIndexChanged += new System.EventHandler(this.physPersonComboBox_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 87);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(105, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Адрес отправителя";
            // 
            // senderAddressBox
            // 
            this.senderAddressBox.Location = new System.Drawing.Point(12, 103);
            this.senderAddressBox.Name = "senderAddressBox";
            this.senderAddressBox.Size = new System.Drawing.Size(200, 20);
            this.senderAddressBox.TabIndex = 6;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(301, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(66, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Получатель";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 126);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(88, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "Адрес доставки";
            // 
            // deliveryAddressBox
            // 
            this.deliveryAddressBox.Location = new System.Drawing.Point(12, 142);
            this.deliveryAddressBox.Name = "deliveryAddressBox";
            this.deliveryAddressBox.Size = new System.Drawing.Size(200, 20);
            this.deliveryAddressBox.TabIndex = 9;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 165);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(85, 13);
            this.label6.TabIndex = 10;
            this.label6.Text = "Длина поездки";
            // 
            // tripLengthBox
            // 
            this.tripLengthBox.Location = new System.Drawing.Point(12, 181);
            this.tripLengthBox.Name = "tripLengthBox";
            this.tripLengthBox.Size = new System.Drawing.Size(200, 20);
            this.tripLengthBox.TabIndex = 11;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 204);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(101, 13);
            this.label7.TabIndex = 12;
            this.label7.Text = "Стоимость заказа";
            // 
            // costBox
            // 
            this.costBox.Location = new System.Drawing.Point(12, 220);
            this.costBox.Name = "costBox";
            this.costBox.Size = new System.Drawing.Size(200, 20);
            this.costBox.TabIndex = 13;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(12, 243);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(51, 13);
            this.label8.TabIndex = 14;
            this.label8.Text = "Поездка";
            // 
            // tripIDComboBox
            // 
            this.tripIDComboBox.FormattingEnabled = true;
            this.tripIDComboBox.Location = new System.Drawing.Point(12, 259);
            this.tripIDComboBox.Name = "tripIDComboBox";
            this.tripIDComboBox.Size = new System.Drawing.Size(200, 21);
            this.tripIDComboBox.TabIndex = 15;
            this.tripIDComboBox.SelectedIndexChanged += new System.EventHandler(this.tripIDComboBox_SelectedIndexChanged);
            // 
            // createNewCheckBox
            // 
            this.createNewCheckBox.AutoSize = true;
            this.createNewCheckBox.Location = new System.Drawing.Point(9, 2);
            this.createNewCheckBox.Name = "createNewCheckBox";
            this.createNewCheckBox.Size = new System.Drawing.Size(150, 17);
            this.createNewCheckBox.TabIndex = 16;
            this.createNewCheckBox.Text = "Создать нового клиента";
            this.createNewCheckBox.UseVisualStyleBackColor = true;
            this.createNewCheckBox.CheckedChanged += new System.EventHandler(this.createNewCheckBox_CheckedChanged);
            // 
            // clientNameBox
            // 
            this.clientNameBox.Location = new System.Drawing.Point(9, 77);
            this.clientNameBox.Name = "clientNameBox";
            this.clientNameBox.Size = new System.Drawing.Size(200, 20);
            this.clientNameBox.TabIndex = 18;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(9, 61);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(127, 13);
            this.label9.TabIndex = 17;
            this.label9.Text = "ФИО контактного лица";
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.label13);
            this.panel1.Controls.Add(this.label12);
            this.panel1.Controls.Add(this.label11);
            this.panel1.Controls.Add(this.clientIDComboBox);
            this.panel1.Controls.Add(this.phoneNumberBox);
            this.panel1.Controls.Add(this.label10);
            this.panel1.Controls.Add(this.companyPersonComboBox);
            this.panel1.Controls.Add(this.clientNameBox);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.createNewCheckBox);
            this.panel1.Controls.Add(this.physPersonComboBox);
            this.panel1.Location = new System.Drawing.Point(230, 25);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(226, 255);
            this.panel1.TabIndex = 19;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(9, 178);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(102, 13);
            this.label13.TabIndex = 25;
            this.label13.Text = "Юридическое лицо";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(9, 139);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(98, 13);
            this.label12.TabIndex = 24;
            this.label12.Text = "Физическое лицо";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(9, 22);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(18, 13);
            this.label11.TabIndex = 23;
            this.label11.Text = "ID";
            // 
            // clientIDComboBox
            // 
            this.clientIDComboBox.FormattingEnabled = true;
            this.clientIDComboBox.Location = new System.Drawing.Point(9, 38);
            this.clientIDComboBox.Name = "clientIDComboBox";
            this.clientIDComboBox.Size = new System.Drawing.Size(200, 21);
            this.clientIDComboBox.TabIndex = 22;
            this.clientIDComboBox.SelectedIndexChanged += new System.EventHandler(this.clientIDComboBox_SelectedIndexChanged);
            // 
            // phoneNumberBox
            // 
            this.phoneNumberBox.Location = new System.Drawing.Point(9, 116);
            this.phoneNumberBox.Name = "phoneNumberBox";
            this.phoneNumberBox.Size = new System.Drawing.Size(200, 20);
            this.phoneNumberBox.TabIndex = 21;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(9, 100);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(103, 13);
            this.label10.TabIndex = 20;
            this.label10.Text = "Контактный номер";
            // 
            // companyPersonComboBox
            // 
            this.companyPersonComboBox.FormattingEnabled = true;
            this.companyPersonComboBox.Location = new System.Drawing.Point(9, 194);
            this.companyPersonComboBox.Name = "companyPersonComboBox";
            this.companyPersonComboBox.Size = new System.Drawing.Size(200, 21);
            this.companyPersonComboBox.TabIndex = 19;
            this.companyPersonComboBox.SelectedIndexChanged += new System.EventHandler(this.companyPersonComboBox_SelectedIndexChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 325);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(444, 36);
            this.button1.TabIndex = 20;
            this.button1.Text = "Сохранить";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // showCargoList
            // 
            this.showCargoList.Location = new System.Drawing.Point(12, 286);
            this.showCargoList.Name = "showCargoList";
            this.showCargoList.Size = new System.Drawing.Size(444, 33);
            this.showCargoList.TabIndex = 21;
            this.showCargoList.Text = "Показать список товаров";
            this.showCargoList.UseVisualStyleBackColor = true;
            this.showCargoList.Click += new System.EventHandler(this.showCargoList_Click);
            // 
            // OrderForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(470, 367);
            this.Controls.Add(this.showCargoList);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.tripIDComboBox);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.costBox);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.tripLengthBox);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.deliveryAddressBox);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.senderAddressBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.senderNameBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel1);
            this.MaximumSize = new System.Drawing.Size(486, 406);
            this.MinimumSize = new System.Drawing.Size(486, 406);
            this.Name = "OrderForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Form1";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox senderNameBox;
        private System.Windows.Forms.ComboBox physPersonComboBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox senderAddressBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox deliveryAddressBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox tripLengthBox;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox costBox;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox tripIDComboBox;
        private System.Windows.Forms.CheckBox createNewCheckBox;
        private System.Windows.Forms.TextBox clientNameBox;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ComboBox companyPersonComboBox;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ComboBox clientIDComboBox;
        private System.Windows.Forms.TextBox phoneNumberBox;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button showCargoList;
    }
}