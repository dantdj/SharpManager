using System.Collections;
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
        private readonly Stack<IEnumerable> _backStack = new Stack<IEnumerable>();
        private readonly Stack<string> _previousDirectoryStack = new Stack<string>();
        private string _currentDirectory;

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

                TB_CurrentDirectory.Text = item;
                _currentDirectory = item;

                SetPreviousReferences();
                WriteToGridView(fileList);
            }
        }

        private void SetPreviousReferences()
        {
            _backStack.Push(FileGrid.ItemsSource);
            _previousDirectoryStack.Push(_currentDirectory);
        }

        private void IntoNewDirectoryFromDataGrid(string filename)
        {
            string item = filename;
            if (TB_CurrentDirectory.Text != "")
            {
                item = _currentDirectory + "\\" + filename;
            }
            IntoNewDirectory(item);
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
            FileGrid.ItemsSource = _backStack.Pop();
            TB_CurrentDirectory.Text = _previousDirectoryStack.Pop();
        }

        private void B_Up_A_Level_Click(object sender, RoutedEventArgs e)
        {
            string newDirectory = Directory.GetParent(_currentDirectory).FullName;
            IntoNewDirectory(newDirectory);
        }
    }
}