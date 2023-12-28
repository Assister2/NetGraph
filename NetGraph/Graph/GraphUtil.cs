using CefSharp;
using CefSharp.DevTools.CSS;
using CefSharp.WinForms;
using EnvDTE;
using CyConex.API;
using CyConex.Chromium;
using CyConex.Helpers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Syncfusion.Schedule;
using Syncfusion.Windows.Forms.Chart;
using Syncfusion.Windows.Forms.Diagram;
using Syncfusion.Windows.Forms.Tools;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms.DataVisualization.Charting;
using Troschuetz.Random;
using static CyConex.RangeSelectForm;
using static Syncfusion.WinForms.Core.NativeTouch;
using CefSharp.DevTools.DOM;
using CefSharp.Web;
using System.Collections;
using Microsoft.Identity.Client;
using System.Text;
using static Syncfusion.Windows.Forms.Tools.CheckedChangedEventArgs;

namespace CyConex.Graph
{
    public static class GraphUtil
    {
        private static List<LocalNode> s_allNodes = new List<LocalNode>();
        private static List<LocalEdge> s_allEdges = new List<LocalEdge>();
        private static List<LocalRelationship> s_allRelationships = new List<LocalRelationship>();
        public static Dictionary<string, object> probabilityDistributions = new Dictionary<string, object>();
        public static Dictionary<string, double> nodeScores = new Dictionary<string, double>();
        private static Dictionary<string, int> RiskBucket = new Dictionary<string, int>();
        private static bool bulkUpdate = false;
        private static ChromiumWebBrowser _browser;

        static bool alreadyRunAsset = false;
        static bool alreadyRunAttack = false;
        static bool alreadyRunActor = false;
        static bool alreadyRunVulnerability = false;

        public static int calcIterations = 0;

        static Random random = new Random();

        public static void InitializeValues()
        {
           
            // Reset variables, lists etc
            s_allNodes.Clear();
            s_allEdges.Clear();
            s_allRelationships.Clear();
            GraphCalcs.graphCalcInProgress = false;
        }

        public static void setBrowser(ChromiumWebBrowser br)
        {
            _browser = br;
        }

        public static ChromiumWebBrowser getBrowser()
        {
            return _browser;
        }

        public static void AddNode(LocalNode node)
        {
            s_allNodes.Add(node);
        }

        public static void AddEdge(LocalEdge edge)
        {
            s_allEdges.Add(edge);
        }

        public static void AddRelationship(LocalRelationship relationship)
        {
            s_allRelationships.Add(relationship);
        }

        public static void CreateAssetGroupEdges()
        {
            // Creates Edges from Assets to Asset Group

            //Get a list of all asset-group nodes
            List<string> assetGroups = new List<string>();
            assetGroups = GetAllAssetGroups();

            // Create a list of all Asset Nodes 
            List<string> assetNodes = new List<string>();
            assetNodes = GetAssetNodes();

            // Check each Asset Node to see if it has a parent
            foreach (string assetNode in assetNodes)
            {
                // Search for Nodes which have a parent
                var nodeWithSpecifiedID = s_allNodes.FirstOrDefault(node => node.nodeID == assetNode && node.parent != "");
                if (nodeWithSpecifiedID != null)
                {
                    // This Asset Node has a parent group node
                    //Check if an Edge already exisits, if not, create edge
                    if (!CheckEdgeExists(nodeWithSpecifiedID.nodeID, nodeWithSpecifiedID.parent))
                    {
                        //Create a edge from the Asset group to the Asset
                        nodeWithSpecifiedID.nodeID = assetNode;

                        LocalEdge edge = new LocalEdge();
                        edge.edgeID = Guid.NewGuid().ToString();
                        edge.edgeStrengthMinValue = "1.00";
                        edge.edgeStrengthValue = "1.00";
                        edge.edgeStrengthDistribution = "[\"SpecificValue\",\"1.00\",null,null,\"edge\"]";
                        edge.enabled = true;
                        List<Tuple<double, double, double, Color>> EdgeStrengths = new List<Tuple<double, double, double, Color>>();

                        //Add the Edge to Edges table
                        AddEdge(edge);

                        //Add the Edge to Relationships table
                        LocalRelationship localRelationship = new LocalRelationship();
                        localRelationship.edgeID = edge.edgeID;
                        localRelationship.sourceNodeID = nodeWithSpecifiedID.nodeID;
                        localRelationship.targetNodeID = nodeWithSpecifiedID.parent;

                        AddRelationship(localRelationship);
                    }
                }

            }

        }


        public static List<string> GetNodes()
        {
            List<string> nodes = new List<string>();
            for (int i = 0; i < s_allNodes.Count; i++)
            {
                nodes.Add(s_allNodes[i].nodeID);
            }
            return nodes;
        }

        public static List<string> GetAllAssetGroups()
        {
            List<string> assets = new List<string>();
            foreach (LocalNode localNode in s_allNodes)
            {
                if (localNode.nodeType.ToLower() == "asset-group")
                {
                    assets.Add(localNode.nodeID);
                }
            }
            return assets;

        }


        public static List<string> GetAllParentGroupNodes()
        {
            List<string> nodes = new List<string>();
            for (int i = 0; i < s_allNodes.Count; i++)
            {
                if (s_allNodes[i].parent != null)
                    if (nodes.IndexOf(s_allNodes[i].parent) == -1)
                        nodes.Add(s_allNodes[i].nodeID);
            }
            return nodes;
        }

        public static List<string> GetParentGroupNodes(string node_id)
        {
            List<string> nodes = new List<string>();
            for (int i = 0; i < s_allNodes.Count; i++)
            {
                if (s_allNodes[i].nodeID == node_id)
                {
                    nodes.Add(node_id);
                }
                if (s_allNodes[i].parent != null)
                    if (nodes.IndexOf(s_allNodes[i].parent) == -1)
                        nodes.Add(s_allNodes[i].nodeID);
            }
            return nodes;
        }

        public static List<string> GetAllAncestorNodes(string node_id)
        {
            Queue<string> nodeQueue = new Queue<string>();
            HashSet<string> visitedNodes = new HashSet<string>();

            List<string> initialNodes = GetParentNodes(node_id);
            foreach (string node in initialNodes)
            {
                nodeQueue.Enqueue(node);
                visitedNodes.Add(node);
            }

            while (nodeQueue.Count > 0)
            {
                string currentNode = nodeQueue.Dequeue();
                List<string> parentNodes = GetParentNodes(currentNode);

                foreach (string parentNode in parentNodes)
                {
                    if (!visitedNodes.Contains(parentNode))
                    {
                        nodeQueue.Enqueue(parentNode);
                        visitedNodes.Add(parentNode);
                    }
                }
            }

            return visitedNodes.ToList();
        }

        public static int CountOfAllAncestorNodes(string node_id)
        {
            Queue<string> nodeQueue = new Queue<string>();
            HashSet<string> visitedNodes = new HashSet<string>();

            List<string> initialNodes = GetParentNodes(node_id);
            foreach (string node in initialNodes)
            {
                nodeQueue.Enqueue(node);
                visitedNodes.Add(node);
            }

            while (nodeQueue.Count > 0)
            {
                string currentNode = nodeQueue.Dequeue();
                List<string> parentNodes = GetParentNodes(currentNode);

                foreach (string parentNode in parentNodes)
                {
                    if (!visitedNodes.Contains(parentNode))
                    {
                        nodeQueue.Enqueue(parentNode);
                        visitedNodes.Add(parentNode);
                    }
                }
            }

            return visitedNodes.Count;
        }


        public static List<string> GetAllUpstreamNodesbyType(string node_id, string node_Type)
        {
            List<string> nodes = new List<string>();
            List<string> tempNodes = new List<string>();
            tempNodes = GetParentNodes(node_id);
            for (int i = 0; i < tempNodes.Count; i++)
            {
                tempNodes.AddRange(GetParentNodes(tempNodes[i]));
                if (GetNodeType(tempNodes[i]) == node_Type.ToLower())
                    nodes.Add(tempNodes[i]);
            }
            return nodes;
        }


        public static List<string> GetChildNodesofParentGroup(string node_id)
        {
            List<string> nodes = new List<string>();
            for (int i = 0; i < s_allNodes.Count; i++)
            {
                if (s_allNodes[i].parent == node_id)
                    if (nodes.IndexOf(s_allNodes[i].parent) == -1)
                        nodes.Add(s_allNodes[i].nodeID);
            }
            return nodes;
        }

        public static bool IsAssetGroup(string node_id)
        {
            bool IsParent = false;
            for (int i = 0; i < s_allNodes.Count; i++)
            {
                if (s_allNodes[i].parent == node_id)
                {
                    IsParent = true;
                    break;
                }

            }
            return IsParent;
        }

        public static bool IsChildOfParentNode(string node_id)
        {
            var foundNode = s_allNodes.FirstOrDefault(node => node.nodeID == node_id);
            return foundNode != null && foundNode.parent != null && foundNode.parent != "";
        }


        public static bool IsParentofChildNode(string node_id)
        {
            bool IsParent = false;

            //Check for asset group
            for (int i = 0; i < s_allNodes.Count; i++)
            {
                if (s_allNodes[i].parent == node_id)
                {
                    IsParent = true;
                    break;
                }

            }

            //Check for linked assets
            for (int i = 0; i < s_allRelationships.Count; i++)
            {
                if (s_allRelationships[i].targetNodeID == node_id)
                {
                    if (GetNodeType(s_allRelationships[i].sourceNodeID) == "asset")
                    {
                        IsParent = true;
                        break;
                    }
                }
            }
            return IsParent;
        }

        public static string GetParentGroupNode(string node_id)
        {
            string Parent_id = "";
            for (int i = 0; i < s_allNodes.Count; i++)
            {
                if (s_allNodes[i].nodeID == node_id)
                {
                    Parent_id = s_allNodes[i].parent;
                    break;
                }

            }
            return Parent_id;
        }

        public static List<string> GetParentControlNodes(string node_ID)
        {
            List<string> sources = new List<string>();
            for (int i = 0; i < s_allRelationships.Count; i++)
            {
                if (s_allRelationships[i].targetNodeID == node_ID)
                {
                    if (GetNodeType(s_allRelationships[i].sourceNodeID) == "control")
                        sources.Add(s_allRelationships[i].sourceNodeID);
                }
            }
            return sources;
        }

        public static List<string> GetParentControlAndObjectiveNodes(string node_ID)
        {
            List<string> sources = new List<string>();
            for (int i = 0; i < s_allRelationships.Count; i++)
            {
                if (s_allRelationships[i].targetNodeID == node_ID)
                {
                    string nodeType = GetNodeType(s_allRelationships[i].sourceNodeID);
                    if (nodeType == "control" || nodeType == "objective")
                        sources.Add(s_allRelationships[i].sourceNodeID);
                }
            }
            return sources;
        }

        public static List<string> GetParentObjectivelNodes(string node_ID)
        {
            List<string> sources = new List<string>();
            for (int i = 0; i < s_allRelationships.Count; i++)
            {
                if (s_allRelationships[i].targetNodeID == node_ID)
                {
                    if (GetNodeType(s_allRelationships[i].sourceNodeID) == "objective")
                        sources.Add(s_allRelationships[i].sourceNodeID);
                }
            }
            return sources;
        }

        public static int GetParentControlNodesCount(string node_ID)
        {
            int value = 0;
            for (int i = 0; i < s_allRelationships.Count; i++)
            {
                if (s_allRelationships[i].targetNodeID == node_ID)
                {
                    if (GetNodeType(s_allRelationships[i].sourceNodeID) == "control")
                        value++;
                }
            }
            return value;
        }

        public static int GetParentObjectiveNodesCount(string node_ID)
        {
            int value = 0;
            for (int i = 0; i < s_allRelationships.Count; i++)
            {
                if (s_allRelationships[i].targetNodeID == node_ID)
                {
                    if (GetNodeType(s_allRelationships[i].sourceNodeID) == "objective")
                        value++;
                }
            }
            return value;
        }

        public static List<string> GetParentVulnerabilityNodes(string node_ID)
        {
            List<string> sources = new List<string>();
            for (int i = 0; i < s_allRelationships.Count; i++)
            {
                if (s_allRelationships[i].targetNodeID == node_ID)
                {
                    if (GetNodeType(s_allRelationships[i].sourceNodeID) == "vulnerability")
                        sources.Add(s_allRelationships[i].sourceNodeID);
                }
            }
            return sources;
        }

        public static List<string> GetParentAssetNodes(string node_ID)
        {
            List<string> sources = new List<string>();
            for (int i = 0; i < s_allRelationships.Count; i++)
            {
                if (s_allRelationships[i].targetNodeID == node_ID)
                {
                    if (GetNodeType(s_allRelationships[i].sourceNodeID) == "asset")
                        sources.Add(s_allRelationships[i].sourceNodeID);
                }
            }
            return sources;
        }

        public static List<string> GetParentObjectiveNodes(string node_ID)
        {
            List<string> sources = new List<string>();
            for (int i = 0; i < s_allRelationships.Count; i++)
            {
                if (s_allRelationships[i].targetNodeID == node_ID)
                {
                    if (GetNodeType(s_allRelationships[i].sourceNodeID) == "objective")
                        sources.Add(s_allRelationships[i].sourceNodeID);
                }
            }
            return sources;
        }



        public static bool HasParentAssetNodes(string node_ID)
        {
            for (int i = 0; i < s_allRelationships.Count; i++)
            {
                if (s_allRelationships[i].targetNodeID == node_ID)
                {
                    if (GetNodeType(s_allRelationships[i].sourceNodeID) == "asset")
                        return true;
                }
            }
            return false;
        }

        public static List<string> GetChildObjectiveNodes(string node_ID)
        {
            List<string> sources = new List<string>();
            for (int i = 0; i < s_allRelationships.Count; i++)
            {
                if (s_allRelationships[i].sourceNodeID == node_ID)
                {
                    if (GetNodeType(s_allRelationships[i].targetNodeID) == "objective")
                        sources.Add(s_allRelationships[i].targetNodeID);
                }
            }
            return sources;
        }

        public static List<string> GetChildAssetNodes(string node_ID)
        {
            List<string> sources = new List<string>();
            for (int i = 0; i < s_allRelationships.Count; i++)
            {
                if (s_allRelationships[i].sourceNodeID == node_ID)
                {
                    if (GetNodeType(s_allRelationships[i].targetNodeID) == "asset")
                        sources.Add(s_allRelationships[i].targetNodeID);
                }
            }
            return sources;
        }

        public static List<string> GetChildAssetGroupNodes(string node_ID)
        {
            List<string> sources = new List<string>();
            for (int i = 0; i < s_allRelationships.Count; i++)
            {
                if (s_allRelationships[i].sourceNodeID == node_ID)
                {
                    if (GetNodeType(s_allRelationships[i].targetNodeID) == "asset-group")
                        sources.Add(s_allRelationships[i].targetNodeID);
                }
            }
            return sources;
        }

        public static List<string> GetAllChildNodes()
        {
            List<string> targets = new List<string>();
            foreach (LocalRelationship localRelationship in s_allRelationships)
            {
                targets.Add(localRelationship.targetNodeID);
            }
            return targets;
        }

        public static List<string> GetAllNodes()
        {
            List<string> nodes = new List<string>();
            foreach (LocalNode localNode in s_allNodes)
                nodes.Add(localNode.nodeID);

            return nodes;
        }

        public static List<string> GetSourceAllNodes()
        {
            List<string> sources = new List<string>();
            foreach (LocalRelationship localRelationship in s_allRelationships)
            {
                sources.Add(localRelationship.sourceNodeID);
            }
            return sources;
        }

        public static List<string> GetParentNodes(string node_ID)
        {
            List<string> sources = new List<string>();
            for (int i = 0; i < s_allRelationships.Count; i++)
            {
                if (s_allRelationships[i].targetNodeID == node_ID)
                    sources.Add(s_allRelationships[i].sourceNodeID);
            }
            return sources;
        }

        public static List<string> GetRootIDs()
        {
            List<string> rootIDs = new List<string>();
            List<string> nodeIDs = GetNodes();
            List<string> targets = GetAllChildNodes();
            foreach (string id in nodeIDs)
            {
                if (targets.IndexOf(id) == -1)
                {
                    rootIDs.Add(id);
                }
            }
            return rootIDs;
        }

        public static List<string> GetAssetNodes()
        {
            List<string> assets = new List<string>();
            foreach (LocalNode localNode in s_allNodes)
            {
                if (localNode.nodeType.ToLower() == "asset")
                {
                    assets.Add(localNode.nodeID);
                }
            }
            return assets;
        }

        public static List<string> GetAssetAndAssetGroupNodes()
        {
            List<string> assets = new List<string>();
            foreach (LocalNode localNode in s_allNodes)
            {
                string tempType = localNode.nodeType.ToLower();
                if (tempType == "asset" || tempType == "asset-group")
                {
                    assets.Add(localNode.nodeID);
                }
            }
            return assets;
        }

        public static List<string> GetAssetGroupNodes()
        {
            List<string> assets = new List<string>();
            foreach (LocalNode localNode in s_allNodes)
            {
                if (localNode.nodeType.ToLower() == "asset-group")
                {
                    assets.Add(localNode.nodeID);
                }
            }
            return assets;
        }

        public static int GetAssetNodeCount()
        {
            int i = 0;
            foreach (LocalNode localNode in s_allNodes)
            {
                if (localNode.nodeType.ToLower() == "asset")
                    i++;
            }
            return i;
        }
        public static int GetAssetGroupNodeCount()
        {
            int i = 0;
            foreach (LocalNode localNode in s_allNodes)
            {
                if (localNode.nodeType.ToLower() == "asset-group")
                    i++;
            }
            return i;
        }
        public static int GetAttackNodeCount()
        {
            int i = 0;
            foreach (LocalNode localNode in s_allNodes)
            {
                if (localNode.nodeType.ToLower() == "attack")
                    i++;
            }
            return i;
        }
        public static int GetActorNodeCount()
        {
            int i = 0;
            foreach (LocalNode localNode in s_allNodes)
            {
                if (localNode.nodeType.ToLower() == "actor")
                    i++;
            }
            return i;
        }
        public static int GetVulnerabilityNodeCount()
        {
            int i = 0;
            foreach (LocalNode localNode in s_allNodes)
            {
                if (localNode.nodeType.ToLower() == "vulnerability")
                    i++;
            }
            return i;
        }
        public static int GetControlNodeCount()
        {
            int i = 0;
            foreach (LocalNode localNode in s_allNodes)
            {
                if (localNode.nodeType.ToLower() == "control")
                    i++;
            }
            return i;
        }
        public static int GetObjectiveNodeCount()
        {
            int i = 0;
            foreach (LocalNode localNode in s_allNodes)
            {
                if (localNode.nodeType.ToLower() == "objective")
                    i++;
            }
            return i;
        }

        public static int GetGroupNodeCount()
        {
            int i = 0;
            foreach (LocalNode localNode in s_allNodes)
            {
                if (localNode.nodeType.ToLower() == "group")
                    i++;
            }
            return i;
        }

        public static int GetEvidenceNodeCount()
        {
            int i = 0;
            foreach (LocalNode localNode in s_allNodes)
            {
                if (localNode.nodeType.ToLower() == "group")
                    i++;
            }
            return i;
        }

        public static List<string> GetObjectiveNodes()
        {
            List<string> assets = new List<string>();
            foreach (LocalNode localNode in s_allNodes)
            {
                if (localNode.nodeType.ToLower() == "objective")
                {
                    assets.Add(localNode.nodeID);
                }
            }
            return assets;
        }

        public static List<string> GetActorNodes()
        {
            List<string> assets = new List<string>();
            foreach (LocalNode localNode in s_allNodes)
            {
                if (localNode.nodeType.ToLower() == "actor")
                {
                    assets.Add(localNode.nodeID);
                }
            }
            return assets;
        }

        public static List<string> GetAttackNodes()
        {
            List<string> assets = new List<string>();
            foreach (LocalNode localNode in s_allNodes)
            {
                if (localNode.nodeType.ToLower() == "attack")
                {
                    assets.Add(localNode.nodeID);
                }
            }
            return assets;
        }

        public static List<string> GetVulnerabilityNodes()
        {
            List<string> assets = new List<string>();
            foreach (LocalNode localNode in s_allNodes)
            {
                if (localNode.nodeType.ToLower() == "vulnerability")
                {
                    assets.Add(localNode.nodeID);
                }
            }
            return assets;
        }

        public static List<string> GetEvidenceNodes()
        {
            List<string> assets = new List<string>();
            foreach (LocalNode localNode in s_allNodes)
            {
                if (localNode.nodeType.ToLower() == "evidence")
                {
                    assets.Add(localNode.nodeID);
                }
            }
            return assets;
        }

        public static List<string> GetControlNodes()
        {
            List<string> assets = new List<string>();
            foreach (LocalNode localNode in s_allNodes)
            {
                if (localNode.nodeType.ToLower() == "control")
                {
                    assets.Add(localNode.nodeID);
                }
            }
            return assets;
        }

        public static List<string> GetGroupNodes()
        {
            List<string> assets = new List<string>();
            foreach (LocalNode localNode in s_allNodes)
            {
                if (localNode.nodeType.ToLower() == "group")
                {
                    assets.Add(localNode.nodeID);
                }
            }
            return assets;
        }

        public static string GetNodeType(string node_id)
        {
            string nodeType = "notfound";
            foreach (LocalNode localNode in s_allNodes)
            {
                if (localNode.nodeID == node_id)
                {
                    nodeType = localNode.nodeType;
                    break;
                }
            }

            return nodeType.ToLower();
        }

        public static List<string> GetChildNodes(string node_id)
        {
            List<string> nodeIDs = new List<string>();
            foreach (LocalRelationship localRelationship in s_allRelationships)
            {
                if (localRelationship.sourceNodeID == node_id)
                {
                    nodeIDs.Add(localRelationship.targetNodeID);
                }
            }
            return nodeIDs;
        }

        public static List<LocalRelationship> GetChildNodesWithRelationship(string node_id)
        {
            List<LocalRelationship> nodeObjs = new List<LocalRelationship>();
            foreach (LocalRelationship localRelationship in s_allRelationships)
            {
                if (localRelationship.sourceNodeID == node_id)
                {
                    nodeObjs.Add(localRelationship);
                }
            }
            return nodeObjs;
        }

        public static List<string> GetOutgoingEdges(string node_id)
        {
            List<string> edgeIDs = new List<string>();
            foreach (LocalRelationship localRelationship in s_allRelationships)
            {
                if (localRelationship.sourceNodeID == node_id)
                {
                    edgeIDs.Add(localRelationship.edgeID);
                }
            }
            return edgeIDs;
        }

        public static bool CheckEdgeExists(string sourceNodeID, string targetNodeID)
        {
            bool edgeExisits = s_allRelationships.Any(r => r.targetNodeID == targetNodeID && r.sourceNodeID == sourceNodeID);

            if (edgeExisits)
                return true;
            else
                return false;

        }

        public static int GetNodeOutgoerEdgesCount(string node_id)
        {
            int i = 0;
            List<string> edgeIDs = new List<string>();
            foreach (LocalRelationship localRelationship in s_allRelationships)
            {
                if (localRelationship.sourceNodeID == node_id)
                {
                    i++;
                }
            }
            return i;
        }

        public static List<string> GetNodeIngoerNodes(string node_id)
        {
            List<string> nodeIDs = new List<string>();
            foreach (LocalRelationship localRelationship in s_allRelationships)
            {
                if (localRelationship.targetNodeID == node_id)
                {
                    nodeIDs.Add(localRelationship.sourceNodeID);
                }
            }
            return nodeIDs;
        }

        public static bool IsNodeEnabled(string node_id)
        {
            bool enabled = false;
            foreach (LocalNode localNode in s_allNodes)
            {
                if (localNode.nodeID == node_id)
                {
                    enabled = localNode.enabled;
                    break;
                }
            }
            return enabled;
        }

        public static bool IsRootNode(string node_id)
        {
            bool flag = false;
            List<string> targets = GetAllChildNodes();
            if (targets.IndexOf(node_id) == -1)
            {
                flag = true;
            }
            return flag;
        }

        public static double GetEdgeStrengthValue(string edge_id)
        {
            double value = 0;
            foreach (LocalEdge localEdge in s_allEdges)
            {
                if (localEdge.edgeID == edge_id)
                {
                    if (localEdge.edgeStrengthValue != null)
                        value = Double.Parse(localEdge.edgeStrengthValue);
                    break;
                }
            }
            return value;
        }

        public static decimal GetNodeEdgeStrengthScore(string edge_id)
        {
            decimal value = 0;
            foreach (LocalEdge localEdge in s_allEdges)
            {
                if (localEdge.edgeID == edge_id)
                {
                    if (localEdge.edgeStrengthScore != null)
                        value = decimal.Parse(localEdge.edgeStrengthScore);
                    break;
                }
            }
            return value;
        }

        public static int GetEdgesCount()
        {
            return s_allRelationships.Count();
        }

        public static double GetNodeEdgeStrengthMinValue(string edge_id)
        {
            double value = 0;
            foreach (LocalEdge localEdge in s_allEdges)
            {
                if (localEdge.edgeID == edge_id)
                {
                    if (localEdge.edgeStrengthMinValue != null)
                        value = Double.Parse(localEdge.edgeStrengthMinValue);
                    break;
                }
            }
            return value;
        }

        public static object GetNodeEdgeStrengthDistribution(string edge_id)
        {
            foreach (LocalEdge localEdge in s_allEdges)
            {
                if (localEdge.edgeID == edge_id)
                {
                    if (localEdge.edgeStrengthDistribution != null)
                        return localEdge.edgeStrengthDistribution;
                }
                return null;
            }
            return null;
        }

        public static double GetNodecontrolAssessedValue(string node_id)
        {
            double value = 0;
            foreach (LocalNode localNode in s_allNodes)
            {
                if (localNode.nodeID == node_id)
                {
                    if (localNode.controlAssessedValue != null)
                        value = Double.Parse(localNode.controlAssessedValue);
                    break;
                }
            }
            return value;
        }

        public static double GetControlNodeAssessedScore(string node_id)
        {
            double value = 0;
            foreach (LocalNode localNode in s_allNodes)
            {
                if (localNode.nodeID == node_id)
                {
                    if (localNode.controlAssessedScore != null)
                        value = Double.Parse(localNode.controlAssessedScore);
                    break;
                }
            }
            return value;
        }



        public static double GetControlNodeAssessedMinValue(string node_id)
        {
            double value = 0;
            foreach (LocalNode localNode in s_allNodes)
            {
                if (localNode.nodeID == node_id)
                {
                    if (localNode.controlAssessedMinValue != null)
                        value = Double.Parse(localNode.controlAssessedMinValue);
                    break;
                }
            }
            return value;
        }

