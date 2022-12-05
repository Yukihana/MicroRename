using System.ComponentModel;

namespace MicroRenameLogic.Types
{
    public class MRTask : INotifyPropertyChanged
    {
        #region Interface : INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion


        #region Property : Filename
        private string filename;
        public string Filename
        {
            get => filename;
            set
            {
                filename = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Filename)));
            }
        }
        #endregion
        #region Property : Extension
        private string extension;
        public string Extension
        {
            get => extension;
            set
            {
                extension = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Extension)));
            }
        }
        #endregion
        #region Property : Directory
        private string directory;
        public string Directory
        {
            get => directory;
            set
            {
                directory = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Directory)));
            }
        }
        #endregion


        #region Ctor
        public MRTask(string filename, string directory, string extension)
        {
            Filename = filename;
            Directory = directory;
            Extension = extension;
        }
        #endregion
    }
}