using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SharpManager
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public Stack<List<object>> Back_Stack = new Stack<List<object>>();
        public Stack<string> PreviousDirectory_Stack = new Stack<string>();
        public string currentDirectory;
        public BindingList<object> fileList = new BindingList<object>();
        public List<object> previousFiles = new List<object>();

        private void Form1_Load(object sender, EventArgs e)
        {
            // Set DataGridView size
            FileGridView.Size = new Size(this.Width - 50, this.Height - 150);

            // Reset contents of DataGridView
            FileGridView.Rows.Clear();
            FileGridView.Refresh();

            // Initializes DataGridView with available drives to navigate to
            List<string> drives = FileManager.getDrives();

            foreach (string drive in drives)
            {
                FileGridView.Rows.Add(drive);
            }
        }

        private void B_UpALevel_Click(object sender, EventArgs e)
        {
            List<object> files = new List<object>();
            string currentDirectory = TB_CurrentDirectory.Text;

            string[] directoryArray = currentDirectory.Split(System.IO.Path.DirectorySeparatorChar);
            string directoryToUse = "";
            for (int i = 0; i < directoryArray.Length - 1; i++)
            {
                directoryToUse += directoryArray[i] + "\\";
            }

            files = FileManager.getDirectoryContents(directoryToUse);
            WriteToGridView(files);
            try
            {
                TB_CurrentDirectory.Text = PreviousDirectory_Stack.Pop();
            }

            catch (InvalidOperationException)
            {
                MessageBox.Show("Cannot go up another level");
            }
        }

        private void B_Back_Click(object sender, EventArgs e)
        {
            /* Clears current items in FileGridView, replaces them files/directories 
             * from previous directory
             */

            try
            {
                previousFiles = Back_Stack.Pop();
                TB_CurrentDirectory.Text = PreviousDirectory_Stack.Pop();
            }
            catch (InvalidOperationException)
            {
                MessageBox.Show("Can't go up further.");
                return;
            }

            if (previousFiles != null)
            {
                WriteToGridView(previousFiles);
            }
        }

        private void TB_CurrentDirectory_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (TB_CurrentDirectory.Text == "")
                {
                    // Set FileGridView to initial data
                    Form1_Load(sender, e);
                }

                else
                {
                    IntoNewDirectory(sender, true, null);
                }
            }
        }

        private void SetPreviousReferences()
        {
            // Puts the current file list into the previous file list and pushes it to the stack for use in the Back function

            List<object> prevFileList = new List<object>(fileList);
            
            Back_Stack.Push(prevFileList);
            PreviousDirectory_Stack.Push(TB_CurrentDirectory.Text);
        }

        private void IntoNewDirectory(object sender, bool isFromTB, DataGridViewCellEventArgs e)
        {
            /* When an item in the DataGridView is double-clicked, its value is sent to the DirectorySearcher
            * function, the DataGridView is cleared, and the new file strings are put in
            * 
            * Replicates moving into a directory
            */

            DataGridView list = null;
            TextBox TB_list = null;
            string item = null;

            if (isFromTB)
            {
                TB_list = (TextBox)sender;
                item = TB_list.Text.ToString();
            }

            else
            {
                list = (DataGridView)sender;
                item = list[e.ColumnIndex, e.RowIndex].Value.ToString();
            }


            if (File.Exists(item))
            {
                FileManager.openFile(item);
            }

            else
            {
                List<object> currentFiles = new List<object>();
                SetPreviousReferences();

                item = item.Replace(" (Folder)", string.Empty);

                currentDirectory = item;
                TB_CurrentDirectory.Text = currentDirectory;

                currentFiles = FileManager.getDirectoryContents(item.ToString());

                WriteToGridView(currentFiles);
            }
        }

        private void WriteToGridView(List<object> files)
        {
            fileList =  new BindingList<object>(files);
            FileGridView.Columns[0].DataPropertyName = "Filename";
            FileGridView.Columns[1].DataPropertyName = "Filetype";
            FileGridView.Columns[2].DataPropertyName = "Filesize";
            FileGridView.Columns[3].DataPropertyName = "DateCreated";
            FileGridView.Columns[4].DataPropertyName = "DateModified";
            FileGridView.DataSource = fileList;
        }

        private void propertiesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //string item = FileListBox.SelectedItem.ToString();
            //FileManager.getFileSize(item);
        }

        private void FileGridView_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            IntoNewDirectory(sender, false, e);
        }

        private void FileGridView_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.A)
            {
                FileManager.Cut("");
            }

            else if (e.KeyCode == Keys.Back)
            {
                B_Back_Click(sender, e);
            }

            else if (e.KeyCode == Keys.Enter)
            {
                IntoNewDirectory(sender, false, null);
            }
        }

        private void FileGridView_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && e.RowIndex >= 0)
            {
                FileGridView.CurrentCell = FileGridView.Rows[e.RowIndex].Cells[e.ColumnIndex];
            }
        }

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FileManager.Cut(FileGridView.CurrentCell.Value.ToString());
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FileManager.Copy(FileGridView.CurrentCell.Value.ToString());
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FileManager.Paste(FileGridView.CurrentCell.Value.ToString());
        }

        private void moveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FileManager.Move(FileGridView.CurrentCell.Value.ToString());
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            FileGridView.Size = new Size(this.Width - 50, this.Height - 150);
        }
    }
}
