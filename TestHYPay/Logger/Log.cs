using System;
using System.Collections.Generic;
using System.Text;

namespace Logger
{
    public class Log
    {
        public static ILogger Logger { get; }

        static Log()
        {
            LoggerContext.SetCurrent(new TraceSourceLoggerFactory());
            Logger = LoggerContext.CreateLog();
        }
    }
}
