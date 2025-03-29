using Microsoft.AspNetCore.Components;
using TogoleseSolidarity.Core.DTOs;

namespace TogoleseSolidarity.APP.Pages
{
    public class MemberItemComponent : ComponentBase
    {
        [Parameter]
        public MemberRead Member { get; set; }  
    }
}
