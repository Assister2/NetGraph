using CefSharp.DevTools.CSS;
using CyConex.Graph;
using CyConex.Helpers;
using Syncfusion.Drawing;
using Syncfusion.Presentation;
using Syncfusion.Windows.Forms.Chart;
using Syncfusion.Windows.Forms.Edit.Utils;
using Syncfusion.Windows.Forms.Grid;
using Syncfusion.WinForms.DataGrid;
using Syncfusion.WinForms.SmithChart;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Windows.Media;
using Color = System.Drawing.Color;

namespace CyConex
{
    public partial class RiskDistrubutionsForm : Form
    {
        HeatMap gridHeatMapConf = new HeatMap(101, 101);
        HeatMap gridHeatMapInteg = new HeatMap(101, 101);
        HeatMap gridHeatMapAvail = new HeatMap(101, 101);
        HeatMap gridHeatMapAcc = new HeatMap(101, 101);

        private MainForm mainForm = null;
        private Dictionary<int, Color> colorHeatMap = new Dictionary<int, Color>();
        List<string> assetNodeList = new List<string>();

        public RiskDistrubutionsForm(MainForm callingForm)
        {
            mainForm = callingForm as MainForm;

            InitializeComponent();
            InitializeAssetListBox();
            InitializeChart(chartConfidentialityBubble);
            InitializeChart(chartIntegrityBubble);
            InitializeChart(chartAvailabilityBubble);
            InitializeChart(chartAccountabilityBubble);

            colorHeatMap = CreateHeatmap(0, 100, 100);

        }

        public void InitializeAssetListBox()
        {
            assetNodeList.Clear();
            lbAssets.Items.Clear();
            assetNodeList = GraphUtil.GetAssetNodes();
            if (assetNodeList != null && assetNodeList.Count > 1) 
            {
                panelAssetList.Visible = true;
                foreach (string nodeGUID in assetNodeList)
                {
                    lbAssets.Items.Add(GraphUtil.GetNodeTitle(nodeGUID));
                }
            }
            else
                panelAssetList.Visible = false;
        }



        private void InitializeChart(ChartControl chart)
        {
            chart.BeginUpdate();
            var series = chart.Series[0];
            series.Resolution = 0D;
            series.Points.Clear();

            var MinMax = new MinMaxInfo(0, 100, 1);
            chart.PrimaryXAxis.SetRange(MinMax);
            MinMax = new MinMaxInfo(0, 100, 1);
            chart.PrimaryYAxis.SetRange(MinMax);

            chart.Series[0].ConfigItems.BubbleItem.MinBounds = new RectangleF(0, 0, 25, 25);
            chart.Series[0].ConfigItems.BubbleItem.MaxBounds = new RectangleF(0, 0, 25, 25);
            chart.EndUpdate();

        }


        public void ProcessChartPoints(ChartControl chart, ChartPointIndexer points)
        {
            if (points != null)
            {
                chart.Series[0].Points.Clear();
                for (int i = 0; i < points.Count; i++)  
                    chart.Series[0].Points.Add(i, points[i].YValues[0]);
            }

            chart.Refresh();
        }

