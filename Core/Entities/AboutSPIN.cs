using Core.Common;
using System.Text.Json.Serialization;

namespace Core.Entities
{
    public class NozzleItem : PropertyChangedBase
    {
        public string? ModuleName { get; set; }

        public string? NozzleName { get; set; }


        [JsonIgnore]
        private string? _customizedName;

        public string? CustomizedName
        {
            get => _customizedName;

            set
            {
                _customizedName = value;

                NotifyOfPropertyChange(nameof(CustomizedName));
            }
        }


        private bool _isByPass;

        public bool IsByPass
        {
            get => _isByPass;

            set
            {
                _isByPass = value;

                NotifyOfPropertyChange(nameof(IsByPass));
            }
        }


        [JsonIgnore]
        private string? _setValue;


        [JsonIgnore]
        public string? SetValue
        {
            get => _setValue;

            set
            {
                _setValue = value;

                NotifyOfPropertyChange(nameof(SetValue));
            }
        }
    }
}