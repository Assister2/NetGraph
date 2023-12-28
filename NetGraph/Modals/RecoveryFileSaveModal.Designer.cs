namespace CyConex
{
    partial class RecoveryFileSaveModal
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RecoveryFileSaveModal));
            this.btnSaveFileAsNew = new Syncfusion.WinForms.Controls.SfButton();
            this.btnDiscardFile = new Syncfusion.WinForms.Controls.SfButton();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnSaveFileAsNew
            // 
            this.btnSaveFileAsNew.AccessibleName = "Button";
            this.btnSaveFileAsNew.Font = new System.Drawing.Font("Segoe UI Semibold", 9F);
            this.btnSaveFileAsNew.Location = new System.Drawing.Point(8, 47);
            this.btnSaveFileAsNew.Name = "btnSaveFileAsNew";
            this.btnSaveFileAsNew.Size = new System.Drawing.Size(198, 28);
            this.btnSaveFileAsNew.TabIndex = 0;
            this.btnSaveFileAsNew.Text = "Save the file as a new file";
            this.btnSaveFileAsNew.Click += new System.EventHandler(this.btnSaveFileAsNew_Click);
            // 
            // btnDiscardFile
            // 
            this.btnDiscardFile.AccessibleName = "Button";
            this.btnDiscardFile.Font = new System.Drawing.Font("Segoe UI Semibold", 9F);
            this.btnDiscardFile.Location = new System.Drawing.Point(231, 47);
            this.btnDiscardFile.Name = "btnDiscardFile";
            this.btnDiscardFile.Size = new System.Drawing.Size(117, 28);
            this.btnDiscardFile.TabIndex = 0;
            this.btnDiscardFile.Text = "Discard the file ";
            this.btnDiscardFile.Click += new System.EventHandler(this.btnDiscardFile_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(343, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "A recovery file has been found. What would you like to do with this file?";
            // 
            // RecoveryFileSaveModal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(360, 94);
            this.CloseButtonVisible = false;
            this.ControlBox = false;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnDiscardFile);
            this.Controls.Add(this.btnSaveFileAsNew);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "RecoveryFileSaveModal";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Style.MdiChild.IconHorizontalAlignment = System.Windows.Forms.HorizontalAlignment.Center;
            this.Style.MdiChild.IconVerticalAlignment = System.Windows.Forms.VisualStyles.VerticalAlignment.Center;
            this.Text = "Recovery File Save Options";
            this.TopMost = true;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Syncfusion.WinForms.Controls.SfButton btnSaveFileAsNew;
        private Syncfusion.WinForms.Controls.SfButton btnDiscardFile;
        private System.Windows.Forms.Label label1;
    }
}