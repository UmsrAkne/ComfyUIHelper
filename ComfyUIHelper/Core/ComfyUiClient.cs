using System.Text.Json.Nodes;
using ComfyUIHelper.Utils;

namespace ComfyUIHelper.Core
{
    using System;
    using System.IO;
    using System.Net.Http;
    using System.Text;
    using System.Text.Json;
    using System.Threading.Tasks;

    public class ComfyUiClient
    {
        private const string ServerAddress = "127.0.0.1:8188"; // 適宜変更
        private const string ClientId = "unique-Client-id-123"; // 任意のID
        private readonly static HttpClient Client = new ();

        /// <summary>
        /// API用のWorkflow JSONファイルを読み込みます
        /// </summary>
        public static JsonNode LoadPromptJson(string path)
        {
            var jsonString = File.ReadAllText(path);
            Logger.Log("workflow読み込み完了");
            return JsonNode.Parse(jsonString);
        }

        /// <summary>
        /// WorkflowをComfyUIの /prompt エンドポイントに送信します
        /// </summary>
        public static async Task<string> ComfyUiApiPrompt(JsonNode promptData)
        {
            // 送信用データ構造の作成
            var payloadObj = new
            {
                client_id = ClientId,
                prompt = promptData,
            };

            var jsonPayload = JsonSerializer.Serialize(payloadObj);
            var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

            try
            {
                var response = await Client.PostAsync($"http://{ServerAddress}/prompt", content);
                response.EnsureSuccessStatusCode();

                var resStr = await response.Content.ReadAsStringAsync();
                Logger.Log($"サーバー応答: {resStr}");

                using var doc = JsonDocument.Parse(resStr);
                var promptId = doc.RootElement.GetProperty("prompt_id").GetString();
                Logger.Log($"prompt_id: {promptId}");
                return promptId;
            }
            catch (Exception e)
            {
                Logger.Log($"workflow送信エラー: {e.Message}");
                return null;
            }
        }
    }
}