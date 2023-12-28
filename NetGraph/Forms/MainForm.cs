using CefSharp;
using CefSharp.WinForms;
using CyConex.Chromium;
using CyConex.Graph;
using CyConex.Helpers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Syncfusion.Windows.Forms.Tools;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;
using System.Net.Http;
using System.Net.Http.Headers;
using Syncfusion.Windows.Forms.Tools.XPMenus;
using System.Drawing.Printing;
using Syncfusion.DocIO.DLS;
using Syncfusion.DocIO;
using Syncfusion.Windows.Forms.Chart;
using CyConex.API;
using Syncfusion.Windows.Forms.Gauge;
using Path = System.IO.Path;
using Color = System.Drawing.Color;
using FontFamily = System.Drawing.FontFamily;
using Syncfusion.Data.Extensions;
using System.Threading;
using System.ComponentModel;
using CyConex.Forms;
using CyConex.Resources;
using Syncfusion.Runtime.Serialization;
using CyConex.Modals;
using System.Windows.Input;
using CefSharp.Handler;
using ContextMenuHandler = CefSharp.Handler.ContextMenuHandler;
using KeyboardHandler = CyConex.Chromium.KeyboardHandler;
using static System.ComponentModel.Design.ObjectSelectorEditor;
using EnvDTE;

namespace CyConex
{

    public partial class MainForm : RibbonForm
    {
        public ApplicationSettings _settings;
        private BoundObjectV2 _mainMapBoundObjectV2;
        public ChromiumWebBrowser _browser;
        public GraphProperties _graphProperties;

        //public ProgressDialog progressDialog = new ProgressDialog();
        public WebBrowserForm wbf = new WebBrowserForm();

        private bool _unsavedChangesInternal;
        private SearchDialogModal searchDlg;
        public ConcurrentDictionary<string, Node> _selectedNodes = new ConcurrentDictionary<string, Node>();
        public ConcurrentDictionary<string, Edge> _selectedEdges = new ConcurrentDictionary<string, Edge>();
        //Used for store log events while we recalculate all on cytoscape side

        private bool _newFile = true;
        private string _fileName = "layout.graph";
        private string _manualSourceNodeID = String.Empty;
        private string tmp_graph_file_path = "";
        private bool tmp_graph_file_auto_save_flag = false;

        public string SelectedNodeedgeStrengthValue;
        public string SelectedNodecontrolBaseScore;
        private Dictionary<string, Node> copyNodes = new Dictionary<string, Node>();
        public List<Node> NodeListViewItems { get; set; }
        public ImageList nodeImageList = new ImageList();
        private Nodes _nodes = new Nodes();
        private List<Node> _suggested_nodes = new List<Node>();
        public string _selected_node_id = "";
        public string _selected_node_master_id = "";
        public string _selected_main_tab = "graph";
        public string _selected_graph_sub_tab = "details";
        public string _selected_node_sub_tab = "framework";
        public string _selected_edge_sub_tab = "edge";
        public bool _user_loggedin = false;
        public JArray _default_edge_relationship = new JArray();
        public bool _set_default_edge_relationship = false;
        private Bitmap bitmap;

        public JArray node_categories = new JArray();

        private GraphCloud _graph_cloud = new GraphCloud();
        private GraphFile _graph_file = new GraphFile();
        private string default_edge_relationship_strength_width = "1";
        private string default_edge_relationship_strength_color = "rgb(0,0,0)";
        public string ApplicationTitle = "";

        private bool StopCalcs = false;
        private int CalcLoops = 0;
        private BackgroundWorker backgroundWorker;

        public CalcLogForm calcLogForm = new CalcLogForm();
        public RiskPanelsForm riskPanelsForm;
        public NodesForm nodesForm;
        public NodeDistributionsForm nodeDistributionsForm;
        public NodePropertyForm nodePropertyForm;
        public NodeRepositoryForm nodeRepositoryForm;
        public ComplianceForm complianceForm;
        public NodeDataForm nodeDataForm;
        RiskDistrubutionsForm HeatMapForm;
        public ReportUtility reportUtility;
        private RecoveryFileSaveModal recoveryFileSaveModal = new RecoveryFileSaveModal();

        public bool IsShiftKey = false;
        public bool IsF1key = false;
        public bool NodeBoxingInProgress = false;
        public int countOfselectedNodesOnGraph = 0;
        private int formWidthLimit = 920;

        private CacheHelper _cacheObj = new CacheHelper();
        public MainForm()
        {
            InitializeComponent();

            _graphProperties = new GraphProperties();
            NodeListViewItems = new List<Node>();

            //Setup refrenced forms
            this.riskPanelsForm = new RiskPanelsForm(this);
            this.nodesForm = new NodesForm(this);
            this.HeatMapForm = new RiskDistrubutionsForm(this);
            this.nodeDistributionsForm = new NodeDistributionsForm();
            this.nodePropertyForm = new NodePropertyForm(this);
            this.nodeRepositoryForm = new NodeRepositoryForm();
            this.complianceForm = new ComplianceForm(this);
            this.nodeDataForm = new NodeDataForm(this);
            this.reportUtility = new ReportUtility();
            this.searchDlg = new SearchDialogModal();

            this.nodeRepositoryForm.ribbonForm = this;
            SetUpInitialVisuals();
            GraphCalcs.Init(this);
        }


        public Node GetNodeFromListView(string node_id)
        {
            Node node = new Node();
            for (int i = 0; i < NodeListViewItems.Count; i++)
            {
                Node tmpItem = NodeListViewItems[i];
                if (tmpItem.ID == node_id)
                {
                    node = tmpItem;
                    break;
                }
            }
            return node;
        }

        public void AddNodeListItem(Node node, bool isNew = true, bool flag = false)
        {
            if (isNew)
            {
                NodeListViewItems.Add(node);
            }
            else
            {
                for (int i = 0; i < NodeListViewItems.Count; i++)
                {
                    Node tmpItem = NodeListViewItems[i];
                    if (tmpItem.ID == node.ID)
                    {
                        NodeListViewItems[i].Type = node.Type;
                        NodeListViewItems[i].Title = node.Title;
                        NodeListViewItems[i].TitleSize = node.TitleSize;
                        NodeListViewItems[i].TitleTextColor = node.TitleTextColor;
                        NodeListViewItems[i].NodeTitlePosition = node.NodeTitlePosition;
                        NodeListViewItems[i].ImagePath = node.ImagePath;
                        NodeListViewItems[i].Note = node.Note;
                        NodeListViewItems[i].frameworkReference = node.frameworkReference;
                        NodeListViewItems[i].frameworkName = node.frameworkName;
                        NodeListViewItems[i].ControlFrameworkVersion = node.ControlFrameworkVersion;
                        NodeListViewItems[i].Category = node.Category;
                        NodeListViewItems[i].Domain = node.Domain;
                        NodeListViewItems[i].Level = node.Level;
                        NodeListViewItems[i].objectiveTargetType = node.objectiveTargetType;
                        NodeListViewItems[i].objectiveTargetValue = node.objectiveTargetValue;
                        NodeListViewItems[i].InherentStrengthValue = node.InherentStrengthValue;
                        NodeListViewItems[i].ImplementedStrength = node.ImplementedStrength;
                        NodeListViewItems[i].ImplementedStrengthValue = node.ImplementedStrengthValue;
                        NodeListViewItems[i].description = node.description;
                        NodeListViewItems[i].MetaTags = node.MetaTags;
                        NodeListViewItems[i].Shape = node.Shape;
                        NodeListViewItems[i].Color = node.Color;
                        NodeListViewItems[i].BorderColor = node.BorderColor;
                        NodeListViewItems[i].NodeImageData = node.NodeImageData;
                        break;
                    }
                }
            }

            Utility.UpdateNodeListFileData(NodeListViewItems);

            if (flag == true)
            {
                nodeRepositoryForm.UpdateNodeListView(NodeListViewItems);
            }
        }

        public void LoadNodeListViewFileData()
        {
            JArray nodeLists = Utility.LoadNodeViewList();

            NodeListViewItems.Clear();
            for (int i = 0; i < nodeLists.Count; i++)
            {
                JObject tmp = (JObject)nodeLists[i];
                Node tmp_node = new Node();
                tmp_node = Utility.JObjectToNode(tmp);
                NodeListViewItems.Add(tmp_node);

            }
            nodeRepositoryForm.UpdateNodeListView(NodeListViewItems);
        }

        public void LoadNodeListViewData()
        {
            Graph.Utility.SaveAuditLog("LoadNodeListViewData", "Function Call", "", "", $"");
            JArray arr = NodeAPI.GetRepoNodeList();
            NodeListViewItems.Clear();

            for (int i = 0; i < arr.Count; i++)
            {
                JObject tmp = arr[i] as JObject;
                string nodeGUID = tmp["nodeGUID"].ToString();
                Node tmp_node = new Node();
                tmp_node.ID = nodeGUID;
                tmp_node.masterID = nodeGUID;

                JObject nodeMeta = NodeAPI.GetRepoNodeMeta(nodeGUID);
                if (nodeMeta != null)
                {
                    JObject nodeGraph = NodeAPI.GetRepoNodeGraph(nodeGUID);
                    JObject nodeVisual = NodeAPI.GetRepoNodeVisual(nodeGUID);
                    JObject nodeNote = NodeAPI.GetRepoNodeNote(nodeGUID);
                    JObject nodeFramework = NodeAPI.GetRepoNodeFramework(nodeGUID);

                    tmp_node = GraphUtil.SetNodeFullData(nodeGUID, nodeMeta, nodeGraph, nodeVisual, nodeNote, nodeFramework);
                    NodeListViewItems.Add(tmp_node);
                }
            }
            NodeRepository.SetRepositoryList(NodeListViewItems);
        }

        public void RemoveListItem(Node item, bool flag = false)
        {
            NodeListViewItems.Remove(item);
            Utility.UpdateNodeListFileData(NodeListViewItems);
            if (flag == true)
            {
                nodeRepositoryForm.UpdateNodeListView(NodeListViewItems);
            }
        }

        private bool _hasUnsavedChanges
        {
            get { return _unsavedChangesInternal; }
            set
            {
                if (_unsavedChangesInternal != value)
                {
                    //TODO: Save state for redo and undo
                    _unsavedChangesInternal = value;
                }
            }
        }
        private async Task UpdateAllSelectedNodes()
        {
            Console.WriteLine("MainForm > UpdateAllSelectedNodes");

            var selectedNodes = await _browser.EvaluateScriptAsync("getSelectedNodesAsJSON();");
            if (selectedNodes.Success && selectedNodes.Result.ToString() != "[]")
            {
                var tmpObj = JArray.Parse(selectedNodes.Result.ToString());
                foreach (var tmpNode in tmpObj)
                {
                    Node node = Node.FromJson(tmpNode.ToString());
                    if (node != null)
                    {
                        _selectedNodes.TryAdd(node.ID, node);
                    }
                }
            }

        }

        private async Task UpdateAllSelectedEdges()
        {
            Console.WriteLine("MainForm > UpdateAllSelectedEdges");

           
            var selectedEdges = await _browser.EvaluateScriptAsync("getSelectedEdgesAsJSON();");
            if (selectedEdges.Success && selectedEdges.Result.ToString() != "[]")
            {
                var tmpObj = JArray.Parse(selectedEdges.Result.ToString());
                foreach (var tmpEdge in tmpObj)
                {
                    Edge edge = Edge.FromJson(tmpEdge.ToString());
                    if (edge != null)
                    {
                        _selectedEdges.TryAdd(edge.ID, edge);
                    }
                }
            }
        }

        public async Task UpdateFirstSelectedNode()
        {
            Console.WriteLine("MainForm > UpdateFirstSelectedNode");
            if (!_selectedNodes.Any() && countOfselectedNodesOnGraph == 1) // Using .Any() instead of .Count() == 0 for efficiency
            {
                var selectedNodes = await _browser.EvaluateScriptAsync("getFirstSelectedNodeAsJSON();");
                if (selectedNodes.Success && selectedNodes.Result.ToString() != "[]")
                {
                    Node node = Node.FromJson(selectedNodes.Result.ToString());
                    if (node != null)
                    {
                        _selectedNodes.TryAdd(node.ID, node);
                    }
                }
            }
        }


        private async Task UpdateToolbarEnableForNodeEdgeAsync(int selectedNodeCount, string node_type = "none")
        {

            bool tmp_flag = false;
            bool edge_flag = false;

            if (_selectedNodes.Count > 0 || _selectedEdges.Count > 0 || selectedNodeCount > 0)
            {
                this.InvokeIfNeed(() =>
                {
                    toolBtnDeleteElement.Enabled = true;
                });
                ;
            }
            else
            {
                this.InvokeIfNeed(() =>
                {
                    toolBtnDeleteElement.Enabled = false;
                });

            }

            if (_selectedNodes.Count > 0 || selectedNodeCount > 0)
            {
                this.InvokeIfNeed(() =>
                {
                    toolBtnNodePlus.Enabled = true;
                    toolBtnNodeMinus.Enabled = true;
                });

            }
            else
            {
                this.InvokeIfNeed(() =>
                {
                    toolBtnNodePlus.Enabled = false;
                    toolBtnNodeMinus.Enabled = false;
                });
                ;
            }

            if ((_selectedNodes.Count == 1 || selectedNodeCount == 1) && node_type != "shape")
            {
                tmp_flag = true;
                if (_selectedNodes.Count() == 0) await UpdateFirstSelectedNode();
                Node node = _selectedNodes.ElementAt(0).Value;
                if (node.ImagePath == "" || node.NodeImageData == "")
                {
                    node_type = "shape";
                }
                else
                {
                    node_type = "image";
                }
                this.InvokeIfNeed(() =>
                {
                   
                    toolBtnShapePanel.Enabled = node_type == "shape" ? true : false;
                    toolComboShape.Enabled = node_type == "shape" ? true : false;
                    toolBtnFillColor.Enabled = node_type == "shape" ? true : false;
                    toolBtnFillPanel.Enabled = node_type == "shape" ? true : false;
                    toolBtnBorderPanel.Enabled = node_type == "shape" ? true : false;
                    toolBtnBorderColor.Enabled = node_type == "shape" ? true : false;
                    toolBtnBorderWidth.Enabled = node_type == "shape" ? true : false;
                });
            }
            else if (_selectedEdges.Count > 0)
            {
                Edge edge = _selectedEdges.ElementAt(0).Value as Edge;
                ShowRequiredRiskPanels(edge.ID);
                nodeDistributionsForm.UpdateNodeAssessmentInfo(null);
                tmp_flag = true;
                edge_flag = true;
            }


            if (_selectedNodes.Count > 0 || selectedNodeCount > 0)
            {
                tmp_flag = true;
            }

            this.InvokeIfNeed(() =>
            {
                toolBtnSetNodeImage.Enabled = node_type == "image" || node_type == "shape" || node_type == "none" ? tmp_flag : false;
                toolBtnClearNodeImage.Enabled = tmp_flag;

                toolStripDrawnEdgeColor.Enabled = drawEdges_ToolStrip.Checked ? true : edge_flag;
                toolStripeDrawnEdgeWidth.Enabled = drawEdges_ToolStrip.Checked ? true : edge_flag;

                toolBtnCopy.Enabled = tmp_flag;
                toolBtnFontPanel.Enabled = tmp_flag;
                toolBtnFontFamily.Enabled = tmp_flag;
                toolBtnFontSize.Enabled = tmp_flag;
                toolBtnFontSizePlus.Enabled = tmp_flag;
                toolBtnFontSizeMinus.Enabled = tmp_flag;
                toolBtnFontBold.Enabled = tmp_flag;
                toolBtnFontItalic.Enabled = tmp_flag;
                toolBtnFontColor.Enabled = tmp_flag;
                toolBtnFontStylePanel.Enabled = tmp_flag;
                toolBtnLabelPosPanel.Enabled = tmp_flag;
                toolBtnTextTop.Enabled = tmp_flag;
                toolBtnTextCenter.Enabled = tmp_flag;
                toolBtnTextBottom.Enabled = tmp_flag;
                toolBtnTextLeft.Enabled = tmp_flag;
                toolBtnTextRight.Enabled = tmp_flag;
                toolBtnNodePlus.Enabled = tmp_flag;
                toolBtnNodeMinus.Enabled = tmp_flag;
            });

                           
        }

        public void SetChartValueForRisk(ChartControl chart, double scoreValue)
        {
            Color color = new Color();
            if (scoreValue > 90)
            {
                color = Color.Red;
            }
            else if (scoreValue > 75)
            {
                color = Color.OrangeRed;
            }
            else if (scoreValue > 50)
            {
                color = Color.DarkOrange;
            }
            else if (scoreValue > 25)
            {
                color = Color.Yellow;
            }
            else if (scoreValue > 10)
            {
                color = Color.GreenYellow;
            }
            else
            {
                color = Color.Green;
            }

            chart.BeginUpdate();
            if (chart.Series.Count == 0)
            {
                ChartSeries sr = new ChartSeries("series1");
                sr.Points.Add(0, scoreValue);
                sr.Type = ChartSeriesType.Bar;
                sr.Styles[0].Interior = new Syncfusion.Drawing.BrushInfo(color);
                chart.Series.Add(sr);
            }
            else
            {
                ChartSeries sr = chart.Series[0];
                sr.Points.Clear();
                sr.Points.Add(0, scoreValue);
                sr.Styles[0].Interior = new Syncfusion.Drawing.BrushInfo(color);
            }
            chart.EndUpdate();
        }

        public void SetGaugeValue(LinearGauge gauge, double scoreValue)
        {
            int onWidth = 0;
            int offWidth = 0;

            if (gauge.LinearFrameType == LinearFrameType.Horizontal)
            {
                onWidth = 15;
                offWidth = 5;
            }

            if (gauge.LinearFrameType == LinearFrameType.Vertical)
            {
                onWidth = 25;
                offWidth = 15;
            }

            gauge.Ranges[5].StartWidth = offWidth;
            gauge.Ranges[5].EndWidth = offWidth;
            gauge.Ranges[4].StartWidth = offWidth;
            gauge.Ranges[4].EndWidth = offWidth;
            gauge.Ranges[3].StartWidth = offWidth;
            gauge.Ranges[3].EndWidth = offWidth;
            gauge.Ranges[2].StartWidth = offWidth;
            gauge.Ranges[2].EndWidth = offWidth;
            gauge.Ranges[1].StartWidth = offWidth;
            gauge.Ranges[1].EndWidth = offWidth;
            gauge.Ranges[0].StartWidth = offWidth;
            gauge.Ranges[0].EndWidth = offWidth;

            if (scoreValue > 90)
            {
                gauge.Ranges[5].StartWidth = onWidth;
                gauge.Ranges[5].EndWidth = onWidth;
                gauge.ThemeStyle.ValueIndicatorColor = gauge.Ranges[5].Color;
            }
            else if (scoreValue > 75)
            {
                gauge.Ranges[4].StartWidth = onWidth;
                gauge.Ranges[4].EndWidth = onWidth;
                gauge.ThemeStyle.ValueIndicatorColor = gauge.Ranges[4].Color;
            }
            else if (scoreValue > 50)
            {
                gauge.Ranges[3].StartWidth = onWidth;
                gauge.Ranges[3].EndWidth = onWidth;
                gauge.ThemeStyle.ValueIndicatorColor = gauge.Ranges[3].Color;
            }
            else if (scoreValue > 25)
            {
                gauge.Ranges[2].StartWidth = onWidth;
                gauge.Ranges[2].EndWidth = onWidth;
                gauge.ThemeStyle.ValueIndicatorColor = gauge.Ranges[2].Color;
            }
            else if (scoreValue > 10)
            {
                gauge.Ranges[1].StartWidth = onWidth;
                gauge.Ranges[1].EndWidth = onWidth;
                gauge.ThemeStyle.ValueIndicatorColor = gauge.Ranges[1].Color;
            }
            else
            {
                gauge.Ranges[0].StartWidth = onWidth;
                gauge.Ranges[0].EndWidth = onWidth;
                gauge.ThemeStyle.ValueIndicatorColor = gauge.Ranges[0].Color;
            }

            gauge.Value = (float)scoreValue;

        }

        public void HideUnnecessaryRiskPanels(Panel panelName)
        {

            if (this.riskPanelsForm.panelRiskActor.Visible && panelName != this.riskPanelsForm.panelRiskActor)
                this.riskPanelsForm.panelRiskActor.Visible = false;

            if (this.riskPanelsForm.panelRiskAttack.Visible && panelName != this.riskPanelsForm.panelRiskAttack)
                this.riskPanelsForm.panelRiskAttack.Visible = false;


            if (this.riskPanelsForm.panelRiskVulnerability.Visible && panelName != this.riskPanelsForm.panelRiskVulnerability)
                this.riskPanelsForm.panelRiskVulnerability.Visible = false;

            if (this.riskPanelsForm.panelRiskAsset.Visible && panelName != this.riskPanelsForm.panelRiskAsset)
                this.riskPanelsForm.panelRiskAsset.Visible = false;

            if (this.riskPanelsForm.panelRiskControl.Visible && panelName != this.riskPanelsForm.panelRiskControl)
                this.riskPanelsForm.panelRiskControl.Visible = false;

            if (this.riskPanelsForm.panelRiskEdge.Visible && panelName != this.riskPanelsForm.panelRiskEdge)
                this.riskPanelsForm.panelRiskEdge.Visible = false;

        }

        public void ShowNodeRiskPanel(Panel panelName)
        {

            // Show the requested panel
            if (!panelName.Visible)
            {
                panelName.Dock = DockStyle.Fill;
                panelName.Visible = true;
                panelName.BringToFront();
            }

        }

        public void HideNodeRiskPanels()
        {
            this.riskPanelsForm.panelRiskActor.Visible = false;
            this.riskPanelsForm.panelRiskAttack.Visible = false;
            this.riskPanelsForm.panelRiskVulnerability.Visible = false;
            this.riskPanelsForm.panelRiskAsset.Visible = false;
            this.riskPanelsForm.panelRiskControl.Visible = false;
            this.riskPanelsForm.panelRiskEdge.Visible = false;
        }

        public void ShowRequiredRiskPanels(string nodeGUID)
        {
            Console.WriteLine("MainForm > ShowRequiredRiskPanels");
            try
            {
                string nodeType = "none";
                nodeType = GraphUtil.GetNodeType(nodeGUID).ToLower();
                if (nodeType == "notfound")
                    if (GraphUtil.IsEdge(nodeGUID))
                        nodeType = "edge";


                switch (nodeType)
                {
                    case "actor":
                        this.InvokeIfNeed(() =>
                        {
                            if (this.riskPanelsForm.lblActorScore.Text == "N/A" || this.riskPanelsForm.lblactorMitigatedScore.Text == "N/A")
                                UpdateNodeRiskPanelValues(nodeGUID);
                            ShowNodeRiskPanel(this.riskPanelsForm.panelRiskActor);
                            HideUnnecessaryRiskPanels(this.riskPanelsForm.panelRiskActor);
                        });
                        break;

                    case "attack":
                        this.InvokeIfNeed(() =>
                        {  
                            if (this.riskPanelsForm.lblattackScore.Text == "N/A" || this.riskPanelsForm.lblattackMitigatedScore.Text == "N/A" || this.riskPanelsForm.lblthreatScore.Text == "N/A")
                                UpdateNodeRiskPanelValues(nodeGUID);
                            ShowNodeRiskPanel(this.riskPanelsForm.panelRiskAttack);
                            HideUnnecessaryRiskPanels(this.riskPanelsForm.panelRiskAttack);
                        });
                        break;

                    case "asset":
                    case "asset-group":
                        this.InvokeIfNeed(() =>
                        {
                            if (this.riskPanelsForm.lblassetScore.Text == "N/A" || this.riskPanelsForm.lblassetMitigatedScore.Text == "N/A" || this.riskPanelsForm.lblimpactScore.Text == "N/A")
                                UpdateNodeRiskPanelValues(nodeGUID);
                            ShowNodeRiskPanel(this.riskPanelsForm.panelRiskAsset);
                            HideUnnecessaryRiskPanels(this.riskPanelsForm.panelRiskAsset);
                            if (this.dockingManager1.GetDockVisibility(this.complianceForm.panelComplianceContainer) == true)
                                complianceForm.UpdateCompliancePanel(nodeGUID);
                        });
                        break;

                    case "vulnerability":
                        this.InvokeIfNeed(() =>
                        {    
                            if (this.riskPanelsForm.lblvulnerabilityScore.Text == "N/A" || this.riskPanelsForm.lblvulnerabilityMitigatedScore.Text == "N/A" || this.riskPanelsForm.lblvulnerabilityLikelihoodScore.Text == "N/A")
                                UpdateNodeRiskPanelValues(nodeGUID);
                            ShowNodeRiskPanel(this.riskPanelsForm.panelRiskVulnerability);
                            HideUnnecessaryRiskPanels(this.riskPanelsForm.panelRiskVulnerability);
                        });
                        break;

                    case "control":
                        this.InvokeIfNeed(() =>
                        {  
                            if (this.riskPanelsForm.lblControlStrengthValue.Text == "N/A" || this.riskPanelsForm.lblControlImplementationValue.Text == "N/A" || this.riskPanelsForm.lblControlValue.Text == "N/A")
                                UpdateNodeRiskPanelValues(nodeGUID);
                            ShowNodeRiskPanel(this.riskPanelsForm.panelRiskControl);
                            HideUnnecessaryRiskPanels(this.riskPanelsForm.panelRiskControl);
                        });
                        break;
                    case "objective":
                        this.InvokeIfNeed(() =>
                        {
                            if (this.dockingManager1.GetDockVisibility(this.complianceForm.panelComplianceContainer) == true)
                                complianceForm.UpdateCompliancePanel(nodeGUID);
                        });
                        break;

                    case "edge":
                        this.InvokeIfNeed(() =>
                        {
                            ShowNodeRiskPanel(this.riskPanelsForm.panelRiskEdge);
                            HideUnnecessaryRiskPanels(this.riskPanelsForm.panelRiskEdge); 
                        });
                        break;

                    case "evidence":
                        //Todo
                        //Console.Beep();
                        break;

                    case "none":
                        this.InvokeIfNeed(() =>
                        {
                            HideUnnecessaryRiskPanels(null);
                        });
                        break;

                    default:
                        this.InvokeIfNeed(() =>
                        {
                            HideUnnecessaryRiskPanels(null);
                        });
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"EXEPTION: MainForm > ShowRequiredRiskPanels : {ex.Message}");
            }
        }

        public void UpdateTabs(bool flag = true)
        {
            Console.WriteLine("Mainform > UpdateTabs");
            try
            {
                this.InvokeIfNeed(() =>
                {
                   
                    if (_selectedNodes.Count > countOfselectedNodesOnGraph)
                        countOfselectedNodesOnGraph = _selectedNodes.Count;

                    UpdateToolbarEnableForNodeEdgeAsync(countOfselectedNodesOnGraph);
                    bool graph_flag = false;
                    //if (this.nodeFrameworkTabPage != null)
                    if (this.nodePropertyForm.nodeFrameworkTabPage != null)
                    {
                        
                        if (_selectedNodes.Count == 1 && flag == true)
                        {
                            graph_flag = true;
                            Node node = _selectedNodes.ElementAt(0).Value;
                            this.nodePropertyForm.nodeTabPage.TabVisible = true;
                            nodePropertyForm.graphTabPage.TabVisible = false;
                            _selected_main_tab = "node";
                            nodePropertyForm.GraphData_tabControl.SelectedTab = nodePropertyForm.nodeTabPage;
                            statusBarNodeGUID.Text = node.ID;

                            if (node.Type.Name.ToLower() == "asset" || node.Type.Name.ToLower() == "asset-group")
                            {
                                Console.WriteLine("Mainform > UpdateTabs > asset");
                                //nodeDistributionsForm.HideNodeValuePanel(node.Type.Name.ToLower());
                                nodeDistributionsForm.updateNodeAttribute(node.Type.Name.ToLower(), node);
                                ShowRequiredRiskPanels(node.ID);
                                UpdateNodeRiskPanelValues(node.ID);
                                nodeDistributionsForm.ShowNodeDistributionPanel(node.Type.Name.ToLower());
                                this.dockingManager1.SetDockLabel(this.nodeDistributionsForm, "Distributions");

                            }
                            else if (node.Type.Name.ToLower() == "attack")
                            {
                                Console.WriteLine("Mainform > UpdateTabs > attack");
                                //nodeDistributionsForm.HideNodeValuePanel(node.Type.Name.ToLower());
                                nodeDistributionsForm.updateNodeAttribute(node.Type.Name.ToLower(), node);
                                ShowRequiredRiskPanels(node.ID);
                                UpdateNodeRiskPanelValues(node.ID);
                                nodeDistributionsForm.ShowNodeDistributionPanel(node.Type.Name.ToLower());
                                this.dockingManager1.SetDockLabel(nodeDistributionsForm.panelContainer, "Distributions");
                            }
                            else if (node.Type.Name.ToLower() == "actor")
                            {
                                Console.WriteLine("Mainform > UpdateTabs > actor");
                                //nodeDistributionsForm.HideNodeValuePanel(node.Type.Name.ToLower());
                                nodeDistributionsForm.updateNodeAttribute(node.Type.Name.ToLower(), node);
                                ShowRequiredRiskPanels(node.ID);
                                UpdateNodeRiskPanelValues(node.ID);
                                nodeDistributionsForm.ShowNodeDistributionPanel(node.Type.Name.ToLower());
                                this.dockingManager1.SetDockLabel(nodeDistributionsForm.panelContainer, "Distributions");
                            }
                            else if (node.Type.Name.ToLower() == "vulnerability")
                            {
                                Console.WriteLine("Mainform > UpdateTabs > vulnerability");
                                nodeDistributionsForm.updateNodeAttribute(node.Type.Name.ToLower(), node);
                                ShowRequiredRiskPanels(node.ID);
                                UpdateNodeRiskPanelValues(node.ID);
                                nodeDistributionsForm.ShowNodeDistributionPanel(node.Type.Name.ToLower());
                                this.dockingManager1.SetDockLabel(nodeDistributionsForm.panelContainer, "Distributions");
                            }
                            else if (node.Type.Name.ToLower() == "objective")
                            {
                                Console.WriteLine("Mainform > UpdateTabs > objective");
                                //nodeDistributionsForm.HideNodeValuePanel(node.Type.Name.ToLower());
                                ShowRequiredRiskPanels(node.ID);
                                UpdateNodeRiskPanelValues(node.ID);
                                nodeDistributionsForm.ShowNodeDistributionPanel(node.Type.Name.ToLower());
                                this.dockingManager1.SetDockLabel(nodeDistributionsForm.panelContainer, "Objective Values");
                            }
                            else if (node.Type.Name.ToLower() == "control")
                            {
                                Console.WriteLine("Mainform > UpdateTabs > control");
                                //nodeDistributionsForm.HideNodeValuePanel(node.Type.Name.ToLower());
                                ShowRequiredRiskPanels(node.ID);
                                UpdateNodeRiskPanelValues(node.ID);
                                nodeDistributionsForm.ShowNodeDistributionPanel(node.Type.Name.ToLower());
                                this.dockingManager1.SetDockLabel(nodeDistributionsForm.panelContainer, "Distributions");
                            }
                            else if (node.Type.Name.ToLower() == "group")
                            {
                                Console.WriteLine("Mainform > UpdateTabs > group");
                                //nodeDistributionsForm.HideNodeValuePanel(node.Type.Name.ToLower());
                                ShowRequiredRiskPanels(node.ID);
                                UpdateNodeRiskPanelValues(node.ID);
                                nodeDistributionsForm.ShowNodeDistributionPanel(node.Type.Name.ToLower());
                                this.dockingManager1.SetDockLabel(nodeDistributionsForm.panelContainer, "Group Values");
                            }
                            else if (node.Type.Name.ToLower() == "evidence")
                            {
                                Console.WriteLine("Mainform > UpdateTabs > evidence");
                                //nodeDistributionsForm.HideNodeValuePanel(node.Type.Name.ToLower());
                                ShowRequiredRiskPanels(node.ID);
                                UpdateNodeRiskPanelValues(node.ID);
                                nodeDistributionsForm.ShowNodeDistributionPanel(node.Type.Name.ToLower());
                                this.dockingManager1.SetDockLabel(nodeDistributionsForm.panelContainer, "Evidence Values");
                            }
                            else if (node.Type.Name.ToLower() == "vulnerability-group")
                            {
                                Console.WriteLine("Mainform > UpdateTabs > vulnerability-group");
                                //nodeDistributionsForm.HideNodeValuePanel(node.Type.Name.ToLower());
                                ShowRequiredRiskPanels(node.ID);
                                UpdateNodeRiskPanelValues(node.ID);
                                nodeDistributionsForm.ShowNodeDistributionPanel(node.Type.Name.ToLower());
                                this.dockingManager1.SetDockLabel(nodeDistributionsForm.panelContainer, "Vulnerability Group Values");
                            }
                            else
                            {
                                //nodeDistributionsForm.HideNodeValuePanel("control");
                                nodeDistributionsForm.ShowNodeDistributionPanel("control");
                            }
                        }
                        else if (_selectedNodes.Count == 0 && _selectedEdges.Count == 0)
                        {
                            //called when graph background clicked
                            this.nodePropertyForm.nodeTabPage.TabVisible = false;
                            HideNodeRiskPanels();
                            nodeDistributionsForm.HideNodeDistributionPanels(null);
                            ShowRiskPanels("all", "all");
                            nodePropertyForm.UpdateGraphStatis();
                        }

                        if (_selectedEdges.Count == 1 && flag == true)
                        {
                            Console.WriteLine("Mainform > UpdateTabs > edge");
                            Edge edge = _selectedEdges.ElementAt(0).Value as Edge;
                            statusBarNodeGUID.Text = edge.ID;
                            nodeDistributionsForm.visiblePanelEdgeDetails(true);
                            UpdateEdgeRiskPanelValues(edge.ID);
                            graph_flag = true;
                            this.nodePropertyForm.edgeTabPage.TabVisible = true;
                            this.nodePropertyForm.nodeTabPage.TabVisible = false;
                            nodePropertyForm.graphTabPage.TabVisible = false;

                            _selected_main_tab = "edge";
                            nodePropertyForm.GraphData_tabControl.SelectedTab = nodePropertyForm.edgeTabPage;

                            toolStripeDrawnEdgeWidth.SelectedIndex = toolStripeDrawnEdgeWidth.Items.IndexOf(edge.edgeStrengthValue.ToString());
                        }
                        else
                        {
                            this.nodeDistributionsForm.visiblePanelEdgeDetails(false);
                            this.nodePropertyForm.edgeTabPage.TabVisible = false;
                        }

                        if (_selectedNodes.Count == 0 && _selectedEdges.Count == 0)
                        {

                        }
                    }
                    if (graph_flag == false)
                    {
                        _selected_main_tab = "graph";
                        this.nodePropertyForm.graphTabPage.TabVisible = true;
                        this.nodePropertyForm.txtGraphTitle.Text = _graphProperties.Name;
                    }
                    else
                    {
                    }

                });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"EXEPTION: MainForm > UpdateTabs : {ex.Message}");
            }

        }


