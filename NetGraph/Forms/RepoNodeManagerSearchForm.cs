using Syncfusion.WinForms.Controls;
using System;
using System.Drawing;
using System.Windows.Forms;
using CefSharp.WinForms;
using CyConex.Helpers;
using FuzzySharp;
using CyConex.Graph;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using CefSharp;
using CyConex.API;
using Newtonsoft.Json.Linq;
using System.Windows.Forms.VisualStyles;
using Syncfusion.Windows.Forms.Spreadsheet.Commands;
using System.Text.RegularExpressions;

namespace CyConex
{
    public partial class RepoNodeManagerSearchForm : SfForm
    {
        private ChromiumWebBrowser _browser;
        RepoNodeEditorForm nodeEditorForm;
        private MainForm _form1;

        private string[] _arrSearchIn = { "Title", "Reference", "Description", "Framework", "Notes" };
        private string[] _arrFilterBy = { "Actor", "Asset", "Attack", "Control", "Evidence", "Group", "Objective" };
        private JArray _searchResults = new JArray();

        public RepoNodeManagerSearchForm()
        {
            InitializeComponent();
            InitializeComboBox();
            this.Style.TitleBar.Height = 26;
        }
        private void InitializeComboBox()
        {

            cmbSearchIn.DisplayMember = "Name";
            for (int i = 0; i < _arrSearchIn.Length; i++)
            {
                CCBoxItem item = new CCBoxItem(_arrSearchIn[i], i);
                cmbSearchIn.Items.Add(item);
                cmbSearchIn.SetItemChecked(i, true);
            }
            cmbSearchIn.MaxDropDownItems = 5;

            cmbFilterBy.DisplayMember = "Name";
            for (int i = 0; i < _arrFilterBy.Length; i++)
            {
                CCBoxItem item = new CCBoxItem(_arrFilterBy[i], i);
                cmbFilterBy.Items.Add(item);
                cmbFilterBy.SetItemChecked(i, true);
            }
            cmbFilterBy.MaxDropDownItems = 6;
        }

