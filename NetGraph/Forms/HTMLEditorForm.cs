using Syncfusion.WinForms.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CyConex.Forms
{
    public partial class HTMLEditorForm : SfForm
    {
        public HTMLEditorForm()
        {
            InitializeComponent();
            //this.FormBorderStyle = FormBorderStyle.None;
            //this.WindowState = FormWindowState.Maximized;
        }

        public void SetDocumentHTMLData(string content)
        {
            htmlEditControl.DocumentHTML = content;
        }

        public string GetDocumentHTMLData()
        {
            return htmlEditControl.DocumentHTML;
        }
    }
}
