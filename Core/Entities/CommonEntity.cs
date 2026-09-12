using Core.Common;

namespace Core.Entities
{
    public class SelectEntity : PropertyChangedBase
    {
        private string? _No;

        public string? No
        {
            get
            {
                return _No;
            }

            set
            {
                _No = value;
                NotifyOfPropertyChange(nameof(No));
            }
        }

        private string? _Name;

        public string? Name
        {
            get
            {
                return _Name;
            }

            set
            {
                _Name = value;
                NotifyOfPropertyChange(nameof(Name));
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

        private string? _Display;

        public string? Display
        {
            get
            {
                return _Display;
            }

            set
            {
                _Display = value;
                NotifyOfPropertyChange(nameof(Display));
            }
        }
    }

    public class WaferCountEntity : SelectEntity
    {
        private double? _WaferCount;

        public double? WaferCount
        {
            get
            {
                return _WaferCount;
            }

            set
            {
                bool isChanged = _WaferCount != value;

                _WaferCount = value;

                if (isChanged)
                {
                    NotifyOfPropertyChange(nameof(WaferCount));
                }
            }
        }

        private double? _SetCount;

        public double? SetCount
        {
            get
            {
                return _SetCount;
            }

            set
            {
                bool isChanged = _SetCount != value;

                _SetCount = value;

                if (isChanged)
                {
                    NotifyOfPropertyChange(nameof(SetCount));
                }
            }
        }
    }

    public class SendMsgRecordItem
    {
        public string? CmdKey { get; set; }

        public string? CmdStr { get; set; }

        public int SendCount { get; set; } = 1;

        public bool IsRecv { get; set; } = false;

        public void Clear()
        {
            CmdKey = null;
            CmdStr = null;
            SendCount = 1;
            IsRecv = false;
        }
    }
    public enum DisplayType
    {
        None,

        Text,

        Image
    }
}
