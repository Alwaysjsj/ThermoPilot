using Core.Common;
using System.Text.Json;

namespace Core.Config
{
    public class ConfigManager : Singleton<ConfigManager>
    {
        private readonly string _pathConfig =
            Path.Combine(
                AppContext.BaseDirectory,
                "Config",
                "SystemConfig.json");

        private readonly string _pathBin =
            Path.Combine(
                AppContext.BaseDirectory,
                "Cache",
                "SystemConfig.bin");

        private Dictionary<string, ConfigItem> _dicItems = [];

        private readonly JsonHelper _helperConfig;
        private readonly JsonHelper _helperBin;


        public ConfigManager()
        {
            _helperConfig = new JsonHelper(_pathConfig);
            _helperBin = new JsonHelper(_pathBin);

            var resultList = _helperConfig.FindAllPaths();

            foreach (var item in resultList)
            {
                var scItem = new ConfigItem
                {
                    Name = item,

                    Desc =
                        _helperConfig.GetValue<string>(
                            $"{item}.Desc") ?? string.Empty,

                    Type =
                        _helperConfig.GetValue<string>(
                            $"{item}.Type") ?? string.Empty,

                    Value =
                        _helperConfig.GetValue<string>(
                            $"{item}.Value") ?? string.Empty,

                    Min =
                        _helperConfig.GetValue<string>(
                            $"{item}.Min") ?? string.Empty,

                    Max =
                        _helperConfig.GetValue<string>(
                            $"{item}.Max") ?? string.Empty,

                    Unit =
                        _helperConfig.GetValue<string>(
                            $"{item}.Unit") ?? string.Empty,

                    Parameter =
                        _helperConfig.GetValue<string>(
                            $"{item}.Parameter") ?? string.Empty,

                    Tag =
                        _helperConfig.GetValue<string>(
                            $"{item}.Tag") ?? string.Empty,

                    IsBaseOnLocal =
                        _helperConfig.GetValue<bool>(
                            $"{item}.IsBaseOnLocal"),

                    IsVisible =
                        _helperConfig.GetValue<bool>(
                            $"{item}.IsVisible"),

                    IsNeedRestart =
                        _helperConfig.GetValue<bool>(
                            $"{item}.IsNeedRestart")
                };

                _dicItems[item] = scItem;
            }


            string? directoryPath =
                Path.GetDirectoryName(_pathBin);

            if (!string.IsNullOrWhiteSpace(directoryPath) &&
                !Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }


            if (!File.Exists(_pathBin))
            {
                Save();
            }
            else
            {
                var list =
                    JsonSerializer.Deserialize<Dictionary<string, string>>(
                        File.ReadAllText(_pathBin));

                foreach (var item in _dicItems)
                {
                    if (list?.ContainsKey(item.Key) == true &&
                        !_dicItems[item.Key].IsBaseOnLocal)
                    {
                        _dicItems[item.Key].Value =
                            list[item.Key];
                    }
                }
            }
        }


        public void AddConfig(
            string pathConfig,
            bool isLocalMode = false)
        {
            JsonHelper addHelper =
                new JsonHelper(pathConfig);

            var resultList =
                addHelper.FindAllPaths();

            foreach (var item in resultList)
            {
                var scItem = new ConfigItem
                {
                    Name = "System." + item,

                    Desc =
                        addHelper.GetValue<string>(
                            $"{item}.Desc") ?? string.Empty,

                    Type =
                        addHelper.GetValue<string>(
                            $"{item}.Type") ?? string.Empty,

                    Value =
                        addHelper.GetValue<string>(
                            $"{item}.Value") ?? string.Empty,

                    Min =
                        addHelper.GetValue<string>(
                            $"{item}.Min") ?? string.Empty,

                    Max =
                        addHelper.GetValue<string>(
                            $"{item}.Max") ?? string.Empty,

                    Unit =
                        addHelper.GetValue<string>(
                            $"{item}.Unit") ?? string.Empty,

                    Parameter =
                        addHelper.GetValue<string>(
                            $"{item}.Parameter") ?? string.Empty,

                    Tag =
                        addHelper.GetValue<string>(
                            $"{item}.Tag") ?? string.Empty,

                    IsBaseOnLocal =
                        addHelper.GetValue<bool>(
                            $"{item}.IsBaseOnLocal"),

                    IsVisible =
                        addHelper.GetValue<bool>(
                            $"{item}.IsVisible"),

                    IsNeedRestart =
                        addHelper.GetValue<bool>(
                            $"{item}.IsNeedRestart")
                };

                _dicItems["System." + item] = scItem;
            }


            if (isLocalMode)
            {
                if (!_dicItems.ContainsKey("System.IsLocalMode"))
                {
                    _dicItems["System.IsLocalMode"] =
                        new ConfigItem
                        {
                            Name = "System.IsLocalMode",
                            Value = "True"
                        };
                }
            }
        }


        private void Save()
        {
            var list =
                _dicItems.ToDictionary(
                    x => x.Key,
                    y => y.Value.Value);

            string content =
                JsonSerializer.Serialize(
                    list,
                    new JsonSerializerOptions
                    {
                        WriteIndented = true
                    });

            _helperBin.Save(content);
        }


        public T? GetUnit<T>(string key)
        {
            if (!_dicItems.ContainsKey(key))
                return default;

            var value =
                _dicItems[key].Unit;

            T? result =
                (T?)Convert.ChangeType(
                    value,
                    typeof(T));

            return result;
        }


        public T? GetConfig<T>(string key)
        {
            if (!_dicItems.ContainsKey(key))
                return default;

            var value =
                _dicItems[key].Value;

            T? result =
                (T?)Convert.ChangeType(
                    value,
                    typeof(T));

            return result;
        }


        public T? GetConfig<T>(
            string key,
            object defaultValue)
        {
            var value =
                !_dicItems.ContainsKey(key)
                    ? defaultValue
                    : _dicItems[key].Value;

            T? result =
                (T?)Convert.ChangeType(
                    value,
                    typeof(T));

            return result;
        }


        public void SetConfig<T>(
            string key,
            T value)
            where T : notnull
        {
            _dicItems[key].Value =
                value.ToString() ?? string.Empty;

            Save();
        }


        public Dictionary<string, ConfigItem> GetAllConfig()
        {
            return _dicItems;
        }
    }
}