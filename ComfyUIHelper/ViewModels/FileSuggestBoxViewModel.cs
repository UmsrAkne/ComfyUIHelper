using System.Collections.ObjectModel;
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
        private readonly string sourceDirectoryPath;

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
            set => SetProperty(ref selectedFile, value);
        }

        private void UpdateSuggestions(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return;
            }

            // Todo: sourceDirectoryPathからファイルを検索してsuggestionsにセットする
            Logger.Log("UpdateSuggestionsAsync");
        }
    }
}