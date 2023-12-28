using EnvDTE;
using CyConex.Graph;
using Syncfusion.Windows.Forms.Tools;
using Syncfusion.WinForms.ListView;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Control = System.Windows.Forms.Control;
using Panel = System.Windows.Forms.Panel;

namespace CyConex
{


    public partial class NodesForm : Form
    {
        private MainForm mainForm = null;
        private Panel panelNodeDetailDynamic;
        private Panel panelColorDynamic;
        private System.Windows.Forms.Label labelScoreDynamic;
        private System.Windows.Forms.Label labelObjectiveTitleDynamic;
        private Panel panelRightDynamic;
        private Panel panelTopLeftDynamic;
        private Panel panelLeftDynamic;
        private FlowLayoutPanel flowLayoutControlsDynamic;
        private System.Windows.Forms.Label labelChildDynamic;
        private System.Windows.Forms.Label labelObjectiveDynamic;
        private Splitter splitter1Dynamic;
        private Panel panelBottomSpacerDynamic;
        private Panel panelBottomDynamic;
        private System.Windows.Forms.Button btnNodeFindDynamic;
        private System.Windows.Forms.Button btnNodeSettingsDynamic;
        private Panel panelChildNodeColorDynamic;
        private System.Windows.Forms.Label labelChildNodeDynamic;
        private System.Windows.Forms.Label labelChildNodeTypeDynamic;
        private System.Windows.Forms.Label labelControlScoreDynamic;
        private System.Windows.Forms.Button btnChildNodeFindDynamic;
        private System.Windows.Forms.Button btnChildNodeSettingsDynamic;

        private bool buildingNodes = false;
        //public static List<KeyValuePair<string, string>> _nodeTypes = new List<KeyValuePair<string, string>>();
        private List<string> _nodeTypes = new List<string>();
        private int startRecord = 0;
        private int endRecord = 0;
        private int showRecords = 20;
        private int totalRecords = 0;
        private List<nodeDetails> _orderedNodeObjects = new List<nodeDetails>();
        private List<nodeDetails> _orderedNodeObjects1 = new List<nodeDetails>();
        private List<nodeDetails> _nodeObjects = new List<nodeDetails>();
        private List<string> _graphNodes = new List<string>();

        public NodesForm(MainForm callingForm)
        {
            mainForm = callingForm as MainForm;

            InitializeComponent();
            InitializeComboBox();
        }

        public void ClearValues()
        {
            _orderedNodeObjects.Clear();
            _orderedNodeObjects1.Clear();
            _nodeObjects.Clear();
            _graphNodes.Clear();
        }

        public Panel PanelChildNodeDynamic { get; private set; }

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
            cmbNodesStatic.DataSource = _nodeTypes;

            cbSortBy.SelectedIndex = 2;



        }

