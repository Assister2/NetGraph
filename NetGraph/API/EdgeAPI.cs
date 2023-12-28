using CefSharp.DevTools.IndexedDB;
using CyConex.Graph;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.VisualStyles;

namespace CyConex.API
{
    public class EdgeAPI
    {
        public static JObject PostRepoEdge(string sourceNodeGUID, string targetNodeGUID, string edgeRelationship, string edgeRelationshipStrength, string edgeTitle, string edgeDescription, string edgeValue )
        {
            Graph.Utility.SaveAuditLog("PostRepoEdge", "+++FUNCTION ENTERED+++", "", "", "");
            HttpResponseMessage response = null;
            JObject ret_obj = new JObject();
            try
            {

                HttpMethod verb = HttpMethod.Post;
                string sub_url = "/netgraph/v1.0/nodes/edges?userGUID=" + AuthAPI._user_guid + "&subscription-key=" + ApplicationSettings.ApiSubscriptionKey;
                string requestUrl = ApplicationSettings.ApiRootURL + sub_url;
                int httpTimeOutInSec = 60;
                JObject obj = new JObject();
                obj["sourceNodeGUID"] = sourceNodeGUID;
                obj["targetNodeGUID"] = targetNodeGUID;
                obj["edgeRelationship"] = edgeRelationship;
                obj["edgeTitle"] = edgeTitle;
                obj["edgeRelationshipStrength"] = edgeRelationshipStrength;
                obj["edgeDescription"] = edgeDescription;
                obj["edgeValue"] = edgeValue;

                response = InvokeApiEndpoint.CreateOrUpdateAPI(verb, AuthAPI._account_token, requestUrl, httpTimeOutInSec, obj);
                ret_obj = JObject.Parse(response.Content.ReadAsStringAsync().Result);
                //System.Windows.Forms.MessageBox.Show("Created! Edge Repository GUID : " + ret_obj["edgeGUID"]);
                Graph.Utility.SaveAuditLog("PostRepoEdge", "POST", requestUrl, response.StatusCode.ToString(), "Edge Created");
            }
            catch (Exception ex)
            {
                //System.Windows.Forms.MessageBox.Show("Cannot create Edge Repository, " + ex.ToString());
                Graph.Utility.SaveAuditLog("PostRepoEdge", "ERROR", "", ex.ToString(), "Cannot create Edge");
                return null;
            }

            return ret_obj;
        }

        public static JArray GetRepoEdges()
        {
            Graph.Utility.SaveAuditLog("GetRepoEdges", "+++FUNCTION ENTERED+++", "", "", "");
            JArray arr = new JArray();
            HttpResponseMessage response = null;
            try
            {
                HttpMethod verb = HttpMethod.Get;
                string sub_url = "/netgraph/v1.0/nodes/edges?userGUID=" + AuthAPI._user_guid + "&subscription-key=" + ApplicationSettings.ApiSubscriptionKey + "&hasDeleted=false" ;
                string requestUrl = ApplicationSettings.ApiRootURL + sub_url;
                int httpTimeOutInSec = 60;

                response = InvokeApiEndpoint.GetOrDeleteAPI(verb, AuthAPI._account_token, requestUrl, httpTimeOutInSec);
                string statusCode = response.StatusCode.ToString();
                Graph.Utility.SaveAuditLog("GetRepoEdges", "GET", requestUrl, response.StatusCode.ToString(), "Get Edges");
                arr = JArray.Parse(response.Content.ReadAsStringAsync().Result);

            }
            catch (Exception ex)
            {
                //System.Windows.Forms.MessageBox.Show("Cannot get edge list : " + ex.ToString());
                Graph.Utility.SaveAuditLog("PostRepoEdge", "ERROR", "", ex.ToString(), "Cannot get edge list");
                return null;
            }

            return arr;
        }

        public static JObject GetRepoEdgeDetail(string edge_id)
        {
            Graph.Utility.SaveAuditLog("GetRepoEdgeDetail", "+++FUNCTION ENTERED+++", "", "", "");
            JObject obj = new JObject();
            HttpResponseMessage response = null;
            try
            {
                HttpMethod verb = HttpMethod.Get;
                string sub_url = "/netgraph/v1.0/nodes/edges/" + edge_id + "?userGUID=" + AuthAPI._user_guid + "&subscription-key=" + ApplicationSettings.ApiSubscriptionKey + "&hasDeleted=false";
                string requestUrl = ApplicationSettings.ApiRootURL + sub_url;
                int httpTimeOutInSec = 60;

                response = InvokeApiEndpoint.GetOrDeleteAPI(verb, AuthAPI._account_token, requestUrl, httpTimeOutInSec);
                string statusCode = response.StatusCode.ToString();
                Graph.Utility.SaveAuditLog("GetRepoEdgeDetail", "GET", requestUrl, response.StatusCode.ToString(), "Get Edge Detail");
                obj = JObject.Parse(response.Content.ReadAsStringAsync().Result);
            }
            catch (Exception ex)
            {
                //System.Windows.Forms.MessageBox.Show("Cannot get repo edge detail : " + ex.ToString());
                Graph.Utility.SaveAuditLog("GetRepoEdgeDetail", "ERROR", "", ex.ToString(), "Cannot get edge Detail");
                return null;
            }

            return obj;
        }

        public static string DeleteRepoEdge(string edgeGUID)
        {
            Graph.Utility.SaveAuditLog("DeleteRepoEdge", "+++FUNCTION ENTERED+++", "", "", "");
            string result = "";

            HttpResponseMessage response = null;
            try
            {
                HttpMethod verb = HttpMethod.Delete;
                string sub_url = "/netgraph/v1.0/nodes/edges/" + edgeGUID + "?userGUID=" + AuthAPI._user_guid + "&subscription-key=" + ApplicationSettings.ApiSubscriptionKey;
                string requestUrl = ApplicationSettings.ApiRootURL + sub_url;
                int httpTimeOutInSec = 60;

                response = InvokeApiEndpoint.CreateOrUpdateAPI(verb, AuthAPI._account_token, requestUrl, httpTimeOutInSec);
                result = response.Content.ReadAsStringAsync().Result.ToString();
                Graph.Utility.SaveAuditLog("GetRepoEdgeDetail", "DELETE", requestUrl, response.StatusCode.ToString(), "Edge Deleted");
                //System.Windows.Forms.MessageBox.Show("Deleted Edge Repository!");
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Cannot delete Edge Repository, " + ex.ToString());
                Graph.Utility.SaveAuditLog("DeleteRepoEdge", "ERROR", "", ex.ToString(), "Cannot get delete edge");
                return null;
            }

            return result;
        }
    }
}
