namespace MicroRenameLogic.Services.Config;

public sealed partial class ConfigData
{
    // Naming

    public string NamingPrefix { get; set; } = ConfigDefaults.DEFAULTPREFIX;
    public string NamingSuffix { get; set; } = ConfigDefaults.DEFAULTSUFFIX;
    public int NamingIndex { get; set; } = ConfigDefaults.DEFAULTINDEX;
    public int NamingDigits { get; set; } = ConfigDefaults.DEFAULTDIGITS;
    public int NamingIncrement { get; set; } = ConfigDefaults.DEFAULTINCREMENT;
    public bool NamingKeepExtension { get; set; } = ConfigDefaults.DEFAULTKEEPEXT;

    // Storage

    public string LastDirectory { get; set; } = string.Empty;

    // Log

    public float BatchColorSaturation { get; set; } = 0.75f;
    public float BatchColorLightness { get; set; } = 0.5f;
}