        public static double GetNodeCalculatedValue(string node_id)
        {
            double value = 0;
            foreach (LocalNode localNode in s_allNodes)
            {
                if (localNode.nodeID == node_id)
                {
                    if (localNode.calculatedValue != null && localNode.calculatedValue != "")
                        value = Double.Parse(localNode.calculatedValue);
                    break;
                }
            }
            return value;
        }

        public static double GetNodeBaseScore(string node_id)
        {
            foreach (LocalNode localNode in s_allNodes)
            {
                if (localNode.nodeID == node_id)
                {
                    if (localNode.controlBaseScore != null)
                        return Double.Parse(localNode.controlBaseScore);
                    else
                        return 0;
                }
            }
            return 0;
        }

        public static double GetNodeBaseValue(string node_id)
        {
            foreach (LocalNode localNode in s_allNodes)
            {
                if (localNode.nodeID == node_id)
                {
                    if (localNode.controlBaseValue != null)
                        return Double.Parse(localNode.controlBaseValue);
                    else
                        return 0;
                }
            }
            return 0;
        }

        public static double GetNodeBaseMinValue(string node_id)
        {
            foreach (LocalNode localNode in s_allNodes)
            {
                if (localNode.nodeID == node_id)
                {
                    if (localNode.controlBaseMinValue != null)
                        return Double.Parse(localNode.controlBaseMinValue);
                    else
                        return 0;
                }
            }
            return 0;
        }

        public static double GetEdgeImpactedValue(string edge_id)
        {
            double value = 0;
            foreach (LocalEdge localEdge in s_allEdges)
            {
                if (localEdge.edgeID == edge_id)
                {
                    if (localEdge.impactedValue != null)
                        value = Double.Parse(localEdge.impactedValue);
                    break;
                }
            }
            return value;
        }

        public static double GetPreviousNodeAssessedValue(string node_id)
        {
            double value = 0;
            foreach (LocalNode localNode in s_allNodes)
            {
                if (localNode.nodeID == node_id)
                {
                    if (localNode.previousControlAssessedValue != null)
                        value = Double.Parse(localNode.previousControlAssessedValue);
                    break;
                }
            }
            return value;
        }

        public static double GetPreviousNodeCalculatedValue(string node_id)
        {
            double value = 0;
            foreach (LocalNode localNode in s_allNodes)
            {
                if (localNode.nodeID == node_id)
                {
                    if (localNode.previousCalculatedValue != null)
                        value = Double.Parse(localNode.previousCalculatedValue);
                    break;
                }
            }
            return value;
        }

        public static string GetNodeObjectiveTargetType(string node_id)
        {
            string value = "N/A";
            foreach (LocalNode localNode in s_allNodes)
            {
                if (localNode.nodeID == node_id)
                {
                    value = localNode.objectiveTargetType == null ? "N/A" : localNode.objectiveTargetType;
                    break;
                }
            }
            return value;
        }

        public static double GetNodeObjectiveTargetValue(string node_id)
        {
            foreach (LocalNode localNode in s_allNodes)
            {
                if (localNode.nodeID == node_id)
                {
                    if (localNode.objectiveTargetValue != null && localNode.objectiveTargetValue != "")
                        return Double.Parse(localNode.objectiveTargetValue);
                    else
                        return 0;
                }
            }
            return 0;
        }

        public static double GetNodeObjectiveAcheivedValue(string node_id)
        {
            foreach (LocalNode localNode in s_allNodes)
            {
                if (localNode.nodeID == node_id)
                {
                    if (localNode.objectiveAcheivedValue != null && localNode.objectiveAcheivedValue != "")
                        return Double.Parse(localNode.objectiveAcheivedValue);
                    else
                        return 0;
                }
            }
            return 0;
        }



        public static double GetEdgeStrengthScore(string edge_id)
        {
            double value = 0;
            foreach (LocalEdge localEdge in s_allEdges)
            {
                if (localEdge.edgeID == edge_id)
                {
                    if (localEdge.edgeStrengthScore != null)
                        value = Double.Parse(localEdge.edgeStrengthScore);
                    break;
                }
            }
            return value;
        }

        public static double GetPreviousNodeControlBaseScore(string node_id)
        {
            double value = 0;
            foreach (LocalNode localNode in s_allNodes)
            {
                if (localNode.nodeID == node_id)
                {
                    if (localNode.previouscontrolBaseScore != null)
                        value = Double.Parse(localNode.previouscontrolBaseScore);
                    break;
                }
            }
            return value;
        }

        public static double GetPreviousNodeControlAssessedScore(string node_id)
        {
            double value = 0;
            foreach (LocalNode localNode in s_allNodes)
            {
                if (localNode.nodeID == node_id)
                {
                    if (localNode.previouscontrolBaseScore != null)
                        value = Double.Parse(localNode.previouscontrolBaseScore);
                    break;
                }
            }
            return value;
        }

        public static bool IsEdge(string edge_id)
        {
            bool value = false;
            foreach (LocalEdge localEdge in s_allEdges)
            {
                if (localEdge.edgeID == edge_id)
                {
                    value = true;
                    break;
                }
            }
            return value;
        }

        public static bool IsEdgeEnabled(string edge_id)
        {
            bool value = false;
            foreach (LocalEdge localEdge in s_allEdges)
            {
                if (localEdge.edgeID == edge_id)
                {
                    value = localEdge.enabled;
                    break;
                }
            }
            return value;
        }

        public static string GetEdgeBetweenNodes(string sourceNodeID, string targetNodeID)
        {
            string edgeID = "";
            foreach (LocalRelationship localRelationship in s_allRelationships)
            {
                if (localRelationship.sourceNodeID == sourceNodeID && localRelationship.targetNodeID == targetNodeID)
                {
                    edgeID = localRelationship.edgeID;
                    break;
                }
            }
            return edgeID;
        }



        public static string GetEdgeSourceNode(string edge_id)
        {
            string value = "";
            foreach (LocalRelationship localRelationship in s_allRelationships)
            {
                if (localRelationship.edgeID == edge_id)
                {
                    value = localRelationship.sourceNodeID;
                    break;
                }
            }
            return value;
        }

        public static string GetEdgeTargetNode(string edge_id)
        {
            string value = "";
            foreach (LocalRelationship localRelationship in s_allRelationships)
            {
                if (localRelationship.edgeID == edge_id)
                {
                    value = localRelationship.targetNodeID;
                    break;
                }
            }
            return value;
        }

        public static int GetNodeOutgoerCount(string node_id)
        {
            int value = 0;
            foreach (LocalRelationship localRelationship in s_allRelationships)
            {
                if (localRelationship.sourceNodeID == node_id)
                {
                    value++;
                }
            }
            return value;
        }

        public static JArray GetAllPathEdges(string source_id, string target_id)
        {
            if (source_id == target_id)
            {
                return null;
            }

            JArray result = new JArray();
            List<string> proceed = new List<string>();
            JArray tmp_path = new JArray();
            proceed = GetOutgoingEdges(source_id);
            if (proceed.Count > 0)
            {
                foreach (string edge in proceed)
                {
                    JArray tmp_edge = new JArray(edge);
                    tmp_path.Add(tmp_edge);
                }

                while (tmp_path.Count > 0)
                {
                    JArray path_item = (JArray)tmp_path.Last();
                    string last_edge = path_item.Last().ToString();
                    string tmp_target_id = GetEdgeTargetNode(last_edge);
                    if (tmp_target_id == target_id)
                    {
                        result.Add(path_item);
                    }
                    else
                    {
                        List<string> todos = GetOutgoingEdges(tmp_target_id);
                        foreach (string todo_item in todos)
                        {
                            if (proceed.IndexOf(todo_item) < 0)
                            {
                                proceed.Add(todo_item);
                                JArray buf = new JArray(path_item);
                                buf.Add(todo_item);
                                tmp_path.Add(buf);
                            }
                        }
                    }
                    tmp_path.Remove(path_item);
                }
            }
            return result;
        }

        public static string GetAllNodesByTitleInPath(string source_id, string target_id)
        {
            if (source_id == target_id)
                return string.Empty;

            string NodeNames = string.Empty;

            JArray nodes = GetAllNodesInPath(source_id, target_id);
            List<string> NodeNamesList = new List<string>();

            if (nodes != null)
            {
                foreach (JToken nodeId in nodes)
                {
                    //NodeNamesList.Add(Utility.RemoveHTML(GetNodeTitle(token.ToString())));
                    List<string> guids = JsonConvert.DeserializeObject<List<string>>(nodeId.ToString());
                    foreach (string guid in guids)
                    {
                        NodeNames += Utility.RemoveHTML(GetNodeTitle(guid)) + " -> ";
                    }
                    NodeNames = NodeNames.Substring(0, NodeNames.Length - 3);
                }
                NodeNames += "\r\n";
            }
            NodeNames = NodeNames.Substring(0, NodeNames.Length - 2);


            return NodeNames;
        }


        public static JArray GetAllNodesInPath(string source_id, string target_id)
        {
            if (source_id == target_id)
            {
                return null;
            }

            JArray result = new JArray();
            List<string> proceed = new List<string>();
            JArray tmp_path = new JArray();
            proceed = GetChildNodes(source_id);
            if (proceed.Count > 0)
            {
                foreach (string node in proceed)
                {
                    JArray tmp_node = new JArray(source_id);
                    tmp_node.Add(node);
                    tmp_path.Add(tmp_node);
                }

                while (tmp_path.Count > 0)
                {
                    JArray path_item = (JArray)tmp_path.Last();
                    string last_node = path_item.Last().ToString();
                    if (last_node == target_id)
                    {
                        result.Add(path_item);
                    }
                    else
                    {
                        List<string> todos = GetChildNodes(last_node);
                        foreach (string todo_item in todos)
                        {
                            //if (proceed.IndexOf(todo_item) < 0)
                            //{
                            if (todo_item != target_id)
                            {
                                if (proceed.IndexOf(todo_item) == -1)       // 25/01/23
                                    proceed.Add(todo_item);
                                else
                                    break;
                            }

                            JArray buf = new JArray(path_item);
                            buf.Add(todo_item);
                            tmp_path.Add(buf);
                            //}
                        }
                    }
                    tmp_path.Remove(path_item);
                }
            }
            return result;
        }

        public static List<string> GetAllPathNodesAndEdges2(string source_id, string target_id)
        {
            List<string> edgesToProcess = new List<string>();
            List<string> processed = new List<string>();
            List<string> tempPath = new List<string>();
            List<string> edgePathList = new List<string>();
            var currentNodeID = "";
            var currentEdgeID = "";

            if (source_id == target_id)
            {
                return null;
            }

            currentNodeID = source_id;

            // Get first node's outgoer edges
            GetOutgoingEdges(currentNodeID).ForEach(edge =>
            {
                if (!edgesToProcess.Contains(edge))
                {
                    edgesToProcess.Add(edge);
                }
            });
            tempPath.Add(currentNodeID);
            if (edgesToProcess.Count() > 0)
            {
                do
                {

                    currentEdgeID = edgesToProcess.Last();
                    if (!processed.Contains(currentEdgeID)) // Check if Edge has already been processed
                    {
                        processed.Add(currentEdgeID);
                        tempPath.Add(currentEdgeID);
                        currentNodeID = GetEdgeTargetNode(currentEdgeID);
                        tempPath.Add(currentNodeID);

                        if (currentNodeID == target_id) //Found the target node
                        {
                            var tempString = "";
                            foreach (var item in tempPath)
                            {
                                tempString += item + ",";
                            }
                            edgePathList.Add(tempString + ";");

                        }
                        else
                        {

                            GetOutgoingEdges(currentNodeID).ForEach(edge =>
                            {
                                if (!edgesToProcess.Contains(edge))
                                {
                                    edgesToProcess.Add(edge);
                                }
                            });
                        }

                    }
                    else
                    {
                        edgesToProcess.Remove(currentEdgeID);
                        tempPath.Remove(currentEdgeID);
                        processed.Remove(currentEdgeID);
                        tempPath.Remove(GetEdgeTargetNode(currentEdgeID));
                    }

                } while (edgesToProcess.Count > 0);
            }

            return edgePathList;
        }



        public static JArray GetAllPathNodesAndEdges(string source_id, string target_id)
        {
            if (source_id == target_id)
            {
                return null;
            }

            JArray result = new JArray();
            List<string> proceed = new List<string>();
            JArray tmp_path = new JArray();
            proceed = GetOutgoingEdges(source_id);

            if (proceed.Count > 0)
            {
                foreach (string edge in proceed)
                {
                    JArray tmp_edge = new JArray(edge);
                    tmp_path.Add(tmp_edge);
                }

                while (tmp_path.Count > 0)
                {
                    JArray path_item = (JArray)tmp_path.Last();
                    string last_edge = path_item.Last().ToString();
                    string tmp_target_id = GetEdgeTargetNode(last_edge);
                    if (tmp_target_id == target_id)
                    {
                        result.Add(path_item);
                    }
                    else
                    {
                        List<string> todos = GetOutgoingEdges(tmp_target_id);
                        foreach (string todo_item in todos)
                        {
                            if (proceed.IndexOf(todo_item) < 0)
                            {
                                proceed.Add(todo_item);
                                JArray buf = new JArray();
                                buf = path_item;
                                buf.Add(todo_item);
                                tmp_path.Add(buf);
                            }
                        }
                    }
                    tmp_path.Remove(path_item);
                }

            }

            JArray fullPaths = new JArray();
            foreach (JArray jArray in result)
            {
                string tmp_full_path = source_id;
                foreach (string edge in jArray)
                {
                    tmp_full_path += "," + edge;
                    tmp_full_path += "," + GetTargetNodeIDFromEdge(edge);
                }
                fullPaths.Add(tmp_full_path);
            }
            return fullPaths;
        }

        public static string GetSourceNodeIDFromEdge(string edge_id)
        {
            string val = "";
            foreach (LocalRelationship rel in s_allRelationships)
            {
                if (rel.edgeID == edge_id)
                {
                    val = rel.sourceNodeID;
                    break;
                }
            }
            return val;
        }

        public static string GetTargetNodeIDFromEdge(string edge_id)
        {
            string val = "";
            foreach (LocalRelationship rel in s_allRelationships)
            {
                if (rel.edgeID == edge_id)
                {
                    val = rel.targetNodeID;
                    break;
                }
            }
            return val;
        }

        public static string CheckGraphSchema()
        {
            string value = "None";
            bool FoundAttack = false;
            bool FoundActor = false;
            bool Foundvulnerability = false;
            bool FoundAsset = false;


            foreach (LocalNode localNode in s_allNodes)
            {
                if (localNode.nodeType.ToLower() == "actor")
                    FoundActor = true;
                else if (localNode.nodeType.ToLower() == "attack")
                    FoundAttack = true;
                else if (localNode.nodeType.ToLower() == "vulnerability")
                    Foundvulnerability = true;
                else if (localNode.nodeType.ToLower() == "asset")
                    FoundAsset = true;

                if (FoundActor == true &&
                    FoundAttack == true &&
                    Foundvulnerability == true &&
                    FoundAsset == true)
                {
                    value = "OK";
                    break;
                }

            }

            if (value != "OK")
            {
                value = FoundActor.ToString() + "," +
                    FoundAttack.ToString() + "," +
                    Foundvulnerability.ToString() + "," +
                    FoundAsset.ToString() + ",";
            }

            return value;

        }

        public static double ClampValueAboveZero(double value)
        {
            if (double.IsNaN(value)) return 0;

            if (value < 0)
                return 0;
            else
                return value;
        }

        public static JArray GetAllPaths(string source_id, string target_id)
        {
            if (source_id == target_id)
            {
                return null;
            }

            JArray result = new JArray();
            List<string> proceed = new List<string>();
            JArray tmp_path = new JArray();
            proceed = GetOutgoingEdges(source_id);
            if (proceed.Count > 0)
            {
                foreach (string edge in proceed)
                {
                    JArray tmp_edge = new JArray(edge);
                    tmp_path.Add(tmp_edge);
                }

                while (tmp_path.Count > 0)
                {
                    JArray path_item = (JArray)tmp_path.Last();
                    string last_edge = path_item.Last().ToString();
                    string tmp_target_id = GetEdgeTargetNode(last_edge);
                    if (tmp_target_id == target_id)
                    {
                        result.Add(path_item);
                    }
                    else
                    {
                        List<string> todos = GetOutgoingEdges(tmp_target_id);
                        foreach (string todo_item in todos)
                        {
                            if (proceed.IndexOf(todo_item) < 0)
                            {
                                proceed.Add(todo_item);
                                JArray buf = new JArray(path_item);
                                buf.Add(todo_item);
                                tmp_path.Add(buf);
                            }
                        }
                    }
                    tmp_path.Remove(path_item);
                }
            }
            return result;
        }

        public static string GetNodeTitle(string node_id)
        {
            string value = "";
            foreach (LocalNode localNode in s_allNodes)
            {
                if (localNode.nodeID == node_id)
                {
                    if (localNode.title != null)
                        value = localNode.title.ToString();
                    break;
                }
            }
            return value;
        }
        public static string GetNodeDescription(string node_id)
        {
            string value = string.Empty;
            foreach (LocalNode localNode in s_allNodes)
            {
                if (localNode.nodeID == node_id)
                {
                    if (localNode.description != null)
                        value = localNode.description.ToString();
                    break;
                }
            }
            return value;
        }

        public static string GetNodeFrameworkName(string node_id)
        {
            string value = "";
            foreach (LocalNode localNode in s_allNodes)
            {
                if (localNode.nodeID == node_id)
                {
                    if (localNode.frameworkReference != null)
                        value = localNode.frameworkName.ToString();
                    break;
                }
            }
            return value;
        }

        public static string GetNodeFrameworkReference(string node_id)
        {
            string value = "";
            foreach (LocalNode localNode in s_allNodes)
            {
                if (localNode.nodeID == node_id)
                {
                    if (localNode.frameworkReference != null)
                        value = localNode.frameworkReference.ToString();
                    break;
                }
            }
            return value;
        }




