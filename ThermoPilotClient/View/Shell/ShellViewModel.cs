using System.Collections.ObjectModel;
using Caliburn.Micro;
using ThermoPilotClient.Entities;
using ThermoPilotClient.Services;
using ThermoPilotClient.View.Recipe;
using UI.Common;

namespace ThermoPilotClient.View.Shell
{
    public class ShellViewModel : ViewModelBase
    {
        private readonly NavigationService _navigationService;

        private readonly MenuService _menuService;


        private Screen? _currentView;


        public ObservableCollection<MenuItemEntity> MenuItems { get; }



        public Screen? CurrentView
        {
            get => _currentView;

            set
            {
                _currentView = value;

                NotifyOfPropertyChange(
                    nameof(CurrentView));
            }
        }



        public ShellViewModel(
            NavigationService navigationService,
            MenuService menuService)
        {
            _navigationService = navigationService;

            _menuService = menuService;


            DisplayName = "ThermoPilot";


            MenuItems =
                _menuService.GetMenus();


            CurrentView =
                _navigationService.Navigate<RecipeEditViewModel>();
        }



        public async void Navigate(
            MenuItemEntity? menu)
        {
            if (menu == null)
                return;


            CurrentView =
                await _navigationService.NavigateAsync(
                    GetViewModelType(menu.ViewModelType));
        }



        private Type GetViewModelType(
            string name)
        {
            return AppDomain.CurrentDomain
                .GetAssemblies()
                .SelectMany(x => x.GetTypes())
                .First(x => x.Name == name);
        }
    }
}
