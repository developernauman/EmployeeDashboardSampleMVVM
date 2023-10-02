using Sample.Common.BAL;
using Sample.Common.Response;
using Sample.EmployeeModule.Contract.Repository;
using Sample.EmployeeModule.Contract.Service;
using Sample.EmployeeModule.Models.Request;
using Sample.EmployeeModule.Models.Response;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sample.EmployeeModule.BAL.Service
{
    public class EmployeeService : BaseService, IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeService(IEmployeeRepository employeeRepository) : base(typeof(IEmployeeRepository))
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<Response<Boolean>> DeleteEmployeeById(long employeeId)
        {
            return await RunMethodAsync(async () => await _employeeRepository.DeleteEmployeeById(employeeId));
        }

        public async Task<Response<IEnumerable<EmployeeDetailsResponse>>> GetEmployeesDetails()
        {
            return await RunMethodAsync(async () => await _employeeRepository.GetEmployeesDetails());
        }

        public async Task<Response<EmployeeDetailsResponse>> GetEmployeeDetailsById(long employeeId)
        {
            return await RunMethodAsync(async () => await _employeeRepository.GetEmployeeDetailsById(employeeId));
        }

        public async Task<Response<EmployeeDetailsResponse>> AddOrUpdateEmployee(EmployeeRequest employee)
        {
            return await RunMethodAsync(async () => await _employeeRepository.AddOrUpdateEmployee(employee));
        }
    }
}
