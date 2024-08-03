namespace MicroRenameLogic.Services.BatchHue;

public readonly struct BatchInfo(int index, string htmlColor)
{
    public readonly int Index { get; } = index;
    public readonly string HtmlColor { get; } = htmlColor;
}