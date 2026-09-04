using Core.Common;
using System.Collections.ObjectModel;
using System.Drawing;

namespace Core.Entities
{
    public enum TemplateType
    {
        Job,
        Lot,
        Wafer,
        FirstFolder,
        SecondFolder,
        ThirdFolder
    }

    public enum NodeType
    {
        Job,
        Lot,
        Wafer
    }

    public class WaferMsgUI : PropertyChangedBase
    {
        public string? Name { get; set; }

        public string? Job_ID { get; set; }

        public string? Lot_ID { get; set; }

        public string? Wafer_ID { get; set; }

        public bool IsWafer
        {
            get => !string.IsNullOrEmpty(Wafer_ID);
        }

        public bool IsJob
        {
            get => string.IsNullOrEmpty(Lot_ID);
        }

        public NodeType NodeType { get; set; }

        public ObservableCollection<WaferMsgUI> SubNodes { get; set; } = [];

        public TemplateType TemplateType { get; set; }

        private bool _isSelected = false;

        public bool IsSelected
        {
            get => _isSelected;

            set
            {
                _isSelected = value;
                NotifyOfPropertyChange(nameof(IsSelected));
            }
        }

        public DateTime? StartTime { get; set; }

        public DateTime? EndTime { get; set; }

        public string? RecipeName { get; set; }

        public int Slot { get; set; }
    }

    public class ModuleMsgUI : PropertyChangedBase
    {
        public int Id { get; set; }

        public string? ModuleType { get; set; }

        public string? Name { get; set; }

        public string? Index { get; set; }

        public string? Wafer_ID { get; set; }

        public string? Lot_ID { get; set; }

        public string? Job_ID { get; set; }

        private bool _isSelected = false;

        public bool IsSelected
        {
            get => _isSelected;

            set
            {
                _isSelected = value;
                NotifyOfPropertyChange(nameof(IsSelected));
            }
        }

        public DateTime? InModuleTime { get; set; }

        public DateTime? OutModuleTime { get; set; }

        public DateTime? RecipeBeginTime { get; set; }

        public DateTime? RecipeEndTime { get; set; }
    }

    public class ParameterMsgUI : PropertyChangedBase
    {
        public int ID { get; set; }

        public string? ModuleName { get; set; }

        public byte ModuleIndex { get; set; }

        public string? Name { get; set; }

        public byte NameIndex { get; set; }

        public double AverageValue { get; set; }

        public DateTime? Time { get; set; }

        public DateTime? WaferInTime { get; set; }

        public DateTime? WaferOutTime { get; set; }

        public DateTime? RecipeBeginTime { get; set; }

        public DateTime? RecipeEndTime { get; set; }

        private bool _isSelected = false;

        public bool IsSelected
        {
            get => _isSelected;

            set
            {
                _isSelected = value;
                NotifyOfPropertyChange(nameof(IsSelected));
            }
        }

        private Color _lineColor;

        /// <summary>
        /// 十六进制颜色编码
        /// </summary>
        public Color LineColor
        {
            get => _lineColor;

            set
            {
                _lineColor = value;
                NotifyOfPropertyChange(nameof(LineColor));
            }
        }

        private bool _isOpenColorPicker = false;

        public bool IsOpenColorPicker
        {
            get => _isOpenColorPicker;

            set
            {
                _isOpenColorPicker = value;
                NotifyOfPropertyChange(nameof(IsOpenColorPicker));
            }
        }
    }
}