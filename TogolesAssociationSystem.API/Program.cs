using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Net.Http.Headers;
using TogoleseAssociationSystem.Application.AutoMapper;
using TogoleseAssociationSystem.Application.Database;
using TogoleseAssociationSystem.Application.Repositories;
using TogoleseAssociationSystem.Application.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IMemberService, MemberService>();
builder.Services.AddScoped<IMemberRepository, MemberRepository>();

var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json")
    .Build();
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(configuration.GetConnectionString("DefaultConnectionString"));
});

var mapperConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new Profiles());
});
IMapper mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//here is the url that's been granted access by the CORS policy (use web url address allowed )
app.UseCors(policy => policy.WithOrigins("https://localhost:7031")
.AllowAnyMethod()
.WithHeaders(HeaderNames.ContentType)
.AllowCredentials());

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

DbInitializer.EnsureSeedData(app);

app.Run();
