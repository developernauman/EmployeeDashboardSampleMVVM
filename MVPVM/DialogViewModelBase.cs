using Prism.Services.Dialogs;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MVPVM
{
    public class DialogViewModelBase : INotifyPropertyChanged, IDialogAware
    {
        private string _title;
        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
                OnPropertyChanged();
            }
        }

        private string _iconSource;
        public string IconSource
        {
            get { return _iconSource; }
            set
            {
                _iconSource = value;
                OnPropertyChanged();
            }
        }

        public event Action<IDialogResult> RequestClose;
        public event PropertyChangedEventHandler PropertyChanged;

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

        }

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
