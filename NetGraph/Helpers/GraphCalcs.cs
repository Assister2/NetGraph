using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Windows.Forms;
using Syncfusion.Windows.Forms.Edit.Utils;
using CefSharp.DevTools.CSS;
using System.Runtime.InteropServices.WindowsRuntime;
using Syncfusion.Windows.Forms.Diagram;
using CefSharp;
using CefSharp.DevTools.Network;
using CefSharp.DevTools.Accessibility;
using CefSharp.DevTools.IndexedDB;
using Syncfusion.Data.Extensions;
using static Syncfusion.Windows.Forms.Diagram.Resources;
using System.Threading.Tasks;
using CefSharp.DevTools.DOM;
using System.Diagnostics;
using Syncfusion.Windows.Forms.Tools;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;
using System.Windows.Forms.VisualStyles;
using System.Windows.Input;

namespace CyConex.Graph
{

    public class GraphCalcs
    {

        public static List<string> NodePairs = new List<string>();
        private static List<string> NodePairs2 = new List<string>();
        private static Dictionary<string, double> s_attackActorScores = new Dictionary<string, double>();
        private static Dictionary<string, double> s_attackAssetScores = new Dictionary<string, double>();
        //private static Dictionary<string, double> s_attackVulnerabilityScores = new Dictionary<string, double>();
        private static Dictionary<string, double> s_assetImpactScores = new Dictionary<string, double>();
        private static Dictionary<string, double> s_assetImpactLikelihoodScores = new Dictionary<string, double>();
        private static Dictionary<string, double> s_assetAssetScores = new Dictionary<string, double>();
        private static Dictionary<string, double> s_assetAssetMitigatedScores = new Dictionary<string, double>();
        private static Dictionary<string, double> s_assetAssetMitigatedScores1 = new Dictionary<string, double>();
        private static Dictionary<string, double> s_calculatedValues = new Dictionary<string, double>();
        private static Dictionary<string, double> s_vulnerabilityScores = new Dictionary<string, double>();
        private static Dictionary<string, double> s_vulnerabilityMitigatedScores = new Dictionary<string, double>();
        private static Dictionary<string, double> s_likelihoodScores = new Dictionary<string, double>();
        public static List<graphResultsList> _IndividualRiskPaths = new List<graphResultsList>();
        public static List<(DateTime, string)> s_CalculationLog = new List<(DateTime, string)>();
        public static List<KeyValuePair<string, string>> s_AverageCalculatedValues = new List<KeyValuePair<string, string>>();
        public static bool useCalcLog = false;
        public static bool autoCalculate = false;
        public static bool graphCalculated = false;
        private static MainForm mainForm = null;
        public static bool graphCalcInProgress = false;
        RiskPanelsForm RiskPathForm;

        public static void Init(MainForm form1)
        {
            mainForm = form1;
        }

        public static void InitializeValues()
        {
            // Reset variables, lists etc
            s_attackActorScores.Clear();
            s_attackAssetScores.Clear();
            //s_attackVulnerabilityScores.Clear();
            s_assetImpactScores.Clear();
            s_assetImpactLikelihoodScores.Clear();
            s_assetAssetScores.Clear();
            s_assetAssetMitigatedScores.Clear();
            s_assetAssetMitigatedScores1.Clear();
            s_calculatedValues.Clear();
            _IndividualRiskPaths.Clear();
            s_CalculationLog.Clear();
            s_AverageCalculatedValues.Clear();
            s_vulnerabilityScores.Clear();
            s_vulnerabilityMitigatedScores.Clear();
            s_likelihoodScores.Clear();


        }

        public static void CalcLogEntry(string entryText)
        {
            if (useCalcLog) s_CalculationLog.Add((DateTime.Now, entryText));
            if (Program.DebugMode) Console.WriteLine(entryText);
        }

        public static async Task<Task<bool>> RecalculateAll()
        {
           

            if (graphCalcInProgress == true)
                 return Task.FromResult(true);
            else
                graphCalcInProgress = true;


            mainForm.ShowCalcBusy();

            string CurrentNodeID;
            string CurrentAssetNode = "";

            List<string> rootNodeIDs;
            DateTime startTime = DateTime.Now;
          

            //clear caches
            s_attackActorScores.Clear();
            s_attackAssetScores.Clear();
            //s_attackVulnerabilityScores.Clear();
            s_assetImpactScores.Clear();
            s_assetAssetScores.Clear();
            s_assetAssetMitigatedScores.Clear();
            s_assetImpactLikelihoodScores.Clear();
            s_assetAssetMitigatedScores1.Clear();
            s_AverageCalculatedValues.Clear();
            

            List<string> nodes;

            s_CalculationLog.Clear();
            CalcLogEntry($"--- Begin Recalculating Graph ---");
            CalcLogEntry($"Checking Graph Schema...");

            // await GraphUtil.SyncFromGraph();
            if (!autoCalculate)
            {
               CalcLogEntry($"Syncing Data from Graph...");
                await GraphUtil.SyncFromGraph();
               CalcLogEntry($"Graph Data Synced");
                GraphUtil.NullInMemoryItems();
            }
            else
            {
               CalcLogEntry($"Not Syncing Data from Graph. Using In Memory Data.");
                GraphUtil.NullInMemoryItems();
            }

            //Create relationships for Asset Groups
            CalcLogEntry($"Creating Asset-Group Edges...");
            GraphUtil.CreateAssetGroupEdges();

            CalcLogEntry($"Calculating Edge Values...");
            GraphUtil.CalculateEdgeStrengthValuesForAllEdges();

            CalcLogEntry($"Calculating Actor Node Values...");
            GraphUtil.CalculateActorScoreForAllNodes();

             CalcLogEntry($"Calculating Attack Node Values...");
            GraphUtil.CalculateAttackScoreForAllNodes();

           CalcLogEntry($"Calculating Asset Node Values...");
            GraphUtil.CalculateAssetScoreForAllNodes();

            CalcLogEntry($"Calculating Vulnerability Node Values...");
            GraphUtil.CalculateVulnerabilityScoreForAllNodes();

           CalcLogEntry($"Calculating Control Node Values...");
            GraphUtil.CalculateControlValuesForAllNodes();

            //Get roots nodes IDs
           CalcLogEntry($"Getting Root Nodes...");

            //Get roots nodes IDs
            rootNodeIDs = GraphUtil.GetRootIDs();
            //var roots = await _browser.EvaluateScriptAsync("getRootsIDs();");
            if (rootNodeIDs != null)
            {
               CalcLogEntry($"Found {rootNodeIDs.Count} root nodes on graph");
            }
            else
            {
               CalcLogEntry($"Error while get root nodes!");

                return Task.FromResult(true);
            }


            //Get all nodes
            nodes = GraphUtil.GetNodes();
            //var allNodes = await _browser.EvaluateScriptAsync("getNodes();");
            if (nodes != null)
            {
               CalcLogEntry($"Found {nodes.Count} nodes in total");
            }
            else
            {
               CalcLogEntry($"Error while getting all nodes!");
                return Task.FromResult(true);
            }

            var TempPaths = new List<string>();
            var Paths = new List<string>();

            //Get all asset nodes on the graph
            //var AssetResult = GraphUtil.GetAssetNodes()
            var AssetResult = GraphUtil.GetAssetAndAssetGroupNodes();

            foreach (var NodeIDNode in rootNodeIDs)
            {
               CalcLogEntry($"Getting all Asset Nodes on graph...");

                //JavascriptResponse AssetResult = await _browser.EvaluateScriptAsync($"GetAssetNodes();");

                TempPaths.Clear();
                if (AssetResult != null)
                {
                   CalcLogEntry($"Found {AssetResult.Count} Asset node(s) on graph...");
                   CalcLogEntry($"Processing each Source Node to Asset node...");
                    if (AssetResult.Count != 0)
                    {
                        foreach (var AssetNode in AssetResult)
                        {
                            CurrentAssetNode = AssetNode.ToString();
                           CalcLogEntry($"Asset Node is: {CurrentAssetNode}");
                           CalcLogEntry($"Checking if Source Node can reach Asset Node...");

                            // Now need to check if Node can reach Asset
                           CalcLogEntry($"Source Node is: {NodeIDNode}, Asset Node is: {CurrentAssetNode}");
                            CurrentNodeID = NodeIDNode;
                            //Get all paths to Asset  

                           CalcLogEntry($"Getting all Paths...");
                            var GetAllPathsResult = GraphUtil.GetAllPathNodesAndEdges2(NodeIDNode, CurrentAssetNode);

                            if (GetAllPathsResult != null)
                            {
                               CalcLogEntry($"Found {GetAllPathsResult.Count().ToString()} paths from Source Node to the Asset...");
                               CalcLogEntry($"Building path list...");
                                var pathCount = 0;
                                if (GetAllPathsResult != null && GetAllPathsResult.Count() > 0)
                                {
                                    foreach (var ID in GetAllPathsResult)
                                    {
                                        Paths.Add(ID.ToString());
                                        pathCount++;
                                    }
                                   CalcLogEntry($"Added {pathCount} paths to path list...");
                                }
                                else
                                {
                                   CalcLogEntry($"No paths found between Source Node and Asset...");
                                }
                                //bool BuildTempPath = false;
                                //string TempString = "";

                            }
                            else
                            {
                               CalcLogEntry($"No path found from the Source Node to the Asset...");
                            }
                        }
                    }
                }

            }

            CalcLogEntry($"Number of paths found : {Paths.Count}");
            
            //Need to set all group value to Zero
            CalcLogEntry($"Zeroing Group Nodes...");
            GraphUtil.ZeroAllGroupNodes();
            //var res = await _browser.EvaluateScriptAsync($"zeroAllGroupNodes();");
            CalcLogEntry($"Zeroing Asset Nodes...");
            GraphUtil.ZeroAllAssetNodes();
            //res = await _browser.EvaluateScriptAsync($"zeroAllAssetNodes();");
            CalcLogEntry($"Zeroing Objective Nodes...");
            GraphUtil.ZeroAllObjectiveNodes();
            //res = await _browser.EvaluateScriptAsync($"zeroAllObjectiveNodes();");
            CalcLogEntry($"Zeroing Control Nodes Previous Values...");
            GraphUtil.ZeroAllControlNodesPreviousValues();

            NodePairs.Clear();
            NodePairs2.Clear();
            s_calculatedValues.Clear();


           CalcLogEntry($"Processing each path longest to shortest...");
            foreach (string PathList in SortByLength(Paths))
            {
                int count = PathList.Count(c => c == ';');
               CalcLogEntry($"Processing path, length {count} : {PathList}");
                RecalculatePath(PathList);
                graphCalculated = true;

            }

           CalcLogEntry($"Updating Distributions");
            GraphUtil.UpdateDistributionsForAllNodes();

           CalcLogEntry($"Caclulating IndividualRiskPaths");
            CalculateIndividualRiskPaths();


            // Remove Listerners
            mainForm._browser.ExecuteScriptAsync("cy.removeListener('data');");
            mainForm._browser.ExecuteScriptAsync("cy.removeListener('data', 'node');");
            mainForm._browser.ExecuteScriptAsync("cy.removeListener('data', 'edge');");
           
           CalcLogEntry($"Syncing Data to Graph...");
            await GraphUtil.SyncToGraph();
           CalcLogEntry($"Graph Data Synced");
            
            // Renable listerners listerners
            mainForm._browser.ExecuteScriptAsync("cy.addListener('data', 'node', nodeDataChanged);");
            mainForm._browser.ExecuteScriptAsync("cy.addListener('data', 'edge', nodeDataChanged);");
            mainForm._browser.ExecuteScriptAsync("cy.on('data', cy_data);");

           CalcLogEntry($"--- End Recalculating Graph ---");

            GraphUtil.calcIterations++;
            graphCalcInProgress = false;
            mainForm.UpdatecalculationLog(startTime, s_CalculationLog);
            mainForm.HideCalcBusy();
            return Task.FromResult(false);

        }



