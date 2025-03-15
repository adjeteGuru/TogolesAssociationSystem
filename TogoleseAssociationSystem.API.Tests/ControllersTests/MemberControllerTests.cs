using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TogolesAssociationSystem.API.Controllers;
using TogoleseAssociationSystem.API.Controllers;
using TogoleseAssociationSystem.Application.DTOs;
using TogoleseAssociationSystem.Domain.Interfaces;
using TogoleseAssociationSystem.Domain.Models;
using Xunit;

namespace TogoleseAssociationSystem.Tests.Controllers
{
    public class MemberControllerTests
    {
        private readonly Mock<IMemberService> mockMemberService;
        private readonly Mock<IMapper> mockMapper;
        private readonly Mock<ILogger<MemberController>> logger;
        private readonly MemberController memberController;

        public MemberControllerTests()
        {
            mockMemberService = new Mock<IMemberService>();
            mockMapper = new Mock<IMapper>();
            logger = new Mock<ILogger<MemberController>>();
            memberController = new MemberController(mockMemberService.Object, mockMapper.Object, logger.Object);
        }

        [Fact]
        public async Task GetAllMembersAsync_ReturnsOkResult_WhenMembersExist()
        {
            // Arrange
            var members = new List<Member> { new Member() };
            mockMemberService.Setup(service => service.GetMembersAsync(It.IsAny<string>())).ReturnsAsync(members);
            mockMemberService.Setup(service => service.GetContributionsAsync()).ReturnsAsync(new List<MembershipContribution>());
            mockMemberService.Setup(service => service.GetClaimsAsync()).ReturnsAsync(new List<Claim>());

            // Act
            var result = await memberController.GetAllMembersAsync();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.IsType<List<MemberRead>>(okResult.Value);
        }

        [Fact]
        public async Task GetAllMembersAsync_ReturnsNotFound_WhenNoMembersExist()
        {
            // Arrange
            mockMemberService.Setup(service => service.GetMembersAsync(It.IsAny<string>())).ReturnsAsync(new List<Member>());

            // Act
            var result = await memberController.GetAllMembersAsync();

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task GetMemberById_ReturnsOkResult_WhenMemberExists()
        {
            // Arrange
            var member = new Member();
            mockMemberService.Setup(service => service.GetMemberByIdAsync(It.IsAny<Guid>())).ReturnsAsync(member);
            mockMapper.Setup(mapper => mapper.Map<MemberRead>(It.IsAny<Member>())).Returns(new MemberRead());

            // Act
            var result = await memberController.GetMemberById(Guid.NewGuid());

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.IsType<MemberRead>(okResult.Value);
        }

        [Fact]
        public async Task GetMemberById_ReturnsNotFound_WhenMemberDoesNotExist()
        {
            // Arrange
            mockMemberService.Setup(service => service.GetMemberByIdAsync(It.IsAny<Guid>())).ReturnsAsync((Member)null);

            // Act
            var result = await memberController.GetMemberById(Guid.NewGuid());

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task CreateNewMemberAsync_ReturnsCreatedAtActionResult()
        {
            // Arrange
            var memberToAdd = new MemberToAdd();
            var member = new Member();
            mockMapper.Setup(mapper => mapper.Map<Member>(It.IsAny<MemberToAdd>())).Returns(member);
            mockMemberService.Setup(service => service.CreateMember(It.IsAny<Member>()));

            // Act
            var result = await memberController.CreateNewMemberAsync(memberToAdd);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
            Assert.Equal(nameof(MemberController.GetMemberById), createdAtActionResult.ActionName);
        }

        [Fact]
        public async Task UpdateMemberAsync_ReturnsOkResult_WhenMemberIsUpdated()
        {
            // Arrange
            var memberUpdate = new MemberUpdateDto { Id = Guid.NewGuid() };
            var member = new Member();
            mockMapper.Setup(mapper => mapper.Map<Member>(It.IsAny<MemberUpdateDto>())).Returns(member);
            mockMemberService.Setup(service => service.UpdateMember(It.IsAny<Member>()));

            // Act
            var result = await memberController.UpdateMemberAsync(memberUpdate.Id, memberUpdate);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task UpdateMemberAsync_ReturnsBadRequest_WhenIdDoesNotMatch()
        {
            // Arrange
            var memberUpdate = new MemberUpdateDto { Id = Guid.NewGuid() };

            // Act
            var result = await memberController.UpdateMemberAsync(Guid.NewGuid(), memberUpdate);

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }
    }
}
