using CyConex.Chromium;
using System;
using System.Collections.Generic;
using System.Linq;
using CyConex.Graph;
using CefSharp;
using CyConex.Helpers;
using Newtonsoft.Json.Linq;
using System.Drawing;
using System.Globalization;

namespace CyConex
{
    public class Nodes
    {
        public async void SetNodeFrameworkData(CyConex.MainForm form1, EventHandlerEventArgs e)
        {
            if (form1.getSelectedNodes().Count() < 1)
            {
                return;
            }

            Graph.NodePositions nodePositions = Graph.NodePositions.FromJson(e.Data);
            string nodeID = form1.getSelectedNodes().ElementAt(0).Value.ID;
            var json = await form1._browser.EvaluateScriptAsync($"getNodeJson('{nodeID}');");
            var jsonRes = json.Result;
            var data = ((IDictionary<String, Object>)jsonRes);
            if (data == null)
            {
                return;
            }

            var node_data = (IDictionary<String, Object>)data["data"];
            form1.InvokeIfNeed(() =>
            {
                form1.nodePropertyForm.setHtmlEditorNodeTitle(node_data.ContainsKey("title") ? node_data["title"].ToString() : "");
                form1.nodePropertyForm.NodeFrmRefrence = node_data.ContainsKey("frameworkReference") ? 
                    node_data["frameworkReference"].ToString() : "";
                form1.nodePropertyForm.NodeFramework = node_data.ContainsKey("frameworkName") ? 
                    node_data["frameworkName"].ToString() : "";
                form1.nodePropertyForm.NodeFrmVersion = node_data.ContainsKey("frameworkNameVersion") ? 
                node_data["frameworkNameVersion"].ToString() : "";
                form1.nodePropertyForm.NodeFrmCategory = node_data.ContainsKey("primaryCategory") ? 
                    node_data["primaryCategory"].ToString() : "";
                form1.nodePropertyForm.NodeFrmDomain = node_data.ContainsKey("domain") ? 
                    node_data["domain"].ToString() : "";
                form1.nodePropertyForm.NodeFrmSubDomain = node_data.ContainsKey("subdomain") ?
                    node_data["subdomain"].ToString() : "";
                form1.nodePropertyForm.NodeFrmLevel = node_data.ContainsKey("level") ? 
                    node_data["level"].ToString() : "";
                form1.nodePropertyForm.NodeFrmSubCategory = node_data.ContainsKey("subCategory") ?
                    node_data["subCategory"].ToString() : "";
                form1.nodePropertyForm.NodeFrmReferenceURL = node_data.ContainsKey("refurl") ?
                    node_data["refurl"].ToString() : "";
                string desc = node_data.ContainsKey("description") ? 
                    node_data["description"].ToString() : "";
                form1.nodePropertyForm.setHtmlEditorNodeDesc(node_data.ContainsKey("description") ? node_data["description"].ToString() : "");
                string metatags = node_data.ContainsKey("metatags") ? node_data["metatags"].ToString() : "";
                form1.nodePropertyForm.setMetaTags(metatags);
            });
        }

