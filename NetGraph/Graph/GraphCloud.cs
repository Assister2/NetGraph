using CefSharp;
using CefSharp.WinForms;
using CyConex.Chromium;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CyConex.API;

namespace CyConex.Graph
{
    internal class GraphCloud
    {
        public bool _is_new = true;
        public JObject _graph_meta = new JObject();
        public string _graph_title = "";
        public async void SaveGraph(ChromiumWebBrowser _browser, bool hasUnsavedChanges, bool flag = false)
        {
            Graph.Utility.SaveAuditLog("SaveGraph", "+++FUNCTION ENTERED+++", "", "", $"");
            _is_new = flag ? true : _is_new;
            if (AuthAPI._tenant_guid == "")
            {
                Graph.Utility.SaveAuditLog("SaveGraph", "", "", "", "No Tenant Selected");
                System.Windows.Forms.MessageBox.Show("Please select the Tenant");
                return;
            }

            if (hasUnsavedChanges)
            {
                _browser.ExecScriptAsync("increaseRevision();");
            }

            JavascriptResponse response = await _browser.EvaluateScriptAsync($"getCyFullData()");
            if (!response.Success)
            {
                throw new Exception("Unable to get graph data");
            }
            var jsonRes = response.Result;
            var data = ((IDictionary<String, Object>)jsonRes);
            string data1 = JsonConvert.SerializeObject(data["data1"], Formatting.Indented);
            string data2 = JsonConvert.SerializeObject(data["data2"], Formatting.Indented);
            string finaldata = data1 + (char)13 + "[]" + (char)13 + data2;

            if (_is_new)
            {
                Graph.Utility.SaveAuditLog("SaveGraph", "PostGraphMeta", "", "", $"Create new graph");
                _graph_meta = GraphAPI.PostGraphMeta();

                if (_graph_meta == null)
                {
                    Graph.Utility.SaveAuditLog("SaveGraph", "ERROR", "", "", $"Unable to create graph");
                    System.Windows.Forms.MessageBox.Show("Cannot create graph meta");
                    return;
                }
            }

            // put graph detail
            Graph.Utility.SaveAuditLog("SaveGraph", "GraphDetailData", "", "", $"Updating Graph Detail...");
            JObject detailObj = GraphUtil.GraphDetailData(data2);

            Graph.Utility.SaveAuditLog("SaveGraph", "PutGraphDetail", "", "", $"Saving Graph Detail...");
            JObject graphDetailObj = GraphAPI.PutGraphDetail(_graph_meta["graphGUID"].ToString(), detailObj);
            if (graphDetailObj == null)
            {
                Graph.Utility.SaveAuditLog("SaveGraph", "ERROR", "", "", $"Attempting to delete Graph...");
                GraphAPI.DeleteGraphMeta(_graph_meta["graphGUID"].ToString());
                return;
            }

            var png64 = await _browser.EvaluateScriptAsync($"cy.jpg();");
            string img_str = png64.Result as string;
            img_str = img_str.Substring(23);

            // post graph file
            JObject fileObj = GraphUtil.GraphFileData(data1);
            fileObj["image"] = img_str;
            _graph_title = fileObj["title"].ToString();

            Graph.Utility.SaveAuditLog("SaveGraph", "PostGraphFile", "", "", $"Saving Graph File...");
            JObject graphFileObj = GraphAPI.PostGraphFile(fileObj );
            if (graphFileObj == null)
            {
                Graph.Utility.SaveAuditLog("SaveGraph", "ERROR", "", "", $"Attempting to delete Graph...");
                GraphAPI.DeleteGraphMeta(_graph_meta["graphGUID"].ToString() );
                return;
            }

            // put graph child data
            JObject graphChildObj = GraphAPI.PutGraphChild(_graph_meta["graphGUID"].ToString(), graphFileObj["graphFileGUID"].ToString());
            if (graphChildObj == null)
            {
                Graph.Utility.SaveAuditLog("SaveGraph", "ERROR", "", "", $"Attempting to delete Graph...");
                GraphAPI.DeleteGraphMeta(_graph_meta["graphGUID"].ToString());
                return;
            }

            // put graph file data
            JObject fileDataObj = new JObject();
            fileDataObj["graphFileData"] = Utility.Base64Encode(finaldata);

           
            JObject graphFileData = GraphAPI.PutGraphFileData(graphChildObj["graphGUID"].ToString(), fileDataObj);
            if (graphFileData == null)
            {
                Graph.Utility.SaveAuditLog("SaveGraph", "ERROR", "", "", $"Attempting to delete Graph...");
                GraphAPI.DeleteGraphMeta(_graph_meta["graphGUID"].ToString());
                return;
            }

            // put graph file image 
            JObject graphFileImage = GraphAPI.PutGraphFileImage(graphFileObj["graphFileGUID"].ToString(), fileDataObj);
            if (graphFileImage == null)
            {
                GraphAPI.DeleteGraphMeta(_graph_meta["graphGUID"].ToString());
                return;
            }

            


            System.Windows.Forms.MessageBox.Show("Saved Graph Data");
            _is_new = false;
        }
    }
}
