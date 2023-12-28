using Syncfusion.WinForms.Controls;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using CefSharp;
using CefSharp.WinForms;
using Newtonsoft.Json.Linq;
using CyConex.Graph;
using System.Collections;
using CyConex.Helpers;
using FuzzySharp;
using Syncfusion.WinForms.ListView;
using Syncfusion.WinForms.ListView.Enums;
using System.Collections.ObjectModel;
using Syncfusion.Windows.Forms.Edit;
using Syncfusion.WinForms.DataGrid.Interactivity;
using Syncfusion.Windows.Forms.CellGrid;

namespace CyConex
{
    public partial class SearchDialogModal : SfForm
    {
        private ChromiumWebBrowser _browser;
        //private string[] _arrSearchIn = { "Title", "Reference", "Description", "Framework", "Notes", "Category", "Sub Category", "Version", "Domain/Sub Domain", "Level", "Tags" };
        private string[] _arrSearchIn = { "Title", "Reference", "Description" };
        private string[] _arrFilterBy = { "Actor", "Asset", "Attack", "Control", "Evidence", "Group", "Objective", "Vulnerability" };
        private int COLUMN_ID = 0;
        private ColumnChooserPopup columnChooser;
        public SearchDialogModal()
        {
            InitializeComponent();
            InitializeComboBox();
            this.Style.TitleBar.Height = 26;
        }

        private void InitializeComboBox()
        {
            sfCmbSearchIn.DisplayMember = "Name";
            //sfCmbSearchIn.DelimiterChar = ",";
            sfCmbSearchIn.DataSource = _arrSearchIn;

            ObservableCollection<object> search_data = new ObservableCollection<object>();
            search_data.Add("Title");
            sfCmbSearchIn.CheckedItems = search_data;

            sfCmbFilterBy.DisplayMember = "Name";
            sfCmbFilterBy.DataSource = _arrFilterBy;

            ObservableCollection<object> filter_data = new ObservableCollection<object>();
            filter_data.Add("Actor");
            filter_data.Add("Asset");
            filter_data.Add("Attack");
            filter_data.Add("Control");
            filter_data.Add("Evidence");
            filter_data.Add("Group");
            filter_data.Add("Objective");
            filter_data.Add("Vulnerability");
            sfCmbFilterBy.CheckedItems = filter_data;

            columnChooser = new ColumnChooserPopup(this.sfDataGridResult);
            columnChooser.ColumnChooser.CheckedListBox.Style.SelectionStyle.SelectionBackColor = Color.LightGreen;
            //sfCmbFilterBy.s
        }

