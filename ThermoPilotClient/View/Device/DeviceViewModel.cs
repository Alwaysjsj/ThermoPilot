using UI.Common;

namespace ThermoPilotClient.View.Device
{
    public class DeviceViewModel
        : ModuleViewModelBase
    {

        public DeviceViewModel()
        {
            DisplayName = "Device";
        }


        public override async Task InitializeAsync()
        {
            await Task.CompletedTask;
        }

    }
}
