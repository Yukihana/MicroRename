using MicroRenameLogic.ViewContexts.LogViewContext;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace MicroRenameWpf.Views;

/// <summary>
/// Interaction logic for EventLogView.xaml
/// </summary>
public partial class EventLogView : UserControl
{
    public EventLogView()
        => InitializeComponent();

    private void UserControl_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
    {
        if (DataContext is EventLogContextLogic logic)
        {
            logic.RequestSelection = SelectionRequested;
        }
    }

    private IEnumerable<EventLogEntry> SelectionRequested()
    {
        List<EventLogEntry> result = [];
        foreach (var item in EventLogTable.SelectedItems)
        {
            if (item is EventLogEntry entry)
                result.Add(entry);
        }

        return result;
    }
}