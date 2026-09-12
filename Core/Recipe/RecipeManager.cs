using Core.Common;

namespace Core.Recipe
{
    public class RecipeManager : Singleton<RecipeManager>
    {
        private readonly string _rootPath;

        public RecipeManager()
        {
            string rootFolder = Path.Combine(
                AppContext.BaseDirectory,
                "Recipes");

            DirectoryInfo directory =
                Directory.CreateDirectory(rootFolder);

            _rootPath = directory.FullName;
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

    }
}