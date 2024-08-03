using MicroRenameLogic.ServiceBase;
using MicroRenameLogic.Services.BatchHue;
using MicroRenameLogic.Services.Rename;
using MicroRenameLogic.Types;
using System;
using System.IO;

namespace MicroRenameLogic.ViewContexts.RenameLogViewContext;

public sealed partial class RenameLogContextLogic
{
    public void Log(RenameLogEntry logEntry)
        => ContextData.Entries.Add(logEntry);

    public void Log(RenameResult result, BatchInfo batchInfo)
    {
        RenameLogEntry logEntry = new()
        {
            TimeStamp = result.TimeStamp,
            // TODO
        };
        // TODO

        Log(logEntry);
    }

    private void Clear()
    {
        MessageBoxResult result = QueryHandler.Query(
            "Unsaved logs be lost permanently. Continue?",
            "Log clear confirmation",
            MessageBoxButton.OKCancel,
            MessageBoxImage.Question);
        if (result == MessageBoxResult.Yes)
            ContextData.Entries.Clear();
    }

    private void Revert()
    {
        // TODO Redo this entire thing. Bring required log parts to this class.
        // Set old to expired, add new. Tldr move a lot of things to this class.
        // Maybe make a rename service that both this and the main context can access.

        if (RequestSelection is null)
            throw new InvalidOperationException("Request Selection action has not been registered yet.");

        RenameLogEntry[] selection = RequestSelection();

        foreach (var item in selection)
        {
            try
            {
                File.Move(item.NewLocation, item.OldLocation);
                item.SetReversed();
            }
            catch (Exception ex)
            { item.SetFaulted(ex); }
        }
    }

    private void Copy()
    {
    }

    private void Save()
    {
    }

    internal void LogAborted(MRTask task, BatchInfo batchInfo)
    {
        throw new NotImplementedException();
    }

    /*
    private void Undo()
    {
    }

    private void Redo()
    {
        if (SelectedItems?.ToArray() is not RenameLogEntry[] items)
            return;

        RenameLogEntry? current = null;
        try
        {
            foreach (var item in items)
            {
                current = item;
                File.Move(item.NewLocation, item.OldLocation);
                item.SetReversed();
            }
        }
        catch (Exception ex)
        { current?.SetFaulted(ex); }
    }
    */
}