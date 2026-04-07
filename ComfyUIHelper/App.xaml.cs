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
    }
}