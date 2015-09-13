namespace LeaveRegister.IImplementation
{
    public interface ILeave
    {
        string[] LeaveTab(string SessionKey, string SessionValue);
        string ApplyLeave(string SessionKey, string SessionValue,string FromDate,string ToDate,string TypeOfLeave);
    }
}