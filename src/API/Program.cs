using API.Extensions;
using Application;
using BuyAndRentHomeWebAPI.Middlewares;
using Infrastructure;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);

var presentationAssembly = typeof(Presentation.AssemblyReference).Assembly;
builder.Services.AddControllers().AddApplicationPart(presentationAssembly);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddResponseCaching();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

string fileDirectory = "../Upload\\files";

app.ConfigureExceptionHandler(app.Environment);

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), fileDirectory)),
    RequestPath = "/StaticFiles"
});

app.UseSwagger();
app.UseSwaggerUI();

app.UseRouting();

app.UseCors(options => options.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

app.UseResponseCaching();

app.UseAuthentication();
app.UseAuthorization();

app.UseWebSockets();
app.UseWebSocketMiddleware();



app.Run();
