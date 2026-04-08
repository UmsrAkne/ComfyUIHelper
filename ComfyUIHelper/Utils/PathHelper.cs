namespace ComfyUIHelper.Utils
{
    public static class PathHelper
    {
        public static string GetModelDirectoryPath()
        {
            var userDirectory = System.Environment.GetFolderPath(System.Environment.SpecialFolder.UserProfile);
            var modelDirectoryPath = System.IO.Path.Combine(userDirectory, "stable-diffusion-webui", "models", "Stable-diffusion");
            return System.IO.Directory.Exists(modelDirectoryPath) ? modelDirectoryPath : string.Empty;
        }

        public static string GetLoraDirectoryPath()
        {
            var userDirectory = System.Environment.GetFolderPath(System.Environment.SpecialFolder.UserProfile);
            var loraDirectoryPath = System.IO.Path.Combine(userDirectory, "stable-diffusion-webui", "models", "Lora");
            return System.IO.Directory.Exists(loraDirectoryPath) ? loraDirectoryPath : string.Empty;
        }

        public static string GetUpscalerDirectoryPath()
        {
            var userDirectory = System.Environment.GetFolderPath(System.Environment.SpecialFolder.UserProfile);
            var upscalerDirectoryPath = System.IO.Path.Combine(userDirectory, "stable-diffusion-webui", "models", "ESRGAN");
            return System.IO.Directory.Exists(upscalerDirectoryPath) ? upscalerDirectoryPath : string.Empty;
        }

        public static string GetVaeDirectoryPath()
        {
            var userDirectory = System.Environment.GetFolderPath(System.Environment.SpecialFolder.UserProfile);
            var vaeDirectoryPath = System.IO.Path.Combine(userDirectory, "stable-diffusion-webui", "models", "VAE");
            return System.IO.Directory.Exists(vaeDirectoryPath) ? vaeDirectoryPath : string.Empty;
        }
    }
}