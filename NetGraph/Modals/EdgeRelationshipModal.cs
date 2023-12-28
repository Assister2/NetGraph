using Syncfusion.WinForms.Controls;
using System;
using System.Windows.Forms;

namespace CyConex
{
    public partial class EdgeRelationshipModal : SfForm
    {
        public EdgeRelationshipModal()
        {
            InitializeComponent();
        }
        public string EdgeRelationData
        {
            get { return txtEditRelationItem.Text; }
            set
            {
                txtEditRelationItem.Text = value;
                txtEditRelationItem.SelectionStart = txtEditRelationItem.Text.Length;
                txtEditRelationItem.SelectionLength = 0;
            }
        }

        private void btnOkay_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel ;
        }
    }
}
