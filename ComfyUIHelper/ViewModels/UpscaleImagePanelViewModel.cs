using ComfyUIHelper.Models;
using ComfyUIHelper.Utils;
using Prism.Mvvm;

namespace ComfyUIHelper.ViewModels
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class UpscaleImagePanelViewModel : BindableBase, IWorkflowPanel
    {
        private string loadCheckPoint = string.Empty;
        private string loadImage = string.Empty;
        private string negativePrompt = "bad quality, low detail,";
        private string positivePrompt = "best quality, extremely detailed";
        private string loadUpScaleModel = string.Empty;
        private string loadVae = string.Empty;
        private string loadLora1 = string.Empty;
        private string loadLora2 = string.Empty;
        private string loadLora3 = string.Empty;
        private int seed = -1;
        private int issueNumber;
        private string fileNamePrefix = string.Empty;
        private double upScaleBy = 1.0;
        private double denoise = 0.6;
        private string currentWorkflowPath = string.Empty;

        [ComfyUiSchema(nameof(UpscaleImageSchema.LoadCheckPoint))]
        public string LoadCheckPoint { get => loadCheckPoint; set => SetProperty(ref loadCheckPoint, value); }

        [ComfyUiSchema(nameof(UpscaleImageSchema.LoadImage))]
        public string LoadImage { get => loadImage; set => SetProperty(ref loadImage, value); }

        [ComfyUiSchema(nameof(UpscaleImageSchema.NegativePrompt))]
        public string NegativePrompt { get => negativePrompt; set => SetProperty(ref negativePrompt, value); }

        [ComfyUiSchema(nameof(UpscaleImageSchema.PositivePrompt))]
        public string PositivePrompt { get => positivePrompt; set => SetProperty(ref positivePrompt, value); }

        [ComfyUiSchema(nameof(UpscaleImageSchema.LoadUpScaleModel))]
        public string LoadUpScaleModel { get => loadUpScaleModel; set => SetProperty(ref loadUpScaleModel, value); }

        [ComfyUiSchema(nameof(UpscaleImageSchema.LoadVae))]
        public string LoadVae { get => loadVae; set => SetProperty(ref loadVae, value); }

        [ComfyUiSchema(nameof(UpscaleImageSchema.LoadLora1))]
        public string LoadLora1 { get => loadLora1; set => SetProperty(ref loadLora1, value); }

        [ComfyUiSchema(nameof(UpscaleImageSchema.LoadLora2))]
        public string LoadLora2 { get => loadLora2; set => SetProperty(ref loadLora2, value); }

        [ComfyUiSchema(nameof(UpscaleImageSchema.LoadLora3))]
        public string LoadLora3 { get => loadLora3; set => SetProperty(ref loadLora3, value); }

        [ComfyUiSchema(nameof(UpscaleImageSchema.Seed))]
        public int Seed { get => seed; set => SetProperty(ref seed, value); }

        [ComfyUiSchema(nameof(UpscaleImageSchema.IssueNumber))]
        public int IssueNumber { get => issueNumber; set => SetProperty(ref issueNumber, value); }

        [ComfyUiSchema(nameof(UpscaleImageSchema.FileNamePrefix))]
        public string FileNamePrefix { get => fileNamePrefix; set => SetProperty(ref fileNamePrefix, value); }

        [ComfyUiSchema(nameof(UpscaleImageSchema.UpScaleBy))]
        public double UpScaleBy { get => upScaleBy; set => SetProperty(ref upScaleBy, value); }

        [ComfyUiSchema(nameof(UpscaleImageSchema.Denoise))]
        public double Denoise { get => denoise; set => SetProperty(ref denoise, value); }

        public string Header { get; set; } = "Upscale Image";

        public string CurrentWorkflowPath
        {
            get => currentWorkflowPath;
            set => SetProperty(ref currentWorkflowPath, value);
        }

        public string CurrentImagePath { get; set; } = string.Empty;

        public string ModelDirectoryPath { get; set; } = PathHelper.GetModelDirectoryPath();

        public string LoraDirectoryPath { get; set; } = PathHelper.GetLoraDirectoryPath();

        public string UpscalerDirectoryPath { get; set; } = PathHelper.GetUpscalerDirectoryPath();

        public string VaeDirectoryPath { get; set; } = PathHelper.GetVaeDirectoryPath();
    }
}