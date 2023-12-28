using Newtonsoft.Json.Linq;
using Syncfusion.Windows.Forms.Grid;
using Syncfusion.Windows.Forms.Maps;
using Syncfusion.WinForms.ListView.Enums;
using Syncfusion.XlsIO.Parser.Biff_Records.PivotTable;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using Troschuetz.Random;
using Syncfusion.Windows.Forms.Chart;
using System.Runtime.InteropServices;
using Syncfusion.Windows.Forms.Diagram;
using System.Linq;
using Syncfusion.Windows.Forms.Tools.Win32API;
using CefSharp.Web;
using Newtonsoft.Json;
using System.Xml.Linq;

namespace CyConex
{
    public partial class RangeSelectForm : Form
    {
        public class DistributionObject
        {
            public string DistributionName { get; set; }
            public string Value1 { get; set; }
            public string Value2 { get; set; }
            public string Value3 { get; set; }
            public string NodeType { get; set; }
            

        }

        public JArray data = new JArray();

        public string Text1 { get; set; }

        public int SelectedRangeMax { get { if (distributionName != "SpecificValue") return rangeSlider.SliderMax; else return valueSlider.Value; } set { valueSlider.Value = value; rangeSlider.SliderMax = value; } }
        public int SelectedRangeMin { get { if (distributionName != "SpecificValue") return rangeSlider.SliderMin; else return valueSlider.Value; } set { rangeSlider.SliderMin = value; } }
        public int RangeLimitMax { get { return rangeSlider.Maximum; } set { valueSlider.Maximum = value; rangeSlider.Maximum = value; } }
        public int RangeLimitMin { get { return rangeSlider.Minimum; } set { valueSlider.Minimum = value; rangeSlider.Minimum = value; } }

        public JArray DistributionParameters { get; set; }

        public int Range1Maximum { set { rangeSlider.Maximum = value; } }

        private object currentDistribution;

        public string distributionName;
        public string distributionType { get; set; }

        private SortedList<string, Type> distributions;

        private SortedList<string, Type> generators;

        private Type typeDistribution;

        private Type typeGenerator;

        private decimal Mu = 0;
        private decimal Alpha = 0;
        private decimal Beta = 100;
        private decimal StandardDev = 10;
        private decimal Gamma = 10;
        private decimal SpecificValue = 0;
        double GraphMax = 0.0;
        double GraphMin = 99999.0;
        bool Initializing = true;
        private double steps = 25;
        private double lastMax = 0;
        private double lastMin = 0;
        private bool UpdatingRange = false;
        public string elementType = "node";


        DistributionObject DistsObject = new DistributionObject();

        public RangeSelectForm()
        {
            InitializeComponent();
        }

        private void NodeDetailsForm_Load(object sender, EventArgs e)
        {
            if (data == null) return;

            label1.Text = Text1.ToString();
            gridControl1.ColCount = rangeSlider.Maximum;
            gridControl1.DefaultRowHeight = gridControl1.Height;
            gridControl1.DefaultColWidth = gridControl1.Width / gridControl1.ColCount;

            gridControl2.ColCount = gridControl1.ColCount;
            gridControl2.DefaultRowHeight = gridControl2.Height;
            gridControl2.DefaultColWidth = gridControl1.DefaultColWidth;

            InitializeRangeData();
            Initializing = false;
        }

        static bool ContainsField(JToken jsonToken, string fieldName)
        {
            if (jsonToken == null || jsonToken.Type != JTokenType.Object)
            {
                return false;
            }

            JObject jobject = jsonToken as JObject;

            return jobject.ContainsKey(fieldName);
        }

