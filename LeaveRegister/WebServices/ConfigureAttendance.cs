using System;
using System.IO;
using System.Web;
using LeaveRegister.IImplementation;
using LeaveRegister.Models;
using System.Configuration;
using System.Web.Services;
using LeaveRegister.Utils;

namespace LeaveRegister.WebServices
{
    [WebService(Namespace = "http://tempuri.org")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    [System.Web.Script.Services.ScriptService]
    public class Admin : IAdmin
    {
        public string ConnectionString = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;

        [WebMethod]
        public LeaveDays SetLeaveDays(int CasualLeave, int SickLeave, int WorkingHours, string SessionId,
            string SessionValue)
        {
            var leaveDays = new LeaveDays {StatusText = "Session out, please login again"};
            CasualLeave = (CasualLeave == null) ? 0 : CasualLeave;
            SickLeave = (SickLeave == null) ? 0 : SickLeave;
            WorkingHours = (WorkingHours == null) ? 0 : WorkingHours;
            SessionId = LeaveRegisterUtils.DecryptPassword(SessionId);
            SessionValue = LeaveRegisterUtils.DecryptPassword(SessionValue);
            if (!DataBaseUtils.IsEmployeeLoggedIn(ConnectionString, SessionId, SessionValue))
            {
                return leaveDays;
            }
            var employeeid = DataBaseUtils.GetEmployeeId(ConnectionString, SessionId);
            if (!DataBaseUtils.IsAdminEmployee(ConnectionString, employeeid))
            {
                leaveDays.StatusText = "Leave configuration can be set only <b>Admin</b> employee";
                return leaveDays;
            }
            if (
                DataBaseUtils.SetLeaveConfigurationInformations(ConnectionString, SickLeave, CasualLeave, WorkingHours) !=
                "Success")
            {
                leaveDays.StatusText = "Some problem occure during setting the configuration, please try lator";
                return leaveDays;
            }
            return DataBaseUtils.GetLeaveConfigurationDetailes(ConnectionString);
        }

        [WebMethod]
        public LeaveDays GetLeaveDays(string SessionKey, string SessionValue)
        {
            var leaveDays = new LeaveDays {StatusText = "Session out, please login again"};
            SessionKey = LeaveRegisterUtils.DecryptPassword(SessionKey);
            SessionValue = LeaveRegisterUtils.DecryptPassword(SessionValue);
            if (!DataBaseUtils.IsEmployeeLoggedIn(ConnectionString, SessionKey, SessionValue))
            {
                return leaveDays;
            }
            var employeeId = DataBaseUtils.GetEmployeeId(ConnectionString, SessionKey);
            if (!DataBaseUtils.IsAdminEmployee(ConnectionString, employeeId))
            {
                leaveDays.StatusText = "Leave configuration data can be see only <b>Admin</b> employee";
                return leaveDays;
            }
            return DataBaseUtils.GetLeaveConfigurationDetailes(ConnectionString);
        }

        [WebMethod]
        public CompanyLeave[] GetCompanyLeave()
        {
            return DataBaseUtils.GetCompanyLeaveList(ConnectionString);
        }

        [WebMethod]
        public string AddCompanyLeave(string SessionId, string SessionValue, string Date, string Day,
            string LeaveDescription)
        {
            const string statusText = "Session Out please login again";
            if ((string.IsNullOrEmpty(SessionId) || (string.IsNullOrEmpty(SessionValue)))) return statusText;
            if (string.IsNullOrEmpty(Date) || string.IsNullOrEmpty(Day) || string.IsNullOrEmpty(LeaveDescription))
            {
                return "Date, Day and Description field is required";
            }
            var sessionId = LeaveRegisterUtils.DecryptPassword(SessionId);
            var sessionValue = LeaveRegisterUtils.DecryptPassword(SessionValue);
            var employeeId = DataBaseUtils.GetEmployeeId(ConnectionString, sessionId);
            if (!DataBaseUtils.IsEmployeeLoggedIn(ConnectionString, sessionId, sessionValue))
            {
                return statusText;
            }
            if (!DataBaseUtils.IsAdminEmployee(ConnectionString, employeeId))
            {
                return "Employee can be add only by Admin Employee";
            }
            return DataBaseUtils.AddCompanyLeave(ConnectionString, Date, Day, LeaveDescription, employeeId);
        }

        [WebMethod]
        public string DeleteAddedLeave(string SessionId, string SessionValue, int LeaveId)
        {
            const string statusText = "Session out! please login again";
            if ((string.IsNullOrEmpty(SessionId) || (string.IsNullOrEmpty(SessionValue)))) return statusText;
            var sessionId = LeaveRegisterUtils.DecryptPassword(SessionId);
            var sessionValue = LeaveRegisterUtils.DecryptPassword(SessionValue);
            var employeeId = DataBaseUtils.GetEmployeeId(ConnectionString, sessionId);
            if (!DataBaseUtils.IsEmployeeLoggedIn(ConnectionString, sessionId, sessionValue))
            {
                return statusText;
            }
            if (!DataBaseUtils.IsAdminEmployee(ConnectionString, employeeId))
            {
                return "Employee can be add only by Admin Employee";
            }
            return DataBaseUtils.DeleteAddedCompanyLeave(ConnectionString, LeaveId);
        }

        [WebMethod]
        public string UpdateCompanyLeave(string SessionId, string SessionValue, string Date, string Day,
            string LeaveDescription, int LeaveId)
        {
            const string statusText = "Session out! please login again";
            if ((string.IsNullOrEmpty(SessionId) || (string.IsNullOrEmpty(SessionValue)))) return statusText;
            var sessionId = LeaveRegisterUtils.DecryptPassword(SessionId);
            var sessionValue = LeaveRegisterUtils.DecryptPassword(SessionValue);
            var employeeId = DataBaseUtils.GetEmployeeId(ConnectionString, sessionId);
            if (!DataBaseUtils.IsEmployeeLoggedIn(ConnectionString, sessionId, sessionValue))
            {
                return statusText;
            }
            if (!DataBaseUtils.IsAdminEmployee(ConnectionString, employeeId))
            {
                return "Employee can be add only by Admin Employee";
            }
            if (string.IsNullOrEmpty(Date))
            {
                return "Date can not be empty";
            }
            if (string.IsNullOrEmpty(Day))
            {
                return "Day can not be empty";
            }
            if (string.IsNullOrEmpty(LeaveDescription))
            {
                return "LeaveDescription can not be empty";
            }
            return DataBaseUtils.UpdateAddedCompanyLeave(ConnectionString, Date, Day, LeaveDescription, employeeId,
                LeaveId);
        }

        [WebMethod]
        public AddedEmployee[] GetAddedEmployee()
        {
            return DataBaseUtils.GetEmployeeList(ConnectionString);
        }

        [WebMethod]
        public string AddEmployee(string SessionId, string SessionValue, string FirstName, string EmployeeId,
            string EmailId, string LastName, string Designation, string Doj, string Company)
        {
            const string statusText = "Session Out! Please try again";
            if ((string.IsNullOrEmpty(SessionId) || (string.IsNullOrEmpty(SessionValue)))) return statusText;
            if (string.IsNullOrEmpty(FirstName))
            {
                return "First Name is required";
            }
            if (string.IsNullOrEmpty(LastName))
            {
                return "Last Name is required";
            }
            if (string.IsNullOrEmpty(Designation))
            {
                return "Designation is required";
            }
            if (string.IsNullOrEmpty(Doj))
            {
                return "Date of Joining is required";
            }
            if (string.IsNullOrEmpty(Company))
            {
                return "Company is required";
            }
            var sessionId = LeaveRegisterUtils.DecryptPassword(SessionId);
            var sessionValue = LeaveRegisterUtils.DecryptPassword(SessionValue);
            var employeeId = DataBaseUtils.GetEmployeeId(ConnectionString, sessionId);
            if (!DataBaseUtils.IsEmployeeLoggedIn(ConnectionString, sessionId, sessionValue))
            {
                return statusText;
            }
            if (!DataBaseUtils.IsAdminEmployee(ConnectionString, employeeId))
            {
                return "Employee can be add only by Admin Employee";
            }
            if (string.IsNullOrEmpty(EmployeeId) || string.IsNullOrEmpty(EmailId))
            {
                return "Employee Id  and Email Id is required";
            }
            if (!LeaveRegisterUtils.VaidateEmail(EmailId))
            {
                return "Please enter valid email Id. Example; aaaa@gmail.com";
            }
            if (DataBaseUtils.IsEmailIdExist(ConnectionString, EmailId))
            {
                return EmailId + " is already has been registered";
            }
            if (DataBaseUtils.IsEmployeeIdExist(ConnectionString, EmployeeId))
            {
                return employeeId + " is already has been registered";
            }
            return
                DataBaseUtils.AddEmployeeByAdmin(ConnectionString, FirstName, EmployeeId, EmailId, LastName,
                    Designation, Doj, Company);
        }

        [WebMethod]
        public string UpdateAddedEmployeeEditEmployee(int Id, string SessionId, string SessionValue, string FirstName,
            string LastName, string Designation, string Doj, string Company)
        {
            const string statusText = "Session Out! Please try again";
            if ((string.IsNullOrEmpty(SessionId) || (string.IsNullOrEmpty(SessionValue)))) return statusText;
            if (Id == null || Id == -1)
            {
                return "Some thing went wrong please reload the page and try again";
            }
            if (string.IsNullOrEmpty(FirstName))
            {
                return "First Name is required";
            }
            if (string.IsNullOrEmpty(LastName))
            {
                return "Last Name is required";
            }
            if (string.IsNullOrEmpty(Designation))
            {
                return "Designation is required";
            }
            if (string.IsNullOrEmpty(Doj))
            {
                return "Date of Joining is required";
            }
            if (string.IsNullOrEmpty(Company))
            {
                return "Company is required";
            }
            var sessionId = LeaveRegisterUtils.DecryptPassword(SessionId);
            var sessionValue = LeaveRegisterUtils.DecryptPassword(SessionValue);
            var employeeId = DataBaseUtils.GetEmployeeId(ConnectionString, sessionId);
            if (!DataBaseUtils.IsEmployeeLoggedIn(ConnectionString, sessionId, sessionValue))
            {
                return statusText;
            }
            if (!DataBaseUtils.IsAdminEmployee(ConnectionString, employeeId))
            {
                return "Employee can be add only by Admin Employee";
            }
            return DataBaseUtils.UpdateEmployeeInfoByAdmin(ConnectionString, Id, FirstName, LastName, Doj, Designation,
                Company);
        }

        [WebMethod]
        public NewsAndUpdate[] GetNewsAndUpdate()
        {
            return DataBaseUtils.GetNewsAndUpdate(ConnectionString);
        }

        [WebMethod]
        public string AddNewsAndUpdate(string SessionId, string SessionValue, string Title, string Description)
        {
            const string statusText = "Session Out! Please try again";
            if ((string.IsNullOrEmpty(SessionId) || (string.IsNullOrEmpty(SessionValue)))) return statusText;
            if (string.IsNullOrEmpty(Title))
            {
                return "News Title is required";
            }
            if (string.IsNullOrEmpty(Description))
            {
                return "News Description is required";
            }
            var sessionId = LeaveRegisterUtils.DecryptPassword(SessionId);
            var sessionValue = LeaveRegisterUtils.DecryptPassword(SessionValue);
            var employeeId = DataBaseUtils.GetEmployeeId(ConnectionString, sessionId);
            if (!DataBaseUtils.IsEmployeeLoggedIn(ConnectionString, sessionId, sessionValue))
            {
                return statusText;
            }
            if (!DataBaseUtils.IsAdminEmployee(ConnectionString, employeeId))
            {
                return "News can be add only by Admin Employee";
            }
            return DataBaseUtils.AddNews(ConnectionString, employeeId, Title, Description);
        }

        [WebMethod]
        public string UpdateNewsAndUpdate(string SessionId, string SessionValue, int NewsId, string Title,
            string Description)
        {
            const string statusText = "Session Out! Please try again";
            if ((string.IsNullOrEmpty(SessionId) || (string.IsNullOrEmpty(SessionValue)))) return statusText;
            if (string.IsNullOrEmpty(Title))
            {
                return "News Title is required";
            }
            if (string.IsNullOrEmpty(Description))
            {
                return "News Description is required";
            }
            var sessionId = LeaveRegisterUtils.DecryptPassword(SessionId);
            var sessionValue = LeaveRegisterUtils.DecryptPassword(SessionValue);
            var employeeId = DataBaseUtils.GetEmployeeId(ConnectionString, sessionId);
            if (!DataBaseUtils.IsEmployeeLoggedIn(ConnectionString, sessionId, sessionValue))
            {
                return statusText;
            }
            if (!DataBaseUtils.IsAdminEmployee(ConnectionString, employeeId))
            {
                return "News can be add only by Admin Employee";
            }
            return DataBaseUtils.UpdateNews(ConnectionString, employeeId, NewsId, Title, Description);
        }

        [WebMethod]
        public string DeleteNewsAndUpdate(string SessionId, string SessionValue, int NewsId)
        {
            const string statusText = "Session Out! Please try again";
            if ((string.IsNullOrEmpty(SessionId) || (string.IsNullOrEmpty(SessionValue)))) return statusText;
            var sessionId = LeaveRegisterUtils.DecryptPassword(SessionId);
            var sessionValue = LeaveRegisterUtils.DecryptPassword(SessionValue);
            var employeeId = DataBaseUtils.GetEmployeeId(ConnectionString, sessionId);
            if (!DataBaseUtils.IsEmployeeLoggedIn(ConnectionString, sessionId, sessionValue))
            {
                return statusText;
            }
            if (!DataBaseUtils.IsAdminEmployee(ConnectionString, employeeId))
            {
                return "News can be add only by Admin Employee";
            }
            return DataBaseUtils.DeleteAddedNews(ConnectionString, NewsId);
        }

        [WebMethod]
        public GetEmployeeId[] EditEmployeeAttendance(string SessionKey, string SessionValue)
        {
            var employeeIds = new GetEmployeeId[1];
            var empId = new GetEmployeeId {StatusText = "Session out! please login again"};
            employeeIds[0] = empId;
            SessionKey = LeaveRegisterUtils.DecryptPassword(SessionKey);
            SessionValue = LeaveRegisterUtils.DecryptPassword(SessionValue);
            if (!DataBaseUtils.IsEmployeeLoggedIn(ConnectionString, SessionKey, SessionValue))
            {
                return employeeIds;
            }
            var employeeId = DataBaseUtils.GetEmployeeId(ConnectionString, SessionKey);
            if (!DataBaseUtils.IsAdminEmployee(ConnectionString, employeeId))
            {
                empId.StatusText = "Employee Id can be display only <b> admin user</b>";
                employeeIds[0] = empId;
                return employeeIds;
            }
            return DataBaseUtils.GetAllEmployeeId(ConnectionString);
        }

        [WebMethod]
        public GetAttendanceStatus GetAttendanceStatus(string SessionKey, string SessionValue, string EmployeeId,
            string Date)
        {
            var attendanceStatus = new GetAttendanceStatus {Status = false};
            if ((string.IsNullOrEmpty(SessionKey)) && (string.IsNullOrEmpty(SessionValue))) return attendanceStatus;
            var sessionId = LeaveRegisterUtils.DecryptPassword(SessionKey);
            var sessionValue = LeaveRegisterUtils.DecryptPassword(SessionValue);
            if (!DataBaseUtils.IsEmployeeLoggedIn(ConnectionString, sessionId, sessionValue))
            {
                return attendanceStatus;
            }
            var employeeId = DataBaseUtils.GetEmployeeId(ConnectionString, sessionId);
            if (!DataBaseUtils.IsAdminEmployee(ConnectionString, employeeId))
            {
                return attendanceStatus;
            }
            return DataBaseUtils.GetAttendanceStatus(ConnectionString, EmployeeId, Date);
        }

        [WebMethod]
        public string ChangeAttendanceStatus(string SessionId, string SessionValue, string AttendanceStatus,
            string EmployeeId, string Date)
        {
            if (string.IsNullOrEmpty(AttendanceStatus) || string.IsNullOrEmpty(Date) || string.IsNullOrEmpty(EmployeeId))
            {
                return "Employee id, Date and Attendance status can not be empty";
            }
            SessionId = LeaveRegisterUtils.DecryptPassword(SessionId);
            SessionValue = LeaveRegisterUtils.DecryptPassword(SessionValue);
            if (!DataBaseUtils.IsEmployeeLoggedIn(ConnectionString, SessionId, SessionValue))
            {
                return "Session out! please login again";
            }
            var employeeId = DataBaseUtils.GetEmployeeId(ConnectionString, SessionId);
            if (!DataBaseUtils.IsAdminEmployee(ConnectionString, employeeId))
            {
                return "Soory you are not admin employee.<br/>Attendance status can be change only admin";
            }
            if (!DataBaseUtils.IsAttendanceRowExist(ConnectionString, EmployeeId, Date))
            {
                if (DataBaseUtils.AddAttendanceByAdmin(ConnectionString, EmployeeId, Date, AttendanceStatus) ==
                    "Success")
                {
                    return "Successfully attendance status has been set";
                }
                else
                {
                    return "Operation got failed during seeting the attendance status";
                }
            }
            else
            {
                if (DataBaseUtils.ChangeAttendanceStatus(ConnectionString, Date, AttendanceStatus, EmployeeId) ==
                    "Success")
                {
                    return "Successfully attendance status has been set";
                }
                else
                {
                    return "Operation got failed during seeting the attendance status";
                }
            }
        }

        [WebMethod]
        public string AcceptOrRejectAppliedLeave(string SessionId, string SessionValue, int LeaveId, string LeaveStatus)
        {
            const string statusText = "Session Out! Please try again";
            if ((string.IsNullOrEmpty(SessionId) || (string.IsNullOrEmpty(SessionValue)))) return statusText;
            var sessionId = LeaveRegisterUtils.DecryptPassword(SessionId);
            var sessionValue = LeaveRegisterUtils.DecryptPassword(SessionValue);
            var employeeId = DataBaseUtils.GetEmployeeId(ConnectionString, sessionId);
            if (!DataBaseUtils.IsEmployeeLoggedIn(ConnectionString, sessionId, sessionValue))
            {
                return statusText;
            }
            if (!DataBaseUtils.IsAdminEmployee(ConnectionString, employeeId))
            {
                return "Leave can be approve/reject only by Admin Employee";
            }
            if (LeaveStatus == "Rejected")
            {
                return DataBaseUtils.AcceptRejectLeaveByAdmin(ConnectionString, LeaveId, LeaveStatus);
            }
            if (DataBaseUtils.AcceptRejectLeaveByAdmin(ConnectionString, LeaveId, LeaveStatus) == "Success")
            {
                var update = DataBaseUtils.GetUpdateById(ConnectionString, LeaveId);
                if (update.Status == "Success" && update.LeaveStatus == "Approved")
                {
                    return DataBaseUtils.FillApprovedLeaveAttendanceShaeet(ConnectionString, update.LeaveFrom,
                        update.LeaveTill,
                        update.LeaveType, update.LeaveAppliedBy, update.TotalDays);
                }
            }
            return "Failed";
        }
        /// <summary>
        /// Get all employee attendance sheet
        /// </summary>
        [WebMethod]
        public void GetEmployeeAttendanceSheet(string SessionId, string SessionValue, string Month)
        {
            if ((!string.IsNullOrEmpty(SessionId)) && (!string.IsNullOrEmpty(SessionValue)))
            {
                var sessionId = LeaveRegisterUtils.DecryptPassword(SessionId);
                var sessionValue = LeaveRegisterUtils.DecryptPassword(SessionValue);
                if (DataBaseUtils.IsEmployeeLoggedIn(ConnectionString, sessionId, sessionValue))
                {
                    try
                    {
                         MemoryStream MemoryStream= new MemoryStream();
                        TextWriter textWriter = new StreamWriter(MemoryStream);
                        textWriter.WriteLine("Something");
                        textWriter.Flush(); // added this line
                        byte[] bytesInStream = MemoryStream.ToArray(); // simpler way of converting to array
                        MemoryStream.Close();
                        HttpContext.Current.Response.Clear();
                        HttpContext.Current.Response.ContentType = "application/force-download";
                        System.Web.HttpContext.Current.Response.AddHeader("content-disposition",
                            "attachment;    filename=abc.xlsx");
                        HttpContext.Current.Response.AddHeader("Accept-Header", MemoryStream.Length.ToString());
        HttpContext.Current.Response.AddHeader("Content-Length", MemoryStream.Length.ToString());
        HttpContext.Current.Response.BinaryWrite(MemoryStream.GetBuffer());
                        HttpContext.Current.ApplicationInstance.CompleteRequest();
                    }
                    catch (Exception e)
                    {

                    }
                }
            }
        }
    }
}