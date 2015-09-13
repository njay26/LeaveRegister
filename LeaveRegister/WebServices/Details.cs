using System.Configuration;
using System.Web.Services;
using LeaveRegister.IImplementation;
using LeaveRegister.Models;
using LeaveRegister.Utils;
using System.Collections.Generic;
using System.IO;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;

namespace LeaveRegister.WebServices
{
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    [System.Web.Script.Services.ScriptService]
    public class Details : IDetaies
    {
        public string ConnectionString = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
        [WebMethod]
        public string[] GetLeaveTypes(string SessionId, string SessionValue)
        {
            var leavType = new Leave();
            return leavType.LeaveTab(SessionId, SessionValue);
        }
        [WebMethod]
        public GetAttendanceStatus CheckAttendanceDetailes(string SessionId, string SessionValue, string Date)
        {
            var attendanceStatus = new GetAttendanceStatus { Status = false };
            if ((string.IsNullOrEmpty(SessionId)) && (string.IsNullOrEmpty(SessionValue))) return attendanceStatus;
            var sessionId = LeaveRegisterUtils.DecryptPassword(SessionId);
            var sessionValue = LeaveRegisterUtils.DecryptPassword(SessionValue);
            if (!DataBaseUtils.IsEmployeeLoggedIn(ConnectionString, sessionId, sessionValue))
            {
                return attendanceStatus;
            }
            var employeeId = DataBaseUtils.GetEmployeeId(ConnectionString, sessionId);
            return DataBaseUtils.GetAttendanceStatus(ConnectionString, employeeId, Date);
        }
        [WebMethod]
        public int GetLeaveTypeDetailes(string SessionId, string SessionValue, string LeaveType)
        {
            const int leaveTypeDetails = 0;
            if ((string.IsNullOrEmpty(SessionId)) && (string.IsNullOrEmpty(SessionValue))) return leaveTypeDetails;
            var sessionId = LeaveRegisterUtils.DecryptPassword(SessionId);
            var sessionValue = LeaveRegisterUtils.DecryptPassword(SessionValue);
            if (!DataBaseUtils.IsEmployeeLoggedIn(ConnectionString, sessionId, sessionValue))
            {
                return leaveTypeDetails;
            }
            var employeeId = DataBaseUtils.GetEmployeeId(ConnectionString, sessionId);
            return DataBaseUtils.GetLeaveDetails(ConnectionString, employeeId, LeaveType);
        }
        [WebMethod]
        public bool GetEmployeeAttendanceSheet(string SessionId, string SessionValue, string Month)
        {
            if ((string.IsNullOrEmpty(SessionId)) && (string.IsNullOrEmpty(SessionValue))) return false;
            var sessionId = LeaveRegisterUtils.DecryptPassword(SessionId);
            var sessionValue = LeaveRegisterUtils.DecryptPassword(SessionValue);
            if (!DataBaseUtils.IsEmployeeLoggedIn(ConnectionString, sessionId, sessionValue))
            {
                return false;
            }
            var employeeId = DataBaseUtils.GetEmployeeId(ConnectionString, sessionId);
            var attendanceSheet = DataBaseUtils.GetOwnAttendanceSheet(ConnectionString, employeeId, Month);
            return true;
        }
    }

}