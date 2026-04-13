using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace ComfyUIHelper.Controls
{
    public partial class LabeledNumericInput : UserControl
    {
        public readonly static DependencyProperty LabelProperty =
            DependencyProperty.Register(
                nameof(Label),
                typeof(string),
                typeof(LabeledNumericInput),
                new PropertyMetadata(string.Empty));

        public readonly static DependencyProperty ValueProperty =
            DependencyProperty.Register(
                nameof(Value),
                typeof(int),
                typeof(LabeledNumericInput),
                new FrameworkPropertyMetadata(0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public readonly static DependencyProperty StepsProperty =
            DependencyProperty.Register(
                nameof(Steps),
                typeof(int),
                typeof(LabeledNumericInput),
                new PropertyMetadata(0));

        public readonly static DependencyProperty MaxValueProperty =
            DependencyProperty.Register(
                nameof(MaxValue),
                typeof(int),
                typeof(LabeledNumericInput),
                new PropertyMetadata(int.MaxValue));

        public readonly static DependencyProperty MinValueProperty =
            DependencyProperty.Register(
                nameof(MinValue),
                typeof(int),
                typeof(LabeledNumericInput),
                new PropertyMetadata(0));

        public LabeledNumericInput()
        {
            InitializeComponent();
        }

        public int Value { get => (int)GetValue(ValueProperty); set => SetValue(ValueProperty, value); }

        public string Label { get => (string)GetValue(LabelProperty); set => SetValue(LabelProperty, value); }

        public int Steps { get => (int)GetValue(StepsProperty); set => SetValue(StepsProperty, value); }

        public int MinValue { get => (int)GetValue(MinValueProperty); set => SetValue(MinValueProperty, value); }

        public int MaxValue { get => (int)GetValue(MaxValueProperty); set => SetValue(MaxValueProperty, value); }

        private void UpButton_Click(object sender, RoutedEventArgs e)
        {
            UpdateValue(Steps);
        }

        private void DownButton_Click(object sender, RoutedEventArgs e)
        {
            UpdateValue(-Steps);
        }

        private void UpdateValue(int delta)
        {
            var newValue = Value + delta;
            Value = Math.Clamp(newValue, MinValue, MaxValue);
        }

        private void ValueTextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Up)
            {
                UpdateValue(Steps);
                e.Handled = true; // TextBox 本来のフォーカス移動などを抑制
            }
            else if (e.Key == Key.Down)
            {
                UpdateValue(-Steps);
                e.Handled = true;
            }
            else if (e.Key == Key.Enter)
            {
                // Enterを押した時に即座にソースへ反映させる（LostFocusを待たない）
                var binding = BindingOperations.GetBindingExpression(ValueTextBox, TextBox.TextProperty);
                binding?.UpdateSource();
            }
        }
    }
}