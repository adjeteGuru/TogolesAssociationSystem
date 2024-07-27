using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TogoleseAssociationSystem.Application.Repositories;
using TogoleseAssociationSystem.Domain.Models;
using Xunit;

namespace TogoleseAssociationSystem.Application.Tests.RepositoriesTests
{
    public class GivenMemberRepositoryIsCalled
    {
        private List<Member> members;
        private MemberRepository systemUnderTest;

        public GivenMemberRepositoryIsCalled()
        {
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

            systemUnderTest = new MemberRepository();
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
            //var members = new List<Member>
            //{
            //    new Member { Id = 1,FirstName ="test", LastName ="test", DateOfBirth = DateTime.Today, IsActive=true, IsChair=false},
            //     new Member { Id = 2,FirstName ="test", LastName ="test", DateOfBirth = DateTime.Today, IsActive=true, IsChair=false},
            //      new Member { Id = 3,FirstName ="test", LastName ="test", DateOfBirth = DateTime.Today, IsActive=true, IsChair=true},
            //};

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
    }
}