        private static void RecalculatePath(string Path)
        {
            string CurrentNodeID;
            string PreviousNodeID = "";
            string PreviousEdgeID = "";
            string LastNodeID = "";
            string lastvulnerabilityNode = "";

            string[] NodeonPath;
            int PathCount = 0;
           CalcLogEntry($"Starting RecalculatePath: {Path}");

           CalcLogEntry($"Splitting Path...");
            NodeonPath = Path.Split(',');

            PathCount = NodeonPath.Count();
           CalcLogEntry($"Nodes and Edges on Path: {PathCount}");

            for (int i = 0; i < PathCount; i++) // Process each Node in the path
            {

                CurrentNodeID = NodeonPath[i];

                if (CurrentNodeID != "")
                {

                   CalcLogEntry($"Processing Node:{CurrentNodeID}");
                   CalcLogEntry($"Getting Node type...");
                    var nodeType = GraphUtil.GetNodeType(CurrentNodeID);

                    if (nodeType != null && nodeType != "notfound")
                    {
                       CalcLogEntry($"Node type is: {nodeType.ToString()}");

                        //NodePair2 maintains a list of Nodes to stop Nodes being cleared if they have unprocessed children
                        //List contains All pairs of Nodes (Source > Target)
                       CalcLogEntry($"Getting Node Children...");

                        var TargetNodes = GraphUtil.GetChildNodes(CurrentNodeID);
                        // var TargetNodes = await _browser.EvaluateScriptAsync($"GetNodeOutgoerNodes('{CurrentNodeID}');");
                       CalcLogEntry($"Node has {TargetNodes.Count} children...");
                        //JArray arrayNodes = JArray.Parse(TargetNodes.ToString());
                        foreach (var ID in TargetNodes)
                        {
                            string tempNodePair = ID.ToString() + " > " + CurrentNodeID;
                           CalcLogEntry($"Looking for NodePair2 Pair: {tempNodePair}");
                            if (NodePairs2.IndexOf(tempNodePair) == -1)
                            {
                               CalcLogEntry($"Node Pair is not is the NodePair2 list: {tempNodePair}");
                               CalcLogEntry($"Adding Node Pair to NodePair2 list: {tempNodePair}");
                                NodePairs2.Add(tempNodePair);
                            }
                        }


                        // Check if this is a control Node
                        if (nodeType.ToString().ToLower() == "control")  // Control Calculation
                        {
                           CalcLogEntry($"Processing Node as Control Node...");


                            //Check if Node is enabled
                           CalcLogEntry($"Check if Node is Enabled...");
                            var nodeEnabled = GraphUtil.IsNodeEnabled(CurrentNodeID);

                            if (nodeEnabled.ToString().ToLower() == "true")
                            {
                               CalcLogEntry($"Node is Enabled...");

                                // Check if Node is a Parent Node of other Control Nodes
                               CalcLogEntry($"Checking if Node is a Parent Node...");
                                if (GraphUtil.IsAssetGroup(CurrentNodeID) == true)
                                {
                                   CalcLogEntry($"Node is a Parent Node...");
                                   CalcLogEntry($"Calculationg Totals of Child Control Nodes...");

                                    double parentNodeCalculatedValue = 0;
                                    double parentNodeControlBaseScore = 0;
                                    double parentNodeControlAssessedScore = 0;

                                    List<string> nodes = GraphUtil.GetChildNodesofParentGroup(CurrentNodeID);
                                   CalcLogEntry($"Parnet Node has {nodes.Count} Child Nodes...");
                                    for (int j = 0; j < nodes.Count; j++)
                                    {
                                        CalculateControl(nodes[j], LastNodeID, PreviousEdgeID);

                                       CalcLogEntry($"Current parentNodeCalculatedValue is {parentNodeCalculatedValue.ToString()}");
                                        double childNodeCalculatedValue = GraphUtil.GetNodeCalculatedValue(nodes[j]);
                                       CalcLogEntry($"Retreived Child Node Calculated Value is: {nodes[j]}");
                                        parentNodeCalculatedValue += childNodeCalculatedValue;
                                       CalcLogEntry($"New parentNodeCalculatedValue is {parentNodeCalculatedValue.ToString()}");

                                       CalcLogEntry($"Current parentNodecontrolBaseScore is {parentNodeControlBaseScore.ToString()}");
                                        double childNodeControlBaseScore = GraphUtil.GetNodeBaseScore(nodes[j]);
                                       CalcLogEntry($"Retreived Child Node Base Value is: {nodes[j]}");
                                        parentNodeControlBaseScore += childNodeControlBaseScore;
                                       CalcLogEntry($"New parentNodecontrolBaseScore is {parentNodeControlBaseScore.ToString()}");

                                       CalcLogEntry($"Current parentNodecontrolAssessedScore is {parentNodeControlAssessedScore.ToString()}");
                                        double childNodeControlAssessedScore = GraphUtil.GetControlNodeAssessedScore(nodes[j]);
                                       CalcLogEntry($"Retreived Child Node Assessed Value is: {nodes[j]}");
                                        parentNodeControlAssessedScore += childNodeControlAssessedScore;
                                       CalcLogEntry($"New parentNodecontrolAssessedScore is {parentNodeControlAssessedScore.ToString()}");
                                    }

                                    if (parentNodeCalculatedValue > 100)
                                        parentNodeCalculatedValue = 100;

                                    if (parentNodeControlBaseScore > 100)
                                        parentNodeControlBaseScore = 100;

                                    if (parentNodeControlAssessedScore > 100)
                                        parentNodeControlAssessedScore = 100;

                                    // Now save the total values to the Parent Node
                                    GraphUtil.SetNodeData(CurrentNodeID, "controlAssessedScore", parentNodeControlAssessedScore.ToString());
                                    GraphUtil.SetNodeData(CurrentNodeID, "controlBaseScore", parentNodeControlBaseScore.ToString());
                                    GraphUtil.AddToNodeScores(CurrentNodeID + ":controlAssessedScore", parentNodeControlAssessedScore);
                                    if (parentNodeCalculatedValue > 100) parentNodeCalculatedValue = 100;
                                    GraphUtil.SetNodeData(CurrentNodeID, "calculatedValue", parentNodeCalculatedValue.ToString());
                                    GraphUtil.AddToNodeScores(CurrentNodeID + ":calculatedValue", parentNodeCalculatedValue);
                                }
                                else
                                {
                                    CalcLogEntry($"Node is not a Parent Node...");
                                    CalculateControl(CurrentNodeID, LastNodeID, PreviousEdgeID);
                                }
                            }
                            else
                            {
                               CalcLogEntry($"Node disabled, not processing.");
                            }
                        }
                        
                        if (nodeType.ToString().ToLower() == "objective")
                        {
                            CalculateObjective(CurrentNodeID, PreviousNodeID, PreviousEdgeID);
                        }

                        // Group or Asset Node *******************************************************************************************************************
                        if (nodeType.ToString().ToLower() == "group" || 
                            nodeType.ToString().ToLower() == "asset")
                        {

                            double edgeImpactValue = 0;

                           CalcLogEntry($"+++ {nodeType.ToString()} Node +++");
                           CalcLogEntry($"Checking if Node is enabled...");
                            var nodeEnabled = GraphUtil.IsNodeEnabled(CurrentNodeID);

                            if (nodeEnabled.ToString().ToLower() == "true")
                            {
                               CalcLogEntry($"Node is enabled...");

                                //Get Current Group Values
                               CalcLogEntry($"Getting Node current values...");
                                
                                var CurrentNodeControlBaseScore = GraphUtil.GetNodeBaseScore(CurrentNodeID);
                               CalcLogEntry($"Current Base Value is:{CurrentNodeControlBaseScore.ToString()}");

                                var CurrentNodeControlAssessedScore = GraphUtil.GetControlNodeAssessedScore(CurrentNodeID);
                               CalcLogEntry($"Current Assesed Value is:{CurrentNodeControlAssessedScore.ToString()}");

                                var CurrentNodeCalculatedValue = GraphUtil.GetNodeCalculatedValue(CurrentNodeID);
                               CalcLogEntry($"Current Calculated Value is:{CurrentNodeCalculatedValue.ToString()}");
                                
                              
                                double nodeControlBaseScore = 0;
                                double nodeCalculatedValue = 0;
                                double nodeControlAssessedScore = 0;
                                double parentNodePreviousControlBaseScore = 0;
                                double parentNodePreviousControlAssessedScore = 0;
                                double previousNodeCalculatedValue = 0;
                                double previousNodeControlAssessedScore = 0;
                                string previousNodeObjectiveTarget = "";
                                string parentNodeType = "";


                                if (PreviousNodeID != CurrentNodeID && PreviousNodeID != "")
                                {
                                    try
                                    {
                                        //Get Parent Node Current Values
                                       CalcLogEntry($"Getting Parent Node values from Node:{PreviousNodeID}  (1017)");

                                        nodeControlBaseScore = GraphUtil.GetNodeBaseScore(PreviousNodeID);
                                       CalcLogEntry($"Parent Node Base Value is:{nodeControlBaseScore.ToString()}");

                                        nodeControlAssessedScore = GraphUtil.GetControlNodeAssessedScore(PreviousNodeID);
                                       CalcLogEntry($"Parent Node Assessed Value is:{nodeControlAssessedScore.ToString()}");

                                        nodeCalculatedValue = GraphUtil.GetNodeCalculatedValue(PreviousNodeID);
                                       CalcLogEntry($"Parent Node Calculated Value is:{nodeCalculatedValue.ToString()}");

                                        parentNodeType = GraphUtil.GetNodeType(PreviousNodeID);
                                       CalcLogEntry($"Parent Node Type is:{parentNodeType.ToString()} (0001)");

                                        //Get Parent Node Previous Values
                                       CalcLogEntry($"Getting Parent Node previous values from Node:{PreviousNodeID} (1010)");

                                        parentNodePreviousControlBaseScore = GraphUtil.GetPreviousNodeControlBaseScore(PreviousNodeID);
                                       CalcLogEntry($"Parent Node previous Base Value is:{parentNodePreviousControlBaseScore.ToString()}");

                                        parentNodePreviousControlAssessedScore = GraphUtil.GetPreviousNodeControlAssessedScore(PreviousNodeID);
                                       CalcLogEntry($"Parent Node previous Assessed Value is:{parentNodePreviousControlBaseScore.ToString()}");

                                        previousNodeCalculatedValue = GraphUtil.GetPreviousNodeCalculatedValue(PreviousNodeID);
                                       CalcLogEntry($"Parent Node previous Calculated Value is:{previousNodeCalculatedValue.ToString()}");

                                        previousNodeObjectiveTarget = GraphUtil.GetNodeObjectiveTargetType(PreviousNodeID);
                                       CalcLogEntry($"Parent Node previous Objective Target is:{previousNodeObjectiveTarget.ToString()}");
                                        
                                    }
                                    catch
                                    {

                                       CalcLogEntry($"No prevous Node found! Setting values to 0...");
                                        nodeControlBaseScore = 0;
                                       CalcLogEntry($"Parent Node Base Value is: 0");
                                        nodeCalculatedValue = 0;
                                       CalcLogEntry($"Parent Node Calculated Value is: 0");
                                        nodeControlAssessedScore = 0;
                                       CalcLogEntry($"Parent Node Assessed Value is: 0");

                                        //Get Parent Node Previous Values
                                        parentNodePreviousControlBaseScore = 0;
                                       CalcLogEntry($"Parent Node previous Base Value is: 0");

                                        previousNodeCalculatedValue = 0;
                                       CalcLogEntry($"Parent Node previous Calculated Value is: 0");

                                        parentNodePreviousControlAssessedScore = 0;
                                       CalcLogEntry($"Parent Node previous Assessed Value is: 0");

                                        previousNodeObjectiveTarget = "Manually set";
                                       CalcLogEntry($"Parent Node previous Inhernet Strength is: Manually Set");
                                    }
                                }
                                else
                                {
                                    //Get Parent Node Current Values
                                   CalcLogEntry($"No previous Node found! Setting values to 0...");
                                    
                                    nodeControlBaseScore = 0;
                                   CalcLogEntry($"Parent Node Base Value is: 0");
                                    
                                    nodeCalculatedValue = 0;
                                   CalcLogEntry($"Parent Node Calculated Value is: 0");

                                    nodeControlAssessedScore = 0;
                                   CalcLogEntry($"Parent Node Assessed Value is: 0");

                                    //Get Parent Node Previous Values
                                    parentNodePreviousControlBaseScore = 0;
                                   CalcLogEntry($"Parent Node previous Base Value is: 0");
                                    
                                    previousNodeCalculatedValue = 0;
                                   CalcLogEntry($"Parent Node previous Calculated Value is: 0");

                                    parentNodePreviousControlAssessedScore = 0;                                    
                                   CalcLogEntry($"Parent Node previous Assessed Value is: 0");
                                   
                                    previousNodeObjectiveTarget = "Manually set";
                                   CalcLogEntry($"Parent Node previous Fundamental Strength is: Manually Set");
                                }

                                if (PreviousNodeID == null || PreviousNodeID == "")
                                {
                                    PreviousNodeID = "No-Node";
                                }

                                string currentNodePair = CurrentNodeID + " > " + PreviousNodeID;
                                var TempNode = PreviousNodeID;
                                PreviousNodeID = CurrentNodeID;


                                //Get Edge Strenth Value
                                CalcLogEntry($"Previous Edge is: {PreviousEdgeID}");
                                var edgeStrengthScore = GraphUtil.GetEdgeStrengthScore(PreviousEdgeID);
                                //JavascriptResponse edgeStrengthValue = await _browser.EvaluateScriptAsync($"getedgeStrengthValue('{PreviousEdgeID}');");
                                CalcLogEntry($"Previous Edge Strenth Value is: {edgeStrengthScore.ToString()}"); // e.g. 0.25


                                // EIV = 0 - ((NBV - NCV) * EAV)
                                // NCV = NAV + EIV
                                //Mutiply Previous Node Value by Edge Strenth Values

                                CalcLogEntry($"Calculating Impacted values...");
                                
                                //28/04/23
                                edgeImpactValue = 0 - (nodeCalculatedValue* Convert.ToDouble(edgeStrengthScore.ToString()));
                                CalcLogEntry($"Node Calculated Value is:{nodeCalculatedValue.ToString()}");
                                CalcLogEntry($"Edge Strength Score is :{edgeStrengthScore.ToString()}");
                                CalcLogEntry($"EdgeImpactedValue = 0 - (nodeCalculatedValue * edgeStrengthScore)");
                                CalcLogEntry($"EdgeImpactedValue = 0 - ({nodeCalculatedValue.ToString()} * {edgeStrengthScore.ToString()})");
                                CalcLogEntry($"EdgeImpactedValue = {edgeImpactValue.ToString()}");
                                if (edgeImpactValue < 0)
                                {
                                   CalcLogEntry($"EdgeImpactValue < 0, setting to 0");
                                    edgeImpactValue = 0;
                                }

                                Double edgeImpactedNodeControlBaseScore = nodeControlBaseScore * Convert.ToDouble(edgeStrengthScore.ToString());  //100 * 1 = 100
                                CalcLogEntry($"edgeImpactedNodeControlBaseScore = NodecontrolBaseScoreResult * edgeStrengthValue");
                                CalcLogEntry($"edgeImpactedNodeControlBaseScore = {nodeControlBaseScore.ToString()} * {edgeStrengthScore.ToString()}");
                                CalcLogEntry($"edgeImpactedNodeControlBaseScore = {edgeImpactedNodeControlBaseScore.ToString()}");
                                if (edgeImpactedNodeControlBaseScore < 0)
                                {
                                   CalcLogEntry($"edgeImpactedNodeControlBaseScore < 0, setting to 0");
                                    edgeImpactedNodeControlBaseScore = 0;
                                }

                                Double edgeImpactedNodeCalculatedValue = nodeCalculatedValue * Convert.ToDouble(edgeStrengthScore.ToString());
                               CalcLogEntry($"NodeImpactedCalculatedValue = NodeCalculatedValueResult * edgeStrengthValue");
                               CalcLogEntry($"NodeImpactedCalculatedValue = {edgeImpactedNodeCalculatedValue.ToString()} * {edgeStrengthScore.ToString()}");
                               CalcLogEntry($"NodeImpactedCalculatedValue = {edgeImpactedNodeCalculatedValue.ToString()}");

                                Double edgeImpactedNodeAssessedValue = nodeControlAssessedScore * Convert.ToDouble(edgeStrengthScore.ToString()); // 100 * 1 = 100
                                CalcLogEntry($"edgeImpactedNodeAssessedValue = nodeControlAssessedScore * edgeStrengthValue");
                                CalcLogEntry($"edgeImpactedNodeAssessedValue = {nodeControlAssessedScore.ToString()} * {edgeStrengthScore.ToString()}");
                                CalcLogEntry($"edgeImpactedNodeAssessedValue = {edgeImpactedNodeAssessedValue.ToString()}");
                                if (edgeImpactedNodeAssessedValue < 0)
                                {
                                   CalcLogEntry($"edgeImpactedNodeAssessedValue < 0, setting to 0");
                                    edgeImpactedNodeAssessedValue = 0;
                                }

                                //Add the values of Current Group and Node together
                                CalcLogEntry($"Calculating new Group values...");

                                Double totalGroupControlBaseScore = 0;
                                CalcLogEntry($"Checking if Node pair needs to be processed: '{currentNodePair}'");
                                if (NodePairs.IndexOf(currentNodePair) == -1 && !currentNodePair.Contains("No-Node"))
                                {

                                   CalcLogEntry($"Processing Node pair '{currentNodePair}'");
                                   //var objectiveTargetType = GraphUtil.GetNodeObjectiveTargetType(CurrentNodeID);
                                   //CalcLogEntry($"Node Objective Type is '{objectiveTargetType}'");

                                    //Get behavour 
                                    CalcLogEntry($"Getting Node Behaviour...");
                                    string nodeBehaviour = GraphUtil.GetnodeBehaviour(CurrentNodeID);
                                    CalcLogEntry($"Node Behaviour is {nodeBehaviour}");

                                    if (nodeType.ToString().ToLower() == "group")
                                    {
                                        // Need to update code when looking at groups in future 
                                        //if (objectiveTargetType != null && objectiveTargetType.ToString().ToLower() != "manually set")
                                        //{
                                        //    CalcLogEntry($"Base Value is calculated as a Group...");
                                        //    CalcLogEntry($"Current Node Base Value is: {CurrentNodeControlBaseScore.ToString()}");
                                        //    CalcLogEntry($"Impact modified Base Value from Parent Node is: {edgeImpactedNodeControlBaseScore.ToString()}");
                                        //    CalcLogEntry($"Previous Base Value from Parent Node is: {parentNodePreviousControlBaseScore.ToString()}");

                                        //    CalcLogEntry($"totalGroupControlBaseScore = GroupcontrolBaseScore + NodeImpactedcontrolBaseScore - (parentNodePreviouscontrolBaseScore * edgeStrengthValue)");
                                        //    CalcLogEntry($"totalGroupControlBaseScore = {CurrentNodeControlBaseScore.ToString()} + {edgeImpactedNodeControlBaseScore.ToString()} - ({parentNodePreviousControlBaseScore.ToString()} * {edgeStrengthScore.ToString()}");
                                        //    totalGroupControlBaseScore = Convert.ToDouble(CurrentNodeControlBaseScore.ToString()) + edgeImpactedNodeControlBaseScore - (parentNodePreviousControlBaseScore * Convert.ToDouble(edgeStrengthScore.ToString()));
                                        //    CalcLogEntry($"totalGroupControlBaseScore = {totalGroupControlBaseScore.ToString()}");
                                        //}
                                        //else
                                        //{
                                        //    CalcLogEntry($"Base Value is manualy set as a Group...");
                                        //    totalGroupControlBaseScore = parentNodePreviousControlBaseScore;
                                        //}
                                    }


                                    CalcLogEntry($"New Group Base Value is:{totalGroupControlBaseScore.ToString()}");

                                    Double TotalGroupCalculatedValue = Convert.ToDouble(CurrentNodeCalculatedValue.ToString()) + edgeImpactedNodeCalculatedValue - (previousNodeCalculatedValue * Convert.ToDouble(edgeStrengthScore.ToString()));

                                    //Calculate Group Assesed Value
                                    CalcLogEntry($"Calculating Group Assessed Value...");
                                    CalcLogEntry($"Current Node Assessed Value is: {nodeControlAssessedScore.ToString()}");
                                    CalcLogEntry($"Impact modified Assessed Value from Parent Node is: {edgeImpactedNodeAssessedValue.ToString()}");
                                    CalcLogEntry($"Previous Calculated Assessed from Parent Node is: {previousNodeControlAssessedScore.ToString()}");
                                    CalcLogEntry($"TotalGroupControlAssessedScore = GroupedgeStrengthValue + edgeImpactedNodeAssessedValue - (previousNodeedgeStrengthValueResult * edgeStrengthValue)");
                                    CalcLogEntry($"TotalGroupControlAssessedScore = {CurrentNodeControlAssessedScore.ToString()} + {edgeImpactedNodeAssessedValue.ToString()} - ({previousNodeControlAssessedScore.ToString()} * {edgeStrengthScore.ToString()})");
                                    Double TotalGroupControlAssessedScore = Convert.ToDouble(CurrentNodeControlAssessedScore.ToString()) + edgeImpactedNodeAssessedValue - (previousNodeControlAssessedScore * Convert.ToDouble(edgeStrengthScore.ToString()));
                                    
                                    if (TotalGroupControlAssessedScore < 0)
                                    {
                                       CalcLogEntry($"TotalGroupControlAssessedScore < 0, setting to 0");
                                        TotalGroupControlAssessedScore = 0;
                                    }
                                    CalcLogEntry($" TotalGroupControlAssessedScore = {TotalGroupControlAssessedScore.ToString()}");


                                    if (nodeBehaviour == "Sum")
                                    {
                                        
                                        // Calculate Group Calculated Value
                                       CalcLogEntry($"Calculating Group Calculated Value...");
                                       CalcLogEntry($"Current Node Calculated Value is: {CurrentNodeCalculatedValue.ToString()}");
                                       CalcLogEntry($"Impact modified Calculated Value from Parent Node is: {edgeImpactedNodeCalculatedValue.ToString()}");
                                       CalcLogEntry($"Previous Calculated Value from Parent Node is: {previousNodeCalculatedValue.ToString()}");
                                       CalcLogEntry($"TotalGroupCalculatedValue = 100 / (GroupcontrolBaseScore / GroupedgeStrengthValue)");
                                       CalcLogEntry($"TotalGroupCalculatedValue = 100 / {CurrentNodeControlBaseScore.ToString()} / ({CurrentNodeControlBaseScore.ToString()} / {CurrentNodeControlAssessedScore.ToString()})");


                                        if (!s_calculatedValues.ContainsKey(CurrentNodeID + ":" + PreviousNodeID))
                                        {
                                            CalcLogEntry($"s_calculatedValues does not contains key {CurrentNodeID + ":" + PreviousNodeID}");
                                            s_calculatedValues.Add(CurrentNodeID + ":" + PreviousNodeID, edgeImpactedNodeCalculatedValue);
                                            CalcLogEntry($"Added {CurrentNodeID + ":" + PreviousNodeID + ", " + edgeImpactedNodeCalculatedValue} to s_calculatedValues contains");
                                        }
                                        else
                                        {
                                            CalcLogEntry($"s_calculatedValues already contains key {CurrentNodeID + ":" + PreviousNodeID}");
                                            s_calculatedValues[CurrentNodeID + ":" + PreviousNodeID] = edgeImpactedNodeCalculatedValue;
                                            CalcLogEntry($"Updated {CurrentNodeID + ":" + PreviousNodeID + ", " + edgeImpactedNodeCalculatedValue} in s_calculatedValues contains");
                                        }

                                        TotalGroupCalculatedValue = s_calculatedValues[CurrentNodeID + ":" + PreviousNodeID];

                                        //if (!s_calculatedValues.ContainsKey(CurrentNodeID))
                                        //    s_calculatedValues.Add(CurrentNodeID, edgeImpactedNodeCalculatedValue);
                                        //else
                                        //{
                                        //    double tempNodeCalcValue = s_calculatedValues[CurrentNodeID];
                                        //    tempNodeCalcValue = tempNodeCalcValue + edgeImpactedNodeCalculatedValue;
                                        //    s_calculatedValues[CurrentNodeID] = tempNodeCalcValue;
                                        //}
                                        //TotalGroupCalculatedValue = s_calculatedValues[CurrentNodeID];

                                    }
                                    if (nodeBehaviour == "High Water Mark")
                                    {
                                       CalcLogEntry($"Current Node controlBaseScore is: {CurrentNodeControlBaseScore}");
                                       CalcLogEntry($"Current Node nodeCalculatedValue is: {CurrentNodeCalculatedValue}");
                                       CalcLogEntry($"Current Node CurrentNodeControlAssessedScore is: {CurrentNodeControlAssessedScore}");

                                       CalcLogEntry($"New Node NodeImpactedcontrolBaseScore is: {edgeImpactedNodeControlBaseScore}");
                                       CalcLogEntry($"New Node NodeImpactedCalculatedValue is: {edgeImpactedNodeCalculatedValue}");
                                       CalcLogEntry($"New Node edgeImpactedNodeAssessedValue is: {edgeImpactedNodeAssessedValue}");

                                        if (edgeImpactedNodeCalculatedValue >= CurrentNodeCalculatedValue)
                                        {
                                           CalcLogEntry($"NodeImpactedCalculatedValue is Higher than CurrentNodecontrolBaseScore");
                                           CalcLogEntry($"Setting Higestest Values...");
                                            totalGroupControlBaseScore = edgeImpactedNodeControlBaseScore;
                                            TotalGroupCalculatedValue = edgeImpactedNodeCalculatedValue;
                                            TotalGroupControlAssessedScore = edgeImpactedNodeAssessedValue;
                                        }
                                        else
                                        {
                                           CalcLogEntry($"NodeImpactedCalculatedValue is Lower than CurrentNodecontrolBaseScore");
                                           CalcLogEntry($"Keeping Current Values Higestest Values...");
                                            totalGroupControlBaseScore = CurrentNodeControlBaseScore;
                                            TotalGroupCalculatedValue = CurrentNodeCalculatedValue;
                                            TotalGroupControlAssessedScore = CurrentNodeControlAssessedScore;
                                        }
                                    }
                                    if (nodeBehaviour == "Low Water Mark")
                                    {
                                       CalcLogEntry($"Current Node controlBaseScore is: {CurrentNodeControlBaseScore}");
                                       CalcLogEntry($"Current Node nodeCalculatedValue is: {CurrentNodeCalculatedValue}");
                                       CalcLogEntry($"Current Node CurrentNodeControlAssessedScore is: {CurrentNodeControlAssessedScore}");

                                       CalcLogEntry($"New Node NodeImpactedcontrolBaseScore is: {edgeImpactedNodeControlBaseScore}");
                                       CalcLogEntry($"New Node NodeImpactedCalculatedValue is: {edgeImpactedNodeCalculatedValue}");
                                       CalcLogEntry($"New Node edgeImpactedNodeAssessedValue is: {edgeImpactedNodeAssessedValue}");

                                        if ((edgeImpactedNodeCalculatedValue <= CurrentNodeCalculatedValue) || (NodePairs.IndexOf(currentNodePair) == -1))
                                        {
                                            if (!s_calculatedValues.ContainsKey(CurrentNodeID))
                                                s_calculatedValues.Add(CurrentNodeID, edgeImpactedNodeCalculatedValue);
                                            else
                                            {
                                                double tempNodeCalcValue = s_calculatedValues[CurrentNodeID];
                                                if (edgeImpactedNodeCalculatedValue < tempNodeCalcValue)
                                                {
                                                    tempNodeCalcValue = edgeImpactedNodeCalculatedValue;
                                                    s_calculatedValues[CurrentNodeID] = tempNodeCalcValue;
                                                }
                                                else
                                                {
                                                    edgeImpactedNodeCalculatedValue = tempNodeCalcValue;
                                                }

                                                s_calculatedValues[CurrentNodeID] = tempNodeCalcValue;
                                            }
                                            
                                            TotalGroupCalculatedValue = s_calculatedValues[CurrentNodeID];
                                           CalcLogEntry($"NodeImpactedCalculatedValue is Lower than CurrentNodecontrolBaseScore");
                                           CalcLogEntry($"Setting Higestest Values...");
                                            totalGroupControlBaseScore = edgeImpactedNodeControlBaseScore;
                                            TotalGroupCalculatedValue = edgeImpactedNodeCalculatedValue;
                                            TotalGroupControlAssessedScore = edgeImpactedNodeAssessedValue;
                                        }
                                        else
                                        {
                                           CalcLogEntry($"NodeImpactedCalculatedValue is Higher than CurrentNodecontrolBaseScore");
                                           CalcLogEntry($"Keeping Current Values Higestest Values...");
                                            totalGroupControlBaseScore = CurrentNodeControlBaseScore;
                                            TotalGroupCalculatedValue = CurrentNodeCalculatedValue;
                                            TotalGroupControlAssessedScore = CurrentNodeControlAssessedScore;
                                        }
                                    }
                                    if (nodeBehaviour == "Average")
                                    {
                                       CalcLogEntry($"Current Node controlBaseScore is: {CurrentNodeControlBaseScore}");
                                       CalcLogEntry($"Current Node nodeCalculatedValue is: {CurrentNodeCalculatedValue}");
                                       CalcLogEntry($"Current Node CurrentNodeControlAssessedScore is: {CurrentNodeControlAssessedScore}");

                                       CalcLogEntry($"New Node NodeImpactedcontrolBaseScore is: {edgeImpactedNodeControlBaseScore}");
                                       CalcLogEntry($"New Node NodeImpactedCalculatedValue is: {edgeImpactedNodeCalculatedValue}");
                                       CalcLogEntry($"New Node edgeImpactedNodeAssessedValue is: {edgeImpactedNodeAssessedValue}");

                                        //check if value already exists
                                        bool flagFound = false;
                                        for (int j = 0; j < s_AverageCalculatedValues.Count; j++)
                                        {
                                            if (s_AverageCalculatedValues[j].Key == currentNodePair) //Already have a value for the keypair
                                            {
                                                flagFound = true;
                                                var newEntry = new KeyValuePair<string, string>(currentNodePair, edgeImpactedNodeCalculatedValue.ToString());  // replace the exiting keypair with a new keypair
                                                s_AverageCalculatedValues[j] = newEntry;
                                                break;
                                            }
                                        }
                                        if (flagFound == false)        
                                            s_AverageCalculatedValues.Add(new KeyValuePair<string, string>(currentNodePair, edgeImpactedNodeCalculatedValue.ToString()));  //This is the first time this KeyPair has been processed
                                        
                                        //Now work out the average
                                        double tempTotal = 0;
                                        int tempCount = 0;
                                        foreach (var pair in s_AverageCalculatedValues)
                                        {                                            
                                           CalcLogEntry($"AverageCalculatedValues: {pair.Key} - {pair.Value}");
                                            if (pair.Key.StartsWith(CurrentNodeID))
                                            {
                                                tempTotal += double.Parse(pair.Value);
                                                tempCount++;
                                            }
                                        }
                                        TotalGroupCalculatedValue = tempTotal / tempCount;
                                    }

                                    
                                    if (TotalGroupCalculatedValue < 0)
                                    {
                                       CalcLogEntry($"TotalGroupCalculatedValue < 0, setting to 0");
                                        TotalGroupCalculatedValue = 0;
                                    }
                                    CalcLogEntry($"New Group Calculated Value is:{TotalGroupCalculatedValue.ToString()}");


                                    //Update the Graph Group Value
                                    CalcLogEntry($"Updating Previous Node Values...");
                                    CalcLogEntry($"Previous Base Value set to: {CurrentNodeControlBaseScore.ToString()}");

                                    GraphUtil.SetNodeData(CurrentNodeID, "previouscalculatedValue", CurrentNodeCalculatedValue.ToString());
                                    CalcLogEntry($"Previous Calculated Values set to: {CurrentNodeCalculatedValue.ToString()}");

                                    GraphUtil.SetNodeData(CurrentNodeID, "previouscalculatedValue", CurrentNodeControlAssessedScore.ToString());
                                    CalcLogEntry($"Previous Assessed Value set to: {CurrentNodeControlAssessedScore.ToString()}");


                                    //Set new values
                                    CalcLogEntry($"Updating Current Node Values...");
                                    GraphUtil.SetNodeData(CurrentNodeID, "controlBaseScore", totalGroupControlBaseScore.ToString());
                                    GraphUtil.AddToNodeScores(CurrentNodeID + ":controlBaseScore", totalGroupControlBaseScore);
                                    CalcLogEntry($"    Current Base Value set to: {totalGroupControlBaseScore.ToString()}");

                                    //if (TotalGroupCalculatedValue > 100) TotalGroupCalculatedValue = 100;
                                    GraphUtil.SetNodeData(CurrentNodeID, "calculatedValue", TotalGroupCalculatedValue.ToString());
                                    GraphUtil.AddToNodeScores(CurrentNodeID + ":calculatedValue", TotalGroupCalculatedValue);
                                    CalcLogEntry($"    Current Calculated Value set to: {TotalGroupCalculatedValue.ToString()}");
                                    
                                    GraphUtil.SetNodeData(CurrentNodeID, "edgeStrengthValue", TotalGroupControlAssessedScore.ToString());
                                    GraphUtil.AddToNodeScores(CurrentNodeID + ":edgeStrengthValue", TotalGroupControlAssessedScore);

                                    CalcLogEntry($"    Current Assessed Value set to: {TotalGroupControlAssessedScore.ToString()}");

                                    //Values updated - Remove Node from already processed pairs
                                   CalcLogEntry($"Node values have been updated, Removing from Pair list...");
                                   CalcLogEntry($"Getting Outgoers for Node {CurrentNodeID}...");

                                    var allOutgoers = GraphUtil.GetChildNodes(CurrentNodeID);
                                   CalcLogEntry($"Found {allOutgoers.Count} outgoers for Node {CurrentNodeID}");
                                    foreach (var ID in allOutgoers)
                                    {
                                        string tempNodePair = ID.ToString() + " > " + CurrentNodeID;
                                       CalcLogEntry($"Looking for Node Pair: {tempNodePair}");
                                        if (NodePairs.IndexOf(tempNodePair) == -1)
                                        {
                                           CalcLogEntry($"Node Pair is not is the Pair list: {tempNodePair}");
                                        }

                                        while (NodePairs.IndexOf(tempNodePair) != -1)
                                        {
                                            NodePairs.RemoveAt(NodePairs.IndexOf(tempNodePair));
                                           CalcLogEntry($"Removing Node Pair: {tempNodePair}");
                                        }
                                    }

                                    //Add Nodes to Pair List
                                    if (nodeType.ToString().ToLower() == "objective" && parentNodeType.ToString().ToLower() == "control")
                                    {
                                        NodePairs.Add(currentNodePair);
                                       CalcLogEntry($"Added to Objective-Control Pair list: '{currentNodePair}'");
                                    }

                                    if (nodeType.ToString().ToLower() == "objective" && parentNodeType.ToString().ToLower() == "objective")
                                    {
                                        NodePairs.Add(currentNodePair);
                                       CalcLogEntry($"Added to Objective-Objective Pair list: '{currentNodePair}'");
                                    }

                                    if (nodeType.ToString().ToLower() == "group" && parentNodeType.ToString().ToLower() == "objective")
                                    {
                                        NodePairs.Add(currentNodePair);
                                       CalcLogEntry($"Added to Group-Objective Pair list: '{currentNodePair}'");
                                    }

                                    if (nodeType.ToString().ToLower() == "asset" && parentNodeType.ToString().ToLower() == "group")
                                    {
                                        NodePairs.Add(currentNodePair);
                                       CalcLogEntry($"Added to Asset-Group Pair list: '{currentNodePair}'");
                                    }

                                    if (nodeType.ToString().ToLower() == "asset" && parentNodeType.ToString().ToLower() == "objective")
                                    {
                                        NodePairs.Add(currentNodePair);
                                       CalcLogEntry($"Added to Asset-Objective Pair list: '{currentNodePair}'");
                                    }


                                    if (nodeType.ToString().ToLower() == "group" && parentNodeType.ToString().ToLower() == "group")
                                    {
                                        NodePairs.Add(currentNodePair);
                                       CalcLogEntry($"Added to Group-Group Pair list: '{currentNodePair}'");
                                    }

                                    if (nodeType.ToString().ToLower() == "asset" && parentNodeType.ToString().ToLower() == "asset")
                                    {
                                        NodePairs.Add(currentNodePair);
                                       CalcLogEntry($"Added to Asset-Asset Pair list: '{currentNodePair}'");
                                    }

                                   CalcLogEntry($"Getting Node Children...");

                                    var TargetNodes2 = GraphUtil.GetChildNodes(CurrentNodeID);
                                  
                                   CalcLogEntry($"Found {TargetNodes2.Count} Children...");
                                    foreach (var ID in TargetNodes2)
                                    {
                                        string tempNodePair = CurrentNodeID + " > " + ID.ToString();
                                       CalcLogEntry($"Looking for NodePair2 Pair: {tempNodePair}");
                                        if (NodePairs2.IndexOf(tempNodePair) == -1)
                                        {
                                           CalcLogEntry($"Node Pair is not is the NodePair2 list: {tempNodePair}");
                                            //Set Node previous values to Zero
                                           CalcLogEntry($"Setting Node previous values to Zero: '{TempNode}'");
                                            GraphUtil.zeroNodePreviousValues(TempNode);
                                        }
                                        else
                                        {
                                           CalcLogEntry($"Node Pair found in the NodePair2 list: {tempNodePair}");
                                           CalcLogEntry($"Node Still has Nodes to Process.");
                                        }
                                    }
                                }
                                else
                                    if (NodePairs.IndexOf(currentNodePair) != -1)
                                {
                                   CalcLogEntry($"Already Processed Node Pair: '{currentNodePair}'");
                                }

                                if (currentNodePair.Contains("No-Node"))
                                {
                                   CalcLogEntry($"Node has no Parent Node: '{currentNodePair}'");
                                }
                            }
                            else
                            {
                               CalcLogEntry($"Group disabled, not processing.");
                            }

                        }        // Compliance processing group, asset or objective nodes


                        if (nodeType.ToString().ToLower() == "vulnerability-group")  // Node is a vulnerability group
                        {
                            double vulnerabilityScore = 0;
                            double vulnerabilityMitigatedScore = 0;
                            double likelihoodScore = 0;

                            //vulnerability groups take likelihood and vulnerability scores from parent vulnerability nodes

                            //Get parent Nodes
                            List<string> parentVulnerabilityGUIDs = new List<string>();

                            parentVulnerabilityGUIDs = GraphUtil.GetParentVulnerabilityNodes(CurrentNodeID);
                            foreach (string vulnerabilityGUID in parentVulnerabilityGUIDs)
                            {
                                // Get vulnerability score from parent node
                                // Save Value to vulnerability score Node / Parent Node list
                                if (!s_vulnerabilityScores.ContainsKey(CurrentNodeID + ":" + vulnerabilityGUID))
                                    s_vulnerabilityScores.Add(CurrentNodeID + ":" + vulnerabilityGUID, GraphUtil.GetVulnerabilityNodeScore(vulnerabilityGUID));
                                else
                                {
                                    s_vulnerabilityScores[CurrentNodeID + ":" + vulnerabilityGUID] = GraphUtil.GetVulnerabilityNodeScore(vulnerabilityGUID);
                                }

                                if (!s_vulnerabilityMitigatedScores.ContainsKey(CurrentNodeID + ":" + vulnerabilityGUID))
                                    s_vulnerabilityMitigatedScores.Add(CurrentNodeID + ":" + vulnerabilityGUID, GraphUtil.GetVulnerabilityMitigatedNodeScore(vulnerabilityGUID));
                                else
                                {
                                    s_vulnerabilityMitigatedScores[CurrentNodeID + ":" + vulnerabilityGUID] = GraphUtil.GetVulnerabilityMitigatedNodeScore(vulnerabilityGUID);
                                }

                                // Get likelihood score from parent node
                                // Save Value to likelihood score Node / Parent Node list
                                if (!s_likelihoodScores.ContainsKey(CurrentNodeID + ":" + vulnerabilityGUID))
                                    s_likelihoodScores.Add(CurrentNodeID + ":" + vulnerabilityGUID, GraphUtil.GetVulnerabilityNodeLikelihoodScore(vulnerabilityGUID));
                                else
                                {
                                    s_likelihoodScores[CurrentNodeID + ":" + vulnerabilityGUID] = GraphUtil.GetVulnerabilityNodeLikelihoodScore(vulnerabilityGUID);
                                }

                            }

                            //Now calculate the totals based on the vulnrability group behaviour
                            string nodeBehaviour = GraphUtil.GetnodeBehaviour(CurrentNodeID);
                            if (nodeBehaviour == "Sum")
                            {
                               foreach (string pairID in s_vulnerabilityScores.Keys)  //<< Will this work???   
                                    if (pairID.StartsWith(CurrentNodeID))
                                        vulnerabilityScore += s_vulnerabilityScores[pairID];

                                foreach (string pairID in s_vulnerabilityMitigatedScores.Keys)  //<< Will this work???   
                                    if (pairID.StartsWith(CurrentNodeID))
                                        vulnerabilityMitigatedScore += s_vulnerabilityScores[pairID];

                                foreach (string pairID in s_likelihoodScores.Keys)  //<< Will this work???   
                                    if (pairID.StartsWith(CurrentNodeID))
                                        likelihoodScore += s_likelihoodScores[pairID];
                            }


                            if (nodeBehaviour == "High Water Mark")
                            {

                                foreach (string pairID in s_vulnerabilityScores.Keys)  //<< Will this work???   
                                    if (pairID.StartsWith(CurrentNodeID))
                                        if (s_vulnerabilityScores[pairID] > vulnerabilityScore)
                                            vulnerabilityScore = s_vulnerabilityScores[pairID];


                                foreach (string pairID in s_vulnerabilityMitigatedScores.Keys)  //<< Will this work???   
                                    if (pairID.StartsWith(CurrentNodeID))
                                        if (s_vulnerabilityMitigatedScores[pairID] > vulnerabilityScore)
                                            vulnerabilityMitigatedScore = s_vulnerabilityMitigatedScores[pairID];

                                foreach (string pairID in s_likelihoodScores.Keys)  //<< Will this work???   
                                    if (pairID.StartsWith(CurrentNodeID))
                                        if (s_likelihoodScores[pairID] > likelihoodScore)
                                            likelihoodScore = s_likelihoodScores[pairID];


                            }

                            if (nodeBehaviour == "Low Water Mark")
                            {
                                vulnerabilityScore = 999999;
                                vulnerabilityMitigatedScore = 999999;
                                likelihoodScore = 999999;

                                foreach (string pairID in s_vulnerabilityScores.Keys)  //<< Will this work???   
                                    if (pairID.StartsWith(CurrentNodeID))
                                        if (s_vulnerabilityScores[pairID] < vulnerabilityScore)
                                            vulnerabilityScore = s_vulnerabilityScores[pairID];

                                foreach (string pairID in s_vulnerabilityMitigatedScores.Keys)  //<< Will this work???   
                                    if (pairID.StartsWith(CurrentNodeID))
                                        if (s_vulnerabilityMitigatedScores[pairID] < vulnerabilityMitigatedScore)
                                            vulnerabilityMitigatedScore = s_vulnerabilityMitigatedScores[pairID];

                                foreach (string pairID in s_likelihoodScores.Keys)  //<< Will this work???   
                                    if (pairID.StartsWith(CurrentNodeID))
                                        if (s_likelihoodScores[pairID] < likelihoodScore)
                                            likelihoodScore = s_likelihoodScores[pairID];

                            }


                            if (nodeBehaviour == "Average")
                            {
                                double vulnerabilityNodeMax = 0;
                                double vulnerabilityNodeMitigatedMax = 0;
                                double vulnerabilityNodeLikelihoodMax = 0;
                                double vulnerabilityNodeAverage = 0;
                                double vulnerabilityNodeMitigatedAverage = 0;
                                double vulnerabilityNodeLikelihoodAverage = 0;

                                //Get the sums 
                                foreach (string vulnerabilityGUID in parentVulnerabilityGUIDs)
                                {
                                    vulnerabilityNodeMax += GraphUtil.GetVulnerabilityNodeScore(vulnerabilityGUID);
                                    vulnerabilityNodeMitigatedMax += GraphUtil.GetVulnerabilityNodeMitigatedScore(vulnerabilityGUID);
                                    vulnerabilityNodeLikelihoodMax += GraphUtil.GetVulnerabilityNodeLikelihoodScore(vulnerabilityGUID);
                                }

                                // now devide by the count to get the average/
                                vulnerabilityNodeAverage = vulnerabilityNodeMax / parentVulnerabilityGUIDs.Count();
                                vulnerabilityNodeMitigatedAverage= vulnerabilityNodeMitigatedMax / parentVulnerabilityGUIDs.Count();
                                vulnerabilityNodeLikelihoodAverage = vulnerabilityNodeLikelihoodMax / parentVulnerabilityGUIDs.Count();

                                vulnerabilityScore = vulnerabilityNodeAverage;
                                vulnerabilityMitigatedScore = vulnerabilityNodeMitigatedAverage;
                                likelihoodScore = vulnerabilityNodeLikelihoodAverage;
                            }


                            // save the Node data.

                            GraphUtil.SetNodeData(CurrentNodeID, "VulnerabilityScore", vulnerabilityScore.ToString());
                            GraphUtil.AddToNodeScores(CurrentNodeID + ":VulnerabilityScore", vulnerabilityScore);
                            GraphUtil.SetNodeData(CurrentNodeID, "vulnerabilityMitigatedScore", vulnerabilityMitigatedScore.ToString());
                            GraphUtil.AddToNodeScores(CurrentNodeID + ":vulnerabilityMitigatedScore", vulnerabilityMitigatedScore);
                            GraphUtil.SetNodeData(CurrentNodeID, "likelihoodScore", likelihoodScore.ToString());
                            GraphUtil.AddToNodeScores(CurrentNodeID + ":likelihoodScore", likelihoodScore);
                        }

                        // Actor Node *******************************************************************************************************************
                        if (nodeType.ToString().ToLower() == "actor") // Risk processing Node is an Actor
                        {
                            double EdgeImpactedValue = 0;
                           CalcLogEntry($"+++ {nodeType.ToString()} Node +++");
                           CalcLogEntry($"Checking if Node is enabled...");
                            var nodeEnabled = GraphUtil.IsNodeEnabled(CurrentNodeID);

                            if (nodeEnabled.ToString().ToLower() == "true")
                            {
                               CalcLogEntry($"Node is enabled...");
                                //Get Actor Score
                               CalcLogEntry($"Getting Actor Score for Node...");
                                var actorScore = GraphUtil.GetActorNodeScore(CurrentNodeID);
                               CalcLogEntry($"Actor Score is: {actorScore}");

                                double NodecontrolBaseScoreResult = 0;
                                double NodeCalculatedValueResult = 0;
                                double NodeedgeStrengthValueResult = 0;
                                string parentNodeType = "";

                                if (LastNodeID != CurrentNodeID && LastNodeID != "")
                                {
                                    try
                                    {
                                        //Get Parent Node Current Values
                                       CalcLogEntry($"Getting Parent Node values from Node:{LastNodeID}  (1011)");
                                        var jsNodecontrolBaseScoreResult = GraphUtil.GetNodeBaseScore(LastNodeID);

                                       CalcLogEntry($"Parent Node Base Value is:{jsNodecontrolBaseScoreResult.ToString()}");
                                        var jsNodeCalculatedValueResult = GraphUtil.GetNodeCalculatedValue(LastNodeID);

                                       CalcLogEntry($"Parent Node Calculated Value is:{jsNodeCalculatedValueResult.ToString()}");
                                        var jsNodeedgeStrengthValueResult = GraphUtil.GetEdgeStrengthScore(LastNodeID);

                                       CalcLogEntry($"Parent Node Assessed Value is:{jsNodeedgeStrengthValueResult.ToString()}");
                                        var jsparentNodeType = GraphUtil.GetNodeType(LastNodeID);

                                       CalcLogEntry($"Parent Node Type is:{jsparentNodeType.ToString()} (0002)");

                                        //Get Parent Node Previous Values
                                       CalcLogEntry($"Getting Parent Node previous values from Node:{LastNodeID}  (1012)");

                                        var jsparentNodePreviouscontrolBaseScore = GraphUtil.GetPreviousNodeControlBaseScore(LastNodeID);

                                       CalcLogEntry($"Parent Node previous Base Value is:{jsparentNodePreviouscontrolBaseScore.ToString()}");

                                        var jspreviousNodeCalculatedValueResult = GraphUtil.GetPreviousNodeCalculatedValue(LastNodeID);

                                       CalcLogEntry($"Parent Node previous Calculated Value is:{jspreviousNodeCalculatedValueResult.ToString()}");

                                        var jspreviousNodeedgeStrengthValueResult = GraphUtil.GetPreviousNodeAssessedValue(LastNodeID);

                                       CalcLogEntry($"Parent Node previous Assessed Value is:{jspreviousNodeedgeStrengthValueResult.ToString()}");

                                        var jspreviousNodeObjectiveTarget = GraphUtil.GetNodeObjectiveTargetType(LastNodeID);

                                       CalcLogEntry($"Parent Node previous Objective Target is:{jspreviousNodeObjectiveTarget.ToString()}");

                                        NodecontrolBaseScoreResult = Convert.ToDouble(jsNodecontrolBaseScoreResult.ToString());
                                        NodeCalculatedValueResult = Convert.ToDouble(jsNodeCalculatedValueResult.ToString());
                                        NodeedgeStrengthValueResult = Convert.ToDouble(jsNodeedgeStrengthValueResult.ToString());
                                        parentNodeType = jsparentNodeType.ToString().ToLower();
                                    }
                                    catch
                                    {
                                       CalcLogEntry($"No prevous Node found! Setting values to 0...");
                                        NodecontrolBaseScoreResult = 0;
                                       CalcLogEntry($"Parent Node Base Value is: 0");
                                        NodeCalculatedValueResult = 0;
                                       CalcLogEntry($"Parent Node Calculated Value is: 0");
                                        NodeedgeStrengthValueResult = 0;
                                       CalcLogEntry($"Parent Node Assessed Value is: 0");
                                    }
                                }
                                else
                                {
                                    //Get Parent Node Current Values
                                   CalcLogEntry($"No prevous Node found! Setting values to 0...");
                                    NodecontrolBaseScoreResult = 0;
                                   CalcLogEntry($"Parent Node Base Value is: 0");
                                    NodeCalculatedValueResult = 0;
                                   CalcLogEntry($"Parent Node Calculated Value is: 0");
                                    NodeedgeStrengthValueResult = 0;
                                   CalcLogEntry($"Parent Node Assessed Value is: 0");

                                }

                                //Get Edge Strenth Value
                               CalcLogEntry($"Previous Edge is: {PreviousEdgeID}");
                                var edgeStrengthValue = GraphUtil.GetEdgeStrengthScore(PreviousEdgeID);
                                //JavascriptResponse edgeStrengthValue = await _browser.EvaluateScriptAsync($"getedgeStrengthValue('{PreviousEdgeID}');");
                               CalcLogEntry($"Previous Edge Strenth Value is: {edgeStrengthValue.ToString()}"); // e.g. 0.25

                               CalcLogEntry($"Calculating Impacted values...");

                                EdgeImpactedValue = 0 - (NodecontrolBaseScoreResult - NodeCalculatedValueResult) * Convert.ToDouble(edgeStrengthValue.ToString());
                                if (EdgeImpactedValue < 0)
                                {
                                   CalcLogEntry($"EdgeImpactedValue < 0, setting to 0");
                                    EdgeImpactedValue = 0;
                                }
                               CalcLogEntry($" |---Node Base Value is: {NodecontrolBaseScoreResult.ToString()}");
                               CalcLogEntry($" |---Node Calculated Value is:{NodeCalculatedValueResult.ToString()}");
                               CalcLogEntry($" |---Edge Assesed Value is :{edgeStrengthValue.ToString()}");
                               CalcLogEntry($"EdgeImpactedValue = 0 - (NodecontrolBaseScoreResult - NodeCalculatedValueResult) * edgeStrengthValue");
                               CalcLogEntry($"EdgeImpactedValue = 0 - ({NodecontrolBaseScoreResult.ToString()} - {NodeCalculatedValueResult.ToString()}) * {edgeStrengthValue.ToString()}");
                               CalcLogEntry($"EdgeImpactedValue = {EdgeImpactedValue.ToString()}");


                                Double NodeImpactedCalculatedValue = NodeCalculatedValueResult * Convert.ToDouble(edgeStrengthValue.ToString());
                                if (NodeImpactedCalculatedValue < 0)
                                {
                                   CalcLogEntry($"NodeImpactedCalculatedValue < 0, setting to 0");
                                    NodeImpactedCalculatedValue = 0;
                                }
                               CalcLogEntry($"NodeImpactedCalculatedValue = NodeCalculatedValueResult * edgeStrengthValue");
                               CalcLogEntry($"NodeImpactedCalculatedValue = {NodeImpactedCalculatedValue.ToString()} * {edgeStrengthValue.ToString()}");
                               CalcLogEntry($"NodeImpactedCalculatedValue = {NodeImpactedCalculatedValue.ToString()}");

                                //Actor Mitigated Score Calculation
                                //(actorScore x (100 – Node Impacted Calculated Value)) / 100 =
                               CalcLogEntry($"Calculating Actor Mitigated Score...");
                                Double actorMitigatedScore = actorScore * (100 - NodeImpactedCalculatedValue) / 100;
                                if (actorMitigatedScore < 0)
                                {
                                   CalcLogEntry($"actorMitigatedScore < 0, setting to 0");
                                    actorMitigatedScore = 0;
                                }
                               CalcLogEntry($"actorMitigatedScore = actorScore * (100 - NodeImpactedCalculatedValue) / 100");
                               CalcLogEntry($"actorMitigatedScore = {actorScore.ToString()} * (100 - {NodeImpactedCalculatedValue.ToString()}) / 100");
                               CalcLogEntry($"actorMitigatedScore = {actorMitigatedScore.ToString()}");

                                if (actorMitigatedScore < 0)
                                {
                                   CalcLogEntry($"actorMitigatedScore is less than 0. Setting actorMitigatedScore to 0...");
                                    actorMitigatedScore = 0;
                                }

                                //Set the Actor Mitigated Score 
                                //Need to check if a previous lower mitgaed score exits
                               CalcLogEntry($"Checking for existing lower Actor Mitigated Score...");
                                double existingActorMitigatedScore = GraphUtil.GetActorNodeMitigatedScore(CurrentNodeID);
                               CalcLogEntry($"Existing Actor Mitigated Score is: {existingActorMitigatedScore}");
                                //20/4/23
                                //GraphUtil.SetNodeData(CurrentNodeID, "actorMitigatedScore", GraphUtil.CalculateModeFromDistributionData(CurrentNodeID + ":Mitigated").ToString());
                                GraphUtil.SetNodeData(CurrentNodeID, "actorMitigatedScore", actorMitigatedScore.ToString());
                                GraphUtil.AddToNodeScores(CurrentNodeID + ":actorMitigatedScore", actorMitigatedScore);


                            }

                        }            // Risk calculate Attack
                        
                        // Attack Node *******************************************************************************************************************
                        if (nodeType.ToString().ToLower() == "attack") // Risk processing Node is an Attack
                        {
                           CalcLogEntry($"+++ {nodeType.ToString()} Node +++");
                           CalcLogEntry($"Checking if Node is enabled...");
                            var nodeEnabled = GraphUtil.IsNodeEnabled(CurrentNodeID);

                            if (nodeEnabled.ToString().ToLower() == "true")
                            {
                               CalcLogEntry($"Node is enabled...");
                                //Get attack Score
                               CalcLogEntry($"Getting Attack Score for Node...");
                                var attackScore = GraphUtil.GetAttackNodeScore(CurrentNodeID);
                               CalcLogEntry($"Attack Score is: {attackScore}");

                                //Get attack Confidentiality Score
                               CalcLogEntry($"Getting Attack Confidentiality Score for Node...");
                                var attackConfidentialityScore = GraphUtil.GetAttackConfidentialityScore(CurrentNodeID);
                               CalcLogEntry($"Attack Confidentiality Score is: {attackConfidentialityScore}");

                                //Get attack Integrity Score
                               CalcLogEntry($"Getting Attack Integrity Score for Node...");
                                var attackIntegrityScore = GraphUtil.GetAttackIntegrityScore(CurrentNodeID);
                               CalcLogEntry($"Attack Integrity Score is: {attackIntegrityScore}");

                                //Get attack Availibility Score
                               CalcLogEntry($"Getting Attack Availibility Score for Node...");
                                var attackAvailibilityScore = GraphUtil.GetAttackAvailibilityScore(CurrentNodeID);
                               CalcLogEntry($"Attack Availibility Score is: {attackAvailibilityScore}");

                                //Get attack Accountability Score
                               CalcLogEntry($"Getting Attack Accountability Score for Node...");
                                var attackAccountabilityScore = GraphUtil.GetAttackAccountabilityScore(CurrentNodeID);
                               CalcLogEntry($"Attack Accountability Score is: {attackAccountabilityScore}");

                                double NodecontrolBaseScoreResult = 0;
                                double NodeCalculatedValueResult = 0;
                                double NodeedgeStrengthValueResult = 0;
                                double actorMitigatedScore = 0;
                                string parentNodeType = "";



                                if (LastNodeID != CurrentNodeID && LastNodeID != "")
                                {
                                    try
                                    {
                                        //Get Parent Node Current Values
                                       CalcLogEntry($"Getting Parent Node values from Node:{LastNodeID}  (1013)");
                                        var jsNodecontrolBaseScoreResult = GraphUtil.GetNodeBaseScore(LastNodeID);

                                       CalcLogEntry($"Parent Node Base Value is:{jsNodecontrolBaseScoreResult.ToString()}");
                                        var jsNodeCalculatedValueResult = GraphUtil.GetNodeCalculatedValue(LastNodeID);

                                       CalcLogEntry($"Parent Node Calculated Value is:{jsNodeCalculatedValueResult.ToString()}");
                                        var jsNodeedgeStrengthValueResult = GraphUtil.GetEdgeStrengthScore(LastNodeID);

                                       CalcLogEntry($"Parent Node Assessed Value is:{jsNodeedgeStrengthValueResult.ToString()}");
                                        var jsparentNodeType = GraphUtil.GetNodeType(LastNodeID);

                                       CalcLogEntry($"Parent Node Type is:{jsparentNodeType} (0003)");

                                        parentNodeType = jsparentNodeType.ToString().ToLower();

                                        if (parentNodeType == "actor")
                                        {
                                            actorMitigatedScore = GraphUtil.GetActorNodeMitigatedScore(LastNodeID);
                                           CalcLogEntry($"Parent Node Actor Mitigated Score is :{actorMitigatedScore}");

                                           CalcLogEntry($"Checking highest previous Attack Actor mitigated score...");
                                            double tempactorMitigatedScore = GraphUtil.GetHighestAttackActorMitigatedScore(CurrentNodeID);
                                            if (actorMitigatedScore > tempactorMitigatedScore)
                                            {
                                               CalcLogEntry($"Setting highest previous Attack Actor mitigated score to :{actorMitigatedScore}");
                                                GraphUtil.SetNodeData(CurrentNodeID, "highestAttackActorMitigatedScore", actorMitigatedScore.ToString());
                                            }
                                        }

                                        if (parentNodeType == "objective")
                                        {
                                           CalcLogEntry($"Parent Node is an Objective. Getting highest previous Attack Actor mitigated score...");
                                            actorMitigatedScore = GraphUtil.GetHighestAttackActorMitigatedScore(CurrentNodeID);
                                           CalcLogEntry($"Treating Actor Mitigated Score as :{actorMitigatedScore}");
                                            //actorMitigatedScore = 0;
                                        }

                                        if (parentNodeType == "vulnerability")
                                        {
                                           CalcLogEntry($"Parent Node is an Vulnerability. Getting highest previous Likelihood score...");
                                            actorMitigatedScore = GraphUtil.GetVulnerabilityNodeLikelihoodScore(LastNodeID);
                                           CalcLogEntry($"Treating Actor Mitigated Score as :{actorMitigatedScore}");
                                            //actorMitigatedScore = 0;
                                        }

                                        if (parentNodeType == "asset")
                                        {
                                           CalcLogEntry($"Parent Node is an Asset. Getting highest previous Likelihood score...");
                                            actorMitigatedScore = GraphUtil.GetVulnerabilityNodeLikelihoodScore(LastNodeID);
                                           CalcLogEntry($"Treating Actor Mitigated Score as :{actorMitigatedScore}");

                                            //Setting Last Node ID to Last Processed lastvulnerabilityNode node
                                            LastNodeID = lastvulnerabilityNode;
                                           CalcLogEntry($"Setting LastNodeID to {lastvulnerabilityNode}");
                                        }


                                        //Get Parent Node Previous Values
                                       CalcLogEntry($"Getting Parent Node previous values from Node:{LastNodeID}  (1014)");

                                        var jsparentNodePreviouscontrolBaseScore = GraphUtil.GetPreviousNodeControlBaseScore(LastNodeID);
                                       CalcLogEntry($"Parent Node previous Base Value is:{jsparentNodePreviouscontrolBaseScore.ToString()}");

                                        var jspreviousNodeCalculatedValueResult = GraphUtil.GetPreviousNodeCalculatedValue(LastNodeID);
                                       CalcLogEntry($"Parent Node previous Calculated Value is:{jspreviousNodeCalculatedValueResult.ToString()}");

                                        var jspreviousNodeedgeStrengthValueResult = GraphUtil.GetPreviousNodeAssessedValue(LastNodeID);
                                       CalcLogEntry($"Parent Node previous Assessed Value is:{jspreviousNodeedgeStrengthValueResult.ToString()}");

                                        var jspreviousNodeObjectiveTarget = GraphUtil.GetNodeObjectiveTargetType(LastNodeID);
                                       CalcLogEntry($"Parent Node previous Objective Target is:{jspreviousNodeObjectiveTarget.ToString()}");

                                        NodecontrolBaseScoreResult = Convert.ToDouble(jsNodecontrolBaseScoreResult.ToString());
                                        NodeCalculatedValueResult = Convert.ToDouble(jsNodeCalculatedValueResult.ToString());
                                        NodeedgeStrengthValueResult = Convert.ToDouble(jsNodeedgeStrengthValueResult.ToString());
                                        parentNodeType = jsparentNodeType.ToString().ToLower();
                                    }
                                    catch
                                    {

                                       CalcLogEntry($"No prevous Node found! Setting values to 0...");
                                        NodecontrolBaseScoreResult = 0;
                                       CalcLogEntry($"Parent Node Base Value is: 0");
                                        NodeCalculatedValueResult = 0;
                                       CalcLogEntry($"Parent Node Calculated Value is: 0");
                                        NodeedgeStrengthValueResult = 0;
                                       CalcLogEntry($"Parent Node Assessed Value is: 0");

                                    }
                                }  // (LastNodeID != CurrentNodeID && LastNodeID != "")
                                else
                                {
                                    //Get Parent Node Current Values
                                   CalcLogEntry($"No prevous Node found! Setting values to 0...");
                                    NodecontrolBaseScoreResult = 0;
                                   CalcLogEntry($"Parent Node Base Value is: 0");
                                    NodeCalculatedValueResult = 0;
                                   CalcLogEntry($"Parent Node Calculated Value is: 0");
                                    NodeedgeStrengthValueResult = 0;
                                   CalcLogEntry($"Parent Node Assessed Value is: 0");

                                }

                                double edgeStrengthValue = 0;
                                List<string> edgeGUIDs = new List<string>();
                               
                                if (parentNodeType != "asset")
                                {
                                    //Get Edge Strenth Value
                                   CalcLogEntry($"Previous Edge is: {PreviousEdgeID}");
                                    edgeStrengthValue = GraphUtil.GetEdgeStrengthScore(PreviousEdgeID);
                                   CalcLogEntry($"Previous Edge Strenth Value is: {edgeStrengthValue.ToString()}"); // e.g. 0.25
                                }
                                else
                                {
                                    // calculate edgeStrengthValue where there are multiple edges between vulnrability node and attack node
                                    bool startNodeFound = false;
                                    
                                    foreach (string id in NodeonPath)
                                    {
                                        if (startNodeFound == true)
                                        {
                                            if (id == CurrentNodeID)
                                                break;
                                            if (GraphUtil.IsEdge(id))
                                                edgeGUIDs.Add(id);      
                                        }
                                        if (id == lastvulnerabilityNode)
                                            startNodeFound = true;
                                    }

                                    if (edgeGUIDs.Count > 0)
                                        edgeStrengthValue = GraphUtil.GetEdgeStrengthScore(edgeGUIDs[0]);
                                    for (int j = 1; j < edgeGUIDs.Count; j++)
                                        edgeStrengthValue = edgeStrengthValue * GraphUtil.GetEdgeStrengthScore(edgeGUIDs[0]);

                                    if (edgeStrengthValue < 0)
                                    {
                                       CalcLogEntry($"edgeStrengthValue < 0, setting to 0");
                                        edgeStrengthValue = 0;
                                    }
                                }

                              
                                Double NodeImpactedCalculatedValue = NodeCalculatedValueResult * Convert.ToDouble(edgeStrengthValue.ToString());
                               CalcLogEntry($"NodeImpactedCalculatedValue = NodeCalculatedValueResult * edgeStrengthValue");
                               CalcLogEntry($"NodeImpactedCalculatedValue = {NodeImpactedCalculatedValue.ToString()} * {edgeStrengthValue.ToString()}");
                               CalcLogEntry($"NodeImpactedCalculatedValue = {NodeImpactedCalculatedValue.ToString()}");

                                //Attack Mitigated Score Calculation
                                //(attack Score  x (100 – Node Impacted Calculated Value)) / 100
                               CalcLogEntry($"Calculating Attack Mitigated Score...");
                                Double attackMitigatedScore = attackScore * (100 - NodeImpactedCalculatedValue) / 100;
                                if (attackMitigatedScore < 0)
                                {
                                   CalcLogEntry($"attackMitigatedScore < 0, setting to 0");
                                    attackMitigatedScore = 0;
                                }
                               CalcLogEntry($"attackMitigatedScore = attackScore * (100 - NodeImpactedCalculatedValue) / 100");
                               CalcLogEntry($"attackMitigatedScore = {attackScore.ToString()} * (100 - {NodeImpactedCalculatedValue.ToString()}) / 100");
                               CalcLogEntry($"attackMitigatedScore = {attackMitigatedScore.ToString()}");
                                if (attackMitigatedScore < 0)
                                {
                                   CalcLogEntry($"attackMitigatedScore is less than 0. Setting attackMitigatedScore to 0...");
                                    attackMitigatedScore = 0;
                                }

                                //Set the Attack Mitigated Score 
                                //Need to check if a previous lower mitigated score exits
                                CalcLogEntry($"Checking for existing lower Attack Mitigated Score...");
                                double existingAttackMitigatedScore = Math.Round(GraphUtil.GetAttackNodeMitigatedScore(CurrentNodeID));
                                CalcLogEntry($"Existing Actor Mitigated Score is: {existingAttackMitigatedScore}");

                                GraphUtil.SetNodeData(CurrentNodeID, "attackMitigatedScore", attackMitigatedScore.ToString());
                                GraphUtil.AddToNodeScores(CurrentNodeID + ":attackMitigatedScore", attackMitigatedScore);


                                //Calculated the Edge Impacted Threat Score
                                CalcLogEntry($"Calculating Edge Impacted actorMitigatedScore Score...");
                                CalcLogEntry($"EdgeImpactedActorMitigatedScore = actorMitigatedScore * edgeStrengthValue");
                                CalcLogEntry($"EdgeImpactedActorMitigatedScore = {actorMitigatedScore.ToString()} * {edgeStrengthValue.ToString()}");
                                Double EdgeImpactedActorMitigatedScore = actorMitigatedScore * edgeStrengthValue;
                                
                                if (EdgeImpactedActorMitigatedScore < 0)
                                {
                                   CalcLogEntry($"EdgeImpactedActorMitigatedScore < 0, setting to 0");
                                    EdgeImpactedActorMitigatedScore = 0;
                                }
                               CalcLogEntry($"EdgeImpactedActorMitigatedScore = {EdgeImpactedActorMitigatedScore.ToString()}");
                                actorMitigatedScore = EdgeImpactedActorMitigatedScore;


                                //Threat Score Calculation
                                //(Attack Mitigated Score * Actor Mitigated Score) / 100
                               CalcLogEntry($"Calculating Threat Score...");
                                Double threatScore = (attackMitigatedScore * actorMitigatedScore) / 100;
                                if (threatScore < 0)
                                {
                                   CalcLogEntry($"threatScore < 0, setting to 0");
                                    threatScore = 0;
                                }
                               CalcLogEntry($"ThreatScore = (attackMitigatedScore * actorMitigatedScore) / 100");
                               CalcLogEntry($"ThreatScore = {attackMitigatedScore.ToString()} * {actorMitigatedScore.ToString()}) / 100");
                               CalcLogEntry($"ThreatScore = {threatScore.ToString()}");

                                //Set the threat Score 
                                //Need to check if a previous higher mitigated score exits
                               CalcLogEntry($"Checking for existing lower ThreatScore Score...");
                                double existingThreatScore = GraphUtil.GetAttackNodeThreatScore(CurrentNodeID);
                               CalcLogEntry($"Existing Threat Score is: {existingThreatScore}");

                                
                                //Get a list of incomming Nodes
                                List<string> NodeGUIDs = new List<string>();
                               CalcLogEntry($"Getting List if incomming Nodes to this Attack Node...");
                                NodeGUIDs = GraphUtil.GetNodeIngoerNodes(CurrentNodeID);

                                // process each incommingNodes
                                double TempAttackActorScoresValue = threatScore;
                                foreach (string GUID in NodeGUIDs)
                                {
                                    if (GraphUtil.GetNodeType(GUID).ToLower() == "actor")    // If Incomming Node is an Actor Node
                                    {
                                        // 1) Get Actor Mitigated Score for each Node
                                        // 2) Get Edge Assessed Value for each Node
                                        // 3) Calculate Edge Impacted Actor Mitigated Score
                                        // 4) Calculate Threat Score
                                        // 5) Check if Threat Score is lower than previous Threat Score
                                        // 6) If higer, update Threat Score
                                        string GUIDKey = CurrentNodeID + ":" + GUID;

                                       actorMitigatedScore = GraphUtil.GetActorNodeMitigatedScore(GUID);
                                       CalcLogEntry($"Source Node Actor Mitigated Score is :{actorMitigatedScore}");

                                        string edgeID = GraphUtil.GetEdgeBetweenNodes(GUID, CurrentNodeID);
                                       CalcLogEntry($"Previous Edge is: {edgeID}");
                                        edgeStrengthValue = GraphUtil.GetEdgeStrengthScore(edgeID);
                                       CalcLogEntry($"Previous Edge Strenth Value is: {edgeStrengthValue.ToString()}"); // e.g. 0.25

                                        //Calculated the Edge Impacted Threat Score
                                       CalcLogEntry($"Calculating Edge Impacted actorMitigatedScore Score...");
                                       CalcLogEntry($"EdgeImpactedActorMitigatedScore = actorMitigatedScore * edgeStrengthValue");
                                       CalcLogEntry($"EdgeImpactedActorMitigatedScore = {actorMitigatedScore.ToString()} * {edgeStrengthValue.ToString()}");
                                        EdgeImpactedActorMitigatedScore = actorMitigatedScore * edgeStrengthValue;
                                       CalcLogEntry($"EdgeImpactedActorMitigatedScore = {EdgeImpactedActorMitigatedScore.ToString()}");
                                        if (EdgeImpactedActorMitigatedScore < 0)
                                        {
                                           CalcLogEntry($"EdgeImpactedActorMitigatedScore < 0, setting to 0");
                                            EdgeImpactedActorMitigatedScore = 0;
                                        }
                                        actorMitigatedScore = EdgeImpactedActorMitigatedScore;

                                        //Threat Score Calculation
                                        //(Attack Mitigated Score * Actor Mitigated Score) / 100
                                       CalcLogEntry($"Calculating Threat Score...");
                                        threatScore = (attackMitigatedScore * actorMitigatedScore) / 100;
                                        if (threatScore < 0)
                                        {
                                           CalcLogEntry($"threatScore < 0, setting to 0");
                                            threatScore = 0;
                                        }
                                       CalcLogEntry($"ThreatScore = (attackMitigatedScore * actorMitigatedScore) / 100");
                                       CalcLogEntry($"ThreatScore = {attackMitigatedScore.ToString()} * {actorMitigatedScore.ToString()}) / 100");
                                       CalcLogEntry($"ThreatScore = {threatScore.ToString()}");

                                        if (s_attackActorScores.ContainsKey(GUIDKey))
                                        {
                                           CalcLogEntry($"Node GUID {GUIDKey} has a value of: {s_attackActorScores[GUIDKey]} ");
                                            //if (threatScore <= AttackActorScores[GUIDKey])  // 23/01/23
                                            //{
                                            //    if (useCalcLog) calculationLog.Add((DateTime.Now, $"Node AttackActorScores is greater that current AttackActorScores...Setting new value...");
                                                s_attackActorScores[GUIDKey] = threatScore;
                                            //    if (useCalcLog) calculationLog.Add((DateTime.Now, $"AttackActorScoresValue = {AttackActorScores[GUIDKey]} ");
                                            //}
                                        }
                                        else
                                        {
                                            s_attackActorScores.Add(GUIDKey, threatScore);
                                        }
                                    }
                                    if (GraphUtil.GetNodeType(GUID).ToLower() == "asset")  //If Incomming Node is an Asset Node
                                    {
                                        // Get a list of incomming nodes to the Asset node (i.e. Parent node of the parent node to the current node)

                                        List<string> NodeGUIDs2 = new List<string>();
                                       CalcLogEntry($"Getting List if incomming Nodes to the Asset Node...");
                                        NodeGUIDs2 = GraphUtil.GetNodeIngoerNodes(GUID);

                                        foreach (string GUID2 in NodeGUIDs2)
                                        {
                                            if (GraphUtil.GetNodeType(GUID2).ToLower() == "vulnerability") //If Parent Node is an Vulnerability Node
                                            {
                                                // process vulnerability type node
                                               CalcLogEntry($"Getting highest Likelihood score in place of actor mitigated score...");
                                                actorMitigatedScore = GraphUtil.GetVulnerabilityNodeLikelihoodScore(GUID2);
                                               CalcLogEntry($"Treating Actor Mitigated Score as :{actorMitigatedScore}");

                                                string edgeID = GraphUtil.GetEdgeBetweenNodes(GUID2, GUID);
                                               CalcLogEntry($"Edge between vulnerability and asset node is: {edgeID}");
                                                edgeStrengthValue = GraphUtil.GetEdgeStrengthScore(edgeID);
                                               CalcLogEntry($"Edge Strenth Value is: {edgeStrengthValue.ToString()}"); // e.g. 0.25

                                                //Calculated the Edge Impacted Threat Score
                                               CalcLogEntry($"Calculating Edge Impacted actorMitigatedScore Score...");
                                               CalcLogEntry($"EdgeImpactedActorMitigatedScore = actorMitigatedScore * edgeStrengthValue");
                                               CalcLogEntry($"EdgeImpactedActorMitigatedScore = {actorMitigatedScore.ToString()} * {edgeStrengthValue.ToString()}");
                                                EdgeImpactedActorMitigatedScore = actorMitigatedScore * edgeStrengthValue;
                                               CalcLogEntry($"EdgeImpactedActorMitigatedScore = {EdgeImpactedActorMitigatedScore.ToString()}");
                                                actorMitigatedScore = EdgeImpactedActorMitigatedScore;

                                                //Threat Score Calculation
                                                //(Attack Mitigated Score * Actor Mitigated Score) / 100
                                               CalcLogEntry($"Calculating Threat Score...");
                                                threatScore = (attackMitigatedScore * actorMitigatedScore) / 100;
                                               CalcLogEntry($"ThreatScore = (attackMitigatedScore * actorMitigatedScore) / 100");
                                               CalcLogEntry($"ThreatScore = {attackMitigatedScore.ToString()} * {actorMitigatedScore.ToString()}) / 100");
                                               CalcLogEntry($"ThreatScore = {threatScore.ToString()}");

                                                string GUIDKey = CurrentNodeID + ":" + GUID;

                                                if (s_attackActorScores.ContainsKey(GUIDKey))   // Didn't Add
                                                {
                                                   CalcLogEntry($"Node GUID {GUIDKey} has a value of: {s_attackActorScores[GUIDKey]} ");
                                                    //if (threatScore >= AttackActorScores[GUIDKey])  // 16/01/22
                                                    //{
                                                    //    if (useCalcLog) calculationLog.Add((DateTime.Now, $"Node AttackActorScores is greater that current AttackActorScores...Setting new value...");
                                                        s_attackActorScores[GUIDKey] = threatScore;  //24/01/23
                                                    //    if (useCalcLog) calculationLog.Add((DateTime.Now, $"AttackActorScoresValue = {AttackActorScores[GUIDKey]} ");
                                                    //}
                                                }
                                                else
                                                {
                                                    s_attackActorScores.Add(GUIDKey, threatScore);
                                                }
                                            }
                                        }
                                       
                                    }

                                }

                                CalcLogEntry($"Set Threat Score to: {threatScore.ToString()}");         
                                GraphUtil.SetNodeData(CurrentNodeID, "threatScore", threatScore.ToString());
                                GraphUtil.AddToNodeScores(CurrentNodeID + ":threatScore", threatScore);
                            }
                        }           // Risk calculate attack

                        // Asset Node *******************************************************************************************************************
                        if (nodeType.ToLower() == "asset" || nodeType.ToLower() == "asset-group") // Risk processing Node is an Asset
                        {
                           CalcLogEntry($"+++ {nodeType.ToString()} Node +++");
                           CalcLogEntry($"Checking if Node is enabled...");
                            var nodeEnabled = GraphUtil.IsNodeEnabled(CurrentNodeID);

                            if (nodeEnabled.ToString().ToLower() == "true")
                            {
                                CalcLogEntry($"Node is enabled...");
                                CalculateAsset(CurrentNodeID, LastNodeID, PreviousEdgeID);

                               
                            }

                        }            // Risk calculate asset

                        // Vulnerability Node ************************************************************************************************************
                        if (nodeType.ToString().ToLower() == "vulnerability")       // Risk processing Node is an Vulnerability 
                        {
                            lastvulnerabilityNode = CurrentNodeID;
                            CalcLogEntry($"+++ {nodeType.ToString()} Node +++");
                            CalcLogEntry($"Checking if Node is enabled...");
                            var nodeEnabled = GraphUtil.IsNodeEnabled(CurrentNodeID);

                            if (nodeEnabled.ToString().ToLower() == "true")
                            {
                                CalcLogEntry($"Node is enabled...");
                                //Get attack Score
                                CalcLogEntry($"Getting Vulnerability Score for Node...");
                                var VulnerabilityScore = GraphUtil.GetVulnerabilityNodeScore(CurrentNodeID);

                                CalcLogEntry($"Vulnerability Score is: {VulnerabilityScore}");
                                GraphUtil.SetNodeData(CurrentNodeID, "VulnerabilityScore", VulnerabilityScore.ToString());
                                GraphUtil.AddToNodeScores(CurrentNodeID + ":VulnerabilityScore", VulnerabilityScore);

                                double NodecontrolBaseScoreResult = 0;
                                double NodeCalculatedValueResult = 0;
                                double NodeedgeStrengthValueResult = 0;
                                double threatScore = 0;
                                double attackScore = 0;
                                double attackMitigatedScore = 0;
                                double parentRiskScore = 0;
                                string parentNodeType = "";


                                if (LastNodeID != CurrentNodeID && LastNodeID != "")
                                {
                                    try
                                    {
                                        //Get Parent Node Current Values
                                       CalcLogEntry($"Getting Parent Node values from Node:{LastNodeID}  (015)");
                                        var jsNodecontrolBaseScoreResult = GraphUtil.GetNodeBaseScore(LastNodeID);

                                       CalcLogEntry($"Parent Node Base Value is:{jsNodecontrolBaseScoreResult.ToString()}");
                                        var jsNodeCalculatedValueResult = GraphUtil.GetNodeCalculatedValue(LastNodeID);

                                       CalcLogEntry($"Parent Node Calculated Value is:{jsNodeCalculatedValueResult.ToString()}");
                                        var jsNodeedgeStrengthValueResult = GraphUtil.GetEdgeStrengthScore(LastNodeID);

                                       CalcLogEntry($"Parent Node Assessed Value is:{jsNodeedgeStrengthValueResult.ToString()}");
                                        var jsparentNodeType = GraphUtil.GetNodeType(LastNodeID);

                                       CalcLogEntry($"Parent Node Type is:{jsparentNodeType} (0004)");

                                        if (jsparentNodeType.ToString().ToLower() == "attack")
                                        {
                                            threatScore = GraphUtil.GetAttackNodeThreatScore(LastNodeID);
                                           CalcLogEntry($"Parent Node Threat Score is :{threatScore}");

                                            attackScore = GraphUtil.GetAttackNodeScore(LastNodeID);
                                           CalcLogEntry($"Parent Node Attack Score is :{attackScore}");

                                            attackMitigatedScore = Math.Round(GraphUtil.GetAttackNodeMitigatedScore(LastNodeID));
                                           CalcLogEntry($"Parent Node Attack Mitigated Score is :{attackMitigatedScore}");
                                        }


                                        NodecontrolBaseScoreResult = Convert.ToDouble(jsNodecontrolBaseScoreResult.ToString());
                                        NodeCalculatedValueResult = Convert.ToDouble(jsNodeCalculatedValueResult.ToString());
                                        NodeedgeStrengthValueResult = Convert.ToDouble(jsNodeedgeStrengthValueResult.ToString());
                                        parentNodeType = jsparentNodeType.ToString().ToLower();
                                    }
                                    catch
                                    {

                                       CalcLogEntry($"No prevous Node found! Setting values to 0...");
                                        NodecontrolBaseScoreResult = 0;
                                       CalcLogEntry($"Parent Node Base Value is: 0");
                                        NodeCalculatedValueResult = 0;
                                       CalcLogEntry($"Parent Node Calculated Value is: 0");
                                        NodeedgeStrengthValueResult = 0;
                                       CalcLogEntry($"Parent Node Assessed Value is: 0");
                                    }
                                }
                                else
                                {
                                    //Get Parent Node Current Values
                                   CalcLogEntry($"No prevous Node found! Setting values to 0...");
                                    NodecontrolBaseScoreResult = 0;
                                   CalcLogEntry($"Parent Node Base Value is: 0");
                                    NodeCalculatedValueResult = 0;
                                   CalcLogEntry($"Parent Node Calculated Value is: 0");
                                    NodeedgeStrengthValueResult = 0;
                                   CalcLogEntry($"Parent Node Assessed Value is: 0");

                                }


                                //Get Edge Strenth Value
                                CalcLogEntry($"Previous Edge is: {PreviousEdgeID}");
                                var edgeStrengthValue = GraphUtil.GetEdgeStrengthScore(PreviousEdgeID);
                                CalcLogEntry($"Previous Edge Strenth Value is: {edgeStrengthValue.ToString()}"); // e.g. 0.25

                                Double NodeImpactedParentRiskScore = parentRiskScore * Convert.ToDouble(edgeStrengthValue.ToString());
                                CalcLogEntry($"NodeImpactedParentRiskScore = parentRiskScore * edgeStrengthValue");
                                CalcLogEntry($"NodeImpactedParentRiskScore = {parentRiskScore.ToString()} * {edgeStrengthValue.ToString()}");
                                CalcLogEntry($"NodeImpactedParentRiskScore = {NodeImpactedParentRiskScore.ToString()}");
                                if (NodeImpactedParentRiskScore < 0)
                                {
                                   CalcLogEntry($"NodeImpactedParentRiskScore < 0, setting to 0");
                                    NodeImpactedParentRiskScore = 0;
                                }

                                Double NodeImpactedCalculatedValue = NodeCalculatedValueResult * Convert.ToDouble(edgeStrengthValue.ToString());
                                CalcLogEntry($"NodeImpactedCalculatedValue = NodeCalculatedValueResult * edgeStrengthValue");
                                CalcLogEntry($"NodeImpactedCalculatedValue = {NodeImpactedCalculatedValue.ToString()} * {edgeStrengthValue.ToString()}");
                                CalcLogEntry($"NodeImpactedCalculatedValue = {NodeImpactedCalculatedValue.ToString()}");
                                if (NodeImpactedCalculatedValue < 0)
                                {
                                    CalcLogEntry($"NodeImpactedCalculatedValue < 0, setting to 0");
                                    NodeImpactedCalculatedValue = 0;
                                }

                                //vulnerability Mitigated Score Calculation
                                CalcLogEntry($"Calculating Vulnerability Mitigated Score...");
                                Double vulnerabilityMitigatedScore = VulnerabilityScore * (100 - NodeImpactedCalculatedValue) / 100;
                                CalcLogEntry($"vulnerabilityMitigatedScore = attackScore * (100 - NodeImpactedCalculatedValue) / 100");
                                CalcLogEntry($"vulnerabilityMitigatedScore = {VulnerabilityScore.ToString()} * (100 - {NodeImpactedCalculatedValue.ToString()}) / 100");
                                CalcLogEntry($"vulnerabilityMitigatedScore = {vulnerabilityMitigatedScore.ToString()}");
                                if (vulnerabilityMitigatedScore < 0)
                                {
                                    CalcLogEntry($"vulnerabilityMitigatedScore < 0, setting to 0");
                                    vulnerabilityMitigatedScore = 0;
                                }

                                
                                GraphUtil.SetNodeData(CurrentNodeID, "vulnerabilityMitigatedScore", vulnerabilityMitigatedScore.ToString());
                                GraphUtil.AddToNodeScores(CurrentNodeID + ":vulnerabilityMitigatedScore", vulnerabilityMitigatedScore);

                                // Calculated the Edge Impacted Threat Score
                                CalcLogEntry($"Calculating Edge Impacted Threat Score...");
                                CalcLogEntry($"EdgeImpactedThreatScore = threatMitigatedScore * edgeStrengthValue");
                                CalcLogEntry($"EdgeImpactedThreatScore = {threatScore.ToString()} * {edgeStrengthValue.ToString()}");
                                Double EdgeImpactedThreatScore = threatScore * edgeStrengthValue;
                                CalcLogEntry($"EdgeImpactedThreatScore = {EdgeImpactedThreatScore.ToString()}");
                                if (EdgeImpactedThreatScore < 0)
                                {
                                   CalcLogEntry($"EdgeImpactedThreatScore < 0, setting to 0");
                                    EdgeImpactedThreatScore = 0;
                                }

                                //Likelihood Score Calculation
                                //(EdgeImpactedThreatScore * Vulnerability Mitigated Score) / 100
                                CalcLogEntry($"Calculating Likelihood Score...");
                                Double likelihoodScore = (EdgeImpactedThreatScore * vulnerabilityMitigatedScore) / 100;
                                if (likelihoodScore < 0)
                                {
                                    CalcLogEntry($"likelihoodScore < 0, setting to 0");
                                    likelihoodScore = 0;
                                }
                                CalcLogEntry($"LikelihoodScore = (EdgeImpactedThreatScore * vulnerabilityMitigatedScore) / 100");
                                CalcLogEntry($"LikelihoodScore = {EdgeImpactedThreatScore.ToString()} * {vulnerabilityMitigatedScore.ToString()}) / 100");
                                CalcLogEntry($"LikelihoodScore = {likelihoodScore.ToString()}");

                             

                                CalcLogEntry($"Calculating Likelihood Score...");
                                //Get a list of incomming Nodes
                                List<string> NodeGUIDs = new List<string>();
                                CalcLogEntry($"Getting List if incomming Nodes to this Vulnerability Node...");
                                NodeGUIDs = GraphUtil.GetNodeIngoerNodes(CurrentNodeID);

                                // process each incommingNodes
                                double TempAttackVulnerabilityScoresValue = likelihoodScore;
                                foreach (string GUID in NodeGUIDs)  // Process each incomming node
                                {
                                    string tempNodeType = GraphUtil.GetNodeType(GUID).ToLower();
                                    if (tempNodeType == "attack" || tempNodeType == "vulnerability")   // Only process if the incomming node is an attack node
                                    {

                                        if (tempNodeType == "vulnerability")
                                        {
                                            // This section supports vulnrability chaining 
                                            //We take the output Licklihood score of the previous node, edge impact it an use it as the Threat value for the input

                                            // Get in licklihoodscore and use it as the threat score
                                            threatScore = GraphUtil.GetVulnerabilityNodeLikelihoodScore(GUID);
                                        }
                                        else
                                        {
                                            threatScore = GraphUtil.GetAttackNodeThreatScore(GUID);
                                        }

                                        string GUIDKey = CurrentNodeID + ":" + GUID;

                                        //if (threatScore != -1 && incommingNodeType == "attack")  // Threat score hasn't been proviously calculated
                                        //{
                                            CalcLogEntry($"Source Node Threat Score is :{threatScore}");

                                            string edgeID = GraphUtil.GetEdgeBetweenNodes(GUID, CurrentNodeID);
                                            CalcLogEntry($"Previous Edge is: {edgeID}");
                                            edgeStrengthValue = GraphUtil.GetEdgeStrengthScore(edgeID);
                                            CalcLogEntry($"Previous Edge Strenth Value is: {edgeStrengthValue.ToString()}"); // e.g. 0.25

                                            // Calculated the Edge Impacted Threat Score
                                            CalcLogEntry($"Calculating Edge Impacted Threat Score...");
                                            CalcLogEntry($"EdgeImpactedThreatScore = threatScore * edgeStrengthValue");
                                            CalcLogEntry($"EdgeImpactedThreatScore = {threatScore.ToString()} * {edgeStrengthValue.ToString()}");

                                            EdgeImpactedThreatScore = threatScore * edgeStrengthValue;
                                            if (EdgeImpactedThreatScore < 0)
                                            {
                                                CalcLogEntry($"EdgeImpactedThreatScore < 0, setting to 0");
                                                EdgeImpactedThreatScore = 0;
                                            }
                                            CalcLogEntry($"EdgeImpactedThreatScore = {EdgeImpactedThreatScore.ToString()}");

                                            //Likelihood Score Calculation
                                            //(EdgeImpactedThreatScore * Vulnerability Mitigated Score) / 100
                                            CalcLogEntry($"Calculating Likelihood Score...");
                                            likelihoodScore = (EdgeImpactedThreatScore * vulnerabilityMitigatedScore) / 100;
                                            if (likelihoodScore < 0)
                                            {
                                                CalcLogEntry($"likelihoodScore < 0, setting to 0");
                                                likelihoodScore = 0;
                                            }
                                            CalcLogEntry($"LikelihoodScore = (EdgeImpactedThreatScore * vulnerabilityMitigatedScore) / 100");
                                            CalcLogEntry($"LikelihoodScore = {EdgeImpactedThreatScore.ToString()} * {vulnerabilityMitigatedScore.ToString()}) / 100");
                                            CalcLogEntry($"LikelihoodScore = {likelihoodScore.ToString()}");



                                            string[] LikelihoodNodeonPath = NodeonPath;
                                        //}
                                        //else
                                        //{
                                        //    CalcLogEntry($"Source Node Threat Score is not calculated");
                                        //}
                                    }

                                    GraphUtil.SetNodeData(CurrentNodeID, "likelihoodScore", likelihoodScore.ToString());
                                    GraphUtil.AddToNodeScores(CurrentNodeID + ":likelihoodScore", likelihoodScore);
                                }

                            }
                            

                        } // Risk calculate Vulnrability


                        PreviousNodeID = CurrentNodeID;
                        LastNodeID = CurrentNodeID;
                    }
                    else  // if (nodeType != null && nodeType != "notfound")
                    {
                       CalcLogEntry($"Item has no Node Type...");
                        //check if element is an Edge
                       CalcLogEntry($"Checking if item is an Edge...");
                        var IsEdge = GraphUtil.IsEdge(CurrentNodeID);
                        if (IsEdge.ToString().ToLower() == "true") //Yes, element is an Edge
                        {
                           CalcLogEntry($"Item is an Edge...");
                           CalcLogEntry($"Checking if Edge is enabled...");
                            var edgeEnabled = GraphUtil.IsEdgeEnabled(CurrentNodeID);
                            if (edgeEnabled.ToString().ToLower() == "true")
                            {
                               CalcLogEntry($"Edge is enabled...");
                                PreviousEdgeID = CurrentNodeID;

                                //Get Edge Source
                               CalcLogEntry($"Getting Edge Source Node ...");
                                var EdgeSource = GraphUtil.GetEdgeSourceNode(CurrentNodeID);
                               
                               CalcLogEntry($"Edge Source Node is:{EdgeSource.ToString()}");

                                //Edge 1: EIV = 0 – ((Node A NBV – Node A NCV) *Edge EAV), so:
                                //Edge 1: EIV = 0 – (100 – 100)  *0 , so:
                                //Set Edge 1: EIV = 0

                                //Get Source NBV
                                var NodecontrolBaseScore = GraphUtil.GetNodeBaseScore(EdgeSource.ToString());
                               
                               CalcLogEntry($"Edge Source Node Base Value is:{NodecontrolBaseScore.ToString()}");

                                //Get Source NCV
                                var NodeCalculatedValue = GraphUtil.GetNodeCalculatedValue(EdgeSource.ToString());
                              
                               CalcLogEntry($"Edge Source Node Calculated Value is:{NodeCalculatedValue.ToString()}");

                                //Get Edge EAV
                                var edgeStrengthValue = GraphUtil.GetEdgeStrengthScore(CurrentNodeID);
                               
                               CalcLogEntry($"Edge Source Node Assessed Value is:{edgeStrengthValue.ToString()}");

                                //Now calculate Edge Impacted Value
                                double EIV = 0 - (Convert.ToDouble(NodecontrolBaseScore.ToString()) - Convert.ToDouble(NodeCalculatedValue.ToString())) * Convert.ToDouble(edgeStrengthValue.ToString());
                               CalcLogEntry($"Edge Impacted Value is:{EIV.ToString()}");

                                //Update Node on Graph
                               CalcLogEntry($"Updating Edge Impacted Value on Graph");
                                GraphUtil.SetNodeData(CurrentNodeID, "impactedValue", EIV.ToString());
                                GraphUtil.SetNodeData(CurrentNodeID, "_impactedValue", EIV.ToString());
                                GraphUtil.SetNodeData(CurrentNodeID, "_edgeStrengthValue", edgeStrengthValue.ToString());
                            }
                            else
                            {
                               CalcLogEntry($"Edge disabled, not processing.");
                            }
                        }
                        else
                        {
                           CalcLogEntry($"Not an Edge!");
                        }
                    }
                } //if (CurrentNodeID != "")
            }

            

           CalcLogEntry($"Finished RecalculatePath...");
        }

