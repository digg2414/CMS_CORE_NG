using System.IO;
using Serilog.Events;
using Serilog.Formatting;

namespace LoggingService
{
    public class CustomTextFormatter : ITextFormatter
    {
        public void Format(LogEvent logEvent, TextWriter output)
        {
            throw new System.NotImplementedException();
        }
    }
}