using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using TogoleseAssociationSystem.Core.DTOs;

namespace TogoleseAssociationSystem.APP.Pages
{
    public class MemberDetailComponent : ComponentBase
    {
        [Parameter]
        public MemberRead Member { get; set; }
        public EditContext  EditContext { get; set; }
        protected override async Task OnParametersSetAsync()
        {
            Member = new MemberRead();
            EditContext = new EditContext(Member);
            await base.OnParametersSetAsync();
        }
    }
}
