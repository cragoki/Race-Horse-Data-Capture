using Stashbox;
using Infrastructure.Config.IoC;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Blazored.Modal;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddBlazoredModal();


builder.Host.UseStashbox();
builder.Host.ConfigureContainer<IStashboxContainer>((context, container) =>
{
    container.AddDependencies(context.Configuration);
});

builder.Host.ConfigureServices((hostContext, services) =>
{
    services.AddDbContextPool<DbContextData>(option =>
        option.UseSqlServer(hostContext.Configuration.GetConnectionString("DefaultConnection"),
        sqlServerOptions => sqlServerOptions.CommandTimeout(120)));
});

var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}



//Using Stashbox for Dependancies


app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
