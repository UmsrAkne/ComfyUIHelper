using System.Windows;
using System.Windows.Controls;

namespace ComfyUIHelper.Controls
{
    public partial class FileDropArea : UserControl
    {
        public readonly static DependencyProperty FilePathProperty =
            DependencyProperty.Register(
                nameof(FilePath),
                typeof(string),
                typeof(FileDropArea),
                new FrameworkPropertyMetadata(string.Empty, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public FileDropArea()
        {
            InitializeComponent();
        }

        public string FilePath { get => (string)GetValue(FilePathProperty); set => SetValue(FilePathProperty, value); }

        private void OnDragOver(object sender, DragEventArgs e)
        {
            e.Effects = e.Data.GetDataPresent(DataFormats.FileDrop)
                ? DragDropEffects.Copy
                : DragDropEffects.None;

            e.Handled = true;
        }

        private void OnDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                var files = (string[])e.Data.GetData(DataFormats.FileDrop);
                if (files is { Length: > 0, })
                {
                    FilePath = files[0];
                }
            }

            e.Handled = true;
        }
    }
}