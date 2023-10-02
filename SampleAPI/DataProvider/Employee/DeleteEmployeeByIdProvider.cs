using Sample.Common.Response;
using Sample.EmployeeModule.Contract.Service;
using SampleAPI.DataProvider.Base;
using System;
using System.Threading.Tasks;

namespace SampleAPI.DataProvider.Employee
{
    public class DeleteEmployeeByIdProvider : EmployeeModuleBaseDIDataProvider<IEmployeeService, Response<Boolean>>
    {
        private long _employeeId;

        public DeleteEmployeeByIdProvider(long employeeId, string authHeader) : base(authHeader)
        {
            _employeeId = employeeId;
        }

        protected override async Task ExecuteBody()
        {
            Data = await Service.DeleteEmployeeById(_employeeId);
        }
    }
}
