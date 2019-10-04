using Microsoft.Extensions.Logging;

namespace Wyd.Engine.Logging
{
    public static class Logger
    {
        private static readonly ILoggerFactory _LoggerFactory;
        private static readonly ILogger _Logger;
        
        static Logger()
        {
            _LoggerFactory = LoggerFactory.Create(builder => { builder.AddConsole(); });
            _Logger = _LoggerFactory.CreateLogger("Console");
        }

        public static void Log(LogLevel logLevel, string message, params object[] args)
        {
            _Logger.Log(logLevel, message, args);
        }
    }
}
