using CyConex.Graph;
using Newtonsoft.Json.Linq;
using Syncfusion.Windows.Forms.Diagram;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Node = CyConex.Graph.Node;

namespace CyConex.API
{
    public class NodeAPI
    {
        public static JObject PostRepoNodeMeta(string nodeTitle, string nodeType, string category, string subCategory)
        {
            Graph.Utility.SaveAuditLog("PostRepoNodeMeta", "+++FUNCTION ENTERED+++", "", "", $"");
            HttpResponseMessage response = null;
            JObject ret_obj = new JObject();
            try
            {

                HttpMethod verb = HttpMethod.Post;
                string sub_url = "/netgraph/v1.0/nodes/meta?enterpriseGUID=" + AuthAPI._enterprise_guid + "&userGUID=" + AuthAPI._user_guid + "&subscription-key=" + ApplicationSettings.ApiSubscriptionKey;
                string requestUrl = ApplicationSettings.ApiRootURL + sub_url;
                int httpTimeOutInSec = 60;
                JObject meta = new JObject();
                string created_date = DateTime.UtcNow.ToString("dd/MM/yyyy");
                created_date = created_date.Replace('.', '/');
                meta["createdDate"] = created_date;
                meta["nodeType"] =  nodeType;
                if (category == null)
                    category = "";
                meta["category"] = category;
                if (subCategory == null)
                    subCategory = "";
                meta["subCategory"] = subCategory;
                meta["tenantGUID"] = AuthAPI._tenant_guid;
                meta["nodeTitle"] = nodeTitle;

                response = InvokeApiEndpoint.CreateOrUpdateAPI(verb, AuthAPI._account_token, requestUrl, httpTimeOutInSec, meta);
                ret_obj = JObject.Parse(response.Content.ReadAsStringAsync().Result);
                
            }
            catch (Exception ex)
            {
                return null;
            }

            return ret_obj;
        }

        public static JObject PutRepoNodeFramework(string nodeGUID, Graph.Node node)
        {
            Graph.Utility.SaveAuditLog("PutRepoNodeFramework", "+++FUNCTION ENTERED+++", "", "", $"");
            JObject ret_obj = new JObject();
            HttpResponseMessage response = null; 
            try
            {
                HttpMethod verb = HttpMethod.Put;
                string sub_url = "/netgraph/v1.0/nodes/" + nodeGUID + "/framework?" + "userGUID=" + AuthAPI._user_guid + "&subscription-key=" + ApplicationSettings.ApiSubscriptionKey;
                string requestUrl = ApplicationSettings.ApiRootURL + sub_url;
                int httpTimeOutInSec = 60;
                JObject obj = new JObject();
                obj["category"] = node.Category;
                obj["subCategory"] = node.SubCategory;
                obj["framework"] = node.frameworkName;
                obj["version"] = node.ControlFrameworkVersion;
                obj["reference"] = node.frameworkReference;
                obj["Domain"] = node.Domain;
                obj["subDomain"] = node.SubDomain;
                obj["level"] = node.Level;
                obj["refUrl"] = node.ReferenceURL;

                response = InvokeApiEndpoint.CreateOrUpdateAPI(verb, AuthAPI._account_token, requestUrl, httpTimeOutInSec, obj);
                ret_obj = JObject.Parse(response.Content.ReadAsStringAsync().Result);
                
            }
            catch (Exception ex)
            {
                return null;
            }

            return ret_obj;
        }

        public static JObject PutRepoNodeVisual(string nodeGUID, Node node)
        {
            Graph.Utility.SaveAuditLog("PutRepoNodeVisual", "+++FUNCTION ENTERED+++", "", "", $"");
            HttpResponseMessage response = null;
            JObject ret_obj = new JObject();
            try
            {
                HttpMethod verb = HttpMethod.Put;
                string sub_url = "/netgraph/v1.0/nodes/" + nodeGUID 
                    + "/visual?" + "enterpriseGUID=" + AuthAPI._enterprise_guid
                    + "&userGUID=" + AuthAPI._user_guid + "&subscription-key=" 
                    + ApplicationSettings.ApiSubscriptionKey;

                string requestUrl = ApplicationSettings.ApiRootURL + sub_url;
                int httpTimeOutInSec = 60;
                JObject obj = new JObject();

                obj["backgroundOpacity"] = node.HTMLOpacity.ToString();
                obj["borderWidth"] = 1;// node.BorderWidth.ToString();
                obj["borderOpacity"] = 1;
                obj["borderColor"] = "RGB(" + node.BorderColor.R.ToString() + "," + node.BorderColor.G.ToString() + "," + node.BorderColor.B.ToString() + ")";
                obj["color"] = "RGB(" + node.Color.R.ToString() + "," + node.Color.G.ToString() + "," + node.Color.B.ToString() + ")";
                obj["fontFamily"] = "Aria";
                obj["fontStyle"] = "normal";
                obj["fontWeight"] = "5";
                obj["height"] = Int16.Parse(node.Height.ToString());
                obj["image"] = node.NodeImageData;
                obj["imagePath"] = node.ImagePath;
                obj["opacity"] = 1;
                obj["originShape"] = node.Shape.Name.ToString();
                obj["originColor"] = "RGB(" + node.origin_color.R.ToString() + "," + node.origin_color.G.ToString() + "," + node.origin_color.B.ToString() + ")";
                obj["position"] = node.NodeTitlePosition.ToString();
                obj["shape"] = node.Shape.Name;
                obj["size"] = Int16.Parse(node.Size.ToString());
                obj["textValign"] = "textvalign";
                obj["textDecoration"] = "textDecoration";
                obj["title"] = node.Title.ToString();
                obj["titleTextColor"] = "RGB(" + node.TitleTextColor.R.ToString() + "," + node.TitleTextColor.G.ToString() + "," + node.TitleTextColor.B.ToString() + ")";
                obj["titleSize"] = Int16.Parse(node.TitleSize.ToString());
                obj["width"] = Int16.Parse(node.Width.ToString());
                obj["enabled"] = true;

                try
                {
                    response = InvokeApiEndpoint.CreateOrUpdateAPI(verb, AuthAPI._account_token, requestUrl, httpTimeOutInSec, obj);
                    ret_obj = JObject.Parse(response.Content.ReadAsStringAsync().Result);
                }
                catch (Exception ex)
                {
                    return null;
                }

            }
            catch (Exception ex)
            {
                return null;
            }

            return ret_obj;
        }

