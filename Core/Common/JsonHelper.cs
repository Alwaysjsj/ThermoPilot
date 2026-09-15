using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Core.Common
{
    public class JsonHelper
    {
        private JObject _root;

        private readonly string _filePath;

        public JsonHelper(string filePath)
        {
            _filePath = filePath;

            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(_filePath);

                _root = JObject.Parse(json);
            }
            else
            {
                _root = [];
            }
        }

        public JToken GetRoot()
        {
            return _root.Root;
        }

        public JToken? FindNode(string path)
        {
            return _root.SelectToken(path);
        }

        public bool AddNode(string path, JToken node)
        {
            var token = _root.SelectToken(path);

            if (token is JArray array)
            {
                array.Add(node);

                return true;
            }
            return false;
        }

        public bool UpdateNode(string path, JArray node)
        {
            _root[path] = node;

            return true;
        }

        public bool DeleteNode(string path)
        {
            var token = _root.SelectToken(path);

            if (token != null)
            {
                token.Remove();
                return true;
            }
            return false;
        }

        public T? GetValue<T>(string path)
        {
            var token = _root.SelectToken(path);

            if (token == null)
            {
                return default(T);
            }
            return (T)Convert.ChangeType(token, typeof(T));
        }

        public bool SetValue<T>(string path, T newValue) where T : notnull
        {
            var token = _root.SelectToken(path);

            if (token != null)
            {
                token.Replace(JToken.FromObject(newValue));
                return true;
            }
            return false;
        }

        public List<string> FindAllPaths()
        {
            var paths = new List<string>();

            FindAllPathsRecursive(GetRoot(), paths);

            return paths;
        }

        private void FindAllPathsRecursive(JToken token, List<string> paths)
        {
            if (!token.HasValues)
            {
                if (token.Parent is JProperty property && property.Parent != null)
                {
                    if (property.Name == "Value")
                    {
                        paths.Add(property.Parent.Path);

                        return;
                    }
                }
            }

            foreach (var prop in token.Children<JProperty>())
            {
                FindAllPathsRecursive(prop.Value, paths);
            }
        }

        public void Save()
        {
            string content = JsonConvert.SerializeObject(_root, Formatting.Indented);

            File.WriteAllText(_filePath, content);
        }

        public void Save(string content)
        {
            _root = JObject.Parse(content);

            File.WriteAllText(_filePath, content);
        }
    }
}
