using AutoMapper;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Smart.Blazor;
using TogoleseAssociationSystem.APP;
using TogoleseAssociationSystem.Core.AutoMapper;
using TogoleseAssociationSystem.Core.ServiceProvider;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7203/") });

builder.Services.AddScoped<IMemberService, MemberService>();

var mapperConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new Profiles());
});
IMapper mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

builder.Services.AddSmart();

await builder.Build().RunAsync();
