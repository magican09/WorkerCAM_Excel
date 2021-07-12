
using System;

namespace EmplCAM
{
    partial class MainRibbon : Microsoft.Office.Tools.Ribbon.RibbonBase
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

       
        public MainRibbon()
            : base(Globals.Factory.GetRibbonFactory())
        {
            InitializeComponent();
        }

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tab1 = this.Factory.CreateRibbonTab();
            this.group1 = this.Factory.CreateRibbonGroup();
            this.buttonLoadFile = this.Factory.CreateRibbonButton();
            this.buttonRun = this.Factory.CreateRibbonButton();
            this.checkBox2 = this.Factory.CreateRibbonCheckBox();
            this.buttonLoadTemplate = this.Factory.CreateRibbonButton();
            this.checkBox1 = this.Factory.CreateRibbonCheckBox();
            this.label1 = this.Factory.CreateRibbonLabel();
            this.checkBoxScoreDate = this.Factory.CreateRibbonCheckBox();
            this.tab1.SuspendLayout();
            this.group1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tab1
            // 
            this.tab1.ControlId.ControlIdType = Microsoft.Office.Tools.Ribbon.RibbonControlIdType.Office;
            this.tab1.Groups.Add(this.group1);
            this.tab1.Label = "WorkerCAM";
            this.tab1.Name = "tab1";
            // 
            // group1
            // 
            this.group1.Items.Add(this.buttonLoadFile);
            this.group1.Items.Add(this.buttonRun);
            this.group1.Items.Add(this.checkBox2);
            this.group1.Items.Add(this.buttonLoadTemplate);
            this.group1.Items.Add(this.checkBox1);
            this.group1.Items.Add(this.label1);
            this.group1.Items.Add(this.checkBoxScoreDate);
            this.group1.Label = "Табелирование";
            this.group1.Name = "group1";
            // 
            // buttonLoadFile
            // 
            this.buttonLoadFile.Label = "Выбрать журнал";
            this.buttonLoadFile.Name = "buttonLoadFile";
            this.buttonLoadFile.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.buttonLoadFile_Click);
            // 
            // buttonRun
            // 
            this.buttonRun.Enabled = false;
            this.buttonRun.Label = "Запуск";
            this.buttonRun.Name = "buttonRun";
            this.buttonRun.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.buttonRun_Click);
            // 
            // checkBox2
            // 
            this.checkBox2.Checked = true;
            this.checkBox2.Enabled = false;
            this.checkBox2.Label = "Скрыть вывод.";
            this.checkBox2.Name = "checkBox2";
            // 
            // buttonLoadTemplate
            // 
            this.buttonLoadTemplate.Enabled = false;
            this.buttonLoadTemplate.Label = "Выбрать шаблон табеля";
            this.buttonLoadTemplate.Name = "buttonLoadTemplate";
            this.buttonLoadTemplate.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.buttonLoadTemplate_Click);
            // 
            // checkBox1
            // 
            this.checkBox1.Checked = true;
            this.checkBox1.Enabled = false;
            this.checkBox1.Label = "Закрыть после окончания.";
            this.checkBox1.Name = "checkBox1";
            // 
            // label1
            // 
            this.label1.Label = "label1dd";
            this.label1.Name = "label1";
            // 
            // checkBoxScoreDate
            // 
            this.checkBoxScoreDate.Enabled = false;
            this.checkBoxScoreDate.Label = "Выбрать текущую дату конечной";
            this.checkBoxScoreDate.Name = "checkBoxScoreDate";
            // 
            // MainRibbon
            // 
            this.Name = "MainRibbon";
            this.RibbonType = "Microsoft.Excel.Workbook";
            this.Tabs.Add(this.tab1);
            this.Load += new Microsoft.Office.Tools.Ribbon.RibbonUIEventHandler(this.MainRibbon_Load);
            this.tab1.ResumeLayout(false);
            this.tab1.PerformLayout();
            this.group1.ResumeLayout(false);
            this.group1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        internal Microsoft.Office.Tools.Ribbon.RibbonTab tab1;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup group1;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton buttonLoadFile;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton buttonRun;
        internal Microsoft.Office.Tools.Ribbon.RibbonCheckBox checkBox1;
        internal Microsoft.Office.Tools.Ribbon.RibbonCheckBox checkBox2;
        internal Microsoft.Office.Tools.Ribbon.RibbonLabel label1;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton buttonLoadTemplate;
        internal Microsoft.Office.Tools.Ribbon.RibbonCheckBox checkBoxScoreDate;
    }

    partial class ThisRibbonCollection
    {
        internal MainRibbon MainRibbon
        {
            get { return this.GetRibbon<MainRibbon>(); }
        }
    }
}
