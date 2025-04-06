using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using TogoleseSolidarity.APP;
using TogoleseSolidarity.Core.ServiceProvider;
using TogoleseSolidarity.Core.ServiceProvider.Interfaces;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7203/") });

builder.Services.AddHttpClient("TogoleseSolidarity.API", client => client.BaseAddress = new Uri("https://localhost:7203/"))
    .AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();

//builder.Services.AddHttpClient("B2CSystemPractice.ServerAPI", client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress))
//    .AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();

// Supply HttpClient instances that include access tokens when making requests to the server project
builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("TogoleseSolidarity.API"));

builder.Services.AddMsalAuthentication(options =>
{
    builder.Configuration.Bind("AzureAdB2C", options.ProviderOptions.Authentication);
    options.ProviderOptions.DefaultAccessTokenScopes.Add("https://togolesesolidarity.onmicrosoft.com/363da589-99ba-4069-944a-a8a3147ca765/User.ReadWrite");
});
builder.Services.AddScoped<IMemberService, MemberService>();
builder.Services.AddScoped<IAlertService, AlertService>();

await builder.Build().RunAsync();
