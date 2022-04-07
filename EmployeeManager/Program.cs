global using EmployeeManager.Data;
global using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Newtonsoft.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("EmployeeAppCon"));
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

}


);
builder.Services.AddControllers();

//JSON Serializer
builder.Services.AddControllersWithViews().AddNewtonsoftJson(options =>
options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore)
    .AddNewtonsoftJson(options => options.SerializerSettings.ContractResolver
    = new DefaultContractResolver());



var app = builder.Build();

// Enable CORS
app.UseCors(options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
 



// Configure the HTTP request pipeline.


app.UseAuthorization();

app.MapControllers();
app.UseStaticFiles(new StaticFileOptions{

    FileProvider=new PhysicalFileProvider(
        Path.Combine(Directory.GetCurrentDirectory(),"Photos")),
    RequestPath="/Photos"


});

app.Run();
