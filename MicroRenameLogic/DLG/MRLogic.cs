using MicroRenameLogic.Commands;
using MicroRenameLogic.FrontendInterfaces;
using MicroRenameLogic.Types;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows;

namespace MicroRenameLogic.DLG
{
    public class MRLogic : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        // Dialog Handlers
        public IMessageBoxHandler MessageDialog = null;
        public IFileSystemHandler FileSystemHandler = null;
        public IDialogHandler SettingsDialog = null;
        public IDialogHandler AboutDialog = null;


        // Model Data
        #region DataModel
        private readonly MRData dataModel = new MRData();
        public MRData DataModel => dataModel;
        #endregion

        #region NamingData
        private readonly NamingData namingData = new NamingData();
        public NamingData NamingData => namingData;
        #endregion

        #region LastLocation
        public string CurrentLocation => SelectedItem?.Directory ?? string.Empty;
        #endregion

        #region SelectedItem
        private MRTask selectedItem = null;
        public MRTask SelectedItem
        {
            get => selectedItem;
            set
            {
                selectedItem = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SelectedItem)));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CurrentLocation)));
            }
        }
        #endregion

        #region SelectedItems
        public readonly List<MRTask> SelectedItems = new List<MRTask>();
        #endregion


        // Other Data
        #region Preview
        private string preview = string.Empty;
        public string Preview
        {
            get => preview;
            set
            {
                preview = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Preview)));
            }
        }
        private void NamingData_PropertyChanged(object sender, PropertyChangedEventArgs e)
            => Preview = GetName(SelectedItem);
        #endregion


        // Commands : Main
        #region Command : Add Files
        private readonly ActionCommand addFilesCommand;
        public ActionCommand AddFilesCommand => addFilesCommand;
        private void AddFiles()
        {
            if (FileSystemHandler != null)
            {
                foreach (string path in FileSystemHandler.GetFiles("Select files to rename...", Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)))
                {
                    AddTask(path);
                }
            }
        }
        #endregion

        #region Command : Add Folders
        private readonly ActionCommand addFolderCommand;
        public ActionCommand AddFolderCommand => addFolderCommand;
        private void AddFolder()
        {
            if (FileSystemHandler != null)
            {
                foreach (string path in FileSystemHandler.GetFilesFromFolder("Select a folder to rename files in...", Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)))
                {
                    AddTask(path);
                }
            }
        }
        #endregion

        #region Command : Remove Files
        private readonly ActionCommand removeFilesCommand;
        public ActionCommand RemoveFilesCommand => removeFilesCommand;
        private void RemoveFiles()
        {
            foreach (MRTask task in SelectedItems)
            {
                DataModel.Tasks.Remove(task);
            }
        }
        #endregion

        #region Command : Clear Missing
        private readonly ActionCommand clearMissingCommand;
        public ActionCommand ClearMissingCommand => clearMissingCommand;
        private void ClearMissing()
        {
            if (MessageDialog != null)
            {
                if (MessageDialog.Query("Clear all missing files?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Asterisk) == MessageBoxResult.Yes)
                {
                    List<MRTask> removeable = new List<MRTask>();

                    // Check for missing
                    foreach (MRTask task in DataModel.Tasks)
                    {
                        if (!FileSystemHandler.FileExists(FileSystemHandler.CombinePath(task.Directory, task.Filename)))
                        {
                            removeable.Add(task);
                        }
                    }

                    // Purge missing
                    foreach (MRTask task in removeable)
                    {
                        DataModel.Tasks.Remove(task);
                    }

                    // Finish
                    removeable.Clear();
                }
            }
        }
        #endregion

        #region Command : Clear All
        private readonly ActionCommand clearAllCommand;
        public ActionCommand ClearAllCommand => clearAllCommand;
        private void ClearAll()
        {
            if (MessageDialog != null)
            {
                if (MessageDialog.Query("Clear all tasks?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Asterisk) == MessageBoxResult.Yes)
                {
                    DataModel.Tasks.Clear();
                }
            }
        }
        #endregion

        #region Command : Rename
        private readonly ActionCommand renameCommand;
        public ActionCommand RenameCommand => renameCommand;
        private void Rename()
        {
            // Iterate (Create a copy array first)
            var items = SelectedItems.ToArray();
            for (int i = 0; i < items.Length; i++)
            {
                // Attempt rename file
                if(RenameFile(items[i]) == false)
                {
                    MessageDialog.Query($"The rename operation stopped at {items[i].Filename}", "Aborted", MessageBoxButton.OK, MessageBoxImage.Warning);
                    SelectedItem = items[i];
                    break;
                }

                // Attempt increment
                if(!MoveNext())
                {
                    break;
                }
            }
        }
        private bool RenameFile(MRTask file)
        {
            byte status = 0;

            // Prepare new name
            string newname = GetName(file);

            // Attempt to move and retry till success or user aborts
            do
            {
                if (FileSystemHandler.MoveFile(
                    FileSystemHandler.CombinePath(file.Directory, file.Filename),
                    FileSystemHandler.CombinePath(file.Directory, newname),
                    DataModel.AutoOverwrite
                    ) == true)
                {
                    file.Filename = newname;
                    status = 1;
                }
                else if (MessageDialog.Query(
                    $"Unable to rename '{file.Filename}' to '{newname}'\nWould you like to try again?",
                    "Rename failed", MessageBoxButton.YesNo, MessageBoxImage.Asterisk)
                    == MessageBoxResult.No)
                {
                    status = 2;
                }
            }
            while (status == 0);

            // loop will never break on zero. Success = 1, Fail = 2, true on success.
            return status == 1;
        }
        #endregion

        #region Command : Settings
        private readonly ActionCommand settingsCommand;
        public ActionCommand SettingsCommand => settingsCommand;
        private void OpenSettings()
        {
            if (SettingsDialog != null)
            {
                SettingsDialog.ShowDialog();
            }
        }
        #endregion

        #region Command : About
        private readonly ActionCommand aboutCommand;
        public ActionCommand AboutCommand => aboutCommand;
        private void OpenAbout()
        {
            if (AboutDialog != null)
            {
                AboutDialog.ShowDialog();
            }
        }
        #endregion

        // Commands : Naming
        #region Command : Reset
        private readonly ActionCommand resetCommand;
        public ActionCommand ResetCommand => resetCommand;
        private void ResetNaming()
        {
            NamingData.Prefix = "Prefix_";
            NamingData.Suffix = NamingData.KeepExtension ? "_suffix" : "_suffix.ext";
            NamingData.Increment = 1;
            NamingData.Index = 1;
            NamingData.Digits = 4;
        }
        #endregion

        #region Command : Next
        private readonly ActionCommand nextCommand;
        public ActionCommand NextCommand => nextCommand;
        private bool MoveNext()
        {
            if (NamingData.Index + NamingData.Increment > NamingData.MaxIndex)
            {
                if (MessageDialog?.Query("With the current number of digits and increment count, the maximum value has been reached. Continue further by increase the number of digits by one?", "Maximum reached", MessageBoxButton.OKCancel, MessageBoxImage.Warning) != MessageBoxResult.OK)
                {
                    return false;
                }
                NamingData.Digits++;
            }

            // Finally
            NamingData.Index += NamingData.Increment;
            return true;
        }
        #endregion

        // Other UI initiated actions
        #region Dropped Files
        public void AddFromDrop(string[] files)
        {
            foreach (string file in files) AddTask(file);
        }
        #endregion

        public MRLogic()
        {
            NamingData.PropertyChanged += NamingData_PropertyChanged;

            addFilesCommand = new ActionCommand(() => AddFiles(), (object parameter) => true);
            addFolderCommand = new ActionCommand(() => AddFolder(), (object parameter) => true);
            renameCommand = new ActionCommand(() => Rename(), (object parameter) => true);
            removeFilesCommand = new ActionCommand(() => RemoveFiles(), (object parameter) => true);
            clearMissingCommand = new ActionCommand(() => ClearMissing(), (object parameter) => true);
            clearAllCommand = new ActionCommand(() => ClearAll(), (object parameter) => true);
            settingsCommand = new ActionCommand(() => OpenSettings(), (object parameter) => true);
            aboutCommand = new ActionCommand(() => OpenAbout(), (object parameter) => true);

            resetCommand = new ActionCommand(() => ResetNaming(), (object parameter) => true);
            nextCommand = new ActionCommand(() => MoveNext(), (object parameter) => true);

            // Force a preview update
            NamingData_PropertyChanged(this, new PropertyChangedEventArgs(nameof(Preview)));
        }

        


        // Methods
        private void AddTask(string path)
        {
            DataModel.Tasks.Add(new MRTask(
                FileSystemHandler.GetFilename(path),
                FileSystemHandler.GetDirectory(path),
                FileSystemHandler.GetExtension(path)
                ));
        }
        private string GetName(MRTask task = null)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(NamingData.Prefix);
            sb.Append(NamingData.Index.ToString().PadLeft(NamingData.Digits, '0'));
            sb.Append(NamingData.Suffix);
            if (NamingData.KeepExtension)
            {
                sb.Append(task == null ? ".*" : task.Extension);
            }
            return sb.ToString();
        }
        
    }
}