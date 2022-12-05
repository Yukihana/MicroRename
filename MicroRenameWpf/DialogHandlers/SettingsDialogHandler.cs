using MicroRenameWpf.Windows;
using IDialogHandler = MicroRenameLogic.FrontendInterfaces.IDialogHandler;

namespace MicroRenameWpf.OSInterface
{
    internal class SettingsDialogHandler : IDialogHandler
    {
        public void ShowDialog() => new SettingsWindow().ShowDialog();
    }
}