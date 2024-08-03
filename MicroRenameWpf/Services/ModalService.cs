using CSX.Internals.Shared.Modals;
using CSX.Internals.Shared.ServiceBases;
using CSX.Wpf.Modals.Services;
using MicroRenameLogic.ServiceBase;
using System;
using System.Windows;

namespace MicroRenameWpf.Services;

public partial class ModalService : IModalService
{
    // Owner

    private Window? _owner = null;

    public void SetOwner(object? parentWindow)
    {
        if (parentWindow is Window window)
            _owner = window;
        else if (parentWindow is null)
            _owner = null;
        else
            throw new ArgumentException("The provided instance must either be null or a valid 'Window' type.", nameof(parentWindow));
    }

    // Standard Message

    public T Query<T>(string message, string caption, ModalButtons<T> buttons, AlertTypes alertType, string? customImageKey) => ModalFactory.ShowMessage(
        owner: _owner,
        message: message,
        caption: caption,
        buttons: buttons,
        alertType: alertType,
        customImageKey: customImageKey);
}