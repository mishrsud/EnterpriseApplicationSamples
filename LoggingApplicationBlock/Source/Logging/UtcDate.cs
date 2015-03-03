using System;
using System.ComponentModel;
using System.Globalization;

namespace Logging
{
	[Serializable, TypeConverter(typeof(UtcDateTypeConverter))]
	public struct UtcDate : IEquatable<UtcDate>, IComparable<UtcDate>, IComparable
	{
		private readonly DateTime _value;

		/// <summary>
		/// Construct a UtcDate from the given date.
		/// 
		/// The given <see cref="DateTime"/> is converted to UTC time if it's not already in UTC time (<see cref="DateTimeKind.Utc"/>).
		/// </summary>
		/// <param name="utcDate">The date. The time of day is ignored.</param>
		public UtcDate(DateTime utcDate)
		{
			_value = utcDate.ToUniversalTime().Date;
		}

		/// <summary>
		/// Construct a UtcDate from the given date.
		/// </summary>
		/// <param name="date">The date. The time of day is ignored.</param>
		public UtcDate(UtcDateTime date)
			: this(date.Year, date.Month, date.Day)
		{
		}

		/// <summary>
		/// Construct an UtcDate. The values given are assumed to be in Utc format already (no conversion done).
		/// 
		/// The given date is assumed to be in UTC time.
		/// </summary>
		/// <param name="year">The year.</param>
		/// <param name="month">The month. 1 is January, 12 is December.</param>
		/// <param name="day">The day of the month.</param>
		public UtcDate(int year, int month, int day)
		{
			_value = new DateTime(year, month, day, 0, 0, 0, DateTimeKind.Utc);
		}

		/// <summary>
		/// GetById the year.
		/// </summary>
		public int Year
		{
			get { return _value.Year; }
		}

		/// <summary>
		/// GetById the month. January is 1, December is 12.
		/// </summary>
		public int Month
		{
			get { return _value.Month; }
		}

		/// <summary>
		/// GetById the day of the month.
		/// </summary>
		public int Day
		{
			get { return _value.Day; }
		}

		/// <summary>
		/// GetById the current UTC date.
		/// </summary>
		public static UtcDate Today
		{
			get { return new UtcDate(UtcDateTime.Now); }
		}

		/// <summary>
		/// Gets the day of the week.
		/// </summary>
		/// <seealso cref="DateTime.DayOfWeek"/>
		public DayOfWeek DayOfWeek
		{
			get { return _value.DayOfWeek; }
		}

		/// <summary>
		/// Gets the day of the year represented by this instance.
		/// </summary>
		/// <seealso cref="DateTime.DayOfYear"/>
		public int DayOfYear
		{
			get { return _value.DayOfYear; }
		}

		#region Operator Overloads

		/// <summary>
		/// Adds a specified time interval to a specified date and time, yielding a new date and time.
		/// </summary>
		/// <param name="utcDate">The date and time value to add.</param>
		/// <param name="timeSpan">The time interval to add.</param>
		/// <returns>An object that is the sum of the values of d and t.</returns>
		public static UtcDate operator +(UtcDate utcDate, TimeSpan timeSpan)
		{
			return new UtcDate(utcDate._value + timeSpan);
		}

		/// <summary>
		/// Adds a specified time interval to a specified date and time, yielding a new date and time.
		/// </summary>
		/// <param name="utcDate">The date and time value to add.</param>
		/// <param name="timeSpan">The time interval to add.</param>
		/// <returns>An object that is the sum of the values of d and t.</returns>
		public static UtcDate Add(UtcDate utcDate, TimeSpan timeSpan)
		{
			return utcDate + timeSpan;
		}

		/// <summary>
		/// Determines whether two specified instances of <see cref="UtcDateTime"/> are equal.
		/// </summary>
		/// <param name="utcDate1">The first object to compare.</param>
		/// <param name="utcDate2">The second object to compare.</param>
		/// <returns>true if d1 and d2 represent the same date and time; otherwise, false.</returns>
		public static bool operator ==(UtcDate utcDate1, UtcDate utcDate2)
		{
			return (utcDate1._value == utcDate2._value);
		}

