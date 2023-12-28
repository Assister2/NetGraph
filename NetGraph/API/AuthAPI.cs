using CefSharp.DevTools.Network;
using CyConex.AD;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Syncfusion.Data.Extensions;
using Syncfusion.SVG.IO;
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
using CyConex.Graph;
using Microsoft.Identity.Client;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;

namespace CyConex.API
{

    internal class AuthAPI
    {

        public static string _user_guid = "";
        public static string _account_token = "";
        public static string _user_token = "";
        public static string _enterprise_guid = "";
        public static string _enterprise_guid_old = "old";
        public static string _tenant_guid = "";
        public static string _email_address = "";
        public static string _user_name = "";

        public static ArrayList _enterprise_items = new ArrayList();
        public static ArrayList _tenant_items = new ArrayList();

        public static async Task AuthLogin()
        {
            Graph.Utility.SaveAuditLog("AuthLogin", "+++FUNCTION ENTERED+++", "", "", $"");
            var app = Helper.PublicClientApp;
            AuthenticationResult authResult = null;

            //This section calls the ADB2C Web Dialog
            try
            {
                //ResultText.Text = "Started";
                authResult = await app.AcquireTokenInteractive(Helper.ApiScopes)
                    //.WithParentActivityOrWindow(new WindowInteropHelper(this).Handle)
                    .ExecuteAsync();
                UserIdentityRequest userIdentityRequest = new UserIdentityRequest();
                userIdentityRequest.token = authResult.IdToken;
                _user_token = userIdentityRequest.token;
                Graph.Utility.SaveAuditLog("AuthLogin", "", "", "", "Submitted User Token");
            }
            catch (MsalException ex)
            {
                Graph.Utility.SaveAuditLog("AuthLogin", "ERROR", "", "", $"Error Acquiring Token:{Environment.NewLine}{ex}");
                return;
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show($"Error Acquiring Token:{Environment.NewLine}{ex}");
                Graph.Utility.SaveAuditLog("AuthLogin", "ERROR", "", "", $"Error Acquiring Token:{Environment.NewLine}{ex}");
                return;

            }

            if (authResult != null)
            {
                Graph.Utility.SaveAuditLog("AuthLogin", "", "", "", "Got User Token");
                JObject user = ParseIdToken(authResult.IdToken);

                if (user["emails"] is JArray emails)
                {
                    _email_address = emails[0].ToString();
                    Graph.Utility.SaveAuditLog("AuthLogin", "Result", "", "", $"Email Address: {_email_address}");
                }
                _user_name = user["given_name"]?.ToString() + " " + user["family_name"]?.ToString();
                Graph.Utility.SaveAuditLog("AuthLogin", "Result", "", "", $"User Name: {_user_name}");
            }

            HttpResponseMessage response = null;
            try
            {
                Graph.Utility.SaveAuditLog("AuthLogin", "POST", "", "", "Get API Access Token");
                HttpMethod verb = HttpMethod.Post;
                int httpTimeOutInSec = 60;
                response = InvokeApiEndpoint.GetOAuthTokenAPI(verb, ApplicationSettings.ApiTokenURL, httpTimeOutInSec,
                    ApplicationSettings.OAuthTokenClientID, ApplicationSettings.OAuthTokenClientSecret, ApplicationSettings.OAuthTokenScope);
                if (response != null && response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    Graph.Utility.SaveAuditLog("AuthLogin", "SUCCESS", "", "", "Got API Access Token");
                    _account_token = JObject.Parse(response.Content.ReadAsStringAsync().Result)["access_token"].ToString();
                }
                else
                {
                    Graph.Utility.SaveAuditLog("AuthLogin", "ERROR", "", "", response.Content.ReadAsStringAsync().Result);
                    return;
                    
                }
            }
            catch (Exception ex)
            {
                Graph.Utility.SaveAuditLog("AuthLogin", "ERROR", "Authentication failed!", "", ex.ToString());
                System.Windows.Forms.MessageBox.Show("Authentication failed! " + ex.ToString());
                return;
            }


            if (authResult != null)
            {
                string sub_url = "/netgraph/v1.0/users?subscription-key=" + ApplicationSettings.ApiSubscriptionKey;
                string requestUrl = ApplicationSettings.ApiRootURL + sub_url;

                try
                {
                    Graph.Utility.SaveAuditLog("GetOAuthTokenAPI", "PUT", "", "", "Get GetOAuthToken");
                    HttpMethod verb = HttpMethod.Put;
                    int httpTimeOutInSec = 60;
                    JObject request = new JObject();
                    request["userToken"] = _user_token;

                    response = InvokeApiEndpoint.CreateOrUpdateAPI(verb, _account_token, requestUrl, httpTimeOutInSec, request);
                    
                    if (response.StatusCode != HttpStatusCode.OK)
                    {
                        if (response.StatusCode == HttpStatusCode.Created)
                        {
                            Graph.Utility.SaveAuditLog("GetOAuthTokenAPI", "SUCCESS", HttpStatusCode.Created.ToString(), "", "Created");
                        }
                        else
                        {
                            Graph.Utility.SaveAuditLog("PutAuthToken", "PUT", sub_url, response.StatusCode.ToString(), "Error get user GUID");
                            return;
                        }
                    }
                    Graph.Utility.SaveAuditLog("GetOAuthTokenAPI", "SUCCESS", "", "", "Token Created");


                }
                catch (Exception ex)
                {
                        System.Windows.Forms.MessageBox.Show($"Error getting User GUID:{ex.ToString()}");
                        Graph.Utility.SaveAuditLog("PutAuthToken", "PUT", ex.Message.ToString(), "", "Error getting user GUID");
                        return;
                }
                
                try
                {
                    if (_user_guid != JObject.Parse(response.Content.ReadAsStringAsync().Result)["userGUID"].ToString())
                    {
                        _user_guid = JObject.Parse(response.Content.ReadAsStringAsync().Result)["userGUID"].ToString();
                    }
                    else
                    {
                        Graph.Utility.SaveAuditLog("Unable to Set user GUID", "ERROR", "", "", "User GUID is Null");
                    }
                    
                }
                catch (Exception ex)
                {
                    Graph.Utility.SaveAuditLog("Unable to Set user GUID", "ERROR", ex.Message.ToString(), "", "");
                    return;
                }
            }
        }

