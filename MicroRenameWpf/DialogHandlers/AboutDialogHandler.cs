using MicroRenameLogic.FrontendInterfaces;
using MicroRenameWpf.Windows;

namespace MicroRenameWpf.OSInterface
{
    internal class AboutDialogHandler : IDialogHandler
    {
        public void ShowDialog() => new AboutWindow().ShowDialog();
    }
}