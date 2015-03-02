using System;
using System.Collections.Generic;
using System.Data;

namespace Database.Extensions
{
	///<summary>
	/// Helper extension methods for <see cref="IDataRecord"/> (and thereby <see cref="IDataReader"/>)
	///</summary>
	public static class DataReaderExtensions
	{
		///<summary>
		/// This method can be used if you have an <see cref="Enum"/> representing the fields in your 
		/// <see cref="IDataReader"/> to return a Dictionary with the ordinals of the the fields. Using 
		/// the ordinals instead of field names when getting values from the fields greatly improves 
		/// performance.
		///</summary>
		///<param name="reader">The reader to search for ordinals</param>
		///<typeparam name="TEnum">The enum with the field names</typeparam>
		///<returns>A dictionary with lookup from <typeparamref name="TEnum"/> to the ordinal of the field</returns>
		///<exception cref="ArgumentNullException">Raised if <paramref name="reader"/> is null</exception>
		///<exception cref="InvalidOperationException">Raised if <typeparamref name="TEnum"/> is not an enum</exception>
		public static Dictionary<TEnum, int> GetOrdinals<TEnum>(this IDataRecord reader)
		{
			if (reader == null) throw new ArgumentNullException("reader");
			var type = typeof(TEnum);
			if (!type.IsEnum) throw new InvalidOperationException("TEnum must be an enum type");
			var result = new Dictionary<TEnum, int>();
			var values = Enum.GetValues(type);
			var count = values.Length;
			for (var i = 0; i < count; i++)
			{
				var value = values.GetValue(i);
				var name = Enum.GetName(type, value);
				int ordinalValue;
				try
				{
					ordinalValue = reader.GetOrdinal(name);
				}
				catch (ArgumentException)
				{
					// TODO : Could consider logging a warning in this case, i.e. ordinal out-of-range in data record
					continue;
				}
				result.Add((TEnum)value, ordinalValue);
			}

			return result;
		}

		///<summary>
		/// Retrieves the ordinals from an IDataReader
		///</summary>
		///<param name="reader">The <see cref="IDataReader"/></param>
		///<returns>A dictionary with lookup from the name to the ordinal of the field</returns>
		///<exception cref="ArgumentNullException">Raised if <paramref name="reader"/> is null</exception>
		public static Dictionary<string, int> GetOrdinals(this IDataRecord reader)
		{
			if (reader == null) throw new ArgumentNullException("reader");

			var fieldCount = reader.FieldCount;
			var result = new Dictionary<string, int>(fieldCount, StringComparer.OrdinalIgnoreCase);
			for (var i = 0; i < fieldCount; i++)
			{
				result.Add(reader.GetName(i), i);
			}

			return result;
		}
	}
}
