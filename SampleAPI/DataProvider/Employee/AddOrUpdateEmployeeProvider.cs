using Sample.Common.Response;
using Sample.EmployeeModule.Contract.Service;
using Sample.EmployeeModule.Models.Request;
using Sample.EmployeeModule.Models.Response;
using SampleAPI.DataProvider.Base;
using System.Threading.Tasks;

namespace SampleAPI.DataProvider.Employee
{
    public class AddOrUpdateEmployeeProvider : EmployeeModuleBaseDIDataProvider<IEmployeeService, Response<EmployeeDetailsResponse>>
    {
        private EmployeeRequest _employee;

        public AddOrUpdateEmployeeProvider(EmployeeRequest employee, string authHeader) : base(authHeader)
        {
            _employee = employee;
        }

        protected override async Task ExecuteBody()
        {
            Data = await Service.AddOrUpdateEmployee(_employee);
        }
    }
}