        private void InitializeRangeData()
        {
            int col = 1;

            for (int i = data.Count - 1; i > -1; i--)
            {
                var item = data[i];
                if (ContainsField(item, "impact"))  // Detect if Node or Edge
                {
                    //Node
                    string tempImpact = item["impact"].ToString();
                    gridControl1.Model[1, col].CellValue = tempImpact;
                    gridControl1.Model[1, col].HorizontalAlignment = GridHorizontalAlignment.Center;
                    gridControl1.Model[1, col].WrapText = true;
                    if ((int)item["value"] < GraphMin)
                        GraphMin = (int)item["value"];
                    if ((int)item["value"] > GraphMax)
                        GraphMax = (int)item["value"];
                    if (ContainsField(item, "description"))
                    {
                        gridControl2.Model[1, col].CellValue = item["description"].ToString();
                        gridControl2.Model[1, col].HorizontalAlignment = GridHorizontalAlignment.Center;
                        gridControl2.Model[1, col].WrapText = true;
                    }

                    }
                else
                {
                    //Edge
                    string tempValue = item["value"].ToString();
                    gridControl1.Model[1, col].CellValue = tempValue;
                    gridControl1.Model[1, col].HorizontalAlignment = GridHorizontalAlignment.Center;
                    gridControl1.Model[1, col].WrapText = true;
                    if ((double)item["value"] < GraphMin)
                        GraphMin = (double)item["value"];
                    if ((double)item["value"] > GraphMax)
                        GraphMax = (double)item["value"];
                    if(ContainsField(item, "description"))
                    {
                        gridControl2.Model[1, col].CellValue = item["description"].ToString();
                        gridControl2.Model[1, col].HorizontalAlignment = GridHorizontalAlignment.Center;
                        gridControl2.Model[1, col].WrapText = true;
                    }

                }

                col++;
            }

            ExtractDistributionParamemeters();
            if (DistsObject.DistributionName != null)
            {
                if (DistsObject.DistributionName == "DistNormal")
                {
                    rbDistNormal.Checked = true;

                    UpdateRangeData();
                    Mu = (int)decimal.Parse(DistsObject.Value1);
                    StandardDev = int.Parse(DistsObject.Value2);
                    numStandardDev.Value = StandardDev;
                    distributionType = "Distribution: Normal";
                    GenerateDistributionButtonClicked();
                }
                else if (DistsObject.DistributionName == "DistUniform")
                {
                    rbDistUniform.Checked = true;

                    UpdateRangeData();
                    Alpha = (int)decimal.Parse(DistsObject.Value2);
                    Beta = (int)decimal.Parse(DistsObject.Value1);
                    distributionType = "Distribution: Uniform";
                    GenerateDistributionButtonClicked();
                }
                else if (DistsObject.DistributionName == "DistTriangle")
                {
                    rbDistTriangle.Checked = true;

                    UpdateRangeData();
                    Beta = (int)decimal.Parse(DistsObject.Value1);
                    Alpha = (int)decimal.Parse(DistsObject.Value2);
                    Gamma = (int)decimal.Parse(DistsObject.Value3);
                    numTriangleGamma.Value = Gamma;
                    distributionType = "Distribution: Triangle";
                    GenerateDistributionButtonClicked();
                }
                //else if (DistsObject.DistributionName == "DistPareto")
                //{
                //    rbDistPareto.Checked = true;

                //    UpdateRangeData();
                //    Alpha = int.Parse(DistsObject.Value1);
                //    numParetoAlpha.Value = decimal.Parse(DistsObject.Value2);
                //    GenerateDistributionButtonClicked();
                //}
                //else if (DistsObject.DistributionName == "DistParetoRight")
                //{
                //    rbDistParetoRight.Checked = true;

                //    UpdateRangeData();
                //    Alpha = int.Parse(DistsObject.Value1);
                //    GenerateGraph("DistParetoRight");
                //    GenerateDistributionButtonClicked();

                //}
                else if (DistsObject.DistributionName == "SpecificValue")
                {
                    rbValue.Checked = true;

                    SpecificValue = decimal.Parse(DistsObject.Value1);
                    distributionType = "Fixed Value";
                    GenerateDistributionButtonClicked();

                }
            }
            else
            {
                rbDistNormal.Checked = true;
                GenerateDistributionButtonClicked();
            }


        }


