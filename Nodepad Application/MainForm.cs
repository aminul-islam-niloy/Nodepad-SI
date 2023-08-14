using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;

namespace Nodepad_Application
{
    public partial class mainForm : Form
    {
        private bool fileAreadySave;
        private bool fileUpdated;
        private string currentFileName;
        private  FontDialog fontDialog = new FontDialog();

        public mainForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
           // fileAreadySave = true;
            fileAreadySave = false;
            fileUpdated = false;
            currentFileName = "";

            if (Control.IsKeyLocked(Keys.CapsLock))
            {
                CAPStoolStripStatusLabel1.Text = "CAPS ON";
            }
            else
            {
                CAPStoolStripStatusLabel1.Text = "caps off";
            }
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFile();

        }

        private void SaveFile()
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Text Files ( *.txt)| *.txt|Rich Text Files (*.rtf)|*.rtf";

            DialogResult save_result = sfd.ShowDialog();
            if (save_result == DialogResult.OK)
            {
                if (Path.GetExtension(sfd.FileName) == ".txt")
                {
                    MainrichTextBox1.SaveFile(sfd.FileName, RichTextBoxStreamType.PlainText);
                }
                if (Path.GetExtension(sfd.FileName) == ".rtf")
                {
                    MainrichTextBox1.SaveFile(sfd.FileName, RichTextBoxStreamType.RichText);
                }

                this.Text = Path.GetFileName(sfd.FileName) + "- Notepad SI";

                fileAreadySave = true;
                fileUpdated = false;
                currentFileName = sfd.FileName;
            }
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.MessageBox.Show("This Application is developed by Aminul Islam Niloy", "Notepad SI", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (fileUpdated)
            {
                DialogResult dia_res = System.Windows.Forms.MessageBox.Show("Do you want to save before exit? ",
                                   "File Save", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);

                switch (dia_res)
                {
                    case DialogResult.Yes:

                        SaveFileUpdate();
                        Application.Exit();

                        break;

                    case DialogResult.No:
                        Application.Exit();
                        break;
                }
            }
            else
            {

                Application.Exit();
            }
           
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            fileUpdated = true;
            undoToolStripMenuItem.Enabled = true;
            undo.Enabled= true;
            undoMenuStrip.Enabled = true;
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            newAddSection();
        }

        private void newAddSection()
        {
            if (fileUpdated)
            {
                DialogResult dia_res = System.Windows.Forms.MessageBox.Show("Do you want to save your changes? ",
                                   "File Save", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);

                switch (dia_res)
                {
                    case DialogResult.Yes:
                        // SaveFileUpdate();
                        SaveFile();
                        clear_Screen();
                        break;
                    case DialogResult.No:
                        clear_Screen();
                        break;
                }
            }
            else
            {
                clear_Screen();
            }

            MessageBox.Text = "New File is crated ";
            // have to set timer 3 sec 

            underlineToolStripMenuItem.Enabled = false;
            undo.Enabled = false;
            undoMenuStrip.Enabled = false;

            redoToolStripMenuItem.Enabled = false;
            redo.Enabled = false;
            redodoMenuStrip.Enabled = false;
            
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openAnFile();

        }

        private void openAnFile()
        {
            if (fileUpdated)
            {
                DialogResult dia_res = System.Windows.Forms.MessageBox.Show("Do you want to save before opan file? ",
                                   "File Save", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);

                switch (dia_res)
                {
                    case DialogResult.Yes:
                        
                        SaveFileUpdate();
                        openFiles();
                        break;

                    case DialogResult.No:
                        openFiles();
                        break;
                }
            }
            else
            {

                openFiles();
            }
        }

        private void openFiles()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Text Files ( *.txt)| *.txt|Rich Text Files (*.rtf)|*.rtf";

            DialogResult result = ofd.ShowDialog();
            if (result == DialogResult.OK)
            {
                if (Path.GetExtension(ofd.FileName) == ".txt")
                {
                    MainrichTextBox1.LoadFile(ofd.FileName, RichTextBoxStreamType.PlainText);
                }
                if (Path.GetExtension(ofd.FileName) == ".rtf")
                {
                    MainrichTextBox1.LoadFile(ofd.FileName, RichTextBoxStreamType.RichText);
                }
                this.Text = Path.GetFileName(ofd.FileName) + "- Notepad SI";

                fileAreadySave = true;
                fileUpdated = false;
                currentFileName = ofd.FileName;

                //1 new task is: if open in loaded and after updaded if again open is called,
                // then it call to the user that he wants to save or not.


            }

