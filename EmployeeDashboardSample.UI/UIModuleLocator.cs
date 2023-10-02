using EmployeeDashboardSample.UI.Views;
using Microsoft.Practices.Unity;
using MVPVM;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace EmployeeDashboardSample.UI
{
    public class UIModuleLocator : IModule
    {
        public void Initialize()
        {
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {

        }

        public void OnInitialized(IContainerProvider containerProvider)
        {
            var container = containerProvider.Resolve<IRegionManager>();
            container.RequestNavigate(RegionNames.SHELL, nameof(WelcomeScreen));
            container.RequestNavigate(RegionNames.LEFTPANEL, nameof(ShellLeftPanel));

            container = null;
        }
    }
}
