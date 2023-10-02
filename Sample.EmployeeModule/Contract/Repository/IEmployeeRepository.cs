using Sample.Common.Response;
using Sample.EmployeeModule.Models.Request;
using Sample.EmployeeModule.Models.Response;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sample.EmployeeModule.Contract.Repository
{
    public interface IEmployeeRepository
    {
        Task<Response<Boolean>> DeleteEmployeeById(long employeeId);
        Task<Response<IEnumerable<EmployeeDetailsResponse>>> GetEmployeesDetails();
        Task<Response<EmployeeDetailsResponse>> GetEmployeeDetailsById(long employeeId);
        Task<Response<EmployeeDetailsResponse>> AddOrUpdateEmployee(EmployeeRequest employee);
    }
}
