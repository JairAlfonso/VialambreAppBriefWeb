using Amazon.S3;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NToastNotify;
using VialambreAppTest1.Data;
using VialambreAppTest1.Models;
using VialambreAppTest1.Utility;

var builder = WebApplication.CreateBuilder(args);

// Add service DbContext Users
builder.Services.AddDbContext<ViAppContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DbConnection")));

builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.AddScoped<BriefService>();

// Add service ToastNotify
builder.Services.AddRazorPages().AddNToastNotifyToastr(new ToastrOptions()
{
    ProgressBar = true,
    PositionClass = ToastPositions.TopCenter,
    PreventDuplicates = true,
    CloseButton = true
});

// Add services Identity
builder.Services.AddIdentity<AppUser, IdentityRole>(options =>
{
    options.Password = new PasswordOptions
    {
        RequireDigit = false,
        RequireUppercase = false,
        RequireLowercase = false,
        RequireNonAlphanumeric = false,
    };
}).AddEntityFrameworkStores<ViAppContext>().AddDefaultTokenProviders();


builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Users/Login";
    options.AccessDeniedPath = "/Users/AccessDenied";
});

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddDefaultAWSOptions(builder.Configuration.GetAWSOptions());

builder.Services.AddAWSService<IAmazonS3>();



var app = builder.Build();

// Add sedder to the database
SeedDatabase();

void SeedDatabase()
{
    using var scope = app.Services.CreateScope();
    try
    {
        var scopedContext = scope.ServiceProvider.GetRequiredService<ViAppContext>();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

        DbInitializer.InitializeAsync(userManager, roleManager).Wait();
        DbInitializer.InitializeAsync(scopedContext).Wait();
    }
    catch
    {

    }
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapRazorPages();

app.Run();

