using System;

namespace Providers
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
