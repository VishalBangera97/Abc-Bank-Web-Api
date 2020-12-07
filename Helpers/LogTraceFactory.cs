using NLog;

namespace ABCBankWebApi.Helpers
{
    public static class LogTraceFactory
    {
        private static ILogger logger = LogManager.GetCurrentClassLogger();

        public static void LogDebug(string message)
        {
            logger.Debug(message);
        }
        public static void LogError(short statusCode,string message)
        {
            logger.Error("Something went wrong: Status Code :"+statusCode+". Error is : "+message);
        }
        public static void LogInfo(string message)
        {
            logger.Info(message);
        }
        public static void LogWarn(string message)
        {
            logger.Warn(message);
        }
    }
}
