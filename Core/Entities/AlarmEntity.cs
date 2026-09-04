using Core.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities
{
    public enum FlowAlarmType
    {
        Normal = 0,
        LL = 1,
        L = 2,
        H = 3,
        HH = 4,
        ExtraFlow = 5
    }

    public enum EventLevel
    {
        Information = 0,
        Warn,
        Alarm
    }

    public enum EventAction
    {
        Clear,
        Retry,
        Remove,
        Continue,
        Abort,
        Clean
    }

    public enum MCToken
    {
        TK_ARM_PAUSE,
        TK_RRC_LL,
        TK_RRC_L,
        TK_RRC_H,
        TK_RRC_HH
    }

    public class AlarmEntity : PropertyChangedBase, ICloneable
    {
        [Key]
        public string Guid { get; set; } = System.Guid.NewGuid().ToString();

        public string Key => $"{Module}.{Name}";

        public bool WaferOn { get; set; }

        public string? Module { get; set; }

        public string? Name { get; set; }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}