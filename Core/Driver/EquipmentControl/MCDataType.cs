using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Core.Driver.EquipmentControl
{
    [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Ansi)]
    public struct _AxisRatioParam
    {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
        public string Module;

        public float X1_Ratio;
        public float X2_Ratio;
        public float Y_Ratio;
        public float Z_Ratio;
        public float THETA_Ratio;

        public short Index;
    }

    public class AxisRatioParam
    {
        public string Module { get; set; } = "";

        public float X1_Ratio { get; set; }

        public float X2_Ratio { get; set; }

        public float Y_Ratio { get; set; }

        public float Z_Ratio { get; set; }

        public float THETA_Ratio { get; set; }

        public byte Index { get; set; }
    }

    public enum _CLASS : short
    {
        CLASS_NULL,

        MS,
        IS,
        OS,
        BFS,
        FTRA_UNIT,
        CRA_UNIT,
        PRA_UNIT,
        MRA_UNIT,
        MPRA_UNIT,

        IRA_UNIT,

        FOUP,

        TRS,
        ECPL,

        CPL,

        COT,
        DEV,

        ADH,
        BADH,

        HP,
        LHP,
        HHP,
        BHP,
        CBHP,
        CHP,
        DHP,

        CWH,

        IBUF,
        BUF,

        C2MP,
        MP2C,
        MP2P,
        P2I,
        I2M,
        M2P,
        MP2MP,
        WEX,
        WEE,
        WES,
        ICPL,
        SCAN,
        ASML,
        DUMMY_ASML,
        I2I,

        WIS,
        THS,

        CHE_T_1,
        CHE_T_2,
        CHE_T_3,
        CHE_T_4,
        HMDS_T,

        LET,
        BOTTLE,

        CACHE,

        CLASS_PROCESS,
        CLASS_SCAN,

        EQUIPMENT,
        SYSTEM,
        PCW,
        TCU,
        POWERBOX,
        NORMALSIGNAL,

        CLASS_END
    }

    public struct _StrokeSpeed
    {
        public _CLASS Class;
        public _CLASS RobotClass;
        public int UP_Speed;
        public int DN_Speed;
    }

    public class StrokeSpeed
    {
        public string szClass { get; set; } = "";

        public string szRobot { get; set; } = "";

        public int UP_Speed { get; set; }

        public int DN_Speed { get; set; }

        public _CLASS Class { get; set; }

        public _CLASS RobotClass { get; set; }
    }
}
