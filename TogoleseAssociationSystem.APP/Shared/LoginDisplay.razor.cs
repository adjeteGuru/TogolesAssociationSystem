using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;

namespace TogoleseSolidarity.APP.Shared;

public class LoginDisplayComponent : ComponentBase
{
    [Inject]
    private NavigationManager NavigationManager { get; set; } = default!;

    protected override void OnInitialized()
    {
        base.OnInitialized();
    }

    public void BeginLogOut()
    {
        NavigationManager.NavigateToLogout("authentication/logout");
    }

    public void BeginLogIn()
    {
        InteractiveRequestOptions requestOptions =
            new()
            {
                Interaction = InteractionType.SignIn,
                ReturnUrl = NavigationManager.Uri,
            };

        requestOptions.TryAddAdditionalParameter("prompt", "login");
        requestOptions.TryAddAdditionalParameter("loginHint", "peter@contoso.com");

        NavigationManager.NavigateToLogin("authentication/login", requestOptions);
    }
}
