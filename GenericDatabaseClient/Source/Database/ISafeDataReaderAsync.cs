using System;
using System.Threading.Tasks;

namespace Database
{
	public interface ISafeDataReaderAsync
	{
		/// <summary>
		/// Asynchronously gets the data value as a type.
		/// </summary>
		/// <typeparam name="T">Type of value</typeparam>
		/// <param name="ordinal">Ordinal position of value</param>
		/// <returns></returns>
		Task<T> GetFieldValueAsync<T>(int ordinal);

		/// <summary>
		/// Asynchronously gets the data value as a type.
		/// </summary>
		/// <typeparam name="T">Type of value</typeparam>
		/// <param name="ordinal">Ordinal position of value</param>
		/// <param name="cancellationToken">Async cancellation token</param>
		Task<T> GetFieldValueAsync<T>(int ordinal, System.Threading.CancellationToken cancellationToken);

		/// <summary>
		/// Gets a value indicating whether the column has a null
		/// or missing value.
		/// </summary>
		/// <param name="ordinal">Ordinal position of value</param>
		/// <returns></returns>
		Task<bool> IsDbNullAsync(int ordinal);

		/// <summary>
		/// Gets a value indicating whether the column has a null
		/// or missing value.
		/// </summary>
		/// <param name="ordinal">Ordinal position of value</param>
		/// <param name="cancellationToken">Async cancellation token</param>
		/// <returns></returns>
		Task<bool> IsDbNullAsync(int ordinal, System.Threading.CancellationToken cancellationToken);

		/// <summary>
		/// Advances the reader to the next result.
		/// </summary>
		/// <returns></returns>
		Task<bool> NextResultAsync();

		/// <summary>
		/// Advances the reader to the next result.
		/// </summary>
		/// <param name="cancellationToken">Async cancellation token</param>
		/// <returns></returns>
		Task<bool> NextResultAsync(System.Threading.CancellationToken cancellationToken);

		/// <summary>
		/// Advances to the next record in a recordset.
		/// </summary>
		/// <returns></returns>
		Task<bool> ReadAsync();

		/// <summary>
		/// Advances to the next record in a recordset.
		/// </summary>
		/// <param name="cancellationToken">Async cancellation token</param>
		/// <returns></returns>
		Task<bool> ReadAsync(System.Threading.CancellationToken cancellationToken);

		/// <summary>
		/// Gets a boolean value from the datareader.
		/// </summary>
		/// <remarks>
		/// Returns <see langword="false" /> for null.
		/// </remarks>
		/// <param name="name">Name of the column containing the value.</param>
		Task<bool> GetBooleanAsync(string name);

		/// <summary>
		/// Gets a boolean value from the datareader.
		/// </summary>
		/// <remarks>
		/// Returns <see langword="false" /> for null.
		/// </remarks>
		/// <param name="index">Ordinal column position of the value.</param>
		Task<bool> GetBooleanAsync(int index);

		/// <summary>
		/// Gets a boolean value from the data reader.
		/// </summary>
		/// <param name="index">Ordinal index of the column holding the value.</param>
		/// <param name="defaultValue">Default return value if the data reader field is <see cref="DBNull"/>.</param>
		/// <returns>A <see cref="Boolean"/>.</returns>
		Task<bool> GetBooleanAsync(int index, bool defaultValue);

		/// <summary>
		/// Gets a boolean value from the data reader.
		/// </summary>
		/// <param name="name">Name of the column containing the value.</param>
		/// <param name="defaultValue">Default return value if the data reader field is <see cref="DBNull"/>.</param>
		/// <returns>A <see cref="Boolean"/>.</returns>
		Task<bool> GetBooleanAsync(string name, bool defaultValue);

		/// <summary>
		/// Gets a nullable boolean value from the data reader.
		/// </summary>
		/// <param name="index">Ordinal index of the column holding the value.</param>
		/// <returns>
		/// A <see cref="Nullable"/>
		/// 	<see cref="Boolean"/>.
		/// </returns>
		Task<bool?> GetBooleanNullableAsync(int index);

		/// <summary>
		/// Gets a nullable boolean value from the data reader.
		/// </summary>
		/// <param name="name">Name of the column containing the value.</param>
		/// <returns>
		/// A <see cref="Nullable"/>
		/// 	<see cref="Boolean"/>.
		/// </returns>
		Task<bool?> GetBooleanNullableAsync(string name);

		/// <summary>
		/// Gets a nullable boolean value from the data reader.
		/// </summary>
		/// <param name="index">Ordinal index of the column holding the value.</param>
		/// <param name="defaultValue">Default return value if the data reader field is <see cref="DBNull"/>.</param>
		/// <returns>
		/// A <see cref="Nullable"/>
		/// 	<see cref="Boolean"/>.
		/// </returns>
		Task<bool?> GetBooleanNullableAsync(int index, bool? defaultValue);

		/// <summary>
		/// Gets a nullable boolean value from the data reader.
		/// </summary>
		/// <param name="name">Name of the column containing the value.</param>
		/// <param name="defaultValue">Default return value if the data reader field is <see cref="DBNull"/>.</param>
		/// <returns>
		/// A <see cref="Nullable"/>
		/// 	<see cref="Boolean"/>.
		/// </returns>
		Task<bool?> GetBooleanNullableAsync(string name, bool? defaultValue);

		/// <summary>
		/// Gets a byte value from the datareader.
		/// </summary>
		/// <remarks>
		/// Returns 0 for null.
		/// </remarks>
		/// <param name="name">Name of the column containing the value.</param>
		Task<byte> GetByteAsync(string name);

