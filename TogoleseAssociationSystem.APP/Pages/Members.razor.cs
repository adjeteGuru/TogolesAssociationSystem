using Microsoft.AspNetCore.Components;
using TogoleseAssociationSystem.Core.Models;
using TogoleseAssociationSystem.Core.ServiceProvider;

namespace TogoleseAssociationSystem.APP.Pages
{
    public class MembersComponent : ComponentBase
    {
        [Inject]
        public IMemberService MemberService { get; set; }

        [Inject]
        public NavigationManager Navigation { get; set; }

        protected List<Member>? Members;

        public string? ErrorMessage { get; set; }

        protected override async Task OnInitializedAsync()
        {
            try
            {
                Members = new List<Member>();
                var members = await MemberService.GetAllExistingMembersAsync();
                Members.AddRange(members);
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }

        protected void NavigateToDetails(Guid id)
        {
            Navigation.NavigateTo($"/memberdetail/{id}/edit");
        }

        protected void NavigateToCreate()
        {
            Navigation.NavigateTo("/membercreate");
        }
    }
}
