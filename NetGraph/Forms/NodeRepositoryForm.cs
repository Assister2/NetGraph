using CefSharp;
using CefSharp.Enums;
using CyConex.API;
using CyConex.Chromium;
using CyConex.Graph;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Syncfusion.WinForms.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Runtime.Caching;
using CyConex.Helpers;

namespace CyConex.Forms
{
    public partial class NodeRepositoryForm : SfForm
    {
        public MainForm ribbonForm = null;
        public ImageList nodeImageList = new ImageList();
        Control selectedControl = null;
        private System.Windows.Forms.Panel newPanel;
        private System.Windows.Forms.Label newLabel;

        //private List<Node> repoNodeList = new List<Node>();
        //private JArray nodeCategory = new JArray();
        private ContextMenuStrip contextMenuStrip = new ContextMenuStrip();

        private CacheHelper _cacheHelper = new CacheHelper();
        private string _selectedCategory = "";
        public NodeRepositoryForm()
        {
            InitializeComponent();
        }

        public void LoadNodeRepositoryData()
        {
            Thread thread = new Thread(new ThreadStart(ThreadLoadNodeRepositoryData));
            thread.Start();
        }

        public void ThreadLoadNodeRepositoryData()
        {
            List<Node> repoNodeList = NodeRepository.LoadRepositoryList();
            _cacheHelper.Set(_cacheHelper.nodeRepoKey, repoNodeList);
            _cacheHelper.Set(_cacheHelper.nodeCategoryKey, LoadCategories());
            InitContextMenuStrip();
        }
        private void InitContextMenuStrip()
        {
            this.InvokeIfNeed(() =>
            { 
                contextMenuStrip.Items.Clear();
                JArray nodeCategory = _cacheHelper.Get(_cacheHelper.nodeCategoryKey) as JArray;
                for (int i = 0; i < nodeCategory.Count; i++)
                {
                    JObject obj = nodeCategory[i] as JObject;
                    string title = obj["title"].ToString();
                    JArray subs = obj["children"] as JArray;
                    ToolStripMenuItem tsmi = new ToolStripMenuItem();
                    tsmi.Text = title;
                    tsmi.Tag = title;
                    for (int j = 0; j < subs.Count; j++)
                    {
                        ToolStripMenuItem sub_item = new ToolStripMenuItem();
                        sub_item.Text = subs[j].ToString();
                        sub_item.Tag = title + "|" + subs[j].ToString();
                        sub_item.Click += new System.EventHandler(this.toolStripMenuItem_Click);
                        tsmi.DropDownItems.Add(sub_item);
                    }
                    tsmi.Click += new System.EventHandler(this.toolStripMenuItem_Click);
                    contextMenuStrip.Items.Add(tsmi);
                }
            });
        }

        private void NodeRepositoryForm_Load(object sender, EventArgs e)
        {
            InitNodeRepositoryPanel();
        }

        private void InitNodeRepositoryPanel()
        {
            this.panelNodeRepository.Dock = DockStyle.Fill;
        }

        private JArray LoadCategories()
        {
            List<Node> repoNodeList = _cacheHelper.Get(_cacheHelper.nodeRepoKey) as List<Node>;
            JArray nodeCategory = new JArray();
            for (int i = 0; i < repoNodeList.Count; i++)
            {
                Node node = repoNodeList[i];
                string category = node.Category;
                string sub_category = node.SubCategory;
                if (category != null && category != "")
                {
                    bool flag = true;
                    for (int j = 0; j < nodeCategory.Count; j++)
                    {
                        JObject obj = nodeCategory[j] as JObject;
                        bool sub_flag = true;
                        if (obj["title"].ToString().ToLower() == category.ToLower())
                        {
                            JArray childs = obj["children"] as JArray;
                            for (int k = 0;  k < childs.Count; k++)
                            {
                                string sub_obj = childs[k].ToString();
                                if (sub_obj.ToLower() == sub_category.ToLower())
                                {
                                    sub_flag = false;
                                    break;
                                }
                            }
                            if (sub_flag && sub_category != "")
                            {
                                childs.Add(sub_category);
                                obj["children"] = childs;
                                nodeCategory[j] = obj;
                            }
                            flag = false;
                            break;
                        }
                    }
                    if (flag)
                    {
                        JObject new_obj = new JObject();
                        new_obj["title"] = category;
                        JArray subs = new JArray();
                        if (sub_category != "")
                        {
                            subs.Add(sub_category);
                        }
                        new_obj["children"] = subs;
                        nodeCategory.Add(new_obj);
                    }
                }
            }
            return nodeCategory;
        }