		/// <summary>
		/// Determines whether the <see cref="DateTime"/> instance are equal to a <see cref="UtcDateTime"/> instance.
		/// </summary>
		/// <param name="dateTime">The first object to compare.</param>
		/// <param name="utcDate">The second object to compare.</param>
		/// <returns>true if d1 and d2 represent the same date and time; otherwise, false.</returns>
		public static bool operator ==(DateTime dateTime, UtcDate utcDate)
		{
			return (dateTime == utcDate._value);
		}

		/// <summary>
		/// Determines whether the <see cref="UtcDateTime"/> instance are equal to a <see cref="DateTime"/> instance.
		/// </summary>
		/// <param name="utcDateTime">The first object to compare.</param>
		/// <param name="dateTime">The second object to compare.</param>
		/// <returns>true if d1 and d2 represent the same date and time; otherwise, false.</returns>
		public static bool operator ==(UtcDate utcDateTime, DateTime dateTime)
		{
			return (utcDateTime._value == dateTime);
		}

		/// <summary>
		/// Determines whether one specified <see cref="UtcDateTime"/> is greater than another specified <see cref="UtcDateTime"/>.
		/// </summary>
		/// <param name="utcDate1">The first object to compare.</param>
		/// <param name="utcDate">The second object to compare.</param>
		/// <returns>true if t1 is greater than t2; otherwise, false.</returns>
		public static bool operator >(UtcDate utcDate1, UtcDate utcDate)
		{
			return (utcDate1._value > utcDate._value);
		}

		/// <summary>
		/// Determines whether one specified <see cref="DateTime"/> is greater than another specified <see cref="UtcDateTime"/>.
		/// </summary>
		/// <param name="dateTime">The first object to compare.</param>
		/// <param name="utcDate">The second object to compare.</param>
		/// <returns>true if t1 is greater than t2; otherwise, false.</returns>
		public static bool operator >(DateTime dateTime, UtcDate utcDate)
		{
			return (dateTime > utcDate._value);
		}

		/// <summary>
		/// Determines whether one specified <see cref="UtcDateTime"/> is greater than another specified <see cref="DateTime"/>.
		/// </summary>
		/// <param name="utcDate">The first object to compare.</param>
		/// <param name="dateTime">The second object to compare.</param>
		/// <returns>true if t1 is greater than t2; otherwise, false.</returns>
		public static bool operator >(UtcDate utcDate, DateTime dateTime)
		{
			return (utcDate._value > dateTime);
		}

		/// <summary>
		/// Determines whether one specified <see cref="UtcDateTime"/> is greater than or equal to another specified <see cref="UtcDateTime"/>.
		/// </summary>
		/// <param name="utcDate1">The first object to compare.</param>
		/// <param name="utcDate2">The second object to compare.</param>
		/// <returns>true if t1 is greater than or equal to t2; otherwise, false.</returns>
		public static bool operator >=(UtcDate utcDate1, UtcDate utcDate2)
		{
			return (utcDate1._value >= utcDate2._value);
		}

		/// <summary>
		/// Determines whether one specified <see cref="DateTime"/> is greater than or equal to another specified <see cref="UtcDateTime"/>.
		/// </summary>
		/// <param name="dateTime">The first object to compare.</param>
		/// <param name="utcDateTime">The second object to compare.</param>
		/// <returns>true if t1 is greater than or equal to t2; otherwise, false.</returns>
		public static bool operator >=(DateTime dateTime, UtcDate utcDateTime)
		{
			return (dateTime >= utcDateTime._value);
		}

