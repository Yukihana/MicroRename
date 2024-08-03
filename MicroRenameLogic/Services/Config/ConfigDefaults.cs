using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroRenameLogic.Services.Config;

public static partial class ConfigDefaults
{
    // Naming

    public const string DEFAULTPREFIX = "Prefix_";
    public const string DEFAULTSUFFIX = "_suffix";
    public const int DEFAULTINDEX = 1;
    public const int DEFAULTDIGITS = 4;
    public const int DEFAULTINCREMENT = 1;

    public const bool DEFAULTKEEPEXT = true;
    public const int DEFAULTMAXINDEX = 9999;
}