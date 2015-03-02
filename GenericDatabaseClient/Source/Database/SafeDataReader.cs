using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Database.Extensions;

namespace Database
{
	public class SafeDataReader : ISafeDataReader, ISafeDataReaderAsync
	{
		private readonly IDataReader _dataReader;
#if !NET40
		private readonly SqlDataReader _sqlDataReader;
#endif

		/// <summary>
		/// Get a reference to the underlying data reader
		/// object that actually contains the data from
		/// the data source.
		/// </summary>
		protected IDataReader DataReader
		{
			get { return _dataReader; }
		}

		/// <summary>
		/// Initializes the SafeDataReader object to use data from
		/// the provided DataReader object.
		/// </summary>
		/// <param name="dataReader">The source DataReader object containing the data.</param>
		public SafeDataReader(IDataReader dataReader)
		{
			_dataReader = dataReader;
#if !NET40
			_sqlDataReader = _dataReader as SqlDataReader;
#endif
		}

		#region Async Stuff: GetFieldValueAsync, IsDbNullAsync, NextResultAsync, ReadAsync
#if !NET40
		/// <summary>
		/// Asynchronously gets the data value as a type.
		/// </summary>
		/// <typeparam name="T">Type of value</typeparam>
		/// <param name="ordinal">Ordinal position of value</param>
		/// <returns></returns>
		public Task<T> GetFieldValueAsync<T>(int ordinal)
		{
			if (_sqlDataReader == null) throw new NotSupportedException("GetFieldValueAsync");
			return _sqlDataReader.GetFieldValueAsync<T>(ordinal);
		}

		/// <summary>
		/// Asynchronously gets the data value as a type.
		/// </summary>
		/// <typeparam name="T">Type of value</typeparam>
		/// <param name="ordinal">Ordinal position of value</param>
		/// <param name="cancellationToken">Async cancellation token</param>
		public Task<T> GetFieldValueAsync<T>(int ordinal, System.Threading.CancellationToken cancellationToken)
		{
			if (_sqlDataReader == null) throw new NotSupportedException("GetFieldValueAsync");
			return _sqlDataReader.GetFieldValueAsync<T>(ordinal, cancellationToken);
		}

		/// <summary>
		/// Gets a value indicating whether the column has a null
		/// or missing value.
		/// </summary>
		/// <param name="ordinal">Ordinal position of value</param>
		/// <returns></returns>
		public Task<bool> IsDbNullAsync(int ordinal)
		{
			if (_sqlDataReader == null) throw new NotSupportedException("IsDbNullAsync");
			return _sqlDataReader.IsDBNullAsync(ordinal);
		}

		/// <summary>
		/// Gets a value indicating whether the column has a null
		/// or missing value.
		/// </summary>
		/// <param name="ordinal">Ordinal position of value</param>
		/// <param name="cancellationToken">Async cancellation token</param>
		/// <returns></returns>
		public Task<bool> IsDbNullAsync(int ordinal, System.Threading.CancellationToken cancellationToken)
		{
			if (_sqlDataReader == null) throw new NotSupportedException("IsDbNullAsync");
			return _sqlDataReader.IsDBNullAsync(ordinal, cancellationToken);
		}

		/// <summary>
		/// Advances the reader to the next result.
		/// </summary>
		/// <returns></returns>
		public Task<bool> NextResultAsync()
		{
			if (_sqlDataReader == null) throw new NotSupportedException("NextResultAsync");
			return _sqlDataReader.NextResultAsync();
		}

		/// <summary>
		/// Advances the reader to the next result.
		/// </summary>
		/// <param name="cancellationToken">Async cancellation token</param>
		/// <returns></returns>
		public Task<bool> NextResultAsync(System.Threading.CancellationToken cancellationToken)
		{
			if (_sqlDataReader == null) throw new NotSupportedException("NextResultAsync");
			return _sqlDataReader.NextResultAsync(cancellationToken);
		}

		/// <summary>
		/// Advances to the next record in a recordset.
		/// </summary>
		/// <returns></returns>
		public Task<bool> ReadAsync()
		{
			if (_sqlDataReader == null) throw new NotSupportedException("NextResultAsync");
			return _sqlDataReader.ReadAsync();
		}

		/// <summary>
		/// Advances to the next record in a recordset.
		/// </summary>
		/// <param name="cancellationToken">Async cancellation token</param>
		/// <returns></returns>
		public Task<bool> ReadAsync(System.Threading.CancellationToken cancellationToken)
		{
			if (_sqlDataReader == null) throw new NotSupportedException("NextResultAsync");
			return _sqlDataReader.ReadAsync(cancellationToken);
		}
#endif
		#endregion

		#region GetBoolean
		/// <summary>
		/// Gets a boolean value from the datareader.
		/// </summary>
		/// <remarks>
		/// Returns <see langword="false" /> for null.
		/// </remarks>
		/// <param name="name">Name of the column containing the value.</param>
		public bool GetBoolean(string name)
		{
			return GetBoolean(_dataReader.GetOrdinal(name));
		}

		/// <summary>
		/// Gets a boolean value from the datareader.
		/// </summary>
		/// <remarks>
		/// Returns <see langword="false" /> for null.
		/// </remarks>
		/// <param name="i">Ordinal column position of the value.</param>
		public virtual bool GetBoolean(int i)
		{
			return !_dataReader.IsDBNull(i) && _dataReader.GetBoolean(i);
		}

		/// <summary>
		/// Gets a boolean value from the data reader.
		/// </summary>
		/// <param name="index">Ordinal index of the column holding the value.</param>
		/// <param name="defaultValue">Default return value if the data reader field is <see cref="DBNull"/>.</param>
		/// <returns>A <see cref="Boolean"/>.</returns>
		public bool GetBoolean(int index, bool defaultValue)
		{
			return IsDBNull(index) ? defaultValue : GetBoolean(index);
		}

		/// <summary>
		/// Gets a boolean value from the data reader.
		/// </summary>
		/// <param name="name">Name of the column containing the value.</param>
		/// <param name="defaultValue">Default return value if the data reader field is <see cref="DBNull"/>.</param>
		/// <returns>A <see cref="Boolean"/>.</returns>
		public bool GetBoolean(string name, bool defaultValue)
		{
			return GetBoolean(GetOrdinal(name), defaultValue);
		}

		/// <summary>
		/// Gets a nullable boolean value from the data reader.
		/// </summary>
		/// <param name="index">Ordinal index of the column holding the value.</param>
		/// <returns>
		/// A <see cref="Nullable"/>
		/// 	<see cref="Boolean"/>.
		/// </returns>
		public bool? GetBooleanNullable(int index)
		{
			return IsDBNull(index) ? null : GetBooleanNullable(index, null);
		}

		/// <summary>
		/// Gets a nullable boolean value from the data reader.
		/// </summary>
		/// <param name="name">Name of the column containing the value.</param>
		/// <returns>
		/// A <see cref="Nullable"/>
		/// 	<see cref="Boolean"/>.
		/// </returns>
		public bool? GetBooleanNullable(string name)
		{
			return GetBooleanNullable(GetOrdinal(name));
		}

		/// <summary>
		/// Gets a nullable boolean value from the data reader.
		/// </summary>
		/// <param name="index">Ordinal index of the column holding the value.</param>
		/// <param name="defaultValue">Default return value if the data reader field is <see cref="DBNull"/>.</param>
		/// <returns>
		/// A <see cref="Nullable"/>
		/// 	<see cref="Boolean"/>.
		/// </returns>
		public bool? GetBooleanNullable(int index, bool? defaultValue)
		{
			return IsDBNull(index) ? defaultValue : DataReader.GetBoolean(index);
		}

		/// <summary>
		/// Gets a nullable boolean value from the data reader.
		/// </summary>
		/// <param name="name">Name of the column containing the value.</param>
		/// <param name="defaultValue">Default return value if the data reader field is <see cref="DBNull"/>.</param>
		/// <returns>
		/// A <see cref="Nullable"/>
		/// 	<see cref="Boolean"/>.
		/// </returns>
		public bool? GetBooleanNullable(string name, bool? defaultValue)
		{
			return GetBooleanNullable(GetOrdinal(name), defaultValue);
		}
		#endregion

		#region GetByte
		/// <summary>
		/// Gets a byte value from the datareader.
		/// </summary>
		/// <remarks>
		/// Returns 0 for null.
		/// </remarks>
		/// <param name="name">Name of the column containing the value.</param>
		public byte GetByte(string name)
		{
			return GetByte(_dataReader.GetOrdinal(name));
		}

		/// <summary>
		/// Gets a byte value from the datareader.
		/// </summary>
		/// <remarks>
		/// Returns 0 for null.
		/// </remarks>
		/// <param name="i">Ordinal column position of the value.</param>
		public virtual byte GetByte(int i)
		{
			return _dataReader.IsDBNull(i) ? (byte)0 : _dataReader.GetByte(i);
		}

		/// <summary>
		/// Gets a byte value from the data reader.
		/// </summary>
		/// <param name="index">Ordinal index of the column holding the value.</param>
		/// <param name="defaultValue">Default return value if the data reader field is <see cref="DBNull"/>.</param>
		/// <returns>A <see cref="byte"/>.</returns>
		public byte GetByte(int index, byte defaultValue)
		{
			return IsDBNull(index) ? defaultValue : GetByte(index);
		}

		/// <summary>
		/// Gets a byte value from the data reader.
		/// </summary>
		/// <param name="name">Name of the column containing the value.</param>
		/// <param name="defaultValue">Default return value if the data reader field is <see cref="DBNull"/>.</param>
		/// <returns>A <see cref="byte"/>.</returns>
		public byte GetByte(string name, byte defaultValue)
		{
			return GetByte(GetOrdinal(name), defaultValue);
		}

		/// <summary>
		/// Gets a nullable byte value from the data reader.
		/// </summary>
		/// <param name="index">Ordinal index of the column holding the value.</param>
		/// <returns>
		/// A <see cref="Nullable"/>
		/// 	<see cref="byte"/>.
		/// </returns>
		public byte? GetByteNullable(int index)
		{
			return GetByteNullable(index, null);
		}

		/// <summary>
		/// Gets a nullable byte value from the data reader.
		/// </summary>
		/// <param name="name">Name of the column containing the value.</param>
		/// <returns>
		/// A <see cref="Nullable"/>
		/// 	<see cref="byte"/>.
		/// </returns>
		public byte? GetByteNullable(string name)
		{
			return GetByteNullable(GetOrdinal(name));
		}

		/// <summary>
		/// Gets a nullable byte value from the data reader.
		/// </summary>
		/// <param name="index">Ordinal index of the column holding the value.</param>
		/// <param name="defaultValue">Default return value if the data reader field is <see cref="DBNull"/>.</param>
		/// <returns>
		/// A <see cref="Nullable"/>
		/// 	<see cref="byte"/>.
		/// </returns>
		public byte? GetByteNullable(int index, byte? defaultValue)
		{
			return IsDBNull(index) ? defaultValue : DataReader.GetByte(index);
		}

		/// <summary>
		/// Gets a nullable byte value from the data reader.
		/// </summary>
		/// <param name="name">Name of the column containing the value.</param>
		/// <param name="defaultValue">Default return value if the data reader field is <see cref="DBNull"/>.</param>
		/// <returns>
		/// A <see cref="Nullable"/>
		/// 	<see cref="byte"/>.
		/// </returns>
		public byte? GetByteNullable(string name, byte? defaultValue)
		{
			return GetByteNullable(GetOrdinal(name), defaultValue);
		}

		#endregion

		#region GetBytes

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
		public long GetBytes(string name, long fieldOffset, byte[] buffer, int bufferOffset, int length)
		{
			return GetBytes(_dataReader.GetOrdinal(name), fieldOffset, buffer, bufferOffset, length);
		}

		/// <summary>
		/// Invokes the GetBytes method of the underlying datareader.
		/// </summary>
		/// <remarks>
		/// Returns 0 for null.
		/// </remarks>
		/// <param name="i">Ordinal column position of the value.</param>
		/// <param name="buffer">Array containing the data.</param>
		/// <param name="bufferoffset">Offset position within the buffer.</param>
		/// <param name="fieldOffset">Offset position within the field.</param>
		/// <param name="length">Length of data to read.</param>
		public virtual long GetBytes(int i, long fieldOffset, byte[] buffer, int bufferoffset, int length)
		{
			return _dataReader.IsDBNull(i) ? 0 : _dataReader.GetBytes(i, fieldOffset, buffer, bufferoffset, length);
		}

		#endregion

		#region GetChar

		/// <summary>
		/// Gets a char value from the datareader.
		/// </summary>
		/// <remarks>
		/// Returns Char.MinValue for null.
		/// </remarks>
		/// <param name="name">Name of the column containing the value.</param>
		public char GetChar(string name)
		{
			return GetChar(_dataReader.GetOrdinal(name));
		}

		/// <summary>
		/// Gets a char value from the datareader.
		/// </summary>
		/// <remarks>
		/// Returns Char.MinValue for null.
		/// </remarks>
		/// <param name="i">Ordinal column position of the value.</param>
		public virtual char GetChar(int i)
		{
			if (_dataReader.IsDBNull(i))
				return char.MinValue;

			var myChar = new char[1];
			_dataReader.GetChars(i, 0, myChar, 0, 1);
			return myChar[0];
		}

		/// <summary>
		/// Gets a char value from the data reader.
		/// </summary>
		/// <param name="index">Ordinal index of the column holding the value.</param>
		/// <param name="defaultValue">Default return value if the data reader field is <see cref="DBNull"/>.</param>
		/// <returns>A <see cref="char"/>.</returns>
		public char GetChar(int index, char defaultValue)
		{
			return IsDBNull(index) ? defaultValue : GetChar(index);
		}

		/// <summary>
		/// Gets a char value from the data reader.
		/// </summary>
		/// <param name="name">Name of the column containing the value.</param>
		/// <param name="defaultValue">Default return value if the data reader field is <see cref="DBNull"/>.</param>
		/// <returns>A <see cref="char"/>.</returns>
		public char GetChar(string name, char defaultValue)
		{
			return GetChar(GetOrdinal(name), defaultValue);
		}

		/// <summary>
		/// Gets a nullable char value from the data reader.
		/// </summary>
		/// <param name="index">Ordinal index of the column holding the value.</param>
		/// <returns>
		/// A <see cref="Nullable"/>
		/// 	<see cref="char"/>.
		/// </returns>
		public char? GetCharNullable(int index)
		{
			return GetCharNullable(index, null);
		}

