namespace CyConex
{
    partial class NodeCategoryModal
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NodeCategoryModal));
            this.txtCaption = new System.Windows.Forms.Label();
            this.categoryText = new Syncfusion.Windows.Forms.Tools.TextBoxExt();
            this.btnOk = new Syncfusion.WinForms.Controls.SfButton();
            this.btnCancel = new Syncfusion.WinForms.Controls.SfButton();
            ((System.ComponentModel.ISupportInitialize)(this.categoryText)).BeginInit();
            this.SuspendLayout();
            // 
            // txtCaption
            // 
            this.txtCaption.AutoSize = true;
            this.txtCaption.Location = new System.Drawing.Point(6, 12);
            this.txtCaption.Name = "txtCaption";
            this.txtCaption.Size = new System.Drawing.Size(49, 13);
            this.txtCaption.TabIndex = 0;
            this.txtCaption.Text = "Category";
            // 
            // categoryText
            // 
            this.categoryText.BeforeTouchSize = new System.Drawing.Size(206, 23);
            this.categoryText.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.categoryText.Location = new System.Drawing.Point(9, 28);
            this.categoryText.Name = "categoryText";
            this.categoryText.Size = new System.Drawing.Size(265, 23);
            this.categoryText.TabIndex = 1;
            // 
            // btnOk
            // 
            this.btnOk.AccessibleName = "Button";
            this.btnOk.Font = new System.Drawing.Font("Segoe UI Semibold", 9F);
            this.btnOk.Location = new System.Drawing.Point(128, 57);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(70, 25);
            this.btnOk.TabIndex = 2;
            this.btnOk.Text = "Ok";
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.AccessibleName = "Button";
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI Semibold", 9F);
            this.btnCancel.Location = new System.Drawing.Point(204, 57);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(70, 25);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // NodeCategoryModal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(280, 94);
            this.CloseButtonVisible = false;
            this.ControlBox = false;
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.categoryText);
            this.Controls.Add(this.txtCaption);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "NodeCategoryModal";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Style.MdiChild.IconHorizontalAlignment = System.Windows.Forms.HorizontalAlignment.Center;
            this.Style.MdiChild.IconVerticalAlignment = System.Windows.Forms.VisualStyles.VerticalAlignment.Center;
            this.Text = "Node Category";
            this.Load += new System.EventHandler(this.NodeCategoryModal_Load);
            ((System.ComponentModel.ISupportInitialize)(this.categoryText)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Label txtCaption;
        private Syncfusion.Windows.Forms.Tools.TextBoxExt categoryText;
        private Syncfusion.WinForms.Controls.SfButton btnOk;
        private Syncfusion.WinForms.Controls.SfButton btnCancel;
    }
}