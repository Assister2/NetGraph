using CyConex.API;
using CyConex.Graph;
using Newtonsoft.Json.Linq;
using Syncfusion.WinForms.Controls;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CyConex
{
    public partial class SelectEnterpriseModal : SfForm
    {
        public EnterpriseItem _selected_item;
        public SelectEnterpriseModal()
        {
            InitializeComponent();
        }

        public void SetGridEnterpriseData(ArrayList arr)
        {
            this.gridEnterprises.Rows.Clear();
            for (int i = 0; i < arr.Count; i++)
            {
                EnterpriseItem item = arr[i] as EnterpriseItem;
                this.gridEnterprises.Rows.Add(item.EnterpriseGUID, item.EnterpriseName,item.AddressLine1, item.AddressLine2, item.Postcode, item.City, item.State, item.Country);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            Graph.Utility.SaveAuditLog("Select Enterprise", "Button Clicked", "", "", $"");
            _selected_item = this.getSelectedItem();
            this.DialogResult = DialogResult.OK;
        }

        private EnterpriseItem getSelectedItem()
        {
            EnterpriseItem enterpriseItem = null;
            if (this.gridEnterprises.SelectedRows.Count > 0)
            {
                try
                {
                    enterpriseItem = new EnterpriseItem(
                        this.gridEnterprises.SelectedRows[0].Cells[0].Value.ToString(),
                        this.gridEnterprises.SelectedRows[0].Cells[1].Value.ToString(),
                        this.gridEnterprises.SelectedRows[0].Cells[2].Value.ToString(),
                        this.gridEnterprises.SelectedRows[0].Cells[3].Value.ToString(),
                        this.gridEnterprises.SelectedRows[0].Cells[4].Value.ToString(),
                        this.gridEnterprises.SelectedRows[0].Cells[5].Value.ToString(),
                        this.gridEnterprises.SelectedRows[0].Cells[6].Value.ToString(),
                        this.gridEnterprises.SelectedRows[0].Cells[7].Value.ToString());
                }
                catch
                { }
            }
            return enterpriseItem;
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            Graph.Utility.SaveAuditLog("Remove Tenant", "Button Clicked", "", "", $"");
            _selected_item = this.getSelectedItem();
            if (_selected_item != null)
            {
                if (MessageBox.Show("Remove Enterprise", "Remove this Enterprise?", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    if (AuthAPI.DeleteEnterprise(_selected_item.EnterpriseGUID))
                    {
                        this.gridEnterprises.Rows.RemoveAt(this.gridEnterprises.SelectedRows[0].Index);
                        AuthAPI._enterprise_items.RemoveAt(this.gridEnterprises.SelectedRows[0].Index);
                    }
                }
            }
            else
            {
                MessageBox.Show("Select the Enterprise Item");
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Graph.Utility.SaveAuditLog("Add Tenant", "Button Clicked", "", "", $"");
            if (string.IsNullOrEmpty(AuthAPI._user_guid))
            {
                System.Windows.Forms.MessageBox.Show("Logon is Missing ");
                Graph.Utility.SaveAuditLog("Add Tenant", "Error", "", "", $"Not Logged On");
            }
            else
            {
                NewEnterpriseModal newEnterprise = new NewEnterpriseModal();
                JObject id = new JObject();
                if (newEnterprise.ShowDialog(this) == DialogResult.OK)
                {
                    string id_str = AuthAPI.CreateEnterprise(newEnterprise.EnterpriseName, newEnterprise.EnterpriseAddress1,
                                newEnterprise.EnterpriseAddress2, newEnterprise.EnterprisePostcode,
                                newEnterprise.EnterpriseCity, newEnterprise.EnterpriseCountry,
                                newEnterprise.EnterpriseState);
                    if (id_str == "" || id_str == null)
                    {
                        System.Windows.Forms.MessageBox.Show("Cannot create Enterprise");
                        return;
                    }
                    id = JObject.Parse(id_str);
                    if (id != null && id["error"] == null )
                    {
                        this.gridEnterprises.Rows.Add(id["enterpriseGUID"].ToString(), newEnterprise.EnterpriseName, newEnterprise.EnterpriseAddress1, newEnterprise.EnterpriseAddress2,
                            newEnterprise.EnterprisePostcode, newEnterprise.EnterpriseCity, newEnterprise.EnterpriseState, newEnterprise.EnterpriseCountry);

                        AuthAPI._enterprise_items.Add(new EnterpriseItem(
                            id["enterpriseGUID"].ToString(),
                            newEnterprise.EnterpriseName,
                            newEnterprise.EnterpriseAddress1,
                            newEnterprise.EnterpriseAddress2,
                            newEnterprise.EnterprisePostcode ,
                            newEnterprise.EnterpriseCity,
                            newEnterprise.EnterpriseState,
                            newEnterprise.EnterpriseCountry
                        ));

                        SettingsAPI.PostSettingMeta("enterpriseGUID", id["enterpriseGUID"].ToString());
                    }
                    else
                    {
                        System.Windows.Forms.MessageBox.Show("Cannot create Enterprise");
                    }
                }
            }
        }

        private void SelectEnterprise_FormClosed(object sender, FormClosedEventArgs e)
        {

        }

        private void btnEnterprisePermission_Click(object sender, EventArgs e)
        {
            EnterpriseItem enterprise_item = this.getSelectedItem();
            SecurityEditorModal securityEditor = new SecurityEditorModal();
            securityEditor.SetObjectItem("enterprise", enterprise_item.EnterpriseGUID, enterprise_item.EnterpriseName);
            securityEditor.ShowEditorModal();
        }

        private void gridEnterprises_Click(object sender, EventArgs e)
        {
            btnRemove.Enabled = gridEnterprises.SelectedRows.Count > 0 ? true : false;
            btnSelect.Enabled = gridEnterprises.SelectedRows.Count > 0 ? true : false;
        }
    }
}
