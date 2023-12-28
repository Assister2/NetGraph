using Syncfusion.WinForms.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CyConex.Modals
{
    public partial class DockingLayoutNameModal : SfForm
    {
        public DockingLayoutNameModal()
        {
            InitializeComponent();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        public string GetLayoutName()
        {
            return txtDockingName.Text;
        }

        public void SetLayoutName(string name)
        {
            txtDockingName.Text = name;
        }
    }
}
