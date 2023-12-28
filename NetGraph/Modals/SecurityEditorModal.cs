using CyConex.API;
using Newtonsoft.Json.Linq;
using Syncfusion.Data.Extensions;
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
    public partial class SecurityEditorModal : SfForm
    {
        private string object_type = string.Empty;
        private string object_id = string.Empty;
        private string object_title = string.Empty;

        private JArray groups = new JArray();
        private JArray groups_detail = new JArray();
        private JArray group_members = new JArray();

        private JArray users = new JArray();
        private JArray users_detail = new JArray();

        private JArray user_groups = new JArray();
        public SecurityEditorModal()
        {
            InitializeComponent();
        }

        public void SetObjectItem(string type, string id, string title)
        {
            object_type = type;
            object_id = id;
            object_title = title;

            lblObjectTitle.Text = title;
        }

        public void InitUserGroupInfo()
        {
            groups.Clear();
            groups_detail.Clear();
            users.Clear();
            users_detail.Clear();
            groups = SecurityAPI.GetObjectSecurityGroups(object_id);
            for (int i = 0; i < groups.Count; i++)
            {
                JObject item = groups[i] as JObject;
                JObject group_detail = SecurityAPI.GetSecurityGroupDetail(item["SecurityGroupID"].ToString());
                JArray members = SecurityAPI.GetSecurityGroupMemebers(item["SecurityGroupID"].ToString());
                group_detail["members"] = members;
                group_detail["SecurityGroupID"] = item["SecurityGroupID"].ToString();
                groups_detail.Add(group_detail);
            }

            users = this.GetUsers();
            for (int i = 0; i < users.Count; i++)
            {
                JObject item = users[i] as JObject;
                string user_guid = "";
                switch (object_type)
                {
                    case "enterprise":
                        user_guid = item["enterpriseUserGUID"].ToString();
                        break;
                    case "tenant":
                        user_guid = item["tenantUserGUID"].ToString();
                        break;
                    case "graph":
                        break;
                }
                JObject tmp = AuthAPI.GetUser(user_guid);
                JObject member_obj = GetGroupInfoFromUser(groups_detail, user_guid);
                tmp["groupInfo"] = member_obj;
                users_detail.Add(tmp);
            }
        }

        public void InitUserDataGridData()
        {
            dataGridUserList.Rows.Clear();
            for (int i = 0; i < users_detail.Count; i++)
            {

                JObject tmp = users_detail[i] as JObject;
                JObject member_obj = tmp["groupInfo"] as JObject;

                if (member_obj["groupName"] != null)
                {
                    string group_name = member_obj["groupName"] == null ? "Reader" : member_obj["groupName"].ToString();
                    string[] rows = new string[] { tmp["givenName"].ToString(), tmp["surname"].ToString(), tmp["emailAddress"].ToString(), group_name };
                    dataGridUserList.Rows.Add(rows);
                }
            }
        }

        public DialogResult ShowEditorModal()
        {
            InitUserGroupInfo();
            InitUserDataGridData();
            return this.ShowDialog();
        }

        public JObject GetGroupInfoFromUser(JArray groups, string user_guid)
        {
            JObject obj = new JObject();
            for (int i = 0; i < groups.Count; i++)
            {
                JObject item = groups[i] as JObject;
                JArray members = item["members"] as JArray;
                if (members != null)
                {
                    for (int j = 0; j < members.Count; j++)
                    {
                        JObject member_item = members[j] as JObject;
                        if (member_item["securityGroupUserGUID"].ToString() == user_guid)
                        {
                            return item;
                        }
                    }
                }
            }
            return obj;
        }

        public JArray GetUsers()
        {
            JArray arr = new JArray();
            switch (object_type)
            {
                case "tenant":
                    arr = AuthAPI.GetTenantUserList("");
                    break;
                case "enterprise":
                    arr = AuthAPI.GetEnterpriseUserList();
                    break;
                case "graph":
                    break;
            }
            return arr;
        }

        private void dataGridUserList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string selected_email = dataGridUserList.SelectedRows[0].Cells[2].Value.ToString();
            JObject user_obj = GetUserInfoFromEmail(selected_email);
            JObject group_obj = GetGroupInfoFromUser(groups_detail, user_obj["userGUID"].ToString());

            if (group_obj["create"] != null)
            {
                chkGroupCreate.Checked = group_obj["create"].ToString().ToLower() == "true" ? true : false;
                chkGroupRead.Checked = group_obj["read"].ToString().ToLower() == "true" ? true : false;
                chkGroupUpdate.Checked = group_obj["update"].ToString().ToLower().Equals("true") ? true : false;
                chkGroupDelete.Checked = group_obj["delete"].ToString().ToLower() == "true" ? true : false;
            }
            else
            {
                chkGroupCreate.Checked = false;
                chkGroupRead.Checked = false;
                chkGroupUpdate.Checked = false;
                chkGroupDelete.Checked = false;
            }
        }

        private JObject GetUserInfoFromEmail(string email)
        {
            JObject tmp = new JObject();
            for (int i = 0; i < users_detail.Count; i++)
            {
                if (users_detail[i]["emailAddress"].ToString() == email)
                {
                    tmp = users_detail[i] as JObject; break;
                }
            }
            return tmp;
        }

        private void btnEditUser_Click(object sender, EventArgs e)
        {
            editSecurityUser();
        }

        private void editSecurityUser()
        {
            string selected_email = dataGridUserList.SelectedRows[0].Cells[2].Value.ToString();
            JObject user_obj = GetUserInfoFromEmail(selected_email);
            JObject group_obj = GetGroupInfoFromUser(groups_detail, user_obj["userGUID"].ToString());
            if (group_obj != null)
            {
                UserSecurityGroupModal userSecurityGroup = new UserSecurityGroupModal();
                userSecurityGroup.SetUserSecurityGroupData(user_obj["userGUID"].ToString(), group_obj["SecurityGroupID"].ToString(), user_obj["givenName"].ToString(), user_obj["surname"].ToString(), user_obj["emailAddress"].ToString(), object_id, object_title, groups_detail);
                userSecurityGroup.ShowDialog(this); 
                InitUserGroupInfo();
            }
        }

        private void btnAddUser_Click(object sender, EventArgs e)
        {
            AddUserSecurityEditorModal addUserSecurityEditor = new AddUserSecurityEditorModal();
            addUserSecurityEditor.SetUserData(users_detail, groups_detail);
            addUserSecurityEditor.ShowDialog(this);
            InitUserGroupInfo(); 

            for (int i = 0; i < addUserSecurityEditor.user_groups.Count; i++)
            {
                JObject tmp = addUserSecurityEditor.user_groups[i] as JObject;
                string[] rows = new string[] { tmp["givenName"].ToString(), tmp["surname"].ToString(), tmp["emailAddress"].ToString(), tmp["groupName"].ToString() };
                dataGridUserList.Rows.Add(rows);
            }
        }

        private void btnRemoveUser_Click(object sender, EventArgs e)
        {
            string selected_email = dataGridUserList.SelectedRows[0].Cells[2].Value.ToString();
            JObject user_obj = GetUserInfoFromEmail(selected_email);
            JObject group_obj = GetGroupInfoFromUser(groups_detail, user_obj["userGUID"].ToString());

            if (group_obj != null )
            {
                dataGridUserList.Rows.RemoveAt(dataGridUserList.SelectedRows[0].Index);
                JObject obj = SecurityAPI.DeleteSecurityGroupUsers(user_obj["userGUID"].ToString(), group_obj["SecurityGroupID"].ToString());
                InitUserGroupInfo();
            }
        }
    }
}