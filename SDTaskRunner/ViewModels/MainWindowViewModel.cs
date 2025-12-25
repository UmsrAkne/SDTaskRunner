using Prism.Mvvm;
using SDTaskRunner.Utils;

namespace SDTaskRunner.ViewModels;

public class MainWindowViewModel : BindableBase
{
    private readonly AppVersionInfo appVersionInfo = new();

    public string Title => appVersionInfo.GetAppNameWithVersion();
}