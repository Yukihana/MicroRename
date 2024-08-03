using MicroRenameLogic.Services.Config;
using MicroRenameLogic.Services.BatchHue;
using MicroRenameLogic.Types;
using MicroRenameLogic.ViewContexts.RenameLogViewContext;
using System;
using System.IO;
using System.Reflection;
using System.Text.Json;

namespace MicroRenameLogic.Services.Rename;

internal static partial class RenameService
{
    // Rename

    public static RenameResult Rename(string oldPath, string newPath, bool autoOverwrite)
    {
        RenameResult result = new(oldPath, newPath);

        try
        {
            if (File.Exists(newPath))
            {
                if (autoOverwrite)
                {
                    File.Delete(newPath);
                }
                else
                {
                    result.Status = RenameResultStatus.FileExists;
                    return result;
                }
            }
            File.Move(oldPath, newPath);
            result.Status = RenameResultStatus.Completed;
        }
        catch (Exception ex)
        {
            result.Status = RenameResultStatus.Faulted;
            result.Exception = ex;
        }

        return result;
    }

    public static RenameLogEntry CreateLogEntry(this RenameResult result, BatchInfo sessionInfo)
    {
        RenameLogEntry logEntry = new()
        {
            OldLocation = result.OldPath,
            NewLocation = result.NewPath,
            TimeStamp = result.TimeStamp,
            Session = sessionInfo.Index,
            Color = sessionInfo.HtmlColor,
        };

        switch (result.Status)
        {
            case RenameResultStatus.Completed:
                logEntry.Status = RenameStatus.Completed;
                break;

            case RenameResultStatus.Faulted:
                logEntry.Status = RenameStatus.Faulted;
                if (result.Exception is not null)
                {
                    logEntry.ErrorMessage = result.Exception.Message;
                    logEntry.ErrorSource = result.Exception.Source;
                    logEntry.ErrorType = result.Exception.GetType().ToString();
                    logEntry.ErrorData = JsonSerializer.Serialize(result.Exception);
                }
                break;

            case RenameResultStatus.FileExists:
                logEntry.Status = RenameStatus.Faulted;
                logEntry.ErrorMessage = "File already exists. Overwrite was not authorized.";
                break;

            default:
                break;
        }

        return logEntry;
    }
}