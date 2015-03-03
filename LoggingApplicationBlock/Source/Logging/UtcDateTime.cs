using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.Serialization;

namespace Logging
{
	[Serializable, TypeConverter(typeof(UtcDateTimeTypeConverter))]
	public struct UtcDateTime : IComparable, IFormattable, IConvertible, ISerializable, IComparable<UtcDateTime>, IEquatable<UtcDateTime>
	{
		private readonly DateTime _dt;

		/// <summary>
		/// Represents the largest possible value of UtcDateTime. This field is read-only.
		/// </summary>
		public static readonly UtcDateTime MaxValue = new UtcDateTime(DateTime.MaxValue);

		/// <summary>
		/// Represents the smallest possible value of UtcDateTime. This field is read-only.
		/// </summary>
		public static readonly UtcDateTime MinValue = new UtcDateTime(DateTime.MinValue);

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="UtcDateTime"/> structure to the UTC time
		/// of a specified <see cref="DateTime"/>.
		/// 
		/// The given <see cref="DateTime"/> is converted to UTC time if it's not already in UTC time (<see cref="DateTimeKind.Utc"/>).
		/// </summary>
		/// <param name="date">
		/// A <see cref="DateTime"/> with the local time.
		/// </param>
		public UtcDateTime(DateTime date)
		{
			_dt = date.ToUniversalTime();			// ToUniversalTime() only convert's if not in UTC time already
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="UtcDateTime"/> structure to the UTC time
		/// of a specified <see cref="UtcDateTime"/>.
		/// </summary>
		/// <param name="date">
		/// A <see cref="UtcDateTime"/> with the local time.
		/// </param>
		public UtcDateTime(UtcDateTime date)
		{
			_dt = date._dt;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="UtcDateTime"/> structure to a specified number of ticks.
		/// </summary>
		/// <param name="ticks">
		/// A date and time expressed in the number of 100-nanosecond intervals that have elapsed
		/// since January 1, 0001 at 00:00:00.000 in the Gregorian calendar.
		/// </param>
		/// <seealso cref="DateTime"/>
		public UtcDateTime(long ticks)
		{
			_dt = new DateTime(ticks, DateTimeKind.Utc);
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="UtcDateTime"/> structure to the specified year, month, and day.
		/// </summary>
		/// <param name="year">The year (1 through 9999).</param>
		/// <param name="month">The month (1 through 12).</param>
		/// <param name="day">The day (1 through the number of days in month).</param>
		/// <exception cref="ArgumentOutOfRangeException">
		/// year is less than 1 or greater than 9999.
		/// -or-
		/// month is less than 1 or greater than 12.
		/// -or-
		/// day is less than 1 or greater than the number of days in month.
		/// 
		/// The given date-time is assumed to be in UTC time.
		/// </exception>
		/// <exception cref="ArgumentException">
		/// The specified parameters evaluate to less than <see cref="MinValue"/> or more than <see cref="MaxValue"/>.
		/// </exception>
		/// <remarks>
		/// This constructor interprets year, month, and day as a year, month, and day in
		/// the Gregorian calendar. To instantiate a <see cref="UtcDateTime"/> value by using the year,
		/// month, and day in another calendar, call the
		/// The time of day for the resulting <see cref="UtcDateTime"/> is midnight (00:00:00).
		/// The <see cref="Kind"/> property is always initialized to <see cref="DateTimeKind.Utc" />.
		/// </remarks>
		public UtcDateTime(int year, int month, int day)
		{
			_dt = new DateTime(year, month, day, 0, 0, 0, DateTimeKind.Utc);
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="UtcDateTime"/> structure to the specified year, month, day, hour, minute, and second.
		/// 
		/// The given date-time is assumed to be in UTC time.
		/// </summary>
		/// <param name="year">The year (1 through 9999).</param>
		/// <param name="month">The month (1 through 12).</param>
		/// <param name="day">The day (1 through the number of days in month).</param>
		/// <param name="hour">The hours (0 through 23).</param>
		/// <param name="minute">The minutes (0 through 59).</param>
		/// <param name="second">The seconds (0 through 59).</param>
		/// <exception cref="ArgumentException">
		/// The specified parameters evaluate to less than <see cref="MinValue"/> or more than <see cref="MaxValue"/>.
		/// </exception>
		/// <exception cref="ArgumentOutOfRangeException">
		/// year is less than 1 or greater than 9999.
		/// -or-
		/// month is less than 1 or greater than 12.
		/// -or-
		/// day is less than 1 or greater than the number of days in month.
		/// -or-
		/// hour is less than 0 or greater than 23.
		/// -or-
		/// minute is less than 0 or greater than 59.
		/// -or-
		/// second is less than 0 or greater than 59.
		/// </exception>
		public UtcDateTime(int year, int month, int day, int hour, int minute, int second)
		{
			_dt = new DateTime(year, month, day, hour, minute, second, DateTimeKind.Utc);
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="UtcDateTime"/> struct. Serialization constructor.
		/// </summary>
		/// <param name="info">The info.</param>
		/// <param name="context">The context.</param>
		private UtcDateTime(SerializationInfo info, StreamingContext context)  // DON'T remove this- serialization depends on this method
		{
			if (info == null) throw new ArgumentNullException("info");

			_dt = info.GetDateTime("dt");
		}
		#endregion Constructors

		#region Properties

		/// <summary>
		/// Gets the date component of this instance.
		/// </summary>
		/// <seealso cref="DateTime.Date"/>
		public UtcDate Date
		{
			[DebuggerStepThrough()]
			get { return new UtcDate(_dt.Date); }
		}

		/// <summary>
		/// Gets the day of the month represented by this instance.
		/// </summary>
		/// <seealso cref="DateTime.Day"/>
		public int Day
		{
			[DebuggerStepThrough()]
			get { return _dt.Day; }
		}

		/// <summary>
		/// Gets the day of the week represented by this instance.
		/// </summary>
		/// <seealso cref="DateTime.DayOfWeek"/>
		public DayOfWeek DayOfWeek
		{
			[DebuggerStepThrough()]
			get { return _dt.DayOfWeek; }
		}

		/// <summary>
		/// Gets the day of the year represented by this instance.
		/// </summary>
		/// <seealso cref="DateTime.DayOfYear"/>
		public int DayOfYear
		{
			[DebuggerStepThrough()]
			get { return _dt.DayOfYear; }
		}

		/// <summary>
		/// Gets the hour component of the date represented by this instance.
		/// </summary>
		/// <seealso cref="DateTime.Hour"/>
		public int Hour
		{
			[DebuggerStepThrough()]
			get { return _dt.Hour; }
		}

		/// <summary>
		/// Gets a value that indicates whether the time represented by this
		/// instance is based on local time, Coordinated Universal Time (UTC), or neither.
		/// </summary>
		/// <remarks>
		/// Will return <see cref="DateTimeKind.Utc"/> unless the instance is not initialized with one of the constructors. In that case a value of <see cref="DateTimeKind.Unspecified"/> is returned.
		/// </remarks>
		/// <seealso cref="DateTime.Kind"/>
		public DateTimeKind Kind
		{
			[DebuggerStepThrough()]
			get { return _dt.Kind; }
		}

		/// <summary>
		/// Gets the milliseconds component of the date represented by this instance.
		/// </summary>
		/// <seealso cref="DateTime.Millisecond"/>
		public int Millisecond
		{
			[DebuggerStepThrough()]
			get { return _dt.Millisecond; }
		}

		/// <summary>
		/// Gets the minute component of the date represented by this instance.
		/// </summary>
		/// <seealso cref="DateTime.Minute"/>
		public int Minute
		{
			[DebuggerStepThrough()]
			get { return _dt.Minute; }
		}

		/// <summary>
		/// Gets the month component of the date represented by this instance.
		/// </summary>
		/// <seealso cref="DateTime.Month"/>
		public int Month
		{
			[DebuggerStepThrough()]
			get { return _dt.Month; }
		}

		/// <summary>
		/// Gets a <see cref="UtcDateTime"/> object that is set to the current date and
		/// time on this computer, expressed as the Coordinated Universal Time (UTC).
		/// </summary>
		/// <seealso cref="DateTime.UtcNow"/>
		public static UtcDateTime Now
		{
			[DebuggerStepThrough()]
			get { return new UtcDateTime(DateTime.UtcNow); }
		}

		/// <summary>
		/// Gets the seconds component of the date represented by this instance.
		/// </summary>
		/// <seealso cref="DateTime.Second"/>
		public int Second
		{
			[DebuggerStepThrough()]
			get { return _dt.Second; }
		}

		/// <summary>
		/// Gets the number of ticks that represent the date and time of this instance.
		/// </summary>
		/// <seealso cref="DateTime.Ticks"/>
		public long Ticks
		{
			[DebuggerStepThrough()]
			get { return _dt.Ticks; }
		}

		/// <summary>
		/// Gets the time of day for this instance.
		/// </summary>
		/// <seealso cref="DateTime.TimeOfDay"/>
		public TimeSpan TimeOfDay
		{
			[DebuggerStepThrough()]
			get { return _dt.TimeOfDay; }
		}

		/// <summary>
		/// Gets the current UTC date.
		/// </summary>
		/// <seealso cref="DateTime.Today"/>
		public static UtcDate Today
		{
			[DebuggerStepThrough()]
			get { return Now.Date; }
		}

		/// <summary>
		/// Gets the year component of the date represented by this instance.
		/// </summary>
		/// <seealso cref="DateTime.Year"/>
		public Int32 Year
		{
			[DebuggerStepThrough()]
			get { return _dt.Year; }
		}

		#endregion Properties

		#region Operator Overloads

		/// <summary>
		/// Adds a specified time interval to a specified date and time, yielding a new date and time.
		/// </summary>
		/// <param name="utcDateTime">The date and time value to add.</param>
		/// <param name="timeSpan">The time interval to add.</param>
		/// <returns>An object that is the sum of the values of d and t.</returns>
		public static UtcDateTime operator +(UtcDateTime utcDateTime, TimeSpan timeSpan)
		{
			return new UtcDateTime(utcDateTime._dt + timeSpan);
		}

		/// <summary>
		/// Determines whether two specified instances of <see cref="UtcDateTime"/> are equal.
		/// </summary>
		/// <param name="utcDateTime1">The first object to compare.</param>
		/// <param name="utcDateTime2">The second object to compare.</param>
		/// <returns>true if d1 and d2 represent the same date and time; otherwise, false.</returns>
		public static bool operator ==(UtcDateTime utcDateTime1, UtcDateTime utcDateTime2)
		{
			return (utcDateTime1._dt == utcDateTime2._dt);
		}

		/// <summary>
		/// Determines whether the <see cref="DateTime"/> instance are equal to a <see cref="UtcDateTime"/> instance.
		/// </summary>
		/// <param name="dateTime">The first object to compare.</param>
		/// <param name="utcDateTime">The second object to compare.</param>
		/// <returns>true if d1 and d2 represent the same date and time; otherwise, false.</returns>
		public static bool operator ==(DateTime dateTime, UtcDateTime utcDateTime)
		{
			return (dateTime == utcDateTime._dt);
		}

		/// <summary>
		/// Determines whether the <see cref="UtcDateTime"/> instance are equal to a <see cref="DateTime"/> instance.
		/// </summary>
		/// <param name="utcDateTime">The first object to compare.</param>
		/// <param name="dateTime">The second object to compare.</param>
		/// <returns>true if d1 and d2 represent the same date and time; otherwise, false.</returns>
		public static bool operator ==(UtcDateTime utcDateTime, DateTime dateTime)
		{
			return (utcDateTime._dt == dateTime);
		}

		/// <summary>
		/// Determines whether one specified <see cref="UtcDateTime"/> is greater than another specified <see cref="UtcDateTime"/>.
		/// </summary>
		/// <param name="utcDateTime1">The first object to compare.</param>
		/// <param name="utcDateTime">The second object to compare.</param>
		/// <returns>true if t1 is greater than t2; otherwise, false.</returns>
		public static bool operator >(UtcDateTime utcDateTime1, UtcDateTime utcDateTime)
		{
			return (utcDateTime1._dt > utcDateTime._dt);
		}

		/// <summary>
		/// Determines whether one specified <see cref="DateTime"/> is greater than another specified <see cref="UtcDateTime"/>.
		/// </summary>
		/// <param name="dateTime">The first object to compare.</param>
		/// <param name="utcDateTime">The second object to compare.</param>
		/// <returns>true if t1 is greater than t2; otherwise, false.</returns>
		public static bool operator >(DateTime dateTime, UtcDateTime utcDateTime)
		{
			return (dateTime > utcDateTime._dt);
		}

		/// <summary>
		/// Determines whether one specified <see cref="UtcDateTime"/> is greater than another specified <see cref="DateTime"/>.
		/// </summary>
		/// <param name="utcDateTime">The first object to compare.</param>
		/// <param name="dateTime">The second object to compare.</param>
		/// <returns>true if t1 is greater than t2; otherwise, false.</returns>
		public static bool operator >(UtcDateTime utcDateTime, DateTime dateTime)
		{
			return (utcDateTime._dt > dateTime);
		}

		/// <summary>
		/// Determines whether one specified <see cref="UtcDateTime"/> is greater than or equal to another specified <see cref="UtcDateTime"/>.
		/// </summary>
		/// <param name="utcDateTime1">The first object to compare.</param>
		/// <param name="utcDateTime2">The second object to compare.</param>
		/// <returns>true if t1 is greater than or equal to t2; otherwise, false.</returns>
		public static bool operator >=(UtcDateTime utcDateTime1, UtcDateTime utcDateTime2)
		{
			return (utcDateTime1._dt >= utcDateTime2._dt);
		}

		/// <summary>
		/// Determines whether one specified <see cref="DateTime"/> is greater than or equal to another specified <see cref="UtcDateTime"/>.
		/// </summary>
		/// <param name="dateTime">The first object to compare.</param>
		/// <param name="utcDateTime">The second object to compare.</param>
		/// <returns>true if t1 is greater than or equal to t2; otherwise, false.</returns>
		public static bool operator >=(DateTime dateTime, UtcDateTime utcDateTime)
		{
			return (dateTime >= utcDateTime._dt);
		}

		/// <summary>
		/// Determines whether one specified <see cref="UtcDateTime"/> is greater than or equal to another specified <see cref="DateTime"/>.
		/// </summary>
		/// <param name="utcDateTime">The first object to compare.</param>
		/// <param name="dateTime">The second object to compare.</param>
		/// <returns>true if t1 is greater than or equal to t2; otherwise, false.</returns>
		public static bool operator >=(UtcDateTime utcDateTime, DateTime dateTime)
		{
			return (utcDateTime._dt >= dateTime);
		}

		/// <summary>
		/// Determines whether two specified instances of <see cref="UtcDateTime"/> are not equal.
		/// </summary>
		/// <param name="utcDateTime1">The first object to compare.</param>
		/// <param name="utcDateTime2">The second object to compare.</param>
		/// <returns>true if d1 and d2 do not represent the same date and time; otherwise, false.</returns>
		public static bool operator !=(UtcDateTime utcDateTime1, UtcDateTime utcDateTime2)
		{
			return (utcDateTime1._dt != utcDateTime2._dt);
		}

		/// <summary>
		/// Determines whether a <see cref="DateTime"/> instance are not equal to a <see cref="UtcDateTime"/> instance.
		/// </summary>
		/// <param name="dateTime">The first object to compare.</param>
		/// <param name="utcDateTime">The second object to compare.</param>
		/// <returns>true if d1 and d2 do not represent the same date and time; otherwise, false.</returns>
		public static bool operator !=(DateTime dateTime, UtcDateTime utcDateTime)
		{
			return (dateTime != utcDateTime._dt);
		}

		/// <summary>
		/// Determines whether a <see cref="UtcDateTime"/> instance are not equal to a <see cref="DateTime"/> instance.
		/// </summary>
		/// <param name="utcDateTime">The first object to compare.</param>
		/// <param name="dateTime">The second object to compare.</param>
		/// <returns>true if d1 and d2 do not represent the same date and time; otherwise, false.</returns>
		public static bool operator !=(UtcDateTime utcDateTime, DateTime dateTime)
		{
			return (utcDateTime._dt != dateTime);
		}

		/// <summary>
		/// Determines whether one specified <see cref="UtcDateTime"/> is less than another specified <see cref="UtcDateTime"/>.
		/// </summary>
		/// <param name="utcDateTime1">The first object to compare.</param>
		/// <param name="utcDateTime2">The second object to compare.</param>
		/// <returns>true if d1 is less than d; otherwise, false.</returns>
		public static bool operator <(UtcDateTime utcDateTime1, UtcDateTime utcDateTime2)
		{
			return (utcDateTime1._dt < utcDateTime2._dt);
		}

		/// <summary>
		/// Determines whether one specified <see cref="DateTime"/> is less than another specified <see cref="UtcDateTime"/>.
		/// </summary>
		/// <param name="dateTime">The first object to compare.</param>
		/// <param name="utcDateTime">The second object to compare.</param>
		/// <returns>true if d1 is less than d2; otherwise, false.</returns>
		public static bool operator <(DateTime dateTime, UtcDateTime utcDateTime)
		{
			return (dateTime < utcDateTime._dt);
		}

		/// <summary>
		/// Determines whether one specified <see cref="UtcDateTime"/> is less than another specified <see cref="DateTime"/>.
		/// </summary>
		/// <param name="utcDateTime">The first object to compare.</param>
		/// <param name="dateTime">The second object to compare.</param>
		/// <returns>true if d is less than d2; otherwise, false.</returns>
		public static bool operator <(UtcDateTime utcDateTime, DateTime dateTime)
		{
			return (utcDateTime._dt < dateTime);
		}

		/// <summary>
		/// Determines whether one specified <see cref="UtcDateTime"/> is less than or equal to another specified <see cref="UtcDateTime"/>.
		/// </summary>
		/// <param name="utcDateTime1">The first object to compare.</param>
		/// <param name="utcDateTime2">The second object to compare.</param>
		/// <returns>true if d1 is less than d2; otherwise, false.</returns>
		public static bool operator <=(UtcDateTime utcDateTime1, UtcDateTime utcDateTime2)
		{
			return (utcDateTime1._dt <= utcDateTime2._dt);
		}

		/// <summary>
		/// Determines whether one specified <see cref="DateTime"/> is less than or equal to another specified <see cref="UtcDateTime"/>.
		/// </summary>
		/// <param name="dateTime">The first object to compare.</param>
		/// <param name="utcDateTime">The second object to compare.</param>
		/// <returns>true if d1 is less than d2; otherwise, false.</returns>
		public static bool operator <=(DateTime dateTime, UtcDateTime utcDateTime)
		{
			return (dateTime <= utcDateTime._dt);
		}

		/// <summary>
		/// Determines whether one specified <see cref="UtcDateTime"/> is less than or equal to another specified <see cref="DateTime"/>.
		/// </summary>
		/// <param name="utcDateTime">The first object to compare.</param>
		/// <param name="dateTime">The second object to compare.</param>
		/// <returns>true if d1 is less than d2; otherwise, false.</returns>
		public static bool operator <=(UtcDateTime utcDateTime, DateTime dateTime)
		{
			return (utcDateTime._dt <= dateTime);
		}

		/// <summary>
		/// Subtracts a specified date and time from another specified date and time and returns a time interval.
		/// </summary>
		/// <param name="utcDateTime1">The date and time value to subtract from (the minuend).</param>
		/// <param name="utcDateTime2">The date and time value to subtract (the subtrahend).</param>
		/// <returns>The time interval between d1 and d2; that is, d1 minus d2.</returns>
		public static TimeSpan operator -(UtcDateTime utcDateTime1, UtcDateTime utcDateTime2)
		{
			return (utcDateTime1._dt - utcDateTime2._dt);
		}

		/// <summary>
		/// Subtracts a specified date and time from another specified date and time and returns a time interval.
		/// </summary>
		/// <param name="dateTime">The date and time value to subtract from (the minuend).</param>
		/// <param name="utcDateTime">The date and time value to subtract (the subtrahend).</param>
		/// <returns>The time interval between d1 and d2; that is, d1 minus d2.</returns>
		public static TimeSpan operator -(DateTime dateTime, UtcDateTime utcDateTime)
		{
			return (dateTime - utcDateTime._dt);
		}

		/// <summary>
		/// Subtracts a specified date and time from another specified date and time and returns a time interval.
		/// </summary>
		/// <param name="utcDateTime">The date and time value to subtract from (the minuend).</param>
		/// <param name="dateTime">The date and time value to subtract (the subtrahend).</param>
		/// <returns>The time interval between d1 and d2; that is, d1 minus d2.</returns>
		public static TimeSpan operator -(UtcDateTime utcDateTime, DateTime dateTime)
		{
			return (utcDateTime._dt - dateTime);
		}

		/// <summary>
		/// Subtracts a specified time interval from a specified date and time and returns a new date and time.
		/// </summary>
		/// <param name="utcDateTime">The date and time value to subtract from.</param>
		/// <param name="timeSpan">The time interval to subtract.</param>
		/// <returns>A <see cref="UtcDateTime"/> whose value is the value of d minus the value of t.</returns>
		public static UtcDateTime operator -(UtcDateTime utcDateTime, TimeSpan timeSpan)
		{
			return new UtcDateTime(utcDateTime._dt - timeSpan);
		}

		#endregion Operator Overloads

		#region Add, Subtract Methods

		/// <summary>
		/// Returns a new <see cref="UtcDateTime"/> that adds the value of the specified TimeSpan to the value of this instance.
		/// </summary>
		/// <param name="value">A positive or negative time interval.</param>
		/// <returns>An object whose value is the sum of the date and time represented by this instance and the time interval represented by value.</returns>
		public UtcDateTime Add(TimeSpan value)
		{
			return new UtcDateTime(_dt.Add(value));
		}

		/// <summary>
		/// Returns a new <see cref="UtcDateTime"/> that adds the specified number of years to the value of this instance.
		/// </summary>
		/// <param name="value">A number of years. The value parameter can be negative or positive.</param>
		/// <returns>An object whose value is the sum of the date and time represented by this instance and the number of years represented by value.</returns>
		public UtcDateTime AddYears(Int32 value)
		{
			return new UtcDateTime(_dt.AddYears(value));
		}

		/// <summary>
		/// Returns a new <see cref="UtcDateTime"/>  that adds the specified number of months to the value of this instance.
		/// </summary>
		/// <param name="value">A number of months. The months parameter can be negative or positive.</param>
		/// <returns>An object whose value is the sum of the date and time represented by this instance and months.</returns>
		public UtcDateTime AddMonths(Int32 value)
		{
			return new UtcDateTime(_dt.AddMonths(value));
		}

		/// <summary>
		/// Returns a new <see cref="UtcDateTime"/> that adds the specified number of days to the value of this instance.
		/// </summary>
		/// <param name="value">A number of whole and fractional days. The value parameter can be negative or positive.</param>
		/// <returns>An object whose value is the sum of the date and time represented by this instance and the number of days represented by value.</returns>
		public UtcDateTime AddDays(double value)
		{
			return new UtcDateTime(_dt.AddDays(value));
		}

		/// <summary>
		/// Returns a new <see cref="UtcDateTime"/> that adds the specified number of hours to the value of this instance.
		/// </summary>
		/// <param name="value">A number of whole and fractional hours. The value parameter can be negative or positive.</param>
		/// <returns>An object whose value is the sum of the date and time represented by this instance and the number of hours represented by value.</returns>
		public UtcDateTime AddHours(double value)
		{
			return new UtcDateTime(_dt.AddHours(value));
		}

		/// <summary>
		/// Returns a new <see cref="UtcDateTime"/> that adds the specified number of minutes to the value of this instance.
		/// </summary>
		/// <param name="value">A number of whole and fractional minutes. The value parameter can be negative or positive.</param>
		/// <returns>An object whose value is the sum of the date and time represented by this instance and the number of minutes represented by value.</returns>
		public UtcDateTime AddMinutes(double value)
		{
			return new UtcDateTime(_dt.AddMinutes(value));
		}

		/// <summary>
		/// Returns a new <see cref="UtcDateTime"/> that adds the specified number of seconds to the value of this instance.
		/// </summary>
		/// <param name="value">A number of whole and fractional seconds. The value parameter can be negative or positive.</param>
		/// <returns>An object whose value is the sum of the date and time represented by this instance and the number of seconds represented by value.</returns>
		public UtcDateTime AddSeconds(double value)
		{
			return new UtcDateTime(_dt.AddSeconds(value));
		}

		/// <summary>
		/// Returns a new <see cref="UtcDateTime"/> that adds the specified number of milliseconds to the value of this instance.
		/// </summary>
		/// <param name="value">A number of whole and fractional milliseconds. The value parameter can be negative or positive. Note that this value is rounded to the nearest integer.</param>
		/// <returns>An object whose value is the sum of the date and time represented by this instance and the number of milliseconds represented by value.</returns>
		public UtcDateTime AddMilliseconds(double value)
		{
			return new UtcDateTime(_dt.AddMilliseconds(value));
		}

		/// <summary>
		/// Returns a new <see cref="UtcDateTime"/> that adds the specified number of ticks to the value of this instance.
		/// </summary>
		/// <param name="value">A number of 100-nanosecond ticks. The value parameter can be positive or negative.</param>
		/// <returns>An object whose value is the sum of the date and time represented by this instance and the time represented by value.</returns>
		public UtcDateTime AddTicks(long value)
		{
			return new UtcDateTime(_dt.AddTicks(value));
		}

		/// <summary>
		/// Subtracts the specified date and time from this instance.
		/// </summary>
		/// <param name="value">The date and time value to subtract.</param>
		/// <returns>A time interval that is equal to the date and time represented by this instance minus the date and time represented by value.</returns>
		public TimeSpan Subtract(UtcDateTime value)
		{
			return _dt - value._dt;
		}

		/// <summary>
		/// Subtracts the specified date and time from this instance.
		/// </summary>
		/// <param name="value">The date and time value to subtract.</param>
		/// <returns>A time interval that is equal to the date and time represented by this instance minus the date and time represented by value.</returns>
		public TimeSpan Subtract(DateTime value)
		{
			return _dt - value;
		}

		/// <summary>
		/// Subtracts the specified duration from this instance.
		/// </summary>
		/// <param name="value">The time interval to subtract.</param>
		/// <returns>An object that is equal to the date and time represented by this instance minus the time interval represented by value.</returns>
		public UtcDateTime Subtract(TimeSpan value)
		{
			return new UtcDateTime(_dt - value);
		}

		#endregion Add, Subtract Methods

		#region Parse Methods

		/// <summary>
		/// Deserializes a 64-bit binary value and recreates an original serialized <see cref="UtcDateTime"/> object.
		/// </summary>
		/// <param name="dateData">
		/// A 64-bit signed integer that encodes the <see cref="DateTimeKind">Kind</see> property in a 2-bit field and the Ticks property in a 62-bit field.
		/// </param>
		/// <returns>
		/// An object that is equivalent to the <see cref="UtcDateTime"/> object that was serialized by the <see cref="ToBinary"/> method.
		/// The returns <see cref="UtcDateTime"/> will always be of kind <see cref="DateTimeKind.Utc"/>.
		/// </returns>
		public static UtcDateTime FromBinary(long dateData)
		{
			return new UtcDateTime(DateTime.FromBinary(dateData).ToUniversalTime());
		}

		/// <summary>
		/// Converts the specified Windows file time to an equivalent UTC time.
		/// </summary>
		/// <param name="fileTime">A Windows file time expressed in ticks.</param>
		/// <returns>
		/// An object that represents the local time equivalent of the date and time represented by the <paramref name="fileTime"/> parameter.
		/// </returns>
		/// <remarks>
		/// A Windows file time is a 64-bit value that represents the number of 100-nanosecond intervals that have
		/// elapsed since 12:00 midnight, January 1, 1601 A.D. (C.E.) Coordinated Universal Time (UTC). Windows uses
		/// a file time to record when an application creates, accesses, or writes to a file.
		///
		/// The fileTime parameter specifies a file time expressed in 100-nanosecond ticks.
		///
		/// Starting with the .NET Framework version 2.0, the return value is a DateTime whose Kind property is Utc.
		/// </remarks>
		public static UtcDateTime FromFileTimeUtc(long fileTime)
		{
			return new UtcDateTime(DateTime.FromFileTimeUtc(fileTime).ToUniversalTime());
		}

		/// <summary>
		/// Converts the specified string representation of a date and time to its <see cref="UtcDateTime"/> equivalent.
		/// </summary>
		/// <param name="s">A string containing a date and time to convert.</param>
		/// <returns>An object that is equivalent to the date and time contained in s.</returns>
		public static UtcDateTime Parse(string s)
		{
			return new UtcDateTime(DateTime.Parse(s, CultureInfo.CurrentCulture, DateTimeStyles.AssumeUniversal));
		}

		/// <summary>
		/// Converts the specified string representation of a date and time to its <see cref="UtcDateTime"/>
		/// equivalent using the specified culture-specific format information.
		/// </summary>
		/// <param name="s">A string containing a date and time to convert.</param>
		/// <param name="provider">An object that supplies culture-specific format information about s.</param>
		/// <returns>An object that is equivalent to the date and time contained in s as specified by provider.</returns>
		public static UtcDateTime Parse(string s, IFormatProvider provider)
		{
			return new UtcDateTime(DateTime.Parse(s, provider, DateTimeStyles.AssumeUniversal));
		}

		/// <summary>
		/// Converts the specified string representation of a date and time to its <see cref="UtcDateTime"/>
		/// equivalent using the specified culture-specific format information and formatting style.
		/// </summary>
		/// <param name="s">A string containing a date and time to convert.</param>
		/// <param name="provider">An object that supplies culture-specific formatting information about s.</param>
		/// <param name="styles">
		/// A bitwise combination of the enumeration values that indicates the style elements that can be present
		/// in s for the parse operation to succeed and that defines how to interpret the parsed date in relation
		/// to the current time zone or the current date. A typical value to specify is <see cref="DateTimeStyles.None"/>.
		/// </param>
		/// <returns>An object that is equivalent to the date and time contained in s, as specified by provider and styles.</returns>
		public static UtcDateTime Parse(string s, IFormatProvider provider, DateTimeStyles styles)
		{
			return new UtcDateTime(DateTime.Parse(s, provider, styles));
		}

		/// <summary>
		/// Converts the specified string representation of a date and time to its <see cref="UtcDateTime"/>
		/// equivalent using the specified format and culture-specific format information.
		/// The format of the string representation must match the specified format exactly.
		/// </summary>
		/// <param name="s">A string that contains a date and time to convert.</param>
		/// <param name="format">A format specifier that defines the required format of s.</param>
		/// <param name="provider">An object that supplies culture-specific format information about s.</param>
		/// <returns>
		/// An object that is equivalent to the date and time contained in s, as specified by format and provider.
		/// </returns>
		public static UtcDateTime ParseExact(string s, string format, IFormatProvider provider)
		{
			return new UtcDateTime(DateTime.ParseExact(s, format, provider));
		}

		/// <summary>
		/// Converts the specified string representation of a date and time to its <see cref="UtcDateTime"/>
		/// equivalent using the specified format, culture-specific format information, and style.
		/// The format of the string representation must match the specified
		/// format exactly or an exception is thrown.
		/// </summary>
		/// <param name="s">A string containing a date and time to convert.</param>
		/// <param name="format">A format specifier that defines the required format of s.</param>
		/// <param name="provider">An object that supplies culture-specific formatting information about s.</param>
		/// <param name="style">
		/// A bitwise combination of the enumeration values that provides additional information about s,
		/// about style elements that may be present in s, or about the conversion from s to a
		/// <see cref="UtcDateTime"/> value. A typical value to specify is None.
		/// </param>
		/// <returns></returns>
		public static UtcDateTime ParseExact(string s, string format, IFormatProvider provider, DateTimeStyles style)
		{
			return new UtcDateTime(DateTime.ParseExact(s, format, provider, style));
		}

		/// <summary>
		/// Converts the specified string representation of a date and time to its <see cref="UtcDateTime"/>
		/// equivalent using the specified array of formats, culture-specific format information, and style.
		/// The format of the string representation must match at least one of the specified
		/// formats exactly or an exception is thrown.
		/// </summary>
		/// <param name="s">A string containing one or more dates and times to convert.</param>
		/// <param name="formats">An array of allowable formats of s.</param>
		/// <param name="provider">An object that supplies culture-specific format information about s.</param>
		/// <param name="style">
		/// A bitwise combination of enumeration values that indicates the permitted format of s.
		/// A typical value to specify is None.
		/// </param>
		/// <returns>
		/// An object that is equivalent to the date and time contained in s, as specified by formats, provider, and style.
		/// </returns>
		public static UtcDateTime ParseExact(string s, string[] formats, IFormatProvider provider, DateTimeStyles style)
		{
			return new UtcDateTime(DateTime.ParseExact(s, formats, provider, style));
		}

		/// <summary>
		/// Converts the specified string representation of a date and time to its <see cref="UtcDateTime"/>
		/// equivalent and returns a value that indicates whether the conversion succeeded.
		/// </summary>
		/// <param name="s">A string containing a date and time to convert.</param>
		/// <param name="result">
		/// When this method returns, contains the <see cref="UtcDateTime"/> value equivalent to the date and time contained in s,
		/// if the conversion succeeded, or <see cref="MinValue"/> if the conversion failed.
		/// The conversion fails if the s parameter is null, is an empty string (""), or does not contain a valid
		/// string representation of a date and time. This parameter is passed uninitialized.
		/// </param>
		/// <returns>true if the s parameter was converted successfully; otherwise, false.</returns>
		public static bool TryParse(string s, out UtcDateTime result)
		{
			DateTime dt;
			bool flag = DateTime.TryParse(s, CultureInfo.CurrentCulture, DateTimeStyles.AssumeUniversal, out dt);
			result = new UtcDateTime(dt);
			return flag;
		}

		/// <summary>
		/// Converts the specified string representation of a date and time to its <see cref="UtcDateTime"/>
		/// equivalent using the specified culture-specific format information and formatting style,
		/// and returns a value that indicates whether the conversion succeeded.
		/// </summary>
		/// <param name="s">A string containing a date and time to convert.</param>
		/// <param name="provider">An object that supplies culture-specific formatting information about s.</param>
		/// <param name="style">A bitwise combination of enumeration values that defines how to interpret the parsed date in relation to the current time zone or the current date. A typical value to specify is None.</param>
		/// <param name="result">
		/// When this method returns, contains the <see cref="UtcDateTime"/> value equivalent to the date and time contained in s,
		/// if the conversion succeeded, or <see cref="MinValue"/> if the conversion failed.
		/// The conversion fails if the s parameter is null, is an empty string (""), or does not contain a valid
		/// string representation of a date and time. This parameter is passed uninitialized.
		/// </param>
		/// <returns>true if the s parameter was converted successfully; otherwise, false.</returns>
		public static bool TryParse(string s, IFormatProvider provider, DateTimeStyles style, out UtcDateTime result)
		{
			DateTime dt;
			bool flag = DateTime.TryParse(s, provider, style, out dt);
			result = new UtcDateTime(dt);
			return flag;
		}

		/// <summary>
		/// Converts the specified string representation of a date and time to its <see cref="UtcDateTime"/>
		/// equivalent using the specified format, culture-specific format information, and style.
		/// The format of the string representation must match the specified format exactly.
		/// The method returns a value that indicates whether the conversion succeeded.
		/// </summary>
		/// <param name="s">A string containing a date and time to convert.</param>
		/// <param name="format">The required format of s.</param>
		/// <param name="provider">An object that supplies culture-specific formatting information about s.</param>
		/// <param name="style">A bitwise combination of one or more enumeration values that indicate the permitted format of s.</param>
		/// <param name="result">
		/// When this method returns, contains the <see cref="UtcDateTime"/> value equivalent to the date and time contained in s,
		/// if the conversion succeeded, or <see cref="MinValue"/> if the conversion failed. The conversion fails if either the
		/// s or format parameter is null, is an empty string, or does not contain a date and time that correspond
		/// to the pattern specified in format. This parameter is passed uninitialized.
		/// </param>
		/// <returns>true if the s parameter was converted successfully; otherwise, false.</returns>
		public static bool TryParseExact(string s, string format, IFormatProvider provider, DateTimeStyles style,
										 out UtcDateTime result)
		{
			DateTime dt;
			bool flag = DateTime.TryParseExact(s, format, provider, style, out dt);
			result = new UtcDateTime(dt);
			return flag;
		}

		/// <summary>
		/// Converts the specified string representation of a date and time to its <see cref="UtcDateTime"/>
		/// equivalent using the specified array of formats, culture-specific format information, and style.
		/// The format of the string representation must match at least one of the specified formats exactly.
		/// The method returns a value that indicates whether the conversion succeeded.
		/// </summary>
		/// <param name="s">A string containing one or more dates and times to convert.</param>
		/// <param name="formats">An array of allowable formats of s.</param>
		/// <param name="provider">An object that supplies culture-specific format information about s.</param>
		/// <param name="style">A bitwise combination of enumeration values that indicates the permitted format of s. A typical value to specify is DateTimeStyles.None.</param>
		/// <param name="result">
		/// When this method returns, contains the <see cref="UtcDateTime"/> value equivalent to the date and time contained in s,
		/// if the conversion succeeded, or <see cref="MinValue"/> if the conversion failed. The conversion fails if s or formats
		/// is null, s or an element of formats is an empty string, or the format of s is not exactly as specified
		/// by at least one of the format patterns in formats. This parameter is passed uninitialized.
		/// </param>
		/// <returns>true if the s parameter was converted successfully; otherwise, false.</returns>
		public static bool TryParseExact(string s, string[] formats, IFormatProvider provider, DateTimeStyles style,
										 out UtcDateTime result)
		{
			DateTime dt;
			bool flag = DateTime.TryParseExact(s, formats, provider, style, out dt);
			result = new UtcDateTime(dt);
			return flag;
		}

		/// <summary>
		/// Converts the specified string representations of year and month
		/// to their combined <see cref="UtcDateTime"/> equivalent assuming <see cref="DateTimeStyles.AssumeUniversal">universal</see> time.
		/// The format of the string representations must be numeric.
		/// The method returns a value that indicates whether the conversion succeeded.
		/// </summary>
		/// <param name="year">The year (1 through the number of years in calendar).</param>
		/// <param name="month">The month (1 through the number of months in calendar).</param>
		/// <param name="result">
		/// When this method returns, contains the <see cref="UtcDateTime"/> value equivalent to the date and time contained in parameters,
		/// if the conversion succeeded, or <see cref="MinValue"/> if the conversion failed. The days component of the result
		/// will always be set to 1. Hour, minute and second components of the result will always be set to 0.
		/// The conversion fails if any of the parameters are null, or not numeric, or out of range.
		/// </param>
		/// <returns>true if the parameters was converted successfully; otherwise, false.</returns>
		public static bool TryParse(string year, string month, out UtcDateTime result)
		{
			return TryParse(year, month, "1", "0", "0", "0", out result);
		}

		/// <summary>
		/// Converts the specified string representations of year, month and day
		/// to their combined <see cref="UtcDateTime"/> equivalent assuming <see cref="DateTimeStyles.AssumeUniversal">universal</see> time.
		/// The format of the string representations must be numeric.
		/// The method returns a value that indicates whether the conversion succeeded.
		/// </summary>
		/// <param name="year">The year (1 through the number of years in calendar).</param>
		/// <param name="month">The month (1 through the number of months in calendar).</param>
		/// <param name="day">The day (1 through the number of days in month).</param>
		/// <param name="result">
		/// When this method returns, contains the <see cref="UtcDateTime"/> value equivalent to the date and time contained in parameters,
		/// if the conversion succeeded, or <see cref="MinValue"/> if the conversion failed. Hours, minutes and seconds of the result will always be set to 0.
		/// The conversion fails if any of the parameters are null, or not numeric, or out of range.
		/// </param>
		/// <returns>true if the parameters was converted successfully; otherwise, false.</returns>
		public static bool TryParse(string year, string month, string day, out UtcDateTime result)
		{
			return TryParse(year, month, day, "0", "0", "0", out result);
		}

		/// <summary>
		/// Converts the specified string representations of year, month, day and hour
		/// to their combined <see cref="UtcDateTime"/> equivalent assuming <see cref="DateTimeStyles.AssumeUniversal">universal</see> time.
		/// The format of the string representations must be numeric.
		/// The method returns a value that indicates whether the conversion succeeded.
		/// </summary>
		/// <param name="year">The year (1 through the number of years in calendar).</param>
		/// <param name="month">The month (1 through the number of months in calendar).</param>
		/// <param name="day">The day (1 through the number of days in month).</param>
		/// <param name="hour">The hours (0 through 23).</param>
		/// <param name="result">
		/// When this method returns, contains the <see cref="UtcDateTime"/> value equivalent to the date and time contained in parameters,
		/// if the conversion succeeded, or <see cref="MinValue"/> if the conversion failed. Minutes and seconds of the result will always be set to 0.
		/// The conversion fails if any of the parameters are null, or not numeric, or out of range.
		/// </param>
		/// <returns>true if the parameters was converted successfully; otherwise, false.</returns>
		public static bool TryParse(string year, string month, string day, string hour, out UtcDateTime result)
		{
			return TryParse(year, month, day, hour, "0", "0", out result);
		}

		/// <summary>
		/// Converts the specified string representations of year, month, day, hour and minute
		/// to their combined <see cref="UtcDateTime"/> equivalent assuming <see cref="DateTimeStyles.AssumeUniversal">universal</see> time.
		/// The format of the string representations must be numeric.
		/// The method returns a value that indicates whether the conversion succeeded.
		/// </summary>
		/// <param name="year">The year (1 through the number of years in calendar).</param>
		/// <param name="month">The month (1 through the number of months in calendar).</param>
		/// <param name="day">The day (1 through the number of days in month).</param>
		/// <param name="hour">The hours (0 through 23).</param>
		/// <param name="minute">The minutes (0 through 59).</param>
		/// <param name="result">
		/// When this method returns, contains the <see cref="UtcDateTime"/> value equivalent to the date and time contained in parameters,
		/// if the conversion succeeded, or <see cref="MinValue"/> if the conversion failed. Seconds of the result will always be set to 0.
		/// The conversion fails if any of the parameters are null, or not numeric, or out of range.
		/// </param>
		/// <returns>true if the parameters was converted successfully; otherwise, false.</returns>
		public static bool TryParse(string year, string month, string day, string hour, string minute, out UtcDateTime result)
		{
			return TryParse(year, month, day, hour, minute, "0", out result);
		}

		/// <summary>
		/// Converts the specified string representations of year, month, day, hour, minute and second
		/// to their combined <see cref="UtcDateTime"/> equivalent assuming <see cref="DateTimeStyles.AssumeUniversal">universal</see> time.
		/// The format of the string representations must be numeric.
		/// The method returns a value that indicates whether the conversion succeeded.
		/// </summary>
		/// <param name="year">The year (1 through the number of years in calendar).</param>
		/// <param name="month">The month (1 through the number of months in calendar).</param>
		/// <param name="day">The day (1 through the number of days in month).</param>
		/// <param name="hour">The hours (0 through 23).</param>
		/// <param name="minute">The minutes (0 through 59).</param>
		/// <param name="second">The seconds (0 through 59).</param>
		/// <param name="result">
		/// When this method returns, contains the <see cref="UtcDateTime"/> value equivalent to the date and time contained in parameters,
		/// if the conversion succeeded, or <see cref="MinValue"/> if the conversion failed. The conversion fails if any of the parameters
		/// are null, or not numeric, or out of range.
		/// </param>
		/// <returns>true if the parameters was converted successfully; otherwise, false.</returns>
		public static bool TryParse(string year, string month, string day, string hour, string minute, string second,
									out UtcDateTime result)
		{
			var s = String.Format("{0:00}-{1:00}-{2:00}T{3:00}:{4:00}:{5:00}Z",
									 Null2Empty(year).PadLeft(4, '0'),
									 Null2Empty(month).PadLeft(2, '0'),
									 Null2Empty(day).PadLeft(2, '0'),
									 Null2Empty(hour).PadLeft(2, '0'),
									 Null2Empty(minute).PadLeft(2, '0'),
									 Null2Empty(second).PadLeft(2, '0'));

			return TryParse(s, CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal | DateTimeStyles.AdjustToUniversal, out result);
		}

		#endregion Parse Methods

		#region GetDateTimeFormats

		/// <summary>
		/// Converts the value of this instance to all the string representations supported by the standard date and time format specifiers.
		/// </summary>
		/// <returns>
		/// A string array where each element is the representation of the value of this instance formatted with one of the standard date and time format specifiers.
		/// </returns>
		/// <remarks>
		/// The string array returned by the UtcDateTime.GetDateTimeFormats() method is equivalent to combining the string arrays returned
		/// by separate calls to the <see cref="UtcDateTime.GetDateTimeFormats(Char)"/> method with the d, D, f, F, g, G, m, o, r, s, t, T, u, U, and y standard format strings.
		/// For more information about standard format specifiers, see Standard Date and Time Format Strings in MSDN Library.
		/// Each element of the return value is formatted using information from the current culture.
		/// For more information about culture-specific formatting information for the current culture, see <see cref="CultureInfo.CurrentCulture"/>.
		/// </remarks>
		public string[] GetDateTimeFormats()
		{
			return _dt.GetDateTimeFormats(CultureInfo.CurrentCulture);
		}

		/// <summary>
		/// Converts the value of this instance to all the string representations supported by the specified standard date and time format specifier.
		/// </summary>
		/// <param name="format">A standard date and time format string (see Remarks).</param>
		/// <returns>
		/// A string array where each element is the representation of the value of this instance formatted with the format standard date and time format specifier.
		/// </returns>
		/// <remarks>
		/// The format parameter can be any of the standard date and time format specifiers.
		/// These include d, D, f, F, g, G, M (or m), O (or o), R (or r), s, t, T, u, U, and Y (or y).
		/// For more information, see Standard Date and Time Format Strings in the MSDN Library.
		/// Each element of the return value is formatted using information from the current culture.
		/// For more information about culture-specific formatting information for the current culture, see <see cref="CultureInfo.CurrentCulture"/>.
		/// </remarks>
		public string[] GetDateTimeFormats(char format)
		{
			return _dt.GetDateTimeFormats(format);
		}

		/// <summary>
		/// Converts the value of this instance to all the string representations supported by the
		/// standard date and time format specifiers and the specified culture-specific formatting information.
		/// </summary>
		/// <param name="provider">
		/// An object that supplies culture-specific formatting information about this instance.
		/// </param>
		/// <returns>
		/// A string array where each element is the representation of the value of this instance formatted with one of the standard date and time format specifiers.
		/// </returns>
		/// <remarks>
		/// The string array returned by the UtcDateTime.GetDateTimeFormats(IFormatProvider) method is equivalent to combining the string arrays returned
		/// by separate calls to the <see cref="UtcDateTime.GetDateTimeFormats(Char, IFormatProvider)"/> method with
		/// the d, D, f, F, g, G, m, o, r, s, t, T, u, U, and y standard format strings.
		/// For more information about standard format specifiers, see Standard Date and Time Format Strings in MSDN Library.
		/// Each element of the return value is formatted using culture-specific information supplied by provider.
		/// </remarks>
		public string[] GetDateTimeFormats(IFormatProvider provider)
		{
			return _dt.GetDateTimeFormats(provider);
		}

		/// <summary>
		/// Converts the value of this instance to all the string representations supported by the specified
		/// standard date and time format specifier and culture-specific formatting information.
		/// </summary>
		/// <param name="format">A date and time format string (see Remarks).</param>
		/// <param name="provider">An object that supplies culture-specific formatting information about this instance.</param>
		/// <returns>
		/// A string array where each element is the representation of the value of this instance formatted with one of the standard date and time format specifiers.
		/// </returns>
		/// <remarks>
		/// The format parameter can be any of the standard date and time format specifiers.
		/// These include d, D, f, F, g, G, M (or m), O (or o), R (or r), s, t, T, u, U, and Y (or y).
		/// For more information, see Standard Date and Time Format Strings in the MSDN Library.
		/// Each element of the return value is formatted using culture-specific information supplied by <i>provider</i>.
		/// </remarks>
		public string[] GetDateTimeFormats(char format, IFormatProvider provider)
		{
			return _dt.GetDateTimeFormats(format, provider);
		}

		#endregion GetDateTimeFormats

		#region Utility Methods

		/// <summary>
		/// Returns the number of days in the specified month and year.
		/// </summary>
		/// <param name="year">The year.</param>
		/// <param name="month">The month (a number ranging from 1 to 12).</param>
		/// <returns>
		/// The number of days in month for the specified year.
		/// For example, if month equals 2 for February, the return value is 28 or 29 depending upon whether year is a leap year.
		/// </returns>
		/// <exception cref="ArgumentOutOfRangeException">
		/// month is less than 1 or greater than 12.
		/// -or-
		/// year is less than 1 or greater than 9999.
		/// </exception>
		public static int DaysInMonth(int year, int month)
		{
			return DateTime.DaysInMonth(year, month);
		}

		/// <summary>
		/// Returns an indication whether the specified year is a leap year.
		/// </summary>
		/// <param name="year">A 4-digit year.</param>
		/// <returns>true if year is a leap year; otherwise, false.</returns>
		/// <remarks>
		/// year is specified as a 4-digit base 10 number; for example, 1996.
		/// year is always interpreted as a year in the Gregorian calendar.
		/// To determine whether a particular year was a leap year in some other calendar,
		/// call that calendar object's IsLeapYear method.
		/// </remarks>
		public static bool IsLeapYear(int year)
		{
			return DateTime.IsLeapYear(year);
		}

		/// <summary>
		/// Indicates whether this instance of <see cref="UtcDateTime"/> is within the daylight saving time range for the current time zone.
		/// </summary>
		/// <returns>Always returns false.</returns>
		public bool IsDaylightSavingTime()
		{
			return _dt.IsDaylightSavingTime();
		}

		#endregion Utility Methods

		#region Equals, GetHashCode, To- Methods

		#region IConvertible Members

		/// <summary>
		/// Converts the value of the current <see cref="UtcDateTime"/> object to its equivalent string representation using the specified culture-specific format information.
		/// </summary>
		/// <param name="provider">An object that supplies culture-specific formatting information.</param>
		/// <returns>A string representation of value of the current <see cref="UtcDateTime"/> object as specified by provider.</returns>
		/// <seealso cref="DateTime.ToString(IFormatProvider)"/>
		public string ToString(IFormatProvider provider)
		{
			return _dt.ToString(provider);
		}

		#endregion IConvertible Members

		#region IFormattable Members

		/// <summary>
		/// Converts the value of the current <see cref="UtcDateTime"/> object to its equivalent string representation
		/// using the specified format and culture-specific format information.
		/// </summary>
		/// <param name="format">A standard or custom date and time format string.</param>
		/// <param name="formatProvider">An object that supplies culture-specific formatting information.</param>
		/// <returns>A string representation of value of the current <see cref="UtcDateTime"/> object as specified by format and provider.</returns>
		/// <seealso cref="DateTime.ToString(string, IFormatProvider)"/>
		public string ToString(string format, IFormatProvider formatProvider)
		{
			return _dt.ToString(format, formatProvider);
		}

		#endregion IFormattable Members

		/// <summary>
		/// Returns a value indicating whether this instance is equal to a specified object.
		/// </summary>
		/// <param name="obj">The object to compare to this instance.</param>
		/// <returns>
		/// true if <i>value</i> is an instance of <see cref="DateTime"/> or <see cref="UtcDateTime"/>
		/// and equals the value of this instance; otherwise, false.
		/// </returns>
		/// <seealso cref="DateTime.Equals(object)" />
		public override bool Equals(object obj)
		{
			if (obj is DateTime) return _dt.Equals(obj);
			if (obj is UtcDateTime) return _dt.Equals(((UtcDateTime)obj)._dt);
			return false;
		}

		/// <summary>
		/// Returns a value indicating whether two instances of <see cref="UtcDateTime"/> are equal.
		/// </summary>
		/// <param name="utcDateTime1">The first object to compare.</param>
		/// <param name="utcDateTime2">The second object to compare.</param>
		/// <returns>true if the two values are equal; otherwise, false.</returns>
		/// <remarks>
		/// t1 and t2 are equal if their Ticks property values are equal.
		/// </remarks>
		public static bool Equals(UtcDateTime utcDateTime1, UtcDateTime utcDateTime2)
		{
			return DateTime.Equals(utcDateTime1._dt, utcDateTime2._dt);
		}

		/// <summary>
		/// Returns a value indicating whether and instance of <see cref="UtcDateTime"/> and
		/// an instance of <see cref="DateTime"/> are equal.
		/// </summary>
		/// <param name="utcDateTime">The first object to compare.</param>
		/// <param name="dateTime">The second object to compare.</param>
		/// <returns>true if the two values are equal; otherwise, false.</returns>
		/// <remarks>
		/// t1 and t2 are equal if their Ticks property values are equal.
		/// The <see cref="DateTime.Kind"/> property of t2 is not considered in the test for equality.
		/// </remarks>
		public static bool Equals(UtcDateTime utcDateTime, DateTime dateTime)
		{
			return DateTime.Equals(utcDateTime._dt, dateTime);
		}

		/// <summary>
		/// Returns a value indicating whether and instance of <see cref="DateTime"/> and
		/// an instance of <see cref="UtcDateTime"/> are equal.
		/// </summary>
		/// <param name="dateTime">The first object to compare.</param>
		/// <param name="utcDateTime">The second object to compare.</param>
		/// <returns>true if the two values are equal; otherwise, false.</returns>
		/// <remarks>
		/// t1 and t2 are equal if their Ticks property values are equal.
		/// The <see cref="DateTime.Kind"/> property of t1 is not considered in the test for equality.
		/// </remarks>
		public static bool Equals(DateTime dateTime, UtcDateTime utcDateTime)
		{
			return DateTime.Equals(dateTime, utcDateTime._dt);
		}

		/// <summary>
		/// Returns the hash code for this instance.
		/// </summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		/// <seealso cref="DateTime.GetHashCode"/>
		public override int GetHashCode()
		{
			return _dt.GetHashCode();
		}

		/// <summary>
		/// Returns the DateTime as a DateTime value.
		/// </summary>
		/// <returns>DateTime value.</returns>
		public DateTime ToDateTime()
		{
			// The underlying DateTime may have unspecified kind if the instance is not initialized with one of the constructors.
			return _dt.Kind == DateTimeKind.Utc ? _dt : _dt.ToUniversalTime();
		}

		/// <summary>
		/// Serializes the current <see cref="UtcDateTime"/> object to a 64-bit binary value
		/// that subsequently can be used to recreate the <see cref="UtcDateTime"/> object.
		/// </summary>
		/// <returns>A 64-bit signed integer that encodes the <see cref="DateTimeKind.Utc"/>Kind and Ticks properties</returns>
		/// <seealso cref="DateTime.ToBinary"/>
		public long ToBinary()
		{
			return _dt.ToBinary();
		}

		/// <summary>
		/// Converts the value of the current <see cref="UtcDateTime"/> object to a Windows file time.
		/// </summary>
		/// <returns>
		/// The value of the current <see cref="UtcDateTime"/> object expressed as a Windows file time.
		/// </returns>
		/// <seealso cref="DateTime.ToFileTimeUtc"/>
		public long ToFileTimeUtc()
		{
			return _dt.ToFileTime();
		}

		/// <summary>
		/// Converts the value of the current <see cref="UtcDateTime"/> object to its equivalent long date string representation.
		/// </summary>
		/// <returns>
		/// A string that contains the long date string representation of the current <see cref="UtcDateTime"/> object.
		/// </returns>
		/// <seealso cref="DateTime.ToLongDateString"/>
		public string ToLongDateString()
		{
			return _dt.ToLongDateString();
		}

		/// <summary>
		/// Converts the value of the current <see cref="UtcDateTime"/>  object to its equivalent long time string representation.
		/// </summary>
		/// <returns>
		/// A string that contains the long time string representation of the current <see cref="UtcDateTime"/>  object.
		/// </returns>
		/// <seealso cref="DateTime.ToLongTimeString"/>
		public string ToLongTimeString()
		{
			return _dt.ToLongTimeString();
		}

		/// <summary>
		/// Converts the value of the current <see cref="UtcDateTime"/> object to its equivalent short date string representation.
		/// </summary>
		/// <returns>A string that contains the short date string representation of the current <see cref="UtcDateTime"/> object.</returns>
		/// <seealso cref="DateTime.ToShortDateString"/>
		public string ToShortDateString()
		{
			return _dt.ToShortDateString();
		}

		/// <summary>
		/// Converts the value of the current <see cref="UtcDateTime"/> object to its equivalent short time string representation.
		/// </summary>
		/// <returns>
		/// A string that contains the short time string representation of the current <see cref="UtcDateTime"/> object.
		/// </returns>
		/// <seealso cref="DateTime.ToShortTimeString"/>
		public string ToShortTimeString()
		{
			return _dt.ToShortTimeString();
		}

		/// <summary>
		/// Converts the value of the current <see cref="UtcDateTime"/> object to its equivalent string representation.
		/// </summary>
		/// <returns>
		/// A string representation of the value of the current <see cref="UtcDateTime"/> object.
		/// </returns>
		/// <seealso cref="DateTime.ToString()"/>
		public override string ToString()
		{
			// ReSharper disable SpecifyACultureInStringConversionExplicitly
			return _dt.ToString();
			// ReSharper restore SpecifyACultureInStringConversionExplicitly
		}

		/// <summary>
		/// Converts the value of the current <see cref="UtcDateTime"/> object to its equivalent string representation using the specified format.
		/// </summary>
		/// <param name="format">A standard or custom date and time format string.</param>
		/// <returns>A string representation of value of the current <see cref="UtcDateTime"/> object as specified by format.</returns>
		/// <seealso cref="DateTime.ToString(string)"/>
		public string ToString(string format)
		{
			return _dt.ToString(format);
		}

		/// <summary>
		/// Converts the value of the <see cref="UtcDateTime"/> to its equivalent ISO 8601 formatted string representation.
		/// </summary>
		public string ToIsoString()
		{
			return ToString("yyyy-MM-ddTHH:mm:ssZ", CultureInfo.InvariantCulture);
		}

		#endregion Equals, GetHashCode, To- Methods

		#region IComparable, IComparable<UTCDateTime>

		#region IComparable Members

		/// <summary>
		/// Compares the current instance with another object of the same type and returns an integer that indicates whether the current instance precedes, follows, or occurs in the same position in the sort order as the other object.
		/// </summary>
		/// <returns>
		/// A value that indicates the relative order of the objects being compared. The return value has these meanings: Value Meaning Less than zero This instance is less than <paramref name="obj"/>. Zero This instance is equal to <paramref name="obj"/>. Greater than zero This instance is greater than <paramref name="obj"/>.
		/// </returns>
		/// <param name="obj">An object to compare with this instance. </param><exception cref="T:System.ArgumentException"><paramref name="obj"/> is not the same type as this instance. </exception><filterpriority>2</filterpriority>
		public int CompareTo(object obj)
		{
			if (obj == null)
			{
				return 1;
			}

			if (!(obj is UtcDateTime))
			{
				throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "Argument type should be {0}", GetType()));
			}

			return CompareTo((UtcDateTime)obj);
		}

		#endregion IComparable Members

		#region IComparable<UtcDateTime> Members

		/// <summary>
		/// Compares the value of this instance to a specified <see cref="UtcDateTime"/> value and returns an integer that indicates
		/// whether this instance is earlier than, the same as, or later than the specified <see cref="UtcDateTime"/> value.
		/// </summary>
		/// <param name="other">The object to compare to the current instance.</param>
		/// <returns>A signed number indicating the relative values of this instance and the value parameter.</returns>
		/// <seealso cref="DateTime.CompareTo(DateTime)"/>
		public int CompareTo(UtcDateTime other)
		{
			return _dt.CompareTo(other._dt);
		}

		#endregion IComparable<UtcDateTime> Members

		/// <summary>
		/// Compares the value of this instance to a specified <see cref="DateTime"/> value and returns an integer that indicates
		/// whether this instance is earlier than, the same as, or later than the specified <see cref="DateTime"/> value.
		/// </summary>
		/// <param name="other">The object to compare to the current instance.</param>
		/// <returns>A signed number indicating the relative values of this instance and the value parameter.</returns>
		/// <seealso cref="DateTime.CompareTo(DateTime)"/>
		public int CompareTo(DateTime other)
		{
			return _dt.CompareTo(other);
		}

		/// <summary>
		/// Compares two instances of <see cref="DateTime"/> and returns an integer that indicates whether the first
		/// instance is earlier than, the same as, or later than the second instance.
		/// </summary>
		/// <param name="utcDateTime1">The first object to compare.</param>
		/// <param name="utcDateTime2">The second object to compare.</param>
		/// <returns>A signed number indicating the relative values of t1 and t2.</returns>
		/// <seealso cref="DateTime.Compare(DateTime, DateTime)"/>
		public static int Compare(UtcDateTime utcDateTime1, UtcDateTime utcDateTime2)
		{
			return DateTime.Compare(utcDateTime1._dt, utcDateTime2._dt);
		}

		/// <summary>
		/// Compares an instance of <see cref="DateTime"/> and an instance of <see cref="UtcDateTime"/>
		/// and returns an integer that indicates whether the first instance is earlier than, the same as,
		/// or later than the second instance.
		/// </summary>
		/// <param name="dateTime">The first object to compare.</param>
		/// <param name="utcDateTime">The second object to compare.</param>
		/// <returns>A signed number indicating the relative values of t1 and t2.</returns>
		/// <seealso cref="DateTime.Compare(DateTime, DateTime)"/>
		public static int Compare(DateTime dateTime, UtcDateTime utcDateTime)
		{
			return DateTime.Compare(dateTime, utcDateTime._dt);
		}

		/// <summary>
		/// Compares an instance of <see cref="UtcDateTime"/> and an instance of <see cref="DateTime"/>
		/// and returns an integer that indicates whether the first instance is earlier than, the same as,
		/// or later than the second instance.
		/// </summary>
		/// <param name="utcDateTime">The first object to compare.</param>
		/// <param name="dateTime">The second object to compare.</param>
		/// <returns>A signed number indicating the relative values of t1 and t2.</returns>
		/// <seealso cref="DateTime.Compare(DateTime, DateTime)"/>
		public static int Compare(UtcDateTime utcDateTime, DateTime dateTime)
		{
			return DateTime.Compare(utcDateTime._dt, dateTime);
		}

		#endregion IComparable, IComparable<UTCDateTime>

		#region IConvertible

		/// <summary>
		/// Returns the <see cref="TypeCode"/> for value type <see cref="DateTime"/>.
		/// </summary>
		/// <returns>The enumerated constant, <see cref="TypeCode.DateTime"/>.</returns>
		public TypeCode GetTypeCode()
		{
			return _dt.GetTypeCode();
		}

		/// <summary>
		/// Converts the value of this instance to an equivalent Boolean value using the specified culture-specific formatting information.
		/// </summary>
		/// <param name="provider">An <see cref="T:System.IFormatProvider"/> interface implementation that supplies culture-specific formatting information.</param>
		/// <returns>
		/// A Boolean value equivalent to the value of this instance.
		/// </returns>
		bool IConvertible.ToBoolean(IFormatProvider provider)
		{
			return ((IConvertible)_dt).ToBoolean(provider);
		}

		/// <summary>
		/// Converts the value of this instance to an equivalent 8-bit unsigned integer using the specified culture-specific formatting information.
		/// </summary>
		/// <param name="provider">An <see cref="T:System.IFormatProvider"/> interface implementation that supplies culture-specific formatting information.</param>
		/// <returns>
		/// An 8-bit unsigned integer equivalent to the value of this instance.
		/// </returns>
		byte IConvertible.ToByte(IFormatProvider provider)
		{
			return ((IConvertible)_dt).ToByte(provider);
		}

		/// <summary>
		/// Converts the value of this instance to an equivalent Unicode character using the specified culture-specific formatting information.
		/// </summary>
		/// <param name="provider">An <see cref="T:System.IFormatProvider"/> interface implementation that supplies culture-specific formatting information.</param>
		/// <returns>
		/// A Unicode character equivalent to the value of this instance.
		/// </returns>
		char IConvertible.ToChar(IFormatProvider provider)
		{
			return ((IConvertible)_dt).ToChar(provider);
		}

		/// <summary>
		/// Converts the value of this instance to an equivalent <see cref="T:System.DateTime"/> using the specified culture-specific formatting information.
		/// </summary>
		/// <param name="provider">An <see cref="T:System.IFormatProvider"/> interface implementation that supplies culture-specific formatting information.</param>
		/// <returns>
		/// A <see cref="T:System.DateTime"/> instance equivalent to the value of this instance.
		/// </returns>
		DateTime IConvertible.ToDateTime(IFormatProvider provider)
		{
			return ((IConvertible)_dt).ToDateTime(provider);
		}

		/// <summary>
		/// Converts the value of this instance to an equivalent <see cref="T:System.Decimal"/> number using the specified culture-specific formatting information.
		/// </summary>
		/// <param name="provider">An <see cref="T:System.IFormatProvider"/> interface implementation that supplies culture-specific formatting information.</param>
		/// <returns>
		/// A <see cref="T:System.Decimal"/> number equivalent to the value of this instance.
		/// </returns>
		decimal IConvertible.ToDecimal(IFormatProvider provider)
		{
			return ((IConvertible)_dt).ToDecimal(provider);
		}

		/// <summary>
		/// Converts the value of this instance to an equivalent double-precision floating-point number using the specified culture-specific formatting information.
		/// </summary>
		/// <param name="provider">An <see cref="T:System.IFormatProvider"/> interface implementation that supplies culture-specific formatting information.</param>
		/// <returns>
		/// A double-precision floating-point number equivalent to the value of this instance.
		/// </returns>
		double IConvertible.ToDouble(IFormatProvider provider)
		{
			return ((IConvertible)_dt).ToDouble(provider);
		}

		/// <summary>
		/// Converts the value of this instance to an equivalent 16-bit signed integer using the specified culture-specific formatting information.
		/// </summary>
		/// <param name="provider">An <see cref="T:System.IFormatProvider"/> interface implementation that supplies culture-specific formatting information.</param>
		/// <returns>
		/// An 16-bit signed integer equivalent to the value of this instance.
		/// </returns>
		short IConvertible.ToInt16(IFormatProvider provider)
		{
			return ((IConvertible)_dt).ToInt16(provider);
		}

		/// <summary>
		/// Converts the value of this instance to an equivalent 32-bit signed integer using the specified culture-specific formatting information.
		/// </summary>
		/// <param name="provider">An <see cref="T:System.IFormatProvider"/> interface implementation that supplies culture-specific formatting information.</param>
		/// <returns>
		/// An 32-bit signed integer equivalent to the value of this instance.
		/// </returns>
		int IConvertible.ToInt32(IFormatProvider provider)
		{
			return ((IConvertible)_dt).ToInt32(provider);
		}

		/// <summary>
		/// Converts the value of this instance to an equivalent 64-bit signed integer using the specified culture-specific formatting information.
		/// </summary>
		/// <param name="provider">An <see cref="T:System.IFormatProvider"/> interface implementation that supplies culture-specific formatting information.</param>
		/// <returns>
		/// An 64-bit signed integer equivalent to the value of this instance.
		/// </returns>
		long IConvertible.ToInt64(IFormatProvider provider)
		{
			return ((IConvertible)_dt).ToInt64(provider);
		}

		/// <summary>
		/// Converts the value of this instance to an equivalent 8-bit signed integer using the specified culture-specific formatting information.
		/// </summary>
		/// <param name="provider">An <see cref="T:System.IFormatProvider"/> interface implementation that supplies culture-specific formatting information.</param>
		/// <returns>
		/// An 8-bit signed integer equivalent to the value of this instance.
		/// </returns>
		sbyte IConvertible.ToSByte(IFormatProvider provider)
		{
			return ((IConvertible)_dt).ToSByte(provider);
		}

		/// <summary>
		/// Converts the value of this instance to an equivalent single-precision floating-point number using the specified culture-specific formatting information.
		/// </summary>
		/// <param name="provider">An <see cref="T:System.IFormatProvider"/> interface implementation that supplies culture-specific formatting information.</param>
		/// <returns>
		/// A single-precision floating-point number equivalent to the value of this instance.
		/// </returns>
		float IConvertible.ToSingle(IFormatProvider provider)
		{
			return ((IConvertible)_dt).ToSingle(provider);
		}

		/// <summary>
		/// Converts the value of this instance to an <see cref="T:System.Object"/> of the specified <see cref="T:System.Type"/> that has an equivalent value, using the specified culture-specific formatting information.
		/// </summary>
		/// <returns>
		/// An <see cref="T:System.Object"/> instance of type <paramref name="conversionType"/> whose value is equivalent to the value of this instance.
		/// </returns>
		/// <param name="conversionType">The <see cref="T:System.Type"/> to which the value of this instance is converted. </param><param name="provider">An <see cref="T:System.IFormatProvider"/> interface implementation that supplies culture-specific formatting information. </param><filterpriority>2</filterpriority>
		object IConvertible.ToType(Type conversionType, IFormatProvider provider)
		{
			return ((IConvertible)_dt).ToType(conversionType, provider);
		}

		/// <summary>
		/// Converts the value of this instance to an equivalent 16-bit unsigned integer using the specified culture-specific formatting information.
		/// </summary>
		/// <param name="provider">An <see cref="T:System.IFormatProvider"/> interface implementation that supplies culture-specific formatting information.</param>
		/// <returns>
		/// An 16-bit unsigned integer equivalent to the value of this instance.
		/// </returns>
		ushort IConvertible.ToUInt16(IFormatProvider provider)
		{
			return ((IConvertible)_dt).ToUInt16(provider);
		}

		/// <summary>
		/// Converts the value of this instance to an equivalent 32-bit unsigned integer using the specified culture-specific formatting information.
		/// </summary>
		/// <param name="provider">An <see cref="T:System.IFormatProvider"/> interface implementation that supplies culture-specific formatting information.</param>
		/// <returns>
		/// An 32-bit unsigned integer equivalent to the value of this instance.
		/// </returns>
		uint IConvertible.ToUInt32(IFormatProvider provider)
		{
			return ((IConvertible)_dt).ToUInt32(provider);
		}

		/// <summary>
		/// Converts the value of this instance to an equivalent 64-bit unsigned integer using the specified culture-specific formatting information.
		/// </summary>
		/// <param name="provider">An <see cref="T:System.IFormatProvider"/> interface implementation that supplies culture-specific formatting information.</param>
		/// <returns>
		/// An 64-bit unsigned integer equivalent to the value of this instance.
		/// </returns>
		ulong IConvertible.ToUInt64(IFormatProvider provider)
		{
			return ((IConvertible)_dt).ToUInt64(provider);
		}

		#endregion IConvertible

		#region ISerializable

		/// <summary>
		/// Populates a <see cref="T:System.Runtime.Serialization.SerializationInfo"/> with the data needed to serialize the target object.
		/// </summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo"/> to populate with data.</param>
		/// <param name="context">The destination (see <see cref="T:System.Runtime.Serialization.StreamingContext"/>) for this serialization.</param>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null) throw new ArgumentNullException("info");
			//info.
			info.AddValue("Value", _dt);
			((ISerializable)_dt).GetObjectData(info, context);
		}

		#endregion ISerializable

		#region IEquatable<UTCDateTime>

		/// <summary>
		/// Returns a value indicating whether this instance is equal to the specified <see cref="UtcDateTime"/> instance.
		/// </summary>
		/// <param name="other">The object to compare to this instance.</param>
		/// <returns>true if the value parameter equals the value of this instance; otherwise, false.</returns>
		/// <seealso cref="DateTime.Equals(DateTime)"/>
		public bool Equals(UtcDateTime other)
		{
			return _dt.Equals(other._dt);
		}

		/// <summary>
		/// Returns a value indicating whether this instance is equal to the specified <see cref="DateTime"/> instance.
		/// </summary>
		/// <param name="other">The object to compare to this instance.</param>
		/// <returns>true if the value parameter equals the value of this instance; otherwise, false.</returns>
		/// <seealso cref="DateTime.Equals(DateTime)"/>
		public bool Equals(DateTime other)
		{
			return _dt.Equals(other);
		}

		#endregion IEquatable<UTCDateTime>

		#region Private Helpers

		/// <summary>
		/// Converts a potential null string value to an empty string.
		/// </summary>
		/// <param name="s">The string to convert.</param>
		/// <returns>
		/// If the specified string is not null, it is returned,
		/// otherwise an empty string is returned.
		/// </returns>
		private static string Null2Empty(string s)
		{
			return s ?? "";
		}

		#endregion Private Helpers
	}
}
