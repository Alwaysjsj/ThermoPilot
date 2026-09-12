using Core.Common;
using System.Collections.ObjectModel;
using System.Text.Json;

namespace Core.Recipe
{
    public class RecipeData : PropertyChangedBase
    {
        private Dictionary<string, string> _header = [];

        public Dictionary<string, string> Header
        {
            get
            {
                return _header;
            }

            set
            {
                _header = value;
                NotifyOfPropertyChange(nameof(Header));
            }
        }

        private ObservableCollection<Dictionary<string, string>> _step = [];

        public ObservableCollection<Dictionary<string, string>> Step
        {
            get
            {
                return _step;
            }

            set
            {
                _step = value;
                NotifyOfPropertyChange(nameof(Step));
            }
        }

        private Dictionary<string, string> _config = [];

        public Dictionary<string, string> Config
        {
            get
            {
                return _config;
            }

            set
            {
                _config = value;
                NotifyOfPropertyChange(nameof(Config));
            }
        }
        public string ToJsonString()
        {
            JsonSerializerOptions options = new JsonSerializerOptions
            {
                WriteIndented = true
            };

            return JsonSerializer.Serialize(this, options);
        }

        public static RecipeData? FromJsonString(string recipeContent)
        {
            if (string.IsNullOrEmpty(recipeContent))
                return new RecipeData();

            return JsonSerializer.Deserialize<RecipeData>(recipeContent);
        }

        public void Copy(RecipeData? source)
        {
            if (source == null)
                return;

            Header.Clear();

            foreach (var item in source.Header)
            {
                Header.Add(item.Key, item.Value);
            }

            Step.Clear();

            foreach (var item in source.Step)
            {
                Step.Add(item);
            }

            Config.Clear();

            foreach (var item in source.Config)
            {
                Config.Add(item.Key, item.Value);
            }
        }
    }
}