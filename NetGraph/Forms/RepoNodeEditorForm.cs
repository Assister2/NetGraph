using CefSharp.WinForms;
using FuzzySharp;
using CyConex.Graph;
using Newtonsoft.Json.Linq;
using Syncfusion.WinForms.Controls;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Collections;
using CyConex.Helpers;
using CyConex.API;
using Syncfusion.Windows.Forms.Tools;
using CyConex.Forms;

namespace CyConex
{
    public partial class RepoNodeEditorForm : SfForm
    {
        string node_id = "";
		public bool IsNewNode;

		private ApplicationSettings _settings;
        public string nodeImgData = "";
        public string nodeImgPath = "";
		public Node node = new Node();
		static List<string> MetaTags = new List<string> { };
		private List<ConnectNode> _connectNodes = new List<ConnectNode>();
		private JArray _linked_nodes = new JArray();
		private EdgeEditForm edgeEditForm = new EdgeEditForm();
		private List<Node> _node_lists = new List<Node>();
		private ChromiumWebBrowser _browser = new ChromiumWebBrowser();
        CyConex.MainForm _form1;
		private JArray node_categories = new JArray();

        public NodeDistributionsForm nodeDistributionsForm = null;
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

		public RepoNodeEditorForm(bool NewNode)
        {
			InitializeComponent();
			IsNewNode = NewNode;
			_settings = ApplicationSettings.Load();
			string cyFileData = "";
			if (File.Exists(@"Graph\cy.data"))
			{
				StreamReader sr = new StreamReader(@"Graph\cy.data");
				cyFileData = sr.ReadToEnd();
				sr.Close();
			}

			JObject cyJsonFileData = cyFileData == "" ? null : JObject.Parse(cyFileData);
			JArray nodeStrength = (JArray)cyJsonFileData["impact"];
			JArray nodeAssets = (JArray)cyJsonFileData["node_assets"];
			JArray nodeKeyWords = (JArray)cyJsonFileData["key_words"];
			JObject attackAttr = cyJsonFileData.ContainsKey("attack_value") ? (JObject)cyJsonFileData["attack_value"] : new JObject();
			JObject actorAttr = cyJsonFileData.ContainsKey("actor_value") ? (JObject)cyJsonFileData["actor_value"] : new JObject();
			JObject assetAttr = cyJsonFileData.ContainsKey("asset_value") ? (JObject)cyJsonFileData["asset_value"] : new JObject();
			node_categories = (JArray)cyJsonFileData["node_categories"];

			_settings.NodeinherentStrengthData = nodeStrength;
			_settings.NodeimplementedStrengthData = nodeAssets;
			_settings.KeyWordsData = nodeKeyWords;

			AutoCompleteStringCollection data = new AutoCompleteStringCollection();
			foreach (string keyword in nodeKeyWords)
			{
				data.Add(keyword);
			}

			this.btnTagWord.AutoCompleteCustomSource = data;
			this.cmbNodeType.SelectedIndex = 0;

            _settings.NodeAttrActor = actorAttr;
			_settings.NodeAttrAttack = attackAttr;
			_settings.NodeAttrAsset = assetAttr;

            this.nodeDistributionsForm = new NodeDistributionsForm();

            initNodeCategories();
            initNodeDistributionForm();

            this.htmlEditNodeTitle.HiddenButtons = ApplicationSettings.ZoopleHTMLEditorHiddenButtons;
			this.htmlEditNodeDesc.HiddenButtons = ApplicationSettings.ZoopleHTMLEditorHiddenButtons;
			this.htmlEditNodeNote.HiddenButtons = ApplicationSettings.ZoopleHTMLEditorHiddenButtons;
        }

