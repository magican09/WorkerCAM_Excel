using Microsoft.Office.Tools.Ribbon;
using System;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
using EmplCRMClassLibrary.Models;
using System.ComponentModel;
using System.Threading;
using System.IO;
using System.Reflection;
using System.Collections.ObjectModel;

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
            xlApp.Visible = false ;
            Excel.Workbook templWB = xlApp.Workbooks.Open(templPath);
            Excel.Worksheet templWorksheet = templWB.Worksheets[1];
            Excel.Workbook jornalWB = xlApp.Workbooks.Open(filePath);
            Excel.Worksheet jornalWorksheet = jornalWB.Worksheets[1];
             

            CommonTimeSheet _commonTimeSheet = new CommonTimeSheet();
            _commonTimeSheet.CreateTimeSheet(ref templWorksheet, ref jornalWorksheet,ScoreDate,
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
    }
}