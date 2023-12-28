using EnvDTE;
using CyConex.Graph;
using CyConex.Helpers;
using Newtonsoft.Json.Linq;
using Syncfusion.Windows.Forms.Grid;
using Syncfusion.Windows.Forms.Tools;
using Syncfusion.WinForms.Controls;
using Syncfusion.WinForms.DataGrid;
using Syncfusion.WinForms.DataGrid.Enums;
using Syncfusion.WinForms.DataGrid.Events;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace CyConex.Forms
{
    public partial class NodeDataForm : SfForm
    {
        public MainForm mainForm = null;
        public List<NodeMaster> nodeMasters = new List<NodeMaster>();
        public List<NodeDetail> nodeDetail = new List<NodeDetail>();
        private List<string> _nodeTypes = new List<string>();

        public NodeDataForm(MainForm callingForm)
        {
            InitializeComponent();
            InitializeComboBox();
            mainForm = callingForm as MainForm;
            gridRiskData.ThemeName = "Office2019Colorful";
        }

        private void InitializeComboBox()
        {
            _nodeTypes.Add("Actors");
            _nodeTypes.Add("Assets");
            _nodeTypes.Add("Attacks");
            _nodeTypes.Add("Controls");
            _nodeTypes.Add("Evidence");
            _nodeTypes.Add("Groups");
            _nodeTypes.Add("Objectives");
            _nodeTypes.Add("Vulnerabilities");

            cmbNodesStatic.ComboBoxMode = Syncfusion.WinForms.ListView.Enums.ComboBoxMode.MultiSelection;
            cmbNodesStatic.DropDownControl.ShowButtons = false;
            cmbNodesStatic.DataSource = _nodeTypes;
            foreach (var selectedItems in cmbNodesStatic.DropDownListView.View.Items)
            {
                cmbNodesStatic.DropDownListView.CheckedItems.Add(selectedItems);
            }

        }

        public void AddToMasterData(string NodeId, string Score, string Type, string Title)
        {
            nodeMasters.Add(new NodeMaster()
            {
                NodeID = NodeId,
                Score = Score,
                Type = Type,
                Title = Title,
                nodeDetails = new List<NodeDetail>()
            });
        }

        public void AddToDetailData(string MasterNodeId, string NodeId, string Score, string Relationship, string Type, string Title)
        {
            nodeDetail.Add(new NodeDetail(MasterNodeId, NodeId, Score, Relationship, Type, Title));
        }

        public void UpdateTable()
        {
            gridRiskData.BeginUpdate();
            gridRiskData.AutoGenerateColumns = true;
            gridRiskData.DataSource = nodeMasters;

            if (nodeMasters.Count == 0) return;
            gridRiskData.RowHeight = 20;

            GridViewDefinition firstLevelGridViewDefinition = new GridViewDefinition();
            firstLevelGridViewDefinition.DataGrid.QueryCellStyle += gridRiskData_QueryCellStyle;
            firstLevelGridViewDefinition.DataGrid.AutoGeneratingColumn += dataGridRiskData_AutoGenerationColumn;
            firstLevelGridViewDefinition.RelationalColumn = "NodeDetail";
            firstLevelGridViewDefinition.DataGrid.Style.HeaderStyle.BackColor = System.Drawing.Color.LightGray;
            firstLevelGridViewDefinition.DataGrid.HeaderRowHeight = 20;
            firstLevelGridViewDefinition.DataGrid.RowHeight = 20;
            firstLevelGridViewDefinition.DataGrid.AllowEditing = false;
            firstLevelGridViewDefinition.DataGrid.AllowGrouping = false;
            firstLevelGridViewDefinition.DataGrid.AllowResizingColumns = true;

            SfDataGrid firstLevelNestedGrid = new SfDataGrid();
            firstLevelNestedGrid.Name = "FirstLevelNestedGrid";
            firstLevelNestedGrid.AutoGenerateColumns = true;
            gridRiskData.DetailsViewDefinitions.Add(firstLevelGridViewDefinition);

            gridRiskData.EndUpdate();   
        }

        public void dataGridRiskData_DetailsViewExpanding(object sender, DetailsViewExpandingEventArgs e)
        {
            NodeMaster nMaster = e.Record as NodeMaster;
            nodeDetail.Clear();
            e.DetailsViewDataSource.Clear();

            //Get Parents
            List<string> parentNodeList = new List<string>();
            parentNodeList = GraphUtil.GetParentNodes(nMaster.NodeID);
            foreach (string parentNodeId in parentNodeList)
            {
                string[] parentStrings = new string[3];
                parentStrings = ProcessNode(parentNodeId);
                AddToDetailData(nMaster.NodeID, parentNodeId, parentStrings[2], "Parent", parentStrings[1], parentStrings[0]);
            }

            //Get Children
            List<string> childNodeList = new List<string>();
            childNodeList = GraphUtil.GetChildNodes(nMaster.NodeID);
            foreach (string childNodeId in childNodeList)
            {
                string[] childStrings = new string[3];
                childStrings = ProcessNode(childNodeId);
                AddToDetailData(nMaster.NodeID, childNodeId, childStrings[2], "Child", childStrings[1], childStrings[0]);
            }

            e.DetailsViewDataSource.Add("NodeDetail", nodeDetail);
            
        }

        public void dataGridRiskData_AutoGenerationColumn(object sender, AutoGeneratingColumnArgs e)
        {
            if (e.Column.MappingName == "NodeID")
                e.Cancel = true;
            if (e.Column.MappingName == "Score")
                e.Column.Width = 50;
            if (e.Column.MappingName == "Type")
                e.Column.Width = 100;
            if (e.Column.MappingName == "Title")
                e.Column.Width = 200;
        }

        void dataGridRiskData_DetailsViewLoading(object sender, DetailsViewLoadingAndUnloadingEventArgs e)
        {
            //e.DetailsViewDataGrid.Columns["NodeID"].Visible = false;
            e.DetailsViewDataGrid.Columns["MasterNodeID"].Visible = false;
            e.DetailsViewDataGrid.Columns["Score"].Width = 50;
            e.DetailsViewDataGrid.Columns["Type"].Width = 100;
            e.DetailsViewDataGrid.Columns["Title"].Width = 200;
        }


        public void ClearData()
        {
            gridRiskData.DataSource = null;
            gridRiskData.DetailsViewDefinitions.Clear();
            nodeMasters.Clear();
            nodeDetail.Clear();
        }

        private void dataGridRiskData_SelectionChanged(object sender, Syncfusion.WinForms.DataGrid.Events.SelectionChangedEventArgs e)
        {
            if (gridRiskData.SelectedItems.Count == 1)
            { 
                btnFind.Enabled = true;
                btnDetail.Enabled = true;
            }
            else
            {
                btnFind.Enabled = false;
                btnDetail.Enabled = false;
            }
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            SfDataGrid detailView = gridRiskData.SelectedDetailsViewGrid;
            if (detailView != null)
            {
                NodeDetail obj = detailView.SelectedItem as NodeDetail;
                mainForm.SelectNodeonGraph(obj.NodeID);
            }
            else
            {
                NodeMaster obj = gridRiskData.SelectedItem as NodeMaster;
                if (obj != null)
                {
                    mainForm.SelectNodeonGraph(obj.NodeID);
                }
            }
        }

        private void btnDetail_Click(object sender, EventArgs e)
        {
            SfDataGrid detailView = gridRiskData.SelectedDetailsViewGrid;
            if (detailView != null)
            {
                NodeDetail obj = detailView.SelectedItem as NodeDetail;
                mainForm.SelectNodeandShowDetail(obj.NodeID);
            }
            else
            {
                NodeMaster obj = gridRiskData.SelectedItem as NodeMaster;
                if (obj != null)
                {
                    mainForm.SelectNodeandShowDetail(obj.NodeID);
                }
            }
        }

        private string[] ProcessNode(string nodeId)
        {
            string[] strings = new string[3]; // 0 = Node Title, 1 = Node Type, 2 = Node Score

            string nodeType = GraphUtil.GetNodeType(nodeId);
            strings[0] = GraphUtil.GetNodeTitle(nodeId);
            switch (nodeType)
            {
                case "control":
                    strings[1] = "Control";
                    strings[2] = GraphUtil.GetNodeCalculatedValue(nodeId).ToString("F2");
                    break;
                case "actor":
                    strings[1] = "Actor";
                    strings[2] = GraphUtil.GetActorNodeMitigatedScore(nodeId).ToString("F2");
                    break;
                case "attack":
                    strings[1] = "Attack";
                    strings[2] = GraphUtil.GetAttackNodeMitigatedScore(nodeId).ToString("F2");
                    break;
                case "vulnerability":
                    strings[1] = "Vulnerability";
                    strings[2] = GraphUtil.GetVulnerabilityNodeMitigatedScore(nodeId).ToString("F2");
                    break;
                case "asset":
                    strings[1] = "Asset";
                    strings[2] = GraphUtil.GetAssetNodeMitigatedScore(nodeId).ToString("F2");
                    break;
                case "objective":
                    strings[1] = "Objective";
                    strings[2] = "N/A";
                    break;
            }

             return strings;
        }
        private void sfButton1_Click(object sender, EventArgs e)
        {
            ClearData();
            List<string> masterNodeList = new List<string>();
            List<string> checked_Node_items = new List<string>();
            foreach (string item in cmbNodesStatic.CheckedItems)
                checked_Node_items.Add(item.ToLower());

            if (checked_Node_items.Contains("objectives"))
                masterNodeList.AddRange(GraphUtil.GetObjectiveNodes());

            if (checked_Node_items.Contains("controls"))
                masterNodeList.AddRange(GraphUtil.GetControlNodes());

            if (checked_Node_items.Contains("groups"))
                masterNodeList.AddRange(GraphUtil.GetGroupNodes());

            if (checked_Node_items.Contains("actors"))
                masterNodeList.AddRange(GraphUtil.GetActorNodes());

            if (checked_Node_items.Contains("attacks"))
                masterNodeList.AddRange(GraphUtil.GetAttackNodes());

            if (checked_Node_items.Contains("assets"))
                masterNodeList.AddRange(GraphUtil.GetAssetNodes());

            if (checked_Node_items.Contains("vulnerabilities"))
                masterNodeList.AddRange(GraphUtil.GetVulnerabilityNodes());

            if (checked_Node_items.Contains("evidence"))
                masterNodeList.AddRange(GraphUtil.GetEvidenceNodes());

            foreach (string masterNodeId in masterNodeList)
            {
                //Get Master Node List
                string[] masterStrings = new string[3];
                masterStrings = ProcessNode(masterNodeId);
                AddToMasterData(masterNodeId, masterStrings[2], masterStrings[1], masterStrings[0]);

            }
            UpdateTable();
        }

        private void gridRiskData_QueryCellStyle(object sender, QueryCellStyleEventArgs e)
        {
            Color color;

            if (e.Column != null && e.Column.MappingName == "Score")
            {
                if (e.DisplayText != "N/A")
                {
                    Type dataType = e.DataRow.RowData.GetType();
                    if (dataType == typeof(NodeMaster))
                    {
                        NodeMaster data = (NodeMaster)e.DataRow.RowData;
                        
                        if (data.Type != "Control")
                            color = GraphUtil.GetRiskColorFromValue(double.Parse(e.DisplayText));
                        else
                            color = GraphUtil.GetRiskColorFromValueInverted(double.Parse(e.DisplayText));
                    }
                    else
                    {
                        NodeDetail data = (NodeDetail)e.DataRow.RowData;
                        if (data.Type != "Control")
                            color = GraphUtil.GetRiskColorFromValue(double.Parse(e.DisplayText));
                        else
                            color = GraphUtil.GetRiskColorFromValueInverted(double.Parse(e.DisplayText));
                    }
                   
                    e.Style.BackColor = color;
                }
            }
        }
    }
}
