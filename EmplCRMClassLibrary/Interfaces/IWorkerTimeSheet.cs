using System;

namespace EmplCRMClassLibrary.Interfaces
{
    public interface IWorkerTimeSheet
    {
         IEmployee Employee { get; set; }
         IWorkMonth WorkMonth { get; set; }
         DateTime FirstDate { get; set; }
         DateTime LastDate { get; set; }
    }
}
