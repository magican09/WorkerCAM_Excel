using System;
using System.Collections.ObjectModel;
using Excel = Microsoft.Office.Interop.Excel;

namespace EmplCRMClassLibrary.Interfaces
{
    public interface ICommonTimeSheet
    {
         ObservableCollection<IWorkerTimeSheet> WorkerTimeSheets { get; set; }
         ObservableCollection<IEmployee> Employees { get; set; }
         DateTime ScoreDate { get; set; }
        DateTime LastDate { get; set; }
        void CreateTimeSheet(ref Excel.Worksheet templWorksheet, ref Excel.Worksheet jornalWorksheet, DateTime scoreDate,
           Action<int, object> ReportProgress = null);

    }
}
