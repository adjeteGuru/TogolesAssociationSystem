using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;
using TogoleseSolidarity.Core.DTOs;
using TogoleseSolidarity.Core.ServiceProvider.Interfaces;

namespace TogoleseSolidarity.APP.Pages;

public class CreateMembershipComponent : ComponentBase
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
    public Guid Id { get; set; }

    [Parameter]
    public MembershipContributionToAdd Contribution { get; set; }

    [Parameter]
    public bool Edit { get; set; }

    [Parameter]
    public bool IsAlertVisible { get; set; }

    [Parameter]
    public bool IsVisible { get; set; }

    public EditContext EditContext { get; set; }

    [Parameter]
    public MemberRead Member { get; set; }

    [Parameter]
    public string AlertMessage { get; set; } = string.Empty;

    protected override void OnInitialized()
    {
        Contribution = new MembershipContributionToAdd();
        EditContext = new EditContext(Contribution);
    }

    protected override async Task OnParametersSetAsync()
    {
        SetEditMode();

        if (Edit)
        {
            Member = await MemberService.GetMemberByIdAsync(Id);
            SetCurrentMemberToContributeDetails();
            AlertService.OnAlert += HandleAlert;
        }
        else
        {
            Contribution = new MembershipContributionToAdd();
            AlertService.OnAlert += HandleAlert;
        }

        await base.OnParametersSetAsync();
    }

    private void HandleAlert(string message)
    {
        IsAlertVisible = true;
        AlertMessage = message;
    }

    protected async Task Submit()
    {
        var contribution = await MemberService.CreateMembershipAsync(Contribution);

        if (contribution == null)
        {
            return;
        }

        if (Member.Memberships == null)
        {
            Member.Memberships = [];
        }

        Member.Memberships.Add(contribution);

        AlertMessage = $"{contribution.ContributionName} is added successfully.";

        AlertService.ShowAlert(AlertMessage);

        Reset();
    }

    private void Reset()
    {
        Contribution = new MembershipContributionToAdd();
        StateHasChanged();
    }

    protected async Task GoBack()
    {
        await JSRuntime.InvokeVoidAsync("history.back");
    }

    private bool SetEditMode()
    {
        Edit = Id != Guid.Empty;
        return Edit;
    }

    private void SetCurrentMemberToContributeDetails()
    {
        Contribution.MemberFirstName = Member.FirstName;
        Contribution.MemberLastName = Member.LastName;
    }

    public void UnsubscribeAlert()
    {
        AlertService.OnAlert -= HandleAlert;
    }
}
