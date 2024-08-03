using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MicroRenameLogic.ServiceBase;
using System;
using System.Collections.Generic;

namespace MicroRenameLogic.ViewContexts.EventLogViewContext;

public sealed partial class EventLogContextLogic : ObservableObject
{
    // Data

    [ObservableProperty]
    private EventLogContextData _contextData = new();

    // Components

    private IModalService QueryService => ServicesRelay.QueryModalService;

    // Delegates

    public Func<IEnumerable<EventLogEntry>>? RequestSelection { get; set; } = null;

    // Commands

    public RelayCommand ClearCommand { get; }
    public RelayCommand CopyCommand { get; }
    public RelayCommand SaveCommand { get; }

    // Initialization

    public EventLogContextLogic()
    {
        ClearCommand = new(Clear);
        CopyCommand = new(Copy);
        SaveCommand = new(Save);
    }
}