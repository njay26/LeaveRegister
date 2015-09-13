using System;
using System.Web.UI.WebControls;

namespace LeaveRegister.Models
{
    public class Employee
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DateofBirth { get; set; }
        public string EmailId { get; set; } 
        public string EmployeeId { get; set; }
        public string Designation { get; set; }
        public string DateOfJoining { get; set; }
        public string Company { get; set; }
        public Image ProfileImage { get; set; }
        public string StatusText { get; set; }
    }

    public class EmployeesListForAttendance
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmployeeId { get; set; }
        public string Designation { get; set; }  
    }
}