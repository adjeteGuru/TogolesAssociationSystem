using FluentAssertions;
using FluentAssertions.Execution;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using TogoleseSolidarity.API.Constants;
using TogoleseSolidarity.API.Exceptions;
using TogoleseSolidarity.API.Middleware;
using TogoleseSolidarity.API.Tests.Extensions;
using Xunit;

namespace TogoleseSolidarity.API.Tests.MiddlewareTests;

public class ExceptionHandlerMiddlewareTests
{
    private readonly Mock<RequestDelegate> next;
    private readonly Mock<ILogger<ExceptionHandlerMiddleware>> logger;
    private ExceptionHandlerMiddleware systemUnderTest;

    public ExceptionHandlerMiddlewareTests()
    {
        logger = new Mock<ILogger<ExceptionHandlerMiddleware>>();
    }

    [Fact]
    public async Task Invoke_WhenCalled_ShouldCallNext()
    {
       var requestDelegate = new Mock<RequestDelegate>();

        var sut = Setup(requestDelegate.Object, out var context);

        await sut.Invoke(context);
        requestDelegate.Verify(x => x(context), Times.Once);
    }


    [Fact]
    public async Task Invoke_WhenValidationExceptionIsThrown_ShouldReturnTheExpectedErrorCode()
    {
        static Task RequestDelegate(HttpContext httpContext)
        {
            throw new ValidationException("error");
        }

        var sut = Setup(RequestDelegate, out var context);

        await sut.Invoke(context);

        await AssertEmptyBodyWithStatusCode(context, StatusCodes.Status400BadRequest);
    }

    [Fact]
    public async Task Invoke_WhenUnknownExceptionIsThrown_ShouldLogTheExpectedErrorCode()
    {
        static Task RequestDelegate(HttpContext httpContext)
        {
            throw new Exception("error");
        }

        var sut = Setup(RequestDelegate, out var context);

        await sut.Invoke(context);

        await AssertEmptyBodyWithStatusCode(context, StatusCodes.Status500InternalServerError);
    }


    //[Fact]
    //public async Task Invoke_WhenValidationExceptionIsThrown_ShouldLogError()
    //{
    //    var failedValidations = new List<ValidationFailure>
    //    {
    //        new ValidationFailure("property1", ErrorCodes.InvalidSurname)
    //    };

    //    static Task RequestDelegate(HttpContext httpContext)
    //    {
    //        throw new ValidationException("error", "r");
    //    }

    //    var sut = Setup(RequestDelegate, out var context);

    //    await sut.Invoke(context);

    //    // Update the expected log message to match the format used in the middleware
    //    var expectedLogMessage = "error, ErrorCodes:- property1: INVALID_SURNAME";
    //    logger.VerifyLogWarning(expectedLogMessage);
    //    //logger.VerifyLogWarning("error, ErrorCodes:- test: INVALID_SURNAME");
    //    //logger.Verify(x => x.Log(
    //    //    It.Is<LogLevel>(l => l == LogLevel.Warning),
    //    //    It.IsAny<EventId>(),
    //    //    It.IsAny<object>(),
    //    //    It.IsAny<Exception>(),
    //    //    It.IsAny<Func<object, Exception?, string>>()
    //    //    ),
    //    //    Times.Once);

    //    //logger.Verify(x => x.Log(
    //    //    It.Is<LogLevel>(l => l == LogLevel.Warning),
    //    //    It.IsAny<EventId>(),
    //    //    It.IsAny<object>(),
    //    //    It.IsAny<Exception>(),
    //    //    It.IsAny<Func<object, Exception, string>>()
    //    //    ),
    //    //    Times.Once);
    //}

    [Fact]
    public async Task Invoke_WhenNotFoundExceptionIsThrown_ShouldLogTheExpectedErrrorCode()
    {
        Task RequestDelegate(HttpContext httpContext)
        {
            throw new NotFoundException("error");
        }
        var sut = Setup(RequestDelegate, out var context);
        await sut.Invoke(context);
        using (new AssertionScope())
        {
            await AssertEmptyBodyWithStatusCode(context, StatusCodes.Status404NotFound);
        }
    }

    private static async Task AssertEmptyBodyWithStatusCode(DefaultHttpContext context, int statusCode)
    {
        var body = await ReadResponseBody(context.Response);
        body.Should().BeEmpty();
        context.Response.StatusCode.Should().Be(statusCode);
    }

    private static Task<string> ReadResponseBody(HttpResponse httpResponse)
    {
        var body = httpResponse.Body;
        body.Position = 0;
        using var reader = new StreamReader(body);
        return reader.ReadToEndAsync();
    }

    private ExceptionHandlerMiddleware Setup(RequestDelegate requestDelegate, out DefaultHttpContext context)
    {
        context = new DefaultHttpContext
        {
            Response = { Body = new MemoryStream() }
        };
        var sut = new ExceptionHandlerMiddleware(requestDelegate, logger.Object);
        return sut;
    }
}
