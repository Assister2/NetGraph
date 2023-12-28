using System;
using System.Drawing;
using System.Windows.Forms;

namespace CyConex.Controls
{
    public partial class NodeColorizationControl : UserControl
	{
		public event EventHandler OnDeleteControlRequested;
		public event EventHandler ColorDialogRequested;

		public double MinValue
		{
			get
			{
				return (double)MinValue_numericUpDownExt.Value;
			}

			set
			{
				MinValue_numericUpDownExt.Value = (decimal)value;
			}
		}

		public double MaxValue
		{
			get
			{
				return (double)MaxValue_numericUpDownExt.Value;
			}

			set
			{
				MaxValue_numericUpDownExt.Value = (decimal)value;
			}
		}

		public Color NodeColor
		{
			get 
			{ 
				return Color_panel.BackColor; 
			}

			set 
			{ 
				Color_panel.BackColor = value; 
			}
		}

		public NodeColorizationControl()
		{
			InitializeComponent();
		}

		private void Delete_noFocusableButton_Click(object sender, EventArgs e)
		{
			OnDeleteControlRequested?.Invoke(this, new EventArgs());
		}

		private void Color_panel_Click(object sender, EventArgs e)
		{
			ColorDialogRequested?.Invoke(this, new EventArgs());
		}
	}
}