        public static JObject PutRepoNodeNote(string nodeGUID, Node node)
        {
            Graph.Utility.SaveAuditLog("PutRepoNodeNote", "+++FUNCTION ENTERED+++", "", "", $"");
            JObject ret_obj = new JObject();
            HttpResponseMessage response = null;
            try
            {
                HttpMethod verb = HttpMethod.Put;
                string sub_url = "/netgraph/v1.0/nodes/" + nodeGUID + "/note?" + "userGUID=" + AuthAPI._user_guid + "&subscription-key=" + ApplicationSettings.ApiSubscriptionKey;
                string requestUrl = ApplicationSettings.ApiRootURL + sub_url;
                int httpTimeOutInSec = 60;
                JObject obj = new JObject();
                obj["note"] = node.Note;

                response = InvokeApiEndpoint.CreateOrUpdateAPI(verb, AuthAPI._account_token, requestUrl, httpTimeOutInSec, obj);
                
                ret_obj = JObject.Parse(response.Content.ReadAsStringAsync().Result);
            }
            catch (Exception ex)
            {
                return null;
            }
            return ret_obj;
        }

        public static JObject PutRepoNodeDescription(string nodeGUID, Node node)
        {
            Graph.Utility.SaveAuditLog("PutRepoNodeDescription", "+++FUNCTION ENTERED+++", "", "", $"");
            JObject ret_obj = new JObject();
            HttpResponseMessage response = null;
            try
            {
                HttpMethod verb = HttpMethod.Put;
                string sub_url = "/netgraph/v1.0/nodes/" + nodeGUID + "/framework?" + "userGUID=" + AuthAPI._user_guid + "&subscription-key=" + ApplicationSettings.ApiSubscriptionKey;
                string requestUrl = ApplicationSettings.ApiRootURL + sub_url;
                int httpTimeOutInSec = 60;
                JObject obj = new JObject();
                obj["description"] = node.description;

                response = InvokeApiEndpoint.CreateOrUpdateAPI(verb, AuthAPI._account_token, requestUrl, httpTimeOutInSec, obj);
                ret_obj = JObject.Parse(response.Content.ReadAsStringAsync().Result);
                
            }
            catch (Exception ex)
            {
                return null;
            }

            return ret_obj;
        }

        public static JObject PutRepoNodeJSON(string nodeGUID, string nodeData )
        {
            Graph.Utility.SaveAuditLog("PutRepoNodeJSON", "+++FUNCTION ENTERED+++", "", "", $"");
            JObject ret_obj = new JObject();
            HttpResponseMessage response = null;
            try
            {
                HttpMethod verb = HttpMethod.Put;
                string sub_url = "/netgraph/v1.0/nodes/" + nodeGUID + "/json?" + "userGUID=" + AuthAPI._user_guid + "&tenantGUID=" + AuthAPI._tenant_guid + "&subscription-key=" + ApplicationSettings.ApiSubscriptionKey;
                string requestUrl = ApplicationSettings.ApiRootURL + sub_url;
                int httpTimeOutInSec = 60;
                JObject obj = new JObject();
                obj["nodeData"] = nodeData;

                response = InvokeApiEndpoint.CreateOrUpdateAPI(verb, AuthAPI._account_token, requestUrl, httpTimeOutInSec, obj);
                ret_obj = JObject.Parse(response.Content.ReadAsStringAsync().Result);

            }
            catch (Exception ex)
            {
                return null;
            }

            return ret_obj;
        }

