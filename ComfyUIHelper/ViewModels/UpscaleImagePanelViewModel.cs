using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text.Json.Nodes;
using ComfyUIHelper.Core;
using ComfyUIHelper.Models;
using ComfyUIHelper.Utils;
using CommunityToolkit.Mvvm.Input;
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
        private AsyncRelayCommand sendRequestCommand;

        public AsyncRelayCommand SendRequestAsyncCommand =>
            sendRequestCommand ??= new AsyncRelayCommand(async () =>
            {
                var workflow = ComfyUiClient.LoadPromptJson(CurrentWorkflowPath);
                UpdateWorkflowValues(workflow);
                await ComfyUiClient.ComfyUiApiPrompt(workflow);
            });

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
            set
            {
                if (SetProperty(ref currentWorkflowPath, value))
                {
                    if (string.IsNullOrWhiteSpace(value))
                    {
                        return;
                    }

                    var workflow = ComfyUiClient.LoadPromptJson(value);
                    LoadValuesFromWorkflow(workflow);
                }
            }
        }

        public string CurrentImagePath { get; set; } = string.Empty;

        public string ModelDirectoryPath { get; set; } = PathHelper.GetModelDirectoryPath();

        public string LoraDirectoryPath { get; set; } = PathHelper.GetLoraDirectoryPath();

        public string UpscalerDirectoryPath { get; set; } = PathHelper.GetUpscalerDirectoryPath();

        public string VaeDirectoryPath { get; set; } = PathHelper.GetVaeDirectoryPath();

        private void UpdateWorkflowValues(JsonNode workflow)
        {
            var properties = GetType().GetProperties();
            var workflowObject = workflow.AsObject();

            foreach (var prop in properties)
            {
                // 1. 属性から SchemaPropertyName (例: "LoadCheckPoint") を取得
                var attribute = prop.GetCustomAttributes(typeof(ComfyUiSchemaAttribute), true)
                    .FirstOrDefault() as ComfyUiSchemaAttribute;

                if (attribute == null)
                {
                    continue;
                }

                // 2. Schema クラスから ComfyUiParameter (NodeId = "Api_LoadCheckPoint") を取得
                var fieldInfo =
                    typeof(UpscaleImageSchema).GetField(attribute.FieldName, BindingFlags.Public | BindingFlags.Static);

                if (fieldInfo?.GetValue(null) is not ComfyUiParameter parameter)
                {
                    continue;
                }

                var value = prop.GetValue(this);
                if (value == null)
                {
                    continue;
                }

                try
                {
                    // parameter.NodeId (例: "Api_LoadCheckPoint") を
                    // JSON内の _meta -> title から探し出して、実際の ID ("4" など) を特定する
                    var actualNodeId = workflowObject
                        .FirstOrDefault(node => node.Value?["_meta"]?["title"]?.ToString() == parameter.NodeTitle)
                        .Key;

                    if (actualNodeId == null)
                    {
                        Logger.Log($"[Skip] タイトル '{parameter.NodeTitle}' を持つノードが見つかりません");
                        continue;
                    }

                    // 実際の ID を使って値を書き換え
                    workflow[actualNodeId] !["inputs"] ![parameter.InputKey] = JsonValue.Create(value);
                    Logger.Log($"[Success] Updated {parameter.NodeTitle} (ID:{actualNodeId}) with {value}");
                }
                catch (Exception ex)
                {
                    Logger.Log($"Failed to update node {parameter.NodeTitle}: {ex.Message}");
                }
            }
        }

        private void LoadValuesFromWorkflow(JsonNode workflow)
        {
            var properties = GetType().GetProperties();
            var workflowObject = workflow.AsObject();

            foreach (var prop in properties)
            {
                // 1. 属性の取得
                var attribute = prop.GetCustomAttributes(typeof(ComfyUiSchemaAttribute), true)
                    .FirstOrDefault() as ComfyUiSchemaAttribute;

                if (attribute == null)
                {
                    continue;
                }

                // 2. スキーマ情報の取得
                var fieldInfo =
                    typeof(UpscaleImageSchema).GetField(attribute.FieldName, BindingFlags.Public | BindingFlags.Static);

                if (fieldInfo?.GetValue(null) is not ComfyUiParameter parameter)
                {
                    continue;
                }

                try
                {
                    // 3. タイトルから実際のノードIDを探す
                    var actualNodeId = workflowObject
                        .FirstOrDefault(node => node.Value?["_meta"]?["title"]?.ToString() == parameter.NodeTitle)
                        .Key;

                    if (actualNodeId == null)
                    {
                        continue;
                    }

                    // 4. JSONから値を取得
                    var jsonValue = workflow[actualNodeId] !["inputs"]?[parameter.InputKey];
                    if (jsonValue == null)
                    {
                        continue;
                    }

                    // 5. プロパティの型に合わせて変換してセット
                    object convertedValue = null;
                    var targetType = prop.PropertyType;

                    if (targetType == typeof(string))
                    {
                        convertedValue = jsonValue.ToString();
                    }
                    else if (targetType == typeof(int))
                    {
                        convertedValue = (int)jsonValue;
                    }
                    else if (targetType == typeof(double))
                    {
                        convertedValue = (double)jsonValue;
                    }

                    if (convertedValue != null)
                    {
                        prop.SetValue(this, convertedValue);
                        Debug.WriteLine($"[Load] {prop.Name} <= {convertedValue}");
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Failed to load value for {prop.Name}: {ex.Message}");
                }
            }
        }
    }
}