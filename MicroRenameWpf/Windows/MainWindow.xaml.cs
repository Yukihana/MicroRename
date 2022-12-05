using MicroRenameWpf.OSInterface;
using MicroRenameLogic.DLG;
using MicroRenameLogic.Types;
using System.Windows;
using System.Windows.Controls;
using MicroRenameWpf.DialogHandlers;

namespace MicroRenameWpf.Windows
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MRLogic Logic;

        public MainWindow()
        {
            InitializeComponent();

            // Prepare Logic and Dialog Handlers
            Logic = new MRLogic
            {
                MessageDialog = new MessageBoxHandler(),
                FileSystemHandler = new FileSystemHandler(),
                AboutDialog = new AboutDialogHandler(),
                SettingsDialog = new SettingsDialogHandler(),
            };

            // Finally attach logic
            DataContext = Logic;
        }

        private void SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Logic.SelectedItems.Clear();
            foreach (MRTask task in (sender as DataGrid).SelectedItems)
            {
                Logic.SelectedItems.Add(task);
            }
        }
        private void Location_Click(object sender, RoutedEventArgs e)
        {
            string link = ((sender as Button)?.Content as TextBlock)?.Text;
            if (link != null)
            {
                System.Diagnostics.Process.Start($"explorer {link}");
            }
        }

        #region Allow dropping of files directly
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
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                Logic.AddFromDrop((string[])e.Data.GetData(DataFormats.FileDrop));
            }
            DropperMarker.Visibility = Visibility.Hidden;
        }
        #endregion














        /*
        bool jRename(string i, string l, string n)
        {
            bool r = false;
            string s = kFmc1Replacement(l, i, n, xOverwrite.IsChecked == true);
            if (s == "Success") r = true;
            else MessageBox.Show(s, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            return r;
        }
        string kFmc1Replacement(string iLocation, string iOldName, string iNewName, bool autoOverwrite)
        {
            string vReturn = "Rename was aborted";

            try
            {
                string vOldPath = Path.Combine(iLocation, iOldName);
                string vNewPath = Path.Combine(iLocation, iNewName);
                bool b = true;

                if(File.Exists(vNewPath))
                {
                    b = false;
                    if (autoOverwrite)
                        b = true;
                    else if (
                        MessageBox.Show("File '" + vNewPath + "' exists. Overwrite?",
                            "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Asterisk)
                        == MessageBoxResult.Yes)
                        b = true;

                    if (b)
                        File.Delete(vNewPath);
                }

                if(b)
                {
                    File.Move(vOldPath, iNewName);
                    vReturn = "Success";
                }
            }
            catch(Exception e)
            {
                vReturn = e.Message;
            }

            // Clearout---------
            return vReturn;
        }
        */
    }
}
