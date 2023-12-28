namespace CyConex.Helpers
{
    partial class CalcLogForm
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
            this.CalculationLog_listView = new System.Windows.Forms.ListView();
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // CalculationLog_listView
            // 
            this.CalculationLog_listView.BackColor = System.Drawing.Color.White;
            this.CalculationLog_listView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.CalculationLog_listView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader3,
            this.columnHeader4});
            this.CalculationLog_listView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CalculationLog_listView.ForeColor = System.Drawing.Color.Black;
            this.CalculationLog_listView.FullRowSelect = true;
            this.CalculationLog_listView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.CalculationLog_listView.HideSelection = false;
            this.CalculationLog_listView.LabelWrap = false;
            this.CalculationLog_listView.Location = new System.Drawing.Point(0, 0);
            this.CalculationLog_listView.MultiSelect = false;
            this.CalculationLog_listView.Name = "CalculationLog_listView";
            this.CalculationLog_listView.Size = new System.Drawing.Size(800, 450);
            this.CalculationLog_listView.TabIndex = 2;
            this.CalculationLog_listView.UseCompatibleStateImageBehavior = false;
            this.CalculationLog_listView.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Time";
            this.columnHeader3.Width = 100;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Description";
            this.columnHeader4.Width = 1024;
            // 
            // CalcLogForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.CalculationLog_listView);
            this.Name = "CalcLogForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Graph Calculation Log";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.CalcLogForm_FormClosed);
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.ListView CalculationLog_listView;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
    }
}