using MicroRenameLogic.Types;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text.Json;

namespace MicroRenameLogic.ViewContexts.EventLogViewContext;

public sealed partial class EventLogContextLogic
{
    public void Log(LogLevel level, string message, Exception? exception, params object[] args)
    {
        EventLogEntry entry = new()
        {
            Timestamp = DateTime.Now,
            Level = level,
            Message = message,
        };

        if (args is not null && args.Any())
            entry.Attachments = ProcessAttachments(args);

        if (exception is not null)
        {
            entry.ExceptionDump = JsonSerializer.Serialize(exception);
            entry.ExceptionSource = exception.Source;
            entry.ExceptionMessage = exception.Message;
        }

        ContextData.Entries.Add(entry);
    }

    private static ObservableCollection<LogAttachment> ProcessAttachments(object[] args)
    {
        if (!args.Any())
            return [];

        ObservableCollection<LogAttachment> attachments = [];

        for (int i = 0; i < args.Length; i++)
        {
            attachments.Add(new()
            {
                Name = $"Attachment{i + 1}",
                Type = args[i].GetType().ToString(),
                Content = JsonSerializer.Serialize(args[i]),
            });
        }

        return attachments;
    }

    private static ObservableCollection<LogAttachment> ProcessAttachments(Dictionary<string, object> args)
    {
        if (!args.Any())
            return [];

        ObservableCollection<LogAttachment> attachments = [];

        foreach (var arg in args)
        {
            attachments.Add(new()
            {
                Name = arg.Key,
                Type = arg.Value.GetType().ToString(),
                Content = JsonSerializer.Serialize(arg.Value),
            });
        }

        return attachments;
    }
}