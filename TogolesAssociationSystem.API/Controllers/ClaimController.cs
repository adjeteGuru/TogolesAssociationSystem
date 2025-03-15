using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TogoleseAssociationSystem.API.Extensions;
using TogoleseAssociationSystem.Application.DTOs;
using TogoleseAssociationSystem.Domain.Interfaces;
using TogoleseAssociationSystem.Domain.Models;

namespace TogoleseAssociationSystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public class ClaimController(IMemberService memberService, IMapper mapper, ILogger<ClaimController> logger) : ControllerBase
    {
        private readonly IMemberService memberService = memberService;
        private readonly IMapper mapper = mapper;

        [HttpPost]
        public async Task<IActionResult> MakeAClaimAsync(ClaimToAdd claimToAdd)
        {
            try
            {
                var membertoFetch = claimToAdd.ConvertToDto();

                var member = await memberService.RetrieveMember(membertoFetch.FirstName, membertoFetch.LastName);
                if (member == null)
                {
                    logger.LogInformation("No claim found");
                    return NotFound();
                }

                var claim = claimToAdd.ConvertToDto(member);

                await memberService.CreateClaimAsync(claim);

                return Ok(claim);
            }
            catch (Exception ex)
            {
                //return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
                logger.LogWarning($"Something wrong happened. Error: {ex.Message}");
                return Ok();
            }
        }

        [HttpGet("claim/{id}")]
        public async Task<IActionResult> GetClaimByIdAsync(Guid id)
        {
            try
            {
                var claim = await memberService.GetClaimByIdAsync(id);
                if (claim == null)
                {
                    logger.LogInformation("No claim found");
                    return NotFound();
                }

                return Ok(claim);
            }
            catch (Exception ex)
            {
                //return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
                logger.LogWarning($"Something wrong happened. Error: {ex.Message}");
                return Ok();
            }
        }

        [HttpGet("member/{id}")]
        public async Task<IActionResult> GetClaimsByMemberIdAsync(Guid id)
        {
            try
            {
                var claims = await memberService.GetClaimsByMemberIdAsync(id);
                if (claims == null)
                {
                    logger.LogInformation("No claim found");
                    return Ok(new List<Claim>());
                }
                return Ok(claims);
            }
            catch (Exception ex)
            {
                //return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
                logger.LogWarning($"Something wrong happened. Error: {ex.Message}");
                return Ok();

            }
        }
    }
}
