using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using TogoleseAssociationSystem.Core.DTOs;
using TogoleseAssociationSystem.Core.Models;
using TogoleseAssociationSystem.Core.ServiceProvider;

namespace TogoleseAssociationSystem.APP.Pages
{
    public class CreateMembershipComponent : ComponentBase
    {

        [Inject]
        public NavigationManager Navigation { get; set; }

        [Inject]
        public IMemberService MemberService { get; set; }    

        [Parameter]
        public int Id { get; set; }

        [Parameter]
        public MembershipContributionToAdd Contribution { get; set; }

        [Parameter]
        public bool Edit { get; set; }

        public EditContext EditContext { get; set; }

        public Member Member { get; set; }       

        protected override void OnInitialized()
        {
            Contribution = new MembershipContributionToAdd();
            EditContext = new EditContext(Contribution);
        }
      
        protected override async Task OnParametersSetAsync()
        {          
            Member ??= new Member();

            Member = await MemberService.GetMemberByIdAsync(Id);
            
            if (Edit = IsEditingMode())
            {
                SetCurrentMemberToContributeDetails();
            }                   

            await base.OnParametersSetAsync();
        }       

        public void SetCurrentMemberToContributeDetails()
        {           
            Contribution.MemberFirstName = Member.FirstName;
            Contribution.MemberLastName = Member.LastName;            
        }

        protected async Task Submit()
        {
            var result = await MemberService.CreateMembershipAsync(Contribution);
            if (result == null)
            {
                return;
            }
            Navigation.NavigateTo("/memberlist");
        }

        public void NavigateToHome()
        {
            Navigation.NavigateTo("/memberlist");
        }

        private bool IsEditingMode()
        {
            return Id > 0 && Member.Id == Id;
        }
    }
}
