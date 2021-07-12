using EmplCRMClassLibrary.Models;

namespace EmplCRMClassLibrary.Interfaces
{
    public interface IEmployee
    {
         string FullName { get; set; }
         int Namber { get; set; }
         IEmployeeContract EmployeeContract { get; set; }
    }
}
