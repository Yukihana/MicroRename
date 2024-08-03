using CommunityToolkit.Mvvm.ComponentModel;
using MicroRenameLogic.ServiceBase;
using MicroRenameLogic.ViewContexts.EventLogViewContext;
using MicroRenameLogic.ViewContexts.RenameLogViewContext;
using System;

namespace MicroRenameLogic.ViewContexts.LogViewContext;

// This class will handle log intake, pass it along and log window startup.
// But user interactions will be handled by each tab's view contexts. (Subloggers)
public sealed partial class LogContextLogic : ObservableObject
{
    // Components

    private readonly IModalService _modalService = ServicesRelay.ModalService;

    // Loggers

    [ObservableProperty]
    private RenameLogContextLogic _renameLogContext = new();

    [ObservableProperty]
    private EventLogContextLogic _eventLogContext = new();

    // IoC

    public IModalService ModalService => _modalService;

    // Lifecycle

    public void LogClosed()
    {
        throw new NotImplementedException();
    }
}