		/// <summary>
		/// Gets a nullable char value from the data reader.
		/// </summary>
		/// <param name="name">Name of the column containing the value.</param>
		/// <returns>
		/// A <see cref="Nullable"/>
		/// 	<see cref="char"/>.
		/// </returns>
		public char? GetCharNullable(string name)
		{
			return GetCharNullable(GetOrdinal(name));
		}

		/// <summary>
		/// Gets a nullable char value from the data reader.
		/// </summary>
		/// <param name="index">Ordinal index of the column holding the value.</param>
		/// <param name="defaultValue">Default return value if the data reader field is <see cref="DBNull"/>.</param>
		/// <returns>
		/// A <see cref="Nullable"/>
		/// 	<see cref="char"/>.
		/// </returns>
		public char? GetCharNullable(int index, char? defaultValue)
		{
			return IsDBNull(index) ? defaultValue : DataReader.GetChar(index);
		}

		/// <summary>
		/// Gets a nullable char value from the data reader.
		/// </summary>
		/// <param name="name">Name of the column containing the value.</param>
		/// <param name="defaultValue">Default return value if the data reader field is <see cref="DBNull"/>.</param>
		/// <returns>
		/// A <see cref="Nullable"/>
		/// 	<see cref="char"/>.
		/// </returns>
		public char? GetCharNullable(string name, char? defaultValue)
		{
			return GetCharNullable(GetOrdinal(name), defaultValue);
		}
		#endregion

		#region GetChars
		/// <summary>
		/// Invokes the GetChars method of the underlying datareader.
		/// </summary>
		/// <remarks>
		/// Returns 0 for null.
		/// </remarks>
		/// <param name="name">Name of the column containing the value.</param>
		/// <param name="buffer">Array containing the data.</param>
		/// <param name="bufferOffset">Offset position within the buffer.</param>
		/// <param name="fieldOffset">Offset position within the field.</param>
		/// <param name="length">Length of data to read.</param>
		public Int64 GetChars(string name, Int64 fieldOffset, char[] buffer, int bufferOffset, int length)
		{
			return GetChars(_dataReader.GetOrdinal(name), fieldOffset, buffer, bufferOffset, length);
		}

		/// <summary>
		/// Invokes the GetChars method of the underlying datareader.
		/// </summary>
		/// <remarks>
		/// Returns 0 for null.
		/// </remarks>
		/// <param name="i">Ordinal column position of the value.</param>
		/// <param name="buffer">Array containing the data.</param>
		/// <param name="bufferoffset">Offset position within the buffer.</param>
		/// <param name="fieldoffset">Offset position within the field.</param>
		/// <param name="length">Length of data to read.</param>
		public virtual Int64 GetChars(int i, Int64 fieldoffset, char[] buffer, int bufferoffset, int length)
		{

			return _dataReader.IsDBNull(i) ? 0 : _dataReader.GetChars(i, fieldoffset, buffer, bufferoffset, length);
		}

		#endregion

		#region GetData
		/// <summary>
		/// Invokes the GetData method of the underlying datareader.
		/// </summary>
		/// <param name="name">Name of the column containing the value.</param>
		public IDataReader GetData(string name)
		{
			return GetData(_dataReader.GetOrdinal(name));
		}

		/// <summary>
		/// Invokes the GetData method of the underlying datareader.
		/// </summary>
		/// <param name="i">Ordinal column position of the value.</param>
		public virtual IDataReader GetData(int i)
		{
			return _dataReader.GetData(i);
		}
		#endregion

		#region GetDataTypeName
		/// <summary>
		/// Invokes the GetDataTypeName method of the underlying datareader.
		/// </summary>
		/// <param name="name">Name of the column containing the value.</param>
		public string GetDataTypeName(string name)
		{
			return GetDataTypeName(_dataReader.GetOrdinal(name));
		}

		/// <summary>
		/// Invokes the GetDataTypeName method of the underlying datareader.
		/// </summary>
		/// <param name="i">Ordinal column position of the value.</param>
		public virtual string GetDataTypeName(int i)
		{
			return _dataReader.GetDataTypeName(i);
		}

		#endregion

		#region GetDateTime

		/// <summary>
		/// Gets a date value from the datareader.
		/// </summary>
		/// <remarks>
		/// Returns DateTime.MinValue for null.
		/// </remarks>
		/// <param name="name">Name of the column containing the value.</param>
		public virtual DateTime GetDateTime(string name)
		{
			return GetDateTime(_dataReader.GetOrdinal(name));
		}

		/// <summary>
		/// Gets a date value from the datareader.
		/// </summary>
		/// <remarks>
		/// Returns DateTime.MinValue for null.
		/// </remarks>
		/// <param name="i">Ordinal column position of the value.</param>
		public virtual DateTime GetDateTime(int i)
		{
			return _dataReader.IsDBNull(i) ? DateTime.MinValue : _dataReader.GetDateTime(i);
		}

		/// <summary>
		/// Gets a <see cref="DateTime"/> value from the data reader.
		/// </summary>
		/// <param name="index">Ordinal index of the column holding the value.</param>
		/// <param name="defaultValue">Default return value if the data reader field is <see cref="DBNull"/>.</param>
		/// <returns>A <see cref="DateTime"/>.</returns>
		public DateTime GetDateTime(int index, DateTime defaultValue)
		{
			return IsDBNull(index) ? defaultValue : GetDateTime(index);
		}

		/// <summary>
		/// Gets a <see cref="DateTime"/> value from the data reader.
		/// </summary>
		/// <param name="name">Name of the column containing the value.</param>
		/// <param name="defaultValue">Default return value if the data reader field is <see cref="DBNull"/>.</param>
		/// <returns>A <see cref="DateTime"/>.</returns>
		public DateTime GetDateTime(string name, DateTime defaultValue)
		{
			return GetDateTime(GetOrdinal(name), defaultValue);
		}

		/// <summary>
		/// Gets a nullable <see cref="DateTime"/> value from the data reader.
		/// </summary>
		/// <param name="index">Ordinal index of the column holding the value.</param>
		/// <returns>
		/// A <see cref="Nullable"/>
		/// 	<see cref="DateTime"/>.
		/// </returns>
		public DateTime? GetDateTimeNullable(int index)
		{
			return GetDateTimeNullable(index, null);
		}

		/// <summary>
		/// Gets a nullable <see cref="DateTime"/> value from the data reader.
		/// </summary>
		/// <param name="name">Name of the column containing the value.</param>
		/// <returns>
		/// A <see cref="Nullable"/>
		/// 	<see cref="DateTime"/>.
		/// </returns>
		public DateTime? GetDateTimeNullable(string name)
		{
			return GetDateTimeNullable(GetOrdinal(name));
		}

		/// <summary>
		/// Gets a nullable <see cref="DateTime"/> value from the data reader.
		/// </summary>
		/// <param name="index">Ordinal index of the column holding the value.</param>
		/// <param name="defaultValue">Default return value if the data reader field is <see cref="DBNull"/>.</param>
		/// <returns>
		/// A <see cref="Nullable"/>
		/// 	<see cref="DateTime"/>.
		/// </returns>
		public DateTime? GetDateTimeNullable(int index, DateTime? defaultValue)
		{
			return IsDBNull(index) ? defaultValue : DataReader.GetDateTime(index);
		}

		/// <summary>
		/// Gets a nullable <see cref="DateTime"/> value from the data reader.
		/// </summary>
		/// <param name="name">Name of the column containing the value.</param>
		/// <param name="defaultValue">Default return value if the data reader field is <see cref="DBNull"/>.</param>
		/// <returns>
		/// A <see cref="Nullable"/>
		/// 	<see cref="DateTime"/>.
		/// </returns>
		public DateTime? GetDateTimeNullable(string name, DateTime? defaultValue)
		{
			return GetDateTimeNullable(GetOrdinal(name), defaultValue);
		}

		#endregion

		#region GetDecimal

		/// <summary>
		/// Gets a decimal value from the datareader.
		/// </summary>
		/// <remarks>
		/// Returns 0 for null.
		/// </remarks>
		/// <param name="name">Name of the column containing the value.</param>
		public decimal GetDecimal(string name)
		{
			return GetDecimal(_dataReader.GetOrdinal(name));
		}

		/// <summary>
		/// Gets a decimal value from the datareader.
		/// </summary>
		/// <remarks>
		/// Returns 0 for null.
		/// </remarks>
		/// <param name="i">Ordinal column position of the value.</param>
		public virtual decimal GetDecimal(int i)
		{
			return _dataReader.IsDBNull(i) ? 0 : _dataReader.GetDecimal(i);
		}

		/// <summary>
		/// Gets a decimal value from the data reader.
		/// </summary>
		/// <param name="index">Ordinal index of the column holding the value.</param>
		/// <param name="defaultValue">Default return value if the data reader field is <see cref="DBNull"/>.</param>
		/// <returns>A <see cref="decimal"/>.</returns>
		public decimal GetDecimal(int index, decimal defaultValue)
		{
			return IsDBNull(index) ? defaultValue : GetDecimal(index);
		}

		/// <summary>
		/// Gets a decimal value from the data reader.
		/// </summary>
		/// <param name="name">Name of the column containing the value.</param>
		/// <param name="defaultValue">Default return value if the data reader field is <see cref="DBNull"/>.</param>
		/// <returns>A <see cref="decimal"/>.</returns>
		public decimal GetDecimal(string name, decimal defaultValue)
		{
			return GetDecimal(GetOrdinal(name), defaultValue);
		}

		/// <summary>
		/// Gets a nullable decimal value from the data reader.
		/// </summary>
		/// <param name="index">Ordinal index of the column holding the value.</param>
		/// <returns>
		/// A <see cref="Nullable"/>
		/// 	<see cref="decimal"/>.
		/// </returns>
		public decimal? GetDecimalNullable(int index)
		{
			return GetDecimalNullable(index, null);
		}

		/// <summary>
		/// Gets a nullable decimal value from the data reader.
		/// </summary>
		/// <param name="name">Name of the column containing the value.</param>
		/// <returns>
		/// A <see cref="Nullable"/>
		/// 	<see cref="decimal"/>.
		/// </returns>
		public decimal? GetDecimalNullable(string name)
		{
			return GetDecimalNullable(GetOrdinal(name));
		}

		/// <summary>
		/// Gets a nullable decimal value from the data reader.
		/// </summary>
		/// <param name="index">Ordinal index of the column holding the value.</param>
		/// <param name="defaultValue">Default return value if the data reader field is <see cref="DBNull"/>.</param>
		/// <returns>
		/// A <see cref="Nullable"/>
		/// 	<see cref="decimal"/>.
		/// </returns>
		public decimal? GetDecimalNullable(int index, decimal? defaultValue)
		{
			return IsDBNull(index) ? defaultValue : DataReader.GetDecimal(index);
		}

		/// <summary>
		/// Gets a nullable decimal value from the data reader.
		/// </summary>
		/// <param name="name">Name of the column containing the value.</param>
		/// <param name="defaultValue">Default return value if the data reader field is <see cref="DBNull"/>.</param>
		/// <returns>
		/// A <see cref="Nullable"/>
		/// 	<see cref="decimal"/>.
		/// </returns>
		public decimal? GetDecimalNullable(string name, decimal? defaultValue)
		{
			return GetDecimalNullable(GetOrdinal(name), defaultValue);
		}
		#endregion

		#region GetDouble
		/// <summary>
		/// Gets a double from the datareader.
		/// </summary>
		/// <remarks>
		/// Returns 0 for null.
		/// </remarks>
		/// <param name="name">Name of the column containing the value.</param>
		public double GetDouble(string name)
		{
			return GetDouble(_dataReader.GetOrdinal(name));
		}

		/// <summary>
		/// Gets a double from the datareader.
		/// </summary>
		/// <remarks>
		/// Returns 0 for null.
		/// </remarks>
		/// <param name="i">Ordinal column position of the value.</param>
		public virtual double GetDouble(int i)
		{
			return _dataReader.IsDBNull(i) ? 0 : _dataReader.GetDouble(i);
		}

		/// <summary>
		/// Gets a double value from the data reader.
		/// </summary>
		/// <param name="index">Ordinal index of the column holding the value.</param>
		/// <param name="defaultValue">Default return value if the data reader field is <see cref="DBNull"/>.</param>
		/// <returns>A <see cref="double"/>.</returns>
		public double GetDouble(int index, double defaultValue)
		{
			return IsDBNull(index) ? defaultValue : GetDouble(index);
		}

		/// <summary>
		/// Gets a double value from the data reader.
		/// </summary>
		/// <param name="name">Name of the column containing the value.</param>
		/// <param name="defaultValue">Default return value if the data reader field is <see cref="DBNull"/>.</param>
		/// <returns>A <see cref="double"/>.</returns>
		public double GetDouble(string name, double defaultValue)
		{
			return GetDouble(GetOrdinal(name), defaultValue);
		}

		/// <summary>
		/// Gets a nullable double value from the data reader.
		/// </summary>
		/// <param name="index">Ordinal index of the column holding the value.</param>
		/// <returns>
		/// A <see cref="Nullable"/>
		/// 	<see cref="double"/>.
		/// </returns>
		public double? GetDoubleNullable(int index)
		{
			return GetDoubleNullable(index, null);
		}

		/// <summary>
		/// Gets a nullable double value from the data reader.
		/// </summary>
		/// <param name="name">Name of the column containing the value.</param>
		/// <returns>
		/// A <see cref="Nullable"/>
		/// 	<see cref="double"/>.
		/// </returns>
		public double? GetDoubleNullable(string name)
		{
			return GetDoubleNullable(GetOrdinal(name));
		}

		/// <summary>
		/// Gets a nullable double value from the data reader.
		/// </summary>
		/// <param name="index">Ordinal index of the column holding the value.</param>
		/// <param name="defaultValue">Default return value if the data reader field is <see cref="DBNull"/>.</param>
		/// <returns>
		/// A <see cref="Nullable"/>
		/// 	<see cref="double"/>.
		/// </returns>
		public double? GetDoubleNullable(int index, double? defaultValue)
		{
			return IsDBNull(index) ? defaultValue : DataReader.GetDouble(index);
		}

		/// <summary>
		/// Gets a nullable double value from the data reader.
		/// </summary>
		/// <param name="name">Name of the column containing the value.</param>
		/// <param name="defaultValue">Default return value if the data reader field is <see cref="DBNull"/>.</param>
		/// <returns>
		/// A <see cref="Nullable"/>
		/// 	<see cref="double"/>.
		/// </returns>
		public double? GetDoubleNullable(string name, double? defaultValue)
		{
			return GetDoubleNullable(GetOrdinal(name), defaultValue);
		}
		#endregion

