using EmplCRMClassLibrary.Interfaces;
using EmplCRMClassLibrary.Models;
using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;

namespace EmplCRMClassLibrary
{
    public class WorkMonth : IWorkMonth
    {
        private double _workeTime;
        public ObservableCollection<IWorkDay> WorkDays { get; set; }
        public DateTime FirstDate { get; set; }
        public DateTime LastDate { get; set; }
        public int WorkedTime { get; set; }
        public IWorkerTimeSheet ParentTimeSheet { get; set; }
        public double WorkeTime
        {
            get
            {
                _workeTime = 0;
                foreach (WorkDay wrday in WorkDays)
                    _workeTime += wrday.WorkeTime;
                return _workeTime;
            }
            set
            {
                
                    
            }
        }
        public WorkMonth()
        {
            WorkDays = new ObservableCollection<IWorkDay>();
            WorkDays.CollectionChanged += WorkDays_CollectionChanged;
        }

        private void WorkDays_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add: // если добавление
                  //  SortDaysByDate();
                    break;
                case NotifyCollectionChangedAction.Remove: // если удаление
                //    SortDaysByDate();
                    break;
                case NotifyCollectionChangedAction.Replace: // если замена
                  //  SortDaysByDate();
                    break;
            }

        }
        

        public  void  SortDaysByDate()
        {
            WorkDays = new ObservableCollection<IWorkDay>(WorkDays.OrderBy(i => i.Date));
          /*  foreach (WorkDay wrkDay in WorkDays)
                wrkDay.AjustTimes();*/
            FirstDate = WorkDays[0].Date;
            LastDate = WorkDays[WorkDays.Count - 1].Date;
            if (ParentTimeSheet != null)
            {
                ParentTimeSheet.FirstDate = FirstDate;
                ParentTimeSheet.LastDate = LastDate;
            }
        }
        public void AddWorkDay (DateTime date, DateTime  inTime, DateTime  outTime)
        {
             WorkDay wrkday;
              wrkday = FindWorkDayByDate(date);
              if (wrkday == null)
              {
                  WorkDays.Add(new WorkDay(date, inTime, outTime));
              }
              else
              {
                  wrkday.InTime = inTime;
                  wrkday.OutTime = outTime;
              }
         }
        public WorkDay FindWorkDayByDate(DateTime  date)
        {
            foreach (WorkDay wrkDay in WorkDays )
                if (wrkDay.Date == date) return wrkDay;
            return null;
        }

       
    }
}
