using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TogoleseAssociationSystem.Application.Database;
using TogoleseAssociationSystem.Application.Repositories;
using TogoleseAssociationSystem.Domain.Models;

namespace TogoleseAssociationSytem.Infrastructure.Tests.RepositoriesTests
{
    [TestFixture]
    public class GivenMemberRepositoryIsCalled
    {
        private AppDbContext dbContext;
        private MemberRepository systemUnderTest;
        private Member expectedResult;
        private List<Member> members;

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
                IsChair = false,
                MembershipDate = DateTime.Today,
                PhotoUrl = Array.Empty<byte>()
            };

            members = new List<Member>
            {
                new Member
                {
                    Id = Guid.Parse("a703c95e-1807-437e-b947-9d7b33cb9b1f"),
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
        }

        [TearDown]
        public void Teardown()
        {
            dbContext?.Dispose();
        }

        [Test]
        public async Task GetMembersAsync_WhenIsInvoked_ThenNoExceptionIsThrown()
        {
            await using var context = GetContext();
            context.Members.AddRange(members);
            await context.SaveChangesAsync();
            var systemUnderTest = new MemberRepository(context);

            Func<Task> func = async () => await systemUnderTest.GetMembersAsync(null);
            await func.Should().NotThrowAsync();
        }

        [Test]
        public async Task GetMembersAsync_WhenIsInvokedWithNoFilter_ThenTheExpectedCountIsReturned()
        {
            var members = CreateTestDataList();

            await using var context = GetContext();
            context.Members.AddRange(members);
            await context.SaveChangesAsync();
            var systemUnderTest = new MemberRepository(context);

            var result = await systemUnderTest.GetMembersAsync(null);
            result.Should().NotBeNull();
            result.Should().HaveCount(3);
        }

        [Test]
        public async Task GetMembersAsync_WhenIsInvokedWithNoFilter_ThenTheExpectedResultIsReturned()
        {
            var members = CreateTestDataList();

            await using var context = GetContext();
            context.Members.AddRange(members);
            await context.SaveChangesAsync();
            var systemUnderTest = new MemberRepository(context);


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

            await using var context = GetContext();
            context.Members.AddRange(members);
            await context.SaveChangesAsync();
            var systemUnderTest = new MemberRepository(context);

            var result = await systemUnderTest.GetMembersAsync("Doe");
            result.Should().BeEquivalentTo(searchMembers);

        }

        [Test]
        public async Task GetMemberByIdAsync_WhenIsInvoked_ThenNoExceptionIsThrown()
        {
            await using var context = GetContext();
            context.Members.AddRange(members);
            await context.SaveChangesAsync();
            var systemUnderTest = new MemberRepository(context);

            Func<Task> func = async () => await systemUnderTest.GetMemberByIdAsync(Guid.Empty);
            await func.Should().NotThrowAsync();
        }

        [Test]
        public async Task GetMemberByIdAsync_WhenIsInvokedWithAValidId_ThenTheExpectedResultIsReturned()
        {
            await using var context = GetContext();
            context.Members.AddRange(members);
            await context.SaveChangesAsync();
            var systemUnderTest = new MemberRepository(context);

            var result = await systemUnderTest.GetMemberByIdAsync(Guid.Parse("a703c95e-1807-437e-b947-9d7b33cb9b1f"));
            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(expectedResult);

        }

        private AppDbContext GetContext()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: $"WebApp_{Guid.NewGuid()}")
                .Options;
            return new AppDbContext(options);
        }

        private List<Member> CreateTestDataList()
        {
            var memberList = new List<Member>();

            Member member = new Member
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
            memberList.Add(member);

            member = new Member
            {
                Id = Guid.Parse("cba764d4-883e-4731-aa29-74e2fae8cc11"),
                FirstName = "Brenda",
                LastName = "Love",
                DateOfBirth = new DateTime(1980, 11, 20),
                IsActive = true,
                IsChair = true,
                MembershipDate = DateTime.Today,
                PhotoUrl = Array.Empty<byte>()
            };
            memberList.Add(member);

            member = new Member
            {
                Id = Guid.Parse("9c86fb1c-0941-4d5a-96ff-f6eb919f8b99"),
                FirstName = "Smith",
                LastName = "Joe",
                DateOfBirth = new DateTime(1970, 07, 30),
                IsActive = true,
                IsChair = false,
                MembershipDate = DateTime.Today,
                PhotoUrl = Array.Empty<byte>()
            };
            memberList.Add(member);

            return memberList;
        }
    }
}