		#region GetFieldType
		/// <summary>
		/// Invokes the GetFieldType method of the underlying datareader.
		/// </summary>
		/// <param name="name">Name of the column containing the value.</param>
		public Type GetFieldType(string name)
		{
			return GetFieldType(_dataReader.GetOrdinal(name));
		}

		/// <summary>
		/// Invokes the GetFieldType method of the underlying datareader.
		/// </summary>
		/// <param name="i">Ordinal column position of the value.</param>
		public virtual Type GetFieldType(int i)
		{
			return _dataReader.GetFieldType(i);
		}
		#endregion

		#region GetFloat
		/// <summary>
		/// Gets a Single value from the datareader.
		/// </summary>
		/// <remarks>
		/// Returns 0 for null.
		/// </remarks>
		/// <param name="name">Name of the column containing the value.</param>
		public float GetFloat(string name)
		{
			return GetFloat(_dataReader.GetOrdinal(name));
		}

		/// <summary>
		/// Gets a Single value from the datareader.
		/// </summary>
		/// <remarks>
		/// Returns 0 for null.
		/// </remarks>
		/// <param name="i">Ordinal column position of the value.</param>
		public virtual float GetFloat(int i)
		{
			return _dataReader.IsDBNull(i) ? 0 : _dataReader.GetFloat(i);
		}

		/// <summary>
		/// Gets a float value from the data reader.
		/// </summary>
		/// <param name="index">Ordinal index of the column holding the value.</param>
		/// <param name="defaultValue">Default return value if the data reader field is <see cref="DBNull"/>.</param>
		/// <returns>A <see cref="float"/>.</returns>
		public float GetFloat(int index, float defaultValue)
		{
			return IsDBNull(index) ? defaultValue : GetFloat(index);
		}

		/// <summary>
		/// Gets a float value from the data reader.
		/// </summary>
		/// <param name="name">Name of the column containing the value.</param>
		/// <param name="defaultValue">Default return value if the data reader field is <see cref="DBNull"/>.</param>
		/// <returns>A <see cref="float"/>.</returns>
		public float GetFloat(string name, float defaultValue)
		{
			return GetFloat(GetOrdinal(name), defaultValue);
		}

		/// <summary>
		/// Gets a nullable float value from the data reader.
		/// </summary>
		/// <param name="index">Ordinal index of the column holding the value.</param>
		/// <returns>
		/// A <see cref="Nullable"/>
		/// 	<see cref="float"/>.
		/// </returns>
		public float? GetFloatNullable(int index)
		{
			return GetFloatNullable(index, null);
		}

		/// <summary>
		/// Gets a nullable float value from the data reader.
		/// </summary>
		/// <param name="name">Name of the column containing the value.</param>
		/// <returns>
		/// A <see cref="Nullable"/>
		/// 	<see cref="float"/>.
		/// </returns>
		public float? GetFloatNullable(string name)
		{
			return GetFloatNullable(GetOrdinal(name));
		}

		/// <summary>
		/// Gets a nullable float value from the data reader.
		/// </summary>
		/// <param name="index">Ordinal index of the column holding the value.</param>
		/// <param name="defaultValue">Default return value if the data reader field is <see cref="DBNull"/>.</param>
		/// <returns>
		/// A <see cref="Nullable"/>
		/// 	<see cref="float"/>.
		/// </returns>
		public float? GetFloatNullable(int index, float? defaultValue)
		{
			return IsDBNull(index) ? defaultValue : DataReader.GetFloat(index);
		}

		/// <summary>
		/// Gets a nullable float value from the data reader.
		/// </summary>
		/// <param name="name">Name of the column containing the value.</param>
		/// <param name="defaultValue">Default return value if the data reader field is <see cref="DBNull"/>.</param>
		/// <returns>
		/// A <see cref="Nullable"/>
		/// 	<see cref="float"/>.
		/// </returns>
		public float? GetFloatNullable(string name, float? defaultValue)
		{
			return GetFloatNullable(GetOrdinal(name), defaultValue);
		}
		#endregion

		#region GetGuid
		/// <summary>
		/// Gets a Guid value from the datareader.
		/// </summary>
		/// <remarks>
		/// Returns Guid.Empty for null.
		/// </remarks>
		/// <param name="name">Name of the column containing the value.</param>
		public Guid GetGuid(string name)
		{
			return GetGuid(_dataReader.GetOrdinal(name));
		}

		/// <summary>
		/// Gets a Guid value from the datareader.
		/// </summary>
		/// <remarks>
		/// Returns Guid.Empty for null.
		/// </remarks>
		/// <param name="i">Ordinal column position of the value.</param>
		public virtual Guid GetGuid(int i)
		{
			return _dataReader.IsDBNull(i) ? Guid.Empty : _dataReader.GetGuid(i);
		}

		/// <summary>
		/// Gets a <see cref="Guid"/> value from the data reader.
		/// </summary>
		/// <param name="index">Ordinal index of the column holding the value.</param>
		/// <param name="defaultValue">Default return value if the data reader field is <see cref="DBNull"/>.</param>
		/// <returns>A <see cref="Guid"/>.</returns>
		public Guid GetGuid(int index, Guid defaultValue)
		{
			return IsDBNull(index) ? defaultValue : GetGuid(index);
		}

		/// <summary>
		/// Gets a <see cref="Guid"/> value from the data reader.
		/// </summary>
		/// <param name="name">Name of the column containing the value.</param>
		/// <param name="defaultValue">Default return value if the data reader field is <see cref="DBNull"/>.</param>
		/// <returns>A <see cref="Guid"/>.</returns>
		public Guid GetGuid(string name, Guid defaultValue)
		{
			return GetGuid(GetOrdinal(name), defaultValue);
		}

		/// <summary>
		/// Gets a nullable <see cref="Guid"/> value from the data reader.
		/// </summary>
		/// <param name="index">Ordinal index of the column holding the value.</param>
		/// <returns>
		/// A <see cref="Nullable"/>
		/// 	<see cref="Guid"/>.
		/// </returns>
		public Guid? GetGuidNullable(int index)
		{
			return GetGuidNullable(index, null);
		}

		/// <summary>
		/// Gets a nullable <see cref="Guid"/> value from the data reader.
		/// </summary>
		/// <param name="name">Name of the column containing the value.</param>
		/// <returns>
		/// A <see cref="Nullable"/>
		/// 	<see cref="Guid"/>.
		/// </returns>
		public Guid? GetGuidNullable(string name)
		{
			return GetGuidNullable(GetOrdinal(name));
		}

		/// <summary>
		/// Gets a nullable <see cref="Guid"/> value from the data reader.
		/// </summary>
		/// <param name="index">Ordinal index of the column holding the value.</param>
		/// <param name="defaultValue">Default return value if the data reader field is <see cref="DBNull"/>.</param>
		/// <returns>
		/// A <see cref="Nullable"/>
		/// 	<see cref="Guid"/>.
		/// </returns>
		public Guid? GetGuidNullable(int index, Guid? defaultValue)
		{
			return IsDBNull(index) ? defaultValue : DataReader.GetGuid(index);
		}

		/// <summary>
		/// Gets a nullable <see cref="Guid"/> value from the data reader.
		/// </summary>
		/// <param name="name">Name of the column containing the value.</param>
		/// <param name="defaultValue">Default return value if the data reader field is <see cref="DBNull"/>.</param>
		/// <returns>
		/// A <see cref="Nullable"/>
		/// 	<see cref="Guid"/>.
		/// </returns>
		public Guid? GetGuidNullable(string name, Guid? defaultValue)
		{
			return GetGuidNullable(GetOrdinal(name), defaultValue);
		}
		#endregion

		#region GetInt16
		/// <summary>
		/// Gets a Short value from the datareader.
		/// </summary>
		/// <remarks>
		/// Returns 0 for null.
		/// </remarks>
		/// <param name="name">Name of the column containing the value.</param>
		public short GetInt16(string name)
		{
			return GetInt16(_dataReader.GetOrdinal(name));
		}

		/// <summary>
		/// Gets a Short value from the datareader.
		/// </summary>
		/// <remarks>
		/// Returns 0 for null.
		/// </remarks>
		/// <param name="i">Ordinal column position of the value.</param>
		public virtual short GetInt16(int i)
		{
			return _dataReader.IsDBNull(i) ? (short)0 : _dataReader.GetInt16(i);
		}

		/// <summary>
		/// Gets a Int16 value from the data reader.
		/// </summary>
		/// <param name="index">Ordinal index of the column holding the value.</param>
		/// <param name="defaultValue">Default return value if the data reader field is <see cref="DBNull"/>.</param>
		/// <returns>A <see cref="Int16"/>.</returns>
		public short GetInt16(int index, short defaultValue)
		{
			return IsDBNull(index) ? defaultValue : GetInt16(index);
		}

		/// <summary>
		/// Gets a Int16 value from the data reader.
		/// </summary>
		/// <param name="name">Name of the column containing the value.</param>
		/// <param name="defaultValue">Default return value if the data reader field is <see cref="DBNull"/>.</param>
		/// <returns>A <see cref="Int16"/>.</returns>
		public short GetInt16(string name, short defaultValue)
		{
			return GetInt16(GetOrdinal(name), defaultValue);
		}

		/// <summary>
		/// Gets a nullable Int16 value from the data reader.
		/// </summary>
		/// <param name="index">Ordinal index of the column holding the value.</param>
		/// <returns>
		/// A <see cref="Nullable"/>
		/// 	<see cref="Int16"/>.
		/// </returns>
		public short? GetInt16Nullable(int index)
		{
			return GetInt16Nullable(index, null);
		}

		/// <summary>
		/// Gets a nullable Int16 value from the data reader.
		/// </summary>
		/// <param name="name">Name of the column containing the value.</param>
		/// <returns>
		/// A <see cref="Nullable"/>
		/// 	<see cref="Int16"/>.
		/// </returns>
		public short? GetInt16Nullable(string name)
		{
			return GetInt16Nullable(GetOrdinal(name));
		}

		/// <summary>
		/// Gets a nullable Int16 value from the data reader.
		/// </summary>
		/// <param name="index">Ordinal index of the column holding the value.</param>
		/// <param name="defaultValue">Default return value if the data reader field is <see cref="DBNull"/>.</param>
		/// <returns>
		/// A <see cref="Nullable"/>
		/// 	<see cref="Int16"/>.
		/// </returns>
		public short? GetInt16Nullable(int index, short? defaultValue)
		{
			return IsDBNull(index) ? defaultValue : DataReader.GetInt16(index);
		}

		/// <summary>
		/// Gets a nullable Int16 value from the data reader.
		/// </summary>
		/// <param name="name">Name of the column containing the value.</param>
		/// <param name="defaultValue">Default return value if the data reader field is <see cref="DBNull"/>.</param>
		/// <returns>
		/// A <see cref="Nullable"/>
		/// 	<see cref="Int16"/>.
		/// </returns>
		public short? GetInt16Nullable(string name, short? defaultValue)
		{
			return GetInt16Nullable(GetOrdinal(name), defaultValue);
		}
		#endregion

		#region GetInt32
		/// <summary>
		/// Gets an integer from the datareader.
		/// </summary>
		/// <remarks>
		/// Returns 0 for null.
		/// </remarks>
		/// <param name="name">Name of the column containing the value.</param>
		public int GetInt32(string name)
		{
			return GetInt32(_dataReader.GetOrdinal(name));
		}

		/// <summary>
		/// Gets an integer from the datareader.
		/// </summary>
		/// <remarks>
		/// Returns 0 for null.
		/// </remarks>
		/// <param name="i">Ordinal column position of the value.</param>
		public virtual int GetInt32(int i)
		{
			return _dataReader.IsDBNull(i) ? 0 : _dataReader.GetInt32(i);
		}

		/// <summary>
		/// Gets a Int32 value from the data reader.
		/// </summary>
		/// <param name="index">Ordinal index of the column holding the value.</param>
		/// <param name="defaultValue">Default return value if the data reader field is <see cref="DBNull"/>.</param>
		/// <returns>A <see cref="Int32"/>.</returns>
		public int GetInt32(int index, int defaultValue)
		{
			return IsDBNull(index) ? defaultValue : GetInt32(index);
		}

		/// <summary>
		/// Gets a Int32 value from the data reader.
		/// </summary>
		/// <param name="name">Name of the column containing the value.</param>
		/// <param name="defaultValue">Default return value if the data reader field is <see cref="DBNull"/>.</param>
		/// <returns>A <see cref="Int32"/>.</returns>
		public int GetInt32(string name, int defaultValue)
		{
			return GetInt32(GetOrdinal(name), defaultValue);
		}

		/// <summary>
		/// Gets a nullable Int32 value from the data reader.
		/// </summary>
		/// <param name="index">Ordinal index of the column holding the value.</param>
		/// <returns>
		/// A <see cref="Nullable"/>
		/// 	<see cref="Int32"/>.
		/// </returns>
		public int? GetInt32Nullable(int index)
		{
			return GetInt32Nullable(index, null);
		}

		/// <summary>
		/// Gets a nullable Int32 value from the data reader.
		/// </summary>
		/// <param name="name">Name of the column containing the value.</param>
		/// <returns>
		/// A <see cref="Nullable"/>
		/// 	<see cref="Int32"/>.
		/// </returns>
		public int? GetInt32Nullable(string name)
		{
			return GetInt32Nullable(GetOrdinal(name));
		}

		/// <summary>
		/// Gets a nullable Int32 value from the data reader.
		/// </summary>
		/// <param name="index">Ordinal index of the column holding the value.</param>
		/// <param name="defaultValue">Default return value if the data reader field is <see cref="DBNull"/>.</param>
		/// <returns>
		/// A <see cref="Nullable"/>
		/// 	<see cref="Int32"/>.
		/// </returns>
		public int? GetInt32Nullable(int index, int? defaultValue)
		{
			return IsDBNull(index) ? defaultValue : DataReader.GetInt32(index);
		}

		/// <summary>
		/// Gets a nullable Int32 value from the data reader.
		/// </summary>
		/// <param name="name">Name of the column containing the value.</param>
		/// <param name="defaultValue">Default return value if the data reader field is <see cref="DBNull"/>.</param>
		/// <returns>
		/// A <see cref="Nullable"/>
		/// 	<see cref="Int32"/>.
		/// </returns>
		public int? GetInt32Nullable(string name, int? defaultValue)
		{
			return GetInt32Nullable(GetOrdinal(name), defaultValue);
		}
		#endregion

