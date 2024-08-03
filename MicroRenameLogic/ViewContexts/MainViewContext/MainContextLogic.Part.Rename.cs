using MicroRenameLogic.ServiceBase;
using MicroRenameLogic.Services.BatchHue;
using MicroRenameLogic.Services.Rename;
using MicroRenameLogic.Types;
using MicroRenameLogic.ViewContexts.EventLogViewContext;
using MicroRenameLogic.ViewContexts.NamingViewContext;
using System.IO;
using System.Runtime.Remoting.Messaging;

namespace MicroRenameLogic.ViewContexts.MainViewContext;

public partial class MainContextLogic
{
    private void Rename()
    {
        // Retrieve selected items
        if (RequestSelection is null)
        {
            EventLog.LogError("Selection requisition delegate has not been registered.");
            return;
        }
        MRTask[] tasks = RequestSelection();
        if (tasks.Length == 0)
            return;

        // Prepare
        BatchInfo batchInfo = BatchHueService.CreateBatchInfo();

        // Iteration
        int successCount = 0;
        bool sessionOverwrite = ContextData.AutoOverwrite;
        foreach (MRTask task in tasks)
        {
            // Rename One
            RenameOperation rop = new(task)
            {
                NewName = NamingContext.GetName(task.Extension),
                Overwrite = sessionOverwrite,
            };
            RenameResult? result = Rename(rop);

            // Aborted
            if (result is null || result.Status is not RenameResultStatus.Completed)
            {
                RenameLog.LogAborted(task, batchInfo);

                ModalService.Query(
                       $"The rename operation was stopped at {task.Filename}. View logs for details.",
                       "Aborted",
                       MessageBoxButton.OK,
                       MessageBoxImage.Warning);
                break;
            }

            // On Progress
            RenameLog.Log(result, batchInfo);
            sessionOverwrite = rop.Overwrite;
            successCount++;

            if (!NamingContext.Step())
                break;
        }

        // Final report
        if (ContextData.ShowCompletionMessage)
        {
            ModalService.Query(
            $"Renamed {successCount} of {tasks.Length} files. View logs for details.",
            "Report",
            MessageBoxButton.OK,
            MessageBoxImage.Warning);
        }
    }

    /// <summary>
    ///
    /// </summary>
    /// <returns>True if the operation can continue.</returns>
    private RenameResult? Rename(RenameOperation rop)// MRTask task, out bool abort, out bool enableOverwrite)
    {
        string oldPath = Path.Combine(rop.Task.Directory, rop.Task.Filename);
        string newPath = Path.Combine(rop.Task.Directory, rop.NewName);

        RenameResult? result = null;

        while (result is null)
        {
            RenameResult r = RenameService.Rename(oldPath, newPath, rop.Overwrite);

            if (r.Status == RenameResultStatus.Completed)
            {
                result = r;
                break;
            }

            if (r.Status == RenameResultStatus.FileExists)
            {
                // Query: Retry, Retry and Enable Overwrite, Abort
            }

            if (r.Status == RenameResultStatus.Faulted)
            {
                // Query: Retry, Abort
            }
        }

        return result;
    }

    private void ResetNaming()
    {
        NamingContext.ContextData.Prefix = "Prefix_";
        NamingContext.ContextData.Suffix = NamingContext.ContextData.KeepExtension ? "_suffix" : "_suffix.ext";
        NamingContext.ContextData.Increment = 1;
        NamingContext.ContextData.Index = 1;
        NamingContext.ContextData.Digits = 4;
    }

    /*
 *  if ()
                {
                    if (!autoOverwrite)
                    {
                        queryService.Query("File already exists. Overwrite?", "Overwrite confirmation", MessageBoxButton.YesNoCancel, MessageBoxImage.Warning);

                        private int result = 0;
                        // Query: File already exists.
                        // Options: Yes (overwrite), No (Skip), Cancel (Abort)

                        if(result == 0)
                        {
                        }
                    }
                }

                        && overwrite)
            }

            if (string.IsNullOrEmpty(result))
{
    file.Filename = newname;
    logEntry.Status = RenameStatus.Completed;
    status = 1;
}
else if (QueryModalService.Query(
    $"Unable to rename '{file.Filename}' to '{newname}'\nWould you like to try again?",
    "Rename failed", MessageBoxButton.YesNo, MessageBoxImage.Asterisk)
    == MessageBoxResult.No)
{
    logEntry.Status = RenameStatus.Faulted;
    logEntry.ErrorMessage = result;
    status = 2;
}
        }
        while (status == 0) ;
    }

    public static bool MoveFile(string oldPath, string newPath, bool overwrite)
{
}
*/
}