        private static void CalculateObjective(string CurrentNodeID, string PreviousNodeID, string PreviousEdgeID)
        {
            List<string> NodeGUIDs = new List<string>();
            double totalNodeObjectiveBaseScore = 0;
            double CurrentNodeObjectiveBaseScore = GraphUtil.GetNodeBaseScore(CurrentNodeID);
            double previousNodeControlBaseScore = GraphUtil.GetNodeBaseScore(PreviousNodeID);
            double edgeStrengthScore = GraphUtil.GetEdgeStrengthValue(PreviousEdgeID);
            string nodeBehaviour = GraphUtil.GetnodeBehaviour(CurrentNodeID);
            double controlScore = 0;
            double controlComplianceScore = 0;
            double edgeStrength = 0;
            string edgeGUID = string.Empty;

            CalcLogEntry($"Parent Node Base Value is:{previousNodeControlBaseScore.ToString()}");

            Double edgeImpactedNodeObjectiveBaseScore = previousNodeControlBaseScore * Convert.ToDouble(edgeStrengthScore.ToString());  //100 * 1 = 100
            CalcLogEntry($"edgeImpactedNodeControlBaseScore = NodecontrolBaseScoreResult * edgeStrengthValue");
            CalcLogEntry($"edgeImpactedNodeControlBaseScore = {previousNodeControlBaseScore.ToString()} * {edgeStrengthScore.ToString()}");
            CalcLogEntry($"edgeImpactedNodeControlBaseScore = {edgeImpactedNodeObjectiveBaseScore.ToString()}");
            if (edgeImpactedNodeObjectiveBaseScore < 0)
            {
                CalcLogEntry($"edgeImpactedNodeControlBaseScore < 0, setting to 0");
                edgeImpactedNodeObjectiveBaseScore = 0;
            }
            CalcLogEntry($"Processing Objective Node...");

            //********************************************************
            // This section is used for calculating Objective Targets

            var objectiveTargetType = GraphUtil.GetNodeObjectiveTargetType(CurrentNodeID);
            CalcLogEntry($"Node Objective Type is '{objectiveTargetType}'");
            if (objectiveTargetType != null && objectiveTargetType.ToString() == "Manually Set")
            {
                CalcLogEntry($"Base Value has been manually set...");
                totalNodeObjectiveBaseScore = Convert.ToDouble(CurrentNodeObjectiveBaseScore.ToString());
            }

            else if (objectiveTargetType == "Sum of Control Strengths") // Get the base strength of each control
            {
                NodeGUIDs = GraphUtil.GetParentControlAndObjectiveNodes(CurrentNodeID);
                //iterate through each and get the total control node score
                controlScore = 0;
                controlComplianceScore = 0;
                edgeStrength = 0;
                edgeGUID = string.Empty;
                foreach (string controlNode in NodeGUIDs)
                {
                    // Get the edge between the nodes
                    edgeGUID = GraphUtil.GetEdgeBetweenNodes(controlNode, CurrentNodeID);
                    //Get the edge strength
                    edgeStrength = GraphUtil.GetEdgeStrengthValue(edgeGUID);
                    // get the control calaculated value
                    controlScore = GraphUtil.GetNodeObjectiveTargetValue(controlNode);
                    // Calaulate the edge impacted value
                    controlScore = controlScore * edgeStrength;
                    // Add the result to controlComplianceScore
                    controlComplianceScore += controlScore;
                }

                // Update Objective Target Value
                GraphUtil.SetNodeData(CurrentNodeID, "objectiveTargetValue", controlComplianceScore.ToString());
                //GraphUtil.AddToNodeScores(CurrentNodeID + ":objectiveTargetValue", controlComplianceScore);
            }

            else if (objectiveTargetType == "Sum of Control Maximums (Edge Impacted)") // Uses 100 as the base strength of each control
            {
                CalcLogEntry($"Target score is (Control Nodes x 100) x Edge Strengths");
                CalcLogEntry($"totalGroupControlBaseScore = {totalNodeObjectiveBaseScore.ToString()}");

                List<string> controlNodes = new List<string>();
                controlNodes = GraphUtil.GetParentControlNodes(CurrentNodeID);
                totalNodeObjectiveBaseScore = 0;
                foreach (string controlNode in controlNodes)
                    //100 x Edge Strength for each control node
                    totalNodeObjectiveBaseScore += 100 * GraphUtil.GetEdgeStrengthScore(GraphUtil.GetEdgeBetweenNodes(controlNode, CurrentNodeID));

                // Now get any parent Objective Nodes
                double objectiveScore = 0;
                List<string> objectiveNodes = new List<string>();
                objectiveNodes = GraphUtil.GetParentObjectivelNodes(CurrentNodeID);
                foreach (string objectiveNode in objectiveNodes)
                    objectiveScore += GraphUtil.GetNodeObjectiveTargetValue(objectiveNode) * GraphUtil.GetEdgeStrengthScore(GraphUtil.GetEdgeBetweenNodes(objectiveNode, CurrentNodeID));

                // Add the two values together
                totalNodeObjectiveBaseScore += objectiveScore;

                GraphUtil.SetNodeData(CurrentNodeID, "objectiveTargetValue", totalNodeObjectiveBaseScore.ToString());
                //GraphUtil.AddToNodeScores(CurrentNodeID + ":objectiveTargetValue", totalNodeObjectiveBaseScore);
            }

            else if (objectiveTargetType == "Sum of Control Maximums") // Uses 100 as the base strength of each control, do not assesses edge strength
            {
                CalcLogEntry($"Target score is Control Nodes x 100");
                //totalGroupControlBaseScore = GraphUtil.GetParentControlNodes(CurrentNodeID).Count() * 100;
                CalcLogEntry($"totalGroupControlBaseScore = {totalNodeObjectiveBaseScore.ToString()}");

                totalNodeObjectiveBaseScore = 100 * GraphUtil.GetParentControlNodes(CurrentNodeID).Count();

                // Now get any parent Objective Nodes
                double objectiveScore = 0;
                List<string> objectiveNodes = new List<string>();
                objectiveNodes = GraphUtil.GetParentObjectivelNodes(CurrentNodeID);
               
                foreach (string objectiveNode in objectiveNodes)
                    objectiveScore += GraphUtil.GetNodeObjectiveTargetValue(objectiveNode);

                // Add the two values together
                totalNodeObjectiveBaseScore += objectiveScore;
                GraphUtil.SetNodeData(CurrentNodeID, "objectiveTargetValue", totalNodeObjectiveBaseScore.ToString());
                //GraphUtil.AddToNodeScores(CurrentNodeID + ":objectiveTargetValue", totalNodeObjectiveBaseScore);
            }

            //********************************************************
            // This section is used for calculating Objective compliance

            
            //get a list of incomming control and objective nodes
            NodeGUIDs = GraphUtil.GetParentControlNodes(CurrentNodeID);

            //iterate through each and get the total control node score
            
            foreach (string controlNode in NodeGUIDs)
            {
                // Get the edge between the nodes
                edgeGUID = GraphUtil.GetEdgeBetweenNodes(controlNode, CurrentNodeID);
                //Get the edge strength
                edgeStrength = GraphUtil.GetEdgeStrengthValue(edgeGUID);
                // get the control calaculated value
                controlScore = GraphUtil.GetNodeCalculatedValue(controlNode);
                // Calaulate the edge impacted value
                controlScore = controlScore * edgeStrength;
                // Add the result to controlComplianceScore
                controlComplianceScore += controlScore;
            }
           
            NodeGUIDs = GraphUtil.GetParentObjectivelNodes(CurrentNodeID);

            //iterate through each and get the total control node score

            foreach (string controlNode in NodeGUIDs)
            {
                // Get the edge between the nodes
                edgeGUID = GraphUtil.GetEdgeBetweenNodes(controlNode, CurrentNodeID);
                //Get the edge strength
                edgeStrength = GraphUtil.GetEdgeStrengthValue(edgeGUID);
                // get the control calaculated value
                controlScore = GraphUtil.GetNodeObjectiveAcheivedValue(controlNode);
                // Calaulate the edge impacted value
                controlScore = controlScore * edgeStrength;
                // Add the result to controlComplianceScore
                controlComplianceScore += controlScore;
            }

            // Set the controlComplianceScore to the Objective node
            GraphUtil.SetNodeData(CurrentNodeID, "objectiveAcheivedValue", controlComplianceScore.ToString());
          //  GraphUtil.AddToNodeScores(CurrentNodeID + ":objectiveAcheivedValue", controlComplianceScore);

        }


