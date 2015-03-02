using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;

namespace Database
{
	/// <summary>
	/// Represents an item in a list of result returned from the data access layer.
	/// </summary>
	public class DataItem : IDataItem
	{
		private readonly Dictionary<string, int> _ordinals;
		private readonly IDataRecord _innerRow;

		/// <summary>
		/// Creates a new <see cref="DataItem"/> from the specified ordinals and record
		/// </summary>
		/// <param name="ordinals">The ordinals of the fields. Should be a lookup from the name of the field to its ordinal in the record</param>
		/// <param name="innerRow">The record to get the values from</param>
		public DataItem(Dictionary<string, int> ordinals, IDataRecord innerRow)
		{
			if (ordinals == null) throw new ArgumentNullException("ordinals");
			if (innerRow == null) throw new ArgumentNullException("innerRow");
			_ordinals = ordinals;
			_innerRow = innerRow;
		}

		/// <summary>
		/// Gets a value for the specified field
		/// </summary>
		/// <typeparam name="T">The type of the field</typeparam>
		/// <param name="fieldName">The name of the field</param>
		/// <returns>The value of the field</returns>
		/// <remarks>If the field value is null or cannot be converted to <typeparamref name="T"/>, the default value for <typeparamref name="T"/> will be returned</remarks>
		public T GetField<T>(string fieldName)
		{
			return GetField(fieldName, default(T));
		}

		/// <summary>
		/// Gets a value for the specified field with with fallback to a default value if value is missing or cannot be converted to <typeparamref name="T"/>
		/// </summary>
		/// <typeparam name="T">The type of the field</typeparam>
		/// <param name="fieldName">The name of the field</param>
		/// <param name="defaultValue">The default value to use if value is missing or value cannot be converted to <typeparamref name="T"/></param>
		/// <returns>The value of the field or <paramref name="defaultValue"/> if value is missing or cannot be converted to <typeparamref name="T"/></returns>
		public T GetField<T>(string fieldName, T defaultValue)
		{
			int ordinal;
			if (string.IsNullOrEmpty(fieldName) || !_ordinals.TryGetValue(fieldName, out ordinal))
				throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "Unknown field '{0}'", fieldName));
			var value = _innerRow[ordinal];
			if (value == DBNull.Value || value == null) return defaultValue;
			if (value is T) return (T)FixDate(value);
			try
			{
				return (T)FixDate(Convert.ChangeType(value, typeof(T), CultureInfo.InvariantCulture));
			}
			catch (Exception e)
			{
				throw new InvalidOperationException("Could not convert value to the desired type", e);
			}
		}

		private static object FixDate(object value)
		{
			if (value is DateTime)
			{
				var dt = (DateTime)value;
				return new DateTime(dt.Ticks, DateTimeKind.Utc);
			}
			return value;
		}
	}
}
