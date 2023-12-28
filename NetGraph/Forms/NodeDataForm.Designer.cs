using Syncfusion.WinForms.DataGrid.Events;

namespace CyConex.Forms
{
    partial class NodeDataForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NodeDataForm));
            this.panelRiskGrid = new System.Windows.Forms.Panel();
            this.gridRiskData = new Syncfusion.WinForms.DataGrid.SfDataGrid();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel46 = new System.Windows.Forms.Panel();
            this.cmbNodesStatic = new Syncfusion.WinForms.ListView.SfComboBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnDetail = new Syncfusion.WinForms.Controls.SfButton();
            this.sfButton1 = new Syncfusion.WinForms.Controls.SfButton();
            this.btnFind = new Syncfusion.WinForms.Controls.SfButton();
            this.label1Static = new System.Windows.Forms.Label();
            this.label46 = new System.Windows.Forms.Label();
            this.sfButton2 = new Syncfusion.WinForms.Controls.SfButton();
            this.sfButton3 = new Syncfusion.WinForms.Controls.SfButton();
            this.panelRiskGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridRiskData)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel46.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbNodesStatic)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelRiskGrid
            // 
            this.panelRiskGrid.Controls.Add(this.gridRiskData);
            this.panelRiskGrid.Controls.Add(this.panel1);
            this.panelRiskGrid.Controls.Add(this.label46);
            this.panelRiskGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelRiskGrid.Location = new System.Drawing.Point(2, 2);
            this.panelRiskGrid.Name = "panelRiskGrid";
            this.panelRiskGrid.Size = new System.Drawing.Size(799, 573);
            this.panelRiskGrid.TabIndex = 3;
            // 
            // gridRiskData
            // 
            this.gridRiskData.AccessibleName = "Table";
            this.gridRiskData.AllowEditing = false;
            this.gridRiskData.AllowGrouping = false;
            this.gridRiskData.AllowResizingColumns = true;
            this.gridRiskData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridRiskData.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.gridRiskData.HeaderRowHeight = 28;
            this.gridRiskData.Location = new System.Drawing.Point(0, 67);
            this.gridRiskData.Name = "gridRiskData";
            this.gridRiskData.NavigationMode = Syncfusion.WinForms.DataGrid.Enums.NavigationMode.Row;
            this.gridRiskData.PasteOption = Syncfusion.WinForms.DataGrid.Enums.PasteOptions.None;
            this.gridRiskData.RowHeight = 20;
            this.gridRiskData.Size = new System.Drawing.Size(799, 506);
            this.gridRiskData.Style.Border3DStyle = System.Windows.Forms.Border3DStyle.Flat;
            this.gridRiskData.Style.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.gridRiskData.Style.HeaderStyle.BackColor = System.Drawing.Color.LightGray;
            this.gridRiskData.TabIndex = 0;
            this.gridRiskData.Text = "sfDataGrid1";
            this.gridRiskData.AutoGeneratingColumn += new Syncfusion.WinForms.DataGrid.Events.AutoGeneratingColumnEventHandler(this.dataGridRiskData_AutoGenerationColumn);
            this.gridRiskData.QueryCellStyle += new Syncfusion.WinForms.DataGrid.Events.QueryCellStyleEventHandler(this.gridRiskData_QueryCellStyle);
            this.gridRiskData.SelectionChanged += new Syncfusion.WinForms.DataGrid.Events.SelectionChangedEventHandler(this.dataGridRiskData_SelectionChanged);
            this.gridRiskData.DetailsViewExpanding += new Syncfusion.WinForms.DataGrid.Events.DetailsViewExpandingEventHandler(this.dataGridRiskData_DetailsViewExpanding);
            this.gridRiskData.DetailsViewLoading += new Syncfusion.WinForms.DataGrid.Events.DetailsViewLoadingAndUnloadingEventHandler(this.dataGridRiskData_DetailsViewLoading);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel46);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.label1Static);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 36);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(799, 31);
            this.panel1.TabIndex = 0;
            // 
            // panel46
            // 
            this.panel46.Controls.Add(this.cmbNodesStatic);
            this.panel46.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel46.Location = new System.Drawing.Point(44, 0);
            this.panel46.Name = "panel46";
            this.panel46.Size = new System.Drawing.Size(341, 24);
            this.panel46.TabIndex = 32;
            // 
            // cmbNodesStatic
            // 
            this.cmbNodesStatic.AllowSelectAll = true;
            this.cmbNodesStatic.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cmbNodesStatic.ComboBoxMode = Syncfusion.WinForms.ListView.Enums.ComboBoxMode.MultiSelection;
            this.cmbNodesStatic.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbNodesStatic.DropDownPosition = Syncfusion.WinForms.Core.Enums.PopupRelativeAlignment.Center;
            this.cmbNodesStatic.Location = new System.Drawing.Point(0, 0);
            this.cmbNodesStatic.MaxDropDownItems = 9;
            this.cmbNodesStatic.Name = "cmbNodesStatic";
            this.cmbNodesStatic.Size = new System.Drawing.Size(341, 24);
            this.cmbNodesStatic.Style.DropDownStyle.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.cmbNodesStatic.Style.TokenStyle.CloseButtonBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.cmbNodesStatic.TabIndex = 30;
            this.cmbNodesStatic.TabStop = false;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.sfButton2);
            this.panel2.Controls.Add(this.sfButton3);
            this.panel2.Controls.Add(this.btnDetail);
            this.panel2.Controls.Add(this.sfButton1);
            this.panel2.Controls.Add(this.btnFind);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel2.Location = new System.Drawing.Point(385, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(414, 31);
            this.panel2.TabIndex = 31;
            // 
            // btnDetail
            // 
            this.btnDetail.AccessibleName = "Button";
            this.btnDetail.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDetail.Enabled = false;
            this.btnDetail.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.btnDetail.Location = new System.Drawing.Point(336, 3);
            this.btnDetail.Name = "btnDetail";
            this.btnDetail.Size = new System.Drawing.Size(75, 25);
            this.btnDetail.Style.Image = ((System.Drawing.Image)(resources.GetObject("resource.Image2")));
            this.btnDetail.TabIndex = 1;
            this.btnDetail.Text = "Detail";
            this.btnDetail.Click += new System.EventHandler(this.btnDetail_Click);
            // 
            // sfButton1
            // 
            this.sfButton1.AccessibleName = "Button";
            this.sfButton1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.sfButton1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.sfButton1.Location = new System.Drawing.Point(174, 3);
            this.sfButton1.Name = "sfButton1";
            this.sfButton1.Size = new System.Drawing.Size(75, 25);
            this.sfButton1.Style.ForeColor = System.Drawing.Color.Black;
            this.sfButton1.Style.Image = ((System.Drawing.Image)(resources.GetObject("resource.Image3")));
            this.sfButton1.TabIndex = 2;
            this.sfButton1.Text = "Refresh";
            this.sfButton1.Click += new System.EventHandler(this.sfButton1_Click);
            // 
            // btnFind
            // 
            this.btnFind.AccessibleName = "Button";
            this.btnFind.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFind.Enabled = false;
            this.btnFind.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.btnFind.Location = new System.Drawing.Point(255, 3);
            this.btnFind.Name = "btnFind";
            this.btnFind.Size = new System.Drawing.Size(75, 25);
            this.btnFind.Style.Image = ((System.Drawing.Image)(resources.GetObject("resource.Image4")));
            this.btnFind.TabIndex = 1;
            this.btnFind.Text = "Find";
            this.btnFind.Click += new System.EventHandler(this.btnFind_Click);
            // 
            // label1Static
            // 
            this.label1Static.AutoEllipsis = true;
            this.label1Static.Dock = System.Windows.Forms.DockStyle.Left;
            this.label1Static.Location = new System.Drawing.Point(0, 0);
            this.label1Static.Name = "label1Static";
            this.label1Static.Size = new System.Drawing.Size(44, 31);
            this.label1Static.TabIndex = 29;
            this.label1Static.Text = "Nodes:";
            this.label1Static.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label46
            // 
            this.label46.AutoEllipsis = true;
            this.label46.BackColor = System.Drawing.Color.WhiteSmoke;
            this.label46.Dock = System.Windows.Forms.DockStyle.Top;
            this.label46.Font = new System.Drawing.Font("Segoe UI Semilight", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label46.Location = new System.Drawing.Point(0, 0);
            this.label46.Name = "label46";
            this.label46.Size = new System.Drawing.Size(799, 36);
            this.label46.TabIndex = 30;
            this.label46.Text = "Graph Elements";
            // 
            // sfButton2
            // 
            this.sfButton2.AccessibleName = "Button";
            this.sfButton2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.sfButton2.Enabled = false;
            this.sfButton2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.sfButton2.Location = new System.Drawing.Point(4, 3);
            this.sfButton2.Name = "sfButton2";
            this.sfButton2.Size = new System.Drawing.Size(82, 25);
            this.sfButton2.Style.ForeColor = System.Drawing.Color.Black;
            this.sfButton2.Style.Image = ((System.Drawing.Image)(resources.GetObject("resource.Image")));
            this.sfButton2.TabIndex = 4;
            this.sfButton2.Text = "Edge Out";
            // 
            // sfButton3
            // 
            this.sfButton3.AccessibleName = "Button";
            this.sfButton3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.sfButton3.Enabled = false;
            this.sfButton3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.sfButton3.Location = new System.Drawing.Point(92, 3);
            this.sfButton3.Name = "sfButton3";
            this.sfButton3.Size = new System.Drawing.Size(75, 25);
            this.sfButton3.Style.Image = ((System.Drawing.Image)(resources.GetObject("resource.Image1")));
            this.sfButton3.TabIndex = 3;
            this.sfButton3.Text = "Edge In";
            // 
            // NodeDataForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(803, 577);
            this.Controls.Add(this.panelRiskGrid);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "NodeDataForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Style.MdiChild.IconHorizontalAlignment = System.Windows.Forms.HorizontalAlignment.Center;
            this.Style.MdiChild.IconVerticalAlignment = System.Windows.Forms.VisualStyles.VerticalAlignment.Center;
            this.Text = "RiskDataForm";
            this.panelRiskGrid.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridRiskData)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel46.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cmbNodesStatic)).EndInit();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel1;
        private Syncfusion.WinForms.Controls.SfButton btnFind;
        private Syncfusion.WinForms.Controls.SfButton sfButton1;
        private Syncfusion.WinForms.Controls.SfButton btnDetail;
        private Syncfusion.WinForms.DataGrid.SfDataGrid gridRiskData;
        private Syncfusion.WinForms.ListView.SfComboBox cmbNodesStatic;
        private System.Windows.Forms.Label label1Static;
        private System.Windows.Forms.Panel panel2;
        public System.Windows.Forms.Panel panelRiskGrid;
        public System.Windows.Forms.Panel panel46;
        public System.Windows.Forms.Label label46;
        private Syncfusion.WinForms.Controls.SfButton sfButton2;
        private Syncfusion.WinForms.Controls.SfButton sfButton3;
    }
}