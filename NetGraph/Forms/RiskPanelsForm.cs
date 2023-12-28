using System.Windows.Forms;
using Syncfusion.WinForms.DataGrid;
using Syncfusion.WinForms.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Controls;


using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;
using DataGrid = System.Windows.Forms.DataGrid;
using CyConex;
using Newtonsoft.Json.Linq;
using Syncfusion.WinForms.DataGrid.Interactivity;
using CefSharp;
using CyConex.Chromium;
using CyConex.Graph;
using System.Linq;
using System.Windows.Forms.DataVisualization.Charting;
using Syncfusion.Windows.Forms.Chart;
using Syncfusion.DocIO.DLS;
using Label = System.Windows.Controls.Label;
using static Syncfusion.Windows.Forms.Tools.MenuDropDown;
using Control = System.Windows.Forms.Control;
using System.Runtime.Remoting.Contexts;
using Syncfusion.Windows.Forms.Diagram;
using Newtonsoft.Json;
using System.Globalization;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using CyConex.Forms;
using Syncfusion.Windows.Forms.Tools;
using CyConex.Helpers;
using CefSharp.DevTools.DOM;
using System.Windows.Navigation;

namespace CyConex
{

    public partial class RiskPanelsForm : SfForm
    {
        public MainForm mainForm;

        public RiskPanelsForm(MainForm callingForm)
        {
            mainForm = callingForm as MainForm;

            InitializeComponent();
            InitializeCharts();
        }

        protected override void SetVisibleCore(bool value)
        {
            if (!this.IsHandleCreated)
            {
                this.CreateHandle();
                value = false;
            }
            base.SetVisibleCore(value);
        }

        private void zeroChartActor()
        {

        }
        private void InitializeChart(ChartControl chart)
        {
            var series = chart.Series[0];
            series.Resolution = 0D;
            series.Points.Clear();

            for (int i = 0; i < 101; i++) // Create a series of zero values
                series.Points.Add(i, 0);

            var MinMax = new MinMaxInfo(0, 100, 1);
            chart.PrimaryXAxis.SetRange(MinMax);

            MinMax = new MinMaxInfo(0, 1, 1);
            chart.PrimaryYAxis.SetRange(MinMax);
        }

        public void InitializeCharts()
        {
            InitializeChart(chartActor);
            InitializeChart(chartActorMitigated);
            InitializeChart(chartAttack);
            InitializeChart(chartAttackMitigated);
            InitializeChart(chartAttackThreat);
            InitializeChart(chartVulnerability);
            InitializeChart(chartVulnerabilityMitigated);
            InitializeChart(chartVulnerabilityLikelihood);
            InitializeChart(chartAsset);
            InitializeChart(chartAssetMitigated);
            InitializeChart(chartAssetImpact);
            InitializeChart(chartControlStrength);
            InitializeChart(chartControlImplementation);
            InitializeChart(chartControlCalculated);
            InitializeChart(chartEdgeStrength);
        }

        public void ProcessChartPoints(ChartControl chart, ChartPointIndexer points)
        {
            Console.WriteLine("RiskPanelsForm > ProcessChartPoints");
            //check if graph MinMax needs to be increased
            if (points.Count > 101)
            {
                MinMaxInfo XMinMax = new MinMaxInfo(0, points.Count, 1);
                chart.PrimaryXAxis.SetRange(XMinMax);
            }
            else
            {
                MinMaxInfo XMinMax = new MinMaxInfo(0, 100, 1);
                chart.PrimaryXAxis.SetRange(XMinMax);
            }


            MinMaxInfo YMinMax = new MinMaxInfo(0, 1, 1);
            chart.PrimaryYAxis.SetRange(YMinMax);

            if (points != null)
            {
                try
                {
                    chart.Series[0].Points.Clear();
                    for (int i = 0; i < points.Count; i++)
                        chart.Series[0].Points.Add(i, points[i].YValues[0]);
                }
                catch
                { 
                    return; 
                }
            }

            chart.Refresh();
        }


