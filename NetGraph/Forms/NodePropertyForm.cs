using CefSharp;
using CyConex.Chromium;
using CyConex.Graph;
using Newtonsoft.Json.Linq;
using Syncfusion.WinForms.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Zoople.HTMLEditControl;

namespace CyConex.Forms
{
    public partial class NodePropertyForm : SfForm
    {
        public MainForm mainForm = null;
        static List<string> MetaTags = new List<string> { };
        public NodePropertyForm(MainForm callingForm)
        {
            InitializeComponent();
            mainForm = callingForm as MainForm;
            this.htmlEditorNodeDesc.LicenceKey = "KPH1022-7636-2076";
            this.htmlEditorNodeDesc.LicenceActivationKey = "ZjLtf93kdRI9pS+7np3pJUs/CV0WafYg+nFeuHcUzeQe17f/m2sEghMdskXbkfbD/lT44byjWwmqEaPu9JskfiJCen4eozPySCWhis4VrQrxrOeumWkkHbcK/h5MtRlAnqMbMwsY9t2YGGdX6Z5+F3swaZxJATdc0V5nMsDZkFY=MzJGOTdFMUZFOEY3QTY0MzJCQThBNUMwODk0NzdGRUI=";
            this.htmlEditorNodeNote.LicenceKey = "KPH1022-7636-2076";
            this.htmlEditorNodeNote.LicenceActivationKey = "ZjLtf93kdRI9pS+7np3pJUs/CV0WafYg+nFeuHcUzeQe17f/m2sEghMdskXbkfbD/lT44byjWwmqEaPu9JskfiJCen4eozPySCWhis4VrQrxrOeumWkkHbcK/h5MtRlAnqMbMwsY9t2YGGdX6Z5+F3swaZxJATdc0V5nMsDZkFY=MzJGOTdFMUZFOEY3QTY0MzJCQThBNUMwODk0NzdGRUI=";
            this.htmlEditorGraphNote.LicenceKey = "KPH1022-7636-2076";
            this.htmlEditorGraphNote.LicenceActivationKey = "ZjLtf93kdRI9pS+7np3pJUs/CV0WafYg+nFeuHcUzeQe17f/m2sEghMdskXbkfbD/lT44byjWwmqEaPu9JskfiJCen4eozPySCWhis4VrQrxrOeumWkkHbcK/h5MtRlAnqMbMwsY9t2YGGdX6Z5+F3swaZxJATdc0V5nMsDZkFY=MzJGOTdFMUZFOEY3QTY0MzJCQThBNUMwODk0NzdGRUI=";
            this.htmlEditorEdgeDesc.LicenceKey = "KPH1022-7636-2076";
            this.htmlEditorEdgeDesc.LicenceActivationKey = "ZjLtf93kdRI9pS+7np3pJUs/CV0WafYg+nFeuHcUzeQe17f/m2sEghMdskXbkfbD/lT44byjWwmqEaPu9JskfiJCen4eozPySCWhis4VrQrxrOeumWkkHbcK/h5MtRlAnqMbMwsY9t2YGGdX6Z5+F3swaZxJATdc0V5nMsDZkFY=MzJGOTdFMUZFOEY3QTY0MzJCQThBNUMwODk0NzdGRUI=";
            this.htmlEditGraphDescription.LicenceKey = "KPH1022-7636-2076";
            this.htmlEditGraphDescription.LicenceActivationKey = "ZjLtf93kdRI9pS+7np3pJUs/CV0WafYg+nFeuHcUzeQe17f/m2sEghMdskXbkfbD/lT44byjWwmqEaPu9JskfiJCen4eozPySCWhis4VrQrxrOeumWkkHbcK/h5MtRlAnqMbMwsY9t2YGGdX6Z5+F3swaZxJATdc0V5nMsDZkFY=MzJGOTdFMUZFOEY3QTY0MzJCQThBNUMwODk0NzdGRUI=";
            //
        }

        public string getMetaTags()
        {
            string str = "";
            string comma = "";
            foreach (string tag in MetaTags)
            {
                str = str + comma + tag;
                comma = ",";
            }
            return str;
        }

