using MicroRenameLogic.ServiceBase;
using MicroRenameLogic.Types;
using MicroRenameLogic.ViewContexts.EventLogViewContext;
using System;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace MicroRenameLogic.ViewContexts.MainViewContext;

public partial class MainContextLogic
{
    private void OpenAbout()
        => AboutDialogService.ShowDialog();

    private void OpenSettings()
        => SettingsDialogService.ShowDialog();

    private void OpenLog()
        => LoggingService.ShowLog();

    private void NamingContext_PropertyChanged(object sender, PropertyChangedEventArgs e)
        => Preview = GetName(SelectedItem);

    public void SetSelectedItems(System.Collections.IList list)
    {
        try
        {
            ContextData.SelectedItems.Clear();
            ContextData.SelectedItems.AddRange(list.Cast<MRTask>());
        }
        catch (Exception ex)
        { EventLog.LogError(ex, "Unable to set selected items"); }
    }
}