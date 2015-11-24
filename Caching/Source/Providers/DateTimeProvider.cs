using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smi.Caching.Providers
{

	/// <summary>
	/// An impementation of <see cref="IDateTimeProvider"/>
	/// </summary>
	public class DateTimeProvider : IDateTimeProvider
	{
		/// <summary>
		///	Gets the current UTC date and time.
		/// </summary>
		public UtcDateTime UtcDateTimeNow
		{
			get { return UtcDateTime.Now; }
		}

		/// <summary>
		/// Gets the current UTC date.
		/// </summary>
		/// <seealso cref="DateTime.Today"/>
		public UtcDate Today
		{
			get { return UtcDate.Today; }
		}
	}
}
