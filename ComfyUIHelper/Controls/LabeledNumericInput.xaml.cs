using System.Windows;
using System.Windows.Controls;

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
                new PropertyMetadata(0));

        public readonly static DependencyProperty StepsProperty =
            DependencyProperty.Register(
                nameof(Steps),
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
    }
}