
using log4net;
using log4net.Config;
using log4net.Core;
using System;
using System.Diagnostics;
using System.Reflection;
using System.Linq;

[assembly: XmlConfigurator(ConfigFile = "Log4net.config", Watch = true)]
namespace Logger
{
    public static class LogRecord
    {

        private static log4net.Core.ILogger logger;
        private static Type logType;
        static LogRecord()
        {
            GlobalContext.Properties["InstanceName"] = Process.GetCurrentProcess().Id;
            var loggers = LoggerManager.GetCurrentLoggers(Assembly.GetCallingAssembly());
            var loggerSystem = loggers.SingleOrDefault(f => f.Name.Equals("LoggerNameSystem", StringComparison.OrdinalIgnoreCase));
            if (loggerSystem == null)
            {
                loggerSystem = loggers.FirstOrDefault(t => t.Name.Equals("LoggerNameRecordData", StringComparison.InvariantCultureIgnoreCase) == false);
            }
            logger = loggerSystem;
            logType = typeof(LogRecord);
        }

        private static Level GetLogLevel(LogLevel level)
        {
            switch (level)
            {
                case LogLevel.Debug:
                    return Level.Debug;
                case LogLevel.Info:
                    return Level.Info;
                case LogLevel.Notice:
                    return Level.Notice;
                case LogLevel.Warn:
                    return Level.Warn;
                case LogLevel.Error:
                    return Level.Error;
                case LogLevel.Fatal:
                    return Level.Fatal;
            }
            return Level.Log4Net_Debug;
        }

        public static void WriteLog(LogLevel level, string message)
        {
            var logLevel = GetLogLevel(level);
            logger.Log(logType, logLevel, message, null);
        }
    }
}