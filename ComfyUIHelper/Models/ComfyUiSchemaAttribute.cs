using System;

namespace ComfyUIHelper.Models
{
    [AttributeUsage(AttributeTargets.Property)] // 「プロパティにだけ貼ることができる
    public class ComfyUiSchemaAttribute : Attribute
    {
        public ComfyUiSchemaAttribute(string fieldName) => FieldName = fieldName;

        public string FieldName { get; }
    }
}