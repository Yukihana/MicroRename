namespace MicroRenameLogic.Types;

public enum RenameStatus : byte
{
    Unknown,
    Completed,
    Expired,
    Reversed,
    Faulted,
}