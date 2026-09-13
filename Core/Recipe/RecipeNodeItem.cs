using Core.Common;
using System.Collections.ObjectModel;

namespace Core.Recipe
{
    public class RecipeNodeItem : PropertyChangedBase, ICloneable
    {
        public bool IsFolder { get; set; }

        public string Name { get; set; } = string.Empty;

        public string FullPath { get; set; } = string.Empty;
        public RecipeNodeItem? Parent { get; set; }

        public string PathWithType => GetPathWithType();

        public string RecipeType => GetRecipeType();

        public string Path => GetRecipePath();

        private ObservableCollection<RecipeNodeItem>? _subNodes;

        public ObservableCollection<RecipeNodeItem>? SubNodes
        {
            get
            {
                return _subNodes;
            }

            set
            {
                _subNodes = value;
                NotifyOfPropertyChange(nameof(SubNodes));
            }
        }

        private bool _isSelected;

        public bool IsSelected
        {
            get
            {
                return _isSelected;
            }

            set
            {
                _isSelected = value;
                NotifyOfPropertyChange(nameof(IsSelected));
            }
        }


        private bool _isLocked;

        public bool IsLocked
        {
            get
            {
                return _isLocked;
            }

            set
            {
                _isLocked = value;
                NotifyOfPropertyChange(nameof(IsLocked));
            }
        }

        public RecipeData? RecipeData { get; set; }

        private string _versionDateTime = string.Empty;

        public string VersionDateTime
        {
            get
            {
                return _versionDateTime;
            }

            set
            {
                _versionDateTime = value;
                NotifyOfPropertyChange(nameof(VersionDateTime));
            }
        }

        public string GetHeader(string key)
        {
            if (RecipeData == null)
                return string.Empty;

            if (RecipeData.Header.TryGetValue(key, out var value))
            {
                return value;
            }

            return string.Empty;
        }

        public string Permission => GetHeader("Permission");

        public string LockedBy => GetHeader("LockedBy");

        public void UpdateHeader(string key, string? value)
        {
            if (RecipeData == null)
                return;

            RecipeData.Header[key] = value ?? string.Empty;
        }

        public void Save()
        {
            if (IsFolder || RecipeData == null)
                return;

            RecipeManager.Instance.UpdateHeaderInfo(RecipeData);

            string content = RecipeData.ToJsonString();

            RecipeManager.Instance.SaveRecipeData(
                FullPath,
                content);
        }

        public object Clone()
        {
            RecipeNodeItem clone =
                (RecipeNodeItem)MemberwiseClone();

            clone.RecipeData =
                RecipeData.FromJsonString(
                    RecipeData?.ToJsonString() ?? string.Empty);

            if (clone.RecipeData == null)
                return clone;

            clone.RecipeData.Header["CreateUser"] =
                Environment.UserName;

            clone.RecipeData.Header["CreateTime"] =
                DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            return clone;
        }

        public void Unlock()
        {
            UpdateHeader("Password", string.Empty);
            UpdateHeader("LockedBy", string.Empty);
            UpdateHeader("Permission", string.Empty);

            Save();

            IsLocked = false;
        }

        public void Paste(
    string newName,
    string newPath,
    RecipeNodeItem parentNode)
        {
            Name = newName;

            FullPath = newPath;

            Parent = parentNode;

            Unlock();

            parentNode.SubNodes?.Add(this);

            if (!File.Exists(newPath))
            {
                File.Create(newPath).Dispose();
            }

            Save();
        }

        public void Lock(string password, string permission)
        {
            string currentUser = Environment.UserName;

            UpdateHeader("Password", password);
            UpdateHeader("LockedBy", currentUser);
            UpdateHeader("Permission", permission);

            Save();

            IsLocked = true;
        }