        public static JObject PutUserLastEnterpriseAndTenant(string lastEnterpriseGUID, string lastTenantGUID)
        {
            Graph.Utility.SaveAuditLog("PutUserLastEnterpriseAndTenant", "+++FUNCTION ENTERED+++", "", "", $"");
            JObject ret_obj = new JObject();
            HttpResponseMessage response = null;
            try
            {
                HttpMethod verb = HttpMethod.Put;
                string sub_url = "/netgraph/v1.0/users/" + AuthAPI._user_guid + "/last-enterprise-tenant?subscription-key=" + ApplicationSettings.ApiSubscriptionKey;
                string requestUrl = ApplicationSettings.ApiRootURL + sub_url;
                int httpTimeOutInSec = 60;
                JObject obj = new JObject();
                obj["lastEnterpriseGUID"] = lastEnterpriseGUID;
                obj["lastTenantGUID"] = lastTenantGUID; 

                response = InvokeApiEndpoint.CreateOrUpdateAPI(verb, AuthAPI._account_token, requestUrl, httpTimeOutInSec, obj);
                ret_obj = JObject.Parse(response.Content.ReadAsStringAsync().Result);
                System.Windows.Forms.MessageBox.Show("Updated Last Enterprise and Tenant!");
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Cannot update Last Enterprise and Tenant, " + ex.ToString());
                return null;
            }

            return ret_obj;
        }

        public static JObject QuickUserSetup()
        {
            Graph.Utility.SaveAuditLog("QuickUserSetup", "+++FUNCTION ENTERED+++", "", "", $"");
            JObject ret_obj = new JObject();
            JArray enterpriseList = new JArray();
            enterpriseList = AuthAPI.GetEnterpriseList();
            if (enterpriseList.Count() == 0)
            {
                Graph.Utility.SaveAuditLog("QuickUserSetup", "INFO", "", "", $"User is not a memebr of an Enterprise");
                Graph.Utility.SaveAuditLog("QuickUserSetup", "INFO", "", "", $"Calling PostQuickUserSetup...");
                
                if (AuthAPI.PostQuickUserSetup() == null)
                {
                    Graph.Utility.SaveAuditLog("QuickUserSetup", "ERROR", "", "", $"Quick User Setup Failed");
                    return null;
                }
            }
           
            Graph.Utility.SaveAuditLog("QuickUserSetup", "INFO", "", "", $"User has an enterprise count of {enterpriseList.Count()}" );

            Graph.Utility.SaveAuditLog("QuickUserSetup", "INFO", "", "", $"Calling GetUserLastEnterpriseAndTenant...");
            ret_obj = AuthAPI.GetUserLastEnterpriseAndTenant();


            //Check if user is now a memebr of the Enterprise and Tenant
            int flag = 1;
            if (ret_obj != null && ret_obj["lastEnterpriseGUID"] != null && ret_obj["lastEnterpriseGUID"].ToString() != "" && ret_obj["lastTenantGUID"] != null && ret_obj["lastTenantGUID"].ToString() != "")
            {
                JObject lastEnterpriseGUIDAccess = CheckUserEnterpriseAccess(ret_obj["lastEnterpriseGUID"].ToString());
                JObject lastTenantGUIDAccess = CheckUserTenantAccess(ret_obj["lastTenantGUID"].ToString());
                if (lastEnterpriseGUIDAccess != null && lastEnterpriseGUIDAccess.ContainsKey("message") && lastEnterpriseGUIDAccess["message"].ToString().ToLower() == "user is authorized")
                {
                    flag = 2;
                }

                if (flag == 2 && lastTenantGUIDAccess != null && lastTenantGUIDAccess.ContainsKey("message") && lastTenantGUIDAccess["message"].ToString().ToLower() == "user is authorized")
                {
                    flag = 3;
                }
                /*if (CheckUserEnterprise(_user_guid, ret_obj["lastEnterpriseGUID"].ToString())){
                    flag = 2;
                }

                if (flag == 2 && CheckUserTenant(_user_guid, ret_obj["lastTenantGUID"].ToString())){
                    flag = 3;
                }*/
            }

            JObject obj = new JObject();
            obj["status"] = flag.ToString();
            obj["user_data"] = ret_obj;
            return obj;
        }