        public static JObject PutRepoNodeGraph(string nodeGUID, Node node)
        {
            Graph.Utility.SaveAuditLog("PutRepoNodeGraph", "+++FUNCTION ENTERED+++", "", "", $"");
            JObject ret_obj = new JObject();
            HttpResponseMessage response = null; 
            try
            {

                HttpMethod verb = HttpMethod.Put;
                string sub_url = "/netgraph/v1.0/nodes/" + nodeGUID + "/graph?" + "userGUID=" + AuthAPI._user_guid + "&subscription-key=" + ApplicationSettings.ApiSubscriptionKey;
                string requestUrl = ApplicationSettings.ApiRootURL + sub_url;
                int httpTimeOutInSec = 60;
                JObject obj = new JObject();
                obj["nodeID"] = node.ID;
                obj["nodeType"] = node.Type.ToString();
                obj["enabled"] = node.Enabled;
                obj["implementedStrength"] = node.ImplementedStrength == null ? "" : node.ImplementedStrength;
                obj["objectiveTargetType"] = node.objectiveTargetType == null ? "" : node.objectiveTargetType;
                obj["objectiveTargetValue"] = node.objectiveTargetValue == null ? "" : node.objectiveTargetValue;
                obj["objectiveAcheivedValue"] = node.objectiveAcheivedValue == null ? "" : node.objectiveAcheivedValue;
                obj["actorScore"] = node.actorScore;
                obj["attackComplexityValue"] = node.attackComplexityValue;
                obj["attackScore"] = node.attackScore;
                obj["assetScore"] = node.assetScore;
                obj["riskScore"] = node.riskScore;
                obj["parent"] = node.Parent;
                obj["title"] = node.Title;
                obj["nodeBehaviour"] = node.nodeBehaviour;
                obj["assessedStatus"] = node.AssessedStatus;
                obj["controlAssessedValue"] = node.controlAssessedValue;
                obj["controlAssessedScore"] = node.controlAssessedScore;
                obj["controlAssessedMinValue"] = node.controlAssessedMinValue;
                obj["controlBaseScore"] = node.controlBaseScore;
                obj["controlBaseValue"] = node.controlBaseValue;
                obj["controlBaseMinValue"] = node.controlBaseMinValue;
                obj["controlBaseScoreAssessmentStatus"] = node.controlBaseScoreAssessmentStatus;
                obj["calculatedValue"] = node.CalculatedValue;
                obj["previousControlAssessedValue"] = node.controlAssessedValue;
                obj["previouscontrolBaseScore"] = node.controlBaseScore;
                obj["previouscalculatedValue"] = node.CalculatedValue;
                obj["actorAccess"] = node.actorAccessText;
                obj["actorAccessValue"] = node.actorAccessValue;
                obj["actorAccessMinValue"] = node.actorAccessMinValue;
                obj["actorCapability"] = node.actorCapabilityText;
                obj["actorCapabilityValue"] = node.actorCapabilityValue;
                obj["actorCapabilityMinValue"] = node.actorCapabilityMinValue;
                obj["actorMitigatedScore"] = node.actorMitigatedScore;
                obj["actorMotivation"] = node.actorMotivationText;
                obj["actorMotivationValue"] = node.actorMotivationValue;
                obj["actorMotivationMinValue"] = node.actorMotivationMinValue;
                obj["actorResources"] = node.actorResourcesText;
                obj["actorResourcesValue"] = node.actorResourcesValue;
                obj["actorResourcesMinValue"] = node.actorResourcesMinValue;
                obj["actorImpactToConfidentialityValue"] = node.actorImpactToConfidentialityValue;
                obj["actorImpactToConfidentialityMinValue"] = node.actorImpactToConfidentialityMinValue;
                obj["actorImpactToIntegrityValue"] = node.actorImpactToIntegrityValue;
                obj["actorImpactToIntegrityMinValue"] = node.actorImpactToIntegrityMinValue;
                obj["actorImpactToAvailabilityValue"] = node.actorImpactToAvailabilityValue;
                obj["actorImpactToAvailabilityMinValue"] = node.actorImpactToAvailabilityMinValue;
                obj["actorImpactToAccountabilityValue"] = node.actorImpactToAccountabilityValue;
                obj["actorImpactToAccountabilityMinValue"] = node.actorimpactToAccountabilityMinValue;
                obj["assetAccountability"] = node.assetAccountabilityText;
                obj["assetAccountabilityValue"] = node.assetAccountabilityValue;
                obj["assetAccountabilityMinValue"] = node.assetAccountabilityMinValue;
                obj["assetAccountabilityProbabilityValue"] = node.assetAccountabilityProbabilityValue;
                obj["assetAvailability"] = node.assetAvailabilityText;
                obj["assetAvailabilityValue"] = node.assetAvailabilityValue;
                obj["assetConfidentiality"] = node.assetConfidentialityText;
                obj["assetConfidentialityValue"] = node.assetConfidentialityValue;
                obj["assetConfidentialityMinValue"] = node.assetConfidentialityMinValue;
                obj["assetConfidentialityProbabilityValue"] = node.assetConfidentialityProbabilityValue;
                obj["assetIntegrity"] = node.assetIntegrityText;
                obj["assetIntegrityValue"] = node.assetIntegrityValue;
                obj["assetIntegrityMinValue"] = node.assetIntegrityMinValue;
                obj["assetIntegrityProbabilityValue"] = node.assetIntegrityProbabilityValue;
                obj["attackComplexity"] = node.attackComplexityText;
                obj["attackComplexityValue"] = node.attackComplexityValue;
                obj["attackImpactToAccountability"] = node.attackImpactToAccountabilityText;
                obj["attackImpactToAccountabilityValue"] = node.attackImpactToAccountabilityValue;
                obj["attackImpactToAvailability"] = node.attackImpactToAvailabilityText;
                obj["attackImpactToAvailabilityValue"] = node.attackImpactToAvailabilityValue;
                obj["attackImpactToAvailabilityMinValue"] = node.attackImpactToAvailabilityMinValue;
                obj["assetAvailabilityProbabilityValue"] = node.assetAvailabilityProbabilityValue;
                obj["attackImpactConfident"] = node.attackImpactToConfidentialityText;
                obj["attackImpactToIntegrity"] = node.attackImpactToIntegrityText;
                obj["attackImpactToIntegrityValue"] = node.attackImpactToIntegrityValue;
                obj["attackProliferation"] = node.attackProliferationText;
                obj["attackProliferationValue"] = node.attackProliferationValue;
                obj["attackProliferationMinValue"] = node.attackProliferationMinValue;
                obj["attackComplexityMinValue"] = node.attackComplexityMinValue;
                obj["attackImpactToAccountabilityMinValue"] = node.attackImpactToAccountabilityMinValue;
                obj["attackImpactToAvailabilityMinValue"] = node.attackImpactToAvailabilityMinValue;
                obj["attackImpactToConfidentialityValue"] = node.attackImpactToConfidentialityValue;
                obj["attackImpactToConfidentialityMinValue"] = node.attackImpactToConfidentialityMinValue;
                obj["attackImpactToIntegrityMinValue"] = node.attackImpactToIntegrityMinValue;
                obj["vulnerabilityEaseOfExploitationValue"] = node.vulnerabilityEaseOfExploitationValue;
                obj["vulnerabilityEaseOfExploitationMinValue"] = node.vulnerabilityEaseOfExploitationMinValue;
                obj["vulnerabilityExposureValue"] = node.vulnerabilityExposureValue;
                obj["vulnerabilityExposureMinValue"] = node.vulnerabilityExposureMinValue;
                obj["vulnerabilityExposesScopeValue"] = node.vulnerabilityExposesScopeValue;
                obj["vulnerabilityExposesScopeMinValue"] = node.vulnerabilityExposesScopeMinValue;
                obj["vulnerabilityInteractionRequiredValue"] = node.vulnerabilityInteractionRequiredValue;
                obj["vulnerabilityInteractionRequiredMinValue"] = node.vulnerabilityInteractionRequiredMinValue;
                obj["vulnerabilityPrivilegesRequiredMinValue"] = node.vulnerabilityPrivilegesRequiredMinValue;
                obj["vulnerabilityPrivilegesRequiredValue"] = node.vulnerabilityPrivilegesRequiredValue;
                obj["vulnerabilityImpactToConfidentialityValue"] = node.vulnerabilityImpactToConfidentialityValue;
                obj["vulnerabilityImpactToConfidentialityMinValue"] = node.vulnerabilityImpactToConfidentialityMinValue;
                obj["vulnerabilityImpactToIntegrityValue"] = node.vulnerabilityImpactToIntegrityValue;
                obj["vulnerabilityImpactToIntegrityMinValue"] = node.vulnerabilityImpactToIntegrityMinValue;
                obj["vulnerabilityImpactToAvailabilityValue"] = node.vulnerabilityImpactToAvailabilityValue;
                obj["vulnerabilityImpactToAvailabilityMinValue"] = node.vulnerabilityImpactToAvailabilityMinValue;
                obj["vulnerabilityImpactToAccountabilityValue"] = node.vulnerabilityImpactToAccountabilityValue;
                obj["vulnerabilityImpactToAccountabilityMinValue"] = node.vulnerabilityImpactToAccountabilityMinValue;
                obj["evidenceConfidenceValue"] = node.evidenceConfidenceValue;
                obj["assetRegulatoryImpactAvailabilityValue"] = node.assetRegulatoryImpactAvailabilityValue;
                obj["assetReputationalImpactAvailabilityValue"] = node.assetReputationalImpactAvailabilityValue;
                obj["assetFinancialImpactAvailabilityValue"] = node.assetFinancialImpactAvailabilityValue;
                obj["assetPrivacyImpactAccountabilityValue"] = node.assetPrivacyImpactAccountabilityValue;
                obj["assetLegalImpactAccountabilityValue"] = node.assetLegalImpactAccountabilityValue;
                obj["assetRegulatoryImpactAccountabilityValue"] = node.assetRegulatoryImpactAccountabilityValue;
                obj["assetReputationalImpactAccountabilityValue"] = node.assetReputationalImpactAccountabilityValue;
                obj["assetPrivacyImpactConfidentialityValue"] = node.assetPrivacyImpactConfidentialityValue;
                obj["assetLegalImpactConfidentialityValue"] = node.assetLegalImpactConfidentialityValue;
                obj["assetReputationalImpactConfidentialityValue"] = node.assetReputationalImpactConfidentialityValue;
                obj["assetFinancialImpactConfidentialityValue"] = node.assetFinancialImpactConfidentialityValue;
                obj["assetPrivacyImpactIntegrityValue"] = node.assetPrivacyImpactIntegrityValue;
                obj["assetLegalImpactIntegrityValue"] = node.assetLegalImpactIntegrityValue;
                obj["assetRegulatoryImpactIntegrityValue"] = node.assetRegulatoryImpactIntegrityValue;
                obj["assetFinancialImpactIntegrityValue"] = node.assetFinancialImpactIntegrityValue;
                obj["assetPrivacyImpactAvailabilityValue"] = node.assetPrivacyImpactAvailabilityValue;
                obj["assetLegalImpactAvailabilityValue"] = node.assetLegalImpactAvailabilityValue;
                obj["assetReputationalImpactIntegrityValue"] = node.assetReputationalImpactIntegrityValue;
                obj["assetPrivacyImpactConfidentialityMinValue"] = node.assetPrivacyImpactConfidentialityMinValue;
                obj["assetLegalImpactConfidentialityMinValue"] = node.assetLegalImpactConfidentialityMinValue;
                obj["assetRegulatoryImpactConfidentialityValue"] = node.assetRegulatoryImpactConfidentialityValue;
                obj["assetRegulatoryImpactConfidentialityMinValue"] = node.assetRegulatoryImpactConfidentialityMinValue;
                obj["assetReputationalImpactConfidentialityMinValue"] = node.assetReputationalImpactConfidentialityMinValue;
                obj["assetFinancialImpactConfidentialityMinValue"] = node.assetFinancialImpactConfidentialityMinValue;
                obj["assetPrivacyImpactIntegrityMinValue"] = node.assetPrivacyImpactIntegrityMinValue;
                obj["assetLegalImpactIntegrityMinValue"] = node.assetLegalImpactIntegrityMinValue;
                obj["assetRegulatoryImpactIntegrityMinValue"] = node.assetRegulatoryImpactIntegrityMinValue;
                obj["assetReputationalImpactIntegrityMinValue"] = node.assetReputationalImpactIntegrityMinValue;
                obj["assetFinancialImpactIntegrityMinValue"] = node.assetFinancialImpactIntegrityMinValue;
                obj["assetPrivacyImpactAvailabilityMinValue"] = node.assetPrivacyImpactAvailabilityMinValue;
                obj["assetLegalImpactAvailabilityMinValue"] = node.assetLegalImpactAvailabilityMinValue;
                obj["assetReputationalImpactAvailabilityMinValue"] = node.assetReputationalImpactAvailabilityMinValue;
                obj["assetFinancialImpactAvailabilityMinValue"] = node.assetFinancialImpactAvailabilityMinValue;
                obj["assetPrivacyImpactAccountabilityMinValue"] = node.assetPrivacyImpactAccountabilityMinValue;
                obj["assetLegalImpactAccountabilityMinValue"] = node.assetLegalImpactAccountabilityMinValue;
                obj["assetRegulatoryImpactAccountabilityMinValue"] = node.assetRegulatoryImpactAccountabilityMinValue;
                obj["assetReputationalImpactAccountabilityMinValue"] = node.assetReputationalImpactAccountabilityMinValue;
                obj["assetFinancialImpactAccountabilityValue"] = node.assetFinancialImpactAccountabilityValue;
                obj["assetFinancialImpactAccountabilityMinValue"] = node.assetFinancialImpactAccountabilityMinValue;
                obj["controlBaseDistribution"] = node.controlBaseDistribution;
                obj["controlAssessedDistribution"] = node.controlAssessedDistribution;
                obj["actoraccessDistribution"] = node.actorAccessDistribution;
                obj["actorCapabilityDistribution"] = node.actorCapabilityDistribution;
                obj["actorResourcesDistribution"] = node.actorResourcesDistribution;
                obj["actorMotivationDistribution"] = node.actorMotivationDistribution;
                obj["actorImpactToConfidentialityDistribution"] = node.actorImpactToConfidentialityDistribution;
                obj["actorImpactToIntegrityDistribution"] = node.actorImpactToIntegrityDistribution;
                obj["actorImpactToAvailabilityDistribution"] = node.actorImpactToAvailabilityDistribution;
                obj["actorImpactToAccountabilityDistribution"] = node.actorImpactToAccountabilityDistribution;
                obj["vulnerabilityEaseOfExploitationDistribution"] = node.vulnerabilityEaseOfExploitationDistribution;
                obj["vulnerabilityExposesScopeDistribution"] = node.vulnerabilityExposesScopeDistribution;
                obj["vulnerabilityInteractionRequiredDistribution"] = node.vulnerabilityInteractionRequiredDistribution;
                obj["vulnerabilityPrivilegesRequiredDistribution"] = node.vulnerabilityPrivilegesRequiredDistribution;
                obj["vulnerabilityExposureDistribution"] = node.vulnerabilityExposureDistribution;
                obj["vulnerabilityImpactToConfidentialityDistribution"] = node.vulnerabilityImpactToConfidentialityDistribution;
                obj["vulnerabilityImpactToIntegrityDistribution"] = node.vulnerabilityImpactToIntegrityDistribution;
                obj["vulnerabilityImpactToAvailabilityDistribution"] = node.vulnerabilityImpactToAvailabilityDistribution;
                obj["vulnerabilityImpactToAccountabilityDistribution"] = node.vulnerabilityImpactToAccountabilityDistribution;
                obj["attackComplexityDistribution"] = node.attackComplexityDistribution;
                obj["attackProliferationDistribution"] = node.attackProliferationDistribution;
                obj["attackImpactToConfidentialityDistribution"] = node.attackImpactToConfidentialityDistribution;
                obj["attackImpactToIntegrityDistribution"] = node.attackImpactToIntegrityDistribution;
                obj["attackImpactToAvailabilityDistribution"] = node.attackImpactToAvailabilityDistribution;
                obj["attackImpactToAccountabilityDistribution"] = node.attackImpactToAccountabilityDistribution;
                obj["assetConfidentialityDistribution"] = node.assetConfidentialityDistribution;
                obj["assetIntegrityDistribution"] = node.assetIntegrityDistribution;
                obj["assetAvailabilityDistribution"] = node.assetAvailabilityDistribution;
                obj["assetAccountabilityDistribution"] = node.assetAccountabilityDistribution;
                obj["assetPrivacyImpactConfidentialityDistribution"] = node.assetPrivacyImpactConfidentialityDistribution;
                obj["assetLegalImpactConfidentialityDistribution"] = node.assetLegalImpactConfidentialityDistribution;
                obj["assetRegulatoryImpactConfidentialityDistribution"] = node.assetRegulatoryImpactConfidentialityDistribution;
                obj["assetReputationalImpactConfidentialityDistribution"] = node.assetReputationalImpactConfidentialityDistribution;
                obj["assetFinancialImpactConfidentialityDistribution"] = node.assetFinancialImpactConfidentialityDistribution;
                obj["assetPrivacyImpactIntegrityDistribution"] = node.assetPrivacyImpactIntegrityDistribution;
                obj["assetLegalImpactIntegrityDistribution"] = node.assetLegalImpactIntegrityDistribution;
                obj["assetReputationalImpactIntegrityDistribution"] = node.assetReputationalImpactIntegrityDistribution;
                obj["assetRegulatoryImpactIntegrityDistribution"] = node.assetRegulatoryImpactIntegrityDistribution;
                obj["assetFinancialImpactIntegrityDistribution"] = node.assetFinancialImpactIntegrityDistribution;
                obj["assetPrvacyImpactAvailabilityDistribution"] = node.assetPrivacyImpactAvailabilityDistribution;
                obj["assetLegalImpactAvailabilityDistribution"] = node.assetLegalImpactAvailabilityDistribution;
                obj["assetRegulatoryImpactAvailabilityDistribution"] = node.assetRegulatoryImpactAvailabilityDistribution;
                obj["assetReputationalImpactAvailabilityDistribution"] = node.assetReputationalImpactAvailabilityDistribution;
                obj["assetFinancialImpactAvailabilityDistribution"] = node.assetFinancialImpactAvailabilityDistribution;
                obj["assetPrivacyImpactAccountabilityDistribution"] = node.assetPrivacyImpactAccountabilityDistribution;
                obj["assetLegalImpactAccountabilityDistribution"] = node.assetLegalImpactAccountabilityDistribution;
                obj["assetRegulatoryImpactAccountabilityDistribution"] = node.assetRegulatoryImpactAccountabilityDistribution;
                obj["assetReputationalImpactAccountabilityDistribution"] = node.assetReputationalImpactAccountabilityDistribution;
                obj["assetFinancialImpactAccountabilityDistribution"] = node.assetFinancialImpactAccountabilityDistribution;
                obj["assetConfidentialityMitigatedScore"] = node.assetConfidentialityMitigatedScore;
                obj["assetIntegrityMitigatedScore"] = node.assetIntegrityMitigatedScore;
                obj["assetAvailabilityMitigatedScore"] = node.assetAvailabilityMitigatedScore;
                obj["assetAccountabilityMitigatedScore"] = node.assetAccountabilityMitigatedScore;

                obj["tags"] = node.MetaTags;

                response = InvokeApiEndpoint.CreateOrUpdateAPI(verb, AuthAPI._account_token, requestUrl, httpTimeOutInSec, obj);
                ret_obj = JObject.Parse(response.Content.ReadAsStringAsync().Result);
                
            }
            catch (Exception ex)
            {
              
                return null;
            }

            return ret_obj;
        }

