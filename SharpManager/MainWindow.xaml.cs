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
        /// <summary>
        /// The directory that the program is currently displaying.
        /// </summary>
        private string _currentDirectory;

        /// <summary>
        /// Initializer for the MainWindow.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Once program has loaded, populate the grid view with the list of available drives on the system.
        /// </summary>
        /// <param name="sender">Object.</param>
        /// <param name="e">RoutedEventArgs</param>
        private void MainWindow1_Loaded(object sender, RoutedEventArgs e)
        {
            WriteToGridView(FileManagement.GetDrives());
        }

        /// <summary>
        /// Sets the ItemsSource for the file grid to be equal to the list of files and directories
        /// </summary>
        /// <param name="fileList">Contains the list to be put into the file grid.</param>
        private void WriteToGridView(IEnumerable<FileProperties> fileList)
        {
            FileGrid.ItemsSource = fileList;
        }

        /// <summary>
        /// If the item passed into the function is a file, that file is opened. If the item is a folder, the function will
        /// push the current directory string to a stack, before opening the new directory and putting
        /// the new file list in the file grid in its place.
        /// </summary>
        /// <param name="item">The path of the item that was clicked on in the file grid.</param>
        private void IntoNewDirectory(string item)
        {
            if (File.Exists(item))
            {
                FileManagement.OpenFile(item);
            }

            else
            {
                IEnumerable<FileProperties> fileList = FileManagement.GetDirectoryContents(item);

                SetPreviousReferences();

                TB_CurrentDirectory.Text = item;
                _currentDirectory = item;
                
                WriteToGridView(fileList);
            }
        }

        /// <summary>
        /// Pushes the current directory to a stack to be returned if the user needs to go back.
        /// </summary>
        private void SetPreviousReferences()
        {
            _previousDirectoryStack.Push(_currentDirectory);
        }

        /// <summary>
        /// Handles going into a new directory by selecting a cell on the file grid.
        /// </summary>
        /// <param name="filename">The file or folder selected.</param>
        private void IntoNewDirectoryFromDataGrid(string filename)
        {
            string item = filename;
            
            if (TB_CurrentDirectory.Text != "")
            {
                item = _currentDirectory + "\\" + filename;
            }
            IntoNewDirectory(item);
        }

        /// <summary>
        /// Reloads the file grid, reflecting changes in any items that have been updated.
        /// </summary>
        private void RefreshGridView()
        {
            IEnumerable<FileProperties> fileList = FileManagement.GetDirectoryContents(_currentDirectory);
            WriteToGridView(fileList);
        }

        /// <summary>
        /// Opens the currently selected item if the enter key has been pressed.
        /// </summary>
        /// <param name="sender">Object.</param>
        /// <param name="e">KeyEventArgs.</param>
        private void TB_CurrentDirectory_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                IntoNewDirectory(TB_CurrentDirectory.Text);
            }
        }

        /// <summary>
        /// Opens an item if it is double-clicked.
        /// </summary>
        /// <param name="sender">Object.</param>
        /// <param name="e">MouseButtonEventArgs.</param>
        private void FileGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var selectedRow = FileGrid.SelectedItem as FileProperties;
            if (selectedRow != null) IntoNewDirectoryFromDataGrid(selectedRow.Filename);
        }

        /// <summary>
        /// Grabs the previous directory from the stack and then opens the contents of that directory, if the "Back" button is clicked.
        /// </summary>
        /// <param name="sender">Object.</param>
        /// <param name="e">RoutedEventArgs.</param>
        private void B_Back_Click(object sender, RoutedEventArgs e)
        {
            string item = _previousDirectoryStack.Pop();
            if (item == null)
            {
                WriteToGridView(FileManagement.GetDrives());
                TB_CurrentDirectory.Text = "";
                _currentDirectory = "";
                return;
            }
            IEnumerable<FileProperties> fileList = FileManagement.GetDirectoryContents(item);

            TB_CurrentDirectory.Text = item;
            _currentDirectory = item;

            WriteToGridView(fileList);
        }

        /// <summary>
        /// Opens the parent directory of the currently open directory, if the "Up A Level" button is clicked.
        /// </summary>
        /// <param name="sender">Object.</param>
        /// <param name="e">RoutedEventArgs</param>
        private void B_Up_A_Level_Click(object sender, RoutedEventArgs e)
        {
            string newDirectory = Directory.GetParent(_currentDirectory).FullName;
            IntoNewDirectory(newDirectory);
        }

        /// <summary>
        /// Opens the current directory again, if the "Refresh" button is clicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void B_Refresh_Click(object sender, RoutedEventArgs e)
        {
            RefreshGridView();
        }
    }
}