        public void SetNodeNodeData(CyConex.MainForm form1, IDictionary<String, Object> node_data /*EventHandlerEventArgs e*/)
        {
            if (form1._selectedNodes.Count() < 1)
            {
                return;
            }

            form1.InvokeIfNeed(() =>
            {
                form1.SelectedNodeedgeStrengthValue = node_data.ContainsKey("edgeStrengthValue") ? 
                    node_data["edgeStrengthValue"].ToString() : "";
                form1.SelectedNodecontrolBaseScore = node_data.ContainsKey("controlBaseScore") ?  
                    node_data["controlBaseScore"].ToString() : "";
            
                try
                {
                    if (node_data["nodeType"].ToString() == "control")
                    {
                        //mainForm.AddNodeCmbNodeBase(mainForm._settings.NodeinherentStrengthData);
                        //mainForm.AddNodeCmbNodeAssessmentStatus(mainForm._settings.NodeimplementedStrengthData);
                        //mainForm.LabelcontrolBaseScore = "Value";
                        //mainForm.LabelcontrolBaseScoreStatus = "Control Strength (Cs)";
                        //mainForm.EnableNodeTextNodecontrolBaseScore(false);

                        //mainForm.LabelAssessmentValue = "Value";
                        //mainForm.LabelAssessmentStatus = "Control Implementation (Ci)";
                        //mainForm.EnableNodeTextNodeAssessmentStatus(true);
                        //mainForm.EnableNodeCmbNodeAssessmentStatus(true);
                    }

                    if (node_data["nodeType"].ToString() == "objective")
                    {
                        //mainForm.LabelcontrolBaseScore = "Value";
                        //mainForm.LabelcontrolBaseScoreStatus = "Objective Target";
                        //mainForm.EnableNodeCmbNodecontrolBaseScore(true);
                        //JArray tmp = new JArray();
                        //tmp.Add("Sum of all Fundamental Strength values");
                        ////tmp.Add("Manually set");
                        //mainForm.AddNodeCmbNodeBase(tmp, false);

                        //mainForm.LabelAssessmentStatus = "Implemented Value";
                        //mainForm.LabelAssessmentValue = "Value";
                        ////mainForm.SetTextNodeCmbNodeAssessmentStatus("Sum of all Implementation Strength value");
                        //mainForm.EnableNodeCmbNodeAssessmentStatus(false);

                    }

                    if (node_data["nodeType"].ToString() == "group")
                    {
                        //mainForm.LabelcontrolBaseScoreStatus = "Objective Target";
                        //mainForm.LabelcontrolBaseScore = "Value";
                        //mainForm.SetTextNodeCmbNodecontrolBaseScore("Sum of all Fundamental strength values");
                        //mainForm.EnableNodeTextNodecontrolBaseScore(false);

                        //mainForm.LabelAssessmentStatus = "Implemented Value";
                        //mainForm.LabelAssessmentValue = "Value";
                        ////mainForm.SetTextNodeCmbNodeAssessmentStatus("Sum of all Implementation Strength value");
                        //mainForm.EnableNodeCmbNodeAssessmentStatus(false);

                    }
                    if (node_data["nodeType"].ToString() == "attack")
                    {
                        //mainForm.LabelcontrolBaseScoreStatus = "Objective Target";
                        //mainForm.LabelcontrolBaseScore = "Value";
                        //mainForm.SetTextNodeCmbNodecontrolBaseScore("Sum of all Fundamental strength values");
                        //mainForm.EnableNodeTextNodecontrolBaseScore(false);

                        //mainForm.LabelAssessmentStatus = "Implemented Value";
                        //mainForm.LabelAssessmentValue = "Value";
                        //mainForm.EnableNodeCmbNodeAssessmentStatus(false);
                    }

                    if (node_data["nodeType"].ToString() == "vulnerability")
                    {
                        //mainForm.LabelcontrolBaseScoreStatus = "Objective Target";
                        //mainForm.LabelcontrolBaseScore = "Value";
                        //mainForm.SetTextNodeCmbNodecontrolBaseScore("Sum of all Fundamental strength values");
                        //mainForm.EnableNodeTextNodecontrolBaseScore(false);

                        //mainForm.LabelAssessmentStatus = "Implemented Value";
                        //mainForm.LabelAssessmentValue = "Value";
                        //mainForm.EnableNodeCmbNodeAssessmentStatus(false);
                    }

                    if (node_data["nodeType"].ToString() == "evidence")
                    {
                        //Todo
                    }

                    if (node_data.ContainsKey("objectiveTargetType") && node_data["objectiveTargetType"] != null)
                    {
                        //mainForm.SetTextNodeCmbNodecontrolBaseScore(node_data["objectiveTargetType"].ToString());
                        //mainForm.SelectIndexNodeCmbNodecontrolBaseScore(node_data["objectiveTargetType"].ToString());
                    }

                    if (node_data.ContainsKey("implementedStrength") && node_data["implementedStrength"] != null)
                    {
                        //mainForm.SetTextNodeCmbNodeAssessmentStatus(node_data["implementedStrength"].ToString());
                        //mainForm.SelectIndexNodeCmbNodeAssessmentStatus(node_data["implementedStrength"].ToString());
                    }
                }
                catch
                { }
            });
        }

        public async void SetNodeAssessmentData(CyConex.MainForm form1, EventHandlerEventArgs e)
        {
            if (form1._selectedNodes.Count() < 1)
            {
                return;
            }

            Graph.NodePositions nodePositions = Graph.NodePositions.FromJson(e.Data);

            string nodeID = form1._selectedNodes.ElementAt(0).Value.ID;
            var json = await form1._browser.EvaluateScriptAsync($"getNodeJson('{nodeID}');");
            var jsonRes = json.Result;
            var data = ((IDictionary<String, Object>)jsonRes);
            var node_data = (IDictionary<String, Object>)data["data"];
            form1.InvokeIfNeed(() =>
            {
                try
                {
                    string tempString = node_data["controlBaseScore"].ToString();
                    decimal tempDouble = decimal.Round(Convert.ToDecimal(tempString), 2);

                    decimal MaxScore = decimal.Round(Convert.ToDecimal(tempDouble), 2);
                    decimal CalculatedScore = decimal.Round(Convert.ToDecimal(node_data["calculatedValue"].ToString()), 2);
                    decimal DiffrenceScore = MaxScore - CalculatedScore;
                    decimal ComplianceScore = decimal.Round(Convert.ToDecimal(node_data["calculatedValue"]) / Convert.ToDecimal(node_data["controlBaseScore"]) * 100, 2);

                   
                    form1.AddYChartCompliance(DiffrenceScore, Color.Salmon, "Not Compliant", true);
                    form1.AddYChartCompliance(CalculatedScore, Color.PaleGreen, "Compliant");
                }
                catch
                {
                    form1.ShowChatCompliance(true);
                }
            });
        }

        public async void SetNodeNotesData(CyConex.MainForm mainForm, EventHandlerEventArgs e)
        {
            if (mainForm._selectedNodes.Count() < 1)
            {
                return;
            }

            Graph.NodePositions nodePositions = Graph.NodePositions.FromJson(e.Data);
            string nodeID = mainForm._selectedNodes.ElementAt(0).Value.ID;
            var json = await mainForm._browser.EvaluateScriptAsync($"getNodeJson('{nodeID}');");
            var jsonRes = json.Result;
            var data = ((IDictionary<String, Object>)jsonRes);
            var node_data = (IDictionary<String, Object>)data["data"];
            mainForm.InvokeIfNeed(() =>
            {
                string note = node_data["note"].ToString();
                /*var from_note = System.Convert.FromBase64String(note);
                string final_note = System.Text.Encoding.UTF8.GetString((from_note));
                mainForm.nodePropertyForm.NodeRichTextNotes = final_note;*/
                mainForm.nodePropertyForm.setHtmlEditorNodeNote(note);
            });
            if (mainForm._selectedNodes.Count() > 0)
            {
                Node node_item = mainForm._selectedNodes.ElementAt(0).Value;
            }
        }

    }
}