        public static JObject CheckUserEnterpriseAccess(string enterpriseGUID)
        {
            Graph.Utility.SaveAuditLog("CheckUserEnterpriseAccess", "+++FUNCTION ENTERED+++", "", "", "");
            JObject obj = new JObject();
            HttpResponseMessage response = null;
            try
            {
                HttpMethod verb = HttpMethod.Get;
                string sub_url = "/netgraph/v1.0/identity/enterprises/" + enterpriseGUID + "/users/" + AuthAPI._user_guid + "?userGUID=" + AuthAPI._user_guid + "&subscription-key=" + ApplicationSettings.ApiSubscriptionKey + "&hasDeleted=false";
                string requestUrl = ApplicationSettings.ApiRootURL + sub_url;
                int httpTimeOutInSec = 60;

                response = InvokeApiEndpoint.GetOrDeleteAPI(verb, AuthAPI._account_token, requestUrl, httpTimeOutInSec);
                string statusCode = response.StatusCode.ToString();
                Graph.Utility.SaveAuditLog("CheckUserEnterpriseAccess", "GET", requestUrl, response.StatusCode.ToString(), "CheckUserEnterpriseAccess");
                obj = JObject.Parse(response.Content.ReadAsStringAsync().Result);

            }
            catch (Exception ex)
            {
                //System.Windows.Forms.MessageBox.Show("Cannot get edge list : " + ex.ToString());
                Graph.Utility.SaveAuditLog("CheckUserEnterpriseAccess", "ERROR", "", ex.ToString(), "Cannot get edge list");
                return null;
            }

            return obj;
        }

        public static JObject CheckUserTenantAccess(string tenantGUID)
        {
            Graph.Utility.SaveAuditLog("CheckUserTenantAccess", "+++FUNCTION ENTERED+++", "", "", "");
            JObject obj = new JObject();
            HttpResponseMessage response = null;
            try
            {
                HttpMethod verb = HttpMethod.Get;
                string sub_url = "/netgraph/v1.0/identity/tenants/" + tenantGUID + "/users/" + AuthAPI._user_guid + "?userGUID=" + AuthAPI._user_guid + "&subscription-key=" + ApplicationSettings.ApiSubscriptionKey + "&hasDeleted=false";
                string requestUrl = ApplicationSettings.ApiRootURL + sub_url;
                int httpTimeOutInSec = 60;

                response = InvokeApiEndpoint.GetOrDeleteAPI(verb, AuthAPI._account_token, requestUrl, httpTimeOutInSec);
                string statusCode = response.StatusCode.ToString();
                Graph.Utility.SaveAuditLog("CheckUserTenantAccess", "GET", requestUrl, response.StatusCode.ToString(), "CheckUserTenantAccess");
                obj = JObject.Parse(response.Content.ReadAsStringAsync().Result);

            }
            catch (Exception ex)
            {
                //System.Windows.Forms.MessageBox.Show("Cannot get edge list : " + ex.ToString());
                Graph.Utility.SaveAuditLog("CheckUserEnterpriseAccess", "ERROR", "", ex.ToString(), "Cannot get edge list");
                return null;
            }

            return obj;
        }

        public static bool CheckUserEnterprise(string userGUID, string enterpriseGUID)
        {
            Graph.Utility.SaveAuditLog("CheckUserEnterprise", "", "+++FUNCTION ENTERED+++", "", $"");
            Graph.Utility.SaveAuditLog("CheckUserEnterprise", "", "userGUID:", userGUID, $"");
            Graph.Utility.SaveAuditLog("CheckUserEnterprise", "", "enterpriseGUID:", enterpriseGUID, $"");
            Graph.Utility.SaveAuditLog("CheckUserEnterprise", "", "", "", $"Getting Enterprise User List...");
            JArray userList = AuthAPI.GetEnterpriseUserList();
            
            bool flag = false;
            if (userList != null && userList.Count > 0)
            {
                for (int i = 0; i < userList.Count; i++)
                {
                    JObject userItem = userList[i] as JObject;
                    if (userItem["enterpriseGUID"].ToString() == enterpriseGUID)
                    {
                        Graph.Utility.SaveAuditLog("CheckUserEnterprise", "", "", "", $"User is a member of the Enteprise");
                        flag = true;
                        return flag;
                    }
                }
            }
            if (flag == false)
            {
                Graph.Utility.SaveAuditLog("CheckUserEnterprise", "", "", "", $"User is NOT a member of the Enteprise");
            }
            
            return flag;
        }

