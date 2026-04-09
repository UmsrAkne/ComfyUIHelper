using System;
using System.Diagnostics;
using System.Windows;
using ComfyUIHelper.Utils;
using ComfyUIHelper.Views;
using Prism.Ioc;

namespace ComfyUIHelper
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            Logger.Initialize(System.AppContext.BaseDirectory);
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            // バインディングエラーのトレースを監視
            PresentationTraceSources.DataBindingSource.Listeners.Add(new BindingErrorTraceListener());
            PresentationTraceSources.DataBindingSource.Switch.Level = SourceLevels.Error;

            base.OnStartup(e);
        }

        private class BindingErrorTraceListener : TraceListener
        {
            public override void Write(string message) => Console.Write(message);

            public override void WriteLine(string message)
            {
                // 本来はコンソールに表示されない BindingError などの内部エラーを表示する。
                var originalColor = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"[WPF Binding Error] {message}");
                Console.ForegroundColor = originalColor;

                // 例外が出たら即座にアプリを落とす場合は以下のコードを使う
                // throw new Exception($"Binding Error: {message}");
            }
        }
    }
}