        private void rangeSlider1_DoubleClick(object sender, EventArgs e)
        {
            if (rangeSlider.SliderMin > rangeSlider.Minimum)
                rangeSlider.SliderMin = rangeSlider.SliderMin - 1;
        }

        private void btOK_Click(object sender, EventArgs e)
        {

        }

        private void rbDistNormal_CheckedChanged(object sender, EventArgs e)
        {

        }
        private void GenerateDistributionButtonClicked()
        {
            showRangeSlider();
            panelDetailNormal.Visible = false;
            panelDetailGamma.Visible = false;
            panelDetailExponential.Visible = false;
            panelDetailTraiangle.Visible = false;
            panelPareto.Visible = false;

            UpdateRangeData();

            if (rbDistNormal.Checked)
            {
                panelDetailNormal.Visible = true;
                distributionType = "Distribution: Normal";

                GenerateGraph("DistNormal");
            }

            if (rbDistUniform.Checked)
            {
                distributionType = "Distribution: Uniform";
                GenerateGraph("DistUniform");
            }

            if (rbDistTriangle.Checked)
            {
                panelDetailTraiangle.Visible = true;
                distributionType = "Distribution: Triangle";

                GenerateGraph("DistTriangle");
            }

            //if (rbDistPareto.Checked)
            //{
            //    panelPareto.Visible = true;
            //    GenerateGraph("DistPareto");
            //}

            //if (rbDistParetoRight.Checked)
            //{
            //    GenerateGraph("DistParetoRight");
            //}

            if (rbValue.Checked)
            {
                distributionType = "Fixed Value";

                GenerateGraph("SpecificValue");

            }

        }

