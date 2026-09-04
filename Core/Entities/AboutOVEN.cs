using Core.Common;
using System.Collections.Concurrent;

namespace Core.Entities
{
    public enum HPType
    {
        // 两温区无上盖加热
        LHPC,

        // 两温区有上盖加热
        GCHC,

        PLCH,

        // 七温区无上盖加热
        GHPC,

        // 新阳热板 两温区：上盖一个温区，底板一个温区
        CVCH
    }


    public enum AzbilReportType : int
    {
        /// <summary>
        /// 当前实际温度
        /// </summary>
        PV,

        /// <summary>
        /// 设定目标温度
        /// </summary>
        LSP,

        /// <summary>
        /// 温度告警上限
        /// </summary>
        AlarmMax,

        /// <summary>
        /// 温度告警下限
        /// </summary>
        AlarmMin,

        /// <summary>
        /// 温度停止上限
        /// </summary>
        StopMax,

        /// <summary>
        /// 温度停止下限
        /// </summary>
        StopMin,

        /// <summary>
        /// 偏移量
        /// </summary>
        Offset,

        /// <summary>
        /// PID参数
        /// </summary>
        PID,

        /// <summary>
        /// 温控仪状态 RUN/READY
        /// </summary>
        MachineState,

        /// <summary>
        /// 自整定状态
        /// </summary>
        ATState,

        /// <summary>
        /// 告警
        /// </summary>
        Alarm,

        /// <summary>
        /// 初始化状态 0:失败 1:成功
        /// </summary>
        IniResult
    }


    public enum AzbilSettingType : int
    {
        /// <summary>
        /// 设定目标温度
        /// </summary>
        LSP,

        /// <summary>
        /// 温度告警上限
        /// </summary>
        AlarmMax,

        /// <summary>
        /// 温度告警下限
        /// </summary>
        AlarmMin,

        /// <summary>
        /// 温度停止上限
        /// </summary>
        StopMax,

        /// <summary>
        /// 温度停止下限
        /// </summary>
        StopMin,

        /// <summary>
        /// 偏移量（测试程序使用）
        /// </summary>
        Offset,

        /// <summary>
        /// PID参数（测试程序使用）
        /// </summary>
        PID,

        /// <summary>
        /// 温控仪工作状态 RUN/READY
        /// </summary>
        MachineState,

        /// <summary>
        /// 自整定状态 0:停止 1:启动
        /// </summary>
        ATState,

        /// <summary>
        /// 实时温度上报周期
        /// </summary>
        ReportPeriod
    }


    /// <summary>
    /// 温控仪控制状态类型
    /// </summary>
    public enum ReportStateType : int
    {
        UnConnected,

        /// <summary>
        /// 正在控温
        /// </summary>
        UnderControl,

        /// <summary>
        /// 稳态
        /// </summary>
        Steady,

        /// <summary>
        /// 超告警上限
        /// </summary>
        UpperAlarmMax,

        /// <summary>
        /// 超告警下限
        /// </summary>
        LowerAlarmMin,

        /// <summary>
        /// 超停止上限
        /// </summary>
        UpperStopMax,

        /// <summary>
        /// 超停止下限
        /// </summary>
        LowerStopMin
    }


    public class HPControlItem : PropertyChangedBase
    {
        public string? ModuleName { get; set; }


        public bool IsStarted
        {
            get
            {
                if (CGlobal.IsSimulateMonitor)
                {
                    return true;
                }

                if (ControlKeys != null && ControlKeys.Any())
                {
                    if (!ControlKeys.Any(p => !p.IsConnected))
                    {
                        return true;
                    }
                }

                return false;
            }
        }


        public List<ControlMsg>? ControlKeys { get; set; }


        public HPType hPtype { get; set; } = HPType.LHPC;


        public double GetTemperature()
        {
            double result = 0;

            switch (hPtype)
            {
                case HPType.LHPC:
                case HPType.GCHC:
                case HPType.PLCH:
                case HPType.CVCH:
                    result = ControlKeys?
                        .FirstOrDefault()?
                        .GetLastPV(hPtype) ?? 0;
                    break;

                case HPType.GHPC:
                    result = ControlKeys?
                        .Select(p => p.GetLastPV(hPtype))
                        .Average() ?? 0;
                    break;
            }

            return Math.Round(result, 2);
        }


