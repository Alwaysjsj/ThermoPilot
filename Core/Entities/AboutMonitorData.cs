using Core.Common;
using System.Diagnostics;

namespace Core.Entities
{
    public enum EnumMonitorDataType
    {
        FlowMeter,
        WaferCount,
        HPTemp,
        TCUTEMP,
        FFUVelocity,
        SpinVelo,
        AnalogueValue
    }

    public class MonitorDataEntity : PropertyChangedBase, ICloneable
    {
        public string? ModuleName { get; set; }

        public EnumMonitorDataType? EntityType { get; set; }

        public string? MonitorName { get; set; }

        public string? MonitorType { get; set; }

        public string? DisplayName { get; set; }


        private double _SetValue;

        public double SetValue
        {
            get => _SetValue;

            set
            {
                bool isChanged = _SetValue != value;

                _SetValue = value;

                if (isChanged)
                {
                    NotifyOfPropertyChange(nameof(SetValue));
                }
            }
        }


        public double AlmMax { get; set; }

        public double AlmMin { get; set; }

        public double StopMax { get; set; }

        public double StopMin { get; set; }


        private float _offset = 0;

        public float Offset
        {
            get => _offset;

            set
            {
                _offset = value;

                NotifyOfPropertyChange(nameof(Offset));
            }
        }


        public string? Unit { get; set; } = "ml/min";

        public double Parameter { get; set; }

        public int WeightNum { get; set; } = 0;

        public float ScaleMax { get; set; } = 2;

        public float ScaleMin { get; set; } = 0;

        public float InputMax { get; set; } = 32767;

        public float InputMin { get; set; } = 0;

        public bool NeedRefresh { get; set; } = false;


        public float K
        {
            get
            {
                if (Math.Abs(InputMax - InputMin) < float.Epsilon)
                {
                    return 0f;
                }

                return (ScaleMax - ScaleMin) / (InputMax - InputMin);
            }
        }


        public float B
        {
            get
            {
                if (Math.Abs(InputMax - InputMin) < float.Epsilon)
                {
                    return 0f;
                }

                return Offset + ScaleMin - InputMin * K;
            }
        }


        private double _mcValue;

        public double MCValue
        {
            get => _mcValue;

            set
            {
                bool isChanged = _mcValue != Math.Round(value, 1);

                _mcValue = Math.Round(value, 1);

                if (isChanged)
                {
                    NotifyOfPropertyChange(nameof(MCValue));
                }
            }
        }


        private double _source;

        public double Source
        {
            get => _source;

            set
            {
                bool isChanged = _source != Math.Round(value, 2);

                _source = Math.Round(value, 2);

                if (isChanged)
                {
                    NotifyOfPropertyChange(nameof(Source));
                }
            }
        }


        private double _use;

        public double Use
        {
            get => _use;

            set
            {
                bool isChanged = _use != Math.Round(value, 1);

                _use = Math.Round(value, 1);

                if (isChanged)
                {
                    NotifyOfPropertyChange(nameof(Use));
                    NotifyOfPropertyChange(nameof(MCValue));
                }
            }
        }


        private double _preUse;

        private readonly Stopwatch _sw = new();


        public void SetMonitorType(string monitorType)
        {
            MonitorType = monitorType;

            switch (MonitorType)
            {
                case "GAS_TYPE":
                    Parameter = 200;
                    break;

                case "FIN_FLOW_TYPE":
                    Parameter = 0.35;
                    break;

                case "KEISO_TYPE":
                    Parameter = 1;
                    break;

                case "AnalogueValue":
                    Parameter = 1;
                    break;

                case "SpinVelo":
                    Parameter = 6;
                    break;
            }
        }


        public void SetMCValue(double mcValue)
        {
            MCValue = mcValue;

            double cycleTime = _sw.ElapsedMilliseconds;

            double oneMinute = 60 * 1000;

            double cycleMinute = cycleTime / oneMinute;

            bool shouldAccumulateSource = false;


            switch (MonitorType)
            {
                case "GAS_TYPE":
                    Use = MCValue / 32767 * Parameter;
                    shouldAccumulateSource = true;
                    break;

                case "FIN_FLOW_TYPE":
                    double plcCycleTime = 200;

                    Use = MCValue * Parameter / plcCycleTime * oneMinute;

                    shouldAccumulateSource = true;
                    break;

                case "KEISO_TYPE":
                    Use = MCValue * Parameter;
                    shouldAccumulateSource = true;
                    break;

                case "AnalogueValue":
                    Use = Parameter *
                          (Offset +
                           ScaleMin +
                           (mcValue - InputMin) *
                           (ScaleMax - ScaleMin) /
                           (InputMax - InputMin));
                    break;

                case "SpinVelo":
                    Use = Parameter * mcValue;
                    break;
            }


            if (_sw.IsRunning)
            {
                if (_preUse == 0 && Use != 0)
                {
                    _preUse = Use;
                }

                if (shouldAccumulateSource)
                {
                    Source = Source +
                             ((Use + _preUse) * cycleMinute / 2);
                }

                _preUse = Use;

                _sw.Restart();
            }

            NeedRefresh = false;
        }


