using API.Middlewares;
using BuyAndRentHomeWebAPI.Middlewares;
using Microsoft.AspNetCore.Diagnostics;
using System.Net;

namespace API.Extensions
{
    public static class ExceptionMiddlewareExtensions
    {
        public static void ConfigureExceptionHandler(this IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseMiddleware<ExceptionMiddleware>();
        }

        public static void ConfigureBuiltinExceptionHandler(this IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler(options =>
                {
                    options.Run(async context =>
                    {
                        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        var ex = context.Features.Get<IExceptionHandlerFeature>();
                        if (ex != null)
                        {
                            await context.Response.WriteAsync(ex.Error.Message);
                        }
                    });
                });
            }
        }
    }
}