        public static bool CheckUserTenant(string userGUID, string tenantGUID)
        {
            Graph.Utility.SaveAuditLog("CheckUserTenant", "", "+++FUNCTION ENTERED+++",  "", $"");
            Graph.Utility.SaveAuditLog("CheckUserTenant", "", "userGUID:", userGUID, $"");
            Graph.Utility.SaveAuditLog("CheckUserTenant", "", "tenantGUID:", tenantGUID, $"");
            Graph.Utility.SaveAuditLog("CheckUserTenant", "", "", "", $"Getting Tenant User List...");
            JArray userList = AuthAPI.GetTenantUserList(tenantGUID);
            
            bool flag = false;
            if (userList != null && userList.Count > 0)
            {
                for (int i = 0; i < userList.Count; i++)
                {
                    JObject userItem = userList[i] as JObject;
                    if (userItem["tenantUserGUID"].ToString() == tenantGUID)
                    {
                        Graph.Utility.SaveAuditLog("CheckUserTenant", "", "", "", $"User is a member of the Tenant");
                        flag = true;
                        break;
                    }
                }
            }

            if (flag == false)
            {
                Graph.Utility.SaveAuditLog("CheckUserTenant", "", "", "", $"User is NOT a member of the Tenant");
            }
            return flag;
        }

        public static JObject GetUserLastEnterpriseAndTenant()
        {
            Graph.Utility.SaveAuditLog("GetUserLastEnterpriseAndTenant", "+++FUNCTION ENTERED+++", "", "", $"");
            JObject arr = new JObject();
            HttpResponseMessage response = null;
            string sub_url = "/netgraph/v1.0/users/" + AuthAPI._user_guid + "/last-enterprise-tenant?subscription-key=" + ApplicationSettings.ApiSubscriptionKey;
            string requestUrl = ApplicationSettings.ApiRootURL + sub_url;
            try
            {
                Graph.Utility.SaveAuditLog("GetUserLastEnterpriseAndTenant", "GET", "", "", "Get UserLastEnterpriseAndTenant");
                HttpMethod verb = HttpMethod.Get;
                int httpTimeOutInSec = 60;

                response = InvokeApiEndpoint.GetOrDeleteAPI(verb, AuthAPI._account_token, requestUrl, httpTimeOutInSec);
                string statusCode = response.StatusCode.ToString();
                if (statusCode.ToLower() == "ok")
                {
                    arr = JObject.Parse(response.Content.ReadAsStringAsync().Result);
                    Graph.Utility.SaveAuditLog("GetUserLastEnterpriseAndTenant", "SUCSSESS", "", "", response.Content.ReadAsStringAsync().Result);
                }
            }
            catch (Exception ex)
            {
                Graph.Utility.SaveAuditLog("GetUserLastEnterpriseAndTenant", "ERROR", requestUrl, ex.Message.ToString(), "Cannot get Enterprise and Tenant");
                return arr;
            }

            return arr;
        }



