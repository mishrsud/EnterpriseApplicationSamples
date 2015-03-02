namespace ApplicationLayer
{
	/// <summary>
	/// The event ID values used in logging
	/// </summary>
	public enum LogEventId
	{
		/// <summary>
		/// Event specifying start-information for queue.
		/// </summary>
		ProducerConsumerQueueStart = 5000,

		/// <summary>
		/// Exception in user Producer-handler.
		/// </summary>
		ProducerConsumerQueueEventHandlerException = 5001,

		/// <summary>
		/// User code does not shut down properly.
		/// </summary>
		ProducerConsumerQueueUserCodeDoesntNotShutDown = 5002,
	}
}
