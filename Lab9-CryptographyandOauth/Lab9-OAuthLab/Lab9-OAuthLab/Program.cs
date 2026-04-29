using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//unsure if i got the lab correctly but if so think that is over kind of interesting tbh
builder.Services.AddControllersWithViews();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddCookie("Bearer")
.AddOAuth("Microsoft", options =>
{
    options.ClientId = builder.Configuration["Authentication:Microsoft:ClientId"];
    options.ClientSecret = builder.Configuration["Authentication:Microsoft:ClientSecret"];
    options.CallbackPath = new PathString("/signin");
    options.AuthorizationEndpoint = "https://login.microsoftonline.com/common/oauth2/v2.0/authorize";
    options.TokenEndpoint = "https://login.microsoftonline.com/common/oauth2/v2.0/token";
    options.UserInformationEndpoint = "https://graph.microsoft.com/v1.0/me";
    options.Scope.Add("user.read");
    options.ClaimActions.MapJsonKey(ClaimTypes.Name, "displayName"); // This now works with the correct version
    options.CorrelationCookie.SameSite = SameSiteMode.Lax;


    options.Events.OnCreatingTicket = async ctx =>
    {
        var request = new HttpRequestMessage(HttpMethod.Get, ctx.Options.UserInformationEndpoint);
        request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", ctx.AccessToken);

        var response = await ctx.Backchannel.SendAsync(
            request,
            HttpCompletionOption.ResponseHeadersRead,
            ctx.HttpContext.RequestAborted);

        var user = JsonDocument.Parse(await response.Content.ReadAsStringAsync());
        ctx.RunClaimActions(user.RootElement);
    };
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
