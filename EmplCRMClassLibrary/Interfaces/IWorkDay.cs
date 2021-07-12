using System;

namespace EmplCRMClassLibrary.Interfaces
{
    public interface IWorkDay
    {
         DateTime InTime { get; set; }
         DateTime OutTime { get; set; }
         DateTime Date { get; set; }
         IWorkMonth ParentWorketMonth { get; set; }
         bool IsAbsent { get; set; }
    }
}
