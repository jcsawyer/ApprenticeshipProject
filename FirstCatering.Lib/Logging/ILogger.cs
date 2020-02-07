using System;

namespace FirstCatering.Lib.Logging
{
    /// <summary>
    /// Logger definition
    /// </summary>
    public interface ILogger
    {
        /// <summary>
        /// Logs a debug message
        /// </summary>
        /// <param name="message">Log message</param>
        void Debug(string message);

        /// <summary>
        /// Logs an error message for the specified <paramref name="exception"/>
        /// </summary>
        /// <param name="exception"><see cref="Exception"/> exception</param>
        void Error(Exception exception);

        /// <summary>
        /// Logs an error message
        /// </summary>
        /// <param name="message">Log message</param>
        void Error(string message);

        /// <summary>
        /// Logs a fatal message for the specified <paramref name="exception"/>
        /// </summary>
        /// <param name="exception"><see cref="Exception"/> fatal exception</param>
        void Fatal(Exception exception);

        /// <summary>
        /// Logs an information message
        /// </summary>
        /// <param name="message">Log message</param>
        void Information(string message);

        /// <summary>
        /// Logs a warning message
        /// </summary>
        /// <param name="message">Log message</param>
        void Warning(string message);
    }
}