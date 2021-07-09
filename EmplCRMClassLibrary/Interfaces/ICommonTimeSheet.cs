using System.Collections.ObjectModel;

namespace EmplCRMClassLibrary.Interfaces
{
    public interface ICommonTimeSheet
    {
         ObservableCollection<IWorkerTimeSheet> WorkerTimeSheets { get; set; }
         ObservableCollection<IEmployee> Employees { get; set; }


    }
}
