using CefSharp;
using CefSharp.DevTools.DOM;
using CyConex.Chromium;
using CyConex.Graph;
using CyConex.Helpers;
using Syncfusion.Windows.Forms.Diagram;
using Syncfusion.WinForms.DataGrid;
using Syncfusion.WinForms.DataGrid.Events;
using Syncfusion.WinForms.DataGrid.Interactivity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using Syncfusion.WinForms.DataGridConverter;
using System.Windows.Media;
using CefSharp.DevTools.CSS;
using Syncfusion.Windows.Forms.Chart;
using System.Collections;
using Syncfusion.Drawing;
using Newtonsoft.Json.Linq;
using Syncfusion.XlsIO;

namespace CyConex.Resources
{
    public partial class ComplianceForm : Form
    {
        public MainForm mainForm = null;
        public List<ComplianceMaster> ComplianceMaster = new List<ComplianceMaster>();
        public List<ComplianceDetail> ComplianceDetail = new List<ComplianceDetail>();
        public List<string> ancestorControlNodes = new List<string>();
        public List<string> ancestorObjectiveNodes = new List<string>();
        public bool initialized = false;
        private string CurrentNodeID;
        private bool containsControls = false;
        private bool containsObjectives = false;
        public List<NodeMaster> nodeMasters = new List<NodeMaster>();

        public ComplianceForm(MainForm callingForm)
        {
            InitializeComponent();
            mainForm = callingForm as MainForm;
            InitializeTable();
            
        }

        private void InitializeTable()
        {   
            GridViewDefinition firstLevelGridViewDefinition = new GridViewDefinition();
            firstLevelGridViewDefinition.DataGrid.QueryCellStyle += gridRiskData_QueryCellStyle;
            firstLevelGridViewDefinition.DataGrid.AutoGeneratingColumn += dataGridRiskData_AutoGenerationColumn;
            firstLevelGridViewDefinition.DataGrid.Style.HeaderStyle.BackColor = System.Drawing.Color.LightGray;
            firstLevelGridViewDefinition.DataGrid.HeaderRowHeight = 20;
            firstLevelGridViewDefinition.DataGrid.RowHeight = 20;
            firstLevelGridViewDefinition.DataGrid.AllowResizingColumns = true;
            gridNodeData.RowHeight = 20;
            gridNodeData.AutoGenerateColumns = true;
            gridNodeData.ThemeName = "Office2019Colorful";

        }

        public void AddToMasterData(string NodeId, double Score, string Type, string Title, string Description, string Framework,
                                    string Reference, double? Strength, double? Implemented, double? ObjectiveTarget, 
                                    double? ObjectiveAcheived, string NodesInPath, string NodeStrengthText, string NodeAcheivedString)
        {
            ComplianceMaster.Add(new ComplianceMaster()
            {
                NodeID = NodeId,
                Score = Score,
                Type = Type,
                Title = Title,
                Description = Description,
                Framework = Framework,
                Reference = Reference,
                Strength = Strength,
                Implemented = Implemented,
                ObjectiveTarget = ObjectiveTarget,
                ObjectiveAcheived = ObjectiveAcheived,
                NodesInPath = NodesInPath,
                ControlStrengthText = NodeStrengthText,
                ControlImplementedText = NodeAcheivedString,
            });

            try
            {
                if (Type == "Control" && NodeId != CurrentNodeID)  // Don't add the currently selcted Node to the Chart
                {
                    AddControlToRadarChart((int)Score, Title);
                    GraphUtil.AddToObjectiveBuckets((int)Score);
                }
                else if (Type == "Objective" && NodeId != CurrentNodeID)
                {
                    AddObjectiveToRadarChart((int)Score, Title);
                    GraphUtil.AddToObjectiveBuckets((int)Score);
                }
            }
            catch { }
            



        }
        private void updateHeatmapChart()
        {
            Dictionary<string, int> riskBuckets = new Dictionary<string, int>();
            riskBuckets = GraphUtil.GetRiskBuckets();
            chartCompliancebyCatagory.Series[0].Points.Clear();

            foreach (KeyValuePair<string, int> item in riskBuckets)
            {
                chartCompliancebyCatagory.Series[0].Points.Add(item.Value);
                chartCompliancebyCatagory.Series[0].Points[chartCompliancebyCatagory.Series[0].Points.Count - 1].Label = item.Key;
                chartCompliancebyCatagory.Series[0].Points[chartCompliancebyCatagory.Series[0].Points.Count - 1].Color = GraphUtil.GetObjectiveColorFromName(item.Key);
            }

        }