		/// <summary>
		/// Determines whether one specified <see cref="UtcDateTime"/> is greater than or equal to another specified <see cref="DateTime"/>.
		/// </summary>
		/// <param name="utcDate">The first object to compare.</param>
		/// <param name="date">The second object to compare.</param>
		/// <returns>true if t1 is greater than or equal to t2; otherwise, false.</returns>
		public static bool operator >=(UtcDate utcDate, DateTime date)
		{
			return (utcDate._value >= date);
		}

		/// <summary>
		/// Determines whether two specified instances of <see cref="UtcDateTime"/> are not equal.
		/// </summary>
		/// <param name="utcDate1">The first object to compare.</param>
		/// <param name="utcDate2">The second object to compare.</param>
		/// <returns>true if d1 and d2 do not represent the same date and time; otherwise, false.</returns>
		public static bool operator !=(UtcDate utcDate1, UtcDate utcDate2)
		{
			return (utcDate1._value != utcDate2._value);
		}

		/// <summary>
		/// Determines whether a <see cref="DateTime"/> instance are not equal to a <see cref="UtcDateTime"/> instance.
		/// </summary>
		/// <param name="dateTime">The first object to compare.</param>
		/// <param name="utcDate">The second object to compare.</param>
		/// <returns>true if d1 and d2 do not represent the same date and time; otherwise, false.</returns>
		public static bool operator !=(DateTime dateTime, UtcDate utcDate)
		{
			return (dateTime != utcDate._value);
		}

		/// <summary>
		/// Determines whether a <see cref="UtcDateTime"/> instance are not equal to a <see cref="DateTime"/> instance.
		/// </summary>
		/// <param name="utcDate">The first object to compare.</param>
		/// <param name="dateTime">The second object to compare.</param>
		/// <returns>true if d1 and d2 do not represent the same date and time; otherwise, false.</returns>
		public static bool operator !=(UtcDate utcDate, DateTime dateTime)
		{
			return (utcDate._value != dateTime);
		}

		/// <summary>
		/// Determines whether one specified <see cref="UtcDateTime"/> is less than another specified <see cref="UtcDateTime"/>.
		/// </summary>
		/// <param name="utcDate1">The first object to compare.</param>
		/// <param name="utcDate2">The second object to compare.</param>
		/// <returns>true if d1 is less than d; otherwise, false.</returns>
		public static bool operator <(UtcDate utcDate1, UtcDate utcDate2)
		{
			return (utcDate1._value < utcDate2._value);
		}

		/// <summary>
		/// Determines whether one specified <see cref="DateTime"/> is less than another specified <see cref="UtcDateTime"/>.
		/// </summary>
		/// <param name="dateTime">The first object to compare.</param>
		/// <param name="utcDate">The second object to compare.</param>
		/// <returns>true if d1 is less than d2; otherwise, false.</returns>
		public static bool operator <(DateTime dateTime, UtcDate utcDate)
		{
			return (dateTime < utcDate._value);
		}

		/// <summary>
		/// Determines whether one specified <see cref="UtcDateTime"/> is less than another specified <see cref="DateTime"/>.
		/// </summary>
		/// <param name="utcDate">The first object to compare.</param>
		/// <param name="dateTime">The second object to compare.</param>
		/// <returns>true if d is less than d2; otherwise, false.</returns>
		public static bool operator <(UtcDate utcDate, DateTime dateTime)
		{
			return (utcDate._value < dateTime);
		}

		/// <summary>
		/// Determines whether one specified <see cref="UtcDateTime"/> is less than or equal to another specified <see cref="UtcDateTime"/>.
		/// </summary>
		/// <param name="utcDate1">The first object to compare.</param>
		/// <param name="utcDate2">The second object to compare.</param>
		/// <returns>true if d1 is less than d2; otherwise, false.</returns>
		public static bool operator <=(UtcDate utcDate1, UtcDate utcDate2)
		{
			return (utcDate1._value <= utcDate2._value);
		}

