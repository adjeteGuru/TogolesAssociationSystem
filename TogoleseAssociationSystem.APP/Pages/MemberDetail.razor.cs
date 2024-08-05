using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using TogoleseAssociationSystem.Core.Models;
using TogoleseAssociationSystem.Core.ServiceProvider;

namespace TogoleseAssociationSystem.APP.Pages
{
    public class MemberDetailComponent : ComponentBase
    {
        [Inject]
        public IMemberService MemberService { get; set; }

        [Inject]
        public NavigationManager Navigation { get; set; }

        [Parameter]
        public int Id { get; set; }
        public Member Member { get; set; }
        public string? ErrorMessage { get; set; }
        public EditContext  EditContext { get; set; }

        protected decimal TotalCount = 0;

        protected decimal TotalCurrentYearAmount = 0;

        protected override async Task OnInitializedAsync()
        {
            Member = new Member();
            EditContext = new EditContext(Member);

            try
            {
                Member = await MemberService.GetMemberByIdAsync(Id);
                CalculateTotalContributionByMember();
                TotalAnnualContribution();
            }
            catch (Exception e)
            {
                ErrorMessage = e.Message;
            }
        }
       
        private void CalculateTotalContributionByMember()
        {           
            foreach (var item in Member.Memberships)
            {
                TotalCount += item.Amount;
            };
        }
        
        private void TotalAnnualContribution()
        {
            foreach (var membership in Member.Memberships)
            {
                var dateFormatted = membership.DateOfContribution.Value.ToString("dd-MMM-yyyy").Split('-');
           
                int.TryParse(dateFormatted[2], out int yearContributed);              
             
                if (membership.IsAnnualContribution.Equals(true) && yearContributed == DateTime.Today.Year)
                {
                    TotalCurrentYearAmount += membership.Amount;
                }
            }
        }

        protected void NavigateToHome()
        {
            Navigation.NavigateTo("/memberlist");
        }

        protected void NavigateToCreate()
        {
            Navigation.NavigateTo("/membercreate/edit");
        }
    }
}
