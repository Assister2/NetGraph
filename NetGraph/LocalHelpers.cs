using System;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Text;

namespace CyConex
{
    internal static class LocalHelpers
	{
		internal static string ReadNoGraphPage()
		{
			string mapForLoad = $"CyConex.HTML.nointernet.html";
			string retval = String.Empty;
			using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(mapForLoad))
			{
				Byte[] pageData = new Byte[stream.Length];
				stream.Read(pageData, 0, pageData.Length);
				retval = Encoding.UTF8.GetString(pageData);
			}
			return retval;
		}

		internal static string ReadGraphPage()
		{
			string mapForLoad = "CyConex.HTML.data.html";
			string retval = String.Empty;
			using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(mapForLoad))
			{
				Byte[] pageData = new Byte[stream.Length];
				stream.Read(pageData, 0, pageData.Length);
				retval = Encoding.UTF8.GetString(pageData);
			}
			return retval;
		}

		internal static byte[] ReadResourceBytes(string name)
		{
			byte[] pageData = null;
			using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(name))
			{
				pageData = new Byte[stream.Length];
				stream.Read(pageData, 0, pageData.Length);
				
			}
			return pageData;
		}

		internal static string ReadResource(string name)
		{
			try
			{
				string retval = String.Empty;
				using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(name))
				{
					Byte[] pageData = new Byte[stream.Length];
					stream.Read(pageData, 0, pageData.Length);
					retval = Encoding.UTF8.GetString(pageData);
				}
				return retval;
			}
			catch
			{ return String.Empty; }
			
		}

		internal static string ToInvariant(this double value)
		{
			return value.ToString(CultureInfo.InvariantCulture);
		}

		internal static double FromInvariant(this string value)
		{
			return Double.Parse(value, CultureInfo.InvariantCulture);
		}

	}
}
