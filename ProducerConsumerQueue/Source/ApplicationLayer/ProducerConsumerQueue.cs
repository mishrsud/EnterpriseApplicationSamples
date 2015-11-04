using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using NLog;

namespace Smi.ApplicationLayer
{
	/// <summary>
	/// A simple Producer-Consumer queue with multiple thread-based consumers.
	/// Usage:
	///		- Create an instance of <see cref="ProducerConsumerQueue{T}"/>.
	///		- Hook up data-event-handler on <see cref="IProducerConsumerQueue{T}.OnConsumeData"/>.
	///		- Start consumers by calling <see cref="IProducerConsumerQueue{T}.Start()"/> or <see cref="IProducerConsumerQueue{T}.Start(int)"/>.
	///		- Add data to queue by calling Add functions.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class ProducerConsumerQueue<T> : IProducerConsumerQueue<T>
	{
		private readonly BlockingCollection<T> _blockingCollection;
		private readonly CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();
		private readonly object _queueLock = new object();
		private Task[] _tasks;
		private bool _started;
		private bool _disposed;

		private readonly Logger _logger = LogManager.GetCurrentClassLogger();

		private readonly TimeSpan _maxTimeCloseDownWaitTime = TimeSpan.FromSeconds(5);

		#if NET40
		/// <summary>
		/// Gets or sets the Consume Data Event Handler.
		/// </summary>
		public GenericEventHandler<T> OnConsumeData { get; set; }
		#endif

		#if NET45
		/// <summary>
		/// Gets or sets the Consume Data Event Handler.
		/// </summary>
		public EventHandler<T> OnConsumeData { get; set; }
		#endif

		/// <summary>
		/// Initializes a new instance of the <see cref="ProducerConsumerQueue{T}"/> class.
		/// </summary>
		public ProducerConsumerQueue()
		{
			_blockingCollection = new BlockingCollection<T>();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ProducerConsumerQueue{T}" /> class.
		/// </summary>
		/// <param name="collection">The collection.</param>
		/// <exception cref="System.ArgumentNullException">collection</exception>
		public ProducerConsumerQueue(IProducerConsumerCollection<T> collection)
		{
			if (collection == null) throw new ArgumentNullException("collection", "The collection argument is null.");

			_blockingCollection = new BlockingCollection<T>(collection);
		}

		/// <summary>
		/// Adds the item to the consumer queue.
		/// </summary>
		/// <param name="data">The item to be added to the collection. The value can be a null reference.</param>
		/// <param name="token">A cancellation token to observe.</param>
		public void Add(T data, CancellationToken token)
		{
			CheckDisposed();
			_blockingCollection.Add(data, token);
		}

		/// <summary>
		/// Adds the item to the consumer queue.
		/// </summary>
		/// <param name="data">The item to be added to the collection. The value can be a null reference.</param>
		public void Add(T data)
		{
			CheckDisposed();
			_blockingCollection.Add(data, _cancellationTokenSource.Token);
		}

		/// <summary>
		/// Gets whether this queue has been marked as complete for adding.
		/// </summary>
		/// <value>Whether this collection has been marked as complete for adding.</value>
		/// <exception cref="System.ObjectDisposedException">Already disposed.</exception>
		public bool IsAddingCompleted
		{
			get
			{
				CheckDisposed();
				return _blockingCollection.IsAddingCompleted;
			}
		}

		/// <summary>
		/// Gets whether this queue has been marked as complete for adding and is empty.
		/// </summary>
		/// <value>
		/// <c>true</c> if this instance is completed; otherwise, <c>false</c>.
		/// </value>
		/// <exception cref="System.ObjectDisposedException">Already disposed.</exception>
		public bool IsCompleted
		{
			get
			{
				CheckDisposed();
				return _blockingCollection.IsCompleted;
			}
		}

		/// <summary>
		/// Gets the number of items contained in the queue.
		/// </summary>
		/// <exception cref="System.ObjectDisposedException">Already disposed.</exception>
		public int Count
		{
			get
			{
				CheckDisposed();
				return _blockingCollection.Count;
			}
		}

		/// <summary>
		/// Marks the queue instances as not accepting any more additions.
		/// </summary>
		/// <exception cref="System.ObjectDisposedException">Already disposed.</exception>
		public void CompleteAdding()
		{
			CheckDisposed();
			_blockingCollection.CompleteAdding();
		}

		/// <summary>
		/// Starts Consumer threads consuming the data in queue.
		/// Using <see cref="P:System.Environment.ProcessorCount"/> number of threads.
		/// </summary>
		public void Start()
		{
			CheckDisposed();
			Start(Environment.ProcessorCount);
		}

		/// <summary>
		/// Starts the specified degree of parallelism.
		/// </summary>
		/// <param name="degreeOfParallelism">The number of threads in the queue.</param>
		public void Start(int degreeOfParallelism)
		{
			if (degreeOfParallelism < 0 || degreeOfParallelism > 512) throw new ArgumentOutOfRangeException("degreeOfParallelism");

			CheckDisposed();
			if (_started) throw new InvalidOperationException("Already started");

			if (degreeOfParallelism == 0) degreeOfParallelism = Environment.ProcessorCount;

			lock (_queueLock)
			{
				if (_started) return;

				_logger.Debug("Starting Producer-Consumer Queue for type '{0}'. Number of consumer threads: {1}", typeof(T), degreeOfParallelism);

				// Start worker threads
				#if NET40
				_tasks = new Task[degreeOfParallelism];

				for (int i = 0; i < degreeOfParallelism; i++)
				{
					_tasks[i] = Task.Factory.StartNew(
						() => DataProcessor(_cancellationTokenSource.Token), _cancellationTokenSource.Token);
				}
				#endif

				#if NET45
				_tasks = Enumerable.Range(0, degreeOfParallelism).Select(
					_ => Task.Run(
						() => DataProcessor(_cancellationTokenSource.Token), _cancellationTokenSource.Token)
					).ToArray();
				#endif

				_started = true;
			}
		}

		/// <summary>
		/// Processing elements as they are received in the queue.
		/// </summary>
		private void DataProcessor(CancellationToken token)
		{
			try
			{
				foreach (var data in _blockingCollection.GetConsumingEnumerable(token))
				{
					if (_disposed) break;
					if ((object)data != null)
					{
						InvokeUserHandler(data);
					}
				}
			}
			catch (OperationCanceledException) { }
		}

		/// <summary>
		/// Invokes the user handler.
		/// </summary>
		/// <param name="data">The data.</param>
		protected void InvokeUserHandler(T data)
		{
			if (_disposed) return;

			var localOnConsumeData = OnConsumeData;
			if (localOnConsumeData == null) return;

			try
			{
				localOnConsumeData(this, data);
			}
			catch (Exception ex)
			{
				_logger.Error("Exception in Producer-Consumer USER handler. Queue type: '{0}'", ex, typeof(T));
			}
		}

		private void CheckDisposed()
		{
			if (_disposed) throw new ObjectDisposedException("ProducerComsumerQueue already disposed");
		}

		/// <summary>
		/// Releases unmanaged and - optionally - managed resources.
		/// </summary>
		/// <param name="disposing">Whether being disposed explicitly (true) or due to a finalizer (false).</param>
		protected virtual void Dispose(bool disposing)
		{
			if (!_disposed)
			{
				_disposed = true;
				OnConsumeData = null;
				try
				{
					_blockingCollection.CompleteAdding();
					_cancellationTokenSource.Cancel();

					if (_tasks != null)
					{
						bool allTasksFinished = Task.WaitAll(_tasks, _maxTimeCloseDownWaitTime);

						if (!allTasksFinished)
						{
							_logger.Warn("Some events didn't shut down after waiting {0} in Queue dispose", _maxTimeCloseDownWaitTime);
						}

						if (allTasksFinished) _tasks.ForEach(t => t.Dispose());
						_tasks = null;
					}

					OnConsumeData = null;
					_cancellationTokenSource.Dispose();
					_blockingCollection.Dispose();
				}
				catch (Exception)
				{
					// Exceptions can in theory occur if event doesn't shut down properly
					_started = false;
				}
			}
		}

		/// <summary>
		/// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
		/// </summary>
		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}
	}
}
