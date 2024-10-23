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
                var richTextBox2 = new RichTextBox();
                var button1 = new Button();
                var button2 = new Button();
                var pictureBox1 = new PictureBox();
                var menuStrip1 = new MenuStrip();
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
                richTextBox1.BackColor = System.Drawing.SystemColors.Control;
                richTextBox1.BorderStyle = BorderStyle.None;
                richTextBox1.Location = new Point(103, 289);
                richTextBox1.Name = "mainTextBox";
                richTextBox1.Size = new Size(689, 160);
                richTextBox1.TabIndex = 0;
                richTextBox1.ReadOnly = true;
                mainTextBox = richTextBox1;
                // 
                // richTextBox2
                // 
                richTextBox2.BackColor = System.Drawing.SystemColors.Control;
                richTextBox2.BorderStyle = BorderStyle.None;
                richTextBox2.Location = new Point(103, 35);
                richTextBox2.Name = "infoTextBox";
                richTextBox2.Size = new Size(689, 560);
                richTextBox2.TabIndex = 0;
                richTextBox2.ReadOnly = true;
                infoTextBox = richTextBox2;
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
                developerButton = button2;
                // 
                // pictureBox1
                // 
                pictureBox1.Location = new Point(311, 27);
                pictureBox1.Name = "logoBox";
                pictureBox1.Size = new Size(256, 241);
                pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                pictureBox1.TabIndex = 3;
                pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                pictureBox1.TabStop = false;
                pictureBox = pictureBox1;

                ((System.ComponentModel.ISupportInitialize)(pictureBox1)).EndInit();
                mainForm.Controls.Add(richTextBox1);
                mainForm.Controls.Add(richTextBox2);
                mainForm.Controls.Add(button1);
                mainForm.Controls.Add(button2);
                mainForm.Controls.Add(pictureBox1);
                mainForm.ResumeLayout(false);
                _isInitialized = true;
            }
        }

        private static void Button1_Click(object sender, EventArgs e)
        {
            pictureBox.BringToFront();
            mainTextBox.BringToFront();
            infoTextBox.Visible = false;
            pictureBox.Image = Resources.logo;
            mainTextBox.Height = 160;
            mainTextBox.Text = "Программа разработана для упрощения жизни и просто для кайфа";
        }

        private static void Button2_Click(object sender, EventArgs e)
        {
            infoTextBox.Visible = true;
            infoTextBox.BringToFront();
            infoTextBox.Text = "РУКОВОДСТВО ПОЛЬЗОВАТЕЛЯ БУДЕТ ТУТ";
        }
    }
}