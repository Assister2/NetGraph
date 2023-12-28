using Syncfusion.WinForms.Controls;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace CyConex
{
    public partial class EdgeDisplayModal : SfForm
    {
        public EdgeDisplayModal()
        {
            InitializeComponent();
        }

        public string EdgeDisplayValueFrom
        {
            get { return txtEdgeDisplayValueFrom.Text; }
            set { txtEdgeDisplayValueFrom.Text = value; }
        }

        public string EdgeDisplayValueTo
        {
            get { return txtEdgeDisplayValueTo.Text; }
            set { txtEdgeDisplayValueTo.Text = value; }
        }

        public string EdgeDisplayWidth
        {
            get { return txtEdgeDisplayWidth.Text; }
            set { txtEdgeDisplayWidth.Text = value; }
        }

        public Color EdgeDisplayColor
        {
            get { return txtEdgeDisplayColor.BackColor; }
            set { txtEdgeDisplayColor.BackColor = value; }
        }
        private void btnRStrengthSave_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void txtEdgeDisplayColor_Click(object sender, EventArgs e)
        {
            ColorDialog colorDlg = new ColorDialog();
            if (colorDlg.ShowDialog() == DialogResult.OK)
            {
                txtEdgeDisplayColor.BackColor = colorDlg.Color;
            }
        }
    }
}
