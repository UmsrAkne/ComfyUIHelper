using System.Windows;
using System.Windows.Controls;
using ComfyUIHelper.Utils;
using ComfyUIHelper.ViewModels;

namespace ComfyUIHelper.Controls
{
    public partial class FileSuggestBox : UserControl
    {
        private readonly FileSuggestBoxViewModel vm;

        public readonly static DependencyProperty SelectedPathProperty =
            DependencyProperty.Register(
                nameof(SelectedPath),
                typeof(string),
                typeof(FileSuggestBox),
                new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public readonly static DependencyProperty SourceDirectoryPathProperty =
            DependencyProperty.Register(
                nameof(SourceDirectoryPath),
                typeof(string),
                typeof(FileSuggestBox),
                new PropertyMetadata(string.Empty, OnSourceDirectoryPathChanged));

        public readonly static DependencyProperty LabelProperty =
            DependencyProperty.Register(
                nameof(Label),
                typeof(string),
                typeof(FileSuggestBox),
                new PropertyMetadata(string.Empty));

        public FileSuggestBox()
        {
            InitializeComponent();

            vm = new FileSuggestBoxViewModel(string.Empty);
            InternalRoot.DataContext = vm;

            // 内部VMの変更を外部プロパティへ同期
            vm.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == nameof(vm.SelectedFile))
                {
                    SelectedPath = vm.SelectedFile;
                }
            };
        }

        public string Label { get => (string)GetValue(LabelProperty); set => SetValue(LabelProperty, value); }

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

        private static void OnSourceDirectoryPathChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is FileSuggestBox control && e.NewValue is string newPath)
            {
                // 内部のViewModelにパスを伝える
                // ※ViewModel側にソースディレクトリを更新するメソッドやプロパティがある前提
                control.vm.SourceDirectoryPath = newPath;
                Logger.Log($"SourceDirectoryPath changed: {newPath}");
            }
        }
    }
}