		/// <summary>
		/// Gets a byte value from the datareader.
		/// </summary>
		/// <remarks>
		/// Returns 0 for null.
		/// </remarks>
		/// <param name="index">Ordinal column position of the value.</param>
		Task<byte> GetByteAsync(int index);

		/// <summary>
		/// Gets a byte value from the data reader.
		/// </summary>
		/// <param name="index">Ordinal index of the column holding the value.</param>
		/// <param name="defaultValue">Default return value if the data reader field is <see cref="DBNull"/>.</param>
		/// <returns>A <see cref="byte"/>.</returns>
		Task<byte> GetByteAsync(int index, byte defaultValue);

		/// <summary>
		/// Gets a byte value from the data reader.
		/// </summary>
		/// <param name="name">Name of the column containing the value.</param>
		/// <param name="defaultValue">Default return value if the data reader field is <see cref="DBNull"/>.</param>
		/// <returns>A <see cref="byte"/>.</returns>
		Task<byte> GetByteAsync(string name, byte defaultValue);

		/// <summary>
		/// Gets a nullable byte value from the data reader.
		/// </summary>
		/// <param name="index">Ordinal index of the column holding the value.</param>
		/// <returns>
		/// A <see cref="Nullable"/>
		/// 	<see cref="byte"/>.
		/// </returns>
		Task<byte?> GetByteNullableAsync(int index);

		/// <summary>
		/// Gets a nullable byte value from the data reader.
		/// </summary>
		/// <param name="name">Name of the column containing the value.</param>
		/// <returns>
		/// A <see cref="Nullable"/>
		/// 	<see cref="byte"/>.
		/// </returns>
		Task<byte?> GetByteNullableAsync(string name);

		/// <summary>
		/// Gets a nullable byte value from the data reader.
		/// </summary>
		/// <param name="index">Ordinal index of the column holding the value.</param>
		/// <param name="defaultValue">Default return value if the data reader field is <see cref="DBNull"/>.</param>
		/// <returns>
		/// A <see cref="Nullable"/>
		/// 	<see cref="byte"/>.
		/// </returns>
		Task<byte?> GetByteNullableAsync(int index, byte? defaultValue);

		/// <summary>
		/// Gets a nullable byte value from the data reader.
		/// </summary>
		/// <param name="name">Name of the column containing the value.</param>
		/// <param name="defaultValue">Default return value if the data reader field is <see cref="DBNull"/>.</param>
		/// <returns>
		/// A <see cref="Nullable"/>
		/// 	<see cref="byte"/>.
		/// </returns>
		byte? GetByteNullableAsync(string name, byte? defaultValue);

		/// <summary>
		/// Invokes the GetBytes method of the underlying datareader.
		/// </summary>
		/// <remarks>
		/// Returns 0 for null.
		/// </remarks>
		/// <param name="name">Name of the column containing the value.</param>
		/// <param name="buffer">Array containing the data.</param>
		/// <param name="bufferOffset">Offset position within the buffer.</param>
		/// <param name="fieldOffset">Offset position within the field.</param>
		/// <param name="length">Length of data to read.</param>
		Task<long> GetBytesAsync(string name, long fieldOffset, byte[] buffer, int bufferOffset, int length);

		/// <summary>
		/// Invokes the GetBytes method of the underlying datareader.
		/// </summary>
		/// <remarks>
		/// Returns 0 for null.
		/// </remarks>
		/// <param name="index">Ordinal column position of the value.</param>
		/// <param name="buffer">Array containing the data.</param>
		/// <param name="bufferOffset">Offset position within the buffer.</param>
		/// <param name="fieldOffset">Offset position within the field.</param>
		/// <param name="length">Length of data to read.</param>
		Task<long> GetBytesAsync(int index, long fieldOffset, byte[] buffer, int bufferOffset, int length);

		/// <summary>
		/// Gets a char value from the datareader.
		/// </summary>
		/// <remarks>
		/// Returns Char.MinValue for null.
		/// </remarks>
		/// <param name="name">Name of the column containing the value.</param>
		Task<char> GetCharAsync(string name);

		/// <summary>
		/// Gets a char value from the datareader.
		/// </summary>
		/// <remarks>
		/// Returns Char.MinValue for null.
		/// </remarks>
		/// <param name="index">Ordinal column position of the value.</param>
		Task<char> GetCharAsync(int index);

		/// <summary>
		/// Gets a char value from the data reader.
		/// </summary>
		/// <param name="index">Ordinal index of the column holding the value.</param>
		/// <param name="defaultValue">Default return value if the data reader field is <see cref="DBNull"/>.</param>
		/// <returns>A <see cref="char"/>.</returns>
		Task<char> GetCharAsync(int index, char defaultValue);

		/// <summary>
		/// Gets a char value from the data reader.
		/// </summary>
		/// <param name="name">Name of the column containing the value.</param>
		/// <param name="defaultValue">Default return value if the data reader field is <see cref="DBNull"/>.</param>
		/// <returns>A <see cref="char"/>.</returns>
		Task<char> GetCharAsync(string name, char defaultValue);

		/// <summary>
		/// Gets a nullable char value from the data reader.
		/// </summary>
		/// <param name="index">Ordinal index of the column holding the value.</param>
		/// <returns>
		/// A <see cref="Nullable"/>
		/// 	<see cref="char"/>.
		/// </returns>
		Task<char?> GetCharNullableAsync(int index);

