using FluentAssertions;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using TogoleseAssociationSystem.Application.Services;
using TogoleseAssociationSystem.Domain.Interfaces;
using TogoleseAssociationSystem.Domain.Models;
namespace TogoleseAssociationSystem.Application.Test.ServicesTests
{
    public class GivenMemberServiceIsCalled
    {
        private Member expectedResult;
        private List<Member> members;
        private Mock<IMemberRepository> mockMemberRepository;
        private MemberService systemUnderTest;

        [SetUp]
        public void Setup()
        {
            expectedResult = new Member
            {
                Id = Guid.Parse("a703c95e-1807-437e-b947-9d7b33cb9b1f"),
                FirstName = "John",
                LastName = "Doe",
                DateOfBirth = new DateTime(2000, 01, 31),
                IsActive = true,
                IsEligibleToClaim = false,
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
                    IsEligibleToClaim = false,
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
                    IsEligibleToClaim = true,
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
                    IsEligibleToClaim = false,
                    MembershipDate = DateTime.Today,
                    PhotoUrl = Array.Empty<byte>()
                },
            };

            mockMemberRepository = new Mock<IMemberRepository>();
            mockMemberRepository.Setup(m => m.GetMembersAsync(It.IsAny<string>())).ReturnsAsync(members);
            mockMemberRepository.Setup(m => m.GetMemberByIdAsync(It.IsAny<Guid>())).ReturnsAsync(expectedResult);

            systemUnderTest = new MemberService(mockMemberRepository.Object);
        }

        [Test]
        public async Task GetMembersAsync_WhenIsInvoked_ThenNoExceptionIsThrown()
        {
            Func<Task> func = async () => await systemUnderTest.GetMembersAsync(null);
            await func.Should().NotThrowAsync();
        }

        [Test]
        public async Task GetMembersAsync_WhenIsInvokedWithNoFilter_ThenTheExpectedCountIsReturned()
        {
            var result = await systemUnderTest.GetMembersAsync(null);
            result.Should().NotBeNull();
            result.Should().HaveCount(3);
        }

        [Test]
        public async Task GetMembersAsync_WhenIsInvokedWithNoFilter_ThenTheExpectedResultIsReturned()
        {
            var result = await systemUnderTest.GetMembersAsync(null);
            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(members);
        }

        [Test]
        public async Task GetMembersAsync_WhenIsInvokedWithFilterSupplied_ThenTheExpectedResultIsReturned()
        {
            var searchMembers = new List<Member>
            {
                expectedResult
            };
            mockMemberRepository.Setup(m => m.GetMembersAsync(It.IsAny<string>())).ReturnsAsync(searchMembers);

            var result = await systemUnderTest.GetMembersAsync("Doe");
            result.Should().BeEquivalentTo(
                new List<Member> 
                {
                    new Member
                    {
                        Id = searchMembers[0].Id,
                        FirstName = searchMembers[0].FirstName,
                        LastName = searchMembers[0].LastName,
                        DateOfBirth = searchMembers[0].DateOfBirth,
                        IsActive = searchMembers[0].IsActive,
                        IsEligibleToClaim = searchMembers[0].IsEligibleToClaim,
                        MembershipDate = searchMembers[0].MembershipDate,
                        PhotoUrl = searchMembers[0].PhotoUrl,
                        NextOfKin = searchMembers[0].NextOfKin,
                        Claims = searchMembers[0].Claims,
                        Memberships = searchMembers[0].Memberships
                    }
                });
        }

        [Test]
        public async Task GetMembersAsync_WhenIsInvokedWithFilterSuppliedAndTheListIsEmpty_ThenTheExpectedErrorIsReturned()
        {
            var emptyMembers = new List<Member>();
            var exception = new Exception("There is no match members found in the db!");
            mockMemberRepository.Setup(m => m.GetMembersAsync(It.IsAny<string>())).ReturnsAsync(emptyMembers);

            Func<Task> func = async () => await systemUnderTest.GetMembersAsync("wrongName");
            await func.Should().ThrowAsync<Exception>(exception.Message);
        }

        [Test]
        public async Task GetMemberByIdAsync_WhenIsInvoked_ThenNoExceptionIsThrown()
        {
            Func<Task> func = async () => await systemUnderTest.GetMemberByIdAsync(Guid.Empty);
            await func.Should().NotThrowAsync();
        }

        [Test]
        public async Task GetMemberByIdAsync_WhenIsInvokedWithAValidId_ThenTheExpectedResultIsReturned()
        {
            var result = await systemUnderTest.GetMemberByIdAsync(Guid.Empty);
            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(expectedResult);
        }

        [Test]
        public async Task GetMemberByIdAsync_WhenIsInvokedWithAnInvalidId_ThenTheExpectedErrorIsReturned()
        {
            var id = Guid.Empty;
            var exception = new Exception($"member with id:{id} is not found!");
            mockMemberRepository.Setup(m => m.GetMemberByIdAsync(It.IsAny<Guid>())).ReturnsAsync((Member)null);

            Func<Task> func = async () => await systemUnderTest.GetMemberByIdAsync(id);
            await func.Should().ThrowAsync<Exception>(exception.Message);
        }
    }
}
