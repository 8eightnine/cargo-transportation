using System;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Windows.Forms;
using About.Properties;
using cargo_transportation;

namespace About
{
    public class About
    {
        private static RichTextBox mainTextBox;
        private static PictureBox pictureBox;
        private static RichTextBox richTextBox;
        private static MainForm _mainForm;


        // Функция для отрисовки элементов управления
        public static void ShowAbout(MainForm mainForm)
        {
            #region desginer
            _mainForm = mainForm;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            var richTextBox1 = new RichTextBox();
                var pictureBox1 = new PictureBox();
                var menuStrip1 = new MenuStrip();
                ((System.ComponentModel.ISupportInitialize)(pictureBox1)).BeginInit();
                mainForm.SuspendLayout();
                mainForm.Controls.Clear();
                // 
                // richTextBox1
                // 
                richTextBox1.BackColor = SystemColors.Control;
                richTextBox1.BorderStyle = BorderStyle.None;
                richTextBox1.Location = new Point(103, 289);
                richTextBox1.Name = "mainTextBox";
                richTextBox1.Size = new Size(689, 160);
                richTextBox1.TabIndex = 0;
            richTextBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            richTextBox1.ReadOnly = true;
                richTextBox1.Anchor = ((AnchorStyles.Bottom | AnchorStyles.Left) | AnchorStyles.Right);
                mainTextBox = richTextBox1;
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
                pictureBox1.Anchor = (((AnchorStyles.Top | AnchorStyles.Bottom) | AnchorStyles.Left) | AnchorStyles.Right);
                pictureBox = pictureBox1;
            ((System.ComponentModel.ISupportInitialize)(pictureBox1)).EndInit();
                //
                // Adding menu to the form
                //
                Assembly asm = AppDomain.CurrentDomain.GetAssemblies().FirstOrDefault(a => a.GetName().Name == "cargo-transportation");
                Type programMenuType = asm.GetType("cargo_transportation.Classes.ProgramMenu");
                object curUser = asm.GetType("cargo_transportation.MainForm").GetField("currentUser").GetValue(mainForm);
                object programMenuInstance = Activator.CreateInstance(programMenuType);
                MethodInfo populateMethod = programMenuType.GetMethod("Populate");
                ToolStripItemCollection result = (ToolStripItemCollection)populateMethod?.Invoke(programMenuInstance, new object[] { curUser });
                int size = result.Count;
                for (int i = size - 1; i >= 0; i--)
                {
                    menuStrip1.Items.Add(result[i]);
                }

            ShowAboutInto();
            #endregion

            #region Adding controls to the form
            mainForm.Controls.Add(menuStrip1);
            mainForm.Controls.Add(mainTextBox);
            mainForm.Controls.Add(pictureBox1);
            mainForm.ResumeLayout();
            mainForm.PerformLayout();
            mainForm.Text = "ИС ООО \"Перевозки и КО\" | Справка";
            #endregion
        }

        public static void ChangePassword(MainForm mainForm)
        {
            ChangePasswordForm cpass = new ChangePasswordForm(mainForm.currentUser);
            cpass.ShowDialog();
        }

        private static void ShowAboutInto()
        {            
            pictureBox.BringToFront();
            mainTextBox.Visible = true;
            mainTextBox.BringToFront();
            pictureBox.Image = Resources.logo;
            mainTextBox.Height = 160;
            mainTextBox.Text = "Программа ИС ООО \"Перевозки и КО\"\nРазработчик: Бузмаков Антон, АП-227\nРазработано в качестве курсовой работы по дисциплине \"Базы данных\"";
        }


        public static void ShowHelp(MainForm mainForm)
        {
            #region desginer
            _mainForm = mainForm;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            var richTextBox2 = new RichTextBox();
            var menuStrip1 = new MenuStrip();
            mainForm.SuspendLayout();
            mainForm.Controls.Clear();
            // 
            // richTextBox1
            // 
            richTextBox2.BackColor = System.Drawing.SystemColors.Control;
            richTextBox2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            richTextBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            richTextBox2.Location = new System.Drawing.Point(12, 27);
            richTextBox2.Name = "richTextBox1";
            richTextBox2.Size = new System.Drawing.Size(780, 412);
            richTextBox2.TabIndex = 6;
            richTextBox2.Text = resources.GetString("richTextBox1.Text");
            richTextBox = richTextBox2;
            //
            // Adding menu to the form
            //
            Assembly asm = AppDomain.CurrentDomain.GetAssemblies().FirstOrDefault(a => a.GetName().Name == "cargo-transportation");
            Type programMenuType = asm.GetType("cargo_transportation.Classes.ProgramMenu");
            object curUser = asm.GetType("cargo_transportation.MainForm").GetField("currentUser").GetValue(mainForm);
            object programMenuInstance = Activator.CreateInstance(programMenuType);
            MethodInfo populateMethod = programMenuType.GetMethod("Populate");
            ToolStripItemCollection result = (ToolStripItemCollection)populateMethod?.Invoke(programMenuInstance, new object[] { curUser });
            int size = result.Count;
            for (int i = size - 1; i >= 0; i--)
            {
                menuStrip1.Items.Add(result[i]);
            }

            #endregion

            #region Adding controls to the form
            mainForm.Controls.Add(menuStrip1);
            mainForm.Controls.Add(richTextBox);
            mainForm.ResumeLayout();
            mainForm.PerformLayout();
            mainForm.Text = "ИС ООО \"Перевозки и КО\" | Справка";
            #endregion
        }
    }
}