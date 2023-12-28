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
    public partial class NewTenantModal : SfForm
    {
        public string TenantName
        {
            get { return txtTenantName.Text; }
            set { txtTenantName.Text = value; }
        }
        public string TenantDescription
        {
            get { return txtTenantDescription.Text; }
            set { txtTenantDescription.Text = value; }
        }

        public NewTenantModal()
        {
            InitializeComponent();
        }

        private void btnTenantCreate_Click(object sender, EventArgs e)
        {
            if (txtTenantName.Text.Trim() == "")
            {
                NetGraphMessageBox.MessageBoxEx(this, "Please enter a Tenant Name", "Tenant Name cannot be empty", MessageBoxButtons.OK, MessageBoxIconEx.Error, defaultButton: MessageBoxDefaultButton.Button3, 468, 234);
            }
            else              
                DialogResult = DialogResult.OK;
        }

        private void btnTenantCancel_Click(object sender, EventArgs e)
        {
            DialogResult=DialogResult.Cancel;
        }
    }
}
