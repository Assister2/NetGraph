using Syncfusion.WinForms.Controls;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace CyConex
{
    public partial class EdgeStrengthModal : SfForm
    {
        public EdgeStrengthModal()
        {
            InitializeComponent();
        }

        public string AssessmentStatus
        {
            get { return txtRStrengthStatus.Text; }
            set { txtRStrengthStatus.Text = value; }
        }

        public string AssessmentValue
        {
            get { return txtRStrengthValue.Text; }
            set { txtRStrengthValue.Text = value; }
        }

        public string AssessmentDesc
        {
            get { return rTextRStrengthDesc.Text; }
            set
            {
                rTextRStrengthDesc.Text = value;
                rTextRStrengthDesc.SelectionStart = rTextRStrengthDesc.Text.Length;
                rTextRStrengthDesc.SelectionLength = 0;
            }
        }

        private void btnRStrengthSave_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

    }
}
