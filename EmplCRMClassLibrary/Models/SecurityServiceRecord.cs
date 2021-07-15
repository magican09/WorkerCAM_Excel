using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmplCRMClassLibrary.Models
{
  public   enum ResultStatus
    {
        Agreed,
       AgreedWithComments,
        Unsettled
    }
   public  class SecurityServiceRecord
    {
       public int Number { get; set; }
        public string FullName  { get; set; }
        public Employee Employee { get; set; }
        public ResultStatus ResultStatus { get; set; }
        public SecurityServiceRecord(string fullName, ResultStatus resultStatus)
        {
            Employee = new Employee(fullName);
            FullName = Employee.FullName;
            ResultStatus = resultStatus;
        }
    }
}
