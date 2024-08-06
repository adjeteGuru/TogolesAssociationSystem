using Microsoft.AspNetCore.Components;
using TogoleseAssociationSystem.Core.Models;

namespace TogoleseAssociationSystem.APP.Pages
{
    public class MembreItemComponent : ComponentBase
    {
        [Parameter]
        public Member Member { get; set; }
    }
}
