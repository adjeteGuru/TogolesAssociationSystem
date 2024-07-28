using Microsoft.AspNetCore.Components;
using TogoleseAssociationSystem.Core.DTOs;
using TogoleseAssociationSystem.Core.ServiceProvider;

namespace TogoleseAssociationSystem.APP.Pages
{
    public class MemberListComponent : ComponentBase
    {
        [Inject]
        public IMemberService MemberService { get; set; }

        public IEnumerable<MemberRead> Members;

        [Parameter]
        public string ErrorMessage { get; set; }

        protected override async Task OnInitializedAsync()
        {
           Members = new List<MemberRead>();
            try
            {
                Members = await MemberService.GetMembersAsync(null);
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }
    }
}
