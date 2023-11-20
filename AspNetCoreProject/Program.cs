using AspNetCoreProject.CustomMiddleware;

//Builder loads the configuration, environment and default services
var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// routing example
app.UseRouting(); // enable routing


// creating endpoints
app.UseEndpoints(endpoints =>
{
    endpoints.Map("files/{filename}.{extension}", async context =>
    {
        string? filename = Convert.ToString(context.Request.RouteValues["filename"]);
        string? extension = Convert.ToString(context.Request.RouteValues["extension"]);
        await context.Response.WriteAsync($"In files - {filename} - {extension}");
    });

    // case insensitive
    endpoints.Map("employee/profile/{employeeName}", async context =>
    {
        string? employeeName = Convert.ToString(context.Request.RouteValues["employeeName"]);
        await context.Response.WriteAsync($"Employee name - {employeeName}");
    });
});

app.Run(async context =>
{
    await context.Response.WriteAsync($"Request received at" +
        $"{context.Request.Path}");
});
app.Run(); // start to server
