using System;
using System.Collections;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Globalization;
using System.Linq;

namespace Logging.Impl
{
	/// <summary>
	/// Helpers to work with Arguments
	/// </summary>
	internal static class ArgumentHelpers
	{
		private static readonly Hashtable s_parsers = new Hashtable();

		//NOTE : We cannot use the Logging framework here as this class is being used in setting up the logging framework....
		//       So we use Trace.WriteLine instead.......

		/// <summary>
		/// Initialize all members before any of this class' methods can be accessed (avoids beforeFieldInit)
		/// </summary>
		static ArgumentHelpers()
		{
			RegisterTypeParser(s => Convert.ToBoolean(s, CultureInfo.CurrentCulture));
			RegisterTypeParser(s => Convert.ToInt16(s, CultureInfo.CurrentCulture));
			RegisterTypeParser(s => Convert.ToInt32(s, CultureInfo.CurrentCulture));
			RegisterTypeParser(s => Convert.ToInt64(s, CultureInfo.CurrentCulture));
			RegisterTypeParser(s => Convert.ToSingle(s, CultureInfo.CurrentCulture));
			RegisterTypeParser(s => Convert.ToDouble(s, CultureInfo.CurrentCulture));
			RegisterTypeParser(s => Convert.ToDecimal(s, CultureInfo.CurrentCulture));
		}

		/// <summary>
		/// Adds the parser to the list of known type parsers.
		/// </summary>
		/// <remarks>
		/// .NET intrinsic types are pre-registerd: short, int, long, float, double, decimal, bool
		/// </remarks>
		private static void RegisterTypeParser<T>(Func<string, T> parser)
		{
			s_parsers[typeof(T)] = parser;
		}

		/// <summary>
		/// Retrieves the named value from the specified <see cref="NameValueCollection"/>.
		/// </summary>
		/// <param name="values">may be null</param>
		/// <param name="name">the value's key</param>
		/// <returns>if <paramref name="values"/> is not null, the value returned by values[name]. <c>null</c> otherwise.</returns>
		public static string GetValue(NameValueCollection values, string name)
		{
			return GetValue(values, name, null);
		}

		/// <summary>
		/// Retrieves the named value from the specified <see cref="NameValueCollection"/>.
		/// </summary>
		/// <param name="values">may be null</param>
		/// <param name="name">the value's key</param>
		/// <param name="defaultValue">the default value, if not found</param>
		/// <returns>if <paramref name="values"/> is not null, the value returned by values[name]. <c>null</c> otherwise.</returns>
		public static string GetValue(NameValueCollection values, string name, string defaultValue)
		{
			if (values != null)
			{
				if (values.AllKeys.Any(key => string.Compare(name, key, true, CultureInfo.CurrentCulture) == 0))
				{
					return values[name];
				}
			}

			return defaultValue;
		}

		/// <summary>
		/// Tries parsing <paramref name="value"/> into an enum of the type of <paramref name="defaultValue"/>.
		/// </summary>
		/// <param name="defaultValue">the default value to return if parsing fails</param>
		/// <param name="value">the string value to parse</param>
		/// <returns>the successfully parsed value, <paramref name="defaultValue"/> otherwise.</returns>
		public static T TryParseEnum<T>(T defaultValue, string value) where T : struct
		{
			if (!typeof(T).IsEnum)
				throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, "ResourceType '{0}' is not an enum type", typeof(T).FullName));

			T result;
			if (string.IsNullOrEmpty(value))
				return defaultValue;

			if (!Enum.TryParse(value, out result))
			{
				Trace.WriteLine(string.Format(CultureInfo.CurrentCulture, "WARN: failed converting value '{0}' to enum type '{1}'", value, defaultValue.GetType().FullName));
				return defaultValue;
			}

			return result;
		}

		/// <summary>
		/// Tries parsing <paramref name="value" /> into an enum of the type of <paramref name="defaultValue" />.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="defaultValue">the default value to return if parsing fails</param>
		/// <param name="value">the string value to parse</param>
		/// <param name="ignoreCase">if set to <c>true</c> casing is ignored when parsing enum value.</param>
		/// <returns>
		/// the successfully parsed value, <paramref name="defaultValue" /> otherwise.
		/// </returns>
		/// <exception cref="System.ArgumentException"></exception>
		public static T TryParseEnum<T>(T defaultValue, string value, bool ignoreCase) where T : struct
		{
			if (!typeof(T).IsEnum)
				throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, "ResourceType '{0}' is not an enum type", typeof(T).FullName));

			T result;
			if (string.IsNullOrEmpty(value))
				return defaultValue;

			if (!Enum.TryParse(value, ignoreCase, out result))
			{
				Trace.WriteLine(string.Format(CultureInfo.CurrentCulture, "WARN: failed converting value '{0}' to enum type '{1}'", value, defaultValue.GetType().FullName));
				return defaultValue;
			}

			return result;
		}

		/// <summary>
		/// Tries parsing <paramref name="value"/> into the specified return type.
		/// </summary>
		/// <param name="defaultValue">the default value to return if parsing fails</param>
		/// <param name="value">the string value to parse</param>
		/// <returns>the successfully parsed value, <paramref name="defaultValue"/> otherwise.</returns>
		public static T TryParse<T>(T defaultValue, string value)
		{
			var result = defaultValue;
			if (string.IsNullOrEmpty(value))
			{
				return defaultValue;
			}

			var parser = s_parsers[typeof(T)] as Func<string, T>;
			if (parser == null)
				throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, "There is no parser registered for type {0}", typeof(T).FullName));

			try
			{
				result = parser(value);
			}
			catch
			{
				Trace.WriteLine(string.Format(CultureInfo.CurrentCulture, "WARN: failed converting value '{0}' to type '{1}' - returning default '{2}'", value, typeof(T).FullName, result));
			}

			return result;
		}
	}
}
