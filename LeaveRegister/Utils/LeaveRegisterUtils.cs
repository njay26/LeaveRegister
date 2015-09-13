using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using LeaveRegister.Models;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;

namespace LeaveRegister.Utils
{
    public static class LeaveRegisterUtils
    {
        #region Methods

        /// <summary>
        /// Vaidate entered email id is correct fromat or not. If email id format is correct ValidateEmail() will return true otherwise retuns false.
        /// </summary>
        public static bool VaidateEmail(string EmailId)
        {
            var status = false;
            if (EmailId.Contains("@") && EmailId.Contains("."))
            {
                var atPosition = EmailId.LastIndexOf("@", StringComparison.Ordinal);
                var dotPosition = EmailId.LastIndexOf(".", StringComparison.Ordinal);
                if (((atPosition + 2) < dotPosition) && ((EmailId.Length - 2) > (dotPosition)) && (atPosition >= 2))
                {
                    status = true;
                }
            }
            return status;
        }
        /// <summary>
        /// Encrypt the password
        /// </summary>
        public static string EncryptPassword(string Password)
        {
            var key1 = Password[1].ToString(CultureInfo.InvariantCulture);
            var key2 = Password[2].ToString(CultureInfo.InvariantCulture);
            var key3 = Password[3].ToString(CultureInfo.InvariantCulture);
            var key4 = Password[4].ToString(CultureInfo.InvariantCulture);
            var key5 = Password[5].ToString(CultureInfo.InvariantCulture);
            var key6 = Password[6].ToString(CultureInfo.InvariantCulture);
            Password = Password.Insert(1, key5);
            Password = Password.Insert(3, key4);
            Password = Password.Insert(5, key3);
            Password = Password.Insert(6, key2);
            Password = Password.Insert(7, key1);
            Password = Password.Insert(4, key6);
            return Password;
        }
        /// <summary>
        /// Decrypt the password
        /// </summary>
        public static string DecryptPassword(string Password)
        {
            if (Password.Length < 13) return "";
            var decryptedPassword = Password.Remove(1, 1);
            decryptedPassword = decryptedPassword.Remove(2, 4);
            decryptedPassword = decryptedPassword.Remove(3, 1);
            return decryptedPassword;
        }
        /// <summary>
        /// Check Employee is currently logged in or not?
        /// </summary>
        public static bool IsEmployeeLoggedIn(string ConnectionString, string SessionId, string SessionValue)
        {
            var emailId = DecryptPassword(SessionId);
            return (DataBaseUtils.IsEmailIdExist(ConnectionString, emailId) &&
                    DataBaseUtils.IsPasswordMatches(ConnectionString, DecryptPassword(SessionValue), emailId));
        }
        /// <summary>
        /// Formate the date "YYYY-MM-DD"
        /// </summary>
        public static DateTime FoamteDate(string Date)
        {
            if (string.IsNullOrEmpty(Date)) return new DateTime();
            return DateTime.ParseExact(Date, "yyyy-MM-dd", null);
        }
        /// <summary>
        /// Get day diiference from and to date
        /// </summary>
        public static int DateDiffereceInDay(string StartDate, string ToDate, Boolean ExcludeWeekends)
        {
            int count = 0;
            var startDate = Convert.ToDateTime(StartDate);
            if (ExcludeWeekends && (startDate.DayOfWeek == DayOfWeek.Saturday || startDate.DayOfWeek == DayOfWeek.Sunday) && (string.IsNullOrEmpty(ToDate))) return count;
            else
            {
                if (string.IsNullOrEmpty(ToDate)) return count + 1;

            }
            var toDate = Convert.ToDateTime(ToDate);
            for (DateTime index = startDate; index < toDate; index = index.AddDays(1))
            {
                if (ExcludeWeekends && index.DayOfWeek != DayOfWeek.Sunday && index.DayOfWeek != DayOfWeek.Saturday)
                {
                    count++;
                }
            }
            return count + 1;
        }
        /// <summary>
        /// Get Date part from date time string here parameter must be 8/18/2015 12:00:00 AM
        /// </summary>
        public static string GetDatePart(string DateFromSql)
        {
            var dateString = DateFromSql.Split(' ');
            dateString = dateString[0].Split('/');
            var year = dateString[2];
            var month = dateString[0];
            var day = dateString[1];
            if (month.Length == 1)
            {
                month = "0" + month;
            }
            if (day.Length == 1)
            {
                day = "0" + day;
            }
            return year + "-" + month + "-" + day;
        }
        /// <summary>
        /// Get date for week day
        /// </summary>
        public static DateTime GetDateForAttendance(DateTime Date)
        {
            if (Date.DayOfWeek == DayOfWeek.Saturday || Date.DayOfWeek == DayOfWeek.Sunday)
            {
                Date = Date.AddDays(1);
                return GetDateForAttendance(Date);
            }
            return Date;
        }
        /// <summary>
        /// Export employee monthly attendance sheet into excel sheet
        /// </summary>
        public static void ExportEmployeeAttendanceSheet(DataTable Table)
        {
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.ClearContent();
            HttpContext.Current.Response.ClearHeaders();
            HttpContext.Current.Response.Buffer = true;
            HttpContext.Current.Response.ContentType = "application/ms-excel";
            HttpContext.Current.Response.Write(@"<!DOCTYPE HTML PUBLIC ""-//W3C//DTD HTML 4.0 Transitional//EN"">");
            HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=Reports.xls");
            HttpContext.Current.Response.Charset = "utf-8";
            HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("windows-1250");
            HttpContext.Current.Response.Write("<font style='font-size:10.0pt; font-family:Calibri;'>");
            HttpContext.Current.Response.Write("<BR><BR><BR>");
            HttpContext.Current.Response.Write("<Table border='1' bgColor='#ffffff' " +
              "borderColor='#000000' cellSpacing='0' cellPadding='0' " +
              "style='font-size:10.0pt; font-family:Calibri; background:white;'> <TR>");
            int columnscount = Table.Columns.Count;
            for (int j = 0; j < columnscount; j++)
            {
                HttpContext.Current.Response.Write("<Td>");
                HttpContext.Current.Response.Write("<B>");
                HttpContext.Current.Response.Write(Table.Columns[j].ToString());
                HttpContext.Current.Response.Write("</B>");
                HttpContext.Current.Response.Write("</Td>");
            }
            HttpContext.Current.Response.Write("</TR>");
            foreach (DataRow row in Table.Rows)
            {
                HttpContext.Current.Response.Write("<TR>");
                for (int i = 0; i < Table.Columns.Count; i++)
                {
                    HttpContext.Current.Response.Write("<Td>");
                    HttpContext.Current.Response.Write(row[i].ToString());
                    HttpContext.Current.Response.Write("</Td>");
                }
                HttpContext.Current.Response.Write("</TR>");
            }
            HttpContext.Current.Response.Write("</Table>");
            HttpContext.Current.Response.Write("</font>");
            HttpContext.Current.Response.Flush();
            try
            {
                HttpContext.Current.Response.End();
            }
            catch (Exception e)
            {
                var a = e.Source;
                var b = e.Message;
            }
        }
        /// <summary>
        /// Get the dataTable object
        /// </summary>
        public static DataTable ToDataTable<T>(List<T> ListItems)
        {
            var dataTable = new DataTable(typeof(T).Name);
            var props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in props)
            {
                dataTable.Columns.Add(prop.Name);
            }
            foreach (T item in ListItems)
            {
                var values = new object[props.Length];
                for (int i = 0; i < props.Length; i++)
                {
                    values[i] = props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }
            return dataTable;
        }
        /// <summary>
        /// Analyse the month attendance Data
        /// </summary>
        public static List<AttendanceSheet> GetAttendanceItemsByMonth(AttendanceSheet[] AttendanceSheetData, string Month)
        {
            if (!string.IsNullOrEmpty(Month))
            {
                var month = DateTime.ParseExact(Month, "MMMM", CultureInfo.CurrentCulture).Month;
                var monthNumber = month.ToString(CultureInfo.InvariantCulture);
                if (monthNumber.Length == 1) monthNumber = "0" + monthNumber;
                var todayDate = DateTime.Today;
                var currentYear = todayDate.Year;
                var lastDayOfMonth = DateTime.DaysInMonth(currentYear, month);
                var startDate = currentYear + "-" + monthNumber + "-" + "01";
                var endDate = currentYear + "-" + monthNumber + "-" + lastDayOfMonth;
                var lists = new List<AttendanceSheet>();
                /*Dictionary to get the search leave*/
                var attendanceSheetsData = new Dictionary<DateTime, AttendanceSheet>();
                if (AttendanceSheetData.Length > 0)
                {
                    foreach (AttendanceSheet attendance in AttendanceSheetData)
                    {
                        attendanceSheetsData.Add(FoamteDate(GetDatePart(attendance.Date)), attendance);
                    }
                }
                var date = FoamteDate(startDate);
                for (var i = 0; i < lastDayOfMonth; i++)
                {
                    if (attendanceSheetsData.ContainsKey(date))
                    {
                        lists.Add(attendanceSheetsData[date]);
                    }
                    else
                    {
                        /*Check date is greater than or equl to today's date*/
                        if (date.CompareTo(todayDate) >= 0 && (date.DayOfWeek != DayOfWeek.Sunday) && (date.DayOfWeek != DayOfWeek.Saturday))
                        {
                            var attendanceOffDays = new AttendanceSheet()
                            {
                                AttendanceStatus = "Waiting for your attendance input",
                                Date = date.ToString("d"),
                                Day = (date.DayOfWeek).ToString(),
                                InTime = "",
                                OutTime = "",
                                Status = true,
                                LunchTime = "",
                                TotalTakenCasualLeave = "0",
                                TotalTakenEarnedeave = "0",
                                TotalTakenHalfDatLeave = "0",
                                TotalTakenSickLeave = "0"
                            };
                            lists.Add(attendanceOffDays);
                        }
                        else
                        {
                            if ((date.CompareTo(todayDate) >= 0 || date.CompareTo(todayDate) == -1) &&
                                ((date.DayOfWeek == DayOfWeek.Sunday) || (date.DayOfWeek == DayOfWeek.Saturday)))
                            {
                                var attendanceOffDays = new AttendanceSheet()
                                {
                                    AttendanceStatus = "Leave",
                                    Date = date.ToString("d"),
                                    Day = (date.DayOfWeek).ToString(),
                                    InTime = "",
                                    OutTime = "",
                                    Status = true,
                                    LunchTime = "",
                                    TotalTakenCasualLeave = "0",
                                    TotalTakenEarnedeave = "0",
                                    TotalTakenHalfDatLeave = "0",
                                    TotalTakenSickLeave = "0"
                                };
                                lists.Add(attendanceOffDays);
                            }

                            else
                            {
                                var attendanceOffDays = new AttendanceSheet()
                                {
                                    AttendanceStatus = "Upsent",
                                    Date = date.ToString("d"),
                                    Day = (date.DayOfWeek).ToString(),
                                    InTime = "",
                                    OutTime = "",
                                    Status = true,
                                    LunchTime = "",
                                    TotalTakenCasualLeave = "0",
                                    TotalTakenEarnedeave = "0",
                                    TotalTakenHalfDatLeave = "0",
                                    TotalTakenSickLeave = "0"
                                };
                                lists.Add(attendanceOffDays);
                            }
                        }
                    }
                    date = date.AddDays(1);
                }
                return lists;
            }
            return new List<AttendanceSheet>();
        }
        /// <summary>
        /// Get all employess attendance Data
        /// </summary>
        public static Dictionary<string, Dictionary<string, List<AttendanceSheet>>> GetAttendanceItemsForAllEmloyee(string ConnectionString,
            AddedEmployee[] Employees)
        {
            var attendanceDictionary = new Dictionary<string, Dictionary<string, List<AttendanceSheet>>>();
            string[] monthName = DateTimeFormatInfo.CurrentInfo.MonthNames;
            foreach (string month in monthName)
            {
                if (!string.IsNullOrEmpty(month))
                {
                    var employeeMonthlyDictionary = new Dictionary<string, List<AttendanceSheet>>();
                    foreach (AddedEmployee employeesListForAttendance in Employees)
                    {
                        var attendanceSheet = DataBaseUtils.GetOwnAttendanceSheet(ConnectionString,
                            employeesListForAttendance.EmployeeId, month);
                        var employeeMonthlyAttendanceList = GetAttendanceItemsByMonth(attendanceSheet, month);
                        employeeMonthlyDictionary.Add(employeesListForAttendance.EmployeeId,
                            employeeMonthlyAttendanceList);
                    }
                    attendanceDictionary.Add(month, employeeMonthlyDictionary);
                }
            }
            return attendanceDictionary;
        }

