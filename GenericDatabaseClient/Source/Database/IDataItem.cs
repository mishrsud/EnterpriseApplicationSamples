namespace Database
{
	/// <summary>
	/// Represents an item in a list of result returned from the data access layer.
	/// </summary>
	public interface IDataItem
	{
		/// <summary>
		/// Gets a value for the specified field
		/// </summary>
		/// <typeparam name="T">The type of the field</typeparam>
		/// <param name="fieldName">The name of the field</param>
		/// <returns>The value of the field</returns>
		/// <remarks>If the field value is null or cannot be converted to <typeparamref name="T"/>, the default value for <typeparamref name="T"/> will be returned</remarks>
		T GetField<T>(string fieldName);

		/// <summary>
		/// Gets a value for the specified field with with fallback to a default value if value is missing or cannot be converted to <typeparamref name="T"/>
		/// </summary>
		/// <typeparam name="T">The type of the field</typeparam>
		/// <param name="fieldName">The name of the field</param>
		/// <param name="defaultValue">The default value to use if value is missing or value cannot be converted to <typeparamref name="T"/></param>
		/// <returns>The value of the field or <paramref name="defaultValue"/> if value is missing or cannot be converted to <typeparamref name="T"/></returns>
		T GetField<T>(string fieldName, T defaultValue);
	}
}
