using Microsoft.Extensions.Configuration;
using Serilog;
using System;

namespace FirstCatering.Lib.Logging
{
    /// <summary>
    /// Logger implementation using Serilog
    /// </summary>
    public class Logger : ILogger
    {
        /// <summary>
        /// Initialises a <see cref="Logger"/> using the given <paramref name="configuration"/>
        /// </summary>
        /// <param name="configuration"><see cref="IConfiguration"/> configuration</param>
        public Logger(IConfiguration configuration)
        {
            Log.Logger = new LoggerConfiguration()
                .ReadFrom
                .Configuration(configuration)
                .Enrich.WithMachineName()
                .Enrich.WithProcessId()
                .Enrich.WithThreadId()
                .CreateLogger();
        }

        /// <summary>
        /// Logs a debug message
        /// </summary>
        /// <param name="message">Log message</param>
        public void Debug(string message)
            => Log.Debug(message);

        /// <summary>
        /// Logs an error message for the specified <paramref name="exception"/>
        /// </summary>
        /// <param name="exception"><see cref="Exception"/> exception</param>
        public void Error(Exception exception) 
            => Log.Error(exception, exception.Message);

        /// <summary>
        /// Logs an error message
        /// </summary>
        /// <param name="message">Log message</param>
        public void Error(string message)
            => Log.Error(message);

        /// <summary>
        /// Logs a fatal message for the specified <paramref name="exception"/>
        /// </summary>
        /// <param name="exception"><see cref="Exception"/> fatal exception</param>
        public void Fatal(Exception exception)
            => Log.Fatal(exception, exception.Message);

        /// <summary>
        /// Logs an information message
        /// </summary>
        /// <param name="message">Log message</param>
        public void Information(string message)
            => Log.Information(message);

        /// <summary>
        /// Logs a warning message
        /// </summary>
        /// <param name="message">Log message</param>
        public void Warning(string message)
            => Log.Warning(message);
    }
}