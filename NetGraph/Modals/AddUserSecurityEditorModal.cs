using CyConex.API;
using Newtonsoft.Json.Linq;
using Syncfusion.Grouping;
using Syncfusion.Windows.Forms.Tools.Win32API;
using Syncfusion.WinForms.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CyConex
{
    public partial class AddUserSecurityEditorModal : SfForm
    {
        private JArray user_details = new JArray();
        private JArray group_details = new JArray();
        public JArray user_groups = new JArray();
        public AddUserSecurityEditorModal()
        {
            InitializeComponent(); 
        }

        public void SetUserData(JArray arr, JArray arr1)
        {
            user_details = arr;
            group_details = arr1;
            InitGridData();
            InitGroupData();
        }

        private void InitGridData()
        {
            for (int i = 0; i < user_details.Count; i++)
            {
                JObject item = user_details[i] as JObject;
                string user_guid = item["userGUID"].ToString();
                JObject groupInfo = item["groupInfo"] as JObject;
                if (groupInfo["groupName"] == null)
                { 
                    string[] rows = new string[] { item["givenName"].ToString(), item["surname"].ToString(), item["emailAddress"].ToString() };
                    gridUserList.Rows.Add(rows);
                }
            }
        }

        private void InitGroupData()
        {
            cmbSecurityGroup.Items.Clear(); 
            for (int i = 0; i < group_details.Count; i++)
            {
                string group_name = group_details[i]["groupName"].ToString();
                cmbSecurityGroup.Items.Add(group_name);
            }
        }

        private void btnAddUser_Click(object sender, EventArgs e)
        {
            string selected_email = gridUserList.SelectedRows[0].Cells[2].Value.ToString();
            JObject user_obj = GetUserInfoFromEmail(selected_email);
            string userGUID = user_obj["userGUID"].ToString();
            JObject group_obj = group_details[cmbSecurityGroup.SelectedIndex] as JObject;
            string groupGUID = group_obj["SecurityGroupID"].ToString();
            JObject obj = SecurityAPI.PostAddUserToSecurityGroup(userGUID, groupGUID);

            System.Windows.Forms.MessageBox.Show("Added new user to the Group!");
            gridUserList.Rows.RemoveAt(gridUserList.SelectedRows[0].Index);

            JObject user_group = new JObject();
            user_group["givenName"] = user_obj["givenName"];
            user_group["surname"] = user_obj["surname"];
            user_group["emailAddress"] = user_obj["emailAddress"];
            user_group["groupName"] = group_obj["groupName"];
            user_groups.Add(user_group);    
        }

        private JObject GetUserInfoFromEmail(string email)
        {
            JObject tmp = new JObject();
            for (int i = 0; i < user_details.Count; i++)
            {
                if (user_details[i]["emailAddress"].ToString() == email)
                {
                    tmp = user_details[i] as JObject; break;
                }
            }
            return tmp;
        }
    }
}
