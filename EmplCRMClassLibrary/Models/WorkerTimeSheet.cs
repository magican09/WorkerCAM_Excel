using EmplCRMClassLibrary.Interfaces;
using System;

namespace EmplCRMClassLibrary.Models
{
    public class WorkerTimeSheet : IWorkerTimeSheet
    {
        private IWorkMonth _workMonth;
        public IEmployee Employee { get; set; }
        public IWorkMonth WorkMonth
        {
            get { return _workMonth; }
            set { _workMonth = value; } 
        }
        public int WorkedDaysNamber { get; set; }
        public int AbsentdDaysNamber { get; set; }
        public int VacationdDaysNamber { get; set; }
        public DateTime FirstDate { get; set; }
        public DateTime LastDate { get; set; }
        public ICommonTimeSheet  ParentCommonTimeSheet { get; set; }
    public WorkerTimeSheet(string fullName, EmployeeContract employeeContract)
        {
            Employee = new Employee(fullName, employeeContract);
            WorkMonth = new WorkMonth();
            WorkMonth.ParentTimeSheet = this;
        }
    }
}
