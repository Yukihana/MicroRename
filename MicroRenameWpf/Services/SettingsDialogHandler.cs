using MicroRenameWpf.Windows;
using IDialogService = MicroRenameLogic.ServiceBase.IDialogService;

namespace MicroRenameWpf.Services
{
    internal class SettingsDialogHandler : IDialogService
    {
        public void ShowDialog() => new SettingsWindow().ShowDialog();
    }
}