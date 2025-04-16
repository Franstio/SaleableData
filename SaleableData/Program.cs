using Blazorise;
using Blazorise.Icons.FontAwesome;
using Blazorise.Tailwind;
using SaleableData.Components;
using SaleableData.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
builder.Services.AddBlazorise().AddTailwindProviders().AddFontAwesomeIcons();
builder.Services.AddScoped<Step2DataService>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
}


app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
