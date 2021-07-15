using Microsoft.Office.Tools.Ribbon;
using System;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
using EmplCRMClassLibrary.Models;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Threading;
using System.IO;
using System.Reflection;
using System.Collections.ObjectModel;
using System.Linq;

namespace EmplCAM
{
    public partial class MainRibbon
    {
        BackgroundWorker bw;
        FormProgressBar prForm;
        ProgressBar commonProgressBar;
        Label commonProgressBarLabel;
        string fileContent;
        string filePath;
        string safefilePath;
        string templPath;
        string templPathOut;
        DateTime ScoreDate;
        private void MainRibbon_Load(object sender, RibbonUIEventArgs e)
        {
            //  var uri = new Uri("Maxls", UriKind.Relative);
            bw = new BackgroundWorker();
            bw.WorkerReportsProgress = true;
            bw.WorkerSupportsCancellation = true;
            bw.DoWork += new DoWorkEventHandler(bw_DoWork);
            bw.ProgressChanged += new ProgressChangedEventHandler(bw_ProgressChanged);
            bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bw_RunWorkerCompleted);
        }

        private void buttonLoadFile_Click(object sender, RibbonControlEventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "c:\\";
                openFileDialog.Filter = "txt files (*.xls)|*.xlsm|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    filePath = openFileDialog.FileName;
                    buttonLoadTemplate.Enabled = true;

                }

            }
        }
        public DateTime JoinDateToTime(DateTime date, DateTime time)
        {
            DateTime outTime;
            outTime = new DateTime(date.Year, date.Month, date.Day) + time.TimeOfDay;
            return outTime;
        }

        void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            string workingDirectory = Environment.CurrentDirectory;
            string projectDirectory = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            Excel.Application xlApp = new Excel.Application();
            xlApp.Visible = false;
            Excel.Workbook templWB = xlApp.Workbooks.Open(templPath);
            Excel.Worksheet templWorksheet = templWB.Worksheets[1];
            Excel.Workbook jornalWB = xlApp.Workbooks.Open(filePath);
            Excel.Worksheet jornalWorksheet = jornalWB.Worksheets[1];


            CommonTimeSheet _commonTimeSheet = new CommonTimeSheet();
            _commonTimeSheet.CreateTimeSheet(ref templWorksheet, ref jornalWorksheet, ScoreDate,
                ((BackgroundWorker)sender).ReportProgress);

            jornalWB.Close();

            templPathOut = filePath.Substring(0, filePath.LastIndexOf(@"\") + 1) + "T13  " + DateTime.Now.ToShortDateString() + ".xls";
            templWB.SaveAs(templPathOut);
            if (!checkBox1.Checked && !checkBox1.Checked || checkBox1.Checked)

                templWB.Close();

        }
        void bw_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            ProgressBarUserState parameters = e.UserState as ProgressBarUserState;
            commonProgressBar.Minimum = parameters.Min;
            commonProgressBar.Maximum = parameters.Max + 1;
            commonProgressBar.Value = parameters.Value;
            commonProgressBarLabel.Text = parameters.Massege;
        }
        void bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {


            commonProgressBarLabel.Text = "Работа окончена!";
            MessageBox.Show(commonProgressBarLabel.Text + "\n Файл сохранен в " + templPathOut);
            buttonRun.Enabled = false;
            prForm.Hide();
        }

        private void buttonRun_Click(object sender, RibbonControlEventArgs e)
        {
            if (checkBoxScoreDate.Checked) ScoreDate = DateTime.Now;
            else ScoreDate = DateTime.MinValue;
            prForm = new FormProgressBar();
            commonProgressBar = (ProgressBar)prForm.Controls["progressBarCommon"];
            commonProgressBarLabel = (Label)prForm.Controls["labelCommon"];
            prForm.Text = filePath;
            prForm.Show();
            bw.RunWorkerAsync();
        }

        private void buttonLoadTemplate_Click(object sender, RibbonControlEventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "c:\\";
                openFileDialog.Filter = "txt files (*.xls)|*.xlsm|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    templPath = openFileDialog.FileName;
                    buttonRun.Enabled = true;

                }

            }
        }

        private void buttonOpenSBFile_Click(object sender, RibbonControlEventArgs e)
        {

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "c:\\";
                openFileDialog.Filter = "txt files (*.xls)|*.xlsm|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    templPath = openFileDialog.FileName;
                    buttonCreateReportSB.Enabled = true;

                }

            }




        }

        private void buttonCreateReportSB_Click(object sender, RibbonControlEventArgs e)
        {
            string workingDirectory = Environment.CurrentDirectory;
            string projectDirectory = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            Excel.Application xlApp = new Excel.Application();
            xlApp.Visible = true;
            Excel.Workbook securityServiceRecordFileWB = xlApp.Workbooks.Open(templPath);
            Excel.Worksheet securityServiceFileWorksheet = securityServiceRecordFileWB.Worksheets["TDSheet"];
            const int EMPTY_ROW_BOUDARY = 20;
            const int FIRST_ROW_POINTER = 7;
            int emptyRowCounter = 0;
            int sbFileRowPointer = 0;
            int rowMaxCounter = 0;
            int rowPointer = 0;
            SecurityServiceRecordJournal securityServiceRecordJournal  = new SecurityServiceRecordJournal();

            while (securityServiceFileWorksheet.Cells[FIRST_ROW_POINTER + sbFileRowPointer, 1].Value != null)
            {
                ResultStatus resultStatus = ResultStatus.Unsettled;
                string employeeFullName = securityServiceFileWorksheet.Cells[FIRST_ROW_POINTER + sbFileRowPointer, 2].Value;
                string strResultStatus = securityServiceFileWorksheet.Cells[FIRST_ROW_POINTER + sbFileRowPointer, 10].Value;

                if (strResultStatus.Contains("Согласовано") && !strResultStatus.Contains("Согласовано с замечаниями"))
                    resultStatus = ResultStatus.Agreed;
                if (strResultStatus.Contains("Согласовано с замечаниями"))
                    resultStatus = ResultStatus.AgreedWithComments;
                securityServiceRecordJournal.SecurityServiceRecords.Add(new SecurityServiceRecord(employeeFullName, resultStatus));
                sbFileRowPointer++;
            }

            securityServiceRecordFileWB.Worksheets.Add();

            Excel.Worksheet securityServiceReportWorksheet = securityServiceRecordFileWB.Worksheets[1];
            securityServiceReportWorksheet.Name = "Отчет";
            int outputRowPointer = 1;
            securityServiceReportWorksheet.Cells[outputRowPointer, 1] = "№ п/п";
            securityServiceReportWorksheet.Cells[outputRowPointer, 2] = "Документ.Ответственный";
            securityServiceReportWorksheet.Cells[outputRowPointer, 3] = "Результат согласования";
            securityServiceReportWorksheet.Cells[outputRowPointer, 4] = "Количество";
            outputRowPointer++;
            foreach (Employee empl in securityServiceRecordJournal.Employees)
            {
                int agreedNumber = securityServiceRecordJournal.SecurityServiceRecords
                  .Where(em => em.FullName == empl.FullName)
                  .Where(sr => sr.ResultStatus == ResultStatus.Agreed).Count();
                int agreedWithCommentsNumber = securityServiceRecordJournal.SecurityServiceRecords
                  .Where(em => em.FullName == empl.FullName)
                  .Where(sr => sr.ResultStatus == ResultStatus.AgreedWithComments).Count();
                if(agreedNumber>0)
                {

                    securityServiceReportWorksheet.Cells[outputRowPointer, 1] = outputRowPointer-1;
                    securityServiceReportWorksheet.Cells[outputRowPointer , 2] = empl.FullName;
                    securityServiceReportWorksheet.Cells[outputRowPointer , 3] = "Согласовано";
                    securityServiceReportWorksheet.Cells[outputRowPointer , 4] = agreedNumber;

                    outputRowPointer++;
                }
                if (agreedWithCommentsNumber > 0)
                {
                    securityServiceReportWorksheet.Cells[outputRowPointer, 1] = outputRowPointer-1;
                    securityServiceReportWorksheet.Cells[outputRowPointer , 2] = empl.FullName;
                    securityServiceReportWorksheet.Cells[outputRowPointer , 3] = "Согласовано с замечаниями";
                    securityServiceReportWorksheet.Cells[outputRowPointer , 4] = agreedWithCommentsNumber;
                    outputRowPointer++;
                }

            }
            string savePath = templPath.Replace(".xlsx", "") + " " + DateTime.Now.ToString("D") + ".xlsx";
            securityServiceRecordFileWB.SaveAs(savePath);
        }
    }
}