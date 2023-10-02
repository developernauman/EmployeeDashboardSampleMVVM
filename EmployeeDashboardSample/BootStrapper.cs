using EmployeeDashboardSample.UI;
using EmployeeDashboardSample.UI.IPresenters;
using EmployeeDashboardSample.UI.PopUp;
using EmployeeDashboardSample.UI.Presenters;
using EmployeeDashboardSample.UI.ViewModels;
using EmployeeDashboardSample.UI.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Unity;
using System;
using System.Collections.Generic;
using System.Windows;
using Unity;

namespace EmployeeDashboardSample
{
    public class BootStrapper : PrismBootstrapper
    {
        #region Overridden Methods 

        protected override Window CreateShell()
        {
            return Container.Resolve<Shell>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            var container = containerRegistry.GetContainer();

            containerRegistry.RegisterForNavigation<WelcomeScreen>();
            containerRegistry.RegisterForNavigation<ShellLeftPanel>();

            containerRegistry.Register<IEmployeeDetailsGridViewPresenter>(() => container.Resolve<EmployeeDetailsGridViewPresenter>());

            containerRegistry.RegisterDialog<PopUpView, PopUpViewModel>();
            containerRegistry.RegisterDialog<EmployeeAddOrUpdate, EmployeeAddOrUpdateViewModel>("EmployeeAddOrUpdatePopUp");
        }

        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            List<Type> moduleTypes = new List<Type>()
            {
                typeof(UIModuleLocator)
            };

            foreach (var moduleLocatorType in moduleTypes)
                moduleCatalog.AddModule(moduleLocatorType);

            base.ConfigureModuleCatalog(moduleCatalog);
        }

        #endregion
    }
}
