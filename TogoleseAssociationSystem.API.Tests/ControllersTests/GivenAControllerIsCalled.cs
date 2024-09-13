using AutoMapper;
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
        private readonly Mock<IMapper> mockMapper;
        private readonly MemberController systemUnderTest;
        private readonly Member expectedResult;
        private readonly List<Member> members;

        public GivenAControllerIsCalled()
        {
            expectedResult = new Member
            {
                Id = Guid.Parse("a703c95e-1807-437e-b947-9d7b33cb9b1f"),
                FirstName = "John",
                LastName = "Doe",
                DateOfBirth = new DateTime(2000, 01, 31),
                IsActive = true,
                IsChair = false,
                MembershipDate = DateTime.Today,
                PhotoUrl = Array.Empty<byte>()
            };

            members = new List<Member>
            {
                new Member
                {
                    Id = Guid.Parse("b4bf30e0-8f69-48c4-a7cd-c9a8019b1807"),
                    FirstName ="John",
                    LastName ="Doe",
                    DateOfBirth = new DateTime(2000,01,31),
                    IsActive=true,
                    IsChair = false,
                    MembershipDate = DateTime.Today,
                    PhotoUrl = Array.Empty<byte>()
                },
                new Member
                {
                   Id = Guid.Parse("cba764d4-883e-4731-aa29-74e2fae8cc11"),
                    FirstName ="Brenda",
                    LastName ="Love",
                    DateOfBirth = new DateTime(1980,11,20),
                    IsActive=true,
                    IsChair = true,
                    MembershipDate = DateTime.Today,
                    PhotoUrl = Array.Empty<byte>()
                },
                new Member
                {
                    Id = Guid.Parse("9c86fb1c-0941-4d5a-96ff-f6eb919f8b99"),
                    FirstName ="Smith",
                    LastName ="Joe",
                    DateOfBirth = new DateTime(1970,07,30),
                    IsActive=true,
                    IsChair = false,
                    MembershipDate = DateTime.Today,
                    PhotoUrl = Array.Empty<byte>()
                },
            };

            mockMemberService = new Mock<IMemberService>();
            mockMemberService.Setup(m => m.GetMembersAsync(It.IsAny<string>())).ReturnsAsync(members);
            mockMapper = new Mock<IMapper>();
            systemUnderTest = new MemberController(mockMemberService.Object, mockMapper.Object);
        }

        [Fact]
        public void Construnctor_WhenInvokesWithNullMemberService_ThenTheExpectedExceptionIsThrown()
        {
            var act = () => new MemberController(null, mockMapper.Object);
            act.Should().Throw<ArgumentNullException>().And.ParamName.Should().Be("memberService");
        }

        [Fact]
        public void Construnctor_WhenInvokesWithMemberService_ThenInitialize()
        {
            var act = () => new MemberController(mockMemberService.Object, mockMapper.Object);
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

            var returnedMembers = new List<Member>
            {
                new Member
                {
                    Id = Guid.Parse("b4bf30e0-8f69-48c4-a7cd-c9a8019b1807"),
                    FirstName ="John",
                    LastName ="Doe",
                    DateOfBirth = new DateTime(2000,01,31),
                    IsActive=true,
                    IsChair = false,
                    MembershipDate = DateTime.Today,
                    PhotoUrl = Array.Empty<byte>()
                },
                new Member
                {
                   Id = Guid.Parse("cba764d4-883e-4731-aa29-74e2fae8cc11"),
                    FirstName ="Brenda",
                    LastName ="Love",
                    DateOfBirth = new DateTime(1980,11,20),
                    IsActive=true,
                    IsChair = true,
                    MembershipDate = DateTime.Today,
                    PhotoUrl = Array.Empty<byte>()
                },
                new Member
                {
                    Id = Guid.Parse("9c86fb1c-0941-4d5a-96ff-f6eb919f8b99"),
                    FirstName ="Smith",
                    LastName ="Joe",
                    DateOfBirth = new DateTime(1970,07,30),
                    IsActive=true,
                    IsChair = false,
                    MembershipDate = DateTime.Today,
                    PhotoUrl = Array.Empty<byte>()
                },
            };
            mockMapper.Setup(x => x.Map<List<Member>>(It.IsAny<List<Member>>())).Returns(returnedMembers);
            var result = await systemUnderTest.GetAllMembersAsync();
            var expectedResult = result as ObjectResult;
            expectedResult!.Value.Should().BeEquivalentTo(returnedMembers);
        }

        [Fact]
        public async Task GetMembersAsync_WhenInvokesWithFilter_ThenTheExpectedResultIsReturned()
        {
            var searchMembers = new List<Member>
            {
                new Member
            {
                Id = Guid.Parse("a703c95e-1807-437e-b947-9d7b33cb9b1f"),
                FirstName = "John",
                LastName = "Doe",
                DateOfBirth = new DateTime(2000, 01, 31),
                IsActive = true,
                IsChair = false,
                MembershipDate = DateTime.Today,
                PhotoUrl = Array.Empty<byte>()
            }
        };

            var returnedMembers = new List<Member>
            {
               new Member
            {
                Id = Guid.Parse("a703c95e-1807-437e-b947-9d7b33cb9b1f"),
                FirstName = "John",
                LastName = "Doe",
                DateOfBirth = new DateTime(2000, 01, 31),
                IsActive = true,
                IsChair = false,
                MembershipDate = DateTime.Today,
                PhotoUrl = Array.Empty<byte>()
            }
        };

            mockMemberService.Setup(m => m.GetMembersAsync(It.IsAny<string>())).ReturnsAsync(searchMembers);
         
            var result = await systemUnderTest.GetAllMembersAsync("Doe");
            var expectedResult = result as ObjectResult;
            expectedResult!.Value.Should().BeEquivalentTo(returnedMembers);
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
            Func<Task> func = async () => await systemUnderTest.GetMemberById(Guid.Empty);
            await func.Should().NotThrowAsync();
        }

        [Fact]
        public async Task GetMemberByIdAsync_WhenInvokesWithId_ThenTheExpectedResultTypeIsReturned()
        {
            mockMemberService.Setup(m => m.GetMemberByIdAsync(It.IsAny<Guid>())).ReturnsAsync(expectedResult);
            var result = await systemUnderTest.GetMemberById(Guid.Empty);
            result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async Task GetMemberByIdAsync_WhenInvokesWithAValidId_ThenTheExpectedResultIsReturned()
        {
            var returnedMember = new Member
            {
                Id = Guid.Parse("a703c95e-1807-437e-b947-9d7b33cb9b1f"),
                FirstName = "John",
                LastName = "Doe",
                DateOfBirth = new DateTime(2000, 01, 31),
                IsActive = true,
                IsChair = false,
                MembershipDate = DateTime.Today,
                PhotoUrl = Array.Empty<byte>()
            };
            mockMemberService.Setup(m => m.GetMemberByIdAsync(It.IsAny<Guid>())).ReturnsAsync(expectedResult);

            var result = (await systemUnderTest.GetMemberById(Guid.Empty)) as ObjectResult;

            result!.Value.Should().BeEquivalentTo(returnedMember);
        }

        [Fact]
        public async Task GetMemberByIdAsync_WhenInvokesAndSomethingWrongOccured_ThenTheExpectedErrorIsReturned()
        {
            var exception = new Exception("Something wrong happenend, please try later!");
            mockMemberService.Setup(m => m.GetMemberByIdAsync(It.IsAny<Guid>())).ThrowsAsync(exception);

            var result = await systemUnderTest.GetMemberById(Guid.Empty);

            var expectedResult = result as ObjectResult;
            expectedResult!.StatusCode.Should().Be(500, exception.Message);
        }

        [Fact]
        public async Task GetMemberByIdAsync_WhenInvokesAndNullIsReturned_ThenTheExpectedErrorIsReturned()
        {
            mockMemberService.Setup(m => m.GetMemberByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync((Member)null);

            var result = await systemUnderTest.GetMemberById(Guid.Empty);

            var expectedResult = result as NotFoundResult;
            expectedResult!.StatusCode.Should().Be(404);
        }
    }
}