        private static void CalculateControl(string CurrentNodeID, string LastNodeID, string PreviousEdgeID)
        {
           CalcLogEntry($"Check if Node is Root Node...");
            // Check if this is a root node - if so set Calulated Value to Assessed Value
            var isRootNode = GraphUtil.IsRootNode(CurrentNodeID);

            double nodeAssessedScore = GraphUtil.GetControlNodeAssessedScore(CurrentNodeID);
           CalcLogEntry($"Retreived Node Assessed Value is: {nodeAssessedScore}");

            double nodeCalculatedValue = GraphUtil.GetNodeCalculatedValue(CurrentNodeID);
           CalcLogEntry($"Retreived Node Calculated Value is: {nodeCalculatedValue}");

            double nodecontrolBaseScore = GraphUtil.GetNodeBaseScore(CurrentNodeID);
           CalcLogEntry($"Retreived Node Base Value is: {nodecontrolBaseScore}");

           
            if (isRootNode.ToString().ToLower() == "true")
            {
               CalcLogEntry($"Node is a Root Node...");
                LastNodeID = CurrentNodeID;

                Double NodeCalculatedValue = (Convert.ToDouble(nodecontrolBaseScore.ToString()) * Convert.ToDouble(nodeAssessedScore.ToString())) / 100;

               CalcLogEntry($"Updating Node Calculated Value on Graph to: {NodeCalculatedValue}");
                if (NodeCalculatedValue > 100) NodeCalculatedValue = 100;
                GraphUtil.SetNodeData(CurrentNodeID, "calculatedValue", NodeCalculatedValue.ToString());
                GraphUtil.AddToNodeScores(CurrentNodeID + ":calculatedValue", NodeCalculatedValue);

            }
            else
            {
                // Child Node
               CalcLogEntry($"Node is a Child Node...");
               CalcLogEntry($"Calculating Child Node Values...");
                LastNodeID = CurrentNodeID;


                //Get Edge Impacted Value

               CalcLogEntry($"Previous Edge is: {PreviousEdgeID}");
                double edgeImpactedValueValue = GraphUtil.GetEdgeImpactedValue(PreviousEdgeID);
               CalcLogEntry($"Previous Edge Impacted Value is: {edgeImpactedValueValue.ToString()}");

                //Calculate Node calculated value
                double NodeCalculatedValue = nodeCalculatedValue + edgeImpactedValueValue;
               CalcLogEntry($"New Node Calculated Value is: {NodeCalculatedValue.ToString()}");

                if (NodeCalculatedValue.ToString() != nodeCalculatedValue.ToString())
                {
                   CalcLogEntry($"Node calucated Value has changed: {NodeCalculatedValue.ToString()} to  {nodeCalculatedValue.ToString()}");
                    //Set Previous Values to Current Values

                    GraphUtil.SetNodeData(CurrentNodeID, "previouscontrolBaseScore", nodecontrolBaseScore.ToString());
                   CalcLogEntry($"Previous Base Values is: {nodecontrolBaseScore.ToString()}");

                    GraphUtil.SetNodeData(CurrentNodeID, "previouscalculatedValue", nodeCalculatedValue.ToString());
                   CalcLogEntry($"Previous Calculated Values is: {nodeCalculatedValue.ToString()}");

                    GraphUtil.SetNodeData(CurrentNodeID, "previousControlAssessedValue", nodeAssessedScore.ToString());
                   CalcLogEntry($"Previous Assessed Values is: {nodeAssessedScore.ToString()}");

                    //Update Node on Graph
                   CalcLogEntry($"Updating Node Calculated Value on Graph");
                    if (NodeCalculatedValue > 100) NodeCalculatedValue = 100;
                    GraphUtil.SetNodeData(CurrentNodeID, "calculatedValue", NodeCalculatedValue.ToString());
                    GraphUtil.AddToNodeScores(CurrentNodeID + ":calculatedValue", NodeCalculatedValue);

                    //Values updated - Remove Node from already processed pairs
                    CalcLogEntry($"Node values have been updated, Removing from Pair list...");
                    var allOutgoers = GraphUtil.GetChildNodes(CurrentNodeID);
                    foreach (var ID in allOutgoers)
                    {
                        string tempNodePair = ID.ToString() + " > " + CurrentNodeID;
                       CalcLogEntry($"Looking for Node Pair: {tempNodePair}");
                        if (NodePairs.IndexOf(tempNodePair) == -1)
                        {
                           CalcLogEntry($"Node Pair is not is the Pair list: {tempNodePair}");
                        }

                        while (NodePairs.IndexOf(tempNodePair) != -1)
                        {
                            NodePairs.RemoveAt(NodePairs.IndexOf(tempNodePair));
                           CalcLogEntry($"Removing Node Pair: {tempNodePair}");
                        }
                    }
                }
            }
        }

