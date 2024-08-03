using CommunityToolkit.Mvvm.ComponentModel;

namespace MicroRenameLogic.ViewContexts.EventLogViewContext;

public sealed partial class LogAttachment : ObservableObject
{
    [ObservableProperty]
    private string _name = string.Empty;

    [ObservableProperty]
    private string _type = string.Empty;

    [ObservableProperty]
    private string _content = string.Empty;
}