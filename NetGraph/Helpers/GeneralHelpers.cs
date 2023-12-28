using Microsoft.Win32;
using System;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CyConex.Helpers
{
    public static class GeneralHelpers
	{
		private static readonly string _true = "true";
		private static readonly string _false = "false";

		/// <summary>
		/// Invoke action on control if invoke required
		/// </summary>
		/// <param name="control">Control for invoke</param>
		/// <param name="action">Action for invoke</param>
		public static void InvokeIfNeed(this System.Windows.Forms.Control control, Action action)
		{
			if (control == null || control.Disposing || control.IsDisposed)
			{
				return;
			}

			if (control.InvokeRequired)
			{
				try
				{
					control.Invoke(action);
				}
				catch
				{
                    Console.WriteLine("EXEPTION caught at GenaralHelpers.cs > InvokeIfNeed");
                }
			}
			else
			{
				action.Invoke();
			}
		}

		public static void SetBorderColor(this PropertyGrid grid, bool showBorder)
		{
			if (showBorder)
			{
				//Set Default ViewBackColor
				PropertyInfo viewBackColor = grid.GetType().GetProperty("ViewBorderColor");
				if (viewBackColor != null)
				{
					viewBackColor.SetValue(grid, System.Drawing.SystemColors.ControlDark, null);
				}
				//Set Default HelpBorderColor
				PropertyInfo helpBorderColor = grid.GetType().GetProperty("HelpBorderColor");
				if (helpBorderColor != null)
				{
					helpBorderColor.SetValue(grid, System.Drawing.SystemColors.ControlDark, null);
				}
			}
			else
			{
				//Set ViewBackColor
				PropertyInfo viewBackColor = grid.GetType().GetProperty("ViewBorderColor");
				if (viewBackColor != null)
				{
					viewBackColor.SetValue(grid, System.Drawing.SystemColors.Window, null);
				}
				//Set HelpBorderColor
				PropertyInfo helpBorderColor = grid.GetType().GetProperty("HelpBorderColor");
				if (helpBorderColor != null)
				{
					helpBorderColor.SetValue(grid, System.Drawing.SystemColors.Window, null);
				}
			}
		}

		public static string ToJavaBool(this bool value)
		{
			if (value)
			{ 
				return _true;
			}
			return _false;
		}

		public static Color GetColorizationColor()
		{
			Color retval = SystemColors.GradientActiveCaption;
			using (RegistryKey registryDWMKey = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\DWM", true))
			{
				if (registryDWMKey != null)
				{
					if (registryDWMKey.GetValueNames().Any(item => item == "ColorizationColor"))
					{

						object colorValue = registryDWMKey.GetValue("ColorizationColor");
						var colorARGB = ParseDWordColor((Int32)colorValue);
						retval = Color.FromArgb(colorARGB.a, colorARGB.r, colorARGB.g, colorARGB.b);
					}
				}
			}
			return retval;
		}

		/// <summary>
		/// Double has value
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
		public static bool HasValue(this double value)
		{
			return !Double.IsNaN(value) && !Double.IsInfinity(value) && !Double.IsPositiveInfinity(value) && !Double.IsNegativeInfinity(value);
		}

		/// <summary>
		/// Convert color from HTML string
		/// </summary>
		/// <param name="color"></param>
		/// <returns></returns>
		public static Color ConvertColorFromHTML(string color)
		{
			System.Drawing.ColorConverter colorConverter = new ColorConverter();
			if (color.StartsWith("#"))
			{
				Color _color = (Color)colorConverter.ConvertFromString(color);
				return System.Drawing.Color.FromArgb(_color.A, _color.R, _color.G, _color.B);
			}
			else
			{
				
				if (color.StartsWith("rgba"))
				{
					string colorStr = color.Replace("rgba(", String.Empty).Replace(")", String.Empty).Replace(" ", String.Empty);
					string[] rgb = colorStr.Split(',');
					int r = Int32.Parse(rgb[0]);
					int g = Int32.Parse(rgb[1]);
					int b = Int32.Parse(rgb[2]);
					int a = Int32.Parse(rgb[3]);
					Color _color = Color.FromArgb(r, g, b, a);
					return _color;

				}
				if (color.StartsWith("rgb"))
				{
					string colorStr = color.Replace("rgb(", String.Empty).Replace(")", String.Empty).Replace(" ", String.Empty);
					string[] rgb = colorStr.Split(',');
					int r = Int32.Parse(rgb[0]);
					int g = Int32.Parse(rgb[1]);
					int b = Int32.Parse(rgb[2]);
					Color _color = Color.FromArgb(r, g, b);
					return _color;

				}

				else
				{
					Color _color = System.Drawing.Color.FromName(color);
					return _color;
				}
			}
		}

		/// <summary>
		/// Convert color to valid HTML string with # or name
		/// </summary>
		/// <param name="color"></param>
		/// <returns></returns>
		public static string ConvertColorToHTML(Color color)
		{
			System.Drawing.Color _color = color;
			string htmlColor = System.Drawing.ColorTranslator.ToHtml(_color);
			if (!htmlColor.StartsWith("#"))
			{
				if (htmlColor != _color.Name)
				{
					//This is situation like LightGrey vs LightGray
					htmlColor = _color.Name;
				}
			}
			if (color.IsSystemColor)
			{
				//htmlColor = "#" + _color.A.ToString("X2") + _color.R.ToString("X2") + _color.G.ToString("X2") + _color.B.ToString("X2");
				htmlColor = "#" + _color.R.ToString("X2") + _color.G.ToString("X2") + _color.B.ToString("X2");
			}
			return htmlColor;
		}

		/// <summary>
		/// Awaitable delay
		/// </summary>
		/// <param name="delay"></param>
		/// <returns></returns>
		public static async Task TaskDelay(int delay)
		{
			await Task.Delay(delay);
		}


		private static (Byte r, Byte g, Byte b, Byte a) ParseDWordColor(Int32 color)
		{

			Byte a = (byte)((color >> 24) & 0xFF);
			Byte r = (byte)((color >> 16) & 0xFF);
			Byte g = (byte)((color >> 8) & 0xFF);
			Byte b = (byte)((color >> 0) & 0xFF);
			return (r, g, b, a);
		}

		public static void ParsePositions(string data)
		{

		}

		public static double ParseDoubleInvariant(this string data)
		{
			return Double.Parse(data, CultureInfo.InvariantCulture);
		}

	}
}