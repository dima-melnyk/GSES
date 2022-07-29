using Amazon.S3;
using GSES.BusinessLogic.Processors;
using GSES.BusinessLogic.Processors.Interfaces;
using GSES.BusinessLogic.Services;
using GSES.BusinessLogic.Services.Interfaces;
using GSES.DataAccess.Repositories;
using GSES.DataAccess.Repositories.Interfaces;
using GSES.DataAccess.Storages.Bases;
using GSES.DataAccess.Storages.File;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.Net;
using System.Net.Mail;
using FluentValidation;
using GSES.BusinessLogic.Validators;

namespace GSES.API
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
            services.AddDefaultAWSOptions(Configuration.GetAWSOptions());

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "GSES.API", Version = "v1" });
            });

            services.AddHttpClient();
            services.AddTransient<SmtpClient>((serviceProvider) =>
            {
                return new SmtpClient()
                {
                    Host = Configuration.GetValue<String>("Email:Smtp:Host"),
                    Port = Configuration.GetValue<int>("Email:Smtp:Port"),
                    Credentials = new NetworkCredential(
                            Configuration.GetValue<String>("Email:Smtp:Username"),
                            Configuration.GetValue<String>("Email:Smtp:Password")
                        )
                };
            });

            services.AddAWSService<IAmazonS3>();
            services.AddScoped<IStorage, S3FileStorage>();
            services.AddTransient<ISubscriberRepository, SubscriberRepository>();
            services.AddValidatorsFromAssemblyContaining<SubscriberValidator>();

            services.AddTransient<IRateProcessor, RateProcessor>();
            services.AddTransient<IRateService, RateService>();
            services.AddTransient<ISubscriberService, SubscriberService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "GSES.API v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
