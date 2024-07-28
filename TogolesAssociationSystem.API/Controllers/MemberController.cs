using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TogoleseAssociationSystem.Application.Services;
using TogoleseAssociationSystem.Domain.DTOs;

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
                var members = await memberService.GetMembersAsync(filter);

                if (!members.Any())
                {
                    return NotFound();
                }
                var membersToRead = mapper.Map<List<MemberRead>>(members);

                return Ok(membersToRead);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMemberByIdAsync(int id)
        {
            try
            {
                var member = await memberService.GetMemberByIdAsync(id);

                if (member == null)
                {
                    return NotFound();
                }
                var memberToRead = mapper.Map<MemberRead>(member);

                return Ok(memberToRead);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}