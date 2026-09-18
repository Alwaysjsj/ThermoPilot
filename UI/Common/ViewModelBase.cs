using Caliburn.Micro;

namespace UI.Common;

public class ViewModelBase : Screen, IDisposable
{
    private CancellationTokenSource? _cancellationTokenSource; //后台任务停止开关

    public virtual void Active() //virtual表示子类以后可以“重写”这个方法
    {
        _cancellationTokenSource = new CancellationTokenSource(); //创建一个新的“取消控制器”
    }

    public void Dispose()
    {
        _cancellationTokenSource?.Dispose(); //这个CancellationTokenSource已经不用了，把它占用的资源释放掉

        _cancellationTokenSource = null; //清空字段
    }

    public virtual void Deactivate()
    {
        _cancellationTokenSource?.Cancel(); //告诉后台任务：“请停止”
    }

    private string? _moduleName;

    public string? ModuleName
    {
        get => _moduleName;

        set
        {
            _moduleName = value;
            NotifyOfPropertyChange(nameof(ModuleName));
        }
    }

    private string? _headerName;

    public string? Header_Name
    {
        get => _headerName;

        set
        {
            _headerName = value;
            NotifyOfPropertyChange(nameof(Header_Name));
        }
    }

}
