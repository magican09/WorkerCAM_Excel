using System;
using System.Collections.ObjectModel;

namespace EmplCRMClassLibrary.Interfaces
{
    public interface IWorkMonth
    {
         ObservableCollection<IWorkDay> WorkDays { get; set; }
         DateTime FirstDate { get; set; }
         DateTime LastDate { get; set; }
         int WorkedTime { get; set; }
          int WorkedDaysNamber { get; set; }
         int AbsentdDaysNamber { get; set; }
        int VacationdDaysNamber { get; set; }
        IWorkerTimeSheet ParentTimeSheet { get; set; }
         void SortDaysByDate();
         void AddWorkDay(DateTime date, DateTime inTime, DateTime outTime);
    }
}
