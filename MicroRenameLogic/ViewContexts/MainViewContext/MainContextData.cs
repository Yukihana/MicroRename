using CommunityToolkit.Mvvm.ComponentModel;
using MicroRenameLogic.Types;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace MicroRenameLogic.ViewContexts.MainViewContext;

public partial class MainContextData : ObservableObject
{
    // Tasks

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(Count))]
    private ObservableCollection<MRTask> _tasks = new();

    public int Count => Tasks.Count;

    // Selected

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(SelectedCount))]
    private List<MRTask> _selectedItems = new();

    public int SelectedCount => SelectedItems.Count;

    // Misc

    [ObservableProperty]
    private bool _autoOverwrite = false;

    [ObservableProperty]
    private bool _makeCopy = false;

    [ObservableProperty]
    public bool _autoProgress = false;

    [ObservableProperty]
    public bool _showCompletionMessage = false;
}