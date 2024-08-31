using Azure.Identity;
using devspark_core_business_layer.DeveloperPortalService;
using devspark_core_business_layer.DeveloperPortalService.Interfaces;
using devspark_core_business_layer.SystemService;
using devspark_core_business_layer.SystemService.Interfaces;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.Graph;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

#region System Service

var configuration = builder.Configuration;
var connectionString = configuration.GetConnectionString("dev");

builder.Services.AddSingleton<IDatabaseService>(provider =>
{
    var dbService = new DatabaseServiceImpl();
    dbService.SetConnectionString(connectionString);
    return dbService;
});

builder.Services.AddSingleton<IUserService, UserServiceImpl>();
builder.Services.AddSingleton<IMicrosoftGraphService, MicrosoftGraphServiceImpl>();

builder.Services.AddSingleton(sp =>
{
    var scopes = new[] { "https://graph.microsoft.com/.default" };

    var clientId = configuration["EntraId:ClientId"];
    var tenantId = configuration["EntraId:TenantId"];
    var clientSecret = configuration["EntraId:ClientSecret"];

    var options = new TokenCredentialOptions
    {
        AuthorityHost = AzureAuthorityHosts.AzurePublicCloud
    };

    var credential = new ClientSecretCredential(tenantId, clientId, clientSecret, options);
    return new GraphServiceClient(credential, scopes);
});

builder.Services.AddSingleton<IMailService, MailServiceImpl>();

#endregion

#region MICROSOFT LOGIN OPENID

builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
})
.AddCookie()
.AddOpenIdConnect(options =>
{
    options.ClientId = configuration["EntraId:ClientId"];
    options.ClientSecret = configuration["EntraId:ClientSecret"];
    options.Authority = $"https://login.microsoftonline.com/{configuration["EntraId:TenantId"]}";
    options.ResponseType = "code";
    options.SaveTokens = true;
    options.Scope.Add("openid");
    options.Scope.Add("profile");
    options.Scope.Add("email");
    options.CallbackPath = "/DevsparkLanding/";
    options.Events = new OpenIdConnectEvents
    {
        OnTokenValidated = async context =>
        {
            var claimsIdentity = (ClaimsIdentity)context.Principal.Identity;
            var redirectUrl = "/DevsparkLanding/DevSparkHome";
            context.Response.Redirect(redirectUrl);
            context.HandleResponse();
        },
        OnAuthenticationFailed = context =>
        {
            context.HandleResponse();
            return Task.CompletedTask;
        }
    };
});
builder.Services.AddSingleton<ICreateDevSpace, CreateDevSpaceServiceImpl>();

#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapAreaControllerRoute(
    name: "LearnerPortal",
    areaName: "LearnerPortal",
    pattern: "LearnerPortal/{controller=Landing}/{action=Index}/{id?}");

app.MapAreaControllerRoute(
    name: "DeveloperPortal",
    areaName: "DeveloperPortal",
    pattern: "DeveloperPortal/{controller=Landing}/{action=Index}/{id?}");

app.MapAreaControllerRoute(
    name: "ContributionPortal",
    areaName: "ContributionPortal",
    pattern: "ContributionPortal/{controller=Landing}/{action=Index}/{id?}");

app.MapAreaControllerRoute(
    name: "ForumPortal",
    areaName: "ForumPortal",
    pattern: "ForumPortal/{controller=Landing}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "area",
    pattern: "{area:exists}/{controller=Landing}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=DevsparkLanding}/{action=Index}/{id?}");

app.Run();
