using Core.Common;
using Core.Driver.EquipmentControl;

namespace Core.Entities
{
    public class SuckBackEntity : PropertyChangedBase
    {
        public string ControllerName { get; set; } = string.Empty;

        public string Address { get; set; } = string.Empty;

        public string Channel { get; set; } = string.Empty;

        public string ModuleName { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        public string Port { get; set; } = string.Empty;

        public bool IsSaved { get; set; }


        private bool _isFullOpen { get; set; } = false;


        public string Nozzles
        {
            get
            {
                string nozzleName = "";

                if (Name.Contains(":"))
                {
                    string[] modules = Name
                        .Split(':')[0]
                        .Split('&', StringSplitOptions.RemoveEmptyEntries);

                    nozzleName = Name.Split(':')[1];

                    if (nozzleName.StartsWith("PR"))
                    {
                        nozzleName = nozzleName.Replace(
                            "PR",
                            "RESIST");
                    }
                    else if (nozzleName.StartsWith("EC"))
                    {
                        nozzleName = nozzleName + "_RESIST";
                    }
                }

                return nozzleName;
            }
        }


        public string[] modules
        {
            get =>
                Name.Split(":")[0]?
                    .Split(
                        '&',
                        StringSplitOptions.RemoveEmptyEntries)
                ?? [];
        }


        public bool IsSet2Success
        {
            get
            {
                return A1 == A1_C &&
                       A2 == A2_C &&
                       S1 == S1_C &&
                       S2 == S2_C &&
                       S3 == S3_C &&
                       H1 == H1_C &&
                       H3 == H3_C &&
                       T1 == T1_C &&
                       T2 == T2_C &&
                       T3 == T3_C &&
                       T4 == T4_C;
            }
        }


        public bool IsSet3Success
        {
            get
            {
                return (H4 == H4_C || H4 == H3_C) &&
                       (S4 == S4_C || S4 == S3_C);
            }
        }


        public bool IsSet4Success
        {
            get
            {
                return (H5 == H5_C || H5 == H3_C) &&
                       (S5 == S5_C || S5 == S3_C);
            }
        }


        public bool IsFullOpen
        {
            get => _isFullOpen;

            set
            {
                _isFullOpen = value;

                NotifyOfPropertyChange(
                    nameof(IsFullOpen));
            }
        }


        private string _a1 = "190";

        public string A1
        {
            get => _a1;

            set
            {
                _a1 = value;

                NotifyOfPropertyChange(nameof(A1));
            }
        }


        private string _a1_C = "190";

        public string A1_C
        {
            get => _a1_C;

            set
            {
                _a1_C = value;

                NotifyOfPropertyChange(nameof(A1_C));
            }
        }


        private string _a2 = "190";

        public string A2
        {
            get => _a2;

            set
            {
                _a2 = value;

                NotifyOfPropertyChange(nameof(A2));
            }
        }


        private string _a2_C = "190";

        public string A2_C
        {
            get => _a2_C;

            set
            {
                _a2_C = value;

                NotifyOfPropertyChange(nameof(A2_C));
            }
        }


        private string _s1 = "300";

        public string S1
        {
            get => _s1;

            set
            {
                _s1 = value;

                NotifyOfPropertyChange(nameof(S1));
            }
        }


        private string _s1_C = "300";

        public string S1_C
        {
            get => _s1_C;

            set
            {
                _s1_C = value;

                NotifyOfPropertyChange(nameof(S1_C));
            }
        }


        private string _s2 = "300";

        public string S2
        {
            get => _s2;

            set
            {
                _s2 = value;

                NotifyOfPropertyChange(nameof(S2));
            }
        }


        private string _s2_C = "300";

        public string S2_C
        {
            get => _s2_C;

            set
            {
                _s2_C = value;

                NotifyOfPropertyChange(nameof(S2_C));
            }
        }


        private string _s3 = "800";

        public string S3
        {
            get => _s3;

            set
            {
                _s3 = value;

                NotifyOfPropertyChange(nameof(S3));
            }
        }


        private string _s3_C = "800";

        public string S3_C
        {
            get => _s3_C;

            set
            {
                _s3_C = value;

                NotifyOfPropertyChange(nameof(S3_C));
            }
        }


        private string _s4 = "100";

        public string S4
        {
            get => _s4;

            set
            {
                _s4 = value;

                NotifyOfPropertyChange(nameof(S4));
            }
        }


        private string _s4_C = "100";

        public string S4_C
        {
            get => _s4_C;

            set
            {
                _s4_C = value;

                NotifyOfPropertyChange(nameof(S4_C));
            }
        }


        private string _s5 = "100";

        public string S5
        {
            get => _s5;

            set
            {
                _s5 = value;

                NotifyOfPropertyChange(nameof(S5));
            }
        }


        private string _s5_C = "100";

        public string S5_C
        {
            get => _s5_C;

            set
            {
                _s5_C = value;

                NotifyOfPropertyChange(nameof(S5_C));
            }
        }


        private string _h1 = "104";

        public string H1
        {
            get => _h1;

            set
            {
                _h1 = value;

                NotifyOfPropertyChange(nameof(H1));
            }
        }


        private string _h1_C = "104";

        public string H1_C
        {
            get => _h1_C;

            set
            {
                _h1_C = value;

                NotifyOfPropertyChange(nameof(H1_C));
            }
        }


        private string _h3 = "156";

        public string H3
        {
            get => _h3;

            set
            {
                _h3 = value;

                NotifyOfPropertyChange(nameof(H3));
            }
        }


        private string _h3_C = "156";

        public string H3_C
        {
            get => _h3_C;

            set
            {
                _h3_C = value;

                NotifyOfPropertyChange(nameof(H3_C));
            }
        }


        private string _h4 = "39";

        public string H4
        {
            get => _h4;

            set
            {
                _h4 = value;

                NotifyOfPropertyChange(nameof(H4));
            }
        }


        private string _h4_C = "39";

        public string H4_C
        {
            get => _h4_C;

            set
            {
                _h4_C = value;

                NotifyOfPropertyChange(nameof(H4_C));
            }
        }


        private string _h5 = "39";

        public string H5
        {
            get => _h5;

            set
            {
                _h5 = value;

                NotifyOfPropertyChange(nameof(H5));
            }
        }


        private string _h5_C = "39";

        public string H5_C
        {
            get => _h5_C;

            set
            {
                _h5_C = value;

                NotifyOfPropertyChange(nameof(H5_C));
            }
        }


        private string _t1 = "20";

        public string T1
        {
            get => _t1;

            set
            {
                _t1 = value;

                NotifyOfPropertyChange(nameof(T1));
            }
        }


        private string _t1_C = "20";

        public string T1_C
        {
            get => _t1_C;

            set
            {
                _t1_C = value;

                NotifyOfPropertyChange(nameof(T1_C));
            }
        }


        private string _t2 = "20";

        public string T2
        {
            get => _t2;

            set
            {
                _t2 = value;

                NotifyOfPropertyChange(nameof(T2));
            }
        }


        private string _t2_C = "20";

        public string T2_C
        {
            get => _t2_C;

            set
            {
                _t2_C = value;

                NotifyOfPropertyChange(nameof(T2_C));
            }
        }


        private string _t3 = "20";

        public string T3
        {
            get => _t3;

            set
            {
                _t3 = value;

                NotifyOfPropertyChange(nameof(T3));
            }
        }


        private string _t3_C = "20";

        public string T3_C
        {
            get => _t3_C;

            set
            {
                _t3_C = value;

                NotifyOfPropertyChange(nameof(T3_C));
            }
        }


        private string _t4 = "1500";

        public string T4
        {
            get => _t4;

            set
            {
                _t4 = value;

                NotifyOfPropertyChange(nameof(T4));
            }
        }


        private string _t4_C = "1500";

        public string T4_C
        {
            get => _t4_C;

            set
            {
                _t4_C = value;

                NotifyOfPropertyChange(nameof(T4_C));
            }
        }
    }


