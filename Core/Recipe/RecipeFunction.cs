using System.Collections.ObjectModel;

namespace Core.Recipe
{
    public partial class RecipeManager
    {
        private readonly string _moduleName = "RecipeManager";
        private readonly object _lock = new();

        private void InitFunction()
        {
            // 暂时留空
            // 后面实现 PublishManager 后，
            // 会在这里注册 New、Delete、Rename、Check、Save 等功能
        }

        private void New(object[] objs)
        {
            bool _isFolder = (bool)objs[0];

            string newPath =
                objs.Length > 1
                    ? (string)objs[1]
                    : "";

            RecipeNodeItem? nodeItem =
                objs.Length > 2
                    ? objs[2] as RecipeNodeItem
                    : null;

            lock (_lock)
            {
                if (_isFolder)
                {
                    Directory.CreateDirectory(newPath);

                    nodeItem?.SubNodes?.Add(
                        new RecipeNodeItem(
                            new DirectoryInfo(newPath),
                            nodeItem));
                }
                else
                {
                    string? directoryPath =
                        Path.GetDirectoryName(newPath);

                    if (directoryPath != null &&
                        !Directory.Exists(directoryPath))
                    {
                        Directory.CreateDirectory(directoryPath);
                    }

                    FileStream file = File.Create(newPath);
                    file.Close();

                    nodeItem?.SubNodes?.Add(
                        new RecipeNodeItem(
                            new FileInfo(newPath),
                            nodeItem,
                            true));
                }
            }
        }

        private void Delete(object[] objs)
        {
            bool _isFolder = (bool)objs[0];

            lock (_lock)
            {
                RecipeNodeItem? nodeItem =
                    objs.Length > 1
                        ? objs[1] as RecipeNodeItem
                        : null;

                if (nodeItem == null)
                    return;

                if (_isFolder)
                {
                    DeleteFolder(nodeItem);

                    Directory.Delete(nodeItem.FullPath);
                }
                else
                {
                    File.Delete(nodeItem.FullPath);
                }

                nodeItem.Parent?.SubNodes?.Remove(nodeItem);
            }
        }

        private void DeleteFolder(RecipeNodeItem nodeItem)
        {
            if (nodeItem.IsFolder)
            {
                if (nodeItem.SubNodes != null &&
                    nodeItem.SubNodes.Any())
                {
                    foreach (var value in nodeItem.SubNodes)
                    {
                        DeleteFolder(value);
                    }
                }
            }
            else
            {
                File.Delete(nodeItem.FullPath);
            }
        }

        private void Rename(object[] objs)
        {
            bool _isFolder = (bool)objs[0];

            string newPath =
                objs.Length > 1
                    ? (string)objs[1]
                    : "";

            RecipeNodeItem? nodeItem =
                objs.Length > 2
                    ? objs[2] as RecipeNodeItem
                    : null;

            if (nodeItem == null)
                return;

            string newName =
                objs.Length > 3
                    ? (string)objs[3]
                    : "";

            if (_isFolder)
            {
                Directory.Move(
                    nodeItem.FullPath,
                    newPath);

                if (nodeItem.SubNodes != null &&
                    nodeItem.SubNodes.Any())
                {
                    RefreshFolder(
                        nodeItem.SubNodes,
                        newPath);
                }
            }
            else
            {
                File.Move(
                    nodeItem.FullPath,
                    newPath);
            }

            nodeItem.FullPath = newPath;
            nodeItem.Name = newName;

            RecipeManager.Instance.UpdateHeaderInfo(
                nodeItem.RecipeData);
        }

        private void RefreshFolder(
            ObservableCollection<RecipeNodeItem> values,
            string newPath)
        {
            if (values != null)
            {
                foreach (RecipeNodeItem item in values)
                {
                    item.FullPath =
                        newPath + "\\" + item.Name;

                    if (item.IsFolder)
                    {
                        if (item.SubNodes != null &&
                            item.SubNodes.Any())
                        {
                            RefreshFolder(
                                item.SubNodes,
                                item.FullPath);
                        }
                    }
                    else
                    {
                        if (!item.FullPath.EndsWith(
                                Core.Common.CGlobal.RecipeFileFormat))
                        {
                            item.FullPath +=
                                Core.Common.CGlobal.RecipeFileFormat;
                        }
                    }
                }
            }
        }
    }
}