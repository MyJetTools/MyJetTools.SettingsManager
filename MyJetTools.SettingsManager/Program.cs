using System.Net;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using MudBlazor.Services;
using MyJetTools.SettingsManager;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.BindServices(new SettingsAccessor());
builder.Services.BindNoSql(new SettingsAccessor());
builder.Services.AddMudServices();


builder.WebHost.ConfigureKestrel(options =>
{
    var httpPort = Environment.GetEnvironmentVariable("HTTP_PORT") ?? "80";
    var grpcPort = Environment.GetEnvironmentVariable("GRPC_PORT") ?? "8080";

    Console.WriteLine($"HTTP PORT: {httpPort}");
    Console.WriteLine($"GRPC PORT: {grpcPort}");

    options.Listen(IPAddress.Any, int.Parse(httpPort), o => o.Protocols = HttpProtocols.Http1);
    options.Listen(IPAddress.Any, int.Parse(grpcPort), o => o.Protocols = HttpProtocols.Http2);
});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");
app.MapControllers();

app.Run();