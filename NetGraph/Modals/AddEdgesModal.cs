using CefSharp;
using CefSharp.WinForms;
using CyConex.Helpers;
using Newtonsoft.Json.Linq;
using Syncfusion.WinForms.Controls;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using CyConex.Graph;
using CyConex.Chromium;
using System.Collections;
using System.IO;
using FuzzySharp;

namespace CyConex
{
    public partial class AddEdgesModal : SfForm
    {
        private ChromiumWebBrowser _browser;
        private List<Node> _nodes = new List<Node>();
        private List<Edge> _edges = new List<Edge>();
        private ApplicationSettings _settings;
        private JavascriptResponse _allNodes = null;
        private JavascriptResponse _allEdges = null;
        private ArrayList favoriteArray = new ArrayList();
        private ArrayList _edge_points = new ArrayList();
        public AddEdgesModal()
        {
            InitializeComponent();

            _settings = ApplicationSettings.Load();

            string cyFileData = "";
            if (File.Exists(@"Graph\cy.data"))
            {
                StreamReader sr = new StreamReader(@"Graph\cy.data");
                cyFileData = sr.ReadToEnd();
                sr.Close();
            }

            JObject cyJsonFileData = cyFileData == "" ? null : JObject.Parse(cyFileData);
            string edgeRelationData = cyJsonFileData["edge_relationship"].ToString();
            JArray edgeRStrengthData = (JArray)cyJsonFileData["edge_relationship_strength"];
            //_settings.EdgeRelationshipStrengthData = edgeRStrengthData;

            string[] lines = edgeRelationData.Split(',');
            Array.Sort(lines);
            for (int i = 0; i < lines.Length; i++)
            {
                cmbEdgeRelationship.Items.Add(lines[i]);
            }
            cmbEdgeRelationship.SelectedIndex = 0;

            for (int i = 0; i < edgeRStrengthData.Count; i++)
            {
                string[] tmp = edgeRStrengthData[i].ToString().Split(',');
                cmbEdgeStrength.Items.Add(tmp[0]);
            }
            cmbEdgeStrength.SelectedItem = "Not Assessed";
        }

        public void ShowDialogWithBrowser(IWin32Window owner, ChromiumWebBrowser _br, Point p)
        {
            this.InvokeIfNeed(() =>
            {
                _browser = _br;
                SetLocation(p);
                initSourceNodes();
                Show(owner);
            });
        }

        public void SetLocation(Point p)
        {
            this.Location = p;
        }

        public async void initSourceNodes()
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
                }

