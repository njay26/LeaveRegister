using LeaveRegister.Models;

namespace LeaveRegister.IImplementation
{
    public interface IDetaies
    {
        string[] GetLeaveTypes(string SessionId, string SessionValue);
        GetAttendanceStatus CheckAttendanceDetailes(string SessionId, string SessionValue, string Date);
        int GetLeaveTypeDetailes(string SessionId, string SessionValue, string LeaveType);
       bool GetEmployeeAttendanceSheet(string SessionId, string SessionValue, string Month);  
    }
}