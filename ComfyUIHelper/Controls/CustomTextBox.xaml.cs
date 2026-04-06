using System.Windows;
using System.Windows.Controls;

namespace ComfyUIHelper.Controls
{
    public partial class CustomTextBox : UserControl
    {
        public readonly static DependencyProperty HeaderProperty =
            DependencyProperty.Register(
                nameof(Header),
                typeof(string),
                typeof(CustomTextBox),
                new PropertyMetadata("Header"));

        public readonly static DependencyProperty TextProperty =
            DependencyProperty.Register(
                nameof(Text),
                typeof(string),
                typeof(CustomTextBox),
                new PropertyMetadata(string.Empty));

        public CustomTextBox()
        {
            InitializeComponent();
        }

        public string Text { get => (string)GetValue(TextProperty); set => SetValue(TextProperty, value); }

        public string Header { get => (string)GetValue(HeaderProperty); set => SetValue(HeaderProperty, value); }
    }
}