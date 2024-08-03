using MicroRenameLogic.ServiceBase;
using MicroRenameLogic.Types;
using MicroRenameLogic.ViewContexts.EventLogViewContext;
using System.IO;
using System.Linq;

namespace MicroRenameLogic.ViewContexts.MainViewContext;

public partial class MainContextLogic
{
    private void RemoveFiles()
    {
        MRTask[] toRemove = [.. ContextData.SelectedItems];
        int count = 0;
        foreach (MRTask task in toRemove)
        {
            if (ContextData.Tasks.Remove(task))
                count++;
        }

        EventLog.LogInformation($"{count} of {toRemove} selected items were removed.");
    }

    private void ClearMissing()
    {
        if (ModalService.Query("Clear all missing files?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Asterisk) == MessageBoxResult.Yes)
        {
            MRTask[] removeable = ContextData.Tasks.Where(x => !File.Exists(x.Location)).ToArray();
            int count = 0;
            foreach (MRTask task in removeable)
            {
                ContextData.Tasks.Remove(task);
                count++;
            }

            EventLog.LogInformation($"Removed {count} missing files.");
        }
    }

    private void ClearAll()
    {
        if (ModalService.Query("Clear all tasks?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Asterisk) == MessageBoxResult.Yes)
        {
            ContextData.Tasks.Clear();
            EventLog.LogInformation("All tasks cleared.");
        }
    }
}