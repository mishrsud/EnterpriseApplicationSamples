using System;

namespace Smi.Caching.Providers
{
	public class GuidProvider : IGuidProvider
	{
		/// <summary>
		/// News the unique identifier.
		/// </summary>
		/// <returns></returns>
		public Guid NewGuid()
		{
			return Guid.NewGuid();
		}
	}
}