        public void SetLocation(Point p)
        {
            this.Location = p;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {

        }

        public void SetBrowser(ChromiumWebBrowser _br)
        {
            _browser = _br;
        }
        public void ShowDialogWithBrowser(IWin32Window owner, ChromiumWebBrowser _br, Point p)
        {
            this.InvokeIfNeed(() =>
            {
                _browser = _br;
                SetLocation(p);
                Search();
                ShowDialog(owner);
            });
        }

        private static String WildCardToRegular(String value)
        {
            return System.Text.RegularExpressions.Regex.Escape(value).Replace("\\?", ".").Replace("\\*", ".*") + "$";
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            Search();
        }

        private bool isFuzzySearch(bool is_fuzzy, string content, string str)
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


        private async void Search()
        {
            List<string> checked_searchIn_items = new List<string>();
            foreach (object item in sfCmbSearchIn.CheckedItems)
            {
                checked_searchIn_items.Add(item.ToString().ToLower());
            }


            List<string> checked_filterBy_items = new List<string>();
            foreach (object item in sfCmbFilterBy.CheckedItems)
            {
                checked_filterBy_items.Add(item.ToString().ToLower());
            }

            bool is_title = checked_searchIn_items.Contains("title");// this.checkTitle.Checked;
            bool is_description = checked_searchIn_items.Contains("description");// this.checkDescription.Checked;
            bool is_notes = checked_searchIn_items.Contains("notes");// this.checkNotes.Checked;
            bool is_framework = checked_searchIn_items.Contains("framework");// this.checkFramework.Checked;
            bool is_control_ref = checked_searchIn_items.Contains("reference");// this.checkControlRef.Checked;
            bool is_category = checked_searchIn_items.Contains("category");
            bool is_sub_category = checked_searchIn_items.Contains("sub category");
            bool is_version = checked_searchIn_items.Contains("version");
            bool is_domain = checked_searchIn_items.Contains("domain/sub domain");
            bool is_level = checked_searchIn_items.Contains("level");
            bool is_tags = checked_searchIn_items.Contains("tags");

            //bool is_fuzzy = this.checkFuzzySearch.Checked;
            string str = this.txtFindWhat.Text.ToLower();
            if (_browser == null)
            {
                _browser = GraphUtil.getBrowser();
            }
            var allNodes = await _browser.EvaluateScriptAsync(@"getNodes();");
            //this.dataGridResult.Rows.Clear();
            this.sfDataGridResult.Columns.Clear();
            if (allNodes.Success)
            {
                var tmpObj = JArray.Parse(allNodes.Result.ToString());
                List<NodeSearch> array = new List<NodeSearch>();
                foreach (var tmpNode in tmpObj)
                {
                    string id = "";
                    string title = "";
                    string description = "";
                    string notes = "";
                    string framework = "";
                    string control_ref = "";
                    string category = "";
                    string sub_category = "";
                    string version = "";
                    string domain = "";
                    string subDomain = "";
                    string level = "";
                    string tags = "";
                    string type = "";

                    if (tmpNode["title"] != null)
                        title = tmpNode["title"].ToString();
                    if (tmpNode["description"] != null)
                        description = tmpNode["description"].ToString();
                    if (tmpNode["note"] != null)
                        notes = tmpNode["note"].ToString();
                    if (tmpNode["frameworkName"] != null)
                        framework = tmpNode["frameworkName"].ToString();
                    if (tmpNode["frameworkReference"] != null)
                        control_ref = tmpNode["frameworkReference"].ToString();
                    if (tmpNode["nodeType"] != null)
                        type = tmpNode["nodeType"].ToString();
                    if (tmpNode["id"] != null)
                        id = tmpNode["id"].ToString();
                    if (tmpNode["category"] != null)
                        category = tmpNode["category"].ToString();
                    if (tmpNode["sub_category"] != null)
                        sub_category = tmpNode["sub_category"].ToString();
                    if (tmpNode["version"] != null)
                        version = tmpNode["version"].ToString();
                    if (tmpNode["domain"] != null)
                        domain = tmpNode["domain"].ToString();
                    if (tmpNode["subdomain"] != null)
                        subDomain = tmpNode["subdomain"].ToString();
                    if (tmpNode["level"] != null)
                        level = tmpNode["level"].ToString();
                    if (tmpNode["tags"] != null)
                        tags = tmpNode["tags"].ToString();

                    bool flag = false;

                    if (!checked_filterBy_items.Contains(type))
                    {
                        continue;
                    }

                    var from_note = System.Convert.FromBase64String(notes);
                    string final_note = System.Text.Encoding.UTF8.GetString((from_note));

                    var from_description = System.Convert.FromBase64String(description);
                    string final_description = System.Text.Encoding.UTF8.GetString((from_description));

                    if (is_category &&
                        (System.Text.RegularExpressions.Regex.IsMatch(category.ToLower(), WildCardToRegular(str)) ||
                            category.ToLower().IndexOf(str) > -1
                        ))
                    {
                        flag = true;
                    }

                    if (is_sub_category &&
                        (System.Text.RegularExpressions.Regex.IsMatch(sub_category.ToLower(), WildCardToRegular(str)) ||
                            sub_category.ToLower().IndexOf(str) > -1
                        ))
                    {
                        flag = true;
                    }

                    if (is_version &&
                        (System.Text.RegularExpressions.Regex.IsMatch(version.ToLower(), WildCardToRegular(str)) ||
                            version.ToLower().IndexOf(str) > -1
                        ))
                    {
                        flag = true;
                    }

                    if (is_domain &&
                        (System.Text.RegularExpressions.Regex.IsMatch(domain.ToLower(), WildCardToRegular(str)) ||
                            domain.ToLower().IndexOf(str) > -1
                        ))
                    {
                        flag = true;
                    }

                    if (is_level &&
                        (System.Text.RegularExpressions.Regex.IsMatch(level.ToLower(), WildCardToRegular(str)) ||
                            level.ToLower().IndexOf(str) > -1
                        ))
                    {
                        flag = true;
                    }

                    if (is_tags &&
                        (System.Text.RegularExpressions.Regex.IsMatch(tags.ToLower(), WildCardToRegular(str)) ||
                            tags.ToLower().IndexOf(str) > -1
                        ))
                    {
                        flag = true;
                    }


                    if (is_title &&
                        (System.Text.RegularExpressions.Regex.IsMatch(title.ToLower(), WildCardToRegular(str)) || title.ToLower().IndexOf(str) > -1
                        ))
                    {
                        flag = true;
                    }


                    if (flag || (is_description &&
                        (System.Text.RegularExpressions.Regex.IsMatch(final_description.ToLower(), WildCardToRegular(str)) ||
                        final_description.ToLower().IndexOf(str) > -1)))
                    {
                        flag = true;
                    }

                    if (flag || (is_notes &&
                        (System.Text.RegularExpressions.Regex.IsMatch(final_note.ToLower(), WildCardToRegular(str)) ||
                        final_note.ToLower().IndexOf(str) > -1)))
                    {
                        flag = true;
                    }


                    if (flag || (is_framework &&
                        (System.Text.RegularExpressions.Regex.IsMatch(framework.ToLower(), WildCardToRegular(str)) ||
                        framework.ToLower().IndexOf(str) > -1)))
                    {
                        flag = true;
                    }


                    if (flag || (is_control_ref &&
                        (System.Text.RegularExpressions.Regex.IsMatch(control_ref.ToLower(), WildCardToRegular(str)) ||
                        control_ref.ToLower().IndexOf(str) > -1)))
                    {
                        flag = true;
                    }

                    if (flag)
                    {
                        array.Add(
                            new NodeSearch(
                                id,
                                Utility.RemoveHTML(title),
                                Utility.RemoveHTML(Utility.Base64Decode(description)),
                                control_ref,
                                framework,
                                Utility.RemoveHTML(Utility.Base64Decode(notes)),
                                category,
                                sub_category,
                                version,
                                domain,
                                subDomain,
                                level,
                                tags
                            )
                        );
                    }

                }
                sfDataGridResult.DataSource = array;
                sfDataGridResult.Columns[COLUMN_ID].Visible = false;
            }
            else
            {
                return;
            }
        }

        private async void sfDataGridResult_SelectionChanged(object sender, System.EventArgs e)
        {
            if (this.sfDataGridResult.SelectedIndex >= 0)
            {
                NodeSearch tmp = this.sfDataGridResult.SelectedItems[0] as NodeSearch;
                if (tmp != null)
                {
                    string nodeID = tmp.ID;
                    await _browser.EvaluateScriptAsync($"selectNodeAnimation('{nodeID}');");
                }
            }
        }

        private void sfDataGridResult_Click(object sender, EventArgs e)
        {
            MouseEventArgs me = (MouseEventArgs)e;
            if (me.Button == System.Windows.Forms.MouseButtons.Right) { MessageBox.Show("Right click"); }
        }

        private void btnSelectColumn_Click(object sender, EventArgs e)
        {
           
        }
        private void txtFindWhat_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == (char)Keys.Enter)
            {
                e.Handled = true; // Optional: to prevent the 'ding' sound.
                Search();
            }
        }

        private void contextMenuStrip1_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }

        private void menuColumnsSelect_Click(object sender, EventArgs e)
        {
            columnChooser.Show();
        }

        private void sfCmbSearchIn_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
