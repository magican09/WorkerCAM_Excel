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
        public int WorkedDaysNamber { get; set; }
        public int AbsentdDaysNamber { get; set; }
        public int VacationdDaysNamber { get; set; }

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


        public void SortDaysByDate()
        {
            WorkDays = new ObservableCollection<IWorkDay>(WorkDays.OrderBy(i => i.Date));//Соритируем текущий список дней..
           
            DateTime firstWorkedDay = WorkDays[0].Date;
            DateTime lastWorkedDay =  WorkDays[WorkDays.Count -1].Date;
              if (lastWorkedDay > ParentTimeSheet.ParentCommonTimeSheet.LastDate) ParentTimeSheet.ParentCommonTimeSheet.LastDate = lastWorkedDay;
              if (ParentTimeSheet.ParentCommonTimeSheet.ScoreDate != DateTime.MinValue)
                  lastWorkedDay = ParentTimeSheet.ParentCommonTimeSheet.ScoreDate;
              else
                  lastWorkedDay = ParentTimeSheet.ParentCommonTimeSheet.LastDate;
             
           
                  if (ParentTimeSheet.Employee.FullName == "Байгубаков Динар Сулпанович")
                ;
            if (ParentTimeSheet.Employee.FullName == "Сидоров Иван Владимирович")
                ;
            for (int ii = 0; ii < lastWorkedDay.Day-firstWorkedDay.Day; ii++)
            {
              
                DateTime observeDay = firstWorkedDay.Add(new TimeSpan(ii+1, 0, 0, 0));
                var findedDay = WorkDays.FirstOrDefault(d => d.Date == observeDay);
                if (findedDay == null) //Если в списке нед дня - добавляем его 
                {
                    WorkDay workDay = new WorkDay();
                    workDay.Date = observeDay;
                    workDay.ParentWorketMonth = this;
                    workDay.CalcDayStatus();
                    WorkDays.Add(workDay);
                }
            }
            WorkDays = new ObservableCollection<IWorkDay>(WorkDays.OrderBy(i => i.Date));//Соритируем текущий список дней..

            FirstDate = WorkDays[0].Date;
            LastDate = WorkDays[WorkDays.Count - 1].Date;
            if (ParentTimeSheet != null)
            {
                ParentTimeSheet.FirstDate = FirstDate;
                ParentTimeSheet.LastDate = LastDate;
            }
            AbsentdDaysNamber = 0;
            WorkedDaysNamber = 0;
            foreach (WorkDay wrDay in WorkDays)
            {
                if (wrDay.IsAbsent == true)
                    AbsentdDaysNamber++;

                if (ParentTimeSheet.Employee.EmployeeContract.VacationDays.Contains(wrDay.Date.DayOfWeek))
                    VacationdDaysNamber++;
                     
               if (wrDay.IsAbsent == false)
                    WorkedDaysNamber++;
            }
            ParentTimeSheet.WorkedDaysNamber = WorkedDaysNamber;
            ParentTimeSheet.AbsentdDaysNamber = AbsentdDaysNamber;
            ParentTimeSheet.VacationdDaysNamber = VacationdDaysNamber;

        }
        public void AddWorkDay(DateTime date, DateTime inTime, DateTime outTime)
        {
            WorkDay wrkday;
            wrkday = FindWorkDayByDate(date);
            if (wrkday == null)
            {
               
                wrkday = new WorkDay(date, inTime, outTime, this);
                WorkDays.Add(wrkday);
                if (wrkday.Date > ParentTimeSheet.ParentCommonTimeSheet.LastDate)
                    ParentTimeSheet.ParentCommonTimeSheet.LastDate = wrkday.Date;
            }
            else
            {
                wrkday.InTime = inTime;
                wrkday.OutTime = outTime;
            }
           
        }
        public WorkDay FindWorkDayByDate(DateTime date)
        {
            foreach (WorkDay wrkDay in WorkDays)
                if (wrkDay.Date == date) return wrkDay;
            return null;
        }


    }
}
