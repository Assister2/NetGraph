using CyConex.Graph;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Syncfusion.WinForms.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using System.Linq;
using CefSharp;
using CyConex.Chromium;
using CefSharp.DevTools.DOMSnapshot;
using CyConex.Helpers;
using CyConex;
using Syncfusion.Windows.Forms.Tools;
using CyConex.Forms;
using static System.Net.Mime.MediaTypeNames;
using System.Threading.Tasks;
using Syncfusion.XlsIO;
using Troschuetz.Random;

namespace CyConex
{
    public partial class NodeDistributionsForm : SfForm
    {

        public MainForm mainForm;
        public int form_flag = -1;
        public NodeDistributionsForm()
        {
            InitializeComponent();
        }

        private void NodeControlForm_Load(object sender, EventArgs e)
        {
            InitNodeControlPanels();
        }

        public void InitNodeControlPanels()
        {
            this.panelNodeControlDetails.Dock = DockStyle.Fill;
            this.panelActorNodeDistributionValues.Dock = DockStyle.Fill;
            this.panelVulnerabilityNodeDistributionValues.Dock = DockStyle.Fill;
            this.panelAssetNodeDistributionValues.Dock = DockStyle.Fill;    
            this.panelAttackNodeDistributionValues.Dock = DockStyle.Fill;
            this.panelEdgeDistributionValues.Dock = DockStyle.Fill;    
            this.panelObjectiveNodeValues.Dock = DockStyle.Fill;
            this.panelNodeGroupDetails.Dock = DockStyle.Fill;
            this.panelEvidenceNodeValues.Dock = DockStyle.Fill;
        }

        public Panel GetPanelFromName(string name)
        {
            Panel panel = null;
            switch (name)
            {
                case "actor":
                    panel = panelActorNodeDistributionValues;
                    break;
                case "attack":
                    panel = panelAttackNodeDistributionValues;
                    break;
                case "asset":
                    panel = panelAssetNodeDistributionValues;
                    break;
                case "asset-group":
                    panel = panelAssetNodeDistributionValues;
                    break;
                case "vulnerability":
                    panel = panelVulnerabilityNodeDistributionValues;
                    break;
                case "vulnerability-group":
                    panel = panelNodeVulnerabilityGroup;
                    break;
                case "control":
                    panel = panelNodeControlDetails;
                    break;
                case "objective":
                    panel = panelObjectiveNodeValues;
                    break;
                case "group":
                    panel = panelNodeGroupDetails;
                    break;
                case "evidence":
                    panel = panelEvidenceNodeValues;
                    break;
            }
            return panel;
        }

        public void HideNodeDistributionPanels(string panelName)
        {
            String targetPanel = "";
            //if (panelName != null)
            //    targetPanel = GetPanelFromName(panelName).ToString();

            if (panelActorNodeDistributionValues.Visible && panelName != "actor")
                panelActorNodeDistributionValues.Visible = false;

            if (panelAttackNodeDistributionValues.Visible && panelName != "attack")
                panelAttackNodeDistributionValues.Visible = false;

            if (panelAssetNodeDistributionValues.Visible && (panelName != "asset" && panelName != "asset-group"))
                panelAssetNodeDistributionValues.Visible = false;

            if (panelVulnerabilityNodeDistributionValues.Visible && panelName != "vulnerability")
                panelVulnerabilityNodeDistributionValues.Visible = false;

            if (panelNodeControlDetails.Visible && panelName != "control")
                panelNodeControlDetails.Visible = false;

            if (panelObjectiveNodeValues.Visible && panelName != "objective")
                panelObjectiveNodeValues.Visible = false;

            if (panelNodeGroupDetails.Visible && panelName != "group")
                panelNodeGroupDetails.Visible = false;

            if (panelNodeVulnerabilityGroup.Visible && panelName != "vulnerability-group")
                panelNodeVulnerabilityGroup.Visible = false;

            if (panelEvidenceNodeValues.Visible && panelName != "evidence")
                panelEvidenceNodeValues.Visible = false;

        }