        public void AddToNodeList(List<nodeDetails> nodeObjects)
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NodesForm));
            this.PanelControlStatic.SuspendLayout();
            this.PanelControlStatic.Controls.Clear();

            // Build the objective panels

            foreach (nodeDetails nodeObject in nodeObjects)
            {
                this.panelNodeDetailDynamic = new System.Windows.Forms.Panel();
                this.panelNodeDetailDynamic.SuspendLayout();
                this.panelNodeDetailDynamic.Name = "nodeDetail";

                this.panelColorDynamic = new System.Windows.Forms.Panel();
                this.labelScoreDynamic = new System.Windows.Forms.Label();
                this.labelObjectiveTitleDynamic = new System.Windows.Forms.Label();
                this.panelRightDynamic = new System.Windows.Forms.Panel();
                this.panelTopLeftDynamic = new System.Windows.Forms.Panel();
                this.panelLeftDynamic = new System.Windows.Forms.Panel();
                this.flowLayoutControlsDynamic = new System.Windows.Forms.FlowLayoutPanel();
                this.labelObjectiveDynamic = new System.Windows.Forms.Label();
                this.labelScoreDynamic = new System.Windows.Forms.Label();

                this.panelNodeDetailDynamic.Tag = nodeObject;

                List<string> associatedParentNodes = new List<string>();
                List<string> associatedChildNodes = new List<string>();

                //Get parent and child Nodes for the Node
                associatedParentNodes.Clear();
                associatedChildNodes.Clear();

                associatedParentNodes = GraphUtil.GetParentNodes(nodeObject.nodeID);
                associatedChildNodes = GraphUtil.GetChildNodes(nodeObject.nodeID);
               


                BuildPrimaryPanel(nodeObject);

                if (associatedParentNodes.Count > 0)
                {
                    // labelControls
                    this.labelChildDynamic = new System.Windows.Forms.Label();
                    this.labelChildDynamic.Text = "Parent Nodes:";

                    BuildSubPanels(associatedParentNodes);
                }
                if (associatedChildNodes.Count > 0)
                {
                    // labelControls
                    this.labelChildDynamic = new System.Windows.Forms.Label();
                    this.labelChildDynamic.Text = "Child Nodes:";

                    BuildSubPanels(associatedChildNodes);
                }

                this.PanelControlStatic.Controls.Add(panelNodeDetailDynamic);
                this.panelNodeDetailDynamic.ResumeLayout();
            }

            this.PanelControlStatic.ResumeLayout();


            void BuildPrimaryPanel(nodeDetails nodeObject)
            {
                // labelObjective
                this.labelObjectiveDynamic.Dock = System.Windows.Forms.DockStyle.Left;
                this.labelObjectiveDynamic.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.labelObjectiveDynamic.Location = new System.Drawing.Point(0, 0);
                this.labelObjectiveDynamic.Size = new System.Drawing.Size(100, 20);
                this.labelObjectiveDynamic.Text = Char.ToUpper(nodeObject.Type.ToLower()[0]) + nodeObject.Type.ToLower().Remove(0, 1);
                this.labelObjectiveDynamic.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;


                // panelTopLeft
                this.panelTopLeftDynamic.Controls.Add(this.labelObjectiveDynamic);
                this.panelTopLeftDynamic.Dock = System.Windows.Forms.DockStyle.Top;
                this.panelTopLeftDynamic.Location = new System.Drawing.Point(0, 0);
                this.panelTopLeftDynamic.Margin = new System.Windows.Forms.Padding(0);
                this.panelTopLeftDynamic.Size = new System.Drawing.Size(221, 20);

                // panelLeft
                this.panelLeftDynamic.Controls.Add(this.labelObjectiveTitleDynamic);
                this.panelLeftDynamic.Controls.Add(this.panelTopLeftDynamic);
                this.panelLeftDynamic.Dock = System.Windows.Forms.DockStyle.Left;
                this.panelLeftDynamic.Location = new System.Drawing.Point(31, 0);
                this.panelLeftDynamic.Margin = new System.Windows.Forms.Padding(0);
                this.panelLeftDynamic.Size = new System.Drawing.Size(221, 128);

                //control Flow Panal
                this.panelRightDynamic.Controls.Add(this.flowLayoutControlsDynamic);
                this.flowLayoutControlsDynamic.Dock = System.Windows.Forms.DockStyle.Fill;
                this.flowLayoutControlsDynamic.Location = new System.Drawing.Point(233, 0);
                this.flowLayoutControlsDynamic.AutoScroll = false;
                this.flowLayoutControlsDynamic.Resize += new System.EventHandler(this.flowpanelControls_Resize);
                this.flowLayoutControlsDynamic.Name = "flowLayoutControlsDynamic";

                // panelRight
                this.panelRightDynamic.Dock = System.Windows.Forms.DockStyle.Fill;
                this.panelRightDynamic.Location = new System.Drawing.Point(256, 0);
                this.panelRightDynamic.Margin = new System.Windows.Forms.Padding(0);
                this.panelRightDynamic.Size = new System.Drawing.Size(292, 128);
                this.panelRightDynamic.Name = "panelRightDynamic";

                //splitter
                this.splitter1Dynamic = new System.Windows.Forms.Splitter();
                this.splitter1Dynamic.BackColor = System.Drawing.SystemColors.ControlDark;
                this.splitter1Dynamic.Location = new System.Drawing.Point(260, 0);
                this.splitter1Dynamic.Size = new System.Drawing.Size(4, 79);
                this.splitter1Dynamic.TabStop = false;


                this.labelObjectiveTitleDynamic.AutoEllipsis = true;
                this.labelObjectiveTitleDynamic.Dock = System.Windows.Forms.DockStyle.Fill;
                this.labelObjectiveTitleDynamic.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.labelObjectiveTitleDynamic.Location = new System.Drawing.Point(0, 0);
                this.labelObjectiveTitleDynamic.Size = new System.Drawing.Size(229, 79);
                this.labelObjectiveTitleDynamic.Text = nodeObject.Title;
                this.labelObjectiveTitleDynamic.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

                // panelBottomSpacer

                this.panelBottomSpacerDynamic = new System.Windows.Forms.Panel();
                this.panelBottomSpacerDynamic.Dock = System.Windows.Forms.DockStyle.Bottom;
                this.panelBottomSpacerDynamic.Location = new System.Drawing.Point(264, 69);
                this.panelBottomSpacerDynamic.Margin = new System.Windows.Forms.Padding(0);
                this.panelBottomSpacerDynamic.Size = new System.Drawing.Size(284, 10);

                this.panelBottomDynamic = new System.Windows.Forms.Panel();
                this.panelBottomDynamic.Dock = System.Windows.Forms.DockStyle.Bottom;
                this.panelBottomDynamic.Location = new System.Drawing.Point(31, 66);
                this.panelBottomDynamic.Margin = new System.Windows.Forms.Padding(0);
                this.panelBottomDynamic.Size = new System.Drawing.Size(255, 31);

                this.panelColorDynamic.BackColor = GraphUtil.GetRiskColorFromValueInverted(GraphUtil.GetNodeCalculatedValue(nodeObject.nodeID));
                this.panelColorDynamic.Controls.Add(this.labelScoreDynamic);
                this.panelColorDynamic.Dock = System.Windows.Forms.DockStyle.Left;
                this.panelColorDynamic.Location = new System.Drawing.Point(0, 0);
                this.panelColorDynamic.Size = new System.Drawing.Size(31, 97);
                this.panelColorDynamic.Name = "panelColorDynamic";

                // labelScore
                this.labelScoreDynamic.Dock = System.Windows.Forms.DockStyle.Fill;
                this.labelScoreDynamic.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.labelScoreDynamic.Location = new System.Drawing.Point(0, 0);
                this.labelScoreDynamic.Size = new System.Drawing.Size(31, 169);
                this.labelScoreDynamic.Text = Math.Round(nodeObject.Value).ToString();
                this.labelScoreDynamic.Name = "labelScoreDynamic";
                this.labelScoreDynamic.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

                // btnNodeFindDynamic
                this.btnNodeFindDynamic = new System.Windows.Forms.Button();
                this.btnNodeFindDynamic.Dock = System.Windows.Forms.DockStyle.Right;
                this.btnNodeFindDynamic.Image = ((System.Drawing.Image)(resources.GetObject("btnObjectiveFindStatic.Image")));
                toolTip1.SetToolTip(this.btnNodeFindDynamic, "Find Node on Graph");
                this.btnNodeFindDynamic.Location = new System.Drawing.Point(484, 0);
                this.btnNodeFindDynamic.Size = new System.Drawing.Size(33, 31);
                this.btnNodeFindDynamic.UseVisualStyleBackColor = true;
                this.btnNodeFindDynamic.Click += (sender, e) => this.mainForm.SelectNodeonGraph(nodeObject.nodeID);

                // btnObjectiveSettings
                this.btnNodeSettingsDynamic = new System.Windows.Forms.Button();
                this.btnNodeSettingsDynamic.Dock = System.Windows.Forms.DockStyle.Right;

                if (nodeObject.Type.ToLower() == "control")
                {
                    this.btnNodeSettingsDynamic.Image = ((System.Drawing.Image)(resources.GetObject("btnControlSettingsStatic.Image")));
                    toolTip1.SetToolTip(this.btnNodeSettingsDynamic, "Edit Control Node");
                }

                else if (nodeObject.Type.ToLower() == "objective")
                {
                    this.btnNodeSettingsDynamic.Image = ((System.Drawing.Image)(resources.GetObject("btnObjectiveSettingsStatic.Image")));
                    toolTip1.SetToolTip(this.btnNodeSettingsDynamic, "Edit Objective Node");
                }
                else if (nodeObject.Type.ToLower() == "group")
                {
                    this.btnNodeSettingsDynamic.Image = ((System.Drawing.Image)(resources.GetObject("btnGroupSettingsStatic.Image")));
                    toolTip1.SetToolTip(this.btnNodeSettingsDynamic, "Edit Group Node");
                }
                else if (nodeObject.Type.ToLower() == "actor")
                {
                    this.btnNodeSettingsDynamic.Image = ((System.Drawing.Image)(resources.GetObject("btnActorSettingsStatic.Image")));
                    toolTip1.SetToolTip(this.btnNodeSettingsDynamic, "Edit Actor Node");
                }
                else if (nodeObject.Type.ToLower() == "attack")
                {
                    this.btnNodeSettingsDynamic.Image = ((System.Drawing.Image)(resources.GetObject("btnAttackSettingsStatic.Image")));
                    toolTip1.SetToolTip(this.btnNodeSettingsDynamic, "Edit Attack Node");
                }
                else if (nodeObject.Type.ToLower() == "vulnerability")
                {
                    this.btnNodeSettingsDynamic.Image = ((System.Drawing.Image)(resources.GetObject("btnVulnerabilitySettingsStatic.Image")));
                    toolTip1.SetToolTip(this.btnNodeSettingsDynamic, "Edit Vulnerability Node");
                }
                else if (nodeObject.Type.ToLower() == "asset")
                {
                    this.btnNodeSettingsDynamic.Image = ((System.Drawing.Image)(resources.GetObject("btnAssetSettingsStatic.Image")));
                    toolTip1.SetToolTip(this.btnNodeSettingsDynamic, "Edit Asset Node");
                }
                else if (nodeObject.Type.ToLower() == "evidence")
                {
                    this.btnNodeSettingsDynamic.Image = ((System.Drawing.Image)(resources.GetObject("btnEvidenceSettingsStatic.Image")));
                    toolTip1.SetToolTip(this.btnNodeSettingsDynamic, "Edit Evidence Node");
                }
                this.btnNodeSettingsDynamic.Location = new System.Drawing.Point(451, 0);
                this.btnNodeSettingsDynamic.Size = new System.Drawing.Size(33, 31);
                this.btnNodeSettingsDynamic.UseVisualStyleBackColor = true;
                this.btnNodeSettingsDynamic.Click += (sender, e) => this.mainForm.SelectNodeandShowDetail(nodeObject.nodeID);

                this.panelBottomDynamic.Controls.Add(this.btnNodeSettingsDynamic);
                this.panelBottomDynamic.Controls.Add(this.btnNodeFindDynamic);

                this.labelScoreDynamic.Dock = System.Windows.Forms.DockStyle.Fill;
                this.labelScoreDynamic.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.labelScoreDynamic.Size = new System.Drawing.Size(31, 97);
                //this.labelScore.Text = riskValue.ToString();
                this.labelScoreDynamic.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;


                this.panelNodeDetailDynamic.Controls.Add(this.panelRightDynamic);
                this.panelNodeDetailDynamic.Controls.Add(this.splitter1Dynamic);
                this.panelNodeDetailDynamic.Controls.Add(this.panelLeftDynamic);
                this.panelNodeDetailDynamic.Controls.Add(this.panelBottomSpacerDynamic);
                this.panelNodeDetailDynamic.Controls.Add(this.panelBottomDynamic);
                this.panelNodeDetailDynamic.Controls.Add(this.panelColorDynamic);
                this.panelNodeDetailDynamic.Location = new System.Drawing.Point(3, 3);
                this.panelNodeDetailDynamic.Size = new System.Drawing.Size(PanelControlStatic.Width - System.Windows.Forms.SystemInformation.VerticalScrollBarWidth - 10, 97);
            }

            void BuildSubPanels(List<string> nodeList)
            {
                this.labelChildDynamic.Dock = System.Windows.Forms.DockStyle.Top;
                this.labelChildDynamic.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.labelChildDynamic.Location = new System.Drawing.Point(0, 0);
                this.labelChildDynamic.Size = new System.Drawing.Size(190, 20);
                this.labelChildDynamic.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
                this.flowLayoutControlsDynamic.Controls.Add(this.labelChildDynamic);

                foreach (string subPanelItem in nodeList)
                {
                    // panelControlColor 
                    this.panelChildNodeColorDynamic = new System.Windows.Forms.Panel();
                    this.panelChildNodeColorDynamic.BackColor = GraphUtil.GetRiskColorFromValueInverted(GraphUtil.GetNodeCalculatedValue(subPanelItem));
                    this.panelChildNodeColorDynamic.Dock = System.Windows.Forms.DockStyle.Left;
                    this.panelChildNodeColorDynamic.Location = new System.Drawing.Point(0, 0);
                    this.panelChildNodeColorDynamic.Size = new System.Drawing.Size(30, 28);
                    this.panelChildNodeColorDynamic.Name = "panelControlColorDynamic";

                    // labelControlScore
                    this.labelControlScoreDynamic = new System.Windows.Forms.Label();
                    this.panelChildNodeColorDynamic.Controls.Add(this.labelControlScoreDynamic);
                    this.labelControlScoreDynamic.Dock = System.Windows.Forms.DockStyle.Fill;
                    this.labelControlScoreDynamic.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    this.labelControlScoreDynamic.Location = new System.Drawing.Point(0, 0);
                    this.labelControlScoreDynamic.Size = new System.Drawing.Size(31, 169);
                    this.labelControlScoreDynamic.Text = Math.Round(GraphUtil.GetNodeCalculatedValue(subPanelItem)).ToString();
                    this.labelControlScoreDynamic.Name = "labelControlScoreDynamic";
                    this.labelControlScoreDynamic.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

                    // labelControlType 
                    this.labelChildNodeTypeDynamic = new System.Windows.Forms.Label();
                    this.labelChildNodeTypeDynamic.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    this.labelChildNodeTypeDynamic.Location = new System.Drawing.Point(10, 0);
                    this.labelChildNodeTypeDynamic.Dock = System.Windows.Forms.DockStyle.Left;
                    this.labelChildNodeTypeDynamic.Size = new System.Drawing.Size(80, 23);
                    this.labelChildNodeTypeDynamic.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
                    string nodeType = GraphUtil.GetNodeType(subPanelItem);
                    this.labelChildNodeTypeDynamic.Text = Char.ToUpper(nodeType[0]) + nodeType.Remove(0, 1);
                    this.labelChildNodeTypeDynamic.AutoEllipsis = true;

                    // labelControl 
                    this.labelChildNodeDynamic = new System.Windows.Forms.Label();
                    this.labelChildNodeDynamic.Location = new System.Drawing.Point(10, 0);
                    this.labelChildNodeDynamic.Dock = System.Windows.Forms.DockStyle.Fill;
                    this.labelChildNodeDynamic.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
                    this.labelChildNodeDynamic.Text = GraphUtil.GetNodeTitle(subPanelItem);
                    this.labelChildNodeDynamic.AutoEllipsis = true;

                    // PanelControl 
                    this.PanelChildNodeDynamic = new System.Windows.Forms.Panel();                   
                    this.PanelChildNodeDynamic.Location = new System.Drawing.Point(3, 3);
                    this.PanelChildNodeDynamic.Width = flowLayoutControlsStatic.Width - 5;
                    this.PanelChildNodeDynamic.Height = 30;

                    // btnControlFind
                    this.btnChildNodeFindDynamic = new System.Windows.Forms.Button();
                    this.btnChildNodeFindDynamic.Dock = System.Windows.Forms.DockStyle.Right;
                    this.btnChildNodeFindDynamic.Image = ((System.Drawing.Image)(resources.GetObject("btnControlFindStatic.Image")));
                    toolTip1.SetToolTip(this.btnChildNodeFindDynamic, "Find Node on Graph");
                    this.btnChildNodeFindDynamic.Location = new System.Drawing.Point(249, 0);
                    this.btnChildNodeFindDynamic.Size = new System.Drawing.Size(33, 28);
                    this.btnChildNodeFindDynamic.UseVisualStyleBackColor = true;
                    this.btnChildNodeFindDynamic.Click += (sender, e) => this.mainForm.SelectNodeonGraph(subPanelItem);

                    // btnControlSettings
                    this.btnChildNodeSettingsDynamic = new System.Windows.Forms.Button();
                    this.btnChildNodeSettingsDynamic.Dock = System.Windows.Forms.DockStyle.Right;

                    if (nodeType == "control")
                    {
                        this.btnChildNodeSettingsDynamic.Image = ((System.Drawing.Image)(resources.GetObject("btnControlSettingsStatic.Image")));
                        toolTip1.SetToolTip(this.btnChildNodeSettingsDynamic, "Edit Control Node");
                    }
                    else if (nodeType == "objective")
                    {
                        this.btnChildNodeSettingsDynamic.Image = ((System.Drawing.Image)(resources.GetObject("btnObjectiveSettingsStatic.Image")));
                        toolTip1.SetToolTip(this.btnChildNodeSettingsDynamic, "Edit Objective Node");
                    }
                    else if (nodeType == "group")
                    {
                        this.btnChildNodeSettingsDynamic.Image = ((System.Drawing.Image)(resources.GetObject("btnGroupSettingsStatic.Image")));
                        toolTip1.SetToolTip(this.btnChildNodeSettingsDynamic, "Edit Group Node");
                    }
                    else if (nodeType == "actor")
                    {
                        this.btnChildNodeSettingsDynamic.Image = ((System.Drawing.Image)(resources.GetObject("btnActorSettingsStatic.Image")));
                        toolTip1.SetToolTip(this.btnChildNodeSettingsDynamic, "Edit Actor Node");
                    }
                    else if (nodeType == "attack")
                    {
                        this.btnChildNodeSettingsDynamic.Image = ((System.Drawing.Image)(resources.GetObject("btnAttackSettingsStatic.Image")));
                        toolTip1.SetToolTip(this.btnChildNodeSettingsDynamic, "Edit Attack Node");
                    }
                    else if (nodeType == "vulnerability")
                    {
                        this.btnChildNodeSettingsDynamic.Image = ((System.Drawing.Image)(resources.GetObject("btnVulnerabilitySettingsStatic.Image")));
                        toolTip1.SetToolTip(this.btnChildNodeSettingsDynamic, "Edit Vulnerability Node");
                    }
                    else if (nodeType == "asset")
                    {
                        this.btnChildNodeSettingsDynamic.Image = ((System.Drawing.Image)(resources.GetObject("btnAssetSettingsStatic.Image")));
                        toolTip1.SetToolTip(this.btnChildNodeSettingsDynamic, "Edit Asset Node");
                    }
                    else if (nodeType == "evidence")
                    {
                        this.btnChildNodeSettingsDynamic.Image = ((System.Drawing.Image)(resources.GetObject("btnEvidenceSettingsStatic.Image")));
                        toolTip1.SetToolTip(this.btnChildNodeSettingsDynamic, "Edit Asset Node");
                    }
                    this.btnChildNodeSettingsDynamic.Location = new System.Drawing.Point(216, 0);
                    this.btnChildNodeSettingsDynamic.Size = new System.Drawing.Size(33, 28);
                    this.btnChildNodeSettingsDynamic.UseVisualStyleBackColor = true;
                    this.btnChildNodeSettingsDynamic.Click += (sender, e) => this.mainForm.SelectNodeandShowDetail(subPanelItem);

                    //PanelChildNodeDynamic
                    this.PanelChildNodeDynamic.Controls.Add(this.labelChildNodeDynamic);
                    this.PanelChildNodeDynamic.Controls.Add(this.labelChildNodeTypeDynamic);
                    this.PanelChildNodeDynamic.Controls.Add(this.panelChildNodeColorDynamic);
                    this.PanelChildNodeDynamic.Controls.Add(this.btnChildNodeSettingsDynamic);
                    this.PanelChildNodeDynamic.Controls.Add(this.btnChildNodeFindDynamic);
                    this.PanelChildNodeDynamic.Name = "PanelControlDynamic";
                    this.PanelChildNodeDynamic.Tag = subPanelItem;

                    this.flowLayoutControlsDynamic.Controls.Add(this.PanelChildNodeDynamic);

                    this.panelNodeDetailDynamic.Height = (this.flowLayoutControlsDynamic.Controls.Count * 35) + 30;
                    if (panelNodeDetailDynamic.Height < 100) panelNodeDetailDynamic.Height = 100;

                }
            }


        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (buildingNodes == true)
                return;
            else
                buildingNodes = true;

            List<string> checked_Node_items = new List<string>();
            foreach (string item in cmbNodesStatic.CheckedItems)
            {
                checked_Node_items.Add(item.ToLower());
            }

            //get each objective node on the graph

            //Build the intial Node GUID list
            _graphNodes.Clear();
            _nodeObjects.Clear();
            if (checked_Node_items.Contains("objectives"))
                _graphNodes.AddRange(GraphUtil.GetObjectiveNodes());

            if (checked_Node_items.Contains("controls"))
                _graphNodes.AddRange(GraphUtil.GetControlNodes());

            if (checked_Node_items.Contains("groups"))
                _graphNodes.AddRange(GraphUtil.GetGroupNodes());

            if (checked_Node_items.Contains("actors"))
                _graphNodes.AddRange(GraphUtil.GetActorNodes());

            if (checked_Node_items.Contains("attacks"))
                _graphNodes.AddRange(GraphUtil.GetAttackNodes());

            if (checked_Node_items.Contains("assets"))
                _graphNodes.AddRange(GraphUtil.GetAssetNodes());

            if (checked_Node_items.Contains("Vulnerabilities"))
                _graphNodes.AddRange(GraphUtil.GetVulnerabilityNodes());

            if (checked_Node_items.Contains("Evidence"))
                _graphNodes.AddRange(GraphUtil.GetEvidenceNodes());


            UpdatePanels();

        }

        private void UpdatePanels()
        {
            //now build add a panel for each object
            
            foreach (string node in _graphNodes)
            {
                nodeDetails graphObject = new nodeDetails();
                graphObject.nodeID = node;
                graphObject.Title = GraphUtil.GetNodeTitle(node);

                //Process filter text
                string searchText = textBoxFilterTextStatic.Text.Trim();
                if (searchText != "")
                {
                    if (graphObject.Title.IndexOf(searchText, StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        graphObject.Value = GraphUtil.GetNodeCalculatedValue(node);
                        graphObject.Type = GraphUtil.GetNodeType(node);
                        _nodeObjects.Add(graphObject);
                    }
                }
                else
                {
                    graphObject.Value = GraphUtil.GetNodeCalculatedValue(node);
                    graphObject.Type = GraphUtil.GetNodeType(node);
                    _nodeObjects.Add(graphObject);
                }
            }


            _orderedNodeObjects.Clear();
            _orderedNodeObjects1.Clear();

            if (cbSortBy.SelectedIndex == 0)
                _orderedNodeObjects = _nodeObjects.OrderBy(x => x.Value).ToList();
            else if (cbSortBy.SelectedIndex == 1)
                _orderedNodeObjects = _nodeObjects.OrderByDescending(x => x.Value).ToList();
            else if (cbSortBy.SelectedIndex == 2)
            {
                var groups = _nodeObjects.GroupBy(s => s.Type);
                foreach (var group in groups)
                    _orderedNodeObjects.AddRange(group.OrderByDescending(x => x.Value).ToList());
            }

            //Show the Panels

            totalRecords = _orderedNodeObjects.Count;
            if (totalRecords > 20)
            {
                //Need to pagenate results

                //Show the first 20 records
                startRecord = 1;
                endRecord = showRecords;

                for (int i = startRecord - 1; i < endRecord - 1; i++)
                    _orderedNodeObjects1.Add(_orderedNodeObjects[i]);

                UpdateRecords();

                this.PanelControlStatic.Controls.Clear();
                AddToNodeList(_orderedNodeObjects1);
                buildingNodes = false;
            }
            else
            {
                DisableAllNavigationButtons();
                labelRecords.Text = "1 to " + totalRecords.ToString() + " of " + totalRecords.ToString();
                this.PanelControlStatic.Controls.Clear();
                AddToNodeList(_orderedNodeObjects);
                buildingNodes = false;

            }
        }

        private void UpdateRecords()
        {
            labelRecords.Text = (startRecord).ToString() + " to " + (endRecord).ToString() + " of " + totalRecords.ToString();
            if (endRecord >= totalRecords)
            {
                DisableForwardNavigationButtons();
                EnableBackwardNavigationButtons();
            }
            else
                EnableForwardNavigationButtons();
            if (startRecord == 1)
            {
                if (totalRecords > endRecord)
                    EnableForwardNavigationButtons();
                else
                    DisableForwardNavigationButtons();
                DisableBackwardNavigationButtons();
            }
            else
                EnableBackwardNavigationButtons();

            _orderedNodeObjects1.Clear();
            for (int i = startRecord - 1; i < endRecord - 1; i++)
                _orderedNodeObjects1.Add(_orderedNodeObjects[i]);
           
            AddToNodeList(_orderedNodeObjects1);
            buildingNodes = false;
        }


        private void flowLayoutControls_Resize(object sender, EventArgs e)
        {
            if (sender is FlowLayoutPanel)
            {
                var flowLayout = (FlowLayoutPanel)sender;
                flowLayout.SuspendLayout();
                
                foreach (Control mycontrol in flowLayout.Controls)
                {
                    if (mycontrol.Name == "nodeDetail")
                    {
                        mycontrol.Width = PanelControlStatic.Width - System.Windows.Forms.SystemInformation.VerticalScrollBarWidth - 10;
                    }                    
                }
                flowLayout.ResumeLayout();
            }
        }

        private void flowpanelControls_Resize(object sender, EventArgs e)
        {
            if (sender is FlowLayoutPanel)
            {
                var flowLayout = (FlowLayoutPanel)sender;
                flowLayout.SuspendLayout();

                foreach (Control mycontrol in flowLayout.Controls)
                {

                    if (mycontrol.Name == "PanelControlDynamic")
                    {
                        mycontrol.Width = flowLayout.Width - 4;
                    }
                }
                flowLayout.ResumeLayout();
            }

        }

        private void textBoxFilterText_TextChanged(object sender, EventArgs e)
        {
           
        }

        public void updateControlPanel(string nodeID)
        {
            if (PanelControlStatic.HasChildren == false)
                return;

            nodeDetails objectiveObject = new nodeDetails();
            foreach (System.Windows.Forms.Control controlPanel in PanelControlStatic.Controls)
            { 
                foreach (System.Windows.Forms.Control controlItem in controlPanel.Controls)
                {
                    if (controlItem.Name == "panelRightDynamic")
                    {
                        foreach (System.Windows.Forms.Control controlItem2 in controlItem.Controls)
                        {
                            if (controlItem2.Name == "flowLayoutControlsDynamic")
                            {
                                foreach (System.Windows.Forms.Control controlItem3 in controlItem2.Controls)
                                {
                                    if (controlItem3.Name == "PanelControlDynamic" && controlItem3.Tag.ToString() == nodeID)  //Check this is the right control to update
                                    {
                                        objectiveObject = controlPanel.Tag as nodeDetails;
                                        foreach (System.Windows.Forms.Control controlItem4 in controlItem3.Controls)
                                        {
                                            if (controlItem4.Name == "panelControlColorDynamic")
                                            {
                                                controlItem4.BackColor = GraphUtil.GetRiskColorFromValueInverted(GraphUtil.GetNodeCalculatedValue(nodeID));

                                                foreach (System.Windows.Forms.Control controlItem5 in controlItem4.Controls)
                                                {
                                                    if (controlItem5.Name == "labelControlScoreDynamic")
                                                    {
                                                        controlItem5.Text = GraphUtil.GetNodeCalculatedValue(nodeID).ToString();
                                                        break;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }      

                            }

                        }
  
                    }

                }

            }
            if (objectiveObject != null)
            {
                updateObjectivePanel(objectiveObject.nodeID);
            }
        }

        public void updateObjectivePanel(string nodeID)
        {
            if (PanelControlStatic.HasChildren == false)
                return;
            //find the panel that relates the the nodeID
            foreach (System.Windows.Forms.Control controlPanel in PanelControlStatic.Controls)
            {
                nodeDetails objectiveObject = controlPanel.Tag as nodeDetails;
                if (objectiveObject != null)
                {
                    if (objectiveObject.nodeID == nodeID) // found the panel
                    {
                        objectiveObject.Value = (GraphUtil.GetNodeCalculatedValue(nodeID));
                        // now need to update the panel
                        foreach (System.Windows.Forms.Control itemPanel in controlPanel.Controls)
                        {
                            if (itemPanel.Name == "panelColorDynamic")
                                itemPanel.BackColor = GraphUtil.GetRiskColorFromValueInverted(objectiveObject.Value);
                            //now update the label
                            foreach (System.Windows.Forms.Control itemLabel in itemPanel.Controls)
                            {
                                if (itemLabel.Name == "labelScoreDynamic")
                                    itemLabel.Text = objectiveObject.Value.ToString();
                                break; 

                            }

                        }
                    }
                }
            }

            PanelControlStatic.SuspendLayout();
            foreach (System.Windows.Forms.Panel controlPanel in PanelControlStatic.Controls)
            {
                nodeDetails objectiveObject = controlPanel.Tag as nodeDetails;
                if (objectiveObject != null)
                {
                    if (objectiveObject.Title.IndexOf(textBoxFilterTextStatic.Text, StringComparison.OrdinalIgnoreCase) >= 0)
                        controlPanel.Visible = true;
                    else
                        controlPanel.Visible = false;
                }
            }
            PanelControlStatic.ResumeLayout();
        }

        private void EnableAllNavigationButtons()
        {
            btnFirst.Enabled = true; 
            btnNext.Enabled = true; 
            btnLast.Enabled = true; 
            btnPrev.Enabled = true;
        }

        private void EnableForwardNavigationButtons()
        {
            btnLast.Enabled = true;
            btnNext.Enabled = true;
        }

        private void EnableBackwardNavigationButtons()
        {
            btnFirst.Enabled = true;
            btnPrev.Enabled = true;
        }

        private void DisableForwardNavigationButtons()
        {
            btnLast.Enabled = false;
            btnNext.Enabled = false;
        }

        private void DisableBackwardNavigationButtons()
        {
            btnFirst.Enabled = false;
            btnPrev.Enabled = false;
        }

        private void DisableAllNavigationButtons()
        {
            btnFirst.Enabled = false;
            btnNext.Enabled = false;
            btnLast.Enabled = false;
            btnPrev.Enabled = false;
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            startRecord = startRecord - showRecords;
            endRecord = startRecord + showRecords;
            if (startRecord < 1)
            {
                startRecord = 1;
                endRecord = startRecord + showRecords;
            }

            if (endRecord > _orderedNodeObjects.Count)
                endRecord = _orderedNodeObjects.Count;

            UpdateRecords();

            
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            startRecord = startRecord + showRecords;
            endRecord = endRecord + showRecords;
            if (endRecord > _orderedNodeObjects.Count)
                endRecord = _orderedNodeObjects.Count;

            UpdateRecords();
        }

        private void btnFirst_Click(object sender, EventArgs e)
        {
            startRecord = 1;
            endRecord = startRecord + showRecords;
            if (endRecord > totalRecords)
                endRecord = totalRecords;

            UpdateRecords();

        }

        private void btnLast_Click(object sender, EventArgs e)
        {

            endRecord = totalRecords;
            startRecord = endRecord - showRecords;
            if (startRecord < 1)
                startRecord = 1;

            UpdateRecords();

        }

        private void panelTopStatic_Resize(object sender, EventArgs e)
        {
            if (panelTopStatic.Width < 580)
            {
                double initialRatio = 5.8;
                double currentRatio = (panelTopStatic.Width - btnRefeshStatic.Width) / 100d;
               
                label1Static.Width = (int)(44 / initialRatio * currentRatio);
                cmbNodesStatic.Width = (int)(160 / initialRatio * currentRatio);
                label2Static.Width = (int)(60 / initialRatio * currentRatio);
                cbSortBy.Width = (int)(83 / initialRatio * currentRatio);
                label3Static.Width = (int)(40 / initialRatio * currentRatio);
                textBoxFilterTextStatic.Width = (int)(160 / initialRatio * currentRatio);
            }
            else
            {
                label1Static.Width = 44;
                cmbNodesStatic.Width = 160;
                label2Static.Width = 60;
                cbSortBy.Width = 83;
                label3Static.Width = 40;
                textBoxFilterTextStatic.Width = 160;
            }
        }
    }

    public class nodeDetails
    {
        public string nodeID { get; set; }
        public string Title { get; set; }
        public double Value { get; set; }
        public string Type { get; set; }

    }
}
