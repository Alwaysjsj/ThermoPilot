# ThermoPilot 项目长期笔记

## 架构
- WPF (.NET 8, x86) + Caliburn.Micro 4.0.212（SimpleContainer + Screen/ViewModelBinder）。
- 三个工程：`Core`（实体/配置/Recipe，无 UI）、`UI`（主题样式/转换器/ViewModelBase）、
  `ThermoPilotClient`（启动入口 App + Bootstrapper + View/ViewModel）。
- 启动链路：`App` 构造函数中 `new Bootstrapper()` → CM `Initialize()` 挂
  `Application.Startup` → `OnStartup` 里 `DisplayRootViewForAsync<HomeViewModel>()` →
  `HomeView`(ContentControl + cal:View.Model) → `CurrentView` = `RecipeEditViewModel`。

## 重要约定 / 踩坑
- **不要**把 Bootstrapper 写进 App.xaml 的 `Application.Resources`：
  .NET Core 3.0+ WPF 对 App.xaml 资源延迟实例化，对象不会被构造，CM 不会启动，
  表现为进程活着但无任何窗口（已在 2026-09-18 踩过并修复）。
- View 的构造函数里不要手动 `DataContext = new XxxViewModel()`，交给 CM 绑定，
  否则容器单例注册失效。
- CM 的 `Screen.DisplayName` 会被写到窗口标题栏，默认是类型全名，需显式设置。
- 根视图（UserControl）无固有尺寸，需给 Width/Height/MinWidth/MinHeight，
  否则窗口尺寸不可控。
- **CM 生成的根窗口是 `SizeToContent = WidthAndHeight`**，即窗口跟着内容"想要"的
  尺寸走。若根视图不给显式尺寸，而内容里有 DataGrid（含 `Width="*"` 列）这类
  在无限宽度下会失控膨胀的控件，窗口会被撑到 3500px 以上（比屏幕还宽）。
- **窗口尺寸的正确做法**：不要在根视图上写 Width/Height（会导致最大化时内容不拉伸），
  而是用 `Services/ShellWindowManager.cs`（继承 CM 的 `WindowManager`，重写
  `EnsureWindow`）把 `SizeToContent` 改成 `Manual` 并设置尺寸/最小尺寸/居中，
  在 `Bootstrapper.Configure()` 里注册为 `IWindowManager`。
  当前值：1280x800，最小 1024x640，启动居中。
- 配置：`Config/SystemConfig.json`、`Config/RecipeConfig.json`（CopyToOutputDirectory），
  运行时缓存在 `Cache/SystemConfig.bin`；Recipe 目录在输出目录 `Recipes/`。
- 全局异常会写入输出目录 `Logs/Crash.log`。

## 本机显示环境
- 主屏 2560x1440，DPI 120（125% 缩放）；WPF 侧 2048x1152 DIP，工作区 2048x1104 DIP。
- 调试窗口尺寸时不要依赖 Python 截图做像素推断（DPI 虚拟化会只截到局部），
  直接在代码里打印 `Window.ActualWidth` / `SystemParameters.WorkArea` 最可靠。

## 环境备忘（本机）
- bash PATH 易被破坏：命令前 `export PATH="/usr/bin:/bin:$PATH"`。
- .NET SDK 9.0.317；项目 net8.0-windows + x86，控制台方式调试需用
  `/c/Program Files (x86)/dotnet/dotnet.exe`（x64 宿主会报 FileLoadException）。
- GUI 自动化验证：Python ctypes（EnumWindows / GetWindowRect / PrintWindow）+ Pillow；
  PowerShell 的 Add-Type 被安全策略拦截，不能用来枚举窗口。