        private void Series_PrepareStyle(object sender, ChartPrepareStyleInfoEventArgs args)
        {
            string tempString = $"{GraphUtil.GetRiskBucketByIndex(args.Index).Item1} ({GraphUtil.GetRiskBucketByIndex(args.Index).Item2})";
            args.Style.Text = tempString;

            //args.Style.Interior = new BrushInfo(GraphUtil.GetObjectiveColorFromName(GraphUtil.GetRiskBucketByIndex(args.Index).Item1));

            args.Style.Interior = new Syncfusion.Drawing.BrushInfo(Syncfusion.Drawing.GradientStyle.Horizontal, System.Drawing.Color.DarkGreen, System.Drawing.Color.LightYellow);
        }

            public void AddToDetailData(string MasterNodeId, string NodeId, string NodeType, string NodeRelationship, string NodeTitle, string Description, string Framework, string Reference)
        {
            ComplianceDetail.Add(new ComplianceDetail()
            {
                MasterNodeID = MasterNodeId,
                NodeID = NodeId,
                Type = NodeType,
                Releationship = NodeRelationship,
                Title = NodeTitle,
                Description = Description,
                Framework = Framework,
                Reference = Reference
            });
        }


        public void UpdateTable()
        {
            gridNodeData.BeginUpdate();
            gridNodeData.DataSource = null;
            gridNodeData.DataSource = ComplianceMaster;

            gridNodeData.SortColumnDescriptions.Add(new SortColumnDescription()
            {
                ColumnName = "Score", 
                SortDirection = ListSortDirection.Descending 
            });

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
            gridNodeData.DetailsViewDefinitions.Add(firstLevelGridViewDefinition);

            gridNodeData.EndUpdate();
            gridNodeData.Refresh();
        }

        private void gridRiskData_QueryCellStyle(object sender, QueryCellStyleEventArgs e)
        {
            System.Drawing.Color color;


            if (e.Column != null && (e.Column.MappingName == "Score"))
            {
                if (e.DisplayText != "N/A")
                {
                    Type dataType = e.DataRow.RowData.GetType();
                    ComplianceMaster data = (ComplianceMaster)e.DataRow.RowData;

                        if (e.Column.MappingName == "EdgeStrength")
                            color = GraphUtil.GetRiskColorFromValue(double.Parse(e.DisplayText));
                        else
                            color = GraphUtil.GetRiskColorFromValueInverted(double.Parse(e.DisplayText));

                    e.Style.BackColor = color;
                }
            }
        }

