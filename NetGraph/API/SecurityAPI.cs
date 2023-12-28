using CefSharp.DevTools.Network;
using CyConex.API;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CyConex.API
{
    public class SecurityAPI
    {
        public static JObject GetSecurityGroupDetail(string object_id, string group_id)
        {
            Graph.Utility.SaveAuditLog("GetSecurityGroupObjectDetail", "+++FUNCTION ENTERED+++", "", "", $"");
            JObject ret_obj = new JObject();
            HttpResponseMessage response = null;
            string requestUrl = ApplicationSettings.ApiRootURL + "/netgraph/v1.0/security/objects/" + object_id + "/grouops/" + group_id + "?userGUID=" + AuthAPI._user_guid + "&subscription-key=" + ApplicationSettings.ApiSubscriptionKey;
            try
            {
                Graph.Utility.SaveAuditLog("GetSecurityGroupObjectDetail", "GET", "", "", "GetSecurityGroupObjectDetail");
                HttpMethod verb = HttpMethod.Get;
                int httpTimeOutInSec = 60;

                response = InvokeApiEndpoint.GetOrDeleteAPI(verb, AuthAPI._account_token, requestUrl, httpTimeOutInSec);
                string statusCode = response.StatusCode.ToString();
                ret_obj = JObject.Parse(response.Content.ReadAsStringAsync().Result);
                Graph.Utility.SaveAuditLog("GetSecurityGroupObjectDetail", "GET", requestUrl, response.StatusCode.ToString(), "GetSecurityGroupObjectDetail");
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("GetSecurityGroupObjectDetail is not exist, " + ex.ToString());
                Graph.Utility.SaveAuditLog("GetSecurityGroupObjectDetail", "GET", requestUrl, response.StatusCode.ToString(), "Cannot get detail info");
                return null;

            }
            return ret_obj;
        }

        public static JObject GetSecurityGroupDetail(string group_id)
        {
            Graph.Utility.SaveAuditLog("GetSecurityGroupDetail", "+++FUNCTION ENTERED+++", "", "", $"");
            JObject ret_obj = new JObject();
            HttpResponseMessage response = null;
            string requestUrl = ApplicationSettings.ApiRootURL + "/netgraph/v1.0/security/groups/" + group_id + "?userGUID=" + AuthAPI._user_guid + "&subscription-key=" + ApplicationSettings.ApiSubscriptionKey;
            try
            {
                Graph.Utility.SaveAuditLog("GetSecurityGroupDetail", "GET", "", "", "GetSecurityGroupDetail");
                HttpMethod verb = HttpMethod.Get;
                int httpTimeOutInSec = 60;

                response = InvokeApiEndpoint.GetOrDeleteAPI(verb, AuthAPI._account_token, requestUrl, httpTimeOutInSec);
                string statusCode = response.StatusCode.ToString();
                ret_obj = JObject.Parse(response.Content.ReadAsStringAsync().Result);
                Graph.Utility.SaveAuditLog("GetSecurityGroupDetail", "GET", requestUrl, response.StatusCode.ToString(), "GetSecurityGroupDetail");
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("GetSecurityGroupDetail is not exist, " + ex.ToString());
                Graph.Utility.SaveAuditLog("GetSecurityGroupDetail", "GET", requestUrl, response.StatusCode.ToString(), "Cannot get detail info");
                return null;

            }
            return ret_obj;
        }

        public static JArray GetSecurityGroupMemebers(string group_id)
        {
            Graph.Utility.SaveAuditLog("GetSecurityGroupMemebers", "+++FUNCTION ENTERED+++", "", "", $"");
            JArray ret_obj = new JArray();
            HttpResponseMessage response = null;
            string requestUrl = ApplicationSettings.ApiRootURL + "/netgraph/v1.0/security/groups/" + group_id + "/users?userGUID=" + AuthAPI._user_guid + "&subscription-key=" + ApplicationSettings.ApiSubscriptionKey;
            try
            {
                Graph.Utility.SaveAuditLog("GetSecurityGroupMemebers", "GET", "", "", "GetSecurityGroupMemebers");
                HttpMethod verb = HttpMethod.Get;
                int httpTimeOutInSec = 60;

                response = InvokeApiEndpoint.GetOrDeleteAPI(verb, AuthAPI._account_token, requestUrl, httpTimeOutInSec);
                string statusCode = response.StatusCode.ToString().ToLower();
                if (statusCode  == "ok")
                {
                    ret_obj = JArray.Parse(response.Content.ReadAsStringAsync().Result);
                }
                Graph.Utility.SaveAuditLog("GetSecurityGroupMemebers", "GET", requestUrl, response.StatusCode.ToString(), "GetSecurityGroupMemebers");
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("GetSecurityGroupMemebers is not exist, " + ex.ToString());
                Graph.Utility.SaveAuditLog("GetSecurityGroupMemebers", "GET", requestUrl, response.StatusCode.ToString(), "Cannot get info");
                return null;

            }
            return ret_obj;
        }

        public static JObject PostCreateDefaultSecurityGroupsForObject(string group_id)
        {
            Graph.Utility.SaveAuditLog("Post CreateDefaultSecurityGroupsForObject", "+++FUNCTION ENTERED+++", "", "", $"");
            HttpResponseMessage response = null;
            JObject ret_obj;

            string requestUrl = ApplicationSettings.ApiRootURL + "/netgraph/v1.0/security/groups?userGUID=" + AuthAPI._user_guid + "&subscription-key=" + ApplicationSettings.ApiSubscriptionKey;
            try
            {
                Graph.Utility.SaveAuditLog("CreateDefaultSecurityGroupsForObject", "POST", "", "", "CreateDefaultSecurityGroupsForObject");
                HttpMethod verb = HttpMethod.Post;

                int httpTimeOutInSec = 60;
                JObject obj = new JObject();
                obj["enterpriseID"] = AuthAPI._enterprise_guid;

                response = InvokeApiEndpoint.CreateOrUpdateAPI(verb, AuthAPI._account_token, requestUrl, httpTimeOutInSec, obj);
                ret_obj = JObject.Parse(response.Content.ReadAsStringAsync().Result);
                //System.Windows.Forms.MessageBox.Show("Created! Enterprise GUID : " + id);
                Graph.Utility.SaveAuditLog("Post CreateDefaultSecurityGroupsForObject", "Post", requestUrl, response.StatusCode.ToString(), "CreateDefaultSecurityGroupsForObject");
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Cannot create the DefaultSecurityGroupsForObject," + ex.ToString());
                Graph.Utility.SaveAuditLog("Create DefaultSecurityGroupsForObject", "Post", requestUrl, response.StatusCode.ToString(), "Cannot create DefaultSecurityGroupsForObject");
                return null;
            }

            return ret_obj;
        }

        public static JObject PutSecurityGroup(string group_id, string group_type, string group_role, string group_name, string group_desc)
        {
            JObject ret_obj = new JObject();
            HttpResponseMessage response = null;
            try
            {
                HttpMethod verb = HttpMethod.Put;
                string sub_url = "/netgraph/v1.0/security/groups/" + group_id + "?userGUID=" + AuthAPI._user_guid + "&subscription-key=" + ApplicationSettings.ApiSubscriptionKey;
                string requestUrl = ApplicationSettings.ApiRootURL + sub_url;
                int httpTimeOutInSec = 60;
                JObject obj = new JObject();
                obj["enterpriseID"] = AuthAPI._enterprise_guid;
                obj["groupType"] = group_type;
                obj["groupRole"] = group_role;
                obj["groupName"] = group_name;
                obj["groupDescription"] = group_desc;
                response = InvokeApiEndpoint.CreateOrUpdateAPI(verb, AuthAPI._account_token, requestUrl, httpTimeOutInSec, obj);
                ret_obj = JObject.Parse(response.Content.ReadAsStringAsync().Result);
                System.Windows.Forms.MessageBox.Show("Put Security Group!");
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Cannot update Security Group, " + ex.ToString());
                return null;
            }

            return ret_obj;
        }

        public static JObject PutSecurityGroupDefaultPermissions(string group_id, string create_p, string read_p, string update_p, string delete_p)
        {
            JObject ret_obj = new JObject();
            HttpResponseMessage response = null;
            try
            {
                HttpMethod verb = HttpMethod.Put;
                string sub_url = "/netgraph/v1.0/security/groups/" + group_id + "/default-permission?userGUID=" + AuthAPI._user_guid + "&subscription-key=" + ApplicationSettings.ApiSubscriptionKey;
                string requestUrl = ApplicationSettings.ApiRootURL + sub_url;
                int httpTimeOutInSec = 60;
                JObject obj = new JObject();
                obj["create"] = create_p;
                obj["read"] = read_p;
                obj["update"] = update_p;
                obj["delete"] = delete_p;
                response = InvokeApiEndpoint.CreateOrUpdateAPI(verb, AuthAPI._account_token, requestUrl, httpTimeOutInSec, obj);
                ret_obj = JObject.Parse(response.Content.ReadAsStringAsync().Result);
                System.Windows.Forms.MessageBox.Show("Put SecurityGroupDefaultPermissions!");
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Cannot update SecurityGroupDefaultPermissions, " + ex.ToString());
                return null;
            }

            return ret_obj;
        }

        public static JObject DeleteSecurityGroup(string group_id)
        {
            JObject ret_obj = new JObject();
            HttpResponseMessage response = null;
            try
            {
                HttpMethod verb = HttpMethod.Delete;
                string sub_url = "/netgraph/v1.0/security/groups/" + group_id + "?userGUID=" + AuthAPI._user_guid + "&subscription-key=" + ApplicationSettings.ApiSubscriptionKey;
                string requestUrl = ApplicationSettings.ApiRootURL + sub_url;
                int httpTimeOutInSec = 60;

                response = InvokeApiEndpoint.GetOrDeleteAPI(verb, AuthAPI._account_token, requestUrl, httpTimeOutInSec);
                if (response == null) return ret_obj;
                string statusCode = response.StatusCode.ToString();
                ret_obj = JObject.Parse(response.Content.ReadAsStringAsync().Result);
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Security Group is not exist" + ex.ToString());
                return ret_obj;
            }

            return ret_obj;
        }

        public static JObject GetSecurityGroupDefaultPermissions(string group_id)
        {
            Graph.Utility.SaveAuditLog("GetSecurityGroupDefaultPermissions", "+++FUNCTION ENTERED+++", "", "", $"");
            JObject ret_obj = new JObject();
            HttpResponseMessage response = null;
            string requestUrl = ApplicationSettings.ApiRootURL + "/netgraph/v1.0/security/groups/" + group_id + "?userGUID=" + AuthAPI._user_guid + "&subscription-key=" + ApplicationSettings.ApiSubscriptionKey;
            try
            {
                Graph.Utility.SaveAuditLog("GetSecurityGroupDefaultPermissions", "GET", "", "", "GetSecurityGroupDefaultPermissions");
                HttpMethod verb = HttpMethod.Get;
                int httpTimeOutInSec = 60;

                response = InvokeApiEndpoint.GetOrDeleteAPI(verb, AuthAPI._account_token, requestUrl, httpTimeOutInSec);
                
                string statusCode = response.StatusCode.ToString();
                ret_obj = JObject.Parse(response.Content.ReadAsStringAsync().Result);
                Graph.Utility.SaveAuditLog("GetSecurityGroupDefaultPermissions", "GET", requestUrl, response.StatusCode.ToString(), "GetSecurityGroupDefaultPermissions");
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("GetSecurityGroupDefaultPermissions is not exist, " + ex.ToString());
                Graph.Utility.SaveAuditLog("GetSecurityGroupDefaultPermissions", "GET", requestUrl, response.StatusCode.ToString(), "Cannot get info");
                return null;

            }
            return ret_obj;
        }

        public static JObject DeleteSecurityGroupUsers(string user_id, string group_id)
        {
            JObject ret_obj = new JObject();
            HttpResponseMessage response = null;
            try
            {
                HttpMethod verb = HttpMethod.Delete;
                string sub_url = "/netgraph/v1.0/security/groups/" + group_id + "/users/" + user_id + "?userGUID=" + AuthAPI._user_guid + "&subscription-key=" + ApplicationSettings.ApiSubscriptionKey;
                string requestUrl = ApplicationSettings.ApiRootURL + sub_url;
                int httpTimeOutInSec = 60;

                response = InvokeApiEndpoint.GetOrDeleteAPI(verb, AuthAPI._account_token, requestUrl, httpTimeOutInSec);
                if (response == null) return ret_obj;
                string statusCode = response.StatusCode.ToString();
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Security Group Users are not exist" + ex.ToString());
                return ret_obj;
            }

            return ret_obj;
        }

        public static JObject PostAddObjectToSecurityGroup(string object_id, JArray group_ids)
        {
            Graph.Utility.SaveAuditLog("Post AddObjectToSecurityGroup", "+++FUNCTION ENTERED+++", "", "", $"");
            HttpResponseMessage response = null;
            JObject ret_obj;

            string requestUrl = ApplicationSettings.ApiRootURL + "/netgraph/v1.0/security/objects/" + object_id + "groups?userGUID=" + AuthAPI._user_guid + "&subscription-key=" + ApplicationSettings.ApiSubscriptionKey;
            try
            {
                Graph.Utility.SaveAuditLog("AddObjectToSecurityGroup", "POST", "", "", "AddObjectToSecurityGroup");
                HttpMethod verb = HttpMethod.Post;

                int httpTimeOutInSec = 60;
                JObject obj = new JObject();
                obj["enterpriseID"] = AuthAPI._enterprise_guid;

                response = InvokeApiEndpoint.CreateOrUpdateAPI(verb, AuthAPI._account_token, requestUrl, httpTimeOutInSec, obj);
                ret_obj = JObject.Parse(response.Content.ReadAsStringAsync().Result);
                //System.Windows.Forms.MessageBox.Show("Created! Enterprise GUID : " + id);
                Graph.Utility.SaveAuditLog("Post AddObjectToSecurityGroup", "Post", requestUrl, response.StatusCode.ToString(), "AddObjectToSecurityGroup");
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Cannot create the AddObjectToSecurityGroup," + ex.ToString());
                Graph.Utility.SaveAuditLog("Create AddObjectToSecurityGroup", "Post", requestUrl, response.StatusCode.ToString(), "Cannot create AddObjectToSecurityGroup");
                return null;
            }

            return ret_obj;
        }

        public static JObject DeleteSecurityGroupObject(string object_id, string group_id)
        {
            JObject ret_obj = new JObject();
            HttpResponseMessage response = null;
            try
            {
                HttpMethod verb = HttpMethod.Delete;
                string sub_url = "/netgraph/v1.0/security/objects/" + object_id + "/groups/" + group_id + "?userGUID=" + AuthAPI._user_guid + "&subscription-key=" + ApplicationSettings.ApiSubscriptionKey;
                string requestUrl = ApplicationSettings.ApiRootURL + sub_url;
                int httpTimeOutInSec = 60;

                response = InvokeApiEndpoint.GetOrDeleteAPI(verb, AuthAPI._account_token, requestUrl, httpTimeOutInSec);
                if (response == null) return ret_obj;
                string statusCode = response.StatusCode.ToString();
                ret_obj = JObject.Parse(response.Content.ReadAsStringAsync().Result);
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Security Group Users are not exist" + ex.ToString());
                return ret_obj;
            }

            return ret_obj;
        }

        public static JObject PutSecurityGroupObject(string object_id, string group_id)
        {
            JObject ret_obj = new JObject();
            HttpResponseMessage response = null;
            try
            {
                HttpMethod verb = HttpMethod.Put;
                string sub_url = "/netgraph/v1.0/security/objects/" + object_id + "/groups/" + group_id + "?userGUID=" + AuthAPI._user_guid + "&subscription-key=" + ApplicationSettings.ApiSubscriptionKey;
                string requestUrl = ApplicationSettings.ApiRootURL + sub_url;
                int httpTimeOutInSec = 60;
                JObject obj = new JObject();
                obj["create"] = "false";
                obj["read"] = "false";
                obj["update"] = "false";
                obj["delete"] = "true";
                response = InvokeApiEndpoint.CreateOrUpdateAPI(verb, AuthAPI._account_token, requestUrl, httpTimeOutInSec, obj);
                ret_obj = JObject.Parse(response.Content.ReadAsStringAsync().Result);
                System.Windows.Forms.MessageBox.Show("Put Security Group Object!");
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Cannot update Security Group Object, " + ex.ToString());
                return null;
            }

            return ret_obj;
        }

        public static JObject PostAddUserToSecurityGroup(string security_group_user_guid, string object_id)
        {
            Graph.Utility.SaveAuditLog("Post PostAddUserToSecurityGroup", "+++FUNCTION ENTERED+++", "", "", $"");
            HttpResponseMessage response = null;
            JObject ret_obj;

            string requestUrl = ApplicationSettings.ApiRootURL + "/netgraph/v1.0/security/groups/" + object_id + "/users?userGUID=" + AuthAPI._user_guid + "&subscription-key=" + ApplicationSettings.ApiSubscriptionKey;
            try
            {
                Graph.Utility.SaveAuditLog("PostAddUserToSecurityGroup", "POST", "", "", "PostAddUserToSecurityGroup");
                HttpMethod verb = HttpMethod.Post;

                int httpTimeOutInSec = 60;
                JObject obj = new JObject();
                obj["securityGroupUserGUID"] = security_group_user_guid;

                response = InvokeApiEndpoint.CreateOrUpdateAPI(verb, AuthAPI._account_token, requestUrl, httpTimeOutInSec, obj);
                ret_obj = JObject.Parse(response.Content.ReadAsStringAsync().Result);
                //System.Windows.Forms.MessageBox.Show("Created! Enterprise GUID : " + id);
                Graph.Utility.SaveAuditLog("Post PostAddUserToSecurityGroup", "Post", requestUrl, response.StatusCode.ToString(), "PostAddUserToSecurityGroup");
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Cannot create the PostAddUserToSecurityGroup," + ex.ToString());
                Graph.Utility.SaveAuditLog("Create PostAddUserToSecurityGroup", "Post", requestUrl, response.StatusCode.ToString(), "Cannot create PostAddUserToSecurityGroup");
                return null;
            }
            return ret_obj;
        }

        public static JObject GetSecurityGroupObject(string object_id, string group_id)
        {
            Graph.Utility.SaveAuditLog("GetSecurityGroupObject", "+++FUNCTION ENTERED+++", "", "", $"");
            JObject ret_obj = new JObject();
            HttpResponseMessage response = null;
            string requestUrl = ApplicationSettings.ApiRootURL + "/netgraph/v1.0/security/objects/" + object_id + "groups/" + group_id + "?userGUID=" + AuthAPI._user_guid + "&subscription-key=" + ApplicationSettings.ApiSubscriptionKey;
            try
            {
                Graph.Utility.SaveAuditLog("GetSecurityGroupObject", "GET", "", "", "GetSecurityGroupObject");
                HttpMethod verb = HttpMethod.Get;
                int httpTimeOutInSec = 60;

                response = InvokeApiEndpoint.GetOrDeleteAPI(verb, AuthAPI._account_token, requestUrl, httpTimeOutInSec);
                string statusCode = response.StatusCode.ToString();
                ret_obj = JObject.Parse(response.Content.ReadAsStringAsync().Result);
                Graph.Utility.SaveAuditLog("GetSecurityGroupObject", "GET", requestUrl, response.StatusCode.ToString(), "GetSecurityGroupObject");
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("GetSecurityGroupObject is not exist, " + ex.ToString());
                Graph.Utility.SaveAuditLog("GetSecurityGroupObject", "GET", requestUrl, response.StatusCode.ToString(), "Cannot get detail info");
                return null;

            }
            return ret_obj;
        }

        public static JObject PutChangeUserSecurityGroup(string user_guid, string old_security_group_guid, string new_security_group_guid)
        {
            JObject ret_obj = new JObject();
            HttpResponseMessage response = null;
            try
            {
                HttpMethod verb = HttpMethod.Put;
                string sub_url = "/netgraph/v1.0/security/groups/users/" + user_guid + "?subscription-key=" + ApplicationSettings.ApiSubscriptionKey;
                string requestUrl = ApplicationSettings.ApiRootURL + sub_url;
                int httpTimeOutInSec = 60;
                JObject obj = new JObject();
                obj["oldSecurityGroupGUID"] = old_security_group_guid;
                obj["newSecurityGroupGUID"] = new_security_group_guid;
                response = InvokeApiEndpoint.CreateOrUpdateAPI(verb, AuthAPI._account_token, requestUrl, httpTimeOutInSec, obj);
                string statusCode = response.StatusCode.ToString();
                if (statusCode.ToLower() == "ok")
                {
                    ret_obj = JObject.Parse(response.Content.ReadAsStringAsync().Result);
                    System.Windows.Forms.MessageBox.Show("Put PutChangeUserSecurityGroup!");
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Cannot update PutChangeUserSecurityGroup, " + ex.ToString());
                return null;
            }
            return ret_obj;
        }

        public static JArray GetObjectSecurityGroups(string object_id )
        {
            Graph.Utility.SaveAuditLog("GetObjectSecurityGroups", "+++FUNCTION ENTERED+++", "", "", $"");
            JArray ret_obj = new JArray();
            HttpResponseMessage response = null;
            string requestUrl = ApplicationSettings.ApiRootURL + "/netgraph/v1.0/security/objects/" + object_id + "/groups?userGUID=" + AuthAPI._user_guid + "&subscription-key=" + ApplicationSettings.ApiSubscriptionKey;
            try
            {
                Graph.Utility.SaveAuditLog("GetObjectSecurityGroups", "GET", "", "", "GetObjectSecurityGroups");
                HttpMethod verb = HttpMethod.Get;
                int httpTimeOutInSec = 60;

                response = InvokeApiEndpoint.GetOrDeleteAPI(verb, AuthAPI._account_token, requestUrl, httpTimeOutInSec);
                string statusCode = response.StatusCode.ToString();
                ret_obj = JArray.Parse(response.Content.ReadAsStringAsync().Result);
                Graph.Utility.SaveAuditLog("GetObjectSecurityGroups", "GET", requestUrl, response.StatusCode.ToString(), "GetObjectSecurityGroups");
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("GetObjectSecurityGroups is not exist, " + ex.ToString());
                Graph.Utility.SaveAuditLog("GetObjectSecurityGroups", "GET", requestUrl, response.StatusCode.ToString(), "Cannot get detail info");
                return null;

            }
            return ret_obj;
        }
    }
}
