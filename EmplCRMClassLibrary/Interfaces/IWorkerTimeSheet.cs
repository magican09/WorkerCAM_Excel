using EmplCRMClassLibrary.Models;
using System;

namespace EmplCRMClassLibrary.Interfaces
{
    public interface IWorkerTimeSheet
    {
         IEmployee Employee { get; set; }
         IWorkMonth WorkMonth { get; set; }
         DateTime FirstDate { get; set; }
         DateTime LastDate { get; set; }
         int WorkedDaysNamber { get; set; }
         int AbsentdDaysNamber { get; set; }
         int VacationdDaysNamber { get; set; }
         ICommonTimeSheet ParentCommonTimeSheet { get; set; }
    }
}
