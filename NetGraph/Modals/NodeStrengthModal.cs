using Syncfusion.WinForms.Controls;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace CyConex
{
    public partial class NodeStrengthModal : SfForm
    {
        public NodeStrengthModal()
        {
            InitializeComponent();
        }

        public string NodeStrength
        {
            get { return txtNodeStrength.Text; }
            set { txtNodeStrength.Text = value; }
        }

        public string Strength
        {
            get { return txtStrength.Text; }
            set { txtStrength.Text = value; }
        }

        public string Description
        {
            get { return txtDescription.Text; }
            set { txtDescription.Text = value; }
        }

        /*public Color AssColor
        {
            get { return txtColor.BackColor; }
            set { txtColor.BackColor = value; }
        }*/

        private void btnOk_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult= DialogResult.Cancel;
        }

        private void autoLabel1_Click(object sender, EventArgs e)
        {

        }

        private void txtDescription_TextChanged(object sender, EventArgs e)
        {

        }

        private void NodeStrengthModal_Load(object sender, EventArgs e)
        {

        }

        private void txtColor_Click(object sender, EventArgs e)
        {
            ColorDialog dlg = new ColorDialog();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                //txtColor.BackColor = dlg.Color;
            }
        }
    }
}
