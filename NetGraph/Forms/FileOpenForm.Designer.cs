namespace CyConex
{
    partial class FileOpenForm
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
            Syncfusion.Windows.Forms.BannerTextInfo bannerTextInfo1 = new Syncfusion.Windows.Forms.BannerTextInfo();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FileOpenForm));
            this.tabControls = new Syncfusion.Windows.Forms.Tools.TabControlAdv();
            this.tabPageNew = new Syncfusion.Windows.Forms.Tools.TabPageAdv();
            this.tabPageRecent = new Syncfusion.Windows.Forms.Tools.TabPageAdv();
            this.flowParentPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.panel8 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label18 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.pictureBox6 = new System.Windows.Forms.PictureBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tabPageCloud = new Syncfusion.Windows.Forms.Tools.TabPageAdv();
            this.flowLayoutCloud = new System.Windows.Forms.FlowLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.textBoxSearch = new Syncfusion.Windows.Forms.Tools.TextBoxExt();
            this.tabPageLocal = new Syncfusion.Windows.Forms.Tools.TabPageAdv();
            this.btnLocalOpen = new System.Windows.Forms.Button();
            this.bannerTextProvider1 = new Syncfusion.Windows.Forms.BannerTextProvider(this.components);
            this.designTimeTabTypeLoader = new Syncfusion.Reflection.TypeLoader(this.components);
            this.sfButton1 = new Syncfusion.WinForms.Controls.SfButton();
            ((System.ComponentModel.ISupportInitialize)(this.tabControls)).BeginInit();
            this.tabControls.SuspendLayout();
            this.tabPageNew.SuspendLayout();
            this.tabPageRecent.SuspendLayout();
            this.flowParentPanel.SuspendLayout();
            this.panel8.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox6)).BeginInit();
            this.panel2.SuspendLayout();
            this.tabPageCloud.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.textBoxSearch)).BeginInit();
            this.tabPageLocal.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControls
            // 
            this.tabControls.ActiveTabColor = System.Drawing.Color.SteelBlue;
            this.tabControls.ActiveTabFont = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.tabControls.Alignment = System.Windows.Forms.TabAlignment.Left;
            this.tabControls.BeforeTouchSize = new System.Drawing.Size(800, 450);
            this.tabControls.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tabControls.Controls.Add(this.tabPageNew);
            this.tabControls.Controls.Add(this.tabPageRecent);
            this.tabControls.Controls.Add(this.tabPageCloud);
            this.tabControls.Controls.Add(this.tabPageLocal);
            this.tabControls.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControls.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControls.InactiveTabColor = System.Drawing.Color.White;
            this.tabControls.ItemSize = new System.Drawing.Size(200, 89);
            this.tabControls.Location = new System.Drawing.Point(0, 0);
            this.tabControls.Margin = new System.Windows.Forms.Padding(0);
            this.tabControls.Name = "tabControls";
            this.tabControls.Padding = new System.Drawing.Point(20, 20);
            this.tabControls.RotateTextWhenVertical = true;
            this.tabControls.Size = new System.Drawing.Size(800, 450);
            this.tabControls.TabIndex = 0;
            this.tabControls.TabStyle = typeof(Syncfusion.Windows.Forms.Tools.TabRendererOffice2016White);
            this.tabControls.TextLineAlignment = System.Drawing.StringAlignment.Near;
            this.tabControls.ThemeName = "TabRendererOffice2016White";
            this.tabControls.SelectedIndexChanged += new System.EventHandler(this.tabControlAdv1_SelectedIndexChanged);
            // 
            // tabPageNew
            // 
            this.tabPageNew.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.tabPageNew.Controls.Add(this.sfButton1);
            this.tabPageNew.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(38)))), ((int)(((byte)(38)))));
            this.tabPageNew.Image = ((System.Drawing.Image)(resources.GetObject("tabPageNew.Image")));
            this.tabPageNew.ImageSize = new System.Drawing.Size(32, 32);
            this.tabPageNew.Location = new System.Drawing.Point(199, 0);
            this.tabPageNew.Name = "tabPageNew";
            this.tabPageNew.ShowCloseButton = true;
            this.tabPageNew.Size = new System.Drawing.Size(601, 450);
            this.tabPageNew.TabIndex = 4;
            this.tabPageNew.Text = "New";
            this.tabPageNew.ThemesEnabled = false;
            // 
            // tabPageRecent
            // 
            this.tabPageRecent.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.tabPageRecent.Controls.Add(this.flowParentPanel);
            this.tabPageRecent.Controls.Add(this.panel2);
            this.tabPageRecent.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(38)))), ((int)(((byte)(38)))));
            this.tabPageRecent.Image = ((System.Drawing.Image)(resources.GetObject("tabPageRecent.Image")));
            this.tabPageRecent.ImageSize = new System.Drawing.Size(32, 32);
            this.tabPageRecent.Location = new System.Drawing.Point(199, 0);
            this.tabPageRecent.Name = "tabPageRecent";
            this.tabPageRecent.ShowCloseButton = true;
            this.tabPageRecent.Size = new System.Drawing.Size(601, 450);
            this.tabPageRecent.TabIndex = 1;
            this.tabPageRecent.Text = "Recent";
            this.tabPageRecent.ThemesEnabled = false;
            // 
            // flowParentPanel
            // 
            this.flowParentPanel.AutoScroll = true;
            this.flowParentPanel.Controls.Add(this.panel8);
            this.flowParentPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowParentPanel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.flowParentPanel.Location = new System.Drawing.Point(0, 24);
            this.flowParentPanel.Name = "flowParentPanel";
            this.flowParentPanel.Size = new System.Drawing.Size(601, 426);
            this.flowParentPanel.TabIndex = 1;
            // 
            // panel8
            // 
            this.panel8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(199)))), ((int)(((byte)(224)))), ((int)(((byte)(244)))));
            this.panel8.Controls.Add(this.panel3);
            this.panel8.Controls.Add(this.label18);
            this.panel8.Controls.Add(this.label19);
            this.panel8.Controls.Add(this.label20);
            this.panel8.Controls.Add(this.pictureBox6);
            this.panel8.Location = new System.Drawing.Point(3, 3);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(586, 106);
            this.panel8.TabIndex = 5;
            this.panel8.MouseEnter += new System.EventHandler(this.panel8_MouseEnter);
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.Gainsboro;
            this.panel3.Location = new System.Drawing.Point(2, 101);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(574, 3);
            this.panel3.TabIndex = 4;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.Location = new System.Drawing.Point(457, 3);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(58, 13);
            this.label18.TabIndex = 3;
            this.label18.Text = "Save Date";
            // 
            // label19
            // 
            this.label19.AutoEllipsis = true;
            this.label19.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.Location = new System.Drawing.Point(100, 24);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(460, 73);
            this.label19.TabIndex = 2;
            this.label19.Text = "Graph Description";
            // 
            // label20
            // 
            this.label20.AutoEllipsis = true;
            this.label20.BackColor = System.Drawing.Color.Transparent;
            this.label20.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label20.Location = new System.Drawing.Point(100, 3);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(347, 20);
            this.label20.TabIndex = 1;
            this.label20.Text = "Graph Title";
            // 
            // pictureBox6
            // 
            this.pictureBox6.BackColor = System.Drawing.Color.White;
            this.pictureBox6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox6.InitialImage = ((System.Drawing.Image)(resources.GetObject("pictureBox6.InitialImage")));
            this.pictureBox6.Location = new System.Drawing.Point(3, 3);
            this.pictureBox6.Name = "pictureBox6";
            this.pictureBox6.Size = new System.Drawing.Size(91, 94);
            this.pictureBox6.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox6.TabIndex = 0;
            this.pictureBox6.TabStop = false;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(601, 24);
            this.panel2.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(455, 5);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Date Modified";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(102, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Graph Name";
            // 
            // tabPageCloud
            // 
            this.tabPageCloud.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.tabPageCloud.Controls.Add(this.flowLayoutCloud);
            this.tabPageCloud.Controls.Add(this.panel1);
            this.tabPageCloud.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(38)))), ((int)(((byte)(38)))));
            this.tabPageCloud.Image = ((System.Drawing.Image)(resources.GetObject("tabPageCloud.Image")));
            this.tabPageCloud.ImageSize = new System.Drawing.Size(32, 32);
            this.tabPageCloud.Location = new System.Drawing.Point(199, 0);
            this.tabPageCloud.Name = "tabPageCloud";
            this.tabPageCloud.ShowCloseButton = true;
            this.tabPageCloud.Size = new System.Drawing.Size(601, 450);
            this.tabPageCloud.TabIndex = 2;
            this.tabPageCloud.Text = "Cloud";
            this.tabPageCloud.ThemesEnabled = false;
            // 
            // flowLayoutCloud
            // 
            this.flowLayoutCloud.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutCloud.Location = new System.Drawing.Point(0, 36);
            this.flowLayoutCloud.Name = "flowLayoutCloud";
            this.flowLayoutCloud.Size = new System.Drawing.Size(601, 414);
            this.flowLayoutCloud.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.textBoxSearch);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(601, 36);
            this.panel1.TabIndex = 0;
            // 
            // textBoxSearch
            // 
            this.textBoxSearch.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.textBoxSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            bannerTextInfo1.Mode = Syncfusion.Windows.Forms.BannerTextMode.EditMode;
            bannerTextInfo1.Text = "Search";
            bannerTextInfo1.Visible = true;
            this.bannerTextProvider1.SetBannerText(this.textBoxSearch, bannerTextInfo1);
            this.textBoxSearch.BeforeTouchSize = new System.Drawing.Size(492, 26);
            this.textBoxSearch.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(171)))), ((int)(((byte)(171)))), ((int)(((byte)(171)))));
            this.textBoxSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxSearch.CornerRadius = 2;
            this.textBoxSearch.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.textBoxSearch.DrawActiveWhenDisabled = true;
            this.textBoxSearch.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.textBoxSearch.Location = new System.Drawing.Point(3, 2);
            this.textBoxSearch.MaxLength = 200;
            this.textBoxSearch.MinimumSize = new System.Drawing.Size(8, 4);
            this.textBoxSearch.Name = "textBoxSearch";
            this.textBoxSearch.Size = new System.Drawing.Size(492, 26);
            this.textBoxSearch.Style = Syncfusion.Windows.Forms.Tools.TextBoxExt.theme.Office2016White;
            this.textBoxSearch.TabIndex = 1;
            this.textBoxSearch.ThemeName = "Office2016White";
            this.textBoxSearch.WordWrap = false;
            // 
            // tabPageLocal
            // 
            this.tabPageLocal.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.tabPageLocal.Controls.Add(this.btnLocalOpen);
            this.tabPageLocal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(38)))), ((int)(((byte)(38)))));
            this.tabPageLocal.Image = ((System.Drawing.Image)(resources.GetObject("tabPageLocal.Image")));
            this.tabPageLocal.ImageSize = new System.Drawing.Size(32, 32);
            this.tabPageLocal.Location = new System.Drawing.Point(199, 0);
            this.tabPageLocal.Name = "tabPageLocal";
            this.tabPageLocal.ShowCloseButton = true;
            this.tabPageLocal.Size = new System.Drawing.Size(601, 450);
            this.tabPageLocal.TabIndex = 3;
            this.tabPageLocal.Text = "Local";
            this.tabPageLocal.ThemesEnabled = false;
            // 
            // btnLocalOpen
            // 
            this.btnLocalOpen.Image = ((System.Drawing.Image)(resources.GetObject("btnLocalOpen.Image")));
            this.btnLocalOpen.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnLocalOpen.Location = new System.Drawing.Point(14, 12);
            this.btnLocalOpen.Name = "btnLocalOpen";
            this.btnLocalOpen.Size = new System.Drawing.Size(104, 102);
            this.btnLocalOpen.TabIndex = 0;
            this.btnLocalOpen.Text = "Browse";
            this.btnLocalOpen.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnLocalOpen.UseVisualStyleBackColor = true;
            this.btnLocalOpen.Click += new System.EventHandler(this.btnLocalOpen_Click);
            // 
            // designTimeTabTypeLoader
            // 
            this.designTimeTabTypeLoader.InvokeMemberName = "TabStyleName";
            // 
            // sfButton1
            // 
            this.sfButton1.AccessibleName = "Button";
            this.sfButton1.BackColor = System.Drawing.Color.White;
            this.sfButton1.CausesValidation = false;
            this.sfButton1.Font = new System.Drawing.Font("Segoe UI Semibold", 9F);
            this.sfButton1.ImageSize = new System.Drawing.Size(64, 64);
            this.sfButton1.Location = new System.Drawing.Point(23, 23);
            this.sfButton1.Name = "sfButton1";
            this.sfButton1.Size = new System.Drawing.Size(100, 112);
            this.sfButton1.Style.BackColor = System.Drawing.Color.White;
            this.sfButton1.Style.Image = ((System.Drawing.Image)(resources.GetObject("resource.Image")));
            this.sfButton1.TabIndex = 0;
            this.sfButton1.Text = "Blank Graph";
            this.sfButton1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.sfButton1.UseVisualStyleBackColor = false;
            // 
            // FileOpenForm
            // 
            this.AllowRoundedCorners = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.tabControls);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "FileOpenForm";
            this.Padding = new System.Windows.Forms.Padding(0);
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.ShowToolTip = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Style.MdiChild.IconHorizontalAlignment = System.Windows.Forms.HorizontalAlignment.Center;
            this.Style.MdiChild.IconVerticalAlignment = System.Windows.Forms.VisualStyles.VerticalAlignment.Center;
            this.Text = "Open Graph";
            this.TopMost = true;
            ((System.ComponentModel.ISupportInitialize)(this.tabControls)).EndInit();
            this.tabControls.ResumeLayout(false);
            this.tabPageNew.ResumeLayout(false);
            this.tabPageRecent.ResumeLayout(false);
            this.flowParentPanel.ResumeLayout(false);
            this.panel8.ResumeLayout(false);
            this.panel8.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox6)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.tabPageCloud.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.textBoxSearch)).EndInit();
            this.tabPageLocal.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Syncfusion.Windows.Forms.Tools.TabControlAdv tabControls;
        private Syncfusion.Windows.Forms.Tools.TabPageAdv tabPageRecent;
        private Syncfusion.Windows.Forms.Tools.TabPageAdv tabPageCloud;
        private Syncfusion.Windows.Forms.Tools.TabPageAdv tabPageLocal;
        private System.Windows.Forms.Button btnLocalOpen;
        private System.Windows.Forms.Panel panel1;
        private Syncfusion.Windows.Forms.Tools.TextBoxExt textBoxSearch;
        private Syncfusion.Windows.Forms.BannerTextProvider bannerTextProvider1;
        private Syncfusion.Reflection.TypeLoader designTimeTabTypeLoader;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.FlowLayoutPanel flowParentPanel;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.PictureBox pictureBox6;
        private System.Windows.Forms.Panel panel3;
        private Syncfusion.Windows.Forms.Tools.TabPageAdv tabPageNew;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutCloud;
        private Syncfusion.WinForms.Controls.SfButton sfButton1;
    }
}