using UI.Common;

namespace ThermoPilotClient.View.Alarm
{
    public class AlarmViewModel
        : ModuleViewModelBase
    {

        public AlarmViewModel()
        {
            DisplayName = "Alarm";
        }


        public override async Task InitializeAsync()
        {
            await Task.CompletedTask;
        }

    }
}
