using EmplCRMClassLibrary.Interfaces;
using System;
using System.Collections.ObjectModel;
using Excel = Microsoft.Office.Interop.Excel;

namespace EmplCRMClassLibrary.Models
{
    public class CommonTimeSheet : ICommonTimeSheet
    {
        public ObservableCollection<IWorkerTimeSheet> WorkerTimeSheets { get; set; }
        public ObservableCollection<IEmployee> Employees { get; set; }
        const int EMPTY_ROW_BOUDARY = 20; //Количество пустых строк после который файл журнала Сигура считаем законченным
        const int JORNAL_FIRST_ROW = 9;// Первая строка в исходном файле журнала их Сигура
        const int TEMPLATE_FIRST_ROW = 13;//Первая строка в шаблоне Т13 
        const int TEMPLATE_COLUMNS_NUMBER = 40;//Количество столбцов в табеле Т13. Если добавили стобцы в табель - увеличить ...
        public CommonTimeSheet()
        {
            WorkerTimeSheets = new ObservableCollection<IWorkerTimeSheet>();
            Employees = new ObservableCollection<IEmployee>();
        }
        public void SetEmployeeVisit(DateTime date, string fullName, DateTime inTime, DateTime outTime)
        {
            WorkerTimeSheet timeSheet;
            timeSheet = FindWorkerTimeSheetByFullName(fullName);//Поиск работника по полному имени.
            if (timeSheet == null)
            {
                WorkerTimeSheets.Add(new WorkerTimeSheet(fullName));
                timeSheet = (WorkerTimeSheet)WorkerTimeSheets[WorkerTimeSheets.Count - 1];
            }
            timeSheet.WorkMonth.AddWorkDay(date, inTime, outTime);


        }
        public WorkerTimeSheet FindWorkerTimeSheetByFullName(string fullName)
        {
            foreach (WorkerTimeSheet tsheet in WorkerTimeSheets)
                if (tsheet.Employee.FullName == fullName) return tsheet;
            return null;
        }
        public void SortTimeSheets()
        {
            foreach (WorkerTimeSheet timeSheet in WorkerTimeSheets)
                timeSheet.WorkMonth.SortDaysByDate();

        }
        public void CreateTimeSheet(ref Excel.Worksheet templWorksheet, ref Excel.Worksheet jornalWorksheet,
           Action<int, object> ReportProgress = null)
        {

            int emptyRowCounter = 0;
            int rowMaxCounter = 0;
            int jornalRowPointer = JORNAL_FIRST_ROW;
            int templRowPointer = TEMPLATE_FIRST_ROW;
            int lastRowNamber = jornalWorksheet.Cells.SpecialCells(Excel.XlCellType.xlCellTypeLastCell).Row;
            ProgressBarUserState currentProrgessBarParameters = new ProgressBarUserState();

            #region Примерный подсчет непустых строк в таблице
            while (emptyRowCounter < EMPTY_ROW_BOUDARY)
            {

                if (jornalWorksheet.Cells[jornalRowPointer++, 1].Value == null)
                    emptyRowCounter++;
                else
                    emptyRowCounter = 0;
                rowMaxCounter++;
                if (ReportProgress != null)
                {
                    currentProrgessBarParameters.Massege = "Подсчет записей в журнале:" + rowMaxCounter.ToString();
                    currentProrgessBarParameters.Value = rowMaxCounter;
                    currentProrgessBarParameters.Max = rowMaxCounter + 1;
                    ReportProgress(rowMaxCounter, currentProrgessBarParameters);
                }
            }
            #endregion
            currentProrgessBarParameters.Max = jornalRowPointer;
            ObservableCollection<JournalRecord> JournalRecords = new ObservableCollection<JournalRecord>();
            jornalRowPointer = JORNAL_FIRST_ROW;
            emptyRowCounter = 0;
            CommonTimeSheet commonTimeSheet = this;
            #region Обратока входного файла
            while (emptyRowCounter < EMPTY_ROW_BOUDARY)
            {
                if (jornalWorksheet.Cells[jornalRowPointer, 1].Value != null)
                {
                    DateTime date;
                    DateTime.TryParse(jornalWorksheet.Cells[jornalRowPointer, 1].Value.ToString(), out date);
                    string fullName = jornalWorksheet.Cells[jornalRowPointer, 3].Value;
                    JournalRecords.Add(new JournalRecord() { FullName = fullName, Date = date });
                    do
                    {
                        DateTime inTime;
                        if (DateTime.TryParse(jornalWorksheet.Cells[jornalRowPointer, 5].Value, out inTime))
                            inTime = JoinDateToTime(date, inTime);
                        else
                            inTime = DateTime.MinValue;
                        DateTime outTime;

                        if (DateTime.TryParse(jornalWorksheet.Cells[jornalRowPointer, 7].Value, out outTime))
                            outTime = JoinDateToTime(date, outTime);
                        else
                            outTime = DateTime.MinValue;

                        JournalRecords[JournalRecords.Count - 1].JournalRecords.Add(new JournalRecord { InTime = inTime, OutTime = outTime });

                        jornalRowPointer++;
                        emptyRowCounter++;
                    }
                    while (jornalWorksheet.Cells[jornalRowPointer, 1].Value == null && emptyRowCounter < EMPTY_ROW_BOUDARY);

                    emptyRowCounter = 0;
                    jornalRowPointer--;
                }
                else
                    emptyRowCounter++;
                jornalRowPointer++;
                #region Вывод строки на рибоном
                if (ReportProgress != null)
                {
                    currentProrgessBarParameters.Value = jornalRowPointer;
                    currentProrgessBarParameters.Massege = "Обработано записей в журнале: " + jornalRowPointer.ToString() + "   из  " + rowMaxCounter.ToString();
                    if (currentProrgessBarParameters.Max < jornalRowPointer)
                        currentProrgessBarParameters.Max = jornalRowPointer + 1;
                    ReportProgress(jornalRowPointer, currentProrgessBarParameters);
                }
                #endregion
            }
            #endregion
            foreach (JournalRecord jrecord in JournalRecords)
            {
                commonTimeSheet.SetEmployeeVisit(jrecord.Date, jrecord.FullName, jrecord.InTime, jrecord.OutTime);
            }
            #region Формивание выходного табеля
            int emplCounter = 1;
            commonTimeSheet.SortTimeSheets();
            currentProrgessBarParameters.Max = commonTimeSheet.WorkerTimeSheets.Count;
            foreach (WorkerTimeSheet wrkTimeSheet in commonTimeSheet.WorkerTimeSheets)
            {
                #region Компирование строк в табеле для новой записи
                templWorksheet.Range[templWorksheet.Cells[templRowPointer, 1],
                        templWorksheet.Cells[templRowPointer + 15, TEMPLATE_COLUMNS_NUMBER]].Copy();
                templWorksheet.Range[templWorksheet.Cells[templRowPointer + 2, 1],
                        templWorksheet.Cells[templRowPointer + 15 + 2, TEMPLATE_COLUMNS_NUMBER]].PasteSpecial(Excel.XlPasteType.xlPasteAll);
                #endregion 
                templWorksheet.Cells[templRowPointer, 1] = emplCounter;
                templWorksheet.Cells[templRowPointer, 2] = wrkTimeSheet.Employee.FullName;
                templWorksheet.Cells[templRowPointer, 37] = wrkTimeSheet.WorkMonth.WorkedTime;
                int workedDaysFirstHalfOfMonth = 0;
                int workedDaysSecondHalfOfMonth = 0;
                foreach (WorkDay wrDay in wrkTimeSheet.WorkMonth.WorkDays)
                {
                    if (wrDay.Date.Day <= 15)
                    {
                        templWorksheet.Cells[templRowPointer, 5 + wrDay.Date.Day] = wrDay.DayStatus;
                        if (wrDay.DayStatus == "З") templWorksheet.Cells[templRowPointer, 5 + wrDay.Date.Day].Interior.Color = Excel.XlRgbColor.rgbYellow;
                        templWorksheet.Cells[templRowPointer + 1, 5 + wrDay.Date.Day] = wrDay.WorkeTime.ToString("0.##");
                        if (wrDay.WorkeTime < 4 && wrDay.WorkeTime > 0) templWorksheet.Cells[templRowPointer + 1, 5 + wrDay.Date.Day].Interior.Color = Excel.XlRgbColor.rgbRed;

                        if (wrDay.Date.DayOfWeek == DayOfWeek.Saturday || wrDay.Date.DayOfWeek == DayOfWeek.Sunday)
                            templWorksheet.Cells[templRowPointer, 5 + wrDay.Date.Day].Interior.Color = Excel.XlRgbColor.rgbBlue;
                        workedDaysFirstHalfOfMonth++;
                    }
                    else if (wrDay.Date.Day > 15)
                    {
                        templWorksheet.Cells[templRowPointer, 5 + wrDay.Date.Day + 1] = wrDay.DayStatus;
                        if (wrDay.DayStatus == "З") templWorksheet.Cells[templRowPointer, 5 + wrDay.Date.Day + 1].Interior.Color = Excel.XlRgbColor.rgbYellow;
                        templWorksheet.Cells[templRowPointer + 1, 5 + wrDay.Date.Day + 1] = wrDay.WorkeTime.ToString("0.##");
                        if (wrDay.WorkeTime < 4 && wrDay.WorkeTime > 0) templWorksheet.Cells[templRowPointer + 1, 5 + wrDay.Date.Day + 1].Interior.Color = Excel.XlRgbColor.rgbRed;

                        if (wrDay.Date.DayOfWeek == DayOfWeek.Saturday || wrDay.Date.DayOfWeek == DayOfWeek.Sunday)
                            templWorksheet.Cells[templRowPointer + 1, 5 + wrDay.Date.Day + 1].Interior.Color = Excel.XlRgbColor.rgbBlue;
                        workedDaysSecondHalfOfMonth++;
                    }
                    

                }
                if(workedDaysFirstHalfOfMonth != 0) templWorksheet.Cells[templRowPointer, 5 + 16] = workedDaysFirstHalfOfMonth;
                templWorksheet.Cells[templRowPointer, 5 + 33] = workedDaysSecondHalfOfMonth+ workedDaysFirstHalfOfMonth;

                templRowPointer += 2;
                emplCounter++;
                #region Вывод строки на рибоном
                if (ReportProgress != null)
                {
                    currentProrgessBarParameters.Massege = "Сформировано на " + emplCounter.ToString() + " рабоников из " +
                        commonTimeSheet.WorkerTimeSheets.Count.ToString() + " (табель на:" + wrkTimeSheet.Employee.FullName + ")";
                    currentProrgessBarParameters.Value = emplCounter;
                    ReportProgress(emplCounter, currentProrgessBarParameters);
                }
                #endregion
            }
            #endregion


        }
        public DateTime JoinDateToTime(DateTime date, DateTime time)
        {
            DateTime outTime;
            outTime = new DateTime(date.Year, date.Month, date.Day) + time.TimeOfDay;
            return outTime;
        }

    }
}
