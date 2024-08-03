using MicroRenameLogic.Types;
using System;

namespace MicroRenameLogic.ViewContexts.EventLogViewContext;

public static partial class EventLogExtensions
{
    // Log Information

    public static void LogInformation(this EventLogContextLogic logic, string message, params object[] args)
    {
        logic.Log(
            level: LogLevel.Information,
            message: message,
            exception: null,
            args: args);
    }

    // Log Error

    public static void LogError(this EventLogContextLogic logic, string message, params object[] args)
    {
        logic.Log(
            level: LogLevel.Error,
            message: message,
            exception: null,
            args: args);
    }

    public static void LogError(this EventLogContextLogic logic, Exception exception, string message, params object[] args)
    {
        logic.Log(
            level: LogLevel.Error,
            message: message,
            exception: exception,
            args: args);
    }

    // Log Warning

    public static void LogWarning(this EventLogContextLogic logic, string message, params object[] args)
    {
        logic.Log(
            level: LogLevel.Warning,
            message: message,
            exception: null,
            args: args);
    }

    // Log Debug

    public static void LogDebug(this EventLogContextLogic logic, string message, params object[] args)
    {
        logic.Log(
            level: LogLevel.Debug,
            message: message,
            exception: null,
            args: args);
    }
}