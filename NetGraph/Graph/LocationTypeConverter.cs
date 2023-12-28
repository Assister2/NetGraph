using System;
using System.ComponentModel;

namespace CyConex.Graph
{
    internal class LocationTypeConverter : TypeConverter
	{
		public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType == typeof(string))
			{
				return FormattableString.Invariant($"X={((Location)value).X}, Y={((Location)value).Y}");
			}
			return base.ConvertTo(context, culture, value, destinationType);
		}

		public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
		{

			return TypeDescriptor.GetProperties(typeof(Location), attributes).Sort(new string[] { "X", "Y" });

		}

		public override bool GetPropertiesSupported(ITypeDescriptorContext context)
		{
			return true;
		}
	}
}