		#region GetInt64
		/// <summary>
		/// Gets a Long value from the datareader.
		/// </summary>
		/// <remarks>
		/// Returns 0 for null.
		/// </remarks>
		/// <param name="name">Name of the column containing the value.</param>
		public Int64 GetInt64(string name)
		{
			return GetInt64(_dataReader.GetOrdinal(name));
		}

		/// <summary>
		/// Gets a Long value from the datareader.
		/// </summary>
		/// <remarks>
		/// Returns 0 for null.
		/// </remarks>
		/// <param name="i">Ordinal column position of the value.</param>
		public virtual Int64 GetInt64(int i)
		{
			return _dataReader.IsDBNull(i) ? 0 : _dataReader.GetInt64(i);
		}

		/// <summary>
		/// Gets a Int64 value from the data reader.
		/// </summary>
		/// <param name="index">Ordinal index of the column holding the value.</param>
		/// <param name="defaultValue">Default return value if the data reader field is <see cref="DBNull"/>.</param>
		/// <returns>A <see cref="Int64"/>.</returns>
		public long GetInt64(int index, long defaultValue)
		{
			return IsDBNull(index) ? defaultValue : GetInt64(index);
		}

		/// <summary>
		/// Gets a Int64 value from the data reader.
		/// </summary>
		/// <param name="name">Name of the column containing the value.</param>
		/// <param name="defaultValue">Default return value if the data reader field is <see cref="DBNull"/>.</param>
		/// <returns>A <see cref="Int64"/>.</returns>
		public long GetInt64(string name, long defaultValue)
		{
			return GetInt64(GetOrdinal(name), defaultValue);
		}

		/// <summary>
		/// Gets a nullable Int64 value from the data reader.
		/// </summary>
		/// <param name="index">Ordinal index of the column holding the value.</param>
		/// <returns>
		/// A <see cref="Nullable"/>
		/// 	<see cref="Int64"/>.
		/// </returns>
		public long? GetInt64Nullable(int index)
		{
			return GetInt64Nullable(index, null);
		}

		/// <summary>
		/// Gets a nullable Int64 value from the data reader.
		/// </summary>
		/// <param name="name">Name of the column containing the value.</param>
		/// <returns>
		/// A <see cref="Nullable"/>
		/// 	<see cref="Int64"/>.
		/// </returns>
		public long? GetInt64Nullable(string name)
		{
			return GetInt64Nullable(GetOrdinal(name));
		}

		/// <summary>
		/// Gets a nullable Int64 value from the data reader.
		/// </summary>
		/// <param name="index">Ordinal index of the column holding the value.</param>
		/// <param name="defaultValue">Default return value if the data reader field is <see cref="DBNull"/>.</param>
		/// <returns>
		/// A <see cref="Nullable"/>
		/// 	<see cref="Int64"/>.
		/// </returns>
		public long? GetInt64Nullable(int index, long? defaultValue)
		{
			return IsDBNull(index) ? defaultValue : DataReader.GetInt64(index);
		}

		/// <summary>
		/// Gets a nullable Int64 value from the data reader.
		/// </summary>
		/// <param name="name">Name of the column containing the value.</param>
		/// <param name="defaultValue">Default return value if the data reader field is <see cref="DBNull"/>.</param>
		/// <returns>
		/// A <see cref="Nullable"/>
		/// 	<see cref="Int64"/>.
		/// </returns>
		public long? GetInt64Nullable(string name, long? defaultValue)
		{
			return GetInt64Nullable(GetOrdinal(name), defaultValue);
		}
		#endregion

		#region GetString
		/// <summary>
		/// Gets a string value from the datareader.
		/// </summary>
		/// <remarks>
		/// Returns empty string for null.
		/// </remarks>
		/// <param name="name">Name of the column containing the value.</param>
		public string GetString(string name)
		{
			return GetString(_dataReader.GetOrdinal(name));
		}

		/// <summary>
		/// Gets a string value from the datareader.
		/// </summary>
		/// <remarks>
		/// Returns empty string for null.
		/// </remarks>
		/// <param name="i">Ordinal column position of the value.</param>
		public virtual string GetString(int i)
		{
			return _dataReader.IsDBNull(i) ? String.Empty : _dataReader.GetString(i);
		}

		/// <summary>
		/// Gets a string value from the data reader.
		/// </summary>
		/// <param name="index">Ordinal index of the column holding the value.</param>
		/// <param name="defaultValue">Default return value if the data reader field is <see cref="DBNull"/>.</param>
		/// <returns>A <see cref="string"/>.</returns>
		public virtual string GetString(int index, string defaultValue)
		{
			return IsDBNull(index) ? defaultValue : GetString(index);
		}

		/// <summary>
		/// Gets a string value from the data reader.
		/// </summary>
		/// <param name="name">Name of the column containing the value.</param>
		/// <param name="defaultValue">Default return value if the data reader field is <see cref="DBNull"/>.</param>
		/// <returns>A <see cref="string"/>.</returns>
		public virtual string GetString(string name, string defaultValue)
		{
			return GetString(GetOrdinal(name));
		}

		/// <summary>
		/// Gets a nullable string value from the data reader.
		/// </summary>
		/// <param name="index">Ordinal index of the column holding the value.</param>
		/// <returns>
		/// A <see cref="Nullable"/>
		/// 	<see cref="string"/>.
		/// </returns>
		public virtual string GetStringNullable(int index)
		{
			return GetStringNullable(index, null);
		}

		/// <summary>
		/// Gets a nullable string value from the data reader.
		/// </summary>
		/// <param name="name">Name of the column containing the value.</param>
		/// <returns>
		/// A <see cref="Nullable"/>
		/// 	<see cref="string"/>.
		/// </returns>
		public virtual string GetStringNullable(string name)
		{
			return GetStringNullable(GetOrdinal(name));
		}

		/// <summary>
		/// Gets a nullable string value from the data reader.
		/// </summary>
		/// <param name="index">Ordinal index of the column holding the value.</param>
		/// <param name="defaultValue">Default return value if the data reader field is <see cref="DBNull"/>.</param>
		/// <returns>
		/// A <see cref="Nullable"/>
		/// 	<see cref="string"/>.
		/// </returns>
		public virtual string GetStringNullable(int index, string defaultValue)
		{
			return IsDBNull(index) ? defaultValue : DataReader.GetString(index);
		}

		/// <summary>
		/// Gets a nullable string value from the data reader.
		/// </summary>
		/// <param name="name">Name of the column containing the value.</param>
		/// <param name="defaultValue">Default return value if the data reader field is <see cref="DBNull"/>.</param>
		/// <returns>
		/// A <see cref="Nullable"/>
		/// 	<see cref="string"/>.
		/// </returns>
		public virtual string GetStringNullable(string name, string defaultValue)
		{
			return GetStringNullable(GetOrdinal(name), defaultValue);
		}
		#endregion

		#region GetTimeSpan
		/// <summary>
		/// Gets a <see cref="TimeSpan"/> value from the data reader.
		/// </summary>
		/// <param name="index">Ordinal index of the column holding the value.</param>
		/// <returns>A <see cref="TimeSpan"/>.</returns>
		public TimeSpan GetTimeSpan(int index)
		{
			var reader = DataReader as SqlDataReader;
			if (reader == null) throw new NotSupportedException();
			return reader.IsDBNull(index) ? new TimeSpan() : reader.GetTimeSpan(index);
		}

		/// <summary>
		/// Gets a <see cref="TimeSpan"/> value from the data reader.
		/// </summary>
		/// <param name="name">Name of the column containing the value.</param>
		/// <returns>A <see cref="TimeSpan"/>.</returns>
		public TimeSpan GetTimeSpan(string name)
		{
			return GetTimeSpan(GetOrdinal(name));
		}

		/// <summary>
		/// Gets a <see cref="TimeSpan"/> value from the data reader.
		/// </summary>
		/// <param name="index">Ordinal index of the column holding the value.</param>
		/// <param name="defaultValue">Default return value if the data reader field is <see cref="DBNull"/>.</param>
		/// <returns>A <see cref="TimeSpan"/>.</returns>
		public TimeSpan GetTimeSpan(int index, TimeSpan defaultValue)
		{
			return IsDBNull(index) ? defaultValue : GetTimeSpan(index);
		}

		/// <summary>
		/// Gets a <see cref="TimeSpan"/> value from the data reader.
		/// </summary>
		/// <param name="name">Name of the column containing the value.</param>
		/// <param name="defaultValue">Default return value if the data reader field is <see cref="DBNull"/>.</param>
		/// <returns>A <see cref="TimeSpan"/>.</returns>
		public TimeSpan GetTimeSpan(string name, TimeSpan defaultValue)
		{
			return GetTimeSpan(GetOrdinal(name), defaultValue);
		}

		/// <summary>
		/// Gets a nullable <see cref="TimeSpan"/> value from the data reader.
		/// </summary>
		/// <param name="index">Ordinal index of the column holding the value.</param>
		/// <returns>
		/// A <see cref="Nullable"/>
		/// 	<see cref="TimeSpan"/>.
		/// </returns>
		public TimeSpan? GetTimeSpanNullable(int index)
		{
			var reader = DataReader as SqlDataReader;
			if (reader == null) throw new NotSupportedException();
			return reader.IsDBNull(index) ? (TimeSpan?)null : reader.GetTimeSpan(index);
		}

		/// <summary>
		/// Gets a nullable <see cref="TimeSpan"/> value from the data reader.
		/// </summary>
		/// <param name="name">Name of the column containing the value.</param>
		/// <returns>
		/// A <see cref="Nullable"/>
		/// 	<see cref="TimeSpan"/>.
		/// </returns>
		public TimeSpan? GetTimeSpanNullable(string name)
		{
			return GetTimeSpanNullable(GetOrdinal(name));
		}

		/// <summary>
		/// Gets a nullable <see cref="TimeSpan"/> value from the data reader.
		/// </summary>
		/// <param name="index">Ordinal index of the column holding the value.</param>
		/// <param name="defaultValue">Default return value if the data reader field is <see cref="DBNull"/>.</param>
		/// <returns>
		/// A <see cref="Nullable"/>
		/// 	<see cref="TimeSpan"/>.
		/// </returns>
		public TimeSpan? GetTimeSpanNullable(int index, TimeSpan? defaultValue)
		{
			return IsDBNull(index) ? defaultValue : GetTimeSpan(index);
		}

		/// <summary>
		/// Gets a nullable <see cref="TimeSpan"/> value from the data reader.
		/// </summary>
		/// <param name="name">Name of the column containing the value.</param>
		/// <param name="defaultValue">Default return value if the data reader field is <see cref="DBNull"/>.</param>
		/// <returns>
		/// A <see cref="Nullable"/>
		/// 	<see cref="TimeSpan"/>.
		/// </returns>
		public TimeSpan? GetTimeSpanNullable(string name, TimeSpan? defaultValue)
		{
			var ts = GetTimeSpanNullable(name);
			return ts ?? defaultValue;
		}

		#endregion

		#region GetTimestamp
		/// <summary>
		/// Gets an SQL Server timestamp value from the data reader.
		/// </summary>
		/// <param name="index">Ordinal index of the column holding the value.</param>
		/// <returns>A byte array.</returns>
		public byte[] GetTimestamp(int index)
		{
			var b = new byte[8];
			GetBytes(index, 0, b, 0, 8);
			return b;
		}

		/// <summary>
		/// Gets a SQL Server timestamp value from the data reader.
		/// </summary>
		/// <param name="name">Name of the column containing the value.</param>
		/// <returns>A byte array.</returns>
		public byte[] GetTimestamp(string name)
		{
			return GetTimestamp(GetOrdinal(name));
		}

		/// <summary>
		/// Gets an SQL Server timestamp value from the data reader.
		/// </summary>
		/// <param name="index">Ordinal index of the column holding the value.</param>
		/// <returns>An <see cref="Int64"/>.</returns>
		public long GetTimestampInt64(int index)
		{
			var b = GetTimestamp(index);
			return b.ToLong();
		}

		/// <summary>
		/// Gets an SQL Server timestamp value from the data reader.
		/// </summary>
		/// <param name="name">Name of the column containing the value.</param>
		/// <returns>A <see cref="Int64"/>.</returns>
		public long GetTimestampInt64(string name)
		{
			return GetTimestampInt64(GetOrdinal(name));
		}
		#endregion

		#region GetValue
		/// <summary>
		/// Gets a value of type <see cref="System.Object" /> from the datareader.
		/// </summary>
		/// <param name="name">Name of the column containing the value.</param>
		public object GetValue(string name)
		{
			return GetValue(_dataReader.GetOrdinal(name));
		}

		/// <summary>
		/// Gets a value of type <see cref="System.Object" /> from the datareader.
		/// </summary>
		/// <param name="i">Ordinal column position of the value.</param>
		public virtual object GetValue(int i)
		{
			return _dataReader.IsDBNull(i) ? null : _dataReader.GetValue(i);
		}
		#endregion

#if !NET40
		#region GetBooleanAsync
		/// <summary>
		/// Gets a boolean value from the datareader.
		/// </summary>
		/// <remarks>
		/// Returns <see langword="false" /> for null.
		/// </remarks>
		/// <param name="name">Name of the column containing the value.</param>
		public async Task<bool> GetBooleanAsync(string name)
		{
			return await GetBooleanAsync(_dataReader.GetOrdinal(name));
		}

		/// <summary>
		/// Gets a boolean value from the datareader.
		/// </summary>
		/// <remarks>
		/// Returns <see langword="false" /> for null.
		/// </remarks>
		/// <param name="i">Ordinal column position of the value.</param>
		public virtual async Task<bool> GetBooleanAsync(int i)
		{
			if (_sqlDataReader == null) throw new NotSupportedException("GetBooleanAsync");
			return !(await _sqlDataReader.IsDBNullAsync(i)) && (await _sqlDataReader.GetFieldValueAsync<bool>(i));
		}

		/// <summary>
		/// Gets a boolean value from the data reader.
		/// </summary>
		/// <param name="index">Ordinal index of the column holding the value.</param>
		/// <param name="defaultValue">Default return value if the data reader field is <see cref="DBNull"/>.</param>
		/// <returns>A <see cref="Boolean"/>.</returns>
		public async Task<bool> GetBooleanAsync(int index, bool defaultValue)
		{
			return await IsDbNullAsync(index) ? defaultValue : await GetBooleanAsync(index);
		}

