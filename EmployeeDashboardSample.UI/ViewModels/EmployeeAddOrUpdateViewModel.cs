using EmployeeDashboardSample.UI.APIModelsMapper;
using GalaSoft.MvvmLight.Command;
using MVPVM;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Windows.Input;

namespace EmployeeDashboardSample.UI.ViewModels
{
    public class StatusComboViewModel
    {
        public int Status { get; set; }
        public string StatusName { get; set; }

        public override string ToString() => StatusName;
    }

    public static class EmployeeAddOrUpdateKeys
    {
        public const string TitleKey = "title";
        public const string OKButtonTextKey = "okButtonText";
        public const string EmployeeDetailsKey = "employeeDetails";
    }

    public class EmployeeAddOrUpdateViewModel : DirtyViewModel, IDialogAware
    {
        #region Employee Properties

        long employeeid;

        string firstName;
        [Required(ErrorMessage = "Field 'FirstName' is required.")]
        public string FirstName
        {
            get { return firstName; }
            set
            {
                firstName = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(CanOkTrigger));
            }
        }

        string lastName;
        [Required(ErrorMessage = "Field 'LastName' is required.")]
        public string LastName
        {
            get { return lastName; }
            set
            {
                lastName = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(CanOkTrigger));
            }
        }

        DateTime _DOB = DateTime.Now.AddYears(-18);
        public DateTime DOB
        {
            get { return _DOB; }
            set
            {
                _DOB = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(CanOkTrigger));
            }
        }

        string statusName;
        public string StatusName
        {
            get { return statusName; }
            set
            {
                statusName = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(CanOkTrigger));
            }
        }

        public IList<StatusComboViewModel> StatusList
        {
            get
            {
                return new List<StatusComboViewModel>
                {
                    new StatusComboViewModel { Status = 1, StatusName = Constants.ACTIVE },
                    new StatusComboViewModel { Status = 0, StatusName = Constants.INACTIVE }
                };
            }
        }

        public bool CanOkTrigger
        {
            get { return IsViewDirty && !string.IsNullOrEmpty(FirstName) && !string.IsNullOrEmpty(LastName) && !string.IsNullOrEmpty(StatusName); }
        }

        #endregion


        private string title;
        public string Title
        {
            get { return title; }
            set
            {
                SetProperty(ref title, value);
            }
        }


        string okButtonText;
        public string OkButtonText
        {
            get { return okButtonText; }
            set
            {
                SetProperty(ref okButtonText, value);
            }
        }


        public event Action<IDialogResult> RequestClose;


        private ICommand okButtonCommand;
        public ICommand OkButtonCommand => okButtonCommand ?? (okButtonCommand = new RelayCommand(() => CloseDialog("True")));

        private ICommand cancelButtonCommand;
        public ICommand CancelButtonCommand => cancelButtonCommand ?? (cancelButtonCommand = new RelayCommand<string>(CloseDialog));


        public virtual bool CanCloseDialog()
        {
            return true;
        }

        public virtual void OnDialogClosed()
        {

        }

        protected virtual void CloseDialog(string parameter)
        {
            ButtonResult result = ButtonResult.None;
            IDialogParameters dialogReturnParams = null;

            if (parameter?.ToLower() == "true")
            {
                result = ButtonResult.OK;
                dialogReturnParams = new DialogParameters
                {
                    { "employeeDetails", GetEmployeeDetails() }
                };
            }
            else if (parameter?.ToLower() == "false")
                result = ButtonResult.Cancel;

            RaiseRequestClose(new DialogResult(result, dialogReturnParams));
        }

        public virtual void OnDialogOpened(IDialogParameters parameters)
        {
            Title = parameters.GetValue<string>(EmployeeAddOrUpdateKeys.TitleKey);
            OkButtonText = parameters.GetValue<string>(EmployeeAddOrUpdateKeys.OKButtonTextKey);

            if (parameters.ContainsKey(EmployeeAddOrUpdateKeys.EmployeeDetailsKey))
                SetEmployeeDetails(parameters.GetValue<EmployeeDetails>(EmployeeAddOrUpdateKeys.EmployeeDetailsKey));
        }

        public virtual void RaiseRequestClose(IDialogResult dialogResult)
        {
            RequestClose?.Invoke(dialogResult);
        }


        EmployeeDetails GetEmployeeDetails()
        {
            return new EmployeeDetails
            {
                DOB = DOB,
                Id = employeeid,
                FirstName = FirstName,
                LastName = LastName,
                StatusName = StatusName
            };
        }

        void SetEmployeeDetails(EmployeeDetails employeeDetails)
        {
            DOB = employeeDetails.DOB;
            employeeid = employeeDetails.Id;
            FirstName = employeeDetails.FirstName;
            LastName = employeeDetails.LastName;
            StatusName = employeeDetails.StatusName;
            IsViewDirty = false;
            OnPropertyChanged(nameof(CanOkTrigger));
        }
    }
}
