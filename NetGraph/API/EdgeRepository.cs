using CyConex.Graph;
using Newtonsoft.Json.Linq;
using Syncfusion.Windows.Forms.Diagram;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyConex.API
{
    public class EdgeRepository
    {
        public static JArray EdgeRepositoryArray = new JArray();
        
        public static JArray InitLinkedNodes()
        {
            Graph.Utility.SaveAuditLog("InitLinkedNodes", "+++FUNCTION ENTERED+++", "", "", "");
            try
            {
                JArray arr = EdgeAPI.GetRepoEdges();
                JArray ret = new JArray();
                if (arr == null) return null;
                foreach (JObject obj in arr)
                {
                    JObject edgeObj = EdgeAPI.GetRepoEdgeDetail(obj["edgeGUID"].ToString());
                    EdgeRepositoryArray.Add(edgeObj);
                    ret.Add(edgeObj);
                }
                return ret;
            }
            catch (Exception ex)
            {
                //System.Windows.Forms.MessageBox.Show("Cannot get edge list : " + ex.ToString());
                Graph.Utility.SaveAuditLog("InitLinkedNodes", "ERROR", "", ex.ToString(), "Cannot get edge list");
                return null;
            }
        }

        public static void SetRepositoryList(JArray list)
        {
            EdgeRepositoryArray = list;
        }

        public static JObject GetRepositoryList(string guid)
        {
            Graph.Utility.SaveAuditLog("GetRepositoryList", "+++FUNCTION ENTERED+++", "", "", "");
            for (int i = 0; i < EdgeRepositoryArray.Count; i++)
            {
                JObject edge = EdgeRepositoryArray[i] as JObject;
                if (edge["edgeGUID"].ToString() == guid)
                {
                    return edge;
                }
            }
            return null;
        }
    }
}
