namespace MicroRenameLogic.Services.Config;

public static partial class ConfigService
{
    static ConfigService()
    {
    }

    // Change this later, to enable saving and reading from disk
    public static readonly ConfigData ConfigData = new();
}