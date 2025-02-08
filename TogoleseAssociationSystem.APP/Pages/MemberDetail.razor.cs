using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;
using TogoleseAssociationSystem.Core.DTOs;
using TogoleseAssociationSystem.Core.ServiceProvider.Interfaces;

namespace TogoleseAssociationSystem.APP.Pages
{
    public class MemberDetailComponent : ComponentBase
    {
        [Inject]
        public IMemberService MemberService { get; set; }

        [Inject]
        public NavigationManager Navigation { get; set; }

        [Inject]
        public IAlertService AlertService { get; set; }

        [Inject]
        public IJSRuntime JSRuntime { get; set; }

        [Parameter]
        public string AlertMessage { get; set; } = string.Empty;

        [Parameter]
        public Guid Id { get; set; }

        [Parameter]
        public bool IsAlertVisible { get; set; }

        [Parameter]
        public bool IsVisible { get; set; }

        public MemberRead Member { get; set; } = new MemberRead();
        public EditContext EditContext { get; set; }

        protected decimal TotalCount = 0;

        protected decimal TotalCurrentYearAmount = 0;

        //protected decimal TotalNumberOfClaimRemain = 0;

        protected override async Task OnInitializedAsync()
        {
            EditContext = new EditContext(Member);

            try
            {
                Member = await MemberService.GetMemberByIdAsync(Id);
                CalculateTotalContributionByMember();
                //TotalAnnualContribution();
                //TotalClaimRemain();
                AlertService.OnAlert += HandleAlert;
            }
            catch (Exception ex)
            {
                AlertService.ShowAlert(ex.Message);
            }
        }

        protected async Task GoBack()
        {
            await JSRuntime.InvokeVoidAsync("history.back");
        }

        protected void NavigateToCreate()
        {
            Navigation.NavigateTo("/membercreate");
        }

        protected void NavigateToAddContribution(Guid selectedMemberId)
        {
            Navigation.NavigateTo($"/membershipcreate/{selectedMemberId}/edit");
        }

        protected void NavigateToAddClaim(Guid selectedMemberId)
        {
            Navigation.NavigateTo($"/claimcreate/{selectedMemberId}/edit");
        }

        protected async Task UpdateMemberDetails(MemberRead member)
        {
            var memberUpdateDto = new MemberUpdateDto
            {
                Id = member.Id,
                FirstName = member.FirstName,
                LastName = member.LastName,
                Title = member.Title,
                Telephone = member.Telephone,
                Address = member.Address,
                Postcode = member.Postcode,
                City = member.City,
                DateOfBirth = member.DateOfBirth,
                PhotoUrl = member.PhotoUrl,
                IsActive = member.IsActive,
                IsChair = member.IsChair,
                MembershipDate = member.MembershipDate,
                NextOfKin = member.NextOfKin,
                NextOfKinContact = member.NextOfKinContact,
                Relationship = member.Relationship,
                Memberships = null
            };

            await MemberService.UpdateMemberDetails(memberUpdateDto);

            AlertMessage = $"{member.FirstName} {member.LastName} is updated successfully";

            AlertService.ShowAlert(AlertMessage);

            AlertService.OnAlert += HandleAlert;

            StateHasChanged();
        }

        public void SetAlert(string message)
        {
            AlertService.ShowAlert(message);
        }

        private void HandleAlert(string message)
        {
            IsAlertVisible = true;
            AlertMessage = message;
        }

        protected async Task OnInputFileChanged(InputFileChangeEventArgs args)
        {
            var memoryStream = new MemoryStream();
            await args.File.OpenReadStream().CopyToAsync(memoryStream);
            var bytes = memoryStream.ToArray();
            Member.PhotoUrl = bytes;
        }

        private void CalculateTotalContributionByMember()
        {
            TotalCount = Member.Memberships.Sum(m => m.Amount);
        }

        private void TotalAnnualContribution()
        {
            TotalCurrentYearAmount = Member.Memberships
                .Where(x => x.DateOfContribution?.Year == DateTime.Today.Year)
                .Sum(m => m.Amount);
        }

        //private void TotalClaimRemain()
        //{
        //    TotalNumberOfClaimRemain = Member.Claims
        //        .Where(c => c.TotalClaimPerMember > 0)
        //        .Sum(c => c.ClaimRemain);
        //}

        private void UnsubscribeAlert()
        {
            AlertService.OnAlert -= HandleAlert;
        }
    }
}
