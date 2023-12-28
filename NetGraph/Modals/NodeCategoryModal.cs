using Syncfusion.WinForms.Controls;
using System;
using System.Windows.Forms;

namespace CyConex
{
    public partial class NodeCategoryModal : SfForm
    {
        public NodeCategoryModal()
        {
            InitializeComponent();
        }

        public string NodeCategoryText
        {
            get { return categoryText.Text; }
            set { categoryText.Text = value; }
        }

        private void NodeCategoryModal_Load(object sender, EventArgs e)
        {

        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult= DialogResult.Cancel;
        }
    }
}
