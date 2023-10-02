using EmployeeDashboardSample.UI.APIModelsMapper;
using EmployeeDashboardSample.UI.IPresenters;
using EmployeeDashboardSample.UI.ViewModels;
using EmployeeDashboardSample.UI.Views;
using MVPVM;
using Prism.Commands;
using Prism.Services.Dialogs;
using System.Threading.Tasks;
using System.Windows;

namespace EmployeeDashboardSample.UI.Presenters
{
    public class EmployeeDetailsGridViewPresenter : BasePresenter<EmployeeDetailsGrid, EmployeeDetailsGridViewModel>, IEmployeeDetailsGridViewPresenter
    {
        IDialogService _dialogService;

        public EmployeeDetailsGridViewPresenter(IDialogService dialogService) : base()
        {
            _dialogService = dialogService;

            LoadData();
            ViewModel.AddEmployeeButtonCommand = new DelegateCommand<string>(AddUpdateOrDeleteEvent);
        }


        async void LoadData()
        {
            var result = await APIMapper.GetEmployeesDetails();
            if (result != null)
            {
                ViewModel.GridButtonCommand = new DelegateCommand<string>(AddUpdateOrDeleteEvent);
                ViewModel.LoadDataIntoGrid(result);
            }
        }

        void AddUpdateOrDeleteEvent(string commandParameter)
        {
            if (commandParameter == "Add")
            {
                OpenPopUp("Add", new DialogParameters
                {
                    { EmployeeAddOrUpdateKeys.OKButtonTextKey, "Add" },
                    { EmployeeAddOrUpdateKeys.TitleKey, "Add Employee PopUp" }
                });
            }
            else if (commandParameter == "Update")
            {
                OpenPopUp("Update", new DialogParameters
                {
                    { EmployeeAddOrUpdateKeys.OKButtonTextKey, "Update" },
                    { EmployeeAddOrUpdateKeys.TitleKey, "Update Employee PopUp" },
                    { EmployeeAddOrUpdateKeys.EmployeeDetailsKey, ViewModel.SelectedEmployee }
                });
            }
            else if (commandParameter == "Delete")
            {
                MessageBoxResult messageBoxResult = MessageBox.Show(string.Format("Are you sure to delete this Employee {0}?", ViewModel.SelectedEmployee.Name), 
                                                                    "Delete Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (messageBoxResult == MessageBoxResult.Yes)
                    DeleteEmployeeAsync();
            }
        }

        async Task DeleteEmployeeAsync()
        {
            var result = await APIMapper.DeleteEmployeeById(ViewModel.SelectedEmployee.Id);
            if (result)
            {
                ViewModel.SelectedEmployee.IsDeleted = true;
                ViewModel.UpdateGridRow(ViewModel.SelectedEmployee);
            }
        }

        void OpenPopUp(string actionFlag, IDialogParameters dialogParams)
        {
            _dialogService.ShowDialog("EmployeeAddOrUpdatePopUp", dialogParams, r =>
            {
                if (r.Result == ButtonResult.OK)
                    AddOrUpdateEmployeeDetails(actionFlag, r.Parameters.GetValue<EmployeeDetails>(EmployeeAddOrUpdateKeys.EmployeeDetailsKey));
            });
        }

        async void AddOrUpdateEmployeeDetails(string key, EmployeeDetails employeeDetails)
        {
            var result = await APIMapper.AddOrUpdateEmployeeDetails(employeeDetails);
            if (result != null)
            {
                if (key == "Update")
                    ViewModel.UpdateGridRow(result);
                else
                    ViewModel.InsertGridRow(result);
            }
        }


    }
}
