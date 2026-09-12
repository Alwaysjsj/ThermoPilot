using Core.Common;
using Core.Language;
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

        [NotMapped]
        public string? Message
        {
            get
            {
                switch (LanguageManager.GetLanguage())
                {
                    case "ZH-CN":
                        return MessageCN;

                    case "EN":
                        return MessageEN;

                    default:
                        return MessageEN;
                }
            }

            set
            {
                switch (LanguageManager.GetLanguage())
                {
                    case "ZH-CN":
                        MessageCN = value;
                        break;

                    case "EN":
                        MessageEN = value;
                        break;

                    default:
                        MessageEN = value;
                        break;
                }
            }
        }

        public string? MessageCN { get; set; }

        public string? MessageEN { get; set; }

        [NotMapped]
        public string? Cause
        {
            get
            {
                switch (LanguageManager.GetLanguage())
                {
                    case "ZH-CN":
                        return CauseCN;

                    case "EN":
                        return CauseEN;

                    default:
                        return CauseEN;
                }
            }

            set
            {
                switch (LanguageManager.GetLanguage())
                {
                    case "ZH-CN":
                        CauseCN = value;
                        break;

                    case "EN":
                        CauseEN = value;
                        break;

                    default:
                        CauseEN = value;
                        break;
                }
            }
        }

        public string? CauseCN { get; set; }

        public string? CauseEN { get; set; }

        [NotMapped]
        public string? Solution
        {
            get
            {
                switch (LanguageManager.GetLanguage())
                {
                    case "ZH-CN":
                        return SolutionCN;

                    case "EN":
                        return SolutionEN;

                    default:
                        return SolutionEN;
                }
            }

            set
            {
                switch (LanguageManager.GetLanguage())
                {
                    case "ZH-CN":
                        SolutionCN = value;
                        break;

                    case "EN":
                        SolutionEN = value;
                        break;

                    default:
                        SolutionEN = value;
                        break;
                }
            }
        }

        public string? SolutionCN { get; set; }

        public string? SolutionEN { get; set; }

        public string? Level { get; set; }

        public string? Action { get; set; } // "Clear,Retry"

        public string OccurTime { get; set; } = string.Empty;

        public string? ProcessTime { get; set; }

        public string? Type { get; set; }

        public uint ALID { get; set; }

        [NotMapped]
        public bool IsMCAlarm { get; set; }

        [NotMapped]
        public string MCToken { get; set; } = string.Empty;
    }
}