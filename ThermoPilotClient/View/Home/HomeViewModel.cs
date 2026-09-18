using Caliburn.Micro;
using ThermoPilotClient.Services;
using ThermoPilotClient.View.Recipe;
using UI.Common;

namespace ThermoPilotClient.View.Home
{
    public class HomeViewModel : ViewModelBase
    {
        private readonly NavigationService _navigationService;


        private Screen? _currentView;


        public Screen? CurrentView
        {
            get => _currentView;

            set
            {
                _currentView = value;

                NotifyOfPropertyChange(nameof(CurrentView));
            }
        }


        public HomeViewModel(
            NavigationService navigationService)
        {
            _navigationService = navigationService;

            // Screen.DisplayName 默认是整个类型全名，
            // Caliburn.Micro 会把它写到窗口标题栏上。
            DisplayName = "ThermoPilot";


            CurrentView =
                _navigationService.Navigate<RecipeEditViewModel>();
        }
    }
}
