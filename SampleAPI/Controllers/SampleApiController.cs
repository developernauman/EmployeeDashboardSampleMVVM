using Microsoft.AspNetCore.Mvc;
using Sample.EmployeeModule.Models.Request;
using Sample.EmployeeModule.Models.Response;
using SampleAPI.Controllers.Base;
using SampleAPI.DataProvider.Employee;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SampleAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SampleApiController : BaseApiController
    {
        public SampleApiController() : base(typeof(SampleApiController))
        {
        }

        [HttpDelete("DeleteEmployeeById/{EmployeeId}")]
        [ProducesResponseType(typeof(Boolean), 200)]
        public async Task<IActionResult> DeleteEmployeeById(long employeeId)
        {
            return await Handle(new DeleteEmployeeByIdProvider(employeeId, string.Empty));
        }

        [HttpGet("GetEmployeeDetailsById/{EmployeeId}")]
        [ProducesResponseType(typeof(EmployeeDetailsResponse), 200)]
        public async Task<IActionResult> GetEmployeeDetailsById(long employeeId)
        {
            return await Handle(new GetEmployeesDetailsByIdProvider(employeeId, string.Empty));
        }

        [HttpGet("GetEmployeesDetails")]
        [ProducesResponseType(typeof(IEnumerable<EmployeeDetailsResponse>), 200)]
        public async Task<IActionResult> GetEmployeesDetails()
        {
            return await Handle(new GetEmployeesDetailsProvider(string.Empty));
        }

        [HttpPut("AddOrUpdateEmployee")]
        [ProducesResponseType(typeof(EmployeeDetailsResponse), 200)]
        public async Task<IActionResult> AddOrUpdateEmployee(EmployeeRequest employeeRequest)
        {
            return await Handle(new AddOrUpdateEmployeeProvider(employeeRequest, string.Empty));
        }
    }
}
