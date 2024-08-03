using CommunityToolkit.Mvvm.ComponentModel;
using MicroRenameLogic.ServiceBase;
using MicroRenameLogic.Services.Config;

namespace MicroRenameLogic.ViewContexts.NamingViewContext;

public partial class NamingContextLogic : ObservableObject
{
    // Components

    private readonly ConfigData Configuration = ServicesRelay.Configuration;

    private readonly IModalService QueryService = ServicesRelay.QueryModalService;

    // Data

    [ObservableProperty]
    private NamingContextData _contextData = new();

    // Commands

    public NamingContextLogic()
    {
        // Force a preview update (Verify if this is needed anymore with new code)
        // PreviewName();
    }

    //
    public bool Step()
    {
        if (ContextData.Index + ContextData.Increment > ContextData.MaxIndex)
        {
            if (QueryService.Query(
                "With the current number of digits and increment count, the maximum value has been reached. Continue further by increase the number of digits by one?",
                "Maximum reached",
                MessageBoxButton.OKCancel,
                MessageBoxImage.Warning)
                != MessageBoxResult.OK)
                return false;
            ContextData.Digits++;
        }

        // Finally
        ContextData.Index += ContextData.Increment;
        return true;
    }

    public void Reset()
    {
        ContextData.Prefix = Configuration.NamingPrefix;
        ContextData.Suffix = Configuration.NamingSuffix;
        ContextData.Index = Configuration.NamingIndex;
        ContextData.Digits = Configuration.NamingDigits;
        ContextData.Increment = Configuration.NamingIncrement;
        ContextData.KeepExtension = Configuration.NamingKeepExtension;
    }
}