        public static Object GetSingleEntry(List<AttendanceSheet> attendanceData, List<Object> args)
        {
            try
            {
                SpreadsheetDocument workbook = args.First() as SpreadsheetDocument;
                var workbookPart = workbook.AddWorkbookPart();
                var sheetPart = workbook.WorkbookPart.AddNewPart<WorksheetPart>();
                var sheetData = new SheetData();
                sheetPart.Worksheet = new Worksheet();
                workbook.WorkbookPart.Workbook = new Workbook();
                workbook.WorkbookPart.Workbook.Sheets = new Sheets();
                Sheets sheets = workbook.WorkbookPart.Workbook.GetFirstChild<Sheets>();
                string relationshipId = workbook.WorkbookPart.GetIdOfPart(sheetPart);
                uint sheetId = 1;
                if (sheets.Elements<Sheet>().Any())
                {
                    sheetId =
                        sheets.Elements<Sheet>().Select(s => s.SheetId.Value).Max() + 1;
                }
                Sheet sheet = new Sheet() { Name = "AttendanceSheet", Id = relationshipId, SheetId = sheetId };
                sheets.Append(sheet);
                WorkbookStylesPart stylesPart = workbookPart.AddNewPart<WorkbookStylesPart>();
                Row headerRow = new Row();
                List<String> columns = new List<string>();
                Columns sheetColumns = new Columns();
                uint cIdx = 1;
                List<string> ColumnHeading = new List<string> { "Date", "Day", "Attendance Status" };
                //for (int i = 0; i < ColumnHeading.Count; i++)
                //{
                //    columns.Add(ColumnHeading[i]);
                //    double width = (ColumnHeading[i].Length != 0) ? ColumnHeading[i].Length : 30;
                //    Column sheetColumn = new Column();//ReportUtils.CreateColumnData(cIdx, cIdx, width);
                //    sheetColumns.Append(sheetColumn);
                //    Cell cell = new Cell();
                //    cell.DataType = CellValues.String;
                //    cell.CellValue = new CellValue(ColumnHeading[i]);
                //    headerRow.AppendChild(cell);
                //    cIdx++;
                //}
                //sheetPart.Worksheet.Append(sheetColumns);
                //sheetData.AppendChild(headerRow);
                foreach (AttendanceSheet attendance in attendanceData)
                {
                    Row newRow = new Row();
                    List<string> data = new List<string> { attendance.Date, attendance.Day, attendance.AttendanceStatus };
                    for (int j = 0; j < data.Count; j++)
                    {
                        Cell cell = new Cell();
                        cell.DataType = CellValues.String;
                        string value = data[j];
                        cell.CellValue = new CellValue(value);
                        newRow.AppendChild(cell);
                    }
                    sheetData.AppendChild(newRow);
                }
                sheetPart.Worksheet.Append(sheetData);
                workbookPart.Workbook.Save();
                workbook.WorkbookPart.Workbook.Save();
                workbook.Close();
                return workbook;
            }
            catch (Exception e)
            {
                return new object();
            }
        }
        public static bool DownLoadFile(MemoryStream memoryStream, string FileName)
        {

            File.WriteAllText(@"D://" + FileName + ".xlsx","memoryStream.Length.ToString()");
            return true;
        }
        #endregion
    }
}