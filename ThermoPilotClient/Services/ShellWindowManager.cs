using System.Windows;
using Caliburn.Micro;

namespace ThermoPilotClient.Services
{
    /// <summary>
    /// 接管 Caliburn.Micro 生成外壳窗口时的尺寸策略。
    ///
    /// CM 默认给生成的窗口设 SizeToContent = WidthAndHeight，窗口会跟着内容
    /// "想要"的尺寸走。内容区里有 DataGrid（含 Width="*" 列）这种在无限宽度下
    /// 测量会失控膨胀的控件，窗口就会被撑到 3500px 以上、比屏幕还宽；
    /// 而如果把尺寸写死在根视图上，窗口最大化时内容又不会跟着拉伸。
    ///
    /// 所以这里统一改成：窗口自己定尺寸（SizeToContent = Manual），
    /// 根视图不给尺寸、由窗口拉伸填满。
    /// </summary>
    public class ShellWindowManager : WindowManager
    {
        private const double DefaultWidth = 1280;

        private const double DefaultHeight = 800;

        private const double DefaultMinWidth = 1024;

        private const double DefaultMinHeight = 640;


        protected override Window EnsureWindow(
            object model,
            object view,
            bool isDialog)
        {
            Window window =
                base.EnsureWindow(
                    model,
                    view,
                    isDialog);

            // view 本身就是 Window（例如 NewRecipeWindow）时说明这个窗口是手写的，
            // 不动它的尺寸策略，只处理 CM 自动生成的外壳窗口。
            if (view is not Window)
            {
                window.SizeToContent =
                    SizeToContent.Manual;

                window.Width = DefaultWidth;

                window.Height = DefaultHeight;

                window.MinWidth = DefaultMinWidth;

                window.MinHeight = DefaultMinHeight;

                window.WindowStartupLocation =
                    WindowStartupLocation.CenterScreen;
            }

            return window;
        }
    }
}