                updateSourceNodeList();
            }

            if (_allEdges.Success)
            {
                var tmpObj = JArray.Parse(_allEdges.Result.ToString());
                foreach (var tmpEdge in tmpObj)
                {
                    var tmpJson = JObject.Parse(tmpEdge.ToString());
                    string s_id = tmpJson["source"].ToString();
                    string t_id = tmpJson["target"].ToString();
                    _edge_points.Add(s_id + t_id);
                }
            }
        }

        public void updateSourceNodeList()
        {
            bool is_control = this.ckSourceControl.Checked;
            bool is_objective = this.ckSourceObjective.Checked;
            bool is_group = this.ckSourceGroup.Checked;
            bool is_asset = this.ckSourceAsset.Checked;
            bool is_attack = this.ckSourceAttack.Checked;
            bool is_actor = this.ckSourceActor.Checked;
            bool is_vulnerability = this.ckSourceVulnerability.Checked;
            ArrayList array = new ArrayList();

            foreach (var node in _nodes)
            {
                if (node.Type == null)
                {
                    continue;
                }

                if ((is_control && node.Type.ToString().ToLower() == "control") ||
                    (is_objective && node.Type.ToString().ToLower() == "objective") ||
                    (is_group && node.Type.ToString().ToLower() == "group") ||
                    (is_attack && node.Type.ToString().ToLower() == "attack") ||
                    (is_asset && node.Type.ToString().ToLower() == "asset"))
                {
                    string title = node.Title;
                    var from_desc = System.Convert.FromBase64String(node.description);
                    string final_desc = System.Text.Encoding.UTF8.GetString(from_desc);
                    array.Add(new EdgeNode(node.ID, title, node.Type.Name, Utility.RemoveRTFFormatting(final_desc)));
                }
            }
            this.gridSourceNodes.DataSource = array;
        }

        private void ckSourceObjective_CheckedChanged(object sender, EventArgs e)
        {
            updateSourceNodeList();
        }

        private void ckSourceControl_CheckedChanged(object sender, EventArgs e)
        {
            updateSourceNodeList();
        }

        private void ckSourceGroup_CheckedChanged(object sender, EventArgs e)
        {
            updateSourceNodeList();
        }

        private void ckSourceAsset_CheckedChanged(object sender, EventArgs e)
        {
            updateSourceNodeList();
        }

        private void gridSourceNodes_SelectionChanged(object sender, EventArgs e)
        {
            if (gridSourceNodes.SelectedRows.Count == 0)
            {
                return;
            }

            var sourceNode = gridSourceNodes.SelectedRows[0];

            string id = sourceNode.Cells[0].Value.ToString();
            string title = sourceNode.Cells[1].Value.ToString();
            string content = sourceNode.Cells[2].Value.ToString();

            var tmpObj = JArray.Parse(_allNodes.Result.ToString());
            ArrayList array = new ArrayList();
            ArrayList arr_connected = new ArrayList();

            foreach (var tmpNode in tmpObj)
            {
                Node node = Node.FromJson(tmpNode.ToString());
                string node_id = node.ID.ToString();
                if (node_id == id)
                {
                    continue;
                }

                string node_title = node.Title;
                var node_desc = System.Convert.FromBase64String(node.description);
                string final_desc = System.Text.Encoding.UTF8.GetString(node_desc);

                if (((Fuzz.Ratio(node_title.ToLower(), title.ToLower()) > 70) || 
                    (Fuzz.Ratio(final_desc.ToLower(), content.ToLower()) >70)) )
                {
                    array.Add(new EdgeNode(node.ID, node_title, node.Type.Name, Utility.RemoveRTFFormatting(final_desc)));
                }

                int edge_flag = existsEdge(id, node_id);
                if (edge_flag != 0)
                {
                    arr_connected.Add(new EdgeNode(node.ID,node_title, node.Type.Name, Utility.RemoveRTFFormatting(final_desc)));
                }
            }
            gridTargetNodes.DataSource = array;
            gridConnectedNodes.DataSource = arr_connected;
            updateGridTargetStyle();
        }

        private async void updateGridTargetStyle()
        {
            _allEdges = await _browser.EvaluateScriptAsync("getEdges();");

            if (gridTargetNodes.Rows.Count == 0)
            {
                return;
            }

            var sourceNode = gridSourceNodes.SelectedRows[0];
            var targetNodes = gridTargetNodes.Rows;
            string source_node_id = sourceNode.Cells[0].Value.ToString();
            int index = 0;
            foreach (DataGridViewRow row in targetNodes)
            {
                string target_node_id = row.Cells[0].Value.ToString();
                int is_exists = existsEdge(source_node_id, target_node_id);
                if (is_exists != 0)
                {
                    gridTargetNodes.Rows[index].DefaultCellStyle.ForeColor = Color.Red;
                    gridTargetNodes.Rows[index].DefaultCellStyle.SelectionForeColor = Color.Red;
                }
                else
                {
                    gridTargetNodes.Rows[index].DefaultCellStyle.ForeColor = Color.Black;
                    gridTargetNodes.Rows[index].DefaultCellStyle.SelectionForeColor = Color.Black;
                }
                index++;
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            this.Hide();
        }

        private async void btnAddEdge_Click(object sender, EventArgs e)
        {
            if (gridSourceNodes.SelectedRows.Count == 0 || gridTargetNodes.SelectedRows.Count == 0 )
            {
                return;
            }

            var sourceNode = gridSourceNodes.SelectedRows[0];
            var targetNodes = gridTargetNodes.SelectedRows;
            string source_node_id = sourceNode.Cells[0].Value.ToString();
            string relationship = cmbEdgeRelationship.SelectedText;
            string strength = cmbEdgeStrength.SelectedText;
            string title = txtEdgeTitle.Text;
            string description = txtEdgeDescription.Text;

            foreach(DataGridViewRow row in targetNodes)
            {
                string target_node_id = row.Cells[0].Value.ToString();
                int is_exists = existsEdge(source_node_id, target_node_id);
                if (is_exists != 0 )
                {
                    _browser.ExecScriptAsync($"addEdge('{source_node_id}','{target_node_id}',1,'{relationship}'" +
                        $",'{strength}','{title}','{description}');");
                }
            }
            updateGridTargetStyle();
            _allEdges = await _browser.EvaluateScriptAsync("getEdges();");
        }

        private int existsEdge(string source_node_id, string target_node_id)
        {
            int flag = 0;
            if (_allEdges.Success)
            {
                flag = _edge_points.IndexOf((source_node_id + target_node_id).ToString()) >= 0 ? 1 : 0;
                if (flag == 0)
                {
                    flag = _edge_points.IndexOf((target_node_id + source_node_id).ToString()) >= 0 ? 2 : 0;
                }
            }
            return flag;
        }

        private JObject getEdge(string source_node_id, string target_node_id)
        {
            JObject edge = new JObject();
            if (_allEdges.Success)
            {
                var tmpObj = JArray.Parse(_allEdges.Result.ToString());
                foreach (JObject tmpEdge in tmpObj)
                {
                    var tmpJson = JObject.Parse(tmpEdge.ToString());
                    string s_id = tmpJson["source"].ToString();
                    string t_id = tmpJson["target"].ToString();
                    if (s_id == source_node_id && t_id == target_node_id)
                    {
                        edge = tmpEdge;
                        break;
                    }
                }
            }
            return edge;
        }

        private async void btnRemoveEdge_Click(object sender, EventArgs e)
        {
            var sourceNode = gridSourceNodes.SelectedRows[0];
            var targetNodes = gridTargetNodes.SelectedRows;
            string source_node_id = sourceNode.Cells[0].Value.ToString();

            foreach (DataGridViewRow row in targetNodes)
            {
                string target_node_id = row.Cells[0].Value.ToString();
                int is_exists = existsEdge(source_node_id, target_node_id);
                if (is_exists != 0 )
                {
                    string edge_id = getEdgeID(source_node_id, target_node_id);
                    _browser.ExecScriptAsync($"deleteEdgeWithID('{edge_id}');");
                }
            }
            _allEdges = await _browser.EvaluateScriptAsync("getEdges();");
        }

        private string getEdgeID(string source_node_id, string target_node_id)
        {
            string edge_id = "";
            
            if (_allEdges.Success)
            {
                var tmpObj = JArray.Parse(_allEdges.Result.ToString());
                foreach (var tmpEdge in tmpObj)
                {
                    var tmpJson = JObject.Parse(tmpEdge.ToString());
                    string s_id = tmpJson["source"].ToString();
                    string t_id = tmpJson["target"].ToString();
                    if (s_id == source_node_id && t_id == target_node_id)
                    {
                        edge_id = tmpJson["id"].ToString();
                        break;
                    }
                }
            }
            return edge_id;
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
            string is_attack = this.ckAsset.Checked ? "attack" : "";
            string is_actor = this.ckSourceActor.Checked ? "actor" : "";
            string is_vulnerability = this.ckSourceVulnerability.Checked ? "actor" : "";
            bool is_fuzzy = this.checkFuzzySearch.Checked;
            string str = this.textSearchContent.Text.ToLower();

            if (_allNodes.Success)
            {
                var tmpObj = JArray.Parse(_allNodes.Result.ToString()); 
                ArrayList array = new ArrayList();
                foreach (var tmpNode in tmpObj)
                {
                    string title = tmpNode["title"].ToString().ToLower();
                    string description = tmpNode["description"].ToString();
                    string type = tmpNode["nodeType"].ToString().ToLower();
                    string id = tmpNode["id"].ToString(); 

                    var from_description = System.Convert.FromBase64String(description);
                    string final_description = System.Text.Encoding.UTF8.GetString((from_description));

                    if ((is_control == "" && is_group == "" && is_objective == "" && is_asset == "") || 
                        (type == is_control || type == is_objective || type == is_group || type == is_asset || type == is_attack || type == is_actor || type == is_vulnerability))
                    {
                        if ((System.Text.RegularExpressions.Regex.IsMatch(title.ToLower(), Utility.WildCardToRegular(str))) ||
                            (System.Text.RegularExpressions.Regex.IsMatch(final_description.ToLower(), Utility.WildCardToRegular(str))) ||
                            title.IndexOf(str) > -1 || final_description.ToLower().IndexOf(str) > -1 ||
                            Utility.isFuzzySearch(is_fuzzy, title, str) ||
                            Utility.isFuzzySearch(is_fuzzy, final_description.ToLower(), str)){

                            var sourceNode = gridSourceNodes.SelectedRows[0];
                            string source_id = sourceNode.Cells[0].Value.ToString();
                            if (source_id != id)
                            {
                                array.Add(new EdgeNode(id, title, type, Utility.RemoveRTFFormatting(final_description)));
                            }
                            
                        }
                    } 
                }
                this.gridAllNodes.DataSource = array;
            }
            else
            {
                return;
            }
        }

        private void bntAddNodeToFavorite_Click(object sender, EventArgs e)
        {
            addNodeToFavorite();
        }

        private async void addNodeToFavorite()
        {
            var tab_sel_index = tabControlAdv1.SelectedIndex;
            var selected_node_id = "";
            if (tab_sel_index == 1)         // Suggested Nodes Tab
            {
                var sourceNode = gridTargetNodes.SelectedRows[0];
                selected_node_id = sourceNode.Cells[0].Value.ToString();
            }
            else if(tab_sel_index == 2)     // All Nodes Tab
            {
                var sourceNode = gridAllNodes.SelectedRows[0];
                selected_node_id = sourceNode.Cells[0].Value.ToString();
            }

            var arr = new ArrayList();
            if (selected_node_id != "")
            {
                var source_json = await _browser.EvaluateScriptAsync($"getNodeJson('{selected_node_id}');");
                var source_jsonRes = source_json.Result;
                var source_data = ((IDictionary<String, Object>)source_jsonRes);
                var source_node_data = (IDictionary<String, Object>)source_data["data"];
                var node_title = source_node_data["title"].ToString();
                string node_type = source_node_data["type"].ToString();
                var node_desc = source_node_data["description"].ToString();
                var from_description = System.Convert.FromBase64String(node_desc);
                string final_description = System.Text.Encoding.UTF8.GetString((from_description));

                favoriteArray.Add(new EdgeNode(selected_node_id, node_title, node_type, Utility.RemoveRTFFormatting(final_description)));
            }
            
            for (int i = 0; i < favoriteArray.Count; i++)
            {
                arr.Add(favoriteArray[i]);
            }
            gridFavoriteNodes.DataSource = arr;
            gridFavoriteNodes.Update();
            gridFavoriteNodes.Refresh();
        }

        private void ckSourceAttack_CheckedChanged(object sender, EventArgs e)
        {
            updateSourceNodeList();
        }
    }
}
