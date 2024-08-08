using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TogoleseAssociationSystem.API.Extensions;
using TogoleseAssociationSystem.Application.Services;
using TogoleseAssociationSystem.Domain.DTOs;

namespace TogoleseAssociationSystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class MembershipController : ControllerBase
    {
        private readonly IMemberService memberService;

        public MembershipController(IMemberService memberService, IMapper mapper)
        {
            this.memberService = memberService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMembershipById(Guid id)
        {
            try
            {
                var membership = await memberService.GetMembershipByIdAsync(id);

                if (membership == null)
                {
                    return NotFound();
                }

                return Ok(membership);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
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
                    return NotFound();
                }
                var membership = membershipToAdd.ConvertToDto(member);

                memberService.CreateMembership(membership);

                return CreatedAtAction(nameof(GetMembershipById), new { id = membership.Id }, membership);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
