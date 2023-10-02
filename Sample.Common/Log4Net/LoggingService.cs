using log4net;
using System;

namespace Sample.Common.Log4Net
{
    public class LoggingService
    {
        protected ILog GetLogger { get; }

        public LoggingService(Type concreteType)
        {
            GetLogger = LogManager.GetLogger(concreteType);
        }
    }
}
