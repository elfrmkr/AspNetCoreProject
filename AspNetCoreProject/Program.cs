

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers(); // add all controller classes

var app = builder.Build();

//app.UseRouting();
//app.UseEndpoints(endpoints => {  endpoints.MapControllers(); });

app.MapControllers(); // this is enough

app.Run();