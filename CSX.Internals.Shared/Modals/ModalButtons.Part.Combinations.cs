namespace CSX.Internals.Shared.Modals;

/// <summary>
/// A few preset modal button choices.
/// </summary>
public partial class ModalButtons
{
    // Some commonly used combinations

    public static ModalButton<string>[] Ok => [ModalButton<string>.OK];
    public static ModalButton<string>[] OkCancel => [ModalButton<string>.OK, ModalButton<string>.Cancel];
    public static ModalButton<string>[] YesNo => [ModalButton<string>.Yes, ModalButton<string>.No];
    public static ModalButton<string>[] YesNoCancel => [ModalButton<string>.Yes, ModalButton<string>.No, ModalButton<string>.Cancel];
    public static ModalButton<string>[] ContinueAbort => [ModalButton<string>.Continue, ModalButton<string>.Abort];
    public static ModalButton<string>[] ContinueSkipAbort => [ModalButton<string>.Continue, ModalButton<string>.Skip, ModalButton<string>.Abort];
    public static ModalButton<string>[] AllYesNoCancel => [ModalButton<string>.Yes, new("yesToAll", "Yes to all"), ModalButton<string>.No, new("noToAll", "No to all"), ModalButton<string>.Cancel];
}