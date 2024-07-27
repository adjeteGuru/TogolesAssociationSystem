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
            this.memberService = memberService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllMembersAsync(string? filter = null)
        {
            var members = await memberService.GetMembersAsync(filter);
            return Ok(members);
        }
    }
}