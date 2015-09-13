using System;
using System.Data;
using System.Data.SqlClient;
using LeaveRegister.Models;


namespace LeaveRegister.Utils
{
    public static class DataBaseUtils
    {
        #region Constants
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
        #endregion
        #region Methods
        /// <summary>
        /// In order to check database is exist or not use boolena method CheckDataBaseExistence
        /// </summary>
        /// <param name="DataBaseName"></param>
        /// <param name="ServerName"></param>
        /// <returns></returns>
        public static bool IsDataBaseExists(string DataBaseName, string ServerName)
        {
            string connection = "Data Source=.\\" + ServerName + ";Initial Catalog=" + DataBaseName + ";Integrated Security=True";
            var con = new SqlConnection(connection);
            try
            {
                con.Open();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        /// <summary>
        /// In order to check EmailID is being registered already or not. If Email ID is alraedy regitered IsEmailIdExist()
        ///  will return false otherwise it will return true.
        /// </summary>
        public static bool IsEmailIdExist(string Connection, string EmailId)
        {
            var con = new SqlConnection(Connection);
            using (var cmd = new SqlCommand(StoreProcedureIsEmailIdExist, con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@EmailId", SqlDbType.VarChar).Value = EmailId;
                con.Open();
                var dataReader = cmd.ExecuteReader();
                var status = dataReader.HasRows;
                con.Close();
                return status;
            }
        }
        /// <summary>
        /// In order to check EmployeeID is being registered already or not. If Employee ID is alraedy regitered IsEmployeeIdExist()
        ///  will return false otherwise it will return true
        /// </summary>
        public static bool IsEmployeeIdExist(string Connection, string EmployeeId)
        {
            var con = new SqlConnection(Connection);
            using (var cmd = new SqlCommand(StoreProcedureIsEmployeeIdIdExist, con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@EmployeeId", SqlDbType.VarChar).Value = EmployeeId;
                con.Open();
                var dataReader = cmd.ExecuteReader();
                var status = dataReader.HasRows;
                con.Close();
                return status;
            }
        }
        /// <summary>
        /// This method checks given passord is correct in the respect of EmailID
        /// </summary>
        public static bool IsPasswordMatches(string ConnectionString, string Password, string EmailId)
        {
            var con = new SqlConnection(ConnectionString);
            var status = false;
            using (var cmd = new SqlCommand(StoreProcedureGetPassword, con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@EmailID", SqlDbType.VarChar).Value = EmailId;
                try
                {
                    con.Open();
                    var dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        var savedPassword = dr["Password"].ToString();
                        savedPassword = LeaveRegisterUtils.DecryptPassword(savedPassword);
                        status = (savedPassword == Password);
                        con.Close();
                    }
                    return status;
                }
                catch (Exception)
                {
                    return status;
                }
            }
        }
        /// <summary>
        /// This method returnes the password in respect to email id
        /// </summary>
        public static string RecoverPasswordText(string ConnectionString, string EmailId)
        {
            var con = new SqlConnection(ConnectionString);
            var status = string.Empty;
            using (var cmd = new SqlCommand(StoreProcedureGetPassword, con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@EmailID", SqlDbType.VarChar).Value = EmailId;
                try
                {
                    con.Open();
                    var dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        var savedPassword = dr["Password"].ToString();
                        savedPassword = LeaveRegisterUtils.DecryptPassword(savedPassword);
                        status = savedPassword;
                        con.Close();
                    }
                    return status;
                }
                catch (Exception)
                {
                    return status;
                }
            }
        }
        /// <summary>
        /// This method is used to validate entred Employee id is matched corresponding email is or not?
        /// </summary>">
        public static bool IsEmployeeIdMatches(string ConnectionString, string EmailId, string EmployeeId)
        {
            var con = new SqlConnection(ConnectionString);
            var status = false;
            try
            {
                using (var cmd = new SqlCommand(StoreProcedureGetEmployeeId, con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@EmailId", SqlDbType.VarChar).Value = EmailId;
                    con.Open();
                    var dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        status = (dr["EmployeeId"].ToString() == EmployeeId);
                    }
                    con.Close();
                }
                return status;
            }
            catch (Exception)
            {
                return status;
            }
        }

        public static string[] EmployeeSignUpOrChangePassword(string ConnectionString, string Password, string RePassword, string EmployeeId)
        {
            var status = new string[2];
            var con = new SqlConnection(ConnectionString);
            try
            {
                using (var cmd = new SqlCommand(StoreProcedureChangePassword, con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@Password", SqlDbType.VarChar).Value = LeaveRegisterUtils.EncryptPassword(Password);
                    cmd.Parameters.Add("@Repassword", SqlDbType.VarChar).Value = LeaveRegisterUtils.EncryptPassword(RePassword);
                    cmd.Parameters.Add("@EmployeeID", SqlDbType.VarChar).Value = EmployeeId;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    status[0] = "Success";
                    status[1] = LeaveRegisterUtils.EncryptPassword(Password);
                }
                return status;
            }
            catch (Exception)
            {
                status[0] = "Failed";
                return status;
            }
        }
        /// <summary>
        /// This method checks whether Employee hs done his registraion or not
        /// </summary>
        public static bool IsEmployeeDoneSignUpBefore(string ConnectionString, string EmailId)
        {
            var con = new SqlConnection(ConnectionString);
            var status = false;
            using (var cmd = new SqlCommand(StoreProcedureGetPassword, con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@EmailID", SqlDbType.VarChar).Value = EmailId;
                try
                {
                    con.Open();
                    var dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        var savedPassword = dr["Password"].ToString();
                        con.Close();
                        if (!string.IsNullOrEmpty(savedPassword))
                        {
                            status = true;
                        }
                    }
                    return status;
                }
                catch (Exception)
                {
                    return status;
                }
            }
        }
        /// <summary>
        /// Get all home page updates in repect to email id
        /// </summary>
        public static Updates[] GetEmployeeUpdates(string ConnectionString, string EmployeeId)
        {
            var con = new SqlConnection(ConnectionString);
            try
            {
                using (var cmd = new SqlCommand(StoreProcedureGetUpdates, con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@EmployeeID", SqlDbType.VarChar).Value = EmployeeId;
                    con.Open();
                    var totalUpdates = 0;
                    var updatesList = cmd.ExecuteReader();
                    while (updatesList.Read())
                    {
                        totalUpdates += 1;
                    }
                    con.Close();
                    var updates = new Updates[totalUpdates];
                    con.Open();
                    var dr = cmd.ExecuteReader();
                    var i = 0;
                    while (dr.Read())
                    {
                        var update = new Updates
                        {
                            LeaveStatus = dr["LeaveStatus"].ToString(),
                            LeaveType = dr["LeaveAppliedType"].ToString(),
                            LeaveApproved = dr["LeaveApproved"].ToString(),
                            LeaveFrom = dr["LeaveAppliedFrom"].ToString(),
                            LeaveTill = dr["LeaveAppliedTo"].ToString(),
                            Status = "Success"
                        };
                        updates[i] = update;
                        i += 1;
                    }
                    return updates;
                }
            }
            catch (Exception)
            {
                var updates = new Updates[1];
                updates[0].Status = "Failed";
                return updates;
            }
        }
        /// <summary>
        /// Get News of the plateforn
        /// </summary>
        public static News[] GetNews(string ConnectionString)
        {
            var con = new SqlConnection(ConnectionString);
            try
            {
                using (var cmd = new SqlCommand(StoreProcedureGetNews, con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    var totalNewsList = cmd.ExecuteReader();
                    var totalNews = 0;
                    while (totalNewsList.Read())
                    {
                        totalNews += 1;
                    }
                    con.Close();
                    var news = new News[totalNews];
                    con.Open();
                    var dr = cmd.ExecuteReader();
                    var i = 0;
                    while (dr.Read())
                    {
                        var newObj = new News
                        {
                            NewsId = (int)dr["ID"],
                            Title = dr["Title"].ToString(),
                            Date = dr["Date"].ToString(),
                            Description = dr["Description"].ToString(),
                            Status = "Success"
                        };
                        news[i] = newObj;
                        i += 1;
                    }
                    con.Close();
                    return news;
                }
            }
            catch (Exception)
            {
                var news = new News[1];
                news[0].Status = "Failed";
                return news;
            }
        }
        /// <summary>
        /// Get the admin employee updates
        /// </summary>
        public static AdminUpdates[] GetAdminEmployeeUpdates(string ConnectionString)
        {
            var con = new SqlConnection(ConnectionString);
            try
            {
                using (var cmd = new SqlCommand(StoreProcedureGetUpdateAdmin, con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    var totalAdminUpdates = 0;
                    con.Open();
                    var totalRows = cmd.ExecuteReader();
                    while (totalRows.Read())
                    {
                        totalAdminUpdates += 1;
                    }
                    con.Close();
                    var adminUpdates = new AdminUpdates[totalAdminUpdates];
                    con.Open();
                    var dr = cmd.ExecuteReader();
                    var i = 0;
                    while (dr.Read())
                    {
                        var adminUpdate = new AdminUpdates
                        {
                            Name = dr["Name"].ToString(),
                            EmployeeId = dr["EmployeeID"].ToString(),
                            LeaveType = dr["LeaveType"].ToString(),
                            LeaveFrom = dr["FromDate"].ToString(),
                            LeaveTill = dr["ToDate"].ToString(),
                            Id = dr["ID"].ToString()
                        };
                        adminUpdates[i] = adminUpdate;
                        i += 1;
                    }
                    con.Close();
                    return adminUpdates;
                }
            }
            catch (Exception)
            {
                return new AdminUpdates[1];
            }
        }
        /// <summary>
        /// In order to get current day's attendance status use GetCurrentAttendanceDetails method
        /// </summary>
        public static Attendance GetCurrentAttendanceDetails(string Connectionstring, string EmployeeId)
        {
            var attendance = new Attendance();
            var con = new SqlConnection(Connectionstring);
            try
            {
                using (var cmd = new SqlCommand(StoreProcedureGetCurrentAtteendaceStatus, con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@EmployeeID", SqlDbType.VarChar).Value = EmployeeId;
                    con.Open();
                    var dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        attendance.InTime = dr["InTime"].ToString();
                        attendance.LunchTime = dr["LunchTime"].ToString();
                        attendance.AttendanceStatus = dr["AttendanceStatus"].ToString();
                        attendance.OuTime = dr["OutTime"].ToString();
                        attendance.TotalWorked = dr["TotalWorked"].ToString();
                    }
                    con.Close();
                }
                return attendance;
            }
            catch (Exception)
            {
                return attendance;
            }
        }
        /// <summary>
        /// This method is being used to get basic info to extract the employee ifo for home page
        /// </summary>
        public static EmployeeInfo EmployeeInfo(string ConnectionString, string EmployeeId)
        {
            var employeeInfo = new EmployeeInfo();
            var con = new SqlConnection(ConnectionString);
            try
            {
                using (var cmd = new SqlCommand(StoreProcedureGetEmployeeHomeInfo, con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@EmployeeId", SqlDbType.VarChar).Value = EmployeeId;
                    con.Open();
                    var dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        employeeInfo.FirstName = dr["FirstName"].ToString();
                        employeeInfo.LastName = dr["LastName"].ToString();
                        employeeInfo.ProfileImage = dr["ProfilePicture"].ToString();
                        employeeInfo.IsAdmin = (bool)dr["IsAdmin"];
                    }
                    con.Close();
                }
                return employeeInfo;
            }
            catch (Exception)
            {
                return employeeInfo;
            }
        }
        /// <summary>
        /// Get the employee Id
        /// </summary>
        public static string GetEmployeeId(string ConnectionString, string EmailId)
        {
            var employeeId = string.Empty;
            var con = new SqlConnection(ConnectionString);
            try
            {
                using (var cmd = new SqlCommand(StoreProcedureGetEmployeeId, con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@EmailId", SqlDbType.VarChar).Value = EmailId;
                    con.Open();
                    var dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        employeeId = dr["EmployeeId"].ToString();
                    }
                    con.Close();
                }
                return employeeId;
            }
            catch (Exception)
            {
                return employeeId;
            }
        }
        /// <summary>
        /// This methods checks given email id is admin employee or not
        /// </summary>
        public static bool IsAdminEmployee(string ConnectionString, string EmployeeId)
        {
            var employeeInfo = EmployeeInfo(ConnectionString, EmployeeId);
            return employeeInfo.IsAdmin;
        }
        /// <summary>
        /// This method will validate Employee is currentaly logged in or not?
        /// </summary>
        public static bool IsEmployeeLoggedIn(string ConnectionString, string SessionId, string SessionValue)
        {
            var isEmailIdExist = IsEmailIdExist(ConnectionString, SessionId);
            var isPasswordMatches = IsPasswordMatches(ConnectionString, SessionValue, SessionId);
            return ((isEmailIdExist) && (isPasswordMatches));
        }
        /// <summary>
        /// Get all profile informations
        /// </summary>
        public static Employee GetProfileinfo(string ConnectionString, string EmployeeId)
        {
            var employee = new Employee();
            var con = new SqlConnection(ConnectionString);
            try
            {
                using (var cmd = new SqlCommand(StoreProcedureGetProfileInfo, con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@EmployeeId", SqlDbType.VarChar).Value = EmployeeId;
                    con.Open();
                    var dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        employee.FirstName = dr["FirstName"].ToString();
                        employee.LastName = dr["LastName"].ToString();
                        employee.DateOfJoining = dr["DateOfJoining"].ToString();
                        employee.DateofBirth = dr["DateOfBirth"].ToString();
                        employee.Designation = dr["Designation"].ToString();
                        employee.EmailId = dr["EmployeeEmailID"].ToString();
                        employee.EmployeeId = dr["EmployeeId"].ToString();
                        employee.Company = dr["Company"].ToString();
                        //employee.ProfileImage = dr["ProfilePicture"].ToString();
                    }
                    con.Close();
                }
                return employee;
            }
            catch (Exception)
            {
                return employee;
            }
        }

        public static string ManageProfileInformations(string ConnectionString, string DateOfBirth, string EmployeeId)
        {
            var con = new SqlConnection(ConnectionString);
            try
            {
                using (var cmd = new SqlCommand(StoreProcedureProfileEdit, con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@DateOfBirth", SqlDbType.Date).Value = LeaveRegisterUtils.FoamteDate(DateOfBirth);
                    cmd.Parameters.Add("@EmployeeId", SqlDbType.VarChar).Value = EmployeeId;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
                return "Successfully saved your profile informations";
            }
            catch (Exception)
            {
                return "Some exception occur during saving the profile informations";
            }
        }
        /// <summary>
        /// Get today's attendance status details
        /// </summary>
        public static TodayCurrentAttendance GetTodaysAttendanceDetails(string ConnectionString, string EmployeeId)
        {
            var attendance = new TodayCurrentAttendance();
            var con = new SqlConnection(ConnectionString);
            try
            {
                using (var cmd = new SqlCommand(StoreProcedureGetCurrentAtteendaceStatus, con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@EmployeeID", SqlDbType.VarChar).Value = EmployeeId;
                    con.Open();
                    var dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        attendance.InTime = dr["InTime"].ToString();
                        attendance.LunchTime = dr["LunchTime"].ToString();
                        attendance.OutTime = dr["OutTime"].ToString();
                        attendance.TotalWorked = dr["TotalWorked"].ToString();
                        attendance.Status = dr["AttendanceStatus"].ToString();
                    }
                    con.Close();
                    attendance.StatusText = "Success";
                    return attendance;
                }
            }
            catch (Exception)
            {
                return attendance;
            }
        }
        /// <summary>
        /// Fill attendance in, lunch or out time.
        /// </summary>
        public static string FillAttendance(string ConnectionString, string EmployeeId, string Time)
        {
            var con = new SqlConnection(ConnectionString);
            string storeProcedureOperation;
            if (Time == "In")
            {
                storeProcedureOperation = StoreProcedureFillInTimeAttendance;
            }
            else
            {
                storeProcedureOperation = Time == "Lunch" ? StoreProcedureFillLunchTimeAttendance : StoreProcedureFillOutTimeAttendance;
            }
            try
            {
                using (var cmd = new SqlCommand(storeProcedureOperation, con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@EmployeeID", SqlDbType.VarChar).Value = EmployeeId;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
                return "Success";
            }
            catch (Exception)
            {
                return "Failed";
            }
        }
        /// <summary>
        /// Get string array of all leave types
        /// </summary>
        public static string[] GetLeaveTypes(string ConnectionString)
        {
            var leaveTypes = new string[7];
            var con = new SqlConnection(ConnectionString);
            try
            {
                using (var cmd = new SqlCommand("select TypeOfLeave from LeaveConfiguration", con))
                {
                    {
                        con.Open();
                        var dr = cmd.ExecuteReader();
                        var i = 0;
                        while (dr.Read())
                        {
                            {
                                var leaveType = dr["TypeOfLeave"].ToString();
                                if (!string.IsNullOrEmpty(leaveType))
                                {
                                    leaveTypes[i] = leaveType;
                                    i += 1;
                                }
                            }
                        }
                        con.Close();
                    }
                }
                return leaveTypes;
            }
            catch (Exception)
            {
                return leaveTypes;
            }
        }
        /// <summary>
        /// Apply for one days or more than one day's leave.
        /// </summary>
        public static string ApplyForLeave(string ConnectionString, string EmployeeId, string FromDate, string ToDate, string LeaveType)
        {
            var con = new SqlConnection(ConnectionString);
            try
            {
                using (var cmd = new SqlCommand(StoreProcedureApplyLeave, con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@EmployeeID", SqlDbType.VarChar).Value = EmployeeId;
                    cmd.Parameters.Add("@TypeofLeave", SqlDbType.VarChar).Value = (string.IsNullOrEmpty(LeaveType)) ? "Casual Leave" : LeaveType;
                    cmd.Parameters.Add("@FromDate", SqlDbType.Date).Value = LeaveRegisterUtils.FoamteDate(FromDate);
                    cmd.Parameters.Add("@ToDate", SqlDbType.VarChar).Value = LeaveRegisterUtils.FoamteDate(ToDate);
                    cmd.Parameters.Add("@TotalDays", SqlDbType.Int).Value = LeaveRegisterUtils.DateDiffereceInDay(FromDate, ToDate, true);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
                return "success";
            }
            catch (Exception)
            {
                return "Failed";
            }
        }
        /// <summary>
        /// Get Attendance Status by selecting date
        /// </summary>
        public static GetAttendanceStatus GetAttendanceStatus(string ConnectionString, string EmployeeId, string Date)
        {
            var attendanceStatus = new GetAttendanceStatus();
            var con = new SqlConnection(ConnectionString);
            try
            {
                using (var cmd = new SqlCommand(StoreProcedureGetAtteendaceStatus, con))
                {
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@EmployeeID", SqlDbType.VarChar).Value = EmployeeId;
                        cmd.Parameters.Add("@SelecteDDate", SqlDbType.Date).Value = LeaveRegisterUtils.FoamteDate(Date);
                        con.Open();
                        var dr = cmd.ExecuteReader();
                        if (dr.Read())
                        {
                            attendanceStatus.AttendanceStatus = dr["AttendanceStatus"].ToString();
                            attendanceStatus.TotalWorked = Convert.ToInt32(dr["TotalWorked"].ToString());
                            attendanceStatus.Status = true;
                        }
                        con.Close();
                    }
                    return attendanceStatus;
                }
            }
            catch (Exception)
            {
                return attendanceStatus;
            }
        }
        /// <summary>
        /// Get LeaveTaken details
        /// </summary>pe">
        public static int GetLeaveDetails(string ConnectionString, string EmployeeId, string LeaveType)
        {
            int totalWorked = 0;
            var con = new SqlConnection(ConnectionString);
            try
            {
                using (var cmd = new SqlCommand(StoreProcedureGetTotalTakenLeaveStatus, con))
                {
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@EmployeeID", SqlDbType.VarChar).Value = EmployeeId;
                        cmd.Parameters.Add("@LeaveType", SqlDbType.VarChar).Value = LeaveType;
                        con.Open();
                        var dr = cmd.ExecuteReader();
                        if (dr.Read())
                        {
                            var totalTakenLeave = dr["TotalDaysTakenLeave"].ToString();
                            if (!string.IsNullOrEmpty(totalTakenLeave))
                            {
                                totalWorked = Convert.ToInt32(totalTakenLeave);
                            }
                        }
                        con.Close();
                    }
                    return totalWorked;
                }
            }
            catch (Exception)
            {
                return totalWorked;
            }
        }

        /// <summary>
        //Need to work on same
        /// </summary>pe">
        public static AttendanceSheet[] GetOwnAttendanceSheet(string ConnectionString, string EmployeeId, string Month)
        {
            var con = new SqlConnection(ConnectionString);
            try
            {
                using (var cmd = new SqlCommand(StoreProcedureGetOwnAttendanceSheet, con))
                {
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@EmployeeID", SqlDbType.VarChar).Value = EmployeeId;
                        cmd.Parameters.Add("@Month", SqlDbType.VarChar).Value = Month;
                        con.Open();
                        var countItems = cmd.ExecuteReader();
                        var daysOfAttendance = 0;
                        while (countItems.Read())
                        {
                            daysOfAttendance += 1;
                        }
                        con.Close();
                        var attendanceSheet = new AttendanceSheet[daysOfAttendance];
                        con.Open();
                        var dr = cmd.ExecuteReader();
                        var i = 0;
                        while (dr.Read())
                        {
                            var attendanceDetails = new AttendanceSheet()
                            {
                                Date = dr["Date"].ToString(),
                                Day = dr["Day"].ToString(),
                                InTime = dr["InTime"].ToString(),
                                LunchTime = dr["LunchTime"].ToString(),
                                OutTime = dr["OutTime"].ToString(),
                                TotalWorked = dr["TotalWorked"].ToString(),
                                AttendanceStatus = dr["AttendanceStatus"].ToString(),
                                TotalTakenCasualLeave = string.Empty,
                                TotalTakenSickLeave = string.Empty,
                                TotalTakenHalfDatLeave = string.Empty,
                                TotalTakenEarnedeave = string.Empty,
                                Status = true
                            };
                            attendanceSheet[i] = attendanceDetails;
                            i += 1;
                        }
                        con.Close();
                        return attendanceSheet;
                    }
                }
            }
            catch (Exception)
            {
                return new AttendanceSheet[1];
            }
        }
        /// <summary>
        /// Add new Employee by admin
        /// </summary>
        public static string AddEmployeeByAdmin(string ConnectionString, string FirstName, string EmployeeId, string EmailId, string LastName, string Designation, string Doj, string Company)
        {
            var con = new SqlConnection(ConnectionString);
            try
            {
                using (var cmd = new SqlCommand(StoreProcedureEmployeeRegistrationByAdmin, con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@FirstName", SqlDbType.VarChar).Value = FirstName;
                    cmd.Parameters.Add("@EmployeeID", SqlDbType.VarChar).Value = EmployeeId;
                    cmd.Parameters.Add("@EmailId", SqlDbType.VarChar).Value = EmailId;
                    cmd.Parameters.Add("@LastName", SqlDbType.VarChar).Value = LastName;
                    cmd.Parameters.Add("@Designation", SqlDbType.VarChar).Value = Designation;
                    cmd.Parameters.Add("@DateofJoining", SqlDbType.VarChar).Value = LeaveRegisterUtils.FoamteDate(Doj);
                    cmd.Parameters.Add("@Company", SqlDbType.VarChar).Value = Company;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
                return "Success";
            }
            catch (Exception)
            {
                return "Failed";
            }
        }

        public static AddedEmployee[] GetEmployeeList(string ConnectionString)
        {
            var con = new SqlConnection(ConnectionString);
            try
            {
                using (var cmd = new SqlCommand("select ID,EmployeeId,EmployeeEmailId,FirstName,LastName,DateOfJoining,Designation,Company from EmployeeLeaveRegister", con))
                {
                    con.Open();
                    var records = cmd.ExecuteReader();
                    var totalRow = 0;
                    while (records.Read())
                    {
                        totalRow += 1;
                    }
                    con.Close();
                    var addedEmployee = new AddedEmployee[totalRow];
                    var i = 0;
                    con.Open();
                    var dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        var employee = new AddedEmployee
                        {
                            Id = (int)dr["ID"],
                            FirstName = dr["FirstName"].ToString(),
                            LastName = dr["LastName"].ToString(),
                            EmployeeId = dr["EmployeeId"].ToString(),
                            EmailId = dr["EmployeeEmailId"].ToString(),
                            Designation = dr["Designation"].ToString(),
                            DateOfJoining = dr["DateOfJoining"].ToString(),
                            Company = dr["Company"].ToString(),
                            StatusText = "Success"
                        };
                        addedEmployee[i] = employee;
                        i += 1;
                    }
                    con.Close();
                    return addedEmployee;
                }
            }
            catch (Exception)
            {
                return new AddedEmployee[1];
            }
        }
        /// <summary>
        /// Get all comany Leave
        /// </summary>
        public static CompanyLeave[] GetCompanyLeaveList(string ConnectionString)
        {
            var con = new SqlConnection(ConnectionString);
            try
            {
                using (var cmd = new SqlCommand("select ID,Convert(varchar(10),[Date],110)as Date,[Day],ReasonDescription from CompanyLeaveRegistration", con))
                {
                    con.Open();
                    var totalItems = cmd.ExecuteReader();
                    var totalItem = 0;
                    while (totalItems.Read())
                    {
                        totalItem += 1;
                    }
                    con.Close();
                    var companyLeaves = new CompanyLeave[totalItem];
                    con.Open();
                    var dr = cmd.ExecuteReader();
                    var i = 0;
                    while (dr.Read())
                    {
                        var companyLeave = new CompanyLeave
                           {
                               CompanyLeaveId = (int)dr["ID"],
                               Date = dr["Date"].ToString(),
                               Day = dr["Day"].ToString(),
                               Description = dr["ReasonDescription"].ToString(),
                               StatusText = "Success"
                           };
                        companyLeaves[i] = companyLeave;
                        i += 1;
                    }
                    con.Close();
                    return companyLeaves;
                }
            }
            catch (Exception)
            {
                var companyLeaves = new CompanyLeave[1];
                var companyLeave = new CompanyLeave { StatusText = "Failed" };
                companyLeaves[0] = companyLeave;
                return companyLeaves;

            }
        }
        /// <summary>
        /// Add company leave 
        /// </summary>
        public static string AddCompanyLeave(string ConnectionString, string Date, string Day, string Description, string EmployeeId)
        {
            var con = new SqlConnection(ConnectionString);
            try
            {
                using (var cmd = new SqlCommand(StoreProcedureAddLeave, con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@Date", SqlDbType.Date).Value = LeaveRegisterUtils.FoamteDate(Date);
                    cmd.Parameters.Add("@Day", SqlDbType.Char).Value = Day;
                    cmd.Parameters.Add("@ReasonDescription", SqlDbType.VarChar).Value = Description;
                    cmd.Parameters.Add("@EmployeeID", SqlDbType.VarChar).Value = EmployeeId;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
                return "Success";
            }
            catch (Exception)
            {
                return "Failed";
            }
        }
        /// <summary>
        /// Get leave configuration informations like casual, sick leaves and total working hours
        /// </summary>
        public static LeaveDays GetLeaveConfigurationDetailes(string ConnectionString)
        {
            var leavDays = new LeaveDays { StatusText = "Failed" };
            var con = new SqlConnection(ConnectionString);
            try
            {
                using (var cmd = new SqlCommand(StoreProcedureGetLeavesAndWorkingHours, con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    var dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        leavDays.CasualLeave = (int)dr["TotalCasualLeave"];
                        leavDays.SickLeave = (int)dr["ToatlSickLeave"];
                        leavDays.TotalWorkingHours = (int)dr["TotalWorkingHours"];
                        leavDays.StatusText = "Success";
                    }
                    con.Close();
                }
                return leavDays;
            }
            catch (Exception)
            {
                return leavDays;
            }
        }
        /// <summary>
        /// Set leave configuration informations like casual, sick leaves and total working hours
        /// </summary>
        public static string SetLeaveConfigurationInformations(string ConnectionString, int TotalSickLeave,
            int TotalCasualLeave, int TotalWorkingHours)
        {
            var con = new SqlConnection(ConnectionString);
            try
            {
                using (var cmd = new SqlCommand(StoreProcedureSetLeavesAndWorkingHours, con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@TotalCasualLeave", SqlDbType.Int).Value = TotalCasualLeave;
                    cmd.Parameters.Add("@TotalSickLeave", SqlDbType.Int).Value = TotalSickLeave;
                    cmd.Parameters.Add("@TotalWorkingHours", SqlDbType.Int).Value = TotalWorkingHours;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    cmd.Clone();
                }
                return "Success";
            }
            catch (Exception)
            {
                return "Failed";
            }
        }
        /// <summary>
        /// Get list of Employee Id
        /// </summary>
        public static GetEmployeeId[] GetAllEmployeeId(string ConnectionString)
        {
            var con = new SqlConnection(ConnectionString);
            try
            {
                using (var cmd = new SqlCommand("select EmployeeId from EmployeeLeaveRegister", con))
                {
                    con.Open();
                    var dr = cmd.ExecuteReader();
                    var totalEmployeeIndex = 0;
                    while (dr.Read())
                    {
                        totalEmployeeIndex += 1;
                    }
                    con.Close();
                    var totalEmployeeIds = new GetEmployeeId[totalEmployeeIndex];
                    con.Open();
                    var employees = cmd.ExecuteReader();
                    var i = 0;
                    while (employees.Read())
                    {
                        var employeeId = new GetEmployeeId
                        {
                            EmployeeId = employees["EmployeeId"].ToString(),
                            StatusText = "Success"
                        };
                        totalEmployeeIds[i] = employeeId;
                        i += 1;
                    }
                    con.Close();
                    return totalEmployeeIds;
                }
            }
            catch (Exception)
            {
                var totalEmployeesIds = new GetEmployeeId[1];
                totalEmployeesIds[0].StatusText = "Failed";
                return totalEmployeesIds;
            }
        }
        /// <summary>
        /// Change attendance status
        /// </summary>
        public static string ChangeAttendanceStatus(string ConnectionString, string Date, string AttendanceStatus, string EmployeeId)
        {
            var con = new SqlConnection(ConnectionString);
            try
            {
                using (var cmd = new SqlCommand(StoreProcedureChangeAttendanceStatus, con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@SelecteDDate", SqlDbType.Date).Value = LeaveRegisterUtils.FoamteDate(Date);
                    cmd.Parameters.Add("@EmployeeID", SqlDbType.VarChar).Value = EmployeeId;
                    cmd.Parameters.Add("@AttendanceStatus", SqlDbType.VarChar).Value = AttendanceStatus;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
                return "Success";
            }
            catch (Exception)
            {
                return "Failed";
            }
        }
        /// <summary>
        /// Add news and updates
        /// </summary>
        public static string AddNews(string ConnectionString, string EmployeeId, string NewsTitle, string NewsDescrption)
        {
            var con = new SqlConnection(ConnectionString);
            try
            {
                using (var cmd = new SqlCommand(StoreProcedureAddNews, con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@EmployeeID", SqlDbType.VarChar).Value = EmployeeId;
                    cmd.Parameters.Add("@Title", SqlDbType.VarChar).Value = NewsTitle;
                    cmd.Parameters.Add("@Description", SqlDbType.VarChar).Value = NewsDescrption;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
                return "Success";
            }
            catch (Exception)
            {
                return "Failed";
            }
        }
        /// <summary>
        /// Do email setup
        /// </summary>
        public static string DoEmailSetup(string ConnectionString, string EmailId, string Password,
            string ConfirmPassword, string SettingFirst, string SettingSecond, string SettingThird, string SettingFourth)
        {
            var con = new SqlConnection(ConnectionString);
            try
            {
                using (var cmd = new SqlCommand(StoreProcedureEmailSetup, con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@EmailID", SqlDbType.VarChar).Value = EmailId;
                    cmd.Parameters.Add("@Password", SqlDbType.VarChar).Value = LeaveRegisterUtils.DecryptPassword(Password);
                    cmd.Parameters.Add("@ConfirmPassword", SqlDbType.VarChar).Value = LeaveRegisterUtils.DecryptPassword(Password);
                    cmd.Parameters.Add("@SeetingFirst", SqlDbType.VarChar).Value = (string.IsNullOrEmpty(SettingFirst))
                        ? ""
                        : SettingFirst;
                    cmd.Parameters.Add("@Seetingsecond", SqlDbType.VarChar).Value = (string.IsNullOrEmpty(SettingSecond))
                        ? ""
                        : SettingSecond;
                    cmd.Parameters.Add("@SeetingThird", SqlDbType.VarChar).Value = (string.IsNullOrEmpty(SettingThird))
                        ? ""
                        : SettingThird;
                    cmd.Parameters.Add("@SeetingFourth", SqlDbType.VarChar).Value = (string.IsNullOrEmpty(SettingFourth))
                        ? ""
                        : SettingFourth;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
                return "Success";
            }
            catch (Exception)
            {
                return "Failed";
            }
        }
        /// <summary>
        /// Get email setup informations
        /// </summary>
        public static Email GetEmailSetupInfo(string ConnectionString)
        {
            var emailSetupInfo = new Email { Status = "Failed" };
            var con = new SqlConnection(ConnectionString);
            try
            {
                using (var cmd = new SqlCommand(StoreProcedureGetEmialSetupInfo, con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    var dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        emailSetupInfo.EmailId = dr["EmailId"].ToString();
                        emailSetupInfo.Password = dr["Password"].ToString();
                        emailSetupInfo.SettingFirst = dr["SeetingFirst"].ToString();
                        emailSetupInfo.SettingSecond = dr["SeetingSecond"].ToString();
                        emailSetupInfo.SettingThird = dr["SeetingThird"].ToString();
                        emailSetupInfo.SettingFourth = dr["SeetingFourth"].ToString();
                        emailSetupInfo.Status = "Suceess";
                    }
                    con.Close();
                }
                return emailSetupInfo;
            }
            catch (Exception)
            {
                return emailSetupInfo;
            }
        }
        /// <summary>
        /// EUpdate the email setup
        /// </summary>
        public static string UpdateEmailSetup(string ConnectionString, string EmailId, string Password,
            string ConfirmPassword, string SettingFirst, string SettingSecond, string SettingThird, string SettingFourth)
        {
            var con = new SqlConnection(ConnectionString);
            try
            {
                using (var cmd = new SqlCommand(StoreProcedureEditEmailSetup, con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@EmailID", SqlDbType.VarChar).Value = EmailId;
                    cmd.Parameters.Add("@Password", SqlDbType.VarChar).Value = LeaveRegisterUtils.DecryptPassword(Password);
                    cmd.Parameters.Add("@ConfirmPassword", SqlDbType.VarChar).Value = LeaveRegisterUtils.DecryptPassword(Password);
                    cmd.Parameters.Add("@SeetingFirst", SqlDbType.VarChar).Value = (string.IsNullOrEmpty(SettingFirst))
                        ? ""
                        : SettingFirst;
                    cmd.Parameters.Add("@Seetingsecond", SqlDbType.VarChar).Value = (string.IsNullOrEmpty(SettingSecond))
                        ? ""
                        : SettingSecond;
                    cmd.Parameters.Add("@SeetingThird", SqlDbType.VarChar).Value = (string.IsNullOrEmpty(SettingThird))
                        ? ""
                        : SettingThird;
                    cmd.Parameters.Add("@SeetingFourth", SqlDbType.VarChar).Value = (string.IsNullOrEmpty(SettingFourth))
                        ? ""
                        : SettingFourth;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
                return "Success";
            }
            catch (Exception)
            {
                return "Failed";
            }
        }
        /// <summary>
        /// Check attendance details row is exist or not
        /// </summary>
        public static bool IsAttendanceRowExist(string ConnectionString, string EmployeeId, string Date)
        {
            var con = new SqlConnection(ConnectionString);
            using (var cmd = new SqlCommand(StoreProcedureIsAttendanceRowExist, con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@EmployeeId", SqlDbType.VarChar).Value = EmployeeId;
                cmd.Parameters.Add("@Date", SqlDbType.VarChar).Value = LeaveRegisterUtils.FoamteDate(Date);
                con.Open();
                var dataReader = cmd.ExecuteReader();
                var status = dataReader.HasRows;
                con.Close();
                return status;
            }
        }

        public static string AddAttendanceByAdmin(string ConnectionString, string EmployeeId, string Date,
            string AttendanceStatus)
        {
            var con = new SqlConnection(ConnectionString);
            try
            {
                using (var cmd = new SqlCommand(StoreProcedureAddAttendanceByAdmin, con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@EmployeeId", SqlDbType.VarChar).Value = EmployeeId;
                    cmd.Parameters.Add("@Date", SqlDbType.VarChar).Value = LeaveRegisterUtils.FoamteDate(Date);
                    cmd.Parameters.Add("@AttendanceStatus", SqlDbType.VarChar).Value = AttendanceStatus;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
                return "Success";
            }
            catch (Exception)
            {
                return "Failed";
            }
        }

        public static string UpdateEmployeeInfoByAdmin(string ConnectionString, int Id, string FirstName,
            string LastName, string Doj, string Designation, string Company)
        {
            var con = new SqlConnection(ConnectionString);
            try
            {
                using (var cmd = new SqlCommand(StoreProcedureUpdateEmployeeInfoByAdmin, con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@ID", SqlDbType.Int).Value = Id;
                    cmd.Parameters.Add("@FirstName", SqlDbType.VarChar).Value = FirstName;
                    cmd.Parameters.Add("@LastName", SqlDbType.VarChar).Value = LastName;
                    cmd.Parameters.Add("@Doj", SqlDbType.Date).Value = LeaveRegisterUtils.FoamteDate(Doj);
                    cmd.Parameters.Add("@Designation", SqlDbType.VarChar).Value = Designation;
                    cmd.Parameters.Add("@Company", SqlDbType.VarChar).Value = Company;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    return "Success";
                }
            }
            catch (Exception)
            {
                return "Failed";
            }
        }
        /// <summary>
        /// Delete added company leave 
        /// </summary>
        public static string DeleteAddedCompanyLeave(string ConnectionString, int Id)
        {
            var con = new SqlConnection(ConnectionString);
            try
            {
                using (var cmd = new SqlCommand(StoreProcedureDeleteAddedLeave, con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@id", SqlDbType.Int).Value = Id;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
                return "Success";
            }
            catch (Exception)
            {
                return "Failed";
            }
        }
        /// <summary>
        /// Update the added  company leave
        /// </summary>
        public static string UpdateAddedCompanyLeave(string ConnectionString, string Date, string Day, string Reason,
            string ModifiedById, int Id)
        {
            var con = new SqlConnection(ConnectionString);
            try
            {
                using (var cmd = new SqlCommand(StoreProcedureUpdateCompanyLeave, con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@Date", SqlDbType.Date).Value = LeaveRegisterUtils.FoamteDate(Date);
                    cmd.Parameters.Add("@Day", SqlDbType.Char).Value = Day;
                    cmd.Parameters.Add("@Reason", SqlDbType.VarChar).Value = Reason;
                    cmd.Parameters.Add("@Modifiedby", SqlDbType.VarChar).Value = ModifiedById;
                    cmd.Parameters.Add("@Id", SqlDbType.Int).Value = Id;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
                return "Success";
            }
            catch (Exception)
            {
                return "failed";
            }
        }
        /// <summary>
        /// Get company news for admin Company News table
        /// </summary>
        public static NewsAndUpdate[] GetNewsAndUpdate(string ConectionString)
        {
            var con = new SqlConnection(ConectionString);
            try
            {
                using (var cmd = new SqlCommand("select ID,Title,Description from NewsAndUpdates", con))
                {
                    con.Open();
                    var news = cmd.ExecuteReader();
                    var totalNewsNumber = 0;
                    while (news.Read())
                    {
                        totalNewsNumber += 1;
                    }
                    con.Close();
                    var newsAndUpdates = new NewsAndUpdate[totalNewsNumber];
                    con.Open();
                    var dr = cmd.ExecuteReader();
                    var i = 0;
                    while (dr.Read())
                    {
                        var newsAndUpdate = new NewsAndUpdate
                        {
                            NewsId = dr["ID"].ToString(),
                            Title = dr["Title"].ToString(),
                            Description = dr["Description"].ToString(),
                            StatusText = "Success"
                        };
                        newsAndUpdates[i] = newsAndUpdate;
                        i += 1;
                    }
                    con.Close();
                    return newsAndUpdates;
                }
            }
            catch (Exception)
            {
                return new NewsAndUpdate[1];
            }
        }
        /// <summary>
        /// Delete added news
        /// </summary>
        public static string DeleteAddedNews(string ConnectionString, int Id)
        {
            var con = new SqlConnection(ConnectionString);
            try
            {
                using (var cmd = new SqlCommand(StoreProcedureDeletedAddedNews, con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@Id", SqlDbType.Int).Value = Id;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    return "Success";
                }
            }
            catch (Exception)
            {
                return "Failed";
            }
        }
        /// <summary>
        /// Update the added news
        /// </summary>
        public static string UpdateNews(string ConnectionString, string EmployeeId, int Id, string Title,
            string Description)
        {
            var con = new SqlConnection(ConnectionString);
            try
            {
                using (var cmd = new SqlCommand(StoreProcedureUpdateNews, con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@Id", SqlDbType.Int).Value = Id;
                    cmd.Parameters.Add("@EmployeeID", SqlDbType.VarChar).Value = EmployeeId;
                    cmd.Parameters.Add("@Title", SqlDbType.VarChar).Value = Title;
                    cmd.Parameters.Add("@Description", SqlDbType.VarChar).Value = Description;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    return "Success";
                }
            }
            catch (Exception)
            {
                return "Failed";
            }
        }
        /// <summary>
        /// Accept or Reject leave by admin
        /// </summary>
        public static string AcceptRejectLeaveByAdmin(string ConnectionString, int LeaveId, string LeaveStatus)
        {
            var con = new SqlConnection(ConnectionString);
            try
            {
                using (var cmd = new SqlCommand(StoreProcedureActionOnOneDayLeaveByAdmin, con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@ID", SqlDbType.Int).Value = LeaveId;
                    cmd.Parameters.Add("@LeaveStatus", SqlDbType.VarChar).Value = LeaveStatus;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    return "Success";
                }

            }
            catch (Exception)
            {
                return "Failed";
            }
        }
        /// <summary>
        /// Fill the attendance sheet of Applied leave date
        /// </summary>
        public static string FillApprovedLeaveAttendanceShaeet(string ConnectionString, string FromDate, string ToDate,
            string LeaveType, string EmployeeId, int TotalDays)
        {
            var con = new SqlConnection(ConnectionString);
            try
            {
                var date = Convert.ToDateTime(LeaveRegisterUtils.GetDatePart(FromDate));
                for (var i = 0; i < TotalDays; i++)
                {
                    date = LeaveRegisterUtils.GetDateForAttendance(date);
                    using (var cmd = new SqlCommand(StoreProcedureFillApproveLeaveAttendance, con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@Date", SqlDbType.Date).Value = date;
                        cmd.Parameters.Add("@EmployeeId", SqlDbType.VarChar).Value = EmployeeId;
                        cmd.Parameters.Add("@AttendanceStatus", SqlDbType.VarChar).Value = LeaveType;
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }
                    date = date.AddDays(1);
                }
                return "Success";
            }
            catch (Exception)
            {
                return "Failed";
            }
        }
        /// <summary>
        /// Get update by Id
        /// </summary>
        public static Updates GetUpdateById(string ConnectionString, int Id)
        {
            var con = new SqlConnection(ConnectionString);
            try
            {
                using (var cmd = new SqlCommand(StoreProcedureGetUpdateItemById, con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@ID", SqlDbType.Int).Value = Id;
                    con.Open();
                    var dr = cmd.ExecuteReader();
                    var update = new Updates();
                    while (dr.Read())
                    {
                        update.LeaveAppliedBy = dr["LeaveAppliedBy"].ToString();
                        update.LeaveType = dr["LeaveAppliedType"].ToString();
                        update.LeaveTill = dr["LeaveAppliedTo"].ToString();
                        update.LeaveFrom = dr["LeaveAppliedFrom"].ToString();
                        update.LeaveStatus = dr["LeaveStatus"].ToString();
                        update.TotalDays = (int)dr["TotalDays"];
                        update.Status = "Success";
                    }
                    con.Close();
                    return update;
                }
            }
            catch (Exception)
            {
                return new Updates { Status = "Failed" };
            }
        }
        #endregion
    }
}