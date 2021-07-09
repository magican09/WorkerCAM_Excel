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
         
        public DateTime FirstDate { get; set; }
        public DateTime LastDate { get; set; }
        public WorkerTimeSheet(string fullName)
        {
            Employee = new Employee(fullName);
            WorkMonth = new WorkMonth();
            WorkMonth.ParentTimeSheet = this;
        }
    }
}