        private void txtNodeSearch_KeyUp(object sender, KeyEventArgs e)
        {
            UpdateNodeListView(this.ribbonForm.NodeListViewItems);
        }

        public bool ListViewFilter(object obj)
        {
            if ((obj as Node).Title.ToLower().IndexOf(txtNodeSearch.Text.ToLower()) > -1)
            {
                return true;
            }

            return false;
        }

        public void UpdateNodeListView(List<Node> NodeListViewItems)
        {
            nodeItemsListView.Items.Clear();
            nodeImageList.Images.Clear();
            nodeImageList.ImageSize = new Size(48, 48);
            foreach (Node tmpItem in NodeListViewItems)
            {
                nodeImageList.Images.Add(tmpItem.ID, Utility.GetImageFromBase64(tmpItem.NodeImageData));
            }
            nodeItemsListView.LargeImageList = nodeImageList;
            nodeItemsListView.SmallImageList = nodeImageList;
            string keywords = txtNodeSearch.Text.ToLower();
            List<string> types = new List<string>();
            //for (int i = 0; i < nodeTypeListbox.SelectedItems.Count; i++)
            //{
            //    types.Add(nodeTypeListbox.SelectedItems[i].ToString().ToLower());
            //}

            foreach (Node tmpItem in NodeListViewItems)
            {
                string pCategory = tmpItem.PrimaryCategory;
                string sCategory = tmpItem.SubCategory;
                string c = sCategory == "" ? pCategory : pCategory + "(" + sCategory + ")";
                c = pCategory == null ? tmpItem.Type.Name : c;
                if (types.IndexOf(c.ToLower()) > -1 && tmpItem.Title.ToLower().IndexOf(keywords) > -1)
                {
                    nodeItemsListView.Items.Add(tmpItem.Title, tmpItem.ID);
                }
            }
        }

        private void LabelFloatEnterColour(object sender)
        {
            if (sender == null)
                return;
            if ((sender as Label) == selectedControl)
                return;
            (sender as Label).BackColor = Color.FromArgb(250, 250, 250);
            (sender as Label).Parent.BackColor = Color.FromArgb(250, 250, 250);
        }

        private void LabelFloatLeaveColour(object sender)
        {
            if (sender == null)
                return;
            if ((sender as Label) == selectedControl)
                return;
            (sender as Label).BackColor = Color.FromArgb(240, 240, 240);
            (sender as Label).Parent.BackColor = Color.FromArgb(240, 240, 240);
        }

        private void PanelFloatEnterColour(object sender)
        {
            if (sender == null)
                return;
            if ((sender as Panel) == selectedControl)
                return;
            (sender as Panel).BackColor = Color.FromArgb(250, 250, 250);
            foreach (Control child in (sender as Panel).Controls)
            {
                child.BackColor = Color.FromArgb(250, 250, 250);
            }
        }
        private void PanelFloatLeaveColour(object sender)
        {
            if (sender == null)
                return;
            if ((sender as Panel) == selectedControl)
                return;
            (sender as Panel).BackColor = Color.FromArgb(240, 240, 240);
            foreach (Control child in (sender as Panel).Controls)
            {
                child.BackColor = Color.FromArgb(240, 240, 240);
            }

        }

        private void SelectedColour(object sender)
        {
            (sender as Panel).BackColor = Color.FromArgb(255, 255, 255);
        }

        private void newPanel_MouseEnter(object sender, EventArgs e)
        {
            PanelFloatEnterColour(sender as Panel);
        }
        
        private void newPanel_MouseLeave(object sender, EventArgs e)
        {
            PanelFloatLeaveColour(sender as Panel);
        }
        
        private void label2_Click(object sender, EventArgs e)
        {
            SetChildBackgrounds(sender as Panel);
        }

        private void newPanel_Click(object sender, EventArgs e)
        {
            SetChildBackgrounds(sender as Panel);

        }

        private void SetRepoLabelText(Label targetLabel)
        {
            lblWhereAreTheNodes.Visible = false;
            lblRepoName.Text = targetLabel.Text;
        }

