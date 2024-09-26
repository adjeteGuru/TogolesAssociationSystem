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
        public IJSRuntime JSRuntime { get; set; }

        [Parameter]
        public Guid Id { get; set; }
        public MemberRead Member { get; set; }

        public string? ErrorMessage { get; set; }
        public EditContext EditContext { get; set; }

        protected decimal TotalCount = 0;

        protected decimal TotalCurrentYearAmount = 0;

        protected override async Task OnInitializedAsync()
        {
            Member = new MemberRead();
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
                Relationship = member.Relationship,
                Memberships = null               
            };
            await MemberService.UpdateMemberDetails(memberUpdateDto);
            StateHasChanged();
            Navigation.NavigateTo("/memberlist");
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
    }
}
