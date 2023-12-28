namespace CyConex
{
    partial class SecurityEditorModal
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SecurityEditorModal));
            this.label1 = new System.Windows.Forms.Label();
            this.lblObjectTitle = new System.Windows.Forms.Label();
            this.dataGridUserList = new System.Windows.Forms.DataGridView();
            this.field_first_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.field_last_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.field_email = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.field_group = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.chkEditSecurity = new System.Windows.Forms.CheckBox();
            this.chkGroupCreate = new System.Windows.Forms.CheckBox();
            this.chkGroupRead = new System.Windows.Forms.CheckBox();
            this.chkGroupUpdate = new System.Windows.Forms.CheckBox();
            this.chkGroupDelete = new System.Windows.Forms.CheckBox();
            this.autoLabel1 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.btnAddUser = new Syncfusion.WinForms.Controls.SfButton();
            this.btnRemoveUser = new Syncfusion.WinForms.Controls.SfButton();
            this.btnEditUser = new Syncfusion.WinForms.Controls.SfButton();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridUserList)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Object";
            // 
            // lblObjectTitle
            // 
            this.lblObjectTitle.AutoSize = true;
            this.lblObjectTitle.Location = new System.Drawing.Point(88, 21);
            this.lblObjectTitle.Name = "lblObjectTitle";
            this.lblObjectTitle.Size = new System.Drawing.Size(61, 13);
            this.lblObjectTitle.TabIndex = 0;
            this.lblObjectTitle.Text = "Object Title";
            // 
            // dataGridUserList
            // 
            this.dataGridUserList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridUserList.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            this.dataGridUserList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridUserList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.field_first_name,
            this.field_last_name,
            this.field_email,
            this.field_group});
            this.dataGridUserList.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dataGridUserList.Location = new System.Drawing.Point(25, 56);
            this.dataGridUserList.Name = "dataGridUserList";
            this.dataGridUserList.RowHeadersVisible = false;
            this.dataGridUserList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridUserList.Size = new System.Drawing.Size(564, 266);
            this.dataGridUserList.TabIndex = 1;
            this.dataGridUserList.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridUserList_CellContentClick);
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
            // field_group
            // 
            this.field_group.HeaderText = "Group";
            this.field_group.Name = "field_group";
            // 
            // chkEditSecurity
            // 
            this.chkEditSecurity.AllowDrop = true;
            this.chkEditSecurity.AutoSize = true;
            this.chkEditSecurity.Enabled = false;
            this.chkEditSecurity.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkEditSecurity.Location = new System.Drawing.Point(121, 339);
            this.chkEditSecurity.Name = "chkEditSecurity";
            this.chkEditSecurity.Size = new System.Drawing.Size(106, 21);
            this.chkEditSecurity.TabIndex = 2;
            this.chkEditSecurity.Text = "Edit Security";
            this.chkEditSecurity.UseVisualStyleBackColor = true;
            this.chkEditSecurity.Visible = false;
            // 
            // chkGroupCreate
            // 
            this.chkGroupCreate.AutoSize = true;
            this.chkGroupCreate.Enabled = false;
            this.chkGroupCreate.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkGroupCreate.Location = new System.Drawing.Point(252, 339);
            this.chkGroupCreate.Name = "chkGroupCreate";
            this.chkGroupCreate.Size = new System.Drawing.Size(69, 21);
            this.chkGroupCreate.TabIndex = 2;
            this.chkGroupCreate.Text = "Create";
            this.chkGroupCreate.UseVisualStyleBackColor = true;
            // 
            // chkGroupRead
            // 
            this.chkGroupRead.AutoSize = true;
            this.chkGroupRead.Enabled = false;
            this.chkGroupRead.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkGroupRead.Location = new System.Drawing.Point(338, 339);
            this.chkGroupRead.Name = "chkGroupRead";
            this.chkGroupRead.Size = new System.Drawing.Size(61, 21);
            this.chkGroupRead.TabIndex = 2;
            this.chkGroupRead.Text = "Read";
            this.chkGroupRead.UseVisualStyleBackColor = true;
            // 
            // chkGroupUpdate
            // 
            this.chkGroupUpdate.AutoSize = true;
            this.chkGroupUpdate.Enabled = false;
            this.chkGroupUpdate.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkGroupUpdate.Location = new System.Drawing.Point(422, 339);
            this.chkGroupUpdate.Name = "chkGroupUpdate";
            this.chkGroupUpdate.Size = new System.Drawing.Size(73, 21);
            this.chkGroupUpdate.TabIndex = 2;
            this.chkGroupUpdate.Text = "Update";
            this.chkGroupUpdate.UseVisualStyleBackColor = true;
            // 
            // chkGroupDelete
            // 
            this.chkGroupDelete.AutoSize = true;
            this.chkGroupDelete.Enabled = false;
            this.chkGroupDelete.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkGroupDelete.Location = new System.Drawing.Point(521, 339);
            this.chkGroupDelete.Name = "chkGroupDelete";
            this.chkGroupDelete.Size = new System.Drawing.Size(68, 21);
            this.chkGroupDelete.TabIndex = 2;
            this.chkGroupDelete.Text = "Delete";
            this.chkGroupDelete.UseVisualStyleBackColor = true;
            // 
            // autoLabel1
            // 
            this.autoLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoLabel1.Location = new System.Drawing.Point(24, 339);
            this.autoLabel1.Name = "autoLabel1";
            this.autoLabel1.Size = new System.Drawing.Size(88, 17);
            this.autoLabel1.TabIndex = 3;
            this.autoLabel1.Text = "Permissions:";
            // 
            // btnAddUser
            // 
            this.btnAddUser.AccessibleName = "Button";
            this.btnAddUser.Font = new System.Drawing.Font("Segoe UI Semibold", 9F);
            this.btnAddUser.Location = new System.Drawing.Point(286, 375);
            this.btnAddUser.Name = "btnAddUser";
            this.btnAddUser.Size = new System.Drawing.Size(96, 28);
            this.btnAddUser.TabIndex = 4;
            this.btnAddUser.Text = "Add User";
            this.btnAddUser.Click += new System.EventHandler(this.btnAddUser_Click);
            // 
            // btnRemoveUser
            // 
            this.btnRemoveUser.AccessibleName = "Button";
            this.btnRemoveUser.Font = new System.Drawing.Font("Segoe UI Semibold", 9F);
            this.btnRemoveUser.Location = new System.Drawing.Point(388, 375);
            this.btnRemoveUser.Name = "btnRemoveUser";
            this.btnRemoveUser.Size = new System.Drawing.Size(96, 28);
            this.btnRemoveUser.TabIndex = 4;
            this.btnRemoveUser.Text = "Remove User";
            this.btnRemoveUser.Click += new System.EventHandler(this.btnRemoveUser_Click);
            // 
            // btnEditUser
            // 
            this.btnEditUser.AccessibleName = "Button";
            this.btnEditUser.Font = new System.Drawing.Font("Segoe UI Semibold", 9F);
            this.btnEditUser.Location = new System.Drawing.Point(490, 375);
            this.btnEditUser.Name = "btnEditUser";
            this.btnEditUser.Size = new System.Drawing.Size(96, 28);
            this.btnEditUser.TabIndex = 4;
            this.btnEditUser.Text = "Edit User";
            this.btnEditUser.Click += new System.EventHandler(this.btnEditUser_Click);
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "First Name";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.Width = 140;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.HeaderText = "Last Name";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.Width = 141;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.HeaderText = "Email";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.Width = 140;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.HeaderText = "Group";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.Width = 140;
            // 
            // SecurityEditorModal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(613, 425);
            this.CloseButtonVisible = false;
            this.ControlBox = false;
            this.Controls.Add(this.btnEditUser);
            this.Controls.Add(this.btnRemoveUser);
            this.Controls.Add(this.btnAddUser);
            this.Controls.Add(this.autoLabel1);
            this.Controls.Add(this.chkGroupDelete);
            this.Controls.Add(this.chkGroupUpdate);
            this.Controls.Add(this.chkGroupRead);
            this.Controls.Add(this.chkGroupCreate);
            this.Controls.Add(this.chkEditSecurity);
            this.Controls.Add(this.dataGridUserList);
            this.Controls.Add(this.lblObjectTitle);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SecurityEditorModal";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Style.MdiChild.IconHorizontalAlignment = System.Windows.Forms.HorizontalAlignment.Center;
            this.Style.MdiChild.IconVerticalAlignment = System.Windows.Forms.VisualStyles.VerticalAlignment.Center;
            this.Text = "SecurityEditor";
            this.TopMost = true;
            ((System.ComponentModel.ISupportInitialize)(this.dataGridUserList)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblObjectTitle;
        private System.Windows.Forms.DataGridView dataGridUserList;
        private System.Windows.Forms.DataGridViewTextBoxColumn field_first_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn field_last_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn field_email;
        private System.Windows.Forms.DataGridViewTextBoxColumn field_group;
        private System.Windows.Forms.CheckBox chkEditSecurity;
        private System.Windows.Forms.CheckBox chkGroupCreate;
        private System.Windows.Forms.CheckBox chkGroupRead;
        private System.Windows.Forms.CheckBox chkGroupUpdate;
        private System.Windows.Forms.CheckBox chkGroupDelete;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel1;
        private Syncfusion.WinForms.Controls.SfButton btnAddUser;
        private Syncfusion.WinForms.Controls.SfButton btnRemoveUser;
        private Syncfusion.WinForms.Controls.SfButton btnEditUser;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
    }
}