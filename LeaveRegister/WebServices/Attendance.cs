using System.Web.Services;
using LeaveRegister.IImplementation;
using LeaveRegister.Models;
using LeaveRegister.Utils;
using System.Configuration;

namespace LeaveRegister.WebServices
{
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    [System.Web.Script.Services.ScriptService]
    public class Attendance : IAttendance
    {
        public string ConnectionString = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
        [WebMethod]
        public TodayCurrentAttendance AttendanceTab(string SessionKey, string SessionValue)
        {
            var attendance = new TodayCurrentAttendance();
            if ((string.IsNullOrEmpty(SessionKey)) && (string.IsNullOrEmpty(SessionValue))) return attendance;
            var sessionId = LeaveRegisterUtils.DecryptPassword(SessionKey);
            var sessionValue = LeaveRegisterUtils.DecryptPassword(SessionValue);
            if (!DataBaseUtils.IsEmployeeLoggedIn(ConnectionString, sessionId, sessionValue))
            {
                attendance.StatusText = "Session Out! <br/> Please again login";
                return attendance;
            }
            var employeeId = DataBaseUtils.GetEmployeeId(ConnectionString, sessionId);
            return DataBaseUtils.GetTodaysAttendanceDetails(ConnectionString, employeeId);
        }

        [WebMethod]
        public TodayCurrentAttendance FillInTimeAttendance(string SessionKey, string SessionValue)
        {
            var attendance = new TodayCurrentAttendance();
            if ((string.IsNullOrEmpty(SessionKey)) && (string.IsNullOrEmpty(SessionValue))) return attendance;
            var sessionId = LeaveRegisterUtils.DecryptPassword(SessionKey);
            var sessionValue = LeaveRegisterUtils.DecryptPassword(SessionValue);
            if (!DataBaseUtils.IsEmployeeLoggedIn(ConnectionString, sessionId, sessionValue))
            {
                attendance.StatusText = "Session Out! <br/> Please again login";
                return attendance;
            }
            var employeeId = DataBaseUtils.GetEmployeeId(ConnectionString, sessionId);
            var todaysAttendanceDetaile = AttendanceTab(SessionKey, SessionValue);
            if (todaysAttendanceDetaile.InTime == null)
            {
                return (DataBaseUtils.FillAttendance(ConnectionString, employeeId, "In") == "Success")
                    ? DataBaseUtils.GetTodaysAttendanceDetails(ConnectionString, employeeId)
                    : attendance;
            }
            return new TodayCurrentAttendance();
        }

        [WebMethod]
        public TodayCurrentAttendance FillLunchTimeAttendance(string SessionKey, string SessionValue)
        {
            var attendance = new TodayCurrentAttendance();
            if ((string.IsNullOrEmpty(SessionKey)) && (string.IsNullOrEmpty(SessionValue))) return attendance;
            var sessionId = LeaveRegisterUtils.DecryptPassword(SessionKey);
            var sessionValue = LeaveRegisterUtils.DecryptPassword(SessionValue);
            if (!DataBaseUtils.IsEmployeeLoggedIn(ConnectionString, sessionId, sessionValue))
            {
                attendance.StatusText = "Session Out! <br/> Please again login";
                return attendance;
            }
            var employeeId = DataBaseUtils.GetEmployeeId(ConnectionString, sessionId);
            var todaysAttendanceDetaile = AttendanceTab(SessionKey, SessionValue);
            if (todaysAttendanceDetaile.LunchTime == "")
            {
                return (DataBaseUtils.FillAttendance(ConnectionString, employeeId, "Lunch") == "Success")
                    ? DataBaseUtils.GetTodaysAttendanceDetails(ConnectionString, employeeId)
                    : attendance;
            }
            return new TodayCurrentAttendance();
        }

        [WebMethod]
        public TodayCurrentAttendance FillOutTimeAttendance(string SessionKey, string SessionValue)
        {
            var attendance = new TodayCurrentAttendance();
            if ((string.IsNullOrEmpty(SessionKey)) && (string.IsNullOrEmpty(SessionValue))) return attendance;
            var sessionId = LeaveRegisterUtils.DecryptPassword(SessionKey);
            var sessionValue = LeaveRegisterUtils.DecryptPassword(SessionValue);
            if (!DataBaseUtils.IsEmployeeLoggedIn(ConnectionString, sessionId, sessionValue))
            {
                attendance.StatusText = "Session Out! <br/> Please again login";
                return attendance;
            }
            var employeeId = DataBaseUtils.GetEmployeeId(ConnectionString, sessionId);
            var todaysAttendanceDetaile = AttendanceTab(SessionKey, SessionValue);
            if (todaysAttendanceDetaile.OutTime == "")
            {
                return (DataBaseUtils.FillAttendance(ConnectionString, employeeId, "Out") == "Success")
                    ? DataBaseUtils.GetTodaysAttendanceDetails(ConnectionString, employeeId)
                    : attendance;
            }
            return new TodayCurrentAttendance();
        }
    }
}