        public void Reset()
        {
            MCValue = 0;
            Use = 0;
            Source = 0;
        }


        public void MonitorBeginOrEnd(bool isBegin)
        {
            if (isBegin)
            {
                _isMCOpen = true;

                _preUse = 0;

                _sw.Restart();
            }
            else
            {
                _isMCOpen = false;

                _preUse = 0;

                Use = 0;

                _sw.Stop();
            }
        }


        public override string ToString()
        {
            return $"{ModuleName} {MonitorName} {MonitorType} {SetValue} {AlmMax} " +
                   $"{AlmMin} {StopMax} {StopMin} {Offset} {Parameter} {Unit} {WeightNum} " +
                   $"{ScaleMax} {ScaleMin} {InputMax} {InputMin} ";
        }


        public object Clone()
        {
            MonitorDataEntity? value =
                MemberwiseClone() as MonitorDataEntity;

            if (value == null)
            {
                return new MonitorDataEntity();
            }
            else
            {
                value.EntityType = EntityType;

                return value;
            }
        }


        public bool _isMCOpen = false;


        public bool IsMCOpen
        {
            get
            {
                if (EntityType == EnumMonitorDataType.FlowMeter ||
                    EntityType == EnumMonitorDataType.AnalogueValue)
                {
                    return _isMCOpen;
                }

                return true;
            }
        }
    }


    public class FlowMonitorEntity : MonitorDataEntity
    {
        public List<FlowMonitorEntity> LinkSeq { get; set; } = [];

        public string? SensorNum { get; set; }


        private bool _isOpen = false;

        public bool IsOpen
        {
            get => _isOpen;

            set
            {
                _isOpen = value;

                NotifyOfPropertyChange(nameof(IsOpen));
            }
        }


        private bool _isAlarm = false;

        public bool IsAlarm
        {
            get => _isAlarm;

            set
            {
                _isAlarm = value;

                NotifyOfPropertyChange(nameof(IsAlarm));
            }
        }


        private bool _isDisplayAvg = false;

        public bool IsDisplayAvg
        {
            get => _isDisplayAvg;

            set
            {
                _isDisplayAvg = value;

                NotifyOfPropertyChange(nameof(IsDisplayAvg));

                NotifyOfPropertyChange(nameof(DisplayValue));
            }
        }


        public float DisplayValue
        {
            get
            {
                if (_isDisplayAvg)
                {
                    return GetAvgValue();
                }
                else
                {
                    return GetInsValue();
                }
            }
        }


        private int _saveCount = 10;

        private Queue<float> _values = new Queue<float>();


        public float Add(double value)
        {
            if (!IsOpen)
            {
                return 0;
            }


            List<float> temps = new List<float>();


            if (_values.Count >= _saveCount)
            {
                temps = _values
                    .TakeLast(_saveCount - 1)
                    .ToList();
            }
            else
            {
                temps = _values.ToList();
            }


            float newValue = (float)value;


            if (newValue <= 0)
            {
                return 0;
            }


            temps.Add(newValue * GetProportion());

            _values.Enqueue(temps.Average());


            return DisplayValue;
        }


        public float GetAvgValue()
        {
            if (_values.Count == 0)
            {
                return 0;
            }


            if (_values.Count > _saveCount)
            {
                List<float> temps =
                    _values
                        .TakeLast(_saveCount)
                        .ToList();

                return (float)Math.Round(
                    temps.Average(),
                    1);
            }
            else
            {
                return (float)Math.Round(
                    _values.ToList().Average(),
                    1);
            }
        }


        public float GetInsValue()
        {
            if (_values.Count == 0)
            {
                return 0;
            }
            else
            {
                return (float)Math.Round(
                    _values.Last(),
                    1);
            }
        }


        public bool Refresh()
        {
            _values.Clear();

            return true;
        }


        private float GetProportion()
        {
            if (!LinkSeq.Any(p => p.IsOpen))
            {
                return 1;
            }
            else
            {
                int totalWeight =
                    LinkSeq
                        .Where(p => p.IsOpen)
                        .Select(p => p.WeightNum)
                        .Sum();


                float proportion =
                    (float)WeightNum /
                    (WeightNum + totalWeight);


                return proportion;
            }
        }
    }


    public class MsgFlowItem
    {
        public string moduleName { get; set; } = "";

        public string monitorName { get; set; } = "";

        public string monitorType { get; set; } = "";

        public string sensorMsg { get; set; } = "";

        public string mcValue { get; set; } = "";
    }


    public class MsgAnalogueItem
    {
        public string Module { get; set; } = "";

        public string Object { get; set; } = "";

        public short Value { get; set; }

        public short SetValue { get; set; }
    }
}