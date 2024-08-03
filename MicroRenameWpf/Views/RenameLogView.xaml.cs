using MicroRenameLogic.ViewContexts.RenameLogViewContext;
using System.Collections;
using System.Windows;
using System.Windows.Controls;

namespace MicroRenameWpf.Views;

/// <summary>
/// Interaction logic for RenameLogView.xaml
/// </summary>
public partial class RenameLogView : UserControl
{
    public RenameLogView()
        => InitializeComponent();

    private void UserControl_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
    {
        if (DataContext is RenameLogContextLogic logic)
        {
            logic.RequestSelection = SelectionRequested;
        }
    }

    private RenameLogEntry[] SelectionRequested()
    {
        IList selected = RenameLogTable.SelectedItems;
        int count = selected.Count;
        RenameLogEntry[] result = new RenameLogEntry[count];

        for (int i = 0; i < count; i++)
            result[i] = (RenameLogEntry)selected[i];

        return result;
    }
}