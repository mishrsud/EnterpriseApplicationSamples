namespace TestApplication
{
	/// <summary>
	/// Sample enumeration demonstrating event IDs that would be logged
	/// </summary>
	public enum LogEventId
	{
		/// <summary> The information only </summary>
		InformationOnly = 5000,

		/// <summary> The custom exception </summary>
		CustomException = 5001,

		/// <summary> The unhandled exception </summary>
		UnhandledException = 5002
	}
}
