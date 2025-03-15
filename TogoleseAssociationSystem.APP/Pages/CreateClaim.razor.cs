using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;
using TogoleseAssociationSystem.Core.DTOs;
using TogoleseAssociationSystem.Core.ServiceProvider.Interfaces;

namespace TogoleseAssociationSystem.APP.Pages;

public class CreateClaimComponent : ComponentBase
{
    [Inject]
    public IMemberService MemberService { get; set; }

    [Inject]
    public NavigationManager NavigationManager { get; set; }

    [Inject]
    public IJSRuntime JSRuntime { get; set; }

    [Inject]
    public IAlertService AlertService { get; set; }

    [Parameter]
    public Guid Id { get; set; }

    [Parameter]
    public ClaimToAdd Claim { get; set; }

    public ClaimToUpdate ClaimToUpdate { get; set; }

    public ClaimReadDto ClaimReadDto { get; set; }

    [Parameter]
    public MemberRead Member { get; set; }

    [Parameter]
    public EditContext EditContext { get; set; }

    [Parameter]
    public bool Edit { get; set; }

    [Parameter]
    public string AlertMessage { get; set; } = string.Empty;

    [Parameter]
    public bool IsAlertVisible { get; set; }

    protected override void OnInitialized()
    {
        Claim = new ClaimToAdd();
        EditContext = new EditContext(Claim);
    }

    protected override async Task OnParametersSetAsync()
    {
        SetEditMode();

        if (Edit)
        {
            ClaimReadDto = await MemberService.GetClaimByIdAsync(Id);
            Member = await MemberService.GetMemberByIdAsync(Id);
            SetCurrentMemberToClaimDetails();
            AlertService.OnAlert += HandleAlert;
        }
        else
        {
            Claim = new ClaimToAdd();
            Member = new MemberRead(); // Ensure Member is not null
        }

        await base.OnParametersSetAsync();
    }

    private void SetCurrentMemberToClaimDetails()
    {
        Claim.MemberFirstName = Member.FirstName;
        Claim.MemberLastName = Member.LastName;
        Claim.NextOfKinName = Member.NextOfKin;
        Claim.NextOfKinContact = Member.NextOfKinContact;
    }

    private bool SetEditMode()
    {
        Edit = Id != Guid.Empty;
        return Edit;
    }

    protected async Task Submit()
    {
        var claimReadDto = await MemberService.CreateClaimAsync(Claim);
        if (Member.Claims == null)
        {
            Member.Claims = [];
        }
        Member.Claims.Add(claimReadDto);
        AlertMessage = $"{claimReadDto.Name} is added successfully.";
        AlertService.ShowAlert(AlertMessage);

        Reset();
    }

    private void Reset()
    {
        Claim = new ClaimToAdd();
        StateHasChanged();
    }

    private void HandleAlert(string message)
    {
        IsAlertVisible = true;
        AlertMessage = message;
    }

    protected void NavigateToHome()
    {
        NavigationManager.NavigateTo("/claims");
    }
}