        public void updateNodeAttribute(string type, Node node)
        {

            switch (type)
            {
                case "asset":
                case "asset-group":
                    lblAssetConfMin.Text = node.assetConfidentialityMinValue.ToString();
                    lblAssetConfMax.Text = node.assetConfidentialityValue.ToString();
                    lblAssetConfDistribution.Text = GraphUtil.GetDistributionType((JArray)GraphUtil.DeserializeJArrayOrDefault(node.assetConfidentialityDistribution));

                    lblAssetIntegMin.Text = node.assetIntegrityMinValue.ToString();
                    lblAssetIntegMax.Text = node.assetIntegrityValue.ToString();
                    lblAssetIntegDistribution.Text = GraphUtil.GetDistributionType((JArray)GraphUtil.DeserializeJArrayOrDefault(node.assetIntegrityDistribution));

                    lblAssetAvailMin.Text = node.assetAvailabilityMinValue.ToString();
                    lblAssetAvailMax.Text = node.assetAvailabilityValue.ToString();
                    lblAssetAvailDistribution.Text = GraphUtil.GetDistributionType((JArray)GraphUtil.DeserializeJArrayOrDefault(node.assetAvailabilityDistribution));

                    lblAssetAccountMin.Text = node.assetAccountabilityMinValue.ToString();
                    lblAssetAccountMax.Text = node.assetAccountabilityValue.ToString();
                    lblAssetAccountDistribution.Text = GraphUtil.GetDistributionType((JArray)GraphUtil.DeserializeJArrayOrDefault(node.assetAccountabilityDistribution));

                    if (GraphUtil.IsParentofChildNode(node.ID))
                    {
                        this.InvokeIfNeed(() =>
                        {
                            string nodeBehaviour = GraphUtil.GetnodeBehaviour(node.ID);
                            this.cbNodeAssetBehaviour.SelectedIndex = cbNodeAssetBehaviour.Items.IndexOf(nodeBehaviour);
                        });
                        panelChildAssetBehaviour.Visible = true;
                    }
                    else
                    {
                        panelChildAssetBehaviour.Visible = false;
                    }


                    //if (cmbImpactFrom.SelectedIndex < 0) return;
                    //switch (cmbImpactFrom.Items[cmbImpactFrom.SelectedIndex].ToString())
                    //{
                    //    case "Confidentiality":
                    //        lblAssetFinMin.Text = node.assetFinancialImpactConfidentialityMinValue.ToString();
                    //        lblAssetFinMax.Text = node.assetFinancialImpactConfidentialityValue.ToString();

                    //        lblAssetRepMin.Text = node.assetReputationalImpactConfidentialityMinValue.ToString();
                    //        lblAssetRepMax.Text = node.assetReputationalImpactConfidentialityValue.ToString();

                    //        lblAssetRegMin.Text = node.assetRegulatoryImpactConfidentialityMinValue.ToString();
                    //        lblAssetRegMax.Text = node.assetRegulatoryImpactConfidentialityValue.ToString();

                    //        lblAssetLegalMin.Text = node.assetLegalImpactConfidentialityMinValue.ToString();
                    //        lblAssetLegalMax.Text = node.assetLegalImpactConfidentialityValue.ToString();

                    //        lblAssetPrivMin.Text = node.assetPrivacyImpactConfidentialityMinValue.ToString();
                    //        lblAssetPrivMax.Text = node.assetPrivacyImpactConfidentialityValue.ToString();
                    //        break;

                    //    case "Integrity":
                    //        lblAssetFinMin.Text = node.assetFinancialImpactIntegrityMinValue.ToString();
                    //        lblAssetRepMin.Text = node.assetReputationalImpactIntegrityMinValue.ToString();

                    //        lblAssetRepMax.Text = node.assetReputationalImpactIntegrityValue.ToString();
                    //        lblAssetRegMin.Text = node.assetRegulatoryImpactIntegrityMinValue.ToString();

                    //        lblAssetRegMax.Text = node.assetRegulatoryImpactIntegrityValue.ToString();
                    //        lblAssetLegalMin.Text = node.assetLegalImpactIntegrityMinValue.ToString();

                    //        lblAssetFinMax.Text = node.assetFinancialImpactIntegrityValue.ToString();
                    //        lblAssetLegalMax.Text = node.assetLegalImpactIntegrityValue.ToString();

                    //        lblAssetPrivMin.Text = node.assetPrivacyImpactIntegrityMinValue.ToString();
                    //        lblAssetPrivMax.Text = node.assetPrivacyImpactIntegrityValue.ToString();
                    //        break;

                    //    case "Availability":
                    //        lblAssetFinMin.Text = node.assetFinancialImpactAvailabilityMinValue.ToString();
                    //        lblAssetFinMax.Text = node.assetFinancialImpactAvailabilityValue.ToString();

                    //        lblAssetRepMin.Text = node.assetReputationalImpactAvailabilityMinValue.ToString();
                    //        lblAssetRepMax.Text = node.assetReputationalImpactAvailabilityValue.ToString();

                    //        lblAssetRegMin.Text = node.assetRegulatoryImpactAvailabilityMinValue.ToString();
                    //        lblAssetRegMax.Text = node.assetRegulatoryImpactAvailabilityValue.ToString();

                    //        lblAssetLegalMin.Text = node.assetLegalImpactAvailabilityMinValue.ToString();
                    //        lblAssetLegalMax.Text = node.assetLegalImpactAvailabilityValue.ToString();

                    //        lblAssetPrivMin.Text = node.assetPrivacyImpactAvailabilityMinValue.ToString();
                    //        lblAssetPrivMax.Text = node.assetPrivacyImpactAvailabilityValue.ToString();
                    //        break;

                    //    case "Accountability":

                    //        lblAssetFinMin.Text = node.assetFinancialImpactAccountabilityMinValue.ToString();
                    //        lblAssetFinMax.Text = node.assetFinancialImpactAccountabilityValue.ToString();

                    //        lblAssetRepMin.Text = node.assetReputationalImpactAccountabilityMinValue.ToString();
                    //        lblAssetRepMax.Text = node.assetReputationalImpactAccountabilityValue.ToString();

                    //        lblAssetRegMin.Text = node.assetRegulatoryImpactAccountabilityMinValue.ToString();
                    //        lblAssetRegMax.Text = node.assetRegulatoryImpactAccountabilityValue.ToString();

                    //        lblAssetLegalMin.Text = node.assetLegalImpactAccountabilityMinValue.ToString();
                    //        lblAssetLegalMax.Text = node.assetLegalImpactAccountabilityValue.ToString();

                    //        lblAssetPrivMin.Text = node.assetPrivacyImpactAccountabilityMinValue.ToString();
                    //        lblAssetPrivMax.Text = node.assetPrivacyImpactAccountabilityValue.ToString();
                    //        break;
                    //}
                    break;

                case "attack":

                    lblAttackComplexityMin.Text = node.attackComplexityMinValue.ToString();
                    lblAttackComplexityMax.Text = node.attackComplexityValue.ToString();
                    lblAttackComplexityDistribution.Text = GraphUtil.GetDistributionType((JArray)GraphUtil.DeserializeJArrayOrDefault(node.attackComplexityDistribution));

                    lblAttackProliferationMin.Text = node.attackProliferationMinValue.ToString();
                    lblAttackProliferationMax.Text = node.attackProliferationValue.ToString();
                    lblAttackProliferationDistribution.Text = GraphUtil.GetDistributionType((JArray)GraphUtil.DeserializeJArrayOrDefault(node.attackProliferationDistribution));

                    lblAttackImpactsConfMax.Text = node.attackImpactToConfidentialityValue.ToString();
                    lblAttackImpactsConfMin.Text = node.attackImpactToConfidentialityMinValue.ToString();

                    lblAttackImpactsIntegMax.Text = node.attackImpactToIntegrityValue.ToString();
                    lblAttackImpactsIntegMin.Text = node.attackImpactToIntegrityMinValue.ToString();

                    lblAttackImpactsAvailMax.Text = node.attackImpactToAvailabilityValue.ToString();
                    lblAttackImpactsAvailMin.Text = node.attackImpactToAvailabilityMinValue.ToString();

                    lblAttackImpactsAccountMax.Text = node.attackImpactToAccountabilityValue.ToString();
                    lblAttackImpactsAccountMin.Text = node.attackImpactToAccountabilityMinValue.ToString();

                    break;
                case "actor":

                    lblActorAccessMin.Text = node.actorAccessMinValue.ToString();
                    lblActorAccessMax.Text = node.actorAccessValue.ToString();
                    lblActorAccessDistribution.Text = GraphUtil.GetDistributionType((JArray)GraphUtil.DeserializeJArrayOrDefault(node.actorAccessDistribution));

                    lblActorCapabilityMin.Text = node.actorCapabilityMinValue.ToString();
                    lblActorCapabilityMax.Text = node.actorCapabilityValue.ToString();
                    lblActorAccessDistribution.Text = GraphUtil.GetDistributionType((JArray)GraphUtil.DeserializeJArrayOrDefault(node.actorCapabilityDistribution));

                    lblActorMotivationMin.Text = node.actorMotivationMinValue.ToString();
                    lblActorMotivationMax.Text = node.actorMotivationValue.ToString();
                    lblActorMotivationDistribution.Text = GraphUtil.GetDistributionType((JArray)GraphUtil.DeserializeJArrayOrDefault(node.actorMotivationDistribution));

                    lblActorResourcesMin.Text = node.actorResourcesMinValue.ToString();
                    lblActorResourcesMax.Text = node.actorResourcesValue.ToString();
                    lblActorResourcesDistribution.Text = GraphUtil.GetDistributionType((JArray)GraphUtil.DeserializeJArrayOrDefault(node.actorResourcesDistribution));

                    lblActorImpactsConfMax.Text = node.actorImpactToConfidentialityValue.ToString();
                    lblActorImpactsConfMin.Text = node.actorImpactToConfidentialityMinValue.ToString();
                    

                    lblActorImpactsIntegMax.Text = node.actorImpactToIntegrityValue.ToString();
                    lblActorImpactsIntegMin.Text = node.actorImpactToIntegrityMinValue.ToString();
                   

                    lblActorImpactsAvailMax.Text = node.actorImpactToAvailabilityValue.ToString();
                    lblActorImpactsAvailMin.Text = node.actorImpactToAvailabilityMinValue.ToString();
                   

                    lblActorImpactsAccountMax.Text = node.actorImpactToAccountabilityValue.ToString();
                    lblActorImpactsAccountMin.Text = node.actorimpactToAccountabilityMinValue.ToString();
                    lblActorAccessDistribution.Text = GraphUtil.GetDistributionType((JArray)GraphUtil.DeserializeJArrayOrDefault(node.actorAccessDistribution));
                    break;

                case "vulnerability":

                    lblVulnEaseofExploitMin.Text = node.vulnerabilityEaseOfExploitationMinValue.ToString();
                    lblVulnEaseofExploitMax.Text = node.vulnerabilityEaseOfExploitationValue.ToString();
                    lblVulnEaseofExploitDistribution.Text = GraphUtil.GetDistributionType((JArray)GraphUtil.DeserializeJArrayOrDefault(node.vulnerabilityEaseOfExploitationDistribution));

                    lblVulnExposureMin.Text = node.vulnerabilityExposureMinValue.ToString();
                    lblVulnExposureMax.Text = node.vulnerabilityExposureValue.ToString();
                    lblVulnExposureDistribution.Text = GraphUtil.GetDistributionType((JArray)GraphUtil.DeserializeJArrayOrDefault(node.vulnerabilityExposureDistribution));

                    lblVulnPrivRequiredMin.Text = node.vulnerabilityPrivilegesRequiredMinValue.ToString();
                    lblVulnPrivRequiredMax.Text = node.vulnerabilityPrivilegesRequiredValue.ToString();
                    lblVulnPrivRequiredDistribution.Text = GraphUtil.GetDistributionType((JArray)GraphUtil.DeserializeJArrayOrDefault(node.vulnerabilityPrivilegesRequiredDistribution));

                    lblVulnInteractionRequiredMin.Text = node.vulnerabilityInteractionRequiredMinValue.ToString();
                    lblVulnInteractionRequiredMax.Text = node.vulnerabilityInteractionRequiredValue.ToString();
                    lblVulnInteractionRequiredDistribution.Text = GraphUtil.GetDistributionType((JArray)GraphUtil.DeserializeJArrayOrDefault(node.vulnerabilityInteractionRequiredDistribution));

                    lblVulnExposesScopeMin.Text = node.vulnerabilityExposesScopeMinValue.ToString();
                    lblVulnExposesScopeMax.Text = node.vulnerabilityExposesScopeValue.ToString();
                    lblVulnExposesScopeDistribution.Text = GraphUtil.GetDistributionType((JArray)GraphUtil.DeserializeJArrayOrDefault(node.vulnerabilityExposesScopeDistribution));

                    lblVulnImpactsConfMax.Text = node.vulnerabilityImpactToConfidentialityValue.ToString();
                    lblVulnImpactsConfMin.Text = node.vulnerabilityImpactToConfidentialityMinValue.ToString();
                  

                    lblVulnImpactsIntegMax.Text = node.vulnerabilityImpactToIntegrityValue.ToString();
                    lblVulnImpactsIntegMin.Text = node.vulnerabilityImpactToIntegrityMinValue.ToString();


                    lblVulnImpactsAvailMax.Text = node.vulnerabilityImpactToAvailabilityValue.ToString();
                    lblVulnImpactsAvailMin.Text = node.vulnerabilityImpactToAvailabilityMinValue.ToString();
                   

                    lblVulnImpactsAccountMax.Text = node.vulnerabilityImpactToAccountabilityValue.ToString();
                    lblVulnImpactsAccountMin.Text = node.vulnerabilityImpactToAccountabilityMinValue.ToString();
                   

                    break;

                case "control":

                    lblControlStrenghMin.Text = node.controlBaseMinValue.ToString();
                    lblControlStrenghMax.Text = node.controlBaseValue.ToString();

                    lblControlImplementationMin.Text = node.controlAssessedMinValue.ToString();
                    lblControlImplementationMax.Text = node.controlAssessedValue.ToString();

                    break;
            }
        }

