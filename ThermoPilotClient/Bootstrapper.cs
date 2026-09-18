using System.Windows;
using Caliburn.Micro;
using ThermoPilotClient.Services;
using ThermoPilotClient.View.Alarm;
using ThermoPilotClient.View.Device;
using ThermoPilotClient.View.Recipe;
using ThermoPilotClient.View.Shell;

namespace ThermoPilotClient
{
    public class Bootstrapper : BootstrapperBase
    {
        private readonly SimpleContainer _container;


        public Bootstrapper()
        {
            _container = new SimpleContainer();

            Initialize();
        }


        protected override void Configure()
        {
            _container.Singleton<IWindowManager, ShellWindowManager>();

            _container.Singleton<ShellViewModel>();

            _container.Singleton<RecipeEditViewModel>();

            _container.Singleton<NavigationService>();

            _container.Singleton<MenuService>();

            _container.Singleton<DeviceViewModel>();

            _container.Singleton<AlarmViewModel>();

            _container.Instance(_container);
        }


        protected override async void OnStartup(
            object sender,
            StartupEventArgs e)
        {
            await DisplayRootViewForAsync<ShellViewModel>();
        }


        protected override object GetInstance(
            Type service,
            string key)
        {
            return _container.GetInstance(service, key);
        }


        protected override IEnumerable<object> GetAllInstances(
            Type service)
        {
            return _container.GetAllInstances(service);
        }
    }
}
