using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Input;
using SharpManager.Helpers;

namespace SharpManager
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private readonly Stack<string> _previousDirectoryStack = new Stack<string>();
        public string CurrentDirectory;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void MainWindow1_Loaded(object sender, RoutedEventArgs e)
        {
            WriteToGridView(FileManagement.GetDrives());
        }

        private void WriteToGridView(IEnumerable<FileProperties> fileList)
        {
            FileGrid.ItemsSource = fileList;
        }

        private void IntoNewDirectory(string item)
        {
            if (File.Exists(item))
            {
                FileManagement.OpenFile(item);
            }

            else
            {
                List<FileProperties> fileList = FileManagement.GetDirectoryContents(item);

                SetPreviousReferences();

                TB_CurrentDirectory.Text = item;
                CurrentDirectory = item;
                
                WriteToGridView(fileList);
            }
        }

        private void SetPreviousReferences()
        {
            _previousDirectoryStack.Push(CurrentDirectory);
        }

        private void IntoNewDirectoryFromDataGrid(string filename)
        {
            string item = filename;
            
            if (TB_CurrentDirectory.Text != "")
            {
                item = CurrentDirectory + "\\" + filename;
            }
            IntoNewDirectory(item);
        }

         private void RefreshGridView()
        {
            List<FileProperties> fileList = FileManagement.GetDirectoryContents(CurrentDirectory);
            WriteToGridView(fileList);
        }

        private void TB_CurrentDirectory_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                IntoNewDirectory(TB_CurrentDirectory.Text);
            }
        }

        private void FileGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var selectedRow = FileGrid.SelectedItem as FileProperties;
            if (selectedRow != null) IntoNewDirectoryFromDataGrid(selectedRow.Filename);
        }

        private void B_Back_Click(object sender, RoutedEventArgs e)
        {
            string item = _previousDirectoryStack.Pop();
            if (item == null)
            {
                WriteToGridView(FileManagement.GetDrives());
                TB_CurrentDirectory.Text = "";
                CurrentDirectory = "";
                return;
            }
            List<FileProperties> fileList = FileManagement.GetDirectoryContents(item);

            TB_CurrentDirectory.Text = item;
            CurrentDirectory = item;

            WriteToGridView(fileList);
        }

        private void B_Up_A_Level_Click(object sender, RoutedEventArgs e)
        {
            string newDirectory = Directory.GetParent(CurrentDirectory).FullName;
            IntoNewDirectory(newDirectory);
        }

        private void B_Refresh_Click(object sender, RoutedEventArgs e)
        {
            RefreshGridView();
        }
    }
}