		/// <summary>
		/// Gets a nullable char value from the data reader.
		/// </summary>
		/// <param name="name">Name of the column containing the value.</param>
		/// <returns>
		/// A <see cref="Nullable"/>
		/// 	<see cref="char"/>.
		/// </returns>
		Task<char?> GetCharNullableAsync(string name);

		/// <summary>
		/// Gets a nullable char value from the data reader.
		/// </summary>
		/// <param name="index">Ordinal index of the column holding the value.</param>
		/// <param name="defaultValue">Default return value if the data reader field is <see cref="DBNull"/>.</param>
		/// <returns>
		/// A <see cref="Nullable"/>
		/// 	<see cref="char"/>.
		/// </returns>
		Task<char?> GetCharNullableAsync(int index, char? defaultValue);

		/// <summary>
		/// Gets a nullable char value from the data reader.
		/// </summary>
		/// <param name="name">Name of the column containing the value.</param>
		/// <param name="defaultValue">Default return value if the data reader field is <see cref="DBNull"/>.</param>
		/// <returns>
		/// A <see cref="Nullable"/>
		/// 	<see cref="char"/>.
		/// </returns>
		Task<char?> GetCharNullableAsync(string name, char? defaultValue);

		/// <summary>
		/// Gets a date value from the datareader.
		/// </summary>
		/// <remarks>
		/// Returns DateTime.MinValue for null.
		/// </remarks>
		/// <param name="name">Name of the column containing the value.</param>
		Task<DateTime> GetDateTimeAsync(string name);

		/// <summary>
		/// Gets a date value from the datareader.
		/// </summary>
		/// <remarks>
		/// Returns DateTime.MinValue for null.
		/// </remarks>
		/// <param name="index">Ordinal column position of the value.</param>
		Task<DateTime> GetDateTimeAsync(int index);

		/// <summary>
		/// Gets a <see cref="DateTime"/> value from the data reader.
		/// </summary>
		/// <param name="index">Ordinal index of the column holding the value.</param>
		/// <param name="defaultValue">Default return value if the data reader field is <see cref="DBNull"/>.</param>
		/// <returns>A <see cref="DateTime"/>.</returns>
		Task<DateTime> GetDateTimeAsync(int index, DateTime defaultValue);

		/// <summary>
		/// Gets a <see cref="DateTime"/> value from the data reader.
		/// </summary>
		/// <param name="name">Name of the column containing the value.</param>
		/// <param name="defaultValue">Default return value if the data reader field is <see cref="DBNull"/>.</param>
		/// <returns>A <see cref="DateTime"/>.</returns>
		Task<DateTime> GetDateTimeAsync(string name, DateTime defaultValue);

		/// <summary>
		/// Gets a nullable <see cref="DateTime"/> value from the data reader.
		/// </summary>
		/// <param name="index">Ordinal index of the column holding the value.</param>
		/// <returns>
		/// A <see cref="Nullable"/>
		/// 	<see cref="DateTime"/>.
		/// </returns>
		Task<DateTime?> GetDateTimeNullableAsync(int index);

		/// <summary>
		/// Gets a nullable <see cref="DateTime"/> value from the data reader.
		/// </summary>
		/// <param name="name">Name of the column containing the value.</param>
		/// <returns>
		/// A <see cref="Nullable"/>
		/// 	<see cref="DateTime"/>.
		/// </returns>
		Task<DateTime?> GetDateTimeNullableAsync(string name);

		/// <summary>
		/// Gets a nullable <see cref="DateTime"/> value from the data reader.
		/// </summary>
		/// <param name="index">Ordinal index of the column holding the value.</param>
		/// <param name="defaultValue">Default return value if the data reader field is <see cref="DBNull"/>.</param>
		/// <returns>
		/// A <see cref="Nullable"/>
		/// 	<see cref="DateTime"/>.
		/// </returns>
		Task<DateTime?> GetDateTimeNullableAsync(int index, DateTime? defaultValue);

		/// <summary>
		/// Gets a nullable <see cref="DateTime"/> value from the data reader.
		/// </summary>
		/// <param name="name">Name of the column containing the value.</param>
		/// <param name="defaultValue">Default return value if the data reader field is <see cref="DBNull"/>.</param>
		/// <returns>
		/// A <see cref="Nullable"/>
		/// 	<see cref="DateTime"/>.
		/// </returns>
		Task<DateTime?> GetDateTimeNullableAsync(string name, DateTime? defaultValue);

		/// <summary>
		/// Gets a decimal value from the datareader.
		/// </summary>
		/// <remarks>
		/// Returns 0 for null.
		/// </remarks>
		/// <param name="name">Name of the column containing the value.</param>
		Task<decimal> GetDecimalAsync(string name);

		/// <summary>
		/// Gets a decimal value from the datareader.
		/// </summary>
		/// <remarks>
		/// Returns 0 for null.
		/// </remarks>
		/// <param name="index">Ordinal column position of the value.</param>
		Task<decimal> GetDecimalAsync(int index);

		/// <summary>
		/// Gets a decimal value from the data reader.
		/// </summary>
		/// <param name="index">Ordinal index of the column holding the value.</param>
		/// <param name="defaultValue">Default return value if the data reader field is <see cref="DBNull"/>.</param>
		/// <returns>A <see cref="decimal"/>.</returns>
		Task<decimal> GetDecimalAsync(int index, decimal defaultValue);

