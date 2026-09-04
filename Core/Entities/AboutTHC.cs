using Core.Common;

namespace Core.Entities
{
    public class ThcItem
    {
        public string? Module { get; set; }

        public string? Key { get; set; }

        public string? THCName { get; set; }

        public List<string>? Modules
        {
            get
            {
                if (!string.IsNullOrEmpty(Module) && Module.Contains(" "))
                {
                    return Module.Split(" ").ToList();
                }

                return null;
            }
        }

        public string? Display
        {
            get
            {
                if (!string.IsNullOrEmpty(Module) && Module.Contains(" "))
                {
                    return Module;
                }
                else
                {
                    return Module + " " + Key;
                }
            }
        }
    }

    public class TCUControlItem : PropertyChangedBase
    {
        private string? _name;

        public string? Name
        {
            get => _name;

            set
            {
                _name = value;
                NotifyOfPropertyChange(nameof(Name));
            }
        }

        private bool _isOpen;

        public bool IsOpen
        {
            get => _isOpen;

            set
            {
                _isOpen = value;
                NotifyOfPropertyChange(nameof(IsOpen));
            }
        }
    }
}