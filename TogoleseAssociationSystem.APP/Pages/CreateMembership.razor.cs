using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;
using TogoleseAssociationSystem.Core.DTOs;
using TogoleseAssociationSystem.Core.ServiceProvider.Interfaces;

namespace TogoleseAssociationSystem.APP.Pages
{
    public class CreateMembershipComponent : ComponentBase
    {

        [Inject]
        public NavigationManager Navigation { get; set; }

        [Inject]
        public IMemberService MemberService { get; set; }

        [Inject]
        public IJSRuntime JSRuntime { get; set; }

        [Parameter]
        public Guid Id { get; set; }

        [Parameter]
        public MembershipContributionToAdd Contribution { get; set; }

        [Parameter]
        public bool Edit { get; set; }

        public EditContext EditContext { get; set; }

        [Parameter]
        public MemberRead Member { get; set; }

        protected override void OnInitialized()
        {
            Contribution = new MembershipContributionToAdd();
            EditContext = new EditContext(Contribution);
        }

        protected override async Task OnParametersSetAsync()
        {
            SetEditMode();

            Member = new MemberRead();

            if (Edit == true)
            {
                Member = await MemberService.GetMemberByIdAsync(Id);
                SetCurrentMemberToContributeDetails();
            }

            await base.OnParametersSetAsync();
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

        protected async Task GoBack()
        {
            await JSRuntime.InvokeVoidAsync("history.back");
        }

        private bool SetEditMode()
        {
            if (Id == Guid.Empty)
            {
                return Edit;
            }
            Edit = true;
            return Edit;
        }

        private void SetCurrentMemberToContributeDetails()
        {
            Contribution.MemberFirstName = Member.FirstName;
            Contribution.MemberLastName = Member.LastName;
        }
    }
}
