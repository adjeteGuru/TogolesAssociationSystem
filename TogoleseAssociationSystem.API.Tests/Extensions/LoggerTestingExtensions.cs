using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using System;

namespace TogoleseSolidarity.API.Tests.Extensions;

public static class LoggerTestingExtensions
{
    public static void LogDebug(this ILogger logger, string message)
    {
        logger.Log(LogLevel.Debug, message);
    }
    public static void VerifyLogWarning<T>(this Mock<ILogger<T>> logger, string message)
    {
       logger.CheckMessageLogged(LogLevel.Warning, message);
    }
    public static void LogError(this ILogger logger, string message)
    {
        logger.Log(LogLevel.Error, message);
    }

    private static void CheckMessageLogged<T>(this Mock<ILogger<T>> logger, LogLevel level, string message)
    {
        AssertionExtensions.Should(delegate
        {
            logger.Verify((ILogger<T> x) => x.Log(level, It.IsAny<EventId>(), It.Is<It.IsAnyType>((object o, Type t) => string.Equals(message, o.ToString(), StringComparison.InvariantCultureIgnoreCase)), It.IsAny<Exception>(), (Func<It.IsAnyType, Exception, string>)It.IsAny<object>()), Times.Once);
        }).NotThrow<MockException>("", Array.Empty<object>());
    }
}
