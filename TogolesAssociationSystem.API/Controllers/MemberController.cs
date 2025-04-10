using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TogoleseSolidarity.API.Extensions;
using TogoleseSolidarity.Application.AutoMapper;
using TogoleseSolidarity.Application.DTOs;
using TogoleseSolidarity.Domain.Interfaces;
using TogoleseSolidarity.Domain.Models;

namespace TogoleseSolidarity.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[ProducesResponseType(StatusCodes.Status401Unauthorized)]
[ProducesResponseType(StatusCodes.Status200OK)]
[ProducesResponseType(StatusCodes.Status404NotFound)]
[ProducesResponseType(StatusCodes.Status400BadRequest)]
public class MemberController(IMemberService memberService, IMapper mapper, ILogger<MemberController> logger) : ControllerBase
{

    //[Authorize]
    [HttpGet]
    public async Task<IActionResult> GetAllMembersAsync(string? filter = null)
    {
        List<MemberRead> membersDto = [];
        try
        {
            IEnumerable<MembershipContributionReadDto> contributionsDto;
            IEnumerable<ClaimReadDto> claimsReadDto;

            var members = await memberService.GetMembersAsync(filter);

            if (!members.Any())
            {
                logger.LogInformation("No members found");
                return Ok(membersDto);
            }

            var contributions = await memberService.GetContributionsAsync();
            var claims = await memberService.GetClaimsAsync();

            if (contributions.ToList().Count == 0 || claims.ToList().Count == 0)
            {
                membersDto = members.ConvertToDto();
            }
            else
            {
                contributionsDto = contributions.ToList().ConvertToDto();
                claimsReadDto = claims.ToList().ConvertToDto();
                membersDto = members.ConvertToDto(contributionsDto, claimsReadDto);
            }

            return Ok(membersDto);
        }
        catch (Exception ex)
        {
            logger.LogWarning($"Something wrong happened. Error: {ex.Message}");
            return Ok(membersDto);
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetMemberById(Guid id)
    {
        try
        {
            var member = await memberService.GetMemberByIdAsync(id);

            if (member == null)
            {
                logger.LogInformation("No member found");
                return NotFound();
            }
            var memberDto = mapper.Map<MemberRead>(member);

            return Ok(memberDto);
        }
        catch (Exception ex)
        {
            logger.LogWarning($"Something wrong happened. Error: {ex.Message}");
            return Ok();
        }
    }

    // POST api/Member
    [HttpPost]
    public async Task<IActionResult> CreateNewMemberAsync([FromBody] MemberToAdd memberToAdd)
    {
        try
        {
            var maper = MapperSettings.GetMapperConfiguration().CreateMapper();
            var member = maper.Map<Member>(memberToAdd);

            await memberService.CreateMember(member);

            return CreatedAtAction(nameof(GetMemberById), new { id = member.Id }, member);
        }
        catch (Exception ex)
        {
            //return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            logger.LogWarning($"Something wrong happened. Error: {ex.Message}");
            return Ok();
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateMemberAsync(Guid id, MemberUpdateDto memberUpdate)
    {
        try
        {
            if (id != memberUpdate.Id)
            {
                BadRequest();
            }

            var member = mapper.Map<Member>(memberUpdate);

            memberService.UpdateMember(member);

            return Ok(memberUpdate);
        }
        catch (Exception ex)
        {
            //return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            logger.LogWarning($"Something wrong happened. Error: {ex.Message}");
            return Ok();
        }
    }
}