		/// <summary>
		/// Determines whether one specified <see cref="DateTime"/> is less than or equal to another specified <see cref="UtcDateTime"/>.
		/// </summary>
		/// <param name="dateTime">The first object to compare.</param>
		/// <param name="utcDate">The second object to compare.</param>
		/// <returns>true if d1 is less than d2; otherwise, false.</returns>
		public static bool operator <=(DateTime dateTime, UtcDate utcDate)
		{
			return (dateTime <= utcDate._value);
		}

		/// <summary>
		/// Determines whether one specified <see cref="UtcDateTime"/> is less than or equal to another specified <see cref="DateTime"/>.
		/// </summary>
		/// <param name="utcDateTime">The first object to compare.</param>
		/// <param name="dateTime">The second object to compare.</param>
		/// <returns>true if d1 is less than d2; otherwise, false.</returns>
		public static bool operator <=(UtcDate utcDateTime, DateTime dateTime)
		{
			return (utcDateTime._value <= dateTime);
		}

		#endregion Operator Overloads

		#region IEquatable<UtcDate> Members

		/// <summary>
		/// Indicates whether the current object is equal to another object of the same type.
		/// </summary>
		/// <returns>
		/// true if the current object is equal to the <paramref name="other"/> parameter; otherwise, false.
		/// </returns>
		/// <param name="other">An object to compare with this object.</param>
		public bool Equals(UtcDate other)
		{
			return other._value.Equals(_value);
		}

		#endregion IEquatable<UtcDate> Members

		/// <summary>
		/// Returns a new <see cref="UtcDate"/> that adds the specified number of years to the value of this instance.
		/// </summary>
		/// <param name="years">The number of years to add. Can be negative or positive.</param>
		/// <returns>An object whose value is the sum of the date represented by this instance and the number of years represented by <paramref name="years"/>.</returns>
		public UtcDate AddYears(int years)
		{
			return new UtcDate(_value.AddYears(years));
		}

		/// <summary>
		/// Returns a new <see cref="UtcDate"/> that adds the specified number of months to the value of this instance.
		/// </summary>
		/// <param name="months">The number of months to add. Can be negative or positive.</param>
		/// <returns>An object whose value is the sum of the date represented by this instance and the number of months represented by <paramref name="months"/>.</returns>
		public UtcDate AddMonths(int months)
		{
			return new UtcDate(_value.AddMonths(months));
		}

		/// <summary>
		/// Returns a new <see cref="UtcDate"/> that adds the specified number of days to the value of this instance.
		/// </summary>
		/// <param name="days">The number of whole or fractional days to add. Can be negative or positive.</param>
		/// <returns>An object whose value is the sum of the date represented by this instance and the number of days represented by <paramref name="days"/>.</returns>
		public UtcDate AddDays(double days)
		{
			return new UtcDate(_value.AddDays(days));
		}

		/// <summary>
		/// Indicates whether this instance and a specified object are equal.
		/// </summary>
		/// <returns>
		/// true if <paramref name="obj"/> and this instance are the same type and represent the same value; otherwise, false.
		/// </returns>
		/// <param name="obj">Another object to compare to. </param><filterpriority>2</filterpriority>
		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (obj.GetType() != typeof(UtcDate)) return false;
			return Equals((UtcDate)obj);
		}

		/// <summary>
		/// Returns the hash code for this instance.
		/// </summary>
		/// <returns>
		/// A 32-bit signed integer that is the hash code for this instance.
		/// </returns>
		/// <filterpriority>2</filterpriority>
		public override int GetHashCode()
		{
			return _value.GetHashCode();
		}

		/// <summary>
		/// Convert this to a <see cref="System.DateTime"/>.
		/// </summary>
		/// <returns>The converted representation.</returns>
		public DateTime ToDateTime()
		{
			return new DateTime(Year, Month, Day, 0, 0, 0, DateTimeKind.Utc);
		}

		/// <summary>
		/// Convert this to a <see cref="UtcDateTime"/>.
		/// </summary>
		/// <returns>The converted representation.</returns>
		public UtcDateTime ToUtcDateTime()
		{
			return new UtcDateTime(Year, Month, Day);
		}