        public async void checkUndoRedoable()
        {
            if (GraphCalcs.graphCalcInProgress == true)
                return;
            JavascriptResponse is_undoable = await _browser.EvaluateScriptAsync($"isUndoable();");
            JavascriptResponse is_redoable = await _browser.EvaluateScriptAsync($"isRedoable();");
            if (is_undoable.Result.Equals(true))
            {
                this.InvokeIfNeed(() => { toolStripUndo.Enabled = true; });
            }
            else
            {
                this.InvokeIfNeed(() => { toolStripUndo.Enabled = false; });
            }

            if (is_redoable.Result.Equals(true))
            {
                this.InvokeIfNeed(() => { toolStripRedo.Enabled = true; });
            }
            else
            {
                this.InvokeIfNeed(() => { toolStripRedo.Enabled = false; });
            }
        }


        private void CreateMainMapBrowser()
        {
            this.InvokeIfNeed(() => { CreateBrowser(ref _browser, panelMainBrowser); });
            GraphUtil.setBrowser(_browser);
        }

        static void DragHandler_DragEnter(object sender, DragEventArgs e)
        {
            // Check if the dragged data is supported by the CEF browser
            if (e.Data.GetDataPresent(DataFormats.Text))
            {
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

       


        private void CreateBrowser(ref ChromiumWebBrowser browser, Control control)
        {
            BrowserSettings browserSettings = new BrowserSettings()
            {
                //Plugins = CefState.Disabled,
                ImageLoading = CefState.Enabled,
                Javascript = CefState.Enabled,
            };

            // Initialize the DragHandler
            DragHandler dragHandler = new DragHandler();

            //Main browser
            browser = new ChromiumWebBrowser("netg://main");
            browser.LoadingStateChanged += MainBrowser_LoadingStateChanged;
            _mainMapBoundObjectV2 = new BoundObjectV2();
            _mainMapBoundObjectV2.OnEventFired += _mainMapBoundObjectV2_OnEventArrived;
            browser.JavascriptObjectRepository.Register("bound", _mainMapBoundObjectV2, isAsync: false, options: BindingOptions.DefaultBinder);

            browser.RequestHandler = new CustomRequestHandler(_settings);
            browser.MenuHandler = new ContextMenuHandler();
            browser.KeyboardHandler = new KeyboardHandler();
            browser.BrowserSettings = browserSettings;
            browser.CreateControl();
            browser.Dock = DockStyle.Fill;
            browser.Name = "CyConexBrowser";
            browser.DragHandler = dragHandler;
            control.Controls.Add(browser);
            browser.BringToFront();

            
        }

        private void _mainMapBoundObjectV2_OnEventArrived(string source, string eventName, object eventData = null)
        {
            string eventDataS = eventData != null ? eventData.ToString() : String.Empty;
            _browser_OnEventFired(this, new EventHandlerEventArgs(source, eventName, eventDataS));
        }


        private void _browser_OnEventFired(object sender, EventHandlerEventArgs e)
        {
            Debug.WriteLine("_browser_OnEventFired : " + e.Source + " - " + e.Event + " - ");
            switch (e.Source)
            {
                case "main":
                    Debug.WriteLine("MainForm > _browser_OnEventFired > main");
                    switch (e.Event)
                    {
                        case "doneLoad":
                            Debug.WriteLine("MainForm > _browser_OnEventFired > doneLoad");
                            PassSettingsToBrowser();
                            break;
                        case "onkey":
                            Debug.WriteLine("MainForm > _browser_OnEventFired > prekey");
                            ProcessKeyboardEvent(e);
                            break;
                        default:
                            Debug.WriteLine("MainForm > _browser_OnEventFired > default");
                            _ = ProcessMainEventAsync(e);
                            break;
                    }
                    break;
                case "node":
                    Debug.WriteLine("MainForm > _browser_OnEventFired > node");
                    ProcessNodeEvent(e);
                    break;
                case "edge":
                    Debug.WriteLine("MainForm > _browser_OnEventFired > edge");
                    ProcessEdgeEvent(e);
                    break;
                default:
                    break;

            }
        }

        private void PassSettingsToBrowser()
        {
            _browser.ExecScriptAsync($"document.body.style.overflow = 'hidden'; var doubleClickInterval = {SystemInformation.DoubleClickTime};");
            _browser.ExecScriptAsync($"var UseLastNodeColorAsDefault={_settings.UseLastNodeColorAsDefault.ToJavaBool()};");
            _browser.ExecScriptAsync($"var UseLastNodeShapeAsDefault={_settings.UseLastNodeShapeAsDefault.ToJavaBool()};");
            _browser.ExecScriptAsync($"var UseLastNodeSizeAsDefault={_settings.UseLastNodeSizeAsDefault.ToJavaBool()};");
            _browser.ExecScriptAsync($"var manualTargetingMode='{_settings.ManualTargetingMode.ToString().ToLower()}';");
            _browser.ExecScriptAsync($"var allowSelfConnectedNodes={_settings.AllowSelfConnectedNodes.ToJavaBool()};");
            _browser.ExecScriptAsync($"var showNodeSelectionCues={_settings.ShowNodeFocusCue.ToJavaBool()};");
            _browser.ExecScriptAsync($"var alwaysSelectLastNodeID={_settings.AutoSelectLastNode.ToJavaBool()};");
            _browser.ExecScriptAsync($"var colorizeNodes={_settings.ColorizeNodes.ToJavaBool()};");
            _browser.ExecScriptAsync($"var debugMode={Program.DebugMode.ToJavaBool()}");

            _browser.ExecScriptAsync("cy.data('title', cy.data('title'))");

            

            ShowHideGrid();
        }


        private void ProcessKeyboardEvent(EventHandlerEventArgs e)
        {
            Console.WriteLine("ProcessKeyboardEvent:" + e.Event);
            CefKeyboardEvent cefKeyboardEvent = JsonConvert.DeserializeObject<CefKeyboardEvent>(e.Data);

            IsF1key = false;
            IsShiftKey = false;

            if (Control.ModifierKeys == Keys.Control && cefKeyboardEvent.WindowsKeyCode.ToString() == "70")
            {

                this.InvokeIfNeed(() =>
                {
                    toolBtnFind.Checked = true;
                    ShowHideFindPanel();
                });
            }
            else if (Control.ModifierKeys == Keys.Control && cefKeyboardEvent.WindowsKeyCode.ToString() == Keys.C.GetHashCode().ToString())
            {

                _ = toolBtnCopy_ClickAsync();

            }
            else if (Control.ModifierKeys == Keys.Control && cefKeyboardEvent.WindowsKeyCode.ToString() == Keys.V.GetHashCode().ToString())
            {
                _ = toolBtnPaste_ClickAsync();
            }
            else
            {
                switch (cefKeyboardEvent.WindowsKeyCode)
                {
                    case 46:
                        ////Del
                        ////Delete selected in this case
                        if (cefKeyboardEvent.Modifiers == CefEventFlags.None)
                        {
                            this.InvokeIfNeed(() => { deleteSelectedNodes(); });
                        }
                        break;
                    default:
                        break;
                }
            }
            Console.WriteLine("Shift & F1:" + IsShiftKey.ToString() + " : " + IsF1key.ToString());
        }

        private async Task ProcessMainEventAsync(EventHandlerEventArgs e)
        {
            try
            {
               
                Console.WriteLine("ProcessMainEvent:" + e.Event);
                switch (e.Event)
                {
                    case "rightClick":
                        Console.WriteLine("ProcessMainEvent > rightClick");
                        await UpdateFirstSelectedNode();
                        Graph.NodePositions nodePositions = Graph.NodePositions.FromJson(e.Data);
                        break;
                    case "rightClickExt":
                        Console.WriteLine("ProcessMainEvent > rightClickExt");
                        await UpdateFirstSelectedNode();
                        Graph.NodePositions nodeP = Graph.NodePositions.FromJson(e.Data);
                        break;
                    //case "tapstart":  //Graph
                    case "click":
                        await UpdateFirstSelectedNode();
                        Console.WriteLine("ProcessMainEvent > Click");
                        _selectedNodes.Clear();
                        _selectedEdges.Clear();
                        await GetCountOfSelectedItems();
                        SetEdgeDraw(false);
                        UpdateTabs(false);
                        //clickPosition = Graph.NodePositions.FromJson(e.Data);

                        break;
                    case "manualTargetingSet":

                        break;
                    case "manualTargetingRemoved":

                        break;
                    case "data":
                        Console.WriteLine("ProcessMainEvent > data");
                        /*JsonSerializerSettings jsonSerializerSettings = new JsonSerializerSettings
                        {
                            Error = delegate (object sender, Newtonsoft.Json.Serialization.ErrorEventArgs args)
                            {
                                Debug.WriteLine($"{args.ErrorContext}");
                                args.ErrorContext.Handled = false;
                            },
                            DateTimeZoneHandling = DateTimeZoneHandling.Local
                        };
                        _graphProperties = JsonConvert.DeserializeObject<GraphProperties>(e.Data, jsonSerializerSettings);
                        this.InvokeIfNeed(() =>
                        {
                            this.nodePropertyForm.setGraphPropertyData(_graphProperties);
                        });*/
                        break;
                    case "boxstart":
                        NodeBoxingInProgress = true;
                        break;
                    case "boxend":
                        NodeBoxingInProgress = false;
                        await GetCountOfSelectedItems();
                        await UpdateFirstSelectedNode();
                        await UpdateToolbarEnableForNodeEdgeAsync(countOfselectedNodesOnGraph);
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"EXEPTION: MainForm > ProcessMainEvent : {ex.Message}");
            }
        }

        private async Task GetCountOfSelectedItems()
        {
            var json = await _browser.EvaluateScriptAsync($"getCountOfSelectedNodes();");
            countOfselectedNodesOnGraph = int.Parse(json.Result.ToString());
            
        }
        private async void ProcessNodeEvent(EventHandlerEventArgs e)
        {
            //Console.WriteLine("ProcessNodeEvent:" + e.Event);

            await GetCountOfSelectedItems();
            if (countOfselectedNodesOnGraph > 1)
                return;

            switch (e.Event)
            {
                case "doubleClick":
                    {
                        Console.WriteLine("MainForm > ProcessNodeEvent > doubleClick");
                        Graph.NodePositions nodePositions = Graph.NodePositions.FromJson(e.Data);
                        string nodeID = nodePositions.ID;
                        var json = await _browser.EvaluateScriptAsync($"getNodeJson('{nodeID}');");
                        var jsonRes = json.Result;
                        var data = ((IDictionary<String, Object>)jsonRes);
                        var node_data = (IDictionary<String, Object>)data["data"];
                        this.InvokeIfNeed(async () =>
                        {
                            this.dockingManager1.SetDockVisibility(nodeDistributionsForm.panelContainer, true);
                            //this.dockingManager1.SetDockVisibility(this.panelProperties, true);
                            btnDetail.Checked = true;
                            //////this.dockingManager1.SetDockVisibility(this.panelProperties, true);
                            this.dockingManager1.SetDockVisibility(this.nodePropertyForm.panelProperties, true);
                        });
                        UpdateTabs();

                        // show Node Property and Values Panel

                        break;
                    }
                case "rightClick":
                    {
                        Console.WriteLine("MainForm > ProcessNodeEvent > rightClick");
                        Graph.NodePositions nodePositions = Graph.NodePositions.FromJson(e.Data);
                        this.InvokeIfNeed(async () =>
                        {
                            var enabled = await _browser.EvaluateScriptAsync($"isNodeEnabled(\"{nodePositions.ID}\");");
                        });
                    }
                    break;
                case "tapstart":  //Node
                    Console.WriteLine("MainForm > ProcessNodeEvent > tapstart");
                    Graph.NodePositions nodePositions1 = Graph.NodePositions.FromJson(e.Data);
                    string nodeID1 = nodePositions1.ID;
                    var json1 = await _browser.EvaluateScriptAsync($"getNodeJson('{nodeID1}');");
                    var jsonRes1 = json1.Result;
                    var data1 = ((IDictionary<String, Object>)jsonRes1);
                    var node_data1 = (IDictionary<String, Object>)data1["data"];
                    _selectedEdges.Clear();
                    _selectedNodes.Clear();
                    _selectedNodes.TryAdd(nodeID1, Node.FromJson(JsonConvert.SerializeObject(node_data1)));

                    break;
                case "click": //Edge
                    Console.WriteLine("MainForm > ProcessNodeEvent > click (Edge)");
                    
                    if (_manualSourceNodeID != "")
                    {
                        _manualSourceNodeID = "";
                        var getEdge = await _browser.EvaluateScriptAsync($"getLastEdgeID();");
                        var edgeID = "";
                        if (getEdge.Result != null)
                        {
                            edgeID = getEdge.Result.ToString();
                        }

                        if (edgeID != "" && _manualSourceNodeID != "")
                        {
                            this.InvokeIfNeed(async () =>
                            {
                                //New Edge
                                var json = await _browser.EvaluateScriptAsync($"getEdgeJson('{edgeID}');");
                                var jsonRes = json.Result;
                                var data = ((IDictionary<String, Object>)jsonRes);
                                var edge_data = (IDictionary<String, Object>)data["data"];
                                //GraphUtil.AddRelationshipWithObject((JObject)edge_data);
                                var source_id = edge_data["source"];
                                var target_id = edge_data["target"];

                                var source_json = await _browser.EvaluateScriptAsync($"getNodeJson('{source_id}');");
                                var source_jsonRes = source_json.Result;
                                var source_data = ((IDictionary<String, Object>)source_jsonRes);
                                var source_node_data = (IDictionary<String, Object>)source_data["data"];
                                var source_type = source_node_data["nodeType"].ToString();

                                var target_json = await _browser.EvaluateScriptAsync($"getNodeJson('{target_id}');");
                                var target_jsonRes = target_json.Result;
                                var target_data = ((IDictionary<String, Object>)target_jsonRes);
                                var target_node_data = (IDictionary<String, Object>)target_data["data"];
                                var target_type = target_node_data["nodeType"].ToString();


                                _manualSourceNodeID = "";
                            });
                        }
                    }
                    break;
                case "select":  //Node
                    {
                        Console.WriteLine("MainForm > ProcessNodeEvent > select (Node)");
                        Console.WriteLine("MainForm > ProcessNodeEvent > select (Node) : Selelcted Node Count is " + _selectedNodes.Count().ToString());
                        if (NodeBoxingInProgress == false && countOfselectedNodesOnGraph == 1) // Only process for a single node selection
                        {

                            Node selectedNode = Node.FromJson(e.Data);

                            var tmp_json = await _browser.EvaluateScriptAsync($"getNodeJson('{selectedNode.ID}');");
                            var tmp_jsonRes = tmp_json.Result;
                            var tmp_data = ((IDictionary<String, Object>)tmp_jsonRes);
                            var node_data = tmp_data == null ? null : (IDictionary<String, Object>)tmp_data["data"];
                            if (node_data == null) return;
                            node_data["masterID"] = node_data.ContainsKey("masterID") ? node_data["masterID"] : "";
                            
                            _selectedNodes.TryAdd(selectedNode.ID, Node.FromJson(JsonConvert.SerializeObject(node_data)));
                            _selected_node_id = node_data["id"].ToString();
                            _selected_node_master_id = node_data["masterID"].ToString();
                            _nodes.SetNodeNodeData(this, node_data);
                            HandleSuggestedNodes(node_data["id"].ToString(), node_data["masterID"].ToString());


                            SetEdgeDraw(false);
                            UpdateTabs();
                            _nodes.SetNodeFrameworkData(this, e);

                            nodeDistributionsForm.UpdateNodeAssessmentInfo(_selected_node_id);
                            SetNodeNotesData(e);

                            if (_selectedNodes.Count > 0)
                                SetNodeDataToToolbar(_selected_node_id);

                            var getEdge = await _browser.EvaluateScriptAsync($"getLastEdgeID();");
                            var edgeID = "";
                            if (getEdge.Result != null)
                            {
                                edgeID = getEdge.Result.ToString();
                            }

                            if (edgeID != "" && _manualSourceNodeID != "")
                            {
                                this.InvokeIfNeed(async () =>
                                {
                                    var json = await _browser.EvaluateScriptAsync($"getEdgeJson('{edgeID}');");
                                    var jsonRes = json.Result;
                                    var data = ((IDictionary<String, Object>)jsonRes);
                                    var edge_data = (IDictionary<String, Object>)data["data"];
                                    var source_id = edge_data["source"];
                                    var target_id = edge_data["target"];

                                    var source_json = await _browser.EvaluateScriptAsync($"getNodeJson('{source_id}');");
                                    var source_jsonRes = source_json.Result;
                                    var source_data = ((IDictionary<String, Object>)source_jsonRes);
                                    var source_node_data = (IDictionary<String, Object>)source_data["data"];
                                    var source_type = source_node_data["nodeType"].ToString();

                                    var target_json = await _browser.EvaluateScriptAsync($"getNodeJson('{target_id}');");
                                    var target_jsonRes = target_json.Result;
                                    var target_data = ((IDictionary<String, Object>)target_jsonRes);
                                    var target_node_data = (IDictionary<String, Object>)target_data["data"];
                                    var target_type = target_node_data["nodeType"].ToString();

                                    string relationship_str = "";
                                    using (SettingsForm settingsForm = new SettingsForm(ref _settings))
                                    {
                                        string[] relationship_arr = settingsForm.getDefaultEdgeRelationship(source_type, target_type);
                                        relationship_str = relationship_arr[2];
                                    }

                                    _manualSourceNodeID = "";
                                });
                            }
                        }
                    }


                    break;
                case "unselect":
                    Console.WriteLine("MainForm > ProcessNodeEvent > unselect");
                    //And remove unselected nodes and update UI
                    {
                        Node selectedNode = Node.FromJson(e.Data);
                        _selectedNodes.TryRemove(selectedNode.ID, out Node value);
                        //_nodes.SetNodeFrameworkData(this, e);
                        //_nodes.SetNodeNodeData(this, e);
                        //SetNodeNotesData(e);
                        //SetNodeAssessmentData(e);
                    }
                    Debug.WriteLine($"Unselect. Selected nodes: {_selectedNodes.Count()}");
                    break;
                case "remove":
                    Console.WriteLine("MainForm > ProcessNodeEvent > remove");
                    SetTotalNodesLabel();
                    _hasUnsavedChanges = true;
                    break;
                case "add":
                    Console.WriteLine("MainForm > ProcessNodeEvent > add");
                    break;
                case "position":
                    //Console.WriteLine("MainForm > ProcessNodeEvent > position");
                    _hasUnsavedChanges = true;
                    break;
                case "data":
                    {
                        Console.WriteLine("MainForm > ProcessNodeEvent > data");
                        Graph.NodePositions nodePositions = Graph.NodePositions.FromJson(e.Data);
                        _hasUnsavedChanges = true;
                    }
                    break;

            }
            //checkUndoRedoable();
        }

        private async void SetNodeDataToToolbar(string node_id)
        {
            try
            {
                var node_json = await _browser.EvaluateScriptAsync($"getNodeJson('{node_id}');");
                var node_jsonRes = node_json.Result;
                var node_data = ((IDictionary<String, Object>)node_jsonRes);
                if (node_data == null) return;
                var node_info = (IDictionary<String, Object>)node_data["data"];

                string font_family = node_info.ContainsKey("fontFamily") ? node_info["fontFamily"].ToString() : "Segoe UI";
                string font_size = node_info.ContainsKey("titleSize") ? node_info["titleSize"].ToString() : "14";
                string node_shape = node_info.ContainsKey("shape") ? node_info["shape"].ToString() : "rectangle";
                string border_width = node_info.ContainsKey("borderWidth") ? node_info["borderWidth"].ToString() : "1";
                Color border_color = node_info.ContainsKey("border_color") ?
                    (node_info["border_color"] == null ?
                        Color.Black : GeneralHelpers.ConvertColorFromHTML(node_info["border_color"].ToString())) : Color.Black;
                Color color = node_info.ContainsKey("color") ?
                    (node_info["color"] == null ? Color.Black : GeneralHelpers.ConvertColorFromHTML(node_info["color"].ToString())) :
                        Color.Black;
                //string position = node_info.ContainsKey("position") ? node_info["position"].ToString() : "Bottom";
                string fontWeight = node_info.ContainsKey("fontWeight") ? node_info["fontWeight"].ToString() : "100";
                string textDecoration = node_info.ContainsKey("textDecoration") ? node_info["textDecoration"].ToString() : "solid";
                Color titleColor = node_info.ContainsKey("titleTextColor") && node_info["titleTextColor"] != null ? GeneralHelpers.ConvertColorFromHTML(node_info["titleTextColor"].ToString()) : Color.Black;
                string fontStyle = node_info.ContainsKey("fontStyle") ? node_info["fontStyle"].ToString() : "normal";
          

            this.InvokeIfNeed(() =>
            {
                toolBtnFontFamily.SelectedIndex = toolBtnFontFamily.Items.IndexOf(font_family) < 0 ? 0 : toolBtnFontFamily.Items.IndexOf(font_family);
                toolBtnFontSize.SelectedIndex = toolBtnFontSize.Items.IndexOf(font_size) < 0 ? 0 : toolBtnFontSize.Items.IndexOf(font_size);
                //toolComboShape.SelectedIndex = toolComboShape.Items.IndexOf("Ellipse");
                for (int i = 0; i < toolComboShape.Items.Count; i++)
                {
                    if (toolComboShape.Items[i].ToString().ToLower() == node_shape.ToLower())
                    {
                        toolComboShape.SelectedIndex = i;
                        break;
                    }
                }
                toolBtnBorderWidth.SelectedIndex = toolBtnBorderWidth.Items.IndexOf(border_width);
                toolBtnBorderColor.BackColor = border_color;
                toolBtnFillColor.BackColor = color;
                toolBtnFontBold.Checked = fontWeight == "100" ? false : true;
                toolBtnFontColor.BackColor = titleColor;
                toolBtnFontItalic.Checked = fontStyle == "normal" ? false : true;
            });
            }
            catch
            {
            }
        }

        private async void HandleSuggestedNodes(string nodeID, string masterID)
        {
            if (masterID == "")
            {
                return;
            }

            ArrayList suggested_ids = Utility.SearchSuggestedIDs(masterID);
            JArray drawed_ids = new JArray();
            var allEdges = await _browser.EvaluateScriptAsync("getEdges();");
            if (allEdges.Success)
            {
                var tmpObj = JArray.Parse(allEdges.Result.ToString());
                foreach (var tmpEdge in tmpObj)
                {
                    var tmpJson = JObject.Parse(tmpEdge.ToString());
                    var tmpID = tmpJson["id"];
                    string tmpSourceId = tmpJson["source"].ToString();
                    string tmpTargetId = tmpJson["target"].ToString();
                    var json = await _browser.EvaluateScriptAsync($"getNodeJson('{tmpTargetId}');");
                    var jsonRes = json.Result;
                    var data = ((IDictionary<String, Object>)jsonRes);
                    if (data == null)
                    {
                        return;
                    }

                    var node_data = (IDictionary<String, Object>)data["data"];
                    node_data["masterID"] = node_data.ContainsKey("masterID") ? node_data["masterID"] : "-1";
                    if (suggested_ids.IndexOf(node_data["masterID"].ToString()) != -1 && tmpSourceId == nodeID)
                    {
                        suggested_ids.Remove(node_data["masterID"].ToString());
                    }
                }
            }

            _suggested_nodes.Clear();

            bool control_flag = false;
            bool attack_flag = false;
            bool actor_flag = false;
            bool group_flag = false;
            bool objective_flag = false;

            for (int i = 0; i < NodeListViewItems.Count; i++)
            {
                Node node = NodeListViewItems[i];
                foreach (string suggestedNode in suggested_ids)
                {
                    if (suggestedNode == node.ID)
                    {
                        switch (node.Type.Name.ToLower())
                        {
                            case "control":
                                control_flag = true;
                                break;
                            case "group":
                                group_flag = true;
                                break;
                            case "objective":
                                objective_flag = true;
                                break;
                            case "asset":
                                break;
                            case "attack":
                                attack_flag = true;
                                break;
                            case "actor":
                                actor_flag = true;
                                break;
                        }
                        _suggested_nodes.Add(node);
                        break;
                    }
                }
            }
            this.InvokeIfNeed(() =>
            {
                toolSuggested.Enabled = _suggested_nodes.Count > 0 ? true : false;
                toolSugControls.Enabled = control_flag;
                toolSugAttacks.Enabled = attack_flag;
                toolSugActor.Enabled = actor_flag;
                toolSugGroups.Enabled = group_flag;
                toolSugObjectives.Enabled = objective_flag;
            });
        }

        private Color Blend(Color backColor, Color color, double amount)
        {

            byte r = (byte)(color.R * amount + backColor.R * (1 - amount));
            byte g = (byte)(color.G * amount + backColor.G * (1 - amount));
            byte b = (byte)(color.B * amount + backColor.B * (1 - amount));
            return Color.FromArgb(255, r, g, b);
        }

        private async void ProcessEdgeEvent(EventHandlerEventArgs e)
        {
            Console.WriteLine("ProcessEdgeEvent:" + e.Event);

            switch (e.Event)
            {
                case "rightClick":
                    {
                        Graph.NodePositions nodePositions = Graph.NodePositions.FromJson(e.Data);
                        this.InvokeIfNeed(async () =>
                        {

                            var enabled = await _browser.EvaluateScriptAsync($"isEdgeEnabled(\"{nodePositions.ID}\");");

                        });
                    }
                    break;
                case "data":
                    //TODO: process edge data
                    Console.WriteLine("Data Edge");
                    break;
                case "select": //Edge
                    {
                        Console.WriteLine("Select Edge");
                        Edge selectedEdge = Edge.FromJson(e.Data);
                        _selectedNodes.Clear();
                        try
                        {
                            _selectedEdges.TryAdd(selectedEdge.ID, selectedEdge);
                            SetEdgeDetailData(e);
                        }
                        catch { }

                        UpdateTabs();
                        break;
                    }
                case "unselect":
                    {
                        Console.WriteLine("Unselect Edge");
                        Graph.NodePositions nodePositions = Graph.NodePositions.FromJson(e.Data);
                        _selectedEdges.TryRemove(nodePositions.ID, out Edge value);
                    }
                    break;
                case "add":
                    {
                        //Edge selectedEdge = Edge.FromJson(e.Data);
                        //_selectedNodes.Clear();
                        //_selectedEdges.TryAdd(selectedEdge.ID, selectedEdge);
                        Console.WriteLine("Add Edge");
                        break;
                    }
                case "remove":
                    {
                        Console.WriteLine("Remove Edge");
                        break;
                    }
                case "newedge":
                    {
                        var getEdge = await _browser.EvaluateScriptAsync($"getLastEdgeID();");
                        var edgeID = "";

                        if (getEdge.Result != null)
                        {
                            edgeID = getEdge.Result.ToString();
                            var json = await _browser.EvaluateScriptAsync($"getEdgeJson('{edgeID}');");
                            var jsonRes = json.Result;
                            var data = ((IDictionary<String, Object>)jsonRes);
                            var edge_data = (IDictionary<String, Object>)data["data"];
                            var source_id = edge_data["source"];
                            var target_id = edge_data["target"];
                            LocalRelationship relationship = new LocalRelationship();
                            relationship.sourceNodeID = source_id.ToString();
                            relationship.targetNodeID = target_id.ToString();
                            relationship.edgeID = edgeID;
                            GraphUtil.AddRelationship(relationship);
                            LocalEdge edge = new LocalEdge();
                            edge.edgeID = edgeID;
                            edge.edgeStrengthValue = "0.00";
                            edge.edgeStrengthScore = "0.00";
                            edge.enabled = true;
                            GraphUtil.AddEdge(edge);
                            GraphUtil.SetEdgeData(edgeID, "edgeStrengthValue", "0.00"); //Default edge value to 1

                        }
                        break;
                    }

            }
            //checkUndoRedoable();
        }

        private async void SetEdgeDetailData(EventHandlerEventArgs e)
        {
            if (_selectedEdges.Count() < 1)
            {
                return;
            }

            string nodeID = _selectedEdges.ElementAt(_selectedEdges.Count - 1).Value.ID;
            var json = await _browser.EvaluateScriptAsync($"getEdgeJson('{nodeID}');");
            var jsonRes = json.Result;
            if (jsonRes == null)
            {
                return;
            }
            var data = ((IDictionary<String, Object>)jsonRes);
            var edge_data = (IDictionary<String, Object>)data["data"];

            this.InvokeIfNeed(() =>
            {
                string relationshipStrength = edge_data.ContainsKey("relationshipStrength") ? edge_data["relationshipStrength"].ToString() : "";
                string edgeStrengthValue = edge_data.ContainsKey("edgeStrengthValue") ? edge_data["edgeStrengthValue"].ToString() : "";
                string edgeStrengthMinValue = edge_data.ContainsKey("edgeStrengthMinValue") ? edge_data["edgeStrengthMinValue"].ToString() : "";
                nodeDistributionsForm.setEdgeDetailData(relationshipStrength, edge_data["relationship"].ToString(), edgeStrengthValue, edgeStrengthMinValue);
                this.toolStripeDrawnEdgeWidth.Text = edge_data.ContainsKey("weight") ? edge_data["weight"].ToString() : "1";
                string edgeNote = edge_data["note"].ToString();
                this.nodePropertyForm.setHtmlEditorEdgeDesc(edgeNote);
            });
        }


        private async void UpdateRiskDistributionPanelValues(string nodeID)
        {
            if (GraphCalcs.graphCalcInProgress) // Don't update if graph is calculating 
                return;

            var nodeType = GraphUtil.GetNodeType(nodeID).ToString().ToLower();
            if (nodeType == "notfound") //Graph may not be up to date
                await GraphUtil.SyncFromGraph();
            else if (nodeType == "asset")
            {
                this.HeatMapForm.CalculateHeatMaps(nodeID);
            }


        }

        private void UpdateEdgeRiskPanelValues(string edgeID)
        {
            Console.WriteLine("MainForm > UpdateEdgeRiskPanelValues");
            if (GraphCalcs.graphCalcInProgress) // Don't update if graph is calculating 
                return;

            this.InvokeIfNeed(() =>
            {
                double tempVal = GraphUtil.CalculateModeFromDistributionData(edgeID);
                tempVal = tempVal / 100;
                this.riskPanelsForm.lblEdgeStrengthValue.Text = (tempVal > -1) ? tempVal.ToString() : "N/A";
                GetEdgeRiskStatusValue(this.riskPanelsForm.lblEdgeValueStatus, this.riskPanelsForm.lblEdgeStrengthValue);
                this.riskPanelsForm.UpdateGraph("edgeStrength", edgeID);
            });
        }

        private async void UpdateNodeRiskPanelValues(string nodeID)
        {
            
            if (GraphCalcs.graphCalcInProgress) // Don't update if graph is calculating 
                return;
            Console.WriteLine("MainForm > UpdateNodeRiskPanelValues");
            try
            {
                var nodeType = GraphUtil.GetNodeType(nodeID).ToString().ToLower();
                if (nodeType == "notfound") //Graph may not be up to date
                    await GraphUtil.SyncFromGraph();

                if (nodeType == "notfound")
                    return;

                this.InvokeIfNeed(() =>
                {
                    //Actor
                    if (nodeType == "actor")
                    {
                        Console.WriteLine("MainForm > UpdateNodeRiskPanelValues > actor");
                        //Generate trhe distribution graphs
                        this.riskPanelsForm.UpdateGraph("actor", nodeID);
                        this.riskPanelsForm.UpdateGraph("actorMitigated", nodeID);

                        this.riskPanelsForm.lblActorScore.Text = (GraphUtil.CalculateModeFromDistributionData(nodeID) is double value && value > -1) ? value.ToString() : "N/A";
                        this.riskPanelsForm.lblactorMitigatedScore.Text = (GraphUtil.CalculateModeFromDistributionData(nodeID + ":Mitigated") is double value1 && value1 > -1) ? value1.ToString() : "N/A";

                        GetNodeRiskStatusValue(this.riskPanelsForm.lblActorValueStatus, this.riskPanelsForm.lblActorScore);
                        GetNodeRiskStatusValue(this.riskPanelsForm.lblactorMitigatedValueStatus, this.riskPanelsForm.lblactorMitigatedScore);
                        this.riskPanelsForm.panelRiskActor.SuspendLayout();

                        this.riskPanelsForm.label30.Visible = true;
                        this.riskPanelsForm.lblactorMitigatedValueStatus.Visible = true;
                        this.riskPanelsForm.lblactorMitigatedScore.Visible = true;
                        this.riskPanelsForm.panelRiskActor.ResumeLayout();
                    }

                    //Attack
                    if (nodeType == "attack")
                    {
                        Console.WriteLine("MainForm > UpdateNodeRiskPanelValues > attack");
                        //Generate trhe distribution graphs
                        this.riskPanelsForm.UpdateGraph("attack", nodeID);
                        this.riskPanelsForm.UpdateGraph("attackMitigated", nodeID);
                        this.riskPanelsForm.UpdateGraph("attackThreat", nodeID);

                        this.riskPanelsForm.lblattackScore.Text = (GraphUtil.CalculateModeFromDistributionData(nodeID) is double value && value > -1) ? value.ToString() : "N/A";
                        this.riskPanelsForm.lblattackMitigatedScore.Text = (GraphUtil.CalculateModeFromDistributionData(nodeID + ":Mitigated") is double value1 && value1 > -1) ? value1.ToString() : "N/A";
                        //this.riskPanelsForm.lblthreatScore.Text = (GraphUtil.CalculateModeFromDistributionData(nodeID + ":Threat") is double value2 && value2 > -1) ? value2.ToString() : "N/A";
                        this.riskPanelsForm.lblthreatScore.Text = (GraphUtil.GetAverageNodeScore(nodeID + ":threatScore") is double value2 && value2 > -1) ? value2.ToString("F4") : "N/A";

                        GetNodeRiskStatusValue(this.riskPanelsForm.lblattackValueStatus, this.riskPanelsForm.lblattackScore);
                        GetNodeRiskStatusValue(this.riskPanelsForm.lblattackMitigatedValueStatus, this.riskPanelsForm.lblattackMitigatedScore);
                        GetNodeRiskStatusValue(this.riskPanelsForm.lblthreatValueStatus, this.riskPanelsForm.lblthreatScore);

                        this.riskPanelsForm.panelRiskAttack.SuspendLayout();
                        this.riskPanelsForm.label31.Visible = true;
                        this.riskPanelsForm.lblattackMitigatedValueStatus.Visible = true;
                        this.riskPanelsForm.lblattackMitigatedScore.Visible = true;
                        this.riskPanelsForm.label8.Visible = true;
                        this.riskPanelsForm.lblthreatValueStatus.Visible = true;
                        this.riskPanelsForm.lblthreatScore.Visible = true;
                        this.riskPanelsForm.panelRiskAttack.ResumeLayout();
                    }

                    //Asset
                    if (nodeType == "asset" || nodeType == "asset-group")
                    {
                        Console.WriteLine("MainForm > UpdateNodeRiskPanelValues > asset");
                        //Generate trhe distribution graphs
                        this.riskPanelsForm.UpdateGraph("asset", nodeID);
                        this.riskPanelsForm.UpdateGraph("assetMitigated", nodeID);
                        this.riskPanelsForm.UpdateGraph("assetImpact", nodeID);

                        this.riskPanelsForm.lblassetScore.Text = (GraphUtil.CalculateModeFromDistributionData(nodeID) is double value && value > -1) ? value.ToString() : "N/A";
                        this.riskPanelsForm.lblassetMitigatedScore.Text = (GraphUtil.CalculateModeFromDistributionData(nodeID + ":Mitigated") is double value1 && value1 > -1) ? value1.ToString() : "N/A";
                        // this.riskPanelsForm.lblimpactScore.Text = (GraphUtil.CalculateModeFromDistributionData(nodeID + ":Impact") is double value2 && value2 > -1) ? value2.ToString() : "N/A";
                        this.riskPanelsForm.lblimpactScore.Text = (GraphUtil.GetAverageNodeScore(nodeID + ":impactScore") is double value2 && value2 > -1) ? value2.ToString("F4") : "N/A";

                        GetNodeRiskStatusValue(this.riskPanelsForm.lblassetValueStatus, this.riskPanelsForm.lblassetScore);
                        GetNodeRiskStatusValue(this.riskPanelsForm.lblassetMitigatedValueStatus, this.riskPanelsForm.lblassetMitigatedScore);
                        GetNodeRiskStatusValue(this.riskPanelsForm.lblimpactValueStatus, this.riskPanelsForm.lblimpactScore);
                        UpdateRiskDistributionPanelValues(nodeID);

                        ShowRiskPanels("asset", nodeID);
                        this.riskPanelsForm.panelRiskAsset.SuspendLayout();
                        this.riskPanelsForm.label27.Visible = true;
                        this.riskPanelsForm.lblassetMitigatedValueStatus.Visible = true;
                        this.riskPanelsForm.lblassetMitigatedScore.Visible = true;
                        this.riskPanelsForm.label1.Visible = true;
                        this.riskPanelsForm.lblimpactValueStatus.Visible = true;
                        this.riskPanelsForm.lblimpactScore.Visible = true;
                        this.riskPanelsForm.panelRiskAsset.ResumeLayout();
                    }

                    //vulnerability
                    if (nodeType == "vulnerability")
                    {
                        Console.WriteLine("MainForm > UpdateNodeRiskPanelValues > vulnerability");
                        this.riskPanelsForm.UpdateGraph("vulnerability", nodeID);
                        this.riskPanelsForm.UpdateGraph("vulnerabilityMitigated", nodeID);
                        this.riskPanelsForm.UpdateGraph("vulnerabilityLikelihood", nodeID);

                        this.riskPanelsForm.lblvulnerabilityScore.Text = (GraphUtil.CalculateModeFromDistributionData(nodeID) is double value && value > -1) ? value.ToString() : "N/A";
                        this.riskPanelsForm.lblvulnerabilityMitigatedScore.Text = (GraphUtil.CalculateModeFromDistributionData(nodeID + ":Mitigated") is double value1 && value1 > -1) ? value1.ToString() : "N/A";
                        //this.riskPanelsForm.lblvulnerabilityLikelihoodScore.Text = (GraphUtil.CalculateModeFromDistributionData(nodeID + ":Likelihood") is double value2 && value2 > -1) ? value2.ToString() : "N/A";
                        this.riskPanelsForm.lblvulnerabilityLikelihoodScore.Text = (GraphUtil.GetAverageNodeScore(nodeID + ":likelihood") is double value2 && value2 > -1) ? value2.ToString("F4") : "N/A";

                        this.riskPanelsForm.lblvulnerabilityLikelihoodFrequency.Text = GraphUtil.ConvertValueToTimeInterval(this.riskPanelsForm.lblvulnerabilityLikelihoodScore.Text);

                        GetNodeRiskStatusValue(this.riskPanelsForm.lblvulnerabilityValueStatus, this.riskPanelsForm.lblvulnerabilityScore);
                        GetNodeRiskStatusValue(this.riskPanelsForm.lblvulnerabilityMitigatedValueStatus, this.riskPanelsForm.lblvulnerabilityMitigatedScore);
                        GetNodeRiskStatusValue(this.riskPanelsForm.lblLikelihoodValueStatus, this.riskPanelsForm.lblvulnerabilityLikelihoodScore);

                        this.riskPanelsForm.panelRiskVulnerability.SuspendLayout();
                        this.riskPanelsForm.label41.Visible = true;
                        this.riskPanelsForm.lblvulnerabilityMitigatedValueStatus.Visible = true;
                        this.riskPanelsForm.lblvulnerabilityMitigatedScore.Visible = true;
                        this.riskPanelsForm.label42.Visible = true;
                        this.riskPanelsForm.lblLikelihoodValueStatus.Visible = true;
                        this.riskPanelsForm.lblvulnerabilityLikelihoodScore.Visible = true;
                        this.riskPanelsForm.panelRiskVulnerability.ResumeLayout();

                    }

                    //Control
                    if (nodeType == "control")
                    {
                        Console.WriteLine("MainForm > UpdateNodeRiskPanelValues > control");
                        this.riskPanelsForm.UpdateGraph("controlStrengh", nodeID);
                        this.riskPanelsForm.UpdateGraph("controlImplementation", nodeID);
                        this.riskPanelsForm.UpdateGraph("controlValue", nodeID);

                        this.riskPanelsForm.lblControlStrengthValue.Text = (GraphUtil.CalculateModeFromDistributionData(nodeID + ":Base") is double value && value > -1) ? value.ToString() : "N/A";
                        this.riskPanelsForm.lblControlImplementationValue.Text = (GraphUtil.CalculateModeFromDistributionData(nodeID + ":Assessed") is double value1 && value1 > -1) ? value1.ToString() : "N/A";
                        this.riskPanelsForm.lblControlValue.Text = (GraphUtil.CalculateModeFromDistributionData(nodeID + ":Calculated") is double value2 && value2 > -1) ? value2.ToString() : "N/A";

                        this.riskPanelsForm.panelRiskControl.SuspendLayout();
                        this.riskPanelsForm.label13.Visible = true;
                        this.riskPanelsForm.lblcontrolValueStatus.Visible = true;
                        this.riskPanelsForm.lblControlValue.Visible = true;
                        this.riskPanelsForm.panelRiskControl.ResumeLayout();

                    }

                    
                    //Evidence
                    if (nodeType == "evidence")
                    {
                        //Todo
                        //Console.Beep();

                    }

                });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"EXEPTION: MainForm > UpdateNodeRiskPanelValues : {ex.Message}");
            }

        }

        private void GetEdgeRiskStatusValue(Label lbl, Label tf)
        {
            string score = tf.Text;
            double scoreValue = 0;
            if (score == null || score == "N/A")
                score = "0";
            try
            {
                scoreValue = Double.Parse(score);
            }
            catch
            {
                scoreValue = 0;
            }

            if (scoreValue == 1.00)
            {
                lbl.Text = "Direct";
            }
            else if (scoreValue > 0.8)
            {
                lbl.Text = "Very Strong"; //0.8 to 1.0
            }
            else if (scoreValue > 0.6)
            {
                lbl.Text = "Strong"; //.6 to .8
            }
            else if (scoreValue > 0.4)
            {
                lbl.Text = "Moderate"; //.4 to 0.6
            }
            else if (scoreValue > 0.2)
            {
                lbl.Text = "Weak"; // .2 to .4
            }
            else if (scoreValue > 0)
            {
                lbl.Text = "Very Weak"; // 0 to .2
            }
            else if (scoreValue == 0)
            {
                lbl.Text = "None";
            }
            else
            {
                lbl.Text = "N/A";
            }
        }

        private void GetNodeRiskStatusValue(Label lbl, Label tf)
        {
            string score = tf.Text;
            double scoreValue = 0;
            if (score == null || score == "N/A")
                score = "0";
            try
            {
                scoreValue = Math.Round(Double.Parse(score));
            }
            catch
            {
                scoreValue = 0;
            }
            //tf.Text = scoreValue.ToString();


            if (scoreValue > 90)
            {
                lbl.Text = "Critical";
            }
            else if (scoreValue > 75)
            {
                lbl.Text = "Very High";
            }
            else if (scoreValue > 50)
            {
                lbl.Text = "High";
            }
            else if (scoreValue > 25)
            {
                lbl.Text = "Moderate";
            }
            else if (scoreValue > 10)
            {
                lbl.Text = "Low";
            }
            else if (scoreValue > 0)
            {
                lbl.Text = "Very Low";
            }
            else
            {
                lbl.Text = "N/A";
            }

        }

        private async void SetNodeNotesData(EventHandlerEventArgs e)
        {
            
            if (_selectedNodes.Count() < 1)
            {
                return;
            }

            Graph.NodePositions nodePositions = Graph.NodePositions.FromJson(e.Data);
            string nodeID = _selectedNodes.ElementAt(0).Value.ID;
            var json = await _browser.EvaluateScriptAsync($"getNodeJson('{nodeID}');");
            var jsonRes = json.Result;
            var data = ((IDictionary<String, Object>)jsonRes);
            if (data == null)
            {
                return;
            }

            try
            {
                var node_data = (IDictionary<String, Object>)data["data"];
                this.InvokeIfNeed(() =>
                {
                    if (node_data.ContainsKey("notes"))
                    { 
                        string note = node_data["note"].ToString();
                        this.nodePropertyForm.setHtmlEditorNodeNote(note);

                        /*var from_note = System.Convert.FromBase64String(note);
                        string final_note = System.Text.Encoding.UTF8.GetString((from_note));
                        this.nodePropertyForm.richTextNodeNotes.Rtf = final_note;*/
                    }
                    this.statusBarNodeGUID.Text = nodeID;
                });
            }
            catch { }

            if (_selectedNodes.Count() > 0)
            {
                Node node_item = _selectedNodes.ElementAt(0).Value;
            }
        }

        private void MainBrowser_LoadingStateChanged(object sender, LoadingStateChangedEventArgs e)
        {
            if (!e.IsLoading)
            {
                _browser.ExecuteScriptAsync("bound.eventHandler(\"main\", \"doneLoad\", \"\");");
                this.InvokeIfNeed(() =>
                {
                    btnNewGraph.Enabled = true;
                    btnOpenLocal.Enabled = true;
                    btnSaveLocal.Enabled = true;
                    btnSaveAsLocal.Enabled = true;
                });
            }
        }


        private void SetupAddNodeMenu()
        {
            foreach (var nodeShape in NodeShapes.NodesShapes)
            {
                ToolStripMenuItem toolStripMenuItem = new ToolStripMenuItem(nodeShape.Name);
                toolStripMenuItem.DisplayStyle = ToolStripItemDisplayStyle.Text;
                toolStripMenuItem.Tag = nodeShape.Shape;
                toolStripMenuItem.Click += AddNode_ToolStripMenuItem_Click;
            }
        }

        private void SetupSetShapeMenu()
        {
            foreach (var nodeShape in NodeShapes.NodesShapes)
            {
                ToolStripMenuItem toolStripMenuItem = new ToolStripMenuItem(nodeShape.Name);
                toolStripMenuItem.DisplayStyle = ToolStripItemDisplayStyle.Text;
                toolStripMenuItem.Tag = nodeShape.Shape;
                toolStripMenuItem.Click += SetShape_ToolStripMenuItem_Click;
            }
        }

        private void SetNodeShape(object sender)
        {
            string shape = (sender as ToolStripMenuItem).Tag.ToString();
            _hasUnsavedChanges = true;
        }

        private void SetShape_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetNodeShape(sender);
        }

