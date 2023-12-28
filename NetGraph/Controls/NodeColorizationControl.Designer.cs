namespace CyConex.Controls
{
	partial class NodeColorizationControl
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

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.Color_panel = new System.Windows.Forms.Panel();
			this.MinValue_numericUpDownExt = new Syncfusion.Windows.Forms.Tools.NumericUpDownExt();
			this.MaxValue_numericUpDownExt = new Syncfusion.Windows.Forms.Tools.NumericUpDownExt();
			this.Delete_noFocusableButton = new CyConex.NoFocusableButton();
			((System.ComponentModel.ISupportInitialize)(this.MinValue_numericUpDownExt)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.MaxValue_numericUpDownExt)).BeginInit();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(3, 3);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(30, 13);
			this.label1.TabIndex = 1;
			this.label1.Text = "From";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(112, 3);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(16, 13);
			this.label2.TabIndex = 3;
			this.label2.Text = "to";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(208, 3);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(30, 13);
			this.label3.TabIndex = 4;
			this.label3.Text = "color";
			// 
			// Color_panel
			// 
			this.Color_panel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.Color_panel.BackColor = System.Drawing.Color.DarkGray;
			this.Color_panel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.Color_panel.Location = new System.Drawing.Point(247, 0);
			this.Color_panel.Name = "Color_panel";
			this.Color_panel.Size = new System.Drawing.Size(43, 20);
			this.Color_panel.TabIndex = 5;
			this.Color_panel.Click += new System.EventHandler(this.Color_panel_Click);
			// 
			// MinValue_numericUpDownExt
			// 
			this.MinValue_numericUpDownExt.BeforeTouchSize = new System.Drawing.Size(68, 20);
			this.MinValue_numericUpDownExt.Border3DStyle = System.Windows.Forms.Border3DStyle.Flat;
			this.MinValue_numericUpDownExt.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(209)))), ((int)(((byte)(211)))), ((int)(((byte)(212)))));
			this.MinValue_numericUpDownExt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.MinValue_numericUpDownExt.DecimalPlaces = 2;
			this.MinValue_numericUpDownExt.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
			this.MinValue_numericUpDownExt.Location = new System.Drawing.Point(37, 0);
			this.MinValue_numericUpDownExt.Maximum = new decimal(new int[] {
            999999999,
            0,
            0,
            0});
			this.MinValue_numericUpDownExt.Name = "MinValue_numericUpDownExt";
			this.MinValue_numericUpDownExt.Size = new System.Drawing.Size(68, 20);
			this.MinValue_numericUpDownExt.TabIndex = 6;
			this.MinValue_numericUpDownExt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.MinValue_numericUpDownExt.ThemeName = "Metro";
			this.MinValue_numericUpDownExt.VisualStyle = Syncfusion.Windows.Forms.VisualStyle.Metro;
			// 
			// MaxValue_numericUpDownExt
			// 
			this.MaxValue_numericUpDownExt.BeforeTouchSize = new System.Drawing.Size(68, 20);
			this.MaxValue_numericUpDownExt.Border3DStyle = System.Windows.Forms.Border3DStyle.Flat;
			this.MaxValue_numericUpDownExt.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(209)))), ((int)(((byte)(211)))), ((int)(((byte)(212)))));
			this.MaxValue_numericUpDownExt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.MaxValue_numericUpDownExt.DecimalPlaces = 2;
			this.MaxValue_numericUpDownExt.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
			this.MaxValue_numericUpDownExt.Location = new System.Drawing.Point(134, 0);
			this.MaxValue_numericUpDownExt.Maximum = new decimal(new int[] {
            999999999,
            0,
            0,
            0});
			this.MaxValue_numericUpDownExt.Name = "MaxValue_numericUpDownExt";
			this.MaxValue_numericUpDownExt.Size = new System.Drawing.Size(68, 20);
			this.MaxValue_numericUpDownExt.TabIndex = 7;
			this.MaxValue_numericUpDownExt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.MaxValue_numericUpDownExt.ThemeName = "Metro";
			this.MaxValue_numericUpDownExt.VisualStyle = Syncfusion.Windows.Forms.VisualStyle.Metro;
			// 
			// Delete_noFocusableButton
			// 
			this.Delete_noFocusableButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.Delete_noFocusableButton.FlatAppearance.BorderSize = 0;
			this.Delete_noFocusableButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.Delete_noFocusableButton.Location = new System.Drawing.Point(294, 0);
			this.Delete_noFocusableButton.Name = "Delete_noFocusableButton";
			this.Delete_noFocusableButton.Size = new System.Drawing.Size(20, 20);
			this.Delete_noFocusableButton.TabIndex = 8;
			this.Delete_noFocusableButton.UseVisualStyleBackColor = true;
			this.Delete_noFocusableButton.Click += new System.EventHandler(this.Delete_noFocusableButton_Click);
			// 
			// NodeColorizationControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.Delete_noFocusableButton);
			this.Controls.Add(this.MaxValue_numericUpDownExt);
			this.Controls.Add(this.MinValue_numericUpDownExt);
			this.Controls.Add(this.Color_panel);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Name = "NodeColorizationControl";
			this.Size = new System.Drawing.Size(320, 24);
			((System.ComponentModel.ISupportInitialize)(this.MinValue_numericUpDownExt)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.MaxValue_numericUpDownExt)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Panel Color_panel;
		private Syncfusion.Windows.Forms.Tools.NumericUpDownExt MinValue_numericUpDownExt;
		private Syncfusion.Windows.Forms.Tools.NumericUpDownExt MaxValue_numericUpDownExt;
		private NoFocusableButton Delete_noFocusableButton;
	}
}
