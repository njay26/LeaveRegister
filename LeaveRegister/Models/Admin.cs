using System.Runtime.Serialization;

namespace LeaveRegister.Models
{
    public class LeaveDays
    {
        public int CasualLeave { get; set; }
        public int SickLeave { get; set; }
        public int TotalWorkingHours { get; set; }
        public string StatusText { get; set; }
    }

    public class CompanyLeave
    {
        [DataMember]
        public string Date { get; set; }
        [DataMember]
        public string Day { get; set; }
        [DataMember]
        public string Description { get; set; }
        [DataMember]
        public int CompanyLeaveId { get; set; }
        [DataMember]
        public string StatusText { get; set; }
    }

    public class AddedEmployee
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string EmployeeId { get; set; }
        [DataMember]
        public string EmailId { get; set; }
        [DataMember]
        public string FirstName { get; set; }
        [DataMember]
        public string LastName { get; set; }
        [DataMember]
        public string Designation { get; set; }
        [DataMember]
        public string Company { get; set; }
        [DataMember]
        public string StatusText { get; set; }
        [DataMember]
        public string DateOfJoining { get; set; }
    }

    public class Email
    {
        public int Id { get; set; }
        public string EmailId { get; set; }
        public string Password { get; set; }
        public string  SettingFirst { get; set; }
        public string SettingSecond { get; set; }
        public string SettingThird { get; set; }
        public string SettingFourth { get; set; }
        public string Status { get; set; }  
    }

    public class NewsAndUpdate
    {
        [DataMember]
        public string NewsId { get; set; }
        [DataMember]
        public string Title { get; set; }
        [DataMember]
        public string Description { get; set; }
        [DataMember]
        public string Date { get; set; }
        [DataMember]
        public string ModifiedDate { get; set; }
        [DataMember]
        public string StatusText { get; set; }

    }

    public class GetEmployeeId
    {
        public string EmployeeId { get; set; }
        public string StatusText { get; set; }
    }
}