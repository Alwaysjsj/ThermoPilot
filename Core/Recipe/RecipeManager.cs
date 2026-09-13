using Core.Common;
using System.Collections.ObjectModel;

namespace Core.Recipe
{
    public class RecipeManager : Singleton<RecipeManager>
    {
        private readonly string _rootPath;

        private readonly RecipeNodeItem? _recipeNodeItem;

        public RecipeManager()
        {
            string rootFolder = Path.Combine(
                AppContext.BaseDirectory,
                "Recipes");

            DirectoryInfo directory =
                Directory.CreateDirectory(rootFolder);

            _rootPath = directory.FullName;

            _recipeNodeItem = new RecipeNodeItem(directory, null);
        }

        public string GetRootPath()
        {
            return _rootPath;
        }

        public void UpdateHeaderInfo(RecipeData? recipeData)
        {
            if (recipeData == null)
                return;

            recipeData.Header["ModifyUser"] =
                Environment.UserName;

            recipeData.Header["ModifyTime"] =
                DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }

        public void SaveRecipeData(string fullFilePath, string content)
        {
            if (string.IsNullOrWhiteSpace(fullFilePath))
                return;

            string? directory =
                Path.GetDirectoryName(fullFilePath);

            if (!string.IsNullOrEmpty(directory))
            {
                Directory.CreateDirectory(directory);
            }

            File.WriteAllText(fullFilePath, content);
        }

        public IEnumerable<string> GetConfigNames(string recipeType)
        {
            return [];
        }

        public ObservableCollection<RecipeNodeItem>? GetRecipes()
        {
            return _recipeNodeItem?.SubNodes;
        }

        public void InitTree(RecipeNodeItem? node)
        {
            if(node == null)
            {
                return;
            }

            node.IsSelected = false;

            if(node.IsSelected != null)
            {
                foreach(var child in node.SubNodes)
                {
                    InitTree(child);
                }
            }
        }
    }
}