        private void GenerateGraph(string ChartType)
        {
            distributionName = ChartType;
            if (UpdatingRange == true) // Prevent incorrect updates
                return;

            if (ChartType == "SpecificValue")
            {
                UpdateDistributionParameters(ChartType, SpecificValue.ToString(), null, null, elementType);
                return;
            }

            // Now build example distribution
            Assembly assembly = Assembly.LoadFrom("Troschuetz.Random.dll");
            Type[] types = assembly.GetTypes();

            this.distributions = new SortedList<string, Type>(types.Length);
            this.generators = new SortedList<string, Type>(types.Length);

            for (int index = 0; index < types.Length; index++)
            {
                if (types[index].FullName == "Troschuetz.Random.Distribution")
                {
                    this.typeDistribution = types[index];
                }
                else if (types[index].FullName == "Troschuetz.Random.Generator")
                {
                    this.typeGenerator = types[index];
                }
                else if (types[index].IsSubclassOf(typeof(Distribution)))
                {// The type inherits from Distribution type.
                    this.distributions.Add(types[index].Name, types[index]);
                }
                else if (types[index].IsSubclassOf(typeof(Generator)))
                {// The type inherits from Generator type.
                    this.generators.Add(types[index].Name, types[index]);
                }
            }
            this.distributions.TrimExcess();
            this.generators.TrimExcess();


            if (ChartType == "DistNormal")  // Normal Distribution 
            {
                this.currentDistribution = null;
                this.currentDistribution = Activator.CreateInstance(this.distributions["NormalDistribution"]);

                PropertyInfo propertyInfo = this.currentDistribution.GetType().GetProperty("Mu");
                propertyInfo.SetValue(this.currentDistribution, (double)Mu, null);

                propertyInfo = this.currentDistribution.GetType().GetProperty("Sigma");
                propertyInfo.SetValue(this.currentDistribution, (double)StandardDev, null);

                panelChart.Visible = true;

                UpdateDistributionParameters(ChartType, Mu.ToString(), StandardDev.ToString(), null, elementType);

            }

            if (ChartType == "DistUniform") //Uniform Distribution
            {
                this.currentDistribution = null;
                this.currentDistribution = Activator.CreateInstance(this.distributions["ContinuousUniformDistribution"]);

                PropertyInfo propertyInfo = this.currentDistribution.GetType().GetProperty("Beta");
                propertyInfo.SetValue(this.currentDistribution, (Int32)Beta, null);

                propertyInfo = this.currentDistribution.GetType().GetProperty("Alpha");
                propertyInfo.SetValue(this.currentDistribution, (Int32)Alpha, null);

                panelChart.Visible = true;

                UpdateDistributionParameters(ChartType, Beta.ToString(), Alpha.ToString(), null, elementType);
            }

            if (ChartType == "DistTriangle") //Traiangle Distribution
            {
                if (Alpha == Beta)
                {
                    panelChart.Visible = false;   // Hide the chart if not a true distribution 
                    return;
                }
                else { panelChart.Visible = true; }

                this.currentDistribution = null;
                this.currentDistribution = Activator.CreateInstance(this.distributions["TriangularDistribution"]);

                PropertyInfo propertyInfo = this.currentDistribution.GetType().GetProperty("Beta");
                propertyInfo.SetValue(this.currentDistribution, (Int32)Beta, null);

                propertyInfo = this.currentDistribution.GetType().GetProperty("Gamma");
                propertyInfo.SetValue(this.currentDistribution, (Int32)numTriangleGamma.Value, null);

                propertyInfo = this.currentDistribution.GetType().GetProperty("Alpha");
                propertyInfo.SetValue(this.currentDistribution, (Int32)Alpha, null);

                UpdateDistributionParameters(ChartType, Beta.ToString(), Alpha.ToString(), numTriangleGamma.Value.ToString(), elementType);
            }

            if (ChartType == "DistPareto" || ChartType == "DistParetoRight") //Pareto Distribution
            {
                if (Alpha == Beta)
                {
                    panelChart.Visible = false;   // Hide the chart if not a true distribution 
                    return;
                }
                else { panelChart.Visible = true; }


                this.currentDistribution = null;
                this.currentDistribution = Activator.CreateInstance(this.distributions["ExponentialDistribution"]);

                //PropertyInfo propertyInfo = this.currentDistribution.GetType().GetProperty("Lambda");
                //propertyInfo.SetValue(this.currentDistribution, (Int32)Alpha, null);

                PropertyInfo propertyInfo = this.currentDistribution.GetType().GetProperty("Lambda");
                propertyInfo.SetValue(this.currentDistribution, (Int32)numParetoAlpha.Value, null);

                UpdateDistributionParameters(ChartType, Alpha.ToString(), numParetoAlpha.Value.ToString(), null, elementType);
            }

            //Generate the samples

            Distribution distribution = (Distribution)this.currentDistribution;
            double[] samples = new double[10000];
            for (int index = 0; index < samples.Length; index++)
            {
                samples[index] = distribution.NextDouble();
            }

            //Determine sum, minimum, maximum and display the last two together with a computed mean value.
            double sum = 0, minimum = double.MaxValue, maximum = double.MinValue;

            for (int index = 0; index < samples.Length; index++)
            {
                sum += samples[index];
                if (samples[index] > maximum)
                    maximum = samples[index];
                if (samples[index] < minimum)
                    minimum = samples[index];
            }
            double mean = sum / samples.Length;
            double variance = 0.0;
            for (int index = 0; index < samples.Length; index++)
            {
                variance += Math.Pow(samples[index] - mean, 2);
            }
            variance /= samples.Length;

            //Compute the range of histogram and generate the histogram values.
            double range = maximum - minimum;
            double[] x, y;
            //if (ChartType == "DistNormal") 
            //    steps = 25;

            if (range == 0 || steps == 0) // cannot occur in case of user defined histogram bounds
            {
                //Samples are all the same, so use a fixed histogram.
                x = new double[] { minimum, minimum + double.Epsilon };
                y = new double[] { samples.Length, 0 };

            }
            else
            {
                x = new double[(int)steps];
                y = new double[(int)steps];

                // Compute the histogram intervals (minimum bound of each interval is the x-value of graph points).
                // The last graph point represents the maximum bound of the last histogram interval.
                for (int index = 0; index < x.Length - 1; index++)
                {
                    x[index] = minimum + range / (double)steps * index;
                }
                x[x.Length - 1] = maximum;

                // Iterate over samples and increase the histogram interval they lie inside.
                int samplesUsed = (int)10000; //samples = 10000
                for (int index = 0; index < samples.Length; index++)
                {
                    if (samples[index] < minimum || samples[index] > maximum)
                    {// If user specified own histogram bounds, ignore samples that lie outside.
                        samplesUsed--;
                    }
                    else if (samples[index] == maximum)
                    {// Maximum is part of last histogram interval
                        y[y.Length - 2]++;
                    }
                    else
                    {
                        y[(int)Math.Floor((samples[index] - minimum) / range * (double)steps)]++;
                    }


                }

                // Relate the number of samples inside each histogram interval to the overall number of samples
                for (int index = 0; index < y.Length - 1; index++)
                {
                    y[index] = y[index] / samplesUsed * (double)steps;
                }

                // Assign the y-value of the last but one graph point to the last one, so that the minimum and
                //   maximum bound of the last histogram interval share the same y-value
                y[y.Length - 1] = y[y.Length - 2];


            }

            PlotChart(x, y, ChartType);
        }

