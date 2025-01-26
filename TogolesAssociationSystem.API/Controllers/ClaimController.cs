using Microsoft.AspNetCore.Mvc;
using TogoleseAssociationSystem.Domain.Interfaces;
using TogoleseAssociationSystem.Domain.Models;

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

        public ClaimController(IMemberService memberService)
        {
            this.memberService = memberService;
        }      

        [HttpPost]
        public async Task<IActionResult> MakeAClaimAsync(Claim claim)
        {            
            try
            {
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
