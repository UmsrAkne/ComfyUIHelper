using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace ComfyUIHelper.Controls
{
    public partial class DoubleNumericInput : UserControl
    {
        public readonly static DependencyProperty LabelProperty =
            DependencyProperty.Register(
                nameof(Label),
                typeof(string),
                typeof(DoubleNumericInput),
                new PropertyMetadata(string.Empty));

        public readonly static DependencyProperty ValueProperty =
            DependencyProperty.Register(
                nameof(Value),
                typeof(double),
                typeof(DoubleNumericInput),
                new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public readonly static DependencyProperty StepProperty =
            DependencyProperty.Register(
                nameof(Step),
                typeof(double),
                typeof(DoubleNumericInput),
                new PropertyMetadata(0.01));

        public readonly static DependencyProperty MaxValueProperty =
            DependencyProperty.Register(
                nameof(MaxValue),
                typeof(double),
                typeof(DoubleNumericInput),
                new PropertyMetadata(1.0));

        public readonly static DependencyProperty MinValueProperty =
            DependencyProperty.Register(
                nameof(MinValue),
                typeof(double),
                typeof(DoubleNumericInput),
                new PropertyMetadata(0.0));

        public DoubleNumericInput()
        {
            InitializeComponent();
        }

        public double Step { get => (double)GetValue(StepProperty); set => SetValue(StepProperty, value); }

        public string Label { get => (string)GetValue(LabelProperty); set => SetValue(LabelProperty, value); }

        public double Value
        {
            get => (double)GetValue(ValueProperty);
            set => SetValue(ValueProperty, value);
        }

        public double MinValue { get => (double)GetValue(MinValueProperty); set => SetValue(MinValueProperty, value); }

        public double MaxValue { get => (double)GetValue(MaxValueProperty); set => SetValue(MaxValueProperty, value); }

        private void UpButton_Click(object sender, RoutedEventArgs e)
        {
            UpdateValue(Step);
        }

        private void DownButton_Click(object sender, RoutedEventArgs e)
        {
            UpdateValue(-Step);
        }

        private void UpdateValue(double delta)
        {
            // 浮動小数点誤差を防ぐために Math.Round を噛ませる
            // Step が 0.01 なら 小数点第2位まで、といった動的な判定も可能ですが
            // 一旦 3〜4桁程度で丸めるのが安全です。
            var newValue = Value + delta;
            newValue = Math.Clamp(newValue, MinValue, MaxValue);

            Value = Math.Round(newValue, 3);
        }

        private void ValueTextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Up)
            {
                UpdateValue(Step);
                e.Handled = true; // TextBox 本来のフォーカス移動などを抑制
            }
            else if (e.Key == Key.Down)
            {
                UpdateValue(-Step);
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