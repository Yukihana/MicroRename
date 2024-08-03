using System;
using CSX.Internals.Shared.Graphics;
using MicroRenameLogic.Types;

namespace MicroRenameLogic.ViewContexts.RenameLogViewContext;

public static partial class RenameLogExtensions
{
    public static void SetSession(this RenameLogEntry entry, (int Id, Color Color) session)
    {
        entry.Session = session.Id;
        entry.Color = session.Color.ToHtmlRgb();
    }

    public static void SetCompleted(this RenameLogEntry context)
    {
        context.Status = RenameStatus.Completed;
        context.ErrorMessage = string.Empty;
        context.ErrorSource = string.Empty;
    }

    public static void SetReversed(this RenameLogEntry context)
    {
        context.Status = RenameStatus.Reversed;
        context.ErrorMessage = string.Empty;
        context.ErrorSource = string.Empty;
    }

    public static void SetFaulted(this RenameLogEntry context, Exception exception)
    {
        context.Status = RenameStatus.Faulted;
        context.ErrorMessage = exception.Message;
        context.ErrorSource = exception.Source;
    }
}