using System;

namespace Sample.EmployeeModule.Models.Response
{
    public class EmployeeDetailsResponse : CommonFields
    {
        public string StatusName
        {
            set { Status = StatusName == Constants.ACTIVE ? true : false; }
            get { return Status ? Constants.ACTIVE : Constants.INACTIVE; }
        }
        
        public DateTime DOB { get; set; }

        public bool IsDeleted { get; set; }

        private bool Status { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
