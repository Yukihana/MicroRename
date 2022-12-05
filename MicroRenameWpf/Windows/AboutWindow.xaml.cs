using MicroRenameWpf.Win32Interop;
using System.Windows;
using System.Windows.Input;

namespace MicroRenameWpf.Windows
{
    /// <summary>
    /// Interaction logic for ash.xaml
    /// </summary>
    public partial class AboutWindow : Window
	{
		#region Init
		public AboutWindow()
		{
			InitializeComponent();
		}
		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			if (SystemParameters.IsGlassEnabled)
			{
				AeroGlass.EnableBlur(this);
			}
		}
		#endregion

		#region Events : Uncategorized
		private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Escape)
			{
				Close();
			}
		}
		private void AboutLinkClick(object sender, RoutedEventArgs e)
		{
			System.Diagnostics.Process.Start("explorer http://www.facebook.com/lilyflowerangel");
		}
		#endregion

		#region Events : Grip
		bool mouseIsDown;
		Point firstPoint;
		private void GripDown(object sender, MouseButtonEventArgs e)
        {
			firstPoint = e.GetPosition(this);
			mouseIsDown = true;
		}
        private void GripUp(object sender, MouseButtonEventArgs e)
        {
			mouseIsDown = false;
		}
        private void GripMove(object sender, MouseEventArgs e)
        {
			if (mouseIsDown == true)
			{
				var Location = e.GetPosition(this);

				// Get the difference between the two points
				double xDiff = firstPoint.X - Location.X;
			    double yDiff = firstPoint.Y - Location.Y;

				// Set the new point
				Left -= xDiff;
				Top -= yDiff;
			}
		}
        #endregion
    }
}