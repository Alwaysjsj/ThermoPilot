using Core.Common;

namespace Core.Entities
{
    public class CheckResultEntity : PropertyChangedBase
    {
        private string? _No;

        public string? No
        {
            get
            {
                return _No;
            }

            set
            {
                _No = value;
                NotifyOfPropertyChange(nameof(No));
            }
        }


        private string? _RecipeType;

        public string? RecipeType
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


        private string? _Comments;

        public string? Comments
        {
            get
            {
                return _Comments;
            }

            set
            {
                _Comments = value;
                NotifyOfPropertyChange(nameof(Comments));
            }
        }
    }
}