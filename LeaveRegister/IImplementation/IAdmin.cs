using LeaveRegister.Models;

namespace LeaveRegister.IImplementation
{
    /*This interface is being having all the requuire mrthod definition that will neccessory to implement into           
     * derive class of this interface
     */
    public interface IAdmin
    {
        LeaveDays SetLeaveDays(int CasualLeave, int SickLeave, int WorkingHours, string SessionId, string SessionValue);
        LeaveDays GetLeaveDays(string SessionKey, string SessionValue);
        CompanyLeave[] GetCompanyLeave();
        string AddCompanyLeave(string SessionId, string SessionValue,string Date,string Day,string LeaveDescription);
        string DeleteAddedLeave(string SessionId, string SessionValue,int LeaveId);  
        string UpdateCompanyLeave(string SessionId, string SessionValue,string Date,string Day,string LeaveDescription,int LeaveId);
        AddedEmployee[] GetAddedEmployee();
        string AddEmployee(string SessionId, string SessionValue, string FirstName, string EmployeeId, string EmailId,string LastName,string Designation, string Doj,string Company);
        string UpdateAddedEmployeeEditEmployee(int Id, string SessionId, string SessionValue, string FirstName, string LastName, string Designation, string Doj, string Company);
        NewsAndUpdate[] GetNewsAndUpdate();
        string AddNewsAndUpdate(string SessionId, string SessionValue,string Title, string Description);
        string UpdateNewsAndUpdate(string SessionId, string SessionValue, int NewsId, string Title, string Description);
        string DeleteNewsAndUpdate(string SessionId, string SessionValue, int NewsId);
        GetEmployeeId[] EditEmployeeAttendance(string SessionKey, string SessionValue);
        GetAttendanceStatus GetAttendanceStatus(string SessionKey, string SessionValue, string EmployeeId, string Date);
        string ChangeAttendanceStatus(string SessionId, string SessionValue,string AttendanceStatus,string EmployeeId, string Date);
        string AcceptOrRejectAppliedLeave(string SessionId, string SessionValue, int LeaveId, string LeaveStatus);
    }
}
