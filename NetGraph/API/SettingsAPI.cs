using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CyConex.API
{
    internal class SettingsAPI
    {
        public static string _settings_guid = "";
        public static string _graph_guid = "";

        public static JArray GetSettingsSearch(string guid_type, string guid )
        {
            Graph.Utility.SaveAuditLog("GetSettingsSearch", "+++FUNCTION ENTERED+++", "", "", $"");
            JArray arr = new JArray();
            HttpResponseMessage response = null;
            try
            {
                HttpMethod verb = HttpMethod.Get;
                string sub_url = "/netgraph/v1.0/settings?userGUID=" + AuthAPI._user_guid + "&" + guid_type + "=" + guid + "&subscription-key=" + ApplicationSettings.ApiSubscriptionKey;
                string requestUrl = ApplicationSettings.ApiRootURL + sub_url;
                int httpTimeOutInSec = 60;

                response = InvokeApiEndpoint.GetOrDeleteAPI(verb, AuthAPI._account_token, requestUrl, httpTimeOutInSec);
                if (response == null) return arr;
                string statusCode = response.StatusCode.ToString();
                if (statusCode.ToLower() != "ok") return arr;
                arr = JArray.Parse(response.Content.ReadAsStringAsync().Result);
                Graph.Utility.SaveAuditLog("GetSettingsSearch", "GET", requestUrl, arr.ToString(), $"");

            }
            catch (Exception ex)
            {
                //System.Windows.Forms.MessageBox.Show("Settings Atttack Value Data is not exist" + ex.ToString());
                Graph.Utility.SaveAuditLog("GetSettingsSearch", "ERROR", ex.ToString(), "Cannot Get Settings", $"");

                return arr;
            }
            return arr;
        }

        public static string GetSettingsGUID()
        {
            if (_settings_guid != "") return _settings_guid;
            if (AuthAPI._enterprise_guid == "" || AuthAPI._tenant_guid == "") return null;

            string ret = "";
            JArray obj = new JArray();
            JObject settingObj = new JObject();
            bool flag = false;
            if (_graph_guid != "")
            {
                obj = GetSettingsSearch("graphGUID", _graph_guid);
                if (obj.Count > 0)
                {
                    flag = true;
                }
            }
            if (!flag && AuthAPI._tenant_guid != "")
            {
                obj = GetSettingsSearch("tenantGUID", AuthAPI._tenant_guid);
                if (obj.Count > 0 )
                { 
                    flag = true;
                }
            }
            if(!flag && AuthAPI._enterprise_guid != "")
            {
                obj = GetSettingsSearch("enterpriseGUID", AuthAPI._enterprise_guid);
                if (obj.Count > 0)
                {
                    flag = true;
                    settingObj = PostSettingMeta("tenantGUID", AuthAPI._tenant_guid);
                    ret = settingObj["settingsGUID"].ToString();
                }
            } 

            if (flag == false)
            {
                settingObj = PostSettingMeta("enterpriseGUID", AuthAPI._enterprise_guid);
                settingObj = PostSettingMeta("tenantGUID", AuthAPI._tenant_guid);
                ret = settingObj["settingsGUID"].ToString();
            }
            else if(ret == "")
            {
                ret = obj[0]["SettingGUID"].ToString();
            }
            _settings_guid = ret;
            return ret;
        }

        public static JObject PutSettingAttackValues(JObject obj)
        {
            if (AuthAPI._tenant_guid == "") return null;
            Graph.Utility.SaveAuditLog("PutSettingAttackValues", "+++FUNCTION ENTERED+++", "", "", $"");

            string settings_guid = _settings_guid != "" ? _settings_guid : _graph_guid;
            settings_guid = settings_guid == "" ? AuthAPI._tenant_guid : settings_guid;
            HttpResponseMessage response = null;
            JObject ret_obj = new JObject();
            try
            {

                HttpMethod verb = HttpMethod.Put;
                string sub_url = "/netgraph/v1.0/settings/" + settings_guid + "/attack-values?userGUID=" + AuthAPI._user_guid + "&subscription-key=" + ApplicationSettings.ApiSubscriptionKey;
                string requestUrl = ApplicationSettings.ApiRootURL + sub_url;
                int httpTimeOutInSec = 60;
     
                
                response = InvokeApiEndpoint.CreateOrUpdateAPI(verb, AuthAPI._account_token, requestUrl, httpTimeOutInSec, obj);
                ret_obj = JObject.Parse(response.Content.ReadAsStringAsync().Result);
                Graph.Utility.SaveAuditLog("PutSettingAttackValues", "PUT", requestUrl, ret_obj.ToString(), $"");

            }
            catch (Exception ex)
            {
                //System.Windows.Forms.MessageBox.Show("Cannot create Edge Repository, " + ex.ToString());
                Graph.Utility.SaveAuditLog("PutSettingAttackValues", "ERROR", ex.ToString(), "Cannot SetAttack Values", $"");

                return null;
            }

            return ret_obj;
        }

        public static JObject PutSettingControlValues(JObject obj)
        {
            if (AuthAPI._tenant_guid == "") return null;
            Graph.Utility.SaveAuditLog("PutSettingControlValues", "+++FUNCTION ENTERED+++", "", "", $"");

            string settings_guid = _settings_guid != "" ? _settings_guid : _graph_guid;
            settings_guid = settings_guid == "" ? AuthAPI._tenant_guid : settings_guid;
            HttpResponseMessage response = null;
            JObject ret_obj = new JObject();
            try
            {

                HttpMethod verb = HttpMethod.Put;
                string sub_url = "/netgraph/v1.0/settings/" + settings_guid + "/control-values?userGUID=" + AuthAPI._user_guid + "&subscription-key=" + ApplicationSettings.ApiSubscriptionKey;
                string requestUrl = ApplicationSettings.ApiRootURL + sub_url;
                int httpTimeOutInSec = 60;


                response = InvokeApiEndpoint.CreateOrUpdateAPI(verb, AuthAPI._account_token, requestUrl, httpTimeOutInSec, obj);
                ret_obj = JObject.Parse(response.Content.ReadAsStringAsync().Result);
                Graph.Utility.SaveAuditLog("PutSettingControlValues", "PUT", requestUrl, ret_obj.ToString(), $"");

            }
            catch (Exception ex)
            {
                //System.Windows.Forms.MessageBox.Show("Cannot create Edge Repository, " + ex.ToString());
                Graph.Utility.SaveAuditLog("PutSettingControlValues", "ERROR", ex.ToString(), "Cannot SetAttack Values", $"");

                return null;
            }

            return ret_obj;
        }

        public static JObject GetSettingsAttackValues(string settings_guid)
        {
            Graph.Utility.SaveAuditLog("GetSettingsAttackValues", "+++FUNCTION ENTERED+++", "", "", $"");
            JObject arr = new JObject();
            HttpResponseMessage response = null;
            try
            {
                HttpMethod verb = HttpMethod.Get;
                string sub_url = "/netgraph/v1.0/settings/" + settings_guid + "/attack-values?userGUID=" + AuthAPI._user_guid + "&tenantGUID=" + AuthAPI._tenant_guid + "&subscription-key=" + ApplicationSettings.ApiSubscriptionKey;
                string requestUrl = ApplicationSettings.ApiRootURL + sub_url;
                int httpTimeOutInSec = 60;

                response = InvokeApiEndpoint.GetOrDeleteAPI(verb, AuthAPI._account_token, requestUrl, httpTimeOutInSec);
                if (response == null) return arr;
                string statusCode = response.StatusCode.ToString();
                arr = JObject.Parse(response.Content.ReadAsStringAsync().Result);
                Graph.Utility.SaveAuditLog("PutSettingsMeta", "GET", requestUrl, arr.ToString(), $"");

            }
            catch (Exception ex)
            {
                //System.Windows.Forms.MessageBox.Show("Settings Atttack Value Data is not exist" + ex.ToString());
                Graph.Utility.SaveAuditLog("GetSettingsAttackValues", "ERROR", ex.ToString(), "Cannot Get Attack Values", $"");

                return arr;
            }

            return arr;
        }

        public static JObject PutSettingsVulnerabilityValues(JObject obj)
        {
            if (AuthAPI._tenant_guid == "") return null;
            Graph.Utility.SaveAuditLog("PutSettingsVulnerabilityValues", "+++FUNCTION ENTERED+++", "", "", $"");

            string settings_guid = _settings_guid != "" ? _settings_guid : _graph_guid;
            settings_guid = settings_guid == "" ? AuthAPI._tenant_guid : settings_guid;

            HttpResponseMessage response = null;
            JObject ret_obj = new JObject();
            try
            {

                HttpMethod verb = HttpMethod.Put;
                string sub_url = "/netgraph/v1.0/settings/" + settings_guid + "/vulnerability-values?userGUID=" + AuthAPI._user_guid + "&subscription-key=" + ApplicationSettings.ApiSubscriptionKey;
                string requestUrl = ApplicationSettings.ApiRootURL + sub_url;
                int httpTimeOutInSec = 60;

                response = InvokeApiEndpoint.CreateOrUpdateAPI(verb, AuthAPI._account_token, requestUrl, httpTimeOutInSec, obj);
                ret_obj = JObject.Parse(response.Content.ReadAsStringAsync().Result);
                //System.Windows.Forms.MessageBox.Show("Update Setting Vulnerability Values : " + ret_obj["edgeGUID"]);
                Graph.Utility.SaveAuditLog("PutSettingsVulnerabilityValues", "PUT", requestUrl, ret_obj.ToString(), $"");

            }
            catch (Exception ex)
            {
                //System.Windows.Forms.MessageBox.Show("Cannot create vulnerability values, " + ex.ToString());
                Graph.Utility.SaveAuditLog("PutSettingsVulnerabilityValues", "ERROR", ex.ToString(), "Cannot Set Vulnerability Values", $"");

                return null;
            }

            return ret_obj;
        }

        public static JObject GetSettingsVulnerability(string settings_guid)
        {
            Graph.Utility.SaveAuditLog("GetSettingsVulnerability", "+++FUNCTION ENTERED+++", "", "", $"");

            JObject arr = new JObject();
            HttpResponseMessage response = null;
            try
            {
                HttpMethod verb = HttpMethod.Get;
                string sub_url = "/netgraph/v1.0/settings/" + settings_guid + "/vulnerability-values?userGUID=" + AuthAPI._user_guid + "&subscription-key=" + ApplicationSettings.ApiSubscriptionKey;
                string requestUrl = ApplicationSettings.ApiRootURL + sub_url;
                int httpTimeOutInSec = 60;

                response = InvokeApiEndpoint.GetOrDeleteAPI(verb, AuthAPI._account_token, requestUrl, httpTimeOutInSec);
                if (response == null) return arr;
                string statusCode = response.StatusCode.ToString();
                arr = JObject.Parse(response.Content.ReadAsStringAsync().Result);
                Graph.Utility.SaveAuditLog("GetSettingsVulnerability", "GET", requestUrl, arr.ToString(), $"");

            }
            catch (Exception ex)
            {
                //System.Windows.Forms.MessageBox.Show("Settings Vulnerability Value Data is not exist" + ex.ToString());
                Graph.Utility.SaveAuditLog("GetSettingsVulnerability", "ERROR", ex.ToString(), "Cannot Get Vulnerability Values", $"");

                return arr;
            }

            return arr;
        }

        public static JObject PutSettingsKeywords(string keywords )
        {
            if (AuthAPI._tenant_guid == "") return null;
            Graph.Utility.SaveAuditLog("PutSettingsKeywords", "+++FUNCTION ENTERED+++", "", "", $"");

            string settings_guid = _settings_guid != "" ? _settings_guid : _graph_guid;
            settings_guid = settings_guid == "" ? AuthAPI._tenant_guid : settings_guid;

            HttpResponseMessage response = null;
            JObject ret_obj = new JObject();
            try
            {

                HttpMethod verb = HttpMethod.Put;
                string sub_url = "/netgraph/v1.0/settings/" + settings_guid + "/keywords?userGUID=" + AuthAPI._user_guid + "&subscription-key=" + ApplicationSettings.ApiSubscriptionKey;
                string requestUrl = ApplicationSettings.ApiRootURL + sub_url;
                int httpTimeOutInSec = 60;
                JObject obj = new JObject();
                obj["keywords"] = keywords;

                response = InvokeApiEndpoint.CreateOrUpdateAPI(verb, AuthAPI._account_token, requestUrl, httpTimeOutInSec, obj);
                ret_obj = JObject.Parse(response.Content.ReadAsStringAsync().Result);
                //System.Windows.Forms.MessageBox.Show("Update Setting Keywords");
                Graph.Utility.SaveAuditLog("PutSettingsKeywords", "PUT", requestUrl, ret_obj.ToString(), $"");

            }
            catch (Exception ex)
            {
                //System.Windows.Forms.MessageBox.Show("Cannot create settings keywords : " + ex.ToString());
                Graph.Utility.SaveAuditLog("PutSettingsKeywords", "ERROR", ex.ToString(), "Cannot Set Keyword Values", $"");

                return null;
            }

            return ret_obj;
        }

        public static JObject PutSettingsCategoryValues(string categories)
        {
            if (AuthAPI._tenant_guid == "") return null;
            Graph.Utility.SaveAuditLog("PutSettingsCategoryValues", "+++FUNCTION ENTERED+++", "", "", $"");

            string settings_guid = _settings_guid != "" ? _settings_guid : _graph_guid;
            settings_guid = settings_guid == "" ? AuthAPI._tenant_guid : settings_guid;

            HttpResponseMessage response = null;
            JObject ret_obj = new JObject();
            try
            {

                HttpMethod verb = HttpMethod.Put;
                string sub_url = "/netgraph/v1.0/settings/" + settings_guid + "/category-values?userGUID=" + AuthAPI._user_guid + "&subscription-key=" + ApplicationSettings.ApiSubscriptionKey;
                string requestUrl = ApplicationSettings.ApiRootURL + sub_url;
                int httpTimeOutInSec = 60;
                JObject obj = new JObject();
                obj["categories"] = categories;

                response = InvokeApiEndpoint.CreateOrUpdateAPI(verb, AuthAPI._account_token, requestUrl, httpTimeOutInSec, obj);
                ret_obj = JObject.Parse(response.Content.ReadAsStringAsync().Result);
                //System.Windows.Forms.MessageBox.Show("Update Setting Categories");
                Graph.Utility.SaveAuditLog("PutSettingsCategoryValues", "PUT", requestUrl, ret_obj.ToString(), $"");

            }
            catch (Exception ex)
            {
                //System.Windows.Forms.MessageBox.Show("Cannot create settings categories : " + ex.ToString());
                Graph.Utility.SaveAuditLog("PutSettingsCategoryValues", "ERROR", ex.ToString(), "Cannot Set Category Values", $"");

                return null;
            }

            return ret_obj;
        }

        public static JObject GetSettingsCategoryValues(string settings_guid)
        {
            Graph.Utility.SaveAuditLog("GetSettingsCategoryValues", "+++FUNCTION ENTERED+++", "", "", $"");

            JObject arr = new JObject();
            HttpResponseMessage response = null;
            try
            {
                HttpMethod verb = HttpMethod.Get;
                string sub_url = "/netgraph/v1.0/settings/" + settings_guid + "/category-values?userGUID=" + AuthAPI._user_guid + "&tenantGUID=" + AuthAPI._tenant_guid + "&subscription-key=" + ApplicationSettings.ApiSubscriptionKey;
                string requestUrl = ApplicationSettings.ApiRootURL + sub_url;
                int httpTimeOutInSec = 60;

                response = InvokeApiEndpoint.GetOrDeleteAPI(verb, AuthAPI._account_token, requestUrl, httpTimeOutInSec);
                if (response == null) return arr;
                string statusCode = response.StatusCode.ToString();
                arr = JObject.Parse(response.Content.ReadAsStringAsync().Result);
                Graph.Utility.SaveAuditLog("GetSettingsCategoryValues", "GET", requestUrl, arr.ToString(), $"");

            }
            catch (Exception ex)
            {
                //System.Windows.Forms.MessageBox.Show("Settings Category Value Data is not exist" + ex.ToString());
                Graph.Utility.SaveAuditLog("GetSettingsCategoryValues", "ERROR", ex.ToString(), "Cannot Get Category Values", $"");

                return arr;
            }

            return arr;
        }

        public static JObject PostSettingMeta(string type, string guid)
        {
            Graph.Utility.SaveAuditLog("PostSettingMeta", "+++FUNCTION ENTERED+++", "", "", $"");

            HttpResponseMessage response = null;
            JObject ret_obj = new JObject();
            try
            {
                HttpMethod verb = HttpMethod.Post;
                string sub_url = "/netgraph/v1.0/settings/meta?userGUID=" + AuthAPI._user_guid + "&subscription-key=" + ApplicationSettings.ApiSubscriptionKey;
                string requestUrl = ApplicationSettings.ApiRootURL + sub_url;
                int httpTimeOutInSec = 60;
                JObject obj = new JObject();
                obj["createdByGUID"] = AuthAPI._user_guid;
                obj["createdDate"] = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss.000");
                obj[type] = guid;

                response = InvokeApiEndpoint.CreateOrUpdateAPI(verb, AuthAPI._account_token, requestUrl, httpTimeOutInSec, obj);
                if (response == null) return null;
                ret_obj = JObject.Parse(response.Content.ReadAsStringAsync().Result);
                if (ret_obj.ContainsKey("error"))
                {
                    return null;
                }
                _settings_guid = ret_obj["settingsGUID"].ToString();
                //System.Windows.Forms.MessageBox.Show("Create Post Settings Meta");
                Graph.Utility.SaveAuditLog("PostSettingMeta", "PUT", requestUrl, ret_obj.ToString(), $"");
            }
            catch (Exception ex)
            {
                //System.Windows.Forms.MessageBox.Show("Cannot create Post Settings Meta, " + ex.ToString());
                Graph.Utility.SaveAuditLog("PostSettingMeta", "ERROR", ex.ToString(), "Cannot Set Meta Values", $"");

                return null;
            }

            return ret_obj;
        }

        public static JObject GetSettingsMeta(string settings_guid)
        {
            Graph.Utility.SaveAuditLog("GetSettingsMeta", "+++FUNCTION ENTERED+++", "", "", $"");

            JObject arr = new JObject();
            HttpResponseMessage response = null;
            try
            {
                HttpMethod verb = HttpMethod.Get;
                string sub_url = "/netgraph/v1.0/settings/meta?userGUID=" + AuthAPI._user_guid + "&tenantGUID=" + AuthAPI._tenant_guid + "&subscription-key=" + ApplicationSettings.ApiSubscriptionKey;
                string requestUrl = ApplicationSettings.ApiRootURL + sub_url;
                int httpTimeOutInSec = 60;

                response = InvokeApiEndpoint.GetOrDeleteAPI(verb, AuthAPI._account_token, requestUrl, httpTimeOutInSec);
                if (response == null) return arr;
                string statusCode = response.StatusCode.ToString();
                arr = JObject.Parse(response.Content.ReadAsStringAsync().Result);
                Graph.Utility.SaveAuditLog("GetSettingsMeta", "GET", requestUrl, arr.ToString(), $"");
            }
            catch (Exception ex)
            {
                //System.Windows.Forms.MessageBox.Show("Settings Meta Data is not exist" + ex.ToString());
                Graph.Utility.SaveAuditLog("GetSettingsMeta", "ERROR", ex.ToString(), "Cannot Get Meta Values", $"");

                return arr;
            }

            return arr;
        }

        public static JObject PutSettingsMeta(string settings_guid)
        {
            Graph.Utility.SaveAuditLog("PutSettingsMeta", "+++FUNCTION ENTERED+++", "", "", $"");

            HttpResponseMessage response = null;
            JObject ret_obj = new JObject();
            try
            {

                HttpMethod verb = HttpMethod.Put;
                string sub_url = "/netgraph/v1.0/settings/" + settings_guid + "/meta?userGUID=" + AuthAPI._user_guid + "&subscription-key=" + ApplicationSettings.ApiSubscriptionKey;
                string requestUrl = ApplicationSettings.ApiRootURL + sub_url;
                int httpTimeOutInSec = 60;
                JObject obj = new JObject();
                obj["createdByGUID"] = AuthAPI._user_guid;
                obj["createdDate"] = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss.000");
                
                response = InvokeApiEndpoint.CreateOrUpdateAPI(verb, AuthAPI._account_token, requestUrl, httpTimeOutInSec, obj);
                ret_obj = JObject.Parse(response.Content.ReadAsStringAsync().Result);
               // System.Windows.Forms.MessageBox.Show("Update Post Settings Meta");
                Graph.Utility.SaveAuditLog("PutSettingsMeta", "PUT", requestUrl, ret_obj.ToString(), $"");

            }
            catch (Exception ex)
            {
                //System.Windows.Forms.MessageBox.Show("Cannot update Post Settings Meta, " + ex.ToString());
                Graph.Utility.SaveAuditLog("PutSettingsMeta", "ERROR", ex.ToString(), "Cannot Put Settings Values", $"");

                return null;
            }

            return ret_obj;
        }

        public static JObject DeleteSettingsMeta(string settings_guid)
        {
            Graph.Utility.SaveAuditLog("DeleteSettingsMeta", "+++FUNCTION ENTERED+++", "", "", $"");

            JObject arr = new JObject();
            HttpResponseMessage response = null;
            try
            {
                HttpMethod verb = HttpMethod.Delete;
                string sub_url = "/netgraph/v1.0/settings/" + settings_guid + "/meta?userGUID=" + AuthAPI._user_guid + "&tenantGUID=" + AuthAPI._tenant_guid + "&subscription-key=" + ApplicationSettings.ApiSubscriptionKey;
                string requestUrl = ApplicationSettings.ApiRootURL + sub_url;
                JObject obj = new JObject();
                obj["createdByGUID"] = AuthAPI._user_guid;
                obj["createdDate"] = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss.000");
                obj["enterpriseGUID"] = AuthAPI._enterprise_guid;
                int httpTimeOutInSec = 60;

                response = InvokeApiEndpoint.GetOrDeleteAPI(verb, AuthAPI._account_token, requestUrl, httpTimeOutInSec);
                if (response == null) return arr;
                string statusCode = response.StatusCode.ToString();
                arr = JObject.Parse(response.Content.ReadAsStringAsync().Result);
                Graph.Utility.SaveAuditLog("DeleteSettingsMeta", "GET", requestUrl, arr.ToString(), $"");

            }
            catch (Exception ex)
            {
                //System.Windows.Forms.MessageBox.Show("Settings Meta Data is not exist" + ex.ToString());
                Graph.Utility.SaveAuditLog("DeleteSettingsMeta", "ERROR", ex.ToString(), "Cannot Delete Settings Meta", $"");

                return arr;
            }
            return arr;
        }
        public static JObject GetSettingAssetValues(string settings_guid)
        {
            Graph.Utility.SaveAuditLog("GetSettingAssetValues", "+++FUNCTION ENTERED+++", "", "", $"");

            JObject arr = new JObject();
            HttpResponseMessage response = null;
            try
            {
                HttpMethod verb = HttpMethod.Get;
                string sub_url = "/netgraph/v1.0/settings/" + settings_guid + "asset-values?userGUID=" + AuthAPI._user_guid + "&subscription-key=" + ApplicationSettings.ApiSubscriptionKey;
                string requestUrl = ApplicationSettings.ApiRootURL + sub_url;
                int httpTimeOutInSec = 60;

                response = InvokeApiEndpoint.GetOrDeleteAPI(verb, AuthAPI._account_token, requestUrl, httpTimeOutInSec);
                if (response == null) return arr;
                string statusCode = response.StatusCode.ToString();
                arr = JObject.Parse(response.Content.ReadAsStringAsync().Result);
                Graph.Utility.SaveAuditLog("GetSettingAssetValues", "GET", requestUrl, arr.ToString(), $"");
            }
            catch (Exception ex)
            {
                //System.Windows.Forms.MessageBox.Show("Settings Meta Data is not exist" + ex.ToString());
                Graph.Utility.SaveAuditLog("GetSettingAssetValues", "ERROR", ex.ToString(), "Cannot Get Settings Asset Values", $"");

                return arr;
            }

            return arr;
        }

        public static JObject PutSettingsAssetValues(JObject obj)
        {
            if (AuthAPI._tenant_guid == "") return null;
            Graph.Utility.SaveAuditLog("PutSettingsAssetValues", "+++FUNCTION ENTERED+++", "", "", $"");

            string settings_guid = _settings_guid != "" ? _settings_guid : _graph_guid;
            settings_guid = settings_guid == "" ? AuthAPI._tenant_guid : settings_guid;

            HttpResponseMessage response = null;
            JObject ret_obj = new JObject();
            try
            {

                HttpMethod verb = HttpMethod.Put;
                string sub_url = "/netgraph/v1.0/settings/" + settings_guid + "/asset-values?userGUID=" + AuthAPI._user_guid + "&subscription-key=" + ApplicationSettings.ApiSubscriptionKey;
                string requestUrl = ApplicationSettings.ApiRootURL + sub_url;
                int httpTimeOutInSec = 60;

                response = InvokeApiEndpoint.CreateOrUpdateAPI(verb, AuthAPI._account_token, requestUrl, httpTimeOutInSec, obj);
                ret_obj = JObject.Parse(response.Content.ReadAsStringAsync().Result);
                Graph.Utility.SaveAuditLog("PutSettingsAssetValues", "PUT", requestUrl, ret_obj.ToString(), $"");

            }
            catch (Exception ex)
            {
                Graph.Utility.SaveAuditLog("PutSettingsAssetValues", "ERROR", ex.ToString(), "Cannot Set Asset Values", $"");

                return null;
            }

            return ret_obj;
        }
    }
}
