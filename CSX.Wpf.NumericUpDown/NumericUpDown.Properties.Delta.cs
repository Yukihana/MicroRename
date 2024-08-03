using System.Windows;

namespace CSX.Wpf.CustomControls;

public partial class NumericUpDown
{
    // Property : MinorDelta

    public static readonly DependencyProperty MinorDeltaProperty =
        DependencyProperty.Register("MinorDelta", typeof(decimal), typeof(NumericUpDown), new PropertyMetadata(1m, OnMinorDeltaChanged, CoerceMinorDelta));

    public decimal MinorDelta
    {
        get { return (decimal)GetValue(MinorDeltaProperty); }
        set { SetValue(MinorDeltaProperty, value); }
    }

    private static void OnMinorDeltaChanged(DependencyObject element, DependencyPropertyChangedEventArgs e)
    {
        var minorDelta = (decimal)e.NewValue;
        var control = (NumericUpDown)element;

        if (minorDelta > control.MajorDelta)
            control.MajorDelta = minorDelta;
    }

    private static object CoerceMinorDelta(DependencyObject element, object baseValue)
    {
        var minorDelta = (decimal)baseValue;

        return minorDelta;
    }

    // Property : MajorDelta

    public static readonly DependencyProperty MajorDeltaProperty =
        DependencyProperty.Register("MajorDelta", typeof(decimal), typeof(NumericUpDown), new PropertyMetadata(10m, OnMajorDeltaChanged, CoerceMajorDelta));

    public decimal MajorDelta
    {
        get { return (decimal)GetValue(MajorDeltaProperty); }
        set { SetValue(MajorDeltaProperty, value); }
    }

    private static void OnMajorDeltaChanged(DependencyObject element, DependencyPropertyChangedEventArgs e)
    {
        var majorDelta = (decimal)e.NewValue;
        var control = (NumericUpDown)element;

        if (majorDelta < control.MinorDelta)
            control.MinorDelta = majorDelta;
    }

    private static object CoerceMajorDelta(DependencyObject element, object baseValue)
    {
        var majorDelta = (decimal)baseValue;

        return majorDelta;
    }
}