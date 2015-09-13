var lr = $.utils = {};
/*prepare html*/
lr.prepareHtml = {
    LoginPage: '<div class="container" id="LeaveRegisterContainer"><form class="form-signin">' +
        '<div><label id="NotYetUser">Not a user yet?<a id="SignUpFromLogin" title="Click to sign up">Sign Up</a></label></div>' +
        '<input type="email" id="LoginEmailIdAddress" title="Please enter your email address" style="margin-bottom: 10px;border-radius: 4px;" class="form-control" placeholder="Email address" autofocus="">' +
        '<input type="password" id="LoginPassword" class="form-control" title="Please enter your password" placeholder="Password">' +
        '<div><label><a id="ForgotPasswordLink" title="Recover your password">Forgot Password?</a></label></div>' +
        ' <button class="btn btn-lg btn-primary btn-block" id="LoginButton" title="Click to sign in" type="submit">Sign in</button></form></div>',
    SignUp: '<div class="container" id="LeaveRegisterContainer"><form class="form-signin">' +
        ' <div><label id="NotYetUser">Already exist?<a id="SignInLink" title="Click to sign in">Sign in</a></label></div>' +
        '<input type="email" title="Please enter your email address" id="RegistrationEmailIdAddress" class="form-control SignUpControl" placeholder="Email address" required="" autofocus="">' +
        '<input type="text" title="Please enter your employee id" id="SignUpEmpId" placeholder="Employee Id" class="form-control SignUpControl" required="" autofocus="">' +
        '<input type="password" maxlength="16" title="Please enter your password" id="SignUpPassword" placeholder="Password" class="form-control" required="" autofocus="">' +
        '<input type="password" maxlength="16" title="Please re-enter your password" id="SignUpRePassword" placeholder="Re-enter Password" class="form-control" required="" autofocus="">' +
        ' <button class="btn btn-lg btn-primary btn-block" id="SignUpButton" title="Click to sign up" type="submit">Sign up</button></form></div>',
    ForgotPassword: '<div class="container" id="LeaveRegisterContainer"><form class="form-signin">' +
        '<label for="inputEmail">Recover your password</label><label id="KnowPassword"><a id="SignInLink" href title="Click to sign in">Sign in</a></label>' +
        '<input type="email" title="Please enter your email address" id="RecoverPasswordEmailId" class="form-control" placeholder="Email address" required="" autofocus="">' +
        '<button class="btn btn-lg btn-primary btn-block" id="RecoverPasswordButton" title="Click to recover your password" type="submit">Recover Password</button>' +
        '</form></div>',
    Home: '<nav id="LeaveRegisterHeader" class="navbar navbar-inverse navbar-fixed-top"><div class="container-fluid"><div class="row"><div class="col-md-4">' +
        '<div class="navbar-header"> <button type="button" class="navbar-toggle collapsed" data-toggle="collapse" data-target="#navbar" aria-expanded="false" aria-controls="navbar">' +
        '<span class="sr-only">Toggle navigation</span><span class="icon-bar"></span><span class="icon-bar"></span><span class="icon-bar"></span></button>' +
        '<a id="CompanyNameTitle" class="navbar-brand" title="Attendance &amp; Leave Register">Attendance &amp; Leave Register</a></div></div><div class="col-md-4">' +
        '<div id="navbar" class="navbar-collapse collapse" aria-expanded="false" style="height: 1px;"><ul class="nav navbar-nav navbar-right">' +
        '<li id="Home" class="Tabs"><a id="HomeMenue" class="MenuTabs" title="To know your\'s updae,news and attendance click on Home">Home</a></li>' +
        '<li id="HowToUse" class="Tabs"><a id="HowToUseMenue" title="To know how to use Leave Register click on How to use" class="MenuTabs">How to use</a></li>' +
        '<li id="Attendance" class="Tabs"><a id="AttendanceMenue" title="To fill your attendance click on Attendnace" class="MenuTabs">Attendance</a></li>' +
        '<li id="Leave" class="Tabs"><a id="LeaveMenue" title="To apply for leave click on Leave" class="MenuTabs">Leave</a></li>' +
        '<li id="Details" class="Tabs"><a id="DetailsMaenu" class="MenuTabs" title="To check your attendance and leave details click on Details">Details</a></li>' +
        '</ul></div></div><div id="ProfileInfo" class="col-md-4"><div class="dropdown MenuTabs pull-right"><a class="dropdown-toggle MenuTabs" data-toggle="dropdown" role="button" aria-haspopup="true" aria-expanded="false">' +
        '<span id="UserName" title="Name">Nirankar Panday </span><span class="caret"></span></a><ul class="dropdown-menu">' +
        '<li><a class="LinkTabs" id="LinkAdmin" title="To manage Attendance &amp; Lave Register click on Admin link">Admin</a></li><li><a id="EditProfileLink" class="LinkTabs" title="In order to edit and view your profile click on Edit Profile link">Manage your profile</a></li>' +
        '<li><a class="LinkTabs" title="In order to change profile password click on Change Password link" id="ChangeProfilePasswordLink">Change Password</a></li>' +
        '<li><a class="LinkTabs" title="In order to logout from account click on Logout link" id="LogOutLink">Logout</a></li></ul></div>' +
        '<img alt="Profile Image" class="pull-right" title="Profile Image" id="ProfileImage" src="Layouts/LeaveRegister/Images/Nirankar.jpg"></div></div></div></nav>' +
        '<div class="container-fluid" id="LeaveRegisterContainer"><div class="panel panel-default" style="padding: 6px;"><div class="row"><div class="col-md-4"><div class="panel panel-default"><div class="panel-heading panel-title HeaderStyles">Updates</div>' +
        '<div id="UpdatesPanelBody" style="background-color: white;border-left: 1px solid beige;border-right: 1px solid beige;float: left;width:100%;height: 500px;overflow-y: auto;overflow-x: hidden;" class="panel-body "></div></div></div><div class="col-md-4 "><div class="panel panel-default "><div class="panel-heading panel-title HeaderStyles ">Attendance</div>' +
        '<div id="AttendancePanelBody" class="panel-body" style="background-color: white;border-right: 1px solid beige;float: left;width:100%;height: 500px;border-left: 1px solid beige;overflow-y: auto;overflow-x: hidden;"></div></div></div><div class="col-md-4"><div class="panel panel-default">' +
        '<div class="panel-heading panel-title HeaderStyles">News</div> <div id="NewsPanelBody" style="background-color: white;width:100%;border-left: 1px solid beige;float: left;height: 500px;overflow-y: auto;overflow-x: hidden;"' +
        ' class="panel-body"></div></div></div></div></div></div>' +
        '<div id="LeaveRegisterFooter" class="container-fluid"><p class="text-muted text-center">Powered by NjayCorporation</p></div>',
    HowToUse: '<div class="container-fluid" id="LeaveRegisterContainer"><div class="panel panel-default" style="padding: 6px;"><div class="row"><div class="col-md-4"><div class="panel panel-default"><div class="panel-heading panel-title HeaderStyles">Fill your attendance</div>' +
        '<div id="FillYourAttendance" class="panel-body "style="background-color: white;border-left: 1px solid beige;border-right: 1px solid beige;float: left;width:100%;height: 500px;overflow-y: auto;overflow-x: hidden;"></div></div></div><div class="col-md-4 "><div class="panel panel-default "><div class="panel-heading panel-title HeaderStyles ">Apply for leave</div>' +
        '<div id="ApplyYourLeave" class="panel-body" class="panel-body" style="background-color: white;border-right: 1px solid beige;float: left;width:100%;height: 500px;border-left: 1px solid beige;overflow-y: auto;overflow-x: hidden;"></div></div></div><div class="col-md-4"><div class="panel panel-default"><div class="panel-heading panel-title HeaderStyles">Get your details</div>' +
        '<div id="GetYourDetails" class="panel-body" style="background-color: white;border-right: 1px solid beige; width:100%;border-left: 1px solid beige;float: left;height: 500px;overflow-y: auto;overflow-x: hidden;"></div></div></div></div></div></div>',
    Attendance: '<div class="container-fluid" id="LeaveRegisterContainer"><div class="panel panel-default"><div class="panel-heading panel-title text-center HeaderStyles">' +
        '<span>Fill your attendance</span><span style="color:#826B6B;"> (</span><span id="TodaysDate" style="color:#826B6B;">Sat Jul 11 2015</span><span style="color:#826B6B;">)</span>' +
        '</div><div class="panel-body"><div class="row"><div class="col-md-6"><div class="panel panel-default"><div class="panel-heading HeaderStyles">Fill your in time attendance</div>' +
        '<div class="panel-body" id="AttendanceLeftPanelBody">' +
        '</div></div></div><div class="col-md-6"><div class="panel panel-default"><div class="panel-heading HeaderStyles ">Fill your out time attendance</div>' +
        '<div class="panel-body" id="AttendanceRightPanelBody"></div></div></div></div></div></div></div>',
    Leave: '<div class="container" id="LeaveRegisterContainer"><div class="panel panel-default"><div class="panel-heading panel-title text-center HeaderStyles"><span>Apply for your leave</span>' +
        '</div><div class="panel-body ApplyLeavePanelBody"><table class="table text-center" id="ApplyLeavePanelBodyTable" style="border: 2px solid #ddd"><tbody><tr style="height:63px">' +
        '<td class="text-right">From</td><td class="text-left"><input id="FromDate" class="EditAttendanceControl" type="date" title="Select from date" autofocus="">' +
        '</td><td class="text-right"><input class="checkbox" type="checkbox" id="EnableToDateControl" title="To enable the to date please checked the checkbox"></td>' +
        '<td class="text-left"><input id="ToDate" class="EditAttendanceControl" type="date" title="Select to date" disabled=""></td></tr><tr><td colspan="2"><span class="SelectLeaveType">Select leave type</span></td>' +
        '<td colspan="2" class="text-left"><select class="form-control" id="LeaveType" title="Select leave type"><option>Select Leave Type</option><option>Casual Leave</option>' +
        '<option>Sick Leave</option><option>Half Day Leave</option><option>Earned day Leave</option></select></td></tr><tr><td colspan="2"><span class="ApplyYourPlannedLeave">Apply your planned leave</span></td>' +
        '<td colspan="2" class="text-left"><button class="btn btn-success" id="ApplyLeaveButton" title="Apply your leave">Apply For Leave</button></td></tr></tbody></table>' +
        '<hr><h4 style="text-align:center;"><u>Company Leave Table</u></h4><table title="Company Leave Table" class="table text-center" id="LeaveTableForEmployee" style="border: 2px solid #ddd"><thead><th>Date</th><th>Day</th><th>Reason</th></thead></table></div></div></div>',
    Details: '<div class="container" id="LeaveRegisterContainer"><div class="panel panel-default"><div class="panel-heading panel-title text-center HeaderStyles">' +
        '<span>Check your attendance &amp; leave details</span></div><div class="panel-body DetailsPanelBody"><div class="row DetailsRow" id="GetAttendanceDetailsRow">' +
        '<span class="DetailsLabel" id="GetAttendanceDetails" title="Get your attence details"><b>Get your attendance details</b></span><div class="row">' +
        '<div id="GetAttendanceDetailsDescription" class="MarginTopTwoPixel"><div class="col-md-4"><input id="DateToKnowAttendance" autofocus class="DetailsControl" type="date" title="Select date to know your attendance">' +
        '</div><div class="col-md-4"><button class="btn btn-success" id="CheckYourAttendance" title="Check your attendance">Check your attendance</button></div></div></div>' +
        '<div id="StatusOfYourAttendance" class="Status"></div></div><div class="row DetailsRow" id="GetLeavesDetailsrow">' +
        '<span class="DetailsLabel" id="GetLeavesDetails" title="Get your leave details"><b>Get your leave details</b></span><div class="MarginTopTwoPixel" id="GetLeavesDetailsDescription">' +
        '<div class="row"><div class="col-md-4"><select class="DetailsControl" id="TypeToKnowLeave" title="Select leave type"><option>Select Leave Type</option><option>Casual Leave</option><option>Sick Leave</option><option>Half Day Leave</option><option>Earned day Leave</option>' +
        '</select></div><div class="col-md-4"><button class="btn btn-success DetailsButton" id="CheckYourLeaveStatus" title="Check your leave status">Check your leave</button>' +
        '</div></div></div><div id="StatusOfYourSelectedLeave" class="Status"></div></div><div class="row DetailsRow" id="GetAttendanceRegisterRow">' +
        '<span class="DetailsLabel" id="GetAttendanceRegister" title="Get your attendance register"><b>Get your attendance register</b></span>' +
        '<div class="MarginTopTwoPixel" id="GetAttendanceRegisterDescription"><div class="row"><div class="col-md-4"><select class="DetailsControl" id="TypeOfMonth" title="Select month">' +
        '<option>Select Month</option><option>January</option><option>February</option><option>March</option><option>April</option><option>May</option><option>June</option>' +
        '<option>July</option><option>August</option><option>September</option><option>October</option><option>November</option><option>December</option></select>' +
        '</div><div class="col-md-4"><button class="btn btn-success DetailsButton" id="GetLeaveregister" title="Get your selected leave register">Get leave register</button>' +
        '</div></div></div><div id="LeaveRegisterTable" title="Leave register"></div></div></div></div></div>',
    ManageYourProfile: '<div class="container" id="LeaveRegisterContainer"><div class="panel panel-default"><div class="panel-heading panel-title text-center HeaderStyles">' +
        '<span>View &amp; Edit your profile information</span></div><div class="panel-body DetailsPanelBody"><div class="row"><div class="col-md-6"><div id="PhotoContainer">' +
        '<img id="ImageProfileChange" class="UploadNewPic" alt="Profile Image" src="Layouts/LeaveRegister/Images/Nirankar.jpg"><input class="UploadNewPic" id="fileinput" type="file" multiple="false">' +
        '<span class="ProgressOutPut"><progress id="ImageProgressBar" class="UploadNewPic" title="change your profile picture"></progress><span id="UploadedImageStatus"></span>' +
        '</span><span id="UploadImageButtonContainer" class="UploadNewPic"><button class="btn btn-success" id="UploadImageButton" title="Upload your selected mage">Upload Profile Image</button></span>' +
        '</div></div><div class="col-md-6"><div id="PrfileInfoContainer"><table class="table borderless" id="ProfileInfoTable"><tbody><tr><td>First Name</td>' +
        '<td><span id="EmpFirstName" class="EnterProfileInfo"/></td></tr><tr><td>Last Name</td><td>' +
        '<span id="EmpLastName" class="EnterProfileInfo"/></td></tr><tr><td>DOB</td><td><input type="Date" id="EmpDOB" class="EnterProfileInfo Date" title="Edit your date of birth">' +
        '</td></tr><tr><td>Employee Id</td><td><span id="EmpID" class="EnterProfileInfo"/></td></tr><tr>' +
        '<td>Designation</td><td><span id="EmpDesignation" class="EnterProfileInfo"/></td></tr><tr><td>Date of joining</td>' +
        '<td><span id="EmpDateOfJoining" class="EnterProfileInfo Date"/></td></tr><tr><td>Company</td>' +
        '<td><span id="EmpCompany" class="EnterProfileInfo"/></td></tr><tr><td colspan="2">' +
        '<button class="btn btn-success EnterProfileInfo" id="ProfileInfoSumbit" title="Save your changes">Save your entered profile information</button>' +
        '</td></tr></tbody></table></div></div></div></div></div></div>',
    ChangePassword: '<div class="container" id="LeaveRegisterContainer"><div class="panel panel-default"><div class="panel-heading panel-title text-center HeaderStyles">' +
        '<span>Change your password</span></div><div class="panel-body DetailsPanelBody"><div class="row text-center col-sm-offset-3  MarginTop"><div class="col-md-4">' +
        '<span id="CurrentPassword" class="LabelForPasswordChange"><b>Current password</b></span></div><div class="col-md-4"><input maxlength="16" type="password" autofocus id="CurrentPasswordInputControl" class="PasswordInput" title="Please enter your current password">' +
        '</div></div><div class="row text-center col-sm-offset-3"><div class="col-md-4"><span id="NewPassword" class="LabelForPasswordChange"><b>New password</b></span>' +
        '</div><div class="col-md-4"><input maxlength="16" id="NewPasswordInputControl" type="password" class="PasswordInput" title="Please enter your new password">' +
        '</div></div><div class="row text-center col-sm-offset-3"><div class="col-md-4"><span id="ReEnterPassword" class="LabelForPasswordChange"><b>Re-enter new password</b></span>' +
        '</div><div class="col-md-4"><input maxlength="16" id="ReEnterPasswordInputControl" type="password" class="PasswordInput" title="Please re-enter your new password">' +
        '</div></div><div class="row text-center col-sm-offset-3"><div class="col-md-4"></div><div class="col-md-4"><button id="ChangePasswordButton" class="btn btn-success PasswordInput" title="Please click to save your changes">Change password</button>' +
        '</div></div></div></div></div>',
    AdminConfigureAttendance: '<div class="container" id="LeaveRegisterContainer"><div class="panel panel-default"><div class="panel-heading panel-title text-center HeaderStyles">' +
        '<span id="AdminModelHeaderText">Admin (Configure Attendance)</span></div><div class="panel-body DetailsPanelBody"><div class="col-md-3" id="LeftAdminPanelBody"><ul class="nav nav-pills nav-stacked"><li id="ConfigureAttendance" class="active"><a id="ConfigureAttendanceMenu" title="Configure attendance">Configure Attendance</a></li>' +
        '<li id="RegisterCompanyLeave"><a id="RegisterCompanyLeaveMenu"  title="Register your company leave">Company Leave</a></li>' +
        '<li id="RegisterCompanyEmployee"><a id="RegisterCompanyEmployeeMenu" title="Register your company employee">Employee Registration</a></li>' +
        '<li id="EditAttendanceRegister"><a id="EditAttendanceRegisterMenu" title="Edit attendance register">Edit Attendance Register</a></li>' +
        '<li id="CompanyNews"><a id="CompanyNewsMenu" title="Add &amp; Remove Company News">Company News</a></li></ul></div><div class="col-md-9 admincontainer text-center" id="RightAdminPanelBody">' +
        '<div class="row ConfigurLeaves"><div class="col-md-3">Total casual leave</div><div class="col-md-3"><input class="SetLeaveInputBox" autofocus="" title="Set totla number of casual leaves" max="30" min="0" type="number" id="TotalCasulaLeave">' +
        '</div></div><div class="row ConfigurLeaves"><div class="col-md-3">Total sick leave</div><div class="col-md-3"><input class="SetLeaveInputBox" title="Set totla number of sick leaves" max="30" min="0" type="number" id="TotalSickLeave">' +
        '</div></div><div class="row ConfigurLeaves"><div class="col-md-3">Total working hours</div><div class="col-md-3">' +
        '<input class="SetLeaveInputBox" title="Set working hours" max="18" min="0" type="number" id="TotalWorkingHours"></div></div><div class="row ConfigurLeaves">' +
        '<div class="col-md-3"><button class="btn btn-success" title="Click to enable edit mode" id="EditLeaveConfiguration">Edit setting value</button>' +
        '</div><div class="col-md-3"><button class="btn btn-success" title="Save the settings" id="SetTotalLeaves">Save the settings</button></div></div></div></div></div></div>',
    AdminCompanyLeave: '<div class="col-md-9 admincontainer" id="RightAdminPanelBody"><div class="row text-center"><table id="LeaveTables" title="Leave Table" class="table .table-hover .table-bordered">' +
        '<thead><tr><th class="CompanyLeaveTableHeader text-center">Date</th><th class="CompanyLeaveTableHeader text-center">Day</th><th class="CompanyLeaveTableHeader text-center">Leave Description</th>' +
        '<th class="CompanyLeaveTableHeader text-center">Actions</th></tr></thead><tbody></tbody></table></div><div class="row pull-right">' +
        '<span class="btn-link link CompanyLeaveRowAction" id="CompanyLeaveAddNewEmpLinkText" title="Add Leave" style="font-size: 18px;color: black;">Add company leave</span></div>' +
        '<div class="modal" id="MyModal" style="display: none;"><div class="modal-dialog"><div class="modal-content"><div class="modal-header"><button type="button" title="Close" class="close" data-dismiss="modal" aria-hidden="true">' +
        '×</button><h4 class="modal-title">Add Company Leave</h4></div><div class="modal-body" style="height:251px;"><div class="AddLeave"><span class="AddLeaveLabel">Date<span class="Madatory">*</span></span><span class="AddLeaveLabelControl"><input class="AddLeaveControl" id="AddLeaveControlDate" title="Choose date" type="date">' +
        '</span></div><div class="AddLeave"><span class="AddLeaveLabel">Day<span class="Madatory">*</span></span><span class="AddLeaveLabelControl"><span class="AddLeaveControl" id="AddLeaveControlDay"/>' +
        '</span></div><div class="AddLeave"><span class="AddLeaveLabel">Description<span class="Madatory">*</span></span><span class="AddLeaveLabelControl"><textarea class="AddLeaveControl" id="AddLeaveControlReason" title="Write leave reason" placeholder="Write leave reason" type="text"></textarea>' +
        '</span></div><span class="Madatory" id="AddCompanyLeaveValidationControls"> * indicates mandatory</span></div><div class="modal-footer"><a class="btn btn-default" title="Close" data-dismiss="modal">Close</a><a id="AddLeaveButton" title="Click to add leave" class="btn btn-primary">Add Leave</a>' +
        '</div></div></div></div></div><input type="hidden" name="RowCompanyLeaveId" id="RowCompanyLeaveId" value=""/><div class="modal in" id="EditCompanyLeaveModal" style="display: none; padding-left: 17px;">' +
        '<div class="modal-dialog"><div class="modal-content"><div class="modal-header"><button type="button" title="Close" class="close" data-dismiss="modal" aria-hidden="true">' +
        '×</button><h4 class="modal-title">Edit Company Leave</h4></div><div class="modal-body" style="height: 251px;"><div class="AddLeave"><span class="AddLeaveLabel">Date<span class="Madatory">*</span></span><span class="AddLeaveLabelControl">' +
        '<input class="AddLeaveControl" id="EditLeaveControlDate" title="Change date" type="date"></span></div><div class="AddLeave"><span class="AddLeaveLabel">Day<span class="Madatory">*</span></span>' +
        '<span class="AddLeaveLabelControl"><span class="AddLeaveControl" id="EditLeaveControlDay"/></span></div><div class="AddLeave"><span class="AddLeaveLabel">Description<span class="Madatory">*</span>' +
        '</span> <span class="AddLeaveLabelControl"><textarea class="AddLeaveControl" id="EditLeaveControlReason" title="Edit leave reason" placeholder="Edit leave reason" type="text"></textarea>' +
        '</span></div><span class="Madatory" id="EditCompanyLeaveValidationControls"> * indicates mandatory</span></div><div class="modal-footer"><a class="btn btn-default" title="Close" data-dismiss="modal">Close</a><a id="EditLeaveButton" title="Click to add leave" class="btn btn-primary">Save Changes</a>' +
        '</div></div></div></div>',
    AdminEmployeeRegistration: '<div class="col-md-9 admincontainer" id="RightAdminPanelBody"><div class="row text-center"><table id="EmployeeTable" title="Employee Table" class="table .table-hover .table-bordered">' +
        '<thead><th class="CompanyLeaveTableHeader text-center">First Name</th><th class="CompanyLeaveTableHeader text-center">Employee ID</th><th class="CompanyLeaveTableHeader text-center">Email ID</th>' +
        '<th class="CompanyLeaveTableHeader text-center">Actions</th></thead></table></div><div class="row pull-right">' +
        '<span class="btn-link CompanyLeaveRowAction" id="EmploayeeRegistrationAddNewEmpLinkText" title="Add Employee" style="font-size: 18px;color: black;">Add Employee</span></div>' +
        '<div class="modal" id="MyModal" style="display: none;"><div class="modal-dialog"><div class="modal-content"><div class="modal-header"><button type="button" title="Close" class="close" data-dismiss="modal" aria-hidden="true">' +
        '×</button><h4 class="modal-title">Add Employee</h4></div><div class="modal-body"><div class="AddLeave"><span class="AddLeaveLabel">First Name<span class="Madatory">*</span></span><span class="AddLeaveLabelControl">' +
        '<input type="text" id="FirstNameInputControl" class="AddEmployeeControl" placeholder="First Name" title="Please enter employee\'s first name"></span></div>' +
        '<div class="AddLeave"><span class="AddLeaveLabel">Last Name<span class="Madatory">*</span></span><span class="AddLeaveLabelControl"><input type="text" id="LastNameInputControl" class="AddEmployeeControl" placeholder="Last Name" title="Please enter employee\'s last name">' +
        '</span></div><div class="AddLeave"><span class="AddLeaveLabel">Employee Id<span class="Madatory">*</span></span><span class="AddLeaveLabelControl"><input type="text" id="EmployeeIdInputControl" placeholder="Employee Id" style="margin-left: -9px;" class="AddEmployeeControl" title="Please enter employee\'s Id">' +
        '</span></div><div class="AddLeave"><span class="AddLeaveLabel">Email Id<span class="Madatory">*</span></span><span class="AddLeaveLabelControl"><input type="text" id="EmailIdInputControl" class="AddEmployeeControl" placeholder="Email Id" title="Please enter employee\'s email id">' +
        '</span></div><div class="AddLeave"><span class="AddLeaveLabel">Designation<span class="Madatory">*</span></span><span class="AddLeaveLabelControl"><input type="text" id="DesignationInputControl" class="AddEmployeeControl" placeholder="Designation" title="Please enter employee\'s designation">' +
        '</span></div><div class="AddLeave"><span class="AddLeaveLabel" title="Date of joining">DOJ<span class="Madatory">*</span></span><span class="AddLeaveLabelControl"><input type="date" id="DOJInputControl" class="AddEmployeeControl" title="Please choose employee\'s date of joining">' +
        '</span></div><div class="AddLeave"><span class="AddLeaveLabel">Company<span class="Madatory">*</span></span><span class="AddLeaveLabelControl"><input type="text" id="CompanyInputControl" class="AddEmployeeControl" placeholder="Company Name" title="Please enter company name">' +
        '</span></div><span class="Madatory" id="AddEmployeeValidationControls"> * indicates mandatory</span></div><div class="modal-footer"><a class="btn btn-default" title="Close" data-dismiss="modal">Close</a><a id="AddEmployeeButton" title="Click to add employee" class="btn btn-primary">Add Employee</a>' +
        '</div></div></div></div><div class="modal in" id="ViewDetailsModal" style="display: none; padding-left: 17px;">' +
        '<div class="modal-dialog"><div class="modal-content"><div class="modal-header"><button type="button" title="Close" class="close" data-dismiss="modal" aria-hidden="true">' +
        '×</button><h4 class="modal-title">Employee Details</h4></div><div class="modal-body"><div class="AddLeave"><label class="AddLeaveLabel">First Name</label><span class="AddLeaveLabelControl">' +
        '<span id="ViewFirstNameInputControl" class="ViewEmplayeesControl"></span></span></div><div class="AddLeave"><label class="AddLeaveLabel">Last Name</label><span class="AddLeaveLabelControl">' +
        '<span id="ViewSecondNameInputControl" class="ViewEmplayeesControl">Neeraj</span></span></div>' +
        '<div class="AddLeave"><label class="AddLeaveLabel">' +
        'Employee Id</label><span class="AddLeaveLabelControl"><span id="ViewEmployeeIdInputControl" class="ViewEmplayeesControl">Neeraj</span></span></div><div class="AddLeave">' +
        '<label class="AddLeaveLabel">Email Id</label><span class="AddLeaveLabelControl"><span id="ViewEmailIdInputControl" class="ViewEmplayeesControl">Neeraj</span></span>' +
        '</div><div class="AddLeave"><label class="AddLeaveLabel">Designation</label><span class="AddLeaveLabelControl"><span id="ViewDesignationInputControl" class="ViewEmplayeesControl"></span>' +
        '</span></div><div class="AddLeave"> <label class="AddLeaveLabel" title="Date of joining">DOJ</label><span class="AddLeaveLabelControl"><span id="ViewDOJInputControl" class="ViewEmplayeesControl"></span>' +
        '</span></div><div class="AddLeave"><label class="AddLeaveLabel">Company</label><span class="AddLeaveLabelControl"><span id="ViewCompanyInputControl" class="ViewEmplayeesControl"></span>' +
        '</span> </div></div><div class="modal-footer"><a class="btn btn-default" title="Close" data-dismiss="modal">Close</a></div></div></div></div><div class="modal in" id="EditEmmployeeModal" style="display: none; padding-left: 17px;">' +
        '<div class="modal-dialog"><div class="modal-content"><div class="modal-header"><button type="button" title="Close" class="close" data-dismiss="modal" aria-hidden="true">' +
        '×</button><h4 class="modal-title">Edit Employee Informations</h4></div><div class="modal-body"><div class="AddLeave"><span class="AddLeaveLabel">First Name<span class="Madatory">*</span></span>' +
        '<span class="AddLeaveLabelControl"><input type="text" id="EditFirstNameInputControl" class="AddEmployeeControl" placeholder="First Name" title="Edit employee\'s first name">' +
        '</span></div><div class="AddLeave"><span class="AddLeaveLabel">Last Name<span class="Madatory">*</span></span><span class="AddLeaveLabelControl"><input type="text" id="EditLastNameInputControl" class="AddEmployeeControl" placeholder="Last Name" title="Edit employee\'s last name">' +
        '</span></div>' +
        '<div class="AddLeave"><span class="AddLeaveLabel">Employee Id</span><span class="AddLeaveLabelControl"><input type="text" id="EditEmployeeIdInputControl" class="AddEmployeeControl" title="Employee\'s employee id" disabled="">' +
        '</span></div><div class="AddLeave"><span class="AddLeaveLabel">Email Id</span><span class="AddLeaveLabelControl"><input type="text" id="EditEmailIdInputControl" class="AddEmployeeControl" title="Employee\'s employee id" disabled="">' +
        '</span></div><div class="AddLeave"><span class="AddLeaveLabel">Designation<span class="Madatory">*</span></span><span class="AddLeaveLabelControl"><input type="text" id="EditDesignationInputControl" class="AddEmployeeControl" placeholder="Designation" title="Edit employee\'s designation">' +
        '</span></div><div class="AddLeave"><span class="AddLeaveLabel" title="Date of joining">DOJ<span class="Madatory">*</span></span><span class="AddLeaveLabelControl"><input type="date" id="EditDOJInputControl" class="AddEmployeeControl" title="Change employee\'s date of joining">' +
        '</span></div><div class="AddLeave"><span class="AddLeaveLabel">Company<span class="Madatory">*</span></span><span class="AddLeaveLabelControl"><input type="text" id="EditCompanyInputControl" class="AddEmployeeControl" placeholder="Company Name" title="Edit company name">' +
        '</span></div><span class="Madatory" id="EditEmployeeValidationControls"> * indicates mandatory</span></div><div class="modal-footer"><a class="btn btn-default" title="Close" data-dismiss="modal">Close</a><a id="EditEmployeeButton" title="Click to save the changes" class="btn btn-primary">Save Changes</a>' +
        '</div></div></div><input type="hidden" name="RowId" id="RowId" value=""/></div>',
    AdminEditEmployeeAttendance: '<div class="col-md-9 admincontainer" id="RightAdminPanelBody"><div class="row ConfigurLeaves"><div class="col-md-3"><span id="SelectEmpIDtoEdit">Select Employee Id</span></div>' +
        '<div class="col-md-3"><select class="EditAttendanceControl" title="Select Employee ID" id="SelectEmpIDtoEditControls" autofocus=""><option>--Select--</option></select></div>' +
        '</div><div class="row ConfigurLeaves"><div class="col-md-3"><span id="SelectDateForEditYourAttendance">Select date</span></div>' +
        '<div class="col-md-3"><input class="EditAttendanceControl" title="Select Date" id="SelectDateToEditAttenceDate" type="date"></div></div>' +
        '<div class="row ConfigurLeaves"><div class="col-md-8"><span id="AttendanceStatusByAdmiin" style="font-style: italic;">' +
        '</span></div></div><div class="row ConfigurLeaves"><div class="col-md-3"><span id="ChangeYourAttendanceLabel">Change your attendance</span></div>' +
        '<div class="col-md-3"><select class="EditAttendanceControl" title="Select attendance status" id="ChangeYourAttendanceControls"><option>--Select--</option><option>Present</option><option>Absent</option><option>Casual Leave</option><option>Sick Leave</option><option>Half Day Leave</option><option>Earned day Leave</option></select></div>' +
        '</div><div class="row ConfigurLeaves"><div class="col-md-3"></div><div class="col-md-3"><button id="ChangeYourAttendanceButton" title="Save change attendance status" class="btn btn-success EditAttendanceControl">Save Changes</button></div></div></div>',
    AdminNews: '<div class="col-md-9 admincontainer" id="RightAdminPanelBody"><div class="row text-center"><table id="NewsTable" title="Company News" class="table .table-hover .table-bordered">' +
        '<thead><th class="CompanyLeaveTableHeader text-center">Title</th><th class="CompanyLeaveTableHeader text-center">Descrpition</th>' +
        '<th class="CompanyLeaveTableHeader text-center">Actions</th></tr></thead></table></div><div class="row pull-right"><span class="btn-link CompanyLeaveRowAction" id="AddNewsLinkText" title="Add News" style="font-size: 18px;color: black;">Add News</span>' +
        '</div><div class="modal" id="MyModal" style="display: none;"><div class="modal-dialog"><div class="modal-content"><div class="modal-header"><button type="button" title="Close" class="close" data-dismiss="modal" aria-hidden="true">' +
        '×</button><h4 class="modal-title">Add Company News</h4></div><div class="modal-body" style="height:200px;"><div class="AddLeave"><span class="AddLeaveLabel">Title<span class="Madatory">*</span></span><span class="AddLeaveLabelControl">' +
        '<input type="text" id="NewsTitleInputControl" class="AddLeaveControl" placeholder="News Title" title="Please enter news title"></span></div><div class="AddLeave"><span class="AddLeaveLabel">' +
        'Description<span class="Madatory">*</span></span><span class="AddLeaveLabelControl"><textarea class="AddLeaveControl" id="NewsDescription" title="Write news description" placeholder="Write leave reason" type="text"></textarea>' +
        '</span></div><span class="Madatory" id="AddNewsValidationControls"> * indicates mandatory</span></div><div class="modal-footer"><a class="btn btn-default" title="Close" data-dismiss="modal">Close</a><a id="AddNewsButton" title="Click to add News" class="btn btn-primary">Add News</a>' +
        '</div></div></div></div><input type="hidden" name="NewsId" id="NewsId" value=""/></div><div class="modal in" id="EditNewsModal" style="display: none; padding-left: 17px;">' +
        '<div class="modal-dialog"><div class="modal-content"><div class="modal-header"><button type="button" title="Close" class="close" data-dismiss="modal" aria-hidden="true">' +
        '×</button><h4 class="modal-title">Edit Company News</h4></div><div class="modal-body" style="height:200px;"><div class="AddLeave"><span class="AddLeaveLabel">' +
        'Title<span class="Madatory">*</span></span><span class="AddLeaveLabelControl"><input type="text" id="EditNewsTitleInputControl" class="AddLeaveControl" placeholder="News Title" title="Edit enter news title">' +
        '</span></div><div class="AddLeave"><span class="AddLeaveLabel">Description<span class="Madatory">*</span></span><span class="AddLeaveLabelControl"><textarea class="AddLeaveControl" id="EditNewsReasonControlReason" title="Write leave reason" placeholder="Edit new reason" type="text"></textarea>' +
        '</span></div><span class="Madatory" id="EditNewsValidationControls"> * indicates mandatory</span></div><div class="modal-footer"><a class="btn btn-default" title="Close" data-dismiss="modal">Close</a><a id="EditNewsButton" title="Click to add News" class="btn btn-primary">Save Changes</a>' +
        '</div></div></div></div>',
    ConfigureRightPanelAttendance: '<div class="col-md-9 admincontainer text-center" id="RightAdminPanelBody"><div class="row ConfigurLeaves"><div class="col-md-3">' +
        'Total casual leave</div><div class="col-md-3"><input class="SetLeaveInputBox" autofocus="" title="Set totla number of casual leaves" max="30" min="0" type="number" ' +
        'id="TotalCasulaLeave"></div></div><div class="row ConfigurLeaves"><div class="col-md-3">Total sick leave</div><div class="col-md-3"><input class="SetLeaveInputBox" ' +
        'title="Set totla number of sick leaves" max="30" min="0" type="number" id="TotalSickLeave"></div></div><div class="row ConfigurLeaves"><div class="col-md-3">Total ' +
        'working hours</div><div class="col-md-3"><input class="SetLeaveInputBox" title="Set working hours" max="18" min="0" type="number" id="TotalWorkingHours"></div></div><div ' +
        'class="row ConfigurLeaves"><div class="col-md-3"><button class="btn btn-success" title="Click to enable edit mode" id="EditLeaveConfiguration">Edit setting value</button>' +
        '</div><div class="col-md-3"><button class="btn btn-success" title="Save the settings" id="SetTotalLeaves">Save the settings</button></div></div></div>',
    HomeTabClick:'<div class="container-fluid" id="LeaveRegisterContainer"><div class="panel panel-default" style="padding: 6px;"><div class="row"><div class="col-md-4"><div class="panel panel-default"><div class="panel-heading panel-title HeaderStyles">' +
        'Updates</div><div id="UpdatesPanelBody" class="panel-body " style="background-color: white;border-left: 1px solid beige;border-right: 1px solid beige;float: left;width:100%;height: 500px;overflow-y: auto;overflow-x: hidden;">' +
        '</div></div></div><div class="col-md-4 "><div class="panel panel-default "><div class="panel-heading panel-title HeaderStyles">' +
        'Attendance</div><div id="AttendancePanelBody" class="panel-body" style="background-color: white;border-right: 1px solid beige;float: left;width:100%;height: 500px;border-left: 1px solid beige;overflow-y: auto;overflow-x: hidden;">' +
        '</div></div></div><div class="col-md-4"><div class="panel panel-default">' +
        '<div class="panel-heading panel-title HeaderStyles">News</div> <div id="NewsPanelBody" class="panel-body" style="background-color: white;width:100%;border-left: 1px solid beige;float: left;height: 500px;overflow-y: auto;overflow-x: hidden;"></div></div></div></div></div>',
    Alert: '<div class="modal" id="Alert"><div class="modal-dialog"><div class="modal-content"><div class="modal-header" id="AlertTitleBody"><h4 class="modal-title" id="AlertTitle"></h4></div>' +
        '<div class="modal-body" id="AlertBody"></div><div class="modal-footer"><a class="btn btn-default" data-dismiss="modal" id="AlertOkButton">OK</a></div></div></div></div></div>'
};
/*All ajax urls*/
lr.ajaxUrls = {
    HomeTab: 'Layouts/ASMXFiles/Home.asmx/HomeTab',
    ManageProfile: 'Layouts/ASMXFiles/Home.asmx/ManageProfile',
    SaveProfile: 'Layouts/ASMXFiles/Home.asmx/SaveProfile',
    ChangePassword: 'Layouts/ASMXFiles/Home.asmx/ChangePassword',
    SignIn: 'Layouts/ASMXFiles/Home.asmx/SignIn',
    RecoverPassword: 'Layouts/ASMXFiles/Home.asmx/RecoverPassword',
    SignUp: 'Layouts/ASMXFiles/Home.asmx/SignUp',
    AddEmployee: 'Layouts/ASMXFiles/Admin.asmx/AddEmployee',
    UpdateAddedEmployeeEditEmployee: 'Layouts/ASMXFiles/Admin.asmx/UpdateAddedEmployeeEditEmployee',
    AddCompanyLeave: 'Layouts/ASMXFiles/Admin.asmx/AddCompanyLeave',
    AcceptOrRejectAppliedLeave: 'Layouts/ASMXFiles/Admin.asmx/AcceptOrRejectAppliedLeave',
    AddNewsAndUpdate: 'Layouts/ASMXFiles/Admin.asmx/AddNewsAndUpdate',
    GetNewsAndUpdate: 'Layouts/ASMXFiles/Admin.asmx/GetNewsAndUpdate',
    UpdateNewsAndUpdate: 'Layouts/ASMXFiles/Admin.asmx/UpdateNewsAndUpdate',
    DeleteNewsAndUpdate: 'Layouts/ASMXFiles/Admin.asmx/DeleteNewsAndUpdate',
    GetCompanyLeave: 'Layouts/ASMXFiles/Admin.asmx/GetCompanyLeave',
    UpdateCompanyLeave: 'Layouts/ASMXFiles/Admin.asmx/UpdateCompanyLeave',
    GetAddedEmployee: 'Layouts/ASMXFiles/Admin.asmx/GetAddedEmployee',
    GetAttendanceStatus: 'Layouts/ASMXFiles/Admin.asmx/GetAttendanceStatus',
    DeleteAddedLeave: 'Layouts/ASMXFiles/Admin.asmx/DeleteAddedLeave',
    SetLeaveDays: 'Layouts/ASMXFiles/Admin.asmx/SetLeaveDays',
    GetLeaveDays: 'Layouts/ASMXFiles/Admin.asmx/GetLeaveDays',
    EditEmployeeAttendance: 'Layouts/ASMXFiles/Admin.asmx/EditEmployeeAttendance',
    ChangeAttendanceStatus: 'Layouts/ASMXFiles/Admin.asmx/ChangeAttendanceStatus',
    DetailesGetLeaveTypes: 'Layouts/ASMXFiles/Details.asmx/GetLeaveTypes',
    DetailesCheckAttendanceDetailes: 'Layouts/ASMXFiles/Details.asmx/CheckAttendanceDetailes',
    DetailesGetLeaveTypeDetailes: 'Layouts/ASMXFiles/Details.asmx/GetLeaveTypeDetailes',
    DetailesGetEmployeeAttendanceSheets: 'Layouts/ASMXFiles/Details.asmx/GetEmployeeAttendanceSheet',
    ApplyLeave: 'Layouts/ASMXFiles/Leave.asmx/ApplyLeave',
    LeaveTab: 'Layouts/ASMXFiles/Leave.asmx/LeaveTab',
    FillOutTimeAttendance: 'Layouts/ASMXFiles/Attendance.asmx/FillOutTimeAttendance',
    AttendanceTab: 'Layouts/ASMXFiles/Attendance.asmx/AttendanceTab',
    FillInTimeAttendance: 'Layouts/ASMXFiles/Attendance.asmx/FillInTimeAttendance',
    FillLunchTimeAttendance: 'Layouts/ASMXFiles/Attendance.asmx/FillLunchTimeAttendance'
};
lr.ids = {
    LoginEmailIdAddress: 'LoginEmailIdAddress',
    LoginPassword: 'LoginPassword',
    CompanyNameTitle:'CompanyNameTitle',
    CompanyName:'CompanyName',
    SignUpFromLogin: 'SignUpFromLogin',
    ForgotPasswordLink: 'ForgotPasswordLink',
    LoginButton: 'LoginButton',
    SignInFromForgotPassword: 'SignInFromForgotPassword',
    RecoverPasswordEmailId: 'RecoverPasswordEmailId',
    RecoverPasswordButton: 'RecoverPasswordButton',
    SignInLink: 'SignInLink',
    RegistrationEmailIdAddress: 'RegistrationEmailIdAddress',
    SignUpEmpId: 'SignUpEmpId',
    SignUpPassword: 'SignUpPassword',
    SignUpRePassword: 'SignUpRePassword',
    SignUpButton: 'SignUpButton',
    SessionId: 'SessionId',
    SessionValue: 'SessionValue',
    LoginContainer: 'LoginContainer',
    LeaveRegisterContainer: 'LeaveRegisterContainer',
    HomeMenue: 'HomeMenue',
    HowToUseMenue: 'HowToUseMenue',
    AttendanceMenue: 'AttendanceMenue',
    LeaveMenue: 'LeaveMenue',
    DetailsMaenu: 'DetailsMaenu',
    LinkAdmin: 'LinkAdmin',
    EditProfileLink: 'EditProfileLink',
    ChangeProfilePasswordLink: 'ChangeProfilePasswordLink',
    LogOutLink: 'LogOutLink',
    LeaveRegisterFooter: 'LeaveRegisterFooter',
    FromDate: 'FromDate',
    EnableToDateControl: 'EnableToDateControl',
    ToDate: 'ToDate',
    LeaveType: 'LeaveType',
    ApplyLeaveButton: 'ApplyLeaveButton',
    InTimeSubmitButton: 'InTimeSubmitButton',
    OutTimeSubmitButton: 'OutTimeSubmitButton',
    DateToKnowAttendance: 'DateToKnowAttendance',
    CheckYourAttendance: 'CheckYourAttendance',
    StatusOfYourAttendance: 'StatusOfYourAttendance',
    TypeToKnowLeave: 'TypeToKnowLeave',
    StatusOfYourSelectedLeave: 'StatusOfYourSelectedLeave',
    TypeOfMonth: 'TypeOfMonth',
    GetLeaveregister: 'GetLeaveregister',
    UserName: 'UserName',
    EmpFirstName: 'EmpFirstName',
    EmpLastName: 'EmpLastName',
    EmpDOB: 'EmpDOB',
    EmpID: 'EmpID',
    EmpDesignation: 'EmpDesignation',
    EmpDateOfJoining: 'EmpDateOfJoining',
    EmpCompany: 'EmpCompany',
    ProfileInfoSumbit: 'ProfileInfoSumbit',
    CurrentPasswordInputControl: 'CurrentPasswordInputControl',
    NewPasswordInputControl: 'NewPasswordInputControl',
    ReEnterPasswordInputControl: 'ReEnterPasswordInputControl',
    ChangePasswordButton: 'ChangePasswordButton',
    ConfigureAttendanceMenu: 'ConfigureAttendanceMenu',
    RegisterCompanyLeaveMenu: 'RegisterCompanyLeaveMenu',
    RegisterCompanyEmployeeMenu: 'RegisterCompanyEmployeeMenu',
    EditAttendanceRegisterMenu: 'EditAttendanceRegisterMenu',
    CompanyNewsMenu: 'CompanyNewsMenu',
    TotalCasulaLeave: 'TotalCasulaLeave',
    TotalSickLeave: 'TotalSickLeave',
    TotalWorkingHours: 'TotalWorkingHours',
    EditLeaveConfiguration: 'EditLeaveConfiguration',
    SetTotalLeaves: 'SetTotalLeaves',
    AdminModelHeaderText: 'AdminModelHeaderText',
    LeftAdminPanelBody: 'LeftAdminPanelBody',
    RightAdminPanelBody: 'RightAdminPanelBody',
    SelectEmpIDtoEditControls: 'SelectEmpIDtoEditControls',
    CompanyNews: 'CompanyNews',
    ConfigureAttendance: 'ConfigureAttendance',
    RegisterCompanyLeave: 'RegisterCompanyLeave',
    RegisterCompanyEmployee: 'RegisterCompanyEmployee',
    EditAttendanceRegister: 'EditAttendanceRegister',
    Home: 'Home',
    HowToUse: 'HowToUse',
    Attendance: 'Attendance',
    Leave: 'Leave',
    Details: 'Details',
    EmploayeeRegistrationAddNewEmpLinkText: 'EmploayeeRegistrationAddNewEmpLinkText',
    MyModal: 'MyModal',
    FirstNameInputControl: 'FirstNameInputControl',
    LastNameInputControl: 'LastNameInputControl',
    DOBInputControl: 'DOBInputControl',
    EmailIdInputControl: 'EmailIdInputControl',
    DesignationInputControl: 'DesignationInputControl',
    DOJInputControl: 'DOJInputControl',
    CompanyInputControl: 'CompanyInputControl',
    AddEmployeeButton: 'AddEmployeeButton',
    CompanyLeaveAddNewEmpLinkText: 'CompanyLeaveAddNewEmpLinkText',
    AddLeaveControlDate: 'AddLeaveControlDate',
    AddLeaveControlDay: 'AddLeaveControlDay',
    AddLeaveControlReason: 'AddLeaveControlReason',
    AddLeaveButton: 'AddLeaveButton',
    AddNewsLinkText: 'AddNewsLinkText',
    NewsTitleInputControl: 'NewsTitleInputControl',
    AddNewsButton: 'AddNewsButton',
    AlertTitleBody: 'AlertTitleBody',
    AlertBody: 'AlertBody',
    AlertTitle: 'AlertTitle',
    Alert: 'Alert',
    AlertOkButton: 'AlertOkButton',
    AttendancePanelBody: 'AttendancePanelBody',
    NewsPanelBody: 'NewsPanelBody',
    UpdatesPanelBody: 'UpdatesPanelBody',
    GetYourDetails: 'GetYourDetails',
    FillYourAttendance: 'FillYourAttendance',
    ApplyYourLeave: 'ApplyYourLeave',
    TodaysDate: 'TodaysDate',
    CurrentTimeContainer: 'CurrentTimeContainer',
    AttendanceLeftPanelBody: 'AttendanceLeftPanelBody',
    AttendanceRightPanelBody: 'AttendanceRightPanelBody',
    CheckYourLeaveStatus: 'CheckYourLeaveStatus',
    SelectDateToEditAttenceDate: 'SelectDateToEditAttenceDate',
    ChangeYourAttendanceControls: 'ChangeYourAttendanceControls',
    ChangeYourAttendanceButton: 'ChangeYourAttendanceButton',
    AttendanceStatusByAdmiin: 'AttendanceStatusByAdmiin',
    EmployeeTable: 'EmployeeTable',
    EmployeeIdInputControl: 'EmployeeIdInputControl',
    AddEmployeeValidationControls: 'AddEmployeeValidationControls',
    ViewDetailsModal: 'ViewDetailsModal',
    ViewFirstNameInputControl: 'ViewFirstNameInputControl',
    ViewSecondNameInputControl: 'ViewSecondNameInputControl',
    ViewDOBInputControl: 'ViewDOBInputControl',
    ViewEmployeeIdInputControl:'ViewEmployeeIdInputControl',
    ViewEmailIdInputControl:'ViewEmailIdInputControl',
    ViewDesignationInputControl:'ViewDesignationInputControl',
    ViewDOJInputControl:'ViewDOJInputControl',
    ViewCompanyInputControl:'ViewCompanyInputControl',
    EditFirstNameInputControl:'EditFirstNameInputControl',
    EditLastNameInputControl:'EditLastNameInputControl',
    EditDOBInputControl:'EditDOBInputControl',
    EditEmployeeIdInputControl:'EditEmployeeIdInputControl',
    EditEmailIdInputControl:'EditEmailIdInputControl',
    EditDesignationInputControl:'EditDesignationInputControl',
    EditDOJInputControl:'EditDOJInputControl',
    EditCompanyInputControl:'EditCompanyInputControl',
    EditEmployeeButton:'EditEmployeeButton',
    EditEmmployeeModal:'EditEmmployeeModal',
    RowId:'RowId',
    EditEmployeeValidationControls:'EditEmployeeValidationControls',
    LeaveTables:'LeaveTables',
    AddCompanyLeaveValidationControls:'AddCompanyLeaveValidationControls',
    RowCompanyLeaveId:'RowCompanyLeaveId',
    EditCompanyLeaveModal:'EditCompanyLeaveModal',
    EditLeaveControlDate:'EditLeaveControlDate',
    EditLeaveControlDay:'EditLeaveControlDay',
    EditLeaveControlReason:'EditLeaveControlReason',
    EditLeaveButton:'EditLeaveButton',
    EditCompanyLeaveValidationControls:'EditCompanyLeaveValidationControls',
    NewsTable:'NewsTable',
    NewsDescription:'NewsDescription',
    AddNewsValidationControls:'AddNewsValidationControls',
    NewsId:'NewsId',
    EditNewsModal:'EditNewsModal',
    EditNewsTitleInputControl:'EditNewsTitleInputControl',
    EditNewsReasonControlReason:'EditNewsReasonControlReason',
    EditNewsButton:'EditNewsButton',
    EditNewsValidationControls:'EditNewsValidationControls',
    LeaveTableForEmployee:'LeaveTableForEmployee'
};
lr.classes = {
    SetLeaveInputBox: 'SetLeaveInputBox',
    modalBody:'modal-body',
    AdminApproveLeave:'AdminApproveLeave',
    AdminRejectLeave:'AdminRejectLeave'
};
lr.alertType = {
  Success:'Success',
  Warning:'Warning',
  Information:'Information',
  Error:'Error'
};
lr.sendData = {
    /*Send data for sign in*/
    LoginInformation: function() {
        var loginInformation = {
            EmailId: $("#" + lr.ids.LoginEmailIdAddress).val(),
            Password: $("#" + lr.ids.LoginPassword).val()
        };
        return JSON.stringify(loginInformation);
    },
    /*Send data for recover password*/
    RecoverPassword: function() {
        var emailId = {
            EmailId: $("#"+lr.ids.RecoverPasswordEmailId).val()
        };
        return JSON.stringify(emailId);
    },
    /*Send data for sign up password*/
    SignUp: function() {
        var signUpData = {
            EmailId: $("#"+lr.ids.RegistrationEmailIdAddress).val(),
            EmployeeId: $("#"+lr.ids.SignUpEmpId).val(),
            Password: $("#"+lr.ids.SignUpPassword).val(),
            RePassword: $("#"+lr.ids.SignUpRePassword).val()
        };
        return JSON.stringify(signUpData);
    },
    /*Send session infromations*/
    Session: function() {
        var sessionData = {
            SessionKey: $("#"+lr.ids.SessionId).val(),
            SessionValue: $("#"+lr.ids.SessionValue).val()
        };
        return JSON.stringify(sessionData);
    },
    ApplyLeave:function() {
        var applyLeaveData = {
         SessionKey: $("#"+lr.ids.SessionId).val(),
         SessionValue: $("#"+lr.ids.SessionValue).val(),
         FromDate:$("#"+lr.ids.FromDate).val(),
         ToDate:$("#"+lr.ids.ToDate).val(),
         TypeOfLeave:lr.functions.GetSelectValue(lr.ids.LeaveType)
        };
        return JSON.stringify(applyLeaveData);
    },
    AttendanceDetails:function() {
        var attendanceDetails = {
               SessionId:$("#"+lr.ids.SessionId).val(),
               SessionValue:$("#"+lr.ids.SessionValue).val(),
               Date  :$("#"+lr.ids.DateToKnowAttendance).val()
        };
        return JSON.stringify(attendanceDetails);
    },
    LeaveDetails:function() {
        var leaveDetails = {
            SessionId: $("#" + lr.ids.SessionId).val(),
            SessionValue: $("#" + lr.ids.SessionValue).val(),
            LeaveType: $("#" + lr.ids.TypeToKnowLeave).val()
        };
        return JSON.stringify(leaveDetails);
    },
    GetYourLeaveRegister:function() {
        var getYourLeaveRegister = {
            SessionId: $("#" + lr.ids.SessionId).val(),
            SessionValue: $("#" + lr.ids.SessionValue).val(),
            Month: $("#" + lr.ids.TypeOfMonth).val()
        };
        return JSON.stringify(getYourLeaveRegister);
    },
    PasswordReset:function() {
        var passwordReset = {
               CurrentPassword:$("#"+lr.ids.CurrentPasswordInputControl).val(),
               NewPassword:$("#"+lr.ids.NewPasswordInputControl).val(),
               ReNewPassword:$("#"+lr.ids.ReEnterPasswordInputControl).val(),
               SessionId:$("#" + lr.ids.SessionId).val(),
               SessionValue:$("#" + lr.ids.SessionValue).val()
        };
        return JSON.stringify(passwordReset);
    },
    SaveProfile:function() {
        var saveProfile = {
          SessionId: $("#" + lr.ids.SessionId).val(),
            SessionValue: $("#" + lr.ids.SessionValue).val(),
            DateOfBirth: $("#" + lr.ids.EmpDOB).val()

        };                            
        return JSON.stringify(saveProfile);
    },
    ConfigureAttendance:function() {
        var configureAttendance = {
            SessionId: $("#" + lr.ids.SessionId).val(),
            SessionValue: $("#" + lr.ids.SessionValue).val(),
            CasualLeave: $("#" + lr.ids.TotalCasulaLeave).val(),
            SickLeave: $("#" + lr.ids.TotalSickLeave).val(),
            WorkingHours: $("#" + lr.ids.TotalWorkingHours).val()
        };
        return JSON.stringify(configureAttendance);
    },
    GetEmployeeAttendanceStatus:function() {
        var getEmployeeAttendanceStatus = {
          SessionKey: $("#" + lr.ids.SessionId).val(),
            SessionValue: $("#" + lr.ids.SessionValue).val(),
            EmployeeId: $("#" + lr.ids.SelectEmpIDtoEditControls).val(),
            Date: $("#" + lr.ids.SelectDateToEditAttenceDate).val()
        };
        return JSON.stringify(getEmployeeAttendanceStatus);
    },
    ChangeAttendanceStatus:function() {
        var changeAttendanceStatus = { 
             SessionId: $("#" + lr.ids.SessionId).val(),
            SessionValue: $("#" + lr.ids.SessionValue).val(),
            AttendanceStatus: $("#" + lr.ids.ChangeYourAttendanceControls).val(),
            EmployeeId: $("#" + lr.ids.SelectEmpIDtoEditControls).val(),
            Date: $("#" + lr.ids.SelectDateToEditAttenceDate).val()
        };
        return JSON.stringify(changeAttendanceStatus);
    },
    AddEmpolyee:function() {
        var addEmployee = {
           SessionId: $("#" + lr.ids.SessionId).val(),
            SessionValue: $("#" + lr.ids.SessionValue).val(),
            FirstName: $("#" + lr.ids.FirstNameInputControl).val(),
            LastName: $("#" + lr.ids.LastNameInputControl).val(),
            Designation: $("#" + lr.ids.DesignationInputControl).val(),
            Doj: $("#" + lr.ids.DOJInputControl).val(),
            EmployeeId: $("#" + lr.ids.EmployeeIdInputControl).val(),
            EmailId: $("#" + lr.ids.EmailIdInputControl).val(),
            Company: $("#" + lr.ids.CompanyInputControl).val()
        };
        return JSON.stringify(addEmployee);
    },
    UpdateEmployeeInfo:function() {
        var updateEmployeeInfo = { 
            Id: $("#" + lr.ids.RowId).val(),
            SessionId: $("#" + lr.ids.SessionId).val(),
            SessionValue: $("#" + lr.ids.SessionValue).val(),
            FirstName: $("#" + lr.ids.EditFirstNameInputControl).val(),
            LastName: $("#" + lr.ids.EditLastNameInputControl).val(),
            Designation: $("#" + lr.ids.EditDesignationInputControl).val(),
            Doj: $("#" + lr.ids.EditDOJInputControl).val(),
            Company: $("#" + lr.ids.EditCompanyInputControl).val()
        };
        return JSON.stringify(updateEmployeeInfo);
    },
    AddCompanyLeaveData: function() {
        var addCompanyLeaveData = {  
           SessionId: $("#" + lr.ids.SessionId).val(),
            SessionValue: $("#" + lr.ids.SessionValue).val(),
            Date: $("#" + lr.ids.AddLeaveControlDate).val(),
            Day: $("#" + lr.ids.AddLeaveControlDay).text(),
            LeaveDescription: $("#" + lr.ids.AddLeaveControlReason).val()
        };
        return JSON.stringify(addCompanyLeaveData);
    },
    DeleteCompanyLeaveRow:function() {//string SessionId, string SessionValue, int LeaveId)
        var deleteCompanyLeaveRow = {
             SessionId: $("#" + lr.ids.SessionId).val(),
            SessionValue: $("#" + lr.ids.SessionValue).val(),
            LeaveId: $("#" + lr.ids.RowCompanyLeaveId).val()
        };
        return JSON.stringify(deleteCompanyLeaveRow);
    },
    UpdateCompanyLeave:function() {
        var updateCompanyLeave = {
            SessionId: $("#" + lr.ids.SessionId).val(),
            SessionValue: $("#" + lr.ids.SessionValue).val(),
            Date: $("#" + lr.ids.EditLeaveControlDate).val(),
            Day: $("#" + lr.ids.EditLeaveControlDay).text(),
            LeaveDescription: $("#" + lr.ids.EditLeaveControlReason).val(),
            LeaveId: $("#" + lr.ids.RowCompanyLeaveId).val()
        };
        return JSON.stringify(updateCompanyLeave);
    },
    AddNewsData:function() {
        var addNewsData = {
            SessionId: $("#" + lr.ids.SessionId).val(),
            SessionValue: $("#" + lr.ids.SessionValue).val(),
            Title: $("#" + lr.ids.NewsTitleInputControl).val(),
            Description: $("#" + lr.ids.NewsDescription).val()
        };
        return JSON.stringify(addNewsData);
    },
    DeleteNews:function() {
        var deleteNews = { 
          SessionId: $("#" + lr.ids.SessionId).val(),
            SessionValue: $("#" + lr.ids.SessionValue).val(),
            NewsId: $("#" + lr.ids.NewsId).val()
        };
        return JSON.stringify(deleteNews);
    },
    EditNewsData:function() {
        var editNewsData = {
            SessionId: $("#" + lr.ids.SessionId).val(),
            SessionValue: $("#" + lr.ids.SessionValue).val(),
            Title: $("#" + lr.ids.EditNewsTitleInputControl).val(),
            Description: $("#" + lr.ids.EditNewsReasonControlReason).val(),
            NewsId: $("#" + lr.ids.NewsId).val()
        };
        return JSON.stringify(editNewsData);
    },
    ActionOnLeave:function(leaveId, leaveActionByAdmin) {
        var actionOnLeave = {
           SessionId: $("#" + lr.ids.SessionId).val(),
            SessionValue: $("#" + lr.ids.SessionValue).val(),
            LeaveId: leaveId,
            LeaveStatus: leaveActionByAdmin
        };
        return JSON.stringify(actionOnLeave);
    },
    PageReloadData:function() {
        var reload= {
            SessionKey: lr.functions.GetQueryString()["sId"],
            SessionValue: lr.functions.GetQueryString()["sVal"]
        };
        return JSON.stringify(reload);
    }
};
lr.constants = {
    Success: 'Success',
    AjaxType: 'POST',
    AjaxDataType: 'json',
    AjaxContentType: 'application/json;charset=utf-8',
};
lr.messages = {
    EmailValidationFailed: 'Please enter email address in right format. Example: abcd@gmail.com',
};
lr.controlValidation = {
    EmailValidation: function(textFielId) {
        var emailId = $("#" + textFielId);
        var emailValue = emailId.val();
        var atRateIndex = emailValue.indexOf('@');
        var dotIntex = emailValue.indexOf('.');
        var lastIndex = emailValue.length-1;
        if ((atRateIndex < 4) || (dotIntex < (atRateIndex + 4)) || (lastIndex < dotIntex + 2)) {
            return lr.messages.EmailValidationFailed;
        } else {
            return lr.constants.Success;
        }
    },
    IsNullOrEmpty: function(inputFieldId) {
        var control = $("#" + inputFieldId);
        var controlValue = $.trim(control.val());
        return (controlValue == null || controlValue == "");
    },
    IsSelectNullOrEmpty: function(selectId) {
        var control = $("#" + selectId);
        var controlValue = $.trim(control.val());
        return (controlValue == null || controlValue == "--Select--"|| controlValue == "Select Leave Type" || controlValue == "Select Month" ||controlValue=="");
    },
    IsDateEmpty:function(dateFieldValue) {
        return (dateFieldValue == "" || dateFieldValue == 'defaullt date');
    }
};
lr.functions = {
    FormatDate: function(dateValue) {
        dateValue = dateValue.split(" ");
        return dateValue[0];
    },
    FormatTime:function(time) { 
        var timeArray = time.split(".");
        timeArray = timeArray[0].split(":");
        var timeHours = (timeArray[0] > 12) ? (24 - timeArray[0]) : timeArray[0];
        if (timeArray[0] > 12) {
         timeHours = 12 - timeHours;   
        }
        var amOrPm = (timeArray[0] >= 12) ? "PM" : "AM";
        return timeHours + ":"+timeArray[1]+":"+timeArray[2]+" "+amOrPm;
    },
    SetDateInputField:function(date) {
        date = date.split("/");
        if (date[1].length == 1) {
            date[1] = "0" + date[1];
        }
        if (date[0].length == 1) {
            date[0] = "0" + date[0];
        }
        return date[2] + "-" + date[0] + "-" + date[1];
    },
    RemoveLeaveRegisterContainer: function() {
        $("#" + lr.ids.LeaveRegisterContainer).remove();
    },
    CurrentTime:function() {
        var now = new Date();
        now=now.toTimeString();
        now = now.split(" ");
        now = now[0].split(":");
        var timeHours = (now[0] > 12) ? (24 - now[0]) : now[0];
        if (now[0] > 12) {
         timeHours = 12 - timeHours;   
        }
        var amOrPm = (now[0] >= 12) ? "PM" : "AM";
        return timeHours + ":"+now[1]+":"+now[2]+" "+amOrPm;
    },
    TodaysDate:function() {
      var now = new Date();
        return now.toDateString();
    },
    RemoveAdminRightPanelContainer: function() {
     $("#" + lr.ids.RightAdminPanelBody).remove();
    },
    ActiveAdminLink:function(linkId) {
        var adminLinkContainer = $(".nav-pills");
        var adminLinks = adminLinkContainer.children();
        for (var i = 0; i < adminLinks.length; i++) {
            $("#"+adminLinks[i].id).removeClass("active");
        };
        $("#" + linkId).addClass("active");
    },
    ActiveHeaderMenue:function(menuId) {
        var menuLinkContainer = $(".navbar-nav");
        var menuLinks = menuLinkContainer.children();
        for (var i = 0; i < menuLinks.length; i++) {
           $("#"+menuLinks[i].id).removeClass("active"); 
        };
        $("#"+menuId).addClass("active");
    },
    OpenModel:function() {
        $("#" + lr.ids.MyModal).modal();
    },
    CloseModel: function() {
        $("#" + lr.ids.MyModal).modal('hide');
        $("#" + lr.ids.Alert).modal('hide');
    },
    Alert:function(alertType, alertMessage) {
        var bgColor = (alertType == lr.alertType.Success) ? "Green" : ((alertType == lr.alertType.Error) ? "Red" : ((alertType == lr.alertType.Warning) ? "Yellow" : ((alertType == lr.alertType.Information) ? "Blue" : "green")));
        var color = (bgColor == "Green" || bgColor == "Red" || bgColor == "Blue") ? "White" : ((bgColor == "Yellow") ? "Black" : "White");
        $("#" + lr.ids.AlertTitle).text(alertType)
        .css("color",color);
        $("#" + lr.ids.AlertTitleBody).css("background-color", bgColor);
        $("#" + lr.ids.AlertBody).empty()
        .html(alertMessage);
        $("#" + lr.ids.Alert).modal();
    },
    GetSelectValue:function(selectId) {
        var selectValue  = $("#" + selectId).val();
        if (selectValue == "" || selectValue == "Select Leave Type" || selectValue == "Select Month" || selectValue == "--Select--") {
            return "";
        }
        return selectValue;
    },
    IsWeekend:function(date) {
        var myDate = new Date(date);
        var day = myDate.getDay();
        return (day == 6 || day == 0);
    },
    isVaildToDate:function(fromDateInputControl, toDateIdInputControl) {
        var fromDate = new Date(fromDateInputControl.val());
        var toDate = new Date(toDateIdInputControl.val());
        return (toDate > fromDate);
    },
    PrepareDdmOptions:function(options) {
        var optionsHtml = '';
        for (var i = 0; i< options.length; i++) {
            optionsHtml += '<option>' + options[i] + '</option>';
        }
        return optionsHtml;
    },
    GetDayName:function(date) {
         date = new Date(date);
        var dayName;
        switch (date.getDay()) {
             case 0:
                 dayName = "Sunday";
                 break;
             case 1:
                 dayName = "Monday";
                 break;
             case 2:
                 dayName = "Tuesday";
                 break;
             case 3:
                 dayName = "Wednesday";
                 break;
             case 4:
                 dayName = "Thursday";
                 break;
             case 5:
                 dayName = "Friday";
                 break;
             case 6:
                 dayName = "Saturday";
                 break;
             default:
                 dayName = "";
                 break;
             };
        return dayName;
    },
    AppendQueryString:function(sessionId, sessionValue) {
       location.hash = "?sId=" + sessionId + "&sVal=" + sessionValue;
    },
    GetQueryString:function() {
         var vars = [], hash;
        var hashes = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
        for (var i = 0; i < hashes.length; i++) {
            hash = hashes[i].split('=');
            vars.push(hash[0]);
            vars[hash[0]] = hash[1];
        }
        return vars;
    }
};
                           