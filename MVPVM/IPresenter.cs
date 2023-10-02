using System.Windows.Controls;

namespace MVPVM
{
    public interface IPresenter
    {
        Control GetView();
    }
}
