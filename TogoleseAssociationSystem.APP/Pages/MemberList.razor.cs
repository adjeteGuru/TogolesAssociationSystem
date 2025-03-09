using Microsoft.AspNetCore.Components;
using TogoleseAssociationSystem.Core.DTOs;
using TogoleseAssociationSystem.Core.ServiceProvider.Interfaces;

namespace TogoleseAssociationSystem.APP.Pages
{
    public class MemberListComponent : ComponentBase
    {
        [Inject]
        public IMemberService MemberService { get; set; }

        [Inject]
        public NavigationManager Navigation { get; set; }

        [Inject]
        public IAlertService AlertService { get; set; }

        [Parameter]
        public bool DisplayAlert { get; set; }

        public MembershipContributionReadDto MembershipContributionRead { get; set; }

        public EventCallback<List<MemberRead>> OnMemberSearched { get; set; }

        protected List<MemberRead>? Members;

        public string? ErrorMessage { get; set; }

        [Parameter]
        public string? Message { get; set; }

        [Parameter]
        public bool IsVisible { get; set; }

        protected int CurrentPage { get; set; } = 1;

        protected int ItemsPerPage { get; set; } = 10;

        protected int TotalCount { get; set; } = 100;

        protected List<MemberRead> GetPagedMembers()
        {
            return Members.Skip((CurrentPage - 1) * ItemsPerPage).Take(ItemsPerPage).ToList();
        }

        protected override async Task OnInitializedAsync()
        {
            try
            {
                Members = new List<MemberRead>();
                var members = await MemberService.GetMembersAsync(CurrentPage, ItemsPerPage, null);
                Members.AddRange(members);
                AlertService.OnAlert += HandleAlert;
            }
            catch (Exception ex)
            {
                AlertService.ShowAlert(ex.Message);
            }
        }

        protected bool CanGoToPreviousPage() => CurrentPage > 1;

        protected bool CanGoToNextPage() => CurrentPage < TotalCount;

        protected async void GoToPreviousPage()
        {
            if (CanGoToPreviousPage())
            {
                CurrentPage--;
                await LoadMembers();
            }
        }
        protected async void GoToNextPage()
        {
            if (CanGoToNextPage())
            {
                CurrentPage++;
                await LoadMembers();
            }
        }

        private async Task LoadMembers()
        {
            try
            {
                Members = new List<MemberRead>();
                var members = await MemberService.GetMembersAsync(CurrentPage, ItemsPerPage, null);
                Members.AddRange(members);
                StateHasChanged();
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                StateHasChanged();
            }
        }


        private void HandleAlert(string message)
        {
            IsVisible = true;
            DisplayAlert = true;
            Message = message;
        }

        public async void HandleSearch(string filter)
        {
            try
            {
                var searchedMembers = await MemberService.GetMembersAsync(CurrentPage, ItemsPerPage, filter);
                Members = searchedMembers.ToList();

                StateHasChanged();
            }
            catch
            {
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

        private void Unsuscribe()
        {
            AlertService.OnAlert -= HandleAlert;
        }
    }
}
