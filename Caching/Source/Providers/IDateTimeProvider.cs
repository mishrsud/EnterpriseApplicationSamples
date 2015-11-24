using System;

namespace Smi.Caching.Providers
{
	public interface IDateTimeProvider
	{
		/// <summary>
		///	Gets the current UTC date and time.
		/// </summary>
		UtcDateTime UtcDateTimeNow { get; }

		/// <summary>
		/// Gets the current UTC date.
		/// </summary>
		/// <seealso cref="DateTime.Today"/>
		UtcDate Today { get; }
	}

}
