using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using System.Windows.Input;

namespace EmployeeDashboardSample.UI.ViewModels
{
    public class PopUpViewModel : BindableBase, IDialogAware
    {
        private string title;
        public string Title
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }

        private object viewContent;
        public object ViewContent
        {
            get { return viewContent; }
            set { SetProperty(ref viewContent, value); }
        }


        private string okButtonText;
        public string OkButtonText
        {
            get { return okButtonText; }
            set { SetProperty(ref okButtonText, value); }
        }

        private string cancelButtonText;
        public string CancelButtonText
        {
            get { return cancelButtonText; }
            set { SetProperty(ref cancelButtonText, value); }
        }


        private ICommand okButtonCommand;        
        public ICommand OkButtonCommand
        {
            get { return okButtonCommand; }
            set { SetProperty(ref okButtonCommand, value); }
        }


        private ICommand cancelButtonCommand;
        public ICommand CancelButtonCommand
        {
            get { return cancelButtonCommand; }
            set { SetProperty(ref cancelButtonCommand, value); }
        }


        public event Action<IDialogResult> RequestClose;

        protected virtual void CloseDialog(string parameter)
        {
            ButtonResult result = ButtonResult.None;

            if (parameter?.ToLower() == "true")
                result = ButtonResult.OK;
            else if (parameter?.ToLower() == "false")
                result = ButtonResult.Cancel;

            RaiseRequestClose(new DialogResult(result));
        }

        public virtual void RaiseRequestClose(IDialogResult dialogResult)
        {
            RequestClose?.Invoke(dialogResult);
        }

        public virtual bool CanCloseDialog()
        {
            return true;
        }

        public virtual void OnDialogClosed()
        {

        }

        public virtual void OnDialogOpened(IDialogParameters parameters)
        {
            ViewContent = parameters.GetValue<object>("object");
        }
    }
}
