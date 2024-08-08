using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;
using System.Text;
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

        [Inject]
        public IJSRuntime JSRuntime { get; set; }

        [Parameter]
        public Guid Id { get; set; }
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

        protected async Task UpdateMemberDetails(Member member)
        {
            await MemberService.UpdateMemberDetails(member);
            StateHasChanged();
            Navigation.NavigateTo("/memberlist");
        }
               
        protected async Task OnInputFileChanged(InputFileChangeEventArgs args)
        {
            var memoryStream = new MemoryStream();
            await args.File.OpenReadStream().CopyToAsync(memoryStream);
            var bytes = memoryStream.ToArray();           
            //var imageFile = Encoding.UTF8.GetString(bytes, 0, bytes.Length);
            Member.PhotoUrl = bytes;


            //InputFileMessage = args.File.Name;
            //await OnFileChanged.InvokeAsync(args);
            //if (File.ContentType == "image/png")
            //{
            //    File = await File.RequestImageFileAsync("image/png", 400, 400);
            //}

            //ErrorMessage = "Please choose the right format.";
            //string imageData = @"data:image / jpeg; base64," + Convert.ToBase64String(File.ReadAllBytes(imgPath));
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
