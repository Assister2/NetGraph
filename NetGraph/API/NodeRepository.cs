using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CyConex.Graph;
using Newtonsoft.Json.Linq;

namespace CyConex.API
{
    public class NodeRepository
    {
        public static List<Node> NodeRepositoryList = new List<Node>();

        public static List<Node> LoadRepositoryList(string searchIn = "Title,Reference,Description,Framework,Notes", string filterByType = "", string searchText = "*")
        {
            JArray arr = NodeAPI.GetRepoNodeList(searchIn, filterByType, searchText );
            NodeRepositoryList.Clear();

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
                    NodeRepositoryList.Add(tmp_node);
                }
            }
            return NodeRepositoryList;
        }

        public static Node GetNodeDataFromID(string nodeGUID)
        {
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
            }
            return tmp_node;
        }

        public static void SetRepositoryList(List<Node> list)
        {
            NodeRepositoryList = list;
        }

        public static Node GetRepositoryList(string guid)
        {
            for(int i = 0; i < NodeRepositoryList.Count; i++)
            {
                Node node = NodeRepositoryList[i];
                if (node.ID == guid)
                {
                    return node;
                }
            }
            return null;
        }

        public static void AddRepositoryData(Node node, bool flag = false)
        {            
            if (flag)
            {
                NodeAPI.AddNodeToServer(node);
            }
            NodeRepositoryList.Add(node);
        }

        public static void RemoveRepositoryDataWithNode(Node node, bool flag = false)
        {
            if (flag)
            {
                NodeAPI.DeleteNodeMeta(node.ID);
            }
            NodeRepositoryList.Remove(node);   
        }

        public static void RemoveRepositoryDataWithID(string guid, bool flag = false)
        {
            if (flag)
            {
                NodeAPI.DeleteNodeMeta(guid);
            }

            for (int i = 0; i < NodeRepositoryList.Count; i++)
            {
                Node node = NodeRepositoryList[i];
                if (node.ID == guid)
                {
                    NodeRepositoryList.RemoveAt(i);
                    break;
                }
            }
        }

        public static void UpdateRepositoryData(string guid, Node node)
        {
            for (int i = 0; i < NodeRepositoryList.Count; i++)
            {
                Node tmp_node = NodeRepositoryList[i];
                if (tmp_node.ID == guid)
                {
                    NodeRepositoryList[i] = tmp_node;
                    //NodeAPI.UpdateNodeToServer(tmp_node);
                    break;
                }
            }
        }
    }
}
