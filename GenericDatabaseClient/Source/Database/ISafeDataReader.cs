using System;
using System.Data;

namespace Database
{
	/// <summary>
	/// A contract for an <see cref="IDataReader"/> that provides methods to safely read typed data from the database.
	/// </summary>
	public interface ISafeDataReader : IDataReader
	{
		#region GetBoolean

		/// <summary>
		/// Gets a boolean value from the data reader.
		/// </summary>
		/// <param name="index">Ordinal index of the column holding the value.</param>
		/// <param name="defaultValue">Default return value if the data reader field is <see cref="DBNull"/>.</param>
		/// <returns>A <see cref="Boolean"/>.</returns>
		bool GetBoolean(int index, bool defaultValue);

		/// <summary>
		/// Gets a boolean value from the data reader.
		/// </summary>
		/// <param name="name">Name of the column containing the value.</param>
		/// <returns>A <see cref="Boolean"/>.</returns>
		bool GetBoolean(string name);

		/// <summary>
		/// Gets a boolean value from the data reader.
		/// </summary>
		/// <param name="name">Name of the column containing the value.</param>
		/// <param name="defaultValue">Default return value if the data reader field is <see cref="DBNull"/>.</param>
		/// <returns>A <see cref="Boolean"/>.</returns>
		bool GetBoolean(string name, bool defaultValue);

		#endregion

		#region GetBooleanNullable

		/// <summary>
		/// Gets a nullable boolean value from the data reader.
		/// </summary>
		/// <param name="index">Ordinal index of the column holding the value.</param>
		/// <returns>A <see cref="Nullable"/> <see cref="Boolean"/>.</returns>
		bool? GetBooleanNullable(int index);

		/// <summary>
		/// Gets a nullable boolean value from the data reader.
		/// </summary>
		/// <param name="index">Ordinal index of the column holding the value.</param>
		/// <param name="defaultValue">Default return value if the data reader field is <see cref="DBNull"/>.</param>
		/// <returns>A <see cref="Nullable"/> <see cref="Boolean"/>.</returns>
		bool? GetBooleanNullable(int index, bool? defaultValue);

		/// <summary>
		/// Gets a nullable boolean value from the data reader.
		/// </summary>
		/// <param name="name">Name of the column containing the value.</param>
		/// <returns>A <see cref="Nullable"/> <see cref="Boolean"/>.</returns>
		bool? GetBooleanNullable(string name);

		/// <summary>
		/// Gets a nullable boolean value from the data reader.
		/// </summary>
		/// <param name="name">Name of the column containing the value.</param>
		/// <param name="defaultValue">Default return value if the data reader field is <see cref="DBNull"/>.</param>
		/// <returns>A <see cref="Nullable"/> <see cref="Boolean"/>.</returns>
		bool? GetBooleanNullable(string name, bool? defaultValue);

		#endregion

		#region GetByte

		/// <summary>
		/// Gets a byte value from the data reader.
		/// </summary>
		/// <param name="index">Ordinal index of the column holding the value.</param>
		/// <param name="defaultValue">Default return value if the data reader field is <see cref="DBNull"/>.</param>
		/// <returns>A <see cref="byte"/>.</returns>
		byte GetByte(int index, byte defaultValue);

		/// <summary>
		/// Gets a byte value from the data reader.
		/// </summary>
		/// <param name="name">Name of the column containing the value.</param>
		/// <returns>A <see cref="byte"/>.</returns>
		byte GetByte(string name);

		/// <summary>
		/// Gets a byte value from the data reader.
		/// </summary>
		/// <param name="name">Name of the column containing the value.</param>
		/// <param name="defaultValue">Default return value if the data reader field is <see cref="DBNull"/>.</param>
		/// <returns>A <see cref="byte"/>.</returns>
		byte GetByte(string name, byte defaultValue);

		#endregion

		#region GetByteNullable

		/// <summary>
		/// Gets a nullable byte value from the data reader.
		/// </summary>
		/// <param name="index">Ordinal index of the column holding the value.</param>
		/// <returns>A <see cref="Nullable"/> <see cref="byte"/>.</returns>
		byte? GetByteNullable(int index);

		/// <summary>
		/// Gets a nullable byte value from the data reader.
		/// </summary>
		/// <param name="index">Ordinal index of the column holding the value.</param>
		/// <param name="defaultValue">Default return value if the data reader field is <see cref="DBNull"/>.</param>
		/// <returns>A <see cref="Nullable"/> <see cref="byte"/>.</returns>
		byte? GetByteNullable(int index, byte? defaultValue);

