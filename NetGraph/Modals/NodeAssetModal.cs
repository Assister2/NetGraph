using Syncfusion.WinForms.Controls;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace CyConex
{
    public partial class NodeAssetModal : SfForm
    {
        public NodeAssetModal()
        {
            InitializeComponent();
        }

        public string AssessmentStatus
        {
            get { return txtAssStatus.Text; }
            set { txtAssStatus.Text = value; }  
        }

        public string AssessmentValue
        {
            get { return txtAssValue.Text; }
            set { txtAssValue.Text = value; }
        }

        public string AssessmentDesc
        {
            get { return rTextAssDesc.Text; }
            set
            {
                rTextAssDesc.Text = value;
                rTextAssDesc.SelectionStart = rTextAssDesc.Text.Length;
                rTextAssDesc.SelectionLength = 0;
            }
        }

        public Color AssessmentColor
        {
            get { return txtAssColor.BackColor; }
            set { txtAssColor.BackColor = value; }
        }

        private void btnAssessmentSave_Click(object sender, EventArgs e)
        {
            double assValue = Double.Parse(txtAssValue.Text);
            if (assValue >= 0 && assValue <= 100)
            {
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                NetGraphMessageBox.MessageBoxEx(this, "Invalide Assessment Value", "Minimum = 0, Maximum = 100", MessageBoxButtons.OK, MessageBoxIconEx.Error, defaultButton: MessageBoxDefaultButton.Button3, 468, 234);
            }
            
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult= DialogResult.Cancel;
        }

        private void autoLabel2_Click(object sender, EventArgs e)
        {

        }

        private void txtAssColor_Click(object sender, EventArgs e)
        {
            ColorDialog dlg = new ColorDialog();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                txtAssColor.BackColor = dlg.Color;
            }
        }
    }
}
