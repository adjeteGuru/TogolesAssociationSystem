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
using TogoleseSolidarity.API.Exceptions;
using TogoleseSolidarity.API.Middleware;
using Xunit;

namespace TogoleseSolidarity.API.Tests.MiddlewareTests;

public class ExceptionHandlerMiddlewareTests
{
    private readonly Mock<RequestDelegate> next;
    private readonly Mock<ILogger<ExceptionHandlerMiddleware>> logger;
    private DefaultHttpContext context;
    private ExceptionHandlerMiddleware systemUnderTest;

    public ExceptionHandlerMiddlewareTests()
    {
        next = new Mock<RequestDelegate>();
        logger = new Mock<ILogger<ExceptionHandlerMiddleware>>();
        context = new DefaultHttpContext
        {
            Response = { Body = new MemoryStream() }
        };
        systemUnderTest = new ExceptionHandlerMiddleware(next.Object, logger.Object);
    }

    [Fact]
    public async Task Invoke_WhenCalled_ShouldCallNext()
    {
        var context = new DefaultHttpContext
        {
            Response = { Body = new MemoryStream() }
        };

        await systemUnderTest.Invoke(context);

        next.Verify(n => n.Invoke(context), Times.Once);
    }

    [Fact]
    public async Task Invoke_WhenExceptionIsThrown_ShouldLogError()
    {
        var context = new DefaultHttpContext
        {
            Response = { Body = new MemoryStream() }
        };
        next.Setup(n => n.Invoke(context)).Throws(new Exception("test"));

        await systemUnderTest.Invoke(context);

        using (new AssertionScope())
        {
            await AssertEmptyBodyWithStatusCode(context, StatusCodes.Status400BadRequest);
        }
    }


    [Fact]
    public async Task Invoke_WhenValidationExceptionIsThrown_ShouldLogError()
    {
        var failedValidations = new List<ValidationFailure>
        {
            new ValidationFailure("test", "test")
        };

        Task RequestDelegate(HttpContext httpContext)
        {
            throw new ValidationException("error", failedValidations);
        }

        var (sut, context) = Setup(RequestDelegate);

        await sut.Invoke(context);

        using (new AssertionScope())
        {
            await AssertEmptyBodyWithStatusCode(context, StatusCodes.Status400BadRequest);
        }
    }

    [Fact]
    public async Task Invoke_WhenNotFoundExceptionIsThrown_ShouldLogError()
    {
        Task RequestDelegate(HttpContext httpContext)
        {
            throw new NotFoundException("error");
        }
        var (sut, context) = Setup(RequestDelegate);
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

    private (ExceptionHandlerMiddleware sut, DefaultHttpContext) Setup(RequestDelegate requestDelegate)
    {
        var context = new DefaultHttpContext
        {
            Response = { Body = new MemoryStream() }
        };
        var sut = new ExceptionHandlerMiddleware(requestDelegate, logger.Object);
        return (sut, context);
    }
}