        public void UpdateGraph(string GraphName, string NodeID)
        {
            Console.WriteLine("RiskPanelsForm > UpdateGraph");
            ChartControl chart = null;

            switch (GraphName)
            {
                case "actor":
                    chart = chartActor;
                    break;
                case "actorMitigated":
                    NodeID += ":Mitigated";
                    chart = chartActorMitigated;
                    break;
                case "attack":
                    chart = chartAttack;
                    break;
                case "attackMitigated":
                    NodeID += ":Mitigated";
                    chart = chartAttackMitigated;
                    break;
                case "attackThreat":
                    NodeID += ":Threat";
                    chart = chartAttackThreat;
                    break;
                case "vulnerability":
                    chart = chartVulnerability;
                    break;
                case "vulnerabilityMitigated":
                    NodeID += ":Mitigated";
                    chart = chartVulnerabilityMitigated;
                    break;
                case "vulnerabilityLikelihood":
                    NodeID += ":Likelihood";
                    chart = chartVulnerabilityLikelihood;
                    break;
                case "asset":
                    chart = chartAsset;
                    break;
                case "assetMitigated":
                    NodeID += ":Mitigated";
                    chart = chartAssetMitigated;
                    break;
                case "assetImpact":
                    chart = chartAssetImpact;
                    NodeID += ":Impact";
                    break;
                case "controlStrengh":
                    chart = chartControlStrength;
                    NodeID += ":Base";
                    break;
                case "controlImplementation":
                    chart = chartControlImplementation;
                    NodeID += ":Assessed";
                    break;
                case "controlValue":
                    chart = chartControlCalculated;
                    NodeID += ":Calculated";
                    break;
                case "edgeStrength":
                    chart = chartEdgeStrength;
                    break;
            }

            ChartPointIndexer distributionData = (ChartPointIndexer)GraphUtil.GetDistributionData(NodeID);
            if (chart != null && distributionData != null)
            {
                ProcessChartPoints(chart, distributionData);
            }
            
            if (distributionData == null)
            {
                chart.Series[0].Points.Clear();
                chart.Refresh();

            }

        }

        private void lblActorScore_TextChanged(object sender, EventArgs e)
        {
            double actorScore = this.lblActorScore.Text == "N/A" || this.lblActorScore.Text == "-" || this.lblActorScore.Text == "" ? 0 : Double.Parse(this.lblActorScore.Text);
            actorScore = Math.Round(actorScore);
            panelThreatActor.BackColor = GraphUtil.GetRiskColorFromValue(actorScore);
            //this.mainForm.SetGaugeValue(this.chartActorValue, actorScore);
        }

        private void lblactorMitigatedScore_TextChanged(object sender, EventArgs e)
        {
            double actorMitigatedScore = this.lblactorMitigatedScore.Text == "N/A" || this.lblactorMitigatedScore.Text == "-" || this.lblactorMitigatedScore.Text == "" ? 0 : Double.Parse(this.lblactorMitigatedScore.Text);
            panelThreatActorMitigated.BackColor = GraphUtil.GetRiskColorFromValue(actorMitigatedScore);
            //this.mainForm.SetGaugeValue(this.chartActorMitigatedValue, actorMitigatedScore);
        }

        private void lblControlStrengthValue_TextChanged(object sender, EventArgs e)
        {
            double controlStrengthValue = this.lblControlStrengthValue.Text == "N/A" || this.lblControlStrengthValue.Text == "-" || this.lblControlStrengthValue.Text == "" ? 0 : Double.Parse(this.lblControlStrengthValue.Text);
            panelControlStrength.BackColor = GraphUtil.GetRiskColorFromValueInverted(controlStrengthValue);
        }

        private void lblControlImplementationValue_TextChanged(object sender, EventArgs e)
        {
            double ControlImplementationValue = this.lblControlImplementationValue.Text == "N/A" || this.lblControlImplementationValue.Text == "-" || this.lblControlImplementationValue.Text == "" ? 0 : Double.Parse(this.lblControlImplementationValue.Text);
            panelControlAssessed.BackColor = GraphUtil.GetRiskColorFromValueInverted(ControlImplementationValue);
        }