        public void dataGridRiskData_AutoGenerationColumn(object sender, AutoGeneratingColumnArgs e)
        {
            
            if (e.Column.MappingName == "NodeID")
                e.Cancel = true;
            if (e.Column.MappingName == "Score")
            {
                e.Column.Width = 75;
            }
            
            if (e.Column.MappingName == "Strength")
            {
                if (containsControls)
                {
                    e.Column.Width = 75;
                    e.Column.HeaderText = "Control Strength";
                }
                else
                    e.Column.Visible = false;
            }
               
            if (e.Column.MappingName == "Implemented")
            {
                if (containsControls)
                {
                    e.Column.Width = 75;
                    e.Column.HeaderText = "Control Implementation";
                }
                else
                    e.Column.Visible = false;
            }
            if (e.Column.MappingName == "Type")
                e.Column.Width = 100;
            if (e.Column.MappingName == "ObjectiveTarget")
            {
                if (containsObjectives)
                {
                    e.Column.Width = 75;
                    e.Column.HeaderText = "Objective Target";
                }
                else
                    e.Column.Visible = false; 
            }

            if (e.Column.MappingName == "ObjectiveAcheived")
            {
                if (containsObjectives)
                {
                    e.Column.Width = 75;
                    e.Column.HeaderText = "Objective Acheived";
                }
                else
                    e.Column.Visible = false;
            }

            if (e.Column.MappingName == "NodesInPath")
            {
                e.Column.HeaderText = "Node Path";
            }

            if (e.Column.MappingName == "ControlStrengthText")
            {
                e.Column.HeaderText = "Control Strength";
            }

            if (e.Column.MappingName == "ControlImplementedText")
            {
                e.Column.HeaderText = "Control Implementation";
            }

        }

        private void AddControlToRadarChart(int value, string title)
        {
            chartRadar.Series[0].Points.Add(value);
            chartRadar.Series[0].Points[chartRadar.Series[0].Points.Count -1].Label = title;
        }

