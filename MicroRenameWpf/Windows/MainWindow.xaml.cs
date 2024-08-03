using MicroRenameLogic;
using MicroRenameLogic.ServiceBase;
using MicroRenameLogic.Types;
using MicroRenameLogic.ViewContexts.MainViewContext;
using MicroRenameWpf.Services;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace MicroRenameWpf.Windows;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    // Lifecycle

    public MainWindow()
    {
        InitializeComponent();
        DataContext = new MainContextLogic();
    }

    private void Window_Closed(object sender, System.EventArgs e)
    {
        // Close other windows when this window is closed
        ServicesRelay.Shutdown();
    }

    // DataContext registration

    private void Window_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
    {
        // Update dependencies and subscriptions

        if (e.OldValue is MainContextLogic oldContext)
        {
            oldContext.RequestSelection = null;
        }
        if (e.NewValue is MainContextLogic newContext)
        {
            newContext.RequestSelection = SelectionRequested;
            newContext.ModalService.SetOwner(this);
        }
    }

    // Control delegation

    private MRTask[] SelectionRequested()
    {
        List<MRTask> list = [];
        foreach (var item in MRFiles.SelectedItems)
        {
            if (item is MRTask task)
                list.Add(task);
        }
        return [.. list];
    }

    private IModalService GetModalService()
        => _modalService;

    // UI functions

    private void Location_Click(object sender, RoutedEventArgs e)
    {
        if (sender is Button button &&
            button.Content is TextBlock textContent &&
            !string.IsNullOrEmpty(textContent.Text))
        {
            System.Diagnostics.Process.Start($"explorer {textContent.Text}");
        }
    }

    // FileDrop

    private void Drag_Enter(object sender, DragEventArgs e)
        => DropperMarker.Visibility = Visibility.Visible;

    private void Drag_Leave(object sender, DragEventArgs e)
        => DropperMarker.Visibility = Visibility.Hidden;

    private void Drag_Over(object sender, DragEventArgs e)
    {
        e.Effects = e.Data.GetDataPresent(DataFormats.FileDrop)
            ? DragDropEffects.Link
            : DragDropEffects.None;
        e.Handled = true;
    }

    private void Drag_Drop(object sender, DragEventArgs e)
    {
        if (DataContext is not MainContextLogic logic)
            return;
        if (e.Data.GetDataPresent(DataFormats.FileDrop))
            logic.AddFromDrop((string[])e.Data.GetData(DataFormats.FileDrop));
        DropperMarker.Visibility = Visibility.Hidden;

        MessageBoxButton.
    }
}