using System;

namespace ComfyUIHelper.Models
{
    public record ComfyUiParameter
    {
        public ComfyUiParameter(string nodeTitle, string inputKey, Type type)
        {
            NodeTitle = nodeTitle;
            InputKey = inputKey;
            Type = type;
        }

        public string NodeTitle { get; set; }

        public string InputKey { get; set; }

        public Type Type { get; set; }
    }
}