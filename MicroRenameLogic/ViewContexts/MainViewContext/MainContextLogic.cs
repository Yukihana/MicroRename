using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MicroRenameLogic.ServiceBase;
using MicroRenameLogic.Services.BatchHue;
using MicroRenameLogic.Types;
using MicroRenameLogic.ViewContexts.EventLogViewContext;
using MicroRenameLogic.ViewContexts.NamingViewContext;
using MicroRenameLogic.ViewContexts.RenameLogViewContext;
using System;

namespace MicroRenameLogic.ViewContexts.MainViewContext;

public sealed partial class MainContextLogic : ObservableObject
{
    // Components

    private readonly IStorageGuiService _storageGuiService = ServicesRelay.StorageGuiService;
    private readonly IDialogService _settingsDialogService = ServicesRelay.SettingsDialogService;
    private readonly IDialogService _aboutDialogService = ServicesRelay.AboutDialogService;
    private readonly ILoggingService _loggingService = ServicesRelay.LoggingService;

    // Class-Scoped Components

    private readonly IModalService _modalService = ServicesRelay.ModalService;
    private readonly BatchHueService BatchHueService = new();

    // Subcomponents

    private EventLogContextLogic EventLog => _loggingService.Logic.EventLogContext;
    private RenameLogContextLogic RenameLog => _loggingService.Logic.RenameLogContext;

    // Data

    [ObservableProperty]
    private MainContextData _contextData = new();

    [ObservableProperty]
    private NamingContextLogic _namingContext = new();

    // IoC

    public Func<MRTask[]>? RequestSelection { get; set; } = null;
    public IModalService ModalService => _modalService;

    // Observable

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(CurrentLocation))]
    private MRTask? _selectedItem = null;

    public string CurrentLocation
        => SelectedItem?.Directory
        ?? string.Empty;

    [ObservableProperty]
    private string _preview = string.Empty;

    // Commands

    private readonly RelayCommand _addFilesCommand;
    private readonly RelayCommand _addFolderCommand;
    private readonly RelayCommand _renameCommand;
    private readonly RelayCommand _logCommand;
    private readonly RelayCommand _removeFilesCommand;
    private readonly RelayCommand _clearMissingCommand;
    private readonly RelayCommand _clearAllCommand;
    private readonly RelayCommand _nextCommand;
    private readonly RelayCommand _resetCommand;
    private readonly RelayCommand _settingsCommand;
    private readonly RelayCommand _aboutCommand;

    public RelayCommand AddFilesCommand => _addFilesCommand;
    public RelayCommand AddFolderCommand => _addFolderCommand;
    public RelayCommand RenameCommand => _renameCommand;
    public RelayCommand LogCommand => _logCommand;
    public RelayCommand RemoveFilesCommand => _removeFilesCommand;
    public RelayCommand ClearMissingCommand => _clearMissingCommand;
    public RelayCommand ClearAllCommand => _clearAllCommand;
    public RelayCommand NextCommand => _nextCommand;
    public RelayCommand ResetCommand => _resetCommand;
    public RelayCommand SettingsCommand => _settingsCommand;
    public RelayCommand AboutCommand => _aboutCommand;

    // Ctor

    public MainContextLogic()
    {
        _addFilesCommand = new RelayCommand(AddFiles);
        _addFolderCommand = new RelayCommand(AddFolder);

        _renameCommand = new RelayCommand(Rename);
        _logCommand = new RelayCommand(OpenLog);

        _removeFilesCommand = new RelayCommand(RemoveFiles);
        _clearMissingCommand = new RelayCommand(ClearMissing);
        _clearAllCommand = new RelayCommand(ClearAll);

        _resetCommand = new RelayCommand(ResetNaming);
        _nextCommand = new RelayCommand(() => MoveNext());

        _settingsCommand = new RelayCommand(OpenSettings);
        _aboutCommand = new RelayCommand(OpenAbout);
    }
}