        private void AddObjectiveToRadarChart(int value, string title)
        {
            chartRadar.Series[1].Points.Add(value);
            chartRadar.Series[1].Points[chartRadar.Series[1].Points.Count - 1].Label = title;
        }

        
        private void UpdateGraphsAndTable(string nodeID)
        {
            containsControls = false;
            containsObjectives = false;
            //get parent Control Nodes
            List<string> nodeGUIDs = new List<string>();


            
            ClearData();

            if (cbShowControls.Checked)
            {
                if (cbShowAncestors.Checked)
                {
                    ancestorControlNodes = GraphUtil.GetAllUpstreamNodesbyType(nodeID, "control");
                    nodeGUIDs = ancestorControlNodes;
                }
                else
                    nodeGUIDs = GraphUtil.GetParentControlNodes(nodeID);

                if (GraphUtil.GetNodeType(nodeID) == "control")
                {
                    double tempNodeBaseScore = GraphUtil.GetNodeBaseScore(nodeID);
                    double tempNodeAssessedScore = GraphUtil.GetControlNodeAssessedScore(nodeID);
                    // Add the Selected Control
                    AddToMasterData(nodeID,
                                        GraphUtil.GetNodeCalculatedValue(nodeID),
                                        "Control",
                                        Utility.RemoveHTML(GraphUtil.GetNodeTitle(nodeID)),
                                        Utility.RemoveHTML(Utility.DecodeBase64Text(GraphUtil.GetNodeDescription(nodeID))),
                                        GraphUtil.GetNodeFrameworkName(nodeID),
                                        GraphUtil.GetNodeFrameworkReference(nodeID),
                                        tempNodeBaseScore,
                                        tempNodeAssessedScore,
                                        null,
                                        null,
                                        GraphUtil.GetAllNodesByTitleInPath(nodeID, CurrentNodeID).ToString(),
                                        GetControlStrengthText((int)tempNodeBaseScore),
                                        GetControlImplementedText((int)tempNodeAssessedScore));
                    containsControls = true;
                }

                // Add the Ancestor Controls
                foreach (string nodeGUID in nodeGUIDs)
                {
                    double tempNodeBaseScore = GraphUtil.GetNodeBaseScore(nodeGUID);
                    double tempNodeAssessedScore = GraphUtil.GetControlNodeAssessedScore(nodeGUID);
                    AddToMasterData(nodeGUID,
                                    GraphUtil.GetNodeCalculatedValue(nodeGUID),
                                    "Control",
                                    Utility.RemoveHTML(GraphUtil.GetNodeTitle(nodeGUID)),
                                    Utility.RemoveHTML(Utility.DecodeBase64Text(GraphUtil.GetNodeDescription(nodeGUID))),
                                    GraphUtil.GetNodeFrameworkName(nodeGUID),
                                    GraphUtil.GetNodeFrameworkReference(nodeGUID),
                                    tempNodeBaseScore,
                                    tempNodeAssessedScore,
                                    null,
                                    null,
                                    GraphUtil.GetAllNodesByTitleInPath(nodeGUID, CurrentNodeID).ToString(),
                                    GetControlStrengthText((int)tempNodeBaseScore),
                                    GetControlImplementedText((int)tempNodeAssessedScore));
                    containsControls = true;
                }
            }


            if (cbShowObjectives.Checked)
            {
                if (cbShowAncestors.Checked)
                {
                    ancestorObjectiveNodes = GraphUtil.GetAllUpstreamNodesbyType(nodeID, "objective");
                    nodeGUIDs = ancestorObjectiveNodes;
                }
                else
                    nodeGUIDs = GraphUtil.GetParentObjectivelNodes(nodeID);

                if (GraphUtil.GetNodeType(nodeID) == "objective")
                {
                    // Add the Selected Objective
                    double tempAcheivedValue = GraphUtil.GetNodeObjectiveAcheivedValue(nodeID);
                    double tempTargetValue = GraphUtil.GetNodeObjectiveTargetValue(nodeID);
                    double tempComplianceScore = (tempAcheivedValue / tempTargetValue) * 100;
                    AddToMasterData(nodeID,
                                        tempComplianceScore,
                                        "Objective",
                                        Utility.RemoveHTML(GraphUtil.GetNodeTitle(nodeID)),
                                        Utility.RemoveHTML(Utility.DecodeBase64Text(GraphUtil.GetNodeDescription(nodeID))),
                                        GraphUtil.GetNodeFrameworkName(nodeID),
                                        GraphUtil.GetNodeFrameworkReference(nodeID),
                                        null,
                                        null,
                                        tempTargetValue,
                                        tempAcheivedValue,
                                        GraphUtil.GetAllNodesByTitleInPath(nodeID, CurrentNodeID).ToString(),
                                        "",
                                        "");

                    containsObjectives = true;
                }

                // Add the Ancestor Objectives
                foreach (string nodeGUID in nodeGUIDs)
                {
                    double tempAcheivedValue = GraphUtil.GetNodeObjectiveAcheivedValue(nodeGUID);
                    double tempTargetValue = GraphUtil.GetNodeObjectiveTargetValue(nodeGUID);
                    double tempComplianceScore = (tempAcheivedValue / tempTargetValue) * 100;
                    AddToMasterData(nodeGUID,
                                    tempComplianceScore,
                                    "Objective",
                                    Utility.RemoveHTML(GraphUtil.GetNodeTitle(nodeGUID)),
                                    Utility.RemoveHTML(Utility.DecodeBase64Text(GraphUtil.GetNodeDescription(nodeGUID))),
                                    GraphUtil.GetNodeFrameworkName(nodeGUID),
                                    GraphUtil.GetNodeFrameworkReference(nodeGUID),
                                    null,
                                    null,
                                    tempTargetValue,
                                    tempAcheivedValue,
                                    GraphUtil.GetAllNodesByTitleInPath(nodeGUID, CurrentNodeID).ToString(),
                                    "",
                                    ""); 
                    containsObjectives = true;
                   
                }
            }

            UpdateTable();
            updateHeatmapChart();
        }

