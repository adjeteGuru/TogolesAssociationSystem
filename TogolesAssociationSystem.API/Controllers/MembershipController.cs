using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TogoleseAssociationSystem.API.Extensions;
using TogoleseAssociationSystem.Application.DTOs;
using TogoleseAssociationSystem.Domain.Interfaces;

namespace TogoleseAssociationSystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public class MembershipController(IMemberService memberService, IMapper mapper, ILogger<MembershipController> logger) : ControllerBase
    {
        private readonly IMemberService memberService = memberService;
        private readonly IMapper mapper = mapper;

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMembershipById(Guid id)
        {
            try
            {
                var membership = await memberService.GetMembershipByIdAsync(id);

                if (membership == null)
                {
                    logger.LogInformation("No membership found");
                    return NotFound();
                }

                var membershipDto = mapper.Map<MembershipContributionReadDto>(membership);

                return Ok(membershipDto);
            }
            catch (Exception ex)
            {
                logger.LogWarning($"Something wrong happened. Error: {ex.Message}");
                return Ok();
            }
        }

        // POST api/Membership
        [HttpPost]
        public async Task<IActionResult> CreateMembershipAsync([FromBody] MembershipContributionToAdd membershipToAdd)
        {
            try
            {
                var membertoFetch = membershipToAdd.ConvertToDto();

                var member = await memberService.RetrieveMember(membertoFetch.FirstName, membertoFetch.LastName);
                if (member == null)
                {
                    logger.LogInformation("No member found");
                    return NotFound();
                }
                var membership = membershipToAdd.ConvertToDto(member);

                await memberService.CreateMembership(membership);

                return CreatedAtAction(nameof(GetMembershipById), new { id = membership.Id }, membership);
            }
            catch (Exception ex)
            {
                logger.LogWarning($"Something wrong happened. Error: {ex.Message}");
                return Ok();
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllMembershipsAsync()
        {
            List<MembershipContributionReadDto> membershipsRead = [];
            try
            {
                var memberships = await memberService.GetContributionsAsync();

                if (!memberships.Any())
                {
                    logger.LogInformation("No memberships found");
                    return NotFound();
                }
                var membershipsDtos = mapper.Map<IEnumerable<MembershipContributionReadDto>>(memberships);
                membershipsRead = [.. membershipsDtos];
                return Ok(membershipsRead);
            }
            catch (Exception ex)
            {
                logger.LogWarning($"Something wrong happened. Error: {ex.Message}");
                return Ok(membershipsRead);
            }
        }
    }
}
