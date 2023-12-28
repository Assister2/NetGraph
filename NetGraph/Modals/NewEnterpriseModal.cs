using Syncfusion.Windows.Forms.Tools;
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
    public partial class NewEnterpriseModal : SfForm
    {
        public NewEnterpriseModal()
        {
            InitializeComponent();
        }

        public string EnterpriseName
        {
            get { return txtEnterpriseName.Text; }
            set { txtEnterpriseName.Text = value; }
        }

        public string EnterpriseAddress1
        {
            get { return txtEnterpriseAddress1.Text; }
            set { txtEnterpriseAddress1.Text = value; }
        }

        public string EnterpriseAddress2
        {
            get { return txtEnterpriseAddress2.Text; }
            set { txtEnterpriseAddress2.Text = value; }
        }

        public string EnterprisePostcode
        {
            get { return txtEnterprisePostcode.Text; }
            set { txtEnterprisePostcode.Text = value; }
        }
    
        public string EnterpriseCity
        {
            get { return txtEnterpriseCity.Text; }
            set { txtEnterpriseCity.Text = value; }
        }

        public string EnterpriseState
        {
            get { return txtEnterpriseState.Text; }
            set { txtEnterpriseState.Text = value; }
        }

        public string EnterpriseCountry
        {
            get { return txtEnterpriseCountry.Text; }
            set { txtEnterpriseCountry.Text = value; }
        }

        private void btnEnterpriseCreate_Click(object sender, EventArgs e)
        {
            if(txtEnterpriseName.Text.Trim() == "")
            {
                NetGraphMessageBox.MessageBoxEx(this, "Please enter an Enterprise Name", "Enterprise Name cannot be empty", MessageBoxButtons.OK, MessageBoxIconEx.Error, defaultButton: MessageBoxDefaultButton.Button3, 468, 234);
            }
            else
                DialogResult = DialogResult.OK;
        }

        private void btnEnterpriseCancel_Click(object sender, EventArgs e)
        {
            DialogResult=DialogResult.Cancel;
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void txtEnterpriseCity_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
