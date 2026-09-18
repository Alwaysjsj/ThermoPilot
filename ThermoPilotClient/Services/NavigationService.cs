using Caliburn.Micro;
using UI.Common;

namespace ThermoPilotClient.Services
{
    public class NavigationService
    {
        private readonly SimpleContainer _container;


        public NavigationService(
            SimpleContainer container)
        {
            _container = container;
        }


        //固定类型导航
        public Screen Navigate<T>()
        {
            return _container.GetInstance(
                typeof(T),
                null) as Screen
                ?? throw new Exception(
                    $"无法创建:{typeof(T).Name}");
        }



        //Type导航
        public Screen Navigate(Type viewModelType)
        {
            return _container.GetInstance(
                viewModelType,
                null) as Screen
                ?? throw new Exception(
                    $"无法创建:{viewModelType.Name}");
        }



        //生命周期导航
        public async Task<Screen> NavigateAsync(
            Type viewModelType)
        {
            var viewModel =
                _container.GetInstance(
                    viewModelType,
                    null) as Screen;


            if (viewModel == null)
            {
                throw new Exception(
                    $"无法创建:{viewModelType.Name}");
            }


            if (viewModel is ModuleViewModelBase module)
            {
                await module.InitializeAsync();
            }


            return viewModel;
        }
    }
}
