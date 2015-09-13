namespace LeaveRegister.Models
{
/*............................................... This file will contain all constant member's name...........................................................*/
    public class LeaveRegisterMembers : System.Web.UI.Page
    {
        #region Store Procedures
        public const string StoreProcedureActionOnMoreThanOneDayLeaveByAdmin = "SP_ActionOnMoreThanOneDayLeaveByAdmin";
        public const string StoreProcedureActionOnOneDayLeaveByAdmin = "SP_ActionOnOneDayLeaveByAdmin";
        public const string StoreProcedureAddLeave = "SP_AddLeave";
        public const string StoreProcedureAddNews = "SP_AddNews";
        public const string StoreProcedureApplyLeave = "SP_ApplyLeave";
        public const string StoreProcedureChangeAttendanceStatus = "SP_ChangeAttendanceStatus";
        public const string StoreProcedureChangePassword = "SP_ChangePassword";
        public const string StoreProcedureEmailSetup = "SP_EmailSetup";
        public const string StoreProcedureDeleteAddedLeave = "SP_DeleteAddedLeave";
        public const string StoreProcedureEditEmailSetup = "SP_EditEmailSetup";
        public const string StoreProcedureEditProfileImage = "SP_EditProfileImage";
        public const string StoreProcedureEmployeeList = "SP_EmployeeList";
        public const string StoreProcedureEmployeeRegistrationByAdmin = "SP_EmployeeRegistrationByAdmin";
        public const string StoreProcedureEmployeeRegistrationBySuperAdmin = "SP_EmployeeRegistrationBySuperAdmin";
        public const string StoreProcedureFillInTimeAttendance = "SP_FillInTimeAttendance";
        public const string StoreProcedureFillLunchTimeAttendance = "SP_FillLunchTimeAttendance";
        public const string StoreProcedureFillOutTimeAttendance = "SP_FillOutTimeAttendance";
        public const string StoreProcedureGetAtteendaceStatus = "SP_GetAtteendaceStatus";
        public const string StoreProcedureGetEmialSetupInfo = "SP_GetEmialSetupInfo";
        public const string StoreProcedureGetLeaveAcceptRejectDetails = "SP_GetLeaveAcceptRejectDetails";
        public const string StoreProcedureGetLeaveList = "SP_GetLeaveList";
        public const string StoreProcedureGetLeavesAndWorkingHours = "SP_GetLeavesAndWorkingHours";
        public const string StoreProcedureGetNews = "SP_GetNews";
        public const string StoreProcedureGetOwnAttendanceSheet = "SP_GetOwnAttendanceSheet";
        public const string StoreProcedureGetPassword = "SP_GetPassword";
        public const string StoreProcedureGetProfileInfo = "SP_GetProfileInfo";
        public const string StoreProcedureGetTotalTakenLeaveStatus = "SP_GetTotalTakenLeaveStatus";
        public const string StoreProcedureGetUpdateAdmin = "SP_GetUpdateAdmin";
        public const string StoreProcedureGetUpdates = "SP_GetUpdates";
        public const string StoreProcedureGetWeekend = "SP_GetWeekend";
        public const string StoreProcedureProfileEdit = "SP_ProfileEdit";
        public const string StoreProcedureSaveProfileImage = "SP_SaveProfileImage";
        public const string StoreProcedureSetLeavesAndWorkingHours = "SP_SetLeavesAndWorkingHours";
        public const string StoreProcedureSetWeekend = "SP_SetWeekend";
        public const string StoreProcedureUpdateNews = "SP_UpdateNews";
        public const string StoreProcedureIsEmployeeIdIdExist = "SP_IsEmployeeIdIdExist";
        public const string StoreProcedureIsEmailIdExist = "SP_IsEmailIdExist";
        public const string StoreProcedureGetEmployeeId = "SP_GetEmployeeID";
        public const string StoreProcedureGetCurrentAtteendaceStatus = "SP_GetCurrentAtteendaceStatus";
        public const string StoreProcedureGetEmployeeHomeInfo = "SP_GetEmployeeHomeInfo";
        public const string StoreProcedureIsAttendanceRowExist = "SP_IsAttendanceRowExist";
        public const string StoreProcedureAddAttendanceByAdmin = "SP_AddAttendanceByAdmin";
        public const string StoreProcedureUpdateEmployeeInfoByAdmin = "SP_UpdateEmployeeInfoByAdmin";
        public const string StoreProcedureUpdateCompanyLeave = "SP_UpdateCompanyLeave";
        public const string StoreProcedureDeletedAddedNews = "SP_DeletedAddedNews";
        public const string StoreProcedureFillApproveLeaveAttendance = "SP_FillApproveLeaveAttendance";
        public const string StoreProcedureGetUpdateItemById = "SP_GetUpdateItemByID"; 
        #endregion
        #region SQL Functions
        public const string FunctionInTimeAttendancestatus = "FN_InTimeAttendancestatus";
        public const string FunctionLunchTimeAttendancestatus = "FN_LunchTimeAttendancestatus";
        public const string FunctionOutTimeAttendancestatus = "FN_OutTimeAttendancestatus"; 
        #endregion

    }
    
}