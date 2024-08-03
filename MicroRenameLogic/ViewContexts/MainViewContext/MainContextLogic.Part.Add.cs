using MicroRenameLogic.ServiceBase;
using MicroRenameLogic.ViewContexts.EventLogViewContext;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MicroRenameLogic.ViewContexts.MainViewContext;

public partial class MainContextLogic
{
    private void AddTasks(IEnumerable<string> files)
    {
        // Analyze
        List<string> newFiles = [];
        foreach (string file in files)
        {
            if (!ContextData.Tasks.Any(x => x.Location == file))
                newFiles.Add(file);
        }

        // Confirm user selection
        bool newOnly = false;
        if (files.Count() > newFiles.Count)
        {
            var result = ModalService.Query(
                message: "Some files have already been added before. Add them again?",
                caption: "Duplicates",
                buttons: MessageBoxButton.YesNoCancel,
                image: MessageBoxImage.Asterisk);

            if (result == MessageBoxResult.Cancel)
            {
                EventLog.LogInformation("Cancelled adding files.");
                return;
            }

            if (result == MessageBoxResult.No)
                newOnly = true;
        }

        if (newOnly)
        {
            foreach (string file in newFiles)
                ContextData.Tasks.Add(new(file));
            EventLog.LogInformation(
                "Added {count} of {all} files. Ignored {ignored} duplicates.",
                newFiles.Count(),
                files.Count(),
                files.Count() - newFiles.Count);
        }
        else
        {
            foreach (string file in files)
                ContextData.Tasks.Add(new(file));
            EventLog.LogInformation("Added {count} files.", files.Count());
        }
    }

    // Add Methods

    private void AddFiles()
    {
        try
        {
            IEnumerable<string> files = StorageGuiService.GetFiles(
                dialogMessage: "Select files to rename...",
                startingPath: Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments));

            AddTasks(files);
        }
        catch (Exception ex)
        { EventLog.LogError(ex, "Unable to add files."); }
    }

    private void AddFolder()
    {
        try
        {
            IEnumerable<string> files = StorageGuiService.GetFilesFromFolder(
                 dialogMessage: "Select a folder to rename files in...",
                 startingPath: Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments));

            AddTasks(files);
        }
        catch (Exception ex)
        { EventLog.LogError(ex, "Unable to add folder."); }
    }

    public void AddFromDrop(string[] files)
        => AddTasks(files);
}