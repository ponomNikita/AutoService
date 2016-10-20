using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutoService.Infrastructure.Logger
{
    public class Logger : ILogger
    {
        public readonly log4net.ILog logger;

        public Logger()
        {
            logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        }
        public void Debug(string log)
        {
            logger.Debug(log);
        }

        public void Error(string log, Exception exception = null)
        {
            logger.Error(log, exception);
        }

        public void Info(string log)
        {
            logger.Info(log);
        }
    }
}