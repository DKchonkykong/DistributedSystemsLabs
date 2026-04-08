using SignaIRLab5.Hubs;

var builder = WebApplication.CreateBuilder(args);

//port number is: 58088
// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddSignalR();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.MapHub<ChatroomHub>("/chatroomhub");

app.Run();
