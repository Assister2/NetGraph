using CyConex.Graph;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CyConex.Helpers
{
    public partial class CalcLogForm : Form
    {
        public CalcLogForm()
        {
            InitializeComponent();
        }

        private void CalcLogForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            GraphCalcs.useCalcLog = false;
        }
    }
}
