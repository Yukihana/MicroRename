using System.Windows;

namespace CSX.Wpf.CustomControls;

public partial class NumericUpDown
{
    // Property: IsDecimalPointDynamic

    public static readonly DependencyProperty IsDecimalPointDynamicProperty
        = DependencyProperty.Register("IsDecimalPointDynamic", typeof(bool), typeof(NumericUpDown), new PropertyMetadata(false));

    public bool IsDecimalPointDynamic
    {
        get { return (bool)GetValue(IsDecimalPointDynamicProperty); }
        set { SetValue(IsDecimalPointDynamicProperty, value); }
    }

    // Property : IsThousandSeparatorVisible

    public static readonly DependencyProperty IsThousandSeparatorVisibleProperty
        = DependencyProperty.Register("IsThousandSeparatorVisible", typeof(bool), typeof(NumericUpDown), new PropertyMetadata(false, OnIsThousandSeparatorVisibleChanged));

    public bool IsThousandSeparatorVisible
    {
        get { return (bool)GetValue(IsThousandSeparatorVisibleProperty); }
        set { SetValue(IsThousandSeparatorVisibleProperty, value); }
    }

    private static void OnIsThousandSeparatorVisibleChanged(DependencyObject element, DependencyPropertyChangedEventArgs e)
    {
        var control = (NumericUpDown)element;

        control.InvalidateProperty(ValueProperty);
    }

    // Property: IsAutoSelectionActive

    public static readonly DependencyProperty IsAutoSelectionActiveProperty
        = DependencyProperty.Register("IsAutoSelectionActive", typeof(bool), typeof(NumericUpDown), new PropertyMetadata(false));

    public bool IsAutoSelectionActive
    {
        get { return (bool)GetValue(IsAutoSelectionActiveProperty); }
        set { SetValue(IsAutoSelectionActiveProperty, value); }
    }

    // Property : IsValueWrapAllowed

    public static readonly DependencyProperty IsValueWrapAllowedProperty =
        DependencyProperty.Register("IsValueWrapAllowed", typeof(bool), typeof(NumericUpDown), new PropertyMetadata(false));

    public bool IsValueWrapAllowed
    {
        get { return (bool)GetValue(IsValueWrapAllowedProperty); }
        set { SetValue(IsValueWrapAllowedProperty, value); }
    }
}