		/// <summary>
		/// Gets a boolean value from the data reader.
		/// </summary>
		/// <param name="name">Name of the column containing the value.</param>
		/// <param name="defaultValue">Default return value if the data reader field is <see cref="DBNull"/>.</param>
		/// <returns>A <see cref="Boolean"/>.</returns>
		public async Task<bool> GetBooleanAsync(string name, bool defaultValue)
		{
			return await GetBooleanAsync(GetOrdinal(name), defaultValue);
		}

		/// <summary>
		/// Gets a nullable boolean value from the data reader.
		/// </summary>
		/// <param name="index">Ordinal index of the column holding the value.</param>
		/// <returns>
		/// A <see cref="Nullable"/>
		/// 	<see cref="Boolean"/>.
		/// </returns>
		public async Task<bool?> GetBooleanNullableAsync(int index)
		{
			return await IsDbNullAsync(index) ? null : await GetBooleanNullableAsync(index, null);
		}

		/// <summary>
		/// Gets a nullable boolean value from the data reader.
		/// </summary>
		/// <param name="name">Name of the column containing the value.</param>
		/// <returns>
		/// A <see cref="Nullable"/>
		/// 	<see cref="Boolean"/>.
		/// </returns>
		public async Task<bool?> GetBooleanNullableAsync(string name)
		{
			return await GetBooleanNullableAsync(GetOrdinal(name));
		}

		/// <summary>
		/// Gets a nullable boolean value from the data reader.
		/// </summary>
		/// <param name="index">Ordinal index of the column holding the value.</param>
		/// <param name="defaultValue">Default return value if the data reader field is <see cref="DBNull"/>.</param>
		/// <returns>
		/// A <see cref="Nullable"/>
		/// 	<see cref="Boolean"/>.
		/// </returns>
		public async Task<bool?> GetBooleanNullableAsync(int index, bool? defaultValue)
		{
			if (_sqlDataReader == null) throw new NotSupportedException("GetBooleanNullableAsync");
			return await IsDbNullAsync(index) ? defaultValue : await _sqlDataReader.GetFieldValueAsync<bool>(index);
		}

		/// <summary>
		/// Gets a nullable boolean value from the data reader.
		/// </summary>
		/// <param name="name">Name of the column containing the value.</param>
		/// <param name="defaultValue">Default return value if the data reader field is <see cref="DBNull"/>.</param>
		/// <returns>
		/// A <see cref="Nullable"/>
		/// 	<see cref="Boolean"/>.
		/// </returns>
		public async Task<bool?> GetBooleanNullableAsync(string name, bool? defaultValue)
		{
			return await GetBooleanNullableAsync(GetOrdinal(name), defaultValue);
		}
		#endregion

		#region GetByteAsync
		/// <summary>
		/// Gets a byte value from the datareader.
		/// </summary>
		/// <remarks>
		/// Returns 0 for null.
		/// </remarks>
		/// <param name="name">Name of the column containing the value.</param>
		public async Task<byte> GetByteAsync(string name)
		{
			return await GetByteAsync(_dataReader.GetOrdinal(name));
		}

		/// <summary>
		/// Gets a byte value from the datareader.
		/// </summary>
		/// <remarks>
		/// Returns 0 for null.
		/// </remarks>
		/// <param name="i">Ordinal column position of the value.</param>
		public virtual async Task<byte> GetByteAsync(int i)
		{
			if (_sqlDataReader == null) throw new NotSupportedException("GetByteAsync");
			return await _sqlDataReader.IsDBNullAsync(i) ? (byte)0 : await _sqlDataReader.GetFieldValueAsync<byte>(i);
		}

		/// <summary>
		/// Gets a byte value from the data reader.
		/// </summary>
		/// <param name="index">Ordinal index of the column holding the value.</param>
		/// <param name="defaultValue">Default return value if the data reader field is <see cref="DBNull"/>.</param>
		/// <returns>A <see cref="byte"/>.</returns>
		public async Task<byte> GetByteAsync(int index, byte defaultValue)
		{
			return await IsDbNullAsync(index) ? defaultValue : await GetByteAsync(index);
		}

		/// <summary>
		/// Gets a byte value from the data reader.
		/// </summary>
		/// <param name="name">Name of the column containing the value.</param>
		/// <param name="defaultValue">Default return value if the data reader field is <see cref="DBNull"/>.</param>
		/// <returns>A <see cref="byte"/>.</returns>
		public async Task<byte> GetByteAsync(string name, byte defaultValue)
		{
			return await GetByteAsync(GetOrdinal(name), defaultValue);
		}

		/// <summary>
		/// Gets a nullable byte value from the data reader.
		/// </summary>
		/// <param name="index">Ordinal index of the column holding the value.</param>
		/// <returns>
		/// A <see cref="Nullable"/>
		/// 	<see cref="byte"/>.
		/// </returns>
		public async Task<byte?> GetByteNullableAsync(int index)
		{
			return await GetByteNullableAsync(index, null);
		}

		/// <summary>
		/// Gets a nullable byte value from the data reader.
		/// </summary>
		/// <param name="name">Name of the column containing the value.</param>
		/// <returns>
		/// A <see cref="Nullable"/>
		/// 	<see cref="byte"/>.
		/// </returns>
		public async Task<byte?> GetByteNullableAsync(string name)
		{
			return await GetByteNullableAsync(GetOrdinal(name));
		}

		/// <summary>
		/// Gets a nullable byte value from the data reader.
		/// </summary>
		/// <param name="index">Ordinal index of the column holding the value.</param>
		/// <param name="defaultValue">Default return value if the data reader field is <see cref="DBNull"/>.</param>
		/// <returns>
		/// A <see cref="Nullable"/>
		/// 	<see cref="byte"/>.
		/// </returns>
		public async Task<byte?> GetByteNullableAsync(int index, byte? defaultValue)
		{
			if (_sqlDataReader == null) throw new NotSupportedException("GetByteNullableAsync");
			return await IsDbNullAsync(index) ? defaultValue : await _sqlDataReader.GetFieldValueAsync<byte>(index);
		}

		/// <summary>
		/// Gets a nullable byte value from the data reader.
		/// </summary>
		/// <param name="name">Name of the column containing the value.</param>
		/// <param name="defaultValue">Default return value if the data reader field is <see cref="DBNull"/>.</param>
		/// <returns>
		/// A <see cref="Nullable"/>
		/// 	<see cref="byte"/>.
		/// </returns>
		public byte? GetByteNullableAsync(string name, byte? defaultValue)
		{
			return GetByteNullable(GetOrdinal(name), defaultValue);
		}
		#endregion

		#region GetBytesAsync
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
		public async Task<long> GetBytesAsync(string name, long fieldOffset, byte[] buffer, int bufferOffset, int length)
		{
			return await GetBytesAsync(_dataReader.GetOrdinal(name), fieldOffset, buffer, bufferOffset, length);
		}

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
		public virtual async Task<long> GetBytesAsync(int index, long fieldOffset, byte[] buffer, int bufferOffset, int length)
		{
			if (_sqlDataReader == null) throw new NotSupportedException("GetBytesAsync");
			return await _sqlDataReader.IsDBNullAsync(index) ? 0 : _dataReader.GetBytes(index, fieldOffset, buffer, bufferOffset, length);
		}
		#endregion

		#region GetCharAsync
		/// <summary>
		/// Gets a char value from the datareader.
		/// </summary>
		/// <remarks>
		/// Returns Char.MinValue for null.
		/// </remarks>
		/// <param name="name">Name of the column containing the value.</param>
		public async Task<char> GetCharAsync(string name)
		{
			return await GetCharAsync(_dataReader.GetOrdinal(name));
		}

		/// <summary>
		/// Gets a char value from the datareader.
		/// </summary>
		/// <remarks>
		/// Returns Char.MinValue for null.
		/// </remarks>
		/// <param name="index">Ordinal column position of the value.</param>
		public virtual async Task<char> GetCharAsync(int index)
		{
			if (_sqlDataReader == null) throw new NotSupportedException("GetCharAsync");
			if (await _sqlDataReader.IsDBNullAsync(index))
				return char.MinValue;

			var myChar = new char[1];
			_dataReader.GetChars(index, 0, myChar, 0, 1);
			return myChar[0];
		}

		/// <summary>
		/// Gets a char value from the data reader.
		/// </summary>
		/// <param name="index">Ordinal index of the column holding the value.</param>
		/// <param name="defaultValue">Default return value if the data reader field is <see cref="DBNull"/>.</param>
		/// <returns>A <see cref="char"/>.</returns>
		public async Task<char> GetCharAsync(int index, char defaultValue)
		{
			return await IsDbNullAsync(index) ? defaultValue : await GetCharAsync(index);
		}

		/// <summary>
		/// Gets a char value from the data reader.
		/// </summary>
		/// <param name="name">Name of the column containing the value.</param>
		/// <param name="defaultValue">Default return value if the data reader field is <see cref="DBNull"/>.</param>
		/// <returns>A <see cref="char"/>.</returns>
		public async Task<char> GetCharAsync(string name, char defaultValue)
		{
			return await GetCharAsync(GetOrdinal(name), defaultValue);
		}

		/// <summary>
		/// Gets a nullable char value from the data reader.
		/// </summary>
		/// <param name="index">Ordinal index of the column holding the value.</param>
		/// <returns>
		/// A <see cref="Nullable"/>
		/// 	<see cref="char"/>.
		/// </returns>
		public async Task<char?> GetCharNullableAsync(int index)
		{
			return await GetCharNullableAsync(index, null);
		}

		/// <summary>
		/// Gets a nullable char value from the data reader.
		/// </summary>
		/// <param name="name">Name of the column containing the value.</param>
		/// <returns>
		/// A <see cref="Nullable"/>
		/// 	<see cref="char"/>.
		/// </returns>
		public async Task<char?> GetCharNullableAsync(string name)
		{
			return await GetCharNullableAsync(GetOrdinal(name));
		}

		/// <summary>
		/// Gets a nullable char value from the data reader.
		/// </summary>
		/// <param name="index">Ordinal index of the column holding the value.</param>
		/// <param name="defaultValue">Default return value if the data reader field is <see cref="DBNull"/>.</param>
		/// <returns>
		/// A <see cref="Nullable"/>
		/// 	<see cref="char"/>.
		/// </returns>
		public async Task<char?> GetCharNullableAsync(int index, char? defaultValue)
		{
			if (_sqlDataReader == null) throw new NotSupportedException("GetCharNullableAsync");
			return await IsDbNullAsync(index) ? defaultValue : await _sqlDataReader.GetFieldValueAsync<char>(index);
		}

		/// <summary>
		/// Gets a nullable char value from the data reader.
		/// </summary>
		/// <param name="name">Name of the column containing the value.</param>
		/// <param name="defaultValue">Default return value if the data reader field is <see cref="DBNull"/>.</param>
		/// <returns>
		/// A <see cref="Nullable"/>
		/// 	<see cref="char"/>.
		/// </returns>
		public async Task<char?> GetCharNullableAsync(string name, char? defaultValue)
		{
			return await GetCharNullableAsync(GetOrdinal(name), defaultValue);
		}
		#endregion

		#region GetDateTimeAsync
		/// <summary>
		/// Gets a date value from the datareader.
		/// </summary>
		/// <remarks>
		/// Returns DateTime.MinValue for null.
		/// </remarks>
		/// <param name="name">Name of the column containing the value.</param>
		public virtual async Task<DateTime> GetDateTimeAsync(string name)
		{
			return await GetDateTimeAsync(_dataReader.GetOrdinal(name));
		}

		/// <summary>
		/// Gets a date value from the datareader.
		/// </summary>
		/// <remarks>
		/// Returns DateTime.MinValue for null.
		/// </remarks>
		/// <param name="index">Ordinal column position of the value.</param>
		public virtual async Task<DateTime> GetDateTimeAsync(int index)
		{
			if (_sqlDataReader == null) throw new NotSupportedException("GetDateTimeAsync");
			return await _sqlDataReader.IsDBNullAsync(index) ? DateTime.MinValue : await _sqlDataReader.GetFieldValueAsync<DateTime>(index);
		}

		/// <summary>
		/// Gets a <see cref="DateTime"/> value from the data reader.
		/// </summary>
		/// <param name="index">Ordinal index of the column holding the value.</param>
		/// <param name="defaultValue">Default return value if the data reader field is <see cref="DBNull"/>.</param>
		/// <returns>A <see cref="DateTime"/>.</returns>
		public async Task<DateTime> GetDateTimeAsync(int index, DateTime defaultValue)
		{
			return await IsDbNullAsync(index) ? defaultValue : await GetDateTimeAsync(index);
		}

		/// <summary>
		/// Gets a <see cref="DateTime"/> value from the data reader.
		/// </summary>
		/// <param name="name">Name of the column containing the value.</param>
		/// <param name="defaultValue">Default return value if the data reader field is <see cref="DBNull"/>.</param>
		/// <returns>A <see cref="DateTime"/>.</returns>
		public async Task<DateTime> GetDateTimeAsync(string name, DateTime defaultValue)
		{
			return await GetDateTimeAsync(GetOrdinal(name), defaultValue);
		}

		/// <summary>
		/// Gets a nullable <see cref="DateTime"/> value from the data reader.
		/// </summary>
		/// <param name="index">Ordinal index of the column holding the value.</param>
		/// <returns>
		/// A <see cref="Nullable"/>
		/// 	<see cref="DateTime"/>.
		/// </returns>
		public async Task<DateTime?> GetDateTimeNullableAsync(int index)
		{
			return await GetDateTimeNullableAsync(index, null);
		}

		/// <summary>
		/// Gets a nullable <see cref="DateTime"/> value from the data reader.
		/// </summary>
		/// <param name="name">Name of the column containing the value.</param>
		/// <returns>
		/// A <see cref="Nullable"/>
		/// 	<see cref="DateTime"/>.
		/// </returns>
		public async Task<DateTime?> GetDateTimeNullableAsync(string name)
		{
			return await GetDateTimeNullableAsync(GetOrdinal(name));
		}

