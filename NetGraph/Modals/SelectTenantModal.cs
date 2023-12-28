using CyConex.API;
using Newtonsoft.Json.Linq;
using Syncfusion.Windows.Forms.Diagram;
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
    public partial class SelectTenantModal : SfForm
    {
        public TenantItem _selected_item = new TenantItem();
        public SelectTenantModal()
        {
            InitializeComponent();
        }

        public void SetTenantGridData(ArrayList arr)
        {
            this.gridTenants.Rows.Clear();

            for (int i = 0; i < arr.Count; i++)
            {
                TenantItem item = arr[i] as TenantItem;
                this.gridTenants.Rows.Add(item.TenantGUID, item.TenantName, item.TenantDescription);
            }
            btnRemove.Enabled = arr.Count > 1 ? true : false;
            btnSelect.Enabled = arr.Count > 0 ? true : false;
        }

        private TenantItem getSelectedItem()
        {
            TenantItem tenantItem = null;
            if (this.gridTenants.SelectedRows.Count > 0)
            {
                tenantItem = new TenantItem(
                    this.gridTenants.SelectedRows[0].Cells[0].Value.ToString(),
                    this.gridTenants.SelectedRows[0].Cells[1].Value.ToString(),
                    this.gridTenants.SelectedRows[0].Cells[2].Value.ToString()
                );
            }
            return tenantItem;
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            Graph.Utility.SaveAuditLog("Select Tenant", "Button Clicked", "", "", $"");
            _selected_item = this.getSelectedItem();
            this.DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult=DialogResult.Cancel;
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (gridTenants.RowCount < 2) return;
            Graph.Utility.SaveAuditLog("Remove Tenant", "Button Clicked", "", "", $"");
            _selected_item = this.getSelectedItem();
            if (_selected_item != null)
            {
                if (MessageBox.Show("Remove Tenant", "Remove this Tenant?", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    if (AuthAPI.DeleteTenant(_selected_item.TenantGUID))
                    {
                        AuthAPI._tenant_items.RemoveAt(this.gridTenants.SelectedRows[0].Index);
                        this.gridTenants.Rows.RemoveAt(this.gridTenants.SelectedRows[0].Index);

                        btnRemove.Enabled = gridTenants.RowCount > 1 && gridTenants.SelectedRows.Count > 0 ? true : false;
                        btnSelect.Enabled = gridTenants.SelectedRows.Count > 0 ? true : false;
                    }
                }
            }
            else
            {
                MessageBox.Show("Select the Tenant Item");
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Graph.Utility.SaveAuditLog("Add Tenant", "Button Clicked", "", "", $"");
            NewTenantModal newTenat = new NewTenantModal();
            if (newTenat.ShowDialog(this) == DialogResult.OK)
            {
                string tenantData = AuthAPI.CreateTenant(newTenat.TenantName, newTenat.TenantDescription);
                if (tenantData == null || tenantData == "" )
                {
                    System.Windows.Forms.MessageBox.Show("Cannot create a new Tenant." );
                    return;
                }
                JObject obj = JObject.Parse(tenantData);
                
                if (obj["error"] == null)
                {
                    TenantItem tenantItem = new TenantItem(
                        obj["tenantGUID"].ToString(),
                        newTenat.TenantName,
                        newTenat.TenantDescription
                    );
                    AuthAPI._tenant_items.Add(tenantItem);
                    SettingsAPI.PostSettingMeta("tenantGUID", obj["tenantGUID"].ToString());
                    this.gridTenants.Rows.Add(obj["tenantGUID"], newTenat.TenantName, newTenat.TenantDescription);

                    btnRemove.Enabled = gridTenants.RowCount > 1 && gridTenants.SelectedRows.Count > 0 ? true : false;
                    btnSelect.Enabled = gridTenants.SelectedRows.Count > 0 ? true : false;
                }
            }
        }

        private void btnTenantPermissions_Click(object sender, EventArgs e)
        {
            TenantItem selected_item = this.getSelectedItem();
            SecurityEditorModal securityEditor = new SecurityEditorModal();
            securityEditor.SetObjectItem("tenant", selected_item.TenantGUID, selected_item.TenantName);
            securityEditor.ShowEditorModal();
        }

        private void btnInviteTest_Click(object sender, EventArgs e)
        {
            JObject obj = InviteAPI.PostNewInvite(AuthAPI._user_guid, AuthAPI._enterprise_guid, AuthAPI._tenant_guid, "dmitrosamsonia@gmail.com");
            InviteAPI.PutAcceptInvite(obj["inviteGUID"].ToString(), AuthAPI._user_guid, AuthAPI._enterprise_guid, AuthAPI._tenant_guid, "dmitrosamsonia@gmail.com");
            int i = 1;
        }

        private void gridTenants_Click(object sender, EventArgs e)
        {
            btnRemove.Enabled = gridTenants.RowCount > 1 && gridTenants.SelectedRows.Count > 0 ? true : false;
            btnSelect.Enabled = gridTenants.SelectedRows.Count > 0 ? true : false;
        }

        private void gridTenants_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