		/// <summary>
		/// Gets a nullable byte value from the data reader.
		/// </summary>
		/// <param name="name">Name of the column containing the value.</param>
		/// <returns>A <see cref="Nullable"/> <see cref="byte"/>.</returns>
		byte? GetByteNullable(string name);

		/// <summary>
		/// Gets a nullable byte value from the data reader.
		/// </summary>
		/// <param name="name">Name of the column containing the value.</param>
		/// <param name="defaultValue">Default return value if the data reader field is <see cref="DBNull"/>.</param>
		/// <returns>A <see cref="Nullable"/> <see cref="byte"/>.</returns>
		byte? GetByteNullable(string name, byte? defaultValue);

		#endregion

		#region GetBytes

		/// <summary>
		/// Reads a stream of bytes from the specified column offset into the buffer as an array, starting at the given buffer offset.
		/// </summary>
		/// <param name="name">Name of the column.</param>
		/// <param name="fieldOffset">The index within the row from which to start the read operation.</param>
		/// <param name="buffer">The buffer into which to read the stream of bytes.</param>
		/// <param name="bufferOffset">The index for buffer to start the read operation.</param>
		/// <param name="length">The number of bytes to read.</param>
		/// <returns>The actual number of characters read.</returns>
		long GetBytes(string name, long fieldOffset, byte[] buffer, int bufferOffset, int length);

		#endregion

		#region GetChar

		/// <summary>
		/// Gets a char value from the data reader.
		/// </summary>
		/// <param name="index">Ordinal index of the column holding the value.</param>
		/// <param name="defaultValue">Default return value if the data reader field is <see cref="DBNull"/>.</param>
		/// <returns>A <see cref="char"/>.</returns>
		char GetChar(int index, char defaultValue);

		/// <summary>
		/// Gets a char value from the data reader.
		/// </summary>
		/// <param name="name">Name of the column containing the value.</param>
		/// <returns>A <see cref="char"/>.</returns>
		char GetChar(string name);

		/// <summary>
		/// Gets a char value from the data reader.
		/// </summary>
		/// <param name="name">Name of the column containing the value.</param>
		/// <param name="defaultValue">Default return value if the data reader field is <see cref="DBNull"/>.</param>
		/// <returns>A <see cref="char"/>.</returns>
		char GetChar(string name, char defaultValue);

		#endregion

		#region GetCharNullable

		/// <summary>
		/// Gets a nullable char value from the data reader.
		/// </summary>
		/// <param name="index">Ordinal index of the column holding the value.</param>
		/// <returns>A <see cref="Nullable"/> <see cref="char"/>.</returns>
		char? GetCharNullable(int index);

		/// <summary>
		/// Gets a nullable char value from the data reader.
		/// </summary>
		/// <param name="index">Ordinal index of the column holding the value.</param>
		/// <param name="defaultValue">Default return value if the data reader field is <see cref="DBNull"/>.</param>
		/// <returns>A <see cref="Nullable"/> <see cref="char"/>.</returns>
		char? GetCharNullable(int index, char? defaultValue);

		/// <summary>
		/// Gets a nullable char value from the data reader.
		/// </summary>
		/// <param name="name">Name of the column containing the value.</param>
		/// <returns>A <see cref="Nullable"/> <see cref="char"/>.</returns>
		char? GetCharNullable(string name);

		/// <summary>
		/// Gets a nullable char value from the data reader.
		/// </summary>
		/// <param name="name">Name of the column containing the value.</param>
		/// <param name="defaultValue">Default return value if the data reader field is <see cref="DBNull"/>.</param>
		/// <returns>A <see cref="Nullable"/> <see cref="char"/>.</returns>
		char? GetCharNullable(string name, char? defaultValue);

		#endregion

		#region GetChars

		/// <summary>
		/// Reads a stream of characters from the specified column offset into the buffer as an array, starting at the given buffer offset.
		/// </summary>
		/// <param name="name">Name of the column.</param>
		/// <param name="fieldOffset">The index within the row from which to start the read operation.</param>
		/// <param name="buffer">The buffer into which to read the stream of bytes.</param>
		/// <param name="bufferOffset">The index for buffer to start the read operation.</param>
		/// <param name="length">The number of bytes to read.</param>
		/// <returns>The actual number of characters read.</returns>
		long GetChars(string name, long fieldOffset, char[] buffer, int bufferOffset, int length);

		#endregion

		#region GetData

		/// <summary>
		/// Returns an <see cref="IDataReader"/> for the specified column ordinal.
		/// </summary>
		/// <param name="name">Name of the column.</param>
		/// <returns>An <see cref="IDataReader"/>.</returns>
		IDataReader GetData(string name);

