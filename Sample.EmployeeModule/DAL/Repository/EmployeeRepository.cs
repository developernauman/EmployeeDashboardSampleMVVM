using Sample.Common.Response;
using Sample.EmployeeModule.Contract.Repository;
using Sample.EmployeeModule.DAL.Base;
using Sample.EmployeeModule.Models.Request;
using Sample.EmployeeModule.Models.Response;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sample.EmployeeModule.DAL.Repository
{
    public class EmployeeRepository : EmployeeModuleBaseRepository, IEmployeeRepository
    {
        public EmployeeRepository() : base(typeof(EmployeeRepository))
        {
        }

        public async Task<Response<Boolean>> DeleteEmployeeById(long employeeId)
        {
            return await ExecuteStoredProcSingleAsync<Boolean>(SP.DeleteEmployeeById, new { EmployeeId = employeeId });
        }

        public async Task<Response<IEnumerable<EmployeeDetailsResponse>>> GetEmployeesDetails()
        {
            return await ExecuteStoredProcListAsync<EmployeeDetailsResponse>(SP.GetEmployeesDetails, null);
        }

        public async Task<Response<EmployeeDetailsResponse>> GetEmployeeDetailsById(long employeeId)
        {
            return await ExecuteStoredProcSingleAsync<EmployeeDetailsResponse>(SP.GetEmployeeDetailsById, new { EmployeeId = employeeId });
        }

        public async Task<Response<EmployeeDetailsResponse>> AddOrUpdateEmployee(EmployeeRequest employee)
        {
            return await ExecuteStoredProcSingleAsync<EmployeeDetailsResponse>(SP.AddOrUpdateEmployee, employee);
        }
    }
}
