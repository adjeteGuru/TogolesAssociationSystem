using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TogolesAssociationSystem.API.Controllers;
using TogoleseAssociationSystem.Application.Services;
using TogoleseAssociationSystem.Domain.Models;
using Xunit;

namespace TogoleseAssociationSystem.API.Tests.ControllersTests
{
    public class GivenAControllerIsCalled
    {
        private readonly Mock<IMemberService> mockMemberService;
        private readonly MemberController systemUnderTest;
        private readonly Member expectedResult;
        private readonly List<Member> members;

        public GivenAControllerIsCalled()
        {
            expectedResult = new Member
            {
                Id = 1,
                FirstName = "John",
                LastName = "Doe",
                DateOfBirth = new DateTime(2000, 01, 31),
                IsActive = true,
                IsChair = false,
                MembershipDate = DateTime.Today,
                PhotoUrl = null
            };

            members = new List<Member>
            {
                new Member
                {
                    Id = 1,
                    FirstName ="John",
                    LastName ="Doe",
                    DateOfBirth = new DateTime(2000,01,31),
                    IsActive=true,
                    IsChair = false,
                    MembershipDate = DateTime.Today,
                    PhotoUrl = null
                },
                new Member
                {
                    Id = 2,
                    FirstName ="Brenda",
                    LastName ="Love",
                    DateOfBirth = new DateTime(1980,11,20),
                    IsActive=true,
                    IsChair = true,
                    MembershipDate = DateTime.Today,
                    PhotoUrl = null
                },
                new Member
                {
                    Id = 3,
                    FirstName ="Smith",
                    LastName ="Joe",
                    DateOfBirth = new DateTime(1970,07,30),
                    IsActive=true,
                    IsChair = false,
                    MembershipDate = DateTime.Today,
                    PhotoUrl = null
                },
            };

            mockMemberService = new Mock<IMemberService>();
            mockMemberService.Setup(m => m.GetMembersAsync(It.IsAny<string>())).ReturnsAsync(members);
            systemUnderTest = new MemberController(mockMemberService.Object);
        }

        [Fact]
        public void Construnctor_WhenInvokesWithNullMemberService_ThenTheExpectedExceptionIsThrown()
        {
            var act = () => new MemberController(null);
            act.Should().Throw<ArgumentNullException>().And.ParamName.Should().Be("memberService");
        }

        [Fact]
        public void Construnctor_WhenInvokesWithMemberService_ThenInitialize()
        {
            var act = () => new MemberController(mockMemberService.Object);
            act.Should().NotThrow();
        }

        [Fact]
        public async Task GetMembersAsync_WhenInvokes_ThenNoExceptionIsThrown()
        {
            Func<Task> func = async () => await systemUnderTest.GetAllMembersAsync();
            await func.Should().NotThrowAsync();
        }

        [Fact]
        public async Task GetMembersAsync_WhenInvokesWithFilter_ThenTheExpectedResultTypeIsReturned()
        {
            var result = await systemUnderTest.GetAllMembersAsync();
            result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async Task GetMembersAsync_WhenInvokesWithNullFilter_ThenTheExpectedResultIsReturned()
        {
            var result = await systemUnderTest.GetAllMembersAsync();
            var expectedResult = result as ObjectResult;
            expectedResult!.Value.Should().BeEquivalentTo(members);
        }

        [Fact]
        public async Task GetMembersAsync_WhenInvokesWithFilter_ThenTheExpectedResultIsReturned()
        {
            var searchMembers = new List<Member>
            {
                new Member
                {
                    Id = 1,
                    FirstName = "John",
                    LastName = "Doe",
                    DateOfBirth = new DateTime(2000, 01, 31),
                    IsActive = true,
                    IsChair = false,
                    MembershipDate = DateTime.Today,
                    PhotoUrl = null
                }
            };
            mockMemberService.Setup(m => m.GetMembersAsync(It.IsAny<string>())).ReturnsAsync(searchMembers);
            var result = await systemUnderTest.GetAllMembersAsync("Doe");
            var expectedResult = result as ObjectResult;
            expectedResult!.Value.Should().BeEquivalentTo(searchMembers);
        }

        [Fact]
        public async Task GetMembersAsync_WhenInvokesAndSomethingWrongOccured_ThenTheExpectedErrorIsReturned()
        {
            var exception = new Exception("Something wrong happenend, please try later!");
            mockMemberService.Setup(m => m.GetMembersAsync(It.IsAny<string>())).ThrowsAsync(exception);

            var result = await systemUnderTest.GetAllMembersAsync();

            var expectedResult = result as ObjectResult;
            expectedResult!.StatusCode.Should().Be(500, exception.Message);
        }

        [Fact]
        public async Task GetMembersAsync_WhenInvokesAndTheListIsEmpty_ThenTheExpectedErrorIsReturned()
        {
            mockMemberService.Setup(m => m.GetMembersAsync(It.IsAny<string>()))
                .ReturnsAsync(new List<Member>());

            var result = await systemUnderTest.GetAllMembersAsync();

            var expectedResult = result as NotFoundResult;
            expectedResult!.StatusCode.Should().Be(404);
        }
        
        [Fact]
        public async Task GetMemberByIdAsync_WhenInvokesWithId_ThenNoExceptionIsThrown()
        {
            Func<Task> func = async () => await systemUnderTest.GetMemberByIdAsync(1);
            await func.Should().NotThrowAsync();
        }
        
        [Fact]
        public async Task GetMemberByIdAsync_WhenInvokesWithId_ThenTheExpectedResultTypeIsReturned()
        {
            mockMemberService.Setup(m => m.GetMemberByIdAsync(It.IsAny<int>())).ReturnsAsync(expectedResult);
            var result = await systemUnderTest.GetMemberByIdAsync(1);
            result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async Task GetMemberByIdAsync_WhenInvokesWithAValidId_ThenTheExpectedResultIsReturned()
        {
            mockMemberService.Setup(m => m.GetMemberByIdAsync(It.IsAny<int>())).ReturnsAsync(expectedResult);
            var result = (await systemUnderTest.GetMemberByIdAsync(1)) as ObjectResult;
            
            result!.Value.Should().BeEquivalentTo(expectedResult);
        }

        [Fact]
        public async Task GetMemberByIdAsync_WhenInvokesAndSomethingWrongOccured_ThenTheExpectedErrorIsReturned()
        {
            var exception = new Exception("Something wrong happenend, please try later!");
            mockMemberService.Setup(m => m.GetMemberByIdAsync(It.IsAny<int>())).ThrowsAsync(exception);

            var result = await systemUnderTest.GetMemberByIdAsync(1);

            var expectedResult = result as ObjectResult;
            expectedResult!.StatusCode.Should().Be(500, exception.Message);
        }

        [Fact]
        public async Task GetMemberByIdAsync_WhenInvokesAndTheListIsEmpty_ThenTheExpectedErrorIsReturned()
        {
            mockMemberService.Setup(m => m.GetMemberByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((Member)null);

            var result = await systemUnderTest.GetMemberByIdAsync(1);

            var expectedResult = result as NotFoundResult;
            expectedResult!.StatusCode.Should().Be(404);
        }
    }
}