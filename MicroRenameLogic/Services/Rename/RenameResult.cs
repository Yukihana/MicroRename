using System;

namespace MicroRenameLogic.Services.Rename;

public sealed partial class RenameResult(string OldPath, string NewPath)
{
    public DateTime TimeStamp { get; } = DateTime.Now;
    public string OldPath { get; } = OldPath;
    public string NewPath { get; } = NewPath;
    public RenameResultStatus Status { get; set; } = RenameResultStatus.Unknown;
    public Exception? Exception { get; set; } = null;
}