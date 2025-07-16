using BlazorAuthDemo.Client;
using BlazorAuthDemo.Client.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("http://localhost:5180") });

builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<AuthState>();
builder.Services.AddScoped<AuthenticationStateProvider>(provider => provider.GetRequiredService<AuthState>());


builder.Services.AddOidcAuthentication(options =>
{
    // Configure your authentication provider options here.
    // For more information, see https://aka.ms/blazor-standalone-auth
    builder.Configuration.Bind("Local", options.ProviderOptions);
});

await builder.Build().RunAsync();
