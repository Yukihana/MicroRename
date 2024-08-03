using CommunityToolkit.Mvvm.ComponentModel;
using MicroRenameLogic.Types;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace MicroRenameLogic.ViewContexts.RenameLogViewContext;

public sealed partial class RenameLogContextData : ObservableObject
{
    [ObservableProperty]
    private ObservableCollection<RenameLogEntry> _entries = [];

    [ObservableProperty]
    private RenameLogEntry? _selectedItem = null;

    partial void OnEntriesChanged(ObservableCollection<RenameLogEntry>? oldValue, ObservableCollection<RenameLogEntry> newValue)
    {
        if (oldValue is not null)
        {
            try
            { oldValue.CollectionChanged -= EntriesUpdated; }
            catch { }
        }

        newValue.CollectionChanged += EntriesUpdated;
    }

    private void EntriesUpdated(object sender, NotifyCollectionChangedEventArgs e)
    {
        TotalCount = Entries.Count;

        uint completed = 0,
            revert = 0,
            warning = 0,
            error = 0;

        foreach (var entry in Entries)
        {
            switch (entry.Status)
            {
                case RenameStatus.Completed: //On reversal, the original entry becomes expired.
                    completed++; break;
                case RenameStatus.Reversed:
                    revert++; break;
                case RenameStatus.Unknown:
                    warning++; break;
                case RenameStatus.Faulted:
                    error++; break;
                default:
                    break;
            }
        }

        CompletedCount = completed;
        RevertCount = revert;
        WarningCount = warning;
        ErrorCount = error;
    }

    // Supplementary Properties

    [ObservableProperty]
    private int _totalCount = 0;

    [ObservableProperty]
    private uint _completedCount = 0;

    [ObservableProperty]
    private uint _revertCount = 0;

    [ObservableProperty]
    private uint _warningCount = 0;

    [ObservableProperty]
    private uint _errorCount = 0;
}