		private void initNodeCategories()
		{
			nodeCategory_Cmb.Items.Add("Control");
			nodeCategory_Cmb.Items.Add("Group");
			nodeCategory_Cmb.Items.Add("Asset");
			nodeCategory_Cmb.Items.Add("Objective");
			nodeCategory_Cmb.Items.Add("Attack");

			for (int i = 0; i < node_categories.Count; i++)
			{
				nodeCategory_Cmb.Items.Add(node_categories[i]["text"]);
			}

			initNodeSubCategories();
		}

		private void initNodeSubCategories()
		{
			nodeSubCategory_Cmb.Items.Clear();
			string cat = nodeCategory_Cmb.Text;
			for (int i = 0; i < node_categories.Count; i++)
			{
				if (cat.ToLower() == node_categories[i]["text"].ToString().ToLower())
				{
					JArray jArray = node_categories[i]["childrens"] as JArray;
					for (int j = 0; j < jArray.Count; j++)
					{
						nodeSubCategory_Cmb.Items.Add(jArray[j]);
					}
					break;
				}
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

        public void initNodeDistributionForm()
        {
            nodeDistributionsForm.TopLevel = false;
            nodeDistributionsForm.FormBorderStyle = FormBorderStyle.None;
            nodeDistributionsForm.Parent = this;

            nodeDistributionsForm.panelContainer.Controls.Add(nodeDistributionsForm.panelNodeControlDetails);
            nodeDistributionsForm.panelContainer.Controls.Add(nodeDistributionsForm.panelActorNodeDistributionValues);
            nodeDistributionsForm.panelContainer.Controls.Add(nodeDistributionsForm.panelVulnerabilityNodeDistributionValues);
            nodeDistributionsForm.panelContainer.Controls.Add(nodeDistributionsForm.panelAssetNodeDistributionValues);
            nodeDistributionsForm.panelContainer.Controls.Add(nodeDistributionsForm.panelAttackNodeDistributionValues);
            nodeDistributionsForm.panelContainer.Controls.Add(nodeDistributionsForm.panelEdgeDistributionValues);
            nodeDistributionsForm.panelContainer.Controls.Add(nodeDistributionsForm.panelObjectiveNodeValues);
            nodeDistributionsForm.panelContainer.Controls.Add(nodeDistributionsForm.panelNodeGroupDetails);
            nodeDistributionsForm.panelContainer.Controls.Add(nodeDistributionsForm.panelNodeVulnerabilityGroup);
            nodeDistributionsForm.panelContainer.Controls.Add(nodeDistributionsForm.panelEvidenceNodeValues);

            nodeDistributionsForm.panelNodeControlDetails.Dock = DockStyle.Fill;
            nodeDistributionsForm.panelActorNodeDistributionValues.Dock = DockStyle.Fill;
            nodeDistributionsForm.panelVulnerabilityNodeDistributionValues.Dock = DockStyle.Fill;
            nodeDistributionsForm.panelAssetNodeDistributionValues.Dock = DockStyle.Fill;
            nodeDistributionsForm.panelAttackNodeDistributionValues.Dock = DockStyle.Fill;
            nodeDistributionsForm.panelEdgeDistributionValues.Dock = DockStyle.Fill;
            nodeDistributionsForm.panelObjectiveNodeValues.Dock = DockStyle.Fill;
            nodeDistributionsForm.panelNodeGroupDetails.Dock = DockStyle.Fill;
            nodeDistributionsForm.panelEvidenceNodeValues.Dock = DockStyle.Fill;
            nodeDistributionsForm.panelNodeVulnerabilityGroup.Dock = DockStyle.Fill;

			nodeDistributionsForm.form_flag = 2;

            panelNodeDistribution.Controls.Add(nodeDistributionsForm.panelContainer);
			nodeDistributionsForm.Show();
			nodeDistributionsForm.panelContainer.Show();
			nodeDistributionsForm.panelContainer.Width = panelNodeDistribution.Width - 10;
			nodeDistributionsForm.panelContainer.Height = panelNodeDistribution.Height - 10;

			tabPageNodeDistribution.Text = "Control";
            nodeDistributionsForm.ShowNodeDistributionPanel("control");
        }

		private int GetComboboxIndex(ComboBox cb, string text)
        {
			int ind = 0;
			for (int i = 0; i < cb.Items.Count; i++)
            {
				if (cb.Items[i].ToString().ToLower() == text.ToLower())
                {
					ind = i;
					break;
                }
            }
			return ind;
        }

        public DialogResult ShowDialogWithNodeID(CyConex.MainForm form1, IWin32Window owner, ChromiumWebBrowser _br, JObject node_data, string type)
        {
			_browser = _br;
			Random random = new Random();
			_form1 = form1;
			nodeDistributionsForm.mainForm = _form1;
			// set node data for editing
			if (node_data.ContainsKey("meta"))
            {
				this.InvokeIfNeed(() =>
				{
					JObject node_meta = node_data["meta"] as JObject;
					JObject node_graph = node_data["node_graph"] as JObject;
					JObject node_visual = node_data["node_visual"] as JObject;
					JObject node_note = node_data["node_note"] as JObject;
					JObject node_framework = node_data["node_framework"] as JObject;

					node_id = node_graph["nodeGUID"].ToString();
					cmbNodeType.SelectedIndex = GetComboboxIndex(cmbNodeType, node_meta["nodeType"].ToString());
					htmlEditNodeTitle.DocumentHTML = node_meta["nodeTitle"].ToString();
					htmlEditNodeDesc.DocumentHTML = "";

					txtFrmReference.Text = node_framework["reference"].ToString();
					string note = node_note["note"].ToString();
					htmlEditNodeNote.DocumentHTML = note;
					txtFramework.Text = node_framework["framework"].ToString();
					txtFrmVersion.Text = node_framework["version"].ToString();
					txtFrmDomain.Text = node_framework["Domain"].ToString();

                    txtSubDomain.Text = node_framework["subDomain"].ToString();
					txtReferenceURL.Text = node_framework["refUrl"].ToString();
					txtFrmLevel.Text = node_framework["level"].ToString();
                });
			}
			else
            {
				IsNewNode = true;
				node_id = Guid.NewGuid().ToString();// random.Next().ToString();
				htmlEditNodeTitle.DocumentHTML = "";
				htmlEditNodeDesc.DocumentHTML = "";
				txtFrmReference.Text = "";
				htmlEditNodeNote.DocumentHTML = "";
				txtFramework.Text = "";
				txtFrmVersion.Text = "";
				txtFrmDomain.Text = "";
				txtSubDomain.Text = "";
				txtReferenceURL.Text = "";
				txtFrmLevel.Text = "";

				nodeImgPath = "";
				nodeImgData = "";
				PicNodeImg = "";
				setMetaTags("");
			}
			return ShowDialog(owner);
        }

		public string PicNodeImg
		{
			get { return picNodeImage.Text; }
			set
			{
				if (value != null && value != "")
				{
					if (File.Exists(value))
                    {
						picNodeImage.Image = new Bitmap(value);
					}
                    else
                    {
						picNodeImage.Image = LoadBase64(nodeImgData);
                    }

					if (picNodeImage.Width > picNodeImage.Image.Width && picNodeImage.Height > picNodeImage.Image.Height)
					{
						picNodeImage.SizeMode = PictureBoxSizeMode.Normal;
					}
					else
					{
						picNodeImage.SizeMode = PictureBoxSizeMode.StretchImage;
					}
				}
			}
		}

		public static Image LoadBase64(string base64)
		{
			byte[] bytes = Convert.FromBase64String(base64);
			Image image;
			using (MemoryStream ms = new MemoryStream(bytes))
			{
				image = Image.FromStream(ms);
			}
			return image;
		}

		private void btnNodeImageSelect_Click(object sender, EventArgs e)
        {
			selectNodeImage();
		}

		private void selectNodeImage()
		{
			OpenFileDialog open = new OpenFileDialog();
			open.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp, *.ico, *.png)|*.jpg; *.jpeg; *.gif; *.bmp; *.ico; *.png";
			if (open.ShowDialog() == DialogResult.OK)
			{
				// display image in picture box  
				picNodeImage.Image = new Bitmap(open.FileName);
				// image file path  
				picNodeImage.Text = open.FileName;
				nodeImgData = GetBase64StringForImage(open.FileName);
				nodeImgPath = open.FileName;

				System.Drawing.Image image = System.Drawing.Image.FromFile(open.FileName);

				if (picNodeImage.Width > picNodeImage.Image.Width && picNodeImage.Height > picNodeImage.Image.Height)
				{
					picNodeImage.SizeMode = PictureBoxSizeMode.Normal;
				}
				else
				{
					picNodeImage.SizeMode = PictureBoxSizeMode.StretchImage;
				}
			}
		}

		protected static string GetBase64StringForImage(string imgPath)
		{
			byte[] imageBytes = System.IO.File.ReadAllBytes(imgPath);
			string base64String = Convert.ToBase64String(imageBytes);
			return base64String;
		}

        private void btnNodeImageClear_Click(object sender, EventArgs e)
        {
			clearNodeImage();
        }

		private void clearNodeImage()
		{
			nodeImgData = "";
			nodeImgPath = "";
			picNodeImage.Image = null;
		}

		private void cmbNodeType_SelectedIndexChanged(object sender, EventArgs e)
        {
			string selected_item_text = cmbNodeType.GetItemText(cmbNodeType.SelectedItem);
			//if (IsNewNode == true) txtNodeTitle.Text = selected_item_text;
			ImageData img = new ImageData();
			img = Utility.DefaultNodeImage(selected_item_text);

			picNodeImage.Image = new Bitmap(img.ImagePath);
			// image file path  
			picNodeImage.Text = img.ImagePath;
			nodeImgData = Utility.GetBase64StringForImage(img.ImagePath);
			nodeImgPath = img.ImagePath;

			System.Drawing.Image image = System.Drawing.Image.FromFile(img.ImagePath);

			if (picNodeImage.Width > picNodeImage.Image.Width && picNodeImage.Height > picNodeImage.Image.Height)
			{
				picNodeImage.SizeMode = PictureBoxSizeMode.Normal;
			}
			else
			{
				picNodeImage.SizeMode = PictureBoxSizeMode.StretchImage;
			}

			if (nodeDistributionsForm != null)
			{
                tabPageNodeDistribution.Text = selected_item_text;
                nodeDistributionsForm.ShowNodeDistributionPanel(selected_item_text.ToLower());
            }
		}

        private void picNodeImage_Click(object sender, EventArgs e)
        {
			selectNodeImage();
        }

        private void btnAutoTag_Click(object sender, EventArgs e)
        {
			setAutoTag();
		}

		private void setAutoTag()
        {
			string node_desc = this.htmlEditNodeDesc.DocumentPlainText;
			node_desc = node_desc.Replace('\n', ' ');
			string node_title = this.htmlEditNodeTitle.DocumentPlainText;
			string[] node_title_arr = node_title.Split(' ');
			string[] node_desc_arr = node_desc.Split(' ');

			foreach (string s in _settings.KeyWordsData)  //Literal search 
			{
				int tmp = node_desc.IndexOf(s, StringComparison.OrdinalIgnoreCase);
				if (tmp != -1)
				{
					this.cloneTagWord(s);
				}
			}

			foreach (string s in node_desc_arr)
			{
				string tmp = isKeyWord(s, _settings.KeyWordsData);
				if (tmp != "")
				{
					this.cloneTagWord(tmp);
				}
			}

			foreach (string s in node_title_arr)
			{
				string tmp = isKeyWord(s, _settings.KeyWordsData);
				if (tmp != "")
				{
					this.cloneTagWord(tmp);
				}
			}
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
		}

		private string isKeyWord(string str, JArray str_arr)
		{
			string match = "";
			double match_percent = 0;
			foreach (string str_item in str_arr)
			{
				double tmp_percent = Fuzz.Ratio(str, str_item);
				if (tmp_percent > 70 && tmp_percent > match_percent)
				{
					match_percent = tmp_percent;
					match = str_item;
				}
			}
			return match;
		}

        private void btnOk_Click(object sender, EventArgs e)
        {
			SetNodeData();
			SaveNodeData();
			DialogResult = DialogResult.OK;
        }

		private void SetNodeData()
        {
			node.ID = node_id;
			node.Type = new NodeType(cmbNodeType.Text, cmbNodeType.Text);
			node.Title = htmlEditNodeTitle.DocumentHTML;
			node.TitleSize = 12;
			node.TitleTextColor = Color.Black;
			byte[] textBytes = System.Text.Encoding.UTF8.GetBytes(nodeImgPath);
			node.ImagePath = nodeImgPath == "" ? "" : System.Convert.ToBase64String(textBytes);
			node.Note = htmlEditNodeNote.DocumentHTML;
			node.frameworkReference = txtFrmReference.Text;
			node.frameworkName = txtFramework.Text;
			node.ControlFrameworkVersion = txtFrmVersion.Text;
			node.Domain = txtFrmDomain.Text;
			node.SubDomain = txtSubDomain.Text;
			node.ReferenceURL = txtReferenceURL.Text;
			node.Level = txtFrmLevel.Text;
			node.description = htmlEditNodeDesc.DocumentHTML;
			node.MetaTags = getMetaTags();
			node.Shape = new NodeShape("shape", "Rectangle");
			node.Color = Color.Transparent;
			node.BorderColor = Color.Transparent;
			node.NodeImageData = nodeImgData;
			node.NodeTitlePosition = "center";
			node.Category = nodeCategory_Cmb.Text;
			node.PrimaryCategory = nodeCategory_Cmb.Text;
			node.SubCategory = nodeSubCategory_Cmb.Text;
		}

		private void SaveNodeData()
        {
			string jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(node);
			NodeAPI.PutRepoNodeJSON(node.ID, jsonString);
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
			DialogResult = DialogResult.Cancel;
        }

        private void btnTagWord_TextChanged(object sender, EventArgs e)
        {

        }

        private void tabPageFramework_Click(object sender, EventArgs e)
        {

        }

        private void btnOpenReferenceURL_Click(object sender, EventArgs e)
        {
			string url = txtReferenceURL.Text;
			_form1.wbf.ShowBrowserForm(_form1, url, 1024, 768);
		}

        private void nodeCategory_Cmb_SelectedIndexChanged(object sender, EventArgs e)
        {
			initNodeSubCategories();
		}

        private void btnEditEdges_Click(object sender, EventArgs e)
        {
			Graph.Utility.SaveAuditLog("btnSourceNodeEdit", "Button Click", "", "", "");
			_linked_nodes = EdgeRepository.InitLinkedNodes();
			_node_lists = NodeRepository.LoadRepositoryList();
			//edgeEditForm.ShowDialogWithBrowser(this, _browser, node, node_id, htmlEditNodeTitle.Text, htmlEditNodeDesc.Text);
		}

        private void sfButton1_Click(object sender, EventArgs e)
        {
           
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

        private void sfButton2_Click(object sender, EventArgs e)
        {
            EditHTMLContent(htmlEditNodeTitle);
            
        }

        private void htmlEditNodeDesc_Load(object sender, EventArgs e)
        {

        }

        private void sfButton3_Click(object sender, EventArgs e)
        {
            EditHTMLContent(htmlEditNodeDesc);
        }

        private void tabPageMeta_Click(object sender, EventArgs e)
        {

        }
    }
}
