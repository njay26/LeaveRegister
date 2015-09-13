namespace LeaveRegister.IImplementation
{
    /*This interface is being having all the requuire mrthod definition that will neccessory to implement into           
     * *derive class of this interface
     */
    public interface IDataBase
    {
        string CreateDatabase(string ServerName);
        string CreateTables();
        string CreateStoreProcedures();
        string CreateFunctions();
        string DoWebConfiguration(string ServerName);
        string DoLeaveRegisterSetting(); 
        string AddAdminEmplyee(string EmailId,string EmployeeId,string Password, string RePassword,string FirstName, string LastName,string DateOfJoining,string Designation, string Company);
    }
}
    