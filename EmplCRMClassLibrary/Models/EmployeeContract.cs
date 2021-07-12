using EmplCRMClassLibrary.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmplCRMClassLibrary.Models
{
    public class EmployeeContract : IEmployeeContract
    {
        public ObservableCollection<DayOfWeek> VacationDays { get; set; } = new ObservableCollection<DayOfWeek>();
       public  int WorkDayLength { get; set; }

    }
}
