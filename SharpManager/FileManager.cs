using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SharpManager
{
    class FileManager
    {
        static StringCollection log = new StringCollection();

        public static List<string> getDrives()
        {
            // Return a list of available drives
            List<string> drives = System.Environment.GetLogicalDrives().ToList<string>();
            return drives;
        }

        public static List<object> getDirectoryContents(string dirToSearch)
        {
            // Scan for files and subdirectories with the directory that is being searched
            List<FileProperties> files = getFileList(dirToSearch);
            List<FileProperties> subDirectories = getDirectoryList(dirToSearch);

            List<object> filesAndSubDirectories = new List<object>();
            filesAndSubDirectories.AddRange(subDirectories);
            filesAndSubDirectories.AddRange(files);

            return filesAndSubDirectories;
        }

        public static List<FileProperties> getFileList(string dirToSearch)
        {
            // Scan for all files directly in this folder
            List<string> files = new List<string>();
            List<FileProperties> properties = new List<FileProperties>();

            try
            {
                files = Directory.GetFiles(dirToSearch).ToList<string>();
                foreach (string item in files)
                {
                    System.IO.FileInfo fileinfo = new System.IO.FileInfo(item);
                    properties.Add(new FileProperties(item, getFileType(fileinfo.FullName), getByteConversion(fileinfo.Length), fileinfo.CreationTime.ToString(), fileinfo.LastWriteTime.ToString()));
                }
            }

            // Thrown if one of the files requires greater permissions than can be provided
            catch (UnauthorizedAccessException e)
            {
                log.Add(e.Message);
                MessageBox.Show("File or folder requires greater permissions than this program has.");
            }

            // Thrown if the directory dirToSearch is not found/not valid
            catch (System.IO.DirectoryNotFoundException e)
            {
                log.Add(e.Message);
                MessageBox.Show("Directory not found!");
            }

            // Thrown if directory cannot be accessed
            catch (System.IO.IOException e)
            {
                log.Add(e.Message);
                MessageBox.Show("Folder cannot be accessed.");
            }

            return properties;
        }

        public static List<FileProperties> getDirectoryList(string dirToSearch)
        {
            // Scan for all subdirectories directly within the folder
            List<string> subDirectories = new List<string>();
            List<FileProperties> properties = new List<FileProperties>();

            try
            {
                subDirectories = Directory.GetDirectories(dirToSearch).ToList<string>();
                foreach (string item in subDirectories)
                {
                    System.IO.DirectoryInfo directoryinfo = new System.IO.DirectoryInfo(item);
                    properties.Add(new FileProperties(item, "Folder", "", directoryinfo.CreationTime.ToString(), directoryinfo.LastWriteTime.ToString()));

                }
            }

            // Thrown if permissions are not high enough to view the requested folder
            catch (UnauthorizedAccessException e)
            {
                log.Add(e.Message);
                MessageBox.Show("Insufficient authorization to access this folder.");
                return null;
            }

            // Thrown if folder cannot be accessed
            catch (IOException e)
            {
                log.Add(e.Message);
            }

            return properties;
        }

        public static void openFile(string filename)
        {
            try
            {
                System.Diagnostics.Process.Start(filename);
            }

            catch
            {
                MessageBox.Show("Could not open file.");
            }
        }

        public static string getFileType(string filepath)
        {
            Dictionary<string, string> file_type_dict = new Dictionary<string, string>
            {
                {".txt", "Text Document"},
                {".exe", "Application"},
                {".py", "Python File"},
                {".jpg", "Image File"},
                {".png", "Image File"},
                {".dll", "Application Extension"},
                {".js", "Javascript File"},
                {".mp3", "Music File"},
                {".mp4", "Video File"},
                {".wmv", "Video File"},
                {".ini", "Configuration File"},
                {".sys", "System File"},
                {".log", "Log File"},
                {".rar", "RAR Archive"},
                {".zip", "Compressed folder"},
                {".bmp", "Image File"},
                {".MSI", "MSI File"},
                {".cab", "cab File"},
                {".gitignore", "Git Ignore File"},
                {".gitattributes", "Git Attributes File"},
                {".csproj", "C# Project File"},
                {"", "File"},
                {".md", "Markdown File"},
                {".sln", "Visual Studio Solution File"}
            };
            string filetype = "";
            string extension = Path.GetExtension(filepath);
            if (file_type_dict.ContainsKey(extension))
            {
                filetype = file_type_dict[extension];
            }
            return filetype;
        }

        private static string getByteConversion(float bytes)
        {
            float bytesInKilobyte = 1024;
            float bytesInMegabyte = 1048576;
            float bytesInGigabyte = 1073741824;

            if (bytes > bytesInGigabyte)
            {
                return Math.Round((bytes / bytesInGigabyte), 2).ToString() + " GB";
            }

            else if (bytes > bytesInMegabyte)
            {
                return Math.Round((bytes / bytesInMegabyte), 2).ToString() + " MB";
            }

            else if (bytes > bytesInKilobyte)
            {
                return Math.Round((bytes / bytesInKilobyte), 2).ToString() + " KB";
            }

            else
            {
                return bytes + " B";
            }
        }

        public static void Cut(string filename)
        {
            // Not yet implemented
        }

        public static void Copy(string filename)
        {
            StringCollection file_list = new System.Collections.Specialized.StringCollection();
            file_list.Add(filename);
            Clipboard.SetFileDropList(file_list);
        }

        public static void Paste(string destinationPath)
        {
            // Not yet implemented
        }

        public static void Move(string filename)
        {
            // Not yet implemented
        }

        public static void createDirectory(string targetPath)
        {
            System.IO.Directory.CreateDirectory(targetPath);
        }
    }
}
