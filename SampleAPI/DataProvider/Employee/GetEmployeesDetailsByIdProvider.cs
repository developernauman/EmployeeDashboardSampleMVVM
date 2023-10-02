using Sample.Common.Response;
using Sample.EmployeeModule.Contract.Service;
using Sample.EmployeeModule.Models.Response;
using SampleAPI.DataProvider.Base;
using System.Threading.Tasks;

namespace SampleAPI.DataProvider.Employee
{
    public class GetEmployeesDetailsByIdProvider : EmployeeModuleBaseDIDataProvider<IEmployeeService, Response<EmployeeDetailsResponse>>
    {
        private long _employeeId;

        public GetEmployeesDetailsByIdProvider(long employeeId, string authHeader) : base(authHeader)
        {
            _employeeId = employeeId;
        }

        protected override async Task ExecuteBody()
        {
            Data = await Service.GetEmployeeDetailsById(_employeeId);
        }
    }
}
