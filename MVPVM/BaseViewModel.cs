using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MVPVM
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        protected bool isViewDirty;

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name)); 
            isViewDirty = true;
        }
    }
}
