using Core.Common;

namespace Core.Entities
{
    public enum CommunicationType
    {
        TCP,

        RS232,

        RS485,

        CAN,

        EtherCAT
    }
    public enum CommunicationStatus
    {
        Offline,

        Connecting,

        Online,

        Error
    }
    public enum DeviceStatus
    {
        Idle,

        Init,

        Running,

        Alarm,

        Error,

        Offline
    }

    public class DeviceEntity : PropertyChangedBase
    {
        public string? _DeviceName;
        public string? DeviceName
        {
            get
            {
                return _DeviceName;
            }

            set
            {
                _DeviceName = value;
                NotifyOfPropertyChange(nameof(DeviceName));
            }
        }

        private string? _DeviceId;

        public string? DeviceId
        {
            get
            {
                return _DeviceId;
            }

            set
            {
                _DeviceId = value;
                NotifyOfPropertyChange(nameof(DeviceId));
            }
        }

        private string? _Model;

        public string? Model
        {
            get
            {
                return _Model;
            }

            set
            {
                _Model = value;
                NotifyOfPropertyChange(nameof(Model));
            }
        }

        private DeviceStatus _Status = DeviceStatus.Idle;

        public DeviceStatus Status
        {
            get
            {
                return _Status;
            }

            set
            {
                _Status = value;
                NotifyOfPropertyChange(nameof(Status));
            }
        }

        private CommunicationStatus _CommunicationStatus = CommunicationStatus.Offline;

        public CommunicationStatus CommunicationStatus
        {
            get
            {
                return _CommunicationStatus;
            }

            set
            {
                _CommunicationStatus = value;
                NotifyOfPropertyChange(nameof(CommunicationStatus));
            }
        }

        private string? _Description;

        public string? Description
        {
            get
            {
                return _Description;
            }

            set
            {
                _Description = value;
                NotifyOfPropertyChange(nameof(Description));
            }
        }

        private string? _IP;

        public string? IP
        {
            get
            {
                return _IP;
            }

            set
            {
                _IP = value;
                NotifyOfPropertyChange(nameof(IP));
            }
        }

        private int _Port;

        public int Port
        {
            get
            {
                return _Port;
            }

            set
            {
                _Port = value;
                NotifyOfPropertyChange(nameof(Port));
            }
        }

        private CommunicationType _CommunicationType = CommunicationType.TCP;

        public CommunicationType CommunicationType
        {
            get
            {
                return _CommunicationType;
            }

            set
            {
                _CommunicationType = value;
                NotifyOfPropertyChange(nameof(CommunicationType));
            }
        }

        private string? _ModuleName;

        public string? ModuleName
        {
            get
            {
                return _ModuleName;
            }

            set
            {
                _ModuleName = value;
                NotifyOfPropertyChange(nameof(ModuleName));
            }
        }

        private string? _Version;

        public string? Version
        {
            get
            {
                return _Version;
            }

            set
            {
                _Version = value;
                NotifyOfPropertyChange(nameof(Version));
            }
        }

        private DateTime _CreateTime = DateTime.Now;

        public DateTime CreateTime
        {
            get
            {
                return _CreateTime;
            }

            set
            {
                _CreateTime = value;
                NotifyOfPropertyChange(nameof(CreateTime));
            }
        }

        private TimeSpan _RunTime = TimeSpan.Zero;

        public TimeSpan RunTime
        {
            get
            {
                return _RunTime;
            }

            set
            {
                _RunTime = value;
                NotifyOfPropertyChange(nameof(RunTime));
            }
        }

        private bool _IsEnabled = true;

        public bool IsEnabled
        {
            get
            {
                return _IsEnabled;
            }

            set
            {
                _IsEnabled = value;
                NotifyOfPropertyChange(nameof(IsEnabled));
            }
        }

        private string? _Remark;

        public string? Remark
        {
            get
            {
                return _Remark;
            }

            set
            {
                _Remark = value;
                NotifyOfPropertyChange(nameof(Remark));
            }
        }

        public DeviceEntity()
        {
            Status = DeviceStatus.Idle;

            CommunicationStatus = CommunicationStatus.Offline;

            CommunicationType = CommunicationType.TCP;

            CreateTime = DateTime.Now;

            RunTime = TimeSpan.Zero;

            IsEnabled = true;
        }
    }
}