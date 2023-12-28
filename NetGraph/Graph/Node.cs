using CyConex.Helpers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Syncfusion.Windows.Forms.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Configuration;
using System.Drawing.Design;
using System.Globalization;
using System.Linq;
using System.Reflection;

namespace CyConex.Graph
{
    public class Node
    {

        [StringValidator(MinLength = 0, MaxLength = 250)]
        public string Title { get; set; }

        public double TitleSize { get; set; }

    
        [JsonIgnore]
        public System.Drawing.Color TitleTextColor { get; set; }

     
        [StringValidator(MinLength = 0, MaxLength = 2000)]
        public string description { get; set; }

        public string Note { get; set; }

        public string frameworkReference { get; set; }
    
        public string ControlFrameworkVersion { get; set; }

        public string frameworkName { get; set; }

        [StringValidator(MinLength = 0, MaxLength = 250)]
        public string Category { get; set; }

        [StringValidator(MinLength = 0, MaxLength = 250)]
        public string Domain { get; set; }

        [StringValidator(MinLength = 0, MaxLength = 250)]
        public string SubDomain { get; set; }
        [StringValidator(MinLength = 0, MaxLength = 250)]
        
        public string ReferenceURL { get; set; }

        [StringValidator(MinLength = 0, MaxLength = 250)]
        
        public string Level { get; set; }
        public double controlBaseScore { get; set; }
        public double controlBaseValue { get; set; }
        public double controlBaseMinValue { get; set; }

        [StringValidator(MinLength = 0, MaxLength = 250)]
        public string controlBaseScoreAssessmentStatus { get; set; }

        [StringValidator(MinLength = 0, MaxLength = 250)]
        public string AssessedStatus { get; set; }

        [StringValidator(MinLength = 0, MaxLength = 250)]
        public string ImplementedStrength { get; set; }

        [StringValidator(MinLength = 0, MaxLength = 250)]
        public string ImagePath { get; set; }

        [StringValidator(MinLength = 0, MaxLength = 250)]
        public string BorderWidth { get; set; }

        [StringValidator(MinLength = 0, MaxLength = 250)]
        public string objectiveTargetType { get; set; }
        public string objectiveTargetValue { get; set; }
        public string objectiveAcheivedValue { get; set; }

        [StringValidator(MinLength = 0, MaxLength = 250)]
        public string InherentStrengthValue { get; set; }

        [StringValidator(MinLength = 0, MaxLength = 250)]
        public string ImplementedStrengthValue { get; set; }

        public string NodeImageData { get; set; }

        [StringValidator(MinLength = 0, MaxLength = 250)]
        public string NodeTitlePosition { get; set; }

        [StringValidator(MinLength = 0, MaxLength = 5000)]
        public string MetaTags { get; set; }

        public bool Enabled { get; set; }

        public double HTMLOpacity { get; set; }

        public NodeShape Shape { get; set; }

        public System.Drawing.Color Color { get; set; }

        public System.Drawing.Color BorderColor { get; set; }

        public System.Drawing.Color origin_color { get; set; }

        public double Size { get; set; }

        public double Height { get; set; }

        public double Width { get; set; }

        public string ID { get; set; }

        public string masterID { get; set; }

    
        public NodeType Type { get; set; }

        public string PrimaryCategory { get; set; }

        public string SubCategory { get; set; }

        public double controlAssessedValue { get; set; }
        public double controlAssessedScore { get; set; }
        public double controlAssessedMinValue { get; set; }
       
        public double actorCapabilityValue { get; set; }

        public string actorCapabilityText { get; set; }

      
        public double actorResourcesValue { get; set; }

        public string actorResourcesText { get; set; }

       
        public double actorMotivationValue { get; set; }

        public string actorMotivationText { get; set; }

      
        public double actorAccessValue { get; set; }

        public string actorAccessText { get; set; }

        public double actorImpactToConfidentialityValue { get; set; }
        public double actorImpactToConfidentialityMinValue { get; set; }
        public string actorImpactToConfidentialityText { get; set; }

        public double actorImpactToIntegrityValue { get; set; }
        public double actorImpactToIntegrityMinValue { get; set; }
        public string actorImpactToIntegrityText { get; set; }
        public double actorImpactToAvailabilityValue { get; set; }
        public double actorImpactToAvailabilityMinValue { get; set; }
        public string actorImpactToAvailabilityText { get; set; }
        public double actorImpactToAccountabilityValue { get; set; }
        public double actorimpactToAccountabilityMinValue { get; set; }

        public string actorImpactToAccountabilityText { get; set; }
        public double actorAccessMinValue { get; set; }
        public double actorCapabilityMinValue { get; set; }
        public double actorMotivationMinValue { get; set; }
        public double actorResourcesMinValue { get; set; }
        public double attackComplexityMinValue { get; set; }
        public double attackProliferationMinValue { get; set; }
        public double vulnerabilityEaseOfExploitationMinValue { get; set; }
        public double vulnerabilityExposureMinValue { get; set; }
        public double vulnerabilityPrivilegesRequiredMinValue { get; set; }
        public double vulnerabilityInteractionRequiredMinValue { get; set; }

        [FormattedDoubleFormatString("N2")]
        public double actorScore { get; set; }
        public double actorMitigatedScore { get; set; }
        

        [FormattedDoubleFormatString("N2")]
        public double attackComplexityValue { get; set; }

        public string attackComplexityText { get; set; }

       
        [FormattedDoubleFormatString("N2")]
        public double attackProliferationValue { get; set; }

        public string attackProliferationText { get; set; }

        [FormattedDoubleFormatString("N2")]
        public double attackImpactToConfidentialityValue { get; set; }
        public double attackImpactToConfidentialityMinValue { get; set; }

        public string attackImpactToConfidentialityText { get; set; }

       
        [FormattedDoubleFormatString("N2")]
        public double attackImpactToIntegrityValue { get; set; }
        public double attackImpactToIntegrityMinValue { get; set; }

        public string attackImpactToIntegrityText { get; set; }

        
        [FormattedDoubleFormatString("N2")]
        public double attackImpactToAvailabilityValue { get; set; }
        public double attackImpactToAvailabilityMinValue { get; set; }

        public string attackImpactToAvailabilityText { get; set; }

      
        [FormattedDoubleFormatString("N2")]
        public double attackImpactToAccountabilityValue { get; set; }
        public double attackImpactToAccountabilityMinValue { get; set; }

        public string attackImpactToAccountabilityText { get; set; }

       
        [FormattedDoubleFormatString("N2")]
        public double attackScore { get; set; }

       
        [FormattedDoubleFormatString("N2")]
        public double assetConfidentialityValue { get; set; }
        public double assetConfidentialityMinValue { get; set; }
        public double assetConfidentialityProbabilityValue { get; set; }
        public string assetConfidentialityText { get; set; }

       
        [FormattedDoubleFormatString("N2")]
        public double assetIntegrityValue { get; set; }
        public double assetIntegrityMinValue { get; set; }
        public double assetIntegrityProbabilityValue { get; set; }


        public string assetIntegrityText { get; set; }

      
        [FormattedDoubleFormatString("N2")]
        public double assetAvailabilityValue { get; set; }
        public double assetAvailabilityMinValue { get; set; }
        public double assetAvailabilityProbabilityValue { get; set; }

        public string assetAvailabilityText { get; set; }

        [FormattedDoubleFormatString("N2")]
        public double assetAccountabilityValue { get; set; }
        public double assetAccountabilityMinValue { get; set; }
        public double assetAccountabilityProbabilityValue { get; set; }

        public string assetAccountabilityText { get; set; }

        public string assetFinancialImpactText { get; set; }
        public double assetFinancialImpactValue { get; set; }
        public double assetFinancialImpactMinValue { get; set; }

        public string assetRegulatoryImpactText { get; set; }
        
        public double assetRegulatoryImpactValue { get; set; }
        public double assetRegulatoryImpactMinValue { get; set; }
        public string assetReputationalImpactText { get; set; } 
        public double assetReputationalImpactValue { get; set; }
        public double assetReputationalImpactMinValue { get; set; }

        public string assetLegalImpactText { get; set; }
        public double assetLegalImpactValue { get; set; }
        public double assetLegalImpactMinValue { get; set; }

        public string assetPrivacyImpactText { get; set; }
        public double assetPrivacyImpactValue { get; set; }
        public double assetPrivacyImpactMinValue { get; set; }


        [FormattedDoubleFormatString("N2")]
        public double assetScore { get; set; }

       
        [FormattedDoubleFormatString("N2")]
        public double riskScore { get; set; }

        public double threatScore { get; set; }

        public double assetLikelihoodScore { get; set; }

        public string vulnerabilityEaseOfExploitationText { get; set; }

    
        public string vulnerabilityExposureText { get; set; }

     
        public string vulnerabilityImpactToConfidentialityText { get; set; }

        public string vulnerabilityImpactToIntegrityText { get; set; }

      
        public string vulnerabilityImpactToAccountabilityText { get; set; }

       
        public string vulnerabilityImpactToAvailabilityText { get; set; }

        public double vulnerabilityEaseOfExploitationValue { get; set; }

        
        public double vulnerabilityExposureValue { get; set; }

        public double vulnerabilityMitigatedScore { get; set; }


        [FormattedDoubleFormatString("N2")]
        public double vulnerabilityImpactToConfidentialityValue { get; set; }
        public double vulnerabilityImpactToConfidentialityMinValue { get; set; }

        [TypeConverter(typeof(FormattedDoubleConverter))]
        [FormattedDoubleFormatString("N2")]
        public double vulnerabilityImpactToIntegrityValue { get; set; }
        public double vulnerabilityImpactToIntegrityMinValue { get; set; }

        [TypeConverter(typeof(FormattedDoubleConverter))]
        [FormattedDoubleFormatString("N2")]
        public double vulnerabilityImpactToAccountabilityValue { get; set; }
        public double vulnerabilityImpactToAccountabilityMinValue { get; set; }
     
        [TypeConverter(typeof(FormattedDoubleConverter))]
        [FormattedDoubleFormatString("N2")]
        public double vulnerabilityImpactToAvailabilityValue { get; set; }
        public double vulnerabilityImpactToAvailabilityMinValue { get; set; }

        public string vulnerabilityPrivilegeText { get; set; }
        public double vulnerabilityPrivilegesRequiredValue { get; set; }

        public string vulnerabilityExposesText { get; set; }
        public double vulnerabilityExposesScopeValue { get; set; }
        public double vulnerabilityExposesScopeMinValue { get; set; }

        public string vulnerabilityInteractiveText { get; set; }
        public double vulnerabilityInteractionRequiredValue { get; set; }

        public double evidenceConfidenceValue { get; set; }
      
        public double impactScore { get; set; }
       
        public double CalculatedValue { get; set; }
        public string nodeBehaviour { get; set; }
        public double assetRegulatoryImpactAvailabilityValue { get; set; }
        public double assetReputationalImpactAvailabilityValue { get; set; }
        public double assetFinancialImpactAvailabilityValue { get; set; }
        public double assetPrivacyImpactAccountabilityValue { get; set; }
        public double assetLegalImpactAccountabilityValue { get; set; }
        public double assetRegulatoryImpactAccountabilityValue { get; set; }
        public double assetReputationalImpactAccountabilityValue { get; set; }
        public double assetPrivacyImpactConfidentialityValue { get; set; }
        public double assetLegalImpactConfidentialityValue { get; set; }
        public double assetReputationalImpactConfidentialityValue { get; set; }
        public double assetFinancialImpactConfidentialityValue { get; set; }
        public double assetPrivacyImpactIntegrityValue { get; set; }
        public double assetLegalImpactIntegrityValue { get; set; }
        public double assetRegulatoryImpactIntegrityValue { get; set; }
        public double assetFinancialImpactIntegrityValue { get; set; }
        public double assetLegalImpactAvailabilityValue { get; set; }
        public double assetRegulatoryImpactConfidentiality { get; set; }
        public double assetReputationalImpactIntegrityValue { get; set; }
        public double assetPrivacyImpactConfidentialityMinValue { get; set; }
        public double assetLegalImpactConfidentialityMinValue { get; set; }
        public double assetRegulatoryImpactConfidentialityValue { get; set; }
        public double assetRegulatoryImpactConfidentialityMinValue { get; set; }
        public double assetReputationalImpactConfidentialityMinValue { get; set; }
        public double assetFinancialImpactConfidentialityMinValue { get; set; }
        public double assetPrivacyImpactIntegrityMinValue { get; set; }
        public double assetLegalImpactIntegrityMinValue { get; set; }
        public double assetRegulatoryImpactIntegrityMinValue { get; set; }
        public double assetReputationalImpactIntegrityMinValue { get; set; }
        public double assetFinancialImpactIntegrityMinValue { get; set; }
        public double assetPrivacyImpactAvailabilityMinValue { get; set; }
        public double assetLegalImpactAvailabilityMinValue { get; set; }
        public double assetRegulatoryImpactAvailabilityMinValue { get; set; }
        public double assetReputationalImpactAvailabilityMinValue { get; set; }
        public double assetFinancialImpactAvailabilityMinValue { get; set; }
        public double assetPrivacyImpactAccountabilityMinValue { get; set; }
        public double assetLegalImpactAccountabilityMinValue { get; set; }
        public double assetRegulatoryImpactAccountabilityMinValue { get; set; }
        public double assetReputationalImpactAccountabilityMinValue { get; set; }
        public double assetPrivacyImpactAvailabilityValue { get; set; }
        public double assetFinancialImpactAccountabilityValue { get; set; }
        public double assetFinancialImpactAccountabilityMinValue { get; set; }
        public string controlBaseDistribution { get; set; }
        public string controlAssessedDistribution { get; set; }
        public string actorAccessDistribution { get; set; }
        public string actorCapabilityDistribution { get; set; }
        public string actorResourcesDistribution { get; set; }
        public string actorMotivationDistribution { get; set; }
        public string actorImpactToConfidentialityDistribution { get; set; }
        public string actorImpactToIntegrityDistribution { get; set; }
        public string actorImpactToAvailabilityDistribution { get; set; }
        public string actorImpactToAccountabilityDistribution { get; set; }
        public string vulnerabilityEaseOfExploitationDistribution { get; set; }
        public string vulnerabilityExposesScopeDistribution { get; set; }
        public string vulnerabilityInteractionRequiredDistribution { get; set; }
        public string vulnerabilityPrivilegesRequiredDistribution { get; set; }
        public string vulnerabilityExposureDistribution { get; set; }
        public string vulnerabilityImpactToConfidentialityDistribution { get; set; }
        public string vulnerabilityImpactToIntegrityDistribution { get; set; }
        public string vulnerabilityImpactToAvailabilityDistribution { get; set; }
        public string vulnerabilityImpactToAccountabilityDistribution { get; set; }
        public string attackComplexityDistribution { get; set; }
        public string attackProliferationDistribution { get; set; }
        public string attackImpactToConfidentialityDistribution { get; set; }
        public string attackImpactToIntegrityDistribution { get; set; }
        public string attackImpactToAvailabilityDistribution { get; set; }
        public string attackImpactToAccountabilityDistribution { get; set; }
        public string assetConfidentialityDistribution { get; set; }
        public string assetIntegrityDistribution { get; set; }
        public string assetAvailabilityDistribution { get; set; }
        public string assetAccountabilityDistribution { get; set; }
        public string assetPrivacyImpactConfidentialityDistribution { get; set; }
        public string assetLegalImpactConfidentialityDistribution { get; set; }
        public string assetRegulatoryImpactConfidentialityDistribution { get; set; }
        public string assetReputationalImpactConfidentialityDistribution { get; set; }
        public string assetFinancialImpactConfidentialityDistribution { get; set; }
        public string assetPrivacyImpactIntegrityDistribution { get; set; }
        public string assetLegalImpactIntegrityDistribution { get; set; }
        public string assetReputationalImpactIntegrityDistribution { get; set; }
        public string assetRegulatoryImpactIntegrityDistribution { get; set; }
        public string assetFinancialImpactIntegrityDistribution { get; set; }
        public string assetPrivacyImpactAvailabilityDistribution { get; set; }
        public string assetLegalImpactAvailabilityDistribution { get; set; }
        public string assetRegulatoryImpactAvailabilityDistribution { get; set; }
        public string assetReputationalImpactAvailabilityDistribution { get; set; }
        public string assetFinancialImpactAvailabilityDistribution { get; set; }
        public string assetPrivacyImpactAccountabilityDistribution { get; set; }
        public string assetLegalImpactAccountabilityDistribution { get; set; }
        public string assetRegulatoryImpactAccountabilityDistribution { get; set; }
        public string assetReputationalImpactAccountabilityDistribution { get; set; }
        public string assetFinancialImpactAccountabilityDistribution { get; set; }
        public double assetConfidentialityMitigatedScore { get; set; }
        public double assetIntegrityMitigatedScore { get; set; }
        public double assetAvailabilityMitigatedScore { get; set; }
        public double assetAccountabilityMitigatedScore { get; set; }

