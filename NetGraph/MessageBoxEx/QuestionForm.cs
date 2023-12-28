using CyConex.Helpers;
using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace CyConex
{
    public partial class QuestionForm : Form
	{
		private MessageBoxDefaultButton _defaultButton;
		private MessageBoxDefaultButton _selfAnswerButton;
		private Button _currentSelfAnswerButton;



		public bool DontShowAgainChecked
		{
			get
			{
				return DontShowAgain_radCheckBox.Checked;
			}
			set
			{
				this.InvokeIfNeed(() => { DontShowAgain_radCheckBox.Checked = value; });
			}
		}


		private Image GetIconImage(MessageBoxIconEx icon)
		{
			Image retval = null;
			switch (icon)
			{
				case MessageBoxIconEx.Asterisk:
					{
						var shStockIconInfo = new NativeMethods.SHSTOCKICONINFO();
						shStockIconInfo.cbSize = (UInt32)Marshal.SizeOf(typeof(NativeMethods.SHSTOCKICONINFO));
						NativeMethods.SHGetStockIconInfo(NativeMethods.SHSTOCKICONID.SIID_INFO, NativeMethods.SHGSI.SHGSI_ICON, ref shStockIconInfo);
						retval = Icon.FromHandle(shStockIconInfo.hIcon).ToBitmap();
					}
					break;
				case MessageBoxIconEx.Error:
					{
						var shStockIconInfo = new NativeMethods.SHSTOCKICONINFO();
						shStockIconInfo.cbSize = (UInt32)Marshal.SizeOf(typeof(NativeMethods.SHSTOCKICONINFO));
						NativeMethods.SHGetStockIconInfo(NativeMethods.SHSTOCKICONID.SIID_ERROR, NativeMethods.SHGSI.SHGSI_ICON, ref shStockIconInfo);
						retval = Icon.FromHandle(shStockIconInfo.hIcon).ToBitmap();
					}
					break;
				case MessageBoxIconEx.Exclamation:
					{
						var shStockIconInfo = new NativeMethods.SHSTOCKICONINFO();
						shStockIconInfo.cbSize = (UInt32)Marshal.SizeOf(typeof(NativeMethods.SHSTOCKICONINFO));
						NativeMethods.SHGetStockIconInfo(NativeMethods.SHSTOCKICONID.SIID_WARNING, NativeMethods.SHGSI.SHGSI_ICON, ref shStockIconInfo);
						retval = Icon.FromHandle(shStockIconInfo.hIcon).ToBitmap();
					}
					break;
				case MessageBoxIconEx.None:
					{
						var shStockIconInfo = new NativeMethods.SHSTOCKICONINFO();
						shStockIconInfo.cbSize = (UInt32)Marshal.SizeOf(typeof(NativeMethods.SHSTOCKICONINFO));
						NativeMethods.SHGetStockIconInfo(NativeMethods.SHSTOCKICONID.SIID_INFO, NativeMethods.SHGSI.SHGSI_ICON, ref shStockIconInfo);
						retval = Icon.FromHandle(shStockIconInfo.hIcon).ToBitmap();
					}
					break;
				case MessageBoxIconEx.Question:
					{
						var shStockIconInfo = new NativeMethods.SHSTOCKICONINFO();
						shStockIconInfo.cbSize = (UInt32)Marshal.SizeOf(typeof(NativeMethods.SHSTOCKICONINFO));
						NativeMethods.SHGetStockIconInfo(NativeMethods.SHSTOCKICONID.SIID_HELP, NativeMethods.SHGSI.SHGSI_ICON, ref shStockIconInfo);
						retval = Icon.FromHandle(shStockIconInfo.hIcon).ToBitmap();
					}
					break;
				case MessageBoxIconEx.Disconnected:
					{
						var shStockIconInfo = new NativeMethods.SHSTOCKICONINFO();
						shStockIconInfo.cbSize = (UInt32)Marshal.SizeOf(typeof(NativeMethods.SHSTOCKICONINFO));
						NativeMethods.SHGetStockIconInfo(NativeMethods.SHSTOCKICONID.SIID_NETWORKCONNECT, NativeMethods.SHGSI.SHGSI_ICON, ref shStockIconInfo);
						retval = Icon.FromHandle(shStockIconInfo.hIcon).ToBitmap();
					}
					break;
			}
			return retval;
		}

		public QuestionForm(string message, string caption, MessageBoxButtons buttons, MessageBoxIconEx icon, bool showDownShowAgain, MessageBoxDefaultButton defaultButton = MessageBoxDefaultButton.Button1, string button1Text = "", string button2Text = "", string button3Text = "", int button1Width = -1, int button2Width = -1, int button3Width = -1, MessageBoxDefaultButton selfAnswerButton = MessageBoxDefaultButton.Button1, string dontShowAgainText = "", int width = 470, int height = 235, Bitmap customIcon = null)
		{
			InitializeComponent();
			this.Width = width;
			this.Height = height;
			//TODO: Autosize form
			Size size = TextRenderer.MeasureText(message, Question_radLabel.Font);
			if (Question_radLabel.Height < size.Height)
			{
				while (Question_radLabel.Height < size.Height)
				{
					this.Height += 10;
					if (this.Height > Screen.PrimaryScreen.Bounds.Height / 2)
					{
						//Limit text height to 1/2 of primary screen
						break;
					}
				}
			}
			_defaultButton = defaultButton;
			_selfAnswerButton = selfAnswerButton;
			//Avoid rebuild in resources. Just for help translation
			this.Text = caption;
			Question_radLabel.Text = message;
			if (!String.IsNullOrEmpty(dontShowAgainText)) { DontShowAgain_radCheckBox.Text = dontShowAgainText; }
			if (customIcon != null)
			{
				Dialog_pictureBox.Image = customIcon;
			}
			else
			{
				Dialog_pictureBox.Image = GetIconImage(icon);
			}
			switch (buttons)
			{
				case MessageBoxButtons.AbortRetryIgnore:
					Yes_button.Text = String.IsNullOrEmpty(button1Text) ? "Abort" : button1Text;
					Yes_button.DialogResult = DialogResult.Abort;
					Yes_button.Visible = true;

					No_button.Text = String.IsNullOrEmpty(button2Text) ?  "Retry" : button2Text;
					No_button.DialogResult = DialogResult.Retry;
					No_button.Visible = true;

					Cancel_button.Text = String.IsNullOrEmpty(button3Text) ? "Ignore" : button3Text;
					Cancel_button.DialogResult = DialogResult.Ignore;
					Cancel_button.Visible = true;
					this.CancelButton = Cancel_button;
					switch (_selfAnswerButton)
					{
						case MessageBoxDefaultButton.Button1:
							_currentSelfAnswerButton = Yes_button;
							break;
						case MessageBoxDefaultButton.Button2:
							_currentSelfAnswerButton = No_button;
							break;
						case MessageBoxDefaultButton.Button3:
							_currentSelfAnswerButton = Cancel_button;
							break;
						default:
							_currentSelfAnswerButton = Yes_button;
							break;
					}
					break;
				case MessageBoxButtons.OK:
					Yes_button.Text = String.IsNullOrEmpty(button1Text) ? "OK" : button1Text;
					Yes_button.DialogResult = DialogResult.OK;
					Yes_button.Visible = true;
					this.CancelButton = Yes_button;

					No_button.Text = "Retry";
					No_button.DialogResult = DialogResult.Retry;
					No_button.Visible = false;

					Cancel_button.Text = "Ignore";
					Cancel_button.DialogResult = DialogResult.Ignore;
					Cancel_button.Visible = false;

					_currentSelfAnswerButton = Yes_button;
					break;
				case MessageBoxButtons.OKCancel:
					Yes_button.Text = String.IsNullOrEmpty(button1Text) ? "OK" : button1Text;
					Yes_button.DialogResult = DialogResult.OK;
					Yes_button.Visible = true;

					No_button.Text = String.IsNullOrEmpty(button2Text) ? "Cancel" : button2Text;
					No_button.DialogResult = DialogResult.Cancel;
					No_button.Visible = true;
					this.CancelButton = No_button;

					Cancel_button.Text = "Ignore";
					Cancel_button.DialogResult = DialogResult.Ignore;
					Cancel_button.Visible = false;

					switch (_selfAnswerButton)
					{
						case MessageBoxDefaultButton.Button1:
							_currentSelfAnswerButton = Yes_button;
							break;
						case MessageBoxDefaultButton.Button2:
							_currentSelfAnswerButton = No_button;
							break;
						default:
							_currentSelfAnswerButton = Yes_button;
							break;
					}
					break;
				case MessageBoxButtons.RetryCancel:
					Yes_button.Text = String.IsNullOrEmpty(button1Text) ? "Retry" : button1Text;
					Yes_button.DialogResult = DialogResult.Retry;
					Yes_button.Visible = true;

					No_button.Text = String.IsNullOrEmpty(button2Text) ? "Cancel" : button2Text;
					No_button.DialogResult = DialogResult.Cancel;
					No_button.Visible = true;
					this.CancelButton = No_button;

					Cancel_button.Text = "Ignore";
					Cancel_button.DialogResult = DialogResult.Ignore;
					Cancel_button.Visible = false;

					switch (_selfAnswerButton)
					{
						case MessageBoxDefaultButton.Button1:
							_currentSelfAnswerButton = Yes_button;
							break;
						case MessageBoxDefaultButton.Button2:
							_currentSelfAnswerButton = No_button;
							break;
						default:
							_currentSelfAnswerButton = Yes_button;
							break;
					}
					break;
				case MessageBoxButtons.YesNo:
					Yes_button.Text = String.IsNullOrEmpty(button1Text) ? "Yes" : button1Text;
					Yes_button.DialogResult = DialogResult.Yes;
					Yes_button.Visible = true;
					No_button.Text = String.IsNullOrEmpty(button2Text) ? "No" : button2Text;
					No_button.DialogResult = DialogResult.No;
					No_button.Visible = true;
					this.CancelButton = No_button;

					Cancel_button.Text = "Ignore";
					Cancel_button.DialogResult = DialogResult.Ignore;
					Cancel_button.Visible = false;

					switch (_selfAnswerButton)
					{
						case MessageBoxDefaultButton.Button1:
							_currentSelfAnswerButton = Yes_button;
							break;
						case MessageBoxDefaultButton.Button2:
							_currentSelfAnswerButton = No_button;
							break;
						default:
							_currentSelfAnswerButton = Yes_button;
							break;
					}
					break;

				case MessageBoxButtons.YesNoCancel:
					Yes_button.Text = String.IsNullOrEmpty(button1Text) ? "Yes" : button1Text;
					Yes_button.DialogResult = DialogResult.Yes;
					Yes_button.Visible = true;

					No_button.Text = String.IsNullOrEmpty(button2Text) ? "No" : button2Text;
					No_button.DialogResult = DialogResult.No;
					No_button.Visible = true;

					Cancel_button.Text = String.IsNullOrEmpty(button3Text) ? "Cancel" : button3Text;
					Cancel_button.DialogResult = DialogResult.Cancel;
					Cancel_button.Visible = true;
					this.CancelButton = Cancel_button;

					switch (_selfAnswerButton)
					{
						case MessageBoxDefaultButton.Button1:
							_currentSelfAnswerButton = Yes_button;
							break;
						case MessageBoxDefaultButton.Button2:
							_currentSelfAnswerButton = No_button;
							break;
						case MessageBoxDefaultButton.Button3:
							_currentSelfAnswerButton = Cancel_button;
							break;
						default:
							_currentSelfAnswerButton = Yes_button;
							break;
					}

					break;
			}
			DontShowAgain_radCheckBox.Visible = showDownShowAgain;
			if (button1Width > -1)
			{
				Yes_button.Width = button1Width;
			}
			if (button2Width > -1)
			{
				No_button.Width = button2Width;
			}
			if (button3Width > -1)
			{
				Cancel_button.Width = button3Width;
			}
		}


		private void QuestionForm_Shown(object sender, EventArgs e)
		{
			switch (_defaultButton)
			{
				case MessageBoxDefaultButton.Button1:
					if (Yes_button.Visible)
					{
						this.AcceptButton = Yes_button;
						Yes_button.Focus();
					}
					break;
				case MessageBoxDefaultButton.Button2:
					if (No_button.Visible)
					{
						this.AcceptButton = No_button;
						No_button.Focus();
					}
					break;
				case MessageBoxDefaultButton.Button3:
					if (Cancel_button.Visible)
					{
						this.AcceptButton = Cancel_button;
						Cancel_button.Focus();
					}
					break;
			}
		}

		private void QuestionForm_Load(object sender, EventArgs e)
		{
			
		}

	}
}
