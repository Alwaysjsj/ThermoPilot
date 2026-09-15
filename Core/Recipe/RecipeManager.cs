using Core.Common;
using Core.Config;
using System.Collections.ObjectModel;

namespace Core.Recipe
{
    public partial class RecipeManager : Singleton<RecipeManager>
    {
        private readonly string _configPath =
            Path.Combine(
                AppContext.BaseDirectory,
                "Config",
                "RecipeConfig.json");

        private readonly string _rootFolderName =
            Path.Combine(
                AppContext.BaseDirectory,
                "Recipes");

        private readonly string _rootPath;

        private readonly RecipeNodeItem _recipeNodeItem;

        private readonly JsonHelper _helper;

        private readonly Dictionary<int, List<string>> _recipeSequence = [];


        public RecipeManager()
        {
            _helper = new JsonHelper(_configPath);

            DirectoryInfo dirInfo =
                Directory.CreateDirectory(_rootFolderName);

            _rootPath = dirInfo.FullName;

            var recipeTypes =
                ConfigManager.Instance
                    .GetConfig<string>("Recipe.RecipeType")
                    ?.Split(',');

            if (recipeTypes != null)
            {
                foreach (var recipeType in recipeTypes)
                {
                    string typeName = recipeType.Trim();

                    if (string.IsNullOrWhiteSpace(typeName))
                        continue;

                    string folderPath =
                        Path.Combine(
                            dirInfo.FullName,
                            typeName);

                    if (!Directory.Exists(folderPath))
                    {
                        Directory.CreateDirectory(folderPath);
                    }
                }
            }

            _recipeNodeItem =
                new RecipeNodeItem(
                    dirInfo,
                    null);

            InitDictionary();
        }


        private void InitDictionary()
        {
            for (int i = 0; i < 100; i++)
            {
                var columns =
                    _helper
                        .FindNode(
                            $"CheckSequence.{i}.Value")
                        ?.ToString();

                if (columns == null)
                    break;

                _recipeSequence[i] =
                    columns
                        .Split(',')
                        .ToList();
            }
        }


        public string GetRootPath()
        {
            return _rootPath;
        }


        public string GetRecipeRootPath()
        {
            return _rootFolderName;
        }


        public ObservableCollection<RecipeNodeItem>? GetRecipes()
        {
            return _recipeNodeItem.SubNodes;
        }


        public void InitTree(RecipeNodeItem? node)
        {
            if (node == null)
                return;

            node.IsSelected = false;

            if (node.SubNodes != null)
            {
                foreach (var child in node.SubNodes)
                {
                    InitTree(child);
                }
            }
        }


        public void UpdateHeaderInfo(RecipeData? recipeData)
        {
            if (recipeData == null)
                return;

            recipeData.Header["ModifyUser"] =
                Environment.UserName;

            recipeData.Header["ModifyTime"] =
                DateTime.Now.ToString(
                    "yyyy-MM-dd HH:mm:ss");
        }


        public Dictionary<string, string> GetStepColumnNames(
            string recipeType)
        {
            Dictionary<string, string> result = [];

            var columns =
                _helper
                    .FindNode(
                        $"FileFormat.{recipeType}.Step")
                    ?.ToList();

            if (columns == null)
                return result;

            foreach (var column in columns)
            {
                string[] item =
                    column
                        .ToString()
                        .Split(',');

                if (item.Length > 1)
                {
                    result[item[0]] = item[1];
                }
                else
                {
                    result[item[0]] =
                        string.Empty;
                }
            }

            return result;
        }


        // 保留一个兼容 RUBI 原方法名的入口
        public Dictionary<string, string> GetStepColumNames(
            string recipeType)
        {
            return GetStepColumnNames(recipeType);
        }


        public IEnumerable<string> GetConfigNames(
            string recipeType)
        {
            var columns =
                _helper
                    .FindNode(
                        $"FileFormat.{recipeType}.Config")
                    ?.ToList();

            return columns?
                .Select(x => x.ToString())
                .ToList()
                ?? [];
        }


        public string GetStepType(string columnName)
        {
            var type =
                _helper.FindNode(
                    $"FieldType.{columnName}.Type");

            const string defaultValue =
                "TextBox";

            if (type == null)
                return defaultValue;

            return type.ToString()
                   ?? defaultValue;
        }


