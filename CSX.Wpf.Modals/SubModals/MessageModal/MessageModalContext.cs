using CommunityToolkit.Mvvm.ComponentModel;
using CSX.Internals.Shared.Modals;

namespace CSX.Wpf.Modals.SubModals.MessageModal;

public sealed partial class MessageModalContext : ObservableObject
{
    [ObservableProperty]
    private string _message = string.Empty;

    [ObservableProperty]
    private AlertTypes _alertType = AlertTypes.None;

    [ObservableProperty]
    private string? _iconResourceKey = string.Empty;
}