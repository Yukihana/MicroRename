using MicroRenameLogic.ServiceBase;
using MicroRenameLogic.ViewContexts.LogViewContext;
using MicroRenameWpf.Windows;
using System;

namespace MicroRenameWpf.Services;

internal partial class LoggingService : ILoggingService
{
    // Logic

    private readonly LogContextLogic _logic = new();
    public LogContextLogic Logic => _logic;

    // Window

    private LogWindow? _logWindow = null;

    public void ShowLog()
    {
        _logWindow ??= new()
        {
            DataContext = _logic,
            LogClosedCallback = OnLogClosed
        };
        _logWindow.Show();
        _logWindow.Activate();
    }

    private void OnLogClosed(EventArgs e)
        => _logWindow = null;

    public void Dispose()
    {
        if (_logWindow == null)
            return;

        try
        { _logWindow.Close(); }
        catch { }
    }
}