        public double GetCoverTemp()
        {
            string result = "0";

            switch (hPtype)
            {
                case HPType.PLCH:
                case HPType.CVCH:
                    result = ControlKeys?
                        .First()
                        .Channels
                        .Last()
                        .Value
                        .PV
                        .GetLastItem() ?? "0";
                    break;
            }

            return Math.Round(Convert.ToDouble(result), 2);
        }
    }


    public class ControlMsg
    {
        /// <summary>
        /// Control名称
        /// </summary>
        public string? Name
        {
            get
            {
                if (!string.IsNullOrEmpty(ConfigPath))
                {
                    List<string> strings =
                        ConfigPath.Split(".").ToList();

                    return strings.FirstOrDefault(
                        p => p.Contains("Azbil"));
                }

                return "";
            }
        }


        /// <summary>
        /// 配置文件路径
        /// </summary>
        public string? ConfigPath { get; set; }


        public bool IsConnected { get; set; }


        /// <summary>
        /// 消息发送函数key
        /// </summary>
        public string? FunctionKey { get; set; }


        /// <summary>
        /// 消息指令内容key
        /// </summary>
        public string? CommandsKey { get; set; }


        public Dictionary<string, ChannelItem> Channels { get; set; }
            = new Dictionary<string, ChannelItem>();


        public int ChannelUnit { get; set; }


        public string GetPVCommandFormat { get; set; }
            = "Get_PV{0}";


        public string SetSVCommandFormat { get; set; }
            = "Set_SV{0}";


        public string GetSVCommandFormat { get; set; }
            = "Get_SV{0}";


        public string GetStateCommandFormat { get; set; }
            = "Get_RunOrReady{0}";


        public string SetStateCommandFormat { get; set; }
            = "Set_RunOrReady{0}";


        // 设置P（比例）
        public string SetProportional { get; set; }
            = "Set_P{0}";


        // 设置I（积分）
        public string SetIntegral { get; set; }
            = "Set_I{0}";


        // 设置D（微分）
        public string SetDerivative { get; set; }
            = "Set_D{0}";


        // 一次性获取PID
        public string GetPID { get; set; }
            = "Get_PID{0}";


        public double GetLastPV(HPType type)
        {
            double result = -999;

            if (Channels.Any())
            {
                switch (type)
                {
                    case HPType.LHPC:

                    case HPType.GHPC:
                        {
                            // 取所有通道的平均值
                            List<double> temps = new List<double>();

                            foreach (var item in Channels)
                            {
                                double value =
                                    Convert.ToDouble(
                                        item.Value.PV.GetLastItem());

                                temps.Add(value);
                            }

                            if (temps.Any())
                            {
                                result = temps.Average();
                            }

                            break;
                        }


                    case HPType.GCHC:

                    case HPType.PLCH:
                        {
                            // 带上盖加热：
                            // 1、2通道取平均值
                            // 3通道为盖板
                            List<double> temps = new List<double>();

                            for (int i = 1; i < 3; i++)
                            {
                                double value =
                                    Convert.ToDouble(
                                        Channels[i.ToString()]
                                            .PV
                                            .GetLastItem());

                                temps.Add(value);
                            }

                            if (temps.Any())
                            {
                                result = temps.Average();
                            }

                            break;
                        }


                    case HPType.CVCH:
                        {
                            // 带上盖加热：
                            // 取1通道的数据
                            // 2通道为盖板
                            List<double> temps = new List<double>();

                            for (int i = 1; i < 2; i++)
                            {
                                double value =
                                    Convert.ToDouble(
                                        Channels[i.ToString()]
                                            .PV
                                            .GetLastItem());

                                temps.Add(value);
                            }

                            if (temps.Any())
                            {
                                result = temps.Average();
                            }

                            break;
                        }
                }
            }

            return result;
        }


        /// <summary>
        /// 温度是否稳定
        /// </summary>
        public bool IsPVStable()
        {
            bool result = true;

            if (Channels.Any())
            {
                foreach (var item in Channels)
                {
                    double[] values =
                        item.Value.PV.GetAllItems();

                    if (values == null || values.Length < 5)
                    {
                        return false;
                    }

                    if (values.Any(p => p.Equals(0)))
                    {
                        return false;
                    }

                    if ((values.Max() - values.Min()) > 0.2)
                    {
                        return false;
                    }
                }
            }

            return result;
        }
    }


    public class ChannelItem
    {
        public LatestThreeItems<double> PV =
            new LatestThreeItems<double>();


        public string? SV;


        public bool IsCover { get; set; } = false;


        private readonly List<Interval<double>> _intervals = new();


