using LeaveRegister.Models;

namespace LeaveRegister.IImplementation
{
    public interface IAttendance
    {
        TodayCurrentAttendance AttendanceTab(string SessionKey, string SessionValue);
        TodayCurrentAttendance FillInTimeAttendance(string SessionKey, string SessionValue);
        TodayCurrentAttendance FillLunchTimeAttendance(string SessionKey, string SessionValue);
        TodayCurrentAttendance FillOutTimeAttendance(string SessionKey, string SessionValue); 
    }
}