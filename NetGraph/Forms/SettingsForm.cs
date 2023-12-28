using CyConex.Graph;
using Syncfusion.Drawing;
using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using System.Collections;
using Newtonsoft.Json.Linq;
using CyConex.Helpers;
using static Syncfusion.XlsIO.Implementation.HtmlSaveOptions;
using System.Windows.Controls;
using CyConex.API;
using Newtonsoft.Json;
using Syncfusion.WinForms.Controls;
using System.Text;
using System.Windows.Forms.DataVisualization.Charting;

namespace CyConex
{
    public partial class SettingsForm : Form
    {
        public ApplicationSettings _settings;
        private JArray node_primary_categories = new JArray();
        private JArray node_sub_categories = new JArray();
        public JArray node_categories = new JArray();
        private JArray default_edge_relations = new JArray();
        public string _settings_guid = "";

        private ArrayList node_control_strength_list = new ArrayList();
        private ArrayList node_control_assets_list = new ArrayList();
        public SettingsForm(ref ApplicationSettings settings)
        {
            InitializeComponent();

            _settings = settings;
            OpenLast_checkBox.Checked = _settings.OpenLastDocumentOnStart;
            AutoSave_checkBox.Checked = _settings.AutoSaveOnChanges;
            AutoSaveTimer_Checkbox.Checked = _settings.AutoSaveOnTimer;
            RestoreWindow_checkBox.Checked = _settings.RestoreWindowStateOnStart;
            AutoCenterGraph_checkbox.Checked = _settings.AutoCenterGraph;
            ShowImportPreview_checkBox.Checked = _settings.ShowImportPreview;

            string settings_guid = SettingsAPI.GetSettingsGUID();
            JObject settings_attack_obj = null;
            JObject settings_vulnerability_obj = null;
            JObject settings_asset_obj = null;

            if (settings_guid != null && settings_guid != "")
            {
                settings_attack_obj = SettingsAPI.GetSettingsAttackValues(settings_guid);
                settings_attack_obj = GraphUtil.ConvertSettingsAttackValue(settings_attack_obj);

                settings_vulnerability_obj = SettingsAPI.GetSettingsVulnerability(settings_guid);
                settings_vulnerability_obj = GraphUtil.ConvertSettingsVulnerability(settings_vulnerability_obj);

                settings_asset_obj = SettingsAPI.GetSettingsAttackValues(settings_guid);
                settings_asset_obj = GraphUtil.ConvertSettingsAsset(settings_asset_obj);
            }

            try
            {
                string cyFileData = "";
                if (File.Exists(@"Graph\cy.data"))
                {
                    StreamReader sr = new StreamReader(@"Graph\cy.data");
                    cyFileData = sr.ReadToEnd();
                    sr.Close();
                }

                JObject cyJsonFileData = cyFileData == "" ? null : JObject.Parse(cyFileData);
                string edgeRelationData = cyJsonFileData["edge_relationship"].ToString();
                JArray nodeAssets = (JArray)cyJsonFileData["node_assets"];
                //JArray edgeStrength = (JArray)cyJsonFileData["edge_relationship_strength"];
                JArray edgeStrengthData = cyJsonFileData.ContainsKey("EdgeStrengthList") ? (JArray)cyJsonFileData["EdgeStrengthList"] : new JArray();
                JArray edgeDisplayData = cyJsonFileData.ContainsKey("EdgeDisplayList") ? (JArray)cyJsonFileData["EdgeDisplayList"] : new JArray();
                JArray nodeStrength = (JArray)cyJsonFileData["impact"];
                JArray keyWords = (JArray)cyJsonFileData["key_words"];


                JObject attackAttr = settings_attack_obj == null ? (cyJsonFileData.ContainsKey("attack_value") ? (JObject)cyJsonFileData["attack_value"] : new JObject()) : settings_attack_obj;
                JObject actorAttr = cyJsonFileData.ContainsKey("actor_value") ? (JObject)cyJsonFileData["actor_value"] : new JObject();
                JObject assetAttr = settings_asset_obj == null ? (cyJsonFileData.ContainsKey("asset_value") ? (JObject)cyJsonFileData["asset_value"] : new JObject()) : settings_asset_obj;
                JObject vulnerabilityAttr = settings_vulnerability_obj == null ? (cyJsonFileData.ContainsKey("vulnerability_value") ? (JObject)cyJsonFileData["vulnerability_value"] : new JObject()) : settings_vulnerability_obj;

                node_categories = cyJsonFileData.ContainsKey("node_categories") ? (JArray)cyJsonFileData["node_categories"] : new JArray();
                default_edge_relations = cyJsonFileData.ContainsKey("default_edge_relation") ? (JArray)cyJsonFileData["default_edge_relation"] : new JArray();

                _settings.EdgeRelationdata = edgeRelationData;
                string[] lines = _settings.EdgeRelationdata.Split(',');
                Array.Sort(lines);
                this.listEdgeRelations.Items.Clear();
                for (int i = 0; i < lines.Length; i++)
                {
                    this.listEdgeRelations.Items.Add(lines[i]);
                    edge_relationship.Items.Add(lines[i]);
                }

                _settings.NodeimplementedStrengthData = nodeAssets;
                ArrayList array1 = new ArrayList();
                for (int i = 0; i < nodeAssets.Count; i++)
                {
                    JObject tmp = (JObject)nodeAssets[i];
                    string impact = tmp.ContainsKey("impact") ? tmp["impact"].ToString() : "";
                    string value = tmp.ContainsKey("value") ? tmp["value"].ToString() : "";
                    string desc = tmp.ContainsKey("description") ? tmp["description"].ToString() : "";
                    //string color = tmp.ContainsKey("color") ? tmp["color"].ToString() : "rgb(0,0,0)";
                    array1.Add(new NodeAsset(impact, value, desc/*, color*/));
                }

                _settings.EdgeStrengthList = edgeStrengthData;
                _settings.EdgeDisplayList = edgeDisplayData;

                _settings.NodeinherentStrengthData = nodeStrength;
                ArrayList array3 = new ArrayList();
                for (int i = 0; i < nodeStrength.Count; i++)
                {
                    JObject tmp = (JObject)nodeStrength[i];
                    string impact = tmp.ContainsKey("impact") ? tmp["impact"].ToString() : "";
                    string value = tmp.ContainsKey("value") ? tmp["value"].ToString() : "";
                    string desc = tmp.ContainsKey("description") ? tmp["description"].ToString() : "";
                    //string color = tmp.ContainsKey("color") ? tmp["color"].ToString() : "rgb(0,0,0)";
                    array3.Add(new NodeControl(impact, value, desc));
                }

                _settings.KeyWordsData = keyWords;
                ArrayList array4 = new ArrayList();
                for (int i = 0; i < keyWords.Count; i++)
                {
                    string tmp = keyWords[i].ToString();
                    this.listKeyWords.ListBox.Items.Add(tmp);
                }

                _settings.NodeAttrActor = actorAttr;
                _settings.NodeAttrAttack = attackAttr;
                _settings.NodeAttrAsset = assetAttr;
                _settings.NodeAttrVulnerability = vulnerabilityAttr;

                initActorNodeAttribute();
                initAttackNodeAttribute();
                initAssetNodeAttribute();
                initVulnerabilityNodeAttribute();
                initNodeCategories();
                initDefaultEdgeRelationship();

                node_control_strength_list = array3;
                node_control_assets_list = array1;
                SetDataToNodeControlDataGrid(dataGridControlStrength, node_control_strength_list);
                SetDataToNodeControlDataGrid(dataGridControlImplementation, node_control_assets_list, "asset");
                SetDataToEdgeDataGrid(dataGridEdgeStrength, _settings.EdgeStrengthList, "edge_strength");
                SetDataToEdgeDataGrid(dataGridEdgeDisplay, _settings.EdgeDisplayList, "edge_display");
            }
            catch
            { 

            }
        }
        private void dataGridControlStrength_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            setDataGridCellColor(e);
        }