        public void ShowNodeDistributionPanel(string panelName)
        {
            
            Panel panel = GetPanelFromName(panelName);
            // Show the requested targetPanel
            if (panel == null) return;
            if (!panel.Visible)
                panel.Visible = true;
            panel.BringToFront();

            HideNodeDistributionPanels(panelName);
           
        }

        private void btnActorAccess_Click(object sender, EventArgs e)
        {
           
        }

        private void btnActorCapability_Click(object sender, EventArgs e)
        {
            
        }

        private void btnActorMotivation_Click(object sender, EventArgs e)
        {
            
        }

        private void btnActorResources_Click(object sender, EventArgs e)
        {
           
        }

        private void btnAttackComplex_Click(object sender, EventArgs e)
        {
           
        }

        private void btnAttackProlif_Click(object sender, EventArgs e)
        {
            
        }

        private void label74_Click(object sender, EventArgs e)
        {

        }
        private void btnVulnEaseofExploit_Click(object sender, EventArgs e)
        {
           
        }

        private void btnVulnExposure_Click(object sender, EventArgs e)
        {
           
        }

        private void btnVulnPrivileges_Click(object sender, EventArgs e)
        {
           
        }

        private void btnVulnInteraction_Click(object sender, EventArgs e)
        {
            
        }

        private void btnVulnExposesScope_Click(object sender, EventArgs e)
        {
            
        }

        private void btnAssetConf_Click(object sender, EventArgs e)
        {
           
        }

        private void btnAssetInteg_Click(object sender, EventArgs e)
        {
           

        }

        private void btnAssetAvail_Click(object sender, EventArgs e)
        {
           

        }

        private void btnAssetAccount_Click(object sender, EventArgs e)
        {
            

        }

        private void btnControlStrength_Click(object sender, EventArgs e)
        {
           
        }

        private void btnControlImplementtion_Click(object sender, EventArgs e)
        {
            
        }

        private void btnActorImpactsConf_Click(object sender, EventArgs e)
        {
            //showNodeRangeSelection(mainForm._settings.NodeAttrActor["impacts_confidentialityiality"], "actorImpactToConfidentiality", label15, lblActorImpactsConfMax, lblActorImpactsConfMin, "node");
        }

        private void btnActorImpactsInteg_Click(object sender, EventArgs e)
        {
           // showNodeRangeSelection(mainForm._settings.NodeAttrActor["impacts_integrity"], "actorImpactToIntegrity", label20, lblActorImpactsIntegMax, lblActorImpactsIntegMin, "node");
        }

        private void btnActorImpactsAvail_Click(object sender, EventArgs e)
        {
          //  showNodeRangeSelection(mainForm._settings.NodeAttrActor["impacts_accountability"], "actorImpactToAvailability", label40, lblActorImpactsAvailMax, lblActorImpactsAvailMin, "node");
        }