        public static JObject DeleteNodeMeta(string nodeGUID)
        {
            Graph.Utility.SaveAuditLog("DeleteNodeMeta", "+++FUNCTION ENTERED+++", "", "", $"");
            JObject result = new JObject();

            HttpResponseMessage response = null;
            try
            {
                 HttpMethod verb = HttpMethod.Delete;
                string sub_url = "/netgraph/v1.0/nodes/" + nodeGUID + "/meta?enterpriseGUID=" 
                    + AuthAPI._enterprise_guid + "&userGUID=" + AuthAPI._user_guid 
                    + "&subscription-key=" + ApplicationSettings.ApiSubscriptionKey;
                string requestUrl = ApplicationSettings.ApiRootURL + sub_url;
                int httpTimeOutInSec = 60;

                response = InvokeApiEndpoint.GetOrDeleteAPI(verb, AuthAPI._account_token, requestUrl, httpTimeOutInSec);
                if (response == null) return null;
                result["status"] = true;
                result["nodeGUID"] = nodeGUID;
              
            }
            catch (Exception ex)
            {
             
                return result;
            }

            return result;
        }

        public static string GetFilterTypeFromString(string filters)
        {
            int filterType = filters == "" ? 63 : 0;
            string[] filterArr = filters.Split(',');
            foreach(var filter in filterArr)
            {
                switch (filter.ToLower())
                {
                    case "actor":
                        filterType += 1;
                        break;
                    case "asset":
                        filterType += 2;
                        break;
                    case "attack":
                        filterType += 4;
                        break;
                    case "control":
                        filterType += 8;
                        break;
                    case "group":
                        filterType += 16;
                        break;
                    case "objective":
                        filterType += 32;
                        break;
                    //case "evidence":
                    //    filterType += 64;
                    //    break;
                }
            }
            return filterType.ToString();
        }

