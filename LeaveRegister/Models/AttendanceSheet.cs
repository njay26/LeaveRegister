// All field exposed by property
namespace LeaveRegister.Models
{
    public class TodayCurrentAttendance
    {
        public string InTime { get; set; }
        public string LunchTime { get; set; }
        public string OutTime { get; set; }
        public string TotalWorked { get; set; }
        public string Status { get; set; } 
        public string StatusText { get; set; }
    }

    public class AttendanceSheet
    {
        public string Date { get; set; }
        public string Day { get; set; }
        public string InTime { get; set; }
        public string LunchTime { get; set; }
        public string OutTime { get; set; }
        public string TotalWorked { get; set; }
        public string AttendanceStatus { get; set; }
        public string TotalTakenCasualLeave { get; set; }
        public string TotalTakenSickLeave { get; set; }
        public string TotalTakenHalfDatLeave { get; set; }
        public string TotalTakenEarnedeave { get; set; }
        public bool Status { get; set; }
    }

    public class GetAttendanceStatus
    {
        public string AttendanceStatus { get; set; }
        public int TotalWorked { get; set; }
        public bool Status { get; set; }
    }
} 