using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using TogoleseAssociationSystem.Core.DTOs;

namespace TogoleseAssociationSystem.APP.Pages
{
    public class CreateMemberComponent : ComponentBase
    {
        [Inject]
        public NavigationManager Navigation { get; set; }
        public EditContext EditContext { get; set; }

        public MemberToAdd  Member { get; set; }
        protected override void OnInitialized()
        {
            Member = new MemberToAdd();
            EditContext = new EditContext(Member);
        }

        protected void NavigateToHome()
        {
            Navigation.NavigateTo("/memberlist");
        }
    }
}
