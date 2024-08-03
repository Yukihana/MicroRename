using CommunityToolkit.Mvvm.ComponentModel;
using MicroRenameLogic.Types;
using System;
using System.Collections.ObjectModel;

namespace MicroRenameLogic.ViewContexts.EventLogViewContext;

public sealed partial class EventLogEntry : ObservableObject
{
    [ObservableProperty]
    private DateTime _timestamp = DateTime.Now;

    [ObservableProperty]
    private LogLevel _level = LogLevel.Information;

    [ObservableProperty]
    private string _message = string.Empty;

    [ObservableProperty]
    private ObservableCollection<LogAttachment> _attachments = [];

    [ObservableProperty]
    private string _exceptionDump = string.Empty;

    [ObservableProperty]
    private string _exceptionSource = string.Empty;

    [ObservableProperty]
    private string _exceptionMessage = string.Empty;
}