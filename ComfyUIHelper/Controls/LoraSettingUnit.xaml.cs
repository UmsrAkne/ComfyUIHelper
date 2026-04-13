using System.Windows;
using System.Windows.Controls;

namespace ComfyUIHelper.Controls
{
    public partial class LoraSettingUnit : UserControl
    {
        public readonly static DependencyProperty SelectedPathProperty =
            DependencyProperty.Register(
                nameof(SelectedPath),
                typeof(string),
                typeof(LoraSettingUnit),
                new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public readonly static DependencyProperty SourceDirectoryPathProperty =
            DependencyProperty.Register(
                nameof(SourceDirectoryPath),
                typeof(string),
                typeof(LoraSettingUnit),
                new PropertyMetadata(string.Empty));

        public readonly static DependencyProperty ValueProperty =
            DependencyProperty.Register(
                nameof(Value),
                typeof(double),
                typeof(LoraSettingUnit),
                new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public LoraSettingUnit()
        {
            InitializeComponent();
        }

        public string SelectedPath
        {
            get => (string)GetValue(SelectedPathProperty);
            set => SetValue(SelectedPathProperty, value);
        }

        public string SourceDirectoryPath
        {
            get => (string)GetValue(SourceDirectoryPathProperty);
            set => SetValue(SourceDirectoryPathProperty, value);
        }

        public double Value
        {
            get => (double)GetValue(ValueProperty);
            set => SetValue(ValueProperty, value);
        }
    }
}