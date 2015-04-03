using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Providers
{
	/// <summary>
	/// Converter that can convert a string to a <see cref="UtcDateTime"/>.
	/// </summary>
	public class UtcDateTimeTypeConverter : TypeConverter
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
				UtcDateTime utcDateTime;
				if (UtcDateTime.TryParse(s, out utcDateTime))
					return utcDateTime;

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
			if (!(value is UtcDateTime))
				throw new ArgumentException("Is not of type UtcDateTime", "value");

			var date = (UtcDateTime)value;

			if (destinationType == typeof(string))
				return InvariantFormat.FormatUtcRoundTrip(date.ToDateTime());

			return base.ConvertTo(context, culture, value, destinationType);
		}
	}

}
