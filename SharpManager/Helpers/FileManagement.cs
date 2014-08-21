using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpManager.Helpers
{
    class FileManagement
    {
        public static List<FileProperties> getDrives()
        {
            // Return a list of available drives
            List<FileProperties> drives = new List<FileProperties>();

            foreach(string drive in Directory.GetLogicalDrives())
            {
                drives.Add(new FileProperties(drive, "", "", "", ""));
            }
            return drives;
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

        public static void createDirectory(string targetPath)
        {
            System.IO.Directory.CreateDirectory(targetPath);
        }
    
        public static List<FileProperties> getDirectoryContents(string dirToSearch)
        {
            List<FileProperties> files = getFileList(dirToSearch);
            List<FileProperties> subDirectories = getDirectoryList(dirToSearch);

            List<FileProperties> contentsOfDirectory = new List<FileProperties>();
            contentsOfDirectory.AddRange(subDirectories);
            contentsOfDirectory.AddRange(files);

            return contentsOfDirectory;
        }
    
        private static List<FileProperties> getFileList(string dirToSearch)
        {
            List<FileProperties> files = new List<FileProperties>();

            foreach (string file in Directory.GetFiles(dirToSearch))
            {
                FileInfo fileinfo = new FileInfo(file);
                files.Add(new FileProperties(Path.GetFileName(file), getFileType(fileinfo.FullName), getByteConversion(fileinfo.Length), fileinfo.CreationTime.ToString(), fileinfo.LastWriteTime.ToString()));
            }

            return files;
        }

        private static List<FileProperties> getDirectoryList(string dirToSearch)
        {
            List<FileProperties> subDirectories = new List<FileProperties>();

            foreach(string subDirectory in Directory.GetDirectories(dirToSearch))
            {
                DirectoryInfo directoryinfo = new DirectoryInfo(subDirectory);
                subDirectories.Add(new FileProperties(Path.GetFileName(subDirectory), "Folder", "", directoryinfo.CreationTime.ToString(), directoryinfo.LastWriteTime.ToString()));
            }

            return subDirectories;
        }

        private static string getFileType(string filepath)
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

        public static void openFile(string filename)
        {
            try
            {
                System.Diagnostics.Process.Start(filename);
            }

            catch
            {
                System.Windows.MessageBox.Show("Could not open the file.");
            }
        }
    }
}
