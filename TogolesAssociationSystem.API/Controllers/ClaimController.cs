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
    public class ClaimController : ControllerBase
    {
        private readonly IMemberService memberService;
        private readonly IMapper mapper;

        public ClaimController(IMemberService memberService, IMapper mapper)
        {
            this.memberService = memberService;
            this.mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> MakeAClaimAsync(ClaimToAdd claimToAdd)
        {
            try
            {
                var membertoFetch = claimToAdd.ConvertToDto();

                var member = await memberService.RetrieveMember(membertoFetch.FirstName, membertoFetch.LastName);
                if (member == null)
                {
                    return NotFound();
                }

                var claim = claimToAdd.ConvertToDto(member);

                await memberService.CreateClaimAsync(claim);

                return Ok(claim);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
