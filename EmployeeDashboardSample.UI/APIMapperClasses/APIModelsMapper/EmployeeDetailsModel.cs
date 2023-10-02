using MVPVM;
using System;

namespace EmployeeDashboardSample.UI.APIModelsMapper
{
    public class EmployeeDetails
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DOB { get; set; }
        public string StatusName { get; set; }
        public bool Status { get { return StatusName == Constants.ACTIVE ? true : false; } }
        public bool IsDeleted { get; set; }

        public string Name { get { return !string.IsNullOrWhiteSpace(FirstName) && !string.IsNullOrWhiteSpace(LastName) ? string.Format("{0} {1}", FirstName, LastName) : FirstName; } }
    }
}