		/// <summary>
		/// Gets a decimal value from the data reader.
		/// </summary>
		/// <param name="name">Name of the column containing the value.</param>
		/// <param name="defaultValue">Default return value if the data reader field is <see cref="DBNull"/>.</param>
		/// <returns>A <see cref="decimal"/>.</returns>
		Task<decimal> GetDecimalAsync(string name, decimal defaultValue);

		/// <summary>
		/// Gets a nullable decimal value from the data reader.
		/// </summary>
		/// <param name="index">Ordinal index of the column holding the value.</param>
		/// <returns>
		/// A <see cref="Nullable"/>
		/// 	<see cref="decimal"/>.
		/// </returns>
		Task<decimal?> GetDecimalNullableAsync(int index);

		/// <summary>
		/// Gets a nullable decimal value from the data reader.
		/// </summary>
		/// <param name="name">Name of the column containing the value.</param>
		/// <returns>
		/// A <see cref="Nullable"/>
		/// 	<see cref="decimal"/>.
		/// </returns>
		Task<decimal?> GetDecimalNullableAsync(string name);

		/// <summary>
		/// Gets a nullable decimal value from the data reader.
		/// </summary>
		/// <param name="index">Ordinal index of the column holding the value.</param>
		/// <param name="defaultValue">Default return value if the data reader field is <see cref="DBNull"/>.</param>
		/// <returns>
		/// A <see cref="Nullable"/>
		/// 	<see cref="decimal"/>.
		/// </returns>
		Task<decimal?> GetDecimalNullableAsync(int index, decimal? defaultValue);

		/// <summary>
		/// Gets a nullable decimal value from the data reader.
		/// </summary>
		/// <param name="name">Name of the column containing the value.</param>
		/// <param name="defaultValue">Default return value if the data reader field is <see cref="DBNull"/>.</param>
		/// <returns>
		/// A <see cref="Nullable"/>
		/// 	<see cref="decimal"/>.
		/// </returns>
		Task<decimal?> GetDecimalNullableAsync(string name, decimal? defaultValue);

		/// <summary>
		/// Gets a double from the datareader.
		/// </summary>
		/// <remarks>
		/// Returns 0 for null.
		/// </remarks>
		/// <param name="name">Name of the column containing the value.</param>
		Task<double> GetDoubleAsync(string name);

		/// <summary>
		/// Gets a double from the datareader.
		/// </summary>
		/// <remarks>
		/// Returns 0 for null.
		/// </remarks>
		/// <param name="index">Ordinal column position of the value.</param>
		Task<double> GetDoubleAsync(int index);

		/// <summary>
		/// Gets a double value from the data reader.
		/// </summary>
		/// <param name="index">Ordinal index of the column holding the value.</param>
		/// <param name="defaultValue">Default return value if the data reader field is <see cref="DBNull"/>.</param>
		/// <returns>A <see cref="double"/>.</returns>
		Task<double> GetDoubleAsync(int index, double defaultValue);

		/// <summary>
		/// Gets a double value from the data reader.
		/// </summary>
		/// <param name="name">Name of the column containing the value.</param>
		/// <param name="defaultValue">Default return value if the data reader field is <see cref="DBNull"/>.</param>
		/// <returns>A <see cref="double"/>.</returns>
		Task<double> GetDoubleAsync(string name, double defaultValue);

		/// <summary>
		/// Gets a nullable double value from the data reader.
		/// </summary>
		/// <param name="index">Ordinal index of the column holding the value.</param>
		/// <returns>
		/// A <see cref="Nullable"/>
		/// 	<see cref="double"/>.
		/// </returns>
		Task<double?> GetDoubleNullableAsync(int index);

		/// <summary>
		/// Gets a nullable double value from the data reader.
		/// </summary>
		/// <param name="name">Name of the column containing the value.</param>
		/// <returns>
		/// A <see cref="Nullable"/>
		/// 	<see cref="double"/>.
		/// </returns>
		Task<double?> GetDoubleNullableAsync(string name);

		/// <summary>
		/// Gets a nullable double value from the data reader.
		/// </summary>
		/// <param name="index">Ordinal index of the column holding the value.</param>
		/// <param name="defaultValue">Default return value if the data reader field is <see cref="DBNull"/>.</param>
		/// <returns>
		/// A <see cref="Nullable"/>
		/// 	<see cref="double"/>.
		/// </returns>
		Task<double?> GetDoubleNullableAsync(int index, double? defaultValue);

		/// <summary>
		/// Gets a nullable double value from the data reader.
		/// </summary>
		/// <param name="name">Name of the column containing the value.</param>
		/// <param name="defaultValue">Default return value if the data reader field is <see cref="DBNull"/>.</param>
		/// <returns>
		/// A <see cref="Nullable"/>
		/// 	<see cref="double"/>.
		/// </returns>
		Task<double?> GetDoubleNullableAsync(string name, double? defaultValue);

		/// <summary>
		/// Gets a Single value from the datareader.
		/// </summary>
		/// <remarks>
		/// Returns 0 for null.
		/// </remarks>
		/// <param name="name">Name of the column containing the value.</param>
		Task<float> GetFloatAsync(string name);

		/// <summary>
		/// Gets a Single value from the datareader.
		/// </summary>
		/// <remarks>
		/// Returns 0 for null.
		/// </remarks>
		/// <param name="index">Ordinal column position of the value.</param>
		Task<float> GetFloatAsync(int index);

