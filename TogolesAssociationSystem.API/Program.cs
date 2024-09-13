using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using TogoleseAssociationSystem.Application.AutoMapper;
using TogoleseAssociationSystem.Application.Configurations;
using TogoleseAssociationSystem.Application.Database;
using TogoleseAssociationSystem.Application.Repositories;
using TogoleseAssociationSystem.Application.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//glogal filter for all controllers autorization attribute set
builder.Services.AddControllers(o =>o.Filters.Add(new AuthorizeFilter()));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

var azureAdConfig = builder.Configuration.GetSection("AzureAd").Get<AzureAdSettings>();
var swaggerConfig = builder.Configuration.GetSection("SwaggerConfiguration").Get<SwaggerSettings>();
//configer to add client credential flow with the token url pointing to the token endpoint of the Identity provider and the scope global api defined 
builder.Services.AddSwaggerGen(o =>
{
    o.AddSecurityDefinition(swaggerConfig.AuthType, new OpenApiSecurityScheme
    {
        Name = swaggerConfig.AuthType,
        Description = swaggerConfig.Description,
        Type = SecuritySchemeType.OAuth2,
        Flows = new OpenApiOAuthFlows
        {
            Implicit = new OpenApiOAuthFlow
            {
                AuthorizationUrl = new Uri(string.Format(swaggerConfig.AuthUrlFormat, azureAdConfig.TenantId), UriKind.RelativeOrAbsolute),
                Scopes = new Dictionary<string, string>
                {
                    {swaggerConfig.Scope, swaggerConfig.AuthScope}
                }
            }
        }
    });
    o.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Id = swaggerConfig.AuthType,
                    Type = ReferenceType.SecurityScheme
                }
            }, new List<string>(){ swaggerConfig.AuthScope }
        }
    });
    o.SwaggerDoc(swaggerConfig.Version, new OpenApiInfo
    {
        Title = swaggerConfig.Title,
        Version = swaggerConfig.Version
    });
    o.DescribeAllParametersInCamelCase();

    AddSwaggerXmlComments(o);
});

builder.Services.AddScoped<IMemberService, MemberService>();
builder.Services.AddScoped<IMemberRepository, MemberRepository>();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.Audience = azureAdConfig.Audience;
    options.Authority = string.Format(azureAdConfig.Instance, azureAdConfig.TenantId);
    options.TokenValidationParameters = new TokenValidationParameters
    {
        SaveSigninToken = true,
        ValidateLifetime = true,
        ValidateAudience = true,
        ValidateIssuer = true
    };
});

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
    //app.UseSwaggerUI(c =>
    //{
    //    c.RoutePrefix = string.Empty;
    //    c.SwaggerEndpoint(string.Format(swaggerConfig.UrlFormat, swaggerConfig.Version), swaggerConfig.Title);
    //    c.OAuthClientId(azureAdConfig.ClientId);
    //});
}

//here is the url that's been granted access by the CORS policy (use web url address allowed )
app.UseCors(policy => policy.WithOrigins("https://localhost:7031")
.AllowAnyMethod()
.WithHeaders(HeaderNames.ContentType)
.AllowCredentials());

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

//DbInitializer.EnsureSeedData(app);

app.Run();
