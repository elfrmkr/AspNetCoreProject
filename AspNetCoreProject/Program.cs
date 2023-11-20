using AspNetCoreProject.CustomMiddleware;
using Microsoft.Extensions.Primitives;
using System.IO;

//Builder loads the configuration, environment and default services
var builder = WebApplication.CreateBuilder(args);
// add middleware
builder.Services.AddTransient<MyCustomMiddleware>();
var app = builder.Build();

// routing example
app.UseRouting(); // enable routing
// creating endpoints
app.UseEndpoints(endpoints =>
{
    // add your end point, Map METHODS
    endpoints.Map("/map1", async (context) =>
    {
        await context.Response.WriteAsync("In map 1");
    });
    endpoints.Map("/map2", async (context) =>
    {
        await context.Response.WriteAsync("In map 2");
    });
});


app.Run(); // start to server
