namespace CSX.Internals.Shared.Modals;

public readonly struct ModalButton<T>(T key, string content)
{
    public readonly T Key { get; } = key;
    public readonly string Content { get; } = content;

    // Replace Value with language resource

    public static ModalButton<string> OK => new("ok", "OK");
    public static ModalButton<string> Cancel => new("cancel", "Cancel");
    public static ModalButton<string> Yes => new("yes", "Yes");
    public static ModalButton<string> No => new("no", "No");
    public static ModalButton<string> Continue => new("continue", "Continue");
    public static ModalButton<string> Abort => new("abort", "Abort");
    public static ModalButton<string> Skip => new("skip", "Skip");
    public static ModalButton<string> Clear => new("clear", "Clear");
    public static ModalButton<string> Accept => new("accept", "Accept");
    public static ModalButton<string> Decline => new("decline", "Decline");
}