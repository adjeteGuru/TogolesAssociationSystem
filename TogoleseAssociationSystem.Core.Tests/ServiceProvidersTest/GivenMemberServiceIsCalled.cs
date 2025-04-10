﻿using Castle.Core.Logging;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using Moq.Protected;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using TogoleseSolidarity.Core.Models;
using TogoleseSolidarity.Core.ServiceProvider;
using TogoleseSolidarity.Core.ServiceProvider.Interfaces;
using Xunit;

namespace TogoleseSolidarity.Core.Tests.ServiceProvidersTest;

public class GivenMemberServiceIsCalled
{
    private List<Member> members;
    private Mock<HttpMessageHandler> httpMessageHandler;
    private HttpClient httpClient;
    private Mock<IAlertService> mockAlertService;
    private MemberService systemUnderTest;
    private const string memberUrl = "api/member";

    public GivenMemberServiceIsCalled()
    {

        members = new List<Member>
        {
            new Member
            {
                Id = Guid.Parse("b4bf30e0-8f69-48c4-a7cd-c9a8019b1807"),
                FirstName ="John",
                LastName ="Doe",
                DateOfBirth = new DateTime(2000,01,31),
                IsActive=true,
                //IsChair = false,
                MembershipDate = DateTime.Today,
                //PhotoUrl = Array.Empty<byte>()
            },
            new Member
            {
               Id = Guid.Parse("cba764d4-883e-4731-aa29-74e2fae8cc11"),
                FirstName ="Brenda",
                LastName ="Love",
                DateOfBirth = new DateTime(1980,11,20),
                IsActive=true,
                //IsChair = true,
                MembershipDate = DateTime.Today,
                //PhotoUrl = Array.Empty<byte>()
            },
            new Member
            {
                Id = Guid.Parse("9c86fb1c-0941-4d5a-96ff-f6eb919f8b99"),
                FirstName ="Smith",
                LastName ="Joe",
                DateOfBirth = new DateTime(1970,07,30),
                IsActive=true,
                //IsChair = false,
                MembershipDate = DateTime.Today,
                //PhotoUrl = Array.Empty<byte>()
            },
        };
        httpMessageHandler = new Mock<HttpMessageHandler>();

        httpClient = new HttpClient(httpMessageHandler.Object)
        {
            BaseAddress = new Uri("http://dummy.com")
        };

        mockAlertService = new Mock<IAlertService>();
        mockAlertService.Setup(x => x.ShowAlert(It.IsAny<string>()));

        systemUnderTest = new MemberService(httpClient, mockAlertService.Object, Mock.Of<ILogger<MemberService>>());
    }

    [Fact]
    public void GetAllAsync_ThenNoExceptionIsReturned()
    {
        Func<Task> func = async () => await systemUnderTest.GetMembersAsync(1, 5, "test");
        func.Should().NotThrowAsync();
    }

    [Fact]
    public async Task GetMembersAsync_WithAnUriSupplied_ThenSendAsyncMethodIsInvoked()
    {
        var response = new HttpResponseMessage(HttpStatusCode.InternalServerError);

        SetMessageHandler(response);
        await systemUnderTest.GetMembersAsync(1, 5, "filter");

        httpMessageHandler.Protected().Verify<Task<HttpResponseMessage>>("SendAsync", Times.Once(),
            ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>());
    }

    [Fact]
    public async Task GetMembersAsync_WithAValidUrlSupplied_ThenExpectedResponseIsReturned()
    {
        var response = new HttpResponseMessage(HttpStatusCode.OK);
        SetMessageHandler(response);

        httpMessageHandler.Protected().Setup<Task<HttpResponseMessage>>("SendAsync",
          ItExpr.Is<HttpRequestMessage>(x => x.RequestUri.Equals($"http://dummy.com/{memberUrl}/filter")),
          ItExpr.IsAny<CancellationToken>()).ReturnsAsync(response);

        await systemUnderTest.GetMembersAsync(1, 5, "filter");

        httpMessageHandler.Protected().Verify<Task<HttpResponseMessage>>("SendAsync", Times.Once(),
           ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>());
    }

    [Fact]
    public async Task GetMembersAsync_AndTheReturnResponseBadRequest_ThenAlertServiceIsInvokedWithTheErrorMessage()
    {
        var response = new HttpResponseMessage(HttpStatusCode.BadRequest);

        SetMessageHandler(response);

        var result = await systemUnderTest.GetMembersAsync(1, 5, null);
        mockAlertService.Verify(x => x.ShowAlert("Bad request."), Times.Once);
    }

    [Fact]
    public async Task GetMembersAsync_AndTheReturnResponseHasNoContent_ThenExceptionIsCalledWithTheErrorMessage()
    {
        var response = new HttpResponseMessage()
        {
            StatusCode = HttpStatusCode.OK,
            Content = new StringContent(JsonConvert.SerializeObject("bad model"))
        };
        SetMessageHandler(response);

        Func<Task> func = async () => await systemUnderTest.GetMembersAsync(1, 5, "test");
        await func.Should().ThrowAsync<Exception>();
    }

    [Fact]
    public async Task GetMembersAsync_AndTheModelReturnedIsEmpty_ThenResultShouldBeEmpty()
    {
        var response = new HttpResponseMessage()
        {
            StatusCode = HttpStatusCode.NoContent,
        };

        SetMessageHandler(response);

        var result = await systemUnderTest.GetMembersAsync(1, 5, "test");

        result.Should().BeEmpty();
    }

    [Fact]
    public async Task GetMembersAsync_WhenResponseReturnedIsAValidModel_ThenTheExpectedListIsReturned()
    {
        var response = new HttpResponseMessage()
        {
            StatusCode = HttpStatusCode.OK,
            Content = new StringContent(JsonConvert.SerializeObject(members))
        };

        SetMessageHandler(response);

        var result = await systemUnderTest.GetMembersAsync(1, 5, "test");
        result.Should().BeEquivalentTo(members);
    }

    private void SetMessageHandler(HttpResponseMessage response)
    {
        httpMessageHandler.Protected().Setup<Task<HttpResponseMessage>>("SendAsync",
           ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
           .ReturnsAsync(response).Verifiable();
    }
}
