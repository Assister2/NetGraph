using Syncfusion.WinForms.Controls;
using System;
using System.Windows.Forms;

namespace CyConex
{
    public partial class NodeAttributeModal : SfForm
    {
        public NodeAttributeModal()
        {
            InitializeComponent();
        }

        public string AttrImpact
        {
            get { return txtNodeAttrImpact.Text; }
            set { txtNodeAttrImpact.Text = value; }
        }

        public string AttrValue
        {
            get { return txtNodeAttrImpactValue.Text; }
            set { txtNodeAttrImpactValue.Text = value; }
        }

        public string AttrDesc
        {
            get { return txtNodeAttrImpactDesc.Text; }
            set
            {
                txtNodeAttrImpactDesc.Text = value;
                txtNodeAttrImpactDesc.SelectionStart = txtNodeAttrImpactDesc.Text.Length;
                txtNodeAttrImpactDesc.SelectionLength = 0;
            }
        }

        private void btnAssessmentSave_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}
