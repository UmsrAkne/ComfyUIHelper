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
                new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnSelectedPathChanged));

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

        public readonly static DependencyProperty FilterExtensionsProperty =
            DependencyProperty.Register(
                nameof(FilterExtensions),
                typeof(string),
                typeof(FileSuggestBox),
                new PropertyMetadata(string.Empty, OnFilterExtensionsChanged));

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

        /// <summary>
        /// フィルタリングする拡張子を指定します。
        /// </summary>
        /// <remarks>
        /// ドットを除いた拡張子を指定します。複数指定はカンマ区切りです（例: "safetensors,ckpt"）。
        /// </remarks>
        public string FilterExtensions
        {
            get => (string)GetValue(FilterExtensionsProperty);
            set => SetValue(FilterExtensionsProperty, value);
        }

        private static void OnSourceDirectoryPathChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is FileSuggestBox control && e.NewValue is string newPath)
            {
                // 内部のViewModelにパスを伝える
                control.vm.SourceDirectoryPath = newPath;
                Logger.Log($"SourceDirectoryPath changed: {newPath}");
            }
        }

        private static void OnFilterExtensionsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is FileSuggestBox control && e.NewValue is string newExtensions)
            {
                // 内部のViewModelにフィルターを伝える
                control.vm.FilterExtensions = newExtensions;
                Logger.Log($"FilterExtensions changed: {newExtensions}");
            }
        }

        private static void OnSelectedPathChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is FileSuggestBox control && control.vm != null)
            {
                var newValue = e.NewValue as string;

                // 循環参照（無限ループ）を防ぐため、値が本当に違う時だけセット
                if (control.vm.SearchText != newValue)
                {
                    // VM の SearchText を更新
                    // これにより、TextBox の表示が Json からの値に切り替わります
                    control.vm.SearchText = newValue;

                    // ついでに SelectedFile も更新して、内部状態を整合させる
                    // リストアイテムの中に項目が存在しなくても SearchText への入力は可能です
                    control.vm.SelectedFile = newValue;
                }
            }
        }
    }
}