using Microsoft.AspNetCore.Components;
using TogoleseAssociationSystem.Core.DTOs;
using TogoleseAssociationSystem.Core.ServiceProvider;

namespace TogoleseAssociationSystem.APP.Pages
{
    public class MemberListComponent : ComponentBase
    {
        [Inject]
        public IMemberService MemberService { get; set; }
       
        public List<MemberRead> Members { get; set; }
       
        public string ErrorMessage { get; set; }

        protected override async Task OnInitializedAsync()
        {          
            try
            {
                Members = new List<MemberRead>();
                var members = await MemberService.GetMembersAsync(null);
                Members.AddRange(members); 
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }

        protected void NavigateToCreate()
        {

        }
    }
}
