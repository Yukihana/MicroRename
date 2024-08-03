using CSX.Internals.Shared.Modals;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace CSX.Wpf.Modals.SubModals.MessageModal;

/// <summary>
/// Interaction logic for MessageContentView.xaml
/// </summary>
public partial class MessageModalView : UserControl
{
    public MessageModalView()
        => InitializeComponent();

    private void Reset()
    {
        UpdateIcon(AlertTypes.None, string.Empty);
    }

    protected virtual void OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
    {
        if (e.OldValue is MessageModalContext oldContext)
            oldContext.PropertyChanged -= DataContext_PropertyChanged;
        if (e.NewValue is MessageModalContext newContext)
            newContext.PropertyChanged += DataContext_PropertyChanged;
    }

    private void DataContext_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        if (DataContext is not MessageModalContext context)
        {
            Reset();
            return;
        }

        switch (e.PropertyName)
        {
            case nameof(MessageModalContext.IconResourceKey):
            case nameof(MessageModalContext.AlertType):
                UpdateIcon(context.AlertType, context.IconResourceKey ?? string.Empty);
                break;

            default:
                break;
        }
    }

    // Icon

    protected string _lastIconKey = string.Empty;
    protected AlertTypes _lastAlertType = AlertTypes.None;

    protected virtual void UpdateIcon(AlertTypes alertType, string iconKey)
    {
        // Avoid redundant changes.
        if (_lastIconKey == iconKey && _lastAlertType == alertType)
            return;

        // Use preset if custom key isn't provided
        if (string.IsNullOrEmpty(iconKey))
        {
            switch (alertType)
            {
                case AlertTypes.Information:
                    iconKey = "ModalInformationIcon";
                    break;

                case AlertTypes.Exclamation:
                    iconKey = "ModalExclamationIcon";
                    break;

                case AlertTypes.Question:
                    iconKey = "ModalQuestionIcon";
                    break;

                case AlertTypes.Success:
                    iconKey = "ModalSuccessIcon";
                    break;

                case AlertTypes.Warning:
                    iconKey = "ModalWarningIcon";
                    break;

                case AlertTypes.Error:
                    iconKey = "ModalErrorIcon";
                    break;

                case AlertTypes.None:
                    break;

                default:
                    break;
            }
        }

        SetIcon(iconKey);

        // Save to avoid redundancy
        _lastIconKey = iconKey;
        _lastAlertType = alertType;
    }

    protected virtual void SetIcon(string key)
    {
        if (string.IsNullOrEmpty(key))
        {
            MessageIcon.Source = null;
            return;
        }

        var resource = TryFindResource(key);
        if (resource is DrawingBrush brush)
        {
            MessageIcon.Source = new DrawingImage(brush.Drawing);
        }
        else if (resource is BitmapImage image)
        {
            MessageIcon.Source = image;
        }
    }
}