		/// <summary>
		/// Gets a float value from the data reader.
		/// </summary>
		/// <param name="index">Ordinal index of the column holding the value.</param>
		/// <param name="defaultValue">Default return value if the data reader field is <see cref="DBNull"/>.</param>
		/// <returns>A <see cref="float"/>.</returns>
		Task<float> GetFloatAsync(int index, float defaultValue);

		/// <summary>
		/// Gets a float value from the data reader.
		/// </summary>
		/// <param name="name">Name of the column containing the value.</param>
		/// <param name="defaultValue">Default return value if the data reader field is <see cref="DBNull"/>.</param>
		/// <returns>A <see cref="float"/>.</returns>
		Task<float> GetFloatAsync(string name, float defaultValue);

		/// <summary>
		/// Gets a nullable float value from the data reader.
		/// </summary>
		/// <param name="index">Ordinal index of the column holding the value.</param>
		/// <returns>
		/// A <see cref="Nullable"/>
		/// 	<see cref="float"/>.
		/// </returns>
		Task<float?> GetFloatNullableAsync(int index);

		/// <summary>
		/// Gets a nullable float value from the data reader.
		/// </summary>
		/// <param name="name">Name of the column containing the value.</param>
		/// <returns>
		/// A <see cref="Nullable"/>
		/// 	<see cref="float"/>.
		/// </returns>
		Task<float?> GetFloatNullableAsync(string name);

		/// <summary>
		/// Gets a nullable float value from the data reader.
		/// </summary>
		/// <param name="index">Ordinal index of the column holding the value.</param>
		/// <param name="defaultValue">Default return value if the data reader field is <see cref="DBNull"/>.</param>
		/// <returns>
		/// A <see cref="Nullable"/>
		/// 	<see cref="float"/>.
		/// </returns>
		Task<float?> GetFloatNullableAsync(int index, float? defaultValue);

		/// <summary>
		/// Gets a nullable float value from the data reader.
		/// </summary>
		/// <param name="name">Name of the column containing the value.</param>
		/// <param name="defaultValue">Default return value if the data reader field is <see cref="DBNull"/>.</param>
		/// <returns>
		/// A <see cref="Nullable"/>
		/// 	<see cref="float"/>.
		/// </returns>
		Task<float?> GetFloatNullableAsync(string name, float? defaultValue);

		/// <summary>
		/// Gets a Guid value from the datareader.
		/// </summary>
		/// <remarks>
		/// Returns Guid.Empty for null.
		/// </remarks>
		/// <param name="name">Name of the column containing the value.</param>
		Task<Guid> GetGuidAsync(string name);

		/// <summary>
		/// Gets a Guid value from the datareader.
		/// </summary>
		/// <remarks>
		/// Returns Guid.Empty for null.
		/// </remarks>
		/// <param name="index">Ordinal column position of the value.</param>
		Task<Guid> GetGuidAsync(int index);

		/// <summary>
		/// Gets a <see cref="Guid"/> value from the data reader.
		/// </summary>
		/// <param name="index">Ordinal index of the column holding the value.</param>
		/// <param name="defaultValue">Default return value if the data reader field is <see cref="DBNull"/>.</param>
		/// <returns>A <see cref="Guid"/>.</returns>
		Task<Guid> GetGuidAsync(int index, Guid defaultValue);

		/// <summary>
		/// Gets a <see cref="Guid"/> value from the data reader.
		/// </summary>
		/// <param name="name">Name of the column containing the value.</param>
		/// <param name="defaultValue">Default return value if the data reader field is <see cref="DBNull"/>.</param>
		/// <returns>A <see cref="Guid"/>.</returns>
		Task<Guid> GetGuidAsync(string name, Guid defaultValue);

		/// <summary>
		/// Gets a nullable <see cref="Guid"/> value from the data reader.
		/// </summary>
		/// <param name="index">Ordinal index of the column holding the value.</param>
		/// <returns>
		/// A <see cref="Nullable"/>
		/// 	<see cref="Guid"/>.
		/// </returns>
		Task<Guid?> GetGuidNullableAsync(int index);

		/// <summary>
		/// Gets a nullable <see cref="Guid"/> value from the data reader.
		/// </summary>
		/// <param name="name">Name of the column containing the value.</param>
		/// <returns>
		/// A <see cref="Nullable"/>
		/// 	<see cref="Guid"/>.
		/// </returns>
		Task<Guid?> GetGuidNullableAsync(string name);

		/// <summary>
		/// Gets a nullable <see cref="Guid"/> value from the data reader.
		/// </summary>
		/// <param name="index">Ordinal index of the column holding the value.</param>
		/// <param name="defaultValue">Default return value if the data reader field is <see cref="DBNull"/>.</param>
		/// <returns>
		/// A <see cref="Nullable"/>
		/// 	<see cref="Guid"/>.
		/// </returns>
		Task<Guid?> GetGuidNullableAsync(int index, Guid? defaultValue);

		/// <summary>
		/// Gets a nullable <see cref="Guid"/> value from the data reader.
		/// </summary>
		/// <param name="name">Name of the column containing the value.</param>
		/// <param name="defaultValue">Default return value if the data reader field is <see cref="DBNull"/>.</param>
		/// <returns>
		/// A <see cref="Nullable"/>
		/// 	<see cref="Guid"/>.
		/// </returns>
		Task<Guid?> GetGuidNullableAsync(string name, Guid? defaultValue);

