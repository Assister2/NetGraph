using System.Windows.Forms;

namespace CyConex
{
    public class NoFocusableButton : Button
	{
		public NoFocusableButton()
		{
			SetStyle(ControlStyles.Selectable, false);
		}
	}
}