        public static JArray GetTenantUserList(string tenantGUID)
        {
            Graph.Utility.SaveAuditLog("GetTenantUserList", "+++FUNCTION ENTERED+++", "", "", $"");
            if (tenantGUID == "") 
                tenantGUID = tenantGUID == "" ? AuthAPI._tenant_guid : tenantGUID;

            if (tenantGUID == "")
            {
                Graph.Utility.SaveAuditLog("GetTenantUserList", "ERROR", "", "", "No tenantGUID Available");
                return null;
            }
                
            JArray arr = new JArray();
            HttpResponseMessage response = null;
            string sub_url = "/netgraph/v1.0/tenants/" + tenantGUID + "/users?userGUID=" + AuthAPI._user_guid + "&subscription-key=" + ApplicationSettings.ApiSubscriptionKey;
            string requestUrl = ApplicationSettings.ApiRootURL + sub_url;
            try
            {
                Graph.Utility.SaveAuditLog("GetTenantUserList", "GET", "", "", "Get TenantUserList");
                HttpMethod verb = HttpMethod.Get;
                int httpTimeOutInSec = 60;

                response = InvokeApiEndpoint.GetOrDeleteAPI(verb, AuthAPI._account_token, requestUrl, httpTimeOutInSec);
                string statusCode = response.StatusCode.ToString();
                if (statusCode.ToString().ToLower() == "ok")
                {
                    arr = JArray.Parse(response.Content.ReadAsStringAsync().Result);
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("TenantUserList does not exist" + ex.ToString());
                Graph.Utility.SaveAuditLog("TenantUserList", "GET", requestUrl, response.StatusCode.ToString(), "Cannot get TenantUserList");
                return arr;
            }

            return arr;
        }

        public static JArray GetEnterpriseUserList(string enterpriseGUID  = "")
        {
            enterpriseGUID = enterpriseGUID == "" ? AuthAPI._enterprise_guid : enterpriseGUID;
            Graph.Utility.SaveAuditLog("GetEnterpriseUserList", "+++FUNCTION ENTERED+++", "", "", $"");
            JArray arr = new JArray();
            HttpResponseMessage response = null;
            string sub_url = "/netgraph/v1.0/enterprises?userGUID=" + AuthAPI._user_guid + "&subscription-key=" + ApplicationSettings.ApiSubscriptionKey;
            string requestUrl = ApplicationSettings.ApiRootURL + sub_url;
            try
            {
                Graph.Utility.SaveAuditLog("GetEnterpriseUserList", "GET", "", "", "Get EnterpriseUserList");
                HttpMethod verb = HttpMethod.Get;
                int httpTimeOutInSec = 60;

                response = InvokeApiEndpoint.GetOrDeleteAPI(verb, AuthAPI._account_token, requestUrl, httpTimeOutInSec);
                if (response == null)
                {
                    Graph.Utility.SaveAuditLog("GetEnterpriseUserList", "ERROR", "", "", $"Null responce");
                    return null;
                }
                else
                {
                    if (response.StatusCode == HttpStatusCode.OK) 
                    {
                        Graph.Utility.SaveAuditLog("GetEnterpriseUserList", "SUCCESS", "", "", $"");
                        return JArray.Parse(response.Content.ReadAsStringAsync().Result);
                    }
                    else
                    {
                        Graph.Utility.SaveAuditLog("GetEnterpriseUserList", "ERROR", response.StatusCode.ToString(), "", $"Null responce");
                        return null;
                    }
                }
                
            }
            catch (Exception ex)
            {

                Graph.Utility.SaveAuditLog("EnterpriseUserList", "ERROR", ex.Message.ToString(), response.StatusCode.ToString(), "Cannot get EnterpriseUserList");
                return arr;
            }

        }


        public static JArray GetEnterpriseList()
        {
            Graph.Utility.SaveAuditLog("GetEnterpriseList", "+++FUNCTION ENTERED+++", "", "", $"");
            JArray arr = new JArray();
            HttpResponseMessage response = null;
            string sub_url = "/netgraph/v1.0/enterprises?userGUID=" + AuthAPI._user_guid + "&subscription-key=" + ApplicationSettings.ApiSubscriptionKey;
            string requestUrl = ApplicationSettings.ApiRootURL + sub_url;
            try
            {
                Graph.Utility.SaveAuditLog("GetEnterpriseList", "GET", "", "", "Get EnterpriseList");
                HttpMethod verb = HttpMethod.Get;
                int httpTimeOutInSec = 60;

                response = InvokeApiEndpoint.GetOrDeleteAPI(verb, AuthAPI._account_token, requestUrl, httpTimeOutInSec);
                string statusCode = response.StatusCode.ToString();
                arr = JArray.Parse(response.Content.ReadAsStringAsync().Result);
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("EnterpriseList does not exist" + ex.ToString());
                Graph.Utility.SaveAuditLog("Get EnterpriseList", "GET", requestUrl, response.StatusCode.ToString(), "Cannot get Enterprise");
                return arr;
            }

            return arr;
        }

        public static void AuthLogout()
        {
            _user_token = "";
        }
        public static JObject ParseIdToken(string idToken)
        {
            // Parse the idToken to get user info
            idToken = idToken.Split('.')[1];
            idToken = Base64UrlDecode(idToken);
            return JObject.Parse(idToken);
        }

        public static string Base64UrlDecode(string s)
        {
            s = s.Replace('-', '+').Replace('_', '/');
            s = s.PadRight(s.Length + (4 - s.Length % 4) % 4, '=');
            var byteArray = Convert.FromBase64String(s);
            var decoded = Encoding.UTF8.GetString(byteArray, 0, byteArray.Count());
            return decoded;
        }

        public static string CreateEnterprise(string name, string address1, string address2, string postcode, string city, string country, string state )
        {
            Graph.Utility.SaveAuditLog("CreateEnterprise", "+++FUNCTION ENTERED+++", "", "", $"");
            HttpResponseMessage response = null;
            string id = "";

            string requestUrl = ApplicationSettings.ApiRootURL + "/netgraph/v1.0/enterprises?userGUID=" + AuthAPI._user_guid + "&subscription-key=" + ApplicationSettings.ApiSubscriptionKey;
            try
            {
                Graph.Utility.SaveAuditLog("CreateEnterprise", "POST", "", "", "Create Enterprise");
                HttpMethod verb = HttpMethod.Post;

                int httpTimeOutInSec = 60;
                JObject obj = new JObject();
                obj["enterpriseName"] = name; 
                obj["addressLine1"] = address1;
                obj["addressLine2"] = address2;
                obj["postcode"] = postcode;
                obj["city"] = city;
                obj["state"] = state;
                obj["country"] = country;

                response = InvokeApiEndpoint.CreateOrUpdateAPI(verb, AuthAPI._account_token, requestUrl, httpTimeOutInSec, obj);
                if (response != null)
                {
                    id = response.Content.ReadAsStringAsync().Result;
                    //System.Windows.Forms.MessageBox.Show("Created! Enterprise GUID : " + id);
                    Graph.Utility.SaveAuditLog("Create Enterprise", "Post", requestUrl, response.StatusCode.ToString(), "Created Enterprise");
                }
                
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Cannot create Enterprise, " + ex.ToString());
                Graph.Utility.SaveAuditLog("Create Enterprise", "Post", requestUrl, response.StatusCode.ToString(), "Cannot create Enterprise");
                return null;
            }

            return id;
        }

        public static JObject PostQuickUserSetup()
        {
            Graph.Utility.SaveAuditLog("Post QuickUserSetup", "+++FUNCTION ENTERED+++", "", "", $"");
            HttpResponseMessage response = null;

            string requestUrl = ApplicationSettings.ApiRootURL + "/netgraph/v1.0/users/setup?&subscription-key=" + ApplicationSettings.ApiSubscriptionKey;
            try
            {
                Graph.Utility.SaveAuditLog("QuickUserSetup", "POST", "", "", "QuickUserSetup");
                HttpMethod verb = HttpMethod.Post;

                int httpTimeOutInSec = 60;
                JObject obj = new JObject();
                obj["userToken"] = AuthAPI._user_token;

                response = InvokeApiEndpoint.CreateOrUpdateAPI(verb, AuthAPI._account_token, requestUrl, httpTimeOutInSec, obj);

                if (response == null)
                {
                    Graph.Utility.SaveAuditLog("Post QuickUserSetup", "ERROR", "", "", "Responce is Null");
                    return null;
                }
                else
                {
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        Graph.Utility.SaveAuditLog("Post QuickUserSetup", "SUCCESS", "", "", "");
                        return JObject.Parse(response.Content.ReadAsStringAsync().Result);
                    }
                    else
                    {
                        Graph.Utility.SaveAuditLog("Post QuickUserSetup", "ERROR", response.StatusCode.ToString(), "", "");
                        return null;
                    }
                }
               
            }
            catch (Exception ex)
            {
                Graph.Utility.SaveAuditLog("Post QuickUserSetup", "Post", requestUrl, response.StatusCode.ToString(), "Quick user setup failed"); ;
                return null;
            }

        }