        public static string GetTitleFromString(string titles)
        {
            int titleData = 0;
            string[] titleArr = titles.Split(',');
            foreach (var tmp in titleArr)
            {
                switch (tmp.ToLower())
                {
                    case "title":
                        titleData += 1;
                        break;
                    case "ref":
                        titleData += 2;
                        break;
                    case "description":
                    case "desc":
                        titleData += 4;
                        break;
                    case "framework":
                        titleData += 8;
                        break;
                    case "notes":
                        titleData += 16;
                        break;
                }
            }
            return titleData.ToString();
        }

        public static JArray GetRepoNodeList(string searchIn = "Title,Reference,Description,Framework,Notes", string filterByType = "", string searchText = "*")
        {
            Graph.Utility.SaveAuditLog("GetRepoNodeList", "+++FUNCTION ENTERED+++", "", "", $"");
            JArray arr = new JArray();
            HttpResponseMessage response = null;
            try
            {
                Graph.Utility.SaveAuditLog("GetRepoNodeList", "Get", "", "", $"");
                HttpMethod verb = HttpMethod.Get;
                string sub_url = "/netgraph/v1.0/nodes/search?tenantGUID=" + AuthAPI._tenant_guid + "&userGUID=" + AuthAPI._user_guid + "&subscription-key=" + ApplicationSettings.ApiSubscriptionKey;
                sub_url = sub_url + "&searchIn=" + GetTitleFromString(searchIn);
                sub_url = sub_url + "&filterByType=" + GetFilterTypeFromString(filterByType);
                sub_url = sub_url + "&searchtext=" + searchText;
                sub_url = sub_url + "&resultLimit=100";
                
                string requestUrl = ApplicationSettings.ApiRootURL + sub_url;
                int httpTimeOutInSec = 60;

                response = InvokeApiEndpoint.GetOrDeleteAPI(verb, AuthAPI._account_token, requestUrl, httpTimeOutInSec);
                if (response == null) return arr;
                string statusCode = response.StatusCode.ToString();
                if (statusCode.ToLower() == "notfound"  && statusCode.ToLower() == "internalservererror")
                {
                    return arr;
                }
                arr = JArray.Parse(response.Content.ReadAsStringAsync().Result);
            }
            catch (Exception ex)
            {
               
                return arr;
            }

            return arr;
        }

