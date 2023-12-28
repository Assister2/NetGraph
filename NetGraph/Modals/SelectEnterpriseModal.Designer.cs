namespace CyConex
{
    partial class SelectEnterpriseModal
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SelectEnterpriseModal));
            this.label1 = new System.Windows.Forms.Label();
            this.btnSelect = new Syncfusion.WinForms.Controls.SfButton();
            this.gridEnterprises = new System.Windows.Forms.DataGridView();
            this.EnterpriseGUID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EnterpriseNaem = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AddressLine1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AddressLine2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Postcode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.City = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.State = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Country = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnRemove = new Syncfusion.WinForms.Controls.SfButton();
            this.btnAdd = new Syncfusion.WinForms.Controls.SfButton();
            this.btnEnterprisePermission = new Syncfusion.WinForms.Controls.SfButton();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.enterpriseItemBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.gridEnterprises)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.enterpriseItemBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(5, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(259, 17);
            this.label1.TabIndex = 86;
            this.label1.Text = "Which Enterprise would you like to use :";
            // 
            // btnSelect
            // 
            this.btnSelect.AccessibleName = "Button";
            this.btnSelect.Font = new System.Drawing.Font("Segoe UI Semibold", 9F);
            this.btnSelect.Location = new System.Drawing.Point(391, 226);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(81, 30);
            this.btnSelect.TabIndex = 3;
            this.btnSelect.Text = "Select";
            this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
            // 
            // gridEnterprises
            // 
            this.gridEnterprises.AllowUserToAddRows = false;
            this.gridEnterprises.AllowUserToDeleteRows = false;
            this.gridEnterprises.AllowUserToResizeRows = false;
            this.gridEnterprises.BackgroundColor = System.Drawing.SystemColors.Window;
            this.gridEnterprises.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridEnterprises.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.EnterpriseGUID,
            this.EnterpriseNaem,
            this.AddressLine1,
            this.AddressLine2,
            this.Postcode,
            this.City,
            this.State,
            this.Country});
            this.gridEnterprises.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.gridEnterprises.Location = new System.Drawing.Point(5, 25);
            this.gridEnterprises.MultiSelect = false;
            this.gridEnterprises.Name = "gridEnterprises";
            this.gridEnterprises.ReadOnly = true;
            this.gridEnterprises.RowHeadersVisible = false;
            this.gridEnterprises.RowTemplate.ReadOnly = true;
            this.gridEnterprises.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridEnterprises.Size = new System.Drawing.Size(467, 195);
            this.gridEnterprises.TabIndex = 87;
            this.gridEnterprises.Click += new System.EventHandler(this.gridEnterprises_Click);
            // 
            // EnterpriseGUID
            // 
            this.EnterpriseGUID.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.EnterpriseGUID.HeaderText = "Enterprise GUID";
            this.EnterpriseGUID.Name = "EnterpriseGUID";
            this.EnterpriseGUID.ReadOnly = true;
            this.EnterpriseGUID.Visible = false;
            // 
            // EnterpriseNaem
            // 
            this.EnterpriseNaem.HeaderText = "Enterprise Name";
            this.EnterpriseNaem.Name = "EnterpriseNaem";
            this.EnterpriseNaem.ReadOnly = true;
            this.EnterpriseNaem.Width = 261;
            // 
            // AddressLine1
            // 
            this.AddressLine1.HeaderText = "Address Line1";
            this.AddressLine1.Name = "AddressLine1";
            this.AddressLine1.ReadOnly = true;
            // 
            // AddressLine2
            // 
            this.AddressLine2.HeaderText = "Address Line2";
            this.AddressLine2.Name = "AddressLine2";
            this.AddressLine2.ReadOnly = true;
            this.AddressLine2.Visible = false;
            // 
            // Postcode
            // 
            this.Postcode.HeaderText = "Postcode";
            this.Postcode.Name = "Postcode";
            this.Postcode.ReadOnly = true;
            this.Postcode.Visible = false;
            // 
            // City
            // 
            this.City.HeaderText = "City";
            this.City.Name = "City";
            this.City.ReadOnly = true;
            // 
            // State
            // 
            this.State.HeaderText = "State";
            this.State.Name = "State";
            this.State.ReadOnly = true;
            this.State.Visible = false;
            // 
            // Country
            // 
            this.Country.HeaderText = "Country";
            this.Country.Name = "Country";
            this.Country.ReadOnly = true;
            this.Country.Visible = false;
            // 
            // btnRemove
            // 
            this.btnRemove.AccessibleName = "Button";
            this.btnRemove.Enabled = false;
            this.btnRemove.Font = new System.Drawing.Font("Segoe UI Semibold", 9F);
            this.btnRemove.Location = new System.Drawing.Point(304, 226);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(81, 30);
            this.btnRemove.TabIndex = 88;
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
            this.btnAdd.TabIndex = 89;
            this.btnAdd.Text = "Add";
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnEnterprisePermission
            // 
            this.btnEnterprisePermission.AccessibleName = "Button";
            this.btnEnterprisePermission.Font = new System.Drawing.Font("Segoe UI Semibold", 9F);
            this.btnEnterprisePermission.Location = new System.Drawing.Point(130, 226);
            this.btnEnterprisePermission.Name = "btnEnterprisePermission";
            this.btnEnterprisePermission.Size = new System.Drawing.Size(81, 31);
            this.btnEnterprisePermission.TabIndex = 95;
            this.btnEnterprisePermission.Text = "Permissions";
            this.btnEnterprisePermission.Click += new System.EventHandler(this.btnEnterprisePermission_Click);
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn1.HeaderText = "Enterprise GUID";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Visible = false;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.HeaderText = "Enterprise Name";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            this.dataGridViewTextBoxColumn2.Width = 261;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.HeaderText = "Address Line1";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.HeaderText = "Address Line2";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            this.dataGridViewTextBoxColumn4.Visible = false;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.HeaderText = "Postcode";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.ReadOnly = true;
            this.dataGridViewTextBoxColumn5.Visible = false;
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.HeaderText = "City";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn7
            // 
            this.dataGridViewTextBoxColumn7.HeaderText = "State";
            this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            this.dataGridViewTextBoxColumn7.ReadOnly = true;
            this.dataGridViewTextBoxColumn7.Visible = false;
            // 
            // dataGridViewTextBoxColumn8
            // 
            this.dataGridViewTextBoxColumn8.HeaderText = "Country";
            this.dataGridViewTextBoxColumn8.Name = "dataGridViewTextBoxColumn8";
            this.dataGridViewTextBoxColumn8.ReadOnly = true;
            this.dataGridViewTextBoxColumn8.Visible = false;
            // 
            // enterpriseItemBindingSource
            // 
            this.enterpriseItemBindingSource.DataSource = typeof(CyConex.EnterpriseItem);
            // 
            // SelectEnterpriseModal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(477, 267);
            this.CloseButtonVisible = false;
            this.ControlBox = false;
            this.Controls.Add(this.btnEnterprisePermission);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.btnRemove);
            this.Controls.Add(this.gridEnterprises);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnSelect);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SelectEnterpriseModal";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Style.MdiChild.IconHorizontalAlignment = System.Windows.Forms.HorizontalAlignment.Center;
            this.Style.MdiChild.IconVerticalAlignment = System.Windows.Forms.VisualStyles.VerticalAlignment.Center;
            this.Text = "Select Enterprise";
            this.TopMost = true;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.SelectEnterprise_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.gridEnterprises)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.enterpriseItemBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private Syncfusion.WinForms.Controls.SfButton btnSelect;
        private System.Windows.Forms.BindingSource enterpriseItemBindingSource;
        private Syncfusion.WinForms.Controls.SfButton btnRemove;
        public System.Windows.Forms.DataGridView gridEnterprises;
        private Syncfusion.WinForms.Controls.SfButton btnAdd;
        private System.Windows.Forms.DataGridViewTextBoxColumn EnterpriseGUID;
        private System.Windows.Forms.DataGridViewTextBoxColumn EnterpriseNaem;
        private System.Windows.Forms.DataGridViewTextBoxColumn AddressLine1;
        private System.Windows.Forms.DataGridViewTextBoxColumn AddressLine2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Postcode;
        private System.Windows.Forms.DataGridViewTextBoxColumn City;
        private System.Windows.Forms.DataGridViewTextBoxColumn State;
        private System.Windows.Forms.DataGridViewTextBoxColumn Country;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn8;
        private Syncfusion.WinForms.Controls.SfButton btnEnterprisePermission;
    }
}