namespace CyConex
{
    partial class NodeAttributeModal
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NodeAttributeModal));
            this.txtNodeAttrImpactDesc = new Syncfusion.Windows.Forms.Tools.TextBoxExt();
            this.btnCancel = new Syncfusion.WinForms.Controls.SfButton();
            this.btnAssessmentSave = new Syncfusion.WinForms.Controls.SfButton();
            this.txtNodeAttrImpact = new Syncfusion.Windows.Forms.Tools.TextBoxExt();
            this.autoLabel3 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.autoLabel2 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.autoLabel1 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.txtNodeAttrImpactValue = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.txtNodeAttrImpactDesc)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNodeAttrImpact)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNodeAttrImpactValue)).BeginInit();
            this.SuspendLayout();
            // 
            // txtNodeAttrImpactDesc
            // 
            this.txtNodeAttrImpactDesc.BeforeTouchSize = new System.Drawing.Size(206, 23);
            this.txtNodeAttrImpactDesc.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNodeAttrImpactDesc.Location = new System.Drawing.Point(5, 80);
            this.txtNodeAttrImpactDesc.Multiline = true;
            this.txtNodeAttrImpactDesc.Name = "txtNodeAttrImpactDesc";
            this.txtNodeAttrImpactDesc.Size = new System.Drawing.Size(270, 65);
            this.txtNodeAttrImpactDesc.TabIndex = 3;
            // 
            // btnCancel
            // 
            this.btnCancel.AccessibleName = "Button";
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI Semibold", 9F);
            this.btnCancel.Location = new System.Drawing.Point(203, 151);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(72, 28);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnAssessmentSave
            // 
            this.btnAssessmentSave.AccessibleName = "Button";
            this.btnAssessmentSave.Font = new System.Drawing.Font("Segoe UI Semibold", 9F);
            this.btnAssessmentSave.Location = new System.Drawing.Point(125, 151);
            this.btnAssessmentSave.Name = "btnAssessmentSave";
            this.btnAssessmentSave.Size = new System.Drawing.Size(72, 28);
            this.btnAssessmentSave.TabIndex = 4;
            this.btnAssessmentSave.Text = "Ok";
            this.btnAssessmentSave.Click += new System.EventHandler(this.btnAssessmentSave_Click);
            // 
            // txtNodeAttrImpact
            // 
            this.txtNodeAttrImpact.BeforeTouchSize = new System.Drawing.Size(206, 23);
            this.txtNodeAttrImpact.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNodeAttrImpact.Location = new System.Drawing.Point(5, 32);
            this.txtNodeAttrImpact.Name = "txtNodeAttrImpact";
            this.txtNodeAttrImpact.Size = new System.Drawing.Size(206, 23);
            this.txtNodeAttrImpact.TabIndex = 1;
            // 
            // autoLabel3
            // 
            this.autoLabel3.Location = new System.Drawing.Point(4, 64);
            this.autoLabel3.Name = "autoLabel3";
            this.autoLabel3.Size = new System.Drawing.Size(60, 13);
            this.autoLabel3.TabIndex = 11;
            this.autoLabel3.Text = "Description";
            // 
            // autoLabel2
            // 
            this.autoLabel2.Location = new System.Drawing.Point(211, 15);
            this.autoLabel2.Name = "autoLabel2";
            this.autoLabel2.Size = new System.Drawing.Size(34, 13);
            this.autoLabel2.TabIndex = 10;
            this.autoLabel2.Text = "Value";
            // 
            // autoLabel1
            // 
            this.autoLabel1.Location = new System.Drawing.Point(4, 15);
            this.autoLabel1.Name = "autoLabel1";
            this.autoLabel1.Size = new System.Drawing.Size(27, 13);
            this.autoLabel1.TabIndex = 9;
            this.autoLabel1.Text = "Title";
            // 
            // txtNodeAttrImpactValue
            // 
            this.txtNodeAttrImpactValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNodeAttrImpactValue.Location = new System.Drawing.Point(217, 32);
            this.txtNodeAttrImpactValue.Name = "txtNodeAttrImpactValue";
            this.txtNodeAttrImpactValue.Size = new System.Drawing.Size(58, 23);
            this.txtNodeAttrImpactValue.TabIndex = 12;
            this.txtNodeAttrImpactValue.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // NodeAttributeModal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 189);
            this.CloseButtonVisible = false;
            this.ControlBox = false;
            this.Controls.Add(this.txtNodeAttrImpactValue);
            this.Controls.Add(this.txtNodeAttrImpactDesc);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnAssessmentSave);
            this.Controls.Add(this.txtNodeAttrImpact);
            this.Controls.Add(this.autoLabel3);
            this.Controls.Add(this.autoLabel2);
            this.Controls.Add(this.autoLabel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "NodeAttributeModal";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Style.MdiChild.IconHorizontalAlignment = System.Windows.Forms.HorizontalAlignment.Center;
            this.Style.MdiChild.IconVerticalAlignment = System.Windows.Forms.VisualStyles.VerticalAlignment.Center;
            this.Text = "?";
            ((System.ComponentModel.ISupportInitialize)(this.txtNodeAttrImpactDesc)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNodeAttrImpact)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNodeAttrImpactValue)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Syncfusion.Windows.Forms.Tools.TextBoxExt txtNodeAttrImpactDesc;
        private Syncfusion.WinForms.Controls.SfButton btnCancel;
        private Syncfusion.WinForms.Controls.SfButton btnAssessmentSave;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel3;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel2;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel1;
        private Syncfusion.Windows.Forms.Tools.TextBoxExt txtNodeAttrImpact;
        private System.Windows.Forms.NumericUpDown txtNodeAttrImpactValue;
    }
}