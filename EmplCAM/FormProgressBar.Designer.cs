
namespace EmplCAM
{
    partial class FormProgressBar
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.progressBarCommon = new System.Windows.Forms.ProgressBar();
            this.labelCommon = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // progressBarCommon
            // 
            this.progressBarCommon.Location = new System.Drawing.Point(42, 48);
            this.progressBarCommon.Name = "progressBarCommon";
            this.progressBarCommon.Size = new System.Drawing.Size(1029, 23);
            this.progressBarCommon.TabIndex = 1;
            // 
            // labelCommon
            // 
            this.labelCommon.AutoSize = true;
            this.labelCommon.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelCommon.Location = new System.Drawing.Point(39, 21);
            this.labelCommon.Name = "labelCommon";
            this.labelCommon.Size = new System.Drawing.Size(21, 20);
            this.labelCommon.TabIndex = 3;
            this.labelCommon.Text = "...";
            // 
            // FormProgressBar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1101, 141);
            this.Controls.Add(this.labelCommon);
            this.Controls.Add(this.progressBarCommon);
            this.Name = "FormProgressBar";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ProgressBar progressBarCommon;
        private System.Windows.Forms.Label labelCommon;
    }
}