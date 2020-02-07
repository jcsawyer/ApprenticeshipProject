using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using FirstCatering.Lib.AspNetCore.Extensions;
using FirstCatering.Lib.IoC;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.IO;

namespace FirstCatering.Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
            => Configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services"><see cref="IServiceCollection"/> service collection</param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddLogger();
            services.AddCors();
            services.AddSecurity();
            services.AddResponseCompression();
            services.AddResponseCaching();
            services.AddControllers();
            services.AddMvcJson();
            services.AddApplicationServices();
            services.AddDbContext();
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "FirstCatering.Api", Version = "v1" });
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.XML";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                options.IncludeXmlComments(xmlPath);
            });
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app"><see cref="IApplicationBuilder"/> app builder</param>
        /// <param name="env"><see cref="IWebHostEnvironment"/> web host environment</param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseException();
            app.UseRouting();
            app.UseCorsAllowAny();
            app.UseHttps();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseResponseCompression();
            app.UseResponseCaching();
            app.UseStaticFiles();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            app.UseSwagger();
            app.UseSwaggerUI(options => {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "FirstCatering.Api");
            });
        }
    }
}
