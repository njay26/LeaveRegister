using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Web.Services;
using System.Xml;
using LeaveRegister.IImplementation;
using LeaveRegister.Utils;
using LeaveRegister.Models;

namespace LeaveRegister.WebServices
{
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    [System.Web.Script.Services.ScriptService]
    public class DataBase : LeaveRegisterMembers, IDataBase
    {
        #region Variables
        public string ConnectionString = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
        #endregion
        #region Methodsz
        [WebMethod]
        public string CreateDatabase(string ServerName)
        {
            if (!string.IsNullOrEmpty(ServerName))
            {
                const string createDataBaseQuery = "create database LeaveRegister";
                var connectionstring = "Data Source=.\\" + ServerName +
                                       ";Initial Catalog=master;Integrated Security=True";
                var con = new SqlConnection(connectionstring);
                using (var cmd = new SqlCommand(createDataBaseQuery, con))
                {
                    try
                    {
                        string status;
                        if (!DataBaseUtils.IsDataBaseExists("LeaveRegister", ServerName))
                        {
                            con.Open();
                            cmd.ExecuteNonQuery();
                            con.Close();
                            status = "Database successfully created";
                        }
                        else
                        {
                            status = "Already same name of database is availbele on your server. " +
                                     "If you are doing setup first time please go to the server and delete the " +
                                     "existing database name of <b> Leaveregiste</b> and try to recreate.";
                        }
                        return status;
                    }
                    catch (SqlException)
                    {
                        return "Some error came during code execution. Please check your server name, " +
                               "seems like there is no any instance of same name of the sql server instance in your machine.";
                    }
                }
            }
            return "Please enter the server name";
        }
        [WebMethod]
        public string CreateTables()
        {
            var con = new SqlConnection(ConnectionString);
            using (var cmd = new SqlCommand(PrepareTablesQueries(), con))
            {
                try
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    return "Tables successfully created";
                }
                catch (SqlException)
                {
                    return "Error occured";
                }
            }
        }
        [WebMethod]
        public string CreateStoreProcedures()
        {
            var con = new SqlConnection(ConnectionString);
            try
            {
                string status = "These procedures: <br/><b>";
                using (var cmd = new SqlCommand(PrepareEditEmailSetupQuery(), con))
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    status += "1. " + StoreProcedureEditEmailSetup + " </br>";
                }
                using (var cmd = new SqlCommand(PrepareEditProfileImageQuery(), con))
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    status += "2. " + StoreProcedureEditProfileImage + " </br>";
                }
                using (var cmd = new SqlCommand(PrepareEmailSetupQuery(), con))
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    status += "3. " + StoreProcedureEmailSetup + " </br>";
                }
                using (var cmd = new SqlCommand(PrepareEmployeeListQuery(), con))
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    status += "4. " + StoreProcedureEmployeeList + " </br>";
                }
                using (var cmd = new SqlCommand(PrepareEmployeeRegistrationByAdminQuery(), con))
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    status += "5. " + StoreProcedureEmployeeRegistrationByAdmin + " </br>";
                }
                using (var cmd = new SqlCommand(PrepareEmployeeRegistrationBySuperAdminQuery(), con))
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    status += "6. " + StoreProcedureEmployeeRegistrationBySuperAdmin + " </br>";
                }
                using (var cmd = new SqlCommand(PrepareFillInTimeAttendanceQuery(), con))
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    status += "7. " + StoreProcedureFillInTimeAttendance + " </br>";
                }
                using (var cmd = new SqlCommand(PrepareFillLunchTimeAttendanceQuery(), con))
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    status += "8. " + StoreProcedureFillLunchTimeAttendance + " </br>";
                }
                using (var cmd = new SqlCommand(PrepareFillOutTimeAttendanceQuery(), con))
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    status += "9. " + StoreProcedureFillOutTimeAttendance + " </br>";
                }
                using (var cmd = new SqlCommand(PrepareGetAtteendaceStatusQuery(), con))
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    status += "10. " + StoreProcedureGetAtteendaceStatus + " </br>";
                }
                using (var cmd = new SqlCommand(PrepareGetEmialSetupInfoQuery(), con))
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    status += "11. " + StoreProcedureGetEmialSetupInfo + " </br>";
                }
                using (var cmd = new SqlCommand(PrepareGetLeaveAcceptRejectDetailsQuery(), con))
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    status += "12. " + StoreProcedureGetLeaveAcceptRejectDetails + " </br>";
                }
                using (var cmd = new SqlCommand(PrepareGetLeaveListQuery(), con))
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    status += "13. " + StoreProcedureGetLeaveList + " </br>";
                } using (var cmd = new SqlCommand(PrepareGetLeavesAndWorkingHoursQuery(), con))
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    status += "14. " + StoreProcedureGetLeavesAndWorkingHours + " </br>";
                }
                using (var cmd = new SqlCommand(PrepareGetNewsQuery(), con))
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    status += "15. " + StoreProcedureGetNews + " </br>";
                } using (var cmd = new SqlCommand(PrepareGetOwnAttendanceSheetQuery(), con))
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    status += "16. " + StoreProcedureGetOwnAttendanceSheet + " </br>";
                } using (var cmd = new SqlCommand(PrepareGetPasswordQuery(), con))
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    status += "17. " + StoreProcedureGetPassword + " </br>";
                }
                using (var cmd = new SqlCommand(PrepareGetProfileInfoQuery(), con))
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    status += "18. " + StoreProcedureGetProfileInfo + " </br>";
                }
                using (var cmd = new SqlCommand(PrepareGetTotalTakenLeaveStatusQuery(), con))
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    status += "19. " + StoreProcedureGetTotalTakenLeaveStatus + " </br>";
                } using (var cmd = new SqlCommand(PrepareGetUpdateAdminQuery(), con))
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    status += "20. " + StoreProcedureGetUpdateAdmin + " </br>";
                } using (var cmd = new SqlCommand(PrepareGetUpdatesQuery(), con))
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    status += "21. " + StoreProcedureGetUpdates + " </br>";
                } using (var cmd = new SqlCommand(PrepareGetWeekendQuery(), con))
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    status += "22. " + StoreProcedureGetWeekend + " </br>";
                } using (var cmd = new SqlCommand(PrepareProfileEditQuery(), con))
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    status += "23. " + StoreProcedureProfileEdit + " </br>";
                } using (var cmd = new SqlCommand(PrepareSaveProfileImageQuery(), con))
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    status += "24. " + StoreProcedureSaveProfileImage + " </br>";
                }
                using (var cmd = new SqlCommand(PrepareSetLeavesAndWorkingHoursQuery(), con))
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    status += "25. " + StoreProcedureSetLeavesAndWorkingHours + " </br>";
                }
                using (var cmd = new SqlCommand(PrepareSetWeekendQuery(), con))
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    status += "26. " + StoreProcedureSetWeekend + " </br>";
                }
                using (var cmd = new SqlCommand(PrepareSpAddLeaveQuery(), con))
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    status += "27. " + StoreProcedureAddLeave + " </br>";
                }
                using (var cmd = new SqlCommand(PrepareSpAddNewsQuery(), con))
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    status += "28. " + StoreProcedureAddNews + " </br>";
                }
                using (var cmd = new SqlCommand(PrepareUpdateNewsQuery(), con))
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    status += "29. " + StoreProcedureUpdateNews + " </br>";
                }
                using (var cmd = new SqlCommand(PrepareSpApplyLeaveQuery(), con))
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    status += "30. " + StoreProcedureApplyLeave + " </br>";
                }
                using (var cmd = new SqlCommand(PrepareSpQueryActionOnOneDayLeaveByAdmin(), con))
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    status += "31. " + StoreProcedureActionOnOneDayLeaveByAdmin + " </br>";
                }
                using (var cmd = new SqlCommand(PrepareSpChangeAttendanceStatusQuery(), con))
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    status += "32. " + StoreProcedureChangeAttendanceStatus + " </br>";
                }
                using (var cmd = new SqlCommand(PrepareSpDeleteAddedLeaveQuery(), con))
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    status += "33. " + StoreProcedureDeleteAddedLeave + " </br>";
                }
                using (var cmd = new SqlCommand(PrepareSpChangePasswordQuery(), con))
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    status += "34. " + StoreProcedureChangePassword + " </br>";
                }
                using (var cmd = new SqlCommand(PrepareSpQueryActionOnMoreThanOneDayLeaveByAdmin(), con))
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    status += "35. " + StoreProcedureActionOnMoreThanOneDayLeaveByAdmin + " </br>";
                }
                using (var cmd = new SqlCommand(PrepareIsEmailIdExistQuery(), con))
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    status += "36. " + StoreProcedureIsEmailIdExist + " </br>";
                }
                using (var cmd = new SqlCommand(PrepareGetEmployeeIdQuery(), con))
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    status += "37. " + StoreProcedureGetEmployeeId + " </br>";
                }
                using (var cmd = new SqlCommand(PrepareGetCurrentAtteendaceStatusQuery(), con))
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    status += "38. " + StoreProcedureGetCurrentAtteendaceStatus + " </br>";
                }
                using (var cmd = new SqlCommand(PrepareGetEmployeeHomeInfoQuery(), con))
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    status += "39. " + StoreProcedureGetEmployeeHomeInfo + " </br>";
                }
                using (var cmd = new SqlCommand(PrepareIsAttendanceRowExistQuery(), con))
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    status += "40. " + StoreProcedureIsAttendanceRowExist + " </br>";
                }
                using (var cmd = new SqlCommand(PrepareAddAttendanceByAdminQuery(), con))
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    status += "41. " + StoreProcedureAddAttendanceByAdmin + " </br>";
                }
                using (var cmd = new SqlCommand(PrepareUpdateEmployeeInfoByAdminQuery(), con))
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    status += "42. " + StoreProcedureUpdateEmployeeInfoByAdmin + " </br>";
                }
                using (var cmd = new SqlCommand(PrepareUpdateCompanyLeaveQuery(), con))
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    status += "43. " + StoreProcedureUpdateCompanyLeave + " </br>";
                }
                using (var cmd = new SqlCommand(PrepareDeletedAddedNewsQuery(), con))
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    status += "44. " + StoreProcedureDeletedAddedNews + " </br>";
                }
                using (var cmd = new SqlCommand(PrepareFillApproveLeaveAttendanceeQuery(), con))
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    status += "45. " + StoreProcedureFillApproveLeaveAttendance + " </br>";
                }
                using (var cmd = new SqlCommand(PrepareGetUpdateItemByIdQuery(), con))
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    status += "46. " + StoreProcedureGetUpdateItemById + " </br>";
                }
                using (var cmd = new SqlCommand(PrepareIsEmployeeIdIdExistQuery(), con))
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    status += "47. " + StoreProcedureIsEmployeeIdIdExist;
                }
                status += "</b><br/> is being successfully created.";
                return status;
            }
            catch (Exception e)
            {
                return "<b>Some error occures during store procedure creation. Exception detail is:</b></br>" + e.Message;
            }
        }
        [WebMethod]
        public string CreateFunctions()
        {
            var con = new SqlConnection(ConnectionString);
            try
            {
                string status = "SQL function: <br/><b>";
                using (var cmd = new SqlCommand(PrepareSqlInTimeAttendancestatusFunctionsQueries(), con))
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    status += "1. " + FunctionInTimeAttendancestatus + "<br/>";
                }
                using (var cmd = new SqlCommand(PrepareSqlLunchTimeAttendancestatusFunctionsQueries(), con))
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    status += "2. " + FunctionLunchTimeAttendancestatus + "<br/>";
                }
                using (var cmd = new SqlCommand(PrepareSqlOutTimeAttendancestatusFunctionsQueries(), con))
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    status += "3. " + FunctionOutTimeAttendancestatus;
                }
                status += "</b> <br/>has been created successfully.";
                return status;
            }
            catch (Exception e)
            {
                return "<b>Some error occures during SQL function creation. Exception detail is:</b></br>" + e.Message;
            }
        }
        [WebMethod]
        public string DoLeaveRegisterSetting()
        {
            const string query = " insert into LeaveConfiguration(ModifiedDate) Values(GETDATE())";
            var con = new SqlConnection(ConnectionString);
            try
            {
                using (var cmd = new SqlCommand(query, con))
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    return "Successfully setting is done for Leave Register.";
                }
            }
            catch (Exception e)
            {
                return "Some error occored duriong leave register setting. Exception detail is: " + e.Message;
            }
        }
        [WebMethod]
        public string DoWebConfiguration(string ServerName)
        {
            var path = System.Web.HttpContext.Current.Server.MapPath("~/Web.config");
            var newConnectionString = @"Data Source=.\" + ServerName + ";Initial Catalog=LeaveRegister;Integrated Security=True";
            var xDoc = new XmlDocument();
            try
            {
                xDoc.Load(path);
                XmlNodeList nodeList = xDoc.GetElementsByTagName("connectionStrings");
                XmlNodeList nodeAppSettings = nodeList[0].ChildNodes;
                XmlAttributeCollection xmlAttCollection = nodeAppSettings[0].Attributes;
                string status;
                if (xmlAttCollection != null)
                {
                    xmlAttCollection[0].InnerXml = "DBCS";
                    xmlAttCollection[1].InnerXml = newConnectionString;
                    status = "Success fully done web config with server name: <b>" + ServerName + "</b>";
                }
                else
                {
                    status = "Fiales to do the web config. Seems like <b>web.config</b> " +
                            "file is not available to do the coniguration.";
                }
                xDoc.Save(path);
                return status;
            }
            catch (Exception)
            {
                return "Error occured during setting the <b>web.config</b> file. Please check you file and intial setting of connection string.";
            }
        }
        [WebMethod]
        public string AddAdminEmplyee(string EmailId, string EmployeeId, string Password, string RePassword, string FirstName, string LastName, string DateOfJoining, string Designation, string Company)
        {
            string status;
            if (string.IsNullOrEmpty(FirstName) || string.IsNullOrEmpty(LastName) || string.IsNullOrEmpty(DateOfJoining) || string.IsNullOrEmpty(Designation) ||
                string.IsNullOrEmpty(Company) || string.IsNullOrEmpty(EmailId)
                || string.IsNullOrEmpty(Password) || string.IsNullOrEmpty(EmployeeId) || string.IsNullOrEmpty(RePassword))
            {
               return "All informations is required";
            }
            if (LeaveRegisterUtils.VaidateEmail(EmailId) && !string.IsNullOrEmpty(EmployeeId))
            {
                if ((Password == RePassword) && (Password.Length > 6))
                {
                    var con = new SqlConnection(ConnectionString);
                    try
                    {
                        var isEmailIdExist = DataBaseUtils.IsEmailIdExist(ConnectionString, EmailId);
                        if ((!isEmailIdExist) && (!DataBaseUtils.IsEmployeeIdExist(ConnectionString, EmployeeId)))
                        {
                            using (var cmd = new SqlCommand(StoreProcedureEmployeeRegistrationBySuperAdmin, con))
                            {
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Parameters.Add("@EmailId", SqlDbType.VarChar).Value = EmailId;
                                cmd.Parameters.Add("@EmployeeID", SqlDbType.VarChar).Value = EmployeeId;
                                cmd.Parameters.Add("@Password", SqlDbType.VarChar).Value =
                                    LeaveRegisterUtils.EncryptPassword(Password);
                                cmd.Parameters.Add("@ReEnterPassword", SqlDbType.VarChar).Value =
                                    LeaveRegisterUtils.EncryptPassword(RePassword);
                                cmd.Parameters.Add("@FirstName", SqlDbType.VarChar).Value = FirstName;
                                cmd.Parameters.Add("@LastName", SqlDbType.VarChar).Value = LastName;
                                cmd.Parameters.Add("@DOJ", SqlDbType.VarChar).Value = LeaveRegisterUtils.FoamteDate(DateOfJoining);
                                cmd.Parameters.Add("@Desgnation", SqlDbType.VarChar).Value = Designation;
                                cmd.Parameters.Add("@Company", SqlDbType.VarChar).Value = Company;
                                con.Open();
                                cmd.ExecuteNonQuery();
                                con.Close();
                                status = "Successfully admin employee has been created. Created employee's Email Id: " +
                                         EmailId + " and Employee Id: " + EmployeeId;
                            }
                        }
                        else
                        {
                            status = (!isEmailIdExist) ? ("Employee id " + EmployeeId + " is already registered. If" +
                                                         " It's not your knowledge check with superadmin") : ("Email id " + EmailId + " is" +
                                                         " already registered. If It's not your knowledge check with superadmin");
                        }
                    }
                    catch (Exception)
                    {
                        status = "Some problem occur during adding the admin employee.";
                    }
                }
                else
                {
                    status = (Password != RePassword) ? ("Password and Repassword is not maching") : ("Please enter minimum 5 character.");
                }
            }
            else
            {
                status = (!string.IsNullOrEmpty(EmployeeId)) ? ("Please enter correct emial id. Example: <b>leave@gmail.com</b>") : ("Employee Id can not blank");
            }
            return status;
        }
        /// <summary>
        /// Prepare all tables sql query
        /// </summary>
        /// <returns></returns>
        private static string PrepareTablesQueries()
        {
            var tablesQuery = new StringBuilder();
            tablesQuery.Append(" create table EmployeeLeaveRegister(\n");
            tablesQuery.Append("ID int identity,\n");
            tablesQuery.Append("EmployeeId varchar(10) primary key not null,\n");
            tablesQuery.Append("EmployeeEmailID varchar(50) not null unique,\n");
            tablesQuery.Append("FirstName Char(30),\n");
            tablesQuery.Append("LastName char(30),\n");
            tablesQuery.Append("DateOfBirth date,\n");
            tablesQuery.Append("DateOfJoining date,\n");
            tablesQuery.Append("Designation varchar(50),\n");
            tablesQuery.Append("Company varchar(30),\n");
            tablesQuery.Append("ProfilePicture binary,\n");
            tablesQuery.Append("[Password] varchar(40),\n");
            tablesQuery.Append("RePassword varchar(40),\n");
            tablesQuery.Append("CONSTRAINT CK_ValidatePassword CHECK  ([Password]=RePassword),\n");
            tablesQuery.Append("TatoalSickLeavetaken int default 0,\n");
            tablesQuery.Append("TotalCasualLeaveTaken int default 0,\n");
            tablesQuery.Append("TotalHalfDayLeaveTaken int default 0,\n");
            tablesQuery.Append("TotalEarnedLeaveGot int default 0,\n");
            tablesQuery.Append("TotalTakenLeave int default 0,\n");
            tablesQuery.Append("IsEmployeeAdmin bit default 0\n");
            tablesQuery.Append(");\n");
            tablesQuery.Append("create table AttendanceSheet(\n");
            tablesQuery.Append("ID int primary key identity ,\n");
            tablesQuery.Append("EmployeeId varchar(10) not null,\n");
            tablesQuery.Append("[Date] date not null,\n");
            tablesQuery.Append("[Day] varchar(30) not null,\n");
            tablesQuery.Append("InTime time,\n");
            tablesQuery.Append("LunchTime time,\n");
            tablesQuery.Append("OutTime time,\n");
            tablesQuery.Append("TotalWorked varchar(100),\n");
            tablesQuery.Append("AttendanceStatus char(40) not null,\n");
            tablesQuery.Append("constraint FK_EmployeeID\n");
            tablesQuery.Append("Foreign Key(EmployeeId) References EmployeeLeaveRegister(EmployeeId)\n");
            tablesQuery.Append(");\n");
            tablesQuery.Append("create table CompanyLeaveRegistration \n");
            tablesQuery.Append("(\n");
            tablesQuery.Append("ID int primary key identity,\n");
            tablesQuery.Append("[Date] date not null unique,\n");
            tablesQuery.Append("[Day] char(40),\n");
            tablesQuery.Append("ReasonDescription varchar(150) not null,\n");
            tablesQuery.Append("LeaveRegisteredBy varchar(10) not null,\n");
            tablesQuery.Append("LeaveRegisteredDate date default SYSDATETIME(),\n");
            tablesQuery.Append("ModifiedBy varchar(10),\n");
            tablesQuery.Append("ModifiedDate date, \n");
            tablesQuery.Append("constraint  FK_ValidateAdmin\n");
            tablesQuery.Append("Foreign key (LeaveRegisteredBy) references EmployeeLeaveRegister(EmployeeId),\n");
            tablesQuery.Append(");\n");
            tablesQuery.Append("create table EmailSetup\n");
            tablesQuery.Append("(\n");
            tablesQuery.Append("ID int primary key identity,\n");
            tablesQuery.Append("EmailId varchar(30),\n");
            tablesQuery.Append("[Password] varchar(30),\n");
            tablesQuery.Append("ConfirmPassword varchar(30),\n");
            tablesQuery.Append("SeetingFirst varchar(50),\n");
            tablesQuery.Append("SeetingSecond varchar(50),\n");
            tablesQuery.Append("SeetingThird varchar(50),\n");
            tablesQuery.Append("SeetingFourth varchar(50),\n");
            tablesQuery.Append("constraint CK_EmailPassword Check([Password]=ConfirmPassword)\n");
            tablesQuery.Append(");\n");
            tablesQuery.Append("create table LeaveConfiguration\n");
            tablesQuery.Append("(\n");
            tablesQuery.Append("ID int primary key identity,\n");
            tablesQuery.Append("TotalCasualLeave int default 0,\n");
            tablesQuery.Append("ToatlSickLeave int default 0,\n");
            tablesQuery.Append("TotalWorkingHours int default 8,\n");
            tablesQuery.Append("WeekEnd char(30),\n");
            tablesQuery.Append("TypeOfLeave Varchar(50),\n");
            tablesQuery.Append("[Owner] varchar(10),\n");
            tablesQuery.Append("LeaveConfigurationEnteredDate date default SYSDATETIME(),\n");
            tablesQuery.Append("ModifiedBy varchar(10),\n");
            tablesQuery.Append("ModifiedDate date, \n");
            tablesQuery.Append("constraint  FK_ValidateOwner\n");
            tablesQuery.Append("Foreign key ([Owner]) references EmployeeLeaveRegister(EmployeeId), \n");
            tablesQuery.Append(");\n");
            tablesQuery.Append("create table NewsAndUpdates\n");
            tablesQuery.Append("(\n");
            tablesQuery.Append("ID int primary key identity,\n");
            tablesQuery.Append("NewsID varchar(15) null,\n");
            tablesQuery.Append("[Date] date not null default SysDateTime(),\n");
            tablesQuery.Append("Title varchar(50) not null,\n");
            tablesQuery.Append("[Description] varchar(150) not null,\n");
            tablesQuery.Append("[Owner] varchar(10) not null,\n");
            tablesQuery.Append("ModifiedBy varchar(10),\n");
            tablesQuery.Append("ModifiedDate date,\n");
            tablesQuery.Append("constraint  FK_ValidateNewsOwner\n");
            tablesQuery.Append("Foreign key ([Owner]) references EmployeeLeaveRegister(EmployeeId), \n");
            tablesQuery.Append(");\n");
            tablesQuery.Append("create table Updates\n");
            tablesQuery.Append("(\n");
            tablesQuery.Append("ID int primary key identity,\n");
            tablesQuery.Append("LeaveAppliedBy varchar(10),\n");
            tablesQuery.Append("LeaveAppliedType varchar(30),\n");
            tablesQuery.Append("LeaveAppliedFrom date,\n");
            tablesQuery.Append("LeaveAppliedTo date,\n");
            tablesQuery.Append("LeaveStatus varchar(30),\n");
            tablesQuery.Append("LeaveApplied date default getdate(),\n");
            tablesQuery.Append("LeaveApproved date,\n");
            tablesQuery.Append("TotalDays int not null,\n");
            tablesQuery.Append("constraint  FK_ValidateUpdatesOwner\n");
            tablesQuery.Append("Foreign key (LeaveAppliedBy) references EmployeeLeaveRegister(EmployeeId), \n");
            tablesQuery.Append(");\n");
            return tablesQuery.ToString();
        }
        /// <summary>
        /// Prepare In Time Attendance status sql function query
        /// </summary>
        /// <returns></returns>
        public static string PrepareSqlInTimeAttendancestatusFunctionsQueries()
        {
            var sqlInTimeAttendancestatusFuntionQuery = new StringBuilder();
            sqlInTimeAttendancestatusFuntionQuery.Append("create function FN_InTimeAttendancestatus(@EmployeeID varchar(10))\n");
            sqlInTimeAttendancestatusFuntionQuery.Append("returns int\n");
            sqlInTimeAttendancestatusFuntionQuery.Append("As\n");
            sqlInTimeAttendancestatusFuntionQuery.Append("begin\n");
            sqlInTimeAttendancestatusFuntionQuery.Append("DECLARE @Status int\n");
            sqlInTimeAttendancestatusFuntionQuery.Append("select @Status=count(*) from AttendanceSheet\n");
            sqlInTimeAttendancestatusFuntionQuery.Append("where EmployeeId=@EmployeeID AND [Date]= convert(varchar(10),getdate(),10) AND InTime is not null\n");
            sqlInTimeAttendancestatusFuntionQuery.Append("return @Status;\n");
            sqlInTimeAttendancestatusFuntionQuery.Append("end\n");
            return sqlInTimeAttendancestatusFuntionQuery.ToString();
        }
        /// <summary>
        /// Prepare Lunch Time Attendance status sql function query
        /// </summary>
        /// <returns></returns>
        public static string PrepareSqlLunchTimeAttendancestatusFunctionsQueries()
        {
            var sqlLunchTimeAttendancestatusFuntionQuery = new StringBuilder();
            sqlLunchTimeAttendancestatusFuntionQuery.Append("create function FN_LunchTimeAttendancestatus(@EmployeeID varchar(10))\n");
            sqlLunchTimeAttendancestatusFuntionQuery.Append("returns int\n");
            sqlLunchTimeAttendancestatusFuntionQuery.Append("As\n");
            sqlLunchTimeAttendancestatusFuntionQuery.Append("begin\n");
            sqlLunchTimeAttendancestatusFuntionQuery.Append("DECLARE @Status int\n");
            sqlLunchTimeAttendancestatusFuntionQuery.Append("select @Status=count(*) from AttendanceSheet\n");
            sqlLunchTimeAttendancestatusFuntionQuery.Append("where EmployeeId=@EmployeeID AND [Date]= convert(varchar(10),getdate(),10) AND LunchTime is not null\n");
            sqlLunchTimeAttendancestatusFuntionQuery.Append("return @Status;\n");
            sqlLunchTimeAttendancestatusFuntionQuery.Append("end\n");
            return sqlLunchTimeAttendancestatusFuntionQuery.ToString();
        }
        /// <summary>
        /// Prepare Out Time Attendance status sql function query
        /// </summary>
        /// <returns></returns>
        public static string PrepareSqlOutTimeAttendancestatusFunctionsQueries()
        {

            var sqlOutTimeAttendancestatusFuntionQuery = new StringBuilder();
            sqlOutTimeAttendancestatusFuntionQuery.Append("create function FN_OutTimeAttendancestatus(@EmployeeID varchar(10))\n");
            sqlOutTimeAttendancestatusFuntionQuery.Append("returns int\n");
            sqlOutTimeAttendancestatusFuntionQuery.Append("As\n");
            sqlOutTimeAttendancestatusFuntionQuery.Append("begin\n");
            sqlOutTimeAttendancestatusFuntionQuery.Append("DECLARE @Status int\n");
            sqlOutTimeAttendancestatusFuntionQuery.Append("select @Status=count(*) from AttendanceSheet\n");
            sqlOutTimeAttendancestatusFuntionQuery.Append("where EmployeeId=@EmployeeID AND [Date]= convert(varchar(10),getdate(),10) AND OutTime is not null\n");
            sqlOutTimeAttendancestatusFuntionQuery.Append("return @Status;\n");
            sqlOutTimeAttendancestatusFuntionQuery.Append("end\n");
            sqlOutTimeAttendancestatusFuntionQuery.Append(" \n");
            return sqlOutTimeAttendancestatusFuntionQuery.ToString();
        }
        /// <summary>
        /// Prepare store procedure query for ActionOnMoreThanOneDayLeaveByAdmin
        /// </summary>
        /// <returns></returns>
        private static string PrepareSpQueryActionOnMoreThanOneDayLeaveByAdmin()
        {
            var actionOnMoreThanOneDayLeaveByAdminQuery = new StringBuilder();
            actionOnMoreThanOneDayLeaveByAdminQuery.Append("create proc SP_ActionOnMoreThanOneDayLeaveByAdmin \n");
            actionOnMoreThanOneDayLeaveByAdminQuery.Append("@EmployeeID varchar(10),\n");
            actionOnMoreThanOneDayLeaveByAdminQuery.Append("@FromDate date,\n");
            actionOnMoreThanOneDayLeaveByAdminQuery.Append("@ToDate date,\n");
            actionOnMoreThanOneDayLeaveByAdminQuery.Append("@LeaveStatus varchar(20)\n");
            actionOnMoreThanOneDayLeaveByAdminQuery.Append("As\n");
            actionOnMoreThanOneDayLeaveByAdminQuery.Append("Begin\n");
            actionOnMoreThanOneDayLeaveByAdminQuery.Append("update Updates\n");
            actionOnMoreThanOneDayLeaveByAdminQuery.Append("set LeaveStatus=@LeaveStatus, LeaveApproved=GETDATE()\n");
            actionOnMoreThanOneDayLeaveByAdminQuery.Append("where LeaveAppliedBy=@EmployeeID AND Convert(varchar(10),LeaveappliedFrom,10)=@FromDate\n");
            actionOnMoreThanOneDayLeaveByAdminQuery.Append("AND Convert(varchar(10),LeaveAppliedTo,10)=@ToDate;\n");
            actionOnMoreThanOneDayLeaveByAdminQuery.Append("End \n");
            return actionOnMoreThanOneDayLeaveByAdminQuery.ToString();
        }
        /// <summary>
        /// Prepare store procedure query for ActionOnOneDayLeaveByAdmin
        /// </summary>
        /// <returns></returns>
        private static string PrepareSpQueryActionOnOneDayLeaveByAdmin()
        {
            var actionOnOneDayLeaveByAdminQuery = new StringBuilder();
            actionOnOneDayLeaveByAdminQuery.Append(" create proc SP_ActionOnOneDayLeaveByAdmin\n");
            actionOnOneDayLeaveByAdminQuery.Append("@ID int,\n");
            actionOnOneDayLeaveByAdminQuery.Append("@LeaveStatus varchar(20)\n");
            actionOnOneDayLeaveByAdminQuery.Append("As\n");
            actionOnOneDayLeaveByAdminQuery.Append("Begin\n");
            actionOnOneDayLeaveByAdminQuery.Append("update Updates\n");
            actionOnOneDayLeaveByAdminQuery.Append("set LeaveStatus=@LeaveStatus, LeaveApproved=GETDATE()\n");
            actionOnOneDayLeaveByAdminQuery.Append("where ID=@ID\n");
            actionOnOneDayLeaveByAdminQuery.Append("End \n");
            return actionOnOneDayLeaveByAdminQuery.ToString();
        }
        /// <summary>
        /// Prepare store procedure query for AddLeave
        /// </summary>
        /// <returns></returns>
        private static string PrepareSpAddLeaveQuery()
        {
            var addLeaveQuery = new StringBuilder();
            addLeaveQuery.Append("create proc SP_AddLeave\n");
            addLeaveQuery.Append("@Date date,\n");
            addLeaveQuery.Append("@Day char(20),\n");
            addLeaveQuery.Append("@ReasonDescription varchar(150),\n");
            addLeaveQuery.Append("@EmployeeID varchar(10)\n");
            addLeaveQuery.Append("As\n");
            addLeaveQuery.Append("Begin\n");
            addLeaveQuery.Append("insert into CompanyLeaveRegistration([Date],[Day],ReasonDescription,LeaveRegisteredBy,LeaveRegisteredDate)\n");
            addLeaveQuery.Append("values(@Date,@Day,@ReasonDescription,@EmployeeID,GETDATE())\n");
            addLeaveQuery.Append("End\n");
            return addLeaveQuery.ToString();
        }
        /// <summary>
        /// Prepare store procedure query for AddNews
        /// </summary>
        /// <returns></returns>
        private static string PrepareSpAddNewsQuery()
        {
            var addNewsQuery = new StringBuilder();
            addNewsQuery.Append(" create proc SP_AddNews\n");
            addNewsQuery.Append(" @EmployeeID varchar(10),\n");
            addNewsQuery.Append("@Title varchar(50),\n");
            addNewsQuery.Append(" @Description varchar(150)\n");
            addNewsQuery.Append("As\n");
            addNewsQuery.Append("Begin\n");
            addNewsQuery.Append("insert into NewsAndUpdates([Date],Title,[Description],[Owner])\n");
            addNewsQuery.Append("values(GETDATE(),@Title,@Description,@EmployeeID)\n");
            addNewsQuery.Append("End\n");
            return addNewsQuery.ToString();
        }
        /// <summary>
        /// Prepare store procedure query for ApplyLeaveQuery
        /// </summary>
        /// <returns></returns>
        private static string PrepareSpApplyLeaveQuery()
        {
            var addApplyQuery = new StringBuilder();
            addApplyQuery.Append("create procedure SP_ApplyLeave\n");
            addApplyQuery.Append("@EmployeeID varchar(10),\n");
            addApplyQuery.Append("@FromDate date,\n");
            addApplyQuery.Append("@ToDate date,\n");
            addApplyQuery.Append("@TypeofLeave varchar(20),\n");
            addApplyQuery.Append("@TotalDays int\n");
            addApplyQuery.Append("As\n");
            addApplyQuery.Append("Begin\n");
            addApplyQuery.Append("insert into Updates (LeaveAppliedBy,LeaveAppliedType,LeaveAppliedFrom,LeaveAppliedTo,LeaveStatus,TotalDays)\n");
            addApplyQuery.Append("values(@EmployeeID,@TypeofLeave,@FromDate,@ToDate,'Waiting admin response',@TotalDays);\n");
            addApplyQuery.Append("End\n");
            return addApplyQuery.ToString();
        }
        /// <summary>
        /// Prepare store procedure query for ChangeAttendanceStatus
        /// </summary>
        /// <returns></returns>
        private static string PrepareSpChangeAttendanceStatusQuery()
        {
            var changeAttendanceStatusQuery = new StringBuilder();
            changeAttendanceStatusQuery.Append("create proc SP_ChangeAttendanceStatus\n");
            changeAttendanceStatusQuery.Append("@EmployeeID varchar(10),\n");
            changeAttendanceStatusQuery.Append("@AttendanceStatus varchar(20),\n");
            changeAttendanceStatusQuery.Append("@SelecteDDate date\n");
            changeAttendanceStatusQuery.Append("As\n");
            changeAttendanceStatusQuery.Append("Begin\n");
            changeAttendanceStatusQuery.Append("update AttendanceSheet\n");
            changeAttendanceStatusQuery.Append("set AttendanceStatus=@AttendanceStatus\n");
            changeAttendanceStatusQuery.Append("where EmployeeId= @EmployeeID AND CONVERT(varchar(10),[Date],10)=@SelecteDDate \n");
            changeAttendanceStatusQuery.Append("End\n");
            return changeAttendanceStatusQuery.ToString();
        }
        /// <summary>
        /// Prepare store procedure query for ChangePassword
        /// </summary>
        /// <returns></returns>
        private static string PrepareSpChangePasswordQuery()
        {
            var changePasswordQuery = new StringBuilder();
            changePasswordQuery.Append("create proc SP_ChangePassword\n");
            changePasswordQuery.Append("@Password varchar(50),\n");
            changePasswordQuery.Append("@Repassword varchar(50),\n");
            changePasswordQuery.Append("@EmployeeID varchar(10)\n");
            changePasswordQuery.Append("As\n");
            changePasswordQuery.Append("begin\n");
            changePasswordQuery.Append("update  EmployeeLeaveRegister\n");
            changePasswordQuery.Append("set [Password]=@Password, RePassword=@Repassword\n");
            changePasswordQuery.Append("where EmployeeId=@EmployeeID\n");
            changePasswordQuery.Append("end\n");
            return changePasswordQuery.ToString();
        }
        /// <summary>
        /// Prepare store procedure query for DeleteAddedLeave
        /// </summary>
        /// <returns></returns>
        private static string PrepareSpDeleteAddedLeaveQuery()
        {
            var deleteAddedLeaveQuery = new StringBuilder();
            deleteAddedLeaveQuery.Append("create proc SP_DeleteAddedLeave\n");
            deleteAddedLeaveQuery.Append("@Id int\n");
            deleteAddedLeaveQuery.Append("As\n");
            deleteAddedLeaveQuery.Append("Begin\n");
            deleteAddedLeaveQuery.Append("delete CompanyLeaveRegistration\n");
            deleteAddedLeaveQuery.Append("where ID=@Id\n");
            deleteAddedLeaveQuery.Append("End\n");
            return deleteAddedLeaveQuery.ToString();
        }
        /// <summary>
        /// Prepare store procedure query for EditEmialSetup
        /// </summary>
        /// <returns></returns>
        private static string PrepareEditEmailSetupQuery()
        {
            var editEmialSetupQuery = new StringBuilder();
            editEmialSetupQuery.Append("create proc SP_EditEmailSetup\n");
            editEmialSetupQuery.Append("@EmailID varchar(30),\n");
            editEmialSetupQuery.Append("@Password varchar(30),\n");
            editEmialSetupQuery.Append("@ConfirmPassword varchar(30),\n");
            editEmialSetupQuery.Append("@SeetingFirst varchar(50),\n");
            editEmialSetupQuery.Append("@Seetingsecond varchar(50),\n");
            editEmialSetupQuery.Append("@SeetingThird varchar(50),\n");
            editEmialSetupQuery.Append("@SeetingFourth varchar(50)\n");
            editEmialSetupQuery.Append("As\n");
            editEmialSetupQuery.Append("Begin\n");
            editEmialSetupQuery.Append("update EmailSetup\n");
            editEmialSetupQuery.Append("set EmailId =@EmailID,Password =@Password,ConfirmPassword=@ConfirmPassword,\n");
            editEmialSetupQuery.Append("SeetingFirst=@SeetingFirst,SeetingSecond=@Seetingsecond,SeetingThird=@SeetingThird,\n");
            editEmialSetupQuery.Append("SeetingFourth=@SeetingFourth\n");
            editEmialSetupQuery.Append("where ID=1\n");
            editEmialSetupQuery.Append("End\n");
            return editEmialSetupQuery.ToString();
        }
        /// <summary>
        /// Prepare store procedure query for EditProfileImage
        /// </summary>
        /// <returns></returns>
        private static string PrepareEditProfileImageQuery()
        {
            var editProfileImageQuery = new StringBuilder();
            editProfileImageQuery.Append("create proc SP_EditProfileImage\n");
            editProfileImageQuery.Append("@EmployeeID varchar(10),\n");
            editProfileImageQuery.Append("@ProfilePicture binary\n");
            editProfileImageQuery.Append("As\n");
            editProfileImageQuery.Append("Begin\n");
            editProfileImageQuery.Append("update EmployeeLeaveRegister\n");
            editProfileImageQuery.Append("set ProfilePicture=@ProfilePicture\n");
            editProfileImageQuery.Append("where EmployeeId=@EmployeeID\n");
            editProfileImageQuery.Append("End\n");
            return editProfileImageQuery.ToString();
        }
        /// <summary>
        /// Prepare store procedure query for EmailSetup
        /// </summary>
        /// <returns></returns>
        private static string PrepareEmailSetupQuery()
        {
            var emailSetupQuery = new StringBuilder();
            emailSetupQuery.Append("create proc SP_EmailSetup\n");
            emailSetupQuery.Append("@EmailID varchar(30),\n");
            emailSetupQuery.Append("@Password varchar(30),\n");
            emailSetupQuery.Append("@ConfirmPassword varchar(30),\n");
            emailSetupQuery.Append("@SeetingFirst varchar(50),\n");
            emailSetupQuery.Append("@Seetingsecond varchar(50),\n");
            emailSetupQuery.Append("@SeetingThird varchar(50),\n");
            emailSetupQuery.Append("@SeetingFourth varchar(50)\n");
            emailSetupQuery.Append("As\n");
            emailSetupQuery.Append("Begin\n");
            emailSetupQuery.Append("insert into EmailSetup(EmailId,Password,ConfirmPassword,SeetingFirst,SeetingSecond,SeetingThird,SeetingFourth)\n");
            emailSetupQuery.Append("values(@EmailID,@Password,@ConfirmPassword,@SeetingFirst,@Seetingsecond,@SeetingThird,@SeetingFourth)\n");
            emailSetupQuery.Append("End");
            return emailSetupQuery.ToString();
        }
        /// <summary>
        /// Prepare store procedure query for EmployeeList
        /// </summary>
        /// <returns></returns>
        private static string PrepareEmployeeListQuery()
        {
            var employeeListQuery = new StringBuilder();
            employeeListQuery.Append("create proc SP_EmployeeList\n");
            employeeListQuery.Append("As\n");
            employeeListQuery.Append("Begin\n");
            employeeListQuery.Append("select FirstName,EmployeeId,EmployeeEmailID from EmployeeLeaveRegister\n");
            employeeListQuery.Append("End");
            return employeeListQuery.ToString();
        }
        /// <summary>
        /// Prepare store procedure query for EmployeeRegistrationByAdmin
        /// </summary>
        /// <returns></returns>
        private static string PrepareEmployeeRegistrationByAdminQuery()
        {
            var employeeRegistrationByAdminQuery = new StringBuilder();
            employeeRegistrationByAdminQuery.Append("create  procedure SP_EmployeeRegistrationByAdmin\n");
            employeeRegistrationByAdminQuery.Append("@EmployeeID varchar(10),\n");
            employeeRegistrationByAdminQuery.Append("@EmailId varchar(50),\n");
            employeeRegistrationByAdminQuery.Append("@FirstName varchar(30),\n");
            employeeRegistrationByAdminQuery.Append("@LastName varchar(30),\n");
            employeeRegistrationByAdminQuery.Append("@Designation varchar(50),\n");
            employeeRegistrationByAdminQuery.Append("@DateofJoining date,\n");
            employeeRegistrationByAdminQuery.Append("@Company varchar(50)\n");
            employeeRegistrationByAdminQuery.Append("As\n");
            employeeRegistrationByAdminQuery.Append("begin\n");
            employeeRegistrationByAdminQuery.Append("insert into EmployeeLeaveRegister(EmployeeId,EmployeeEmailID,FirstName,LastName,DateOfJoining,Designation,Company,IsEmployeeAdmin)\n");
            employeeRegistrationByAdminQuery.Append("values(@EmployeeID,@EmailId,@FirstName,@LastName,@DateofJoining,@Designation,@Company,0)\n");
            employeeRegistrationByAdminQuery.Append("end");
            return employeeRegistrationByAdminQuery.ToString();
        }
        /// <summary>
        /// Prepare store procedure query for EmployeeRegistrationBySuperAdmin
        /// </summary>
        /// <returns></returns>
        private static string PrepareEmployeeRegistrationBySuperAdminQuery()
        {
            var employeeRegistrationBySuperAdminQuery = new StringBuilder();
            employeeRegistrationBySuperAdminQuery.Append("create  procedure SP_EmployeeRegistrationBySuperAdmin\n");
            employeeRegistrationBySuperAdminQuery.Append("@EmployeeID varchar(10),\n");
            employeeRegistrationBySuperAdminQuery.Append("@EmailId varchar(50),\n");
            employeeRegistrationBySuperAdminQuery.Append("@Password varchar(50),\n");
            employeeRegistrationBySuperAdminQuery.Append("@FirstName varchar(30),\n");
            employeeRegistrationBySuperAdminQuery.Append("@LastName varchar(30),\n");
            employeeRegistrationBySuperAdminQuery.Append("@DOJ date,\n");
            employeeRegistrationBySuperAdminQuery.Append("@Desgnation varchar(30),\n");
            employeeRegistrationBySuperAdminQuery.Append("@Company varchar(40),\n");
            employeeRegistrationBySuperAdminQuery.Append("@ReEnterPassword varchar(50)\n");
            employeeRegistrationBySuperAdminQuery.Append("As\n");
            employeeRegistrationBySuperAdminQuery.Append("begin\n");
            employeeRegistrationBySuperAdminQuery.Append("insert into EmployeeLeaveRegister(EmployeeId,EmployeeEmailID,FirstName,LastName,DateOfJoining,Designation,Company,[Password],RePassword,IsEmployeeAdmin)\n");
            employeeRegistrationBySuperAdminQuery.Append("values(@EmployeeID,@EmailId,@FirstName,@LastName,@DOJ,@Desgnation,@Company,@Password,@ReEnterPassword,1)\n");
            employeeRegistrationBySuperAdminQuery.Append("end");
            return employeeRegistrationBySuperAdminQuery.ToString();
        }
        /// <summary>
        /// Prepare store procedure query for FillInTimeAttendance
        /// </summary>
        /// <returns></returns>
        private static string PrepareFillInTimeAttendanceQuery()
        {
            var fillllInTimeAttendanceQuery = new StringBuilder();
            fillllInTimeAttendanceQuery.Append("create proc SP_FillInTimeAttendance\n");
            fillllInTimeAttendanceQuery.Append("@EmployeeID varchar(10) \n");
            fillllInTimeAttendanceQuery.Append("As\n");
            fillllInTimeAttendanceQuery.Append("Begin\n");
            fillllInTimeAttendanceQuery.Append("insert into AttendanceSheet(EmployeeId,Date,Day,InTime,AttendanceStatus)\n");
            fillllInTimeAttendanceQuery.Append("values(@EmployeeID,CONVERT(VARCHAR(10),GETDATE(),10),datename(dw,getdate())," +
                                               "CONVERT(varchar(15),GetDate(),14),'Present')\n");
            fillllInTimeAttendanceQuery.Append("End");
            return fillllInTimeAttendanceQuery.ToString();
        }
        /// <summary>
        /// Prepare store procedure query for FillLunchTimeAttendance
        /// </summary>
        /// <returns></returns>
        private static string PrepareFillLunchTimeAttendanceQuery()
        {
            var fillLunchTimeAttendanceQuery = new StringBuilder();
            fillLunchTimeAttendanceQuery.Append("create proc SP_FillLunchTimeAttendance\n");
            fillLunchTimeAttendanceQuery.Append("@EmployeeID varchar(10)\n");
            fillLunchTimeAttendanceQuery.Append("As\n");
            fillLunchTimeAttendanceQuery.Append("Begin\n");
            fillLunchTimeAttendanceQuery.Append("update AttendanceSheet\n");
            fillLunchTimeAttendanceQuery.Append("set LunchTime = CONVERT(VARCHAR(10),GETDATE(),14)\n");
            fillLunchTimeAttendanceQuery.Append("where EmployeeId=@EmployeeID AND [Date] =CONVERT(VARCHAR(10),GETDATE(),10)\n");
            fillLunchTimeAttendanceQuery.Append("End");
            return fillLunchTimeAttendanceQuery.ToString();
        }
        /// <summary>
        /// Prepare store procedure query for FillOutTimeAttendance
        /// </summary>
        /// <returns></returns>
        private static string PrepareFillOutTimeAttendanceQuery()
        {
            var fillOutTimeAttendanceQuery = new StringBuilder();
            fillOutTimeAttendanceQuery.Append("create proc SP_FillOutTimeAttendance\n");
            fillOutTimeAttendanceQuery.Append("@EmployeeID varchar(10)\n");
            fillOutTimeAttendanceQuery.Append("As\n");
            fillOutTimeAttendanceQuery.Append("Begin\n");
            fillOutTimeAttendanceQuery.Append("update AttendanceSheet\n");
            fillOutTimeAttendanceQuery.Append("set OutTime = CONVERT(VARCHAR(10),GETDATE(),14)\n");
            fillOutTimeAttendanceQuery.Append("where EmployeeId=@EmployeeID AND [Date] =CONVERT(VARCHAR(10),GETDATE(),10);\n");
            fillOutTimeAttendanceQuery.Append("update AttendanceSheet\n");
            fillOutTimeAttendanceQuery.Append("set TotalWorked=DATEDIFF(HOUR,OutTime,InTime)\n");
            fillOutTimeAttendanceQuery.Append("where EmployeeId=@EmployeeID AND [Date] =CONVERT(VARCHAR(10),GETDATE(),10)\n");
            fillOutTimeAttendanceQuery.Append("End");
            return fillOutTimeAttendanceQuery.ToString();
        }
        /// <summary>
        /// Prepare store procedure query for FillOutTimeAttendance
        /// </summary>
        /// <returns></returns>
        private static string PrepareGetAtteendaceStatusQuery()
        {
            var getAtteendaceStatusQuery = new StringBuilder();
            getAtteendaceStatusQuery.Append("create proc SP_GetAtteendaceStatus\n");
            getAtteendaceStatusQuery.Append("@EmployeeID varchar(10),\n");
            getAtteendaceStatusQuery.Append("@SelecteDDate date\n");
            getAtteendaceStatusQuery.Append("As\n");
            getAtteendaceStatusQuery.Append("Begin\n");
            getAtteendaceStatusQuery.Append("select TotalWorked,AttendanceStatus from AttendanceSheet\n");
            getAtteendaceStatusQuery.Append("where EmployeeId= @EmployeeID AND CONVERT(varchar(10),[Date],10)=@SelecteDDate\n");
            getAtteendaceStatusQuery.Append("End");
            return getAtteendaceStatusQuery.ToString();
        }
        /// <summary>
        /// Prepare store procedure query for GetEmialSetupInfo
        /// </summary>
        /// <returns></returns>
        private static string PrepareGetEmialSetupInfoQuery()
        {
            var getEmialSetupInfoQuery = new StringBuilder();
            getEmialSetupInfoQuery.Append("create proc SP_GetEmialSetupInfo\n");
            getEmialSetupInfoQuery.Append("As\n");
            getEmialSetupInfoQuery.Append("Begin\n");
            getEmialSetupInfoQuery.Append("select * from EmailSetup\n");
            getEmialSetupInfoQuery.Append("End");
            return getEmialSetupInfoQuery.ToString();
        }
        /// <summary>
        /// Prepare store procedure query for GetLeaveAcceptRejectDetails
        /// </summary>
        /// <returns></returns>
        private static string PrepareGetLeaveAcceptRejectDetailsQuery()
        {
            var getLeaveAcceptRejectDetailsQuery = new StringBuilder();
            getLeaveAcceptRejectDetailsQuery.Append("create proc SP_GetLeaveAcceptRejectDetails\n");
            getLeaveAcceptRejectDetailsQuery.Append("As\n");
            getLeaveAcceptRejectDetailsQuery.Append("Begin\n");
            getLeaveAcceptRejectDetailsQuery.Append("select Updates.LeaveAppliedBy As EmployeeID,\n");
            getLeaveAcceptRejectDetailsQuery.Append("(EmployeeLeaveRegister.FirstName+EmployeeLeaveRegister.LastName) As Name,\n");
            getLeaveAcceptRejectDetailsQuery.Append("Updates.LeaveappliedFrom as FromDate, Updates.LeaveAppliedTo as ToDate,\n");
            getLeaveAcceptRejectDetailsQuery.Append("Updates.LeaveAppliedType as LeaveType,\n");
            getLeaveAcceptRejectDetailsQuery.Append("Updates.LeaveApproved as ApprovedDate,Updates.LeaveStatus,\n");
            getLeaveAcceptRejectDetailsQuery.Append("(DATEDIFF(dd, LeaveappliedFrom, LeaveAppliedTo) + 1)\n");
            getLeaveAcceptRejectDetailsQuery.Append("-(DATEDIFF(wk, LeaveappliedFrom, LeaveAppliedTo) * 2)\n");
            getLeaveAcceptRejectDetailsQuery.Append("-(CASE WHEN DATENAME(dw, LeaveappliedFrom) = 'Sunday' THEN 1 ELSE 0 END)\n");
            getLeaveAcceptRejectDetailsQuery.Append("-(CASE WHEN DATENAME(dw, LeaveAppliedTo) = 'Saturday' THEN 1 ELSE 0 END) as TotalDaysTakenLeave\n");
            getLeaveAcceptRejectDetailsQuery.Append("from Updates\n");
            getLeaveAcceptRejectDetailsQuery.Append("left join EmployeeLeaveRegister\n");
            getLeaveAcceptRejectDetailsQuery.Append("on Updates.LeaveAppliedBy=EmployeeLeaveRegister.EmployeeId\n");
            getLeaveAcceptRejectDetailsQuery.Append("End");
            return getLeaveAcceptRejectDetailsQuery.ToString();
        }
        /// <summary>
        /// Prepare store procedure query for GetLeaveList
        /// </summary>
        /// <returns></returns>
        private static string PrepareGetLeaveListQuery()
        {
            var getLeaveListQuery = new StringBuilder();
            getLeaveListQuery.Append("create proc SP_GetLeaveList\n");
            getLeaveListQuery.Append("As\n");
            getLeaveListQuery.Append("Begin\n");
            getLeaveListQuery.Append("select [Date],[Day],ReasonDescription from CompanyLeaveRegistration\n");
            getLeaveListQuery.Append("End");
            return getLeaveListQuery.ToString();
        }
        /// <summary>
        /// Prepare store procedure query for GetLeavesAndWorkingHours
        /// </summary>
        /// <returns></returns>
        private static string PrepareGetLeavesAndWorkingHoursQuery()
        {
            var getLeavesAndWorkingHoursQuery = new StringBuilder();
            getLeavesAndWorkingHoursQuery.Append("create proc SP_GetLeavesAndWorkingHours\n");
            getLeavesAndWorkingHoursQuery.Append("As\n");
            getLeavesAndWorkingHoursQuery.Append("Begin\n");
            getLeavesAndWorkingHoursQuery.Append("select TotalCasualLeave,ToatlSickLeave,TotalWorkingHours from LeaveConfiguration\n");
            getLeavesAndWorkingHoursQuery.Append("where ID=1\n");
            getLeavesAndWorkingHoursQuery.Append("End");
            return getLeavesAndWorkingHoursQuery.ToString();
        }
        /// <summary>
        /// Prepare store procedure query for GetNews
        /// </summary>
        /// <returns></returns>
        private static string PrepareGetNewsQuery()
        {
            var getNewsQuery = new StringBuilder();
            getNewsQuery.Append("create proc SP_GetNews\n");
            getNewsQuery.Append("As\n");
            getNewsQuery.Append("Begin\n");
            getNewsQuery.Append("select ID, [Date],Title,[Description] from NewsAndUpdates\n");
            getNewsQuery.Append("end");
            return getNewsQuery.ToString();
        }
        /// <summary>
        /// Prepare store procedure query for GetOwnAttendanceSheet
        /// </summary>
        /// <returns></returns>
        private static string PrepareGetOwnAttendanceSheetQuery()
        {
            var getOwnAttendanceSheetQuery = new StringBuilder();
            getOwnAttendanceSheetQuery.Append("create proc SP_GetOwnAttendanceSheet\n");
            getOwnAttendanceSheetQuery.Append("@EmployeeID varchar(10),\n");
            getOwnAttendanceSheetQuery.Append("@Month varchar(15)\n");
            getOwnAttendanceSheetQuery.Append("As\n");
            getOwnAttendanceSheetQuery.Append("Begin\n");
            getOwnAttendanceSheetQuery.Append("select Date,Day,InTime,LunchTime,OutTime,TotalWorked,AttendanceStatus from AttendanceSheet\n");
            getOwnAttendanceSheetQuery.Append("where EmployeeId=@EmployeeID AND DATENAME(month,[Date])=@Month\n");
            getOwnAttendanceSheetQuery.Append("End");
            return getOwnAttendanceSheetQuery.ToString();
        }
        /// <summary>
        /// Prepare store procedure query for GetPassword
        /// </summary>
        /// <returns></returns>
        private static string PrepareGetPasswordQuery()
        {
            var getPasswordQuery = new StringBuilder();
            getPasswordQuery.Append("create proc SP_GetPassword\n");
            getPasswordQuery.Append("@EmailID varchar(50)\n");
            getPasswordQuery.Append("As\n");
            getPasswordQuery.Append("begin\n");
            getPasswordQuery.Append("select [Password] from EmployeeLeaveRegister\n");
            getPasswordQuery.Append("where EmployeeEmailID=@EmailID\n");
            getPasswordQuery.Append("end\n");
            return getPasswordQuery.ToString();
        }
        /// <summary>
        /// Prepare store procedure query for GetProfileInfo
        /// </summary>
        /// <returns></returns>
        private static string PrepareGetProfileInfoQuery()
        {
            var getProfileInfoQuery = new StringBuilder();
            getProfileInfoQuery.Append("create proc SP_GetProfileInfo\n");
            getProfileInfoQuery.Append("@EmployeeId varchar(10)\n");
            getProfileInfoQuery.Append("As\n");
            getProfileInfoQuery.Append("begin\n");
            getProfileInfoQuery.Append("select EmployeeId,EmployeeEmailID,FirstName, LastName," +
                                       "DateOfBirth,Designation,ProfilePicture,Company,DateOfJoining from EmployeeLeaveRegister\n");
            getProfileInfoQuery.Append("where EmployeeId=@EmployeeId\n");
            getProfileInfoQuery.Append("end");
            return getProfileInfoQuery.ToString();
        }
        /// <summary>
        /// Prepare store procedure query for GetTotalTakenLeaveStatus
        /// </summary>
        /// <returns></returns>
        private static string PrepareGetTotalTakenLeaveStatusQuery()
        {
            var getTotalTakenLeaveStatusQuery = new StringBuilder();
            getTotalTakenLeaveStatusQuery.Append("create proc SP_GetTotalTakenLeaveStatus\n");
            getTotalTakenLeaveStatusQuery.Append("@EmployeeID varchar(10),\n");
            getTotalTakenLeaveStatusQuery.Append("@LeaveType varchar(20)\n");
            getTotalTakenLeaveStatusQuery.Append("As\n");
            getTotalTakenLeaveStatusQuery.Append("Begin\n");
            getTotalTakenLeaveStatusQuery.Append("select SUM(TotalDays)as TotalDaysTakenLeave  from Updates\n");
            getTotalTakenLeaveStatusQuery.Append("where LeaveAppliedBy=@EmployeeID AND LeaveAppliedType=@LeaveType AND LeaveStatus='Approved'\n");
            getTotalTakenLeaveStatusQuery.Append("End");
            return getTotalTakenLeaveStatusQuery.ToString();
        }
        /// <summary>
        /// Prepare store procedure query for GetTotssalTakenLeaveStatus
        /// </summary>
        /// <returns></returns>
        private static string PrepareGetUpdateAdminQuery()
        {
            var getTotssalTakenLeaveStatusQuery = new StringBuilder();
            getTotssalTakenLeaveStatusQuery.Append("create proc SP_GetUpdateAdmin\n");
            getTotssalTakenLeaveStatusQuery.Append("As\n");
            getTotssalTakenLeaveStatusQuery.Append("Begin\n");
            getTotssalTakenLeaveStatusQuery.Append("select (EmployeeLeaveRegister.FirstName+EmployeeLeaveRegister.LastName) As " +
                                                   "Name, Updates.LeaveAppliedBy As EmployeeID,\n");
            getTotssalTakenLeaveStatusQuery.Append(" Updates.ID as ID,Updates.LeaveAppliedType as LeaveType, Updates.LeaveappliedFrom as FromDate, Updates.LeaveAppliedTo as ToDate\n");
            getTotssalTakenLeaveStatusQuery.Append("from Updates\n");
            getTotssalTakenLeaveStatusQuery.Append("left join EmployeeLeaveRegister\n");
            getTotssalTakenLeaveStatusQuery.Append("on Updates.LeaveAppliedBy=EmployeeLeaveRegister.EmployeeId\n");
            getTotssalTakenLeaveStatusQuery.Append("where Updates.LeaveStatus ='Waiting admin response'\n");
            getTotssalTakenLeaveStatusQuery.Append("End");
            return getTotssalTakenLeaveStatusQuery.ToString();
        }
        /// <summary>
        /// Prepare store procedure query for GetUpdates
        /// </summary>
        /// <returns></returns>
        private static string PrepareGetUpdatesQuery()
        {
            var getUpdatesQuery = new StringBuilder();
            getUpdatesQuery.Append("create proc SP_GetUpdates\n");
            getUpdatesQuery.Append("@EmployeeID varchar(10)\n");
            getUpdatesQuery.Append("As\n");
            getUpdatesQuery.Append("Begin\n");
            getUpdatesQuery.Append("select top 20 LeaveAppliedType,LeaveAppliedFrom,LeaveAppliedTo,LeaveStatus,LeaveApproved from Updates\n");
            getUpdatesQuery.Append("where LeaveAppliedBy=@EmployeeID order by LeaveappliedFrom desc\n");
            getUpdatesQuery.Append("End");
            return getUpdatesQuery.ToString();
        }
        /// <summary>
        /// Prepare store procedure query for GetWeekend
        /// </summary>
        /// <returns></returns>
        private static string PrepareGetWeekendQuery()
        {
            var getWeekendQuery = new StringBuilder();
            getWeekendQuery.Append("create proc SP_GetWeekend\n");
            getWeekendQuery.Append("As\n");
            getWeekendQuery.Append("Begin\n");
            getWeekendQuery.Append("select WeekEnd from LeaveConfiguration\n");
            getWeekendQuery.Append("where ID=1\n");
            getWeekendQuery.Append("End");
            return getWeekendQuery.ToString();
        }
        /// <summary>
        /// Prepare store procedure query for ProfileEdit
        /// </summary>
        /// <returns></returns>
        private static string PrepareProfileEditQuery()
        {
            var profileEditQuery = new StringBuilder();
            profileEditQuery.Append("create proc SP_ProfileEdit\n");
            profileEditQuery.Append("@DateOfBirth date,\n");
            profileEditQuery.Append("@EmployeeId varchar(10)\n");
            profileEditQuery.Append("As\n");
            profileEditQuery.Append("begin\n");
            profileEditQuery.Append("update  EmployeeLeaveRegister\n");
            profileEditQuery.Append("set DateOfBirth=@DateOfBirth\n");
            profileEditQuery.Append("where EmployeeId=@EmployeeId\n");
            profileEditQuery.Append("end");
            return profileEditQuery.ToString();
        }
        /// <summary>
        /// Prepare store procedure query for SaveProfileImage
        /// </summary>
        /// <returns></returns>
        private static string PrepareSaveProfileImageQuery()
        {
            var saveProfileImageQuery = new StringBuilder();
            saveProfileImageQuery.Append("create proc SP_SaveProfileImage\n");
            saveProfileImageQuery.Append("@profileImage binary,\n");
            saveProfileImageQuery.Append("@EmployeeId varchar(10)\n");
            saveProfileImageQuery.Append("As\n");
            saveProfileImageQuery.Append("begin\n");
            saveProfileImageQuery.Append("update EmployeeLeaveRegister\n");
            saveProfileImageQuery.Append("set ProfilePicture=@profileImage\n");
            saveProfileImageQuery.Append("where EmployeeId=@EmployeeId\n");
            saveProfileImageQuery.Append("end");
            return saveProfileImageQuery.ToString();
        }
        /// <summary>
        /// Prepare store procedure query for SetLeavesAndWorkingHours
        /// </summary>
        /// <returns></returns>
        private static string PrepareSetLeavesAndWorkingHoursQuery()
        {
            var setLeavesAndWorkingHoursQuery = new StringBuilder();
            setLeavesAndWorkingHoursQuery.Append("create proc SP_SetLeavesAndWorkingHours\n");
            setLeavesAndWorkingHoursQuery.Append("@TotalCasualLeave int,\n");
            setLeavesAndWorkingHoursQuery.Append("@TotalSickLeave int,\n");
            setLeavesAndWorkingHoursQuery.Append("@TotalWorkingHours int\n");
            setLeavesAndWorkingHoursQuery.Append("As\n");
            setLeavesAndWorkingHoursQuery.Append("Begin\n");
            setLeavesAndWorkingHoursQuery.Append("update LeaveConfiguration\n");
            setLeavesAndWorkingHoursQuery.Append("set TotalCasualLeave=@TotalCasualLeave,[ToatlSickLeave]=@TotalSickLeave,[TotalWorkingHours]=@TotalWorkingHours\n");
            setLeavesAndWorkingHoursQuery.Append("where ID=1\n");
            setLeavesAndWorkingHoursQuery.Append("End");
            return setLeavesAndWorkingHoursQuery.ToString();
        }
        /// <summary>
        /// Prepare store procedure query for SetWeekend
        /// </summary>
        /// <returns></returns>
        private static string PrepareSetWeekendQuery()
        {
            var setWeekendQuery = new StringBuilder();
            setWeekendQuery.Append("create proc SP_SetWeekend\n");
            setWeekendQuery.Append("@WeekEnd varchar(50)\n");
            setWeekendQuery.Append("As\n");
            setWeekendQuery.Append("Begin\n");
            setWeekendQuery.Append("update LeaveConfiguration\n");
            setWeekendQuery.Append("set WeekEnd=@WeekEnd\n");
            setWeekendQuery.Append("where ID=1\n");
            setWeekendQuery.Append("End");
            return setWeekendQuery.ToString();
        }
        /// <summary>
        /// Prepare store procedure query for UpdateNews
        /// </summary>
        /// <returns></returns>
        private static string PrepareUpdateNewsQuery()
        {
            var updateNewsQuery = new StringBuilder();
            updateNewsQuery.Append("create proc SP_UpdateNews\n");
            updateNewsQuery.Append("@EmployeeID varchar(15),\n");
            updateNewsQuery.Append("@Id int,\n");
            updateNewsQuery.Append("@Title varchar(50),\n");
            updateNewsQuery.Append("@Description varchar(150)\n");
            updateNewsQuery.Append("As\n");
            updateNewsQuery.Append("Begin\n");
            updateNewsQuery.Append("update NewsAndUpdates\n");
            updateNewsQuery.Append("set Title =@Title,[Description]=@Description,ModifiedBy=@EmployeeID, ModifiedDate=GETDATE()\n");
            updateNewsQuery.Append("where ID=@Id\n");
            updateNewsQuery.Append("end");
            return updateNewsQuery.ToString();
        }
        /// <summary>
        /// Prepare store procedure query for IsEmailIdExist
        /// </summary>
        /// <returns></returns>
        private static string PrepareIsEmailIdExistQuery()
        {

            var isEmailIdExistQuery = new StringBuilder();
            isEmailIdExistQuery.Append("create proc SP_IsEmailIdExist\n");
            isEmailIdExistQuery.Append("@EmailId varchar(30)\n");
            isEmailIdExistQuery.Append("As\n");
            isEmailIdExistQuery.Append("Begin\n");
            isEmailIdExistQuery.Append("select EmployeeEmailID from EmployeeLeaveRegister\n");
            isEmailIdExistQuery.Append("where EmployeeEmailID=@EmailId \n");
            isEmailIdExistQuery.Append("end");
            return isEmailIdExistQuery.ToString();
        }
        /// <summary>
        /// Prepare store procedure query for IsEmployeeIdIdExist
        /// </summary>
        /// <returns></returns>
        private static string PrepareIsEmployeeIdIdExistQuery()
        {
            var isEmployeeIdIdExistQuery = new StringBuilder();
            isEmployeeIdIdExistQuery.Append("create proc SP_IsEmployeeIdIdExist\n");
            isEmployeeIdIdExistQuery.Append("@EmployeeId varchar(15)\n");
            isEmployeeIdIdExistQuery.Append("As\n");
            isEmployeeIdIdExistQuery.Append("Begin\n");
            isEmployeeIdIdExistQuery.Append("select EmployeeId from EmployeeLeaveRegister\n");
            isEmployeeIdIdExistQuery.Append("where EmployeeId=@EmployeeId \n");
            isEmployeeIdIdExistQuery.Append("end");
            return isEmployeeIdIdExistQuery.ToString();
        }
        /// <summary>
        /// Prepare store procedure query for GetEmployeeIdQuery
        /// </summary>
        /// <returns></returns>
        public static string PrepareGetEmployeeIdQuery()  
        {
            var getEmployeeIdQuery = new StringBuilder();
            getEmployeeIdQuery.Append("create procedure SP_GetEmployeeID \n");
            getEmployeeIdQuery.Append("@EmailId varchar(50) \n");
            getEmployeeIdQuery.Append("As \n");
            getEmployeeIdQuery.Append("Begin \n");
            getEmployeeIdQuery.Append("select EmployeeId from EmployeeLeaveRegister\n");
            getEmployeeIdQuery.Append("where EmployeeEmailID=@EmailId\n");
            getEmployeeIdQuery.Append("End\n");
            return getEmployeeIdQuery.ToString();
        }
        /// <summary>
        /// Prepare store procedure query for GetCurrentAtteendaceStatusQuery
        /// </summary>
        /// <returns></returns>
        public static string PrepareGetCurrentAtteendaceStatusQuery()
        {
            var getCurrentAtteendaceStatusQuery = new StringBuilder();
            getCurrentAtteendaceStatusQuery.Append("create proc SP_GetCurrentAtteendaceStatus\n");
            getCurrentAtteendaceStatusQuery.Append("@EmployeeID varchar(10)\n");
            getCurrentAtteendaceStatusQuery.Append("As\n");
            getCurrentAtteendaceStatusQuery.Append("Begin\n");
            getCurrentAtteendaceStatusQuery.Append("select InTime,LunchTime,OutTime,TotalWorked,AttendanceStatus from AttendanceSheet\n");
            getCurrentAtteendaceStatusQuery.Append("where EmployeeId= @EmployeeID AND CONVERT(varchar(10),[Date],10)=CONVERT(varchar(10),GETDATE(),10) \n");
            getCurrentAtteendaceStatusQuery.Append("End\n");
            return getCurrentAtteendaceStatusQuery.ToString();
        }
           /// <summary>
        /// Prepare store procedure query for GetCurrentGetEmployeeHomeInfoQuery
        /// </summary>
        /// <returns></returns>
        public static string PrepareGetEmployeeHomeInfoQuery()
        {
            var getetEmployeeHomeInfoQuery = new StringBuilder();
            getetEmployeeHomeInfoQuery.Append("create proc SP_GetEmployeeHomeInfo\n");
            getetEmployeeHomeInfoQuery.Append("@EmployeeId varchar(10)\n");
            getetEmployeeHomeInfoQuery.Append("As\n");
            getetEmployeeHomeInfoQuery.Append("Begin\n");
            getetEmployeeHomeInfoQuery.Append("select FirstName, LastName, ProfilePicture, IsEmployeeAdmin as IsAdmin from EmployeeLeaveRegister\n");
            getetEmployeeHomeInfoQuery.Append("Where EmployeeId=@EmployeeId\n");
            getetEmployeeHomeInfoQuery.Append("End");
            return getetEmployeeHomeInfoQuery.ToString();
        }
        /// <summary>
        /// Prepare store procedure query for IsAttendanceRowExist
        /// </summary>
        /// <returns></returns>
        public static string PrepareIsAttendanceRowExistQuery() 
        {
            var isAttendanceRowExist = new StringBuilder();
            isAttendanceRowExist.Append("create proc SP_IsAttendanceRowExist\n");
            isAttendanceRowExist.Append("@EmployeeId varchar(40),\n");
            isAttendanceRowExist.Append("@Date date\n");
            isAttendanceRowExist.Append("As\n");
            isAttendanceRowExist.Append("Begin\n");
            isAttendanceRowExist.Append("select * from AttendanceSheet\n");
            isAttendanceRowExist.Append("where EmployeeId=@EmployeeId AND  [Date]=@Date\n");
            isAttendanceRowExist.Append("End");
            return isAttendanceRowExist.ToString();
        }
        /// <summary>
        /// Prepare store procedure query for IsAttendanceRowExist
        /// </summary>
        /// <returns></returns>
        public static string PrepareAddAttendanceByAdminQuery()
        {
            var addAttendanceByAdmin = new StringBuilder();
            addAttendanceByAdmin.Append("create proc SP_AddAttendanceByAdmin\n");
            addAttendanceByAdmin.Append("@EmployeeId varchar(40),\n");
            addAttendanceByAdmin.Append("@Date date,\n");
            addAttendanceByAdmin.Append("@AttendanceStatus varchar(30)\n");
            addAttendanceByAdmin.Append("As\n");
            addAttendanceByAdmin.Append("Begin\n");
            addAttendanceByAdmin.Append("insert into AttendanceSheet(EmployeeId,Date,Day,AttendanceStatus,InTime,OutTime,TotalWorked)\n");
            addAttendanceByAdmin.Append("values(@EmployeeID,@Date,datename(dw,@Date),@AttendanceStatus,'00:00:00','00:00:00',0)\n");
            addAttendanceByAdmin.Append("End");
            return addAttendanceByAdmin.ToString();
        }

        /// <summary>
        /// Prepare store procedure query for update employee info
        /// </summary>
        /// <returns></returns>
        public static string PrepareUpdateEmployeeInfoByAdminQuery()
        {
            var pdateEmployeeInfoByAdmin = new StringBuilder();
            pdateEmployeeInfoByAdmin.Append("create procedure SP_UpdateEmployeeInfoByAdmin\n");
            pdateEmployeeInfoByAdmin.Append("@ID int,\n");
            pdateEmployeeInfoByAdmin.Append("@FirstName varchar(30),\n");
            pdateEmployeeInfoByAdmin.Append("@LastName varchar(30),\n");
            pdateEmployeeInfoByAdmin.Append("@Doj date,\n");
            pdateEmployeeInfoByAdmin.Append("@Designation varchar(30),\n");
            pdateEmployeeInfoByAdmin.Append("@Company varchar(30)\n");
            pdateEmployeeInfoByAdmin.Append("as\n");
            pdateEmployeeInfoByAdmin.Append("begin\n");
            pdateEmployeeInfoByAdmin.Append("update EmployeeLeaveRegister\n");
            pdateEmployeeInfoByAdmin.Append("set FirstName=@FirstName, LastName=@LastName,DateOfJoining=@Doj,Designation=@Designation,Company=@Company\n");
            pdateEmployeeInfoByAdmin.Append("where ID=@ID\n");
            pdateEmployeeInfoByAdmin.Append("end\n");
            return pdateEmployeeInfoByAdmin.ToString();
        }
        /// <summary>
        /// Prepare store procedure query for update company leave
        /// </summary>
        /// <returns></returns>
        public static string PrepareUpdateCompanyLeaveQuery()
        {
            var updateCompanyLeave = new StringBuilder();
            updateCompanyLeave.Append("create procedure SP_UpdateCompanyLeave\n");
            updateCompanyLeave.Append("@Id int,\n");
            updateCompanyLeave.Append("@Date date,\n");
            updateCompanyLeave.Append("@Day char(10),\n");
            updateCompanyLeave.Append("@Reason varchar(100),\n");
            updateCompanyLeave.Append("@Modifiedby varchar(10)\n");
            updateCompanyLeave.Append("As\n");
            updateCompanyLeave.Append("Begin\n");
            updateCompanyLeave.Append("update  CompanyLeaveRegistration\n");
            updateCompanyLeave.Append("set ReasonDescription=@Reason,Date=@Date,Day=@Day,ModifiedBy=@Modifiedby,ModifiedDate=GETDATE()\n");
            updateCompanyLeave.Append("where ID=@Id\n");
            updateCompanyLeave.Append("End\n");
            return updateCompanyLeave.ToString();
        }
        /// <summary>
        /// Prepare store procedure qurey for News Deletion
        /// </summary>
        public static string PrepareDeletedAddedNewsQuery()
        {
            var deletedAddedNews = new StringBuilder();
            deletedAddedNews.Append("create proc SP_DeletedAddedNews\n");
            deletedAddedNews.Append("@Id int\n");
            deletedAddedNews.Append("As \n");
            deletedAddedNews.Append("Begin\n");
            deletedAddedNews.Append("delete from NewsAndUpdates where ID=@Id;\n");
            deletedAddedNews.Append("end\n");
            return deletedAddedNews.ToString();
        }
        /// <summary>
        /// Prepare fill attendance sheet after aprroved/rejected the applied leave
        /// </summary>
        public static string PrepareFillApproveLeaveAttendanceeQuery()
        {
            var fillApproveLeaveAttendance = new StringBuilder();
            fillApproveLeaveAttendance.Append("create proc SP_FillApproveLeaveAttendance\n");
            fillApproveLeaveAttendance.Append("@EmployeeId varchar(10),\n");
            fillApproveLeaveAttendance.Append("@Date date,\n");
            fillApproveLeaveAttendance.Append("@AttendanceStatus varchar(40)\n");
            fillApproveLeaveAttendance.Append("As\n");
            fillApproveLeaveAttendance.Append("Begin\n");
            fillApproveLeaveAttendance.Append("insert into AttendanceSheet(EmployeeId,Date,Day,AttendanceStatus)\n");
            fillApproveLeaveAttendance.Append("values(@EmployeeId,@Date,DATENAME(dw,@Date),@AttendanceStatus)\n");
            fillApproveLeaveAttendance.Append("end\n");
            return fillApproveLeaveAttendance.ToString();
        }
        /// <summary>
        /// Preapre store procedure query for Get update item by ID
        /// </summary>
        public static string PrepareGetUpdateItemByIdQuery()
        {
            var getUpdateItemById = new StringBuilder();
            getUpdateItemById.Append("create proc SP_GetUpdateItemByID\n");
            getUpdateItemById.Append("@ID int\n");
            getUpdateItemById.Append("As\n");
            getUpdateItemById.Append("Begin\n");
            getUpdateItemById.Append("select * from Updates where ID=@ID\n");
            getUpdateItemById.Append("End\n");
            return getUpdateItemById.ToString();
        }
        #endregion
    }
}