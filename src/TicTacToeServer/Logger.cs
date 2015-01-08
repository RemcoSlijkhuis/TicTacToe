using System;
using NLog;

namespace TicTacToeServer
{
    /// <summary>
    /// Logging service for the console/service application using NLog
    /// </summary>
    public class Logger
    {
        /// <summary>
        /// Singleton instance of the logger
        /// </summary>
        public static Logger Current
        {
            get
            {
                if (current == null)
                {
                    lock (SyncRoot)
                    {
                        if (current == null)
                            current = new Logger();
                    }
                }

                return current;
            }
        }

        private static Logger current;
        private static readonly object SyncRoot = new object();

        private NLog.Logger logger;
        
        public Logger()
        {
            logger = LogManager.GetCurrentClassLogger();
        }

        private LogEventInfo CreateLogEventInfo(LogLevel logLevel, string message, params object[] args)
        {
            return new LogEventInfo(logLevel, logger.Name, (args.Length > 0) ? string.Format(message, args) : message);
        }

        private LogEventInfo CreateLogEventInfo(LogLevel logLevel, Exception ex)
        {
            return new LogEventInfo(logLevel, logger.Name, null, "Exception", null, ex);
        }

        private LogEventInfo CreateLogEventInfo(LogLevel logLevel, Exception ex, string message, params object[] args)
        {
            return new LogEventInfo(logLevel, logger.Name, null, message, args, ex);
        }

        private void LogInternal(LogLevel type, Exception ex)
        {
            if(logger != null)
                logger.Log(GetType(), CreateLogEventInfo(type, ex));
        }

        private void LogInternal(LogLevel type, Exception ex, string message, params object[] args)
        {
            if (logger != null)
                logger.Log(GetType(), CreateLogEventInfo(type, ex, message, args));
        }

        private void LogInternal(string message, params object[] args)
        {
            if (logger != null)
                logger.Log(GetType(), CreateLogEventInfo(LogLevel.Info, message, args));
        }

        private void LogInternal(LogLevel type, string message, params object[] args)
        {
            if (logger != null)
                logger.Log(GetType(), CreateLogEventInfo(type, message, args));
        }

        public void Log(string message, LogLevel type, params object[] args)
        {
            LogInternal(type, message, args);
        }

        public void Info(string message, params object[] args)
        {
            LogInternal(message, args);
        }

        public void Debug(string message, params object[] args)
        {
            LogInternal(LogLevel.Debug, message, args);
        }

        public void Warn(Exception ex)
        {
            LogInternal(LogLevel.Warn, ex);
        }

        public void Warn(string message, params object[] args)
        {
            LogInternal(LogLevel.Warn, message, args);
        }

        public void Warn(string message, Exception ex, params object[] args)
        {
            LogInternal(LogLevel.Warn, ex, message, args);
        }

        public void Error(string message, params object[] args)
        {
            LogInternal(LogLevel.Error, message, args);
        }

        public void Error(Exception ex)
        {
            LogInternal(LogLevel.Error, ex);
        }

        public void Error(string message, Exception ex, params object[] args)
        {
            LogInternal(LogLevel.Error, ex, message, args);
        }

        public void Fatal(string message, params object[] args)
        {
            LogInternal(LogLevel.Fatal, message, args);
        }

        public void Fatal(Exception ex)
        {
            LogInternal(LogLevel.Fatal, ex);
        }

        public void Fatal(string message, Exception ex, params object[] args)
        {
            LogInternal(LogLevel.Fatal, ex, message, args);
        }

        public void Dispose()
        {
            logger.Factory.Dispose();
            logger = null;
        }
    }
}
