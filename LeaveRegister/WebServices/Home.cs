using System.Configuration;
using System.Web.Services;
using LeaveRegister.IImplementation;
using LeaveRegister.Models;
using LeaveRegister.Utils;

namespace LeaveRegister.WebServices
{
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    [System.Web.Script.Services.ScriptService]
    public class Home : LeaveRegisterMembers, IHome
    {
        #region Variables
        public string ConnectionString = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
        #endregion
        [WebMethod]
        public string RecoverPassword(string EmailId)
        {
            return (DataBaseUtils.IsEmailIdExist(ConnectionString, EmailId))
                ? DataBaseUtils.RecoverPasswordText(ConnectionString, EmailId)
                : "Entered email id is not registred, please entred your registred email id.";
        }
        [WebMethod]
        public string SignUp(string EmailId, string EmployeeId, string Password, string RePassword)
        {
            var status = (DataBaseUtils.IsEmailIdExist(ConnectionString, EmailId))
                ? ((DataBaseUtils.IsEmployeeIdExist(ConnectionString, EmployeeId)
                    ? ((DataBaseUtils.IsEmployeeIdMatches(ConnectionString, EmailId, EmployeeId))
                        ? "Success"
                        : "Employee Id and Email Id is not maching")
                    : "Entered employee id is not registered."))
                : "Entered email id is not registered.";
            if (status == "Success")
            {
                status = (DataBaseUtils.IsEmployeeDoneSignUpBefore(ConnectionString, EmailId))
                    ? "Employee registration already has been done."
                    : (Password.Length > 6)
                        ? ((Password == RePassword)
                            ? (DataBaseUtils.EmployeeSignUpOrChangePassword(ConnectionString, Password, RePassword,
                                EmployeeId))[0]
                            : "Password and RePassword is not Maching")
                        : "Please enter your password minimum 6 character.";
            }
            return status;
        }
        [WebMethod]
        public HomePage SignIn(string EmailId, string Password)
        {
            var homePage = new HomePage();
            var isEmailIdExist = DataBaseUtils.IsEmailIdExist(ConnectionString, EmailId);
            var isPasswordMatches = DataBaseUtils.IsPasswordMatches(ConnectionString, Password, EmailId);
            homePage.StatusText = (isEmailIdExist)
                ? ((isPasswordMatches
                    ? "Success"
                    : "Entred password is not matching in the respect of Email id"))
                : "Email id is not registered yet. Please ask your admin to register your email id.";
            homePage.Status = (isEmailIdExist && isPasswordMatches);
            if (!homePage.Status) return homePage;
            var employeeId = DataBaseUtils.GetEmployeeId(ConnectionString,EmailId);
            if (string.IsNullOrEmpty(employeeId)) return homePage;
            homePage.Updates = DataBaseUtils.GetEmployeeUpdates(ConnectionString, employeeId);
            homePage.News = DataBaseUtils.GetNews(ConnectionString);
            homePage.AttendanceDetails = DataBaseUtils.GetCurrentAttendanceDetails(ConnectionString, employeeId);
            homePage.EmployeeInfo = DataBaseUtils.EmployeeInfo(ConnectionString, employeeId);
            if (homePage.EmployeeInfo.IsAdmin)
            {
                homePage.AdminUpdates = DataBaseUtils.GetAdminEmployeeUpdates(ConnectionString);
            }
            homePage.SessionKey = LeaveRegisterUtils.EncryptPassword(EmailId);
            homePage.SessionValue = LeaveRegisterUtils.EncryptPassword(Password);
            return homePage;
        }
        [WebMethod]
        public Employee ManageProfile(string SessionKey, string SessionValue)
        {
            var employee = new Employee();
            if ((string.IsNullOrEmpty(SessionKey) && (string.IsNullOrEmpty(SessionValue)))) return employee;
            var sessionId = LeaveRegisterUtils.DecryptPassword(SessionKey);
            var sessionValue = LeaveRegisterUtils.DecryptPassword(SessionValue);
            var employeeId = DataBaseUtils.GetEmployeeId(ConnectionString, sessionId);
            if (!DataBaseUtils.IsEmployeeLoggedIn(ConnectionString, sessionId, sessionValue))
            {
                employee.StatusText = "Session out";
                return employee;
            }
            employee= DataBaseUtils.GetProfileinfo(ConnectionString,employeeId);
            employee.StatusText = "success";
            return employee;
        }
        [WebMethod]
        public string SaveProfile(string DateOfBirth, string SessionId, string SessionValue)
        {
            if (string.IsNullOrEmpty(SessionId) && string.IsNullOrEmpty(SessionValue))
                return "Session out! please log in again";
            var sessionId = LeaveRegisterUtils.DecryptPassword(SessionId);
            var sessionValue = LeaveRegisterUtils.DecryptPassword(SessionValue);
            if (!DataBaseUtils.IsEmployeeLoggedIn(ConnectionString, sessionId, sessionValue)) return "Session out! please log in again";
            var employeeId = DataBaseUtils.GetEmployeeId(ConnectionString, sessionId);
            return DataBaseUtils.ManageProfileInformations(ConnectionString, DateOfBirth, employeeId);
            
        }
        [WebMethod]
        public string UploadProfilePic(string PictureLocation, string SessionId, string SessionValue)
        {
            return "";
        }
        [WebMethod]
        public string[] ChangePassword(string CurrentPassword, string NewPassword, string ReNewPassword, string SessionId, string SessionValue)
        {
            var charLimitValidationMessage = new[] { "Password length must be minimum 7 character" };
            var currentPasswordNotMatched = new[] {"Current password is not matching"};
            var newAndRenewPasswordNotMatched = new[] { "Entred new password and renew pasword is not matching." };
          var sessionOut= new []{"Session expired, please login again"};

            var sessionId = LeaveRegisterUtils.DecryptPassword(SessionId);
            var sessionValue = LeaveRegisterUtils.DecryptPassword(SessionValue);
            var employeeId = DataBaseUtils.GetEmployeeId(ConnectionString, sessionId);
            return !DataBaseUtils.IsEmployeeLoggedIn(ConnectionString, sessionId, sessionValue)
                ? sessionOut
                : (!DataBaseUtils.IsPasswordMatches(ConnectionString, CurrentPassword, sessionId)
                    ? currentPasswordNotMatched
                    : ((!(NewPassword.Length > 6))
                        ? charLimitValidationMessage
                        : ((NewPassword != ReNewPassword)
                            ? newAndRenewPasswordNotMatched
                            : DataBaseUtils.EmployeeSignUpOrChangePassword(ConnectionString, NewPassword, ReNewPassword,
                                employeeId))));
        }
        [WebMethod]
        public string Logout()
        {
            return "Successfully logged out.";
        }
        [WebMethod]
        public HomePage RefreshPage(string SessionId, string SessionValue)
        {
            return SignIn(SessionId, SessionValue);
        }
        [WebMethod]
        public HomeTab HomeTab(string SessionKey, string SessionValue)
        {
            var homTab = new HomeTab();
            if ((string.IsNullOrEmpty(SessionKey)) && (string.IsNullOrEmpty(SessionValue))) return homTab;
            var sessionId = LeaveRegisterUtils.DecryptPassword(SessionKey);
            var sessionValue = LeaveRegisterUtils.DecryptPassword(SessionValue);
            if (!DataBaseUtils.IsEmployeeLoggedIn(ConnectionString, sessionId, sessionValue))
            {
                homTab.Status = false;
                homTab.StatusText = "Session Out! <br/> Please again login";
                return homTab;
            }
            homTab.Status = true;
            if (!homTab.Status) return homTab;
            var employeeId = DataBaseUtils.GetEmployeeId(ConnectionString, sessionId);
            if (string.IsNullOrEmpty(employeeId)) return homTab;
            homTab.Updates = DataBaseUtils.GetEmployeeUpdates(ConnectionString, employeeId);
            homTab.News = DataBaseUtils.GetNews(ConnectionString);
            homTab.AttendanceDetails = DataBaseUtils.GetCurrentAttendanceDetails(ConnectionString, employeeId);
            homTab.EmployeeInfo = DataBaseUtils.EmployeeInfo(ConnectionString, employeeId);
            if (homTab.EmployeeInfo.IsAdmin)
            {
                homTab.AdminUpdates = DataBaseUtils.GetAdminEmployeeUpdates(ConnectionString);
            }
            return homTab;
        }
    }

}