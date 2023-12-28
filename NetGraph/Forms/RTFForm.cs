using Syncfusion.Windows.Forms.Diagram;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Forms;

namespace CyConex.Forms
{
    public partial class RTFForm : Form
    {
        public RTFForm()
        {
            InitializeComponent();
        }

        public void LoadResouceFile(string title, string fileName)
        {
            string filePath = "Not set";
            try
            {
                string appPath = Application.ExecutablePath;

                // get the directory path of the currently executing application
                string dirPath = Path.GetDirectoryName(appPath);

                // build the file path by appending the file name to the directory path
                filePath = Path.Combine(dirPath, "Resources/Text/" + fileName);

                richTextBox1.LoadFile(filePath);
                this.Text = title;
            }
            catch 
            {
                richTextBox1.Text = $"Unable to load resource: {filePath}";
            }
        } 
    }
}
