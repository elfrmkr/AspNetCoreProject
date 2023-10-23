using AspNetCoreProject.CustomMiddleware;
using Microsoft.Extensions.Primitives;
using System.IO;

//Builder loads the configuration, environment and default services
var builder = WebApplication.CreateBuilder(args);
// add middleware
builder.Services.AddTransient<MyCustomMiddleware>();
var app = builder.Build();

//app.Run(async (HttpContext context) =>
//{
//    System.IO.StreamReader reader = new StreamReader(context.Request.Body);
//    string body = await reader.ReadToEndAsync();
//    //parsing string to dictionary, keys should be unique but can have multiple values
//    Dictionary<string, StringValues> queryDict =
//    Microsoft.AspNetCore.WebUtilities.QueryHelpers.ParseQuery(body);

//    if(queryDict.ContainsKey("firstName"))
//    {
//        string firstName = queryDict["firstName"][0];
//        await context.Response.WriteAsync(firstName);
//    }
//});


/*********************************************/

//// this is a middleware
//app.Run(async (HttpContext context) => {
//    await context.Response.WriteAsync("Hello");
//});

//// second middleware doesnt execute because of the method used
//// to form middleware (Run)
//// Run doesn't execute chain middleware
//app.Run(async (HttpContext context) => {
//    await context.Response.WriteAsync("Hello again");
//});

/*********************************************/

// middleware 1, takes two arguments
app.Use(async (HttpContext context, RequestDelegate next) =>
{
    await context.Response.WriteAsync("Middleware 1\n");
    await next(context); //calling the next middleware
});

//middleware 2
//app.UseMiddleware<MyCustomMiddleware>();
//app.UseMyCustomMiddleware();
app.UseHelloCustomMiddleware();

//middleware 3, takes only one argument
app.Run(async (HttpContext context) => {
    await context.Response.WriteAsync("Middleware 3\n");
});


app.Run(); // start to server
