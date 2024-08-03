using System.Windows;

namespace CSX.Wpf.CustomControls;

public partial class NumericUpDown
{
    #region Property: DecimalPlaces

    public static readonly DependencyProperty DecimalPlacesProperty
        = DependencyProperty.Register("DecimalPlaces", typeof(int), typeof(NumericUpDown), new PropertyMetadata(0, OnDecimalPlacesChanged, CoerceDecimalPlaces));

    public int DecimalPlaces
    {
        get { return (int)GetValue(DecimalPlacesProperty); }
        set { SetValue(DecimalPlacesProperty, value); }
    }

    private static void OnDecimalPlacesChanged(DependencyObject element, DependencyPropertyChangedEventArgs e)
    {
        var control = (NumericUpDown)element;
        var decimalPlaces = (int)e.NewValue;

        control.Culture.NumberFormat.NumberDecimalDigits = decimalPlaces;

        if (control.IsDecimalPointDynamic)
        {
            control.IsDecimalPointDynamic = false;
            control.InvalidateProperty(ValueProperty);
            control.IsDecimalPointDynamic = true;
        }
        else
        {
            control.InvalidateProperty(ValueProperty);
        }
    }

    private static object CoerceDecimalPlaces(DependencyObject element, object baseValue)
    {
        var decimalPlaces = (int)baseValue;
        var control = (NumericUpDown)element;

        if (decimalPlaces < control.MinDecimalPlaces)
        {
            decimalPlaces = control.MinDecimalPlaces;
        }
        else if (decimalPlaces > control.MaxDecimalPlaces)
        {
            decimalPlaces = control.MaxDecimalPlaces;
        }

        return decimalPlaces;
    }

    #endregion Property: DecimalPlaces

    #region Property: MaxDecimalPlaces

    public static readonly DependencyProperty MaxDecimalPlacesProperty =
        DependencyProperty.Register("MaxDecimalPlaces", typeof(int), typeof(NumericUpDown), new PropertyMetadata(28, OnMaxDecimalPlacesChanged, CoerceMaxDecimalPlaces));

    public int MaxDecimalPlaces
    {
        get { return (int)GetValue(MaxDecimalPlacesProperty); }
        set { SetValue(MaxDecimalPlacesProperty, value); }
    }

    private static void OnMaxDecimalPlacesChanged(DependencyObject element, DependencyPropertyChangedEventArgs e)
    {
        var control = (NumericUpDown)element;

        control.InvalidateProperty(DecimalPlacesProperty);
    }

    private static object CoerceMaxDecimalPlaces(DependencyObject element, object baseValue)
    {
        var maxDecimalPlaces = (int)baseValue;
        var control = (NumericUpDown)element;

        if (maxDecimalPlaces > 28)
        {
            maxDecimalPlaces = 28;
        }
        else if (maxDecimalPlaces < 0)
        {
            maxDecimalPlaces = 0;
        }
        else if (maxDecimalPlaces < control.MinDecimalPlaces)
        {
            control.MinDecimalPlaces = maxDecimalPlaces;
        }

        return maxDecimalPlaces;
    }

    #endregion Property: MaxDecimalPlaces

    #region Property: MinDecimalPlaces

    public static readonly DependencyProperty MinDecimalPlacesProperty =
        DependencyProperty.Register("MinDecimalPlaces", typeof(int), typeof(NumericUpDown), new PropertyMetadata(0, OnMinDecimalPlacesChanged, CoerceMinDecimalPlaces));

    public int MinDecimalPlaces
    {
        get { return (int)GetValue(MinDecimalPlacesProperty); }
        set { SetValue(MinDecimalPlacesProperty, value); }
    }

    private static void OnMinDecimalPlacesChanged(DependencyObject element, DependencyPropertyChangedEventArgs e)
    {
        var control = (NumericUpDown)element;

        control.InvalidateProperty(DecimalPlacesProperty);
    }

    private static object CoerceMinDecimalPlaces(DependencyObject element, object baseValue)
    {
        var minDecimalPlaces = (int)baseValue;
        var control = (NumericUpDown)element;

        if (minDecimalPlaces < 0)
        {
            minDecimalPlaces = 0;
        }
        else if (minDecimalPlaces > 28)
        {
            minDecimalPlaces = 28;
        }
        else if (minDecimalPlaces > control.MaxDecimalPlaces)
        {
            control.MaxDecimalPlaces = minDecimalPlaces;
        }

        return minDecimalPlaces;
    }

    #endregion Property: MinDecimalPlaces
}