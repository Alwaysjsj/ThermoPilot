using System.Windows.Controls;

namespace ThermoPilotClient.View.Recipe
{
    /// <summary>
    /// RecipeEditView.xaml 的交互逻辑
    /// </summary>
    public partial class RecipeEditView : UserControl
    {
        public RecipeEditView()
        {
            InitializeComponent();

            DataContext = new RecipeEditViewModel();
        }
    }
}
