using System.Windows;

namespace ThermoPilotClient
{
    public partial class App : Application
    {
        private Bootstrapper? _bootstrapper;


        public App()
        {
            // 必须在 Application.Run() 之前创建，
            // Bootstrapper 的构造函数会调用 Initialize()，
            // 由它把 OnStartup 挂到 Application.Startup 事件上。
            _bootstrapper = new Bootstrapper();


        }
    }
}