            MessageBox.Text = "File is lodded sucessfully "; 

            underlineToolStripMenuItem.Enabled = false;
            undo.Enabled = false;
            undoMenuStrip.Enabled = false;

            redoToolStripMenuItem.Enabled = false;
            redo.Enabled = false;
            redodoMenuStrip.Enabled = false;
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileUpdate();
            MessageBox.Text = "File is saved ";
        }

        private void SaveFileUpdate()
        {
            if (fileAreadySave)
            {
                if (Path.GetExtension(currentFileName) == ".txt")
                {
                    MainrichTextBox1.SaveFile(currentFileName, RichTextBoxStreamType.PlainText);
                }

                if (Path.GetExtension(currentFileName) == ".rtf")
                {
                    MainrichTextBox1.SaveFile(currentFileName, RichTextBoxStreamType.RichText);
                }
                fileUpdated = false;

                MessageBox.Text = "File is  saveed";
            }

            else
            {
                if (fileUpdated)
                {
                    SaveFile();
                }
                else
                {
                    clear_Screen();
                }

            }
        }

        private void clear_Screen()
        {
            MainrichTextBox1.Clear();
            fileUpdated = false;
            this.Text = "Notepad SI";
        }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            undoBtn();
        }

        private void undoBtn()
        {
            MainrichTextBox1.Undo();

            undoToolStripMenuItem.Enabled = false;
            undo.Enabled = false;
            undoMenuStrip.Enabled = false;
            redoToolStripMenuItem.Enabled = true;
            redo.Enabled = true;
            redodoMenuStrip.Enabled = true;

            MessageBox.Text = "Undo done ";
        }

        private void redoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            redoBtn();
        }

        private void redoBtn()
        {
            MainrichTextBox1.Redo();
            undoToolStripMenuItem.Enabled = true;
            undo.Enabled = true;
            undoMenuStrip.Enabled= true;

            redoToolStripMenuItem.Enabled = false;
            redo.Enabled = false;
            redodoMenuStrip.Enabled = false;

            MessageBox.Text = "Redo done ";

        }

        private void sellectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MainrichTextBox1.SelectAll();
        }

        private void dateTimeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MainrichTextBox1.SelectedText = DateTime.Now.ToString();
        }

        private void boldToolStripMenuItem_Click(object sender, EventArgs e)
        {
            boldBtn();
        }

        private void boldBtn()
        {
            FontStyle_apply(FontStyle.Bold);

            MessageBox.Text = "Change to bold ";
        }

        private void FontStyle_apply(FontStyle fontStyle)
        {
            MainrichTextBox1.SelectionFont = new Font(MainrichTextBox1.Font, fontStyle);
        }

        //private void FontTextstyle()
        //{
           
        //}

        private void fonteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fonteStyleBtn();

        }

        private void fonteStyleBtn()
        {
            fontDialog.ShowColor = true;
            fontDialog.ShowApply = true;

            fontDialog.Apply += new System.EventHandler(font_apply_dialog);


            DialogResult result = fontDialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                if (MainrichTextBox1.SelectionLength > 0)
                {
                    MainrichTextBox1.SelectionFont = fontDialog.Font;
                    MainrichTextBox1.SelectionColor = fontDialog.Color;
                }

            }
        }

        private void font_apply_dialog(object sender, EventArgs e)
        {
            if (MainrichTextBox1.SelectionLength > 0)
            {
                MainrichTextBox1.SelectionFont = fontDialog.Font;
                MainrichTextBox1.SelectionColor= fontDialog.Color;
            }
        }

        private void changeColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            colorBtn();
        }

        private void colorBtn()
        {
            ColorDialog colorDialog = new ColorDialog();
        
            DialogResult result = colorDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                if (MainrichTextBox1.SelectionLength > 0)
                {
                    MainrichTextBox1.SelectionColor = colorDialog.Color;
                }
            }
        }

        private void viewToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void normalToolStripMenuItem_Click(object sender, EventArgs e)
        {

            FontStyle_apply(FontStyle.Regular);
        }

        private void italicToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FontStyle_apply(FontStyle.Italic);
        }

        private void underlineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FontStyle_apply(FontStyle.Underline);
        }

        private void toolStripContainer1_TopToolStripPanel_Click(object sender, EventArgs e)
        {

        }

        private void fileToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            newAddSection();
        }

        private void open_Click(object sender, EventArgs e)
        {
            openAnFile();
        }

        private void save_Click(object sender, EventArgs e)
        {
            SaveFileUpdate();
        }

        private void save_as_Click(object sender, EventArgs e)
        {
            SaveFile();
        }

        private void undo_Click(object sender, EventArgs e)
        {
            undoBtn();
        }

        private void redo_Click(object sender, EventArgs e)
        {
            redoBtn();
        }

        private void toolStripButton12_Click(object sender, EventArgs e)
        {
            fonteStyleBtn();
        }

        private void toolStripButton9_Click(object sender, EventArgs e)
        {
            pasteText();
        }

        private void toolStripButton13_Click(object sender, EventArgs e)
        {
            colorBtn();
        }

        private void toolStripButton14_Click(object sender, EventArgs e)
        {
            FontStyle_apply(FontStyle.Bold);
        }

        int click ;
        private void toolStripButton15_Click(object sender, EventArgs e)
        {
            FontStyle_apply(FontStyle.Underline);
                    

        }

        private void toolStripButton16_Click(object sender, EventArgs e)
        {
            FontStyle_apply(FontStyle.Italic);
        }

        private void toolStripButton18_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.MessageBox.Show("This Application is developed by Aminul Islam Niloy", "Notepad SI", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
     
        }

        private void closeAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clear_Screen();
            MessageBox.Text = "All closed ";
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            clear_Screen();
        }

        private void MainrichTextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            capsLockBtn();

           

        }

       

        private void capsLockBtn()
        {
            if (Control.IsKeyLocked(Keys.CapsLock))
            {
                CAPStoolStripStatusLabel1.Text = "CAPS ON";
            }
            else
            {
                CAPStoolStripStatusLabel1.Text = "caps off";
            }
        }

        private void toolStripStatusLabel1_Click(object sender, EventArgs e)
        {

        }

        private void backToNormalToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void undoMenuStrip_Click(object sender, EventArgs e)
        {
            undoBtn();
        }

        private void redodoMenuStrip_Click(object sender, EventArgs e)
        {
            redoBtn();
        }

        private void backToNormalToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            FontStyle_apply(FontStyle.Regular);
        }

        private void toolStripButton1_Click_1(object sender, EventArgs e)
        {
            
        }

        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MainrichTextBox1.SelectAll();
        }

        private void fonteStyleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fonteStyleBtn();
        }

        private void colorApplyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            colorBtn();
        }

        private void boldToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            boldBtn();
        }

        private void underlineToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            FontStyle_apply(FontStyle.Underline);
        }

        private void italicToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            FontStyle_apply(FontStyle.Italic);
        }

        private void fontGalleryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fonteStyleBtn();
        }

        private void normalToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            FontStyle_apply(FontStyle.Regular);
        }

        private void toolStripButton17_Click(object sender, EventArgs e)
        {
            
            FontStyle_apply(FontStyle.Regular);
           
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // MainrichTextBox1.Copy();

            copyText();
        }

        private void copyText()
        {
            if (MainrichTextBox1.SelectionLength > 0)
            {
                Clipboard.SetText(MainrichTextBox1.SelectedText);

            }
        }

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cutText();
        }

        private void cutText()
        {
            if (MainrichTextBox1.SelectionLength > 0)
            {
                Clipboard.SetText(MainrichTextBox1.SelectedText);
                MainrichTextBox1.SelectedText = "";
            }
        }

        private void pasteToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            pasteText();
        }

        private void pasteText()
        {
            if (Clipboard.ContainsText())
            {
                MainrichTextBox1.SelectedText = Clipboard.GetText();
            }
        }

        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            copyText();
        }

        private void toolStripButton8_Click(object sender, EventArgs e)
        {
            cutText();
        }

        private void copyToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            copyText();
        }

        private void cutToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            cutText();
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pasteText();
        }

       

        private void toolStripButton1_Click_2(object sender, EventArgs e)
        {
            MainrichTextBox1.SelectionAlignment= HorizontalAlignment.Left;
        }

        private void middleAllignment_Click(object sender, EventArgs e)
        {
            MainrichTextBox1.SelectionAlignment = HorizontalAlignment.Center;
        }

        private void rightAlllignment_Click(object sender, EventArgs e)
        {
            MainrichTextBox1.SelectionAlignment = HorizontalAlignment.Right;
        }


       



    }

}
