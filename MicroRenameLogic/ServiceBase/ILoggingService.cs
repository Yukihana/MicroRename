using MicroRenameLogic.ViewContexts.LogViewContext;
using System;

namespace MicroRenameLogic.ServiceBase;

public interface ILoggingService : IDisposable
{
    LogContextLogic Logic { get; }

    void ShowLog();
}