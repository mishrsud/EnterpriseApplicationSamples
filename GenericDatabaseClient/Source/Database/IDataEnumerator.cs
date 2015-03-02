using System;
using System.Collections.Generic;

namespace Database
{
	/// <summary>
	/// Provides enumeration of items returned from data source
	/// </summary>
	public interface IDataEnumerator : IEnumerable<IDataItem>, IDisposable
	{

	}
}