        public void UpdateCompliancePanel(string nodeID)
        {
            CurrentNodeID = nodeID;
            lblObjectiveTitle.Text = "Objective: " + Utility.RemoveHTML(GraphUtil.GetNodeTitle(nodeID));

            double targetScore = GraphUtil.GetNodeObjectiveTargetValue(nodeID);
            double acheivedScore = GraphUtil.GetNodeObjectiveAcheivedValue(nodeID);
            double complianceScore = (100 / targetScore) * acheivedScore;
            double diffrence = targetScore - acheivedScore;

            lblTargetScore.Text = GraphUtil.ClampValueAboveZero(targetScore).ToString("F2");
            lblAcheivedScore.Text = GraphUtil.ClampValueAboveZero(acheivedScore).ToString("F2");
            lblComplianceScore.Text = Math.Round(GraphUtil.ClampValueAboveZero(complianceScore), 2).ToString() + "%";

            int parentControlNodeCount = GraphUtil.GetParentControlNodesCount(nodeID);
            int parentObjectiveNodeCount = GraphUtil.GetParentObjectiveNodesCount(nodeID);
            ancestorControlNodes = GraphUtil.GetAllUpstreamNodesbyType(nodeID, "control");
            ancestorObjectiveNodes = GraphUtil.GetAllUpstreamNodesbyType(nodeID, "objective");
            
            int ancestorControlNodeCount = ancestorControlNodes.Count();
            int ancestorObjectiveNodeCount = ancestorObjectiveNodes.Count();
            lblAncestorControlNodeCount.Text = ancestorControlNodeCount.ToString();
            lblAncestorObjectiveNodeCount.Text = ancestorObjectiveNodeCount.ToString();

            lblParentControlNodeCount.Text = parentControlNodeCount.ToString();
            lblParentObjectiveNodeCount.Text = parentObjectiveNodeCount.ToString();


            //Build Compliance Chart
            chartCompliance.Series[0].Points.Clear();
            if (diffrence > 0)
            {
                int pointsCount = chartCompliance.Series[0].Points.Count;
                chartCompliance.Series[0].Points.AddY(diffrence);
                chartCompliance.Series[0].Points[pointsCount].Color = System.Drawing.Color.Salmon;
                chartCompliance.Series[0].Points[pointsCount].Label = "Not Compliant";
            }
            if (acheivedScore > 0)
            {
                int pointsCount = chartCompliance.Series[0].Points.Count;
                chartCompliance.Series[0].Points.AddY(acheivedScore);
                chartCompliance.Series[0].Points[pointsCount].Color = System.Drawing.Color.PaleGreen;
                chartCompliance.Series[0].Points[pointsCount].Label = "Compliant";
            }

            UpdateGraphsAndTable(nodeID);
            
        }

        public void ClearData()
        {
            gridNodeData.DataSource = null;
            gridNodeData.DetailsViewDefinitions.Clear();
            ComplianceMaster.Clear();
            ComplianceDetail.Clear();
            chartRadar.Series[0].Points.Clear();
            chartRadar.Series[1].Points.Clear();
            GraphUtil.ClearBuckets();
        }

        private void cbShowAncestors_CheckedChanged(object sender, EventArgs e)
        {
            UpdateGraphsAndTable(CurrentNodeID);
        }

        private void panelTopRight_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            if (gridNodeData.SelectedItem == null)
                return;
            try
            {
                ComplianceMaster obj = gridNodeData.SelectedItem as ComplianceMaster;
                mainForm.SelectNodeonGraph(obj.NodeID);
            }
            catch { }
        }

        private void btnDetail_Click(object sender, EventArgs e)
        {
            if (gridNodeData.SelectedItem == null)
                return;
            try
            {
                ComplianceMaster obj = gridNodeData.SelectedItem as ComplianceMaster;
                mainForm.SelectNodeandShowDetail(obj.NodeID);
            }
            catch { }
            
        }

        private void gridNodeData_SelectionChanged(object sender, Syncfusion.WinForms.DataGrid.Events.SelectionChangedEventArgs e)
        {
            if (gridNodeData.SelectedItems.Count == 1)
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

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ColumnChooserPopup columnChooser = new ColumnChooserPopup(this.gridNodeData);
            columnChooser.Show();
        }

