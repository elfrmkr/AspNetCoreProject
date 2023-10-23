using AspNetCoreProject.CustomMiddleware;
using Microsoft.Extensions.Primitives;
using System.IO;

//Builder loads the configuration, environment and default services
var builder = WebApplication.CreateBuilder(args);
// add middleware
builder.Services.AddTransient<MyCustomMiddleware>();
var app = builder.Build();


/*********************************************/
// conditional middleware
app.UseWhen(context =>

context.Request.Query.ContainsKey("username"),
    app =>
    {
        app.Use(async (context, next) =>
        {
            await context.Response.WriteAsync("Hello from" +
            " middleware branch");
            await next();

        });
    });

    app.Run(async context =>
    {
        await context.Response.WriteAsync("Hello from" +
        " middleware at main chain");

    });


app.Run(); // start to server
