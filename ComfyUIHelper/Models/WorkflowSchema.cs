namespace ComfyUIHelper.Models
{
    public class WorkflowSchema
    {
        // Loader / Encode 系（固有のプロパティ名を持つもの）
        public readonly static ComfyUiParameter LoadCheckPoint = new ("Api_LoadCheckPoint", "ckpt_name");
        public readonly static ComfyUiParameter LoadImage = new ("Api_LoadImage", "image");
        public readonly static ComfyUiParameter NegativePrompt = new ("Api_NegativePrompt", "text");
        public readonly static ComfyUiParameter PositivePrompt = new ("Api_PositivePrompt", "text");
        public readonly static ComfyUiParameter LoadUpScaleModel = new ("Api_LoadUpScaleModel", "model_name");
        public readonly static ComfyUiParameter LoadVae = new ("Api_LoadVAE", "vae_name");

        // Lora 系（名前と強度、どちらを操作するかでキーが変わります。下記は名前の変更例）
        public readonly static ComfyUiParameter LoadLora1 = new ("Api_LoadLora1", "lora_name");
        public readonly static ComfyUiParameter LoadLora2 = new ("Api_LoadLora2", "lora_name");
        public readonly static ComfyUiParameter LoadLora3 = new ("Api_LoadLora3", "lora_name");

        // Primitive 系（JSON上で class_type が Primitive... になっているもの。キーは一律 "value"）
        public readonly static ComfyUiParameter Seed = new ("Api_Seed", "value");
        public readonly static ComfyUiParameter IssueNumber = new ("Api_IssueNumber", "value");
        public readonly static ComfyUiParameter FileNamePrefix = new ("Api_FileNamePrefix", "value");
        public readonly static ComfyUiParameter UpScaleBy = new ("Api_UpScaleBy", "value");
        public readonly static ComfyUiParameter Denoise = new ("Api_Denoise", "value");
    }
}