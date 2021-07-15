using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmplCRMClassLibrary.Models
{
    public class SecurityServiceRecordJournal
    {
        public ObservableCollection<SecurityServiceRecord> SecurityServiceRecords { get; set; }
            = new ObservableCollection<SecurityServiceRecord>();
        public ObservableCollection<Employee> Employees { get; set; } = new ObservableCollection<Employee>();

        public SecurityServiceRecordJournal()
        {
            SecurityServiceRecords.CollectionChanged += ReportJornalRecords_CollectionChanged;
        }

        private void ReportJornalRecords_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add: // если добавление
                    string  inputEmployeeFullName =((SecurityServiceRecord) e.NewItems[0]).FullName;
                     var empl = Employees.FirstOrDefault(em => em.FullName == inputEmployeeFullName);
                   
                    if (empl == null)
                    {
                        Employees.Add(new Employee(inputEmployeeFullName));
                    }
                    else
                    {

                    }
                    break;
                case NotifyCollectionChangedAction.Remove: // если удаление
                                                           //    SortDaysByDate();
                    break;
                case NotifyCollectionChangedAction.Replace: // если замена
                                                            //  SortDaysByDate();
                    break;
            }
        }

        //public void  AddRecord( SecurityServiceRecord securityServiceRecord)
        //{
        //    var record = ReportJornalRecords.First(emp => emp.FullName == securityServiceRecord.Employee.FullName);
        //    if(record==null)
        //    {
        //        ReportJornalRecords.Add(securityServiceRecord);
        //    }
        //    else
        //    {

        //    }

        //}
    }
}
