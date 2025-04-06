using Microsoft.AspNetCore.Components;
using TogoleseSolidarity.Core.DTOs;

namespace TogoleseSolidarity.APP.Pages;

public class MembershipItemComponent : ComponentBase
{
    [Parameter]
    public MembershipContributionReadDto Membership { get; set; }
}