        public void Add(
            double lower,
            double upper,
            double value)
        {
            if (upper <= lower)
            {
                return;
            }

            _intervals.Add(
                new Interval<double>
                {
                    Lower = lower,
                    Upper = upper,
                    Value = value
                });

            _intervals.Sort(
                (a, b) => a.Lower.CompareTo(b.Lower));
        }


        public void UpdateOffsetValue(
            double targetTemp,
            double newValue)
        {
            int index = _intervals.BinarySearch(
                new Interval<double>
                {
                    Lower = targetTemp
                },
                Comparer<Interval<double>>.Create(
                    (a, b) =>
                        a.Lower.CompareTo(b.Lower)));


            if (index < 0)
            {
                Add(
                    targetTemp,
                    targetTemp + 20,
                    newValue);

                index = _intervals.Count;
            }


            if (index >= 0 &&
                index < _intervals.Count &&
                _intervals[index].Contains(targetTemp))
            {
                _intervals[index].Value = newValue;
            }
        }


        public double GetOffsetValue(double targetTemp)
        {
            int index = _intervals.BinarySearch(
                new Interval<double>
                {
                    Lower = targetTemp
                },
                Comparer<Interval<double>>.Create(
                    (a, b) =>
                        a.Lower.CompareTo(b.Lower)));


            if (index < 0)
            {
                index = ~index - 1;
            }


            if (index >= 0 &&
                index < _intervals.Count &&
                _intervals[index].Contains(targetTemp))
            {
                return _intervals[index].Value;
            }


            return 0;
        }
    }


    public class Interval<T>
    {
        // 区间下限（包含）
        public double Lower { get; set; }


        // 区间上限（不包含，即 [Lower, Upper)）
        public double Upper { get; set; }


        public T? Value { get; set; }


        public bool Contains(double x)
        {
            return x >= Lower && x < Upper;
        }
    }


    public class LatestThreeItems<T>
    {
        private string? _lastItem;


        private ConcurrentQueue<T> _queue =
            new ConcurrentQueue<T>();


        public void AddItem(T item)
        {
            _lastItem = item?.ToString();

            _queue.Enqueue(item);

            if (_queue.Count > 5)
            {
                _queue.TryDequeue(out _);
            }
        }


        public T[] GetAllItems()
        {
            if (_queue != null && _queue.Count > 0)
            {
                return _queue.ToArray();
            }

            return new T[0];
        }


        public string? GetLastItem()
        {
            return _lastItem;
        }
    }


    public class TemperatureMonitor
    {
        public ExtControlEntity? extControlEntity;


        public bool IsCover { get; set; } = false;


        public bool IsControl { get; set; } = false;


        public float ActuallyPV { get; set; }


        public float PreviousPV { get; set; }


        public bool IsPVRising { get; set; } = false;


        public bool IsSendReady { get; set; } = false;


        public bool IsStable { get; set; } = false;


        public bool IsInitOK
        {
            get
            {
                if (extControlEntity == null || !IsStable)
                {
                    return false;
                }

                return ActuallyPV >=
                       extControlEntity.SetValue -
                       extControlEntity.AlmMin;
            }
        }


        public bool IsAllowSend { get; set; } = false;


        public bool IsAlarm()
        {
            if (extControlEntity == null)
            {
                return false;
            }

            if (IsStable)
            {
                return !(
                    ActuallyPV <=
                    extControlEntity.SetValue +
                    extControlEntity.AlmMax
                    &&
                    ActuallyPV >=
                    extControlEntity.SetValue -
                    extControlEntity.AlmMin);
            }
            else
            {
                return ActuallyPV >
                       extControlEntity.SetValue +
                       extControlEntity.AlmMax;
            }
        }


        public bool IsStop()
        {
            if (extControlEntity == null)
            {
                return false;
            }

            if (IsStable)
            {
                return !(
                    ActuallyPV <=
                    extControlEntity.SetValue +
                    extControlEntity.StopMax
                    &&
                    ActuallyPV >=
                    extControlEntity.SetValue -
                    extControlEntity.StopMin);
            }
            else
            {
                return ActuallyPV >
                       extControlEntity.SetValue +
                       extControlEntity.StopMax;
            }
        }
    }


    public class InitTempItem : PropertyChangedBase
    {
        public string? ModuleName { get; set; }


        private string? _temperature;


        public string? Temperature
        {
            get => _temperature;

            set
            {
                _temperature = value;

                NotifyOfPropertyChange(
                    nameof(Temperature));
            }
        }
    }
}