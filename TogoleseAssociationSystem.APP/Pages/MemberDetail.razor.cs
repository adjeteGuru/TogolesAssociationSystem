using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using TogoleseAssociationSystem.Core.DTOs;
using TogoleseAssociationSystem.Core.Models;
using TogoleseAssociationSystem.Core.ServiceProvider;

namespace TogoleseAssociationSystem.APP.Pages
{
    public class MemberDetailComponent : ComponentBase
    {
        [Inject]
        public IMemberService MemberService { get; set; }

        [Parameter]
        public int Id { get; set; }

        public Member Member { get; set; }

        public string? ErrorMessage { get; set; }

        public EditContext  EditContext { get; set; }              

        protected override async Task OnInitializedAsync()
        {
            Member = new Member();
            EditContext = new EditContext(Member);

            try
            {
                Member = await MemberService.GetMemberByIdAsync(Id);
            }
            catch (Exception e)
            {
                ErrorMessage = e.Message;
            }
        } 
        
    }
}
