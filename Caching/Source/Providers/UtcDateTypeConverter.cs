using System;
using System.ComponentModel;
using System.Globalization;

namespace Providers
{
	/// <summary>
	/// Converter from string to a UtcDate
	/// </summary>
	public class UtcDateTypeConverter : TypeConverter
	{
		/// <see cref="TypeConverter.CanConvertFrom(ITypeDescriptorContext,System.Type)"/>
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
		}

		/// <see cref="TypeConverter.ConvertFrom(ITypeDescriptorContext,CultureInfo,object)"/>
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			var s = value as string;
			if (s != null)
			{
				UtcDate utcDate;
				if (UtcDate.TryParse(s, out utcDate))
					return utcDate;

				throw new FormatException(string.Format(CultureInfo.InvariantCulture, "Invalid format '{0}' for UtcDate.", value));
			}

			return base.ConvertFrom(context, culture, value);
		}

		/// <see cref="TypeConverter.CanConvertTo(System.Type)"/>
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			return destinationType == typeof(string) || base.CanConvertTo(context, destinationType);
		}

		/// <see cref="TypeConverter.ConvertTo(object,System.Type)"/>
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (!(value is UtcDate))
				throw new ArgumentException("Is not of type UtcDate", "value");

			var date = (UtcDate)value;

			if (destinationType == typeof(string))
				return date.ToDateTime().ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);

			return base.ConvertTo(context, culture, value, destinationType);
		}
	}

}
