using System.Windows;
using Caliburn.Micro;

namespace ThermoPilotClient
{
    public class Bootstrapper : BootstrapperBase
    {
        public Bootstrapper()
        {
            Initialize();
        }

        protected override async void OnStartup(object sender, StartupEventArgs e)
        {
            await DisplayRootViewForAsync<HomeViewModel>();
        }
    }
}
