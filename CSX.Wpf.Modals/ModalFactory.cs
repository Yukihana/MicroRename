using CSX.Internals.Shared.Modals;
using CSX.Wpf.Modals.SubModals.MessageModal;
using CSX.Wpf.Modals.Views;
using System;
using System.Windows;

namespace CSX.Wpf.Modals.Services;

public static partial class ModalFactory
{
    // Setup

    private static bool _setupCompleted = false;
    public const string ResourceDictionaryPath = "/CSX.Wpf.Modals;component/ResourceDictionaries.xaml";

    public static void Setup()
    {
        if (_setupCompleted)
            return;

        if (Application.Current is not null)
        {
            ResourceDictionary dic = new()
            {
                Source = new Uri(ResourceDictionaryPath, UriKind.RelativeOrAbsolute)
            };
            Application.Current.Resources.MergedDictionaries.Add(dic);
        }

        _setupCompleted = true;
    }

    private static void ValidateSetup()
    {
        if (!_setupCompleted)
            throw new InvalidOperationException("ModalService.Setup() hasn't been called for resource initialization yet.");
    }

    // Public API

    public static T ShowModal<T>(
        object? owner,
        UIElement content,
        string caption,
        ModalButtons<T> buttons,
        AlertTypes alertType)
    {
        ValidateSetup();

        ModalWindow window = new()
        {
            Owner = owner as Window,
            Title = caption,
            Buttons = buttons,
            AlertType = alertType,
        };

        window.DisplayModal(content);

        return window.Result;
    }

    public static T ShowMessage<T>(
        object? owner,
        string message,
        string caption,
        ModalButtons<T> buttons,
        AlertTypes alertType,
        string? customImageKey)
    {
        ValidateSetup();

        MessageModalContext context = new()
        {
            Message = message,
            IconResourceKey = customImageKey,
        };

        MessageModalView view = new()
        {
            DataContext = context
        };

        return ShowModal(
            owner: owner,
            content: view,
            caption: caption,
            buttons: buttons,
            alertType: alertType);
    }
}