using System;
using Microsoft.Extensions.Logging;

namespace TestUtilities
{
    public class DummyLogger<T> : ILogger<T>
    {
        public bool WasCalled { get; set; }
        
        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            WasCalled = true;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return true;
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            throw new NotImplementedException();
        }
    }
}