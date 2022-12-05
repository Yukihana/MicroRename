using FI = MicroRenameLogic.FrontendInterfaces;
using SW = System.Windows;

namespace MicroRenameWpf.DialogHandlers
{
    internal class MessageBoxHandler : FI.IMessageBoxHandler
    {
        public FI.MessageBoxResult Query(string message, string caption, FI.MessageBoxButton buttons, FI.MessageBoxImage image)
            => (FI.MessageBoxResult)SW.MessageBox.Show(message, caption, (SW.MessageBoxButton)buttons, (SW.MessageBoxImage)image);
    }
}