using Azure.Identity;
using devspark_core_business_layer.ContributionPortalService;
using devspark_core_business_layer.ContributionPortalService.Interfaces;
using devspark_core_business_layer.DeveloperPortalService;
using devspark_core_business_layer.DeveloperPortalService.Interfaces;
using devspark_core_business_layer.ForumPortalService;
using devspark_core_business_layer.ForumPortalService.Interfaces;
using devspark_core_business_layer.LearnerPortalService;
using devspark_core_business_layer.LearnerPortalService.Interfaces;
using devspark_core_business_layer.SystemService;
using devspark_core_business_layer.SystemService.Interfaces;
using devspark_core_model.LearnerPortalModels;
using devspark_core_model.SystemModels;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.Graph;
using Newtonsoft.Json;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

#region System Service

var configuration = builder.Configuration;
var connectionString = configuration.GetConnectionString("malshan");

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
builder.Services.AddDataProtection();

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
    options.CallbackPath = "/Access/";
    options.Events = new OpenIdConnectEvents
    {
        OnTokenValidated = async context =>
        {
            var claimsIdentity = (ClaimsIdentity)context.Principal.Identity;
            var redirectUrl = "/Access/SignInSuccess";
            context.Response.Redirect(redirectUrl);
        },
        OnAuthenticationFailed = context =>
        {
            context.HandleResponse();
            return Task.CompletedTask;
        }
    };
    options.SignedOutRedirectUri = "https://localhost:44360/";
});

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/MicrosoftSignIn";
    options.Cookie.SameSite = SameSiteMode.Strict;
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
});

#endregion

#region Learner Portal Services
builder.Services.AddSingleton<IOpenAIStaticService>(provider =>
{
    OpenAICredentials openAICredentials = new OpenAICredentials()
    {
        EndPoint = configuration["OpenAiCredentials:EndPoint"],
        Key = configuration["OpenAiCredentials:Key"],
        DeploymentName = configuration["OpenAiCredentials:DeploymentName"]
    };

    var openAiStaticService = new OpenAIStaticServiceImpl();
    openAiStaticService.SetOpenAICredentials(openAICredentials);
    return openAiStaticService;
});

builder.Configuration.AddJsonFile("prompts.json", optional: false, reloadOnChange: true);

builder.Services.AddSingleton<ICourseService, CourseServiceImpl>();
#endregion

#region Developer Portal Services

builder.Services.AddSingleton<ICreateDevSpace, CreateDevSpaceServiceImpl>();

#endregion

#region Forum Portal Services

builder.Services.AddSingleton<IForumService, ForumServiceImpl>();

#endregion

#region Contribution Portal Services

builder.Services.AddSingleton<ICodeSnippetService, CodeSnippetServiceImpl>();

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

app.UseAuthentication();
app.UseAuthorization();
app.UseSession();

app.MapAreaControllerRoute(
    name: "LearnerPortal",
    areaName: "LearnerPortal",
    pattern: "LearnerPortal/{controller=Dashboard}/{action=Index}/{id?}");

app.MapAreaControllerRoute(
    name: "DeveloperPortal",
    areaName: "DeveloperPortal",
    pattern: "DeveloperPortal/{controller=DevSpace}/{action=HomeIndex}/{id?}");

app.MapAreaControllerRoute(
    name: "ContributionPortal",
    areaName: "ContributionPortal",
    pattern: "ContributionPortal/{controller=DevSpace}/{action=HomeIndex}/{id?}");

app.MapAreaControllerRoute(
    name: "ForumPortal",
    areaName: "ForumPortal",
    pattern: "ForumPortal/{controller=ForumPortalController}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "areaRoute",
    pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=DevsparkLanding}/{action=Index}/{id?}");

app.Run();