        public static void SetNodeData(string node_id, string param, string value)
        {
            for (int i = 0; i < s_allNodes.Count; i++)
            {
                if (s_allNodes[i].nodeID == node_id)
                {
                    switch (param.ToLower())
                    {
                        case "type":
                            s_allNodes[i].nodeType = value;
                            break;
                        case "controlbasescore":
                            s_allNodes[i].controlBaseScore = value;
                            break;
                        case "controlbasevalue":
                            s_allNodes[i].controlBaseValue = value;
                            break;
                        case "controlbaseminvalue":
                            s_allNodes[i].controlBaseMinValue = value;
                            break;
                        case "calculatedvalue":
                            s_allNodes[i].calculatedValue = value;
                            break;
                        case "controlassessedminvalue":
                            s_allNodes[i].controlAssessedMinValue = value;
                            break;
                        case "controlassessedvalue":
                            s_allNodes[i].controlAssessedValue = value;
                            break;
                        case "controlassessedscore":
                            s_allNodes[i].controlAssessedScore = value;
                            break;
                        case "previouscontrolbaseScore":
                            s_allNodes[i].previouscontrolBaseScore = value;
                            break;
                        case "previouscalculatedvalue":
                            s_allNodes[i].previousCalculatedValue = value;
                            break;
                        case "objectivetargettype":
                            s_allNodes[i].objectiveTargetType = value;
                            break;
                        case "objectivetargetvalue":
                            s_allNodes[i].objectiveTargetValue = value;
                            break;
                        case "objectiveacheivedvalue":
                            s_allNodes[i].objectiveAcheivedValue = value;
                            break;
                        case "enabled":
                            s_allNodes[i].enabled = value == "true";
                            break;
                        case "impactedvalue":
                            s_allNodes[i].enabled = value == "true";
                            break;
                        case "actoraccessvalue":
                            s_allNodes[i].actorAccessValue = value;
                            break;
                        case "actoraccessminvalue":
                            s_allNodes[i].actorAccessMinValue = value;
                            break;
                        case "actorcapabilityvalue":
                            s_allNodes[i].actorCapabilityValue = value;
                            break;
                        case "actorcapabilityminvalue":
                            s_allNodes[i].actorCapabilityMinValue = value;
                            break;
                        case "actorresourcesvalue":
                            s_allNodes[i].actorResourcesValue = value;
                            break;
                        case "actorresourcesminvalue":
                            s_allNodes[i].actorResourcesMinValue = value;
                            break;
                        case "actormotivationvalue":
                            s_allNodes[i].actorMotivationValue = value;
                            break;
                        case "actormotivationminvalue":
                            s_allNodes[i].actorMotivationMinValue = value;
                            break;
                        case "actormitigatedscore":
                            s_allNodes[i].actorMitigatedScore = value;
                            break;
                        case "actorImpactToConfidentialityvalue":
                            s_allNodes[i].actorImpactToConfidentialityValue = value;
                            break;
                        case "actorImpactToConfidentialityminvalue":
                            s_allNodes[i].actorImpactToConfidentialityMinValue = value;
                            break;
                        case "actorimpacttointegrityvalue":
                            s_allNodes[i].actorImpactToIntegrityValue = value;
                            break;
                        case "actorimpacttointegrityminvalue":
                            s_allNodes[i].actorImpactToIntegrityMinValue = value;
                            break;
                        case "actorimpacttoavailabilityvalue":
                            s_allNodes[i].actorImpactToAvailabilityValue = value;
                            break;
                        case "actorimpacttoavailabilityminvalue":
                            s_allNodes[i].actorImpactToAvailabilityMinValue = value;
                            break;
                        case "actorimpacttoaccountabilityvalue":
                            s_allNodes[i].actorImpactToAccountabilityValue = value;
                            break;
                        case "actorimpacttoaccountabilityminvalue":
                            s_allNodes[i].actorImpactToAccountabilityMinValue = value;
                            break;
                        case "attackmitigatedscore":
                            s_allNodes[i].attackMitigatedScore = value;
                            break;
                        case "threatscore":
                            s_allNodes[i].threatScore = value;
                            break;
                        case "riskscore":
                            s_allNodes[i].riskScore = value;
                            break;
                        case "assetscore":
                            s_allNodes[i].assetScore = value;
                            break;
                        case "assetmitigatedscore":
                            s_allNodes[i].assetMitigatedScore = value;
                            break;
                        case "assetlikelihoodscore":
                            s_allNodes[i].assetLikelihoodScore = value;
                            break;
                        case "implementedstrength":
                            s_allNodes[i].implementedStrength = value;
                            break;
                        case "vulnerabilityeaseofexploitationvalue":
                            s_allNodes[i].vulnerabilityEaseOfExploitationValue = value;
                            CalculateVulnerabilityScoreForNode(node_id);
                            break;
                        case "vulnerabilityeaseofexploitationminvalue":
                            s_allNodes[i].vulnerabilityEaseOfExploitationMinValue = value;
                            CalculateVulnerabilityScoreForNode(node_id);
                            break;
                        case "vulnerabilityexposesscopevalue":
                            s_allNodes[i].vulnerabilityExposesScopeValue = value;
                            CalculateVulnerabilityScoreForNode(node_id);
                            break;
                        case "vulnerabilityexposesscopeminvalue":
                            s_allNodes[i].vulnerabilityExposesScopeMinValue = value;
                            CalculateVulnerabilityScoreForNode(node_id);
                            break;
                        case "vulnerabilityinteractionrequiredvalue":
                            s_allNodes[i].vulnerabilityInteractionRequiredValue = value;
                            CalculateVulnerabilityScoreForNode(node_id);
                            break;
                        case "vulnerabilityinteractionrequiredminvalue":
                            s_allNodes[i].vulnerabilityInteractionRequiredMinValue = value;
                            CalculateVulnerabilityScoreForNode(node_id);
                            break;
                        case "vulnerabilityprivilegesrequiredvalue":
                            s_allNodes[i].vulnerabilityPrivilegesRequiredValue = value;
                            CalculateVulnerabilityScoreForNode(node_id);
                            break;
                        case "vulnerabilityprivilegesrequiredminvalue":
                            s_allNodes[i].vulnerabilityPrivilegesRequiredMinValue = value;
                            CalculateVulnerabilityScoreForNode(node_id);
                            break;
                        case "vulnerabilityexposurevalue":
                            s_allNodes[i].vulnerabilityExposureValue = value;
                            CalculateVulnerabilityScoreForNode(node_id);
                            break;
                        case "vulnerabilityexposureminvalue":
                            s_allNodes[i].vulnerabilityExposureMinValue = value;
                            CalculateVulnerabilityScoreForNode(node_id);
                            break;
                        case "vulnerabilityimpacttoconfidentialityvalue":
                            s_allNodes[i].vulnerabilityImpactToConfidentialityValue = value;
                            CalculateVulnerabilityScoreForNode(node_id);
                            break;
                        case "vulnerabilityimpacttoconfidentialityminvalue":
                            s_allNodes[i].vulnerabilityImpactToConfidentialityMinValue = value;
                            CalculateVulnerabilityScoreForNode(node_id);
                            break;
                        case "vulnerabilityimpacttointegrityvalue":
                            s_allNodes[i].vulnerabilityImpactToIntegrityValue = value;
                            CalculateVulnerabilityScoreForNode(node_id);
                            break;
                        case "vulnerabilityimpacttointegrityminvalue":
                            s_allNodes[i].vulnerabilityImpactToIntegrityMinValue = value;
                            CalculateVulnerabilityScoreForNode(node_id);
                            break;
                        case "vulnerabilityimpacttoavailabilityvalue":
                            s_allNodes[i].vulnerabilityImpactToAvailabilityValue = value;
                            CalculateVulnerabilityScoreForNode(node_id);
                            break;
                        case "vulnerabilityimpacttoavailabilityminvalue":
                            s_allNodes[i].vulnerabilityImpactToAvailabilityMinValue = value;
                            CalculateVulnerabilityScoreForNode(node_id);
                            break;
                        case "vulnerabilityimpacttoaccountabilityvalue":
                            s_allNodes[i].vulnerabilityImpactToAccountabilityValue = value;
                            CalculateVulnerabilityScoreForNode(node_id);
                            break;
                        case "vulnerabilityimpacttoaccountabilityminvalue":
                            s_allNodes[i].vulnerabilityImpactToAccountabilityMinValue = value;
                            CalculateVulnerabilityScoreForNode(node_id);
                            break;
                        case "likelihoodscore":
                            s_allNodes[i].likelihoodScore = value;
                            break;
                        case "vulnerabilitymitigatedscore":
                            s_allNodes[i].vulnerabilityMitigatedScore = value;
                            break;
                        case "impactscore":
                            s_allNodes[i].impactScore = value;
                            break;
                        case "parent":
                            s_allNodes[i].parent = value;
                            break;
                        case "title":
                            s_allNodes[i].title = value;
                            break;
                        case "attackcomplexityvalue":
                            s_allNodes[i].attackComplexityValue = value;
                            CalculateAttackScoreForNode(node_id);
                            break;
                        case "attackcomplexityminvalue":
                            s_allNodes[i].attackComplexityMinValue = value;
                            CalculateAttackScoreForNode(node_id);
                            break;
                        case "attackproliferationvalue":
                            s_allNodes[i].attackProliferationValue = value;
                            CalculateAttackScoreForNode(node_id);
                            break;
                        case "attackproliferationminvalue":
                            s_allNodes[i].attackProliferationMinValue = value;
                            CalculateAttackScoreForNode(node_id);
                            break;
                        case "attackimpacttoconfidentialityvalue":
                            s_allNodes[i].attackImpactToConfidentialityValue = value;
                            CalculateAttackScoreForNode(node_id);
                            break;
                        case "attackimpacttoconfidentialityminvalue":
                            s_allNodes[i].attackImpactToConfidentialityMinValue = value;
                            CalculateAttackScoreForNode(node_id);
                            break;
                        case "attackimpacttointegrityvalue":
                            s_allNodes[i].attackImpactToIntegrityValue = value;
                            CalculateAttackScoreForNode(node_id);
                            break;
                        case "attackimpacttointegrityminvalue":
                            s_allNodes[i].attackImpactToIntegrityMinValue = value;
                            CalculateAttackScoreForNode(node_id);
                            break;
                        case "attackimpacttoavailabilityvalue":
                            s_allNodes[i].attackImpactToAvailabilityValue = value;
                            CalculateAttackScoreForNode(node_id);
                            break;
                        case "attackimpacttoavailabilityminvalue":
                            s_allNodes[i].attackImpactToAvailabilityMinValue = value;
                            CalculateAttackScoreForNode(node_id);
                            break;
                        case "attackimpacttoaccountabilityvalue":
                            s_allNodes[i].attackImpactToAccountabilityValue = value;
                            CalculateAttackScoreForNode(node_id);
                            break;
                        case "attackimpacttoaccountabilityminvalue":
                            s_allNodes[i].attackImpactToAccountabilityMinValue = value;
                            CalculateAttackScoreForNode(node_id);
                            break;
                        case "assetconfidentialityvalue":
                            s_allNodes[i].assetConfidentialityValue = value;
                            CalculateAssetScoreForNode(node_id);
                            break;
                        case "assetconfidentialityminvalue":
                            s_allNodes[i].assetConfidentialityMinValue = value;
                            CalculateAssetScoreForNode(node_id);
                            break;
                        case "assetconfidentialityprobabilityvalue":
                            s_allNodes[i].assetConfidentialityProbabilityValue = value;
                            CalculateAssetScoreForNode(node_id);
                            break;
                        case "assetintegrityvalue":
                            s_allNodes[i].assetIntegrityValue = value;
                            CalculateAssetScoreForNode(node_id);
                            break;
                        case "assetintegrityminvalue":
                            s_allNodes[i].assetIntegrityMinValue = value;
                            CalculateAssetScoreForNode(node_id);
                            break;
                        case "assetintegrityprobabilityvalue":
                            s_allNodes[i].assetIntegrityProbabilityValue = value;
                            CalculateAssetScoreForNode(node_id);
                            break;
                        case "assetavailabilityvalue":
                            s_allNodes[i].assetAvailabilityValue = value;
                            CalculateAssetScoreForNode(node_id);
                            break;
                        case "assetavailabilityminvalue":
                            s_allNodes[i].assetAvailabilityMinValue = value;
                            CalculateAssetScoreForNode(node_id);
                            break;
                        case "assetavailabilityprobabilityvalue":
                            s_allNodes[i].assetAvailabilityProbabilityValue = value;
                            CalculateAssetScoreForNode(node_id);
                            break;
                        case "assetaccountabilityvalue":
                            s_allNodes[i].assetAccountabilityValue = value;
                            CalculateAssetScoreForNode(node_id);
                            break;
                        case "assetaccountabilityminvalue":
                            s_allNodes[i].assetAccountabilityMinValue = value;
                            CalculateAssetScoreForNode(node_id);
                            break;
                        case "assetaccountabilityprobabilityvalue":
                            s_allNodes[i].assetAccountabilityProbabilityValue = value;
                            CalculateAssetScoreForNode(node_id);
                            break;
                        case "nodebehaviour":
                            s_allNodes[i].nodeBehaviour = value;
                            CalculateAssetScoreForNode(node_id);
                            break;
                        case "assetprivacyimpactconfidentialityvalue":
                            s_allNodes[i].assetPrivacyImpactConfidentialityValue = value;
                            CalculateAssetScoreForNode(node_id);
                            break;
                        case "assetprivacyimpactconfidentialityminvalue":
                            s_allNodes[i].assetPrivacyImpactConfidentialityMinValue = value;
                            CalculateAssetScoreForNode(node_id);
                            break;
                        case "assetlegalimpactconfidentialityvalue":
                            s_allNodes[i].assetLegalImpactConfidentialityValue = value;
                            CalculateAssetScoreForNode(node_id);
                            break;
                        case "assetlegalimpactconfidentialityminvalue":
                            s_allNodes[i].assetLegalImpactConfidentialityMinValue = value;
                            CalculateAssetScoreForNode(node_id);
                            break;
                        case "assetregulatoryimpactconfidentialityvalue":
                            s_allNodes[i].assetRegulatoryImpactConfidentialityValue = value;
                            CalculateAssetScoreForNode(node_id);
                            break;
                        case "assetregulatoryimpactconfidentialityminvalue":
                            s_allNodes[i].assetRegulatoryImpactConfidentialityMinValue = value;
                            CalculateAssetScoreForNode(node_id);
                            break;
                        case "assetreputationalimpactconfidentialityvalue":
                            s_allNodes[i].assetReputationalImpactConfidentialityValue = value;
                            CalculateAssetScoreForNode(node_id);
                            break;
                        case "assetreputationalimpactconfidentialityminvalue":
                            s_allNodes[i].assetReputationalImpactConfidentialityMinValue = value;
                            CalculateAssetScoreForNode(node_id);
                            break;
                        case "assetfinancialimpactconfidentialityvalue":
                            s_allNodes[i].assetFinancialImpactConfidentialityValue = value;
                            CalculateAssetScoreForNode(node_id);
                            break;
                        case "assetfinancialimpactconfidentialityminvalue":
                            s_allNodes[i].assetFinancialImpactConfidentialityMinValue = value;
                            CalculateAssetScoreForNode(node_id);
                            break;
                        case "assetprivacyimpactintegrityvalue":
                            s_allNodes[i].assetPrivacyImpactIntegrityValue = value;
                            CalculateAssetScoreForNode(node_id);
                            break;
                        case "assetprivacyimpactintegrityminvalue":
                            s_allNodes[i].assetPrivacyImpactIntegrityMinValue = value;
                            CalculateAssetScoreForNode(node_id);
                            break;
                        case "assetlegalimpactintegrityvalue":
                            s_allNodes[i].assetLegalImpactIntegrityValue = value;
                            CalculateAssetScoreForNode(node_id);
                            break;
                        case "assetlegalimpactintegrityminvalue":
                            s_allNodes[i].assetLegalImpactIntegrityMinValue = value;
                            CalculateAssetScoreForNode(node_id);
                            break;
                        case "assetreputationalimpactintegrityvalue":
                            s_allNodes[i].assetReputationalImpactIntegrityValue = value;
                            CalculateAssetScoreForNode(node_id);
                            break;
                        case "assetreputationalimpactintegrityminvalue":
                            s_allNodes[i].assetReputationalImpactIntegrityMinValue = value;
                            CalculateAssetScoreForNode(node_id);
                            break;
                        case "assetregulatoryimpactintegrityvalue":
                            s_allNodes[i].assetRegulatoryImpactIntegrityValue = value;
                            CalculateAssetScoreForNode(node_id);
                            break;
                        case "assetregulatoryimpactintegrityminvalue":
                            s_allNodes[i].assetRegulatoryImpactIntegrityMinValue = value;
                            CalculateAssetScoreForNode(node_id);
                            break;
                        case "assetfinancialimpactintegrityvalue":
                            s_allNodes[i].assetFinancialImpactIntegrityValue = value;
                            CalculateAssetScoreForNode(node_id);
                            break;
                        case "assetfinancialimpactintegrityminvalue":
                            s_allNodes[i].assetFinancialImpactIntegrityMinValue = value;
                            CalculateAssetScoreForNode(node_id);
                            break;
                        case "assetprivacyimpactavailabilityvalue":
                            s_allNodes[i].assetPrivacyImpactAvailabilityValue = value;
                            CalculateAssetScoreForNode(node_id);
                            break;
                        case "assetprivacyimpactavailabilityminvalue":
                            s_allNodes[i].assetPrivacyImpactAvailabilityMinValue = value;
                            CalculateAssetScoreForNode(node_id);
                            break;
                        case "assetlegalimpactavailabilityvalue":
                            s_allNodes[i].assetLegalImpactAvailabilityValue = value;
                            CalculateAssetScoreForNode(node_id);
                            break;
                        case "assetlegalimpactavailabilityminvalue":
                            s_allNodes[i].assetLegalImpactAvailabilityMinValue = value;
                            CalculateAssetScoreForNode(node_id);
                            break;
                        case "assetregulatoryimpactavailabilityvalue":
                            s_allNodes[i].assetRegulatoryImpactAvailabilityValue = value;
                            CalculateAssetScoreForNode(node_id);
                            break;
                        case "assetregulatoryimpactavailabilityminvalue":
                            s_allNodes[i].assetRegulatoryImpactAvailabilityMinValue = value;
                            CalculateAssetScoreForNode(node_id);
                            break;
                        case "assetreputationalimpactavailabilityvalue":
                            s_allNodes[i].assetReputationalImpactAvailabilityValue = value;
                            CalculateAssetScoreForNode(node_id);
                            break;
                        case "assetreputationalimpactavailabilityminvalue":
                            s_allNodes[i].assetReputationalImpactAvailabilityMinValue = value;
                            CalculateAssetScoreForNode(node_id);
                            break;
                        case "assetfinancialimpactavailabilityvalue":
                            s_allNodes[i].assetFinancialImpactAvailabilityValue = value;
                            CalculateAssetScoreForNode(node_id);
                            break;
                        case "assetfinancialimpactavailabilityminvalue":
                            s_allNodes[i].assetFinancialImpactAvailabilityMinValue = value;
                            CalculateAssetScoreForNode(node_id);
                            break;
                        case "assetprivacyimpactaccountabilityvalue":
                            s_allNodes[i].assetPrivacyImpactAccountabilityValue = value;
                            CalculateAssetScoreForNode(node_id);
                            break;
                        case "assetprivacyimpactaccountabilityminvalue":
                            s_allNodes[i].assetPrivacyImpactAccountabilityMinValue = value;
                            CalculateAssetScoreForNode(node_id);
                            break;
                        case "assetlegalimpactaccountabilityvalue":
                            s_allNodes[i].assetLegalImpactAccountabilityValue = value;
                            CalculateAssetScoreForNode(node_id);
                            break;
                        case "assetlegalimpactaccountabilityminvalue":
                            s_allNodes[i].assetLegalImpactAccountabilityMinValue = value;
                            CalculateAssetScoreForNode(node_id);
                            break;
                        case "assetregulatoryimpactaccountabilityvalue":
                            s_allNodes[i].assetRegulatoryImpactAccountabilityValue = value;
                            CalculateAssetScoreForNode(node_id);
                            break;
                        case "assetregulatoryimpactaccountabilityminvalue":
                            s_allNodes[i].assetRegulatoryImpactAccountabilityMinValue = value;
                            CalculateAssetScoreForNode(node_id);
                            break;
                        case "assetreputationalimpactaccountabilityvalue":
                            s_allNodes[i].assetReputationalImpactAccountabilityValue = value;
                            CalculateAssetScoreForNode(node_id);
                            break;
                        case "assetreputationalimpactaccountabilityminvalue":
                            s_allNodes[i].assetReputationalImpactAccountabilityMinValue = value;
                            CalculateAssetScoreForNode(node_id);
                            break;
                        case "assetfinancialimpactaccountabilityminvalue":
                            s_allNodes[i].assetFinancialImpactAccountabilityMinValue = value;
                            CalculateAssetScoreForNode(node_id);
                            break;
                        case "assetginancialimpactaccountabilityvalue":
                            s_allNodes[i].assetFinancialImpactAccountabilityValue = value;
                            CalculateAssetScoreForNode(node_id);
                            break;
                        case "controlbasedistribution":
                            s_allNodes[i].controlBaseDistribution = value;
                            break;
                        case "controlassesseddistribution":
                            s_allNodes[i].controlAssessedDistribution = value;
                            break;
                        case "actoraccessdistribution":
                            s_allNodes[i].actorAccessDistribution = value;
                            break;
                        case "actorcapabilitydistribution":
                            s_allNodes[i].actorCapabilityDistribution = value;
                            break;
                        case "actorresourcesdistribution":
                            s_allNodes[i].actorResourcesDistribution = value;
                            break;
                        case "actormotivationdistribution":
                            s_allNodes[i].actorMotivationDistribution = value;
                            break;
                        case "actorimpacttoconfidentialitydistribution":
                            s_allNodes[i].actorImpactToConfidentialityDistribution = value;
                            break;
                        case "actorimpacttointegritydistribution":
                            s_allNodes[i].actorImpactToIntegrityDistribution = value;
                            break;
                        case "actorimpacttoavailabilitydistribution":
                            s_allNodes[i].actorImpactToAvailabilityDistribution = value;
                            break;
                        case "actorimpacttoaccountabilitydistribution":
                            s_allNodes[i].actorImpactToAccountabilityDistribution = value;
                            break;
                        case "vulnerabilityeaseofexploitationdistribution":
                            s_allNodes[i].vulnerabilityEaseOfExploitationDistribution = value;
                            break;
                        case "vulnerabilityexposesscopedistribution":
                            s_allNodes[i].vulnerabilityExposesScopeDistribution = value;
                            break;
                        case "vulnerabilityinteractionrequireddistribution":
                            s_allNodes[i].vulnerabilityInteractionRequiredDistribution = value;
                            break;
                        case "vulnerabilityprivilegesrequireddistribution":
                            s_allNodes[i].vulnerabilityPrivilegesRequiredDistribution = value;
                            break;
                        case "vulnerabilityexposuremindistribution":
                            s_allNodes[i].vulnerabilityExposureDistribution = value;
                            break;
                        case "vulnerabilityimpacttoconfidentialitydistribution":
                            s_allNodes[i].vulnerabilityImpactToConfidentialityDistribution = value;
                            break;
                        case "vulnerabilityimpacttointegritydistribution":
                            s_allNodes[i].vulnerabilityImpactToIntegrityDistribution = value;
                            break;
                        case "vulnerabilityimpacttoavailabilitydistribution":
                            s_allNodes[i].vulnerabilityImpactToAvailabilityDistribution = value;
                            break;
                        case "vulnerabilityimpacttoaccountabilitydistribution":
                            s_allNodes[i].vulnerabilityImpactToAccountabilityDistribution = value;
                            break;
                        case "attackcomplexitydistribution":
                            s_allNodes[i].attackComplexityDistribution = value;
                            break;
                        case "attackproliferationdistribution":
                            s_allNodes[i].attackProliferationDistribution = value;
                            break;
                        case "attackimpacttoconfidentialitydistribution":
                            s_allNodes[i].attackImpactToConfidentialityDistribution = value;
                            break;
                        case "attackimpacttointegritydistribution":
                            s_allNodes[i].attackImpactToIntegrityDistribution = value;
                            break;
                        case "attackimpacttoavailabilitydistribution":
                            s_allNodes[i].attackImpactToAvailabilityDistribution = value;
                            break;
                        case "attackimpacttoaccountabilitydistribution":
                            s_allNodes[i].attackImpactToAccountabilityDistribution = value;
                            break;
                        case "assetconfidentialitydistribution":
                            s_allNodes[i].assetConfidentialityDistribution = value;
                            break;
                        case "assetintegritydistribution":
                            s_allNodes[i].assetIntegrityDistribution = value;
                            break;
                        case "assetavailabilitydistribution":
                            s_allNodes[i].assetAvailabilityDistribution = value;
                            break;
                        case "assetaccountabilitydistribution":
                            s_allNodes[i].assetAccountabilityDistribution = value;
                            break;
                        case "assetprivacyimpactconfidentialitydistribution":
                            s_allNodes[i].assetPrivacyImpactConfidentialityDistribution = value;
                            break;
                        case "assetlegalimpactconfidentialitydistribution":
                            s_allNodes[i].assetLegalImpactConfidentialityDistribution = value;
                            break;
                        case "assetregulatoryimpactconfidentialitydistribution":
                            s_allNodes[i].assetRegulatoryImpactConfidentialityDistribution = value;
                            break;
                        case "assetreputationalimpactconfidentialitydistribution":
                            s_allNodes[i].assetReputationalImpactConfidentialityDistribution = value;
                            break;
                        case "assetfinancialimpactconfidentialitydistribution":
                            s_allNodes[i].assetFinancialImpactConfidentialityDistribution = value;
                            break;
                        case "assetprivacyimpactintegritydistribution":
                            s_allNodes[i].assetPrivacyImpactIntegrityDistribution = value;
                            break;
                        case "assetlegalimpactintegritydistribution":
                            s_allNodes[i].assetLegalImpactIntegrityDistribution = value;
                            break;
                        case "assetreputationalimpactintegritydistribution":
                            s_allNodes[i].assetReputationalImpactIntegrityDistribution = value;
                            break;
                        case "assetRegulatoryImpactIntegrityDistribution":
                            s_allNodes[i].assetRegulatoryImpactIntegrityDistribution = value;
                            break;
                        case "assetfinancialimpactintegritydistribution":
                            s_allNodes[i].assetFinancialImpactIntegrityDistribution = value;
                            break;
                        case "assetprivacyimpactavailabilitydistribution":
                            s_allNodes[i].assetPrivacyImpactAvailabilityDistribution = value;
                            break;
                        case "assetlegalimpactavailabilitydistribution":
                            s_allNodes[i].assetLegalImpactAvailabilityDistribution = value;
                            break;
                        case "assetregulatoryimpactavailabilitydistribution":
                            s_allNodes[i].assetRegulatoryImpactAvailabilityDistribution = value;
                            break;
                        case "assetreputationalimpactavailabilitydistribution":
                            s_allNodes[i].assetReputationalImpactAvailabilityDistribution = value;
                            break;
                        case "assetfinancialimpactavailabilitydistribution":
                            s_allNodes[i].assetFinancialImpactAvailabilityDistribution = value;
                            break;
                        case "assetprivacyimpactaccountabilitydistribution":
                            s_allNodes[i].assetPrivacyImpactAccountabilityDistribution = value;
                            break;
                        case "assetlegalimpactaccountabilitydistribution":
                            s_allNodes[i].assetLegalImpactAccountabilityDistribution = value;
                            break;
                        case "assetregulatoryimpactaccountabilitydistribution":
                            s_allNodes[i].assetRegulatoryImpactAccountabilityDistribution = value;
                            break;
                        case "assetreputationalimpactaccountabilitydistribution":
                            s_allNodes[i].assetReputationalImpactAccountabilityDistribution = value;
                            break;
                        case "assetfinancialimpactaccountabilitydistribution":
                            s_allNodes[i].assetFinancialImpactAccountabilityDistribution = value;
                            break;
                        case "assetconfidentialitymitigatedscore":
                            s_allNodes[i].assetConfidentialityMitigatedScore = value;
                            break;
                        case "assetintegritymitigatedscore":
                            s_allNodes[i].assetIntegrityMitigatedScore = value;
                            break;
                        case "assetavailabilitymitigatedscore":
                            s_allNodes[i].assetAvailabilityMitigatedScore = value;
                            break;
                        case "assetaccountabilitymitigatedscore":
                            s_allNodes[i].assetAccountabilityMitigatedScore = value;
                            break;
                        case "frameworkreference":
                            s_allNodes[i].frameworkReference = value;
                            break;
                        case "description":
                            s_allNodes[i].description = value;
                            break;


                    }
                    break;
                }
            }
        }






        public static void SetEdgeData(string edge_id, string param, string value)
        {
            for (int i = 0; i < s_allEdges.Count; i++)
            {
                if (s_allEdges[i].edgeID == edge_id)
                {
                    switch (param.ToLower())
                    {
                        case "impactedvalue":
                            s_allEdges[i].impactedValue = value;
                            break;
                        case "edgestrengthvalue":
                            s_allEdges[i].edgeStrengthValue = value;
                            break;
                        case "edgestrengthscore":
                            s_allEdges[i].edgeStrengthScore = value;
                            break;
                        case "edgestrengthminvalue":
                            s_allEdges[i].edgeStrengthMinValue = value;
                            break;
                        case "edgestrengthdistribution":
                            s_allEdges[i].edgeStrengthDistribution = value;
                            break;
                        case "enabled":
                            s_allEdges[i].enabled = value == "true";
                            break;
                    }
                    break;
                }
            }
        }

        public static void SetEdgeStrength(string edge_id, string value)
        {
            for (int i = 0; i < s_allEdges.Count; i++)
            {
                if (s_allEdges[i].edgeID == edge_id)
                {
                    s_allEdges[i].edgeStrengthValue = value;
                    break;
                }
            }
        }

