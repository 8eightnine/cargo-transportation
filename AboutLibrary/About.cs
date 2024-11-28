using About.Properties;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace About
{
    public class About
    {
        private static bool _isInitialized;
        private static RichTextBox mainTextBox;
        private static RichTextBox infoTextBox;
        private static Button aboutButton;
        private static Button developerButton;
        private static PictureBox pictureBox;
        private static WebBrowser webBrowser;


        // Функция для отрисовки элементов управления
        public static void ShowAbout(Form mainForm)
        {
            if (_isInitialized)
            {
                return;
            }
            else
            {
                var richTextBox1 = new RichTextBox();
                var button1 = new Button();
                var button2 = new Button();
                var pictureBox1 = new PictureBox();
                var menuStrip1 = new MenuStrip();
                var webBrowser1 = new WebBrowser();
                ((System.ComponentModel.ISupportInitialize)(pictureBox1)).BeginInit();
                mainForm.SuspendLayout();
                foreach (Control control in mainForm.Controls.OfType<Control>().ToList())
                {
                    if (!(control is MenuStrip))
                    {
                        mainForm.Controls.Remove(control);
                    }
                }
                // 
                // richTextBox1
                // 
                richTextBox1.BackColor = SystemColors.Control;
                richTextBox1.BorderStyle = BorderStyle.None;
                richTextBox1.Location = new Point(103, 289);
                richTextBox1.Name = "mainTextBox";
                richTextBox1.Size = new Size(689, 160);
                richTextBox1.TabIndex = 0;
                richTextBox1.ReadOnly = true;
                richTextBox1.Anchor = ((AnchorStyles)(((AnchorStyles.Bottom | AnchorStyles.Left) | AnchorStyles.Right)));
                mainTextBox = richTextBox1;
                // 
                // button1
                // 
                button1.Location = new Point(12, 27);
                button1.Name = "aboutButton";
                button1.Size = new Size(85, 23);
                button1.Click += Button1_Click;
                button1.TabIndex = 1;
                button1.Text = "О программе";
                button1.UseVisualStyleBackColor = true;
                button1.Anchor = ((AnchorStyles)(AnchorStyles.Top | AnchorStyles.Left));
                aboutButton = button1;
                // 
                // button2
                // 
                button2.Location = new Point(12, 56);
                button2.Name = "developerButton";
                button2.Size = new Size(85, 23);
                button2.Click += Button2_Click;
                button2.TabIndex = 2;
                button2.Text = "Разработчик";
                button2.UseVisualStyleBackColor = true;
                button2.Anchor = ((AnchorStyles)(AnchorStyles.Top | AnchorStyles.Left));
                developerButton = button2;
                // 
                // pictureBox1
                // 
                pictureBox1.Location = new Point(311, 27);
                pictureBox1.Name = "logoBox";
                pictureBox1.Size = new Size(256, 241);
                pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
                pictureBox1.TabIndex = 3;
                pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
                pictureBox1.TabStop = false;
                pictureBox1.Anchor = ((AnchorStyles)((((AnchorStyles.Top | AnchorStyles.Bottom) | AnchorStyles.Left) | AnchorStyles.Right)));
                pictureBox = pictureBox1;
                // 
                // webBrowser1
                // 
                webBrowser1.Location = new Point(103, 27);
                webBrowser1.MinimumSize = new Size(20, 20);
                webBrowser1.Name = "webBrowser1";
                webBrowser1.Size = new Size(689, 422);
                webBrowser1.TabIndex = 5;
                webBrowser1.Visible = false;
                webBrowser1.Url = new Uri("https://glowing-alfajores-408db9.netlify.app/", UriKind.Absolute);
                webBrowser1.Anchor = ((AnchorStyles)((((AnchorStyles.Top | AnchorStyles.Bottom) | AnchorStyles.Left) | AnchorStyles.Right)));
                webBrowser = webBrowser1;
                //
                // Adding controls to the form
                //
                ((System.ComponentModel.ISupportInitialize)(pictureBox1)).EndInit();
                mainForm.Controls.Add(richTextBox1);
                mainForm.Controls.Add(button1);
                mainForm.Controls.Add(button2);
                mainForm.Controls.Add(webBrowser);
                mainForm.Controls.Add(pictureBox1);
                mainForm.ResumeLayout();
                mainForm.PerformLayout();
                _isInitialized = true;
                mainForm.Text = "ИС ООО \"Перевозки и КО\" | Справка";
            }
        }

        private static void Button1_Click(object sender, EventArgs e)
        {
            pictureBox.BringToFront();
            mainTextBox.BringToFront();
            webBrowser.Visible = false;
            pictureBox.Image = Resources.logo;
            mainTextBox.Height = 160;
            mainTextBox.Text = "Программа ИС ООО \"Перевозки и КО\"\nРазработчик: Бузмаков Антон, АП-227\nРазработано в качестве курсовой работы по дисциплине \"Базы данных\"";
        }

        private static void Button2_Click(object sender, EventArgs e)
        {
            webBrowser.Visible = true;
            webBrowser.BringToFront();
        }
    }
}