using System;
using System.Collections.Generic;

namespace Smi.ApplicationLayer
{
	public static class IEnumerableExtensions
	{
		/// <summary>
		/// Fors the each.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="source">The source.</param>
		/// <param name="action">The action.</param>
		/// <exception cref="System.ArgumentNullException">
		/// source
		/// or
		/// action
		/// </exception>
		public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
		{
			if (source == null) throw new ArgumentNullException("source");
			if (action == null) throw new ArgumentNullException("action");

			foreach (var obj in source)
			{
				action(obj);
			}
		}
	}
}