        public void SetLocation(Point p)
        {
            this.Location = p;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        public void ShowDialogWithBrowser(CyConex.MainForm form1, ChromiumWebBrowser _br, Point p)
        {
            this.InvokeIfNeed(() =>
            {
                _form1 = form1;
                _browser = _br;
                ShowDialog();
            });
        }

        private Node getNodeByID(string id, string type)
        {
            Node node = new Node();
            if (_form1.NodeListViewItems != null && _form1.NodeListViewItems.Count > 0)
            {
                ArrayList array = new ArrayList();
                foreach (Node tmpNode in _form1.NodeListViewItems)
                {
                    if (id == tmpNode.ID)
                    {
                        node = type == "copy" ? tmpNode.Clone() : tmpNode;
                    }
                }
            }
            return node;
        }

        private void SearchItems()
        {
            List<string> checked_searchIn_items = new List<string>();
            foreach (CCBoxItem item in cmbSearchIn.CheckedItems)
            {
                checked_searchIn_items.Add(item.Name.ToLower());
            }
            List<string> checked_filterBy_items = new List<string>();
            foreach (CCBoxItem item in cmbFilterBy.CheckedItems)
            {
                checked_filterBy_items.Add(item.Name.ToLower());
            }
            string str = this.txtFindWhat.Text.ToLower();

            _searchResults.Clear();
            _searchResults = NodeAPI.GetRepoNodeList(
                string.Join(",", checked_searchIn_items.ToArray()),
                string.Join(",", checked_filterBy_items.ToArray()),
                str == "" ? "*" : str);

            //ArrayList array = new ArrayList();

            this.gridSearchResult.Rows.Clear();
            foreach (JObject obj in _searchResults)
            {
                JObject tmp = new JObject();
                string nodeTitle = Utility.RemoveHTML(obj["nodeTitle"].ToString());
                string nodeType = obj["nodeType"].ToString();
                string nodeDescription = Utility.RemoveHTML(obj["nodeDescription"].ToString());
                string nodeGUID = obj["nodeGUID"].ToString();
                this.gridSearchResult.Rows.Add(nodeTitle, nodeType, nodeDescription, nodeGUID);
                //array.Add(tmp);
            }
            //this.gridSearchResult.DataSource = array;
        }

        private void gridSearchResult_DoubleClick(object sender, EventArgs e)
        {
            if (this.gridSearchResult.SelectedRows.Count == 0) return;
            string nodeID = this.gridSearchResult.SelectedRows[0].Cells[3].Value.ToString();
            JObject obj = new JObject();
            for (int i = 0; i < _searchResults.Count; i++)
            {
                JObject tmp_obj = _searchResults[i] as JObject;
                if (tmp_obj["nodeGUID"].ToString() == nodeID)
                {
                    obj = tmp_obj;
                    break;
                }
            }
            _form1.addNodeQuick(obj["nodeType"].ToString(), obj["nodeType"].ToString().ToLower(), "Not Defined", "Not Defined", obj["nodeTitle"].ToString(), obj["nodeDescription"].ToString());

        }

        private void addNodeModal()
        {
            ShowEditorDialog(this, _browser, "", "new");
        }

        private void editNodeModal()
        {
            if (this.gridSearchResult.SelectedRows.Count == 0) return;
            string nodeID = this.gridSearchResult.SelectedRows[0].Cells[3].Value.ToString();
            ShowEditorDialog(this, _browser, nodeID, "edit");
        }

        private void addNodeToServer()
        {
            string guid = NodeAPI.AddNodeToServer(nodeEditorForm.node);
            if (guid != null)
            {
                _form1.nodeRepositoryForm.AddNodeRepoItem(guid, nodeEditorForm.node);
                NodeRepository.AddRepositoryData(nodeEditorForm.node);
                //_searchArray.Add(new NodeSearch(guid, nodeEditorForm.node.Title, nodeEditorForm.node.Type.Name, nodeEditorForm.node.ID));
            }
        }

        private void ShowEditorDialog(IWin32Window owner, ChromiumWebBrowser _br, string nodeID, string type)
        {
            JObject node_data = new JObject();
            if (nodeID != "") {
                //node_data = NodeAPI.GetRepoNodeJSON(nodeID);
                node_data = NodeAPI.GetNodeData(nodeID);
            }

            if (nodeEditorForm.ShowDialogWithNodeID(_form1, this, _browser, node_data, type) == DialogResult.OK)
            {
                //_form1.AddNodeListItem(nodeEditorForm.node, nodeEditorForm.IsNewNode, true);

                if (nodeEditorForm.IsNewNode)
                {
                    addNodeToServer();
                }
                else
                {
                    NodeRepository.UpdateRepositoryData(nodeEditorForm.node.ID, nodeEditorForm.node);
                }
                SearchItems();
            }
        }

        private void deleteNodeItem()
        {
            if (this.gridSearchResult.SelectedRows.Count == 0) return;
            string nodeID = this.gridSearchResult.SelectedRows[0].Cells[3].Value.ToString();

            try
            {
                if (NetGraphMessageBox.MessageBoxEx(this, $"Delete node?", "Delete node", MessageBoxButtons.YesNo, MessageBoxIconEx.Question, out bool dontShowAgainChecked, defaultButton: MessageBoxDefaultButton.Button2) != DialogResult.Yes)
                {
                    return;
                }
                if (NodeAPI.DeleteNodeMeta(nodeID) == null) return;
                gridSearchResult.Rows.RemoveAt(gridSearchResult.SelectedRows[0].Index);
                //_form1.deleteNodeWithID(nodeID); 
                NodeRepository.RemoveRepositoryDataWithID(nodeID, true);
                _form1.nodeRepositoryForm.RemoveNodeRepoItem(nodeID);
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Cannot delete Node : " + ex.ToString());
            }
        }

        private void copyNodeItem()
        {
            if (this.gridSearchResult.SelectedRows.Count == 0) return;
            string nodeID = this.gridSearchResult.SelectedRows[0].Cells[3].Value.ToString();
            ShowEditorDialog(this, _browser, nodeID, "copy");
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (nodeEditorForm == null)
                nodeEditorForm = new RepoNodeEditorForm(true);
            addNodeModal();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if  (nodeEditorForm == null)
                nodeEditorForm = new RepoNodeEditorForm(false);
            editNodeModal();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (nodeEditorForm == null)
                nodeEditorForm = new RepoNodeEditorForm(false);
            deleteNodeItem();
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            if (nodeEditorForm == null)
                nodeEditorForm = new RepoNodeEditorForm(false);
            copyNodeItem();
        }

        private void sfBtnFind_Click(object sender, EventArgs e)
        {
            SearchItems();
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
        }

    }
}
