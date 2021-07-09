using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Excel = Microsoft.Office.Interop.Excel;
using Office = Microsoft.Office.Core;
using Microsoft.Office.Tools.Excel;
using Microsoft.Office.Tools.Ribbon;
using System.IO;

namespace EmplCAM
{
    public partial class ThisAddIn
    {
        private void ThisAddIn_Startup(object sender, System.EventArgs e)
        {
        }

        private void ThisAddIn_Shutdown(object sender, System.EventArgs e)
        {
        }

        #region VSTO generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InternalStartup()
        {
            this.Startup += new System.EventHandler(ThisAddIn_Startup);
            this.Shutdown += new System.EventHandler(ThisAddIn_Shutdown);
        }
        protected override Microsoft.Office.Core.IRibbonExtensibility CreateRibbonExtensibilityObject()
        {
            var ribbon = new MainRibbon();
            string workingDirectory = Environment.CurrentDirectory;
            string projectDirectory = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            projectDirectory = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase);///
            projectDirectory = AppDomain.CurrentDomain.BaseDirectory;// Assembly.GetExecutingAssembly().Location; 
           /// projectDirectory = Application.StartupPath.ToString();
            projectDirectory = Directory.GetCurrentDirectory();
            projectDirectory = AppDomain.CurrentDomain.BaseDirectory;
            ribbon.label1.Label = projectDirectory;
            // ribbon.ButtonClicked += ribbon_ButtonClicked;
            return Globals.Factory.GetRibbonFactory().CreateRibbonManager(new IRibbonExtension[] { ribbon });
        }

        #endregion
    }
}
