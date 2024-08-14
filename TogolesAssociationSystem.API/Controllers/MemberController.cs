using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TogoleseAssociationSystem.Application.AutoMapper;
using TogoleseAssociationSystem.Application.Services;
using TogoleseAssociationSystem.Domain.DTOs;
using TogoleseAssociationSystem.Domain.Models;

namespace TogolesAssociationSystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
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
                var contributions = await memberService.GetContributionsAsync();

                var members = await memberService.GetMembersAsync(filter);              

                if (!members.Any())
                {
                    return NotFound();
                }

                return Ok(members);
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

                return Ok(member);
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
        public async Task<IActionResult> UpdateMemberAsync(Guid id, Member memberUpdate)
        {
            try
            {
                if (id != memberUpdate.Id)
                {
                    BadRequest();
                }               

                memberService.UpdateMember(memberUpdate);

                return Ok(memberUpdate);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}