        private void SetChildBackgrounds(Control targetControl)
        {
            if (targetControl == null)
                return;

            selectedControl = targetControl;
            foreach (Control childControl in panelMenuItems.Controls)
            {
                if (childControl != targetControl)
                {
                    childControl.BackColor = Color.FromArgb(240, 240, 240);
                    foreach (Control childLabels in childControl.Controls)
                    {
                        if (childLabels is Label)
                        {
                            (childLabels as Label).Font = new Font(label1.Font, FontStyle.Regular);
                            (childLabels as Label).BackColor = Color.FromArgb(240, 240, 240);
                        }
                    }
                }
            }

            (targetControl as Panel).BackColor = Color.FromArgb(255, 255, 255);
            foreach (Control childLabels in targetControl.Controls)
            {
                if (childLabels is Label)
                {
                    (childLabels as Label).Font = new Font(label1.Font, FontStyle.Bold);
                    SetRepoLabelText(childLabels as Label);
                    InitNodeRepoData(childLabels.Text);
                }
            }
        }

        public void ClearPanels()
        {
            panelMenuItems.Controls.Clear();
            lblRepoName.Text = "";
            nodeItemsListView.Items.Clear();
            contextMenuStrip.Items.Clear();
        }
        private void InitNodeRepoData(string lbl)
        {
            _selectedCategory = lbl;
            nodeItemsListView.Items.Clear();
            nodeImageList.Images.Clear();
            nodeImageList.ImageSize = new Size(48, 48);
            int tmp_index = 0;
            List<Node> repoNodeList = _cacheHelper.Get(_cacheHelper.nodeRepoKey) as List<Node>;
            if (repoNodeList == null)
            {
                return;
            }
            for (int i = 0; i < repoNodeList.Count; i++)
            {
                Node item = repoNodeList[i];
                if (item.Category.ToLower() == lbl.ToLower() || item.SubCategory.ToLower() == lbl.ToLower())
                {
                    ListViewItem nodeItem = new ListViewItem();
                    nodeItem.Tag = item.ID;
                    nodeItem.Text = Utility.RemoveHTML(item.Title);
                    nodeItem.ImageIndex = tmp_index;
                    Image img;
                    string image_path = Utility.Base64Decode(item.ImagePath);
                    /*if (File.Exists(image_path))
                    {
                        using (FileStream stream = new FileStream(image_path, FileMode.Open, FileAccess.Read))
                        {
                            img = Image.FromStream(stream);
                            stream.Dispose();
                        }
                    }
                    else */if (item.NodeImageData != null && item.NodeImageData != "")
                    {
                        img = Utility.GetImageFromBase64(item.NodeImageData);
                    }
                    else
                    {
                        ImageData imgData = Utility.DefaultNodeImage(item.Type.Name);
                        img = imgData.Image;
                    }

                    nodeItemsListView.Items.Add(nodeItem);
                    nodeImageList.Images.Add(item.ID, img);

                    tmp_index++;
                }
            }
            nodeItemsListView.LargeImageList = nodeImageList;
            nodeItemsListView.SmallImageList = nodeImageList;
        }

        private void showPopUpMenu()
        {
            Point clickPos = new System.Drawing.Point(panelMoreNodes.Top, panelMoreNodes.Left);
            ShowMenuUnderControl();
        }

        private void panel1_Click(object sender, EventArgs e)
        {
            showPopUpMenu();
        }

        public void ShowMenuUnderControl()
        {
            contextMenuStrip.Show(panelMoreNodes, new Point(panelMoreNodes.Width + 5, 0));
        }

        private void panel7_MouseEnter(object sender, EventArgs e)
        {
            PanelFloatEnterColour(sender as Panel);
        }

        private void newLabel_MouseEnter(object sender, EventArgs e)
        {
            PanelFloatEnterColour(sender as Panel);
        }

        private void newLabel_MouseLeave(object sender, EventArgs e)
        {
            PanelFloatLeaveColour(sender as Panel);
        }

        public void AddNodeRepositoryPanel(string menuItemText, string menuItemTag)
        {
            for (int i = 0; i < this.panelMenuItems.Controls.Count; i++)
            {
                Panel tmp = this.panelMenuItems.Controls[i] as Panel;
                if (tmp.Tag != null)
                {
                    string tmp_tag = tmp.Tag.ToString().ToLower();
                    if (tmp_tag == menuItemTag.ToLower())
                    {
                        return;
                    }
                }
            }

            this.newLabel = new System.Windows.Forms.Label();
            this.newLabel.AutoSize = true;
            this.newLabel.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.newLabel.Location = new System.Drawing.Point(3, 3);
            this.newLabel.Text = menuItemText;
            this.newLabel.Click += new System.EventHandler(this.label2_Click);
            this.newLabel.MouseEnter += new System.EventHandler(this.label_MouseEnter);
            this.newLabel.MouseLeave += new System.EventHandler(this.label_MouseLeave);

            this.newPanel = new System.Windows.Forms.Panel();
            this.newPanel.ContextMenuStrip = this.contextMenuStrip2;
            this.newPanel.Controls.Add(this.newLabel);
            this.newPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.newPanel.Location = new System.Drawing.Point(0, 0);
            this.newPanel.Size = new System.Drawing.Size(274, 23);
            this.newPanel.Tag = menuItemTag;
            this.newPanel.Click += new System.EventHandler(this.newPanel_Click);
            this.newPanel.MouseEnter += new System.EventHandler(this.newPanel_MouseEnter);
            this.newPanel.MouseLeave += new System.EventHandler(this.newPanel_MouseLeave);

            this.panelMenuItems.Controls.Add(this.newPanel);
            SetChildBackgrounds(this.newPanel);
        }

