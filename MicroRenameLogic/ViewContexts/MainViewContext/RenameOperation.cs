using MicroRenameLogic.Services.Rename;
using MicroRenameLogic.Types;

namespace MicroRenameLogic.ViewContexts.MainViewContext;

internal class RenameOperation(MRTask task)
{
    public MRTask Task { get; private set; } = task;

    public string NewName { get; set; } = string.Empty;

    public bool Abort { get; set; } = false;

    public bool Overwrite { get; set; } = false;

    public RenameResult? Result { get; internal set; } = null;
}