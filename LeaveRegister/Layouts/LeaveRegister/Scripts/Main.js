$(document).ready(function () {
    /*check wheathe employee is logged in or not*/
    if (main.IsEmployeeLoogedIn()) {
        $.ajax({
            url: lr.ajaxUrls.HomeTab,
            type: lr.constants.AjaxType,
            dataType: lr.constants.AjaxDataType,
            contentType: lr.constants.AjaxContentType,
            data: lr.sendData.PageReloadData(),
            success: function (data) {
                var info = data.d;
                if (info.Status) {
                    lr.functions.RemoveLeaveRegisterContainer();
                    $("body").append(lr.prepareHtml.Home);
                    main.Home();
                    main.HowToUse();
                    main.Attendance();
                    main.ApplyLeave();
                    main.Details();
                    main.Login();
                    main.ManageYourProfile();
                    main.ChnagePassword();
                    main.Admin();
                    main.Logout();
                    main.PageReload();
                    prepareData.Home(info, '');
                    return;
                } else {
                    main.LoginPage();
                    return;
                }
            },
            error: function () {
                main.LoginPage();
            }
        });
    } else {
        main.LoginPage();
    }
});
var main = {
    IsEmployeeLoogedIn: function() {
        return (lr.functions.GetQueryString()["sId"] != undefined && lr.functions.GetQueryString()["sId"] != null && lr.functions.GetQueryString()["sVal"] != "");
    },
    PageReload:function() {
        $("#" + lr.ids.CompanyNameTitle).click(function () {
            location.reload();
        });
        $("#" + lr.ids.CompanyName).click(function () {
            location.reload();
        });
    },
    LoginPage:function() {
        $("body").append(lr.prepareHtml.LoginPage)
            .append(lr.prepareHtml.Alert);
        main.SignUp();
        main.ForgotPasswordContainer();
        main.Login();
        main.PageReload();
    },
    SignUp: function () {
        $("#" + lr.ids.SignUpFromLogin).click(function () {
            lr.functions.RemoveLeaveRegisterContainer();
            $("body").append(lr.prepareHtml.SignUp);
            $("#" + lr.ids.RegistrationEmailIdAddress).focus();
            main.SignInContainer();
            main.EmployeeSignUp();
        });
    },
    SignInContainer: function () {
        $("#" + lr.ids.SignInLink).click(function () {
            lr.functions.RemoveLeaveRegisterContainer();
            $("body").append(lr.prepareHtml.LoginPage);
            $("#" + lr.ids.LoginEmailIdAddress).focus();
            main.SignUp();
            main.ForgotPasswordContainer();
            main.Login();
        });
    },
    ForgotPasswordContainer: function () {
        $("#" + lr.ids.ForgotPasswordLink).click(function () {
            lr.functions.RemoveLeaveRegisterContainer();
            $("body").append(lr.prepareHtml.ForgotPassword);
            $("#" + lr.ids.RecoverPasswordEmailId).focus();
            main.SignInContainer();
            main.EmployeeRequestForgotPassword();
        });
    },
    Login: function () {
        $("#" + lr.ids.LoginButton).click(function (event) {
            event.preventDefault();
            var emailValidationMessage = lr.controlValidation.EmailValidation(lr.ids.LoginEmailIdAddress);
            if (emailValidationMessage != lr.constants.Success) {
                lr.functions.Alert(lr.alertType.Information, emailValidationMessage);
                return;
            }
            if (lr.controlValidation.IsNullOrEmpty(lr.ids.LoginPassword)) {
                lr.functions.Alert(lr.alertType.Information, "Password can not be empty.");
                return;
            } else {
                $.ajax({
                    url: lr.ajaxUrls.SignIn,
                    dataType: lr.constants.AjaxDataType,
                    type: lr.constants.AjaxType,
                    data: lr.sendData.LoginInformation(),
                    contentType: lr.constants.AjaxContentType,
                    success: function (data) {
                        var info = data.d;
                        if (info.Status) {
                            lr.functions.RemoveLeaveRegisterContainer();
                            $("body").append(lr.prepareHtml.Home);
                            main.Home();
                            main.HowToUse();
                            main.Attendance();
                            main.ApplyLeave();
                            main.Details();
                            main.Login();
                            main.ManageYourProfile();
                            main.ChnagePassword();
                            main.Admin();
                            main.Logout();
                            main.PageReload();
                            prepareData.Home(info, '');
                            return;
                        } else {
                            lr.functions.Alert(lr.alertType.Warning, info.StatusText);
                            return;
                        }
                    },
                    error: function () {
                        lr.functions.Alert(lr.alertType.Error, "Some error occur. Please try later");
                        return;
                    }
                });
            }
        });
    },
    ActionOnHomeTab:function() {
        $.ajax({
            url: lr.ajaxUrls.HomeTab,
            type: lr.constants.AjaxType,
            dataType: lr.constants.AjaxDataType,
            contentType: lr.constants.AjaxContentType,
            data: lr.sendData.Session(),
            success: function (data) {
                var info = data.d;
                if (info.Status) {
                    lr.functions.RemoveLeaveRegisterContainer();
                    lr.functions.ActiveHeaderMenue(lr.ids.Home);
                    $("#" + lr.ids.LeaveRegisterFooter).before(lr.prepareHtml.HomeTabClick);
                    prepareData.Home(info, 'homeTab');
                } else {
                    lr.functions.Alert(lr.alertType.Error, "Some <b>problem</b> occor. Please try again");
                }
            },
            error: function () {
                lr.functions.Alert(lr.alertType.Error, "Some <b>error</b> occor. Please try later");
            }
        });  
    },
    Home: function () {
        $("#" + lr.ids.HomeMenue).click(function () {
            main.ActionOnHomeTab();
        });
    },
    HowToUse: function () {
        $("#" + lr.ids.HowToUseMenue).click(function () {
            lr.functions.RemoveLeaveRegisterContainer();
            lr.functions.ActiveHeaderMenue(lr.ids.HowToUse);
            $("#" + lr.ids.LeaveRegisterFooter).before(lr.prepareHtml.HowToUse);
            prepareData.HowToUse();
        });
    },
    Attendance: function () {
        $("#" + lr.ids.AttendanceMenue).click(function () {
            $.ajax({
                url: lr.ajaxUrls.AttendanceTab,
                type: lr.constants.AjaxType,
                dataType: lr.constants.AjaxDataType,
                contentType: lr.constants.AjaxContentType,
                data: lr.sendData.Session(),
                success: function (data) {
                    var info = data.d;
                    if (info.StatusText == "Success") {
                        lr.functions.RemoveLeaveRegisterContainer();
                        lr.functions.ActiveHeaderMenue(lr.ids.Attendance);
                        $("#" + lr.ids.LeaveRegisterFooter).before(lr.prepareHtml.Attendance);
                        prepareData.Attendance(info);
                        main.CompleteYourInTimeAttendance();
                        main.CompleteYourOutTimeAttendance();
                    } else {
                        lr.functions.Alert(lr.alertType.Error, "Some <b>problem</b> occor. Please try again");
                    }
                },
                error: function () {
                    lr.functions.Alert(lr.alertType.Error, "Some <b>error</b> occor. Please try later");
                }
            });
        });
    },
    ApplyLeave: function () {
        $("#" + lr.ids.LeaveMenue).click(function () {
            lr.functions.RemoveLeaveRegisterContainer();
            lr.functions.ActiveHeaderMenue(lr.ids.Leave);
            $("#" + lr.ids.LeaveRegisterFooter).before(lr.prepareHtml.Leave);
            $("#" + lr.ids.FromDate).focus();
            actions.ApplyLeave();
            actions.GetLeaveTableForEmployee();
        });
    },
    Details: function () {
        $("#" + lr.ids.DetailsMaenu).click(function () {
            lr.functions.RemoveLeaveRegisterContainer();
            lr.functions.ActiveHeaderMenue(lr.ids.Details);
            $("#" + lr.ids.LeaveRegisterFooter).before(lr.prepareHtml.Details);
            $("#" + lr.ids.DateToKnowAttendance).focus();
            actions.GetYourAttendanceDeatil();
            actions.CheckYourLeaveStatus();
            actions.GetYourLeaveRegister();
        });
    },
    Admin: function () {
        $("#" + lr.ids.LinkAdmin).click(function () {
            lr.functions.RemoveLeaveRegisterContainer();
            lr.functions.ActiveHeaderMenue("");
            $("#" + lr.ids.LeaveRegisterFooter).before(lr.prepareHtml.AdminConfigureAttendance);
            $("#" + lr.ids.TotalCasulaLeave).focus();
            main.AddRemoveCompanyLeave();
            main.AdminCompanyNews();
            main.AdminEditEmployeeAttendance();
            main.AdminEmployeeRegistration();
            main.AdminRightPanelConfigureLeave();
            adminActions.ConfigureLeaveAttendance();
            adminActions.GetConfigurationData();
        });
    },
    ManageYourProfile: function () {
        $("#" + lr.ids.EditProfileLink).click(function () {
            $.ajax({
                url: lr.ajaxUrls.ManageProfile,
                type: lr.constants.AjaxType,
                dataType: lr.constants.AjaxDataType,
                contentType: lr.constants.AjaxContentType,
                data: lr.sendData.Session(),
                success: function (data) {
                    var info = data.d;
                    if (info.StatusText == "success") {
                        lr.functions.RemoveLeaveRegisterContainer();
                        lr.functions.ActiveHeaderMenue("");
                        $("#" + lr.ids.LeaveRegisterFooter).before(lr.prepareHtml.ManageYourProfile);
                        profile.FillEmmployeeInformations(info);
                        profile.SaveProfileInformation();
                        $("#" + lr.ids.EmpDOB).focus();
                    } else {
                        lr.functions.Alert(lr.alertType.Warning, "Some <b>problem</b> occor. Please try agin");
                    }
                },
                error: function () {
                    lr.functions.Alert(lr.alertType.Error, "Some <b>error</b> occor. Please try later");
                }
            });
        });
    },
    ChnagePassword: function () {
        $("#" + lr.ids.ChangeProfilePasswordLink).click(function () {
            lr.functions.RemoveLeaveRegisterContainer();
            lr.functions.ActiveHeaderMenue("");
            $("#" + lr.ids.LeaveRegisterFooter).before(lr.prepareHtml.ChangePassword);
            profile.ChangePassword();
            $("#" + lr.ids.CurrentPasswordInputControl).focus();
        });
    },
    Logout: function () {
        $("#" + lr.ids.LogOutLink).click(function () {
            location.reload();
            location.hash = "";
        });
    },
    AddRemoveCompanyLeave: function () {
        $("#" + lr.ids.RegisterCompanyLeaveMenu).click(function () {
            lr.functions.RemoveAdminRightPanelContainer();
            lr.functions.ActiveAdminLink(lr.ids.RegisterCompanyLeave);
            $("#" + lr.ids.AdminModelHeaderText).text("Admin (Company Leave)");
            $("#" + lr.ids.LeftAdminPanelBody).after(lr.prepareHtml.AdminCompanyLeave);
            main.AddLeave();
            adminActions.GetCompanyLeaveTables();
            adminActions.AddCompanyLeaveByadmin();
        });
    },
    AdminEmployeeRegistration: function () {
        $("#" + lr.ids.RegisterCompanyEmployeeMenu).click(function () {
            lr.functions.RemoveAdminRightPanelContainer();
            lr.functions.ActiveAdminLink(lr.ids.RegisterCompanyEmployee);
            $("#" + lr.ids.AdminModelHeaderText).text("Admin (Employee Registration)");
            $("#" + lr.ids.LeftAdminPanelBody).after(lr.prepareHtml.AdminEmployeeRegistration);
            main.AddEmployee();
            adminActions.GetEmployeeRegistrationTable();
            adminActions.AddEmployeeByAdmin();
        });
    },
    AdminEditEmployeeAttendance: function () {
        $("#" + lr.ids.EditAttendanceRegisterMenu).click(function () {
            lr.functions.RemoveAdminRightPanelContainer();
            lr.functions.ActiveAdminLink(lr.ids.EditAttendanceRegister);
            $("#" + lr.ids.AdminModelHeaderText).text("Admin (Edit Employee Attendance)");
            $("#" + lr.ids.LeftAdminPanelBody).after(lr.prepareHtml.AdminEditEmployeeAttendance);
            $("#" + lr.ids.SelectEmpIDtoEditControls).focus();
            adminActions.EditEmployeeattendance();
        });
    },
    AdminCompanyNews: function () {
        $("#" + lr.ids.CompanyNewsMenu).click(function () {
            lr.functions.RemoveAdminRightPanelContainer();
            lr.functions.ActiveAdminLink(lr.ids.CompanyNews);
            $("#" + lr.ids.AdminModelHeaderText).text("Admin (Company News)");
            $("#" + lr.ids.LeftAdminPanelBody).after(lr.prepareHtml.AdminNews);
            main.AddNews();
            adminActions.GetComanyNews();
        });
    },
    AdminRightPanelConfigureLeave: function () {
        $("#" + lr.ids.ConfigureAttendanceMenu).click(function () {
            lr.functions.RemoveAdminRightPanelContainer();
            lr.functions.ActiveAdminLink(lr.ids.ConfigureAttendance);
            $("#" + lr.ids.AdminModelHeaderText).text("Admin (Configure Attendance)");
            $("#" + lr.ids.LeftAdminPanelBody).after(lr.prepareHtml.ConfigureRightPanelAttendance);
            $("#" + lr.ids.TotalCasulaLeave).focus();
            adminActions.ConfigureLeaveAttendance();
            adminActions.GetConfigurationData();
        });
    },
    AddEmployee: function () {
        $("#" + lr.ids.EmploayeeRegistrationAddNewEmpLinkText).click(function () {
            lr.functions.OpenModel();
            $("#" + lr.ids.FirstNameInputControl).focus();
        });
    },
    AddLeave: function () {
        $("#" + lr.ids.CompanyLeaveAddNewEmpLinkText).click(function () {
            lr.functions.OpenModel();
            $("#" + lr.ids.AddLeaveControlDate).focus();
        });
    },
    AddNews: function () {
        $("#" + lr.ids.AddNewsLinkText).click(function () {
            lr.functions.OpenModel();
            $("#" + lr.ids.NewsTitleInputControl).focus();
            adminActions.AddNewsByAdmin();
        });
    },
    CompleteYourInTimeAttendance: function () {
        $("#" + lr.ids.InTimeSubmitButton).click(function () {
            $.ajax({
                url: lr.ajaxUrls.FillInTimeAttendance,
                type: lr.constants.AjaxType,
                dataType: lr.constants.AjaxDataType,
                contentType: lr.constants.AjaxContentType,
                data: lr.sendData.Session(),
                success: function (data) {
                    var info = data.d;
                    if (info.StatusText == "Success") {
                        lr.functions.Alert(lr.alertType.Success, "<b>Thank You</b> you have completed your in time attendance");
                        prepareData.Attendance(info);
                        main.CompleteYourOutTimeAttendance();
                    } else {
                        lr.functions.Alert(lr.alertType.Error, "Something went wrong please try again");
                    }
                },
                error: function () {
                    lr.functions.Alert(lr.alertType.Error, "Some <b>error</b> occor. Please try later");
                }
            });
        });
    },
    CompleteYourOutTimeAttendance: function () {
        if (("#" + lr.ids.OutTimeSubmitButton).length) {
            $("#" + lr.ids.OutTimeSubmitButton).click(function () {
                $.ajax({
                    url: lr.ajaxUrls.FillOutTimeAttendance,
                    type: lr.constants.AjaxType,
                    dataType: lr.constants.AjaxDataType,
                    contentType: lr.constants.AjaxContentType,
                    data: lr.sendData.Session(),
                    success: function (data) {
                        var info = data.d;
                        if (info.StatusText == "Success") {
                            lr.functions.Alert(lr.alertType.Success, "<b>Thank You</b> you have completed your out time attendance");
                            prepareData.Attendance(info);
                        } else {
                            lr.functions.Alert(lr.alertType.Error, "Something went wrong please try again");
                        }
                    },
                    error: function () {
                        lr.functions.Alert(lr.alertType.Error, "Some <b>error</b> occor. Please try later");
                    }
                });
            });
        }
    },
    EmployeeSignUp:function() {
        $("#" + lr.ids.SignUpButton).click(function() {
            if (lr.controlValidation.IsNullOrEmpty(lr.ids.RegistrationEmailIdAddress)) {
                lr.functions.Alert(lr.alertType.Warning, "<b>Email address</b> can not be empty");
                return;
            }
            if (lr.controlValidation.IsNullOrEmpty(lr.ids.SignUpEmpId)) {
                lr.functions.Alert(lr.alertType.Warning, "<b>Employee Id</b> can not be empty");
                return;
            }
            if (lr.controlValidation.IsNullOrEmpty(lr.ids.SignUpPassword)) {
                lr.functions.Alert(lr.alertType.Warning, "<b>Password</b> can not be empty");
                return;
            }
            if (lr.controlValidation.IsNullOrEmpty(lr.ids.SignUpRePassword)) {
                lr.functions.Alert(lr.alertType.Warning, "<b>Re Password</b> can not be empty");
                return;
            }
            var password = $("#" + lr.ids.SignUpPassword).val();
            var rePassword = $("#" + lr.ids.SignUpRePassword).val();
            if (password.length < 6) {
                lr.functions.Alert(lr.alertType.Warning, "Please enter minimum 7 alphanumeric character for your <b>password</b>");
                return;
            }
            if (password != rePassword) {
                lr.functions.Alert(lr.alertType.Warning, "<b>Password </b> and <b>Re Password</b> can not be empty");
                return;
            }
            $.ajax({
              url:lr.ajaxUrls.SignUp,
              type:lr.constants.AjaxType,
              dataType:lr.constants.AjaxDataType,
              contentType:lr.constants.AjaxContentType,
              data:lr.sendData.SignUp(),
              success: function (data) {
              if (data.d == "Success") {
                  lr.functions.Alert(lr.alertType.Success, "Successfully you have set your password. Now please log in to access <b>Leave Register</b>");
              } else {
                  lr.functions.Alert(lr.alertType.Information, data.d);
              }
              },
              error:function() {
                  lr.functions.Alert(lr.alertType.Error, "Something went wrong, please try later");  
              }
            });
        });
    },
    EmployeeRequestForgotPassword:function() {
        $("#" + lr.ids.RecoverPasswordButton).click(function() {
            if (lr.controlValidation.IsNullOrEmpty(lr.ids.RecoverPasswordEmailId)) {
                lr.functions.Alert(lr.alertType.Warning, "Email address can not be empty");
                return;
            }
            $.ajax({
                url: lr.ajaxUrls.RecoverPassword,
                type: lr.constants.AjaxType,
                dataType: lr.constants.AjaxDataType,
                contentType: lr.constants.AjaxContentType,
                data: lr.sendData.RecoverPassword(),
                success: function (data) {
                        lr.functions.Alert(lr.alertType.Success, data.d);
                },
                error: function () {
                    lr.functions.Alert(lr.alertType.Error, "Something went wrong, please try later");
                }
            });
        });
    }
};
var profile = {
    FillEmmployeeInformations: function (data) {
        $("#" + lr.ids.EmpFirstName).html(data.FirstName);
        $("#" + lr.ids.EmpLastName).html(data.LastName);
        if (data.DateofBirth != "") {
            $("#" + lr.ids.EmpDOB).val(lr.functions.SetDateInputField(lr.functions.FormatDate(data.DateofBirth)));
        }
        $("#" + lr.ids.EmpID).html(data.EmailId);
        $("#" + lr.ids.EmpDesignation).html(data.Designation);
        $("#" + lr.ids.EmpDateOfJoining).html(lr.functions.FormatDate(data.DateOfJoining));
        $("#" + lr.ids.EmpCompany).html(data.Company);
    },
    ChangePassword: function () {
        $("#" + lr.ids.ChangePasswordButton).click(function () {
            var newPassword = $("#" + lr.ids.NewPasswordInputControl).val();
            var reNewPassword = $("#" + lr.ids.ReEnterPasswordInputControl).val();
            if (lr.controlValidation.IsNullOrEmpty(lr.ids.CurrentPasswordInputControl)) {
                lr.functions.Alert(lr.alertType.Warning, "Current password can not empty");
                return;
            };
            if (lr.controlValidation.IsNullOrEmpty(lr.ids.NewPasswordInputControl)) {
                lr.functions.Alert(lr.alertType.Warning, "Please enter your new password");
                return;
            }
            if (newPassword.length < 7) {
                lr.functions.Alert(lr.alertType.Warning, "Password length must be minimum 7 character");
                return;
            }
            if (lr.controlValidation.IsNullOrEmpty(lr.ids.ReEnterPasswordInputControl)) {
                lr.functions.Alert(lr.alertType.Warning, "Please enter your Re-enter new password");
                return;
            }
            if (newPassword != reNewPassword) {
                lr.functions.Alert(lr.alertType.Warning, "Password and Re Enter new password is not matching");
                return;
            } else {
                $.ajax({
                    url: lr.ajaxUrls.ChangePassword,
                    type: lr.constants.AjaxType,
                    dataType: lr.constants.AjaxDataType,
                    contentType: lr.constants.AjaxContentType,
                    data: lr.sendData.PasswordReset(),
                    success: function (data) {
                        if (data.d[0] == "Success") {
                            lr.functions.Alert(lr.alertType.Success, "Successfully your password has been changed");
                            $("#" + lr.ids.SessionValue).val(data.d[1]);
                        } else {
                            lr.functions.Alert(lr.alertType.Warning, data.d[0]);
                        }
                    },
                    error: function () {
                        lr.functions.Alert(lr.alertType.Error, "Some <b>error</b> occor. Please try later");
                    }
                });
            }
        });
    },
    SaveProfileInformation: function () {
        $("#" + lr.ids.ProfileInfoSumbit).click(function () {
            if (lr.controlValidation.IsNullOrEmpty(lr.ids.EmpDOB)) {
                lr.functions.Alert(lr.alertType.Warning, "Please select date for your <b>date of birth</b>");
                return;
            } else {
                $.ajax({
                    url: lr.ajaxUrls.SaveProfile,
                    type: lr.constants.AjaxType,
                    dataType: lr.constants.AjaxDataType,
                    contentType: lr.constants.AjaxContentType,
                    data: lr.sendData.SaveProfile(),
                    success: function (data) {
                        lr.functions.Alert(lr.alertType.Success, data.d);
                    },
                    error: function () {
                        lr.functions.Alert(lr.alertType.Error, "Some <b>error</b> occor. Please try later");
                    }
                });
            }
        });
    }
};
var adminActions = {
    ConfigureLeaveAttendance: function() {
        var setConfiguration = $("#" + lr.ids.SetTotalLeaves);
        $("." + lr.classes.SetLeaveInputBox).prop("disabled", "disabled");
        setConfiguration.prop("disabled", "disabled");
        $("#" + lr.ids.EditLeaveConfiguration).click(function() {
            $("." + lr.classes.SetLeaveInputBox).removeAttr("disabled");
            setConfiguration.removeAttr("disabled");
        });
        setConfiguration.click(function() {
            if (lr.controlValidation.IsNullOrEmpty(lr.ids.TotalCasulaLeave) || lr.controlValidation.IsNullOrEmpty(lr.ids.TotalSickLeave) || lr.controlValidation.IsNullOrEmpty(lr.ids.TotalWorkingHours)) {
                lr.functions.Alert(lr.alertType.Warning, "<b>Total casual leave</b>,<b> Total sick leave</b> and <b>working hours</b> controls can not be empty.");
            } else {
                $.ajax({
                    url: lr.ajaxUrls.SetLeaveDays,
                    type: lr.constants.AjaxType,
                    contentType: lr.constants.AjaxContentType,
                    dataType: lr.constants.AjaxDataType,
                    data: lr.sendData.ConfigureAttendance(),
                    success: function(data) {
                        var info = data.d;
                        if (info.StatusText == "Success") {
                            lr.functions.Alert(lr.alertType.Success, "Successfully saved settings");
                            adminActions.ConfigureLeaveAttendance(info);
                        } else {
                            lr.functions.Alert(lr.alertType.Warning, info.StatusText);
                        }
                    },
                    error: function() {
                        lr.functions.Alert(lr.alertType.Error, "Some <b>error</b> occor. Please try later");
                    }
                });
            }
        });
    },
    SetConfigurationData: function(info) {
        $("#" + lr.ids.TotalCasulaLeave).val(info.CasualLeave);
        $("#" + lr.ids.TotalSickLeave).val(info.SickLeave);
        $("#" + lr.ids.TotalWorkingHours).val(info.TotalWorkingHours);
        $("." + lr.classes.SetLeaveInputBox).prop("disabled", "disabled");
        $("#" + lr.ids.SetTotalLeaves).prop("disabled", "disabled");
    },
    GetConfigurationData: function() {
        $.ajax({
            url: lr.ajaxUrls.GetLeaveDays,
            type: lr.constants.AjaxType,
            dataType: lr.constants.AjaxDataType,
            contentType: lr.constants.AjaxContentType,
            data: lr.sendData.Session(),
            success: function(data) {
                var info = data.d;
                if (info.StatusText == "Success") {
                    adminActions.SetConfigurationData(info);
                } else {
                    lr.functions.Alert(lr.alertType.Warning, info.StatusText);
                }
            },
            error: function() {
                lr.functions.Alert(lr.alertType.Error, "Some <b>error</b> occor. Please try later");
            }
        });
    },
    EditEmployeeattendance: function() {
        $("#" + lr.ids.AttendanceStatusByAdmiin).html("Please select <u>employee id</u> and <u>date</u> to know/change the attendance status");
        $("#" + lr.ids.ChangeYourAttendanceControls).prop("disabled", "disabled");
        $("#" + lr.ids.ChangeYourAttendanceButton).prop("disabled", "disabled");
        $.ajax({
            url: lr.ajaxUrls.EditEmployeeAttendance,
            type: lr.constants.AjaxType,
            dataType: lr.constants.AjaxDataType,
            contentType: lr.constants.AjaxContentType,
            data: lr.sendData.Session(),
            success: function(data) {
                var info = data.d;
                var options = new Array();
                for (var i = 0; i < info.length; i++) {
                    options[i] = info[i].EmployeeId;
                }
                var optionsHtml = lr.functions.PrepareDdmOptions(options);
                $("#" + lr.ids.SelectEmpIDtoEditControls).append(optionsHtml);
            },
            error: function() {
                lr.functions.Alert(lr.alertType.Error, "Some <b>error</b> occor. Please try later");
            }
        });
        $("#" + lr.ids.SelectEmpIDtoEditControls).change(function() {
            $("#" + lr.ids.SelectDateToEditAttenceDate).val("");
            $("#" + lr.ids.ChangeYourAttendanceControls).prop("selectedIndex", 0)
                .prop("disabled", "disabled");
            $("#" + lr.ids.ChangeYourAttendanceButton).prop("disabled", "disabled");
            $("#" + lr.ids.AttendanceStatusByAdmiin).html("Please select <u>employee id</u> and <u>date</u> to know/change the attendance status");
        });
        $("#" + lr.ids.SelectDateToEditAttenceDate).change(function() {
            $("#" + lr.ids.ChangeYourAttendanceControls).prop("selectedIndex", 0);
            if (lr.controlValidation.IsSelectNullOrEmpty(lr.ids.SelectEmpIDtoEditControls)) {
                lr.functions.Alert(lr.alertType.Warning, "Please select employee Id");
                $("#" + lr.ids.SelectDateToEditAttenceDate).val("");
                return;
            }
            if (lr.controlValidation.IsSelectNullOrEmpty(lr.ids.SelectDateToEditAttenceDate)) {
                $("#" + lr.ids.ChangeYourAttendanceControls).prop("selectedIndex", 0)
                    .prop("disabled", "disabled");
                $("#" + lr.ids.ChangeYourAttendanceButton).prop("disabled", "disabled");
                $("#" + lr.ids.AttendanceStatusByAdmiin).html("Please select <u>employee id</u> and <u>date</u> to know/change the attendance status");
            } else {
                $.ajax({
                    url: lr.ajaxUrls.GetAttendanceStatus,
                    type: lr.constants.AjaxType,
                    dataType: lr.constants.AjaxDataType,
                    contentType: lr.constants.AjaxContentType,
                    data: lr.sendData.GetEmployeeAttendanceStatus(),
                    success: function(data) {
                        $("#" + lr.ids.ChangeYourAttendanceControls).removeAttr("disabled");
                        var info = data.d;
                        if (info.AttendanceStatus != null) {
                            var totalWorked = info.TotalWorked;
                            if (totalWorked != null) {
                                totalWorked = totalWorked.toString();
                                if (totalWorked.indexOf("-") != -1) {
                                    totalWorked = totalWorked.substring(1, totalWorked.length);
                                }
                            } else {
                                totalWorked = 0;
                            }

                            $("#" + lr.ids.AttendanceStatusByAdmiin).html("Attendance status of selected employee id with date is  <b>" + info.AttendanceStatus + "</b>.<br/>" +
                                "Total worked of selected employee id with date is <b>" + totalWorked + " hours</b>.");
                        } else {
                            $("#" + lr.ids.AttendanceStatusByAdmiin).html("There are no attendance information of selected employee id with date");
                        }
                        adminActions.SaveAttendaceChange();
                    },
                    error: function() {
                        lr.functions.Alert(lr.alertType.Error, "Some <b>error</b> occor. Please try later");
                    }
                });
            }
        });
    },
    SaveAttendaceChange: function() {
        var attendanceType = $("#" + lr.ids.ChangeYourAttendanceControls);
        var saveAttendanceButton = $("#" + lr.ids.ChangeYourAttendanceButton);
        attendanceType.change(function() {
            if (!lr.controlValidation.IsSelectNullOrEmpty(lr.ids.ChangeYourAttendanceControls)) {
                saveAttendanceButton.removeAttr("disabled");
            } else {
                saveAttendanceButton.prop("disabled", "disabled");
            }
        });
        saveAttendanceButton.click(function() {
            $.ajax({
                url: lr.ajaxUrls.ChangeAttendanceStatus,
                type: lr.constants.AjaxType,
                dataType: lr.constants.AjaxDataType,
                contentType: lr.constants.AjaxContentType,
                data: lr.sendData.ChangeAttendanceStatus(),
                success: function(data) {
                    $("#" + lr.ids.AttendanceStatusByAdmiin).html(data.d);
                },
                error: function() {
                    lr.functions.Alert(lr.alertType.Error, "Some <b>error</b> occor. Please try later");
                }
            });
        });
    },
    GetEmployeeRegistrationTable: function() {
        $('#' + lr.ids.EmployeeTable).dataTable({
            processing: true,
            serverSide: false,
            retrieve: true,
            ajax: {
                url: lr.ajaxUrls.GetAddedEmployee,
                contentType: "application/json",
                type: "POST",
                dataSrc: function(rslt) {
                    return rslt.d;
                }
            },
            order: [[0, "desc"]],
            columns: [
                { data: "FirstName" },
                { data: "EmployeeId" },
                { data: "EmailId" },
                {
                    "mRender": function(data, type, full) {
                        return '<a class="btn btn-info btn-sm" onclick="adminActions.ViewEmployeeInformations(\'' + full.FirstName + '\',\'' + full.LastName + '\',\'' + full.EmployeeId + '\'' +
                            ',\'' + full.EmailId + '\',\'' + full.Designation + '\',\'' + full.DateOfJoining + '\',\'' + full.Company + '\')" title="view the row informations">' + 'View' + '</a> ' +
                            '<a title="Edit the row informations" onclick="adminActions.EditEmployeedetails(\'' + full.Id + '\',\'' + full.FirstName + '\',\'' + full.LastName + '\',\'' + full.EmployeeId + '\'' +
                            ',\'' + full.EmailId + '\',\'' + full.Designation + '\',\'' + full.DateOfJoining + '\',\'' + full.Company + '\')" class="btn btn-info btn-sm" >' + 'Edit' + '</a>';
                    }
                }
            ],
            columnDefs: [{ targets: [1], orderData: [2] }]
        });
    },
    ViewEmployeeInformations: function(firstName, lastName, employeeId, emailId, designation, dateOfJoining, company) {
        $("#" + lr.ids.ViewFirstNameInputControl).html($.trim(firstName));
        $("#" + lr.ids.ViewSecondNameInputControl).html($.trim(lastName));
        $("#" + lr.ids.ViewEmailIdInputControl).html($.trim(emailId));
        $("#" + lr.ids.ViewDesignationInputControl).html($.trim(designation));
        $("#" + lr.ids.ViewDOJInputControl).html(lr.functions.FormatDate(dateOfJoining));
        $("#" + lr.ids.ViewCompanyInputControl).html($.trim(company));
        $("#" + lr.ids.ViewEmployeeIdInputControl).html($.trim(employeeId));
        $("#" + lr.ids.ViewDetailsModal).modal();
    },
    EditEmployeedetails: function(rowId, firstName, lastName, employeeId, emailId, designation, dateOfJoining, company) {
        rowId = $.trim(rowId);
        firstName = $.trim(firstName);
        lastName = $.trim(lastName);
        employeeId = $.trim(employeeId);
        emailId = $.trim(emailId);
        designation = $.trim(designation);
        company = $.trim(company);
        var employeeRowId = $("#" + lr.ids.RowId);
        var employeeFirstName = $("#" + lr.ids.EditFirstNameInputControl);
        var employeeLastName = $("#" + lr.ids.EditLastNameInputControl);
        var employeeEmployeeId = $("#" + lr.ids.EditEmployeeIdInputControl);
        var employeeEmailId = $("#" + lr.ids.EditEmailIdInputControl);
        var employeeDesignation = $("#" + lr.ids.EditDesignationInputControl);
        var employeeDateOfJoining = $("#" + lr.ids.EditDOJInputControl);
        var employeeCompany = $("#" + lr.ids.EditCompanyInputControl);
        employeeRowId.val(rowId);
        employeeFirstName.val(firstName);
        employeeLastName.val(lastName);
        employeeEmployeeId.val(employeeId);
        employeeEmailId.val(emailId);
        employeeDesignation.val(designation);
        employeeDateOfJoining.val(lr.functions.SetDateInputField(lr.functions.FormatDate(dateOfJoining)));
        employeeCompany.val(company);
        $("#" + lr.ids.EditEmmployeeModal).modal();
        employeeFirstName.focus();
        $("#" + lr.ids.EditEmployeeButton).click(function() {
            var vaildationControls = $("#" + lr.ids.EditEmployeeValidationControls);
            if (lr.controlValidation.IsNullOrEmpty(lr.ids.EditFirstNameInputControl)) {
                vaildationControls.html("First name can not be empty");
                employeeFirstName.focus();
                return;
            }
            if (lr.controlValidation.IsNullOrEmpty(lr.ids.EditLastNameInputControl)) {
                vaildationControls.html("Last name can not be empty");
                employeeLastName.focus();
                return;
            }
            if (lr.controlValidation.IsNullOrEmpty(lr.ids.EditDesignationInputControl)) {
                vaildationControls.html("Designation can not be empty");
                employeeDesignation.focus();
                return;
            }
            if (lr.controlValidation.IsNullOrEmpty(lr.ids.EditDOJInputControl)) {
                vaildationControls.html("DOJ can not be empty");
                employeeDateOfJoining.focus();
                return;
            }
            if (lr.controlValidation.IsNullOrEmpty(lr.ids.EditCompanyInputControl)) {
                vaildationControls.html("Company name can not be empty");
                employeeCompany.focus();
                return;
            }
            if (lr.controlValidation.IsNullOrEmpty(lr.ids.RowId)) {
                vaildationControls.html("Somethink went wrong please reload the page and try again");
                return;
            }
            $.ajax({
                url: lr.ajaxUrls.UpdateAddedEmployeeEditEmployee,
                type: lr.constants.AjaxType,
                dataType: lr.constants.AjaxDataType,
                contentType: lr.constants.AjaxContentType,
                data: lr.sendData.UpdateEmployeeInfo(),
                success: function(data) {
                    if (data.d == "Success") {
                        vaildationControls.html("")
                            .css("color", "green")
                            .html("<h4>Employee's changes has been added suucessfully. preparing Employee tabel..</h5>");
                        setTimeout(function() {
                            $("#" + lr.ids.UserName).html(employeeFirstName.val());
                            lr.functions.RemoveAdminRightPanelContainer();
                            $("#" + lr.ids.LeftAdminPanelBody).after(lr.prepareHtml.AdminEmployeeRegistration);
                            main.AddEmployee();
                            adminActions.AddEmployeeByAdmin();
                            adminActions.GetEmployeeRegistrationTable();
                            $("#" + lr.ids.EditEmmployeeModal).modal('hide');
                            $('body').removeClass('modal-open');
                            $('.modal-backdrop').remove();
                        }, 1500);
                    } else {
                        vaildationControls.css("color", "red")
                            .html(data.d);
                    }
                },
                error: function() {
                    vaildationControls.html("Somethink went wrong please reload the page and try again");
                }
            });
        });
    },
    AddEmployeeByAdmin: function() {
        $("#" + lr.ids.AddEmployeeButton).click(function() {
            var addEmployeeValidationControls = $("#" + lr.ids.AddEmployeeValidationControls);
            if (lr.controlValidation.IsNullOrEmpty(lr.ids.FirstNameInputControl)) {
                addEmployeeValidationControls.html("First name can not be empty");
                $("#" + lr.ids.FirstNameInputControl).focus();
                return;
            }
            if (lr.controlValidation.IsNullOrEmpty(lr.ids.LastNameInputControl)) {
                addEmployeeValidationControls.html("Last name can not be empty");
                $("#" + lr.ids.LastNameInputControl).focus();
                return;
            }
            if (lr.controlValidation.IsNullOrEmpty(lr.ids.EmployeeIdInputControl)) {
                addEmployeeValidationControls.html("Employee Id can not be empty");
                $("#" + lr.ids.EmployeeIdInputControl).focus();
                return;
            }
            if (lr.controlValidation.IsNullOrEmpty(lr.ids.EmailIdInputControl) || (lr.controlValidation.EmailValidation(lr.ids.EmailIdInputControl) != lr.constants.Success)) {
                addEmployeeValidationControls.html("Email id can not be empty, please enter vailid email example <b>abcd@efg.com</b>");
                $("#" + lr.ids.EmailIdInputControl).focus();
                return;
            }
            if (lr.controlValidation.IsNullOrEmpty(lr.ids.DesignationInputControl)) {
                addEmployeeValidationControls.html("Designation can not be empty");
                $("#" + lr.ids.DesignationInputControl).focus();
                return;
            }
            if (lr.controlValidation.IsNullOrEmpty(lr.ids.DOJInputControl)) {
                addEmployeeValidationControls.html("Date of joining can not be empty");
                $("#" + lr.ids.DOJInputControl).focus();
                return;
            }
            if (lr.controlValidation.IsNullOrEmpty(lr.ids.CompanyInputControl)) {
                addEmployeeValidationControls.html("Company can not be empty");
                $("#" + lr.ids.CompanyInputControl).focus();
                return;
            } else {
                $.ajax({
                    url: lr.ajaxUrls.AddEmployee,
                    type: lr.constants.AjaxType,
                    dataType: lr.constants.AjaxDataType,
                    contentType: lr.constants.AjaxContentType,
                    data: lr.sendData.AddEmpolyee(),
                    success: function(data) {
                        if (data.d == "Success") {
                            addEmployeeValidationControls.html("")
                                .css("color", "green")
                                .html("<h4>Employee has been added suucessfully. preparing Employee tabel..</h5>");
                            setTimeout(function() {
                                lr.functions.RemoveAdminRightPanelContainer();
                                $("#" + lr.ids.LeftAdminPanelBody).after(lr.prepareHtml.AdminEmployeeRegistration);
                                main.AddEmployee();
                                adminActions.AddEmployeeByAdmin();
                                adminActions.GetEmployeeRegistrationTable();
                                $("#" + lr.ids.MyModal).modal('hide');
                                $('body').removeClass('modal-open');
                                $('.modal-backdrop').remove();
                            }, 1500);
                        } else {
                            addEmployeeValidationControls.css("color", "red")
                                .html(data.d);
                        }
                    },
                    error: function() {
                        addEmployeeValidationControls.html("Some <b>error</b> occor. Please try later");
                    }
                });
            }
        });
    },
    GetCompanyLeaveTables: function() {
        $("#" + lr.ids.LeaveTables).dataTable({
            processing: true,
            serverSide: false,
            retrieve: true,
            ajax: {
                url: lr.ajaxUrls.GetCompanyLeave,
                contentType: "application/json",
                type: "POST",
                dataSrc: function(rslt) {
                    return rslt.d;
                }
            },
            order: [[0, "desc"]],
            columns: [
                { data: "Date" },
                { data: "Day" },
                { data: "Description" },
                {
                    "mRender": function(data, type, full) {
                        return '<a class="btn btn-info btn-sm" onclick="adminActions.EditCompanyLeave(\'' + full.CompanyLeaveId + '\',\'' + full.Date + '\',\'' + full.Day + '\'' +
                            ',\'' + full.Description.replace("'", "\\'") + '\')" title="Edit row informations">' + 'Edit' + '</a> ' +
                            '<a title="Delete row informations" onclick="adminActions.DeleteCompanyLeave(\'' + full.CompanyLeaveId + '\')" class="btn btn-danger btn-sm" >' + 'Delete' + '</a>';
                    }
                }
            ],
            columnDefs: [{ targets: [1], orderData: [2] }]
        });
    },
    AddCompanyLeaveByadmin: function() {
        var date = $("#" + lr.ids.AddLeaveControlDate);
        var day = $("#" + lr.ids.AddLeaveControlDay);
        var description = $("#" + lr.ids.AddLeaveControlReason);
        var validationComment = $("#" + lr.ids.AddCompanyLeaveValidationControls);
        date.change(function() {
            day.html(lr.functions.GetDayName(date.val()));
        });
        $("#" + lr.ids.AddLeaveButton).click(function() {
            if (lr.controlValidation.IsNullOrEmpty(lr.ids.AddLeaveControlDate)) {
                validationComment.html("Date field can not be empty");
                date.focus();
                return;
            }
            if (lr.controlValidation.IsNullOrEmpty(lr.ids.AddLeaveControlReason)) {
                validationComment.html("Leave description can nnot be empty");
                description.focus();
                return;
            }
            $.ajax({
                url: lr.ajaxUrls.AddCompanyLeave,
                type: lr.constants.AjaxType,
                dataType: lr.constants.AjaxDataType,
                contentType: lr.constants.AjaxContentType,
                data: lr.sendData.AddCompanyLeaveData(),
                success: function(data) {
                    if (data.d == "Success") {
                        validationComment.html("")
                            .css("color", "green")
                            .html("<h5>Company leave has been added suucessfully. preparing Company tabel..</h5>");
                        setTimeout(function() {
                            lr.functions.RemoveAdminRightPanelContainer();
                            $("#" + lr.ids.LeftAdminPanelBody).after(lr.prepareHtml.AdminCompanyLeave);
                            main.AddLeave();
                            adminActions.AddCompanyLeaveByadmin();
                            adminActions.GetCompanyLeaveTables();
                            $("#" + lr.ids.MyModal).modal('hide');
                            $('body').removeClass('modal-open');
                            $('.modal-backdrop').remove();
                        }, 1000);
                    } else {
                        validationComment.css("color", "red")
                            .html(data.d + " Seems like there are one already leave is available with selected date <b>" + date.val() + "</b>");
                    }
                },
                error: function() {
                    validationComment.html("Some thing went wrong please try again");
                }
            });
        });
    },
    EditCompanyLeave: function(rowCompanyLeaveId, date, day, description) {
        rowCompanyLeaveId = $.trim(rowCompanyLeaveId);
        date = $.trim(date);
        date = date.split("-");
        date = date[0] + "/" + date[1] + "/" + date[2];
        day = $.trim(day);
        description = $.trim(description);
        var editDate = $("#" + lr.ids.EditLeaveControlDate);
        var editDay = $("#" + lr.ids.EditLeaveControlDay);
        var editDescription = $("#" + lr.ids.EditLeaveControlReason);
        var editValidation = $("#" + lr.ids.EditCompanyLeaveValidationControls);
        $("#" + lr.ids.RowCompanyLeaveId).val(rowCompanyLeaveId);
        editDate.val(lr.functions.SetDateInputField(date));
        editDay.html(day);
        editDescription.val(description);
        $("#" + lr.ids.EditCompanyLeaveModal).modal();
        $("#" + lr.ids.EditLeaveControlDate).focus();
        editDate.change(function() {
            editDay.html(lr.functions.GetDayName(editDate.val()));
        });
        $("#" + lr.ids.EditLeaveButton).click(function() {
            if (lr.controlValidation.IsNullOrEmpty(lr.ids.EditLeaveControlDate)) {
                editValidation.html("Date field can not be empty");
                editDate.focus();
                return;
            }
            if (lr.controlValidation.IsNullOrEmpty(lr.ids.EditLeaveControlReason)) {
                editValidation.html("Leave description can nnot be empty");
                editDescription.focus();
                return;
            }
            $.ajax({
                url: lr.ajaxUrls.UpdateCompanyLeave,
                type: lr.constants.AjaxType,
                dataType: lr.constants.AjaxDataType,
                contentType: lr.constants.AjaxContentType,
                data: lr.sendData.UpdateCompanyLeave(),
                success: function(data) {
                    if (data.d == "Success") {
                        editValidation.html("")
                            .css("color", "green")
                            .html("<h5>Company leave cahnges has been added suucessfully. Preparing Company tabel..</h5>");
                        setTimeout(function() {
                            $("#" + lr.ids.EditCompanyLeaveModal).modal('hide');
                            $('body').removeClass('modal-open');
                            $('.modal-backdrop').remove();
                            lr.functions.RemoveAdminRightPanelContainer();
                            $("#" + lr.ids.LeftAdminPanelBody).after(lr.prepareHtml.AdminCompanyLeave);
                            main.AddLeave();
                            adminActions.AddCompanyLeaveByadmin();
                            adminActions.GetCompanyLeaveTables();
                        }, 1000);
                    } else {
                        editValidation.css("color", "red")
                            .html(data.d + " Seems like there are one already leave is available with selected date <b>" + date.val() + "</b>");
                    }
                },
                error: function() {
                    editValidation.html("Some thing went wrong please try again");
                }
            });
        });
    },
    DeleteCompanyLeave: function(leaveId) {
        $("#" + lr.ids.RowCompanyLeaveId).val(leaveId);
        if (confirm("Are you sure, you wanted to delete?")) {
            $.ajax({
                url: lr.ajaxUrls.DeleteAddedLeave,
                type: lr.constants.AjaxType,
                dataType: lr.constants.AjaxDataType,
                contentType: lr.constants.AjaxContentType,
                data: lr.sendData.DeleteCompanyLeaveRow(),
                success: function(data) {
                    if (data.d == "Success") {
                        setTimeout(function() {
                            lr.functions.RemoveAdminRightPanelContainer();
                            $("#" + lr.ids.LeftAdminPanelBody).after(lr.prepareHtml.AdminCompanyLeave);
                            main.AddLeave();
                            adminActions.AddCompanyLeaveByadmin();
                            adminActions.GetCompanyLeaveTables();
                            lr.functions.CloseModel();
                            $('body').removeClass('modal-open');
                            $('.modal-backdrop').remove();
                        }, 1000);
                    } else {
                        lr.functions.Alert(lr.alertType.Information, data.d);
                        return;
                    }
                },
                error: function() {
                    lr.functions.Alert(lr.alertType.Error, "Some thing went wrong please try lator");
                }
            });
        }
    },
    GetComanyNews: function() {
        $("#" + lr.ids.NewsTable).dataTable({
            processing: true,
            serverSide: false,
            retrieve: true,
            ajax: {
                url: lr.ajaxUrls.GetNewsAndUpdate,
                contentType: "application/json",
                type: "POST",
                dataSrc: function(rslt) {
                    return rslt.d;
                }
            },
            order: [[0, "desc"]],
            columns: [
                { data: "Title" },
                { data: "Description" },
                {
                    "mRender": function(data, type, full) {
                        return '<a class="btn btn-info btn-sm" onclick="adminActions.EditCompanyNews(\'' + full.NewsId + '\',\'' + full.Title + '\',\'' + full.Description.replace("'", "\\'") + '\')" title="Edit row informations">' + 'Edit' + '</a> ' +
                            '<a title="Delete row informations" onclick="adminActions.DeleteCompanyNews(\'' + full.NewsId + '\')" class="btn btn-danger btn-sm" >' + 'Delete' + '</a>';
                    }
                }
            ],
            columnDefs: [{ targets: [1], orderData: [2] }]
        });
    },
    EditCompanyNews: function (newsId, title, description) {
        $("#" + lr.ids.NewsId).val($.trim(newsId));
        var newsTitle = $("#" + lr.ids.EditNewsTitleInputControl);
        var newsDescription = $("#" + lr.ids.EditNewsReasonControlReason);
        var validationMessage = $("#" + lr.ids.EditNewsValidationControls);
        $("#" + lr.ids.EditNewsModal).modal();
        newsTitle.val($.trim(title))
            .focus();
        newsDescription.val($.trim(description));
        $("#" + lr.ids.EditNewsButton).click(function() {
                if (lr.controlValidation.IsNullOrEmpty(lr.ids.EditNewsTitleInputControl)) {
                    validationMessage.html("News Title can not be empty");
                    newsTitle.focus();
                    return;
                }
                if (lr.controlValidation.IsNullOrEmpty(lr.ids.EditNewsReasonControlReason)) {
                    validationMessage.html("News Description can not be empty");
                    newsDescription.focus();
                    return;
                }
            $.ajax({
               url:lr.ajaxUrls.UpdateNewsAndUpdate,
               type:lr.constants.AjaxType,
               dataType:lr.constants.AjaxDataType,
               contentType:lr.constants.AjaxContentType,
               data:lr.sendData.EditNewsData(),
               success: function (data) {
                   if (data.d == "Success") {
                       validationMessage.html("Suucessfully saved your changes. Preparing news table...")
                           .css('color', 'green');
                       setTimeout(function () {
                           $("#" + lr.ids.EditNewsModal).modal('hide');
                           $('body').removeClass('modal-open');
                           $('.modal-backdrop').remove();
                           lr.functions.RemoveAdminRightPanelContainer();
                           $("#" + lr.ids.LeftAdminPanelBody).after(lr.prepareHtml.AdminNews);
                           main.AddNews();
                           adminActions.GetComanyNews();
                       }, 1000);
                   } else {
                       validationMessage.html("Some thing went wrong, please try again.")
                           .css('color', 'red');
                   }  
               },
               error:function(data) {
                   validationMessage.html(data.d);
               }
            });
        });
    },
    DeleteCompanyNews: function(newsId) {
        $("#" + lr.ids.NewsId).val($.trim(newsId));
        if (confirm("Are you sure, you wanted to delete?")) {
            $.ajax({
                url: lr.ajaxUrls.DeleteNewsAndUpdate,
                type: lr.constants.AjaxType,
                dataType: lr.constants.AjaxDataType,
                contentType: lr.constants.AjaxContentType,
                data: lr.sendData.DeleteNews(),
                success: function(data) {
                    if (data.d == "Success") {
                        setTimeout(function() {
                            lr.functions.RemoveAdminRightPanelContainer();
                            $("#" + lr.ids.LeftAdminPanelBody).after(lr.prepareHtml.AdminNews);
                            main.AddNews();
                            adminActions.GetComanyNews();
                        }, 1000);
                    } else {
                        lr.functions.Alert(lr.alertType.Information, "Some thing went wrong, please try again.");
                    }
                },
                error: function() {
                    lr.functions.Alert(lr.alertType.Error, "Failed, please try again");
                }
            });
        }
    },
    AddNewsByAdmin: function() {
        $("#" + lr.ids.AddNewsButton).click(function() {
            var title = $("#" + lr.ids.NewsTitleInputControl);
            var description = $("#" + lr.ids.NewsDescription);
            var validationControl = $("#" + lr.ids.AddNewsValidationControls);
            if (lr.controlValidation.IsNullOrEmpty(lr.ids.NewsTitleInputControl)) {
                validationControl.html("Title can not be empty");
                title.focus();
                return;
            }
            if (lr.controlValidation.IsNullOrEmpty(lr.ids.NewsDescription)) {
                validationControl.html("Descrption can not be empty");
                description.focus();
                return;
            }
            $.ajax({
               url:lr.ajaxUrls.AddNewsAndUpdate,
               type:lr.constants.AjaxType,
               dataType:lr.constants.AjaxDataType,
               contentType:lr.constants.AjaxContentType,
               data: lr.sendData.AddNewsData(),
               success:function(data) {
                   if (data.d == "Success") {
                       validationControl.html("")
                            .css("color", "green")
                            .html("<h5>News has been added suucessfully. Preparing News tabel..</h5>");
                       setTimeout(function () {
                           lr.functions.CloseModel();
                           $('body').removeClass('modal-open');
                           $('.modal-backdrop').remove();
                           lr.functions.RemoveAdminRightPanelContainer();
                           $("#" + lr.ids.LeftAdminPanelBody).after(lr.prepareHtml.AdminNews);
                           main.AddNews();
                           adminActions.GetComanyNews();
                       }, 1000);
                   } else {
                       validationControl.css("color", "red")
                            .html("There are some problem occur during saving news, please try again ");
                   }
               },
               error:function(data) {
                   validationControl.html(data.d);
               }
            });
        });
    },
    AcceptAppliedLeave:function() {
        $("." + lr.classes.AdminApproveLeave).click(function() {
            var leaveId = $(this).attr("value");
            $.ajax({
               url:lr.ajaxUrls.AcceptOrRejectAppliedLeave,
               type:lr.constants.AjaxType,
               dataType:lr.constants.AjaxDataType,
               contentType:lr.constants.AjaxContentType,
               data:lr.sendData.ActionOnLeave(leaveId,"Approved"),
               success:function(data) {
                    if (data.d == "Success") {
                        lr.functions.Alert(lr.alertType.Success, "Successfully operation completed.Updating updates information..");
                        setTimeout(function () {
                            lr.functions.CloseModel();
                            main.ActionOnHomeTab();
                        }, 1500);
                    } else {
                        lr.functions.Alert(lr.alertType.Error, "Something went wrong, please try lator");   
                    }
               },
               error:function() {
                   lr.functions.Alert(lr.alertType.Error, "Something went wrong, please try lator");
               }
            });
        });
    },
    RejectAppliedLeave:function() {
        $("." + lr.classes.AdminRejectLeave).click(function () {
            var leaveId = $(this).attr("value");
            $.ajax({
                url: lr.ajaxUrls.AcceptOrRejectAppliedLeave,
                type: lr.constants.AjaxType,
                dataType: lr.constants.AjaxDataType,
                contentType: lr.constants.AjaxContentType,
                data: lr.sendData.ActionOnLeave(leaveId, "Rejected"),
                success: function (data) {
                    if (data.d == "Success") {
                        lr.functions.Alert(lr.alertType.Success, "Successfully operation completed.Updating updates information.. ");
                        setTimeout(function () {
                            lr.functions.CloseModel();
                            main.ActionOnHomeTab();
                        }, 1500);
                    } else {
                        lr.functions.Alert(lr.alertType.Error, "Something went wrong, please try lator");
                    }
                },
                error: function () {
                    lr.functions.Alert(lr.alertType.Error, "Something went wrong, please try lator");
                }
            });
        }); 
    }
};
var actions = {
    ApplyLeave: function () {
        var fromDate = $("#" + lr.ids.FromDate);
        var toDate = $("#" + lr.ids.ToDate);
        $("#" + lr.ids.EnableToDateControl).click(function () {
            if (this.checked) {
                toDate.removeAttr("disabled");
            } else {
                toDate.prop("disabled", "disabled")
                    .val("");
            }
        });
        $("#" + lr.ids.ApplyLeaveButton).click(function () {
            if (lr.controlValidation.IsNullOrEmpty(lr.ids.FromDate)) {
                lr.functions.Alert(lr.alertType.Warning, "Please select your <b>from date</b>");
                return;
            } else {
                if (lr.functions.IsWeekend(fromDate.val())) {
                    lr.functions.Alert(lr.alertType.Warning, "Please select <b>week day as a from date</b>");
                    fromDate.val("");
                    return;
                }
            }
            var enableToDate = $("#" + lr.ids.EnableToDateControl);
            if (enableToDate[0].checked) {
                if (lr.controlValidation.IsNullOrEmpty(lr.ids.ToDate)) {
                    lr.functions.Alert(lr.alertType.Warning, "Please select your <b>to date</b>");
                    return;
                } else {
                    if (lr.functions.IsWeekend(toDate.val())) {
                        lr.functions.Alert(lr.alertType.Warning, "Please select <b>week day as a to date</b>");
                        toDate.val("");
                        return;
                    }
                    if (!lr.functions.isVaildToDate($("#" + lr.ids.FromDate), toDate)) {
                        lr.functions.Alert(lr.alertType.Warning, "<b>To Date</b> must be greater <b>From Date.</b>");
                        toDate.val("");
                        return;
                    }
                }
            }
            if (lr.functions.GetSelectValue(lr.ids.LeaveType) == "") {
                lr.functions.Alert(lr.alertType.Warning, "Please choose your <b>leave type</b>");
                return;
            } else {
                $.ajax({
                    url: lr.ajaxUrls.ApplyLeave,
                    type: lr.constants.AjaxType,
                    dataType: lr.constants.AjaxDataType,
                    contentType: lr.constants.AjaxContentType,
                    data: lr.sendData.ApplyLeave(),
                    success: function (data) {
                        if (data.d == "success") {
                            lr.functions.Alert(lr.alertType.Success, "You have successfully applied your leave.");
                        } else {
                            lr.functions.Alert(lr.alertType.Error, "Something went wrong please try again");
                        }
                    },
                    error: function () {
                        lr.functions.Alert(lr.alertType.Error, "Some <b>error</b> occor. Please try later");
                    }
                });
            }
        });
    },
    GetLeaveTableForEmployee:function() {
        $("#" + lr.ids.LeaveTableForEmployee).dataTable({
            processing: true,
            serverSide: false,
            retrieve: true,
            ajax: {
                url: lr.ajaxUrls.GetCompanyLeave,
                contentType: "application/json",
                type: "POST",
                dataSrc: function (rslt) {
                    return rslt.d;
                }
            },
            order: [[0, "desc"]],
            columns: [
                { data: "Date" },
                { data: "Day" },
                { data: "Description" }
            ],
            columnDefs: [{ targets: [1], orderData: [2]}]
        });
    },
    GetYourAttendanceDeatil: function () {
        $("#" + lr.ids.CheckYourAttendance).click(function () {
            if (lr.controlValidation.IsNullOrEmpty(lr.ids.DateToKnowAttendance)) {
                lr.functions.Alert(lr.alertType.Warning, "Please select date to know your attendance detail");
                return;
            } else {
                $.ajax({
                    url: lr.ajaxUrls.DetailesCheckAttendanceDetailes,
                    type: lr.constants.AjaxType,
                    dataType: lr.constants.AjaxDataType,
                    contentType: lr.constants.AjaxContentType,
                    data: lr.sendData.AttendanceDetails(),
                    success: function (data) {
                        var info = data.d;
                        var totalWorked = info.TotalWorked;
                        if (info.AttendanceStatus != null) {
                            if (totalWorked != null) {
                                totalWorked = totalWorked.toString();
                                if (totalWorked.indexOf("-") != -1) {
                                    totalWorked = totalWorked.substring(1, totalWorked.length);
                                }
                            } else {
                                totalWorked = "Not available";
                            }
                            lr.functions.Alert(lr.alertType.Success, "Your attendance detailes is mentioned below for <b>" + $("#" + lr.ids.DateToKnowAttendance).val() +
                                "</b>.<br/>Attendance status: <b>" + info.AttendanceStatus + "</b><br/>Total Worked: <b>" + totalWorked + " Hours</b>");
                        } else {
                            lr.functions.Alert(lr.alertType.Information, "There are no information of selected date <b>" + $("#" + lr.ids.DateToKnowAttendance).val() + "");
                        }
                    },
                    error: function () {
                        lr.functions.Alert(lr.alertType.Error, "Some <b>error</b> occor. Please try later");
                    }
                });
            }
        });
    },
    CheckYourLeaveStatus: function () {
        $("#" + lr.ids.CheckYourLeaveStatus).click(function () {
            if (lr.controlValidation.IsSelectNullOrEmpty(lr.ids.TypeToKnowLeave)) {
                lr.functions.Alert(lr.alertType.Warning, "Please select leave type to know your selected leave detail");
                return;
            } else {
                $.ajax({
                    url: lr.ajaxUrls.DetailesGetLeaveTypeDetailes,
                    type: lr.constants.AjaxType,
                    dataType: lr.constants.AjaxDataType,
                    contentType: lr.constants.AjaxContentType,
                    data: lr.sendData.LeaveDetails(),
                    success: function (data) {
                        var selectedLeaveType = $("#" + lr.ids.TypeToKnowLeave).val();
                        lr.functions.Alert(lr.alertType.Success, "Your leave detailes is mentioned below for <b>" + selectedLeaveType +
                            "</b>.<br/>You have taken total " + selectedLeaveType + " : <b>" + data.d + " days</b>");
                    },
                    error: function () {
                        lr.functions.Alert(lr.alertType.Error, "Some <b>error</b> occor. Please try later");
                    }
                });
            }
        });
    },
    GetYourLeaveRegister: function () {
        $("#" + lr.ids.GetLeaveregister).click(function () {
            if (lr.controlValidation.IsSelectNullOrEmpty(lr.ids.TypeOfMonth)) {
                lr.functions.Alert(lr.alertType.Warning, "Please select month to dowload your selected leave register");
                return;
            } else {
                $.ajax({
                    url: lr.ajaxUrls.DetailesGetEmployeeAttendanceSheets,
                    type: lr.constants.AjaxType,
                    dataType: lr.constants.AjaxDataType,
                    contentType: lr.constants.AjaxContentType,
                    data: lr.sendData.GetYourLeaveRegister(),
                    success: function () {
                        lr.functions.Alert(lr.alertType.Success, "Your <b>" + $("#" + lr.ids.TypeOfMonth).val() + "</b> month's leave register is downloading....");
                        setTimeout(function () {
                            lr.functions.CloseModel();
                        }, 2500);
                    },
                    error: function () {
                        lr.functions.Alert(lr.alertType.Error, "Some <b>error</b> occor. Please try later");
                    }
                });
            }
        });
    }
};
var prepareData = {
    Home: function (data, homeTab) {
        var attendanceDetails = data.AttendanceDetails;
        var employeeInfo = data.EmployeeInfo;
        var news = data.News;
        var updates = data.Updates;
        var sessionKey = data.SessionKey;
        var sessionValue = data.SessionValue;
        var adminUpdates = '';
        if (homeTab != 'homeTab') {
            sessionKey = (sessionKey == undefined || sessionKey == "") ? lr.functions.GetQueryString()["sId"] : sessionKey;
            sessionValue = (sessionValue == undefined || sessionValue == "") ? lr.functions.GetQueryString()["sVal"] : sessionValue;
            $("#" + lr.ids.SessionId).val(sessionKey);
            $("#" + lr.ids.SessionValue).val(sessionValue);
            lr.functions.AppendQueryString(sessionKey, sessionValue);
        }
        if (employeeInfo.IsAdmin) {
            adminUpdates = data.AdminUpdates;
        } else {
            $("#" + lr.ids.LinkAdmin).remove();
        }
        $("#" + lr.ids.UserName).text(employeeInfo.FirstName);
        //set Attendace details
        if (attendanceDetails.InTime == null || attendanceDetails.AttendanceStatus == null) {
            $("#" + lr.ids.AttendancePanelBody).append(prepareData.InTimeAttendanceIsNotFilled());
            lr.functions.Alert(lr.alertType.Information, "Please fill your <b>in time</b> attendance");
        } else {
            if (attendanceDetails.OuTime == "" || $.trim(attendanceDetails.OuTime) == ":undefined:undefined AM") {
                $("#" + lr.ids.AttendancePanelBody).append(prepareData.OutTimeAttendanceIsNotFilled(lr.functions.FormatTime(attendanceDetails.InTime), attendanceDetails.AttendanceStatus));
                if ($.trim(attendanceDetails.AttendanceStatus) == "Present") {
                    lr.functions.Alert(lr.alertType.Information, "Please fill your <b>out time</b> attendance");
                }
            } else {
                $("#" + lr.ids.AttendancePanelBody).append(prepareData.AttendanceCompleted(lr.functions.FormatTime(attendanceDetails.InTime), lr.functions.FormatTime(attendanceDetails.OuTime)));
            }
        };
        //set News data
        $("#" + lr.ids.NewsPanelBody).append(prepareData.NewsData(news));
        //set the updates    
        $("#" + lr.ids.UpdatesPanelBody).append(prepareData.Updates(adminUpdates, updates, employeeInfo.IsAdmin));
        adminActions.AcceptAppliedLeave();
        adminActions.RejectAppliedLeave();
    },
    InTimeAttendanceIsNotFilled: function () {
        return '<span style="font-weight:bold;">In Time</span><br/><br/><ul><li>You haven\'t fill your in time attendance.<br/><span style="color:blue;">Please fill your in time attendance.</span></li></ul>' +
            '<br/><hr><span style="font-weight:bold;">Out Time</span><br/><br/><ul><li>You haven\'t fill your in out attendance.</li></ul>';
    },
    OutTimeAttendanceIsNotFilled: function (inTime,status) {
        if (inTime == null || $.trim(inTime) == "" || $.trim(inTime) == ":undefined:undefined AM") {
        return '<span>You are on leave as <b>' + $.trim(status) + '</b></span>';   
    }
        return '<span style="font-weight:bold;">In Time</span><br/><br/><ul><li>You have completed your in time attendance at <h4 id="InTimeAttendanceTime">' + inTime + '</h4></li></ul>' +
            '<br/><hr><span style="font-weight:bold;">Out Time</span><br/><br/><ul><li>You haven\'t fill your out time attendance.' +
            '<br/><span style="color:blue;">Please fill your out time attendance.</span></li></ul>';
    },
    AttendanceCompleted: function (inTime, outTime) {
        return '<span style="font-weight:bold;">In Time</span><br/><br/><ul><li>You have completed your in time attendance at <h4>' + inTime + '</h4></li></ul>' +
            '<br/><hr><span style="font-weight:bold;">Out Time</span><br/><br/><ul><li>You have completed your out time attendance at <h4>' + outTime + '</h4></li></ul>';
    },
    NewsData: function (newsData) {
        var prepareNews = '';
        if (newsData.length > 0) {
            for (var i = 0; i < newsData.length; i++) {
                prepareNews += '<div class="row"><div><span class="pull-left" style="margin-left:17px;font-weight: bold;">' + newsData[i].Title + '</span></div>' +
                    '<div style="float:right;margin-top:2px;margin-right:18px;font-weight: bold;">' + lr.functions.FormatDate(newsData[i].Date) + '</div>' +
                    '<div style="margin-left:17px;margin-right:18px;margin-top:32px;">' + newsData[i].Description + '<div></div></div></div><hr>';
            };
            return prepareNews;
        } else {
            return '<div class="row"><div><span class="pull-left" style="margin-left:17px;font-weight: bold;">There are no news yet from admin side.</span></div>';
        }
    },
    Updates: function (adminupdates, employeeUpdates, isAdmin) {
        var updatesHtml = '';
        if (isAdmin) {
            updatesHtml += '<span style="font-weight:bold;">Leave Request</span><br/><br/>';
            if (adminupdates.length > 0) {
                for (var i = 0; i < adminupdates.length; i++) {

                    if (lr.functions.FormatDate(adminupdates[i].LeaveTill) == "1/1/0001") {
                        updatesHtml += '<span class="highLightLeave">' + adminupdates[i].Name + '</span> with employee id <span class="highLightLeave">' + adminupdates[i].EmployeeId + '</span> has ' +
                            'applied <span class="highLightLeave">' + adminupdates[i].LeaveType + '</span> for <span class="highLightLeave">' +
                            '' + lr.functions.FormatDate(adminupdates[i].LeaveFrom) + ' </span><a class="AdminApproveLeave" title="Accept applied leave" value="' + adminupdates[i].Id + '">Accept</a> <a class="AdminRejectLeave" value="' + adminupdates[i].Id + '" title="Reject applied leave">Reject</a></span><br/><hr>';
                    } else {
                        updatesHtml += '<span class="highLightLeave">' + adminupdates[i].Name + '</span> with ' +
                            'employee id <span class="highLightLeave">' + adminupdates[i].EmployeeId + '</span> has applied <span class="highLightLeave">' + adminupdates[i].LeaveType + '</span> from ' +
                            '<span class="highLightLeave">' + lr.functions.FormatDate(adminupdates[i].LeaveFrom) + '</span> to <span class="highLightLeave">' + lr.functions.FormatDate(adminupdates[i].LeaveTill) + '</span>' +
                            '<span> <a class="AdminApproveLeave" title="Accept applied leave" value="' + adminupdates[i].Id + '">Accept</a> <a value="' + adminupdates[i].Id + '" class="AdminRejectLeave" ' +
                            'title="Reject applied leave">Reject</a></span><br/><hr>';
                    }
                }
            } else {
                updatesHtml += 'There are no leave request from employee.<br/><br/><hr>';
            }
        };
        updatesHtml += '<span style="font-weight:bold;">Applied Leave Status</span><br/><br/>';
        if (employeeUpdates.length > 0) {
            for (var j = 0; j < employeeUpdates.length; j++) {
                switch (employeeUpdates[j].LeaveStatus) {
                    case 'Waiting admin response':
                        {
                            if (lr.functions.FormatDate(employeeUpdates[j].LeaveTill) == "1/1/0001") {
                                updatesHtml += 'You have applied <span class="highLightLeave">' + employeeUpdates[j].LeaveType + '</span> for <span class="highLightLeave">' + lr.functions.FormatDate(employeeUpdates[j].LeaveFrom) +
                                    ' .The leave status is <span class="highLightLeave">' + '<u>' + employeeUpdates[j].LeaveStatus + '</u></span><br/><hr>';
                            } else {
                                updatesHtml += 'You have applied <span class="highLightLeave">' + employeeUpdates[j].LeaveType + '</span> from <span class="highLightLeave">' + lr.functions.FormatDate(employeeUpdates[j].LeaveFrom) + '</span> to' +
                                    ' <span class="highLightLeave">' + lr.functions.FormatDate(employeeUpdates[j].LeaveTill) + '</span>. The leave status is <span class="highLightLeave">' +
                                    '<u>' + employeeUpdates[j].LeaveStatus + '</u></span><br/><hr>';
                            }
                            break;
                        }
                    default:
                        {
                            if (employeeUpdates[j].Leavetill == "" || lr.functions.FormatDate(employeeUpdates[j].LeaveTill) == "1/1/0001") {
                                updatesHtml += 'You have applied <span class="highLightLeave">' + employeeUpdates[j].LeaveType + '</span> for <span class="highLightLeave">' + lr.functions.FormatDate(employeeUpdates[j].LeaveFrom) +
                                    '. The leave status is <span class="highLightLeave"><u style="color:green;">' + '' + employeeUpdates[j].LeaveStatus + '</u></span> on <span class="highLightLeave">' + lr.functions.FormatDate(employeeUpdates[j].LeaveApproved) + '.</span><br/><hr>';
                            } else {
                                updatesHtml += 'You have applied <span class="highLightLeave">' + employeeUpdates[j].LeaveType + '</span> from <span class="highLightLeave">' +
                                    '' + lr.functions.FormatDate(employeeUpdates[j].LeaveFrom) + '</span> to <span class="highLightLeave">' + lr.functions.FormatDate(employeeUpdates[j].LeaveTill) + '</span>. Leave has been  ' +
                                    '<span class="highLightLeave"><u style="color:green;">' + employeeUpdates[j].LeaveStatus + '</u></span> on <span class="highLightLeave">' + lr.functions.FormatDate(employeeUpdates[j].LeaveApproved) + '.</span><br/><hr>';
                            }
                        }
                }
            }
        } else {
            updatesHtml += 'There are no applied leave from your side.<br/><br/>';
        }
        return updatesHtml;
    },
    HowToUse: function () {
        var fillYourAttendance = '<ul>' +
            '<li>Please follow the below steps...</li> <li>Please click on Attendance tab.</li><li>If you haven\'t fill your In time attendance then click on Present button.</li>' +
            '<li>If you have filled your in time attendance then you will able to fill your out time attendance. In order to fill your out time attendance please click on Present button.</li>' +
            '<li>Once you fill your in time attendance or out time attendance then you can not change your in time or out time attendance by yourself.</li><li>If you wanted to edit your attendance status, you need to request your Admin. </li></ul>';
        var applyForLeave = '<ul><li>You can apply one day or more than one day leave. Here leave will count only week days.</li><li>In order to apply leave please click on Leave tab.</li>' +
            '<li>To apply one day leave, select from date and choose your leave type then click on Apply for Leave button.</li><li>To apply more than one day leave, select from date after that please checked the check box then choose To date and choose your leave type then click on Apply for Leave button.</li>' +
            '<li>Your leave request will be sent to admin.</li><li>You can see your applied leave status updates on Home page.</li></ul>';
        var getYourDetails = '<ul><li>In order to check your details please click on Details Tab.</li><li>You can check your attendance status of any of the previous week day.</li>' +
            '<li>You can check your left leaves by selecting your leave type.</li> <li>You can download your attendance register by selecting month.</li></ul>';
        $("#" + lr.ids.FillYourAttendance).html(fillYourAttendance);
        $("#" + lr.ids.ApplyYourLeave).html(applyForLeave);
        $("#" + lr.ids.GetYourDetails).html(getYourDetails);
    },
    Attendance: function (data) {
        var inTime = data.InTime,
            outTime = data.OutTime;
        var currentTime = lr.functions.CurrentTime();
        var toDaysDate = lr.functions.TodaysDate();
        $("#" + lr.ids.TodaysDate).html(toDaysDate);
        var attendanceLeftPanelData = '';
        var attendanceRightPanelData = '';
        if (inTime == null) {
            attendanceLeftPanelData += '<div class="attendance-panel-body"><ul> <li>You haven\'t filled your in time attendance yet.<b> Please fill your in time attendance.</b></li></ul>' +
                '<table class="table AttendanceInTimeTable"><tbody><tr class="text-center"><td><span>Time: </span></td><td><span id="CurrentTimeContainer">' + currentTime + '</span></td></tr><tr class="text-center">' +
                '<td><span>Click on button to complete your attendance</span></td><td><button class="btn btn-success btn-md text-center" title="Click to complete your attendance" id="InTimeSubmitButton">Present</button></td></tr></tbody></table></div>';
        } else {
            if ($.trim(data.Status) == "Present") {
                attendanceLeftPanelData += '<div class="attendance-panel-body"><ul> <li>You have completed your in time attendance at <b>' + lr.functions.FormatTime(inTime) + '</b>.</li></ul>';
            } else {
                attendanceLeftPanelData += '<div class="attendance-panel-body"><ul> <li>You are on leave as <b>' + $.trim(data.Status) + '</b> todaye.</li></ul>';  
            }
        }
        $("#" + lr.ids.AttendanceLeftPanelBody).html(attendanceLeftPanelData);
        if (inTime == null && outTime == null) {
            attendanceRightPanelData += '<div class="attendance-panel-body"><ul> <li>In order to fill <b>out time</b> attendance, please fill your in time attendance first.</li></ul>';
        } else {
            if (inTime != null && outTime == "" && $.trim(data.Status) == "Present") {
                attendanceRightPanelData += '<div class="attendance-panel-body"><ul><li>You haven\'t filled your out time attendance yet.<b> Please fill your out time attendance.</b></li></ul>' +
                    '<table class="table AttendanceInTimeTable"><tbody><tr class="text-center"><td><span>Time: </span></td><td><span id="CurrentTimeContainer">' + currentTime + '</span></td></tr><tr class="text-center"><td>' +
                    '<span>Click on button to complete your attendance</span></td><td><button class="btn btn-success btn-md text-center" title="Click to complete your attendance" id="OutTimeSubmitButton">Present</button></td></tr></tbody></table></div>';
            }
            else {
                if ($.trim(data.Status) == "Present") {
                    attendanceRightPanelData += '<div class="attendance-panel-body"><ul> <li>You have completed your out time attendance at <b>' + lr.functions.FormatTime(outTime) + '</b>.</li></ul>';
                } else {
                    attendanceRightPanelData += '<div class="attendance-panel-body"><ul> <li>You are on leave as <b>' + $.trim(data.Status) + '</b> todaye.</li></ul>';
                }
            }
        }
        $("#" + lr.ids.AttendanceRightPanelBody).html(attendanceRightPanelData);
    }
};