        public static JArray GetEnterprises()
        {
            Graph.Utility.SaveAuditLog("GetEnterprises", "+++FUNCTION ENTERED+++", "", "", $"");
            JArray arr = new JArray();
            HttpResponseMessage response = null;
            string sub_url = "/netgraph/v1.0/enterprises?userGUID=" + _user_guid + "&subscription-key=" + ApplicationSettings.ApiSubscriptionKey;
            string requestUrl = ApplicationSettings.ApiRootURL + sub_url;
            try
            {
                Graph.Utility.SaveAuditLog("GetEnterprises", "GET", "", "", "Get Enterprise");
                HttpMethod verb = HttpMethod.Get;
                int httpTimeOutInSec = 60;

                response = InvokeApiEndpoint.GetOrDeleteAPI(verb, _account_token, requestUrl, httpTimeOutInSec);
                string statusCode = response.StatusCode.ToString();
                arr = JArray.Parse(response.Content.ReadAsStringAsync().Result);
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Enterprise does not exist" + ex.ToString());
                Graph.Utility.SaveAuditLog("Get Enterprise", "GET", requestUrl, response.StatusCode.ToString(), "Cannot get Enterprise");
                return arr;
            }

            return arr;
        }


        public static bool DeleteEnterprise(string enterprise_id)
        {
            Graph.Utility.SaveAuditLog("DeleteEnterprise", "+++FUNCTION ENTERED+++", "", "", $"");
            bool flag = false;
            HttpResponseMessage response = null;
            string sub_url = "/netgraph/v1.0/enterprises/" + enterprise_id + "?userGUID=" + _user_guid + "&subscription-key=" + ApplicationSettings.ApiSubscriptionKey;
            string requestUrl = ApplicationSettings.ApiRootURL + sub_url;
            try
            {
                Graph.Utility.SaveAuditLog("DeleteEnterprise", "DELETE", "", "", "Delete Enterprise");
                HttpMethod verb = HttpMethod.Delete;
                int httpTimeOutInSec = 60;

                response = InvokeApiEndpoint.GetOrDeleteAPI(verb, _account_token, requestUrl, httpTimeOutInSec);
                flag = true;
                Graph.Utility.SaveAuditLog("Delete Enterprise", "DELETE", requestUrl, response.StatusCode.ToString(), "Deleted Enterprise");

            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Cannot delete Enterprise, " + ex.ToString());
                Graph.Utility.SaveAuditLog("Delete Enterprise", "DELETE", requestUrl, response.StatusCode.ToString(), "Cannot delete Enterprise");
                return flag;
            }

            return flag;
        }

