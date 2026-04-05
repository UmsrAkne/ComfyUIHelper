namespace ComfyUIHelper.Models
{
    public record ComfyUiParameter
    {
        public ComfyUiParameter(string nodeTitle, string inputKey)
        {
            NodeTitle = nodeTitle;
            InputKey = inputKey;
        }

        public string NodeTitle { get; set; }

        public string InputKey { get; set; }
    }
}