        private static void CalculateAsset(string CurrentNodeID, string LastNodeID, string PreviousEdgeID)
        {
            
            CalcLogEntry($"Getting Asset Score for Node...");
            var assetScore = GraphUtil.GetAssetNodeScore(CurrentNodeID);
            CalcLogEntry($"Asset Score is: {assetScore}");
            //GraphUtil.SetNodeData(CurrentNodeID, "assetScore", assetScore.ToString());

            double NodecontrolBaseScoreResult = 0;
            double NodeCalculatedValueResult = 0;
            double NodeedgeStrengthValueResult = 0;

            double threatScore = 0;
            double parentAssetScore = 0;
            string parentNodeType = "";
            double likelihoodScore = 0;

            double parentAssetNodeConfidentialityMitigatedScore = 0;
            double parentAssetNodeIntegrityMitigatedScore = 0;
            double parentAssetNodeAvailabilityMitigatedScore = 0;
            double parentAssetNodeAccountabilityMitigatedScore = 0;
            double parentAssetNodeObjectiveTargetValue = 0;
            double parentAssetNodeObjectiveAcheivedValue = 0;

            double assetLikelihoodScore = 0;


            string GUIDKey = CurrentNodeID + ":" + LastNodeID;

            if (LastNodeID != CurrentNodeID && LastNodeID != "")
            {
                try
                {
                    //Get Parent Node Current Values
                   CalcLogEntry($"Getting Parent Node values from Node:{LastNodeID}  (1015)");
                    var jsNodecontrolBaseScoreResult = GraphUtil.GetNodeBaseScore(LastNodeID);

                   CalcLogEntry($"Parent Node Base Value is:{jsNodecontrolBaseScoreResult.ToString()}");
                    var jsNodeCalculatedValueResult = GraphUtil.GetNodeCalculatedValue(LastNodeID);

                   CalcLogEntry($"Parent Node Calculated Value is:{jsNodeCalculatedValueResult.ToString()}");
                    var jsNodeedgeStrengthValueResult = GraphUtil.GetEdgeStrengthScore(LastNodeID);

                   CalcLogEntry($"Parent Node Assessed Value is:{jsNodeedgeStrengthValueResult.ToString()}");
                    parentNodeType = GraphUtil.GetNodeType(LastNodeID).ToLower();

                   CalcLogEntry($"Parent Node Type is:{parentNodeType} (0004)");

                    if (parentNodeType == "asset" || parentNodeType == "asset-group")
                    {
                        parentAssetScore = GraphUtil.GetAssetNodeScore(LastNodeID);
                        CalcLogEntry($"Parent Node Asset Score is :{parentAssetScore}");

                        assetLikelihoodScore = GraphUtil.GetAssetNodeLikelihoodScore(LastNodeID);
                        CalcLogEntry($"Parent Node assetLikelihoodScore Score is :{assetLikelihoodScore}");
                        NodecontrolBaseScoreResult = 0;
                        NodeCalculatedValueResult = 0;
                        NodeedgeStrengthValueResult = 0;

                    }
                    else
                    {
                        NodecontrolBaseScoreResult = Convert.ToDouble(jsNodecontrolBaseScoreResult.ToString());
                        NodeCalculatedValueResult = Convert.ToDouble(jsNodeCalculatedValueResult.ToString());
                        NodeedgeStrengthValueResult = Convert.ToDouble(jsNodeedgeStrengthValueResult.ToString());
                    }

                    if (parentNodeType == "attack")
                    {
                        threatScore = GraphUtil.GetAttackNodeThreatScore(LastNodeID);
                       CalcLogEntry($"Parent Node Threat Score is :{threatScore}");
                    }

                    else if (parentNodeType == "vulnerability" || parentNodeType == "vulnerability - group")
                    {
                        likelihoodScore = GraphUtil.GetVulnerabilityNodeLikelihoodScore(LastNodeID);
                        CalcLogEntry($"Parent Node Risk Score is :{likelihoodScore}");
                    }
                    
                }
                catch
                {
                   CalcLogEntry($"No prevous Node found! Setting values to 0...");
                    NodecontrolBaseScoreResult = 0;
                   CalcLogEntry($"Parent Node Base Value is: 0");
                    NodeCalculatedValueResult = 0;
                   CalcLogEntry($"Parent Node Calculated Value is: 0");
                    NodeedgeStrengthValueResult = 0;
                   CalcLogEntry($"Parent Node Assessed Value is: 0");
                }
            }
            else
            {
                //Get Parent Node Current Values
               CalcLogEntry($"No prevous Node found! Setting values to 0...");
                NodecontrolBaseScoreResult = 0;
               CalcLogEntry($"Parent Node Base Value is: 0");
                NodeCalculatedValueResult = 0;
               CalcLogEntry($"Parent Node Calculated Value is: 0");
                NodeedgeStrengthValueResult = 0;
               CalcLogEntry($"Parent Node Assessed Value is: 0");
            }

            //Get Edge Strenth Value
            CalcLogEntry($"Previous Edge is: {PreviousEdgeID}");
            var edgeStrengthValue = GraphUtil.GetEdgeStrengthScore(PreviousEdgeID);
            CalcLogEntry($"Previous Edge Strenth Value is: {edgeStrengthValue.ToString()}"); // e.g. 0.25

            Double NodeImpactedCalculatedValue = NodeCalculatedValueResult * Convert.ToDouble(edgeStrengthValue.ToString());
            if (NodeImpactedCalculatedValue < 0)
            {
               CalcLogEntry($"NodeImpactedCalculatedValue < 0, setting to 0");
                NodeImpactedCalculatedValue = 0;
            }
            CalcLogEntry($"NodeImpactedCalculatedValue = NodeCalculatedValueResult * edgeStrengthValue");
            CalcLogEntry($"NodeImpactedCalculatedValue = {NodeImpactedCalculatedValue.ToString()} * {edgeStrengthValue.ToString()}");
            CalcLogEntry($"NodeImpactedCalculatedValue = {NodeImpactedCalculatedValue.ToString()}");

            //Asset Mitigated Score Calculation
            CalcLogEntry($"Calculating Asset Mitigated Score...");
            Double assetMitigatedScore = assetScore * (100 - NodeImpactedCalculatedValue) / 100;
            if (assetMitigatedScore < 0)
            {
               CalcLogEntry($"assetMitigatedScore < 0, setting to 0");
                assetMitigatedScore = 0;
            }
           CalcLogEntry($"assetMitigatedScore = assetScore * (100 - NodeImpactedCalculatedValue) / 100");
           CalcLogEntry($"assetMitigatedScore = {assetScore.ToString()} * (100 - {NodeImpactedCalculatedValue.ToString()}) / 100");
           CalcLogEntry($"assetMitigatedScore = {assetMitigatedScore.ToString()}");

            
           CalcLogEntry($"assetMitigatedScore = {assetMitigatedScore} ");
            //GraphUtil.SetNodeData(CurrentNodeID, "assetMitigatedScore", assetMitigatedScore.ToString());

            
            //20/4/23
           
            GraphUtil.SetNodeData(CurrentNodeID, "assetMitigatedScore", assetMitigatedScore.ToString());
            GraphUtil.AddToNodeScores(CurrentNodeID + ":assetMitigatedScore", assetMitigatedScore);



            // Calculated the Edge Impacted Likelihood Score
            CalcLogEntry($"Calculating Edge Impacted Likelihood Score...");
            CalcLogEntry($"EdgeImpactedLikelihoodScore = likelihoodScore * edgeStrengthValue");
            CalcLogEntry($"EdgeImpactedLikelihoodScore = {likelihoodScore.ToString()} * {edgeStrengthValue.ToString()}");
            Double edgeImpactedLikelihoodScore = likelihoodScore * edgeStrengthValue;
            if (edgeImpactedLikelihoodScore < 0)
            {
                CalcLogEntry($"edgeImpactedLikelihoodScore < 0, setting to 0");
                edgeImpactedLikelihoodScore = 0;
            }
            CalcLogEntry($"EdgeImpactedLikelihoodScore = {edgeImpactedLikelihoodScore.ToString()}");


            //Impact Score Calculation
            CalcLogEntry($"Calculating Impact Score...");
            Double impactScore = (edgeImpactedLikelihoodScore * assetMitigatedScore) / 100;
            if (impactScore < 0)
            {
               CalcLogEntry($"impactScore < 0, setting to 0");
                impactScore = 0;
            }
            CalcLogEntry($"impactScore = (EdgeImpactedLikelihoodScore * assetMitigatedScore) / 100");
            CalcLogEntry($"impactScore = ({edgeImpactedLikelihoodScore.ToString()} * {assetMitigatedScore.ToString()}) / 100");
            CalcLogEntry($"impactScore = {impactScore.ToString()}");
            GraphUtil.SetNodeData(CurrentNodeID, "impactScore", impactScore.ToString());
            GraphUtil.AddToNodeScores(CurrentNodeID + ":impactScore", impactScore);
            GraphUtil.AddToDistributionData(CurrentNodeID + ":impactScore", (int)impactScore);

            //Calculate Asset Confidetiality Score 
            CalcLogEntry($"Getting Asset Confidentiality Score for Node...");
            var assetConfidentialityProbabilityValue = GraphUtil.GetAssetConfidentialityProbabilityValue(CurrentNodeID);
            CalcLogEntry($"Asset Confidetiality Score is: {assetConfidentialityProbabilityValue}");


            Double assetConfidentialityLikelihoodScore = (assetConfidentialityProbabilityValue * edgeImpactedLikelihoodScore) / 100;
            CalcLogEntry($"assetConfidentialityLikelihoodScore = assetConfidentialityProbabilityValue * edgeImpactedLikelihoodScore / 100");
            CalcLogEntry($"assetConfidentialityLikelihoodScore = {assetConfidentialityProbabilityValue.ToString()} *  {edgeImpactedLikelihoodScore.ToString()}) / 100");
            CalcLogEntry($"assetConfidentialityLikelihoodScore = {assetConfidentialityLikelihoodScore.ToString()}");
            GraphUtil.SetNodeData(CurrentNodeID, "assetConfidentialityLikelihoodScore", assetConfidentialityLikelihoodScore.ToString());
            GraphUtil.AddToNodeScores(CurrentNodeID + ":assetConfidentialityLikelihoodScore", assetConfidentialityLikelihoodScore);

            Double assetConfidentialityMitigatedScore = (assetConfidentialityProbabilityValue * (100 - NodeImpactedCalculatedValue) / 100);
            CalcLogEntry($"assetConfidentialityMitigatedScore = assetConfidentialityProbabilityValue * (100 - NodeImpactedCalculatedValue) / 100");
            CalcLogEntry($"assetConfidentialityMitigatedScore = {assetConfidentialityProbabilityValue.ToString()} *  {NodeImpactedCalculatedValue.ToString()}) / 100");
            CalcLogEntry($"assetConfidentialityMitigatedScore = {assetConfidentialityMitigatedScore.ToString()}");
            GraphUtil.SetNodeData(CurrentNodeID, "assetConfidentialityMitigatedScore", assetConfidentialityMitigatedScore.ToString());
            GraphUtil.AddToNodeScores(CurrentNodeID + ":assetConfidentialityMitigatedScore", assetConfidentialityMitigatedScore);


            //Calculate Asset Integrity Score 
            CalcLogEntry($"Getting Asset Integrity Score for Node...");
            var assetIntegrityProbabilityValue = GraphUtil.GetAssetNodeIntegrityProbabilityValue(CurrentNodeID);
            CalcLogEntry($"Asset Integrity Score is: {assetIntegrityProbabilityValue}");

            Double assetIntegrityLikelihoodScore = (assetIntegrityProbabilityValue * edgeImpactedLikelihoodScore) / 100;
            CalcLogEntry($"assetIntegrityLikelihoodScore = assetIntegrityProbabilityValue * edgeImpactedLikelihoodScore / 100");
            CalcLogEntry($"assetIntegrityLikelihoodScore = {assetIntegrityProbabilityValue.ToString()} *  {edgeImpactedLikelihoodScore.ToString()}) / 100");
            CalcLogEntry($"assetIntegrityLikelihoodScore = {assetIntegrityLikelihoodScore.ToString()}");
            GraphUtil.SetNodeData(CurrentNodeID, "assetIntegrityLikelihoodScore", assetIntegrityLikelihoodScore.ToString());
            GraphUtil.AddToNodeScores(CurrentNodeID + ":assetIntegrityLikelihoodScore", assetIntegrityLikelihoodScore);

            Double assetIntegrityMitigatedScore = (assetIntegrityProbabilityValue * (100 - NodeImpactedCalculatedValue) / 100);
            CalcLogEntry($"assetIntegrityMitigatedScore = assetIntegrityProbabilityValue * (100 - NodeImpactedCalculatedValue) / 100");
            CalcLogEntry($"assetIntegrityMitigatedScore = {assetIntegrityProbabilityValue.ToString()} *  {NodeImpactedCalculatedValue.ToString()}) / 100");
            CalcLogEntry($"assetIntegrityMitigatedScore = {assetIntegrityMitigatedScore.ToString()}");
            GraphUtil.SetNodeData(CurrentNodeID, "assetIntegrityMitigatedScore", assetIntegrityMitigatedScore.ToString());
            GraphUtil.AddToNodeScores(CurrentNodeID + ":assetIntegrityMitigatedScore", assetIntegrityMitigatedScore);


            //Calculate Asset Availability Score 
            CalcLogEntry($"Getting Asset Availability Score for Node...");
            var assetAvailabilityProbabilityValue = GraphUtil.GetAssetNodeAvailabilityProbabilityValue(CurrentNodeID);
            CalcLogEntry($"Asset Availability Score is: {assetAvailabilityProbabilityValue}");

            CalcLogEntry($"Calculating Attack Availability Mitigated Score...");
            Double assetAvailabilityLikelihoodScore = (assetAvailabilityProbabilityValue * edgeImpactedLikelihoodScore) / 100;
            CalcLogEntry($"assetAvailabilityLikelihoodScore = assetAvailabilityProbabilityValue * edgeImpactedLikelihoodScore / 100");
            CalcLogEntry($"assetAvailabilityLikelihoodScore = {assetAvailabilityProbabilityValue.ToString()} *  {edgeImpactedLikelihoodScore.ToString()}) / 100");
            CalcLogEntry($"assetAvailabilityLikelihoodScore = {assetAvailabilityProbabilityValue.ToString()}");
            GraphUtil.SetNodeData(CurrentNodeID, "assetAvailabilityLikelihoodScore", assetAvailabilityLikelihoodScore.ToString());
            GraphUtil.AddToNodeScores(CurrentNodeID + ":assetAvailabilityLikelihoodScore", assetAvailabilityLikelihoodScore);

            Double assetAvailabilityMitigatedScore = (assetAvailabilityProbabilityValue * (100 - NodeImpactedCalculatedValue) / 100);
            CalcLogEntry($"assetAvailabilityMitigatedScore = assetIntegrityProbabilityValue * (100 - NodeImpactedCalculatedValue) / 100");
            CalcLogEntry($"assetAvailabilityMitigatedScore = {assetIntegrityProbabilityValue.ToString()} *  {NodeImpactedCalculatedValue.ToString()}) / 100");
            CalcLogEntry($"assetAvailabilityMitigatedScore = {assetAvailabilityMitigatedScore.ToString()}");
            GraphUtil.SetNodeData(CurrentNodeID, "assetAvailabilityMitigatedScore", assetAvailabilityMitigatedScore.ToString());
            GraphUtil.AddToNodeScores(CurrentNodeID + ":assetAvailabilityMitigatedScore", assetAvailabilityMitigatedScore);


            //Calculate Asset Accountability Score 
            CalcLogEntry($"Getting Asset Accountability Score for Node...");
            var assetAccountabilityProbabilityValue = GraphUtil.GetAssetNodeAccountabilityProbabilityValue(CurrentNodeID);
            CalcLogEntry($"Asset Accountability Score is: {assetAccountabilityProbabilityValue}");

            Double assetAccountabilityLikelihoodScore = (assetAccountabilityProbabilityValue * edgeImpactedLikelihoodScore) / 100;
            CalcLogEntry($"assetAccountabilityLikelihoodScore = assetAccountabilityProbabilityValue * edgeImpactedLikelihoodScore / 100");
            CalcLogEntry($"assetAccountabilityLikelihoodScore = {assetAccountabilityProbabilityValue.ToString()} *  {edgeImpactedLikelihoodScore.ToString()}) / 100");
            CalcLogEntry($"assetAccountabilityLikelihoodScore = {assetAccountabilityProbabilityValue.ToString()}");
            GraphUtil.SetNodeData(CurrentNodeID, "assetAccountabilityLikelihoodScore", assetAccountabilityLikelihoodScore.ToString());
            GraphUtil.AddToNodeScores(CurrentNodeID + ":assetAccountabilityLikelihoodScore", assetAccountabilityLikelihoodScore);

            Double assetAccountabilityMitigatedScore = (assetAccountabilityProbabilityValue * (100 - NodeImpactedCalculatedValue) / 100);
            CalcLogEntry($"assetAccountabilityMitigatedScore = assetAccountabilityProbabilityValue * (100 - NodeImpactedCalculatedValue) / 100");
            CalcLogEntry($"assetAccountabilityMitigatedScore = {assetAccountabilityProbabilityValue.ToString()} *  {NodeImpactedCalculatedValue.ToString()}) / 100");
            CalcLogEntry($"assetAccountabilityMitigatedScore = {assetAccountabilityMitigatedScore.ToString()}");
            GraphUtil.SetNodeData(CurrentNodeID, "assetAccountabilityMitigatedScore", assetAccountabilityMitigatedScore.ToString());
            GraphUtil.AddToNodeScores(CurrentNodeID + ":assetAccountabilityMitigatedScore", assetAccountabilityMitigatedScore);


            //Get a list of incomming Nodes
            List<string> incommingNodes = new List<string>();
            CalcLogEntry($"Getting List if incomming Nodes to this Asset Node...");
            incommingNodes = GraphUtil.GetNodeIngoerNodes(CurrentNodeID);        

            // process each incommingNodes
            double edgeImpactedParentAssetNodeConfidentialityMitigatedScore = 0;
            double edgeImpactedParentAssetNodeIntegrityMitigatedScore = 0;
            double edgeImpactedParentAssetNodeAvailabilityMitigatedScore = 0;
            double edgeImpactedParentAssetNodeAccountabilityMitigatedScore = 0;
            double edgeImpactedParentAssetNodeObjectiveTargetValue = 0;
            double edgeImpactedParentAssetNodeObjectiveAcheivedValue = 0;
            double edgeImpactedParentAssetNodeImpactScore = 0;

            foreach (string ParentGUID in incommingNodes)
            {
                string incommingNodeType = GraphUtil.GetNodeType(ParentGUID).ToLower();

                if (incommingNodeType == "vulnerability")
                {
                    CalcLogEntry($"Source Node is an Vulnerability Node");

                    likelihoodScore = GraphUtil.GetVulnerabilityNodeLikelihoodScore(ParentGUID);
                    if (likelihoodScore != -1) //Check that Likelihood score has been calculated, if not, skip
                    {
                        CalcLogEntry($"Source Node Likelihood Score is :{likelihoodScore}");

                        string edgeID = "";
                        CalcLogEntry($"Getting Edge between Node {ParentGUID} and {CurrentNodeID}");
                        edgeID = GraphUtil.GetEdgeBetweenNodes(ParentGUID, CurrentNodeID);
                        GUIDKey = CurrentNodeID + ":" + ParentGUID;

                        if (string.IsNullOrEmpty(edgeID) && !string.IsNullOrEmpty(ParentGUID))
                        {
                            CalcLogEntry($"No Edge found between Nodes, chack if group Parent Node has an Edge");
                            CalcLogEntry($"Getting Edge between Node {ParentGUID} and {ParentGUID}");
                            edgeID = GraphUtil.GetEdgeBetweenNodes(ParentGUID, ParentGUID);
                            //GUIDKey = ParentGUID + ":" + GUID; //Change GUIDKey to be parentGUID
                        }

                        CalcLogEntry($"Previous Edge is: {edgeID}");
                        edgeStrengthValue = GraphUtil.GetEdgeStrengthScore(edgeID);
                        if (edgeStrengthValue < 0)
                        {
                            CalcLogEntry($"edgeStrengthValue < 0, setting to 0");
                            edgeStrengthValue = 0;
                        }
                        CalcLogEntry($"Previous Edge Strenth Value is: {edgeStrengthValue.ToString()}"); // e.g. 0.25

                        // Calculated the Edge Impacted Likelihood Score
                        CalcLogEntry($"Calculating Edge Impacted Likelihood Score...");
                        CalcLogEntry($"EdgeImpactedLikelihoodScore = likelihoodScore * edgeStrengthValue");
                        CalcLogEntry($"EdgeImpactedLikelihoodScore = {likelihoodScore.ToString()} * {edgeStrengthValue.ToString()}");
                        edgeImpactedLikelihoodScore = likelihoodScore * edgeStrengthValue;
                        if (edgeImpactedLikelihoodScore < 0)
                        {
                            CalcLogEntry($"edgeImpactedLikelihoodScore < 0, setting to 0");
                            edgeImpactedLikelihoodScore = 0;
                        }
                        CalcLogEntry($"EdgeImpactedLikelihoodScore = {edgeImpactedLikelihoodScore.ToString()}");

                        //Impact Score Calculation
                        CalcLogEntry($"Calculating Impact Score...");
                        impactScore = (edgeImpactedLikelihoodScore * assetMitigatedScore) / 100;
                        if (impactScore < 0)
                        {
                            CalcLogEntry($"impactScore < 0, setting to 0");
                            impactScore = 0;
                        }
                        CalcLogEntry($"impactScore = (edgeImpactedLikelihoodScore * assetMitigatedScore) / 100");
                        CalcLogEntry($"impactScore = {edgeImpactedLikelihoodScore.ToString()} * {assetMitigatedScore.ToString()}) / 100");
                        CalcLogEntry($"impactScore = {impactScore.ToString()}");

                        if (s_assetImpactLikelihoodScores.ContainsKey(GUIDKey))
                        {
                            CalcLogEntry($"Node GUID {GUIDKey} has a value of: {s_assetImpactLikelihoodScores[GUIDKey]} ");
                            s_assetImpactLikelihoodScores[GUIDKey] = edgeImpactedLikelihoodScore;  // 17/01/23
                        }
                        else
                        {
                            CalcLogEntry($"GUIDKey not found for AssetImpactLikelihoodScores...");
                            s_assetImpactLikelihoodScores.Add(GUIDKey, edgeImpactedLikelihoodScore);
                            CalcLogEntry($"AssetImpactLikelihoodScores = {edgeImpactedLikelihoodScore} ");
                        }

                        if (s_assetImpactScores.ContainsKey(GUIDKey))
                        {
                            CalcLogEntry($"Node GUID {GUIDKey} has a value of: {s_assetImpactScores[GUIDKey]} ");
                            s_assetImpactScores[GUIDKey] = impactScore;
                        }
                        else
                        {
                            if (impactScore != -1)
                            {
                                CalcLogEntry($"This GUID Key does not exist. Creating new entry to AssetImpactScores...");
                                s_assetImpactScores.Add(GUIDKey, impactScore);
                                CalcLogEntry($"AssetImpactScores = {s_assetImpactScores[GUIDKey]} ");
                            }
                            else
                                CalcLogEntry($"The Impact Score has not be correctly calculated");
                        }

                    }

                    CalcLogEntry($"Set assetLikelihoodScore to: {edgeImpactedLikelihoodScore}");
                    GraphUtil.SetNodeData(CurrentNodeID, "assetLikelihoodScore", edgeImpactedLikelihoodScore.ToString());
                    GraphUtil.AddToNodeScores(CurrentNodeID + ":assetLikelihoodScore", edgeImpactedLikelihoodScore);

                }

                if (incommingNodeType == "asset" || incommingNodeType == "asset-group") // Called where current Asset is a child to another Asset
                {
                    GUIDKey = CurrentNodeID + ":" + ParentGUID;

                    CalcLogEntry($"Parent Node is an Asset Node");

                    double parentNodeAssetScore = GraphUtil.GetAssetNodeScore(ParentGUID);
                    CalcLogEntry($"Parent Node Asset Score is :{parentNodeAssetScore}");

                    double parentNodeAssetMitigatedScore = GraphUtil.GetAssetNodeMitigatedScore(ParentGUID);
                    CalcLogEntry($"Parent Node Asset Mitigated Score is :{parentNodeAssetMitigatedScore}");

                    double parentAssetNodeLikelihoodScore = GraphUtil.GetAssetNodeLikelihoodScore(ParentGUID);
                    CalcLogEntry($"Parent Node Impact Score is :{parentAssetNodeLikelihoodScore}");

                    double parentAssetNodeImpactScore = GraphUtil.GetAssetNodeImpactScore(ParentGUID);
                    CalcLogEntry($"Parent Node Impact Score is :{parentAssetNodeImpactScore}");

                    parentAssetNodeConfidentialityMitigatedScore = GraphUtil.GetAssetNodeConfidentialityMitigatedValue(ParentGUID);
                    CalcLogEntry($"Parent Node ConfidentialityMitigatedValue is :{parentAssetNodeConfidentialityMitigatedScore}");

                    parentAssetNodeIntegrityMitigatedScore = GraphUtil.GetAssetNodeIntegrityMitigatedScore(ParentGUID);
                    CalcLogEntry($"Parent Node ntegrityMitigatedScore is :{parentAssetNodeIntegrityMitigatedScore}");

                    parentAssetNodeAvailabilityMitigatedScore = GraphUtil.GetAssetNodeAvailabilityMitigatedScore(ParentGUID);
                    CalcLogEntry($"Parent Node AvailabilityMitigatedScore is :{parentAssetNodeAvailabilityMitigatedScore}");

                    parentAssetNodeAccountabilityMitigatedScore = GraphUtil.GetAssetNodeAccountabilityMitigatedScore(ParentGUID);
                    CalcLogEntry($"Parent Node AccountabilityMitigatedScore is :{parentAssetNodeAccountabilityMitigatedScore}");

                    parentAssetNodeObjectiveTargetValue = GraphUtil.GetNodeObjectiveTargetValue(ParentGUID);
                    CalcLogEntry($"Parent Node ObjectiveTargetScore is :{parentAssetNodeObjectiveTargetValue}");

                    parentAssetNodeObjectiveAcheivedValue = GraphUtil.GetNodeObjectiveAcheivedValue(ParentGUID);
                    CalcLogEntry($"Parent Node ObjectiveAcheivedScore is :{parentAssetNodeObjectiveAcheivedValue}");

                    string edgeID = GraphUtil.GetEdgeBetweenNodes(ParentGUID, CurrentNodeID);
                    CalcLogEntry($"Previous Edge is: {edgeID}");
                    edgeStrengthValue = GraphUtil.GetEdgeStrengthScore(edgeID);
                    CalcLogEntry($"Previous Edge Strenth Value is: {edgeStrengthValue.ToString()}");

                    // Calculating the Edge Mitigated Asset Score
                    CalcLogEntry($"Calculating Edge Impacted Asset Score...");
                    CalcLogEntry($"edgeImpactedAssetScore = parentNodeAssetScore * edgeStrengthValue");
                    CalcLogEntry($"edgeImpactedAssetScore = {parentNodeAssetScore.ToString()} * {edgeStrengthValue.ToString()}");
                    double edgeImpactedAssetScore = parentNodeAssetScore * edgeStrengthValue;
                    if (edgeImpactedAssetScore < 0)
                    {
                        CalcLogEntry($"edgeImpactedAssetScore < 0, setting to 0");
                        edgeImpactedAssetScore = 0;
                    }
                    CalcLogEntry($"edgeImpactedAssetMitigatedScore = {edgeImpactedAssetScore.ToString()}");

                    // Calculating the Edge Mitigated Asset Mitigated Score
                    CalcLogEntry($"Calculating Edge Impacted Asset Mitigated Score...");
                    CalcLogEntry($"edgeImpactedAssetMitigatedScore = parentNodeAssetMitigatedScore * edgeStrengthValue");
                    CalcLogEntry($"edgeImpactedAssetMitigatedScore = {parentNodeAssetMitigatedScore.ToString()} * {edgeStrengthValue.ToString()}");
                    double edgeImpactedAssetMitigatedScore = parentNodeAssetMitigatedScore * edgeStrengthValue;
                    if (edgeImpactedAssetMitigatedScore < 0)
                    {
                        CalcLogEntry($"edgeImpactedAssetMitigatedScore < 0, setting to 0");
                        edgeImpactedAssetMitigatedScore = 0;
                    }
                    CalcLogEntry($"edgeImpactedAssetMitigatedScore = {edgeImpactedAssetMitigatedScore.ToString()}");

                    // Calculating the Edge Mitigated Impact Score
                    CalcLogEntry($"Calculating Edge Impacted Impact Score...");
                    CalcLogEntry($"edgeImpactedImpactScore = parentAssetNodeImpactScore * edgeStrengthValue");
                    CalcLogEntry($"edgeImpactedImpactScore = {parentAssetNodeImpactScore.ToString()} * {edgeStrengthValue.ToString()}");

                    double edgeImpactedImpactScore = parentAssetNodeImpactScore * edgeStrengthValue;
                    if (edgeImpactedImpactScore < 0)
                    {
                        CalcLogEntry($"edgeImpactedImpactScore < 0, setting to 0");
                        edgeImpactedImpactScore = 0;
                    }
                    CalcLogEntry($"edgeImpactedImpactScore = {edgeImpactedImpactScore.ToString()}");



                    // Calaculate the Edge Impacted Values 
                    var sourceAssetNodeScores = new Dictionary<string, double>
                    {
                        {"ConfidentialityMitigatedScore", parentAssetNodeConfidentialityMitigatedScore},
                        {"IntegrityMitigatedScore", parentAssetNodeIntegrityMitigatedScore},
                        {"AvailabilityMitigatedScore", parentAssetNodeAvailabilityMitigatedScore},
                        {"AccountabilityMitigatedScore", parentAssetNodeAccountabilityMitigatedScore},
                        {"assetLikelihoodScore", parentAssetNodeLikelihoodScore},
                        {"assetImpactScore", parentAssetNodeImpactScore},
                        {"parentNodeAssetScore", parentNodeAssetScore},
                        {"parentNodeAssetMitigatedScore", parentNodeAssetMitigatedScore},
                        {"objectiveTargetValue", parentAssetNodeObjectiveTargetValue},
                        {"objectiveAcheivedValue", parentAssetNodeObjectiveAcheivedValue}

                    };

                    var updatedScores = new Dictionary<string, double>();

                    foreach (var score in sourceAssetNodeScores)
                    {
                        double edgeImpactedScore = score.Value * edgeStrengthValue;
                        updatedScores[score.Key] = edgeImpactedScore;
                        CalcLogEntry($"edgeImpactedSourceAssetNode{score.Key} = {edgeImpactedScore}");
                    }

                    foreach (var updatedScore in updatedScores)
                    {
                        sourceAssetNodeScores[updatedScore.Key] = updatedScore.Value;
                    }


                    //Get the behaviour of the current node
                    CalcLogEntry($"Getting Current Node Behaviour...");
                    string nodeBehaviour = GraphUtil.GetnodeBehaviour(CurrentNodeID);
                    CalcLogEntry($"Node Behaviour is {nodeBehaviour}");



                    if (nodeBehaviour == "Sum")
                    {
                        CalcLogEntry($"Calculating Sum...");

                        foreach (var scoreType in sourceAssetNodeScores)
                        {
                            string key = $"{GUIDKey}:{scoreType.Key}";
                            if (s_calculatedValues.TryGetValue(key, out var _))
                            {
                                CalcLogEntry($"s_calculatedValues already contains key {key}");
                                s_calculatedValues[key] = scoreType.Value;
                                CalcLogEntry($"Updated {key}, {scoreType.Value} in s_calculatedValues contains");
                            }
                            else
                            {
                                CalcLogEntry($"s_calculatedValues does not contain key {key}");
                                s_calculatedValues.Add(key, scoreType.Value);
                                CalcLogEntry($"Added {key}, {scoreType.Value} to s_calculatedValues contains");
                            }
                        }

                        // assetScore
                        // Now iterate through all and calculate the sum
                        double sumEdgeImpactedAssetScore = 0;
                        foreach (string Id in s_calculatedValues.Keys.ToList())
                        {
                            if (Id.StartsWith(CurrentNodeID) && Id.EndsWith(":parentNodeAssetScore"))  // Find Parent Nodes values for assetLikelihoodScore 
                            {
                                sumEdgeImpactedAssetScore += s_calculatedValues[Id];
                            }
                        }
                        sumEdgeImpactedAssetScore = GraphUtil.ClampNodeScore(sumEdgeImpactedAssetScore);
                        CalcLogEntry($"Set assetScore to: {sumEdgeImpactedAssetScore}");
                        GraphUtil.SetNodeData(CurrentNodeID, "assetScore", sumEdgeImpactedAssetScore.ToString());
                        GraphUtil.AddToNodeScores(CurrentNodeID + ":assetScore", sumEdgeImpactedAssetScore);

                        // assetMitigatedScore
                        // Now iterate through all and calculate the sum
                        double sumEdgeImpacteAssetMitigatedScore = 0;
                        foreach (string Id in s_calculatedValues.Keys.ToList())
                        {
                            if (Id.StartsWith(CurrentNodeID) && Id.EndsWith(":parentNodeAssetMitigatedScore"))  // Find Parent Nodes values for assetLikelihoodScore 
                            {
                                sumEdgeImpacteAssetMitigatedScore += s_calculatedValues[Id];
                            }
                        }
                        sumEdgeImpacteAssetMitigatedScore = GraphUtil.ClampNodeScore(sumEdgeImpacteAssetMitigatedScore);
                        CalcLogEntry($"Set assetMitigatedScore to: {sumEdgeImpacteAssetMitigatedScore}");
                        GraphUtil.SetNodeData(CurrentNodeID, "assetMitigatedScore", sumEdgeImpacteAssetMitigatedScore.ToString());
                        GraphUtil.AddToNodeScores(CurrentNodeID + ":assetMitigatedScore", sumEdgeImpacteAssetMitigatedScore);

                        // assetLikelihoodScore
                        // Now iterate through all and calculate the sum
                        double sumEdgeImpacteAssetLikelihoodScore = 0;
                        foreach (string Id in s_calculatedValues.Keys.ToList())
                        {
                            if (Id.StartsWith(CurrentNodeID) && Id.EndsWith(":assetLikelihoodScore"))  // Find Parent Nodes values for assetLikelihoodScore 
                            {
                                sumEdgeImpacteAssetLikelihoodScore += s_calculatedValues[Id];
                            }
                        }
                        sumEdgeImpacteAssetLikelihoodScore = GraphUtil.ClampNodeScore(sumEdgeImpacteAssetLikelihoodScore);
                        CalcLogEntry($"Set assetLikelihoodScore to: {sumEdgeImpacteAssetLikelihoodScore}");
                        GraphUtil.SetNodeData(CurrentNodeID, "assetLikelihoodScore", sumEdgeImpacteAssetLikelihoodScore.ToString());
                        GraphUtil.AddToNodeScores(CurrentNodeID + ":assetLikelihoodScore", sumEdgeImpacteAssetLikelihoodScore);

                        //ImpactScore
                        edgeImpactedParentAssetNodeImpactScore = 0;
                        foreach (string Id in s_calculatedValues.Keys.ToList())
                        {
                            if (Id.StartsWith(CurrentNodeID) && Id.EndsWith(":assetImpactScore"))  // Find Parent Nodes values for impactScore 
                            {
                                edgeImpactedParentAssetNodeImpactScore += s_calculatedValues[Id];
                            }
                        }
                        CalcLogEntry($"Set impactScore to: {edgeImpactedParentAssetNodeImpactScore}");
                        GraphUtil.SetNodeData(CurrentNodeID, "impactScore", edgeImpactedParentAssetNodeImpactScore.ToString());
                        GraphUtil.AddToNodeScores(CurrentNodeID + ":impactScore", edgeImpactedParentAssetNodeImpactScore);
                        GraphUtil.AddToDistributionData(CurrentNodeID + ":impactScore", (int)edgeImpactedParentAssetNodeImpactScore);

                        // ConfidentialityMitigatedScore
                        edgeImpactedParentAssetNodeConfidentialityMitigatedScore = 0;
                        foreach (string Id in s_calculatedValues.Keys.ToList())
                        {
                            if (Id.StartsWith(CurrentNodeID) && Id.EndsWith(":ConfidentialityMitigatedScore"))  // Find Parent Nodes values for ConfidentialityMitigatedScore 
                            {
                                edgeImpactedParentAssetNodeConfidentialityMitigatedScore += s_calculatedValues[Id];
                            }
                        }


                        // IntegrityMitigatedScore
                        edgeImpactedParentAssetNodeIntegrityMitigatedScore = 0;
                        foreach (string Id in s_calculatedValues.Keys.ToList())
                        {
                            if (Id.StartsWith(CurrentNodeID) && Id.EndsWith(":IntegrityMitigatedScore"))  // Find Parent Nodes values for IntegrityMitigatedScore 
                            {
                                edgeImpactedParentAssetNodeIntegrityMitigatedScore += s_calculatedValues[Id];
                            }
                        }


                        //AvailabilityyMitigatedScore
                        edgeImpactedParentAssetNodeAvailabilityMitigatedScore = 0;
                        foreach (string Id in s_calculatedValues.Keys.ToList())
                        {
                            if (Id.StartsWith(CurrentNodeID) && Id.EndsWith(":AvailabilityMitigatedScore"))  // Find Parent Nodes values for AvailabilityyMitigatedScore 
                            {
                                edgeImpactedParentAssetNodeAvailabilityMitigatedScore += s_calculatedValues[Id];
                            }
                        }


                        //AccountabilityMitigatedScore
                        edgeImpactedParentAssetNodeAccountabilityMitigatedScore = 0;
                        foreach (string Id in s_calculatedValues.Keys.ToList())
                        {
                            if (Id.StartsWith(CurrentNodeID) && Id.EndsWith(":AccountabilityMitigatedScore"))  // Find Parent Nodes values for AccountabilityMitigatedScore 
                            {
                                edgeImpactedParentAssetNodeAccountabilityMitigatedScore += s_calculatedValues[Id];
                            }
                        }

                        //ObjectiveTargetScore
                        edgeImpactedParentAssetNodeObjectiveTargetValue = 0;
                        foreach (string Id in s_calculatedValues.Keys.ToList())
                        {
                            if (Id.StartsWith(CurrentNodeID) && Id.EndsWith(":objectiveTargetValue"))  // Find Parent Nodes values for AccountabilityMitigatedScore 
                            {
                                edgeImpactedParentAssetNodeObjectiveTargetValue += s_calculatedValues[Id];
                            }
                        }
                        CalcLogEntry($"Set objectiveTargetValue to: {edgeImpactedParentAssetNodeObjectiveTargetValue}");
                        GraphUtil.SetNodeData(CurrentNodeID, "objectiveTargetValue", edgeImpactedParentAssetNodeObjectiveTargetValue.ToString());

                        //ObjectiveAcheivedScore
                        edgeImpactedParentAssetNodeObjectiveAcheivedValue = 0;
                        foreach (string Id in s_calculatedValues.Keys.ToList())
                        {
                            if (Id.StartsWith(CurrentNodeID) && Id.EndsWith(":objectiveAcheivedValue"))  // Find Parent Nodes values for AccountabilityMitigatedScore 
                            {
                                edgeImpactedParentAssetNodeObjectiveAcheivedValue += s_calculatedValues[Id];
                            }
                        }
                        CalcLogEntry($"Set objectiveAcheivedValue to: {edgeImpactedParentAssetNodeObjectiveAcheivedValue}");
                        GraphUtil.SetNodeData(CurrentNodeID, "objectiveAcheivedValue", edgeImpactedParentAssetNodeObjectiveAcheivedValue.ToString());

                    }

                    if (nodeBehaviour == "High Water Mark")
                    {
                        CalcLogEntry($"Calculating High Water Mark...");

                        foreach (var scoreType in sourceAssetNodeScores)
                        {
                            string key = $"{GUIDKey}:{scoreType.Key}";
                            if (s_calculatedValues.TryGetValue(key, out var _))
                            {
                                CalcLogEntry($"s_calculatedValues already contains key {key}");
                                s_calculatedValues[key] = scoreType.Value;
                                CalcLogEntry($"Updated {key}, {scoreType.Value} in s_calculatedValues contains");
                            }
                            else
                            {
                                CalcLogEntry($"s_calculatedValues does not contain key {key}");
                                s_calculatedValues.Add(key, scoreType.Value);
                                CalcLogEntry($"Added {key}, {scoreType.Value} to s_calculatedValues contains");
                            }
                        }

                        // assetScore
                        // Now iterate through all and calculate the sum
                        double sumEdgeImpacteAssetScore = 0;
                        foreach (string Id in s_calculatedValues.Keys.ToList())
                        {
                            if (Id.StartsWith(CurrentNodeID) && Id.EndsWith(":parentNodeAssetScore"))  // Find Parent Nodes values for assetLikelihoodScore 
                            {
                                if (s_calculatedValues[Id] > sumEdgeImpacteAssetScore)
                                    sumEdgeImpacteAssetScore = s_calculatedValues[Id];
                            }
                        }
                        sumEdgeImpacteAssetScore = GraphUtil.ClampNodeScore(sumEdgeImpacteAssetScore);
                        CalcLogEntry($"Set assetScore to: {sumEdgeImpacteAssetScore}");
                        GraphUtil.SetNodeData(CurrentNodeID, "assetScore", sumEdgeImpacteAssetScore.ToString());
                        GraphUtil.AddToNodeScores(CurrentNodeID + ":assetScore", sumEdgeImpacteAssetScore);

                        // assetMitigatedScore
                        // Now iterate through all and calculate the sum
                        double sumEdgeImpacteAssetMitigatedScore = 0;
                        foreach (string Id in s_calculatedValues.Keys.ToList())
                        {
                            if (Id.StartsWith(CurrentNodeID) && Id.EndsWith(":parentNodeAssetMitigatedScore"))  // Find Parent Nodes values for assetLikelihoodScore 
                            {
                                if (s_calculatedValues[Id] > sumEdgeImpacteAssetMitigatedScore)
                                    sumEdgeImpacteAssetMitigatedScore = s_calculatedValues[Id];
                            }
                        }
                        sumEdgeImpacteAssetMitigatedScore = GraphUtil.ClampNodeScore(sumEdgeImpacteAssetMitigatedScore);
                        CalcLogEntry($"Set assetMitigatedScore to: {sumEdgeImpacteAssetMitigatedScore}");
                        GraphUtil.SetNodeData(CurrentNodeID, "assetMitigatedScore", sumEdgeImpacteAssetMitigatedScore.ToString());
                        GraphUtil.AddToNodeScores(CurrentNodeID + ":assetMitigatedScore", sumEdgeImpacteAssetMitigatedScore);


                        // assetLikelihoodScore
                        // Now iterate through all and get the highest value
                        double edgeImpacteAssetLikelihoodScore = 0;
                        foreach (string Id in s_calculatedValues.Keys.ToList())
                        {
                            if (Id.StartsWith(CurrentNodeID) && Id.EndsWith(":assetLikelihoodScore"))  // Find Parent Nodes values for assetLikelihoodScore 
                            {
                                if (s_calculatedValues[Id] > edgeImpacteAssetLikelihoodScore)
                                    edgeImpacteAssetLikelihoodScore = s_calculatedValues[Id];
                            }
                        }
                        edgeImpacteAssetLikelihoodScore = GraphUtil.ClampNodeScore(edgeImpacteAssetLikelihoodScore);
                        CalcLogEntry($"Set assetLikelihoodScore to: {edgeImpacteAssetLikelihoodScore}");
                        GraphUtil.SetNodeData(CurrentNodeID, "assetLikelihoodScore", edgeImpacteAssetLikelihoodScore.ToString());
                        GraphUtil.AddToNodeScores(CurrentNodeID + ":assetLikelihoodScore", edgeImpacteAssetLikelihoodScore);


                        //ImpactScore
                        edgeImpactedParentAssetNodeImpactScore = 0;
                        foreach (string Id in s_calculatedValues.Keys.ToList())
                        {
                            if (Id.StartsWith(CurrentNodeID) && Id.EndsWith(":assetImpactScore"))  // Find Parent Nodes values for impactScore 
                            {
                                if (s_calculatedValues[Id] > edgeImpactedParentAssetNodeImpactScore)
                                    edgeImpactedParentAssetNodeImpactScore = s_calculatedValues[Id];
                            }
                        }
                        CalcLogEntry($"Set impactScore to: {edgeImpactedParentAssetNodeImpactScore}");
                        GraphUtil.SetNodeData(CurrentNodeID, "impactScore", edgeImpactedParentAssetNodeImpactScore.ToString());
                        GraphUtil.AddToNodeScores(CurrentNodeID + ":impactScore", edgeImpactedParentAssetNodeImpactScore);
                        GraphUtil.AddToDistributionData(CurrentNodeID + ":impactScore", (int)edgeImpactedParentAssetNodeImpactScore);

                        // ConfidentialityMitigatedScore
                        edgeImpactedParentAssetNodeConfidentialityMitigatedScore = 0;
                        foreach (string Id in s_calculatedValues.Keys.ToList())
                        {
                            if (Id.StartsWith(CurrentNodeID) && Id.EndsWith(":ConfidentialityMitigatedScore"))  // Find Parent Nodes values for ConfidentialityMitigatedScore 
                            {
                                if (s_calculatedValues[Id] > edgeImpactedParentAssetNodeConfidentialityMitigatedScore)
                                    edgeImpactedParentAssetNodeConfidentialityMitigatedScore = s_calculatedValues[Id];
                            }
                        }


                        // IntegrityMitigatedScore
                        edgeImpactedParentAssetNodeIntegrityMitigatedScore = 0;
                        foreach (string Id in s_calculatedValues.Keys.ToList())
                        {
                            if (Id.StartsWith(CurrentNodeID) && Id.EndsWith(":IntegrityMitigatedScore"))  // Find Parent Nodes values for IntegrityMitigatedScore 
                            {
                                if (s_calculatedValues[Id] > edgeImpactedParentAssetNodeIntegrityMitigatedScore)
                                    edgeImpactedParentAssetNodeIntegrityMitigatedScore = s_calculatedValues[Id];
                            }
                        }


                        //AvailabilityyMitigatedScore
                        edgeImpactedParentAssetNodeAvailabilityMitigatedScore = 0;
                        foreach (string Id in s_calculatedValues.Keys.ToList())
                        {
                            if (Id.StartsWith(CurrentNodeID) && Id.EndsWith(":AvailabilityMitigatedScore"))  // Find Parent Nodes values for AvailabilityyMitigatedScore 
                            {
                                if (s_calculatedValues[Id] > edgeImpactedParentAssetNodeAvailabilityMitigatedScore)
                                    edgeImpactedParentAssetNodeAvailabilityMitigatedScore = s_calculatedValues[Id];
                            }
                        }

                        //AccountabilityMitigatedScore
                        edgeImpactedParentAssetNodeAccountabilityMitigatedScore = 0;
                        foreach (string Id in s_calculatedValues.Keys.ToList())
                        {
                            if (Id.StartsWith(CurrentNodeID) && Id.EndsWith(":AccountabilityMitigatedScore"))  // Find Parent Nodes values for AccountabilityMitigatedScore 
                            {
                                if (s_calculatedValues[Id] > edgeImpactedParentAssetNodeAccountabilityMitigatedScore)
                                    edgeImpactedParentAssetNodeAccountabilityMitigatedScore = s_calculatedValues[Id];
                            }
                        }

                        //ObjectiveTargetScore
                        edgeImpactedParentAssetNodeObjectiveTargetValue = 0;
                        foreach (string Id in s_calculatedValues.Keys.ToList())
                        {
                            if (Id.StartsWith(CurrentNodeID) && Id.EndsWith(":objectiveTargetValue"))  // Find Parent Nodes values for AccountabilityMitigatedScore 
                            {
                                if (s_calculatedValues[Id] > edgeImpactedParentAssetNodeObjectiveTargetValue)
                                    edgeImpactedParentAssetNodeObjectiveTargetValue = s_calculatedValues[Id];
                            }
                        }
                        CalcLogEntry($"Set objectiveTargetValue to: {edgeImpactedParentAssetNodeObjectiveTargetValue}");
                        GraphUtil.SetNodeData(CurrentNodeID, "objectiveTargetValue", edgeImpactedParentAssetNodeObjectiveTargetValue.ToString());

                        //ObjectiveAcheivedScore
                        edgeImpactedParentAssetNodeObjectiveAcheivedValue = 0;
                        foreach (string Id in s_calculatedValues.Keys.ToList())
                        {
                            if (Id.StartsWith(CurrentNodeID) && Id.EndsWith(":objectiveAcheivedValue"))  // Find Parent Nodes values for AccountabilityMitigatedScore 
                            {
                                if (s_calculatedValues[Id] > edgeImpactedParentAssetNodeObjectiveAcheivedValue)
                                    edgeImpactedParentAssetNodeObjectiveAcheivedValue = s_calculatedValues[Id];
                            }
                        }
                        CalcLogEntry($"Set objectiveAcheivedValue to: {edgeImpactedParentAssetNodeObjectiveAcheivedValue}");
                        GraphUtil.SetNodeData(CurrentNodeID, "objectiveAcheivedValue", edgeImpactedParentAssetNodeObjectiveAcheivedValue.ToString());
                    }

                    if (nodeBehaviour == "Low Water Mark")
                    {
                        CalcLogEntry($"Calculating Low Water Mark...");

                        foreach (var scoreType in sourceAssetNodeScores)
                        {
                            string key = $"{GUIDKey}:{scoreType.Key}";
                            if (s_calculatedValues.TryGetValue(key, out var _))
                            {
                                CalcLogEntry($"s_calculatedValues already contains key {key}");
                                s_calculatedValues[key] = scoreType.Value;
                                CalcLogEntry($"Updated {key}, {scoreType.Value} in s_calculatedValues contains");
                            }
                            else
                            {
                                CalcLogEntry($"s_calculatedValues does not contain key {key}");
                                s_calculatedValues.Add(key, scoreType.Value);
                                CalcLogEntry($"Added {key}, {scoreType.Value} to s_calculatedValues contains");
                            }
                        }

                        // assetScore
                        // Now iterate through all and calculate the sum
                        double sumEdgeImpacteAssetScore = 999;
                        foreach (string Id in s_calculatedValues.Keys.ToList())
                        {
                            if (Id.StartsWith(CurrentNodeID) && Id.EndsWith(":parentNodeAssetScore"))  // Find Parent Nodes values for assetLikelihoodScore 
                            {
                                if (s_calculatedValues[Id] < sumEdgeImpacteAssetScore)
                                    sumEdgeImpacteAssetScore = s_calculatedValues[Id];
                            }
                        }
                        if (sumEdgeImpacteAssetScore == 999) sumEdgeImpacteAssetScore = 0;
                        sumEdgeImpacteAssetScore = GraphUtil.ClampNodeScore(sumEdgeImpacteAssetScore);
                        CalcLogEntry($"Set assetScore to: {sumEdgeImpacteAssetScore}");
                        GraphUtil.SetNodeData(CurrentNodeID, "assetScore", sumEdgeImpacteAssetScore.ToString());
                        GraphUtil.AddToNodeScores(CurrentNodeID + ":assetScore", sumEdgeImpacteAssetScore);

                        // assetMitigatedScore
                        // Now iterate through all and calculate the sum
                        double sumEdgeImpacteAssetMitigatedScore = 999;
                        foreach (string Id in s_calculatedValues.Keys.ToList())
                        {
                            if (Id.StartsWith(CurrentNodeID) && Id.EndsWith(":parentNodeAssetMitigatedScore"))  // Find Parent Nodes values for assetLikelihoodScore 
                            {
                                if (s_calculatedValues[Id] < sumEdgeImpacteAssetMitigatedScore)
                                    sumEdgeImpacteAssetMitigatedScore = s_calculatedValues[Id];
                            }
                        }
                        if (sumEdgeImpacteAssetMitigatedScore == 999) sumEdgeImpacteAssetMitigatedScore = 0;
                        sumEdgeImpacteAssetMitigatedScore = GraphUtil.ClampNodeScore(sumEdgeImpacteAssetMitigatedScore);
                        CalcLogEntry($"Set assetMitigatedScore to: {sumEdgeImpacteAssetMitigatedScore}");
                        GraphUtil.SetNodeData(CurrentNodeID, "assetMitigatedScore", sumEdgeImpacteAssetMitigatedScore.ToString());
                        GraphUtil.AddToNodeScores(CurrentNodeID + ":assetMitigatedScore", sumEdgeImpacteAssetMitigatedScore);


                        // assetLikelihoodScore
                        // Now iterate through all and get the highest value
                        double edgeImpacteAssetLikelihoodScore = 999;
                        foreach (string Id in s_calculatedValues.Keys.ToList())
                        {
                            if (Id.StartsWith(CurrentNodeID) && Id.EndsWith(":assetLikelihoodScore"))  // Find Parent Nodes values for assetLikelihoodScore 
                            {
                                if (s_calculatedValues[Id] < edgeImpacteAssetLikelihoodScore)
                                    edgeImpacteAssetLikelihoodScore = s_calculatedValues[Id];
                            }
                        }
                        if (edgeImpacteAssetLikelihoodScore == 999) edgeImpacteAssetLikelihoodScore = 0;
                        edgeImpacteAssetLikelihoodScore = GraphUtil.ClampNodeScore(edgeImpacteAssetLikelihoodScore);
                        CalcLogEntry($"Set assetLikelihoodScore to: {edgeImpacteAssetLikelihoodScore}");
                        GraphUtil.SetNodeData(CurrentNodeID, "assetLikelihoodScore", edgeImpacteAssetLikelihoodScore.ToString());
                        GraphUtil.AddToNodeScores(CurrentNodeID + ":assetLikelihoodScore", edgeImpacteAssetLikelihoodScore);


                        //ImpactScore
                        edgeImpactedParentAssetNodeImpactScore = 999;
                        foreach (string Id in s_calculatedValues.Keys.ToList())
                        {
                            if (Id.StartsWith(CurrentNodeID) && Id.EndsWith(":assetImpactScore"))  // Find Parent Nodes values for impactScore 
                            {
                                if (s_calculatedValues[Id] < edgeImpactedParentAssetNodeImpactScore)
                                    edgeImpactedParentAssetNodeImpactScore = s_calculatedValues[Id];
                            }
                        }
                        if (edgeImpactedParentAssetNodeImpactScore == 999) edgeImpactedParentAssetNodeImpactScore = 0;
                        CalcLogEntry($"Set impactScore to: {edgeImpactedParentAssetNodeImpactScore}");
                        GraphUtil.SetNodeData(CurrentNodeID, "impactScore", edgeImpactedParentAssetNodeImpactScore.ToString());
                        GraphUtil.AddToNodeScores(CurrentNodeID + ":impactScore", edgeImpactedParentAssetNodeImpactScore);
                        GraphUtil.AddToDistributionData(CurrentNodeID + ":impactScore", (int)edgeImpactedParentAssetNodeImpactScore);


                        // ConfidentialityMitigatedScore
                        edgeImpactedParentAssetNodeConfidentialityMitigatedScore = 999;
                        foreach (string Id in s_calculatedValues.Keys.ToList())
                        {
                            if (Id.StartsWith(CurrentNodeID) && Id.EndsWith(":ConfidentialityMitigatedScore"))  // Find Parent Nodes values for ConfidentialityMitigatedScore 
                            {
                                if (s_calculatedValues[Id] < edgeImpactedParentAssetNodeConfidentialityMitigatedScore)
                                    edgeImpactedParentAssetNodeConfidentialityMitigatedScore = s_calculatedValues[Id];
                            }
                        }
                        if (edgeImpactedParentAssetNodeConfidentialityMitigatedScore == 999) edgeImpactedParentAssetNodeConfidentialityMitigatedScore = 0;



                        // IntegrityMitigatedScore
                        edgeImpactedParentAssetNodeIntegrityMitigatedScore = 999;
                        foreach (string Id in s_calculatedValues.Keys.ToList())
                        {
                            if (Id.StartsWith(CurrentNodeID) && Id.EndsWith(":IntegrityMitigatedScore"))  // Find Parent Nodes values for IntegrityMitigatedScore 
                            {
                                if (s_calculatedValues[Id] < edgeImpactedParentAssetNodeIntegrityMitigatedScore)
                                    edgeImpactedParentAssetNodeIntegrityMitigatedScore = s_calculatedValues[Id];
                            }
                        }
                        if (edgeImpactedParentAssetNodeIntegrityMitigatedScore == 999) edgeImpactedParentAssetNodeIntegrityMitigatedScore = 0;


                        //AvailabilityyMitigatedScore
                        edgeImpactedParentAssetNodeAvailabilityMitigatedScore = 999;
                        foreach (string Id in s_calculatedValues.Keys.ToList())
                        {
                            if (Id.StartsWith(CurrentNodeID) && Id.EndsWith(":AvailabilityMitigatedScore"))  // Find Parent Nodes values for AvailabilityyMitigatedScore 
                            {
                                if (s_calculatedValues[Id] < edgeImpactedParentAssetNodeAvailabilityMitigatedScore)
                                    edgeImpactedParentAssetNodeAvailabilityMitigatedScore = s_calculatedValues[Id];
                            }
                        }
                        if (edgeImpactedParentAssetNodeAvailabilityMitigatedScore == 999) edgeImpactedParentAssetNodeAvailabilityMitigatedScore = 0;


                        //AccountabilityMitigatedScore
                        edgeImpactedParentAssetNodeAccountabilityMitigatedScore = 999;
                        foreach (string Id in s_calculatedValues.Keys.ToList())
                        {
                            if (Id.StartsWith(CurrentNodeID) && Id.EndsWith(":AccountabilityMitigatedScore"))  // Find Parent Nodes values for AccountabilityMitigatedScore 
                            {
                                if (s_calculatedValues[Id] < edgeImpactedParentAssetNodeAccountabilityMitigatedScore)
                                    edgeImpactedParentAssetNodeAccountabilityMitigatedScore = s_calculatedValues[Id];
                            }
                        }
                        if (edgeImpactedParentAssetNodeAccountabilityMitigatedScore == 999) edgeImpactedParentAssetNodeAccountabilityMitigatedScore = 0;


                        //ObjectiveTargetScore
                        edgeImpactedParentAssetNodeObjectiveTargetValue = 999;
                        foreach (string Id in s_calculatedValues.Keys.ToList())
                        {
                            if (Id.StartsWith(CurrentNodeID) && Id.EndsWith(":objectiveTargetValue"))  // Find Parent Nodes values for AccountabilityMitigatedScore 
                            {
                                if (s_calculatedValues[Id] < edgeImpactedParentAssetNodeObjectiveTargetValue)
                                    edgeImpactedParentAssetNodeObjectiveTargetValue = s_calculatedValues[Id];
                            }
                        }
                        if (edgeImpactedParentAssetNodeObjectiveTargetValue == 999) edgeImpactedParentAssetNodeObjectiveTargetValue = 0;
                        CalcLogEntry($"Set objectiveTargetValue to: {edgeImpactedParentAssetNodeObjectiveTargetValue}");
                        GraphUtil.SetNodeData(CurrentNodeID, "objectiveTargetValue", edgeImpactedParentAssetNodeObjectiveTargetValue.ToString());

                        //ObjectiveAcheivedScore
                        edgeImpactedParentAssetNodeObjectiveAcheivedValue = 999;
                        foreach (string Id in s_calculatedValues.Keys.ToList())
                        {
                            if (Id.StartsWith(CurrentNodeID) && Id.EndsWith(":objectiveAcheivedValue"))  // Find Parent Nodes values for AccountabilityMitigatedScore 
                            {
                                if (s_calculatedValues[Id] < edgeImpactedParentAssetNodeObjectiveAcheivedValue)
                                    edgeImpactedParentAssetNodeObjectiveAcheivedValue = s_calculatedValues[Id];
                            }
                        }
                        if (edgeImpactedParentAssetNodeObjectiveAcheivedValue == 999) edgeImpactedParentAssetNodeObjectiveAcheivedValue = 0;
                        CalcLogEntry($"Set objectiveAcheivedValue to: {edgeImpactedParentAssetNodeObjectiveAcheivedValue}");
                        GraphUtil.SetNodeData(CurrentNodeID, "objectiveAcheivedValue", edgeImpactedParentAssetNodeObjectiveAcheivedValue.ToString());

                    }

                    if (nodeBehaviour == "Average")
                    {
                        CalcLogEntry($"Calculating Average...");
                        int tempCount = 0;

                        foreach (var scoreType in sourceAssetNodeScores)
                        {
                            string key = $"{GUIDKey}:{scoreType.Key}";
                            if (s_calculatedValues.TryGetValue(key, out var _))
                            {
                                CalcLogEntry($"s_calculatedValues already contains key {key}");
                                s_calculatedValues[key] = scoreType.Value;
                                CalcLogEntry($"Updated {key}, {scoreType.Value} in s_calculatedValues contains");
                            }
                            else
                            {
                                CalcLogEntry($"s_calculatedValues does not contain key {key}");
                                s_calculatedValues.Add(key, scoreType.Value);
                                CalcLogEntry($"Added {key}, {scoreType.Value} to s_calculatedValues contains");
                            }
                        }

                        // assetScore
                        // Now iterate through all and calculate the sum
                        double sumEdgeImpacteAssetScore = 0;
                        tempCount = 0;
                        foreach (string Id in s_calculatedValues.Keys.ToList())
                        {
                            if (Id.StartsWith(CurrentNodeID) && Id.EndsWith(":parentNodeAssetScore"))  // Find Parent Nodes values for assetLikelihoodScore 
                            {
                                sumEdgeImpacteAssetScore += s_calculatedValues[Id];
                                tempCount++;
                            }
                        }
                        sumEdgeImpacteAssetScore = sumEdgeImpacteAssetScore / tempCount;
                        sumEdgeImpacteAssetScore = GraphUtil.ClampNodeScore(sumEdgeImpacteAssetScore);
                        CalcLogEntry($"Set assetScore to: {sumEdgeImpacteAssetScore}");
                        GraphUtil.SetNodeData(CurrentNodeID, "assetScore", sumEdgeImpacteAssetScore.ToString());
                        GraphUtil.AddToNodeScores(CurrentNodeID + ":assetScore", sumEdgeImpacteAssetScore);

                        // assetMitigatedScore
                        // Now iterate through all and calculate the sum
                        double sumEdgeImpacteAssetMitigatedScore = 0;
                        tempCount = 0;
                        foreach (string Id in s_calculatedValues.Keys.ToList())
                        {
                            if (Id.StartsWith(CurrentNodeID) && Id.EndsWith(":parentNodeAssetMitigatedScore"))  // Find Parent Nodes values for assetLikelihoodScore 
                            {
                                sumEdgeImpacteAssetMitigatedScore += s_calculatedValues[Id];
                                tempCount++;
                            }
                        }
                        sumEdgeImpacteAssetMitigatedScore = sumEdgeImpacteAssetMitigatedScore / tempCount;
                        CalcLogEntry($"Set assetMitigatedScore to: {sumEdgeImpacteAssetMitigatedScore}");
                        GraphUtil.SetNodeData(CurrentNodeID, "assetMitigatedScore", sumEdgeImpacteAssetMitigatedScore.ToString());
                        GraphUtil.AddToNodeScores(CurrentNodeID + ":assetMitigatedScore", sumEdgeImpacteAssetMitigatedScore);


                        // assetLikelihoodScore
                        // Now iterate through all and get the highest value
                        double edgeImpacteAssetLikelihoodScore = 0;
                        tempCount = 0;
                        foreach (string Id in s_calculatedValues.Keys.ToList())
                        {
                            if (Id.StartsWith(CurrentNodeID) && Id.EndsWith(":assetLikelihoodScore"))  // Find Parent Nodes values for assetLikelihoodScore 
                            {
                                edgeImpacteAssetLikelihoodScore += s_calculatedValues[Id];
                                tempCount++;
                            }
                        }
                        edgeImpacteAssetLikelihoodScore = edgeImpacteAssetLikelihoodScore / tempCount;
                        edgeImpacteAssetLikelihoodScore = GraphUtil.ClampNodeScore(edgeImpacteAssetLikelihoodScore);
                        CalcLogEntry($"Set assetLikelihoodScore to: {edgeImpacteAssetLikelihoodScore}");
                        GraphUtil.SetNodeData(CurrentNodeID, "assetLikelihoodScore", edgeImpacteAssetLikelihoodScore.ToString());
                        GraphUtil.AddToNodeScores(CurrentNodeID + ":assetLikelihoodScore", edgeImpacteAssetLikelihoodScore);


                        //ImpactScore
                        edgeImpactedParentAssetNodeImpactScore = 0;
                        tempCount = 0;
                        foreach (string Id in s_calculatedValues.Keys.ToList())
                        {
                            if (Id.StartsWith(CurrentNodeID) && Id.EndsWith(":assetImpactScore"))  // Find Parent Nodes values for impactScore 
                            {
                                edgeImpactedParentAssetNodeImpactScore += s_calculatedValues[Id];
                                tempCount++;
                            }
                        }
                        edgeImpactedParentAssetNodeImpactScore = edgeImpactedParentAssetNodeImpactScore / tempCount;
                        CalcLogEntry($"Set impactScore to: {edgeImpactedParentAssetNodeImpactScore}");
                        GraphUtil.SetNodeData(CurrentNodeID, "impactScore", edgeImpactedParentAssetNodeImpactScore.ToString());
                        GraphUtil.AddToNodeScores(CurrentNodeID + ":impactScore", edgeImpactedParentAssetNodeImpactScore);
                        GraphUtil.AddToDistributionData(CurrentNodeID + ":impactScore", (int)edgeImpactedParentAssetNodeImpactScore);



                        // ConfidentialityMitigatedScore
                        edgeImpactedParentAssetNodeConfidentialityMitigatedScore = 0;
                        tempCount = 0;
                        foreach (string Id in s_calculatedValues.Keys.ToList())
                        {
                            if (Id.StartsWith(CurrentNodeID) && Id.EndsWith(":ConfidentialityMitigatedScore"))  // Find Parent Nodes values for ConfidentialityMitigatedScore 
                            {
                                edgeImpactedParentAssetNodeConfidentialityMitigatedScore += s_calculatedValues[Id];
                                tempCount++;
                            }
                        }
                        edgeImpactedParentAssetNodeConfidentialityMitigatedScore = edgeImpactedParentAssetNodeConfidentialityMitigatedScore / tempCount;

                        // IntegrityMitigatedScore
                        edgeImpactedParentAssetNodeIntegrityMitigatedScore = 0;
                        tempCount = 0;
                        foreach (string Id in s_calculatedValues.Keys.ToList())
                        {
                            if (Id.StartsWith(CurrentNodeID) && Id.EndsWith(":IntegrityMitigatedScore"))  // Find Parent Nodes values for IntegrityMitigatedScore 
                            {
                                edgeImpactedParentAssetNodeIntegrityMitigatedScore += s_calculatedValues[Id];
                                tempCount++;
                            }
                        }
                        edgeImpactedParentAssetNodeIntegrityMitigatedScore = edgeImpactedParentAssetNodeIntegrityMitigatedScore / tempCount;

                        //AvailabilityyMitigatedScore
                        edgeImpactedParentAssetNodeAvailabilityMitigatedScore = 0;
                        tempCount = 0;
                        foreach (string Id in s_calculatedValues.Keys.ToList())
                        {
                            if (Id.StartsWith(CurrentNodeID) && Id.EndsWith(":AvailabilityMitigatedScore"))  // Find Parent Nodes values for AvailabilityyMitigatedScore 
                            {
                                edgeImpactedParentAssetNodeAvailabilityMitigatedScore += s_calculatedValues[Id];
                                tempCount++;
                            }
                        }
                        edgeImpactedParentAssetNodeAvailabilityMitigatedScore = edgeImpactedParentAssetNodeAvailabilityMitigatedScore / tempCount;


                        //AccountabilityMitigatedScore
                        edgeImpactedParentAssetNodeAccountabilityMitigatedScore = 0;
                        tempCount = 0;
                        foreach (string Id in s_calculatedValues.Keys.ToList())
                        {
                            if (Id.StartsWith(CurrentNodeID) && Id.EndsWith(":AccountabilityMitigatedScore"))  // Find Parent Nodes values for AccountabilityMitigatedScore 
                            {
                                edgeImpactedParentAssetNodeAccountabilityMitigatedScore += s_calculatedValues[Id];
                                tempCount++;
                            }
                        }
                        edgeImpactedParentAssetNodeAccountabilityMitigatedScore = edgeImpactedParentAssetNodeAccountabilityMitigatedScore / tempCount;


                        //ObjectiveTargetScore
                        edgeImpactedParentAssetNodeObjectiveTargetValue = 0;
                        foreach (string Id in s_calculatedValues.Keys.ToList())
                        {
                            if (Id.StartsWith(CurrentNodeID) && Id.EndsWith(":objectiveTargetValue"))  // Find Parent Nodes values for AccountabilityMitigatedScore 
                            {
                                edgeImpactedParentAssetNodeObjectiveTargetValue += s_calculatedValues[Id];
                                tempCount++;
                            }
                        }
                        edgeImpactedParentAssetNodeObjectiveTargetValue = edgeImpactedParentAssetNodeObjectiveTargetValue / tempCount;
                        CalcLogEntry($"Set objectiveTargetValue to: {edgeImpactedParentAssetNodeObjectiveTargetValue}");
                        GraphUtil.SetNodeData(CurrentNodeID, "objectiveTargetValue", edgeImpactedParentAssetNodeObjectiveTargetValue.ToString());

                        //ObjectiveAcheivedScore
                        edgeImpactedParentAssetNodeObjectiveAcheivedValue = 0;
                        foreach (string Id in s_calculatedValues.Keys.ToList())
                        {
                            if (Id.StartsWith(CurrentNodeID) && Id.EndsWith(":objectiveAcheivedValue"))  // Find Parent Nodes values for AccountabilityMitigatedScore 
                            {
                                edgeImpactedParentAssetNodeObjectiveAcheivedValue += s_calculatedValues[Id];
                            }
                        }
                        edgeImpactedParentAssetNodeObjectiveAcheivedValue = edgeImpactedParentAssetNodeObjectiveAcheivedValue / tempCount;
                        CalcLogEntry($"Set objectiveAcheivedValue to: {edgeImpactedParentAssetNodeObjectiveAcheivedValue}");
                        GraphUtil.SetNodeData(CurrentNodeID, "objectiveAcheivedValue", edgeImpactedParentAssetNodeObjectiveAcheivedValue.ToString());

                    }


                    // Check for lowest Edge Mitigated Asset Scores
                    if (s_assetAssetScores.ContainsKey(GUIDKey))
                    {
                        CalcLogEntry($"Node GUID {GUIDKey} has a value of: {s_assetAssetScores[GUIDKey]} ");
                        //if (edgeImpactedAssetScore <= AssetAssetScores[GUIDKey])
                        //{
                        //    if (useCalcLog) calculationLog.Add((DateTime.Now, $"Node AssetAssetScores is greater that current AssetAssetScores...Setting new value...");
                        //    AssetAssetScores[GUIDKey] = edgeImpactedAssetScore;
                        //    if (useCalcLog) calculationLog.Add((DateTime.Now, $"AssetAssetScores = {AssetAssetScores[GUIDKey]} ");
                        //}
                        s_assetAssetScores[GUIDKey] = edgeImpactedAssetScore; // 17/01/23
                    }
                    else
                    {
                        CalcLogEntry($"GUIDKey not found for AssetAssetScores...");
                        s_assetAssetScores.Add(GUIDKey, edgeImpactedAssetScore);
                        CalcLogEntry($"AssetAssetScores = {edgeImpactedAssetScore} ");
                    }

                    // Check for hightest Edge Mitigated Asset Mitigated Scores
                    if (s_assetAssetMitigatedScores.ContainsKey(GUIDKey))
                    {
                        CalcLogEntry($"Node GUID {GUIDKey} has a value of: {s_assetAssetMitigatedScores[GUIDKey]} ");
                        //if (edgeImpactedAssetMitigatedScore <= AssetAssetMitigatedScores[GUIDKey])
                        //{
                        //    if (useCalcLog) calculationLog.Add((DateTime.Now, $"Node AssetAssetMitigatedScores is greater that current AssetAssetMitigatedScores...Setting new value...");
                        //    AssetAssetMitigatedScores[GUIDKey] = edgeImpactedAssetMitigatedScore;
                        //    if (useCalcLog) calculationLog.Add((DateTime.Now, $"AssetAssetMitigatedScores = {edgeImpactedAssetMitigatedScore} ");
                        //}
                        s_assetAssetMitigatedScores[GUIDKey] = edgeImpactedAssetMitigatedScore;
                    }
                    else
                    {
                        CalcLogEntry($"GUIDKey not found for AssetAssetMitigatedScores...");
                        s_assetAssetMitigatedScores.Add(GUIDKey, edgeImpactedAssetMitigatedScore);
                        CalcLogEntry($"AssetAssetMitigatedScores = {edgeImpactedAssetMitigatedScore} ");
                    }

                    // Check for hightest Edge Impacted Likelihood Score
                    if (s_assetImpactLikelihoodScores.ContainsKey(GUIDKey))
                    {
                        CalcLogEntry($"Node GUID {GUIDKey} has a value of: {s_assetImpactLikelihoodScores[GUIDKey]} ");
                        s_assetImpactLikelihoodScores[GUIDKey] = edgeImpactedLikelihoodScore;
                    }
                    else
                    {
                        CalcLogEntry($"GUIDKey not found for AssetImpactLikelihoodScores...");
                        s_assetImpactLikelihoodScores.Add(GUIDKey, edgeImpactedLikelihoodScore);
                        CalcLogEntry($"AssetImpactLikelihoodScores = {edgeImpactedLikelihoodScore} ");
                    }


                    // Check for hightest Edge Mitigated Impact Scores
                    if (s_assetImpactScores.ContainsKey(GUIDKey))
                    {
                        CalcLogEntry($"Node GUID {GUIDKey} has a value of: {s_assetImpactScores[GUIDKey]} ");
                        s_assetImpactScores[GUIDKey] = edgeImpactedImpactScore;
                    }
                    else
                    {
                        CalcLogEntry($"GUIDKey not found for AssetImpactScores...");
                        s_assetImpactScores.Add(GUIDKey, edgeImpactedImpactScore);
                        CalcLogEntry($"AssetImpactScores = {edgeImpactedImpactScore} ");
                    }
                }

                if (incommingNodeType == "objective")
                {
                    //get incomming node Objective Target and Acheived values
                    double ObjectiveNodeTargetScore = GraphUtil.GetNodeObjectiveTargetValue(ParentGUID);
                    double ObjectiveNodeAcheivedScore = GraphUtil.GetNodeObjectiveAcheivedValue(ParentGUID);

                    //get the edge impact score
                    string edgeID = GraphUtil.GetEdgeBetweenNodes(ParentGUID, CurrentNodeID);
                    double edgeImpactScore = GraphUtil.GetEdgeStrengthScore(edgeID);

                    double edgeImpactedObjectiveNodeTargetScore = ObjectiveNodeTargetScore * edgeImpactScore;
                    double edgeImpactedObjectiveNodeAcheivedScore = ObjectiveNodeAcheivedScore * edgeImpactScore;

                    string key = $"{CurrentNodeID}:{ParentGUID}:objectiveTargetValue";
                    if (s_calculatedValues.ContainsKey(key))
                    {
                        CalcLogEntry($"s_calculatedValues already contains key {key}");
                        s_calculatedValues[key] = edgeImpactedObjectiveNodeTargetScore;
                        CalcLogEntry($"Updated {key}, {edgeImpactedObjectiveNodeTargetScore} in s_calculatedValues contains");
                    }
                    else
                    {
                        CalcLogEntry($"s_calculatedValues does not contain key {key}");
                        s_calculatedValues.Add(key, edgeImpactedObjectiveNodeTargetScore);
                        CalcLogEntry($"Added {key}, {edgeImpactedObjectiveNodeTargetScore} to s_calculatedValues contains");
                    }
                    // Now iterate through all and calculate the sum
                    double sumObjectiveTargetValue = 0;
                    foreach (string Id in s_calculatedValues.Keys.ToList())
                    {
                        if (Id.StartsWith(CurrentNodeID) && Id.EndsWith(":objectiveTargetValue"))  // Find Parent Nodes values for ObjectiveTargetValue 
                        {
                            sumObjectiveTargetValue += s_calculatedValues[Id];
                        }
                    }
                    CalcLogEntry($"Set objectiveTargetValue to: {sumObjectiveTargetValue}");
                    GraphUtil.SetNodeData(CurrentNodeID, "objectiveTargetValue", sumObjectiveTargetValue.ToString());
                    GraphUtil.AddToNodeScores(CurrentNodeID + ":objectiveTargetValue", sumObjectiveTargetValue);



                    key = $"{CurrentNodeID}:{ParentGUID}:objectiveAcheivedValue";
                    if (s_calculatedValues.ContainsKey(key))
                    {
                        CalcLogEntry($"s_calculatedValues already contains key {key}");
                        s_calculatedValues[key] = edgeImpactedObjectiveNodeAcheivedScore;
                        CalcLogEntry($"Updated {key}, {edgeImpactedObjectiveNodeAcheivedScore} in s_calculatedValues contains");
                    }
                    else
                    {
                        CalcLogEntry($"s_calculatedValues does not contain key {key}");
                        s_calculatedValues.Add(key, edgeImpactedObjectiveNodeAcheivedScore);
                        CalcLogEntry($"Added {key}, {edgeImpactedObjectiveNodeAcheivedScore} to s_calculatedValues contains");
                    }
                    // Now iterate through all and calculate the sum
                    double sumObjectiveAcheivedValue = 0;
                    foreach (string Id in s_calculatedValues.Keys.ToList())
                    {
                        if (Id.StartsWith(CurrentNodeID) && Id.EndsWith(":objectiveAcheivedValue"))  // Find Parent Nodes values for ObjectiveAcheivedValue 
                        {
                            sumObjectiveAcheivedValue += s_calculatedValues[Id];
                        }
                    }
                    CalcLogEntry($"Set ObjectiveAcheivedValue to: {sumObjectiveAcheivedValue}");
                    GraphUtil.SetNodeData(CurrentNodeID, "objectiveAcheivedValue", sumObjectiveAcheivedValue.ToString());
                    GraphUtil.AddToNodeScores(CurrentNodeID + ":objectiveAcheivedValue", sumObjectiveAcheivedValue);

                }
            }

            //Now add the current Node assetConfidentialityMitigatedScore to the sumEdgeImpacteSourceAssetNodeConfidentialityMitigatedScore value
            edgeImpactedParentAssetNodeConfidentialityMitigatedScore = GraphUtil.GetAssetConfidentialityMitigatedScore(CurrentNodeID) + edgeImpactedParentAssetNodeConfidentialityMitigatedScore;

            if (edgeImpactedParentAssetNodeConfidentialityMitigatedScore > 100)
                edgeImpactedParentAssetNodeConfidentialityMitigatedScore = 100;

            CalcLogEntry($"Set assetConfidentialityMitigatedScore to: {edgeImpactedParentAssetNodeConfidentialityMitigatedScore}");
            GraphUtil.SetNodeData(CurrentNodeID, "assetConfidentialityMitigatedScore", edgeImpactedParentAssetNodeConfidentialityMitigatedScore.ToString());
            GraphUtil.AddToNodeScores(CurrentNodeID + ":assetConfidentialityMitigatedScore", edgeImpactedParentAssetNodeConfidentialityMitigatedScore);
            GraphUtil.AddToDistributionData(CurrentNodeID + ":assetConfidentialityMitigatedScore", (int)edgeImpactedParentAssetNodeConfidentialityMitigatedScore);


            //Now add the current Node assetIntegerityMitigatedScore to the edgeImpactedParentAssetNodeIntegrityMitigatedScore value
            edgeImpactedParentAssetNodeIntegrityMitigatedScore = GraphUtil.GetAssetNodeIntegrityMitigatedScore(CurrentNodeID) + edgeImpactedParentAssetNodeIntegrityMitigatedScore;

            if (edgeImpactedParentAssetNodeIntegrityMitigatedScore > 100)
                edgeImpactedParentAssetNodeIntegrityMitigatedScore = 100;

            CalcLogEntry($"Set assetIntegrityMitigatedScore to: {edgeImpactedParentAssetNodeIntegrityMitigatedScore}");
            GraphUtil.SetNodeData(CurrentNodeID, "assetIntegrityMitigatedScore", edgeImpactedParentAssetNodeIntegrityMitigatedScore.ToString());
            GraphUtil.AddToNodeScores(CurrentNodeID + ":assetIntegrityMitigatedScore", edgeImpactedParentAssetNodeIntegrityMitigatedScore);
            GraphUtil.AddToDistributionData(CurrentNodeID + ":assetIntegrityMitigatedScore", (int)edgeImpactedParentAssetNodeIntegrityMitigatedScore);


            //Now add the current Node assetIntegerityMitigatedScore to the edgeImpactedParentAssetNodeAvailabilityMitigatedScore value
            edgeImpactedParentAssetNodeAvailabilityMitigatedScore = GraphUtil.GetAssetNodeAvailabilityMitigatedScore(CurrentNodeID) + edgeImpactedParentAssetNodeAvailabilityMitigatedScore;

            if (edgeImpactedParentAssetNodeAvailabilityMitigatedScore > 100)
                edgeImpactedParentAssetNodeAvailabilityMitigatedScore = 100;

            CalcLogEntry($"Set assetAvailabilityMitigatedScore to: {edgeImpactedParentAssetNodeAvailabilityMitigatedScore}");
            GraphUtil.SetNodeData(CurrentNodeID, "assetAvailabilityMitigatedScore", edgeImpactedParentAssetNodeAvailabilityMitigatedScore.ToString());
            GraphUtil.AddToNodeScores(CurrentNodeID + ":assetAvailabilityMitigatedScore", edgeImpactedParentAssetNodeAvailabilityMitigatedScore);
            GraphUtil.AddToDistributionData(CurrentNodeID + ":assetAvailabilityMitigatedScore", (int)edgeImpactedParentAssetNodeAvailabilityMitigatedScore);


            //Now add the current Node assetIntegerityMitigatedScore to the edgeImpactedParentAssetNodeAccountabilityMitigatedScore value
            edgeImpactedParentAssetNodeAccountabilityMitigatedScore = GraphUtil.GetAssetNodeAccountabilityMitigatedScore(CurrentNodeID) + edgeImpactedParentAssetNodeAccountabilityMitigatedScore;

            if (edgeImpactedParentAssetNodeAccountabilityMitigatedScore > 100)
                edgeImpactedParentAssetNodeAccountabilityMitigatedScore = 100;

            CalcLogEntry($"Set assetAccountabilityMitigatedScore to: {edgeImpactedParentAssetNodeAccountabilityMitigatedScore}");
            GraphUtil.SetNodeData(CurrentNodeID, "assetAccountabilityMitigatedScore", edgeImpactedParentAssetNodeAccountabilityMitigatedScore.ToString());
            GraphUtil.AddToNodeScores(CurrentNodeID + ":assetAccountabilityMitigatedScore", edgeImpactedParentAssetNodeAccountabilityMitigatedScore);
            GraphUtil.AddToDistributionData(CurrentNodeID + ":assetAccountabilityMitigatedScore", (int)edgeImpactedParentAssetNodeAccountabilityMitigatedScore);
          
        }

