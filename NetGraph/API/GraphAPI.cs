using CefSharp.DevTools.IndexedDB;
using CefSharp.DevTools.Network;
using Microsoft.Identity.Client;
using CyConex.AD;
using CyConex.Graph;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Syncfusion.Data.Extensions;
using Syncfusion.Windows.Forms.Diagram;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Interop;

namespace CyConex.API
{
    internal class GraphAPI
    {
        public static string _graph_guid = "";
        public static JObject PostGraphMeta()
        {
            Graph.Utility.SaveAuditLog("PostGraphMeta", "+++FUNCTION ENTERED+++", "", "", "");
            HttpResponseMessage response = null;
            JObject ret_obj = new JObject();
            try
            {

                HttpMethod verb = HttpMethod.Post;
                string sub_url = "/netgraph/v1.0/graphs/meta?userGUID=" + AuthAPI._user_guid + "&tenantGUID=" + AuthAPI._tenant_guid + "&subscription-key=" + ApplicationSettings.ApiSubscriptionKey;
                string requestUrl = ApplicationSettings.ApiRootURL + sub_url;
                int httpTimeOutInSec = 60;
                JObject obj = new JObject();
                obj["createdByGuid"] = AuthAPI._user_guid;
                obj["createdDateTime"] = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss.000");
                obj["tenantGUID"] = AuthAPI._tenant_guid;
                // DateTime.UtcNow.ToString("dd/MM/yyyy");

                response = InvokeApiEndpoint.CreateOrUpdateAPI(verb, AuthAPI._account_token, requestUrl, httpTimeOutInSec, obj);
                ret_obj = JObject.Parse(response.Content.ReadAsStringAsync().Result);
                _graph_guid = ret_obj["graphGUID"].ToString();
                //System.Windows.Forms.MessageBox.Show("Created! Graph Meta : ");
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    if (response.StatusCode == HttpStatusCode.Created)
                    {
                        Graph.Utility.SaveAuditLog("PostGraphMeta", "SUCCESS", HttpStatusCode.Created.ToString(), "", "Created");
                        ret_obj = JObject.Parse(response.Content.ReadAsStringAsync().Result);
                        System.Windows.Forms.MessageBox.Show(ret_obj.ToString());
                    }
                    else
                    {
                        Graph.Utility.SaveAuditLog("PostGraphMeta", "ERROR", sub_url, response.StatusCode.ToString(), "Error Updating Graph");
                        return null;
                    }

                }
                else
                {
                    Graph.Utility.SaveAuditLog("PostGraphMeta", "SUCCESS", "", "", "Posted Graph");
                }

                try
                {
                    ret_obj = JObject.Parse(response.Content.ReadAsStringAsync().Result);
                }
                catch
                (Exception ex)
                {
                    Graph.Utility.SaveAuditLog("PostGraphMeta", "ERROR", ex.Message.ToString(), "", "Unable to parse responce");
                }

            }
            catch (Exception ex)
            {
                Graph.Utility.SaveAuditLog("PostGraphFile", "ERROR", ex.Message.ToString(), "", "Unable to parse responce");
                //System.Windows.Forms.MessageBox.Show("Cannot create Graph File, " + ex.ToString());
                return null;
            }

            return ret_obj;
        }

