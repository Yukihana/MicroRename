using CommunityToolkit.Mvvm.ComponentModel;
using MicroRenameLogic.Types;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace MicroRenameLogic.ViewContexts.EventLogViewContext;

public sealed partial class EventLogContextData : ObservableObject
{
    [ObservableProperty]
    private ObservableCollection<EventLogEntry> _entries = [];

    partial void OnEntriesChanged(ObservableCollection<EventLogEntry>? oldValue, ObservableCollection<EventLogEntry> newValue)
    {
        if (oldValue is not null)
        {
            try
            { oldValue.CollectionChanged -= EntriesUpdated; }
            catch { }
        }

        newValue.CollectionChanged += EntriesUpdated;
    }

    // May need to initialize this connection in the ctor
    private void EntriesUpdated(object sender, NotifyCollectionChangedEventArgs e)
    {
        TotalCount = Entries.Count;

        uint warning = 0,
            error = 0,
            special = 0;

        foreach (var entry in Entries)
        {
            switch (entry.Level)
            {
                case LogLevel.Warning:
                    warning++; break;
                case LogLevel.Error:
                    error++; break;
                case LogLevel.Special:
                    special++; break;
                default:
                    break;
            }
        }

        WarningCount = warning;
        ErrorCount = error;
        SpecialCount = special;
    }

    [ObservableProperty]
    private int _totalCount = 0;

    [ObservableProperty]
    private uint _warningCount = 0;

    [ObservableProperty]
    private uint _errorCount = 0;

    [ObservableProperty]
    private uint _specialCount = 0;
}