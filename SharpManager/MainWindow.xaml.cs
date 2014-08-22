using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SharpManager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        string currentDirectory;
        Stack<System.Collections.IEnumerable> back_stack = new Stack<System.Collections.IEnumerable>();
        Stack<string> previousDirectory_stack = new Stack<string>();

        private void MainWindow1_Loaded(object sender, RoutedEventArgs e)
        {
            WriteToGridView(Helpers.FileManagement.getDrives());
        }

        private void WriteToGridView(List<Helpers.FileProperties> fileList)
        {
            FileGrid.ItemsSource = fileList;
        }

        private void IntoNewDirectory(string item)
        {
            if (File.Exists(item))
            {
                Helpers.FileManagement.openFile(item);
            }

            else
            {
                List<Helpers.FileProperties> fileList = new List<Helpers.FileProperties>();
                fileList = Helpers.FileManagement.getDirectoryContents(item);
                
                TB_CurrentDirectory.Text = item;
                currentDirectory = item;

                SetPreviousReferences();
                WriteToGridView(fileList);
            }
        }

        private void SetPreviousReferences()
        {
            back_stack.Push(FileGrid.ItemsSource);
            previousDirectory_stack.Push(currentDirectory);
        }

        private void IntoNewDirectoryFromDataGrid(string filename)
        {
            string item = filename;
            if (TB_CurrentDirectory.Text != "")
            {
                item = currentDirectory + "\\" + filename;
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
            var selectedRow = FileGrid.SelectedItem as Helpers.FileProperties;
            IntoNewDirectoryFromDataGrid(selectedRow.Filename);
        }

        private void B_Back_Click(object sender, RoutedEventArgs e)
        {
            FileGrid.ItemsSource = back_stack.Pop();
            TB_CurrentDirectory.Text = previousDirectory_stack.Pop();
        }

        private void B_Up_A_Level_Click(object sender, RoutedEventArgs e)
        {
            string newDirectory = Directory.GetParent(currentDirectory).FullName;
            IntoNewDirectory(newDirectory);
        }
    }
}
