using MicroRenameWpf.Win32Interop;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using FolderBrowserDialog = System.Windows.Forms.FolderBrowserDialog;
using FormDialogResult = System.Windows.Forms.DialogResult;
using IFileSystemHandler = MicroRenameLogic.FrontendInterfaces.IFileSystemHandler;

namespace MicroRenameWpf.DialogHandlers
{
    public class FileSystemHandler : IFileSystemHandler
    {
        #region File check
        public bool FileExists(string filename) => File.Exists(filename);
        #endregion


        #region File operations
        public bool MoveFile(string oldPath, string newPath, bool overwrite)
        {
            bool result;
            try
            {
                if(File.Exists(newPath) && overwrite)
                {
                    File.Delete(newPath);
                }
                File.Move(oldPath, newPath);
                result = true;
            }
            catch (Exception ex)
            {
                result = false;
                MessageBox.Show(ex.Message, ex.Source, MessageBoxButton.OK, MessageBoxImage.Error);
            }

            return result;
        }
        #endregion


        #region Get operations
        public string GetFile(string dialogMessage, string startingPath = null)
            => throw new NotImplementedException();
        public string GetFolder(string dialogMessage, string startingPath = null)
            => throw new NotImplementedException();
        public IEnumerable<string> GetFiles(string dialogMessage, string startingPath = null)
        {
            List<string> filenames = new List<string>();
            OpenFileDialog o = new OpenFileDialog
            {
                Title = dialogMessage,
                InitialDirectory = startingPath,
                Filter = "AllFiles (*.*)|*.*",
                CheckFileExists = true,
                Multiselect = true
            };

            if (o.ShowDialog() == true)
            {
                filenames.AddRange(o.FileNames);
            }

            return filenames;
        }
        public IEnumerable<string> GetFilesFromFolder(string dialogMessage, string startingPath = null)
        {
            List<string> filenames = new List<string>();

            FolderBrowserDialog o = new FolderBrowserDialog()
            {
                Description = dialogMessage,
                ShowNewFolderButton = true,
                SelectedPath = startingPath,
            };

            if (o.ShowDialog() == FormDialogResult.OK)
            {
                filenames.AddRange(Directory.GetFiles(o.SelectedPath));
            }

            filenames.Sort(new NaturalStringComparer());
            return filenames;
        }
        #endregion


        #region Path parsing operations
        public string GetDirectory(string path)
            => Path.GetDirectoryName(path);
        public string GetFilename(string path)
            => Path.GetFileName(path);
        public string GetExtension(string path)
            => Path.GetExtension(path);
        #endregion


        #region Path generating operations
        public string CombinePath(string directory, string filename)
            => Path.Combine(directory, filename);
        #endregion
    }
}