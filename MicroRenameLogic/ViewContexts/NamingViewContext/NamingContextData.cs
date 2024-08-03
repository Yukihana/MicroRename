using CommunityToolkit.Mvvm.ComponentModel;
using MicroRenameLogic.Services.Config;

namespace MicroRenameLogic.ViewContexts.NamingViewContext;

public sealed partial class NamingContextData : ObservableObject
{
    // Primary

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(PreviewName))]
    private string _prefix = ConfigService.ConfigData.NamingPrefix;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(PreviewName))]
    private string _suffix = ConfigService.ConfigData.NamingSuffix;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(PreviewName))]
    private int _index = ConfigService.ConfigData.NamingIndex;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(MaxIndex))]
    [NotifyPropertyChangedFor(nameof(PreviewName))]
    private int _digits = ConfigService.ConfigData.NamingDigits;

    [ObservableProperty]
    private int _increment = ConfigService.ConfigData.NamingIncrement;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(PreviewName))]
    private string _extension = string.Empty;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(PreviewName))]
    private bool _keepExtension = ConfigService.ConfigData.NamingKeepExtension;

    // Secondary

    public int MaxIndex
        => (10 ^ Digits) - 1;

    public string PreviewName
        => this.GetName();
}