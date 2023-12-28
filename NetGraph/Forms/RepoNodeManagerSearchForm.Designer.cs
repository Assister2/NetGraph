namespace CyConex
{
    partial class RepoNodeManagerSearchForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RepoNodeManagerSearchForm));
            this.btnDelete = new Syncfusion.WinForms.Controls.SfButton();
            this.btnEdit = new Syncfusion.WinForms.Controls.SfButton();
            this.btnAdd = new Syncfusion.WinForms.Controls.SfButton();
            this.btnCopy = new Syncfusion.WinForms.Controls.SfButton();
            this.gridSearchResult = new System.Windows.Forms.DataGridView();
            this.nodeTitle = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nodeType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nodeDescription = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nodeGUID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sfBtnFind = new Syncfusion.WinForms.Controls.SfButton();
            this.txtFindWhat = new Syncfusion.Windows.Forms.Tools.TextBoxExt();
            this.autoLabel6 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.autoLabel5 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.autoLabel4 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel6 = new System.Windows.Forms.Panel();
            this.panel8 = new System.Windows.Forms.Panel();
            this.panel12 = new System.Windows.Forms.Panel();
            this.cmbSearchIn = new CyConex.CheckedComboBox();
            this.panel11 = new System.Windows.Forms.Panel();
            this.panel7 = new System.Windows.Forms.Panel();
            this.panel10 = new System.Windows.Forms.Panel();
            this.cmbFilterBy = new CyConex.CheckedComboBox();
            this.panel9 = new System.Windows.Forms.Panel();
            this.panel13 = new System.Windows.Forms.Panel();
            this.panel14 = new System.Windows.Forms.Panel();
            this.btnTest = new Syncfusion.WinForms.Controls.SfButton();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nodeSearchBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.nodeShapeBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.nodeAssetBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.gridSearchResult)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFindWhat)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel6.SuspendLayout();
            this.panel8.SuspendLayout();
            this.panel12.SuspendLayout();
            this.panel11.SuspendLayout();
            this.panel7.SuspendLayout();
            this.panel10.SuspendLayout();
            this.panel9.SuspendLayout();
            this.panel13.SuspendLayout();
            this.panel14.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nodeSearchBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nodeShapeBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nodeAssetBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // btnDelete
            // 
            this.btnDelete.AccessibleName = "Button";
            this.btnDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDelete.Font = new System.Drawing.Font("Segoe UI Semibold", 9F);
            this.btnDelete.Location = new System.Drawing.Point(486, 5);
            this.btnDelete.Margin = new System.Windows.Forms.Padding(5);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Padding = new System.Windows.Forms.Padding(5);
            this.btnDelete.Size = new System.Drawing.Size(71, 29);
            this.btnDelete.TabIndex = 11;
            this.btnDelete.Text = "Delete";
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.AccessibleName = "Button";
            this.btnEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEdit.Font = new System.Drawing.Font("Segoe UI Semibold", 9F);
            this.btnEdit.Location = new System.Drawing.Point(736, 5);
            this.btnEdit.Margin = new System.Windows.Forms.Padding(5);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Padding = new System.Windows.Forms.Padding(5);
            this.btnEdit.Size = new System.Drawing.Size(74, 29);
            this.btnEdit.TabIndex = 12;
            this.btnEdit.Text = "Edit";
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.AccessibleName = "Button";
            this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAdd.Font = new System.Drawing.Font("Segoe UI Semibold", 9F);
            this.btnAdd.Location = new System.Drawing.Point(652, 5);
            this.btnAdd.Margin = new System.Windows.Forms.Padding(5);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Padding = new System.Windows.Forms.Padding(5);
            this.btnAdd.Size = new System.Drawing.Size(74, 29);
            this.btnAdd.TabIndex = 13;
            this.btnAdd.Text = "Add";
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnCopy
            // 
            this.btnCopy.AccessibleName = "Button";
            this.btnCopy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCopy.Font = new System.Drawing.Font("Segoe UI Semibold", 9F);
            this.btnCopy.Location = new System.Drawing.Point(568, 5);
            this.btnCopy.Margin = new System.Windows.Forms.Padding(5);
            this.btnCopy.Name = "btnCopy";
            this.btnCopy.Padding = new System.Windows.Forms.Padding(5);
            this.btnCopy.Size = new System.Drawing.Size(74, 29);
            this.btnCopy.TabIndex = 20;
            this.btnCopy.Text = "Copy";
            this.btnCopy.Click += new System.EventHandler(this.btnCopy_Click);
            // 
            // gridSearchResult
            // 
            this.gridSearchResult.AllowUserToAddRows = false;
            this.gridSearchResult.AllowUserToDeleteRows = false;
            this.gridSearchResult.AllowUserToOrderColumns = true;
            this.gridSearchResult.BackgroundColor = System.Drawing.SystemColors.Window;
            this.gridSearchResult.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridSearchResult.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.nodeTitle,
            this.nodeType,
            this.nodeDescription,
            this.nodeGUID});
            this.gridSearchResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridSearchResult.Location = new System.Drawing.Point(0, 0);
            this.gridSearchResult.Name = "gridSearchResult";
            this.gridSearchResult.ReadOnly = true;
            this.gridSearchResult.RowHeadersVisible = false;
            this.gridSearchResult.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridSearchResult.Size = new System.Drawing.Size(815, 349);
            this.gridSearchResult.TabIndex = 22;
            this.gridSearchResult.DoubleClick += new System.EventHandler(this.gridSearchResult_DoubleClick);
            // 
            // nodeTitle
            // 
            this.nodeTitle.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.nodeTitle.DataPropertyName = "nodeTitle";
            this.nodeTitle.HeaderText = "Node Title";
            this.nodeTitle.Name = "nodeTitle";
            this.nodeTitle.ReadOnly = true;
            // 
            // nodeType
            // 
            this.nodeType.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.nodeType.DataPropertyName = "nodeType";
            this.nodeType.HeaderText = "Node Type";
            this.nodeType.Name = "nodeType";
            this.nodeType.ReadOnly = true;
            // 
            // nodeDescription
            // 
            this.nodeDescription.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.nodeDescription.DataPropertyName = "nodeDescription";
            this.nodeDescription.HeaderText = "Node Description";
            this.nodeDescription.Name = "nodeDescription";
            this.nodeDescription.ReadOnly = true;
            // 
            // nodeGUID
            // 
            this.nodeGUID.HeaderText = "Node GUID";
            this.nodeGUID.Name = "nodeGUID";
            this.nodeGUID.ReadOnly = true;
            this.nodeGUID.Visible = false;
            // 
            // sfBtnFind
            // 
            this.sfBtnFind.AccessibleName = "Button";
            this.sfBtnFind.Font = new System.Drawing.Font("Segoe UI Semibold", 10F);
            this.sfBtnFind.Location = new System.Drawing.Point(5, 11);
            this.sfBtnFind.Name = "sfBtnFind";
            this.sfBtnFind.Size = new System.Drawing.Size(74, 23);
            this.sfBtnFind.TabIndex = 27;
            this.sfBtnFind.Text = "Find";
            this.sfBtnFind.Click += new System.EventHandler(this.sfBtnFind_Click);
            // 
            // txtFindWhat
            // 
            this.txtFindWhat.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
            this.txtFindWhat.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.HistoryList;
            this.txtFindWhat.BeforeTouchSize = new System.Drawing.Size(664, 23);
            this.txtFindWhat.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtFindWhat.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFindWhat.Location = new System.Drawing.Point(3, 10);
            this.txtFindWhat.Name = "txtFindWhat";
            this.txtFindWhat.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtFindWhat.Size = new System.Drawing.Size(664, 23);
            this.txtFindWhat.TabIndex = 26;
            // 
            // autoLabel6
            // 
            this.autoLabel6.Location = new System.Drawing.Point(6, 9);
            this.autoLabel6.Name = "autoLabel6";
            this.autoLabel6.Size = new System.Drawing.Size(44, 13);
            this.autoLabel6.TabIndex = 23;
            this.autoLabel6.Text = "Filter By";
            // 
            // autoLabel5
            // 
            this.autoLabel5.Location = new System.Drawing.Point(2, 8);
            this.autoLabel5.Name = "autoLabel5";
            this.autoLabel5.Size = new System.Drawing.Size(53, 13);
            this.autoLabel5.TabIndex = 24;
            this.autoLabel5.Text = "Search In";
            // 
            // autoLabel4
            // 
            this.autoLabel4.Location = new System.Drawing.Point(2, 14);
            this.autoLabel4.Name = "autoLabel4";
            this.autoLabel4.Size = new System.Drawing.Size(56, 13);
            this.autoLabel4.TabIndex = 25;
            this.autoLabel4.Text = "Find What";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(10, 10);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(815, 42);
            this.panel1.TabIndex = 30;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.panel5);
            this.panel3.Controls.Add(this.panel4);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(61, 0);
            this.panel3.Name = "panel3";
            this.panel3.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.panel3.Size = new System.Drawing.Size(754, 42);
            this.panel3.TabIndex = 1;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.txtFindWhat);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel5.Location = new System.Drawing.Point(0, 0);
            this.panel5.Name = "panel5";
            this.panel5.Padding = new System.Windows.Forms.Padding(3, 10, 3, 0);
            this.panel5.Size = new System.Drawing.Size(670, 42);
            this.panel5.TabIndex = 1;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.sfBtnFind);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel4.Location = new System.Drawing.Point(670, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(84, 42);
            this.panel4.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.autoLabel4);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(61, 42);
            this.panel2.TabIndex = 0;
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.panel8);
            this.panel6.Controls.Add(this.panel7);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel6.Location = new System.Drawing.Point(10, 52);
            this.panel6.Name = "panel6";
            this.panel6.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.panel6.Size = new System.Drawing.Size(815, 35);
            this.panel6.TabIndex = 31;
            // 
            // panel8
            // 
            this.panel8.Controls.Add(this.panel12);
            this.panel8.Controls.Add(this.panel11);
            this.panel8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel8.Location = new System.Drawing.Point(0, 0);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(430, 35);
            this.panel8.TabIndex = 1;
            // 
            // panel12
            // 
            this.panel12.Controls.Add(this.cmbSearchIn);
            this.panel12.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel12.Location = new System.Drawing.Point(61, 0);
            this.panel12.Name = "panel12";
            this.panel12.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.panel12.Size = new System.Drawing.Size(369, 35);
            this.panel12.TabIndex = 1;
            // 
            // cmbSearchIn
            // 
            this.cmbSearchIn.CheckOnClick = true;
            this.cmbSearchIn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbSearchIn.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cmbSearchIn.DropDownHeight = 1;
            this.cmbSearchIn.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbSearchIn.FormattingEnabled = true;
            this.cmbSearchIn.IntegralHeight = false;
            this.cmbSearchIn.Location = new System.Drawing.Point(0, 5);
            this.cmbSearchIn.Name = "cmbSearchIn";
            this.cmbSearchIn.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cmbSearchIn.Size = new System.Drawing.Size(369, 24);
            this.cmbSearchIn.TabIndex = 29;
            this.cmbSearchIn.ValueSeparator = ", ";
            // 
            // panel11
            // 
            this.panel11.Controls.Add(this.autoLabel5);
            this.panel11.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel11.Location = new System.Drawing.Point(0, 0);
            this.panel11.Name = "panel11";
            this.panel11.Size = new System.Drawing.Size(61, 35);
            this.panel11.TabIndex = 0;
            // 
            // panel7
            // 
            this.panel7.Controls.Add(this.panel10);
            this.panel7.Controls.Add(this.panel9);
            this.panel7.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel7.Location = new System.Drawing.Point(430, 0);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(385, 35);
            this.panel7.TabIndex = 0;
            // 
            // panel10
            // 
            this.panel10.Controls.Add(this.cmbFilterBy);
            this.panel10.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel10.Location = new System.Drawing.Point(56, 0);
            this.panel10.Name = "panel10";
            this.panel10.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.panel10.Size = new System.Drawing.Size(329, 35);
            this.panel10.TabIndex = 1;
            // 
            // cmbFilterBy
            // 
            this.cmbFilterBy.CheckOnClick = true;
            this.cmbFilterBy.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbFilterBy.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cmbFilterBy.DropDownHeight = 1;
            this.cmbFilterBy.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbFilterBy.FormattingEnabled = true;
            this.cmbFilterBy.IntegralHeight = false;
            this.cmbFilterBy.Location = new System.Drawing.Point(0, 5);
            this.cmbFilterBy.Name = "cmbFilterBy";
            this.cmbFilterBy.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cmbFilterBy.Size = new System.Drawing.Size(329, 24);
            this.cmbFilterBy.TabIndex = 28;
            this.cmbFilterBy.ValueSeparator = ", ";
            // 
            // panel9
            // 
            this.panel9.Controls.Add(this.autoLabel6);
            this.panel9.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel9.Location = new System.Drawing.Point(0, 0);
            this.panel9.Name = "panel9";
            this.panel9.Size = new System.Drawing.Size(56, 35);
            this.panel9.TabIndex = 0;
            // 
            // panel13
            // 
            this.panel13.Controls.Add(this.gridSearchResult);
            this.panel13.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel13.Location = new System.Drawing.Point(10, 87);
            this.panel13.Name = "panel13";
            this.panel13.Padding = new System.Windows.Forms.Padding(0, 0, 0, 40);
            this.panel13.Size = new System.Drawing.Size(815, 389);
            this.panel13.TabIndex = 32;
            // 
            // panel14
            // 
            this.panel14.Controls.Add(this.btnTest);
            this.panel14.Controls.Add(this.btnDelete);
            this.panel14.Controls.Add(this.btnCopy);
            this.panel14.Controls.Add(this.btnEdit);
            this.panel14.Controls.Add(this.btnAdd);
            this.panel14.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel14.Location = new System.Drawing.Point(10, 437);
            this.panel14.Name = "panel14";
            this.panel14.Padding = new System.Windows.Forms.Padding(5);
            this.panel14.Size = new System.Drawing.Size(815, 39);
            this.panel14.TabIndex = 33;
            // 
            // btnTest
            // 
            this.btnTest.AccessibleName = "Button";
            this.btnTest.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnTest.Font = new System.Drawing.Font("Segoe UI Semibold", 9F);
            this.btnTest.Location = new System.Drawing.Point(0, 7);
            this.btnTest.Margin = new System.Windows.Forms.Padding(5);
            this.btnTest.Name = "btnTest";
            this.btnTest.Padding = new System.Windows.Forms.Padding(5);
            this.btnTest.Size = new System.Drawing.Size(71, 29);
            this.btnTest.TabIndex = 21;
            this.btnTest.Text = "Test";
            this.btnTest.Click += new System.EventHandler(this.btnTest_Click);
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn1.DataPropertyName = "nodeType";
            this.dataGridViewTextBoxColumn1.HeaderText = "Node Type";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn2.DataPropertyName = "nodeTitle";
            this.dataGridViewTextBoxColumn2.HeaderText = "Node Title";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn3.DataPropertyName = "nodeDescription";
            this.dataGridViewTextBoxColumn3.HeaderText = "Node Description";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.HeaderText = "Node GUID";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            this.dataGridViewTextBoxColumn4.Visible = false;
            // 
            // nodeSearchBindingSource
            // 
            this.nodeSearchBindingSource.DataSource = typeof(CyConex.Graph.NodeSearch);
            // 
            // nodeShapeBindingSource
            // 
            this.nodeShapeBindingSource.DataSource = typeof(CyConex.Graph.NodeShape);
            // 
            // nodeAssetBindingSource
            // 
            this.nodeAssetBindingSource.DataSource = typeof(CyConex.Graph.NodeAsset);
            // 
            // RepoNodeManagerSearchForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(835, 486);
            this.Controls.Add(this.panel14);
            this.Controls.Add(this.panel13);
            this.Controls.Add(this.panel6);
            this.Controls.Add(this.panel1);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "RepoNodeManagerSearchForm";
            this.Padding = new System.Windows.Forms.Padding(10);
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.ShowToolTip = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Style.MdiChild.IconHorizontalAlignment = System.Windows.Forms.HorizontalAlignment.Center;
            this.Style.MdiChild.IconVerticalAlignment = System.Windows.Forms.VisualStyles.VerticalAlignment.Center;
            this.Text = "Node Manage";
            this.TopMost = true;
            ((System.ComponentModel.ISupportInitialize)(this.gridSearchResult)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFindWhat)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel6.ResumeLayout(false);
            this.panel8.ResumeLayout(false);
            this.panel12.ResumeLayout(false);
            this.panel11.ResumeLayout(false);
            this.panel11.PerformLayout();
            this.panel7.ResumeLayout(false);
            this.panel10.ResumeLayout(false);
            this.panel9.ResumeLayout(false);
            this.panel9.PerformLayout();
            this.panel13.ResumeLayout(false);
            this.panel14.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nodeSearchBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nodeShapeBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nodeAssetBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.BindingSource nodeSearchBindingSource;
        private System.Windows.Forms.BindingSource nodeShapeBindingSource;
        private System.Windows.Forms.BindingSource nodeAssetBindingSource;
        private Syncfusion.WinForms.Controls.SfButton btnDelete;
        private Syncfusion.WinForms.Controls.SfButton btnEdit;
        private Syncfusion.WinForms.Controls.SfButton btnAdd;
        private Syncfusion.WinForms.Controls.SfButton btnCopy;
        private System.Windows.Forms.DataGridView gridSearchResult;
        private CheckedComboBox cmbFilterBy;
        private CheckedComboBox cmbSearchIn;
        private Syncfusion.WinForms.Controls.SfButton sfBtnFind;
        private Syncfusion.Windows.Forms.Tools.TextBoxExt txtFindWhat;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel6;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel5;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel4;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Panel panel10;
        private System.Windows.Forms.Panel panel9;
        private System.Windows.Forms.Panel panel11;
        private System.Windows.Forms.Panel panel12;
        private System.Windows.Forms.Panel panel13;
        private System.Windows.Forms.Panel panel14;
        private Syncfusion.WinForms.Controls.SfButton btnTest;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn nodeTitle;
        private System.Windows.Forms.DataGridViewTextBoxColumn nodeType;
        private System.Windows.Forms.DataGridViewTextBoxColumn nodeDescription;
        private System.Windows.Forms.DataGridViewTextBoxColumn nodeGUID;
    }
}