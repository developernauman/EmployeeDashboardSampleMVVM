using EmployeeDashboardSample.UI.APIModelsMapper;
using MVPVM;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace EmployeeDashboardSample.UI.ViewModels
{
    public class EmployeeDetailsGridViewModel : BaseViewModel
    {
        ICommand gridButtonCommand;
        public ICommand GridButtonCommand
        {
            get { return gridButtonCommand; }
            set
            {
                gridButtonCommand = value;
                OnPropertyChanged();
            }
        }


        ICommand addEmployeeButtonCommand;
        public ICommand AddEmployeeButtonCommand
        {
            get { return addEmployeeButtonCommand; }
            set
            {
                addEmployeeButtonCommand = value;
                OnPropertyChanged();
            }
        }


        EmployeeDetails selectedEmployee;
        public EmployeeDetails SelectedEmployee
        {
            get { return selectedEmployee; }
            set
            {
                selectedEmployee = value;
                OnPropertyChanged();
            }
        }


        ObservableCollection<EmployeeDetails> employeesList;
        public ObservableCollection<EmployeeDetails> EmployeesList
        {
            get { return employeesList; }
            set
            {
                employeesList = value;
                OnPropertyChanged();
            }
        }


        public void InsertGridRow(EmployeeDetails employeeDetails)
        {
            if (employeeDetails != null)
            {
                if (EmployeesList == null)
                    EmployeesList = new ObservableCollection<EmployeeDetails>();

                EmployeesList.Insert(employeesList.Count, employeeDetails);
            }
        }

        public void UpdateGridRow(EmployeeDetails employeeDetails)
        {
            if (EmployeesList != null && employeesList.Count > 0 && employeeDetails != null)
            {
                var employee = EmployeesList.FirstOrDefault(item => item.Id == employeeDetails.Id);
                if (employee != null)
                {
                    if (employeeDetails.IsDeleted)
                    {
                        EmployeesList.Remove(employee);
                    }
                    else
                    {
                        var index = EmployeesList.IndexOf(employee);

                        EmployeesList.RemoveAt(index);
                        EmployeesList.Insert(index, employeeDetails);

                        SelectedEmployee = employeeDetails;
                    }
                }
            }
        }

        public void LoadDataIntoGrid(IEnumerable<EmployeeDetails> employeeDetails)
        {
            if (EmployeesList == null)
                EmployeesList = new ObservableCollection<EmployeeDetails>();

            EmployeesList.Clear();
            EmployeesList.AddRange(employeeDetails);
        }
    }
}
