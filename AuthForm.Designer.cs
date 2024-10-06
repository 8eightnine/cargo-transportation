namespace cargo_transportation
{
    partial class AuthForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.LoginButton = new System.Windows.Forms.Button();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.authTab = new System.Windows.Forms.TabPage();
            this.registerLabel = new System.Windows.Forms.Label();
            this.passwordLabel = new System.Windows.Forms.Label();
            this.loginLabel = new System.Windows.Forms.Label();
            this.passwordBox = new System.Windows.Forms.TextBox();
            this.loginBox = new System.Windows.Forms.TextBox();
            this.registerTab = new System.Windows.Forms.TabPage();
            this.progressBar2 = new System.Windows.Forms.ProgressBar();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.switchToLoginLabel = new System.Windows.Forms.Label();
            this.registerPasswordLabel = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.registerPasswordBox = new System.Windows.Forms.TextBox();
            this.registerNameBox = new System.Windows.Forms.TextBox();
            this.registerButton = new System.Windows.Forms.Button();
            this.tabControl.SuspendLayout();
            this.authTab.SuspendLayout();
            this.registerTab.SuspendLayout();
            this.SuspendLayout();
            // 
            // LoginButton
            // 
            this.LoginButton.Location = new System.Drawing.Point(77, 136);
            this.LoginButton.Margin = new System.Windows.Forms.Padding(2);
            this.LoginButton.Name = "LoginButton";
            this.LoginButton.Size = new System.Drawing.Size(93, 23);
            this.LoginButton.TabIndex = 3;
            this.LoginButton.Text = "Войти";
            this.LoginButton.UseVisualStyleBackColor = true;
            this.LoginButton.Click += new System.EventHandler(this.LoginButton_Click);
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.authTab);
            this.tabControl.Controls.Add(this.registerTab);
            this.tabControl.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.tabControl.Location = new System.Drawing.Point(-26, -38);
            this.tabControl.Margin = new System.Windows.Forms.Padding(2);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(351, 218);
            this.tabControl.TabIndex = 0;
            // 
            // authTab
            // 
            this.authTab.Controls.Add(this.registerLabel);
            this.authTab.Controls.Add(this.passwordLabel);
            this.authTab.Controls.Add(this.loginLabel);
            this.authTab.Controls.Add(this.passwordBox);
            this.authTab.Controls.Add(this.loginBox);
            this.authTab.Controls.Add(this.LoginButton);
            this.authTab.Location = new System.Drawing.Point(4, 22);
            this.authTab.Margin = new System.Windows.Forms.Padding(2);
            this.authTab.Name = "authTab";
            this.authTab.Padding = new System.Windows.Forms.Padding(2);
            this.authTab.Size = new System.Drawing.Size(343, 192);
            this.authTab.TabIndex = 0;
            this.authTab.Text = "AuthTab";
            this.authTab.UseVisualStyleBackColor = true;
            // 
            // registerLabel
            // 
            this.registerLabel.AutoSize = true;
            this.registerLabel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.registerLabel.ForeColor = System.Drawing.SystemColors.Highlight;
            this.registerLabel.Location = new System.Drawing.Point(63, 114);
            this.registerLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.registerLabel.Name = "registerLabel";
            this.registerLabel.Size = new System.Drawing.Size(113, 13);
            this.registerLabel.TabIndex = 6;
            this.registerLabel.Text = "Зарегистрироваться";
            this.registerLabel.Click += new System.EventHandler(this.registerLabel_Click);
            // 
            // passwordLabel
            // 
            this.passwordLabel.AutoSize = true;
            this.passwordLabel.Location = new System.Drawing.Point(54, 68);
            this.passwordLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.passwordLabel.Name = "passwordLabel";
            this.passwordLabel.Size = new System.Drawing.Size(45, 13);
            this.passwordLabel.TabIndex = 5;
            this.passwordLabel.Text = "Пароль";
            // 
            // loginLabel
            // 
            this.loginLabel.AutoSize = true;
            this.loginLabel.Location = new System.Drawing.Point(54, 27);
            this.loginLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.loginLabel.Name = "loginLabel";
            this.loginLabel.Size = new System.Drawing.Size(38, 13);
            this.loginLabel.TabIndex = 4;
            this.loginLabel.Text = "Логин";
            // 
            // passwordBox
            // 
            this.passwordBox.Location = new System.Drawing.Point(57, 84);
            this.passwordBox.Margin = new System.Windows.Forms.Padding(2);
            this.passwordBox.Name = "passwordBox";
            this.passwordBox.Size = new System.Drawing.Size(135, 20);
            this.passwordBox.TabIndex = 2;
            this.passwordBox.UseSystemPasswordChar = true;
            this.passwordBox.TextChanged += new System.EventHandler(this.passwordBox_TextChanged);
            // 
            // loginBox
            // 
            this.loginBox.Location = new System.Drawing.Point(57, 42);
            this.loginBox.Margin = new System.Windows.Forms.Padding(2);
            this.loginBox.Name = "loginBox";
            this.loginBox.Size = new System.Drawing.Size(135, 20);
            this.loginBox.TabIndex = 1;
            this.loginBox.TextChanged += new System.EventHandler(this.loginBox_TextChanged);
            // 
            // registerTab
            // 
            this.registerTab.Controls.Add(this.progressBar2);
            this.registerTab.Controls.Add(this.progressBar1);
            this.registerTab.Controls.Add(this.switchToLoginLabel);
            this.registerTab.Controls.Add(this.registerPasswordLabel);
            this.registerTab.Controls.Add(this.label3);
            this.registerTab.Controls.Add(this.registerPasswordBox);
            this.registerTab.Controls.Add(this.registerNameBox);
            this.registerTab.Controls.Add(this.registerButton);
            this.registerTab.Location = new System.Drawing.Point(4, 22);
            this.registerTab.Margin = new System.Windows.Forms.Padding(2);
            this.registerTab.Name = "registerTab";
            this.registerTab.Padding = new System.Windows.Forms.Padding(2);
            this.registerTab.Size = new System.Drawing.Size(343, 192);
            this.registerTab.TabIndex = 1;
            this.registerTab.Text = "RegisterTab";
            this.registerTab.UseVisualStyleBackColor = true;
            // 
            // progressBar2
            // 
            this.progressBar2.Location = new System.Drawing.Point(57, 86);
            this.progressBar2.Name = "progressBar2";
            this.progressBar2.Size = new System.Drawing.Size(135, 23);
            this.progressBar2.TabIndex = 14;
            // 
            // progressBar1
            // 
            this.progressBar1.ForeColor = System.Drawing.Color.Green;
            this.progressBar1.Location = new System.Drawing.Point(57, 44);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(135, 23);
            this.progressBar1.TabIndex = 13;
            // 
            // switchToLoginLabel
            // 
            this.switchToLoginLabel.AutoSize = true;
            this.switchToLoginLabel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.switchToLoginLabel.ForeColor = System.Drawing.SystemColors.Highlight;
            this.switchToLoginLabel.Location = new System.Drawing.Point(47, 114);
            this.switchToLoginLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.switchToLoginLabel.Name = "switchToLoginLabel";
            this.switchToLoginLabel.Size = new System.Drawing.Size(167, 13);
            this.switchToLoginLabel.TabIndex = 12;
            this.switchToLoginLabel.Text = "Уже зарегистрированы? Войти";
            this.switchToLoginLabel.Click += new System.EventHandler(this.switchToLoginLabel_Click);
            // 
            // registerPasswordLabel
            // 
            this.registerPasswordLabel.AutoSize = true;
            this.registerPasswordLabel.Location = new System.Drawing.Point(54, 68);
            this.registerPasswordLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.registerPasswordLabel.Name = "registerPasswordLabel";
            this.registerPasswordLabel.Size = new System.Drawing.Size(45, 13);
            this.registerPasswordLabel.TabIndex = 11;
            this.registerPasswordLabel.Text = "Пароль";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(54, 27);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(38, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "Логин";
            // 
            // registerPasswordBox
            // 
            this.registerPasswordBox.ForeColor = System.Drawing.Color.Black;
            this.registerPasswordBox.Location = new System.Drawing.Point(57, 84);
            this.registerPasswordBox.Margin = new System.Windows.Forms.Padding(2);
            this.registerPasswordBox.Name = "registerPasswordBox";
            this.registerPasswordBox.Size = new System.Drawing.Size(135, 20);
            this.registerPasswordBox.TabIndex = 8;
            this.registerPasswordBox.UseSystemPasswordChar = true;
            this.registerPasswordBox.TextChanged += new System.EventHandler(this.registerPasswordBox_TextChanged);
            // 
            // registerNameBox
            // 
            this.registerNameBox.ForeColor = System.Drawing.Color.Black;
            this.registerNameBox.Location = new System.Drawing.Point(57, 42);
            this.registerNameBox.Margin = new System.Windows.Forms.Padding(2);
            this.registerNameBox.Name = "registerNameBox";
            this.registerNameBox.Size = new System.Drawing.Size(135, 20);
            this.registerNameBox.TabIndex = 7;
            this.registerNameBox.TextChanged += new System.EventHandler(this.registerNameBox_TextChanged);
            // 
            // registerButton
            // 
            this.registerButton.Location = new System.Drawing.Point(77, 136);
            this.registerButton.Margin = new System.Windows.Forms.Padding(2);
            this.registerButton.Name = "registerButton";
            this.registerButton.Size = new System.Drawing.Size(93, 23);
            this.registerButton.TabIndex = 9;
            this.registerButton.Text = "Войти";
            this.registerButton.UseVisualStyleBackColor = true;
            this.registerButton.Click += new System.EventHandler(this.registerButton_Click);
            // 
            // AuthForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(214, 159);
            this.Controls.Add(this.tabControl);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AuthForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Авторизация";
            this.TopMost = true;
            this.tabControl.ResumeLayout(false);
            this.authTab.ResumeLayout(false);
            this.authTab.PerformLayout();
            this.registerTab.ResumeLayout(false);
            this.registerTab.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button LoginButton;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage authTab;
        private System.Windows.Forms.TabPage registerTab;
        private System.Windows.Forms.TextBox loginBox;
        private System.Windows.Forms.Label loginLabel;
        private System.Windows.Forms.TextBox passwordBox;
        private System.Windows.Forms.Label passwordLabel;
        private System.Windows.Forms.Label registerLabel;
        private System.Windows.Forms.Label switchToLoginLabel;
        private System.Windows.Forms.Label registerPasswordLabel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox registerPasswordBox;
        private System.Windows.Forms.TextBox registerNameBox;
        private System.Windows.Forms.Button registerButton;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.ProgressBar progressBar2;
    }
}

