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
    public partial class UserDetailModal : SfForm
    {
        public MainForm ribbonForm = null;

        public UserDetailModal()
        {
            InitializeComponent();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        public void SetUserData(JObject user_data)
        {
            txtFirstName.Text = user_data["givenName"].ToString();
            txtLastName.Text = user_data["surname"].ToString();
            txtDisplayName.Text = user_data["displayName"].ToString();
            txtEmailAddress.Text = user_data["emailAddress"].ToString();

            UpdateEnterprise();
            UpdateTenant();
        }

        public void UpdateEnterprise()
        {
            string enterpriseDetail = AuthAPI.GetEnterpriseDetail(AuthAPI._enterprise_guid);
            if (enterpriseDetail != "")
            {
                JObject enterpriseObj = JObject.Parse(enterpriseDetail);
                txtEnterprise.Text = enterpriseObj["name"].ToString();
                btnChangeTenant.Enabled = true;
            }
            else
            {
                btnChangeTenant.Enabled = false;
            }
        }

        public void UpdateTenant()
        {
            string tenantDetail = AuthAPI.GetTenantDetail(AuthAPI._tenant_guid);
            if (tenantDetail != "")
            {
                JObject tenanObj = JObject.Parse(tenantDetail);
                txtTenant.Text = tenanObj["name"].ToString();
            }
        }

        private void btnChangeEnterprise_Click(object sender, EventArgs e)
        {
            if (ribbonForm != null) ribbonForm.changeEnterprise();
            UpdateEnterprise() ;
            UpdateTenant();
        }

        private void btnChangeTenant_Click(object sender, EventArgs e)
        {
            if (ribbonForm != null) ribbonForm.changeTenant();
            UpdateTenant() ;
        }
    }
}
