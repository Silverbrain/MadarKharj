using Microsoft.EntityFrameworkCore;
using PersonalFinance.Client.Pages;
using PersonalFinance.Components;
using PersonalFinance.Repository;
using PersonalFinance.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

builder.Services.AddControllers();

builder.Services.AddScoped(http => new HttpClient{
    BaseAddress = new Uri(builder.Configuration.GetSection("BaseUri").Value!)
});

builder.Services.AddLogging(ops => ops.AddConsole());

builder.Services.AddDbContext<PFDbContext>(ops =>{
    ops.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
});

builder.Services.AddTransient<IAccountRepository, AccountRepository>();
builder.Services.AddTransient<IAccountService, AccountService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.MapControllers();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(PersonalFinance.Client._Imports).Assembly);

app.Run();
