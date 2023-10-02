using EmployeeDashboardSample.UI.IViews;
using System.Windows.Controls;

namespace EmployeeDashboardSample.UI.Views
{
    /// <summary>
    /// Interaction logic for EmployeeDetailsGrid.xaml
    /// </summary>
    public partial class EmployeeDetailsGrid : UserControl, IEmployeeDetailsGrid
    {
        public EmployeeDetailsGrid()
        {
            InitializeComponent();            
        }
    }
}