		/// <summary>
		/// Try to parse a string into a <see cref="UtcDate"/>.
		/// </summary>
		/// <param name="value">The string to parse.</param>
		/// <param name="result">the result</param>
		/// <returns>true if and only if the parsing succeeded.</returns>
		/// <remarks>
		/// The accepted format of <paramref name="value"/> is the ISO 8601 extended format, "yyyy-MM-dd".
		/// Whitespace at beginning or end will be trimmed away before parsing (and hence will have no effect).
		/// </remarks>
		public static bool TryParse(string value, out UtcDate result)
		{
			bool parsed = false;
			var time = new DateTime();
			if (value != null)
			{
				value = value.Trim();
				parsed = DateTime.TryParseExact(value, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal,
												out time);
			}
			result = parsed ? new UtcDate(time) : new UtcDate();
			return parsed;
		}

		/// <summary>
		/// Parse a string into a <see cref="UtcDate"/>.
		/// </summary>
		/// <param name="value">The string to parse.</param>
		/// <returns>Parsed <see cref="UtcDate"/> object.</returns>
		/// <exception cref="ArgumentNullException">Thrown if <paramref name="value"/> is null.</exception>
		/// <exception cref="ArgumentOutOfRangeException">Thrown if <paramref name="value"/> is not in the accepted format (see <see cref="UtcDate.TryParse"/>).</exception>
		public static UtcDate Parse(string value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}

			UtcDate parsedDate;
			if (TryParse(value, out parsedDate))
			{
				return parsedDate;
			}
			throw new ArgumentOutOfRangeException("value", value, "Could not parse value as ISO 8601 extended date format.");
		}

		/// <summary>
		/// Converts the value of the current <see cref="UtcDate"/> object to its equivalent string representation.
		/// </summary>
		/// <returns>
		/// A string representation of the value of the current <see cref="UtcDate"/> object.
		/// </returns>
		/// <seealso cref="DateTime.ToString()"/>
		public override string ToString()
		{
			return _value.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
		}

		#region IComparable, IComparable<UTCDateTime>

		#region IComparable Members

		/// <summary>
		/// Compares the current instance with another object of the same type and returns an integer that indicates whether the current instance precedes, follows, or occurs in the same position in the sort order as the other object.
		/// </summary>
		/// <returns>
		/// A value that indicates the relative order of the objects being compared.
		/// The return value has these meanings: Value Meaning Less than zero This instance is less than <paramref name="obj"/>. Zero This instance is equal to <paramref name="obj"/>. Greater than zero This instance is greater than <paramref name="obj"/>.
		/// </returns>
		/// <param name="obj">An object to compare with this instance. </param><exception cref="T:System.ArgumentException"><paramref name="obj"/> is not the same type as this instance. </exception><filterpriority>2</filterpriority>
		public int CompareTo(object obj)
		{
			if (obj == null)
			{
				//Deals with object of other(heterogenous) types
				return 1;
			}

			if (!(obj is UtcDate))
			{
				throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "Argument type should be {0}", GetType()));
			}

			return CompareTo((UtcDate)obj);
		}

		#endregion IComparable Members

		#region IComparable<UtcDate> Members

		/// <summary>
		/// Compares the current object with another object of the same type.
		/// </summary>
		/// <returns>
		/// A value that indicates the relative order of the objects being compared.
		/// The return value has the following meanings: Value Meaning Less than zero This object is less than the <paramref name="other"/> parameter.Zero This object is equal to <paramref name="other"/>. Greater than zero This object is greater than <paramref name="other"/>.
		/// </returns>
		/// <param name="other">An object to compare with this object.</param>
		public int CompareTo(UtcDate other)
		{
			return _value.CompareTo(other._value);
		}

		#endregion IComparable<UtcDate> Members

		#endregion IComparable, IComparable<UTCDateTime>
	}
}
