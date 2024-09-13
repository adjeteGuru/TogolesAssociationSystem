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

        public EventCallback<List<Member>> OnMemberSearched { get; set; }

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
                ErrorMessage = ex.Message;
            }
        }

        public async void HandleSearch(string filter)
        {
            try
            {
                var searchedMembers = await MemberService.GetMembersAsync(filter);
                Members = searchedMembers.ToList();
              
                StateHasChanged();
            }
            catch
            {      
                //alert to state the error then load home
                Navigation.NavigateTo("/memberlist");
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
