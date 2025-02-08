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

        [HttpGet("{id}")]
        public async Task<IActionResult> GetClaimByIdAsync(Guid id)
        {
            try
            {
                var claim = await memberService.GetClaimByIdAsync(id);
                if (claim == null)
                {
                    return NotFound();
                }

                return Ok(claim);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // POST api/claim
        //[HttpPost]
        //public async Task<IActionResult> CreateClaimAsync([FromBody] ClaimToAdd claimToAdd)
        //{
        //    try
        //    {
        //        var membertoFetch = claimToAdd.ConvertToDto();

        //        var member = await memberService.RetrieveMember(membertoFetch.FirstName, membertoFetch.LastName);
        //        if (member == null)
        //        {
        //            return NotFound();
        //        }
        //        var claim = claimToAdd.ConvertToDto(member);

        //        await memberService.CreateClaimAsync(claim);

        //        return CreatedAtAction(nameof(GetClaimByIdAsync), new { id = claim.Id }, claim);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        //    }
        //}
    }
}