        private void lblControlValue_TextChanged(object sender, EventArgs e)
        {
            double controlValue = this.lblControlValue.Text == "N/A" || this.lblControlValue.Text == "-" || this.lblControlValue.Text == "" || this.lblControlValue.Text == "None" ? 0 : Double.Parse(this.lblControlValue.Text);
            panelControlValue.BackColor = GraphUtil.GetRiskColorFromValueInverted(controlValue);
        }

        private void lblvulnerabilityScore_TextChanged(object sender, EventArgs e)
        {
            double vulnerabilityScore = this.lblvulnerabilityScore.Text == "N/A" || this.lblvulnerabilityScore.Text == "-" || this.lblvulnerabilityScore.Text == "" ? 0 : Double.Parse(this.lblvulnerabilityScore.Text);
            panelVulnerabilityScore.BackColor = GraphUtil.GetRiskColorFromValue(vulnerabilityScore);
        }

        private void lblvulnerabilityMitigatedScore_TextChanged(object sender, EventArgs e)
        {
            double vulnerabilityMitigatedScore = this.lblvulnerabilityMitigatedScore.Text == "N/A" || this.lblvulnerabilityMitigatedScore.Text == "-" || this.lblvulnerabilityMitigatedScore.Text == "" ? 0 : Double.Parse(this.lblvulnerabilityMitigatedScore.Text);
            panelVulnerabilityMitigatedScore.BackColor = GraphUtil.GetRiskColorFromValue(vulnerabilityMitigatedScore);
        }

        private void lblvulnerabilityLikelihoodScore_TextChanged(object sender, EventArgs e)
        {
            double vulnerabilityLikelihoodScore = this.lblvulnerabilityLikelihoodScore.Text == "N/A" || this.lblvulnerabilityLikelihoodScore.Text == "-" || this.lblvulnerabilityLikelihoodScore.Text == "" ? 0 : Double.Parse(this.lblvulnerabilityLikelihoodScore.Text);
            panelVulnerabilityLikelihoodScore.BackColor = GraphUtil.GetRiskColorFromValue(vulnerabilityLikelihoodScore);
        }

        private void lblassetScore_TextChanged(object sender, EventArgs e)
        {
            double assetScore = this.lblassetScore.Text == "N/A" || this.lblassetScore.Text == "-" || this.lblassetScore.Text == "" ? 0 : Double.Parse(this.lblassetScore.Text);
            panelAssetScore.BackColor = GraphUtil.GetRiskColorFromValue(assetScore);

        }

        private void lblassetMitigatedScore_TextChanged(object sender, EventArgs e)
        {
            double assetMitigatedScore = this.lblassetMitigatedScore.Text == "N/A" || this.lblassetMitigatedScore.Text == "-" || this.lblassetMitigatedScore.Text == "" ? 0 : Double.Parse(this.lblassetMitigatedScore.Text);
            panelAssetMitigatedScore.BackColor = GraphUtil.GetRiskColorFromValue(assetMitigatedScore);
        }

        private void lblimpactScore_TextChanged(object sender, EventArgs e)
        {
            double impactScore = this.lblimpactScore.Text == "N/A" || this.lblimpactScore.Text == "-" || this.lblimpactScore.Text == "" ? 0 : Double.Parse(this.lblimpactScore.Text);
            panelImpactScore.BackColor = GraphUtil.GetRiskColorFromValue(impactScore);
        }

        private void lblattackScore_TextChanged(object sender, EventArgs e)
        {
            double attackScore = this.lblattackScore.Text == "N/A" || this.lblattackScore.Text == "-" || this.lblattackScore.Text == "" ? 0 : Double.Parse(this.lblattackScore.Text);
            panelAttackScore.BackColor = GraphUtil.GetRiskColorFromValue(attackScore);
        }

        private void lblattackMitigatedScore_TextChanged(object sender, EventArgs e)
        {
            double attackMitigatedScore = this.lblattackMitigatedScore.Text == "N/A" || this.lblattackMitigatedScore.Text == "-" || this.lblattackMitigatedScore.Text == "" ? 0 : Double.Parse(this.lblattackMitigatedScore.Text);
            panelAttackMitigatedScore.BackColor = GraphUtil.GetRiskColorFromValue(attackMitigatedScore);
        }

