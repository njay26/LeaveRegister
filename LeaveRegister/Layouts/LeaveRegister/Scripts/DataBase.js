$(document).ready(function () {
    $("body").append(lr.prepareHtml.Alert);
    //Create Data Base
    $("#CreateDBButton").click(function () {
        $.ajax({
            url: 'Layouts/ASMXFiles/DataBase.asmx/CreateDatabase',
            dataType: 'json',
            type: 'POST',
            data: "{\"ServerName\":\"" + $("#DBNameControl").val() + "\"}",
            contentType: 'application/json; charset=utf-8',
            success: function (result) {
                lr.functions.Alert(lr.alertType.Success,result.d);
            },
            error: function (res) {
                lr.functions.Alert(lr.alertType.Error, res);
            }
        });
    });

    //Create tables
    $("#CreateTableButton").click(function () {
        $.ajax({
            url: 'Layouts/ASMXFiles/DataBase.asmx/CreateTables',
            dataType: 'json',
            type: 'POST',
            contentType: 'application/json; charset=utf-8',
            success: function (result) {
                lr.functions.Alert(lr.alertType.Success, result.d);
            },
            error: function (res) {
                lr.functions.Alert(lr.alertType.Error, res);
            }
        });
    });

    //Configure web configuration
    $("#CreateWebConfigButton").click(function () {
        $.ajax({
            url: 'Layouts/ASMXFiles/DataBase.asmx/DoWebConfiguration',
            dataType: 'json',
            type: 'POST',
            data: "{\"ServerName\":\"" + $("#DBNameControl").val() + "\"}",
            contentType: 'application/json; charset=utf-8',
            success: function (result) {
                lr.functions.Alert(lr.alertType.Success, result.d);
            },
            error: function (res) {
                lr.functions.Alert(lr.alertType.Error, res);
            }
        });
    });

    //Create SP
    $("#CreateSPButton").click(function () {
        $.ajax({
            url: 'Layouts/ASMXFiles/DataBase.asmx/CreateStoreProcedures',
            dataType: 'json',
            type: 'POST',
            contentType: 'application/json; charset=utf-8',
            success: function (result) {
                lr.functions.Alert(lr.alertType.Success, result.d);
            },
            error: function (res) {
                lr.functions.Alert(lr.alertType.Error, res);
            }
        });
    });

    //store functions
    $("#CreateFunctionButton").click(function () {
        $.ajax({
            url: 'Layouts/ASMXFiles/DataBase.asmx/CreateFunctions',
            dataType: 'json',
            type: 'POST',
            contentType: 'application/json; charset=utf-8',
            success: function (result) {
                lr.functions.Alert(lr.alertType.Success, result.d);
            },
            error: function (res) {
                lr.functions.Alert(lr.alertType.Error, res);
            }
        });
    });

    //Do leave register settings
    $("#DoLeaveRegisterSettingButton").click(function () {
        $.ajax({
            url: 'Layouts/ASMXFiles/DataBase.asmx/DoLeaveRegisterSetting',
            dataType: 'json',
            type: 'POST',
            contentType: 'application/json; charset=utf-8',
            success: function (result) {
                lr.functions.Alert(lr.alertType.Success, result.d);
            },
            error: function (res) {
                lr.functions.Alert(lr.alertType.Error, res);
            }
        });
    });

    //Add admin employee
    $("#CreateAdminUserButton").click(function () {
        $.ajax(
            {
                url: 'Layouts/ASMXFiles/DataBase.asmx/AddAdminEmplyee',
                dataType: 'json',
                type: 'POST',
                data: SendData(),
                contentType: 'application/json; charset=utf-8',
                success: function (result) {
                    lr.functions.Alert(lr.alertType.Information, result.d);
                },
                error: function (result) {
                    lr.functions.Alert(lr.alertType.Error, result.d);
                }
            });
    });
        function SendData() {
            var person = { 
                EmailId: $("#EmailIdInputControls").val(),
                EmployeeId: $("#EmployeeIdInputControl").val(),
                Password: $("#AdminPasswordControls").val(),
                RePassword: $("#AdminRePasswordControls").val(),
                FirstName: $("#FirstNameInputControl").val(),
                LastName: $("#LastNameInputControl").val(),
                DateOfJoining: $("#DOJInputControls").val(),
                Designation: $("#DesignationInputControl").val(),
                Company: $("#CompanyInputControls").val()
            };
           return JSON.stringify(person);
       }
});