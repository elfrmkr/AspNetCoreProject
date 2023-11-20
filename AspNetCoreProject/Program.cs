using AspNetCoreProject.CustomMiddleware;

//Builder loads the configuration, environment and default services
var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.Use(async (context, next) =>
{
   Microsoft.AspNetCore.Http.Endpoint? endpoint = context.GetEndpoint();
    if(endpoint != null)
    {
       await context.Response.WriteAsync($"Endpoint: {endpoint.DisplayName}\n");
    }
    await next(context);
});

// routing example
app.UseRouting(); // enable routing


app.Use(async (context, next) =>
{
    Microsoft.AspNetCore.Http.Endpoint? endpoint = context.GetEndpoint();
    if (endpoint != null)
    {
        await context.Response.WriteAsync($"Endpoint: {endpoint.DisplayName}\n");
    }
    await next(context);
});
// creating endpoints
app.UseEndpoints(endpoints =>
{
    // add your end point, Map METHODS
    endpoints.MapGet("/map1", async (context) =>
    {
        await context.Response.WriteAsync("In map 1");
    });
    endpoints.MapPost("/map2", async (context) =>
    {
        await context.Response.WriteAsync("In map 2");
    });
});

app.Run(async context =>
{
    await context.Response.WriteAsync($"Request received at" +
        $"{context.Request.Path}");
});
app.Run(); // start to server
