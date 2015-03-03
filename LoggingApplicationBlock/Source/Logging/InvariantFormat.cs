using System;
using System.Globalization;

namespace Logging
{
	/// <summary>
	/// Optimized methods for formatting values - to be used instead of built in FCL formatting methods (e.g. DateTime.ToString() when the 
	/// format is statically known and the culture is invariant
	/// </summary>
	internal static class InvariantFormat
	{
		/// <summary>
		/// Formats a UTC <see cref="DateTime"/> according to the O format under the <see cref="CultureInfo.InvariantCulture"/>.
		/// </summary>
		/// <param name="v">The <see cref="DateTime"/> to format.</param>
		/// <returns>The same as <c>v.ToString("o", CultureInfo.InvariantCulture)</c>.</returns>
		public static string FormatUtcRoundTrip(DateTime v)
		{
			if (v.Kind != DateTimeKind.Utc) throw new ArgumentException("The DateTime must be UTC", "v");

			// The format is fixed according to this:
			//"yyyy'-'MM'-'dd'T'HH':'mm':'ss.fffffffK"

			var s = new char[28];
			var i = 0;

			// Append year
			var y = v.Year;
			s[i++] = (char)('0' + y / 1000);
			y = y % 1000;
			s[i++] = (char)('0' + y / 100);
			y = y % 100;
			s[i++] = (char)('0' + y / 10);
			y = y % 10;
			s[i++] = (char)('0' + y);

			s[i++] = '-';

			// Append month
			var m = v.Month;
			s[i++] = (char)('0' + m / 10);
			m = m % 10;
			s[i++] = (char)('0' + m);

			s[i++] = '-';

			// Append day
			var d = v.Day;
			s[i++] = (char)('0' + d / 10);
			d = d % 10;
			s[i++] = (char)('0' + d);

			s[i++] = 'T';

			// Append hour
			var h = v.Hour;
			s[i++] = (char)('0' + h / 10);
			h = h % 10;
			s[i++] = (char)('0' + h);

			s[i++] = ':';

			// Append minutes
			var mi = v.Minute;
			s[i++] = (char)('0' + mi / 10);
			mi = mi % 10;
			s[i++] = (char)('0' + mi);

			s[i++] = ':';

			// Append seconds
			var ss = v.Second;
			s[i++] = (char)('0' + ss / 10);
			ss = ss % 10;
			s[i++] = (char)('0' + ss);

			s[i++] = '.';

			// Append 7-digit fraction
			var f = (int)(v.Ticks % (10000 * 1000));
			s[i++] = (char)('0' + f / 1000000);
			f = f % 1000000;
			s[i++] = (char)('0' + f / 100000);
			f = f % 100000;
			s[i++] = (char)('0' + f / 10000);
			f = f % 10000;
			s[i++] = (char)('0' + f / 1000);
			f = f % 1000;
			s[i++] = (char)('0' + f / 100);
			f = f % 100;
			s[i++] = (char)('0' + f / 10);
			f = f % 10;
			s[i++] = (char)('0' + f);

			// Append time-zone
			s[i] = ('Z');

			return new string(s);
		}

	}
}
