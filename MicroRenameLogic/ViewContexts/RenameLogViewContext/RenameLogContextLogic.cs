using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MicroRenameLogic.ServiceBase;
using System;

namespace MicroRenameLogic.ViewContexts.RenameLogViewContext;

public sealed partial class RenameLogContextLogic : ObservableObject
{
    // Data

    [ObservableProperty]
    private RenameLogContextData _contextData = new();

    // Components and Services

    private readonly IModalService _modalService = ServicesRelay.ModalService;

    // Delegates

    public Func<RenameLogEntry[]>? RequestSelection { get; set; } = null;

    // Commands

    private readonly RelayCommand _clearCommand;
    private readonly RelayCommand _copyCommand;
    private readonly RelayCommand _saveCommand;
    private readonly RelayCommand _revertCommand;

    public RelayCommand ClearCommand => _clearCommand;
    public RelayCommand CopyCommand => _copyCommand;
    public RelayCommand SaveCommand => _saveCommand;
    public RelayCommand RevertCommand => _revertCommand;

    public RenameLogContextLogic()
    {
        _clearCommand = new(Clear);
        _copyCommand = new(Copy);
        _saveCommand = new(Save);
        _revertCommand = new(Revert);
    }
}