        private void gridNodeData_DetailsViewExpanding(object sender, DetailsViewExpandingEventArgs e)
        {
            ComplianceMaster nMaster = e.Record as ComplianceMaster;
            ComplianceDetail.Clear();
            e.DetailsViewDataSource.Clear();

            //Get Parents
            List<string> parentNodeList = new List<string>();
            parentNodeList = GraphUtil.GetParentObjectiveNodes(nMaster.NodeID);
            parentNodeList.AddRange(GraphUtil.GetParentControlNodes(nMaster.NodeID));
            foreach (string parentNodeId in parentNodeList)
            {
                string[] parentStrings = new string[5];
                parentStrings = ProcessNode(parentNodeId);
                AddToDetailData(nMaster.NodeID, parentNodeId, parentStrings[1], "Parent", parentStrings[0], parentStrings[2], parentStrings[3], parentStrings[4]);
            }

            //Get Children
            List<string> childNodeList = new List<string>();
            childNodeList = GraphUtil.GetChildObjectiveNodes(nMaster.NodeID);
            childNodeList.AddRange(GraphUtil.GetChildAssetNodes(nMaster.NodeID));
            childNodeList.AddRange(GraphUtil.GetChildAssetGroupNodes(nMaster.NodeID));
            foreach (string childNodeId in childNodeList)
            {
                string[] childStrings = new string[5];
                childStrings = ProcessNode(childNodeId);
                AddToDetailData(nMaster.NodeID, childNodeId, childStrings[1], "Child", childStrings[0], childStrings[2], childStrings[3], childStrings[4]);
            }

            e.DetailsViewDataSource.Add("NodeDetail", ComplianceDetail);

        }

        private string[] ProcessNode(string nodeId)
        {
            string[] strings = new string[5]; // 0 = Node Title, 1 = Node Type, 2 = Description, 3 = Framework, 4 = Reference

            string nodeType = GraphUtil.GetNodeType(nodeId);
            switch (nodeType)
            {
                case "control":
                    strings[0] = Utility.RemoveHTML(GraphUtil.GetNodeTitle(nodeId));
                    strings[1] = "Control";
                    strings[2] = Utility.RemoveHTML(GraphUtil.GetNodeDescription(nodeId));
                    strings[3] = GraphUtil.GetNodeFrameworkName(nodeId);
                    strings[4] = GraphUtil.GetNodeFrameworkReference(nodeId);
                    break;
                case "objective":
                    strings[0] = Utility.RemoveHTML(GraphUtil.GetNodeTitle(nodeId));
                    strings[1] = "Objective";
                    strings[2] = Utility.RemoveHTML(GraphUtil.GetNodeDescription(nodeId));
                    strings[3] = GraphUtil.GetNodeFrameworkName(nodeId);
                    strings[4] = GraphUtil.GetNodeFrameworkReference(nodeId);
                    break;
                case "asset":
                    strings[0] = Utility.RemoveHTML(GraphUtil.GetNodeTitle(nodeId));
                    strings[1] = "Asset";
                    strings[2] = Utility.RemoveHTML(GraphUtil.GetNodeDescription(nodeId));
                    strings[3] = GraphUtil.GetNodeFrameworkName(nodeId);
                    strings[4] = GraphUtil.GetNodeFrameworkReference(nodeId);
                    break;
                case "asset-group":
                    strings[0] = Utility.RemoveHTML(GraphUtil.GetNodeTitle(nodeId));
                    strings[1] = "Asset Group";
                    strings[2] = Utility.RemoveHTML(GraphUtil.GetNodeDescription(nodeId));
                    strings[3] = GraphUtil.GetNodeFrameworkName(nodeId);
                    strings[4] = GraphUtil.GetNodeFrameworkReference(nodeId);
                    break;
            }

            return strings;
        }

