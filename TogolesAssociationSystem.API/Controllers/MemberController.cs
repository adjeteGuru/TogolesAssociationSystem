using Microsoft.AspNetCore.Mvc;

namespace TogolesAssociationSystem.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MemberController : ControllerBase
    {     
        public MemberController()
        {
           
        }

        [HttpGet]
        public IEnumerable<string>? Get()
        {
            return default;
        }
    }
}