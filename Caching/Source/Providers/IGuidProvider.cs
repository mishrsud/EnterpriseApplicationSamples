using System;

namespace Smi.Caching.Providers
{
	public interface IGuidProvider
	{
		/// <summary>
		/// Creates a new GUID.
		/// </summary>
		/// <returns>New GUId</returns>
		Guid NewGuid();
	}
}
