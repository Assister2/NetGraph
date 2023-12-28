using CefSharp.DevTools.CSS;
using CyConex.API;
using CyConex.Graph;
using Newtonsoft.Json.Linq;
using Syncfusion.WinForms.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace CyConex
{
    public partial class FileOpenForm : SfForm
    {
        public string file_type = "local";
        public string local_file_name = "";
        public string cloud_file_data = "";
        public string graph_guid = "";
        public string child_guid = "";

        public FileOpenForm()
        {
            InitializeComponent();
        }

        private void tabControlAdv1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        public DialogResult ShowDialogWithTab(string tab, List<JObject> recents )
        {
            switch (tab)
            {
                case "recent":
                    tabControls.SelectedTab = tabPageRecent;
                    break;
                case "cloud":
                    tabControls.SelectedTab = tabPageCloud;
                    break;
                case "local":
                    tabControls.SelectedTab = tabPageLocal;
                    break;
            }
            InitRecentGraphs(recents );
            LoadGraphData();
            return this.ShowDialog();
        }           

        private void RecentImageItem_Click(object sender, EventArgs e)
        {
            string path = (sender as Control).Tag.ToString();
            local_file_name = path;
            file_type = "local";
            graph_guid = AuthAPI._tenant_guid;
            child_guid = "";
            DialogResult = DialogResult.Yes;
        }

        private void InitRecentGraphs(List<JObject> recents )
        {
            this.flowParentPanel.Controls.Clear();
            
            for (int i = 0; i < recents.Count; i++)
            {
                JObject item = recents[i];

                PictureBox pb = new PictureBox();
                pb.Location = new System.Drawing.Point(3, 3);
                pb.Size = new System.Drawing.Size(90, 94);
                pb.SizeMode = PictureBoxSizeMode.Zoom;
                pb.Tag = item["path"];
                pb.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
                pb.BackColor = System.Drawing.Color.White;
                pb.Click += RecentImageItem_Click;
                if (item.ContainsKey("image") && item["image"].ToString() != "")
                {
                    if (File.Exists(item["image"].ToString()))
                    {
                        Bitmap bmp = null;
                        try
                        {
                            using (var bmpFromFile = (Bitmap)Image.FromFile(item["image"].ToString()))
                            {
                                bmp = new Bitmap(bmpFromFile);
                                pb.Image= bmp;
                            }
                        } 
                        catch
                        {
                            
                        }
                    }
                    else
                    {
                        pb.Image = new Bitmap("Recent/node_default.png");
                    }
                }
                else
                {
                    pb.Image = new Bitmap("Recent/node_default.png");
                }
                pb.MouseEnter += new System.EventHandler(this.panelMouseEnter);
                pb.MouseLeave += new System.EventHandler(this.panelMouseLeave);

                Label lbl_savedate = new Label();  //date
                lbl_savedate.AutoSize = true;
                lbl_savedate.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                lbl_savedate.Location = new System.Drawing.Point(440, 3);
                lbl_savedate.Size = new System.Drawing.Size(58, 13);
                if (item.ContainsKey("date"))
                {
                    try
                    {
                        var dateTime = item["date"];
                        DateTime dt = (DateTime)dateTime;
                        string tempDate = dt.ToString("f");
                        lbl_savedate.Text = tempDate;
                    }
                    catch 
                    {
                        lbl_savedate.Text = "?";
                    };
                }
                lbl_savedate.MouseEnter += new System.EventHandler(this.panelMouseEnter);
                lbl_savedate.MouseLeave += new System.EventHandler(this.panelMouseLeave);
                lbl_savedate.BackColor = System.Drawing.Color.Transparent;
                lbl_savedate.Click += RecentImageItem_Click;
                lbl_savedate.Tag = item["path"];

                Label lbl_description = new Label(); //Description
                lbl_description.AutoEllipsis = true;
                lbl_description.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                lbl_description.Location = new System.Drawing.Point(100, 24);
                lbl_description.Size = new System.Drawing.Size(460, 73);
                lbl_description.Text = item.ContainsKey("description") && item["description"].ToString() != "" ? item["description"].ToString() : Utility.GetFileName(item["path"].ToString());
                lbl_description.MouseEnter += new System.EventHandler(this.panelMouseEnter);
                lbl_description.MouseLeave += new System.EventHandler(this.panelMouseLeave);
                lbl_description.BackColor = System.Drawing.Color.Transparent;
                lbl_description.Click += RecentImageItem_Click;
                lbl_description.Tag = item["path"];


                Label lbl_title = new Label(); //Title
                lbl_title.AutoEllipsis = true;
                lbl_title.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                lbl_title.Location = new System.Drawing.Point(100, 3);
                lbl_title.Size = new System.Drawing.Size(347, 20);
                lbl_title.Text = item.ContainsKey("title") && item["title"].ToString() != "" ? item["title"].ToString() : Utility.GetFileName(item["path"].ToString());
                lbl_title.MouseEnter += new System.EventHandler(this.panelMouseEnter);
                lbl_title.MouseLeave += new System.EventHandler(this.panelMouseLeave);
                lbl_title.BackColor = System.Drawing.Color.Transparent;
                lbl_title.Click += RecentImageItem_Click;
                lbl_title.Tag = item["path"];

                Panel bar = new Panel();
                bar.BackColor = System.Drawing.Color.Gainsboro;
                bar.Location = new System.Drawing.Point(0, 104);
                bar.Name = "panel3";
                bar.Size = new System.Drawing.Size(586, 2);
                bar.Click += RecentImageItem_Click;
                bar.Tag = item["path"];

                Panel panel = new Panel();
                panel.Size = new System.Drawing.Size(586, 106);
                panel.Controls.Add(lbl_savedate);
                panel.Controls.Add(lbl_description);
                panel.Controls.Add(lbl_title);
                panel.Controls.Add(pb);
                panel.Controls.Add(bar);
                panel.MouseEnter += new System.EventHandler(this.panelMouseEnter);
                panel.MouseLeave += new System.EventHandler(this.panelMouseLeave);
                panel.Click += RecentImageItem_Click;
                panel.Tag = item["path"];

                this.flowParentPanel.Controls.Add(panel);
            }
        }

        private void LoadGraphData()
        {
            JArray tenant_graph_arr = new JArray();
            JArray user_graph_arr = new JArray();

            if (AuthAPI._tenant_guid != null && AuthAPI._tenant_guid != "" )
            {
                tenant_graph_arr = GraphAPI.GetTenantGraphs();
                user_graph_arr = GraphAPI.GetUserGraphs();
                
                InitCloudGraphData(tenant_graph_arr, user_graph_arr );
            }
        }

        private void RecentCloudImageItem_Click(object sender, EventArgs e)
        {
            string data = (sender as Control).Tag.ToString();
            string[] arr = data.Split(',');
            string graphGUID = arr[0];
            string childGUID = arr[1];

            JObject graphFileData = GraphAPI.GetGraphFileData(childGUID);
            file_type = "cloud";
            cloud_file_data = graphFileData["graphFileData"].ToString();
            graph_guid = graphGUID;
            child_guid = childGUID;
            DialogResult = DialogResult.Yes;
        }

        private void InitCloudGraphData(JArray tenant_graph_arr, JArray user_graph_arr)
        {
            this.flowLayoutCloud.Controls.Clear(); 
            for (int i = 0; i < tenant_graph_arr.Count; i++)
            {
                JObject obj = tenant_graph_arr[i] as JObject;
                JObject graphChildObj = GraphAPI.GetGraphChild(obj["graphGUID"].ToString());
                if (graphChildObj["childGraphGUID"] == null || graphChildObj["childGraphGUID"].ToString() == "00000000-0000-0000-0000-000000000000") continue;

                JObject graphFile = GraphAPI.GetGraphFile(graphChildObj["childGraphGUID"].ToString());
                //JObject graph_detail = GraphAPI.GetGraphDetail(obj["graphGUID"].ToString());
                //JObject graphFileImage = GraphAPI.GetGraphFileImage(graphChildObj["childGraphGUID"].ToString());
                //JObject graphFileData = GraphAPI.GetGraphFileData(graphChildObj["childGraphGUID"].ToString());
                //JObject graph_meta = GraphAPI.GetGraphMeta();

                PictureBox pb = new PictureBox();
                pb.Location = new System.Drawing.Point(3, 3);
                pb.Size = new System.Drawing.Size(90, 94);
                pb.SizeMode = PictureBoxSizeMode.Zoom;
                pb.Tag = obj["graphGUID"].ToString() + "," + graphChildObj["childGraphGUID"].ToString();
                pb.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
                pb.BackColor = System.Drawing.Color.White;
                pb.Click += RecentCloudImageItem_Click;
                if (graphFile.ContainsKey("image") && graphFile["image"].ToString() != "")
                {

                    Image img = Utility.GetImageFromBase64(graphFile["image"].ToString());
                    pb.Image = new Bitmap(img);
                }
                else
                {
                    pb.Image = new Bitmap("Recent/node_default.png");
                }
                pb.MouseEnter += new System.EventHandler(this.panelMouseEnter);
                pb.MouseLeave += new System.EventHandler(this.panelMouseLeave);

                Label lbl_savedate = new Label();  //date
                lbl_savedate.AutoSize = true;
                lbl_savedate.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                lbl_savedate.Location = new System.Drawing.Point(380, 10);
                lbl_savedate.Size = new System.Drawing.Size(118, 13);
                if (graphFile.ContainsKey("savedDateTime"))
                {
                    try
                    {
                        var dateTime = graphFile["savedDateTime"];
                        DateTime dt = (DateTime)dateTime;
                        string tempDate = dt.ToString("f");
                        lbl_savedate.Text = tempDate;
                    }
                    catch
                    {
                        lbl_savedate.Text = "?";
                    };
                }
                lbl_savedate.MouseEnter += new System.EventHandler(this.panelMouseEnter);
                lbl_savedate.MouseLeave += new System.EventHandler(this.panelMouseLeave);
                lbl_savedate.BackColor = System.Drawing.Color.Transparent;
                lbl_savedate.Click += RecentCloudImageItem_Click;
                lbl_savedate.Tag = obj["graphGUID"].ToString() + "," + graphChildObj["childGraphGUID"].ToString(); ;

                Label lbl_description = new Label(); //Description
                lbl_description.AutoEllipsis = true;
                lbl_description.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                lbl_description.Location = new System.Drawing.Point(100, 24);
                lbl_description.Size = new System.Drawing.Size(460, 73);
                lbl_description.Text = graphFile.ContainsKey("description") && graphFile["description"].ToString() != "" ? graphFile["description"].ToString() : graphFile["title"].ToString();
                lbl_description.MouseEnter += new System.EventHandler(this.panelMouseEnter);
                lbl_description.MouseLeave += new System.EventHandler(this.panelMouseLeave);
                lbl_description.BackColor = System.Drawing.Color.Transparent;
                lbl_description.Click += RecentCloudImageItem_Click;
                lbl_description.Tag = obj["graphGUID"].ToString() + "," + graphChildObj["childGraphGUID"].ToString(); ;


                Label lbl_title = new Label(); //Title
                lbl_title.AutoEllipsis = true;
                lbl_title.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                lbl_title.Location = new System.Drawing.Point(100, 3);
                lbl_title.Size = new System.Drawing.Size(347, 20);
                lbl_title.Text = graphFile.ContainsKey("title") && graphFile["title"].ToString() != "" ? graphFile["title"].ToString() : "";
                lbl_title.MouseEnter += new System.EventHandler(this.panelMouseEnter);
                lbl_title.MouseLeave += new System.EventHandler(this.panelMouseLeave);
                lbl_title.BackColor = System.Drawing.Color.Transparent;
                lbl_title.Click += RecentCloudImageItem_Click;
                lbl_title.Tag = obj["graphGUID"].ToString() + "," + graphChildObj["childGraphGUID"].ToString(); ;

                Panel bar = new Panel();
                bar.BackColor = System.Drawing.Color.Gainsboro;
                bar.Location = new System.Drawing.Point(0, 104);
                bar.Name = "panel4";
                bar.Size = new System.Drawing.Size(586, 2);
                bar.Click += RecentCloudImageItem_Click;
                bar.Tag = obj["graphGUID"].ToString() + "," + graphChildObj["childGraphGUID"].ToString(); ;

                Panel panel = new Panel();
                panel.Size = new System.Drawing.Size(586, 106);
                panel.Controls.Add(lbl_savedate);
                panel.Controls.Add(lbl_description);
                panel.Controls.Add(lbl_title);
                panel.Controls.Add(pb);
                panel.Controls.Add(bar);
                panel.MouseEnter += new System.EventHandler(this.panelMouseEnter);
                panel.MouseLeave += new System.EventHandler(this.panelMouseLeave);
                panel.Click += RecentCloudImageItem_Click;
                panel.Tag = obj["graphGUID"].ToString() + "," + graphChildObj["childGraphGUID"].ToString(); ;

                this.flowLayoutCloud.Controls.Add(panel);
            }
        }

        private void btnLocalOpen_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog()
            {
                Filter = "All files (*.*)|*.*|Graph files (*.graph)|*.graph",
                FilterIndex = 2,
                Title = "Open graph"
            })
            {
                try
                {
                    if (openFileDialog.ShowDialog(this) == DialogResult.OK)
                    {
                        local_file_name = openFileDialog.FileName;
                        DialogResult = DialogResult.Yes;
                    }
                }
                catch (Exception ex)
                {
                    NetGraphMessageBox.MessageBoxEx(this, $"Unable to open file. Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIconEx.Error);
                }
            }
        }

        private void panel8_MouseEnter(object sender, EventArgs e)
        {
            panel8.BackColor = SystemColors.Control;
        }

        private void panelMouseEnter(object sender, EventArgs e)
        {
            if (sender.GetType() == typeof(Panel))
            {
                (sender as Panel).BackColor = Color.FromArgb(199, 224, 244); ;
            }
            else if (sender.GetType() == typeof(Label))
            {
                (sender as Label).Parent.BackColor = Color.FromArgb(199, 224, 244); ;
            }
            else if (sender.GetType() == typeof(PictureBox))
            {
                (sender as PictureBox).Parent.BackColor = Color.FromArgb(199, 224, 244); 
            }

        }

        private void panelMouseLeave(object sender, EventArgs e)
        {

            if (sender.GetType() == typeof(Panel))
            {
                (sender as Panel).BackColor = Color.FromArgb(255, 255, 255);
            }
            else if (sender.GetType() == typeof(Label))
            {
                (sender as Label).Parent.BackColor = Color.FromArgb(255, 255, 255);
            }
            else if (sender.GetType() == typeof(PictureBox))
            {
               (sender as PictureBox).Parent.BackColor = Color.FromArgb(255, 255, 255);
            }

        }


    }
}
