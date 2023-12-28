using CyConex.API;
using Newtonsoft.Json.Linq;
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
    public partial class UserSecurityGroupModal : SfForm
    {
        string _old_security_group_id;
        string _first_name;
        string _last_name;
        string _email;
        string _obj_id;
        string _obj_title;
        string _user_guid;
        JArray _groups_detail;

        public UserSecurityGroupModal()
        {
            InitializeComponent(); 
        }

        public void SetUserSecurityGroupData(string userGUID, string security_group_id, string firstName, string lastName, string email, string obj_id, string obj_title, JArray groups_detail )
        {
            _old_security_group_id = security_group_id; 
            _first_name = firstName;
            _last_name = lastName;
            _email = email;
            _obj_id = obj_id;
            _obj_title = obj_title;
            _groups_detail = groups_detail;
            _user_guid = userGUID;
            DisplaySecurityGroupData();
        }

        public void DisplaySecurityGroupData()
        {
            lblUserName.Text = _first_name + " " + _last_name;
            lblEmailAddress.Text = _email;
            lblObjectTitle.Text = _obj_title;

            cmbSecurityGroup.Items.Clear();
            int selected_index = 0;
            for (int i = 0; i < _groups_detail.Count; i++)
            {
                string group_name = _groups_detail[i]["groupName"].ToString();
                cmbSecurityGroup.Items.Add( group_name );
                if (_groups_detail[i]["SecurityGroupID"].ToString() == _old_security_group_id)
                {
                    selected_index = i;
                }
            }
            
            if (_groups_detail.Count > 0)
            {
                cmbSecurityGroup.SelectedIndex = selected_index;
                lblGroupDescription.Text = _groups_detail[selected_index]["groupDescription"].ToString();
            }
        }

        private void cmbSecurityGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblGroupDescription.Text = _groups_detail[cmbSecurityGroup.SelectedIndex]["groupDescription"].ToString();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            UpdateUserSecurityGroup();
        }

        private void UpdateUserSecurityGroup()
        {
            string security_group_guid = _groups_detail[cmbSecurityGroup.SelectedIndex]["SecurityGroupID"].ToString();
            if (_old_security_group_id != security_group_guid)
            {
                JObject obj = SecurityAPI.PutChangeUserSecurityGroup(_user_guid, _old_security_group_id, security_group_guid);
            }
            DialogResult = DialogResult.OK;
        }
    }
}