        public Dictionary<int, Color> CreateHeatmap(int minValue, int maxValue, int numSteps)
        {
            // Define the color range for the heatmap
            Color Color1 = Color.Green;
            Color Color2 = Color.GreenYellow;
            Color Color3 = Color.Yellow;
            Color Color4 = Color.DarkOrange;
            Color Color5 = Color.OrangeRed;
            Color Color6 = Color.Red;

            // Define the mid-point values
            int mid1 = (int)(((double)numSteps / 5.0 / (double)maxValue) * 100.0);
            int mid2 = (int)(((double)numSteps / 5.0 / (double)maxValue) * 200.0);
            int mid3 = (int)(((double)numSteps / 5.0 / (double)maxValue) * 300.0);
            int mid4 = (int)(((double)numSteps / 5.0 / (double)maxValue) * 400.0);

            // Create a dictionary to store the color values for each value in the range
            Dictionary<int, Color> heatmapColors = new Dictionary<int, Color>();
            for (int i = minValue; i <= maxValue; i++)
            {
                if (i <= mid1)
                {
                    // Violet to blue gradient
                    int r = (int)(((double)(i - minValue) / (double)(mid1 - minValue)) * (Color2.R - Color1.R)) + Color1.R;
                    int g = (int)(((double)(i - minValue) / (double)(mid1 - minValue)) * (Color2.G - Color1.G)) + Color1.G;
                    int b = (int)(((double)(i - minValue) / (double)(mid1 - minValue)) * (Color2.B - Color1.B)) + Color1.B;
                    heatmapColors.Add(i, Color.FromArgb(255, r, g, b));
                }
                else if (i <= mid2)
                {
                    // Blue to cyan gradient
                    int r = (int)(((double)(i - mid1) / (double)(mid2 - mid1)) * (Color3.R - Color2.R)) + Color2.R;
                    int g = (int)(((double)(i - mid1) / (double)(mid2 - mid1)) * (Color3.G - Color2.G)) + Color2.G;
                    int b = (int)(((double)(i - mid1) / (double)(mid2 - mid1)) * (Color3.B - Color2.B)) + Color2.B;
                    heatmapColors.Add(i, Color.FromArgb(255, r, g, b));
                }
                else if (i <= mid3)
                {
                    // Cyan to green gradient
                    int r = (int)(((double)(i - mid2) / (double)(mid3 - mid2)) * (Color4.R - Color3.R)) + Color3.R;
                    int g = (int)(((double)(i - mid2) / (double)(mid3 - mid2)) * (Color4.G - Color3.G)) + Color3.G;
                    int b = (int)(((double)(i - mid2) / (double)(mid3 - mid2)) * (Color4.B - Color3.B)) + Color3.B;
                    heatmapColors.Add(i, Color.FromArgb(255, r, g, b));
                }
                else if (i <= mid4)
                {
                    // Green to yellow gradient
                    int r = (int)(((double)(i - mid3) / (double)(mid4 - mid3)) * (Color5.R - Color4.R)) + Color4.R;
                    int g = (int)(((double)(i - mid3) / (double)(mid4 - mid3)) * (Color5.G - Color4.G)) + Color4.G;
                    int b = (int)(((double)(i - mid3) / (double)(mid4 - mid3)) * (Color5.B - Color4.B)) + Color4.B;
                    heatmapColors.Add(i, Color.FromArgb(255, r, g, b));
                }
                else
                {
                    // Yellow to red gradient
                    int r = (int)(((double)(i - mid4) / (double)(maxValue - mid4)) * (Color6.R - Color5.R)) + Color5.R;
                    int g = (int)(((double)(i - mid4) / (double)(maxValue - mid4)) * (Color6.G - Color5.G)) + Color5.G;
                    int b = (int)(((double)(i - mid4) / (double)(maxValue - mid4)) * (Color6.B - Color5.B)) + Color5.B;
                    heatmapColors.Add(i, Color.FromArgb(255, r, g, b));
                }
            }

            return heatmapColors;
        }


        public Color GetHeatmapColor(double strength, double intensity)
        {

            if (intensity > 255) intensity = 255;
            if (intensity < 0) intensity = 0;
            if (intensity == 0)
                return Color.FromArgb((int)intensity, Color.White); 

            if (strength> 100) strength = 100;

            if (strength == -1) // Retun WhaiteSmoke if value is -1
            {
                return Color.FromArgb((int)intensity, 245, 245, 245);
            }
           
            if (strength < -1) strength = 0;

            Color heatMapColor;
            // Get color associated with value
            if (colorHeatMap.ContainsKey((int)strength))
            {
                heatMapColor = colorHeatMap[(int)strength];
            }
            else if (strength < 0)
            {
                heatMapColor = colorHeatMap[0];
            }
            else
            {
                heatMapColor = colorHeatMap[500];
            }


            return Color.FromArgb((int)intensity, heatMapColor.R, heatMapColor.G, heatMapColor.B);  // New color with specified alpha value
        }



        public static double ConvertRange(double oldValue, double oldMin, double oldMax, double newMin, double newMax)
        {
            return (double)(((float)(oldValue - oldMin) / (oldMax - oldMin)) * (newMax - newMin) + newMin);
        }

      
        private void HeatMapForm_Load(object sender, EventArgs e)
        {
            gridHeatMapConf.Dock = DockStyle.Fill;
            gridHeatMapInteg.Dock = DockStyle.Fill;
            gridHeatMapAvail.Dock = DockStyle.Fill;
            gridHeatMapAcc.Dock = DockStyle.Fill;
        }

