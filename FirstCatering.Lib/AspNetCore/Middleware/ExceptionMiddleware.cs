using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Net;
using System.Net.Mime;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using FirstCatering.Lib.Logging;

namespace FirstCatering.Lib.AspNetCore.Middleware
{
    /// <summary>
    /// Middleware to catch unhandled exceptions when invoking actions
    /// and return HTTP500
    /// </summary>
    public class ExceptionMiddleware
    {
        /// <summary>
        /// <see cref="RequestDelegate"/> request
        /// </summary>
        private RequestDelegate Request { get; }

        /// <summary>
        /// <see cref="IWebHostEnvironment"/> host environment
        /// </summary>
        private IWebHostEnvironment Environment { get; }

        /// <summary>
        /// <see cref="ILogger"/> logger
        /// </summary>
        private ILogger Logger { get; }

        /// <summary>
        /// Initialises a <see cref="ExceptionMiddleware"/> for the specified <paramref name="request"/>
        /// in the <paramref name="environment"/> using the given <paramref name="logger"/>
        /// </summary>
        /// <param name="request"><see cref="RequestDelegate"/> request</param>
        /// <param name="environment"><see cref="IWebHostEnvironment"/> host environment</param>
        /// <param name="logger"><see cref="ILogger"/> logger</param>
        public ExceptionMiddleware(
            RequestDelegate request,
            IWebHostEnvironment environment,
            ILogger logger)
        {
            Environment = environment;
            Logger = logger;
            Request = request;
        }

        /// <summary>
        /// Action invocation interceptor to wrap in try/catch statement
        /// </summary>
        /// <param name="context"><see cref="HttpContext"/> context</param>
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await Request(context).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                if (Environment.IsDevelopment())
                    throw;
                await ResponseAsync(context, HttpStatusCode.InternalServerError, string.Empty).ConfigureAwait(false);
            }
        }

        /// <summary>
        /// Action response interceptor on an unhandled exception
        /// to return desired error response
        /// </summary>
        /// <param name="context"><see cref="HttpContext"/> context</param>
        /// <param name="statusCode"><see cref="HttpStatusCode"/> response status code</param>
        /// <param name="response">response</param>
        private static async Task ResponseAsync(HttpContext context, HttpStatusCode statusCode, string response)
        {
            context.Response.Clear();
            context.Response.StatusCode = (int)statusCode;
            context.Response.ContentType = MediaTypeNames.Application.Json;

            await context.Response.WriteAsync(response).ConfigureAwait(false);
        }
    }
}