        private void lblthreatScore_TextChanged(object sender, EventArgs e)
        {
            double threatScore = this.lblthreatScore.Text == "N/A" || this.lblthreatScore.Text == "-" || this.lblthreatScore.Text == "" ? 0 : Double.Parse(this.lblthreatScore.Text);
            panelThreatScore.BackColor = GraphUtil.GetRiskColorFromValue(threatScore);
        }

        private void sfDataGrid1_CellClick(object sender, Syncfusion.WinForms.DataGrid.Events.CellClickEventArgs e)
        {
            if (cbShowRiskPath.Checked)
            {
                string jArray;
                if (e.DataRow.RowIndex > 0)
                {
                    int rowIndex = e.DataRow.RowIndex;
                    graphResultsList data = (graphResultsList)e.DataRow.RowData;
                    jArray = data.FullNodeEdgePath;
                    this.mainForm._browser.ExecScriptAsync($"drawHighlight('{true}', {jArray});");
                }
            }
        }

        private void cbShowRiskPath_CheckStateChanged(object sender, EventArgs e)
        {
            if (!cbShowRiskPath.Checked)
                this.mainForm._browser.ExecScriptAsync($"drawHighlight('{false}');");
        }

        private void pbUserLoggedIn_Click(object sender, EventArgs e)
        {
            Graph.Utility.SaveAuditLog("handleAccountLogin", "Logged In Icon Click", "", "", $"");
            this.mainForm.handleAccountLogin();
        }

        private void pbUserNotLoggedIn_Click(object sender, EventArgs e)
        {
            Graph.Utility.SaveAuditLog("handleAccountLogin", "Logged Out Icon Click", "", "", $"");
            this.mainForm.handleAccountLogin();
        }


        private void clearRiskList()
        {
            this.panelRiskAssetRiskList.Controls.Clear();
        }

