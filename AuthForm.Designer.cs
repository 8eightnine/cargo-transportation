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
            this.registerTab = new System.Windows.Forms.TabPage();
            this.loginBox = new System.Windows.Forms.TextBox();
            this.passwordBox = new System.Windows.Forms.TextBox();
            this.loginLabel = new System.Windows.Forms.Label();
            this.passwordLabel = new System.Windows.Forms.Label();
            this.registerLabel = new System.Windows.Forms.Label();
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
            this.LoginButton.Location = new System.Drawing.Point(115, 210);
            this.LoginButton.Name = "LoginButton";
            this.LoginButton.Size = new System.Drawing.Size(140, 35);
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
            this.tabControl.Location = new System.Drawing.Point(-1, -62);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(379, 303);
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
            this.authTab.Location = new System.Drawing.Point(4, 29);
            this.authTab.Name = "authTab";
            this.authTab.Padding = new System.Windows.Forms.Padding(3);
            this.authTab.Size = new System.Drawing.Size(371, 270);
            this.authTab.TabIndex = 0;
            this.authTab.Text = "AuthTab";
            this.authTab.UseVisualStyleBackColor = true;
            // 
            // registerTab
            // 
            this.registerTab.Controls.Add(this.switchToLoginLabel);
            this.registerTab.Controls.Add(this.registerPasswordLabel);
            this.registerTab.Controls.Add(this.label3);
            this.registerTab.Controls.Add(this.registerPasswordBox);
            this.registerTab.Controls.Add(this.registerNameBox);
            this.registerTab.Controls.Add(this.registerButton);
            this.registerTab.Location = new System.Drawing.Point(4, 29);
            this.registerTab.Name = "registerTab";
            this.registerTab.Padding = new System.Windows.Forms.Padding(3);
            this.registerTab.Size = new System.Drawing.Size(371, 270);
            this.registerTab.TabIndex = 1;
            this.registerTab.Text = "RegisterTab";
            this.registerTab.UseVisualStyleBackColor = true;
            // 
            // loginBox
            // 
            this.loginBox.Location = new System.Drawing.Point(86, 64);
            this.loginBox.Name = "loginBox";
            this.loginBox.Size = new System.Drawing.Size(200, 26);
            this.loginBox.TabIndex = 1;
            this.loginBox.TextChanged += new System.EventHandler(this.loginBox_TextChanged);
            // 
            // passwordBox
            // 
            this.passwordBox.Location = new System.Drawing.Point(86, 130);
            this.passwordBox.Name = "passwordBox";
            this.passwordBox.Size = new System.Drawing.Size(200, 26);
            this.passwordBox.TabIndex = 2;
            this.passwordBox.UseSystemPasswordChar = true;
            this.passwordBox.TextChanged += new System.EventHandler(this.passwordBox_TextChanged);
            // 
            // loginLabel
            // 
            this.loginLabel.AutoSize = true;
            this.loginLabel.Location = new System.Drawing.Point(82, 41);
            this.loginLabel.Name = "loginLabel";
            this.loginLabel.Size = new System.Drawing.Size(55, 20);
            this.loginLabel.TabIndex = 4;
            this.loginLabel.Text = "Логин";
            // 
            // passwordLabel
            // 
            this.passwordLabel.AutoSize = true;
            this.passwordLabel.Location = new System.Drawing.Point(81, 105);
            this.passwordLabel.Name = "passwordLabel";
            this.passwordLabel.Size = new System.Drawing.Size(67, 20);
            this.passwordLabel.TabIndex = 5;
            this.passwordLabel.Text = "Пароль";
            // 
            // registerLabel
            // 
            this.registerLabel.AutoSize = true;
            this.registerLabel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.registerLabel.ForeColor = System.Drawing.SystemColors.Highlight;
            this.registerLabel.Location = new System.Drawing.Point(95, 175);
            this.registerLabel.Name = "registerLabel";
            this.registerLabel.Size = new System.Drawing.Size(169, 20);
            this.registerLabel.TabIndex = 6;
            this.registerLabel.Text = "Зарегистрироваться";
            this.registerLabel.Click += new System.EventHandler(this.registerLabel_Click);
            // 
            // switchToLoginLabel
            // 
            this.switchToLoginLabel.AutoSize = true;
            this.switchToLoginLabel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.switchToLoginLabel.ForeColor = System.Drawing.SystemColors.Highlight;
            this.switchToLoginLabel.Location = new System.Drawing.Point(70, 175);
            this.switchToLoginLabel.Name = "switchToLoginLabel";
            this.switchToLoginLabel.Size = new System.Drawing.Size(245, 20);
            this.switchToLoginLabel.TabIndex = 12;
            this.switchToLoginLabel.Text = "Уже зарегистрированы? Войти";
            this.switchToLoginLabel.Click += new System.EventHandler(this.switchToLoginLabel_Click);
            // 
            // registerPasswordLabel
            // 
            this.registerPasswordLabel.AutoSize = true;
            this.registerPasswordLabel.Location = new System.Drawing.Point(81, 105);
            this.registerPasswordLabel.Name = "registerPasswordLabel";
            this.registerPasswordLabel.Size = new System.Drawing.Size(67, 20);
            this.registerPasswordLabel.TabIndex = 11;
            this.registerPasswordLabel.Text = "Пароль";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(81, 42);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 20);
            this.label3.TabIndex = 10;
            this.label3.Text = "Логин";
            // 
            // registerPasswordBox
            // 
            this.registerPasswordBox.Location = new System.Drawing.Point(86, 130);
            this.registerPasswordBox.Name = "registerPasswordBox";
            this.registerPasswordBox.Size = new System.Drawing.Size(200, 26);
            this.registerPasswordBox.TabIndex = 8;
            // 
            // registerNameBox
            // 
            this.registerNameBox.Location = new System.Drawing.Point(86, 64);
            this.registerNameBox.Name = "registerNameBox";
            this.registerNameBox.Size = new System.Drawing.Size(200, 26);
            this.registerNameBox.TabIndex = 7;
            // 
            // registerButton
            // 
            this.registerButton.Location = new System.Drawing.Point(115, 210);
            this.registerButton.Name = "registerButton";
            this.registerButton.Size = new System.Drawing.Size(140, 35);
            this.registerButton.TabIndex = 9;
            this.registerButton.Text = "Войти";
            this.registerButton.UseVisualStyleBackColor = true;
            this.registerButton.Click += new System.EventHandler(this.registerButton_Click);
            // 
            // AuthForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(378, 244);
            this.Controls.Add(this.tabControl);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AuthForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
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
    }
}