        private void btnActorImpactsAccount_Click(object sender, EventArgs e)
        {
         //   showNodeRangeSelection(mainForm._settings.NodeAttrActor["impacts_availability"], "actorImpactToAccountability", label107, lblActorImpactsAccountMax, lblActorImpactsAccountMin, "node");
        }

        private void lblAttackImpactsAccountMax_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click_1(object sender, EventArgs e)
        {
        //showNodeRangeSelection(mainForm._settings.NodeAttrAttack["impacts_confidentialityiality"], "attackImpactToConfidentiality", label13, lblAttackImpactsConfMax, lblAttackImpactsConfMin, "node");
        }

        private void button7_Click(object sender, EventArgs e)
        {
            //showNodeRangeSelection(mainForm._settings.NodeAttrAttack["impacts_integrity"], "attackImpactToIntegrity", label29, lblAttackImpactsIntegMax, lblAttackImpactsIntegMin, "node");
        }

        private void button8_Click(object sender, EventArgs e)
        {
           // showNodeRangeSelection(mainForm._settings.NodeAttrAttack["impacts_availability"], "attackImpactToAvailability", label110, lblAttackImpactsAvailMax, lblAttackImpactsAvailMin, "node");
        }

        private void button9_Click(object sender, EventArgs e)
        {
          //  showNodeRangeSelection(mainForm._settings.NodeAttrAttack["impacts_accountability"], "attackImpactToAccountability", label115, lblAttackImpactsAccountMax, lblAttackImpactsAccountMin, "node");
        }

        private void button10_Click(object sender, EventArgs e)
        {
          //  showNodeRangeSelection(mainForm._settings.NodeAttrVulnerability["impacts_confidentiality"], "vulnerabilityImpactToConfidentiality", label16, lblVulnImpactsConfMax, lblVulnImpactsConfMin, "node");
        }

        private void button11_Click(object sender, EventArgs e)
        {
          //  showNodeRangeSelection(mainForm._settings.NodeAttrVulnerability["impacts_integrity"], "vulnerabilityImpactToIntegrity", label108, lblVulnImpactsIntegMax, lblVulnImpactsIntegMin, "node");
        }

        private void button12_Click(object sender, EventArgs e)
        {
            //showNodeRangeSelection(mainForm._settings.NodeAttrVulnerability["impacts_availability"], "vulnerabilityImpactToAvailability", label118, lblVulnImpactsAvailMax, lblVulnImpactsAvailMin, "node");
        }

        private void button13_Click(object sender, EventArgs e)
        {
          //  showNodeRangeSelection(mainForm._settings.NodeAttrVulnerability["impacts_accountability"], "vulnerabilityImpactToAccountability", label123, lblVulnImpactsAccountMax, lblVulnImpactsAccountMin, "node");
        }

        private void btnAssetFinImpact_Click(object sender, EventArgs e)
        {
          //  showNodeRangeSelection(mainForm._settings.NodeAttrAsset["financial_impact"], "assetFinancialImpact" + cmbImpactFrom.Items[cmbImpactFrom.SelectedIndex].ToString(), label25, lblAssetFinMax, lblAssetFinMin, "node");
        }

        private void button15_Click(object sender, EventArgs e)
        {
         //   showNodeRangeSelection(mainForm._settings.NodeAttrAsset["reputational_impact"], "assetReputationalImpact" + cmbImpactFrom.Items[cmbImpactFrom.SelectedIndex].ToString(), label119, lblAssetRepMax, lblAssetRepMin, "node");
        }

        private void button16_Click(object sender, EventArgs e)
        {
         //   showNodeRangeSelection(mainForm._settings.NodeAttrAsset["regulatory_impact"], "assetRegulatoryImpact" + cmbImpactFrom.Items[cmbImpactFrom.SelectedIndex].ToString(), label127, lblAssetRegMax, lblAssetRegMin, "node");
        }

        private void button17_Click(object sender, EventArgs e)
        {
        //    showNodeRangeSelection(mainForm._settings.NodeAttrAsset["legal_impact"], "assetLegalImpact" + cmbImpactFrom.Items[cmbImpactFrom.SelectedIndex].ToString(), label132, lblAssetLegalMax, lblAssetLegalMin, "node");
        }

        private void button18_Click(object sender, EventArgs e)
        {
       //     showNodeRangeSelection(mainForm._settings.NodeAttrAsset["privacy_impact"], "assetPrivacyImpact" + cmbImpactFrom.Items[cmbImpactFrom.SelectedIndex].ToString(), label136, lblAssetPrivMax, lblAssetPrivMin, "node");
        }

        private void showNodeRangeSelection(JToken data, string NodeAttribute, string lableHead, Label lblMax, Label lblMin, string elementType, Label lblDistribution)
        {
            if (form_flag == 1)
            {
                showNodeRangeSelectionMainForm(data, NodeAttribute, lableHead, lblMax, lblMin, elementType, lblDistribution);
            }
            else if (form_flag == 2)
            {
                showNodeRangeSelectionNodeForm(data, NodeAttribute, lableHead, lblMax, lblMin, elementType, lblDistribution);
            }

            UpdateEdgeStrengthDisplayValue();
        }