        public static IEnumerable<string> SortByLength(IEnumerable<string> e)
        {
            // Use LINQ to sort the array received and return a copy.
            var sorted = from s in e
                         orderby s.Length descending
                         select s;
            return sorted;
        }

        public static void CalculateIndividualRiskPaths()
        {
            //Calculate Graph
            //Build a lists of all Actor and Asset Nodes 
            JArray actorNodes = new JArray();
            JArray assetNodes = new JArray();
            JArray nodesInPath = new JArray();
            JArray paths = new JArray();
            actorNodes = GraphUtil.GetNodesByType("actor");
            assetNodes = GraphUtil.GetNodesByType("asset");

            //Get all the Nodes between the Actor and Assets
            if (actorNodes == null && assetNodes == null)
                return;

            foreach (string actorID in actorNodes)
            {
                foreach (string assetID in assetNodes)
                {
                    nodesInPath.Merge(GraphUtil.GetAllNodesInPath(actorID, assetID));
                }
            }

            _IndividualRiskPaths.Clear();

            double actorMitigatedScore = 0;
            double attackMitigatedScore = 0;
            double vulnerabilityMitigatedScore = 0;
            double assetMitigatedScore = 0;
            double threatScore = 0;
            double likelihoodScore = 0;
            double impactScore = 0;

            string actorGUID = "";
            string attackGUID = "";
            string assetGUID = "";
            string vulnerabilityGUID = "";
            string previousNodeType = "";
            string previousNodeGUID = "";
            string parentAssetGUID = "";

            List<string> nodesOnPath = new List<string>();


            int indexnodesInPath = 0;
            foreach (var path in nodesInPath)
            {
                foreach (string nodeGUID in path)
                {
                    string nodeType = GraphUtil.GetNodeType(nodeGUID.ToString());

                    if (!string.IsNullOrEmpty(nodeType))
                    {
                        if (nodeType.ToLower() == "actor")
                        {
                            actorGUID = nodeGUID;
                            actorMitigatedScore = Math.Round(GraphUtil.GetActorNodeMitigatedScore(nodeGUID));
                        }

                        if (nodeType.ToLower() == "attack")
                        {
                            attackGUID = nodeGUID;
                            attackMitigatedScore = Math.Round(GraphUtil.GetAttackNodeMitigatedScore(nodeGUID));
                            if (previousNodeType == "actor")
                                threatScore = GraphUtil.GetSpecificThreatScore(actorGUID, attackGUID);
                            if (previousNodeType == "vulnerability")
                                threatScore = GraphUtil.GetSpecificThreatScore(vulnerabilityGUID, attackGUID);
                            if (previousNodeType == "asset")
                                threatScore = GraphUtil.GetAssetThreatScore(assetGUID, attackGUID);
                        }

                        if (nodeType.ToLower() == "vulnerability")
                        {
                            vulnerabilityGUID = nodeGUID;
                            vulnerabilityMitigatedScore = GraphUtil.GetVulnerabilityNodeMitigatedScore(nodeGUID);
                            likelihoodScore = GraphUtil.GetSpecificLikelihoodScore(threatScore, attackGUID, vulnerabilityGUID);
                        }

                        if (nodeType.ToLower() == "asset")
                        {
                            if (previousNodeType.ToLower() != "asset")
                            {
                                assetGUID = nodeGUID;
                                assetMitigatedScore = GraphUtil.GetAssetNodeMitigatedScore(nodeGUID);
                                impactScore = GraphUtil.GetSpecificImpactScore(likelihoodScore, vulnerabilityGUID, assetGUID);
                            }
                            else
                            {
                                // Both this Node and the Previous nodes were assets, this means they much be chained assets
                                // Just get the values from the previous Asset Node, and take account of the Edge impact
                                parentAssetGUID = assetGUID;
                                assetGUID = nodeGUID;
                                assetMitigatedScore = GraphUtil.GetAssetNodeMitigatedScore(nodeGUID);
                                impactScore = GraphUtil.GetSpecificImpactScore(likelihoodScore, parentAssetGUID, assetGUID);

                            }

                            if (nodeGUID == path[path.Count() - 1].ToString())  //Check this is the last node in the path
                            {
                                graphResultsList tempResultsList = new graphResultsList();
                                tempResultsList.ActorGUID = actorGUID;
                                tempResultsList.ActorTitle = GraphUtil.GetNodeTitle(actorGUID);
                                tempResultsList.ActorMitigatedScore = actorMitigatedScore;
                                tempResultsList.AttackGUID = attackGUID;
                                tempResultsList.AttackTitle = GraphUtil.GetNodeTitle(attackGUID);
                                tempResultsList.AttackMitigatedScore = attackMitigatedScore;
                                tempResultsList.VulnerabilityGUID = vulnerabilityGUID;
                                tempResultsList.VulnerabilityTitle = GraphUtil.GetNodeTitle(vulnerabilityGUID);
                                tempResultsList.VulnerabilityMitigatedScore = vulnerabilityMitigatedScore;
                                tempResultsList.AssetGUID = assetGUID;
                                tempResultsList.AssetTitle = GraphUtil.GetNodeTitle(assetGUID);
                                tempResultsList.AssetMitigatedScore = assetMitigatedScore;
                                tempResultsList.ThreatScore = threatScore;
                                tempResultsList.LikelihoodScore = likelihoodScore;
                                tempResultsList.ImpactScore = impactScore;
                                tempResultsList.FullNodeEdgePath = nodesInPath[indexnodesInPath].ToString();
                                tempResultsList.PathGUID = System.Guid.NewGuid().ToString(); 
                                tempResultsList.RiskStatus = GraphUtil.GetRiskStatusFromValue(tempResultsList.ImpactScore);
                                tempResultsList.RiskColor = GraphUtil.GetRiskColorFromValue(tempResultsList.ImpactScore);
                                tempResultsList.RiskStatement = BuildRiskStatment(tempResultsList.AssetTitle,
                                                                                  tempResultsList.VulnerabilityTitle,
                                                                                  tempResultsList.ActorTitle,
                                                                                  tempResultsList.AttackTitle,
                                                                                  tempResultsList.ImpactScore);
                                _IndividualRiskPaths.Add(tempResultsList);
                                tempResultsList = null;
                            }


                        }
                        previousNodeType = nodeType;
                        previousNodeGUID = nodeGUID;

                    }
                }
                indexnodesInPath++;
            }
        }

        

        static string BuildRiskStatment(string assetName, string vulnrabilityName, string actorName, string AttackName, double RiskValue)
        {
            string riskStatement = "";
            string riskStatus = GraphUtil.GetRiskStatusFromValue(RiskValue);

            // Example Text: There is a CRITICAL risk to {assetName}. This risk comes from a {vulnrabilityName} which allows {actorName} to undertake an {AttackName}  
            riskStatement = $"There is a {riskStatus} risk to {assetName}. This risk comes from a {vulnrabilityName} which allows {actorName} to undertake an {AttackName}";
            return riskStatement;
        }

    }
}
