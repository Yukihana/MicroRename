using MicroRenameLogic.ServiceBase;
using MicroRenameLogic.Services.Config;
using System;

namespace MicroRenameLogic;

// Initialize the components here before anything else
public static partial class ServicesRelay
{
    // Common

    private static T ThrowFor<T>(string serviceName)
        => throw new InvalidOperationException($"A service of type '{typeof(T)}' for {serviceName} has not been set yet.");

    // Data

    public static ConfigData Configuration => ConfigService.ConfigData;

    // Services : Singleton

    private static ISettingsService? _settingsService = null;
    private static ILoggingService? _loggingService = null;
    private static IStorageGuiService? _storageGuiService = null;
    private static IDialogService? _aboutDialogService = null;
    private static IDialogService? _settingsDialogService = null;

    public static ISettingsService SettingsService
    {
        get => _settingsService
            ?? ThrowFor<ISettingsService>(nameof(SettingsService));
        set => _settingsService = value;
    }

    public static ILoggingService LoggingService
    {
        get => _loggingService
            ?? ThrowFor<ILoggingService>(nameof(LoggingService));
        set => _loggingService = value;
    }

    public static IStorageGuiService StorageGuiService
    {
        get => _storageGuiService
            ?? ThrowFor<IStorageGuiService>(nameof(StorageGuiService));
        set => _storageGuiService = value;
    }

    public static IDialogService AboutDialogService
    {
        get => _aboutDialogService
            ?? ThrowFor<IDialogService>(nameof(AboutDialogService));
        set => _aboutDialogService = value;
    }

    public static IDialogService SettingsDialogService
    {
        get => _settingsDialogService
            ?? ThrowFor<IDialogService>(nameof(SettingsDialogService));
        set => _settingsDialogService = value;
    }

    // Services : Factory

    private static Func<IModalService>? _modalServiceFactory = null;

    public static Func<IModalService> ModalServiceFactory
    {
        get => _modalServiceFactory
            ?? ThrowFor<Func<IModalService>>(nameof(ModalServiceFactory));
        set => _modalServiceFactory = value;
    }

    public static IModalService ModalService
        => ModalServiceFactory();

    // LifeCycle

    public static void Shutdown()
    {
        // Non dialog windows
        LoggingService.Dispose();
    }
}