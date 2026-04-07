using System.Diagnostics;
using ComfyUIHelper.Utils;
using Prism.Mvvm;

namespace ComfyUIHelper.ViewModels;

public class MainWindowViewModel : BindableBase
{
    private readonly AppVersionInfo appVersionInfo = new();

    public MainWindowViewModel()
    {
        SetupDummyData();
    }

    public string Title => appVersionInfo.Title;

    public IWorkflowPanel CurrentWorkFlow { get; } = new UpscaleImagePanelViewModel();

    [Conditional("DEBUG")]
    private void SetupDummyData()
    {
    }
}