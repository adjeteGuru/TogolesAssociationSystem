using Microsoft.AspNetCore.Components;
using TogoleseAssociationSystem.Core.DTOs;

namespace TogoleseAssociationSystem.APP.Pages
{
    public class MembreItemComponent : ComponentBase
    {
        [Parameter]
        public MemberRead Member { get; set; }
    }
}
