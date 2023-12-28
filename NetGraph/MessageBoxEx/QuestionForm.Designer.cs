using System.Windows.Forms;

namespace CyConex
{
	partial class QuestionForm
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
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(QuestionForm));
			this.Main_flowLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
			this.Cancel_button = new System.Windows.Forms.Button();
			this.No_button = new System.Windows.Forms.Button();
			this.Yes_button = new System.Windows.Forms.Button();
			this.Question_radLabel = new System.Windows.Forms.Label();
			this.Dialog_pictureBox = new System.Windows.Forms.PictureBox();
			this.DontShowAgain_radCheckBox = new System.Windows.Forms.CheckBox();
			this.toolTip = new System.Windows.Forms.ToolTip(this.components);
			this.panel1 = new System.Windows.Forms.Panel();
			this.panel2 = new System.Windows.Forms.Panel();
			this.panel3 = new System.Windows.Forms.Panel();
			this.Main_flowLayoutPanel.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.Dialog_pictureBox)).BeginInit();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// Main_flowLayoutPanel
			// 
			this.Main_flowLayoutPanel.BackColor = System.Drawing.SystemColors.Window;
			this.Main_flowLayoutPanel.Controls.Add(this.Cancel_button);
			this.Main_flowLayoutPanel.Controls.Add(this.No_button);
			this.Main_flowLayoutPanel.Controls.Add(this.Yes_button);
			this.Main_flowLayoutPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.Main_flowLayoutPanel.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
			this.Main_flowLayoutPanel.Location = new System.Drawing.Point(0, 186);
			this.Main_flowLayoutPanel.Name = "Main_flowLayoutPanel";
			this.Main_flowLayoutPanel.Padding = new System.Windows.Forms.Padding(0, 6, 7, 0);
			this.Main_flowLayoutPanel.Size = new System.Drawing.Size(471, 43);
			this.Main_flowLayoutPanel.TabIndex = 2;
			// 
			// Cancel_button
			// 
			this.Cancel_button.BackColor = System.Drawing.Color.White;
			this.Cancel_button.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.Cancel_button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.Cancel_button.Location = new System.Drawing.Point(381, 9);
			this.Cancel_button.Name = "Cancel_button";
			this.Cancel_button.Size = new System.Drawing.Size(80, 25);
			this.Cancel_button.TabIndex = 2;
			this.Cancel_button.Text = "Cancel";
			this.Cancel_button.UseVisualStyleBackColor = false;
			// 
			// No_button
			// 
			this.No_button.BackColor = System.Drawing.Color.White;
			this.No_button.DialogResult = System.Windows.Forms.DialogResult.No;
			this.No_button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.No_button.Location = new System.Drawing.Point(295, 9);
			this.No_button.Name = "No_button";
			this.No_button.Size = new System.Drawing.Size(80, 25);
			this.No_button.TabIndex = 1;
			this.No_button.Text = "No";
			this.No_button.UseVisualStyleBackColor = false;
			// 
			// Yes_button
			// 
			this.Yes_button.BackColor = System.Drawing.Color.White;
			this.Yes_button.DialogResult = System.Windows.Forms.DialogResult.Yes;
			this.Yes_button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.Yes_button.Location = new System.Drawing.Point(209, 9);
			this.Yes_button.Name = "Yes_button";
			this.Yes_button.Size = new System.Drawing.Size(80, 25);
			this.Yes_button.TabIndex = 0;
			this.Yes_button.Text = "Yes";
			this.Yes_button.UseVisualStyleBackColor = false;
			// 
			// Question_radLabel
			// 
			this.Question_radLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.Question_radLabel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.Question_radLabel.Location = new System.Drawing.Point(66, 12);
			this.Question_radLabel.Name = "Question_radLabel";
			this.Question_radLabel.Size = new System.Drawing.Size(393, 134);
			this.Question_radLabel.TabIndex = 0;
			this.Question_radLabel.Text = "This is example text";
			// 
			// Dialog_pictureBox
			// 
			this.Dialog_pictureBox.Location = new System.Drawing.Point(12, 12);
			this.Dialog_pictureBox.Name = "Dialog_pictureBox";
			this.Dialog_pictureBox.Size = new System.Drawing.Size(48, 48);
			this.Dialog_pictureBox.TabIndex = 2;
			this.Dialog_pictureBox.TabStop = false;
			// 
			// DontShowAgain_radCheckBox
			// 
			this.DontShowAgain_radCheckBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.DontShowAgain_radCheckBox.AutoSize = true;
			this.DontShowAgain_radCheckBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.DontShowAgain_radCheckBox.Location = new System.Drawing.Point(69, 160);
			this.DontShowAgain_radCheckBox.Name = "DontShowAgain_radCheckBox";
			this.DontShowAgain_radCheckBox.Size = new System.Drawing.Size(144, 17);
			this.DontShowAgain_radCheckBox.TabIndex = 1;
			this.DontShowAgain_radCheckBox.Text = "Do not show this again";
			// 
			// panelValues
			// 
			this.panel1.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
			this.panel1.Controls.Add(this.panel3);
			this.panel1.Controls.Add(this.panel2);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panel1.Location = new System.Drawing.Point(0, 184);
			this.panel1.Name = "panelValues";
			this.panel1.Size = new System.Drawing.Size(471, 2);
			this.panel1.TabIndex = 3;
			// 
			// panel2
			// 
			this.panel2.BackColor = System.Drawing.SystemColors.Window;
			this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
			this.panel2.Location = new System.Drawing.Point(0, 0);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(10, 2);
			this.panel2.TabIndex = 0;
			// 
			// panel3
			// 
			this.panel3.BackColor = System.Drawing.SystemColors.Window;
			this.panel3.Dock = System.Windows.Forms.DockStyle.Right;
			this.panel3.Location = new System.Drawing.Point(461, 0);
			this.panel3.Name = "panel3";
			this.panel3.Size = new System.Drawing.Size(10, 2);
			this.panel3.TabIndex = 1;
			// 
			// QuestionForm
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.BackColor = System.Drawing.SystemColors.Window;
			this.ClientSize = new System.Drawing.Size(471, 229);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.Question_radLabel);
			this.Controls.Add(this.Main_flowLayoutPanel);
			this.Controls.Add(this.DontShowAgain_radCheckBox);
			this.Controls.Add(this.Dialog_pictureBox);
			this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "QuestionForm";
			this.ShowInTaskbar = false;
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Load += new System.EventHandler(this.QuestionForm_Load);
			this.Shown += new System.EventHandler(this.QuestionForm_Shown);
			this.Main_flowLayoutPanel.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.Dialog_pictureBox)).EndInit();
			this.panel1.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.FlowLayoutPanel Main_flowLayoutPanel;
		private Button Cancel_button;
		private Button No_button;
		private Button Yes_button;
		private System.Windows.Forms.PictureBox Dialog_pictureBox;
		private Label Question_radLabel;
		private System.Windows.Forms.ToolTip toolTip;
		internal CheckBox DontShowAgain_radCheckBox;
		private Panel panel1;
		private Panel panel3;
		private Panel panel2;
	}
}