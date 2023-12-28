using System;
using System.ComponentModel;
using System.Globalization;
using System.Linq;

namespace CyConex.Graph
{
    public class FormattedDoubleConverter : TypeConverter
	{
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return sourceType == typeof(string) || sourceType == typeof(double);
		}

		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			return destinationType == typeof(string) || destinationType == typeof(double);
		}

		public override object ConvertFrom(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value)
		{
			if (value is double)
			{
				return value;
			}
			if (value is string str)
			{
				return double.Parse(str, CultureInfo.InvariantCulture);
			}
			return null;
		}

		public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType != typeof(string))
			{
				return null;
			}

			if (value is double @double)
			{
				var property = context.PropertyDescriptor;
				if (property != null)
				{
					// Analyze the property for a second attribute that gives the format string
					var formatStrAttr = property.Attributes.OfType<FormattedDoubleFormatString>().FirstOrDefault();
					if (formatStrAttr != null)
					{
						return @double.ToString(formatStrAttr.FormatString, CultureInfo.InvariantCulture);
					}
					else
					{
						return @double.ToString(CultureInfo.InvariantCulture);
					}
				}
			}

			return null;
		}
	}

	[AttributeUsage(AttributeTargets.Property)]
	class FormattedDoubleFormatString : Attribute
	{
		public string FormatString { get; private set; }

		public FormattedDoubleFormatString(string formatString)
		{
			FormatString = formatString;
		}
	}
}