        public static JObject GetRepoNodeMeta(string node_id)
        {
            Graph.Utility.SaveAuditLog("GetRepoNodeMeta", "+++FUNCTION ENTERED+++", "", "", $"");
            JObject ret_obj = new JObject();
            HttpResponseMessage response = null;
            try
            {
                HttpMethod verb = HttpMethod.Get;
                string sub_url = "/netgraph/v1.0/nodes/" + node_id + "/meta?enterpriseGUID=" + AuthAPI._enterprise_guid + "&userGUID=" + AuthAPI._user_guid + "&subscription-key=" + ApplicationSettings.ApiSubscriptionKey;
                string requestUrl = ApplicationSettings.ApiRootURL + sub_url;
                int httpTimeOutInSec = 60;

                response = InvokeApiEndpoint.GetOrDeleteAPI(verb, AuthAPI._account_token, requestUrl, httpTimeOutInSec);
                if (response == null) return null;
                string statusCode = response.StatusCode.ToString();
                ret_obj = JObject.Parse(response.Content.ReadAsStringAsync().Result);
            }
            catch (Exception ex)
            {
               
                return null;
            }

            return ret_obj;
        }

        public static JObject GetRepoNodeJSON(string nodeGUID)
        {
            Graph.Utility.SaveAuditLog("GetRepoNodeJSON", "+++FUNCTION ENTERED+++", "", "", $"");
            JObject ret_obj = new JObject();
            HttpResponseMessage response = null;
            try
            {
                HttpMethod verb = HttpMethod.Get;
                string sub_url = "/netgraph/v1.0/nodes/" + nodeGUID + "/json?userGUID=" + AuthAPI._user_guid + "&tenantGUID=" + AuthAPI._tenant_guid + "&subscription-key=" + ApplicationSettings.ApiSubscriptionKey;
                string requestUrl = ApplicationSettings.ApiRootURL + sub_url;
                int httpTimeOutInSec = 60;

                response = InvokeApiEndpoint.GetOrDeleteAPI(verb, AuthAPI._account_token, requestUrl, httpTimeOutInSec);
                if (response == null) return null;
                string statusCode = response.StatusCode.ToString();
                ret_obj = JObject.Parse(response.Content.ReadAsStringAsync().Result);
            }
            catch (Exception ex)
            {

                return null;
            }

            return ret_obj;
        }
        public static JObject GetRepoNodeGraph(string node_id)
        {
            Graph.Utility.SaveAuditLog("GetRepoNodeGraph", "+++FUNCTION ENTERED+++", "", "", $"");
            JObject ret_obj = new JObject();
            HttpResponseMessage response = null;
            try
            {
                HttpMethod verb = HttpMethod.Get;
                string sub_url = "/netgraph/v1.0/nodes/" + node_id + "/graph?userGUID=" + AuthAPI._user_guid + "&subscription-key=" + ApplicationSettings.ApiSubscriptionKey;
                string requestUrl = ApplicationSettings.ApiRootURL + sub_url;
                int httpTimeOutInSec = 60;

                response = InvokeApiEndpoint.GetOrDeleteAPI(verb, AuthAPI._account_token, requestUrl, httpTimeOutInSec);
                if (response == null) return ret_obj;
                string statusCode = response.StatusCode.ToString();
                ret_obj = JObject.Parse(response.Content.ReadAsStringAsync().Result);
            }
            catch (Exception ex)
            {
               
                return null;
            }

            return ret_obj;
        }

