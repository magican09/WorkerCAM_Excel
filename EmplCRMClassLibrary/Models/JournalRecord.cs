using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmplCRMClassLibrary.Models
{
    public class JournalRecord
    {
        public DateTime Date { get; set; }
        public string FullName { get; set; }
        public DateTime InTime { get; set; }
        public DateTime OutTime { get; set; }
        public ObservableCollection<JournalRecord> JournalRecords { get; set; }
        public JournalRecord()
        {
            JournalRecords = new ObservableCollection<JournalRecord>();
            JournalRecords.CollectionChanged += JournalRecords_CollectionChanged;
        }
        private void JournalRecords_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {

            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add: // если добавление
                    AjustDays();                                //  SortDaysByDate();
                    break;
                case NotifyCollectionChangedAction.Remove: // если удаление
                                                           //    SortDaysByDate();
                    break;
                case NotifyCollectionChangedAction.Replace: // если замена
                                                            //  SortDaysByDate();
                    break;
            }

        }
        public void AjustDays()
        {
            InTime = DateTime.MinValue;
            OutTime = DateTime.MinValue;
            ObservableCollection<DateTime> TimeRow = new ObservableCollection<DateTime>();

            foreach (JournalRecord rec in JournalRecords)
            {
                if (rec.InTime != DateTime.MinValue) TimeRow.Add(rec.InTime);
                if (rec.OutTime != DateTime.MinValue) TimeRow.Add(rec.OutTime);
            }
            if (TimeRow.Count > 0)
            {
                TimeRow = new ObservableCollection<DateTime>(TimeRow.OrderBy(i => i.TimeOfDay));
                InTime = TimeRow[0];
                if(TimeRow.Count >1 && TimeRow[0]!= TimeRow[TimeRow.Count - 1])
                        OutTime = TimeRow[TimeRow.Count - 1];

            }


        }
    }
}
