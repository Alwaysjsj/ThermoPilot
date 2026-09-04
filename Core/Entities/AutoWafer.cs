using Core.Common;

namespace Core.Entities
{
    public class WaferRouteEntity : PropertyChangedBase
    {
        public string? ModuleName { get; set; }

        public string? ModuleType { get; set; }

        public string? RecipeName { get; set; }

        public DateTime? WaferInTime { get; set; }

        public DateTime? WaferOutTime { get; set; }

        public DateTime? RecipeStartTime { get; set; }

        public DateTime? RecipeEndTime { get; set; }
    }
}
