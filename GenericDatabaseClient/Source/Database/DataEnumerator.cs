using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database
{
	public sealed class DataEnumerator : IDataEnumerator
	{
		private readonly IDataReader _reader;
		private IEnumerator<IDataItem> _enumerator;

		/// <summary>
		/// Initializes a new instance of the <see cref="DataEnumerator"/> class.
		/// </summary>
		/// <param name="reader">The reader.</param>
		public DataEnumerator(IDataReader reader)
		{
			if (reader == null) throw new ArgumentNullException("reader");
			_reader = reader;
		}

		/// <summary>
		/// Returns an enumerator that iterates through the collection.
		/// </summary>
		/// <returns>
		/// A <see cref="T:System.Collections.Generic.IEnumerator`1"/> that can be used to iterate through the collection.
		/// </returns>
		/// <filterpriority>1</filterpriority>
		public IEnumerator<IDataItem> GetEnumerator()
		{
			return _enumerator ?? (_enumerator = new DataReaderEnumerator(_reader));
		}

		/// <summary>
		/// Returns an enumerator that iterates through a collection.
		/// </summary>
		/// <returns>
		/// An <see cref="T:System.Collections.IEnumerator"/> object that can be used to iterate through the collection.
		/// </returns>
		/// <filterpriority>2</filterpriority>
		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		/// <summary>
		/// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
		/// </summary>
		/// <filterpriority>2</filterpriority>
		public void Dispose()
		{
			_reader.Dispose();
		}

		private sealed class DataReaderEnumerator : IEnumerator<IDataItem>
		{
			private readonly IDataReader _reader;
			private IDataItem _current;
			private Dictionary<string, int> _ordinals;

			public DataReaderEnumerator(IDataReader reader)
			{
				if (reader == null) throw new ArgumentNullException("reader");
				_reader = reader;
			}

			public void Dispose()
			{
				_current = null;
				_ordinals = null;
			}

			public bool MoveNext()
			{
				_current = null;
				return _reader.Read();
			}

			public void Reset()
			{
				throw new InvalidOperationException();
			}

			public IDataItem Current
			{
				get { return _current ?? (_current = new DataItem(Ordinals, _reader)); }
			}

			object IEnumerator.Current
			{
				get { return Current; }
			}

			private Dictionary<string, int> Ordinals
			{
				get
				{
					if (_ordinals != null) return _ordinals;
					var ordinals = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
					var fieldCount = _reader.FieldCount;
					for (var i = 0; i < fieldCount; i++)
					{
						var name = _reader.GetName(i);
						ordinals.Add(name, _reader.GetOrdinal(name));
					}
					return (_ordinals = ordinals);
				}
			}
		}
	}
}
