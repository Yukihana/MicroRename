using System.Text;

namespace MicroRenameLogic.ViewContexts.NamingViewContext;

public static partial class NamingExtensions
{
    public static string GetName(this NamingContextData context)
    {
        StringBuilder sb = new();
        sb.Append(context.Prefix);
        sb.Append(context.Index.ToString().PadLeft(context.Digits, '0'));
        sb.Append(context.Suffix);
        if (context.KeepExtension)
            sb.Append(context.Extension);
        return sb.ToString();
    }

    public static string GetName(this NamingContextLogic context)
        => context.ContextData.GetName();

    public static string GetName(this NamingContextData context, string extension)
    {
        StringBuilder sb = new();
        sb.Append(context.Prefix);
        sb.Append(context.Index.ToString().PadLeft(context.Digits, '0'));
        sb.Append(context.Suffix);
        if (context.KeepExtension)
            sb.Append(extension);
        return sb.ToString();
    }

    public static string GetName(this NamingContextLogic context, string extension)
        => context.ContextData.GetName(extension);
}