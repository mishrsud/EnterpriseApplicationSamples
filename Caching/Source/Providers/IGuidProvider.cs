using System;

namespace Providers
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
