using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Middleware;
using SpaceX.LaunchPads.AutoMapperConfiguration;
using SpaceX.LaunchPads.SpaceXDataService;

namespace SpaceX.LaunchPads.Api
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
            var spaceXLaunchPadUri = Configuration["SpaceXLaunchPadUrl"].ToString();
            if (string.IsNullOrEmpty(spaceXLaunchPadUri)) throw new ArgumentNullException("SpaceXLaunchPadUrl is missing from the configuraiton.");

            //configure httpclientfactory with rety policy
            services.AddHttpClient<ILaunchPadRepository, LaunchPadServiceRepository>(
                client =>
                {
                    client.BaseAddress = new Uri(spaceXLaunchPadUri);
                    client.DefaultRequestHeaders
                        .Accept
                        .Add(new MediaTypeWithQualityHeaderValue("application/json"));

                })
            .AddPolicyHandler(HttpPolicy.NewRetryPolicy());

            services.AddAutoMapper(typeof(LaunchPadProfile).Assembly);

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            //order is important.
            app.UseRequestResponseLoggingMiddleware();
            app.UseExceptionHandlerMiddlware();

            if (env.IsDevelopment())
            {
                //app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
