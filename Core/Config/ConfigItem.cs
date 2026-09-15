using Core.Common;

namespace Core.Config
{
    public class ConfigItem : PropertyChangedBase
    {
        public string Name { get; set; } = string.Empty;

        public string _Value { get; set; } = string.Empty;

        public string Value
        {
            get => _Value;

            set
            {
                _Value = value;
                NotifyOfPropertyChange(nameof(Value));
            }
        }

        public string Min { get; set; } = string.Empty;

        public string Max { get; set; } = string.Empty;

        public string Unit { get; set; } = string.Empty;

        public string Type { get; set; } = string.Empty;

        public string Tag { get; set; } = string.Empty;

        public string Parameter { get; set; } = string.Empty;

        public List<object> Parameters =>
            Parameter.Split(';')
            .ToList()
            .Select(x => (object)x)
            .ToList();

        public string Desc { get; set; } = string.Empty;

        public bool IsBaseOnLocal { get; set; }

        public bool IsVisible { get; set; }

        public bool IsNeedRestart { get; set; }
    }
}