        public string Parent { get; set; }

        public Location Location { get; set; }

        public List<string> ConnectedNodes { get; set; }

        public override string ToString()
        {
            return Title;
        }

        /// <summary>
        /// Default parameterless constructor (required for YAXLib serialization)
        /// </summary>
        public Node() : this("")
        {
            Location = new Location(0, 0);
            Size = 40;
            TitleSize = 15;
            Type = NodeTypes.NodesTypes.FirstOrDefault(item => item.Name == "Control");
            Enabled = true;
            ConnectedNodes = new List<string>();
        }

        public Node(string text = "")
        {
            Title = text;
            Location = new Location(0, 0);
            Size = 40;
            TitleSize = 15;
            Type = NodeTypes.NodesTypes.FirstOrDefault(item => item.Name == "Control");
            Enabled = true;
            ConnectedNodes = new List<string>();
        }

        public Node(string id, string text = "")
        {
            ID = id;
            Title = text;
            Location = new Location(0, 0);
            Size = 40;
            TitleSize = 15;
            Type = NodeTypes.NodesTypes.FirstOrDefault(item => item.Name == "Control");
            Enabled = true;
            ConnectedNodes = new List<string>();
        }

        public Node Clone()
        {
            return (Node)this.MemberwiseClone();
        }

        public static Node FromJson(string json)
        {
            
            if (json == null)
                return null;

            Console.WriteLine("Node.cs > FromJson");
            var nodeJson = JObject.Parse(json);
            var nodeData = nodeJson["data"];
            if (nodeData == null)
            {
                nodeData = nodeJson;
            }
            //Try to handel missing data from old graphs 
            if (nodeData["id"] == null)
            {
                nodeData["id"] = "0";
            }

            if (nodeData["masterID"] == null)
            {
                nodeData["masterID"] = "";
            }

            if (nodeData["title"] == null)
            {
                nodeData["title"] = "Unknown";
            }

            if (nodeData["description"] == null)
            {
                nodeData["description"] = "";
            }

            if (nodeData["note"] == null)
            {
                nodeData["note"] = "";
            }

            if (nodeData["domain"] == null)
            {
                nodeData["domain"] = "";
            }

            if (nodeData["subdomain"] == null)
            {
                nodeData["subdomain"] = "";
            }

            if (nodeData["refurl"] == null)
            {
                nodeData["refurl"] = "";
            }

            if (nodeData["frameworkReference"] == null)
            {
                nodeData["frameworkReference"] = "";
            }

            if (nodeData["frameworkNameVersion"] == null)
            {
                nodeData["frameworkNameVersion"] = "";
            }

            if (nodeData["frameworkName"] == null)
            {
                nodeData["frameworkName"] = "";
            }

            if (nodeData["category"] == null)
            {
                nodeData["category"] = "";
            }

            if (nodeData["level"] == null)
            {
                nodeData["level"] = "";
            }

            if (nodeData["enabled"] == null)
            {
                nodeData["enabled"] = false;
            }

            if (nodeData["color"] == null)
            {
                nodeData["color"] = "";
            }

            if (nodeData["opacity"] == null)
            {
                nodeData["opacity"] = "100";
            }

            if (nodeData["titleTextColor"] == null)
            {
                nodeData["titleTextColor"] = "";
            }

            if (nodeData["shape"] == null)
            {
                nodeData["shape"] = "elipse";
            }

            if (nodeData["height"] == null)
            {
                nodeData["height"] = "48";
            }

            if (nodeData["width"] == null)
            {
                nodeData["width"] = "48";
            }

            if (nodeData["nodeType"] == null)
            {
                nodeData["nodeType"] = "control";
            }

            if (nodeData["primaryCategory"] == null)
            {
                nodeData["primaryCategory"] = "control";
            }

            if (nodeData["subCategory"] == null)
            {
                nodeData["subCategory"] = "";
            }

            if (nodeData["controlBaseScore"] == null)
            {
                nodeData["controlBaseScore"] = "0.0";
            }
            if (nodeData["controlBaseValue"] == null)
            {
                nodeData["controlBaseValue"] = "0.0";
            }
            if (nodeData["controlBaseMinValue"] == null)
            {
                nodeData["controlBaseMinValue"] = "0.0";
            }

            if (nodeData["calculatedValue"] == null)
            {
                nodeData["calculatedValue"] = "0.0";
            }

            if (nodeData["controlAssessedValue"] == null)
            {
                nodeData["controlAssessedValue"] = "0.0";
            }

            if (nodeData["controlAssessedMinValue"] == null)
            {
                nodeData["controlAssessedMinValue"] = "0.0";
            }

            if (nodeData["actorCapabilityValue"] == null || nodeData["actorCapabilityValue"].ToString() == "")
            {
                nodeData["actorCapabilityValue"] = "0.0";
            }

            if (nodeData["actorCapabilityMinValue"] == null || nodeData["actorCapabilityMinValue"].ToString() == "")
            {
                nodeData["actorCapabilityMinValue"] = "0.0";
            }

            if (nodeData["actorResourcesValue"] == null || nodeData["actorResourcesValue"].ToString() == "")
            {
                nodeData["actorResourcesValue"] = "0.0";
            }

            if (nodeData["actorResourcesMinValue"] == null || nodeData["actorResourcesMinValue"].ToString() == "")
            {
                nodeData["actorResourcesMinValue"] = "0.0";
            }

            if (nodeData["actorMotivationValue"] == null || nodeData["actorMotivationValue"].ToString() == "")
            {
                nodeData["actorMotivationValue"] = "0.0";
            }

            if (nodeData["actorMotivationMinValue"] == null || nodeData["actorMotivationMinValue"].ToString() == "")
            {
                nodeData["actorMotivationMinValue"] = "0.0";
            }

            if (nodeData["actorAccessValue"] == null || nodeData["actorAccessValue"].ToString() == "")
            {
                nodeData["actorAccessValue"] = "0.0";
            }

            if (nodeData["actorAccessMinValue"] == null || nodeData["actorAccessMinValue"].ToString() == "")
            {
                nodeData["actorAccessMinValue"] = "0.0";
            }

            if (nodeData["actorImpactToConfidentialityValue"] == null || nodeData["actorImpactToConfidentialityValue"].ToString() == "")
            {
                nodeData["actorImpactToConfidentialityValue"] = "0.0";
            }
            if (nodeData["actorImpactToConfidentialityMinValue"] == null || nodeData["actorImpactToConfidentialityMinValue"].ToString() == "")
            {
                nodeData["actorImpactToConfidentialityMinValue"] = "0.0";
            }


            if (nodeData["actorImpactToIntegrityValue"] == null || nodeData["actorImpactToIntegrityValue"].ToString() == "")
            {
                nodeData["actorImpactToIntegrityValue"] = "0.0";
            }
            if (nodeData["actorImpactToIntegrityMinValue"] == null || nodeData["actorImpactToIntegrityMinValue"].ToString() == "")
            {
                nodeData["actorImpactToIntegrityMinValue"] = "0.0";
            }

            if (nodeData["actorImpactToAvailabilityValue"] == null || nodeData["actorImpactToAvailabilityValue"].ToString() == "")
            {
                nodeData["actorImpactToAvailabilityValue"] = "0.0";
            }
            if (nodeData["actorImpactToAvailabilityMinValue"] == null || nodeData["actorImpactToAvailabilityMinValue"].ToString() == "")
            {
                nodeData["actorImpactToAvailabilityMinValue"] = "0.0";
            }

            if (nodeData["actorImpactToAccountabilityValue"] == null || nodeData["actorImpactToAccountabilityValue"].ToString() == "")
            {
                nodeData["actorImpactToAccountabilityValue"] = "0.0";
            }
            if (nodeData["actorImpactToAccountabilityMinValue"] == null || nodeData["actorImpactToAccountabilityMinValue"].ToString() == "")
            {
                nodeData["actorImpactToAccountabilityMinValue"] = "0.0";
            }

            if (nodeData["actorScore"] == null || nodeData["actorScore"].ToString() == "")
            {
                nodeData["actorScore"] = "0.0";
            }

            if (nodeData["attackComplexityValue"] == null || nodeData["attackComplexityValue"].ToString() == "")
            {
                nodeData["attackComplexityValue"] = "0.0";
            }

            if (nodeData["attackComplexityMinValue"] == null || nodeData["attackComplexityMinValue"].ToString() == "")
            {
                nodeData["attackComplexityMinValue"] = "0.0";
            }

            if (nodeData["attackProliferationValue"] == null || nodeData["attackProliferationValue"].ToString() == "")
            {
                nodeData["attackProliferationValue"] = "0.0";
            }

            if (nodeData["attackProliferationMinValue"] == null || nodeData["attackProliferationMinValue"].ToString() == "")
            {
                nodeData["attackProliferationMinValue"] = "0.0";
            }

            if (nodeData["attackImpactToConfidentialityValue"] == null || nodeData["attackImpactToConfidentialityValue"].ToString() == "")
            {
                nodeData["attackImpactToConfidentialityValue"] = "0.0";
            }
            if (nodeData["attackImpactToConfidentialityMinValue"] == null || nodeData["attackImpactToConfidentialityMinValue"].ToString() == "")
            {
                nodeData["attackImpactToConfidentialityMinValue"] = "0.0";
            }

            if (nodeData["attackImpactToIntegrityValue"] == null || nodeData["attackImpactToIntegrityValue"].ToString() == "")
            {
                nodeData["attackImpactToIntegrityValue"] = "0.0";
            }
            if (nodeData["attackImpactToIntegrityMinValue"] == null || nodeData["attackImpactToIntegrityMinValue"].ToString() == "")
            {
                nodeData["attackImpactToIntegrityMinValue"] = "0.0";
            }

            if (nodeData["attackImpactToAvailabilityValue"] == null || nodeData["attackImpactToAvailabilityValue"].ToString() == "")
            {
                nodeData["attackImpactToAvailabilityValue"] = "0.0";
            }
            if (nodeData["attackImpactToAvailabilityMinValue"] == null || nodeData["attackImpactToAvailabilityMinValue"].ToString() == "")
            {
                nodeData["attackImpactToAvailabilityMinValue"] = "0.0";
            }

            if (nodeData["attackImpactToAccountabilityValue"] == null || nodeData["attackImpactToAccountabilityValue"].ToString() == "")
            {
                nodeData["attackImpactToAccountabilityValue"] = "0.0";
            }
            if (nodeData["attackImpactToAccountabilityMinValue"] == null || nodeData["attackImpactToAccountabilityMinValue"].ToString() == "")
            {
                nodeData["attackImpactToAccountabilityMinValue"] = "0.0";
            }

            if (nodeData["attackScore"] == null || nodeData["attackScore"].ToString() == "")
            {
                nodeData["attackScore"] = "0.0";
            }

            if (nodeData["assetConfidentialityValue"] == null || nodeData["assetConfidentialityValue"].ToString() == "")
            {
                nodeData["assetConfidentialityValue"] = "0.0";
            }

            if (nodeData["assetConfidentialityMinValue"] == null || nodeData["assetConfidentialityMinValue"].ToString() == "")
            {
                nodeData["assetConfidentialityMinValue"] = "0.0";
            }
            if (nodeData["assetConfidentialityProbabilityValue"] == null || nodeData["assetConfidentialityProbabilityValue"].ToString() == "")
            {
                nodeData["assetConfidentialityProbabilityValue"] = "0.0";
            }

            if (nodeData["assetIntegrityValue"] == null || nodeData["assetIntegrityValue"].ToString() == "")
            {
                nodeData["assetIntegrityValue"] = "0.0";
            }
            if (nodeData["assetIntegrityMinValue"] == null || nodeData["assetIntegrityMinValue"].ToString() == "")
            {
                nodeData["assetIntegrityMinValue"] = "0.0";
            }
            if (nodeData["assetIntegrityProbabilityValue"] == null || nodeData["assetIntegrityProbabilityValue"].ToString() == "")
            {
                nodeData["assetIntegrityProbabilityValue"] = "0.0";
            }

            if (nodeData["assetAvailabilityValue"] == null || nodeData["assetAvailabilityValue"].ToString() == "")
            {
                nodeData["assetAvailabilityValue"] = "0.0";

            }
            if (nodeData["assetAvailabilityMinValue"] == null || nodeData["assetAvailabilityMinValue"].ToString() == "")
            {
                nodeData["assetAvailabilityMinValue"] = "0.0";
            }
            if (nodeData["assetAvailabilityProbabilityValue"] == null || nodeData["assetAvailabilityProbabilityValue"].ToString() == "")
            {
                nodeData["assetAvailabilityProbabilityValue"] = "0.0";
            }

            if (nodeData["assetAccountabilityValue"] == null || nodeData["assetAccountabilityValue"].ToString() == "")
            {
                nodeData["assetAccountabilityValue"] = "0.0";
            }
            if (nodeData["assetAccountabilityMinValue"] == null || nodeData["assetAccountabilityMinValue"].ToString() == "")
            {
                nodeData["assetAccountabilityMinValue"] = "0.0";
            }
            if (nodeData["assetAccountabilityProbabilityValue"] == null || nodeData["assetAccountabilityProbabilityValue"].ToString() == "")
            {
                nodeData["assetAccountabilityProbabilityValue"] = "0.0";
            }

            if (nodeData["assetFinancialImpactValue"] == null || nodeData["assetFinancialImpactValue"].ToString() == "")
            {
                nodeData["assetFinancialImpactValue"] = "0.0";
            }
            if (nodeData["assetFinancialImpactMinValue"] == null || nodeData["assetFinancialImpactMinValue"].ToString() == "")
            {
                nodeData["assetFinancialImpactMinValue"] = "0.0";
            }

            if (nodeData["assetRegulatoryImpactValue"] == null || nodeData["assetRegulatoryImpactValue"].ToString() == "")
            {
                nodeData["assetRegulatoryImpactValue"] = "0.0";
            }
            if (nodeData["assetRegulatoryImpactMinValue"] == null || nodeData["assetRegulatoryImpactMinValue"].ToString() == "")
            {
                nodeData["assetRegulatoryImpactMinValue"] = "0.0";
            }

            if (nodeData["assetReputationalImpactValue"] == null || nodeData["assetReputationalImpactValue"].ToString() == "")
            {
                nodeData["assetReputationalImpactValue"] = "0.0";
            }
            if (nodeData["assetReputationalImpactMinValue"] == null || nodeData["assetReputationalImpactMinValue"].ToString() == "")
            {
                nodeData["assetReputationalImpactMinValue"] = "0.0";
            }

            if (nodeData["assetLegalImpactValue"] == null || nodeData["assetLegalImpactValue"].ToString() == "")
            {
                nodeData["assetLegalImpactValue"] = "0.0";
            }
            if (nodeData["assetLegalImpactMinValue"] == null || nodeData["assetLegalImpactMinValue"].ToString() == "")
            {
                nodeData["assetLegalImpactMinValue"] = "0.0";
            }

            if (nodeData["assetPrivacyImpactValue"] == null || nodeData["assetPrivacyImpactValue"].ToString() == "")
            {
                nodeData["assetPrivacyImpactValue"] = "0.0";
            }
            if (nodeData["assetPrivacyImpactMinValue"] == null || nodeData["assetPrivacyImpactMinValue"].ToString() == "")
            {
                nodeData["assetPrivacyImpactMinValue"] = "0.0";
            }

            if (nodeData["vulnerabilityEaseOfExploitationValue"] == null || nodeData["vulnerabilityEaseOfExploitationValue"].ToString() == "")
            {
                nodeData["vulnerabilityEaseOfExploitationValue"] = "0.0";
            }

            if (nodeData["vulnerabilityEaseOfExploitationMinValue"] == null || nodeData["vulnerabilityEaseOfExploitationMinValue"].ToString() == "")
            {
                nodeData["vulnerabilityEaseOfExploitationMinValue"] = "0.0";
            }

            if (nodeData["vulnerabilityExposureValue"] == null || nodeData["vulnerabilityExposureValue"].ToString() == "")
            {
                nodeData["vulnerabilityExposureValue"] = "0.0";
            }
            if (nodeData["vulnerabilityExposureMinValue"] == null || nodeData["vulnerabilityExposureMinValue"].ToString() == "")
            {
                nodeData["vulnerabilityExposureMinValue"] = "0.0";
            }

            if (nodeData["vulnerabilityPrivilegesRequiredValue"] == null || nodeData["vulnerabilityPrivilegesRequiredValue"].ToString() == "")
            {
                nodeData["vulnerabilityPrivilegesRequiredValue"] = "0.0";
            }
            if (nodeData["vulnerabilityPrivilegesRequiredMinValue"] == null || nodeData["vulnerabilityPrivilegesRequiredMinValue"].ToString() == "")
            {
                nodeData["vulnerabilityPrivilegesRequiredMinValue"] = "0.0";
            }

            if (nodeData["vulnerabilityInteractionRequiredValue"] == null || nodeData["vulnerabilityInteractionRequiredValue"].ToString() == "")
            {
                nodeData["vulnerabilityInteractionRequiredValue"] = "0.0";
            }
            if (nodeData["vulnerabilityInteractionRequiredMinValue"] == null || nodeData["vulnerabilityInteractionRequiredMinValue"].ToString() == "")
            {
                nodeData["vulnerabilityInteractionRequiredMinValue"] = "0.0";
            }

            if (nodeData["vulnerabilityExposesScopeValue"] == null || nodeData["vulnerabilityExposesScopeValue"].ToString() == "")
            {
                nodeData["vulnerabilityExposesScopeValue"] = "0.0";
            }
            if (nodeData["vulnerabilityExposesScopeMinValue"] == null || nodeData["vulnerabilityExposesScopeMinValue"].ToString() == "")
            {
                nodeData["vulnerabilityExposesScopeMinValue"] = "0.0";
            }

            if (nodeData["vulnerabilityImpactToConfidentialityValue"] == null || nodeData["vulnerabilityImpactToConfidentialityValue"].ToString() == "")
            {
                nodeData["vulnerabilityImpactToConfidentialityValue"] = "0.0";
            }
            if (nodeData["vulnerabilityImpactToConfidentialityMinValue"] == null || nodeData["vulnerabilityImpactToConfidentialityMinValue"].ToString() == "")
            {
                nodeData["vulnerabilityImpactToConfidentialityMinValue"] = "0.0";
            }

            if (nodeData["vulnerabilityImpactToIntegrityValue"] == null || nodeData["vulnerabilityImpactToIntegrityValue"].ToString() == "")
            {
                nodeData["vulnerabilityImpactToIntegrityValue"] = "0.0";
            }
            if (nodeData["vulnerabilityImpactToIntegrityMinValue"] == null || nodeData["vulnerabilityImpactToIntegrityMinValue"].ToString() == "")
            {
                nodeData["vulnerabilityImpactToIntegrityMinValue"] = "0.0";
            }

            if (nodeData["vulnerabilityImpactToAvailabilityValue"] == null || nodeData["vulnerabilityImpactToAvailabilityValue"].ToString() == "")
            {
                nodeData["vulnerabilityImpactToAvailabilityValue"] = "0.0";
            }
            if (nodeData["vulnerabilityImpactToAvailabilityMinValue"] == null || nodeData["vulnerabilityImpactToAvailabilityMinValue"].ToString() == "")
            {
                nodeData["vulnerabilityImpactToAvailabilityMinValue"] = "0.0";
            }

            if (nodeData["vulnerabilityImpactToAccountabilityValue"] == null || nodeData["vulnerabilityImpactToAccountabilityValue"].ToString() == "")
            {
                nodeData["vulnerabilityImpactToAccountabilityValue"] = "0.0";
            }
            if (nodeData["vulnerabilityImpactToAccountabilityMinValue"] == null || nodeData["vulnerabilityImpactToAccountabilityMinValue"].ToString() == "")
            {
                nodeData["vulnerabilityImpactToAccountabilityMinValue"] = "0.0";
            }

            if (nodeData["assetReputationalImpactConfidentialityValue"] == null || nodeData["assetReputationalImpactConfidentialityValue"].ToString() == "")
            {
                nodeData["assetReputationalImpactConfidentialityValue"] = "0.0";
            }
            if (nodeData["assetFinancialImpactConfidentialityValue"] == null || nodeData["assetFinancialImpactConfidentialityValue"].ToString() == "")
            {
                nodeData["assetFinancialImpactConfidentialityValue"] = "0.0";
            }
            if (nodeData["assetPrivacyImpactIntegrityValue"] == null || nodeData["assetPrivacyImpactIntegrityValue"].ToString() == "")
            {
                nodeData["assetPrivacyImpactIntegrityValue"] = "0.0";
            }
            if (nodeData["assetLegalImpactIntegrityValue"] == null || nodeData["assetLegalImpactIntegrityValue"].ToString() == "")
            {
                nodeData["assetLegalImpactIntegrityValue"] = "0.0";
            }
            if (nodeData["assetRegulatoryImpactIntegrityValue"] == null || nodeData["assetRegulatoryImpactIntegrityValue"].ToString() == "")
            {
                nodeData["assetRegulatoryImpactIntegrityValue"] = "0.0";
            }
            if (nodeData["assetFinancialImpactIntegrityValue"] == null || nodeData["assetFinancialImpactIntegrityValue"].ToString() == "")
            {
                nodeData["assetFinancialImpactIntegrityValue"] = "0.0";
            }
            if (nodeData["assetPrivacyImpactAvailabilityValue"] == null || nodeData["assetPrivacyImpactAvailabilityValue"].ToString() == "")
            {
                nodeData["assetPrivacyImpactAvailabilityValue"] = "0.0";
            }
            if (nodeData["assetRegulatoryImpactConfidentiality"] == null || nodeData["assetRegulatoryImpactConfidentiality"].ToString() == "")
            {
                nodeData["assetRegulatoryImpactConfidentiality"] = "0.0";
            }
            if (nodeData["assetReputationalImpactIntegrityValue"] == null || nodeData["assetReputationalImpactIntegrityValue"].ToString() == "")
            {
                nodeData["assetReputationalImpactIntegrityValue"] = "0.0";
            }
            if (nodeData["assetPrivacyImpactConfidentialityMinValue"] == null || nodeData["assetPrivacyImpactConfidentialityMinValue"].ToString() == "")
            {
                nodeData["assetPrivacyImpactConfidentialityMinValue"] = "0.0";
            }
            if (nodeData["assetLegalImpactConfidentialityMinValue"] == null || nodeData["assetLegalImpactConfidentialityMinValue"].ToString() == "")
            {
                nodeData["assetLegalImpactConfidentialityMinValue"] = "0.0";
            }
            if (nodeData["assetRegulatoryImpactConfidentialityValue"] == null || nodeData["assetRegulatoryImpactConfidentialityValue"].ToString() == "")
            {
                nodeData["assetRegulatoryImpactConfidentialityValue"] = "0.0";
            }
            if (nodeData["assetRegulatoryImpactConfidentialityMinValue"] == null || nodeData["assetRegulatoryImpactConfidentialityMinValue"].ToString() == "")
            {
                nodeData["assetRegulatoryImpactConfidentialityMinValue"] = "0.0";
            }
            if (nodeData["assetReputationalImpactConfidentialityMinValue"] == null || nodeData["assetReputationalImpactConfidentialityMinValue"].ToString() == "")
            {
                nodeData["assetReputationalImpactConfidentialityMinValue"] = "0.0";
            }
            if (nodeData["assetFinancialImpactConfidentialityMinValue"] == null || nodeData["assetFinancialImpactConfidentialityMinValue"].ToString() == "")
            {
                nodeData["assetFinancialImpactConfidentialityMinValue"] = "0.0";
            }
            if (nodeData["assetPrivacyImpactIntegrityMinValue"] == null || nodeData["assetPrivacyImpactIntegrityMinValue"].ToString() == "")
            {
                nodeData["assetPrivacyImpactIntegrityMinValue"] = "0.0";
            }
            if (nodeData["assetLegalImpactIntegrityMinValue"] == null || nodeData["assetLegalImpactIntegrityMinValue"].ToString() == "")
            {
                nodeData["assetLegalImpactIntegrityMinValue"] = "0.0";
            }
            if (nodeData["assetRegulatoryImpactIntegrityMinValue"] == null || nodeData["assetRegulatoryImpactIntegrityMinValue"].ToString() == "")
            {
                nodeData["assetRegulatoryImpactIntegrityMinValue"] = "0.0";
            }
            if (nodeData["assetReputationalImpactIntegrityMinValue"] == null || nodeData["assetReputationalImpactIntegrityMinValue"].ToString() == "")
            {
                nodeData["assetReputationalImpactIntegrityMinValue"] = "0.0";
            }
            if (nodeData["assetFinancialImpactIntegrityMinValue"] == null || nodeData["assetFinancialImpactIntegrityMinValue"].ToString() == "")
            {
                nodeData["assetFinancialImpactIntegrityMinValue"] = "0.0";
            }
            if (nodeData["assetPrivacyImpactAvailabilityMinValue"] == null || nodeData["assetPrivacyImpactAvailabilityMinValue"].ToString() == "")
            {
                nodeData["assetPrivacyImpactAvailabilityMinValue"] = "0.0";
            }
            if (nodeData["assetLegalImpactAvailabilityMinValue"] == null || nodeData["assetLegalImpactAvailabilityMinValue"].ToString() == "")
            {
                nodeData["assetLegalImpactAvailabilityMinValue"] = "0.0";
            }
            if (nodeData["assetRegulatoryImpactAvailabilityMinValue"] == null || nodeData["assetRegulatoryImpactAvailabilityMinValue"].ToString() == "")
            {
                nodeData["assetRegulatoryImpactAvailabilityMinValue"] = "0.0";
            }
            if (nodeData["assetReputationalImpactAvailabilityMinValue"] == null || nodeData["assetReputationalImpactAvailabilityMinValue"].ToString() == "")
            {
                nodeData["assetReputationalImpactAvailabilityMinValue"] = "0.0";
            }
            if (nodeData["assetFinancialImpactAvailabilityMinValue"] == null || nodeData["assetFinancialImpactAvailabilityMinValue"].ToString() == "")
            {
                nodeData["assetFinancialImpactAvailabilityMinValue"] = "0.0";
            }
            if (nodeData["assetPrivacyImpactAccountabilityMinValue"] == null || nodeData["assetPrivacyImpactAccountabilityMinValue"].ToString() == "")
            {
                nodeData["assetPrivacyImpactAccountabilityMinValue"] = "0.0";
            }
            if (nodeData["assetLegalImpactAccountabilityMinValue"] == null || nodeData["assetLegalImpactAccountabilityMinValue"].ToString() == "")
            {
                nodeData["assetLegalImpactAccountabilityMinValue"] = "0.0";
            }
            if (nodeData["assetRegulatoryImpactAccountabilityMinValue"] == null || nodeData["assetRegulatoryImpactAccountabilityMinValue"].ToString() == "")
            {
                nodeData["assetRegulatoryImpactAccountabilityMinValue"] = "0.0";
            }
            if (nodeData["assetReputationalImpactAccountabilityMinValue"] == null || nodeData["assetReputationalImpactAccountabilityMinValue"].ToString() == "")
            {
                nodeData["assetReputationalImpactAccountabilityMinValue"] = "0.0";
            }
            if (nodeData["assetFinancialImpactAccountabilityValue"] == null || nodeData["assetFinancialImpactAccountabilityValue"].ToString() == "")
            {
                nodeData["assetFinancialImpactAccountabilityValue"] = "0.0";
            }
            if (nodeData["assetFinancialImpactAccountabilityMinValue"] == null || nodeData["assetFinancialImpactAccountabilityMinValue"].ToString() == "")
            {
                nodeData["assetFinancialImpactAccountabilityMinValue"] = "0.0";
            }
            if (nodeData["distributionData"] == null || nodeData["distributionData"].ToString() == "")
            {
                nodeData["distributionData"] = "[]";
            }
            if (nodeData["assetScore"] == null || nodeData["assetScore"].ToString() == "")
            {
                nodeData["assetScore"] = "0.0";
            }

            if (nodeData["threatScore"] == null || nodeData["threatScore"].ToString() == "")
            {
                nodeData["threatScore"] = "0.0";
            }

            if (nodeData["assetLikelihoodScore"] == null || nodeData["assetLikelihoodScore"].ToString() == "")
            {
                nodeData["assetLikelihoodScore"] = "0.0";
            }

            if (nodeData["impactScore"] == null || nodeData["impactScore"].ToString() == "")
            {
                nodeData["impactScore"] = "0.0";
            }

            if (nodeData["controlBaseScoreAssessmentStatus"] == null)
            {
                nodeData["controlBaseScoreAssessmentStatus"] = "";
            }

            if (nodeData["borderColor"] == null)
            {
                nodeData["borderColor"] = "";
            }

            if (nodeData["borderWidth"] == null)
            {
                nodeData["borderWidth"] = "1";
            }

            if (nodeData["image"] == null)
            {
                nodeData["image"] = "";
            }

            if (nodeData["imagePath"] == null)
            {
                nodeData["imagePath"] = "";
            }

            if (nodeData["implementedStrength"] == null)
            {
                nodeData["implementedStrength"] = "";
            }

            if (nodeData["objectiveTargetType"] == null)
            {
                nodeData["objectiveTargetType"] = "";
            }

            if (nodeData["objectiveTargetValue"] == null)
            {
                nodeData["objectiveTargetValue"] = "0.0";
            }

            if (nodeData["objectiveAcheivedValue"] == null)
            {
                nodeData["objectiveAcheivedValue"] = "0.0";
            }

            if (nodeData["x"] == null || nodeData["x"].ToString() == "")
            {
                nodeData["x"] = "0";
            }

            if (nodeData["y"] == null || nodeData["y"].ToString() == "")
            {
                nodeData["y"] = "0";
            }

            if (nodeData["calculatedValue"] == null || nodeData["calculatedValue"].ToString() == "")
            {
                nodeData["calculatedValue"] = "0.0";
            }

            if (nodeData["parent"] == null)
            {
                nodeData["parent"] = "";
            }

            if (nodeData["nodeBehaviour"] == null)
            {
                nodeData["nodeBehaviour"] = "Sum";
            }

            if (nodeData["evidenceConfidenceValue"] == null)
            {
                nodeData["evidenceConfidenceValue"] = "0";
            }

            if (nodeData["controlBaseDistribution "] == null)
            {
                nodeData["controlBaseDistribution "] = "";
            }

            if (nodeData["controlAssessedDistribution"] == null)
            {
                nodeData["controlAssessedDistribution"] = "";
            }
           
            if (nodeData["actorAccessDistribution "] == null)
            {
                nodeData["actorAccessDistribution "] = "";
            }
            
            if (nodeData["actorCapabilityDistribution"] == null)
            {
                nodeData["actorCapabilityDistribution"] = "";
            }
           
            if (nodeData["actorResourcesDistribution "] == null)
            {
                nodeData["actorResourcesDistribution "] = "";
            }
            
            if (nodeData["actorMotivationDistribution "] == null)
            {
                nodeData["actorMotivationDistribution "] = "";
            }
           
            if (nodeData["actorImpactToConfidentialityDistribution "] == null)
            {
                nodeData["actorImpactToConfidentialityDistribution "] = "";
            }
            
            if (nodeData["actorImpactToIntegrityDistribution "] == null)
            {
                nodeData["actorImpactToIntegrityDistribution "] = "";
            }
            
            if (nodeData["actorImpactToAvailabilityDistribution "] == null)
            {
                nodeData["actorImpactToAvailabilityDistribution "] = "";
            }
           
            if (nodeData["actorImpactToAccountabilityDistribution "] == null)
            {
                nodeData["actorImpactToAccountabilityDistribution "] = "";
            }
            
            if (nodeData["vulnerabilityEaseOfExploitationDistribution"] == null)
            {
                nodeData["vulnerabilityEaseOfExploitationDistribution"] = "";
            }
            
            if (nodeData["vulnerabilityExposesScopeDistribution"] == null)
            {
                nodeData["vulnerabilityExposesScopeDistribution"] = "";
            }
            
            if (nodeData["vulnerabilityInteractionRequiredDistribution"] == null)
            {
                nodeData["vulnerabilityInteractionRequiredDistribution"] = "";
            }
            
            if (nodeData["vulnerabilityPrivilegesRequiredDistribution"] == null)
            {
                nodeData["vulnerabilityPrivilegesRequiredDistribution"] = "";
            }
            
            if (nodeData["vulnerabilityExposureDistribution"] == null)
            {
                nodeData["vulnerabilityExposureDistribution"] = "";
            }
            
            if (nodeData["vulnerabilityImpactToConfidentialityDistribution"] == null)
            {
                nodeData["vulnerabilityImpactToConfidentialityDistribution"] = "";
            }
            
            if (nodeData["vulnerabilityImpactToIntegrityDistribution"] == null)
            {
                nodeData["vulnerabilityImpactToIntegrityDistribution"] = "";
            }
            
            if (nodeData["vulnerabilityImpactToAvailabilityDistribution"] == null)
            {
                nodeData["vulnerabilityImpactToAvailabilityDistribution"] = "";
            }
            
            if (nodeData["vulnerabilityImpactToAccountabilityDistribution"] == null)
            {
                nodeData["vulnerabilityImpactToAccountabilityDistribution"] = "";
            }
            
            if (nodeData["attackComplexityDistribution"] == null)
            {
                nodeData["attackComplexityDistribution"] = "";
            }
            
            if (nodeData["attackProliferationDistribution"] == null)
            {
                nodeData["attackProliferationDistribution"] = "";
            }
            
            if (nodeData["attackImpactToConfidentialityDistribution"] == null)
            {
                nodeData["attackImpactToConfidentialityDistribution"] = "";
            }
            
            if (nodeData["attackImpactToIntegrityDistribution"] == null)
            {
                nodeData["attackImpactToIntegrityDistribution"] = "";
            }
            
            if (nodeData["attackImpactToAvailabilityDistribution"] == null)
            {
                nodeData["attackImpactToAvailabilityDistribution"] = "";
            }
            
            if (nodeData["attackImpactToAccountabilityDistribution"] == null)
            {
                nodeData["attackImpactToAccountabilityDistribution"] = "";
            }
            
            if (nodeData["assetConfidentialityDistribution"] == null)
            {
                nodeData["assetConfidentialityDistribution"] = "";
            }
            
            if (nodeData["assetIntegrityDistribution"] == null)
            {
                nodeData["assetIntegrityDistribution"] = "";
            }
            
            if (nodeData["assetAvailabilityDistribution"] == null)
            {
                nodeData["assetAvailabilityDistribution"] = "";
            }
            
            if (nodeData["assetAccountabilityDistribution"] == null)
            {
                nodeData["assetAccountabilityDistribution"] = "";
            }
            
            if (nodeData["assetPrivacyImpactConfidentialityDistribution"] == null)
            {
                nodeData["assetPrivacyImpactConfidentialityDistribution"] = "";
            }
            
            if (nodeData["assetLegalImpactConfidentialityDistribution"] == null)
            {
                nodeData["assetLegalImpactConfidentialityDistribution"] = "";
            }
            
            if (nodeData["assetRegulatoryImpactConfidentialityDistribution"] == null)
            {
                nodeData["assetRegulatoryImpactConfidentialityDistribution"] = "";
            }
            
            if (nodeData["assetReputationalImpactConfidentialityDistribution"] == null)
            {
                nodeData["assetReputationalImpactConfidentialityDistribution"] = "";
            }
            
            if (nodeData["assetFinancialImpactConfidentialityDistribution"] == null)
            {
                nodeData["assetFinancialImpactConfidentialityDistribution"] = "";
            }
            
            if (nodeData["assetPrivacyImpactIntegrityDistribution"] == null)
            {
                nodeData["assetPrivacyImpactIntegrityDistribution"] = "";
            }
            
            if (nodeData["assetLegalImpactIntegrityDistribution"] == null)
            {
                nodeData["assetLegalImpactIntegrityDistribution"] = "";
            }
            
            if (nodeData["assetReputationalImpactIntegrityDistribution"] == null)
            {
                nodeData["assetReputationalImpactIntegrityDistribution"] = "";
            }
            
            if (nodeData["assetRegulatoryImpactIntegrityDistribution"] == null)
            {
                nodeData["assetRegulatoryImpactIntegrityDistribution"] = "";
            }
            
            if (nodeData["assetFinancialImpactIntegrityDistribution"] == null)
            {
                nodeData["assetFinancialImpactIntegrityDistribution"] = "";
            }
            
            if (nodeData["assetPrivacyImpactAvailabilityDistribution"] == null)
            {
                nodeData["assetPrivacyImpactAvailabilityDistribution"] = "";
            }
            
            if (nodeData["assetLegalImpactAvailabilityDistribution"] == null)
            {
                nodeData["assetLegalImpactAvailabilityDistribution"] = "";
            }
            
            if (nodeData["assetReputationalImpactAvailabilityDistribution"] == null)
            {
                nodeData["assetReputationalImpactAvailabilityDistribution"] = "";
            }

            if (nodeData["assetRegulatoryImpactAvailabilityDistribution"] == null)
            {
                nodeData["assetRegulatoryImpactAvailabilityDistribution"] = "";
            }

            if (nodeData["assetFinancialImpactAvailabilityDistribution"] == null)
            {
                nodeData["assetFinancialImpactAvailabilityDistribution"] = "";
            }
            
            if (nodeData["assetPrivacyImpactAccountabilityDistribution"] == null)
            {
                nodeData["assetPrivacyImpactAccountabilityDistribution"] = "";
            }
            
            if (nodeData["assetLegalImpactAccountabilityDistribution"] == null)
            {
                nodeData["assetLegalImpactAccountabilityDistribution"] = "";
            }
            
            if (nodeData["assetRegulatoryImpactAccountabilityDistribution"] == null)
            {
                nodeData["assetRegulatoryImpactAccountabilityDistribution"] = "";
            }
            
            if (nodeData["assetReputationalImpactAccountabilityDistribution"] == null)
            {
                nodeData["assetReputationalImpactAccountabilityDistribution"] = "";
            }
            
            if (nodeData["assetFinancialImpactAccountabilityDistribution"] == null)
            {
                nodeData["assetFinancialImpactAccountabilityDistribution"] = "";
            }

            if (nodeData["assetConfidentialityMitigatedScore"] == null)
            {
                nodeData["assetConfidentialityMitigatedScore"] = "0.0";
            }

            if (nodeData["assetIntegrityMitigatedScore"] == null)
            {
                nodeData["assetIntegrityMitigatedScore"] = "0.0";
            }

            if (nodeData["assetAvailabilityMitigatedScore"] == null)
            {
                nodeData["assetAvailabilityMitigatedScore"] = "0.0";
            }

            if (nodeData["assetAccountabilityMitigatedScore"] == null)
            {
                nodeData["assetAccountabilityMitigatedScore"] = "0.0";
            }


            Node retval = new Node();

            retval.ID = nodeData["id"] != null ? nodeData["id"].ToString() : "";
            retval.Parent = nodeData["parent"] != null ? nodeData["parent"].ToString() : "";
            retval.masterID = nodeData["masterID"] != null ? nodeData["masterID"].ToString() : "";
            retval.Title = nodeData["title"] != null ? nodeData["title"].ToString() : "";
            retval.TitleSize = nodeData["titleSize"] != null ? ConvertToDoubleOrDefault(nodeData["titleSize"].ToString()) : 14;
            retval.description = nodeData["description"] != null ? nodeData["description"].ToString() : "";
            retval.Note = nodeData["note"] != null ? nodeData["note"].ToString() : "";
            retval.Domain = nodeData["domain"] != null ? nodeData["domain"].ToString() : "";
            retval.SubDomain = nodeData["subdomain"] != null ? nodeData["subdomain"].ToString() : "";
            retval.ReferenceURL = nodeData["refurl"] != null ? nodeData["refurl"].ToString() : "";
            retval.frameworkReference = nodeData["frameworkReference"] != null ? nodeData["frameworkReference"].ToString() : "";
            retval.ControlFrameworkVersion = nodeData["frameworkNameVersion"] != null ? nodeData["frameworkNameVersion"].ToString() : "";
            retval.frameworkName = nodeData["frameworkName"] != null ? nodeData["frameworkName"].ToString() : "";
            retval.Category = nodeData["category"] != null ? nodeData["category"].ToString() : "";
            retval.PrimaryCategory = nodeData["primaryCategory"] != null ? nodeData["primaryCategory"].ToString() : "control";
            retval.SubCategory = nodeData["subCategory"] != null ? nodeData["subCategory"].ToString() : "";
            retval.Level = nodeData["level"] != null ? nodeData["level"].ToString() : "";
            retval.Enabled = nodeData["enabled"] != null ? nodeData["enabled"].ToString().ToLower() == "true" : false;
            retval.HTMLOpacity = nodeData["opacity"] != null ? ConvertToDoubleOrDefault(nodeData["opacity"].ToString()) : 1;
            retval.Color = nodeData["color"] != null ? GeneralHelpers.ConvertColorFromHTML(nodeData["color"].ToString()) : GeneralHelpers.ConvertColorFromHTML("#ccc");
            retval.BorderColor = nodeData["borderColor"] != null ? GeneralHelpers.ConvertColorFromHTML(nodeData["borderColor"].ToString()) : GeneralHelpers.ConvertColorFromHTML("#000");
            retval.TitleTextColor = nodeData["titleTextColor"] != null ? GeneralHelpers.ConvertColorFromHTML(nodeData["titleTextColor"].ToString()) : GeneralHelpers.ConvertColorFromHTML("#000");
            retval.Shape = nodeData["shape"] != null ? NodeShapes.NodesShapes.FirstOrDefault(item => item.Shape == nodeData["shape"].ToString()) : null;
            retval.Height = nodeData["height"] != null && nodeData["height"].ToString() != "" ? ConvertToDoubleOrDefault(nodeData["height"].ToString()) : 48;
            retval.Width = nodeData["width"] != null && nodeData["width"].ToString() != "" ? ConvertToDoubleOrDefault(nodeData["width"].ToString()) : 48;
            retval.Type = nodeData["nodeType"] != null ? NodeTypes.NodesTypes.FirstOrDefault(item => item.Type == nodeData["nodeType"].ToString()) : null;
            
            retval.controlBaseScore = nodeData["controlBaseScore"] != null ? ConvertToDoubleOrDefault(nodeData["controlBaseScore"].ToString()) : 1;
            retval.controlBaseValue = nodeData["controlBaseValue"] != null ? ConvertToDoubleOrDefault(nodeData["controlBaseValue"].ToString()) : 1;
            retval.controlBaseMinValue = nodeData["controlBaseMinValue"] != null ? ConvertToDoubleOrDefault(nodeData["controlBaseMinValue"].ToString()) : 1;
            retval.CalculatedValue = nodeData["calculatedValue"] != null ? ConvertToDoubleOrDefault(nodeData["calculatedValue"].ToString()) : 1;
            retval.controlAssessedMinValue = nodeData["controlAssessedMinValue"] != null ? ConvertToDoubleOrDefault(nodeData["controlAssessedMinValue"].ToString()) : 1;
            retval.controlAssessedValue = nodeData["controlAssessedValue"] != null ? ConvertToDoubleOrDefault(nodeData["controlAssessedValue"].ToString()) : 1;
            retval.controlBaseScoreAssessmentStatus = nodeData["controlBaseScoreAssessmentStatus"] != null ? nodeData["controlBaseScoreAssessmentStatus"].ToString() : "";
            retval.AssessedStatus = nodeData["AssessedStatus"] != null ? nodeData["AssessedStatus"].ToString() : "";
            retval.NodeImageData = nodeData["image"] != null ? nodeData["image"].ToString() : "";
            retval.ImagePath = nodeData["imagePath"] != null ? nodeData["imagePath"].ToString() : "";
            retval.objectiveTargetType = nodeData["objectiveTargetType"] != null ? nodeData["objectiveTargetType"].ToString() : "";
            retval.objectiveTargetValue = nodeData["objectiveTargetValue"] != null ? nodeData["objectiveTargetValue"].ToString() : "";
            retval.objectiveAcheivedValue = nodeData["objectiveAcheivedValue"] != null ? nodeData["objectiveAcheivedValue"].ToString() : "";

            retval.actorCapabilityText = nodeData["actorCapability"] != null ? nodeData["actorCapability"].ToString() : "";
            retval.actorCapabilityValue = nodeData["actorCapabilityValue"] != null ? ConvertToDoubleOrDefault(nodeData["actorCapabilityValue"].ToString()) : 1;
            retval.actorCapabilityMinValue = nodeData["actorCapabilityMinValue"] != null ? ConvertToDoubleOrDefault(nodeData["actorCapabilityMinValue"].ToString()) : 1;
            retval.actorResourcesText = nodeData["actorResources"] != null ? nodeData["actorResources"].ToString() : "";
            retval.actorResourcesValue = nodeData["actorResourcesValue"] != null ? ConvertToDoubleOrDefault(nodeData["actorResourcesValue"].ToString()) : 1;
            retval.actorResourcesMinValue = nodeData["actorResourcesMinValue"] != null ? ConvertToDoubleOrDefault(nodeData["actorResourcesMinValue"].ToString()) : 1;
            retval.actorMotivationText = nodeData["actorMotivation"] != null ? nodeData["actorMotivation"].ToString() : "";
            retval.actorMotivationValue = nodeData["actorMotivationValue"] != null ? ConvertToDoubleOrDefault(nodeData["actorMotivationValue"].ToString()) : 1;
            retval.actorMotivationMinValue = nodeData["actorMotivationMinValue"] != null ? ConvertToDoubleOrDefault(nodeData["actorMotivationMinValue"].ToString()) : 1;
            retval.actorAccessText = nodeData["actorAccess"] != null ? nodeData["actorAccess"].ToString() : "";
            retval.actorAccessValue = nodeData["actorAccessValue"] != null ? ConvertToDoubleOrDefault(nodeData["actorAccessValue"].ToString()) : 1;
            retval.actorAccessMinValue = nodeData["actorAccessMinValue"] != null ? ConvertToDoubleOrDefault(nodeData["actorAccessMinValue"].ToString()) : 1;
            retval.actorImpactToConfidentialityText = nodeData["actorImpactToConfidentiality"] != null ? nodeData["actorImpactToConfidentiality"].ToString() : "";
            retval.actorImpactToConfidentialityValue = nodeData["actorImpactToConfidentialityValue"] != null ? ConvertToDoubleOrDefault(nodeData["actorImpactToConfidentialityValue"].ToString()) : 1;
            retval.actorImpactToConfidentialityMinValue = nodeData["actorImpactToConfidentialityMinValue"] != null ? ConvertToDoubleOrDefault(nodeData["actorImpactToConfidentialityMinValue"].ToString()) : 1;
            retval.actorImpactToIntegrityText = nodeData["actorImpactToIntegrity"] != null ? nodeData["actorImpactToIntegrity"].ToString() : "";
            retval.actorImpactToIntegrityValue = nodeData["actorImpactToIntegrityValue"] != null ? ConvertToDoubleOrDefault(nodeData["actorImpactToIntegrityValue"].ToString()) : 1;
            retval.actorImpactToIntegrityMinValue = nodeData["actorImpactToIntegrityMinValue"] != null ? ConvertToDoubleOrDefault(nodeData["actorImpactToIntegrityMinValue"].ToString()) : 1;
            retval.actorImpactToAvailabilityText = nodeData["actorImpactToAvailability"] != null ? nodeData["actorImpactToAvailability"].ToString() : "";
            retval.actorImpactToAvailabilityValue = nodeData["actorImpactToAvailabilityValue"] != null ? ConvertToDoubleOrDefault(nodeData["actorImpactToAvailabilityValue"].ToString()) : 1;
            retval.actorImpactToAvailabilityMinValue = nodeData["actorImpactToAvailabilityMinValue"] != null ? ConvertToDoubleOrDefault(nodeData["actorImpactToAvailabilityMinValue"].ToString()) : 1;
            retval.actorImpactToAccountabilityText = nodeData["actorImpactToAccountability"] != null ? nodeData["actorImpactToAccountability"].ToString() : "";
            retval.actorImpactToAccountabilityValue = nodeData["actorImpactToAccountabilityValue"] != null ? ConvertToDoubleOrDefault(nodeData["actorImpactToAccountabilityValue"].ToString()) : 1;
            retval.actorimpactToAccountabilityMinValue = nodeData["actorImpactToAccountabilityMinValue"] != null ? ConvertToDoubleOrDefault(nodeData["actorImpactToAccountabilityMinValue"].ToString()) : 1;
            retval.actorScore = nodeData["actorScore"] != null ? ConvertToDoubleOrDefault(nodeData["actorScore"].ToString()) : 1;

            retval.attackComplexityText = nodeData["attackComplexity"] != null ? nodeData["attackComplexity"].ToString() : "";
            retval.attackComplexityValue = nodeData["attackComplexityValue"] != null ? ConvertToDoubleOrDefault(nodeData["attackComplexityValue"].ToString()) : 1;
            retval.attackComplexityMinValue = nodeData["attackComplexityMinValue"] != null ? ConvertToDoubleOrDefault(nodeData["attackComplexityMinValue"].ToString()) : 1;
            retval.attackProliferationText = nodeData["attackProliferation"] != null ? nodeData["attackProliferation"].ToString() : "";
            retval.attackProliferationValue = nodeData["attackProliferationValue"] != null ? ConvertToDoubleOrDefault(nodeData["attackProliferationValue"].ToString()) : 1;
            retval.attackProliferationMinValue = nodeData["attackProliferationMinValue"] != null ? ConvertToDoubleOrDefault(nodeData["attackProliferationMinValue"].ToString()) : 1;
            retval.attackImpactToConfidentialityText = nodeData["attackImpactToConfidentiality"] != null ? nodeData["attackImpactToConfidentiality"].ToString() : "";
            retval.attackImpactToConfidentialityValue = nodeData["attackImpactToConfidentialityValue"] != null ? ConvertToDoubleOrDefault(nodeData["attackImpactToConfidentialityValue"].ToString()) : 1;
            retval.attackImpactToConfidentialityMinValue = nodeData["attackImpactToConfidentialityMinValue"] != null ? ConvertToDoubleOrDefault(nodeData["attackImpactToConfidentialityMinValue"].ToString()) : 1;
            retval.attackImpactToIntegrityText = nodeData["attackImpactToIntegrity"] != null ? nodeData["attackImpactToIntegrity"].ToString() : "";
            retval.attackImpactToIntegrityValue = nodeData["attackImpactToIntegrityValue"] != null ? ConvertToDoubleOrDefault(nodeData["attackImpactToIntegrityValue"].ToString()) : 1;
            retval.attackImpactToIntegrityMinValue = nodeData["attackImpactToIntegrityMinValue"] != null ? ConvertToDoubleOrDefault(nodeData["attackImpactToIntegrityMinValue"].ToString()) : 1;
            retval.attackImpactToAvailabilityText = nodeData["attackImpactToAvailability"] != null ? nodeData["attackImpactToAvailability"].ToString() : "";
            retval.attackImpactToAvailabilityValue = nodeData["attackImpactToAvailabilityValue"] != null ? ConvertToDoubleOrDefault(nodeData["attackImpactToAvailabilityValue"].ToString()) : 1;
            retval.attackImpactToAvailabilityMinValue = nodeData["attackImpactToAvailabilityMinValue"] != null ? ConvertToDoubleOrDefault(nodeData["attackImpactToAvailabilityMinValue"].ToString()) : 1;
            retval.attackImpactToAccountabilityText = nodeData["attackImpactToAccountability"] != null ? nodeData["attackImpactToAccountability"].ToString() : "";
            retval.attackImpactToAccountabilityValue = nodeData["attackImpactToAccountabilityValue"] != null ? ConvertToDoubleOrDefault(nodeData["attackImpactToAccountabilityValue"].ToString()) : 1;
            retval.attackImpactToAccountabilityMinValue = nodeData["attackImpactToAccountabilityMinValue"] != null ? ConvertToDoubleOrDefault(nodeData["attackImpactToAccountabilityMinValue"].ToString()) : 1;
            retval.attackScore = nodeData["attackScore"] != null ? ConvertToDoubleOrDefault(nodeData["attackScore"].ToString()) : 1;

            retval.assetConfidentialityText = nodeData["assetConfidentiality"] != null ? nodeData["assetConfidentiality"].ToString() : "";
            retval.assetConfidentialityValue = nodeData["assetConfidentialityValue"] != null ? ConvertToDoubleOrDefault(nodeData["assetConfidentialityValue"].ToString()) : 1;
            retval.assetConfidentialityMinValue = nodeData["assetConfidentialityMinValue"] != null ? ConvertToDoubleOrDefault(nodeData["assetConfidentialityMinValue"].ToString()) : 1;
            retval.assetConfidentialityProbabilityValue = nodeData["assetConfidentialityProbabilityValue"] != null ? ConvertToDoubleOrDefault(nodeData["assetConfidentialityProbabilityValue"].ToString()) : 1;
            
            retval.assetIntegrityText = nodeData["assetIntegrity"] != null ? nodeData["assetIntegrity"].ToString() : "";
            retval.assetIntegrityValue = nodeData["assetIntegrityValue"] != null ? ConvertToDoubleOrDefault(nodeData["assetIntegrityValue"].ToString()) : 1;
            retval.assetIntegrityMinValue = nodeData["assetIntegrityMinValue"] != null ? ConvertToDoubleOrDefault(nodeData["assetIntegrityMinValue"].ToString()) : 1;
            retval.assetIntegrityProbabilityValue = nodeData["assetIntegrityProbabilityValue"] != null ? ConvertToDoubleOrDefault(nodeData["assetIntegrityProbabilityValue"].ToString()) : 1;
            
            retval.assetAvailabilityText = nodeData["assetAvailability"] != null ? nodeData["assetAvailability"].ToString() : "";
            retval.assetAvailabilityValue = nodeData["assetAvailabilityValue"] != null ? ConvertToDoubleOrDefault(nodeData["assetAvailabilityValue"].ToString()) : 1;
            retval.assetAvailabilityMinValue = nodeData["assetAvailabilityMinValue"] != null ? ConvertToDoubleOrDefault(nodeData["assetAvailabilityMinValue"].ToString()) : 1;
            retval.assetAvailabilityProbabilityValue = nodeData["assetAvailabilityProbabilityValue"] != null ? ConvertToDoubleOrDefault(nodeData["assetAvailabilityProbabilityValue"].ToString()) : 1;
            
            retval.assetAccountabilityText = nodeData["assetAccountability"] != null ? nodeData["assetAccountability"].ToString() : "";
            retval.assetAccountabilityValue = nodeData["assetAccountabilityValue"] != null ? ConvertToDoubleOrDefault(nodeData["assetAccountabilityValue"].ToString()) : 1;
            retval.assetAccountabilityMinValue = nodeData["assetAccountabilityMinValue"] != null ? ConvertToDoubleOrDefault(nodeData["assetAccountabilityMinValue"].ToString()) : 1;
            retval.assetAccountabilityProbabilityValue = nodeData["assetAccountabilityProbabilityValue"] != null ? ConvertToDoubleOrDefault(nodeData["assetAccountabilityProbabilityValue"].ToString()) : 1;
            
            retval.assetFinancialImpactText = nodeData["assetFinancialImpact"] != null ? nodeData["assetFinancialImpact"].ToString() : "";
            retval.assetFinancialImpactValue = nodeData["assetFinancialImpactValue"] != null ? ConvertToDoubleOrDefault(nodeData["assetFinancialImpactValue"].ToString()) : 1;
            retval.assetFinancialImpactMinValue = nodeData["assetFinancialImpactMinValue"] != null ? ConvertToDoubleOrDefault(nodeData["assetFinancialImpactMinValue"].ToString()) : 1;
            
            retval.assetRegulatoryImpactText = nodeData["assetRegulatoryImpact"] != null ? nodeData["assetRegulatoryImpact"].ToString() : "";
            retval.assetRegulatoryImpactValue = nodeData["assetRegulatoryImpactValue"] != null ? ConvertToDoubleOrDefault(nodeData["assetRegulatoryImpactValue"].ToString()) : 1;
            retval.assetRegulatoryImpactMinValue = nodeData["assetRegulatoryImpactMinValue"] != null ? ConvertToDoubleOrDefault(nodeData["assetRegulatoryImpactMinValue"].ToString()) : 1;
            
            retval.assetReputationalImpactText = nodeData["assetReputationalImpact"] != null ? nodeData["assetReputationalImpact"].ToString() : "";
            retval.assetReputationalImpactValue = nodeData["assetReputationalImpactValue"] != null ? ConvertToDoubleOrDefault(nodeData["assetReputationalImpactValue"].ToString()) : 1;
            retval.assetReputationalImpactMinValue = nodeData["assetReputationalImpactMinValue"] != null ? ConvertToDoubleOrDefault(nodeData["assetReputationalImpactMinValue"].ToString()) : 1;
            
            retval.assetLegalImpactText = nodeData["assetLegalImpact"] != null ? nodeData["assetLegalImpact"].ToString() : "";
            retval.assetLegalImpactValue = nodeData["assetLegalImpactValue"] != null ? ConvertToDoubleOrDefault(nodeData["assetLegalImpactValue"].ToString()) : 1;
            retval.assetLegalImpactMinValue = nodeData["assetLegalImpactMinValue"] != null ? ConvertToDoubleOrDefault(nodeData["assetLegalImpactMinValue"].ToString()) : 1;
            
            retval.assetPrivacyImpactText = nodeData["assetPrivacyImpact"] != null ? nodeData["assetPrivacyImpact"].ToString() : "";
            retval.assetPrivacyImpactValue = nodeData["assetPrivacyImpactValue"] != null ? ConvertToDoubleOrDefault(nodeData["assetPrivacyImpactValue"].ToString()) : 1;
            retval.assetPrivacyImpactMinValue = nodeData["assetPrivacyImpactMinValue"] != null ? ConvertToDoubleOrDefault(nodeData["assetPrivacyImpactMinValue"].ToString()) : 1;

            retval.assetConfidentialityMitigatedScore = nodeData["assetConfidentialityMitigatedScore"] != null ? ConvertToDoubleOrDefault(nodeData["assetConfidentialityMitigatedScore"].ToString()) : 1;
            retval.assetIntegrityMitigatedScore = nodeData["assetIntegrityMitigatedScore"] != null ? ConvertToDoubleOrDefault(nodeData["assetIntegrityMitigatedScore"].ToString()) : 1;
            retval.assetAvailabilityMitigatedScore = nodeData["assetAvailabilityMitigatedScore"] != null ? ConvertToDoubleOrDefault(nodeData["assetAvailabilityMitigatedScore"].ToString()) : 1;
            retval.assetAccountabilityMitigatedScore = nodeData["assetAccountabilityMitigatedScore"] != null ? ConvertToDoubleOrDefault(nodeData["assetAccountabilityMitigatedScore"].ToString()) : 1;

            retval.assetScore = nodeData["assetScore"] != null ? ConvertToDoubleOrDefault(nodeData["assetScore"].ToString()) : 1;
            retval.assetLikelihoodScore = nodeData["assetLikelihoodScore"] != null ? ConvertToDoubleOrDefault(nodeData["assetScore"].ToString()) : 1;

            retval.vulnerabilityEaseOfExploitationText = nodeData["vulnerabilityEaseOfExploitation"] != null ? nodeData["vulnerabilityEaseOfExploitation"].ToString() : "";
            retval.vulnerabilityEaseOfExploitationValue = nodeData["vulnerabilityEaseOfExploitationValue"] != null ? ConvertToDoubleOrDefault(nodeData["vulnerabilityEaseOfExploitationValue"].ToString()) : 1;
            retval.vulnerabilityEaseOfExploitationMinValue = nodeData["vulnerabilityEaseOfExploitationMinValue"] != null ? ConvertToDoubleOrDefault(nodeData["vulnerabilityEaseOfExploitationMinValue"].ToString()) : 1;
            
            retval.vulnerabilityExposureText = nodeData["vulnerabilityExposure"] != null ? nodeData["vulnerabilityExposure"].ToString() : "";
            retval.vulnerabilityExposureValue = nodeData["vulnerabilityExposureValue"] != null ? ConvertToDoubleOrDefault(nodeData["vulnerabilityExposureValue"].ToString()) : 1;
            retval.vulnerabilityExposureMinValue = nodeData["vulnerabilityExposureMinValue"] != null ? ConvertToDoubleOrDefault(nodeData["vulnerabilityExposureMinValue"].ToString()) : 1;
            
            retval.vulnerabilityPrivilegeText = nodeData["vulnerabilityPrivilege"] != null ? nodeData["vulnerabilityPrivilege"].ToString() : "";
            retval.vulnerabilityPrivilegesRequiredValue = nodeData["vulnerabilityPrivilegesRequiredValue"] != null ? ConvertToDoubleOrDefault(nodeData["vulnerabilityPrivilegesRequiredValue"].ToString()) : 1;
            retval.vulnerabilityPrivilegesRequiredMinValue = nodeData["vulnerabilityPrivilegesRequiredMinValue"] != null ? ConvertToDoubleOrDefault(nodeData["vulnerabilityPrivilegesRequiredMinValue"].ToString()) : 1;

            retval.vulnerabilityInteractiveText = nodeData["vulnerabilityInteractive"] != null ? nodeData["vulnerabilityInteractive"].ToString() : "";
            retval.vulnerabilityInteractionRequiredValue = nodeData["vulnerabilityInteractionRequiredValue"] != null ? ConvertToDoubleOrDefault(nodeData["vulnerabilityInteractionRequiredValue"].ToString()) : 1;
            retval.vulnerabilityInteractionRequiredMinValue = nodeData["vulnerabilityInteractionRequiredMinValue"] != null ? ConvertToDoubleOrDefault(nodeData["vulnerabilityInteractionRequiredMinValue"].ToString()) : 1;

            retval.vulnerabilityExposesText = nodeData["vulnerabilityExposes"] != null ? nodeData["vulnerabilityExposes"].ToString() : "";
            retval.vulnerabilityExposesScopeValue = nodeData["vulnerabilityExposesScopeValue"] != null ? ConvertToDoubleOrDefault(nodeData["vulnerabilityExposesScopeValue"].ToString()) : 1;
            retval.vulnerabilityExposesScopeMinValue = nodeData["vulnerabilityExposesScopeMinValue"] != null ? ConvertToDoubleOrDefault(nodeData["vulnerabilityExposesScopeMinValue"].ToString()) : 1;

            retval.vulnerabilityImpactToConfidentialityValue = nodeData["vulnerabilityImpactToConfidentialityValue"] != null ? ConvertToDoubleOrDefault(nodeData["vulnerabilityImpactToConfidentialityValue"].ToString()) : 1;
            retval.vulnerabilityImpactToConfidentialityMinValue = nodeData["vulnerabilityImpactToConfidentialityMinValue"] != null ? ConvertToDoubleOrDefault(nodeData["vulnerabilityImpactToConfidentialityMinValue"].ToString()) : 1;
            retval.vulnerabilityImpactToIntegrityValue = nodeData["vulnerabilityImpactToIntegrityValue"] != null ? ConvertToDoubleOrDefault(nodeData["vulnerabilityImpactToIntegrityValue"].ToString()) : 1;
            retval.vulnerabilityImpactToIntegrityMinValue = nodeData["vulnerabilityImpactToIntegrityMinValue"] != null ? ConvertToDoubleOrDefault(nodeData["vulnerabilityImpactToIntegrityMinValue"].ToString()) : 1;
            retval.vulnerabilityImpactToAvailabilityValue = nodeData["vulnerabilityImpactToAvailabilityValue"] != null ? ConvertToDoubleOrDefault(nodeData["vulnerabilityImpactToAvailabilityValue"].ToString()) : 1;
            retval.vulnerabilityImpactToAvailabilityMinValue = nodeData["vulnerabilityImpactToAvailabilityMinValue"] != null ? ConvertToDoubleOrDefault(nodeData["vulnerabilityImpactToAvailabilityMinValue"].ToString()) : 1;
            retval.vulnerabilityImpactToAccountabilityValue = nodeData["vulnerabilityImpactToAccountabilityValue"] != null ? ConvertToDoubleOrDefault(nodeData["vulnerabilityImpactToAccountabilityValue"].ToString()) : 1;
            retval.vulnerabilityImpactToAccountabilityMinValue = nodeData["vulnerabilityImpactToAccountabilityMinValue"] != null ? ConvertToDoubleOrDefault(nodeData["vulnerabilityImpactToAccountabilityMinValue"].ToString()) : 1;

            retval.vulnerabilityImpactToConfidentialityText = nodeData["vulnerabilityImpactToConfidentiality"] != null ? nodeData["vulnerabilityImpactToConfidentiality"].ToString() : "";
            retval.vulnerabilityImpactToIntegrityText = nodeData["vulnerabilityImpactToIntegrity"] != null ? nodeData["vulnerabilityImpactToIntegrity"].ToString() : "";
            retval.vulnerabilityImpactToAvailabilityText = nodeData["vulnerabilityImpactToAvailability"] != null ? nodeData["vulnerabilityImpactToAvailability"].ToString() : "";
            retval.vulnerabilityImpactToAccountabilityText = nodeData["vulnerabilityImpactToAccountability"] != null ? nodeData["vulnerabilityImpactToAccountability"].ToString() : "";
         
           
            retval.evidenceConfidenceValue = nodeData["evidenceConfidenceValue"] != null ? ConvertToDoubleOrDefault(nodeData["evidenceConfidenceValue"].ToString()) : 1;

            retval.threatScore = nodeData["threatScore"] != null ? ConvertToDoubleOrDefault(nodeData["threatScore"].ToString()) : 1;
            retval.impactScore = nodeData["impactScore"] != null ? ConvertToDoubleOrDefault(nodeData["impactScore"].ToString()) : 1;
            retval.threatScore = nodeData["assetLikelihoodScore"] != null ? ConvertToDoubleOrDefault(nodeData["assetLikelihoodScore"].ToString()) : 1;

            retval.nodeBehaviour = nodeData["nodeBehaviour"] != null ? nodeData["nodeBehaviour"].ToString() : "Sum";

            retval.assetRegulatoryImpactAvailabilityValue = nodeData["assetRegulatoryImpactAvailabilityValue"] != null ? ConvertToDoubleOrDefault(nodeData["assetRegulatoryImpactAvailabilityValue"].ToString()) : 1;
            retval.assetReputationalImpactAvailabilityValue = nodeData["assetReputationalImpactAvailabilityValue"] != null ? ConvertToDoubleOrDefault(nodeData["assetReputationalImpactAvailabilityValue"].ToString()) : 1;
            retval.assetFinancialImpactAvailabilityValue = nodeData["assetFinancialImpactAvailabilityValue"] != null ? ConvertToDoubleOrDefault(nodeData["assetFinancialImpactAvailabilityValue"].ToString()) : 1;
            retval.assetPrivacyImpactAccountabilityValue = nodeData["assetPrivacyImpactAccountabilityValue"] != null ? ConvertToDoubleOrDefault(nodeData["assetPrivacyImpactAccountabilityValue"].ToString()) : 1;
            retval.assetLegalImpactAccountabilityValue = nodeData["assetLegalImpactAccountabilityValue"] != null ? ConvertToDoubleOrDefault(nodeData["assetLegalImpactAccountabilityValue"].ToString()) : 1;
            retval.assetRegulatoryImpactAccountabilityValue = nodeData["assetRegulatoryImpactAccountabilityValue"] != null ? ConvertToDoubleOrDefault(nodeData["assetRegulatoryImpactAccountabilityValue"].ToString()) : 1;
            retval.assetReputationalImpactAccountabilityValue = nodeData["assetReputationalImpactAccountabilityValue"] != null ? ConvertToDoubleOrDefault(nodeData["assetReputationalImpactAccountabilityValue"].ToString()) : 1;
            retval.assetPrivacyImpactConfidentialityValue = nodeData["assetPrivacyImpactConfidentialityValue"] != null ? ConvertToDoubleOrDefault(nodeData["assetPrivacyImpactConfidentialityValue"].ToString()) : 1;
            retval.assetLegalImpactConfidentialityValue = nodeData["assetLegalImpactConfidentialityValue"] != null ? ConvertToDoubleOrDefault(nodeData["assetLegalImpactConfidentialityValue"].ToString()) : 1;
            retval.assetReputationalImpactConfidentialityValue = nodeData["assetReputationalImpactConfidentialityValue"] != null ? ConvertToDoubleOrDefault(nodeData["assetReputationalImpactConfidentialityValue"].ToString()) : 1;
            retval.assetFinancialImpactConfidentialityValue = nodeData["assetFinancialImpactConfidentialityValue"] != null ? ConvertToDoubleOrDefault(nodeData["assetFinancialImpactConfidentialityValue"].ToString()) : 1;
            retval.assetPrivacyImpactIntegrityValue = nodeData["assetPrivacyImpactIntegrityValue"] != null ? ConvertToDoubleOrDefault(nodeData["assetPrivacyImpactIntegrityValue"].ToString()) : 1;
            retval.assetLegalImpactIntegrityValue = nodeData["assetLegalImpactIntegrityValue"] != null ? ConvertToDoubleOrDefault(nodeData["assetLegalImpactIntegrityValue"].ToString()) : 1;
            retval.assetRegulatoryImpactIntegrityValue = nodeData["assetRegulatoryImpactIntegrityValue"] != null ? ConvertToDoubleOrDefault(nodeData["assetRegulatoryImpactIntegrityValue"].ToString()) : 1;
            retval.assetFinancialImpactIntegrityValue = nodeData["assetFinancialImpactIntegrityValue"] != null ? ConvertToDoubleOrDefault(nodeData["assetFinancialImpactIntegrityValue"].ToString()) : 1;
            retval.assetPrivacyImpactAvailabilityValue = nodeData["assetPrivacyImpactAvailabilityValue"] != null ? ConvertToDoubleOrDefault(nodeData["assetPrivacyImpactAvailabilityValue"].ToString()) : 1;
            retval.assetLegalImpactAvailabilityValue = nodeData["assetLegalImpactAvailabilityValue"] != null ? ConvertToDoubleOrDefault(nodeData["assetLegalImpactAvailabilityValue"].ToString()) : 1;
            retval.assetRegulatoryImpactConfidentiality = nodeData["assetRegulatoryImpactConfidentiality"] != null ? ConvertToDoubleOrDefault(nodeData["assetRegulatoryImpactConfidentiality"].ToString()) : 1;
            retval.assetReputationalImpactIntegrityValue = nodeData["assetReputationalImpactIntegrityValue"] != null ? ConvertToDoubleOrDefault(nodeData["assetReputationalImpactIntegrityValue"].ToString()) : 1;
            retval.assetPrivacyImpactConfidentialityMinValue = nodeData["assetPrivacyImpactConfidentialityMinValue"] != null ? ConvertToDoubleOrDefault(nodeData["assetPrivacyImpactConfidentialityMinValue"].ToString()) : 1;
            retval.assetLegalImpactConfidentialityMinValue = nodeData["assetLegalImpactConfidentialityMinValue"] != null ? ConvertToDoubleOrDefault(nodeData["assetLegalImpactConfidentialityMinValue"].ToString()) : 1;
            retval.assetRegulatoryImpactConfidentialityValue = nodeData["assetRegulatoryImpactConfidentialityValue"] != null ? ConvertToDoubleOrDefault(nodeData["assetRegulatoryImpactConfidentialityValue"].ToString()) : 1;
            retval.assetRegulatoryImpactConfidentialityMinValue = nodeData["assetRegulatoryImpactConfidentialityMinValue"] != null ? ConvertToDoubleOrDefault(nodeData["assetRegulatoryImpactConfidentialityMinValue"].ToString()) : 1;
            retval.assetReputationalImpactConfidentialityMinValue = nodeData["assetReputationalImpactConfidentialityMinValue"] != null ? ConvertToDoubleOrDefault(nodeData["assetReputationalImpactConfidentialityMinValue"].ToString()) : 1;
            retval.assetFinancialImpactConfidentialityMinValue = nodeData["assetFinancialImpactConfidentialityMinValue"] != null ? ConvertToDoubleOrDefault(nodeData["assetFinancialImpactConfidentialityMinValue"].ToString()) : 1;
            retval.assetPrivacyImpactIntegrityMinValue = nodeData["assetPrivacyImpactIntegrityMinValue"] != null ? ConvertToDoubleOrDefault(nodeData["assetPrivacyImpactIntegrityMinValue"].ToString()) : 1;
            retval.assetLegalImpactIntegrityMinValue = nodeData["assetLegalImpactIntegrityMinValue"] != null ? ConvertToDoubleOrDefault(nodeData["assetLegalImpactIntegrityMinValue"].ToString()) : 1;
            retval.assetRegulatoryImpactIntegrityMinValue = nodeData["assetRegulatoryImpactIntegrityMinValue"] != null ? ConvertToDoubleOrDefault(nodeData["assetRegulatoryImpactIntegrityMinValue"].ToString()) : 1;
            retval.assetReputationalImpactIntegrityMinValue = nodeData["assetReputationalImpactIntegrityMinValue"] != null ? ConvertToDoubleOrDefault(nodeData["assetReputationalImpactIntegrityMinValue"].ToString()) : 1;
            retval.assetFinancialImpactIntegrityMinValue = nodeData["assetFinancialImpactIntegrityMinValue"] != null ? ConvertToDoubleOrDefault(nodeData["assetFinancialImpactIntegrityMinValue"].ToString()) : 1;
            retval.assetPrivacyImpactAvailabilityMinValue = nodeData["assetPrivacyImpactAvailabilityMinValue"] != null ? ConvertToDoubleOrDefault(nodeData["assetPrivacyImpactAvailabilityMinValue"].ToString()) : 1;
            retval.assetLegalImpactAvailabilityMinValue = nodeData["assetLegalImpactAvailabilityMinValue"] != null ? ConvertToDoubleOrDefault(nodeData["assetLegalImpactAvailabilityMinValue"].ToString()) : 1;
            retval.assetRegulatoryImpactAvailabilityMinValue = nodeData["assetRegulatoryImpactAvailabilityMinValue"] != null ? ConvertToDoubleOrDefault(nodeData["assetRegulatoryImpactAvailabilityMinValue"].ToString()) : 1;
            retval.assetReputationalImpactAvailabilityMinValue = nodeData["assetReputationalImpactAvailabilityMinValue"] != null ? ConvertToDoubleOrDefault(nodeData["assetReputationalImpactAvailabilityMinValue"].ToString()) : 1;
            retval.assetFinancialImpactAvailabilityMinValue = nodeData["assetFinancialImpactAvailabilityMinValue"] != null ? ConvertToDoubleOrDefault(nodeData["assetFinancialImpactAvailabilityMinValue"].ToString()) : 1;
            retval.assetPrivacyImpactAccountabilityMinValue = nodeData["assetPrivacyImpactAccountabilityMinValue"] != null ? ConvertToDoubleOrDefault(nodeData["assetPrivacyImpactAccountabilityMinValue"].ToString()) : 1;
            retval.assetLegalImpactAccountabilityMinValue = nodeData["assetLegalImpactAccountabilityMinValue"] != null ? ConvertToDoubleOrDefault(nodeData["assetLegalImpactAccountabilityMinValue"].ToString()) : 1;
            retval.assetRegulatoryImpactAccountabilityMinValue = nodeData["assetRegulatoryImpactAccountabilityMinValue"] != null ? ConvertToDoubleOrDefault(nodeData["assetRegulatoryImpactAccountabilityMinValue"].ToString()) : 1;
            retval.assetReputationalImpactAccountabilityMinValue = nodeData["assetReputationalImpactAccountabilityMinValue"] != null ? ConvertToDoubleOrDefault(nodeData["assetReputationalImpactAccountabilityMinValue"].ToString()) : 1;
            retval.assetFinancialImpactAccountabilityValue = nodeData["assetFinancialImpactAccountabilityValue"] != null ? ConvertToDoubleOrDefault(nodeData["assetFinancialImpactAccountabilityValue"].ToString()) : 1;
            retval.assetFinancialImpactAccountabilityMinValue = nodeData["assetFinancialImpactAccountabilityMinValue"] != null ? ConvertToDoubleOrDefault(nodeData["assetFinancialImpactAccountabilityMinValue"].ToString()) : 1;

            retval.controlBaseDistribution = nodeData["controlBaseDistribution"] != null ? nodeData["controlBaseDistribution"].ToString() : "";
            retval.controlAssessedDistribution = nodeData["controlAssessedDistribution"] != null ? nodeData["controlAssessedDistribution"].ToString() : "";
            retval.actorAccessDistribution = nodeData["actorAccessDistribution"] != null ? nodeData["actorAccessDistribution"].ToString() : "";
            retval.actorCapabilityDistribution = nodeData["actorCapabilityDistribution"] != null ? nodeData["actorCapabilityDistribution"].ToString() : "";
            retval.actorResourcesDistribution = nodeData["actorResourcesDistribution"] != null ? nodeData["actorResourcesDistribution"].ToString() : "";
            retval.actorMotivationDistribution = nodeData["actorMotivationDistribution"] != null ? nodeData["actorMotivationDistribution"].ToString() : "";
            retval.actorImpactToConfidentialityDistribution = nodeData["actorImpactToConfidentialityDistribution"] != null ? nodeData["actorImpactToConfidentialityDistribution"].ToString() : "";
            retval.actorImpactToIntegrityDistribution = nodeData["actorImpactToIntegrityDistribution"] != null ? nodeData["actorImpactToIntegrityDistribution"].ToString() : "";
            retval.actorImpactToAvailabilityDistribution = nodeData["actorImpactToAvailabilityDistribution"] != null ? nodeData["actorImpactToAvailabilityDistribution"].ToString() : "";
            retval.actorImpactToAccountabilityDistribution = nodeData["actorImpactToAccountabilityDistribution"] != null ? nodeData["actorImpactToAccountabilityDistribution"].ToString() : "";
            retval.vulnerabilityEaseOfExploitationDistribution = nodeData["vulnerabilityEaseOfExploitationDistribution"] != null ? nodeData["vulnerabilityEaseOfExploitationDistribution"].ToString() : "";
            retval.vulnerabilityExposesScopeDistribution = nodeData["vulnerabilityExposesScopeDistribution"] != null ? nodeData["vulnerabilityExposesScopeDistribution"].ToString() : "";
            retval.vulnerabilityInteractionRequiredDistribution = nodeData["vulnerabilityInteractionRequiredDistribution"] != null ? nodeData["vulnerabilityInteractionRequiredDistribution"].ToString() : "";
            retval.vulnerabilityPrivilegesRequiredDistribution = nodeData["vulnerabilityPrivilegesRequiredDistribution"] != null ? nodeData["vulnerabilityPrivilegesRequiredDistribution"].ToString() : "";
            retval.vulnerabilityExposureDistribution = nodeData["vulnerabilityExposureDistribution"] != null ? nodeData["vulnerabilityExposureDistribution"].ToString() : "";
            retval.vulnerabilityImpactToConfidentialityDistribution = nodeData["vulnerabilityImpactToConfidentialityDistribution"] != null ? nodeData["vulnerabilityImpactToConfidentialityDistribution"].ToString() : "";
            retval.vulnerabilityImpactToIntegrityDistribution = nodeData["vulnerabilityImpactToIntegrityDistribution"] != null ? nodeData["vulnerabilityImpactToIntegrityDistribution"].ToString() : "";
            retval.vulnerabilityImpactToAvailabilityDistribution = nodeData["vulnerabilityImpactToAvailabilityDistribution"] != null ? nodeData["vulnerabilityImpactToAvailabilityDistribution"].ToString() : "";
            retval.vulnerabilityImpactToAccountabilityDistribution = nodeData["vulnerabilityImpactToAccountabilityDistribution"] != null ? nodeData["vulnerabilityImpactToAccountabilityDistribution"].ToString() : "";
            retval.attackComplexityDistribution = nodeData["attackComplexityDistribution"] != null ? nodeData["attackComplexityDistribution"].ToString() : "";
            retval.attackProliferationDistribution = nodeData["attackProliferationDistribution"] != null ? nodeData["attackProliferationDistribution"].ToString() : "";
            retval.attackImpactToConfidentialityDistribution = nodeData["attackImpactToConfidentialityDistribution"] != null ? nodeData["attackImpactToConfidentialityDistribution"].ToString() : "";
            retval.attackImpactToIntegrityDistribution = nodeData["attackImpactToIntegrityDistribution"] != null ? nodeData["attackImpactToIntegrityDistribution"].ToString() : "";
            retval.attackImpactToAvailabilityDistribution = nodeData["attackImpactToAvailabilityDistribution"] != null ? nodeData["attackImpactToAvailabilityDistribution"].ToString() : "";
            retval.attackImpactToAccountabilityDistribution = nodeData["attackImpactToAccountabilityDistribution"] != null ? nodeData["attackImpactToAccountabilityDistribution"].ToString() : "";
            retval.assetConfidentialityDistribution = nodeData["assetConfidentialityDistribution"] != null ? nodeData["assetConfidentialityDistribution"].ToString() : "";
            retval.assetIntegrityDistribution = nodeData["assetIntegrityDistribution"] != null ? nodeData["assetIntegrityDistribution"].ToString() : "";
            retval.assetAvailabilityDistribution = nodeData["assetAvailabilityDistribution"] != null ? nodeData["assetAvailabilityDistribution"].ToString() : "";
            retval.assetAccountabilityDistribution = nodeData["assetAccountabilityDistribution"] != null ? nodeData["assetAccountabilityDistribution"].ToString() : "";
            retval.assetPrivacyImpactConfidentialityDistribution = nodeData["assetPrivacyImpactConfidentialityDistribution"] != null ? nodeData["assetPrivacyImpactConfidentialityDistribution"].ToString() : "";
            retval.assetLegalImpactConfidentialityDistribution = nodeData["assetLegalImpactConfidentialityDistribution"] != null ? nodeData["assetLegalImpactConfidentialityDistribution"].ToString() : "";
            retval.assetRegulatoryImpactConfidentialityDistribution = nodeData["assetRegulatoryImpactConfidentialityDistribution"] != null ? nodeData["assetRegulatoryImpactConfidentialityDistribution"].ToString() : "";
            retval.assetReputationalImpactConfidentialityDistribution = nodeData["assetReputationalImpactConfidentialityDistribution"] != null ? nodeData["assetReputationalImpactConfidentialityDistribution"].ToString() : "";
            retval.assetFinancialImpactConfidentialityDistribution = nodeData["assetFinancialImpactConfidentialityDistribution"] != null ? nodeData["assetFinancialImpactConfidentialityDistribution"].ToString() : "";
            retval.assetPrivacyImpactIntegrityDistribution = nodeData["assetPrivacyImpactIntegrityDistribution"] != null ? nodeData["assetPrivacyImpactIntegrityDistribution"].ToString() : "";
            retval.assetLegalImpactIntegrityDistribution = nodeData["assetLegalImpactIntegrityDistribution"] != null ? nodeData["assetLegalImpactIntegrityDistribution"].ToString() : "";
            retval.assetReputationalImpactIntegrityDistribution = nodeData["assetReputationalImpactIntegrityDistribution"] != null ? nodeData["assetReputationalImpactIntegrityDistribution"].ToString() : "";
            retval.assetRegulatoryImpactIntegrityDistribution = nodeData["assetRegulatoryImpactIntegrityDistribution"] != null ? nodeData["assetRegulatoryImpactIntegrityDistribution"].ToString() : "";
            retval.assetFinancialImpactIntegrityDistribution = nodeData["assetFinancialImpactIntegrityDistribution"] != null ? nodeData["assetFinancialImpactIntegrityDistribution"].ToString() : "";
            retval.assetPrivacyImpactAvailabilityDistribution = nodeData["assetPrivacyImpactAvailabilityDistribution"] != null ? nodeData["assetPrivacyImpactAvailabilityDistribution"].ToString() : "";
            retval.assetLegalImpactAvailabilityDistribution = nodeData["assetLegalImpactAvailabilityDistribution"] != null ? nodeData["assetLegalImpactAvailabilityDistribution"].ToString() : "";
            retval.assetReputationalImpactAvailabilityDistribution = nodeData["assetReputationalImpactAvailabilityDistribution"] != null ? nodeData["assetReputationalImpactAvailabilityDistribution"].ToString() : "";
            retval.assetFinancialImpactAvailabilityDistribution = nodeData["assetFinancialImpactAvailabilityDistribution"] != null ? nodeData["assetFinancialImpactAvailabilityDistribution"].ToString() : "";
            retval.assetPrivacyImpactAccountabilityDistribution = nodeData["assetPrivacyImpactAccountabilityDistribution"] != null ? nodeData["assetPrivacyImpactAccountabilityDistribution"].ToString() : "";
            retval.assetLegalImpactAccountabilityDistribution = nodeData["assetLegalImpactAccountabilityDistribution"] != null ? nodeData["assetLegalImpactAccountabilityDistribution"].ToString() : "";
            retval.assetRegulatoryImpactAccountabilityDistribution = nodeData["assetRegulatoryImpactAccountabilityDistribution"] != null ? nodeData["assetRegulatoryImpactAccountabilityDistribution"].ToString() : "";
            retval.assetReputationalImpactAccountabilityDistribution = nodeData["assetReputationalImpactAccountabilityDistribution"] != null ? nodeData["assetReputationalImpactAccountabilityDistribution"].ToString() : "";
            retval.assetFinancialImpactAccountabilityDistribution = nodeData["assetFinancialImpactAccountabilityDistribution"] != null ? nodeData["assetFinancialImpactAccountabilityDistribution"].ToString() : "";
            retval.assetRegulatoryImpactAvailabilityDistribution = nodeData["assetRegulatoryImpactAvailabilityDistribution"] != null ? nodeData["assetRegulatoryImpactAvailabilityDistribution"].ToString() : "";

            retval.ImplementedStrength = nodeData["implementedStrength"] != null ? nodeData["implementedStrength"].ToString() : "";
            retval.BorderWidth = nodeData["borderWidth"] != null ? nodeData["borderWidth"].ToString() : "1";
            retval.Location = nodeData["x"] != null && nodeData["y"] != null ? 
            new Location(Double.Parse(nodeData["x"].ToString()), Double.Parse(nodeData["y"].ToString())):
            new Location(0, 0);
            
            return retval;
                      
        }