        private void gridNodeData_DetailsViewLoading(object sender, DetailsViewLoadingAndUnloadingEventArgs e)
        {
            //e.DetailsViewDataGrid.Columns["NodeID"].Visible = false;
            e.DetailsViewDataGrid.Columns["MasterNodeID"].Visible = false;
            e.DetailsViewDataGrid.Columns["Type"].Width = 100;
            e.DetailsViewDataGrid.Columns["Title"].Width = 200;
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            string folder = Utility.PromptForFolder();
            if (folder != null)
            {
                var options = new ExcelExportingOptions();
                var excelEngine = gridNodeData.ExportToExcel(gridNodeData.View, options);
                var workBook = excelEngine.Excel.Workbooks[0];
                IWorksheet worksheet = workBook.Worksheets[0];

                int lastRow = worksheet.UsedRange.LastRow;
                int lastColumn = worksheet.UsedRange.LastColumn;

                for (int col = 1; col <= lastColumn; col++)
                {
                    bool shouldWrapColumn = false;

                    for (int row = 1; row <= lastRow; row++)
                    {
                        string cellValue = worksheet.Range[row, col].DisplayText;
                        if (cellValue.Length > 50)
                        {
                            shouldWrapColumn = true;
                            break;
                        }
                    }

                    if (shouldWrapColumn)
                    {
                        worksheet.Range[1, col, lastRow, col].CellStyle.WrapText = true;
                        worksheet.Range[1, col, lastRow, col].ColumnWidth = 50;
                    }
                    else
                    {
                        worksheet.Columns[col].AutofitColumns();
                    }

                    
                }
                for (int row = 1; row <= lastRow; row++)
                {
                    worksheet.AutofitRow(row);
                }

                try
                {
                    workBook.SaveAs(folder + "\\Compliance.xlsx");
                }
                catch(Exception ex)
                {
                    MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private string GetControlStrengthText(int ControlStrengthValue)
        {
            JArray data = new JArray();
            data = mainForm._settings.NodeinherentStrengthData;

            if (data == null)
            {
                return "No data available";
            }

            List<Tuple<string, int, string>> ValuesText = new List<Tuple<string, int, string>>();

            for (int i = 0; i < data.Count; i++)
            {
                string tempImpact = string.Empty;
                int tempValue = 0;
                string tempDescription = string.Empty;
                var item = data[i];
                tempImpact = (string)item["impact"];
                tempValue = (int)item["value"];
                tempDescription = (string)item["description"];
                ValuesText.Add(Tuple.Create(tempImpact, tempValue, tempDescription));
            }

            // Order the list by the absolute difference between each tuple's value and the ControlStrengthValue
            // Then take the first tuple, which will have the nearest value.
            var nearestTuple = ValuesText.OrderBy(t => Math.Abs(t.Item2 - ControlStrengthValue)).FirstOrDefault();

            // If a tuple is found, return its description; otherwise, return a default message.
            return nearestTuple?.Item3 ?? "No description available";

        }

        private string GetControlImplementedText(int ControlImplementedValue)
        {
            JArray data = new JArray();
            data = mainForm._settings.NodeimplementedStrengthData;

            if (data == null)
            {
                return "No data available";
            }

            List<Tuple<string, int, string>> ValuesText = new List<Tuple<string, int, string>>();

            for (int i = 0; i < data.Count; i++)
            {
                string tempImpact = string.Empty;
                int tempValue = 0;
                string tempDescription = string.Empty;
                var item = data[i];
                tempImpact = (string)item["impact"];
                tempValue = (int)item["value"];
                tempDescription = (string)item["description"];
                ValuesText.Add(Tuple.Create(tempImpact, tempValue, tempDescription));
            }

            // Order the list by the absolute difference between each tuple's value and the ControlStrengthValue
            // Then take the first tuple, which will have the nearest value.
            var nearestTuple = ValuesText.OrderBy(t => Math.Abs(t.Item2 - ControlImplementedValue)).FirstOrDefault();

            // If a tuple is found, return its description; otherwise, return a default message.
            return nearestTuple?.Item3 ?? "No description available";

        }

        private void panelComplianceContainer_Resize(object sender, EventArgs e)
        {
            panelTopLeft.Width = (panelComplianceContainer.Width / 100) * 40;
            panelTopCenter.Width = (panelComplianceContainer.Width / 100) * 40;
            panelTopRight.Width = (panelComplianceContainer.Width / 100) * 20;
        }

        private void btnNodePath_Click(object sender, EventArgs e)
        {

        }
    }
}
