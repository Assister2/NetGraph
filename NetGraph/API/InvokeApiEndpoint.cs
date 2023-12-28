using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Net.Http.Headers;

namespace CyConex
{
    internal class InvokeApiEndpoint
    {
        #region Public method

        //CreateOrUpdateAPI()
        public static HttpResponseMessage CreateOrUpdateAPI(HttpMethod verb, string authorization, string requestUrl, int httpTimeOutInSec, dynamic requestContent = null)
        {
            Graph.Utility.SaveAuditLog("CreateOrUpdateAPI", "+++FUNCTION ENTERED+++", requestUrl, "", $"");
            Graph.Utility.SaveAuditLog("CreateOrUpdateAPI", "VERB", verb.Method.ToUpper(), "", $"");
            Graph.Utility.SaveAuditLog("CreateOrUpdateAPI", "requestUrl", requestUrl, "", $"");
            Graph.Utility.SaveAuditLog("CreateOrUpdateAPI", "requestContent", JsonConvert.SerializeObject(requestContent), "", $"");
            HttpResponseMessage response = null;
            try
            {
                HttpRequestMessage request = new HttpRequestMessage(verb, new Uri(requestUrl));
                if (verb.Method.ToUpper() == "POST" || verb.Method.ToUpper() == "PUT")
                {
                    string tempcontent = JsonConvert.SerializeObject(requestContent);
                    request.Content = new StringContent(JsonConvert.SerializeObject(requestContent), Encoding.UTF8, "application/json");
                    request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                }
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", authorization);
                //httpClient.Timeout
                var ctsForTimeout = new CancellationTokenSource();
                ctsForTimeout.CancelAfter(TimeSpan.FromSeconds(Convert.ToInt32(httpTimeOutInSec)));
                var cancellationTokenForTimeout = ctsForTimeout.Token;
                using (var linkedCts = CancellationTokenSource.CreateLinkedTokenSource(cancellationTokenForTimeout))
                {
                    var httpClientHandler = new HttpClientHandler();
                    var httpClient = new HttpClient(httpClientHandler);
                    httpClient.Timeout = TimeSpan.FromSeconds(Convert.ToInt32(httpTimeOutInSec));

                    Graph.Utility.SaveAuditLog("CreateOrUpdateAPI", "SendAsync...", "", "", $"");
                    response = httpClient.SendAsync(request, linkedCts.Token).GetAwaiter().GetResult();

                    if (verb == HttpMethod.Post)
                    { 
                        if (response.StatusCode != HttpStatusCode.Created)
                        {
                            Graph.Utility.SaveAuditLog("CreateOrUpdateAPI", "ERROR", "POST", response.StatusCode.ToString(), "");
                            return null;
                        }
                        else if (response.StatusCode == HttpStatusCode.Created)
                        {
                            Graph.Utility.SaveAuditLog("CreateOrUpdateAPI", "SUCCESS", response.StatusCode.ToString(), "", "");
                            return response;
                        }
                    }

                    if (verb == HttpMethod.Put)
                    {
                        if (response.StatusCode != HttpStatusCode.OK)
                        {
                            if (response.StatusCode == HttpStatusCode.Created)
                            {
                                Graph.Utility.SaveAuditLog("CreateOrUpdateAPI", "SUCCESS", "PUT", response.StatusCode.ToString(), ""); //New User Created
                                return response;
                            }
                            else
                            {
                                Graph.Utility.SaveAuditLog("CreateOrUpdateAPI", "ERROR", "PUT", response.StatusCode.ToString(), "");
                                return null;
                            }
                        }
                        else if (response.StatusCode == HttpStatusCode.OK)
                        {
                            Graph.Utility.SaveAuditLog("CreateOrUpdateAPI", "SUCCESS", response.StatusCode.ToString(), "", "");
                            return response;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Graph.Utility.SaveAuditLog("CreateOrUpdateAPI", "ERROR", "", ex.Message.ToString(), "");
                return null;
            }
            return null;
        }

        //GetOrDeleteAPI()
        public static HttpResponseMessage GetOrDeleteAPI(HttpMethod verb, string authorization, string requestUrl, int httpTimeOutInSec)
        {
            Graph.Utility.SaveAuditLog("GetOrDeleteAPI", "+++FUNCTION ENTERED+++", requestUrl, "", $"");
            Graph.Utility.SaveAuditLog("GetOrDeleteAPI", "VERB", verb.Method.ToUpper(), "", $"");
            Graph.Utility.SaveAuditLog("GetOrDeleteAPI", "requestUrl", verb.Method.ToUpper(), "", $"");
            HttpResponseMessage response = null;
            try
            {
                HttpRequestMessage request = new HttpRequestMessage(verb, new Uri(requestUrl));
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", authorization);
                //httpClient.Timeout
                var ctsForTimeout = new CancellationTokenSource();
                ctsForTimeout.CancelAfter(TimeSpan.FromSeconds(Convert.ToInt32(httpTimeOutInSec)));
                var cancellationTokenForTimeout = ctsForTimeout.Token;
                using (var linkedCts = CancellationTokenSource.CreateLinkedTokenSource(cancellationTokenForTimeout))
                {
                    var httpClientHandler = new HttpClientHandler();
                    var httpClient = new HttpClient(httpClientHandler);
                    httpClient.Timeout = TimeSpan.FromSeconds(Convert.ToInt32(httpTimeOutInSec));
                    response = httpClient.SendAsync(request, linkedCts.Token).GetAwaiter().GetResult();
                    
                    if ((verb == HttpMethod.Get && response.StatusCode != HttpStatusCode.OK))
                    {
                        //throw new Exception("BackendFailed StatusCode: " + response.StatusCode.ToString());
                        Graph.Utility.SaveAuditLog("GetOrDeleteAPI", "ERROR", "GET", response.StatusCode.ToString(), "");
                    }
                    if ((verb == HttpMethod.Delete && response.StatusCode != HttpStatusCode.NoContent))
                    {
                        //throw new Exception("BackendFailed StatusCode: " + response.StatusCode.ToString());
                        Graph.Utility.SaveAuditLog("GetOrDeleteAPI", "ERROR", "DELETE", response.StatusCode.ToString(), "");
                    }
                    Graph.Utility.SaveAuditLog("CreateOrUpdateAPI", "Responce", response.StatusCode.ToString(), response.Content.ToString(), "");
                    return response;
                }
            }
            catch (Exception ex)
            {
                Graph.Utility.SaveAuditLog("GetOrDeleteAPI", "ERROR", "", ex.Message.ToString(), "");
                return null;
            }
        }

        //GetOAuthTokenAPI()
        public static HttpResponseMessage GetOAuthTokenAPI(HttpMethod verb, string requestUrl, int httpTimeOutInSec, string clientId, string clientSecret, string scope)
        {
            Graph.Utility.SaveAuditLog("GetOAuthTokenAPI", "+++FUNCTION ENTERED+++", verb.ToString(), requestUrl, $"");
            HttpResponseMessage response = null;
            try
            {
                HttpRequestMessage request = new HttpRequestMessage(verb, new Uri(requestUrl));
                var data = new Dictionary<string, string>();
                data.Add("client_id", clientId);
                data.Add("client_secret", clientSecret);
                data.Add("scope", scope);
                data.Add("grant_type", "client_credentials");

                request.Content = new FormUrlEncodedContent(data);

                request.Content.Headers.ContentType = new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded") { CharSet = "UTF-8" };
                //httpClient.Timeout
                var ctsForTimeout = new CancellationTokenSource();
                ctsForTimeout.CancelAfter(TimeSpan.FromSeconds(Convert.ToInt32(httpTimeOutInSec)));
                var cancellationTokenForTimeout = ctsForTimeout.Token;
                
                using (var linkedCts = CancellationTokenSource.CreateLinkedTokenSource(cancellationTokenForTimeout))
                {
                    var httpClientHandler = new HttpClientHandler();
                    var httpClient = new HttpClient(httpClientHandler);
                    httpClient.Timeout = TimeSpan.FromSeconds(Convert.ToInt32(httpTimeOutInSec));

                    Graph.Utility.SaveAuditLog("GetOAuthTokenAPI", "SendAsync", "", "", "Requesting Token");
                    response = httpClient.SendAsync(request, linkedCts.Token).GetAwaiter().GetResult();
                    if (response.StatusCode != HttpStatusCode.OK)
                    {
                        Graph.Utility.SaveAuditLog("GetOAuthTokenAPI", "ERROR", "", "", response.StatusCode.ToString());
                        //System.Windows.Forms.MessageBox.Show("GetOAuthTokenAPI failed! " + response.ToString());
                        return null;
                    }
                    else
                    {
                        Graph.Utility.SaveAuditLog("GetOAuthTokenAPI", "SUCCESS", "", "", response.StatusCode.ToString());
                        return response;
                    }
                    
                }
            }
            catch (Exception ex)
            {
                Graph.Utility.SaveAuditLog("GetOAuthTokenAPI", "ERROR", "", "", ex.ToString());
                return null;
            }
        }
        #endregion
    }
}