        public static bool DeleteTenant(string tenant_id)
        {
            Graph.Utility.SaveAuditLog("DeleteTenant", "+++FUNCTION ENTERED+++", "", "", $"");
            bool flag = false;
            HttpResponseMessage response = null;
            string sub_url = "/netgraph/v1.0/tenants/" + tenant_id + "?userGUID=" + _user_guid + "&subscription-key=" + ApplicationSettings.ApiSubscriptionKey;
            string requestUrl = ApplicationSettings.ApiRootURL + sub_url;

            try
            {
                Graph.Utility.SaveAuditLog("DeleteTenant", "DELETE", "", "", "Delete Tenant");
                HttpMethod verb = HttpMethod.Delete;
                int httpTimeOutInSec = 60;
                response = InvokeApiEndpoint.GetOrDeleteAPI(verb, _account_token, requestUrl, httpTimeOutInSec);
                flag = true;
                Graph.Utility.SaveAuditLog("Delete Tenant", "DELETE", requestUrl, response.StatusCode.ToString(), "Deleted Tenant");
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Cannot delete Tenant, " + ex.ToString());
                Graph.Utility.SaveAuditLog("Delete Tenant", "DELETE", requestUrl, response.StatusCode.ToString(), "Cannot deleted Tenant");
                return flag;
            }

            return flag;
        }

