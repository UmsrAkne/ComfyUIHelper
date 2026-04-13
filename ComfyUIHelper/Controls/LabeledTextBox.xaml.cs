using System.Windows;
using System.Windows.Controls;

namespace ComfyUIHelper.Controls
{
    public partial class LabeledTextBox : UserControl
    {
        public readonly static DependencyProperty TextProperty =
            DependencyProperty.Register(
                nameof(Text),
                typeof(string),
                typeof(LabeledTextBox),
                new PropertyMetadata("Text"));

        public readonly static DependencyProperty LabelProperty =
            DependencyProperty.Register(
                nameof(Label),
                typeof(string),
                typeof(LabeledTextBox),
                new PropertyMetadata("Label"));

        public LabeledTextBox()
        {
            InitializeComponent();
        }

        public string Label { get => (string)GetValue(LabelProperty); set => SetValue(LabelProperty, value); }

        public string Text { get => (string)GetValue(TextProperty); set => SetValue(TextProperty, value); }
    }
}