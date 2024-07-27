using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TogoleseAssociationSystem.Application.Repositories;
using TogoleseAssociationSystem.Application.Services;
using TogoleseAssociationSystem.Domain.Models;
using Xunit;

namespace TogoleseAssociationSystem.Application.Tests.ServicesTests
{
    public class GivenMemberServiceIsCalled
    {
        private readonly Member expectedResult;
        private readonly List<Member> members;
        private Mock<IMemberRepository> mockMemberRepository;
        private MemberService systemUnderTest;

        public GivenMemberServiceIsCalled()
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

            mockMemberRepository = new Mock<IMemberRepository>();
            mockMemberRepository.Setup(m => m.GetMembersAsync(It.IsAny<string>())).ReturnsAsync(members);
            mockMemberRepository.Setup(m => m.GetMemberByIdAsync(It.IsAny<int>())).ReturnsAsync(expectedResult);

            systemUnderTest = new MemberService(mockMemberRepository.Object);
        }

        [Fact]
        public async Task GetMembersAsync_WhenIsInvoked_ThenNoExceptionIsThrown()
        {
            Func<Task> func = async () => await systemUnderTest.GetMembersAsync(null);
            await func.Should().NotThrowAsync();
        }

        [Fact]
        public async Task GetMembersAsync_WhenIsInvokedWithNoFilter_ThenTheExpectedCountIsReturned()
        {
            var result = await systemUnderTest.GetMembersAsync(null);
            result.Should().NotBeNull();
            result.Should().HaveCount(3);
        }

        [Fact]
        public async Task GetMembersAsync_WhenIsInvokedWithNoFilter_ThenTheExpectedResultIsReturned()
        {
            var result = await systemUnderTest.GetMembersAsync(null);
            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(members);
        }

        [Fact]
        public async Task GetMembersAsync_WhenIsInvokedWithFilterSupplied_ThenTheExpectedResultIsReturned()
        {
            var searchMembers = new List<Member>
            {
                expectedResult
            };
            mockMemberRepository.Setup(m => m.GetMembersAsync(It.IsAny<string>())).ReturnsAsync(searchMembers);

            var result = await systemUnderTest.GetMembersAsync("Doe");
            result.Should().BeEquivalentTo(searchMembers);
        }

        [Fact]
        public async Task GetMembersAsync_WhenIsInvokedWithFilterSuppliedAndTheListIsEmpty_ThenTheExpectedErrorIsReturned()
        {
            var emptyMembers = new List<Member>();
            var exception = new Exception("There is no match members found in the db!");
            mockMemberRepository.Setup(m => m.GetMembersAsync(It.IsAny<string>())).ReturnsAsync(emptyMembers);

            Func<Task> func = async () => await systemUnderTest.GetMembersAsync("wrongName");
            await func.Should().ThrowAsync<Exception>(exception.Message);
        }

        [Fact]
        public async Task GetMemberByIdAsync_WhenIsInvoked_ThenNoExceptionIsThrown()
        {
            Func<Task> func = async () => await systemUnderTest.GetMemberByIdAsync(1);
            await func.Should().NotThrowAsync();
        }

        [Fact]
        public async Task GetMemberByIdAsync_WhenIsInvokedWithAValidId_ThenTheExpectedResultIsReturned()
        {
            var result = await systemUnderTest.GetMemberByIdAsync(1);
            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(expectedResult);
        }

        [Fact]
        public async Task GetMemberByIdAsync_WhenIsInvokedWithAnInvalidId_ThenTheExpectedErrorIsReturned()
        {
            var id = 100;
            var exception = new Exception($"member with id:{id} is not found!");
            mockMemberRepository.Setup(m => m.GetMemberByIdAsync(It.IsAny<int>())).ReturnsAsync((Member)null);

            Func<Task> func = async () => await systemUnderTest.GetMemberByIdAsync(id);
            await func.Should().ThrowAsync<Exception>(exception.Message);
        }
    }
}
