using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows;
using SharpManager.Helpers.HelpersWindows;
using SharpManager.Properties;

namespace SharpManager.Helpers
{
    /// <summary>
    /// A class containing a variety of useful methods for file management.
    /// </summary>
    internal static class FileManagement
    { 
        /// <summary>
        /// Returns a list of the available drives on the system.
        /// </summary>
        /// <returns>List of available drives.</returns>
        public static IEnumerable<FileProperties> GetDrives()
        {
            return Directory.GetLogicalDrives().Select(drive => new FileProperties(drive, "", "", "", "")).ToList();
        }

        /// <summary>
        /// Converts a byte value to its relevant megabyte or gigabyte value, depending on the size of the file, rounded to two decimal places.
        /// </summary>
        /// <param name="bytes">Number of bytes to convert</param>
        /// <returns>Returns a constructed string with the calculated gigabyte/megabyte/kilobyte/byte value.</returns>
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

        /// <summary>
        /// Simple method to consolidate the Directory.CreateDirectory method into the program.
        /// </summary>
        /// <param name="targetPath">Contains the path that the new directory will be created at.</param>
        public static void CreateDirectory(string targetPath)
        {
            Directory.CreateDirectory(targetPath);
        }

        /// <summary>
        /// Searches a directory to get a list of all first-level subdirectories and files within the directory, ready to display in the grid view.
        /// </summary>
        /// <param name="dirToSearch">Contains the directory being scanned.</param>
        /// <returns>Returns the all files and folders found inside the directory being scanned.</returns>
        public static IEnumerable<FileProperties> GetDirectoryContents(string dirToSearch)
        {
            IEnumerable<FileProperties> files = GetFileList(dirToSearch);
            IEnumerable<FileProperties> subDirectories = GetDirectoryList(dirToSearch);
            var contentsOfDirectory = new List<FileProperties>();

            contentsOfDirectory.AddRange(subDirectories);
            contentsOfDirectory.AddRange(files);

            return contentsOfDirectory;
        }

        /// <summary>
        /// Gets the list of files within a directory, as well as grabbing a variety of properties about the files for display in the grid view.
        /// </summary>
        /// <param name="dirToSearch">Contains the directory being scanned.</param>
        /// <returns>Returns a list of all files found in the directory being scanned.</returns>
        private static IEnumerable<FileProperties> GetFileList(string dirToSearch)
        {
            return (from file in Directory.GetFiles(dirToSearch)
                let fileinfo = new FileInfo(file)
                select
                    new FileProperties(Path.GetFileName(file), GetFileType(fileinfo.FullName),
                        GetByteConversion(fileinfo.Length), fileinfo.CreationTime.ToString(),
                        fileinfo.LastWriteTime.ToString())).ToList();
        }

        /// <summary>
        /// Gets the list of first-level subdirectories within a directory, as well as grabbing a variety of properties about the subdirectories for display in the grid view.
        /// </summary>
        /// <param name="dirToSearch">Contains the directory being scanned.</param>
        /// <returns>Returns a list of all first-level subdirectories inside the directory being scanned.</returns>
        private static IEnumerable<FileProperties> GetDirectoryList(string dirToSearch)
        {
            return (from subDirectory in Directory.GetDirectories(dirToSearch)
                let directoryinfo = new DirectoryInfo(subDirectory)
                select
                    new FileProperties(Path.GetFileName(subDirectory), "Folder", "",
                        directoryinfo.CreationTime.ToString(), directoryinfo.LastWriteTime.ToString())).ToList();
        }

        /// <summary>
        /// Compares the extension of a file to a dictionary of file type formats to provide a more descriptive idea of what a given type of file is.
        /// For example, a .png file would be given the description "Image file".
        /// If the file extension cannot be found in the dictionary, the extension itself is returned for use in the grid view.
        /// </summary>
        /// <param name="filepath">The path to the file to be checked.</param>
        /// <returns>The file type description associated with the extension.</returns>
        private static string GetFileType(string filepath)
        {
            var fileTypeDict = new Dictionary<string, string>();

            string files = Resources.files;
            fileTypeDict = files.Split(new[] { Environment.NewLine }, StringSplitOptions.None)
             .ToDictionary(x => x.Split(',')[0], x => x.Split(',')[1]);

            string filetype = null;
            string extension = Path.GetExtension(filepath);

            if (extension != null && fileTypeDict.ContainsKey(extension))
            {
                filetype = fileTypeDict[extension];
            }
            else
            {
                filetype = extension;
            }

            return filetype;
        }

        /// <summary>
        /// Takes a file to be moved into another location.
        /// </summary>
        /// <param name="filename">The path to the file that will be moved.</param>
        /// <param name="destination">The path that the file will be moved to.</param>
        public static void MoveFile(string filename, string destination)
        {
            Directory.Move(filename, destination);
        }

        /// <summary>
        /// Opens a file in the default program associated with it. If the file is an image file that is compatible with the Image Previewer, open it in the Image Previewer.
        /// </summary>
        /// <param name="filename">The path of the file to be opened.</param>
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

        /// <summary>
        /// Simple method to consolidate the File.Delete method into the program.
        /// </summary>
        /// <param name="filename">The path of the file to be deleted.</param>
        public static void DeleteFile(string filename)
        {
            File.Delete(filename);
        }
    }
}