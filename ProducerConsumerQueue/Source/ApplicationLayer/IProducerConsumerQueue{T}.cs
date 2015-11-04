using System;
using System.Threading;

namespace Smi.ApplicationLayer
{
	#if NET40
	/// <summary>
	/// Mimicks .NET 4.5 and above version of generic event handler that does not constraint the TEventArgs type param
	/// </summary>
	/// <typeparam name="TEventArgs">The type of the event arguments.</typeparam>
	/// <param name="sender">The sender.</param>
	/// <param name="e">The <see cref="TEventArgs"/> instance containing the event data.</param>
	public delegate void GenericEventHandler<TEventArgs>(object sender, TEventArgs e);
	#endif

	/// <summary>
	/// A contract for a Producer-Consumer queue with multi-threaded consumers
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public interface IProducerConsumerQueue<T> : IDisposable
	{
		#if NET40
		/// <summary>
		/// Gets or sets the on consume data.
		/// </summary>
		/// <remarks>
		/// This is a workaround for the difference between definition of delegate EventHandler{TEventArgs}
		/// </remarks>
		GenericEventHandler<T> OnConsumeData { get; set; }
		#endif

		#if NET45
		/// <summary>
		/// Gets or sets the on consume data.
		/// </summary>
		EventHandler<T> OnConsumeData { get; set; }
		#endif

		/// <summary>
		/// Starts Consumer threads consuming the data in queue.
		/// Using <see cref="P:System.Environment.ProcessorCount"/> number of threads.
		/// </summary>
		void Start();

		/// <summary>
		/// Starts the specified degree of parallelism.
		/// </summary>
		/// <param name="degreeOfParallelism">The degree of parallelism.</param>
		void Start(int degreeOfParallelism);

		/// <summary>
		/// Adds the item to the consumer queue.
		/// </summary>
		/// <param name="data">The item to be added to the collection. The value can be a null reference.</param>
		/// <param name="token">A cancellation token to observe.</param>
		void Add(T data, CancellationToken token);

		/// <summary>
		/// Adds the item to the consumer queue.
		/// </summary>
		/// <param name="data">The item to be added to the collection. The value can be a null reference.</param>
		/// <exception cref="System.ArgumentNullException">data</exception>
		void Add(T data);

		/// <summary>
		/// Gets whether this queue has been marked as complete for adding.
		/// </summary>
		/// <value>Whether this collection has been marked as complete for adding.</value>
		/// <exception cref="System.ObjectDisposedException">Already disposed.</exception>
		bool IsAddingCompleted { get; }

		/// <summary>
		/// Gets whether this queue has been marked as complete for adding and is empty.
		/// </summary>
		/// <value>
		/// <c>true</c> if this instance is completed; otherwise, <c>false</c>.
		/// </value>
		/// <exception cref="System.ObjectDisposedException">Already disposed.</exception>
		bool IsCompleted { get; }

		/// <summary>
		/// Gets the number of items contained in the queue.
		/// </summary>
		/// <exception cref="System.ObjectDisposedException">Already disposed.</exception>
		int Count { get; }

		/// <summary>
		/// Marks the queue instances as not accepting any more additions.
		/// </summary>
		/// <exception cref="System.ObjectDisposedException">Already disposed.</exception>
		void CompleteAdding();
	}
}