		#endregion

		#region GetDataTypeName

		/// <summary>
		/// Gets the data type information for the specified column.
		/// </summary>
		/// <param name="name">Name of the column.</param>
		/// <returns>The data type information for the specified column.</returns>
		string GetDataTypeName(string name);

		#endregion

		#region GetDateTime

		/// <summary>
		/// Gets a <see cref="DateTime"/> value from the data reader.
		/// </summary>
		/// <param name="index">Ordinal index of the column holding the value.</param>
		/// <param name="defaultValue">Default return value if the data reader field is <see cref="DBNull"/>.</param>
		/// <returns>A <see cref="DateTime"/>.</returns>
		DateTime GetDateTime(int index, DateTime defaultValue);

		/// <summary>
		/// Gets a <see cref="DateTime"/> value from the data reader.
		/// </summary>
		/// <param name="name">Name of the column containing the value.</param>
		/// <returns>A <see cref="DateTime"/>.</returns>
		DateTime GetDateTime(string name);

		/// <summary>
		/// Gets a <see cref="DateTime"/> value from the data reader.
		/// </summary>
		/// <param name="name">Name of the column containing the value.</param>
		/// <param name="defaultValue">Default return value if the data reader field is <see cref="DBNull"/>.</param>
		/// <returns>A <see cref="DateTime"/>.</returns>
		DateTime GetDateTime(string name, DateTime defaultValue);

		#endregion

		#region GetDateTimeNullable

		/// <summary>
		/// Gets a nullable <see cref="DateTime"/> value from the data reader.
		/// </summary>
		/// <param name="index">Ordinal index of the column holding the value.</param>
		/// <returns>A <see cref="Nullable"/> <see cref="DateTime"/>.</returns>
		DateTime? GetDateTimeNullable(int index);

		/// <summary>
		/// Gets a nullable <see cref="DateTime"/> value from the data reader.
		/// </summary>
		/// <param name="index">Ordinal index of the column holding the value.</param>
		/// <param name="defaultValue">Default return value if the data reader field is <see cref="DBNull"/>.</param>
		/// <returns>A <see cref="Nullable"/> <see cref="DateTime"/>.</returns>
		DateTime? GetDateTimeNullable(int index, DateTime? defaultValue);

		/// <summary>
		/// Gets a nullable <see cref="DateTime"/> value from the data reader.
		/// </summary>
		/// <param name="name">Name of the column containing the value.</param>
		/// <returns>A <see cref="Nullable"/> <see cref="DateTime"/>.</returns>
		DateTime? GetDateTimeNullable(string name);

		/// <summary>
		/// Gets a nullable <see cref="DateTime"/> value from the data reader.
		/// </summary>
		/// <param name="name">Name of the column containing the value.</param>
		/// <param name="defaultValue">Default return value if the data reader field is <see cref="DBNull"/>.</param>
		/// <returns>A <see cref="Nullable"/> <see cref="DateTime"/>.</returns>
		DateTime? GetDateTimeNullable(string name, DateTime? defaultValue);

		#endregion

		#region GetDecimal

		/// <summary>
		/// Gets a decimal value from the data reader.
		/// </summary>
		/// <param name="index">Ordinal index of the column holding the value.</param>
		/// <param name="defaultValue">Default return value if the data reader field is <see cref="DBNull"/>.</param>
		/// <returns>A <see cref="decimal"/>.</returns>
		decimal GetDecimal(int index, decimal defaultValue);

		/// <summary>
		/// Gets a decimal value from the data reader.
		/// </summary>
		/// <param name="name">Name of the column containing the value.</param>
		/// <returns>A <see cref="decimal"/>.</returns>
		decimal GetDecimal(string name);

		/// <summary>
		/// Gets a decimal value from the data reader.
		/// </summary>
		/// <param name="name">Name of the column containing the value.</param>
		/// <param name="defaultValue">Default return value if the data reader field is <see cref="DBNull"/>.</param>
		/// <returns>A <see cref="decimal"/>.</returns>
		decimal GetDecimal(string name, decimal defaultValue);

		#endregion

		#region GetDecimalNullable

		/// <summary>
		/// Gets a nullable decimal value from the data reader.
		/// </summary>
		/// <param name="index">Ordinal index of the column holding the value.</param>
		/// <returns>A <see cref="Nullable"/> <see cref="decimal"/>.</returns>
		decimal? GetDecimalNullable(int index);

