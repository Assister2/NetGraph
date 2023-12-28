
using CefSharp;
using CefSharp.OffScreen;
using FuzzySharp;
using CyConex.Helpers;
using Newtonsoft.Json.Linq;
using Syncfusion.Windows.Forms.Diagram;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Documents;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace CyConex.Graph
{
    internal class Utility
    {
        public static bool is_log = true;
        JObject ParseIdToken(string idToken)
        {
            // Parse the idToken to get user info
            idToken = idToken.Split('.')[1];
            idToken = Base64UrlDecode(idToken);
            return JObject.Parse(idToken);
        }

        public static bool CheckInternetConnection()
        {
            try
            {
                using (var client = new WebClient())
                using (var stream = client.OpenRead("http://www.google.com"))
                {
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        public static bool IsBase64String(string base64)
        {
            try
            {
                Convert.FromBase64String(base64);
                return true;
            }

            catch (Exception ex)
            {
                return false;
            }
        }

        public static void SaveAuditLog(string apiName, string url_type, string apiUrl, string status_code, string response)
        {
            if (is_log == false) return;
            string logs = "\n[" + DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss") + "]";
            logs += " - " + apiName;
            logs += " - " + url_type.ToUpper() + " : " + apiUrl;
            logs += " - " + status_code;
            logs += " - " + response;

            StreamWriter sw = new StreamWriter(@"Graph\audit.log", true);

            sw.Write(logs);
            sw.Close();
        }

        public static void ClearAuditLog()
        {
            if (File.Exists(@"Graph\audit.log"))
            {
                System.IO.File.WriteAllText(@"Graph\audit.log", string.Empty);
            }
        }

        private string Base64UrlDecode(string s)
        {
            s = s.Replace('-', '+').Replace('_', '/');
            s = s.PadRight(s.Length + (4 - s.Length % 4) % 4, '=');
            var byteArray = Convert.FromBase64String(s);
            var decoded = Encoding.UTF8.GetString(byteArray, 0, byteArray.Count());
            return decoded;
        }

        public static void ClearBubbleSet(ChromiumWebBrowser browser)
        {
            browser.EvaluateScriptAsync($"ClearBubbleSet()");
        }

        public static void ShowBubbleSet(ChromiumWebBrowser browser, List<string> node_guids, List<string> edge_guids, Color color)
        {
            string node_ids = string.Join(",", node_guids);
            node_ids = "[" + node_ids + "]";

            string edge_ids = string.Join(",", edge_guids);
            edge_ids = "[" + edge_ids + "]";
            string color_str = color.ToString();
            browser.EvaluateScriptAsync($"ShowBubbleSet('{node_ids}', '{edge_ids}', '{color_str}')");
        }

        public static string RemoveHTML(string input)
        {
            string tempString = input;
            tempString = Regex.Replace(input, "<style>(.|\n)*?</style>", string.Empty);
            tempString = Regex.Replace(tempString, @"<xml>(.|\n)*?</xml>", string.Empty); // remove all <xml></xml> tags and anything inbetween.  
            tempString = Regex.Replace(tempString, @"<(.|\n)*?>", string.Empty); // remove any tags but not there content "<p>bob<span> johnson</span></p>" becomes "bob johnson"
            tempString = WebUtility.HtmlDecode(tempString);
            return tempString;
        }

        public static string RemoveRTFFormatting(string rtfContent)
        {
            rtfContent = rtfContent.Trim();

            Regex rtfRegEx = new Regex("({\\\\)(.+?)(})|(\\\\)(.+?)(\\b)",
                                            RegexOptions.IgnoreCase
                                            | RegexOptions.Multiline
                                            | RegexOptions.Singleline
                                            | RegexOptions.ExplicitCapture
                                            | RegexOptions.IgnorePatternWhitespace
                                            | RegexOptions.Compiled
                                            );
            string output = rtfRegEx.Replace(rtfContent, string.Empty);
            output = Regex.Replace(output, @"\}", string.Empty); //replacing the remainingbraces

            return output;

        }

        public static JArray LoadNodeViewList()
        {
            string cyFileData = "";
            if (File.Exists(@"Graph\nodelist.data"))
            {
                StreamReader sr = new StreamReader(@"Graph\nodelist.data");
                cyFileData = sr.ReadToEnd();
                sr.Close();
            }

            JObject cyJsonFileData = cyFileData == "" ? null : JObject.Parse(cyFileData);
            JArray nodeLists = (JArray)cyJsonFileData["node_list"];
            return nodeLists;
        }

        public static void UpdateNodeListFileData(List<Node> NodeListViewItems)
        {
            string json_data = "{node_list: [";
            string item;
            for (int i = 0; i < NodeListViewItems.Count; i++)
            {
                item = "{";
                item += "id: '" + NodeListViewItems[i].ID + "',";
                item += "type:'" + NodeListViewItems[i].Type.Name + "',";
                item += "title:'" + NodeListViewItems[i].Title + "',";
                item += "titleSize:'" + NodeListViewItems[i].TitleSize + "',";
                item += "titleTextColor:'" + NodeListViewItems[i].TitleTextColor + "',";
                item += "titlePosition:'" + NodeListViewItems[i].NodeTitlePosition + "',";
                item += "imagePath:'" + NodeListViewItems[i].ImagePath + "',";
                item += "note:'" + NodeListViewItems[i].Note + "',";
                item += "frameworkReference:'" + NodeListViewItems[i].frameworkReference + "',";
                item += "frameworkName:'" + NodeListViewItems[i].frameworkName + "',";
                item += "frameworkNameVersion:'" + NodeListViewItems[i].ControlFrameworkVersion + "',";
                item += "category:'" + NodeListViewItems[i].Category + "',";
                item += "domain:'" + NodeListViewItems[i].Domain + "',";
                item += "subdomain:'" + NodeListViewItems[i].SubDomain + "',";
                item += "refurl:'" + NodeListViewItems[i].ReferenceURL + "',";
                item += "level:'" + NodeListViewItems[i].Level + "',";
                item += "objectiveTargetType:'" + NodeListViewItems[i].objectiveTargetType + "',";
                item += "objectiveTargetValue:'" + NodeListViewItems[i].objectiveTargetValue + "',";

                item += "inherentStrengthValue:'" + NodeListViewItems[i].InherentStrengthValue + "',";
                item += "implementedStrength:'" + NodeListViewItems[i].ImplementedStrength + "',";
                item += "implementedStrengthValue:'" + NodeListViewItems[i].ImplementedStrengthValue + "',";

                item += "assetConfidentiality:'" + NodeListViewItems[i].assetConfidentialityText + "',";
                item += "assetIntegrity:'" + NodeListViewItems[i].assetConfidentialityText + "',";
                item += "assetAvailability:'" + NodeListViewItems[i].assetConfidentialityText + "',";
                item += "assetAccountable:'" + NodeListViewItems[i].assetConfidentialityText + "',";
                item += "actorCapability:'" + NodeListViewItems[i].actorCapabilityText + "',";
                item += "actorResources:'" + NodeListViewItems[i].actorResourcesText + "',";
                item += "actorMotivation:'" + NodeListViewItems[i].actorMotivationText + "',";
                item += "actorAccess:'" + NodeListViewItems[i].actorAccessText + "',";
                item += "attackComplex:'" + NodeListViewItems[i].attackComplexityText + "',";
                item += "attackProliferation:'" + NodeListViewItems[i].attackProliferationText + "',";
                item += "attackImpactToIntegrity:'" + NodeListViewItems[i].attackImpactToIntegrityText + "',";
                item += "attackImpactToConfidentiality:'" + NodeListViewItems[i].attackImpactToConfidentialityText + "',";
                item += "attackImpactToAvailability:'" + NodeListViewItems[i].attackImpactToAvailabilityText + "',";
                item += "attackImpactToAccountability:'" + NodeListViewItems[i].attackImpactToAccountabilityText + "',";

                item += "vulnerabilityEaseOfExploitation:'" + NodeListViewItems[i].vulnerabilityEaseOfExploitationText + "',";
                item += "vulnerabilityExposure:'" + NodeListViewItems[i].vulnerabilityExposureText + "',";
                item += "vulnerabilityImpactToConfidentiality:'" + NodeListViewItems[i].vulnerabilityImpactToConfidentialityText + "',";
                item += "vulnerabilityImpactToIntegrity:'" + NodeListViewItems[i].vulnerabilityImpactToIntegrityText + "',";
                item += "vulnerabilityImpactToAvailability:'" + NodeListViewItems[i].vulnerabilityImpactToAvailabilityText + "',";
                item += "vulnerabilityImpactToAccountability:'" + NodeListViewItems[i].vulnerabilityImpactToAccountabilityText + "',";

                item += "description:'" + NodeListViewItems[i].description + "',";
                item += "metaTags:'" + NodeListViewItems[i].MetaTags + "',";
                item += "shape:'" + NodeListViewItems[i].Shape + "',";
                item += "color:'" + NodeListViewItems[i].Color + "',";
                item += "borderColor:'" + NodeListViewItems[i].BorderColor + "',";
                item += "nodeImageData:'" + NodeListViewItems[i].NodeImageData + "'},";
                json_data += item;
            }
            json_data += "]}";
            StreamWriter sw = new StreamWriter(@"Graph\nodelist.data");
            sw.WriteLine(json_data);
            sw.Close();
        }

        public static JArray LoadLinkedNodes()
        {
            string cyFileData = "";
            if (File.Exists(@"Graph\linkednode.data"))
            {
                StreamReader sr = new StreamReader(@"Graph\linkednode.data");
                cyFileData = sr.ReadToEnd();
                sr.Close();
            }
            JObject cyJsonFileData = cyFileData == "" ? null : JObject.Parse(cyFileData);
            JArray arr = (JArray)cyJsonFileData["linked_nodes"];
            return arr;
        }

        public static JArray LoadNodeList()
        {
            string cyFileData = "";
            if (File.Exists(@"Graph\nodelist.data"))
            {
                StreamReader sr = new StreamReader(@"Graph\nodelist.data");
                cyFileData = sr.ReadToEnd();
                sr.Close();
            }
            JObject cyJsonFileData = cyFileData == "" ? null : JObject.Parse(cyFileData);
            JArray arr = (JArray)cyJsonFileData["node_list"];
            return arr;
        }

        public static Node LoadNodeViewItem(JArray arr, string node_id)
        {
            Node node = new Node();
            for (int i = 0; i < arr.Count; i++)
            {
                JObject item = (JObject)arr[i];
                if (item["id"].ToString() == node_id)
                {
                    node = JObjectToNode(item);
                }
            }
            return node;
        }

        public static Node GetNodeListItem(List<Node> arr, string node_id)
        {
            Node node = new Node();
            for (int i = 0; i < arr.Count; i++)
            {
                Node item = (Node)arr[i];
                if (item.ID == node_id)
                {
                    return item;
                }
            }
            return node;
        }

        public static Node JObjectToNode(JObject tmp)
        {
            Node tmp_node = new Node();
            NodeType nt = new NodeType("type", tmp["type"].ToString());
            try
            {
                tmp_node.Type = nt;
                tmp_node.ID = tmp["id"].ToString();
                tmp_node.masterID = tmp["masterID"] != null ? tmp["masterID"].ToString() : "";
                tmp_node.Title = tmp["title"].ToString();
                tmp_node.TitleSize = (double)tmp["titleSize"];
                tmp_node.TitleTextColor = GeneralHelpers.ConvertColorFromHTML(tmp["titleTextColor"].ToString());
                tmp_node.NodeTitlePosition = tmp["titlePosition"] != null ? tmp["titlePosition"].ToString() : "";
                tmp_node.ImagePath = tmp["imagePath"].ToString();
                tmp_node.Note = tmp["note"].ToString();
                tmp_node.frameworkName = tmp["frameworkName"].ToString();
                tmp_node.frameworkReference = tmp["frameworkReference"].ToString();
                tmp_node.ControlFrameworkVersion = tmp["frameworkNameVersion"].ToString();
                tmp_node.Category = tmp["category"].ToString();
                tmp_node.Domain = tmp["domain"] != null ? tmp["domain"].ToString() : "";
                tmp_node.SubDomain = tmp["subdomain"] == null ? "" : tmp["subdomain"].ToString();
                tmp_node.ReferenceURL = tmp["refurl"] == null ? "" : tmp["refurl"].ToString();
                tmp_node.Level = tmp["level"].ToString();
                tmp_node.objectiveTargetType = tmp["objectiveTargetType"].ToString();
                tmp_node.objectiveTargetValue = tmp["objectiveTargetValue"].ToString();

                tmp_node.InherentStrengthValue = tmp["inherentStrengthValue"].ToString();
                tmp_node.ImplementedStrength = tmp["implementedStrength"].ToString();
                tmp_node.ImplementedStrengthValue = tmp["implementedStrengthValue"].ToString();
                tmp_node.description = tmp["description"].ToString();
                tmp_node.MetaTags = tmp["metaTags"].ToString();
                NodeShape ns = new NodeShape("shape", tmp["shape"].ToString());
                tmp_node.Shape = ns;
                tmp_node.Color = GeneralHelpers.ConvertColorFromHTML(tmp["color"].ToString());
                tmp_node.BorderColor = tmp["borderColor"] != null ?
                        GeneralHelpers.ConvertColorFromHTML(tmp["borderColor"].ToString()) : Color.Black;
                tmp_node.NodeImageData = tmp["nodeImageData"].ToString();

                tmp_node.actorCapabilityValue = tmp["actorCapabilityValue"] != null ? Convert.ToDouble(tmp["actorCapabilityValue"], CultureInfo.InvariantCulture) : 1;
                tmp_node.actorResourcesValue = tmp["actorResourcesValue"] != null ? Convert.ToDouble(tmp["actorResourcesValue"], CultureInfo.InvariantCulture) : 1;
                tmp_node.actorMotivationValue = tmp["actorMotivationValue"] != null ? Convert.ToDouble(tmp["actorMotivationValue"], CultureInfo.InvariantCulture) : 1;
                tmp_node.actorAccessValue = tmp["actorAccessValue"] != null ? Convert.ToDouble(tmp["actorAccessValue"], CultureInfo.InvariantCulture) : 1;
                tmp_node.actorScore = tmp["actorScore"] != null ? Convert.ToDouble(tmp["actorScore"], CultureInfo.InvariantCulture) : 1;

                tmp_node.attackComplexityValue = tmp["attackComplexityValue"] != null ? Convert.ToDouble(tmp["attackComplexityValue"], CultureInfo.InvariantCulture) : 1;
                tmp_node.attackProliferationValue = tmp["attackProliferationValue"] != null ? Convert.ToDouble(tmp["attackProliferationValue"], CultureInfo.InvariantCulture) : 1;
                tmp_node.attackImpactToConfidentialityValue = tmp["attackImpactToConfidentialityValue"] != null ? Convert.ToDouble(tmp["attackImpactToConfidentialityValue"], CultureInfo.InvariantCulture) : 1;
                tmp_node.attackImpactToIntegrityValue = tmp["attackImpactToIntegrityValue"] != null ? Convert.ToDouble(tmp["attackImpactToIntegrityValue"], CultureInfo.InvariantCulture) : 1;
                tmp_node.attackImpactToAvailabilityValue = tmp["attackImpactToAvailabilityValue"] != null ? Convert.ToDouble(tmp["attackImpactToAvailabilityValue"], CultureInfo.InvariantCulture) : 1;
                tmp_node.attackImpactToAccountabilityValue = tmp["attackImpactToAccountabilityValue"] != null ? Convert.ToDouble(tmp["attackImpactToAccountabilityValue"], CultureInfo.InvariantCulture) : 1;
                tmp_node.attackScore = tmp["attackScore"] != null ? Convert.ToDouble(tmp["attackScore"], CultureInfo.InvariantCulture) : 1;

                tmp_node.assetConfidentialityValue = tmp["assetConfidentialityValue"] != null ? Convert.ToDouble(tmp["assetConfidentialityValue"], CultureInfo.InvariantCulture) : 1;
                tmp_node.assetIntegrityValue = tmp["assetIntegrityValue"] != null ? Convert.ToDouble(tmp["assetIntegrityValue"], CultureInfo.InvariantCulture) : 1;
                tmp_node.assetAvailabilityValue = tmp["assetAvailabilityValue"] != null ? Convert.ToDouble(tmp["assetAvailabilityValue"], CultureInfo.InvariantCulture) : 1;
                tmp_node.assetAccountabilityValue = tmp["assetAccountabilityValue"] != null ? Convert.ToDouble(tmp["assetAccountabilityValue"], CultureInfo.InvariantCulture) : 1;
                tmp_node.assetScore = tmp["assetScore"] != null ? Convert.ToDouble(tmp["assetScore"], CultureInfo.InvariantCulture) : 1;

                tmp_node.vulnerabilityEaseOfExploitationValue = tmp["vulnerabilityEaseOfExploitationValue"] != null ? Convert.ToDouble(tmp["vulnerabilityEaseOfExploitationValue"], CultureInfo.InvariantCulture) : 1;
                tmp_node.vulnerabilityEaseOfExploitationMinValue = tmp["vulnerabilityEaseOfExploitationMinValue"] != null ? Convert.ToDouble(tmp["vulnerabilityEaseOfExploitationMinValue"], CultureInfo.InvariantCulture) : 1;
                tmp_node.vulnerabilityExposureValue = tmp["vulnerabilityExposureValue"] != null ? Convert.ToDouble(tmp["vulnerabilityExposureValue"], CultureInfo.InvariantCulture) : 1;
                tmp_node.vulnerabilityExposureMinValue = tmp["vulnerabilityExposureMinValue"] != null ? Convert.ToDouble(tmp["vulnerabilityExposureMinValue"], CultureInfo.InvariantCulture) : 1;
                tmp_node.vulnerabilityExposesScopeValue = tmp["vulnerabilityExposesScopeValue"] != null ? Convert.ToDouble(tmp["vulnerabilityExposesScopeValue"], CultureInfo.InvariantCulture) : 1;
                tmp_node.vulnerabilityExposesScopeMinValue = tmp["vulnerabilityExposesScopeMinValue"] != null ? Convert.ToDouble(tmp["vulnerabilityExposesScopeMinValue"], CultureInfo.InvariantCulture) : 1;
                tmp_node.vulnerabilityInteractionRequiredValue = tmp["vulnerabilityInteractionRequiredValue"] != null ? Convert.ToDouble(tmp["vulnerabilityInteractionRequiredValue"], CultureInfo.InvariantCulture) : 1;
                tmp_node.vulnerabilityInteractionRequiredMinValue = tmp["vulnerabilityInteractionRequiredMinValue"] != null ? Convert.ToDouble(tmp["vulnerabilityInteractionRequiredMinValue"], CultureInfo.InvariantCulture) : 1;
                tmp_node.vulnerabilityPrivilegesRequiredValue = tmp["vulnerabilityPrivilegesRequiredValue"] != null ? Convert.ToDouble(tmp["vulnerabilityPrivilegesRequiredValue"], CultureInfo.InvariantCulture) : 1;
                tmp_node.vulnerabilityPrivilegesRequiredMinValue = tmp["vulnerabilityPrivilegesRequiredMinValue"] != null ? Convert.ToDouble(tmp["vulnerabilityPrivilegesRequiredMinValue"], CultureInfo.InvariantCulture) : 1;
                tmp_node.vulnerabilityImpactToConfidentialityValue = tmp["vulnerabilityImpactToConfidentialityValue"] != null ? Convert.ToDouble(tmp["vulnerabilityImpactToConfidentialityValue"], CultureInfo.InvariantCulture) : 1;
                tmp_node.vulnerabilityImpactToIntegrityValue = tmp["vulnerabilityImpactToIntegrityValue"] != null ? Convert.ToDouble(tmp["vulnerabilityImpactToIntegrityValue"], CultureInfo.InvariantCulture) : 1;
                tmp_node.vulnerabilityImpactToAvailabilityValue = tmp["vulnerabilityImpactToAvailabilityValue"] != null ? Convert.ToDouble(tmp["vulnerabilityImpactToAvailabilityValue"], CultureInfo.InvariantCulture) : 1;
                tmp_node.vulnerabilityImpactToAccountabilityValue = tmp["vulnerabilityImpactToAccountabilityValue"] != null ? Convert.ToDouble(tmp["vulnerabilityImpactToAccountabilityValue"], CultureInfo.InvariantCulture) : 1;

            }
            catch
            { }

            return tmp_node;
        }

        public static void SaveLinkedNodes(JArray arr)
        {
            string json_data = "{linked_nodes: [";
            string item;
            for (int i = 0; i < arr.Count; i++)
            {
                item = "{";
                item += "source_node_id: '" + arr[i]["source_node_id"] + "',";
                item += "target_node_id:'" + arr[i]["target_node_id"] + "',";
                item += "edge_title:'" + arr[i]["edge_title"] + "',";
                item += "edge_description:'" + arr[i]["edge_description"] + "',";
                item += "edge_relationship:'" + arr[i]["edge_relationship"] + "',";
                item += "edge_strength:'" + arr[i]["edge_strength"] + "',";
                item += "edge_strength_value:'" + arr[i]["edge_strength_value"] + "'},";
                json_data += item;
            }
            json_data += "]}";
            StreamWriter sw = new StreamWriter(@"Graph\linkednode.data");
            sw.WriteLine(json_data);
            sw.Close();
        }

        public static string DecodeBase64RTF(string str)
        {
            if (str == null)
            {
                return "";
            }

            var content = System.Convert.FromBase64String(str);
            string final_content = System.Text.Encoding.UTF8.GetString(content);
            return final_content;
        }

        public static string DecodeBase64Text(string str)
        {
            if (str == null || str == "")
                return "";

            var content = System.Convert.FromBase64String(str);
            string final_content = System.Text.Encoding.UTF8.GetString(content);
            return final_content;
        }

        public static string DecodeBase64TextandRemoveRTF(string str)
        {
            if (str == null || str == "")
                return "";

            var content = System.Convert.FromBase64String(str);
            string final_content = System.Text.Encoding.UTF8.GetString(content);
            return RemoveRTFFormatting(final_content);
        }

        public static String WildCardToRegular(String value)
        {
            return System.Text.RegularExpressions.Regex.Escape(value).Replace("\\?", ".").Replace("\\*", ".*") + "$";
        }

        public static bool isFuzzySearch(bool is_fuzzy, string content, string str)
        {
            bool flag = false;
            content = content.Replace('\n', ' ');
            string[] content_arr = content.Split(' ');
            foreach (string s in content_arr)
            {
                double tmp = Fuzz.Ratio(str.ToLower(), s.ToLower());
                if (tmp > 70)
                {
                    flag = true;
                    break;
                }
            }

            return is_fuzzy ? flag : false;
        }

        public static ArrayList SearchSuggestedIDs(string masterID)
        {
            ArrayList results = new ArrayList();
            JArray arr = new JArray();
            arr = LoadLinkedNodes();
            foreach (JObject node in arr)
            {
                if (node["target_node_id"].ToString() == masterID)
                {
                    results.Add(node["source_node_id"].ToString());
                }
            }
            return results;
        }

        public static string GetBase64StringForImage(string imgPath)
        {
            byte[] imageBytes = System.IO.File.ReadAllBytes(imgPath);
            string base64String = Convert.ToBase64String(imageBytes);
            return base64String;
        }

        public static ImageData GetImageDataFromFile(string imgPath)
        {
            if (imgPath == null || imgPath == "")
            {
                return new ImageData();
            }

            ImageData imageData = new ImageData();
            imageData.Image = new Bitmap(imgPath);
            // image file path  
            imageData.ImagePath = imgPath;
            imageData.Data = Utility.GetBase64StringForImage(imgPath);

            System.Drawing.Image image = System.Drawing.Image.FromFile(imgPath);
            imageData.ImageWidth = image.Width.ToString();
            imageData.ImageHeight = image.Height.ToString();

            return imageData;
        }

        public static string GetSVGDataFromFile(string svgPath)
        {
            if (svgPath == null || svgPath == "") return null;

            string svgData = System.IO.File.ReadAllText(svgPath);
            return svgData;
        }

        public static string ListToString(List<string> list)
        {
            string ret = "";
            foreach (string item in list)
            {
                ret += item + "\n";
            }
            return ret;
        }

        public static string GetFileName(string path)
        {
            path = Path.GetFileName(path);
            string[] arr = path.Split('.');
            return arr[0];
        }

        public static string ConvertRecentImagePath(string path)
        {
            string flname = GetFileName(path);
            path = "Recent/" + flname + ".png";
            return path;
        }

        public static ImageData GetImageData(string path)
        {
            ImageData imageData = new ImageData();
            if (!File.Exists(path))
            {
                path = "Recent/node_default.png";
            }
            imageData = GetImageDataFromFile(path);
            return imageData;
        }

        public static ImageData DefaultNodeImage(string type)
        {
            string path = "Resources/nodes/";
            ImageData imageData = new ImageData();
            switch (type.ToLower())
            {
                case "control":
                    path += "control.png";
                    break;
                case "group":
                    path += "group.png";
                    break;
                case "asset":
                    path += "asset.png";
                    break;
                case "attack":
                    path += "attack.png";
                    break;
                case "actor":
                    path += "actor.png";
                    break;
                case "objective":
                    path += "objective.png";
                    break;
                case "info":
                    path += "infobox.png";
                    break;
                case "default_graph":
                    path = "Resources/Icons/Node-WF.png";
                    break;
                case "vulnerability":
                    path += "vulnerability.png";
                    break;
                case "vulnerability-group":
                    path += "vulnerability.png";
                    break;
                case "evidence":
                    path += "evidence.png";
                    break;
                default:
                    path += "control.png";
                    break;

            }
            imageData = GetImageDataFromFile(path);
            return imageData;
        }

        public static int CheckSourceAndTargetNode(JArray edge_list, string node1, string node2)
        {
            // 0 : nothing, 1: source node, 2: target node, 3: both of them.
            int flag = 0;
            if (node1 != null && node2 != null)
            {
                for (int i = 0; i < edge_list.Count; i++)
                {
                    JObject obj = (JObject)edge_list[i];
                    if (obj["source_node_id"].ToString() == node1 && obj["target_node_id"].ToString() == node2)
                    {
                        flag += 2;
                    }
                    else if (obj["source_node_id"].ToString() == node2 && obj["target_node_id"].ToString() == node1)
                    {
                        flag += 1;
                    }
                }
            }
            return flag;
        }

        public static Image GetImageFromBase64(string base64)
        {
            try
            {
                byte[] b = Convert.FromBase64String(base64);
                System.IO.MemoryStream m = new System.IO.MemoryStream(b);
                Image img = System.Drawing.Image.FromStream(m);
                return img;
            }
            catch
            {
                return new Bitmap(16, 16);
            }

        }

        public static bool IsUrlValid(string webUrl)
        {
            if (webUrl == null)
            {
                return false;
            }

            return Regex.IsMatch(webUrl, @"(http|https)://(([www\.])?|([\da-z-\.]+))\.([a-z\.]{2,3})$");
        }

        public static string GetFileExtension(string file_name)
        {
            string[] arr = file_name.Split('.');
            return arr[arr.Length - 1].ToString();
        }
        public static int FindDataFromList(List<JObject> list, string data)
        {
            int ind = -1;
            for (int i = 0; i < list.Count; i++)
            {
                JObject obj = list[i];
                if (obj["path"].ToString() == data)
                {
                    ind = i;
                    break;
                }
            }
            return ind;
        }

        public static bool IsBase64(string base64String)
        {
            // Credit: oybek https://stackoverflow.com/users/794764/oybek
            if (string.IsNullOrEmpty(base64String) || base64String.Length % 4 != 0
               || base64String.Contains(" ") || base64String.Contains("\t") || base64String.Contains("\r") || base64String.Contains("\n"))
                return false;

            try
            {
                Convert.FromBase64String(base64String);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static string GenerateRandomString(int length = 5)
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
            var stringChars = new char[length];
            var random = new Random();

            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }

            var finalString = new String(stringChars);
            return finalString.ToLower();
        }

        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }

        public static string Base64Decode(string base64EncodedData)
        {
            try
            {
                var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
                return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
            }
            catch (Exception e)
            {
                return base64EncodedData;
            }
        }

        public static void DeleteTempFiles()
        {
            string tempPath = Path.GetTempPath();
            List<string> list = Directory.GetFiles(tempPath, "*.graphtmp", SearchOption.TopDirectoryOnly).ToList();
            if (list.Count > 0)
            {
                foreach(string file_path in list)
                {
                    File.Delete(file_path);
                }
            }
        }

        public static string GraphTempPath()
        {
            string tempPath = Path.GetTempPath();
            return tempPath + "/" + Utility.GenerateRandomString() + ".graphtmp";
        }

        public static void SaveFile(string path, string data)
        {
            if (File.Exists(path))
            {
                File.Delete(path);
            }
            File.WriteAllText(path, data, Encoding.UTF8);
        }

        public static double CalculateSizeIncreaseFactor(int input)
        {
            // Calculate base of the logarithm for a 10% increase at input 1
            double b = Math.Pow(10, 10);

            // Apply a logarithmic transformation to the input and convert it to a size increase factor.
            // Added a multiplier of 0.5 to make the size increase more noticeable.
            double factor = 20 * Math.Log(input + 1) / Math.Log(b) + 1;

            // Return the size increase factor.
            return factor;
        }

        public static string PromptForFile(string Filters)
        {
            // Create an instance of OpenFileDialog
            OpenFileDialog openFileDialog = new OpenFileDialog();

            // Set filter options and filter index
            openFileDialog.Filter = Filters;
            openFileDialog.FilterIndex = 1;
            openFileDialog.Multiselect = false;

            // Call the ShowDialog method to show the dialog box
            DialogResult userClickedOK = openFileDialog.ShowDialog();

            // Process input if the user clicked OK
            if (userClickedOK == DialogResult.OK)
            {
                // Return the selected file path
                return openFileDialog.FileName;
            }
            else
                return null;
        }

        public static string PromptForFolder()
        {
            FolderBrowserDialog folderBrowserDialog1 = new FolderBrowserDialog();

            folderBrowserDialog1.Description = "Select the directory to save the file";

            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {

                return folderBrowserDialog1.SelectedPath;

            }
            else { return null; }
        }
    }
}