		/// <summary>
		/// Gets a nullable <see cref="DateTime"/> value from the data reader.
		/// </summary>
		/// <param name="index">Ordinal index of the column holding the value.</param>
		/// <param name="defaultValue">Default return value if the data reader field is <see cref="DBNull"/>.</param>
		/// <returns>
		/// A <see cref="Nullable"/>
		/// 	<see cref="DateTime"/>.
		/// </returns>
		public async Task<DateTime?> GetDateTimeNullableAsync(int index, DateTime? defaultValue)
		{
			if (_sqlDataReader == null) throw new NotSupportedException("GetDateTimeNullableAsync");
			return await IsDbNullAsync(index) ? defaultValue : await _sqlDataReader.GetFieldValueAsync<DateTime>(index);
		}

		/// <summary>
		/// Gets a nullable <see cref="DateTime"/> value from the data reader.
		/// </summary>
		/// <param name="name">Name of the column containing the value.</param>
		/// <param name="defaultValue">Default return value if the data reader field is <see cref="DBNull"/>.</param>
		/// <returns>
		/// A <see cref="Nullable"/>
		/// 	<see cref="DateTime"/>.
		/// </returns>
		public async Task<DateTime?> GetDateTimeNullableAsync(string name, DateTime? defaultValue)
		{
			return await GetDateTimeNullableAsync(GetOrdinal(name), defaultValue);
		}
		#endregion

		#region GetDecimalAsync
		/// <summary>
		/// Gets a decimal value from the datareader.
		/// </summary>
		/// <remarks>
		/// Returns 0 for null.
		/// </remarks>
		/// <param name="name">Name of the column containing the value.</param>
		public async Task<decimal> GetDecimalAsync(string name)
		{
			return await GetDecimalAsync(_dataReader.GetOrdinal(name));
		}

		/// <summary>
		/// Gets a decimal value from the datareader.
		/// </summary>
		/// <remarks>
		/// Returns 0 for null.
		/// </remarks>
		/// <param name="index">Ordinal column position of the value.</param>
		public virtual async Task<decimal> GetDecimalAsync(int index)
		{
			if (_sqlDataReader == null) throw new NotSupportedException("GetDecimalAsync");
			return await _sqlDataReader.IsDBNullAsync(index) ? 0 : await _sqlDataReader.GetFieldValueAsync<decimal>(index);
		}

		/// <summary>
		/// Gets a decimal value from the data reader.
		/// </summary>
		/// <param name="index">Ordinal index of the column holding the value.</param>
		/// <param name="defaultValue">Default return value if the data reader field is <see cref="DBNull"/>.</param>
		/// <returns>A <see cref="decimal"/>.</returns>
		public async Task<decimal> GetDecimalAsync(int index, decimal defaultValue)
		{
			return await IsDbNullAsync(index) ? defaultValue : await GetDecimalAsync(index);
		}

		/// <summary>
		/// Gets a decimal value from the data reader.
		/// </summary>
		/// <param name="name">Name of the column containing the value.</param>
		/// <param name="defaultValue">Default return value if the data reader field is <see cref="DBNull"/>.</param>
		/// <returns>A <see cref="decimal"/>.</returns>
		public async Task<decimal> GetDecimalAsync(string name, decimal defaultValue)
		{
			return await GetDecimalAsync(GetOrdinal(name), defaultValue);
		}

		/// <summary>
		/// Gets a nullable decimal value from the data reader.
		/// </summary>
		/// <param name="index">Ordinal index of the column holding the value.</param>
		/// <returns>
		/// A <see cref="Nullable"/>
		/// 	<see cref="decimal"/>.
		/// </returns>
		public async Task<decimal?> GetDecimalNullableAsync(int index)
		{
			return await GetDecimalNullableAsync(index, null);
		}

		/// <summary>
		/// Gets a nullable decimal value from the data reader.
		/// </summary>
		/// <param name="name">Name of the column containing the value.</param>
		/// <returns>
		/// A <see cref="Nullable"/>
		/// 	<see cref="decimal"/>.
		/// </returns>
		public async Task<decimal?> GetDecimalNullableAsync(string name)
		{
			return await GetDecimalNullableAsync(GetOrdinal(name));
		}

		/// <summary>
		/// Gets a nullable decimal value from the data reader.
		/// </summary>
		/// <param name="index">Ordinal index of the column holding the value.</param>
		/// <param name="defaultValue">Default return value if the data reader field is <see cref="DBNull"/>.</param>
		/// <returns>
		/// A <see cref="Nullable"/>
		/// 	<see cref="decimal"/>.
		/// </returns>
		public async Task<decimal?> GetDecimalNullableAsync(int index, decimal? defaultValue)
		{
			if (_sqlDataReader == null) throw new NotSupportedException("GetDecimalNullableAsync");
			return await IsDbNullAsync(index) ? defaultValue : await _sqlDataReader.GetFieldValueAsync<decimal>(index);
		}

		/// <summary>
		/// Gets a nullable decimal value from the data reader.
		/// </summary>
		/// <param name="name">Name of the column containing the value.</param>
		/// <param name="defaultValue">Default return value if the data reader field is <see cref="DBNull"/>.</param>
		/// <returns>
		/// A <see cref="Nullable"/>
		/// 	<see cref="decimal"/>.
		/// </returns>
		public async Task<decimal?> GetDecimalNullableAsync(string name, decimal? defaultValue)
		{
			return await GetDecimalNullableAsync(GetOrdinal(name), defaultValue);
		}

		#endregion

		#region GetDoubleAsync
		/// <summary>
		/// Gets a double from the datareader.
		/// </summary>
		/// <remarks>
		/// Returns 0 for null.
		/// </remarks>
		/// <param name="name">Name of the column containing the value.</param>
		public async Task<double> GetDoubleAsync(string name)
		{
			return await GetDoubleAsync(_dataReader.GetOrdinal(name));
		}

		/// <summary>
		/// Gets a double from the datareader.
		/// </summary>
		/// <remarks>
		/// Returns 0 for null.
		/// </remarks>
		/// <param name="index">Ordinal column position of the value.</param>
		public virtual async Task<double> GetDoubleAsync(int index)
		{
			if (_sqlDataReader == null) throw new NotSupportedException("GetDoubleAsync");
			return await _sqlDataReader.IsDBNullAsync(index) ? 0 : await _sqlDataReader.GetFieldValueAsync<double>(index);
		}

		/// <summary>
		/// Gets a double value from the data reader.
		/// </summary>
		/// <param name="index">Ordinal index of the column holding the value.</param>
		/// <param name="defaultValue">Default return value if the data reader field is <see cref="DBNull"/>.</param>
		/// <returns>A <see cref="double"/>.</returns>
		public async Task<double> GetDoubleAsync(int index, double defaultValue)
		{
			return await IsDbNullAsync(index) ? defaultValue : await GetDoubleAsync(index);
		}

		/// <summary>
		/// Gets a double value from the data reader.
		/// </summary>
		/// <param name="name">Name of the column containing the value.</param>
		/// <param name="defaultValue">Default return value if the data reader field is <see cref="DBNull"/>.</param>
		/// <returns>A <see cref="double"/>.</returns>
		public async Task<double> GetDoubleAsync(string name, double defaultValue)
		{
			return await GetDoubleAsync(GetOrdinal(name), defaultValue);
		}

		/// <summary>
		/// Gets a nullable double value from the data reader.
		/// </summary>
		/// <param name="index">Ordinal index of the column holding the value.</param>
		/// <returns>
		/// A <see cref="Nullable"/>
		/// 	<see cref="double"/>.
		/// </returns>
		public async Task<double?> GetDoubleNullableAsync(int index)
		{
			return await GetDoubleNullableAsync(index, null);
		}

		/// <summary>
		/// Gets a nullable double value from the data reader.
		/// </summary>
		/// <param name="name">Name of the column containing the value.</param>
		/// <returns>
		/// A <see cref="Nullable"/>
		/// 	<see cref="double"/>.
		/// </returns>
		public async Task<double?> GetDoubleNullableAsync(string name)
		{
			return await GetDoubleNullableAsync(GetOrdinal(name));
		}

		/// <summary>
		/// Gets a nullable double value from the data reader.
		/// </summary>
		/// <param name="index">Ordinal index of the column holding the value.</param>
		/// <param name="defaultValue">Default return value if the data reader field is <see cref="DBNull"/>.</param>
		/// <returns>
		/// A <see cref="Nullable"/>
		/// 	<see cref="double"/>.
		/// </returns>
		public async Task<double?> GetDoubleNullableAsync(int index, double? defaultValue)
		{
			if (_sqlDataReader == null) throw new NotSupportedException("GetDoubleNullableAsync");
			return await IsDbNullAsync(index) ? defaultValue : await _sqlDataReader.GetFieldValueAsync<double>(index);
		}

		/// <summary>
		/// Gets a nullable double value from the data reader.
		/// </summary>
		/// <param name="name">Name of the column containing the value.</param>
		/// <param name="defaultValue">Default return value if the data reader field is <see cref="DBNull"/>.</param>
		/// <returns>
		/// A <see cref="Nullable"/>
		/// 	<see cref="double"/>.
		/// </returns>
		public async Task<double?> GetDoubleNullableAsync(string name, double? defaultValue)
		{
			return await GetDoubleNullableAsync(GetOrdinal(name), defaultValue);
		}

		#endregion

		#region GetFloatAsync
		/// <summary>
		/// Gets a Single value from the datareader.
		/// </summary>
		/// <remarks>
		/// Returns 0 for null.
		/// </remarks>
		/// <param name="name">Name of the column containing the value.</param>
		public async Task<float> GetFloatAsync(string name)
		{
			return await GetFloatAsync(_dataReader.GetOrdinal(name));
		}

		/// <summary>
		/// Gets a Single value from the datareader.
		/// </summary>
		/// <remarks>
		/// Returns 0 for null.
		/// </remarks>
		/// <param name="index">Ordinal column position of the value.</param>
		public virtual async Task<float> GetFloatAsync(int index)
		{
			if (_sqlDataReader == null) throw new NotSupportedException("GetFloatAsync");
			return await _sqlDataReader.IsDBNullAsync(index) ? 0 : await _sqlDataReader.GetFieldValueAsync<float>(index);
		}

		/// <summary>
		/// Gets a float value from the data reader.
		/// </summary>
		/// <param name="index">Ordinal index of the column holding the value.</param>
		/// <param name="defaultValue">Default return value if the data reader field is <see cref="DBNull"/>.</param>
		/// <returns>A <see cref="float"/>.</returns>
		public async Task<float> GetFloatAsync(int index, float defaultValue)
		{
			return await IsDbNullAsync(index) ? defaultValue : await GetFloatAsync(index);
		}

		/// <summary>
		/// Gets a float value from the data reader.
		/// </summary>
		/// <param name="name">Name of the column containing the value.</param>
		/// <param name="defaultValue">Default return value if the data reader field is <see cref="DBNull"/>.</param>
		/// <returns>A <see cref="float"/>.</returns>
		public async Task<float> GetFloatAsync(string name, float defaultValue)
		{
			return await GetFloatAsync(GetOrdinal(name), defaultValue);
		}

		/// <summary>
		/// Gets a nullable float value from the data reader.
		/// </summary>
		/// <param name="index">Ordinal index of the column holding the value.</param>
		/// <returns>
		/// A <see cref="Nullable"/>
		/// 	<see cref="float"/>.
		/// </returns>
		public async Task<float?> GetFloatNullableAsync(int index)
		{
			return await GetFloatNullableAsync(index, null);
		}

		/// <summary>
		/// Gets a nullable float value from the data reader.
		/// </summary>
		/// <param name="name">Name of the column containing the value.</param>
		/// <returns>
		/// A <see cref="Nullable"/>
		/// 	<see cref="float"/>.
		/// </returns>
		public async Task<float?> GetFloatNullableAsync(string name)
		{
			return await GetFloatNullableAsync(GetOrdinal(name));
		}

		/// <summary>
		/// Gets a nullable float value from the data reader.
		/// </summary>
		/// <param name="index">Ordinal index of the column holding the value.</param>
		/// <param name="defaultValue">Default return value if the data reader field is <see cref="DBNull"/>.</param>
		/// <returns>
		/// A <see cref="Nullable"/>
		/// 	<see cref="float"/>.
		/// </returns>
		public async Task<float?> GetFloatNullableAsync(int index, float? defaultValue)
		{
			if (_sqlDataReader == null) throw new NotSupportedException("GetFloatNullableAsync");
			return await IsDbNullAsync(index) ? defaultValue : await _sqlDataReader.GetFieldValueAsync<float>(index);
		}

		/// <summary>
		/// Gets a nullable float value from the data reader.
		/// </summary>
		/// <param name="name">Name of the column containing the value.</param>
		/// <param name="defaultValue">Default return value if the data reader field is <see cref="DBNull"/>.</param>
		/// <returns>
		/// A <see cref="Nullable"/>
		/// 	<see cref="float"/>.
		/// </returns>
		public async Task<float?> GetFloatNullableAsync(string name, float? defaultValue)
		{
			return await GetFloatNullableAsync(GetOrdinal(name), defaultValue);
		}
		#endregion

		#region GetGuidAsync
		/// <summary>
		/// Gets a Guid value from the datareader.
		/// </summary>
		/// <remarks>
		/// Returns Guid.Empty for null.
		/// </remarks>
		/// <param name="name">Name of the column containing the value.</param>
		public async Task<Guid> GetGuidAsync(string name)
		{
			return await GetGuidAsync(_dataReader.GetOrdinal(name));
		}

		/// <summary>
		/// Gets a Guid value from the datareader.
		/// </summary>
		/// <remarks>
		/// Returns Guid.Empty for null.
		/// </remarks>
		/// <param name="index">Ordinal column position of the value.</param>
		public virtual async Task<Guid> GetGuidAsync(int index)
		{
			if (_sqlDataReader == null) throw new NotSupportedException("GetGuidAsync");
			return await _sqlDataReader.IsDBNullAsync(index) ? Guid.Empty : await _sqlDataReader.GetFieldValueAsync<Guid>(index);
		}

		/// <summary>
		/// Gets a <see cref="Guid"/> value from the data reader.
		/// </summary>
		/// <param name="index">Ordinal index of the column holding the value.</param>
		/// <param name="defaultValue">Default return value if the data reader field is <see cref="DBNull"/>.</param>
		/// <returns>A <see cref="Guid"/>.</returns>
		public async Task<Guid> GetGuidAsync(int index, Guid defaultValue)
		{
			return await IsDbNullAsync(index) ? defaultValue : await GetGuidAsync(index);
		}