		/// <summary>
		/// Gets a nullable decimal value from the data reader.
		/// </summary>
		/// <param name="index">Ordinal index of the column holding the value.</param>
		/// <param name="defaultValue">Default return value if the data reader field is <see cref="DBNull"/>.</param>
		/// <returns>A <see cref="Nullable"/> <see cref="decimal"/>.</returns>
		decimal? GetDecimalNullable(int index, decimal? defaultValue);

		/// <summary>
		/// Gets a nullable decimal value from the data reader.
		/// </summary>
		/// <param name="name">Name of the column containing the value.</param>
		/// <returns>A <see cref="Nullable"/> <see cref="decimal"/>.</returns>
		decimal? GetDecimalNullable(string name);

		/// <summary>
		/// Gets a nullable decimal value from the data reader.
		/// </summary>
		/// <param name="name">Name of the column containing the value.</param>
		/// <param name="defaultValue">Default return value if the data reader field is <see cref="DBNull"/>.</param>
		/// <returns>A <see cref="Nullable"/> <see cref="decimal"/>.</returns>
		decimal? GetDecimalNullable(string name, decimal? defaultValue);

		#endregion

		#region GetDouble

		/// <summary>
		/// Gets a double value from the data reader.
		/// </summary>
		/// <param name="index">Ordinal index of the column holding the value.</param>
		/// <param name="defaultValue">Default return value if the data reader field is <see cref="DBNull"/>.</param>
		/// <returns>A <see cref="double"/>.</returns>
		double GetDouble(int index, double defaultValue);

		/// <summary>
		/// Gets a double value from the data reader.
		/// </summary>
		/// <param name="name">Name of the column containing the value.</param>
		/// <returns>A <see cref="double"/>.</returns>
		double GetDouble(string name);

		/// <summary>
		/// Gets a double value from the data reader.
		/// </summary>
		/// <param name="name">Name of the column containing the value.</param>
		/// <param name="defaultValue">Default return value if the data reader field is <see cref="DBNull"/>.</param>
		/// <returns>A <see cref="double"/>.</returns>
		double GetDouble(string name, double defaultValue);

		#endregion

		#region GetDoubleNullable

		/// <summary>
		/// Gets a nullable double value from the data reader.
		/// </summary>
		/// <param name="index">Ordinal index of the column holding the value.</param>
		/// <returns>A <see cref="Nullable"/> <see cref="double"/>.</returns>
		double? GetDoubleNullable(int index);

		/// <summary>
		/// Gets a nullable double value from the data reader.
		/// </summary>
		/// <param name="index">Ordinal index of the column holding the value.</param>
		/// <param name="defaultValue">Default return value if the data reader field is <see cref="DBNull"/>.</param>
		/// <returns>A <see cref="Nullable"/> <see cref="double"/>.</returns>
		double? GetDoubleNullable(int index, double? defaultValue);

		/// <summary>
		/// Gets a nullable double value from the data reader.
		/// </summary>
		/// <param name="name">Name of the column containing the value.</param>
		/// <returns>A <see cref="Nullable"/> <see cref="double"/>.</returns>
		double? GetDoubleNullable(string name);

		/// <summary>
		/// Gets a nullable double value from the data reader.
		/// </summary>
		/// <param name="name">Name of the column containing the value.</param>
		/// <param name="defaultValue">Default return value if the data reader field is <see cref="DBNull"/>.</param>
		/// <returns>A <see cref="Nullable"/> <see cref="double"/>.</returns>
		double? GetDoubleNullable(string name, double? defaultValue);

		#endregion

		#region GetFieldType

		/// <summary>
		/// Gets the type of a column from the data reader.
		/// </summary>
		/// <param name="name">Name of the column.</param>
		/// <returns>The <see cref="Type"/> of the column.</returns>
		Type GetFieldType(string name);

		#endregion

		#region GetFloat

		/// <summary>
		/// Gets a float value from the data reader.
		/// </summary>
		/// <param name="index">Ordinal index of the column holding the value.</param>
		/// <param name="defaultValue">Default return value if the data reader field is <see cref="DBNull"/>.</param>
		/// <returns>A <see cref="float"/>.</returns>
		float GetFloat(int index, float defaultValue);

		/// <summary>
		/// Gets a float value from the data reader.
		/// </summary>
		/// <param name="name">Name of the column containing the value.</param>
		/// <returns>A <see cref="float"/>.</returns>
		float GetFloat(string name);

		/// <summary>
		/// Gets a float value from the data reader.
		/// </summary>
		/// <param name="name">Name of the column containing the value.</param>
		/// <param name="defaultValue">Default return value if the data reader field is <see cref="DBNull"/>.</param>
		/// <returns>A <see cref="float"/>.</returns>
		float GetFloat(string name, float defaultValue);

		#endregion