        public static JObject PostGraphFile(JObject obj)
        {
            HttpResponseMessage response = null;
            JObject ret_obj = new JObject();
            try
            {
                Graph.Utility.SaveAuditLog("PostGraphFile", "+++FUNCTION ENTERED+++", "", "", "");
                HttpMethod verb = HttpMethod.Post;
                string sub_url = "/netgraph/v1.0/graphs/files?userGUID=" + AuthAPI._user_guid + "&tenantGUID=" + AuthAPI._tenant_guid + "&subscription-key=" + ApplicationSettings.ApiSubscriptionKey;
                string requestUrl = ApplicationSettings.ApiRootURL + sub_url;
                int httpTimeOutInSec = 60;

                response = InvokeApiEndpoint.CreateOrUpdateAPI(verb, AuthAPI._account_token, requestUrl, httpTimeOutInSec, obj);
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    if (response.StatusCode == HttpStatusCode.Created)
                    {
                        Graph.Utility.SaveAuditLog("PostGraphFile", "SUCCESS", HttpStatusCode.Created.ToString(), "", "Created");
                        ret_obj = JObject.Parse(response.Content.ReadAsStringAsync().Result);
                       // System.Windows.Forms.MessageBox.Show(ret_obj.ToString());
                    }
                    else
                    {
                        Graph.Utility.SaveAuditLog("PostGraphFile", "ERROR", sub_url, response.StatusCode.ToString(), "Error Updating Graph");
                        return null;
                    }

                }
                else
                {
                    Graph.Utility.SaveAuditLog("PostGraphFile", "SUCCESS", "", "", "Updated Graph");
                }

                try
                {
                    ret_obj = JObject.Parse(response.Content.ReadAsStringAsync().Result);
                }
                catch
                (Exception ex)
                {
                    Graph.Utility.SaveAuditLog("PostGraphFile", "ERROR", ex.Message.ToString(), "", "Unable to parse responce");
                }
                
            }
            catch (Exception ex)
            {
                Graph.Utility.SaveAuditLog("PostGraphFile", "ERROR", ex.Message.ToString(), "", "Unable to parse responce");
                //System.Windows.Forms.MessageBox.Show("Cannot create Graph File, " + ex.ToString());
                return null;
            }

