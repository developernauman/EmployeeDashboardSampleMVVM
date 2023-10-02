using Sample.Common.Response;
using Sample.EmployeeModule.Contract.Service;
using Sample.EmployeeModule.Models.Response;
using SampleAPI.DataProvider.Base;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SampleAPI.DataProvider.Employee
{
    public class GetEmployeesDetailsProvider : EmployeeModuleBaseDIDataProvider<IEmployeeService, Response<IEnumerable<EmployeeDetailsResponse>>>
    {
        public GetEmployeesDetailsProvider(string authHeader) : base(authHeader)
        {

        }

        protected override async Task ExecuteBody()
        {
            Data = await Service.GetEmployeesDetails();
        }
    }
}