        private void dataGridNodeAsset_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            setDataGridCellColor(e, "asset");
        }

        private void dataGridEdgeDisplay_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            setDataGridCellColor(e, "edge_display");
        }

        private void setDataGridCellColor(DataGridViewCellFormattingEventArgs e, string type = "value")
        {
            if (e.RowIndex > _settings.EdgeDisplayList.Count() - 1)
                return;
            if (e.ColumnIndex == 3)
            {
                string color = "rgb(0,0,0)";
                /*if (type == "value")
                {
                    color = (node_control_strength_list[e.RowIndex] as NodeControl).Color;
                }
                else if (type == "asset")
                {
                    color = (node_control_assets_list[e.RowIndex] as NodeAsset).Color;
                }
                else */if (type == "edge_display")
                {
                    color = (_settings.EdgeDisplayList[e.RowIndex] as JObject)["color"].ToString();
                }
                e.CellStyle.BackColor = GeneralHelpers.ConvertColorFromHTML(color );
            }
        }

        private void initNodeCategories()
        {
            ArrayList array = new ArrayList();
            for (int i = 0; i < node_categories.Count; i++)
            {
                string text = node_categories[i]["text"].ToString();
                array.Add(new NodeCategory(text));
            }
            nodePrimaryCategory_Grid.DataSource = array;
        }

        private void initNodeSubCategories(string data)
        {
            ArrayList array = new ArrayList();
            for (int i = 0; i < node_categories.Count; i++)
            {
                string text = node_categories[i]["text"].ToString();
                if (text == data)
                {
                    JArray tmpArr = node_categories[i]["childrens"] as JArray;
                    foreach (string tmp in tmpArr)
                    {
                        array.Add(new NodeCategory(tmp));
                    }
                    break;
                }
            }
            nodeSubCategory_Grid.DataSource = array;
        }

        public JArray getDefaultEdgeRelationships()
        {
            return default_edge_relations;
        }

        public string[] getDefaultEdgeRelationship(string source, string target)
        {
            string[] lines = _settings.EdgeRelationdata.Split(',');
            for (int i = 0; i < default_edge_relations.Count; i++)
            {
                if (default_edge_relations[i]["source"].ToString().ToLower() == source.ToLower() &&
                    default_edge_relations[i]["target"].ToString().ToLower() == target.ToLower())
                {
                    for (int j = 0; j < lines.Length; j++)
                    {
                        string tmp = default_edge_relations[i]["relation"].ToString();
                        if (lines[j].Contains(tmp))
                        {
                            return new string[] { source, target, lines[j] };
                        }
                    }
                }
            }
            return new string[] { source, target };
        }

        private void initDefaultEdgeRelationship()
        {
            string[] row = new string[] { };
            row = getDefaultEdgeRelationship("Control", "Actor");
            defaultEdgeRelation_GridView.Rows.Add(row);
            row = getDefaultEdgeRelationship("Control", "Attack");
            defaultEdgeRelation_GridView.Rows.Add(row);
            row = getDefaultEdgeRelationship("Control", "Vulnerability");
            defaultEdgeRelation_GridView.Rows.Add(row);
            row = getDefaultEdgeRelationship("Control", "Asset");
            defaultEdgeRelation_GridView.Rows.Add(row);
            row = getDefaultEdgeRelationship("Control", "Objective");
            defaultEdgeRelation_GridView.Rows.Add(row);
            row = getDefaultEdgeRelationship("Control", "Group");
            defaultEdgeRelation_GridView.Rows.Add(row);
            row = getDefaultEdgeRelationship("Objective", "Objective");
            defaultEdgeRelation_GridView.Rows.Add(row);
            row = getDefaultEdgeRelationship("Objective", "Asset");
            defaultEdgeRelation_GridView.Rows.Add(row);
            row = getDefaultEdgeRelationship("Objective", "Attack");
            defaultEdgeRelation_GridView.Rows.Add(row);
            row = getDefaultEdgeRelationship("Objective", "Actor");
            defaultEdgeRelation_GridView.Rows.Add(row);
            row = getDefaultEdgeRelationship("Group", "Group");
            defaultEdgeRelation_GridView.Rows.Add(row);
            row = getDefaultEdgeRelationship("Group", "Asset");
            defaultEdgeRelation_GridView.Rows.Add(row);
            row = getDefaultEdgeRelationship("Group", "Attack");
            defaultEdgeRelation_GridView.Rows.Add(row);
            row = getDefaultEdgeRelationship("Group", "Actor");
            defaultEdgeRelation_GridView.Rows.Add(row);
            row = getDefaultEdgeRelationship("Actor", "Attack");
            defaultEdgeRelation_GridView.Rows.Add(row);
            row = getDefaultEdgeRelationship("Attack", "Asset");
            defaultEdgeRelation_GridView.Rows.Add(row);
            row = getDefaultEdgeRelationship("Vulnerability", "Asset");
            defaultEdgeRelation_GridView.Rows.Add(row);
            row = getDefaultEdgeRelationship("Attack", "Vulnerability");
            defaultEdgeRelation_GridView.Rows.Add(row);
            row = getDefaultEdgeRelationship("Objective", "Vulnerability");
            defaultEdgeRelation_GridView.Rows.Add(row);
        }

        private void setNodeAttributeData(JArray data, Syncfusion.Windows.Forms.Grid.GridListControl gridList)
        {
            if (data == null)
            {
                return;
            }

            ArrayList array = new ArrayList();
            for (int i = 0; i < data.Count; i++)
            {
                string impact = (string)data[i]["impact"];
                string value = (string)data[i]["value"];
                string desc = (string)data[i]["desc"];
                desc = desc == null ? (string)data[i]["description"] : desc;
                array.Add(new NodeAttribute(impact, value, desc));
            }
            gridList.DataSource = array;
        }

        private void initActorNodeAttribute()
        {
            JArray capabilityObj = _settings.NodeAttrActor.ContainsKey("capability") ?
                _settings.NodeAttrActor["capability"] as JArray : new JArray();
            JArray resourcesObj = _settings.NodeAttrActor.ContainsKey("resources") ?
                _settings.NodeAttrActor["resources"] as JArray : new JArray();
            JArray motivationObj = _settings.NodeAttrActor.ContainsKey("motivation") ?
                _settings.NodeAttrActor["motivation"] as JArray : new JArray();
            JArray accessObj = _settings.NodeAttrActor.ContainsKey("access") ?
                _settings.NodeAttrActor["access"] as JArray : new JArray();
            JArray impactsConfident = _settings.NodeAttrActor.ContainsKey("impacts_confidentialityiality") ?
                _settings.NodeAttrActor["impacts_confidentialityiality"] as JArray : new JArray();
            JArray impactsIntegrity = _settings.NodeAttrActor.ContainsKey("impacts_integrity") ?
                _settings.NodeAttrActor["impacts_integrity"] as JArray : new JArray();
            JArray impactsAvailability = _settings.NodeAttrActor.ContainsKey("impacts_availability") ?
                _settings.NodeAttrActor["impacts_availability"] as JArray : new JArray();
            JArray impactsAccountability = _settings.NodeAttrActor.ContainsKey("impacts_accountability") ?
                _settings.NodeAttrActor["impacts_accountability"] as JArray : new JArray();

            setNodeAttributeData(capabilityObj, gridActorCapability);
            setNodeAttributeData(resourcesObj, gridActorResources);
            setNodeAttributeData(motivationObj, gridActorMotivation);
            setNodeAttributeData(accessObj, gridActorAccess);
            setNodeAttributeData(impactsConfident, gridactorImpactToConfidentiality);
            setNodeAttributeData(impactsIntegrity, gridactorImpactToIntegrity);
            setNodeAttributeData(impactsAvailability, gridactorImpactToAvailability);
            setNodeAttributeData(impactsAccountability, gridactorImpactToAccountability);
        }

        private void initAttackNodeAttribute()
        {
            JArray complexObj = _settings.NodeAttrAttack.ContainsKey("complex") ?
                _settings.NodeAttrAttack["complex"] as JArray : new JArray();
            JArray prolifeObj = _settings.NodeAttrAttack.ContainsKey("prolife") ?
                _settings.NodeAttrAttack["prolife"] as JArray : new JArray();
            JArray impactConfidentialityObj = _settings.NodeAttrAttack.ContainsKey("impacts_confidentialityiality") ?
                _settings.NodeAttrAttack["impacts_confidentialityiality"] as JArray : new JArray();
            JArray impactIntegrityObj = _settings.NodeAttrAttack.ContainsKey("impacts_integrity") ?
                _settings.NodeAttrAttack["impacts_integrity"] as JArray : new JArray();
            JArray impactAvailabilityObj = _settings.NodeAttrAttack.ContainsKey("impacts_availability") ?
                _settings.NodeAttrAttack["impacts_availability"] as JArray : new JArray();
            JArray impactAccountableObj = _settings.NodeAttrAttack.ContainsKey("impacts_accountability") ?
                _settings.NodeAttrAttack["impacts_accountability"] as JArray : new JArray();

            setNodeAttributeData(complexObj, gridAttackComplex);
            setNodeAttributeData(prolifeObj, gridAttackProlife);
            setNodeAttributeData(impactConfidentialityObj, gridattackImpactToConfidentiality);
            setNodeAttributeData(impactIntegrityObj, gridattackImpactToIntegrity);
            setNodeAttributeData(impactAvailabilityObj, gridattackImpactToAvailability);
            setNodeAttributeData(impactAccountableObj, gridattackImpactToAccountability);
        }

        private void initAssetNodeAttribute()
        {
            JArray confidentialityObj = _settings.NodeAttrAsset.ContainsKey("confidentiality") ?
                _settings.NodeAttrAsset["confidentiality"] as JArray : new JArray();
            JArray integrityObj = _settings.NodeAttrAsset.ContainsKey("integrity") ?
                _settings.NodeAttrAsset["integrity"] as JArray : new JArray();
            JArray avilibilityObj = _settings.NodeAttrAsset.ContainsKey("availibility") ?
                _settings.NodeAttrAsset["availibility"] as JArray : new JArray();
            JArray accountableObj = _settings.NodeAttrAsset.ContainsKey("accountable") ?
                _settings.NodeAttrAsset["accountable"] as JArray : new JArray();
            JArray financialImpactObj = _settings.NodeAttrAsset.ContainsKey("financial_impact") ?
                _settings.NodeAttrAsset["financial_impact"] as JArray : new JArray();
            JArray reputationalImpactObj = _settings.NodeAttrAsset.ContainsKey("reputational_impact") ?
                _settings.NodeAttrAsset["reputational_impact"] as JArray : new JArray();
            JArray regulatoryImpact = _settings.NodeAttrAsset.ContainsKey("regulatory_impact") ?
                _settings.NodeAttrAsset["regulatory_impact"] as JArray : new JArray();
            JArray legalImpactObj = _settings.NodeAttrAsset.ContainsKey("legal_impact") ?
                _settings.NodeAttrAsset["legal_impact"] as JArray : new JArray();
            JArray privacyImpactObj = _settings.NodeAttrAsset.ContainsKey("privacy_impact") ?
                _settings.NodeAttrAsset["privacy_impact"] as JArray : new JArray();

            setNodeAttributeData(confidentialityObj, gridAssetConfidentiality);
            setNodeAttributeData(integrityObj, gridAssetIntegrity);
            setNodeAttributeData(avilibilityObj, gridAssetAvailibility);
            setNodeAttributeData(accountableObj, gridAssetAccountability);
            setNodeAttributeData(financialImpactObj, gridAssetFinancialImpact);
            setNodeAttributeData(reputationalImpactObj, gridAssetReputationalImpact);
            setNodeAttributeData(regulatoryImpact, gridAssetRegulatoryImpact);
            setNodeAttributeData(legalImpactObj, gridAssetLegalImpact);
            setNodeAttributeData(privacyImpactObj, gridAssetPrivacyImpact);

        }

        private void initVulnerabilityNodeAttribute()
        {
            JArray easeObj = _settings.NodeAttrVulnerability.ContainsKey("ease") ?
                    _settings.NodeAttrVulnerability["ease"] as JArray : new JArray();
            JArray exposureObj = _settings.NodeAttrVulnerability.ContainsKey("exposure") ?
                                _settings.NodeAttrVulnerability["exposure"] as JArray : new JArray();
            JArray impactConfObj = _settings.NodeAttrVulnerability.ContainsKey("impacts_confidentiality") ?
                                _settings.NodeAttrVulnerability["impacts_confidentiality"] as JArray : new JArray();
            JArray impactIntegrityObj = _settings.NodeAttrVulnerability.ContainsKey("impacts_integrity") ?
                                _settings.NodeAttrVulnerability["impacts_integrity"] as JArray : new JArray();
            JArray impactAvailabilityObj = _settings.NodeAttrVulnerability.ContainsKey("impacts_availability") ?
                                _settings.NodeAttrVulnerability["impacts_availability"] as JArray : new JArray();
            JArray impactAccountabilityObj = _settings.NodeAttrVulnerability.ContainsKey("impacts_accountability") ?
                                _settings.NodeAttrVulnerability["impacts_accountability"] as JArray : new JArray();
            JArray privilegesObj = _settings.NodeAttrVulnerability.ContainsKey("privileges_required") ?
                                _settings.NodeAttrVulnerability["privileges_required"] as JArray : new JArray();
            JArray interactionObj = _settings.NodeAttrVulnerability.ContainsKey("interaction_required") ?
                                _settings.NodeAttrVulnerability["interaction_required"] as JArray : new JArray();
            JArray exposesScopeObj = _settings.NodeAttrVulnerability.ContainsKey("exposes_scope") ?
                                _settings.NodeAttrVulnerability["exposes_scope"] as JArray : new JArray();
            JArray regulatoryImpactObj = _settings.NodeAttrVulnerability.ContainsKey("regulatory_impact") ?
                                _settings.NodeAttrVulnerability["regulatory_impact"] as JArray : new JArray();

            setNodeAttributeData(easeObj, gridNodeVulnerabilityEase);
            setNodeAttributeData(exposureObj, gridNodeVulnerabilityExposure);
            setNodeAttributeData(impactConfObj, gridNodeVulnerabilityImpactsConfident);
            setNodeAttributeData(impactIntegrityObj, gridNodeVulnerabilityImpactsIntegrity);
            setNodeAttributeData(impactAvailabilityObj, gridNodeVulnerabilityImpactsAvailability);
            setNodeAttributeData(impactAccountabilityObj, gridNodeVulnerabilityImpactsAccountability);
            setNodeAttributeData(privilegesObj, gridNodeVulnerabilityPrivileges);
            setNodeAttributeData(interactionObj, gridNodeVulnerabilityInteractionRequired);
            setNodeAttributeData(exposesScopeObj, gridNodeVulnerabilityExposesScope);
            setNodeAttributeData(regulatoryImpactObj, gridNodeVulnerabilityRegulatoryImpact);
        }

        private void resetDontShow()
        {
            _settings.ShowDeleteTreeDialog = true;
            _settings.ShowDeleteNodeDialog = true;
            _settings.ShowClearAllEdgesDialog = true;
            _settings.ShowClearAllNodesAndLinks = true;
            _settings.ShowClearAllLinksFromNode = true;
            _settings.ShowClearAllLinksToNode = true;
            _settings.ShowClearAllEdges = true;
            _settings.ShowRelayoutDialog = true;
            _settings.ShowMaxFontSizeReached = true;
        }

        private void ResetDontShow_button_Click(object sender, EventArgs e)
        {
            resetDontShow();
        }

       
        private void addEdgeRelationship()
        {
            EdgeRelationshipModal edgeRelModal = new EdgeRelationshipModal();
            if (edgeRelModal.ShowDialog(this) == DialogResult.OK)
            {
                this.listEdgeRelations.Items.Add(edgeRelModal.EdgeRelationData);
            }
        }

        private void removeEdgeRelationship()
        {
            if (this.listEdgeRelations.SelectedItem != null)
            {
                this.listEdgeRelations.Items.Remove(this.listEdgeRelations.SelectedItem);
            }
        }

        private void sfButton1_Click(object sender, EventArgs e)
        {
            addEdgeRelationship();
        }

        private void sfButton2_Click(object sender, EventArgs e)
        {
            removeEdgeRelationship();
        }

        private void addNodeAsset()
        {

            NodeAssetModal nodeAssetModal = new NodeAssetModal();
            if (nodeAssetModal.ShowDialog(this) == DialogResult.OK)
            {
                node_control_assets_list.Add(new NodeAsset(nodeAssetModal.AssessmentStatus, nodeAssetModal.AssessmentValue,
                    nodeAssetModal.AssessmentDesc
                    /*,"rgb(" + nodeAssetModal.AssessmentColor.R + "," + nodeAssetModal.AssessmentColor.G + "," + nodeAssetModal.AssessmentColor.B + ")"*/));
            }
            SetDataToNodeControlDataGrid(dataGridControlImplementation, node_control_assets_list, "asset");
        }

        private void btnAddNodeAsset_Click(object sender, EventArgs e)
        {
            addNodeAsset();
        }

        private void editNodeAsset()
        {
            if (dataGridControlImplementation.SelectedRows.Count == 0)
            {
                return;
            }

            NodeAssetModal nodeAssetModal = new NodeAssetModal();
            NodeAsset obj = node_control_assets_list[dataGridControlImplementation.SelectedRows[0].Index] as NodeAsset;

            nodeAssetModal.AssessmentStatus = obj.Strength;
            nodeAssetModal.AssessmentValue = obj.Value;
            nodeAssetModal.AssessmentDesc = obj.Description; ;
            if (nodeAssetModal.ShowDialog(this) == DialogResult.OK)
            {
                node_control_assets_list[dataGridControlImplementation.SelectedRows[0].Index] = new NodeAsset(
                    nodeAssetModal.AssessmentStatus, nodeAssetModal.AssessmentValue,
                    nodeAssetModal.AssessmentDesc
                    /*,"rgb(" + nodeAssetModal.AssessmentColor.R + "," + nodeAssetModal.AssessmentColor.G + "," + nodeAssetModal.AssessmentColor.B + ")"*/);
            }
            SetDataToNodeControlDataGrid(dataGridControlImplementation, node_control_assets_list, "asset");
        }

        private void btnEditNodeAsset_Click(object sender, EventArgs e)
        {
            editNodeAsset();
        }

        private void removeNodeAsset()
        {
            node_control_assets_list.RemoveAt(dataGridControlImplementation.SelectedRows[0].Index);
            SetDataToNodeControlDataGrid(dataGridControlImplementation, node_control_assets_list, "asset");
        }

        private void btnRemoveNodeAsset_Click(object sender, EventArgs e)
        {
            removeNodeAsset();
        }

        private void addNodeStrength()
        {
            NodeStrengthModal strengthModal = new NodeStrengthModal();
            if (strengthModal.ShowDialog(this) == DialogResult.OK)
            {
                NodeControl tmp = new NodeControl(
                    strengthModal.NodeStrength, strengthModal.Strength, strengthModal.Description
                    /*,"rgb(" + strengthModal.AssColor.R + "," + strengthModal.AssColor.G + "," + strengthModal.AssColor.B + ")"*/);
                node_control_strength_list.Add(tmp);
            }
            SetDataToNodeControlDataGrid(dataGridControlStrength, node_control_strength_list);
        }

        private void SetDataToEdgeDataGrid(DataGridView grid, JArray arr, string type = "value")
        {
            grid.Rows.Clear();
            for (int i = 0; i < arr.Count; i++)
            {
                if (type == "edge_strength")
                {
                    JObject tmp = arr[i] as JObject;
                    if (tmp != null)
                    {
                        string strength = tmp.ContainsKey("strength") ? tmp["strength"].ToString() : "";
                        string value = tmp.ContainsKey("value") ? tmp["value"].ToString() : "";
                        string description = tmp.ContainsKey("description") ? tmp["description"].ToString() : "";
                        grid.Rows.Add(strength, value, description);
                    }
                }
                else if (type == "edge_display")
                {
                    JObject tmp = arr[i] as JObject;
                    if (tmp != null)
                    {
                        string value_from = tmp.ContainsKey("valueFrom") ? tmp["valueFrom"].ToString() : "";
                        string value_to = tmp.ContainsKey("valueTo") ? tmp["valueTo"].ToString() : "";
                        string width = tmp.ContainsKey("width") ? tmp["width"].ToString() : "";
                        //string color = tmp.ContainsKey("color") ? tmp["color"].ToString() : "rgb(0,0,0)";
                        grid.Rows.Add(value_from, value_to, width, "");
                    }
                }
            }
        }
        private void SetDataToNodeControlDataGrid(DataGridView grid, ArrayList arr, string type = "value")
        {
            grid.Rows.Clear();
            for (int i = 0; i < arr.Count; i++)
            {
                if (type == "value")
                {
                    NodeControl tmp = arr[i] as NodeControl;
                    if (tmp != null)
                    {
                        grid.Rows.Add(tmp.ControlStrength, tmp.Strength, tmp.Description, "");
                    }
                }
                else if(type == "asset")
                {
                    NodeAsset tmp = arr[i] as NodeAsset;
                    if (tmp != null)
                    {
                        grid.Rows.Add(tmp.Strength, tmp.Value, tmp.Description, "");
                    }
                }
            }
        }

        private void btnAddNodeStrength_Click(object sender, EventArgs e)
        {
            addNodeStrength();
        }

        private void editNodeStrength()
        {
            if (dataGridControlStrength.SelectedRows.Count == 0)
            {
                return;
            }

            int selected_row_index = dataGridControlStrength.SelectedRows[0].Index;
            NodeControl obj = node_control_strength_list[selected_row_index] as NodeControl;
            NodeStrengthModal strengthModal = new NodeStrengthModal();
            
            strengthModal.NodeStrength = obj.ControlStrength;
            strengthModal.Strength = obj.Strength;
            strengthModal.Description = obj.Description;
            
            if (strengthModal.ShowDialog(this) == DialogResult.OK)
            {
                node_control_strength_list[dataGridControlStrength.SelectedRows[0].Index] =
                    new NodeControl(strengthModal.NodeStrength, strengthModal.Strength,
                    strengthModal.Description
                    /*,"rgb(" + strengthModal.AssColor.R + "," + strengthModal.AssColor.G + "," + strengthModal.AssColor.B + ")"*/);

            }
  
            SetDataToNodeControlDataGrid(dataGridControlStrength, node_control_strength_list);
        }

        private void btnEditNodeStrength_Click(object sender, EventArgs e)
        {
            editNodeStrength();
        }

        private void removeNodeStrength()
        {
            node_control_strength_list.RemoveAt(dataGridControlStrength.SelectedRows[0].Index);
            SetDataToNodeControlDataGrid(dataGridControlStrength, node_control_strength_list);
        }

        private void btnRemoveNodeStrength_Click(object sender, EventArgs e)
        {
            removeNodeStrength();
        }

        private void addKeyWord()
        {
            KeyWordForm tmpForm = new KeyWordForm();
            if (tmpForm.ShowDialog(this) == DialogResult.OK)
            {
                this.listKeyWords.ListBox.Items.Add(tmpForm.KeyWordText);
            }
        }

        private void btnAddKeyWord_Click(object sender, EventArgs e)
        {
            addKeyWord();
        }

        private void removeKeyWord()
        {
            if (this.listKeyWords.ListBox.SelectedItem != null)
            {
                this.listKeyWords.ListBox.Items.Remove(this.listKeyWords.ListBox.SelectedItem);
            }
        }

        private void btnRemoveKeyWord_Click(object sender, EventArgs e)
        {
            removeKeyWord();
        }

        private void editKeyWord()
        {
            if (this.listKeyWords.ListBox.SelectedIndex < 0)
            {
                return;
            }

            KeyWordForm tmpForm = new KeyWordForm();
            tmpForm.KeyWordText = this.listKeyWords.ListBox.Items[this.listKeyWords.ListBox.SelectedIndex].ToString();
            if (tmpForm.ShowDialog(this) == DialogResult.OK)
            {
                this.listKeyWords.ListBox.Items[this.listKeyWords.ListBox.SelectedIndex] = tmpForm.KeyWordText;
            }
        }

        private void btnEditKeyWord_Click(object sender, EventArgs e)
        {
            editKeyWord();
        }

        private void SettingsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            _settings.OpenLastDocumentOnStart = OpenLast_checkBox.Checked;
            _settings.AutoSaveOnChanges = AutoSave_checkBox.Checked;
            _settings.AutoCenterGraph = AutoCenterGraph_checkbox.Checked;
            _settings.AutoSaveOnTimer = AutoSaveTimer_Checkbox.Checked;
            //_settings.AutoSelectLastNode = AlwaysSelectLastNode_checkBox.Checked;
            _settings.ShowImportPreview = ShowImportPreview_checkBox.Checked;
            _settings.RestoreWindowStateOnStart = RestoreWindow_checkBox.Checked;
            _settings.Save();

            string json_data = GetNodeEdgeGridData();
            StreamWriter sw = new StreamWriter(@"Graph\cy.data");
            sw.WriteLine(json_data);
            sw.Close();

            JObject cyJsonFileData = json_data == "" ? null : JObject.Parse(json_data);
            JArray keyWords = (JArray)cyJsonFileData["key_words"];

            JObject attackAttr = cyJsonFileData.ContainsKey("attack_value") ? (JObject)cyJsonFileData["attack_value"] : new JObject();
            JObject assetAttr = cyJsonFileData.ContainsKey("asset_value") ? (JObject)cyJsonFileData["asset_value"] : new JObject();
            JObject vulnerabilityAttr = cyJsonFileData.ContainsKey("vulnerability_value") ? (JObject)cyJsonFileData["vulnerability_value"] : new JObject();

            // handle cloud function
            string settings_guid = SettingsAPI.GetSettingsGUID();
            SettingsAPI.PutSettingAttackValues(attackAttr);
            SettingsAPI.PutSettingsVulnerabilityValues(vulnerabilityAttr);
            SettingsAPI.PutSettingsKeywords(JsonConvert.SerializeObject(keyWords));
            SettingsAPI.PutSettingsCategoryValues(node_categories.ToString());
            SettingsAPI.PutSettingsAssetValues(assetAttr);
        }

        private void defaultColor()
        { 
        }

        private void DefaultColor_panel_Click(object sender, EventArgs e)
        {
            defaultColor();
        }

        private void importKeywords()
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "Image Files(*.csv)|*.csv";
            string fileContent = "";
            if (open.ShowDialog() == DialogResult.OK)
            {
                var fileStream = open.OpenFile();
                using (StreamReader reader = new StreamReader(fileStream))
                {
                    fileContent = reader.ReadToEnd();
                    string[] content_arr = fileContent.Split('\r', '\n');
                    importCSVtoKeyWords(content_arr);
                }
            }
        }

        private void btnImportKeywords_Click(object sender, EventArgs e)
        {
            importKeywords();
        }

        public void importCSVtoKeyWords(string[] content_arr)
        {
            foreach (string word in content_arr)
            {
                if (word != "" && listKeyWords.ListBox.FindString(word) == -1)
                {
                    this.listKeyWords.ListBox.Items.Add(word);
                }
            }
        }

        private void btnAddAttackComplex_Click(object sender, EventArgs e)
        {
            addNodeAttribute(gridAttackComplex, "Attack Nodes");
        }

        private void btnEditAttackComplex_Click(object sender, EventArgs e)
        {
            editNodeAttribute(gridAttackComplex, "Attack Nodes");
        }

        private void btnRemoveAttackComplex_Click(object sender, EventArgs e)
        {
            removeNodeAttribute(gridAttackComplex);
        }

        private void btnAddAttackProlife_Click(object sender, EventArgs e)
        {
            addNodeAttribute(gridAttackProlife, "Attack Nodes");
        }

        private void btnEditAttackProlife_Click(object sender, EventArgs e)
        {
            editNodeAttribute(gridAttackProlife, "Attack Nodes");
        }

        private void btnRemoveAttackProlife_Click(object sender, EventArgs e)
        {
            removeNodeAttribute(gridAttackProlife);
        }

        private void btnAddActorCapability_Click(object sender, EventArgs e)
        {
            addNodeAttribute(gridActorCapability, "Actor Nodes");
        }

        private void btnEditActorCapability_Click(object sender, EventArgs e)
        {
            editNodeAttribute(gridActorCapability, "Actor Nodes");
        }

        private void btnRemoveActorCapability_Click(object sender, EventArgs e)
        {
            removeNodeAttribute(gridActorCapability);
        }

        private void btnAddActorResources_Click(object sender, EventArgs e)
        {
            addNodeAttribute(gridActorResources, "Actor Nodes");
        }

        private void btnEditActorResources_Click(object sender, EventArgs e)
        {
            editNodeAttribute(gridActorResources, "Actor Nodes");
        }

        private void btnRemoveActorResources_Click(object sender, EventArgs e)
        {
            removeNodeAttribute(gridActorResources);
        }

        private void btnAddActorMotivation_Click(object sender, EventArgs e)
        {
            addNodeAttribute(gridActorMotivation, "Actor Nodes");
        }

        private void btnEditActorMotivation_Click(object sender, EventArgs e)
        {
            editNodeAttribute(gridActorMotivation, "Actor Nodes");
        }

        private void btnRemoveActorMotivation_Click(object sender, EventArgs e)
        {
            removeNodeAttribute(gridActorMotivation);
        }

        private void btnAddActorAccess_Click(object sender, EventArgs e)
        {
            addNodeAttribute(gridActorAccess, "Actor Nodes");
        }

        private void btnEditActorAccess_Click(object sender, EventArgs e)
        {
            editNodeAttribute(gridActorAccess, "Actor Nodes");
        }

        private void btnRemoveActorAccess_Click(object sender, EventArgs e)
        {
            removeNodeAttribute(gridActorAccess);
        }

        private void addNodeAttribute(Syncfusion.Windows.Forms.Grid.GridListControl gridList, string title = "Asset Nodes")
        {
            ArrayList array = new ArrayList();
            for (int i = 0; i < gridList.Items.Count; i++)
            {
                NodeAttribute tmpAttr = (NodeAttribute)gridList.Items[i];
                array.Add(new NodeAttribute(tmpAttr.Title, tmpAttr.Value, tmpAttr.Description));
            }

            NodeAttributeModal nodeAttrModal = new NodeAttributeModal();
            nodeAttrModal.Text = title;
            if (nodeAttrModal.ShowDialog(this) == DialogResult.OK)
            {
                array.Add(new NodeAttribute(nodeAttrModal.AttrImpact, nodeAttrModal.AttrValue, nodeAttrModal.AttrDesc));
            }
            gridList.DataSource = array;
        }

        private void editNodeAttribute(Syncfusion.Windows.Forms.Grid.GridListControl gridList, string title = "Asset Nodes")
        {
            if (gridList.SelectedIndex == -1)
            {
                return;
            }

            ArrayList array = new ArrayList();
            for (int i = 0; i < gridList.Items.Count; i++)
            {
                NodeAttribute tmpAttr = (NodeAttribute)gridList.Items[i];
                array.Add(new NodeAttribute(tmpAttr.Title, tmpAttr.Value, tmpAttr.Description));
            }

            NodeAttributeModal nodeAttrModal = new NodeAttributeModal();
            NodeAttribute obj = (NodeAttribute)gridList.SelectedItem;
            nodeAttrModal.Text = title;
            nodeAttrModal.AttrImpact = obj.Title;
            nodeAttrModal.AttrValue = obj.Value;
            nodeAttrModal.AttrDesc = obj.Description;
            if (nodeAttrModal.ShowDialog(this) == DialogResult.OK)
            {
                array[gridList.SelectedIndex] = new NodeAttribute(nodeAttrModal.AttrImpact, nodeAttrModal.AttrValue, nodeAttrModal.AttrDesc);
            }
            gridList.DataSource = array;
        }

        private void removeNodeAttribute(Syncfusion.Windows.Forms.Grid.GridListControl gridList)
        {
            if (gridList.SelectedIndex < 0 || gridList.SelectedIndex > (gridList.Items.Count - 1))
            {
                return;
            }

            ArrayList array = new ArrayList();
            for (int i = 0; i < gridList.Items.Count; i++)
            {
                NodeAttribute tmpAttr = (NodeAttribute)gridList.Items[i];
                array.Add(new NodeAttribute(tmpAttr.Title, tmpAttr.Value, tmpAttr.Description));
            }
            array.RemoveAt(gridList.SelectedIndex);
            gridList.DataSource = array;
        }

        private void btnAddAssetConfidentiality_Click(object sender, EventArgs e)
        {
            addNodeAttribute(gridAssetConfidentiality, "Asset Node Confidentiality");
        }

        private void btnEditAssetConfidentiality_Click(object sender, EventArgs e)
        {
            editNodeAttribute(gridAssetConfidentiality, "Asset Node Confidentiality");
        }

        private void btnRemoveAssetConfidentiality_Click(object sender, EventArgs e)
        {
            removeNodeAttribute(gridAssetConfidentiality);
        }

        private void btnAddAssetIntegrity_Click(object sender, EventArgs e)
        {
            addNodeAttribute(gridAssetIntegrity, "Asset Node Integrity");
        }

        private void btnEditAssetIntegrity_Click(object sender, EventArgs e)
        {
            editNodeAttribute(gridAssetIntegrity, "Asset Node Integrity");
        }

        private void btnRemoveAssetIntegrity_Click(object sender, EventArgs e)
        {
            removeNodeAttribute(gridAssetIntegrity);
        }

        private void btnAddAssetAvailibility_Click(object sender, EventArgs e)
        {
            addNodeAttribute(gridAssetAvailibility, "Asset Node Availibility");
        }

        private void btnEditAssetAvailibility_Click(object sender, EventArgs e)
        {
            editNodeAttribute(gridAssetAvailibility, "Asset Node Availibility");
        }

        private void btnRemoveAssetAvailibility_Click(object sender, EventArgs e)
        {
            removeNodeAttribute(gridAssetAvailibility);
        }

        private void btnAddAssetAccount_Click(object sender, EventArgs e)
        {
            addNodeAttribute(gridAssetAccountability, "Asset Node Accountability");
        }

        private void btnEditAssetAccount_Click(object sender, EventArgs e)
        {
            editNodeAttribute(gridAssetAccountability, "Asset Node Accountability");
        }

        private void btnRemoveAssetAccount_Click(object sender, EventArgs e)
        {
            removeNodeAttribute(gridAssetAccountability);
        }

        private string getNodeAttrData(string attr, Syncfusion.Windows.Forms.Grid.GridListControl gridList, bool flag = true)
        {
            string tmp_data = flag ? "\"" + attr + "\":[" : "[";
            string comma = "";
            for (int i = 0; i < gridList.Items.Count; i++)
            {
                NodeAttribute nodeAttr = (NodeAttribute)gridList.Items[i];
                if (nodeAttr.Title.Trim() == "")
                {
                    continue;
                }

                string tmp_str = "{";
                tmp_str += "\"impact\":\"" + nodeAttr.Title + "\",";
                tmp_str += "\"value\":\"" + nodeAttr.Value + "\",";
                tmp_str += "\"description\":\"" + nodeAttr.Description + "\"";
                tmp_str += "}";
                tmp_data += comma + tmp_str;
                comma = ",";
            }
            tmp_data += "]";
            return tmp_data;
        }

        private void addNodePrimaryCategory_Btn_Click(object sender, EventArgs e)
        {
            addNodeCategory(nodePrimaryCategory_Grid);
        }

        private void addNodeCategory(Syncfusion.Windows.Forms.Grid.GridListControl gridList, string parent = "")
        {
            ArrayList array = new ArrayList();
            for (int i = 0; i < gridList.Items.Count; i++)
            {
                NodeCategory tmpCategory = gridList.Items[i] as NodeCategory;
                array.Add(tmpCategory);
            }

            NodeCategoryModal modal = new NodeCategoryModal();
            if (gridList.Name == "nodeSubCategory_Grid")
            {
                modal.Text = "Sub-Catagory";
                modal.txtCaption.Text = "Sub-Catagory";
            }
            else
            {
                modal.Text = "Catagory";
                modal.txtCaption.Text = "Catagory";
            }
            if (modal.ShowDialog(this) == DialogResult.OK)
            {
                for (int i = 0; i < gridList.Items.Count; i++)
                {
                    NodeCategory nc = gridList.Items[i] as NodeCategory;
                    if (nc.NodeCategoryData == modal.NodeCategoryText || modal.NodeCategoryText == "")
                    {
                        MessageBox.Show("The category is duplicated or empty!");
                        return;
                    }
                }
                array.Add(new NodeCategory(modal.NodeCategoryText, parent));
            }

            // update node category
            if (parent == "")
            {
                JObject obj = new JObject();
                obj["text"] = modal.NodeCategoryText;
                obj["childrens"] = new JArray();
                node_categories.Add(obj);
            }
            else
            {
                for (var i = 0; i < node_categories.Count; i++)
                {
                    JObject tmp_obj = node_categories[i] as JObject;
                    if (tmp_obj["text"].ToString().ToLower() == parent.ToLower())
                    {
                        JArray tmp_arr = node_categories[i]["childrens"] as JArray;
                        tmp_arr.Add(modal.NodeCategoryText);
                    }
                }
            }

            gridList.DataSource = array;
        }

        private void editNodeCategory(Syncfusion.Windows.Forms.Grid.GridListControl gridList, string parent = "")
        {
            if (gridList.SelectedIndex == -1)
            {
                return;
            }

            ArrayList array = new ArrayList();
            for (int i = 0; i < gridList.Items.Count; i++)
            {
                NodeCategory tmpAttr = (NodeCategory)gridList.Items[i];
                array.Add(tmpAttr);
            }

            NodeCategoryModal modal = new NodeCategoryModal();
            NodeCategory obj = (NodeCategory)gridList.SelectedItem;
            modal.NodeCategoryText = obj.NodeCategoryData;
            string origin_text = "";
            if (modal.ShowDialog(this) == DialogResult.OK)
            {
                origin_text = (gridList.SelectedItem as NodeCategory).NodeCategoryData;
                array[gridList.SelectedIndex] = new NodeCategory(modal.NodeCategoryText, parent);
            }

            for (var i = 0; i < node_categories.Count; i++)
            {
                JObject tmp_obj = node_categories[i] as JObject;
                if (parent == "")
                {
                    if (tmp_obj["text"].ToString().ToLower() == origin_text.ToLower())
                    {
                        node_categories[i]["text"] = modal.NodeCategoryText;
                        return;
                    }
                }
                else
                {
                    for (var j = 0; j < (tmp_obj["childrens"] as JArray).Count; j++)
                    {
                        if (tmp_obj["childrens"][j].ToString().ToLower() == origin_text.ToLower())
                        {
                            node_categories[i]["childrens"][j] = modal.NodeCategoryText;
                            return;
                        }
                    }
                }
            }

            gridList.DataSource = array;
        }

        private void removeNodeCategory(Syncfusion.Windows.Forms.Grid.GridListControl gridList, string parent = "")
        {
            if (gridList.SelectedIndex == -1 || gridList.SelectedIndex > (gridList.Items.Count - 1))
            {
                return;
            }

            ArrayList array = new ArrayList();
            for (int i = 0; i < gridList.Items.Count; i++)
            {
                NodeCategory tmpAsset = (NodeCategory)gridList.Items[i];
                array.Add(tmpAsset);
            }

            for (var i = 0; i < node_categories.Count; i++)
            {
                JObject tmp_obj = node_categories[i] as JObject;
                if (parent == "")
                {
                    if (tmp_obj["text"].ToString().ToLower() == (gridList.SelectedItem as NodeCategory).NodeCategoryData)
                    {
                        node_categories.RemoveAt(i);
                        break;
                    }
                }
                else
                {
                    for (var j = 0; j < (tmp_obj["childrens"] as JArray).Count; j++)
                    {
                        if (tmp_obj["childrens"][j].ToString().ToLower() == (gridList.SelectedItem as NodeCategory).NodeCategoryData)
                        {
                            (node_categories[i]["childrens"] as JArray).Remove(j);
                            break;
                        }
                    }
                }
            }

            array.RemoveAt(gridList.SelectedIndex);
            gridList.DataSource = array;
        }

        private void editNodePrimaryCategory_Btn_Click(object sender, EventArgs e)
        {
            editNodeCategory(nodePrimaryCategory_Grid);
        }

        private void removeNodePrimaryCategory_Btn_Click(object sender, EventArgs e)
        {
            removeNodeCategory(nodePrimaryCategory_Grid);
        }

        private void addNodeSubCategory_Btn_Click(object sender, EventArgs e)
        {
            if (nodePrimaryCategory_Grid.SelectedItem == null)
            {
                NetGraphMessageBox.MessageBoxEx(this, "Please select the main category", "Not selected main category", MessageBoxButtons.OK, MessageBoxIconEx.Information);
                return;
            }
            addNodeCategory(nodeSubCategory_Grid, (nodePrimaryCategory_Grid.SelectedItem as NodeCategory).NodeCategoryData);
        }

        private void editNodeSubCategory_Btn_Click(object sender, EventArgs e)
        {
            editNodeCategory(nodeSubCategory_Grid, (nodePrimaryCategory_Grid.SelectedItem as NodeCategory).NodeCategoryData);
        }

        private void removeNodeSubCategory_Btn_Click(object sender, EventArgs e)
        {
            removeNodeCategory(nodeSubCategory_Grid, (nodePrimaryCategory_Grid.SelectedItem as NodeCategory).NodeCategoryData);
        }

        private void nodePrimaryCategory_SelectedValueChanged(object sender, EventArgs e)
        {
            string data = (nodePrimaryCategory_Grid.SelectedItem as NodeCategory).NodeCategoryData;
            initNodeSubCategories(data);
        }

        private void gridAssetConfidentiality_Click(object sender, EventArgs e)
        {
            enableAssetButtons("confidentiality");
        }

        private void gridAssetIntegrity_Click(object sender, EventArgs e)
        {
            enableAssetButtons("integrity");
        }

        private void gridAssetAvailibility_Click(object sender, EventArgs e)
        {
            enableAssetButtons("availibility");
        }

        private void gridAssetAccountability_Click(object sender, EventArgs e)
        {
            enableAssetButtons("accountability");
        }

        private void enableAssetButtons(string type)
        {
            int asset_confidentiality_selected = gridAssetConfidentiality.SelectedIndex;
            int asset_integrity_selected = gridAssetIntegrity.SelectedIndex;
            int asset_account_selected = gridAssetAccountability.SelectedIndex;
            int asset_availibility_selected = gridAssetAvailibility.SelectedIndex;

            btnEditAssetConfidentiality.Enabled = type == "confidentiality" && asset_confidentiality_selected >= 0 ? true : false;
            btnRemoveAssetConfidentiality.Enabled = type == "confidentiality" && asset_confidentiality_selected >= 0 ? true : false;

            btnEditAssetIntegrity.Enabled = type == "integrity" && asset_integrity_selected >= 0 ? true : false;
            btnRemoveAssetIntegrity.Enabled = type == "integrity" && asset_integrity_selected >= 0 ? true : false;

            btnEditAssetAvailibility.Enabled = type == "availibility" && asset_confidentiality_selected >= 0 ? true : false;
            btnRemoveAssetAvailibility.Enabled = type == "availibility" && asset_confidentiality_selected >= 0 ? true : false;

            btnEditAssetAccount.Enabled = type == "accountability" && asset_account_selected >= 0 ? true : false;
            btnRemoveAssetAccount.Enabled = type == "accountability" && asset_account_selected >= 0 ? true : false;
        }

        private void gridActorCapability_Click(object sender, EventArgs e)
        {
            enableActorButtons("capability");
        }

        private void gridActorResources_Click(object sender, EventArgs e)
        {
            enableActorButtons("resources");
        }

        private void gridActorMotivation_Click(object sender, EventArgs e)
        {
            enableActorButtons("motivation");
        }

        private void gridActorAccess_Click(object sender, EventArgs e)
        {
            enableActorButtons("access");
        }
        private void enableActorButtons(string type)
        {
            int actor_capability_selected = gridActorCapability.SelectedIndex;
            int actor_resources_selected = gridActorResources.SelectedIndex;
            int actor_motivation_selected = gridActorMotivation.SelectedIndex;
            int actor_access_selected = gridActorAccess.SelectedIndex;

            btnEditActorCapability.Enabled = type == "capability" && actor_capability_selected >= 0 ? true : false;
            btnRemoveActorCapability.Enabled = type == "capability" && actor_capability_selected >= 0 ? true : false;

            btnEditActorResources.Enabled = type == "resources" && actor_resources_selected >= 0 ? true : false;
            btnRemoveActorResources.Enabled = type == "resources" && actor_resources_selected >= 0 ? true : false;

            btnEditActorMotivation.Enabled = type == "motivation" && actor_motivation_selected >= 0 ? true : false;
            btnRemoveActorMotivation.Enabled = type == "motivation" && actor_motivation_selected >= 0 ? true : false;

            btnEditActorAccess.Enabled = type == "access" && actor_access_selected >= 0 ? true : false;
            btnRemoveActorAccess.Enabled = type == "access" && actor_access_selected >= 0 ? true : false;
        }

        private void gridAttackComplex_Click(object sender, EventArgs e)
        {
            enableAttackButtons("complex");
        }

        private void gridAttackProlife_Click(object sender, EventArgs e)
        {
            enableAttackButtons("prolife");
        }

        private void enableAttackButtons(string type)
        {
            int attack_complex_selected = gridAttackComplex.SelectedIndex;
            int attack_prolife_selected = gridAttackProlife.SelectedIndex;
            int attack_impacts_confidentialityiality_selected = gridattackImpactToConfidentiality.SelectedIndex;
            int attack_impacts_integrity_selected = gridattackImpactToIntegrity.SelectedIndex;
            int attack_impacts_availability_selected = gridattackImpactToAvailability.SelectedIndex;
            int attack_impacts_accountability_selected = gridattackImpactToAccountability.SelectedIndex;

            btnEditAttackComplex.Enabled = type == "complex" && attack_complex_selected >= 0 ? true : false;
            btnRemoveAttackComplex.Enabled = type == "complex" && attack_complex_selected >= 0 ? true : false;

            btnEditAttackProlife.Enabled = type == "prolife" && attack_prolife_selected >= 0 ? true : false;
            btnRemoveAttackProlife.Enabled = type == "prolife" && attack_prolife_selected >= 0 ? true : false;

            btnEditattackImpactToConfidentiality.Enabled = type == "impacts_confidentialityiality" && attack_impacts_confidentialityiality_selected >= 0 ? true : false;
            btnRemoveattackImpactToConfidentiality.Enabled = type == "impacts_confidentialityiality" && attack_impacts_confidentialityiality_selected >= 0 ? true : false;

            btnEditattackImpactToIntegrity.Enabled = type == "impacts_integrity" && attack_impacts_integrity_selected >= 0 ? true : false;
            btnRemoveattackImpactToIntegrity.Enabled = type == "impacts_integrity" && attack_impacts_integrity_selected >= 0 ? true : false;

            btnEditattackImpactToAvailability.Enabled = type == "impacts_availability" && attack_impacts_availability_selected >= 0 ? true : false;
            btnRemoveattackImpactToAvailability.Enabled = type == "impacts_availability" && attack_impacts_availability_selected >= 0 ? true : false;

            btnEditattackImpactToAccountability.Enabled = type == "impacts_accountability" && attack_impacts_accountability_selected >= 0 ? true : false;
            btnRemoveattackImpactToAccountability.Enabled = type == "impacts_accountability" && attack_impacts_accountability_selected >= 0 ? true : false;
        }

        private void enableNodeControlButtons(string type)
        {
            try
            {
                int strength_selected = dataGridControlStrength.SelectedRows.Count;// .Rows[0].Index;
                int asset_selected = dataGridControlImplementation.SelectedRows.Count;// .Rows[0].Index;

                btnEditNodeControlStrength.Enabled = type == "value" && strength_selected >= 0 ? true : false;
                btnRemoveNodeControlStrength.Enabled = type == "value" && strength_selected >= 0 ? true : false;

                btnEditNodeAsset.Enabled = type == "asset" && asset_selected >= 0 ? true : false;
                btnRemoveNodeAsset.Enabled = type == "asset" && asset_selected >= 0 ? true : false;
            }
            catch { }
        }

        private void btnAddattackImpactToConfidentiality_Click(object sender, EventArgs e)
        {
            addNodeAttribute(gridattackImpactToConfidentiality, "Attack Nodes");
        }

        private void btnEditattackImpactToConfidentiality_Click(object sender, EventArgs e)
        {
            editNodeAttribute(gridattackImpactToConfidentiality, "Attack Nodes");
        }

        private void btnRemoveattackImpactToConfidentiality_Click(object sender, EventArgs e)
        {
            removeNodeAttribute(gridattackImpactToConfidentiality);
        }

        private void btnAddattackImpactToIntegrity_Click(object sender, EventArgs e)
        {
            addNodeAttribute(gridattackImpactToIntegrity, "Attack Nodes");
        }

        private void btnEditattackImpactToIntegrity_Click(object sender, EventArgs e)
        {
            editNodeAttribute(gridattackImpactToIntegrity, "Attack Nodes");
        }

        private void btnRemoveattackImpactToIntegrity_Click(object sender, EventArgs e)
        {
            removeNodeAttribute(gridattackImpactToIntegrity);
        }

        private void btnAddattackImpactToAvailability_Click(object sender, EventArgs e)
        {
            addNodeAttribute(gridattackImpactToAvailability, "Attack Nodes");
        }

        private void btnEditattackImpactToAvailability_Click(object sender, EventArgs e)
        {
            editNodeAttribute(gridattackImpactToAvailability, "Attack Nodes");
        }

        private void btnRemoveattackImpactToAvailability_Click(object sender, EventArgs e)
        {
            removeNodeAttribute(gridattackImpactToAvailability);
        }

        private void btnAddattackImpactToAccountability_Click(object sender, EventArgs e)
        {
            addNodeAttribute(gridattackImpactToAccountability, "Attack Nodes");
        }

        private void btnEditattackImpactToAccountability_Click(object sender, EventArgs e)
        {
            editNodeAttribute(gridattackImpactToAccountability, "Attack Nodes");
        }

        private void btnRemoveattackImpactToAccountability_Click(object sender, EventArgs e)
        {
            removeNodeAttribute(gridattackImpactToAccountability);
        }

        private void gridattackImpactToConfidentiality_Click(object sender, EventArgs e)
        {
            enableAttackButtons("impacts_confidentialityiality");
        }

        private void gridattackImpactToIntegrity_Click(object sender, EventArgs e)
        {
            enableAttackButtons("impacts_integrity");
        }

        private void gridattackImpactToAvailability_Click(object sender, EventArgs e)
        {
            enableAttackButtons("impacts_availability");
        }

        private void gridattackImpactToAccountability_Click(object sender, EventArgs e)
        {
            enableAttackButtons("impacts_accountability");
        }

        private void btnEdgeRelationshipEdit_Click(object sender, EventArgs e)
        {
            editEdgeRelationship();
        }

        private void editEdgeRelationship()
        {
            EdgeRelationshipModal edgeRelModal = new EdgeRelationshipModal();
            edgeRelModal.EdgeRelationData = this.listEdgeRelations.SelectedItem.ToString();
            if (edgeRelModal.ShowDialog(this) == DialogResult.OK)
            {
                this.listEdgeRelations.SelectedValue = edgeRelModal.EdgeRelationData;
            }
        }

        private void btnAddNodeVulEase_Click(object sender, EventArgs e)
        {
            addNodeAttribute(gridNodeVulnerabilityEase, "Ease of Explotation");
        }

        private void btnEditNodeVulEase_Click(object sender, EventArgs e)
        {
            editNodeAttribute(gridNodeVulnerabilityEase, "Ease of Explotation");
        }

        private void btnRemoveNodeVulEase_Click(object sender, EventArgs e)
        {
            removeNodeAttribute(gridNodeVulnerabilityEase);
        }

        private void btnAddNodeVulExposure_Click(object sender, EventArgs e)
        {
            addNodeAttribute(gridNodeVulnerabilityExposure, "Exposure To Attack");
        }

        private void btnEditNodeVulExposure_Click(object sender, EventArgs e)
        {
            editNodeAttribute(gridNodeVulnerabilityExposure, "Exposure To Attack");
        }

        private void btnRemoveNodeVulExposure_Click(object sender, EventArgs e)
        {
            removeNodeAttribute(gridNodeVulnerabilityExposure);
        }

        private void btnAddNodeVulImpactsConf_Click(object sender, EventArgs e)
        {
            addNodeAttribute(gridNodeVulnerabilityImpactsConfident, "Impacts Confident");
        }

        private void btnEditNodeVulImpactsConf_Click(object sender, EventArgs e)
        {
            editNodeAttribute(gridNodeVulnerabilityImpactsConfident, "Impacts Confident");
        }

        private void btnRemoveNodeVulImpactsConf_Click(object sender, EventArgs e)
        {
            removeNodeAttribute(gridNodeVulnerabilityImpactsConfident);
        }

        private void btnAddNodeVulImpactsIntegrity_Click(object sender, EventArgs e)
        {
            addNodeAttribute(gridNodeVulnerabilityImpactsIntegrity, "Impacts Integrity");
        }

        private void btnEditNodeVulImpactsIntegrity_Click(object sender, EventArgs e)
        {
            editNodeAttribute(gridNodeVulnerabilityImpactsIntegrity, "Impacts Integrity");
        }

        private void btnRemoveNodeVulImpactsIntegrity_Click(object sender, EventArgs e)
        {
            removeNodeAttribute(gridNodeVulnerabilityImpactsIntegrity);
        }

        private void btnAddNodeVulImpactsAvailability_Click(object sender, EventArgs e)
        {
            addNodeAttribute(gridNodeVulnerabilityImpactsAvailability, "Impacts Availability");
        }

        private void btnEditNodeVulImpactsAvailability_Click(object sender, EventArgs e)
        {
            editNodeAttribute(gridNodeVulnerabilityImpactsAvailability, "Impacts Availability");
        }

        private void btnRemoveNodeVulImpactsAvailability_Click(object sender, EventArgs e)
        {
            removeNodeAttribute(gridNodeVulnerabilityImpactsAvailability);
        }

        private void btnAddNodeVulImpactsAccountability_Click(object sender, EventArgs e)
        {
            addNodeAttribute(gridNodeVulnerabilityImpactsAccountability, "Impacts Accountability");
        }

        private void btnEditNodeVulImpactsAccountability_Click(object sender, EventArgs e)
        {
            editNodeAttribute(gridNodeVulnerabilityImpactsAccountability, "Impacts Accountability");
        }

        private void btnRemoveNodeVulImpactsAccountability_Click(object sender, EventArgs e)
        {
            removeNodeAttribute(gridNodeVulnerabilityImpactsAccountability);
        }

        private void enableVulnerability(string type)
        {
            int ease_selected = gridNodeVulnerabilityEase.SelectedIndex;
            int exposure_selected = gridNodeVulnerabilityExposure.SelectedIndex;
            int impact_conf_selected = gridNodeVulnerabilityImpactsConfident.SelectedIndex;
            int impacts_integrity_selected = gridNodeVulnerabilityImpactsIntegrity.SelectedIndex;
            int impacts_availability_selected = gridNodeVulnerabilityImpactsAvailability.SelectedIndex;
            int impacts_accountability_selected = gridNodeVulnerabilityImpactsAccountability.SelectedIndex;
            int privacy_selected = gridNodeVulnerabilityPrivileges.SelectedIndex;
            int interaction_selected = gridNodeVulnerabilityInteractionRequired.SelectedIndex;
            int exposes_selected = gridNodeVulnerabilityExposesScope.SelectedIndex;
            int regulatory_selected = gridNodeVulnerabilityRegulatoryImpact.SelectedIndex;  

            btnEditNodeVulEase.Enabled = type == "ease" && ease_selected >= 0 ? true : false;
            btnRemoveNodeVulEase.Enabled = type == "ease" && ease_selected >= 0 ? true : false;

            btnEditNodeVulExposure.Enabled = type == "exposure" && exposure_selected >= 0 ? true : false;
            btnRemoveNodeVulExposure.Enabled = type == "exposure" && exposure_selected >= 0 ? true : false;

            btnEditNodeVulImpactsConf.Enabled = type == "impactsconf" && impact_conf_selected >= 0 ? true : false;
            btnRemoveNodeVulImpactsConf.Enabled = type == "impactsconf" && impact_conf_selected >= 0 ? true : false;

            btnEditNodeVulImpactsIntegrity.Enabled = type == "impactsintegrity" && impacts_integrity_selected >= 0 ? true : false;
            btnRemoveNodeVulImpactsIntegrity.Enabled = type == "impactsintegrity" && impacts_integrity_selected >= 0 ? true : false;

            btnEditNodeVulImpactsAvailability.Enabled = type == "impactsavailability" && impacts_availability_selected >= 0 ? true : false;
            btnRemoveNodeVulImpactsAvailability.Enabled = type == "impactsavailability" && impacts_availability_selected >= 0 ? true : false;

            btnEditNodeVulImpactsAccountability.Enabled = type == "impactsaccountability" && impacts_accountability_selected >= 0 ? true : false;
            btnRemoveNodeVulImpactsAccountability.Enabled = type == "impactsaccountability" && impacts_accountability_selected >= 0 ? true : false;

            btnEditNodeVulPriv.Enabled = type == "privileges" && privacy_selected>= 0 ? true : false;
            btnRemoveNodeVulPriv.Enabled = type == "privileges" && privacy_selected >= 0 ? true : false;

            btnEditNodeVulInteraction.Enabled = type == "interactionRequired" && interaction_selected>= 0 ? true : false;
            btnRemoveNodeVulInteraction.Enabled = type == "interactionRequired" && interaction_selected >= 0 ? true : false;

            btnEditNodeVulExposes.Enabled = type == "exposesScope" && exposes_selected>= 0 ? true : false;
            btnRemoveNodeVulExposes.Enabled = type == "exposesScope" && exposes_selected >= 0 ? true : false;

            btnEditVulNodeRegulatoryImpact.Enabled = type == "regulatoryImpact" && regulatory_selected >= 0 ? true : false; 
            btnRemoveVulNodeRegulatoryImpact.Enabled = type == "regulatoryImpact" && regulatory_selected >= 0 ? true : false;
        }

        private void gridNodeVulnerabilityEase_Click(object sender, EventArgs e)
        {
            enableVulnerability("ease");
        }

        private void gridNodeVulnerabilityExposure_Click(object sender, EventArgs e)
        {
            enableVulnerability("exposure");
        }

        private void gridNodeVulnerabilityImpactsConfident_Click(object sender, EventArgs e)
        {
            enableVulnerability("impactsconf");
        }

        private void gridNodeVulnerabilityImpactsIntegrity_Click(object sender, EventArgs e)
        {
            enableVulnerability("impactsintegrity");
        }

        private void gridNodeVulnerabilityImpactsAvailability_Click(object sender, EventArgs e)
        {
            enableVulnerability("impactsavailability");
        }

        private void gridNodeVulnerabilityImpactsAccountability_Click(object sender, EventArgs e)
        {
            enableVulnerability("impactsaccountability");
        }

        private void btnAddAssetFinancialImpact_Click(object sender, EventArgs e)
        {
            addNodeAttribute(gridAssetFinancialImpact, "Asset Node Financial Impact");
        }

        private void btnEditAssetFinancialImpact_Click(object sender, EventArgs e)
        {
            editNodeAttribute(gridAssetFinancialImpact, "Asset Node Financial Impact");
        }

        private void btnRemoveAssetFinancialImpact_Click(object sender, EventArgs e)
        {
            removeNodeAttribute(gridAssetFinancialImpact);
        }

        private void btnAddAssetReputationalImpact_Click(object sender, EventArgs e)
        {
            addNodeAttribute(gridAssetReputationalImpact, "Asset Node Reputational Impact");
        }

        private void btnEditAssetReputationalImpact_Click(object sender, EventArgs e)
        {
            editNodeAttribute(gridAssetReputationalImpact, "Asset Node Reputational Impact");
        }

        private void btnDeleteAssetReputationalImpact_Click(object sender, EventArgs e)
        {
            removeNodeAttribute(gridAssetReputationalImpact);
        }

        private void btnAddAssetLegalImpact_Click(object sender, EventArgs e)
        {
            addNodeAttribute(gridAssetLegalImpact, "Asset Node Legal Impact");
        }

        private void btnDeleteAssetLegalImpact_Click(object sender, EventArgs e)
        {
            editNodeAttribute(gridAssetLegalImpact, "Asset Node Legal Impact");
        }

        private void btnRemoveAssetLegalImpact_Click(object sender, EventArgs e)
        {
            removeNodeAttribute(gridAssetLegalImpact );
        }

        private void btnAddAssetPrivacyImpact_Click(object sender, EventArgs e)
        {
            addNodeAttribute(gridAssetPrivacyImpact, "Asset Node Privacy Impact");
        }

        private void btnEditAssetPrivacyImpact_Click(object sender, EventArgs e)
        {
            editNodeAttribute(gridAssetPrivacyImpact, "Asset Node Privacy Impact");
        }

        private void btnDeleteAssetPrivacyImpact_Click(object sender, EventArgs e)
        {
            removeNodeAttribute(gridAssetPrivacyImpact );
        }

        private void btnAddAssetRegulatoryImpact_Click(object sender, EventArgs e)
        {
            addNodeAttribute(gridAssetRegulatoryImpact, "Asset Node Regulatory Impact");
        }

        private void btnEditAssetRegulatoryImpact_Click(object sender, EventArgs e)
        {
            editNodeAttribute(gridAssetRegulatoryImpact, "Asset Node Regulatory Impact");
        }

        private void btnDeleteAssetRegulatoryImpact_Click(object sender, EventArgs e)
        {
            removeNodeAttribute(gridAssetRegulatoryImpact );
        }

        private void dataGridControlStrength_Click(object sender, EventArgs e)
        {
            enableNodeControlButtons("value");

        }

        private void dataGridControlImplementation_Click(object sender, EventArgs e)
        {
            enableNodeControlButtons("asset");

        }

        private void btnAddNodeVulPriv_Click(object sender, EventArgs e)
        {
            addNodeAttribute(gridNodeVulnerabilityPrivileges, "Privileges Required");
        }

        private void btnAddNodeVulInteraction_Click(object sender, EventArgs e)
        {
            addNodeAttribute(gridNodeVulnerabilityInteractionRequired, "Interaction Required");
        }

        private void btnAddNodeVulExposes_Click(object sender, EventArgs e)
        {
            addNodeAttribute(gridNodeVulnerabilityExposesScope, "Exposes Scope");
        }

        private void btnEditNodeVulPriv_Click(object sender, EventArgs e)
        {
            editNodeAttribute(gridNodeVulnerabilityPrivileges, "Privileges Required");
        }

        private void btnEditNodeVulInteraction_Click(object sender, EventArgs e)
        {
            editNodeAttribute(gridNodeVulnerabilityInteractionRequired, "Interaction Required");
        }

        private void btnEditNodeVulExposes_Click(object sender, EventArgs e)
        {
            editNodeAttribute(gridNodeVulnerabilityExposesScope, "Exposes Scope");
        }

        private void btnRemoveNodeVulPriv_Click(object sender, EventArgs e)
        {
            removeNodeAttribute(gridNodeVulnerabilityPrivileges);
        }

        private void btnRemoveNodeVulInteraction_Click(object sender, EventArgs e)
        {
            removeNodeAttribute(gridNodeVulnerabilityInteractionRequired);
        }

        private void btnRemoveNodeVulExposes_Click(object sender, EventArgs e)
        {
            removeNodeAttribute(gridNodeVulnerabilityExposesScope);
        }

        private void btnAddVulNodeRegulatoryImpact_Click(object sender, EventArgs e)
        {
            addNodeAttribute(gridNodeVulnerabilityRegulatoryImpact, "Regulatory Impact");
        }

        private void btnEditVulNodeRegulatoryImpact_Click(object sender, EventArgs e)
        {
            editNodeAttribute(gridNodeVulnerabilityRegulatoryImpact, "Regulatory Impact");
        }

        private void btnRemoveVulNodeRegulatoryImpact_Click(object sender, EventArgs e)
        {
            removeNodeAttribute(gridNodeVulnerabilityRegulatoryImpact);
        }

        private void btnAddactorImpactToConfidentiality_Click(object sender, EventArgs e)
        {
            addNodeAttribute(gridactorImpactToConfidentiality, "Actor Impacts Confidentiality");
        }

        private void btnEditactorImpactToConfidentiality_Click(object sender, EventArgs e)
        {
            editNodeAttribute(gridactorImpactToConfidentiality, "Actor Impacts Confidentiality");
        }

        private void btnRemoveactorImpactToConfidentiality_Click(object sender, EventArgs e)
        {
            removeNodeAttribute(gridactorImpactToConfidentiality);
        }

        private void btnAddactorImpactToIntegrity_Click(object sender, EventArgs e)
        {
            addNodeAttribute(gridactorImpactToIntegrity, "Actor Impacts Integrity");
        }

        private void btnEditactorImpactToIntegrity_Click(object sender, EventArgs e)
        {
            editNodeAttribute(gridactorImpactToIntegrity, "Actor Impacts Integrity");
        }

        private void btnRemoveactorImpactToIntegrity_Click(object sender, EventArgs e)
        {
            removeNodeAttribute(gridactorImpactToIntegrity);
        }

        private void btnAddactorImpactToAvailability_Click(object sender, EventArgs e)
        {
            addNodeAttribute(gridactorImpactToAvailability, "Actor Impacts Availability");
        }

        private void btnEditactorImpactToAvailability_Click(object sender, EventArgs e)
        {
            editNodeAttribute(gridactorImpactToAvailability, "Actor Impacts Availability");
        }

        private void btnRemoveactorImpactToAvailability_Click(object sender, EventArgs e)
        {
            removeNodeAttribute(gridactorImpactToAvailability);
        }

        private void btnAddactorImpactToAccountability_Click(object sender, EventArgs e)
        {
            addNodeAttribute(gridactorImpactToAccountability, "Actor Impacts Accountability");
        }

        private void btnEditactorImpactToAccountability_Click(object sender, EventArgs e)
        {
            editNodeAttribute(gridactorImpactToAccountability, "Actor Impacts Accountability");
        }

        private void btnRemoveactorImpactToAccountability_Click(object sender, EventArgs e)
        {
            removeNodeAttribute(gridactorImpactToAccountability);
        }

        private void gridNodeVulnerabilityPrivileges_Click(object sender, EventArgs e)
        {
            enableVulnerability("privileges");
        }

        private void gridNodeVulnerabilityInteractionRequired_Click(object sender, EventArgs e)
        {
            enableVulnerability("interactionRequired");
        }

        private void gridNodeVulnerabilityExposesScope_Click(object sender, EventArgs e)
        {
            enableVulnerability("exposesScope");
        }

        private void gridNodeVulnerabilityRegulatoryImpact_Click(object sender, EventArgs e)
        {
            enableVulnerability("regulatoryImpact");
        }

        private void dataGridControlStrength_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnNodeControlStrengthUp_Click(object sender, EventArgs e)
        {
            ChangeOrderNodeControlStrength("up");
        }

        private void btnNodeControlDown_Click(object sender, EventArgs e)
        {
            ChangeOrderNodeControlStrength("down");
        }

        private void btnNodeControlAssetUp_Click(object sender, EventArgs e)
        {
            ChangeOrderNodeControlAsset("up");
        }

        private void btnNodeControlAssetDown_Click(object sender, EventArgs e)
        {
            ChangeOrderNodeControlAsset("down");
        }

        private void ChangeOrderNodeControlAsset(string dir)
        {
            int selected_index = dataGridControlImplementation.SelectedRows[0].Index;
            int temp_index = -1;
            int row_count = dataGridControlImplementation.Rows.Count;
            
            if (dir == "up")
            {
                temp_index = selected_index - 1;
            }
            else
            {
                temp_index = selected_index + 1;
            }
            if (selected_index == -1 || temp_index < 0 || temp_index >= row_count) return;

            NodeAsset selected_item = node_control_assets_list[selected_index] as NodeAsset;
            NodeAsset temp_item = node_control_assets_list[temp_index] as NodeAsset;
            node_control_assets_list[selected_index] = temp_item;
            node_control_assets_list[temp_index] = selected_item;

            SetDataToNodeControlDataGrid(dataGridControlImplementation, node_control_assets_list, "asset");
     
        }

        private void ChangeOrderNodeControlStrength(string dir)
        {
            int selected_index = dataGridControlStrength.SelectedRows[0].Index;
            int temp_index = -1;
            int row_count = dataGridControlStrength.Rows.Count;

            if (dir == "up")
            {
                temp_index = selected_index - 1;
            }
            else
            {
                temp_index = selected_index + 1;
            }
            if (selected_index == -1 || temp_index < 0 || temp_index >= row_count) return;

            NodeControl selected_item = node_control_strength_list[selected_index] as NodeControl;
            NodeControl temp_item = node_control_strength_list[temp_index] as NodeControl;
            node_control_strength_list[selected_index] = temp_item;
            node_control_strength_list[temp_index] = selected_item;

            SetDataToNodeControlDataGrid(dataGridControlStrength, node_control_strength_list);
        }

        private void EnableOrderControlBtns(DataGridView grid, SfButton upBtn, SfButton downBtn )
        {
            if (grid == null) return;

            int selected_index = grid.SelectedRows[0].Index;
            int row_count = grid.Rows.Count;

            if (selected_index == -1 || row_count < 2 )
            {
                upBtn.Enabled = false;
                downBtn.Enabled = false;
                return;
            }

            if (selected_index == 0)
            {
                upBtn.Enabled = false;
                downBtn.Enabled = true;
            }else if (selected_index == row_count - 1)
            {
                upBtn.Enabled = true;
                downBtn.Enabled = false;
            }
            else
            {
                upBtn.Enabled = true;
                downBtn.Enabled = true;
            }
        }

        private void GetSettingsValue()
        {
            _settings.OpenLastDocumentOnStart = OpenLast_checkBox.Checked;
            _settings.AutoSaveOnChanges = AutoSave_checkBox.Checked;
            _settings.RestoreWindowStateOnStart = RestoreWindow_checkBox.Checked;
            _settings.AutoSaveOnTimer = AutoSaveTimer_Checkbox.Checked;

            _settings.AutoCenterGraph = AutoCenterGraph_checkbox.Checked;
            _settings.ShowImportPreview = ShowImportPreview_checkBox.Checked;

            string cyFileData = GetNodeEdgeGridData();

            JObject cyJsonFileData = cyFileData == "" ? null : JObject.Parse(cyFileData);
            string edgeRelationData = cyJsonFileData["edge_relationship"].ToString();
            JArray nodeAssets = (JArray)cyJsonFileData["node_assets"];
            JArray edgeStrengthData = cyJsonFileData.ContainsKey("EdgeStrengthList") ? (JArray)cyJsonFileData["EdgeStrengthList"] : new JArray();
            JArray edgeDisplayData = cyJsonFileData.ContainsKey("EdgeDisplayList") ? (JArray)cyJsonFileData["EdgeDisplayList"] : new JArray();
            JArray nodeStrength = (JArray)cyJsonFileData["impact"];
            JArray keyWords = (JArray)cyJsonFileData["key_words"];

            JObject attackAttr = cyJsonFileData.ContainsKey("attack_value") ? (JObject)cyJsonFileData["attack_value"] : new JObject();
            JObject actorAttr = cyJsonFileData.ContainsKey("actor_value") ? (JObject)cyJsonFileData["actor_value"] : new JObject();
            JObject assetAttr = cyJsonFileData.ContainsKey("asset_value") ? (JObject)cyJsonFileData["asset_value"] : new JObject();
            JObject vulnerabilityAttr = cyJsonFileData.ContainsKey("vulnerability_value") ? (JObject)cyJsonFileData["vulnerability_value"] : new JObject();

            node_categories = cyJsonFileData.ContainsKey("node_categories") ? (JArray)cyJsonFileData["node_categories"] : new JArray();
            default_edge_relations = cyJsonFileData.ContainsKey("default_edge_relation") ? (JArray)cyJsonFileData["default_edge_relation"] : new JArray();

            _settings.EdgeRelationdata = edgeRelationData;
            _settings.NodeimplementedStrengthData = nodeAssets;
            _settings.EdgeStrengthList = edgeStrengthData;
            _settings.EdgeDisplayList = edgeDisplayData;
            _settings.NodeinherentStrengthData = nodeStrength;
            _settings.KeyWordsData = keyWords;
            _settings.NodeAttrActor = actorAttr;
            _settings.NodeAttrAttack = attackAttr;
            _settings.NodeAttrAsset = assetAttr;
            _settings.NodeAttrVulnerability = vulnerabilityAttr;
        }

        private string GetNodeEdgeGridData()
        {
            string tmp_data = "";
            string comma = "";
            string json_data = "{\"edge_relationship\":\"";
            for (int i = 0; i < listEdgeRelations.Items.Count; i++)
            {
                tmp_data = tmp_data + comma + listEdgeRelations.Items[i].ToString();
                comma = ",";
            }
            _settings.EdgeRelationdata = tmp_data;
            json_data += tmp_data + "\"";

            comma = "";
            tmp_data = "\"node_assets\":[";

            for (int i = 0; i < dataGridControlImplementation.Rows.Count; i++)
            {
                NodeAsset tmpAsset = new NodeAsset(
                    dataGridControlImplementation.Rows[i].Cells[0].Value.ToString(),
                    dataGridControlImplementation.Rows[i].Cells[1].Value.ToString(),
                    dataGridControlImplementation.Rows[i].Cells[2].Value.ToString()
                    /*,dataGridControlImplementation.Rows[i].Cells[3].Value.ToString()*/);

                string tmp_str = "{";
                tmp_str += "\"impact\":\"" + tmpAsset.Strength + "\",";
                tmp_str += "\"value\":\"" + tmpAsset.Value + "\",";
                //tmp_str += "\"color\":\"" + tmpAsset.Color + "\",";
                tmp_str += "\"description\":\"" + tmpAsset.Description + "\"";
                tmp_str += "}";
                tmp_data += comma + tmp_str;
                comma = ",";
            }
            tmp_data += "]";
            json_data += "," + tmp_data;

            tmp_data = "\"node_categories\": " + node_categories.ToString();
            json_data += "," + tmp_data;

            tmp_data = "\"default_edge_relation\":[";
            comma = "";
            for (int i = 0; i < defaultEdgeRelation_GridView.Rows.Count; i++)
            {
                string source_str = defaultEdgeRelation_GridView.Rows[i].Cells[0].Value.ToString();
                string target_str = defaultEdgeRelation_GridView.Rows[i].Cells[1].Value.ToString();
                string relation_str = defaultEdgeRelation_GridView.Rows[i].Cells[2].Value == null ? "" : defaultEdgeRelation_GridView.Rows[i].Cells[2].Value.ToString();
                string tmp_str = "{\"source\":\"" + source_str + "\",\"target\":\"" + target_str + "\",\"relation\":\"" + relation_str + "\"}";
                tmp_data += comma + tmp_str;
                comma = ",";
            }
            tmp_data += "]";
            json_data += "," + tmp_data;

            json_data += ",\"EdgeStrengthList\":[";
            tmp_data = "";
            comma = "";

            for (int i = 0; i < _settings.EdgeStrengthList.Count; i++)
            {
                JObject tmp_obj = _settings.EdgeStrengthList[i] as JObject;
                string strength = tmp_obj.ContainsKey("strength") ? tmp_obj["strength"].ToString() : "";
                string value = tmp_obj.ContainsKey("value") ? tmp_obj["value"].ToString() : "";
                string description = tmp_obj.ContainsKey("description") ? tmp_obj["description"].ToString() : "";
                string tmp_str = "{\"strength\":\"" + strength + "\",\"value\":\"" + value + "\",\"description\":\"" + description + "\"}";

                tmp_data += comma + tmp_str;
                comma = ",";
            }
            tmp_data += "]";
            json_data += tmp_data;

            json_data += ",\"EdgeDisplayList\":[";
            tmp_data = "";
            comma = "";

            for (int i = 0; i < _settings.EdgeDisplayList.Count; i++)
            {
                JObject tmp_obj = _settings.EdgeDisplayList[i] as JObject;
                string value_from = tmp_obj.ContainsKey("valueFrom") ? tmp_obj["valueFrom"].ToString() : "";
                string value_to = tmp_obj.ContainsKey("valueTo") ? tmp_obj["valueTo"].ToString() : "";
                string width = tmp_obj.ContainsKey("width") ? tmp_obj["width"].ToString() : "";
                string color = tmp_obj.ContainsKey("color") ? tmp_obj["color"].ToString() : "rgb(0,0,0)";
                string tmp_str = "{\"valueFrom\":\"" + value_from + "\",\"valueTo\":\"" + value_to + "\",\"width\":\"" + width + "\",\"color\":\"" + color + "\"}";

                tmp_data += comma + tmp_str;
                comma = ",";
            }
            tmp_data += "]";
            json_data += tmp_data;

            // set node attributes start
            JObject asset_obj = new JObject();
            json_data += ", \"asset_value\": {";
            // --------- asset node attribute start
            // **** asset node confidentiality attribute start
            tmp_data = getNodeAttrData("confidentiality", gridAssetConfidentiality);
            asset_obj["assetConfidentiality"] = getNodeAttrData("confidentiality", gridAssetConfidentiality, false);
            json_data += tmp_data;
            // **** asset node confidentiality attribute end

            // **** asset node integrity attribute start
            tmp_data = getNodeAttrData("integrity", gridAssetIntegrity);
            asset_obj["assetIntegrity"] = getNodeAttrData("integrity", gridAssetConfidentiality, false);
            json_data += "," + tmp_data;
            // **** asset node integrity attribute end

            // **** asset node availibility attribute start
            tmp_data = getNodeAttrData("availibility", gridAssetAvailibility);
            asset_obj["assetAvailability"] = getNodeAttrData("availibility", gridAssetAvailibility, false);
            json_data += "," + tmp_data;
            // **** asset node availibility attribute end

            // **** asset node accountable attribute start
            tmp_data = getNodeAttrData("accountable", gridAssetAccountability);
            asset_obj["assetAccountable"] = getNodeAttrData("accountable", gridAssetAccountability, false);
            json_data += "," + tmp_data;
            // **** asset node accountable attribute end

            // **** asset node financial impact attribute start
            tmp_data = getNodeAttrData("financial_impact", gridAssetFinancialImpact);
            asset_obj["assetFinancialImpact"] = getNodeAttrData("financial_impact", gridAssetFinancialImpact, false);
            json_data += "," + tmp_data;
            // **** asset node financial impact attribute end

            // **** asset node reputational impact attribute start
            tmp_data = getNodeAttrData("reputational_impact", gridAssetReputationalImpact);
            asset_obj["assetReputationalImpact"] = getNodeAttrData("reputational_impact", gridAssetReputationalImpact, false);
            json_data += "," + tmp_data;
            // **** asset node reputational impact attribute end

            // **** asset node regulatory impact attribute start
            tmp_data = getNodeAttrData("regulatory_impact", gridAssetRegulatoryImpact);
            asset_obj["assetRegulatoryImpact"] = getNodeAttrData("regulatory_impact", gridAssetRegulatoryImpact, false);
            json_data += "," + tmp_data;
            // **** asset node regulatory impact attribute end

            // **** asset node legal impact attribute start
            tmp_data = getNodeAttrData("legal_impact", gridAssetLegalImpact);
            asset_obj["assetLegalImpact"] = getNodeAttrData("legal_impact", gridAssetLegalImpact, false);
            json_data += "," + tmp_data;
            // **** asset node legal impact attribute end

            // **** asset node privacy impact attribute start
            tmp_data = getNodeAttrData("privacy_impact", gridAssetPrivacyImpact);
            asset_obj["assetPrivacyImpact"] = getNodeAttrData("privacy_impact", gridAssetPrivacyImpact, false);
            json_data += "," + tmp_data;
            // **** asset node privacy impact attribute end

            json_data += "}";
            // --------- asset node attribute end

            // --------- actor node attribute start
            json_data += ", \"actor_value\": {";
            // **** actor node capability attribute start
            tmp_data = getNodeAttrData("capability", gridActorCapability);
            json_data += tmp_data;
            // **** actor node capability attribute end

            // **** actor node resources attribute start
            tmp_data = getNodeAttrData("resources", gridActorResources);
            json_data += "," + tmp_data;
            // **** actor node resources attribute end

            // **** actor node motivation attribute start
            tmp_data = getNodeAttrData("motivation", gridActorMotivation);
            json_data += "," + tmp_data;
            // **** actor node motivation attribute end

            // **** actor node access attribute start
            tmp_data = getNodeAttrData("access", gridActorAccess);
            json_data += "," + tmp_data;
            // **** actor node access attribute end

            tmp_data = getNodeAttrData("impacts_confidentialityiality", gridactorImpactToConfidentiality);
            json_data += "," + tmp_data;

            tmp_data = getNodeAttrData("impacts_integrity", gridactorImpactToIntegrity);
            json_data += "," + tmp_data;

            tmp_data = getNodeAttrData("impacts_availability", gridactorImpactToAvailability);
            json_data += "," + tmp_data;

            tmp_data = getNodeAttrData("impacts_accountability", gridactorImpactToAccountability);
            json_data += "," + tmp_data;
            json_data += "}";
            // --------- actor node attribute end

            // --------- attack node attribute start
            JObject attack_obj = new JObject();
            json_data += ", \"attack_value\": {";
            // **** attack node complex attribute start
            tmp_data = getNodeAttrData("complex", gridAttackComplex);
            json_data += tmp_data;
            attack_obj["attackComplexity"] = getNodeAttrData("complex", gridAttackComplex, false);
            // **** attack node complex attribute end

            // **** attack node prolife attribute start
            tmp_data = getNodeAttrData("prolife", gridAttackProlife);
            json_data += "," + tmp_data;
            attack_obj["attackProliferation"] = getNodeAttrData("complex", gridAttackProlife, false);
            // **** attack node prolife attribute end

            // **** attack node impact confidentiality attribute start
            tmp_data = getNodeAttrData("impacts_confidentialityiality", gridattackImpactToConfidentiality);
            json_data += "," + tmp_data;
            attack_obj["attackImpactsConfidentiality"] = getNodeAttrData("impacts_confidentialityiality", gridattackImpactToConfidentiality, false);
            // **** attack node impact confidentiality attribute end

            // **** attack node impact integrity attribute start
            tmp_data = getNodeAttrData("impacts_integrity", gridattackImpactToIntegrity);
            json_data += "," + tmp_data;
            attack_obj["attackImpactsIntegrity"] = getNodeAttrData("impacts_integrity", gridattackImpactToIntegrity, false);
            // **** attack node impact integrity attribute end

            // **** attack node impact availability attribute start
            tmp_data = getNodeAttrData("impacts_availability", gridattackImpactToAvailability);
            json_data += "," + tmp_data;
            attack_obj["attackImpactsAvailibility"] = getNodeAttrData("impacts_availability", gridattackImpactToAvailability, false);
            // **** attack node impact availability attribute end

            // **** attack node impact accountable attribute start
            tmp_data = getNodeAttrData("impacts_accountability", gridattackImpactToAccountability);
            json_data += "," + tmp_data;
            attack_obj["attackImpactsAccountability"] = getNodeAttrData("impacts_accountability", gridattackImpactToAccountability, false);
            // **** attack node impact accountable attribute end
            // --------- attack node attribute end
            json_data += "}";

            // --------- vulnerability node attribute start
            JObject vul_obj = new JObject();
            json_data += ", \"vulnerability_value\": {";
            // **** vulnerability node ease attribute start
            tmp_data = getNodeAttrData("ease", gridNodeVulnerabilityEase);
            json_data += tmp_data;
            vul_obj["vulnerabilityEase"] = getNodeAttrData("ease", gridNodeVulnerabilityEase, false);
            // **** vulnerability node ease attribute end

            // **** vulnerability node exposure attribute start
            tmp_data = getNodeAttrData("exposure", gridNodeVulnerabilityExposure);
            json_data += "," + tmp_data;
            vul_obj["vulnerabilityExposure"] = getNodeAttrData("exposure", gridNodeVulnerabilityExposure);
            // **** vulnerability node exposure attribute end

            // **** vulnerability node impact confident attribute start
            tmp_data = getNodeAttrData("impacts_confidentiality", gridNodeVulnerabilityImpactsConfident);
            json_data += "," + tmp_data;
            vul_obj["vulnerabilityImpactsConfidentiality"] = getNodeAttrData("impacts_confidentiality", gridNodeVulnerabilityImpactsConfident, false);
            // **** vulnerability node impact confidnet attribute end

            // **** vulnerability node impact integrity attribute start
            tmp_data = getNodeAttrData("impacts_integrity", gridNodeVulnerabilityImpactsIntegrity);
            json_data += "," + tmp_data;
            vul_obj["vulnerabilityImpactsIntegrity"] = getNodeAttrData("impacts_integrity", gridNodeVulnerabilityImpactsIntegrity, false);
            // **** vulnerability node impact integrity attribute end

            // **** vulnerability node regularity impact integrity attribute start
            tmp_data = getNodeAttrData("regulatory_impact", gridNodeVulnerabilityRegulatoryImpact);
            vul_obj["vulnerabilityRegulatoryImpact"] = getNodeAttrData("regulatory_impact", gridNodeVulnerabilityImpactsConfident, false);
            json_data += "," + tmp_data;
            //vul_obj["vulnerability"]
            // **** vulnerability node impact integrity attribute end

            // **** vulnerability node impact availability attribute start
            tmp_data = getNodeAttrData("impacts_availability", gridNodeVulnerabilityImpactsAvailability);
            json_data += "," + tmp_data;
            vul_obj["vulnerabilityImpactsAvailibility"] = getNodeAttrData("impacts_availability", gridNodeVulnerabilityImpactsAvailability, false);
            // **** vulnerability node impact availability attribute end

            // **** vulnerability node impact accountable attribute start
            tmp_data = getNodeAttrData("impacts_accountability", gridNodeVulnerabilityImpactsAccountability);
            json_data += "," + tmp_data;
            vul_obj["vulnerabilityImpactsAccountability"] = getNodeAttrData("impacts_accountability", gridNodeVulnerabilityImpactsAccountability, false);
            // **** vulnerability node impact accountable attribute end

            // **** vulnerability node privileges_required attribute start
            tmp_data = getNodeAttrData("privileges_required", gridNodeVulnerabilityPrivileges);
            json_data += "," + tmp_data;
            vul_obj["vulnerabilityPrivilege"] = getNodeAttrData("privileges_required", gridNodeVulnerabilityPrivileges, false);
            // **** vulnerability node privileges_required attribute end

            // **** vulnerability node interaction_required attribute start
            tmp_data = getNodeAttrData("interaction_required", gridNodeVulnerabilityInteractionRequired);
            json_data += "," + tmp_data;
            vul_obj["vulnerabilityInteractive"] = getNodeAttrData("interaction_required", gridNodeVulnerabilityInteractionRequired, false);
            // **** vulnerability node interaction_required attribute end

            // **** vulnerability node exposes_scope attribute start
            tmp_data = getNodeAttrData("exposes_scope", gridNodeVulnerabilityExposesScope);
            json_data += "," + tmp_data;
            vul_obj["vulnerabilityExposes"] = getNodeAttrData("exposes_scope", gridNodeVulnerabilityExposesScope, false);
            // **** vulnerability node impact accountableexposes_scope attribute end

            // --------- vulnerability node attribute end
            json_data += "}";

            // set node attributes end

            json_data += ",\"impact\":[";
            tmp_data = "";
            comma = "";

            for (int i = 0; i < dataGridControlStrength.Rows.Count; i++)
            {
                NodeControl tmpControl = new NodeControl(
                    dataGridControlStrength.Rows[i].Cells[0].Value.ToString(),
                    dataGridControlStrength.Rows[i].Cells[1].Value.ToString(),
                    dataGridControlStrength.Rows[i].Cells[2].Value.ToString()/*,
                    dataGridControlStrength.Rows[i].Cells[3].Value.ToString()*/);

                string tmp_str = "{";
                tmp_str += "\"impact\":\"" + tmpControl.ControlStrength + "\",";
                tmp_str += "\"value\":\"" + tmpControl.Strength + "\",";
                //tmp_str += "\"color\":\"" + tmpControl.Color + "\",";
                tmp_str += "\"description\":\"" + tmpControl.Description + "\"";
                tmp_str += "}";
                tmp_data += comma + tmp_str;
                comma = ",";
            }
            tmp_data += "]";

            JObject keyword_obj = new JObject();
            json_data += tmp_data;
            json_data += ",\"key_words\":[";
            tmp_data = "";
            comma = "";
            for (int i = 0; i < listKeyWords.ListBox.Items.Count; i++)
            {
                tmp_data += comma + "\"" + listKeyWords.ListBox.Items[i].ToString() + "\"";
                comma = ",";
                keyword_obj[(i + 1)] = listKeyWords.ListBox.Items[i].ToString();
            }
            tmp_data += "]";
            json_data += tmp_data + "}";
            return json_data;
        }

        private void SetSettingsValue(string cyFileData)
        {
            try
            {
                JObject cyJsonFileData = cyFileData == "" ? null : JObject.Parse(cyFileData);
                string edgeRelationData = cyJsonFileData["EdgeRelationdata"].ToString();
                JArray nodeImplementStrength = (JArray)cyJsonFileData["NodeimplementedStrengthData"];
                JArray edgeStrengthData = cyJsonFileData.ContainsKey("EdgeStrengthList") ? (JArray)cyJsonFileData["EdgeStrengthList"] : new JArray();
                JArray edgeDisplayData = cyJsonFileData.ContainsKey("EdgeDisplayList") ? (JArray)cyJsonFileData["EdgeDisplayList"] : new JArray();
                JArray nodeStrength = (JArray)cyJsonFileData["NodeinherentStrengthData"];
                JArray keyWords = (JArray)cyJsonFileData["KeyWordsData"];

                JObject attackAttr = cyJsonFileData.ContainsKey("NodeAttrAttack") ? (JObject)cyJsonFileData["NodeAttrAttack"] : new JObject();
                JObject actorAttr = cyJsonFileData.ContainsKey("NodeAttrActor") ? (JObject)cyJsonFileData["NodeAttrActor"] : new JObject();
                JObject assetAttr = cyJsonFileData.ContainsKey("NodeAttrAsset") ? (JObject)cyJsonFileData["NodeAttrAsset"] : new JObject();
                JObject vulnerabilityAttr = cyJsonFileData.ContainsKey("NodeAttrVulnerability") ? (JObject)cyJsonFileData["NodeAttrVulnerability"] : new JObject();
                _settings.OpenLastDocumentOnStart = cyJsonFileData["OpenLastDocumentOnStart"].ToString() == "True";
                _settings.AutoSaveOnChanges = cyJsonFileData["AutoSaveOnChanges"].ToString() == "True";
                _settings.RestoreWindowStateOnStart = cyJsonFileData["RestoreWindowStateOnStart"].ToString() == "True";
                _settings.AutoSaveOnTimer = cyJsonFileData.ContainsKey("AutoSaveOnTimer") ? cyJsonFileData["AutoSaveOnTimer"].ToString().ToLower() == "true" : false;
                _settings.DefaultNodeSize = cyJsonFileData.ContainsKey("DefaultNodeSize") ? (decimal)cyJsonFileData["DefaultNodeSize"] : 100;
                _settings.AutoCenterGraph = cyJsonFileData.ContainsKey("AutoCenterGraph") ? cyJsonFileData["AutoCenterGraph"].ToString().ToLower() == "true" : false;
                _settings.ShowImportPreview = cyJsonFileData.ContainsKey("ShowImportPreview") ? cyJsonFileData["ShowImportPreview"].ToString().ToLower() == "true" : false;


                OpenLast_checkBox.Checked = _settings.OpenLastDocumentOnStart;
                AutoSave_checkBox.Checked = _settings.AutoSaveOnChanges;
                AutoSaveTimer_Checkbox.Checked = _settings.AutoSaveOnTimer;
                RestoreWindow_checkBox.Checked = _settings.RestoreWindowStateOnStart;
                AutoCenterGraph_checkbox.Checked = _settings.AutoCenterGraph;
                ShowImportPreview_checkBox.Checked = _settings.ShowImportPreview;
                

                _settings.EdgeRelationdata = edgeRelationData;
                string[] lines = _settings.EdgeRelationdata.Split(',');
                Array.Sort(lines);
                this.listEdgeRelations.Items.Clear();
                for (int i = 0; i < lines.Length; i++)
                {
                    this.listEdgeRelations.Items.Add(lines[i]);
                    edge_relationship.Items.Add(lines[i]);
                }

                _settings.NodeimplementedStrengthData = nodeImplementStrength;
                ArrayList array1 = new ArrayList();
                for (int i = 0; i < nodeImplementStrength.Count; i++)
                {
                    JObject tmp = (JObject)nodeImplementStrength[i];
                    string impact = tmp.ContainsKey("impact") ? tmp["impact"].ToString() : "";
                    string value = tmp.ContainsKey("value") ? tmp["value"].ToString() : "";
                    string desc = tmp.ContainsKey("description") ? tmp["description"].ToString() : "";
                    //string color = tmp.ContainsKey("color") ? tmp["color"].ToString() : "rgb(0,0,0)";
                    array1.Add(new NodeAsset(impact, value, desc/*, color*/));
                }

                _settings.EdgeStrengthList = edgeStrengthData;
                this.dataGridEdgeStrength.Rows.Clear();
                for (int i = 0; i < edgeStrengthData.Count; i++)
                {
                    JObject tmp = (JObject)edgeStrengthData[i];
                    string strength = tmp.ContainsKey("strength") ? tmp["strength"].ToString() : "";
                    string value = tmp.ContainsKey("value") ? tmp["value"].ToString() : "";
                    string desc = tmp.ContainsKey("description") ? tmp["description"].ToString() : "";
                    this.dataGridEdgeStrength.Rows.Add(strength, value, desc);
                }

                _settings.EdgeDisplayList = edgeDisplayData;
                this.dataGridEdgeDisplay.Rows.Clear();
                for (int i = 0; i < edgeDisplayData.Count; i++)
                {
                    JObject tmp = (JObject)edgeDisplayData[i];
                    string value_from = tmp.ContainsKey("valueFrom") ? tmp["valueFrom"].ToString() : "";
                    string value_to = tmp.ContainsKey("valueTo") ? tmp["valueTo"].ToString() : "";
                    string width = tmp.ContainsKey("width") ? tmp["width"].ToString() : "";
                    this.dataGridEdgeDisplay.Rows.Add(value_from, value_to, width, "");
                }

                _settings.NodeinherentStrengthData = nodeStrength;
                ArrayList array3 = new ArrayList();
                for (int i = 0; i < nodeStrength.Count; i++)
                {
                    JObject tmp = (JObject)nodeStrength[i];
                    string impact = tmp.ContainsKey("impact") ? tmp["impact"].ToString() : "";
                    string value = tmp.ContainsKey("value") ? tmp["value"].ToString() : "";
                    string desc = tmp.ContainsKey("description") ? tmp["description"].ToString() : "";
                    //string color = tmp.ContainsKey("color") ? tmp["color"].ToString() : "rgb(0,0,0)";
                    array3.Add(new NodeControl(impact, value, desc/*, color*/));
                }

                _settings.KeyWordsData = keyWords;
                ArrayList array4 = new ArrayList();
                for (int i = 0; i < keyWords.Count; i++)
                {
                    string tmp = keyWords[i].ToString();
                    this.listKeyWords.ListBox.Items.Add(tmp);
                }

                _settings.NodeAttrActor = actorAttr;
                _settings.NodeAttrAttack = attackAttr;
                _settings.NodeAttrAsset = assetAttr;
                _settings.NodeAttrVulnerability = vulnerabilityAttr;

                initActorNodeAttribute();
                initAttackNodeAttribute();
                initAssetNodeAttribute();
                initVulnerabilityNodeAttribute();
                initNodeCategories();
                initDefaultEdgeRelationship();

                node_control_strength_list = array3;
                node_control_assets_list = array1;
                SetDataToNodeControlDataGrid(dataGridControlStrength, node_control_strength_list);
                SetDataToNodeControlDataGrid(dataGridControlImplementation, node_control_assets_list, "asset");
            }
            catch
            {

            }
        }

        private void btnSaveSettings_Click(object sender, EventArgs e)
        {
            string file_name = "";
            using (SaveFileDialog saveFileDialog = new SaveFileDialog()
            {
                Filter = "All files (*.*)|*.*|Settings file (*.ini)|*.ini",
                FilterIndex = 2,
                Title = "Save Settings",
                FileName = String.IsNullOrEmpty(file_name) ? "layout.graph" : Path.GetFileName(file_name)
            })
            {
                if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
                {
                   GetSettingsValue();
                    _settings.Save(saveFileDialog.FileName);
                }
            }
        }

        private void btnLoadSettings_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFile = new OpenFileDialog()
            {
                Filter = "All files (*.*)|*.*|Settings file (*.ini)|*.ini",
                FilterIndex = 2,
                Title = "Open file for settings"
            })
            {
                if (openFile.ShowDialog(this) == DialogResult.OK)
                {
                    string file_data = System.IO.File.ReadAllText(openFile.FileName);
                    SetSettingsValue(file_data);
                }
            }
        }

        private void btnAddEdgeStrength_Click(object sender, EventArgs e)
        {
            AddEdgeStrengthItem();
        }

        private void AddEdgeStrengthItem()
        {
            EdgeStrengthModal modal = new EdgeStrengthModal();
            if (modal.ShowDialog(this) == DialogResult.OK)
            {
                dataGridEdgeStrength.Rows.Add(modal.AssessmentStatus, modal.AssessmentValue, modal.AssessmentDesc);
                JObject obj = new JObject();
                obj["strength"] = modal.AssessmentStatus;
                obj["value"] = modal.AssessmentValue;
                obj["description"] = modal.AssessmentDesc;
                _settings.EdgeStrengthList.Add(obj);
            }
        }

        private void btnEditEdgeStrength_Click(object sender, EventArgs e)
        {
            if (this.dataGridEdgeStrength.SelectedRows.Count > 0)
            {
                int selected_index = dataGridEdgeStrength.SelectedRows[0].Index;
                string strength = dataGridEdgeStrength.SelectedRows[0].Cells[0].Value.ToString();
                string value = dataGridEdgeStrength.SelectedRows[0].Cells[1].Value.ToString();
                string desc = dataGridEdgeStrength.SelectedRows[0].Cells[2].Value.ToString();

                EdgeStrengthModal modal = new EdgeStrengthModal();
                modal.AssessmentStatus = strength;
                modal.AssessmentValue = value;
                modal.AssessmentDesc = desc;
                if (modal.ShowDialog(this) == DialogResult.OK)
                {
                    dataGridEdgeStrength.Rows[selected_index].Cells[0].Value = modal.AssessmentStatus;
                    dataGridEdgeStrength.Rows[selected_index].Cells[1].Value = modal.AssessmentValue;
                    dataGridEdgeStrength.Rows[selected_index].Cells[2].Value = modal.AssessmentDesc;

                    JObject obj = new JObject();
                    obj["strength"] = modal.AssessmentStatus;
                    obj["value"] = modal.AssessmentValue;
                    obj["description"] = modal.AssessmentDesc;
                    _settings.EdgeStrengthList[selected_index] = obj;
                }
            }
        }

        private void btnRemoveEdgeStrength_Click(object sender, EventArgs e)
        {
            if (this.dataGridEdgeStrength.SelectedRows.Count != 0)
            {
                this.dataGridEdgeStrength.Rows.RemoveAt(this.dataGridEdgeStrength.SelectedRows[0].Index);
                _settings.EdgeStrengthList.RemoveAt(this.dataGridEdgeStrength.SelectedRows[0].Index);
            }
        }

        private void btnAddEdgeDisplay_Click(object sender, EventArgs e)
        {
            EdgeDisplayModal modal = new EdgeDisplayModal();
            if (modal.ShowDialog(this) == DialogResult.OK)
            {
                JObject obj = new JObject();
                obj["valueFrom"] = modal.EdgeDisplayValueFrom;
                obj["valueTo"] = modal.EdgeDisplayValueTo;
                obj["width"] = modal.EdgeDisplayWidth;
                obj["color"] = "rgb(" + modal.EdgeDisplayColor.R + "," + modal.EdgeDisplayColor.G + "," + modal.EdgeDisplayColor.B + ")";
                _settings.EdgeDisplayList.Add(obj);
                dataGridEdgeDisplay.Rows.Add(obj["valueFrom"], obj["valueTo"], obj["width"], "");
            }
        }

        private void btnEditEdgeDisplay_Click(object sender, EventArgs e)
        {
            if (this.dataGridEdgeDisplay.SelectedRows.Count > 0)
            {
                int selected_index = dataGridEdgeDisplay.SelectedRows[0].Index;
                string valueFrom = dataGridEdgeDisplay.SelectedRows[0].Cells[0].Value.ToString();
                string valueTo = dataGridEdgeDisplay.SelectedRows[0].Cells[1].Value.ToString();
                string width = dataGridEdgeDisplay.SelectedRows[0].Cells[2].Value.ToString();
                string color = (_settings.EdgeDisplayList[selected_index] as JObject)["color"].ToString();

                EdgeDisplayModal modal = new EdgeDisplayModal();
                modal.EdgeDisplayValueFrom = valueFrom;
                modal.EdgeDisplayValueTo = valueTo;
                modal.EdgeDisplayWidth = width;
                modal.EdgeDisplayColor = GeneralHelpers.ConvertColorFromHTML(color);
                if (modal.ShowDialog(this) == DialogResult.OK)
                {
                    dataGridEdgeDisplay.Rows[selected_index].Cells[0].Value = modal.EdgeDisplayValueFrom;
                    dataGridEdgeDisplay.Rows[selected_index].Cells[1].Value = modal.EdgeDisplayValueTo;
                    dataGridEdgeDisplay.Rows[selected_index].Cells[2].Value = modal.EdgeDisplayWidth;

                    (_settings.EdgeDisplayList[selected_index] as JObject)["valueFrom"] = modal.EdgeDisplayValueFrom;
                    (_settings.EdgeDisplayList[selected_index] as JObject)["valueTo"] = modal.EdgeDisplayValueTo;
                    (_settings.EdgeDisplayList[selected_index] as JObject)["width"] = modal.EdgeDisplayWidth;
                    (_settings.EdgeDisplayList[selected_index] as JObject)["color"] = "rgb(" + modal.EdgeDisplayColor.R + "," + modal.EdgeDisplayColor.G + "," + modal.EdgeDisplayColor.B + ")";
                }
            }
        }

        private void btnRemoveEdgeDisplay_Click(object sender, EventArgs e)
        {
            if (this.dataGridEdgeDisplay.SelectedRows.Count != 0)
            {
                this.dataGridEdgeDisplay.Rows.RemoveAt(this.dataGridEdgeDisplay.SelectedRows[0].Index);
                _settings.EdgeDisplayList.RemoveAt(this.dataGridEdgeDisplay.SelectedRows[0].Index);
            }
        }
    }
}