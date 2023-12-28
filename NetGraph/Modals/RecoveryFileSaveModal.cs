using CyConex.API;
using Newtonsoft.Json.Linq;
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
    public partial class RecoveryFileSaveModal : SfForm
    {
        public RecoveryFileSaveModal()
        {
            InitializeComponent(); 
        }

        private void btnSaveFileAsNew_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void btnDiscardFile_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Ignore;
        }
    }
}
