//using FluentAssertions;
//using System;
//using System.Collections.Generic;
//using System.Threading.Tasks;
//using TogoleseAssociationSystem.Application.Repositories;
//using TogoleseAssociationSystem.Domain.Models;
//using Xunit;

//namespace TogoleseAssociationSystem.Application.Tests.RepositoriesTests
//{
//    public class GivenMemberRepositoryIsCalled
//    {
//        private Member expectedResult;
//        private List<Member> members;
//        private MemberRepository systemUnderTest;

//        public GivenMemberRepositoryIsCalled()
//        {
//            expectedResult = new Member
//            {
//                Id = 1,
//                FirstName = "John",
//                LastName = "Doe",
//                DateOfBirth = new DateTime(2000, 01, 31),
//                IsActive = true,
//                IsChair = false,
//                MembershipDate = DateTime.Today,
//                PhotoUrl = null
//            };

//            members = new List<Member>
//            {
//                new Member
//                {
//                    Id = 1,
//                    FirstName ="John",
//                    LastName ="Doe",
//                    DateOfBirth = new DateTime(2000,01,31),
//                    IsActive=true,
//                    IsChair = false,
//                    MembershipDate = DateTime.Today,
//                    PhotoUrl = null
//                },
//                new Member
//                {
//                    Id = 2,
//                    FirstName ="Brenda",
//                    LastName ="Love",
//                    DateOfBirth = new DateTime(1980,11,20),
//                    IsActive=true,
//                    IsChair = true,
//                    MembershipDate = DateTime.Today,
//                    PhotoUrl = null
//                },
//                new Member
//                {
//                    Id = 3,
//                    FirstName ="Smith",
//                    LastName ="Joe",
//                    DateOfBirth = new DateTime(1970,07,30),
//                    IsActive=true,
//                    IsChair = false,
//                    MembershipDate = DateTime.Today,
//                    PhotoUrl = null
//                },
//            };

//            systemUnderTest = new MemberRepository();
//        }
         
//        [Fact]
//        public async Task GetMembersAsync_WhenIsInvoked_ThenNoExceptionIsThrown()
//        {         
//            Func<Task> func = async () => await systemUnderTest.GetMembersAsync(null);
//            await func.Should().NotThrowAsync();
//        }

//        [Fact]
//        public async Task GetMembersAsync_WhenIsInvokedWithNoFilter_ThenTheExpectedCountIsReturned()
//        {        
//            var result = await systemUnderTest.GetMembersAsync(null);
//            result.Should().NotBeNull();
//            result.Should().HaveCount(3);
//        }

//        [Fact]
//        public async Task GetMembersAsync_WhenIsInvokedWithNoFilter_ThenTheExpectedResultIsReturned()
//        {           
//            var result = await systemUnderTest.GetMembersAsync(null);
//            result.Should().NotBeNull();
//            result.Should().BeEquivalentTo(members);
//        }

//        [Fact]
//        public async Task GetMembersAsync_WhenIsInvokedWithFilterSupplied_ThenTheExpectedResultIsReturned()
//        {           
//            var searchMembers = new List<Member>
//            {
//                expectedResult
//            };

//            var result = await systemUnderTest.GetMembersAsync("Doe");           
//            result.Should().BeEquivalentTo(searchMembers);
//        }       
       
//        [Fact]
//        public async Task GetMemberByIdAsync_WhenIsInvoked_ThenNoExceptionIsThrown()
//        {
//            Func<Task> func = async () => await systemUnderTest.GetMemberByIdAsync(1);
//            await func.Should().NotThrowAsync();
//        }

//        [Fact]
//        public async Task GetMemberByIdAsync_WhenIsInvokedWithAValidId_ThenTheExpectedResultIsReturned()
//        {
//            var result = await systemUnderTest.GetMemberByIdAsync(1);
//            result.Should().NotBeNull();
//            result.Should().BeEquivalentTo(expectedResult);
//        }       
//    }
//}