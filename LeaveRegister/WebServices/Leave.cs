using System;
using System.Configuration;
using System.Web.Services;
using LeaveRegister.IImplementation;
using LeaveRegister.Utils;

namespace LeaveRegister.WebServices
{
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    [System.Web.Script.Services.ScriptService]
    public class Leave:ILeave
    {
        public string ConnectionString = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
        [WebMethod]
        public string[] LeaveTab(string SessionKey, string SessionValue)
        {
          var leaveType= new string[1];
          leaveType[0] = "Session Out! <br/> Please again login";
            if ((string.IsNullOrEmpty(SessionKey)) && (string.IsNullOrEmpty(SessionValue))) return leaveType;
            var sessionId = LeaveRegisterUtils.DecryptPassword(SessionKey);
            var sessionValue = LeaveRegisterUtils.DecryptPassword(SessionValue);
            if (!DataBaseUtils.IsEmployeeLoggedIn(ConnectionString, sessionId, sessionValue))
            {
                return leaveType;
            }
            return DataBaseUtils.GetLeaveTypes(ConnectionString);
        }
        [WebMethod]
        public string ApplyLeave(string SessionKey, string SessionValue, string FromDate, string ToDate, string TypeOfLeave)
        {
           if(string.IsNullOrEmpty(FromDate)) return "Please select from date";
            if (!(string.IsNullOrEmpty(ToDate)))
            {
                var fromDate = Convert.ToDateTime(FromDate);
                var toDate = Convert.ToDateTime(ToDate);
                var difference = DateTime.Compare(toDate, fromDate);
                if (difference == 0 || difference < 0)
                {
                    return "Please select later date for To Date";   
                }
            }
            if ((string.IsNullOrEmpty(SessionKey)) && (string.IsNullOrEmpty(SessionValue))) return "Session Out! <br/> Please again login";
            var sessionId = LeaveRegisterUtils.DecryptPassword(SessionKey);
            var sessionValue = LeaveRegisterUtils.DecryptPassword(SessionValue);
            if (!DataBaseUtils.IsEmployeeLoggedIn(ConnectionString, sessionId, sessionValue))
            {
                return "Session Out! <br/> Please again login";
            }
            var employeeId = DataBaseUtils.GetEmployeeId(ConnectionString, sessionId);
            return DataBaseUtils.ApplyForLeave(ConnectionString, employeeId, FromDate, ToDate, TypeOfLeave);
        }
    }
}