            return ret_obj;
        }

        public static JObject PutGraphDetail(string graphGUID, JObject obj)
        {
            Graph.Utility.SaveAuditLog("PutGraphDetail", "+++FUNCTION ENTERED+++", "", "", $"");
            JObject ret_obj = new JObject();
            HttpResponseMessage response = null;
            try
            {
                HttpMethod verb = HttpMethod.Put;
                string sub_url = "/netgraph/v1.0/graphs/" + graphGUID + "?userGUID=" + AuthAPI._user_guid + "&tenantGUID=" + AuthAPI._tenant_guid + "&subscription-key=" + ApplicationSettings.ApiSubscriptionKey;
                string requestUrl = ApplicationSettings.ApiRootURL + sub_url;
                int httpTimeOutInSec = 60;
                
                response = InvokeApiEndpoint.CreateOrUpdateAPI(verb, AuthAPI._account_token, requestUrl, httpTimeOutInSec, obj);
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    Graph.Utility.SaveAuditLog("PutGraphDetail", "ERROR", sub_url, response.StatusCode.ToString(), "Error Updating Graph");
                    return null;
                }
                else
                {
                    Graph.Utility.SaveAuditLog("PutGraphDetail", "SUCCESS", "", "", "Updated Graph");
                }
                try
                {
                    ret_obj = JObject.Parse(response.Content.ReadAsStringAsync().Result);
                }
                catch
                (Exception ex)
                {
                    Graph.Utility.SaveAuditLog("PostGraphFile", "ERROR", ex.Message.ToString(), "", "Unable to parse responce");
                }

             }
            catch (Exception ex)
            {
             
                return null;
            }

            return ret_obj;
        }

        public static JObject PutGraphChild(string graphGUID, string childGraphGUID)
        {
            Graph.Utility.SaveAuditLog("PutGraphChild", "+++FUNCTION ENTERED+++", "", "", $"");
            JObject ret_obj = new JObject();
            HttpResponseMessage response = null;
            try
            {
                HttpMethod verb = HttpMethod.Put;
                string sub_url = "/netgraph/v1.0/graphs/" + graphGUID + "/child?userGUID=" + AuthAPI._user_guid + "&tenantGUID=" + AuthAPI._tenant_guid + "&subscription-key=" + ApplicationSettings.ApiSubscriptionKey;
                string requestUrl = ApplicationSettings.ApiRootURL + sub_url;
                int httpTimeOutInSec = 60;
                JObject obj = new JObject();
                obj["childGraphGUID"] = childGraphGUID;
            
                response = InvokeApiEndpoint.CreateOrUpdateAPI(verb, AuthAPI._account_token, requestUrl, httpTimeOutInSec, obj);
                if (response == null) return ret_obj;
                ret_obj = JObject.Parse(response.Content.ReadAsStringAsync().Result);
                //System.Windows.Forms.MessageBox.Show("Updated Graph Child!");
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    Graph.Utility.SaveAuditLog("PutGraphChild", "ERROR", sub_url, response.StatusCode.ToString(), "Error Updating Graph");
                    return null;
                }
                else
                {
                    Graph.Utility.SaveAuditLog("PutGraphChild", "SUCCESS", "", "", "Updated Graph Child GUID");
                }
                try
                {
                    ret_obj = JObject.Parse(response.Content.ReadAsStringAsync().Result);
                }
                catch
                (Exception ex)
                {
                    Graph.Utility.SaveAuditLog("PutGraphChild", "ERROR", ex.Message.ToString(), "", "Unable to parse responce");
                }
            }
            catch (Exception ex)
            {
                Graph.Utility.SaveAuditLog("PutGraphChild", "ERROR", ex.Message.ToString(), "", "Unable to updated Graph Child GUID");
                return null;
            }

            return ret_obj;
        }

        public static JObject PutGraphFile(string fileGUID, JObject obj)
        {
            Graph.Utility.SaveAuditLog("PutGraphFile", "+++FUNCTION ENTERED+++", "", "", $"");
            JObject ret_obj = new JObject();
            HttpResponseMessage response = null;
            try
            {
                HttpMethod verb = HttpMethod.Put;
                string sub_url = "/netgraph/v1.0/graphs/files/" + fileGUID + "?userGUID=" + AuthAPI._user_guid + "&tenantGUID=" + AuthAPI._tenant_guid + "&subscription-key=" + ApplicationSettings.ApiSubscriptionKey;
                string requestUrl = ApplicationSettings.ApiRootURL + sub_url;
                int httpTimeOutInSec = 60;

                response = InvokeApiEndpoint.CreateOrUpdateAPI(verb, AuthAPI._account_token, requestUrl, httpTimeOutInSec, obj);
                ret_obj = JObject.Parse(response.Content.ReadAsStringAsync().Result);
                if (response == null)
                {
                    Graph.Utility.SaveAuditLog("PutGraphFile", "ERROR", "", "", $"Null Responce");
                    return null;
                }
                else
                {
                    Graph.Utility.SaveAuditLog("PutGraphFile", "SUCCESS", "", "", $"Posted Graph File Data");
                    return ret_obj;
                }
            }
            catch (Exception ex)
            {
                Graph.Utility.SaveAuditLog("PutGraphFile", "ERROR", ex.Message, "", $"");
                return null;
            }
        }

        public static JObject PutGraphFileImage(string fileGUID, JObject obj)
        {
            Graph.Utility.SaveAuditLog("PutGraphFileImage", "+++FUNCTION ENTERED+++", "", "", $"");
            JObject ret_obj = new JObject();
            HttpResponseMessage response = null;
            try
            {
                HttpMethod verb = HttpMethod.Put;
                string sub_url = "/netgraph/v1.0/graphs/files/" + fileGUID + "/image?userGUID=" + AuthAPI._user_guid + "&tenantGUID=" + AuthAPI._tenant_guid + "&subscription-key=" + ApplicationSettings.ApiSubscriptionKey;
                string requestUrl = ApplicationSettings.ApiRootURL + sub_url;
                int httpTimeOutInSec = 60;

                response = InvokeApiEndpoint.CreateOrUpdateAPI(verb, AuthAPI._account_token, requestUrl, httpTimeOutInSec, obj);
                ret_obj = JObject.Parse(response.Content.ReadAsStringAsync().Result);
                if (response == null)
                {
                    Graph.Utility.SaveAuditLog("PutGraphFileImage", "ERROR", "", "", $"Null Responce");
                    return null;
                }
                else
                {
                    Graph.Utility.SaveAuditLog("PutGraphFileImage", "SUCCESS", "", "", $"Updated Graph File Image");
                    return ret_obj;
                }
            }
            catch (Exception ex)
            {
                Graph.Utility.SaveAuditLog("PutGraphFileImage", "ERROR", ex.Message, "", $"");
                return null;
            }
        }

        public static JObject PutGraphFileData(string fileGUID, JObject obj)
        {
            Graph.Utility.SaveAuditLog("PutGraphFileData", "+++FUNCTION ENTERED+++", "", "", $"");
            JObject ret_obj = new JObject();
            HttpResponseMessage response = null;
            try
            {
                HttpMethod verb = HttpMethod.Put;
                string sub_url = "/netgraph/v1.0/graphs/files/" + fileGUID + "/data?userGUID=" + AuthAPI._user_guid + "&tenantGUID=" + AuthAPI._tenant_guid + "&subscription-key=" + ApplicationSettings.ApiSubscriptionKey;
                string requestUrl = ApplicationSettings.ApiRootURL + sub_url;
                int httpTimeOutInSec = 60;

                response = InvokeApiEndpoint.CreateOrUpdateAPI(verb, AuthAPI._account_token, requestUrl, httpTimeOutInSec, obj);
                
                if (response == null) 
                {
                    Graph.Utility.SaveAuditLog("PutGraphFileData", "ERROR", "", "", $"Null Responce");
                    return null;
                }
                else
                {
                    Graph.Utility.SaveAuditLog("PutGraphFileData", "SUCCESS", "", "", $"Updated Graph File Data");
                    ret_obj = JObject.Parse(response.Content.ReadAsStringAsync().Result);
                }

            }
            catch (Exception ex)
            {
                Graph.Utility.SaveAuditLog("PutGraphFileData", "ERROR", "", ex.Message, $"Cannot Update Graph File Data");
                return null;
            }

            return ret_obj;
        }

        public static JObject PostGraphFile()
        {
            Graph.Utility.SaveAuditLog("PostGraphFile", "+++FUNCTION ENTERED+++", "", "", $"");
            HttpResponseMessage response = null;
            JObject ret_obj = new JObject();
            try
            {

                HttpMethod verb = HttpMethod.Post;
                string sub_url = "/netgraph/v1.0/graphs/files?userGUID=" + AuthAPI._user_guid + "&tenantGUID=" + AuthAPI._tenant_guid + "&subscription-key=" + ApplicationSettings.ApiSubscriptionKey;
                string requestUrl = ApplicationSettings.ApiRootURL + sub_url;
                int httpTimeOutInSec = 60;
                JObject obj = new JObject();
                obj["createdByGuid"] = AuthAPI._user_guid;
                obj["created_DateTime"] = DateTime.UtcNow.ToString("dd/MM/yyyy");

                response = InvokeApiEndpoint.CreateOrUpdateAPI(verb, AuthAPI._account_token, requestUrl, httpTimeOutInSec, obj);
                ret_obj = JObject.Parse(response.Content.ReadAsStringAsync().Result);
                
                if (response == null)
                {
                    Graph.Utility.SaveAuditLog("PostGraphFile", "ERROR", "", "", $"Null Responce");
                    return null;
                }
                else
                {
                    Graph.Utility.SaveAuditLog("PostGraphFile", "SUCCESS", "", "", $"Posted Graph File Data");
                    return ret_obj;
                }
            }
            catch (Exception ex)
            {
                Graph.Utility.SaveAuditLog("PostGraphFile", "ERROR", ex.Message, "", $"");
                return null;
            }

            
        }

        public static JObject GetGraphMeta()
        {
            Graph.Utility.SaveAuditLog("GetGraphMeta", "+++FUNCTION ENTERED+++", "", "", $"");
            JObject arr = new JObject();
            HttpResponseMessage response = null;
            try
            {
                HttpMethod verb = HttpMethod.Get;
                string sub_url = "/netgraph/v1.0/graphs/files?userGUID=" + AuthAPI._user_guid + "&tenantGUID=" + AuthAPI._tenant_guid + "&subscription-key=" + ApplicationSettings.ApiSubscriptionKey;
                string requestUrl = ApplicationSettings.ApiRootURL + sub_url;
                int httpTimeOutInSec = 60;

                response = InvokeApiEndpoint.GetOrDeleteAPI(verb, AuthAPI._account_token, requestUrl, httpTimeOutInSec);
                if (response == null) return arr;
                string statusCode = response.StatusCode.ToString();
                arr = JObject.Parse(response.Content.ReadAsStringAsync().Result);
                if (response == null)
                {
                    Graph.Utility.SaveAuditLog("GetGraphMeta", "ERROR", "", "", $"Null Responce");
                    return null;
                }
                else
                {
                    Graph.Utility.SaveAuditLog("GetGraphMeta", "SUCCESS", "", "", $"Posted Graph File Data");
                    return arr;
                }
            }
            catch (Exception ex)
            {
                Graph.Utility.SaveAuditLog("GetGraphMeta", "ERROR", ex.Message, "", $"");
                return null;
            }
        }

        public static JObject GetGraphChild(string graphGUID)
        {
            Graph.Utility.SaveAuditLog("GetGraphChild", "+++FUNCTION ENTERED+++", "", "", $"");
            JObject arr = new JObject();
            HttpResponseMessage response = null;
            try
            {
                HttpMethod verb = HttpMethod.Get;
                string sub_url = "/netgraph/v1.0/graphs/" + graphGUID + "/child?userGUID=" + AuthAPI._user_guid + "&tenantGUID=" + AuthAPI._tenant_guid + "&subscription-key=" + ApplicationSettings.ApiSubscriptionKey;
                string requestUrl = ApplicationSettings.ApiRootURL + sub_url;
                int httpTimeOutInSec = 60;

                response = InvokeApiEndpoint.GetOrDeleteAPI(verb, AuthAPI._account_token, requestUrl, httpTimeOutInSec);
                if (response == null) return arr;
                string statusCode = response.StatusCode.ToString();
                arr = JObject.Parse(response.Content.ReadAsStringAsync().Result);
                if (response == null)
                {
                    Graph.Utility.SaveAuditLog("GetGraphChild", "ERROR", "", "", $"Null Responce");
                    return null;
                }
                else
                {
                    Graph.Utility.SaveAuditLog("GetGraphChild", "SUCCESS", "", "", $"Posted Graph File Data");
                    return arr;
                }
            }
            catch (Exception ex)
            {
                Graph.Utility.SaveAuditLog("GetGraphChild", "ERROR", ex.Message, "", $"");
                return null;
            }
        }

        public static JObject GetGraphDetail(string graphGUID)
        {
            Graph.Utility.SaveAuditLog("GetGraphDetail", "+++FUNCTION ENTERED+++", "", "", $"");
            JObject arr = new JObject();
            HttpResponseMessage response = null;
            try
            {
                HttpMethod verb = HttpMethod.Get;
                string sub_url = "/netgraph/v1.0/graphs/" + graphGUID + "?userGUID=" + AuthAPI._user_guid + "&tenantGUID=" + AuthAPI._tenant_guid + "&subscription-key=" + ApplicationSettings.ApiSubscriptionKey;
                string requestUrl = ApplicationSettings.ApiRootURL + sub_url;
                int httpTimeOutInSec = 60;

                response = InvokeApiEndpoint.GetOrDeleteAPI(verb, AuthAPI._account_token, requestUrl, httpTimeOutInSec);
                if (response == null) return arr;
                string statusCode = response.StatusCode.ToString();
                arr = JObject.Parse(response.Content.ReadAsStringAsync().Result);
                if (response == null)
                {
                    Graph.Utility.SaveAuditLog("GetGraphDetail", "ERROR", "", "", $"Null Responce");
                    return null;
                }
                else
                {
                    Graph.Utility.SaveAuditLog("GetGraphDetail", "SUCCESS", "", "", $"Posted Graph Detail");
                    return arr;
                }
            }
            catch (Exception ex)
            {
                Graph.Utility.SaveAuditLog("GetGraphDetail", "ERROR", ex.Message, "", $"");
                return null;
            }
        }

        public static JObject GetGraphFileImage(string fileGUID)
        {
            Graph.Utility.SaveAuditLog("GetGraphFileImage", "+++FUNCTION ENTERED+++", "", "", $"");
            JObject arr = new JObject();
            HttpResponseMessage response = null;
            try
            {
                HttpMethod verb = HttpMethod.Get;
                string sub_url = "/netgraph/v1.0/graphs/files/" + fileGUID + "/image?userGUID=" + AuthAPI._user_guid + "&tenantGUID=" + AuthAPI._tenant_guid + "&subscription-key=" + ApplicationSettings.ApiSubscriptionKey;
                string requestUrl = ApplicationSettings.ApiRootURL + sub_url;
                int httpTimeOutInSec = 60;

                response = InvokeApiEndpoint.GetOrDeleteAPI(verb, AuthAPI._account_token, requestUrl, httpTimeOutInSec);
                if (response == null) return arr;
                string statusCode = response.StatusCode.ToString();
                arr = JObject.Parse(response.Content.ReadAsStringAsync().Result);
                if (response == null)
                {
                    Graph.Utility.SaveAuditLog("GetGraphFileImage", "ERROR", "", "", $"Null Responce");
                    return null;
                }
                else
                {
                    Graph.Utility.SaveAuditLog("GetGraphFileImage", "SUCCESS", "", "", $"Posted Graph Detail");
                    return arr;
                }
            }
            catch (Exception ex)
            {
                Graph.Utility.SaveAuditLog("GetGraphFileImage", "ERROR", ex.Message, "", $"");
                return null;
            }
        }

        public static JObject GetGraphFileData(string fileGUID)
        {
            Graph.Utility.SaveAuditLog("GetGraphFileData", "+++FUNCTION ENTERED+++", "", "", $"");
            JObject arr = new JObject();
            HttpResponseMessage response = null;
            try
            {
                HttpMethod verb = HttpMethod.Get;
                string sub_url = "/netgraph/v1.0/graphs/files/" + fileGUID + "/data?userGUID=" + AuthAPI._user_guid + "&tenantGUID=" + AuthAPI._tenant_guid + "&subscription-key=" + ApplicationSettings.ApiSubscriptionKey;
                string requestUrl = ApplicationSettings.ApiRootURL + sub_url;
                int httpTimeOutInSec = 60;

                response = InvokeApiEndpoint.GetOrDeleteAPI(verb, AuthAPI._account_token, requestUrl, httpTimeOutInSec);
                if (response == null) return arr;
                string statusCode = response.StatusCode.ToString();
                arr = JObject.Parse(response.Content.ReadAsStringAsync().Result);
                if (response == null)
                {
                    Graph.Utility.SaveAuditLog("GetGraphFileData", "ERROR", "", "", $"Null Responce");
                    return null;
                }
                else
                {
                    Graph.Utility.SaveAuditLog("GetGraphFileData", "SUCCESS", "", "", $"Posted Graph Detail");
                    return arr;
                }
            }
            catch (Exception ex)
            {
                Graph.Utility.SaveAuditLog("GetGraphFileData", "ERROR", ex.Message, "", $"");
                return null;
            }
        }

        public static JObject PutGraphMeta(string graphGUID, JObject obj)
        {
            Graph.Utility.SaveAuditLog("PutGraphMeta", "+++FUNCTION ENTERED+++", "", "", $"");
            JObject ret_obj = new JObject();
            HttpResponseMessage response = null;
            try
            {
                HttpMethod verb = HttpMethod.Put;
                string sub_url = "/netgraph/v1.0/graphs/" + graphGUID + "/meta?" + "?userGUID=" + AuthAPI._user_guid + "&tenantGUID=" + AuthAPI._tenant_guid + "&subscription-key=" + ApplicationSettings.ApiSubscriptionKey;
                string requestUrl = ApplicationSettings.ApiRootURL + sub_url;
                int httpTimeOutInSec = 60;

                response = InvokeApiEndpoint.CreateOrUpdateAPI(verb, AuthAPI._account_token, requestUrl, httpTimeOutInSec, obj);
                ret_obj = JObject.Parse(response.Content.ReadAsStringAsync().Result);
                if (response == null)
                {
                    Graph.Utility.SaveAuditLog("PutGraphMeta", "ERROR", "", "", $"Null Responce");
                    return null;
                }
                else
                {
                    Graph.Utility.SaveAuditLog("PutGraphMeta", "SUCCESS", "", "", $"Posted Graph Detail");
                    return ret_obj;
                }
            }
            catch (Exception ex)
            {
                Graph.Utility.SaveAuditLog("PutGraphMeta", "ERROR", ex.Message, "", $"");
                return null;
            }
        }

        public static JObject GetGraphFile(string fileGUID)
        {
            JObject arr = new JObject();
            HttpResponseMessage response = null;
            try
            {
                HttpMethod verb = HttpMethod.Get;
                string sub_url = "/netgraph/v1.0/graphs/files/" + fileGUID + "?userGUID=" + AuthAPI._user_guid + "&tenantGUID=" + AuthAPI._tenant_guid + "&subscription-key=" + ApplicationSettings.ApiSubscriptionKey;
                string requestUrl = ApplicationSettings.ApiRootURL + sub_url;
                int httpTimeOutInSec = 60;

                response = InvokeApiEndpoint.GetOrDeleteAPI(verb, AuthAPI._account_token, requestUrl, httpTimeOutInSec);
                if (response == null) { return arr; }
                string statusCode = response.StatusCode.ToString();
                arr = JObject.Parse(response.Content.ReadAsStringAsync().Result);
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Graph File is not exist" + ex.ToString());
                return arr;
            }

            return arr;
        }

        public static JArray DeleteGraphFile(string fileGUID)
        {
            JArray arr = new JArray();
            HttpResponseMessage response = null;
            try
            {
                HttpMethod verb = HttpMethod.Get;
                string sub_url = "/netgraph/v1.0/graphs/files/" + fileGUID + "?userGUID=" + AuthAPI._user_guid + "&tenantGUID=" + AuthAPI._tenant_guid + "&subscription-key=" + ApplicationSettings.ApiSubscriptionKey;
                string requestUrl = ApplicationSettings.ApiRootURL + sub_url;
                int httpTimeOutInSec = 60;

                response = InvokeApiEndpoint.GetOrDeleteAPI(verb, AuthAPI._account_token, requestUrl, httpTimeOutInSec);
                if (response == null) return arr;
                string statusCode = response.StatusCode.ToString();
                arr = JArray.Parse(response.Content.ReadAsStringAsync().Result);
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Graph File is not exist" + ex.ToString());
                return arr;
            }

            return arr;
        }

        public static JArray DeleteGraphMeta(string graphGUID)
        {
            JArray arr = new JArray();
            HttpResponseMessage response = null;
            try
            {
                HttpMethod verb = HttpMethod.Delete;
                string sub_url = "/netgraph/v1.0/graphs/" + graphGUID + "/meta?userGUID=" + AuthAPI._user_guid + "&tenantGUID=" + AuthAPI._tenant_guid + "&subscription-key=" + ApplicationSettings.ApiSubscriptionKey;
                string requestUrl = ApplicationSettings.ApiRootURL + sub_url;
                int httpTimeOutInSec = 60;

                response = InvokeApiEndpoint.GetOrDeleteAPI(verb, AuthAPI._account_token, requestUrl, httpTimeOutInSec);
                if (response == null) return arr;
                string statusCode = response.StatusCode.ToString();
                arr = JArray.Parse(response.Content.ReadAsStringAsync().Result);
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Graph Meta does not exist" + ex.ToString());
                return arr;
            }

            return arr;
        }

        public static JArray GetTenantGraphs()
        {
            JArray arr = new JArray();
            HttpResponseMessage response = null;
            try
            {
                HttpMethod verb = HttpMethod.Get;
                string sub_url = "/netgraph/v1.0/graphs/tenants/" + AuthAPI._tenant_guid + "?userGUID=" + AuthAPI._user_guid + "&subscription-key=" + ApplicationSettings.ApiSubscriptionKey;
                string requestUrl = ApplicationSettings.ApiRootURL + sub_url;
                int httpTimeOutInSec = 60;

                response = InvokeApiEndpoint.GetOrDeleteAPI(verb, AuthAPI._account_token, requestUrl, httpTimeOutInSec);
                string statusCode = response.StatusCode.ToString();
                if (response == null || statusCode.ToLower() == "ok") return arr;
                arr = JArray.Parse(response.Content.ReadAsStringAsync().Result);
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Graph Tenant Graphs is not exist" + ex.ToString());
                return arr;
            }

            return arr;
        }

        public static JArray GetUserGraphs()
        {
            JArray arr = new JArray();
            HttpResponseMessage response = null;
            try
            {
                HttpMethod verb = HttpMethod.Get;
                string sub_url = "/netgraph/v1.0/graphs/users/" + AuthAPI._user_guid + "?tenantGUID=" + AuthAPI._tenant_guid + "&subscription-key=" + ApplicationSettings.ApiSubscriptionKey;
                string requestUrl = ApplicationSettings.ApiRootURL + sub_url;
                int httpTimeOutInSec = 60;

                response = InvokeApiEndpoint.GetOrDeleteAPI(verb, AuthAPI._account_token, requestUrl, httpTimeOutInSec);
                if (response == null) return arr;
                string statusCode = response.StatusCode.ToString();
                arr = JArray.Parse(response.Content.ReadAsStringAsync().Result);
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Graph User Graphs is not exist" + ex.ToString());
                return arr;
            }

            return arr;
        }

    }
}
