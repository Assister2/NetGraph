namespace CyConex
{
    partial class EdgeStrengthModal
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EdgeStrengthModal));
            this.autoLabel1 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.autoLabel2 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.autoLabel3 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.txtRStrengthStatus = new Syncfusion.Windows.Forms.Tools.TextBoxExt();
            this.rTextRStrengthDesc = new System.Windows.Forms.RichTextBox();
            this.btnRStrengthSave = new Syncfusion.WinForms.Controls.SfButton();
            this.btnCancel = new Syncfusion.WinForms.Controls.SfButton();
            this.txtRStrengthValue = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.txtRStrengthStatus)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRStrengthValue)).BeginInit();
            this.SuspendLayout();
            // 
            // autoLabel1
            // 
            this.autoLabel1.Location = new System.Drawing.Point(6, 12);
            this.autoLabel1.Name = "autoLabel1";
            this.autoLabel1.Size = new System.Drawing.Size(108, 13);
            this.autoLabel1.TabIndex = 0;
            this.autoLabel1.Text = "Relationship Strength";
            // 
            // autoLabel2
            // 
            this.autoLabel2.Location = new System.Drawing.Point(237, 13);
            this.autoLabel2.Name = "autoLabel2";
            this.autoLabel2.Size = new System.Drawing.Size(34, 13);
            this.autoLabel2.TabIndex = 1;
            this.autoLabel2.Text = "Value";
            // 
            // autoLabel3
            // 
            this.autoLabel3.Location = new System.Drawing.Point(5, 66);
            this.autoLabel3.Name = "autoLabel3";
            this.autoLabel3.Size = new System.Drawing.Size(60, 13);
            this.autoLabel3.TabIndex = 2;
            this.autoLabel3.Text = "Description";
            // 
            // txtRStrengthStatus
            // 
            this.txtRStrengthStatus.BeforeTouchSize = new System.Drawing.Size(227, 23);
            this.txtRStrengthStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.txtRStrengthStatus.Location = new System.Drawing.Point(6, 28);
            this.txtRStrengthStatus.Name = "txtRStrengthStatus";
            this.txtRStrengthStatus.Size = new System.Drawing.Size(227, 23);
            this.txtRStrengthStatus.TabIndex = 1;
            // 
            // rTextRStrengthDesc
            // 
            this.rTextRStrengthDesc.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.rTextRStrengthDesc.Location = new System.Drawing.Point(5, 82);
            this.rTextRStrengthDesc.Name = "rTextRStrengthDesc";
            this.rTextRStrengthDesc.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.rTextRStrengthDesc.Size = new System.Drawing.Size(292, 98);
            this.rTextRStrengthDesc.TabIndex = 3;
            this.rTextRStrengthDesc.Text = "";
            // 
            // btnRStrengthSave
            // 
            this.btnRStrengthSave.AccessibleName = "Button";
            this.btnRStrengthSave.Font = new System.Drawing.Font("Segoe UI Semibold", 9F);
            this.btnRStrengthSave.Location = new System.Drawing.Point(147, 186);
            this.btnRStrengthSave.Name = "btnRStrengthSave";
            this.btnRStrengthSave.Size = new System.Drawing.Size(72, 28);
            this.btnRStrengthSave.TabIndex = 4;
            this.btnRStrengthSave.Text = "Ok";
            this.btnRStrengthSave.Click += new System.EventHandler(this.btnRStrengthSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.AccessibleName = "Button";
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI Semibold", 9F);
            this.btnCancel.Location = new System.Drawing.Point(225, 186);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(72, 28);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // txtRStrengthValue
            // 
            this.txtRStrengthValue.DecimalPlaces = 2;
            this.txtRStrengthValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRStrengthValue.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.txtRStrengthValue.Location = new System.Drawing.Point(239, 29);
            this.txtRStrengthValue.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.txtRStrengthValue.Name = "txtRStrengthValue";
            this.txtRStrengthValue.Size = new System.Drawing.Size(58, 23);
            this.txtRStrengthValue.TabIndex = 11;
            this.txtRStrengthValue.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // EdgeStrengthModal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(302, 227);
            this.CloseButtonVisible = false;
            this.ControlBox = false;
            this.Controls.Add(this.txtRStrengthValue);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnRStrengthSave);
            this.Controls.Add(this.rTextRStrengthDesc);
            this.Controls.Add(this.txtRStrengthStatus);
            this.Controls.Add(this.autoLabel3);
            this.Controls.Add(this.autoLabel2);
            this.Controls.Add(this.autoLabel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "EdgeStrengthModal";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Style.MdiChild.IconHorizontalAlignment = System.Windows.Forms.HorizontalAlignment.Center;
            this.Style.MdiChild.IconVerticalAlignment = System.Windows.Forms.VisualStyles.VerticalAlignment.Center;
            this.Text = "Relation Strength";
            this.TopMost = true;
            ((System.ComponentModel.ISupportInitialize)(this.txtRStrengthStatus)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRStrengthValue)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel1;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel2;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel3;
        private Syncfusion.Windows.Forms.Tools.TextBoxExt txtRStrengthStatus;
        private System.Windows.Forms.RichTextBox rTextRStrengthDesc;
        private Syncfusion.WinForms.Controls.SfButton btnRStrengthSave;
        private Syncfusion.WinForms.Controls.SfButton btnCancel;
        private System.Windows.Forms.NumericUpDown txtRStrengthValue;
    }
}