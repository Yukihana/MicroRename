using CSX.Internals.Shared.Modals;

namespace MicroRenameLogic.ServiceBase;

public interface IModalService
{
    void SetOwner(object? parentWindow);

    T Query<T>(string message, string caption, ModalButtons<T> buttons, AlertTypes alertType, string? customImageKey);
}