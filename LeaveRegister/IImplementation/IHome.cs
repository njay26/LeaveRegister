using LeaveRegister.Models;

namespace LeaveRegister.IImplementation
{
    public interface IHome
    {
        string RecoverPassword(string EmailId);
        string SignUp(string EmailId, string EmployeeId,string Password,string RePassword);
        HomePage SignIn(string EmailId, string Password);
        Employee ManageProfile(string SessionKey, string SessionValue);
        string SaveProfile(string DateOfBirth, string SessionId,string SessionValue);
        string UploadProfilePic(string PictureLocation,string SessionId,string SessionValue);
        string[] ChangePassword(string CurrentPassword, string NewPassword, string ReNewPassword, string SessionId, string SessionValue);
        string Logout();
        HomePage RefreshPage(string SessionKey, string SessionValue);
        HomeTab HomeTab(string SessionKey, string SessionValue);
    }
}