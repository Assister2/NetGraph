using Syncfusion.WinForms.Controls;
using System;
using System.Windows.Forms;

namespace CyConex
{
    public partial class KeyWordForm : SfForm
    {
        public KeyWordForm()
        {
            InitializeComponent();

            this.Style.TitleBar.Height = 26;
        }

        public string KeyWordText
        {
            get {  return txtKeyWord.Text; }
            set { txtKeyWord.Text = value; }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}
