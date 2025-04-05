using AutoMapper;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.Net.Http.Headers;
using Swashbuckle.AspNetCore.SwaggerGen;
using TogoleseSolidarity.Application.AutoMapper;
using TogoleseSolidarity.Application.Services;
using TogoleseSolidarity.Domain.Interfaces;
using TogoleseSolidarity.Infrastructure.Database;
using TogoleseSolidarity.Application.Configurations;
using TogoleseSolidarity.Infrastructure.Repositories;
using TogoleseSolidarity.API.Middleware;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Identity.Web;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

var azureAdConfig = builder.Configuration.GetSection("AzureAd").Get<AzureAdSettings>();
//var azureAdB2CConfig = builder.Configuration.GetSection("AzureAdB2C").Get<AzureAdB2cSettings>();
var swaggerConfig = builder.Configuration.GetSection("SwaggerConfiguration").Get<SwaggerSettings>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddMicrosoftIdentityWebApi(builder.Configuration.GetSection("AzureAdB2C"));

//builder.Services.AddSwaggerGen(o =>
//{
//    o.AddSecurityDefinition(swaggerConfig.AuthType, new OpenApiSecurityScheme
//    {
//        Name = swaggerConfig.AuthType,
//        Description = swaggerConfig.Description,
//        Type = SecuritySchemeType.OAuth2,
//        Flows = new OpenApiOAuthFlows
//        {
//            Implicit = new OpenApiOAuthFlow
//            {
//                AuthorizationUrl = new Uri(string.Format(swaggerConfig.AuthUrlFormat, azureAdConfig.TenantId), UriKind.RelativeOrAbsolute),
//                Scopes = new Dictionary<string, string>
//                {
//                    {swaggerConfig.Scope, swaggerConfig.AuthScope}
//                }
//            }
//        }
//    });
//    o.AddSecurityRequirement(new OpenApiSecurityRequirement
//    {
//        {
//            new OpenApiSecurityScheme
//            {
//                Reference = new OpenApiReference
//                {
//                    Id = swaggerConfig.AuthType,
//                    Type = ReferenceType.SecurityScheme
//                }
//            }, new List<string>(){ swaggerConfig.AuthScope }
//        }
//    });
//    o.SwaggerDoc(swaggerConfig.Version, new OpenApiInfo
//    {
//        Title = swaggerConfig.Title,
//        Version = swaggerConfig.Version
//    });
//    o.DescribeAllParametersInCamelCase();

//    AddSwaggerXmlComments(o);
//});


//builder.Services.AddAuthentication(options =>
//{
//    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
//})
//.AddJwtBearer(options =>
//{
//    options.Audience = azureAdConfig.Audience;
//    options.Authority = string.Format(azureAdConfig.Instance, azureAdConfig.TenantId);
//    options.TokenValidationParameters = new TokenValidationParameters
//    {
//        SaveSigninToken = true,
//        ValidateLifetime = true,
//        ValidateAudience = true,
//        ValidateIssuer = true
//    };
//});

//Adding OIDC authentication
//builder.Services.AddAuthentication(options => 
//{ 
//    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
//options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme; }
//).AddOpenIdConnect(options =>
//{
//    options.ClientId = azureAdB2CConfig.ClientId;
//    options.ClientSecret = "your-client-secret";
//    options.Authority = "https://your-identity-provider.com";
//    options.ResponseType = OpenIdConnectResponseType.Code;
//    options.CallbackPath = "/signin-oidc";
//    options.Scope.Add("openid");
//    options.Scope.Add("profile");
//    options.Scope.Add("email");
//});

//builder.Services.AddAuthentication(OpenIdConnectDefaults.AuthenticationScheme)
//    .AddSignIn("AzureAdB2C", builder.Configuration,
//    options => Configuration.Bind("AzureAdB2C", options));

//builder.Services.Configure<OpenIdConnectOptions>(builder.Configuration.GetSection("AzureAdB2C"));

builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IMemberService, MemberService>();
builder.Services.AddScoped<IMemberRepository, MemberRepository>();

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnectionString"));
});

var mapperConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new Profiles());
});

static void AddSwaggerXmlComments(SwaggerGenOptions options)
{
    foreach (var xmlDocFile in Directory.EnumerateFiles(AppContext.BaseDirectory, "*.XML"))
        options.IncludeXmlComments(xmlDocFile);
}
IMapper mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();

    app.UseSwaggerUI();
}

app.UseCors(policy => policy.WithOrigins("https://localhost:7031")
.AllowAnyMethod()
.WithHeaders(HeaderNames.ContentType)
.AllowCredentials());

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseMiddleware<ExceptionHandlerMiddleware>();

app.Run();
