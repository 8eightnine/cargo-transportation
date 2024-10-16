using AboutLibrary.Properties;
using System;
using System.Drawing;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace AboutLibrary
{
    public class About
    {

        private static RichTextBox mainTextBox;
        private static Button aboutButton;
        private static Button developerButton;
        private static PictureBox pictureBox;
        private static MenuStrip menuStrip;


        public static void ShowAbout(Form mainForm)
        {
            var richTextBox1 = new RichTextBox();
            var button1 = new Button();
            var button2 = new Button();
            var pictureBox1 = new PictureBox();
            var menuStrip1 = new MenuStrip();
            ((System.ComponentModel.ISupportInitialize)(pictureBox1)).BeginInit();
            mainForm.SuspendLayout();
            mainForm.Controls.Clear();
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
            mainForm.Controls.Add(button1);
            mainForm.Controls.Add(button2);
            mainForm.Controls.Add(pictureBox1);
            mainForm.ResumeLayout(false);
        }

        private static void Button1_Click(object sender, EventArgs e)
        {
            pictureBox.Image = Resources.logo;
            mainTextBox.Text = "Программа разработана для упрощения жизни и просто для кайфа";
        }
    }
}
