namespace MicroRenameLogic.FrontendInterfaces
{
    public interface IMessageBoxHandler
    {
        MessageBoxResult Query(string message, string caption, MessageBoxButton buttons, MessageBoxImage image);
    }
}