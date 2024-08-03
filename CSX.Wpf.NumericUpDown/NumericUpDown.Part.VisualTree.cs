using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace CSX.Wpf.CustomControls;

public partial class NumericUpDown
{
    public override void OnApplyTemplate()
    {
        base.OnApplyTemplate();
        AttachToVisualTree();
        AttachCommands();
    }

    // Parts

    protected TextBox TextBox;
    protected RepeatButton IncreaseButton;
    protected RepeatButton DecreaseButton;

    private void AttachToVisualTree()
    {
        if (GetTemplateChild("PART_TextBox") is TextBox textBox)
        {
            TextBox = textBox;
            TextBox.LostFocus += TextBoxOnLostFocus;
            TextBox.PreviewMouseLeftButtonUp += TextBoxOnPreviewMouseLeftButtonUp;

            TextBox.UndoLimit = 1;
            TextBox.IsUndoEnabled = true;
        }

        if (GetTemplateChild("PART_IncreaseButton") is RepeatButton increaseButton)
        {
            IncreaseButton = increaseButton;
            IncreaseButton.Focusable = false;
            IncreaseButton.Command = _minorIncreaseValueCommand;
            IncreaseButton.PreviewMouseRightButtonDown += ButtonOnPreviewMouseRightButtonDown;
            IncreaseButton.PreviewMouseLeftButtonDown += (sender, args) => RemoveFocus();
        }

        if (GetTemplateChild("PART_DecreaseButton") is RepeatButton decreaseButton)
        {
            DecreaseButton = decreaseButton;
            DecreaseButton.Focusable = false;
            DecreaseButton.Command = _minorDecreaseValueCommand;
            DecreaseButton.PreviewMouseRightButtonDown += ButtonOnPreviewMouseRightButtonDown;
            DecreaseButton.PreviewMouseLeftButtonDown += (sender, args) => RemoveFocus();
        }
    }

    // Commands

    private readonly RoutedUICommand _minorIncreaseValueCommand = new("MinorIncreaseValue", "MinorIncreaseValue", typeof(NumericUpDown));
    private readonly RoutedUICommand _minorDecreaseValueCommand = new("MinorDecreaseValue", "MinorDecreaseValue", typeof(NumericUpDown));
    private readonly RoutedUICommand _majorIncreaseValueCommand = new("MajorIncreaseValue", "MajorIncreaseValue", typeof(NumericUpDown));
    private readonly RoutedUICommand _majorDecreaseValueCommand = new("MajorDecreaseValue", "MajorDecreaseValue", typeof(NumericUpDown));
    private readonly RoutedUICommand _updateValueStringCommand = new("UpdateValueString", "UpdateValueString", typeof(NumericUpDown));
    private readonly RoutedUICommand _cancelChangesCommand = new("CancelChanges", "CancelChanges", typeof(NumericUpDown));

    private void AttachCommands()
    {
        CommandBindings.Add(new CommandBinding(_minorIncreaseValueCommand, (a, b) => IncreaseValue(true)));
        CommandBindings.Add(new CommandBinding(_minorDecreaseValueCommand, (a, b) => DecreaseValue(true)));
        CommandBindings.Add(new CommandBinding(_majorIncreaseValueCommand, (a, b) => IncreaseValue(false)));
        CommandBindings.Add(new CommandBinding(_majorDecreaseValueCommand, (a, b) => DecreaseValue(false)));
        CommandBindings.Add(new CommandBinding(_updateValueStringCommand, (a, b) => Value = ParseStringToDecimal(TextBox.Text)));
        CommandBindings.Add(new CommandBinding(_cancelChangesCommand, (a, b) => CancelChanges()));

        CommandManager.RegisterClassInputBinding(typeof(TextBox), new KeyBinding(_minorIncreaseValueCommand, new KeyGesture(Key.Up)));
        CommandManager.RegisterClassInputBinding(typeof(TextBox), new KeyBinding(_minorDecreaseValueCommand, new KeyGesture(Key.Down)));
        CommandManager.RegisterClassInputBinding(typeof(TextBox), new KeyBinding(_majorIncreaseValueCommand, new KeyGesture(Key.PageUp)));
        CommandManager.RegisterClassInputBinding(typeof(TextBox), new KeyBinding(_majorDecreaseValueCommand, new KeyGesture(Key.PageDown)));
        CommandManager.RegisterClassInputBinding(typeof(TextBox), new KeyBinding(_updateValueStringCommand, new KeyGesture(Key.Enter)));
        CommandManager.RegisterClassInputBinding(typeof(TextBox), new KeyBinding(_cancelChangesCommand, new KeyGesture(Key.Escape)));

        /*
        //Incase multiple of these controls on the same page conflict with Up/Down buttons

        TextBox.InputBindings.Add(new KeyBinding(_minorIncreaseValueCommand, new KeyGesture(Key.Up)));
        TextBox.InputBindings.Add(new KeyBinding(_minorDecreaseValueCommand, new KeyGesture(Key.Down)));
        */
    }
}