    public static class ParameterConvert
    {
        public static void AxisRatioParamSet(
            string properStr,
            string value,
            ref AxisRatioParam axisRatio)
        {
            switch (properStr)
            {
                case "X1_Ratio":
                    axisRatio.X1_Ratio =
                        (float)Convert.ToDouble(value);
                    break;

                case "X2_Ratio":
                    axisRatio.X2_Ratio =
                        (float)Convert.ToDouble(value);
                    break;

                case "Y_Ratio":
                    axisRatio.Y_Ratio =
                        (float)Convert.ToDouble(value);
                    break;

                case "Z_Ratio":
                    axisRatio.Z_Ratio =
                        (float)Convert.ToDouble(value);
                    break;

                case "THETA_Ratio":
                    axisRatio.THETA_Ratio =
                        (float)Convert.ToDouble(value);
                    break;
            }
        }


        public static void AxisRatioParamSet(
            string properStr,
            float value,
            ref _AxisRatioParam axisRatio)
        {
            switch (properStr)
            {
                case "X1_Ratio":
                    axisRatio.X1_Ratio = value;
                    break;

                case "X2_Ratio":
                    axisRatio.X2_Ratio = value;
                    break;

                case "Y_Ratio":
                    axisRatio.Y_Ratio = value;
                    break;

                case "Z_Ratio":
                    axisRatio.Z_Ratio = value;
                    break;

                case "THETA_Ratio":
                    axisRatio.THETA_Ratio = value;
                    break;
            }
        }


        public static void StrokeParameterSet(
            string properStr,
            string value,
            ref StrokeSpeed strokeSpeed)
        {
            switch (properStr)
            {
                case "UP_Speed":
                    strokeSpeed.UP_Speed =
                        Convert.ToInt32(value);
                    break;

                case "DN_Speed":
                    strokeSpeed.DN_Speed =
                        Convert.ToInt32(value);
                    break;
            }
        }


        public static void StrokeParameterSet(
            string properStr,
            float value,
            ref _StrokeSpeed strokeSpeed)
        {
            switch (properStr)
            {
                case "UP_Speed":
                    strokeSpeed.UP_Speed =
                        Convert.ToInt32(value);
                    break;

                case "DN_Speed":
                    strokeSpeed.DN_Speed =
                        Convert.ToInt32(value);
                    break;
            }
        }
    }
}