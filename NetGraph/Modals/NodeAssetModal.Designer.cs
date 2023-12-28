namespace CyConex
{
    partial class NodeAssetModal
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NodeAssetModal));
            this.autoLabel1 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.autoLabel2 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.autoLabel3 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.txtAssStatus = new Syncfusion.Windows.Forms.Tools.TextBoxExt();
            this.btnAssessmentSave = new Syncfusion.WinForms.Controls.SfButton();
            this.btnCancel = new Syncfusion.WinForms.Controls.SfButton();
            this.autoLabel4 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.txtAssColor = new System.Windows.Forms.Panel();
            this.txtAssValue = new System.Windows.Forms.NumericUpDown();
            this.rTextAssDesc = new Syncfusion.Windows.Forms.Tools.TextBoxExt();
            ((System.ComponentModel.ISupportInitialize)(this.txtAssStatus)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAssValue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rTextAssDesc)).BeginInit();
            this.SuspendLayout();
            // 
            // autoLabel1
            // 
            this.autoLabel1.Location = new System.Drawing.Point(5, 13);
            this.autoLabel1.Name = "autoLabel1";
            this.autoLabel1.Size = new System.Drawing.Size(101, 13);
            this.autoLabel1.TabIndex = 0;
            this.autoLabel1.Text = "Implementation Title";
            // 
            // autoLabel2
            // 
            this.autoLabel2.Location = new System.Drawing.Point(229, 13);
            this.autoLabel2.Name = "autoLabel2";
            this.autoLabel2.Size = new System.Drawing.Size(34, 13);
            this.autoLabel2.TabIndex = 1;
            this.autoLabel2.Text = "Value";
            this.autoLabel2.Click += new System.EventHandler(this.autoLabel2_Click);
            // 
            // autoLabel3
            // 
            this.autoLabel3.Location = new System.Drawing.Point(5, 62);
            this.autoLabel3.Name = "autoLabel3";
            this.autoLabel3.Size = new System.Drawing.Size(60, 13);
            this.autoLabel3.TabIndex = 2;
            this.autoLabel3.Text = "Description";
            // 
            // txtAssStatus
            // 
            this.txtAssStatus.BeforeTouchSize = new System.Drawing.Size(417, 136);
            this.txtAssStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAssStatus.Location = new System.Drawing.Point(6, 30);
            this.txtAssStatus.Name = "txtAssStatus";
            this.txtAssStatus.Size = new System.Drawing.Size(217, 23);
            this.txtAssStatus.TabIndex = 1;
            this.txtAssStatus.TabStop = false;
            // 
            // btnAssessmentSave
            // 
            this.btnAssessmentSave.AccessibleName = "Button";
            this.btnAssessmentSave.Font = new System.Drawing.Font("Segoe UI Semibold", 9F);
            this.btnAssessmentSave.Location = new System.Drawing.Point(173, 173);
            this.btnAssessmentSave.Name = "btnAssessmentSave";
            this.btnAssessmentSave.Size = new System.Drawing.Size(72, 28);
            this.btnAssessmentSave.TabIndex = 4;
            this.btnAssessmentSave.Text = "OK";
            this.btnAssessmentSave.Click += new System.EventHandler(this.btnAssessmentSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.AccessibleName = "Button";
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI Semibold", 9F);
            this.btnCancel.Location = new System.Drawing.Point(251, 173);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(72, 28);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // autoLabel4
            // 
            this.autoLabel4.Location = new System.Drawing.Point(292, 13);
            this.autoLabel4.Name = "autoLabel4";
            this.autoLabel4.Size = new System.Drawing.Size(31, 13);
            this.autoLabel4.TabIndex = 6;
            this.autoLabel4.Text = "Color";
            // 
            // txtAssColor
            // 
            this.txtAssColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtAssColor.Location = new System.Drawing.Point(293, 30);
            this.txtAssColor.Name = "txtAssColor";
            this.txtAssColor.Size = new System.Drawing.Size(30, 23);
            this.txtAssColor.TabIndex = 11;
            this.txtAssColor.Click += new System.EventHandler(this.txtAssColor_Click);
            // 
            // txtAssValue
            // 
            this.txtAssValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAssValue.Location = new System.Drawing.Point(229, 30);
            this.txtAssValue.Name = "txtAssValue";
            this.txtAssValue.Size = new System.Drawing.Size(58, 23);
            this.txtAssValue.TabIndex = 10;
            this.txtAssValue.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // rTextAssDesc
            // 
            this.rTextAssDesc.BeforeTouchSize = new System.Drawing.Size(417, 136);
            this.rTextAssDesc.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rTextAssDesc.Location = new System.Drawing.Point(6, 78);
            this.rTextAssDesc.Multiline = true;
            this.rTextAssDesc.Name = "rTextAssDesc";
            this.rTextAssDesc.Size = new System.Drawing.Size(311, 87);
            this.rTextAssDesc.TabIndex = 3;
            // 
            // NodeAssetModal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(324, 206);
            this.CloseButtonVisible = false;
            this.ControlBox = false;
            this.Controls.Add(this.txtAssColor);
            this.Controls.Add(this.txtAssValue);
            this.Controls.Add(this.autoLabel4);
            this.Controls.Add(this.rTextAssDesc);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnAssessmentSave);
            this.Controls.Add(this.txtAssStatus);
            this.Controls.Add(this.autoLabel3);
            this.Controls.Add(this.autoLabel2);
            this.Controls.Add(this.autoLabel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "NodeAssetModal";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Style.MdiChild.IconHorizontalAlignment = System.Windows.Forms.HorizontalAlignment.Center;
            this.Style.MdiChild.IconVerticalAlignment = System.Windows.Forms.VisualStyles.VerticalAlignment.Center;
            this.Text = "Control Implementation ";
            this.TopMost = true;
            ((System.ComponentModel.ISupportInitialize)(this.txtAssStatus)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAssValue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rTextAssDesc)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel1;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel2;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel3;
        private Syncfusion.Windows.Forms.Tools.TextBoxExt txtAssStatus;
        private Syncfusion.WinForms.Controls.SfButton btnAssessmentSave;
        private Syncfusion.WinForms.Controls.SfButton btnCancel;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel4;
        private System.Windows.Forms.Panel txtAssColor;
        private System.Windows.Forms.NumericUpDown txtAssValue;
        private Syncfusion.Windows.Forms.Tools.TextBoxExt rTextAssDesc;
    }
}