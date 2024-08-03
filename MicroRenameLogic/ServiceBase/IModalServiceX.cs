using CSX.Internals.Shared.Modals;

namespace CSX.Internals.Shared.ServiceBases;

public interface IModalServiceX
{
    /// <summary>
    /// Shows a message box dialog with buttons.
    /// </summary>
    /// <param name="owner">The parent window, if required.</param>
    /// <param name="message">The string content of the modal.</param>
    /// <param name="caption">The title of the modal window.</param>
    /// <param name="buttons">User response options.</param>
    /// <param name="alertType">The type of notification (and image if one isn't provided).</param>
    /// <param name="imageResouceIdentifier">The resource identifier of a custom image, if provided.</param>
    /// <param name="defaultButtonIndex">In the button array the button at this index is highlighted by default.</param>
    /// <param name="cancelButtonIndex">In the button array the button at this index cancels the modal.</param>
    /// <returns>The index of the button selected, or -1 if the modal was simply closed.</returns>
    int ShowMessage(
        object? owner,
        string message,
        string caption,
        string[] buttons,
        AlertTypes alertType,
        string? imageResouceIdentifier,
        int defaultButtonIndex = 0,
        int cancelButtonIndex = -1);

    /// <summary>
    /// Shows a dialog with a templated area backed by the provided data context.
    /// Note that the provided context may be contaminated after usage, so it's always better to provide a copy.
    /// The same can be inspected after the dialog closes for user made changes.
    /// </summary>
    /// <param name="owner">The parent window, if required.</param>
    /// <param name="dataContext">The data context for the modal's content.</param>
    /// <param name="caption">The title of the modal window.</param>
    /// <param name="buttons">User response options.</param>
    /// <param name="alertType">The type of notification.</param>
    /// <param name="defaultButtonIndex">In the button array the button at this index is highlighted by default.</param>
    /// <param name="cancelButtonIndex">In the button array the button at this index cancels the modal.</param>
    /// <returns>The index of the button selected, or -1 if the modal was simply closed.</returns>
    int ShowContext(
        object? owner,
        object dataContext,
        string caption,
        string[] buttons,
        AlertTypes alertType,
        int defaultButtonIndex = 0,
        int cancelButtonIndex = -1);
}