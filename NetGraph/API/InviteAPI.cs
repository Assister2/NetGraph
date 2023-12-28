using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CyConex.API
{
    public class InviteAPI
    {
        public static JObject PostNewInvite(string creatorGUID, string enterpriseID, string tenantGUID, string emailAddress)
        {
            Graph.Utility.SaveAuditLog("PostNewInvite", "+++FUNCTION ENTERED+++", "", "", $"");
            HttpResponseMessage response = null;
            JObject ret_obj = new JObject();
            try
            {

                HttpMethod verb = HttpMethod.Post;
                string sub_url = "/netgraph/v1.0/inviteusers?userGUID=" + AuthAPI._user_guid + "&subscription-key=" + ApplicationSettings.ApiSubscriptionKey;
                string requestUrl = ApplicationSettings.ApiRootURL + sub_url;
                int httpTimeOutInSec = 60;
                JObject data = new JObject();
                data["creatorGUID"] = creatorGUID;
                data["enterpriseID"] = enterpriseID;
                data["tenantGUID"] = tenantGUID;
                data["emailAddress"] = emailAddress;
                response = InvokeApiEndpoint.CreateOrUpdateAPI(verb, AuthAPI._account_token, requestUrl, httpTimeOutInSec, data);
                ret_obj = JObject.Parse(response.Content.ReadAsStringAsync().Result);
                System.Windows.Forms.MessageBox.Show("Invited! ");
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Cannot invite, " + ex.ToString());
                return null;
            }

            return ret_obj;
        }

        public static JObject PutAcceptInvite(string invite_guid, string creatorGUID, string enterpriseID, string tenantGUID, string emailAddress)
        {
            Graph.Utility.SaveAuditLog("PutRepoNodeFramework", "+++FUNCTION ENTERED+++", "", "", $"");
            JObject ret_obj = new JObject();
            HttpResponseMessage response = null;
            try
            {
                HttpMethod verb = HttpMethod.Put;
                string sub_url = "/netgraph/v1.0/inviteusers/" + invite_guid + "?userGUID=" + AuthAPI._user_guid + "&subscription-key=" + ApplicationSettings.ApiSubscriptionKey;
                string requestUrl = ApplicationSettings.ApiRootURL + sub_url;
                int httpTimeOutInSec = 60;
                JObject data = new JObject();
                data["creatorGUID"] = creatorGUID;
                data["enterpriseID"] = enterpriseID;
                data["tenantGUID"] = tenantGUID;
                data["emailAddress"] = emailAddress;

                response = InvokeApiEndpoint.CreateOrUpdateAPI(verb, AuthAPI._account_token, requestUrl, httpTimeOutInSec, data);
                ret_obj = JObject.Parse(response.Content.ReadAsStringAsync().Result);
                System.Windows.Forms.MessageBox.Show("Invite Accept!");
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Cannot update Node Framework, " + ex.ToString());
                return null;
            }

            return ret_obj;
        }

        public static JArray GetInviteDetail(string invite_user_guid)
        {
            Graph.Utility.SaveAuditLog("GetInviteDetail", "+++FUNCTION ENTERED+++", "", "", $"");
            JArray arr = new JArray();
            HttpResponseMessage response = null;
            try
            {
                Graph.Utility.SaveAuditLog("GetInviteDetail", "Get", "", "", $"");
                HttpMethod verb = HttpMethod.Get;
                string sub_url = "/netgraph/v1.0/inviteusers/" + invite_user_guid + "?userGUID=" + AuthAPI._user_guid + "&subscription-key=" + ApplicationSettings.ApiSubscriptionKey;

                string requestUrl = ApplicationSettings.ApiRootURL + sub_url;
                int httpTimeOutInSec = 60;

                response = InvokeApiEndpoint.GetOrDeleteAPI(verb, AuthAPI._account_token, requestUrl, httpTimeOutInSec);
                if (response == null) return arr;
                string statusCode = response.StatusCode.ToString();
                if (statusCode.ToLower() == "notfound")
                {
                    return arr;
                }
                arr = JArray.Parse(response.Content.ReadAsStringAsync().Result);
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("InviteDetailInfo is not exist" + ex.ToString());
                return arr;
            }

            return arr;
        }

        public static JArray GetOriginatorPendingInvites(string invite_user_guid)
        {
            Graph.Utility.SaveAuditLog("GetOriginatorPendingInvites", "+++FUNCTION ENTERED+++", "", "", $"");
            JArray arr = new JArray();
            HttpResponseMessage response = null;
            try
            {
                Graph.Utility.SaveAuditLog("GetOriginatorPendingInvites", "Get", "", "", $"");
                HttpMethod verb = HttpMethod.Get;
                string sub_url = "/netgraph/v1.0/inviteusers/originator-pending/list?inviteCreatorUserGUID" + invite_user_guid + "?userGUID=" + AuthAPI._user_guid + "&subscription-key=" + ApplicationSettings.ApiSubscriptionKey;

                string requestUrl = ApplicationSettings.ApiRootURL + sub_url;
                int httpTimeOutInSec = 60;

                response = InvokeApiEndpoint.GetOrDeleteAPI(verb, AuthAPI._account_token, requestUrl, httpTimeOutInSec);
                if (response == null) return arr;
                string statusCode = response.StatusCode.ToString();
                if (statusCode.ToLower() == "notfound")
                {
                    return arr;
                }
                arr = JArray.Parse(response.Content.ReadAsStringAsync().Result);
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("InviteDetailInfo is not exist" + ex.ToString());
                return arr;
            }

            return arr;
        }

        public static JArray GetRecipientPendingInvites(string email)
        {
            Graph.Utility.SaveAuditLog("GetRecipientPendingInvites", "+++FUNCTION ENTERED+++", "", "", $"");
            JArray arr = new JArray();
            HttpResponseMessage response = null;
            try
            {
                Graph.Utility.SaveAuditLog("GetRecipientPendingInvites", "Get", "", "", $"");
                HttpMethod verb = HttpMethod.Get;
                string sub_url = "/netgraph/v1.0/inviteusers/recipient-pending/list?email=" + email + "?userGUID=" + AuthAPI._user_guid + "&subscription-key=" + ApplicationSettings.ApiSubscriptionKey;

                string requestUrl = ApplicationSettings.ApiRootURL + sub_url;
                int httpTimeOutInSec = 60;

                response = InvokeApiEndpoint.GetOrDeleteAPI(verb, AuthAPI._account_token, requestUrl, httpTimeOutInSec);
                if (response == null) return arr;
                string statusCode = response.StatusCode.ToString();
                if (statusCode.ToLower() == "notfound")
                {
                    return arr;
                }
                arr = JArray.Parse(response.Content.ReadAsStringAsync().Result);
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("InviteDetailInfo is not exist" + ex.ToString());
                return arr;
            }

            return arr;
        }
    }
}
