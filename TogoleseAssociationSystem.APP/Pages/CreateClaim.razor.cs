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
        }
        else
        {
            Claim = new ClaimToAdd();               
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
        await MemberService.CreateClaimAsync(Claim);
        //if (Edit)
        //{
        //    await MemberService.UpdateClaimAsync(ClaimToUpdate);
        //}
        //else
        //{
        //    await MemberService.CreateClaimAsync(Claim);
        //}
        NavigationManager.NavigateTo($"/memberdetail/{Member.Id}/edit");
        //await JSRuntime.InvokeVoidAsync("history.back");
    }

    protected void NavigateToHome()
    {
        NavigationManager.NavigateTo("/claims");
    }
}