        public static string GetEnterpriseDetail(string enterprise_id)
        {
            if (enterprise_id == null || enterprise_id == "") return "";
            Graph.Utility.SaveAuditLog("GetEnterpriseDetail", "+++FUNCTION ENTERED+++", "", "", $"");
            string result = "";
            HttpResponseMessage response = null;
            string sub_url = "/netgraph/v1.0/enterprises/" + enterprise_id + "?userGUID=" + _user_guid + "&subscription-key=" + ApplicationSettings.ApiSubscriptionKey;
            string requestUrl = ApplicationSettings.ApiRootURL + sub_url;

            try
            {
                Graph.Utility.SaveAuditLog("GetEnterpriseDetail", "GET", "", "", "Get Enterprise Detail");
                HttpMethod verb = HttpMethod.Get;
                int httpTimeOutInSec = 60;

                response = InvokeApiEndpoint.GetOrDeleteAPI(verb, _account_token, requestUrl, httpTimeOutInSec);
                if (response == null) { return null; }
                string statusCode = response.StatusCode.ToString();
                if (statusCode.ToLower() == "ok")
                {
                    result = response.Content.ReadAsStringAsync().Result;
                    Graph.Utility.SaveAuditLog("GetEnterpriseDetail", "GET", requestUrl, response.StatusCode.ToString(), "Get Enterprise Deail Info");
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Not exists, " + ex.ToString());
                Graph.Utility.SaveAuditLog("GetEnterpriseDetail", "GET", requestUrl, response.StatusCode.ToString(), "Cannot get Enterprise Deail Info");
                return result;
            }
            return result;
        }

        public static string CreateTenant(string tenantName, string tenantDescription)
        {
            Graph.Utility.SaveAuditLog("CreateTenant", "+++FUNCTION ENTERED+++", "", "", $"");
            HttpResponseMessage response = null;
            string id = "";
            string requestUrl = ApplicationSettings.ApiRootURL + "/netgraph/v1.0/tenants?enterpriseGUID=" + AuthAPI._enterprise_guid + "&userGUID=" + AuthAPI._user_guid + "&subscription-key=" + ApplicationSettings.ApiSubscriptionKey;

            try
            {
                Graph.Utility.SaveAuditLog("CreateTenant", "POST", "", "", "Creat Tenant");
                HttpMethod verb = HttpMethod.Post;
                int httpTimeOutInSec = 60;
                TenantItem request = new TenantItem();
                request.TenantName = tenantName;
                request.TenantDescription = tenantDescription;

                response = InvokeApiEndpoint.CreateOrUpdateAPI(verb, AuthAPI._account_token, requestUrl, httpTimeOutInSec, request);
                if (response == null) { return null; }
                string statusCode = response.StatusCode.ToString();
                if (statusCode.ToLower() == "ok" || statusCode.ToLower() == "created")
                {
                    id = response.Content.ReadAsStringAsync().Result;
                    //System.Windows.Forms.MessageBox.Show("Created! Tenant GUID : " + id);
                    Graph.Utility.SaveAuditLog("Create Tenant", "POST", requestUrl, response.StatusCode.ToString(), "Created Tenant");
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Error, " + ex.ToString());
                Graph.Utility.SaveAuditLog("Create Tenant", "POST", requestUrl, response.StatusCode.ToString(), "Cannot created Tenant");
                return null;
            }
            return id;
        }

        public static string GetTenantDetail(string tenant_id)
        {
            if (tenant_id == null || tenant_id == "") return "";
            Graph.Utility.SaveAuditLog("GetTenantDetail", "+++FUNCTION ENTERED+++", "", "", $"");
            string result = "";
            HttpResponseMessage response = null;

            string sub_url = "/netgraph/v1.0/tenants/" + tenant_id + "?userGUID=" + _user_guid + "&subscription-key=" + ApplicationSettings.ApiSubscriptionKey;
            string requestUrl = ApplicationSettings.ApiRootURL + sub_url;
            try
            {
                Graph.Utility.SaveAuditLog("GetTenantDetail", "GET", "", "", "Get Tenant Detail Info");
                HttpMethod verb = HttpMethod.Get;
                int httpTimeOutInSec = 60;

                response = InvokeApiEndpoint.GetOrDeleteAPI(verb, _account_token, requestUrl, httpTimeOutInSec);
                string statusCode = response.StatusCode.ToString();
                result = response.Content.ReadAsStringAsync().Result;
                Graph.Utility.SaveAuditLog("GetTenantDetail", "GET", requestUrl, response.StatusCode.ToString(), "Get Tenant Detail Info");
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Cannot get Tenant Detailed Info, " + ex.ToString());
                Graph.Utility.SaveAuditLog("GetTenantDetail", "GET", requestUrl, response.StatusCode.ToString(), "Cannot get Tenant Detail Info");
                return result;
            }
            return result;
        }

        public static JArray GetTenants(string enterprise_id)
        {
            Graph.Utility.SaveAuditLog("GetTenants", "+++FUNCTION ENTERED+++", "", "", $"");
            JArray tenants = new JArray();
            HttpResponseMessage response = null;
            JArray tenants_ids = new JArray();
            string requestUrl = ApplicationSettings.ApiRootURL + "/netgraph/v1.0/tenants?enterpriseGUID=" + enterprise_id + "&userGUID=" + AuthAPI._user_guid + "&subscription-key=" + ApplicationSettings.ApiSubscriptionKey;
            try
            {
                Graph.Utility.SaveAuditLog("GetTenants", "GET", "", "", "Get Tenants");
                HttpMethod verb = HttpMethod.Get;
                int httpTimeOutInSec = 60;

                response = InvokeApiEndpoint.GetOrDeleteAPI(verb, AuthAPI._account_token, requestUrl, httpTimeOutInSec);
                string statusCode = response.StatusCode.ToString();
                tenants_ids = JArray.Parse(response.Content.ReadAsStringAsync().Result);
                Graph.Utility.SaveAuditLog("GetTenants", "GET", requestUrl, response.StatusCode.ToString(), "Get Tenants");
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Tenants is not exist, " + ex.ToString());
                Graph.Utility.SaveAuditLog("GetTenants", "GET", requestUrl, response.StatusCode.ToString(), "Cannot get Tenants");
                return null;

            }
            return tenants_ids;
        }

        public static JObject GetUser(string user_guid = "")
        {
            Graph.Utility.SaveAuditLog("Get User", "+++FUNCTION ENTERED+++", "", "", $"");
            user_guid = user_guid == "" ? AuthAPI._user_guid : user_guid;
            JObject user_obj = new JObject();
            HttpResponseMessage response = null; 
            string requestUrl = ApplicationSettings.ApiRootURL + "/netgraph/v1.0/users/" + user_guid + "?subscription-key=" + ApplicationSettings.ApiSubscriptionKey;
            try
            {
                Graph.Utility.SaveAuditLog("Get User", "GET", "", "", "Get User");
                HttpMethod verb = HttpMethod.Get;
                int httpTimeOutInSec = 60;

                response = InvokeApiEndpoint.GetOrDeleteAPI(verb, AuthAPI._account_token, requestUrl, httpTimeOutInSec);
                string statusCode = response.StatusCode.ToString();
                user_obj = JObject.Parse(response.Content.ReadAsStringAsync().Result);
                Graph.Utility.SaveAuditLog("Get User", "GET", requestUrl, response.StatusCode.ToString(), "Get User");
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("User is not exist, " + ex.ToString());
                Graph.Utility.SaveAuditLog("Get User", "GET", requestUrl, response.StatusCode.ToString(), "Cannot get user");
                return null;

            }
            return user_obj;
        }
    }
}
