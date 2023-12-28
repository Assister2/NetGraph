using System;
using System.Windows.Forms;

namespace CyConex
{
    public static class NetGraphMessageBox
	{
		/// <summary>
		/// Display custom dialog
		/// </summary>
		/// <param name="owner">Owner window</param>
		/// <param name="text">Text for display</param>
		/// <param name="caption">Window caption</param>
		/// <param name="buttons">Buttons</param>
		/// <param name="icon">Dialog icon</param>
		/// <returns>Dialog result</returns>
		internal static DialogResult MessageBoxEx(Form owner, string text, string caption, MessageBoxButtons buttons, MessageBoxIconEx icon, MessageBoxDefaultButton defaultButton = MessageBoxDefaultButton.Button1, string button1Text = "", string button2Text = "", string button3Text = "", int button1Width = -1, int button2Width = -1, int button3Width = -1, int width = 468, int height = 234)
		{
			using (QuestionForm questionForm = new QuestionForm(text, caption, buttons, icon, false, defaultButton, button1Text, button2Text, button3Text, button1Width, button2Width, button3Width, width: width, height: height))
			{
				DialogResult retval = DialogResult.None;
				if (owner.InvokeRequired)
				{
					owner.Invoke((MethodInvoker)(() => { retval = questionForm.ShowDialog(owner); }));
				}
				else
				{
					if (owner.IsDisposed)
					{
						retval = questionForm.ShowDialog(Application.OpenForms[0]);
					}
					else
					{
						retval = questionForm.ShowDialog(owner);
					}
				}
				return retval;
			}
		}

		internal static DialogResult MessageBoxEx(Form owner, string text, string caption, MessageBoxButtons buttons, MessageBoxIconEx icon, MessageBoxDefaultButton defaultButton, int width = 468, int height = 234)
		{
			using (QuestionForm questionForm = new QuestionForm(text, caption, buttons, icon, false, defaultButton, width: width, height: height))
			{
				DialogResult retval = DialogResult.None;
				if (owner.InvokeRequired)
				{
					owner.Invoke((MethodInvoker)(() => { retval = questionForm.ShowDialog(owner); }));
				}
				else
				{
					if (owner.IsDisposed)
					{
						retval = questionForm.ShowDialog(Application.OpenForms[0]);
					}
					else
					{
						retval = questionForm.ShowDialog(owner);
					}
				}
				return retval;
			}
		}

		/// <summary>
		/// Display custom dialog and handle don't show again check status
		/// </summary>
		/// <param name="owner">Owner window</param>
		/// <param name="text">Text for display</param>
		/// <param name="caption">Window caption</param>
		/// <param name="buttons">Buttons</param>
		/// <param name="icon">Dialog icon</param>
		/// <param name="dontShowAgainChecked">Don't show again checked</param>
		/// <returns>Dialog result</returns>
		internal static DialogResult MessageBoxEx(Form owner, string text, string caption, MessageBoxButtons buttons, MessageBoxIconEx icon, out bool dontShowAgainChecked, MessageBoxDefaultButton defaultButton = MessageBoxDefaultButton.Button1, string button1Text = "", string button2Text = "", string button3Text = "", string dontShowAgainText = "", bool initiallyDontShowAgainChecked = false, int width = 468, int height = 234)
		{
			using (QuestionForm questionForm = new QuestionForm(text, caption, buttons, icon, true, defaultButton, button1Text, button2Text, button3Text, width: width, height: height))
			{
				if (!String.IsNullOrEmpty(dontShowAgainText))
				{
					questionForm.DontShowAgain_radCheckBox.Text = dontShowAgainText;
				}
				questionForm.DontShowAgain_radCheckBox.Checked = initiallyDontShowAgainChecked;
				DialogResult retval = DialogResult.None;
				if (owner.InvokeRequired)
				{
					owner.Invoke((MethodInvoker)(() => { retval = questionForm.ShowDialog(owner); }));
				}
				else
				{
					retval = questionForm.ShowDialog(owner);
				}
				dontShowAgainChecked = questionForm.DontShowAgain_radCheckBox.Checked;
				return retval;
			}
		}

	}
}
