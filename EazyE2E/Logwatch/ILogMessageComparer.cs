//Copyright 2019 Ian Duckworth

namespace EazyE2E.Logwatch
{
	/// <summary>
	/// Overrides a standard comparison of a log message and watch text
	/// </summary>
    public interface ILogMessageComparer
    {
		/// <summary>
		/// Provides custom comparison logic for checking watch text against a log message.  Overrides the standard .Contains implementation
		/// </summary>
		/// <param name="watchText"></param>
		/// <param name="logMessage"></param>
		/// <returns></returns>
        bool Compare(string watchText, string logMessage);
    }
}
