using CefSharp;
using CefSharp.WinForms;
using FuzzySharp;
using CyConex.API;
using CyConex.Graph;
using CyConex.Helpers;
using Newtonsoft.Json.Linq;
using Syncfusion.WinForms.Controls;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace CyConex
{
    public partial class EdgeEditForm : SfForm
    {
        private ChromiumWebBrowser _browser;
        private string _node_id;
        private Node _sel_node = new Node();
        private JArray _linked_nodes = new JArray();
        private List<Node> _node_lists = new List<Node>();
        private JavascriptResponse _allNodes = null;
        private JavascriptResponse _allEdges = null;
        private List<Node> _nodes = new List<Node>();
        private List<Edge> _edges = new List<Edge>();
        private ArrayList favoriteArray = new ArrayList();
        private CyConex.RepoNodeEditorForm _nodeEditor;
        public EdgeEditForm()
        {
            InitializeComponent();

            //_settings = ApplicationSettings.Load();
            //string cyFileData = "";
            //if (File.Exists(@"Graph\cy.data"))
            //{
            //    StreamReader sr = new StreamReader(@"Graph\cy.data");
            //    cyFileData = sr.ReadToEnd();
            //    sr.Close();
            //}

            //JObject cyJsonFileData = cyFileData == "" ? null : JObject.Parse(cyFileData);
            //string edgeRelationData = cyJsonFileData["edge_relationship"].ToString();
            //JArray edgeRStrengthData = (JArray)cyJsonFileData["edge_relationship_strength"];
            //_settings.EdgeRelationshipStrengthData = edgeRStrengthData;

            //string[] lines = edgeRelationData.Split(',');
            //Array.Sort(lines);
            //for (int i = 0; i < lines.Length; i++)
            //{
            //    cmbEdgeRelationship.Items.Add(lines[i]);
            //}
            //cmbEdgeRelationship.SelectedIndex = 0;

            //for (int i = 0; i < edgeRStrengthData.Count; i++)
            //{
            //    string[] tmp = edgeRStrengthData[i].ToString().Split(',');
            //    cmbEdgeStrength.Items.Add(tmp[0]);
            //}
            //cmbEdgeStrength.SelectedItem = "Not Assessed";

            //_node_lists = Utility.LoadNodeList();
            
            buttonEnables();
        }

        private void buttonEnables()
        {
            if (tabContainer.SelectedIndex == 0)
            {
                btnAddEdgeTo.Enabled = false;
                btnAddEdgeFrom.Enabled = true;
            }
            else if (tabContainer.SelectedIndex == 1)
            {
                btnAddEdgeTo.Enabled = true;
                btnAddEdgeFrom.Enabled = false;
            }
            else
            {
                btnAddEdgeTo.Enabled = true;
                btnAddEdgeFrom.Enabled = true;
            }
        }

        public void ShowDialogWithBrowser(IWin32Window owner, ChromiumWebBrowser _br, Node node, string node_id, string title, string description )
        {
            _nodeEditor = owner as CyConex.RepoNodeEditorForm;
            this._node_id = node_id;
            this.InvokeIfNeed(() =>
            {
                _browser = _br;
                //initSourceNodes(node_id );
                _sel_node = node;
                lblNodeTitle.Text = title;
                lblNodeDescription.Text = description;
                InitSourceAndTargetGrid(node_id);
                AddSuggestedNodes();
                ShowDialog(owner);
            });
        }

        public async void initSourceNodes(string node_id )
        {

            _allNodes = await _browser.EvaluateScriptAsync("getNodes();");
            _allEdges = await _browser.EvaluateScriptAsync("getEdges();");
            if (_allNodes.Success)
            {
                var tmpObj = JArray.Parse(_allNodes.Result.ToString());
                foreach (var tmpNode in tmpObj)
                {
                    Node node = Node.FromJson(tmpNode.ToString());
                    _nodes.Add(node);
                    if (node.ID == _node_id)
                    {
                        _sel_node = node;
                    }
                }
            }
        }
        private void AddSuggestedNodes()
        {
            string id = _sel_node.ID;
            string title = _sel_node.Title;
            ArrayList array = new ArrayList();

            foreach (var tmpNode in NodeRepository.NodeRepositoryList)
            {
                string node_id = tmpNode.ID;
                if (node_id == id)
                {
                    continue;
                }

                string node_title = tmpNode.Title;
                string node_type = tmpNode.Type.Name;
                string final_desc = Utility.DecodeBase64TextandRemoveRTF(tmpNode.description);

                if (id == null || id == "")
                {
                    array.Add(new EdgeNode(node_id, node_title, node_type, final_desc));
                }
                else
                {
                    if (Fuzz.Ratio(node_title.ToLower(), title.ToLower()) > 70)
                    {
                        array.Add(new EdgeNode(node_id, node_title, node_type, final_desc));
                    }
                }
            }
            gridSuggestedNodes.DataSource = array;
        }
        public void InitSourceAndTargetGrid(string node_id)
        {
            ArrayList arr_connected_in = new ArrayList();
            ArrayList arr_connected_out = new ArrayList();
            if (_sel_node.ID != null && _sel_node.ID != "")
            {
                for (int i = 0; i < _linked_nodes.Count; i++)
                {
                    JObject obj = (JObject)_linked_nodes[i];
                    if (obj["targetNodeGUID"].ToString() == node_id)
                    {
                        Node tmp = Utility.GetNodeListItem(_node_lists, obj["sourceNodeGUID"].ToString());
                        arr_connected_in.Add(new EdgeNode(
                            obj["sourceNodeGUID"].ToString(),
                            tmp.Title,//obj["edge_title"].ToString(),
                            tmp.Type.Name,
                            Utility.DecodeBase64TextandRemoveRTF(tmp.description)// obj["edge_description"].ToString()
                        ));
                    }

                    if (obj["sourceNodeGUID"].ToString() == node_id)
                    {
                        Node tmp = Utility.GetNodeListItem(_node_lists, obj["targetNodeGUID"].ToString());
                        arr_connected_out.Add(new EdgeNode(
                            obj["targetNodeGUID"].ToString(),
                            tmp.Title,//obj["edge_title"].ToString(),
                            tmp.Type.Name,
                            Utility.DecodeBase64TextandRemoveRTF(tmp.description)// obj["edge_description"].ToString()
                        ));
                    }
                }
            }
            else
            {
                ArrayList in_ids = new ArrayList();
                ArrayList out_ids = new ArrayList();
                for (int i = 0; i < _linked_nodes.Count; i++)
                {
                    JObject obj = (JObject)_linked_nodes[i];
                    if (in_ids.IndexOf(obj["source_node_id"].ToString()) < 0)
                    {
                        Node tmp = Utility.GetNodeListItem(_node_lists, obj["sourceNodeGUID"].ToString());
                        in_ids.Add(obj["sourceNodeGUID"].ToString());
                        arr_connected_in.Add(new EdgeNode(
                            obj["sourceNodeGUID"].ToString(),
                            tmp.Title,//obj["edge_title"].ToString(),
                            tmp.Type.Name,
                            Utility.DecodeBase64TextandRemoveRTF(tmp.description)// obj["edge_description"].ToString()
                        ));
                    }
                    if (out_ids.IndexOf(obj["targetNodeGUID"].ToString()) < 0)
                    {
                        Node tmp = Utility.GetNodeListItem(_node_lists, obj["targetNodeGUID"].ToString());
                        out_ids.Add(obj["targetNodeGUID"].ToString());
                        arr_connected_out.Add(new EdgeNode(
                            obj["targetNodeGUID"].ToString(),
                            tmp.Title,// obj["edge_title"].ToString(),
                            tmp.Type.Name,
                            Utility.DecodeBase64TextandRemoveRTF(tmp.description)// obj["edge_description"].ToString()
                        ));
                    }
                }
            }

            gridSourceNodes.DataSource = arr_connected_in;
            gridTargetNodes.DataSource = arr_connected_out;
        }

        private void btnAddEdge_Click(object sender, EventArgs e)
        {
            AddEdge("to");
        }

        private void AddEdge(string flag = "")
        {
            var tab_sel_index = tabContainer.SelectedIndex;
            if (tab_sel_index == -1)
            {
                return;
            }

            DataGridViewRow sel_item = null;
            switch (tab_sel_index)
            {
                case 0:
                    sel_item = gridSourceNodes.SelectedRows[0];
                    break;
                case 1:
                    sel_item = gridTargetNodes.SelectedRows[0];
                    break;
                case 2:
                    sel_item = gridSuggestedNodes.SelectedRows[0];
                    break;
                case 3:
                    sel_item = gridAllNodes.SelectedRows[0];
                    break;
            }
            if (sel_item == null)
            {
                return;
            }

            string selected_node_id = sel_item.Cells[0].Value.ToString();

            string source = flag == "from" ? _node_id.ToString() : selected_node_id;
            string target = flag == "from" ? selected_node_id : _node_id.ToString();
            string title = txtEdgeTitle.Text.ToString();
            string desc = txtEdgeDescription.Text.ToString();
            string edge_rel = cmbEdgeRelationship.Text;
            string edge_strength = cmbEdgeStrength.Text;

            JObject obj = new JObject();
            obj["source_node_id"] = source;
            obj["target_node_id"] = target;
            obj["edge_title"] = title;
            obj["edge_description"] = desc;
            obj["edge_relationship"] = edge_rel;
            obj["edge_strength"] = edge_strength;
            obj["edge_strength_value"] = "1";

            _linked_nodes.Add(obj);
            //Utility.SaveLinkedNodes(_linked_nodes);
            EdgeAPI.PostRepoEdge(source, target, edge_rel, edge_strength, title, desc, "1");

            MessageBox.Show("Edge is created, successfuly");
            UpdateListAndButtons(tab_sel_index, flag, sel_item );
            InitSourceAndTargetGrid(_node_id);
        }

        private void UpdateListAndButtons(int tab_index, string flag, DataGridViewRow sel_item )
        {
            switch(tab_index)
            {
                case 0:
                    gridSourceNodes.Rows.Remove(sel_item);
                    break;
                case 1:
                    gridTargetNodes.Rows.Remove(sel_item);
                    break;
                case 2:
                case 3:
                    if (flag == "from")
                    {
                        btnAddEdgeFrom.Enabled = false;
                    }
                    else if (flag == "to")
                    {
                        btnAddEdgeTo.Enabled = false;
                    }
                    break;
            }
        }

        private void tabContainer_SelectedIndexChanged(object sender, EventArgs e)
        {
            buttonEnables();
        }

        private void btnAddEdgeFrom_Click(object sender, EventArgs e)
        {
            AddEdge("from");
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            Search();
        }

        private void Search()
        {

            string is_control = this.ckControl.Checked ? "control" : "";
            string is_group = this.ckGroup.Checked ? "group" : "";
            string is_objective = this.ckObjective.Checked ? "objective" : "";
            string is_asset = this.ckAsset.Checked ? "asset" : "";
            string is_attack = this.ckAttack.Checked ? "attack" : "";
            string is_actor = this.ckActor.Checked ? "actor" : "";
            string is_vulnerability = this.ckVulnerability.Checked ? "actor" : "";
            bool is_fuzzy = this.checkFuzzySearch.Checked;
            string str = this.textSearchContent.Text.ToLower();

            ArrayList array = new ArrayList();
            foreach (var tmpNode in NodeRepository.NodeRepositoryList)
            {
                string title = tmpNode.Title.ToLower();
                string description = Utility.DecodeBase64TextandRemoveRTF(tmpNode.description).ToLower();
                string type = tmpNode.Type.Name.ToLower();
                string id = tmpNode.ID.ToLower();   

                if ((is_control == "" && is_group == "" && is_objective == "" && is_asset == "") ||
                    (type == is_control || type == is_objective || type == is_group || type == is_asset || type == is_attack || type == is_actor || type == is_vulnerability))
                {
                    if ((System.Text.RegularExpressions.Regex.IsMatch(title.ToLower(), Utility.WildCardToRegular(str))) ||
                        (System.Text.RegularExpressions.Regex.IsMatch(description.ToLower(), Utility.WildCardToRegular(str))) ||
                        title.IndexOf(str) > -1 || description.ToLower().IndexOf(str) > -1 ||
                        Utility.isFuzzySearch(is_fuzzy, title, str) ||
                        Utility.isFuzzySearch(is_fuzzy, description.ToLower(), str))
                    {
                        Node tmp = NodeRepository.GetRepositoryList(id);
                        array.Add(new EdgeNode(id, title, tmp.Type.Name, description));
                    }
                }
            }
            this.gridAllNodes.DataSource = array;
        }

        private void btnAddFavorites_Click(object sender, EventArgs e)
        {
            addNodeToFavorite();
        }

        private void addNodeToFavorite()
        {
            var tab_sel_index = tabContainer.SelectedIndex;
            if (tab_sel_index == -1)
            {
                return;
            }

            string selected_node_id = "";
            string selected_title = "";
            string selected_desc = "";
            string selected_type = "";
            DataGridViewRow sel_item = null;
            switch (tab_sel_index)
            {
                case 0:
                    sel_item = gridSourceNodes.SelectedRows[0];
                    break;
                case 1:
                    sel_item = gridTargetNodes.SelectedRows[0];
                    break;
                case 2:
                    sel_item = gridSuggestedNodes.SelectedRows[0];
                    break;
                case 3:
                    sel_item = gridAllNodes.SelectedRows[0];
                    break;
            }

            selected_node_id = sel_item.Cells[0].Value.ToString();
            selected_title = sel_item.Cells[1].Value.ToString();
            selected_type = sel_item.Cells[2].Value.ToString();
            selected_desc = sel_item.Cells[3].Value.ToString();

            var arr = new ArrayList();
            if (selected_node_id != "")
            {
                Node tmp = Utility.GetNodeListItem(_node_lists, selected_node_id);
                favoriteArray.Add(new EdgeNode(selected_node_id, selected_title, tmp.Type.Name, selected_desc));
            }

            ArrayList ids = new ArrayList();
            for (int i = 0; i < favoriteArray.Count; i++)
            {
                if (ids.IndexOf(((EdgeNode)favoriteArray[i]).NodeID) == -1)
                {
                    arr.Add(favoriteArray[i]);
                    ids.Add(((EdgeNode)favoriteArray[i]).NodeID);
                }
            }
            gridFavoriteNodes.DataSource = arr;
            gridFavoriteNodes.Update();
            gridFavoriteNodes.Refresh();
        }

        private void btnRemoveEdge_Click(object sender, EventArgs e)
        {
            var tab_sel_index = tabContainer.SelectedIndex;
            if (tab_sel_index == -1)
            {
                return;
            }

            DataGridViewRow sel_item = null;
            switch (tab_sel_index)
            {
                case 0:
                    sel_item = gridSourceNodes.SelectedRows[0];
                    break;
                case 1:
                    sel_item = gridTargetNodes.SelectedRows[0];
                    break;
                case 2:
                    sel_item = gridSuggestedNodes.SelectedRows[0];
                    break;
                case 3:
                    sel_item = gridAllNodes.SelectedRows[0];
                    break;
            }

            string selected_node_id = sel_item.Cells[0].Value.ToString();

            string source = _node_id.ToString();
            string target = selected_node_id;

            for (int i = 0; i < _linked_nodes.Count; i++)
            {
                if ((source == _linked_nodes[i]["sourceNodeGUID"].ToString() && target == _linked_nodes[i]["targetNodeGUID"].ToString()) ||
                (target == _linked_nodes[i]["sourceNodeGUID"].ToString() && source == _linked_nodes[i]["targetNodeGUID"].ToString()))
                {
                    _linked_nodes.RemoveAt(i);
                    break;
                }
            }
            Utility.SaveLinkedNodes(_linked_nodes);
            InitSourceAndTargetGrid(_node_id);
        }

        private void EdgeEditForm_Load(object sender, EventArgs e)
        {

        }

        private void gridSourceNodes_SelectionChanged(object sender, EventArgs e)
        {

        }

        private void gridTargetNodes_SelectionChanged(object sender, EventArgs e)
        {

        }

        private void gridSuggestedNodes_SelectionChanged(object sender, EventArgs e)
        {
            UpdateButtonsState(gridSuggestedNodes.SelectedRows);
        }

        private void gridAllNodes_SelectionChanged(object sender, EventArgs e)
        {
            UpdateButtonsState(gridAllNodes.SelectedRows);
        }

        private void gridFavoriteNodes_SelectionChanged(object sender, EventArgs e)
        {
            UpdateButtonsState(gridFavoriteNodes.SelectedRows);
        }

        private void UpdateButtonsState(DataGridViewSelectedRowCollection selectedRows)
        {
            if (selectedRows != null && selectedRows.Count > 0  )
            {
                UpdateButtonsState(selectedRows[0]);
            }
        }

        private void UpdateButtonsState(DataGridViewRow item)
        {
            if (item == null)
            {
                return;
            }
            // check source and target node
            int flag = Utility.CheckSourceAndTargetNode(_linked_nodes, _node_id, item.Cells[0].Value.ToString());
            if (flag == 0)
            {
                btnAddEdgeFrom.Enabled = true;
                btnAddEdgeTo.Enabled = true;
            }else if(flag == 1)
            {
                btnAddEdgeFrom.Enabled = false;
                btnAddEdgeTo.Enabled = true;
            }else if(flag == 2)
            {
                btnAddEdgeFrom.Enabled = true;
                btnAddEdgeTo.Enabled= false;
            }else if(flag == 3)
            {
                btnAddEdgeFrom.Enabled = false;
                btnAddEdgeTo.Enabled = false;
            }
        }

    }
}
