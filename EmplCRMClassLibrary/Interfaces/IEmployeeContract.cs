using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmplCRMClassLibrary.Interfaces
{

    public interface IEmployeeContract
    {
       
         ObservableCollection<DayOfWeek> VacationDays { get; set; }
        int WorkDayLength { get; set; }
    }
}
