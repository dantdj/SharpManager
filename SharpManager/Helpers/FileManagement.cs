using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows;
using SharpManager.Helpers.HelpersWindows;

namespace SharpManager.Helpers
{
    internal class FileManagement
    { 
        public static List<FileProperties> GetDrives()
        {
            // Return a list of available drives

            return Directory.GetLogicalDrives().Select(drive => new FileProperties(drive, "", "", "", "")).ToList();
        }

        private static string GetByteConversion(float bytes)
        {
            const float bytesInKilobyte = 1024;
            const float bytesInMegabyte = 1048576;
            const float bytesInGigabyte = 1073741824;

            if (bytes > bytesInGigabyte)
            {
                return Math.Round((bytes/bytesInGigabyte), 2) + " GB";
            }

            if (bytes > bytesInMegabyte)
            {
                return Math.Round((bytes/bytesInMegabyte), 2) + " MB";
            }

            if (bytes > bytesInKilobyte)
            {
                return Math.Round((bytes/bytesInKilobyte), 2) + " KB";
            }

            return bytes + " B";
        }

        public static void CreateDirectory(string targetPath)
        {
            Directory.CreateDirectory(targetPath);
        }

        public static List<FileProperties> GetDirectoryContents(string dirToSearch)
        {
            IEnumerable<FileProperties> files = GetFileList(dirToSearch);
            IEnumerable<FileProperties> subDirectories = GetDirectoryList(dirToSearch);
            var contentsOfDirectory = new List<FileProperties>();

            contentsOfDirectory.AddRange(subDirectories);
            contentsOfDirectory.AddRange(files);

            return contentsOfDirectory;
        }

        private static IEnumerable<FileProperties> GetFileList(string dirToSearch)
        {
            return (from file in Directory.GetFiles(dirToSearch)
                let fileinfo = new FileInfo(file)
                select
                    new FileProperties(Path.GetFileName(file), GetFileType(fileinfo.FullName),
                        GetByteConversion(fileinfo.Length), fileinfo.CreationTime.ToString(),
                        fileinfo.LastWriteTime.ToString())).ToList();
        }

        private static IEnumerable<FileProperties> GetDirectoryList(string dirToSearch)
        {
            return (from subDirectory in Directory.GetDirectories(dirToSearch)
                let directoryinfo = new DirectoryInfo(subDirectory)
                select
                    new FileProperties(Path.GetFileName(subDirectory), "Folder", "",
                        directoryinfo.CreationTime.ToString(), directoryinfo.LastWriteTime.ToString())).ToList();
        }

        private static string GetFileType(string filepath)
        {
            var fileTypeDict = new Dictionary<string, string>
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
            if (extension != null && fileTypeDict.ContainsKey(extension))
            {
                filetype = fileTypeDict[extension];
            }
            return filetype;
        }

        public static void MoveFile(string filename, string destination)
        {
            Directory.Move(filename, destination);
        }

        public static void OpenFile(string filename)
        {
            var imageExtensions = new List<string> {".jpg", ".png", ".bmp", ".tiff", ".tif", ".gif", ".ico"};

            try
            {
                if (imageExtensions.Contains(Path.GetExtension(filename)))
                {
                    var imageWindow = new ImageWindow {ImagePath = filename};
                    imageWindow.Show();
                    return;
                }

                Process.Start(filename);
            }

            catch
            {
                MessageBox.Show("Could not open the file.");
            }
        }
    }
}