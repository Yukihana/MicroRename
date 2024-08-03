using CSX.Wpf.Modals.Services;
using MicroRenameWpf.Services;
using MicroRenameWpf.Services.Settings;
using System.Windows;
using MRServices = MicroRenameLogic.ServicesRelay;

namespace MicroRenameWpf;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    protected override void OnStartup(StartupEventArgs e)
    {
        ServicesSetup();
        base.OnStartup(e);
    }

    protected static void ServicesSetup()
    {
        ModalFactory.Setup();

        // MicroRenameLogic
        MRServices.SettingsService = new SettingsService();
        MRServices.LoggingService = new LoggingService();
        MRServices.ModalServiceFactory = () => new ModalService();

        MRServices.SettingsDialogService = new SettingsDialogHandler();
        MRServices.AboutDialogService = new AboutDialogService();

        // Obsolete
        MRServices.StorageGuiService = new StorageGuiService(); // Move this to modals
    }
}