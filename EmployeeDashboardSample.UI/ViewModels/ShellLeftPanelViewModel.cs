using EmployeeDashboardSample.UI.IPresenters;
using EmployeeDashboardSample.UI.Views;
using GalaSoft.MvvmLight.Command;
using Microsoft.Practices.Unity;
using MVPVM;
using Prism.Ioc;
using Prism.Regions;
using Prism.Unity;
using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using Unity.Injection;

namespace EmployeeDashboardSample.UI.ViewModels
{
    public class ShellLeftPanelViewModel : BaseViewModel
    {
        IRegionManager regionManager;
        IContainerProvider containerProvider;

        public ShellLeftPanelViewModel(IRegionManager RegionManager, IContainerProvider ContainerProvider)
        {
            regionManager = RegionManager;
            containerProvider = ContainerProvider;
            EmployeesRecordCommand = new RelayCommand<string>(ShowOrHideEmployeesRecordsView);
        }


        ICommand employeesRecordCommand;
        public ICommand EmployeesRecordCommand
        {
            get { return employeesRecordCommand; }
            set { employeesRecordCommand = value; OnPropertyChanged(); }
        }


        void ShowOrHideEmployeesRecordsView(string param)
        {
            Utility.ShowView(regionManager, RegionNames.SHELL, containerProvider.Resolve<IEmployeeDetailsGridViewPresenter>().GetView());
        }
    }
}