		#region GetFloatNullable

		/// <summary>
		/// Gets a nullable float value from the data reader.
		/// </summary>
		/// <param name="index">Ordinal index of the column holding the value.</param>
		/// <returns>A <see cref="Nullable"/> <see cref="float"/>.</returns>
		float? GetFloatNullable(int index);

		/// <summary>
		/// Gets a nullable float value from the data reader.
		/// </summary>
		/// <param name="index">Ordinal index of the column holding the value.</param>
		/// <param name="defaultValue">Default return value if the data reader field is <see cref="DBNull"/>.</param>
		/// <returns>A <see cref="Nullable"/> <see cref="float"/>.</returns>
		float? GetFloatNullable(int index, float? defaultValue);

		/// <summary>
		/// Gets a nullable float value from the data reader.
		/// </summary>
		/// <param name="name">Name of the column containing the value.</param>
		/// <returns>A <see cref="Nullable"/> <see cref="float"/>.</returns>
		float? GetFloatNullable(string name);

		/// <summary>
		/// Gets a nullable float value from the data reader.
		/// </summary>
		/// <param name="name">Name of the column containing the value.</param>
		/// <param name="defaultValue">Default return value if the data reader field is <see cref="DBNull"/>.</param>
		/// <returns>A <see cref="Nullable"/> <see cref="float"/>.</returns>
		float? GetFloatNullable(string name, float? defaultValue);

		#endregion

		#region GetGuid

		/// <summary>
		/// Gets a <see cref="Guid"/> value from the data reader.
		/// </summary>
		/// <param name="index">Ordinal index of the column holding the value.</param>
		/// <param name="defaultValue">Default return value if the data reader field is <see cref="DBNull"/>.</param>
		/// <returns>A <see cref="Guid"/>.</returns>
		Guid GetGuid(int index, Guid defaultValue);

		/// <summary>
		/// Gets a <see cref="Guid"/> value from the data reader.
		/// </summary>
		/// <param name="name">Name of the column containing the value.</param>
		/// <returns>A <see cref="Guid"/>.</returns>
		Guid GetGuid(string name);

		/// <summary>
		/// Gets a <see cref="Guid"/> value from the data reader.
		/// </summary>
		/// <param name="name">Name of the column containing the value.</param>
		/// <param name="defaultValue">Default return value if the data reader field is <see cref="DBNull"/>.</param>
		/// <returns>A <see cref="Guid"/>.</returns>
		Guid GetGuid(string name, Guid defaultValue);

		#endregion

		#region GetGuidNullable

		/// <summary>
		/// Gets a nullable <see cref="Guid"/> value from the data reader.
		/// </summary>
		/// <param name="index">Ordinal index of the column holding the value.</param>
		/// <returns>A <see cref="Nullable"/> <see cref="Guid"/>.</returns>
		Guid? GetGuidNullable(int index);

		/// <summary>
		/// Gets a nullable <see cref="Guid"/> value from the data reader.
		/// </summary>
		/// <param name="index">Ordinal index of the column holding the value.</param>
		/// <param name="defaultValue">Default return value if the data reader field is <see cref="DBNull"/>.</param>
		/// <returns>A <see cref="Nullable"/> <see cref="Guid"/>.</returns>
		Guid? GetGuidNullable(int index, Guid? defaultValue);

		/// <summary>
		/// Gets a nullable <see cref="Guid"/> value from the data reader.
		/// </summary>
		/// <param name="name">Name of the column containing the value.</param>
		/// <returns>A <see cref="Nullable"/> <see cref="Guid"/>.</returns>
		Guid? GetGuidNullable(string name);

		/// <summary>
		/// Gets a nullable <see cref="Guid"/> value from the data reader.
		/// </summary>
		/// <param name="name">Name of the column containing the value.</param>
		/// <param name="defaultValue">Default return value if the data reader field is <see cref="DBNull"/>.</param>
		/// <returns>A <see cref="Nullable"/> <see cref="Guid"/>.</returns>
		Guid? GetGuidNullable(string name, Guid? defaultValue);

		#endregion

		#region GetInt16

		/// <summary>
		/// Gets a Int16 value from the data reader.
		/// </summary>
		/// <param name="index">Ordinal index of the column holding the value.</param>
		/// <param name="defaultValue">Default return value if the data reader field is <see cref="DBNull"/>.</param>
		/// <returns>A <see cref="Int16"/>.</returns>
		short GetInt16(int index, short defaultValue);

		/// <summary>
		/// Gets a Int16 value from the data reader.
		/// </summary>
		/// <param name="name">Name of the column containing the value.</param>
		/// <returns>A <see cref="Int16"/>.</returns>
		short GetInt16(string name);