        private void PlotChart(double[] x, double[] y, string ChartType)
        {
            var series = new ChartSeries();
            series.Resolution = 0D;
            series.StackingGroup = "Default Group";
            series.Style.Border.Color = System.Drawing.Color.Transparent;
            series.Style.Border.Width = 2F;
            series.Style.DisplayShadow = true;
            series.Style.DrawTextShape = false;
            series.Type = Syncfusion.Windows.Forms.Chart.ChartSeriesType.SplineArea;



            if (ChartType == "DistPareto")
                // x = RescaleArray(x, Alpha, Beta);

                if (ChartType == "DistParetoRight")
                {
                    //x = RescaleArray(x, Alpha, Beta);
                    Array.Reverse(y);
                }


            for (int i = 0; i < x.Length; i++)
            {
                double X = x[i];
                double Y = y[i];

                if (elementType == "edge") // Edge 
                {
                    X = x[i] / 100;
                    Y = y[i];
                    if (ChartType == "DistNormal" || ChartType == "DistTriangle" || ChartType == "DistPareto" || ChartType == "DistParetoRight")
                        series.Points.Add(Math.Round(X, 2), Y);

                    if (ChartType == "DistUniform")
                        series.Points.Add(Math.Round(X, 2), Math.Round(Y, 0));
                }
                else if ((elementType == "node"))
                {
                    if (ChartType == "DistNormal" || ChartType == "DistTriangle" || ChartType == "DistPareto" || ChartType == "DistParetoRight")
                        series.Points.Add(Math.Round(X), Y);

                    if (ChartType == "DistUniform")
                        series.Points.Add(Math.Round(X), Math.Round(Y, 0));
                }
            }

            var MinMax = new MinMaxInfo(0, 100, steps);
            if (elementType == "edge")
            {
                MinMax = new MinMaxInfo(0, 1, 0.1); // Reduce 
            }

            chartDistribution.PrimaryXAxis.SetRange(MinMax);
            chartDistribution.PrimaryYAxis.ValueType = Syncfusion.Windows.Forms.Chart.ChartValueType.Custom;
            chartDistribution.Series.Clear();
            chartDistribution.Series.Add(series);
            series.XAxis.AutoSize = false;
            series.XAxis.SetRange(MinMax);
        }



