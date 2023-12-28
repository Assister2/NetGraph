using System;
using System.Drawing;
using System.Windows.Forms;

namespace CyConex.Controls
{
    public class NoFocusableSplitContainer : SplitContainer
	{
		public NoFocusableSplitContainer()
		{
			SetStyle(ControlStyles.Selectable, false);
			SplitterWidth = 7;
		}
		
		protected override void OnGotFocus(EventArgs e)
		{

		}

		protected override void OnPaint(PaintEventArgs e)
		{
			e.Graphics.Clear(Color.FromKnownColor(KnownColor.Gainsboro));
		}

		protected override bool ShowFocusCues
		{
			get
			{
				return false;
			}
		}
	}
}
