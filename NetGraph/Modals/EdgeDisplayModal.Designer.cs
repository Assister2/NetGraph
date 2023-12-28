namespace CyConex
{
    partial class EdgeDisplayModal
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EdgeDisplayModal));
            this.autoLabel2 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.autoLabel3 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.btnRStrengthSave = new Syncfusion.WinForms.Controls.SfButton();
            this.btnCancel = new Syncfusion.WinForms.Controls.SfButton();
            this.txtEdgeDisplayValueFrom = new System.Windows.Forms.NumericUpDown();
            this.autoLabel1 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.txtEdgeDisplayValueTo = new System.Windows.Forms.NumericUpDown();
            this.autoLabel4 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.txtEdgeDisplayWidth = new System.Windows.Forms.NumericUpDown();
            this.txtEdgeDisplayColor = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.txtEdgeDisplayValueFrom)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEdgeDisplayValueTo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEdgeDisplayWidth)).BeginInit();
            this.SuspendLayout();
            // 
            // autoLabel2
            // 
            this.autoLabel2.Location = new System.Drawing.Point(5, 9);
            this.autoLabel2.Name = "autoLabel2";
            this.autoLabel2.Size = new System.Drawing.Size(60, 13);
            this.autoLabel2.TabIndex = 1;
            this.autoLabel2.Text = "Value From";
            // 
            // autoLabel3
            // 
            this.autoLabel3.Location = new System.Drawing.Point(199, 9);
            this.autoLabel3.Name = "autoLabel3";
            this.autoLabel3.Size = new System.Drawing.Size(31, 13);
            this.autoLabel3.TabIndex = 2;
            this.autoLabel3.Text = "Color";
            // 
            // btnRStrengthSave
            // 
            this.btnRStrengthSave.AccessibleName = "Button";
            this.btnRStrengthSave.Font = new System.Drawing.Font("Segoe UI Semibold", 9F);
            this.btnRStrengthSave.Location = new System.Drawing.Point(90, 54);
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
            this.btnCancel.Location = new System.Drawing.Point(168, 54);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(72, 28);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // txtEdgeDisplayValueFrom
            // 
            this.txtEdgeDisplayValueFrom.DecimalPlaces = 2;
            this.txtEdgeDisplayValueFrom.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEdgeDisplayValueFrom.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.txtEdgeDisplayValueFrom.Location = new System.Drawing.Point(7, 25);
            this.txtEdgeDisplayValueFrom.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.txtEdgeDisplayValueFrom.Name = "txtEdgeDisplayValueFrom";
            this.txtEdgeDisplayValueFrom.Size = new System.Drawing.Size(58, 23);
            this.txtEdgeDisplayValueFrom.TabIndex = 11;
            this.txtEdgeDisplayValueFrom.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // autoLabel1
            // 
            this.autoLabel1.Location = new System.Drawing.Point(69, 9);
            this.autoLabel1.Name = "autoLabel1";
            this.autoLabel1.Size = new System.Drawing.Size(50, 13);
            this.autoLabel1.TabIndex = 1;
            this.autoLabel1.Text = "Value To";
            // 
            // txtEdgeDisplayValueTo
            // 
            this.txtEdgeDisplayValueTo.DecimalPlaces = 2;
            this.txtEdgeDisplayValueTo.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEdgeDisplayValueTo.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.txtEdgeDisplayValueTo.Location = new System.Drawing.Point(71, 25);
            this.txtEdgeDisplayValueTo.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.txtEdgeDisplayValueTo.Name = "txtEdgeDisplayValueTo";
            this.txtEdgeDisplayValueTo.Size = new System.Drawing.Size(58, 23);
            this.txtEdgeDisplayValueTo.TabIndex = 11;
            this.txtEdgeDisplayValueTo.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // autoLabel4
            // 
            this.autoLabel4.Location = new System.Drawing.Point(133, 9);
            this.autoLabel4.Name = "autoLabel4";
            this.autoLabel4.Size = new System.Drawing.Size(35, 13);
            this.autoLabel4.TabIndex = 1;
            this.autoLabel4.Text = "Width";
            // 
            // txtEdgeDisplayWidth
            // 
            this.txtEdgeDisplayWidth.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEdgeDisplayWidth.Location = new System.Drawing.Point(135, 25);
            this.txtEdgeDisplayWidth.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.txtEdgeDisplayWidth.Name = "txtEdgeDisplayWidth";
            this.txtEdgeDisplayWidth.Size = new System.Drawing.Size(58, 23);
            this.txtEdgeDisplayWidth.TabIndex = 11;
            this.txtEdgeDisplayWidth.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // txtEdgeDisplayColor
            // 
            this.txtEdgeDisplayColor.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEdgeDisplayColor.Location = new System.Drawing.Point(199, 25);
            this.txtEdgeDisplayColor.Name = "txtEdgeDisplayColor";
            this.txtEdgeDisplayColor.ReadOnly = true;
            this.txtEdgeDisplayColor.Size = new System.Drawing.Size(41, 23);
            this.txtEdgeDisplayColor.TabIndex = 12;
            this.txtEdgeDisplayColor.Click += new System.EventHandler(this.txtEdgeDisplayColor_Click);
            // 
            // EdgeDisplayModal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(244, 93);
            this.CloseButtonVisible = false;
            this.ControlBox = false;
            this.Controls.Add(this.txtEdgeDisplayColor);
            this.Controls.Add(this.txtEdgeDisplayWidth);
            this.Controls.Add(this.txtEdgeDisplayValueTo);
            this.Controls.Add(this.txtEdgeDisplayValueFrom);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnRStrengthSave);
            this.Controls.Add(this.autoLabel4);
            this.Controls.Add(this.autoLabel1);
            this.Controls.Add(this.autoLabel3);
            this.Controls.Add(this.autoLabel2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "EdgeDisplayModal";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Style.MdiChild.IconHorizontalAlignment = System.Windows.Forms.HorizontalAlignment.Center;
            this.Style.MdiChild.IconVerticalAlignment = System.Windows.Forms.VisualStyles.VerticalAlignment.Center;
            this.Text = "Edge Display";
            this.TopMost = true;
            ((System.ComponentModel.ISupportInitialize)(this.txtEdgeDisplayValueFrom)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEdgeDisplayValueTo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEdgeDisplayWidth)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel2;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel3;
        private Syncfusion.WinForms.Controls.SfButton btnRStrengthSave;
        private Syncfusion.WinForms.Controls.SfButton btnCancel;
        private System.Windows.Forms.NumericUpDown txtEdgeDisplayValueFrom;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel1;
        private System.Windows.Forms.NumericUpDown txtEdgeDisplayValueTo;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel4;
        private System.Windows.Forms.NumericUpDown txtEdgeDisplayWidth;
        private System.Windows.Forms.TextBox txtEdgeDisplayColor;
    }
}