        public static double ConvertToDoubleOrDefault(string input)
        {
            if (double.TryParse(input, out double result))
            {
                return result;
            }

            return 0;
        }

        public static Node FromDictionary(IDictionary<String, Object> jsonDict)
        {
            var id = jsonDict["id"].ToString();
            var color = jsonDict["color"].ToString();
            //System.Drawing.Color drwColor = GeneralHelpers.ConvertColorFromHTML(color);
            Node retval = new Node()
            {
                ID = jsonDict["id"].ToString(),
                masterID = jsonDict.ContainsKey("masterID") ? jsonDict["masterID"].ToString() : "",
                Title = jsonDict["title"].ToString(),
                TitleSize = ConvertToDoubleOrDefault(jsonDict["titleSize"].ToString()),
                description = jsonDict["description"].ToString(),
                Note = jsonDict["note"].ToString(),
                Domain = jsonDict["domain"].ToString(),
                SubDomain = jsonDict["subdomain"].ToString(),
                ReferenceURL = jsonDict["refurl"].ToString(),
                frameworkReference = jsonDict["frameworkReference"].ToString(),
                ControlFrameworkVersion = jsonDict["frameworkNameVersion"].ToString(),
                frameworkName = jsonDict["frameworkName"].ToString(),
                Category = jsonDict["category"].ToString(),
                PrimaryCategory = jsonDict.ContainsKey("primaryCategory") ? jsonDict["primaryCategory"].ToString() : "control",
                SubCategory = jsonDict.ContainsKey("subCategory") ? jsonDict["subCategory"].ToString() : "",
                Level = jsonDict["level"].ToString(),
                Enabled = jsonDict["enabled"].ToString().ToLower() == "true",
                //HTMLColor = jsonDict["color"].ToString(),
                HTMLOpacity = ConvertToDoubleOrDefault(jsonDict["opacity"].ToString()),
                Color = GeneralHelpers.ConvertColorFromHTML(jsonDict["color"].ToString()),
                BorderColor = jsonDict.ContainsKey("borderColor") ? 
                    GeneralHelpers.ConvertColorFromHTML(jsonDict["borderColor"].ToString()) : System.Drawing.Color.Black,
                BorderWidth = jsonDict["borderWidth"].ToString(),
                TitleTextColor = GeneralHelpers.ConvertColorFromHTML(jsonDict["titleTextColor"].ToString()),
                Shape = NodeShapes.NodesShapes.FirstOrDefault(item => item.Shape == jsonDict["shape"].ToString()),
                Size = ConvertToDoubleOrDefault(jsonDict["height"].ToString()),
                Type = NodeTypes.NodesTypes.FirstOrDefault(item => item.Type == jsonDict["nodeType"].ToString()),
                
                controlBaseScore = ConvertToDoubleOrDefault(jsonDict["controlBaseScore"].ToString()),
                controlBaseValue = ConvertToDoubleOrDefault(jsonDict["controlBaseValue"].ToString()),
                controlBaseMinValue = ConvertToDoubleOrDefault(jsonDict["controlBaseMinValue"].ToString()),
                controlAssessedMinValue = ConvertToDoubleOrDefault(jsonDict["controlAssessedMinValue"].ToString()),
                controlAssessedValue = ConvertToDoubleOrDefault(jsonDict["controlAssessedValue"].ToString()),

                CalculatedValue = ConvertToDoubleOrDefault(jsonDict["calculatedValue"].ToString()),
                
                actorCapabilityText = jsonDict["actorCapability"] != null ? jsonDict["actorCapability"].ToString() : "",
                actorResourcesText = jsonDict["actorResources"] != null ? jsonDict["actorResources"].ToString() : "",
                actorMotivationText = jsonDict["actorMotivation"] != null ? jsonDict["actorMotivation"].ToString() : "",
                actorAccessText = jsonDict["actorAccess"] != null ? jsonDict["actorAccess"].ToString() : "",
                actorImpactToConfidentialityText = jsonDict["actorImpactToConfidentiality"] != null ? jsonDict["actorImpactToConfidentiality"].ToString() : "",
                actorImpactToIntegrityText = jsonDict["actorImpactToIntegrity"] != null ? jsonDict["actorImpactToIntegrity"].ToString() : "",
                actorImpactToAvailabilityText = jsonDict["actorImpactToAvailability"] != null ? jsonDict["actorImpactToAvailability"].ToString() : "",
                actorImpactToAccountabilityText = jsonDict["actorImpactToAccountability"] != null ? jsonDict["actorImpactToAccountability"].ToString() : "",
                actorScore = jsonDict["actorScore"] != null ? ConvertToDoubleOrDefault(jsonDict["actorScore"].ToString()) : 1,

                attackComplexityText = jsonDict["attackComplexity"] != null ? jsonDict["attackComplexity"].ToString() : "",
                attackProliferationText = jsonDict["attackProliferation"] != null ? jsonDict["attackProliferation"].ToString() : "",
                attackImpactToConfidentialityText = jsonDict["attackImpactToConfidentiality"] != null ? jsonDict["attackImpactToConfidentiality"].ToString() : "",
                attackImpactToIntegrityText = jsonDict["attackImpactToIntegrity"] != null ? jsonDict["attackImpactToIntegrity"].ToString() : "",
                attackImpactToAvailabilityText = jsonDict["attackImpactToAvailability"] != null ? jsonDict["attackImpactToAvailability"].ToString() : "",
                attackImpactToAccountabilityText = jsonDict["attackImpactToAccountability"] != null ? jsonDict["attackImpactToAccountability"].ToString() : "",
                attackScore = jsonDict["attackScore"] != null ? ConvertToDoubleOrDefault(jsonDict["attackScore"].ToString()) : 1,

                assetConfidentialityText = jsonDict["assetConfidentiality"] != null ? jsonDict["assetConfidentiality"].ToString() : "",
                assetIntegrityText = jsonDict["assetIntegrity"] != null ? jsonDict["assetIntegrity"].ToString() : "",
                assetAvailabilityText = jsonDict["assetAvailability"] != null ? jsonDict["assetAvailability"].ToString() : "",
                assetAccountabilityText = jsonDict["assetAccountability"] != null ? jsonDict["assetAccountability"].ToString() : "",
                assetScore = jsonDict["assetScore"] != null ? ConvertToDoubleOrDefault(jsonDict["assetScore"].ToString()) : 1,

                controlBaseScoreAssessmentStatus = jsonDict.ContainsKey("controlBaseScoreAssessmentStatus") ? 
                    jsonDict["controlBaseScoreAssessmentStatus"].ToString() : "",
                AssessedStatus = jsonDict.ContainsKey("AssessedStatus") ? 
                    jsonDict["AssessedStatus"].ToString() : "",
                NodeImageData = jsonDict.ContainsKey("image") ? jsonDict["image"].ToString() : "",
                ImagePath =  jsonDict.ContainsKey("imagePath") ? jsonDict["imagePath"].ToString() : "",
                Location = new Location(Double.Parse(jsonDict["x"].ToString()), Double.Parse(jsonDict["y"].ToString()))
            };
            return retval;
        }

    }

}