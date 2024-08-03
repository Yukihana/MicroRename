using CSX.Internals.Shared.Modals;
using CSX.Wpf.Modals.Services;
using System;
using System.Media;
using System.Windows;
using System.Windows.Controls;

namespace CSX.Wpf.Modals.Views;

/// <summary>
/// Interaction logic for ModalWindow.xaml
/// </summary>
public partial class ModalWindow : Window
{
    // Load

    public ModalWindow()
        => InitializeComponent();

    private bool _shown;

    protected override void OnContentRendered(EventArgs e)
    {
        base.OnContentRendered(e);

        // Tasks to perform everytime the window is shown

        // Keep track if this is first shown.

        if (_shown)
            return;

        _shown = true;

        // Tasks to perform only the first time the window is shown.

        SoundService.PlayAlert(AlertType);
    }

    // Runtime

    public void DisplayModal(UIElement element)
    {
        // Content
        ContentPanel.Child = element;

        // Buttons
        for (int i = 0; i < Buttons.Length; i++)
        {
            string key = Buttons[i].Key;
            string value = Buttons[i].Value;

            TextBlock t = new()
            {
                Text = value,
            };
            Button b = new()
            {
                Content = t,
                Tag = key,
            };
            b.Click += Button_Click;
            ButtonsPanel.Children.Add(b);

            if (key == CancelButtonKey)
                b.IsCancel = true;
            else if (key == DefaultButtonKey)
                b.IsDefault = true;
        }

        // Show
        ShowDialog();
    }

    private void Button_Click(object sender, RoutedEventArgs e)
    {
        if (sender is not Button button ||
            button.Tag is not string key)
            return;

        Result = key;
        DialogResult = key != CancelButtonKey;
        Close();
    }

    // Data

    public ModalButtons Buttons { get; set; } = ModalButtons.OkCancel;
    public AlertTypes AlertType { get; set; } = AlertTypes.None;
    public string DefaultButtonKey { get; set; } = ModalButtons.OK.Key;
    public string CancelButtonKey { get; set; } = ModalButtons.Cancel.Key;
    public string Result { get; set; } = string.Empty;
}