        public static JObject GetRepoNodeVisual(string node_id)
        {
            Graph.Utility.SaveAuditLog("GetRepoNodeVisual", "+++FUNCTION ENTERED+++", "", "", $"");
            JObject ret_obj = new JObject();
            HttpResponseMessage response = null;
            try
            {
                HttpMethod verb = HttpMethod.Get;
                string sub_url = "/netgraph/v1.0/nodes/" + node_id + "/visual?userGUID=" + AuthAPI._user_guid + "&subscription-key=" + ApplicationSettings.ApiSubscriptionKey;
                string requestUrl = ApplicationSettings.ApiRootURL + sub_url;
                int httpTimeOutInSec = 60;

                response = InvokeApiEndpoint.GetOrDeleteAPI(verb, AuthAPI._account_token, requestUrl, httpTimeOutInSec);
                if (response == null) return ret_obj;
                string statusCode = response.StatusCode.ToString();
                ret_obj = JObject.Parse(response.Content.ReadAsStringAsync().Result);
            }
            catch (Exception ex)
            {
               
                return null;
            }

            return ret_obj;
        }

        public static JObject GetRepoNodeNote(string node_id)
        {
            Graph.Utility.SaveAuditLog("GetRepoNodeNote", "+++FUNCTION ENTERED+++", "", "", $"");
            JObject ret_obj = new JObject();
            HttpResponseMessage response = null;
            try
            {
                HttpMethod verb = HttpMethod.Get;
                string sub_url = "/netgraph/v1.0/nodes/" + node_id + "/note?userGUID=" + AuthAPI._user_guid + "&subscription-key=" + ApplicationSettings.ApiSubscriptionKey;
                string requestUrl = ApplicationSettings.ApiRootURL + sub_url;
                int httpTimeOutInSec = 60;

                response = InvokeApiEndpoint.GetOrDeleteAPI(verb, AuthAPI._account_token, requestUrl, httpTimeOutInSec);
                if (response == null) return ret_obj;
                string statusCode = response.StatusCode.ToString();
                ret_obj = JObject.Parse(response.Content.ReadAsStringAsync().Result);
            }
            catch (Exception ex)
            {
               
                return null;
            }

            return ret_obj;
        }

