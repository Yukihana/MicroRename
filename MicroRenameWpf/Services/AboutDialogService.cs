using MicroRenameLogic.ServiceBase;
using MicroRenameWpf.Windows;

namespace MicroRenameWpf.Services;

internal class AboutDialogService : IDialogService
{
    public void ShowDialog() => new AboutWindow().ShowDialog();
}