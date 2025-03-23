using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using TogoleseSolidarity.APP;
using TogoleseSolidarity.Core.ServiceProvider;
using TogoleseSolidarity.Core.ServiceProvider.Interfaces;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7203/") });

builder.Services.AddScoped<IMemberService, MemberService>();
builder.Services.AddScoped<IAlertService, AlertService>();

await builder.Build().RunAsync();