		/// <summary>
		/// Gets a Int16 value from the data reader.
		/// </summary>
		/// <param name="name">Name of the column containing the value.</param>
		/// <param name="defaultValue">Default return value if the data reader field is <see cref="DBNull"/>.</param>
		/// <returns>A <see cref="Int16"/>.</returns>
		short GetInt16(string name, short defaultValue);

		#endregion

		#region GetInt16Nullable

		/// <summary>
		/// Gets a nullable Int16 value from the data reader.
		/// </summary>
		/// <param name="index">Ordinal index of the column holding the value.</param>
		/// <returns>A <see cref="Nullable"/> <see cref="Int16"/>.</returns>
		short? GetInt16Nullable(int index);

		/// <summary>
		/// Gets a nullable Int16 value from the data reader.
		/// </summary>
		/// <param name="index">Ordinal index of the column holding the value.</param>
		/// <param name="defaultValue">Default return value if the data reader field is <see cref="DBNull"/>.</param>
		/// <returns>A <see cref="Nullable"/> <see cref="Int16"/>.</returns>
		short? GetInt16Nullable(int index, short? defaultValue);

		/// <summary>
		/// Gets a nullable Int16 value from the data reader.
		/// </summary>
		/// <param name="name">Name of the column containing the value.</param>
		/// <returns>A <see cref="Nullable"/> <see cref="Int16"/>.</returns>
		short? GetInt16Nullable(string name);

		/// <summary>
		/// Gets a nullable Int16 value from the data reader.
		/// </summary>
		/// <param name="name">Name of the column containing the value.</param>
		/// <param name="defaultValue">Default return value if the data reader field is <see cref="DBNull"/>.</param>
		/// <returns>A <see cref="Nullable"/> <see cref="Int16"/>.</returns>
		short? GetInt16Nullable(string name, short? defaultValue);

		#endregion

		#region GetInt32

		/// <summary>
		/// Gets a Int32 value from the data reader.
		/// </summary>
		/// <param name="index">Ordinal index of the column holding the value.</param>
		/// <param name="defaultValue">Default return value if the data reader field is <see cref="DBNull"/>.</param>
		/// <returns>A <see cref="Int32"/>.</returns>
		int GetInt32(int index, int defaultValue);

		/// <summary>
		/// Gets a Int32 value from the data reader.
		/// </summary>
		/// <param name="name">Name of the column containing the value.</param>
		/// <returns>A <see cref="Int32"/>.</returns>	
		int GetInt32(string name);

		/// <summary>
		/// Gets a Int32 value from the data reader.
		/// </summary>
		/// <param name="name">Name of the column containing the value.</param>
		/// <param name="defaultValue">Default return value if the data reader field is <see cref="DBNull"/>.</param>
		/// <returns>A <see cref="Int32"/>.</returns>	
		int GetInt32(string name, int defaultValue);

		#endregion

		#region GetInt32Nullable

		/// <summary>
		/// Gets a nullable Int32 value from the data reader.
		/// </summary>
		/// <param name="index">Ordinal index of the column holding the value.</param>
		/// <returns>A <see cref="Nullable"/> <see cref="Int32"/>.</returns>
		int? GetInt32Nullable(int index);

		/// <summary>
		/// Gets a nullable Int32 value from the data reader.
		/// </summary>
		/// <param name="index">Ordinal index of the column holding the value.</param>
		/// <param name="defaultValue">Default return value if the data reader field is <see cref="DBNull"/>.</param>
		/// <returns>A <see cref="Nullable"/> <see cref="Int32"/>.</returns>	
		int? GetInt32Nullable(int index, int? defaultValue);

		/// <summary>
		/// Gets a nullable Int32 value from the data reader.
		/// </summary>
		/// <param name="name">Name of the column containing the value.</param>
		/// <returns>A <see cref="Nullable"/> <see cref="Int32"/>.</returns>	
		int? GetInt32Nullable(string name);

		/// <summary>
		/// Gets a nullable Int32 value from the data reader.
		/// </summary>
		/// <param name="name">Name of the column containing the value.</param>
		/// <param name="defaultValue">Default return value if the data reader field is <see cref="DBNull"/>.</param>
		/// <returns>A <see cref="Nullable"/> <see cref="Int32"/>.</returns>	
		int? GetInt32Nullable(string name, int? defaultValue);

		#endregion

		#region GetInt64

