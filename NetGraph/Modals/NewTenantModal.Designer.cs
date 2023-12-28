namespace CyConex
{
    partial class NewTenantModal
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NewTenantModal));
            this.btnTenantCreate = new Syncfusion.WinForms.Controls.SfButton();
            this.btnTenantCancel = new Syncfusion.WinForms.Controls.SfButton();
            this.txtTenantName = new Syncfusion.Windows.Forms.Tools.TextBoxExt();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtTenantDescription = new Syncfusion.Windows.Forms.Tools.TextBoxExt();
            ((System.ComponentModel.ISupportInitialize)(this.txtTenantName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTenantDescription)).BeginInit();
            this.SuspendLayout();
            // 
            // btnTenantCreate
            // 
            this.btnTenantCreate.AccessibleName = "Button";
            this.btnTenantCreate.Font = new System.Drawing.Font("Segoe UI Semibold", 9F);
            this.btnTenantCreate.Location = new System.Drawing.Point(346, 245);
            this.btnTenantCreate.Name = "btnTenantCreate";
            this.btnTenantCreate.Size = new System.Drawing.Size(79, 29);
            this.btnTenantCreate.TabIndex = 4;
            this.btnTenantCreate.Text = "Create";
            this.btnTenantCreate.Click += new System.EventHandler(this.btnTenantCreate_Click);
            // 
            // btnTenantCancel
            // 
            this.btnTenantCancel.AccessibleName = "Button";
            this.btnTenantCancel.Font = new System.Drawing.Font("Segoe UI Semibold", 9F);
            this.btnTenantCancel.Location = new System.Drawing.Point(261, 245);
            this.btnTenantCancel.Name = "btnTenantCancel";
            this.btnTenantCancel.Size = new System.Drawing.Size(79, 29);
            this.btnTenantCancel.TabIndex = 3;
            this.btnTenantCancel.TabStop = false;
            this.btnTenantCancel.Text = "Cancel";
            this.btnTenantCancel.Click += new System.EventHandler(this.btnTenantCancel_Click);
            // 
            // txtTenantName
            // 
            this.txtTenantName.BeforeTouchSize = new System.Drawing.Size(199, 20);
            this.txtTenantName.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTenantName.Location = new System.Drawing.Point(8, 59);
            this.txtTenantName.Name = "txtTenantName";
            this.txtTenantName.Size = new System.Drawing.Size(417, 20);
            this.txtTenantName.TabIndex = 1;
            this.txtTenantName.TabStop = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(5, 87);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(97, 13);
            this.label3.TabIndex = 87;
            this.label3.Text = "Tenant Description";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(5, 43);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 13);
            this.label2.TabIndex = 86;
            this.label2.Text = "Tenant Name";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(5, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(240, 17);
            this.label1.TabIndex = 85;
            this.label1.Text = "Please Enter the New Tenant Details";
            // 
            // txtTenantDescription
            // 
            this.txtTenantDescription.BeforeTouchSize = new System.Drawing.Size(199, 20);
            this.txtTenantDescription.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTenantDescription.Location = new System.Drawing.Point(8, 103);
            this.txtTenantDescription.Multiline = true;
            this.txtTenantDescription.Name = "txtTenantDescription";
            this.txtTenantDescription.Size = new System.Drawing.Size(417, 136);
            this.txtTenantDescription.TabIndex = 2;
            this.txtTenantDescription.TabStop = false;
            // 
            // NewTenantModal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(428, 288);
            this.CloseButtonVisible = false;
            this.ControlBox = false;
            this.Controls.Add(this.btnTenantCreate);
            this.Controls.Add(this.btnTenantCancel);
            this.Controls.Add(this.txtTenantDescription);
            this.Controls.Add(this.txtTenantName);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "NewTenantModal";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Style.MdiChild.IconHorizontalAlignment = System.Windows.Forms.HorizontalAlignment.Center;
            this.Style.MdiChild.IconVerticalAlignment = System.Windows.Forms.VisualStyles.VerticalAlignment.Center;
            this.Text = "New Tenant";
            this.TopMost = true;
            ((System.ComponentModel.ISupportInitialize)(this.txtTenantName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTenantDescription)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Syncfusion.WinForms.Controls.SfButton btnTenantCreate;
        private Syncfusion.WinForms.Controls.SfButton btnTenantCancel;
        private Syncfusion.Windows.Forms.Tools.TextBoxExt txtTenantName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private Syncfusion.Windows.Forms.Tools.TextBoxExt txtTenantDescription;
    }
}