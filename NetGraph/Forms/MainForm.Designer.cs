
using CyConex.Controls;
using CyConex.Forms;
using CyConex.Graph;
using Newtonsoft.Json.Linq;
using Syncfusion.Windows.Forms.Tools;
using System;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace CyConex
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            Syncfusion.Windows.Forms.Gauge.LinearRange linearRange1 = new Syncfusion.Windows.Forms.Gauge.LinearRange();
            Syncfusion.Windows.Forms.Gauge.LinearRange linearRange2 = new Syncfusion.Windows.Forms.Gauge.LinearRange();
            Syncfusion.Windows.Forms.Gauge.LinearRange linearRange3 = new Syncfusion.Windows.Forms.Gauge.LinearRange();
            Syncfusion.Windows.Forms.Gauge.LinearRange linearRange4 = new Syncfusion.Windows.Forms.Gauge.LinearRange();
            Syncfusion.Windows.Forms.Gauge.LinearRange linearRange5 = new Syncfusion.Windows.Forms.Gauge.LinearRange();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.ribbonControlAdv1 = new Syncfusion.Windows.Forms.Tools.RibbonControlAdv();
            this.ribbonTabFile = new Syncfusion.Windows.Forms.Tools.ToolStripTabItem();
            this.toolStripEx23 = new Syncfusion.Windows.Forms.Tools.ToolStripEx();
            this.btnAccountLogin = new System.Windows.Forms.ToolStripButton();
            this.btnAccountEnterprise = new System.Windows.Forms.ToolStripButton();
            this.btnAccountTenant = new System.Windows.Forms.ToolStripButton();
            this.btnAccountUser = new System.Windows.Forms.ToolStripButton();
            this.toolStripEx29 = new Syncfusion.Windows.Forms.Tools.ToolStripEx();
            this.btnNewGraph = new System.Windows.Forms.ToolStripButton();
            this.toolStripEx4 = new Syncfusion.Windows.Forms.Tools.ToolStripEx();
            this.btnSaveCloud = new System.Windows.Forms.ToolStripButton();
            this.btnOpenCloud = new System.Windows.Forms.ToolStripButton();
            this.btnSaveAsCloud = new System.Windows.Forms.ToolStripButton();
            this.toolStripEx5 = new Syncfusion.Windows.Forms.Tools.ToolStripEx();
            this.btnOpenLocal = new System.Windows.Forms.ToolStripButton();
            this.btnSaveLocal = new System.Windows.Forms.ToolStripButton();
            this.btnSaveAsLocal = new System.Windows.Forms.ToolStripButton();
            this.toolStripEx24 = new Syncfusion.Windows.Forms.Tools.ToolStripEx();
            this.toolBtnPrint = new System.Windows.Forms.ToolStripButton();
            this.toolStripEx25 = new Syncfusion.Windows.Forms.Tools.ToolStripEx();
            this.btnSettingsForm = new System.Windows.Forms.ToolStripButton();
            this.ribbonTabHome = new Syncfusion.Windows.Forms.Tools.ToolStripTabItem();
            this.toolStripEx1 = new Syncfusion.Windows.Forms.Tools.ToolStripEx();
            this.toolStripUndo = new System.Windows.Forms.ToolStripButton();
            this.toolStripRedo = new System.Windows.Forms.ToolStripButton();
            this.toolStripEx15 = new Syncfusion.Windows.Forms.Tools.ToolStripEx();
            this.toolBtnCopy = new System.Windows.Forms.ToolStripButton();
            this.toolBtnPast = new System.Windows.Forms.ToolStripButton();
            this.toolBtnCopyImage = new System.Windows.Forms.ToolStripButton();
            this.toolStripEx14 = new Syncfusion.Windows.Forms.Tools.ToolStripEx();
            this.toolStripButton57 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton8 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton6 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.toolStripEx2 = new Syncfusion.Windows.Forms.Tools.ToolStripEx();
            this.btnAddObjectiveNode = new System.Windows.Forms.ToolStripButton();
            this.btnAddEvidenceNode = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton23 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSplitButton1 = new System.Windows.Forms.ToolStripSplitButton();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripEx6 = new Syncfusion.Windows.Forms.Tools.ToolStripEx();
            this.toolStripButton4 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton22 = new System.Windows.Forms.ToolStripButton();
            this.toolStripEx26 = new Syncfusion.Windows.Forms.Tools.ToolStripEx();
            this.btnDrawEdges = new System.Windows.Forms.ToolStripButton();
            this.toolStripEx35 = new Syncfusion.Windows.Forms.Tools.ToolStripEx();
            this.toolBtnDeleteElement = new System.Windows.Forms.ToolStripButton();
            this.toolStripEx16 = new Syncfusion.Windows.Forms.Tools.ToolStripEx();
            this.toolStripEx30 = new Syncfusion.Windows.Forms.Tools.ToolStripPanelItem();
            this.cbAutoCalc = new Syncfusion.Windows.Forms.Tools.ToolStripCheckBox();
            this.toolStripButton19 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSplitButton2 = new System.Windows.Forms.ToolStripSplitButton();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem8 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem9 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem12 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem13 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.clearDistributionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripEx11 = new Syncfusion.Windows.Forms.Tools.ToolStripEx();
            this.toolStripButton5 = new System.Windows.Forms.ToolStripButton();
            this.toolStripEx28 = new Syncfusion.Windows.Forms.Tools.ToolStripEx();
            this.toolStripButton18 = new System.Windows.Forms.ToolStripButton();
            this.toolBtnFind = new System.Windows.Forms.ToolStripButton();
            this.ribbonTabView = new Syncfusion.Windows.Forms.Tools.ToolStripTabItem();
            this.toolStripEx32 = new Syncfusion.Windows.Forms.Tools.ToolStripEx();
            this.btnDetail = new System.Windows.Forms.ToolStripButton();
            this.btnDistributions = new System.Windows.Forms.ToolStripButton();
            this.btnValues = new System.Windows.Forms.ToolStripButton();
            this.btnNodeData = new System.Windows.Forms.ToolStripButton();
            this.btnCompliance = new System.Windows.Forms.ToolStripButton();
            this.btnRiskHeatMap = new System.Windows.Forms.ToolStripButton();
            this.toolStripEx22 = new Syncfusion.Windows.Forms.Tools.ToolStripEx();
            this.toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
            this.ckShowEdges = new System.Windows.Forms.ToolStripMenuItem();
            this.ckShowActor = new System.Windows.Forms.ToolStripMenuItem();
            this.ckShowAttack = new System.Windows.Forms.ToolStripMenuItem();
            this.ckShowAsset = new System.Windows.Forms.ToolStripMenuItem();
            this.ckShowControl = new System.Windows.Forms.ToolStripMenuItem();
            this.ckShowEvidence = new System.Windows.Forms.ToolStripMenuItem();
            this.ckShowGroup = new System.Windows.Forms.ToolStripMenuItem();
            this.ckShowObjective = new System.Windows.Forms.ToolStripMenuItem();
            this.ckShowVulnerability = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripPanelItem9 = new Syncfusion.Windows.Forms.Tools.ToolStripPanelItem();
            this.btnGrid = new System.Windows.Forms.ToolStripButton();
            this.btnLabels = new System.Windows.Forms.ToolStripButton();
            this.btnSwitch = new System.Windows.Forms.ToolStripButton();
            this.toolStripPanelItem11 = new Syncfusion.Windows.Forms.Tools.ToolStripPanelItem();
            this.btnTippyShow = new System.Windows.Forms.ToolStripButton();
            this.toolBtnEdgeTransparency = new System.Windows.Forms.ToolStripDropDownButton();
            this.miEdgeTrans0 = new System.Windows.Forms.ToolStripMenuItem();
            this.miEdgeTrans25 = new System.Windows.Forms.ToolStripMenuItem();
            this.miEdgeTrans50 = new System.Windows.Forms.ToolStripMenuItem();
            this.miEdgeTrans75 = new System.Windows.Forms.ToolStripMenuItem();
            this.miEdgeTrans100 = new System.Windows.Forms.ToolStripMenuItem();
            this.btnHeatMaps = new System.Windows.Forms.ToolStripDropDownButton();
            this.miRiskHeatmap = new System.Windows.Forms.ToolStripMenuItem();
            this.miComplianceHeatmap = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripEx33 = new Syncfusion.Windows.Forms.Tools.ToolStripEx();
            this.toolStripEx20 = new Syncfusion.Windows.Forms.Tools.ToolStripEx();
            this.toolStripPanelItem16 = new Syncfusion.Windows.Forms.Tools.ToolStripPanelItem();
            this.toolCmbLayoutList = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripPanelItem17 = new Syncfusion.Windows.Forms.Tools.ToolStripPanelItem();
            this.toolBtnSaveAs = new System.Windows.Forms.ToolStripButton();
            this.toolBtnDockingSave = new System.Windows.Forms.ToolStripButton();
            this.toolBtnDockingDelete = new System.Windows.Forms.ToolStripButton();
            this.toolBtnDockingRename = new System.Windows.Forms.ToolStripButton();
            this.toolBtnDockingApply = new System.Windows.Forms.ToolStripButton();
            this.toolStripEx31 = new Syncfusion.Windows.Forms.Tools.ToolStripEx();
            this.btnRiskList = new System.Windows.Forms.ToolStripButton();
            this.toolStripEx34 = new Syncfusion.Windows.Forms.Tools.ToolStripEx();
            this.toolStripEx10 = new Syncfusion.Windows.Forms.Tools.ToolStripEx();
            this.toolStripPanelItem5 = new Syncfusion.Windows.Forms.Tools.ToolStripPanelItem();
            this.toolStripPanelItem8 = new Syncfusion.Windows.Forms.Tools.ToolStripPanelItem();
            this.cbNodePanel = new Syncfusion.Windows.Forms.Tools.ToolStripCheckBox();
            this.toolStripEx27 = new Syncfusion.Windows.Forms.Tools.ToolStripEx();
            this.btnHeatMap = new System.Windows.Forms.ToolStripButton();
            this.btnNodePath = new System.Windows.Forms.ToolStripButton();
            this.ribbonTabLayout = new Syncfusion.Windows.Forms.Tools.ToolStripTabItem();
            this.toolStripEx8 = new Syncfusion.Windows.Forms.Tools.ToolStripEx();
            this.toolStripButton10 = new System.Windows.Forms.ToolStripButton();
            this.toolStripEx9 = new Syncfusion.Windows.Forms.Tools.ToolStripEx();
            this.AlignToGrid_toolStripMenuItem = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton12 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton13 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton14 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton15 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton16 = new System.Windows.Forms.ToolStripButton();
            this.toolBtnAvsdfLayout = new System.Windows.Forms.ToolStripButton();
            this.toolBtnCiseLayout = new System.Windows.Forms.ToolStripButton();
            this.toolBtnCoseBilkentLayout = new System.Windows.Forms.ToolStripButton();
            this.toolBtnElkLayered = new System.Windows.Forms.ToolStripButton();
            this.toolBtnElkMrTree = new System.Windows.Forms.ToolStripButton();
            this.ribbonTabNode = new Syncfusion.Windows.Forms.Tools.ToolStripTabItem();
            this.toolStripEx37 = new Syncfusion.Windows.Forms.Tools.ToolStripEx();
            this.btnNodeManager = new System.Windows.Forms.ToolStripButton();
            this.btnNodeRepository = new System.Windows.Forms.ToolStripButton();
            this.toolStripEx3 = new Syncfusion.Windows.Forms.Tools.ToolStripEx();
            this.toolSuggested = new System.Windows.Forms.ToolStripButton();
            this.toolSugObjectives = new System.Windows.Forms.ToolStripButton();
            this.toolSugControls = new System.Windows.Forms.ToolStripButton();
            this.toolSugAttacks = new System.Windows.Forms.ToolStripButton();
            this.toolSugGroups = new System.Windows.Forms.ToolStripButton();
            this.toolSugActor = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnNodeAutoSize = new System.Windows.Forms.ToolStripButton();
            this.ribbonTabFormat = new Syncfusion.Windows.Forms.Tools.ToolStripTabItem();
            this.toolStripEx13 = new Syncfusion.Windows.Forms.Tools.ToolStripEx();
            this.toolStripPanelItem13 = new Syncfusion.Windows.Forms.Tools.ToolStripPanelItem();
            this.toolBtnEnableBackgroundEdit = new System.Windows.Forms.ToolStripButton();
            this.btnBackgroundImage = new System.Windows.Forms.ToolStripButton();
            this.btnClearBackgroundImage = new System.Windows.Forms.ToolStripButton();
            this.toolBtnFullSize = new System.Windows.Forms.ToolStripButton();
            this.toolBtnBackgroundColor = new System.Windows.Forms.ToolStripButton();
            this.toolStripEx17 = new Syncfusion.Windows.Forms.Tools.ToolStripEx();
            this.toolBtnTextLeft = new System.Windows.Forms.ToolStripButton();
            this.toolBtnLabelPosPanel = new Syncfusion.Windows.Forms.Tools.ToolStripPanelItem();
            this.toolBtnTextTop = new System.Windows.Forms.ToolStripButton();
            this.toolBtnTextCenter = new System.Windows.Forms.ToolStripButton();
            this.toolBtnTextBottom = new System.Windows.Forms.ToolStripButton();
            this.toolBtnTextRight = new System.Windows.Forms.ToolStripButton();
            this.toolStripEx36 = new Syncfusion.Windows.Forms.Tools.ToolStripEx();
            this.toolBtnNodeMinus = new System.Windows.Forms.ToolStripButton();
            this.toolBtnNodePlus = new System.Windows.Forms.ToolStripButton();
            this.toolStripEx18 = new Syncfusion.Windows.Forms.Tools.ToolStripEx();
            this.toolStripPanelItem14 = new Syncfusion.Windows.Forms.Tools.ToolStripPanelItem();
            this.toolBtnSetNodeImage = new System.Windows.Forms.ToolStripButton();
            this.toolBtnClearNodeImage = new System.Windows.Forms.ToolStripButton();
            this.toolStripEx19 = new Syncfusion.Windows.Forms.Tools.ToolStripEx();
            this.toolBtnShapePanel = new Syncfusion.Windows.Forms.Tools.ToolStripPanelItem();
            this.toolStripLabel3 = new System.Windows.Forms.ToolStripLabel();
            this.toolComboShape = new Syncfusion.Windows.Forms.Tools.ToolStripComboBoxEx();
            this.toolBtnFillPanel = new Syncfusion.Windows.Forms.Tools.ToolStripPanelItem();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.toolBtnFillColor = new System.Windows.Forms.ToolStripButton();
            this.toolTextFillColor = new System.Windows.Forms.ToolStripTextBox();
            this.toolBtnBorderPanel = new Syncfusion.Windows.Forms.Tools.ToolStripPanelItem();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.toolBtnBorderColor = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabel4 = new System.Windows.Forms.ToolStripLabel();
            this.toolBtnBorderWidth = new Syncfusion.Windows.Forms.Tools.ToolStripComboBoxEx();
            this.ribbonTabDev = new Syncfusion.Windows.Forms.Tools.ToolStripTabItem();
            this.toolStripEx7 = new Syncfusion.Windows.Forms.Tools.ToolStripEx();
            this.toolStripButton7 = new System.Windows.Forms.ToolStripButton();
            this.ShowCalculationLog_toolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripEx12 = new Syncfusion.Windows.Forms.Tools.ToolStripEx();
            this.toolStripButton9 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton17 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton21 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton20 = new System.Windows.Forms.ToolStripButton();
            this.toolBtnOpenAuditlog = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton27 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton28 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton36 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton37 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton32 = new System.Windows.Forms.ToolStripButton();
            this.ribbonTabHelp = new Syncfusion.Windows.Forms.Tools.ToolStripTabItem();
            this.toolStripEx21 = new Syncfusion.Windows.Forms.Tools.ToolStripEx();
            this.toolStripButton34 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton29 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton30 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton31 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton33 = new System.Windows.Forms.ToolStripButton();
            this.toolBtnFontPanel = new Syncfusion.Windows.Forms.Tools.ToolStripPanelItem();
            this.toolBtnFontFamily = new Syncfusion.Windows.Forms.Tools.ToolStripComboBoxEx();
            this.toolBtnFontSize = new Syncfusion.Windows.Forms.Tools.ToolStripComboBoxEx();
            this.toolBtnFontStylePanel = new Syncfusion.Windows.Forms.Tools.ToolStripPanelItem();
            this.toolBtnFontBold = new System.Windows.Forms.ToolStripButton();
            this.toolBtnFontItalic = new System.Windows.Forms.ToolStripButton();
            this.toolBtnFontColor = new System.Windows.Forms.ToolStripButton();
            this.toolBtnFontSizePlus = new System.Windows.Forms.ToolStripButton();
            this.toolBtnFontSizeMinus = new System.Windows.Forms.ToolStripButton();
            this.toolStripPanelItem10 = new Syncfusion.Windows.Forms.Tools.ToolStripPanelItem();
            this.backImg = new System.Windows.Forms.ToolStripButton();
            this.btnDrawEdge = new Syncfusion.Windows.Forms.Tools.ToolStripPanelItem();
            this.drawEdges_ToolStrip = new System.Windows.Forms.ToolStripButton();
            this.toolStripPanelItem2 = new Syncfusion.Windows.Forms.Tools.ToolStripPanelItem();
            this.toolStripLabel5 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripDrawnEdgeColor = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabel6 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripeDrawnEdgeWidth = new Syncfusion.Windows.Forms.Tools.ToolStripComboBoxEx();
            this.toolStripPanelItem1 = new Syncfusion.Windows.Forms.Tools.ToolStripPanelItem();
            this.label32 = new System.Windows.Forms.Label();
            this.toolStripPanelItem7 = new Syncfusion.Windows.Forms.Tools.ToolStripPanelItem();
            this.toolStripButton44 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton45 = new System.Windows.Forms.ToolStripButton();
            this.toolStripNodeManager = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton24 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton25 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton26 = new System.Windows.Forms.ToolStripButton();
            this.NodeID_columnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.NodeText_columnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.FRLayout_toolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.KKLayout_toolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ISOMLayout_toolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.LinLogLayout_toolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SimpleTreeLayout_toolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SimpleCircleLayout_toolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SigiyamaLayout_toolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CompoundFDPLayout_toolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.EndAlgo_toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.NoOverlap_toolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.FSA_toolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.FSAOneWay_toolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.designTimeTabTypeLoader = new Syncfusion.Reflection.TypeLoader(this.components);
            this.nodeColorDialog = new System.Windows.Forms.ColorDialog();
            this.barItem1 = new Syncfusion.Windows.Forms.Tools.XPMenus.BarItem();
            this.barItem2 = new Syncfusion.Windows.Forms.Tools.XPMenus.BarItem();
            this.barItem3 = new Syncfusion.Windows.Forms.Tools.XPMenus.BarItem();
            this.parentBarItem1 = new Syncfusion.Windows.Forms.Tools.XPMenus.ParentBarItem();
            this.controlBarItem = new Syncfusion.Windows.Forms.Tools.XPMenus.BarItem();
            this.assetBarItem = new Syncfusion.Windows.Forms.Tools.XPMenus.BarItem();
            this.groupBarItem = new Syncfusion.Windows.Forms.Tools.XPMenus.BarItem();
            this.attackBarItem = new Syncfusion.Windows.Forms.Tools.XPMenus.BarItem();
            this.objectiveBarItem = new Syncfusion.Windows.Forms.Tools.XPMenus.BarItem();
            this.popupMenusManager = new Syncfusion.Windows.Forms.Tools.XPMenus.PopupMenusManager(this.components);
            this.toolStripPanelItem4 = new Syncfusion.Windows.Forms.Tools.ToolStripPanelItem();
            this.toolStripLabel7 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripButton11 = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabel8 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripComboBoxEx2 = new Syncfusion.Windows.Forms.Tools.ToolStripComboBoxEx();
            this.printDocument = new System.Drawing.Printing.PrintDocument();
            this.printPreviewDialog = new System.Windows.Forms.PrintPreviewDialog();
            this.label6 = new System.Windows.Forms.Label();
            this.splashControl1 = new Syncfusion.Windows.Forms.Tools.SplashControl();
            this.panelMainBrowser = new System.Windows.Forms.Panel();
            this.panel205 = new System.Windows.Forms.Panel();
            this.panel206 = new System.Windows.Forms.Panel();
            this.linearGauge1 = new Syncfusion.Windows.Forms.Gauge.LinearGauge();
            this.panelRiskDataGrid = new System.Windows.Forms.Panel();
            this.statusBarAdv1 = new Syncfusion.Windows.Forms.Tools.StatusBarAdv();
            this.statusBarImage = new Syncfusion.Windows.Forms.Tools.StatusBarAdvPanel();
            this.statusBarCalcTime = new Syncfusion.Windows.Forms.Tools.StatusBarAdvPanel();
            this.progressBar1 = new Syncfusion.Windows.Forms.Tools.ProgressBarAdv();
            this.statusBarStopCalcs = new Syncfusion.Windows.Forms.Tools.StatusBarAdvPanel();
            this.statusBarNodeGUID = new Syncfusion.Windows.Forms.Tools.StatusBarAdvPanel();
            this.nodeListItemBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.flowLayout1 = new Syncfusion.Windows.Forms.Tools.FlowLayout(this.components);
            this.ilForm1 = new System.Windows.Forms.ImageList(this.components);
            this.pbBusySpinner = new System.Windows.Forms.PictureBox();
            this.dockingClientPanel1 = new Syncfusion.Windows.Forms.Tools.DockingClientPanel();
            this.dockingManager1 = new Syncfusion.Windows.Forms.Tools.DockingManager(this.components);
            this.panelUserLogin = new System.Windows.Forms.Panel();
            this.pbUserLoginStatus = new System.Windows.Forms.PictureBox();
            this.lblUserEmail = new System.Windows.Forms.Label();
            this.lblUserName = new System.Windows.Forms.Label();
            this.toolStripPanelItem6 = new Syncfusion.Windows.Forms.Tools.ToolStripPanelItem();
            this.toolStripComboBox1 = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripPanelItem12 = new Syncfusion.Windows.Forms.Tools.ToolStripPanelItem();
            this.toolStripComboBox2 = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripPanelItem15 = new Syncfusion.Windows.Forms.Tools.ToolStripPanelItem();
            this.saveTimer = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControlAdv1)).BeginInit();
            this.ribbonControlAdv1.SuspendLayout();
            this.ribbonTabFile.Panel.SuspendLayout();
            this.toolStripEx23.SuspendLayout();
            this.toolStripEx29.SuspendLayout();
            this.toolStripEx4.SuspendLayout();
            this.toolStripEx5.SuspendLayout();
            this.toolStripEx24.SuspendLayout();
            this.toolStripEx25.SuspendLayout();
            this.ribbonTabHome.Panel.SuspendLayout();
            this.toolStripEx1.SuspendLayout();
            this.toolStripEx15.SuspendLayout();
            this.toolStripEx14.SuspendLayout();
            this.toolStripEx2.SuspendLayout();
            this.toolStripEx6.SuspendLayout();
            this.toolStripEx26.SuspendLayout();
            this.toolStripEx35.SuspendLayout();
            this.toolStripEx16.SuspendLayout();
            this.toolStripEx11.SuspendLayout();
            this.toolStripEx28.SuspendLayout();
            this.ribbonTabView.Panel.SuspendLayout();
            this.toolStripEx32.SuspendLayout();
            this.toolStripEx22.SuspendLayout();
            this.toolStripEx20.SuspendLayout();
            this.toolStripEx31.SuspendLayout();
            this.toolStripEx10.SuspendLayout();
            this.toolStripEx27.SuspendLayout();
            this.ribbonTabLayout.Panel.SuspendLayout();
            this.toolStripEx8.SuspendLayout();
            this.toolStripEx9.SuspendLayout();
            this.ribbonTabNode.Panel.SuspendLayout();
            this.toolStripEx37.SuspendLayout();
            this.toolStripEx3.SuspendLayout();
            this.ribbonTabFormat.Panel.SuspendLayout();
            this.toolStripEx13.SuspendLayout();
            this.toolStripEx17.SuspendLayout();
            this.toolStripEx36.SuspendLayout();
            this.toolStripEx18.SuspendLayout();
            this.toolStripEx19.SuspendLayout();
            this.ribbonTabDev.Panel.SuspendLayout();
            this.toolStripEx7.SuspendLayout();
            this.toolStripEx12.SuspendLayout();
            this.ribbonTabHelp.Panel.SuspendLayout();
            this.toolStripEx21.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.statusBarAdv1)).BeginInit();
            this.statusBarAdv1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.statusBarImage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.statusBarCalcTime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.progressBar1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.statusBarStopCalcs)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.statusBarNodeGUID)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nodeListItemBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.flowLayout1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbBusySpinner)).BeginInit();
            this.dockingClientPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dockingManager1)).BeginInit();
            this.panelUserLogin.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbUserLoginStatus)).BeginInit();
            this.SuspendLayout();
            // 
            // ribbonControlAdv1
            // 
            this.ribbonControlAdv1.BackColor = System.Drawing.Color.White;
            this.ribbonControlAdv1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ribbonControlAdv1.BorderStyle = Syncfusion.Windows.Forms.Tools.ToolStripBorderStyle.StaticEdge;
            this.ribbonControlAdv1.CanApplyTheme = false;
            this.ribbonControlAdv1.CanOverrideStyle = true;
            this.ribbonControlAdv1.CaptionFont = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ribbonControlAdv1.CausesValidation = false;
            this.ribbonControlAdv1.Dock = Syncfusion.Windows.Forms.Tools.DockStyleEx.Top;
            this.ribbonControlAdv1.EnableQATCustomization = false;
            this.ribbonControlAdv1.EnableRibbonCustomization = false;
            this.ribbonControlAdv1.EnableRibbonStateAccelerator = false;
            this.ribbonControlAdv1.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.ribbonControlAdv1.Header.AddMainItem(ribbonTabFile);
            this.ribbonControlAdv1.Header.AddMainItem(ribbonTabHome);
            this.ribbonControlAdv1.Header.AddMainItem(ribbonTabView);
            this.ribbonControlAdv1.Header.AddMainItem(ribbonTabLayout);
            this.ribbonControlAdv1.Header.AddMainItem(ribbonTabNode);
            this.ribbonControlAdv1.Header.AddMainItem(ribbonTabFormat);
            this.ribbonControlAdv1.Header.AddMainItem(ribbonTabDev);
            this.ribbonControlAdv1.Header.AddMainItem(ribbonTabHelp);
            this.ribbonControlAdv1.LauncherStyle = Syncfusion.Windows.Forms.Tools.LauncherStyle.Metro;
            this.ribbonControlAdv1.Location = new System.Drawing.Point(0, 0);
            this.ribbonControlAdv1.Margin = new System.Windows.Forms.Padding(0);
            this.ribbonControlAdv1.MenuButtonFont = new System.Drawing.Font("Segoe UI", 8.25F);
            this.ribbonControlAdv1.MenuButtonText = "File";
            this.ribbonControlAdv1.MenuButtonVisible = false;
            this.ribbonControlAdv1.MenuButtonWidth = 56;
            this.ribbonControlAdv1.MenuColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(114)))), ((int)(((byte)(198)))));
            this.ribbonControlAdv1.Name = "ribbonControlAdv1";
            this.ribbonControlAdv1.OfficeColorScheme = Syncfusion.Windows.Forms.Tools.ToolStripEx.ColorScheme.Managed;
            // 
            // ribbonControlAdv1.OfficeMenu
            // 
            this.ribbonControlAdv1.OfficeMenu.Name = "OfficeMenu";
            this.ribbonControlAdv1.OfficeMenu.ShowItemToolTips = true;
            this.ribbonControlAdv1.OfficeMenu.Size = new System.Drawing.Size(12, 65);
            this.ribbonControlAdv1.QuickPanelImageLayout = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.ribbonControlAdv1.QuickPanelVisible = false;
            this.ribbonControlAdv1.RibbonHeaderImage = Syncfusion.Windows.Forms.Tools.RibbonHeaderImage.None;
            this.ribbonControlAdv1.RibbonStyle = Syncfusion.Windows.Forms.Tools.RibbonStyle.Office2016;
            this.ribbonControlAdv1.ScaleMenuButtonImage = false;
            this.ribbonControlAdv1.SelectedTab = this.ribbonTabHome;
            this.ribbonControlAdv1.ShowContextMenu = false;
            this.ribbonControlAdv1.ShowLauncher = false;
            this.ribbonControlAdv1.ShowRibbonDisplayOptionButton = false;
            this.ribbonControlAdv1.Size = new System.Drawing.Size(1335, 154);
            this.ribbonControlAdv1.SystemText.QuickAccessDialogDropDownName = "Start menu";
            this.ribbonControlAdv1.SystemText.RenameDisplayLabelText = "&Display Name:";
            this.ribbonControlAdv1.TabIndex = 0;
            this.ribbonControlAdv1.ThemeName = "Office2016";
            this.ribbonControlAdv1.ThemeStyle.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(90)))), ((int)(((byte)(189)))));
            this.ribbonControlAdv1.ThemeStyle.CloseButtonBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(90)))), ((int)(((byte)(189)))));
            this.ribbonControlAdv1.ThemeStyle.ControlBoxBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(90)))), ((int)(((byte)(189)))));
            this.ribbonControlAdv1.ThemeStyle.HeaderBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(90)))), ((int)(((byte)(189)))));
            this.ribbonControlAdv1.ThemeStyle.HoverControlBoxBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(90)))), ((int)(((byte)(189)))));
            this.ribbonControlAdv1.ThemeStyle.HoverQuickDropDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(90)))), ((int)(((byte)(189)))));
            this.ribbonControlAdv1.ThemeStyle.MoreCommandsStyle.PropertyGridViewBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(171)))), ((int)(((byte)(171)))), ((int)(((byte)(171)))));
            this.ribbonControlAdv1.ThemeStyle.RibbonPanelStyle.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(90)))), ((int)(((byte)(189)))));
            this.ribbonControlAdv1.ThemeStyle.RibbonPanelStyle.HoverCollapseButtonBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(90)))), ((int)(((byte)(189)))));
            this.ribbonControlAdv1.ThemeStyle.RibbonPanelStyle.PanelBackColor = System.Drawing.Color.White;
            this.ribbonControlAdv1.ThemeStyle.TabBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.ribbonControlAdv1.ThemeStyle.TabBottomLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(90)))), ((int)(((byte)(189)))));
            this.ribbonControlAdv1.ThemeStyle.TabFont = new System.Drawing.Font("Arial", 9F);
            this.ribbonControlAdv1.ThemeStyle.TabForeColor = System.Drawing.Color.Black;
            this.ribbonControlAdv1.ThemeStyle.TabGroupBackColor = System.Drawing.Color.Maroon;
            this.ribbonControlAdv1.ThemeStyle.TabGroupForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.ribbonControlAdv1.ThemeStyle.TabSelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.ribbonControlAdv1.ThemeStyle.TabSeparatorColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.ribbonControlAdv1.TitleAlignment = Syncfusion.Windows.Forms.Tools.TextAlignment.Left;
            this.ribbonControlAdv1.TitleColor = System.Drawing.Color.White;
            this.ribbonControlAdv1.TitleFont = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            // 
            // ribbonTabFile
            // 
            this.ribbonTabFile.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.ribbonTabFile.BackColor = System.Drawing.Color.RoyalBlue;
            this.ribbonTabFile.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.ribbonTabFile.Name = "ribbonTabFile";
            this.ribbonTabFile.Padding = new System.Windows.Forms.Padding(0);
            // 
            // ribbonControlAdv1.ribbonPanel1
            // 
            this.ribbonTabFile.Panel.Controls.Add(this.toolStripEx23);
            this.ribbonTabFile.Panel.Controls.Add(this.toolStripEx29);
            this.ribbonTabFile.Panel.Controls.Add(this.toolStripEx4);
            this.ribbonTabFile.Panel.Controls.Add(this.toolStripEx5);
            this.ribbonTabFile.Panel.Controls.Add(this.toolStripEx24);
            this.ribbonTabFile.Panel.Controls.Add(this.toolStripEx25);
            this.ribbonTabFile.Panel.Margin = new System.Windows.Forms.Padding(0);
            this.ribbonTabFile.Panel.Name = "ribbonPanel1";
            this.ribbonTabFile.Panel.Padding = new System.Windows.Forms.Padding(0);
            this.ribbonTabFile.Panel.ScrollPosition = 0;
            this.ribbonTabFile.Panel.ShowCaption = true;
            this.ribbonTabFile.Panel.TabIndex = 12;
            this.ribbonTabFile.Panel.Text = "File";
            this.ribbonTabFile.Position = 0;
            this.ribbonTabFile.Size = new System.Drawing.Size(48, 25);
            this.ribbonTabFile.Tag = "0";
            this.ribbonTabFile.Text = "File";
            // 
            // toolStripEx23
            // 
            this.toolStripEx23.BackColor = System.Drawing.Color.IndianRed;
            this.toolStripEx23.CanOverflow = false;
            this.toolStripEx23.CanOverrideStyle = true;
            this.toolStripEx23.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStripEx23.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.toolStripEx23.ForeColor = System.Drawing.Color.Black;
            this.toolStripEx23.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStripEx23.Image = null;
            this.toolStripEx23.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnAccountLogin,
            this.btnAccountEnterprise,
            this.btnAccountTenant,
            this.btnAccountUser});
            this.toolStripEx23.Location = new System.Drawing.Point(0, 0);
            this.toolStripEx23.Name = "toolStripEx23";
            this.toolStripEx23.Office12Mode = false;
            this.toolStripEx23.Padding = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.toolStripEx23.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.toolStripEx23.Size = new System.Drawing.Size(202, 86);
            this.toolStripEx23.TabIndex = 2;
            this.toolStripEx23.Text = "Account";
            this.toolStripEx23.ThemeName = "";
            // 
            // btnAccountLogin
            // 
            this.btnAccountLogin.Image = ((System.Drawing.Image)(resources.GetObject("btnAccountLogin.Image")));
            this.btnAccountLogin.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnAccountLogin.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAccountLogin.Name = "btnAccountLogin";
            this.btnAccountLogin.Size = new System.Drawing.Size(40, 70);
            this.btnAccountLogin.Text = "Login";
            this.btnAccountLogin.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnAccountLogin.Click += new System.EventHandler(this.btnAccountLogin_Click);
            // 
            // btnAccountEnterprise
            // 
            this.btnAccountEnterprise.Enabled = false;
            this.btnAccountEnterprise.Image = ((System.Drawing.Image)(resources.GetObject("btnAccountEnterprise.Image")));
            this.btnAccountEnterprise.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnAccountEnterprise.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAccountEnterprise.Name = "btnAccountEnterprise";
            this.btnAccountEnterprise.Size = new System.Drawing.Size(63, 70);
            this.btnAccountEnterprise.Text = "Enterprise";
            this.btnAccountEnterprise.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnAccountEnterprise.Click += new System.EventHandler(this.btnAccountEnterprise_Click);
            // 
            // btnAccountTenant
            // 
            this.btnAccountTenant.Enabled = false;
            this.btnAccountTenant.Image = ((System.Drawing.Image)(resources.GetObject("btnAccountTenant.Image")));
            this.btnAccountTenant.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnAccountTenant.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAccountTenant.Name = "btnAccountTenant";
            this.btnAccountTenant.Size = new System.Drawing.Size(46, 70);
            this.btnAccountTenant.Text = "Tenant";
            this.btnAccountTenant.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnAccountTenant.Click += new System.EventHandler(this.btnAccountTenant_Click);
            // 
            // btnAccountUser
            // 
            this.btnAccountUser.Enabled = false;
            this.btnAccountUser.Image = ((System.Drawing.Image)(resources.GetObject("btnAccountUser.Image")));
            this.btnAccountUser.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnAccountUser.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAccountUser.Name = "btnAccountUser";
            this.btnAccountUser.Size = new System.Drawing.Size(46, 70);
            this.btnAccountUser.Text = "Details";
            this.btnAccountUser.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnAccountUser.Click += new System.EventHandler(this.btnAccountUser_Click);
            // 
            // toolStripEx29
            // 
            this.toolStripEx29.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStripEx29.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.toolStripEx29.ForeColor = System.Drawing.Color.MidnightBlue;
            this.toolStripEx29.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStripEx29.Image = null;
            this.toolStripEx29.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNewGraph});
            this.toolStripEx29.Location = new System.Drawing.Point(0, 0);
            this.toolStripEx29.Name = "toolStripEx29";
            this.toolStripEx29.Office12Mode = false;
            this.toolStripEx29.Padding = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.toolStripEx29.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.toolStripEx29.Size = new System.Drawing.Size(43, 86);
            this.toolStripEx29.TabIndex = 5;
            this.toolStripEx29.Text = "Graph";
            // 
            // btnNewGraph
            // 
            this.btnNewGraph.Enabled = false;
            this.btnNewGraph.Image = ((System.Drawing.Image)(resources.GetObject("btnNewGraph.Image")));
            this.btnNewGraph.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnNewGraph.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnNewGraph.Name = "btnNewGraph";
            this.btnNewGraph.Size = new System.Drawing.Size(36, 70);
            this.btnNewGraph.Text = "New";
            this.btnNewGraph.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNewGraph.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnNewGraph.Click += new System.EventHandler(this.btnNewGraph_Click);
            // 
            // toolStripEx4
            // 
            this.toolStripEx4.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStripEx4.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.toolStripEx4.ForeColor = System.Drawing.Color.Black;
            this.toolStripEx4.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStripEx4.Image = null;
            this.toolStripEx4.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnSaveCloud,
            this.btnOpenCloud,
            this.btnSaveAsCloud});
            this.toolStripEx4.Location = new System.Drawing.Point(0, 0);
            this.toolStripEx4.Name = "toolStripEx4";
            this.toolStripEx4.Office12Mode = false;
            this.toolStripEx4.Padding = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.toolStripEx4.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.toolStripEx4.Size = new System.Drawing.Size(132, 86);
            this.toolStripEx4.TabIndex = 0;
            this.toolStripEx4.Text = "Cloud";
            this.toolStripEx4.ThemeName = "";
            this.toolStripEx4.ThemeStyle.BackColor = System.Drawing.Color.White;
            this.toolStripEx4.ThemeStyle.BottomToolStripBackColor = System.Drawing.Color.White;
            this.toolStripEx4.ThemeStyle.CaptionBackColor = System.Drawing.Color.White;
            // 
            // btnSaveCloud
            // 
            this.btnSaveCloud.Enabled = false;
            this.btnSaveCloud.Image = ((System.Drawing.Image)(resources.GetObject("btnSaveCloud.Image")));
            this.btnSaveCloud.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnSaveCloud.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSaveCloud.Name = "btnSaveCloud";
            this.btnSaveCloud.Size = new System.Drawing.Size(36, 70);
            this.btnSaveCloud.Text = "Save";
            this.btnSaveCloud.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnSaveCloud.Click += new System.EventHandler(this.btnSaveCloud_Click);
            // 
            // btnOpenCloud
            // 
            this.btnOpenCloud.Enabled = false;
            this.btnOpenCloud.Image = ((System.Drawing.Image)(resources.GetObject("btnOpenCloud.Image")));
            this.btnOpenCloud.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnOpenCloud.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnOpenCloud.Name = "btnOpenCloud";
            this.btnOpenCloud.Size = new System.Drawing.Size(40, 70);
            this.btnOpenCloud.Text = "Open";
            this.btnOpenCloud.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnOpenCloud.Click += new System.EventHandler(this.btnOpenCloud_Click);
            // 
            // btnSaveAsCloud
            // 
            this.btnSaveAsCloud.Enabled = false;
            this.btnSaveAsCloud.Image = ((System.Drawing.Image)(resources.GetObject("btnSaveAsCloud.Image")));
            this.btnSaveAsCloud.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnSaveAsCloud.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSaveAsCloud.Name = "btnSaveAsCloud";
            this.btnSaveAsCloud.Size = new System.Drawing.Size(49, 70);
            this.btnSaveAsCloud.Text = "Save As";
            this.btnSaveAsCloud.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnSaveAsCloud.Click += new System.EventHandler(this.btnSaveAsCloud_Click);
            // 
            // toolStripEx5
            // 
            this.toolStripEx5.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStripEx5.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.toolStripEx5.ForeColor = System.Drawing.Color.Black;
            this.toolStripEx5.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStripEx5.Image = null;
            this.toolStripEx5.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.toolStripEx5.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnOpenLocal,
            this.btnSaveLocal,
            this.btnSaveAsLocal});
            this.toolStripEx5.Location = new System.Drawing.Point(0, 0);
            this.toolStripEx5.Name = "toolStripEx5";
            this.toolStripEx5.Office12Mode = false;
            this.toolStripEx5.Padding = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.toolStripEx5.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.toolStripEx5.Size = new System.Drawing.Size(132, 86);
            this.toolStripEx5.TabIndex = 1;
            this.toolStripEx5.Text = "Local Files";
            this.toolStripEx5.ThemeName = "";
            this.toolStripEx5.ThemeStyle.BackColor = System.Drawing.Color.White;
            this.toolStripEx5.ThemeStyle.BottomToolStripBackColor = System.Drawing.Color.White;
            this.toolStripEx5.ThemeStyle.CaptionBackColor = System.Drawing.Color.White;
            // 
            // btnOpenLocal
            // 
            this.btnOpenLocal.Enabled = false;
            this.btnOpenLocal.Image = ((System.Drawing.Image)(resources.GetObject("btnOpenLocal.Image")));
            this.btnOpenLocal.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnOpenLocal.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnOpenLocal.Name = "btnOpenLocal";
            this.btnOpenLocal.Size = new System.Drawing.Size(40, 70);
            this.btnOpenLocal.Text = "Open";
            this.btnOpenLocal.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnOpenLocal.Click += new System.EventHandler(this.btnOpenLocal_Click);
            // 
            // btnSaveLocal
            // 
            this.btnSaveLocal.Enabled = false;
            this.btnSaveLocal.Image = ((System.Drawing.Image)(resources.GetObject("btnSaveLocal.Image")));
            this.btnSaveLocal.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnSaveLocal.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSaveLocal.Name = "btnSaveLocal";
            this.btnSaveLocal.Size = new System.Drawing.Size(36, 70);
            this.btnSaveLocal.Text = "Save";
            this.btnSaveLocal.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnSaveLocal.Click += new System.EventHandler(this.btnSaveLocal_Click);
            // 
            // btnSaveAsLocal
            // 
            this.btnSaveAsLocal.Enabled = false;
            this.btnSaveAsLocal.Image = ((System.Drawing.Image)(resources.GetObject("btnSaveAsLocal.Image")));
            this.btnSaveAsLocal.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnSaveAsLocal.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSaveAsLocal.Name = "btnSaveAsLocal";
            this.btnSaveAsLocal.Size = new System.Drawing.Size(49, 70);
            this.btnSaveAsLocal.Text = "Save As";
            this.btnSaveAsLocal.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnSaveAsLocal.Click += new System.EventHandler(this.btnSaveAsLocal_Click);
            // 
            // toolStripEx24
            // 
            this.toolStripEx24.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStripEx24.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.toolStripEx24.ForeColor = System.Drawing.Color.Black;
            this.toolStripEx24.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStripEx24.Image = null;
            this.toolStripEx24.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolBtnPrint});
            this.toolStripEx24.Location = new System.Drawing.Point(0, 0);
            this.toolStripEx24.Name = "toolStripEx24";
            this.toolStripEx24.Office12Mode = false;
            this.toolStripEx24.Padding = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.toolStripEx24.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.toolStripEx24.Size = new System.Drawing.Size(43, 86);
            this.toolStripEx24.TabIndex = 3;
            this.toolStripEx24.Text = "Print";
            this.toolStripEx24.ThemeName = "";
            this.toolStripEx24.ThemeStyle.BackColor = System.Drawing.Color.White;
            this.toolStripEx24.ThemeStyle.BottomToolStripBackColor = System.Drawing.Color.White;
            this.toolStripEx24.ThemeStyle.CaptionBackColor = System.Drawing.Color.White;
            // 
            // toolBtnPrint
            // 
            this.toolBtnPrint.Image = ((System.Drawing.Image)(resources.GetObject("toolBtnPrint.Image")));
            this.toolBtnPrint.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolBtnPrint.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolBtnPrint.Name = "toolBtnPrint";
            this.toolBtnPrint.Size = new System.Drawing.Size(36, 70);
            this.toolBtnPrint.Text = "Print";
            this.toolBtnPrint.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolBtnPrint.Click += new System.EventHandler(this.toolBtnPrint_Click);
            // 
            // toolStripEx25
            // 
            this.toolStripEx25.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStripEx25.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.toolStripEx25.ForeColor = System.Drawing.Color.Black;
            this.toolStripEx25.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStripEx25.Image = null;
            this.toolStripEx25.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnSettingsForm});
            this.toolStripEx25.Location = new System.Drawing.Point(0, 0);
            this.toolStripEx25.Name = "toolStripEx25";
            this.toolStripEx25.Office12Mode = false;
            this.toolStripEx25.Padding = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.toolStripEx25.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.toolStripEx25.Size = new System.Drawing.Size(60, 86);
            this.toolStripEx25.TabIndex = 4;
            this.toolStripEx25.Text = "Settings";
            this.toolStripEx25.ThemeName = "";
            this.toolStripEx25.ThemeStyle.BackColor = System.Drawing.Color.White;
            this.toolStripEx25.ThemeStyle.BottomToolStripBackColor = System.Drawing.Color.White;
            this.toolStripEx25.ThemeStyle.CaptionBackColor = System.Drawing.Color.White;
            // 
            // btnSettingsForm
            // 
            this.btnSettingsForm.Image = ((System.Drawing.Image)(resources.GetObject("btnSettingsForm.Image")));
            this.btnSettingsForm.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnSettingsForm.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSettingsForm.Name = "btnSettingsForm";
            this.btnSettingsForm.Size = new System.Drawing.Size(53, 70);
            this.btnSettingsForm.Text = "Settings";
            this.btnSettingsForm.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnSettingsForm.Click += new System.EventHandler(this.btnSettingsForm_Click);
            // 
            // ribbonTabHome
            // 
            this.ribbonTabHome.Name = "ribbonTabHome";
            // 
            // ribbonControlAdv1.ribbonPanel2
            // 
            this.ribbonTabHome.Panel.Controls.Add(this.toolStripEx1);
            this.ribbonTabHome.Panel.Controls.Add(this.toolStripEx15);
            this.ribbonTabHome.Panel.Controls.Add(this.toolStripEx14);
            this.ribbonTabHome.Panel.Controls.Add(this.toolStripEx2);
            this.ribbonTabHome.Panel.Controls.Add(this.toolStripEx6);
            this.ribbonTabHome.Panel.Controls.Add(this.toolStripEx26);
            this.ribbonTabHome.Panel.Controls.Add(this.toolStripEx35);
            this.ribbonTabHome.Panel.Controls.Add(this.toolStripEx16);
            this.ribbonTabHome.Panel.Controls.Add(this.toolStripEx11);
            this.ribbonTabHome.Panel.Controls.Add(this.toolStripEx28);
            this.ribbonTabHome.Panel.Name = "ribbonPanel2";
            this.ribbonTabHome.Panel.OfficeColorScheme = Syncfusion.Windows.Forms.Tools.ToolStripEx.ColorScheme.Silver;
            this.ribbonTabHome.Panel.ScrollPosition = 0;
            this.ribbonTabHome.Panel.TabIndex = 11;
            this.ribbonTabHome.Panel.Text = "Home";
            this.ribbonTabHome.Position = 1;
            this.ribbonTabHome.Size = new System.Drawing.Size(53, 25);
            this.ribbonTabHome.Tag = "9";
            this.ribbonTabHome.Text = "Home";
            // 
            // toolStripEx1
            // 
            this.toolStripEx1.AutoSize = false;
            this.toolStripEx1.CanOverrideStyle = true;
            this.toolStripEx1.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStripEx1.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.toolStripEx1.ForeColor = System.Drawing.Color.Black;
            this.toolStripEx1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStripEx1.Image = null;
            this.toolStripEx1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripUndo,
            this.toolStripRedo});
            this.toolStripEx1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.VerticalStackWithOverflow;
            this.toolStripEx1.Location = new System.Drawing.Point(0, 1);
            this.toolStripEx1.Name = "toolStripEx1";
            this.toolStripEx1.Office12Mode = false;
            this.toolStripEx1.Padding = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.toolStripEx1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.toolStripEx1.Size = new System.Drawing.Size(42, 90);
            this.toolStripEx1.TabIndex = 0;
            this.toolStripEx1.Text = "Undo";
            this.toolStripEx1.ThemeName = "";
            this.toolStripEx1.ThemeStyle.BackColor = System.Drawing.Color.White;
            this.toolStripEx1.ThemeStyle.BottomToolStripBackColor = System.Drawing.Color.White;
            this.toolStripEx1.ThemeStyle.CaptionBackColor = System.Drawing.Color.White;
            // 
            // toolStripUndo
            // 
            this.toolStripUndo.AutoSize = false;
            this.toolStripUndo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripUndo.Image = ((System.Drawing.Image)(resources.GetObject("toolStripUndo.Image")));
            this.toolStripUndo.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripUndo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripUndo.Name = "toolStripUndo";
            this.toolStripUndo.Size = new System.Drawing.Size(32, 40);
            this.toolStripUndo.Text = "toolStripUndo";
            this.toolStripUndo.Click += new System.EventHandler(this.toolStripUndo_Click);
            // 
            // toolStripRedo
            // 
            this.toolStripRedo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripRedo.Image = ((System.Drawing.Image)(resources.GetObject("toolStripRedo.Image")));
            this.toolStripRedo.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripRedo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripRedo.Name = "toolStripRedo";
            this.toolStripRedo.Size = new System.Drawing.Size(35, 20);
            this.toolStripRedo.Text = "toolStripButton21";
            this.toolStripRedo.Click += new System.EventHandler(this.toolStripRedo_Click);
            // 
            // toolStripEx15
            // 
            this.toolStripEx15.AutoSize = false;
            this.toolStripEx15.BackColor = System.Drawing.Color.Transparent;
            this.toolStripEx15.CanOverrideStyle = true;
            this.toolStripEx15.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStripEx15.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.toolStripEx15.ForeColor = System.Drawing.Color.Black;
            this.toolStripEx15.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStripEx15.Image = null;
            this.toolStripEx15.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolBtnCopy,
            this.toolBtnPast,
            this.toolBtnCopyImage});
            this.toolStripEx15.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.toolStripEx15.Location = new System.Drawing.Point(44, 1);
            this.toolStripEx15.Name = "toolStripEx15";
            this.toolStripEx15.Office12Mode = false;
            this.toolStripEx15.Padding = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.toolStripEx15.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.toolStripEx15.Size = new System.Drawing.Size(81, 90);
            this.toolStripEx15.TabIndex = 1;
            this.toolStripEx15.Text = "Clipboard";
            this.toolStripEx15.ThemeName = "";
            this.toolStripEx15.ThemeStyle.BackColor = System.Drawing.Color.White;
            this.toolStripEx15.ThemeStyle.BottomToolStripBackColor = System.Drawing.Color.White;
            this.toolStripEx15.ThemeStyle.CaptionBackColor = System.Drawing.Color.White;
            // 
            // toolBtnCopy
            // 
            this.toolBtnCopy.Enabled = false;
            this.toolBtnCopy.Image = ((System.Drawing.Image)(resources.GetObject("toolBtnCopy.Image")));
            this.toolBtnCopy.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolBtnCopy.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolBtnCopy.Name = "toolBtnCopy";
            this.toolBtnCopy.Size = new System.Drawing.Size(37, 49);
            this.toolBtnCopy.Text = "Copy";
            this.toolBtnCopy.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolBtnCopy.Click += new System.EventHandler(this.toolBtnCopy_Click);
            // 
            // toolBtnPast
            // 
            this.toolBtnPast.Enabled = false;
            this.toolBtnPast.Image = ((System.Drawing.Image)(resources.GetObject("toolBtnPast.Image")));
            this.toolBtnPast.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolBtnPast.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolBtnPast.Name = "toolBtnPast";
            this.toolBtnPast.Size = new System.Drawing.Size(38, 49);
            this.toolBtnPast.Text = "Paste";
            this.toolBtnPast.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolBtnPast.Click += new System.EventHandler(this.toolBtnPast_ClickAsync);
            // 
            // toolBtnCopyImage
            // 
            this.toolBtnCopyImage.AutoSize = false;
            this.toolBtnCopyImage.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolBtnCopyImage.Image = ((System.Drawing.Image)(resources.GetObject("toolBtnCopyImage.Image")));
            this.toolBtnCopyImage.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolBtnCopyImage.Name = "toolBtnCopyImage";
            this.toolBtnCopyImage.Size = new System.Drawing.Size(23, 20);
            this.toolBtnCopyImage.Text = "Copy Image to Clipboard";
            this.toolBtnCopyImage.Click += new System.EventHandler(this.toolBtnCopyImage_Click);
            // 
            // toolStripEx14
            // 
            this.toolStripEx14.CanOverrideStyle = true;
            this.toolStripEx14.CaptionAlignment = Syncfusion.Windows.Forms.Tools.CaptionAlignment.Center;
            this.ribbonControlAdv1.SetDisplayMode(this.toolStripEx14, Syncfusion.Windows.Forms.Tools.RibbonItemDisplayMode.Normal);
            this.toolStripEx14.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStripEx14.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.toolStripEx14.ForeColor = System.Drawing.Color.Black;
            this.toolStripEx14.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStripEx14.Image = null;
            this.toolStripEx14.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton57,
            this.toolStripButton1,
            this.toolStripButton8,
            this.toolStripButton6,
            this.toolStripButton2});
            this.toolStripEx14.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.toolStripEx14.Location = new System.Drawing.Point(127, 1);
            this.toolStripEx14.Name = "toolStripEx14";
            this.toolStripEx14.Office12Mode = false;
            this.toolStripEx14.Padding = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.toolStripEx14.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.toolStripEx14.Size = new System.Drawing.Size(252, 90);
            this.toolStripEx14.TabIndex = 0;
            this.toolStripEx14.Text = "Risk Nodes";
            this.toolStripEx14.ThemeName = "";
            this.toolStripEx14.ThemeStyle.BackColor = System.Drawing.Color.White;
            this.toolStripEx14.ThemeStyle.BottomToolStripBackColor = System.Drawing.Color.White;
            this.toolStripEx14.ThemeStyle.CaptionBackColor = System.Drawing.Color.White;
            // 
            // toolStripButton57
            // 
            this.toolStripButton57.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton57.Image")));
            this.toolStripButton57.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripButton57.ImageTransparentColor = System.Drawing.Color.White;
            this.toolStripButton57.Name = "toolStripButton57";
            this.toolStripButton57.Size = new System.Drawing.Size(38, 74);
            this.toolStripButton57.Text = "Actor";
            this.toolStripButton57.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolStripButton57.Click += new System.EventHandler(this.toolStripButton57_Click);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.White;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(43, 74);
            this.toolStripButton1.Text = "Attack";
            this.toolStripButton1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click_1);
            // 
            // toolStripButton8
            // 
            this.toolStripButton8.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton8.Image")));
            this.toolStripButton8.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripButton8.ImageTransparentColor = System.Drawing.Color.White;
            this.toolStripButton8.Name = "toolStripButton8";
            this.toolStripButton8.Size = new System.Drawing.Size(76, 74);
            this.toolStripButton8.Text = "Vulnerability";
            this.toolStripButton8.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolStripButton8.Click += new System.EventHandler(this.toolStripButton8_Click_1);
            // 
            // toolStripButton6
            // 
            this.toolStripButton6.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton6.Image")));
            this.toolStripButton6.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripButton6.ImageTransparentColor = System.Drawing.Color.White;
            this.toolStripButton6.Name = "toolStripButton6";
            this.toolStripButton6.Size = new System.Drawing.Size(38, 74);
            this.toolStripButton6.Text = "Asset";
            this.toolStripButton6.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolStripButton6.Click += new System.EventHandler(this.toolStripButton6_Click);
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton2.Image")));
            this.toolStripButton2.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.White;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(50, 74);
            this.toolStripButton2.Text = "Control";
            this.toolStripButton2.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolStripButton2.Click += new System.EventHandler(this.toolStripButton2_Click);
            // 
            // toolStripEx2
            // 
            this.toolStripEx2.CanOverrideStyle = true;
            this.toolStripEx2.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStripEx2.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.toolStripEx2.ForeColor = System.Drawing.Color.Black;
            this.toolStripEx2.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStripEx2.Image = null;
            this.toolStripEx2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnAddObjectiveNode,
            this.btnAddEvidenceNode,
            this.toolStripButton23,
            this.toolStripSplitButton1});
            this.toolStripEx2.Location = new System.Drawing.Point(381, 1);
            this.toolStripEx2.Name = "toolStripEx2";
            this.toolStripEx2.Office12Mode = false;
            this.toolStripEx2.Padding = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.toolStripEx2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.toolStripEx2.Size = new System.Drawing.Size(222, 90);
            this.toolStripEx2.TabIndex = 14;
            this.toolStripEx2.Text = "Compliance Nodes";
            this.toolStripEx2.ThemeName = "";
            this.toolStripEx2.ThemeStyle.BackColor = System.Drawing.Color.White;
            this.toolStripEx2.ThemeStyle.BottomToolStripBackColor = System.Drawing.Color.White;
            this.toolStripEx2.ThemeStyle.CaptionBackColor = System.Drawing.Color.White;
            // 
            // btnAddObjectiveNode
            // 
            this.btnAddObjectiveNode.Image = ((System.Drawing.Image)(resources.GetObject("btnAddObjectiveNode.Image")));
            this.btnAddObjectiveNode.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnAddObjectiveNode.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAddObjectiveNode.Name = "btnAddObjectiveNode";
            this.btnAddObjectiveNode.Size = new System.Drawing.Size(59, 74);
            this.btnAddObjectiveNode.Text = "Objective";
            this.btnAddObjectiveNode.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnAddObjectiveNode.Click += new System.EventHandler(this.btnObjectiveNode_Click);
            // 
            // btnAddEvidenceNode
            // 
            this.btnAddEvidenceNode.Image = ((System.Drawing.Image)(resources.GetObject("btnAddEvidenceNode.Image")));
            this.btnAddEvidenceNode.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnAddEvidenceNode.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAddEvidenceNode.Name = "btnAddEvidenceNode";
            this.btnAddEvidenceNode.Size = new System.Drawing.Size(56, 74);
            this.btnAddEvidenceNode.Text = "Evidence";
            this.btnAddEvidenceNode.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnAddEvidenceNode.Click += new System.EventHandler(this.toolStripButton22_Click_1);
            this.btnAddEvidenceNode.DoubleClick += new System.EventHandler(this.toolStripButton22_DoubleClick);
            // 
            // toolStripButton23
            // 
            this.toolStripButton23.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton23.Image")));
            this.toolStripButton23.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripButton23.ImageTransparentColor = System.Drawing.Color.White;
            this.toolStripButton23.Name = "toolStripButton23";
            this.toolStripButton23.Size = new System.Drawing.Size(50, 74);
            this.toolStripButton23.Text = "Control";
            this.toolStripButton23.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolStripButton23.Click += new System.EventHandler(this.toolStripButton2_Click);
            // 
            // toolStripSplitButton1
            // 
            this.toolStripSplitButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.toolStripMenuItem2});
            this.toolStripSplitButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripSplitButton1.Image")));
            this.toolStripSplitButton1.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripSplitButton1.ImageTransparentColor = System.Drawing.Color.White;
            this.toolStripSplitButton1.Name = "toolStripSplitButton1";
            this.toolStripSplitButton1.Size = new System.Drawing.Size(50, 74);
            this.toolStripSplitButton1.Text = "Asset";
            this.toolStripSplitButton1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolStripSplitButton1.Click += new System.EventHandler(this.toolStripButton3_ButtonClick);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(208, 22);
            this.toolStripMenuItem1.Text = "Add to Compound Asset";
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.CheckOnClick = true;
            this.toolStripMenuItem2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(208, 22);
            this.toolStripMenuItem2.Text = "Show Hidden Asset Edges";
            // 
            // toolStripEx6
            // 
            this.toolStripEx6.CanOverrideStyle = true;
            this.toolStripEx6.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStripEx6.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.toolStripEx6.ForeColor = System.Drawing.Color.Black;
            this.toolStripEx6.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStripEx6.Image = null;
            this.toolStripEx6.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton4,
            this.toolStripButton22});
            this.toolStripEx6.Location = new System.Drawing.Point(605, 1);
            this.toolStripEx6.Name = "toolStripEx6";
            this.toolStripEx6.Office12Mode = false;
            this.toolStripEx6.Padding = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.toolStripEx6.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.toolStripEx6.Size = new System.Drawing.Size(87, 90);
            this.toolStripEx6.TabIndex = 15;
            this.toolStripEx6.Text = "Nodes";
            this.toolStripEx6.ThemeName = "";
            this.toolStripEx6.ThemeStyle.BackColor = System.Drawing.Color.White;
            this.toolStripEx6.ThemeStyle.BottomToolStripBackColor = System.Drawing.Color.White;
            this.toolStripEx6.ThemeStyle.CaptionBackColor = System.Drawing.Color.White;
            // 
            // toolStripButton4
            // 
            this.toolStripButton4.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton4.Image")));
            this.toolStripButton4.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripButton4.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton4.Name = "toolStripButton4";
            this.toolStripButton4.Size = new System.Drawing.Size(44, 74);
            this.toolStripButton4.Text = "Group";
            this.toolStripButton4.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolStripButton4.Click += new System.EventHandler(this.toolStripButton4_Click_1);
            // 
            // toolStripButton22
            // 
            this.toolStripButton22.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton22.Image")));
            this.toolStripButton22.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripButton22.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton22.Name = "toolStripButton22";
            this.toolStripButton22.Size = new System.Drawing.Size(36, 74);
            this.toolStripButton22.Text = "Text";
            this.toolStripButton22.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolStripButton22.Click += new System.EventHandler(this.toolStripButton22_Click_3);
            // 
            // toolStripEx26
            // 
            this.toolStripEx26.AutoSize = false;
            this.toolStripEx26.CanOverrideStyle = true;
            this.toolStripEx26.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStripEx26.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.toolStripEx26.ForeColor = System.Drawing.Color.Black;
            this.toolStripEx26.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStripEx26.Image = null;
            this.toolStripEx26.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnDrawEdges});
            this.toolStripEx26.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.toolStripEx26.Location = new System.Drawing.Point(694, 1);
            this.toolStripEx26.Name = "toolStripEx26";
            this.toolStripEx26.Office12Mode = false;
            this.toolStripEx26.Padding = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.toolStripEx26.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.toolStripEx26.Size = new System.Drawing.Size(59, 90);
            this.toolStripEx26.TabIndex = 11;
            this.toolStripEx26.Text = "Edges";
            this.toolStripEx26.ThemeName = "";
            this.toolStripEx26.ThemeStyle.BackColor = System.Drawing.Color.White;
            this.toolStripEx26.ThemeStyle.BottomToolStripBackColor = System.Drawing.Color.White;
            this.toolStripEx26.ThemeStyle.CaptionBackColor = System.Drawing.Color.White;
            this.toolStripEx26.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.toolStripEx26_ItemClicked);
            // 
            // btnDrawEdges
            // 
            this.btnDrawEdges.CheckOnClick = true;
            this.btnDrawEdges.Image = ((System.Drawing.Image)(resources.GetObject("btnDrawEdges.Image")));
            this.btnDrawEdges.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnDrawEdges.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnDrawEdges.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDrawEdges.Name = "btnDrawEdges";
            this.btnDrawEdges.Size = new System.Drawing.Size(42, 74);
            this.btnDrawEdges.Text = "Draw\r\nEdges\r\n";
            this.btnDrawEdges.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnDrawEdges.Click += new System.EventHandler(this.toolStripButton6_Click_1);
            // 
            // toolStripEx35
            // 
            this.toolStripEx35.AutoSize = false;
            this.toolStripEx35.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStripEx35.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.toolStripEx35.ForeColor = System.Drawing.Color.MidnightBlue;
            this.toolStripEx35.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStripEx35.Image = null;
            this.toolStripEx35.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolBtnDeleteElement});
            this.toolStripEx35.Location = new System.Drawing.Point(755, 1);
            this.toolStripEx35.Name = "toolStripEx35";
            this.toolStripEx35.Office12Mode = false;
            this.toolStripEx35.Padding = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.toolStripEx35.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.toolStripEx35.Size = new System.Drawing.Size(57, 90);
            this.toolStripEx35.TabIndex = 16;
            // 
            // toolBtnDeleteElement
            // 
            this.toolBtnDeleteElement.Enabled = false;
            this.toolBtnDeleteElement.Image = ((System.Drawing.Image)(resources.GetObject("toolBtnDeleteElement.Image")));
            this.toolBtnDeleteElement.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolBtnDeleteElement.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolBtnDeleteElement.Name = "toolBtnDeleteElement";
            this.toolBtnDeleteElement.Size = new System.Drawing.Size(44, 74);
            this.toolBtnDeleteElement.Text = "Delete";
            this.toolBtnDeleteElement.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolBtnDeleteElement.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolBtnDeleteElement.Click += new System.EventHandler(this.toolBtnDeleteElement_Click);
            // 
            // toolStripEx16
            // 
            this.toolStripEx16.AutoSize = false;
            this.toolStripEx16.BackColor = System.Drawing.Color.White;
            this.toolStripEx16.CanOverrideStyle = true;
            this.toolStripEx16.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStripEx16.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.toolStripEx16.ForeColor = System.Drawing.Color.Black;
            this.toolStripEx16.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStripEx16.Image = null;
            this.toolStripEx16.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripEx30,
            this.toolStripButton19,
            this.toolStripSplitButton2});
            this.toolStripEx16.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.VerticalStackWithOverflow;
            this.toolStripEx16.Location = new System.Drawing.Point(814, 1);
            this.toolStripEx16.Name = "toolStripEx16";
            this.toolStripEx16.Office12Mode = false;
            this.toolStripEx16.Padding = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.toolStripEx16.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.toolStripEx16.Size = new System.Drawing.Size(129, 90);
            this.toolStripEx16.TabIndex = 7;
            this.toolStripEx16.Text = "Calculate";
            this.toolStripEx16.ThemeName = "";
            this.toolStripEx16.ThemeStyle.BackColor = System.Drawing.Color.White;
            this.toolStripEx16.ThemeStyle.BottomToolStripBackColor = System.Drawing.Color.White;
            this.toolStripEx16.ThemeStyle.CaptionBackColor = System.Drawing.Color.White;
            // 
            // toolStripEx30
            // 
            this.toolStripEx30.CausesValidation = false;
            this.toolStripEx30.ForeColor = System.Drawing.Color.Black;
            this.toolStripEx30.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cbAutoCalc});
            this.toolStripEx30.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.VerticalStackWithOverflow;
            this.toolStripEx30.Name = "toolStripEx30";
            this.toolStripEx30.Size = new System.Drawing.Size(120, 28);
            this.toolStripEx30.Text = "toolStripPanelItem6";
            this.toolStripEx30.Transparent = true;
            this.toolStripEx30.UseStandardLayout = true;
            // 
            // cbAutoCalc
            // 
            this.cbAutoCalc.Name = "cbAutoCalc";
            this.cbAutoCalc.Size = new System.Drawing.Size(115, 19);
            this.cbAutoCalc.Text = "Auto Calculate";
            this.cbAutoCalc.CheckStateChanged += new System.EventHandler(this.cbAutoCalc_CheckStateChanged);
            this.cbAutoCalc.Click += new System.EventHandler(this.cbAutoCalc_Click);
            // 
            // toolStripButton19
            // 
            this.toolStripButton19.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton19.Image")));
            this.toolStripButton19.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.toolStripButton19.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton19.Name = "toolStripButton19";
            this.toolStripButton19.Size = new System.Drawing.Size(122, 20);
            this.toolStripButton19.Text = "Calculate Once";
            this.toolStripButton19.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.toolStripButton19.ToolTipText = "Calculate Graph";
            this.toolStripButton19.Click += new System.EventHandler(this.toolStripButton19_Click_2);
            // 
            // toolStripSplitButton2
            // 
            this.toolStripSplitButton2.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem4,
            this.toolStripMenuItem5,
            this.toolStripMenuItem8,
            this.toolStripMenuItem9,
            this.toolStripMenuItem12,
            this.toolStripMenuItem13,
            this.toolStripMenuItem3,
            this.clearDistributionsToolStripMenuItem});
            this.toolStripSplitButton2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripSplitButton2.Image")));
            this.toolStripSplitButton2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.toolStripSplitButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripSplitButton2.Name = "toolStripSplitButton2";
            this.toolStripSplitButton2.Size = new System.Drawing.Size(122, 20);
            this.toolStripSplitButton2.Text = "Multiple";
            this.toolStripSplitButton2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(170, 22);
            this.toolStripMenuItem4.Tag = "100";
            this.toolStripMenuItem4.Text = "x 100";
            this.toolStripMenuItem4.Click += new System.EventHandler(this.toolStripMenuItem4_Click);
            // 
            // toolStripMenuItem5
            // 
            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
            this.toolStripMenuItem5.Size = new System.Drawing.Size(170, 22);
            this.toolStripMenuItem5.Tag = "250";
            this.toolStripMenuItem5.Text = "x 250";
            this.toolStripMenuItem5.Click += new System.EventHandler(this.toolStripMenuItem4_Click);
            // 
            // toolStripMenuItem8
            // 
            this.toolStripMenuItem8.Name = "toolStripMenuItem8";
            this.toolStripMenuItem8.Size = new System.Drawing.Size(170, 22);
            this.toolStripMenuItem8.Tag = "500";
            this.toolStripMenuItem8.Text = "x 500";
            this.toolStripMenuItem8.Click += new System.EventHandler(this.toolStripMenuItem4_Click);
            // 
            // toolStripMenuItem9
            // 
            this.toolStripMenuItem9.Name = "toolStripMenuItem9";
            this.toolStripMenuItem9.Size = new System.Drawing.Size(170, 22);
            this.toolStripMenuItem9.Tag = "1000";
            this.toolStripMenuItem9.Text = "x 1000";
            this.toolStripMenuItem9.Click += new System.EventHandler(this.toolStripMenuItem4_Click);
            // 
            // toolStripMenuItem12
            // 
            this.toolStripMenuItem12.Name = "toolStripMenuItem12";
            this.toolStripMenuItem12.Size = new System.Drawing.Size(170, 22);
            this.toolStripMenuItem12.Tag = "2000";
            this.toolStripMenuItem12.Text = "x 2000";
            this.toolStripMenuItem12.Click += new System.EventHandler(this.toolStripMenuItem4_Click);
            // 
            // toolStripMenuItem13
            // 
            this.toolStripMenuItem13.Name = "toolStripMenuItem13";
            this.toolStripMenuItem13.Size = new System.Drawing.Size(170, 22);
            this.toolStripMenuItem13.Tag = "5000";
            this.toolStripMenuItem13.Text = "x 5000";
            this.toolStripMenuItem13.Click += new System.EventHandler(this.toolStripMenuItem4_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(167, 6);
            // 
            // clearDistributionsToolStripMenuItem
            // 
            this.clearDistributionsToolStripMenuItem.Name = "clearDistributionsToolStripMenuItem";
            this.clearDistributionsToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.clearDistributionsToolStripMenuItem.Text = "Clear Distributions";
            this.clearDistributionsToolStripMenuItem.Click += new System.EventHandler(this.clearDistributionsToolStripMenuItem_Click);
            // 
            // toolStripEx11
            // 
            this.toolStripEx11.AutoSize = false;
            this.toolStripEx11.CanOverrideStyle = true;
            this.toolStripEx11.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStripEx11.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.toolStripEx11.ForeColor = System.Drawing.Color.Black;
            this.toolStripEx11.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStripEx11.Image = null;
            this.toolStripEx11.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton5});
            this.toolStripEx11.Location = new System.Drawing.Point(945, 1);
            this.toolStripEx11.Name = "toolStripEx11";
            this.toolStripEx11.Office12Mode = false;
            this.toolStripEx11.Padding = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.toolStripEx11.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.toolStripEx11.Size = new System.Drawing.Size(60, 90);
            this.toolStripEx11.TabIndex = 12;
            this.toolStripEx11.Text = "View";
            this.toolStripEx11.ThemeName = "";
            this.toolStripEx11.ThemeStyle.BackColor = System.Drawing.Color.White;
            this.toolStripEx11.ThemeStyle.BottomToolStripBackColor = System.Drawing.Color.White;
            this.toolStripEx11.ThemeStyle.CaptionBackColor = System.Drawing.Color.White;
            // 
            // toolStripButton5
            // 
            this.toolStripButton5.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton5.Image")));
            this.toolStripButton5.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.toolStripButton5.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripButton5.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton5.Name = "toolStripButton5";
            this.toolStripButton5.Size = new System.Drawing.Size(45, 74);
            this.toolStripButton5.Text = "Centre\r\nGraph";
            this.toolStripButton5.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolStripButton5.Click += new System.EventHandler(this.toolStripButton5_Click_1);
            // 
            // toolStripEx28
            // 
            this.toolStripEx28.AutoSize = false;
            this.toolStripEx28.CanOverrideStyle = true;
            this.toolStripEx28.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStripEx28.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.toolStripEx28.ForeColor = System.Drawing.Color.Black;
            this.toolStripEx28.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStripEx28.Image = null;
            this.toolStripEx28.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton18,
            this.toolBtnFind});
            this.toolStripEx28.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.VerticalStackWithOverflow;
            this.toolStripEx28.Location = new System.Drawing.Point(1007, 1);
            this.toolStripEx28.Name = "toolStripEx28";
            this.toolStripEx28.Office12Mode = false;
            this.toolStripEx28.Padding = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.toolStripEx28.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.toolStripEx28.Size = new System.Drawing.Size(72, 90);
            this.toolStripEx28.TabIndex = 13;
            this.toolStripEx28.Text = "Tools";
            this.toolStripEx28.ThemeName = "";
            this.toolStripEx28.ThemeStyle.BackColor = System.Drawing.Color.White;
            this.toolStripEx28.ThemeStyle.BottomToolStripBackColor = System.Drawing.Color.White;
            this.toolStripEx28.ThemeStyle.CaptionBackColor = System.Drawing.Color.White;
            // 
            // toolStripButton18
            // 
            this.toolStripButton18.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton18.Image")));
            this.toolStripButton18.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.toolStripButton18.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton18.Name = "toolStripButton18";
            this.toolStripButton18.Size = new System.Drawing.Size(65, 20);
            this.toolStripButton18.Text = "Pointer";
            this.toolStripButton18.Click += new System.EventHandler(this.toolStripButton18_Click_2);
            // 
            // toolBtnFind
            // 
            this.toolBtnFind.CheckOnClick = true;
            this.toolBtnFind.Image = ((System.Drawing.Image)(resources.GetObject("toolBtnFind.Image")));
            this.toolBtnFind.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.toolBtnFind.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolBtnFind.Name = "toolBtnFind";
            this.toolBtnFind.Size = new System.Drawing.Size(65, 20);
            this.toolBtnFind.Text = "Find";
            this.toolBtnFind.CheckStateChanged += new System.EventHandler(this.toolBtnFind_CheckStateChanged);
            // 
            // ribbonTabView
            // 
            this.ribbonTabView.Name = "ribbonTabView";
            // 
            // ribbonControlAdv1.ribbonPanel3
            // 
            this.ribbonTabView.Panel.Controls.Add(this.toolStripEx32);
            this.ribbonTabView.Panel.Controls.Add(this.toolStripEx22);
            this.ribbonTabView.Panel.Controls.Add(this.toolStripEx33);
            this.ribbonTabView.Panel.Controls.Add(this.toolStripEx20);
            this.ribbonTabView.Panel.Controls.Add(this.toolStripEx31);
            this.ribbonTabView.Panel.Controls.Add(this.toolStripEx34);
            this.ribbonTabView.Panel.Controls.Add(this.toolStripEx10);
            this.ribbonTabView.Panel.Controls.Add(this.toolStripEx27);
            this.ribbonTabView.Panel.Name = "ribbonPanel3";
            this.ribbonTabView.Panel.ScrollPosition = 0;
            this.ribbonTabView.Panel.TabIndex = 2;
            this.ribbonTabView.Panel.Text = "View";
            this.ribbonTabView.Position = 2;
            this.ribbonTabView.Size = new System.Drawing.Size(48, 25);
            this.ribbonTabView.Tag = "3";
            this.ribbonTabView.Text = "View";
            // 
            // toolStripEx32
            // 
            this.toolStripEx32.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStripEx32.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.toolStripEx32.ForeColor = System.Drawing.Color.MidnightBlue;
            this.toolStripEx32.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStripEx32.Image = null;
            this.toolStripEx32.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnDetail,
            this.btnDistributions,
            this.btnValues,
            this.btnNodeData,
            this.btnCompliance,
            this.btnRiskHeatMap});
            this.toolStripEx32.Location = new System.Drawing.Point(0, 1);
            this.toolStripEx32.Name = "toolStripEx32";
            this.toolStripEx32.Office12Mode = false;
            this.toolStripEx32.Padding = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.toolStripEx32.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.toolStripEx32.Size = new System.Drawing.Size(379, 85);
            this.toolStripEx32.TabIndex = 12;
            this.toolStripEx32.Text = "Detail Panels";
            // 
            // btnDetail
            // 
            this.btnDetail.CheckOnClick = true;
            this.btnDetail.Image = ((System.Drawing.Image)(resources.GetObject("btnDetail.Image")));
            this.btnDetail.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnDetail.ImageTransparentColor = System.Drawing.Color.White;
            this.btnDetail.Name = "btnDetail";
            this.btnDetail.Size = new System.Drawing.Size(41, 69);
            this.btnDetail.Text = "Node\r\nDetail";
            this.btnDetail.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnDetail.CheckStateChanged += new System.EventHandler(this.btnDetail_CheckStateChanged);
            // 
            // btnDistributions
            // 
            this.btnDistributions.CheckOnClick = true;
            this.btnDistributions.Image = ((System.Drawing.Image)(resources.GetObject("btnDistributions.Image")));
            this.btnDistributions.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnDistributions.ImageTransparentColor = System.Drawing.Color.White;
            this.btnDistributions.Name = "btnDistributions";
            this.btnDistributions.Size = new System.Drawing.Size(78, 69);
            this.btnDistributions.Text = "Set\r\nDistributions";
            this.btnDistributions.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnDistributions.CheckedChanged += new System.EventHandler(this.btnDistributions_CheckStateChanged);
            // 
            // btnValues
            // 
            this.btnValues.CheckOnClick = true;
            this.btnValues.Image = ((System.Drawing.Image)(resources.GetObject("btnValues.Image")));
            this.btnValues.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnValues.ImageTransparentColor = System.Drawing.Color.White;
            this.btnValues.Name = "btnValues";
            this.btnValues.Size = new System.Drawing.Size(78, 69);
            this.btnValues.Text = "View\r\nDistributions";
            this.btnValues.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnValues.CheckStateChanged += new System.EventHandler(this.btnValues_CheckStateChanged);
            // 
            // btnNodeData
            // 
            this.btnNodeData.CheckOnClick = true;
            this.btnNodeData.Image = ((System.Drawing.Image)(resources.GetObject("btnNodeData.Image")));
            this.btnNodeData.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnNodeData.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnNodeData.Name = "btnNodeData";
            this.btnNodeData.Size = new System.Drawing.Size(42, 69);
            this.btnNodeData.Text = "Node \r\nData";
            this.btnNodeData.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnNodeData.CheckStateChanged += new System.EventHandler(this.toolBtnRiskData_CheckStateChanged);
            // 
            // btnCompliance
            // 
            this.btnCompliance.CheckOnClick = true;
            this.btnCompliance.Image = ((System.Drawing.Image)(resources.GetObject("btnCompliance.Image")));
            this.btnCompliance.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnCompliance.ImageTransparentColor = System.Drawing.Color.White;
            this.btnCompliance.Name = "btnCompliance";
            this.btnCompliance.Size = new System.Drawing.Size(71, 69);
            this.btnCompliance.Text = "Compliance";
            this.btnCompliance.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnCompliance.CheckStateChanged += new System.EventHandler(this.btnCompliance_CheckStateChanged);
            // 
            // btnRiskHeatMap
            // 
            this.btnRiskHeatMap.CheckOnClick = true;
            this.btnRiskHeatMap.Image = ((System.Drawing.Image)(resources.GetObject("btnRiskHeatMap.Image")));
            this.btnRiskHeatMap.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnRiskHeatMap.ImageTransparentColor = System.Drawing.Color.White;
            this.btnRiskHeatMap.Name = "btnRiskHeatMap";
            this.btnRiskHeatMap.Size = new System.Drawing.Size(62, 69);
            this.btnRiskHeatMap.Text = "Asset Risk";
            this.btnRiskHeatMap.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnRiskHeatMap.CheckStateChanged += new System.EventHandler(this.btnRiskHeatMap_CheckStateChanged);
            // 
            // toolStripEx22
            // 
            this.toolStripEx22.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStripEx22.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.toolStripEx22.ForeColor = System.Drawing.Color.Black;
            this.toolStripEx22.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStripEx22.Image = null;
            this.toolStripEx22.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripDropDownButton1,
            this.toolStripPanelItem9,
            this.btnGrid,
            this.btnLabels,
            this.btnSwitch,
            this.toolStripPanelItem11,
            this.btnTippyShow,
            this.toolBtnEdgeTransparency,
            this.btnHeatMaps});
            this.toolStripEx22.Location = new System.Drawing.Point(0, 1);
            this.toolStripEx22.Name = "toolStripEx22";
            this.toolStripEx22.Office12Mode = false;
            this.toolStripEx22.Padding = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.toolStripEx22.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.toolStripEx22.Size = new System.Drawing.Size(430, 85);
            this.toolStripEx22.TabIndex = 9;
            this.toolStripEx22.Text = "View";
            this.toolStripEx22.ThemeName = "";
            // 
            // toolStripDropDownButton1
            // 
            this.toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ckShowEdges,
            this.ckShowActor,
            this.ckShowAttack,
            this.ckShowAsset,
            this.ckShowControl,
            this.ckShowEvidence,
            this.ckShowGroup,
            this.ckShowObjective,
            this.ckShowVulnerability});
            this.toolStripDropDownButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButton1.Image")));
            this.toolStripDropDownButton1.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton1.Name = "toolStripDropDownButton1";
            this.toolStripDropDownButton1.Size = new System.Drawing.Size(66, 69);
            this.toolStripDropDownButton1.Text = "Node\r\nVisibility ";
            this.toolStripDropDownButton1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // ckShowEdges
            // 
            this.ckShowEdges.Checked = true;
            this.ckShowEdges.CheckOnClick = true;
            this.ckShowEdges.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckShowEdges.Name = "ckShowEdges";
            this.ckShowEdges.Size = new System.Drawing.Size(139, 22);
            this.ckShowEdges.Text = "Edges";
            this.ckShowEdges.CheckStateChanged += new System.EventHandler(this.ckShowEdges_CheckStateChanged);
            // 
            // ckShowActor
            // 
            this.ckShowActor.Checked = true;
            this.ckShowActor.CheckOnClick = true;
            this.ckShowActor.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckShowActor.Name = "ckShowActor";
            this.ckShowActor.Size = new System.Drawing.Size(139, 22);
            this.ckShowActor.Text = "Actor";
            this.ckShowActor.CheckStateChanged += new System.EventHandler(this.ckShowActor_CheckStateChanged);
            // 
            // ckShowAttack
            // 
            this.ckShowAttack.Checked = true;
            this.ckShowAttack.CheckOnClick = true;
            this.ckShowAttack.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckShowAttack.Name = "ckShowAttack";
            this.ckShowAttack.Size = new System.Drawing.Size(139, 22);
            this.ckShowAttack.Text = "Attack";
            this.ckShowAttack.CheckStateChanged += new System.EventHandler(this.ckShowAttack_CheckStateChanged);
            // 
            // ckShowAsset
            // 
            this.ckShowAsset.Checked = true;
            this.ckShowAsset.CheckOnClick = true;
            this.ckShowAsset.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckShowAsset.Name = "ckShowAsset";
            this.ckShowAsset.Size = new System.Drawing.Size(139, 22);
            this.ckShowAsset.Text = "Asset";
            this.ckShowAsset.CheckStateChanged += new System.EventHandler(this.ckShowAsset_CheckStateChanged);
            // 
            // ckShowControl
            // 
            this.ckShowControl.Checked = true;
            this.ckShowControl.CheckOnClick = true;
            this.ckShowControl.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckShowControl.Name = "ckShowControl";
            this.ckShowControl.Size = new System.Drawing.Size(139, 22);
            this.ckShowControl.Text = "Control";
            this.ckShowControl.CheckStateChanged += new System.EventHandler(this.ckShowControl_CheckStateChanged);
            // 
            // ckShowEvidence
            // 
            this.ckShowEvidence.Checked = true;
            this.ckShowEvidence.CheckOnClick = true;
            this.ckShowEvidence.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckShowEvidence.Name = "ckShowEvidence";
            this.ckShowEvidence.Size = new System.Drawing.Size(139, 22);
            this.ckShowEvidence.Text = "Evidence";
            this.ckShowEvidence.CheckStateChanged += new System.EventHandler(this.ckShowEvidence_CheckStateChanged);
            // 
            // ckShowGroup
            // 
            this.ckShowGroup.Checked = true;
            this.ckShowGroup.CheckOnClick = true;
            this.ckShowGroup.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckShowGroup.Name = "ckShowGroup";
            this.ckShowGroup.Size = new System.Drawing.Size(139, 22);
            this.ckShowGroup.Text = "Group";
            this.ckShowGroup.CheckStateChanged += new System.EventHandler(this.ckShowGroup_CheckStateChanged);
            // 
            // ckShowObjective
            // 
            this.ckShowObjective.Checked = true;
            this.ckShowObjective.CheckOnClick = true;
            this.ckShowObjective.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckShowObjective.Name = "ckShowObjective";
            this.ckShowObjective.Size = new System.Drawing.Size(139, 22);
            this.ckShowObjective.Text = "Objective";
            this.ckShowObjective.CheckStateChanged += new System.EventHandler(this.ckShowObjective_CheckStateChanged);
            // 
            // ckShowVulnerability
            // 
            this.ckShowVulnerability.Checked = true;
            this.ckShowVulnerability.CheckOnClick = true;
            this.ckShowVulnerability.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckShowVulnerability.Name = "ckShowVulnerability";
            this.ckShowVulnerability.Size = new System.Drawing.Size(139, 22);
            this.ckShowVulnerability.Text = "Vulnerability";
            this.ckShowVulnerability.CheckStateChanged += new System.EventHandler(this.ckShowVulnerability_CheckStateChanged);
            // 
            // toolStripPanelItem9
            // 
            this.toolStripPanelItem9.CausesValidation = false;
            this.toolStripPanelItem9.ForeColor = System.Drawing.Color.MidnightBlue;
            this.toolStripPanelItem9.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.VerticalStackWithOverflow;
            this.toolStripPanelItem9.Name = "toolStripPanelItem9";
            this.toolStripPanelItem9.Size = new System.Drawing.Size(23, 72);
            this.toolStripPanelItem9.Text = "toolStripPanelItem9";
            this.toolStripPanelItem9.Transparent = true;
            this.toolStripPanelItem9.UseStandardLayout = true;
            // 
            // btnGrid
            // 
            this.btnGrid.CheckOnClick = true;
            this.btnGrid.Image = ((System.Drawing.Image)(resources.GetObject("btnGrid.Image")));
            this.btnGrid.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnGrid.ImageTransparentColor = System.Drawing.Color.White;
            this.btnGrid.Name = "btnGrid";
            this.btnGrid.Size = new System.Drawing.Size(36, 69);
            this.btnGrid.Text = "Grid";
            this.btnGrid.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnGrid.CheckStateChanged += new System.EventHandler(this.btnGrid_CheckStateChanged);
            // 
            // btnLabels
            // 
            this.btnLabels.CheckOnClick = true;
            this.btnLabels.Image = ((System.Drawing.Image)(resources.GetObject("btnLabels.Image")));
            this.btnLabels.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnLabels.ImageTransparentColor = System.Drawing.Color.White;
            this.btnLabels.Name = "btnLabels";
            this.btnLabels.Size = new System.Drawing.Size(62, 69);
            this.btnLabels.Text = "Show \r\nNode Text";
            this.btnLabels.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnLabels.CheckStateChanged += new System.EventHandler(this.btnLabels_CheckStateChanged);
            this.btnLabels.Click += new System.EventHandler(this.btnLabels_Click);
            // 
            // btnSwitch
            // 
            this.btnSwitch.CheckOnClick = true;
            this.btnSwitch.Image = ((System.Drawing.Image)(resources.GetObject("btnSwitch.Image")));
            this.btnSwitch.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnSwitch.ImageTransparentColor = System.Drawing.Color.White;
            this.btnSwitch.Name = "btnSwitch";
            this.btnSwitch.Size = new System.Drawing.Size(62, 69);
            this.btnSwitch.Text = "Show\r\nReference";
            this.btnSwitch.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnSwitch.CheckStateChanged += new System.EventHandler(this.btnSwitch_CheckStateChanged);
            this.btnSwitch.Click += new System.EventHandler(this.btnSwitch_Click);
            // 
            // toolStripPanelItem11
            // 
            this.toolStripPanelItem11.CausesValidation = false;
            this.toolStripPanelItem11.ForeColor = System.Drawing.Color.MidnightBlue;
            this.toolStripPanelItem11.Name = "toolStripPanelItem11";
            this.toolStripPanelItem11.Size = new System.Drawing.Size(23, 72);
            this.toolStripPanelItem11.Text = "Nodes Show/Hide";
            this.toolStripPanelItem11.Transparent = true;
            // 
            // btnTippyShow
            // 
            this.btnTippyShow.CheckOnClick = true;
            this.btnTippyShow.Image = ((System.Drawing.Image)(resources.GetObject("btnTippyShow.Image")));
            this.btnTippyShow.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnTippyShow.ImageTransparentColor = System.Drawing.Color.White;
            this.btnTippyShow.Name = "btnTippyShow";
            this.btnTippyShow.Size = new System.Drawing.Size(36, 69);
            this.btnTippyShow.Text = "Tips";
            this.btnTippyShow.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnTippyShow.Click += new System.EventHandler(this.btnTippyShow_Click);
            // 
            // toolBtnEdgeTransparency
            // 
            this.toolBtnEdgeTransparency.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miEdgeTrans0,
            this.miEdgeTrans25,
            this.miEdgeTrans50,
            this.miEdgeTrans75,
            this.miEdgeTrans100});
            this.toolBtnEdgeTransparency.Image = ((System.Drawing.Image)(resources.GetObject("toolBtnEdgeTransparency.Image")));
            this.toolBtnEdgeTransparency.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolBtnEdgeTransparency.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolBtnEdgeTransparency.Name = "toolBtnEdgeTransparency";
            this.toolBtnEdgeTransparency.Size = new System.Drawing.Size(90, 69);
            this.toolBtnEdgeTransparency.Text = "Edge \r\nTransparency \r\n";
            this.toolBtnEdgeTransparency.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // miEdgeTrans0
            // 
            this.miEdgeTrans0.Checked = true;
            this.miEdgeTrans0.CheckOnClick = true;
            this.miEdgeTrans0.CheckState = System.Windows.Forms.CheckState.Checked;
            this.miEdgeTrans0.Name = "miEdgeTrans0";
            this.miEdgeTrans0.Size = new System.Drawing.Size(101, 22);
            this.miEdgeTrans0.Text = "0%";
            this.miEdgeTrans0.Click += new System.EventHandler(this.miEdgeTrans0_Click);
            // 
            // miEdgeTrans25
            // 
            this.miEdgeTrans25.Name = "miEdgeTrans25";
            this.miEdgeTrans25.Size = new System.Drawing.Size(101, 22);
            this.miEdgeTrans25.Text = "25%";
            this.miEdgeTrans25.Click += new System.EventHandler(this.miEdgeTrans25_Click);
            // 
            // miEdgeTrans50
            // 
            this.miEdgeTrans50.CheckOnClick = true;
            this.miEdgeTrans50.Name = "miEdgeTrans50";
            this.miEdgeTrans50.Size = new System.Drawing.Size(101, 22);
            this.miEdgeTrans50.Text = "50%";
            this.miEdgeTrans50.Click += new System.EventHandler(this.miEdgeTrans50_Click);
            // 
            // miEdgeTrans75
            // 
            this.miEdgeTrans75.CheckOnClick = true;
            this.miEdgeTrans75.Name = "miEdgeTrans75";
            this.miEdgeTrans75.Size = new System.Drawing.Size(101, 22);
            this.miEdgeTrans75.Text = "75%";
            this.miEdgeTrans75.Click += new System.EventHandler(this.miEdgeTrans75_Click);
            // 
            // miEdgeTrans100
            // 
            this.miEdgeTrans100.CheckOnClick = true;
            this.miEdgeTrans100.Name = "miEdgeTrans100";
            this.miEdgeTrans100.Size = new System.Drawing.Size(101, 22);
            this.miEdgeTrans100.Text = "100%";
            this.miEdgeTrans100.Click += new System.EventHandler(this.miEdgeTrans100_Click);
            // 
            // btnHeatMaps
            // 
            this.btnHeatMaps.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miRiskHeatmap,
            this.miComplianceHeatmap});
            this.btnHeatMaps.Image = ((System.Drawing.Image)(resources.GetObject("btnHeatMaps.Image")));
            this.btnHeatMaps.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnHeatMaps.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnHeatMaps.Name = "btnHeatMaps";
            this.btnHeatMaps.Size = new System.Drawing.Size(71, 49);
            this.btnHeatMaps.Text = "Heatmaps";
            this.btnHeatMaps.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // miRiskHeatmap
            // 
            this.miRiskHeatmap.Name = "miRiskHeatmap";
            this.miRiskHeatmap.Size = new System.Drawing.Size(183, 22);
            this.miRiskHeatmap.Text = "Risk Heatmap";
            this.miRiskHeatmap.Click += new System.EventHandler(this.miRiskHeatmap_Click);
            // 
            // miComplianceHeatmap
            // 
            this.miComplianceHeatmap.Name = "miComplianceHeatmap";
            this.miComplianceHeatmap.Size = new System.Drawing.Size(183, 22);
            this.miComplianceHeatmap.Text = "Compliance Heatmap";
            this.miComplianceHeatmap.Click += new System.EventHandler(this.miComplianceHeatmap_Click);
            // 
            // toolStripEx33
            // 
            this.toolStripEx33.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStripEx33.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.toolStripEx33.ForeColor = System.Drawing.Color.MidnightBlue;
            this.toolStripEx33.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStripEx33.Image = null;
            this.toolStripEx33.Location = new System.Drawing.Point(0, 1);
            this.toolStripEx33.Name = "toolStripEx33";
            this.toolStripEx33.Office12Mode = false;
            this.toolStripEx33.Padding = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.toolStripEx33.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.toolStripEx33.Size = new System.Drawing.Size(104, 97);
            this.toolStripEx33.TabIndex = 13;
            this.toolStripEx33.Text = "Compliance";
            // 
            // toolStripEx20
            // 
            this.toolStripEx20.AutoSize = false;
            this.toolStripEx20.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStripEx20.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.toolStripEx20.ForeColor = System.Drawing.Color.MidnightBlue;
            this.toolStripEx20.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStripEx20.Image = null;
            this.toolStripEx20.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripPanelItem16,
            this.toolStripPanelItem17});
            this.toolStripEx20.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.VerticalStackWithOverflow;
            this.toolStripEx20.Location = new System.Drawing.Point(0, 1);
            this.toolStripEx20.Name = "toolStripEx20";
            this.toolStripEx20.Office12Mode = false;
            this.toolStripEx20.Padding = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.toolStripEx20.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.toolStripEx20.Size = new System.Drawing.Size(42, 85);
            this.toolStripEx20.TabIndex = 15;
            this.toolStripEx20.Text = "Docking Windows";
            // 
            // toolStripPanelItem16
            // 
            this.toolStripPanelItem16.CausesValidation = false;
            this.toolStripPanelItem16.ForeColor = System.Drawing.Color.MidnightBlue;
            this.toolStripPanelItem16.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolCmbLayoutList});
            this.toolStripPanelItem16.Name = "toolStripPanelItem16";
            this.toolStripPanelItem16.Size = new System.Drawing.Size(33, 31);
            this.toolStripPanelItem16.Text = "toolStripPanelItem16";
            this.toolStripPanelItem16.Transparent = true;
            this.toolStripPanelItem16.UseStandardLayout = true;
            // 
            // toolCmbLayoutList
            // 
            this.toolCmbLayoutList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.toolCmbLayoutList.Name = "toolCmbLayoutList";
            this.toolCmbLayoutList.Size = new System.Drawing.Size(121, 23);
            this.toolCmbLayoutList.SelectedIndexChanged += new System.EventHandler(this.toolCmbLayoutList_SelectedIndexChanged_1);
            // 
            // toolStripPanelItem17
            // 
            this.toolStripPanelItem17.CausesValidation = false;
            this.toolStripPanelItem17.ForeColor = System.Drawing.Color.MidnightBlue;
            this.toolStripPanelItem17.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolBtnSaveAs,
            this.toolBtnDockingSave,
            this.toolBtnDockingDelete,
            this.toolBtnDockingRename,
            this.toolBtnDockingApply});
            this.toolStripPanelItem17.Name = "toolStripPanelItem17";
            this.toolStripPanelItem17.Size = new System.Drawing.Size(33, 27);
            this.toolStripPanelItem17.Text = "toolStripPanelItem17";
            this.toolStripPanelItem17.Transparent = true;
            this.toolStripPanelItem17.UseStandardLayout = true;
            // 
            // toolBtnSaveAs
            // 
            this.toolBtnSaveAs.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolBtnSaveAs.Image = ((System.Drawing.Image)(resources.GetObject("toolBtnSaveAs.Image")));
            this.toolBtnSaveAs.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolBtnSaveAs.Name = "toolBtnSaveAs";
            this.toolBtnSaveAs.Size = new System.Drawing.Size(23, 20);
            this.toolBtnSaveAs.Text = "toolBtnSaveAs";
            this.toolBtnSaveAs.ToolTipText = "Save As";
            this.toolBtnSaveAs.Click += new System.EventHandler(this.toolBtnSaveAs_Click);
            // 
            // toolBtnDockingSave
            // 
            this.toolBtnDockingSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolBtnDockingSave.Image = ((System.Drawing.Image)(resources.GetObject("toolBtnDockingSave.Image")));
            this.toolBtnDockingSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolBtnDockingSave.Name = "toolBtnDockingSave";
            this.toolBtnDockingSave.Size = new System.Drawing.Size(23, 20);
            this.toolBtnDockingSave.Text = "toolStripButton36";
            this.toolBtnDockingSave.ToolTipText = "Save";
            this.toolBtnDockingSave.Click += new System.EventHandler(this.toolBtnDockingSave_Click);
            // 
            // toolBtnDockingDelete
            // 
            this.toolBtnDockingDelete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolBtnDockingDelete.Image = ((System.Drawing.Image)(resources.GetObject("toolBtnDockingDelete.Image")));
            this.toolBtnDockingDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolBtnDockingDelete.Name = "toolBtnDockingDelete";
            this.toolBtnDockingDelete.Size = new System.Drawing.Size(23, 20);
            this.toolBtnDockingDelete.Text = "toolStripButton37";
            this.toolBtnDockingDelete.ToolTipText = "Delete";
            this.toolBtnDockingDelete.Click += new System.EventHandler(this.toolBtnDockingDelete_Click);
            // 
            // toolBtnDockingRename
            // 
            this.toolBtnDockingRename.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolBtnDockingRename.Image = ((System.Drawing.Image)(resources.GetObject("toolBtnDockingRename.Image")));
            this.toolBtnDockingRename.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolBtnDockingRename.Name = "toolBtnDockingRename";
            this.toolBtnDockingRename.Size = new System.Drawing.Size(23, 20);
            this.toolBtnDockingRename.Text = "toolStripButton39";
            this.toolBtnDockingRename.ToolTipText = "Rename";
            this.toolBtnDockingRename.Click += new System.EventHandler(this.toolBtnDockingRename_Click);
            // 
            // toolBtnDockingApply
            // 
            this.toolBtnDockingApply.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolBtnDockingApply.Image = ((System.Drawing.Image)(resources.GetObject("toolBtnDockingApply.Image")));
            this.toolBtnDockingApply.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolBtnDockingApply.Name = "toolBtnDockingApply";
            this.toolBtnDockingApply.Size = new System.Drawing.Size(23, 20);
            this.toolBtnDockingApply.Text = "toolStripButton36";
            this.toolBtnDockingApply.ToolTipText = "Apply";
            this.toolBtnDockingApply.Click += new System.EventHandler(this.toolBtnDockingApply_Click);
            // 
            // toolStripEx31
            // 
            this.toolStripEx31.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStripEx31.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.toolStripEx31.ForeColor = System.Drawing.Color.MidnightBlue;
            this.toolStripEx31.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStripEx31.Image = null;
            this.toolStripEx31.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnRiskList});
            this.toolStripEx31.Location = new System.Drawing.Point(0, 1);
            this.toolStripEx31.Name = "toolStripEx31";
            this.toolStripEx31.Office12Mode = false;
            this.toolStripEx31.Padding = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.toolStripEx31.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.toolStripEx31.Size = new System.Drawing.Size(59, 85);
            this.toolStripEx31.TabIndex = 11;
            this.toolStripEx31.Text = "Risk";
            // 
            // btnRiskList
            // 
            this.btnRiskList.CheckOnClick = true;
            this.btnRiskList.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnRiskList.ImageTransparentColor = System.Drawing.Color.White;
            this.btnRiskList.Name = "btnRiskList";
            this.btnRiskList.Size = new System.Drawing.Size(52, 69);
            this.btnRiskList.Text = "Risk List";
            this.btnRiskList.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnRiskList.CheckStateChanged += new System.EventHandler(this.btnRiskList_CheckStateChanged);
            this.btnRiskList.Click += new System.EventHandler(this.btnRiskList_Click);
            // 
            // toolStripEx34
            // 
            this.toolStripEx34.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStripEx34.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.toolStripEx34.ForeColor = System.Drawing.Color.MidnightBlue;
            this.toolStripEx34.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStripEx34.Image = null;
            this.toolStripEx34.Location = new System.Drawing.Point(0, 1);
            this.toolStripEx34.Name = "toolStripEx34";
            this.toolStripEx34.Office12Mode = false;
            this.toolStripEx34.Padding = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.toolStripEx34.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.toolStripEx34.Size = new System.Drawing.Size(104, 97);
            this.toolStripEx34.TabIndex = 14;
            // 
            // toolStripEx10
            // 
            this.toolStripEx10.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStripEx10.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.toolStripEx10.ForeColor = System.Drawing.Color.Black;
            this.toolStripEx10.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStripEx10.Image = null;
            this.toolStripEx10.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripPanelItem5,
            this.toolStripPanelItem8});
            this.toolStripEx10.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.VerticalStackWithOverflow;
            this.toolStripEx10.Location = new System.Drawing.Point(0, 1);
            this.toolStripEx10.Name = "toolStripEx10";
            this.toolStripEx10.Office12Mode = false;
            this.toolStripEx10.Padding = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.toolStripEx10.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.toolStripEx10.Size = new System.Drawing.Size(98, 85);
            this.toolStripEx10.TabIndex = 1;
            this.toolStripEx10.Text = "Panels";
            this.toolStripEx10.ThemeName = "";
            // 
            // toolStripPanelItem5
            // 
            this.toolStripPanelItem5.CausesValidation = false;
            this.toolStripPanelItem5.ForeColor = System.Drawing.Color.MidnightBlue;
            this.toolStripPanelItem5.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.VerticalStackWithOverflow;
            this.toolStripPanelItem5.Name = "toolStripPanelItem5";
            this.toolStripPanelItem5.Size = new System.Drawing.Size(89, 23);
            this.toolStripPanelItem5.Text = "toolStripPanelItem5";
            this.toolStripPanelItem5.Transparent = true;
            this.toolStripPanelItem5.UseStandardLayout = true;
            // 
            // toolStripPanelItem8
            // 
            this.toolStripPanelItem8.CausesValidation = false;
            this.toolStripPanelItem8.ForeColor = System.Drawing.Color.MidnightBlue;
            this.toolStripPanelItem8.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cbNodePanel});
            this.toolStripPanelItem8.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.VerticalStackWithOverflow;
            this.toolStripPanelItem8.Name = "toolStripPanelItem8";
            this.toolStripPanelItem8.Size = new System.Drawing.Size(89, 28);
            this.toolStripPanelItem8.Text = "toolStripPanelItem5";
            this.toolStripPanelItem8.Transparent = true;
            this.toolStripPanelItem8.UseStandardLayout = true;
            // 
            // cbNodePanel
            // 
            this.cbNodePanel.Name = "cbNodePanel";
            this.cbNodePanel.Size = new System.Drawing.Size(84, 19);
            this.cbNodePanel.Text = "Node Panel";
            this.cbNodePanel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cbNodePanel.CheckStateChanged += new System.EventHandler(this.cbNodePanel_CheckStateChanged);
            // 
            // toolStripEx27
            // 
            this.toolStripEx27.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStripEx27.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.toolStripEx27.ForeColor = System.Drawing.Color.Black;
            this.toolStripEx27.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStripEx27.Image = null;
            this.toolStripEx27.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnHeatMap,
            this.btnNodePath});
            this.toolStripEx27.Location = new System.Drawing.Point(0, 1);
            this.toolStripEx27.Name = "toolStripEx27";
            this.toolStripEx27.Office12Mode = false;
            this.toolStripEx27.Padding = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.toolStripEx27.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.toolStripEx27.Size = new System.Drawing.Size(133, 85);
            this.toolStripEx27.TabIndex = 10;
            this.toolStripEx27.ThemeName = "";
            // 
            // btnHeatMap
            // 
            this.btnHeatMap.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnHeatMap.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnHeatMap.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnHeatMap.Name = "btnHeatMap";
            this.btnHeatMap.Size = new System.Drawing.Size(61, 69);
            this.btnHeatMap.Text = "Heat Map";
            this.btnHeatMap.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnHeatMap.Click += new System.EventHandler(this.toolStripButton6_Click_2);
            // 
            // btnNodePath
            // 
            this.btnNodePath.Enabled = false;
            this.btnNodePath.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnNodePath.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnNodePath.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnNodePath.Name = "btnNodePath";
            this.btnNodePath.Size = new System.Drawing.Size(65, 69);
            this.btnNodePath.Text = "Node Path";
            this.btnNodePath.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnNodePath.Click += new System.EventHandler(this.btnNodePath_Click);
            // 
            // ribbonTabLayout
            // 
            this.ribbonTabLayout.Name = "ribbonTabLayout";
            // 
            // ribbonControlAdv1.ribbonPanel4
            // 
            this.ribbonTabLayout.Panel.Controls.Add(this.toolStripEx8);
            this.ribbonTabLayout.Panel.Controls.Add(this.toolStripEx9);
            this.ribbonTabLayout.Panel.Name = "ribbonPanel4";
            this.ribbonTabLayout.Panel.ScrollPosition = 0;
            this.ribbonTabLayout.Panel.TabIndex = 3;
            this.ribbonTabLayout.Panel.Text = "Layout";
            this.ribbonTabLayout.Position = 3;
            this.ribbonTabLayout.Size = new System.Drawing.Size(57, 25);
            this.ribbonTabLayout.Tag = "2";
            this.ribbonTabLayout.Text = "Layout";
            // 
            // toolStripEx8
            // 
            this.toolStripEx8.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStripEx8.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.toolStripEx8.ForeColor = System.Drawing.Color.Black;
            this.toolStripEx8.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStripEx8.Image = null;
            this.toolStripEx8.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton10});
            this.toolStripEx8.Location = new System.Drawing.Point(0, 1);
            this.toolStripEx8.Name = "toolStripEx8";
            this.toolStripEx8.Office12Mode = false;
            this.toolStripEx8.Padding = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.toolStripEx8.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.toolStripEx8.Size = new System.Drawing.Size(87, 94);
            this.toolStripEx8.TabIndex = 0;
            this.toolStripEx8.ThemeName = "";
            // 
            // toolStripButton10
            // 
            this.toolStripButton10.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton10.Image")));
            this.toolStripButton10.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripButton10.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton10.Name = "toolStripButton10";
            this.toolStripButton10.Size = new System.Drawing.Size(80, 78);
            this.toolStripButton10.Text = "Center Graph";
            this.toolStripButton10.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolStripButton10.Click += new System.EventHandler(this.toolStripButton10_Click);
            // 
            // toolStripEx9
            // 
            this.toolStripEx9.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStripEx9.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.toolStripEx9.ForeColor = System.Drawing.Color.Black;
            this.toolStripEx9.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStripEx9.Image = null;
            this.toolStripEx9.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AlignToGrid_toolStripMenuItem,
            this.toolStripButton12,
            this.toolStripButton13,
            this.toolStripButton14,
            this.toolStripButton15,
            this.toolStripButton16,
            this.toolBtnAvsdfLayout,
            this.toolBtnCiseLayout,
            this.toolBtnCoseBilkentLayout,
            this.toolBtnElkLayered,
            this.toolBtnElkMrTree});
            this.toolStripEx9.Location = new System.Drawing.Point(0, 1);
            this.toolStripEx9.Name = "toolStripEx9";
            this.toolStripEx9.Office12Mode = false;
            this.toolStripEx9.Padding = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.toolStripEx9.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.toolStripEx9.Size = new System.Drawing.Size(654, 94);
            this.toolStripEx9.TabIndex = 1;
            this.toolStripEx9.ThemeName = "";
            // 
            // AlignToGrid_toolStripMenuItem
            // 
            this.AlignToGrid_toolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("AlignToGrid_toolStripMenuItem.Image")));
            this.AlignToGrid_toolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.AlignToGrid_toolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.AlignToGrid_toolStripMenuItem.Name = "AlignToGrid_toolStripMenuItem";
            this.AlignToGrid_toolStripMenuItem.Size = new System.Drawing.Size(52, 78);
            this.AlignToGrid_toolStripMenuItem.Text = "Grid";
            this.AlignToGrid_toolStripMenuItem.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.AlignToGrid_toolStripMenuItem.Click += new System.EventHandler(this.toolStripButton11_Click);
            // 
            // toolStripButton12
            // 
            this.toolStripButton12.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton12.Image")));
            this.toolStripButton12.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripButton12.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton12.Name = "toolStripButton12";
            this.toolStripButton12.Size = new System.Drawing.Size(52, 78);
            this.toolStripButton12.Text = "Circle";
            this.toolStripButton12.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolStripButton12.Click += new System.EventHandler(this.toolStripButton12_Click);
            // 
            // toolStripButton13
            // 
            this.toolStripButton13.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton13.Image")));
            this.toolStripButton13.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripButton13.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton13.Name = "toolStripButton13";
            this.toolStripButton13.Size = new System.Drawing.Size(66, 78);
            this.toolStripButton13.Text = "Concentric";
            this.toolStripButton13.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolStripButton13.Click += new System.EventHandler(this.toolStripButton13_Click);
            // 
            // toolStripButton14
            // 
            this.toolStripButton14.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton14.Image")));
            this.toolStripButton14.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripButton14.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton14.Name = "toolStripButton14";
            this.toolStripButton14.Size = new System.Drawing.Size(52, 78);
            this.toolStripButton14.Text = "Dagre";
            this.toolStripButton14.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolStripButton14.Click += new System.EventHandler(this.toolStripButton14_Click);
            // 
            // toolStripButton15
            // 
            this.toolStripButton15.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton15.Image")));
            this.toolStripButton15.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripButton15.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton15.Name = "toolStripButton15";
            this.toolStripButton15.Size = new System.Drawing.Size(54, 78);
            this.toolStripButton15.Text = "Breadth ";
            this.toolStripButton15.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolStripButton15.Click += new System.EventHandler(this.toolStripButton15_Click);
            // 
            // toolStripButton16
            // 
            this.toolStripButton16.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton16.Image")));
            this.toolStripButton16.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripButton16.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton16.Name = "toolStripButton16";
            this.toolStripButton16.Size = new System.Drawing.Size(52, 78);
            this.toolStripButton16.Text = "Cola";
            this.toolStripButton16.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolStripButton16.Click += new System.EventHandler(this.toolStripButton16_Click);
            // 
            // toolBtnAvsdfLayout
            // 
            this.toolBtnAvsdfLayout.Image = ((System.Drawing.Image)(resources.GetObject("toolBtnAvsdfLayout.Image")));
            this.toolBtnAvsdfLayout.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolBtnAvsdfLayout.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolBtnAvsdfLayout.Name = "toolBtnAvsdfLayout";
            this.toolBtnAvsdfLayout.Size = new System.Drawing.Size(52, 78);
            this.toolBtnAvsdfLayout.Text = "Avsdf";
            this.toolBtnAvsdfLayout.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolBtnAvsdfLayout.Click += new System.EventHandler(this.toolBtnAvsdfLayout_Click);
            // 
            // toolBtnCiseLayout
            // 
            this.toolBtnCiseLayout.Image = ((System.Drawing.Image)(resources.GetObject("toolBtnCiseLayout.Image")));
            this.toolBtnCiseLayout.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolBtnCiseLayout.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolBtnCiseLayout.Name = "toolBtnCiseLayout";
            this.toolBtnCiseLayout.Size = new System.Drawing.Size(52, 78);
            this.toolBtnCiseLayout.Text = "Cise";
            this.toolBtnCiseLayout.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolBtnCiseLayout.Click += new System.EventHandler(this.toolBtnCiseLayout_Click);
            // 
            // toolBtnCoseBilkentLayout
            // 
            this.toolBtnCoseBilkentLayout.Image = ((System.Drawing.Image)(resources.GetObject("toolBtnCoseBilkentLayout.Image")));
            this.toolBtnCoseBilkentLayout.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolBtnCoseBilkentLayout.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolBtnCoseBilkentLayout.Name = "toolBtnCoseBilkentLayout";
            this.toolBtnCoseBilkentLayout.Size = new System.Drawing.Size(74, 78);
            this.toolBtnCoseBilkentLayout.Text = "Cose Bilkent";
            this.toolBtnCoseBilkentLayout.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolBtnCoseBilkentLayout.Click += new System.EventHandler(this.toolBtnCoseBilkentLayout_Click);
            // 
            // toolBtnElkLayered
            // 
            this.toolBtnElkLayered.Image = ((System.Drawing.Image)(resources.GetObject("toolBtnElkLayered.Image")));
            this.toolBtnElkLayered.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolBtnElkLayered.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolBtnElkLayered.Name = "toolBtnElkLayered";
            this.toolBtnElkLayered.Size = new System.Drawing.Size(73, 78);
            this.toolBtnElkLayered.Text = "ELK(Layered)";
            this.toolBtnElkLayered.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolBtnElkLayered.Click += new System.EventHandler(this.toolBtnElkLayered_Click);
            // 
            // toolBtnElkMrTree
            // 
            this.toolBtnElkMrTree.Image = ((System.Drawing.Image)(resources.GetObject("toolBtnElkMrTree.Image")));
            this.toolBtnElkMrTree.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolBtnElkMrTree.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolBtnElkMrTree.Name = "toolBtnElkMrTree";
            this.toolBtnElkMrTree.Size = new System.Drawing.Size(68, 78);
            this.toolBtnElkMrTree.Text = "ELK(mrTree)";
            this.toolBtnElkMrTree.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolBtnElkMrTree.Click += new System.EventHandler(this.toolBtnElkMrTree_Click);
            // 
            // ribbonTabNode
            // 
            this.ribbonTabNode.Name = "ribbonTabNode";
            // 
            // ribbonControlAdv1.ribbonPanel5
            // 
            this.ribbonTabNode.Panel.Controls.Add(this.toolStripEx37);
            this.ribbonTabNode.Panel.Controls.Add(this.toolStripEx3);
            this.ribbonTabNode.Panel.Name = "ribbonPanel5";
            this.ribbonTabNode.Panel.ScrollPosition = 0;
            this.ribbonTabNode.Panel.ShowLauncher = false;
            this.ribbonTabNode.Panel.TabIndex = 4;
            this.ribbonTabNode.Panel.Text = "Node";
            this.ribbonTabNode.Position = 4;
            this.ribbonTabNode.Size = new System.Drawing.Size(51, 25);
            this.ribbonTabNode.Tag = "1";
            this.ribbonTabNode.Text = "Node";
            // 
            // toolStripEx37
            // 
            this.toolStripEx37.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStripEx37.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.toolStripEx37.ForeColor = System.Drawing.Color.MidnightBlue;
            this.toolStripEx37.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStripEx37.Image = null;
            this.toolStripEx37.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNodeManager,
            this.btnNodeRepository});
            this.toolStripEx37.Location = new System.Drawing.Point(0, 1);
            this.toolStripEx37.Name = "toolStripEx37";
            this.toolStripEx37.Office12Mode = false;
            this.toolStripEx37.Padding = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.toolStripEx37.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.toolStripEx37.Size = new System.Drawing.Size(130, 94);
            this.toolStripEx37.TabIndex = 3;
            this.toolStripEx37.Text = "Repository";
            // 
            // btnNodeManager
            // 
            this.btnNodeManager.Enabled = false;
            this.btnNodeManager.Image = ((System.Drawing.Image)(resources.GetObject("btnNodeManager.Image")));
            this.btnNodeManager.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnNodeManager.ImageTransparentColor = System.Drawing.Color.White;
            this.btnNodeManager.Name = "btnNodeManager";
            this.btnNodeManager.Size = new System.Drawing.Size(57, 78);
            this.btnNodeManager.Text = "Manager";
            this.btnNodeManager.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnNodeManager.Click += new System.EventHandler(this.toolStripButton32_Click);
            // 
            // btnNodeRepository
            // 
            this.btnNodeRepository.CheckOnClick = true;
            this.btnNodeRepository.Enabled = false;
            this.btnNodeRepository.Image = ((System.Drawing.Image)(resources.GetObject("btnNodeRepository.Image")));
            this.btnNodeRepository.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnNodeRepository.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnNodeRepository.Name = "btnNodeRepository";
            this.btnNodeRepository.Size = new System.Drawing.Size(66, 78);
            this.btnNodeRepository.Text = "Repository";
            this.btnNodeRepository.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnNodeRepository.CheckedChanged += new System.EventHandler(this.btnNodeRepository_CheckedChanged);
            // 
            // toolStripEx3
            // 
            this.toolStripEx3.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStripEx3.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.toolStripEx3.ForeColor = System.Drawing.Color.Black;
            this.toolStripEx3.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStripEx3.Image = null;
            this.toolStripEx3.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolSuggested,
            this.toolSugObjectives,
            this.toolSugControls,
            this.toolSugAttacks,
            this.toolSugGroups,
            this.toolSugActor,
            this.toolStripSeparator2,
            this.btnNodeAutoSize});
            this.toolStripEx3.Location = new System.Drawing.Point(0, 1);
            this.toolStripEx3.Name = "toolStripEx3";
            this.toolStripEx3.Office12Mode = false;
            this.toolStripEx3.Padding = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.toolStripEx3.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.toolStripEx3.ShowCaption = false;
            this.toolStripEx3.Size = new System.Drawing.Size(407, 94);
            this.toolStripEx3.TabIndex = 2;
            this.toolStripEx3.ThemeName = "";
            // 
            // toolSuggested
            // 
            this.toolSuggested.CheckOnClick = true;
            this.toolSuggested.Enabled = false;
            this.toolSuggested.Image = ((System.Drawing.Image)(resources.GetObject("toolSuggested.Image")));
            this.toolSuggested.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolSuggested.ImageTransparentColor = System.Drawing.Color.White;
            this.toolSuggested.Name = "toolSuggested";
            this.toolSuggested.Size = new System.Drawing.Size(56, 91);
            this.toolSuggested.Text = "Suggest \r\nNodes";
            this.toolSuggested.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolSuggested.Click += new System.EventHandler(this.toolSuggested_Click);
            // 
            // toolSugObjectives
            // 
            this.toolSugObjectives.Enabled = false;
            this.toolSugObjectives.Image = ((System.Drawing.Image)(resources.GetObject("toolSugObjectives.Image")));
            this.toolSugObjectives.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolSugObjectives.ImageTransparentColor = System.Drawing.Color.White;
            this.toolSugObjectives.Name = "toolSugObjectives";
            this.toolSugObjectives.Size = new System.Drawing.Size(64, 91);
            this.toolSugObjectives.Text = "Add\r\nObjectives";
            this.toolSugObjectives.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolSugObjectives.Click += new System.EventHandler(this.toolSugObjectives_Click);
            // 
            // toolSugControls
            // 
            this.toolSugControls.Enabled = false;
            this.toolSugControls.Image = ((System.Drawing.Image)(resources.GetObject("toolSugControls.Image")));
            this.toolSugControls.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolSugControls.ImageTransparentColor = System.Drawing.Color.White;
            this.toolSugControls.Name = "toolSugControls";
            this.toolSugControls.Size = new System.Drawing.Size(55, 91);
            this.toolSugControls.Text = "Add \r\nControls";
            this.toolSugControls.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolSugControls.Click += new System.EventHandler(this.toolSugControls_Click);
            // 
            // toolSugAttacks
            // 
            this.toolSugAttacks.CheckOnClick = true;
            this.toolSugAttacks.Enabled = false;
            this.toolSugAttacks.Image = ((System.Drawing.Image)(resources.GetObject("toolSugAttacks.Image")));
            this.toolSugAttacks.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolSugAttacks.ImageTransparentColor = System.Drawing.Color.White;
            this.toolSugAttacks.Name = "toolSugAttacks";
            this.toolSugAttacks.Size = new System.Drawing.Size(54, 91);
            this.toolSugAttacks.Text = "Add\r\nAttacks";
            this.toolSugAttacks.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.toolSugAttacks.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolSugAttacks.Click += new System.EventHandler(this.toolSugAttacks_Click);
            // 
            // toolSugGroups
            // 
            this.toolSugGroups.Enabled = false;
            this.toolSugGroups.Image = ((System.Drawing.Image)(resources.GetObject("toolSugGroups.Image")));
            this.toolSugGroups.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolSugGroups.ImageTransparentColor = System.Drawing.Color.White;
            this.toolSugGroups.Name = "toolSugGroups";
            this.toolSugGroups.Size = new System.Drawing.Size(53, 91);
            this.toolSugGroups.Text = "Add\r\nGroups";
            this.toolSugGroups.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.toolSugGroups.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolSugGroups.Click += new System.EventHandler(this.toolSugGroups_Click);
            // 
            // toolSugActor
            // 
            this.toolSugActor.Enabled = false;
            this.toolSugActor.Image = ((System.Drawing.Image)(resources.GetObject("toolSugActor.Image")));
            this.toolSugActor.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolSugActor.ImageTransparentColor = System.Drawing.Color.White;
            this.toolSugActor.Name = "toolSugActor";
            this.toolSugActor.Size = new System.Drawing.Size(53, 91);
            this.toolSugActor.Text = "Add\r\nActors";
            this.toolSugActor.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.toolSugActor.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 94);
            // 
            // btnNodeAutoSize
            // 
            this.btnNodeAutoSize.CheckOnClick = true;
            this.btnNodeAutoSize.Image = ((System.Drawing.Image)(resources.GetObject("btnNodeAutoSize.Image")));
            this.btnNodeAutoSize.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnNodeAutoSize.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnNodeAutoSize.Name = "btnNodeAutoSize";
            this.btnNodeAutoSize.Size = new System.Drawing.Size(59, 91);
            this.btnNodeAutoSize.Text = "Auto Size";
            this.btnNodeAutoSize.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnNodeAutoSize.CheckStateChanged += new System.EventHandler(this.btnNodeAutoSize_CheckStateChanged);
            // 
            // ribbonTabFormat
            // 
            this.ribbonTabFormat.Name = "ribbonTabFormat";
            // 
            // ribbonControlAdv1.ribbonPanel6
            // 
            this.ribbonTabFormat.Panel.Controls.Add(this.toolStripEx13);
            this.ribbonTabFormat.Panel.Controls.Add(this.toolStripEx17);
            this.ribbonTabFormat.Panel.Controls.Add(this.toolStripEx36);
            this.ribbonTabFormat.Panel.Controls.Add(this.toolStripEx18);
            this.ribbonTabFormat.Panel.Controls.Add(this.toolStripEx19);
            this.ribbonTabFormat.Panel.Name = "ribbonPanel6";
            this.ribbonTabFormat.Panel.ScrollPosition = 0;
            this.ribbonTabFormat.Panel.TabIndex = 13;
            this.ribbonTabFormat.Panel.Text = "Format";
            this.ribbonTabFormat.Position = 5;
            this.ribbonTabFormat.Size = new System.Drawing.Size(59, 25);
            this.ribbonTabFormat.Tag = "1";
            this.ribbonTabFormat.Text = "Format";
            // 
            // toolStripEx13
            // 
            this.toolStripEx13.BackColor = System.Drawing.Color.Transparent;
            this.toolStripEx13.CaptionStyle = Syncfusion.Windows.Forms.Tools.CaptionStyle.Bottom;
            this.toolStripEx13.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStripEx13.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.toolStripEx13.ForeColor = System.Drawing.Color.Black;
            this.toolStripEx13.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStripEx13.Image = null;
            this.toolStripEx13.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.toolStripEx13.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripPanelItem13});
            this.toolStripEx13.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.toolStripEx13.Location = new System.Drawing.Point(0, 1);
            this.toolStripEx13.Name = "toolStripEx13";
            this.toolStripEx13.Office12Mode = false;
            this.toolStripEx13.Padding = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.toolStripEx13.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.toolStripEx13.Size = new System.Drawing.Size(42, 94);
            this.toolStripEx13.TabIndex = 9;
            this.toolStripEx13.Text = "Graph Background";
            this.toolStripEx13.ThemeName = "Default";
            // 
            // toolStripPanelItem13
            // 
            this.toolStripPanelItem13.CausesValidation = false;
            this.toolStripPanelItem13.ForeColor = System.Drawing.Color.MidnightBlue;
            this.toolStripPanelItem13.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolBtnEnableBackgroundEdit,
            this.btnBackgroundImage,
            this.btnClearBackgroundImage,
            this.toolBtnFullSize,
            this.toolBtnBackgroundColor});
            this.toolStripPanelItem13.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.VerticalStackWithOverflow;
            this.toolStripPanelItem13.Name = "toolStripPanelItem13";
            this.toolStripPanelItem13.Size = new System.Drawing.Size(283, 73);
            this.toolStripPanelItem13.Text = "toolStripPanelItem13";
            this.toolStripPanelItem13.Transparent = true;
            // 
            // toolBtnEnableBackgroundEdit
            // 
            this.toolBtnEnableBackgroundEdit.CheckOnClick = true;
            this.toolBtnEnableBackgroundEdit.Image = ((System.Drawing.Image)(resources.GetObject("toolBtnEnableBackgroundEdit.Image")));
            this.toolBtnEnableBackgroundEdit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolBtnEnableBackgroundEdit.Name = "toolBtnEnableBackgroundEdit";
            this.toolBtnEnableBackgroundEdit.Size = new System.Drawing.Size(114, 20);
            this.toolBtnEnableBackgroundEdit.Text = "Edit Background";
            this.toolBtnEnableBackgroundEdit.ToolTipText = "Edit Background";
            this.toolBtnEnableBackgroundEdit.CheckedChanged += new System.EventHandler(this.toolBtnEnableBackgroundEdit_CheckedChanged);
            // 
            // btnBackgroundImage
            // 
            this.btnBackgroundImage.Enabled = false;
            this.btnBackgroundImage.Image = ((System.Drawing.Image)(resources.GetObject("btnBackgroundImage.Image")));
            this.btnBackgroundImage.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnBackgroundImage.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnBackgroundImage.Name = "btnBackgroundImage";
            this.btnBackgroundImage.Size = new System.Drawing.Size(143, 20);
            this.btnBackgroundImage.Text = "Add Background Item";
            this.btnBackgroundImage.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnBackgroundImage.Click += new System.EventHandler(this.btnBackgroundImage_Click);
            // 
            // btnClearBackgroundImage
            // 
            this.btnClearBackgroundImage.Enabled = false;
            this.btnClearBackgroundImage.Image = ((System.Drawing.Image)(resources.GetObject("btnClearBackgroundImage.Image")));
            this.btnClearBackgroundImage.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClearBackgroundImage.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnClearBackgroundImage.Name = "btnClearBackgroundImage";
            this.btnClearBackgroundImage.Size = new System.Drawing.Size(121, 20);
            this.btnClearBackgroundImage.Text = "Clear Background";
            this.btnClearBackgroundImage.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClearBackgroundImage.Click += new System.EventHandler(this.btnClearBackgroundImage_Click);
            // 
            // toolBtnFullSize
            // 
            this.toolBtnFullSize.Enabled = false;
            this.toolBtnFullSize.Image = ((System.Drawing.Image)(resources.GetObject("toolBtnFullSize.Image")));
            this.toolBtnFullSize.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.toolBtnFullSize.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolBtnFullSize.Name = "toolBtnFullSize";
            this.toolBtnFullSize.Size = new System.Drawing.Size(136, 20);
            this.toolBtnFullSize.Text = "Add Full Screen Item";
            // 
            // toolBtnBackgroundColor
            // 
            this.toolBtnBackgroundColor.Enabled = false;
            this.toolBtnBackgroundColor.Image = ((System.Drawing.Image)(resources.GetObject("toolBtnBackgroundColor.Image")));
            this.toolBtnBackgroundColor.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.toolBtnBackgroundColor.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolBtnBackgroundColor.Name = "toolBtnBackgroundColor";
            this.toolBtnBackgroundColor.Size = new System.Drawing.Size(123, 20);
            this.toolBtnBackgroundColor.Text = "Background Color";
            this.toolBtnBackgroundColor.Click += new System.EventHandler(this.toolBtnBackgroundColor_Click);
            // 
            // toolStripEx17
            // 
            this.toolStripEx17.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStripEx17.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.toolStripEx17.ForeColor = System.Drawing.Color.Black;
            this.toolStripEx17.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStripEx17.Image = null;
            this.toolStripEx17.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolBtnTextLeft,
            this.toolBtnLabelPosPanel,
            this.toolBtnTextRight});
            this.toolStripEx17.Location = new System.Drawing.Point(0, 1);
            this.toolStripEx17.Name = "toolStripEx17";
            this.toolStripEx17.Office12Mode = false;
            this.toolStripEx17.Padding = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.toolStripEx17.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.toolStripEx17.Size = new System.Drawing.Size(42, 94);
            this.toolStripEx17.TabIndex = 6;
            this.toolStripEx17.Text = "Label Position";
            this.toolStripEx17.ThemeName = "Default";
            // 
            // toolBtnTextLeft
            // 
            this.toolBtnTextLeft.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolBtnTextLeft.Enabled = false;
            this.toolBtnTextLeft.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolBtnTextLeft.Name = "toolBtnTextLeft";
            this.toolBtnTextLeft.Size = new System.Drawing.Size(23, 4);
            this.toolBtnTextLeft.Text = "toolStripButton38";
            this.toolBtnTextLeft.Click += new System.EventHandler(this.toolBtnTextLeft_Click);
            // 
            // toolBtnLabelPosPanel
            // 
            this.toolBtnLabelPosPanel.AutoSize = false;
            this.toolBtnLabelPosPanel.CausesValidation = false;
            this.toolBtnLabelPosPanel.Enabled = false;
            this.toolBtnLabelPosPanel.ForeColor = System.Drawing.Color.MidnightBlue;
            this.toolBtnLabelPosPanel.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolBtnTextTop,
            this.toolBtnTextCenter,
            this.toolBtnTextBottom});
            this.toolBtnLabelPosPanel.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.VerticalStackWithOverflow;
            this.toolBtnLabelPosPanel.Name = "toolBtnLabelPosPanel";
            this.toolBtnLabelPosPanel.Padding = new System.Windows.Forms.Padding(2, 8, 2, 2);
            this.toolBtnLabelPosPanel.Size = new System.Drawing.Size(27, 86);
            this.toolBtnLabelPosPanel.Text = "toolStripPanelItem3";
            this.toolBtnLabelPosPanel.Transparent = true;
            this.toolBtnLabelPosPanel.UseStandardLayout = true;
            // 
            // toolBtnTextTop
            // 
            this.toolBtnTextTop.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolBtnTextTop.Enabled = false;
            this.toolBtnTextTop.Image = ((System.Drawing.Image)(resources.GetObject("toolBtnTextTop.Image")));
            this.toolBtnTextTop.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolBtnTextTop.Name = "toolBtnTextTop";
            this.toolBtnTextTop.Size = new System.Drawing.Size(22, 20);
            this.toolBtnTextTop.Text = "toolStripButton35";
            this.toolBtnTextTop.Click += new System.EventHandler(this.toolBtnTextTop_Click);
            // 
            // toolBtnTextCenter
            // 
            this.toolBtnTextCenter.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolBtnTextCenter.Enabled = false;
            this.toolBtnTextCenter.Image = ((System.Drawing.Image)(resources.GetObject("toolBtnTextCenter.Image")));
            this.toolBtnTextCenter.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolBtnTextCenter.Name = "toolBtnTextCenter";
            this.toolBtnTextCenter.Size = new System.Drawing.Size(22, 20);
            this.toolBtnTextCenter.Text = "toolStripButton36";
            this.toolBtnTextCenter.Click += new System.EventHandler(this.toolBtnTextCenter_Click);
            // 
            // toolBtnTextBottom
            // 
            this.toolBtnTextBottom.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolBtnTextBottom.Enabled = false;
            this.toolBtnTextBottom.Image = ((System.Drawing.Image)(resources.GetObject("toolBtnTextBottom.Image")));
            this.toolBtnTextBottom.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolBtnTextBottom.Name = "toolBtnTextBottom";
            this.toolBtnTextBottom.Size = new System.Drawing.Size(22, 20);
            this.toolBtnTextBottom.Text = "toolStripButton37";
            this.toolBtnTextBottom.Click += new System.EventHandler(this.toolBtnTextBottom_Click);
            // 
            // toolBtnTextRight
            // 
            this.toolBtnTextRight.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolBtnTextRight.Enabled = false;
            this.toolBtnTextRight.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolBtnTextRight.Name = "toolBtnTextRight";
            this.toolBtnTextRight.Size = new System.Drawing.Size(23, 4);
            this.toolBtnTextRight.Text = "toolStripButton39";
            this.toolBtnTextRight.Click += new System.EventHandler(this.toolBtnTextRight_Click);
            // 
            // toolStripEx36
            // 
            this.toolStripEx36.AutoSize = false;
            this.toolStripEx36.CaptionStyle = Syncfusion.Windows.Forms.Tools.CaptionStyle.Bottom;
            this.toolStripEx36.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStripEx36.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.toolStripEx36.ForeColor = System.Drawing.Color.MidnightBlue;
            this.toolStripEx36.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStripEx36.Image = null;
            this.toolStripEx36.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolBtnNodeMinus,
            this.toolBtnNodePlus});
            this.toolStripEx36.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.VerticalStackWithOverflow;
            this.toolStripEx36.Location = new System.Drawing.Point(0, 1);
            this.toolStripEx36.Name = "toolStripEx36";
            this.toolStripEx36.Office12Mode = false;
            this.toolStripEx36.Padding = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.toolStripEx36.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.toolStripEx36.ShowCaption = false;
            this.toolStripEx36.Size = new System.Drawing.Size(42, 94);
            this.toolStripEx36.TabIndex = 11;
            this.toolStripEx36.Text = "Node Size";
            // 
            // toolBtnNodeMinus
            // 
            this.toolBtnNodeMinus.Enabled = false;
            this.toolBtnNodeMinus.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolBtnNodeMinus.Name = "toolBtnNodeMinus";
            this.toolBtnNodeMinus.Size = new System.Drawing.Size(35, 17);
            this.toolBtnNodeMinus.Text = "Reduce Size";
            this.toolBtnNodeMinus.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolBtnNodeMinus.Click += new System.EventHandler(this.toolBtnNodeMinus_Click);
            // 
            // toolBtnNodePlus
            // 
            this.toolBtnNodePlus.Enabled = false;
            this.toolBtnNodePlus.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolBtnNodePlus.Name = "toolBtnNodePlus";
            this.toolBtnNodePlus.Size = new System.Drawing.Size(35, 17);
            this.toolBtnNodePlus.Text = "Increase Size";
            this.toolBtnNodePlus.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolBtnNodePlus.Click += new System.EventHandler(this.toolBtnNodePlus_Click);
            // 
            // toolStripEx18
            // 
            this.toolStripEx18.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStripEx18.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.toolStripEx18.ForeColor = System.Drawing.Color.Black;
            this.toolStripEx18.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStripEx18.Image = null;
            this.toolStripEx18.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripPanelItem14});
            this.toolStripEx18.Location = new System.Drawing.Point(0, 1);
            this.toolStripEx18.Name = "toolStripEx18";
            this.toolStripEx18.Office12Mode = false;
            this.toolStripEx18.Padding = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.toolStripEx18.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.toolStripEx18.Size = new System.Drawing.Size(42, 94);
            this.toolStripEx18.TabIndex = 10;
            this.toolStripEx18.Text = "Node Image";
            this.toolStripEx18.ThemeName = "";
            // 
            // toolStripPanelItem14
            // 
            this.toolStripPanelItem14.CausesValidation = false;
            this.toolStripPanelItem14.ForeColor = System.Drawing.Color.MidnightBlue;
            this.toolStripPanelItem14.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolBtnSetNodeImage,
            this.toolBtnClearNodeImage});
            this.toolStripPanelItem14.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.VerticalStackWithOverflow;
            this.toolStripPanelItem14.Name = "toolStripPanelItem14";
            this.toolStripPanelItem14.Size = new System.Drawing.Size(74, 52);
            this.toolStripPanelItem14.Text = "toolStripPanelItem14";
            this.toolStripPanelItem14.Transparent = true;
            this.toolStripPanelItem14.UseStandardLayout = true;
            // 
            // toolBtnSetNodeImage
            // 
            this.toolBtnSetNodeImage.Enabled = false;
            this.toolBtnSetNodeImage.Image = ((System.Drawing.Image)(resources.GetObject("toolBtnSetNodeImage.Image")));
            this.toolBtnSetNodeImage.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolBtnSetNodeImage.Name = "toolBtnSetNodeImage";
            this.toolBtnSetNodeImage.Size = new System.Drawing.Size(69, 20);
            this.toolBtnSetNodeImage.Text = "Change";
            this.toolBtnSetNodeImage.Click += new System.EventHandler(this.toolBtnSetNodeImage_ClickAsync);
            // 
            // toolBtnClearNodeImage
            // 
            this.toolBtnClearNodeImage.Enabled = false;
            this.toolBtnClearNodeImage.Image = ((System.Drawing.Image)(resources.GetObject("toolBtnClearNodeImage.Image")));
            this.toolBtnClearNodeImage.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolBtnClearNodeImage.Name = "toolBtnClearNodeImage";
            this.toolBtnClearNodeImage.Size = new System.Drawing.Size(69, 20);
            this.toolBtnClearNodeImage.Text = "Remove";
            this.toolBtnClearNodeImage.Click += new System.EventHandler(this.toolBtnClearNodeImage_Click);
            // 
            // toolStripEx19
            // 
            this.toolStripEx19.AutoSize = false;
            this.toolStripEx19.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStripEx19.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.toolStripEx19.ForeColor = System.Drawing.Color.Black;
            this.toolStripEx19.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStripEx19.Image = null;
            this.toolStripEx19.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolBtnShapePanel,
            this.toolBtnFillPanel,
            this.toolBtnBorderPanel});
            this.toolStripEx19.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.toolStripEx19.Location = new System.Drawing.Point(0, 1);
            this.toolStripEx19.Name = "toolStripEx19";
            this.toolStripEx19.Office12Mode = false;
            this.toolStripEx19.Padding = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.toolStripEx19.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.toolStripEx19.Size = new System.Drawing.Size(42, 94);
            this.toolStripEx19.TabIndex = 5;
            this.toolStripEx19.Text = "Node Shape";
            this.toolStripEx19.ThemeName = "Default";
            // 
            // toolBtnShapePanel
            // 
            this.toolBtnShapePanel.AutoSize = false;
            this.toolBtnShapePanel.CausesValidation = false;
            this.toolBtnShapePanel.Enabled = false;
            this.toolBtnShapePanel.ForeColor = System.Drawing.Color.MidnightBlue;
            this.toolBtnShapePanel.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel3,
            this.toolComboShape});
            this.toolBtnShapePanel.Name = "toolBtnShapePanel";
            this.toolBtnShapePanel.Size = new System.Drawing.Size(180, 25);
            this.toolBtnShapePanel.Text = "toolStripPanelItem6";
            this.toolBtnShapePanel.Transparent = true;
            this.toolBtnShapePanel.UseStandardLayout = true;
            // 
            // toolStripLabel3
            // 
            this.toolStripLabel3.Enabled = false;
            this.toolStripLabel3.Name = "toolStripLabel3";
            this.toolStripLabel3.Size = new System.Drawing.Size(48, 18);
            this.toolStripLabel3.Text = "Shape   ";
            // 
            // toolComboShape
            // 
            this.toolComboShape.AutoSize = false;
            this.toolComboShape.Enabled = false;
            this.toolComboShape.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.toolComboShape.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(152)))), ((int)(((byte)(152)))), ((int)(((byte)(152)))));
            this.toolComboShape.Items.AddRange(new object[] {
            "Ellipse",
            "Triangle",
            "Round Triangle",
            "Rectangle",
            "Round Rectangle",
            "Bottom Round Rectangle",
            "Cut Rectangle",
            "Barrel",
            "Rhomboid",
            "Diamond",
            "Round Diamond",
            "Pentagon",
            "Round Pentagon",
            "Hexagon",
            "Round Hexagon",
            "Concave Hexagon",
            "Heptagon",
            "Round Heptagon",
            "Octagon",
            "Round Octagon",
            "Star",
            "Tag",
            "Round Tag",
            "Vee"});
            this.toolComboShape.MaxLength = 32767;
            this.toolComboShape.Name = "toolComboShape";
            this.toolComboShape.Size = new System.Drawing.Size(121, 15);
            this.toolComboShape.Style = Syncfusion.Windows.Forms.Tools.ToolStripExStyle.Office2016Colorful;
            this.toolComboShape.SelectedIndexChanged += new System.EventHandler(this.toolComboShape_SelectedIndexChanged);
            // 
            // toolBtnFillPanel
            // 
            this.toolBtnFillPanel.AutoSize = false;
            this.toolBtnFillPanel.CausesValidation = false;
            this.toolBtnFillPanel.Enabled = false;
            this.toolBtnFillPanel.ForeColor = System.Drawing.Color.MidnightBlue;
            this.toolBtnFillPanel.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this.toolBtnFillColor,
            this.toolTextFillColor});
            this.toolBtnFillPanel.Name = "toolBtnFillPanel";
            this.toolBtnFillPanel.Size = new System.Drawing.Size(150, 25);
            this.toolBtnFillPanel.Text = "Border";
            this.toolBtnFillPanel.Transparent = true;
            this.toolBtnFillPanel.UseStandardLayout = true;
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Enabled = false;
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(43, 18);
            this.toolStripLabel1.Text = "Fill       ";
            // 
            // toolBtnFillColor
            // 
            this.toolBtnFillColor.BackColor = System.Drawing.Color.Black;
            this.toolBtnFillColor.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("toolBtnFillColor.BackgroundImage")));
            this.toolBtnFillColor.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolBtnFillColor.Enabled = false;
            this.toolBtnFillColor.Image = ((System.Drawing.Image)(resources.GetObject("toolBtnFillColor.Image")));
            this.toolBtnFillColor.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolBtnFillColor.Name = "toolBtnFillColor";
            this.toolBtnFillColor.Size = new System.Drawing.Size(23, 18);
            this.toolBtnFillColor.Text = "toolStripButton41";
            this.toolBtnFillColor.Click += new System.EventHandler(this.toolBtnFillColor_Click);
            // 
            // toolTextFillColor
            // 
            this.toolTextFillColor.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.toolTextFillColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.toolTextFillColor.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.toolTextFillColor.Name = "toolTextFillColor";
            this.toolTextFillColor.Size = new System.Drawing.Size(100, 23);
            // 
            // toolBtnBorderPanel
            // 
            this.toolBtnBorderPanel.AutoSize = false;
            this.toolBtnBorderPanel.CausesValidation = false;
            this.toolBtnBorderPanel.Enabled = false;
            this.toolBtnBorderPanel.ForeColor = System.Drawing.Color.MidnightBlue;
            this.toolBtnBorderPanel.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel2,
            this.toolBtnBorderColor,
            this.toolStripLabel4,
            this.toolBtnBorderWidth});
            this.toolBtnBorderPanel.Name = "toolBtnBorderPanel";
            this.toolBtnBorderPanel.Size = new System.Drawing.Size(180, 25);
            this.toolBtnBorderPanel.Text = "toolStripPanelItem5";
            this.toolBtnBorderPanel.Transparent = true;
            this.toolBtnBorderPanel.UseStandardLayout = true;
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Enabled = false;
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(42, 18);
            this.toolStripLabel2.Text = "Edge   ";
            // 
            // toolBtnBorderColor
            // 
            this.toolBtnBorderColor.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolBtnBorderColor.Enabled = false;
            this.toolBtnBorderColor.Image = ((System.Drawing.Image)(resources.GetObject("toolBtnBorderColor.Image")));
            this.toolBtnBorderColor.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolBtnBorderColor.Name = "toolBtnBorderColor";
            this.toolBtnBorderColor.Size = new System.Drawing.Size(23, 18);
            this.toolBtnBorderColor.Text = "toolStripButton42";
            this.toolBtnBorderColor.ToolTipText = "toolBtnBorderColor";
            this.toolBtnBorderColor.Click += new System.EventHandler(this.toolBtnBorderColor_Click);
            // 
            // toolStripLabel4
            // 
            this.toolStripLabel4.Enabled = false;
            this.toolStripLabel4.Name = "toolStripLabel4";
            this.toolStripLabel4.Padding = new System.Windows.Forms.Padding(0, 2, 0, 0);
            this.toolStripLabel4.Size = new System.Drawing.Size(42, 18);
            this.toolStripLabel4.Text = "Width ";
            // 
            // toolBtnBorderWidth
            // 
            this.toolBtnBorderWidth.AutoSize = false;
            this.toolBtnBorderWidth.Enabled = false;
            this.toolBtnBorderWidth.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.toolBtnBorderWidth.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(152)))), ((int)(((byte)(152)))), ((int)(((byte)(152)))));
            this.toolBtnBorderWidth.Items.AddRange(new object[] {
            "0",
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "11",
            "12"});
            this.toolBtnBorderWidth.MaxLength = 32767;
            this.toolBtnBorderWidth.Name = "toolBtnBorderWidth";
            this.toolBtnBorderWidth.Size = new System.Drawing.Size(50, 85);
            this.toolBtnBorderWidth.Style = Syncfusion.Windows.Forms.Tools.ToolStripExStyle.Office2016Colorful;
            this.toolBtnBorderWidth.SelectedIndexChanged += new System.EventHandler(this.toolBtnBorderWidth_SelectedIndexChanged);
            this.toolBtnBorderWidth.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.toolBtnBorderWidth_KeyPress);
            // 
            // ribbonTabDev
            // 
            this.ribbonTabDev.Name = "ribbonTabDev";
            // 
            // ribbonControlAdv1.ribbonPanel7
            // 
            this.ribbonTabDev.Panel.Controls.Add(this.toolStripEx7);
            this.ribbonTabDev.Panel.Controls.Add(this.toolStripEx12);
            this.ribbonTabDev.Panel.Name = "ribbonPanel7";
            this.ribbonTabDev.Panel.ScrollPosition = 0;
            this.ribbonTabDev.Panel.TabIndex = 8;
            this.ribbonTabDev.Panel.Text = "Developer";
            this.ribbonTabDev.Position = 6;
            this.ribbonTabDev.Size = new System.Drawing.Size(75, 25);
            this.ribbonTabDev.Tag = "5";
            this.ribbonTabDev.Text = "Developer";
            // 
            // toolStripEx7
            // 
            this.toolStripEx7.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStripEx7.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.toolStripEx7.ForeColor = System.Drawing.Color.Black;
            this.toolStripEx7.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStripEx7.Image = null;
            this.toolStripEx7.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton7,
            this.ShowCalculationLog_toolStripButton});
            this.toolStripEx7.Location = new System.Drawing.Point(0, 1);
            this.toolStripEx7.Name = "toolStripEx7";
            this.toolStripEx7.Office12Mode = false;
            this.toolStripEx7.Padding = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.toolStripEx7.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.toolStripEx7.Size = new System.Drawing.Size(200, 90);
            this.toolStripEx7.TabIndex = 0;
            this.toolStripEx7.Text = "Debug";
            this.toolStripEx7.ThemeName = "";
            // 
            // toolStripButton7
            // 
            this.toolStripButton7.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton7.Image")));
            this.toolStripButton7.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripButton7.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton7.Name = "toolStripButton7";
            this.toolStripButton7.Size = new System.Drawing.Size(107, 74);
            this.toolStripButton7.Text = "Browser Debugger";
            this.toolStripButton7.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolStripButton7.Click += new System.EventHandler(this.toolStripButton7_Click);
            // 
            // ShowCalculationLog_toolStripButton
            // 
            this.ShowCalculationLog_toolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("ShowCalculationLog_toolStripButton.Image")));
            this.ShowCalculationLog_toolStripButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.ShowCalculationLog_toolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ShowCalculationLog_toolStripButton.Name = "ShowCalculationLog_toolStripButton";
            this.ShowCalculationLog_toolStripButton.Size = new System.Drawing.Size(86, 74);
            this.ShowCalculationLog_toolStripButton.Text = "Show Calc Log";
            this.ShowCalculationLog_toolStripButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.ShowCalculationLog_toolStripButton.CheckStateChanged += new System.EventHandler(this.ShowcalculationLog_toolStripButton_CheckedChanged);
            this.ShowCalculationLog_toolStripButton.Click += new System.EventHandler(this.ShowCalculationLog_toolStripButton_Click);
            // 
            // toolStripEx12
            // 
            this.toolStripEx12.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStripEx12.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.toolStripEx12.ForeColor = System.Drawing.Color.Black;
            this.toolStripEx12.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStripEx12.Image = null;
            this.toolStripEx12.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton9,
            this.toolStripButton17,
            this.toolStripButton21,
            this.toolStripButton20,
            this.toolBtnOpenAuditlog,
            this.toolStripButton27,
            this.toolStripButton28,
            this.toolStripButton36,
            this.toolStripButton37,
            this.toolStripButton32});
            this.toolStripEx12.Location = new System.Drawing.Point(202, 1);
            this.toolStripEx12.Name = "toolStripEx12";
            this.toolStripEx12.Office12Mode = false;
            this.toolStripEx12.Padding = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.toolStripEx12.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.toolStripEx12.Size = new System.Drawing.Size(544, 90);
            this.toolStripEx12.TabIndex = 2;
            this.toolStripEx12.ThemeName = "";
            this.toolStripEx12.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.toolStripEx12_ItemClicked);
            // 
            // toolStripButton9
            // 
            this.toolStripButton9.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton9.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton9.Image")));
            this.toolStripButton9.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton9.Name = "toolStripButton9";
            this.toolStripButton9.Size = new System.Drawing.Size(57, 74);
            this.toolStripButton9.Text = "Path Test";
            // 
            // toolStripButton17
            // 
            this.toolStripButton17.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton17.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton17.Image")));
            this.toolStripButton17.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton17.Name = "toolStripButton17";
            this.toolStripButton17.Size = new System.Drawing.Size(29, 74);
            this.toolStripButton17.Text = "N/S";
            // 
            // toolStripButton21
            // 
            this.toolStripButton21.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton21.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton21.Image")));
            this.toolStripButton21.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton21.Name = "toolStripButton21";
            this.toolStripButton21.Size = new System.Drawing.Size(29, 74);
            this.toolStripButton21.Text = "N/S";
            // 
            // toolStripButton20
            // 
            this.toolStripButton20.Checked = true;
            this.toolStripButton20.CheckState = System.Windows.Forms.CheckState.Checked;
            this.toolStripButton20.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton20.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton20.Image")));
            this.toolStripButton20.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton20.Name = "toolStripButton20";
            this.toolStripButton20.Size = new System.Drawing.Size(61, 74);
            this.toolStripButton20.Text = "Audit Log";
            this.toolStripButton20.Click += new System.EventHandler(this.toolstripeButtonAuditLog_Click);
            // 
            // toolBtnOpenAuditlog
            // 
            this.toolBtnOpenAuditlog.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolBtnOpenAuditlog.Image = ((System.Drawing.Image)(resources.GetObject("toolBtnOpenAuditlog.Image")));
            this.toolBtnOpenAuditlog.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolBtnOpenAuditlog.Name = "toolBtnOpenAuditlog";
            this.toolBtnOpenAuditlog.Size = new System.Drawing.Size(62, 74);
            this.toolBtnOpenAuditlog.Text = "Open Log";
            this.toolBtnOpenAuditlog.Click += new System.EventHandler(this.toolBtnOpenAuditlog_Click);
            // 
            // toolStripButton27
            // 
            this.toolStripButton27.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton27.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton27.Image")));
            this.toolStripButton27.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton27.Name = "toolStripButton27";
            this.toolStripButton27.Size = new System.Drawing.Size(23, 74);
            this.toolStripButton27.Text = "toolStripButton27";
            // 
            // toolStripButton28
            // 
            this.toolStripButton28.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton28.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton28.Image")));
            this.toolStripButton28.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton28.Name = "toolStripButton28";
            this.toolStripButton28.Size = new System.Drawing.Size(61, 74);
            this.toolStripButton28.Text = "Risk Form";
            this.toolStripButton28.Click += new System.EventHandler(this.toolStripButton28_Click_1);
            // 
            // toolStripButton36
            // 
            this.toolStripButton36.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton36.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton36.Image")));
            this.toolStripButton36.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton36.Name = "toolStripButton36";
            this.toolStripButton36.Size = new System.Drawing.Size(70, 74);
            this.toolStripButton36.Text = "Bubble Test";
            this.toolStripButton36.Click += new System.EventHandler(this.toolStripButton36_Click);
            // 
            // toolStripButton37
            // 
            this.toolStripButton37.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton37.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton37.Image")));
            this.toolStripButton37.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton37.Name = "toolStripButton37";
            this.toolStripButton37.Size = new System.Drawing.Size(46, 74);
            this.toolStripButton37.Text = "Report";
            this.toolStripButton37.Click += new System.EventHandler(this.toolStripButton37_Click);
            // 
            // toolStripButton32
            // 
            this.toolStripButton32.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton32.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton32.Image")));
            this.toolStripButton32.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton32.Name = "toolStripButton32";
            this.toolStripButton32.Size = new System.Drawing.Size(99, 74);
            this.toolStripButton32.Text = "GetSelectedItems";
            // 
            // ribbonTabHelp
            // 
            this.ribbonTabHelp.Name = "ribbonTabHelp";
            // 
            // ribbonControlAdv1.ribbonPanel8
            // 
            this.ribbonTabHelp.Panel.Controls.Add(this.toolStripEx21);
            this.ribbonTabHelp.Panel.Name = "ribbonPanel8";
            this.ribbonTabHelp.Panel.ScrollPosition = 0;
            this.ribbonTabHelp.Panel.TabIndex = 14;
            this.ribbonTabHelp.Panel.Text = "Help";
            this.ribbonTabHelp.Position = 7;
            this.ribbonTabHelp.Size = new System.Drawing.Size(47, 25);
            this.ribbonTabHelp.Tag = "1";
            this.ribbonTabHelp.Text = "Help";
            // 
            // toolStripEx21
            // 
            this.toolStripEx21.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStripEx21.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.toolStripEx21.ForeColor = System.Drawing.Color.MidnightBlue;
            this.toolStripEx21.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStripEx21.Image = null;
            this.toolStripEx21.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton34,
            this.toolStripButton29,
            this.toolStripButton30,
            this.toolStripButton31,
            this.toolStripButton33});
            this.toolStripEx21.Location = new System.Drawing.Point(0, 1);
            this.toolStripEx21.Name = "toolStripEx21";
            this.toolStripEx21.Office12Mode = false;
            this.toolStripEx21.Padding = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.toolStripEx21.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.toolStripEx21.Size = new System.Drawing.Size(42, 94);
            this.toolStripEx21.TabIndex = 0;
            // 
            // toolStripButton34
            // 
            this.toolStripButton34.AutoSize = false;
            this.toolStripButton34.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton34.Image")));
            this.toolStripButton34.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripButton34.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton34.Name = "toolStripButton34";
            this.toolStripButton34.Size = new System.Drawing.Size(60, 80);
            this.toolStripButton34.Text = "Help";
            this.toolStripButton34.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // toolStripButton29
            // 
            this.toolStripButton29.AutoSize = false;
            this.toolStripButton29.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton29.Image")));
            this.toolStripButton29.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripButton29.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton29.Name = "toolStripButton29";
            this.toolStripButton29.Size = new System.Drawing.Size(60, 80);
            this.toolStripButton29.Text = "Contact \r\nSupport";
            this.toolStripButton29.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // toolStripButton30
            // 
            this.toolStripButton30.AutoSize = false;
            this.toolStripButton30.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton30.Image")));
            this.toolStripButton30.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripButton30.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton30.Name = "toolStripButton30";
            this.toolStripButton30.Size = new System.Drawing.Size(60, 80);
            this.toolStripButton30.Text = "Feedback";
            this.toolStripButton30.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // toolStripButton31
            // 
            this.toolStripButton31.AutoSize = false;
            this.toolStripButton31.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton31.Image")));
            this.toolStripButton31.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripButton31.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton31.Name = "toolStripButton31";
            this.toolStripButton31.Size = new System.Drawing.Size(60, 80);
            this.toolStripButton31.Text = "What\'s \r\nNew";
            this.toolStripButton31.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // toolStripButton33
            // 
            this.toolStripButton33.AutoSize = false;
            this.toolStripButton33.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton33.Image")));
            this.toolStripButton33.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripButton33.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton33.Name = "toolStripButton33";
            this.toolStripButton33.Size = new System.Drawing.Size(60, 80);
            this.toolStripButton33.Text = "About";
            this.toolStripButton33.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // toolBtnFontPanel
            // 
            this.toolBtnFontPanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.toolBtnFontPanel.CausesValidation = false;
            this.toolBtnFontPanel.Enabled = false;
            this.toolBtnFontPanel.ForeColor = System.Drawing.Color.MidnightBlue;
            this.toolBtnFontPanel.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolBtnFontFamily,
            this.toolBtnFontSize});
            this.toolBtnFontPanel.Name = "toolBtnFontPanel";
            this.toolBtnFontPanel.Size = new System.Drawing.Size(150, 40);
            this.toolBtnFontPanel.Text = "toolStripPanelItem1";
            this.toolBtnFontPanel.Transparent = true;
            this.toolBtnFontPanel.UseStandardLayout = true;
            // 
            // toolBtnFontFamily
            // 
            this.toolBtnFontFamily.AutoSize = false;
            this.toolBtnFontFamily.Enabled = false;
            this.toolBtnFontFamily.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.toolBtnFontFamily.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(152)))), ((int)(((byte)(152)))), ((int)(((byte)(152)))));
            this.toolBtnFontFamily.MaxLength = 32767;
            this.toolBtnFontFamily.Name = "toolBtnFontFamily";
            this.toolBtnFontFamily.Size = new System.Drawing.Size(100, 36);
            this.toolBtnFontFamily.Style = Syncfusion.Windows.Forms.Tools.ToolStripExStyle.Office2016Colorful;
            this.toolBtnFontFamily.SelectedIndexChanged += new System.EventHandler(this.toolBtnFontFamily_SelectedIndexChanged);
            // 
            // toolBtnFontSize
            // 
            this.toolBtnFontSize.AutoSize = false;
            this.toolBtnFontSize.Enabled = false;
            this.toolBtnFontSize.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.toolBtnFontSize.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(152)))), ((int)(((byte)(152)))), ((int)(((byte)(152)))));
            this.toolBtnFontSize.MaxLength = 32767;
            this.toolBtnFontSize.Name = "toolBtnFontSize";
            this.toolBtnFontSize.Size = new System.Drawing.Size(40, 21);
            this.toolBtnFontSize.Style = Syncfusion.Windows.Forms.Tools.ToolStripExStyle.Office2016Colorful;
            this.toolBtnFontSize.SelectedIndexChanged += new System.EventHandler(this.toolBtnFontSize_SelectedIndexChanged);
            // 
            // toolBtnFontStylePanel
            // 
            this.toolBtnFontStylePanel.CausesValidation = false;
            this.toolBtnFontStylePanel.Enabled = false;
            this.toolBtnFontStylePanel.ForeColor = System.Drawing.Color.MidnightBlue;
            this.toolBtnFontStylePanel.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolBtnFontBold,
            this.toolBtnFontItalic,
            this.toolBtnFontColor,
            this.toolBtnFontSizePlus,
            this.toolBtnFontSizeMinus});
            this.toolBtnFontStylePanel.Name = "toolBtnFontStylePanel";
            this.toolBtnFontStylePanel.Size = new System.Drawing.Size(139, 40);
            this.toolBtnFontStylePanel.Text = "toolStripPanelItem2";
            this.toolBtnFontStylePanel.Transparent = true;
            this.toolBtnFontStylePanel.UseStandardLayout = true;
            // 
            // toolBtnFontBold
            // 
            this.toolBtnFontBold.AutoSize = false;
            this.toolBtnFontBold.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolBtnFontBold.Enabled = false;
            this.toolBtnFontBold.Image = ((System.Drawing.Image)(resources.GetObject("toolBtnFontBold.Image")));
            this.toolBtnFontBold.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolBtnFontBold.Name = "toolBtnFontBold";
            this.toolBtnFontBold.Size = new System.Drawing.Size(32, 33);
            this.toolBtnFontBold.Text = "toolStripButton50";
            this.toolBtnFontBold.Click += new System.EventHandler(this.toolBtnFontBold_Click);
            // 
            // toolBtnFontItalic
            // 
            this.toolBtnFontItalic.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolBtnFontItalic.Enabled = false;
            this.toolBtnFontItalic.Image = ((System.Drawing.Image)(resources.GetObject("toolBtnFontItalic.Image")));
            this.toolBtnFontItalic.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolBtnFontItalic.Name = "toolBtnFontItalic";
            this.toolBtnFontItalic.Size = new System.Drawing.Size(23, 33);
            this.toolBtnFontItalic.Text = "toolStripButton51";
            this.toolBtnFontItalic.Click += new System.EventHandler(this.toolBtnFontItalic_Click);
            // 
            // toolBtnFontColor
            // 
            this.toolBtnFontColor.AutoSize = false;
            this.toolBtnFontColor.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolBtnFontColor.Enabled = false;
            this.toolBtnFontColor.Image = ((System.Drawing.Image)(resources.GetObject("toolBtnFontColor.Image")));
            this.toolBtnFontColor.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolBtnFontColor.Name = "toolBtnFontColor";
            this.toolBtnFontColor.Size = new System.Drawing.Size(32, 33);
            this.toolBtnFontColor.Text = "toolStripButton40";
            this.toolBtnFontColor.Click += new System.EventHandler(this.toolBtnFontColor_Click);
            // 
            // toolBtnFontSizePlus
            // 
            this.toolBtnFontSizePlus.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolBtnFontSizePlus.Enabled = false;
            this.toolBtnFontSizePlus.Image = ((System.Drawing.Image)(resources.GetObject("toolBtnFontSizePlus.Image")));
            this.toolBtnFontSizePlus.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolBtnFontSizePlus.Name = "toolBtnFontSizePlus";
            this.toolBtnFontSizePlus.Size = new System.Drawing.Size(23, 33);
            this.toolBtnFontSizePlus.Text = "toolStripButton8";
            this.toolBtnFontSizePlus.Click += new System.EventHandler(this.toolBtnFontPlus_Click);
            // 
            // toolBtnFontSizeMinus
            // 
            this.toolBtnFontSizeMinus.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolBtnFontSizeMinus.Enabled = false;
            this.toolBtnFontSizeMinus.Image = ((System.Drawing.Image)(resources.GetObject("toolBtnFontSizeMinus.Image")));
            this.toolBtnFontSizeMinus.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolBtnFontSizeMinus.Name = "toolBtnFontSizeMinus";
            this.toolBtnFontSizeMinus.Size = new System.Drawing.Size(23, 33);
            this.toolBtnFontSizeMinus.Text = "toolStripButton9";
            this.toolBtnFontSizeMinus.Click += new System.EventHandler(this.toolBtnFontMinus_Click);
            // 
            // toolStripPanelItem10
            // 
            this.toolStripPanelItem10.AutoSize = false;
            this.toolStripPanelItem10.CausesValidation = false;
            this.toolStripPanelItem10.Enabled = false;
            this.toolStripPanelItem10.ForeColor = System.Drawing.Color.MidnightBlue;
            this.toolStripPanelItem10.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.VerticalStackWithOverflow;
            this.toolStripPanelItem10.Name = "toolStripPanelItem10";
            this.toolStripPanelItem10.Padding = new System.Windows.Forms.Padding(2, 8, 2, 2);
            this.toolStripPanelItem10.Size = new System.Drawing.Size(27, 86);
            this.toolStripPanelItem10.Text = "toolStripPanelItem3";
            this.toolStripPanelItem10.Transparent = true;
            this.toolStripPanelItem10.UseStandardLayout = true;
            // 
            // backImg
            // 
            this.backImg.BackColor = System.Drawing.Color.RosyBrown;
            this.backImg.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.backImg.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.backImg.Name = "backImg";
            this.backImg.Size = new System.Drawing.Size(54, 87);
            this.backImg.Text = "BackImg";
            this.backImg.Click += new System.EventHandler(this.backImg_Click);
            // 
            // btnDrawEdge
            // 
            this.btnDrawEdge.AutoSize = false;
            this.btnDrawEdge.CausesValidation = false;
            this.btnDrawEdge.ForeColor = System.Drawing.Color.MidnightBlue;
            this.btnDrawEdge.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.drawEdges_ToolStrip});
            this.btnDrawEdge.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.VerticalStackWithOverflow;
            this.btnDrawEdge.Name = "btnDrawEdge";
            this.btnDrawEdge.Size = new System.Drawing.Size(70, 58);
            this.btnDrawEdge.Text = "toolStripPanelItem3";
            this.btnDrawEdge.Transparent = true;
            // 
            // drawEdges_ToolStrip
            // 
            this.drawEdges_ToolStrip.CheckOnClick = true;
            this.drawEdges_ToolStrip.Image = ((System.Drawing.Image)(resources.GetObject("drawEdges_ToolStrip.Image")));
            this.drawEdges_ToolStrip.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.drawEdges_ToolStrip.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.drawEdges_ToolStrip.Name = "drawEdges_ToolStrip";
            this.drawEdges_ToolStrip.Size = new System.Drawing.Size(72, 51);
            this.drawEdges_ToolStrip.Text = "Draw Edges";
            this.drawEdges_ToolStrip.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // toolStripPanelItem2
            // 
            this.toolStripPanelItem2.CausesValidation = false;
            this.toolStripPanelItem2.ForeColor = System.Drawing.Color.MidnightBlue;
            this.toolStripPanelItem2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel5,
            this.toolStripDrawnEdgeColor,
            this.toolStripLabel6,
            this.toolStripeDrawnEdgeWidth});
            this.toolStripPanelItem2.Name = "toolStripPanelItem2";
            this.toolStripPanelItem2.Size = new System.Drawing.Size(148, 27);
            this.toolStripPanelItem2.Text = "toolStripPanelItem2";
            this.toolStripPanelItem2.Transparent = true;
            this.toolStripPanelItem2.UseStandardLayout = true;
            // 
            // toolStripLabel5
            // 
            this.toolStripLabel5.Name = "toolStripLabel5";
            this.toolStripLabel5.Size = new System.Drawing.Size(29, 20);
            this.toolStripLabel5.Text = "Line";
            // 
            // toolStripDrawnEdgeColor
            // 
            this.toolStripDrawnEdgeColor.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripDrawnEdgeColor.Enabled = false;
            this.toolStripDrawnEdgeColor.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDrawnEdgeColor.Name = "toolStripDrawnEdgeColor";
            this.toolStripDrawnEdgeColor.Size = new System.Drawing.Size(23, 20);
            this.toolStripDrawnEdgeColor.Text = "toolStripButton8";
            this.toolStripDrawnEdgeColor.Click += new System.EventHandler(this.toolStripDrawnEdgeColor_Click);
            // 
            // toolStripLabel6
            // 
            this.toolStripLabel6.Name = "toolStripLabel6";
            this.toolStripLabel6.Size = new System.Drawing.Size(39, 20);
            this.toolStripLabel6.Text = "Width";
            // 
            // toolStripeDrawnEdgeWidth
            // 
            this.toolStripeDrawnEdgeWidth.AutoSize = false;
            this.toolStripeDrawnEdgeWidth.Enabled = false;
            this.toolStripeDrawnEdgeWidth.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.toolStripeDrawnEdgeWidth.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(152)))), ((int)(((byte)(152)))), ((int)(((byte)(152)))));
            this.toolStripeDrawnEdgeWidth.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6"});
            this.toolStripeDrawnEdgeWidth.MaxLength = 32767;
            this.toolStripeDrawnEdgeWidth.Name = "toolStripeDrawnEdgeWidth";
            this.toolStripeDrawnEdgeWidth.Size = new System.Drawing.Size(50, 23);
            this.toolStripeDrawnEdgeWidth.Sorted = true;
            this.toolStripeDrawnEdgeWidth.Style = Syncfusion.Windows.Forms.Tools.ToolStripExStyle.Office2016White;
            this.toolStripeDrawnEdgeWidth.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.toolStripeDrawnEdgeWidth_KeyPress);
            this.toolStripeDrawnEdgeWidth.TextChanged += new System.EventHandler(this.toolStripeDrawnEdgeWidth_SelectedIndexChanged);
            // 
            // toolStripPanelItem1
            // 
            this.toolStripPanelItem1.AutoSize = false;
            this.toolStripPanelItem1.CausesValidation = false;
            this.toolStripPanelItem1.ForeColor = System.Drawing.Color.MidnightBlue;
            this.toolStripPanelItem1.Name = "toolStripPanelItem1";
            this.toolStripPanelItem1.Size = new System.Drawing.Size(30, 58);
            this.toolStripPanelItem1.Text = "toolStripPanelItem1";
            this.toolStripPanelItem1.Transparent = true;
            this.toolStripPanelItem1.UseStandardLayout = true;
            // 
            // label32
            // 
            this.label32.AutoSize = true;
            this.label32.Location = new System.Drawing.Point(16, 273);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(75, 13);
            this.label32.TabIndex = 16;
            this.label32.Text = "Impact Value:";
            // 
            // toolStripPanelItem7
            // 
            this.toolStripPanelItem7.AutoSize = false;
            this.toolStripPanelItem7.CausesValidation = false;
            this.toolStripPanelItem7.ForeColor = System.Drawing.Color.MidnightBlue;
            this.toolStripPanelItem7.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton44,
            this.toolStripButton45});
            this.toolStripPanelItem7.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.VerticalStackWithOverflow;
            this.toolStripPanelItem7.Name = "toolStripPanelItem7";
            this.toolStripPanelItem7.Padding = new System.Windows.Forms.Padding(2, 6, 2, 2);
            this.toolStripPanelItem7.Size = new System.Drawing.Size(120, 89);
            this.toolStripPanelItem7.Text = "toolStripPanelItem7";
            this.toolStripPanelItem7.Transparent = true;
            this.toolStripPanelItem7.UseStandardLayout = true;
            // 
            // toolStripButton44
            // 
            this.toolStripButton44.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton44.Image")));
            this.toolStripButton44.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton44.Name = "toolStripButton44";
            this.toolStripButton44.Size = new System.Drawing.Size(115, 20);
            this.toolStripButton44.Text = "Show / Hide";
            this.toolStripButton44.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.toolStripButton44.ToolTipText = "Show / Hide";
            // 
            // toolStripButton45
            // 
            this.toolStripButton45.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton45.Image")));
            this.toolStripButton45.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton45.Name = "toolStripButton45";
            this.toolStripButton45.Size = new System.Drawing.Size(115, 20);
            this.toolStripButton45.Text = "Title / Ref.";
            this.toolStripButton45.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // toolStripNodeManager
            // 
            this.toolStripNodeManager.Name = "toolStripNodeManager";
            this.toolStripNodeManager.Size = new System.Drawing.Size(23, 23);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 87);
            // 
            // toolStripButton24
            // 
            this.toolStripButton24.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripButton24.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton24.Name = "toolStripButton24";
            this.toolStripButton24.Size = new System.Drawing.Size(98, 65);
            this.toolStripButton24.Text = "Set Values to 100";
            this.toolStripButton24.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // toolStripButton25
            // 
            this.toolStripButton25.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripButton25.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton25.Name = "toolStripButton25";
            this.toolStripButton25.Size = new System.Drawing.Size(122, 65);
            this.toolStripButton25.Text = "Set Random Assessed";
            this.toolStripButton25.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // toolStripButton26
            // 
            this.toolStripButton26.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripButton26.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton26.Name = "toolStripButton26";
            this.toolStripButton26.Size = new System.Drawing.Size(117, 65);
            this.toolStripButton26.Text = "Set Default Assessed";
            this.toolStripButton26.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // NodeID_columnHeader
            // 
            this.NodeID_columnHeader.Text = "ID";
            // 
            // NodeText_columnHeader
            // 
            this.NodeText_columnHeader.Text = "Text";
            this.NodeText_columnHeader.Width = 150;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "ID";
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Text";
            this.columnHeader2.Width = 150;
            // 
            // FRLayout_toolStripMenuItem
            // 
            this.FRLayout_toolStripMenuItem.Name = "FRLayout_toolStripMenuItem";
            this.FRLayout_toolStripMenuItem.Size = new System.Drawing.Size(32, 19);
            // 
            // KKLayout_toolStripMenuItem
            // 
            this.KKLayout_toolStripMenuItem.Name = "KKLayout_toolStripMenuItem";
            this.KKLayout_toolStripMenuItem.Size = new System.Drawing.Size(32, 19);
            // 
            // ISOMLayout_toolStripMenuItem
            // 
            this.ISOMLayout_toolStripMenuItem.Name = "ISOMLayout_toolStripMenuItem";
            this.ISOMLayout_toolStripMenuItem.Size = new System.Drawing.Size(32, 19);
            // 
            // LinLogLayout_toolStripMenuItem
            // 
            this.LinLogLayout_toolStripMenuItem.Name = "LinLogLayout_toolStripMenuItem";
            this.LinLogLayout_toolStripMenuItem.Size = new System.Drawing.Size(32, 19);
            // 
            // SimpleTreeLayout_toolStripMenuItem
            // 
            this.SimpleTreeLayout_toolStripMenuItem.Name = "SimpleTreeLayout_toolStripMenuItem";
            this.SimpleTreeLayout_toolStripMenuItem.Size = new System.Drawing.Size(32, 19);
            // 
            // SimpleCircleLayout_toolStripMenuItem
            // 
            this.SimpleCircleLayout_toolStripMenuItem.Name = "SimpleCircleLayout_toolStripMenuItem";
            this.SimpleCircleLayout_toolStripMenuItem.Size = new System.Drawing.Size(32, 19);
            // 
            // SigiyamaLayout_toolStripMenuItem
            // 
            this.SigiyamaLayout_toolStripMenuItem.Name = "SigiyamaLayout_toolStripMenuItem";
            this.SigiyamaLayout_toolStripMenuItem.Size = new System.Drawing.Size(32, 19);
            // 
            // CompoundFDPLayout_toolStripMenuItem
            // 
            this.CompoundFDPLayout_toolStripMenuItem.Name = "CompoundFDPLayout_toolStripMenuItem";
            this.CompoundFDPLayout_toolStripMenuItem.Size = new System.Drawing.Size(32, 19);
            // 
            // EndAlgo_toolStripSeparator
            // 
            this.EndAlgo_toolStripSeparator.Name = "EndAlgo_toolStripSeparator";
            this.EndAlgo_toolStripSeparator.Size = new System.Drawing.Size(6, 6);
            // 
            // NoOverlap_toolStripMenuItem
            // 
            this.NoOverlap_toolStripMenuItem.Name = "NoOverlap_toolStripMenuItem";
            this.NoOverlap_toolStripMenuItem.Size = new System.Drawing.Size(32, 19);
            // 
            // FSA_toolStripMenuItem
            // 
            this.FSA_toolStripMenuItem.Name = "FSA_toolStripMenuItem";
            this.FSA_toolStripMenuItem.Size = new System.Drawing.Size(32, 19);
            // 
            // FSAOneWay_toolStripMenuItem
            // 
            this.FSAOneWay_toolStripMenuItem.Name = "FSAOneWay_toolStripMenuItem";
            this.FSAOneWay_toolStripMenuItem.Size = new System.Drawing.Size(32, 19);
            // 
            // designTimeTabTypeLoader
            // 
            this.designTimeTabTypeLoader.InvokeMemberName = "TabStyleName";
            // 
            // barItem1
            // 
            this.barItem1.BarName = "barItem1";
            this.barItem1.ID = "123";
            this.barItem1.ShowToolTipInPopUp = false;
            this.barItem1.SizeToFit = true;
            this.barItem1.Text = "123";
            // 
            // barItem2
            // 
            this.barItem2.BarName = "barItem2";
            this.barItem2.ID = "321";
            this.barItem2.ShowToolTipInPopUp = false;
            this.barItem2.SizeToFit = true;
            this.barItem2.Text = "321";
            // 
            // barItem3
            // 
            this.barItem3.BarName = "barItem3";
            this.barItem3.ID = "222";
            this.barItem3.ShowToolTipInPopUp = false;
            this.barItem3.SizeToFit = true;
            this.barItem3.Text = "222";
            // 
            // parentBarItem1
            // 
            this.parentBarItem1.BarName = "parentBarItem1";
            this.parentBarItem1.Items.AddRange(new Syncfusion.Windows.Forms.Tools.XPMenus.BarItem[] {
            this.controlBarItem,
            this.assetBarItem,
            this.groupBarItem,
            this.attackBarItem,
            this.objectiveBarItem});
            this.parentBarItem1.MetroColor = System.Drawing.Color.LightSkyBlue;
            this.parentBarItem1.ShowToolTipInPopUp = false;
            this.parentBarItem1.SizeToFit = true;
            this.parentBarItem1.WrapLength = 20;
            // 
            // controlBarItem
            // 
            this.controlBarItem.BarName = "controlBarItem";
            this.controlBarItem.Checked = true;
            this.controlBarItem.ID = "Control";
            this.controlBarItem.ShowToolTipInPopUp = false;
            this.controlBarItem.SizeToFit = true;
            this.controlBarItem.Text = "Control";
            this.controlBarItem.Click += new System.EventHandler(this.controlBarItem_Click);
            // 
            // assetBarItem
            // 
            this.assetBarItem.BarName = "assetBarItem";
            this.assetBarItem.Checked = true;
            this.assetBarItem.ID = "Asset";
            this.assetBarItem.ShowToolTipInPopUp = false;
            this.assetBarItem.SizeToFit = true;
            this.assetBarItem.Text = "Asset";
            this.assetBarItem.Click += new System.EventHandler(this.assetBarItem_Click);
            // 
            // groupBarItem
            // 
            this.groupBarItem.BarName = "groupBarItem";
            this.groupBarItem.Checked = true;
            this.groupBarItem.ID = "Group";
            this.groupBarItem.ShowToolTipInPopUp = false;
            this.groupBarItem.SizeToFit = true;
            this.groupBarItem.Text = "Group";
            this.groupBarItem.Click += new System.EventHandler(this.groupBarItem_Click);
            // 
            // attackBarItem
            // 
            this.attackBarItem.BarName = "attackBarItem";
            this.attackBarItem.Checked = true;
            this.attackBarItem.ID = "Attack";
            this.attackBarItem.ShowToolTipInPopUp = false;
            this.attackBarItem.SizeToFit = true;
            this.attackBarItem.Text = "Attack";
            this.attackBarItem.Click += new System.EventHandler(this.attackBarItem_Click);
            // 
            // objectiveBarItem
            // 
            this.objectiveBarItem.BarName = "objectiveBarItem";
            this.objectiveBarItem.Checked = true;
            this.objectiveBarItem.ID = "Objective";
            this.objectiveBarItem.ShowToolTipInPopUp = false;
            this.objectiveBarItem.SizeToFit = true;
            this.objectiveBarItem.Text = "Objective";
            this.objectiveBarItem.Click += new System.EventHandler(this.objectiveBarItem_Click);
            // 
            // popupMenusManager
            // 
            this.popupMenusManager.ParentForm = this;
            // 
            // toolStripPanelItem4
            // 
            this.toolStripPanelItem4.AutoSize = false;
            this.toolStripPanelItem4.CausesValidation = false;
            this.toolStripPanelItem4.ForeColor = System.Drawing.Color.MidnightBlue;
            this.toolStripPanelItem4.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel7,
            this.toolStripButton11,
            this.toolStripLabel8,
            this.toolStripComboBoxEx2});
            this.toolStripPanelItem4.Name = "toolStripPanelItem4";
            this.toolStripPanelItem4.Size = new System.Drawing.Size(200, 75);
            this.toolStripPanelItem4.Text = "toolStripPanelItem4";
            this.toolStripPanelItem4.Transparent = true;
            // 
            // toolStripLabel7
            // 
            this.toolStripLabel7.Name = "toolStripLabel7";
            this.toolStripLabel7.Size = new System.Drawing.Size(29, 15);
            this.toolStripLabel7.Text = "Line";
            // 
            // toolStripButton11
            // 
            this.toolStripButton11.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton11.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton11.Image")));
            this.toolStripButton11.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton11.Name = "toolStripButton11";
            this.toolStripButton11.Size = new System.Drawing.Size(23, 20);
            this.toolStripButton11.Text = "toolStripButton11";
            // 
            // toolStripLabel8
            // 
            this.toolStripLabel8.Name = "toolStripLabel8";
            this.toolStripLabel8.Size = new System.Drawing.Size(39, 15);
            this.toolStripLabel8.Text = "Width";
            // 
            // toolStripComboBoxEx2
            // 
            this.toolStripComboBoxEx2.AutoSize = false;
            this.toolStripComboBoxEx2.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.toolStripComboBoxEx2.MaxLength = 32767;
            this.toolStripComboBoxEx2.Name = "toolStripComboBoxEx2";
            this.toolStripComboBoxEx2.Size = new System.Drawing.Size(50, 21);
            // 
            // printDocument
            // 
            this.printDocument.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.printDocument_PrintPage);
            // 
            // printPreviewDialog
            // 
            this.printPreviewDialog.AutoScrollMargin = new System.Drawing.Size(0, 0);
            this.printPreviewDialog.AutoScrollMinSize = new System.Drawing.Size(0, 0);
            this.printPreviewDialog.ClientSize = new System.Drawing.Size(400, 300);
            this.printPreviewDialog.Enabled = true;
            this.printPreviewDialog.Icon = ((System.Drawing.Icon)(resources.GetObject("printPreviewDialog.Icon")));
            this.printPreviewDialog.Name = "printPreviewDialog";
            this.printPreviewDialog.Visible = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(15, 31);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(68, 13);
            this.label6.TabIndex = 0;
            this.label6.Text = "Asset Score:";
            // 
            // splashControl1
            // 
            this.splashControl1.AutoModeDisableOwner = true;
            this.splashControl1.CloseSplashForm = true;
            this.splashControl1.HideHostForm = true;
            this.splashControl1.HostForm = this;
            this.splashControl1.SplashImage = ((System.Drawing.Image)(resources.GetObject("splashControl1.SplashImage")));
            this.splashControl1.Text = "";
            this.splashControl1.TransparentColor = System.Drawing.Color.Empty;
            // 
            // panelMainBrowser
            // 
            this.panelMainBrowser.BackColor = System.Drawing.Color.Gainsboro;
            this.panelMainBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMainBrowser.Location = new System.Drawing.Point(0, 0);
            this.panelMainBrowser.Name = "panelMainBrowser";
            this.panelMainBrowser.Size = new System.Drawing.Size(118, 110);
            this.panelMainBrowser.TabIndex = 9;
            this.panelMainBrowser.Resize += new System.EventHandler(this.panelMainBrowser_Resize);
            // 
            // panel205
            // 
            this.panel205.Location = new System.Drawing.Point(0, 0);
            this.panel205.Name = "panel205";
            this.panel205.Size = new System.Drawing.Size(200, 100);
            this.panel205.TabIndex = 0;
            // 
            // panel206
            // 
            this.panel206.Location = new System.Drawing.Point(0, 0);
            this.panel206.Name = "panel206";
            this.panel206.Size = new System.Drawing.Size(200, 100);
            this.panel206.TabIndex = 0;
            // 
            // linearGauge1
            // 
            this.linearGauge1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.linearGauge1.ForeColor = System.Drawing.Color.Gray;
            this.linearGauge1.GaugelabelFont = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linearGauge1.LinearFrameType = Syncfusion.Windows.Forms.Gauge.LinearFrameType.Horizontal;
            this.linearGauge1.Location = new System.Drawing.Point(0, 0);
            this.linearGauge1.MajorTicksHeight = 15;
            this.linearGauge1.Margin = new System.Windows.Forms.Padding(0);
            this.linearGauge1.MaximumValue = 100F;
            this.linearGauge1.MinimumSize = new System.Drawing.Size(100, 50);
            this.linearGauge1.MinorTickCount = 3;
            this.linearGauge1.MinorTickHeight = 10;
            this.linearGauge1.Name = "linearGauge1";
            this.linearGauge1.PointerPlacement = Syncfusion.Windows.Forms.Gauge.Placement.Center;
            linearRange1.Color = System.Drawing.Color.Green;
            linearRange1.EndValue = 10F;
            linearRange1.Height = 5;
            linearRange1.InRange = true;
            linearRange1.Name = "GaugeRange1";
            linearRange1.RangePlacement = Syncfusion.Windows.Forms.Gauge.TickPlacement.Inside;
            linearRange1.StartValue = 0F;
            linearRange2.Color = System.Drawing.Color.GreenYellow;
            linearRange2.EndValue = 25F;
            linearRange2.Height = 5;
            linearRange2.InRange = false;
            linearRange2.Name = "GaugeRange2";
            linearRange2.RangePlacement = Syncfusion.Windows.Forms.Gauge.TickPlacement.Inside;
            linearRange2.StartValue = 10F;
            linearRange3.Color = System.Drawing.Color.Yellow;
            linearRange3.EndValue = 50F;
            linearRange3.Height = 5;
            linearRange3.InRange = false;
            linearRange3.Name = "GaugeRange3";
            linearRange3.RangePlacement = Syncfusion.Windows.Forms.Gauge.TickPlacement.Inside;
            linearRange3.StartValue = 25F;
            linearRange4.Color = System.Drawing.Color.DarkOrange;
            linearRange4.EndValue = 75F;
            linearRange4.Height = 5;
            linearRange4.InRange = false;
            linearRange4.Name = "GaugeRange4";
            linearRange4.RangePlacement = Syncfusion.Windows.Forms.Gauge.TickPlacement.Inside;
            linearRange4.StartValue = 50F;
            linearRange5.Color = System.Drawing.Color.OrangeRed;
            linearRange5.EndValue = 90F;
            linearRange5.Height = 5;
            linearRange5.InRange = false;
            linearRange5.Name = "GaugeRange5";
            linearRange5.RangePlacement = Syncfusion.Windows.Forms.Gauge.TickPlacement.Inside;
            linearRange5.StartValue = 75F;
            this.linearGauge1.Ranges.Add(linearRange1);
            this.linearGauge1.Ranges.Add(linearRange2);
            this.linearGauge1.Ranges.Add(linearRange3);
            this.linearGauge1.Ranges.Add(linearRange4);
            this.linearGauge1.Ranges.Add(linearRange5);
            this.linearGauge1.ScaleColor = System.Drawing.Color.Gray;
            this.linearGauge1.Size = new System.Drawing.Size(300, 125);
            this.linearGauge1.TabIndex = 0;
            this.linearGauge1.Text = "linearGauge2";
            this.linearGauge1.ThemeName = "";
            this.linearGauge1.ThemeStyle.BackgroundGradientEndColor = System.Drawing.Color.White;
            this.linearGauge1.ThemeStyle.BackgroundGradientStartColor = System.Drawing.Color.White;
            this.linearGauge1.ThemeStyle.ChannelHeight = 5;
            this.linearGauge1.ThemeStyle.LabelFont = new System.Drawing.Font("Microsoft Sans Serif", 7F);
            this.linearGauge1.ThemeStyle.ShowBackgroundFrame = false;
            this.linearGauge1.ThemeStyle.ValueIndicatorColor = System.Drawing.Color.Red;
            this.linearGauge1.ThemeStyle.ValueIndicatorHeight = 5;
            this.linearGauge1.ValueIndicatorColor = System.Drawing.Color.Gray;
            // 
            // panelRiskDataGrid
            // 
            this.panelRiskDataGrid.Location = new System.Drawing.Point(8, 519);
            this.panelRiskDataGrid.Name = "panelRiskDataGrid";
            this.panelRiskDataGrid.Size = new System.Drawing.Size(118, 55);
            this.panelRiskDataGrid.TabIndex = 128;
            this.panelRiskDataGrid.Visible = false;
            // 
            // statusBarAdv1
            // 
            this.statusBarAdv1.BeforeTouchSize = new System.Drawing.Size(1335, 20);
            this.statusBarAdv1.Border3DStyle = System.Windows.Forms.Border3DStyle.Flat;
            this.statusBarAdv1.BorderColor = System.Drawing.Color.WhiteSmoke;
            this.statusBarAdv1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.statusBarAdv1.Controls.Add(this.statusBarImage);
            this.statusBarAdv1.Controls.Add(this.statusBarCalcTime);
            this.statusBarAdv1.Controls.Add(this.progressBar1);
            this.statusBarAdv1.Controls.Add(this.statusBarStopCalcs);
            this.statusBarAdv1.Controls.Add(this.statusBarNodeGUID);
            this.statusBarAdv1.CustomLayoutBounds = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.statusBarAdv1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.statusBarAdv1.Location = new System.Drawing.Point(0, 755);
            this.statusBarAdv1.Name = "statusBarAdv1";
            this.statusBarAdv1.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.statusBarAdv1.Size = new System.Drawing.Size(1335, 20);
            this.statusBarAdv1.Spacing = new System.Drawing.Size(2, 2);
            this.statusBarAdv1.TabIndex = 129;
            this.statusBarAdv1.UseMetroColorAsBorder = false;
            // 
            // statusBarImage
            // 
            this.statusBarImage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.statusBarImage.BeforeTouchSize = new System.Drawing.Size(34, 14);
            this.statusBarImage.BorderSingle = System.Windows.Forms.ButtonBorderStyle.None;
            this.statusBarImage.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.statusBarImage.Location = new System.Drawing.Point(0, 2);
            this.statusBarImage.Margin = new System.Windows.Forms.Padding(0);
            this.statusBarImage.Name = "statusBarImage";
            this.statusBarImage.Size = new System.Drawing.Size(34, 14);
            this.statusBarImage.TabIndex = 2;
            this.statusBarImage.Text = null;
            // 
            // statusBarCalcTime
            // 
            this.statusBarCalcTime.BeforeTouchSize = new System.Drawing.Size(75, 14);
            this.statusBarCalcTime.Border3DStyle = System.Windows.Forms.Border3DStyle.Flat;
            this.statusBarCalcTime.BorderSingle = System.Windows.Forms.ButtonBorderStyle.None;
            this.statusBarCalcTime.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.statusBarCalcTime.Location = new System.Drawing.Point(36, 2);
            this.statusBarCalcTime.Margin = new System.Windows.Forms.Padding(0);
            this.statusBarCalcTime.Name = "statusBarCalcTime";
            this.statusBarCalcTime.Size = new System.Drawing.Size(75, 14);
            this.statusBarCalcTime.TabIndex = 0;
            this.statusBarCalcTime.Text = "{Calc msec}";
            // 
            // progressBar1
            // 
            this.progressBar1.BackgroundStyle = Syncfusion.Windows.Forms.Tools.ProgressBarBackgroundStyles.Office2016White;
            this.progressBar1.BackMultipleColors = new System.Drawing.Color[] {
        System.Drawing.Color.Empty};
            this.progressBar1.BackSegments = false;
            this.progressBar1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(147)))), ((int)(((byte)(149)))), ((int)(((byte)(152)))));
            this.progressBar1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.progressBar1.CustomText = null;
            this.progressBar1.CustomWaitingRender = false;
            this.progressBar1.ForegroundImage = null;
            this.progressBar1.GradientEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(165)))), ((int)(((byte)(220)))));
            this.progressBar1.GradientStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(165)))), ((int)(((byte)(220)))));
            this.progressBar1.Location = new System.Drawing.Point(113, 2);
            this.progressBar1.MultipleColors = new System.Drawing.Color[] {
        System.Drawing.Color.Empty};
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.ProgressStyle = Syncfusion.Windows.Forms.Tools.ProgressBarStyles.Office2016White;
            this.progressBar1.SegmentWidth = 12;
            this.progressBar1.Size = new System.Drawing.Size(176, 14);
            this.progressBar1.Step = 1;
            this.progressBar1.TabIndex = 0;
            this.progressBar1.Text = "progressBarAdv1";
            this.progressBar1.TextShadow = false;
            this.progressBar1.TextVisible = false;
            this.progressBar1.ThemeName = "Office2016White";
            this.progressBar1.Value = 0;
            this.progressBar1.Visible = false;
            this.progressBar1.WaitingGradientWidth = 400;
            // 
            // statusBarStopCalcs
            // 
            this.statusBarStopCalcs.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.statusBarStopCalcs.BeforeTouchSize = new System.Drawing.Size(27, 14);
            this.statusBarStopCalcs.BorderSingle = System.Windows.Forms.ButtonBorderStyle.None;
            this.statusBarStopCalcs.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.statusBarStopCalcs.Location = new System.Drawing.Point(291, 2);
            this.statusBarStopCalcs.Margin = new System.Windows.Forms.Padding(0);
            this.statusBarStopCalcs.Name = "statusBarStopCalcs";
            this.statusBarStopCalcs.Size = new System.Drawing.Size(27, 14);
            this.statusBarStopCalcs.TabIndex = 5;
            this.statusBarStopCalcs.Text = null;
            this.statusBarStopCalcs.Visible = false;
            this.statusBarStopCalcs.Click += new System.EventHandler(this.statusBarAdvPanel1_Click);
            // 
            // statusBarNodeGUID
            // 
            this.statusBarNodeGUID.BeforeTouchSize = new System.Drawing.Size(268, 14);
            this.statusBarNodeGUID.BorderSingle = System.Windows.Forms.ButtonBorderStyle.None;
            this.statusBarNodeGUID.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.statusBarNodeGUID.Location = new System.Drawing.Point(320, 2);
            this.statusBarNodeGUID.Margin = new System.Windows.Forms.Padding(0);
            this.statusBarNodeGUID.Name = "statusBarNodeGUID";
            this.statusBarNodeGUID.Size = new System.Drawing.Size(268, 14);
            this.statusBarNodeGUID.TabIndex = 1;
            this.statusBarNodeGUID.Text = "{Node GUID}";
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.dataGridViewTextBoxColumn1.DefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridViewTextBoxColumn1.HeaderText = "ID";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewTextBoxColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.HeaderText = "Text";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            this.dataGridViewTextBoxColumn2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // ilForm1
            // 
            this.ilForm1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ilForm1.ImageStream")));
            this.ilForm1.TransparentColor = System.Drawing.Color.Transparent;
            this.ilForm1.Images.SetKeyName(0, "Calc.png");
            this.ilForm1.Images.SetKeyName(1, "Save.png");
            // 
            // pbBusySpinner
            // 
            this.pbBusySpinner.BackColor = System.Drawing.Color.White;
            this.pbBusySpinner.ErrorImage = ((System.Drawing.Image)(resources.GetObject("pbBusySpinner.ErrorImage")));
            this.pbBusySpinner.Image = ((System.Drawing.Image)(resources.GetObject("pbBusySpinner.Image")));
            this.pbBusySpinner.Location = new System.Drawing.Point(132, 635);
            this.pbBusySpinner.Name = "pbBusySpinner";
            this.pbBusySpinner.Size = new System.Drawing.Size(87, 82);
            this.pbBusySpinner.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbBusySpinner.TabIndex = 133;
            this.pbBusySpinner.TabStop = false;
            this.pbBusySpinner.Visible = false;
            // 
            // dockingClientPanel1
            // 
            this.dockingClientPanel1.Controls.Add(this.panelMainBrowser);
            this.dockingClientPanel1.Location = new System.Drawing.Point(8, 335);
            this.dockingClientPanel1.Name = "dockingClientPanel1";
            this.dockingClientPanel1.Size = new System.Drawing.Size(118, 110);
            this.dockingClientPanel1.TabIndex = 134;
            // 
            // dockingManager1
            // 
            this.dockingManager1.AnimateAutoHiddenWindow = true;
            this.dockingManager1.AutoHideTabForeColor = System.Drawing.Color.Empty;
            this.dockingManager1.CanOverrideStyle = true;
            this.dockingManager1.CloseTabOnMiddleClick = false;
            this.dockingManager1.DockLayoutStream = ((System.IO.MemoryStream)(resources.GetObject("dockingManager1.DockLayoutStream")));
            this.dockingManager1.DragProviderStyle = Syncfusion.Windows.Forms.Tools.DragProviderStyle.Office2016Colorful;
            this.dockingManager1.HostControl = this;
            this.dockingManager1.MetroButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.dockingManager1.MetroColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(158)))), ((int)(((byte)(218)))));
            this.dockingManager1.MetroSplitterBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(159)))), ((int)(((byte)(183)))));
            this.dockingManager1.ReduceFlickeringInRtl = false;
            this.dockingManager1.ThemeName = "Office2016Colorful";
            this.dockingManager1.ThemeStyle.DockPreviewBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.dockingManager1.ThemeStyle.DockPreviewBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.dockingManager1.ThemeStyle.DockWindowStyle.ActiveCaptionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.dockingManager1.ThemeStyle.DockWindowStyle.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(210)))), ((int)(((byte)(210)))));
            this.dockingManager1.ThemeStyle.DockWindowStyle.FocusedBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(210)))), ((int)(((byte)(210)))));
            this.dockingManager1.VisualStyle = Syncfusion.Windows.Forms.VisualStyle.Office2016Colorful;
            this.dockingManager1.DockVisibilityChanged += new Syncfusion.Windows.Forms.Tools.DockVisibilityChangedEventHandler(this.dockingManager1_DockVisibilityChanged);
            this.dockingManager1.CaptionButtons.Add(new Syncfusion.Windows.Forms.Tools.CaptionButton(Syncfusion.Windows.Forms.Tools.CaptionButtonType.Close, "CloseButton"));
            this.dockingManager1.CaptionButtons.Add(new Syncfusion.Windows.Forms.Tools.CaptionButton(Syncfusion.Windows.Forms.Tools.CaptionButtonType.Pin, "PinButton"));
            this.dockingManager1.CaptionButtons.Add(new Syncfusion.Windows.Forms.Tools.CaptionButton(Syncfusion.Windows.Forms.Tools.CaptionButtonType.Maximize, "MaximizeButton"));
            this.dockingManager1.CaptionButtons.Add(new Syncfusion.Windows.Forms.Tools.CaptionButton(Syncfusion.Windows.Forms.Tools.CaptionButtonType.Restore, "RestoreButton"));
            this.dockingManager1.CaptionButtons.Add(new Syncfusion.Windows.Forms.Tools.CaptionButton(Syncfusion.Windows.Forms.Tools.CaptionButtonType.Menu, "MenuButton"));
            // 
            // panelUserLogin
            // 
            this.panelUserLogin.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panelUserLogin.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(114)))), ((int)(((byte)(198)))));
            this.panelUserLogin.Controls.Add(this.pbUserLoginStatus);
            this.panelUserLogin.Controls.Add(this.lblUserEmail);
            this.panelUserLogin.Controls.Add(this.lblUserName);
            this.panelUserLogin.Location = new System.Drawing.Point(884, 1);
            this.panelUserLogin.Name = "panelUserLogin";
            this.panelUserLogin.Size = new System.Drawing.Size(242, 61);
            this.panelUserLogin.TabIndex = 135;
            this.panelUserLogin.Click += new System.EventHandler(this.btnAccountLogin_Click);
            this.panelUserLogin.MouseEnter += new System.EventHandler(this.panel1_MouseEnter);
            this.panelUserLogin.MouseLeave += new System.EventHandler(this.panel1_MouseLeave);
            // 
            // pbUserLoginStatus
            // 
            this.pbUserLoginStatus.Image = ((System.Drawing.Image)(resources.GetObject("pbUserLoginStatus.Image")));
            this.pbUserLoginStatus.Location = new System.Drawing.Point(205, 18);
            this.pbUserLoginStatus.Name = "pbUserLoginStatus";
            this.pbUserLoginStatus.Size = new System.Drawing.Size(24, 24);
            this.pbUserLoginStatus.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbUserLoginStatus.TabIndex = 2;
            this.pbUserLoginStatus.TabStop = false;
            this.pbUserLoginStatus.Click += new System.EventHandler(this.btnAccountLogin_Click);
            this.pbUserLoginStatus.MouseEnter += new System.EventHandler(this.panel1_MouseEnter);
            this.pbUserLoginStatus.MouseLeave += new System.EventHandler(this.panel1_MouseLeave);
            // 
            // lblUserEmail
            // 
            this.lblUserEmail.AutoSize = true;
            this.lblUserEmail.ForeColor = System.Drawing.SystemColors.Control;
            this.lblUserEmail.Location = new System.Drawing.Point(3, 24);
            this.lblUserEmail.Name = "lblUserEmail";
            this.lblUserEmail.Size = new System.Drawing.Size(0, 13);
            this.lblUserEmail.TabIndex = 1;
            this.lblUserEmail.MouseEnter += new System.EventHandler(this.label2_MouseEnter);
            // 
            // lblUserName
            // 
            this.lblUserName.AutoSize = true;
            this.lblUserName.ForeColor = System.Drawing.SystemColors.Control;
            this.lblUserName.Location = new System.Drawing.Point(3, 9);
            this.lblUserName.Name = "lblUserName";
            this.lblUserName.Size = new System.Drawing.Size(0, 13);
            this.lblUserName.TabIndex = 0;
            this.lblUserName.Click += new System.EventHandler(this.btnAccountLogin_Click);
            this.lblUserName.MouseEnter += new System.EventHandler(this.label1_MouseEnter);
            // 
            // toolStripPanelItem6
            // 
            this.toolStripPanelItem6.CausesValidation = false;
            this.toolStripPanelItem6.ForeColor = System.Drawing.Color.MidnightBlue;
            this.toolStripPanelItem6.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripComboBox1});
            this.toolStripPanelItem6.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.VerticalStackWithOverflow;
            this.toolStripPanelItem6.Name = "toolStripPanelItem6";
            this.toolStripPanelItem6.Size = new System.Drawing.Size(23, 23);
            this.toolStripPanelItem6.Text = "toolStripPanelItem6";
            this.toolStripPanelItem6.Transparent = true;
            this.toolStripPanelItem6.UseStandardLayout = true;
            // 
            // toolStripComboBox1
            // 
            this.toolStripComboBox1.Name = "toolStripComboBox1";
            this.toolStripComboBox1.Size = new System.Drawing.Size(121, 23);
            // 
            // toolStripPanelItem12
            // 
            this.toolStripPanelItem12.CausesValidation = false;
            this.toolStripPanelItem12.ForeColor = System.Drawing.Color.MidnightBlue;
            this.toolStripPanelItem12.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripComboBox2});
            this.toolStripPanelItem12.Name = "toolStripPanelItem12";
            this.toolStripPanelItem12.Size = new System.Drawing.Size(23, 23);
            this.toolStripPanelItem12.Text = "toolStripPanelItem12";
            this.toolStripPanelItem12.Transparent = true;
            this.toolStripPanelItem12.UseStandardLayout = true;
            // 
            // toolStripComboBox2
            // 
            this.toolStripComboBox2.Name = "toolStripComboBox2";
            this.toolStripComboBox2.Size = new System.Drawing.Size(121, 23);
            // 
            // toolStripPanelItem15
            // 
            this.toolStripPanelItem15.CausesValidation = false;
            this.toolStripPanelItem15.ForeColor = System.Drawing.Color.MidnightBlue;
            this.toolStripPanelItem15.Name = "toolStripPanelItem15";
            this.toolStripPanelItem15.Size = new System.Drawing.Size(23, 23);
            this.toolStripPanelItem15.Text = "toolStripPanelItem15";
            this.toolStripPanelItem15.Transparent = true;
            this.toolStripPanelItem15.UseStandardLayout = true;
            // 
            // saveTimer
            // 
            this.saveTimer.Enabled = true;
            this.saveTimer.Interval = 60000;
            this.saveTimer.Tick += new System.EventHandler(this.saveTimer_Tick);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(1335, 774);
            this.Controls.Add(this.panelUserLogin);
            this.Controls.Add(this.dockingClientPanel1);
            this.Controls.Add(this.pbBusySpinner);
            this.Controls.Add(this.statusBarAdv1);
            this.Controls.Add(this.panelRiskDataGrid);
            this.Controls.Add(this.ribbonControlAdv1);
            this.DoubleBuffered = false;
            this.EnableAeroTheme = false;
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Padding = new System.Windows.Forms.Padding(1, 0, 1, 1);
            this.ShowApplicationIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.TopLeftRadius = 5;
            this.TransparencyKey = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Resize += new System.EventHandler(this.MainForm_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControlAdv1)).EndInit();
            this.ribbonControlAdv1.ResumeLayout(false);
            this.ribbonControlAdv1.PerformLayout();
            this.ribbonTabFile.Panel.ResumeLayout(false);
            this.ribbonTabFile.Panel.PerformLayout();
            this.toolStripEx23.ResumeLayout(false);
            this.toolStripEx23.PerformLayout();
            this.toolStripEx29.ResumeLayout(false);
            this.toolStripEx29.PerformLayout();
            this.toolStripEx4.ResumeLayout(false);
            this.toolStripEx4.PerformLayout();
            this.toolStripEx5.ResumeLayout(false);
            this.toolStripEx5.PerformLayout();
            this.toolStripEx24.ResumeLayout(false);
            this.toolStripEx24.PerformLayout();
            this.toolStripEx25.ResumeLayout(false);
            this.toolStripEx25.PerformLayout();
            this.ribbonTabHome.Panel.ResumeLayout(false);
            this.ribbonTabHome.Panel.PerformLayout();
            this.toolStripEx1.ResumeLayout(false);
            this.toolStripEx1.PerformLayout();
            this.toolStripEx15.ResumeLayout(false);
            this.toolStripEx15.PerformLayout();
            this.toolStripEx14.ResumeLayout(false);
            this.toolStripEx14.PerformLayout();
            this.toolStripEx2.ResumeLayout(false);
            this.toolStripEx2.PerformLayout();
            this.toolStripEx6.ResumeLayout(false);
            this.toolStripEx6.PerformLayout();
            this.toolStripEx26.ResumeLayout(false);
            this.toolStripEx26.PerformLayout();
            this.toolStripEx35.ResumeLayout(false);
            this.toolStripEx35.PerformLayout();
            this.toolStripEx16.ResumeLayout(false);
            this.toolStripEx16.PerformLayout();
            this.toolStripEx11.ResumeLayout(false);
            this.toolStripEx11.PerformLayout();
            this.toolStripEx28.ResumeLayout(false);
            this.toolStripEx28.PerformLayout();
            this.ribbonTabView.Panel.ResumeLayout(false);
            this.ribbonTabView.Panel.PerformLayout();
            this.toolStripEx32.ResumeLayout(false);
            this.toolStripEx32.PerformLayout();
            this.toolStripEx22.ResumeLayout(false);
            this.toolStripEx22.PerformLayout();
            this.toolStripEx20.ResumeLayout(false);
            this.toolStripEx20.PerformLayout();
            this.toolStripEx31.ResumeLayout(false);
            this.toolStripEx31.PerformLayout();
            this.toolStripEx10.ResumeLayout(false);
            this.toolStripEx10.PerformLayout();
            this.toolStripEx27.ResumeLayout(false);
            this.toolStripEx27.PerformLayout();
            this.ribbonTabLayout.Panel.ResumeLayout(false);
            this.ribbonTabLayout.Panel.PerformLayout();
            this.toolStripEx8.ResumeLayout(false);
            this.toolStripEx8.PerformLayout();
            this.toolStripEx9.ResumeLayout(false);
            this.toolStripEx9.PerformLayout();
            this.ribbonTabNode.Panel.ResumeLayout(false);
            this.ribbonTabNode.Panel.PerformLayout();
            this.toolStripEx37.ResumeLayout(false);
            this.toolStripEx37.PerformLayout();
            this.toolStripEx3.ResumeLayout(false);
            this.toolStripEx3.PerformLayout();
            this.ribbonTabFormat.Panel.ResumeLayout(false);
            this.ribbonTabFormat.Panel.PerformLayout();
            this.toolStripEx13.ResumeLayout(false);
            this.toolStripEx13.PerformLayout();
            this.toolStripEx17.ResumeLayout(false);
            this.toolStripEx17.PerformLayout();
            this.toolStripEx36.ResumeLayout(false);
            this.toolStripEx36.PerformLayout();
            this.toolStripEx18.ResumeLayout(false);
            this.toolStripEx18.PerformLayout();
            this.toolStripEx19.ResumeLayout(false);
            this.toolStripEx19.PerformLayout();
            this.ribbonTabDev.Panel.ResumeLayout(false);
            this.ribbonTabDev.Panel.PerformLayout();
            this.toolStripEx7.ResumeLayout(false);
            this.toolStripEx7.PerformLayout();
            this.toolStripEx12.ResumeLayout(false);
            this.toolStripEx12.PerformLayout();
            this.ribbonTabHelp.Panel.ResumeLayout(false);
            this.ribbonTabHelp.Panel.PerformLayout();
            this.toolStripEx21.ResumeLayout(false);
            this.toolStripEx21.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.statusBarAdv1)).EndInit();
            this.statusBarAdv1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.statusBarImage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.statusBarCalcTime)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.progressBar1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.statusBarStopCalcs)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.statusBarNodeGUID)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nodeListItemBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.flowLayout1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbBusySpinner)).EndInit();
            this.dockingClientPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dockingManager1)).EndInit();
            this.panelUserLogin.ResumeLayout(false);
            this.panelUserLogin.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbUserLoginStatus)).EndInit();
            this.ResumeLayout(false);

        }


        private void SfListView1_DragEnter(object sender, DragEventArgs e)
        {
            throw new NotImplementedException();
        }


        public void SetTextNodeCmbNodeAssessmentStatus(string str)
        {
            /*for (int i = 0; i < _settings.NodeimplementedStrengthData.Count; i++)
            {
                JObject tmp = (JObject)_settings.NodeimplementedStrengthData[i];
                if (str.ToLower().Equals(tmp["implementedStrength"].ToString().ToLower()))
                {
                    txtNodeNodeAssessmentStatus.Text = tmp["implementedStrength"].ToString();
                    break;
                }
            }*/
        }


        public void AddYChartCompliance(decimal data, Color color, string text, bool is_clear = false)
        {
        //    if (is_clear)
        //        riskPanelsForm.chartCompliance.Series[0].Points.Clear();
        //    if (data > 0)
        //    {
        //        int i = riskPanelsForm.chartCompliance.Series[0].Points.Count;
        //        riskPanelsForm.chartCompliance.Series[0].Points.AddY(data);
        //        riskPanelsForm.chartCompliance.Series[0].Points[i].Color = color;
        //        riskPanelsForm.chartCompliance.Series[0].Points[i].Label = text;
        //    }
        }

        public void ShowChatCompliance(bool flag)
        {
            //riskPanelsForm.chartCompliance.Visible = flag;
        }

        public void SetUpInitialVisuals()
        {


            this.ApplicationTitle = "CyConex Cyber Graph Studio: " + $"v.{Assembly.GetExecutingAssembly().GetName().Version}" + " (DEVELOPMENT BUILD)";
            this.toolStripEx14.Width = 265;
            this.toolStripEx15.Width = 85;
            this.toolStripEx19.Width = 190;
            this.toolStripEx13.Width = 290;


            riskPanelsForm.panelRiskAssetRiskList.Controls.Clear();

            this.Text = ApplicationTitle;
          
            this.ribbonControlAdv1.Height = 159;
            this.ribbonControlAdv1.SelectedTab = ribbonTabHome;
            this.dockingManager1.EnableContextMenu = false;
            this.dockingManager1.CloseEnabled = true;
           

            //panelRiskContainer
            this.riskPanelsForm.panelRiskContainer.Controls.Add(this.riskPanelsForm.panelRiskVulnerability); 
            this.riskPanelsForm.panelRiskVulnerability.Dock = DockStyle.Top;

            this.riskPanelsForm.panelRiskContainer.Controls.Add(this.riskPanelsForm.panelRiskAsset); 
            this.riskPanelsForm.panelRiskAsset.Dock = DockStyle.Top;

            this.riskPanelsForm.panelRiskContainer.Controls.Add(this.riskPanelsForm.panelRiskControl); 
            this.riskPanelsForm.panelRiskControl.Dock = DockStyle.Top;

            this.riskPanelsForm.panelRiskContainer.Controls.Add(this.riskPanelsForm.panelRiskEdge); 
            this.riskPanelsForm.panelRiskEdge.Dock = DockStyle.Top;

            this.riskPanelsForm.panelRiskContainer.Controls.Add(this.riskPanelsForm.panelRiskActor); 
            this.riskPanelsForm.panelRiskActor.Dock = DockStyle.Top;

            this.riskPanelsForm.panelRiskContainer.Controls.Add(this.riskPanelsForm.panelRiskAttack); 
            this.riskPanelsForm.panelRiskAttack.Dock = DockStyle.Top;


            //riskPanelsForm.panelCompliance.Visible = false;

            

            statusBarCalcTime.Text = "";
            statusBarNodeGUID.Text = "";
            statusBarImage.BackgroundImage = null;


        }

        public void CompletedGraphRecalc()
        {
            if (this.dockingManager1.GetDockVisibility(riskPanelsForm.panelRiskContainer))
            {
                if (_selectedNodes.Count > 0)
                {
                    Node node = _selectedNodes.ElementAt(0).Value;
                    UpdateNodeRiskPanelValues(node.ID);
                    //ShowRequiredRiskPanels(node.ID);
                    nodeDistributionsForm.UpdateNodeAssessmentInfo(node.ID);
                }
                else if (_selectedEdges.Count > 0)
                {
                    Edge edge = _selectedEdges.ElementAt(0).Value;
                    string tempNodeGUID = GraphUtil.GetTargetNodeIDFromEdge(edge.ID);
                    UpdateEdgeRiskPanelValues(edge.ID);
                }
                else
                {
                    ShowRequiredRiskPanels(null);
                    nodeDistributionsForm.UpdateNodeAssessmentInfo(null);
                }
            };
            if (this.dockingManager1.GetDockVisibility(riskPanelsForm.panelRiskAssetRiskList))
            {
                ShowRiskPanels("all", "all");
            }
                
        }

        public void ShowCalcBusy()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.statusBarImage.BackgroundImage = ilForm1.Images[0];
        }

        public void HideCalcBusy()
        {
            this.statusBarImage.BackgroundImage = null;
        }

        #endregion
        private System.Windows.Forms.ColumnHeader NodeID_columnHeader;
        private System.Windows.Forms.ColumnHeader NodeText_columnHeader;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.ToolStripMenuItem FRLayout_toolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem KKLayout_toolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ISOMLayout_toolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem LinLogLayout_toolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem SimpleTreeLayout_toolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem SimpleCircleLayout_toolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem SigiyamaLayout_toolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem CompoundFDPLayout_toolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem FSA_toolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator EndAlgo_toolStripSeparator;
        private System.Windows.Forms.ToolStripMenuItem NoOverlap_toolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem FSAOneWay_toolStripMenuItem;
        private Syncfusion.Reflection.TypeLoader designTimeTabTypeLoader;
        private System.Windows.Forms.ColorDialog nodeColorDialog;
        private Syncfusion.Windows.Forms.Tools.ToolStripTabItem ribbonTabNode;
        private Syncfusion.Windows.Forms.Tools.ToolStripTabItem ribbonTabLayout;
        private Syncfusion.Windows.Forms.Tools.ToolStripTabItem ribbonTabView;
        private Syncfusion.Windows.Forms.Tools.ToolStripEx toolStripEx3;
        private Syncfusion.Windows.Forms.Tools.ToolStripTabItem ribbonTabDev;
        private Syncfusion.Windows.Forms.Tools.ToolStripEx toolStripEx7;
        private System.Windows.Forms.ToolStripButton toolStripButton7;
        private Syncfusion.Windows.Forms.Tools.ToolStripEx toolStripEx8;
        private System.Windows.Forms.ToolStripButton toolStripButton10;
        private Syncfusion.Windows.Forms.Tools.ToolStripEx toolStripEx9;
        private System.Windows.Forms.ToolStripButton AlignToGrid_toolStripMenuItem;
        private System.Windows.Forms.ToolStripButton toolStripButton12;
        private System.Windows.Forms.ToolStripButton toolStripButton13;
        private System.Windows.Forms.ToolStripButton toolStripButton14;
        private System.Windows.Forms.ToolStripButton toolStripButton15;
        private System.Windows.Forms.ToolStripButton toolStripButton16;
        private Syncfusion.Windows.Forms.Tools.ToolStripEx toolStripEx10;
        private Syncfusion.Windows.Forms.Tools.ToolStripEx toolStripEx12;
        private System.Windows.Forms.ToolStripButton toolStripNodeManager;
        private System.Windows.Forms.ToolStripButton ShowCalculationLog_toolStripButton;
        private System.Windows.Forms.ToolStripButton toolStripButton24;
        private System.Windows.Forms.ToolStripButton toolStripButton25;
        private System.Windows.Forms.ToolStripButton toolStripButton26;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private ToolStripButton btnNodeManager;
        private BindingSource nodeListItemBindingSource;
        private ToolStripButton toolSugControls;
        private ToolStripButton toolSugObjectives;
        private ToolStripButton toolSugGroups;
        private ToolStripButton toolSugAttacks;
        private ToolStripButton toolSuggested;

        private Syncfusion.Windows.Forms.Tools.ToolStripEx toolStripEx14;
        private Syncfusion.Windows.Forms.Tools.ToolStripTabItem ribbonTabHome;
        private ToolStripButton toolStripButton2;
        private ToolStripButton toolStripButton1;
        private Syncfusion.Windows.Forms.Tools.ToolStripEx toolStripEx1;
        private ToolStripButton toolStripUndo;
        private ToolStripButton toolStripRedo;
        private Syncfusion.Windows.Forms.Tools.ToolStripEx toolStripEx15;
        private ToolStripButton toolBtnPast;
        private Syncfusion.Windows.Forms.Tools.ToolStripEx toolStripEx17;
        private ToolStripButton toolBtnTextLeft;
        private Syncfusion.Windows.Forms.Tools.ToolStripEx toolStripEx19;
        private Syncfusion.Windows.Forms.Tools.ToolStripPanelItem toolBtnFontPanel;
        private Syncfusion.Windows.Forms.Tools.ToolStripPanelItem toolBtnFontStylePanel;
        public ToolStripButton toolBtnFontBold;
        public ToolStripButton toolBtnFontItalic;
        public ToolStripButton toolBtnFontColor;
        private Syncfusion.Windows.Forms.Tools.ToolStripPanelItem toolBtnLabelPosPanel;
        private ToolStripButton toolBtnTextTop;
        private ToolStripButton toolBtnTextCenter;
        private ToolStripButton toolBtnTextBottom;
        private ToolStripButton toolBtnTextRight;
        private Syncfusion.Windows.Forms.Tools.ToolStripComboBoxEx toolBtnFontFamily;
        private Syncfusion.Windows.Forms.Tools.ToolStripComboBoxEx toolBtnFontSize;
        private Syncfusion.Windows.Forms.Tools.ToolStripPanelItem toolBtnShapePanel;
        private ToolStripLabel toolStripLabel3;
        private Syncfusion.Windows.Forms.Tools.ToolStripComboBoxEx toolComboShape;
        private Syncfusion.Windows.Forms.Tools.ToolStripPanelItem toolBtnFillPanel;
        private ToolStripLabel toolStripLabel1;
        private ToolStripButton toolBtnFillColor;
        private Syncfusion.Windows.Forms.Tools.ToolStripPanelItem toolBtnBorderPanel;
        private ToolStripLabel toolStripLabel2;
        private ToolStripButton toolBtnBorderColor;
        private Syncfusion.Windows.Forms.Tools.ToolStripComboBoxEx toolBtnBorderWidth;
        private Syncfusion.Windows.Forms.Tools.ToolStripEx toolStripEx16;
        private Syncfusion.Windows.Forms.Tools.ToolStripPanelItem toolStripPanelItem7;
        private ToolStripButton toolStripButton44;
        private ToolStripButton toolStripButton45;
        private ToolStripLabel toolStripLabel4;
        private Syncfusion.Windows.Forms.Tools.ToolStripEx toolStripEx22;
        private ToolStripButton toolSugActor;
        private ToolStripButton toolStripButton57;
        private ToolStripTextBox toolTextFillColor;
        private Syncfusion.Windows.Forms.Tools.XPMenus.BarItem barItem1;
        private Syncfusion.Windows.Forms.Tools.XPMenus.BarItem barItem2;
        private Syncfusion.Windows.Forms.Tools.XPMenus.BarItem barItem3;
        private Syncfusion.Windows.Forms.Tools.XPMenus.PopupMenusManager popupMenusManager;
        private Syncfusion.Windows.Forms.Tools.XPMenus.ParentBarItem parentBarItem1;
        private Syncfusion.Windows.Forms.Tools.XPMenus.BarItem controlBarItem;
        private Syncfusion.Windows.Forms.Tools.XPMenus.BarItem assetBarItem;
        private Syncfusion.Windows.Forms.Tools.XPMenus.BarItem groupBarItem;
        private Syncfusion.Windows.Forms.Tools.XPMenus.BarItem attackBarItem;
        private Syncfusion.Windows.Forms.Tools.XPMenus.BarItem objectiveBarItem;
        private ToolStripButton toolBtnCopy;
        private ToolStripButton toolBtnCopyImage;
        private ToolStripButton toolBtnFontSizePlus;
        private ToolStripButton toolBtnFontSizeMinus;
        private Label label32;
        private Syncfusion.Windows.Forms.Tools.ToolStripPanelItem toolStripPanelItem4;
        private ToolStripLabel toolStripLabel7;
        private ToolStripButton toolStripButton11;
        private ToolStripLabel toolStripLabel8;
        private Syncfusion.Windows.Forms.Tools.ToolStripComboBoxEx toolStripComboBoxEx2;
        private Syncfusion.Windows.Forms.Tools.ToolStripPanelItem toolStripPanelItem1;
        private ToolStripButton drawEdges_ToolStrip;
        private Syncfusion.Windows.Forms.Tools.ToolStripPanelItem toolStripPanelItem2;
        private ToolStripLabel toolStripLabel5;
        private ToolStripButton toolStripDrawnEdgeColor;
        private ToolStripLabel toolStripLabel6;
        private Syncfusion.Windows.Forms.Tools.ToolStripComboBoxEx toolStripeDrawnEdgeWidth;
        private System.Drawing.Printing.PrintDocument printDocument;
        private PrintPreviewDialog printPreviewDialog;
        private Label label6;
        private SplashControl splashControl1;
        private Panel panel149;
        private TextBoxExt txtNodeFrmTitle;
        private Panel panelMainBrowser;
        private ToolStripPanelItem btnDrawEdge;
        private ToolStripButton toolStripButton8;
        private ToolStripTabItem ribbonTabFile;
        private ToolStripEx toolStripEx4;
        private ToolStripButton btnOpenCloud;
        private ToolStripButton btnSaveCloud;
        private ToolStripButton btnSaveAsCloud;
        private ToolStripEx toolStripEx5;
        private ToolStripButton btnOpenLocal;
        private ToolStripButton btnSaveLocal;
        private ToolStripButton btnSaveAsLocal;
        private ToolStripEx toolStripEx23;
        private ToolStripButton btnAccountLogin;
        private ToolStripButton btnAccountEnterprise;
        private ToolStripButton btnAccountTenant;
        private ToolStripEx toolStripEx24;
        private ToolStripButton toolBtnPrint;
        private ToolStripEx toolStripEx25;
        private ToolStripButton btnSettingsForm;
        private RibbonControlAdv ribbonControlAdv1;
        private Panel panel205;
        private Panel panel206;
        private Syncfusion.Windows.Forms.Gauge.LinearGauge linearGauge1;
        private ToolStripButton btnNewGraph;
        private ToolStripButton toolStripButton9;
        private Panel panelRiskDataGrid;
        private ToolStripButton toolStripButton17;
        private ToolStripButton toolBtnOpenAuditlog;
        private ToolStripTabItem ribbonTabFormat;
        private ToolStripEx toolStripEx26;
        private ToolStripEx toolStripEx11;
        private ToolStripButton toolStripButton5;
        private ToolStripButton btnDrawEdges;
        private ToolStripButton toolStripButton21;
        private ToolStripButton toolStripButton20;
        private ToolStripEx toolStripEx27;
        private ToolStripButton btnHeatMap;
        public ToolStripButton btnNodePath;
        private ToolStripPanelItem toolStripEx30;
        private ToolStripEx toolStripEx28;
        private ToolStripButton toolStripButton18;
        private ToolStripButton toolStripButton19;
        private ToolStripButton toolBtnFind;
        private StatusBarAdv statusBarAdv1;
        private StatusBarAdvPanel statusBarCalcTime;
        private ToolStripPanelItem toolStripPanelItem5;
        private ToolStripPanelItem toolStripPanelItem9;
        private ToolStripCheckBox cbAutoCalc;
        private StatusBarAdvPanel statusBarNodeGUID;
        private StatusBarAdvPanel statusBarImage;
        private ToolStripPanelItem toolStripPanelItem8;
        private ToolStripCheckBox cbNodePanel;
        private FlowLayout flowLayout1;
        private ToolStripEx toolStripEx13;
        private ToolStripPanelItem toolStripPanelItem10;
        private ToolStripButton btnBackgroundImage;
        private ToolStripButton btnClearBackgroundImage;
        private ToolStripButton backImg;
        private ToolStripPanelItem toolStripPanelItem11;
        private ToolStripButton toolBtnFullSize;
        private ToolStripButton toolBtnEnableBackgroundEdit;
        private ToolStripButton toolBtnBackgroundColor;
        private ToolStripPanelItem toolStripPanelItem13;
        private ToolStripEx toolStripEx18;
        private ToolStripPanelItem toolStripPanelItem14;
        private ToolStripButton toolBtnSetNodeImage;
        private ToolStripButton toolBtnClearNodeImage;
        private ImageList ilForm1;
        private ToolStripEx toolStripEx2;
        private ToolStripButton btnAddObjectiveNode;
        private ToolStripButton btnAddEvidenceNode;
        private ToolStripButton toolStripButton23;
        private ToolStripSplitButton toolStripSplitButton1;
        private ToolStripMenuItem toolStripMenuItem1;
        private ToolStripMenuItem toolStripMenuItem2;
        private ToolStripEx toolStripEx6;
        private ToolStripButton toolStripButton4;
        private ToolStripButton toolStripButton22;
        private ToolStripButton btnAccountUser;
        private ToolStripButton toolStripButton27;
        private PictureBox pbBusySpinner;
        private ToolStripSplitButton toolStripSplitButton2;
        private ToolStripMenuItem toolStripMenuItem4;
        private ToolStripMenuItem toolStripMenuItem5;
        private ToolStripMenuItem toolStripMenuItem8;
        private ToolStripMenuItem toolStripMenuItem9;
        private ToolStripSeparator toolStripMenuItem3;
        private ToolStripMenuItem clearDistributionsToolStripMenuItem;
        private StatusBarAdvPanel statusBarStopCalcs;
        private ProgressBarAdv progressBar1;
        private ToolStripMenuItem toolStripMenuItem12;
        private ToolStripMenuItem toolStripMenuItem13;
        private ToolStripButton toolStripButton28;
        private DockingClientPanel dockingClientPanel1;
        private DockingManager dockingManager1;
        private Panel panelUserLogin;
        private ToolStripTabItem ribbonTabHelp;
        private ToolStripEx toolStripEx21;
        private ToolStripButton toolStripButton34;
        private ToolStripButton toolStripButton29;
        private ToolStripButton toolStripButton30;
        private ToolStripButton toolStripButton31;
        private ToolStripButton toolStripButton33;
        private ToolStripEx toolStripEx29;
        private ToolStripButton btnDetail;
        private ToolStripButton btnValues;
        private ToolStripButton btnRiskList;
        private ToolStripEx toolStripEx31;
        private ToolStripButton btnDistributions;
        private ToolStripEx toolStripEx32;
        private ToolStripEx toolStripEx33;
        private ToolStripButton btnGrid;
        private ToolStripButton btnLabels;
        private ToolStripButton btnSwitch;
        private ToolStripDropDownButton toolStripDropDownButton1;
        private ToolStripMenuItem ckShowEdges;
        private ToolStripMenuItem ckShowActor;
        private ToolStripMenuItem ckShowAttack;
        private ToolStripMenuItem ckShowAsset;
        private ToolStripMenuItem ckShowControl;
        private ToolStripMenuItem ckShowEvidence;
        private ToolStripMenuItem ckShowGroup;
        private ToolStripMenuItem ckShowObjective;
        private ToolStripMenuItem ckShowVulnerability;
        private ToolStripEx toolStripEx34;
        private ToolStripButton toolStripButton35;
        private ToolStripButton btnNodeData;
        public Label lblUserEmail;
        public Label lblUserName;
        private PictureBox pbUserLoginStatus;
        private ToolStripPanelItem toolStripPanelItem12;
        private ToolStripPanelItem toolStripPanelItem6;
        private ToolStripComboBox toolStripComboBox1;
        private ToolStripEx toolStripEx20;
        private ToolStripComboBox toolStripComboBox2;
        private ToolStripPanelItem toolStripPanelItem15;
        private ToolStripPanelItem toolStripPanelItem16;
        private ToolStripComboBox toolCmbLayoutList;
        private ToolStripPanelItem toolStripPanelItem17;
        private ToolStripButton toolBtnDockingSave;
        private ToolStripButton toolBtnDockingDelete;
        private ToolStripButton toolBtnDockingRename;
        private Timer saveTimer;
        private ToolStripButton toolBtnDockingApply;
        private ToolStripButton toolStripButton36;
        private ToolStripButton toolStripButton37;
        private ToolStripEx toolStripEx35;
        private ToolStripButton toolBtnDeleteElement;
        private ToolStripEx toolStripEx36;
        private ToolStripButton toolBtnNodeMinus;
        private ToolStripButton toolBtnNodePlus;
        private ToolStripButton btnNodeRepository;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripButton btnNodeAutoSize;
        private ToolStripButton btnTippyShow;
        private ToolStripEx toolStripEx37;
        private ToolStripDropDownButton toolBtnEdgeTransparency;
        private ToolStripMenuItem miEdgeTrans100;
        private ToolStripMenuItem miEdgeTrans50;
        private ToolStripMenuItem miEdgeTrans75;
        private ToolStripMenuItem miEdgeTrans0;
        private ToolStripMenuItem miEdgeTrans25;
        private ToolStripButton toolBtnSaveAs;
        private ToolStripButton btnCompliance;
        private ToolStripButton btnRiskHeatMap;
        private ToolStripDropDownButton btnHeatMaps;
        private ToolStripMenuItem miRiskHeatmap;
        private ToolStripMenuItem miComplianceHeatmap;
        private ToolStripButton toolStripButton32;
        private ToolStripButton toolBtnAvsdfLayout;
        private ToolStripButton toolBtnCiseLayout;
        private ToolStripButton toolBtnCoseBilkentLayout;
        private ToolStripButton toolBtnElkLayered;
        private ToolStripButton toolBtnElkMrTree;
        private ToolStripButton toolStripButton6;
    }
}