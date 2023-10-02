using System;
using System.ComponentModel.DataAnnotations;

namespace Sample.EmployeeModule.Models.Request
{
    public class EmployeeRequest : CommonFields
    {
        [MaxLength(100)]
        public string FirstName { get; set; }

        [MaxLength(100)]
        public string LastName { get; set; }
        public DateTime DOB { get; set; }
        public bool Status { get; set; }
        public bool IsDeleted { get; set; }
    }
}
