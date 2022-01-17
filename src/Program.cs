using System.Configuration;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WDPR_A.ViewModels;
using WDPR_A.Hubs;
using Microsoft.Extensions.Hosting;
using Microsoft.Data.SqlClient;
using Azure.Security.KeyVault.Secrets;
using Azure.Identity;
using System;

var builder = WebApplication.CreateBuilder(args);
// connectie string voor sqlserver of sqlite (dezelfde naam )
var connectionString = builder.Configuration.GetConnectionString("WDPRContextConnection");
builder.Services.AddDbContext<WDPRContext>(options =>
{
    if (builder.Environment.IsProduction())
    {
        var cS = new SqlConnectionStringBuilder(connectionString);
        var DB_URL = builder.Configuration.GetConnectionString("DB_URL");
        var DB_KEY = builder.Configuration.GetConnectionString("DB_KEY");
        var client = new SecretClient(new Uri(DB_URL), new DefaultAzureCredential());
        var secret = client.GetSecret(DB_KEY);
        cS.Password = secret.Value.Value;
        options.UseSqlServer(cS.ConnectionString);
    }
    else
    {
        options.UseSqlite(connectionString);
    }
});
builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<WDPRContext>();
builder.Services.AddControllersWithViews();
builder.Services.AddSignalR();
builder.Services.AddScoped<RoleSystem>();
builder.Services.AddTransient<Random>(); // injects a new instance to every service that uses it
builder.Services.AddScoped<Generate>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddTransient<ChatViewModel>();
builder.Services.AddTransient<ChatManager>();

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


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.MapHub<ChatHub>("/chatHub");

app.Run();