		/// <summary>
		/// Gets a <see cref="Guid"/> value from the data reader.
		/// </summary>
		/// <param name="name">Name of the column containing the value.</param>
		/// <param name="defaultValue">Default return value if the data reader field is <see cref="DBNull"/>.</param>
		/// <returns>A <see cref="Guid"/>.</returns>
		public async Task<Guid> GetGuidAsync(string name, Guid defaultValue)
		{
			return await GetGuidAsync(GetOrdinal(name), defaultValue);
		}

		/// <summary>
		/// Gets a nullable <see cref="Guid"/> value from the data reader.
		/// </summary>
		/// <param name="index">Ordinal index of the column holding the value.</param>
		/// <returns>
		/// A <see cref="Nullable"/>
		/// 	<see cref="Guid"/>.
		/// </returns>
		public async Task<Guid?> GetGuidNullableAsync(int index)
		{
			return await GetGuidNullableAsync(index, null);
		}

		/// <summary>
		/// Gets a nullable <see cref="Guid"/> value from the data reader.
		/// </summary>
		/// <param name="name">Name of the column containing the value.</param>
		/// <returns>
		/// A <see cref="Nullable"/>
		/// 	<see cref="Guid"/>.
		/// </returns>
		public async Task<Guid?> GetGuidNullableAsync(string name)
		{
			return await GetGuidNullableAsync(GetOrdinal(name));
		}

		/// <summary>
		/// Gets a nullable <see cref="Guid"/> value from the data reader.
		/// </summary>
		/// <param name="index">Ordinal index of the column holding the value.</param>
		/// <param name="defaultValue">Default return value if the data reader field is <see cref="DBNull"/>.</param>
		/// <returns>
		/// A <see cref="Nullable"/>
		/// 	<see cref="Guid"/>.
		/// </returns>
		public async Task<Guid?> GetGuidNullableAsync(int index, Guid? defaultValue)
		{
			if (_sqlDataReader == null) throw new NotSupportedException("GetGuidNullableAsync");
			return await IsDbNullAsync(index) ? defaultValue : await _sqlDataReader.GetFieldValueAsync<Guid>(index);
		}

		/// <summary>
		/// Gets a nullable <see cref="Guid"/> value from the data reader.
		/// </summary>
		/// <param name="name">Name of the column containing the value.</param>
		/// <param name="defaultValue">Default return value if the data reader field is <see cref="DBNull"/>.</param>
		/// <returns>
		/// A <see cref="Nullable"/>
		/// 	<see cref="Guid"/>.
		/// </returns>
		public async Task<Guid?> GetGuidNullableAsync(string name, Guid? defaultValue)
		{
			return await GetGuidNullableAsync(GetOrdinal(name), defaultValue);
		}
		#endregion

		#region GetInt16Async
		/// <summary>
		/// Gets a Short value from the datareader.
		/// </summary>
		/// <remarks>
		/// Returns 0 for null.
		/// </remarks>
		/// <param name="name">Name of the column containing the value.</param>
		public async Task<short> GetInt16Async(string name)
		{
			return await GetInt16Async(_dataReader.GetOrdinal(name));
		}

		/// <summary>
		/// Gets a Short value from the datareader.
		/// </summary>
		/// <remarks>
		/// Returns 0 for null.
		/// </remarks>
		/// <param name="index">Ordinal column position of the value.</param>
		public virtual async Task<short> GetInt16Async(int index)
		{
			if (_sqlDataReader == null) throw new NotSupportedException("GetInt16Async");
			return await _sqlDataReader.IsDBNullAsync(index) ? (short)0 : await _sqlDataReader.GetFieldValueAsync<short>(index);
		}

		/// <summary>
		/// Gets a Int16 value from the data reader.
		/// </summary>
		/// <param name="index">Ordinal index of the column holding the value.</param>
		/// <param name="defaultValue">Default return value if the data reader field is <see cref="DBNull"/>.</param>
		/// <returns>A <see cref="Int16"/>.</returns>
		public async Task<short> GetInt16Async(int index, short defaultValue)
		{
			return await IsDbNullAsync(index) ? defaultValue : await GetInt16Async(index);
		}

		/// <summary>
		/// Gets a Int16 value from the data reader.
		/// </summary>
		/// <param name="name">Name of the column containing the value.</param>
		/// <param name="defaultValue">Default return value if the data reader field is <see cref="DBNull"/>.</param>
		/// <returns>A <see cref="Int16"/>.</returns>
		public async Task<short> GetInt16Async(string name, short defaultValue)
		{
			return await GetInt16Async(GetOrdinal(name), defaultValue);
		}

		/// <summary>
		/// Gets a nullable Int16 value from the data reader.
		/// </summary>
		/// <param name="index">Ordinal index of the column holding the value.</param>
		/// <returns>
		/// A <see cref="Nullable"/>
		/// 	<see cref="Int16"/>.
		/// </returns>
		public async Task<short?> GetInt16NullableAsync(int index)
		{
			return await GetInt16NullableAsync(index, null);
		}

		/// <summary>
		/// Gets a nullable Int16 value from the data reader.
		/// </summary>
		/// <param name="name">Name of the column containing the value.</param>
		/// <returns>
		/// A <see cref="Nullable"/>
		/// 	<see cref="Int16"/>.
		/// </returns>
		public async Task<short?> GetInt16NullableAsync(string name)
		{
			return await GetInt16NullableAsync(GetOrdinal(name));
		}

		/// <summary>
		/// Gets a nullable Int16 value from the data reader.
		/// </summary>
		/// <param name="index">Ordinal index of the column holding the value.</param>
		/// <param name="defaultValue">Default return value if the data reader field is <see cref="DBNull"/>.</param>
		/// <returns>
		/// A <see cref="Nullable"/>
		/// 	<see cref="Int16"/>.
		/// </returns>
		public async Task<short?> GetInt16NullableAsync(int index, short? defaultValue)
		{
			if (_sqlDataReader == null) throw new NotSupportedException("GetInt16NullableAsync");
			return await IsDbNullAsync(index) ? defaultValue : await _sqlDataReader.GetFieldValueAsync<short>(index);
		}

		/// <summary>
		/// Gets a nullable Int16 value from the data reader.
		/// </summary>
		/// <param name="name">Name of the column containing the value.</param>
		/// <param name="defaultValue">Default return value if the data reader field is <see cref="DBNull"/>.</param>
		/// <returns>
		/// A <see cref="Nullable"/>
		/// 	<see cref="Int16"/>.
		/// </returns>
		public async Task<short?> GetInt16NullableAsync(string name, short? defaultValue)
		{
			return await GetInt16NullableAsync(GetOrdinal(name), defaultValue);
		}
		#endregion

		#region GetInt32Async
		/// <summary>
		/// Gets an integer from the datareader.
		/// </summary>
		/// <remarks>
		/// Returns 0 for null.
		/// </remarks>
		/// <param name="name">Name of the column containing the value.</param>
		public async Task<int> GetInt32Async(string name)
		{
			return await GetInt32Async(_dataReader.GetOrdinal(name));
		}

		/// <summary>
		/// Gets an integer from the datareader.
		/// </summary>
		/// <remarks>
		/// Returns 0 for null.
		/// </remarks>
		/// <param name="index">Ordinal column position of the value.</param>
		public virtual async Task<int> GetInt32Async(int index)
		{
			if (_sqlDataReader == null) throw new NotSupportedException("GetInt32Async");
			return await _sqlDataReader.IsDBNullAsync(index) ? 0 : await _sqlDataReader.GetFieldValueAsync<int>(index);
		}

		/// <summary>
		/// Gets a Int32 value from the data reader.
		/// </summary>
		/// <param name="index">Ordinal index of the column holding the value.</param>
		/// <param name="defaultValue">Default return value if the data reader field is <see cref="DBNull"/>.</param>
		/// <returns>A <see cref="Int32"/>.</returns>
		public async Task<int> GetInt32Async(int index, int defaultValue)
		{
			return await IsDbNullAsync(index) ? defaultValue : await GetInt32Async(index);
		}

		/// <summary>
		/// Gets a Int32 value from the data reader.
		/// </summary>
		/// <param name="name">Name of the column containing the value.</param>
		/// <param name="defaultValue">Default return value if the data reader field is <see cref="DBNull"/>.</param>
		/// <returns>A <see cref="Int32"/>.</returns>
		public async Task<int> GetInt32Async(string name, int defaultValue)
		{
			return await GetInt32Async(GetOrdinal(name), defaultValue);
		}

		/// <summary>
		/// Gets a nullable Int32 value from the data reader.
		/// </summary>
		/// <param name="index">Ordinal index of the column holding the value.</param>
		/// <returns>
		/// A <see cref="Nullable"/>
		/// 	<see cref="Int32"/>.
		/// </returns>
		public async Task<int?> GetInt32NullableAsync(int index)
		{
			return await GetInt32NullableAsync(index, null);
		}

		/// <summary>
		/// Gets a nullable Int32 value from the data reader.
		/// </summary>
		/// <param name="name">Name of the column containing the value.</param>
		/// <returns>
		/// A <see cref="Nullable"/>
		/// 	<see cref="Int32"/>.
		/// </returns>
		public async Task<int?> GetInt32NullableAsync(string name)
		{
			return await GetInt32NullableAsync(GetOrdinal(name));
		}

		/// <summary>
		/// Gets a nullable Int32 value from the data reader.
		/// </summary>
		/// <param name="index">Ordinal index of the column holding the value.</param>
		/// <param name="defaultValue">Default return value if the data reader field is <see cref="DBNull"/>.</param>
		/// <returns>
		/// A <see cref="Nullable"/>
		/// 	<see cref="Int32"/>.
		/// </returns>
		public async Task<int?> GetInt32NullableAsync(int index, int? defaultValue)
		{
			if (_sqlDataReader == null) throw new NotSupportedException("GetInt32NullableAsync");
			return await IsDbNullAsync(index) ? defaultValue : await _sqlDataReader.GetFieldValueAsync<int>(index);
		}

		/// <summary>
		/// Gets a nullable Int32 value from the data reader.
		/// </summary>
		/// <param name="name">Name of the column containing the value.</param>
		/// <param name="defaultValue">Default return value if the data reader field is <see cref="DBNull"/>.</param>
		/// <returns>
		/// A <see cref="Nullable"/>
		/// 	<see cref="Int32"/>.
		/// </returns>
		public async Task<int?> GetInt32NullableAsync(string name, int? defaultValue)
		{
			return await GetInt32NullableAsync(GetOrdinal(name), defaultValue);
		}
		#endregion

		#region GetInt64Async
		/// <summary>
		/// Gets a Long value from the datareader.
		/// </summary>
		/// <remarks>
		/// Returns 0 for null.
		/// </remarks>
		/// <param name="name">Name of the column containing the value.</param>
		public async Task<Int64> GetInt64Async(string name)
		{
			return await GetInt64Async(_dataReader.GetOrdinal(name));
		}

		/// <summary>
		/// Gets a Long value from the datareader.
		/// </summary>
		/// <remarks>
		/// Returns 0 for null.
		/// </remarks>
		/// <param name="index">Ordinal column position of the value.</param>
		public virtual async Task<Int64> GetInt64Async(int index)
		{
			if (_sqlDataReader == null) throw new NotSupportedException("GetInt64NullableAsync");
			return await _sqlDataReader.IsDBNullAsync(index) ? 0 : await _sqlDataReader.GetFieldValueAsync<Int64>(index);
		}

		/// <summary>
		/// Gets a Int64 value from the data reader.
		/// </summary>
		/// <param name="index">Ordinal index of the column holding the value.</param>
		/// <param name="defaultValue">Default return value if the data reader field is <see cref="DBNull"/>.</param>
		/// <returns>A <see cref="Int64"/>.</returns>
		public async Task<long> GetInt64Async(int index, long defaultValue)
		{
			return await IsDbNullAsync(index) ? defaultValue : await GetInt64Async(index);
		}

		/// <summary>
		/// Gets a Int64 value from the data reader.
		/// </summary>
		/// <param name="name">Name of the column containing the value.</param>
		/// <param name="defaultValue">Default return value if the data reader field is <see cref="DBNull"/>.</param>
		/// <returns>A <see cref="Int64"/>.</returns>
		public async Task<long> GetInt64Async(string name, long defaultValue)
		{
			return await GetInt64Async(GetOrdinal(name), defaultValue);
		}

		/// <summary>
		/// Gets a nullable Int64 value from the data reader.
		/// </summary>
		/// <param name="index">Ordinal index of the column holding the value.</param>
		/// <returns>
		/// A <see cref="Nullable"/>
		/// 	<see cref="Int64"/>.
		/// </returns>
		public async Task<long?> GetInt64NullableAsync(int index)
		{
			return await GetInt64NullableAsync(index, null);
		}

		/// <summary>
		/// Gets a nullable Int64 value from the data reader.
		/// </summary>
		/// <param name="name">Name of the column containing the value.</param>
		/// <returns>
		/// A <see cref="Nullable"/>
		/// 	<see cref="Int64"/>.
		/// </returns>
		public async Task<long?> GetInt64NullableAsync(string name)
		{
			return await GetInt64NullableAsync(GetOrdinal(name));
		}

		/// <summary>
		/// Gets a nullable Int64 value from the data reader.
		/// </summary>
		/// <param name="index">Ordinal index of the column holding the value.</param>
		/// <param name="defaultValue">Default return value if the data reader field is <see cref="DBNull"/>.</param>
		/// <returns>
		/// A <see cref="Nullable"/>
		/// 	<see cref="Int64"/>.
		/// </returns>
		public async Task<long?> GetInt64NullableAsync(int index, long? defaultValue)
		{
			if (_sqlDataReader == null) throw new NotSupportedException("GetInt64NullableAsync");
			return await IsDbNullAsync(index) ? defaultValue : await _sqlDataReader.GetFieldValueAsync<Int64>(index);
		}

		/// <summary>
		/// Gets a nullable Int64 value from the data reader.
		/// </summary>
		/// <param name="name">Name of the column containing the value.</param>
		/// <param name="defaultValue">Default return value if the data reader field is <see cref="DBNull"/>.</param>
		/// <returns>
		/// A <see cref="Nullable"/>
		/// 	<see cref="Int64"/>.
		/// </returns>
		public async Task<long?> GetInt64NullableAsync(string name, long? defaultValue)
		{
			return await GetInt64NullableAsync(GetOrdinal(name), defaultValue);
		}
		#endregion

