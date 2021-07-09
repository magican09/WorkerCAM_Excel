using EmplCRMClassLibrary.Interfaces;

namespace EmplCRMClassLibrary.Models
{
    public class Employee : IEmployee
    {
        public string FullName { get; set; }
        public int Namber { get; set; }
        public Employee(string fullName)
        {
            FullName = fullName;
        }
    }
}
