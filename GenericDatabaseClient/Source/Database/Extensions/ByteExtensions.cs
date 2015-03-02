using System;

namespace Database.Extensions
{
	public static class ByteExtensions
	{
		/// <summary>
		/// Converts the byte array to a <see cref="long"/> using <see cref="BitConverter.ToInt64"/>.
		/// </summary>
		/// <param name="input">The byte array to convert.</param>
		/// <exception cref="ArgumentNullException">Thrown if <paramref name="input"/> is null.</exception>
		/// <remarks>
		/// This method assumes the array is constructed using big-endianness, which is the standard for SQL Server. 
		/// Thus if the current system is using little-endianness, a reverse copy of the array is used for the conversion.
		/// </remarks>
		public static long ToLong(this byte[] input)
		{
			if (input == null) throw new ArgumentNullException("input");
			if (BitConverter.IsLittleEndian)
				input = input.ReverseCopy();
			return BitConverter.ToInt64(input, 0);
		}

		/// <summary>
		/// Creates a reverse copy of the array.
		/// </summary>
		/// <param name="input">The array to create a reverse copy of.</param>
		/// <exception cref="ArgumentNullException">Thrown if <paramref name="input"/> is null.</exception>
		private static byte[] ReverseCopy(this byte[] input)
		{
			if (input == null) throw new ArgumentNullException("input");
			var length = input.Length;
			var copy = new byte[length];
			for (var i = 0; i < length; i++)
			{
				copy[i] = input[length - 1 - i];
			}
			return copy;
		}
	}
}
