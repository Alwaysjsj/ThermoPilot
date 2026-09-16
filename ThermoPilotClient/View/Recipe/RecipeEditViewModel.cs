using System.Collections.ObjectModel;
using Core.Recipe;

namespace ThermoPilotClient.View.Recipe
{
    public class RecipeEditViewModel
    {
        public ObservableCollection<RecipeNodeItem>? RecipeNodes { get; set; }

        public RecipeNodeItem? SelectedNode { get; set; } //当前选中的Recipe节点

        public RecipeEditViewModel()
        {
            RecipeNodes = RecipeManager.Instance.GetRecipes();
        }

        public void New()
        {
            System.Diagnostics.Debug.WriteLine("Recipe New");
        }
    }
}