        public string GetStepParameter(string columnName)
        {
            /*
             * RecipeConfig.json 原文件里
             * 这个字段就叫 Paramater，
             * 虽然拼错了，但这里暂时必须按配置文件读取。
             */
            var parameter =
                _helper.FindNode(
                    $"FieldType.{columnName}.Paramater");

            return parameter?.ToString()
                   ?? string.Empty;
        }


        public string GetStepFunction(string columnName)
        {
            var function =
                _helper.FindNode(
                    $"FieldType.{columnName}.Function");

            return function?.ToString()
                   ?? string.Empty;
        }


        public void SaveRecipeData(
            string fullFilePath,
            string content)
        {
            if (string.IsNullOrWhiteSpace(fullFilePath))
                return;

            string? directory =
                Path.GetDirectoryName(fullFilePath);

            if (!string.IsNullOrWhiteSpace(directory))
            {
                Directory.CreateDirectory(directory);
            }

            File.WriteAllText(
                fullFilePath,
                content);
        }


        public IEnumerable<RecipeNodeItem> FindAllRecipe()
        {
            return FindAllNode(_recipeNodeItem)
                .Where(x => !x.IsFolder);
        }


        private IEnumerable<RecipeNodeItem> FindAllNode(
            RecipeNodeItem? recipeNodeItem)
        {
            List<RecipeNodeItem> result = [];

            if (recipeNodeItem == null)
                return result;

            if (recipeNodeItem.SubNodes != null)
            {
                foreach (var item in recipeNodeItem.SubNodes)
                {
                    result.AddRange(
                        FindAllNode(item));
                }
            }
            else
            {
                result.Add(recipeNodeItem);
            }

            return result.Distinct();
        }


        public RecipeNodeItem? FindNodeByPath(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
                return null;

            RecipeNodeItem? findNode =
                _recipeNodeItem;

            string[] paths =
                path.Split(
                    ['\\', '/'],
                    StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < paths.Length; i++)
            {
                string item = paths[i];

                bool isRecipeNode =
                    i == paths.Length - 1;

                findNode =
                    findNode?
                        .SubNodes?
                        .FirstOrDefault(
                            x =>
                                x.Name == item &&
                                x.IsFolder != isRecipeNode);
            }

            return findNode;
        }


        public int GetStepSequence(string moduleName)
        {
            if (_recipeSequence.Count == 0)
                return -1;

            foreach (var item in _recipeSequence)
            {
                if (item.Value.Contains(moduleName))
                {
                    return item.Key;
                }
            }

            return -1;
        }


        public void ReLoadFiles()
        {
            DirectoryInfo dirInfo =
                new DirectoryInfo(_rootPath);

            RecipeNodeItem newRootNode =
                new RecipeNodeItem(
                    dirInfo,
                    null);

            UpdateSubNodes(
                _recipeNodeItem,
                newRootNode);
        }


        private void UpdateSubNodes(
            RecipeNodeItem oldNode,
            RecipeNodeItem newNode)
        {
            List<RecipeNodeItem> oldChildren =
                oldNode.SubNodes?.ToList()
                ?? [];

            List<RecipeNodeItem> newChildren =
                newNode.SubNodes?.ToList()
                ?? [];


            var removedNodes =
                oldChildren
                    .Where(
                        old =>
                            !newChildren.Any(
                                current =>
                                    current.Name == old.Name &&
                                    current.IsFolder == old.IsFolder))
                    .ToList();

            foreach (var node in removedNodes)
            {
                oldNode.SubNodes?.Remove(node);
            }


            var addedNodes =
                newChildren
                    .Where(
                        current =>
                            !oldChildren.Any(
                                old =>
                                    old.Name == current.Name &&
                                    old.IsFolder == current.IsFolder))
                    .ToList();

            foreach (var node in addedNodes)
            {
                oldNode.SubNodes?.Add(node);
            }


            var sameNodes =
                oldChildren
                    .Where(
                        old =>
                            newChildren.Any(
                                current =>
                                    current.Name == old.Name &&
                                    current.IsFolder == old.IsFolder))
                    .ToList();

            foreach (var node in sameNodes)
            {
                if (!node.IsFolder)
                {
                    node.Load();
                }
            }


            foreach (
                var oldChild in
                oldNode.SubNodes
                ?? new ObservableCollection<RecipeNodeItem>())
            {
                var matchingNewChild =
                    newChildren.FirstOrDefault(
                        current =>
                            current.Name == oldChild.Name &&
                            current.IsFolder == oldChild.IsFolder);

                if (matchingNewChild != null)
                {
                    UpdateSubNodes(
                        oldChild,
                        matchingNewChild);
                }
            }
        }
    }
}