        private string FormatDouble(double value)
        {
            if (Math.Abs(value) >= 1000000 || (Math.Abs(value) < 0.001 && value != 0))
                return value.ToString("0.###e+0");
            else
                return value.ToString("0.###");
        }

        private void rbDistLeft_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void rbDistNormal_Click(object sender, EventArgs e)
        {
            GenerateDistributionButtonClicked();
        }

        private void rbDistLeft_Click(object sender, EventArgs e)
        {
            GenerateDistributionButtonClicked();
        }

        private void rbDistRight_Click(object sender, EventArgs e)
        {
            GenerateDistributionButtonClicked();
        }

        private void rbDistTriangle_Click(object sender, EventArgs e)
        {
            GenerateDistributionButtonClicked();
        }

        private void rbDistUniform_Click(object sender, EventArgs e)
        {
            GenerateDistributionButtonClicked();
        }

        private void rbDistExponential_Click(object sender, EventArgs e)
        {
            GenerateDistributionButtonClicked();
        }

        private void CheckRangeSliderPositions()
        {
            if (Initializing == true)
                return;

            if (rangeSlider.SliderMax == rangeSlider.SliderMin)
            {
                if (rangeSlider.SliderMin > 1)
                    rangeSlider.SliderMin = rangeSlider.SliderMax - 1;
                else
                    rangeSlider.SliderMax = rangeSlider.SliderMax + 1;
            }
        }


        private void rangeSlider1_ValueChanged(object sender, EventArgs e)
        {
            CheckRangeSliderPositions();

            if (Initializing == true)
                return;


            if (lastMax == rangeSlider.SliderMax && lastMin == rangeSlider.SliderMin)
                return; lastMax = rangeSlider.SliderMax; lastMin = rangeSlider.SliderMin;

            UpdateRangeData();

            if (rbDistNormal.Checked)
                GenerateGraph("DistNormal");
            else if (rbDistUniform.Checked)
                GenerateGraph("DistUniform");
            else if (rbDistTriangle.Checked)
                GenerateGraph("DistTriangle");
            //else if (rbDistPareto.Checked)
            //    GenerateGraph("DistPareto");
            //else if (rbDistParetoRight.Checked)
            //    GenerateGraph("DistParetoRight");
            else if (rbValue.Checked)
                GenerateGraph("SpecificValue");

        }

        private void UpdateRangeData()
        {
            UpdatingRange = true;
            double total = 0;
            int col = 1;
            decimal lowest = 9999;
            decimal highest = 0;

            

            JArray orderedArrayAssending = new JArray();
            try
            {
                orderedArrayAssending = new JArray(data.OrderBy(x => (double)x.SelectToken("value")));
            }
            catch { }

            if (rbValue.Checked == true) // Specifc Value
            {
                var item = orderedArrayAssending[valueSlider.Value -1];
                SpecificValue = (decimal)item["value"];
                UpdatingRange = false;
                return;
            }


            for (int i = rangeSlider.SliderMin -1; i < rangeSlider.SliderMax; i++)
            {
                var item = orderedArrayAssending[i];
                total += (double)item["value"];
                if ((decimal)item["value"] > highest) highest = (decimal)item["value"];
                if ((decimal)item["value"] < lowest) lowest = (decimal)item["value"];
                col++;
            }
            
            if (elementType == "edge")  // Edges have values <= 1
            {
                highest = highest * 100;
                lowest = lowest * 100;
                total = total * 100;
            }
            steps = (int)(highest - lowest);

            Mu = (decimal)total / ((rangeSlider.SliderMax - rangeSlider.SliderMin) +1);
            Alpha = lowest;
            Beta = highest;

            numTriangleGamma.Maximum = highest; numTriangleGamma.Minimum = lowest;
            UpdatingRange = false;
        }

        private void numStandardDev_ValueChanged(object sender, EventArgs e)
        {
            StandardDev = (int)numStandardDev.Value;
            GenerateGraph("DistNormal");
        }

