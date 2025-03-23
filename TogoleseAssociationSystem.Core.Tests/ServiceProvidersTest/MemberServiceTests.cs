using FluentAssertions;
using Moq;
using Moq.Protected;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using TogoleseSolidarity.Core.DTOs;
using TogoleseSolidarity.Core.ServiceProvider;
using TogoleseSolidarity.Core.ServiceProvider.Interfaces;
using Xunit;

namespace TogoleseSolidarity.Core.Tests.ServiceProvidersTest;

public class MemberServiceTests
{
    //private readonly Mock<HttpClient> mockHttpClient;
    private readonly HttpClient mockHttpClient;
    private readonly Mock<IAlertService> mockAlertService;
    private readonly MemberService memberService;
    private Mock<HttpMessageHandler> httpMessageHandler;

    public MemberServiceTests()
    {
        //mockHttpClient = new Mock<HttpClient>();
        httpMessageHandler = new Mock<HttpMessageHandler>();

        mockHttpClient = new HttpClient(httpMessageHandler.Object)
        {
            BaseAddress = new Uri("http://dummy.com")
        };
        mockAlertService = new Mock<IAlertService>();
        memberService = new MemberService(mockHttpClient, mockAlertService.Object);
    }

    [Fact]
    public async Task GetMemberByIdAsync_ReturnsMember_WhenMemberExists()
    {
        // Arrange
        var memberId = Guid.NewGuid();
        var memberRead = new MemberRead { Id = memberId };
        var responseMessage = new HttpResponseMessage(HttpStatusCode.OK)
        {
            Content = new StringContent(JsonConvert.SerializeObject(memberRead))
        };
        SetMessageHandler(responseMessage);
        //mockHttpClient.Setup(client => client.GetAsync(It.IsAny<string>())).ReturnsAsync(responseMessage);

        // Act
        var result = await memberService.GetMemberByIdAsync(memberId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(memberId, result.Id);
    }

    [Fact]
    public async Task GetMembersAsync_ReturnsMembers_WhenMembersExist()
    {
        // Arrange
        var members = new List<MemberRead> { new MemberRead() };
        var responseMessage = new HttpResponseMessage(HttpStatusCode.OK)
        {
            Content = new StringContent(JsonConvert.SerializeObject(members))
        };

        SetMessageHandler(responseMessage);
        // Act
        var result = await memberService.GetMembersAsync(1, 10);

        // Assert
        result.Should().BeEquivalentTo(members);
    }

    [Fact]
    public async Task CreateMemberAsync_ReturnsMember_WhenMemberIsCreated()
    {
        // Arrange
        var memberToAdd = new MemberToAdd();
        var memberRead = new MemberRead();
        var responseMessage = new HttpResponseMessage(HttpStatusCode.OK)
        {
            Content = new StringContent(JsonConvert.SerializeObject(memberRead))
        };

        SetMessageHandler(responseMessage);
        // Act
        var result = await memberService.CreateMemberAsync(memberToAdd);

        // Assert
        result.Should().BeEquivalentTo(memberRead);
    }

    [Fact]
    public async Task CreateMembershipAsync_ReturnsMembership_WhenMembershipIsCreated()
    {
        // Arrange
        var contributionToAdd = new MembershipContributionToAdd();
        var membershipRead = new MembershipContributionReadDto();
        var responseMessage = new HttpResponseMessage(HttpStatusCode.OK)
        {
            Content = new StringContent(JsonConvert.SerializeObject(membershipRead))
        };

        SetMessageHandler(responseMessage);
        // Act
        var result = await memberService.CreateMembershipAsync(contributionToAdd);

        // Assert
        result.Should().BeEquivalentTo(membershipRead);
    }

    [Fact]
    public async Task UpdateMemberDetails_ReturnsUpdatedMember_WhenMemberIsUpdated()
    {
        // Arrange
        var memberUpdate = new MemberUpdateDto { Id = Guid.NewGuid() };
        var memberRead = new MemberRead();
        var responseMessage = new HttpResponseMessage(HttpStatusCode.OK)
        {
            Content = new StringContent(JsonConvert.SerializeObject(memberRead))
        };
        SetMessageHandler(responseMessage);

        // Act
        var result = await memberService.UpdateMemberDetails(memberUpdate);

        // Assert
        result.Should().BeEquivalentTo(memberRead);
    }

    [Fact]
    public async Task GetAllExistingMembersAsync_ReturnsMembers_WhenMembersExist()
    {
        // Arrange
        var members = new List<MemberRead> { new MemberRead() };
        var responseMessage = new HttpResponseMessage(HttpStatusCode.OK)
        {
            Content = new StringContent(JsonConvert.SerializeObject(members))
        };
        SetMessageHandler(responseMessage);

        // Act
        var result = await memberService.GetAllExistingMembersAsync();

        // Assert
        result.Should().BeEquivalentTo(members);
    }

    [Fact]
    public async Task GetAllMembershipsAsync_ReturnsMemberships_WhenMembershipsExist()
    {
        // Arrange
        var memberships = new List<MembershipContributionReadDto> { new MembershipContributionReadDto() };
        var responseMessage = new HttpResponseMessage(HttpStatusCode.OK)
        {
            Content = new StringContent(JsonConvert.SerializeObject(memberships))
        };
        SetMessageHandler(responseMessage);

        // Act
        var result = await memberService.GetAllMembershipsAsync();

        // Assert
        result.Should().BeEquivalentTo(memberships);
    }

    [Fact]
    public async Task CreateClaimAsync_ReturnsClaim_WhenClaimIsCreated()
    {
        // Arrange
        var claimToAdd = new ClaimToAdd();
        var claimRead = new ClaimReadDto();
        var responseMessage = new HttpResponseMessage(HttpStatusCode.OK)
        {
            Content = new StringContent(JsonConvert.SerializeObject(claimRead))
        };

        SetMessageHandler(responseMessage);
        // Act
        var result = await memberService.CreateClaimAsync(claimToAdd);

        // Assert
        result.Should().BeEquivalentTo(claimRead);
    }

    [Fact]
    public async Task GetClaimByIdAsync_ReturnsClaim_WhenClaimExists()
    {
        // Arrange
        var claimId = Guid.NewGuid();
        var claimRead = new ClaimReadDto { Id = claimId };
        var responseMessage = new HttpResponseMessage(HttpStatusCode.OK)
        {
            Content = new StringContent(JsonConvert.SerializeObject(claimRead))
        };
        SetMessageHandler(responseMessage);

        // Act
        var result = await memberService.GetClaimByIdAsync(claimId);

        // Assert
        result.Should().BeEquivalentTo(claimRead);
    }

    [Fact]
    public async Task UpdateClaimAsync_UpdatesClaim_WhenClaimIsUpdated()
    {
        // Arrange
        var claimUpdate = new ClaimToUpdate { Id = Guid.Empty, Name = "test" };
        var responseMessage = new HttpResponseMessage(HttpStatusCode.OK)
        {
            Content = new StringContent(JsonConvert.SerializeObject(new ClaimReadDto() { Id = Guid.Empty, Name = "new test" }))
        };
        SetMessageHandler(responseMessage);

        // Act
        await memberService.UpdateClaimAsync(claimUpdate);

        // Assert
        mockAlertService.Verify(service => service.ShowAlert(It.IsAny<string>()), Times.Never);
    }

    [Fact]
    public async Task GetClaimsByMemberIdAsync_ReturnsClaims_WhenClaimsExist()
    {
        // Arrange
        var memberId = Guid.NewGuid();
        var claims = new List<ClaimReadDto> { new ClaimReadDto() };
        var responseMessage = new HttpResponseMessage(HttpStatusCode.OK)
        {
            Content = new StringContent(JsonConvert.SerializeObject(claims))
        };
        SetMessageHandler(responseMessage);

        // Act
        var result = await memberService.GetClaimsByMemberIdAsync(memberId);

        // Assert
        result.Should().BeEquivalentTo(claims);
    }

    private void SetMessageHandler(HttpResponseMessage response)
    {
        httpMessageHandler.Protected().Setup<Task<HttpResponseMessage>>("SendAsync",
           ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
           .ReturnsAsync(response).Verifiable();
    }
}