        public void setMetaTags(string str)
        {
            string[] arr = str.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            MetaTags.Clear();
            this.flowLayoutPanel.Controls.Clear();
            foreach (string tag in arr)
            {
                cloneTagWord(tag);
            }

        }
        private void btnTagWord_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cloneTagWord(this.btnTagWord.Text);
                this.btnTagWord.Text = "";
            }
        }

        private void UpdateNodeMetaTags()
        {
            string nodeID = mainForm.selectedNodeID();
            if (nodeID == "") return;
            mainForm._browser.ExecScriptAsync($"setElementData('{nodeID}', 'metatags', '{getMetaTags()}');");
        }
        public static float getTextWidth(string text, Font f)
        {
            float textWidth = 0;

            using (Bitmap bmp = new Bitmap(1, 1))
            using (Graphics g = Graphics.FromImage(bmp))
            {
                textWidth = g.MeasureString(text, f).Width;
            }

            return textWidth;

        }
        private void cloneTagWord(string str)
        {
            if (MetaTags.IndexOf(str) != -1)
            {
                return;
            }
            else
            {
                MetaTags.Add(str);
            }

            Syncfusion.Windows.Forms.Tools.ButtonEdit tmpButtonEdit = new Syncfusion.Windows.Forms.Tools.ButtonEdit();
            Syncfusion.Windows.Forms.Tools.TextBoxExt tmpTagWord = new Syncfusion.Windows.Forms.Tools.TextBoxExt();
            Syncfusion.Windows.Forms.Tools.ButtonEditChildButton tmpChildbutton = new Syncfusion.Windows.Forms.Tools.ButtonEditChildButton();

            float text_width = getTextWidth(str, this.btnTagWord.Font);

            tmpButtonEdit.UseVisualStyle = true;
            tmpButtonEdit.TextBox = tmpTagWord;
            tmpButtonEdit.Size = new System.Drawing.Size((int)text_width + 30, 21);
            tmpTagWord.Location = new System.Drawing.Point(3, 4);
            tmpTagWord.BorderStyle = System.Windows.Forms.BorderStyle.None;
            tmpTagWord.Enabled = false;
            tmpTagWord.BackColor = Color.White;
            tmpTagWord.Text = str;
            tmpTagWord.ReadOnly = true;

            tmpChildbutton.Text = "x";
            tmpChildbutton.Click += new System.EventHandler(this.closeTagButtonWrap);

            this.flowLayoutPanel.Controls.Add(tmpButtonEdit);
            tmpButtonEdit.Buttons.Add(tmpChildbutton);
            tmpButtonEdit.Controls.Add(tmpChildbutton);
            tmpButtonEdit.Controls.Add(tmpTagWord);

            UpdateNodeMetaTags();
        }

        private void closeTagButtonWrap(object sender, EventArgs e)
        {
            Syncfusion.Windows.Forms.Tools.ButtonEditChildButton obj = sender as Syncfusion.Windows.Forms.Tools.ButtonEditChildButton;
            Syncfusion.Windows.Forms.Tools.ButtonEdit obj_wrap = obj.Parent as Syncfusion.Windows.Forms.Tools.ButtonEdit;
            System.Windows.Forms.FlowLayoutPanel obj_panel = obj_wrap.Parent as System.Windows.Forms.FlowLayoutPanel;

            lock (obj_wrap)
            {
                if (obj_panel != null)
                {
                    obj_panel.Controls.Remove(obj_wrap);
                }
            }

            UpdateNodeMetaTags();
        }
        private void NodePropertyForm_Load(object sender, EventArgs e)
        {
            InitNodePropertyPanels();

            this.nodeTabPage.TabVisible = false;
            this.edgeTabPage.TabVisible = false;
            //this.htmlEditorNodeDesc.Document.ExecCommand("fontName", false, "Arial");
        }

        public void InitNodePropertyPanels()
        {
            this.panelProperties.Dock = DockStyle.Fill;
        }

        public void initNodeMainCategories()
        {
            this.cmbNodeFrmCategory.Items.Clear();
            foreach (JObject nodeCategory in mainForm.node_categories)
            {
                string text = nodeCategory["text"].ToString();
                if (text == "") continue;
                cmbNodeFrmCategory.Items.Add(text);
            }
        }

        public void initNodeSubCategories()
        {
            this.cmbNodeFrmSubCategory.Items.Clear();
            string cat = cmbNodeFrmCategory.Text;
            if (mainForm == null) return;
            for (int i = 0; i < mainForm.node_categories.Count; i++)
            {
                string text = mainForm.node_categories[i]["text"].ToString();
                if (text == "") continue;
                if (cat.ToLower() == text.ToLower())
                {
                    JArray jArray = mainForm.node_categories[i]["childrens"] as JArray;
                    for (int j = 0; j < jArray.Count; j++)
                    {
                        cmbNodeFrmSubCategory.Items.Add(jArray[j]);
                    }
                    break;
                }
            }
        }

        private void cmbNodeFrmCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            initNodeSubCategories();
            string nodeID = mainForm.selectedNodeID();
            if (nodeID == "") return;
            mainForm._browser.ExecScriptAsync($"setElementData('{nodeID}', 'primaryCategory', '{cmbNodeFrmCategory.Text}');");
        }

        public void UpdateGraphStatis()
        {
            lblControlNodes.Text = GraphUtil.GetControlNodeCount().ToString();
            lblGroupNodes.Text = GraphUtil.GetGroupNodeCount().ToString();
            lblObjectiveNodes.Text = GraphUtil.GetObjectiveNodeCount().ToString();
            lblAttackNodes.Text = GraphUtil.GetAttackNodeCount().ToString();
            lblActorNodes.Text = GraphUtil.GetActorNodeCount().ToString();
            lblAssetNodes.Text = GraphUtil.GetAssetNodeCount().ToString();
            lblDisabledNodes.Text = GraphUtil.GetControlNodeCount().ToString();
            lblEvidenceNodes.Text = GraphUtil.GetEvidenceNodeCount().ToString();
            lblEdges.Text = GraphUtil.GetEdgesCount().ToString();
        }

        public void setGraphPropertyData(GraphProperties graphProperties)
        {
            this.txtGraphTitle.Text = graphProperties.Name;
            this.lblGraphCreated.Text = graphProperties.Created.ToString();
            this.lblGraphUpdated.Text = graphProperties.Updated.ToString();
            this.txtGraphMajor.Text = graphProperties.MajorVersion.ToString();
            this.txtGraphMinor.Text = graphProperties.MinorVersion.ToString();
            this.lblGraphRevision.Text = graphProperties?.Revision.ToString();
            this.htmlEditGraphDescription.DocumentHTML = Utility.Base64Decode(graphProperties.Description);
        }

        private void txtNodeFrmReferenceURL_TextChanged(object sender, EventArgs e)
        {
            string nodeID = mainForm.selectedNodeID();
            if (nodeID == "") return;
            mainForm._browser.ExecScriptAsync($"setElementData('{nodeID}', 'refurl', '{txtNodeFrmReferenceURL.Text}');");
        }

        private void cmbNodeFrmSubCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            string nodeID = mainForm.selectedNodeID();
            if (nodeID == "") return;
            mainForm._browser.ExecScriptAsync($"setElementData('{nodeID}', 'subCategory', '{cmbNodeFrmSubCategory.Text}');");
        }

        private void txtNodeFrmSubDomain_TextChanged(object sender, EventArgs e)
        {
            string nodeID = mainForm.selectedNodeID();
            if (nodeID == "") return;
            mainForm._browser.ExecScriptAsync($"setElementData('{nodeID}', 'subdomain', '{txtNodeFrmSubDomain.Text}');");
        }

        private void txtNodeFrmReference_TextChanged(object sender, EventArgs e)
        {
            string nodeID = mainForm.selectedNodeID();
            if (nodeID == "") return;
            mainForm._browser.ExecScriptAsync($"setElementData('{nodeID}', 'frameworkReference', '{txtNodeFrmReference.Text}');");
            GraphUtil.SetNodeData(nodeID, "frameworkReference", txtNodeFrmReference.Text);
        }

        private void txtNodeFramework_TextChanged(object sender, EventArgs e)
        {
            string nodeID = mainForm.selectedNodeID();
            if (nodeID == "") return;
            mainForm._browser.ExecScriptAsync($"setElementData('{nodeID}', 'frameworkName', '{txtNodeFramework.Text}');");
        }

        private void txtNodeFrmDomain_TextChanged(object sender, EventArgs e)
        {
            string nodeID = mainForm.selectedNodeID();
            if (nodeID == "") return;
            mainForm._browser.ExecScriptAsync($"setElementData('{nodeID}', 'domain', '{txtNodeFrmDomain.Text}');");
        }

        private void txtNodeFrmLevel_TextChanged(object sender, EventArgs e)
        {
            string nodeID = mainForm.selectedNodeID();
            if (nodeID == "") return;
            mainForm._browser.ExecScriptAsync($"setElementData('{nodeID}', 'level', '{txtNodeFrmLevel.Text}');");
        }

        private void txtGraphTitle_TextChanged(object sender, EventArgs e)
        {
            mainForm._graphProperties.Name = txtGraphTitle.Text;
            mainForm._browser.ExecScriptAsync($"setGraphData('name', '{mainForm._graphProperties.Name}');");
        }

        private void GraphData_tabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (mainForm == null || mainForm._selected_main_tab == GraphData_tabControl.SelectedTab.Text.ToLower())
            {
                return;
            }

            mainForm._selected_main_tab = GraphData_tabControl.SelectedTab.Text.ToLower();
        }

        private void nodeTabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (mainForm._selected_node_sub_tab == nodeTabControl.SelectedTab.Text.ToLower())
            {
                return;
            }

            mainForm._selected_node_sub_tab = nodeTabControl.SelectedTab.Text.ToLower();
        }

        private void graphTabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (mainForm._selected_graph_sub_tab == graphTabControl.SelectedTab.Text.ToLower())
            {
                return;
            }

            mainForm._selected_graph_sub_tab = graphTabControl.SelectedTab.Text.ToLower();
        }

        private void edgeTabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (mainForm._selected_edge_sub_tab == edgeTabControl.SelectedTab.Text.ToLower())
            {
                return;
            }

            mainForm._selected_edge_sub_tab = edgeTabControl.SelectedTab.Text.ToLower();
        }

        private void txtNodeFrmVersion_TextChanged(object sender, EventArgs e)
        {
            string nodeID = mainForm.selectedNodeID();
            if (nodeID == "") return;
            mainForm._browser.ExecScriptAsync($"setElementData('{nodeID}', 'frameworkNameVersion', '{txtNodeFrmVersion.Text}');");
        }

        private void htmlEditorNodeDesc_HTMLChanged(object sender, EventArgs e)
        {
            UpdateNodeDesc();
        }

        private void UpdateNodeDesc()
        {
            string nodeID = mainForm.selectedNodeID();
            if (nodeID == "") return;
            mainForm._browser.ExecScriptAsync($"setElementData('{nodeID}', 'description', '{Utility.Base64Encode(htmlEditorNodeDesc.DocumentHTML)}');");
            GraphUtil.SetNodeData(nodeID, "description", Utility.Base64Encode(htmlEditorNodeDesc.DocumentHTML));
        }
        private void htmlEditGraphDescription_HTMLChanged(object sender, EventArgs e)
        {
            mainForm._browser.ExecScriptAsync($"setGraphData('description', '{Utility.Base64Encode(htmlEditGraphDescription.DocumentHTML)}');");
        }

        private void htmlEditorEdgeDesc_HTMLChanged(object sender, EventArgs e)
        {
            string edgeID = mainForm.selectedEdgeID();
            if (edgeID == "") return;
            mainForm._browser.ExecScriptAsync($"setElementData('{edgeID}', 'note', '{Utility.Base64Encode(htmlEditorEdgeDesc.DocumentHTML)}');");
        }

        private void htmlEditorNodeNote_HTMLChanged(object sender, EventArgs e)
        {
            string nodeID = mainForm.selectedNodeID();
            if (nodeID == "") return;
            mainForm._browser.ExecScriptAsync($"setElementData('{nodeID}', 'note', '{Utility.Base64Encode(htmlEditorNodeNote.DocumentHTML)}');");
        }

        private void htmlEditorGraphNote_HTMLChanged(object sender, EventArgs e)
        {
            mainForm._browser.ExecScriptAsync($"setGraphData('note', '{Utility.Base64Encode(htmlEditorNodeNote.DocumentHTML)}');");
        }

        public void setHtmlEditorNodeNote(string data)
        {
            if (data == null || data == "")
            {
                htmlEditorNodeNote.DocumentHTML = "";
            }
            else
            {
                htmlEditorNodeNote.DocumentHTML = Utility.Base64Decode(data);
            }
        }

        public void setHtmlEditorNodeDesc(string data)
        {
            if (data == null || data == "")
            {
                htmlEditorNodeDesc.DocumentHTML = "";
            }
            else
            {
                htmlEditorNodeDesc.DocumentHTML = Utility.Base64Decode(data);
            }
        }

        public void setHtmlEditorGraphDesc1(string data)
        {
            if (data == null || data == "")
            {
                htmlEditGraphDescription.DocumentHTML = "";
            }
            else
            {
                htmlEditGraphDescription.DocumentHTML = Utility.Base64Decode(data);
            }
        }

        public void setHtmlEditorNodeTitle(string data)
        {
            if (data == null || data == "")
            {
                htmlEditNodeTitle.DocumentHTML = "";
            }
            else
            {

                htmlEditNodeTitle.DocumentHTML = data;
            }
        }

        public void setHtmlEditorEdgeDesc(string data)
        {
            if (data == null || data == "")
            {
                htmlEditorEdgeDesc.DocumentHTML = "";
            }
            else
            {
                htmlEditorEdgeDesc.DocumentHTML = Utility.Base64Decode(data);
            }
        }

        public void setHtmlEditorGraphNote(string data)
        {
            if (data == null || data == "")
            {
                htmlEditorGraphNote.DocumentHTML = "";
            }
            else
            {
                htmlEditorGraphNote.DocumentHTML = Utility.Base64Decode(data);
            }
        }

        private void btnNodeDescEditHTML_Click(object sender, EventArgs e)
        {
            EditHTMLContent(htmlEditorNodeDesc);
            UpdateNodeDesc();
        }

        private void EditHTMLContent(Zoople.HTMLEditControl control)
        {
            HTMLEditorForm htmlEditorForm = new HTMLEditorForm();
            htmlEditorForm.SetDocumentHTMLData(control.DocumentHTML);
            DialogResult dialogResult = htmlEditorForm.ShowDialog();
            if (dialogResult == DialogResult.OK || dialogResult == DialogResult.Cancel)
            {
                control.DocumentHTML = htmlEditorForm.GetDocumentHTMLData();
            }
        }

        private void btnGraphNoteEditHTML_Click(object sender, EventArgs e)
        {
            EditHTMLContent(htmlEditorGraphNote);
        }

        private void btnEdgeNoteEditHTML_Click(object sender, EventArgs e)
        {
            EditHTMLContent(htmlEditorEdgeDesc);
        }

        private void btnNodeNoteEditHTML_Click(object sender, EventArgs e)
        {
            EditHTMLContent(htmlEditorNodeNote);
        }

        private void btnNodeTitleHTMLEditor_Click(object sender, EventArgs e)
        {
            EditHTMLContent(htmlEditNodeTitle);
            updateNodeTitle();
        }

        private void htmlEditNodeTitle_HTMLChanged(object sender, EventArgs e)
        {
            string nodeID = mainForm.selectedNodeID();
            if (nodeID == "") return;
            mainForm._browser.ExecScriptAsync($"setElementData('{nodeID}', 'title', '{htmlEditNodeTitle.DocumentHTML}');");
            GraphUtil.SetNodeData(nodeID, "title", htmlEditNodeTitle.DocumentHTML);
        }

        public void updateNodeTitle()
        {
            string nodeID = mainForm.selectedNodeID();
            if (nodeID == "") return;
            mainForm._browser.ExecScriptAsync($"setElementData('{nodeID}', 'title', '{htmlEditNodeTitle.DocumentHTML}');");
            GraphUtil.SetNodeData(nodeID, "title", htmlEditNodeTitle.DocumentHTML);
        }
        private void btnGraphDescEditHTML_Click(object sender, EventArgs e)
        {
            EditHTMLContent(htmlEditGraphDescription);
        }

        private void txtGraphMajor_ValueChanged(object sender, EventArgs e)
        {
            mainForm._graphProperties.MajorVersion = int.Parse(txtGraphMajor.Text);
            mainForm._browser.ExecScriptAsync($"setGraphData('majorVersion', '{mainForm._graphProperties.MajorVersion}');");
            mainForm._browser.ExecScriptAsync($"setGraphData('major', '{mainForm._graphProperties.MajorVersion}');");
        }


        private void txtGraphMinor_ValueChanged_1(object sender, EventArgs e)
        {
            mainForm._graphProperties.MinorVersion = int.Parse(txtGraphMinor.Text);
            mainForm._browser.ExecScriptAsync($"setGraphData('minorVersion', '{mainForm._graphProperties.MinorVersion}');");
            mainForm._browser.ExecScriptAsync($"setGraphData('minor', '{mainForm._graphProperties.MinorVersion}');");
        }

        private void txtGraphMajor_ValueChanged_1(object sender, EventArgs e)
        {
            mainForm._graphProperties.MajorVersion = (double)txtGraphMajor.Value;
            mainForm._browser.ExecScriptAsync($"setGraphData('majorVersion', '{mainForm._graphProperties.MajorVersion}');");
            mainForm._browser.ExecScriptAsync($"setGraphData('major', '{mainForm._graphProperties.MinorVersion}');");
        }

        private void btnGraphDescEditHTML_Click_1(object sender, EventArgs e)
        {
            EditHTMLContent(htmlEditGraphDescription);
        }
    }
}