        private void showNodeRangeSelectionMainForm(JToken data, string NodeAttribute, string lableHead, Label lblMax, Label lblMin, string elementType, Label lblDistribution)
        {
            if (data == null || data.Count() == 0)
            {
                return;
            }
            
            JArray orderedArrayDescending = new JArray();
            //Sort the JArray by value decending 
            try
            {
                orderedArrayDescending = new JArray(data.OrderByDescending(x => (double)x.SelectToken("value")));
            }
            catch { }

            if (orderedArrayDescending.Count > 1)
            {
                RangeSelectForm rangeForm = new RangeSelectForm();
                rangeForm.data = orderedArrayDescending;
                rangeForm.Text1 = lableHead;
                rangeForm.Range1Maximum = orderedArrayDescending.Count;

                //Set current values
                int count = 0;
                int col = 0;


                rangeForm.RangeLimitMax = orderedArrayDescending.Count;
                rangeForm.RangeLimitMin = 1;
                rangeForm.elementType = elementType;

                if (mainForm._selectedNodes.Count() > 0 && mainForm._selectedNodes.ElementAt(0).Value != null)
                {
                    //Node selected
                    Node node = mainForm._selectedNodes.ElementAt(0).Value;
                    // Get the PropertyInfo object
                    PropertyInfo propertyInfo = node.GetType().GetProperty(NodeAttribute + "Distribution");

                    if (propertyInfo != null)
                    {
                        // Get the value of the property
                        string value = (string)propertyInfo.GetValue(node);
                        if (value != null && value != "")
                            rangeForm.DistributionParameters = (JArray)JsonConvert.DeserializeObject(value);
                        else
                            rangeForm.DistributionParameters = new JArray();
                    }
                    else
                    {
                        rangeForm.DistributionParameters = new JArray();
                    }
                }
                else
                {
                    if (mainForm._selectedEdges.Count() > 0 && mainForm._selectedEdges.ElementAt(0).Value != null )
                    {
                        //Edge selected
                        Edge edge = mainForm._selectedEdges.ElementAt(0).Value;
                        // Get the PropertyInfo object
                        PropertyInfo propertyInfo = edge.GetType().GetProperty(NodeAttribute + "Distribution");

                        if (propertyInfo != null)
                        {
                            // Get the value of the property
                            string value = (string)propertyInfo.GetValue(edge);
                            if (value != null && value != "")
                                rangeForm.DistributionParameters = (JArray)JsonConvert.DeserializeObject(value);
                            else
                                rangeForm.DistributionParameters = new JArray();
                        }
                        else
                        {
                            rangeForm.DistributionParameters = new JArray();
                        }
                    }
                }

                if (rangeForm.DistributionParameters != null && rangeForm.DistributionParameters.Count > 0)  // Set range pointers to existing values
                { 

                    for (int i = orderedArrayDescending.Count - 1; i > -1; i--)
                    {

                        count++;
                        col = (orderedArrayDescending.Count - count) + 1;
                        var item = orderedArrayDescending[i] as JToken;


                        if (item["value"].Value<double>().ToString("F2") == lblMax.Text || item["value"].ToString() == lblMax.Text)
                            rangeForm.SelectedRangeMax = count;


                        if (item["value"].Value<double>().ToString("F2") == lblMin.Text || item["value"].ToString() == lblMin.Text)
                            rangeForm.SelectedRangeMin = count;

                    }
                }
                else  // New distribution, so set range pointers to min and max
                {
                    rangeForm.SelectedRangeMax = rangeForm.RangeLimitMax;
                    rangeForm.SelectedRangeMin = rangeForm.RangeLimitMin;
                }



                //Show the dialog and get the results
                if (rangeForm.ShowDialog(this) == DialogResult.OK)
                {
                    lblMax.Text = orderedArrayDescending[orderedArrayDescending.Count - rangeForm.SelectedRangeMax]["value"].ToString();
                    lblMin.Text = orderedArrayDescending[orderedArrayDescending.Count - rangeForm.SelectedRangeMin]["value"].ToString();
                    lblDistribution.Text = rangeForm.distributionType;

                    if (rangeForm.DistributionParameters[4].ToString() == "node")
                    {
                        mainForm.saveNodeAttribute(NodeAttribute,
                            orderedArrayDescending[orderedArrayDescending.Count - rangeForm.SelectedRangeMin]["impact"].ToString(),
                            orderedArrayDescending[orderedArrayDescending.Count - rangeForm.SelectedRangeMin]["value"].ToString(),
                            orderedArrayDescending[orderedArrayDescending.Count - rangeForm.SelectedRangeMax]["impact"].ToString(),
                            orderedArrayDescending[orderedArrayDescending.Count - rangeForm.SelectedRangeMax]["value"].ToString(),
                            rangeForm.DistributionParameters);
                    }
                    else
                    {
                        mainForm.saveNodeAttribute(NodeAttribute,
                            orderedArrayDescending[orderedArrayDescending.Count - rangeForm.SelectedRangeMin]["strength"].ToString(),
                            orderedArrayDescending[orderedArrayDescending.Count - rangeForm.SelectedRangeMin]["value"].ToString(),
                            orderedArrayDescending[orderedArrayDescending.Count - rangeForm.SelectedRangeMax]["strength"].ToString(),
                            orderedArrayDescending[orderedArrayDescending.Count - rangeForm.SelectedRangeMax]["value"].ToString(),
                            rangeForm.DistributionParameters);
                    }
                }
            }
            else if (orderedArrayDescending.Count > 0)
            {
                lblMax.Text = orderedArrayDescending[0]["value"].ToString();
                lblMin.Text = orderedArrayDescending[0]["value"].ToString();
            }
            else
            {
                lblMax.Text = "-";
                lblMin.Text = "-";
            }

        }

        private void showNodeRangeSelectionNodeForm(JToken data, string NodeAttribute, string lableHead, Label lblMax, Label lblMin, string elementType, Label lblDistribution)
        {
            if (data == null || data.Count() == 0)
            {
                return;
            }

            JArray orderedArrayDescending = new JArray();
            //Sort the JArray by value decending 
            try
            {
                orderedArrayDescending = new JArray(data.OrderByDescending(x => (double)x.SelectToken("value")));
            }
            catch { }

            if (orderedArrayDescending.Count > 1)
            {
                RangeSelectForm rangeForm = new RangeSelectForm();
                rangeForm.data = orderedArrayDescending;
                rangeForm.Text1 = lableHead;
                rangeForm.Range1Maximum = orderedArrayDescending.Count;

                //Set current values
                int count = 0;
                int col = 0;

                rangeForm.RangeLimitMax = orderedArrayDescending.Count;
                rangeForm.RangeLimitMin = 1;
                rangeForm.elementType = elementType;


                if (rangeForm.DistributionParameters != null && rangeForm.DistributionParameters.Count > 0)  // Set range pointers to existing values
                {

                    for (int i = orderedArrayDescending.Count - 1; i > -1; i--)
                    {

                        count++;
                        col = (orderedArrayDescending.Count - count) + 1;
                        var item = orderedArrayDescending[i] as JToken;


                        if (item["value"].Value<double>().ToString("F2") == lblMax.Text || item["value"].ToString() == lblMax.Text)
                            rangeForm.SelectedRangeMax = count;


                        if (item["value"].Value<double>().ToString("F2") == lblMin.Text || item["value"].ToString() == lblMax.Text)
                            rangeForm.SelectedRangeMin = count;

                    }
                }
                else  // New distribution, so set range pointers to min and max
                {
                    rangeForm.SelectedRangeMax = rangeForm.RangeLimitMax;
                    rangeForm.SelectedRangeMin = rangeForm.RangeLimitMin;
                }



                //Show the dialog and get the results
                if (rangeForm.ShowDialog(this) == DialogResult.OK)
                {
                    lblMax.Text = orderedArrayDescending[orderedArrayDescending.Count - rangeForm.SelectedRangeMax]["value"].ToString();
                    lblMin.Text = orderedArrayDescending[orderedArrayDescending.Count - rangeForm.SelectedRangeMin]["value"].ToString();
                }
            }
            else if (orderedArrayDescending.Count > 0)
            {
                lblMax.Text = orderedArrayDescending[0]["value"].ToString();
                lblMin.Text = orderedArrayDescending[0]["value"].ToString();
            }
            else
            {
                lblMax.Text = "-";
                lblMin.Text = "-";
            }

        }

        private void cmbImpactFrom_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Handle being called without a provided node
            if (mainForm._selectedNodes.Count > 0)
            {
                //updateNodeAttribute("asset", _selectedNodes.ElementAt(0).Value);
                updateNodeAttribute("asset", mainForm._selectedNodes.ElementAt(0).Value);
            }
        }

