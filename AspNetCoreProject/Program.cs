
//Builder loads the configuration, environment and default services
using Microsoft.AspNetCore.Builder;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.UseStaticFiles();

app.UseRouting();

app.UseEndpoints(endpoints => { 
    endpoints.Map("/", async context =>
    {
        await context.Response.WriteAsync("Hello world!");
    });
});
app.Run(); // start to server
