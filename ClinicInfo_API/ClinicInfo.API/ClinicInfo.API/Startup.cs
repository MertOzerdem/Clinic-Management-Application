using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClinicInfo.API.Entities;
using ClinicInfo.API.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace ClinicInfo.API
{
    public class Startup
    {
        public static IConfiguration Configuration { get; private set; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddCors(c =>
            //{
            //    c.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader().AllowCredentials());
            //});
            //services.AddCors();
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials()
                    );
            });

            services.AddMvc()
                .AddMvcOptions(o => o.OutputFormatters.Add(
                    new XmlDataContractSerializerOutputFormatter()));

            //var connectionString = Startup.Configuration["connectionStrings:PatientInfoDBConnectionString"];
            //services.AddDbContext<PatientInfoContext>(o => o.UseSqlServer(connectionString));
            var connection = @"Server=(localdb)\mssqllocaldb;Database=PatientInfoDB;Trusted_Connection=True;ConnectRetryCount=0";
            services.AddDbContext<PatientInfoContext>(options => options.UseSqlServer(connection));
            services.AddScoped<IPatientInfoRepository, PatientInfoRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, PatientInfoContext patientInfoContext)
        {
            //app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            //app.UseCors("AllowOrigin");
            app.UseCors("CorsPolicy");

            patientInfoContext.EnsureSeedDataForContext();
            app.UseStatusCodePages();
            app.UseMvc();

            AutoMapper.Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Models.ServiceTypeForCreateDto, Entities.ServiceType>();
                cfg.CreateMap<Entities.ServiceType, Models.ServiceTypeDto>();
                cfg.CreateMap<Entities.Service, Models.ServiceDto>();
                cfg.CreateMap<Entities.Patient, Models.PatientSimpleDto>();
                cfg.CreateMap<Models.PatientSimpleDto, Entities.Patient>();
                cfg.CreateMap<Models.PatientCreatorDto, Entities.Patient>();
                cfg.CreateMap<Entities.Payment, Models.SinglePaymentDto>();
                cfg.CreateMap<Entities.Notification, Models.NotificationDto>();
                cfg.CreateMap<Entities.Notification, Models.NotificationCreatorDto>();
                cfg.CreateMap<Models.NotificationCreatorDto, Entities.Notification>();
                cfg.CreateMap<Models.CartDto, Entities.Payment>();
                cfg.CreateMap<Entities.Payment, Models.CartDto>();
                
            });


            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Hello World!");
            });
        }
    }
}