		/// <summary>
		/// Gets a Int64 value from the data reader.
		/// </summary>
		/// <param name="index">Ordinal index of the column holding the value.</param>
		/// <param name="defaultValue">Default return value if the data reader field is <see cref="DBNull"/>.</param>
		/// <returns>A <see cref="Int64"/>.</returns>
		long GetInt64(int index, long defaultValue);

		/// <summary>
		/// Gets a Int64 value from the data reader.
		/// </summary>
		/// <param name="name">Name of the column containing the value.</param>
		/// <returns>A <see cref="Int64"/>.</returns>		
		long GetInt64(string name);

		/// <summary>
		/// Gets a Int64 value from the data reader.
		/// </summary>
		/// <param name="name">Name of the column containing the value.</param>
		/// <param name="defaultValue">Default return value if the data reader field is <see cref="DBNull"/>.</param>
		/// <returns>A <see cref="Int64"/>.</returns>		
		long GetInt64(string name, long defaultValue);

		#endregion

		#region GetInt64Nullable

		/// <summary>
		/// Gets a nullable Int64 value from the data reader.
		/// </summary>
		/// <param name="index">Ordinal index of the column holding the value.</param>
		/// <returns>A <see cref="Nullable"/> <see cref="Int64"/>.</returns>
		long? GetInt64Nullable(int index);

		/// <summary>
		/// Gets a nullable Int64 value from the data reader.
		/// </summary>
		/// <param name="index">Ordinal index of the column holding the value.</param>
		/// <param name="defaultValue">Default return value if the data reader field is <see cref="DBNull"/>.</param>
		/// <returns>A <see cref="Nullable"/> <see cref="Int64"/>.</returns>	
		long? GetInt64Nullable(int index, long? defaultValue);

		/// <summary>
		/// Gets a nullable Int64 value from the data reader.
		/// </summary>
		/// <param name="name">Name of the column containing the value.</param>
		/// <returns>A <see cref="Nullable"/> <see cref="Int64"/>.</returns>	
		long? GetInt64Nullable(string name);

		/// <summary>
		/// Gets a nullable Int64 value from the data reader.
		/// </summary>
		/// <param name="name">Name of the column containing the value.</param>
		/// <param name="defaultValue">Default return value if the data reader field is <see cref="DBNull"/>.</param>
		/// <returns>A <see cref="Nullable"/> <see cref="Int64"/>.</returns>	
		long? GetInt64Nullable(string name, long? defaultValue);

		#endregion

		#region GetString

		/// <summary>
		/// Gets a string value from the data reader.
		/// </summary>
		/// <param name="index">Ordinal index of the column holding the value.</param>
		/// <param name="defaultValue">Default return value if the data reader field is <see cref="DBNull"/>.</param>
		/// <returns>A <see cref="string"/>.</returns>
		string GetString(int index, string defaultValue);

		/// <summary>
		/// Gets a string value from the data reader.
		/// </summary>
		/// <param name="name">Name of the column containing the value.</param>
		/// <returns>A <see cref="string"/>.</returns>		
		string GetString(string name);

		/// <summary>
		/// Gets a string value from the data reader.
		/// </summary>
		/// <param name="name">Name of the column containing the value.</param>
		/// <param name="defaultValue">Default return value if the data reader field is <see cref="DBNull"/>.</param>
		/// <returns>A <see cref="string"/>.</returns>		
		string GetString(string name, string defaultValue);

		#endregion

		#region GetStringNullable

		/// <summary>
		/// Gets a nullable string value from the data reader.
		/// </summary>
		/// <param name="index">Ordinal index of the column holding the value.</param>
		/// <returns>A <see cref="Nullable"/> <see cref="string"/>.</returns>	
		string GetStringNullable(int index);

		/// <summary>
		/// Gets a nullable string value from the data reader.
		/// </summary>
		/// <param name="index">Ordinal index of the column holding the value.</param>
		/// <param name="defaultValue">Default return value if the data reader field is <see cref="DBNull"/>.</param>
		/// <returns>A <see cref="Nullable"/> <see cref="string"/>.</returns>		
		string GetStringNullable(int index, string defaultValue);

		/// <summary>
		/// Gets a nullable string value from the data reader.
		/// </summary>
		/// <param name="name">Name of the column containing the value.</param>
		/// <returns>A <see cref="Nullable"/> <see cref="string"/>.</returns>		
		string GetStringNullable(string name);

		/// <summary>
		/// Gets a nullable string value from the data reader.
		/// </summary>
		/// <param name="name">Name of the column containing the value.</param>
		/// <param name="defaultValue">Default return value if the data reader field is <see cref="DBNull"/>.</param>
		/// <returns>A <see cref="Nullable"/> <see cref="string"/>.</returns>	
		string GetStringNullable(string name, string defaultValue);

