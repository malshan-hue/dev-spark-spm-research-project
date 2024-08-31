using devspark_core_business_layer.ForumPortalService;
using devspark_core_business_layer.ForumPortalService.Interfaces;
using devspark_core_business_layer.SystemService;
using devspark_core_business_layer.SystemService.Interfaces;

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
builder.Services.AddSingleton<IForumService, ForumServiceImpl>();

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
    pattern: "ForumPortal/{controller=ForumPortalController}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "area",
    pattern: "{area:exists}/{controller=Landing}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=DevsparkLanding}/{action=Index}/{id?}");

app.Run();