        private void toolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem menuItem = (ToolStripMenuItem)sender;
            AddNodeRepositoryPanel(menuItem.Text, menuItem.Tag.ToString());
        }

        private void nodeItemsListView_DoubleClick(object sender, EventArgs e)
        {
            if (nodeItemsListView.SelectedItems.Count == 0) return;
            ListViewItem item = nodeItemsListView.SelectedItems[0];

            List<Node> repoNodeList = _cacheHelper.Get(_cacheHelper.nodeRepoKey) as List<Node>;
            if (item != null && item.Tag != null)
            {
                string tmp_id = item.Tag.ToString();
                Node node = new Node();
                for (int i = 0; i < repoNodeList.Count; i++)
                {
                    if (repoNodeList[i].ID == tmp_id)
                    {
                        node = repoNodeList[i];
                        ribbonForm.AddNodeFromRepository(node);
                        break;
                    }
                }
            }
        }

        public void AddNodeRepoItem(string nodeID, Node node, bool flag = true)
        {
            if (node.ID == "" || node.ID == null) node.ID = nodeID;
            bool tmp_flag = _cacheHelper.UpdateNodeRepo(node);
            if (tmp_flag && flag == true)
            {
                _cacheHelper.Set(_cacheHelper.nodeCategoryKey, LoadCategories());
                InitContextMenuStrip();
            }
        }

        public void RemoveNodeRepoItem(string nodeID, bool flag = true)
        {
            _cacheHelper.RemoveNodeRepoItem(nodeID);
            if (flag == true)
            {
                _cacheHelper.Set(_cacheHelper.nodeCategoryKey, LoadCategories());
                InitContextMenuStrip();
            }
        }

        private void btnRepoRefresh_Click(object sender, EventArgs e)
        {
            JArray tmpList = NodeAPI.GetRepoNodeList();
            List<Node> tmpRepoNodeList = new List<Node>();
            List<Node> repoNodeList = _cacheHelper.Get(_cacheHelper.nodeRepoKey) as List<Node>;
            for (int i = 0;i < tmpList.Count; i++)
            {
                JObject tmp = tmpList[i] as JObject;
                string nodeGUID = tmp["nodeGUID"].ToString();
                bool flag = false;
                for (int j = 0; j < repoNodeList.Count; j++)
                {
                    Node node = repoNodeList[j];
                    if (node.ID == nodeGUID)
                    {
                        flag = true;
                        tmpRepoNodeList.Add(node);
                        break;
                    }
                }
                if (!flag)
                {
                    Node node = new Node();
                    node = NodeRepository.GetNodeDataFromID(nodeGUID);
                    tmpRepoNodeList.Add((Node)node);
                }
            }
            _cacheHelper.Set(_cacheHelper.nodeRepoKey, tmpRepoNodeList);
            _cacheHelper.Set(_cacheHelper.nodeCategoryKey, LoadCategories());
            InitContextMenuStrip();

            InitNodeRepoData(_selectedCategory);
            /*if (nodeItemsListView.SelectedItems.Count > 0)
            {
                InitNodeRepoData(nodeItemsListView.SelectedItems[0].Text);
            }*/
        }

        private void label2_MouseEnter(object sender, EventArgs e)
        {
            LabelFloatEnterColour(sender);
        }

        private void label2_MouseLeave(object sender, EventArgs e)
        {
            LabelFloatLeaveColour(sender);
        }

        private void label_MouseEnter(object sender, EventArgs e)
        {
            LabelFloatEnterColour(sender);
        }

        private void label_MouseLeave(object sender, EventArgs e)
        {
            LabelFloatLeaveColour(sender);
        }

        private void label1_Click(object sender, EventArgs e)
        {
            showPopUpMenu();
        }
    }
}
        
