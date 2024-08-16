//using FluentAssertions;
//using NUnit.Framework;
//using System;
//using System.Collections.Generic;
//using System.Threading.Tasks;
//using TogoleseAssociationSystem.Application.Database;
//using TogoleseAssociationSystem.Application.Repositories;
//using TogoleseAssociationSystem.Domain.Models;

//namespace TogoleseAssociationSystem.Application.Tests.RepositoriesTests
//{
//    [TestFixture]
//    public class GivenMemberRepositoryIsCalled
//    {
//        private int dbCount;
//        private Member expectedResult;
//        private List<Member> members;
//        private MemberRepository systemUnderTest;
//        private AppDbContext dbContext;

//        [SetUp]
//        public void Setup()
//        {
//            //expectedResult = new Member
//            //{
//            //    Id = Guid.Parse("a703c95e-1807-437e-b947-9d7b33cb9b1f"),
//            //    FirstName = "John",
//            //    LastName = "Doe",
//            //    DateOfBirth = new DateTime(2000, 01, 31),
//            //    IsActive = true,
//            //    IsChair = false,
//            //    MembershipDate = DateTime.Today,
//            //    PhotoUrl = Array.Empty<byte>()
//            //};

//            //members = new List<Member>
//            //{
//            //    new Member
//            //    {
//            //        Id = Guid.Parse("b4bf30e0-8f69-48c4-a7cd-c9a8019b1807"),
//            //        FirstName ="John",
//            //        LastName ="Doe",
//            //        DateOfBirth = new DateTime(2000,01,31),
//            //        IsActive=true,
//            //        IsChair = false,
//            //        MembershipDate = DateTime.Today,
//            //        PhotoUrl = Array.Empty<byte>()
//            //    },
//            //    new Member
//            //    {
//            //       Id = Guid.Parse("cba764d4-883e-4731-aa29-74e2fae8cc11"),
//            //        FirstName ="Brenda",
//            //        LastName ="Love",
//            //        DateOfBirth = new DateTime(1980,11,20),
//            //        IsActive=true,
//            //        IsChair = true,
//            //        MembershipDate = DateTime.Today,
//            //        PhotoUrl = Array.Empty<byte>()
//            //    },
//            //    new Member
//            //    {
//            //        Id = Guid.Parse("9c86fb1c-0941-4d5a-96ff-f6eb919f8b99"),
//            //        FirstName ="Smith",
//            //        LastName ="Joe",
//            //        DateOfBirth = new DateTime(1970,07,30),
//            //        IsActive=true,
//            //        IsChair = false,
//            //        MembershipDate = DateTime.Today,
//            //        PhotoUrl = Array.Empty<byte>()
//            //    },
//            //};
//            CreateTestData();

//            dbContext = DatabaseManager.GetDbContext();
//            systemUnderTest = new MemberRepository(dbContext);
//        }

//        [TearDown]
//        public void Teardown()
//        {
//            dbContext?.Dispose();
//            dbContext = null;

//            using var cleanupContext = DatabaseManager.GetDbContext();
//            DatabaseManager.DeleteAllMembers(cleanupContext);
//        }

//        [Test]
//        public async Task GetMembersAsync_WhenIsInvoked_ThenNoExceptionIsThrown()
//        {
//            Func<Task> func = async () => await systemUnderTest.GetMembersAsync(null);
//            await func.Should().NotThrowAsync();
//        }

//        [Test]
//        public async Task GetMembersAsync_WhenIsInvokedWithNoFilter_ThenTheExpectedCountIsReturned()
//        {
//            var result = await systemUnderTest.GetMembersAsync(null);
//            result.Should().NotBeNull();
//            result.Should().HaveCount(1);
//        }

//        [Test]
//        public async Task GetMembersAsync_WhenIsInvokedWithNoFilter_ThenTheExpectedResultIsReturned()
//        {
//            var result = await systemUnderTest.GetMembersAsync(null);
//            result.Should().NotBeNull();
//            result.Should().BeEquivalentTo(members);
//        }

//        [Test]
//        public async Task GetMembersAsync_WhenIsInvokedWithFilterSupplied_ThenTheExpectedResultIsReturned()
//        {
//            var searchMembers = new List<Member>
//            {
//                expectedResult
//            };

//            var result = await systemUnderTest.GetMembersAsync("Doe");
//            result.Should().BeEquivalentTo(searchMembers);

//        }

//        [Test]
//        public async Task GetMemberByIdAsync_WhenIsInvoked_ThenNoExceptionIsThrown()
//        {
//            Func<Task> func = async () => await systemUnderTest.GetMemberByIdAsync(Guid.Empty);
//            await func.Should().NotThrowAsync();
//        }

//        [Test]
//        public async Task GetMemberByIdAsync_WhenIsInvokedWithAValidId_ThenTheExpectedResultIsReturned()
//        {
//            var result = await systemUnderTest.GetMemberByIdAsync(Guid.Parse("a703c95e-1807-437e-b947-9d7b33cb9b1f"));
//            result.Should().NotBeNull();
//            result.Should().BeEquivalentTo(expectedResult);

//        }

//        private void CreateTestData()
//        {
//            using var dbSetupContext = DatabaseManager.GetDbContext();
//            dbSetupContext.Members.Add(new Member
//            {
//                Id = Guid.Parse("b4bf30e0-8f69-48c4-a7cd-c9a8019b1807"),
//                FirstName = "John",
//                LastName = "Doe",
//                DateOfBirth = new DateTime(2000, 01, 31),
//                IsActive = true,
//                IsChair = false,
//                MembershipDate = DateTime.Today,
//                PhotoUrl = Array.Empty<byte>()
//            });

//            dbSetupContext.SaveChanges();
//        }

//        private void CreateTestDataList()
//        {            

//            using var dbSetupContext = DatabaseManager.GetDbContext();

//            var memberList = new List<Member>();

//            Member member = new Member
//            {
//                Id = Guid.Parse("b4bf30e0-8f69-48c4-a7cd-c9a8019b1807"),
//                FirstName = "John",
//                LastName = "Doe",
//                DateOfBirth = new DateTime(2000, 01, 31),
//                IsActive = true,
//                IsChair = false,
//                MembershipDate = DateTime.Today,
//                PhotoUrl = Array.Empty<byte>()
//            };
//            memberList.Add(member);

//            member = new Member
//            {
//                Id = Guid.Parse("cba764d4-883e-4731-aa29-74e2fae8cc11"),
//                FirstName = "Brenda",
//                LastName = "Love",
//                DateOfBirth = new DateTime(1980, 11, 20),
//                IsActive = true,
//                IsChair = true,
//                MembershipDate = DateTime.Today,
//                PhotoUrl = Array.Empty<byte>()
//            };
//            memberList.Add(member);

//            member = new Member
//            {
//                Id = Guid.Parse("9c86fb1c-0941-4d5a-96ff-f6eb919f8b99"),
//                FirstName = "Smith",
//                LastName = "Joe",
//                DateOfBirth = new DateTime(1970, 07, 30),
//                IsActive = true,
//                IsChair = false,
//                MembershipDate = DateTime.Today,
//                PhotoUrl = Array.Empty<byte>()
//            };
//            memberList.Add(member);

//            dbSetupContext.Members.AddRange(memberList);
//            dbSetupContext.SaveChanges();
//        }
//    }
//}