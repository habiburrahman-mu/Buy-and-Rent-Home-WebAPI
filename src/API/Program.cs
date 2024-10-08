using API.Extensions;
using API.WebSocketHandlers;
using Application;
using BuyAndRentHomeWebAPI.Middlewares;
using Infrastructure;
using Presentation;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Infrastructure.Hubs;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddPresentationServices(builder.Configuration);

var presentationAssembly = typeof(Presentation.AssemblyReference).Assembly;
builder.Services.AddControllers().AddApplicationPart(presentationAssembly).AddNewtonsoftJson();
builder.Services.AddCors();

//builder.Services.AddSingleton<IChatWebSocketHandler, ChatWebSocketHandler>();

var secretKey = builder.Configuration.GetSection("AppSettings:Key").Value;
if (secretKey is null)
    throw new ArgumentNullException("Secret key not found.");
var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(opt =>
    {
        opt.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            ValidateIssuer = false,
            ValidateAudience = false,
            IssuerSigningKey = key,
        };

        opt.Events = new JwtBearerEvents
        {
            OnMessageReceived = context =>
            {
                var accessToken = context.Request.Query["access_token"];

                // If the request is for our hub...
                var path = context.HttpContext.Request.Path;
                if (!string.IsNullOrEmpty(accessToken) && (path.StartsWithSegments("/notifications")))
                {
                    // Read the token out of the query string
                    context.Token = accessToken;
                }
                return Task.CompletedTask;
            }
        };
    });

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Buy Rent Home Api", Version = "v1" });
    c.AddSecurityDefinition("bearer", new OpenApiSecurityScheme
    {
        Description = "`Token` - with `Bearer` prefix",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Scheme = "bearer"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement() {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "bearer"
                },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header,
            },
            new List<string>()
        }
    });
});

builder.Services.AddResponseCaching();

var app = builder.Build();

string fileDirectory = "../Upload\\files";

app.ConfigureExceptionHandler(app.Environment);

string providerPath = Path.Combine(Directory.GetCurrentDirectory(), fileDirectory);
Directory.CreateDirectory(providerPath); // if exists it will ignore

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(providerPath),
    RequestPath = "/StaticFiles"
});

app.UseSwagger();
app.UseSwaggerUI();

//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

app.UseRouting();

app.UseCors(options => options.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

app.UseResponseCaching();

app.UseAuthentication();
app.UseAuthorization();

//app.UseWebSockets();
//app.UseWebSocketMiddleware();

app.UseEndpoints(endpoints =>
{
    _ = endpoints.MapControllers();
});

app.MapHub<NotificationsHub>("notifications");

app.Run();
