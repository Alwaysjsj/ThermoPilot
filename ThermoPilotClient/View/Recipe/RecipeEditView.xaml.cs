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

            // DataContext 由 Caliburn.Micro 的 ViewModelBinder 绑定，
            // 不要在这里 new 一个 ViewModel：那样会绕开 IoC 容器再造一个实例，
            // 使 RecipeEditViewModel 的单例注册失效。
        }
    }
}
