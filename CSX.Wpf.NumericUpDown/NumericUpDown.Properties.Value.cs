using System.Windows;

namespace CSX.Wpf.CustomControls;

public partial class NumericUpDown
{
    // Property : Value

    public static readonly DependencyProperty ValueProperty
        = DependencyProperty.Register("Value", typeof(decimal), typeof(NumericUpDown), new PropertyMetadata(0m, OnValueChanged, CoerceValue));

    public decimal Value
    {
        get { return (decimal)GetValue(ValueProperty); }
        set { SetValue(ValueProperty, value); }
    }

    private static void OnValueChanged(DependencyObject element, DependencyPropertyChangedEventArgs e)
    {
        var control = (NumericUpDown)element;

        if (control.TextBox != null)
        {
            control.TextBox.UndoLimit = 0;
            control.TextBox.UndoLimit = 1;
        }
    }

    private static object CoerceValue(DependencyObject element, object baseValue)
    {
        var control = (NumericUpDown)element;
        var value = (decimal)baseValue;

        control.CoerceValueToBounds(ref value);

        // Get the text representation of Value
        var valueString = value.ToString(control.Culture);

        // Count all decimal places
        var decimalPlaces = control.GetDecimalPlacesCount(valueString);

        if (decimalPlaces > control.DecimalPlaces)
        {
            if (control.IsDecimalPointDynamic)
            {
                // Assigning DecimalPlaces will coerce the number
                control.DecimalPlaces = decimalPlaces;

                // If the specified number of decimal places is still too much
                if (decimalPlaces > control.DecimalPlaces)
                {
                    value = control.TruncateValue(valueString, control.DecimalPlaces);
                }
            }
            else
            {
                // Remove all overflowing decimal places
                value = control.TruncateValue(valueString, decimalPlaces);
            }
        }
        else if (control.IsDecimalPointDynamic)
        {
            control.DecimalPlaces = decimalPlaces;
        }

        // Change formatting based on this flag
        if (control.IsThousandSeparatorVisible)
        {
            if (control.TextBox != null)
            {
                control.TextBox.Text = value.ToString("N", control.Culture);
            }
        }
        else
        {
            if (control.TextBox != null)
            {
                control.TextBox.Text = value.ToString("F", control.Culture);
            }
        }

        return value;
    }

    // Property : MaxValue

    public static readonly DependencyProperty MaxValueProperty =
        DependencyProperty.Register("MaxValue", typeof(decimal), typeof(NumericUpDown), new PropertyMetadata(1000000000m, OnMaxValueChanged, CoerceMaxValue));

    public decimal MaxValue
    {
        get { return (decimal)GetValue(MaxValueProperty); }
        set { SetValue(MaxValueProperty, value); }
    }

    private static void OnMaxValueChanged(DependencyObject element, DependencyPropertyChangedEventArgs e)
    {
        var control = (NumericUpDown)element;
        var maxValue = (decimal)e.NewValue;

        // If maxValue steps over MinValue, shift it
        if (maxValue < control.MinValue)
        {
            control.MinValue = maxValue;
        }

        if (maxValue <= control.Value)
        {
            control.Value = maxValue;
        }
    }

    private static object CoerceMaxValue(DependencyObject element, object baseValue)
    {
        var maxValue = (decimal)baseValue;
        return maxValue;
    }

    // Property : MinValue

    public static readonly DependencyProperty MinValueProperty =
        DependencyProperty.Register("MinValue", typeof(decimal), typeof(NumericUpDown), new PropertyMetadata(0m, OnMinValueChanged, CoerceMinValue));

    public decimal MinValue
    {
        get { return (decimal)GetValue(MinValueProperty); }
        set { SetValue(MinValueProperty, value); }
    }

    private static void OnMinValueChanged(DependencyObject element, DependencyPropertyChangedEventArgs e)
    {
        var control = (NumericUpDown)element;
        var minValue = (decimal)e.NewValue;

        // If minValue steps over MaxValue, shift it
        if (minValue > control.MaxValue)
        {
            control.MaxValue = minValue;
        }

        if (minValue >= control.Value)
        {
            control.Value = minValue;
        }
    }

    private static object CoerceMinValue(DependencyObject element, object baseValue)
    {
        var minValue = (decimal)baseValue;
        return minValue;
    }
}