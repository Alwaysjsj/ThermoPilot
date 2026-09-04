using Core.Common;

namespace Core.Entities
{
    public enum EnumRecipeType
    {
        Flow,
        Coat,
        Bct,
        Develop,
        ADH,
        HotPlate,
        PLCH,
        Cool,
        System,
        Pump,
        WES,
        WEE,
        Dummy,
        Wash,
        WIS
    }

    public class RecipeArmEntity : PropertyChangedBase
    {
        public string? OperationMode { get; set; }

        public string? ScanMode { get; set; }

        public string? IOSpeed { get; set; }

        public string? UDSpeed { get; set; }

        public string? IOAcc { get; set; }

        public string? UDAcc { get; set; }

        public string? IODec { get; set; }

        public string? UDDec { get; set; }

        public string? TargetPosition { get; set; }

        public string? ScanPosition { get; set; }

        public string? TpOffset { get; set; }

        public string? ScOffset { get; set; }

        public string? TpHeight { get; set; }

        public string? ScHeight { get; set; }
    

    public RecipeArmEntity(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return;
            }

            var values = value.Split(
                new char[] { ' ' },
                StringSplitOptions.RemoveEmptyEntries);

            OperationMode = values[0];

            if (values.Count() <= 4)
            {
                if (values.Length > 1)
                    IOSpeed = values[1];

                if (values.Length > 2)
                    TargetPosition = values[2];

                if (values.Length > 3)
                    TpHeight = values[3];
            }
            else
            {
                if (values.Length > 1)
                    IOSpeed = values[1];

                if (values.Length > 2)
                    UDSpeed = values[2];

                if (values.Length > 3)
                    IOAcc = values[3];

                if (values.Length > 4)
                    UDAcc = values[4];

                if (values.Length > 5)
                    IODec = values[5];

                if (values.Length > 6)
                    UDDec = values[6];

                if (values.Length > 7)
                    TargetPosition = values[7];

                if (values.Length > 8)
                    TpOffset = values[8];

                if (values.Length > 9)
                    TpHeight = values[9];

                if (values.Length > 10)
                    ScanMode = values[10];

                if (values.Length > 11)
                    ScanPosition = values[11];

                if (values.Length > 12)
                    ScOffset = values[12];

                if (values.Length > 13)
                    ScHeight = values[13];
            }
        }

        public override string ToString()
        {
            if (ScanMode == "Fixed")
            {
                return $"{OperationMode} {IOSpeed} {UDSpeed} {IOAcc} {UDAcc} {IODec} {UDDec} {TargetPosition} {TpOffset} {TpHeight} {ScanMode}";
            }

            return $"{OperationMode} {IOSpeed} {UDSpeed} {IOAcc} {UDAcc} {IODec} {UDDec} {TargetPosition} {TpOffset} {TpHeight} {ScanMode} {ScanPosition} {ScOffset} {ScHeight}";
        }
    } 
}