using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using ComfyUIHelper.Utils;
using Prism.Mvvm;

namespace ComfyUIHelper.ViewModels
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class FileSuggestBoxViewModel : BindableBase
    {
        private string searchText;
        private bool isSuggestionOpen;
        private ObservableCollection<string> suggestions = new ();
        private string selectedFile;
        private string sourceDirectoryPath;
        private string filterExtensions;

        public FileSuggestBoxViewModel(string sourceDirectoryPath)
        {
            this.sourceDirectoryPath = sourceDirectoryPath;
        }

        public string SearchText
        {
            get => searchText;
            set
            {
                if (SetProperty(ref searchText, value))
                {
                    // 文字が入っていればポップアップを開き、検索を実行
                    IsSuggestionOpen = !string.IsNullOrWhiteSpace(value);
                    UpdateSuggestions(value);
                }
            }
        }

        public bool IsSuggestionOpen { get => isSuggestionOpen; set => SetProperty(ref isSuggestionOpen, value); }

        public ObservableCollection<string> Suggestions
        {
            get => suggestions;
            set => SetProperty(ref suggestions, value);
        }

        public string SelectedFile
        {
            get => selectedFile;
            set
            {
                if (SetProperty(ref selectedFile, value))
                {
                    // アイテムが選択されたら、それを検索テキストに反映
                    if (!string.IsNullOrEmpty(value))
                    {
                        // セッターの中で SearchText を更新することで、TextBox に値が入る
                        SearchText = value;

                        // 選択直後にポップアップを閉じる
                        IsSuggestionOpen = false;
                    }
                }
            }
        }

        public string SourceDirectoryPath
        {
            get => sourceDirectoryPath;
            set => SetProperty(ref sourceDirectoryPath, value);
        }

        public string FilterExtensions
        {
            get => filterExtensions;
            set => SetProperty(ref filterExtensions, value);
        }

        private void UpdateSuggestions(string value)
        {
            Suggestions.Clear();

            if (string.IsNullOrWhiteSpace(value) || !Directory.Exists(SourceDirectoryPath))
            {
                IsSuggestionOpen = false;
                return;
            }

            try
            {
                // 拡張子フィルターの準備
                var extensions = (FilterExtensions ?? string.Empty)
                    .Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                    .Select(e => "." + e.ToLowerInvariant())
                    .ToList();

                // ディレクトリ内を検索
                var filteredFiles = Directory.EnumerateFiles(SourceDirectoryPath, $"*{value}*", SearchOption.TopDirectoryOnly);

                if (extensions.Any())
                {
                    filteredFiles = filteredFiles.Where(file => extensions.Contains(Path.GetExtension(file).ToLowerInvariant()));
                }

                var result = filteredFiles
                    .Select(Path.GetFileName)
                    .Take(10); // 出すぎると邪魔なので上位10件くらいに絞る

                foreach (var file in result)
                {
                    Suggestions.Add(file);
                }

                // 候補が空ならポップアップを閉じる
                IsSuggestionOpen = Suggestions.Any();
            }
            catch (Exception ex)
            {
                Logger.Log($"ファイル検索中にエラー: {ex.Message}");
            }
        }
    }
}