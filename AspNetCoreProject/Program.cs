using AspNetCoreProject.CustomConstraints;

//Builder loads the configuration, environment and default services
var builder = WebApplication.CreateBuilder(args);
// add constraint
builder.Services.AddRouting(options =>
{
    options.ConstraintMap.Add("months", typeof(MonthsCustomConstraint));

});

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

    // case insensitive, you can give default parameters when nothing is provided: elif in this case
    // we can give min and max length
    endpoints.Map("employee/profile/{employeeName:length(4,8):alpha=elif}", async context =>
    {
        string? employeeName = Convert.ToString(context.Request.RouteValues["employeeName"]);
        await context.Response.WriteAsync($"Employee name - {employeeName}");
    });



    // making the id as an optional parameter
    // we can make constraints for router parameters: int, bool, datetime
    // if it doesn't satisfy it, it will fallback to other endpoints that satisfes
    endpoints.Map("/products/details/{id:int:range(1,1000)?}", async context =>
    {
        if(context.Request.RouteValues.ContainsKey("id"))
        {
            int id = Convert.ToInt32(context.Request.RouteValues["id"]);
            await context.Response.WriteAsync($"Product id - {id}");
        } else
        {
            await context.Response.WriteAsync("Product details id is not supplied");
        }
    });

    //daily-digest-report/{reportdate}
    endpoints.Map("/daily-digest-report/{reportdate:datetime}", async context =>
    {
        if (context.Request.RouteValues.ContainsKey("reportdate"))
        {
            DateTime reportdate = Convert.ToDateTime(context.Request.RouteValues["reportdate"]);
            await context.Response.WriteAsync($"Date - {reportdate}");
        }
        else
        {
            await context.Response.WriteAsync("Date is not supplied");
        }
    });

    //cities/cityid

    endpoints.Map("/cities/{cityid:guid}", async context =>
    {
            Guid cityid =Guid.Parse(Convert.ToString(context.Request.RouteValues["cityid"])!); // value cannot be null
            await context.Response.WriteAsync($"City information - {cityid}");   
    });

    //sales-report/2030/apr, :regex(^(apr|jul|oct|jan)$)
    endpoints.Map("/sales-report/{year:int:min(1900)}/{month:months}", async context =>
    {
        int year = Convert.ToInt32(context.Request.RouteValues["year"]);
        string? month = Convert.ToString(context.Request.RouteValues["month"]);
        if(month == "apr" || month == "jul" || month == "oct" || month == "jan") {
          await context.Response.WriteAsync($"Sales report year:{year} and month: {month}");
        } else  {
            await context.Response.WriteAsync($"This month({month}) is not allowed  for sales report");
        }
    });
});

app.Run(async context =>
{
    await context.Response.WriteAsync($"No route macthed at" +
        $"{context.Request.Path}");
});
app.Run(); // start to server
