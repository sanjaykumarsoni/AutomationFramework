using log4net.Appender;
using log4net.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationFramework.logger
{
   public  class CustomLogAppender : AppenderSkeleton
    {

        /// <summary>
        /// Writes the logging event with parameters loggingevent class. We can customize the appender.
        /// </summary>
        override protected void Append(LoggingEvent loggingEvent)
        {
            string logDetails = string.Format("{0} {1} {2} {3}", loggingEvent.Level.DisplayName, loggingEvent.LoggerName, Environment.NewLine, loggingEvent.LocationInformation.FullInfo);
        }

        /// <summary>
        /// This appender requires a <see cref="Layout"/> to be set.
        /// </summary>
        override protected bool RequiresLayout
        {
            get { return true; }
        }

    }
}
