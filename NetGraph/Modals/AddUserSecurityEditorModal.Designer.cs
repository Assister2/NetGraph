namespace CyConex
{
    partial class AddUserSecurityEditorModal
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
            this.autoLabel1 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.txtFilterContent = new Syncfusion.Windows.Forms.Grid.GridAwareTextBox();
            this.gridUserList = new System.Windows.Forms.DataGridView();
            this.field_first_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.field_last_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.field_email = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.autoLabel2 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.btnAddUser = new Syncfusion.WinForms.Controls.SfButton();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cmbSecurityGroup = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.gridUserList)).BeginInit();
            this.SuspendLayout();
            // 
            // autoLabel1
            // 
            this.autoLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLabel1.Location = new System.Drawing.Point(29, 28);
            this.autoLabel1.Name = "autoLabel1";
            this.autoLabel1.Size = new System.Drawing.Size(43, 17);
            this.autoLabel1.TabIndex = 0;
            this.autoLabel1.Text = "Filter:";
            // 
            // txtFilterContent
            // 
            this.txtFilterContent.AutoSuggestFormula = false;
            this.txtFilterContent.DisabledBackColor = System.Drawing.SystemColors.Window;
            this.txtFilterContent.EnabledBackColor = System.Drawing.SystemColors.Window;
            this.txtFilterContent.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFilterContent.Location = new System.Drawing.Point(78, 28);
            this.txtFilterContent.Name = "txtFilterContent";
            this.txtFilterContent.Size = new System.Drawing.Size(515, 23);
            this.txtFilterContent.TabIndex = 1;
            // 
            // gridUserList
            // 
            this.gridUserList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.gridUserList.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            this.gridUserList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridUserList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.field_first_name,
            this.field_last_name,
            this.field_email});
            this.gridUserList.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.gridUserList.Location = new System.Drawing.Point(29, 57);
            this.gridUserList.Name = "gridUserList";
            this.gridUserList.RowHeadersVisible = false;
            this.gridUserList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridUserList.Size = new System.Drawing.Size(564, 266);
            this.gridUserList.TabIndex = 2;
            // 
            // field_first_name
            // 
            this.field_first_name.HeaderText = "First Name";
            this.field_first_name.Name = "field_first_name";
            // 
            // field_last_name
            // 
            this.field_last_name.HeaderText = "Last Name";
            this.field_last_name.Name = "field_last_name";
            // 
            // field_email
            // 
            this.field_email.HeaderText = "Email";
            this.field_email.Name = "field_email";
            // 
            // autoLabel2
            // 
            this.autoLabel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLabel2.Location = new System.Drawing.Point(29, 335);
            this.autoLabel2.Name = "autoLabel2";
            this.autoLabel2.Size = new System.Drawing.Size(107, 17);
            this.autoLabel2.TabIndex = 0;
            this.autoLabel2.Text = "Security Group:";
            // 
            // btnAddUser
            // 
            this.btnAddUser.AccessibleName = "Button";
            this.btnAddUser.Font = new System.Drawing.Font("Segoe UI Semibold", 9F);
            this.btnAddUser.Location = new System.Drawing.Point(497, 329);
            this.btnAddUser.Name = "btnAddUser";
            this.btnAddUser.Size = new System.Drawing.Size(96, 28);
            this.btnAddUser.TabIndex = 4;
            this.btnAddUser.Text = "Add User";
            this.btnAddUser.Click += new System.EventHandler(this.btnAddUser_Click);
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "First Name";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.Width = 187;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.HeaderText = "Last Name";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.Width = 187;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.HeaderText = "Email";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.Width = 187;
            // 
            // cmbSecurityGroup
            // 
            this.cmbSecurityGroup.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cmbSecurityGroup.Location = new System.Drawing.Point(142, 331);
            this.cmbSecurityGroup.Name = "cmbSecurityGroup";
            this.cmbSecurityGroup.Size = new System.Drawing.Size(220, 24);
            this.cmbSecurityGroup.TabIndex = 5;
            // 
            // AddUserSecurityEditorModal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(620, 384);
            this.CloseButtonVisible = false;
            this.ControlBox = false;
            this.Controls.Add(this.cmbSecurityGroup);
            this.Controls.Add(this.btnAddUser);
            this.Controls.Add(this.gridUserList);
            this.Controls.Add(this.txtFilterContent);
            this.Controls.Add(this.autoLabel2);
            this.Controls.Add(this.autoLabel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AddUserSecurityEditorModal";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Style.MdiChild.IconHorizontalAlignment = System.Windows.Forms.HorizontalAlignment.Center;
            this.Style.MdiChild.IconVerticalAlignment = System.Windows.Forms.VisualStyles.VerticalAlignment.Center;
            this.Text = "AddUserSecurityEditor";
            this.TopMost = true;
            ((System.ComponentModel.ISupportInitialize)(this.gridUserList)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel1;
        private Syncfusion.Windows.Forms.Grid.GridAwareTextBox txtFilterContent;
        private System.Windows.Forms.DataGridView gridUserList;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel2;
        private Syncfusion.WinForms.Controls.SfButton btnAddUser;
        private System.Windows.Forms.DataGridViewTextBoxColumn field_first_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn field_last_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn field_email;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.ComboBox cmbSecurityGroup;
    }
}