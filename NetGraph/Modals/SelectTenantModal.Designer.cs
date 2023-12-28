namespace CyConex
{
    partial class SelectTenantModal
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SelectTenantModal));
            this.label1 = new System.Windows.Forms.Label();
            this.btnSelect = new Syncfusion.WinForms.Controls.SfButton();
            this.gridTenants = new System.Windows.Forms.DataGridView();
            this.TenantGUID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TenantName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TenantDescription = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnRemove = new Syncfusion.WinForms.Controls.SfButton();
            this.btnAdd = new Syncfusion.WinForms.Controls.SfButton();
            this.btnTenantPermissions = new Syncfusion.WinForms.Controls.SfButton();
            this.btnInviteTest = new Syncfusion.WinForms.Controls.SfButton();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tenantItemBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.gridTenants)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tenantItemBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(2, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(239, 17);
            this.label1.TabIndex = 90;
            this.label1.Text = "Which Tenant would you like to use :";
            // 
            // btnSelect
            // 
            this.btnSelect.AccessibleName = "Button";
            this.btnSelect.Enabled = false;
            this.btnSelect.Font = new System.Drawing.Font("Segoe UI Semibold", 9F);
            this.btnSelect.Location = new System.Drawing.Point(391, 226);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(81, 30);
            this.btnSelect.TabIndex = 89;
            this.btnSelect.Text = "Select";
            this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
            // 
            // gridTenants
            // 
            this.gridTenants.AllowUserToAddRows = false;
            this.gridTenants.AllowUserToDeleteRows = false;
            this.gridTenants.AllowUserToResizeRows = false;
            this.gridTenants.BackgroundColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridTenants.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.gridTenants.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridTenants.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.TenantGUID,
            this.TenantName,
            this.TenantDescription});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gridTenants.DefaultCellStyle = dataGridViewCellStyle2;
            this.gridTenants.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.gridTenants.Location = new System.Drawing.Point(5, 36);
            this.gridTenants.MultiSelect = false;
            this.gridTenants.Name = "gridTenants";
            this.gridTenants.ReadOnly = true;
            this.gridTenants.RowHeadersVisible = false;
            this.gridTenants.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridTenants.Size = new System.Drawing.Size(467, 183);
            this.gridTenants.TabIndex = 91;
            this.gridTenants.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridTenants_CellContentClick);
            this.gridTenants.Click += new System.EventHandler(this.gridTenants_Click);
            // 
            // TenantGUID
            // 
            this.TenantGUID.HeaderText = "Tenant GUID";
            this.TenantGUID.Name = "TenantGUID";
            this.TenantGUID.ReadOnly = true;
            this.TenantGUID.Visible = false;
            // 
            // TenantName
            // 
            this.TenantName.HeaderText = "Tenant Name";
            this.TenantName.Name = "TenantName";
            this.TenantName.ReadOnly = true;
            // 
            // TenantDescription
            // 
            this.TenantDescription.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.TenantDescription.HeaderText = "Tenant Description";
            this.TenantDescription.Name = "TenantDescription";
            this.TenantDescription.ReadOnly = true;
            // 
            // btnRemove
            // 
            this.btnRemove.AccessibleName = "Button";
            this.btnRemove.Enabled = false;
            this.btnRemove.Font = new System.Drawing.Font("Segoe UI Semibold", 9F);
            this.btnRemove.Location = new System.Drawing.Point(304, 226);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(81, 30);
            this.btnRemove.TabIndex = 92;
            this.btnRemove.Text = "Remove";
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.AccessibleName = "Button";
            this.btnAdd.Font = new System.Drawing.Font("Segoe UI Semibold", 9F);
            this.btnAdd.Location = new System.Drawing.Point(217, 225);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(81, 31);
            this.btnAdd.TabIndex = 93;
            this.btnAdd.Text = "Add";
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnTenantPermissions
            // 
            this.btnTenantPermissions.AccessibleName = "Button";
            this.btnTenantPermissions.Font = new System.Drawing.Font("Segoe UI Semibold", 9F);
            this.btnTenantPermissions.Location = new System.Drawing.Point(130, 225);
            this.btnTenantPermissions.Name = "btnTenantPermissions";
            this.btnTenantPermissions.Size = new System.Drawing.Size(81, 31);
            this.btnTenantPermissions.TabIndex = 94;
            this.btnTenantPermissions.Text = "Permissions";
            this.btnTenantPermissions.Click += new System.EventHandler(this.btnTenantPermissions_Click);
            // 
            // btnInviteTest
            // 
            this.btnInviteTest.AccessibleName = "Button";
            this.btnInviteTest.Enabled = false;
            this.btnInviteTest.Font = new System.Drawing.Font("Segoe UI Semibold", 9F);
            this.btnInviteTest.Location = new System.Drawing.Point(5, 225);
            this.btnInviteTest.Name = "btnInviteTest";
            this.btnInviteTest.Size = new System.Drawing.Size(96, 28);
            this.btnInviteTest.TabIndex = 95;
            this.btnInviteTest.Text = "Invite Test";
            this.btnInviteTest.Click += new System.EventHandler(this.btnInviteTest_Click);
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "Tenant GUID";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Visible = false;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.HeaderText = "Tenant Name";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn3.HeaderText = "Tenant Description";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            // 
            // tenantItemBindingSource
            // 
            this.tenantItemBindingSource.DataSource = typeof(CyConex.TenantItem);
            // 
            // SelectTenantModal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(477, 267);
            this.Controls.Add(this.btnInviteTest);
            this.Controls.Add(this.btnTenantPermissions);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.btnRemove);
            this.Controls.Add(this.gridTenants);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnSelect);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SelectTenantModal";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Style.MdiChild.IconHorizontalAlignment = System.Windows.Forms.HorizontalAlignment.Center;
            this.Style.MdiChild.IconVerticalAlignment = System.Windows.Forms.VisualStyles.VerticalAlignment.Center;
            this.Text = "Select Tenant";
            this.TopMost = true;
            ((System.ComponentModel.ISupportInitialize)(this.gridTenants)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tenantItemBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private Syncfusion.WinForms.Controls.SfButton btnSelect;
        private System.Windows.Forms.DataGridView gridTenants;
        private System.Windows.Forms.BindingSource tenantItemBindingSource;
        private Syncfusion.WinForms.Controls.SfButton btnRemove;
        private Syncfusion.WinForms.Controls.SfButton btnAdd;
        private System.Windows.Forms.DataGridViewTextBoxColumn TenantGUID;
        private System.Windows.Forms.DataGridViewTextBoxColumn TenantName;
        private System.Windows.Forms.DataGridViewTextBoxColumn TenantDescription;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private Syncfusion.WinForms.Controls.SfButton btnTenantPermissions;
        private Syncfusion.WinForms.Controls.SfButton btnInviteTest;
    }
}