        public static JObject GetRepoNodeFramework(string node_id)
        {
            Graph.Utility.SaveAuditLog("GetRepoNodeFramework", "+++FUNCTION ENTERED+++", "", "", $"");
            JObject ret_obj = new JObject();
            HttpResponseMessage response = null;
            try
            {
                HttpMethod verb = HttpMethod.Get;
                string sub_url = "/netgraph/v1.0/nodes/" + node_id + "/framework?userGUID=" + AuthAPI._user_guid + "&subscription-key=" + ApplicationSettings.ApiSubscriptionKey;
                string requestUrl = ApplicationSettings.ApiRootURL + sub_url;
                int httpTimeOutInSec = 60;

                response = InvokeApiEndpoint.GetOrDeleteAPI(verb, AuthAPI._account_token, requestUrl, httpTimeOutInSec);
                if (response == null) return ret_obj;
                string statusCode = response.StatusCode.ToString();
                ret_obj = JObject.Parse(response.Content.ReadAsStringAsync().Result);
            }
            catch (Exception ex)
            {
                return null;
            }

            return ret_obj;
        }

        public static string UpdateNodeToServer(Node node)
        {
            Graph.Utility.SaveAuditLog("UpdateNodeToServer", "+++FUNCTION ENTERED+++", "", "", $"");
            JObject ret_obj = PostRepoNodeMeta(node.Title, node.Type.Name, node.Category, node.SubCategory);
            string guid = "";
            if (ret_obj == null) return null;

            guid = ret_obj["nodeGUID"].ToString();

            ret_obj = PutRepoNodeFramework(guid, node);
            if (ret_obj == null)
            {
                DeleteNodeMeta(guid);
                return null;
            }
            ret_obj = PutRepoNodeNote(guid, node);
            if (ret_obj == null)
            {
                DeleteNodeMeta(guid);
                return null;
            }
            ret_obj = PutRepoNodeVisual(guid, node);
            if (ret_obj == null)
            {
                DeleteNodeMeta(guid);
                return null;
            }
            ret_obj = PutRepoNodeGraph(guid, node);
            if (ret_obj == null)
            {
                DeleteNodeMeta(guid);
                return null;
            }
            return guid;
        }

        public static string AddNodeToServer(Node node)
        {
            Graph.Utility.SaveAuditLog("AddNodeToServer", "+++FUNCTION ENTERED+++", "", "", $"");
            JObject ret_obj = PostRepoNodeMeta(node.Title, node.Type.Name, node.Category, node.SubCategory);
            string guid = "";
            if (ret_obj == null) return null;

            guid = ret_obj["nodeGUID"].ToString();

            ret_obj = PutRepoNodeFramework(guid, node);
            if (ret_obj == null)
            {
                DeleteNodeMeta(guid);
                return null;
            }
            ret_obj = PutRepoNodeNote(guid, node);
            if (ret_obj == null)
            {
                DeleteNodeMeta(guid);
                return null;
            }
            ret_obj = PutRepoNodeVisual(guid, node);
            if (ret_obj == null)
            {
                DeleteNodeMeta(guid);
                return null;
            }
            ret_obj = PutRepoNodeGraph(guid, node);
            if (ret_obj == null)
            {
                DeleteNodeMeta(guid);
                return null;
            }
            return guid;
        }

        public static JObject GetNodeBasicInfo(string nodeGUID)
        {
            Graph.Utility.SaveAuditLog("GetRepoNodeBasicInfo", "+++FUNCTION ENTERED+++", "", "", $"");
            JObject ret_obj = new JObject();
            HttpResponseMessage response = null;
            try
            {
                HttpMethod verb = HttpMethod.Get;
                string sub_url = "/netgraph/v1.0/nodes?nodeGUID=" + nodeGUID + "&userGUID=" + AuthAPI._user_guid + "&subscription-key=" + ApplicationSettings.ApiSubscriptionKey;
                string requestUrl = ApplicationSettings.ApiRootURL + sub_url;
                int httpTimeOutInSec = 60;

                response = InvokeApiEndpoint.GetOrDeleteAPI(verb, AuthAPI._account_token, requestUrl, httpTimeOutInSec);
                if (response == null) return ret_obj;
                string statusCode = response.StatusCode.ToString();
                ret_obj = JObject.Parse(response.Content.ReadAsStringAsync().Result);
            }
            catch (Exception ex)
            {
                return null;
            }

            return ret_obj;
        }

        public static JObject GetNodeData(string nodeGUID)
        {
            JObject ret_obj = new JObject();
            ret_obj["meta"] = NodeAPI.GetRepoNodeMeta(nodeGUID);
            ret_obj["node_graph"] = NodeAPI.GetRepoNodeGraph(nodeGUID);
            ret_obj["node_visual"] = NodeAPI.GetRepoNodeVisual(nodeGUID);
            ret_obj["node_note"] = NodeAPI.GetRepoNodeNote(nodeGUID);
            ret_obj["node_framework"] = NodeAPI.GetRepoNodeFramework(nodeGUID);

            return ret_obj;
        }
    }
}
