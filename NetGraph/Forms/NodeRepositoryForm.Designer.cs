namespace CyConex.Forms
{
    partial class NodeRepositoryForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NodeRepositoryForm));
            this.panelNodeRepository = new System.Windows.Forms.Panel();
            this.lblWhereAreTheNodes = new System.Windows.Forms.Label();
            this.nodeItemsListView = new System.Windows.Forms.ListView();
            this.panel3 = new System.Windows.Forms.Panel();
            this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem7 = new System.Windows.Forms.ToolStripMenuItem();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnRepoRefresh = new System.Windows.Forms.Button();
            this.lblRepoName = new System.Windows.Forms.Label();
            this.panel8 = new System.Windows.Forms.Panel();
            this.panelMenuItems = new System.Windows.Forms.Panel();
            this.panelMoreNodes = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.panelSearch = new System.Windows.Forms.Panel();
            this.txtNodeSearch = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.subItemAToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.subItemBToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.subItemCToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem6 = new System.Windows.Forms.ToolStripMenuItem();
            this.panelNodeRepository.SuspendLayout();
            this.panel3.SuspendLayout();
            this.contextMenuStrip2.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panelMoreNodes.SuspendLayout();
            this.panelSearch.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelNodeRepository
            // 
            this.panelNodeRepository.BackColor = System.Drawing.SystemColors.Window;
            this.panelNodeRepository.Controls.Add(this.lblWhereAreTheNodes);
            this.panelNodeRepository.Controls.Add(this.nodeItemsListView);
            this.panelNodeRepository.Controls.Add(this.panel3);
            this.panelNodeRepository.Controls.Add(this.panel8);
            this.panelNodeRepository.Controls.Add(this.panelMenuItems);
            this.panelNodeRepository.Controls.Add(this.panelMoreNodes);
            this.panelNodeRepository.Controls.Add(this.panelSearch);
            this.panelNodeRepository.Location = new System.Drawing.Point(169, 19);
            this.panelNodeRepository.Name = "panelNodeRepository";
            this.panelNodeRepository.Size = new System.Drawing.Size(274, 495);
            this.panelNodeRepository.TabIndex = 9;
            this.panelNodeRepository.Visible = false;
            // 
            // lblWhereAreTheNodes
            // 
            this.lblWhereAreTheNodes.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.lblWhereAreTheNodes.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblWhereAreTheNodes.ForeColor = System.Drawing.Color.DimGray;
            this.lblWhereAreTheNodes.Location = new System.Drawing.Point(0, 211);
            this.lblWhereAreTheNodes.Name = "lblWhereAreTheNodes";
            this.lblWhereAreTheNodes.Size = new System.Drawing.Size(274, 97);
            this.lblWhereAreTheNodes.TabIndex = 14;
            this.lblWhereAreTheNodes.Text = "\r\n\r\n\r\nWhere are all the nodes?\r\n\r\nClick the More Nodes menu above to browse avail" +
    "able node catagories.";
            this.lblWhereAreTheNodes.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // nodeItemsListView
            // 
            this.nodeItemsListView.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.nodeItemsListView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.nodeItemsListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.nodeItemsListView.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.nodeItemsListView.FullRowSelect = true;
            this.nodeItemsListView.GridLines = true;
            this.nodeItemsListView.HideSelection = false;
            this.nodeItemsListView.Location = new System.Drawing.Point(0, 211);
            this.nodeItemsListView.Margin = new System.Windows.Forms.Padding(11);
            this.nodeItemsListView.MultiSelect = false;
            this.nodeItemsListView.Name = "nodeItemsListView";
            this.nodeItemsListView.Size = new System.Drawing.Size(274, 284);
            this.nodeItemsListView.TabIndex = 13;
            this.nodeItemsListView.UseCompatibleStateImageBehavior = false;
            this.nodeItemsListView.DoubleClick += new System.EventHandler(this.nodeItemsListView_DoubleClick);
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.panel3.ContextMenuStrip = this.contextMenuStrip2;
            this.panel3.Controls.Add(this.panel2);
            this.panel3.Controls.Add(this.lblRepoName);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 175);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(274, 36);
            this.panel3.TabIndex = 12;
            // 
            // contextMenuStrip2
            // 
            this.contextMenuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem7});
            this.contextMenuStrip2.Name = "contextMenuStrip1";
            this.contextMenuStrip2.ShowImageMargin = false;
            this.contextMenuStrip2.Size = new System.Drawing.Size(93, 26);
            // 
            // toolStripMenuItem7
            // 
            this.toolStripMenuItem7.Name = "toolStripMenuItem7";
            this.toolStripMenuItem7.Size = new System.Drawing.Size(92, 22);
            this.toolStripMenuItem7.Text = "Remove";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnRepoRefresh);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel2.Location = new System.Drawing.Point(244, 0);
            this.panel2.Name = "panel2";
            this.panel2.Padding = new System.Windows.Forms.Padding(0, 5, 5, 0);
            this.panel2.Size = new System.Drawing.Size(30, 36);
            this.panel2.TabIndex = 2;
            // 
            // btnRepoRefresh
            // 
            this.btnRepoRefresh.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnRepoRefresh.Image = ((System.Drawing.Image)(resources.GetObject("btnRepoRefresh.Image")));
            this.btnRepoRefresh.Location = new System.Drawing.Point(0, 5);
            this.btnRepoRefresh.Name = "btnRepoRefresh";
            this.btnRepoRefresh.Size = new System.Drawing.Size(25, 25);
            this.btnRepoRefresh.TabIndex = 1;
            this.btnRepoRefresh.UseVisualStyleBackColor = true;
            this.btnRepoRefresh.Click += new System.EventHandler(this.btnRepoRefresh_Click);
            // 
            // lblRepoName
            // 
            this.lblRepoName.Font = new System.Drawing.Font("Segoe UI Semilight", 16F);
            this.lblRepoName.Location = new System.Drawing.Point(3, 3);
            this.lblRepoName.Name = "lblRepoName";
            this.lblRepoName.Size = new System.Drawing.Size(241, 33);
            this.lblRepoName.TabIndex = 0;
            // 
            // panel8
            // 
            this.panel8.BackColor = System.Drawing.Color.Silver;
            this.panel8.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel8.Location = new System.Drawing.Point(0, 173);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(274, 2);
            this.panel8.TabIndex = 11;
            // 
            // panelMenuItems
            // 
            this.panelMenuItems.AutoScroll = true;
            this.panelMenuItems.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.panelMenuItems.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelMenuItems.Location = new System.Drawing.Point(0, 58);
            this.panelMenuItems.MaximumSize = new System.Drawing.Size(0, 115);
            this.panelMenuItems.MinimumSize = new System.Drawing.Size(0, 30);
            this.panelMenuItems.Name = "panelMenuItems";
            this.panelMenuItems.Size = new System.Drawing.Size(274, 115);
            this.panelMenuItems.TabIndex = 10;
            // 
            // panelMoreNodes
            // 
            this.panelMoreNodes.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.panelMoreNodes.Controls.Add(this.label1);
            this.panelMoreNodes.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelMoreNodes.Location = new System.Drawing.Point(0, 35);
            this.panelMoreNodes.Name = "panelMoreNodes";
            this.panelMoreNodes.Size = new System.Drawing.Size(274, 23);
            this.panelMoreNodes.TabIndex = 8;
            this.panelMoreNodes.Click += new System.EventHandler(this.panel1_Click);
            this.panelMoreNodes.MouseEnter += new System.EventHandler(this.newPanel_MouseEnter);
            this.panelMoreNodes.MouseLeave += new System.EventHandler(this.newPanel_MouseLeave);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.label1.Location = new System.Drawing.Point(3, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(95, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "More Nodes  ›";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            this.label1.MouseEnter += new System.EventHandler(this.label_MouseEnter);
            this.label1.MouseLeave += new System.EventHandler(this.label_MouseLeave);
            // 
            // panelSearch
            // 
            this.panelSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.panelSearch.Controls.Add(this.txtNodeSearch);
            this.panelSearch.Controls.Add(this.panel1);
            this.panelSearch.Controls.Add(this.button1);
            this.panelSearch.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelSearch.Location = new System.Drawing.Point(0, 0);
            this.panelSearch.Name = "panelSearch";
            this.panelSearch.Padding = new System.Windows.Forms.Padding(5);
            this.panelSearch.Size = new System.Drawing.Size(274, 35);
            this.panelSearch.TabIndex = 5;
            // 
            // txtNodeSearch
            // 
            this.txtNodeSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNodeSearch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtNodeSearch.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.txtNodeSearch.Location = new System.Drawing.Point(5, 5);
            this.txtNodeSearch.Name = "txtNodeSearch";
            this.txtNodeSearch.Size = new System.Drawing.Size(234, 25);
            this.txtNodeSearch.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(239, 5);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(5, 25);
            this.panel1.TabIndex = 2;
            // 
            // button1
            // 
            this.button1.Dock = System.Windows.Forms.DockStyle.Right;
            this.button1.Image = ((System.Drawing.Image)(resources.GetObject("button1.Image")));
            this.button1.Location = new System.Drawing.Point(244, 5);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(25, 25);
            this.button1.TabIndex = 0;
            this.button1.UseVisualStyleBackColor = true;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.toolStripMenuItem2,
            this.toolStripMenuItem3,
            this.toolStripMenuItem4,
            this.toolStripMenuItem5,
            this.toolStripMenuItem6});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.ShowImageMargin = false;
            this.contextMenuStrip1.Size = new System.Drawing.Size(107, 136);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.subItemAToolStripMenuItem,
            this.subItemBToolStripMenuItem,
            this.subItemCToolStripMenuItem});
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(106, 22);
            this.toolStripMenuItem1.Tag = "Catagory 1";
            this.toolStripMenuItem1.Text = "Catagory 1";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.toolStripMenuItem_Click);
            // 
            // subItemAToolStripMenuItem
            // 
            this.subItemAToolStripMenuItem.Name = "subItemAToolStripMenuItem";
            this.subItemAToolStripMenuItem.Size = new System.Drawing.Size(132, 22);
            this.subItemAToolStripMenuItem.Tag = "Catagory 1|Sub Item A";
            this.subItemAToolStripMenuItem.Text = "Sub Item A";
            this.subItemAToolStripMenuItem.Click += new System.EventHandler(this.toolStripMenuItem_Click);
            // 
            // subItemBToolStripMenuItem
            // 
            this.subItemBToolStripMenuItem.Name = "subItemBToolStripMenuItem";
            this.subItemBToolStripMenuItem.Size = new System.Drawing.Size(132, 22);
            this.subItemBToolStripMenuItem.Tag = "Catagory 1|Sub Item B";
            this.subItemBToolStripMenuItem.Text = "Sub Item B";
            this.subItemBToolStripMenuItem.Click += new System.EventHandler(this.toolStripMenuItem_Click);
            // 
            // subItemCToolStripMenuItem
            // 
            this.subItemCToolStripMenuItem.Name = "subItemCToolStripMenuItem";
            this.subItemCToolStripMenuItem.Size = new System.Drawing.Size(132, 22);
            this.subItemCToolStripMenuItem.Tag = "Catagory 1|Sub Item C";
            this.subItemCToolStripMenuItem.Text = "Sub Item C";
            this.subItemCToolStripMenuItem.Click += new System.EventHandler(this.toolStripMenuItem_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(106, 22);
            this.toolStripMenuItem2.Tag = "Catagory 2";
            this.toolStripMenuItem2.Text = "Catagory 2";
            this.toolStripMenuItem2.Click += new System.EventHandler(this.toolStripMenuItem_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(106, 22);
            this.toolStripMenuItem3.Tag = "Catagory 3";
            this.toolStripMenuItem3.Text = "Catagory 3";
            this.toolStripMenuItem3.Click += new System.EventHandler(this.toolStripMenuItem_Click);
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(106, 22);
            this.toolStripMenuItem4.Tag = "Catagory 4";
            this.toolStripMenuItem4.Text = "Catagory 4";
            this.toolStripMenuItem4.Click += new System.EventHandler(this.toolStripMenuItem_Click);
            // 
            // toolStripMenuItem5
            // 
            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
            this.toolStripMenuItem5.Size = new System.Drawing.Size(106, 22);
            this.toolStripMenuItem5.Tag = "Catagory 5";
            this.toolStripMenuItem5.Text = "Catagory 5";
            this.toolStripMenuItem5.Click += new System.EventHandler(this.toolStripMenuItem_Click);
            // 
            // toolStripMenuItem6
            // 
            this.toolStripMenuItem6.Name = "toolStripMenuItem6";
            this.toolStripMenuItem6.Size = new System.Drawing.Size(106, 22);
            this.toolStripMenuItem6.Tag = "Catagory 1";
            this.toolStripMenuItem6.Text = "Catagory 6";
            this.toolStripMenuItem6.Click += new System.EventHandler(this.toolStripMenuItem_Click);
            // 
            // NodeRepositoryForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(613, 559);
            this.Controls.Add(this.panelNodeRepository);
            this.Name = "NodeRepositoryForm";
            this.Style.MdiChild.IconHorizontalAlignment = System.Windows.Forms.HorizontalAlignment.Center;
            this.Style.MdiChild.IconVerticalAlignment = System.Windows.Forms.VisualStyles.VerticalAlignment.Center;
            this.Text = "NodeRepositoryForm";
            this.panelNodeRepository.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.contextMenuStrip2.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panelMoreNodes.ResumeLayout(false);
            this.panelMoreNodes.PerformLayout();
            this.panelSearch.ResumeLayout(false);
            this.panelSearch.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Panel panelNodeRepository;
        private System.Windows.Forms.Panel panelMenuItems;
        private System.Windows.Forms.Panel panelSearch;
        private System.Windows.Forms.Panel panelMoreNodes;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem4;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem5;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem6;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.TextBox txtNodeSearch;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ToolStripMenuItem subItemAToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem subItemBToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem subItemCToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip2;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem7;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label lblRepoName;
        private System.Windows.Forms.Button btnRepoRefresh;
        public System.Windows.Forms.ListView nodeItemsListView;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label lblWhereAreTheNodes;
    }
}