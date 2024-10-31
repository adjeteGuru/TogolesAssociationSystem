using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;
using TogoleseAssociationSystem.Core.DTOs;
using TogoleseAssociationSystem.Core.ServiceProvider.Interfaces;

namespace TogoleseAssociationSystem.APP.Pages
{
    public class CreateMemberComponent : ComponentBase
    {
        [Inject]
        public NavigationManager Navigation { get; set; }

        [Inject]
        public IMemberService MemberService { get; set; }

        [Inject]
        public IJSRuntime JSRuntime { get; set; }

        [Inject]
        public IAlertService AlertService { get; set; }

        [Parameter]
        public bool DisplayAlert { get; set; }

        [Parameter]
        public string AlertMessage { get; set; } = string.Empty;

        [Parameter]
        public bool IsVisible { get; set; }

        public EditContext EditContext { get; set; }

        public MemberToAdd Member { get; set; } = new MemberToAdd();

        protected override void OnInitialized()
        {
            EditContext = new EditContext(Member);
            AlertService.OnAlert += HandleAlert;
        }

        protected void NavigateToHome()
        {
            Navigation.NavigateTo("/memberlist");
        }

        protected async Task Submit()
        {
            var member = await MemberService.CreateMemberAsync(Member);
            
            if (member == null)
            {
                return;
            }

            AlertMessage = $"{member.FirstName} {member.LastName}'s account is created successfully";

            AlertService.ShowAlert(AlertMessage);

            Reset();
        }

        private void Reset()
        {
            Member = new MemberToAdd();
            StateHasChanged();
        }

        private void HandleAlert(string message)
        {
            DisplayAlert = true;
            AlertMessage = message;
        }

        protected async Task OnInputFileChanged(InputFileChangeEventArgs args)
        {
            var file = args.File;
            if (file != null)
            {
                using var memoryStream = new MemoryStream();
                await file.OpenReadStream().CopyToAsync(memoryStream);
                Member.PhotoUrl = memoryStream.ToArray();
            }
        }

        private void UnsubscribeAlert()
        {
            AlertService.OnAlert -= HandleAlert;
        }
    }
}
