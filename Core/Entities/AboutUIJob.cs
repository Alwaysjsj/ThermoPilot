using Core.Common;

namespace Core.Entities
{
    public class UILotEntity : PropertyChangedBase
    {
        private string? _Guid;

        public string? Guid
        {
            get => _Guid;

            set
            {
                _Guid = value;
                NotifyOfPropertyChange(nameof(Guid));
            }
        }


        private string? _No;

        public string? No
        {
            get => _No;

            set
            {
                _No = value;
                NotifyOfPropertyChange(nameof(No));
            }
        }


        private string? _LotName;

        public string? LotName
        {
            get => _LotName;

            set
            {
                _LotName = value;
                NotifyOfPropertyChange(nameof(LotName));
            }
        }


        private string? _FlowRecipe;

        public string? FlowRecipe
        {
            get => _FlowRecipe;

            set
            {
                _FlowRecipe = value;
                NotifyOfPropertyChange(nameof(FlowRecipe));
            }
        }


        private string? _StartModule;

        public string? StartModule
        {
            get => _StartModule;

            set
            {
                _StartModule = value;
                NotifyOfPropertyChange(nameof(StartModule));
            }
        }


        private int _LoopCount = 1;

        public int LoopCount
        {
            get => _LoopCount;

            set
            {
                _LoopCount = value;
                NotifyOfPropertyChange(nameof(LoopCount));
            }
        }
    }


    public class UICarrierEntity : PropertyChangedBase
    {
        private string? _Name;

        public string? Name
        {
            get => _Name;

            set
            {
                _Name = value;
                NotifyOfPropertyChange(nameof(Name));
            }
        }


        private string? _Slots;

        public string? Slots
        {
            get => _Slots;

            set
            {
                _Slots = value;
                NotifyOfPropertyChange(nameof(Slots));
            }
        }
    }


    public class RecipeRunEntity : PropertyChangedBase
    {
        private bool _IsRecipeRunning;

        public bool IsRecipeRunning
        {
            get => _IsRecipeRunning;

            set
            {
                _IsRecipeRunning = value;
                NotifyOfPropertyChange(nameof(IsRecipeRunning));
            }
        }


        private string? _RecipeName;

        public string? RecipeName
        {
            get => _RecipeName;

            set
            {
                _RecipeName = value;
                NotifyOfPropertyChange(nameof(RecipeName));
            }
        }


        private int? _CurrentStepLoopCount;

        public int? CurrentStepLoopCount
        {
            get => _CurrentStepLoopCount;

            set
            {
                _CurrentStepLoopCount = value;
                NotifyOfPropertyChange(nameof(CurrentStepLoopCount));
            }
        }


        private int? _CurrentStepLoopNo;

        public int? CurrentStepLoopNo
        {
            get => _CurrentStepLoopNo;

            set
            {
                _CurrentStepLoopNo = value;
                NotifyOfPropertyChange(nameof(CurrentStepLoopNo));
            }
        }


        private int? _RecipeStepCount;

        public int? RecipeStepCount
        {
            get => _RecipeStepCount;

            set
            {
                _RecipeStepCount = value;
                NotifyOfPropertyChange(nameof(RecipeStepCount));
            }
        }


        private int? _CurrentStepNo;

        public int? CurrentStepNo
        {
            get => _CurrentStepNo;

            set
            {
                _CurrentStepNo = value;
                NotifyOfPropertyChange(nameof(CurrentStepNo));
            }
        }


        private string? _StepUseTime;

        public string? StepUseTime
        {
            get => _StepUseTime;

            set
            {
                _StepUseTime = value;
                NotifyOfPropertyChange(nameof(StepUseTime));
            }
        }


        private string? _CurrentStepTime;

        public string? CurrentStepTime
        {
            get => _CurrentStepTime;

            set
            {
                _CurrentStepTime = value;
                NotifyOfPropertyChange(nameof(CurrentStepTime));
            }
        }


        private string? _RecipeTime;

        public string? RecipeTime
        {
            get => _RecipeTime;

            set
            {
                _RecipeTime = value;
                NotifyOfPropertyChange(nameof(RecipeTime));
            }
        }


        private string? _RecipeUseTime;

        public string? RecipeUseTime
        {
            get => _RecipeUseTime;

            set
            {
                _RecipeUseTime = value;
                NotifyOfPropertyChange(nameof(RecipeUseTime));
            }
        }


        private Dictionary<string, string>? _Config;

        public Dictionary<string, string>? Config
        {
            get => _Config;

            set
            {
                _Config = value;
                NotifyOfPropertyChange(nameof(Config));
            }
        }
    }
}