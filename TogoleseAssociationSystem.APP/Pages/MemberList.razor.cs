using Microsoft.AspNetCore.Components;
using TogoleseAssociationSystem.Core.Models;
using TogoleseAssociationSystem.Core.ServiceProvider;

namespace TogoleseAssociationSystem.APP.Pages
{
    public class MemberListComponent : ComponentBase
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
                var members = await MemberService.GetMembersAsync(null);
                Members.AddRange(members);
            }
            catch (Exception ex)
            {
                if (ex.Message == "Failed to fetch" || ex.InnerException.Message == "TypeError: Failed to fetch")
                {
                    ErrorMessage = "Loading, please wait while the api load...";
                }
                ErrorMessage = ex.Message;
            }
        }
        protected void NavigateToDetails(int id)
        {
            Navigation.NavigateTo($"/memberdetail/{id}/edit");
        }
        protected void NavigateToCreate()
        {
            Navigation.NavigateTo("/membercreate");
        }
    }
}