        private void cmbEdgeRelationship_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (mainForm == null || mainForm._selectedEdges.Count < 1) return;
            Edge edge = mainForm._selectedEdges.ElementAt(0).Value as Edge;
            mainForm._browser.ExecScriptAsync($"setElementData('{edge.ID}', 'relationship', '{cmbEdgeRelationship.Text}');");
        }

        private void SetObjectiveStrengthValue()
        {
            mainForm._browser.ExecScriptAsync($"setElementData('{mainForm._selected_node_id}', 'objectiveTargetValue', '{txtObjectiveTargetStrengthValue.Value.ToString()}');");
            GraphUtil.SetNodeData(mainForm._selected_node_id, "objectiveTargetValue", txtObjectiveTargetStrengthValue.Value.ToString());
            mainForm.nodesForm.updateObjectivePanel(mainForm._selected_node_id);
        }
        private void txtObjectiveTotalStrengthValue_ValueChanged(object sender, EventArgs e)
        {
            if (rbControlObjectiveManuallySet.Checked)
                SetObjectiveStrengthValue();
        }

        private void txtGroupTotalValue_ValueChanged(object sender, EventArgs e)
        {
            //if (ribbonForm.UpdatecontrolBaseScore && cmbGroupTotal.GetItemText(cmbGroupTotal.SelectedItem) == "Manually set")
            //{
            //    ribbonForm._browser.ExecScriptAsync($"setElementData('{Form1._selected_node_id}', 'controlBaseScore', '{txtGroupTotalValue.Value.ToString()}');");
            //    GraphUtil.SetNodeData(Form1._selected_node_id, "controlBaseScore", txtGroupTotalValue.Value.ToString());
            //    if (GraphCalcs.autoCalculate) GraphCalcs.RecalculateAll();
            //}
        }

        private void comboBoxNodeGroupBehaviour_SelectionChangeCommitted(object sender, EventArgs e)
        {
            string controlType = GraphUtil.GetNodeType(mainForm._selected_node_id).ToLower();

            GraphUtil.SetNodeData(mainForm._selected_node_id, "nodeBehaviour", cbNodeGroupBehaviour.Items[cbNodeGroupBehaviour.SelectedIndex].ToString());
            if (GraphCalcs.autoCalculate) GraphCalcs.RecalculateAll();

            mainForm._browser.ExecScriptAsync($"setElementData('{mainForm._selected_node_id}', 'nodeBehaviour', '{cbNodeGroupBehaviour.Items[cbNodeGroupBehaviour.SelectedIndex].ToString()}');");
        }


        public void setControlStrengthValue(string maxValue, string minValue)
        {
            lblControlStrenghMax.Text = maxValue;
            lblControlStrenghMin.Text = minValue;
        }

        public void setControlImplementationValue(string maxValue, string minValue)
        {
            lblControlImplementationMax.Text = maxValue;
            lblControlImplementationMin.Text = minValue;
        }

        public void UpdateNodeAssessmentInfo(string nodeGUID)
        {

            string nodeType = "none";

            nodeType = GraphUtil.GetNodeType(nodeGUID).ToLower();

            //Base Value
            if (nodeType == "control")
            {
                this.InvokeIfNeed(() =>
                {

                    // Get the Base Strength
                    double BaseValue = GraphUtil.GetNodeBaseValue(nodeGUID);
                    double BaseMinValue = GraphUtil.GetNodeBaseMinValue(nodeGUID);
                    setControlStrengthValue(BaseValue.ToString(), BaseMinValue.ToString());


                    // Get Assessed Strength Values
                    double AsessedValue = GraphUtil.GetNodecontrolAssessedValue(nodeGUID);
                    double AsessedMinValue = GraphUtil.GetControlNodeAssessedMinValue(nodeGUID);
                    setControlImplementationValue(AsessedValue.ToString(), AsessedMinValue.ToString());
                });

            }
            else if (nodeType == "objective")
            {
                this.InvokeIfNeed(() =>
                {
                    string objectiveType = GraphUtil.GetNodeObjectiveTargetType(nodeGUID);
                    switch (objectiveType)
                    {
                        case "Manually Set":
                            rbControlObjectiveManuallySet.Checked = true;
                            SetRbCheckState(rbControlObjectiveManuallySet);
                            txtObjectiveTargetStrengthValue.Value = (decimal)GraphUtil.GetNodeObjectiveTargetValue(nodeGUID);
                            break;
                        case "Sum of Control Strengths":
                            rbSumOfActualControlEdgeImpacted.Checked = true;
                            SetRbCheckState(rbSumOfActualControlEdgeImpacted);
                            break;
                        case "Sum of Control Maximums (Edge Impacted)":
                            rbSumOfControlMaximumsEdgeImpacted.Checked = true;
                            SetRbCheckState(rbSumOfControlMaximumsEdgeImpacted);
                            break;
                        case "Sum of Control Maximums":
                            rbSumOfControlMaximums.Checked = true;
                            SetRbCheckState(rbSumOfControlMaximums);
                            break;
                    }

                });

            }

            else if (nodeType == "group")
            {
                this.InvokeIfNeed(() =>
                {
                    this.cmbGroupTotal.Items.Clear();
                    this.cmbGroupTotal.Items.Add("Sum of all Fundamental strength values");
                    this.cmbGroupTotal.Items.Add("Manually set");
                    this.cmbGroupTotal.DropDownStyle = ComboBoxStyle.DropDownList;
                    string implementedStrengthText = GraphUtil.GetInherentStrengthText(nodeGUID);
                    cmbGroupTotal.SelectedIndex = cmbGroupTotal.Items.IndexOf(implementedStrengthText);
                    txtGroupTotalValue.Value = Convert.ToDecimal(GraphUtil.GetNodeBaseScore(nodeGUID));
                });

                this.InvokeIfNeed(() =>
                {
                    this.cmbGroupImplementation.Items.Clear();
                    this.cmbGroupImplementation.Items.Add("Sum of all Implementation Strength values");
                    this.cmbGroupImplementation.DropDownStyle = ComboBoxStyle.DropDownList;
                    string inherentStrengthText = GraphUtil.GetImplementedStrengthText(nodeGUID);
                    cmbGroupImplementation.SelectedIndex = cmbGroupImplementation.Items.IndexOf(inherentStrengthText);
                    txtGroupImplementationValue.Value = Convert.ToDecimal(GraphUtil.GetEdgeStrengthValue(nodeGUID));
                });

                this.InvokeIfNeed(() =>
                {
                    string nodeBehaviour = GraphUtil.GetnodeBehaviour(nodeGUID);
                    this.cbNodeGroupBehaviour.SelectedIndex = cbNodeGroupBehaviour.Items.IndexOf(nodeBehaviour);
                });
            }
            else if (nodeType == "vulnerability-group")
            {
                this.InvokeIfNeed(() =>
                {
                    string nodeBehaviour = GraphUtil.GetnodeBehaviour(nodeGUID);
                    this.cbNodeVulnerabilityGroupBehaviour.SelectedIndex = cbNodeVulnerabilityGroupBehaviour.Items.IndexOf(nodeBehaviour);
                });
            }

           }
    
        public void visiblePanelEdgeDetails(bool flag)
        {
            this.panelEdgeDistributionValues.Visible = flag;
            if (flag) { this.panelEdgeDistributionValues.BringToFront(); }
        }

       public void setEdgeDetailData(string relationshipStrength, string relationship, string edgeStrengthValue, string edgeStrengthMinValue)
       {
            cmbEdgeRelationship.Text = relationship;
            
            lblEdgeMin.Text = edgeStrengthMinValue == "" ? ""  : decimal.Parse(edgeStrengthMinValue).ToString("F2"); ;
            lblEdgeMax.Text = edgeStrengthValue== "" ? "" :  decimal.Parse(edgeStrengthValue).ToString("F2"); ;
        }
        public void setRelationshipForEdge(string[] lines)
        {
            for (int i = 0; i < lines.Length; i++)
            {
                cmbEdgeRelationship.Items.Add(lines[i]);
            }
            cmbEdgeRelationship.SelectedIndex = 0;

        }


        private void pictureBox5_Click(object sender, EventArgs e)
        {
            RTFForm RtfForm = new RTFForm();
            RtfForm.LoadResouceFile("Threat Actor Distributions", "en-ThreatActorDistributions.rtf");
            RtfForm.ShowDialog();
        }


        private void button14_Click(object sender, EventArgs e)
        {
            
        }


        private void txtObjectiveTargetStrengthValue_KeyDown(object sender, KeyEventArgs e)
        {
            // Suppress system chinme on Enter key
            if (e.KeyCode == Keys.Enter)
            {
                //enter key is down
                e.Handled = true;
                e.SuppressKeyPress = true;

            }
        }

        private void SetRbCheckState(RadioButtonAdv control)
        {
            if (control != rbSumOfControlMaximums) rbSumOfControlMaximums.Checked = false;
            if (control != rbSumOfControlMaximumsEdgeImpacted) rbSumOfControlMaximumsEdgeImpacted.Checked = false;
            if (control != rbSumOfActualControlEdgeImpacted) rbSumOfActualControlEdgeImpacted.Checked = false;
            if (control != rbControlObjectiveManuallySet)
            {
                rbControlObjectiveManuallySet.Checked = false;
                txtObjectiveTargetStrengthValue.Value = 100;
                txtObjectiveTargetStrengthValue.Visible = false;
            }
            else
                txtObjectiveTargetStrengthValue.Visible = true;
        }

        private void cbNodeVulnerabilityGroupBehaviour_SelectedIndexChanged(object sender, EventArgs e)
        {

            GraphUtil.SetNodeData(mainForm._selected_node_id, "nodeBehaviour", cbNodeVulnerabilityGroupBehaviour.Items[cbNodeVulnerabilityGroupBehaviour.SelectedIndex].ToString());
            if (GraphCalcs.autoCalculate) GraphCalcs.RecalculateAll();

            mainForm._browser.ExecScriptAsync($"setElementData('{mainForm._selected_node_id}', 'nodeBehaviour', '{cbNodeVulnerabilityGroupBehaviour.Items[cbNodeVulnerabilityGroupBehaviour.SelectedIndex].ToString()}');");
        }

        private void cbNodeAssetBehaviour_SelectionChangeCommitted(object sender, EventArgs e)
        {
            GraphUtil.SetNodeData(mainForm._selected_node_id, "nodeBehaviour", cbNodeAssetBehaviour.Items[cbNodeAssetBehaviour.SelectedIndex].ToString());
            if (GraphCalcs.autoCalculate) GraphCalcs.RecalculateAll();
            mainForm._browser.ExecScriptAsync($"setElementData('{mainForm._selected_node_id}', 'nodeBehaviour', '{cbNodeAssetBehaviour.Items[cbNodeAssetBehaviour.SelectedIndex].ToString()}');");
        }

        private void rbSumOfControlMaximumsEdgeImpacted_Click(object sender, EventArgs e)
        {
            if (rbSumOfControlMaximumsEdgeImpacted.Checked)
            {
                SetRbCheckState(sender as RadioButtonAdv);

                GraphUtil.SetNodeData(mainForm._selected_node_id, "objectiveTargetType", "Sum of Control Maximums (Edge Impacted)");
                mainForm._browser.EvaluateScriptAsync($"setElementData('{mainForm._selected_node_id}', 'objectiveTargetType', 'Sum of Control Strengths');");
            }
        }

        private void rbSumOfControlStrengths_Click(object sender, EventArgs e)
        {
            if (rbSumOfControlMaximums.Checked)
            {
                SetRbCheckState(sender as RadioButtonAdv);

                GraphUtil.SetNodeData(mainForm._selected_node_id, "objectiveTargetType", "Sum of Control Maximums");
                mainForm._browser.EvaluateScriptAsync($"setElementData('{mainForm._selected_node_id}', 'objectiveTargetType', 'Sum of Control Strengths');");
            }
        }

        private void rbControlObjectiveManuallySet_Click(object sender, EventArgs e)
        {
            if (rbControlObjectiveManuallySet.Checked)
            {
                SetRbCheckState(sender as RadioButtonAdv);

                GraphUtil.SetNodeData(mainForm._selected_node_id, "objectiveTargetType", "Manually Set");
                mainForm._browser.EvaluateScriptAsync($"setElementData('{mainForm._selected_node_id}', 'objectiveTargetType', 'Manually Set');");
                SetObjectiveStrengthValue();
            }
        }

        private void label33_Click(object sender, EventArgs e)
        {

        }

        private void lblVulnExposureMax_Click(object sender, EventArgs e)
        {

        }

        private void label53_Click(object sender, EventArgs e)
        {

        }

        private void label41_Click(object sender, EventArgs e)
        {
                    }

        private void label56_Click(object sender, EventArgs e)
        {

        }

        private void label101_Click(object sender, EventArgs e)
        {

        }

        private void label100_Click(object sender, EventArgs e)
        {

        }

        private void label125_Click(object sender, EventArgs e)
        {

        }

        private void lblVulnPrivRequiredMax_Click(object sender, EventArgs e)
        {

        }

        private void label94_Click(object sender, EventArgs e)
        {

        }

        private void label69_Click(object sender, EventArgs e)
        {

        }

        private void label106_Click(object sender, EventArgs e)
        {

        }

        private void label73_Click(object sender, EventArgs e)
        {

        }

        private void label89_Click(object sender, EventArgs e)
        {

        }

        private void panel306_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel28_Click(object sender, EventArgs e)
        {
            showNodeRangeSelection(mainForm._settings.NodeAttrActor["access"], "actorAccess", label6.Text + " - " + label52.Text, lblActorAccessMax, lblActorAccessMin, "node", lblActorAccessDistribution);

        }

        private void panel29_Click(object sender, EventArgs e)
        {
            showNodeRangeSelection(mainForm._settings.NodeAttrActor["capability"], "actorCapability", label6.Text + " - " + label61.Text, lblActorCapabilityMax, lblActorCapabilityMin, "node", lblActorCapabilityDistribution);

        }

        private void panel22_Click(object sender, EventArgs e)
        {
            showNodeRangeSelection(mainForm._settings.NodeAttrActor["resources"], "actorResources", label6.Text + " - " + label62.Text, lblActorResourcesMax, lblActorResourcesMin, "node", lblActorResourcesDistribution);
        }

        private void panel32_Click(object sender, EventArgs e)
        {
            showNodeRangeSelection(mainForm._settings.NodeAttrActor["motivation"], "actorMotivation", label6.Text + " - " + label67.Text, lblActorMotivationMax, lblActorMotivationMin, "node", lblActorMotivationDistribution);
        }

        private void DistributionPanel_MouseEnter(object sender, EventArgs e)
        {
            (sender as Panel).BackColor = Color.Gainsboro;

        }

        private void DistributionPanel_MouseLeave(object sender, EventArgs e)
        {
            (sender as Panel).BackColor = Color.Transparent;
        }

        private void panelVulnerabilityNodeDistributionValues_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label26_Click(object sender, EventArgs e)
        {

        }

        private void panel49_Click(object sender, EventArgs e)
        {
            showNodeRangeSelection(mainForm._settings.NodeAttrAttack["complex"], "attackComplexity", label26.Text + " - " + label68.Text, lblAttackComplexityMax, lblAttackComplexityMin, "node", lblAttackComplexityDistribution);

        }

        private void panel35_Click(object sender, EventArgs e)
        {
            showNodeRangeSelection(mainForm._settings.NodeAttrAttack["prolife"], "attackProliferation", label26.Text + " - " + label73.Text, lblAttackProliferationMax, lblAttackProliferationMin, "node", lblAttackProliferationDistribution);

        }

        private void panel73_Click(object sender, EventArgs e)
        {
            showNodeRangeSelection(mainForm._settings.NodeAttrAsset["confidentiality"], "assetConfidentiality", label2.Text + " - " + label80.Text, lblAssetConfMax, lblAssetConfMin, "node", lblAssetConfDistribution);

        }

        private void panel7_Click(object sender, EventArgs e)
        {
            showNodeRangeSelection(mainForm._settings.NodeAttrAsset["availibility"], "assetAvailability", label2.Text + " - " + label99.Text, lblAssetAvailMax, lblAssetAvailMin, "node", lblAssetAvailDistribution);

        }

        private void panel2_Click(object sender, EventArgs e)
        {
            showNodeRangeSelection(mainForm._settings.NodeAttrAsset["accountable"],"assetAccountability", label2.Text + " - " + label104.Text, lblAssetAccountMax, lblAssetAccountMin, "node", lblAssetAccountDistribution);

        }

        private void panel51_Click(object sender, EventArgs e)
        {
            showNodeRangeSelection(mainForm._settings.NodeAttrAsset["integrity"], "assetIntegrity", label2.Text + " - " + label92.Text, lblAssetIntegMax, lblAssetIntegMin, "node", lblAssetIntegDistribution);
        }

        private void panelAttackNodeDistributionValues_Paint(object sender, PaintEventArgs e)
        {

        }

        private void lblVulnInteractionRequiredMin_Click(object sender, EventArgs e)
        {
                    }

        private void panel230_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel121_Click(object sender, EventArgs e)
        {
            showNodeRangeSelection(mainForm._settings.NodeAttrVulnerability["ease"], "vulnerabilityEaseOfExploitation", label11.Text + " - " + label74.Text, lblVulnEaseofExploitMax, lblVulnEaseofExploitMin, "node", lblVulnEaseofExploitDistribution);

        }

        private void panel74_Click(object sender, EventArgs e)
        {
            
            showNodeRangeSelection(mainForm._settings.NodeAttrVulnerability["exposure"], "vulnerabilityExposure", label11.Text + " - " + label79.Text, lblVulnExposureMax, lblVulnExposureMin, "node", lblVulnExposureDistribution);

        }

        private void panel8_Click(object sender, EventArgs e)
        {
            showNodeRangeSelection(mainForm._settings.NodeAttrVulnerability["privileges_required"], "vulnerabilityPrivilegesRequired", label11.Text + " - " + label84.Text, lblVulnPrivRequiredMax, lblVulnPrivRequiredMin, "node", lblVulnPrivRequiredDistribution);

        }

        private void panel4_Click(object sender, EventArgs e)
        {
            showNodeRangeSelection(mainForm._settings.NodeAttrVulnerability["interaction_required"], "vulnerabilityInteractionRequired", label11.Text + " - " + label89.Text, lblVulnInteractionRequiredMax, lblVulnInteractionRequiredMin, "node", lblVulnInteractionRequiredDistribution);

        }

        private void panel122_Click(object sender, EventArgs e)
        {
            
            showNodeRangeSelection(mainForm._settings.NodeAttrVulnerability["exposes_scope"], "vulnerabilityExposesScope", label11.Text + " - " + label94.Text, lblVulnExposesScopeMax, lblVulnExposesScopeMin, "node", lblVulnExposesScopeDistribution);

        }

        private void label81_Click(object sender, EventArgs e)
        {

        }

        private void rbSumOfActualControlEdgeImpacted_Click(object sender, EventArgs e)
        {
            if (rbSumOfActualControlEdgeImpacted.Checked)
            {
                SetRbCheckState(sender as RadioButtonAdv);

                GraphUtil.SetNodeData(mainForm._selected_node_id, "objectiveTargetType", "Sum of Control Strengths");
                mainForm._browser.EvaluateScriptAsync($"setElementData('{mainForm._selected_node_id}', 'objectiveTargetType', 'Sum of Control Strengths');");
            }
        }

        private void panel37_Click(object sender, EventArgs e)
        {
            showNodeRangeSelection(mainForm._settings.EdgeStrengthList, "edgeStrength", label10.Text + " - " + label36.Text, lblEdgeMax, lblEdgeMin, "edge", lblEdgeDistribution);
        }

        private void panel15_Click(object sender, EventArgs e)
        {
            showNodeRangeSelection(mainForm._settings.NodeinherentStrengthData, "controlBase", label4.Text + " - " + label95.Text, lblControlStrenghMax, lblControlStrenghMin, "node", lblControlStrenghDistribution);

        }

        private void panel13_Click(object sender, EventArgs e)
        {
            showNodeRangeSelection(mainForm._settings.NodeimplementedStrengthData, "controlAssessed", label4.Text + " - " + label106.Text, lblControlImplementationMax, lblControlImplementationMin, "node", lblControlImplementationDistribution);
        }

        private void lblEdgeMin_TextChanged(object sender, EventArgs e)
        {
            //UpdateEdgeStrengthDisplayValue();
        }
        private void lblEdgeMax_TextChanged(object sender, EventArgs e)
        {
            //UpdateEdgeStrengthDisplayValue();
        }

        public void UpdateEdgeStrengthDisplayValue()
        {
            if (mainForm.selectedEdgeID() != "")
            {
                double res = GraphUtil.GetEdgeStrengthValue(mainForm.selectedEdgeID());
                JObject obj = GraphUtil.GetEdgeDisplayData(mainForm._settings.EdgeDisplayList, res);
                if (obj.ContainsKey("width") && obj.ContainsKey("color"))
                {
                    string width = obj["width"].ToString();
                    string color = obj["color"].ToString();
                    mainForm._browser.EvaluateScriptAsync($"setElementData('{mainForm.selectedEdgeID()}', 'weight', '{width}');");
                    mainForm._browser.EvaluateScriptAsync($"setElementData('{mainForm.selectedEdgeID()}', 'color', '{color}');");
                }
            }
        }

    }
}