		/// <summary>
		/// Gets a Short value from the datareader.
		/// </summary>
		/// <remarks>
		/// Returns 0 for null.
		/// </remarks>
		/// <param name="name">Name of the column containing the value.</param>
		Task<short> GetInt16Async(string name);

		/// <summary>
		/// Gets a Short value from the datareader.
		/// </summary>
		/// <remarks>
		/// Returns 0 for null.
		/// </remarks>
		/// <param name="index">Ordinal column position of the value.</param>
		Task<short> GetInt16Async(int index);

		/// <summary>
		/// Gets a Int16 value from the data reader.
		/// </summary>
		/// <param name="index">Ordinal index of the column holding the value.</param>
		/// <param name="defaultValue">Default return value if the data reader field is <see cref="DBNull"/>.</param>
		/// <returns>A <see cref="Int16"/>.</returns>
		Task<short> GetInt16Async(int index, short defaultValue);

		/// <summary>
		/// Gets a Int16 value from the data reader.
		/// </summary>
		/// <param name="name">Name of the column containing the value.</param>
		/// <param name="defaultValue">Default return value if the data reader field is <see cref="DBNull"/>.</param>
		/// <returns>A <see cref="Int16"/>.</returns>
		Task<short> GetInt16Async(string name, short defaultValue);

		/// <summary>
		/// Gets a nullable Int16 value from the data reader.
		/// </summary>
		/// <param name="index">Ordinal index of the column holding the value.</param>
		/// <returns>
		/// A <see cref="Nullable"/>
		/// 	<see cref="Int16"/>.
		/// </returns>
		Task<short?> GetInt16NullableAsync(int index);

		/// <summary>
		/// Gets a nullable Int16 value from the data reader.
		/// </summary>
		/// <param name="name">Name of the column containing the value.</param>
		/// <returns>
		/// A <see cref="Nullable"/>
		/// 	<see cref="Int16"/>.
		/// </returns>
		Task<short?> GetInt16NullableAsync(string name);

		/// <summary>
		/// Gets a nullable Int16 value from the data reader.
		/// </summary>
		/// <param name="index">Ordinal index of the column holding the value.</param>
		/// <param name="defaultValue">Default return value if the data reader field is <see cref="DBNull"/>.</param>
		/// <returns>
		/// A <see cref="Nullable"/>
		/// 	<see cref="Int16"/>.
		/// </returns>
		Task<short?> GetInt16NullableAsync(int index, short? defaultValue);

		/// <summary>
		/// Gets a nullable Int16 value from the data reader.
		/// </summary>
		/// <param name="name">Name of the column containing the value.</param>
		/// <param name="defaultValue">Default return value if the data reader field is <see cref="DBNull"/>.</param>
		/// <returns>
		/// A <see cref="Nullable"/>
		/// 	<see cref="Int16"/>.
		/// </returns>
		Task<short?> GetInt16NullableAsync(string name, short? defaultValue);

		/// <summary>
		/// Gets an integer from the datareader.
		/// </summary>
		/// <remarks>
		/// Returns 0 for null.
		/// </remarks>
		/// <param name="name">Name of the column containing the value.</param>
		Task<int> GetInt32Async(string name);

		/// <summary>
		/// Gets an integer from the datareader.
		/// </summary>
		/// <remarks>
		/// Returns 0 for null.
		/// </remarks>
		/// <param name="index">Ordinal column position of the value.</param>
		Task<int> GetInt32Async(int index);

		/// <summary>
		/// Gets a Int32 value from the data reader.
		/// </summary>
		/// <param name="index">Ordinal index of the column holding the value.</param>
		/// <param name="defaultValue">Default return value if the data reader field is <see cref="DBNull"/>.</param>
		/// <returns>A <see cref="Int32"/>.</returns>
		Task<int> GetInt32Async(int index, int defaultValue);

		/// <summary>
		/// Gets a Int32 value from the data reader.
		/// </summary>
		/// <param name="name">Name of the column containing the value.</param>
		/// <param name="defaultValue">Default return value if the data reader field is <see cref="DBNull"/>.</param>
		/// <returns>A <see cref="Int32"/>.</returns>
		Task<int> GetInt32Async(string name, int defaultValue);

		/// <summary>
		/// Gets a nullable Int32 value from the data reader.
		/// </summary>
		/// <param name="index">Ordinal index of the column holding the value.</param>
		/// <returns>
		/// A <see cref="Nullable"/>
		/// 	<see cref="Int32"/>.
		/// </returns>
		Task<int?> GetInt32NullableAsync(int index);

		/// <summary>
		/// Gets a nullable Int32 value from the data reader.
		/// </summary>
		/// <param name="name">Name of the column containing the value.</param>
		/// <returns>
		/// A <see cref="Nullable"/>
		/// 	<see cref="Int32"/>.
		/// </returns>
		Task<int?> GetInt32NullableAsync(string name);

		/// <summary>
		/// Gets a nullable Int32 value from the data reader.
		/// </summary>
		/// <param name="index">Ordinal index of the column holding the value.</param>
		/// <param name="defaultValue">Default return value if the data reader field is <see cref="DBNull"/>.</param>
		/// <returns>
		/// A <see cref="Nullable"/>
		/// 	<see cref="Int32"/>.
		/// </returns>
		Task<int?> GetInt32NullableAsync(int index, int? defaultValue);

		/// <summary>
		/// Gets a nullable Int32 value from the data reader.
		/// </summary>
		/// <param name="name">Name of the column containing the value.</param>
		/// <param name="defaultValue">Default return value if the data reader field is <see cref="DBNull"/>.</param>
		/// <returns>
		/// A <see cref="Nullable"/>
		/// 	<see cref="Int32"/>.
		/// </returns>
		Task<int?> GetInt32NullableAsync(string name, int? defaultValue);

