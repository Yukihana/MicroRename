﻿using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace CSX.Wpf.CustomControls;

/// <summary>
/// Follow steps 1a or 1b and then 2 to use this custom control in a XAML file.
///
/// Step 1a) Using this custom control in a XAML file that exists in the current project.
/// Add this XmlNamespace attribute to the root element of the markup file where it is
/// to be used:
///
///     xmlns:MyNamespace="clr-namespace:CherrySoft.Wpf.CustomControls"
///
///
/// Step 1b) Using this custom control in a XAML file that exists in a different project.
/// Add this XmlNamespace attribute to the root element of the markup file where it is
/// to be used:
///
///     xmlns:MyNamespace="clr-namespace:CherrySoft.Wpf.CustomControls;assembly=CherrySoft.Wpf.CustomControls"
///
/// You will also need to add a project reference from the project where the XAML file lives
/// to this project and Rebuild to avoid compilation errors:
///
///     Right click on the target project in the Solution Explorer and
///     "Add Reference"->"Projects"->[Select this project]
///
///
/// Step 2)
/// Go ahead and use your control in the XAML file.
///
///     <MyNamespace:CustomControl1/>
///
/// </summary>
[TemplatePart(Name = "PART_TextBox", Type = typeof(TextBox))]
[TemplatePart(Name = "PART_IncreaseButton", Type = typeof(RepeatButton))]
[TemplatePart(Name = "PART_DecreaseButton", Type = typeof(RepeatButton))]
public partial class NumericUpDown : Control
{
    protected readonly CultureInfo Culture;

    private void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        => InvalidateProperty(ValueProperty);

    // Ctors

    static NumericUpDown()
        => DefaultStyleKeyProperty.OverrideMetadata(typeof(NumericUpDown), new FrameworkPropertyMetadata(typeof(NumericUpDown)));

    public NumericUpDown()
    {
        Culture = (CultureInfo)CultureInfo.CurrentCulture.Clone();
        Culture.NumberFormat.NumberDecimalDigits = DecimalPlaces;

        Loaded += OnLoaded;
    }
}