		#region GetStringAsync
		/// <summary>
		/// Gets a string value from the datareader.
		/// </summary>
		/// <remarks>
		/// Returns empty string for null.
		/// </remarks>
		/// <param name="name">Name of the column containing the value.</param>
		public async Task<string> GetStringAsync(string name)
		{
			return await GetStringAsync(_dataReader.GetOrdinal(name));
		}

		/// <summary>
		/// Gets a string value from the datareader.
		/// </summary>
		/// <remarks>
		/// Returns empty string for null.
		/// </remarks>
		/// <param name="index">Ordinal column position of the value.</param>
		public virtual async Task<string> GetStringAsync(int index)
		{
			if (_sqlDataReader == null) throw new NotSupportedException("GetStringAsync");
			return await _sqlDataReader.IsDBNullAsync(index) ? String.Empty : await _sqlDataReader.GetFieldValueAsync<string>(index);
		}

		/// <summary>
		/// Gets a string value from the data reader.
		/// </summary>
		/// <param name="index">Ordinal index of the column holding the value.</param>
		/// <param name="defaultValue">Default return value if the data reader field is <see cref="DBNull"/>.</param>
		/// <returns>A <see cref="string"/>.</returns>
		public virtual async Task<string> GetStringAsync(int index, string defaultValue)
		{
			return await IsDbNullAsync(index) ? defaultValue : await GetStringAsync(index);
		}

		/// <summary>
		/// Gets a string value from the data reader.
		/// </summary>
		/// <param name="name">Name of the column containing the value.</param>
		/// <param name="defaultValue">Default return value if the data reader field is <see cref="DBNull"/>.</param>
		/// <returns>A <see cref="string"/>.</returns>
		public virtual async Task<string> GetStringAsync(string name, string defaultValue)
		{
			return await GetStringAsync(GetOrdinal(name));
		}

		/// <summary>
		/// Gets a nullable string value from the data reader.
		/// </summary>
		/// <param name="index">Ordinal index of the column holding the value.</param>
		/// <returns>
		/// A <see cref="Nullable"/>
		/// 	<see cref="string"/>.
		/// </returns>
		public virtual async Task<string> GetStringNullableAsync(int index)
		{
			return await GetStringNullableAsync(index, null);
		}

		/// <summary>
		/// Gets a nullable string value from the data reader.
		/// </summary>
		/// <param name="name">Name of the column containing the value.</param>
		/// <returns>
		/// A <see cref="Nullable"/>
		/// 	<see cref="string"/>.
		/// </returns>
		public virtual async Task<string> GetStringNullableAsync(string name)
		{
			return await GetStringNullableAsync(GetOrdinal(name));
		}

		/// <summary>
		/// Gets a nullable string value from the data reader.
		/// </summary>
		/// <param name="index">Ordinal index of the column holding the value.</param>
		/// <param name="defaultValue">Default return value if the data reader field is <see cref="DBNull"/>.</param>
		/// <returns>
		/// A <see cref="Nullable"/>
		/// 	<see cref="string"/>.
		/// </returns>
		public virtual async Task<string> GetStringNullableAsync(int index, string defaultValue)
		{
			if (_sqlDataReader == null) throw new NotSupportedException("GetStringNullableAsync");
			return await IsDbNullAsync(index) ? defaultValue : await _sqlDataReader.GetFieldValueAsync<string>(index);
		}

		/// <summary>
		/// Gets a nullable string value from the data reader.
		/// </summary>
		/// <param name="name">Name of the column containing the value.</param>
		/// <param name="defaultValue">Default return value if the data reader field is <see cref="DBNull"/>.</param>
		/// <returns>
		/// A <see cref="Nullable"/>
		/// 	<see cref="string"/>.
		/// </returns>
		public virtual async Task<string> GetStringNullableAsync(string name, string defaultValue)
		{
			return await GetStringNullableAsync(GetOrdinal(name), defaultValue);
		}
		#endregion

		#region GetTimeSpanAsync
		/// <summary>
		/// Gets a <see cref="TimeSpan"/> value from the data reader.
		/// </summary>
		/// <param name="index">Ordinal index of the column holding the value.</param>
		/// <returns>A <see cref="TimeSpan"/>.</returns>
		public async Task<TimeSpan> GetTimeSpanAsync(int index)
		{
			if (_sqlDataReader == null) throw new NotSupportedException("GetTimeSpanAsync");
			return await _sqlDataReader.IsDBNullAsync(index) ? new TimeSpan() : await _sqlDataReader.GetFieldValueAsync<TimeSpan>(index);
		}

		/// <summary>
		/// Gets a <see cref="TimeSpan"/> value from the data reader.
		/// </summary>
		/// <param name="name">Name of the column containing the value.</param>
		/// <returns>A <see cref="TimeSpan"/>.</returns>
		public async Task<TimeSpan> GetTimeSpanAsync(string name)
		{
			return await GetTimeSpanAsync(GetOrdinal(name));
		}

		/// <summary>
		/// Gets a <see cref="TimeSpan"/> value from the data reader.
		/// </summary>
		/// <param name="index">Ordinal index of the column holding the value.</param>
		/// <param name="defaultValue">Default return value if the data reader field is <see cref="DBNull"/>.</param>
		/// <returns>A <see cref="TimeSpan"/>.</returns>
		public async Task<TimeSpan> GetTimeSpanAsync(int index, TimeSpan defaultValue)
		{
			return await IsDbNullAsync(index) ? defaultValue : await GetTimeSpanAsync(index);
		}

		/// <summary>
		/// Gets a <see cref="TimeSpan"/> value from the data reader.
		/// </summary>
		/// <param name="name">Name of the column containing the value.</param>
		/// <param name="defaultValue">Default return value if the data reader field is <see cref="DBNull"/>.</param>
		/// <returns>A <see cref="TimeSpan"/>.</returns>
		public async Task<TimeSpan> GetTimeSpanAsync(string name, TimeSpan defaultValue)
		{
			return await GetTimeSpanAsync(GetOrdinal(name), defaultValue);
		}

		/// <summary>
		/// Gets a nullable <see cref="TimeSpan"/> value from the data reader.
		/// </summary>
		/// <param name="index">Ordinal index of the column holding the value.</param>
		/// <returns>
		/// A <see cref="Nullable"/>
		/// 	<see cref="TimeSpan"/>.
		/// </returns>
		public async Task<TimeSpan?> GetTimeSpanNullableAsync(int index)
		{
			if (_sqlDataReader == null) throw new NotSupportedException("GetTimeSpanNullableAsync");
			return await _sqlDataReader.IsDBNullAsync(index) ? (TimeSpan?)null : await _sqlDataReader.GetFieldValueAsync<TimeSpan>(index);
		}

		/// <summary>
		/// Gets a nullable <see cref="TimeSpan"/> value from the data reader.
		/// </summary>
		/// <param name="name">Name of the column containing the value.</param>
		/// <returns>
		/// A <see cref="Nullable"/>
		/// 	<see cref="TimeSpan"/>.
		/// </returns>
		public async Task<TimeSpan?> GetTimeSpanNullableAsync(string name)
		{
			return await GetTimeSpanNullableAsync(GetOrdinal(name));
		}

		/// <summary>
		/// Gets a nullable <see cref="TimeSpan"/> value from the data reader.
		/// </summary>
		/// <param name="index">Ordinal index of the column holding the value.</param>
		/// <param name="defaultValue">Default return value if the data reader field is <see cref="DBNull"/>.</param>
		/// <returns>
		/// A <see cref="Nullable"/>
		/// 	<see cref="TimeSpan"/>.
		/// </returns>
		public async Task<TimeSpan?> GetTimeSpanNullableAsync(int index, TimeSpan? defaultValue)
		{
			return await IsDbNullAsync(index) ? defaultValue : await GetTimeSpanAsync(index);
		}

		/// <summary>
		/// Gets a nullable <see cref="TimeSpan"/> value from the data reader.
		/// </summary>
		/// <param name="name">Name of the column containing the value.</param>
		/// <param name="defaultValue">Default return value if the data reader field is <see cref="DBNull"/>.</param>
		/// <returns>
		/// A <see cref="Nullable"/>
		/// 	<see cref="TimeSpan"/>.
		/// </returns>
		public async Task<TimeSpan?> GetTimeSpanNullableAsync(string name, TimeSpan? defaultValue)
		{
			var ts = await GetTimeSpanNullableAsync(name);
			return ts ?? defaultValue;
		}
		#endregion

		#region GetTimestampAsync
		/// <summary>
		/// Gets an SQL Server timestamp value from the data reader.
		/// </summary>
		/// <param name="index">Ordinal index of the column holding the value.</param>
		/// <returns>A byte array.</returns>
		public async Task<byte[]> GetTimestampAsync(int index)
		{
			var b = new byte[8];
			await GetBytesAsync(index, 0, b, 0, 8);
			return b;
		}

		/// <summary>
		/// Gets a SQL Server timestamp value from the data reader.
		/// </summary>
		/// <param name="name">Name of the column containing the value.</param>
		/// <returns>A byte array.</returns>
		public async Task<byte[]> GetTimestampAsync(string name)
		{
			return await GetTimestampAsync(GetOrdinal(name));
		}

		/// <summary>
		/// Gets an SQL Server timestamp value from the data reader.
		/// </summary>
		/// <param name="index">Ordinal index of the column holding the value.</param>
		/// <returns>An <see cref="Int64"/>.</returns>
		public async Task<long> GetTimestampInt64Async(int index)
		{
			var b = await GetTimestampAsync(index);
			return b.ToLong();
		}

		/// <summary>
		/// Gets an SQL Server timestamp value from the data reader.
		/// </summary>
		/// <param name="name">Name of the column containing the value.</param>
		/// <returns>A <see cref="Int64"/>.</returns>
		public async Task<long> GetTimestampInt64Async(string name)
		{
			return await GetTimestampInt64Async(GetOrdinal(name));
		}

		#endregion

		#region GetValueAsync
		/// <summary>
		/// Gets a value of type <see cref="System.Object" /> from the datareader.
		/// </summary>
		/// <param name="name">Name of the column containing the value.</param>
		public async Task<object> GetValueAsync(string name)
		{
			return await GetValueAsync(_dataReader.GetOrdinal(name));
		}

		/// <summary>
		/// Gets a value of type <see cref="System.Object" /> from the datareader.
		/// </summary>
		/// <param name="index">Ordinal column position of the value.</param>
		public virtual async Task<object> GetValueAsync(int index)
		{
			if (_sqlDataReader == null) throw new NotSupportedException("GetValueAsync");
			return await _sqlDataReader.IsDBNullAsync(index) ? null : await _sqlDataReader.GetFieldValueAsync<object>(index);
		}
		#endregion
#endif

		#region IDataReader Plumbing
		/// <summary>
		/// Reads the next row of data from the datareader.
		/// </summary>
		public bool Read()
		{
			return _dataReader.Read();
		}

		/// <summary>
		/// Moves to the next result set in the datareader.
		/// </summary>
		public bool NextResult()
		{
			return _dataReader.NextResult();
		}

		/// <summary>
		/// Closes the datareader.
		/// </summary>
		public void Close()
		{
			_dataReader.Close();
		}

		/// <summary>
		/// Returns the depth property value from the datareader.
		/// </summary>
		public int Depth { get { return _dataReader.Depth; } }

		/// <summary>
		/// Returns the FieldCount property from the datareader.
		/// </summary>
		public int FieldCount { get { return _dataReader.FieldCount; } }

		/// <summary>
		/// Invokes the GetName method of the underlying datareader.
		/// </summary>
		/// <param name="i">Ordinal column position of the value.</param>
		public virtual string GetName(int i)
		{
			return _dataReader.GetName(i);
		}

		/// <summary>
		/// Gets an ordinal value from the datareader.
		/// </summary>
		/// <param name="name">Name of the column containing the value.</param>
		public int GetOrdinal(string name)
		{
			return _dataReader.GetOrdinal(name);
		}

		/// <summary>
		/// Invokes the GetSchemaTable method of the underlying datareader.
		/// </summary>
		public DataTable GetSchemaTable()
		{
			return _dataReader.GetSchemaTable();
		}

		/// <summary>
		/// Invokes the GetValues method of the underlying datareader.
		/// </summary>
		/// <param name="values">An array of System.Object to
		/// copy the values into.</param>
		public int GetValues(object[] values)
		{
			return _dataReader.GetValues(values);
		}

		/// <summary>
		/// Returns the IsClosed property value from the datareader.
		/// </summary>
		public bool IsClosed { get { return _dataReader.IsClosed; } }

		/// <summary>
		/// Returns the RecordsAffected property value from the underlying datareader.
		/// </summary>
		public int RecordsAffected { get { return _dataReader.RecordsAffected; } }

		/// <summary>
		/// Invokes the IsDBNull method of the underlying datareader.
		/// </summary>
		/// <param name="i">Ordinal column position of the value.</param>
		public virtual bool IsDBNull(int i)
		{
			return _dataReader.IsDBNull(i);
		}

		/// <summary>
		/// Invokes the IsDBNull method of the underlying datareader.
		/// </summary>
		/// <param name="name">Name of the column containing the value.</param>
		public virtual bool IsDBNull(string name)
		{
			var index = GetOrdinal(name);
			return IsDBNull(index);
		}

		/// <summary>
		/// Returns a value from the datareader.
		/// </summary>
		/// <param name="name">Name of the column containing the value.</param>
		public object this[string name]
		{
			get
			{
				var val = _dataReader[name];
				return DBNull.Value.Equals(val) ? null : val;
			}
		}

		/// <summary>
		/// Returns a value from the datareader.
		/// </summary>
		/// <param name="i">Ordinal column position of the value.</param>
		public virtual object this[int i]
		{
			get { return _dataReader.IsDBNull(i) ? null : _dataReader[i]; }
		}

		#endregion

		#region IDisposable Support

		private bool _disposedValue; // To detect redundant calls

		/// <summary>
		/// Disposes the object.
		/// </summary>
		/// <param name="disposing">True if called by
		/// the public Dispose method.</param>
		protected virtual void Dispose(bool disposing)
		{
			if (!_disposedValue)
			{
				if (disposing)
				{
					// free unmanaged resources when explicitly called
					_dataReader.Dispose();
				}

				// free shared unmanaged resources
			}
			_disposedValue = true;
		}

		/// <summary>
		/// Disposes the object.
		/// </summary>
		public void Dispose()
		{
			// Do not change this code. Put cleanup code in Dispose(bool disposing) above.
			Dispose(true);
			GC.SuppressFinalize(this);
		}
		#endregion
	}
}
