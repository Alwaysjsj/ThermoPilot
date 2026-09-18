using System.Collections.ObjectModel;
using Core.Recipe;
using UI.Common;

namespace ThermoPilotClient.View.Recipe
{
    public class RecipeEditViewModel : ViewModelBase
    {
        private ObservableCollection<RecipeNodeItem>? _recipeNodes;

        private RecipeNodeItem? _selectedNode;


        public ObservableCollection<RecipeNodeItem>? RecipeNodes
        {
            get => _recipeNodes;

            set
            {
                _recipeNodes = value;
                NotifyOfPropertyChange(nameof(RecipeNodes));
            }
        }

        public RecipeNodeItem? SelectedNode
        {
            get => _selectedNode;

            set
            {
                _selectedNode = value;
                NotifyOfPropertyChange(nameof(SelectedNode));
            }
        }


        public RecipeEditViewModel()
        {
            ModuleName = "Recipe";

            Header_Name = "Recipe Edit";

            RecipeNodes = RecipeManager.Instance.GetRecipes();
        }
    }
}
