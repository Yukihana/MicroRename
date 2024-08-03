using MicroRenameLogic.ViewContexts.LogViewContext;
using MicroRenameWpf.Services;
using System;
using System.Windows;

namespace MicroRenameWpf.Windows;

/// <summary>
/// Interaction logic for LogWindow.xaml
/// </summary>
public partial class LogWindow : Window
{
    public LogWindow()
    {
        InitializeComponent();
    }

    public Action<EventArgs>? LogClosedCallback = null;

    private void Window_Closed(object sender, EventArgs e)
        => LogClosedCallback?.Invoke(e);

    private void Window_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
    {
        if (e.NewValue is LogContextLogic context)
        {
            context.ModalService.SetOwner(this);
        }
    }
}