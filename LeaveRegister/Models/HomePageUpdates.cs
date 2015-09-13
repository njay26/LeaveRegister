using System.Web.UI.WebControls;

namespace LeaveRegister.Models
{
    public class HomePage
    {
        public EmployeeInfo EmployeeInfo { get; set; }
        public AdminUpdates[] AdminUpdates { get; set; }
        public Updates[] Updates { get; set; }
        public Attendance AttendanceDetails { get; set; }
        public News[] News { get; set; }
        public string SessionKey { get; set; }
        public string SessionValue { get; set; }
        public string StatusText { get; set; }
        public bool Status { get; set; }
    }
    public class EmployeeInfo
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ProfileImage { get; set; }
        public bool IsAdmin { get; set; }
    }
    public class Updates
    {
        public string LeaveAppliedBy { get; set; }
        public string LeaveType { get; set; }
        public string LeaveFrom { get; set; }
        public string LeaveTill { get; set; }
        public string LeaveStatus { get; set; }
        public string LeaveApproved { get; set; }
        public string Status { get; set; }
        public int TotalDays { get; set; }
    }
    /// <summary>
    /// Here Admin employee only access those item which are all leave status is waiting for reponse
    /// </summary>
    public class AdminUpdates
    {
        public string Name { get; set; }
        public string EmployeeId { get; set; }
        public string LeaveType { get; set; }
        public string LeaveFrom { get; set; }
        public string LeaveTill { get; set; }
        public string Id { get; set; }
    }
    public class Attendance
    {
        public string InTime { get; set; }
        public string LunchTime { get; set; }
        public string OuTime { get; set; }
        public string TotalWorked { get; set; }
        public string AttendanceStatus { get; set; }
    }
    public class EmmployeeAttendanceSheets
    {
        public string Date { get; set; }
        public string Day { get; set; }
        public string AttendanceStatus { get; set; }
    }
    public class News
    {
        public int NewsId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Date { get; set; }
        public string Status { get; set; }
    }
    public class HomeTab
    {
        public EmployeeInfo EmployeeInfo { get; set; }
        public AdminUpdates[] AdminUpdates { get; set; }
        public Updates[] Updates { get; set; }
        public Attendance AttendanceDetails { get; set; }
        public News[] News { get; set; }
        public string StatusText { get; set; }
        public bool Status { get; set; }
    }
}