		#endregion

		#region GetTimeSpan

		/// <summary>
		/// Gets a <see cref="TimeSpan"/> value from the data reader.
		/// </summary>
		/// <param name="index">Ordinal index of the column holding the value.</param>
		/// <returns>A <see cref="TimeSpan"/>.</returns>
		TimeSpan GetTimeSpan(int index);

		/// <summary>
		/// Gets a <see cref="TimeSpan"/> value from the data reader.
		/// </summary>
		/// <param name="index">Ordinal index of the column holding the value.</param>
		/// <param name="defaultValue">Default return value if the data reader field is <see cref="DBNull"/>.</param>
		/// <returns>A <see cref="TimeSpan"/>.</returns>		
		TimeSpan GetTimeSpan(int index, TimeSpan defaultValue);

		/// <summary>
		/// Gets a <see cref="TimeSpan"/> value from the data reader.
		/// </summary>
		/// <param name="name">Name of the column containing the value.</param>
		/// <returns>A <see cref="TimeSpan"/>.</returns>		
		TimeSpan GetTimeSpan(string name);

		/// <summary>
		/// Gets a <see cref="TimeSpan"/> value from the data reader.
		/// </summary>
		/// <param name="name">Name of the column containing the value.</param>
		/// <param name="defaultValue">Default return value if the data reader field is <see cref="DBNull"/>.</param>
		/// <returns>A <see cref="TimeSpan"/>.</returns>		
		TimeSpan GetTimeSpan(string name, TimeSpan defaultValue);

		#endregion

		#region GetTimeSpanNullable

		/// <summary>
		/// Gets a nullable <see cref="TimeSpan"/> value from the data reader.
		/// </summary>
		/// <param name="index">Ordinal index of the column holding the value.</param>
		/// <returns>A <see cref="Nullable"/> <see cref="TimeSpan"/>.</returns>
		TimeSpan? GetTimeSpanNullable(int index);

		/// <summary>
		/// Gets a nullable <see cref="TimeSpan"/> value from the data reader.
		/// </summary>
		/// <param name="index">Ordinal index of the column holding the value.</param>
		/// <param name="defaultValue">Default return value if the data reader field is <see cref="DBNull"/>.</param>
		/// <returns>A <see cref="Nullable"/> <see cref="TimeSpan"/>.</returns>		
		TimeSpan? GetTimeSpanNullable(int index, TimeSpan? defaultValue);

		/// <summary>
		/// Gets a nullable <see cref="TimeSpan"/> value from the data reader.
		/// </summary>
		/// <param name="name">Name of the column containing the value.</param>
		/// <returns>A <see cref="Nullable"/> <see cref="TimeSpan"/>.</returns>		
		TimeSpan? GetTimeSpanNullable(string name);

		/// <summary>
		/// Gets a nullable <see cref="TimeSpan"/> value from the data reader.
		/// </summary>
		/// <param name="name">Name of the column containing the value.</param>
		/// <param name="defaultValue">Default return value if the data reader field is <see cref="DBNull"/>.</param>
		/// <returns>A <see cref="Nullable"/> <see cref="TimeSpan"/>.</returns>	
		TimeSpan? GetTimeSpanNullable(string name, TimeSpan? defaultValue);

		#endregion

		#region GetTimestamp

		/// <summary>
		/// Gets an SQL Server timestamp value from the data reader.
		/// </summary>
		/// <param name="index">Ordinal index of the column holding the value.</param>
		/// <returns>A byte array.</returns>
		byte[] GetTimestamp(int index);

		/// <summary>
		/// Gets an SQL Server timestamp value from the data reader.
		/// </summary>
		/// <param name="name">Name of the column containing the value.</param>
		/// <returns>A byte array.</returns>
		byte[] GetTimestamp(string name);

		/// <summary>
		/// Gets an SQL Server timestamp value from the data reader.
		/// </summary>
		/// <param name="index">Ordinal index of the column holding the value.</param>
		/// <returns>An <see cref="Int64"/>.</returns>
		long GetTimestampInt64(int index);

		/// <summary>
		/// Gets an SQL Server timestamp value from the data reader.
		/// </summary>
		/// <param name="name">Name of the column containing the value.</param>
		/// <returns>A <see cref="Int64"/>.</returns>
		long GetTimestampInt64(string name);

		#endregion

		#region GetValue

		/// <summary>
		/// Gets an object value from the data reader.
		/// </summary>
		/// <param name="name">Name of the column containing the value.</param>
		/// <returns>An <see cref="object"/>.</returns>
		object GetValue(string name);

		#endregion
	}
}