        private void rbDistUniform_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void numTriangleAlpha_ValueChanged(object sender, EventArgs e)
        {
            GenerateGraph("DistTriangle");
        }

        private void numTriangleBeta_ValueChanged(object sender, EventArgs e)
        {
            GenerateGraph("DistTriangle");
        }

        private void numTriangleGamma_ValueChanged(object sender, EventArgs e)
        {
            GenerateGraph("DistTriangle");
        }

        public static double[] RescaleArray(double[] inputArray, double newMin, double newMax)
        {
            double oldMin = inputArray[0];
            double oldMax = inputArray[0];

            // Find the current minimum and maximum values in the input array
            for (int i = 1; i < inputArray.Length; i++)
            {
                if (inputArray[i] < oldMin)
                    oldMin = inputArray[i];

                if (inputArray[i] > oldMax)
                    oldMax = inputArray[i];
            }

            // Calculate the scaling factor
            double scalingFactor = (newMax - newMin) / (oldMax - oldMin);

            // Create a new array to store the rescaled values
            double[] rescaledArray = new double[inputArray.Length];

            // Rescale the values and store them in the rescaled array
            for (int i = 0; i < inputArray.Length; i++)
            {
                rescaledArray[i] = (inputArray[i] - oldMin) * scalingFactor + newMin;
            }

            return rescaledArray;
        }

        private void rbDistRight_CheckedChanged(object sender, EventArgs e)
        {
            GenerateDistributionButtonClicked();
        }

        private void UpdateDistributionParameters(string distribution, string value1, string value2, string value3, string elementType)
        {
            DistributionParameters = DistributionParameters == null ? new JArray() : DistributionParameters;
            DistributionParameters.Clear();
            DistributionParameters.Add(distribution);
            DistributionParameters.Add(value1);
            DistributionParameters.Add(value2);
            DistributionParameters.Add(value3);
            DistributionParameters.Add(elementType);
        }

        private void ExtractDistributionParamemeters()
        {
            if (DistributionParameters != null && DistributionParameters.Count > 0)
            {
                if (DistributionParameters[0] != null)
                    DistsObject.DistributionName = DistributionParameters[0].ToString();
                if (DistributionParameters[1] != null)
                    DistsObject.Value1 = DistributionParameters[1].ToString();
                if (DistributionParameters[2] != null)
                    DistsObject.Value2 = DistributionParameters[2].ToString();
                if (DistributionParameters[3] != null)
                    DistsObject.Value3 = DistributionParameters[3].ToString();
                if (DistributionParameters[4] != null)
                    DistsObject.NodeType = DistributionParameters[4].ToString();
            }
            else
            {

            }

        }

        private void numericUpDownExt1_ValueChanged(object sender, EventArgs e)
        {
            GenerateDistributionButtonClicked();
        }

        private void showRangeSlider()
        {
            CheckRangeSliderPositions();
            if (rbValue.Checked ==  true)
            {
                valueSlider.Visible = true;
                rangeSlider.Visible = false;
                panelChart.Visible = false;
                panelValues.Visible = false;
            }
            else
            {
                valueSlider.Visible = false;
                rangeSlider.Visible = true;
                panelChart.Visible = true;
                panelValues.Visible = true;
            }

        }

        private void rbValue_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        private void valueSlider_ValueChanged(object sender, EventArgs e)
        {

            if (Initializing == true)
                return;

            UpdateRangeData();
            GenerateGraph("SpecificValue");


        }

        private void rbValue_Click(object sender, EventArgs e)
        {
            GenerateDistributionButtonClicked();
        }

        private void panelDetailNormal_Paint(object sender, PaintEventArgs e)
        {

        }

        private void valueSlider_Scroll(object sender, EventArgs e)
        {

        }

        private void panelDetailGamma_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panelChart_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
