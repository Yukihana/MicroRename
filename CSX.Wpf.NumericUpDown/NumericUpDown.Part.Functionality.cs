using System.Linq;
using System.Windows.Input;
using System.Windows;

namespace CSX.Wpf.CustomControls;

public partial class NumericUpDown
{
    private void IncreaseValue(bool minor)
    {
        // Get the value that's currently in the TextBox.Text
        var value = ParseStringToDecimal(TextBox.Text);

        // Coerce the value to min/max
        CoerceValueToBounds(ref value);

        // Only change the value if it has any meaning
        if (value <= MaxValue)
        {
            if (minor)
            {
                if (IsValueWrapAllowed && value + MinorDelta > MaxValue)
                {
                    value = MinValue;
                }
                else
                {
                    value += MinorDelta;
                }
            }
            else
            {
                if (IsValueWrapAllowed && value + MajorDelta > MaxValue)
                {
                    value = MinValue;
                }
                else
                {
                    value += MajorDelta;
                }
            }
        }

        Value = value;
    }

    private void DecreaseValue(bool minor)
    {
        // Get the value that's currently in the TextBox.Text
        var value = ParseStringToDecimal(TextBox.Text);

        // Coerce the value to min/max
        CoerceValueToBounds(ref value);

        // Only change the value if it has any meaning
        if (value >= MinValue)
        {
            if (minor)
            {
                if (IsValueWrapAllowed && value - MinorDelta < MinValue)
                {
                    value = MaxValue;
                }
                else
                {
                    value -= MinorDelta;
                }
            }
            else
            {
                if (IsValueWrapAllowed && value - MajorDelta < MinValue)
                {
                    value = MaxValue;
                }
                else
                {
                    value -= MajorDelta;
                }
            }
        }

        Value = value;
    }

    private void CoerceValueToBounds(ref decimal value)
    {
        if (value < MinValue)
        {
            value = MinValue;
        }
        else if (value > MaxValue)
        {
            value = MaxValue;
        }
    }

    // Visual Assist

    private void TextBoxOnLostFocus(object sender, RoutedEventArgs routedEventArgs)
    {
        Value = ParseStringToDecimal(TextBox.Text);
    }

    private void ButtonOnPreviewMouseRightButtonDown(object sender, MouseButtonEventArgs mouseButtonEventArgs)
    {
        Value = 0;
    }

    private void TextBoxOnPreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs mouseButtonEventArgs)
    {
        if (IsAutoSelectionActive)
        {
            TextBox.SelectAll();
        }
    }

    private void RemoveFocus()
    {
        // Passes focus here and then just deletes it
        Focusable = true;
        Focus();
        Focusable = false;
    }

    private void CancelChanges()
    {
        TextBox.Undo();
    }

    // Extended

    private decimal ParseStringToDecimal(string source)
    {
        decimal.TryParse(source, out decimal value);

        return value;
    }

    public int GetDecimalPlacesCount(string valueString)
    {
        return valueString.SkipWhile(c => c.ToString(Culture)
                != Culture.NumberFormat.NumberDecimalSeparator).Skip(1).Count();
    }

    private decimal TruncateValue(string valueString, int decimalPlaces)
    {
        var endPoint = valueString.Length - (decimalPlaces - DecimalPlaces);
        var tempValueString = valueString.Substring(0, endPoint);

        return decimal.Parse(tempValueString, Culture);
    }
}