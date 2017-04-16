using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
using log4net.Appender;
using log4net.Layout;
using log4net.Core;
using log4net.Config;

namespace AutomationFramework.logger
{
    /// <summary>
    /// Log4Net appender implementation.
    /// </summary>
    public class Log4NetHelper
    {
        private static ILog _logger;
        private static ConsoleAppender _consoleAppender;
        private static FileAppender _fileAppender;
        private static string layout = "%date{dd.MM.yyyy}[%class][%method][%level]-%message-%newline";

        public static string Layout
        {
            set { layout = value;}
        }
         
        private static PatternLayout GetPatternLayout()
        {
            var patternLayout = new PatternLayout()
            {
                ConversionPattern = layout
            };
            patternLayout.ActivateOptions();
            return patternLayout;
        }
 
        private static ConsoleAppender GetConsoleAppender()
        {
            var consoleAppnder = new ConsoleAppender()
            {
                Name = "ConsoleAppender",
                Layout = GetPatternLayout(),
                Threshold = Level.All

            };
            consoleAppnder.ActivateOptions();
            return consoleAppnder;
        }
        private static FileAppender GetFileAppender()
        {
            var fileAppender = new FileAppender()
            {
                Name = "fileAppender",
                Layout=GetPatternLayout(),
                Threshold=Level.All,
                AppendToFile=true,
                File="FileLogger.log",
            };
            fileAppender.ActivateOptions();
            return fileAppender;
        }
        public static ILog GetLogger(Type type)
        {
            if (_consoleAppender == null)
                _consoleAppender = GetConsoleAppender();
            if (_fileAppender == null)
                _fileAppender = GetFileAppender();
            if (_logger != null)
                return _logger;
            BasicConfigurator.Configure(_consoleAppender);
            BasicConfigurator.Configure(_fileAppender);
            _logger = LogManager.GetLogger(type);
            return _logger;
        }
        //ILog Logger = Log4NetHelper.GetLogger(typeof(Log4NetHelper));
        //Logger.Debug("This is Debug Information");
        //Logger.Info("This is Info Information");
        //Logger.Warn("This is Warn Information");
        //Logger.Error("This is Error Information");
        //Logger.Fatal("This is Fatal Information");
    }
}
