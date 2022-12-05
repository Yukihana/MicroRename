using MicroRenameLogic.Types;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace MicroRenameLogic.DLG
{
    public class MRData : INotifyPropertyChanged
    {
        #region Interface : INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Tasks
        private readonly ObservableCollection<MRTask> tasks = new ObservableCollection<MRTask>();
        public ObservableCollection<MRTask> Tasks => tasks;
        #endregion

        #region Property : Count
        public int Count => tasks.Count;
        private void Tasks_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Count)));
        #endregion

        #region Property : AutoOverwrite
        private bool autoOverwrite = false;
        public bool AutoOverwrite
        {
            get => autoOverwrite;
            set
            {
                autoOverwrite = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(AutoOverwrite)));
            }
        }
        #endregion

        #region Property : MakeCopy
        private bool makeCopy = false;
        public bool MakeCopy
        {
            get => makeCopy;
            set
            {
                makeCopy = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(MakeCopy)));
            }
        }
        #endregion

        #region Ctor
        public MRData() => tasks.CollectionChanged += Tasks_CollectionChanged;
        #endregion
    }
}