        public void CalculateHeatMaps(string nodeID)
        {
            if (nodeID == null || nodeID == "")
                return;


            ChartPointIndexer likelihoodDistribution;
            ChartPointIndexer confidentialityImpactDistribution;
            ChartPointIndexer integrityImpactDistribution;
            ChartPointIndexer availabilityImpactDistribution;
            ChartPointIndexer accountabilityImpactDistribution;


                
            int MAX_SIZE = 10;
            double likelihoodHighestValue = 0;

            double confidentialityValue = 0;
            double likelihoodValue = 0;
            double confidentialityRiskValue = 0;
            double confidentialityX = 0;
            double confidentialityY = 0;
            double confidentialityHighestValue = 0;
            double confidentialityHighestRiskValue = 0;
            double confidentialityHighestIntensity = 0;

            double integrityRiskValue = 0;
            double integrityValue = 0;
            double integrityX = 0;
            double integrityY = 0;
            double integrityHighestValue = 0;
            double integrityHighestRiskValue = 0;
            double integrityHighestIntensity = 0;

            double availabilityRiskValue = 0;
            double availabilityValue = 0;
            double availabilityX = 0;
            double availabilityY = 0;
            double availabilityHighestValue = 0;
            double availabilityHighestRiskValue = 0;
            double availabilityHighestIntensity = 0;

            double accountabilityRiskValue = 0;
            double accountabilityValue = 0;
            double accountabilityX = 0;
            double accountabilityY = 0;
            double accountabilityHighestValue = 0;
            double accountabilityHighestRiskValue = 0;
            double accountabilityHighestIntensity = 0;

            //Get distribution data
            likelihoodDistribution = (ChartPointIndexer)GraphUtil.GetIncommingLikelihoodDistribution(nodeID);
            confidentialityImpactDistribution = (ChartPointIndexer)GraphUtil.GetDistributionData(nodeID + ":AssetConfidentialityMitigatedDistribution");
            integrityImpactDistribution = (ChartPointIndexer)GraphUtil.GetDistributionData(nodeID + ":AssetIntegrityMitigatedDistribution");
            availabilityImpactDistribution = (ChartPointIndexer)GraphUtil.GetDistributionData(nodeID + ":AssetAvailabilityMitigatedDistribution");
            accountabilityImpactDistribution = (ChartPointIndexer)GraphUtil.GetDistributionData(nodeID + ":AssetAccountabilityMitigatedDistribution");

            //Clear charts where necessary 
            if (likelihoodDistribution == null || confidentialityImpactDistribution == null || integrityImpactDistribution == null || availabilityImpactDistribution == null || accountabilityImpactDistribution == null)
            {
                lblConfidentialityXValue.Text = "N/A";
                panelConfidentialityLikelihood.BackColor = GetHeatmapColor(-1, 255);
                lblConfidentialityXStatus.Text = GraphUtil.GetRiskStatusFromValue(0);

                chartConfidentialityBubble.Series[0].Points.Clear();
                chartConfidentialityBubble.Refresh();
                lblConfidentialityYValue.Text = "N/A";
                lblConfidentialityRiskValue.Text = "N/A";
                panelConfidentialityImpact.BackColor = GetHeatmapColor(-1, 255);
                lblConfidentialityXStatus.Text = GraphUtil.GetRiskStatusFromValue(0);
                panelConfidentialityRisk.BackColor = GetHeatmapColor(-1, 255);
                lblConfidentialityRiskStatus.Text = GraphUtil.GetRiskStatusFromValue(0);

                chartIntegrityBubble.Series[0].Points.Clear();
                chartIntegrityBubble.Refresh();
                lblIntegrityYValue.Text = "N/A";
                lblIntegrityRiskValue.Text = "N/A";
                panelIntegrityImpact.BackColor = GetHeatmapColor(-1, 255);
                lblIntegrityYStatus.Text = GraphUtil.GetRiskStatusFromValue(0);
                panelIntegrityRisk.BackColor = GetHeatmapColor(-1, 255);
                lblIntegrityRiskStatus.Text = GraphUtil.GetRiskStatusFromValue(0);

                chartAvailabilityBubble.Series[0].Points.Clear();
                chartAvailabilityBubble.Refresh();
                lblAvailabilityYValue.Text = "N/A";
                lblAvailabilityRiskValue.Text = "N/A";
                panelAvailabilityImpact.BackColor = GetHeatmapColor(-1, 255);
                lblAvailabilityYStatus.Text = GraphUtil.GetRiskStatusFromValue(0);
                panelAvailabilityRisk.BackColor = GetHeatmapColor(-1, 255);
                lblAvailabilityRiskStatus.Text = GraphUtil.GetRiskStatusFromValue(0);

                chartAccountabilityBubble.Series[0].Points.Clear();
                chartAccountabilityBubble.Refresh();
                lblAccountabilityYValue.Text = "N/A";
                lblAccountabilityRiskValue.Text = "N/A";
                panelAccountabilityImpact.BackColor = GetHeatmapColor(-1, 255);
                lblAccountabilityYStatus.Text = GraphUtil.GetRiskStatusFromValue(0);
                panelAccountabilityRisk.BackColor = GetHeatmapColor(-1, 255);
                lblAccountabilityRiskStatus.Text = GraphUtil.GetRiskStatusFromValue(0);
                return;
            }



            for (int i = 0; i < 101; i++)
            {
                for (int j = 0; j < 101; j++)
                {
                    likelihoodValue = (int)likelihoodDistribution[i].YValues[0];
                    confidentialityValue = (int)confidentialityImpactDistribution[j].YValues[0];
                    integrityValue = (int)integrityImpactDistribution[j].YValues[0];
                    availabilityValue = (int)availabilityImpactDistribution[j].YValues[0];
                    accountabilityValue = (int)accountabilityImpactDistribution[j].YValues[0];

                    if (likelihoodValue > 0)
                    {
                        if (likelihoodValue > likelihoodHighestValue)
                            likelihoodHighestValue = likelihoodValue;

                        if (confidentialityValue > 0)
                        {
                            if (confidentialityValue > confidentialityHighestValue)
                                confidentialityHighestValue = confidentialityValue;

                            if (confidentialityValue + likelihoodValue > confidentialityHighestIntensity)
                                confidentialityHighestIntensity = confidentialityValue + likelihoodValue;
                        }

                        if (integrityValue > 0)
                        {
                            if (integrityValue > integrityHighestValue)
                                integrityHighestValue = integrityValue;

                            if (integrityValue + likelihoodValue > integrityHighestIntensity)
                                integrityHighestIntensity = integrityValue + likelihoodValue;
                        }

                        if (availabilityValue > 0)
                        {
                            if (availabilityValue > availabilityHighestValue)
                                availabilityHighestValue = availabilityValue;

                            if (availabilityValue + likelihoodValue > availabilityHighestIntensity)
                                availabilityHighestIntensity = availabilityValue + likelihoodValue;
                        }

                        if (accountabilityValue > 0)
                        {
                            if (accountabilityValue > accountabilityHighestValue)
                                accountabilityHighestValue = accountabilityValue;

                            if (accountabilityValue + likelihoodValue > accountabilityHighestIntensity)
                                accountabilityHighestIntensity = accountabilityValue + likelihoodValue;
                        }
                    }
                }
            }

 
            int confidentialityPoints = 0;
            int integrityPoints = 0;
            int availabilityPoints = 0;
            int accountabilityPoints = 0;

            chartConfidentialityBubble.BeginUpdate();
            chartIntegrityBubble.BeginUpdate();
            chartAvailabilityBubble.BeginUpdate();
            chartAccountabilityBubble.BeginUpdate();

            chartConfidentialityBubble.Series[0].Points.Clear();
            chartIntegrityBubble.Series[0].Points.Clear();
            chartAvailabilityBubble.Series[0].Points.Clear();
            chartAccountabilityBubble.Series[0].Points.Clear();

            for (int x = 0; x < 101; x++)
            {
                likelihoodValue = likelihoodDistribution[x].YValues[0];
                double likelihoodNormalisedValue = ConvertRange(likelihoodValue, 0, likelihoodHighestValue, 0, 50);

                for (int y = 0; y < 101; y++)
                {
                    confidentialityValue = (int)confidentialityImpactDistribution[y].YValues[0];
                    integrityValue = (int)integrityImpactDistribution[y].YValues[0];
                    availabilityValue = (int)availabilityImpactDistribution[y].YValues[0];
                    accountabilityValue = (int)accountabilityImpactDistribution[y].YValues[0];

                    //Confidentiality
                    if (likelihoodValue > 0 && confidentialityValue > 0)
                    {
                        double confidentialityNormalisedValue = ConvertRange(confidentialityValue, 0, confidentialityHighestValue, 0, 50);
                                                        
                        double intensity = (likelihoodValue + confidentialityValue);
                        confidentialityRiskValue = (likelihoodNormalisedValue * confidentialityNormalisedValue);
                        if (confidentialityRiskValue > confidentialityHighestRiskValue)
                        {
                            confidentialityHighestRiskValue = confidentialityRiskValue;
                            confidentialityX = x;
                            confidentialityY = y;
                        }

                        double normalisedSize = ConvertRange(intensity, 0, confidentialityHighestIntensity, 0, MAX_SIZE);
                        double normalisedIntensity = ConvertRange(intensity, 0, confidentialityHighestIntensity, 0, 255);

                        int colorIndex = x * y;
                        double normalisedcolorIndex = ConvertRange(colorIndex, 0, 10000, 0, 100);

                        if (normalisedIntensity > 0 && normalisedSize > 0)
                        {
                            this.InvokeIfNeed(() =>
                            {
                                chartConfidentialityBubble.Series[0].Points.Add(x, y, normalisedSize);
                                chartConfidentialityBubble.Series[0].Styles[confidentialityPoints].Interior = new BrushInfo(Syncfusion.Drawing.GradientStyle.PathEllipse, GetHeatmapColor(normalisedcolorIndex, 0), GetHeatmapColor(normalisedcolorIndex, normalisedIntensity));
                                chartConfidentialityBubble.Series[0].Styles[confidentialityPoints].Border.Color = Color.Transparent;
                               
                            });
                            confidentialityPoints++;
                        } 
                    }

                    //Integrity
                    if (likelihoodValue > 0 && integrityValue > 0)
                    {
                        double integrityNormalisedValue = ConvertRange(integrityValue, 0, integrityHighestValue, 0, 50);

                        double intensity = (likelihoodValue + integrityValue);
                        integrityRiskValue = (likelihoodNormalisedValue * integrityNormalisedValue);
                        if (integrityRiskValue > integrityHighestRiskValue)
                        {
                            integrityHighestRiskValue = integrityRiskValue;
                            integrityX = x;
                            integrityY = y;
                        }

                        double normalisedSize = ConvertRange(intensity, 0, integrityHighestIntensity, 0, MAX_SIZE);
                        double normalisedIntensity = ConvertRange(intensity, 0, integrityHighestIntensity, 0, 255);

                        double colorIndex = x * y;
                        double normalisedcolorIndex = ConvertRange(colorIndex, 0, 10000, 0, 100);

                        if (normalisedIntensity > 0 && normalisedSize > 0)
                        {
                            this.InvokeIfNeed(() =>
                            {
                                chartIntegrityBubble.Series[0].Points.Add(x, y, normalisedSize);
                                chartIntegrityBubble.Series[0].Styles[integrityPoints].Interior = new BrushInfo(Syncfusion.Drawing.GradientStyle.PathEllipse, GetHeatmapColor(normalisedcolorIndex, 0), GetHeatmapColor(normalisedcolorIndex, normalisedIntensity));
                                chartIntegrityBubble.Series[0].Styles[integrityPoints].Border.Color = Color.Transparent;
                                
                            });
                            integrityPoints++;
                        }
                            

                    }

                    //Availability
                    if (likelihoodValue > 0 && availabilityValue > 0)
                    {
                        double availabilityNormalisedValue = ConvertRange(availabilityValue, 0, availabilityHighestValue, 0, 50);

                        double intensity = (likelihoodValue + availabilityValue);
                        availabilityRiskValue = (likelihoodNormalisedValue * availabilityNormalisedValue);
                        if (availabilityRiskValue > availabilityHighestRiskValue)
                        {
                            availabilityHighestRiskValue = availabilityRiskValue;
                            availabilityX = x;
                            availabilityY = y;
                        }

                        double normalisedSize = ConvertRange(intensity, 0, availabilityHighestIntensity, 0, MAX_SIZE);
                        double normalisedIntensity = ConvertRange(intensity, 0, availabilityHighestIntensity, 0, 255);

                        double colorIndex = x * y;
                        double normalisedcolorIndex = ConvertRange(colorIndex, 0, 10000, 0, 100);

                        if (normalisedIntensity > 0 && normalisedSize > 0)
                        {
                            this.InvokeIfNeed(() =>
                            {
                                chartAvailabilityBubble.Series[0].Points.Add(x, y, normalisedSize);
                                chartAvailabilityBubble.Series[0].Styles[availabilityPoints].Interior = new BrushInfo(Syncfusion.Drawing.GradientStyle.PathEllipse, GetHeatmapColor(normalisedcolorIndex, 0), GetHeatmapColor(normalisedcolorIndex, normalisedIntensity));
                                chartAvailabilityBubble.Series[0].Styles[availabilityPoints].Border.Color = Color.Transparent;
                               
                            });
                            availabilityPoints++;
                        }
                            
                    }

                    //Accountability
                    if (likelihoodValue > 0 && accountabilityValue > 0)
                    {
                        double accountabilityNormalisedValue = ConvertRange(accountabilityValue, 0, accountabilityHighestValue, 0, 50);

                        double intensity = (likelihoodValue + accountabilityValue);
                        accountabilityRiskValue = (likelihoodNormalisedValue * accountabilityNormalisedValue);
                        if (accountabilityRiskValue > accountabilityHighestRiskValue)
                        {
                            accountabilityHighestRiskValue = accountabilityRiskValue;
                            accountabilityX = x;
                            accountabilityY = y;
                        }

                        double normalisedSize = ConvertRange(intensity, 0, accountabilityHighestIntensity, 0, MAX_SIZE);
                        double normalisedIntensity = ConvertRange(intensity, 0, accountabilityHighestIntensity, 0, 255);

                        int colorIndex = x * y;
                        double normalisedcolorIndex = ConvertRange(colorIndex, 0, 10000, 0, 100);

                        if (normalisedIntensity > 0 && normalisedSize > 0)
                        {
                            this.InvokeIfNeed(() =>
                            {
                                    
                                chartAccountabilityBubble.Series[0].Points.Add(x, y, normalisedSize);
                                chartAccountabilityBubble.Series[0].Styles[accountabilityPoints].Interior = new BrushInfo(Syncfusion.Drawing.GradientStyle.PathEllipse, GetHeatmapColor(normalisedcolorIndex, 0), GetHeatmapColor(normalisedcolorIndex, normalisedIntensity));
                                chartAccountabilityBubble.Series[0].Styles[accountabilityPoints].Border.Color = Color.Transparent;
                                
                            });
                            accountabilityPoints++;
                        }
                    }
                }
            }
            chartConfidentialityBubble.EndUpdate();
            chartIntegrityBubble.EndUpdate();
            chartAvailabilityBubble.EndUpdate();
            chartAccountabilityBubble.EndUpdate();

            this.InvokeIfNeed(() =>
            {
                double tempValue = 0;
                List<String> GUIDs = null;
                lblConfidentialityXStatus.Text = GraphUtil.GetRiskStatusFromValue(confidentialityX);
                lblConfidentialityXValue.Text = (confidentialityX).ToString();

                ////Get the highest Likelihood score from the parent nodes
                //GUIDs = GraphUtil.GetParentVulnerabilityNodes(mainForm._selected_node_id);
                //foreach (String GUID in GUIDs)
                //{
                //    double tempAverage = GraphUtil.GetAverageNodeScore(GUID + ":likelihoodScore");
                //    if (tempAverage > tempValue)
                //        confidentialityX = tempAverage;
                //}

                //confidentialityX = GraphUtil.GetAssetNodeLikelihoodScore(mainForm._selected_node_id);
                confidentialityX = GraphUtil.GetAverageNodeScore(nodeID + ":assetLikelihoodScore");

                lblConfidentialityXValue.Text = confidentialityX.ToString("F2");
                panelConfidentialityLikelihood.BackColor = GetHeatmapColor((int)confidentialityX, 255);

                lblConfidentialityYStatus.Text = GraphUtil.GetRiskStatusFromValue(confidentialityY);
                confidentialityY = GraphUtil.GetAverageNodeScore(mainForm._selected_node_id + ":assetConfidentialityMitigatedScore");
                lblConfidentialityYValue.Text = confidentialityY.ToString("F2");
                panelConfidentialityImpact.BackColor = GetHeatmapColor((int)confidentialityY, 255);

                lblConfidentialityRiskStatus.Text = GraphUtil.GetRiskStatusFromValue(ConvertRange((int)confidentialityX * (int)confidentialityY, 0, 10000, 0, 100));
                lblConfidentialityRiskValue.Text = (confidentialityX * confidentialityY).ToString("F0");
                panelConfidentialityRisk.BackColor = GetHeatmapColor((int)ConvertRange(confidentialityX * confidentialityY, 0, 10000, 0, 100), 255);
            });

            this.InvokeIfNeed(() =>
            {
                integrityX = confidentialityX;
                //lblIntegrityXStatus.Text = GraphUtil.GetRiskStatusFromValue(integrityX);
                //lblIntegrityXValue.Text = integrityX.ToString("F");
                //panelIntegrityLikelihood.BackColor = GetHeatmapColor((int)integrityX, 255);

                lblIntegrityYStatus.Text = GraphUtil.GetRiskStatusFromValue(integrityY);
                integrityY = GraphUtil.GetAverageNodeScore(mainForm._selected_node_id + ":assetIntegrityMitigatedScore");
                lblIntegrityYValue.Text = (integrityY).ToString("F2");
                panelIntegrityImpact.BackColor = GetHeatmapColor((int)integrityY, 255);

                lblIntegrityRiskStatus.Text = GraphUtil.GetRiskStatusFromValue(ConvertRange((int)integrityX * (int)integrityY, 0, 10000, 0, 100));
                lblIntegrityRiskValue.Text = (integrityX * integrityY).ToString("F0");
                panelIntegrityRisk.BackColor = GetHeatmapColor((int)ConvertRange(integrityX * integrityY, 0, 10000, 0, 100), 255);
            });

            this.InvokeIfNeed(() =>
            {
                availabilityX = confidentialityX;
                //lblAvailabilityXStatus.Text = GraphUtil.GetRiskStatusFromValue(availabilityX);
                //lblAvailabilityXValue.Text = availabilityX.ToString("F2");
                //panelAvailabilityLikelihood.BackColor = GetHeatmapColor((int)availabilityX, 255);

                availabilityY = GraphUtil.GetAverageNodeScore(mainForm._selected_node_id + ":assetAvailabilityMitigatedScore");
                lblAvailabilityYStatus.Text = GraphUtil.GetRiskStatusFromValue(availabilityY);
                lblAvailabilityYValue.Text = (availabilityY).ToString("F2");
                panelAvailabilityImpact.BackColor = GetHeatmapColor((int)availabilityY, 255);

                lblAvailabilityRiskStatus.Text = GraphUtil.GetRiskStatusFromValue(ConvertRange((int)availabilityX * (int)availabilityY, 0, 10000, 0, 100));
                lblAvailabilityRiskValue.Text = (availabilityX * availabilityY).ToString("F0");
                panelAvailabilityRisk.BackColor = GetHeatmapColor((int)ConvertRange(availabilityX * availabilityY, 0, 10000, 0, 100), 255);
            });

            this.InvokeIfNeed(() =>
            {
                accountabilityX = confidentialityX;
                //lblAccountabilityXStatus.Text = GraphUtil.GetRiskStatusFromValue(accountabilityX);
                //lblAccountabilityXValue.Text = accountabilityX.ToString("F2");
                //panelAccountabilityLikelihood.BackColor = GetHeatmapColor((int)accountabilityX, 255);

                accountabilityY = GraphUtil.GetAverageNodeScore(mainForm._selected_node_id + ":assetAccountabilityMitigatedScore");
                lblAccountabilityYStatus.Text = GraphUtil.GetRiskStatusFromValue(accountabilityY);
                lblAccountabilityYValue.Text = (accountabilityY).ToString("F2");
                panelAccountabilityImpact.BackColor = GetHeatmapColor((int)accountabilityY, 255);

                lblAccountabilityRiskStatus.Text = GraphUtil.GetRiskStatusFromValue(ConvertRange((int)accountabilityX * (int)accountabilityY, 0, 10000, 0, 100));
                lblAccountabilityRiskValue.Text = (accountabilityX * accountabilityY).ToString("F0");
                panelAccountabilityRisk.BackColor = GetHeatmapColor((int)ConvertRange(accountabilityX * accountabilityY, 0, 10000, 0, 100), 255);
            });

        }

        private void lbAssets_SelectedIndexChanged(object sender, EventArgs e)
        {
            CalculateHeatMaps(assetNodeList[lbAssets.SelectedIndex]);
            if (lbAssets.SelectedIndex > -1) btnFind.Enabled = true;
            else
                btnFind.Enabled = false;
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            mainForm.SelectNodeonGraph(assetNodeList[lbAssets.SelectedIndex]);
        }
    }
}