        private void cloneNode()
        {
            _hasUnsavedChanges = true;
            checkUndoRedoable();
        }

        private void CloneNode_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cloneNode();
        }

        private void addNodeForToolStrip(object sender)
        {

            checkUndoRedoable();
        }

        private void AddNode_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            addNodeForToolStrip(sender);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //this.SuspendLayout();
            this.Height = 750;
            this.Width = 1000;
            this.ClientSize = new Size(this.Width, this.Height);


            //CEF related
            CEFHelper.InitCEF(_settings);
            CreateMainMapBrowser();
            //Setup add node menu
            SetupAddNodeMenu();
            SetupSetShapeMenu();
            Utility.ClearAuditLog();
            _settings = ApplicationSettings.Load();
            _graphProperties = new GraphProperties();

            NodeListViewItems = new List<Node>();
            SetUpInitialVisuals();
            GraphCalcs.Init(this);

            //Check recent files
            List<JObject> newRecentFiles = new List<JObject>();
            foreach (JObject file in _settings.ResentFiles)
            {
                if (File.Exists(file["name"].ToString()))
                {
                    newRecentFiles.Add(file);
                }
            }

            if (!_settings.ResentFiles.SequenceEqual(newRecentFiles))
            {
                _settings.ResentFiles = newRecentFiles;
            }

            if (_settings.RestoreWindowStateOnStart)
            {
                if (_settings.MainWindowMaximized)
                {
                    this.WindowState = FormWindowState.Maximized;
                }
                else
                {
                    if (_settings.MainWindowPosition.IsEmpty)
                    {
                        this.StartPosition = FormStartPosition.CenterScreen;
                    }
                    else
                    {
                        this.StartPosition = FormStartPosition.Manual;
                        this.Bounds = _settings.MainWindowPosition;
                    }
                }
            }

            SetupEdgeData();
            InitFontItems();
            InitNodeCategoryPopup();
            updateNodeTypeListbox();
            InitRecentGraphs();

            using (SettingsForm settingsForm = new SettingsForm(ref _settings))
            {
                this._default_edge_relationship = settingsForm.getDefaultEdgeRelationships();
            }

            btnGrid.Checked = _settings.ShowGrid;
            btnLabels.Checked = _settings.ShowLabel;
            btnSwitch.Checked = _settings.SwitchTitle;

            //Docking panels update
            dockingManager1.LockHostFormUpdate();
            bool tmp_node_repository = _settings.ShowNodeRepository;
            //bool tmp_node_information = _settings.ShowNodeInformation;
            //bool tmp_node_values_panel = _settings.ShowNodeValuesPanel;
            //bool tmp_risk_panel = _settings.ShowRiskPanel;
            //bool tmp_node_data_panel = _settings.ShowNodeDataPanel;
            //bool tmp_risk_list_panel = _settings.ShowRiskListPanel;
            bool tmp_risk_heat_map = _settings.ShowRiskHeatMap;

            //this.btnNodeRepository.Checked = true;// _settings.ShowNodeRepository;
            //this.btnDetail.Checked = true;//_settings.ShowNodeInformation;
            //this.btnDistributions.Checked = true;//_settings.ShowNodeValuesPanel;
            //this.btnCompliance.Checked = true;// _settings.ShowCompliancePanel;
            //this.btnValues.Checked = true;//_settings.ShowRiskPanel;
            //this.btnNodeData.Checked = true;//_settings.ShowNodeDataPanel;
            //this.btnRiskList.Checked = true;//_settings.ShowRiskListPanel;
            //this.btnRiskHeatMap.Checked = true;//_settings.ShowRiskHeatMap;
            this.btnDetail.Checked = true;
            this.btnDistributions.Checked = true;
            //this.btnCompliance.Checked = true;
            this.btnValues.Checked = true;
            this.btnNodeData.Checked = true;
            this.btnRiskList.Checked = true;

            this.btnDetail.Checked = false;
            this.btnDistributions.Checked = false;
            this.btnCompliance.Checked = false;
            this.btnValues.Checked = false;
            this.btnNodeData.Checked = false;
            this.btnRiskList.Checked = false;
            this.btnNodeRepository.Checked = false;

            //this.btnNodeRepository.Checked =  tmp_node_repository;
            //this.btnDetail.Checked = tmp_node_information;
            //this.btnDistributions.Checked = tmp_node_values_panel;
            //this.btnCompliance.Checked = _settings.ShowCompliancePanel;
            //this.btnValues.Checked = tmp_risk_panel;
            //this.btnNodeData.Checked =tmp_node_data_panel;
            //this.btnRiskList.Checked = tmp_risk_list_panel;
            this.btnRiskHeatMap.Checked = tmp_risk_heat_map;
            dockingManager1.UnlockHostFormUpdate();

            this.cbAutoCalc.Checked = _settings.AutoCalc;

            InitNodeControlPanels();
            //InitNodePropertyPanel();

            CheckInternetConnectivity();
            RefreshDockingList();
            HandleAutoSaveGraph();
            //string text = btnLabels.Checked ? "true" : "false";
            //_browser.ExecScriptAsync($"showHideTippy('{text}');");
        }
        public void HandleAutoSaveGraph()
        {
            string tempPath = Path.GetTempPath();
            List<string> list = Directory.GetFiles(tempPath, "*.graphtmp", SearchOption.TopDirectoryOnly).ToList();
            if (list.Count > 0)
            {
                DialogResult dialogResult = recoveryFileSaveModal.ShowDialog(this);
                if (dialogResult == DialogResult.OK)
                {
                    LoadAutoSavedGraph(list[0]);
                }
                else if (dialogResult == DialogResult.Ignore)
                {
                    Utility.DeleteTempFiles();
                    newGraphAction();
                }
            }
        }

        public void LoadAutoSavedGraph(string tmp_path)
        {
            tmp_graph_file_path = tmp_path;
            LoadDataFromFile(tmp_graph_file_path, true);

            if (_settings.AutoSaveOnTimer)
            {
                // Hook up the Elapsed event for the timer. 
                saveTimer.Enabled = true;
            }
        }

        public void CheckInternetConnectivity()
        {
            bool flag = Utility.CheckInternetConnection();
            if (flag == false)
            {
                btnAccountLogin.Enabled = false;
                panelUserLogin.Visible = false;
            }
        }

        public void InitNodeControlPanels()
        {
            nodeDistributionsForm.TopLevel = false;
            nodeDistributionsForm.FormBorderStyle = FormBorderStyle.None;
            nodeDistributionsForm.Parent = this;


            nodeDistributionsForm.panelContainer.Controls.Add(nodeDistributionsForm.panelNodeControlDetails);
            nodeDistributionsForm.panelContainer.Controls.Add(nodeDistributionsForm.panelActorNodeDistributionValues);
            nodeDistributionsForm.panelContainer.Controls.Add(nodeDistributionsForm.panelVulnerabilityNodeDistributionValues);
            nodeDistributionsForm.panelContainer.Controls.Add(nodeDistributionsForm.panelAssetNodeDistributionValues);
            nodeDistributionsForm.panelContainer.Controls.Add(nodeDistributionsForm.panelAttackNodeDistributionValues);
            nodeDistributionsForm.panelContainer.Controls.Add(nodeDistributionsForm.panelEdgeDistributionValues);
            nodeDistributionsForm.panelContainer.Controls.Add(nodeDistributionsForm.panelObjectiveNodeValues);
            nodeDistributionsForm.panelContainer.Controls.Add(nodeDistributionsForm.panelNodeGroupDetails);
            nodeDistributionsForm.panelContainer.Controls.Add(nodeDistributionsForm.panelNodeVulnerabilityGroup);
            nodeDistributionsForm.panelContainer.Controls.Add(nodeDistributionsForm.panelEvidenceNodeValues);

            nodeDistributionsForm.panelNodeControlDetails.Dock = DockStyle.Fill;
            nodeDistributionsForm.panelActorNodeDistributionValues.Dock = DockStyle.Fill;
            nodeDistributionsForm.panelVulnerabilityNodeDistributionValues.Dock = DockStyle.Fill;
            nodeDistributionsForm.panelAssetNodeDistributionValues.Dock = DockStyle.Fill;
            nodeDistributionsForm.panelAttackNodeDistributionValues.Dock = DockStyle.Fill;
            nodeDistributionsForm.panelEdgeDistributionValues.Dock = DockStyle.Fill;
            nodeDistributionsForm.panelObjectiveNodeValues.Dock = DockStyle.Fill;
            nodeDistributionsForm.panelNodeGroupDetails.Dock = DockStyle.Fill;
            nodeDistributionsForm.panelEvidenceNodeValues.Dock = DockStyle.Fill;
            nodeDistributionsForm.panelNodeVulnerabilityGroup.Dock = DockStyle.Fill;

            nodeDistributionsForm.mainForm = this;
            nodeDistributionsForm.form_flag = 1;
        }

        private void RecentGraph_Click(object sender, EventArgs e)
        {
            PictureBox img = sender as PictureBox;
            Label lbl = img.Controls[0] as Label;

            bool flag = LoadDataFromFile(lbl.Text);
        }

        private void InitRecentGraphs()
        {
            //panelRecentGraphs.Controls.Clear();
            //panelRecentGraphs.BorderStyle = BorderStyle.None;
            //Label titleLabel = new Label();
            //titleLabel.Text = "Recent Graphs";
            //titleLabel.Font = new Font(new FontFamily("Segoe UI"), 16);
            //titleLabel.Left = 5;
            //titleLabel.Top = 10;
            //titleLabel.Width = 200;
            //titleLabel.Height = 30;
            //panelRecentGraphs.Controls.Add(titleLabel);
            //List<string> list = _settings.ResentFiles;
            //int index = 0;

            //foreach (string item in list)
            //{
            //    Panel pan = new Panel();
            //    pan.Left = 10 + 150 * (index < 5 ? index : index - 5); ;
            //    pan.Top = 50 + 150 * (index < 5 ? 0 : 1);
            //    pan.Height = 150;
            //    pan.Width = 150;

            //    //ImageData img = new ImageData();
            //    //string path = Utility.ConvertRecentImagePath(item);
            //    //img = Utility.GetImageData(path);

            //    PictureBox pictureBox = new PictureBox();
            //    if (File.Exists(Utility.ConvertRecentImagePath(item)))
            //    {
            //        using (FileStream stream = new FileStream(Utility.ConvertRecentImagePath(item), FileMode.Open, FileAccess.Read))
            //        {
            //            pictureBox.Image = Image.FromStream(stream);
            //            //pictureBox.Image = img.Image;// new Bitmap(img.ImagePath);
            //            stream.Dispose();
            //        }
            //    }

            //    pictureBox.Left = 8;
            //    pictureBox.Top = 0;
            //    pictureBox.Width = 100;
            //    pictureBox.Height = 100;
            //    pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            //    pictureBox.BorderStyle = BorderStyle.FixedSingle;
            //    pictureBox.Cursor = Cursors.Hand;
            //    pictureBox.Click += new System.EventHandler(RecentGraph_Click);
            //    Label tmp = new Label();
            //    tmp.Text = item;
            //    tmp.Visible = false;
            //    pictureBox.Controls.Add(tmp);
            //    pan.Controls.Add(pictureBox);
            //    Label label = new Label();
            //    string flname = Path.GetFileName(item);
            //    string[] arr = flname.Split('.');
            //    flname = arr[0];
            //    label.Font = new Font(new FontFamily("Segoe UI"), 10);
            //    flname = flname.Length > 15 ? flname.Substring(0, 12) + "..." : flname;
            //    label.Text = Path.GetFileName(flname);
            //    label.Width = 116;
            //    label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            //    label.Height = 30;
            //    label.Top = 100;
            //    label.Left = 0;
            //    pan.Controls.Add(label);
            //    panelRecentGraphs.Controls.Add(pan);
            //    index++;
            //}

        }

        private void InitNodeCategoryPopup()
        {
            try
            {
                string cyFileData = "";
                if (File.Exists(@"Graph\cy.data"))
                {
                    StreamReader sr = new StreamReader(@"Graph\cy.data");
                    cyFileData = sr.ReadToEnd();
                    sr.Close();
                }

                JObject cyJsonFileData = cyFileData == "" ? null : JObject.Parse(cyFileData);
                node_categories = (JArray)cyJsonFileData["node_categories"];

                this.nodePropertyForm.cmbNodeFrmCategory.Items.Clear();
                foreach (JObject nodeCategory in node_categories)
                {
                    string text = nodeCategory["text"].ToString();
                    if (text == "") continue;
                    nodePropertyForm.cmbNodeFrmCategory.Items.Add(text);


                    JArray childrens = nodeCategory["childrens"] as JArray;
                    if (childrens.Count < 1)
                    {
                        BarItem tmp = new BarItem();
                        tmp.Text = text;
                        //tmp.Checked = true;
                        tmp.Click += new System.EventHandler(this.NodeCategoryItem_Click);
                        parentBarItem1.Items.Add(tmp);
                    }
                    else
                    {
                        ParentBarItem tmpParent = new ParentBarItem();
                        tmpParent.Text = text;
                        parentBarItem1.Items.Add(tmpParent);
                        for (int i = 0; i < childrens.Count; i++)
                        {
                            BarItem tmpSub = new BarItem();
                            tmpSub.Text = childrens[i].ToString();
                            //tmpSub.Checked = true;
                            tmpSub.Click += new System.EventHandler(this.NodeCategoryItem_Click);
                            tmpParent.Items.Add(tmpSub);
                        }
                    }
                }
                nodePropertyForm.initNodeSubCategories();
            }
            catch
            { }

        }

        private void InitFontItems()
        {
            FontFamily[] ffArray = FontFamily.Families;
            foreach (FontFamily ff in ffArray)
            {
                toolBtnFontFamily.Items.Add(ff.Name);
            }
            toolBtnFontFamily.SelectedIndex = 1;
            //Build Font Sizes	
            for (int fsize = 7; fsize < 73; fsize++)
            {
                toolBtnFontSize.Items.Add(fsize.ToString());
            }
            toolBtnFontSize.SelectedIndex = 7;
        }

        private void SetupEdgeData()
        {
            try
            {
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
                //JArray edgeRStrengthData = (JArray)cyJsonFileData["edge_relationship_strength"];
                JArray edgeStrengthData = cyJsonFileData.ContainsKey("EdgeStrengthList") ?  (JArray)cyJsonFileData["EdgeStrengthList"] : new JArray();
                JArray edgeDisplayData = cyJsonFileData.ContainsKey("EdgeDisplayList") ? (JArray)cyJsonFileData["EdgeDisplayList"] : new JArray();
                JArray nodeStrength = (JArray)cyJsonFileData["impact"];
                JArray nodeAssets = (JArray)cyJsonFileData["node_assets"];

                //_settings.EdgeRelationshipStrengthData = edgeRStrengthData;
                _settings.EdgeStrengthList = edgeStrengthData;
                _settings.EdgeDisplayList = edgeDisplayData;
                _settings.NodeinherentStrengthData = nodeStrength;
                _settings.NodeimplementedStrengthData = nodeAssets;

                string[] lines = edgeRelationData.Split(',');
                Array.Sort(lines);
                nodeDistributionsForm.setRelationshipForEdge(lines);

                // set default edge relationship strength width and name
                if (edgeDisplayData.Count > 0)
                {
                    JObject obj = edgeDisplayData[0] as JObject;
                    default_edge_relationship_strength_color = obj.ContainsKey("color") ? obj["color"].ToString() : "rgb(0,0,0)";
                    default_edge_relationship_strength_width = obj.ContainsKey("width") ? obj["width"].ToString() : "1";
                }
                else
                {
                    default_edge_relationship_strength_color = "rgb(0,0,0)";
                    default_edge_relationship_strength_width = "1";
                }
            }
            catch { }

        }

        private async void AddFileToRecentFiles(string fileName, bool flag = false, string location = "local")
        {
            JavascriptResponse response = new JavascriptResponse();
            try
            {
                response = await _browser.EvaluateScriptAsync($"getCyFullData()");
                if (!response.Success)
                {
                    return;
                }
            }
            catch
            {
                return;
            }
            var jsonRes = response.Result;
            var data = ((IDictionary<String, Object>)jsonRes);
            string data1 = JsonConvert.SerializeObject(data["data1"], Formatting.Indented);
            string data2 = JsonConvert.SerializeObject(data["data2"], Formatting.Indented);

            JObject fileObj = GraphUtil.GraphFileData(data1);

            JObject obj = new JObject();
            obj["title"] = fileObj["title"];
            obj["name"] = fileObj["title"];
            obj["path"] = fileName;
            obj["description"] = fileObj["description"];
            obj["location"] = location;
            obj["date"] = fileObj["savedDateTime"];
            obj["image"] = Utility.ConvertRecentImagePath(fileName);

            int find_index = Utility.FindDataFromList(_settings.ResentFiles, fileName);
            if (find_index > -1)
            {
                _settings.ResentFiles.RemoveAt(find_index);
            }
            _settings.ResentFiles.Insert(0, obj);

            if (_settings.ResentFiles.Count > 20)
            {
                _settings.ResentFiles.RemoveAt(_settings.ResentFiles.Count - 1);
            }
            _settings.Save();

            // save screenshot image
            try
            {

                var png64 = await _browser.EvaluateScriptAsync($"cy.jpg();");
                string img_str = png64.Result as string;
                Image img = LoadBase64(img_str.Substring(22));
                Bitmap bitmap = new Bitmap(img);
                if (!File.Exists(Utility.ConvertRecentImagePath(fileName)))
                {
                    bitmap.Save(Utility.ConvertRecentImagePath(fileName), System.Drawing.Imaging.ImageFormat.Png);
                }
                else
                {
                    if (flag == true)
                    {
                        File.Delete(Utility.ConvertRecentImagePath(fileName));
                        bitmap.Save(Utility.ConvertRecentImagePath(fileName), System.Drawing.Imaging.ImageFormat.Png);
                    }

                }
            }
            catch(Exception e)
            {
                return;
            }

            InitRecentGraphs();
        }


        private bool LoadDataFromFile(string fileName, bool flag = false)
        {
            _fileName = fileName;
            string extension = Utility.GetFileExtension(_fileName);
            if (extension != "graphtmp" && _settings.graphFileExtension.Contains(extension) != true)
            {
                System.Windows.Forms.MessageBox.Show("The file extension is not allowed!");
                return false;
            }

            //read file to content string
            string content = File.ReadAllText(_fileName);
            JObject obj = DeserializeDataFromFile(content);
            if (obj["status"].ToString().ToLower() == "false")
            {
                System.Windows.Forms.MessageBox.Show(obj["message"].ToString());
                return false;
            }
            DrawGraphFile(obj["data"].ToString());
            AddFileToRecentFiles(_fileName);
            _settings.LastFile = fileName;
            _hasUnsavedChanges = false;
            _newFile = flag;
            this.InvokeIfNeed(() => { this.Text = $"CyConex: Cyber Graph Studio ({Path.GetFileName(_fileName)})"; });
            //checkUndoRedoable();
            return true;
        }

        private void SetTotalNodesLabel()
        {
            //JavascriptResponse response = await _browser.EvaluateScriptAsync("cy.nodes(':inside').length;");
            //if (response.Success)
            //{
            //    //this.InvokeIfNeed(() => { ToolStripStatusLabel.Text = $"Total nodes: {response.Result}"; });
            //}
            //else
            //{
            //    throw new Exception("Unable to execute \"cy.nodes(':inside').length;\" in browser");
            //}
            //checkUndoRedoable();
        }

        private async Task SerializeDataToFile(string filename)
        {
            JavascriptResponse response = await _browser.EvaluateScriptAsync($"getCyFullData()");
            if (!response.Success)
            {
                throw new Exception("Unable to get graph data");
            }
            var jsonRes = response.Result;
            var data = ((IDictionary<String, Object>)jsonRes);
            string data1 = JsonConvert.SerializeObject(data["data1"], Formatting.Indented);
            string data2 = JsonConvert.SerializeObject(data["data2"], Formatting.Indented);
            string data3 = JsonConvert.SerializeObject(data["data3"], Formatting.Indented);
            string data4 = JsonConvert.SerializeObject(data["data4"], Formatting.Indented);

            // Temporay Disabled
            //JObject postMetaObj = GraphAPI.PostGraphMeta();

            //if (postMetaObj == null)
            //{
            //    return;
            //}

            // put graph detail
            JObject detailObj = GraphUtil.GraphDetailData(data2);
            // Temporay Disabled
            //JObject graphDetailObj = GraphAPI.PutGraphDetail(postMetaObj["graphGUID"].ToString(), detailObj);

            // post graph file
            // Temporay Disabled
            //JObject fileObj = GraphUtil.GraphFileData(data1);
            //JObject graphFileObj = GraphAPI.PostGraphFile(fileObj );

            // put graph file data
            try
            {
                var png64 = await _browser.EvaluateScriptAsync($"cy.jpg();");
                string img_str = png64.Result as string;
                JObject fileDataObj = new JObject();
                fileDataObj["graphFileData"] = img_str;
                // Temporay Disabled
                //JObject graphFileData = GraphAPI.PutGraphFileData(graphFileObj["graphFileGUID"].ToString(), fileDataObj);

                // put graph file image 
                // Temporay Disabled
                //JObject graphFileImage = GraphAPI.PutGraphFileImage(graphFileObj["graphFileGUID"].ToString(), fileDataObj);

                //System.Windows.Forms.MessageBox.Show("Saved Graph Data");
                //Compine the strings
                string finaldata = data1 + (char)13 + "[]" + (char)13 + data2 + (char)13 + "[]" + data3 + (char)13 + "[]" + data4;

                //save to filee
                File.WriteAllText(filename, finaldata);
                _hasUnsavedChanges = false;
            }
            catch (Exception ex)
            {
                string finaldata = data1 + (char)13 + "[]" + (char)13 + data2 + (char)13 + "[]" + data3 + (char)13 + "[]" + data4;

                //save to filee
                File.WriteAllText(filename, finaldata);
                _hasUnsavedChanges = false;
            }
        }

        private bool IsBase64(string base64String)
        {

            if (string.IsNullOrEmpty(base64String) || base64String.Length % 4 != 0
               || base64String.Contains(" ") || base64String.Contains("\t") || base64String.Contains("\r") || base64String.Contains("\n"))
            {
                return false;
            }

            try
            {
                Convert.FromBase64String(base64String);
                return true;
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception);
                // Handle the exception
            }
            return false;
        }

        private string Base64Encoded(string str)
        {
            if (str == null || str == "")
            {
                return "";
            }

            if (IsBase64(str))
            {
                return str;
            }
            else
            {
                var textBytes = System.Text.Encoding.UTF8.GetBytes(str);
                string desc = System.Convert.ToBase64String(textBytes);

                return desc;
            }
        }

        private async void DrawGraphFile(string content)
        {

            string[] separatingStrings = { "[]" };

            string[] sections = content.Split(separatingStrings, System.StringSplitOptions.RemoveEmptyEntries);

            // Remove Listerners
            _browser.ExecuteScriptAsync("cy.removeListener('data');");
            _browser.ExecuteScriptAsync("cy.removeListener('data', 'node');");

            //Reset Graph
            _browser.ExecScriptAsync("resetGraph();");

            //Section 0 Graph data
            _browser.ExecScriptAsync($"cy.data({sections[0]})");

            //Section 1 node and edge data           
            if (sections.Count() > 1 && sections[1] != null)
            {
                _browser.ExecScriptAsync($"drawGraph({sections[1]})");
            }

            //Section 2 graph background images         
            if (sections.Count() > 2 && sections[2] != null)
            {
                _browser.ExecScriptAsync($"addBackgrounds('{sections[2]}')");
            }

            if (sections.Count() > 3 && sections[3] != null)
            {

                _browser.ExecScriptAsync($"setBackgroundColor({sections[3]})");
            }


            // Renable listerners listerners
            _browser.ExecuteScriptAsync("cy.addListener('data', 'node', nodeDataChanged);");
            _browser.ExecuteScriptAsync("cy.on('data', cy_data);");

            //Temp disable and enable auto calc to enasure data is loaded from file
            bool TempBool = GraphCalcs.autoCalculate;
            GraphCalcs.autoCalculate = false;
            await GraphUtil.SyncFromGraph();
            GraphCalcs.RecalculateAll();
            GraphCalcs.autoCalculate = TempBool;

            if (_settings.AutoCenterGraph == true)
            {
                _browser.ExecScriptAsync("cy.animate({ fit:{}, zoom: 2}, { duration: 200 });");
            }


            JsonSerializerSettings jsonSerializerSettings = new JsonSerializerSettings
            {
                Error = delegate (object sender, Newtonsoft.Json.Serialization.ErrorEventArgs args)
                {
                    Debug.WriteLine($"{args.ErrorContext}");
                    args.ErrorContext.Handled = false;
                },
                DateTimeZoneHandling = DateTimeZoneHandling.Local
            };
            _graphProperties = JsonConvert.DeserializeObject<GraphProperties>(sections[0], jsonSerializerSettings);
            this.InvokeIfNeed(() =>
            {
                this.nodePropertyForm.setGraphPropertyData(_graphProperties);
            });
        }

        private JObject DeserializeDataFromFile(string content)
        {
            JObject obj = new JObject();
            obj["status"] = true;
            obj["message"] = "success";
            //Find section seprator "[]" and split into seprate sections 
            string[] separatingStrings = { "[]" };

            string[] sections = content.Split(separatingStrings, System.StringSplitOptions.RemoveEmptyEntries);

            // handle old file type
            JObject json_1 = new JObject();
            var json_2 = new JArray();

            if (sections[0] != null)
            {
                try
                {
                    json_1 = JObject.Parse(sections[0]);
                }
                catch (JsonReaderException ex)
                {
                    obj["status"] = false;
                    obj["message"] = ex.Message;
                    return obj;
                }


            }
            if (sections.Count() > 1 && sections[1] != null)
            {
                try
                {
                    json_2 = JArray.Parse(sections[1]);
                }
                catch (Exception ex)
                {
                    obj["status"] = false;
                    obj["message"] = ex.ToString();
                    return obj;
                }
            }

            try
            {
                json_1["description"] = Base64Encoded(json_1["description"].ToString());
                json_1["notes"] = Base64Encoded(json_1["notes"].ToString());
                for (int i = 0; i < json_2.Count; i++)
                {
                    string tmp_str = json_2[i].ToString();
                    JObject tmp_obj = JObject.Parse(tmp_str);
                    tmp_obj["data"]["description"] = Base64Encoded((string)tmp_obj["data"]["description"]);
                    tmp_obj["data"]["note"] = Base64Encoded((string)tmp_obj["data"]["note"]);
                    json_2[i] = tmp_obj.ToString(Formatting.None);
                }
                string section_1 = JsonConvert.SerializeObject(json_1);
                string section_2 = json_2.ToString(Formatting.None);
            }
            catch (Exception ex)
            {
                obj["status"] = false;
                obj["message"] = ex.ToString();
                return obj;
            }

            obj["data"] = content;
            return obj;
        }

        private async void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                if (_hasUnsavedChanges)
                {
                    if (!_newFile && _settings.AutoSaveOnChanges)
                    {
                        await SerializeDataToFile(_fileName);
                    }
                    else
                    {
                        //This is defalt answer
                        e.Cancel = false;
                        DialogResult result = NetGraphMessageBox.MessageBoxEx(this, "There are unsaved changes on the current graph. Save before close application?", "Unsaved changes", MessageBoxButtons.YesNoCancel, MessageBoxIconEx.Question, defaultButton: MessageBoxDefaultButton.Button3, 468, 234);
                        if (result == DialogResult.Cancel)
                        {
                            e.Cancel = true;
                            return;
                        }
                        if (result == DialogResult.Yes)
                        {
                            e.Cancel = false;
                            saveGraphFile();
                        }
                    }
                }
            }
        }

        private void removeResentFile(object sender)
        {
            string fileName = (sender as ToolStripItem).Tag.ToString();
            if (!File.Exists(fileName))
            {
                if (NetGraphMessageBox.MessageBoxEx(this, $"File \"{fileName}\" not found. Remove it from recent files?", "File not found", MessageBoxButtons.YesNo, MessageBoxIconEx.Question) == DialogResult.Yes)
                {
                    _settings.ResentFiles.RemoveAt(Utility.FindDataFromList(_settings.ResentFiles, fileName));
                }
            }
        }

        private void Item_Click(object sender, EventArgs e)
        {
           removeResentFile(sender);
        }

        //private List<Edge> GetEdgesForNode(Node node, List<Edge> allEdges, List<Node> allNodes)
        //{
        //    List<Edge> edges = allEdges.Where(item => item.Source == node.ID && !item.IsSelfLoop && !item.SelectedToProcessing).ToList();
        //    //List<DataEdge> edges = _dataGraph.OutEdges(vertex).Where(item => !item.IsSelfLoop && !item.SelectedToProcessing && item.Source.Enabled && item.Target.Enabled && item.Enabled).ToList();
        //    List<Edge> retval = new List<Edge>(edges);
        //    foreach (var edge in edges)
        //    {
        //        Node targetNode = allNodes.FirstOrDefault(item => item.ID == edge.Target);
        //        retval.AddRange(GetEdgesForNode(targetNode, allEdges, allNodes).Except(retval));
        //    }
        //    return retval;

        //}

        public void UpdatecalculationLog(DateTime startTime, List<(DateTime, string)> calculationLog)
        {
            calculationLog.Add((DateTime.Now, $"Calculation finished"));
            DateTime stopTime = DateTime.Now;
            GraphCalcs.s_CalculationLog.Add((stopTime, $"Calculation took {(stopTime - startTime).TotalMilliseconds} msec"));
            statusBarCalcTime.Text = $"{(stopTime - startTime).TotalMilliseconds} msec";
            UpdateLogList();

            if (_settings.SaveCalculationLog)
            {
                if (_newFile)
                {
                    File.WriteAllLines("calculation.log", GraphCalcs.s_CalculationLog.Select(item => item.Item1.ToString("HH:mm:ss.fff") + "\t" + item.Item2));
                }
                else
                {
                    File.WriteAllLines(Path.GetFileNameWithoutExtension(_fileName) + "_calculation.log", GraphCalcs.s_CalculationLog.Select(item => item.Item1.ToString("HH:mm:ss.fff") + "\t" + item.Item2));
                }
            }
        }
        private void UpdateLogList()
        {
            this.InvokeIfNeed(() =>
            {
                if (calcLogForm == null || calcLogForm.IsDisposed)
                    calcLogForm = new CalcLogForm();

                calcLogForm.CalculationLog_listView.Items.Clear();
                calcLogForm.CalculationLog_listView.Items.AddRange(GraphCalcs.s_CalculationLog.Select(item => new ListViewItem(
                new string[]
                    {
                        item.Item1.ToString("HH:mm:ss.fff"), item.Item2
                    },
                    item.Item2.StartsWith("Processing source") ? 1 : (item.Item2.Contains("Skipping") ? 2 : 0))).ToArray()
                );
                calcLogForm.CalculationLog_listView.Columns[1].AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
            });
        }

        private void ConnectedNodes_dataGridView_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            e.Handled = false;
        }

        private void underAction()
        {
            _browser.ExecScriptAsync("undoCy();");
        }

        private void redoAction()
        {
            _browser.ExecScriptAsync("redoCy();");
        }


        private void ShowcalculationLog_toolStripButton_CheckedChanged(object sender, EventArgs e)
        {
            //panelCalcLog.Visible = !ShowCalculationLog_toolStripButton.Checked;
            _settings.ShowCalculationLog = ShowCalculationLog_toolStripButton.Checked;
            _settings.Save();
        }

        private void changeLayout(string info)
        {
            _browser.ExecScriptAsync($"var layout = cy.elements().layout({info}); layout.run();");
        }

        private async Task<Task<bool>> GraphSchemaCheck(bool silent)
        {
            //string GraphStatus = "";
            //string messageText = "The graph cannot be calculated, the following errors were found:" + (char)13 + (char)13;
            //await GraphUtil.SyncFromGraph();
            //GraphStatus = GraphUtil.CheckGraphSchema();

            //if (GraphStatus == "OK")
            //    return Task.FromResult(true);
            //else if (GraphStatus != "OK" && silent == false)
            //{
            //    string[] responce = GraphStatus.Split(',');

            //    if (responce[0].ToLower() == "false")
            //        messageText += "    There must be at least one Actor Node on the Graph." + (char)13;
            //    if (responce[1].ToLower() == "false")
            //        messageText += "    There must be at least one Attack Node on the Graph." + (char)13;
            //    if (responce[2].ToLower() == "false")
            //        messageText += "    There must be at least one Vulnerability Node on the Graph." + (char)13;
            //    if (responce[3].ToLower() == "false")
            //        messageText += "    There must be at least one Asset Node on the Graph." + (char)13;
            //    DialogResult result = NetGraphMessageBox.MessageBoxEx(this, messageText, "Graph Error", MessageBoxButtons.OK, MessageBoxIconEx.Error, button1Text: "OK", defaultButton: MessageBoxDefaultButton.Button1);
            //    return Task.FromResult(false);

            //}
            //else if (GraphStatus != "OK" && silent == true)
            //    return Task.FromResult(true);

            return Task.FromResult(true);
        }

        private void listView1_ItemDrag(object sender, ItemDragEventArgs e)
        {

        }

        private void MainBrowser_panel_DragEnter(object sender, DragEventArgs e)
        {

        }

        private void NewGraph()
        {
            _browser.ExecScriptAsync("resetGraph();");
            InitializeGraph();
            _newFile = true;
            _hasUnsavedChanges = false;
            _fileName = "layout.graph";
            this.Text = "CyConex Graph Studio";


        }

        public void InitializeGraph()
        {
            // Reset graph variables, lists etc across the app
            this.InitializeValues();
            GraphCalcs.InitializeValues();
            GraphUtil.InitializeValues();
            calcLogForm.CalculationLog_listView.Items.Clear();
        }

        public void InitializeValues()
        {
            // Reset variables, lists etc
            _selectedNodes.Clear();
            _selectedEdges.Clear();
        }


        private void saveGraphToFile()
        {

        }

        private void saveGraphFile()
        {
            if (_newFile)
            {
                using (SaveFileDialog saveFileDialog = new SaveFileDialog()
                {
                    Filter = "All files (*.*)|*.*|Graph files (*.graph)|*.graph",
                    FilterIndex = 2,
                    Title = "Save graph",
                    FileName = String.IsNullOrEmpty(_fileName) ? "layout.graph" : Path.GetFileName(_fileName)
                })
                {
                    if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
                    {
                        _fileName = saveFileDialog.FileName;
                        //_userSaveFileOnClose = true;
                    }
                    else
                    {
                        //_userSaveFileOnClose = false;
                        return;
                    }
                }
            }
            //_userSaveFileOnClose = true;
            this.Text = $"CyConex Cyber Graph Studio: ({Path.GetFileName(_fileName)})";
            if (_hasUnsavedChanges)
            {
                _browser.ExecScriptAsync("increaseRevision();");
            }
            _ = SerializeDataToFile(_fileName);
            AddFileToRecentFiles(_fileName, true);
            _settings.LastFile = _fileName;
            _newFile = false;
            _hasUnsavedChanges = false;
            this.ribbonControlAdv1.SelectedTab = ribbonTabHome;
            new System.Threading.Thread(() =>
            {
                System.Threading.Thread.CurrentThread.IsBackground = true;
                /* run your code here */
                this.InvokeIfNeed(() =>
                {
                    statusBarImage.BackgroundImage = ilForm1.Images[1];
                    statusBarImage.Refresh();
                });
                System.Threading.Thread.Sleep(1000);
                this.InvokeIfNeed(() =>
                {
                    statusBarImage.BackgroundImage = null;

                });
            }).Start();

            Utility.DeleteTempFiles();
        }

        private void backStageButton3_Click(object sender, EventArgs e)
        {

        }

        private void saveAsGraphFile()
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog()
            {
                Title = "Save as graph",
                Filter = "All files (*.*)|*.*|Graph files (*.graph)|*.graph",
                FilterIndex = 2,
                FileName = Path.GetFileNameWithoutExtension(Path.GetFileName(_fileName)) + ".graph"
            })
            {
                if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
                {
                    _fileName = saveFileDialog.FileName;
                    this.Text = $"CyConex Cyber Graph Studio: ({Path.GetFileName(_fileName)})";
                    SerializeDataToFile(_fileName);
                    _newFile = false;
                    _hasUnsavedChanges = false;

                    AddFileToRecentFiles(_fileName, true);
                }
            }
            //checkUndoRedoable();
            this.ribbonControlAdv1.SelectedTab = ribbonTabHome;
        }

        private void settingFormShow()
        {
            //Temp Commented Out
            using (SettingsForm settingsForm = new SettingsForm(ref _settings))
            {
                settingsForm.ShowDialog(this);
                _settings.Save();
                PassSettingsToBrowser();

                node_categories = settingsForm.node_categories;
                UpdateSettingsItemToMainForm();
            }
        }

        private void UpdateSettingsItemToMainForm()
        {
            // set category and subcategory for node
            nodePropertyForm.initNodeMainCategories();
        }



        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            _browser?.ShowDevTools();
        }


        private void animateGraph()
        {
            _browser.ExecScriptAsync("cy.animate({ fit:{}, zoom: 2}, { duration: 200 });");
        }

        private void toolStripButton10_Click(object sender, EventArgs e)
        {
            animateGraph();
        }

        private void toolStripButton11_Click(object sender, EventArgs e)
        {
            changeLayout("{ name: 'grid', animate: true }");
        }

        private void toolStripButton12_Click(object sender, EventArgs e)
        {
            changeLayout("{ name: 'circle', animate: true }");
        }

        private void toolStripButton13_Click(object sender, EventArgs e)
        {
            changeLayout("{ name: 'concentric', animate: true }");
        }

        private void toolStripButton14_Click(object sender, EventArgs e)
        {
            _browser.ExecScriptAsync(@"
                    var layout = cy.elements().layout({
                    name: 'dagre',
                    animate: true,
                    animationDuration: 1000,
                    });
                layout.run();
                ");
        }

        private void toolStripButton15_Click(object sender, EventArgs e)
        {
            changeLayout("{ name: 'breadthfirst', animate: true }");
        }

        private void toolStripButton16_Click(object sender, EventArgs e)
        {
            _browser.ExecScriptAsync(@"
                    var layout = cy.elements().layout({
                    name: 'cola',
                    animate: true,
                    maxSimulationTime: 1000,
                    fit: true,
                    avoidOverlap: true,
                    edgeLength: 500,
                    });
                layout.run();
                ");
        }


        private void deleteSelectedNodes()
        {
            int totalNodes = _selectedNodes.Count;
            int totalEdges = _selectedEdges.Count;
            int totalObjects = totalNodes + totalEdges;

            if (_settings.ShowDeleteNodeDialog)
            {
                if (totalObjects > 0)
                {

                    if (NetGraphMessageBox.MessageBoxEx(this, $"Delete selected object{(totalObjects > 1 ? "s" : "")}?", $"Delete object{(totalObjects > 1 ? "s" : "")}", MessageBoxButtons.YesNo, MessageBoxIconEx.Question, out bool dontShowAgainChecked, defaultButton: MessageBoxDefaultButton.Button2) != DialogResult.Yes)
                    {
                        return;
                    }
                    _settings.ShowDeleteNodeDialog = !dontShowAgainChecked;
                    _settings.Save();
                }
            }
            for (int i = 0; i < _selectedEdges.Count; i++)
            {
                Edge edge = _selectedEdges.ElementAt(i).Value;
                GraphUtil.DeleteEdgeWithID(edge.ID);
                _browser.ExecScriptAsync($"deleteEdgeWithID('{edge.ID}');");
            }
            _selectedEdges.Clear();

            for (int i = 0; i < _selectedNodes.Count; i++)
            {
                Node node = _selectedNodes.ElementAt(i).Value;
                GraphUtil.DeleteNodeWithID(node.ID);
                _browser.ExecScriptAsync($"deleteNodeWithID('{node.ID}');");
            }
            _selectedNodes.Clear();
        }

        private void toolStripButton21_Click(object sender, EventArgs e)
        {

        }

        private void clearGraph()
        {
            if (_settings.ShowClearAllNodesAndLinks)
            {
                if (NetGraphMessageBox.MessageBoxEx(this, "All nodes and edges will be deleted.\r\nContinue?", "Clear all", MessageBoxButtons.YesNo, MessageBoxIconEx.Question, out bool dontShowAgainChecked, defaultButton: MessageBoxDefaultButton.Button2) != DialogResult.Yes)
                {
                    return;
                }
                _settings.ShowClearAllNodesAndLinks = !dontShowAgainChecked;
                _settings.Save();
            }
            _hasUnsavedChanges = true;
            _browser.ExecScriptAsync("cy.elements().remove();");
            _selectedNodes.Clear();
            _selectedEdges.Clear();
            checkUndoRedoable();
        }

        public async void addNodeQuick(string type, string shape, string inher_strength = "Not Defined", string implement_strength = "Not Defined", string title = "", string description = "")
        {

            IsShiftKey = Keyboard.IsKeyDown(Key.LeftShift).ToString().ToLower() == "true";
            IsF1key = Keyboard.IsKeyDown(Key.F1).ToString().ToLower() == "true";

            var NodeColor = "";
            var NodeTitle = "";
            var nodeBehaviour = "";

            if (type.ToLower() == "control")
            {
                NodeTitle = "Control";
                shape = "square";
                NodeColor = $"rgb(0,0,0)";
                inher_strength = "Not Assessed";
                implement_strength = "Not Assessed";
            }

            if (type.ToLower() == "group")
            {
                NodeTitle = "Group";
                shape = "square";
                NodeColor = $"rgb(0,0,0)";
                inher_strength = "Sum of all Fundamental strength values";
                implement_strength = "Sum of all Implementation Strength values";
                nodeBehaviour = "Sum";
            }

            if (type.ToLower() == "objective")
            {
                NodeTitle = "Objective";
                shape = "square";
                NodeColor = $"rgb(0,0,0)";
                inher_strength = "Sum of Control Maximums";
                implement_strength = "Sum of all Implementation Strength values";
                nodeBehaviour = "Sum";

            }

            if (type.ToLower() == "asset")
            {
                NodeTitle = "Asset";
                shape = "square";
                NodeColor = $"rgb(0,0,0)";

            }

            if (type.ToLower() == "attack")
            {
                NodeTitle = "Attack";
                shape = "square";
                NodeColor = $"rgb(0,0,0)";

            }

            if (type.ToLower() == "actor")
            {
                NodeTitle = "Actor";
                shape = "square";
                NodeColor = $"rgb(0,0,0)";

            }

            if (type.ToLower() == "evidence")
            {
                NodeTitle = "Evidence";
                shape = "square";
                NodeColor = $"rgb(0, 0, 0)";
            }

            if (type.ToLower() == "info")
            {
                NodeTitle = "Info";

            }

            if (type.ToLower() == "vulnerability")
            {
                NodeTitle = "Vulnerability";
                shape = "square";
                NodeColor = $"rgb(0,0,0)";
                inher_strength = "Not Assessed";
                implement_strength = "Not Assessed";

            }

            if (type.ToLower() == "vulnerability-group")
            {
                NodeTitle = "Vulnerability Group";
                shape = "square";
                NodeColor = $"rgb(0,0,0)";
                inher_strength = "Not Assessed";
                implement_strength = "Not Assessed";

            }

            ImageData img = Utility.DefaultNodeImage(type);
            var textBytes = System.Text.Encoding.UTF8.GetBytes(img.ImagePath);
            string path = System.Convert.ToBase64String(textBytes);

            JObject obj = new JObject();

            obj["title"] = title == "" ? NodeTitle : title;
            obj["description"] = description;
            obj["titleSize"] = 10;
            obj["height"] = img.ImageHeight;
            obj["width"] = img.ImageWidth;
            obj["color"] = NodeColor;
            obj["border_color"] = $"'rgb(0,0,0)'";
            obj["shape"] = shape;
            obj["controltype"] = type.ToLower();
            obj["objectiveTargetType"] = inher_strength;
            obj["objectiveTargetValue"] = "0";
            obj["implementedStrength"] = implement_strength;
            obj["controlBaseScoreAssessmentStatusManual"] = "false";
            obj["textColor"] = $"rgb(0,0,0)";
            obj["image"] = img.Data;
            obj["imagePath"] = path;
            obj["background_opacity"] = 0;
            obj["border_opacity"] = 0;
            obj["controlBaseScore"] = 0;
            obj["nodeBehaviour"] = nodeBehaviour;
            obj["isShift"] = IsShiftKey.ToString().ToLower();
            obj["isF1"] = IsF1key.ToString().ToLower();

            JavascriptResponse nodeID = await _browser.EvaluateScriptAsync($"addNodeWithObject({JsonConvert.SerializeObject(obj)})");

            _browser.ExecScriptAsync($"adjustNodePosition();");
            try
            {
                if (nodeID.Result == null) return;
                obj["id"] = nodeID.Result.ToString();
                GraphUtil.AddNodeWithObject(obj);
                _hasUnsavedChanges = true;
            }
            catch { };

            SetEdgeDraw(false);

            if (btnNodeAutoSize.Checked)
            {
                int node_count = GraphUtil.CountOfAllAncestorNodes(nodeID.Result.ToString());
                decimal node_size = Decimal.Parse(Utility.CalculateSizeIncreaseFactor(node_count).ToString());
                node_size = node_size < _settings.DefaultNodeSize ? _settings.DefaultNodeSize : node_size;
                _browser.ExecScriptAsync($"setElementData('{nodeID.Result.ToString()}', 'width', '{node_size}');");
                _browser.ExecScriptAsync($"setElementData('{nodeID.Result.ToString()}', 'height', '{node_size}');");
            }
        }

        public async void AddNodeFromRepository(Node node)
        {
            ImageData img = Utility.DefaultNodeImage(node.Type.Name);
            var textBytes = System.Text.Encoding.UTF8.GetBytes(img.ImagePath);
            string path = System.Convert.ToBase64String(textBytes);

            JObject obj = new JObject();

            obj["title"] = node.Title;
            obj["description"] = node.description;
            obj["titleSize"] = node.TitleSize;
            obj["height"] = img.ImageHeight;
            obj["width"] = img.ImageWidth;
            obj["color"] = "rgb(" + node.Color.R + "," + node.Color.G + "," + node.Color.B + ")";
            obj["border_color"] = "rgb(" + node.BorderColor.R + "," + node.BorderColor.G + "," + node.BorderColor.B + ")";
            obj["shape"] = node.Shape.Name;
            obj["controltype"] = node.Type.Name;
            obj["objectiveTargetType"] = node.InherentStrengthValue;
            obj["objectiveTargetValue"] = "0";
            obj["implementedStrength"] = node.ImplementedStrength;
            obj["controlBaseScoreAssessmentStatusManual"] =node.controlBaseScoreAssessmentStatus;
            obj["textColor"] = "rgb(" + node.TitleTextColor.R + "," + node.TitleTextColor.G + "," + node.TitleTextColor.B + ")";
            obj["image"] = node.NodeImageData == null || node.NodeImageData == "" ? 
                    img.Data : node.NodeImageData;
            obj["imagePath"] = path;
            obj["background_opacity"] = 0;
            obj["border_opacity"] = 0;
            obj["controlBaseScore"] = node.controlBaseScore;
            obj["nodeBehaviour"] = node.nodeBehaviour;
            obj["isShift"] = IsShiftKey.ToString().ToLower();
            obj["isF1"] = IsF1key.ToString().ToLower();

            JavascriptResponse nodeID = await _browser.EvaluateScriptAsync($"addNodeWithObject({JsonConvert.SerializeObject(obj)})");

            _browser.ExecScriptAsync($"adjustNodePosition();");
            try
            {
                if (nodeID.Result == null) return;
                obj["id"] = nodeID.Result.ToString();
                GraphUtil.AddNodeWithObject(obj);
                _hasUnsavedChanges = true;
            }
            catch { };

            SetEdgeDraw(false);

            if (btnNodeAutoSize.Checked == true)
            {
                int node_count = GraphUtil.CountOfAllAncestorNodes(nodeID.Result.ToString());
                decimal node_size = Decimal.Parse(Utility.CalculateSizeIncreaseFactor(node_count).ToString());
                node_size = node_size < _settings.DefaultNodeSize ? _settings.DefaultNodeSize : node_size;
                _browser.ExecScriptAsync($"setElementData('{node.ID}', 'width', '{node_size}');");
                _browser.ExecScriptAsync($"setElementData('{node.ID}', 'height', '{node_size}');");
            }
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            addNodeQuick("Control", "Control");
        }

        private async void newGraphAction()
        {
            if (_hasUnsavedChanges)
            {
                DialogResult result = NetGraphMessageBox.MessageBoxEx(this, "There are unsaved changes on the current graph. Save before create new graph?",
                    "Unsaved changes", MessageBoxButtons.YesNoCancel, MessageBoxIconEx.Question, defaultButton: MessageBoxDefaultButton.Button3, 468, 234);
                if (result == DialogResult.Cancel)
                {
                    return;
                }
                if (result == DialogResult.Yes)
                {
                    btnSaveLocal.PerformClick();
                }
            }
            
            await _browser.EvaluateScriptAsync($"disableDrawMode();");
            await _browser.EvaluateScriptAsync($"clearImageBackground()");
            await _browser.EvaluateScriptAsync($"disableDrawMode();");
            this.drawEdges_ToolStrip.Checked = false;
            NewGraph();
            //backStageView1.IsVisible = false;

            this.nodePropertyForm.nodeTabPage.TabVisible = false;
            this.nodePropertyForm.edgeTabPage.TabVisible = false;
            this.nodePropertyForm.graphTabPage.TabVisible = true;
            this.ribbonControlAdv1.SelectedTab = this.ribbonTabHome;
            checkUndoRedoable();
        }


        private void toolStripButton3_ButtonClick(object sender, EventArgs e)
        {
            
        }


        public ConcurrentDictionary<string, Node> getSelectedNodes()
        {
            return this._selectedNodes;
        }

        public ConcurrentDictionary<string, Edge> getSelectedEdges()
        {
            return this._selectedEdges;
        }


        private void showNodeManagerForm()
        {
            this.InvokeIfNeed(() =>
            {
                if (AuthAPI._account_token == null || AuthAPI._tenant_guid == "")
                {
                    System.Windows.Forms.MessageBox.Show("You have to login and select Tenant, firstly");
                }
                else
                {
                    RepoNodeManagerSearchForm nodeManagerForm = new RepoNodeManagerSearchForm();
                    Point p = this.Location;
                    nodeManagerForm.ShowDialogWithBrowser(this, _browser, p);
                }
            });
        }


        private void toolStripButton32_Click(object sender, EventArgs e)
        {
            showNodeManagerForm();
        }

        public static Image LoadBase64(string base64)
        {
            if (base64 == null || Utility.IsBase64String(base64) == false)
            {
                return null;
            }
            byte[] bytes = Convert.FromBase64String(base64);
            Image image;
            using (MemoryStream ms = new MemoryStream(bytes))
            {
                image = Image.FromStream(ms);
            }
            return image;

        }

        private void toolStripButton1_Click_1(object sender, EventArgs e)
        {
            addNodeQuick("Attack", "attack");
        }


        private void toolSuggested_Click(object sender, EventArgs e)
        {
            AddSuggestedNodes();
        }

        private void toolSugObjectives_Click(object sender, EventArgs e)
        {
            AddSuggestedNodes("objective");
        }

        private void toolSugControls_Click(object sender, EventArgs e)
        {
            AddSuggestedNodes("control");
        }

        private void toolSugAttacks_Click(object sender, EventArgs e)
        {
            AddSuggestedNodes("attack");
        }

        private void toolSugGroups_Click(object sender, EventArgs e)
        {
            AddSuggestedNodes("group");
        }

        private void addSuggestedNodeAndEdge(Node item, string selected_node_id)
        {

            Random random = new Random();

            JObject obj = new JObject();
            obj["title"] = item.Title;
            obj["titleSize"] = item.TitleSize;
            obj["size"] = item.Size;
            obj["color"] = $"'rgb({item.Color.R}, {item.Color.G}, {item.Color.B})'";
            obj["border_color"] = $"'rgb({item.BorderColor.R}, {item.BorderColor.G}, {item.BorderColor.B})'";
            obj["shape"] = item.Shape.Name;
            obj["x"] = random.Next() % 500;
            obj["y"] = random.Next() % 500;
            obj["description"] = item.description;
            obj["controlref"] = item.frameworkReference;
            obj["domain"] = item.Domain;
            obj["subdomain"] = item.SubDomain;
            obj["refurl"] = item.ReferenceURL;
            obj["controlframework"] = item.frameworkName;
            obj["controlframeworkversion"] = item.ControlFrameworkVersion;
            obj["controltype"] = item.Type.Name.ToLower();
            obj["objectiveTargetType"] = item.objectiveTargetType;
            obj["objectiveTargetValue"] = item.objectiveTargetValue;
            obj["implementedStrength"] = item.ImplementedStrength;
            obj["textColor"] = $"'rgb({item.TitleTextColor.R},{item.TitleTextColor.G},{item.TitleTextColor.B})'";
            obj["controlBaseScore"] = item.controlBaseScore;
            obj["image"] = item.NodeImageData;
            obj["imagePath"] = item.ImagePath;
            obj["position"] = item.NodeTitlePosition;
            obj["note"] = item.Note;
            obj["borderWidth"] = "1";
            obj["masterID"] = item.ID;
            obj["selectedMasterID"] = _selected_node_master_id;
            obj["is_suggested"] = true;
            obj["selected_node_id"] = selected_node_id;
            if (_selected_node_master_id != item.ID)
            {
                _browser.ExecScriptAsync($"addNodeWithObject({JsonConvert.SerializeObject(obj)})");

                if (btnNodeAutoSize.Checked == true)
                {
                    int node_count = GraphUtil.CountOfAllAncestorNodes(item.ID.ToString());
                    decimal node_size = Decimal.Parse(Utility.CalculateSizeIncreaseFactor(node_count).ToString());
                    node_size = node_size < _settings.DefaultNodeSize ? _settings.DefaultNodeSize : node_size;
                    _browser.ExecScriptAsync($"setElementData('{item.ID}', 'width', '{node_size}');");
                    _browser.ExecScriptAsync($"setElementData('{item.ID}', 'height', '{node_size}');");
                }
            }
            else
            {
                _browser.ExecScriptAsync($"addLoopEdge({JsonConvert.SerializeObject(obj)})");
            }
        }

        private void AddSuggestedNodes(string type = "")
        {
            List<Node> lst = new List<Node>();
            foreach (Node node in _suggested_nodes)
            {
                if (type == "")
                {
                    lst.Add(node);
                }
                else if (type == node.Type.Name.ToLower())
                {
                    lst.Add(node);
                }
            }

            foreach (Node node in lst)
            {
                addSuggestedNodeAndEdge(node, _selected_node_id);
            }

            switch (type)
            {
                case "control":
                    toolSugControls.Enabled = false;
                    break;
                case "group":
                    toolSugGroups.Enabled = false;
                    break;
                case "attack":
                    toolSugAttacks.Enabled = false;
                    break;
                case "asset":
                    break;
                case "objective":
                    toolSugObjectives.Enabled = false;
                    break;
                case "actor":
                    toolSugActor.Enabled = false;
                    break;
                default:
                    toolSuggested.Enabled = false;
                    toolSugControls.Enabled = false;
                    toolSugGroups.Enabled = false;
                    toolSugAttacks.Enabled = false;
                    toolSugObjectives.Enabled = false;
                    toolSugActor.Enabled = false;
                    break;
            }

            if (toolSugControls.Enabled == false && toolSugGroups.Enabled == false &&
                toolSugAttacks.Enabled == false && toolSugObjectives.Enabled == false)
            {
                toolSuggested.Enabled = false;
            }
        }


        private void toolStripButton57_Click(object sender, EventArgs e)
        {
            addNodeQuick("Actor", "actor");
        }

        private void toolBtnFontFamily_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_selected_node_id == null || _selected_node_id == "")
            {
                return;
            }

            string font_family = toolBtnFontFamily.Text;
            _browser.ExecScriptAsync($"setElementData('{_selected_node_id}', 'fontFamily', '{font_family}');");
        }

        private void toolBtnFontSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_selected_node_id == null || _selected_node_id == "")
            {
                return;
            }

            string font_size = toolBtnFontSize.Text;
            _browser.ExecScriptAsync($"setElementData('{_selected_node_id}', 'titleSize', '{font_size}');");
        }


        private void toolBtnFontBold_Click(object sender, EventArgs e)
        {
            
            if (_selectedNodes.Count < 1)
            {
                return;
            }

            Node node = _selectedNodes.ElementAt(0).Value;
            toolBtnFontBold.Checked = !toolBtnFontBold.Checked;
            var font_bold = toolBtnFontBold.Checked ? "600" : "100";
            _browser.ExecScriptAsync($"setElementData('{node.ID}', 'fontWeight', '{font_bold}');");
        }

        private void toolBtnFontItalic_Click(object sender, EventArgs e)
        {
            
            if (_selectedNodes.Count < 1)
            {
                return;
            }

            Node node = _selectedNodes.ElementAt(0).Value;
            //var node_json = await _browser.EvaluateScriptAsync($"getElementData('{node.ID}','fontStyle');");
            toolBtnFontItalic.Checked = !toolBtnFontItalic.Checked;
            var font_style = !toolBtnFontItalic.Checked ? "normal" : "italic";
            _browser.ExecScriptAsync($"setElementData('{node.ID}', 'fontStyle', '{font_style}');");
        }

        private void toolBtnFontColor_Click(object sender, EventArgs e)
        {
            
            if (_selectedNodes.Count < 1)
            {
                return;
            }

            Node node = _selectedNodes.ElementAt(0).Value;
            ColorDialog cd = new ColorDialog();
            if (cd.ShowDialog(this) == DialogResult.OK)
            {
                node.Color = cd.Color;
                _browser.ExecScriptAsync($"setElementData('{node.ID}', 'titleTextColor', 'rgb({cd.Color.R},{cd.Color.G},{cd.Color.B})');");
            }
        }

        private void toolBtnTextLeft_Click(object sender, EventArgs e)
        {
            
            if (_selectedNodes.Count < 1)
            {
                return;
            }


            Node node = _selectedNodes.ElementAt(0).Value;
            node.NodeTitlePosition = "Left";
            _browser.ExecScriptAsync($"setElementData('{node.ID}', 'position', '{node.NodeTitlePosition}');");
            _browser.ExecScriptAsync($"setElementData('{node.ID}', 'position', '{node.NodeTitlePosition}');");
        }

        private void toolBtnTextTop_Click(object sender, EventArgs e)
        {
            
            if (_selectedNodes.Count < 1)
            {
                return;
            }

            Node node = _selectedNodes.ElementAt(0).Value;
            node.NodeTitlePosition = "Top";
            _browser.ExecScriptAsync($"setElementData('{node.ID}', 'position', '{node.NodeTitlePosition}');");
        }

        private void toolBtnTextCenter_Click(object sender, EventArgs e)
        {
            
            if (_selectedNodes.Count < 1)
            {
                return;
            }

            Node node = _selectedNodes.ElementAt(0).Value;
            node.NodeTitlePosition = "Above";
            _browser.ExecScriptAsync($"setElementData('{node.ID}', 'position', '{node.NodeTitlePosition}');");
        }

        private void toolBtnTextBottom_Click(object sender, EventArgs e)
        {
            
            if (_selectedNodes.Count < 1)
            {
                return;
            }

            Node node = _selectedNodes.ElementAt(0).Value;
            node.NodeTitlePosition = "Bottom";
            _browser.ExecScriptAsync($"setElementData('{node.ID}', 'position', '{node.NodeTitlePosition}');");
        }

        private void toolBtnTextRight_Click(object sender, EventArgs e)
        {
            
            if (_selectedNodes.Count < 1)
            {
                return;
            }

            Node node = _selectedNodes.ElementAt(0).Value;
            node.NodeTitlePosition = "Right";
            _browser.ExecScriptAsync($"setElementData('{node.ID}', 'position', '{node.NodeTitlePosition}');");
        }


        private void toolComboShape_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_selected_node_id == "")
            {
                return;
            }

            string shape = toolComboShape.Text.ToLower();
            shape = shape.Replace(" ", "-");
            _browser.ExecScriptAsync($"setElementData('{_selected_node_id}', 'shape', '{shape}');");
            _browser.ExecScriptAsync($"setElementData('{_selected_node_id}', 'origin_shape', '{shape}');");
        }



        private void ShowHideGrid()
        {
            _settings.ShowGrid = this.btnGrid.Checked;
            _browser.ExecScriptAsync($"showHideGrid('{_settings.ShowGrid}');");
            _settings.Save();
        }

        private void ShowHideLabels()
        {
            string text = !btnLabels.Checked ?
                            "none" : btnSwitch.Checked ? "false" : "true";

            _settings.ShowLabel = btnLabels.Checked;
            _settings.SwitchTitle = btnSwitch.Checked;
            _settings.Save();
            _browser.ExecScriptAsync($"setTitleType('{text}');");
        }

        private void toolBtnBorderColor_Click(object sender, EventArgs e)
        {
            
            if (_selectedNodes.Count < 1)
            {
                return;
            }

            Node node = _selectedNodes.ElementAt(0).Value;
            ColorDialog cd = new ColorDialog();
            if (cd.ShowDialog(this) == DialogResult.OK)
            {
                node.Color = cd.Color;
                _browser.ExecScriptAsync($"setElementData('{node.ID}', 'border_color', 'rgb({cd.Color.R},{cd.Color.G},{cd.Color.B})');");
            }
        }

        private void toolBtnFillColor_Click(object sender, EventArgs e)
        {
            
            if (_selectedNodes.Count < 1)
            {
                return;
            }

            Node node = _selectedNodes.ElementAt(0).Value;
            ColorDialog cd = new ColorDialog();
            if (cd.ShowDialog(this) == DialogResult.OK)
            {
                node.Color = cd.Color;
                toolBtnFillColor.BackColor = cd.Color;
                _browser.ExecScriptAsync($"setElementData('{node.ID}', 'color', 'rgb({cd.Color.R},{cd.Color.G},{cd.Color.B})');");
            }
        }

        private void toolBtnBorderWidth_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            if (_selectedNodes.Count < 1)
            {
                return;
            }

            Node node = _selectedNodes.ElementAt(0).Value;
            _browser.ExecScriptAsync($"setElementData('{node.ID}', 'borderWidth', '{toolBtnBorderWidth.Text}');");
        }

        private void toolBtnCopy_Click(object sender, EventArgs e)
        {
            _ = toolBtnCopy_ClickAsync();
        }

        private async Task toolBtnCopy_ClickAsync()
        {
            Console.WriteLine("MainForm >  toolBtnCopy_ClickAsync");
            // get selected nodes
            await UpdateAllSelectedNodes();
            await UpdateAllSelectedEdges();
            toolBtnPast.Enabled = true;

            copyNodes = new Dictionary<string, Node>();
            copyNodes = _selectedNodes.ToDictionary(entry => entry.Key,
                                           entry => entry.Value.Clone());

        }

        private async Task toolBtnPaste_ClickAsync()
        {
            Console.WriteLine("MainForm >  toolBtnPaste_ClickAsync");
            if (copyNodes.Count() > 0)
            {
                for (int i = 0; i < copyNodes.Count(); i++)
                {
                    Node tmp = copyNodes.ElementAt(i).Value;
                    await _browser.EvaluateScriptAsync($"cloneNode({JsonConvert.SerializeObject(tmp.ID)})");
                }
            }
           
        }


        private void toolBtnPast_ClickAsync(object sender, EventArgs e)
        {
            _ = toolBtnPaste_ClickAsync();
            
        }

        private void NodeCategoryItem_Click(object sender, EventArgs e)
        {
            (sender as BarItem).Checked = !(sender as BarItem).Checked;
            updateNodeTypeListbox();
        }

        private void toolStripButton49_Click(object sender, EventArgs e)
        {
            animateGraph();
        }

        private void groupBarItem_Click(object sender, EventArgs e)
        {
            groupBarItem.Checked = !groupBarItem.Checked;
            updateNodeTypeListbox();
        }

        private void controlBarItem_Click(object sender, EventArgs e)
        {
            controlBarItem.Checked = !controlBarItem.Checked;
            updateNodeTypeListbox();
        }
        private void assetBarItem_Click(object sender, EventArgs e)
        {
            assetBarItem.Checked = !assetBarItem.Checked;
            updateNodeTypeListbox();
        }

        private void attackBarItem_Click(object sender, EventArgs e)
        {
            attackBarItem.Checked = !attackBarItem.Checked;
            updateNodeTypeListbox();
        }

        private void objectiveBarItem_Click(object sender, EventArgs e)
        {
            objectiveBarItem.Checked = !objectiveBarItem.Checked;
            updateNodeTypeListbox();
        }

        private void updateNodeTypeListbox()
        {
            //nodeRepositoryForm.nodeTypeListbox.Items.Clear();

            //for (int i = 0; i < parentBarItem1.Items.Count; i++)
            //{
            //    var tmp = parentBarItem1.Items[i];
            //    Type t = tmp.GetType();
            //    if (t.Name == "BarItem")
            //    {
            //        if (tmp.Checked)
            //        {
            //            nodeRepositoryForm.nodeTypeListbox.Items.Add(tmp.Text);
            //        }
            //    }
            //    else
            //    {
            //        var parentText = tmp.Text;
            //        ParentBarItem pbi = tmp as ParentBarItem;
            //        for (int j = 0; j < pbi.Items.Count; j++)
            //        {
            //            var subText = pbi.Items[j].Text;
            //            if (pbi.Items[j].Checked)
            //            {
            //                nodeRepositoryForm.nodeTypeListbox.Items.Add(parentText + "(" + subText + ")");
            //            }
            //        }
            //    }
            //}
            //nodeRepositoryForm.UpdateNodeListView(NodeListViewItems);
        }

        private HttpResponseMessage CallAzureFunction(string input)
        {
            HttpRequestMessage request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(AuthApp.AzureFunctionUrl)
            };
            request.Content = new StringContent(input, Encoding.UTF8, "application/json");
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpClient httpClientHandler = new HttpClient();
            var response = httpClientHandler.SendAsync(request).GetAwaiter().GetResult();
            return response;
        }

        private async void AddRelationshipToRepository(object sender)
        {
            string edgeID = ((sender as ToolStripMenuItem).Tag as Graph.NodePositions).ID;
            var json = await _browser.EvaluateScriptAsync($"getEdgeJson('{edgeID}');");
            var jsonRes = json.Result;
            var data = ((IDictionary<String, Object>)jsonRes);
            var edge_data = (IDictionary<String, Object>)data["data"];
            var source_id = edge_data["source"];
            var target_id = edge_data["target"];

            var source_json = await _browser.EvaluateScriptAsync($"getNodeJson('{source_id}');");
            var source_jsonRes = source_json.Result;
            var source_data = ((IDictionary<String, Object>)source_jsonRes);
            var source_node_data = (IDictionary<String, Object>)source_data["data"];
            Node source_node = Node.FromDictionary(source_node_data);

            var target_json = await _browser.EvaluateScriptAsync($"getNodeJson('{target_id}');");
            var target_jsonRes = target_json.Result;
            var target_data = ((IDictionary<String, Object>)target_jsonRes);
            var target_node_data = (IDictionary<String, Object>)target_data["data"];
            Node target_node = Node.FromDictionary(target_node_data);

            AddNodeListItem(source_node);
            AddNodeListItem(target_node);

            string desc = edge_data.ContainsKey("description") ? edge_data["description"].ToString() : "";
            var from_desc = System.Convert.FromBase64String(desc);
            string final_desc = System.Text.Encoding.UTF8.GetString(from_desc);

            JArray _linked_nodes = Utility.LoadLinkedNodes();
            JObject obj = new JObject();
            obj["source_node_id"] = source_id.ToString();
            obj["target_node_id"] = target_id.ToString();
            obj["edge_title"] = edge_data["title"].ToString();
            obj["edge_description"] = "0";// final_desc;
            obj["edge_relationship"] = edge_data["relationship"].ToString();
            obj["edge_strength"] = edge_data["relationshipStrength"].ToString();
            obj["edge_strength_value"] = "1";
            obj["edge_Strength_Min_Value"] = edge_data["edgeStrengthMinValue"].ToString();
            obj["edge_Strength_Distribution"] = edge_data["edgeStrengthDistribution"].ToString();

            _linked_nodes.Add(obj);
            Utility.SaveLinkedNodes(_linked_nodes);
        }

        private void addToRepositoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            addToRepository(sender);
        }

        private async void addToRepository(object sender)
        {
            string nodeID = ((sender as ToolStripMenuItem).Tag as Graph.NodePositions).ID;
            var node_json = await _browser.EvaluateScriptAsync($"getNodeJson('{nodeID}');");
            var node_jsonRes = node_json.Result;
            var node_data = ((IDictionary<String, Object>)node_jsonRes);
            var node_result = (IDictionary<String, Object>)node_data["data"];
            Node node_obj = Node.FromDictionary(node_result);

            AddNodeListItem(node_obj);
        }


        public void changeTenant()
        {
            //call view
            if (AuthAPI._enterprise_guid == "")
            {
                changeEnterprise();
            }
            else
            {
                JArray tenants_ids = AuthAPI.GetTenants(AuthAPI._enterprise_guid);
                if (tenants_ids == null) return;

                ArrayList arr = new ArrayList();
                for (var i = 0; i < tenants_ids.Count; i++)
                {
                    JObject item = JObject.Parse(tenants_ids[i].ToString());
                    string detail = AuthAPI.GetTenantDetail(item["tenantGUID"].ToString());
                    if (detail == null || detail == "") continue;
                    JObject detailObj = JObject.Parse(detail);

                    //string guid, string name, string line1, string line2, string pcode, string city, string state, string country
                    arr.Add(new TenantItem(
                        detailObj["tenantGUID"].ToString(),
                        detailObj["name"].ToString(),
                        detailObj["description"].ToString()
                    ));
                }

                SelectTenantModal selectTenantModal = new SelectTenantModal();
                selectTenantModal.SetTenantGridData(arr);
                if (selectTenantModal.ShowDialog(this) == DialogResult.OK)
                {
                    TenantItem ti = selectTenantModal._selected_item;
                    if (ti == null) return;
                    _settings.SelectedTenantItem = ti;
                    //lblTenantName.Text = ti.TenantName;
                    AuthAPI._tenant_guid = ti.TenantGUID;
                    SettingsAPI.GetSettingsGUID();

                    AuthAPI.PutUserLastEnterpriseAndTenant(AuthAPI._enterprise_guid, AuthAPI._tenant_guid);
                    btnAccountTenant.Enabled = true;                                        
                    btnSaveAsCloud.Enabled = true;
                    btnSaveCloud.Enabled = true;
                    btnOpenCloud.Enabled = true;
                    nodeRepositoryForm.LoadNodeRepositoryData();
                }
            }
        }

        private void changeNodeAttribute(ComboBox cmb, string txt, JArray data, string type = "")
        {
            
            if (data == null || cmb.SelectedIndex == -1 || cmb.SelectedIndex > (cmb.Items.Count - 1))
            {
                return;
            }

            string sel_text = cmb.SelectedItem.ToString();
            for (int i = 0; i < data.Count; i++)
            {
                string tmp = (string)data[i]["impact"];
                if (tmp == sel_text)
                {
                    txt = (string)data[i]["value"];
                    break;
                }
            }

            if (type != "")
            {
                try
                {
                    if (_selectedNodes.ElementAt(0).Value != null)
                    {
                        Node node = _selectedNodes.ElementAt(0).Value;
                        _browser.ExecScriptAsync($"setElementData('{node.ID}', '{type}', '{sel_text}');");

                        type = type + "Value";
                        _browser.ExecScriptAsync($"setElementData('{node.ID}', '{type}', '{txt}');");
                        GraphUtil.SetNodeData(node.ID, type, txt);

                        if (GraphCalcs.autoCalculate) GraphCalcs.RecalculateAll();
                    }
                }
                catch { }

            }

        }

        public void saveNodeAttribute(string NodeAttribute = "", string minTextDescription = "", string minValue = "0", string maxTextDescription = "", string maxValue = "0", JArray ditributionData = null)
        {
            Node node = null;
            Edge edge = null;
            Type tempType = null;
            string Id = "";
            object obj = null;
            string elementType = "";

            
            if (NodeAttribute != "")
            {
                try
                {
                    if (ditributionData[4].ToString() == "node")  // Distribution belongs to a node
                    {
                        if (_selectedNodes.ElementAt(0).Value != null)
                        {
                            node = _selectedNodes.ElementAt(0).Value;
                            tempType = node.GetType();
                            Id = node.ID;
                            obj = node;
                            elementType = "node";
                        }
                    }
                    else
                    {
                        if (_selectedEdges.ElementAt(0).Value != null) // Distribution belongs to an edge
                        {
                            edge = _selectedEdges.ElementAt(0).Value;
                            tempType = edge.GetType();
                            Id = edge.ID;
                            obj = edge;
                            elementType = "edge";
                        }
                    }
                    if (obj != null)
                    {

                        //Set maximum values
                        _browser.ExecScriptAsync($"setElementData('{Id}', '{NodeAttribute}', '{maxTextDescription}');");
                        _browser.ExecScriptAsync($"setElementData('{Id}', '{NodeAttribute + "Value"}', '{maxValue}');");

                        PropertyInfo propertyInfo = tempType.GetProperty(NodeAttribute + "Value");
                        if (elementType == "node")
                        {
                            GraphUtil.SetNodeData(Id, NodeAttribute + "Value", maxValue);
                            propertyInfo.SetValue(obj, double.Parse(maxValue));
                        }

                        else
                        {
                            GraphUtil.SetEdgeData(Id, NodeAttribute + "Value", maxValue);
                            propertyInfo.SetValue(obj, maxValue);
                        }



                        //Set minmum values
                        _browser.ExecScriptAsync($"setElementData('{Id}', '{NodeAttribute + "Min"}', '{minTextDescription}');");
                        _browser.ExecScriptAsync($"setElementData('{Id}', '{NodeAttribute + "MinValue"}', '{minValue}');");

                        propertyInfo = tempType.GetProperty(NodeAttribute + "MinValue");
                        if (elementType == "node")
                        {
                            GraphUtil.SetNodeData(Id, NodeAttribute + "MinValue", minValue);
                            propertyInfo.SetValue(obj, double.Parse(minValue));
                        }
                        else
                        {
                            GraphUtil.SetEdgeData(Id, NodeAttribute + "MinValue", minValue);
                            propertyInfo.SetValue(obj, minValue);
                        }




                        JArray jArray = JArray.FromObject(ditributionData);
                        _browser.ExecScriptAsync($"setElementData('{Id}', '{NodeAttribute + "Distribution"}', '{JsonConvert.SerializeObject(jArray)}');");

                        if (elementType == "node")
                            GraphUtil.SetNodeData(Id, NodeAttribute + "Distribution", jArray.ToString());
                        else
                            GraphUtil.SetEdgeData(Id, NodeAttribute + "Distribution", jArray.ToString());

                        propertyInfo = tempType.GetProperty(NodeAttribute + "Distribution");
                        propertyInfo.SetValue(obj, jArray.ToString());


                        if (GraphCalcs.autoCalculate) GraphCalcs.RecalculateAll();
                    }
                }
                catch
                {
                    throw;
                }
            }
        }

        private void toolStripUndo_Click(object sender, EventArgs e)
        {
            this.underAction();
        }

        private void toolStripRedo_Click(object sender, EventArgs e)
        {
            this.redoAction();
        }

        public async void handleAccountLogin()
        {
            await AuthAPI.AuthLogin();
            this.InvokeIfNeed(async () =>
            {
                if (AuthAPI._user_guid != "")
                {
                    btnAccountLogin.Image = new Bitmap("Resources/LogOutIcon.png");
                    btnAccountLogin.Text = "Logout";
                    btnAccountUser.Enabled = true;
                    JObject obj = AuthAPI.QuickUserSetup();
                    JObject user_data = obj["user_data"] as JObject;
                    string status = obj["status"].ToString();
                    btnAccountEnterprise.Enabled = true;

                    lblUserName.Text = AuthAPI._user_name;
                    lblUserEmail.Text = AuthAPI._email_address;
                    pbUserLoginStatus.Image = new Bitmap("Resources/UserLoggedIn.png");

                    if (status == "3")
                    {
                        AuthAPI._enterprise_guid = user_data["lastEnterpriseGUID"].ToString();
                        AuthAPI._tenant_guid = user_data["lastTenantGUID"].ToString();
                        btnAccountTenant.Enabled = true;
                        btnNodeManager.Enabled = true;
                        btnNodeRepository.Enabled = true;
                        nodeRepositoryForm.LoadNodeRepositoryData();
                    }
                    else if (status == "2")
                    {
                        changeTenant();
                    } else
                    {
                        changeEnterprise();
                    }
                }
                btnAccountLogin.Enabled = true;
                panelUserLogin.Enabled = true;
                pbUserLoginStatus.Enabled = true;
            });
        }

        private async void toolBtnCopyImage_Click(object sender, EventArgs e)
        {
            await ExecuteScriptAndSetImageToClipboardAsync();
        }

        public async Task ExecuteScriptAndSetImageToClipboardAsync()
        {
            try
            {
                // Ensure _browser is initialized and ready before using.
                if (_browser == null)
                {
                    return;
                }

                // Execute the script and wait for the result.
                // Note: Ensure that the script "cy.jpg();" is valid and returns a Base64 encoded image string.
                var png64 = await _browser.EvaluateScriptAsync("getCapturedImage();");
                return;
                // Check if the result is a string.
                
                if (png64 == null)
                {
                    return;
                }

                string img_str = png64.Result as string;

                // Check if the string is long enough to safely call Substring.
                if (img_str.Length <= 22)
                {
                    return;
                }

                // Remove the data URI scheme (assuming it starts with "data:image/png;base64,")
                string base64Image = img_str.Substring(22);

                // Load the image from the Base64 string.
                // Note: Ensure that LoadBase64 is defined properly and returns an Image.
                Image img = LoadBase64(base64Image);

                if (img == null) 
                {
                    return;
                }

                // Set the image to the clipboard.
                // Note: This should be running in a context where clipboard access is possible.
                Clipboard.SetImage(img);
            }
            catch (Exception ex)
            {
                return;
            }
        }



        private void toolBtnFontPlus_Click(object sender, EventArgs e)
        {
            
            if (_selectedNodes.Count < 1)
            {
                return;
            }

            Node node = _selectedNodes.ElementAt(0).Value;
            string title_size = (node.TitleSize + 1).ToString();
            node.TitleSize = Double.Parse(title_size);
            _browser.ExecScriptAsync($"setElementData('{node.ID}', 'titleSize', '{title_size}');");
            toolBtnFontSize.Text = title_size;
        }

        private void toolBtnFontMinus_Click(object sender, EventArgs e)
        {
            
            if (_selectedNodes.Count < 1)
            {
                return;
            }

            Node node = _selectedNodes.ElementAt(0).Value;
            string title_size = node.TitleSize < 8 ? "7" : (node.TitleSize - 1).ToString();
            node.TitleSize = Double.Parse(title_size);
            _browser.ExecScriptAsync($"setElementData('{node.ID}', 'titleSize', '{title_size}');");
            toolBtnFontSize.Text = title_size;
        }

        private void printDocument_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawImage(bitmap, 0, 0);
        }


        private void toolStripeDrawnEdgeWidth_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Verify that the pressed key isn't CTRL or any non-numeric digit
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void toolBtnBorderWidth_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Verify that the pressed key isn't CTRL or any non-numeric digit
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void toolStripDrawnEdgeColor_Click(object sender, EventArgs e)
        {
            if (_selectedEdges.Count == 0) return;
            ColorDialog colorDlg = new ColorDialog();
            if (colorDlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string edgeID = _selectedEdges.ElementAt(0).Value.ID;
                _browser.ExecScriptAsync($"setElementData('{edgeID}', 'color', 'rgb({colorDlg.Color.R},{colorDlg.Color.G},{colorDlg.Color.B})');");
            }
        }

        private void toolStripeDrawnEdgeWidth_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_selectedEdges.Count == 0) return;
            string edgeID = _selectedEdges.ElementAt(0).Value.ID;
            _browser.ExecScriptAsync($"setElementData('{edgeID}', 'weight', {toolStripeDrawnEdgeWidth.Text});");
        }

        private async void saveWordData(string file_path)
        {
            List<string> assetNodes = new List<string>();
            List<string> attackNodes = new List<string>();
            List<string> controlNodes = new List<string>();
            List<string> groupNodes = new List<string>();
            List<string> actorNodes = new List<string>();
            List<string> objectiveNodes = new List<string>();

            var allNodes = await _browser.EvaluateScriptAsync("getNodes();");
            if (allNodes.Success)
            {
                var tmpObj = JArray.Parse(allNodes.Result.ToString());
                foreach (var tmpNode in tmpObj)
                {
                    Node node = Node.FromJson(tmpNode.ToString());
                    switch (node.Type.Name.ToLower())
                    {
                        case "actor":
                            actorNodes.Add(node.Title);
                            break;
                        case "attack":
                            attackNodes.Add(node.Title);
                            break;
                        case "asset":
                            assetNodes.Add(node.Title);
                            break;
                        case "control":
                            controlNodes.Add(node.Title);
                            break;
                        case "objective":
                            objectiveNodes.Add(node.Title);
                            break;
                        case "group":
                            groupNodes.Add(node.Title);
                            break;
                    }
                }
            }

            WordDocument document = new WordDocument();
            //Add a new section to the document.
            WSection section = document.AddSection() as WSection;
            //Set Margin of the section.
            section.PageSetup.Margins.All = 72;
            //Set the page size of the section. 

            //Create Paragraph styles.
            WParagraphStyle normalStyle = document.AddParagraphStyle("Normal") as WParagraphStyle;
            normalStyle.CharacterFormat.FontName = "Calibri";
            normalStyle.CharacterFormat.FontSize = 11f;
            normalStyle.ParagraphFormat.BeforeSpacing = 0;
            normalStyle.ParagraphFormat.AfterSpacing = 8;
            normalStyle.ParagraphFormat.LineSpacing = 13.8f;

            WParagraphStyle headingStyle = document.AddParagraphStyle("Heading 1") as WParagraphStyle;
            headingStyle.ApplyBaseStyle("Normal");
            headingStyle.CharacterFormat.FontName = "Calibri Light";
            headingStyle.CharacterFormat.FontSize = 16f;
            headingStyle.ParagraphFormat.BeforeSpacing = 12;
            headingStyle.ParagraphFormat.AfterSpacing = 0;
            headingStyle.ParagraphFormat.Keep = true;
            headingStyle.ParagraphFormat.KeepFollow = true;
            headingStyle.ParagraphFormat.OutlineLevel = OutlineLevel.Level1;

            IWParagraph paragraph = section.AddParagraph();
            paragraph.ApplyStyle("Heading 1");
            paragraph.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;
            WTextRange textRange = paragraph.AppendText("Word Pairs for (Description Text & Value):") as WTextRange;
            textRange.CharacterFormat.FontSize = 18f;
            textRange.CharacterFormat.FontName = "Calibri";

            //Append the paragraph.
            paragraph = section.AddParagraph();
            paragraph.ParagraphFormat.FirstLineIndent = 36;
            paragraph.BreakCharacterFormat.FontSize = 12f;
            textRange = paragraph.AppendText("Graph Name ") as WTextRange;
            textRange.CharacterFormat.FontSize = 12f;

            //Append the paragraph.
            paragraph = section.AddParagraph();
            paragraph.ParagraphFormat.FirstLineIndent = 36;
            paragraph.BreakCharacterFormat.FontSize = 12f;
            textRange = paragraph.AppendText("Graph Description ") as WTextRange;
            textRange.CharacterFormat.FontSize = 12f;

            //Append the paragraph.
            paragraph = section.AddParagraph();
            paragraph.ParagraphFormat.FirstLineIndent = 36;
            paragraph.BreakCharacterFormat.FontSize = 12f;
            textRange = paragraph.AppendText("Major Version ") as WTextRange;
            textRange.CharacterFormat.FontSize = 12f;

            //Append the paragraph.
            paragraph = section.AddParagraph();
            paragraph.ParagraphFormat.FirstLineIndent = 36;
            paragraph.BreakCharacterFormat.FontSize = 12f;
            textRange = paragraph.AppendText("Minor Version ") as WTextRange;
            textRange.CharacterFormat.FontSize = 12f;

            //Append the paragraph.
            paragraph = section.AddParagraph();
            paragraph.ParagraphFormat.FirstLineIndent = 36;
            paragraph.BreakCharacterFormat.FontSize = 12f;
            textRange = paragraph.AppendText("Revision ") as WTextRange;
            textRange.CharacterFormat.FontSize = 12f;

            //Append the paragraph.
            paragraph = section.AddParagraph();
            paragraph.ParagraphFormat.FirstLineIndent = 36;
            paragraph.BreakCharacterFormat.FontSize = 12f;
            textRange = paragraph.AppendText("Updated ") as WTextRange;
            textRange.CharacterFormat.FontSize = 12f;

            //Append the paragraph.
            paragraph = section.AddParagraph();
            paragraph.ParagraphFormat.FirstLineIndent = 36;
            paragraph.BreakCharacterFormat.FontSize = 12f;
            textRange = paragraph.AppendText("Count of Actor Nodes " + actorNodes.Count) as WTextRange;
            textRange.CharacterFormat.FontSize = 12f;

            //Append the paragraph.
            paragraph = section.AddParagraph();
            paragraph.ParagraphFormat.FirstLineIndent = 36;
            paragraph.BreakCharacterFormat.FontSize = 12f;
            textRange = paragraph.AppendText("Count of Attack Nodes " + attackNodes.Count) as WTextRange;
            textRange.CharacterFormat.FontSize = 12f;

            //Append the paragraph.
            paragraph = section.AddParagraph();
            paragraph.ParagraphFormat.FirstLineIndent = 36;
            paragraph.BreakCharacterFormat.FontSize = 12f;
            textRange = paragraph.AppendText("Count of Asset " + assetNodes.Count) as WTextRange;
            textRange.CharacterFormat.FontSize = 12f;

            //Append the paragraph.
            paragraph = section.AddParagraph();
            paragraph.ParagraphFormat.FirstLineIndent = 36;
            paragraph.BreakCharacterFormat.FontSize = 12f;
            textRange = paragraph.AppendText("Count of Control Nodes " + controlNodes.Count) as WTextRange;
            textRange.CharacterFormat.FontSize = 12f;

            //Append the paragraph.
            paragraph = section.AddParagraph();
            paragraph.ParagraphFormat.FirstLineIndent = 36;
            paragraph.BreakCharacterFormat.FontSize = 12f;
            textRange = paragraph.AppendText("Count of Objective Nodes " + objectiveNodes.Count) as WTextRange;
            textRange.CharacterFormat.FontSize = 12f;

            //Append the paragraph.
            paragraph = section.AddParagraph();
            paragraph.ParagraphFormat.FirstLineIndent = 36;
            paragraph.BreakCharacterFormat.FontSize = 12f;
            textRange = paragraph.AppendText("Count of Group Nodes " + groupNodes.Count) as WTextRange;
            textRange.CharacterFormat.FontSize = 12f;

            paragraph = section.AddParagraph();
            paragraph.ApplyStyle("Heading 1");
            paragraph.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;
            textRange = paragraph.AppendText("Actor Nodes Info:") as WTextRange;
            textRange.CharacterFormat.FontSize = 16f;
            textRange.CharacterFormat.FontName = "Calibri";

            for (int i = 0; i < actorNodes.Count; i++)
            {
                paragraph = section.AddParagraph();
                paragraph.ParagraphFormat.FirstLineIndent = 36;
                paragraph.BreakCharacterFormat.FontSize = 12f;
                textRange = paragraph.AppendText(actorNodes[i]) as WTextRange;
                textRange.CharacterFormat.FontSize = 12f;
            }

            paragraph = section.AddParagraph();
            paragraph.ApplyStyle("Heading 1");
            paragraph.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;
            textRange = paragraph.AppendText("Attack Nodes Info:") as WTextRange;
            textRange.CharacterFormat.FontSize = 16f;
            textRange.CharacterFormat.FontName = "Calibri";

            for (int i = 0; i < actorNodes.Count; i++)
            {
                paragraph = section.AddParagraph();
                paragraph.ParagraphFormat.FirstLineIndent = 36;
                paragraph.BreakCharacterFormat.FontSize = 12f;
                textRange = paragraph.AppendText(actorNodes[i]) as WTextRange;
                textRange.CharacterFormat.FontSize = 12f;
            }

            paragraph = section.AddParagraph();
            paragraph.ApplyStyle("Heading 1");
            paragraph.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;
            textRange = paragraph.AppendText("Asset Nodes Info:") as WTextRange;
            textRange.CharacterFormat.FontSize = 16f;
            textRange.CharacterFormat.FontName = "Calibri";

            for (int i = 0; i < assetNodes.Count; i++)
            {
                paragraph = section.AddParagraph();
                paragraph.ParagraphFormat.FirstLineIndent = 36;
                paragraph.BreakCharacterFormat.FontSize = 12f;
                textRange = paragraph.AppendText(assetNodes[i]) as WTextRange;
                textRange.CharacterFormat.FontSize = 12f;
            }

            paragraph = section.AddParagraph();
            paragraph.ApplyStyle("Heading 1");
            paragraph.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;
            textRange = paragraph.AppendText("Control Nodes Info:") as WTextRange;
            textRange.CharacterFormat.FontSize = 16f;
            textRange.CharacterFormat.FontName = "Calibri";

            for (int i = 0; i < controlNodes.Count; i++)
            {
                paragraph = section.AddParagraph();
                paragraph.ParagraphFormat.FirstLineIndent = 36;
                paragraph.BreakCharacterFormat.FontSize = 12f;
                textRange = paragraph.AppendText(controlNodes[i]) as WTextRange;
                textRange.CharacterFormat.FontSize = 12f;
            }

            paragraph = section.AddParagraph();
            paragraph.ApplyStyle("Heading 1");
            paragraph.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;
            textRange = paragraph.AppendText("Objective Nodes Info:") as WTextRange;
            textRange.CharacterFormat.FontSize = 16f;
            textRange.CharacterFormat.FontName = "Calibri";

            for (int i = 0; i < objectiveNodes.Count; i++)
            {
                paragraph = section.AddParagraph();
                paragraph.ParagraphFormat.FirstLineIndent = 36;
                paragraph.BreakCharacterFormat.FontSize = 12f;
                textRange = paragraph.AppendText(objectiveNodes[i]) as WTextRange;
                textRange.CharacterFormat.FontSize = 12f;
            }

            paragraph = section.AddParagraph();
            paragraph.ApplyStyle("Heading 1");
            paragraph.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Left;
            textRange = paragraph.AppendText("Group Nodes Info:") as WTextRange;
            textRange.CharacterFormat.FontSize = 16f;
            textRange.CharacterFormat.FontName = "Calibri";

            for (int i = 0; i < groupNodes.Count; i++)
            {
                paragraph = section.AddParagraph();
                paragraph.ParagraphFormat.FirstLineIndent = 36;
                paragraph.BreakCharacterFormat.FontSize = 12f;
                textRange = paragraph.AppendText(groupNodes[i]) as WTextRange;
                textRange.CharacterFormat.FontSize = 12f;
            }

            //Append the paragraph.
            section.AddParagraph();
            //Save the Word document to stream.
            MemoryStream outputStream = new MemoryStream();
            //document.Save(outputStream, FormatType.Docx);
            //Save the stream as a Word document file in the local machine.
            document.Save(file_path, FormatType.Docx);
        }

        private void btnOpenWordTemplate_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFile = new OpenFileDialog()
            {
                Filter = "All files (*.*)|*.*|Word files (*.docx)|*.docx",
                FilterIndex = 2,
                Title = "Open file for word template"
            })
            {
                if (openFile.ShowDialog(this) == DialogResult.OK)
                {
                    //lblWordTemplateName.Text = openFile.FileName;
                }
            }
        }

        private async void SaveWordWithTemplate()
        {

            //if (lblWordTemplateName.Text == "" || lblWordTemplateName.Text == "_")
            //{
            //    NetGraphMessageBox.MessageBoxEx(this, "Word Template Empty", "Select Word Template", MessageBoxButtons.OK, MessageBoxIconEx.Information);
            //    return;
            //}
            //List<string> assetNodes = new List<string>();
            //List<string> attackNodes = new List<string>();
            //List<string> controlNodes = new List<string>();
            //List<string> groupNodes = new List<string>();
            //List<string> actorNodes = new List<string>();
            //List<string> objectiveNodes = new List<string>();

            //var allNodes = await _browser.EvaluateScriptAsync("getNodes();");
            //if (allNodes.Success)
            //{
            //    var tmpObj = JArray.Parse(allNodes.Result.ToString());
            //    foreach (var tmpNode in tmpObj)
            //    {
            //        Node node = Node.FromJson(tmpNode.ToString());
            //        switch (node.Type.Name.ToLower())
            //        {
            //            case "actor":
            //                actorNodes.Add(node.Title);
            //                break;
            //            case "attack":
            //                attackNodes.Add(node.Title);
            //                break;
            //            case "asset":
            //                assetNodes.Add(node.Title);
            //                break;
            //            case "control":
            //                controlNodes.Add(node.Title);
            //                break;
            //            case "objective":
            //                objectiveNodes.Add(node.Title);
            //                break;
            //            case "group":
            //                groupNodes.Add(node.Title);
            //                break;
            //        }
            //    }
            //}

            //using (SaveFileDialog saveFileDialog = new SaveFileDialog()
            //{
            //    Title = "Save as graph",
            //    Filter = "All files (*.*)|*.*|Graph Report (*.docx)|*.docx",
            //    FilterIndex = 2,
            //    FileName = Path.GetFileNameWithoutExtension(Path.GetFileName(_fileName)) + ".docx"
            //})
            //{
            //    if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
            //    {
            //        _fileName = saveFileDialog.FileName;

            //        WordDocument wordDocument = new WordDocument(lblWordTemplateName.Text);

            //        // Replace Contents
            //        wordDocument.Replace(new Regex("\\[ActorNodesCount\\]"), actorNodes.Count.ToString());
            //        wordDocument.Replace(new Regex("\\[AttackNodesCount\\]"), attackNodes.Count.ToString());
            //        wordDocument.Replace(new Regex("\\[AssetNodesCount\\]"), assetNodes.Count.ToString());
            //        wordDocument.Replace(new Regex("\\[ControlNodesCount\\]"), controlNodes.Count.ToString());
            //        wordDocument.Replace(new Regex("\\[ObjectiveNodesCount\\]"), objectiveNodes.Count.ToString());
            //        wordDocument.Replace(new Regex("\\[GroupNodesCount\\]"), groupNodes.Count.ToString());
            //        wordDocument.Replace(new Regex("\\[ActorNodes\\]"), Utility.ListToString(actorNodes));
            //        wordDocument.Replace(new Regex("\\[AttackNodes\\]"), Utility.ListToString(attackNodes));
            //        wordDocument.Replace(new Regex("\\[AssetNodes\\]"), Utility.ListToString(assetNodes));
            //        wordDocument.Replace(new Regex("\\[ControlNodes\\]"), Utility.ListToString(controlNodes));
            //        wordDocument.Replace(new Regex("\\[ObjectiveNodes\\]"), Utility.ListToString(objectiveNodes));
            //        wordDocument.Replace(new Regex("\\[GroupNodes\\]"), Utility.ListToString(groupNodes));

            //        wordDocument.Save(_fileName, FormatType.Docx);
            //    }
            //}
        }

        private void btnSaveWord_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog()
            {
                Title = "Save as graph",
                Filter = "All files (*.*)|*.*|Graph Report (*.docx)|*.docx",
                FilterIndex = 2,
                FileName = Path.GetFileNameWithoutExtension(Path.GetFileName(_fileName)) + ".docx"
            })
            {
                if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
                {
                    _fileName = saveFileDialog.FileName;
                    saveWordData(_fileName);
                }
            }
        }

        private void btnSaveWordTemplate_Click(object sender, EventArgs e)
        {
            SaveWordWithTemplate();
        }

        public string selectedNodeID()
        {
            
            string nodeID = "";
            if (_selectedNodes.Count > 0)
            {
                Node node = _selectedNodes.ElementAt(0).Value;
                nodeID = node.ID;
            }
            return nodeID;
        }

        public string selectedEdgeID()
        {
            string edgeID = "";
            if (_selectedEdges.Count > 0)
            {
                Edge edge = _selectedEdges.ElementAt(0).Value;
                edgeID = edge.ID;
            }
            return edgeID;
        }

        private void dockingManager1_DockVisibilityChanged(object sender, DockVisibilityChangedEventArgs arg)
        {
            switch (arg.Control.Name)
            {
                case "panelNodeRepository":
                    btnNodeRepository.Checked = arg.Control.Visible;
                    break;
                case "panelProperties":
                    btnDetail.Checked = arg.Control.Visible;
                    break;
                case "panelRisk":
                    btnValues.Checked = arg.Control.Visible;
                    break;
                case "panelContainer":
                    btnDistributions.Checked = arg.Control.Visible;
                    break;
                case "panelCompliance":
                    btnCompliance.Checked = arg.Control.Visible;
                    break;
                case "panelRiskAssetRiskList":
                    btnRiskList.Checked = arg.Control.Visible;
                    break;
                case "panelRiskContainer":
                    btnValues.Checked = arg.Control.Visible;
                    break;
                case "panelHeatMap":
                    btnRiskHeatMap.Checked = arg.Control.Visible;
                    break;
                case "panelRiskGrid":
                    btnNodeData.Checked = arg.Control.Visible;
                    break;
                case "panelComplianceContainer":
                    btnCompliance.Checked= arg.Control.Visible;
                    break;
                case "panelFindContainer":
                    toolBtnFind.Checked = arg.Control.Visible;
                    break;
            }
        }


        private void toolStripButton8_Click_1(object sender, EventArgs e)
        {
            addNodeQuick("Vulnerability", "vulnerability");
        }


        private void btnSaveLocal_Click(object sender, EventArgs e)
        {
            saveGraphFile();
        }

        private bool LoadDataFromCloud(string graph_data)
        {
            graph_data = Utility.Base64Decode(graph_data);

            JObject obj = DeserializeDataFromFile(graph_data);
            if (obj["status"].ToString().ToLower() == "false") {
                System.Windows.Forms.MessageBox.Show("File Format is not valid");
                return false;
            }
            DrawGraphFile(obj["data"].ToString());
            _hasUnsavedChanges = false;
            _newFile = false;
            return true;
        }

        private void LoadGraphFileData(string file_type, string local_file_data, string cloud_file_data, string graph_guid, string child_guid)
        {
            if (file_type == "local")
            {
                bool flag = LoadDataFromFile(local_file_data);
                if (flag == false) return;
                AddFileToRecentFiles(local_file_data);
                SettingsAPI._graph_guid = AuthAPI._tenant_guid;
                SettingsAPI._settings_guid = "";
            }
            else
            {
                if (LoadDataFromCloud(cloud_file_data) == false) return;
                SettingsAPI._graph_guid = graph_guid;
                SettingsAPI._settings_guid = "";
            }
            //Deselect all on graph
            _browser.ExecuteScriptAsync("deselectAll();");
            this.ribbonControlAdv1.SelectedTab = ribbonTabHome;
            InitializeGraph();
        }

        private void btnOpenLocal_Click(object sender, EventArgs e)
        {
            FileOpenForm fom = new FileOpenForm();
            if (fom.ShowDialogWithTab("local", _settings.ResentFiles) == DialogResult.Yes)
            {
                LoadGraphFileData(fom.file_type, fom.local_file_name, fom.cloud_file_data, fom.graph_guid, fom.child_guid);
            }
        }

        private void btnSaveAsLocal_Click(object sender, EventArgs e)
        {
            saveAsGraphFile();
        }

        private void btnOpenCloud_Click(object sender, EventArgs e)
        {
            FileOpenForm fom = new FileOpenForm();
            if (fom.ShowDialogWithTab("cloud", _settings.ResentFiles) == DialogResult.Yes)
            {
                LoadGraphFileData(fom.file_type, fom.local_file_name, fom.cloud_file_data, fom.graph_guid, fom.child_guid);
            }
        }

        private void btnSaveCloud_Click(object sender, EventArgs e)
        {
            Graph.Utility.SaveAuditLog("SaveCloud", "Button Click", "", "", $"");
            SaveGraphCloud(false);
        }

        private void btnSaveAsCloud_Click(object sender, EventArgs e)
        {
            Graph.Utility.SaveAuditLog("SaveAsCloud", "Button Click", "", "", $"");
            SaveGraphCloud();
        }

        private void SaveGraphCloud(bool flag = true)
        {
            Graph.Utility.SaveAuditLog("SaveGraphCloud", "+++FUNCTION ENTERED+++", "", "", $"");
            _graph_cloud.SaveGraph(_browser, _hasUnsavedChanges, flag);
            _settings.LastFile = _graph_cloud._graph_title;
            _newFile = false;
            _hasUnsavedChanges = false;
            this.ribbonControlAdv1.SelectedTab = ribbonTabHome;
        }

        private void btnAccountTenant_Click(object sender, EventArgs e)
        {
            //call view
            Graph.Utility.SaveAuditLog("AccountTenant", "Button Click", "", "", $"");
            if (AuthAPI._enterprise_guid == "")
            {
                //ShowBusySpinner();
                Graph.Utility.SaveAuditLog("AccountTenant", "Error", "", "", $"No Enterprise selected");
                changeEnterprise();
                //HideBusySpinner();
            }
            else
            {
                if (AuthAPI._tenant_items.Count == 0 || AuthAPI._enterprise_guid != AuthAPI._enterprise_guid_old)
                {
                    ShowBusySpinner();
                    AuthAPI._enterprise_guid_old = AuthAPI._enterprise_guid;
                    JArray tenants_ids = AuthAPI.GetTenants(AuthAPI._enterprise_guid);
                    if (tenants_ids == null) return;

                    //ArrayList arr = new ArrayList();
                    AuthAPI._tenant_items.Clear();
                    for (var i = 0; i < tenants_ids.Count; i++)
                    {
                        JObject item = JObject.Parse(tenants_ids[i].ToString());
                        string detail = AuthAPI.GetTenantDetail(item["tenantGUID"].ToString());
                        JObject detailObj = JObject.Parse(detail);

                        //string guid, string name, string line1, string line2, string pcode, string city, string state, string country
                        AuthAPI._tenant_items.Add(new TenantItem(
                            detailObj["tenantGUID"].ToString(),
                            detailObj["name"].ToString(),
                            detailObj["description"].ToString()
                        ));
                    }
                    HideBusySpinner();
                }
                ShowBusySpinner();
                Graph.Utility.SaveAuditLog("AccountTenant", "Show Modal", "", "", $"");
                SelectTenantModal selectTenantModal = new SelectTenantModal();
                selectTenantModal.SetTenantGridData(AuthAPI._tenant_items);
                if (selectTenantModal.ShowDialog(this) == DialogResult.OK)
                {
                    TenantItem ti = selectTenantModal._selected_item;
                    _settings.SelectedTenantItem = ti;
                    _settings.Save();
                    Graph.Utility.SaveAuditLog("AccountTenant", "Tenant Selected", ti.TenantGUID, ti.TenantName, $"");
                    AuthAPI._tenant_guid = ti.TenantGUID;
                    AuthAPI.PutUserLastEnterpriseAndTenant(AuthAPI._enterprise_guid, AuthAPI._tenant_guid);
                }
                HideBusySpinner();
            }
        }

        public void changeEnterprise()
        {
            Graph.Utility.SaveAuditLog("btnAccountEnterprise", "Button Click", "", "", $"");

            if (string.IsNullOrEmpty(AuthAPI._user_guid))
            {
                Graph.Utility.SaveAuditLog("btnAccountEnterprise", "Error", "", "", $"Not Logged In");
                System.Windows.Forms.MessageBox.Show("You are not logged in. Please login first.");
            }
            else
            {
                //API Select User Enterprise
                if (AuthAPI._enterprise_items.Count == 0)
                {
                    JArray enterprise_ids = AuthAPI.GetEnterprises();
                    if (enterprise_ids.Count == 0)
                    {
                        Graph.Utility.SaveAuditLog("btnAccountEnterprise", "No Existing Enterprise", "", "", $"Please create an Enterprise.");
                        System.Windows.Forms.MessageBox.Show("You have not created any Enterprises. Please create an Enterprise first.");
                        ShowBusySpinner();
                        NewEnterpriseModal newEnterprise = new NewEnterpriseModal();
                        JObject id = new JObject();
                        if (newEnterprise.ShowDialog(this) == DialogResult.OK)
                        {

                            id = JObject.Parse(AuthAPI.CreateEnterprise(newEnterprise.EnterpriseName, newEnterprise.EnterpriseAddress1,
                                        newEnterprise.EnterpriseAddress2, newEnterprise.EnterprisePostcode,
                                        newEnterprise.EnterpriseCity, newEnterprise.EnterpriseCountry,
                                        newEnterprise.EnterpriseState));
                            HideBusySpinner();
                            return;
                        }
                    }
                    //ArrayList arr = new ArrayList();
                    AuthAPI._enterprise_items.Clear();
                    for (var i = 0; i < enterprise_ids.Count; i++)
                    {
                        JObject item = JObject.Parse(enterprise_ids[i].ToString());
                        string detail = AuthAPI.GetEnterpriseDetail(item["enterpriseGUID"].ToString());
                        if (detail == "") continue;
                        JObject detailObj = JObject.Parse(detail);

                        //string guid, string name, string line1, string line2, string pcode, string city, string state, string country

                        ShowBusySpinner();
                        AuthAPI._enterprise_items.Add(new EnterpriseItem(
                            detailObj["enterpriseGUID"].ToString(),
                            detailObj["name"].ToString(),
                            detailObj["addressLine1"].ToString(),
                            detailObj["addressLine2"].ToString(),
                            detailObj["postcode"].ToString(),
                            detailObj["city"].ToString(),
                            detailObj["state"].ToString(),
                            detailObj["country"].ToString()
                        ));
                        HideBusySpinner();
                    }
                }

                SelectEnterpriseModal seModal = new SelectEnterpriseModal();
                Graph.Utility.SaveAuditLog("btnAccountEnterprise", "Show Dialog", "", "", $"");
                ShowBusySpinner();
                seModal.SetGridEnterpriseData(AuthAPI._enterprise_items);

                if (seModal.ShowDialog() == DialogResult.OK)
                {
                    EnterpriseItem ei = seModal._selected_item;
                    _settings.SelectedEnterpriseItem = ei;
                    _settings.SelectedTenantItem = null;
                    _settings.Save();
                    Graph.Utility.SaveAuditLog("btnAccountEnterprise", "Enterprise Selected", "", ei.EnterpriseGUID, ei.EnterpriseName);
                    AuthAPI._tenant_guid = "";
                    AuthAPI._enterprise_guid = ei.EnterpriseGUID;
                    AuthAPI._enterprise_guid_old = AuthAPI._enterprise_guid;
                    btnAccountTenant.Enabled = true;

                    ShowBusySpinner();
                    SettingsAPI.GetSettingsGUID();
                    changeTenant();
                    HideBusySpinner();
                }
                HideBusySpinner();
            }
        }

        private async Task btnAccountEnterprise_ClickAsync(object sender, EventArgs e)
        {
            ShowBusySpinner();
            await Task.Run(() => changeEnterprise());
            HideBusySpinner();
        }

        private async void btnAccountLogin_Click(object sender, EventArgs e)
        {
            Graph.Utility.SaveAuditLog("handleAccountLogin", "Button Click", "", "", $"");
            if (AuthAPI._user_token == "" || AuthAPI._user_token == null)
            {
                ShowBusySpinner();
                btnAccountLogin.Enabled = false;
                panelUserLogin.Enabled = false;
                pbUserLoginStatus.Enabled = false;

                await Task.Run(() => handleAccountLogin());
                HideBusySpinner();
            }
            else
            {
                if (sender.GetType().Name.ToLower() == "panel")
                {
                    JObject accountDetail = AuthAPI.GetUser();
                    UserDetailModal userDetailForm = new UserDetailModal();
                    userDetailForm.SetUserData(accountDetail);
                    userDetailForm.ribbonForm = this;
                    userDetailForm.ShowDialog();
                }
                else
                {
                    btnAccountEnterprise.Enabled = false;
                    btnAccountTenant.Enabled = false;
                    btnAccountLogin.Image = new Bitmap("Resources/LoginIcon.png");
                    btnAccountLogin.Text = "Login";
                    btnAccountUser.Enabled = false;
                    btnNodeManager.Enabled = false;

                    lblUserName.Text = "";
                    lblUserEmail.Text = "";
                    pbUserLoginStatus.Image = new Bitmap("Resources/NoUser.png");

                    AuthAPI._user_guid = "";
                    AuthAPI._user_name = "";
                    AuthAPI.AuthLogout();

                    btnSaveCloud.Enabled = false;
                    btnOpenCloud.Enabled = false;
                    btnSaveAsCloud.Enabled = false;

                    _cacheObj.ClearCache();
                    nodeRepositoryForm.ClearPanels();
                }
            }
        }

        private void btnSettingsForm_Click(object sender, EventArgs e)
        {
            settingFormShow();
        }


        private void btnNewGraph_Click(object sender, EventArgs e)
        {
            newGraphAction();
        }


        private void toolBtnOpenAuditlog_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("notepad.exe", @"Graph\audit.log");
        }

        private void toolstripeButtonAuditLog_Click(object sender, EventArgs e)
        {
            toolBtnOpenAuditlog.Checked = !toolBtnOpenAuditlog.Checked;
            Utility.is_log = toolBtnOpenAuditlog.Checked;
        }

        public void SetEdgeDraw(bool state)
        {
            Console.WriteLine("MainForm > SetEdgeDraw");
            try
            {
                this.InvokeIfNeed(async () =>
                {
                    btnDrawEdges.Checked = state;
                    if (state)
                    {
                        await _browser.EvaluateScriptAsync($"enableDrawMode();");
                    }
                    else
                    {
                        await _browser.EvaluateScriptAsync($"disableDrawMode();");
                    }
                });
            }
            catch (Exception ex) 
            { 
                Console.WriteLine($"EXEPTION: MainForm > SetEdgeDraw : {ex.Message}"); 
            }
        }


        private async void toolStripButton6_Click_1(object sender, EventArgs e)
        {
            await _browser.EvaluateScriptAsync($"update_edge_data('{this.default_edge_relationship_strength_width}', '{this.default_edge_relationship_strength_color}')");

            if (_set_default_edge_relationship == false)
            {
                _set_default_edge_relationship = true;

                string der = this._default_edge_relationship.ToString();
                await _browser.EvaluateScriptAsync($"setDefaultEdgeRelationship({this._default_edge_relationship});");
            }

            //draw_strip_btn_checked = btnDrawEdges.Checked;

            SetEdgeDraw(btnDrawEdges.Checked);
            UpdateTabs();
        }

        private void toolStripButton6_Click_2(object sender, EventArgs e)
        {
           
        }

        private void btnNodePath_Click(object sender, EventArgs e)
        {
            btnNodePath.Checked = false;
            btnNodePath.Enabled = false;
            //GraphCalcs.CalculateIndividualRiskPaths();
            _browser.ExecScriptAsync($"drawHighlight('{false}','');");
        }

        private void toolStripButton5_Click_1(object sender, EventArgs e)
        {
            animateGraph();
        }

        private async void toolStripButton18_Click_2(object sender, EventArgs e)
        {
            btnDrawEdges.Checked = false;
            await _browser.EvaluateScriptAsync($"disableDrawMode();");
            UpdateTabs();
        }

        private async void toolStripButton19_Click_2(object sender, EventArgs e)
        {
            GraphCalcs.graphCalculated = false;
            var responce = await GraphSchemaCheck(false);
            if (responce.Result == true)
            {
                GraphUtil.ClearDistributionData();
                await GraphCalcs.RecalculateAll();
            }
            CompletedGraphRecalc();

            UpdateHeatmap(false);
        }
        private void toolBtnFind_CheckStateChanged(object sender, EventArgs e)
        {
            ShowHideFindPanel();
        }

        private void ShowHideFindPanel()
        {
            dockingManager1.LockHostFormUpdate();
            if (!dockingManager1.GetEnableDocking(searchDlg.panelFindContainer))
            {
                dockingManager1.SetEnableDocking(searchDlg.panelFindContainer, true);
                dockingManager1.DockControl(searchDlg.panelFindContainer, this, DockingStyle.Right, 400);
                dockingManager1.SetDockVisibility(searchDlg.panelFindContainer, this.toolBtnFind.Checked);
                dockingManager1.SetDockLabel(searchDlg.panelFindContainer, "Find Graph");

            }
            this.dockingManager1.SetDockVisibility(searchDlg.panelFindContainer, toolBtnFind.Checked);
            dockingManager1.UnlockHostFormUpdate();
        }
        private void toolStripButton6_Click_4(object sender, EventArgs e)
        {
            /*this.InvokeIfNeed(() =>
            {
                SearchDialogModal sd = new SearchDialogModal();
                Point p = this.Location;
                sd.ShowDialogWithBrowser(this, _browser, p);
            });*/
        }

        private void cbNodeValues_CheckStateChanged(object sender, EventArgs e)
        {

           
        }

        private void cbRiskDetails_CheckStateChanged(object sender, EventArgs e)
        {
            
        }

        private void cbNodeInformation_CheckStateChanged(object sender, EventArgs e)
        {

            
        }

        private void cbComplianceDetails_CheckStateChanged(object sender, EventArgs e)
        {
            
        }

        private void cbGridShowHide_CheckStateChanged(object sender, EventArgs e)
        {
            
        }

        private void cbLabelShowHide_CheckStateChanged(object sender, EventArgs e)
        {
            
        }

        private void cbSwitchTitleRef_CheckStateChanged(object sender, EventArgs e)
        {
            
        }


        private void ShowRiskPanels(string nodeType, string node_ID)
        {
            var orderedRiskPaths = GraphCalcs._IndividualRiskPaths.OrderByDescending(x => x.ImpactScore).ToList();  //Order by Impact Score
            this.riskPanelsForm.panelRiskAssetRiskList.SuspendLayout();
            this.riskPanelsForm.panelRiskAssetRiskList.Controls.Clear();
            foreach (var item in orderedRiskPaths)
            {
                if ((nodeType.ToLower() == "actor" && item.ActorGUID == node_ID) ||
                   (nodeType.ToLower() == "attack" && item.AttackGUID == node_ID) ||
                   (nodeType.ToLower() == "vulnerability" && item.VulnerabilityGUID == node_ID) ||
                   (nodeType.ToLower() == "asset" && item.AssetGUID == node_ID) ||
                   (nodeType.ToLower() == "all" && node_ID == "all"))
                {
                    if (item.ImpactScore > 0) // Prevent adding 0 value risks
                    {
                        this.riskPanelsForm.AddToRiskList(
                            item.ImpactScore,
                             GraphUtil.GetRiskStatusFromValue(item.ImpactScore),
                             item.RiskStatement,
                             item.FullNodeEdgePath,
                             GraphUtil.GetRiskColorFromValue(item.ImpactScore)
                             );
                    }
                }
            }
            this.riskPanelsForm.panelRiskAssetRiskList.ResumeLayout();
        }


        private void cbRiskList_CheckStateChanged(object sender, EventArgs e)
        {
            
        }

        private void txtActorAccessValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (GraphCalcs.autoCalculate) _ = GraphCalcs.RecalculateAll();
        }

        private void cbAutoCalc_Click(object sender, EventArgs e)
        {

        }

        private void cbAutoCalc_CheckStateChanged(object sender, EventArgs e)
        {
            GraphCalcs.autoCalculate = cbAutoCalc.Checked;
            _settings.AutoCalc = this.btnRiskList.Checked;
            _settings.Save();
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {

        }

        private void toolStripButton23_Click(object sender, EventArgs e)
        {

        }

        private void cbNodePanel_CheckStateChanged(object sender, EventArgs e)
        {
            this.dockingManager1.LockHostFormUpdate();

            if (!dockingManager1.GetEnableDocking(nodesForm.panelNodesListStatic))
            {
                nodesForm.panelNodesListStatic.Dock = DockStyle.Fill;
                dockingManager1.SetEnableDocking(nodesForm.panelNodesListStatic, true);
                dockingManager1.DockControl(nodesForm.panelNodesListStatic, this.dockingClientPanel1, DockingStyle.Tabbed, 250);
                dockingManager1.SetDockLabel(nodesForm.panelNodesListStatic, "Nodes");
            }

            if (this.cbNodePanel.Checked)
            {
                this.dockingManager1.SetDockVisibility(nodesForm.panelNodesListStatic, true);
            }
            else
            {
                this.dockingManager1.SetDockVisibility(nodesForm.panelNodesListStatic, false);
            }
            this.dockingManager1.UnlockHostFormUpdate();

            _settings.ShowNodesPanel = this.cbNodePanel.Checked;
            _settings.Save();
        }

        private void ShowCalculationLog_toolStripButton_Click(object sender, EventArgs e)
        {
            if (calcLogForm == null || calcLogForm.IsDisposed)
                calcLogForm = new CalcLogForm();
            GraphCalcs.useCalcLog = true;

            calcLogForm.Show();
        }

        public async void SelectNodeandShowDetail(string nodeID)
        {
            Console.WriteLine("MainForm > SelectNodeandShowDetail");
            var json1 = await _browser.EvaluateScriptAsync($"getNodeJson('{nodeID}');");
            var jsonRes1 = json1.Result;
            var data1 = ((IDictionary<String, Object>)jsonRes1);
            var node_data1 = (IDictionary<String, Object>)data1["data"];
            _selected_node_id = node_data1["id"].ToString();
            _selectedEdges.Clear();
            _selectedNodes.Clear();
            _selectedNodes.TryAdd(nodeID, Node.FromJson(JsonConvert.SerializeObject(node_data1)));
            string nodeType = (GraphUtil.GetNodeType(nodeID));
            if (nodeType == "objective" ||
                nodeType == "ocontrol" ||
                nodeType == "group" ||
                nodeType == "asset" ||
                nodeType == "attack" ||
                nodeType == "actor" ||
                nodeType == "vulnerability")
            {
                btnDistributions.Checked = true;
                UpdateTabs(true);
                nodeDistributionsForm.UpdateNodeAssessmentInfo(nodeID);
            }

            if (nodeType == "control")
            {
                btnDistributions.Checked = true;
                UpdateTabs(true);
                nodeDistributionsForm.UpdateNodeAssessmentInfo(nodeID);
            }

        }

        public async void SelectNodeonGraph(string nodeID)
        {
            await _browser.EvaluateScriptAsync($"selectNodeAnimation('{nodeID}');");
        }

        private void btnAddEvidenceNode_Click(object sender, EventArgs e)
        {

        }

        private void backImg_Click(object sender, EventArgs e)
        {

        }

        private void btnBackgroundImage_Click(object sender, EventArgs e)
        {
            SetBackgroundImage();
        }

        private void SetBackgroundImage(bool flag = false)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog()
            {
                Filter = "Jpg file (*.jpg)|*.jpg|Png File (*.png)|*.png|SVG file (*.svg)|*.svg",
                FilterIndex = 2,
                Title = "Open Background Image File"
            })
            {
                try
                {
                    if (openFileDialog.ShowDialog(this) == DialogResult.OK)
                    {
                        string local_file_name = openFileDialog.FileName;
                        string extension = Utility.GetFileExtension(local_file_name).ToLower();

                        //_browser.ExecScriptAsync($"set_fullscreen_size();");
                        int w = panelMainBrowser.Width;
                        int h = panelMainBrowser.Height;

                        string img_data = Utility.GetBase64StringForImage(local_file_name);
                        if (extension == "svg")
                        {
                            _browser.ExecScriptAsync($"setBackgroundSVG('{img_data}', '{flag.ToString()}', {w}, {h} );");
                        }
                        else
                        {

                            if (extension == "jpg")
                            {
                                img_data = "data:image/jpeg;base64," + img_data;
                            }
                            else if (extension == "png")
                            {
                                img_data = "data:image/png;base64," + img_data;
                            }
                            _browser.ExecScriptAsync($"setBackgroundImage('{img_data}', '{flag.ToString()}', {w}, {h} );");
                        }

                        _hasUnsavedChanges = true;
                    }
                }
                catch (Exception ex)
                {
                    NetGraphMessageBox.MessageBoxEx(this, $"Unable to open file. Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIconEx.Error);
                }
            }
        }

        public void ShowHideNodes()
        {
            JObject obj = new JObject();
            obj["control"] = ckShowControl.Checked;
            obj["asset"] = ckShowAsset.Checked;
            obj["actor"] = ckShowActor.Checked;
            obj["attack"] = ckShowAttack.Checked;
            obj["vulnerability"] = ckShowVulnerability.Checked;
            obj["group"] = ckShowGroup.Checked;
            obj["evidence"] = ckShowEvidence.Checked;
            obj["objective"] = ckShowObjective.Checked;

            _browser.ExecScriptAsync($"showNodes('{JsonConvert.SerializeObject(obj)}');");
        }

        private void btnClearBackgroundImage_Click(object sender, EventArgs e)
        {
            _hasUnsavedChanges = true;
            _browser.ExecScriptAsync($"clearImageBackground();");
        }


        private void toolBtnEnableBackgroundEdit_CheckedChanged(object sender, EventArgs e)
        {
            if (toolBtnEnableBackgroundEdit.Checked)
            {
                _browser.ExecScriptAsync($"enalbeImageBackground();");
                btnBackgroundImage.Enabled = true;
                btnClearBackgroundImage.Enabled = true;
                toolBtnFullSize.Enabled = true;
                toolBtnBackgroundColor.Enabled = true;

                ribbonTabHome.Visible = false;
                ribbonTabNode.Visible = false;
                ribbonTabLayout.Visible = false;
                ribbonTabView.Visible = false;
            }
            else
            {
                _browser.ExecScriptAsync($"disableImageBackground();");
                btnBackgroundImage.Enabled = false;
                btnClearBackgroundImage.Enabled = false;
                toolBtnFullSize.Enabled = false;
                toolBtnBackgroundColor.Enabled = false;

                ribbonTabHome.Visible = true;
                ribbonTabNode.Visible = true;
                ribbonTabLayout.Visible = true;
                ribbonTabView.Visible = true;
            }
        }

        private void toolBtnSetNodeImage_ClickAsync(object sender, EventArgs e)
        {
            _ = toolBtnSetNodeImageAsync(sender, e);
        }
               
        private async Task toolBtnSetNodeImageAsync(object sender, EventArgs e)
        {
            if (countOfselectedNodesOnGraph != _selectedNodes.Count())
            {
                await UpdateAllSelectedNodes();
            }

            if (_selectedNodes.Count() == 0)
                return;

            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp, *.ico, *.png, *.svg)|*.jpg; *.jpeg; *.gif; *.bmp; *.ico; *.png; *.svg";
            if (open.ShowDialog() == DialogResult.OK)
            {
                string extension = Utility.GetFileExtension(open.FileName).ToLower();
                string flName = open.FileName;
                string flData = Utility.GetBase64StringForImage(open.FileName);

                var textBytes = System.Text.Encoding.UTF8.GetBytes(flName);
                string imgPath = System.Convert.ToBase64String(textBytes);

                for (int i = 0; i < _selectedNodes.Count(); i++)
                {
                    _browser.ExecScriptAsync($"setElementData('{_selectedNodes.ElementAt(i).Key}', 'imagePath', '{imgPath}');");
                    if (extension == "svg")
                    {
                        _browser.ExecScriptAsync($"setElementSvgData('{_selectedNodes.ElementAt(i).Key}', 'data:image/svg+xml;base64,{flData}');");
                    }
                    else
                    {
                        _browser.ExecScriptAsync($"setElementData('{_selectedNodes.ElementAt(i).Key}', 'image', 'data:image/png;base64,{flData}');");
                    }
                }
            }
        }

        private void toolBtnClearNodeImage_Click(object sender, EventArgs e)
        {
            _ = toolBtnClearNodeImageAsync(sender, e);
        }

        private async Task toolBtnClearNodeImageAsync(object sender, EventArgs e)
        {
            
            if (countOfselectedNodesOnGraph != _selectedNodes.Count())
            {
                await UpdateAllSelectedNodes();
            }

            if (_selectedNodes.Count() == 0)
                return;

            for (int i = 0; i < _selectedNodes.Count(); i++)
            {

                _browser.ExecScriptAsync($"cy.startBatch();");
                _browser.ExecScriptAsync($"setElementData('{_selectedNodes.ElementAt(i).Key}', 'imagePath', '');");
                _browser.ExecScriptAsync($"setElementData('{_selectedNodes.ElementAt(i).Key}', 'image', '');");
                _browser.ExecScriptAsync($"setElementData('{_selectedNodes.ElementAt(i).Key}', 'background_opacity', '1');");
                _browser.ExecScriptAsync($"setElementData('{_selectedNodes.ElementAt(i).Key}', 'border_opacity', '1');");
                _browser.ExecScriptAsync($"setElementData('{_selectedNodes.ElementAt(i).Key}', 'shape', '{toolComboShape.Text.ToLower()}');");
                _browser.ExecScriptAsync($"setElementData('{_selectedNodes.ElementAt(i).Key}', 'color', 'rgb(0,0,0)');");
                _browser.ExecScriptAsync($"setElementData('{_selectedNodes.ElementAt(i).Key}', 'size', '{_settings.DefaultNodeSize}');");
                _browser.ExecScriptAsync($"setElementData('{_selectedNodes.ElementAt(i).Key}', 'height', '{_settings.DefaultNodeSize}');");
                _browser.ExecScriptAsync($"setElementData('{_selectedNodes.ElementAt(i).Key}', 'width', '{_settings.DefaultNodeSize}');");
                _browser.ExecScriptAsync($"setElementData('{_selectedNodes.ElementAt(i).Key}', 'border_color', 'rgb(0,0,0)');");
                _browser.ExecScriptAsync($"setElementData('{_selectedNodes.ElementAt(i).Key}', 'borderWidth', '1');");
                _browser.ExecScriptAsync($"cy.endBatch();");

            }

            await UpdateToolbarEnableForNodeEdgeAsync(countOfselectedNodesOnGraph, "shape");
        }


            private void btnObjectiveNode_Click(object sender, EventArgs e)
        {
            addNodeQuick("Objective", "rectangle", "Sum of all Fundamental Strength values", "Sum of all Implementation Strength values");
        }

        private void toolStripButton22_Click_1(object sender, EventArgs e)
        {
            addNodeQuick("Evidence", "rectangle", "Sum of all Fundamental Strength values", "Sum of all Implementation Strength values");
        }

        private void toolStripButton22_DoubleClick(object sender, EventArgs e)
        {

        }

        private void toolStripButton4_Click_1(object sender, EventArgs e)
        {
            addNodeQuick("Group", "group", "Sum of all Fundamental strength values", "Sum of all Implementation Strength values");
        }

        private void toolStripButton22_Click_3(object sender, EventArgs e)
        {
            addNodeQuick("Info", "info");
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            var w = this.Width;
            if (w < formWidthLimit)
            {
                panelUserLogin.Visible = false;
            }
            else
            {
                panelUserLogin.Visible = true;
            }
        }
        private void panelMainBrowser_Resize(object sender, System.EventArgs e)
        {
            var w = panelMainBrowser.Width;
            var h = panelMainBrowser.Height;
            _browser.ExecScriptAsync($"if ((typeof resizeMainBrowser) == 'function') resizeMainBrowser({w}, {h});");
        }

        private void toolBtnBackgroundColor_Click(object sender, System.EventArgs e)
        {
            ColorDialog colorDlg = new ColorDialog();

            if (colorDlg.ShowDialog() == System.Windows.Forms.DialogResult.OK) {
                _browser.ExecScriptAsync($"setBackgroundColor('rgb({colorDlg.Color.R}, {colorDlg.Color.G}, {colorDlg.Color.B})');");
            }
        }

        private void label74_Click(object sender, EventArgs e)
        {

        }

        private void btnAccountUser_Click(object sender, EventArgs e)
        {
            JObject accountDetail = AuthAPI.GetUser();
            UserDetailModal userDetailForm = new UserDetailModal();
            userDetailForm.SetUserData(accountDetail);
            userDetailForm.ribbonForm = this;
            userDetailForm.ShowDialog();
        }

        private void lblAttackImpactsAccountMax_Click(object sender, EventArgs e)
        {

        }


        public void ShowBusySpinner()
        {
            pbBusySpinner.Left = (this.ClientSize.Width - pbBusySpinner.Width) / 2;
            pbBusySpinner.Top = (this.ClientSize.Height - pbBusySpinner.Height) / 2;
            pbBusySpinner.Visible = true;
            pbBusySpinner.BringToFront();
        }
        public void HideBusySpinner()
        {
            pbBusySpinner.Visible = false;
        }

        private void btnAccountEnterprise_Click(object sender, EventArgs e)
        {
            changeEnterprise();
        }

        private void cmbImpactFrom_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            // Handle being called without a provided node
            if (_selectedNodes.Count > 0)
            {
                //updateNodeAttribute("asset", _selectedNodes.ElementAt(0).Value);
                nodeDistributionsForm.updateNodeAttribute("asset", _selectedNodes.ElementAt(0).Value);
            }
        }

        private async void toolStripMenuItem4_Click(object sender, EventArgs e)
        {

            StopCalcs = false;
            CalcLoops = int.Parse((sender as ToolStripItem).Tag.ToString());
            

            GraphCalcs.graphCalculated = false;
            var responce = await GraphSchemaCheck(false);
            if (responce.Result == true)
            {
                progressBar1.Value = 0;
                progressBar1.Maximum = 100;
                progressBar1.Visible = true;
                statusBarStopCalcs.Visible = true;

                backgroundWorker = new BackgroundWorker { WorkerReportsProgress = true };
                backgroundWorker.DoWork += BackgroundWorker_DoWork;
                backgroundWorker.ProgressChanged += BackgroundWorker_ProgressChanged;
                backgroundWorker.RunWorkerCompleted += BackgroundWorker_RunWorkerCompleted;
                backgroundWorker.RunWorkerAsync();
            }
           
        }

        private void BackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            int i = 0;
            double ProgressSteps = 100.0 / CalcLoops;
            double Progress = 0;
            bool currentAutoCalculate = GraphCalcs.autoCalculate;
            GraphCalcs.autoCalculate = true;

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            while (i < CalcLoops && StopCalcs == false)
            {
                i++;
                Task<Task<bool>> Task = GraphCalcs.RecalculateAll();
                Task.Wait();

                Progress = i * ProgressSteps;
                Progress = Math.Round(Progress);
                if (stopwatch.Elapsed >= TimeSpan.FromSeconds(1))
                {

                    backgroundWorker.ReportProgress((int)Progress);

                    stopwatch.Restart();
                }

            }
            GraphCalcs.autoCalculate = currentAutoCalculate;
        }

        private void BackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            CompletedGraphRecalc();
            progressBar1.Visible = false;
            statusBarStopCalcs.Visible = false;
            progressBar1.Value = 0;
        }

        private void BackgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
            progressBar1.Refresh();

        }


        private void clearDistributionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GraphUtil.ClearDistributionData();
            this.riskPanelsForm.InitializeCharts();
        }

        private void statusBarAdvPanel1_Click(object sender, EventArgs e)
        {
            StopCalcs = true;
        }

        private void toolStripEx12_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void toolStripButton28_Click_1(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void cmbGroupTotal_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cbAssetDistributions_CheckStateChanged(object sender, EventArgs e)
        {
           
        }


        private async void PrintImageHandler(object sender, PrintPageEventArgs ppeArgs)
        {
            var png64 = await _browser.EvaluateScriptAsync($"cy.jpg();");
            string img_str = png64.Result as string;
            Image img = LoadBase64(img_str.Substring(23));
            Graphics g = ppeArgs.Graphics;
            bitmap = new Bitmap(img);
            g.DrawImage(bitmap, 0, 0);

        }

        private async void toolBtnPrint_Click(object sender, EventArgs e)
        {
            var png64 = await _browser.EvaluateScriptAsync($"cy.png();");
            string img_str = png64.Result as string;
            Image img = LoadBase64(img_str.Substring(22));

            //Panel panel = new Panel();
            //this.Controls.Add(panelMainBrowser);
            Graphics grp = panelMainBrowser.CreateGraphics();
            Size formSize = this.panelMainBrowser.Size;
            bitmap = new Bitmap(img);// new Bitmap(MainBrowser_panel.Width, MainBrowser_panel.Height, grp);
            grp = Graphics.FromImage(bitmap);
            Point panelLocation = PointToScreen(panelMainBrowser.Location);
            //grp.CopyFromScreen(panelLocation.X, panelLocation.Y, 0, 0, formSize);
            printPreviewDialog.Document = printDocument;
            printPreviewDialog.PrintPreviewControl.Zoom = 1;
            printPreviewDialog.ShowDialog();
        }

        private async void PrintImage(object sender, PrintPageEventArgs e)
        {
            try
            {
                var png64 = await _browser.EvaluateScriptAsync($"cy.jpg();");
                string img_str = png64.Result as string;
                Image img = LoadBase64(img_str.Substring(23));
                e.Graphics.DrawImage(img, 0, 0, e.PageBounds.Width, e.PageBounds.Height);
            }
            catch
            { }

        }

        private void label2_MouseEnter(object sender, EventArgs e)
        {
            LogonPanelActive(true);
        }

        private void panel1_MouseEnter(object sender, EventArgs e)
        {
            LogonPanelActive(true);
        }

        private void panel1_MouseLeave(object sender, EventArgs e)
        {
            LogonPanelActive(false);
        }

        private void LogonPanelActive(bool active)
        {
            if (active)
                panelUserLogin.BackColor = Color.FromArgb(22, 81, 170);
            else
                panelUserLogin.BackColor = Color.FromArgb(0, 114, 198);
        }

        private void label1_MouseEnter(object sender, EventArgs e)
        {
            LogonPanelActive(true);
        }

        private void btnDistributions_CheckStateChanged(object sender, EventArgs e)
        {
            if (!dockingManager1.GetEnableDocking(nodeDistributionsForm.panelContainer))
            {
                dockingManager1.LockHostFormUpdate();
                dockingManager1.SetEnableDocking(nodeDistributionsForm.panelContainer, true);
                dockingManager1.DockControl(nodeDistributionsForm.panelContainer, this, DockingStyle.Right, 300);
                dockingManager1.SetDockVisibility(nodeDistributionsForm.panelContainer, this.btnDistributions.Checked);
                dockingManager1.SetDockLabel(nodeDistributionsForm.panelContainer, "Distributions");
            }

            if (this.btnDistributions.Checked)
            {
                this.dockingManager1.SetDockVisibility(nodeDistributionsForm.panelContainer, true);
            }
            else
            {
                this.dockingManager1.SetDockVisibility(nodeDistributionsForm.panelContainer, false);
            }
            this.dockingManager1.UnlockHostFormUpdate();

            _settings.ShowNodeValuesPanel = this.btnDistributions.Checked;
            _settings.Save();
        }

        private void btnDetail_CheckStateChanged(object sender, EventArgs e)
        {
            this.dockingManager1.LockHostFormUpdate();

            if (!dockingManager1.GetEnableDocking(this.nodePropertyForm.panelProperties))
            {
                dockingManager1.LockHostFormUpdate();
                dockingManager1.SetEnableDocking(this.nodePropertyForm.panelProperties, true);
                dockingManager1.DockControl(this.nodePropertyForm.panelProperties, this, DockingStyle.Right, 300);
                dockingManager1.SetDockVisibility(this.nodePropertyForm.panelProperties, this.btnDetail.Checked);
                dockingManager1.SetDockLabel(this.nodePropertyForm.panelProperties, "Details");
            }

            if (this.btnDetail.Checked)
            {
                this.dockingManager1.SetDockVisibility(this.nodePropertyForm.panelProperties, true);
            }
            else
            {
                this.dockingManager1.SetDockVisibility(this.nodePropertyForm.panelProperties, false);
            }


            this.dockingManager1.UnlockHostFormUpdate();

            _settings.ShowNodeInformation = this.btnDetail.Checked;
            _settings.Save();
        }

        private void cbRiskValues_Click(object sender, EventArgs e)
        {

        }

        private void btnValues_CheckStateChanged(object sender, EventArgs e)
        {
            this.dockingManager1.LockHostFormUpdate();

            if (!dockingManager1.GetEnableDocking(this.riskPanelsForm.panelRiskContainer))
            {
                dockingManager1.LockHostFormUpdate();
                dockingManager1.SetEnableDocking(this.riskPanelsForm.panelRiskContainer, true);
                dockingManager1.DockControl(this.riskPanelsForm.panelRiskContainer, this, DockingStyle.Left, 200);
                dockingManager1.SetDockVisibility(this.riskPanelsForm.panelRiskContainer, this.btnValues.Checked);
                dockingManager1.SetDockLabel(this.riskPanelsForm.panelRiskContainer, "Values");
            }

            if (this.btnValues.Checked)
            {
                this.dockingManager1.SetDockVisibility(this.riskPanelsForm.panelRiskContainer, true);
            }
            else
            {
                this.dockingManager1.SetDockVisibility(this.riskPanelsForm.panelRiskContainer, false);
            }
            this.dockingManager1.UnlockHostFormUpdate();

            _settings.ShowRiskPanel = this.btnValues.Checked;
            _settings.Save();
        }

        private void btnRiskList_CheckStateChanged(object sender, EventArgs e)
        {
            this.dockingManager1.LockHostFormUpdate();

            if (!this.dockingManager1.GetEnableDocking(this.riskPanelsForm.panelRiskAssetRiskList))
            {
                dockingManager1.SetEnableDocking(this.riskPanelsForm.panelRiskAssetRiskList, true);
                this.dockingManager1.DockControl(this.riskPanelsForm.panelRiskAssetRiskList, this.dockingClientPanel1, DockingStyle.Left, 300);
                this.dockingManager1.SetDockVisibility(this.riskPanelsForm.panelRiskAssetRiskList, this.btnRiskList.Checked);
                this.dockingManager1.SetDockLabel(this.riskPanelsForm.panelRiskAssetRiskList, "Risks");
            }

            if (this.btnRiskList.Checked)
            {
                this.dockingManager1.SetDockVisibility(this.riskPanelsForm.panelRiskAssetRiskList, true);

            }
            else
            {
                this.dockingManager1.SetDockVisibility(this.riskPanelsForm.panelRiskAssetRiskList, false);
            }
            this.dockingManager1.UnlockHostFormUpdate();

            _settings.ShowRiskListPanel = this.btnRiskList.Checked;
            _settings.Save();
        }

        private void btnRiskHeatMap_CheckStateChanged(object sender, EventArgs e)
        {
            this.dockingManager1.LockHostFormUpdate();

            if (!dockingManager1.GetEnableDocking(HeatMapForm.panelHeatMap))
            {
                HeatMapForm.panelHeatMap.Dock = DockStyle.Fill;
                dockingManager1.LockHostFormUpdate();
                dockingManager1.SetEnableDocking(HeatMapForm.panelHeatMap, true);
                dockingManager1.DockControl(HeatMapForm.panelHeatMap, this.dockingClientPanel1, DockingStyle.Left, 700);
                dockingManager1.SetDockVisibility(HeatMapForm.panelHeatMap, this.btnRiskHeatMap.Checked);
                dockingManager1.SetDockLabel(HeatMapForm.panelHeatMap, "Asset Risk Distributions");
            }

            if (this.btnRiskHeatMap.Checked)
            {
                this.dockingManager1.SetDockVisibility(HeatMapForm.panelHeatMap, true);
                HeatMapForm.InitializeAssetListBox();
            }
            else
            {
                this.dockingManager1.SetDockVisibility(HeatMapForm.panelHeatMap, false);
            }
            this.dockingManager1.UnlockHostFormUpdate();

             _settings.ShowRiskHeatMap = this.btnRiskHeatMap.Checked;
             _settings.Save();
        }

        private void btnGrid_CheckStateChanged(object sender, EventArgs e)
        {
            ShowHideGrid();
        }

        private void btnSwitch_Click(object sender, EventArgs e)
        {
            ShowHideLabels();
        }

        private void btnLabels_Click(object sender, EventArgs e)
        {
            ShowHideLabels();
        }

        private async void btnCompliance_CheckStateChanged(object sender, EventArgs e)
        {
            this.dockingManager1.LockHostFormUpdate();

            if (!dockingManager1.GetEnableDocking(this.complianceForm.panelComplianceContainer))
            {
                if (this.complianceForm.panelComplianceContainer.Width < 0) this.complianceForm.panelComplianceContainer.Width = 500;
                dockingManager1.SetEnableDocking(this.complianceForm.panelComplianceContainer, true);
                dockingManager1.DockControl(this.complianceForm.panelComplianceContainer, this.dockingClientPanel1, DockingStyle.Left, 500);
                this.dockingManager1.SetDockVisibility(this.complianceForm.panelComplianceContainer, this.btnCompliance.Checked);
                dockingManager1.SetDockLabel(this.complianceForm.panelComplianceContainer, "Compliance");
                this.complianceForm.initialized = true;
            }

            if (this.btnCompliance.Checked)
            {
                this.dockingManager1.SetDockVisibility(this.complianceForm.panelComplianceContainer, true);
            }
            else
            {
                this.dockingManager1.SetDockVisibility(this.complianceForm.panelComplianceContainer, false);
            }
            this.dockingManager1.UnlockHostFormUpdate();

            var selectedNodes = await _browser.EvaluateScriptAsync("getSelectedNodesAsJSON();");
            string selectedNodeID = "";
            if (selectedNodes.Success && selectedNodes.Result.ToString() != "[]")
            {
                var tmpObj = JArray.Parse(selectedNodes.Result.ToString());
                foreach (var tmpNode in tmpObj)
                {
                    Node node = Node.FromJson(tmpNode.ToString());
                    selectedNodeID = node.ID;
                    break;
                }
            }

            if (selectedNodeID != "")
            {
                ShowRequiredRiskPanels(selectedNodeID);
            }

            //_settings.ShowCompliancePanel = this.btnCompliance.Checked;
            //_settings.Save();
        }

        private void ckShowEdges_CheckStateChanged(object sender, EventArgs e)
        {
            _browser.ExecScriptAsync($"showEdge('{ckShowEdges.Checked}');");
        }

        private void ckShowActor_CheckStateChanged(object sender, EventArgs e)
        {
            ShowHideNodes();
        }

        private void ckShowAttack_CheckStateChanged(object sender, EventArgs e)
        {
            ShowHideNodes();
        }

        private void ckShowAsset_CheckStateChanged(object sender, EventArgs e)
        {
            ShowHideNodes();
        }

        private void ckShowControl_CheckStateChanged(object sender, EventArgs e)
        {
            ShowHideNodes();
        }

        private void ckShowEvidence_CheckStateChanged(object sender, EventArgs e)
        {
            ShowHideNodes();
        }

        private void ckShowGroup_CheckStateChanged(object sender, EventArgs e)
        {
            ShowHideNodes();
        }

        private void ckShowObjective_CheckStateChanged(object sender, EventArgs e)
        {
            ShowHideNodes();
        }

        private void ckShowVulnerability_CheckStateChanged(object sender, EventArgs e)
        {
            ShowHideNodes();
        }

        private void toolBtnRiskData_Click(object sender, EventArgs e)
        {
          
            
        }

        private void toolStripButton35_Click(object sender, EventArgs e)
        {
            addNodeQuick("Vulnerability-Group", "vulnerability-group");
        }

        private void btnRiskList_Click(object sender, EventArgs e)
        {

        }

        private void toolBtnRiskData_CheckStateChanged(object sender, EventArgs e)
        {
            if (!dockingManager1.GetEnableDocking(nodeDataForm.panelRiskGrid))
            {
                dockingManager1.LockHostFormUpdate();
                dockingManager1.SetEnableDocking(nodeDataForm.panelRiskGrid, true);
                dockingManager1.DockControl(nodeDataForm.panelRiskGrid, this, DockingStyle.Right, 400);
                dockingManager1.SetDockVisibility(nodeDataForm.panelRiskGrid, this.btnNodeData.Checked);
                dockingManager1.SetDockLabel(nodeDataForm.panelRiskGrid, "Node Data");
            }

            if (this.btnNodeData.Checked)
            {
                this.dockingManager1.SetDockVisibility(nodeDataForm.panelRiskGrid, true);
            }
            else
            {
                this.dockingManager1.SetDockVisibility(nodeDataForm.panelRiskGrid, false);
            }
            this.dockingManager1.UnlockHostFormUpdate();

            _settings.ShowNodeDataPanel = this.btnNodeData.Checked;
            _settings.Save();
        }


        private void toolBtnDockingSave_Click(object sender, EventArgs e)
        {
            string fileName = Utility.GenerateRandomString();
            string layoutName = toolCmbLayoutList.SelectedIndex < 0 ? 
                "" : toolCmbLayoutList.Items[toolCmbLayoutList.SelectedIndex].ToString();
            DockingLayoutNameModal dlnModal = new DockingLayoutNameModal();
            dlnModal.SetLayoutName("");
            bool flag = false;
            if (layoutName != "")
            {
                for (int i = 0; i < _settings.DockingLayouts.Count; i++)
                {
                    JObject layer_obj = _settings.DockingLayouts[i] as JObject;
                    if (layer_obj["layoutName"].ToString() == layoutName)
                    {
                        fileName = layer_obj["fileName"].ToString();
                        break;
                    }
                }
            }
            else
            {
                if (dlnModal.ShowDialog() == DialogResult.OK)
                {
                    layoutName = dlnModal.GetLayoutName();
                }
                else
                {
                    return;
                }
                flag = true;
            }
            AppStateSerializer serializer = new AppStateSerializer(SerializeMode.BinaryFile, fileName);
            this.dockingManager1.SaveDockState(serializer);
            serializer.PersistNow();

            if (flag)
            {
                JObject obj = new JObject();
                obj["layoutName"] = layoutName;
                obj["fileName"] = fileName;
                _settings.DockingLayouts.Add(obj);
                _settings.Save();

                toolCmbLayoutList.Items.Add(layoutName);
                toolCmbLayoutList.SelectedIndex = toolCmbLayoutList.Items.Count - 1;
            }
        }

        private void RefreshDockingList()
        {
            int selIndex = 0;
            toolCmbLayoutList.Items.Clear();
            if (_settings.DockingLayouts.Count == 0)
                return;
            for (int i = 0; i < _settings.DockingLayouts.Count; i++)
            {
                JObject obj = _settings.DockingLayouts[i] as JObject;
                string layoutName = obj["layoutName"].ToString();
                toolCmbLayoutList.Items.Add(layoutName);
            }
            if (selIndex + 1 > _settings.DockingLayouts.Count && toolCmbLayoutList.Items.Count > 0)
            {
                toolCmbLayoutList.SelectedIndex = selIndex;
            }
        }

        private void toolBtnDockingDelete_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to delete this layout?", "Confirm", MessageBoxButtons.YesNo); //Gets users input by showing the message box

            if (toolCmbLayoutList.SelectedIndex >= 0 && result == DialogResult.Yes) //Creates the yes function
            {
                int selectedIndex = toolCmbLayoutList.SelectedIndex;
                string layoutName = toolCmbLayoutList.Items[selectedIndex].ToString();
                toolCmbLayoutList.Items.RemoveAt(selectedIndex);

                _settings.DockingLayouts.RemoveAt(selectedIndex);
                _settings.Save();

                toolCmbLayoutList.SelectedIndex = (selectedIndex) < 0 ? 0 : selectedIndex - 1;
            }
        }

        private void toolBtnDockingRename_Click(object sender, EventArgs e)
        {
            int selectedIndex = toolCmbLayoutList.SelectedIndex;
            string layoutName = toolCmbLayoutList.Items[selectedIndex].ToString();

            DockingLayoutNameModal dlnModal = new DockingLayoutNameModal();
            dlnModal.SetLayoutName(layoutName);

            if (dlnModal.ShowDialog() == DialogResult.OK)
            {
                toolCmbLayoutList.Items[selectedIndex] = dlnModal.GetLayoutName();
                _settings.DockingLayouts[selectedIndex]["layoutName"] = dlnModal.GetLayoutName();
                _settings.Save();
            }
        }

        private void saveTimer_Tick(object sender, EventArgs e)
        {
            if (tmp_graph_file_auto_save_flag)
            {
                AutoSaveGraphData();
            }
            else
            {
                if (_settings.AutoSaveOnTimer)
                {
                    tmp_graph_file_auto_save_flag = true;
                }
            }
        }

        public void AutoSaveGraphData()
        {
            tmp_graph_file_path = tmp_graph_file_path == "" ? Utility.GraphTempPath() : tmp_graph_file_path;

            this.Text = $"CyConex Cyber Graph Studio: ({Path.GetFileName(tmp_graph_file_path)})";
            if (_hasUnsavedChanges)
            {
                _browser.ExecScriptAsync("increaseRevision();");
            }
            _ = SerializeDataToFile(tmp_graph_file_path);
        }

        public void StopTimerForSave()
        {
            saveTimer.Enabled = false;
        }

        private void toolCmbLayoutList_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            
        }

        private void toolBtnDockingApply_Click(object sender, EventArgs e)
        {
            if (toolCmbLayoutList.SelectedIndex == -1) return;
            string layoutName = toolCmbLayoutList.Items[toolCmbLayoutList.SelectedIndex].ToString();
            for (int i = 0; i < _settings.DockingLayouts.Count; i++)
            {
                JObject obj = _settings.DockingLayouts[i] as JObject;
                if (obj["layoutName"].ToString() == layoutName)
                {
                    string fileName = obj["fileName"].ToString();
                    AppStateSerializer serializer = new AppStateSerializer(SerializeMode.BinaryFile, fileName);

                    this.dockingManager1.LoadDockState(serializer);
                    serializer.PersistNow();
                    break;
                }
            }
        }

        private void toolStripButton36_Click(object sender, EventArgs e)
        {
            _browser.ExecScriptAsync($"ShowBubbleSet();");
        }

        private void toolStripButton37_Click(object sender, EventArgs e)
        {
            reportUtility.fullFileName = Utility.PromptForFile("Word Documents (.docx)|*.docx");
            reportUtility.LoadDocumentIntoMemory();

            reportUtility.ReportItem("<<AssetNodeTitlesBulletList>>");
            reportUtility.ReportItem("<<AssetGroupNodeTitlesBulletList>>");
            reportUtility.ReportItem("<<ActorNodeTitlesBulletList>>");
            reportUtility.ReportItem("<<AttackNodeTitlesBulletList>>");
            reportUtility.ReportItem("<<VulnerabilityNodeTitlesBulletList>>");
            reportUtility.ReportItem("<<ControlNodeTitlesBulletList>>");
            reportUtility.ReportItem("<<ObjectiveNodeTitlesBulletList>>");
            reportUtility.ReportItem("<<AssetNodeTitlesDescriptionBulletList>>");
            reportUtility.ReportItem("<<AssetGroupNodeTitlesDescriptionBulletList>>");
            reportUtility.ReportItem("<<ActorNodeTitlesDescriptionBulletList>>");
            reportUtility.ReportItem("<<AttackNodeTitlesDescriptionBulletList>>");
            reportUtility.ReportItem("<<VulnerabilityNodeTitlesDescriptionBulletList>>");
            reportUtility.ReportItem("<<ControlNodeTitlesDescriptionBulletList>>");
            reportUtility.ReportItem("<<ObjectiveNodeTitlesDescriptionBulletList>>");
            reportUtility.ReportItem("<<AssetNodeTitlesRefrenceDescriptionBulletList>>");
            reportUtility.ReportItem("<<AssetGroupNodeTitlesRefrenceDescriptionBulletList>>");
            reportUtility.ReportItem("<<ActorNodeTitlesRefrenceDescriptionBulletList>>");
            reportUtility.ReportItem("<<AttackNodeTitlesRefrenceDescriptionBulletList>>");
            reportUtility.ReportItem("<<VulnerabilityNodeTitlesRefrenceDescriptionBulletList>>");
            reportUtility.ReportItem("<<ControlNodeTitlesRefrenceDescriptionBulletList>>");
            reportUtility.ReportItem("<<ObjectiveNodeTitlesRefrenceDescriptionBulletList>>");
            reportUtility.ReportItem("<<AssetNodeTitlesNumberedList>>");
            reportUtility.ReportItem("<<AssetGroupNodeTitlesNumberedList>>");
            reportUtility.ReportItem("<<ActorNodeTitlesNumberedList>>");
            reportUtility.ReportItem("<<AttackNodeTitlesNumberedList>>");
            reportUtility.ReportItem("<<VulnerabilityNodeTitlesNumberedList>>");
            reportUtility.ReportItem("<<ControlNodeTitlesNumberedList>>");
            reportUtility.ReportItem("<<ObjectiveNodeTitlesNumberedList>>");
            reportUtility.ReportItem("<<AssetNodeTitlesDescriptionNumberedList>>");
            reportUtility.ReportItem("<<AssetGroupNodeTitlesDescriptionNumberedList>>");
            reportUtility.ReportItem("<<ActorNodeTitlesDescriptionNumberedList>>");
            reportUtility.ReportItem("<<AttackNodeTitlesDescriptionNumberedList>>");
            reportUtility.ReportItem("<<VulnerabilityNodeTitlesDescriptionNumberedList>>");
            reportUtility.ReportItem("<<ControlNodeTitlesDescriptionNumberedList>>");
            reportUtility.ReportItem("<<ObjectiveNodeTitlesDescriptionNumberedList>>");
            reportUtility.ReportItem("<<AssetNodeTitlesRefrenceDescriptionNumberedList>>");
            reportUtility.ReportItem("<<AssetGroupNodeTitlesRefrenceDescriptionNumberedList>>");
            reportUtility.ReportItem("<<ActorNodeTitlesRefrenceDescriptionNumberedList>>");
            reportUtility.ReportItem("<<AttackNodeTitlesRefrenceDescriptionNumberedList>>");
            reportUtility.ReportItem("<<VulnerabilityNodeTitlesRefrenceDescriptionNumberedList>>");
            reportUtility.ReportItem("<<ControlNodeTitlesRefrenceDescriptionNumberedList>>");
            reportUtility.ReportItem("<<ObjectiveNodeTitlesRefrenceDescriptionNumberedList>>");
            reportUtility.ReportItem("<<AssetNodeTitlesRefrenceDescriptionTable>>");
            reportUtility.ReportItem("<<AssetGroupNodeTitlesRefrenceDescriptionTable>>");
            reportUtility.ReportItem("<<ActorNodeTitlesRefrenceDescriptionTable>>");
            reportUtility.ReportItem("<<AttackNodeTitlesRefrenceDescriptionTable>>");  
            reportUtility.ReportItem("<<VulnerabilityNodeTitlesRefrenceDescriptionTable>>");
            reportUtility.ReportItem("<<ControlNodeTitlesRefrenceDescriptionTable>>");
            reportUtility.ReportItem("<<ObjectiveNodeTitlesRefrenceDescriptionTable>>");
            reportUtility.ReportItem("<<ControlNodeScoreTDFRSITableLow>>");
            reportUtility.ReportItem("<<ControlNodeScoreTDFRSITableLow3>>");
            reportUtility.ReportItem("<<ControlNodeScoreTDFRSITableLow5>>");
            reportUtility.ReportItem("<<ControlNodeScoreTDFRSITableLow10>>");
            reportUtility.ReportItem("<<ControlNodeScoreTDFRSITableHigh>>");
            reportUtility.ReportItem("<<ControlNodeScoreTDFRSITableHigh3>>");
            reportUtility.ReportItem("<<ControlNodeScoreTDFRSITableHigh5>>");
            reportUtility.ReportItem("<<ControlNodeScoreTDFRSITableHigh10>>");
            reportUtility.ReportItem("<<ObjectiveNodeScoreTDFRSITableLow>>");
            reportUtility.ReportItem("<<ObjectiveNodeScoreTDFRSITableLow3>>");
            reportUtility.ReportItem("<<ObjectiveNodeScoreTDFRSITableLow5>>");
            reportUtility.ReportItem("<<ObjectiveNodeScoreTDFRSITableLow10>>");
            reportUtility.ReportItem("<<ObjectiveNodeScoreTDFRSITableHigh>>");
            reportUtility.ReportItem("<<ObjectiveNodeScoreTDFRSITableHigh3>>");
            reportUtility.ReportItem("<<ObjectiveNodeScoreTDFRSITableHigh5>>");
            reportUtility.ReportItem("<<ObjectiveNodeScoreTDFRSITableHigh10>>");
            reportUtility.SaveDocumentToFile(reportUtility.GetFileDirectory() + "\\report.docx");
            reportUtility.OpenFile(reportUtility.GetFileDirectory() + "\\report.docx");
        }

        private void toolBtnDeleteElement_Click(object sender, EventArgs e)
        {
            _browser.ExecScriptAsync($"deleteElement();");
        }

        private void toolBtnNodePlus_Click(object sender, EventArgs e)
        {
            _browser.ExecScriptAsync($"plusNodeSize();");
        }

        private void toolBtnNodeMinus_Click(object sender, EventArgs e)
        {
            _browser.ExecScriptAsync($"minusNodeSize();");
        }

        private void toolStripEx26_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void btnNodeRepository_CheckedChanged(object sender, EventArgs e)
        {
            if (!dockingManager1.GetEnableDocking(this.nodeRepositoryForm.panelNodeRepository))
            {
                dockingManager1.LockHostFormUpdate();
                dockingManager1.SetEnableDocking(this.nodeRepositoryForm.panelNodeRepository, true);
                dockingManager1.DockControl(this.nodeRepositoryForm.panelNodeRepository, this, DockingStyle.Left, 300);
                dockingManager1.SetDockVisibility(this.nodeRepositoryForm.panelNodeRepository, false);
                dockingManager1.SetDockLabel(nodeRepositoryForm.panelNodeRepository, "Node Repository");

            }


            if (this.btnNodeRepository.Checked)
            {
                this.dockingManager1.SetDockVisibility(this.nodeRepositoryForm.panelNodeRepository, true);
            }
            else
            {
                this.dockingManager1.SetDockVisibility(this.nodeRepositoryForm.panelNodeRepository, false);
            }
            this.dockingManager1.UnlockHostFormUpdate();

            _settings.ShowNodeRepository = this.btnNodeRepository.Checked;
            _settings.Save();
        }

        private void btnTippyShow_Click(object sender, EventArgs e)
        {
            if (btnTippyShow.Checked == true)
            {
                _browser.ExecScriptAsync($"showHideTippy(true);");
            }
            else
            {
                _browser.ExecScriptAsync($"showHideTippy(false);");
            }
        }

        private void AutoResizeNodes()
        {
            Console.WriteLine("MainForm > AutoResizeNodes");
            decimal defaultNodeSize = _settings.DefaultNodeSize;
            decimal nodeSize = defaultNodeSize;

            List<string> nodes = new List<string>();
            nodes = GraphUtil.GetAllNodes();

            foreach (var nodeID in nodes)
            {
                int node_count = GraphUtil.CountOfAllAncestorNodes(nodeID);
                nodeSize = defaultNodeSize * Decimal.Parse(Utility.CalculateSizeIncreaseFactor(node_count).ToString());
                nodeSize = nodeSize < defaultNodeSize ? defaultNodeSize : nodeSize;
                _browser.ExecScriptAsync($"setElementSize('{nodeID}', '{nodeSize}', '{nodeSize}');");
            }


        }

        private void ResetNodeSize()
        {
            Console.WriteLine("MainForm > ResetNodeSize");
            decimal defaultNodeSize = _settings.DefaultNodeSize;
            decimal nodeSize = defaultNodeSize;

            List<string> nodes = new List<string>();
            nodes = GraphUtil.GetAllNodes();

            foreach (var nodeID in nodes)
            {
                _browser.ExecScriptAsync($"setElementSize('{nodeID}', '{nodeSize}', '{nodeSize}');");
            }
        }

        private void btnLabels_CheckStateChanged(object sender, EventArgs e)
        {
            if (btnLabels.Checked)
                btnLabels.Text = "Hide\r\nNode Labels";
            else
                btnLabels.Text = "Show\r\nNode Labels";
        }

        private void btnSwitch_CheckStateChanged(object sender, EventArgs e)
        {
            if (btnSwitch.Checked)
                btnSwitch.Text = "Show Node\r\nReference";
            else
                btnSwitch.Text = "Show Node\r\nTitle";
        }

        private void miRiskHeatmap_Click(object sender, EventArgs e)
        {
            UpdateHeatmap();
        }

        private void UpdateHeatmap(bool flag = false)
        {
            if (flag)
            {
                if (miRiskHeatmap.Checked == false && miComplianceHeatmap.Checked == false)
                {
                    miRiskHeatmap.Checked = true;
                }
            }

            if (!miRiskHeatmap.Checked)
            {
                miRiskHeatmap.Checked = true;
                miComplianceHeatmap.Checked = false;
                _browser.ExecuteScriptAsync($"generate_heatmap('true','risk');");
            }
            else
            {
                miRiskHeatmap.Checked = false;
                _browser.ExecuteScriptAsync($"generate_heatmap('false','risk');");
            }
        }
        private void miComplianceHeatmap_Click(object sender, EventArgs e)
        {
            if (!miComplianceHeatmap.Checked)
            {
                miComplianceHeatmap.Checked = true;
                miRiskHeatmap.Checked = false;
                _browser.ExecuteScriptAsync($"generate_heatmap('true','compliance');");
            }
            else
            {
                miComplianceHeatmap.Checked = false;
                _browser.ExecuteScriptAsync($"generate_heatmap('false','compliance');");
            }
        }

        private async void UpdateEdgeTransparent(ToolStripMenuItem toolItem, double opacity)
        {
            ToolStripItemCollection collections = toolBtnEdgeTransparency.DropDownItems;
            foreach (ToolStripMenuItem item in collections)
            {
                item.Checked = false;
            }
            toolItem.Checked = true;

            var allEdges = await _browser.EvaluateScriptAsync("getEdges();");
            var tmpObj = JArray.Parse(allEdges.Result.ToString());
            foreach (var tmpEdge in tmpObj)
            {
                var tmpJson = JObject.Parse(tmpEdge.ToString());
                var tmpID = tmpJson["id"];
                _browser.ExecScriptAsync($"setElementData('{tmpID}', 'opacity', '{opacity}');");
            }
        }
        private void miEdgeTrans0_Click(object sender, EventArgs e)
        {
            UpdateEdgeTransparent(miEdgeTrans0, 1.0);
        }

        private void miEdgeTrans25_Click(object sender, EventArgs e)
        {
            UpdateEdgeTransparent(miEdgeTrans25, 0.75);
        }

        private void miEdgeTrans50_Click(object sender, EventArgs e)
        {
            UpdateEdgeTransparent(miEdgeTrans50, 0.5);
        }

        private void miEdgeTrans75_Click(object sender, EventArgs e)
        {
            UpdateEdgeTransparent(miEdgeTrans75, 0.25);
        }

        private void miEdgeTrans100_Click(object sender, EventArgs e)
        {
            UpdateEdgeTransparent(miEdgeTrans100, 0);
        }

        private void toolStripButton32_Click_1Async(object sender, EventArgs e)
        {
            _ = UpdateAllSelectedNodes();
        }

        private void toolBtnSaveAs_Click(object sender, EventArgs e)
        {
            DockingLayoutNameModal dlnModal = new DockingLayoutNameModal();
            dlnModal.SetLayoutName("");
            if (dlnModal.ShowDialog() == DialogResult.OK)
            {
                string layoutName = dlnModal.GetLayoutName();
                string fileName = Utility.GenerateRandomString();
                AppStateSerializer serializer = new AppStateSerializer(SerializeMode.BinaryFile, fileName);
                this.dockingManager1.SaveDockState(serializer);
                serializer.PersistNow();

                JObject obj = new JObject();
                obj["layoutName"] = layoutName;
                obj["fileName"] = fileName;
                _settings.DockingLayouts.Add(obj);
                _settings.Save();

                toolCmbLayoutList.Items.Add(layoutName);
                toolCmbLayoutList.SelectedIndex = toolCmbLayoutList.Items.Count - 1;
            }
        }

        private void toolBtnAvsdfLayout_Click(object sender, EventArgs e)
        {
            changeLayout("{ name: 'avsdf', animate: true }");
        }

        private void toolBtnCiseLayout_Click(object sender, EventArgs e)
        {
            changeLayout("{ name: 'cise', animate: false, nodeSeparation: 12.5, allowNodesInsideCircle: false, " +
                "maxRatioOfNodesInsideCircle: 0.1, refresh: 1, idealInterClusterEdgeLengthCoefficient: 1.4}");
        }

        private void toolBtnCoseBilkentLayout_Click(object sender, EventArgs e)
        {
            changeLayout("{ name: 'cose-bilkent', animate: true }");
        }

        private void toolBtnElkLayered_Click(object sender, EventArgs e)
        {
            changeLayout("{ name: 'elk', elk:{algorithm: 'layered',}, animate: true, }");
        }

        private void toolBtnElkMrTree_Click(object sender, EventArgs e)
        {
            changeLayout("{ name: 'elk', elk:{algorithm: 'mrtree', hierarchicalNodeWidth: 1000, animTimeFactor: 1000, nodeNode: 100, hierarchicalNodeWidth: 100}, animate: true}");
        }

        private void btnNodeAutoSize_CheckStateChanged(object sender, EventArgs e)
        {
            if (btnNodeAutoSize.Checked)
                AutoResizeNodes();
            else
                ResetNodeSize();
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            addNodeQuick("asset", "asset", "Sum of all Fundamental strength values", "Sum of all Implementation Strength values");
        }
    }
}
