namespace Logging
{
	///<summary>
	/// The type of method that is passed into e.g. <see cref="ILogger.Debug(int,System.Action{FormatMessageHandler})"/> 
	/// and allows the callback method to "submit" it's message to the underlying output system.
	///</summary>
	///<param name="format">the format argument as in <see cref="string.Format(string,object[])"/></param>
	///<param name="args">the argument list as in <see cref="string.Format(string,object[])"/></param>
	///<seealso cref="ILogger"/>
	public delegate string FormatMessageHandler(string format, params object[] args);
}