		/// <summary>
		/// Gets a Long value from the datareader.
		/// </summary>
		/// <remarks>
		/// Returns 0 for null.
		/// </remarks>
		/// <param name="name">Name of the column containing the value.</param>
		Task<Int64> GetInt64Async(string name);

		/// <summary>
		/// Gets a Long value from the datareader.
		/// </summary>
		/// <remarks>
		/// Returns 0 for null.
		/// </remarks>
		/// <param name="index">Ordinal column position of the value.</param>
		Task<Int64> GetInt64Async(int index);

		/// <summary>
		/// Gets a Int64 value from the data reader.
		/// </summary>
		/// <param name="index">Ordinal index of the column holding the value.</param>
		/// <param name="defaultValue">Default return value if the data reader field is <see cref="DBNull"/>.</param>
		/// <returns>A <see cref="Int64"/>.</returns>
		Task<long> GetInt64Async(int index, long defaultValue);

		/// <summary>
		/// Gets a Int64 value from the data reader.
		/// </summary>
		/// <param name="name">Name of the column containing the value.</param>
		/// <param name="defaultValue">Default return value if the data reader field is <see cref="DBNull"/>.</param>
		/// <returns>A <see cref="Int64"/>.</returns>
		Task<long> GetInt64Async(string name, long defaultValue);

		/// <summary>
		/// Gets a nullable Int64 value from the data reader.
		/// </summary>
		/// <param name="index">Ordinal index of the column holding the value.</param>
		/// <returns>
		/// A <see cref="Nullable"/>
		/// 	<see cref="Int64"/>.
		/// </returns>
		Task<long?> GetInt64NullableAsync(int index);

		/// <summary>
		/// Gets a nullable Int64 value from the data reader.
		/// </summary>
		/// <param name="name">Name of the column containing the value.</param>
		/// <returns>
		/// A <see cref="Nullable"/>
		/// 	<see cref="Int64"/>.
		/// </returns>
		Task<long?> GetInt64NullableAsync(string name);

		/// <summary>
		/// Gets a nullable Int64 value from the data reader.
		/// </summary>
		/// <param name="index">Ordinal index of the column holding the value.</param>
		/// <param name="defaultValue">Default return value if the data reader field is <see cref="DBNull"/>.</param>
		/// <returns>
		/// A <see cref="Nullable"/>
		/// 	<see cref="Int64"/>.
		/// </returns>
		Task<long?> GetInt64NullableAsync(int index, long? defaultValue);

		/// <summary>
		/// Gets a nullable Int64 value from the data reader.
		/// </summary>
		/// <param name="name">Name of the column containing the value.</param>
		/// <param name="defaultValue">Default return value if the data reader field is <see cref="DBNull"/>.</param>
		/// <returns>
		/// A <see cref="Nullable"/>
		/// 	<see cref="Int64"/>.
		/// </returns>
		Task<long?> GetInt64NullableAsync(string name, long? defaultValue);

		/// <summary>
		/// Gets a string value from the datareader.
		/// </summary>
		/// <remarks>
		/// Returns empty string for null.
		/// </remarks>
		/// <param name="name">Name of the column containing the value.</param>
		Task<string> GetStringAsync(string name);

		/// <summary>
		/// Gets a string value from the datareader.
		/// </summary>
		/// <remarks>
		/// Returns empty string for null.
		/// </remarks>
		/// <param name="index">Ordinal column position of the value.</param>
		Task<string> GetStringAsync(int index);

		/// <summary>
		/// Gets a string value from the data reader.
		/// </summary>
		/// <param name="index">Ordinal index of the column holding the value.</param>
		/// <param name="defaultValue">Default return value if the data reader field is <see cref="DBNull"/>.</param>
		/// <returns>A <see cref="string"/>.</returns>
		Task<string> GetStringAsync(int index, string defaultValue);

		/// <summary>
		/// Gets a string value from the data reader.
		/// </summary>
		/// <param name="name">Name of the column containing the value.</param>
		/// <param name="defaultValue">Default return value if the data reader field is <see cref="DBNull"/>.</param>
		/// <returns>A <see cref="string"/>.</returns>
		Task<string> GetStringAsync(string name, string defaultValue);

		/// <summary>
		/// Gets a nullable string value from the data reader.
		/// </summary>
		/// <param name="index">Ordinal index of the column holding the value.</param>
		/// <returns>
		/// A <see cref="Nullable"/>
		/// 	<see cref="string"/>.
		/// </returns>
		Task<string> GetStringNullableAsync(int index);

		/// <summary>
		/// Gets a nullable string value from the data reader.
		/// </summary>
		/// <param name="name">Name of the column containing the value.</param>
		/// <returns>
		/// A <see cref="Nullable"/>
		/// 	<see cref="string"/>.
		/// </returns>
		Task<string> GetStringNullableAsync(string name);

		/// <summary>
		/// Gets a nullable string value from the data reader.
		/// </summary>
		/// <param name="index">Ordinal index of the column holding the value.</param>
		/// <param name="defaultValue">Default return value if the data reader field is <see cref="DBNull"/>.</param>
		/// <returns>
		/// A <see cref="Nullable"/>
		/// 	<see cref="string"/>.
		/// </returns>
		Task<string> GetStringNullableAsync(int index, string defaultValue);

