using Core.Common;

namespace Core.Entities
{
    public class RecipeEntity : PropertyChangedBase
    {
        private string? _Name;

        public string? Name
        {
            get
            {
                return _Name;
            }

            set
            {
                _Name = value;
                NotifyOfPropertyChange(nameof(Name));
            }
        }

        private string? _RecipeId;

        public string? RecipeId
        {
            get
            {
                return _RecipeId;
            }
            set
            {
                _RecipeId = value;
                NotifyOfPropertyChange(nameof(RecipeId));
            }
        }

        private EnumRecipeType _RecipeType;

        public EnumRecipeType RecipeType
        {
            get
            {
                return _RecipeType;
            }

            set
            {
                _RecipeType = value;
                NotifyOfPropertyChange(nameof(RecipeType));
            }
        }

        private string? _RecipePath;

        public string? RecipePath
        {
            get
            {
                return _RecipePath;
            }

            set
            {
                _RecipePath = value;
                NotifyOfPropertyChange(nameof(RecipePath));
            }
        }
    }
}