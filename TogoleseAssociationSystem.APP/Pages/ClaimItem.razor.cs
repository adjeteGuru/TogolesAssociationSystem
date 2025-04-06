using Microsoft.AspNetCore.Components;
using TogoleseSolidarity.Core.DTOs;

namespace TogoleseSolidarity.APP.Pages
{
    public class ClaimItemComponent : ComponentBase
    {
        [Parameter]
        public ClaimReadDto Claim { get; set; }
    }
}
