using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TogoleseAssociationSystem.API.Extensions;
using TogoleseAssociationSystem.Application.AutoMapper;
using TogoleseAssociationSystem.Application.DTOs;
using TogoleseAssociationSystem.Domain.Interfaces;
using TogoleseAssociationSystem.Domain.Models;

namespace TogolesAssociationSystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public class MemberController : ControllerBase
    {
        private readonly IMemberService memberService;
        private readonly IMapper mapper;

        public MemberController(IMemberService memberService, IMapper mapper)
        {
            this.memberService = memberService ?? throw new ArgumentNullException(nameof(memberService));
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllMembersAsync(string? filter = null)
        {
            try
            {
                IEnumerable<MembershipContributionReadDto> contributionsDto;
                List<MemberRead> membersDto;
                var members = await memberService.GetMembersAsync(filter);

                if (!members.Any())
                {
                    return NotFound();
                }

                var contributions = await memberService.GetContributionsAsync();

                if (contributions.ToList().Count == 0)
                {
                    membersDto = members.ConvertToDto();
                }
                else
                {
                    contributionsDto = contributions.ToList().ConvertToDto();
                    membersDto = members.ConvertToDto(contributionsDto);
                }

                return Ok(membersDto);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
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
                    return NotFound();
                }
                var memberDto = mapper.Map<MemberRead>(member);

                return Ok(memberDto);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
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

                memberService.CreateMember(member);

                return CreatedAtAction(nameof(GetMemberById), new { id = member.Id }, member);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
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
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}