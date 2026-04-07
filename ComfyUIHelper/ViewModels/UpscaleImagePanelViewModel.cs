using ComfyUIHelper.Models;
using Prism.Mvvm;

namespace ComfyUIHelper.ViewModels
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class UpscaleImagePanelViewModel : BindableBase
    {
        private string positivePrompt = "best quality, extremely detailed";
        private string negativePrompt = "bad quality, low detail,";
        private double denoise = 0.6;

        [ComfyUiSchema(nameof(UpscaleImageSchema.PositivePrompt))]
        public string PositivePrompt { get => positivePrompt; set => SetProperty(ref positivePrompt, value); }

        [ComfyUiSchema(nameof(UpscaleImageSchema.NegativePrompt))]
        public string NegativePrompt { get => negativePrompt; set => SetProperty(ref negativePrompt, value); }

        [ComfyUiSchema(nameof(UpscaleImageSchema.Denoise))]
        public double Denoise { get => denoise; set => SetProperty(ref denoise, value); }
    }
}