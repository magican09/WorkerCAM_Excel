using EmplCRMClassLibrary.Interfaces;
using System;

namespace EmplCRMClassLibrary.Models
{
    public class WorkDay : IWorkDay
    {
        private DateTime _inTime ;
        private DateTime _outTime;
        private double _workeTime;

        private string _dayStatus;
        public double WorkeTime
        {
            get
            {
                if (_inTime.ToShortTimeString() != "0:00" && _outTime.ToShortTimeString() != "0:00")

                    _workeTime = (_outTime - _inTime).Hours;
                return _workeTime;
            }
            set
            {
                // if(_inTime.ToShortTimeString() != "0:00"&& _inTime.ToShortTimeString() != "0:00")

                  //  _workeTime = (_inTime- _inTime).Hours;
            }
        }
        public DateTime InTime 
        { 
            get 
            {
               
                return _inTime;
            } 
            set {
              
                //if ( value <= _inTime )
                {
                    _inTime = value;
                    CalcDayStatus();
                }
               
               
            } 
        }
        public DateTime OutTime
        {
            get
            {
  
                return _outTime;
              
            }
            set
            {
             //        if (  value >= _outTime)
                {
                    _outTime = value;
                }
               // CalcDayStatus();


            }
        }
        public DateTime Date { get; set; }
        public string DayStatus 
        {
            get {
                CalcDayStatus();
                return _dayStatus;
            }

            set { _dayStatus = value; } }

        public WorkDay()
        {
            /*_inTime = DateTime.MaxValue;
            _outTime = DateTime.MaxValue;
             Date = DateTime.MaxValue;*/
        }
        public WorkDay(DateTime date, DateTime inTime, DateTime outTime)
        {
           /* _inTime = DateTime.MaxValue;
            _outTime = DateTime.MaxValue;
            Date = DateTime.MaxValue;*/

            InTime = inTime;
            OutTime = outTime;
            Date = date;
        }
        public void CalcDayStatus()
        {
            if (InTime != DateTime.MinValue && OutTime != DateTime.MinValue)
                _dayStatus = "Я";
            else if (InTime == DateTime.MinValue && OutTime == DateTime.MinValue)
                _dayStatus = "НН";
            else  if (InTime != DateTime.MinValue && OutTime == DateTime.MinValue)
                _dayStatus = "З";
            
         //  ((OutTime-InTime).Hours)
           

        }
        public void AjustTimes()
        {
            if (_inTime > _outTime)
                SwapTimes(ref _inTime, ref _outTime);
        }

             

        private void SwapTimes(ref DateTime  _inDate, ref DateTime _outDate)
        {
            DateTime buf_date;
          
                buf_date = _inDate;
                _inDate = _outDate;
                _outDate = buf_date;


        }
    }
}
