namespace UI.Common
{
    public abstract class ModuleViewModelBase : ViewModelBase
    {
        public virtual Task InitializeAsync()
        {
            return Task.CompletedTask;
        }


        public virtual Task CloseAsync()
        {
            return Task.CompletedTask;
        }
    }
}