        public void AddToRiskList(double riskValue, string riskStatus, string riskText, string nodePath, Color riskColor)
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RiskPanelsForm));
            this.panelRiskDetail = new System.Windows.Forms.Panel();
            this.panelBottom = new System.Windows.Forms.Panel();
            this.labelRiskStatus = new System.Windows.Forms.Label();
            this.panelColor = new System.Windows.Forms.Panel();
            this.btnNodePath = new System.Windows.Forms.Button();
            this.btnCopy = new System.Windows.Forms.Button();
            this.btnHeatMap = new System.Windows.Forms.Button();
            this.labelScore = new System.Windows.Forms.Label();

            this.labelRiskText = new System.Windows.Forms.Label();
            this.labelRiskText.AutoEllipsis = true;
            this.labelRiskText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelRiskText.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelRiskText.Location = new System.Drawing.Point(31, 13);
            this.labelRiskText.Size = new System.Drawing.Size(255, 53);
            this.labelRiskText.Text = riskText;

            this.panelBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelBottom.Location = new System.Drawing.Point(31, 66);
            this.panelBottom.Margin = new System.Windows.Forms.Padding(0);
            this.panelBottom.Size = new System.Drawing.Size(255, 31);

            this.labelRiskStatus.Dock = System.Windows.Forms.DockStyle.Top;
            this.labelRiskStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelRiskStatus.Location = new System.Drawing.Point(31, 0);
            this.labelRiskStatus.Size = new System.Drawing.Size(255, 13);
            this.labelRiskStatus.Text = riskStatus;

            this.panelColor.BackColor = riskColor;
            this.panelColor.Controls.Add(this.labelScore);
            this.panelColor.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelColor.Location = new System.Drawing.Point(0, 0);
            this.panelColor.Size = new System.Drawing.Size(31, 97);

            this.btnNodePath.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnNodePath.Image = ((System.Drawing.Image)(resources.GetObject("btnNodePath.Image")));
            this.btnNodePath.Size = new System.Drawing.Size(33, 31);
            this.btnNodePath.UseVisualStyleBackColor = true;
            this.btnNodePath.Tag = nodePath + ";" + riskColor.ToArgb().ToString();
            this.btnNodePath.Click += NodePath_Click;

            this.btnCopy.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnCopy.Image = ((System.Drawing.Image)(resources.GetObject("btnCopy.Image")));
            this.btnCopy.Size = new System.Drawing.Size(33, 31);
            this.btnCopy.UseVisualStyleBackColor = true;
            this.btnCopy.Click += CopyToClipBoard;
            this.btnCopy.Tag = riskValue.ToString() + "," + riskStatus + "," + riskText;

            this.btnHeatMap.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnHeatMap.Image = ((System.Drawing.Image)(resources.GetObject("btnHeatMap.Image")));
            this.btnHeatMap.Size = new System.Drawing.Size(33, 31);
            this.btnHeatMap.UseVisualStyleBackColor = true;

            this.panelBottom.Controls.Add(this.btnNodePath);
            this.panelBottom.Controls.Add(this.btnCopy);
            this.panelBottom.Controls.Add(this.btnHeatMap);

            this.labelScore.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelScore.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelScore.Size = new System.Drawing.Size(31, 97);
            this.labelScore.Text = riskValue.ToString();
            this.labelScore.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            this.panelRiskDetail.Controls.Add(this.labelRiskText);
            this.panelRiskDetail.Controls.Add(this.panelBottom);
            this.panelRiskDetail.Controls.Add(this.labelRiskStatus);
            this.panelRiskDetail.Controls.Add(this.panelColor);
            this.panelRiskDetail.Location = new System.Drawing.Point(3, 3);
            this.panelRiskDetail.Size = new System.Drawing.Size(panelRiskAssetRiskList.Width - 10, 97);

            this.panelRiskAssetRiskList.Controls.Add(panelRiskDetail);

        }

        private void NodePath_Click(object sender, EventArgs e)
        {
            mainForm.btnNodePath.Enabled = true;
            mainForm.btnNodePath.Checked = true;

            string data = (sender as Control).Tag.ToString();
            string[] content = data.Split(';');
            this.mainForm._browser.ExecScriptAsync($"drawHighlight('{true}', " +
                $"{content[0]} , " +
                $"'{ColorTranslator.ToHtml(Color.FromArgb(int.Parse(content[1], CultureInfo.InvariantCulture)))}');");
        }

        private void CopyToClipBoard(object sender, EventArgs e)
        {
            string copyText = (sender as Control).Tag.ToString();
            Clipboard.SetText(copyText);
        }

        private void panel5_Resize(object sender, EventArgs e)
        {
            //foreach (Control control in panel5.Controls)
            //{
            //    control.Width = panel5.Width - 10;
            //}
        }


        private void panelRiskAssetRiskList_SizeChanged(object sender, EventArgs e)
        {

            foreach (Control control in panelRiskAssetRiskList.Controls)
            {
                control.Width = panelRiskAssetRiskList.Width - 10;
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            RTFForm RtfForm = new RTFForm();
            RtfForm.LoadResouceFile("Threat Actor Node", "en-ThreatActor.rtf");
            RtfForm.ShowDialog();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            RTFForm RtfForm = new RTFForm();
            RtfForm.LoadResouceFile("Attack Node", "en-Attack.rtf");
            RtfForm.ShowDialog();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            RTFForm RtfForm = new RTFForm();
            RtfForm.LoadResouceFile("Vulnerability Node", "en-Vulnerability.rtf");
            RtfForm.ShowDialog();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            RTFForm RtfForm = new RTFForm();
            RtfForm.LoadResouceFile("Asset Node", "en-Asset.rtf");
            RtfForm.ShowDialog();
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            RTFForm RtfForm = new RTFForm();
            RtfForm.LoadResouceFile("Control Node", "en-Control.rtf");
            RtfForm.ShowDialog();
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            RTFForm RtfForm = new RTFForm();
            RtfForm.LoadResouceFile("Edge", "en-Edge.rtf");
            RtfForm.ShowDialog();
        }

        private void lblvulnerabilityLikelihoodFrequency_TextChanged(object sender, EventArgs e)
        {
            panelVulnerabilityLikelihoodFrequency.BackColor = panelVulnerabilityLikelihoodScore.BackColor;
        }
    }

}