        public static async Task SyncFromGraph()
        {
            s_allEdges.Clear();
            s_allNodes.Clear();
            s_allRelationships.Clear();
            JavascriptResponse allNodes = null;
            JavascriptResponse allEdges = null;

            try
            {
                allNodes = await _browser.EvaluateScriptAsync("getNodes();");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            if (allNodes != null && allNodes.Success)
            {
                var tmpObj = JArray.Parse(allNodes.Result.ToString());
                List<Node> nodes = new List<Node>();
                foreach (var tmpNode in tmpObj)
                {
                    Node node = Node.FromJson(tmpNode.ToString());
                    LocalNode lNode = new LocalNode();
                    lNode.nodeID = node.ID;

                    lNode.nodeType = node.Type.ToString();
                    lNode.enabled = node.Enabled;
                    lNode.implementedStrength = node.ImplementedStrength == null ? "" : node.ImplementedStrength.ToString();
                    lNode.objectiveTargetType = node.objectiveTargetType == null ? "" : node.objectiveTargetType.ToString();
                    lNode.objectiveTargetValue = node.objectiveTargetValue == null ? "" : node.objectiveTargetValue.ToString();

                    lNode.controlAssessedValue = node.controlAssessedValue.ToString();
                    lNode.controlAssessedScore = node.controlAssessedScore.ToString();
                    lNode.controlAssessedMinValue = node.controlAssessedMinValue.ToString();
                    lNode.controlBaseScore = node.controlBaseScore.ToString();
                    lNode.controlBaseValue = node.controlBaseValue.ToString();
                    lNode.controlBaseMinValue = node.controlBaseMinValue.ToString();
                    lNode.calculatedValue = node.CalculatedValue.ToString();
                    lNode.controlBaseDistribution = node.controlBaseDistribution.ToString();
                    lNode.controlAssessedDistribution = node.controlAssessedDistribution.ToString();

                    lNode.riskScore = node.riskScore.ToString();

                    lNode.parent = node.Parent.ToString();
                    lNode.title = node.Title.ToString();
                    lNode.description = node.description.ToString();
                    lNode.frameworkReference = node.frameworkReference.ToString();
                    lNode.frameworkName = node.frameworkName.ToString();
                    lNode.nodeBehaviour = node.nodeBehaviour.ToString();

                    lNode.actorCapabilityValue = node.actorCapabilityValue.ToString();
                    lNode.actorResourcesValue = node.actorResourcesValue.ToString();
                    lNode.actorMotivationValue = node.actorMotivationValue.ToString();
                    lNode.actorAccessValue = node.actorAccessValue.ToString();
                    lNode.actorScore = node.actorScore.ToString();
                    lNode.actorAccessValue = node.actorAccessValue.ToString();
                    lNode.actorAccessMinValue = node.actorAccessMinValue.ToString();
                    lNode.actorCapabilityValue = node.actorCapabilityValue.ToString();
                    lNode.actorCapabilityMinValue = node.actorCapabilityMinValue.ToString();
                    lNode.actorMotivationValue = node.actorMotivationValue.ToString();
                    lNode.actorMotivationMinValue = node.actorMotivationMinValue.ToString();
                    lNode.actorResourcesValue = node.actorResourcesValue.ToString();
                    lNode.actorResourcesMinValue = node.actorResourcesMinValue.ToString();
                    lNode.actorImpactToConfidentialityValue = node.actorImpactToConfidentialityValue.ToString();
                    lNode.actorImpactToConfidentialityMinValue = node.actorImpactToConfidentialityMinValue.ToString();
                    lNode.actorImpactToIntegrityValue = node.actorImpactToIntegrityValue.ToString();
                    lNode.actorImpactToIntegrityMinValue = node.actorImpactToIntegrityMinValue.ToString();
                    lNode.actorImpactToAvailabilityValue = node.actorImpactToAvailabilityValue.ToString();
                    lNode.actorImpactToAvailabilityMinValue = node.actorImpactToAvailabilityMinValue.ToString();
                    lNode.actorImpactToAccountabilityValue = node.actorImpactToAccountabilityValue.ToString();
                    lNode.actorImpactToAccountabilityMinValue = node.actorimpactToAccountabilityMinValue.ToString();
                    lNode.actorAccessDistribution = node.actorAccessDistribution.ToString();
                    lNode.actorCapabilityDistribution = node.actorCapabilityDistribution.ToString();
                    lNode.actorResourcesDistribution = node.actorResourcesDistribution.ToString();
                    lNode.actorMotivationDistribution = node.actorMotivationDistribution.ToString();
                    lNode.actorImpactToConfidentialityDistribution = node.actorImpactToConfidentialityDistribution.ToString();
                    lNode.actorImpactToIntegrityDistribution = node.actorImpactToIntegrityDistribution.ToString();
                    lNode.actorImpactToAvailabilityDistribution = node.actorImpactToAvailabilityDistribution.ToString();
                    lNode.actorImpactToAccountabilityDistribution = node.actorImpactToAccountabilityDistribution.ToString();

                    lNode.attackComplexityValue = node.attackComplexityValue.ToString();
                    lNode.attackProliferationValue = node.attackProliferationValue.ToString();
                    lNode.attackScore = node.attackScore.ToString();
                    lNode.attackComplexityValue = node.attackComplexityValue.ToString();
                    lNode.attackComplexityMinValue = node.attackComplexityMinValue.ToString();
                    lNode.attackImpactToAccountabilityValue = node.attackImpactToAccountabilityValue.ToString();
                    lNode.attackImpactToAccountabilityMinValue = node.attackImpactToAccountabilityMinValue.ToString();
                    lNode.attackImpactToAvailabilityValue = node.attackImpactToAvailabilityValue.ToString();
                    lNode.attackImpactToAvailabilityMinValue = node.attackImpactToAvailabilityMinValue.ToString();
                    lNode.attackImpactToConfidentialityValue = node.attackImpactToConfidentialityValue.ToString();
                    lNode.attackImpactToConfidentialityMinValue = node.attackImpactToConfidentialityMinValue.ToString();
                    lNode.attackImpactToIntegrityValue = node.attackImpactToIntegrityValue.ToString();
                    lNode.attackImpactToIntegrityMinValue = node.attackImpactToIntegrityMinValue.ToString();
                    lNode.attackProliferationValue = node.attackProliferationValue.ToString();
                    lNode.attackProliferationMinValue = node.attackProliferationMinValue.ToString();
                    lNode.attackComplexityDistribution = node.attackComplexityDistribution.ToString();
                    lNode.attackProliferationDistribution = node.attackProliferationDistribution.ToString();
                    lNode.attackImpactToConfidentialityDistribution = node.attackImpactToConfidentialityDistribution.ToString();
                    lNode.attackImpactToIntegrityDistribution = node.attackImpactToIntegrityDistribution.ToString();
                    lNode.attackImpactToAvailabilityDistribution = node.attackImpactToAvailabilityDistribution.ToString();
                    lNode.attackImpactToAccountabilityDistribution = node.attackImpactToAccountabilityDistribution.ToString();

                    lNode.assetAvailabilityValue = node.assetAvailabilityValue.ToString();
                    lNode.assetAccountabilityValue = node.assetAccountabilityValue.ToString();
                    lNode.assetScore = node.assetScore.ToString();
                    lNode.assetRegulatoryImpactAvailabilityValue = node.assetRegulatoryImpactAvailabilityValue.ToString();
                    lNode.assetReputationalImpactAvailabilityValue = node.assetReputationalImpactAvailabilityValue.ToString();
                    lNode.assetFinancialImpactAvailabilityValue = node.assetFinancialImpactAvailabilityValue.ToString();
                    lNode.assetPrivacyImpactAccountabilityValue = node.assetPrivacyImpactAccountabilityValue.ToString();
                    lNode.assetLegalImpactAccountabilityValue = node.assetLegalImpactAccountabilityValue.ToString();
                    lNode.assetRegulatoryImpactAccountabilityValue = node.assetRegulatoryImpactAccountabilityValue.ToString();
                    lNode.assetReputationalImpactAccountabilityValue = node.assetReputationalImpactAccountabilityValue.ToString();
                    lNode.assetPrivacyImpactConfidentialityValue = node.assetPrivacyImpactConfidentialityValue.ToString();
                    lNode.assetLegalImpactConfidentialityValue = node.assetLegalImpactConfidentialityValue.ToString();
                    lNode.assetReputationalImpactConfidentialityValue = node.assetReputationalImpactConfidentialityValue.ToString();
                    lNode.assetFinancialImpactConfidentialityValue = node.assetFinancialImpactConfidentialityValue.ToString();
                    lNode.assetPrivacyImpactIntegrityValue = node.assetPrivacyImpactIntegrityValue.ToString();
                    lNode.assetLegalImpactIntegrityValue = node.assetLegalImpactIntegrityValue.ToString();
                    lNode.assetRegulatoryImpactIntegrityValue = node.assetRegulatoryImpactIntegrityValue.ToString();
                    lNode.assetFinancialImpactIntegrityValue = node.assetFinancialImpactIntegrityValue.ToString();
                    lNode.assetPrivacyImpactAvailabilityValue = node.assetPrivacyImpactAvailabilityValue.ToString();
                    lNode.assetLegalImpactAvailabilityValue = node.assetLegalImpactAvailabilityValue.ToString();
                    lNode.assetReputationalImpactIntegrityValue = node.assetReputationalImpactIntegrityValue.ToString();
                    lNode.assetPrivacyImpactConfidentialityMinValue = node.assetPrivacyImpactConfidentialityMinValue.ToString();
                    lNode.assetLegalImpactConfidentialityMinValue = node.assetLegalImpactConfidentialityMinValue.ToString();
                    lNode.assetRegulatoryImpactConfidentialityValue = node.assetRegulatoryImpactConfidentialityValue.ToString();
                    lNode.assetRegulatoryImpactConfidentialityMinValue = node.assetRegulatoryImpactConfidentialityMinValue.ToString();
                    lNode.assetReputationalImpactConfidentialityMinValue = node.assetReputationalImpactConfidentialityMinValue.ToString();
                    lNode.assetFinancialImpactConfidentialityMinValue = node.assetFinancialImpactConfidentialityMinValue.ToString();
                    lNode.assetPrivacyImpactIntegrityMinValue = node.assetPrivacyImpactIntegrityMinValue.ToString();
                    lNode.assetLegalImpactIntegrityMinValue = node.assetLegalImpactIntegrityMinValue.ToString();
                    lNode.assetRegulatoryImpactIntegrityMinValue = node.assetRegulatoryImpactIntegrityMinValue.ToString();
                    lNode.assetReputationalImpactIntegrityMinValue = node.assetReputationalImpactIntegrityMinValue.ToString();
                    lNode.assetFinancialImpactIntegrityMinValue = node.assetFinancialImpactIntegrityMinValue.ToString();
                    lNode.assetPrivacyImpactAvailabilityMinValue = node.assetPrivacyImpactAvailabilityMinValue.ToString();
                    lNode.assetLegalImpactAvailabilityMinValue = node.assetLegalImpactAvailabilityMinValue.ToString();
                    lNode.assetReputationalImpactAvailabilityMinValue = node.assetReputationalImpactAvailabilityMinValue.ToString();
                    lNode.assetFinancialImpactAvailabilityMinValue = node.assetFinancialImpactAvailabilityMinValue.ToString();
                    lNode.assetPrivacyImpactAccountabilityMinValue = node.assetPrivacyImpactAccountabilityMinValue.ToString();
                    lNode.assetLegalImpactAccountabilityMinValue = node.assetLegalImpactAccountabilityMinValue.ToString();
                    lNode.assetRegulatoryImpactAccountabilityMinValue = node.assetRegulatoryImpactAccountabilityMinValue.ToString();
                    lNode.assetReputationalImpactAccountabilityMinValue = node.assetReputationalImpactAccountabilityMinValue.ToString();
                    lNode.assetFinancialImpactAccountabilityValue = node.assetFinancialImpactAccountabilityValue.ToString();
                    lNode.assetFinancialImpactAccountabilityMinValue = node.assetFinancialImpactAccountabilityMinValue.ToString();
                    lNode.assetAccountabilityValue = node.assetAccountabilityValue.ToString();
                    lNode.assetAvailabilityValue = node.assetAvailabilityValue.ToString();
                    lNode.assetConfidentialityValue = node.assetConfidentialityValue.ToString();
                    lNode.assetConfidentialityMinValue = node.assetConfidentialityMinValue.ToString();
                    lNode.assetConfidentialityProbabilityValue = node.assetConfidentialityProbabilityValue.ToString();
                    lNode.assetIntegrityValue = node.assetIntegrityValue.ToString();
                    lNode.assetConfidentialityDistribution = node.assetConfidentialityDistribution.ToString();
                    lNode.assetIntegrityDistribution = node.assetIntegrityDistribution.ToString();
                    lNode.assetAvailabilityDistribution = node.assetAvailabilityDistribution.ToString();
                    lNode.assetAccountabilityDistribution = node.assetAccountabilityDistribution.ToString();
                    lNode.assetPrivacyImpactConfidentialityDistribution = node.assetPrivacyImpactConfidentialityDistribution.ToString();
                    lNode.assetLegalImpactConfidentialityDistribution = node.assetLegalImpactConfidentialityDistribution.ToString();
                    lNode.assetRegulatoryImpactConfidentialityDistribution = node.assetRegulatoryImpactConfidentialityDistribution.ToString();
                    lNode.assetReputationalImpactConfidentialityDistribution = node.assetReputationalImpactConfidentialityDistribution.ToString();
                    lNode.assetFinancialImpactConfidentialityDistribution = node.assetFinancialImpactConfidentialityDistribution.ToString();
                    lNode.assetPrivacyImpactIntegrityDistribution = node.assetPrivacyImpactIntegrityDistribution.ToString();
                    lNode.assetLegalImpactIntegrityDistribution = node.assetLegalImpactIntegrityDistribution.ToString();
                    lNode.assetReputationalImpactIntegrityDistribution = node.assetReputationalImpactIntegrityDistribution.ToString();
                    lNode.assetRegulatoryImpactIntegrityDistribution = node.assetRegulatoryImpactIntegrityDistribution.ToString();
                    lNode.assetFinancialImpactIntegrityDistribution = node.assetFinancialImpactIntegrityDistribution.ToString();
                    lNode.assetPrivacyImpactAvailabilityDistribution = node.assetPrivacyImpactAvailabilityDistribution.ToString();
                    lNode.assetLegalImpactAvailabilityDistribution = node.assetLegalImpactAvailabilityDistribution.ToString();
                    lNode.assetRegulatoryImpactAvailabilityDistribution = node.assetRegulatoryImpactAvailabilityDistribution.ToString();
                    lNode.assetReputationalImpactAvailabilityDistribution = node.assetReputationalImpactAvailabilityDistribution.ToString();
                    lNode.assetFinancialImpactAvailabilityDistribution = node.assetFinancialImpactAvailabilityDistribution.ToString();
                    lNode.assetPrivacyImpactAccountabilityDistribution = node.assetPrivacyImpactAccountabilityDistribution.ToString();
                    lNode.assetLegalImpactAccountabilityDistribution = node.assetLegalImpactAccountabilityDistribution.ToString();
                    lNode.assetRegulatoryImpactAccountabilityDistribution = node.assetRegulatoryImpactAccountabilityDistribution.ToString();
                    lNode.assetReputationalImpactAccountabilityDistribution = node.assetReputationalImpactAccountabilityDistribution.ToString();
                    lNode.assetFinancialImpactAccountabilityDistribution = node.assetFinancialImpactAccountabilityDistribution.ToString();
                    lNode.assetConfidentialityMitigatedScore = node.assetConfidentialityMitigatedScore.ToString();
                    lNode.assetIntegrityMitigatedScore = node.assetIntegrityMitigatedScore.ToString();
                    lNode.assetAvailabilityMitigatedScore = node.assetAvailabilityMitigatedScore.ToString();
                    lNode.assetAccountabilityMitigatedScore = node.assetAccountabilityMitigatedScore.ToString();

                    lNode.vulnerabilityEaseOfExploitationValue = node.vulnerabilityEaseOfExploitationValue.ToString();
                    lNode.vulnerabilityEaseOfExploitationMinValue = node.vulnerabilityEaseOfExploitationMinValue.ToString();
                    lNode.vulnerabilityExposureValue = node.vulnerabilityExposureValue.ToString();
                    lNode.vulnerabilityExposureMinValue = node.vulnerabilityExposureMinValue.ToString();
                    lNode.vulnerabilityInteractionRequiredValue = node.vulnerabilityInteractionRequiredValue.ToString();
                    lNode.vulnerabilityInteractionRequiredMinValue = node.vulnerabilityInteractionRequiredMinValue.ToString();
                    lNode.vulnerabilityPrivilegesRequiredMinValue = node.vulnerabilityPrivilegesRequiredMinValue.ToString();
                    lNode.vulnerabilityPrivilegesRequiredValue = node.vulnerabilityPrivilegesRequiredValue.ToString();
                    lNode.vulnerabilityImpactToConfidentialityValue = node.vulnerabilityImpactToConfidentialityValue.ToString();
                    lNode.vulnerabilityImpactToConfidentialityMinValue = node.vulnerabilityImpactToConfidentialityMinValue.ToString();
                    lNode.vulnerabilityImpactToIntegrityValue = node.vulnerabilityImpactToIntegrityValue.ToString();
                    lNode.vulnerabilityImpactToIntegrityMinValue = node.vulnerabilityImpactToIntegrityMinValue.ToString();
                    lNode.vulnerabilityImpactToAvailabilityValue = node.vulnerabilityImpactToAvailabilityValue.ToString();
                    lNode.vulnerabilityImpactToAvailabilityMinValue = node.vulnerabilityImpactToAvailabilityMinValue.ToString();
                    lNode.vulnerabilityImpactToAccountabilityValue = node.vulnerabilityImpactToAccountabilityValue.ToString();
                    lNode.vulnerabilityImpactToAccountabilityMinValue = node.vulnerabilityImpactToAccountabilityMinValue.ToString();
                    lNode.vulnerabilityEaseOfExploitationDistribution = node.vulnerabilityEaseOfExploitationDistribution.ToString();
                    lNode.vulnerabilityExposesScopeValue = node.vulnerabilityExposesScopeValue.ToString();
                    lNode.vulnerabilityExposesScopeMinValue = node.vulnerabilityExposesScopeMinValue.ToString();
                    lNode.vulnerabilityExposesScopeDistribution = node.vulnerabilityExposesScopeDistribution.ToString();
                    lNode.vulnerabilityInteractionRequiredDistribution = node.vulnerabilityInteractionRequiredDistribution.ToString();
                    lNode.vulnerabilityPrivilegesRequiredDistribution = node.vulnerabilityPrivilegesRequiredDistribution.ToString();
                    lNode.vulnerabilityExposureDistribution = node.vulnerabilityExposureDistribution.ToString();
                    lNode.vulnerabilityImpactToConfidentialityDistribution = node.vulnerabilityImpactToConfidentialityDistribution.ToString();
                    lNode.vulnerabilityImpactToIntegrityDistribution = node.vulnerabilityImpactToIntegrityDistribution.ToString();
                    lNode.vulnerabilityImpactToAvailabilityDistribution = node.vulnerabilityImpactToAvailabilityDistribution.ToString();
                    lNode.vulnerabilityImpactToAccountabilityDistribution = node.vulnerabilityImpactToAccountabilityDistribution.ToString();

                    s_allNodes.Add(lNode);
                }
            }
            else
            {

            }

            try
            {
                allEdges = await _browser.EvaluateScriptAsync("getEdges();");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return;
            }


            if (allEdges.Success)
            {
                var tmpObj = JArray.Parse(allEdges.Result.ToString());
                List<Edge> edges = new List<Edge>();
                foreach (var tmpEdge in tmpObj)
                {
                    try
                    {
                        Edge edge = Edge.FromJson(tmpEdge.ToString());
                        LocalEdge lEdge = new LocalEdge();
                        lEdge.edgeID = edge.ID;
                        lEdge.edgeStrengthValue = edge.edgeStrengthValue.ToString();
                        lEdge.impactedValue = edge.ImpactedValue.ToString();
                        lEdge.enabled = edge.Enabled;
                        lEdge.edgeStrengthMinValue = edge.edgeStrengthMinValue;
                        lEdge.edgeStrengthDistribution = edge.edgeStrengthDistribution;
                        s_allEdges.Add(lEdge);

                        LocalRelationship lRelationship = new LocalRelationship();
                        lRelationship.sourceNodeID = edge.Source;
                        lRelationship.targetNodeID = edge.Target;
                        lRelationship.edgeID = edge.ID;
                        lRelationship.Relationship = edge.Relationship;

                        s_allRelationships.Add(lRelationship);
                    }
                    catch
                    { }
                }
            }
            else
            {

            }
        }

        public static async Task SyncToGraph()
        {
            foreach (var node in s_allNodes)
            {
                _browser.ExecScriptAsync($"setElementData('{node.nodeID}', 'controlBaseScore', '{node.controlBaseScore}');");
                _browser.ExecScriptAsync($"setElementData('{node.nodeID}', 'controlAssessedScore', '{node.controlAssessedScore}');");
                _browser.ExecScriptAsync($"setElementData('{node.nodeID}', 'calculatedValue', '{node.calculatedValue}');");
                if (node.nodeType.ToLower() == "actor")
                {
                    _browser.ExecScriptAsync($"setElementData('{node.nodeID}', 'actorScore', '{node.actorScore}');");
                    _browser.ExecScriptAsync($"setElementData('{node.nodeID}', 'actorMitigatedScore', '{node.actorMitigatedScore}');");
                }
                if (node.nodeType.ToLower() == "attack")
                {
                    _browser.ExecScriptAsync($"setElementData('{node.nodeID}', 'attackScore', '{node.attackScore}');");
                    _browser.ExecScriptAsync($"setElementData('{node.nodeID}', 'attackMitigatedScore', '{node.attackMitigatedScore}');");
                    _browser.ExecScriptAsync($"setElementData('{node.nodeID}', 'threatScore', '{node.threatScore}');");
                }
                if (node.nodeType.ToLower() == "vulnerability")
                {
                    _browser.ExecScriptAsync($"setElementData('{node.nodeID}', 'vulnerabilityScore', '{node.vulnerabilityScore}');");
                    _browser.ExecScriptAsync($"setElementData('{node.nodeID}', 'vulnerabilityMitigatedScore', '{node.vulnerabilityMitigatedScore}');");
                }
                if (node.nodeType.ToLower() == "asset" || node.nodeType.ToLower() == "asset-group")
                {
                    _browser.ExecScriptAsync($"setElementData('{node.nodeID}', 'assetScore', '{node.assetScore}');");
                    _browser.ExecScriptAsync($"setElementData('{node.nodeID}', 'assetMitigatedScore', '{node.assetMitigatedScore}');");
                    _browser.ExecScriptAsync($"setElementData('{node.nodeID}', 'riskScore', '{node.riskScore}');");
                    _browser.ExecScriptAsync($"setElementData('{node.nodeID}', 'objectiveAcheivedValue', '{node.objectiveAcheivedValue}');");
                    _browser.ExecScriptAsync($"setElementData('{node.nodeID}', 'objectiveTargetValue', '{node.objectiveTargetValue}');");
                }
                if (node.nodeType.ToLower() == "objective")
                {
                    _browser.ExecScriptAsync($"setElementData('{node.nodeID}', 'objectiveAcheivedValue', '{node.objectiveAcheivedValue}');");
                    _browser.ExecScriptAsync($"setElementData('{node.nodeID}', 'objectiveTargetValue', '{node.objectiveTargetValue}');");
                }


                if (node.enabled)
                {
                    _browser.ExecScriptAsync($"enableNode('{node.nodeID}');");
                }
                else
                {
                    _browser.ExecScriptAsync($"disableNode('{node.nodeID}');");
                }
            }

            foreach (var edge in s_allEdges)
            {
                _browser.ExecScriptAsync($"setElementData('{edge.edgeID}', 'impactedvalue', '{edge.impactedValue}');");
                _browser.ExecScriptAsync($"setElementData('{edge.edgeID}', 'edgeStrengthValue', '{edge.edgeStrengthValue}');");
                if (edge.enabled)
                {
                    _browser.ExecScriptAsync($"enableEdge('{edge.edgeID}');");
                }
                else
                {
                    _browser.ExecScriptAsync($"disableEdge('{edge.edgeID}');");
                }
            }
        }

        public static void SetRootObjectivesToNotAssessed()
        {
            //Only do this for root objectives nodes
            for (int i = 0; i < s_allNodes.Count(); i++)
            {
                if (s_allNodes[i].nodeType.ToLower() == "objective" && IsRootNode(s_allNodes[i].nodeID) == true)
                {
                    s_allNodes[i].controlBaseScore = "100";
                }
            }
        }

        public static bool GraphBulkUpdate(bool flag)
        {
            bulkUpdate = flag;
            return flag;
        }

        public static void NullInMemoryItems()
        {
            for (int i = 0; i < s_allNodes.Count; i++)
            {
                s_allNodes[i].actorMitigatedScore = null;
                s_allNodes[i].highestAttackActorMitigatedScore = null;
                s_allNodes[i].attackMitigatedScore = null;
                s_allNodes[i].threatScore = null;
                s_allNodes[i].assetMitigatedScore = null;
                s_allNodes[i].impactScore = null;
                s_allNodes[i].vulnerabilityMitigatedScore = null;
                s_allNodes[i].likelihoodScore = null;
            }
        }

        public static void ZeroAllGroupNodes()
        {
            for (int i = 0; i < s_allNodes.Count; i++)
            {
                if (s_allNodes[i].nodeType.ToLower() == "group")
                {
                    s_allNodes[i].controlBaseScore = "0";
                    s_allNodes[i].calculatedValue = "0";
                    s_allNodes[i].previouscontrolBaseScore = "0";
                    s_allNodes[i].previousCalculatedValue = "0";
                    s_allNodes[i].previousControlAssessedValue = "0";
                }
            }
        }

        public static JArray GetNodesByType(string nodeType)
        {
            JArray nodelist = new JArray();
            for (int i = 0; i < s_allNodes.Count; i++)
            {
                if (s_allNodes[i].nodeType.ToLower() == nodeType)
                {
                    nodelist.Add(s_allNodes[i].nodeID);
                }
            }
            return nodelist;
        }


        public static void ZeroAllControlNodesPreviousValues()
        {
            for (int i = 0; i < s_allNodes.Count; i++)
            {
                if (s_allNodes[i].nodeType.ToLower() == "control")
                {
                    s_allNodes[i].previouscontrolBaseScore = "0";
                    s_allNodes[i].previouscontrolAssessedScore = "0";
                    s_allNodes[i].previousCalculatedValue = "0";
                }
            }
        }

        public static void ZeroAllAssetNodes()
        {
            for (int i = 0; i < s_allNodes.Count; i++)
            {
                if (s_allNodes[i].nodeType.ToLower() == "asset")
                {
                    s_allNodes[i].controlBaseScore = "0";
                    s_allNodes[i].calculatedValue = "0";
                    s_allNodes[i].previouscontrolBaseScore = "0";
                    s_allNodes[i].previousCalculatedValue = "0";
                    s_allNodes[i].previousControlAssessedValue = "0";
                }
            }
        }

        public static void ZeroAllObjectiveNodes()
        {
            for (int i = 0; i < s_allNodes.Count; i++)
            {
                if (s_allNodes[i].nodeType.ToLower() == "objective")
                {
                    if (s_allNodes[i].objectiveTargetType != "Manually set")
                    {
                        s_allNodes[i].controlBaseScore = "0";
                    }

                    s_allNodes[i].calculatedValue = "0";
                    s_allNodes[i].previouscontrolBaseScore = "0";
                    s_allNodes[i].previousCalculatedValue = "0";
                    s_allNodes[i].previousControlAssessedValue = "0";
                }
            }
        }

        public static void zeroNodePreviousValues(string node_id)
        {
            for (int i = 0; i < s_allNodes.Count; i++)
            {
                if (s_allNodes[i].nodeID == node_id)
                {
                    s_allNodes[i].previouscontrolBaseScore = "0";
                    s_allNodes[i].previousCalculatedValue = "0";
                    s_allNodes[i].previousControlAssessedValue = "0";
                    break;
                }
            }
        }

        public static void UpdateDistributionsForAllNodes()
        {
            for (int i = 0; i < s_allNodes.Count; i++)
            {
                switch (s_allNodes[i].nodeType.ToLower())
                {
                    case "control":
                        AddToDistributionData(s_allNodes[i].nodeID + ":Base", (int)GetNodeBaseScore(s_allNodes[i].nodeID));
                        AddToDistributionData(s_allNodes[i].nodeID + ":Assessed", (int)GetControlNodeAssessedScore(s_allNodes[i].nodeID));
                        AddToDistributionData(s_allNodes[i].nodeID + ":Calculated", (int)GetNodeCalculatedValue(s_allNodes[i].nodeID));
                        break;
                    case "asset":
                    case "asset-group":
                        AddToDistributionData(s_allNodes[i].nodeID, (int)GetAssetNodeScore(s_allNodes[i].nodeID));
                        AddToDistributionData(s_allNodes[i].nodeID + ":Mitigated", (int)GetAssetNodeMitigatedScore(s_allNodes[i].nodeID));
                        AddToDistributionData(s_allNodes[i].nodeID + ":Impact", (int)GetAssetNodeImpactScore(s_allNodes[i].nodeID));
                        AddToDistributionData(s_allNodes[i].nodeID + ":AssetConfidentialityMitigatedDistribution", (int)GetAssetNodeConfidentialityMitigatedValue(s_allNodes[i].nodeID));
                        AddToDistributionData(s_allNodes[i].nodeID + ":ConfidentialityRaw", (int)GetAssetConfidentialityProbabilityValue(s_allNodes[i].nodeID));
                        AddToDistributionData(s_allNodes[i].nodeID + ":AssetIntegrityMitigatedDistribution", (int)GetAssetNodeIntegrityMitigatedScore(s_allNodes[i].nodeID));
                        AddToDistributionData(s_allNodes[i].nodeID + ":IntegrityRaw", (int)GetAssetNodeIntegrityProbabilityValue(s_allNodes[i].nodeID));
                        AddToDistributionData(s_allNodes[i].nodeID + ":AssetAvailabilityMitigatedDistribution", (int)GetAssetNodeAvailabilityMitigatedScore(s_allNodes[i].nodeID));
                        AddToDistributionData(s_allNodes[i].nodeID + ":AvailabilityRaw", (int)GetAssetNodeAvailabilityProbabilityValue(s_allNodes[i].nodeID));
                        AddToDistributionData(s_allNodes[i].nodeID + ":AssetAccountabilityMitigatedDistribution", (int)GetAssetNodeAccountabilityMitigatedScore(s_allNodes[i].nodeID));
                        AddToDistributionData(s_allNodes[i].nodeID + ":AccountabilityRaw", (int)GetAssetNodeAccountabilityProbabilityValue(s_allNodes[i].nodeID));
                        AddToDistributionData(s_allNodes[i].nodeID + ":AssetLikelihoodScore", (int)GetAssetNodeLikelihoodScore(s_allNodes[i].nodeID));

                        break;
                    case "attack":
                        AddToDistributionData(s_allNodes[i].nodeID, (int)GetAttackNodeScore(s_allNodes[i].nodeID));
                        AddToDistributionData(s_allNodes[i].nodeID + ":Mitigated", (int)GetAttackNodeMitigatedScore(s_allNodes[i].nodeID));
                        AddToDistributionData(s_allNodes[i].nodeID + ":Threat", (int)GetAttackNodeThreatScore(s_allNodes[i].nodeID));
                        break;
                    case "objective":
                        AddToDistributionData(s_allNodes[i].nodeID + ":Target", (int)GetNodeObjectiveTargetValue(s_allNodes[i].nodeID));
                        AddToDistributionData(s_allNodes[i].nodeID + ":Achieved", (int)GetNodeCalculatedValue(s_allNodes[i].nodeID));
                        break;
                    case "actor":
                        AddToDistributionData(s_allNodes[i].nodeID, (int)GetActorNodeScore(s_allNodes[i].nodeID));
                        AddToDistributionData(s_allNodes[i].nodeID + ":Mitigated", (int)GetActorNodeMitigatedScore(s_allNodes[i].nodeID));
                        break;
                    case "vulnerability":
                        AddToDistributionData(s_allNodes[i].nodeID, (int)GetVulnerabilityNodeScore(s_allNodes[i].nodeID)); ;
                        AddToDistributionData(s_allNodes[i].nodeID + ":Mitigated", (int)GetVulnerabilityNodeMitigatedScore(s_allNodes[i].nodeID));
                        AddToDistributionData(s_allNodes[i].nodeID + ":Likelihood", (int)GetVulnerabilityNodeLikelihoodScore(s_allNodes[i].nodeID));
                        break;


                }
            }
        }




        public static LocalNode GetNodeJson(string node_id)
        {
            LocalNode obj = new LocalNode();
            foreach (LocalNode node in s_allNodes)
            {
                if (node.nodeID == node_id)
                {
                    obj = node;
                    break;
                }
            }
            return obj;
        }

        public static LocalEdge GetEdgeJson(string edge_id)
        {
            LocalEdge obj = new LocalEdge();
            foreach (LocalEdge edge in s_allEdges)
            {
                if (edge.edgeID == edge_id)
                {
                    obj = edge;
                    break;
                }
            }
            return obj;
        }

        public static void AddNodeWithObject(JObject obj)
        {
            LocalNode node = new LocalNode();
            node.title = obj["title"].ToString();
            node.nodeID = obj["id"].ToString();
            node.controlBaseScore = obj["controlBaseScore"] == null ? "" : obj["controlBaseScore"].ToString();
            node.calculatedValue = obj["calculatedValue"] == null ? "" : obj["calculatedValue"].ToString();
            node.controlAssessedScore = obj["controlAssessedScore"] == null ? "" : obj["controlAssessedScore"].ToString();
            node.previouscontrolBaseScore = obj["controlBaseScore"] == null ? "" : obj["controlBaseScore"].ToString();
            node.previousCalculatedValue = obj["calculatedValue"] == null ? "" : obj["calculatedValue"].ToString();
            node.previousControlAssessedValue = obj["edgeStrengthValue"] == null ? "" : obj["edgeStrengthValue"].ToString();
            node.enabled = obj["enabled"] == null ? true : obj["enabled"].ToString() == "true";
            node.objectiveTargetType = obj["objectiveTargetType"] == null ? "" : obj["objectiveTargetType"].ToString();
            node.objectiveTargetValue = obj["objectiveTargetValue"] == null ? "" : obj["objectiveTargetValue"].ToString();
            node.implementedStrength = obj["implementedStrength"] == null ? "" : obj["implementedStrength"].ToString();
            node.nodeType = obj["controltype"] == null ? "" : obj["controltype"].ToString();
            node.nodeBehaviour = obj["nodeBehaviour"] == null ? "" : obj["nodeBehaviour"].ToString();
            AddNode(node);

        }

        public static void AddRelationshipWithObject(JObject obj)
        {
            LocalRelationship rel = new LocalRelationship();
            rel.sourceNodeID = obj["source"] == null ? "" : obj["source"].ToString();
            rel.targetNodeID = obj["target"] == null ? "" : obj["target"].ToString();
            rel.edgeID = obj["edge_id"] == null ? "" : obj["edge_id"].ToString();
            AddRelationship(rel);
        }

        public static void DeleteEdgeWithID(string edge_id)
        {
            //remove Edge
            LocalEdge objEdge = new LocalEdge();
            foreach (LocalEdge edge in s_allEdges)
            {
                if (edge.edgeID == edge_id)
                {
                    s_allEdges.Remove(edge);
                    break;
                }
            }

            //remove Relationship
            LocalRelationship objRelationship = new LocalRelationship();
            foreach (LocalRelationship relationship in s_allRelationships)
            {
                if (relationship.edgeID == edge_id)
                {
                    s_allRelationships.Remove(relationship);
                    break;
                }
            }
        }

        public static void DeleteNodeWithID(string node_id)
        {
            LocalNode obj = new LocalNode();
            foreach (LocalNode node in s_allNodes)
            {
                if (node.nodeID == node_id)
                {
                    s_allNodes.Remove(node);
                    break;
                }
            }
        }

        public static string LastEdgeID()
        {
            if (s_allEdges == null || s_allEdges.Count < 1)
            {
                return "";
            }

            return s_allEdges[s_allEdges.Count - 1].edgeID;
        }

        public static string LastNodeID()
        {
            if (s_allNodes == null || s_allNodes.Count < 1)
            {
                return "";
            }

            return s_allNodes[s_allNodes.Count - 1].nodeID;
        }


        public static void CloneNode(string node_id)
        {
            LocalNode obj = new LocalNode();
            foreach (LocalNode node in s_allNodes)
            {
                if (node.nodeID == node_id)
                {
                    s_allNodes.Add(node);
                    break;
                }
            }
        }

        public static int GetRandomInteger(int Lower, int Upper)
        {

            int maxValueExclusive = Upper + 1;
            int randomValue = random.Next(Lower, maxValueExclusive);
            return randomValue;

        }

        public static object GetIncommingLikelihoodDistribution(string nodeID)
        {
            List<string> NodeGUIDs = GetNodeIngoerNodes(nodeID);
            ChartPointIndexer tempDistribution;
            ChartSeries tempSeries = new ChartSeries();
            ChartPointIndexer sumDistribution = new ChartPointIndexer(tempSeries);
            for (int i = 0; i < 101; i++) { sumDistribution.Add(i, 0); } // Create empty distribution 

            // process each NodeGUIDs
            foreach (string GUID in NodeGUIDs)  // Process each incomming node
            {
                string nodeType = GetNodeType(GUID).ToLower();
                if (nodeType == "vulnerability")
                {
                    tempDistribution = (ChartPointIndexer)GraphUtil.GetDistributionData(GUID + ":Likelihood");
                    if (tempDistribution != null)
                    {
                        for (int i = 0; i < 101; i++)
                        {
                            int tempValue = (int)tempDistribution[i].YValues[0];
                            tempValue += (int)sumDistribution[i].YValues[0];
                            sumDistribution[i].YValues[0] = tempValue;
                        }
                        return sumDistribution;
                    }
                    return null;
                }
                if (nodeType == "asset")
                {
                    tempDistribution = (ChartPointIndexer)GraphUtil.GetDistributionData(GUID + ":AssetLikelihoodScore");
                    if (tempDistribution != null)
                    {
                        for (int i = 0; i < 101; i++)
                        {
                            int tempValue = (int)tempDistribution[i].YValues[0];
                            tempValue += (int)sumDistribution[i].YValues[0];
                            sumDistribution[i].YValues[0] = tempValue;
                        }
                        return sumDistribution;
                    }
                    return null;
                }
                return null;
            }
            return null;
        }

        public static double GetRandomValueFromDistribution(double MaxValue, double MinValue, JArray Distribution)
        {
            object currentDistribution;
            Type typeDistribution;
            Type typeGenerator;
            SortedList<string, Type> distributions;
            SortedList<string, Type> generators;
            double Mu = 0.00;
            double Alpha = 0.00;
            double Beta = 100;
            int StandardDev = 10;
            int Gamma = 10;


            // Extract Distribution
            DistributionObject DistsObject = new DistributionObject();

            try
            {
                if (Distribution != null && Distribution.Count > 0)
                {
                    if (Distribution[0] != null)
                        DistsObject.DistributionName = Distribution[0].ToString();
                    if (Distribution[1] != null)
                        DistsObject.Value1 = Distribution[1].ToString();
                    if (Distribution[2] != null)
                        DistsObject.Value2 = Distribution[2].ToString();
                    if (Distribution[3] != null)
                        DistsObject.Value3 = Distribution[3].ToString();
                }
                else
                    return 0;
            }
            catch { return 0; }


            if (MaxValue <= MinValue && DistsObject.DistributionName != "SpecificValue")
                return 0;

            try
            {
                if (DistsObject.DistributionName != null)
                {
                    if (DistsObject.DistributionName == "DistNormal")
                    {
                        Mu = double.Parse(DistsObject.Value1);
                        StandardDev = int.Parse(DistsObject.Value2);
                    }
                    else if (DistsObject.DistributionName == "DistUniform")
                    {
                        Alpha = double.Parse(DistsObject.Value2);
                        Beta = double.Parse(DistsObject.Value1);
                    }
                    else if (DistsObject.DistributionName == "DistTriangle")
                    {
                        Beta = int.Parse(DistsObject.Value1);
                        Alpha = int.Parse(DistsObject.Value2);
                        Gamma = int.Parse(DistsObject.Value3);
                    }
                    else if (DistsObject.DistributionName == "SpecificValue")
                    {
                        return double.Parse(DistsObject.Value1);

                    }
                }
                else
                    return 0;
            }
            catch { return 0; }

            try
            {
                // Setup the distribution 
                Assembly assembly = Assembly.LoadFrom("Troschuetz.Random.dll");
                Type[] types = assembly.GetTypes();

                distributions = new SortedList<string, Type>(types.Length);
                generators = new SortedList<string, Type>(types.Length);

                for (int index = 0; index < types.Length; index++)
                {
                    if (types[index].FullName == "Troschuetz.Random.Distribution")
                    {
                        typeDistribution = types[index];
                    }
                    else if (types[index].FullName == "Troschuetz.Random.Generator")
                    {
                        typeGenerator = types[index];
                    }
                    else if (types[index].IsSubclassOf(typeof(Distribution)))
                    {// The type inherits from Distribution type.
                        distributions.Add(types[index].Name, types[index]);
                    }
                    else if (types[index].IsSubclassOf(typeof(Generator)))
                    {// The type inherits from Generator type.
                        generators.Add(types[index].Name, types[index]);
                    }
                }
            }
            catch { return 0; }

            distributions.TrimExcess();
            generators.TrimExcess();

            currentDistribution = null;

            if (DistsObject.DistributionName == "DistNormal")  // Normal Distribution 
            {

                currentDistribution = Activator.CreateInstance(distributions["NormalDistribution"]);

                PropertyInfo propertyInfo = currentDistribution.GetType().GetProperty("Mu");
                propertyInfo.SetValue(currentDistribution, (double)Mu, null);

                propertyInfo = currentDistribution.GetType().GetProperty("Sigma");
                propertyInfo.SetValue(currentDistribution, (double)StandardDev, null);

            }

            if (DistsObject.DistributionName == "DistUniform") //Uniform Distribution
            {
                currentDistribution = Activator.CreateInstance(distributions["ContinuousUniformDistribution"]);

                PropertyInfo propertyInfo = currentDistribution.GetType().GetProperty("Beta");
                propertyInfo.SetValue(currentDistribution, (Int32)Beta, null);

                propertyInfo = currentDistribution.GetType().GetProperty("Alpha");
                propertyInfo.SetValue(currentDistribution, (Int32)Alpha, null);


            }

            if (DistsObject.DistributionName == "DistTriangle") //Traiangle Distribution
            {
                currentDistribution = Activator.CreateInstance(distributions["TriangularDistribution"]);

                PropertyInfo propertyInfo = currentDistribution.GetType().GetProperty("Beta");
                propertyInfo.SetValue(currentDistribution, (Int32)Beta, null);

                propertyInfo = currentDistribution.GetType().GetProperty("Gamma");
                propertyInfo.SetValue(currentDistribution, (Int32)Gamma, null);

                propertyInfo = currentDistribution.GetType().GetProperty("Alpha");
                propertyInfo.SetValue(currentDistribution, (Int32)Alpha, null);
            }


            //Generate the samples

            Distribution distribution = (Distribution)currentDistribution;
            double[] samples = new double[10000];
            try
            {
                for (int index = 0; index < samples.Length; index++)
                {
                    samples[index] = distribution.NextDouble();
                }
            }
            catch { }


            // Get a random value from the Sample and return it

            if (MinValue > 1 && MaxValue > 1)
            {
                //Node
                try
                {
                    double tempVal = -1;
                    //while (tempVal < MinValue || tempVal > MaxValue) tempVal = samples[GetRandomInteger(0, samples.Length)];
                    tempVal = samples[GetRandomInteger(0, samples.Length)];
                    if (tempVal < 0) tempVal = 0;
                    return tempVal;
                }
                catch { return 0; }
            }
            else
            {
                //Edge
                try
                {
                    double tempVal = -1;
                    // while (tempVal / 100 < MinValue || tempVal / 100 > MaxValue) tempVal = samples[GetRandomInteger(0, samples.Length)];
                    tempVal = samples[GetRandomInteger(0, samples.Length)];
                    if (tempVal < 0) tempVal = 0;
                    return tempVal;
                }
                catch { return 0; }
            }
        }

        public static string GetDistributionType(JArray Distribution)
        {
            // Extract Distribution
            DistributionObject DistsObject = new DistributionObject();

            try
            {
                if (Distribution != null && Distribution.Count > 0)
                {
                    if (Distribution[0] != null)
                        DistsObject.DistributionName = Distribution[0].ToString();
                }
                else
                    return string.Empty;
            }
            catch { return string.Empty; }

            try
            {
                if (DistsObject.DistributionName != null)
                {
                    if (DistsObject.DistributionName == "DistNormal")
                    {
                        return "Distribution: Normal";
                    }
                    else if (DistsObject.DistributionName == "DistUniform")
                    {
                        return "Distribution: Uniform";
                    }
                    else if (DistsObject.DistributionName == "DistTriangle")
                    {
                        return "Distribution: Triangle";
                    }
                    else if (DistsObject.DistributionName == "SpecificValue")
                    {
                        return "Specific Value";

                    }
                }
                else
                    return string.Empty;
            }
            catch { return string.Empty; }
            return string.Empty;
        }


        public static void CalculateActorScoreForAllNodes()
        {
            Double actorScore = 0;

            for (int i = 0; i < s_allNodes.Count; i++)
            {

                if (s_allNodes[i].nodeType.ToLower() == "actor")
                {
                    if (GraphCalcs.useCalcLog) GraphCalcs.s_CalculationLog.Add((DateTime.Now, $"Processing Actor Node: {s_allNodes[i].nodeID}"));
                    // Get Values
                    double actorCapabilityMaxValue = ParseDoubleOrDefault(s_allNodes[i].actorCapabilityValue);
                    if (GraphCalcs.useCalcLog) GraphCalcs.s_CalculationLog.Add((DateTime.Now, $"actorCapabilityMaxValue: {actorCapabilityMaxValue.ToString()}"));
                    double actorCapabilityMinValue = ParseDoubleOrDefault(s_allNodes[i].actorCapabilityMinValue);
                    if (GraphCalcs.useCalcLog) GraphCalcs.s_CalculationLog.Add((DateTime.Now, $"actorCapabilityMinValue: {actorCapabilityMinValue.ToString()}"));
                    double actorResourcesMaxValue = ParseDoubleOrDefault(s_allNodes[i].actorResourcesValue);
                    if (GraphCalcs.useCalcLog) GraphCalcs.s_CalculationLog.Add((DateTime.Now, $"actorResourcesMaxValue: {actorResourcesMaxValue.ToString()}"));
                    double actorResourcesMinValue = ParseDoubleOrDefault(s_allNodes[i].actorResourcesMinValue);
                    if (GraphCalcs.useCalcLog) GraphCalcs.s_CalculationLog.Add((DateTime.Now, $"actorResourcesMinValue: {actorResourcesMinValue.ToString()}"));
                    double actorMotivationMaxValue = ParseDoubleOrDefault(s_allNodes[i].actorMotivationValue);
                    if (GraphCalcs.useCalcLog) GraphCalcs.s_CalculationLog.Add((DateTime.Now, $"actorMotivationMaxValue: {actorMotivationMaxValue.ToString()}"));
                    double actorMotivationMinValue = ParseDoubleOrDefault(s_allNodes[i].actorMotivationMinValue);
                    if (GraphCalcs.useCalcLog) GraphCalcs.s_CalculationLog.Add((DateTime.Now, $"actorMotivationMinValue: {actorMotivationMinValue.ToString()}"));
                    double actorAccessMaxValue = ParseDoubleOrDefault(s_allNodes[i].actorAccessValue);
                    if (GraphCalcs.useCalcLog) GraphCalcs.s_CalculationLog.Add((DateTime.Now, $"actorAccessMaxValue: {actorAccessMaxValue.ToString()}"));
                    double actorAccessMinValue = ParseDoubleOrDefault(s_allNodes[i].actorAccessMinValue);
                    if (GraphCalcs.useCalcLog) GraphCalcs.s_CalculationLog.Add((DateTime.Now, $"actorAccessMinValue: {actorAccessMinValue.ToString()}"));


                    // Get Distributions
                    JArray actorCapabilityDistribution = DeserializeJArrayOrDefault(s_allNodes[i].actorCapabilityDistribution);
                    if (GraphCalcs.useCalcLog) GraphCalcs.s_CalculationLog.Add((DateTime.Now, $"actorCapabilityDistribution: {s_allNodes[i].actorCapabilityDistribution}"));
                    JArray actorResourcesDistribution = DeserializeJArrayOrDefault(s_allNodes[i].actorResourcesDistribution);
                    if (GraphCalcs.useCalcLog) GraphCalcs.s_CalculationLog.Add((DateTime.Now, $"actorResourcesDistribution: {s_allNodes[i].actorResourcesDistribution}"));
                    JArray actorMotivationDistribution = DeserializeJArrayOrDefault(s_allNodes[i].actorMotivationDistribution);
                    if (GraphCalcs.useCalcLog) GraphCalcs.s_CalculationLog.Add((DateTime.Now, $"actorMotivationDistribution: {s_allNodes[i].actorMotivationDistribution}"));
                    JArray actorAccessDistribution = DeserializeJArrayOrDefault(s_allNodes[i].actorAccessDistribution);
                    if (GraphCalcs.useCalcLog) GraphCalcs.s_CalculationLog.Add((DateTime.Now, $"actorAccessDistribution: {s_allNodes[i].actorAccessDistribution}"));


                    //Get the probability values
                    double actorCapabilityProbabilityValue = GetRandomValueFromDistribution(actorCapabilityMaxValue, actorCapabilityMinValue, actorCapabilityDistribution);
                    if (GraphCalcs.useCalcLog) GraphCalcs.s_CalculationLog.Add((DateTime.Now, $"actorCapabilityProbabilityValue: {actorCapabilityProbabilityValue.ToString()}"));
                    double actorResourcesProbabilityValue = GetRandomValueFromDistribution(actorResourcesMaxValue, actorResourcesMinValue, actorResourcesDistribution);
                    if (GraphCalcs.useCalcLog) GraphCalcs.s_CalculationLog.Add((DateTime.Now, $"actorResourcesProbabilityValue: {actorResourcesProbabilityValue.ToString()}"));
                    double actorMotivationProbabilityValue = GetRandomValueFromDistribution(actorMotivationMaxValue, actorMotivationMinValue, actorMotivationDistribution);
                    if (GraphCalcs.useCalcLog) GraphCalcs.s_CalculationLog.Add((DateTime.Now, $"actorMotivationProbabilityValue: {actorMotivationProbabilityValue.ToString()}"));
                    double actorAccessProbabilityValue = GetRandomValueFromDistribution(actorAccessMaxValue, actorAccessMinValue, actorAccessDistribution);
                    if (GraphCalcs.useCalcLog) GraphCalcs.s_CalculationLog.Add((DateTime.Now, $"actorAccessProbabilityValue: {actorAccessProbabilityValue.ToString()}"));


                    //Calaculate the result
                    actorScore = (actorCapabilityProbabilityValue + actorResourcesProbabilityValue + actorMotivationProbabilityValue + actorAccessProbabilityValue) / 4;
                    if (GraphCalcs.useCalcLog) GraphCalcs.s_CalculationLog.Add((DateTime.Now, $"actorScore: {actorScore.ToString()}"));

                    s_allNodes[i].actorScore = ClampNodeScore(actorScore).ToString();
                    GraphUtil.AddToNodeScores(s_allNodes[i].nodeID, actorScore);


                }
            }
        }


        public static void CalculateVulnerabilityScoreForNode(string node_id)
        {
            Double vulnerabilityScore = 0;
            Double vulnerabilityEaseOfExploitation = 0;
            Double vulnerabilityExposure = 0;

            for (int i = 0; i < s_allNodes.Count; i++)
            {

                if (s_allNodes[i].nodeID == node_id)
                {
                    if (s_allNodes[i].vulnerabilityEaseOfExploitationValue != null)
                        vulnerabilityEaseOfExploitation = Double.Parse(s_allNodes[i].vulnerabilityEaseOfExploitationValue);
                    else
                        vulnerabilityEaseOfExploitation = 0;

                    if (s_allNodes[i].vulnerabilityExposureValue != null)
                        vulnerabilityExposure = Double.Parse(s_allNodes[i].vulnerabilityExposureValue);
                    else
                        vulnerabilityExposure = 0;

                    vulnerabilityScore = (vulnerabilityEaseOfExploitation + vulnerabilityExposure) / 2;
                    s_allNodes[i].vulnerabilityScore = vulnerabilityScore.ToString();

                    break;
                }
            }
        }

        public static void CalculateVulnerabilityScoreForAllNodes()
        {
            for (int i = 0; i < s_allNodes.Count; i++)
            {

                if (s_allNodes[i].nodeType.ToLower() == "vulnerability")
                {
                    if (GraphCalcs.useCalcLog) GraphCalcs.s_CalculationLog.Add((DateTime.Now, $"Processing Vulnerability Node: {s_allNodes[i].nodeID}"));

                    // Get Values
                    double vulnerabilityEaseOfExploitationMaxValue = ParseDoubleOrDefault(s_allNodes[i].vulnerabilityEaseOfExploitationValue);
                    if (GraphCalcs.useCalcLog) GraphCalcs.s_CalculationLog.Add((DateTime.Now, $"vulnerabilityEaseOfExploitationMaxValue: {vulnerabilityEaseOfExploitationMaxValue.ToString()}"));

                    double vulnerabilityEaseOfExploitationMinValue = ParseDoubleOrDefault(s_allNodes[i].vulnerabilityEaseOfExploitationMinValue);
                    if (GraphCalcs.useCalcLog) GraphCalcs.s_CalculationLog.Add((DateTime.Now, $"vulnerabilityEaseOfExploitationMinValue: {vulnerabilityEaseOfExploitationMinValue.ToString()}"));

                    double vulnerabilityExposureMaxValue = ParseDoubleOrDefault(s_allNodes[i].vulnerabilityExposureValue);
                    if (GraphCalcs.useCalcLog) GraphCalcs.s_CalculationLog.Add((DateTime.Now, $"vulnerabilityExposureMaxValue: {vulnerabilityExposureMaxValue.ToString()}"));

                    double vulnerabilityExposureMinValue = ParseDoubleOrDefault(s_allNodes[i].vulnerabilityExposureMinValue);
                    if (GraphCalcs.useCalcLog) GraphCalcs.s_CalculationLog.Add((DateTime.Now, $"vulnerabilityExposureMinValue: {vulnerabilityExposureMinValue.ToString()}"));

                    double vulnerabilityPrivilegesRequiredMaxValue = ParseDoubleOrDefault(s_allNodes[i].vulnerabilityPrivilegesRequiredValue);
                    if (GraphCalcs.useCalcLog) GraphCalcs.s_CalculationLog.Add((DateTime.Now, $"vulnerabilityPrivilegesRequiredMaxValue: {vulnerabilityPrivilegesRequiredMaxValue.ToString()}"));

                    double vulnerabilityPrivilegesRequiredMinValue = ParseDoubleOrDefault(s_allNodes[i].vulnerabilityPrivilegesRequiredMinValue);
                    if (GraphCalcs.useCalcLog) GraphCalcs.s_CalculationLog.Add((DateTime.Now, $"vulnerabilityPrivilegesRequiredMinValue: {vulnerabilityPrivilegesRequiredMinValue.ToString()}"));

                    double vulnerabilityInteractionRequiredMaxValue = ParseDoubleOrDefault(s_allNodes[i].vulnerabilityInteractionRequiredValue);
                    if (GraphCalcs.useCalcLog) GraphCalcs.s_CalculationLog.Add((DateTime.Now, $"vulnerabilityInteractionRequiredMaxValue: {vulnerabilityInteractionRequiredMaxValue.ToString()}"));

                    double vulnerabilityInteractionRequiredMinValue = ParseDoubleOrDefault(s_allNodes[i].vulnerabilityInteractionRequiredMinValue);
                    if (GraphCalcs.useCalcLog) GraphCalcs.s_CalculationLog.Add((DateTime.Now, $"vulnerabilityInteractionRequiredMinValue: {vulnerabilityInteractionRequiredMinValue.ToString()}"));

                    double vulnerabilityExposesScopeMaxValue = ParseDoubleOrDefault(s_allNodes[i].vulnerabilityExposesScopeValue);
                    if (GraphCalcs.useCalcLog) GraphCalcs.s_CalculationLog.Add((DateTime.Now, $"vulnerabilityExposesScopeMaxValue: {vulnerabilityExposesScopeMaxValue.ToString()}"));

                    double vulnerabilityExposesScopeMinValue = ParseDoubleOrDefault(s_allNodes[i].vulnerabilityExposesScopeMinValue);
                    if (GraphCalcs.useCalcLog) GraphCalcs.s_CalculationLog.Add((DateTime.Now, $"vulnerabilityExposesScopeMinValue: {vulnerabilityExposesScopeMinValue.ToString()}"));

                    // Get Distributions
                    JArray vulnerabilityEaseOfExploitationDistribution = DeserializeJArrayOrDefault(s_allNodes[i].vulnerabilityEaseOfExploitationDistribution);
                    if (GraphCalcs.useCalcLog) GraphCalcs.s_CalculationLog.Add((DateTime.Now, $"vulnerabilityEaseOfExploitationDistribution: {s_allNodes[i].vulnerabilityEaseOfExploitationDistribution}"));
                    JArray vulnerabilityExposureDistribution = DeserializeJArrayOrDefault(s_allNodes[i].vulnerabilityExposureDistribution);
                    if (GraphCalcs.useCalcLog) GraphCalcs.s_CalculationLog.Add((DateTime.Now, $"vulnerabilityExposureDistribution: {s_allNodes[i].vulnerabilityExposureDistribution}"));
                    JArray vulnerabilityPrivilegesRequiredDistribution = DeserializeJArrayOrDefault(s_allNodes[i].vulnerabilityPrivilegesRequiredDistribution);
                    if (GraphCalcs.useCalcLog) GraphCalcs.s_CalculationLog.Add((DateTime.Now, $"vulnerabilityPrivilegesRequiredDistribution: {s_allNodes[i].vulnerabilityPrivilegesRequiredDistribution}"));
                    JArray vulnerabilityInteractionRequiredDistribution = DeserializeJArrayOrDefault(s_allNodes[i].vulnerabilityInteractionRequiredDistribution);
                    if (GraphCalcs.useCalcLog) GraphCalcs.s_CalculationLog.Add((DateTime.Now, $"vulnerabilityInteractionRequiredDistribution: {s_allNodes[i].vulnerabilityInteractionRequiredDistribution}"));
                    JArray vulnerabilityExposesScopeDistribution = DeserializeJArrayOrDefault(s_allNodes[i].vulnerabilityExposesScopeDistribution);
                    if (GraphCalcs.useCalcLog) GraphCalcs.s_CalculationLog.Add((DateTime.Now, $"vulnerabilityExposesScopeDistribution: {s_allNodes[i].vulnerabilityExposesScopeDistribution}"));

                    // Get the probability values
                    double vulnerabilityEaseOfExploitationProbabilityValue = GetRandomValueFromDistribution(vulnerabilityEaseOfExploitationMaxValue, vulnerabilityEaseOfExploitationMinValue, vulnerabilityEaseOfExploitationDistribution);
                    if (GraphCalcs.useCalcLog) GraphCalcs.s_CalculationLog.Add((DateTime.Now, $"vulnerabilityEaseOfExploitationProbabilityValue: {vulnerabilityEaseOfExploitationProbabilityValue.ToString()}"));
                    double vulnerabilityExposureProbabilityValue = GetRandomValueFromDistribution(vulnerabilityExposureMaxValue, vulnerabilityExposureMinValue, vulnerabilityExposureDistribution);
                    if (GraphCalcs.useCalcLog) GraphCalcs.s_CalculationLog.Add((DateTime.Now, $"vulnerabilityExposureProbabilityValue: {vulnerabilityExposureProbabilityValue.ToString()}"));
                    double vulnerabilityPrivilegesRequiredProbabilityValue = GetRandomValueFromDistribution(vulnerabilityPrivilegesRequiredMaxValue, vulnerabilityPrivilegesRequiredMinValue, vulnerabilityPrivilegesRequiredDistribution);
                    if (GraphCalcs.useCalcLog) GraphCalcs.s_CalculationLog.Add((DateTime.Now, $"vulnerabilityPrivilegesRequiredProbabilityValue: {vulnerabilityPrivilegesRequiredProbabilityValue.ToString()}"));
                    double vulnerabilityInteractionRequiredProbabilityValue = GetRandomValueFromDistribution(vulnerabilityInteractionRequiredMaxValue, vulnerabilityInteractionRequiredMinValue, vulnerabilityInteractionRequiredDistribution);
                    if (GraphCalcs.useCalcLog) GraphCalcs.s_CalculationLog.Add((DateTime.Now, $"vulnerabilityInteractionRequiredProbabilityValue: {vulnerabilityInteractionRequiredProbabilityValue.ToString()}"));
                    double vulnerabilityExposesScopeProbabilityValue = GetRandomValueFromDistribution(vulnerabilityExposesScopeMaxValue, vulnerabilityExposesScopeMinValue, vulnerabilityExposesScopeDistribution);
                    if (GraphCalcs.useCalcLog) GraphCalcs.s_CalculationLog.Add((DateTime.Now, $"vulnerabilityExposesScopeProbabilityValue: {vulnerabilityExposesScopeProbabilityValue.ToString()}"));

                    double vulnerabilityScore = (vulnerabilityEaseOfExploitationProbabilityValue + vulnerabilityExposureProbabilityValue + vulnerabilityPrivilegesRequiredProbabilityValue + vulnerabilityInteractionRequiredProbabilityValue + vulnerabilityExposesScopeProbabilityValue) / 5;
                    if (GraphCalcs.useCalcLog) GraphCalcs.s_CalculationLog.Add((DateTime.Now, $"vulnerabilityScore: {vulnerabilityScore.ToString()}"));
                    s_allNodes[i].vulnerabilityScore = ClampNodeScore(vulnerabilityScore).ToString();
                    GraphUtil.AddToNodeScores(s_allNodes[i].nodeID, vulnerabilityScore);




                }
            }
        }

        public static string ConvertValueToTimeInterval(string stringValue)
        {
            if (stringValue == null || stringValue == "")
                return string.Empty;

            if (stringValue == "N/A")
                return "N/A";

            double value = 0;
            try
            {
                value = double.Parse(stringValue);
            }
            catch
            {
                return "N/A";
            }
            double frequency = 1 / (value / 100);


            if (frequency < 3)
            {
                // Convert years to months and round
                int months = (int)Math.Round(frequency * 12);
                return $"{months} months";
            }
            else
            {
                // Round years to one decimal place
                frequency = Math.Round(frequency, 1);
                return $"{frequency} years";
            }
        }

        public static void CalculateControlValuesForAllNodes()
        {
            for (int i = 0; i < s_allNodes.Count; i++)
            {

                if (s_allNodes[i].nodeType.ToLower() == "control")
                {
                    if (GraphCalcs.useCalcLog) GraphCalcs.s_CalculationLog.Add((DateTime.Now, $"Processing Control Node: {s_allNodes[i].nodeID}"));

                    // Get Values
                    double controlBaseMaxValue = ParseDoubleOrDefault(s_allNodes[i].controlBaseValue);
                    if (GraphCalcs.useCalcLog) GraphCalcs.s_CalculationLog.Add((DateTime.Now, $"controlBaseMaxValue: {controlBaseMaxValue.ToString()}"));
                    double controlBaseMinValue = ParseDoubleOrDefault(s_allNodes[i].controlBaseMinValue);
                    if (GraphCalcs.useCalcLog) GraphCalcs.s_CalculationLog.Add((DateTime.Now, $"controlBaseMinValue: {controlBaseMinValue.ToString()}"));

                    double controlAssessedMaxValue = ParseDoubleOrDefault(s_allNodes[i].controlAssessedValue);
                    if (GraphCalcs.useCalcLog) GraphCalcs.s_CalculationLog.Add((DateTime.Now, $"controlAssessedMaxValue: {controlAssessedMaxValue.ToString()}"));
                    double controlAssessedMinValue = ParseDoubleOrDefault(s_allNodes[i].controlAssessedMinValue);
                    if (GraphCalcs.useCalcLog) GraphCalcs.s_CalculationLog.Add((DateTime.Now, $"controlAssessedMinValue: {controlAssessedMinValue.ToString()}"));

                    // Get Distributions
                    JArray controlBaseDistribution = DeserializeJArrayOrDefault(s_allNodes[i].controlBaseDistribution);
                    if (GraphCalcs.useCalcLog) GraphCalcs.s_CalculationLog.Add((DateTime.Now, $"controlBaseDistribution: {s_allNodes[i].controlBaseDistribution}"));
                    JArray controlAssessedDistribution = DeserializeJArrayOrDefault(s_allNodes[i].controlAssessedDistribution);
                    if (GraphCalcs.useCalcLog) GraphCalcs.s_CalculationLog.Add((DateTime.Now, $"controlAssessedDistribution: {s_allNodes[i].controlAssessedDistribution}"));


                    // Get the probability values
                    double controlBaseProbabilityValue = GetRandomValueFromDistribution(controlBaseMaxValue, controlBaseMinValue, controlBaseDistribution);
                    if (GraphCalcs.useCalcLog) GraphCalcs.s_CalculationLog.Add((DateTime.Now, $"controlBaseProbabilityValue: {controlBaseProbabilityValue.ToString()}"));
                    double controlAssessedProbabilityValue = GetRandomValueFromDistribution(controlAssessedMaxValue, controlAssessedMinValue, controlAssessedDistribution);
                    if (GraphCalcs.useCalcLog) GraphCalcs.s_CalculationLog.Add((DateTime.Now, $"controlAssessedProbabilityValue: {controlAssessedProbabilityValue.ToString()}"));

                    s_allNodes[i].controlBaseScore = ClampNodeScore(controlBaseProbabilityValue).ToString();
                    s_allNodes[i].controlAssessedScore = ClampNodeScore(controlAssessedProbabilityValue).ToString();
                    GraphUtil.AddToNodeScores(s_allNodes[i].nodeID + ":ControlBaseScore", controlBaseProbabilityValue);
                    GraphUtil.AddToNodeScores(s_allNodes[i].nodeID + ":ControlAssessedScore", controlAssessedProbabilityValue);


                }
            }
        }

        public static void CalculateEdgeStrengthValuesForAllEdges()
        {
            for (int i = 0; i < s_allEdges.Count; i++)
            {
                if (GraphCalcs.useCalcLog) GraphCalcs.s_CalculationLog.Add((DateTime.Now, $"Processing Edge: {s_allEdges[i].edgeID}"));
                // Get Values
                double edgeStrengthMaxValue = ParseDoubleOrDefault(s_allEdges[i].edgeStrengthValue);
                if (GraphCalcs.useCalcLog) GraphCalcs.s_CalculationLog.Add((DateTime.Now, $"edgeStrengthMaxValue: {edgeStrengthMaxValue.ToString()}"));
                double edgeStrengthMinValue = ParseDoubleOrDefault(s_allEdges[i].edgeStrengthMinValue);
                if (GraphCalcs.useCalcLog) GraphCalcs.s_CalculationLog.Add((DateTime.Now, $"edgeStrengthMinValue: {edgeStrengthMinValue.ToString()}"));


                // Get Distribution
                JArray edgeDistribution = DeserializeJArrayOrDefault(s_allEdges[i].edgeStrengthDistribution);
                if (GraphCalcs.useCalcLog) GraphCalcs.s_CalculationLog.Add((DateTime.Now, $"edgeStrengthDistribution: {s_allEdges[i].edgeStrengthDistribution}"));

                // Get the probability values
                decimal edgeProbabilityValue = (decimal)GetRandomValueFromDistribution(edgeStrengthMaxValue, edgeStrengthMinValue, edgeDistribution);
                if (GraphCalcs.useCalcLog) GraphCalcs.s_CalculationLog.Add((DateTime.Now, $"edgeProbabilityValue: {edgeProbabilityValue.ToString()}"));


                if (edgeProbabilityValue > 1) // Need to do this because probablity ranges come back at 0 to 100, but edges are 0 to 1
                {
                    AddToDistributionData(s_allEdges[i].edgeID, (int)ClampNodeScore((double)edgeProbabilityValue));
                    edgeProbabilityValue = edgeProbabilityValue / 100; // Devide by 100 to get correct Edge strength value

                }
                else
                {
                    AddToDistributionData(s_allEdges[i].edgeID, (int)ClampNodeScore((double)edgeProbabilityValue * 100));
                }


                s_allEdges[i].edgeStrengthScore = ClampEdgeScore(edgeProbabilityValue).ToString();

                decimal tempValue = edgeProbabilityValue;



            }
        }


        public static void CalculateAttackScoreForAllNodes()
        {

            for (int i = 0; i < s_allNodes.Count; i++)
            {

                if (s_allNodes[i].nodeType.ToLower() == "attack")
                {
                    if (GraphCalcs.useCalcLog) GraphCalcs.s_CalculationLog.Add((DateTime.Now, $"Processing Attack Node: {s_allNodes[i].nodeID}"));
                    // Get Values
                    Double attackComplexityMaxValue = ParseDoubleOrDefault(s_allNodes[i].attackComplexityValue);
                    if (GraphCalcs.useCalcLog) GraphCalcs.s_CalculationLog.Add((DateTime.Now, $"attackComplexityMaxValue: {attackComplexityMaxValue.ToString()}"));
                    Double attackComplexityMinValue = ParseDoubleOrDefault(s_allNodes[i].attackComplexityMinValue);
                    if (GraphCalcs.useCalcLog) GraphCalcs.s_CalculationLog.Add((DateTime.Now, $"attackComplexityMinValue: {attackComplexityMinValue.ToString()}"));
                    Double attackProliferationMaxValue = ParseDoubleOrDefault(s_allNodes[i].attackProliferationValue);
                    if (GraphCalcs.useCalcLog) GraphCalcs.s_CalculationLog.Add((DateTime.Now, $"attackProliferationMaxValue: {attackProliferationMaxValue.ToString()}"));
                    Double attackProliferationMinValue = ParseDoubleOrDefault(s_allNodes[i].attackProliferationMinValue);
                    if (GraphCalcs.useCalcLog) GraphCalcs.s_CalculationLog.Add((DateTime.Now, $"attackProliferationMinValue: {attackProliferationMinValue.ToString()}"));

                    // Get Distributions
                    JArray attackComplexityDistribution = DeserializeJArrayOrDefault(s_allNodes[i].attackComplexityDistribution);
                    if (GraphCalcs.useCalcLog) GraphCalcs.s_CalculationLog.Add((DateTime.Now, $"attackComplexityDistribution: {s_allNodes[i].attackComplexityDistribution}"));
                    JArray attackProliferationDistribution = DeserializeJArrayOrDefault(s_allNodes[i].attackProliferationDistribution);
                    if (GraphCalcs.useCalcLog) GraphCalcs.s_CalculationLog.Add((DateTime.Now, $"attackProliferationDistribution: {s_allNodes[i].attackProliferationDistribution}"));

                    // Get the probability values
                    Double attackComplexityProbabilityValue = GetRandomValueFromDistribution(attackComplexityMaxValue, attackComplexityMinValue, attackComplexityDistribution);
                    if (GraphCalcs.useCalcLog) GraphCalcs.s_CalculationLog.Add((DateTime.Now, $"attackComplexityProbabilityValue: {attackComplexityProbabilityValue.ToString()}"));
                    Double attackProliferationProbabilityValue = GetRandomValueFromDistribution(attackProliferationMaxValue, attackProliferationMinValue, attackProliferationDistribution);
                    if (GraphCalcs.useCalcLog) GraphCalcs.s_CalculationLog.Add((DateTime.Now, $"attackProliferationProbabilityValue: {attackProliferationProbabilityValue.ToString()}"));

                    //// Additional round to ease issues with exessive even numbers
                    //attackComplexityProbabilityValue1 = GetRandomValueFromDistribution(attackComplexityMaxValue, attackComplexityMinValue, attackComplexityDistribution);
                    //attackProliferationProbabilityValue1 = GetRandomValueFromDistribution(attackProliferationMaxValue, attackProliferationMinValue, attackProliferationDistribution);

                    Double attackScore = (attackComplexityProbabilityValue + attackProliferationProbabilityValue) / 2;
                    attackScore = (double)ClampNodeScore(attackScore);
                    if (GraphCalcs.useCalcLog) GraphCalcs.s_CalculationLog.Add((DateTime.Now, $"attackScore: {attackScore}"));

                    s_allNodes[i].attackScore = attackScore.ToString();

                    GraphUtil.AddToNodeScores(s_allNodes[i].nodeID, attackScore);


                }
            }
        }

        public static double ClampNodeScore(double score)
        {
            if (score > 100)
                return 100;
            if (score < 0)
                return 0;
            return score;
        }

        public static decimal ClampEdgeScore(decimal score)
        {
            if (score > 1)
                return 1;
            if (score < 0)
                return 0;
            return (decimal)score;
        }

        private static double ParseDoubleOrDefault(string value)
        {
            return value != null ? Double.Parse(value) : 0;
        }

        public static JArray DeserializeJArrayOrDefault(string json)
        {
            return json != null ? (JArray)JsonConvert.DeserializeObject(json) : new JArray();
        }



        public static void CalculateAttackScoreForNode(string node_id)
        {
            Double attackScore = 0;
            Double AttackComplexity = 0;
            Double AttackProliferation = 0;

            for (int i = 0; i < s_allNodes.Count; i++)
            {

                if (s_allNodes[i].nodeID == node_id)
                {
                    if (s_allNodes[i].attackComplexityValue != null)
                        AttackComplexity = Double.Parse(s_allNodes[i].attackComplexityValue);
                    else
                        AttackComplexity = 0;

                    if (s_allNodes[i].attackProliferationValue != null)
                        AttackProliferation = Double.Parse(s_allNodes[i].attackProliferationValue);
                    else
                        AttackProliferation = 0;

                    attackScore = (AttackComplexity + AttackProliferation) / 2;
                    s_allNodes[i].attackScore = attackScore.ToString();

                    break;

                }
            }
        }

        public static void CalculateAssetScoreForNode(string node_id)
        {
            Double assetScore = 0;
            Double assetConfidentiality = 0;
            Double assetIntegrity = 0;
            Double assetAvailability = 0;
            Double assetAccountability = 0;

            for (int i = 0; i < s_allNodes.Count; i++)
            {

                if (s_allNodes[i].nodeID == node_id)
                {
                    if (s_allNodes[i].assetConfidentialityValue != null)
                        assetConfidentiality = Double.Parse(s_allNodes[i].assetConfidentialityValue);
                    else
                        assetConfidentiality = 0;

                    if (s_allNodes[i].assetIntegrityValue != null)
                        assetIntegrity = Double.Parse(s_allNodes[i].assetIntegrityValue);
                    else
                        assetIntegrity = 0;

                    if (s_allNodes[i].assetAvailabilityValue != null)
                        assetAvailability = Double.Parse(s_allNodes[i].assetAvailabilityValue);
                    else
                        assetAvailability = 0;

                    if (s_allNodes[i].assetAccountabilityValue != null)
                        assetAccountability = Double.Parse(s_allNodes[i].assetAccountabilityValue);
                    else
                        assetAccountability = 0;
                    assetScore = (assetConfidentiality + assetIntegrity + assetAvailability + assetAccountability) / 4;
                    s_allNodes[i].assetScore = assetScore.ToString();
                    break;

                }
            }
        }

        public static void CalculateAssetScoreForAllNodes()
        {

            for (int i = 0; i < s_allNodes.Count; i++)
            {

                if (s_allNodes[i].nodeType.ToLower() == "asset")
                {
                    if (GraphCalcs.useCalcLog) GraphCalcs.s_CalculationLog.Add((DateTime.Now, $"Processing Asset Node: {s_allNodes[i].nodeID}"));

                    // Get Values
                    double assetConfidentialityMaxValue = ParseDoubleOrDefault(s_allNodes[i].assetConfidentialityValue);
                    if (GraphCalcs.useCalcLog) GraphCalcs.s_CalculationLog.Add((DateTime.Now, $"assetConfidentialityMaxValue: {assetConfidentialityMaxValue.ToString()}"));
                    double assetConfidentialityMinValue = ParseDoubleOrDefault(s_allNodes[i].assetConfidentialityMinValue);
                    if (GraphCalcs.useCalcLog) GraphCalcs.s_CalculationLog.Add((DateTime.Now, $"assetConfidentialityMinValue: {assetConfidentialityMinValue.ToString()}"));

                    double assetIntegrityMaxValue = ParseDoubleOrDefault(s_allNodes[i].assetIntegrityValue);
                    if (GraphCalcs.useCalcLog) GraphCalcs.s_CalculationLog.Add((DateTime.Now, $"assetIntegrityMaxValue: {assetIntegrityMaxValue.ToString()}"));
                    double assetIntegrityMinValue = ParseDoubleOrDefault(s_allNodes[i].assetIntegrityMinValue);
                    if (GraphCalcs.useCalcLog) GraphCalcs.s_CalculationLog.Add((DateTime.Now, $"assetIntegrityMinValue: {assetIntegrityMinValue.ToString()}"));

                    double assetAvailabilityMaxValue = ParseDoubleOrDefault(s_allNodes[i].assetAvailabilityValue);
                    if (GraphCalcs.useCalcLog) GraphCalcs.s_CalculationLog.Add((DateTime.Now, $"assetAvailabilityMaxValue: {assetAvailabilityMaxValue.ToString()}"));
                    double assetAvailabilityMinValue = ParseDoubleOrDefault(s_allNodes[i].assetAvailabilityMinValue);
                    if (GraphCalcs.useCalcLog) GraphCalcs.s_CalculationLog.Add((DateTime.Now, $"assetAvailabilityMinValue: {assetAvailabilityMinValue.ToString()}"));

                    double assetAccountabilityMaxValue = ParseDoubleOrDefault(s_allNodes[i].assetAccountabilityValue);
                    if (GraphCalcs.useCalcLog) GraphCalcs.s_CalculationLog.Add((DateTime.Now, $"assetAccountabilityMaxValue: {assetAccountabilityMaxValue.ToString()}"));
                    double assetAccountabilityMinValue = ParseDoubleOrDefault(s_allNodes[i].assetAccountabilityMinValue);
                    if (GraphCalcs.useCalcLog) GraphCalcs.s_CalculationLog.Add((DateTime.Now, $"assetAccountabilityMinValue: {assetAccountabilityMinValue.ToString()}"));

                    // Get Distributions
                    JArray assetConfidentialityDistribution = DeserializeJArrayOrDefault(s_allNodes[i].assetConfidentialityDistribution);
                    if (GraphCalcs.useCalcLog) GraphCalcs.s_CalculationLog.Add((DateTime.Now, $"assetConfidentialityDistribution: {s_allNodes[i].assetConfidentialityDistribution}"));
                    JArray assetIntegrityDistribution = DeserializeJArrayOrDefault(s_allNodes[i].assetIntegrityDistribution);
                    if (GraphCalcs.useCalcLog) GraphCalcs.s_CalculationLog.Add((DateTime.Now, $"assetIntegrityDistribution: {s_allNodes[i].assetIntegrityDistribution}"));
                    JArray assetAvailabilityDistribution = DeserializeJArrayOrDefault(s_allNodes[i].assetAvailabilityDistribution);
                    if (GraphCalcs.useCalcLog) GraphCalcs.s_CalculationLog.Add((DateTime.Now, $"assetAvailabilityDistribution: {s_allNodes[i].assetAvailabilityDistribution}"));
                    JArray assetAccountabilityDistribution = DeserializeJArrayOrDefault(s_allNodes[i].assetAccountabilityDistribution);
                    if (GraphCalcs.useCalcLog) GraphCalcs.s_CalculationLog.Add((DateTime.Now, $"assetAccountabilityDistribution: {s_allNodes[i].assetAccountabilityDistribution}"));

                    // Get the probability values
                    double assetConfidentialityProbabilityValue = GetRandomValueFromDistribution(assetConfidentialityMaxValue, assetConfidentialityMinValue, assetConfidentialityDistribution);
                    if (GraphCalcs.useCalcLog) GraphCalcs.s_CalculationLog.Add((DateTime.Now, $"assetConfidentialityProbabilityValue: {assetConfidentialityProbabilityValue.ToString()}"));
                    double assetIntegrityProbabilityValue = GetRandomValueFromDistribution(assetIntegrityMaxValue, assetIntegrityMinValue, assetIntegrityDistribution);
                    if (GraphCalcs.useCalcLog) GraphCalcs.s_CalculationLog.Add((DateTime.Now, $"assetIntegrityProbabilityValue: {assetIntegrityProbabilityValue.ToString()}"));
                    double assetAvailabilityProbabilityValue = GetRandomValueFromDistribution(assetAvailabilityMaxValue, assetAvailabilityMinValue, assetAvailabilityDistribution);
                    if (GraphCalcs.useCalcLog) GraphCalcs.s_CalculationLog.Add((DateTime.Now, $"assetAvailabilityProbabilityValue: {assetAvailabilityProbabilityValue.ToString()}"));
                    double assetAccountabilityProbabilityValue = GetRandomValueFromDistribution(assetAccountabilityMaxValue, assetAccountabilityMinValue, assetAccountabilityDistribution);
                    if (GraphCalcs.useCalcLog) GraphCalcs.s_CalculationLog.Add((DateTime.Now, $"assetAccountabilityProbabilityValue: {assetAccountabilityProbabilityValue.ToString()}"));

                    double assetScore = (assetConfidentialityProbabilityValue + assetIntegrityProbabilityValue + assetAvailabilityProbabilityValue + assetAccountabilityProbabilityValue) / 4;
                    if (GraphCalcs.useCalcLog) GraphCalcs.s_CalculationLog.Add((DateTime.Now, $"assetScore: {assetScore.ToString()}"));

                    //Save the individual elements 
                    s_allNodes[i].assetConfidentialityProbabilityValue = assetConfidentialityProbabilityValue.ToString();
                    s_allNodes[i].assetIntegrityProbabilityValue = assetIntegrityProbabilityValue.ToString();
                    s_allNodes[i].assetAvailabilityProbabilityValue = assetAvailabilityProbabilityValue.ToString();
                    s_allNodes[i].assetAccountabilityProbabilityValue = assetAccountabilityProbabilityValue.ToString();

                    s_allNodes[i].assetScore = ClampNodeScore(assetScore).ToString();
                    GraphUtil.AddToNodeScores(s_allNodes[i].nodeID, assetScore);


                }
            }
        }

        public static double GetActorNodeScore(string node_id)
        {
            double value = 0;
            foreach (LocalNode localNode in s_allNodes)
            {
                if (localNode.nodeID == node_id)
                {
                    if (localNode.actorScore == null)
                    {
                        if (!alreadyRunActor)
                        {
                            CalculateActorScoreForAllNodes();
                            value = GetActorNodeScore(node_id);
                            alreadyRunActor = true;
                            return Math.Round(value);
                        }
                        else
                        {
                            value = -1;
                            alreadyRunActor = false;
                            return Math.Round(value);
                        }

                    }
                    else
                    {
                        value = Double.Parse(localNode.actorScore);
                        alreadyRunActor = false;
                        return Math.Round(value);
                    }
                }
            }
            return value;
        }

        public static double GetActorNodeMitigatedScore(string node_id)
        {
            double value = 0;
            foreach (LocalNode localNode in s_allNodes)
            {
                if (localNode.nodeID == node_id)
                {
                    if (localNode.actorMitigatedScore == null)
                    {
                        if (localNode.actorScore != null)
                            value = Math.Round(Double.Parse(localNode.actorScore));
                        else
                            value = 0;
                        return Math.Round(value);
                    }
                    else
                    {
                        value = Double.Parse(localNode.actorMitigatedScore);
                        return Math.Round(value);
                    }
                }
            }
            return value;
        }

        public static double GetHighestAttackActorMitigatedScore(string node_id)
        {
            double value = 100;
            foreach (LocalNode localNode in s_allNodes)
            {
                if (localNode.nodeID == node_id)
                {
                    if (localNode.highestAttackActorMitigatedScore == null)
                    {
                        value = 100;
                        return Math.Round(value);
                    }
                    else
                    {
                        value = Double.Parse(localNode.highestAttackActorMitigatedScore);
                        return Math.Round(value);
                    }
                }
            }
            return Math.Round(value);
        }

        public static double GetAttackNodeScore(string node_id)
        {
            double value = 0;
            foreach (LocalNode localNode in s_allNodes)
            {
                if (localNode.nodeID == node_id)
                {
                    if (localNode.attackScore == null)
                    {
                        if (!alreadyRunAttack)
                        {
                            CalculateAttackScoreForAllNodes();
                            value = GetAttackNodeScore(node_id);
                            alreadyRunAttack = true;
                            return Math.Round(value);
                        }
                        else
                        {
                            value = -1;
                            alreadyRunAttack = false;
                            return Math.Round(value);
                        }
                    }
                    else
                    {
                        value = Double.Parse(localNode.attackScore);
                        alreadyRunAttack = false;
                        return Math.Round(value);
                    }
                }
            }
            return value;
        }

        public static double GetAttackConfidentialityScore(string node_id)
        {
            double value = 0;
            foreach (LocalNode localNode in s_allNodes)
            {
                if (localNode.nodeID == node_id)
                {
                    if (localNode.attackImpactToConfidentialityValue != null)
                        value = Double.Parse(localNode.attackImpactToConfidentialityValue);
                    else
                        value = 0;
                    return value;
                }
            }
            return value;
        }

        public static double GetAttackIntegrityScore(string node_id)
        {
            double value = 0;
            foreach (LocalNode localNode in s_allNodes)
            {
                if (localNode.nodeID == node_id)
                {
                    if (localNode.attackImpactToIntegrityValue != null)
                        value = Double.Parse(localNode.attackImpactToIntegrityValue);
                    else
                        value = 0;
                    return value;
                }
            }
            return value;
        }

        public static double GetAttackAvailibilityScore(string node_id)
        {
            double value = 0;
            foreach (LocalNode localNode in s_allNodes)
            {
                if (localNode.nodeID == node_id)
                {
                    if (localNode.attackImpactToAvailabilityValue != null)
                        value = Double.Parse(localNode.attackImpactToAvailabilityValue);
                    else
                        value = 0;
                    return value;
                }
            }
            return value;
        }

        public static double GetAttackAccountabilityScore(string node_id)
        {
            double value = 0;
            foreach (LocalNode localNode in s_allNodes)
            {
                if (localNode.nodeID == node_id)
                {
                    if (localNode.attackImpactToAccountabilityValue != null)
                        value = Double.Parse(localNode.attackImpactToAccountabilityValue);
                    else
                        value = 0;
                    return value;
                }
            }
            return value;
        }

        public static double GetAttackNodeMitigatedScore(string node_id)
        {
            double value = 0;
            foreach (LocalNode localNode in s_allNodes)
            {
                if (localNode.nodeID == node_id)
                {
                    if (localNode.attackMitigatedScore == null)
                    {
                        if (localNode.attackScore != null)
                            value = Double.Parse(localNode.attackScore);
                        else
                            value = 0;
                        break;
                    }
                    else
                    {
                        value = Double.Parse(localNode.attackMitigatedScore);
                        break;
                    }

                    break;
                }
            }
            return value;
        }

        public static double GetAttackNodeThreatScore(string node_id)
        {
            double value = 0;
            foreach (LocalNode localNode in s_allNodes)
            {
                if (localNode.nodeID == node_id)
                {
                    if (localNode.threatScore == null)
                    {
                        value = -1;
                        break;
                    }
                    else
                    {
                        value = Double.Parse(localNode.threatScore);
                        break;
                    }

                }
            }
            return value;
        }

        public static double GetVulnerabilityNodeLikelihoodScore(string node_id)
        {
            double value = -1;
            foreach (LocalNode localNode in s_allNodes)
            {
                if (localNode.nodeID == node_id)
                {
                    if (localNode.likelihoodScore == null)
                    {
                        value = -1;

                        //throw new ApplicationException("Returned Null - Need to check the impliactions of this"); // 12/04/23
                        break;
                    }
                    else
                    {
                        value = Double.Parse(localNode.likelihoodScore);
                        break;
                    }

                }
            }
            return value;
        }

        public static double GetAssetNodeLikelihoodScore(string node_id)
        {
            double value = -1;
            foreach (LocalNode localNode in s_allNodes)
            {
                if (localNode.nodeID == node_id)
                {
                    if (localNode.assetLikelihoodScore == null)
                    {
                        value = -1;
                        break;
                    }
                    else
                    {
                        value = Double.Parse(localNode.assetLikelihoodScore);
                        break;
                    }

                }
            }
            return value;
        }

        public static double GetAssetNodeScore(string node_id)
        {
            double value = 0;
            foreach (LocalNode localNode in s_allNodes)
            {
                if (localNode.nodeID == node_id)
                {
                    if (localNode.assetScore == null)
                    {
                        if (!alreadyRunAsset)
                        {
                            CalculateAssetScoreForAllNodes();
                            value = GetAssetNodeScore(node_id);
                            alreadyRunAsset = true;
                        }
                        else
                        {
                            value = -1;
                            alreadyRunAsset = false;
                            return value;
                        }
                    }
                    else
                    {
                        value = Double.Parse(localNode.assetScore);
                        alreadyRunAsset = false;
                        return value;
                    }
                    break;
                }
            }
            return value;
        }

        public static double GetVulnerabilityNodeScore(string node_id)
        {
            double value = 0;
            foreach (LocalNode localNode in s_allNodes)
            {
                if (localNode.nodeID == node_id)
                {
                    if (localNode.vulnerabilityScore == null)
                    {
                        if (!alreadyRunVulnerability)
                        {
                            CalculateVulnerabilityScoreForAllNodes();
                            value = GetVulnerabilityNodeScore(node_id);
                            alreadyRunVulnerability = true;
                        }
                        else
                        {
                            value = -1;
                            alreadyRunVulnerability = false;
                            return value;
                        }
                    }
                    else
                    {
                        value = Double.Parse(localNode.vulnerabilityScore);
                        alreadyRunVulnerability = false;
                        return value;
                    }
                    break;
                }
            }
            return value;
        }

        public static double GetVulnerabilityMitigatedNodeScore(string node_id)
        {
            double value = 0;
            foreach (LocalNode localNode in s_allNodes)
            {
                if (localNode.nodeID == node_id)
                {
                    if (localNode.vulnerabilityMitigatedScore == null)
                    {
                        if (!alreadyRunVulnerability)
                        {
                            CalculateVulnerabilityScoreForAllNodes();
                            value = GetVulnerabilityMitigatedNodeScore(node_id);
                            alreadyRunVulnerability = true;
                        }
                        else
                        {
                            value = -1;
                            alreadyRunVulnerability = false;
                            return value;
                        }
                    }
                    else
                    {
                        value = Double.Parse(localNode.vulnerabilityMitigatedScore);
                        alreadyRunVulnerability = false;
                        return value;
                    }
                    break;
                }
            }
            return value;
        }

        public static double GetAssetConfidentialityValue(string node_id)
        {
            double value = 0;
            foreach (LocalNode localNode in s_allNodes)
            {
                if (localNode.nodeID == node_id)
                {
                    if (localNode.assetConfidentialityValue != null)
                        value = Double.Parse(localNode.assetConfidentialityValue);
                    break;
                }
            }
            return value;
        }

        public static double GetAssetNodeConfidentialityMitigatedValue(string node_id)
        {
            double value = 0;
            foreach (LocalNode localNode in s_allNodes)
            {
                if (localNode.nodeID == node_id)
                {
                    if (localNode.assetConfidentialityMitigatedScore != null)
                        value = Double.Parse(localNode.assetConfidentialityMitigatedScore);
                    break;
                }
            }
            return value;
        }

        public static double GetAssetConfidentialityProbabilityValue(string node_id)
        {
            double value = 0;
            foreach (LocalNode localNode in s_allNodes)
            {
                if (localNode.nodeID == node_id)
                {
                    if (localNode.assetConfidentialityProbabilityValue != null)
                        value = Double.Parse(localNode.assetConfidentialityProbabilityValue);
                    break;
                }
            }
            return value;
        }

        public static double GetAssetNodeIntegrityProbabilityValue(string node_id)
        {
            double value = 0;
            foreach (LocalNode localNode in s_allNodes)
            {
                if (localNode.nodeID == node_id)
                {
                    if (localNode.assetIntegrityProbabilityValue != null)
                        value = Double.Parse(localNode.assetIntegrityProbabilityValue);
                    break;
                }
            }
            return value;
        }


        public static double GetAssetNodeAvailabilityProbabilityValue(string node_id)
        {
            double value = 0;
            foreach (LocalNode localNode in s_allNodes)
            {
                if (localNode.nodeID == node_id)
                {
                    if (localNode.assetAvailabilityProbabilityValue != null)
                        value = Double.Parse(localNode.assetAvailabilityProbabilityValue);
                    break;
                }
            }
            return value;
        }

        public static double GetAssetNodeAccountabilityProbabilityValue(string node_id)
        {
            double value = 0;
            foreach (LocalNode localNode in s_allNodes)
            {
                if (localNode.nodeID == node_id)
                {
                    if (localNode.assetAccountabilityProbabilityValue != null)
                        value = Double.Parse(localNode.assetAccountabilityProbabilityValue);
                    break;
                }
            }
            return value;
        }



        public static double GetAssetNodeMitigatedScore(string node_id)
        {
            double value = 0;
            foreach (LocalNode localNode in s_allNodes)
            {
                if (localNode.nodeID == node_id)
                {
                    if (localNode.assetMitigatedScore == null)
                    {
                        if (localNode.assetScore != null)
                            value = Double.Parse(localNode.assetScore);
                        else
                            value = 0;
                    }
                    else
                    {
                        value = Double.Parse(localNode.assetMitigatedScore);
                    }

                    break;
                }
            }
            return value;
        }

        public static double GetVulnerabilityNodeMitigatedScore(string node_id)
        {
            double value = -1;
            foreach (LocalNode localNode in s_allNodes)
            {
                if (localNode.nodeID == node_id)
                {
                    if (localNode.vulnerabilityMitigatedScore == null)
                    {
                        if (localNode.vulnerabilityScore != null)
                            value = Double.Parse(localNode.vulnerabilityScore);
                        else
                            value = 0;
                    }
                    else
                    {
                        value = Double.Parse(localNode.vulnerabilityMitigatedScore);
                    }

                    break;
                }
            }
            return value;
        }

        public static string GetImplementedStrengthText(string node_id)
        {
            string value = "Not Assessed";
            foreach (LocalNode localNode in s_allNodes)
            {
                if (localNode.nodeID == node_id)
                {
                    if (localNode.implementedStrength == null)
                    {
                        value = "Not Assessed";
                    }
                    else
                    {
                        value = localNode.implementedStrength.ToString();
                    }

                    break;
                }
            }
            return value;
        }

        public static string GetInherentStrengthText(string node_id)
        {
            string value = "Not Assessed";
            foreach (LocalNode localNode in s_allNodes)
            {
                if (localNode.nodeID == node_id)
                {
                    if (localNode.objectiveTargetType == null)
                    {
                        value = "Not Assessed";
                    }
                    else
                    {
                        value = localNode.objectiveTargetType.ToString();
                    }

                    break;
                }
            }
            return value;
        }

        public static string GetnodeBehaviour(string node_id)
        {
            string value = "Sum";
            foreach (LocalNode localNode in s_allNodes)
            {
                if (localNode.nodeID == node_id)
                {
                    if (localNode.nodeBehaviour == null || localNode.nodeBehaviour == "")
                    {
                        value = "Sum";
                    }
                    else
                    {
                        value = localNode.nodeBehaviour.ToString();
                    }

                    break;
                }
            }
            return value;
        }

        public static double GetAssetNodeImpactScore(string node_id)
        {
            double value = -1;
            foreach (LocalNode localNode in s_allNodes)
            {
                if (localNode.nodeID == node_id)
                {
                    if (localNode.impactScore == null)
                    {
                        value = 0;
                        //throw new ApplicationException("Returned Null - Need to check the impliactions of this"); // 12/04/23
                        return value;

                    }
                    else
                    {
                        value = Double.Parse(localNode.impactScore);
                    }

                    break;
                }
            }
            return value;
        }

        public static double GetAssetConfidentialityMitigatedScore(string node_id)
        {
            foreach (LocalNode localNode in s_allNodes)
            {
                if (localNode.nodeID == node_id)
                {
                    if (localNode.assetConfidentialityMitigatedScore == null)
                        return 0;
                    else
                        return Double.Parse(localNode.assetConfidentialityMitigatedScore);
                }
            }
            return 0;
        }

        public static double GetAssetNodeIntegrityMitigatedScore(string node_id)
        {
            foreach (LocalNode localNode in s_allNodes)
            {
                if (localNode.nodeID == node_id)
                {
                    if (localNode.assetIntegrityMitigatedScore == null)
                        return 0;
                    else
                        return Double.Parse(localNode.assetIntegrityMitigatedScore);
                }
            }
            return 0;
        }

        public static double GetAssetNodeAvailabilityMitigatedScore(string node_id)
        {
            foreach (LocalNode localNode in s_allNodes)
            {
                if (localNode.nodeID == node_id)
                {
                    if (localNode.assetAvailabilityMitigatedScore == null)
                        return 0;
                    else
                        return Double.Parse(localNode.assetAvailabilityMitigatedScore);
                }
            }
            return 0;
        }

        public static double GetAssetNodeAccountabilityMitigatedScore(string node_id)
        {
            foreach (LocalNode localNode in s_allNodes)
            {
                if (localNode.nodeID == node_id)
                {
                    if (localNode.assetAccountabilityMitigatedScore == null)
                        return 0;
                    else
                        return Double.Parse(localNode.assetAccountabilityMitigatedScore);
                }
            }
            return 0;
        }


        public static double GetRiskScore(string node_id)
        {
            double value = 0;
            foreach (LocalNode localNode in s_allNodes)
            {
                if (localNode.nodeID == node_id)
                {
                    if (localNode.riskScore == null)
                    {
                        value = 100;
                    }
                    else
                    {
                        value = Double.Parse(localNode.riskScore);
                    }

                    break;
                }
            }
            return value;
        }

        public static double GetSpecificThreatScore(string actorNodeID, string attackNodeID)
        {
            //Threat score = (ActorMitigatedScore x Edge Strength) x AttackMitigatedScore

            // Get Actor Mitigated Score
            double actorMitigatedScore = GetActorNodeMitigatedScore(actorNodeID);

            //Get Edge Strengh
            string edgeID = GetEdgeBetweenNodes(actorNodeID, attackNodeID);
            double edgeStrength = GetEdgeStrengthScore(edgeID);

            // Get Attack Mitigated Value
            double attackMitigatedScore = GetAttackNodeMitigatedScore(attackNodeID);

            //calculate ThreatScore
            double threatScore = ((actorMitigatedScore * edgeStrength) * attackMitigatedScore) / 100;

            return threatScore;

        }

        public static double GetAssetThreatScore(string assetNodeID, string attackNodeID)
        {
            //Threat score = (assetPassedLikelihoodScoreScore x Edge Strength) x AttackMitigatedScore

            // Get Asset Threat Score
            double assetPassedThreatScore = GetVulnerabilityNodeLikelihoodScore(assetNodeID);

            //Get Edge Strengh
            string edgeID = GetEdgeBetweenNodes(assetNodeID, attackNodeID);
            double edgeStrength = GetEdgeStrengthScore(edgeID);

            // Get Attack Mitigated Value
            double attackMitigatedScore = GetAttackNodeMitigatedScore(attackNodeID);

            //calculate ThreatScore
            double threatScore = ((assetPassedThreatScore * edgeStrength) * attackMitigatedScore) / 100;

            return threatScore;

        }

        public static double GetSpecificLikelihoodScore(double threatScore, string attackNodeID, string vulnerabilityNodeID)
        {
            //Likelihood score = (threat score x Edge Strength) x vulnerabilityMitigatedScore

            //Get Edge Strengh
            string edgeID = GetEdgeBetweenNodes(attackNodeID, vulnerabilityNodeID);
            double edgeStrength = GetEdgeStrengthScore(edgeID);

            // Get Vulnerability Mitigated Value
            double vulnerabilityMitigatedScore = GetVulnerabilityNodeMitigatedScore(vulnerabilityNodeID);

            //calculate Likelihood Sxore
            double likelihoodScore = ((threatScore * edgeStrength) * vulnerabilityMitigatedScore) / 100;

            return likelihoodScore;

        }

        public static double GetSpecificImpactScore(double likelihoodScore, string vulnerabilityNodeID, string assetNodeID)
        {
            //TODO Need to chained Assets

            //Impact score = (likelihood score x Edge Strength) x assetMitigatedScore

            //Get Edge Strengh
            string edgeID = GetEdgeBetweenNodes(vulnerabilityNodeID, assetNodeID);
            double edgeStrength = GetEdgeStrengthScore(edgeID);

            // Get asset Mitigated Value
            double assetMitigatedScore = GetAssetNodeMitigatedScore(assetNodeID);

            //calculate Likelihood Sxore
            double impactscore = ((likelihoodScore * edgeStrength) * assetMitigatedScore) / 100;

            return impactscore;

        }

        public static JObject GraphDetailData(string data)
        {
            Graph.Utility.SaveAuditLog("GraphDetailData", "+++FUNCTION ENTERED+++", "", "", $"");
            JArray arr = JArray.Parse(data);
            JObject retObj = new JObject();

            int controlCount = 0;
            int assetCount = 0;
            int attackCount = 0;
            int objectiveCount = 0;
            int actorCount = 0;
            int edgeCount = 0;
            string tags = "";

            for (int i = 0; i < arr.Count; i++)
            {
                JObject obj = arr[i]["data"] as JObject;
                string node_type = obj["nodeType"] != null ? obj["nodeType"].ToString() : "edge";
                string metatags = obj["metatags"] != null ? obj["metatags"].ToString() : "";
                switch (node_type.ToLower())
                {
                    case "control":
                        controlCount++;
                        break;
                    case "asset":
                        assetCount++;
                        break;
                    case "attack":
                        attackCount++;
                        break;
                    case "objective":
                        objectiveCount++;
                        break;
                    case "actor":
                        actorCount++;
                        break;
                    case "edge":
                        edgeCount++;
                        break;
                }
                tags = tags + metatags;
            }
            retObj["nodeCount"] = (controlCount + assetCount + attackCount + objectiveCount + actorCount).ToString();
            retObj["controlCount"] = controlCount.ToString();
            retObj["assetCount"] = assetCount.ToString();
            retObj["attackCount"] = attackCount.ToString();
            retObj["objectiveCount"] = objectiveCount.ToString();
            retObj["actorCount"] = actorCount.ToString();
            retObj["edgeCount"] = edgeCount.ToString();
            retObj["tags"] = tags;

            return retObj;
        }

        public static JObject GraphFileData(string data)
        {
            JObject obj = JObject.Parse(data);
            JObject ret_obj = new JObject();
            ret_obj["savedDateTime"] = DateTime.UtcNow.ToString("u");
            ret_obj["savedByGUID"] = AuthAPI._user_guid;
            ret_obj["parentGraphGUID"] = GraphAPI._graph_guid;
            ret_obj["fileLocked"] = 0;
            ret_obj["lockedByGUID"] = null;
            ret_obj["title"] = obj["name"];
            ret_obj["status"] = "";
            ret_obj["majorVersion"] = obj["majorVersion"];
            ret_obj["minorVersion"] = obj["minorVersion"];
            ret_obj["revision"] = obj["revision"];
            ret_obj["description"] = obj["description"];
            ret_obj["note"] = obj["note"];
            ret_obj["image"] = obj["image"];
            return ret_obj;
        }

        public static Node SetNodeFullData(string nodeGUID, JObject nodeMeta, JObject nodeGraph, JObject nodeVisual, JObject nodeNote, JObject nodeFramework)
        {
            Node node = new Node();
            node.ID = nodeGUID;
            node.masterID = nodeGUID;
            node = SetNodeMetaData(node, nodeMeta);
            node = SetNodeGraphData(node, nodeGraph);
            node = SetNodeVisualData(node, nodeVisual);
            node = SetNodeNoteData(node, nodeNote);
            node = SetNodeFrameworkData(node, nodeFramework);
            return node;
        }

        //public static Edge SetEdgeFullData(string edgeGUID, JObject edgeData)
        //{
        //    Edge edge = new Edge();
        //    edge.ID = edgeGUID;
        //    edge.Source = !edgeData.ContainsKey("source") ? "" : edgeData["source"].ToString();
        //    edge.Target = !edgeData.ContainsKey("target") ? "" : edgeData["target"].ToString();
        //    edge.Weight = !edgeData.ContainsKey("weight") ? 1 : Convert.ToDouble(edgeData["weight"].ToString(), CultureInfo.InvariantCulture);
        //    edge.Title = !edgeData.ContainsKey("title") ? "" : edgeData["title"].ToString();
        //    edge.Description = edgeData.ContainsKey("description") ? "" : edgeData["description"].ToString();
        //    edge.Note = !edgeData.ContainsKey("note") ? "" : edgeData["note"].ToString();
        //    edge.Relationship = !edgeData.ContainsKey("relationship") ? "" : edgeData["relationship"].ToString();
        //    edge.LabelSize = !edgeData.ContainsKey("labelSize") ? 1 : Convert.ToDouble(edgeData["labelSize"].ToString());
        //    edge.Enabled = !edgeData.ContainsKey("enabled") ? true : edgeData["enabled"].ToString().ToLower() == "true";
        //    edge.DrawingWeight = !edgeData.ContainsKey("edgeStrengthValue") ? 1 : Convert.ToDouble(edgeData["edgeStrengthValue"].ToString(), CultureInfo.InvariantCulture);
        //    edge.ImpactedValue = !edgeData.ContainsKey("impactedValue") ? 1 : Convert.ToDouble(edgeData["impactedValue"].ToString(), CultureInfo.InvariantCulture);
        //    edge.Color = !edgeData.ContainsKey("color") ? Color.Black : GeneralHelpers.ConvertColorFromHTML(edgeData["color"].ToString());
        //    edge.edgeStrengthValue = !edgeData.ContainsKey("edgeStrengthValue") ? 1 : Convert.ToDouble(edgeData["edgeStrengthValue"], CultureInfo.InvariantCulture);
        //    return edge;
        //}

        public static Node SetNodeMetaData(Node node, JObject nodeMeta)
        {
            string tmp_type = nodeMeta["nodeType"] == null ? "" : nodeMeta["nodeType"].ToString();
            node.Type = new NodeType(tmp_type, tmp_type);
            node.Category = nodeMeta["category"] == null ? "" : nodeMeta["category"].ToString();
            node.SubCategory = nodeMeta["subCategory"] == null ? "" : nodeMeta["subCategory"].ToString();
            return node;
        }

        public static Node SetNodeGraphData(Node node, JObject nodeGraph)
        {
            node.AssessedStatus = nodeGraph["assessedStatus"] == null ? "" : nodeGraph["assessedStatus"].ToString();
            node.controlAssessedValue = nodeGraph["controlAssessedValue"] == null || nodeGraph["controlAssessedValue"].ToString() == "" ? 0.0 : Double.Parse(nodeGraph["controlAssessedValue"].ToString(), CultureInfo.InvariantCulture);
            node.controlAssessedScore = nodeGraph["controlAssessedScore"] == null || nodeGraph["controlAssessedScore"].ToString() == "" ? 0.0 : Double.Parse(nodeGraph["controlAssessedScore"].ToString(), CultureInfo.InvariantCulture);
            node.controlAssessedMinValue = nodeGraph["controlAssessedMinValue"] == null || nodeGraph["controlAssessedMinValue"].ToString() == "" ? 0.0 : Double.Parse(nodeGraph["controlAssessedMinValue"].ToString(), CultureInfo.InvariantCulture);
            node.controlBaseScore = nodeGraph["controlBaseScore"] == null || nodeGraph["controlBaseScore"].ToString() == "" ? 0.0 : Double.Parse(nodeGraph["controlBaseScore"].ToString(), CultureInfo.InvariantCulture);
            node.controlBaseValue = nodeGraph["controlBaseValue"] == null || nodeGraph["controlBaseValue"].ToString() == "" ? 0.0 : Double.Parse(nodeGraph["controlBaseValue"].ToString(), CultureInfo.InvariantCulture);
            node.controlBaseMinValue = nodeGraph["controlBaseMinValue"] == null || nodeGraph["controlBaseMinValue"].ToString() == "" ? 0.0 : Double.Parse(nodeGraph["controlBaseMinValue"].ToString(), CultureInfo.InvariantCulture);
            node.CalculatedValue = nodeGraph["calculatedValue"] == null || nodeGraph["calculatedValue"].ToString() == "" ? 0.0 : Double.Parse(nodeGraph["calculatedValue"].ToString(), CultureInfo.InvariantCulture);

            node.actorAccessText = nodeGraph["actorAccess"] == null ? "" : nodeGraph["actorAccess"].ToString();
            node.actorAccessValue = nodeGraph["actorAccessValue"] == null || nodeGraph["actorAccessValue"].ToString() == "" ? 0.0 : Double.Parse(nodeGraph["actorAccessValue"].ToString(), CultureInfo.InvariantCulture);
            node.actorCapabilityText = nodeGraph["actorCapability"] == null ? "" : nodeGraph["actorCapability"].ToString();
            node.actorCapabilityValue = nodeGraph["actorCapabilityValue"] == null || nodeGraph["actorCapabilityValue"].ToString() == "" ? 0.0 : Double.Parse(nodeGraph["actorCapabilityValue"].ToString(), CultureInfo.InvariantCulture);
            node.actorMotivationText = nodeGraph["actorMotivation"] == null ? "" : nodeGraph["actorMotivation"].ToString();
            node.actorMotivationValue = nodeGraph["actorMotivationValue"] == null || nodeGraph["actorMotivationValue"].ToString() == "" ? 0.0 : Double.Parse(nodeGraph["actorMotivationValue"].ToString(), CultureInfo.InvariantCulture);
            node.actorResourcesText = nodeGraph["actorResources"] == null ? "" : nodeGraph["actorResources"].ToString();
            node.actorResourcesValue = nodeGraph["actorResourcesValue"] == null || nodeGraph["actorResourcesValue"].ToString() == "" ? 0.0 : Double.Parse(nodeGraph["actorResourcesValue"].ToString(), CultureInfo.InvariantCulture);

            node.assetAccountabilityText = nodeGraph["assetAccountability"] == null ? "" : nodeGraph["assetAccountability"].ToString();
            node.assetAccountabilityValue = nodeGraph["assetAccountabilityValue"] == null || nodeGraph["assetAccountabilityValue"].ToString() == "" ? 0.0 : Double.Parse(nodeGraph["assetAccountabilityValue"].ToString(), CultureInfo.InvariantCulture);
            node.assetAvailabilityText = nodeGraph["assetAvailability"] == null ? "" : nodeGraph["assetAvailability"].ToString();
            node.assetAvailabilityValue = nodeGraph["assetAvailabilityValue"] == null || nodeGraph["assetAvailabilityValue"].ToString() == "" ? 0.0 : Double.Parse(nodeGraph["assetAvailabilityValue"].ToString(), CultureInfo.InvariantCulture);
            node.assetConfidentialityText = nodeGraph["assetConfidentiality"] == null ? "" : nodeGraph["assetConfidentiality"].ToString();
            node.assetConfidentialityValue = nodeGraph["assetConfidentialityValue"] == null || nodeGraph["assetConfidentialityValue"].ToString() == "" ? 0.0 : Double.Parse(nodeGraph["assetConfidentialityValue"].ToString(), CultureInfo.InvariantCulture);
            node.assetConfidentialityValue = nodeGraph["assetConfidentialityProbabilityValue"] == null || nodeGraph["assetConfidentialityProbabilityValue"].ToString() == "" ? 0.0 : Double.Parse(nodeGraph["assetConfidentialityProbabilityValue"].ToString(), CultureInfo.InvariantCulture);
            node.assetIntegrityText = nodeGraph["assetIntegrity"] == null ? "" : nodeGraph["assetIntegrity"].ToString();
            node.assetIntegrityValue = nodeGraph["assetIntegrityValue"] == null || nodeGraph["assetIntegrityValue"].ToString() == "" ? 0.0 : Double.Parse(nodeGraph["assetIntegrityValue"].ToString(), CultureInfo.InvariantCulture);

            node.attackComplexityText = nodeGraph["attackComplixity"] == null ? "" : nodeGraph["attackComplixity"].ToString();
            node.attackComplexityValue = nodeGraph["attackComplixityValue"] == null || nodeGraph["attackComplixityValue"].ToString() == "" ? 0.0 : Double.Parse(nodeGraph["attackComplixityValue"].ToString(), CultureInfo.InvariantCulture);
            node.attackImpactToAccountabilityText = nodeGraph["attackImpactToAccountability"] == null ? "" : nodeGraph["attackImpactToAccountability"].ToString();
            node.attackImpactToAccountabilityValue = nodeGraph["attackImpactToAccountabilityValue"] == null || nodeGraph["attackImpactToAccountabilityValue"].ToString() == "" ? 0.0 : Double.Parse(nodeGraph["attackImpactToAccountabilityValue"].ToString(), CultureInfo.InvariantCulture);
            node.attackImpactToAvailabilityText = nodeGraph["attackImpactToAvailability"] == null ? "" : nodeGraph["attackImpactToAvailability"].ToString();
            node.attackImpactToAvailabilityValue = nodeGraph["attackImpactToAvailabilityValue"] == null || nodeGraph["attackImpactToAvailabilityValue"].ToString() == "" ? 0.0 : Double.Parse(nodeGraph["attackImpactToAvailabilityValue"].ToString(), CultureInfo.InvariantCulture);
            node.attackImpactToConfidentialityText = nodeGraph["attackImpactConfident"] == null ? "" : nodeGraph["attackImpactConfident"].ToString();
            node.attackImpactToConfidentialityValue = nodeGraph["attackImpactToConfidentialityValue"] == null || nodeGraph["attackImpactToConfidentialityValue"].ToString() == "" ? 0.0 : Double.Parse(nodeGraph["attackImpactToConfidentialityValue"].ToString(), CultureInfo.InvariantCulture);
            node.attackImpactToConfidentialityMinValue = nodeGraph["attackImpactToConfidentialityMinValue"] == null || nodeGraph["attackImpactToConfidentialityMinValue"].ToString() == "" ? 0.0 : Double.Parse(nodeGraph["attackImpactToConfidentialityMinValue"].ToString(), CultureInfo.InvariantCulture);
            node.attackImpactToIntegrityText = nodeGraph["attackImpactToIntegrity"] == null ? "" : nodeGraph["attackImpactToIntegrity"].ToString();
            node.attackImpactToIntegrityValue = nodeGraph["attackImpactToIntegrityValue"] == null || nodeGraph["attackImpactToIntegrityValue"].ToString() == "" ? 0.0 : Double.Parse(nodeGraph["attackImpactToIntegrityValue"].ToString(), CultureInfo.InvariantCulture);
            node.attackImpactToIntegrityMinValue = nodeGraph["attackImpactToIntegrityMinValue"] == null || nodeGraph["attackImpactToIntegrityMinValue"].ToString() == "" ? 0.0 : Double.Parse(nodeGraph["attackImpactToIntegrityMinValue"].ToString(), CultureInfo.InvariantCulture);
            node.attackImpactToAvailabilityValue = nodeGraph["attackImpactToAvailabilityValue"] == null || nodeGraph["attackImpactToAvailabilityValue"].ToString() == "" ? 0.0 : Double.Parse(nodeGraph["attackImpactToAvailabilityValue"].ToString(), CultureInfo.InvariantCulture);
            node.attackImpactToAvailabilityMinValue = nodeGraph["attackImpactToAvailabilityMinValue"] == null || nodeGraph["attackImpactToAvailabilityMinValue"].ToString() == "" ? 0.0 : Double.Parse(nodeGraph["attackImpactToAvailabilityMinValue"].ToString(), CultureInfo.InvariantCulture);
            node.attackImpactToAccountabilityValue = nodeGraph["attackImpactToAccountabilityValue"] == null || nodeGraph["attackImpactToAccountabilityValue"].ToString() == "" ? 0.0 : Double.Parse(nodeGraph["attackImpactToAccountabilityValue"].ToString(), CultureInfo.InvariantCulture);
            node.attackImpactToAccountabilityMinValue = nodeGraph["attackImpactToAccountabilityMinValue"] == null || nodeGraph["attackImpactToAccountabilityMinValue"].ToString() == "" ? 0.0 : Double.Parse(nodeGraph["attackImpactToAccountabilityMinValue"].ToString(), CultureInfo.InvariantCulture);
            node.attackProliferationText = nodeGraph["attackProliferation"] == null ? "" : nodeGraph["attackProliferation"].ToString();
            node.attackProliferationValue = nodeGraph["attackProliferationValue"] == null || nodeGraph["attackProliferationValue"].ToString() == "" ? 0.0 : Double.Parse(nodeGraph["attackProliferationValue"].ToString(), CultureInfo.InvariantCulture);
            node.MetaTags = nodeGraph["tags"] == null ? "" : nodeGraph["tags"].ToString();
            return node;
        }

        public static Node SetNodeVisualData(Node node, JObject nodeVisual)
        {
            node.BorderWidth = nodeVisual["borderWidth"] == null ? null : nodeVisual["borderWidth"].ToString();
            node.BorderColor = nodeVisual["borderColor"] == null ? new Color() : GeneralHelpers.ConvertColorFromHTML(nodeVisual["borderColor"].ToString());
            node.Color = nodeVisual["color"] == null ? new Color() : GeneralHelpers.ConvertColorFromHTML(nodeVisual["color"].ToString());
            node.Height = nodeVisual["height"] == null || nodeVisual["height"].ToString() == "" ? 1.0 : Double.Parse(nodeVisual["height"].ToString(), CultureInfo.InvariantCulture);
            node.NodeImageData = nodeVisual["image"] == null ? null : nodeVisual["image"].ToString();
            node.ImagePath = nodeVisual["imagePath"] == null ? null : nodeVisual["imagePath"].ToString();
            node.HTMLOpacity = nodeVisual["opacity"] == null || nodeVisual["opacity"].ToString() == "" ? 1.0 : Double.Parse(nodeVisual["opacity"].ToString(), CultureInfo.InvariantCulture);
            node.Shape = nodeVisual["originShape"] == null || nodeVisual["originShape"].ToString() == "" ? new NodeShape("none", "none") : new NodeShape(nodeVisual["originShape"].ToString(), nodeVisual["originShape"].ToString());
            node.origin_color = nodeVisual["originColor"] == null ? new Color() : GeneralHelpers.ConvertColorFromHTML(nodeVisual["originColor"].ToString());
            node.NodeTitlePosition = nodeVisual["position"] == null ? null : nodeVisual["position"].ToString();
            node.Size = nodeVisual["size"] == null || nodeVisual["size"].ToString() == "" ? 1.0 : Double.Parse(nodeVisual["size"].ToString(), CultureInfo.InvariantCulture);
            node.Title = nodeVisual["title"] == null ? null : nodeVisual["title"].ToString();
            node.TitleTextColor = nodeVisual["titleTextColor"] == null ? new Color() : GeneralHelpers.ConvertColorFromHTML(nodeVisual["titleTextColor"].ToString());
            node.TitleSize = nodeVisual["titleSize"] == null || nodeVisual["titleSize"].ToString() == "" ? 1.0 : Double.Parse(nodeVisual["titleSize"].ToString(), CultureInfo.InvariantCulture);
            node.Width = nodeVisual["width"] == null || nodeVisual["width"].ToString() == "" ? 1.0 : Double.Parse(nodeVisual["width"].ToString(), CultureInfo.InvariantCulture);
            node.Enabled = nodeVisual["enabled"] == null || nodeVisual["enabled"].ToString() == "" ? true : Boolean.Parse(nodeVisual["enabled"].ToString());
            return node;
        }

        public static Node SetNodeNoteData(Node node, JObject nodeNote)
        {
            node.Note = nodeNote["note"] == null ? "" : nodeNote["note"].ToString();
            return node;
        }

        public static void ClearDistributionData()
        {
            probabilityDistributions.Clear();
            nodeScores.Clear();
            calcIterations = 0;

        }

        public static void AddToDistributionData(string node_id, int index)
        {
            if (index < 0) return;

            if (!probabilityDistributions.ContainsKey(node_id))
            {
                // This is the first time this probability has been calaculated
                //Create and zero'ed distribution 
                try
                {
                    probabilityDistributions.Add(node_id, (ChartPointIndexer)CreateEmptyDistributionData());
                }
                catch
                { }
            }

            try
            {
                ChartPointIndexer tempData = (ChartPointIndexer)probabilityDistributions[node_id];


                //Need to handle distrubtions with Indexs greater that the 101 by default
                if (index > tempData.Count - 1)
                {
                    for (int i = tempData.Count; i < index + 2; i++)  // Increases the size upto the index value
                        tempData.Add(i, 0);
                }


                //Get the current value for the index and increase by 1
                double yValue = tempData[index].YValues[0];
                yValue += 1;
                tempData[index].YValues[0] = yValue;

                //Now update the distribution
                probabilityDistributions[node_id] = tempData;
            }
            catch
            { }
        }

        public static void AddToNodeScores(string node_id, double value)
        {
            node_id = calcIterations.ToString() + ":" + node_id;
            if (!nodeScores.ContainsKey(node_id))
            {
                try
                {
                    nodeScores.Add(node_id, value);
                }
                catch
                { }
            }

            try
            {
                nodeScores[node_id] = value;
            }
            catch
            { }
        }

        public static double GetNodeScore(string node_id)
        {
            node_id = calcIterations - 1 + ":" + node_id;
            if (!nodeScores.ContainsKey(node_id))
                return -1;
            else
                return nodeScores[node_id];
        }


        public static double GetAverageNodeScore(string node_id)
        {

            double score = 0;

            // need to remove the proceeding calcIteraction Value
            //RemoveTextBeforeDelimiter(node_id, ":");
            foreach (var nodeGUID in nodeScores.Keys)
            {
                if (nodeGUID.Contains(node_id))
                    score += nodeScores[nodeGUID];
            }

            score = score / calcIterations;
            return score;
        }




        public static string RemoveTextBeforeDelimiter(string text, string delimiter)
        {

            int delimiterIndex = text.IndexOf(delimiter);
            if (delimiterIndex >= 0)
            {
                return text.Substring(delimiterIndex + 1);
            }
            else
                return text;

        }


        public static double CalculateModeFromDistributionData(string node_id)
        {
            if (!probabilityDistributions.ContainsKey(node_id))
                return -1;

            List<double> multiyValues = new List<double>(); // Use a list instead of an array to handle variable length
            ChartPointIndexer tempData = (ChartPointIndexer)probabilityDistributions[node_id];
            double highestYValue = 0;
            double mode = 0;

            for (int i = 0; i < tempData.Count; i++)
            {
                double yValue = (double)tempData[i].YValues[0];

                if (yValue == highestYValue)
                {
                    multiyValues.Add((double)tempData[i].X);
                }

                if (yValue > highestYValue)
                {
                    multiyValues.Clear();
                    multiyValues.Add((double)tempData[i].X);
                    highestYValue = yValue;
                    mode = (int)tempData[i].X;
                }
            }

            if (multiyValues.Count > 1)
            {
                mode = 0;
                for (int i = 0; i < multiyValues.Count; i++)
                {
                    mode += multiyValues[i];
                }
                mode = mode / multiyValues.Count;

            }

            return mode;
        }



        public static int CalculateMean(string node_id)
        {
            if (!probabilityDistributions.ContainsKey(node_id))
                return -1;

            ChartPointIndexer tempData = (ChartPointIndexer)probabilityDistributions[node_id];
            int totalYValue = 0;
            int mean = 0;

            for (int i = 0; i < 101; i++)
            {
                int yValue = (int)tempData[i].YValues[0]; // Access Y-value at index i instead of always at index 0

                totalYValue += yValue;
            }
            mean = totalYValue / 101;

            return mean;
        }

        public static int CalculateMedian(string node_id)
        {
            if (!probabilityDistributions.ContainsKey(node_id))
                return -1;

            ChartPointIndexer tempData = (ChartPointIndexer)probabilityDistributions[node_id];

            List<int> yValues = new List<int>(); // Use a list instead of an array to handle variable length

            for (int i = 0; i < tempData.Count; i++)
            {
                int yValue = (int)tempData[i].YValues[0];

                if (yValue != 0) // Ignore values of 0
                {
                    yValues.Add(yValue);
                }
            }

            yValues.Sort(); // Sort the Y-values in ascending order

            int middleIndex = yValues.Count / 2;
            double median;

            if (tempData.Count % 2 == 0) // If the number of elements is even
            {
                median = (yValues[middleIndex - 1] + yValues[middleIndex]) / 2.0;
            }
            else // If the number of elements is odd
            {
                median = yValues[middleIndex];
            }

            return (int)median;
        }


        public static object CreateEmptyDistributionData()
        {
            ChartSeries tempSeries = new ChartSeries();
            ChartPointIndexer tempPoints = new ChartPointIndexer(tempSeries);

            for (int i = 0; i < 101; i++)
            {
                tempPoints.Add(i, 0);
            }
            return tempPoints;
        }


        public static object GetDistributionData(string node_id)
        {
            if (probabilityDistributions.ContainsKey(node_id))
            {
                return probabilityDistributions[node_id];
            }
            return null;
        }



        public static Node SetNodeFrameworkData(Node node, JObject nodeFramework)
        {
            node.Category = nodeFramework["category"] == null ? "" : nodeFramework["category"].ToString();
            node.SubCategory = nodeFramework["subCategory"] == null ? "" : nodeFramework["subCategory"].ToString();
            node.frameworkName = nodeFramework["framework"] == null ? "" : nodeFramework["framework"].ToString();
            node.ControlFrameworkVersion = nodeFramework["version"] == null ? "" : nodeFramework["version"].ToString();
            node.frameworkReference = nodeFramework["reference"] == null ? "" : nodeFramework["reference"].ToString();
            node.Domain = nodeFramework["Domain"] == null ? "" : nodeFramework["Domain"].ToString();
            node.SubDomain = nodeFramework["subDomain"] == null ? "" : nodeFramework["subDomain"].ToString();
            node.Level = nodeFramework["level"] == null ? "" : nodeFramework["level"].ToString();
            node.ReferenceURL = nodeFramework["refUrl"] == null ? "" : nodeFramework["refUrl"].ToString();
            return node;
        }

        internal static void AddRelationshipWithObject(JavascriptResponse lastEdge)
        {
            throw new NotImplementedException();
        }

        public static string GetRiskStatusFromValue(double RiskValue)
        {
            string riskStatus = "";

            if (RiskValue > 90)
            {
                riskStatus = "Critical";
            }
            else if (RiskValue > 75)
            {
                riskStatus = "Very High";
            }
            else if (RiskValue > 50)
            {
                riskStatus = "High";
            }
            else if (RiskValue > 25)
            {
                riskStatus = "Moderate";
            }
            else if (RiskValue > 10)
            {
                riskStatus = "Low";
            }
            else if (RiskValue > 0)
            {
                riskStatus = "Very Low";
            }
            else
            {
                riskStatus = "N/A";
            }

            return riskStatus;
        }

        public static void IncRiskBucket(string bucket)
        {
            if (!RiskBucket.ContainsKey(bucket))
                RiskBucket.Add(bucket, 1);
            else
            {
                int tempInt = RiskBucket[bucket];
                RiskBucket[bucket] = tempInt + 1;
            }
        }

        public static void ClearBuckets()
        {
            RiskBucket.Clear();
        }

        public static void AddToObjectiveBuckets(int RiskValue)
        {
             if (RiskValue > 90)
                IncRiskBucket("Excellent");
            else if (RiskValue > 75)
                IncRiskBucket("Very High");
            else if (RiskValue > 50)
                IncRiskBucket("High");
            else if (RiskValue > 25)
                IncRiskBucket("Moderate");
            else if (RiskValue > 10)
                IncRiskBucket("Low");
            else if (RiskValue > 0)
                IncRiskBucket("Very Low");
            else
                IncRiskBucket("N/A");
        }


        public static Dictionary<string, int> GetRiskBuckets()
        {
           return RiskBucket;
        }

        public static int GetRiskBucketCount(string bucket)
        {
            if (!RiskBucket.ContainsKey(bucket))
                return RiskBucket[bucket];
            else
            {
                return 0;
            }
        }

        public static (string,int) GetRiskBucketByIndex(int index)
        {
            var item = RiskBucket.ElementAt(index);
            return (item.Key, item.Value);
        }

        public static Color GetObjectiveColorFromName(string riskName)
        {

            if (riskName == "Excellent")
                return Color.Green;
            else if (riskName == "Very High")
                return Color.GreenYellow;
            else if (riskName == "High")
                return Color.Yellow;
            else if (riskName == "Moderate")
                return Color.Orange;
            else if (riskName == "Low")
                return Color.OrangeRed;
            else if (riskName == "Very Low")
                return Color.Red;
            else if (riskName == "N/A")
                return Color.Gray;

            return Color.Gray;

        }

        public static Color GetRiskColorFromValue(double riskValue)
        {
            Color riskColor;

            if (riskValue > 90)
            {
                riskColor = Color.Red;
            }
            else if (riskValue > 75)
            {
                riskColor = Color.OrangeRed;
            }
            else if (riskValue > 50)
            {
                riskColor = Color.Orange;
            }
            else if (riskValue > 25)
            {
                riskColor = Color.Yellow;
            }
            else if (riskValue > 10)
            {
                riskColor = Color.YellowGreen;
            }
            else if (riskValue > 0)
            {
                riskColor = Color.Green;
            }
            else
            {
                riskColor = Color.Gray;
            }

            return riskColor;
        }

        public static Color GetRiskColorFromValueInverted(double riskValue)
        {
            Color riskColor;

            if (riskValue > 90)
            {
                riskColor = Color.Green;
            }
            else if (riskValue > 75)
            {
                riskColor = Color.YellowGreen;
            }
            else if (riskValue > 50)
            {
                riskColor = Color.Yellow;
            }
            else if (riskValue > 25)
            {
                riskColor = Color.Orange;
            }
            else if (riskValue > 10)
            {

                riskColor = Color.OrangeRed;
            }
            else if (riskValue > 0)
            {

                riskColor = Color.Red;
            }
            else
            {
                riskColor = Color.Gray;
            }

            return riskColor;
        }

        public static JObject ConvertSettingsAttackValue(JObject apiAttack)
        {
            try
            {
                JObject ret = new JObject();
                string tmp = "[]";
                tmp = apiAttack.ContainsKey("attackComplexity") ? apiAttack["attackComplexity"].ToString() : "[]";
                ret["complex"] = JArray.Parse(tmp);

                tmp = apiAttack.ContainsKey("attackProliferation") ? apiAttack["attackProliferation"].ToString() : "[]";
                ret["prolife"] = JArray.Parse(tmp);

                tmp = apiAttack.ContainsKey("attackImpactsConfidentiality") ? apiAttack["attackImpactsConfidentiality"].ToString() : "[]";
                ret["impacts_confidentialityiality"] = JArray.Parse(tmp);

                tmp = apiAttack.ContainsKey("attackImpactsIntegrity") ? apiAttack["attackImpactsIntegrity"].ToString() : "[]";
                ret["impacts_integrity"] = JArray.Parse(tmp);

                tmp = apiAttack.ContainsKey("attackImpactsAvailibility") ? apiAttack["attackImpactsAvailibility"].ToString() : "[]";
                ret["impacts_availability"] = JArray.Parse(tmp);

                tmp = apiAttack.ContainsKey("attackImpactsAccountability") ? apiAttack["attackImpactsAccountability"].ToString() : "[]";
                ret["impacts_accountable"] = JArray.Parse(tmp);
                return ret;
            }
            catch
            {
                return null;
            }
        }

        public static JObject ConvertSettingsVulnerability(JObject apiVulnerability)
        {
            try
            {
                JObject ret = new JObject();
                string tmp = "[]";
                tmp = apiVulnerability.ContainsKey("vulnerabilityEase") ? apiVulnerability["vulnerabilityEase"].ToString() : "[]";
                ret["ease"] = JArray.Parse(tmp);

                tmp = apiVulnerability.ContainsKey("vulnerabilityExposure") ? apiVulnerability["vulnerabilityEase"].ToString() : "[]";
                ret["exposure"] = JArray.Parse(tmp);

                tmp = apiVulnerability.ContainsKey("vulnerabilityImpactsConfidentiality") ? apiVulnerability["vulnerabilityImpactsConfidentiality"].ToString() : "[]";
                ret["impacts_confidentiality"] = JArray.Parse(tmp);

                tmp = apiVulnerability.ContainsKey("vulnerabilityImpactsIntegrity") ? apiVulnerability["vulnerabilityImpactsIntegrity"].ToString() : "[]";
                ret["impacts_integrity"] = JArray.Parse(tmp);

                tmp = apiVulnerability.ContainsKey("vulnerabilityImpactsAvailibility") ? apiVulnerability["vulnerabilityImpactsAvailibility"].ToString() : "[]";
                ret["impacts_availability"] = JArray.Parse(tmp);

                tmp = apiVulnerability.ContainsKey("vulnerabilityImpactsAccountability") ? apiVulnerability["vulnerabilityImpactsAccountability"].ToString() : "[]";
                ret["impacts_accountability"] = JArray.Parse(tmp);

                tmp = apiVulnerability.ContainsKey("vulnerabilityPrivilegesRequired") ? apiVulnerability["vulnerabilityPrivilegesRequired"].ToString() : "[]";
                ret["privileges_required"] = JArray.Parse(tmp);

                tmp = apiVulnerability.ContainsKey("vulnerabilityInteractionRequired") ? apiVulnerability["vulnerabilityInteractionRequired"].ToString() : "[]";
                ret["interaction_required"] = JArray.Parse(tmp);

                tmp = apiVulnerability.ContainsKey("vulnerabilityExposesScope") ? apiVulnerability["vulnerabilityExposesScope"].ToString() : "[]";
                ret["exposes_scope"] = JArray.Parse(tmp);

                return ret;
            }
            catch
            {
                return null;
            }
        }

        public static JObject ConvertSettingsAsset(JObject apiAsset)
        {
            try
            {
                JObject ret = new JObject();
                string tmp = "[]";
                tmp = apiAsset.ContainsKey("assetConfidentiality") ? apiAsset["assetConfidentiality"].ToString() : "[]";
                ret["confidentiality"] = JArray.Parse(tmp);

                tmp = apiAsset.ContainsKey("assetIntegrity") ? apiAsset["assetIntegrity"].ToString() : "[]";
                ret["integrity"] = JArray.Parse(tmp);

                tmp = apiAsset.ContainsKey("assetAvailability") ? apiAsset["assetAvailability"].ToString() : "[]";
                ret["availability"] = JArray.Parse(tmp);

                tmp = apiAsset.ContainsKey("assetAccountability") ? apiAsset["assetAccountability"].ToString() : "[]";
                ret["accountability"] = JArray.Parse(tmp);

                tmp = apiAsset.ContainsKey("assetFinancialImpact") ? apiAsset["assetFinancialImpact"].ToString() : "[]";
                ret["financial_impact"] = JArray.Parse(tmp);

                tmp = apiAsset.ContainsKey("assetReputationalImpact") ? apiAsset["assetReputationalImpact"].ToString() : "[]";
                ret["reputational_impact"] = JArray.Parse(tmp);

                tmp = apiAsset.ContainsKey("assetRegulatoryImpact") ? apiAsset["assetRegulatoryImpact"].ToString() : "[]";
                ret["regulatory_impact"] = JArray.Parse(tmp);

                tmp = apiAsset.ContainsKey("assetLegalImpact") ? apiAsset["assetLegalImpact"].ToString() : "[]";
                ret["legal_impact"] = JArray.Parse(tmp);

                tmp = apiAsset.ContainsKey("assetPrivacyImpact") ? apiAsset["assetPrivacyImpact"].ToString() : "[]";
                ret["privacy_impact"] = JArray.Parse(tmp);

                return ret;
            }
            catch
            {
                return null;
            }
        }

        public static JObject GetEdgeDisplayData(JArray arr, double value)
        {
            JObject obj = new JObject();
            for(int i = 0; i < arr.Count; i++)
            {
                JObject tmp = arr[i] as JObject;
                double value_from = tmp.ContainsKey("valueFrom") ? double.Parse(tmp["valueFrom"].ToString()) : 1.0;
                double value_to = tmp.ContainsKey("valueTo") ? double.Parse(tmp["valueTo"].ToString()) : 1.0;
                string width = tmp.ContainsKey("width") ? tmp["width"].ToString() : "1";
                string color = tmp.ContainsKey("color") ? tmp["color"].ToString() : "rgb(0,0,0)";
                if (value >= value_from && value <= value_to)
                {
                    obj["valueFrom"] = value_from;
                    obj["valueTo"] = value_to;
                    obj["width"] = width;
                    obj["color"] = color;
                    break;
                }
            }
            return obj;
        }

       
    }
}
