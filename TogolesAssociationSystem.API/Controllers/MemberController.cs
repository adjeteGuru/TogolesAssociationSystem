using Microsoft.AspNetCore.Mvc;
using TogoleseAssociationSystem.Application.Services;

namespace TogolesAssociationSystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class MemberController : ControllerBase
    {
        private readonly IMemberService memberService;

        public MemberController(IMemberService memberService)
        {
            this.memberService = memberService ?? throw new ArgumentNullException(nameof(memberService));
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

                return Ok(members);
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

                return Ok(member);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}