        private string GetPathWithType()
        {
            if (string.IsNullOrWhiteSpace(FullPath))
                return string.Empty;

            string rootPath =
                RecipeManager.Instance.GetRootPath();

            string relativePath =
                System.IO.Path.GetRelativePath(
                    rootPath,
                    FullPath);

            if (!IsFolder)
            {
                relativePath =
                    System.IO.Path.ChangeExtension(
                        relativePath,
                        null) ?? relativePath;
            }

            return relativePath;
        }

        private string GetRecipeType()
        {
            if (string.IsNullOrWhiteSpace(PathWithType))
                return string.Empty;

            return PathWithType
                .Split(
                    System.IO.Path.DirectorySeparatorChar,
                    StringSplitOptions.RemoveEmptyEntries)
                .FirstOrDefault() ?? string.Empty;
        }

        private string GetRecipePath()
        {
            if (string.IsNullOrWhiteSpace(PathWithType))
                return string.Empty;

            string[] parts =
                PathWithType.Split(
                    System.IO.Path.DirectorySeparatorChar,
                    StringSplitOptions.RemoveEmptyEntries);

            if (parts.Length <= 1)
                return string.Empty;

            return string.Join(
                System.IO.Path.DirectorySeparatorChar,
                parts.Skip(1));
        }
        public RecipeNodeItem()
        {
        }

        public RecipeNodeItem(DirectoryInfo directory,RecipeNodeItem? parent)
        {
            Name = directory.Name;

            FullPath = directory.FullName;

            Parent = parent;

            IsFolder = true;

            SubNodes = [];

           var directories = 
                parent == null
                ? GetSortedDirectories(directory)
                : directory.GetDirectories()
                .OrderBy(x => x.CreationTime);

            foreach (var subDirectory in directories)
            {
                SubNodes.Add(
                    new RecipeNodeItem(
                        subDirectory,
                        this));
            }

            foreach(var file in directory.GetFiles())
            {
                SubNodes.Add(new RecipeNodeItem(
                    file,
                    this,
                    false));
            }
        }

        public RecipeNodeItem(FileInfo file,
            RecipeNodeItem parent,
            bool isNewFile)
        {
            Name = System.IO.Path.GetFileNameWithoutExtension(file.Name);

            FullPath = file.FullName;

            Parent = parent;

            IsFolder = false;

            if (isNewFile)
            {
                string content = CreateDefaultRecipeData(RecipeType);

                File.WriteAllText(file.FullName, content);

                IsLocked = false;

                VersionDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            }

            RecipeData = RecipeData.FromJsonString(File.ReadAllText(FullPath));

            if (!isNewFile)
            {
                if (RecipeData?.Header.ContainsKey("Password") ?? false)
                {
                    string password = GetHeader("Password");

                    IsLocked = !string.IsNullOrEmpty(password);
                }
            }
        }

        private string CreateDefaultRecipeData(string recipeType)
        {
            var recipeData = new RecipeData();

            recipeData.Header["RecipeType"] = recipeType;

            recipeData.Header["CreateUser"] = Environment.UserName;

            recipeData.Header["CreateTime"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            recipeData.Header["ModifyUser"] = Environment.UserName;

            recipeData.Header["ModifyTime"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            recipeData.Header["Description"] = string.Empty;

            recipeData.Header["IsCheck"] = false.ToString();

            recipeData.Header["Password"] = string.Empty;

            recipeData.Header["LockedBy"] = string.Empty;

            var configs = RecipeManager.Instance.GetConfigNames(recipeType);

            foreach (var item in configs) 
            {
                recipeData.Config[item] = string.Empty;
            }

            return recipeData.ToJsonString();
        }

        private IEnumerable<DirectoryInfo> GetSortedDirectories(DirectoryInfo directory)
        {
            return directory.GetDirectories().OrderBy(x => x.CreationTime);
        }

        public void Load()
        {
            if (File.Exists(FullPath))
            {
                LoadFromContent(File.ReadAllText(FullPath));
            }
        }

        public void LoadFromContent(string content)
        {
            var recipeData = RecipeData.FromJsonString(content);

            RecipeData?.Copy(recipeData);
        }
    }
}