using BuyandRentHomeWebAPI.Data;
using BuyandRentHomeWebAPI.Extensions;
using BuyandRentHomeWebAPI.Helper;
using BuyandRentHomeWebAPI.Data.Interfaces;
using BuyandRentHomeWebAPI.Middlewares;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using BuyandRentHomeWebAPI.Services.Interfaces;
using BuyandRentHomeWebAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.OpenApi.Models;
using Microsoft.Extensions.FileProviders;
using System.IO;
using BuyandRentHomeWebAPI.WebSocketHandlers;
using System.Collections.Generic;

namespace BuyandRentHomeWebAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container. 
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<BuyRentHomeDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("Default")));

            services.AddControllers().AddNewtonsoftJson();
            services.AddCors();
            services.AddAutoMapper(typeof(AutoMapperProfiles).Assembly);
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            // resolve services
            services.AddScoped<ISharedService, SharedService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IPhotoService, PhotoService>();
            services.AddScoped<IPropertyService, PropertyService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IUserPrivilegeService, UserPrivilegeService>();
            services.AddScoped<ICitiesAreaManagerService, CitiesAreaManagerService>();
            services.AddScoped<IVisitingRequestService, VisitingRequestService>();


            services.AddSingleton<IChatWebSocketHandler, ChatWebSocketHandler>();

            var secretKey = Configuration.GetSection("AppSettings:Key").Value;
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(opt =>
                {
                    opt.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        IssuerSigningKey = key,
                    };
                });

            services.AddSwaggerGen(c =>
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

            services.AddResponseCaching();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            string fileDirectory = "../Upload\\files";

            app.ConfigureExceptionHandler(env);

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

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