		/// <summary>
		/// Gets a nullable string value from the data reader.
		/// </summary>
		/// <param name="name">Name of the column containing the value.</param>
		/// <param name="defaultValue">Default return value if the data reader field is <see cref="DBNull"/>.</param>
		/// <returns>
		/// A <see cref="Nullable"/>
		/// 	<see cref="string"/>.
		/// </returns>
		Task<string> GetStringNullableAsync(string name, string defaultValue);

		/// <summary>
		/// Gets a <see cref="TimeSpan"/> value from the data reader.
		/// </summary>
		/// <param name="index">Ordinal index of the column holding the value.</param>
		/// <returns>A <see cref="TimeSpan"/>.</returns>
		Task<TimeSpan> GetTimeSpanAsync(int index);

		/// <summary>
		/// Gets a <see cref="TimeSpan"/> value from the data reader.
		/// </summary>
		/// <param name="name">Name of the column containing the value.</param>
		/// <returns>A <see cref="TimeSpan"/>.</returns>
		Task<TimeSpan> GetTimeSpanAsync(string name);

		/// <summary>
		/// Gets a <see cref="TimeSpan"/> value from the data reader.
		/// </summary>
		/// <param name="index">Ordinal index of the column holding the value.</param>
		/// <param name="defaultValue">Default return value if the data reader field is <see cref="DBNull"/>.</param>
		/// <returns>A <see cref="TimeSpan"/>.</returns>
		Task<TimeSpan> GetTimeSpanAsync(int index, TimeSpan defaultValue);

		/// <summary>
		/// Gets a <see cref="TimeSpan"/> value from the data reader.
		/// </summary>
		/// <param name="name">Name of the column containing the value.</param>
		/// <param name="defaultValue">Default return value if the data reader field is <see cref="DBNull"/>.</param>
		/// <returns>A <see cref="TimeSpan"/>.</returns>
		Task<TimeSpan> GetTimeSpanAsync(string name, TimeSpan defaultValue);

		/// <summary>
		/// Gets a nullable <see cref="TimeSpan"/> value from the data reader.
		/// </summary>
		/// <param name="index">Ordinal index of the column holding the value.</param>
		/// <returns>
		/// A <see cref="Nullable"/>
		/// 	<see cref="TimeSpan"/>.
		/// </returns>
		Task<TimeSpan?> GetTimeSpanNullableAsync(int index);

		/// <summary>
		/// Gets a nullable <see cref="TimeSpan"/> value from the data reader.
		/// </summary>
		/// <param name="name">Name of the column containing the value.</param>
		/// <returns>
		/// A <see cref="Nullable"/>
		/// 	<see cref="TimeSpan"/>.
		/// </returns>
		Task<TimeSpan?> GetTimeSpanNullableAsync(string name);

		/// <summary>
		/// Gets a nullable <see cref="TimeSpan"/> value from the data reader.
		/// </summary>
		/// <param name="index">Ordinal index of the column holding the value.</param>
		/// <param name="defaultValue">Default return value if the data reader field is <see cref="DBNull"/>.</param>
		/// <returns>
		/// A <see cref="Nullable"/>
		/// 	<see cref="TimeSpan"/>.
		/// </returns>
		Task<TimeSpan?> GetTimeSpanNullableAsync(int index, TimeSpan? defaultValue);

		/// <summary>
		/// Gets a nullable <see cref="TimeSpan"/> value from the data reader.
		/// </summary>
		/// <param name="name">Name of the column containing the value.</param>
		/// <param name="defaultValue">Default return value if the data reader field is <see cref="DBNull"/>.</param>
		/// <returns>
		/// A <see cref="Nullable"/>
		/// 	<see cref="TimeSpan"/>.
		/// </returns>
		Task<TimeSpan?> GetTimeSpanNullableAsync(string name, TimeSpan? defaultValue);

		/// <summary>
		/// Gets an SQL Server timestamp value from the data reader.
		/// </summary>
		/// <param name="index">Ordinal index of the column holding the value.</param>
		/// <returns>A byte array.</returns>
		Task<byte[]> GetTimestampAsync(int index);

		/// <summary>
		/// Gets a SQL Server timestamp value from the data reader.
		/// </summary>
		/// <param name="name">Name of the column containing the value.</param>
		/// <returns>A byte array.</returns>
		Task<byte[]> GetTimestampAsync(string name);

		/// <summary>
		/// Gets an SQL Server timestamp value from the data reader.
		/// </summary>
		/// <param name="index">Ordinal index of the column holding the value.</param>
		/// <returns>An <see cref="Int64"/>.</returns>
		Task<long> GetTimestampInt64Async(int index);

		/// <summary>
		/// Gets an SQL Server timestamp value from the data reader.
		/// </summary>
		/// <param name="name">Name of the column containing the value.</param>
		/// <returns>A <see cref="Int64"/>.</returns>
		Task<long> GetTimestampInt64Async(string name);

		/// <summary>
		/// Gets a value of type <see cref="System.Object" /> from the datareader.
		/// </summary>
		/// <param name="name">Name of the column containing the value.</param>
		Task<object> GetValueAsync(string name);

		/// <summary>
		/// Gets a value of type <see cref="System.Object" /> from the datareader.
		/// </summary>
		/// <param name="index">Ordinal column position of the value.</param>
		Task<object> GetValueAsync(int index);

		/// <summary>
		/// Closes the datareader.
		/// </summary>
		void Close();
	}
}
