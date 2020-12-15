using ABCBankWebApi.Helpers;
using ABCBankWebApi.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using System.Net;

namespace ABCBankWebApi.Extensions
{
    public static class ExceptionFactory
    {
        public static void ConfigureExceptionHandler(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(a => a.Run(async context =>
            { 
                context.Response.ContentType = "application/json";
                var contextFeature = context.Features.Get<IExceptionHandlerPathFeature>();
                if (contextFeature != null)
                {
                    LogTraceFactory.LogError(500,$"Something went wrong:{contextFeature.Error.Message}");
                    await context.Response.WriteAsync(new ErrorDetails()
                    {
                        Message = "Internal Server Error",
                        StatusCode = context.Response.StatusCode
                    }.ToString());
                }
            }));
        }
    }
}
